using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CTM_DB.Class;
using CTM_DB;
using System.IO;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
namespace CTM_DB
{
    public partial class Main_Form : Form
    {
        GUI_Function GUI_Function;

        string User_Name_Lable;

        int Currunt_Index;

        #region Defintion Of Panel List
        List<Panel> Main_Panels;
        List<Panel> All_Panels;
        List<Panel> Sub_Panel;
        List<Panel> ComboBox_Panel;
        #endregion

        #region Defination Of Text Box List For Each Panel
        List<TextBox> Student_Info;
        List<TextBox> Receipt_Info;
        List<TextBox> Account_Info;
        List<TextBox> Movement_Receipt_Info;
        List<TextBox> Employee_Info;
        List<TextBox> Employee_Receipt_Info;

        #endregion
        
        
        public Main_Form(ref string User_Name)
        {
            InitializeComponent();

            User_Name_Lable = User_Name;

            #region List Of Panels
            Main_Panels = new List<Panel>() {Main_Panel, Student_Panel, Receipt_Table_Panel, Receipt_Panel, Account_Panel, Safe_Panel, Student_More_Panel, Student_Table_Panel, Movment_Receipt_Panel, Expenses_Panel, Imports_Panel, Employee_Panel, Employee_Receipt_Panel};
            All_Panels = new List<Panel> {Main_Panel ,Student_Panel, Receipt_Table_Panel, Receipt_Panel, Account_Panel, Safe_Panel, Account_Receipt_Info_Panel, Account_Info_Panel, Student_More_Panel, Student_Table_Panel, Movment_Receipt_Panel, Expenses_Panel, Imports_Panel,Employee_Panel, Employee_Receipt_Panel};
            Sub_Panel = new List<Panel>() {Account_Receipt_Info_Panel, Account_Info_Panel};
            ComboBox_Panel = new List<Panel>() {Student_Type_ComboBox, Student_Class_ComboBox, Student_Class_ConboBox_SubPanel1,Student_Class_ComboBox_SubPanel2,Student_Class_ComboBox_SubPanel3,Student_Class_ComboBox_SubPanel4, Student_Registration_ComboBox, Student_Transportation_ComboBox};
            #endregion

            #region List Of Text Box For Each Panel

            Student_Info = new List<TextBox>() { Student_Id_TextBox, Student_First_Name_TextBox, Student_Last_Name_TextBox, Student_Father_TextBox, Student_Date_TextBox, Student_Type_TextBox, Student_Phone1_TextBox, Student_Phone2_TextBox, Student_Class_TextBox, Student_Registration_TextBox, Student_School_TextBox,  Student_Transportation_TextBox, Student_City_TextBox, Student_Block_TextBox, Student_Street_TextBox, Student_Home_Detail_TextBox, Student_File_Stutus_TextBox, Student_File_Number_TextBox, Student_Account_Number_TextBox, Student_Note_TextBox, Student_Pic_TextBox};
            Receipt_Info = new List<TextBox>() { Receipt_Number_TextBox, Receipt_Book_TextBox, Receipt_Brunsh_TextBox, Receipt_Receipt_TextBox, Receipt_FirstName_TextBox, Receipt_LastName_TextBox, Receipt_AmountPaid_TextBox, Receipt_Date_TextBox, Receipt_For_TextBox };
            Account_Info = new List<TextBox>() { Account_Number_TextBox, Account_Name_TextBox, Account_Type_TextBox, Account_Conect1_TextBox, Account_Conect2_TextBox, Account_Whats_TextBox, Account_Total_TextBox, Account_Amount_TextBox, Account_Remender_TextBox };
            Movement_Receipt_Info = new List<TextBox>() { Movement_Receipt_Number_TextBox, Movement_Receipt_Book_TextBox, Movement_Receipt_Type_TextBox, Movement_Receipt_Owner_TextBox, Movement_Receipt_Kind_TextBox, Movement_Receipt_Amount_TextBox, Movement_Receipt_Date_TextBox, Movement_Receipt_Detail_TextBox };
            Employee_Info = new List<TextBox>() { Employee_Number_TextBox, Employee_Name_TextBox, Employye_Connect1_TextBox, Employee_Connect2_TextBox, Employee_Childern_TextBox, Employee_Type_TextBox, Employee_Class_TextBox, Employee_Number_Hour_TextBox, Employee_Detail_TextBox };
            Employee_Receipt_Info = new List<TextBox>() { Employee_Receipt_Number_TextBox, Employee_Receipt_Book_TextBox, Employee_Receipt_Owner_TextBox, Employee_Receipt_Date_TextBox, Employee_Receipt_Name_TextBox, Employee_Receipt_Sallary_TextBox, Employee_Receipt_Discount_TextBox, Employee_Receipt_Total_TextBox, Employee_Receipt_Detail_TextBox };

            #endregion

        }

        // Cheak For Internet Connection
        //

        public bool Cheak_Internet_Connection()
        {
            bool Conection = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            if (Conection == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Close Main Form ..
        //

        private void Close_Buttom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Maximized Main Form ..
        //

        private void Full_Secren_Buttom_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        // Main Form Load ..
        //

        private void Main_Form_Load(object sender, EventArgs e)
        {
            User_Name.Text = User_Name_Lable;

            WindowState = FormWindowState.Maximized;

            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Main_Panel);

            Account_Receipt_Table.Rows[0].MinimumHeight = 40;
            Account_Detail_Table.Rows[0].MinimumHeight = 40;
            Receipt_Table.Rows[0].MinimumHeight = 35;
            Student_Table.Rows[0].MinimumHeight = 40;
            Expenses_Table.Rows[0].MinimumHeight = 35;

        }

        // Update Buttom
        //

        private void Update_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Process Process = new Process();
                Process.Save_Update();
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Main Form Menu Events 
        //

        private void Home_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Main_Panel);
        }

        private void Student_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Student_Panel);

            Student_Main_Search_TextBox.Visible = true;

            Student Student = new Student();

            List<Student> All_Student = Student.Sql_Select();

            int Number_Student = All_Student.Count();
            Number_Student_Lable.Text = Number_Student.ToString();

            if (Number_Student > 0)
            {
                Currunt_Index = 0;
                Currant_Student_Lable.Text = (Currunt_Index + 1).ToString();
                Student.Get_Student_Info(All_Student[0], ref Student_Info);
            }

            bool Connection = Cheak_Internet_Connection();
            if (Connection == true)
            {
                Process Process = new Process();
                Process.Save_Update();
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Receipt_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Receipt_Table_Panel);

            Receipt_Table.Rows.Clear();

            Receipt Receipt = new Receipt();
            List<Receipt> Receipts = Receipt.Sql_Select();

            int Number_Receipts = Receipts.Count();

            for (int i = Number_Receipts - 1; i > -1; i--)
            {
                string[] Row = new string[] { Receipts.ElementAt(i).Get_Book(), Receipts.ElementAt(i).Get_Branch(), Receipts.ElementAt(i).Get_For(), Receipts.ElementAt(i).Get_Recipient(), Receipts.ElementAt(i).Get_Date(), Receipts.ElementAt(i).Get_Amount_Paid(), Receipts.ElementAt(i).Get_First_Name() + " " + Receipts.ElementAt(i).Get_Last_Name(), Receipts.ElementAt(i).Get_Number() };
                Receipt_Table.Rows.Add(Row);
            }

            NewReceipt_Buttom.Visible = true;
            Receipt_Back_Buttom.Visible = true;
            Receipt_Next_Buttom.Visible = true;
            Receipt_First_Buttom.Visible = true;
            Receipt_Last_Buttom.Visible = true;
            ReceiptMenu_Buttom.Visible = true;
            Receipt_Info_Search_TextBox.Visible = true;
            Receipt_Save_Buttom.Visible = false;
            Receipt_Cancel_Panel.Visible = false;
            Receipt_Menu_Panel.Visible = false;

            bool Connection = Cheak_Internet_Connection();
            if (Connection == true)
            {
                Process Process = new Process();
                Process.Save_Update();
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Account_Buttom_Click(object sender, EventArgs e)
        {
            Add_Account_Buttom.Visible = true;

            Account_Next_Buttom.Visible = true;
            Account_Back_Buttom.Visible = true;
            Account_First_Buttom.Visible = true;
            Account_Last_Buttom.Visible = true;

            Account_Info_Search_TextBox.Visible = true;

            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Account_Panel);
            GUI_Function.Panel_Visible(ref Sub_Panel, ref Account_Info_Panel);

            Account_Receipt_Table.Rows.Clear();
            Account_Detail_Table.Rows.Clear();

            Account Account = new Account();
            List<Account> Accounts = Account.Sql_Select();

            Currunt_Index = 0;

            int Number_Accounts = Accounts.Count();

            if (Number_Accounts > 0)
            {
                Account.Get_Account_Info(ref Account_Info, ref Account_Detail_Table, ref Account_Receipt_Table, Accounts[Currunt_Index]);
            }

            bool Connection = Cheak_Internet_Connection();
            if (Connection == true)
            {
                Process Process = new Process();
                Process.Save_Update();
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void The_Safe_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Safe_Panel);

            bool Connection = Cheak_Internet_Connection();
            if (Connection == true)
            {
                Process Process = new Process();
                Process.Save_Update();
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Employee_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Employee_Panel);

            Employee Employee = new Employee();

            List<Employee> All_Employee = Employee.Sql_Select();

            int Number_Employee = All_Employee.Count();

            if (Number_Employee > 0)
            {
                Currunt_Index = 0;
                Employee.Get_Employee_Info(ref Employee_Info, All_Employee[Currunt_Index]);
            }

            bool Connection = Cheak_Internet_Connection();
            if (Connection == true)
            {
                Process Process = new Process();
                Process.Save_Update();
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        


        #region Student Panel

        // Student Add Buttom ..
        //

        private void Student_Add_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Process Process = new Process();
                Process.Save_Update();

                Student_Add_Buttom.Visible = false;
                Student_Next_Buttom.Visible = false;
                Student_Back_Buttom.Visible = false;
                Student_More_Buttom.Visible = false;


                GUI_Function = new GUI_Function();
                GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);
                GUI_Function.Set_Student_TextBox(ref Student_Info);

                Student Student = new Student();
                Student_Id_TextBox.Text = Student.New_Id();
                Student_Id_TextBox.ForeColor = Color.DimGray;

                Student_Menu_Buttom.Visible = false;
                Student_Menu_Panel.Visible = false;

                Student_Main_Search_TextBox.Visible = false;

                Student_SaveInfo_Buttom.Visible = true;
                Student_Cancel_Buttom.Visible = true;
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Student_Menu_Add_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Student_Add_Buttom.Visible = false;
                Student_Next_Buttom.Visible = false;
                Student_Back_Buttom.Visible = false;

                GUI_Function = new GUI_Function();
                GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);
                GUI_Function.Set_Student_TextBox(ref Student_Info);

                Student Student = new Student();
                Student_Id_TextBox.Text = Student.New_Id();
                Student_Id_TextBox.ForeColor = Color.DimGray;

                Student_Main_Search_TextBox.Visible = false;

                Student_Menu_Buttom.Visible = false;
                Student_Menu_Panel.Visible = false;

                Student_SaveInfo_Buttom.Visible = true;
                Student_Cancel_Buttom.Visible = true;
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Student Basic Opreation Save - Cancel - Delete
        //

        private void Student_SaveInfo_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Student New_Student = new Student();
                New_Student.Set_Student_Info(ref Student_Info, ref New_Student);

                Process Process = new Process();
                Process.Save_Process("Save", "Student", New_Student.Get_Id(), User_Name.Text);

                New_Student.Sql_Insert(New_Student);
                New_Student.FireBase_Insert(New_Student);

                List<Student> All_Student = New_Student.Sql_Select();
                Number_Student_Lable.Text = All_Student.Count.ToString();

                New_Student.Get_Student_Info(All_Student[All_Student.Count() - 1], ref Student_Info);
                Currunt_Index = All_Student.Count() - 1;

                Currant_Student_Lable.Text = (Currunt_Index + 1).ToString();

                Student_Add_Buttom.Visible = true;
                Student_Next_Buttom.Visible = true;
                Student_Back_Buttom.Visible = true;
                Student_More_Buttom.Visible = true;

                GUI_Function = new GUI_Function();
                GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

                Student_Menu_Buttom.Visible = true;

                Student_Main_Search_TextBox.Visible = true;

                Student_SaveInfo_Buttom.Visible = false;
                Student_Cancel_Buttom.Visible = false;
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Student_Cancel_Buttom_Click(object sender, EventArgs e)
        {
            Student_Add_Buttom.Visible = true;
            Student_Next_Buttom.Visible = true;
            Student_Back_Buttom.Visible = true;
            Student_More_Buttom.Visible = true;

            Student_Main_Search_TextBox.Visible = true;

            GUI_Function = new GUI_Function();
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            Student_Menu_Buttom.Visible = true;

            Student_SaveInfo_Buttom.Visible = false;
            Student_Cancel_Buttom.Visible = false;

            Student Student = new Student();
            List<Student> All_Student = Student.Sql_Select();

            if (All_Student.Count == 0)
            {
                GUI_Function = new GUI_Function();
                GUI_Function.Set_Student_TextBox(ref Student_Info);
            }
            else
            {
                Student.Get_Student_Info(All_Student[Currunt_Index], ref Student_Info);
            }

        }

        private void Student_Menu_Delete_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                DialogResult Result = MessageBox.Show("هل انت متاكد من حذف الطالب الحالي", "حذف الطالب الحالي", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                Student Deleted_Student = new Student();

                if (Result == DialogResult.Yes)
                {
                    Process Process = new Process();
                    Process.Save_Process("Delete", "Student", Student_Id_TextBox.Text, User_Name.Text);

                    Deleted_Student.FireBase_Delete(Student_Id_TextBox.Text);
                    Deleted_Student.Sql_Delete(Student_Id_TextBox.Text);

                    List<Student> All_Student = Deleted_Student.Sql_Select();
                    int Number_Student = All_Student.Count();
                    Number_Student_Lable.Text = Number_Student.ToString();

                    if (Number_Student > 0)
                    {
                        if (Currunt_Index > 1)
                        {
                            Currunt_Index = Currunt_Index - 1;
                            Deleted_Student.Get_Student_Info(All_Student[Currunt_Index], ref Student_Info);
                        }
                        else
                        {
                            Currunt_Index = 0;
                            Deleted_Student.Get_Student_Info(All_Student[Currunt_Index], ref Student_Info);
                        }

                        Currant_Student_Lable.Text = (Currunt_Index + 1).ToString();
                    }
                    else
                    {
                        GUI_Function = new GUI_Function();
                        GUI_Function.Set_Student_TextBox(ref Student_Info);
                    }
                }
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Student Move Bettwen Record Opreation Next - Back - First - Last
        //

        private void Student_Next_Buttom_Click(object sender, EventArgs e)
        {
            Student Student = new Student();

            List<Student> All_Student = Student.Sql_Select();

            int Number_Student = All_Student.Count();

            if (Currunt_Index + 1 < Number_Student)
            {
                Currunt_Index = Currunt_Index + 1;
                Currant_Student_Lable.Text = (Currunt_Index + 1).ToString();

                Student.Get_Student_Info(All_Student[Currunt_Index], ref Student_Info);
            }
        }

        private void Student_Back_Buttom_Click(object sender, EventArgs e)
        {
            Student Student = new Student();

            List<Student> All_Student = Student.Sql_Select();

            int Number_Student = All_Student.Count();

            if (Currunt_Index - 1 > -1)
            {
                Currunt_Index = Currunt_Index - 1;
                Currant_Student_Lable.Text = (Currunt_Index + 1).ToString();

                Student.Get_Student_Info(All_Student[Currunt_Index], ref Student_Info);
            }
        }

        private void Student_First_Buttom_Click(object sender, EventArgs e)
        {
            Student Student = new Student();

            List<Student> Students = Student.Sql_Select();

            int Number_Student = Students.Count();

            Currunt_Index = 0;
            Currant_Student_Lable.Text = (Currunt_Index + 1).ToString();

            Student.Get_Student_Info(Students[Currunt_Index], ref Student_Info);
        }

        private void Student_Last_Buttom_Click(object sender, EventArgs e)
        {
            Student Student = new Student();

            List<Student> Students = Student.Sql_Select();

            int Number_Student = Students.Count();

            Currunt_Index = Number_Student - 1;
            Currant_Student_Lable.Text = (Currunt_Index + 1).ToString();

            Student.Get_Student_Info(Students[Currunt_Index], ref Student_Info);
        }

        // Student Table View Buttom
        //

        private void Student_Menu_Table_View_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Student_Table_Panel);

            Student_Table.Rows.Clear();

            Student Student = new Student();

            List<Student> All_Student = Student.Sql_Select();

            int Number_Student = All_Student.Count();

            for (int i = 0; i < Number_Student; i++)
            {
                string[] Row = new string[] {All_Student[i].Get_Id(),All_Student[i].Get_Connect2(),All_Student[i].Get_Connect1(),All_Student[i].Get_Transportation(),
                All_Student[i].Get_School_Name(), All_Student[i].Get_Registration(), All_Student[i].Get_Class(),All_Student[i].Get_First_Name() +" "+ All_Student[i].Get_Last_Name()};

                Student_Table.Rows.Add(Row);
            }
        }

        private void Student_BackTo_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Student_Panel);
        }

        // Student More Info Events
        //



        // Student  More Buttom ..
        //

        private void Student_More_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Student_More_Panel);
        }

        private void Student_Up_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Student_Panel);
        }

        // Student Menu Panel Events
        //

        private void Student_Menu_Buttom_Click(object sender, EventArgs e)
        {
            if (Student_Menu_Panel.Visible == false)
            {
                Student_Menu_Panel.Visible = true;
                Student_Menu_Panel.Left = 0;
                Student_Menu_Panel.Top = 42;
            }
            else
            {
                Student_Menu_Panel.Visible = false;
            }
        }

        // Student_Search Events
        //

        private void Student_Table_Search_TextBox_TextChanged(object sender, EventArgs e)
        {
            Student_Table.Rows.Clear();

            Student Student = new Student();

            List<Student> All_Student = Student.Sql_Search(Student_Table_Search_TextBox.Text);

            int Number_Student = All_Student.Count();

            for (int i = 0; i < Number_Student; i++)
            {
                string[] Row = new string[] {All_Student[i].Get_Id(),All_Student[i].Get_Connect2(),All_Student[i].Get_Connect1(),All_Student[i].Get_Transportation(),
                All_Student[i].Get_School_Name(), All_Student[i].Get_Registration(), All_Student[i].Get_Class(),All_Student[i].Get_First_Name() +" "+ All_Student[i].Get_Last_Name()};

                Student_Table.Rows.Add(Row);
            }
        }

        private void Student_Main_Search_TextBox_TextChanged(object sender, EventArgs e)
        {
            Student Student = new Student();

            List<Student> All_Student = Student.Sql_Search(Student_Main_Search_TextBox.Text);

            int Number_Student = All_Student.Count();

            Number_Student_Lable.Text = Number_Student.ToString();

            if (Number_Student > 0)
            {
                Currunt_Index = 0;
                Currant_Student_Lable.Text = (Currunt_Index + 1).ToString();
                Student.Get_Student_Info(All_Student[0], ref Student_Info);
            }
        }

        private void Student_Cancel_Filter_Buttom_Click(object sender, EventArgs e)
        {
            Student_Cancel_Filter_Buttom.Visible = false;

            Student_Main_Search_TextBox.Text = "... ابحث";

            Student Student = new Student();

            List<Student> Students = Student.Sql_Select();

            int Number_Student = Students.Count();

            Currunt_Index = 0;
            Currant_Student_Lable.Text = (Currunt_Index + 1).ToString();

            Student.Get_Student_Info(Students[Currunt_Index], ref Student_Info);
        }

        // Student View Buttom
        //

        private void Student_View_Info_Buttom_Click(object sender, EventArgs e)
        {
            if (Student_Table.SelectedCells.Count >= 1)
            {
                Student Student = new Student();

                List<Student> Students = Student.Sql_Select();

                int Index = Student_Table.CurrentCell.RowIndex;

                if (Index < Students.Count())
                {
                    GUI_Function = new GUI_Function();
                    GUI_Function.Panel_Visible(ref All_Panels, ref Student_Panel);

                    Student.Get_Student_Info(Students[Index], ref Student_Info);

                    Currunt_Index = Index;

                }
            }
        }

        // Student Text Box Event Enter - Leave
        //

        private void Student_Id_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Id_TextBox.Text == "000000000")
            {
                Student_Id_TextBox.Text = "";
                Student_Id_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Id_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Id_TextBox.Text == "")
            {
                Student_Id_TextBox.Text = "000000000";
                Student_Id_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_First_Name_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_First_Name_TextBox.Text == "الاسم الاول")
            {
                Student_First_Name_TextBox.Text = "";
                Student_First_Name_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_First_Name_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_First_Name_TextBox.Text == "")
            {
                Student_First_Name_TextBox.Text = "الاسم الاول";
                Student_First_Name_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Last_Name_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Last_Name_TextBox.Text == "الاسم الاخير")
            {
                Student_Last_Name_TextBox.Text = "";
                Student_Last_Name_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Last_Name_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Last_Name_TextBox.Text == "")
            {
                Student_Last_Name_TextBox.Text = "الاسم الاخير";
                Student_Last_Name_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Father_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Father_TextBox.Text == "اسم الاب")
            {
                Student_Father_TextBox.Text = "";
                Student_Father_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Father_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Father_TextBox.Text == "")
            {
                Student_Father_TextBox.Text = "اسم الاب";
                Student_Father_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Date_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Date_TextBox.Text == "01/01/1990")
            {
                Student_Date_TextBox.Text = "";
                Student_Date_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Date_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Date_TextBox.Text == "")
            {
                Student_Date_TextBox.Text = "01/01/1990";
                Student_Date_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Type_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Type_TextBox.Text == "النوع")
            {
                Student_Type_TextBox.Text = "";
                Student_Type_TextBox.ForeColor = Color.DimGray;
            }

            Student_Type_ComboBox.Visible = true;
            Student_Type_ComboBox.Left = 564;
            Student_Type_ComboBox.Top = 316;
        }

        private void Student_Type_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Type_TextBox.Text == "")
            {
                Student_Type_TextBox.Text = "النوع";
                Student_Type_TextBox.ForeColor = Color.Silver;
            }

        }

        private void Student_Phone1_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Phone1_TextBox.Text == "022-0")
            {
                Student_Phone1_TextBox.Text = "";
                Student_Phone1_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Phone1_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Phone1_TextBox.Text == "")
            {
                Student_Phone1_TextBox.Text = "022-0";
                Student_Phone1_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Phone2_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Phone2_TextBox.Text == "022-0")
            {
                Student_Phone2_TextBox.Text = "";
                Student_Phone2_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Phone2_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Phone2_TextBox.Text == "")
            {
                Student_Phone2_TextBox.Text = "022-0";
                Student_Phone2_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Class_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Class_TextBox.Text == "المرحلة الدراسية")
            {
                Student_Class_TextBox.Text = "";
                Student_Class_TextBox.ForeColor = Color.DimGray;
            }

            Student_Class_ComboBox.Visible = true;
            Student_Class_ComboBox.Left = 138;
            Student_Class_ComboBox.Top = 153;
        }

        private void Student_Class_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Class_TextBox.Text == "")
            {
                Student_Class_TextBox.Text = "المرحلة الدراسية";
                Student_Class_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Registration_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Registration_TextBox.Text == "نوع التسجيل")
            {
                Student_Registration_TextBox.Text = "";
                Student_Registration_TextBox.ForeColor = Color.DimGray;
            }

            Student_Registration_ComboBox.Visible = true;
            Student_Registration_ComboBox.Left = 138;
            Student_Registration_ComboBox.Top = 194;
        }

        private void Student_Registration_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Registration_TextBox.Text == "")
            {
                Student_Registration_TextBox.Text = "نوع التسجيل";
                Student_Registration_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_School_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_School_TextBox.Text == "اسم المدرسة")
            {
                Student_School_TextBox.Text = "";
                Student_School_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_School_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_School_TextBox.Text == "")
            {
                Student_School_TextBox.Text = "اسم المدرسة";
                Student_School_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Transportation_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Transportation_TextBox.Text == "المواصلات")
            {
                Student_Transportation_TextBox.Text = "";
                Student_Transportation_TextBox.ForeColor = Color.DimGray;
            }

            Student_Transportation_ComboBox.Visible = true;
            Student_Transportation_ComboBox.Left = 138;
            Student_Transportation_ComboBox.Top = 275;
        }

        private void Student_Transportation_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Transportation_TextBox.Text == "")
            {
                Student_Transportation_TextBox.Text = "المواصلات";
                Student_Transportation_TextBox.ForeColor = Color.Silver;
            }

            if (Student_Transportation_TextBox.Text == "مع")
            {
                Student_Home_Lable.Visible = true;
                Line3_Panel.Visible = true;

                Student_City_TextBox.Visible = true;
                Student_Block_TextBox.Visible = true;
                Student_Street_TextBox.Visible = true;
                Student_Home_Detail_TextBox.Visible = true;
            }
        }

        private void Student_City_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_City_TextBox.Text == "المدينة")
            {
                Student_City_TextBox.Text = "";
                Student_City_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_City_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_City_TextBox.Text == "")
            {
                Student_City_TextBox.Text = "المدينة";
                Student_City_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Block_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Block_TextBox.Text == "الحي")
            {
                Student_Block_TextBox.Text = "";
                Student_Block_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Block_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Block_TextBox.Text == "")
            {
                Student_Block_TextBox.Text = "الحي";
                Student_Block_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Street_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Street_TextBox.Text == "الشارع")
            {
                Student_Street_TextBox.Text = "";
                Student_Street_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Street_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Street_TextBox.Text == "")
            {
                Student_Street_TextBox.Text = "الشارع";
                Student_Street_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Home_Detail_TextBox_Enter(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);

            if (Student_Home_Detail_TextBox.Text == "التفاصيل")
            {
                Student_Home_Detail_TextBox.Text = "";
                Student_Home_Detail_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Home_Detail_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Home_Detail_TextBox.Text == "")
            {
                Student_Home_Detail_TextBox.Text = "التفاصيل";
                Student_Home_Detail_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_File_Stutus_TextBox_Enter(object sender, EventArgs e)
        {
            if (Student_File_Stutus_TextBox.Text == "مفعل")
            {
                Student_File_Stutus_TextBox.Text = "";
                Student_File_Stutus_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_File_Stutus_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_File_Stutus_TextBox.Text == "")
            {
                Student_File_Stutus_TextBox.Text = "مفعل";
                Student_File_Stutus_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_File_Number_TextBox_Enter(object sender, EventArgs e)
        {
            if (Student_File_Number_TextBox.Text == "201819001")
            {
                Student_File_Number_TextBox.Text = "";
                Student_File_Number_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_File_Number_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_File_Number_TextBox.Text == "")
            {
                Student_File_Number_TextBox.Text = "2018119001";
                Student_File_Number_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Account_Number_TextBox_Enter(object sender, EventArgs e)
        {
            if (Student_Account_Number_TextBox.Text == "201819001")
            {
                Student_Account_Number_TextBox.Text = "";
                Student_Account_Number_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Account_Number_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Account_Number_TextBox.Text == "")
            {
                Student_Account_Number_TextBox.Text = "201819001";
                Student_Account_Number_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Note_TextBox_Enter(object sender, EventArgs e)
        {
            if (Student_Note_TextBox.Text == "اكتب المزبد من الملاحظات")
            {
                Student_Note_TextBox.Text = "";
                Student_Note_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Note_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Note_TextBox.Text == "")
            {
                Student_Note_TextBox.Text = "اكتب المزبد من الملاحظات";
                Student_Note_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Transportation_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (Student_Transportation_TextBox.Text == "مع")
            {
                Student_Home_Lable.Visible = true;
                Line3_Panel.Visible = true;

                Student_City_TextBox.Visible = true;
                Student_Block_TextBox.Visible = true;
                Student_Street_TextBox.Visible = true;
                Student_Home_Detail_TextBox.Visible = true;
            }
            else
            {
                Student_Home_Lable.Visible = false;
                Line3_Panel.Visible = false;

                Student_City_TextBox.Visible = false;
                Student_Block_TextBox.Visible = false;
                Student_Street_TextBox.Visible = false;
                Student_Home_Detail_TextBox.Visible = false;
            }
        }

        private void Student_Table_Search_TextBox_Enter(object sender, EventArgs e)
        {
            if (Student_Table_Search_TextBox.Text == "...اكتب ما تبحث عنه او اختر معامل بحث")
            {
                Student_Table_Search_TextBox.Text = "";
                Student_Table_Search_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Student_Table_Search_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Table_Search_TextBox.Text == "")
            {
                Student_Table_Search_TextBox.Text = "...اكتب ما تبحث عنه او اختر معامل بحث";
                Student_Table_Search_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Student_Main_Search_TextBox_Enter(object sender, EventArgs e)
        {
            if (Student_Main_Search_TextBox.Text == "... ابحث")
            {
                Student_Main_Search_TextBox.Text = "";
                Student_Cancel_Filter_Buttom.Visible = true;
            }
        }

        private void Student_Main_Search_TextBox_Leave(object sender, EventArgs e)
        {
            if (Student_Main_Search_TextBox.Text == "")
            {
                Student_Main_Search_TextBox.Text = "... ابحث";
                Student_Cancel_Filter_Buttom.Visible = false;
            }
        }

        // Student Combo Box Events
        //

        private void Student_Panel_Click(object sender, EventArgs e)
        {
            GUI_Function.Hide_ComboBox_Panel(ref ComboBox_Panel);
            Student_Menu_Panel.Visible = false;
        }

        private void Student_Type_Down_Buttom_Click(object sender, EventArgs e)
        {
            if (Student_Type_ComboBox.Visible == false)
            {
                Student_Type_ComboBox.Visible = true;
                Student_Type_ComboBox.Left = 564;
                Student_Type_ComboBox.Top = 316;
            }
            else
            {
                Student_Type_ComboBox.Visible = false;
            }
        }

        private void Student_Type_ComboBox_Buttom1_Click(object sender, EventArgs e)
        {
            Student_Type_ComboBox.Visible = false;
            Student_Type_TextBox.Text = "ذكر";
            Student_Type_TextBox.ForeColor = Color.DimGray;
        }

        private void Student_Type_ComboBox_Buttom2_Click(object sender, EventArgs e)
        {
            Student_Type_ComboBox.Visible = false;
            Student_Type_TextBox.Text = "انثى";
            Student_Type_TextBox.ForeColor = Color.DimGray;
        }

        private void Student_Class_Down_Buttom_Click(object sender, EventArgs e)
        {
            if (Student_Class_ComboBox.Visible == false)
            {
                Student_Class_ComboBox.Visible = true;
                Student_Class_ComboBox.Left = 138;
                Student_Class_ComboBox.Top = 153;
            }
            else
            {
                Student_Class_ComboBox.Visible = false;
            }
        }

        private void Student_Class_ComboBox_Buttom1_Click(object sender, EventArgs e)
        {
            if (Student_Class_ConboBox_SubPanel1.Visible == false)
            {
                Student_Class_ConboBox_SubPanel1.Visible = true;
                Student_Class_ConboBox_SubPanel1.Left = 365;
                Student_Class_ConboBox_SubPanel1.Top = 153;

                Student_Class_ComboBox_SubPanel2.Visible = false;
                Student_Class_ComboBox_SubPanel3.Visible = false;
                Student_Class_ComboBox_SubPanel4.Visible = false;
            }
            else
            {
                Student_Class_ConboBox_SubPanel1.Visible = false;
            }
        }

        private void ComboBox_Sub1_Buttom1_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "الثالث الثانوي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ConboBox_SubPanel1.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void ComboBox_Sub1_Buttom2_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "الثاني الثانوي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ConboBox_SubPanel1.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void ComboBox_Sub1_Butom3_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "الاول الثانوي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ConboBox_SubPanel1.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void Student_Class_ComboBox_Buttom2_Click(object sender, EventArgs e)
        {
            if (Student_Class_ComboBox_SubPanel2.Visible == false)
            {
                Student_Class_ComboBox_SubPanel2.Visible = true;
                Student_Class_ComboBox_SubPanel2.Left = 365;
                Student_Class_ComboBox_SubPanel2.Top = 153;

                Student_Class_ConboBox_SubPanel1.Visible = false;
                Student_Class_ComboBox_SubPanel3.Visible = false;
                Student_Class_ComboBox_SubPanel4.Visible = false;
            }
            else
            {
                Student_Class_ComboBox_SubPanel2.Visible = false;
            }
        }

        private void ComboBox_Sub2_Buttom1_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "الثالث الاعدادي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ComboBox_SubPanel2.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void ComboBox_Sub2_Buttom2_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "الثاني الاعدادي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ComboBox_SubPanel2.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void ComboBox_Sub2_Buttom3_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "الاول الاعدادي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ComboBox_SubPanel2.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void Student_Class_ComboBox_Butto3_Click(object sender, EventArgs e)
        {
            if (Student_Class_ComboBox_SubPanel3.Visible == false)
            {
                Student_Class_ComboBox_SubPanel3.Visible = true;
                Student_Class_ComboBox_SubPanel3.Left = 365;
                Student_Class_ComboBox_SubPanel3.Top = 153;

                Student_Class_ConboBox_SubPanel1.Visible = false;
                Student_Class_ComboBox_SubPanel2.Visible = false;
                Student_Class_ComboBox_SubPanel4.Visible = false;
            }
            else
            {
                Student_Class_ComboBox_SubPanel3.Visible = false;
            }
        }

        private void ComboBox_Sub3_Buttom1_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "السادس الابتدائي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ComboBox_SubPanel3.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void ComboBox_Sub3_Buttom2_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "الخامس الابتدائي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ComboBox_SubPanel3.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void ComboBox_Sub3_Buttom3_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "الرابع الابتدائي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ComboBox_SubPanel3.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void ComboBox_Sub3_Buttom4_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "الثالث الابتدائي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ComboBox_SubPanel3.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void ComBoBox_Sub3_Buttom5_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "الثاني الابتدائي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ComboBox_SubPanel3.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void ComboBox_Sub3_Buttom6_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "الاول الابتدائي";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ComboBox_SubPanel3.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void Student_Class_ComboBox_Buttom4_Click(object sender, EventArgs e)
        {
            if (Student_Class_ComboBox_SubPanel4.Visible == false)
            {
                Student_Class_ComboBox_SubPanel4.Visible = true;
                Student_Class_ComboBox_SubPanel4.Left = 365;
                Student_Class_ComboBox_SubPanel4.Top = 153;

                Student_Class_ConboBox_SubPanel1.Visible = false;
                Student_Class_ComboBox_SubPanel2.Visible = false;
                Student_Class_ComboBox_SubPanel3.Visible = false;
            }
            else
            {
                Student_Class_ComboBox_SubPanel4.Visible = false;
            }
        }

        private void ComboBox_Sub4_Buttom1_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "رياض الاطفال 1";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ComboBox_SubPanel4.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void ComboBox_Sub4_Buttom2_Click(object sender, EventArgs e)
        {
            Student_Class_TextBox.Text = "رياض الاطفال 2";
            Student_Class_TextBox.ForeColor = Color.DimGray;

            Student_Class_ComboBox_SubPanel4.Visible = false;
            Student_Class_ComboBox.Visible = false;
        }

        private void Student_Registration_Down_Buttom_Click(object sender, EventArgs e)
        {
            if (Student_Registration_ComboBox.Visible == false)
            {
                Student_Registration_ComboBox.Visible = true;
                Student_Registration_ComboBox.Left = 138;
                Student_Registration_ComboBox.Top = 194;
            }
            else
            {
                Student_Registration_ComboBox.Visible = false;
            }
        }

        private void Student_Registration_ComboBox_Buttom1_Click(object sender, EventArgs e)
        {
            Student_Registration_TextBox.Text = "خاص";
            Student_Registration_TextBox.ForeColor = Color.DimGray;
            Student_Registration_ComboBox.Visible = false;
        }

        private void Student_Registration_ComboBox_Buttom2_Click(object sender, EventArgs e)
        {
            Student_Registration_TextBox.Text = "عام";
            Student_Registration_TextBox.ForeColor = Color.DimGray;
            Student_Registration_ComboBox.Visible = false;
        }

        private void Student_Registration_ComboBox_Buttom3_Click(object sender, EventArgs e)
        {
            Student_Registration_TextBox.Text = "منازل";
            Student_Registration_TextBox.ForeColor = Color.DimGray;
            Student_Registration_ComboBox.Visible = false;
        }

        private void Down_Click(object sender, EventArgs e)
        {
            if (Student_Transportation_ComboBox.Visible == false)
            {
                Student_Transportation_ComboBox.Visible = true;
                Student_Transportation_ComboBox.Left = 137;
                Student_Transportation_ComboBox.Top = 278;
            }
            else
            {
                Student_Transportation_ComboBox.Visible = false;
            }
        }

        private void Student_Transportation_ComboBox_Buttom1_Click(object sender, EventArgs e)
        {
            Student_Transportation_TextBox.Text = "مع";
            Student_Transportation_TextBox.ForeColor = Color.DimGray;
            Student_Transportation_ComboBox.Visible = false;

            Student_Home_Lable.Visible = true;
            Line3_Panel.Visible = true;

            Student_City_TextBox.Visible = true;
            Student_Block_TextBox.Visible = true;
            Student_Street_TextBox.Visible = true;
            Student_Home_Detail_TextBox.Visible = true;
        }

        private void Student_Transportation_ComboBox_Buttom2_Click(object sender, EventArgs e)
        {
            Student_Transportation_TextBox.Text = "بدون";
            Student_Transportation_TextBox.ForeColor = Color.DimGray;
            Student_Transportation_ComboBox.Visible = false;

            Student_Home_Lable.Visible = false;
            Line3_Panel.Visible = false;

            Student_City_TextBox.Visible = false;
            Student_Block_TextBox.Visible = false;
            Student_Street_TextBox.Visible = false;
            Student_Home_Detail_TextBox.Visible = false;
        }

        #endregion

        #region Receipts Panel

        // Add New Receipt Buttom 
        //

        private void NewReceipt_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                GUI_Function = new GUI_Function();
                GUI_Function.Clear_Receipt_TextBox(ref Receipt_Info);

                NewReceipt_Buttom.Visible = false;
                Receipt_Back_Buttom.Visible = false;
                Receipt_Next_Buttom.Visible = false;
                Receipt_First_Buttom.Visible = false;
                Receipt_Last_Buttom.Visible = false;
                ReceiptMenu_Buttom.Visible = false;

                Receipt_Info_Search_TextBox.Visible = false;

                Receipt_Save_Buttom.Visible = true;
                Receipt_Cancel_Panel.Visible = true;
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Receipt_Menu_Add_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                GUI_Function = new GUI_Function();
                GUI_Function.Clear_Receipt_TextBox(ref Receipt_Info);

                Receipt_Menu_Panel.Visible = false;

                NewReceipt_Buttom.Visible = false;
                Receipt_Back_Buttom.Visible = false;
                Receipt_Next_Buttom.Visible = false;
                Receipt_First_Buttom.Visible = false;
                Receipt_Last_Buttom.Visible = false;
                ReceiptMenu_Buttom.Visible = false;

                Receipt_Info_Search_TextBox.Visible = false;

                Receipt_Save_Buttom.Visible = true;
                Receipt_Cancel_Panel.Visible = true;
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Add_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {

                GUI_Function = new GUI_Function();
                GUI_Function.Panel_Visible(ref All_Panels, ref Receipt_Panel);
                GUI_Function.Clear_Receipt_TextBox(ref Receipt_Info);

                NewReceipt_Buttom.Visible = false;
                Receipt_Back_Buttom.Visible = false;
                Receipt_Next_Buttom.Visible = false;
                Receipt_First_Buttom.Visible = false;
                Receipt_Last_Buttom.Visible = false;
                ReceiptMenu_Buttom.Visible = false;
                Receipt_Menu_Panel.Visible = false;

                Receipt_Info_Search_TextBox.Visible = false;

                Receipt_Save_Buttom.Visible = true;
                Receipt_Cancel_Panel.Visible = true;
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Receipts Basic Opreation Save - Cancel - Delete
        //

        private void Receipt_Save_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Receipt Receipt = new Receipt();
                Receipt.Set_Receipt_Info(Receipt_Info, ref Receipt);

                Process Process = new Process();
                Process.Save_Process("Save", "Receipt", Receipt.Get_Number(), User_Name.Text);

                Receipt.Sql_Insert(Receipt);
                Receipt.FireBase_Insert(Receipt);

                NewReceipt_Buttom.Visible = true;
                Receipt_Back_Buttom.Visible = true;
                Receipt_Next_Buttom.Visible = true;
                Receipt_First_Buttom.Visible = true;
                Receipt_Last_Buttom.Visible = true;
                ReceiptMenu_Buttom.Visible = true;

                Receipt_Info_Search_TextBox.Visible = true;

                Receipt_Save_Buttom.Visible = false;
                Receipt_Cancel_Panel.Visible = false;

                List<Receipt> Receipts = Receipt.Sql_Select();

                int Number_Receipt = Receipts.Count();

                Currunt_Index = Number_Receipt - 1;

                Receipt.Get_Receipt_Info(ref Receipt_Info, Receipts[Currunt_Index]);
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Receipt_Cancel_Panel_Click(object sender, EventArgs e)
        {
            NewReceipt_Buttom.Visible = true;
            Receipt_Back_Buttom.Visible = true;
            Receipt_Next_Buttom.Visible = true;
            Receipt_First_Buttom.Visible = true;
            Receipt_Last_Buttom.Visible = true;
            ReceiptMenu_Buttom.Visible = true;

            Receipt_Info_Search_TextBox.Visible = true;

            Receipt_Save_Buttom.Visible = false;
            Receipt_Cancel_Panel.Visible = false;

            Receipt Receipt = new Receipt();

            List<Receipt> Receipts = Receipt.Sql_Select();

            if (Receipts.Count == 0)
            {
                GUI_Function = new GUI_Function();
                GUI_Function.Clear_Receipt_TextBox(ref Receipt_Info);
            }
            else
            {
                Receipt.Get_Receipt_Info(ref Receipt_Info, Receipts[Currunt_Index]);
            }
        }

        private void Receipt_Menu_Delete_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Receipt_Menu_Panel.Visible = false;

                DialogResult Result = MessageBox.Show("هل انت متاكد من حذف الايصال الحالي", "حذف الايصال الحالي", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                Receipt Deleted_Receipt = new Receipt();

                if (Result == DialogResult.Yes)
                {
                    Process Process = new Process();
                    Process.Save_Process("Delete", "Receipt", Receipt_Number_TextBox.Text, User_Name.Text);

                    Deleted_Receipt.FireBase_Delete(Receipt_Number_TextBox.Text);
                    Deleted_Receipt.Sql_Delete(Receipt_Number_TextBox.Text);

                    List<Receipt> Receipts = Deleted_Receipt.Sql_Select();

                    int Number_Student = Receipts.Count();

                    if (Number_Student > 0)
                    {
                        if (Currunt_Index > 1)
                        {
                            Currunt_Index = Currunt_Index - 1;
                            Deleted_Receipt.Get_Receipt_Info(ref Receipt_Info, Receipts[Currunt_Index]);
                        }
                        else
                        {
                            Currunt_Index = 0;
                            Deleted_Receipt.Get_Receipt_Info(ref Receipt_Info, Receipts[Currunt_Index]);
                        }
                    }
                    else
                    {
                        GUI_Function = new GUI_Function();
                        GUI_Function.Clear_Receipt_TextBox(ref Receipt_Info);
                    }
                }
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Basic View Opreation 1) View Receipt 2) View Receipts Table
        //

        private void Receipt_View_Buttom_Click(object sender, EventArgs e)
        {
            if (Receipt_Table.SelectedCells.Count >= 1)
            {
                Receipt Receipt = new Receipt();

                List<Receipt> Receipts = Receipt.Sql_Select();

                int Index = Receipt_Table.CurrentCell.RowIndex;

                if (Index < Receipts.Count())
                {
                    GUI_Function = new GUI_Function();
                    GUI_Function.Panel_Visible(ref All_Panels, ref Receipt_Panel);

                    Receipt.Get_Receipt_Info(ref Receipt_Info, Receipts[Receipts.Count() - Index - 1]);

                    Currunt_Index = Receipts.Count() - Index - 1;

                }
            }
            
        }

        private void Receipt_Menu_View_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Receipt_Table_Panel);
        }

        // Search Events In Receipt Table
        //

        private void Receipt_Table_Search_TextBox_TextChanged(object sender, EventArgs e)
        {
            Receipt_Table.Rows.Clear();

            Receipt Receipt = new Receipt();
            List<Receipt> Receipts = Receipt.Sql_Search(Receipt_Table_Search_TextBox.Text);

            int Number_Receipts = Receipts.Count();

            if (Number_Receipts > 0)
            {
                for (int i = Number_Receipts - 1; i > -1; i--)
                {
                    string[] Row = new string[] { Receipts.ElementAt(i).Get_Book(), Receipts.ElementAt(i).Get_Branch(), Receipts.ElementAt(i).Get_For(), Receipts.ElementAt(i).Get_Recipient(), Receipts.ElementAt(i).Get_Date(), Receipts.ElementAt(i).Get_Amount_Paid(), Receipts.ElementAt(i).Get_First_Name() + " " + Receipts.ElementAt(i).Get_Last_Name(), Receipts.ElementAt(i).Get_Number() };
                    Receipt_Table.Rows.Add(Row);
                }
            }
        }

        private void Receipt_Info_Search_TextBox_TextChanged(object sender, EventArgs e)
        {
            Receipt Receipt = new Receipt();

            List<Receipt> All_Receipt = Receipt.Sql_Search(Receipt_Info_Search_TextBox.Text);

            int Number_Receipt = All_Receipt.Count();

            if (Number_Receipt > 0)
            {
                Currunt_Index = 0;
                Receipt.Get_Receipt_Info(ref Receipt_Info, All_Receipt[0]);
            }
        }

        private void Receipt_Search_Cancel_Buttom_Click(object sender, EventArgs e)
        {
            Receipt_Info_Search_TextBox.Text = "... ابحث";

            Receipt_Search_Cancel_Buttom.Visible = false;

            Receipt Receipt = new Receipt();

            List<Receipt> Receipts = Receipt.Sql_Select();

            int Number_Receipt = Receipts.Count();

            Currunt_Index = 0;

            Receipt.Get_Receipt_Info(ref Receipt_Info, Receipts[Currunt_Index]);
        }

        // Print Receipt Events
        //

        private void Receipt_Menu_Print_Buttom_Click(object sender, EventArgs e)
        {
            Receipt_Info_Report This_Receipt = new Receipt_Info_Report();

            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlDataAdapter Adapter = new SqlDataAdapter("Select * From Receipt_Table Where Number='" + Receipt_Number_TextBox.Text + "'", Connect);

            Adapter.Fill(This_Receipt.CTM_DBDataSet.Receipt_Table);

            This_Receipt.reportViewer1.RefreshReport();

            This_Receipt.Show();
        }

        private void Print_All_Receipt_Buttom_Click(object sender, EventArgs e)
        {
            Receipt_Form All_Receipt = new Receipt_Form();
            All_Receipt.Show();
        }

        // Receipt Move Bettwen Record Opreation Next - Back - First - Last
        //

        private void Receipt_Back_Buttom_Click(object sender, EventArgs e)
        {
            Receipt Receipt = new Receipt();

            List<Receipt> Receipts = Receipt.Sql_Select();

            int Number_Receipt = Receipts.Count();

            if (Currunt_Index - 1 > -1)
            {
                Currunt_Index = Currunt_Index - 1;
                Receipt.Get_Receipt_Info(ref Receipt_Info, Receipts[Currunt_Index]);
            }
        }

        private void Receipt_Next_Buttom_Click(object sender, EventArgs e)
        {
            Receipt Receipt = new Receipt();

            List<Receipt> Receipts = Receipt.Sql_Select();

            int Number_Receipt = Receipts.Count();

            if (Currunt_Index + 1 < Number_Receipt)
            {
                Currunt_Index = Currunt_Index + 1;
                Receipt.Get_Receipt_Info(ref Receipt_Info, Receipts[Currunt_Index]);
            }
            
            
        }

        private void Receipt_First_Buttom_Click(object sender, EventArgs e)
        {
            Receipt Receipt = new Receipt();

            List<Receipt> Receipts = Receipt.Sql_Select();

            int Number_Receipt = Receipts.Count();

            Currunt_Index = 0;

            Receipt.Get_Receipt_Info(ref Receipt_Info, Receipts[Currunt_Index]);
        }

        private void Receipt_Last_Buttom_Click(object sender, EventArgs e)
        {
            Receipt Receipt = new Receipt();

            List<Receipt> Receipts = Receipt.Sql_Select();

            int Number_Receipt = Receipts.Count();

            Currunt_Index = Number_Receipt - 1;

            Receipt.Get_Receipt_Info(ref Receipt_Info, Receipts[Currunt_Index]);
        }

        // Receipt Menu Event
        //

        private void ReceiptMenu_Buttom_Click(object sender, EventArgs e)
        {
            if (Receipt_Menu_Panel.Visible == false)
            {
                Receipt_Menu_Panel.Visible = true;
                Receipt_Menu_Panel.Top = 54;
                Receipt_Menu_Panel.Left = 15;
            }
            else
            {
                Receipt_Menu_Panel.Visible = false;
            }
        }

        private void Receipt_Panel_Click(object sender, EventArgs e)
        {
            Receipt_Menu_Panel.Visible = false;
        }

        // Receipts TextBox Event Enter - Leave
        //

        private void Receipt_Number_TextBox_Enter(object sender, EventArgs e)
        {
            if (Receipt_Number_TextBox.Text == "0000000000")
            {
                Receipt_Number_TextBox.Text = "";
                Receipt_Number_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Receipt_Number_TextBox_Leave(object sender, EventArgs e)
        {
            if (Receipt_Number_TextBox.Text == "")
            {
                Receipt_Number_TextBox.Text = "0000000000";
                Receipt_Number_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Receipt_Book_TextBox_Enter(object sender, EventArgs e)
        {
            if (Receipt_Book_TextBox.Text == "A19 or B19")
            {
                Receipt_Book_TextBox.Text = "";
                Receipt_Book_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Receipt_Book_TextBox_Leave(object sender, EventArgs e)
        {
            if (Receipt_Book_TextBox.Text == "")
            {
                Receipt_Book_TextBox.Text = "A19 or B19";
                Receipt_Book_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Receipt_Brunsh_TextBox_Enter(object sender, EventArgs e)
        {
            if (Receipt_Brunsh_TextBox.Text == "اسم الفرع")
            {
                Receipt_Brunsh_TextBox.Text = "";
                Receipt_Brunsh_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Receipt_Brunsh_TextBox_Leave(object sender, EventArgs e)
        {
            if (Receipt_Brunsh_TextBox.Text == "")
            {
                Receipt_Brunsh_TextBox.Text = "اسم الفرع";
                Receipt_Brunsh_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Receipt_Receipt_TextBox_Enter(object sender, EventArgs e)
        {
            if (Receipt_Receipt_TextBox.Text == "اسم المستلم")
            {
                Receipt_Receipt_TextBox.Text = "";
                Receipt_Receipt_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Receipt_Receipt_TextBox_Leave(object sender, EventArgs e)
        {
            if (Receipt_Receipt_TextBox.Text == "")
            {
                Receipt_Receipt_TextBox.Text = "اسم المستلم";
                Receipt_Receipt_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Receipt_FirstName_TextBox_Enter(object sender, EventArgs e)
        {
            if (Receipt_FirstName_TextBox.Text == "الاسم الاول")
            {
                Receipt_FirstName_TextBox.Text = "";
                Receipt_FirstName_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Receipt_FirstName_TextBox_Leave(object sender, EventArgs e)
        {
            if (Receipt_FirstName_TextBox.Text == "")
            {
                Receipt_FirstName_TextBox.Text = "الاسم الاول";
                Receipt_FirstName_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Receipt_LastName_TextBox_Enter(object sender, EventArgs e)
        {
            if (Receipt_LastName_TextBox.Text == "الاسم الاخير")
            {
                Receipt_LastName_TextBox.Text = "";
                Receipt_LastName_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Receipt_LastName_TextBox_Leave(object sender, EventArgs e)
        {
            if (Receipt_LastName_TextBox.Text == "")
            {
                Receipt_LastName_TextBox.Text = "الاسم الاخير";
                Receipt_LastName_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Receipt_AmountPaid_TextBox_Enter(object sender, EventArgs e)
        {
            if (Receipt_AmountPaid_TextBox.Text == "000 L.E")
            {
                Receipt_AmountPaid_TextBox.Text = "";
                Receipt_AmountPaid_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Receipt_AmountPaid_TextBox_Leave(object sender, EventArgs e)
        {
            if (Receipt_AmountPaid_TextBox.Text == "")
            {
                Receipt_AmountPaid_TextBox.Text = "000 L.E";
                Receipt_AmountPaid_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Receipt_Date_TextBox_Enter(object sender, EventArgs e)
        {
            if (Receipt_Date_TextBox.Text == "01/01/1990")
            {
                Receipt_Date_TextBox.Text = "";
                Receipt_Date_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Receipt_Date_TextBox_Leave(object sender, EventArgs e)
        {
            if (Receipt_Date_TextBox.Text == "")
            {
                Receipt_Date_TextBox.Text = "01/01/1990";
                Receipt_Date_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Receipt_For_TextBox_Enter(object sender, EventArgs e)
        {
            if (Receipt_For_TextBox.Text == "مصروفات")
            {
                Receipt_For_TextBox.Text = "";
                Receipt_For_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Receipt_For_TextBox_Leave(object sender, EventArgs e)
        {
            if (Receipt_For_TextBox.Text == "")
            {
                Receipt_For_TextBox.Text = "مصروفات";
                Receipt_For_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Receipt_Table_Search_TextBox_Enter(object sender, EventArgs e)
        {
            if (Receipt_Table_Search_TextBox.Text == "...اكتب ما تبحث عنه او اختر معامل بحث")
            {
                Receipt_Table_Search_TextBox.Text = "";
                Receipt_Table_Search_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Receipt_Table_Search_TextBox_Leave(object sender, EventArgs e)
        {
            if (Receipt_Table_Search_TextBox.Text == "")
            {
                Receipt_Table_Search_TextBox.Text = "...اكتب ما تبحث عنه او اختر معامل بحث";
                Receipt_Table_Search_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Receipt_Info_Search_TextBox_Enter(object sender, EventArgs e)
        {
            if (Receipt_Info_Search_TextBox.Text == "... ابحث")
            {
                Receipt_Info_Search_TextBox.Text = "";
                Receipt_Search_Cancel_Buttom.Visible = true;
            }
        }

        private void Receipt_Info_Search_TextBox_Leave(object sender, EventArgs e)
        {
            if (Receipt_Info_Search_TextBox.Text == "")
            {
                Receipt_Info_Search_TextBox.Text = "... ابحث";
                Receipt_Search_Cancel_Buttom.Visible = false;
            }
        }

        #endregion

        #region Account Panel

        // Add New Account Buttom
        //

        private void Add_Account_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                GUI_Function = new GUI_Function();
                GUI_Function.Account_TextBox_Defult(ref Account_Info, ref Account_Receipt_Table, ref Account_Detail_Table);

                Account Account = new Account();
                Account_Number_TextBox.Text = Account.New_Id();
                Account_Number_TextBox.ForeColor = Color.DimGray;

                Add_Account_Buttom.Visible = false;
                Account_Receipt_Info_Buttom.Visible = false;
                Menu_Account_Butttom.Visible = false;
                Account_Menu_Panel.Visible = false;

                Account_Next_Buttom.Visible = false;
                Account_Back_Buttom.Visible = false;
                Account_First_Buttom.Visible = false;
                Account_Last_Buttom.Visible = false;

                Account_Info_Search_TextBox.Visible = false;

                Account_Save_Buttom.Visible = true;
                Account_Cancel_Buttom.Visible = true;
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Account_Menu_Add_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                GUI_Function = new GUI_Function();
                GUI_Function.Account_TextBox_Defult(ref Account_Info, ref Account_Receipt_Table, ref Account_Detail_Table);

                Account Account = new Account();
                Account_Number_TextBox.Text = Account.New_Id();
                Account_Number_TextBox.ForeColor = Color.DimGray;

                Add_Account_Buttom.Visible = false;
                Account_Receipt_Info_Buttom.Visible = false;
                Menu_Account_Butttom.Visible = false;
                Account_Menu_Panel.Visible = false;

                Account_Next_Buttom.Visible = false;
                Account_Back_Buttom.Visible = false;
                Account_First_Buttom.Visible = false;
                Account_Last_Buttom.Visible = false;

                Account_Info_Search_TextBox.Visible = false;

                Account_Save_Buttom.Visible = true;
                Account_Cancel_Buttom.Visible = true;
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Account Menu Panel  Events 
        //

        private void Menu_Account_Butttom_Click(object sender, EventArgs e)
        {
            if (Account_Menu_Panel.Visible == false)
            {
                Account_Menu_Panel.Visible = true;

                Account_Menu_Panel.Left = 2;
                Account_Menu_Panel.Top = 0;
            }
            else
            {
                Account_Menu_Panel.Visible = false;
            }
        }

        private void Account_Info_Panel_Click(object sender, EventArgs e)
        {
            Account_Menu_Panel.Visible = false;
        }

        // Account Inf and Account Receipts Info Events
        //

        private void Account_Receipt_Info_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref Main_Panels, ref Account_Panel);
            GUI_Function.Panel_Visible(ref Sub_Panel, ref Account_Receipt_Info_Panel);

            Account_Receipt_Info_Buttom.ForeColor = Color.White;

            Add_Account_Buttom.Visible = false;
            Account_Next_Buttom.Visible = false;
            Account_Back_Buttom.Visible = false;
            Account_First_Buttom.Visible = false;
            Account_Last_Buttom.Visible = false;
            Account_Info_Search_TextBox.Visible = false;
        }

        private void Account_Back_Info_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref Main_Panels, ref Account_Panel);
            GUI_Function.Panel_Visible(ref Sub_Panel, ref Account_Info_Panel);

            Add_Account_Buttom.Visible = true;
            Account_Next_Buttom.Visible = true;
            Account_Back_Buttom.Visible = true;
            Account_First_Buttom.Visible = true;
            Account_Last_Buttom.Visible = true;
            Account_Info_Search_TextBox.Visible = true;
        }
        
        // Account Basic Opreation Save - Cancel - Delete
        //

        private void Account_Save_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Account Account = new Account();
                Account.Set_Account_Info(Account_Info, Account_Detail_Table, ref Account);

                Process Process = new Process();
                Process.Save_Process("Save", "Account", Account.Get_Number(), User_Name.Text);

                Account.Sql_Insert(Account);
                Account.FireBase_Insert(Account);

                Add_Account_Buttom.Visible = true;
                Account_Receipt_Info_Buttom.Visible = true;
                Menu_Account_Butttom.Visible = true;

                Account_Next_Buttom.Visible = true;
                Account_Back_Buttom.Visible = true;
                Account_First_Buttom.Visible = true;
                Account_Last_Buttom.Visible = true;

                Account_Info_Search_TextBox.Visible = true;

                Account_Save_Buttom.Visible = false;
                Account_Cancel_Buttom.Visible = false;

                List<Account> Accounts = Account.Sql_Select();

                int Number_Receipt = Accounts.Count();

                Currunt_Index = Number_Receipt - 1;

                Account.Get_Account_Info(ref Account_Info, ref Account_Detail_Table, ref Account_Receipt_Table, Accounts[Currunt_Index]);
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Account_Cancel_Buttom_Click(object sender, EventArgs e)
        {
            Add_Account_Buttom.Visible = true;
            Account_Receipt_Info_Buttom.Visible = true;
            Menu_Account_Butttom.Visible = true;

            Account_Next_Buttom.Visible = true;
            Account_Back_Buttom.Visible = true;
            Account_First_Buttom.Visible = true;
            Account_Last_Buttom.Visible = true;

            Account_Info_Search_TextBox.Visible = true;

            Account_Save_Buttom.Visible = false;
            Account_Cancel_Buttom.Visible = false;

            Account Account = new Account();

            List<Account> Accounts = Account.Sql_Select();

            if (Accounts.Count == 0)
            {
                GUI_Function = new GUI_Function();
                GUI_Function.Account_TextBox_Defult(ref Account_Info, ref Receipt_Table, ref Account_Detail_Table);
            }
            else
            {
                Account.Get_Account_Info(ref Account_Info, ref Account_Detail_Table, ref Account_Receipt_Table, Accounts[Currunt_Index]);
            }
            

           

            
        }

        private void Account_Menu_Delete_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Account_Menu_Panel.Visible = false;

                Account_Receipt_Info_Buttom.Visible = true;

                DialogResult Result = MessageBox.Show("هل انت متاكد من حذف الحساب الحالي", "حذف الحساب الحالي", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                Account Deleted_Account = new Account();

                if (Result == DialogResult.Yes)
                {
                    Process Process = new Process();
                    Process.Save_Process("Delete", "Account", Account_Number_TextBox.Text, User_Name.Text);

                    Deleted_Account.Set_Account_Info(Account_Info, Account_Detail_Table, ref Deleted_Account);
                    Deleted_Account.FireBase_Delete(Deleted_Account);
                    Deleted_Account.Sql_Delete(Account_Number_TextBox.Text);

                    List<Account> Accounts = Deleted_Account.Sql_Select();

                    int Number_Account = Accounts.Count();

                    if (Number_Account > 0)
                    {
                        if (Currunt_Index > 1)
                        {
                            Currunt_Index = Currunt_Index - 1;
                            Deleted_Account.Get_Account_Info(ref Account_Info, ref Account_Detail_Table, ref Account_Receipt_Table, Accounts[Currunt_Index]);
                        }
                        else
                        {
                            Currunt_Index = 0;
                            Deleted_Account.Get_Account_Info(ref Account_Info, ref Account_Detail_Table, ref Account_Receipt_Table, Accounts[Currunt_Index]);
                        }
                    }
                    else
                    {
                        GUI_Function = new GUI_Function();
                        GUI_Function.Account_TextBox_Defult(ref Account_Info, ref Account_Receipt_Table, ref Account_Detail_Table);
                    }
                }
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        // Account Search Event
        //

        private void Account_Info_Search_TextBox_TextChanged(object sender, EventArgs e)
        {
            Account Account = new Account();

            List<Account> All_Account = Account.Sql_Search(Account_Info_Search_TextBox.Text);

            int Number_Account = All_Account.Count();

            if (Number_Account > 0)
            {
                Currunt_Index = 0;
                Account.Get_Account_Info(ref Account_Info, ref Account_Detail_Table, ref Account_Receipt_Table, All_Account[Currunt_Index]);
            }
        }

        private void Account_Search_Cancel_Buttom_Click(object sender, EventArgs e)
        {
            Account_Info_Search_TextBox.Text = "... ابحث";

            Account_Search_Cancel_Buttom.Visible = false;

            Account Account = new Account();

            List<Account> Accounts = Account.Sql_Select();

            int Number_Account = Accounts.Count();

            Currunt_Index = 0;

            Account.Get_Account_Info(ref Account_Info, ref Account_Detail_Table, ref Account_Receipt_Table, Accounts[Currunt_Index]);
        }

        // Account Move Bettwen Record Opreation Next - Back - First - Last
        //

        private void Account_Next_Buttom_Click(object sender, EventArgs e)
        {
            Account Account = new Account();

            List<Account> Accounts = Account.Sql_Select();

            int Number_Account = Accounts.Count();

            if (Currunt_Index + 1 < Number_Account)
            {
                Currunt_Index = Currunt_Index + 1;
                Account.Get_Account_Info(ref Account_Info, ref Account_Detail_Table, ref Account_Receipt_Table, Accounts[Currunt_Index]);
            }
        }

        private void Account_Back_Buttom_Click(object sender, EventArgs e)
        {
            Account Account = new Account();

            List<Account> Accounts = Account.Sql_Select();

            int Number_Account = Accounts.Count();

            if (Currunt_Index - 1 > -1)
            {
                Currunt_Index = Currunt_Index - 1;
                Account.Get_Account_Info(ref Account_Info, ref Account_Detail_Table, ref Account_Receipt_Table, Accounts[Currunt_Index]);
            }
        }

        private void Account_First_Buttom_Click(object sender, EventArgs e)
        {
            Account Account = new Account();

            List<Account> Accounts = Account.Sql_Select();

            int Number_Account = Accounts.Count();

            Currunt_Index = 0;

            Account.Get_Account_Info(ref Account_Info, ref Account_Detail_Table, ref Account_Receipt_Table, Accounts[Currunt_Index]);
        }

        private void Account_Last_Buttom_Click(object sender, EventArgs e)
        {
            Account Account = new Account();

            List<Account> Accounts = Account.Sql_Select();

            int Number_Account = Accounts.Count();

            Currunt_Index = Number_Account - 1;

            Account.Get_Account_Info(ref Account_Info, ref Account_Detail_Table, ref Account_Receipt_Table, Accounts[Currunt_Index]);
        }

        // Account Text Box Events Enter - Leave
        //

        private void Account_Number_TextBox_Enter(object sender, EventArgs e)
        {
            if (Account_Number_TextBox.Text == "0000000000")
            {
                Account_Number_TextBox.Text = "";
                Account_Number_TextBox.ForeColor = Color.Black;
            }
        }

        private void Account_Number_TextBox_Leave(object sender, EventArgs e)
        {
            if (Account_Number_TextBox.Text == "")
            {
                Account_Number_TextBox.Text = "0000000000";
                Account_Number_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Account_Name_TextBox_Enter(object sender, EventArgs e)
        {
            if (Account_Name_TextBox.Text == "اسم الحساب")
            {
                Account_Name_TextBox.Text = "";
                Account_Name_TextBox.ForeColor = Color.Black;
            }
        }

        private void Account_Name_TextBox_Leave(object sender, EventArgs e)
        {
            if (Account_Name_TextBox.Text == "")
            {
                Account_Name_TextBox.Text = "اسم الحساب";
                Account_Name_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Account_Type_TextBox_Enter(object sender, EventArgs e)
        {
            if (Account_Type_TextBox.Text == "فردي / مشترك")
            {
                Account_Type_TextBox.Text = "";
                Account_Type_TextBox.ForeColor = Color.Black;
            }
        }

        private void Account_Type_TextBox_Leave(object sender, EventArgs e)
        {
            if (Account_Type_TextBox.Text == "")
            {
                Account_Type_TextBox.Text = "فردي / مشترك";
                Account_Type_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Account_Conect1_TextBox_Enter(object sender, EventArgs e)
        {
            if (Account_Conect1_TextBox.Text == "022 - 01")
            {
                Account_Conect1_TextBox.Text = "";
                Account_Conect1_TextBox.ForeColor = Color.Black;
            }
        }

        private void Account_Conect1_TextBox_Leave(object sender, EventArgs e)
        {
            if (Account_Conect1_TextBox.Text == "")
            {
                Account_Conect1_TextBox.Text = "022 - 01";
                Account_Conect1_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Account_Conect2_TextBox_Enter(object sender, EventArgs e)
        {
            if (Account_Conect2_TextBox.Text == "022 - 01")
            {
                Account_Conect2_TextBox.Text = "";
                Account_Conect2_TextBox.ForeColor = Color.Black;
            }
        }

        private void Account_Conect2_TextBox_Leave(object sender, EventArgs e)
        {
            if (Account_Conect2_TextBox.Text == "")
            {
                Account_Conect2_TextBox.Text = "022 - 01";
                Account_Conect2_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Account_Whats_TextBox_Enter(object sender, EventArgs e)
        {
            if (Account_Whats_TextBox.Text == "022 - 01")
            {
                Account_Whats_TextBox.Text = "";
                Account_Whats_TextBox.ForeColor = Color.Black;
            }
        }

        private void Account_Whats_TextBox_Leave(object sender, EventArgs e)
        {
            if (Account_Whats_TextBox.Text == "")
            {
                Account_Whats_TextBox.Text = "022 - 01";
                Account_Whats_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Account_Total_TextBox_Enter(object sender, EventArgs e)
        {
            if (Account_Total_TextBox.Text == "000 ج.م")
            {
                Account_Total_TextBox.Text = "";
                Account_Total_TextBox.ForeColor = Color.Black;
            }
        }

        private void Account_Total_TextBox_Leave(object sender, EventArgs e)
        {
            if (Account_Total_TextBox.Text == "")
            {
                Account_Total_TextBox.Text = "000 ج.م";
                Account_Total_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Account_Amount_TextBox_Enter(object sender, EventArgs e)
        {
            if (Account_Amount_TextBox.Text == "000 ج.م")
            {
                Account_Amount_TextBox.Text = "";
                Account_Amount_TextBox.ForeColor = Color.Black;
            }
        }

        private void Account_Amount_TextBox_Leave(object sender, EventArgs e)
        {
            if (Account_Amount_TextBox.Text == "")
            {
                Account_Amount_TextBox.Text = "000 ج.م";
                Account_Amount_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Account_Remender_TextBox_Enter(object sender, EventArgs e)
        {
            if (Account_Remender_TextBox.Text == "000 ج.م")
            {
                Account_Remender_TextBox.Text = "";
                Account_Remender_TextBox.ForeColor = Color.Black;
            }
        }

        private void Account_Remender_TextBox_Leave(object sender, EventArgs e)
        {
            if (Account_Remender_TextBox.Text == "")
            {
                Account_Remender_TextBox.Text = "000 ج.م";
                Account_Remender_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Account_Info_Search_TextBox_Enter(object sender, EventArgs e)
        {
            if (Account_Info_Search_TextBox.Text == "... ابحث")
            {
                Account_Info_Search_TextBox.Text = "";
                Account_Search_Cancel_Buttom.Visible = true;
            }
        }

        private void Account_Info_Search_TextBox_Leave(object sender, EventArgs e)
        {
            if (Account_Info_Search_TextBox.Text == "")
            {
                Account_Info_Search_TextBox.Text = "... ابحث";
                Account_Search_Cancel_Buttom.Visible = false;
            }
        }

        #endregion

        #region Employee Panel

        // Add New Employee Buttom
        //

        private void Employee_Add_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Employee_Add_Buttom.Visible = false;

                Employee_Next_Buttom.Visible = false;
                Employee_Back_Buttom.Visible = false;
                Employee_First_Buttom.Visible = false;
                Employee_Last_Buttom.Visible = false;

                GUI_Function = new GUI_Function();
                GUI_Function.Employee_TextBox_Defult(ref Employee_Info);

                Employee Employee = new Employee();
                Employee_Number_TextBox.Text = Employee.New_Id();
                Employee_Number_TextBox.ForeColor = Color.DimGray;

                Employee_Menu_Buttom.Visible = false;
                Employee_Menu_Panel.Visible = false;

                Employee_Save_Buttom.Visible = true;
                Employee_Cancel_Buttom.Visible = true;
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Employee_Menu_Add_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Employee_Add_Buttom.Visible = false;

                Employee_Next_Buttom.Visible = false;
                Employee_Back_Buttom.Visible = false;
                Employee_First_Buttom.Visible = false;
                Employee_Last_Buttom.Visible = false;

                GUI_Function = new GUI_Function();
                GUI_Function.Employee_TextBox_Defult(ref Employee_Info);

                Employee Employee = new Employee();
                Employee_Number_TextBox.Text = Employee.New_Id();

                Employee_Number_TextBox.ForeColor = Color.DimGray;
                Employee_Menu_Buttom.Visible = false;
                Employee_Menu_Panel.Visible = false;

                Employee_Save_Buttom.Visible = true;
                Employee_Cancel_Buttom.Visible = true;
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Basic Opreation Save - Cancel - Delete Buttom
        //

        private void Employee_Save_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Employee New_Employee = new Employee();
                New_Employee.Set_Employee_Info(Employee_Info, ref New_Employee);

                Process Process = new Process();
                Process.Save_Process("Save", "Employee", New_Employee.Get_Number(), User_Name.Text);

                New_Employee.Sql_Insert(New_Employee);
                New_Employee.FireBase_Insert(New_Employee);

                List<Employee> All_Employee = New_Employee.Sql_Select();
                New_Employee.Get_Employee_Info(ref Employee_Info, All_Employee[All_Employee.Count() - 1]);
                Currunt_Index = All_Employee.Count() - 1;


                Employee_Add_Buttom.Visible = true;

                Employee_Next_Buttom.Visible = true;
                Employee_Back_Buttom.Visible = true;
                Employee_First_Buttom.Visible = true;
                Employee_Last_Buttom.Visible = true;

                Employee_Menu_Buttom.Visible = true;

                Employee_Save_Buttom.Visible = false;
                Employee_Cancel_Buttom.Visible = false;
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Employee_Cancel_Buttom_Click(object sender, EventArgs e)
        {

            Employee Employee = new Employee();
            List<Employee> All_Employee = Employee.Sql_Select();
            if (All_Employee.Count == 0)
            {
                GUI_Function = new GUI_Function();
                GUI_Function.Employee_TextBox_Defult(ref Employee_Info);
            }
            else
            {
                Employee.Get_Employee_Info(ref Employee_Info, All_Employee[Currunt_Index]);
            }

            Employee_Add_Buttom.Visible = true;

            Employee_Next_Buttom.Visible = true;
            Employee_Back_Buttom.Visible = true;
            Employee_First_Buttom.Visible = true;
            Employee_Last_Buttom.Visible = true;

            Employee_Menu_Buttom.Visible = true;

            Employee_Save_Buttom.Visible = false;
            Employee_Cancel_Buttom.Visible = false;
        }

        private void Employee_Menu_Delete_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Employee_Menu_Panel.Visible = false;

                DialogResult Result = MessageBox.Show("هل انت متاكد من حذف الموظف الحالي", "حذف الموظف الحالي", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                Employee Deleted_Employee = new Employee();

                if (Result == DialogResult.Yes)
                {
                    Process Process = new Process();
                    Process.Save_Process("Delete", "Employee", Employee_Number_TextBox.Text, User_Name.Text);

                    Deleted_Employee.FireBase_Delete(Employee_Number_TextBox.Text);
                    Deleted_Employee.Sql_Delete(Employee_Number_TextBox.Text);

                    List<Employee> Employees = Deleted_Employee.Sql_Select();

                    int Number_Employee = Employees.Count();

                    if (Number_Employee > 0)
                    {
                        if (Currunt_Index > 1)
                        {
                            Currunt_Index = Currunt_Index - 1;
                            Deleted_Employee.Get_Employee_Info(ref Employee_Info, Employees[Currunt_Index]);
                        }
                        else
                        {
                            Currunt_Index = 0;
                            Deleted_Employee.Get_Employee_Info(ref Receipt_Info, Employees[Currunt_Index]);
                        }
                    }
                    else
                    {
                        GUI_Function = new GUI_Function();
                        GUI_Function.Employee_TextBox_Defult(ref Employee_Info);
                    }
                }
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Move Bettwen Records Opreation Next - Back - First - Last
        //

        private void Employee_Back_Buttom_Click(object sender, EventArgs e)
        {
            Employee Employee = new Employee();

            List<Employee> Employees = Employee.Sql_Select();

            int Number_Employee = Employees.Count();

            if (Currunt_Index - 1 > -1)
            {
                Currunt_Index = Currunt_Index - 1;
                Employee.Get_Employee_Info(ref Employee_Info, Employees[Currunt_Index]);
            }



        }

        private void Employee_Next_Buttom_Click(object sender, EventArgs e)
        {
            Employee Employee = new Employee();

            List<Employee> Employees = Employee.Sql_Select();

            int Number_Employee = Employees.Count();

            if (Currunt_Index + 1 < Number_Employee)
            {
                Currunt_Index = Currunt_Index + 1;
                Employee.Get_Employee_Info(ref Employee_Info, Employees[Currunt_Index]);
            }
        }

        private void Employee_First_Buttom_Click(object sender, EventArgs e)
        {
            Employee Employee = new Employee();

            List<Employee> Employees = Employee.Sql_Select();

            int Number_Employee = Employees.Count();

            Currunt_Index = 0;

            Employee.Get_Employee_Info(ref Employee_Info, Employees[Currunt_Index]);
        }

        private void Employee_Last_Buttom_Click(object sender, EventArgs e)
        {
            Employee Employee = new Employee();

            List<Employee> Employees = Employee.Sql_Select();

            int Number_Employee = Employees.Count();

            Currunt_Index = Number_Employee - 1;

            Employee.Get_Employee_Info(ref Employee_Info, Employees[Currunt_Index]);
        }

        // Employee Menu Events
        //

        private void Employee_Menu_Buttom_Click(object sender, EventArgs e)
        {
            if (Employee_Menu_Panel.Visible == false)
            {
                Employee_Menu_Panel.Visible = true;
                Employee_Menu_Panel.Left = 2;
                Employee_Menu_Panel.Top = 42;
            }
            else
            {
                Employee_Menu_Panel.Visible = false;
            }
        }

        private void Employee_Panel_Click(object sender, EventArgs e)
        {
            Employee_Menu_Panel.Visible = false;
        }

        // Employee Text Box Events Enter - Leave
        //

        private void Employee_Number_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Number_TextBox.Text == "000000000")
            {
                Employee_Number_TextBox.Text = "";
                Employee_Number_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Number_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Number_TextBox.Text == "")
            {
                Employee_Number_TextBox.Text = "000000000";
                Employee_Number_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Name_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Name_TextBox.Text == "اسم الموظف")
            {
                Employee_Name_TextBox.Text = "";
                Employee_Name_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Name_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Name_TextBox.Text == "")
            {
                Employee_Name_TextBox.Text = "اسم الموظف";
                Employee_Name_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employye_Connect1_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employye_Connect1_TextBox.Text == "022-0")
            {
                Employye_Connect1_TextBox.Text = "";
                Employye_Connect1_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employye_Connect1_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employye_Connect1_TextBox.Text == "")
            {
                Employye_Connect1_TextBox.Text = "022-0";
                Employye_Connect1_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Connect2_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Connect2_TextBox.Text == "022-0")
            {
                Employee_Connect2_TextBox.Text = "";
                Employee_Connect2_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Connect2_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Connect2_TextBox.Text == "")
            {
                Employee_Connect2_TextBox.Text = "022-0";
                Employee_Connect2_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Childern_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Childern_TextBox.Text == "لا يوجد / اسم الحساب")
            {
                Employee_Childern_TextBox.Text = "";
                Employee_Childern_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Childern_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Childern_TextBox.Text == "")
            {
                Employee_Childern_TextBox.Text = "لا يوجد / اسم الحساب";
                Employee_Childern_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Type_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Type_TextBox.Text == "مدرس صف / اختصاص")
            {
                Employee_Type_TextBox.Text = "";
                Employee_Type_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Type_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Type_TextBox.Text == "")
            {
                Employee_Type_TextBox.Text = "مدرس صف / اختصاص";
                Employee_Type_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Class_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Class_TextBox.Text == "ابتدائي / اعدادي")
            {
                Employee_Class_TextBox.Text = "";
                Employee_Class_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Class_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Class_TextBox.Text == "")
            {
                Employee_Class_TextBox.Text = "ابتدائي / اعدادي";
                Employee_Class_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Number_Hour_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Number_Hour_TextBox.Text == "اسبوعيا")
            {
                Employee_Number_Hour_TextBox.Text = "";
                Employee_Number_Hour_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Number_Hour_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Number_Hour_TextBox.Text == "")
            {
                Employee_Number_Hour_TextBox.Text = "اسبوعيا";
                Employee_Number_Hour_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Detail_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Detail_TextBox.Text == "التفاصيل")
            {
                Employee_Detail_TextBox.Text = "";
                Employee_Detail_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Detail_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Detail_TextBox.Text == "")
            {
                Employee_Detail_TextBox.Text = "التفاصيل";
                Employee_Detail_TextBox.ForeColor = Color.Silver;
            }
        }

        #endregion 

        #region Employee Receipt Panel

        // Employee Receipts Basic Opreation Save - Cancel
        //

        private void Employee_Receipt_Save_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Employee_Receipt Employee_Receipt = new Employee_Receipt();
                Employee_Receipt.Set_Employee_Receipt_Info(Employee_Receipt_Info, ref Employee_Receipt);

                Process Process = new Process();
                Process.Save_Process("Save", "Salary", Employee_Receipt.Get_Number(), User_Name.Text);

                Employee_Receipt.Sql_Insert(Employee_Receipt);
                Employee_Receipt.FireBase_Insert(Employee_Receipt);

                GUI_Function = new GUI_Function();
                GUI_Function.Panel_Visible(ref All_Panels, ref Safe_Panel);
                GUI_Function.Employee_Receipt_TextBox_Defult(ref Employee_Receipt_Info);
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Employee_Receipt_Cancel_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Safe_Panel);
            GUI_Function.Employee_Receipt_TextBox_Defult(ref Employee_Receipt_Info);
        }

        // Employee Receipts Back to Safe Panel
        //

        private void Employee_Receipt_Back_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Safe_Panel);
            GUI_Function.Employee_Receipt_TextBox_Defult(ref Employee_Receipt_Info);
        }

        // Employee Receipts Text Box Events Enter - Leave
        //

        private void Employee_Receipt_Number_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Receipt_Number_TextBox.Text == "000000000")
            {
                Employee_Receipt_Number_TextBox.Text = "";
                Employee_Receipt_Number_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Receipt_Number_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Receipt_Number_TextBox.Text == "")
            {
                Employee_Receipt_Number_TextBox.Text = "000000000";
                Employee_Receipt_Number_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Receipt_Book_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Receipt_Book_TextBox.Text == "A19 or B19")
            {
                Employee_Receipt_Book_TextBox.Text = "";
                Employee_Receipt_Book_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Receipt_Book_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Receipt_Book_TextBox.Text == "")
            {
                Employee_Receipt_Book_TextBox.Text = "A19 or B19";
                Employee_Receipt_Book_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Receipt_Owner_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Receipt_Owner_TextBox.Text == "اسم المسؤول")
            {
                Employee_Receipt_Owner_TextBox.Text = "";
                Employee_Receipt_Owner_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Receipt_Owner_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Receipt_Owner_TextBox.Text == "")
            {
                Employee_Receipt_Owner_TextBox.Text = "اسم المسؤول";
                Employee_Receipt_Owner_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Receipt_Date_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Receipt_Date_TextBox.Text == "01/01/1990")
            {
                Employee_Receipt_Date_TextBox.Text = "";
                Employee_Receipt_Date_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Receipt_Date_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Receipt_Date_TextBox.Text == "")
            {
                Employee_Receipt_Date_TextBox.Text = "01/01/1990";
                Employee_Receipt_Date_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Receipt_Name_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Receipt_Name_TextBox.Text == "اسم الموظف")
            {
                Employee_Receipt_Name_TextBox.Text = "";
                Employee_Receipt_Name_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Receipt_Name_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Receipt_Name_TextBox.Text == "")
            {
                Employee_Receipt_Name_TextBox.Text = "اسم الموظف";
                Employee_Receipt_Name_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Receipt_Sallary_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Receipt_Sallary_TextBox.Text == "000 L.E")
            {
                Employee_Receipt_Sallary_TextBox.Text = "";
                Employee_Receipt_Sallary_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Receipt_Sallary_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Receipt_Sallary_TextBox.Text == "")
            {
                Employee_Receipt_Sallary_TextBox.Text = "000 L.E";
                Employee_Receipt_Sallary_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Receipt_Discount_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Receipt_Discount_TextBox.Text == "000 L.E")
            {
                Employee_Receipt_Discount_TextBox.Text = "";
                Employee_Receipt_Discount_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Receipt_Discount_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Receipt_Discount_TextBox.Text == "")
            {
                Employee_Receipt_Discount_TextBox.Text = "000 L.E";
                Employee_Receipt_Discount_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Receipt_Total_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Receipt_Total_TextBox.Text == "000 L.E")
            {
                Employee_Receipt_Total_TextBox.Text = "";
                Employee_Receipt_Total_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Receipt_Total_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Receipt_Total_TextBox.Text == "")
            {
                Employee_Receipt_Total_TextBox.Text = "000 L.E";
                Employee_Receipt_Total_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Employee_Receipt_Detail_TextBox_Enter(object sender, EventArgs e)
        {
            if (Employee_Receipt_Detail_TextBox.Text == "المزيد من التفاصيل حول حساب المرتب")
            {
                Employee_Receipt_Detail_TextBox.Text = "";
                Employee_Receipt_Detail_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Employee_Receipt_Detail_TextBox_Leave(object sender, EventArgs e)
        {
            if (Employee_Receipt_Detail_TextBox.Text == "")
            {
                Employee_Receipt_Detail_TextBox.Text = "المزيد من التفاصيل حول حساب المرتب";
                Employee_Receipt_Detail_TextBox.ForeColor = Color.Silver;
            }
        }

        #endregion

        #region Movement Receipt Panel

        // Movement Receipts Back To Safe Panel Buttom
        //

        private void Movement_Receipt_Back_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Safe_Panel);
        }

        // Movement Receipt Basic Opreation Save - Cancel
        //

        private void Movement_Receipt_Cancel_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Safe_Panel);
            GUI_Function.Movement_Receipt_TextBox_Defult(ref Movement_Receipt_Info);
        }

        private void Movement_Receipt_Save_Buttom_Click(object sender, EventArgs e)
        {
            bool Connection = Cheak_Internet_Connection();

            if (Connection == true)
            {
                Movement_Receipt Movement_Receipt = new Movement_Receipt();
                Movement_Receipt.Set_Movement_Receipt_Info(Movement_Receipt_Info, ref Movement_Receipt);

                Process Process = new Process();
                Process.Save_Process("Save", "Movement", Movement_Receipt.Get_Number(), User_Name.Text);

                Movement_Receipt.Sql_Insert(Movement_Receipt);
                Movement_Receipt.FireBase_Insert(Movement_Receipt);

                GUI_Function = new GUI_Function();
                GUI_Function.Panel_Visible(ref All_Panels, ref Safe_Panel);
                GUI_Function.Movement_Receipt_TextBox_Defult(ref Movement_Receipt_Info);
            }
            else
            {
                MessageBox.Show("تحقق من الاتصال بالانترنت ! عرض البيانات فقط", "فشل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Movement Receipt Text Box Events Enter - Leave And Combo Box Events
        //

        private void Movement_Receipt_Number_TextBox_Enter(object sender, EventArgs e)
        {
            if (Movement_Receipt_Number_TextBox.Text == "2018190001")
            {
                Movement_Receipt_Number_TextBox.Text = "";
                Movement_Receipt_Number_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Movement_Receipt_Number_TextBox_Leave(object sender, EventArgs e)
        {
            if (Movement_Receipt_Number_TextBox.Text == "")
            {
                Movement_Receipt_Number_TextBox.Text = "2018190001";
                Movement_Receipt_Number_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Movement_Receipt_Book_TextBox_Enter(object sender, EventArgs e)
        {
            if (Movement_Receipt_Book_TextBox.Text == "A19 or B19")
            {
                Movement_Receipt_Book_TextBox.Text = "";
                Movement_Receipt_Book_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Movement_Receipt_Book_TextBox_Leave(object sender, EventArgs e)
        {
            if (Movement_Receipt_Book_TextBox.Text == "")
            {
                Movement_Receipt_Book_TextBox.Text = "A19 or B19";
                Movement_Receipt_Book_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Movement_Receipt_Owner_TextBox_Enter(object sender, EventArgs e)
        {
            if (Movement_Receipt_Owner_TextBox.Text == "اسم المسؤول")
            {
                Movement_Receipt_Owner_TextBox.Text = "";
                Movement_Receipt_Owner_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Movement_Receipt_Owner_TextBox_Leave(object sender, EventArgs e)
        {
            if (Movement_Receipt_Owner_TextBox.Text == "")
            {
                Movement_Receipt_Owner_TextBox.Text = "اسم المسؤول";
                Movement_Receipt_Owner_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Movement_Receipt_Amount_TextBox_Enter(object sender, EventArgs e)
        {
            if (Movement_Receipt_Amount_TextBox.Text == "000 L.E")
            {
                Movement_Receipt_Amount_TextBox.Text = "";
                Movement_Receipt_Amount_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Movement_Receipt_Amount_TextBox_Leave(object sender, EventArgs e)
        {
            if (Movement_Receipt_Amount_TextBox.Text == "")
            {
                Movement_Receipt_Amount_TextBox.Text = "000 L.E";
                Movement_Receipt_Amount_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Movement_Receipt_Date_TextBox_Enter(object sender, EventArgs e)
        {
            if (Movement_Receipt_Date_TextBox.Text == "01/01/1990")
            {
                Movement_Receipt_Date_TextBox.Text = "";
                Movement_Receipt_Date_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Movement_Receipt_Date_TextBox_Leave(object sender, EventArgs e)
        {
            if (Movement_Receipt_Date_TextBox.Text == "")
            {
                Movement_Receipt_Date_TextBox.Text = "01/01/1990";
                Movement_Receipt_Date_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Movement_Receipt_Detail_TextBox_Enter(object sender, EventArgs e)
        {
            if (Movement_Receipt_Detail_TextBox.Text == "تفاصيل")
            {
                Movement_Receipt_Detail_TextBox.Text = "";
                Movement_Receipt_Detail_TextBox.ForeColor = Color.DimGray;
            }
        }

        private void Movement_Receipt_Detail_TextBox_Leave(object sender, EventArgs e)
        {
            if (Movement_Receipt_Detail_TextBox.Text == "")
            {
                Movement_Receipt_Detail_TextBox.Text = "تفاصيل";
                Movement_Receipt_Detail_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Movement_Receipt_Type_TextBox_Enter(object sender, EventArgs e)
        {
            if (Movement_Receipt_Type_TextBox.Text == "واردات / مصروفات")
            {
                Movement_Receipt_Type_TextBox.Text = "";
                Movement_Receipt_Type_TextBox.ForeColor = Color.DimGray;

                Movement_Receipt_Type_Combo.Visible = true;
                Movement_Receipt_Type_Combo.Left = 607;
                Movement_Receipt_Type_Combo.Top = 273;
            }
        }

        private void Movement_Receipt_Type_TextBox_Leave(object sender, EventArgs e)
        {
            if (Movement_Receipt_Type_TextBox.Text == "")
            {
                Movement_Receipt_Type_TextBox.Text = "واردات / مصروفات";
                Movement_Receipt_Type_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Movement_Receipt_Type_Combo_Import_Click(object sender, EventArgs e)
        {
            Movement_Receipt_Type_TextBox.Text = "واردات";
            Movement_Receipt_Type_Combo.Visible = false;
            Movement_Receipt_Type_TextBox.ForeColor = Color.DimGray;
        }

        private void Movement_Receipt_Combo_Expenses_Click(object sender, EventArgs e)
        {
            Movement_Receipt_Type_TextBox.Text = "مصروفات";
            Movement_Receipt_Type_Combo.Visible = false;
            Movement_Receipt_Type_TextBox.ForeColor = Color.DimGray;
        }

        private void Movement_Receipt_Kind_TextBox_Enter(object sender, EventArgs e)
        {
            if (Movement_Receipt_Kind_TextBox.Text == "الصنف")
            {
                Movement_Receipt_Kind_TextBox.Text = "";
                Movement_Receipt_Kind_TextBox.ForeColor = Color.DimGray;

                if (Movement_Receipt_Type_TextBox.Text == "مصروفات")
                {
                    Movement_Receipt_Kind_Combo.Visible = true;
                    Movement_Receipt_Kind_Combo.Left = 189;
                    Movement_Receipt_Kind_Combo.Top = 192;
                }
            }
        }

        private void Movement_Receipt_Kind_TextBox_Leave(object sender, EventArgs e)
        {
            if (Movement_Receipt_Kind_TextBox.Text == "")
            {
                Movement_Receipt_Kind_TextBox.Text = "الصنف";
                Movement_Receipt_Kind_TextBox.ForeColor = Color.Silver;
            }
        }

        private void Movement_Receipt_Combo_Mantin_Click(object sender, EventArgs e)
        {
            Movement_Receipt_Kind_TextBox.Text = "صيانة";
            Movement_Receipt_Kind_Combo.Visible = false;
            Movement_Receipt_Kind_TextBox.ForeColor = Color.DimGray;
        }

        private void Movement_Receipt_Combo_Tool_Click(object sender, EventArgs e)
        {
            Movement_Receipt_Kind_TextBox.Text = "قرطاسية";
            Movement_Receipt_Kind_Combo.Visible = false;
            Movement_Receipt_Kind_TextBox.ForeColor = Color.DimGray;
        }

        private void Movement_Receipt_Combo_Transportation_Click(object sender, EventArgs e)
        {
            Movement_Receipt_Kind_TextBox.Text = "مواصلات";
            Movement_Receipt_Kind_Combo.Visible = false;
            Movement_Receipt_Kind_TextBox.ForeColor = Color.DimGray;
        }

        private void Movement_Receipt_Combo_Rent_Click(object sender, EventArgs e)
        {
            Movement_Receipt_Kind_TextBox.Text = "ايجار";
            Movement_Receipt_Kind_Combo.Visible = false;
            Movement_Receipt_Kind_TextBox.ForeColor = Color.DimGray;
        }

        private void Movment_Receipt_Panel_Click(object sender, EventArgs e)
        {
            Movement_Receipt_Type_Combo.Visible = false;
            Movement_Receipt_Kind_Combo.Visible = false;
        }

        private void Movement_Receipt_Type_Buttom_Click(object sender, EventArgs e)
        {
            if (Movement_Receipt_Type_Combo.Visible == false)
            {
                Movement_Receipt_Type_Combo.Visible = true;
                Movement_Receipt_Type_Combo.Left = 607;
                Movement_Receipt_Type_Combo.Top = 273;
            }
            else
            {
                Movement_Receipt_Type_Combo.Visible = false;
            }
        }

        private void Movement_Receipt_Kind_Buttom_Click(object sender, EventArgs e)
        {
            if (Movement_Receipt_Kind_Combo.Visible == false)
            {
                if (Movement_Receipt_Type_TextBox.Text == "مصروفات")
                {
                    Movement_Receipt_Kind_Combo.Visible = true;
                    Movement_Receipt_Kind_Combo.Left = 189;
                    Movement_Receipt_Kind_Combo.Top = 192;
                }
            }
            else
            {
                Movement_Receipt_Kind_Combo.Visible = false;
            }
        }

        #endregion

        #region The Safe Panel

        private void Movement_Receipt_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Movment_Receipt_Panel);
        }

        private void Expenses_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Expenses_Panel);

            Expenses_Table.Rows.Clear();

            Movement_Receipt Movement_Receipt = new Movement_Receipt();
            List<Movement_Receipt> Movement_Receipts = Movement_Receipt.Sql_Select("مصروفات");

            int Number_Receipts = Movement_Receipts.Count();

            for (int i = Number_Receipts - 1; i > -1; i--)
            {
                string[] Row = new string[] { Movement_Receipts.ElementAt(i).Get_Detail(), Movement_Receipts.ElementAt(i).Get_Date(), Movement_Receipts.ElementAt(i).Get_Amount(), Movement_Receipts.ElementAt(i).Get_Kind(), Movement_Receipts.ElementAt(i).Get_Owner(), Movement_Receipts.ElementAt(i).Get_Book(), Movement_Receipts.ElementAt(i).Get_Number() };
                Expenses_Table.Rows.Add(Row);
            }


        }

        private void Imports_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Imports_Panel);

            Imports_Table.Rows.Clear();

            Movement_Receipt Movement_Receipt = new Movement_Receipt();
            List<Movement_Receipt> Movement_Receipts = Movement_Receipt.Sql_Select("واردات");

            int Number_Receipts = Movement_Receipts.Count();

            for (int i = Number_Receipts - 1; i > -1; i--)
            {
                string[] Row = new string[] { Movement_Receipts.ElementAt(i).Get_Detail(), Movement_Receipts.ElementAt(i).Get_Date(), Movement_Receipts.ElementAt(i).Get_Amount(), Movement_Receipts.ElementAt(i).Get_Kind(), Movement_Receipts.ElementAt(i).Get_Owner(), Movement_Receipts.ElementAt(i).Get_Book(), Movement_Receipts.ElementAt(i).Get_Number() };
                Imports_Table.Rows.Add(Row);
            }
        }

        private void Employee_Receipt_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Employee_Receipt_Panel);
        }

        #endregion

        #region Expenses Panel

        private void Expenses_Back_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Safe_Panel);
        }

        #endregion

        #region Imports Panel

        private void Imports_Back_Buttom_Click(object sender, EventArgs e)
        {
            GUI_Function = new GUI_Function();
            GUI_Function.Panel_Visible(ref All_Panels, ref Safe_Panel);
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void All_Student_Print_Buttom_Click(object sender, EventArgs e)
        {
            Student_Form Student_Form = new Student_Form();
            Student_Form.Show();
        }

        private void Student_Menu_Print_Buttom_Click(object sender, EventArgs e)
        {
            Student_Info_Form Student_Info_Form = new Student_Info_Form();

            SqlConnection Connect = new SqlConnection(@"Server=localhost\sqlexpress; DataBase=CTM_DB; Integrated Security=true;");
            SqlDataAdapter Adapter = new SqlDataAdapter("Select * From Student_Table Where Id='" + Student_Id_TextBox.Text + "'", Connect);

            Adapter.Fill(Student_Info_Form.CTM_DBDataSet.Student_Table);

            Student_Info_Form.reportViewer1.RefreshReport();

            Student_Info_Form.Show();
        }

        private void Student_More_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        
        
    
        

        

        

















    }
}
