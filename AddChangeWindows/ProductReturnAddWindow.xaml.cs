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
        bool view = false;
        ProductReturn productReturn;

        List<Sales> sales = SalesDA.RetrieveAllSales();
        public ProductReturnAddWindow()
        {
            InitializeComponent();
            btnAddChange.Content = "Add";
        }

        public ProductReturnAddWindow(Object productReturnA)
        {
            InitializeComponent();
            productReturn = (ProductReturn)productReturnA;
            btnAddChange.Content = "Change";
            view = true;
            txtSalesId.Text = productReturn.Sale_id.ToString();
            txtReturnReason.Text = productReturn.ReturnReason;
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

                    if (int.TryParse(txtSalesId.Text, out int number1))
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
                            if (view == false) ProductReturnDA.ProductReturnAdd(number1, txtReturnReason.Text);
                            else ProductReturnDA.ProductReturnChange(productReturn.Id, number1, txtReturnReason.Text);
                            this.Close();
                        }
                        else MessageBox.Show("Sale not found.");
                    }
                    else
                    {
                        MessageBox.Show("Enter a valid sales id.");
                    }
                }
                else
                {
                    MessageBox.Show("All fields must be filled in!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
