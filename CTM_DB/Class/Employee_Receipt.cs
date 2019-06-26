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
namespace CTM_DB.Class
{
    class Employee_Receipt
    {
        string Number;
        string Book;
        string Owner;
        string Date;
        string Name;
        string Sallary;
        string Discount;
        string Total;
        string Detail;
        string Employee_Number;

        public Employee_Receipt() { }

        public Employee_Receipt(string number, string book, string owner, string date,
            string name, string sallary, string discount, string total, string detail, string employee_number)
        {
            Number = number;
            Book = book;
            Owner = owner;
            Date = date;
            Name = name;
            Sallary = sallary;
            Discount = discount;
            Total = total;
            Detail = detail;
            Employee_Number = employee_number;
        }

        #region Get Function

        // Get Number
        //

        public string Get_Number()
        {
            return Number;
        }

        // Get Book
        //

        public string Get_Book()
        {
            return Book;
        }

        // Get Owner
        //

        public string Get_Owner()
        {
            return Owner;
        }

        // Get Date 
        //

        public string Get_Date()
        {
            return Date;
        }

        // Get Name
        //

        public string Get_Name()
        {
            return Name;
        }

        // Get Sallary
        //

        public string Get_Sallary()
        {
            return Sallary;
        }

        // Get Discount
        //

        public string Get_Discount()
        {
            return Discount;
        }

        // Get Total 
        //

        public string Get_Total()
        {
            return Total;
        }

        // Get Detail
        //

        public string Get_Detail()
        {
            return Detail;
        }

        // Get Employee Number 
        //

        public string Get_Employee_Number()
        {
            return Employee_Number;
        }

        #endregion

        #region SQL Function

        // Insert Employee  Receipt Into Sql DataBase ...
        //

        public void Sql_Insert(Employee_Receipt Employee_Receipt)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Insert into Employee_Receipt_Table(Number, Book, Owner, Date, Name, Sallary, Discount, Total, Detail, Employee_Number) values('" + Employee_Receipt.Number + "','" + Employee_Receipt.Book + "','" + Employee_Receipt.Owner + "','" + Employee_Receipt.Date + "','" + Employee_Receipt.Name + "','" + Employee_Receipt.Sallary + "','" + Employee_Receipt.Discount + "','" + Employee_Receipt.Total + "', '" + Employee_Receipt.Detail + "', '" + Employee_Receipt.Employee_Number + "')", Connect);
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

        // Delete Employee Receipt From Sql DataBase Using Number
        //

        public void Sql_Delete(string Employee_Receipt_Number)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Delete From Employee_Receipt_Table Where Number='" + Employee_Receipt_Number + "'", Connect);
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

        // Read Movement Receipt Where Type From Sql Data Base ...
        //

        public List<Employee_Receipt> Sql_Select(string Employee_Number)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Employee_Receipt_Table", Connect);

            SqlDataReader Read;
            List<Employee_Receipt> Employee_Receipts = new List<Employee_Receipt>();

            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    if (Read["Employee_Number"].ToString() == Employee_Number)
                    {
                        Employee_Receipt Employee_Receipt = new Employee_Receipt(Read["Number"].ToString(), Read["Book"].ToString(), Read["Owner"].ToString(), Read["Date"].ToString(), Read["Name"].ToString(), Read["Sallary"].ToString(), Read["Discount"].ToString(), Read["Total"].ToString(), Read["Detail"].ToString(),Read["Employee_Number"].ToString());
                        Employee_Receipts.Add(Employee_Receipt);
                    }
                }
            }
            finally
            {
                Connect.Close();
            }

            return Employee_Receipts;
        }

        #endregion

        #region FireBase Function

        // Insert Data In FireBase DataBase ...
        //

        public async void FireBase_Insert(Employee_Receipt Employee_Receipt)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            var Employee_Receipt_Data = new FireBase_Employee_Receipt
            {
                Number = Employee_Receipt.Get_Number(),
                Book = Employee_Receipt.Get_Book(),
                Owner = Employee_Receipt.Get_Owner(),
                Date = Employee_Receipt.Get_Date(),
                Name = Employee_Receipt.Get_Name(),
                Sallary = Employee_Receipt.Get_Sallary(),
                Discount = Employee_Receipt.Get_Discount(),
                Total = Employee_Receipt.Get_Total(),
                Detail = Employee_Receipt.Get_Detail(),
                Employee_Number = Employee_Receipt.Get_Employee_Number()
            };

            SetResponse Respose = await Client.SetTaskAsync("Employee_Receipt/" + Employee_Receipt.Get_Number(), Employee_Receipt_Data);

            return;
        }

        // Delete Employee Receipt From FireBase DataBase ...
        //

        public async void FireBase_Delete(string Employee_Receipt_Number)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            FirebaseResponse Respose = await Client.DeleteTaskAsync("Employee_Receipt/" + Employee_Receipt_Number);
        }

        #endregion

        #region GUI Function

        public void Set_Employee_Receipt_Info(List<TextBox> TextBox, ref Employee_Receipt Employee_Receipt)
        {
            Employee_Receipt = new Employee_Receipt(TextBox[0].Text, TextBox[1].Text, TextBox[2].Text, TextBox[3].Text, TextBox[4].Text, TextBox[5].Text,
            TextBox[6].Text, TextBox[7].Text, TextBox[8].Text, "");
        }

        #endregion
    }
}
