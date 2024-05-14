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
        public StatisticView()
        {
            int[] salesQuantity = SalesDA.RetrieveAllSalesDateQuantity().Select(sales => sales.Quantity).ToArray();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {

                    Values = new ChartValues<int> (salesQuantity)
                }
            };
            
            DateTime[] salesTimes = SalesDA.RetrieveAllSalesDateQuantity().Select(sales => sales.TimeOfSale).ToArray();
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
            InitializeComponent();
        }

        private void btnShowStatistic_Click(object sender, RoutedEventArgs e)
        {
            int[] salesQuantity = SalesDA.RetrieveAllSalesDateQuantity().Select(sales => sales.Quantity).ToArray();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {

                    Values = new ChartValues<int> (salesQuantity)
                }
            };

            DateTime[] salesTimes = SalesDA.RetrieveAllSalesDateQuantity().Select(sales => sales.TimeOfSale).ToArray();
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
        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
