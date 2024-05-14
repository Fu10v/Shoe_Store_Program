using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;
using Shoe_Store_DB.View_Layer;
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
    /// Interaction logic for QuantityWindow.xaml
    /// </summary>
    public partial class QuantityWindow : Window
    {
        bool view;
        public static int quantity;
        ProductCart productCart;
        ProductQuantity productQuantity;
        Product product;
        List<ProductQuantity> productQuantities;

        public QuantityWindow(Object productQuantityI, Object productI)
        {
            productQuantity = (ProductQuantity)productQuantityI;
            product = (Product)productI;
            InitializeComponent();
            quantity = 0;
            view = true;
            btnAdd.Content = "Додати";
        }
        public QuantityWindow(Object productCartI)
        {

            productCart = (ProductCart)productCartI;
            InitializeComponent();
            quantity = 0;
            view = false;
            btnAdd.Content = "Змінити";
            productQuantities = ProductQuantityDA.RetrieveProductQuantityUQ(productCart.Id);
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (view == true)
            {
                if (int.TryParse(txtQuantity.Text, out int number))
                {
                    
                    if (number > 0)
                    {
                        if (number > productQuantity.Quantity)
                        {
                            MessageBox.Show("Недостатньо товару!");
                        }
                        else
                        {
                            bool productFind = false;
                            quantity = number;
                            foreach (ProductCart productS in DBWindow.CartList)
                            {
                                if (productS.Id == product.Id && productS.PrductQuantityId == productQuantity.Id)
                                {
                                    if (productS.Quantity + number > productQuantity.Quantity)
                                    {
                                        MessageBox.Show("Недостатньо товару!");
                                    }
                                    else
                                    {
                                        productS.Quantity += number;
                                        productFind = true;
                                    }
                                }
                            }
                            if (productFind == false)
                            {
                                DBWindow.CartList.Add(new ProductCart(product.Id, product.Name, product.Gender, product.Type, product.Brand, product.Material, product.Season, productQuantity.Color, productQuantity.Size, product.Price, QuantityWindow.quantity, productQuantity.Id));
                            }
                            this.Close();
                        }

                    }
                    else MessageBox.Show("Введіть позитивне число!");
                }
                else
                {
                    MessageBox.Show("Введіть число!");
                }
            }
            else
            {
                if (int.TryParse(txtQuantity.Text, out int number))
                {

                    if (number > 0)
                    {
                        ProductQuantity productQuantityF = productQuantities[0];
                        if (number > productQuantityF.Quantity)
                        {
                            MessageBox.Show("Недостатньо товару!");
                        }
                        else
                        {
                            foreach (ProductCart productCartL in DBWindow.CartList)
                            {
                                if (productCartL == productCart)
                                {
                                    productCartL.Quantity = number;
                                }
                            }
                            this.Close();
                        }

                    }
                    else MessageBox.Show("Введіть позитивне число!");
                }
                else
                {
                    MessageBox.Show("Введіть число!");
                }

            }
            
        }
    }
}
