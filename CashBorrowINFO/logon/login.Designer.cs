namespace CashBorrowINFO.logon
{
    partial class login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.cbSaveName = new System.Windows.Forms.CheckBox();
            this.link_Register = new System.Windows.Forms.LinkLabel();
            this.link_ShowRule = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.edtPassword = new System.Windows.Forms.TextBox();
            this.edtID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Cancle = new System.Windows.Forms.Button();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.link_ForgetPass = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // cbSaveName
            // 
            this.cbSaveName.AutoSize = true;
            this.cbSaveName.Location = new System.Drawing.Point(289, 229);
            this.cbSaveName.Name = "cbSaveName";
            this.cbSaveName.Size = new System.Drawing.Size(84, 16);
            this.cbSaveName.TabIndex = 10;
            this.cbSaveName.Text = "记录用户名";
            this.cbSaveName.UseVisualStyleBackColor = true;
            // 
            // link_Register
            // 
            this.link_Register.AutoSize = true;
            this.link_Register.BackColor = System.Drawing.SystemColors.ControlLight;
            this.link_Register.DisabledLinkColor = System.Drawing.Color.White;
            this.link_Register.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.link_Register.Location = new System.Drawing.Point(198, 330);
            this.link_Register.Name = "link_Register";
            this.link_Register.Size = new System.Drawing.Size(35, 14);
            this.link_Register.TabIndex = 9;
            this.link_Register.TabStop = true;
            this.link_Register.Text = "注册";
            this.link_Register.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_Register_LinkClicked);
            // 
            // link_ShowRule
            // 
            this.link_ShowRule.AutoSize = true;
            this.link_ShowRule.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.link_ShowRule.Location = new System.Drawing.Point(336, 330);
            this.link_ShowRule.Name = "link_ShowRule";
            this.link_ShowRule.Size = new System.Drawing.Size(63, 14);
            this.link_ShowRule.TabIndex = 8;
            this.link_ShowRule.TabStop = true;
            this.link_ShowRule.Text = "查看规则";
            this.link_ShowRule.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_ShowRule_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(144, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(285, 35);
            this.label3.TabIndex = 6;
            this.label3.Text = "社会借贷数据共享中心";
            // 
            // edtPassword
            // 
            this.edtPassword.Location = new System.Drawing.Point(256, 202);
            this.edtPassword.Name = "edtPassword";
            this.edtPassword.PasswordChar = '*';
            this.edtPassword.Size = new System.Drawing.Size(172, 21);
            this.edtPassword.TabIndex = 5;
            // 
            // edtID
            // 
            this.edtID.Location = new System.Drawing.Point(256, 140);
            this.edtID.Name = "edtID";
            this.edtID.Size = new System.Drawing.Size(172, 21);
            this.edtID.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(181, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(83, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "账号/手机/邮箱：";
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancle.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Cancle.Location = new System.Drawing.Point(339, 275);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Size = new System.Drawing.Size(107, 35);
            this.btn_Cancle.TabIndex = 1;
            this.btn_Cancle.Text = "取消";
            this.btn_Cancle.UseVisualStyleBackColor = true;
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // btn_Submit
            // 
            this.btn_Submit.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Submit.Location = new System.Drawing.Point(150, 275);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(111, 35);
            this.btn_Submit.TabIndex = 0;
            this.btn_Submit.Text = "登录";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Silver;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
            // 
            // link_ForgetPass
            // 
            this.link_ForgetPass.AutoSize = true;
            this.link_ForgetPass.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.link_ForgetPass.Location = new System.Drawing.Point(253, 330);
            this.link_ForgetPass.Name = "link_ForgetPass";
            this.link_ForgetPass.Size = new System.Drawing.Size(63, 14);
            this.link_ForgetPass.TabIndex = 7;
            this.link_ForgetPass.TabStop = true;
            this.link_ForgetPass.Text = "找回密码";
            this.link_ForgetPass.Visible = false;
            this.link_ForgetPass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forgetPass_LinkClicked);
            // 
            // login
            // 
            this.AcceptButton = this.btn_Submit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.CancelButton = this.btn_Cancle;
            this.ClientSize = new System.Drawing.Size(566, 392);
            this.ControlBox = false;
            this.Controls.Add(this.cbSaveName);
            this.Controls.Add(this.link_Register);
            this.Controls.Add(this.link_ShowRule);
            this.Controls.Add(this.link_ForgetPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.edtPassword);
            this.Controls.Add(this.edtID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.btn_Submit);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.login_FormClosing);
            this.Load += new System.EventHandler(this.login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.Button btn_Cancle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edtID;
        private System.Windows.Forms.TextBox edtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel link_ShowRule;
        private System.Windows.Forms.LinkLabel link_Register;
        private System.Windows.Forms.CheckBox cbSaveName;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.LinkLabel link_ForgetPass;
    }
}