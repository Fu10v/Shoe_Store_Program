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
    internal class BrandDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Classes.Brand> RetrieveAllBrands()
        {
            string query = "SELECT brand_id, brand_name FROM brand;";
            cmd = DBHelper.RunQuery(query);
            List<Classes.Brand> brands = new List<Classes.Brand>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["brand_id"].ToString());
                    string name = dr["brand_name"].ToString();
                    Classes.Brand brand = new Classes.Brand(id, name);
                    brands.Add(brand);
                }
            }
            return brands;
        }

        public static Classes.Brand BrandAdd(string brandName)
        {
            string query = "INSERT INTO brand (brand_name) VALUES (@firstParameter);";
            cmd = DBHelper.RunQuery(query, brandName);
            query = "select brand_id, brand_name from brand where brand_name = @firstParameter;";
            cmd = DBHelper.RunQuery(query, brandName);
            Classes.Brand brand = new Classes.Brand();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["brand_id"].ToString());
                    string name = dr["brand_name"].ToString();
                    brand = new Classes.Brand(id, name);
                }
            }
            return brand;
        }
    }
}
