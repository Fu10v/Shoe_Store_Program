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
            btnAddChange.Content = "Додати";
        }

        public SupplierAddWindow(Object supplierA)
        {
            InitializeComponent();
            supplier = (Supplier)supplierA;
            btnAddChange.Content = "Змінити";
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
                        if (txtFPhoneNumber.Text.Length == 9)
                        {
                            long? number4;
                            if (long.TryParse(txtSPhoneNumber.Text, out long number5) == false) number4 = null;
                            else number4 = number5;
                            if (number4 != null && txtSPhoneNumber.Text.Length != 9)
                                MessageBox.Show("Номер телефону повинен складатися з 9 цифр.");
                            else
                            {
                                if (txtErdpouCode.Text.Length == 8)
                                {
                                    if (view == false) SupplierDA.SupplierAdd(txtName.Text, number1, number2, number4, txtEmail.Text, txtAddress.Text, number3);
                                    else SupplierDA.SupplierChange(supplier.Id, txtName.Text, number1, number2, number4, txtEmail.Text, txtAddress.Text, number3);
                                    this.Close();
                                }
                                else MessageBox.Show("Код ЄРДПОУ повинен складатися з 8 цифр.");
                            }
                        }
                        else MessageBox.Show("Номер телефону повинен складатися з 9 цифр.");
                    }
                    else
                    {
                        MessageBox.Show("Введіть коректний номер телефону, код ЄРДПОУ, поточний рахунок.");
                    }
                }
                else
                {
                    MessageBox.Show("Перший телефон, код ЄРДПОУ, розрахунковий рахунок обов'язкові!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
