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
    /// Interaction logic for SalesAddWindow.xaml
    /// </summary>
    public partial class SalesAddWindow : Window
    {
        List<Employee> employees = EmployeeDA.RetrieveAllEmployeesCashiers();
        List<Customer> customers = CustomerDA.RetrieveAllCustomers();

        bool view = false;

        public ObservableCollection<string> cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }
        public ObservableCollection<string> cb3 { get; set; }

        private Sales sales;

        public SalesAddWindow()
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Оформити";
        }

        public SalesAddWindow(Object salesA)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Змінити";
            sales = (Sales)salesA;
            cbEmployee.Text = sales.Employee;
            cbCustomer.Text = sales.Customer;
            txtTimeOfSale.Text = Convert.ToString(sales.TimeOfSale);
            view = true;
        }
        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(EmployeeDA.RetrieveAllEmployeesCashiers().Select(employee => employee.Name));
            cb2 = new ObservableCollection<string>(CustomerDA.RetrieveAllCustomers().Select(customer => customer.Name));
            cb3 = new ObservableCollection<string>(ProductDA.RetrieveAllProducts().Select(product => product.Name));
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
                if (cbEmployee.Text != "" && txtTimeOfSale.Text != "" )
                {
                    if (DateTime.TryParse(txtTimeOfSale.Text, out var timeOfSale))
                    {
                        if (timeOfSale > DateTime.Now)
                        {
                            MessageBox.Show("Введена дата не повинна перевизувати поточну.");
                        }
                        else
                        {
                            int i1 = -1;
                            int i2 = -1;
                            foreach (Classes.Employee employee in employees)
                            {
                                if (employee.Name == cbEmployee.Text)
                                {
                                    i1 = employee.Id;
                                }
                            }
                            foreach (Classes.Customer customer in customers)
                            {
                                if (customer.Name == cbCustomer.Text)
                                {
                                    i2 = customer.Id;
                                }
                            }
                            if (i1 != -1)
                            {
                                if (i2 == -1 && cbCustomer.Text != "")
                                {
                                    MessageBox.Show("Клієнта не знайдено");
                                }
                                else
                                {
                                    if (i1 != -1 && i2 == -1)
                                    {
                                        if (view == false) SalesDA.SalesAdd(i1, timeOfSale);
                                        else SalesDA.SalesChange(sales.Id, i1, timeOfSale);
                                        this.Close();
                                    }
                                    else
                                    {

                                        if (view == false) SalesDA.SalesAdd(i1, i2, timeOfSale);
                                        else SalesDA.SalesChange(sales.Id, i1, i2, timeOfSale);
                                        this.Close();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Співробітника не знайдено.");
                            }
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Неправильний формат дати.");
                    }
                }
                else
                {
                    MessageBox.Show("Співробітник і час продажу обов'язкові!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void txtTimeOfSale_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTimeOfSale.Text != "" && txtTimeOfSale.Text != "дд.мм.рррр гг:хм")
            {
                txtTimeOfSale.Foreground = Brushes.Black;
            }
            else
            {
                txtTimeOfSale.Text = "дд.мм.рррр гг:хм";
                txtTimeOfSale.Foreground = Brushes.Gray;
            }
        }
    }
}

