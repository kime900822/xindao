namespace CashBorrowINFO.logon
{
    partial class FindPassword_form
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
            this.edtTelephone = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbGetMessage = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.edtChechNum = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // edtTelephone
            // 
            this.edtTelephone.Location = new System.Drawing.Point(126, 84);
            this.edtTelephone.Name = "edtTelephone";
            this.edtTelephone.Size = new System.Drawing.Size(172, 21);
            this.edtTelephone.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(26, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "手机号：";
            // 
            // lbGetMessage
            // 
            this.lbGetMessage.AutoSize = true;
            this.lbGetMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbGetMessage.Location = new System.Drawing.Point(231, 125);
            this.lbGetMessage.Name = "lbGetMessage";
            this.lbGetMessage.Size = new System.Drawing.Size(67, 14);
            this.lbGetMessage.TabIndex = 16;
            this.lbGetMessage.TabStop = true;
            this.lbGetMessage.Text = "发送验证码";
            this.lbGetMessage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbGetMessage_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(26, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 27);
            this.label2.TabIndex = 17;
            this.label2.Text = "验证码：";
            // 
            // edtChechNum
            // 
            this.edtChechNum.Location = new System.Drawing.Point(126, 170);
            this.edtChechNum.Name = "edtChechNum";
            this.edtChechNum.Size = new System.Drawing.Size(172, 21);
            this.edtChechNum.TabIndex = 18;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(200, 242);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 30);
            this.btnOK.TabIndex = 19;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FindPassword_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(343, 399);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.edtChechNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbGetMessage);
            this.Controls.Add(this.edtTelephone);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Name = "FindPassword_form";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "密码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edtTelephone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lbGetMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox edtChechNum;
    }
}