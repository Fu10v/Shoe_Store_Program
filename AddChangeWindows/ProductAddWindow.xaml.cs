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
using System.Windows.Shapes;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;

namespace Shoe_Store_DB.AddChangeWindows
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class ProductAddWindow : Window
    {
        List<Classes.Type> types = TypeDA.RetrieveAllTypes();
        List<Classes.Brand> brands = BrandDA.RetrieveAllBrands();
        List<Classes.Material> materials = MaterialDA.RetrieveAllMaterials();

        bool view = false;
        Product product;

        public string[] cb1 { get; set; }
        public ObservableCollection<string> cb2 { get; set; }
        public ObservableCollection<string> cb3 { get; set; }
        public ObservableCollection<string> cb4 { get; set; }
        public string[] cb5 { get; set; }

        public ProductAddWindow()
        {
            InitializeComponent();
            UpdateLists();
            btnAddChange.Content = "Add";
        }

        public ProductAddWindow(Object productA)
        {
            InitializeComponent();
            product = (Product)productA;
            UpdateLists();
            txtName.Text = product.Name;
            txtPrice.Text = Convert.ToString(product.Price);
            cbGender.Text = product.Gender;
            cbType.Text = product.Type;
            cbBrand.Text = product.Brand;
            cbMaterial.Text = product.Material;
            view = true;
            btnAddChange.Content = "Change";
        }
        private void UpdateLists()
        {
            cb1 = new String[] { "чоловіче", "жіноче", "унісекс" };
            cb2 = new ObservableCollection<string>(TypeDA.RetrieveAllTypes().Select(type => type.Name));
            cb3 = new ObservableCollection<string>(BrandDA.RetrieveAllBrands().Select(employee => employee.Name));
            cb4 = new ObservableCollection<string>(MaterialDA.RetrieveAllMaterials().Select(material => material.Name));
            cb5 = new String[] { "зима", "весна", "літо", "осінь", "демісезон", "всесезон" };
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
                if (cbType.Text != "" && cbMaterial.Text != "" && cbBrand.Text != "" && txtPrice.Text != "")
                {
                    double.TryParse(txtPrice.Text, out double number);
                    if (number >= 0)
                    {
                        bool k1 = false;
                        int i1 = -1;
                        Classes.Type newtype;
                        bool k2 = false;
                        int i2 = -1;
                        Classes.Brand newbrand;
                        bool k3 = false;
                        int i3 = -1;
                        Classes.Material newmaterial;
                        foreach (Classes.Type type in types)
                        {
                            if (type.Name == cbType.Text)
                            {
                                k1 = true;
                                i1 = type.Id;
                            }
                        }
                        foreach (Classes.Brand brand in brands)
                        {
                            if (brand.Name == cbBrand.Text)
                            {
                                k2 = true;
                                i2 = brand.Id;
                            }
                        }
                        foreach (Classes.Material material in materials)
                        {
                            if (material.Name == cbMaterial.Text)
                            {
                                k3 = true;
                                i3 = material.Id;
                            }
                        }

                        if (k1 == false)
                        {
                            newtype = TypeDA.TypeAdd(cbType.Text);
                            i1 = newtype.Id;
                        }
                        if (k2 == false)
                        {
                            newbrand = BrandDA.BrandAdd(cbBrand.Text);
                            i2 = newbrand.Id;
                        }
                        if (k3 == false)
                        {
                            newmaterial = MaterialDA.MaterialAdd(cbMaterial.Text);
                            i3 = newmaterial.Id;
                        }
                        if (view == false) ProductDA.ProductAdd(txtName.Text, cbGender.Text, i1, i2, i3, cbSeason.Text, number);
                        else ProductDA.ProductChange(product.Id, txtName.Text, cbGender.Text, i1, i2, i3, cbSeason.Text, number);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please enter only positive numeric or 0 for price.");
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
