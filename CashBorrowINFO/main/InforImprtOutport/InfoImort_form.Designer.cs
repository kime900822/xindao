namespace CashBorrowINFO.main.InforImprtOutport
{
    partial class InfoImort_form
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lkDownload = new System.Windows.Forms.LinkLabel();
            this.edtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridBorrow = new System.Windows.Forms.DataGridView();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBorrow)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Excel|*.xls";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lkDownload);
            this.groupBox1.Controls.Add(this.edtPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导入";
            // 
            // lkDownload
            // 
            this.lkDownload.AutoSize = true;
            this.lkDownload.Location = new System.Drawing.Point(636, 25);
            this.lkDownload.Name = "lkDownload";
            this.lkDownload.Size = new System.Drawing.Size(53, 12);
            this.lkDownload.TabIndex = 3;
            this.lkDownload.TabStop = true;
            this.lkDownload.Text = "模板下载";
            this.lkDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkDownload_LinkClicked);
            // 
            // edtPath
            // 
            this.edtPath.Enabled = false;
            this.edtPath.Location = new System.Drawing.Point(192, 22);
            this.edtPath.Name = "edtPath";
            this.edtPath.Size = new System.Drawing.Size(397, 21);
            this.edtPath.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "导入文件";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(27, 20);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "导入数据";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 476);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(760, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridBorrow);
            this.groupBox2.Location = new System.Drawing.Point(13, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(759, 382);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "导入结果";
            // 
            // dataGridBorrow
            // 
            this.dataGridBorrow.AllowUserToAddRows = false;
            this.dataGridBorrow.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridBorrow.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridBorrow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridBorrow.Location = new System.Drawing.Point(6, 33);
            this.dataGridBorrow.Name = "dataGridBorrow";
            this.dataGridBorrow.ReadOnly = true;
            this.dataGridBorrow.RowTemplate.Height = 23;
            this.dataGridBorrow.Size = new System.Drawing.Size(746, 343);
            this.dataGridBorrow.TabIndex = 1;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "模板.xls";
            this.saveFileDialog.Filter = "Excel|*.xls";
            // 
            // InfoImort_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.MinimizeBox = false;
            this.Name = "InfoImort_form";
            this.ShowIcon = false;
            this.Text = "借款信息批量导入";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InfoImort_form_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBorrow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridBorrow;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.LinkLabel lkDownload;
    }
}