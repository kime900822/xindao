namespace CashBorrowINFO.main.CustomerManager
{
    partial class Repay_form
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
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.edtRInterest = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.edtRType = new System.Windows.Forms.TextBox();
            this.edtRamount = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.edtOverTime = new System.Windows.Forms.TextBox();
            this.edtRFine = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.借款人信息 = new System.Windows.Forms.GroupBox();
            this.lblBLimit = new System.Windows.Forms.Label();
            this.lblBAMOUNT = new System.Windows.Forms.Label();
            this.lblCID = new System.Windows.Forms.Label();
            this.lblCName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddlBSysid = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridRepay = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.借款人信息.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRepay)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancle
            // 
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(685, 433);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 5;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(576, 433);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.edtRInterest);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.edtRType);
            this.groupBox2.Controls.Add(this.edtRamount);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(759, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "还款信息";
            // 
            // edtRInterest
            // 
            this.edtRInterest.Location = new System.Drawing.Point(109, 39);
            this.edtRInterest.Name = "edtRInterest";
            this.edtRInterest.Size = new System.Drawing.Size(258, 21);
            this.edtRInterest.TabIndex = 7;
            this.edtRInterest.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtRInterest_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "还利息";
            // 
            // edtRType
            // 
            this.edtRType.Location = new System.Drawing.Point(109, 64);
            this.edtRType.Name = "edtRType";
            this.edtRType.Size = new System.Drawing.Size(258, 21);
            this.edtRType.TabIndex = 5;
            // 
            // edtRamount
            // 
            this.edtRamount.Location = new System.Drawing.Point(109, 15);
            this.edtRamount.Name = "edtRamount";
            this.edtRamount.Size = new System.Drawing.Size(258, 21);
            this.edtRamount.TabIndex = 4;
            this.edtRamount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtRamount_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.edtOverTime);
            this.groupBox3.Controls.Add(this.edtRFine);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(398, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(355, 79);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "逾期信息";
            // 
            // edtOverTime
            // 
            this.edtOverTime.Location = new System.Drawing.Point(89, 24);
            this.edtOverTime.Name = "edtOverTime";
            this.edtOverTime.Size = new System.Drawing.Size(258, 21);
            this.edtOverTime.TabIndex = 8;
            // 
            // edtRFine
            // 
            this.edtRFine.Location = new System.Drawing.Point(89, 48);
            this.edtRFine.Name = "edtRFine";
            this.edtRFine.Size = new System.Drawing.Size(258, 21);
            this.edtRFine.TabIndex = 7;
            this.edtRFine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtRFine_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "处罚金";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "超时时间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "还款方式";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "还本金";
            // 
            // 借款人信息
            // 
            this.借款人信息.Controls.Add(this.lblBLimit);
            this.借款人信息.Controls.Add(this.lblBAMOUNT);
            this.借款人信息.Controls.Add(this.lblCID);
            this.借款人信息.Controls.Add(this.lblCName);
            this.借款人信息.Controls.Add(this.label2);
            this.借款人信息.Controls.Add(this.label5);
            this.借款人信息.Controls.Add(this.label4);
            this.借款人信息.Controls.Add(this.label3);
            this.借款人信息.Location = new System.Drawing.Point(13, 79);
            this.借款人信息.Name = "借款人信息";
            this.借款人信息.Size = new System.Drawing.Size(759, 80);
            this.借款人信息.TabIndex = 1;
            this.借款人信息.TabStop = false;
            this.借款人信息.Text = "借款信息";
            // 
            // lblBLimit
            // 
            this.lblBLimit.AutoSize = true;
            this.lblBLimit.Location = new System.Drawing.Point(516, 49);
            this.lblBLimit.Name = "lblBLimit";
            this.lblBLimit.Size = new System.Drawing.Size(0, 12);
            this.lblBLimit.TabIndex = 7;
            // 
            // lblBAMOUNT
            // 
            this.lblBAMOUNT.AutoSize = true;
            this.lblBAMOUNT.Location = new System.Drawing.Point(514, 22);
            this.lblBAMOUNT.Name = "lblBAMOUNT";
            this.lblBAMOUNT.Size = new System.Drawing.Size(0, 12);
            this.lblBAMOUNT.TabIndex = 6;
            // 
            // lblCID
            // 
            this.lblCID.AutoSize = true;
            this.lblCID.Location = new System.Drawing.Point(108, 49);
            this.lblCID.Name = "lblCID";
            this.lblCID.Size = new System.Drawing.Size(0, 12);
            this.lblCID.TabIndex = 5;
            // 
            // lblCName
            // 
            this.lblCName.AutoSize = true;
            this.lblCName.Location = new System.Drawing.Point(109, 23);
            this.lblCName.Name = "lblCName";
            this.lblCName.Size = new System.Drawing.Size(0, 12);
            this.lblCName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(416, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "借款金额";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(416, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "剩余还款额";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "身份证号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "姓名";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ddlBSysid);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "借款信息选择";
            // 
            // ddlBSysid
            // 
            this.ddlBSysid.FormattingEnabled = true;
            this.ddlBSysid.Location = new System.Drawing.Point(107, 21);
            this.ddlBSysid.Name = "ddlBSysid";
            this.ddlBSysid.Size = new System.Drawing.Size(260, 20);
            this.ddlBSysid.TabIndex = 1;
            this.ddlBSysid.SelectedIndexChanged += new System.EventHandler(this.ddlBSysid_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "借款单号";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridRepay);
            this.groupBox4.Location = new System.Drawing.Point(12, 272);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(753, 155);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "还款历史";
            // 
            // dataGridRepay
            // 
            this.dataGridRepay.AllowUserToAddRows = false;
            this.dataGridRepay.AllowUserToDeleteRows = false;
            this.dataGridRepay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridRepay.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridRepay.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridRepay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRepay.Location = new System.Drawing.Point(0, 21);
            this.dataGridRepay.Name = "dataGridRepay";
            this.dataGridRepay.RowTemplate.Height = 23;
            this.dataGridRepay.Size = new System.Drawing.Size(746, 128);
            this.dataGridRepay.TabIndex = 0;
            // 
            // Repay_form
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.CancelButton = this.btnCancle;
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.借款人信息);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.MinimizeBox = false;
            this.Name = "Repay_form";
            this.ShowIcon = false;
            this.Text = " 还款";
            this.Load += new System.EventHandler(this.Repay_form_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.借款人信息.ResumeLayout(false);
            this.借款人信息.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRepay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox 借款人信息;
        private System.Windows.Forms.ComboBox ddlBSysid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label lblBLimit;
        private System.Windows.Forms.Label lblBAMOUNT;
        private System.Windows.Forms.Label lblCID;
        private System.Windows.Forms.Label lblCName;
        private System.Windows.Forms.TextBox edtRType;
        private System.Windows.Forms.TextBox edtRamount;
        private System.Windows.Forms.TextBox edtRFine;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox edtRInterest;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridRepay;
        private System.Windows.Forms.TextBox edtOverTime;
    }
}