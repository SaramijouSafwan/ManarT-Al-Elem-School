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
    class Account
    {
        string Number;
        string Name;
        string Type;
        string Connect1;
        string Connect2;
        string Whats_App;
        string Total;
        string Amount_Paid;
        string Remender;
        List<Receipt> Receipts;
        List<Account_Detail> Details;

        public Account() { }

        public Account(string number, string name, string type, string connect1, string connect2, string whatsApp, string total, string amountPaid, string remender, List<Receipt> receipts, List<Account_Detail> detail)
        {
            Number = number;
            Name = name;
            Type = type;
            Connect1 = connect1;
            Connect2 = connect2;
            Whats_App = whatsApp;
            Total = total;
            Amount_Paid = amountPaid;
            Remender = remender;
            Receipts = receipts;
            Details = detail;
        }

        # region Get Function

        // Get Account Number
        //

        public string Get_Number()
        {
            return Number;
        }

        // Get Account Name 
        //

        public string Get_Name()
        {
            return Name;
        }

        // Get Account Type
        //

        public string Get_Type()
        {
            return Type;
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

        // Get WhatsApp
        //

        public string Get_Whats_App()
        {
            return Whats_App;
        }

        // Get Total
        //

        public string Get_Total()
        {
            return Total;
        }

        // Get Amount Paid
        //

        public string Get_Amount_Paid()
        {
            return Amount_Paid;
        }

        // Get Remender
        //

        public string Get_Remender()
        {
            return Remender;
        }

        // Get Receipt
        //

        public List<Receipt> Get_Receipt()
        {
            return Receipts;
        }

        // Get Detail
        //

        public List<Account_Detail> Get_Account_Detail()
        {
            return Details;
        }

#endregion

        #region SQL Function

        // Insert Account Into Account Table
        //

        public void Sql_Insert(Account Account)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Insert into Account_Table(Number, Name, Type, Connect1, Connect2, Whats_App, Total, Amount_Paid, Remender) values('" + Account.Get_Number() + "','" + Account.Get_Name() + "','" + Account.Get_Type() + "','" + Account.Get_Connect1() + "','" + Account.Get_Connect2() + "','" + Account.Get_Whats_App() + "','" + Account.Get_Total() + "','" + Account.Get_Amount_Paid() + "','" + Account.Get_Remender() + "')", Connect);
            try
            {
                Connect.Open();
                Cmd.ExecuteNonQuery();

                Account_Detail Detail = new Account_Detail();
                Detail.Sql_Insert(Account.Get_Account_Detail());

            }
            finally
            {
                Connect.Close();
            }
            return;
        }

        // Delete Account From Sql DataBase Using Id
        //

        public void Sql_Delete(string Account_Number)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Delete From Account_Table Where Number='" + Account_Number + "'", Connect);
            try
            {
                Account_Detail Detail = new Account_Detail();
                Detail.Sql_Delete(Account_Number);

                Connect.Open();
                Cmd.ExecuteNonQuery();

            }
            finally
            {
                Connect.Close();
            }
            return;
        }

        // Read All Account From Sql Data Base ...
        //

        public List<Account> Sql_Select()
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Account_Table", Connect);

            SqlDataReader Read;
            List<Account> Accounts = new List<Account>();
            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    Account_Detail Detail = new Account_Detail();
                    List<Account_Detail> Details =  Detail.Sql_Select(Read["Number"].ToString());

                    Receipt Receipt = new Receipt();
                    List<Receipt> Account_Receipt = Receipt.Sql_Select(Read["Number"].ToString());

                    Account Account = new Account(Read["Number"].ToString(), Read["Name"].ToString(), Read["Type"].ToString(), Read["Connect1"].ToString(), Read["Connect2"].ToString(), Read["Whats_App"].ToString(), Read["Total"].ToString(), Read["Amount_Paid"].ToString(), Read["Remender"].ToString(),Account_Receipt,Details);
                    Accounts.Add(Account);
                }
            }
            finally
            {
                Connect.Close();
            }

            return Accounts;
        }

        // Search For Account in Account Table
        //

        public List<Account> Sql_Search(string Value)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Account_Table Where Number+Name+Type+Connect1+Connect2+Whats_App like '%" + Value + "%' ", Connect);

            SqlDataReader Read;
            List<Account> Accounts = new List<Account>();
            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    Account_Detail Detail = new Account_Detail();
                    List<Account_Detail> Details = Detail.Sql_Select(Read["Number"].ToString());

                    Receipt Receipt = new Receipt();
                    List<Receipt> Account_Receipt = Receipt.Sql_Select(Read["Number"].ToString());

                    Account Account = new Account(Read["Number"].ToString(), Read["Name"].ToString(), Read["Type"].ToString(), Read["Connect1"].ToString(), Read["Connect2"].ToString(), Read["Whats_App"].ToString(), Read["Total"].ToString(), Read["Amount_Paid"].ToString(), Read["Remender"].ToString(), Account_Receipt, Details);
                    Accounts.Add(Account);
                }
            }
            finally
            {
                Connect.Close();
            }

            return Accounts;
        }
        #endregion

        #region FireBase Function

        // Insert Data In FireBase DataBase ...
        //

        public async void FireBase_Insert(Account Account)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            Account_Detail Detail = new Account_Detail();

            Detail.FireBase_Insert(Account.Get_Account_Detail());
            
            var Account_Data = new FireBase_Account
            {
                Number = Account.Get_Number(),
                Name = Account.Get_Name(),
                Type = Account.Get_Type(),
                Connect1 = Account.Get_Connect1(),
                Connect2 = Account.Get_Connect2(),
                Whats_App = Account.Get_Whats_App(),
                Total = Account.Get_Total(),
                Amount_Paid = Account.Get_Amount_Paid(),
                Remender = Account.Get_Remender()
            };

            SetResponse Account_Respose = await Client.SetTaskAsync("Account/" + Account.Get_Number(), Account_Data);

            return;
        }

        // Delete Student From FireBase DataBase ...
        //

        public async void FireBase_Delete(Account Account)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            int Number_Detail = Account.Get_Account_Detail().Count();

            Account_Detail Detail = new Account_Detail();

            for(int i = 0; i < Number_Detail; i++)
            {
                Detail.FireBase_Delete(Account.Get_Account_Detail().ElementAt(i).Get_Student_Name());
            }
            FirebaseResponse Respose = await Client.DeleteTaskAsync("Account/" + Account.Get_Number());
        }

        #endregion

        #region Account GUI Function

        public void Set_Account_Info(List<TextBox> TextBox, DataGridView Detail_Table, ref Account Account) 
        {
            List<Account_Detail> Details = new List<Account_Detail>();
            List<Receipt> Receipts = new List<Receipt>();

            int Number_Row = Detail_Table.Rows.Count;

            for (int i = 0; i < Number_Row -1; i++)
            {
                Account_Detail Detail = new Account_Detail(TextBox[0].Text, Detail_Table.Rows[i].Cells[6].Value.ToString(), Detail_Table.Rows[i].Cells[5].Value.ToString(), Detail_Table.Rows[i].Cells[4].Value.ToString(), Detail_Table.Rows[i].Cells[3].Value.ToString(), Detail_Table.Rows[i].Cells[2].Value.ToString(), Detail_Table.Rows[i].Cells[1].Value.ToString(), Detail_Table.Rows[i].Cells[0].Value.ToString());
                Details.Add(Detail);
            }

            Account = new Account(TextBox[0].Text, TextBox[1].Text, TextBox[2].Text, TextBox[3].Text, TextBox[4].Text, TextBox[5].Text, TextBox[6].Text, TextBox[7].Text, TextBox[8].Text,Receipts,Details);
        }

        public void Get_Account_Info(ref List<TextBox> TextBox, ref DataGridView Detail_Table, ref DataGridView Receipt_Table, Account Account) 
        {
            if (Account.Get_Number() != "0000000000")
            {
                TextBox[0].Text = Account.Get_Number();
                TextBox[0].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[0].Text = "0000000000";
                TextBox[0].ForeColor = Color.Silver;
            }

            if(Account.Get_Name() != "اسم الحساب")
            {
                TextBox[1].Text = Account.Get_Name();
                TextBox[1].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[1].Text = "اسم الحساب";
                TextBox[1].ForeColor = Color.Silver;
            }

            if (Account.Get_Type() != "فردي / مشترك")
            {
                TextBox[2].Text = Account.Get_Type();
                TextBox[2].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[2].Text = "فردي / مشترك";
                TextBox[2].ForeColor = Color.Silver;
            }

            if (Account.Get_Connect1() != "022 - 01")
            {
                TextBox[3].Text = Account.Get_Connect1();
                TextBox[3].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[3].Text = "022 - 01";
                TextBox[3].ForeColor = Color.Silver;
            }

            if (Account.Get_Connect2() != "022 - 01")
            {
                TextBox[4].Text = Account.Get_Connect2();
                TextBox[4].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[4].Text = "022 - 01";
                TextBox[4].ForeColor = Color.Silver;
            }

            if (Account.Get_Whats_App() != "022 - 01")
            {
                TextBox[5].Text = Account.Get_Whats_App();
                TextBox[5].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[5].Text = "022 - 01";
                TextBox[5].ForeColor = Color.Silver;
            }

            if (Account.Get_Total() != "000 ج.م")
            {
                TextBox[6].Text = Account.Get_Total();
                TextBox[6].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[6].Text = "000 ج.م";
                TextBox[6].ForeColor = Color.Silver;
            }

            if (Account.Get_Amount_Paid() != "000 ج.م")
            {
                TextBox[7].Text = Account.Get_Amount_Paid();
                TextBox[7].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[7].Text = "000 ج.م";
                TextBox[7].ForeColor = Color.Silver;
            }

            if (Account.Get_Remender() != "000 ج.م")
            {
                TextBox[8].Text = Account.Get_Remender();
                TextBox[8].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[8].Text = "000 ج.م";
                TextBox[8].ForeColor = Color.Silver;
            }

            int Number_Detail = Account.Get_Account_Detail().Count();
            int Number_Receipt = Account.Get_Receipt().Count();

            Detail_Table.Rows.Clear();

            for (int i = 0; i < Number_Detail; i++)
            {
                string[] Row = new string[] { Account.Get_Account_Detail().ElementAt(i).Get_Total(), Account.Get_Account_Detail().ElementAt(i).Get_Extra_Fee(), Account.Get_Account_Detail().ElementAt(i).Get_Transportaion_Fee(), Account.Get_Account_Detail().ElementAt(i).Get_Study_Fee(), Account.Get_Account_Detail().ElementAt(i).Get_Registration_Fee(), Account.Get_Account_Detail().ElementAt(i).Get_Class(), Account.Get_Account_Detail().ElementAt(i).Get_Student_Name() };
                Detail_Table.Rows.Add(Row);
            }

            Receipt_Table.Rows.Clear();

            for (int i = 0; i < Number_Receipt; i++)
            {
                string[] Row = new string[] { Account.Get_Receipt().ElementAt(i).Get_Book(), Account.Get_Receipt().ElementAt(i).Get_Branch(), Account.Get_Receipt().ElementAt(i).Get_Recipient(), Account.Get_Receipt().ElementAt(i).Get_For(), Account.Get_Receipt().ElementAt(i).Get_Date(), Account.Get_Receipt().ElementAt(i).Get_Amount_Paid(), Account.Get_Receipt().ElementAt(i).Get_Number() };
                Receipt_Table.Rows.Add(Row);
            }
        
        }

        #endregion

        #region Help Function

        public bool Get_Account_Id(string Last_Name, ref string Account_Id)
        {
            Account Account = new Account();
            List<Account> Accounts = Account.Sql_Select();

            int Number_Account = Accounts.Count;

            for (int i = 0; i < Number_Account; i++)
            {
                if (Accounts[i].Get_Name().ToString() == Last_Name)
                {
                    Account_Id = Accounts[i].Get_Number();
                    return true;
                }
            }

            return false;
        }

        public string New_Id()
        {
            Account Account = new Account();
            List<Account> Accounts = Account.Sql_Select();

            int Number_Account = Accounts.Count;

            if (Number_Account == 0)
            {
                string Id = "181900001";
                return Id;
            }
            else
            {
                string Id = (int.Parse(Accounts[Number_Account - 1].Get_Number()) + 1).ToString();
                return Id;
            }
        }

        public void Get_Amount_Remender(string Account_Id, ref string amount, ref string remender, ref string total, ref Account Account)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Account_Table Where Number='" + Account_Id + "'", Connect);

            SqlDataReader Read;
            
            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    amount = Read["Amount_Paid"].ToString();
                    remender =  Read["Remender"].ToString();
                    total = Read["Total"].ToString();

                    List<Receipt> Receipts = new List<Receipt>();
                    List<Account_Detail> Details = new List<Account_Detail>();

                    Account = new Account(Read["Number"].ToString(), Read["Name"].ToString(), Read["Type"].ToString(), Read["Connect1"].ToString(), Read["Connect2"].ToString(), Read["Whats_App"].ToString(), Read["Total"].ToString(), Read["Amount_Paid"].ToString(), Read["Remender"].ToString(), Receipts, Details);

                }
            }
            finally
            {
                Connect.Close();
            }
        }

        public async void Update_Account(string Account_Id, string Receipt_Value)
        {
            Account Account = new Account();

            string Amount = "";
            string Remender = "";
            string Total = "";

            Get_Amount_Remender(Account_Id, ref Amount, ref Remender, ref Total, ref Account);
            
            Amount = (int.Parse(Amount) + int.Parse(Receipt_Value)).ToString();
            Remender = (int.Parse(Total) - int.Parse(Amount)).ToString();

            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Update Account_Table Set Amount_Paid='"+Amount+"', Remender='"+Remender+"' Where Number='" + Account_Id + "'", Connect);
            try
            {
                Connect.Open();
                Cmd.ExecuteNonQuery();
            }
            finally
            {
                Connect.Close();
            }

            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            var Account_Data = new FireBase_Account
            {
                Number = Account.Get_Number(),
                Name = Account.Get_Name(),
                Type = Account.Get_Type(),
                Connect1 = Account.Get_Connect1(),
                Connect2 = Account.Get_Connect2(),
                Whats_App = Account.Get_Whats_App(),
                Total = Account.Get_Total(),
                Amount_Paid = Amount,
                Remender = Remender
            };

            FirebaseResponse Respose = await Client.UpdateTaskAsync("Account/" + Account_Id, Account_Data);

            return;
        }
        #endregion
    }
}
