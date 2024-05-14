using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Shoe_Store_DB.Helper;
using Shoe_Store_DB.DA_Layer;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.AddChangeWindows;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.ObjectModel;
using Shoe_Store_DB.View_Layer;

namespace Shoe_Store_DB.Views
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : UserControl
    {
        bool view = true;
        int productId;
        Style styleButton;

        Product product;

        public string[] cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }
        public ObservableCollection<string> cb3 { get; set; }
        public ObservableCollection<string> cb4 { get; set; }
        public string[] cb5 { get; set; }
        public ObservableCollection<string> cb6 { get; set; }
        public ObservableCollection<string> cb7 { get; set; }

        
        public ProductView()
        {
            InitializeComponent();
            dataGridProduct.Items.Clear();
            dataGridProduct.ItemsSource = ProductDA.RetrieveAllProducts();
            view = true;
            styleButton = btnInfo.Style;
            UpdateLists();

        }

        private void UpdateLists()
        {
            cb1 = new String[] { "чоловіче", "жіноче", "унісекс" };
            cb2 = new ObservableCollection<string>(TypeDA.RetrieveAllTypes().Select(type => type.Name));
            cb3 = new ObservableCollection<string>(BrandDA.RetrieveAllBrands().Select(employee => employee.Name));
            cb4 = new ObservableCollection<string>(MaterialDA.RetrieveAllMaterials().Select(material => material.Name));
            cb5 = new String[] { "зима", "весна", "літо", "осінь", "демісезон", "всесезон" };
            cb6 = new ObservableCollection<string>(ColorDA.RetrieveAllColors().Select(color => color.Name));
            cb7 = new ObservableCollection<string>(SizeDA.RetrieveAllSizes().Select(size => size.Name));
            DataContext = null;
            DataContext = this;
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
                        dataGridProduct.ItemsSource = ProductDA.ProductSearch(search);
                        ButtonInfoEnabled();

                    }
                    else
                    {
                        dataGridProduct.ItemsSource = ProductDA.RetrieveAllProducts();
                        ButtonInfoEnabled();
                        UpdateLists();
                    }
                }
                else
                {
                    if (txtSearch.Text != "")
                    {
                        string search = txtSearch.Text;
                        dataGridProduct.ItemsSource = ProductQuantityDA.ProductQuantitySearch(productId, search);
                        ButtonInfoDisabled();

                    }
                    else
                    {
                        dataGridProduct.ItemsSource = ProductQuantityDA.RetrieveProductQuantity(productId);
                        ButtonInfoDisabled();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridProduct.SelectedItem != null)
                {
                    DataGridColumns2();
                    product = (Product)dataGridProduct.SelectedItem;
                    dataGridProduct.ItemsSource = ProductQuantityDA.RetrieveProductQuantity(product.Id);
                    productId = product.Id;
                    ButtonInfoDisabled();
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
            dataGridProduct.ItemsSource = ProductDA.RetrieveAllProducts();
            ButtonInfoEnabled();
            UpdateLists();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProduct.SelectedItem != null)
            {
                try
                {
                    if (view == true)
                    {
                        Product product = (Product)dataGridProduct.SelectedItem;
                        dataGridProduct.ItemsSource = ProductDA.ProductDelete(product.Id);
                        ButtonInfoEnabled();
                    }
                    else
                    {
                        ProductQuantity productQuantity = (ProductQuantity)dataGridProduct.SelectedItem;
                        dataGridProduct.ItemsSource = ProductQuantityDA.ProductQuantityDelete(productQuantity.Id, productId);
                        ButtonInfoDisabled();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
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



        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProduct.SelectedItem != null)
            {
                if (view == true)
                {
                    ProductAddWindow AddWindow = new ProductAddWindow(dataGridProduct.SelectedItem);
                    AddWindow.Show();
                }
                else
                {
                    ProductQuantityAddWindow AddWindow = new ProductQuantityAddWindow(productId, dataGridProduct.SelectedItem);
                    AddWindow.Show();
                }
            }
                
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (view == true)
            {
                ProductAddWindow productAddWindow = new ProductAddWindow();
                productAddWindow.Show();
            }
            else
            {
                ProductQuantityAddWindow productQuantityAddWindow = new ProductQuantityAddWindow(productId);
                productQuantityAddWindow.Show();
            }
        }



        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            cbGender.Text= string.Empty;
            cbType.Text= string.Empty;
            cbBrand.Text= string.Empty;
            cbMaterial.Text= string.Empty;
            cbSeason.Text= string.Empty;
            cbColor.Text= string.Empty;
            cbSize.Text= string.Empty;
            txtPriceFrom.Text= "від";
            txtPriceFrom.Foreground = Brushes.Gray;
            txtPriceTo.Text= "до";
            txtPriceTo.Foreground = Brushes.Gray;
            txtQuantityFrom.Text = "від";
            txtQuantityFrom.Foreground = Brushes.Gray;
            txtQuantityTo.Text = "до";
            txtQuantityTo.Foreground = Brushes.Gray;
        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (cbGender.Text != "" || cbType.Text != "" || cbBrand.Text != "" || cbMaterial.Text != "" || cbSeason.Text != "" || cbColor.Text != "" || cbSize.Text !="" || (txtPriceFrom.Text != "" && txtPriceFrom.Text != "від") || (txtPriceTo.Text != "" && txtPriceTo.Text != "до") || (txtQuantityFrom.Text != "" && txtQuantityFrom.Text != "від") || (txtQuantityTo.Text != "" && txtQuantityTo.Text != "до"))
            {
               
                if (((double.TryParse(txtPriceFrom.Text, out double number1) && number1 >= 0 && (txtPriceTo.Text == "до" || txtPriceTo.Text == "")) || (double.TryParse(txtPriceTo.Text, out double number2) && number2 >= 0 && (txtPriceFrom.Text == "від" || txtPriceFrom.Text == "")) || (double.TryParse(txtPriceFrom.Text, out number1) && double.TryParse(txtPriceTo.Text, out number2)) || ((txtPriceFrom.Text == "від" || txtPriceFrom.Text == "") && (txtPriceTo.Text == "до" || txtPriceTo.Text == ""))) && ((double.TryParse(txtQuantityFrom.Text, out double number3) && number3 >= 0 && (txtQuantityTo.Text == "до" || txtQuantityTo.Text == "")) || (double.TryParse(txtQuantityTo.Text, out double number4) && number4 >= 0 && (txtQuantityFrom.Text == "від" || txtQuantityFrom.Text == "")) || (double.TryParse(txtQuantityFrom.Text, out number3) && double.TryParse(txtQuantityTo.Text, out number4)) || ((txtQuantityFrom.Text == "від" || txtQuantityFrom.Text == "") && (txtQuantityTo.Text == "до" || txtQuantityTo.Text == ""))))
                {
                    DataGridColumns1();
                    dataGridProduct.ItemsSource = ProductDA.ProductFilter(cbGender.Text, cbType.Text, cbBrand.Text, cbMaterial.Text, cbSeason.Text, cbColor.Text, cbSize.Text, txtPriceFrom.Text, txtPriceTo.Text, txtQuantityFrom.Text, txtQuantityTo.Text);
                    ButtonInfoEnabled();
                }
                else
                {
                    MessageBox.Show("У полях ціни та кількості вводьте тільки позитивні числа!");
                }
            }
            else
            {
                DataGridColumns1();
                dataGridProduct.ItemsSource = ProductDA.RetrieveAllProducts();
                ButtonInfoEnabled();
            }
        }

        private void txtPriceFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPriceFrom.Foreground = Brushes.Black;
        }

        private void txtPriceTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPriceTo.Foreground = Brushes.Black;
        }

        private void txtQuantityFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtQuantityFrom.Foreground = Brushes.Black;
        }

        private void txtQuantityTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtQuantityTo.Foreground = Brushes.Black;
        }

        private void DataGridColumns1()
        {
            var columns = new List<DataGridColumn>
            {
            new DataGridTextColumn { Header = "Назва", Binding = new Binding("Name") },
            new DataGridTextColumn { Header = "Стать", Binding = new Binding("Gender") },
            new DataGridTextColumn { Header = "Тип", Binding = new Binding("Type") },
            new DataGridTextColumn { Header = "Бренд", Binding = new Binding("Brand") },
            new DataGridTextColumn { Header = "Матеріал", Binding = new Binding("Material") },
            new DataGridTextColumn { Header = "Сезон", Binding = new Binding("Season") },
            new DataGridTextColumn { Header = "Кольори", Binding = new Binding("Colors") },
            new DataGridTextColumn { Header = "Розміри", Binding = new Binding("Sizes") },
            new DataGridTextColumn { Header = "Ціна, грн", Binding = new Binding("Price") },
            new DataGridTextColumn { Header = "Кількість", Binding = new Binding("Quantity") }
            };

            // Очищаємо поточні стовпці
            dataGridProduct.Columns.Clear();

            // Додаємо нові стовпці до DataGrid
            foreach (var column in columns)
            {
                dataGridProduct.Columns.Add(column);
            }
        }
        private void DataGridColumns2()
        {
            var columns = new List<DataGridColumn>
            {
            new DataGridTextColumn { Header = "Колір", Binding = new Binding("Color") },
            new DataGridTextColumn { Header = "Розмір", Binding = new Binding("Size") },
            new DataGridTextColumn { Header = "Кількість", Binding = new Binding("Quantity") }
            };

            // Очищаємо поточні стовпці
            dataGridProduct.Columns.Clear();

            // Додаємо нові стовпці до DataGrid
            foreach (var column in columns)
            {
                dataGridProduct.Columns.Add(column);
            }
        }

        private void btnAddToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProduct.SelectedItem != null)
            {
                try
                {
                    if (view == true)
                    {
                        DataGridColumns2();
                        product = (Product)dataGridProduct.SelectedItem;
                        dataGridProduct.ItemsSource = ProductQuantityDA.RetrieveProductQuantity(product.Id);
                        productId = product.Id;
                        ButtonInfoDisabled();
                    }
                    else
                    {
                        ProductQuantity productQuantity = (ProductQuantity)dataGridProduct.SelectedItem;
                        QuantityWindow quantityWindow = new QuantityWindow(dataGridProduct.SelectedItem, product);
                        quantityWindow.Show();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
    }

}
