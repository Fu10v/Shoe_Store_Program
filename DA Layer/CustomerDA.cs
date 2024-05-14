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
    internal class CustomerDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static List<Customer> RetrieveAllCustomers()
        {
            string query = "SELECT customer_id, concat(customer_first_name, ' ', customer_surname, ' ',customer_middle_name) as customer_name, customer_phone_number, customer_email, discount_card_id, discount_card_accumulation FROM customer order by customer_name;";
            cmd = DBHelper.RunQuery(query);
            List<Customer> customers = new List<Customer>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["customer_id"]);
                    string name = dr["customer_name"].ToString();
                    long phoneNumber = Convert.ToInt64(dr["customer_phone_number"]);
                    string email = dr["customer_email"].ToString();
                    long discountCardId = Convert.ToInt64(dr["discount_card_id"]);
                    double discountCardAccumulation = Convert.ToDouble(dr["discount_card_accumulation"]);
                    Customer customer = new Customer(id, name, phoneNumber, email, discountCardId, discountCardAccumulation);
                    customers.Add(customer);
                }
            }
            return customers;
        }
        public static List<Customer> CustomerSearch(string search)
        {
            string query = "SELECT customer_id, concat(customer_first_name, ' ', customer_surname, ' ',customer_middle_name) as customer_name, customer_phone_number, customer_email, discount_card_id, discount_card_accumulation FROM customer where customer_first_name like @searchParameter or customer_surname like @searchParameter or customer_middle_name like @searchParameter or customer_phone_number like @searchParameter or customer_email like @searchParameter or discount_card_id like @searchParameter or discount_card_accumulation like @searchParameter order by customer_name;";
            cmd = DBHelper.RunQuerySearch(query, search);
            List<Customer> customers = new List<Customer>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["customer_id"]);
                    string name = dr["customer_name"].ToString();
                    long phoneNumber = Convert.ToInt64(dr["customer_phone_number"]);
                    string email = dr["customer_email"].ToString();
                    long discountCardId = Convert.ToInt64(dr["discount_card_id"]);
                    double discountCardAccumulation = Convert.ToDouble(dr["discount_card_accumulation"]);
                    Customer customer = new Customer(id, name, phoneNumber, email, discountCardId, discountCardAccumulation);
                    customers.Add(customer);
                }
            }
            return customers;
        }

        public static List<Customer> CustomerFilter(string from, string to)
        {
            string query = "SELECT customer_id, concat(customer_first_name, ' ', customer_surname, ' ',customer_middle_name) as customer_name, customer_phone_number, customer_email, discount_card_id, discount_card_accumulation FROM customer where customer_id = customer_id";
            cmd = DBHelper.RunQueryCustomerFilter(query, from, to);
            List<Customer> customers = new List<Customer>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["customer_id"]);
                    string name = dr["customer_name"].ToString();
                    long phoneNumber = Convert.ToInt64(dr["customer_phone_number"]);
                    string email = dr["customer_email"].ToString();
                    long discountCardId = Convert.ToInt64(dr["discount_card_id"]);
                    double discountCardAccumulation = Convert.ToDouble(dr["discount_card_accumulation"]);
                    Customer customer = new Customer(id, name, phoneNumber, email, discountCardId, discountCardAccumulation);
                    customers.Add(customer);
                }
            }
            return customers;
        }

        public static List<Customer> CustomerDelete(int id)
        {
            string query = "DELETE FROM customer WHERE customer_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, id);
            return RetrieveAllCustomers();
        }

        public static void CustomerAdd(string firstName, string surname, string middleName, long phoneNumber, string email, long discId, int discAccum)
        {
            string query = "INSERT INTO customer (customer_first_name, customer_surname, customer_middle_name, customer_phone_number, customer_email, discount_card_id, discount_card_accumulation) VALUES (@firstName, @surname, @middleName, @phoneNumber, @email, @discId, @discAccum);";
            cmd = DBHelper.RunQueryCustomerAddChange(query, firstName, surname, middleName, phoneNumber, email, discId, discAccum);
        }
        public static void CustomerChange(int id, string firstName, string surname, string middleName, long phoneNumber, string email, long discId, int discAccum)
        {
            string query = "update customer set customer_first_name = @firstName, customer_surname = @surname, customer_middle_name = @middleName, customer_phone_number = @phoneNumber, customer_email = @email, discount_card_id = @discId, discount_card_accumulation = @discAccum where customer_id = " + id + ";";
            cmd = DBHelper.RunQueryCustomerAddChange(query, firstName, surname, middleName, phoneNumber, email, discId, discAccum);
        }
    }
}
