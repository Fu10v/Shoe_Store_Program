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
    /// Interaction logic for QuantityReturnWindow.xaml
    /// </summary>
    public partial class QuantityReturnWindow : Window
    {
        public static int quantity;
        SalesList salesList;
        List<SalesList> salesLists;

        public QuantityReturnWindow(Object salesList)
        {
            this.salesList = (SalesList)salesList;
            InitializeComponent();
            quantity = 0;
            btnAdd.Content = "Змінити";
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
                if (int.TryParse(txtQuantity.Text, out int number))
                {

                    if (number > 0)
                    {
                        if (number > salesList.Quantity)
                        {
                            MessageBox.Show("Кількість повернутаємого товару перевищує кількість купленого.");
                        }
                        else
                        {
                            foreach (SalesList SalesListL in ProductReturnAddWindow.salesLists)
                            {
                                if (SalesListL == salesList)
                                {
                                    SalesListL.Quantity = number;
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
