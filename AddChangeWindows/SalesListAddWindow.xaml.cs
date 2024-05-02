using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Shoe_Store_DB.AddChangeWindows
{
    /// <summary>
    /// Interaction logic for SalesList.xaml
    /// </summary>
    public partial class SalesListAddWindow : Window
    {
        int salesId;
        bool view = false;
        List<Product> products = ProductDA.RetrieveAllProducts();
        SalesList salesList;
        public ObservableCollection<string> cb1 { get; set; }
        public SalesListAddWindow(int salesId)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Add";
            this.salesId = salesId;
        }

        public SalesListAddWindow(int salesId, Object salesListA)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Change";
            view = true;
            this.salesId = salesId;
            salesList = (SalesList)salesListA;
            cbProduct.Text = salesList.Product;
            txtQuantity.Text = salesList.Quantity.ToString();
        }

        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(ProductDA.RetrieveAllProducts().Select(product => product.Name));
            DataContext = this;
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
                if (cbProduct.Text != "" && txtQuantity.Text != "")
                {
                    if (int.TryParse(txtQuantity.Text, out int quantity) && quantity >= 0)
                    {
                        int i1 = -1;
                        foreach (Classes.Product product in products)
                        {
                            if (product.Name == cbProduct.Text)
                            {
                                i1 = product.Id;
                            }
                        }
                        if (i1 != -1)
                        {
                            if (view == false) SalesListDA.SalesListAdd(salesId, i1, quantity);
                            else SalesListDA.SalesListChange(salesList.Id, salesId, i1, quantity);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect product.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter only positive numeric or 0 for quantity.");
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
