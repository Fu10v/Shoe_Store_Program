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
    /// Interaction logic for SupplierAddWindow.xaml
    /// </summary>
    public partial class SupplierAddWindow : Window
    {
        bool view = false;
        Supplier supplier;
        public SupplierAddWindow()
        {
            InitializeComponent();
            btnAddChange.Content = "Add";
        }

        public SupplierAddWindow(Object supplierA)
        {
            InitializeComponent();
            supplier = (Supplier)supplierA;
            btnAddChange.Content = "Change";
            view = true;
            txtName.Text = supplier.Name;
            txtErdpouCode.Text = supplier.Edrpou_Code.ToString();
            txtFPhoneNumber.Text = supplier.FirstPhoneNumber.ToString();
            txtSPhoneNumber.Text = supplier.SecondPhoneNumber.ToString();
            txtEmail.Text = supplier.Email;
            txtAddress.Text = supplier.Address;
            txtCurrentAccount.Text = supplier.CurrentAccount.ToString();
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
                if (txtErdpouCode.Text != "" && txtFPhoneNumber.Text != "" && txtCurrentAccount.Text != "")
                {

                    if (long.TryParse(txtErdpouCode.Text, out long number1) && long.TryParse(txtFPhoneNumber.Text, out long number2) && long.TryParse(txtCurrentAccount.Text, out long number3))
                    {
                        long? number4;
                        if (long.TryParse(txtSPhoneNumber.Text, out long number5) == false) number4 = null;
                        else number4 = number5;
                        if (view == false) SupplierDA.SupplierAdd(txtName.Text, number1, number2, number4, txtEmail.Text, txtAddress.Text, number3);
                        else SupplierDA.SupplierChange(supplier.Id, txtName.Text, number1, number2, number4, txtEmail.Text, txtAddress.Text, number3);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Enter a valid phone number, ERDPOU code, current account.");
                    }
                }
                else
                {
                    MessageBox.Show("First phone number, ERDPOU code, current account must be filled in!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
