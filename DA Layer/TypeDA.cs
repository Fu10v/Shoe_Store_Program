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
    internal class TypeDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Classes.Type> RetrieveAllTypes()
        {
            string query = "SELECT type_id, type_name FROM product_type;";
            cmd = DBHelper.RunQuery(query);
            List<Classes.Type> types = new List<Classes.Type>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["type_id"].ToString());
                    string name = dr["type_name"].ToString();
                    Classes.Type type = new Classes.Type(id, name);
                    types.Add(type);
                }
            }
            return types;
        }

        public static Classes.Type TypeAdd(string typeName)
        {
            string query = "INSERT INTO product_type (type_name) VALUES (@firstParameter);";
            cmd = DBHelper.RunQuery(query, typeName);
            query = "select type_id, type_name from product_type where type_name = @firstParameter;";
            cmd = DBHelper.RunQuery(query, typeName);
            Classes.Type type = new Classes.Type();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["type_id"].ToString());
                    string name = dr["type_name"].ToString();
                    type = new Classes.Type(id, name);
                }
            }
            return type;
        }
    }
}
