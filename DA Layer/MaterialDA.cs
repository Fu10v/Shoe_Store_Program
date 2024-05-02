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
    internal class MaterialDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        public static List<Classes.Material> RetrieveAllMaterials()
        {
            string query = "SELECT material_id, material_name FROM material;";
            cmd = DBHelper.RunQuery(query);
            List<Classes.Material> materials = new List<Classes.Material>();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["material_id"].ToString());
                    string name = dr["material_name"].ToString();
                    Classes.Material material = new Classes.Material(id, name);
                    materials.Add(material);
                }
            }
            return materials;
        }
        public static Classes.Material MaterialAdd(string materialName)
        {
            string query = "INSERT INTO material (material_name) VALUES (@firstParameter);";
            cmd = DBHelper.RunQuery(query, materialName);
            query = "select material_id, material_name from material where material_name = @firstParameter;";
            cmd = DBHelper.RunQuery(query, materialName);
            Classes.Material material = new Classes.Material();
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["material_id"].ToString());
                    string name = dr["material_name"].ToString();
                    material = new Classes.Material(id, name);
                }
            }
            return material;
        }
    }
}

