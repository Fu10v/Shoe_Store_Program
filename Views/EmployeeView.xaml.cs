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
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Shoe_Store_DB.Views
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        public string[] cb1 { get; set; }
        public string[] cb2 { get; set; }

        public EmployeeView()
        {
            InitializeComponent();
            dataGrid.ItemsSource = EmployeeDA.RetrieveAllEmployees();
            UpdateLists();
        }

        private void UpdateLists()
        {
            cb1 = new String[] { "менеджер", "продавець-консультант", "касир", "складський працівник" };

            cb2 = new String[] { "чоловік", "жінка" };
            DataContext = null;
            DataContext = this;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtSearch.Text != "")
                {
                    string search = txtSearch.Text;
                    dataGrid.ItemsSource = EmployeeDA.EmployeeSearch(search);

                }
                else
                {
                    dataGrid.ItemsSource = EmployeeDA.RetrieveAllEmployees();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            cbPosition.Text = string.Empty;
            cbGender.Text = string.Empty;
            dpDateFrom.Text = string.Empty;
            dpDateTo.Text = string.Empty;

        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (cbPosition.Text != "" || cbGender.Text != "" || dpDateFrom.SelectedDate != null || dpDateTo.SelectedDate != null) 
            {
                DateTime? dateFrom = null;
                if (dpDateFrom.SelectedDate.HasValue) dateFrom = (DateTime)dpDateFrom.SelectedDate;
                DateTime? dateTo = null;
                if (dpDateTo.SelectedDate.HasValue) dateTo = (DateTime)dpDateTo.SelectedDate;
                if ((dateFrom != null && dateFrom > DateTime.Now) || (dateTo != null && dateTo > DateTime.Now))
                    MessageBox.Show("Невірна дата.");
                else  dataGrid.ItemsSource = EmployeeDA.EmployeeFilter(cbPosition.Text, cbGender.Text, dateFrom, dateTo);
            }
            else dataGrid.ItemsSource = EmployeeDA.RetrieveAllEmployees();
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            UpdateLists();
            dataGrid.ItemsSource = EmployeeDA.RetrieveAllEmployees();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                EmployeeAddWindow AddWindow = new EmployeeAddWindow(dataGrid.SelectedItem);
                AddWindow.Show();
            }
                

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAddWindow AddWindow = new EmployeeAddWindow();
            AddWindow.Show();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                try
                {
                    Employee employee = (Employee)dataGrid.SelectedItem;
                    dataGrid.ItemsSource = EmployeeDA.EmployeeDelete(employee.Id);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
                
        }
    }
}
