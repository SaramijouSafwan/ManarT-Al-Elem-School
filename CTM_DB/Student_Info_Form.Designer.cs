namespace CTM_DB
{
    partial class Student_Info_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.CTM_DBDataSet = new CTM_DB.CTM_DBDataSet();
            this.Student_TableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Student_TableTableAdapter = new CTM_DB.CTM_DBDataSetTableAdapters.Student_TableTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.CTM_DBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Student_TableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.Student_TableBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CTM_DB.Report4.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(954, 353);
            this.reportViewer1.TabIndex = 0;
            // 
            // CTM_DBDataSet
            // 
            this.CTM_DBDataSet.DataSetName = "CTM_DBDataSet";
            this.CTM_DBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Student_TableBindingSource
            // 
            this.Student_TableBindingSource.DataMember = "Student_Table";
            this.Student_TableBindingSource.DataSource = this.CTM_DBDataSet;
            // 
            // Student_TableTableAdapter
            // 
            this.Student_TableTableAdapter.ClearBeforeFill = true;
            // 
            // Student_Info_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 353);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Student_Info_Form";
            this.Text = "Student_Info_Form";
            this.Load += new System.EventHandler(this.Student_Info_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CTM_DBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Student_TableBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource Student_TableBindingSource;
        private CTM_DBDataSetTableAdapters.Student_TableTableAdapter Student_TableTableAdapter;
        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        public CTM_DBDataSet CTM_DBDataSet;
    }
}