using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
namespace CTM_DB.Class
{
    class User
    {
        string Id;
        string User_Name;
        string Password;
        string First_Name;
        string Last_Name;
        string Type;

        public User() { }

        public User(string id, string user_name, string password, string first_name, string last_name, string type)
        {
            Id = id;
            User_Name = user_name;
            Password = password;
            First_Name = first_name;
            Last_Name = last_name;
            Type = type;
        }

        #region Get_Function

        // Get Id
        //

        public string Get_Id()
        {
            return Id;
        }

        // Get User Name
        //

        public string Get_User()
        {
            return User_Name;
        }

        // Get Password
        //

        public string Get_Password()
        {
            return Password;
        }

        // Get First Name
        //

        public string Get_First_Name()
        {
            return First_Name;
        }

        // Get Last Name
        //

        public string Get_Last_Name()
        {
            return Last_Name;
        }

        // Get Type
        //

        public string Get_Type()
        {
            return Type;
        }

        #endregion

        #region SQL Function 

        // Read User Where Sql Data Base ...
        //

        public List<User> Sql_Select()
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From User_Table", Connect);

            SqlDataReader Read;
            List<User> Users = new List<User>();

            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                  User User = new User(Read["Id"].ToString(), Read["User_Name"].ToString(), Read["Password"].ToString(), Read["First_Name"].ToString(), Read["Last_Name"].ToString(), Read["Type"].ToString());
                  Users.Add(User);
                }
            }
            finally
            {
                Connect.Close();
            }

            return Users;
        }

        #endregion
    }
}
