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
    /// Interaction logic for InvoiceAddWindow.xaml
    /// </summary>
    public partial class InvoiceAddWindow : Window
    {
        List<Employee> employees = EmployeeDA.RetrieveAllEmployees();
        List<Supplier> suppliers = SupplierDA.RetrieveAllSuppliers();

        bool view = false;

        public ObservableCollection<string> cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }

        private Invoice invoice;

        public InvoiceAddWindow()
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Add";
        }

        public InvoiceAddWindow(Object invoiceA)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Change";
            invoice = (Invoice)invoiceA;
            cbEmployee.Text = invoice.Employee;
            cbSupplier.Text = invoice.Customer;
            txtTimeOfSale.Text = Convert.ToString(invoice.SupplyTime);
            view = true;
        }
        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(EmployeeDA.RetrieveAllEmployees().Select(employee => employee.Name));
            cb2 = new ObservableCollection<string>(SupplierDA.RetrieveAllSuppliers().Select(supplier => supplier.Name));
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
                if (cbEmployee.Text != "" && txtTimeOfSale.Text != "" && cbSupplier.Text != "")
                {
                    DateTime.TryParse(txtTimeOfSale.Text, out var timeOfSale);

                    if (timeOfSale.ToString() != "01.01.0001 00:00:00")
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
                        foreach (Classes.Supplier supplier in suppliers)
                        {
                            if (supplier.Name == cbSupplier.Text)
                            {
                                i2 = supplier.Id;
                            }
                        }
                        if (i1 != -1 && i2 != -1)
                        {
                            if (view == false) InvoiceDA.InvoiceAdd(i1, i2, timeOfSale);
                            else InvoiceDA.InvoiceChange(invoice.Id, i1, i2, timeOfSale);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect employee or supplier.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect date format.");
                    }
                }
                else
                {
                    MessageBox.Show("Employee and time of sale must be filled in!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void txtTimeOfSale_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTimeOfSale.Text != "" && txtTimeOfSale.Text != "dd.mm.yyyy hh:mm")
            {
                txtTimeOfSale.Foreground = Brushes.Black;
            }
            else
            {
                txtTimeOfSale.Text = "dd.mm.yyyy hh:mm";
                txtTimeOfSale.Foreground = Brushes.Gray;
            }
        }
    }
}
