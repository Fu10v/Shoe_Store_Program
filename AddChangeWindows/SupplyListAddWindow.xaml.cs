using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Shoe_Store_DB.AddChangeWindows
{
    /// <summary>
    /// Interaction logic for SupplyListAddWindow.xaml
    /// </summary>
    public partial class SupplyListAddWindow : Window
    {
        int invoiceId;
        bool view = false;
        List<Product> products = ProductDA.RetrieveAllProducts();
        List<Classes.Color> colors = ColorDA.RetrieveAllColors();
        List<Classes.Size> sizes = SizeDA.RetrieveAllSizes();
        List<ProductQuantity> productQuantitys;
        SupplyList supplyList;
        public ObservableCollection<string> cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }
        public ObservableCollection<string> cb3 { get; set; }
        public SupplyListAddWindow(int invoiceId)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Add";
            this.invoiceId = invoiceId;
        }

        public SupplyListAddWindow(int invoiceId, Object salesListA)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Change";
            view = true;
            this.invoiceId = invoiceId;
            supplyList = (SupplyList)salesListA;
            cbProduct.Text = supplyList.Product;
            txtQuantity.Text = supplyList.Quantity.ToString();
            txtPrice.Text = supplyList.Price.ToString();
        }

        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(ProductDA.RetrieveAllProducts().Select(product => product.Name));
            cb2 = new ObservableCollection<string>(SizeDA.RetrieveAllSizes().Select(product => product.Name));
            cb3 = new ObservableCollection<string>(ColorDA.RetrieveAllColors().Select(product => product.Name));
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
                if (cbProduct.Text != "" && txtQuantity.Text != "" && txtPrice.Text != "")
                {
                    

                    if (int.TryParse(txtPrice.Text, out int price) && price >= 0)
                    {
                        if (int.TryParse(txtQuantity.Text, out int quantity) && quantity >= 0)
                        {

                            int i1 = -1;
                            foreach (Classes.Product product in products)
                            {
                                if (product.Name == cbProduct.Text)
                                {
                                    i1 = product.Id;
                                }
                            }
                            
                            if (i1 != -1)
                            {
                                productQuantitys = ProductQuantityDA.RetrieveProductQuantity(i1);
                                int i0 = -1;
                                foreach (Classes.ProductQuantity productQuantity in productQuantitys)
                                {
                                    if (productQuantity.ProductId == i1 && productQuantity.Size == cbSize.Text && productQuantity.Color == cbColor.Text)
                                    {
                                        i0 = productQuantity.Id;
                                        break;
                                    }
                                }
                                if (i0 == -1) 
                                {
                                    int i2 = -1;
                                    Classes.Size newsize;
                                    int i3 = -1;
                                    Classes.Color newcolor;
                                    foreach (Classes.Size size in sizes)
                                    {
                                        if (size.Name == cbSize.Text)
                                        {
                                            i2 = size.Id;
                                            break;
                                        }
                                    }
                                    foreach (Classes.Color color in colors)
                                    {
                                        if (color.Name == cbColor.Text)
                                        {
                                            i3 = color.Id;
                                            break;
                                        }
                                    }

                                    if (i2 == -1)
                                    {
                                        newsize = SizeDA.SizeAdd(cbSize.Text);
                                        i2 = newsize.Id;
                                    }
                                    if (i3 == -1)
                                    {
                                        newcolor = ColorDA.ColorAdd(cbColor.Text);
                                        i3 = newcolor.Id;
                                    }
                                    ProductQuantityDA.ProductQuantityAdd(i1, i2, i3, quantity);
                                    productQuantitys = ProductQuantityDA.RetrieveProductQuantity(i1);
                                    foreach (Classes.ProductQuantity productQuantity in productQuantitys)
                                    {
                                        if (productQuantity.ProductId == i1 && productQuantity.Size == cbSize.Text && productQuantity.Color == cbColor.Text)
                                        {
                                            i0 = productQuantity.Id;
                                            break;
                                        }
                                    }
                                    if (view == false) SupplyListDA.SupplyListAdd(invoiceId, i0, price, quantity);
                                    else SupplyListDA.SupplyListChange(supplyList.Id, invoiceId, i0, price, quantity);
                                    this.Close();
                                }
                                else
                                {
                                    if (view == false) SupplyListDA.SupplyListAdd(invoiceId, i0, price, quantity);
                                    else SupplyListDA.SupplyListChange(supplyList.Id, invoiceId, i0, price, quantity);
                                    this.Close();
                                }
                                
                                
                            }
                            else
                            {
                                MessageBox.Show("Продукт не знайдено.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введіть лише позитивне число або 0 для кількості.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введіть лише позитивне число або 0 для ціни.");
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
