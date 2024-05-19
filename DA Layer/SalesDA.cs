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
    internal class SalesDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Sales> RetrieveAllSales()
        {
            string query = "SELECT s.sales_id, concat(e.employee_first_name, ' ', e.employee_surname, ' ', e.employee_middle_name) as employee, ifnull(concat(c.customer_first_name, ' ', c.customer_surname, ' ', c.customer_middle_name), '') as customer, s.sale_time, ifnull(group_concat(Distinct product_name order by 1 separator ', '), '') as sold_items, ifnull(sum(sales_list_quantity), 0) as quantity, ifnull(sum(sales_list_price * sales_list_quantity), 0) as total from sales s join employee e using(employee_id) left join customer c using(customer_id) left join sales_list using(sales_id) left join product_quantity using(product_quantity_id) left join product using(product_id) group by 1 order by sale_time desc;";
            cmd = DBHelper.RunQuery(query);
            List<Sales> salesList = new List<Sales>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["sales_id"].ToString());
                    string employee_name = dr["employee"].ToString();
                    string customer_name = dr["customer"].ToString();
                    DateTime time_of_sale = (DateTime)dr["sale_time"];
                    string soldItems = dr["sold_items"].ToString();
                    int quantity = Convert.ToInt32(dr["quantity"].ToString());
                    double total = Convert.ToDouble(dr["total"]);
                    Sales sales = new Sales(id, employee_name, customer_name, time_of_sale, soldItems, quantity, total);
                    salesList.Add(sales);
                }
            }
            return salesList;
        }

        public static List<Sales> RetrieveAllSalesDateQuantity()
        {
            string query = "SELECT concat(Year(sale_time), ' ', Month(sale_time), ' ', Day(sale_time)) as sale_date, ifnull(sum(sales_list_quantity), 0) as quantity from sales s join employee e using(employee_id) left join customer c using(customer_id) left join sales_list using(sales_id) left join product p using(product_id) group by sale_date order by sale_date desc;";
            cmd = DBHelper.RunQuery(query);
            List<Sales> salesList = new List<Sales>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string [] date = dr["sale_date"].ToString().Split();
                    DateTime time_of_sale = new DateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2]));
                    int quantity = Convert.ToInt32(dr["quantity"].ToString());
                    Sales sales = new Sales(time_of_sale, quantity);
                    salesList.Add(sales);
                }
            }
            return salesList;
        }

        public static List<Sales> SalesSearch(string search)
        {
            string query = "SELECT s.sales_id, concat(e.employee_first_name, ' ', e.employee_surname, ' ', e.employee_middle_name) as employee, ifnull(concat(c.customer_first_name, ' ', c.customer_surname, ' ', c.customer_middle_name), '') as customer, s.sale_time, ifnull(group_concat(Distinct product_name order by 1 separator ', '), '') as sold_items, ifnull(sum(sales_list_quantity), 0) as quantity, ifnull(sum(product_price * sales_list_quantity), 0) as total from sales s join employee e using(employee_id) left join customer c using(customer_id) left join sales_list using(sales_id) left join product_quantity using(product_quantity_id) left join product using(product_id) where concat(e.employee_first_name, ' ', e.employee_surname, ' ', e.employee_middle_name) like @searchParameter or ifnull(concat(c.customer_first_name, ' ', c.customer_surname, ' ', c.customer_middle_name), '') like @searchParameter or sale_time like @searchParameter or product_name like @searchParameter group by 1 order by sale_time desc;";
            cmd = DBHelper.RunQuerySearch(query, search);
            List<Sales> salesList = new List<Sales>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["sales_id"].ToString());
                    string employee_name = dr["employee"].ToString();
                    string customer_name = dr["customer"].ToString();
                    DateTime time_of_sale = (DateTime)dr["sale_time"];
                    string soldItems = dr["sold_items"].ToString();
                    int quantity = Convert.ToInt32(dr["quantity"].ToString());
                    double total = Convert.ToDouble(dr["total"]);
                    Sales sales = new Sales(id, employee_name, customer_name, time_of_sale, soldItems, quantity, total);
                    salesList.Add(sales);
                }
            }
            return salesList;
        }

        public static List<Sales> SalesFilter(string employeeNameS, string customerNameS, DateTime? dateFromS, DateTime? dateToS, string productNameS, string quantityFromS, string quantityToS)
        {
            string query = "SELECT s.sales_id, concat(e.employee_first_name, ' ', e.employee_surname, ' ', e.employee_middle_name) as employee, ifnull(concat(c.customer_first_name, ' ', c.customer_surname, ' ', c.customer_middle_name), '') as customer, s.sale_time, ifnull(group_concat(Distinct product_name order by 1 separator ', '), '') as sold_items, ifnull(sum(sales_list_quantity), 0) as quantity, ifnull(sum(product_price * sales_list_quantity), 0) as total from sales s join employee e using(employee_id) left join customer c using(customer_id) left join  sales_list using(sales_id) left join product_quantity using(product_quantity_id) left join product using(product_id) where sales_id = sales_id";
            cmd = DBHelper.RunQuerySalesFilter(query, employeeNameS, customerNameS, dateFromS, dateToS, productNameS, quantityFromS, quantityToS);
            List<Sales> salesList = new List<Sales>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["sales_id"].ToString());
                    string employee_name = dr["employee"].ToString();
                    string customer_name = dr["customer"].ToString();
                    DateTime time_of_sale = (DateTime)dr["sale_time"];
                    string soldItems = dr["sold_items"].ToString();
                    int quantity = Convert.ToInt32(dr["quantity"].ToString());
                    double total = Convert.ToDouble(dr["total"]);
                    Sales sales = new Sales(id, employee_name, customer_name, time_of_sale, soldItems, quantity, total);
                    salesList.Add(sales);
                }
            }
            return salesList;
        }

        public static List<Sales> SalesDelete(int parameter)
        {
            string query = "DELETE FROM sales_list WHERE sales_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, parameter);
            query = "DELETE FROM sales WHERE sales_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, parameter);
            return RetrieveAllSales();
        }

        public static void SalesAdd(int employeeId, int customerId, DateTime saleTime)
        {
            string query = "insert into sales (employee_id, customer_id, sale_time) values (@employeeId, @customerId, @saleTime);";
            cmd = DBHelper.RunQuerySalesAddChange(query, employeeId, customerId, saleTime);
        }

        public static void SalesAdd(int employeeId, DateTime saleTime)
        {
            string query = "insert into sales (employee_id, sale_time) values (@employeeId, @saleTime);";
            cmd = DBHelper.RunQuerySalesAddChange(query, employeeId, saleTime);
        }

        public static void SalesChange(int id, int employeeId, int customerId, DateTime saleTime)
        {
            string query = "update sales set employee_id = @employeeId, customer_id =  @customerId, sale_time = @saleTime where sales_id = " + id + ";";
            cmd = DBHelper.RunQuerySalesAddChange(query, employeeId, customerId, saleTime);
        }

        public static void SalesChange(int id, int employeeId, DateTime saleTime)
        {
            string query = "update sales set employee_id = @employeeId, sale_time = @saleTime where sales_id = " + id + ";";
            cmd = DBHelper.RunQuerySalesAddChange(query, employeeId, saleTime);
        }
    }
}
