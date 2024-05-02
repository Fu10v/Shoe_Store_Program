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
    /// Interaction logic for SupplierView.xaml
    /// </summary>
    public partial class SupplierView : UserControl
    {
        public SupplierView()
        {
            InitializeComponent();
            dataGrid.ItemsSource = SupplierDA.RetrieveAllSuppliers();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtSearch.Text != "")
                {
                    string search = txtSearch.Text;
                    dataGrid.ItemsSource = SupplierDA.SupplierSearch(search);

                }
                else
                {
                    dataGrid.ItemsSource = SupplierDA.RetrieveAllSuppliers();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = SupplierDA.RetrieveAllSuppliers();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                SupplierAddWindow AddWindow = new SupplierAddWindow(dataGrid.SelectedItem);
                AddWindow.Show();
            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SupplierAddWindow AddWindow = new SupplierAddWindow();
            AddWindow.Show();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                try
                {
                    Supplier supplier = (Supplier)dataGrid.SelectedItem;
                    dataGrid.ItemsSource = SupplierDA.SupplierDelete(supplier.Id);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }

        }
    }
}
