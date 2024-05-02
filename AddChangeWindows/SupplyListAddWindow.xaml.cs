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
    /// Interaction logic for SupplyListAddWindow.xaml
    /// </summary>
    public partial class SupplyListAddWindow : Window
    {
        int invoiceId;
        bool view = false;
        List<Product> products = ProductDA.RetrieveAllProducts();
        SupplyList supplyList;
        public ObservableCollection<string> cb1 { get; set; }
        public SupplyListAddWindow(int invoiceId)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Add";
            this.invoiceId = invoiceId;
        }

        public SupplyListAddWindow(int invoiceId, Object salesListA)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Change";
            view = true;
            this.invoiceId = invoiceId;
            supplyList = (SupplyList)salesListA;
            cbProduct.Text = supplyList.Product;
            txtQuantity.Text = supplyList.Quantity.ToString();
            txtPrice.Text = supplyList.Price.ToString();
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
                if (cbProduct.Text != "" && txtQuantity.Text != "" && txtPrice.Text != "")
                {
                    

                    if (int.TryParse(txtPrice.Text, out int price) && price >= 0)
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
                                if (view == false) SupplyListDA.SupplyListAdd(invoiceId, i1, price, quantity);
                                else SupplyListDA.SupplyListChange(supplyList.Id, invoiceId, i1, price, quantity);
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
                        MessageBox.Show("Please enter only positive numeric or 0 for price.");
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
