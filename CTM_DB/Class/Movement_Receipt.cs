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
    class Movement_Receipt
    {
        string Number;
        string Book;
        string Type;
        string Owner;
        string Kind;
        string Amount;
        string Date;
        string Detail;

        public Movement_Receipt() { }

        public Movement_Receipt(string number, string book, string type, string owner, string kind, string amount, string date, string detail)
        {
            Number = number;
            Book = book;
            Type = type;
            Owner = owner;
            Kind = kind;
            Amount = amount;
            Date = date;
            Detail = detail;
        }

        #region Get Function

        // Get Number
        //

        public string Get_Number()
        {
            return Number;
        }

        // Get Book Number
        //

        public string Get_Book()
        {
            return Book;
        }

        // Get Type
        //

        public string Get_Type()
        {
            return Type;
        }

        // Get Owner
        //

        public string Get_Owner()
        {
            return Owner;
        }

        // Get Kind
        //

        public string Get_Kind()
        {
            return Kind;
        }

        // Get Amount
        //

        public string Get_Amount()
        {
            return Amount;
        }

        // Get Date
        //

        public string Get_Date()
        {
            return Date;
        }

        // Get Detail
        //

        public string Get_Detail()
        {
            return Detail;
        }

#endregion

        #region SQL Function

        // Insert Movement Receipt Into Sql DataBase ...
        //

        public void Sql_Insert(Movement_Receipt Movement_Receipt)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Insert into Movement_Receipt_Table(Number, Book, Type, Owner, Kind, Amount, Date, Detail) values('" + Movement_Receipt.Number + "','" + Movement_Receipt.Book + "','" + Movement_Receipt.Type + "','" + Movement_Receipt.Owner + "','" + Movement_Receipt.Kind + "','" + Movement_Receipt.Amount + "','" + Movement_Receipt.Date + "','" + Movement_Receipt.Detail + "')", Connect);
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

        // Delete Movement Receipt From Sql DataBase Using Number
        //

        public void Sql_Delete(string Movement_Receipt_Number)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Delete From Movement_Receipt_Table Where Number='" + Movement_Receipt_Number + "'", Connect);
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

        public List<Movement_Receipt> Sql_Select(string Type)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Movement_Receipt_Table", Connect);

            SqlDataReader Read;
            List<Movement_Receipt> Movement_Receipts = new List<Movement_Receipt>();

            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    if (Read["Type"].ToString() == Type)
                    {
                        Movement_Receipt Movement_Receipt = new Movement_Receipt(Read["Number"].ToString(), Read["Book"].ToString(), Read["Type"].ToString(), Read["Owner"].ToString(), Read["Kind"].ToString(), Read["Amount"].ToString(), Read["Date"].ToString(), Read["Detail"].ToString());
                        Movement_Receipts.Add(Movement_Receipt);
                    }
                }
            }
            finally
            {
                Connect.Close();
            }

            return Movement_Receipts;
        }

        #endregion

        #region FireBase Function

        // Insert Data In FireBase DataBase ...
        //

        public async void FireBase_Insert(Movement_Receipt Movement_Receipt)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            var Movement_Receipt_Data = new FireBase_Movement_Receipt
            {
                Number = Movement_Receipt.Get_Number(),
                Book = Movement_Receipt.Get_Book(),
                Type = Movement_Receipt.Get_Type(),
                Owner = Movement_Receipt.Get_Owner(),
                Kind = Movement_Receipt.Get_Kind(),
                Amount = Movement_Receipt.Get_Amount(),
                Date = Movement_Receipt.Get_Date(),
                Detail = Movement_Receipt.Get_Detail(),
            };

            SetResponse Respose = await Client.SetTaskAsync("Movement_Receipt/" + Movement_Receipt.Get_Number(), Movement_Receipt_Data);

            return;
        }

        // Delete Student From FireBase DataBase ...
        //

        public async void FireBase_Delete(string Movement_Receipt_Number)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            FirebaseResponse Respose = await Client.DeleteTaskAsync("Movement_Receipt/" + Movement_Receipt_Number);
        }

        #endregion

        #region Receipt GUI Function

        public void Set_Movement_Receipt_Info(List<TextBox> TextBox, ref Movement_Receipt Movement_Receipt)
        {
            Movement_Receipt = new Movement_Receipt(TextBox[0].Text, TextBox[1].Text, TextBox[2].Text, TextBox[3].Text, TextBox[4].Text, TextBox[5].Text,
            TextBox[6].Text, TextBox[7].Text);
        }

        #endregion
    }
}
