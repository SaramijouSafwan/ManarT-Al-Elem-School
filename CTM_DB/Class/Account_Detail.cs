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
namespace CTM_DB.Class
{
    class Account_Detail
    {
        string Account_Id;
        string Student_Name;
        string Class;
        string Study_Fee;
        string Registration_Fee;
        string Transportaion_Fee;
        string Extra_Fee;
        string Total;

        public Account_Detail() { }

        public Account_Detail(string accountId, string studentName, string _class, string registrationFee, string studyFee, string transportaionFee, string extraFee, string total)
        {
            Account_Id = accountId;
            Student_Name = studentName;
            Class = _class;
            Study_Fee = studyFee;
            Registration_Fee = registrationFee;
            Transportaion_Fee = transportaionFee;
            Extra_Fee = extraFee;
            Total = total;
        }

        #region GetFunction

        // Get Account Id
        //

        public string Get_Account_Id()
        {
            return Account_Id;
        }

        // Get Student Name
        //

        public string Get_Student_Name()
        {
            return Student_Name;
        }

        // Get Class
        //

        public string Get_Class()
        {
            return Class;
        }

        // Get Study Fee
        //

        public string Get_Study_Fee()
        {
            return Study_Fee;
        }

        // Get Registration Fee
        //

        public string Get_Registration_Fee()
        {
            return Registration_Fee;
        }

        // Get Transportaion Fee
        //

        public string Get_Transportaion_Fee()
        {
            return Transportaion_Fee;
        }

        // Get Extra Fee
        //

        public string Get_Extra_Fee()
        {
            return Extra_Fee;
        }
        
        // Get Total
        //

        public string Get_Total()
        {
            return Total;
        }

        #endregion

        #region Sql Function

        // Insert Detail Into Sql DataBase ...
        //

        public void Sql_Insert(List<Account_Detail> Detail)
        {
            int Number_Detail = Detail.Count();

            for (int i = 0; i < Number_Detail; i++)
            {
                SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
                SqlCommand Cmd = new SqlCommand("Insert into Account_Detail(Account_Id, Student_Name, Class,Study_Fee, Registration_Fee,Transportaion_Fee,Extra_Fee,Total) values('" + Detail[i].Account_Id + "','" + Detail[i].Student_Name + "','" + Detail[i].Class + "','" + Detail[i].Study_Fee + "','" + Detail[i].Registration_Fee + "','" + Detail[i].Transportaion_Fee + "','" + Detail[i].Extra_Fee + "','" + Detail[i].Total + "')", Connect);
                try
                {
                    Connect.Open();
                    Cmd.ExecuteNonQuery();
                }
                finally
                {
                    Connect.Close();
                }
            }
            return;
        }

        // Read All Student From Sql Data Base ...
        //

        public List<Account_Detail> Sql_Select(string Account_Id)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Account_Detail", Connect);

            SqlDataReader Read;
            List<Account_Detail> Details = new List<Account_Detail>();

            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    if (Account_Id.ToString() == Read["Account_Id"].ToString())
                    {
                        Account_Detail Detail = new Account_Detail(Read["Account_Id"].ToString(), Read["Student_Name"].ToString(), Read["Class"].ToString(), Read["Study_Fee"].ToString(), Read["Registration_Fee"].ToString(), Read["Transportaion_Fee"].ToString(), Read["Extra_Fee"].ToString(), Read["Total"].ToString());
                        Details.Add(Detail);
                    }
                }
            }
            finally
            {
                Connect.Close();
            }

            return Details;
        }

        // Delete Account Detail From SQL DataBase
        //

        public void Sql_Delete(string Account_Id)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Delete From Account_Detail Where Account_Id='" + Account_Id + "'", Connect);
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

        #endregion

        #region FireBase

        // Insert Data In FireBase DataBase ...
        //

        public async void FireBase_Insert(List<Account_Detail> Detail)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            int Number_Detail = Detail.Count;

            FirebaseResponse CounterRespose = await Client.GetTaskAsync("Number_Account_Detail/" + "Now");
            FireBase_Account_Detail_Counter Data = CounterRespose.ResultAs<FireBase_Account_Detail_Counter>();

            string Counter_Value = Data.Counter;

            for (int i = 0; i < Number_Detail; i++)
            {
                var Account_Detail = new FireBase_Account_Detail
                {
                    Account_Id = Detail[i].Get_Account_Id(),
                    Student_Name = Detail[i].Get_Student_Name(),
                    Class = Detail[i].Get_Class(),
                    Study_Fee = Detail[i].Get_Study_Fee(),
                    Registration_Fee = Detail[i].Get_Registration_Fee(),
                    Transportaion_Fee = Detail[i].Get_Transportaion_Fee(),
                    Extra_Fee = Detail[i].Get_Extra_Fee(),
                    Total = Detail[i].Get_Total()
                };

                SetResponse Respose = await Client.SetTaskAsync("Account_Detail/" + Counter_Value, Account_Detail);

                Counter_Value = (int.Parse(Counter_Value) + 1).ToString();
            }

            Update_Counter(Counter_Value);

            return;
        }

        // Delete Data From FireBase DataBase
        //

        public async void FireBase_Delete(string Detail_Student_Name)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            FirebaseResponse Respose = await Client.DeleteTaskAsync("Account_Detail/" + Detail_Student_Name);
        }

        #endregion

        #region Help Function

        public async void Account_Detail_Counter_Defult()
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            var Number_Account_Detail = new FireBase_Account_Detail_Counter
            {
                Counter = "1"
            };

            SetResponse Respose = await Client.SetTaskAsync("Number_Account_Detail/" + "Now", Number_Account_Detail);
        }

        public async void Update_Counter(string Counter_Value)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            var Number_Account_Detail = new FireBase_Account_Detail_Counter
            {
                Counter = Counter_Value
            };

            SetResponse Respose = await Client.SetTaskAsync("Number_Account_Detail/" + "Now", Number_Account_Detail);

            return;
        }

        #endregion
    }
}
