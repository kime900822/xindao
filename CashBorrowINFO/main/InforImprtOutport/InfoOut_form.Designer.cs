namespace CashBorrowINFO.main.InforImprtOutport
{
    partial class InfoOut_form
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
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.edtOut = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dateE = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dateS = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.edtCID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pagerControl1 = new TActionProject.PagerControl();
            this.dataGridBorrow = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBorrow)).BeginInit();
            this.SuspendLayout();
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "借款信息导出";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 476);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(760, 23);
            this.progressBar.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.edtOut);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dateE);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dateS);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.edtCID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 85);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "条件选择";
            // 
            // edtOut
            // 
            this.edtOut.Location = new System.Drawing.Point(593, 48);
            this.edtOut.Name = "edtOut";
            this.edtOut.Size = new System.Drawing.Size(75, 23);
            this.edtOut.TabIndex = 8;
            this.edtOut.Text = "导出";
            this.edtOut.UseVisualStyleBackColor = true;
            this.edtOut.Click += new System.EventHandler(this.edtOut_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(593, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateE
            // 
            this.dateE.Checked = false;
            this.dateE.CustomFormat = "yyyy-MM-dd";
            this.dateE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateE.Location = new System.Drawing.Point(345, 48);
            this.dateE.Name = "dateE";
            this.dateE.ShowCheckBox = true;
            this.dateE.Size = new System.Drawing.Size(156, 21);
            this.dateE.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(272, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "小于";
            // 
            // dateS
            // 
            this.dateS.Checked = false;
            this.dateS.CustomFormat = "yyyy-MM-dd";
            this.dateS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateS.Location = new System.Drawing.Point(101, 50);
            this.dateS.Name = "dateS";
            this.dateS.ShowCheckBox = true;
            this.dateS.Size = new System.Drawing.Size(156, 21);
            this.dateS.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "借款日期大于";
            // 
            // edtCID
            // 
            this.edtCID.Location = new System.Drawing.Point(101, 19);
            this.edtCID.Name = "edtCID";
            this.edtCID.Size = new System.Drawing.Size(156, 21);
            this.edtCID.TabIndex = 1;
            this.edtCID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtCID_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "借款人身份证";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pagerControl1);
            this.groupBox2.Controls.Add(this.dataGridBorrow);
            this.groupBox2.Location = new System.Drawing.Point(13, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(759, 367);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "借款信息";
            // 
            // pagerControl1
            // 
            this.pagerControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.pagerControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(78)))), ((int)(((byte)(151)))));
            this.pagerControl1.JumpText = "Go";
            this.pagerControl1.Location = new System.Drawing.Point(7, 338);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.PageIndex = 1;
            this.pagerControl1.PageSize = 100;
            this.pagerControl1.RecordCount = 0;
            this.pagerControl1.Size = new System.Drawing.Size(746, 29);
            this.pagerControl1.TabIndex = 3;
            this.pagerControl1.OnPageChanged += new System.EventHandler(this.pagerControl1_OnPageChanged);
            // 
            // dataGridBorrow
            // 
            this.dataGridBorrow.AllowUserToAddRows = false;
            this.dataGridBorrow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridBorrow.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridBorrow.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridBorrow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridBorrow.Location = new System.Drawing.Point(7, 21);
            this.dataGridBorrow.Name = "dataGridBorrow";
            this.dataGridBorrow.ReadOnly = true;
            this.dataGridBorrow.RowTemplate.Height = 23;
            this.dataGridBorrow.Size = new System.Drawing.Size(746, 317);
            this.dataGridBorrow.TabIndex = 0;
            // 
            // InfoOut_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.MinimizeBox = false;
            this.Name = "InfoOut_form";
            this.ShowIcon = false;
            this.Text = "借款信息批量导出";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InfoOut_form_FormClosed);
            this.Load += new System.EventHandler(this.InfoOut_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBorrow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DateTimePicker dateE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edtCID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button edtOut;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridBorrow;
        private TActionProject.PagerControl pagerControl1;
    }
}