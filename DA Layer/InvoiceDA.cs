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
    internal class InvoiceDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Invoice> RetrieveAllInvoices()
        {
            string query = "SELECT invoice_id, concat(e.employee_first_name, ' ', e.employee_surname, ' ', e.employee_middle_name) as employee, supplier_name, supply_time, ifnull(group_concat(Distinct product_name order by 1 separator ', '), '') as delivered_items from invoice left join employee e using(employee_id) left join supplier using(supplier_id) left join supply_list using(invoice_id) left join product_quantity using(product_quantity_id) left join product using(product_id) group by 1 order by supply_time desc;";
            cmd = DBHelper.RunQuery(query);
            List<Invoice> invoices = new List<Invoice>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["invoice_id"].ToString());
                    string employee_name = dr["employee"].ToString();
                    string supplier_name = dr["supplier_name"].ToString();
                    DateTime supply_time = (DateTime)dr["supply_time"];
                    string delivered_items = dr["delivered_items"].ToString();
                    Invoice invoice = new Invoice(id, employee_name, supplier_name, supply_time, delivered_items);
                    invoices.Add(invoice);
                }
            }
            return invoices;
        }

        public static List<Invoice> InvoiceSearch(string search)
        {
            string query = "SELECT invoice_id, concat(e.employee_first_name, ' ', e.employee_surname, ' ', e.employee_middle_name) as employee, supplier_name, supply_time, ifnull(group_concat(Distinct product_name order by 1 separator ', '), '') as delivered_items from invoice left join employee e using(employee_id) left join supplier using(supplier_id) left join supply_list using(invoice_id) left join product_quantity using(product_quantity_id) left join product p using(product_id) where concat(e.employee_first_name, ' ', e.employee_surname, ' ', e.employee_middle_name) like @searchParameter or supplier_name like @searchParameter or supply_time like @searchParameter or p.product_name like @searchParameter group by 1 order by supply_time desc;";
            cmd = DBHelper.RunQuerySearch(query, search);
            List<Invoice> invoices = new List<Invoice>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["invoice_id"].ToString());
                    string employee_name = dr["employee"].ToString();
                    string supplier_name = dr["supplier_name"].ToString();
                    DateTime supply_time = (DateTime)dr["supply_time"];
                    string delivered_items = dr["delivered_items"].ToString();
                    Invoice invoice = new Invoice(id, employee_name, supplier_name, supply_time, delivered_items);
                    invoices.Add(invoice);
                }
            }
            return invoices;
        }

        public static List<Invoice> InvoiceFilter(string employeeNameS, string supplierNameS, DateTime? dateFromS, DateTime? dateToS, string productNameS)
        {
            string query = "SELECT invoice_id, concat(e.employee_first_name, ' ', e.employee_surname, ' ', e.employee_middle_name) as employee, supplier_name, supply_time, ifnull(group_concat(Distinct p.product_name order by 1 separator ', '), '') as delivered_items from invoice left join employee e using(employee_id) left join supplier using(supplier_id) left join supply_list using(invoice_id) left join product_quantity using(product_quantity_id) left join product p using(product_id) where invoice_id like invoice_id";
            cmd = DBHelper.RunQueryInvoiceFilter(query, employeeNameS, supplierNameS, dateFromS, dateToS, productNameS);
            List<Invoice> invoices = new List<Invoice>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["invoice_id"].ToString());
                    string employee_name = dr["employee"].ToString();
                    string supplier_name = dr["supplier_name"].ToString();
                    DateTime supply_time = (DateTime)dr["supply_time"];
                    string delivered_items = dr["delivered_items"].ToString();
                    Invoice invoice = new Invoice(id, employee_name, supplier_name, supply_time, delivered_items);
                    invoices.Add(invoice);
                }
            }
            return invoices;
        }

        public static List<Invoice> InvoiceDelete(int parameter)
        {
            string query = "DELETE FROM supply_list WHERE invoice_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, parameter);
            query = "DELETE FROM invoice WHERE invoice_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, parameter);
            return RetrieveAllInvoices();
        }

        public static void InvoiceAdd(int employeeId, int supplierId, DateTime supplyTime)
        {
            string query = "INSERT INTO invoice (employee_id, supplier_id, supply_time) VALUES (@employeeId, @supplierId, @supplyTime);";
            cmd = DBHelper.RunQueryInvoiceAddChange(query, employeeId, supplierId, supplyTime);
        }

        public static void InvoiceChange(int id, int employeeId, int supplierId, DateTime supplyTime)
        {
            string query = "UPDATE invoice SET employee_id = @employeeId, supplier_id = @supplierId, supply_time = @supplyTime WHERE invoice_id = " + id + ";";
            cmd = DBHelper.RunQueryInvoiceAddChange(query, employeeId, supplierId, supplyTime);
        }

    }
}
