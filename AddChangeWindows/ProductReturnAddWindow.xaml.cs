using Accessibility;
using Org.BouncyCastle.Crypto.Generators;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Shoe_Store_DB.AddChangeWindows
{
    /// <summary>
    /// Interaction logic for ProductReturnAddWindow.xaml
    /// </summary>
    public partial class ProductReturnAddWindow : Window
    {
        int isListChecked = -1;
        bool view = false;
        ProductReturn productReturn;
        public static List<Object> salesLists;
        List<Sales> sales = SalesDA.RetrieveAllSales();
        List<ProductReturn> productReturns = ProductReturnDA.RetrieveAllProductReturns();
        int saleId;
        DateTime saleDate;
        public ProductReturnAddWindow()
        {
            InitializeComponent();
            btnAddChange.Content = "Оформити";
            btnChange.IsEnabled = true;
            btnChange.Background = Brushes.Black;
            btnChange.Foreground = Brushes.White;
            btnDelete.IsEnabled = true;
            btnDelete.Background = Brushes.Black;
            btnDelete.Foreground = Brushes.White;
        }

        public ProductReturnAddWindow(Object productReturnA)
        {
            InitializeComponent();
            productReturn = (ProductReturn)productReturnA;
            btnAddChange.Content = "Змінити";
            view = true;
            txtSalesId.Text = productReturn.SaleId.ToString();
            txtReturnReason.Text = productReturn.ReturnReason;
            btnChange.IsEnabled = false;
            btnChange.Background = Brushes.LightGray;
            btnChange.Foreground = Brushes.Black;
            btnDelete.IsEnabled = false;
            btnDelete.Background = Brushes.LightGray;
            btnDelete.Foreground = Brushes.Black;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSalesId.Text != "" && txtReturnReason.Text != "")
                {

                    if (int.TryParse(txtSalesId.Text, out int number1) && number1 >= 0)
                    {
                        bool saleFind = false;
                        foreach (Classes.Sales sale in sales)
                        {
                            if (sale.Id == number1)
                            {
                                saleFind = true;
                                saleId = sale.Id;
                                saleDate = sale.TimeOfSale;
                            }
                        }
                       
                        if (saleFind)
                        {
                            bool productReturnFind = false;
                            foreach (Classes.ProductReturn productReturn in productReturns)
                            {
                                if (productReturn.SaleId == number1)
                                {
                                    productReturnFind = true;
                                }
                            }
                            if (productReturnFind == true)
                            {
                                MessageBox.Show("Цей продаж вже повертали.");
                            }
                            else
                            {   
                                if (isListChecked != number1)
                                {
                                    MessageBox.Show("Оновіть товари.");
                                }
                                else
                                {
                                    TimeSpan timeSpan = DateTime.Now - saleDate;
                                    if (timeSpan.Days > 14)
                                    {
                                        MessageBoxResult result = MessageBox.Show("Пройшло більше 14 днів. Ви впевненні в поверненні товару?", "Підтвердження", MessageBoxButton.YesNo);
                                        if (result == MessageBoxResult.Yes)
                                        {
                                            if (view == false)
                                            {
                                                ProductReturnDA.ProductReturnAdd(number1, txtReturnReason.Text);
                                                List<ProductReturn> productReturns = ProductReturnDA.RetrieveAllProductReturns();
                                                ProductReturn productReturnI = productReturns[0];
                                                foreach (SalesList salesList in salesLists)
                                                {
                                                    ProductReturnListDA.ProductReturnListAdd(productReturnI.Id, salesList.Id, salesList.Quantity);
                                                }
                                            }
                                            else
                                            {
                                                ProductReturnDA.ProductReturnChange(productReturn.Id, number1, txtReturnReason.Text);
                                                List<ProductReturnList> productReturnList = ProductReturnListDA.RetrieveProductReturnList(productReturn.Id);
                                                foreach (SalesList salesList in salesLists)
                                                {
                                                    foreach (ProductReturnList productReturnList1 in productReturnList)
                                                    {
                                                        if (productReturnList1.SalesListId == salesList.Id)
                                                        {
                                                            ProductReturnListDA.ProductReturnListChange(productReturnList1.Id, productReturn.Id, salesList.Id, salesList.Quantity);
                                                            break;
                                                        }
                                                    }

                                                }
                                            }

                                            this.Close();
                                        }
                                    }
                                    else
                                    {

                                        if (view == false)
                                        {
                                            ProductReturnDA.ProductReturnAdd(number1, txtReturnReason.Text);
                                            List<ProductReturn> productReturns = ProductReturnDA.RetrieveAllProductReturns();
                                            ProductReturn productReturnI = productReturns[0];
                                            foreach (SalesList salesList in salesLists)
                                            {
                                                ProductReturnListDA.ProductReturnListAdd(productReturnI.Id, salesList.Id, salesList.Quantity);
                                            }
                                        }
                                        else
                                        {
                                            ProductReturnDA.ProductReturnChange(productReturn.Id, number1, txtReturnReason.Text);
                                            List<ProductReturnList> productReturnList = ProductReturnListDA.RetrieveProductReturnList(productReturn.Id);
                                            foreach (SalesList salesList in salesLists)
                                            {
                                                foreach (ProductReturnList productReturnList1 in productReturnList)
                                                {
                                                    if (productReturnList1.SalesListId == salesList.Id)
                                                    {
                                                        ProductReturnListDA.ProductReturnListChange(productReturnList1.Id, productReturn.Id, salesList.Id, salesList.Quantity);
                                                        break;
                                                    }
                                                }

                                            }
                                        }

                                        this.Close();
                                    }
                                }
                                
                            }
                            
                            
                        }
                        else MessageBox.Show("Продаж не знайдено.");
                    }
                    else
                    {
                        MessageBox.Show("Введіть коректний номер продажу.");
                    }
                }
                else
                {
                    MessageBox.Show("Всі поля повинні бути заповнені!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }


        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                QuantityReturnWindow quantityReturnWindow = new QuantityReturnWindow(dataGrid.SelectedItem);
                quantityReturnWindow.Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                SalesList salesList = (SalesList)dataGrid.SelectedItem;
                salesLists.Remove(salesList);
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = salesLists;
            }
        }

        private void btnFillDataGrid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtSalesId.Text, out int number1) && number1 >= 0)
                {
                    bool k1 = false;

                    foreach (Classes.Sales sale in sales)
                    {
                        if (sale.Id == number1)
                        {
                            k1 = true;
                        }
                    }
                    if (k1)
                    {
                        salesLists = SalesListDA.RetrieveSalesList(number1).Cast<object>().ToList();
                        dataGrid.ItemsSource = salesLists;
                        isListChecked = number1;
                    }
                    else MessageBox.Show("Продаж не знайдено.");
                }
                else
                {
                    MessageBox.Show("Введіть коректний номер продажу.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
