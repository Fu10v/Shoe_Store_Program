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
using LiveCharts;
using LiveCharts.Wpf;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;

namespace Shoe_Store_DB.Views
{
    /// <summary>
    /// Interaction logic for StatisticView.xaml
    /// </summary>
    public partial class StatisticView : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public ObservableCollection<string> cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }
        public ObservableCollection<string> cb3 { get; set; }

        public StatisticView()
        {
            InitializeComponent();
            txtQuantity.Text = "15";
            int.TryParse(txtQuantity.Text, out int quantity);
            string dateFrom = "";
            string dateTo = "";
            if (dpDateFrom.SelectedDate != null) 
            {
                DateTime dateTime = (DateTime)dpDateFrom.SelectedDate;
                dateFrom = $"{dateTime.Year} {dateTime.Month} {dateTime.Day}";
            }
            if (dpDateTo.SelectedDate != null)
            {
                DateTime dateTime = (DateTime)dpDateFrom.SelectedDate;
                dateTo = $"{dateTime.Year} {dateTime.Month} {dateTime.Day}";
            }
            double[] salesQuantity = StatisticDA.RetrieveAllSales(quantity, dateFrom, dateTo, "", "", "", "", "").Select(sales => sales.Total).ToArray();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {

                    Values = new ChartValues<double> (salesQuantity)
                }
            };
            
            DateTime[] salesTimes = StatisticDA.RetrieveAllSales(quantity, dateFrom, dateTo, "", "", "", "", "").Select(sales => sales.DateTime).ToArray();
            DateOnly[] salesDates = new DateOnly[salesTimes.Length];
            int i = 0;
            foreach (DateTime sales in salesTimes)
            {
                DateOnly dateOnly = new DateOnly(sales.Year, sales.Month, sales.Day);
                salesDates[i] = dateOnly;
                i++;
            }
            Labels = salesDates.Select(time => time.ToString()).ToArray();
            YFormatter = value => value.ToString();

            DataContext = this;
            UpdateLists();
        }

        private void btnShowStatistic_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
            {
                string dateFrom = "";
                string dateTo = "";
                if (dpDateFrom.SelectedDate != null)
                {
                    DateTime dateTime = (DateTime)dpDateFrom.SelectedDate;
                    dateFrom = $"{dateTime.Year} {dateTime.Month} {dateTime.Day}";
                }
                if (dpDateTo.SelectedDate != null)
                {
                    DateTime dateTime = (DateTime)dpDateFrom.SelectedDate;
                    dateTo = $"{dateTime.Year} {dateTime.Month} {dateTime.Day}";
                }
                double[] salesQuantity = StatisticDA.RetrieveAllSales(quantity, dateFrom, dateTo, "", "", "", "", "").Select(sales => sales.Total).ToArray();
                SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {

                        Values = new ChartValues<double> (salesQuantity)
                    }
                };

                DateTime[] salesTimes = StatisticDA.RetrieveAllSales(quantity, dateFrom, dateTo, "", "", "", "", "").Select(sales => sales.DateTime).ToArray();
                DateOnly[] salesDates = new DateOnly[salesTimes.Length];
                int i = 0;
                foreach (DateTime sales in salesTimes)
                {
                    DateOnly dateOnly = new DateOnly(sales.Year, sales.Month, sales.Day);
                    salesDates[i] = dateOnly;
                    i++;
                }
                Labels = salesDates.Select(time => time.ToString()).ToArray();
                YFormatter = value => value.ToString("F2");
                string cbProductTxt = cbProduct.Text;
                string cbBrandTxt = cbBrand.Text;
                string cbEmployeeTxt = cbEmployee.Text;
                DataContext = null;
                DataContext = this;
                cbProduct.Text = cbProductTxt;
                cbBrand.Text = cbBrandTxt;
                cbEmployee.Text = cbEmployeeTxt;
            }
            else
            {
                MessageBox.Show("В поле кількості відображень введіть позитивне число.");
            }
        }

        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(EmployeeDA.RetrieveAllEmployeesCashiers().Select(employe => employe.Name));
            cb2 = new ObservableCollection<string>(BrandDA.RetrieveAllBrands().Select(brand => brand.Name));
            cb3 = new ObservableCollection<string>(ProductDA.RetrieveAllProducts().Select(product => product.Name));
            DataContext = null;
            DataContext = this;
        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
            {
                DateTime dateTimeFrom;
                DateTime dateTimeTo;
                string dateFrom = "";
                string dateTo = "";
                if (dpDateFrom.SelectedDate != null)
                {
                    dateTimeFrom = (DateTime)dpDateFrom.SelectedDate;
                    dateFrom = $"{dateTimeFrom.Year} {dateTimeFrom.Month} {dateTimeFrom.Day}";
                }
                if (dpDateTo.SelectedDate != null)
                {
                    dateTimeTo = (DateTime)dpDateFrom.SelectedDate;
                    dateTo = $"{dateTimeTo.Year} {dateTimeTo.Month} {dateTimeTo.Day}";
                }
                if ((dpDateFrom.SelectedDate != null && dpDateFrom.SelectedDate > DateTime.Now) || (dpDateTo.SelectedDate != null && dpDateTo.SelectedDate > DateTime.Now))
                    MessageBox.Show("Введена дата не повинна перевищувати сьогоднішню дату.");
                else
                {
                    if (dpDateFrom.SelectedDate != null && dpDateTo.SelectedDate != null && dpDateFrom.SelectedDate > dpDateTo.SelectedDate)
                    {
                        MessageBox.Show("Некоректний діапазон дат.");
                    }
                    else
                    {
                        string[] name = cbEmployee.Text.Split(" ");
                        double[] salesQuantity;
                        if (name.Length == 3)
                        {
                            salesQuantity = StatisticDA.RetrieveAllSales(quantity, dateFrom, dateTo, name[0], name[1], name[2], cbProduct.Text, cbBrand.Text).Select(sales => sales.Total).ToArray();
                        }
                        else
                        {
                            salesQuantity = StatisticDA.RetrieveAllSales(quantity, dateFrom, dateTo, "", "", "", cbProduct.Text, cbBrand.Text).Select(sales => sales.Total).ToArray();
                        }
                        SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {

                        Values = new ChartValues<double> (salesQuantity)
                    }
                };

                        DateTime[] salesTimes = StatisticDA.RetrieveAllSales(quantity, dateFrom, dateTo, "", "", "", "", "").Select(sales => sales.DateTime).ToArray();
                        DateOnly[] salesDates = new DateOnly[salesTimes.Length];
                        int i = 0;
                        foreach (DateTime sales in salesTimes)
                        {
                            DateOnly dateOnly = new DateOnly(sales.Year, sales.Month, sales.Day);
                            salesDates[i] = dateOnly;
                            i++;
                        }
                        Labels = salesDates.Select(time => time.ToString()).ToArray();
                        YFormatter = value => value.ToString("F2");
                        string cbProductTxt = cbProduct.Text;
                        string cbBrandTxt = cbBrand.Text;
                        string cbEmployeeTxt = cbEmployee.Text;
                        DataContext = null;
                        DataContext = this;
                        cbProduct.Text = cbProductTxt;
                        cbBrand.Text = cbBrandTxt;
                        cbEmployee.Text = cbEmployeeTxt;
                    }
                }
                
            }
            else
            {
                MessageBox.Show("В поле кількості відображень введіть позитивне число.");
            }
        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            cbBrand.Text = string.Empty;
            cbProduct.Text = string.Empty;
            cbEmployee.Text = string.Empty;
            dpDateFrom.Text = string.Empty;
            dpDateTo.Text = string.Empty;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
    }
}
