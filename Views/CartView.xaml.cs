using Shoe_Store_DB.AddChangeWindows;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;
using Shoe_Store_DB.View_Layer;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shoe_Store_DB.Views
{
    /// <summary>
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : UserControl
    {
        List<Employee> employees = EmployeeDA.RetrieveAllEmployees();
        List<Customer> customers = CustomerDA.RetrieveAllCustomers();

        double discount = 0;
        double totalWithDiscount = 0;
        double total = 0;
        int customerId;
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
        }

        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(EmployeeDA.RetrieveAllEmployees().Select(employee => employee.Name));
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
                                string[] customerName = customer.Name.Split(' ');
                                if (chbDiscount.IsChecked == true)
                                {
                                    CustomerDA.CustomerChange(customer.Id, customerName[0], customerName[1], customerName[2], customer.PhoneNumber, customer.Email, customer.DiscountCardId, 0);
                                }
                                else CustomerDA.CustomerChange(customer.Id, customerName[0], customerName[1], customerName[2], customer.PhoneNumber, customer.Email, customer.DiscountCardId, customer.DiscountCardAccumulation + total);
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
    }
}
