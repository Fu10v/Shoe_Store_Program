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
    internal class SupplyListDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<SupplyList> RetrieveSupplyList(int invoiceIdParameter)
        {
            string query = "SELECT supply_list_id, invoice_id, product_name, supply_list_price, supply_list_quantity FROM supply_list left join product using(product_id) where invoice_id = @firstParameter order by 1;";
            cmd = DBHelper.RunQuery(query, invoiceIdParameter);
            List<SupplyList> supplylists = new List<SupplyList>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["supply_list_id"]);
                    int invoice_id = Convert.ToInt32(dr["invoice_id"]);
                    string productName = dr["product_name"].ToString();
                    int supply_list_price = Convert.ToInt32(dr["supply_list_price"]);
                    int supply_list_quantity = Convert.ToInt32(dr["supply_list_quantity"]);
                    SupplyList supplyList = new SupplyList(id, invoice_id, productName, supply_list_price, supply_list_quantity);
                    supplylists.Add(supplyList);
                }
            }
            return supplylists;
        }
        public static List<SupplyList> SupplyListSearch(int invoiceIdParameter, string search)
        {
            string query = "SELECT supply_list_id, invoice_id, product_name, supply_list_price, supply_list_quantity FROM supply_list left join product using(product_id) where (invoice_id = @firstParameter) and (supply_list_id like @searchParameter or product_name like @searchParameter or supply_list_price like @searchParameter or supply_list_quantity like @searchParameter) order by 1;";
            cmd = DBHelper.RunQuerySecondSearch(query, invoiceIdParameter, search);
            List<SupplyList> supplylists = new List<SupplyList>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["supply_list_id"]);
                    int invoice_id = Convert.ToInt32(dr["invoice_id"]);
                    string productName = dr["product_name"].ToString();
                    int supply_list_price = Convert.ToInt32(dr["supply_list_price"]);
                    int supply_list_quantity = Convert.ToInt32(dr["supply_list_quantity"]);
                    SupplyList supplyList = new SupplyList(id, invoice_id, productName, supply_list_price, supply_list_quantity);
                    supplylists.Add(supplyList);
                }
            }
            return supplylists;
        }
        public static List<SupplyList> SupplyListDelete(int parameter, int invoiceIdParameter)
        {
            string query = "DELETE FROM supply_list WHERE supply_list_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, parameter);
            return RetrieveSupplyList(invoiceIdParameter);
        }

        public static void SupplyListAdd(int invoiceId, int productId, int price, int quantity)
        {
            string query = "INSERT INTO supply_list (invoice_id, product_id, supply_list_price, supply_list_quantity) VALUES (@invoiceId, @productId, @price, @quantity);";
            cmd = DBHelper.RunQuerySupplyListAddChange(query, invoiceId, productId, price, quantity);

        }
        public static void SupplyListChange(int id, int invoiceId, int productId, int price, int quantity)
        {
            string query = "UPDATE supply_list SET invoice_id = @invoiceId, product_id = @productId, supply_list_price = @price, supply_list_quantity = @quantity WHERE supply_list_id = " + id + ";";
            cmd = DBHelper.RunQuerySupplyListAddChange(query, invoiceId, productId, price, quantity);
        }
    }
}
