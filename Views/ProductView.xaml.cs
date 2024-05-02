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
                    Product product = (Product)dataGridProduct.SelectedItem;
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
            txtPriceFrom.Text= "from";
            txtPriceFrom.Foreground = Brushes.Gray;
            txtPriceTo.Text= "to";
            txtPriceTo.Foreground = Brushes.Gray;
        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (cbGender.Text != "" || cbType.Text != "" || cbBrand.Text != "" || cbMaterial.Text != "" || cbSeason.Text != "" || cbColor.Text != "" || cbSize.Text !="" || (txtPriceFrom.Text != "" && txtPriceFrom.Text != "from") || (txtPriceTo.Text != "" && txtPriceTo.Text != "from"))
            {
               
                if ((double.TryParse(txtPriceFrom.Text, out double number1)== false && txtPriceFrom.Text != "" && txtPriceFrom.Text != "from") || (double.TryParse(txtPriceTo.Text, out double number2) == false && txtPriceTo.Text != "" && txtPriceTo.Text != "to"))
                {
                    MessageBox.Show("Enter only positive numbers in the price fields!");
                }
                else
                {
                    dataGridProduct.ItemsSource = ProductDA.ProductFilter(cbGender.Text, cbType.Text, cbBrand.Text, cbMaterial.Text, cbSeason.Text, cbColor.Text, cbSize.Text, txtPriceFrom.Text, txtPriceTo.Text);
                    ButtonInfoEnabled();
                }
            }
            else
            {
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

    }

}
