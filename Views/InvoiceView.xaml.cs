using Shoe_Store_DB.AddChangeWindows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shoe_Store_DB.Views
{
    /// <summary>
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    public partial class InvoiceView : UserControl
    {

        bool view = true;
        int invoiceId;
        Style styleButton;

        public ObservableCollection<string> cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }
        public ObservableCollection<string> cb3 { get; set; }

        public InvoiceView()
        {
            InitializeComponent();
            dataGrid.ItemsSource = InvoiceDA.RetrieveAllInvoices();
            view = true;
            styleButton = btnInfo.Style;
            UpdateLists();

        }

        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(EmployeeDA.RetrieveAllEmployees().Select(employee => employee.Name));
            cb2 = new ObservableCollection<string>(SupplierDA.RetrieveAllSuppliers().Select(supplier => supplier.Name));
            cb3 = new ObservableCollection<string>(ProductDA.RetrieveAllProducts().Select(product => product.Name));
            DataContext = this;
        }


        private void btnShowInvoices_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = InvoiceDA.RetrieveAllInvoices();
            ButtonInfoEnabled();
            UpdateLists();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                    Invoice invoice = (Invoice)dataGrid.SelectedItem;
                    dataGrid.ItemsSource = SupplyListDA.RetrieveSupplyList(invoice.Id);
                    invoiceId = invoice.Id;
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
                        Invoice invoice = (Invoice)dataGrid.SelectedItem;
                        dataGrid.ItemsSource = InvoiceDA.InvoiceDelete(invoice.Id);
                        ButtonInfoEnabled();
                    }
                    else
                    {
                        SupplyList supplyList = (SupplyList)dataGrid.SelectedItem;
                        dataGrid.ItemsSource = SupplyListDA.SupplyListDelete(supplyList.Id, invoiceId);
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
                        dataGrid.ItemsSource = InvoiceDA.InvoiceSearch(search);
                        ButtonInfoEnabled();

                    }
                    else
                    {
                        dataGrid.ItemsSource = InvoiceDA.RetrieveAllInvoices();
                        ButtonInfoEnabled();
                    }
                }
                else
                {
                    if (txtSearch.Text != "")
                    {
                        string search = txtSearch.Text;
                        dataGrid.ItemsSource = SupplyListDA.SupplyListSearch(invoiceId, search);
                        ButtonInfoDisabled();

                    }
                    else
                    {
                        dataGrid.ItemsSource = SupplyListDA.RetrieveSupplyList(invoiceId);
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
            if (cbEmployee.Text != "" || cbSupplier.Text != "" || dpDateFrom.SelectedDate != null || dpDateTo.SelectedDate != null || cbProduct.Text != "")
            {
                DateTime? dateFrom = null;
                if (dpDateFrom.SelectedDate.HasValue) dateFrom = (DateTime)dpDateFrom.SelectedDate;
                DateTime? dateTo = null;
                if (dpDateTo.SelectedDate.HasValue) dateTo = (DateTime)dpDateTo.SelectedDate;
                dataGrid.ItemsSource = InvoiceDA.InvoiceFilter(cbEmployee.Text, cbSupplier.Text, dateFrom, dateTo, cbProduct.Text);
                ButtonInfoEnabled();
            }
            else
            {
                dataGrid.ItemsSource = InvoiceDA.RetrieveAllInvoices();
                ButtonInfoEnabled();
            }
        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            cbProduct.Text = string.Empty;
            cbEmployee.Text = string.Empty;
            cbSupplier.Text = string.Empty;
            dpDateFrom.Text = string.Empty;
            dpDateTo.Text = string.Empty;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                if (view == true)
                {
                    InvoiceAddWindow AddWindow = new InvoiceAddWindow(dataGrid.SelectedItem);
                    AddWindow.Show();
                }
                else
                {
                    SupplyListAddWindow AddWindow = new SupplyListAddWindow(invoiceId, dataGrid.SelectedItem);
                    AddWindow.Show();
                }
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (view == true)
            {
                InvoiceAddWindow AddWindow = new InvoiceAddWindow();
                AddWindow.Show();
            }
            else
            {
                SupplyListAddWindow AddWindow = new SupplyListAddWindow(invoiceId);
                AddWindow.Show();
            }
        }
    }
}
