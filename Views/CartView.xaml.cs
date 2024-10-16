using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Shoe_Store_DB.AddChangeWindows;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;
using Shoe_Store_DB.View_Layer;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using iTextSharp;
using LiveCharts.Wpf;
using System.Globalization;

namespace Shoe_Store_DB.Views
{
    /// <summary>
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : UserControl
    {
        List<Employee> employees = EmployeeDA.RetrieveAllEmployeesCashiers();
        List<Customer> customers = CustomerDA.RetrieveAllCustomers();

        double discount = 0;
        double totalWithDiscount = 0;
        double total = 0;
        int customerId = -1;
        Customer customer;
        bool paymentDiscount = false;

        public ObservableCollection<string> cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }

        public CartView()
        {
            InitializeComponent();
            dataGridProduct.Items.Clear();
            dataGridProduct.ItemsSource = DBWindow.CartList;
            UpdateLists();
            UpdateInfo();
            CultureInfo ukrainianCulture = new CultureInfo("uk-UA");
            ukrainianCulture.NumberFormat.CurrencySymbol = "₴";
            System.Threading.Thread.CurrentThread.CurrentCulture = ukrainianCulture;
        }

        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(EmployeeDA.RetrieveAllEmployeesCashiers().Select(employee => employee.Name));
            cb2 = new ObservableCollection<string>(CustomerDA.RetrieveAllCustomers().Select(customer => customer.Name));
            DataContext = null;
            DataContext = this;
        }

        private void UpdateInfo()
        {
            total = 0;
            foreach (ProductCart productCart in DBWindow.CartList)
            {
                total = total + productCart.Total;
            }
            tbQuantity.Text = $"Кількість продажей: {DBWindow.CartList.Count()}";
            tbTotal.Text = $"Загальна сума: {total}";
            double discountU = total * discount / 100;
            totalWithDiscount = total - discountU;
            tbTotalWithDisc.Text = $"Сума зі хнижкою, грн: {totalWithDiscount}";
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            dataGridProduct.ItemsSource = null;
            dataGridProduct.Items.Clear();
            dataGridProduct.ItemsSource = DBWindow.CartList;
            tbChange.Text = "Решта, грн: ";
            chbDiscount.IsEnabled = false;
            txtPayment.Text = "";
            cbCustomer.Text = "";
            cbEmployee.Text = "";
            UpdateLists();
            UpdateInfo();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            QuantityWindow quantityWindow = new QuantityWindow(dataGridProduct.SelectedItem);
            quantityWindow.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ProductCart product = dataGridProduct.SelectedItem as ProductCart;
            DBWindow.CartList.Remove(product);
            dataGridProduct.ItemsSource = null;
            dataGridProduct.Items.Clear();
            dataGridProduct.ItemsSource = DBWindow.CartList;
            UpdateInfo();

        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {

            if (cbEmployee.Text != "")
            {
                int i1 = -1;
                foreach (Classes.Employee employee in employees)
                {
                    if (employee.Name == cbEmployee.Text)
                    {
                        i1 = employee.Id;
                    }
                }
                if (i1 != -1)
                {
                    if (customerId == -1 && cbCustomer.Text != "")
                    {
                        MessageBox.Show("Клієнта не знайдено");
                    }
                    else
                    {
                        if (int.TryParse(txtPayment.Text, out int payment) && payment >= 0)
                        {
                            if ((chbDiscount.IsChecked == false && payment < total) || (chbDiscount.IsChecked == true && payment < totalWithDiscount))
                            {
                                MessageBox.Show("Недостатньо коштів.");
                            }
                            else
                            {
                                if (i1 != -1 && customerId == -1)
                                {
                                    SalesDA.SalesAdd(i1, DateTime.Now);
                                }
                                else
                                {
                                    SalesDA.SalesAdd(i1, customerId, DateTime.Now);
                                }
                                List<Sales> sales = SalesDA.RetrieveAllSales();
                                Sales sale = sales[0];
                                foreach (ProductCart productCart in DBWindow.CartList)
                                {
                                    ProductQuantityDA.ProductQuantityChangeQuantityDown(productCart.PrductQuantityId, productCart.Quantity);
                                    SalesListDA.SalesListAdd(sale.Id, productCart.PrductQuantityId, productCart.Price, productCart.Quantity);
                                }
                                if (cbCustomer.Text != "")
                                {
                                    string[] customerName = customer.Name.Split(' ');
                                    if (chbDiscount.IsChecked == true)
                                    {
                                        CustomerDA.CustomerChange(customer.Id, customerName[0], customerName[1], customerName[2], customer.PhoneNumber, customer.Email, customer.DiscountCardId, 0);
                                    }
                                    else CustomerDA.CustomerChange(customer.Id, customerName[0], customerName[1], customerName[2], customer.PhoneNumber, customer.Email, customer.DiscountCardId, customer.DiscountCardAccumulation + total);

                                }
                                
                                CheckCreate(sale.Id);
                                DBWindow.CartList.Clear();
                                MessageBox.Show("Покупку оформлено.");
                                dataGridProduct.ItemsSource = null;
                                dataGridProduct.Items.Clear();
                                dataGridProduct.ItemsSource = DBWindow.CartList;
                                tbChange.Text = "Решта, грн: ";
                                chbDiscount.IsEnabled = false;
                                txtPayment.Text = "";
                                cbCustomer.Text = "";
                                cbEmployee.Text = "";
                                UpdateInfo();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введіть позитивне чісло в оплату або 0.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Співробітника не знайдено.");
                }
            }
            else
            {
                MessageBox.Show("Співробітник обов'язковий!");
            }

        }

        private void cbCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCustomer = "";
            if (cbCustomer.SelectedValue != null)
                selectedCustomer = cbCustomer.SelectedValue.ToString();

            if (selectedCustomer != "")
            {
                customerId = -1;
                foreach (Customer customer1 in customers)
                {
                    if (customer1.Name == selectedCustomer)
                    {
                        customerId = customer1.Id;
                    }
                }
                if (customerId != -1)
                {
                    customer = CustomerDA.RetrieveCustomer(customerId)[0];
                    if (customer.DiscountCardAccumulation > 15000)
                    {
                        discount = 15;
                    }
                    else if (customer.DiscountCardAccumulation < 1000)
                    {
                        discount = 0;
                    }
                    else
                    {
                        discount = (int)customer.DiscountCardAccumulation / 1000;
                    }
                    tbDiscount.Text = $"Знижка, %: {discount}";
                    chbDiscount.IsEnabled = true;
                    UpdateInfo();
                }
            }
            else
            {
                customerId = -1;
                discount = 0;
                chbDiscount.IsEnabled = false;
                tbDiscount.Text = $"Знижка, %: {0}";
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtPayment.Text, out int payment) && payment >= 0)
            {
                if (chbDiscount.IsChecked == false)
                    tbChange.Text = $"Решта, грн: {Math.Round(payment - total, 2)}";
                else tbChange.Text = $"Решта, грн: {Math.Round(payment - totalWithDiscount, 2)}";

            }

        }

        public void CheckCreate(int salesId)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(folderPath, $"{salesId}.pdf");

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Створення PDF документа
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\times.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, 14);
                // Відкриття документа для запису
                document.Open();
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 250f, 270f }); // Встановлюємо відносні ширини для колонок

                // Додавання логотипу магазину
                PdfPCell logoCell = new PdfPCell();
                string logoPath = "C:\\Users\\PPand\\source\\repos\\Shoe_Store_DB\\Images\\logo.png"; // Замініть на фактичний шлях до логотипу
                if (File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(220, 100);
                    logoCell.AddElement(logo);
                }
                logoCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                logoCell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                logoCell.BorderWidthBottom = 1f;
                headerTable.AddCell(logoCell);
                

                // Додавання інформації про магазин
                PdfPCell infoCell = new PdfPCell();
                infoCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                infoCell.PaddingLeft = 10f; // Задаємо відступ для правильного відображення
                infoCell.AddElement(new Paragraph("Магазин Sole Haven", font));
                infoCell.AddElement(new Paragraph("Адреса:", font));
                infoCell.AddElement(new Paragraph("Харків, Хірківська обл., вул. Китаєнка", font));
                infoCell.AddElement(new Paragraph("Номер телефону: (123) 456-7890", font));
                infoCell.AddElement(new Paragraph(" ")); // Додати порожній рядок
                infoCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                infoCell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                infoCell.BorderWidthBottom = 1f;
                headerTable.AddCell(infoCell);
                document.Add(headerTable);
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph($"Номер продажу: {salesId}", font));
                document.Add(new Paragraph($"Дата: {new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)}", font));
                document.Add(new Paragraph(" "));
                // Додавання перерахунку куплених товарів
                PdfPTable table = new PdfPTable(4); // 4 колонки для найменування, кількості, ціни та загальної суми
                table.WidthPercentage = 100;
                table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                PdfPCell pdfPCell1 = new PdfPCell(new Phrase("Товар", font));
                pdfPCell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(pdfPCell1);
                PdfPCell pdfPCell2 = new PdfPCell(new Phrase("Кількість", font));
                pdfPCell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(pdfPCell2);
                PdfPCell pdfPCell3 = new PdfPCell(new Phrase("Ціна, грн", font));
                pdfPCell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(pdfPCell3);
                PdfPCell pdfPCell4 = new PdfPCell(new Phrase("Загальна сума, грн", font));
                pdfPCell4.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(pdfPCell4);

                // Отримання списку товарів з DBWindow.CartList
                 // Замініть це на фактичний спосіб отримання списку товарів

                double totalAmount = 0;

                foreach (ProductCart product in DBWindow.CartList)
                {
                    table.AddCell(product.Name);
                    table.AddCell(product.Quantity.ToString());
                    table.AddCell(product.Price.ToString("C"));
                    table.AddCell(product.Total.ToString("C"));

                    totalAmount += product.Total;
                }

                document.Add(table);

                // Додавання підсумкової інформації
                if (chbDiscount.IsChecked == true)
                {
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph($"Загальна сума, грн: {total}", font));
                    document.Add(new Paragraph($"Знижка, грн: {discount * total / 100}", font));
                    document.Add(new Paragraph($"Сума зі знижкою, грн: {totalWithDiscount}", font));
                }
                else
                {
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph($"Загальна сума, грн: {total:C}", font));
                }
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph($"Дякуємо за покупку в Sole Haven!", font));
                // Закриття документа
                document.Close();
            }

            MessageBox.Show("Чек створено за шляхом: " + filePath);
        }




    }
}
