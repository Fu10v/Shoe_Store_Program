using Shoe_Store_DB.Classes;
using Shoe_Store_DB.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Shoe_Store_DB.DA_Layer
{
    internal class ProductQuantityDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static List<ProductQuantity> RetrieveProductQuantity(int productId)
        {
            string query = "SELECT pq.product_quantity_id, product_id, s.size_name as product_size, c.color_name as product_color, pq.product_quantity  FROM shoe_store_db.product_quantity pq join product p using(product_id) join size s using(size_id) join color c using(color_id) where pq.product_id = @firstParameter order by product_quantity desc;";
            cmd = DBHelper.RunQuery(query, productId);
            List<ProductQuantity> productQuantitys = new List<ProductQuantity>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["product_quantity_id"].ToString());
                    int productid = Convert.ToInt32(dr["product_id"]);
                    string size = dr["product_size"].ToString();
                    string color = dr["product_color"].ToString();
                    int quantity = Convert.ToInt32(dr["product_quantity"].ToString());
                    ProductQuantity productQuantity = new ProductQuantity(id, productid, size, color, quantity);
                    productQuantitys.Add(productQuantity);
                }
            }
            return productQuantitys;
        }

        public static List<ProductQuantity> RetrieveProductQuantityUQ(int productQuantityId)
        {
            string query = "SELECT pq.product_quantity_id, product_id, s.size_name as product_size, c.color_name as product_color, pq.product_quantity  FROM shoe_store_db.product_quantity pq join product p using(product_id) join size s using(size_id) join color c using(color_id) where product_quantity_id = @firstParameter order by product_quantity desc;";
            cmd = DBHelper.RunQuery(query, productQuantityId);
            List<ProductQuantity> productQuantitys = new List<ProductQuantity>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["product_quantity_id"].ToString());
                    int productid = Convert.ToInt32(dr["product_id"]);
                    string size = dr["product_size"].ToString();
                    string color = dr["product_color"].ToString();
                    int quantity = Convert.ToInt32(dr["product_quantity"].ToString());
                    ProductQuantity productQuantity = new ProductQuantity(id, productid, size, color, quantity);
                    productQuantitys.Add(productQuantity);
                }
            }
            return productQuantitys;
        }

        public static List<ProductQuantity> ProductQuantitySearch(int productId, string search)
        {
            string query = "SELECT pq.product_quantity_id, product_id, s.size_name as product_size, c.color_name as product_color, pq.product_quantity FROM shoe_store_db.product_quantity pq join product p using(product_id) join size s using(size_id) join color c using(color_id) where (pq.product_id = @firstParameter) and (p.product_name like @searchParameter or s.size_name like @searchParameter or c.color_name like @searchParameter) order by product_quantity desc;";
            cmd = DBHelper.RunQuerySecondSearch(query, productId, search);
            List<ProductQuantity> productQuantitys = new List<ProductQuantity>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["product_quantity_id"].ToString());
                    int productid = Convert.ToInt32(dr["product_id"]);
                    string size = dr["product_size"].ToString();
                    string color = dr["product_color"].ToString();
                    int quantity = Convert.ToInt32(dr["product_quantity"].ToString());
                    ProductQuantity productQuantity = new ProductQuantity(id, productid, size, color, quantity);
                    productQuantitys.Add(productQuantity);
                }
            }
            return productQuantitys;
        }

        public static List<ProductQuantity> ProductQuantityDelete(int parameter, int productId)
        {
            string query = "DELETE FROM product_quantity WHERE product_quantity_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, parameter);
            return RetrieveProductQuantity(productId);
        }

        public static void ProductQuantityAdd(int productId, int sizeId, int colorId, int quantity)
        {
            string query = "INSERT INTO product_quantity (product_id, size_id, color_id, product_quantity) VALUES (@productId, @sizeId, @colorId, @quantity);";
            cmd = DBHelper.RunQueryProductQuantityAddChange(query, productId, sizeId, colorId, quantity);
        }
        public static void ProductQuantityChange(int productQuantityId, int productId, int sizeId, int colorId, int quantity)
        {
            string query = "update product_quantity set size_id = @sizeId, color_id = @colorId, product_quantity = @quantity where product_quantity_id = " + productQuantityId + ";";
            cmd = DBHelper.RunQueryProductQuantityAddChange(query, productId, sizeId, colorId, quantity);
        }
        public static void ProductQuantityChangeQuantity(int productQuantityId, int quantity)
        {
            string query = "update product_quantity set product_quantity = product_quantity - @quantity where product_quantity_id = " + productQuantityId + ";";
            cmd = DBHelper.RunQueryProductQuantityAddChangeQuantity(query, quantity);
        }
    }
}
