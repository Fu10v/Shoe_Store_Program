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
    internal class ColorDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Classes.Color> RetrieveAllColors()
        {
            string query = "SELECT color_id, color_name FROM color;";
            cmd = DBHelper.RunQuery(query);
            List<Classes.Color> colors = new List<Classes.Color>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["color_id"].ToString());
                    string name = dr["color_name"].ToString();
                    Classes.Color color = new Classes.Color(id, name);
                    colors.Add(color);
                }
            }
            return colors;
        }

        public static Classes.Color ColorAdd(string colorName)
        {
            string query = "INSERT INTO color (color_name) VALUES (@firstParameter);";
            cmd = DBHelper.RunQuery(query, colorName);
            query = "select color_id, color_name from color where color_name = @firstParameter;";
            cmd = DBHelper.RunQuery(query, colorName);
            Classes.Color color = new Classes.Color();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["color_id"].ToString());
                    string name = dr["color_name"].ToString();
                    color = new Classes.Color(id, name);
                }
            }
            return color;
        }
    }
}
