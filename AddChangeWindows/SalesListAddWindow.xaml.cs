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
    /// Interaction logic for SalesList.xaml
    /// </summary>
    public partial class SalesListAddWindow : Window
    {
        int salesId;
        int i1 = -1;
        bool view = false;
        List<Product> products = ProductDA.RetrieveAllProducts();
        List<ProductQuantity> productQuantitys;
        List<Classes.Color> colors = ColorDA.RetrieveAllColors();
        List<Classes.Size> sizes = SizeDA.RetrieveAllSizes();
        SalesList salesList;
        public ObservableCollection<string> cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }
        public ObservableCollection<string> cb3 { get; set; }

        public SalesListAddWindow(int salesId)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Додати";
            this.salesId = salesId;
        }

        public SalesListAddWindow(int salesId, Object salesListA)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Змінити";
            view = true;
            this.salesId = salesId;
            salesList = (SalesList)salesListA;
            cbProduct.Text = salesList.Product.ToString();
            txtQuantity.Text = salesList.Quantity.ToString();
            txtPrice.Text = salesList.Price.ToString();
            cbSize.Text = salesList.Size.ToString();
            cbColor.Text = salesList.Color.ToString();
        }

        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(ProductDA.RetrieveAllProducts().Select(product => product.Name));
            cb2 = new ObservableCollection<string>(SizeDA.RetrieveAllSizes().Select(product => product.Name));
            cb3 = new ObservableCollection<string>(ColorDA.RetrieveAllColors().Select(product => product.Name));
            DataContext = null;
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
                if (cbProduct.Text != "" && txtQuantity.Text != "" && cbSize.Text != "" && cbColor.Text!= "" && txtPrice.Text != "")
                {
                    if (int.TryParse(txtQuantity.Text, out int quantity) && quantity >= 0)
                    {
                        if (int.TryParse(txtPrice.Text, out int price) && price >= 0)
                        {
                            string selectedProduct = cbProduct.Text;
                            int iProduct = -1;
                            foreach (Classes.Product product in products)
                            {
                                if (product.Name == selectedProduct)
                                {
                                    iProduct = product.Id;
                                }
                            }
                            if (iProduct == -1)
                            {
                                MessageBox.Show("Продукт не знайдено.");
                            }
                            else
                            {
                                productQuantitys = ProductQuantityDA.RetrieveProductQuantity(iProduct);
                                int i2 = -1;
                                foreach (Classes.ProductQuantity productQuantity in productQuantitys)
                                {
                                    if (productQuantity.ProductId == iProduct && productQuantity.Size == cbSize.Text && productQuantity.Color == cbColor.Text)
                                    {
                                        i2 = productQuantity.Id;
                                        break;
                                    }
                                }
                                if (iProduct != -1 && i2 != -1)
                                {
                                    if (view == false) SalesListDA.SalesListAdd(salesId, i2, price, quantity);
                                    else SalesListDA.SalesListChange(salesList.Id, salesId, i2, price, quantity);
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Продукт не знайдено.");
                                }
                            }
                            

                        }
                        else MessageBox.Show("Введіть позитивне число або 0 для ціни.");
                    }
                    else
                    {
                        MessageBox.Show("Введіть позитивне число або 0 для кількості.");
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

        private void cbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedProduct = "";
            if (cbProduct.SelectedItem != null)
            {
                selectedProduct = cbProduct.SelectedValue.ToString();
            }
            i1 = -1;
            foreach (Classes.Product product in products)
            {
                if (product.Name == selectedProduct)
                {
                    i1 = product.Id;
                }
            }
            if (i1 != -1)
            {
                productQuantitys = ProductQuantityDA.RetrieveProductQuantity(i1);
                cbSize.ItemsSource = new ObservableCollection<string>(ProductQuantityDA.RetrieveProductQuantity(i1).Select(productQuantity => productQuantity.Size));
                cbColor.ItemsSource = new ObservableCollection<string>(ProductQuantityDA.RetrieveProductQuantity(i1).Select(productQuantity => productQuantity.Color));
            }
            
        }
    }
}
