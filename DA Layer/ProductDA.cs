using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.Helper;

namespace Shoe_Store_DB.DA_Layer
{
    class ProductDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static List<Product> RetrieveAllProducts()
        {
            string query = "SELECT p.product_id, p.product_name, p.product_gender, t.type_name as product_type, b.brand_name as product_brand, m.material_name as product_material, p.product_season, ifnull(group_concat(Distinct c.color_name order by 1 separator ', '), '') as product_colors, ifnull(group_concat(Distinct s.size_name order by 1 separator ', '), '') as product_sizes, p.product_price, ifnull(sum(pq.product_quantity), 0) as quantity FROM shoe_store_db.product p join shoe_store_db.product_type t using(type_id) join shoe_store_db.brand b using(brand_id) join material m using(material_id) left join product_quantity pq using(product_id) left join color c using(color_id) left join size s using(size_id) group by p.product_id order by quantity desc;";
            cmd = DBHelper.RunQuery(query);
            List<Product> products = new List<Product>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["product_id"].ToString());
                    string name = dr["product_name"].ToString();
                    string gender = dr["product_gender"].ToString();
                    string type = dr["product_type"].ToString();
                    string brand = dr["product_brand"].ToString();
                    string material = dr["product_material"].ToString();
                    string season = dr["product_season"].ToString();
                    string colors = dr["product_colors"].ToString();
                    string sizes = dr["product_sizes"].ToString();
                    int price = Convert.ToInt32(dr["product_price"].ToString());
                    int quantity = Convert.ToInt32(dr["quantity"]);
                    Product product = new Product(id, name, gender, type, brand, material, season, colors, sizes, price, quantity);
                    products.Add(product);
                }
            }
            return products;
        }
        public static List<Product> ProductSearch(string search)
        {
            string query = "SELECT p.product_id, p.product_name, p.product_gender, t.type_name as product_type, b.brand_name as product_brand, m.material_name as product_material, p.product_season, ifnull(group_concat(Distinct c.color_name order by 1 separator ', '), '') as product_colors, ifnull(group_concat(Distinct s.size_name order by 1 separator ', '), '') as product_sizes, p.product_price, ifnull(sum(pq.product_quantity), 0) as quantity FROM shoe_store_db.product p join shoe_store_db.product_type t using(type_id) join shoe_store_db.brand b using(brand_id) join material m using(material_id) left join product_quantity pq using(product_id) left join color c using(color_id) left join size s using(size_id) where p.product_name like @searchParameter or p.product_gender like @searchParameter or t.type_name like @searchParameter or b.brand_name like @searchParameter or m.material_name like @searchParameter or p.product_season like @searchParameter or c.color_name like @searchParameter or s.size_name like @searchParameter group by p.product_id order by quantity desc;";
            cmd = DBHelper.RunQuerySearch(query, search);
            List<Product> products = new List<Product>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["product_id"].ToString());
                    string name = dr["product_name"].ToString();
                    string gender = dr["product_gender"].ToString();
                    string type = dr["product_type"].ToString();
                    string brand = dr["product_brand"].ToString();
                    string material = dr["product_material"].ToString();
                    string season = dr["product_season"].ToString();
                    string colors = dr["product_colors"].ToString();
                    string sizes = dr["product_sizes"].ToString();
                    int price = Convert.ToInt32(dr["product_price"].ToString());
                    int quantity = Convert.ToInt32(dr["quantity"].ToString());
                    Product product = new Product(id, name, gender, type, brand, material, season, colors, sizes, price, quantity);
                    products.Add(product);
                }
            }
            return products;
        }

        public static List<Product> ProductDelete(int id)
        {

            string query = "DELETE FROM product_quantity WHERE product_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, id);
            query = "DELETE FROM product WHERE product_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, id);
            return RetrieveAllProducts();
        }

        public static List<Product> ProductFilter(string genderS, string typeS, string brandS, string materialS, string seasonS, string colorS, string sizeS, string priceFromS, string priceToS, string quantityFromS, string quantityToS)
        {
            string query = "SELECT p.product_id, p.product_name, p.product_gender, t.type_name as product_type, b.brand_name as product_brand, m.material_name as product_material, p.product_season, ifnull(group_concat(Distinct c.color_name order by 1 separator ', '), '') as product_colors, ifnull(group_concat(Distinct s.size_name order by 1 separator ', '), '') as product_sizes, p.product_price, ifnull(sum(pq.product_quantity), 0) as quantity FROM shoe_store_db.product p join shoe_store_db.product_type t using(type_id) join shoe_store_db.brand b using(brand_id) join material m using(material_id) left join product_quantity pq using(product_id) left join color c using(color_id) left join size s using(size_id) where product_id = product_id";
            cmd = DBHelper.RunQueryProductFilter(query, genderS, typeS, brandS, materialS, seasonS, colorS, sizeS, priceFromS, priceToS, quantityFromS, quantityToS);
            List<Product> products = new List<Product>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["product_id"].ToString());
                    string name = dr["product_name"].ToString();
                    string gender = dr["product_gender"].ToString();
                    string type = dr["product_type"].ToString();
                    string brand = dr["product_brand"].ToString();
                    string material = dr["product_material"].ToString();
                    string season = dr["product_season"].ToString();
                    string colors = dr["product_colors"].ToString();
                    string sizes = dr["product_sizes"].ToString();
                    int price = Convert.ToInt32(dr["product_price"].ToString());
                    int quantity = Convert.ToInt32(dr["quantity"].ToString());
                    Product product = new Product(id, name, gender, type, brand, material, season, colors, sizes, price, quantity);
                    products.Add(product);
                }
            }
            return products;
        }

        public static void ProductAdd(string name, string gender, int typeId, int brandId, int materialId, string season, double price)
        {
            string query = "INSERT INTO product (product_name, product_gender, type_id, brand_id, material_id, product_season, product_price) VALUES (@name, @gender, @typeId, @brandId, @materialId, @season, @price);";
            cmd = DBHelper.RunQueryProductAddChange(query, name, gender, typeId, brandId, materialId, season, price);
        }
        public static void ProductChange(int id, string name, string gender, int typeId, int brandId, int materialId, string season, double price)
        {
            string query = "update product set product_name = @name, product_gender = @gender, type_id = @typeId, brand_id = @brandId, material_id = @materialId, product_season = @season, product_price =@price where product_id = " + id + ";";
            cmd = DBHelper.RunQueryProductAddChange(query, name, gender, typeId, brandId, materialId, season, price);
        }
    }
}
