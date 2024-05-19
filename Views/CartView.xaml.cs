using Shoe_Store_DB.AddChangeWindows;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;
using Shoe_Store_DB.View_Layer;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Shoe_Store_DB.Views
{
    /// <summary>
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : UserControl
    {
        List<Employee> employees = EmployeeDA.RetrieveAllEmployees();
        List<Customer> customers = CustomerDA.RetrieveAllCustomers();

        public ObservableCollection<string> cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }

        public CartView()
        {
            InitializeComponent();
            dataGridProduct.Items.Clear();
            dataGridProduct.ItemsSource = DBWindow.CartList;
            UpdateLists();
            UpdateInfo();
        }

        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(EmployeeDA.RetrieveAllEmployees().Select(employee => employee.Name));
            cb2 = new ObservableCollection<string>(CustomerDA.RetrieveAllCustomers().Select(customer => customer.Name));
            DataContext = null;
            DataContext = this;
        }

        private void UpdateInfo()
        {
            double sum = 0;
            foreach (ProductCart productCart in DBWindow.CartList)
            {
                sum = sum + productCart.Total;
            }
            tbQuantity.Text = $"Кількість продажей: {DBWindow.CartList.Count()}";
            tbTotal.Text = $"Загальна сума: {sum}";
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            dataGridProduct.ItemsSource = null;
            dataGridProduct.Items.Clear();
            dataGridProduct.ItemsSource = DBWindow.CartList;
            UpdateLists();
            UpdateInfo();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            QuantityWindow quantityWindow = new QuantityWindow(dataGridProduct.SelectedItem);
            quantityWindow.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ProductCart product = dataGridProduct.SelectedItem as ProductCart;
            DBWindow.CartList.Remove(product);
            dataGridProduct.ItemsSource = null;
            dataGridProduct.Items.Clear();
            dataGridProduct.ItemsSource = DBWindow.CartList;
            UpdateInfo();

        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {

            if (cbEmployee.Text != "")
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
                            SalesDA.SalesAdd(i1, DateTime.Now);
                        }
                        else
                        {
                            SalesDA.SalesAdd(i1, i2, DateTime.Now);
                        }
                        List<Sales> sales = SalesDA.RetrieveAllSales();
                        Sales sale = sales[0]; 
                        foreach (ProductCart productCart in DBWindow.CartList)
                        {
                            ProductQuantityDA.ProductQuantityChangeQuantityDown(productCart.PrductQuantityId, productCart.Quantity);
                            SalesListDA.SalesListAdd(sale.Id, productCart.PrductQuantityId, productCart.Price, productCart.Quantity);
                        }
                        DBWindow.CartList.Clear();
                        MessageBox.Show("Покупку оформлено.");
                        dataGridProduct.ItemsSource = null;
                        dataGridProduct.Items.Clear();
                        dataGridProduct.ItemsSource = DBWindow.CartList;
                        UpdateInfo();
                    }
                }
                else
                {
                    MessageBox.Show("Співробітника не знайдено.");
                }
            }
            else
            {
                MessageBox.Show("Співробітник обов'язковий!");
            }

        }
    }
}
