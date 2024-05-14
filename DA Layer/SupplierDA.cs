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
    internal class SupplierDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static List<Supplier> RetrieveAllSuppliers()
        {
            string query = "SELECT supplier_id, supplier_name, supplier_edrpou_code, supplier_fphone_number, ifnull(supplier_sphone_number, '') as second_phone_number, supplier_email, supplier_address, supplier_current_account FROM shoe_store_db.supplier order by supplier_name;";
            cmd = DBHelper.RunQuery(query);
            List<Supplier> suppliers = new List<Supplier>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["supplier_id"]);
                    string name = dr["supplier_name"].ToString();
                    long edrpouCode = Convert.ToInt64(dr["supplier_edrpou_code"]);
                    long fPhoneNumber = Convert.ToInt64(dr["supplier_fphone_number"]);
                    string input = dr["second_phone_number"].ToString();
                    long? sPhoneNumber = string.IsNullOrEmpty(input) ? (long?)null : Convert.ToInt64(input);
                    string email = dr["supplier_email"].ToString();
                    string address = dr["supplier_address"].ToString();
                    long currentAccount = Convert.ToInt64(dr["supplier_current_account"]);
                    Supplier supplier = new Supplier(id, name, edrpouCode, fPhoneNumber, sPhoneNumber, email, address, currentAccount);
                    suppliers.Add(supplier);
                }
            }
            return suppliers;
        }
        public static List<Supplier> SupplierSearch(string search)
        {
            string query = "SELECT supplier_id, supplier_name, supplier_edrpou_code, supplier_fphone_number, ifnull(supplier_sphone_number, '') as second_phone_number, supplier_email, supplier_address, supplier_current_account FROM shoe_store_db.supplier where supplier_id like @searchParameter or supplier_name like @searchParameter or supplier_edrpou_code like @searchParameter or supplier_fphone_number like @searchParameter or supplier_sphone_number like @searchParameter or supplier_email like @searchParameter or supplier_address like @searchParameter or supplier_current_account like @searchParameter order by supplier_name;";
            cmd = DBHelper.RunQuerySearch(query, search);
            List<Supplier> suppliers = new List<Supplier>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["supplier_id"]);
                    string name = dr["supplier_name"].ToString();
                    long edrpouCode = Convert.ToInt64(dr["supplier_edrpou_code"]);
                    long fPhoneNumber = Convert.ToInt64(dr["supplier_fphone_number"]);
                    string input = dr["second_phone_number"].ToString();
                    long? sPhoneNumber = string.IsNullOrEmpty(input) ? (long?)null : Convert.ToInt64(input);
                    string email = dr["supplier_email"].ToString();
                    string address = dr["supplier_address"].ToString();
                    long currentAccount = Convert.ToInt64(dr["supplier_current_account"]);
                    Supplier supplier = new Supplier(id, name, edrpouCode, fPhoneNumber, sPhoneNumber, email, address, currentAccount);
                    suppliers.Add(supplier);
                }
            }
            return suppliers;
        }

        public static List<Supplier> SupplierDelete(int id)
        {
            string query = "DELETE FROM supplier WHERE supplier_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, id);
            return RetrieveAllSuppliers();
        }

        public static void SupplierAdd(string name, long erdpouCode, long fpn, long? spn, string email, string address, long curentAccount)
        {
            string query = "INSERT INTO supplier (supplier_name, supplier_edrpou_code, supplier_fphone_number, supplier_sphone_number, supplier_email, supplier_address, supplier_current_account) VALUES (@Sname, @erdpouCode, @fpn, @spn, @email, @address, @curentAccount);";
            cmd = DBHelper.RunQuerySupplierAddChange(query, name, erdpouCode, fpn, spn, email, address, curentAccount);
        }
        public static void SupplierChange(int id, string name, long erdpouCode, long fpn, long? spn, string email, string address, long curentAccount)
        {
            string query = "UPDATE supplier SET supplier_name = @Sname, supplier_edrpou_code = @erdpouCode, supplier_fphone_number = @fpn, supplier_sphone_number = @spn, supplier_email = @email, supplier_address = @address, supplier_current_account = @curentAccount WHERE supplier_id = " + id + ";";
            cmd = DBHelper.RunQuerySupplierAddChange(query, name, erdpouCode, fpn, spn, email, address, curentAccount);
        }
    }
}
