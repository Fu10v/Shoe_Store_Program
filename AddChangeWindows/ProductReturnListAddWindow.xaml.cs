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
    /// Interaction logic for ProductReturnListAddWindow.xaml
    /// </summary>
    public partial class ProductReturnListAddWindow : Window
    {
        int productReturnId;
        int salesId;
        int i1 = -1;
        bool view = false;
        List<SalesList> salesLists;
        List<ProductReturnList> productReturnLists;
        ProductReturnList productReturnList;
        public ObservableCollection<string> cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }
        public ObservableCollection<string> cb3 { get; set; }

        public ProductReturnListAddWindow(int productReturnId, int salesId)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Додати";
            this.productReturnId = productReturnId;
            this.salesId = salesId;
            salesLists = SalesListDA.RetrieveSalesList(salesId);
            productReturnLists = ProductReturnListDA.RetrieveProductReturnList(productReturnId);
        }

        public ProductReturnListAddWindow(int productReturnId, int salesId, Object productReturnList)
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Змінити";
            view = true;
            this.productReturnId = productReturnId;
            this.salesId = salesId;
            salesLists = SalesListDA.RetrieveSalesList(salesId);
            productReturnLists = ProductReturnListDA.RetrieveProductReturnList(productReturnId);
            this.productReturnList = (ProductReturnList)productReturnList;
            cbProduct.Text = this.productReturnList.Product.ToString();
            txtQuantity.Text = this.productReturnList.Quantity.ToString();
            cbSize.Text = this.productReturnList.Size.ToString();
            cbColor.Text = this.productReturnList.Color.ToString();
        }
        
        private void UpdateLists()
        {
            cb1 = new ObservableCollection<string>(SalesListDA.RetrieveSalesList(salesId).Select(salesList => salesList.Product));
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
                if (cbProduct.Text != "" && txtQuantity.Text != "" && cbSize.Text != "" && cbColor.Text != "")
                {
                    if (int.TryParse(txtQuantity.Text, out int quantity) && quantity >= 0)
                    {
                        bool inList = false;
                        if (view == false)
                        {
                            foreach (ProductReturnList productReturnListS in productReturnLists)
                            {
                                if (productReturnListS.Product == cbProduct.Text && productReturnListS.Size == cbSize.Text && productReturnListS.Color == cbColor.Text)
                                {
                                    inList = true;
                                    break;
                                }
                            }
                        }
                        
                        if ((inList == false && view == false) || view == true)
                        {
                            SalesList salesListA = null;
                            int i = -1;
                            foreach (SalesList salesList in salesLists)
                            {
                                if (salesList.Product == cbProduct.Text && salesList.Size == cbSize.Text && salesList.Color == cbColor.Text)
                                {
                                    salesListA = salesList;
                                    break;
                                }
                            }
                            if (salesListA != null)
                            {
                                if (quantity > salesListA.Quantity)
                                {
                                    if (view == false) ProductReturnListDA.ProductReturnListAdd(productReturnId, salesListA.Id, quantity);
                                    else ProductReturnListDA.ProductReturnListChange(productReturnList.Id, productReturnId, salesListA.Id, quantity);
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Кількість повернутаємого товару перевищує кількість купленого.");
                                }
                                
                            }
                            else
                            {
                                MessageBox.Show("Продукт не знайдено");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Такий продукт вже повертали.");
                        }
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
    }
}
