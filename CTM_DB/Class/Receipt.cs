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
    class Receipt
    {
        string Number;
        string Book;
        string Branch;
        string Recipient;
        string First_Name;
        string Last_Name;
        string Amount_Paid;
        string Date;
        string For;
        string Account_Number;

        // Defult Constructor
        //

        public Receipt() { }

        // Parameter Constructor
        //

        public Receipt(string receiptNumber, string receiptBook, string branch, string recipient,
            string firstName, string lastName, string amountPaid, string date, string _for, string account_Number)
        {
            Number = receiptNumber;
            Book = receiptBook;
            Date = date;
            First_Name = firstName;
            Last_Name = lastName;
            Amount_Paid = amountPaid;
            Recipient = recipient;
            For = _for;
            Branch = branch;
            Account_Number = account_Number;
        }

        #region Get Function

        // Get Receipt Number
        //
        public string Get_Number()
        {
            return Number;
        }

        // Get Receipt Book
        //
        public string Get_Book()
        {
            return Book;
        }

        // Get Date
        //
        public string Get_Date()
        {
            return Date;
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

        // Get Amount Paid
        //
        public string Get_Amount_Paid()
        {
            return Amount_Paid;
        }

        // Get Recipient
        //
        public string Get_Recipient()
        {
            return Recipient;
        }

        // Get For
        //
        public string Get_For()
        {
            return For;
        }

        // Get Branch
        //
        public string Get_Branch()
        {
            return Branch;
        }

        // Get Account Number
        //
        public string Get_Account_Number()
        {
            return Account_Number;
        }
        #endregion

        #region SQL Function

        // Insert Receipt Into Sql DataBase ...
        //

        public void Sql_Insert(Receipt Receipt)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Insert into Receipt_Table(Number, Book, Bransh, Recipient, First_Name, Last_Name, Amount_Paid, Date, _For, Account_Number) values('" + Receipt.Number + "','" + Receipt.Book + "','" + Receipt.Branch + "','" + Receipt.Recipient + "','" + Receipt.First_Name + "','" + Receipt.Last_Name + "','" + Receipt.Amount_Paid + "','" + Receipt.Date + "','" + Receipt.For + "','" + Receipt.Account_Number + "')", Connect);
            try
            {
                Connect.Open();
                Cmd.ExecuteNonQuery();
            }
            finally
            {
                Connect.Close();
            }

            if (Receipt.Get_Account_Number() != "ارشيف")
            {
                Account Account = new Account();
                Account.Update_Account(Receipt.Get_Account_Number(), Receipt.Get_Amount_Paid());
            }
            return;
        }

        // Delete Receipt From Sql DataBase Using Id
        //

        public void Sql_Delete(string Receipt_Number)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Delete From Receipt_Table Where Number='" + Receipt_Number + "'", Connect);
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

        // Read All Receipt From Sql Data Base ...
        //

        public List<Receipt> Sql_Select()
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Receipt_Table", Connect);

            SqlDataReader Read;
            List<Receipt> Receipts = new List<Receipt>();

            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    Receipt Receipt = new Receipt(Read["Number"].ToString(), Read["Book"].ToString(), Read["Bransh"].ToString(), Read["Recipient"].ToString(), Read["First_Name"].ToString(), Read["Last_Name"].ToString(), Read["Amount_Paid"].ToString(), Read["Date"].ToString(), Read["_For"].ToString(), Read["Account_Number"].ToString());
                    Receipts.Add(Receipt);
                }
            }
            finally
            {
                Connect.Close();
            }

            return Receipts;
        }

        // Read Receipt Where Account Number From Sql Data Base ...
        //

        public List<Receipt> Sql_Select(string Account_Number)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Receipt_Table", Connect);

            SqlDataReader Read;
            List<Receipt> Receipts = new List<Receipt>();

            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    if (Read["Account_Number"].ToString() == Account_Number)
                    {
                        Receipt Receipt = new Receipt(Read["Number"].ToString(), Read["Book"].ToString(), Read["Bransh"].ToString(), Read["Recipient"].ToString(), Read["First_Name"].ToString(), Read["Last_Name"].ToString(), Read["Amount_Paid"].ToString(), Read["Date"].ToString(), Read["_For"].ToString(), Read["Account_Number"].ToString());
                        Receipts.Add(Receipt);
                    }
                }
            }
            finally
            {
                Connect.Close();
            }

            return Receipts;
        }

        // Search In Receipt Table
        //

        public List<Receipt> Sql_Search(string Value)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Receipt_Table Where Number+Book+Bransh+Recipient+First_Name+Last_Name+Amount_Paid+Date+_For+Account_Number like '%" + Value + "%' ", Connect);

            SqlDataReader Read;
            List<Receipt> Receipts = new List<Receipt>();

            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    Receipt Receipt = new Receipt(Read["Number"].ToString(), Read["Book"].ToString(), Read["Bransh"].ToString(), Read["Recipient"].ToString(), Read["First_Name"].ToString(), Read["Last_Name"].ToString(), Read["Amount_Paid"].ToString(), Read["Date"].ToString(), Read["_For"].ToString(), Read["Account_Number"].ToString());
                    Receipts.Add(Receipt);
                }
            }
            finally
            {
                Connect.Close();
            }

            return Receipts;
        }

        #endregion

        #region FireBase Function

        // Insert Data In FireBase DataBase ...
        //

        public async void FireBase_Insert(Receipt Receipt)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            var Receipt_Data = new FireBase_Receipt
            {
                Number = Receipt.Get_Number(),
                Book = Receipt.Get_Book(),
                Branch = Receipt.Get_Branch(),
                Recipient = Receipt.Get_Recipient(),
                First_Name = Receipt.Get_First_Name(),
                Last_Name = Receipt.Get_Last_Name(),
                Amount_Paid = Receipt.Get_Amount_Paid(),
                Date = Receipt.Get_Date(),
                For = Receipt.Get_For(),
                Account_Number = Receipt.Get_Account_Number()
            };

            SetResponse Respose = await Client.SetTaskAsync("Receipt/" + Receipt.Get_Number(), Receipt_Data);

            return;
        }

        // Delete Student From FireBase DataBase ...
        //

        public async void FireBase_Delete(string Receipt_Number)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            FirebaseResponse Respose = await Client.DeleteTaskAsync("Receipt/" + Receipt_Number);
        }

        #endregion

        #region Receipt GUI Function

        public void Set_Receipt_Info(List<TextBox> TextBox, ref Receipt Receipt)
        {
            string Account_Id = "";

            Account Account = new Account();
            bool Cheak = Account.Get_Account_Id(TextBox[5].Text, ref Account_Id);

            if (Cheak == true)
            {
                Receipt = new Receipt(TextBox[0].Text, TextBox[1].Text, TextBox[2].Text, TextBox[3].Text, TextBox[4].Text, TextBox[5].Text,
                TextBox[6].Text, TextBox[7].Text, TextBox[8].Text, Account_Id);
            }
            else
            {
                MessageBox.Show("هذا الايصال لا ينتمي لاي حساب سيتم تسجيل هذا الايصال في الارشيف", "لا يوجد حساب",MessageBoxButtons.OK, MessageBoxIcon.Information);
                Receipt = new Receipt(TextBox[0].Text, TextBox[1].Text, TextBox[2].Text, TextBox[3].Text, TextBox[4].Text, TextBox[5].Text,
                TextBox[6].Text, TextBox[7].Text, TextBox[8].Text, "ارشيف");
            }
            
        }

        public void Get_Receipt_Info(ref List<TextBox> TextBox, Receipt Receipt)
        {
            if (Receipt.Get_Number() != "0000000000")
            {
                TextBox[0].Text = Receipt.Get_Number();
                TextBox[0].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[0].Text = "0000000000";
                TextBox[0].ForeColor = Color.Silver;
            }

            if (Receipt.Get_Book() != "A19 or B19")
            {
                TextBox[1].Text = Receipt.Get_Book();
                TextBox[1].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[1].Text = "A19 or B19";
                TextBox[1].ForeColor = Color.Silver;
            }

            if (Receipt.Get_Branch() != "اسم الفرع")
            {
                TextBox[2].Text = Receipt.Get_Branch();
                TextBox[2].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[2].Text = "اسم الفرع";
                TextBox[2].ForeColor = Color.Silver;
            }

            if (Receipt.Get_Recipient() != "اسم المستلم")
            {
                TextBox[3].Text = Receipt.Get_Recipient();
                TextBox[3].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[3].Text = "اسم المستلم";
                TextBox[3].ForeColor = Color.Silver;
            }

            if (Receipt.Get_First_Name() != "الاسم الاول")
            {
                TextBox[4].Text = Receipt.Get_First_Name();
                TextBox[4].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[4].Text = "الاسم الاول";
                TextBox[4].ForeColor = Color.Silver;
            }

            if (Receipt.Get_Last_Name() != "الاسم الاخير")
            {
                TextBox[5].Text = Receipt.Get_Last_Name();
                TextBox[5].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[5].Text = "الاسم الاخير";
                TextBox[5].ForeColor = Color.Silver;
            }

            if (Receipt.Get_Amount_Paid() != "000 L.E")
            {
                TextBox[6].Text = Receipt.Get_Amount_Paid();
                TextBox[6].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[6].Text = "000 L.E";
                TextBox[6].ForeColor = Color.Silver;
            }

            if (Receipt.Get_Date() != "01/01/1990")
            {
                TextBox[7].Text = Receipt.Get_Date();
                TextBox[7].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[7].Text = "01/01/1990";
                TextBox[7].ForeColor = Color.Silver;
            }

            if (Receipt.Get_First_Name() != "الاسم الاول")
            {
                TextBox[8].Text = Receipt.Get_For();
                TextBox[8].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[8].Text = "التفاصيل";
                TextBox[8].ForeColor = Color.Silver;
            }
        }

        #endregion

        #region Help Function 

        

        #endregion
    }
}
        