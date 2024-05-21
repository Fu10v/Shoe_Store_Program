using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using Shoe_Store_DB.Classes;
using Shoe_Store_DB.DA_Layer;

namespace Shoe_Store_DB.Helper
{
    internal class DBHelper
    {
        private static MySqlConnection connection;
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static void EstablishConnection()
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "127.0.0.1";
                builder.UserID = "root";
                builder.Password = "YourMajesty5";
                builder.Database = "shoe_store_db";
                builder.SslMode = MySqlSslMode.None;
                connection = new MySqlConnection(builder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("connection Failed");
            }
        }

        public static MySqlCommand RunQuery(string query)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQuery(string query, string firstParameter)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@firstParameter", firstParameter);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
        public static MySqlCommand RunQuery(string query, int firstParameter)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@firstParameter", firstParameter);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }


        public static MySqlCommand RunQuerySearch(string query, string searchParameter)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@searchParameter", $"%{searchParameter}%");
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQuerySecondSearch(string query, int firstParameter, string searchParameter)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@firstParameter", firstParameter);
                    cmd.Parameters.AddWithValue("@searchParameter", $"%{searchParameter}%");
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQueryProductAddChange(string query, string name, string gender, int typeId, int brandId, int materialId, string season, double price)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@typeId", typeId);
                    cmd.Parameters.AddWithValue("@brandId", brandId);
                    cmd.Parameters.AddWithValue("@materialId", materialId);
                    cmd.Parameters.AddWithValue("@season", season);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQueryProductQuantityAddChange(string query, int productId, int sizeId, int colorId, int quantity)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@productId", productId);
                    cmd.Parameters.AddWithValue("@sizeId", sizeId);
                    cmd.Parameters.AddWithValue("@colorId", colorId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQueryProductQuantityAddChangeQuantity(string query, int quantity)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQuerySalesAddChange(string query, int employeeId, int customerId, DateTime saleTime)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    cmd.Parameters.AddWithValue("@saleTime", saleTime);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQuerySalesAddChange(string query, int employeeId, DateTime saleTime)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);
                    cmd.Parameters.AddWithValue("@saleTime", saleTime);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQuerySalesListAddChange(string query, int salesId, int productQuantityId, double price, int quantity)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@salesId", salesId);
                    cmd.Parameters.AddWithValue("@productQuantityId", productQuantityId);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQueryEmployeeAddChange(string query, string firstName, string surname, string middleName, string position, long phoneNumber, string email, string address, string gender, DateTime? dateOfBirth)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@surname", surname);
                    cmd.Parameters.AddWithValue("@middleName", middleName);
                    cmd.Parameters.AddWithValue("@position", position);
                    cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQueryCustomerAddChange(string query, string firstName, string surname, string middleName, long phoneNumber, string email, long discId, double discAccum)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@surname", surname);
                    cmd.Parameters.AddWithValue("@middleName", middleName);
                    cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@discId", discId);
                    cmd.Parameters.AddWithValue("@discAccum", discAccum);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQuerySupplierAddChange( string query, string Sname, long erdpouCode, long fpn, long? spn, string email, string address, long curentAccount)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Sname", Sname);
                    cmd.Parameters.AddWithValue("@erdpouCode", erdpouCode);
                    cmd.Parameters.AddWithValue("@fpn", fpn);
                    cmd.Parameters.AddWithValue("@spn", spn);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@curentAccount", curentAccount);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQueryProductReturnAddChange(string query, int salesId, string returnReason)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@salesId", salesId);
                    cmd.Parameters.AddWithValue("@returnReason", returnReason);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQueryProductReturnListAddChange(string query, int productReturnId, int salesListId, int productReturnListQuantity)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@productReturnId", productReturnId);
                    cmd.Parameters.AddWithValue("@salesListId", salesListId);
                    cmd.Parameters.AddWithValue("@productReturnListQuantity", productReturnListQuantity);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQueryInvoiceAddChange(string query, int employeeId, int supplierId, DateTime supplyTime)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);
                    cmd.Parameters.AddWithValue("@supplierId", supplierId);
                    cmd.Parameters.AddWithValue("@supplyTime", supplyTime);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQuerySupplyListAddChange(string query, int invoiceId, int productQuantityId, int price, int quantity)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@invoiceId", invoiceId);
                    cmd.Parameters.AddWithValue("@productQuantityId", productQuantityId);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQueryProductFilter(string query, string genderS, string typeS, string brandS, string materialS, string seasonS, string colorS, string sizeS, string priceFromS, string priceToS, string quantityFromS, string quantityToS)
        {
            try
            {
                if (genderS != null && genderS != "")
                {
                    query = query + " and product_gender = \"" + genderS + "\"";
                }
                if (typeS != null && typeS != "")
                {
                    query = query + " and type_name = \"" + typeS + "\"";
                }
                if (brandS != null && brandS != "")
                {
                    query = query + " and brand_name = \"" + brandS + "\"";
                }
                if (materialS != null && materialS != "")
                {
                    query = query + " and material_name = \"" + materialS + "\"";
                }
                if (seasonS != null && seasonS != "")
                {
                    query = query + " and product_season = \"" + seasonS + "\"";
                }
                if (colorS != null && colorS != "")
                {
                    query = query + " and color_name = \"" + colorS + "\"";
                }
                if (sizeS != null && sizeS != "")
                {
                    query = query + " and size_name = \"" + sizeS + "\"";
                }
                if (priceFromS != null && priceFromS != "" && priceFromS != "від")
                {
                    query = query + " and product_price between " + priceFromS + "";
                }
                else
                {
                    query = query + " and product_price between product_price";
                }
                if (priceToS != null && priceToS != "" && priceToS != "до")
                {
                    query = query + " and " + priceToS + "";
                }
                else
                {
                    query = query + " and product_price";
                }
                query = query + " group by 1";
                if ((quantityFromS != null && quantityFromS != "" && quantityFromS != "від") || (quantityToS != null && quantityToS != "" && quantityToS != "до"))
                {
                    query = query + " having";
                    if (quantityFromS != null && quantityFromS != "" && quantityFromS != "від")
                    {
                        query = query + " quantity between " + quantityFromS + "";
                    }
                    else
                    {
                        query = query + " quantity between quantity";
                    }
                    if (quantityToS != null && quantityToS != "" && quantityToS != "до")
                    {
                        query = query + " and " + quantityToS + "";
                    }
                    else
                    {
                        query = query + " and quantity";
                    }
                }
                query = query + " order by quantity desc;";
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQuerySalesFilter(string query, string employeeNameS, string customerNameS, DateTime? dateFromS, DateTime? dateToS, string productNameS, string quantityFromS, string quantityToS)
        {
            try
            {
                if (productNameS != null && productNameS != "")
                {
                    query = query + " and product_name = \"" + productNameS + "\"";
                }
                if (dateFromS != null)
                {
                    query = query + " and sale_time between @dateFrom";
                }
                else
                {
                    query = query + " and sale_time between sale_time";
                }
                if (dateToS != null)
                {
                    query = query + " and @dateTo";
                }
                else
                {
                    query = query + " and sale_time";
                }
                query = query + " group by 1";
                query = query + " having";
                if ((employeeNameS != null && employeeNameS != "") || (customerNameS != null && customerNameS != ""))
                {
                    
                    if (employeeNameS != null && employeeNameS != "")
                    {
                        query = query + " employee = \"" + employeeNameS + "\"";
                        if (customerNameS != null && customerNameS != "")
                        {
                            query = query + " and customer = \"" + customerNameS + "\"";
                        }
                    }
                    if ((customerNameS != null && customerNameS != "") && (employeeNameS == null || employeeNameS == ""))
                    {
                        query = query + " customer = \"" + customerNameS + "\"";
                    }
                }
                else query = query + " employee = employee";
                if ((quantityFromS != null && quantityFromS != "" && quantityFromS != "від") || (quantityToS != null && quantityToS != "" && quantityToS != "до"))
                {
                    query = query + " and";
                    if (quantityFromS != null && quantityFromS != "" && quantityFromS != "від")
                    {
                        query = query + " total between " + quantityFromS + "";
                    }
                    else
                    {
                        query = query + " total between total";
                    }
                    if (quantityToS != null && quantityToS != "" && quantityToS != "до")
                    {
                        query = query + " and " + quantityToS + "";
                    }
                    else
                    {
                        query = query + " and total";
                    }
                }
                query = query + " order by sale_time desc";
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@dateFrom", dateFromS);
                    cmd.Parameters.AddWithValue("@dateTo", dateToS);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }


        public static MySqlCommand RunQueryEmployeeFilter(string query, string positionS, string genderS, DateTime? dateFromS, DateTime? dateToS)
        {
            try
            {
                bool dateFromKey = false;
                bool dateToKey = false;
                if (positionS != null && positionS != "")
                {
                    query = query + " and employee_position = \"" + positionS + "\"";
                }
                if (genderS != null && genderS != "")
                {
                    query = query + " and employee_gender = \"" + genderS + "\"";
                }
                
                if (dateFromS != null)
                {
                    query = query + " and employee_date_of_birth between @dateFrom";
                }
                else
                {
                    query = query + " and employee_date_of_birth between employee_date_of_birth";
                }
                if (dateToS != null)
                {
                    query = query + " and @dateTo";
                }
                else
                {
                    query = query + " and employee_date_of_birth";
                }
                query = query + " group by employee_id order by employee_name;";
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@dateFrom", dateFromS);
                    cmd.Parameters.AddWithValue("@dateTo", dateToS);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQueryCustomerFilter(string query, string from, string to)
        {
            try
            {

                if (from != null && from != "" && from != "від")
                {
                    query = query + " and discount_card_accumulation between " + from + "";
                }
                else
                {
                    query = query + " and discount_card_accumulation between discount_card_accumulation";
                }
                if (to != null && to != "" && to != "до")
                {
                    query = query + " and " + to + "";
                }
                else
                {
                    query = query + " and discount_card_accumulation";
                }
                query = query + " order by customer_name;";
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
        public static MySqlCommand RunQueryInvoiceFilter(string query, string employeeNameS, string supplierNameS, DateTime? dateFromS, DateTime? dateToS, string productNameS)
        {
            try
            {
                if (productNameS != null && productNameS != "")
                {
                    query = query + " and product_name = \"" + productNameS + "\"";
                }
                if (supplierNameS != null && supplierNameS != "")
                {
                    query = query + " and supplier_name = \"" + supplierNameS + "\"";
                }
                if (dateFromS != null)
                {
                    query = query + " and supply_time between @dateFrom";
                }
                else
                {
                    query = query + " and supply_time between supply_time";
                }
                if (dateToS != null)
                {
                    query = query + " and @dateTo";
                }
                else
                {
                    query = query + " and supply_time";
                }
                query = query + " group by 1";
                if ((employeeNameS != null && employeeNameS != ""))
                {
                    query = query + " having";
                    query = query + " employee = \"" + employeeNameS + "\"";
                }
                query = query + " order by supply_time desc;";
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@dateFrom", dateFromS);
                    cmd.Parameters.AddWithValue("@dateTo", dateToS);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
    }
}
