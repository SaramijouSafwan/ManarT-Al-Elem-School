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
    class Process
    {
        string Number;
        string Code;
        string Record_Id;
        string Owner;

        public Process() { }

        public Process(string number, string code, string record_id, string owner)
        {
            Number = number;
            Code = code;
            Record_Id = record_id;
            Owner = owner;
        }

        #region Get Function

        public string Get_Number()
        {
            return Number;
        }

        public string Get_Process_Code()
        {
            return Code;
        }

        public string Get_Record_Id()
        {
            return Record_Id;
        }

        public string Get_Owner()
        {
            return Owner;
        }

        #endregion

        public async void Counter_Defult()
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            var Number_Process_Data = new FireBase_Counter
            {
                Counter = "1"
            };

            SetResponse Respose = await Client.SetTaskAsync("Number_Process/" + "Now", Number_Process_Data);
        }

        public string Get_Process_Code(string Type, string Department)
        {
            string Process_Code = "";

            if (Department == "Student")
            {
                if (Type == "Save")
                {
                    Process_Code = "S1";
                }
                else if (Type == "Delete")
                {
                    Process_Code = "S2";
                }
                else if (Type == "Update")
                {
                    Process_Code = "S3";
                }
            }
            else if (Department == "Receipt")
            {
                if (Type == "Save")
                {
                    Process_Code = "R1";
                }
                else if (Type == "Delete")
                {
                    Process_Code = "R2";
                }
                else if (Type == "Update")
                {
                    Process_Code = "R3";
                }
            }
            else if (Department == "Account")
            {
                if (Type == "Save")
                {
                    Process_Code = "A1";
                }
                else if (Type == "Delete")
                {
                    Process_Code = "A2";
                }
                else if (Type == "Update")
                {
                    Process_Code = "A3";
                }
            }
            else if (Department == "Employee")
            {
                if (Type == "Save")
                {
                    Process_Code = "E1";
                }
                else if (Type == "Delete")
                {
                    Process_Code = "E2";
                }
                else if (Type == "Update")
                {
                    Process_Code = "E3";
                }
            }
            else if (Department == "Salary")
            {
                if (Type == "Save")
                {
                    Process_Code = "SA1";
                }
                else if (Type == "Delete")
                {
                    Process_Code = "SA2";
                }
                else if (Type == "Update")
                {
                    Process_Code = "SA3";
                }
            }
            else if (Department == "Movement")
            {
                if (Type == "Save")
                {
                    Process_Code = "MR1";
                }
                else if (Type == "Delete")
                {
                    Process_Code = "MR2";
                }
                else if (Type == "Update")
                {
                    Process_Code = "MR3";
                }
            }
            
            return Process_Code;
        }

        public async void Update_Counter(string Value)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            var Number_Process_Data = new FireBase_Counter
            {
                Counter = Value
            };

            SetResponse Respose = await Client.SetTaskAsync("Number_Process/" + "Now", Number_Process_Data);

            Update_Process_Number((int.Parse(Value) - 1).ToString());
        }

        public async void Save_Process(string Type, string Department, string Id, string _Owner)
        {
            
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            FirebaseResponse Respose = await Client.GetTaskAsync("Number_Process/" + "Now");
            FireBase_Counter Data = Respose.ResultAs<FireBase_Counter>();

            string Counter = Data.Counter;

            var Process_Data = new FireBase_Process
            {
                Number = Counter,
                Code = Get_Process_Code(Type, Department),
                Record_Id = Id,
                Owner = _Owner
            };

            SetResponse Process_Respose = await Client.SetTaskAsync("Process/" + Counter, Process_Data);

            Update_Counter((int.Parse(Counter) + 1).ToString());

            return;
        }

        public async void Excute_Process(Process Process)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            if (Process.Code == "S1")
            {
                FirebaseResponse Respose = await Client.GetTaskAsync("Student/" + Process.Record_Id);
                FireBase_Student Data = Respose.ResultAs<FireBase_Student>();

                Student Student = new Student(Data.Id, Data.First_Name, Data.Last_Name, Data.Father_Name, Data.Birth, Data.Type, Data.Connect1, Data.Connect2,
                                              Data.Class, Data.Registration, Data.School_Name, Data.Transportation, Data.City, Data.Block,
                                              Data.Street, Data.Detail, Data.Account_Number, Data.File_Number, Data.Note, Data.File_Stutus, Data.Picture);
                Student.Sql_Insert(Student);
                return;
            }
            else if (Process.Code == "S2")
            {
                Student Student = new Student();
                Student.Sql_Delete(Process.Record_Id);
                return;
            }
            else if (Process.Code == "S3")
            {
                return;
            }
            else if (Process.Code == "R1")
            {
                FirebaseResponse Respose = await Client.GetTaskAsync("Receipt/" + Process.Record_Id);
                FireBase_Receipt  Data = Respose.ResultAs<FireBase_Receipt>();

                Receipt Receipt = new Receipt(Data.Number, Data.Book, Data.Branch, Data.Recipient,
                                              Data.First_Name, Data.Last_Name, Data.Amount_Paid, Data.Date, Data.For, Data.Account_Number);
                Receipt.Sql_Insert(Receipt);
                return;
            }
            else if (Process.Code == "R2")
            {
                Receipt Receipt = new Receipt();
                Receipt.Sql_Delete(Process.Record_Id);
            }
            else if (Process.Code == "R3")
            {
                return;
            }
            else if (Process.Code == "A1")
            {
                FirebaseResponse Respose = await Client.GetTaskAsync("Account/" + Process.Record_Id);
                FireBase_Account Data = Respose.ResultAs<FireBase_Account>();

                Respose = await Client.GetTaskAsync("Number_Account_Detail/" + "Now");
                FireBase_Account_Detail_Counter Counter_Data = Respose.ResultAs<FireBase_Account_Detail_Counter>();

                List<Receipt> Receipts = new List<Receipt>();

                int Number_Detail = int.Parse(Counter_Data.Counter);

                List<Account_Detail> Detail = new List<Account_Detail>();

                for (int i = 1; i < Number_Detail; i++)
                {
                    Respose = await Client.GetTaskAsync("Account_Detail/" + i.ToString());
                    FireBase_Account_Detail Detail_Data = Respose.ResultAs<FireBase_Account_Detail>();

                    if (Detail_Data.Account_Id == Process.Record_Id)
                    {
                        Account_Detail Account_Detail = new Account_Detail(Detail_Data.Account_Id, Detail_Data.Student_Name, Detail_Data.Class, Detail_Data.Registration_Fee, Detail_Data.Study_Fee, Detail_Data.Transportaion_Fee, Detail_Data.Extra_Fee, Detail_Data.Total);
                        Detail.Add(Account_Detail);
                    }
                }
                

                Account Account = new Account(Data.Number, Data.Name, Data.Type, Data.Connect1, Data.Connect2, Data.Whats_App, Data.Total, Data.Amount_Paid, Data.Remender, Receipts, Detail);

                Account.Sql_Insert(Account);

                return;
            }
            else if (Process.Code == "A2")
            {
                Account Account = new Account();
                Account.Sql_Delete(Process.Record_Id);

                Account_Detail Account_Detail = new Account_Detail();
                Account_Detail.Sql_Delete(Process.Record_Id);

                return;
            }
            else if (Process.Code == "A3")
            {
                return;
            }
            else if (Process.Code == "E1")
            {
                FirebaseResponse Respose = await Client.GetTaskAsync("Employee/" + Process.Record_Id);
                FireBase_Employee Data = Respose.ResultAs<FireBase_Employee>();

                List<Employee_Receipt> Employee_Receipt = new List<Employee_Receipt>();

                Employee Employee = new Employee(Data.Number, Data.Name, Data.Connect1, Data.Connect2, Data.Children, Data.Type, Data.Class, Data.Number_Hour, Data.Detail, Employee_Receipt);
                Employee.Sql_Insert(Employee);
                return;

            }
            else if (Process.Code == "E2")
            {
                Employee Employee = new Employee();
                Employee.Sql_Delete(Process.Record_Id);
                return;
            }
            else if (Process.Code == "E3")
            {
                return;
            }
            else if (Process.Code == "SA1")
            {
                FirebaseResponse Respose = await Client.GetTaskAsync("Employee_Receipt/" + Process.Record_Id);
                FireBase_Employee_Receipt Data = Respose.ResultAs<FireBase_Employee_Receipt>();

                Employee_Receipt Employee_Receipt = new Employee_Receipt(Data.Number, Data.Book, Data.Owner, Data.Date,
                                                                         Data.Name, Data.Sallary, Data.Discount, Data.Total, Data.Detail, Data.Employee_Number);
                Employee_Receipt.Sql_Insert(Employee_Receipt);
                return;
            }
            else if (Process.Code == "SA2")
            {
                return;
            }
            else if (Process.Code == "SA3")
            {
                return;
            }
            else if (Process.Code == "MR1")
            {
                FirebaseResponse Respose = await Client.GetTaskAsync("Movement_Receipt/" + Process.Record_Id);
                FireBase_Movement_Receipt Data = Respose.ResultAs<FireBase_Movement_Receipt>();

                Movement_Receipt Movement_Receipt = new Movement_Receipt(Data.Number, Data.Book, Data.Type, Data.Owner, Data.Kind, Data.Amount, Data.Date, Data.Detail);

                Movement_Receipt.Sql_Insert(Movement_Receipt);
                return;
            }
            else if (Process.Code == "MR2")
            {
                return;
            }
            else if (Process.Code == "MR3")
            {
                return;
            }

        }

        public bool If_Process_Exist(List<Process> NonExcuted, Process Process)
        {
            int Number_Process = NonExcuted.Count;

            for (int i = 0; i < Number_Process; i++)
            {
                if (NonExcuted[i] == Process)
                {
                    return true;
                }
            }

            return false;

        }

        public bool Cheak_If_Save(Process Process)
        {
            if (Process.Code == "S1" || Process.Code == "R1" || Process.Code == "A1" || Process.Code == "E1" || Process.Code == "SA1" || Process.Code == "MR1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Cheak_If_Delete(Process Process)
        {
            if (Process.Code == "S2" || Process.Code == "R2" || Process.Code == "A2" || Process.Code == "E2" || Process.Code == "SA2" || Process.Code == "MR2")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void Download_Update(string Currnt_Opreation)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            FirebaseResponse Respose = await Client.GetTaskAsync("Number_Process/" + "Now");
            FireBase_Counter Data = Respose.ResultAs<FireBase_Counter>();

            int Counter = int.Parse(Data.Counter);

            List<Process> All_Process = new List<Process>();

            int Start;

            if (int.Parse(Currnt_Opreation) == 0)
                Start = 1;
            else
                Start = int.Parse(Currnt_Opreation) + 1;

            for (int i = Start; i < Counter; i++)
            {
                FirebaseResponse Process_Respose = await Client.GetTaskAsync("Process/" + i);
                FireBase_Process Process_Data = Process_Respose.ResultAs<FireBase_Process>();

                Process Process = new Process(Process_Data.Number, Process_Data.Code, Process_Data.Record_Id, Process_Data.Owner);
                All_Process.Add(Process);
            }

            List<Process> NonExcuted_Process = new List<Process>();

            int Number_Process = All_Process.Count;

            for (int i = 0; i < Number_Process; i++)
            {
                bool Cheak_Save = Cheak_If_Save(All_Process[i]);

                bool Cheak_Delete = Cheak_If_Delete(All_Process[i]);

                if (Cheak_Save == true)
                {
                    bool Excut_this = true;

                    for (int y = i + 1; y < Number_Process; y++)
                    {
                        Cheak_Delete = Cheak_If_Delete(All_Process[y]);

                        if (All_Process[i].Record_Id == All_Process[y].Record_Id && Cheak_Delete == true)
                        {
                            Excut_this = false;
                            NonExcuted_Process.Add(All_Process[y]);
                            break;
                        }
                    }
                    if (Excut_this == true)
                    {
                        Excute_Process(All_Process[i]);
                    }
                }
                else if(Cheak_Delete == true)
                {
                    bool Process_Exist = If_Process_Exist(NonExcuted_Process, All_Process[i]);

                    if (Process_Exist == false)
                    {
                        Excute_Process(All_Process[i]);
                    }
                }
                else
                {
                    Excute_Process(All_Process[i]);
                }
                
            }
            return;
        }

        public string Get_Process_Number()
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Opreation", Connect);

            SqlDataReader Read;
            string Opreation_Number = "";

            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    Opreation_Number = Read["Now"].ToString();
                }
            }
            finally
            {
                Connect.Close();
            }

            return Opreation_Number;
        }

        public void Update_Process_Number(string Now)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Update Opreation Set Now='" + Now + "'", Connect);
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

        public async void Save_Update()
        {
            Process Process = new Process();
            string Opreation_Number = Process.Get_Process_Number();
            Process.Download_Update(Opreation_Number);

            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            FirebaseResponse Respose = await Client.GetTaskAsync("Number_Process/" + "Now");
            FireBase_Counter Data = Respose.ResultAs<FireBase_Counter>();

            int Counter = int.Parse(Data.Counter);

            Counter--;

            Process.Update_Process_Number(Counter.ToString());
        }
    }
}