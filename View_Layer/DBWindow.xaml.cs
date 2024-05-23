using Shoe_Store_DB.Views;
using Shoe_Store_DB.Classes;
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
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Navigation;
using Shoe_Store_DB.AddChangeWindows;

namespace Shoe_Store_DB.View_Layer
{
    /// <summary>
    /// Interaction logic for DBWindow.xaml
    /// </summary>
    public partial class DBWindow : Window
    {
        private User user;

        public static List<Object> CartList { get; set; }

        public DBWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            radioButton1.IsChecked = true;
            txtMode.Text = $"Режим: {user.Mode}";
            CartList = new List<Object>();
            if (user.Mode == "employee")
            {
                bAdmin.Background = Brushes.White;
                radioButton2.IsEnabled = false;
                radioButton3.IsEnabled = false;
                radioButton5.IsEnabled = false;
                radioButton6.IsEnabled = false;
                btnShowStatistic.IsEnabled = false;
                btnShowStatistic.Background = Brushes.White;
                RowDefinition3.Height = new GridLength(0);
                buttonsStackPanel.Children.Remove(btnShowStatistic);
            }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            ProductView productView = new ProductView();
            viewBox.Content = productView;
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            SalesView salesView = new SalesView();
            viewBox.Content = salesView;
        }

        private void radioButton4_Checked(object sender, RoutedEventArgs e)
        {
            CustomerView customerView = new CustomerView();
            viewBox.Content = customerView;
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            ProductReturnView productReturnView = new ProductReturnView();
            viewBox.Content = productReturnView;
        }

        private void radioButton5_Checked(object sender, RoutedEventArgs e)
        {
            EmployeeView employeeView = new EmployeeView();
            viewBox.Content = employeeView;
        }

        private void radioButton6_Checked(object sender, RoutedEventArgs e)
        {
            SupplierView supplierView = new SupplierView();
            viewBox.Content = supplierView;
        }

        private void radioButton7_Checked(object sender, RoutedEventArgs e)
        {
            InvoiceView invoiceView = new InvoiceView();
            viewBox.Content = invoiceView;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnRegisterAClient_Click(object sender, RoutedEventArgs e)
        {
            CustomerAddWindow customerAddWindow = new CustomerAddWindow();
            customerAddWindow.Show();
        }

        private void btnReturnProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductReturnAddWindow productReturnAddWindow = new ProductReturnAddWindow();
            productReturnAddWindow.Show();
        }

        private void btnShowOrder_Click(object sender, RoutedEventArgs e)
        {
            radioButton1.IsChecked = true;
            radioButton1.IsEnabled = false;
            radioButton1.IsEnabled = true;
            radioButton1.IsChecked = false;
            CartView cartView = new CartView();
            viewBox.Content = cartView;
        }

        private void btnShowStatistic_Click(object sender, RoutedEventArgs e)
        {
            StatisticView statisticView = new StatisticView();
            viewBox.Content = statisticView;
        }
    }
}
