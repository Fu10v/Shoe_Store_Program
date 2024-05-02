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
    internal class EmployeeDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static List<Employee> RetrieveAllEmployees()
        {
            string query = "SELECT employee_id, concat(employee_first_name, ' ', employee_surname, ' ',employee_middle_name) as employee_name, employee_position, employee_phone_number, employee_email, employee_address, employee_gender, employee_date_of_birth FROM employee order by 1;";
            cmd = DBHelper.RunQuery(query);
            List<Employee> employees = new List<Employee>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["employee_id"]);
                    string name = dr["employee_name"].ToString();
                    string position = dr["employee_position"].ToString();
                    long phoneNumber = Convert.ToInt64(dr["employee_phone_number"]);
                    string email = dr["employee_email"].ToString();
                    string address = dr["employee_address"].ToString();
                    string gender = dr["employee_gender"].ToString();
                    DateTime date = (DateTime)dr["employee_date_of_birth"];
                    DateOnly dateOfBirth = new DateOnly(date.Year, date.Month, date.Day); ;
                    Employee employee = new Employee(id, name, position, phoneNumber, email, address, gender,dateOfBirth);
                    employees.Add(employee);
                }
            }
            return employees;
        }
        public static List<Employee> EmployeeSearch(string search)
        {
            string query = "SELECT employee_id, concat(employee_first_name, ' ', employee_surname, ' ', employee_middle_name) as employee_name, employee_position, employee_phone_number, employee_email, employee_address, employee_gender, employee_date_of_birth FROM employee where employee_id like @searchParameter or employee_first_name like @searchParameter or employee_surname like @searchParameter or employee_middle_name like @searchParameter or employee_position like @searchParameter or employee_phone_number like @searchParameter or employee_email like @searchParameter or employee_address like @searchParameter or employee_gender like @searchParameter or employee_date_of_birth like @searchParameter order by 1;";
            cmd = DBHelper.RunQuerySearch(query, search);
            List<Employee> employees = new List<Employee>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["employee_id"]);
                    string name = dr["employee_name"].ToString();
                    string position = dr["employee_position"].ToString();
                    long phoneNumber = Convert.ToInt64(dr["employee_phone_number"]);
                    string email = dr["employee_email"].ToString();
                    string address = dr["employee_address"].ToString();
                    string gender = dr["employee_gender"].ToString();
                    DateTime date = (DateTime)dr["employee_date_of_birth"];
                    DateOnly dateOfBirth = new DateOnly(date.Year, date.Month, date.Day); ;
                    Employee employee = new Employee(id, name, position, phoneNumber, email, address, gender, dateOfBirth);
                    employees.Add(employee);
                }
            }
            return employees;
        }

        public static List<Employee> EmployeeFilter(string positionS, string genderS, DateTime? dateFromS, DateTime? dateToS)
        {
            string query = "SELECT employee_id, concat(employee_first_name, ' ', employee_surname, ' ',employee_middle_name) as employee_name, employee_position, employee_phone_number, employee_email, employee_address, employee_gender, employee_date_of_birth FROM employee where employee_id = employee_id";
            cmd = DBHelper.RunQueryEmployeeFilter(query, positionS, genderS, dateFromS, dateToS);
            List<Employee> employees = new List<Employee>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["employee_id"]);
                    string name = dr["employee_name"].ToString();
                    string position = dr["employee_position"].ToString();
                    long phoneNumber = Convert.ToInt64(dr["employee_phone_number"]);
                    string email = dr["employee_email"].ToString();
                    string address = dr["employee_address"].ToString();
                    string gender = dr["employee_gender"].ToString();
                    DateTime date = (DateTime)dr["employee_date_of_birth"];
                    DateOnly dateOfBirth = new DateOnly(date.Year, date.Month, date.Day); ;
                    Employee employee = new Employee(id, name, position, phoneNumber, email, address, gender, dateOfBirth);
                    employees.Add(employee);
                }
            }
            return employees;
        }

        public static List<Employee> EmployeeDelete(int id)
        {
            string query = "DELETE FROM employee WHERE employee_id = @firstParameter;";
            cmd = DBHelper.RunQuery(query, id);
            return RetrieveAllEmployees();
        }

        public static void EmployeeAdd(string firstName, string surname, string middleName, string position, long phoneNumber, string email, string address, string gender, DateTime? dateOfBirth)
        {
            string query = "INSERT INTO employee (employee_first_name, employee_surname, employee_middle_name, employee_position, employee_phone_number, employee_email, employee_address, employee_gender, employee_date_of_birth) VALUES (@firstName, @surname, @middleName, @position, @phoneNumber, @email, @address, @gender, @dateOfBirth);";
            cmd = DBHelper.RunQueryEmployeeAddChange(query, firstName, surname, middleName, position, phoneNumber, email, address, gender, dateOfBirth);
        }
        public static void EmployeeChange(int id, string firstName, string surname, string middleName, string position, long phoneNumber, string email, string address, string gender, DateTime? dateOfBirth)
        {
            string query = "update employee set employee_first_name = @firstName, employee_surname = @surname, employee_middle_name = @middleName, employee_position = @position, employee_phone_number = @phoneNumber, employee_email = @email, employee_address = @address, employee_gender = @gender, employee_date_of_birth = @dateOfBirth where employee_id = " + id + ";";
            cmd = DBHelper.RunQueryEmployeeAddChange(query, firstName, surname, middleName, position, phoneNumber, email, address, gender, dateOfBirth);
        }
    }
}
