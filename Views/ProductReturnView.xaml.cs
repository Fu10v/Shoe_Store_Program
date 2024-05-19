using Shoe_Store_DB.AddChangeWindows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shoe_Store_DB.Views
{
    /// <summary>
    /// Interaction logic for ProductReturnView.xaml
    /// </summary>
    public partial class ProductReturnView : UserControl
    {
        bool view = true;
        int productReturnId;
        int salesId;
        Style styleButton;
        public ProductReturnView()
        {
            InitializeComponent();
            view = true;
            styleButton = btnInfo.Style;
            dataGrid.ItemsSource = ProductReturnDA.RetrieveAllProductReturns();
        }


        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (view == true)
                {
                    if (txtSearch.Text != "")
                    {
                        string search = txtSearch.Text;
                        dataGrid.ItemsSource = ProductReturnDA.ProductReturnSearch(search);
                        ButtonInfoEnabled();

                    }
                    else
                    {
                        dataGrid.ItemsSource = ProductReturnDA.RetrieveAllProductReturns();
                        ButtonInfoEnabled();
                    }
                }
                else
                {
                    if (txtSearch.Text != "")
                    {
                        string search = txtSearch.Text;
                        dataGrid.ItemsSource = ProductReturnListDA.ProductReturnListSearch(productReturnId, search);
                        ButtonInfoDisabled();
                    }
                    else
                    {
                        dataGrid.ItemsSource = ProductReturnListDA.RetrieveProductReturnList(productReturnId);
                        ButtonInfoDisabled();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            DataGridColumns1();
            dataGrid.ItemsSource = ProductReturnDA.RetrieveAllProductReturns();
            ButtonInfoEnabled();
            
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                    DataGridColumns2();
                    ProductReturn productReturn = (ProductReturn)dataGrid.SelectedItem;
                    dataGrid.ItemsSource = ProductReturnListDA.RetrieveProductReturnList(productReturn.Id);
                    productReturnId = productReturn.Id;
                    ButtonInfoDisabled();
                    salesId = productReturn.SaleId;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                if (view == true)
                {
                    ProductReturnAddWindow AddWindow = new ProductReturnAddWindow(dataGrid.SelectedItem);
                    AddWindow.Show();
                }
                else
                {
                    ProductReturnListAddWindow AddWindow = new ProductReturnListAddWindow(productReturnId, salesId, dataGrid.SelectedItem);
                    AddWindow.Show();
                }
            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            if (view == true)
            {
                ProductReturnAddWindow AddWindow = new ProductReturnAddWindow();
                AddWindow.Show();
            }
            else
            {
                ProductReturnListAddWindow AddWindow = new ProductReturnListAddWindow(productReturnId, salesId);
                AddWindow.Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                    if (view == true)
                    {
                        ProductReturn productReturn = (ProductReturn)dataGrid.SelectedItem;
                        dataGrid.ItemsSource = ProductReturnDA.ProductReturnDelete(productReturn.Id);
                        ButtonInfoEnabled();
                    }
                    else
                    {
                        ProductReturnList productReturnList = (ProductReturnList)dataGrid.SelectedItem;
                        dataGrid.ItemsSource = ProductReturnListDA.ProductReturnListDelete(productReturnList.Id, productReturnId);
                        ButtonInfoDisabled();
                    }
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void DataGridColumns1()
        {
            var columns = new List<DataGridColumn>
            {
            new DataGridTextColumn { Header = "Номер продажу", Binding = new Binding("SaleId") },
            new DataGridTextColumn { Header = "Причина повернення", Binding = new Binding("ReturnReason") },
            new DataGridTextColumn { Header = "Повернуті товари", Binding = new Binding("Products") }
            };

            // Очищаємо поточні стовпці
            dataGrid.Columns.Clear();

            // Додаємо нові стовпці до DataGrid
            foreach (var column in columns)
            {
                dataGrid.Columns.Add(column);
            }
        }
        private void DataGridColumns2()
        {
            var columns = new List<DataGridColumn>
            {
            new DataGridTextColumn { Header = "Товар", Binding = new Binding("Product") },
            new DataGridTextColumn { Header = "Розмір", Binding = new Binding("Size") },
            new DataGridTextColumn { Header = "Кольор", Binding = new Binding("Color") },
            new DataGridTextColumn { Header = "Кількість", Binding = new Binding("Quantity") },
            };

            // Очищаємо поточні стовпці
            dataGrid.Columns.Clear();

            // Додаємо нові стовпці до DataGrid
            foreach (var column in columns)
            {
                dataGrid.Columns.Add(column);
            }
        }

        private void ButtonInfoEnabled()
        {
            view = true;
            btnInfo.IsEnabled = true;
            btnInfo.ClearValue(Button.BackgroundProperty);
            btnInfo.Foreground = Brushes.White;
            btnInfo.Style = styleButton;
        }
        private void ButtonInfoDisabled()
        {
            view = false;
            btnInfo.IsEnabled = false;
            btnInfo.Background = Brushes.LightGray;
            btnInfo.Foreground = Brushes.Black;
        }
    }
}