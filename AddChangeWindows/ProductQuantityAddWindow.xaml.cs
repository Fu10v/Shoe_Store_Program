using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ProductQuantityAddWindow.xaml
    /// </summary>
    public partial class ProductQuantityAddWindow : Window
    {
        List<Classes.Color> colors = ColorDA.RetrieveAllColors();
        List<Classes.Size> sizes = SizeDA.RetrieveAllSizes();
        bool view = false;
        int productId;
        ProductQuantity productQuantity;

        public ObservableCollection<string> cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }

        public ProductQuantityAddWindow(int productId)
        {
            InitializeComponent();
            this.productId = productId;
            UpdateLists();
            btnAddChange.Content = "Додати";
        }

        public ProductQuantityAddWindow(int productId, Object productQuantityA)
        {
            InitializeComponent();
            productQuantity = (ProductQuantity)productQuantityA;
            this.productId = productId;
            UpdateLists();
            txtQuantity.Text = Convert.ToString(productQuantity.Quantity);
            cbColor.Text = productQuantity.Color;
            cbSize.Text = productQuantity.Size;
            view = true;
            btnAddChange.Content = "Змінити";
        }

        private void UpdateLists()
        {
            cb2 = new ObservableCollection<string>(ColorDA.RetrieveAllColors().Select(color => color.Name));
            cb1 = new ObservableCollection<string>(SizeDA.RetrieveAllSizes().Select(size => size.Name));
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
                if (cbSize.Text != "" && cbColor.Text != "" && txtQuantity.Text != "")
                {
                    

                    if (int.TryParse(txtQuantity.Text, out int number) && number >= 0)
                    {
                        bool k1 = false;
                        int i1 = -1;
                        Classes.Size newsize;
                        bool k2 = false;
                        int i2 = -1;
                        Classes.Color newcolor;
                        foreach (Classes.Size size in sizes)
                        {
                            if (size.Name == cbSize.Text)
                            {
                                k1 = true;
                                i1 = size.Id;
                            }
                        }
                        foreach (Classes.Color color in colors)
                        {
                            if (color.Name == cbColor.Text)
                            {
                                k2 = true;
                                i2 = color.Id;
                            }
                        }

                        if (k1 == false)
                        {
                            newsize = SizeDA.SizeAdd(cbSize.Text);
                            i1 = newsize.Id;
                        }
                        if (k2 == false)
                        {
                            newcolor = ColorDA.ColorAdd(cbColor.Text);
                            i2 = newcolor.Id;
                        }

                        if (view == false) ProductQuantityDA.ProductQuantityAdd(productId, i1, i2, number);
                        else ProductQuantityDA.ProductQuantityChange(productQuantity.Id, productId, i1, i2, number);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Введіть лише позитивне число або 0 для кількості.");
                    }
                }
                else
                {
                    MessageBox.Show("Всі поля повинні бути заповнені!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
