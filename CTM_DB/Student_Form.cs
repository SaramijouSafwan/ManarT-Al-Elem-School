﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTM_DB
{
    public partial class Student_Form : Form
    {
        public Student_Form()
        {
            InitializeComponent();
        }

        private void Student_Form_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'CTM_DBDataSet.Student_Table' table. You can move, or remove it, as needed.
            this.Student_TableTableAdapter.Fill(this.CTM_DBDataSet.Student_Table);

            this.reportViewer1.RefreshReport();
        }
    }
}
