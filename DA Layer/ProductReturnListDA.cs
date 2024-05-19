using Shoe_Store_DB.Classes;
using Shoe_Store_DB.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Shoe_Store_DB.DA_Layer
{
    internal class ProductReturnListDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<ProductReturnList> RetrieveProductReturnList(int salesIdParameter)
        {
            string query = "SELECT product_return_list_id, product_return_id, sales_list_id, product_name, color_name, size_name, product_return_list_quantity from product_return_list join product_return using(product_return_id) join sales_list using(sales_list_id) join product_quantity using(product_quantity_id) join product using(product_id) join color using(color_id) join size using(size_id) where product_return_id = @firstParameter group by 2,1 order by product_name;";
            cmd = DBHelper.RunQuery(query, salesIdParameter);
            List<ProductReturnList> productReturnLists = new List<ProductReturnList>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["product_return_list_id"].ToString());
                    int productReturnId = Convert.ToInt32(dr["product_return_id"].ToString());
                    int salesListId = Convert.ToInt32(dr["sales_list_id"].ToString());
                    string productName = dr["product_name"].ToString();
                    string colorName = dr["color_name"].ToString();
                    string sizeName = dr["size_name"].ToString();
                    int quantuty = Convert.ToInt32(dr["product_return_list_quantity"].ToString());
                    ProductReturnList productReturnList = new ProductReturnList(id, productReturnId, salesListId, productName, sizeName, colorName, quantuty);
                    productReturnLists.Add(productReturnList);
                }
            }
            return productReturnLists;
        }

        public static List<ProductReturnList> RetrieveProductReturnListUQ(int salesIdParameter)
        {
            string query = "SELECT product_return_list_id, product_return_id, sales_list_id, product_name, color_name, size_name, product_return_list_quantity from product_return_list join product_return using(product_return_id) join sales_list using(sales_list_id) join product_quantity using(product_quantity_id) join product using(product_id) join color using(color_id) join size using(size_id) where product_return_list_id = @firstParameter group by 2,1 order by 1;";
            cmd = DBHelper.RunQuery(query, salesIdParameter);
            List<ProductReturnList> productReturnLists = new List<ProductReturnList>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["product_return_list_id"].ToString());
                    int productReturnId = Convert.ToInt32(dr["product_return_id"].ToString());
                    int salesListId = Convert.ToInt32(dr["sales_list_id"].ToString());
                    string productName = dr["product_name"].ToString();
                    string colorName = dr["color_name"].ToString();
                    string sizeName = dr["size_name"].ToString();
                    int quantuty = Convert.ToInt32(dr["product_return_list_quantity"].ToString());
                    ProductReturnList productReturnList = new ProductReturnList(id, productReturnId, salesListId, productName, sizeName, colorName, quantuty);
                    productReturnLists.Add(productReturnList);
                }
            }
            return productReturnLists;
        }

        public static List<ProductReturnList> ProductReturnListSearch(int salesIdParameter, string search)
        {
            string query = "SELECT product_return_list_id, product_return_id, sales_list_id, product_name, color_name, size_name, product_return_list_quantity from product_return_list join product_return using(product_return_id) join sales_list using(sales_list_id) join product_quantity using(product_quantity_id) join product using(product_id) join color using(color_id) join size using(size_id) where (product_return_id = @firstParameter) and (product_name like @searchParameter or size_name like @searchParameter or color_name like @searchParameter) group by 2, 1 order by product_name;";
            cmd = DBHelper.RunQuery(query, salesIdParameter);
            List<ProductReturnList> productReturnLists = new List<ProductReturnList>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["product_return_list_id"].ToString());
                    int productReturnId = Convert.ToInt32(dr["product_return_id"].ToString());
                    int salesListId = Convert.ToInt32(dr["sales_list_id"].ToString());
                    string productName = dr["product_name"].ToString();
                    string colorName = dr["color_name"].ToString();
                    string sizeName = dr["size_name"].ToString();
                    int quantuty = Convert.ToInt32(dr["product_return_list_quantity"].ToString());
                    ProductReturnList productReturnList = new ProductReturnList(id, productReturnId, salesListId, productName, sizeName, colorName, quantuty);
                    productReturnLists.Add(productReturnList);
                }
            }
            return productReturnLists;
        }
        public static List<ProductReturnList> ProductReturnListDelete(int parameter, int salesIdParameter)
        {
            string query = "DELETE FROM product_return_list WHERE product_return_list_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, parameter);
            return RetrieveProductReturnList(salesIdParameter);
        }

        public static void ProductReturnListAdd(int productReturnId, int salesListId, int productReturnListQuantity)
        {
            string query = "insert into product_return_list (product_return_id, sales_list_id, product_return_list_quantity) values (@productReturnId, @salesListId, @productReturnListQuantity);";
            cmd = DBHelper.RunQueryProductReturnListAddChange(query, productReturnId, salesListId, productReturnListQuantity);

        }
        public static void ProductReturnListChange(int id, int productReturnId, int salesListId, int productReturnListQuantity)
        {
            string query = "update product_return_list set product_return_id = @productReturnId, sales_list_id = @salesListId, product_return_list_quantity = @productReturnListQuantity where sales_list_id = " + id + ";";
            cmd = DBHelper.RunQueryProductReturnListAddChange(query, productReturnId, salesListId, productReturnListQuantity);
        }
    }
}
