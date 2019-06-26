using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace CTM_DB.Class
{
    class GUI_Function
    {

        // Defult Constructor
        public GUI_Function() { }
        
        /// Make Visible For Target Panel Is : True
        /// <param name="Panels"></param>
        /// <param name="Target_Panel"></param>
        
        public void Panel_Visible(ref List<Panel> Panels, ref Panel Target_Panel)
        {
            int Number_Panels = Panels.Count();

            for (int i = 0; i < Number_Panels; i++)
            {
                if (Panels[i] == Target_Panel)
                {
                    Panels[i].Visible = true;
                }
                else
                {
                    Panels[i].Visible = false;
                }
            }
        }


        /// Make Visible For ComBoBox Panels : False
        /// <param name="ComboBox Panel"></param>
        
        public void Hide_ComboBox_Panel(ref List<Panel> ComboBox_Panel)
        {
            int Number_ComboBox_Panel = ComboBox_Panel.Count();

            for (int i = 0; i < Number_ComboBox_Panel; i++)
            {
                ComboBox_Panel[i].Visible = false;
            }
        }


        public void Set_Student_TextBox(ref List<TextBox> TextBox)
        {
            TextBox[0].Text = "000000000";
            TextBox[0].ForeColor = Color.Silver;

            TextBox[1].Text = "الاسم الاول";
            TextBox[1].ForeColor = Color.Silver;

            TextBox[2].Text = "الاسم الاخير";
            TextBox[2].ForeColor = Color.Silver;

            TextBox[3].Text = "اسم الاب";
            TextBox[3].ForeColor = Color.Silver;

            TextBox[4].Text = "01/01/1990";
            TextBox[4].ForeColor = Color.Silver;

            TextBox[5].Text = "النوع";
            TextBox[5].ForeColor = Color.Silver;

            TextBox[6].Text = "022-0";
            TextBox[6].ForeColor = Color.Silver;

            TextBox[7].Text = "022-0";
            TextBox[7].ForeColor = Color.Silver;

            TextBox[8].Text = "المرحلة الدراسية";
            TextBox[8].ForeColor = Color.Silver;

            TextBox[9].Text = "نوع التسجيل";
            TextBox[9].ForeColor = Color.Silver;

            TextBox[10].Text = "اسم المدرسة";
            TextBox[10].ForeColor = Color.Silver;

            TextBox[11].Text = "المواصلات";
            TextBox[11].ForeColor = Color.Silver;

            TextBox[12].Text = "المدينة";
            TextBox[12].ForeColor = Color.Silver;

            TextBox[13].Text = "الحي";
            TextBox[13].ForeColor = Color.Silver;

            TextBox[14].Text = "الشارع";
            TextBox[14].ForeColor = Color.Silver;

            TextBox[15].Text = "التفاصيل";
            TextBox[15].ForeColor = Color.Silver;

            TextBox[16].Text = "مفعل";
            TextBox[16].ForeColor = Color.Silver;

            TextBox[17].Text = "2018119001";
            TextBox[17].ForeColor = Color.Silver;

            TextBox[18].Text = "201819001";
            TextBox[18].ForeColor = Color.Silver;

            TextBox[19].Text = "اكتب المزبد من الملاحظات";
            TextBox[19].ForeColor = Color.Silver;

        }

        public void Clear_Receipt_TextBox(ref List<TextBox> TextBox)
        {
            TextBox[0].Text = "0000000000";
            TextBox[0].ForeColor = Color.Silver;

            TextBox[1].Text = "A19 or B19";
            TextBox[1].ForeColor = Color.Silver;

            TextBox[2].Text = "اسم الفرع";
            TextBox[2].ForeColor = Color.Silver;

            TextBox[3].Text = "اسم المستلم";
            TextBox[3].ForeColor = Color.Silver;

            TextBox[4].Text = "الاسم الاول";
            TextBox[4].ForeColor = Color.Silver;

            TextBox[5].Text = "الاسم الاخير";
            TextBox[5].ForeColor = Color.Silver;

            TextBox[6].Text = "000 L.E";
            TextBox[6].ForeColor = Color.Silver;

            TextBox[7].Text = "01/01/1990";
            TextBox[7].ForeColor = Color.Silver;

            TextBox[8].Text = "مصروفات";
            TextBox[8].ForeColor = Color.Silver;
        }

        public void Account_TextBox_Defult(ref List<TextBox> TextBox, ref DataGridView Receipt_Table, ref DataGridView Account_Detail_Table)
        {
            TextBox[0].Text = "0000000000";
            TextBox[0].ForeColor = Color.Silver;

            TextBox[1].Text = "اسم الحساب";
            TextBox[1].ForeColor = Color.Silver;

            TextBox[2].Text = "فردي / مشترك";
            TextBox[2].ForeColor = Color.Silver;

            TextBox[3].Text = "022 - 01";
            TextBox[3].ForeColor = Color.Silver;

            TextBox[4].Text = "022 - 01";
            TextBox[4].ForeColor = Color.Silver;

            TextBox[5].Text = "022 - 01";
            TextBox[5].ForeColor = Color.Silver;

            TextBox[6].Text = "000 ج.م";
            TextBox[6].ForeColor = Color.Silver;

            TextBox[7].Text = "000 ج.م";
            TextBox[7].ForeColor = Color.Silver;

            TextBox[8].Text = "000 ج.م";
            TextBox[8].ForeColor = Color.Silver;

            Receipt_Table.Rows.Clear();
            Account_Detail_Table.Rows.Clear();
        }

        public void Movement_Receipt_TextBox_Defult(ref List<TextBox> TextBox)
        {
            TextBox[0].Text = "2018190001";
            TextBox[0].ForeColor = Color.Silver;

            TextBox[1].Text = "A19 or B19";
            TextBox[1].ForeColor = Color.Silver;

            TextBox[2].Text = "واردات / مصروفات";
            TextBox[2].ForeColor = Color.Silver;

            TextBox[3].Text = "اسم المسؤول";
            TextBox[3].ForeColor = Color.Silver;

            TextBox[4].Text = "الصنف";
            TextBox[4].ForeColor = Color.Silver;

            TextBox[5].Text = "000 L.E";
            TextBox[5].ForeColor = Color.Silver;

            TextBox[6].Text = "01/01/1990";
            TextBox[6].ForeColor = Color.Silver;

            TextBox[7].Text = "تفاصيل";
            TextBox[7].ForeColor = Color.Silver;
        }

        public void Employee_Receipt_TextBox_Defult(ref List<TextBox> TextBox)
        {
            TextBox[0].Text = "000000000";
            TextBox[0].ForeColor = Color.Silver;

            TextBox[1].Text = "A19 or B19";
            TextBox[1].ForeColor = Color.Silver;

            TextBox[2].Text = "اسم المسؤول";
            TextBox[2].ForeColor = Color.Silver;

            TextBox[3].Text = "01/01/1990";
            TextBox[3].ForeColor = Color.Silver;

            TextBox[4].Text = "اسم الموظف";
            TextBox[4].ForeColor = Color.Silver;

            TextBox[5].Text = "000 L.E";
            TextBox[5].ForeColor = Color.Silver;

            TextBox[6].Text = "000 L.E";
            TextBox[6].ForeColor = Color.Silver;

            TextBox[7].Text = "000 L.E";
            TextBox[7].ForeColor = Color.Silver;

            TextBox[8].Text = "المزيد من التفاصيل حول حساب المرتب";
            TextBox[8].ForeColor = Color.Silver;
        }

        public void Employee_TextBox_Defult(ref List<TextBox> TextBox)
        {
            TextBox[0].Text = "000000000";
            TextBox[0].ForeColor = Color.Silver;

            TextBox[1].Text = "اسم الموظف";
            TextBox[1].ForeColor = Color.Silver;

            TextBox[2].Text = "022-0";
            TextBox[2].ForeColor = Color.Silver;

            TextBox[3].Text = "022-0";
            TextBox[3].ForeColor = Color.Silver;

            TextBox[4].Text = "لا يوجد / اسم الحساب";
            TextBox[4].ForeColor = Color.Silver;

            TextBox[5].Text = "مدرس صف / اختصاص";
            TextBox[5].ForeColor = Color.Silver;

            TextBox[6].Text = "ابتدائي / اعدادي";
            TextBox[6].ForeColor = Color.Silver;

            TextBox[7].Text = "اسبوعيا";
            TextBox[7].ForeColor = Color.Silver;

            TextBox[8].Text = "التفاصيل";
            TextBox[8].ForeColor = Color.Silver;
        }


    }
}
