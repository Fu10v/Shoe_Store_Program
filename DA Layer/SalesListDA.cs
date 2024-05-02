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
            string query = "SELECT sl.sales_list_id, sl.sales_id, p.product_name, sl.sales_list_quantity from sales_list sl join sales s using(sales_id) join product p using(product_id) where sl.sales_id = @firstParameter group by 2, 1 order by 1;";
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
                    string productName = dr["product_name"].ToString();
                    int quantuty = Convert.ToInt32(dr["sales_list_quantity"].ToString());
                    SalesList salesList = new SalesList(id, salesId, productName, quantuty);
                    salesLists.Add(salesList);
                }
            }
            return salesLists;
        }
        public static List<SalesList> SalesListSearch(int salesIdParameter, string search)
        {
            string query = "SELECT sl.sales_list_id, sl.sales_id, p.product_name, sl.sales_list_quantity from sales_list sl join sales s using(sales_id) join product p using(product_id) where (sl.sales_id = @firstParameter) and (sl.sales_list_id like @searchParameter or p.product_name like @searchParameter) group by 2, 1 order by 1;";
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
                    string productName = dr["product_name"].ToString();
                    int quantuty = Convert.ToInt32(dr["sales_list_quantity"].ToString());
                    SalesList salesList = new SalesList(id, salesId, productName, quantuty);
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

        public static void SalesListAdd(int salesId, int productId, int quantity)
        {
            string query = "insert into sales_list (sales_id, product_id, sales_list_quantity) values (@salesId, @productId, @quantity);";
            cmd = DBHelper.RunQuerySalesListAddChange(query, salesId, productId, quantity);

        }
        public static void SalesListChange( int id, int salesId, int productId, int quantity)
        {
            string query = "update sales_list set sales_id = @salesId, product_id = @productId, sales_list_quantity = @quantity where sales_list_id = " + id + ";";
            cmd = DBHelper.RunQuerySalesListAddChange(query, salesId, productId, quantity);
        }
    }
}
