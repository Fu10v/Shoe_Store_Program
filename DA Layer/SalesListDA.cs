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
    internal class SalesListDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<SalesList> RetrieveSalesList(int salesIdParameter)
        {
            string query = "SELECT sl.sales_list_id, sl.sales_id, product_quantity_id, product_name, color_name, size_name, sl.sales_list_quantity, sales_list_price, ifnull(sum(sales_list_price * sales_list_quantity), 0) as total from sales_list sl join sales s using(sales_id) join product_quantity using(product_quantity_id) join product using(product_id) join color using(color_id) join size using(size_id) where sl.sales_id = @firstParameter group by 2, 1 order by 1;";
            cmd = DBHelper.RunQuery(query, salesIdParameter);
            List<SalesList> salesLists = new List<SalesList>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["sales_list_id"].ToString());
                    int salesId = Convert.ToInt32(dr["sales_id"].ToString());
                    int productQuantityId = Convert.ToInt32(dr["product_quantity_id"].ToString());
                    string productName = dr["product_name"].ToString();
                    string colorName = dr["color_name"].ToString();
                    string sizeName = dr["size_name"].ToString();
                    int quantuty = Convert.ToInt32(dr["sales_list_quantity"].ToString());
                    double price = Convert.ToInt32(dr["sales_list_price"].ToString());
                    double total = Convert.ToDouble(dr["total"]);
                    SalesList salesList = new SalesList(id, salesId, productQuantityId, productName, sizeName, colorName, quantuty, price, total);
                    salesLists.Add(salesList);
                }
            }
            return salesLists;
        }
        public static List<SalesList> SalesListSearch(int salesIdParameter, string search)
        {
            string query = "SELECT sl.sales_list_id, sl.sales_id, product_quantity_id, product_name, color_name, size_name, sl.sales_list_quantity, sales_list_price, ifnull(sum(sales_list_price * sales_list_quantity), 0) as total from sales_list sl join sales s using(sales_id) join product_quantity using(product_quantity_id) join product using(product_id) join color using(color_id) join size using(size_id) where (sl.sales_id = @firstParameter) and (p.product_name like @searchParameter) group by 2, 1 order by 1;";
            cmd = DBHelper.RunQuerySecondSearch(query, salesIdParameter, search);
            List<SalesList> salesLists = new List<SalesList>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["sales_list_id"].ToString());
                    int salesId = Convert.ToInt32(dr["sales_id"].ToString());
                    int productQuantityId = Convert.ToInt32(dr["product_quantity_id"].ToString());
                    string productName = dr["product_name"].ToString();
                    string colorName = dr["color_name"].ToString();
                    string sizeName = dr["size_name"].ToString();
                    int quantuty = Convert.ToInt32(dr["sales_list_quantity"].ToString());
                    double price = Convert.ToInt32(dr["sales_list_price"].ToString());
                    double total = Convert.ToDouble(dr["total"]);
                    SalesList salesList = new SalesList(id, salesId, productQuantityId, productName, sizeName, colorName, quantuty, price, total);
                    salesLists.Add(salesList);
                }
            }
            return salesLists;
        }
        public static List<SalesList> SalesListDelete(int parameter, int salesIdParameter)
        {
            string query = "DELETE FROM sales_list WHERE sales_list_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, parameter);
            return RetrieveSalesList(salesIdParameter);
        }

        public static void SalesListAdd(int salesId, int productQuantityId, double price, int quantity)
        {
            string query = "insert into sales_list (sales_id, product_quantity_id, sales_list_price, sales_list_quantity) values (@salesId, @productQuantityId, @price, @quantity);";
            cmd = DBHelper.RunQuerySalesListAddChange(query, salesId, productQuantityId, price, quantity);

        }
        public static void SalesListChange( int id, int salesId, int productQuantityId, double price, int quantity)
        {
            string query = "update sales_list set sales_id = @salesId, product_quantity_id = @productQuantityId, sales_list_price = @price, sales_list_quantity = @quantity where sales_list_id = " + id + ";";
            cmd = DBHelper.RunQuerySalesListAddChange(query, salesId, productQuantityId, price, quantity);
        }
    }
}
