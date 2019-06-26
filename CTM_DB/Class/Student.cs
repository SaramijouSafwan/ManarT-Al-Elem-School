using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using CTM_DB.Class;
using System.Drawing;
namespace CTM_DB
{
    class Student
    {
        string Id;
        string First_Name;
        string Last_Name;
        string Father_Name;
        string Connect1;
        string Connect2;
        string Birth;
        string Class;
        string Type;
        string Registration;
        string School_Name;
        string Transportation;
        string City;
        string Block;
        string Street;
        string Detail;
        string Account_Number;
        string File_Number;
        string Note;
        string File_Stutus;
        string Picture;
        

        // Defult Constructor
        //

        public Student() { }

        // Parameter Constructor
        //

        public Student(string id, string firstName, string lastName, string fatherName, string birth, string type, string connect1, string connect2, 
             string _class,  string registration, string schoolName, string transportion, string city, string block,
            string street, string detail, string account_number, string file_number, string note, string file_stutus, string pic)
        {
            Id = id;
            First_Name = firstName;
            Last_Name = lastName;
            Father_Name = fatherName;
            Connect1 = connect1;
            Connect2 = connect2;
            Birth = birth;
            Class = _class;
            Type = type;
            Registration = registration;
            School_Name = schoolName;
            Transportation = transportion;
            City = city;
            Block = block;
            Street = street;
            Detail = detail;
            Account_Number = account_number;
            File_Number = file_number;
            Note = note;
            File_Stutus = file_stutus;
            Picture = pic;
        }

        #region Get Function

        // Get Id
        //
        public string Get_Id()
        {
            return Id;
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

        // Get Father Name
        //
        public string Get_Father_Name()
        {
            return Father_Name;
        }

        // Get Phone
        //
        public string Get_Connect1()
        {
            return Connect1;
        }

        // Get Father Phone
        //
        public string Get_Connect2()
        {
            return Connect2;
        }

        // Get Birth
        //
        public string Get_Birth()
        {
            return Birth;
        }

        // Get Class
        //
        public string Get_Class()
        {
            return Class;
        }

        // Get Type
        //
        public string Get_Type()
        {
            return Type;
        }

        // Get Registration Type
        //
        public string Get_Registration()
        {
            return Registration;
        }

        // Get School Name
        //
        public string Get_School_Name()
        {
            return School_Name;
        }

        // Get Transportation
        //
        public string Get_Transportation()
        {
            return Transportation;
        }

        // Get City
        //
        public string Get_City()
        {
            return City;
        }

        // Get Block
        //
        public string Get_Block()
        {
            return Block;
        }

        // Get Street
        //
        public string Get_Street()
        {
            return Street;
        }

        // Get Detail
        //
        public string Get_Detail()
        {
            return Detail;
        }

        // Get Account_Number
        //
        public string Get_Account_Number()
        {
            return Account_Number;
        }

        // Get File_Number
        //
        public string Get_File_Number()
        {
            return File_Number;
        }

        // Get Note
        //
        public string Get_Note()
        {
            return Note;
        }

        // Get File Stutus
        //
        public string Get_File_Stutus()
        {
            return File_Stutus;
        }

        // Get Picture
        //
        public string Get_Picture()
        {
            return Picture;
        }
        #endregion

        #region SQL Function

        // Insert Student Into Sql DataBase ...
        //

        public void Sql_Insert(Student Student)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Insert into Student_Table(Id, First_Name, Last_Name,Father_Name,Date,Type,Connect1,Connect2,Class,Registration,School_Name,Transportation,City,Block,Street,Detail,Account_Number,File_Number,Note,File_Stutus,Pic) values('"+Student.Id+"','"+Student.First_Name+"','"+Student.Last_Name+"','"+Student.Father_Name+"','"+Student.Birth+"','"+Student.Type+"','"+Student.Connect1+"','"+Student.Connect2+"','"+Student.Class+"','"+Student.Registration+"','"+Student.School_Name+"','"+Student.Transportation+"','"+Student.City+"','"+Student.Block+"','"+Student.Street+"','"+Student.Detail+"','"+Student.Account_Number+"','"+Student.File_Number+"','"+Student.Note+"','"+Student.File_Stutus+"','"+Student.Picture+"')", Connect);
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

        // Read All Student From Sql Data Base ...
        //

        public List<Student> Sql_Select()
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Student_Table",Connect);

            SqlDataReader Read;
            List<Student> Students = new List<Student>();

            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                   Student Student = new Student(Read["Id"].ToString(), Read["First_Name"].ToString(), Read["Last_Name"].ToString(), Read["Father_Name"].ToString(), Read["Date"].ToString(), Read["Type"].ToString(), Read["Connect1"].ToString(), Read["Connect2"].ToString(), Read["Class"].ToString(), Read["Registration"].ToString(), Read["School_Name"].ToString(), Read["Transportation"].ToString(), Read["City"].ToString(), Read["Block"].ToString(), Read["Street"].ToString(), Read["Detail"].ToString(), Read["Account_Number"].ToString(), Read["File_Number"].ToString(), Read["Note"].ToString(), Read["File_Stutus"].ToString(), Read["Pic"].ToString());
                   Students.Add(Student);
                }
            }
            finally
            {
                Connect.Close();
            }

            return Students;
        }

        // Delete Student From Sql DataBase Using Id
        //

        public void Sql_Delete(string Student_Id)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Delete From Student_Table Where Id='"+Student_Id+"'", Connect);
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

        // Update Student Info in Sql DataBase..
        //

        public void Sql_Update(string Student_Id, Student Student)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Update Student_Table Set Id='"+Student.Id+"', First_Name='"+Student.First_Name+"', Last_Name='"+Student.Last_Name+"', Father_Name='"+Student.Father_Name+"', Date='"+Student.Birth+"', Type='"+Student.Type+"', Connect1='"+Student.Connect1+"', Connect2='"+Student.Connect2+"', Class='"+Student.Class+"', Registration='"+Student.Registration+"', School_Name='"+Student.School_Name+"', Transportation='"+Student.Transportation+"', City='"+Student.City+"', Block='"+Student.Block+"', Street='"+Student.Street+"', Detail='"+Detail+"', Account_Number='"+Student.Account_Number+"', File_Number='"+File_Number+"', Note='"+Note+"', File_Stutus='"+File_Stutus+"', Pic='"+Student.Picture+"' Where Id='"+Student_Id+"'", Connect);
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

        // Search In Student Table
        //

        public List<Student> Sql_Search(string Value)
        {
            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlCommand Cmd = new SqlCommand("Select * From Student_Table Where First_Name+Last_Name+Father_Name+Type+Class+Registration+School_Name+Transportation+City+Block+Street+Detail+File_Stutus like '%" + Value + "%' ", Connect);

            SqlDataReader Read;
            List<Student> Students = new List<Student>();

            try
            {
                Connect.Open();
                Read = Cmd.ExecuteReader();

                while (Read.Read())
                {
                    Student Student = new Student(Read["Id"].ToString(), Read["First_Name"].ToString(), Read["Last_Name"].ToString(), Read["Father_Name"].ToString(), Read["Date"].ToString(), Read["Type"].ToString(), Read["Connect1"].ToString(), Read["Connect2"].ToString(), Read["Class"].ToString(), Read["Registration"].ToString(), Read["School_Name"].ToString(), Read["Transportation"].ToString(), Read["City"].ToString(), Read["Block"].ToString(), Read["Street"].ToString(), Read["Detail"].ToString(), Read["Account_Number"].ToString(), Read["File_Number"].ToString(), Read["Note"].ToString(), Read["File_Stutus"].ToString(), Read["Pic"].ToString());
                    Students.Add(Student);
                }
            }
            finally
            {
                Connect.Close();
            }

            return Students;
        }
        #endregion

        #region FireBase Function

        // Insert Data In FireBase DataBase ...
        //

        public async void FireBase_Insert(Student Student)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            var Student_Data = new FireBase_Student
            {
                Id = Student.Get_Id(),
                First_Name = Student.Get_First_Name(),
                Last_Name = Student.Get_Last_Name(),
                Father_Name = Student.Get_Father_Name(),
                Connect1 = Student.Get_Connect1(),
                Connect2 = Student.Get_Connect2(),
                Birth = Student.Get_Birth(),
                Class = Student.Get_Class(),
                Type = Student.Get_Type(),
                Registration = Student.Get_Registration(),
                School_Name = Student.Get_School_Name(),
                Transportation = Student.Get_Transportation(),
                City = Student.Get_City(),
                Block = Student.Get_Block(),
                Street = Student.Get_Street(),
                Detail = Student.Get_Detail(),
                Account_Number = Student.Get_Account_Number(),
                File_Number = Student.Get_File_Number(),
                Note = Student.Get_Note(),
                File_Stutus = Student.Get_File_Stutus(),
                Picture = Student.Get_Picture()
            };

            SetResponse Respose = await Client.SetTaskAsync("Student/" + Student.Get_Id(), Student_Data);

            return;
        }

        // Delete Student From FireBase DataBase ...
        //

        public async void FireBase_Delete(string Student_Id)
        {
            IFirebaseConfig Config = new FirebaseConfig
            {
                AuthSecret = "VH1rdiDgj2hzD0WN5JZ67BSXTN7OdOeM0fnry50G",
                BasePath = "https://ctm-db.firebaseio.com/"
            };

            IFirebaseClient Client = new FireSharp.FirebaseClient(Config);

            if (Client == null)
            {
                MessageBox.Show("تأكد من انك متصل بالانترنت FireBase يوجد خطأ في الاتصال بقاعدة البيانات من نوع", "خطأ في الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FirebaseResponse Respose = await Client.DeleteTaskAsync("Student/" + Student_Id);
        }

        #endregion

        #region Student GUI Function

        public void Set_Student_Info(ref List<TextBox> TextBox, ref Student Student)
        {
            Student = new Student(TextBox[0].Text, TextBox[1].Text, TextBox[2].Text, TextBox[3].Text, TextBox[4].Text, TextBox[5].Text,
                TextBox[6].Text, TextBox[7].Text, TextBox[8].Text, TextBox[9].Text, TextBox[10].Text, TextBox[11].Text, TextBox[12].Text,
                TextBox[13].Text, TextBox[14].Text, TextBox[15].Text, TextBox[18].Text, TextBox[17].Text, TextBox[19].Text, TextBox[16].Text, TextBox[20].Text); 
        }

        public void Get_Student_Info(Student Student, ref List<TextBox> TextBox)
        {
            if (Student.Id.ToString() != "000000000")
            {
                TextBox[0].Text = Student.Id;
                TextBox[0].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[0].Text = "000000000";
                TextBox[0].ForeColor = Color.Silver;
            }

            if (Student.First_Name.ToString() != "الاسم الاول")
            {
                TextBox[1].Text = Student.First_Name;
                TextBox[1].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[1].Text = "الاسم الاول";
                TextBox[1].ForeColor = Color.Silver;
            }
            if (Student.Last_Name.ToString() != "الاسم الاخير")
            {
                TextBox[2].Text = Student.Last_Name;
                TextBox[2].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[2].Text = "الاسم الاخير";
                TextBox[2].ForeColor = Color.Silver;
            }

            if (Student.Father_Name.ToString() != "اسم الاب")
            {
                TextBox[3].Text = Student.Father_Name;
                TextBox[3].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[3].Text = "اسم الاب";
                TextBox[3].ForeColor = Color.Silver;
            }

            if (Student.Connect1.ToString() != "022-0")
            {
                TextBox[6].Text = Student.Connect1;
                TextBox[6].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[6].Text = "022-0";
                TextBox[6].ForeColor = Color.Silver;
            }

            if (Student.Connect2.ToString() != "022-0")
            {
                TextBox[7].Text = Student.Connect2;
                TextBox[7].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[7].Text = "022-0";
                TextBox[7].ForeColor = Color.Silver;
            }

            if (Student.Birth.ToString() != "01/01/1990")
            {
                TextBox[4].Text = Student.Birth;
                TextBox[4].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[4].Text = "01/01/1990";
                TextBox[4].ForeColor = Color.Silver;
            }

            if (Student.Type.ToString() != "النوع")
            {
                TextBox[5].Text = Student.Type;
                TextBox[5].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[5].Text = "النوع";
                TextBox[5].ForeColor = Color.Silver;
            }

            if (Student.Class.ToString() != "المرحلة الدراسية")
            {
                TextBox[8].Text = Student.Class;
                TextBox[8].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[8].Text = "المرحلة الدراسية";
                TextBox[8].ForeColor = Color.Silver;
            }

            if (Student.Registration.ToString() != "نوع التسجيل")
            {
                TextBox[9].Text = Student.Registration;
                TextBox[9].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[9].Text = "نوع التسجيل";
                TextBox[9].ForeColor = Color.Silver;
            }

            if (Student.School_Name.ToString() != "اسم المدرسة")
            {
                TextBox[10].Text = Student.School_Name;
                TextBox[10].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[10].Text = "اسم المدرسة";
                TextBox[10].ForeColor = Color.Silver;
            }

            if (Student.Transportation.ToString() != "المواصلات")
            {
                TextBox[11].Text = Student.Transportation;
                TextBox[11].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[11].Text = "المواصلات";
                TextBox[11].ForeColor = Color.Silver;
            }

            if (Student.City.ToString() != "المدينة")
            {
                TextBox[12].Text = Student.City;
                TextBox[12].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[12].Text = "المدينة";
                TextBox[12].ForeColor = Color.Silver;
            }

            if (Student.Block.ToString() != "الحي")
            {
                TextBox[13].Text = Student.Block;
                TextBox[13].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[13].Text = "الحي";
                TextBox[13].ForeColor = Color.Silver;
            }

            if (Student.Street.ToString() != "الشارع")
            {
                TextBox[14].Text = Student.Street;
                TextBox[14].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[14].Text = "الشارع";
                TextBox[14].ForeColor = Color.Silver;
            }

            if (Student.Detail.ToString() != "التفاصيل")
            {
                TextBox[15].Text = Student.Detail;
                TextBox[15].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[15].Text = "التفاصيل";
                TextBox[15].ForeColor = Color.Silver;
            }

            if (Student.Account_Number.ToString() != "201819001")
            {
                TextBox[18].Text = Student.Account_Number;
                TextBox[18].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[18].Text = "201819001";
                TextBox[18].ForeColor = Color.Silver;
            }
            if (Student.File_Number.ToString() != "201819001")
            {
                TextBox[17].Text = Student.File_Number;
                TextBox[17].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[17].Text = "201819001";
                TextBox[17].ForeColor = Color.Silver;
            }

            if (Student.Note.ToString() != "اكتب المزبد من الملاحظات")
            {
                TextBox[19].Text = Student.Note;
                TextBox[19].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[19].Text = "اكتب المزبد من الملاحظات";
                TextBox[19].ForeColor = Color.Silver;
            }

            if (Student.File_Stutus.ToString() != "")
            {
                TextBox[16].Text = Student.File_Stutus;
                TextBox[16].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[16].Text = "";
                TextBox[16].ForeColor = Color.Silver;
            }

            if (Student.File_Stutus.ToString() != "")
            {
                TextBox[20].Text = Student.Picture;
                TextBox[20].ForeColor = Color.DimGray;
            }
            else
            {
                TextBox[20].Text = "";
                TextBox[20].ForeColor = Color.Silver;
            }
        }

        #endregion

        #region Help Function

        public string New_Id()
        {
            Student Student = new Student();
            List<Student> Students = Student.Sql_Select();

            int Number_Student = Students.Count;

            if (Number_Student == 0)
            {
                string Id = "2018190001";
                return Id;
            }
            else
            {
                string Id = (int.Parse(Students[Number_Student - 1].Get_Id()) + 1).ToString();
                return Id;
            }
        }

        
        #endregion
    }
}
