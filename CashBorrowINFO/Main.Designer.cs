using System.Windows.Forms;

namespace CashBorrowINFO
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsShowUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsShowTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.M10 = new System.Windows.Forms.ToolStripMenuItem();
            this.M11 = new System.Windows.Forms.ToolStripMenuItem();
            this.M111 = new System.Windows.Forms.ToolStripMenuItem();
            this.M112 = new System.Windows.Forms.ToolStripMenuItem();
            this.M113 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.M12 = new System.Windows.Forms.ToolStripMenuItem();
            this.M13 = new System.Windows.Forms.ToolStripMenuItem();
            this.M20 = new System.Windows.Forms.ToolStripMenuItem();
            this.M21 = new System.Windows.Forms.ToolStripMenuItem();
            this.M22 = new System.Windows.Forms.ToolStripMenuItem();
            this.M30 = new System.Windows.Forms.ToolStripMenuItem();
            this.M31 = new System.Windows.Forms.ToolStripMenuItem();
            this.M32 = new System.Windows.Forms.ToolStripMenuItem();
            this.M33 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.M34 = new System.Windows.Forms.ToolStripMenuItem();
            this.N35 = new System.Windows.Forms.ToolStripMenuItem();
            this.M40 = new System.Windows.Forms.ToolStripMenuItem();
            this.M41 = new System.Windows.Forms.ToolStripMenuItem();
            this.M42 = new System.Windows.Forms.ToolStripMenuItem();
            this.M43 = new System.Windows.Forms.ToolStripMenuItem();
            this.M44 = new System.Windows.Forms.ToolStripMenuItem();
            this.M50 = new System.Windows.Forms.ToolStripMenuItem();
            this.M60 = new System.Windows.Forms.ToolStripMenuItem();
            this.M61 = new System.Windows.Forms.ToolStripMenuItem();
            this.M62 = new System.Windows.Forms.ToolStripMenuItem();
            this.N70 = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsShowUser,
            this.tsShowTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsShowUser
            // 
            this.tsShowUser.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tsShowUser.Name = "tsShowUser";
            this.tsShowUser.Size = new System.Drawing.Size(131, 17);
            this.tsShowUser.Text = "toolStripStatusLabel1";
            // 
            // tsShowTime
            // 
            this.tsShowTime.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tsShowTime.Name = "tsShowTime";
            this.tsShowTime.Size = new System.Drawing.Size(131, 17);
            this.tsShowTime.Text = "toolStripStatusLabel2";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.M10,
            this.M20,
            this.M30,
            this.M40,
            this.M50,
            this.M60,
            this.N70});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // M10
            // 
            this.M10.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.M11,
            this.toolStripSeparator1,
            this.M12,
            this.M13});
            this.M10.Image = ((System.Drawing.Image)(resources.GetObject("M10.Image")));
            this.M10.Name = "M10";
            this.M10.Size = new System.Drawing.Size(84, 21);
            this.M10.Text = "客户管理";
            // 
            // M11
            // 
            this.M11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.M111,
            this.M112,
            this.M113});
            this.M11.Image = ((System.Drawing.Image)(resources.GetObject("M11.Image")));
            this.M11.Name = "M11";
            this.M11.Size = new System.Drawing.Size(148, 22);
            this.M11.Text = "资料管理中心";
            // 
            // M111
            // 
            this.M111.Name = "M111";
            this.M111.Size = new System.Drawing.Size(148, 22);
            this.M111.Text = "客户查询";
            this.M111.Click += new System.EventHandler(this.M111_Click);
            // 
            // M112
            // 
            this.M112.Name = "M112";
            this.M112.Size = new System.Drawing.Size(148, 22);
            this.M112.Text = "借款记录查询";
            this.M112.Click += new System.EventHandler(this.M112_Click);
            // 
            // M113
            // 
            this.M113.Name = "M113";
            this.M113.Size = new System.Drawing.Size(148, 22);
            this.M113.Text = "短信发送查询";
            this.M113.Click += new System.EventHandler(this.M113_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // M12
            // 
            this.M12.Image = ((System.Drawing.Image)(resources.GetObject("M12.Image")));
            this.M12.Name = "M12";
            this.M12.Size = new System.Drawing.Size(148, 22);
            this.M12.Text = "借款";
            this.M12.Click += new System.EventHandler(this.M12_Click);
            // 
            // M13
            // 
            this.M13.Image = ((System.Drawing.Image)(resources.GetObject("M13.Image")));
            this.M13.Name = "M13";
            this.M13.Size = new System.Drawing.Size(148, 22);
            this.M13.Text = "还款";
            this.M13.Click += new System.EventHandler(this.M13_Click);
            // 
            // M20
            // 
            this.M20.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.M21,
            this.M22});
            this.M20.Image = ((System.Drawing.Image)(resources.GetObject("M20.Image")));
            this.M20.Name = "M20";
            this.M20.Size = new System.Drawing.Size(156, 21);
            this.M20.Text = "借款信息批量导入导出";
            // 
            // M21
            // 
            this.M21.Image = ((System.Drawing.Image)(resources.GetObject("M21.Image")));
            this.M21.Name = "M21";
            this.M21.Size = new System.Drawing.Size(172, 22);
            this.M21.Text = "借款信息批量导入";
            this.M21.Click += new System.EventHandler(this.M21_Click);
            // 
            // M22
            // 
            this.M22.Image = ((System.Drawing.Image)(resources.GetObject("M22.Image")));
            this.M22.Name = "M22";
            this.M22.Size = new System.Drawing.Size(172, 22);
            this.M22.Text = "借款信息批量导出";
            this.M22.Click += new System.EventHandler(this.M22_Click);
            // 
            // M30
            // 
            this.M30.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.M31,
            this.M32,
            this.M33,
            this.toolStripSeparator2,
            this.M34,
            this.N35});
            this.M30.Image = ((System.Drawing.Image)(resources.GetObject("M30.Image")));
            this.M30.Name = "M30";
            this.M30.Size = new System.Drawing.Size(84, 21);
            this.M30.Text = "余额充值";
            // 
            // M31
            // 
            this.M31.Name = "M31";
            this.M31.Size = new System.Drawing.Size(148, 22);
            this.M31.Text = "银联支付";
            this.M31.Click += new System.EventHandler(this.M31_Click);
            // 
            // M32
            // 
            this.M32.Name = "M32";
            this.M32.Size = new System.Drawing.Size(148, 22);
            this.M32.Text = "支付宝支付";
            this.M32.Click += new System.EventHandler(this.M32_Click);
            // 
            // M33
            // 
            this.M33.Name = "M33";
            this.M33.Size = new System.Drawing.Size(148, 22);
            this.M33.Text = "短信扣费";
            this.M33.Click += new System.EventHandler(this.M33_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // M34
            // 
            this.M34.Name = "M34";
            this.M34.Size = new System.Drawing.Size(148, 22);
            this.M34.Text = "扣费记录查询";
            this.M34.Click += new System.EventHandler(this.M34_Click);
            // 
            // N35
            // 
            this.N35.Name = "N35";
            this.N35.Size = new System.Drawing.Size(148, 22);
            this.N35.Text = "充值记录查询";
            this.N35.Click += new System.EventHandler(this.N35_Click);
            // 
            // M40
            // 
            this.M40.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.M41,
            this.M42,
            this.M43,
            this.M44});
            this.M40.Name = "M40";
            this.M40.Size = new System.Drawing.Size(92, 21);
            this.M40.Text = "客户授信查询";
            // 
            // M41
            // 
            this.M41.Name = "M41";
            this.M41.Size = new System.Drawing.Size(172, 22);
            this.M41.Text = "本平台内查询";
            this.M41.Click += new System.EventHandler(this.M41_Click);
            // 
            // M42
            // 
            this.M42.Name = "M42";
            this.M42.Size = new System.Drawing.Size(172, 22);
            this.M42.Text = "失信人员查询";
            this.M42.Click += new System.EventHandler(this.M42_Click);
            // 
            // M43
            // 
            this.M43.Name = "M43";
            this.M43.Size = new System.Drawing.Size(172, 22);
            this.M43.Text = "企业贷款信息查询";
            this.M43.Click += new System.EventHandler(this.M43_Click);
            // 
            // M44
            // 
            this.M44.Name = "M44";
            this.M44.Size = new System.Drawing.Size(172, 22);
            this.M44.Text = "银行征信";
            this.M44.Click += new System.EventHandler(this.M44_Click);
            // 
            // M50
            // 
            this.M50.Name = "M50";
            this.M50.Size = new System.Drawing.Size(68, 21);
            this.M50.Text = "行业资讯";
            this.M50.Click += new System.EventHandler(this.M50_Click);
            // 
            // M60
            // 
            this.M60.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.M61,
            this.M62});
            this.M60.Image = ((System.Drawing.Image)(resources.GetObject("M60.Image")));
            this.M60.Name = "M60";
            this.M60.Size = new System.Drawing.Size(84, 21);
            this.M60.Text = "用户管理";
            // 
            // M61
            // 
            this.M61.Name = "M61";
            this.M61.Size = new System.Drawing.Size(177, 22);
            this.M61.Text = "查看/修改用户信息";
            this.M61.Click += new System.EventHandler(this.M61_Click);
            // 
            // M62
            // 
            this.M62.Name = "M62";
            this.M62.Size = new System.Drawing.Size(177, 22);
            this.M62.Text = "修改密码";
            this.M62.Click += new System.EventHandler(this.M62_Click);
            // 
            // N70
            // 
            this.N70.Image = ((System.Drawing.Image)(resources.GetObject("N70.Image")));
            this.N70.Name = "N70";
            this.N70.Size = new System.Drawing.Size(84, 21);
            this.N70.Text = "退出登录";
            this.N70.Click += new System.EventHandler(this.N70_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(784, 511);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "信导-社会借贷数据共享中心";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem M10;
        private System.Windows.Forms.ToolStripMenuItem M11;
        private System.Windows.Forms.ToolStripMenuItem M12;
        private System.Windows.Forms.ToolStripMenuItem M20;
        private System.Windows.Forms.ToolStripMenuItem M30;
        private System.Windows.Forms.ToolStripMenuItem M40;
        private System.Windows.Forms.ToolStripMenuItem M50;
        private System.Windows.Forms.ToolStripMenuItem M13;
        private System.Windows.Forms.ToolStripMenuItem M21;
        private System.Windows.Forms.ToolStripMenuItem M22;
        private System.Windows.Forms.ToolStripMenuItem M31;
        private System.Windows.Forms.ToolStripMenuItem M32;
        private System.Windows.Forms.ToolStripMenuItem M33;
        private System.Windows.Forms.ToolStripMenuItem M34;
        private System.Windows.Forms.ToolStripMenuItem M41;
        private System.Windows.Forms.ToolStripMenuItem M42;
        private System.Windows.Forms.ToolStripMenuItem M43;
        private System.Windows.Forms.ToolStripMenuItem M44;
        private System.Windows.Forms.ToolStripMenuItem M60;
        private System.Windows.Forms.ToolStripMenuItem M61;
        private System.Windows.Forms.ToolStripMenuItem M62;
        private System.Windows.Forms.ToolStripMenuItem N70;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsShowUser;
        private System.Windows.Forms.ToolStripStatusLabel tsShowTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem N35;
        private System.Windows.Forms.ToolStripMenuItem M111;
        private System.Windows.Forms.ToolStripMenuItem M112;
        private System.Windows.Forms.ToolStripMenuItem M113;
        private PictureBox pictureBox1;
    }
}

