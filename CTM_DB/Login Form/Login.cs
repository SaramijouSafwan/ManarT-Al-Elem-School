using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CTM_DB.Class;
using CTM_DB;
using System.IO;
using FireSharp.Config;
using FireSharp.Interfaces;
namespace CTM_DB
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Close_App_Buttom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userNameTextBox_Enter(object sender, EventArgs e)
        {
            if (userNameTextBox.Text == "Enter User Name")
            {
                userNameTextBox.Text = "";
                userNameTextBox.ForeColor = Color.DimGray;
            }
        }

        private void userNameTextBox_Leave(object sender, EventArgs e)
        {
            if (userNameTextBox.Text == "")
            {
                userNameTextBox.Text = "Enter User Name";
                userNameTextBox.ForeColor = Color.Silver;
            }
        }

        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "Enter Password")
            {
                passwordTextBox.Text = "";
                passwordTextBox.ForeColor = Color.DimGray;
            }
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "")
            {
                passwordTextBox.Text = "Enter Password";
                passwordTextBox.ForeColor = Color.Silver;
            }
        }

        private void loginButtom_Click(object sender, EventArgs e)
        {
            User User = new User();
            List<User> Users = User.Sql_Select();

            int Number_User = Users.Count;

            bool Cheak = false;

            string User_Name = "";

            for (int i = 0; i < Number_User; i++)
            {
                if (Users[i].Get_User() == userNameTextBox.Text && Users[i].Get_Password() == passwordTextBox.Text)
                {
                    Cheak = true;
                    User_Name = Users[i].Get_First_Name() + " " + Users[i].Get_Last_Name();
                    break;
                }
                else
                {
                    MessageBox.Show("اسم المستخدم او كلمة المرور غير صحيحة حاول مرة اخرى", "خطا في الدخول",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    passwordTextBox.Text = "Enter Password";
                    passwordTextBox.ForeColor = Color.Silver;
                    userNameTextBox.Text = "Enter User Name";
                    userNameTextBox.ForeColor = Color.Silver;
                    break;

                }
            }

            if (Cheak == true)
            {
                Main_Form Main = new Main_Form(ref User_Name);
                Main.Show();
            }
        }


    }
}
