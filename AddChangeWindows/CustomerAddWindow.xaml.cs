using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;
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

namespace Shoe_Store_DB.AddChangeWindows
{
    /// <summary>
    /// Interaction logic for CustomerAddWindow.xaml
    /// </summary>
    public partial class CustomerAddWindow : Window
    {
        Customer customer;
        bool view = false;
        public CustomerAddWindow()
        {
            InitializeComponent();
            btnAddChange.Content = "Add";
            txtDiscountAccumulation.Text = "3000";
        }
        public CustomerAddWindow(Object customerA)
        {
            InitializeComponent();
            customer = (Customer)customerA;
            btnAddChange.Content = "Change";
            view = true;
            string[] name = customer.Name.Split(' ');
            txtFirstName.Text = name[0];
            txtSurname.Text = name[1];
            txtMiddleName.Text = name[2];
            txtPhoneNumber.Text = Convert.ToString(customer.PhoneNumber);
            txtEmail.Text = customer.Email;
            txtDiscountAccumulation.Text = customer.DiscountCardAccumulation.ToString();
            txtDiscountId.Text = customer.DiscountCardId.ToString();
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
                if (txtPhoneNumber.Text != "" && txtDiscountId.Text != "" && txtDiscountAccumulation.Text != "")
                {
                    
                    if (long.TryParse(txtPhoneNumber.Text, out long number1) && long.TryParse(txtDiscountId.Text, out long number2) && int.TryParse(txtDiscountAccumulation.Text, out int number3))
                    {
                        if (view == false) CustomerDA.CustomerAdd(txtFirstName.Text, txtSurname.Text, txtMiddleName.Text, number1, txtEmail.Text, number2, number3);
                        else CustomerDA.CustomerChange(customer.Id, txtFirstName.Text, txtSurname.Text, txtMiddleName.Text, number1, txtEmail.Text, number2, number3);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Enter a valid phone number, discoun card id, discount card accumulation.");
                    }
                }
                else
                {
                    MessageBox.Show("All fields must be filled in!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }

}
