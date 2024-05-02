using Shoe_Store_DB.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shoe_Store_DB.Helper;

namespace Shoe_Store_DB.DA_Layer
{
    public static class UserDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static User RetrieveUser(string username)
        {
            string query = "SELECT user_login, user_pass, user_mode FROM shoe_store_db.db_user where user_login = (@firstParameter) limit 1";
            cmd = DBHelper.RunQuery(query, username);
            User aUser = null;
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string userName = dr["user_login"].ToString();
                    string password = dr["user_pass"].ToString();
                    string mode = dr["user_mode"].ToString();
                    aUser = new User(userName, password, mode);
                }
            }
            return aUser;
        }
    }
}