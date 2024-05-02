using Shoe_Store_DB.AddChangeWindows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shoe_Store_DB.Views
{
    /// <summary>
    /// Interaction logic for ProductReturnView.xaml
    /// </summary>
    public partial class ProductReturnView : UserControl
    {
        public ProductReturnView()
        {
            InitializeComponent();
            dataGrid.ItemsSource = ProductReturnDA.RetrieveAllProductReturns();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtSearch.Text != "")
                {
                    string search = txtSearch.Text;
                    dataGrid.ItemsSource = ProductReturnDA.ProductReturnSearch(search);

                }
                else
                {
                    dataGrid.ItemsSource = ProductReturnDA.RetrieveAllProductReturns();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = ProductReturnDA.RetrieveAllProductReturns();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                ProductReturnAddWindow AddWindow = new ProductReturnAddWindow(dataGrid.SelectedItem);
                AddWindow.Show();
            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductReturnAddWindow AddWindow = new ProductReturnAddWindow();
            AddWindow.Show();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                try
                {
                    ProductReturn productReturn = (ProductReturn)dataGrid.SelectedItem;
                    dataGrid.ItemsSource = ProductReturnDA.ProductReturnDelete(productReturn.Id);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }

        }
    }
}