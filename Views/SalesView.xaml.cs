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
            UpdateInfo(SalesDA.RetrieveAllSales());

        }
        
        private void UpdateInfo(List<Sales> sales)
        {
            double sum = 0;
            foreach (Sales sales1 in sales)
            {
                sum = sum + sales1.Total;
            }
            tbQuantity.Text = $"Кількість продажей: {sales.Count()}";
            tbTotal.Text = $"Загальна сума: {sum}";
        }

        private void UpdateInfo(List<SalesList> salesLists)
        {
            double sum = 0;
            int quantity = 0;
            foreach (SalesList salesList in salesLists)
            {
                sum = sum + salesList.Total;
                quantity = quantity + salesList.Quantity;
            }
            tbQuantity.Text = $"Кількість товару: {quantity}";
            tbTotal.Text = $"Загальна сума: {sum}";
        }

        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(EmployeeDA.RetrieveAllEmployees().Select(employee => employee.Name));
            cb2 = new ObservableCollection<string>(CustomerDA.RetrieveAllCustomers().Select(customer => customer.Name));
            cb3 = new ObservableCollection<string>(ProductDA.RetrieveAllProducts().Select(product => product.Name));
            DataContext = null;
            DataContext = this;
        }


        private void btnShowSales_Click(object sender, RoutedEventArgs e)
        {
            DataGridColumns1();
            dataGrid.ItemsSource = SalesDA.RetrieveAllSales();
            ButtonInfoEnabled();
            UpdateLists();
            UpdateInfo(SalesDA.RetrieveAllSales());
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                    DataGridColumns2();
                    Sales sales = (Sales)dataGrid.SelectedItem;
                    dataGrid.ItemsSource = SalesListDA.RetrieveSalesList(sales.Id);
                    salesId = sales.Id;
                    ButtonInfoDisabled();
                    UpdateInfo(SalesListDA.RetrieveSalesList(sales.Id));
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
                        UpdateInfo(SalesDA.RetrieveAllSales());
                    }
                    else
                    {
                        SalesList salesList = (SalesList)dataGrid.SelectedItem;
                        dataGrid.ItemsSource = SalesListDA.SalesListDelete(salesList.Id, salesId);
                        ButtonInfoDisabled();
                        UpdateInfo(SalesListDA.RetrieveSalesList(salesId));
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
                        UpdateInfo(SalesDA.SalesSearch(search));

                    }
                    else
                    {
                        dataGrid.ItemsSource = SalesDA.RetrieveAllSales();
                        ButtonInfoEnabled();
                        UpdateInfo(SalesDA.RetrieveAllSales());
                    }
                }
                else
                {
                    if (txtSearch.Text != "")
                    {
                        string search = txtSearch.Text;
                        dataGrid.ItemsSource = SalesListDA.SalesListSearch(salesId, search);
                        ButtonInfoDisabled();
                        UpdateInfo(SalesListDA.SalesListSearch(salesId, search));
                    }
                    else
                    {
                        dataGrid.ItemsSource = SalesListDA.RetrieveSalesList(salesId);
                        ButtonInfoDisabled();
                        UpdateInfo(SalesListDA.RetrieveSalesList(salesId));
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
            if (cbEmployee.Text != "" || cbCustomer.Text != "" || dpDateFrom.SelectedDate != null || dpDateTo.SelectedDate != null || cbProduct.Text != "" || (txtQuantityFrom.Text != "" && txtQuantityFrom.Text != "від") || (txtQuantityTo.Text != "" && txtQuantityTo.Text != "до"))
            {
                if ((double.TryParse(txtQuantityFrom.Text, out double number3) && number3 >= 0 && (txtQuantityTo.Text == "до" || txtQuantityTo.Text == "")) || (double.TryParse(txtQuantityTo.Text, out double number4) && number4 >= 0 && (txtQuantityFrom.Text == "від" || txtQuantityFrom.Text == "")) || (double.TryParse(txtQuantityFrom.Text, out number3) && double.TryParse(txtQuantityTo.Text, out number4)) || ((txtQuantityFrom.Text == "від" || txtQuantityFrom.Text == "") && (txtQuantityTo.Text == "до" || txtQuantityTo.Text == "")))
                {
                    DateTime? dateFrom = null;
                    if (dpDateFrom.SelectedDate.HasValue) dateFrom = (DateTime)dpDateFrom.SelectedDate;
                    DateTime? dateTo = null;
                    if (dpDateTo.SelectedDate.HasValue) dateTo = (DateTime)dpDateTo.SelectedDate;
                    if ((dateFrom != null && dateFrom > DateTime.Now) || (dateTo != null && dateTo > DateTime.Now))
                        MessageBox.Show("Невірна дата.");
                    else
                    {
                        DataGridColumns1();
                        dataGrid.ItemsSource = SalesDA.SalesFilter(cbEmployee.Text, cbCustomer.Text, dateFrom, dateTo, cbProduct.Text, txtQuantityFrom.Text, txtQuantityTo.Text);
                        ButtonInfoEnabled();
                        UpdateInfo(SalesDA.SalesFilter(cbEmployee.Text, cbCustomer.Text, dateFrom, dateTo, cbProduct.Text, txtQuantityFrom.Text, txtQuantityTo.Text));
                    }
                }
                else
                {
                    MessageBox.Show("У полях кількості вводьте тільки позитивні числа!");
                }
            }
            else
            {
                DataGridColumns1();
                dataGrid.ItemsSource = SalesDA.RetrieveAllSales();
                ButtonInfoEnabled();
                UpdateInfo(SalesDA.RetrieveAllSales());
            }
        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            cbProduct.Text = string.Empty;
            cbEmployee.Text = string.Empty;
            cbCustomer.Text = string.Empty;
            dpDateFrom.Text = string.Empty;
            dpDateTo.Text = string.Empty;
            txtQuantityFrom.Text = "від";
            txtQuantityFrom.Foreground = Brushes.Gray;
            txtQuantityTo.Text = "до";
            txtQuantityTo.Foreground = Brushes.Gray;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                if (view == true)
                {
                    SalesAddWindow AddWindow = new SalesAddWindow(dataGrid.SelectedItem);
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
        private void DataGridColumns1()
        {
            var columns = new List<DataGridColumn>
            {
            new DataGridTextColumn { Header = "Номер продажу", Binding = new Binding("Id") },
            new DataGridTextColumn { Header = "Співробітник", Binding = new Binding("Employee") },
            new DataGridTextColumn { Header = "Клієнт", Binding = new Binding("Customer") },
            new DataGridTextColumn { Header = "Дата та час продажу", Binding = new Binding("TimeOfSale") },
            new DataGridTextColumn { Header = "Проданий товар", Binding = new Binding("SoldItems") },
            new DataGridTextColumn { Header = "Загальна сума, грн", Binding = new Binding("Total") }
            };

            // Очищаємо поточні стовпці
            dataGrid.Columns.Clear();

            // Додаємо нові стовпці до DataGrid
            foreach (var column in columns)
            {
                dataGrid.Columns.Add(column);
            }
        }
        private void DataGridColumns2()
        {
            var columns = new List<DataGridColumn>
            {
            new DataGridTextColumn { Header = "Товар", Binding = new Binding("Product") },
            new DataGridTextColumn { Header = "Розмір", Binding = new Binding("Size") },
            new DataGridTextColumn { Header = "Кольор", Binding = new Binding("Color") },
            new DataGridTextColumn { Header = "Кількість", Binding = new Binding("Quantity") },
            new DataGridTextColumn { Header = "Ціна, грн", Binding = new Binding("Price") },
            new DataGridTextColumn { Header = "Загальна сума, грн", Binding = new Binding("Total") }
            };

            // Очищаємо поточні стовпці
            dataGrid.Columns.Clear();

            // Додаємо нові стовпці до DataGrid
            foreach (var column in columns)
            {
                dataGrid.Columns.Add(column);
            }
        }

        private void txtQuantityFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtQuantityFrom.Foreground = Brushes.Black;
        }

        private void txtQuantityTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtQuantityTo.Foreground = Brushes.Black;
        }

    }
}
