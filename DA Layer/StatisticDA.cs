using Mysqlx.Crud;
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
    internal class StatisticDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Statistic> RetrieveAllSales(int limit, string dateFrom, string dateTo, string employeeFirstName, string employeeSurname, string employeeMiddleName, string productName, string brandName)
        {
            string query = "SELECT concat(Year(sale_time), ' ', Month(sale_time), ' ', Day(sale_time)) as sale_date, ifnull(sum(sales_list_price * sales_list_quantity), 0) as total from sales s join employee e using(employee_id) left join customer c using(customer_id) left join sales_list using(sales_id) left join product_quantity using(product_quantity_id) left join product p using(product_id) left join brand using(brand_id)";
            if (employeeFirstName != "")
            {
                query = query + " where employee_first_name = @employeeFirstName and employee_surname = @employeeSurname and employee_middle_name = @employeeMiddleName";
            }
            else
            {
                query = query + " where employee_first_name = employee_first_name";
            }
            if (productName != "")
            {
                query = query + " where product_name = @productName";
            }
            else
            {
                query = query + " and product_name = product_name";
            }
            if (brandName != "")
            {
                query = query + " where brand_name = @brandName";
            }
            else
            {
                query = query + " and brand_name = brand_name";
            }
            query = query + " group by sale_date";
            query = query + " having total = total";
            if (dateFrom != "")
            {
                query = query + " and sale_date between @dateFrom";
            }
            else
            {
                query = query + " and sale_date between sale_date";
            }
            if (dateTo != "")
            {
                query = query + " and @dateTo";
            }
            else
            {
                query = query + " and sale_date";
            }
            query = query + " order by sale_date desc limit "+limit+" ;";
            cmd = DBHelper.RunQueryStatistic(query, dateFrom, dateTo, employeeFirstName, employeeSurname, employeeMiddleName, productName, brandName);
            List<Statistic> statistics = new List<Statistic>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string[] date = dr["sale_date"].ToString().Split();
                    DateTime time_of_sale = new DateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2]));
                    double quantity = Convert.ToDouble(dr["total"].ToString());
                    Statistic statistic = new Statistic(time_of_sale, quantity);
                    statistics.Add(statistic);
                }
            }
            return statistics;
        }
    }
}
