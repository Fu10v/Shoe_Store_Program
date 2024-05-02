using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.Helper;

namespace Shoe_Store_DB.DA_Layer
{
    internal class ProductReturnDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static List<ProductReturn> RetrieveAllProductReturns()
        {
            string query = "SELECT return_id, sales_id, return_reason, ifnull(group_concat(Distinct product_name order by 1 separator ', '), '') as returned_products FROM product_return left join sales using(sales_id) left join sales_list using(sales_id) left join product using(product_id) group by 1 order by 1;";
            cmd = DBHelper.RunQuery(query);
            List<ProductReturn> productReturns = new List<ProductReturn>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["return_id"]);
                    int salesid = Convert.ToInt32(dr["sales_id"]);
                    string returnReason = dr["return_reason"].ToString();
                    string returnedProducts = dr["returned_products"].ToString();
                    ProductReturn productReturn = new ProductReturn(id, salesid, returnReason, returnedProducts);
                    productReturns.Add(productReturn);
                }
            }
            return productReturns;
        }

        public static List<ProductReturn> ProductReturnSearch(string search)
        {
            string query = "SELECT return_id, sales_id, return_reason, ifnull(group_concat(Distinct product_name order by 1 separator ', '), '') as returned_products FROM product_return left join sales using(sales_id) left join sales_list using(sales_id) left join product using(product_id) where return_id like @searchParameter or sales_id like @searchParameter or return_reason like @searchParameter or product_name like @searchParameter group by 1 order by 1;";
            cmd = DBHelper.RunQuerySearch(query, search);
            List<ProductReturn> productReturns = new List<ProductReturn>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["return_id"]);
                    int salesid = Convert.ToInt32(dr["sales_id"]);
                    string returnReason = dr["return_reason"].ToString();
                    string returnedProducts = dr["returned_products"].ToString();
                    ProductReturn productReturn = new ProductReturn(id, salesid, returnReason, returnedProducts);
                    productReturns.Add(productReturn);
                }
            }
            return productReturns;
        }

        public static List<ProductReturn> ProductReturnDelete(int id)
        {
            string query = "DELETE FROM product_return WHERE return_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, id);
            return RetrieveAllProductReturns();
        }

        public static void ProductReturnAdd(int salesId, string returnReason)
        {
            string query = "INSERT INTO product_return (sales_id, return_reason) VALUES (@salesId, @returnReason);";
            cmd = DBHelper.RunQueryProductReturnAddChange(query, salesId, returnReason);
        }
        public static void ProductReturnChange(int id, int salesId, string returnReason)
        {
            string query = "UPDATE product_return SET sales_id = @salesId, return_reason = @returnReason WHERE return_id = " + id + ";";
            cmd = DBHelper.RunQueryProductReturnAddChange(query, salesId, returnReason);
        }
    }
}
