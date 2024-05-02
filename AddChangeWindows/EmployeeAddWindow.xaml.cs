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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Shoe_Store_DB.AddChangeWindows
{
    /// <summary>
    /// Interaction logic for EmployeeAddWindow.xaml
    /// </summary>
    public partial class EmployeeAddWindow : Window
    {
        Employee employee;
        bool view = false;

        public string[] cb1 { get; set; }
        public string[] cb2 { get; set; }

        public EmployeeAddWindow()
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Add";
        }

        public EmployeeAddWindow(Object employeeA)
        {
            InitializeComponent();
            employee = (Employee)employeeA;
            UpdateLists();
            btnAddChange.Content = "Change";
            view = true;
            string[] name = employee.Name.Split(' ');
            txtFirstName.Text = name[0];
            txtSurname.Text = name[1];
            txtMiddleName.Text = name[2];
            cbPosition.Text = employee.Position;
            cbGender.Text = employee.Gender;
            txtPhoneNumber.Text = Convert.ToString(employee.PhoneNumber);
            txtEmail.Text = employee.Email;
            txtAddress.Text = employee.Address;
            DateOnly dateOnly = employee.DateOfBirth;
            DateTime? date = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day);
            dpDate.SelectedDate = date;
        }

        private void UpdateLists()
        {
            cb1 = new String[] { "менеджер", "продавець-консультант", "касир", "складський працівник" };

            cb2 = new String[] { "чоловік", "жінка" };

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
                if (cbPosition.Text != "" && cbGender.Text != "" && dpDate.SelectedDate.HasValue)
                {
                    long.TryParse(txtPhoneNumber.Text, out long number);
                    if (number >= 0)
                    {
                        DateTime? date = null;
                        if (dpDate.SelectedDate.HasValue) date = (DateTime)dpDate.SelectedDate;
                        if (view == false) EmployeeDA.EmployeeAdd(txtFirstName.Text, txtSurname.Text, txtMiddleName.Text, cbPosition.Text, number, txtEmail.Text, txtAddress.Text, cbGender.Text, date);
                        else EmployeeDA.EmployeeChange( employee.Id, txtFirstName.Text, txtSurname.Text, txtMiddleName.Text, cbPosition.Text, number, txtEmail.Text, txtAddress.Text, cbGender.Text, date);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Enter a valid phone number or 0 if the employee does not have a phone number.");
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
