using Shoe_Store_DB.AddChangeWindows;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;
using Shoe_Store_DB.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SalesView.xaml
    /// </summary>
    public partial class SalesView : UserControl
    {
        bool view = true;
        int salesId;
        Style styleButton;

        public ObservableCollection<string> cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }
        public ObservableCollection<string> cb3 { get; set; }

        public SalesView()
        {
            InitializeComponent();
            dataGrid.ItemsSource = SalesDA.RetrieveAllSales();
            view = true;
            styleButton = btnInfo.Style;
            UpdateLists();

        }

        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(EmployeeDA.RetrieveAllEmployees().Select(employee => employee.Name));
            cb2 = new ObservableCollection<string>(CustomerDA.RetrieveAllCustomers().Select(customer => customer.Name));
            cb3 = new ObservableCollection<string>(ProductDA.RetrieveAllProducts().Select(product => product.Name));
            DataContext = this;
        }


        private void btnShowSales_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = SalesDA.RetrieveAllSales();
            ButtonInfoEnabled();
            UpdateLists();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                    Sales sales = (Sales)dataGrid.SelectedItem;
                    dataGrid.ItemsSource = SalesListDA.RetrieveSalesList(sales.Id);
                    salesId = sales.Id;
                    ButtonInfoDisabled();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                    if (view == true)
                    {
                        Sales sales = (Sales)dataGrid.SelectedItem;
                        dataGrid.ItemsSource = SalesDA.SalesDelete(sales.Id);
                        ButtonInfoEnabled();
                    }
                    else
                    {
                        SalesList salesList = (SalesList)dataGrid.SelectedItem;
                        dataGrid.ItemsSource = SalesListDA.SalesListDelete(salesList.Id, salesId);
                        ButtonInfoDisabled();
                    }
                }
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (view == true)
                {
                    if (txtSearch.Text != "")
                    {
                        string search = txtSearch.Text;
                        dataGrid.ItemsSource = SalesDA.SalesSearch(search);
                        ButtonInfoEnabled();

                    }
                    else
                    {
                        dataGrid.ItemsSource = SalesDA.RetrieveAllSales();
                        ButtonInfoEnabled();
                    }
                }
                else
                {
                    if (txtSearch.Text != "")
                    {
                        string search = txtSearch.Text;
                        dataGrid.ItemsSource = SalesListDA.SalesListSearch(salesId, search);
                        ButtonInfoDisabled();

                    }
                    else
                    {
                        dataGrid.ItemsSource = SalesListDA.RetrieveSalesList(salesId);
                        ButtonInfoDisabled();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void ButtonInfoEnabled()
        {
            view = true;
            btnInfo.IsEnabled = true;
            btnInfo.ClearValue(Button.BackgroundProperty);
            btnInfo.Foreground = Brushes.White;
            btnInfo.Style = styleButton;
        }
        private void ButtonInfoDisabled()
        {
            view = false;
            btnInfo.IsEnabled = false;
            btnInfo.Background = Brushes.LightGray;
            btnInfo.Foreground = Brushes.Black;
        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (cbEmployee.Text != "" || cbCustomer.Text != "" || dpDateFrom.SelectedDate != null || dpDateTo.SelectedDate != null || cbProduct.Text != "")
            {
                DateTime? dateFrom = null;
                if (dpDateFrom.SelectedDate.HasValue) dateFrom = (DateTime)dpDateFrom.SelectedDate;
                DateTime? dateTo = null;
                if (dpDateTo.SelectedDate.HasValue) dateTo = (DateTime)dpDateTo.SelectedDate;
                dataGrid.ItemsSource = SalesDA.SalesFilter(cbEmployee.Text, cbCustomer.Text, dateFrom, dateTo, cbProduct.Text);
                ButtonInfoEnabled();
            }
            else
            {
                dataGrid.ItemsSource = SalesDA.RetrieveAllSales();
                ButtonInfoEnabled();
            }
        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            cbProduct.Text = string.Empty;
            cbEmployee.Text = string.Empty;
            cbCustomer.Text = string.Empty;
            dpDateFrom.Text = string.Empty;
            dpDateTo.Text = string.Empty;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                if (view == true)
                {
                    ProductAddWindow AddWindow = new ProductAddWindow(dataGrid.SelectedItem);
                    AddWindow.Show();
                }
                else
                {
                    SalesListAddWindow AddWindow = new SalesListAddWindow(salesId, dataGrid.SelectedItem);
                    AddWindow.Show();
                }
            }
                
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (view == true)
            {
                SalesAddWindow AddWindow = new SalesAddWindow();
                AddWindow.Show();
            }
            else
            {
                SalesListAddWindow AddWindow = new SalesListAddWindow(salesId);
                AddWindow.Show();
            }
        }
    }
}
