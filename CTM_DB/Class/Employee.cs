using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Windows.Forms;
using System.Drawing;

namespace CTM_DB.Class
{
    class Employee
    {
        string Number;
        string Name;
        string Connect1;
        string Connect2;
        string Children;
        string Type;
        string Class;
        string Number_Hour;
        string Detail;
        List<Employee_Receipt> Sallary;

        public Employee() { }

        public Employee(string number, string name, string connect1, string connect2, string children, string type, string _class, string number_hour, string detail, List<Employee_Receipt> sallary)
        {
            Number = number;
            Name = name;
            Connect1 = connect1;
            Connect2 = connect2;
            Children = children;
            Type = type;
            Class = _class;
            Number_Hour = number_hour;
            Detail = detail;
            Sallary = sallary;
        }

        #region Get Function 

        // Get Number
        //

        public string Get_Number()
        {
            return Number;
        }

        // Get Name
        // 

        public string Get_Name()
        {
            return Name;
        }

        // Get Connect 1
        //

        public string Get_Connect1()
        {
            return Connect1;
        }

        // Get Connect 2
        //

        public string Get_Connect2()
        {
            return Connect2;
        }

        // Get Children
        //

        public string Get_Children()
        {
            return Children;
        }

        // Get Type
        //

        public string Get_Type()
        {
            return Type;
        }

        // Get Class
        //

        public string Get_Class()
        {
            return Class;
        }

        // Get Number Hour
        //

        public string Get_Number_Hour()
        {
            return Number_Hour;
        }

        // Get Detail
        //

        public string Get_Detail()
        {
            return Detail;
        }

        // Get Sallary
        //

        public List<Employee_Receipt> Get_Sallary()
        {
            return Sallary;
        }

        #endregion

        #region SQL Function

        // Insert Employee Into Employee Table
        //

        public void Sql_Insert(Employee Employee)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Insert into Employee_Table(Number, Name, Connect1, Connect2, Children, Type, Class, Number_Hour, Detail) values('" + Employee.Number + "','" + Employee.Name + "','" + Employee.Connect1 + "','" + Employee.Connect2 + "','" + Employee.Children + "','" + Employee.Type + "','" + Employee.Class + "','" + Employee.Number_Hour + "','" + Employee.Detail + "')", Connect);
            try
            {
                Connect.Open();
                Cmd.ExecuteNonQuery();
            }
            finally
            {
                Connect.Close();
            }
            return;
        }

        // Delete Employee From Sql DataBase Using Id
        //

        public void Sql_Delete(string Employee_Number)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Delete From Employee_Table Where Number='" + Employee_Number + "'", Connect);
            try
            {
                Employee_Receipt Employee_Receipt = new Employee_Receipt();
                Employee_Receipt.Sql_Delete(Employee_Number);

                Connect.Open();
                Cmd.ExecuteNonQuery();

            }
            finally
            {
                Connect.Close();
            }
            return;
        }

        // Read All Employee From Sql Data Base ...
        //

        public List<Employee> Sql_Select()
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Employee_Table", Connect);

            SqlDataReader Read;
            List<Employee> Employees = new List<Employee>();
            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    Employee_Receipt Employee_Receipt = new Employee_Receipt();
                    List<Employee_Receipt> Employee_Receipts = Employee_Receipt.Sql_Select(Read["Number"].ToString());

                    Employee Employee = new Employee(Read["Number"].ToString(), Read["Name"].ToString(), Read["Connect1"].ToString(), Read["Connect2"].ToString(), Read["Children"].ToString(), Read["Type"].ToString(), Read["Class"].ToString(), Read["Number_Hour"].ToString(), Read["Detail"].ToString(), Employee_Receipts);
                    Employees.Add(Employee);
                }
            }
            finally
            {
                Connect.Close();
            }

            return Employees;
        }

        #endregion

        #region FireBase Function

        // Insert Data In FireBase DataBase ...
        //

        public async void FireBase_Insert(Employee Employee)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            var Employee_Data = new FireBase_Employee
            {
                Number = Employee.Get_Number(),
                Name = Employee.Get_Name(),
                Connect1 = Employee.Get_Connect1(),
                Connect2 = Employee.Get_Connect2(),
                Children = Employee.Get_Children(),
                Type = Employee.Get_Type(),
                Class = Employee.Get_Class(),
                Number_Hour = Employee.Get_Number_Hour(),
                Detail = Employee.Get_Detail()
            };

            SetResponse Respose = await Client.SetTaskAsync("Employee/" + Employee.Get_Number(), Employee_Data);

            return;
        }

        // Delete Employee From FireBase DataBase ...
        //

        public async void FireBase_Delete(string Employee_Number)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            FirebaseResponse Respose = await Client.DeleteTaskAsync("Employee/" + Employee_Number);
        }

        #endregion

        #region GUI Function

        public void Set_Employee_Info(List<TextBox> TextBox, ref Employee Employee)
        {
            List<Employee_Receipt> Employee_Receipt = new List<Employee_Receipt>();
            Employee = new Employee(TextBox[0].Text, TextBox[1].Text, TextBox[2].Text, TextBox[3].Text, TextBox[4].Text, TextBox[5].Text,
            TextBox[6].Text, TextBox[7].Text, TextBox[8].Text, Employee_Receipt);
        }

        public void Get_Employee_Info(ref List<TextBox> TextBox, Employee Employee)
        {
            if (Employee.Get_Number() != "000000000")
            {
                TextBox[0].Text = Employee.Get_Number();
                TextBox[0].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[0].Text = "000000000";
                TextBox[0].ForeColor = Color.Silver;
            }

            if (Employee.Get_Name() != "اسم الموظف")
            {
                TextBox[1].Text = Employee.Get_Name();
                TextBox[1].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[1].Text = "اسم الموظف";
                TextBox[1].ForeColor = Color.Silver;
            }

            if (Employee.Get_Connect1() != "022-0")
            {
                TextBox[2].Text = Employee.Get_Connect2();
                TextBox[2].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[2].Text = "022-0";
                TextBox[2].ForeColor = Color.Silver;
            }

            if (Employee.Get_Connect2() != "022-0")
            {
                TextBox[3].Text = Employee.Get_Connect2();
                TextBox[3].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[3].Text = "022-0";
                TextBox[3].ForeColor = Color.Silver;
            }

            if (Employee.Get_Children() != "لا يوجد / اسم الحساب")
            {
                TextBox[4].Text = Employee.Get_Children();
                TextBox[4].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[4].Text = "لا يوجد / اسم الحساب";
                TextBox[4].ForeColor = Color.Silver;
            }

            if (Employee.Get_Type() != "مدرس صف / اختصاص")
            {
                TextBox[5].Text = Employee.Get_Type();
                TextBox[5].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[5].Text = "مدرس صف / اختصاص";
                TextBox[5].ForeColor = Color.Silver;
            }

            if (Employee.Get_Class() != "ابتدائي / اعدادي")
            {
                TextBox[6].Text = Employee.Get_Class();
                TextBox[6].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[6].Text = "ابتدائي / اعدادي";
                TextBox[6].ForeColor = Color.Silver;
            }

            if (Employee.Get_Number_Hour() != "اسبوعيا")
            {
                TextBox[7].Text = Employee.Get_Number_Hour();
                TextBox[7].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[7].Text = "اسبوعيا";
                TextBox[7].ForeColor = Color.Silver;
            }

            if (Employee.Get_Detail() != "التفاصيل")
            {
                TextBox[8].Text = Employee.Get_Detail();
                TextBox[8].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[8].Text = "التفاصيل";
                TextBox[8].ForeColor = Color.Silver;
            }
        }

        #endregion

        # region Help Function

        public string New_Id()
        {
            Employee Employee = new Employee();
            List<Employee> Employees = Employee.Sql_Select();

            int Number_Employee = Employees.Count;

            if (Number_Employee == 0)
            {
                string Id = "181900001";
                return Id;
            }
            else
            {
                string Id = (int.Parse(Employees[Number_Employee - 1].Get_Number()) + 1).ToString();
                return Id;
            }
        }


        #endregion

    }
}
