using Shoe_Store_DB.AddChangeWindows;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;
using Shoe_Store_DB.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shoe_Store_DB.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {

        public CustomerView()
        {
            InitializeComponent();
            dataGrid.ItemsSource = CustomerDA.RetrieveAllCustomers();
        }


        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtSearch.Text != "")
                {
                    string search = txtSearch.Text;
                    dataGrid.ItemsSource = CustomerDA.CustomerSearch(search);

                }
                else
                {
                    dataGrid.ItemsSource = CustomerDA.RetrieveAllCustomers();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            txtFrom.Text = "від";
            txtFrom.Foreground = Brushes.Gray;
            txtTo.Text = "до";
            txtTo.Foreground = Brushes.Gray;
        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {

            if ((txtFrom.Text != "від" && txtFrom.Text != "") || (txtTo.Text != "до" && txtTo.Text != ""))
            {
                if ((double.TryParse(txtFrom.Text, out double number1) && number1 >= 0 && (txtTo.Text == "до" || txtTo.Text == "")) || (double.TryParse(txtTo.Text, out double number2) && number2 >= 0 && (txtFrom.Text == "від" || txtFrom.Text == "")) || (double.TryParse(txtFrom.Text, out number1) && double.TryParse(txtTo.Text, out number2)) || ((txtFrom.Text == "від" || txtFrom.Text == "") && (txtTo.Text == "до" || txtTo.Text == "")))
                {
                    if (double.TryParse(txtFrom.Text, out double from) && double.TryParse(txtTo.Text, out double to) && from > to)
                    {
                        MessageBox.Show("Некоректний діапазон накопичень.");
                    }
                    else
                    {
                        dataGrid.ItemsSource = CustomerDA.CustomerFilter(txtFrom.Text, txtTo.Text);
                    }
                    
                }
                else
                {
                    MessageBox.Show("У полях вводьте тільки позитивні числа!");
                }
            }
            else dataGrid.ItemsSource = CustomerDA.RetrieveAllCustomers();
        }
        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = CustomerDA.RetrieveAllCustomers();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                CustomerAddWindow AddWindow = new CustomerAddWindow(dataGrid.SelectedItem);
                AddWindow.Show();
            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CustomerAddWindow AddWindow = new CustomerAddWindow();
            AddWindow.Show();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                try
                {
                    Customer customer = (Customer)dataGrid.SelectedItem;
                    dataGrid.ItemsSource = CustomerDA.CustomerDelete(customer.Id);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }

        }

        private void txtPriceFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtFrom.Foreground = Brushes.Black;
        }

        private void txtPriceTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtTo.Foreground = Brushes.Black;
        }

    }
}

