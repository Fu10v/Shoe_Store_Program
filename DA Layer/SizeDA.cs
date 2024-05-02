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
    internal class SizeDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Classes.Size> RetrieveAllSizes()
        {
            string query = "SELECT size_id, size_name FROM size;";
            cmd = DBHelper.RunQuery(query);
            List<Classes.Size> sizes = new List<Classes.Size>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["size_id"].ToString());
                    string name = dr["size_name"].ToString();
                    Classes.Size size = new Classes.Size(id, name);
                    sizes.Add(size);
                }
            }
            return sizes;
        }

        public static Classes.Size SizeAdd(string sizeName)
        {
            string query = "INSERT INTO size (size_name) VALUES (@firstParameter);";
            cmd = DBHelper.RunQuery(query, sizeName);
            query = "select size_id, size_name from size where size_name = @firstParameter;";
            cmd = DBHelper.RunQuery(query, sizeName);
            Classes.Size size = new Classes.Size();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["size_id"].ToString());
                    string name = dr["size_name"].ToString();
                    size = new Classes.Size(id, name);
                }
            }
            return size;
        }
    }
}
