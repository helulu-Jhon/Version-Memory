namespace MainFrame
{
    partial class CMainFrame
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_X = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Y = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Grayvalue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Cma1Statu = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Cma2Statu = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Cma3Statu = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Cma4Statu = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_RunStatu = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Time = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_User = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.初始化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadConfig_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteConfig_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Initial_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Param_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通信设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.串口设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网口设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetUser_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetSys_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单次运行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单次离线运行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartRun_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dmTcpClient = new DMSkin.Socket.DMTcpClient(this.components);
            this.dmTcpServer = new DMSkin.Socket.DMTcpServer(this.components);
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxOutPut = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxConfigName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.btnResertNum = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.hWindowControl3 = new HalconDotNet.HWindowControl();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.hWindowControl2 = new HalconDotNet.HWindowControl();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.hWindowControl4 = new HalconDotNet.HWindowControl();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(3, 17);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(342, 228);
            this.hWindowControl1.TabIndex = 2;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(342, 228);
            this.hWindowControl1.HMouseMove += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseMove);
            this.hWindowControl1.HMouseDown += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseDown);
            this.hWindowControl1.HMouseUp += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseUp);
            this.hWindowControl1.HMouseWheel += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseWheel);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_X,
            this.toolStripStatusLabel_Y,
            this.toolStripStatusLabel_Grayvalue,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel_Cma1Statu,
            this.toolStripStatusLabel_Cma2Statu,
            this.toolStripStatusLabel_Cma3Statu,
            this.toolStripStatusLabel_Cma4Statu,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel_RunStatu,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel_Time,
            this.toolStripStatusLabel_User});
            this.statusStrip.Location = new System.Drawing.Point(0, 518);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1052, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_X
            // 
            this.toolStripStatusLabel_X.Name = "toolStripStatusLabel_X";
            this.toolStripStatusLabel_X.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel_X.Text = "X:0    ";
            // 
            // toolStripStatusLabel_Y
            // 
            this.toolStripStatusLabel_Y.Name = "toolStripStatusLabel_Y";
            this.toolStripStatusLabel_Y.Size = new System.Drawing.Size(41, 17);
            this.toolStripStatusLabel_Y.Text = "Y:0    ";
            // 
            // toolStripStatusLabel_Grayvalue
            // 
            this.toolStripStatusLabel_Grayvalue.Name = "toolStripStatusLabel_Grayvalue";
            this.toolStripStatusLabel_Grayvalue.Size = new System.Drawing.Size(43, 17);
            this.toolStripStatusLabel_Grayvalue.Text = "G:0    ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel_Cma1Statu
            // 
            this.toolStripStatusLabel_Cma1Statu.Name = "toolStripStatusLabel_Cma1Statu";
            this.toolStripStatusLabel_Cma1Statu.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel_Cma1Statu.Text = "相机1状态";
            // 
            // toolStripStatusLabel_Cma2Statu
            // 
            this.toolStripStatusLabel_Cma2Statu.Name = "toolStripStatusLabel_Cma2Statu";
            this.toolStripStatusLabel_Cma2Statu.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel_Cma2Statu.Text = "相机2状态";
            // 
            // toolStripStatusLabel_Cma3Statu
            // 
            this.toolStripStatusLabel_Cma3Statu.Name = "toolStripStatusLabel_Cma3Statu";
            this.toolStripStatusLabel_Cma3Statu.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel_Cma3Statu.Text = "相机3状态";
            // 
            // toolStripStatusLabel_Cma4Statu
            // 
            this.toolStripStatusLabel_Cma4Statu.Name = "toolStripStatusLabel_Cma4Statu";
            this.toolStripStatusLabel_Cma4Statu.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel_Cma4Statu.Text = "相机4状态";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(210, 17);
            this.toolStripStatusLabel3.Spring = true;
            this.toolStripStatusLabel3.Text = "          ";
            // 
            // toolStripStatusLabel_RunStatu
            // 
            this.toolStripStatusLabel_RunStatu.Name = "toolStripStatusLabel_RunStatu";
            this.toolStripStatusLabel_RunStatu.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel_RunStatu.Text = "运行状态";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(210, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel_Time
            // 
            this.toolStripStatusLabel_Time.Name = "toolStripStatusLabel_Time";
            this.toolStripStatusLabel_Time.Size = new System.Drawing.Size(126, 17);
            this.toolStripStatusLabel_Time.Text = "0000-00-00 00:00:00";
            // 
            // toolStripStatusLabel_User
            // 
            this.toolStripStatusLabel_User.Name = "toolStripStatusLabel_User";
            this.toolStripStatusLabel_User.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel_User.Text = "当前用户";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.初始化ToolStripMenuItem,
            this.编辑参数ToolStripMenuItem,
            this.通信设置ToolStripMenuItem,
            this.SetUser_ToolStripMenuItem,
            this.SetSys_ToolStripMenuItem,
            this.运行ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1052, 25);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // 初始化ToolStripMenuItem
            // 
            this.初始化ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadConfig_ToolStripMenuItem,
            this.DeleteConfig_ToolStripMenuItem,
            this.Initial_ToolStripMenuItem});
            this.初始化ToolStripMenuItem.Name = "初始化ToolStripMenuItem";
            this.初始化ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.初始化ToolStripMenuItem.Text = "初始化";
            // 
            // LoadConfig_ToolStripMenuItem
            // 
            this.LoadConfig_ToolStripMenuItem.Name = "LoadConfig_ToolStripMenuItem";
            this.LoadConfig_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.LoadConfig_ToolStripMenuItem.Text = "加载配置";
            this.LoadConfig_ToolStripMenuItem.Click += new System.EventHandler(this.LoadConfig_ToolStripMenuItem_Click);
            // 
            // DeleteConfig_ToolStripMenuItem
            // 
            this.DeleteConfig_ToolStripMenuItem.Name = "DeleteConfig_ToolStripMenuItem";
            this.DeleteConfig_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DeleteConfig_ToolStripMenuItem.Text = "删除配置";
            this.DeleteConfig_ToolStripMenuItem.Click += new System.EventHandler(this.DeleteConfig_ToolStripMenuItem_Click);
            // 
            // Initial_ToolStripMenuItem
            // 
            this.Initial_ToolStripMenuItem.Name = "Initial_ToolStripMenuItem";
            this.Initial_ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.Initial_ToolStripMenuItem.Text = "初始化程序";
            this.Initial_ToolStripMenuItem.Click += new System.EventHandler(this.Initial_ToolStripMenuItem_Click);
            // 
            // 编辑参数ToolStripMenuItem
            // 
            this.编辑参数ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Measure_ToolStripMenuItem,
            this.Param_ToolStripMenuItem});
            this.编辑参数ToolStripMenuItem.Name = "编辑参数ToolStripMenuItem";
            this.编辑参数ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑参数ToolStripMenuItem.Text = "编辑";
            // 
            // Measure_ToolStripMenuItem
            // 
            this.Measure_ToolStripMenuItem.Name = "Measure_ToolStripMenuItem";
            this.Measure_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.Measure_ToolStripMenuItem.Text = "测量设置";
            this.Measure_ToolStripMenuItem.Click += new System.EventHandler(this.Measure_ToolStripMenuItem_Click);
            // 
            // Param_ToolStripMenuItem
            // 
            this.Param_ToolStripMenuItem.Name = "Param_ToolStripMenuItem";
            this.Param_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.Param_ToolStripMenuItem.Text = "参数设置";
            this.Param_ToolStripMenuItem.Click += new System.EventHandler(this.Param_ToolStripMenuItem_Click);
            // 
            // 通信设置ToolStripMenuItem
            // 
            this.通信设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.串口设置ToolStripMenuItem,
            this.网口设置ToolStripMenuItem});
            this.通信设置ToolStripMenuItem.Name = "通信设置ToolStripMenuItem";
            this.通信设置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.通信设置ToolStripMenuItem.Text = "通信设置";
            // 
            // 串口设置ToolStripMenuItem
            // 
            this.串口设置ToolStripMenuItem.Name = "串口设置ToolStripMenuItem";
            this.串口设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.串口设置ToolStripMenuItem.Text = "串口设置";
            // 
            // 网口设置ToolStripMenuItem
            // 
            this.网口设置ToolStripMenuItem.Name = "网口设置ToolStripMenuItem";
            this.网口设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.网口设置ToolStripMenuItem.Text = "网口设置";
            // 
            // SetUser_ToolStripMenuItem
            // 
            this.SetUser_ToolStripMenuItem.Name = "SetUser_ToolStripMenuItem";
            this.SetUser_ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.SetUser_ToolStripMenuItem.Text = "用户管理";
            this.SetUser_ToolStripMenuItem.Click += new System.EventHandler(this.SetUser_ToolStripMenuItem_Click);
            // 
            // SetSys_ToolStripMenuItem
            // 
            this.SetSys_ToolStripMenuItem.Name = "SetSys_ToolStripMenuItem";
            this.SetSys_ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.SetSys_ToolStripMenuItem.Text = "系统设置";
            this.SetSys_ToolStripMenuItem.Click += new System.EventHandler(this.SetSys_ToolStripMenuItem_Click);
            // 
            // 运行ToolStripMenuItem
            // 
            this.运行ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.单次运行ToolStripMenuItem,
            this.单次离线运行ToolStripMenuItem,
            this.StartRun_ToolStripMenuItem});
            this.运行ToolStripMenuItem.Name = "运行ToolStripMenuItem";
            this.运行ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.运行ToolStripMenuItem.Text = "运行";
            // 
            // 单次运行ToolStripMenuItem
            // 
            this.单次运行ToolStripMenuItem.Name = "单次运行ToolStripMenuItem";
            this.单次运行ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.单次运行ToolStripMenuItem.Text = "单次运行";
            // 
            // 单次离线运行ToolStripMenuItem
            // 
            this.单次离线运行ToolStripMenuItem.Name = "单次离线运行ToolStripMenuItem";
            this.单次离线运行ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.单次离线运行ToolStripMenuItem.Text = "单次离线运行";
            // 
            // StartRun_ToolStripMenuItem
            // 
            this.StartRun_ToolStripMenuItem.Name = "StartRun_ToolStripMenuItem";
            this.StartRun_ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.StartRun_ToolStripMenuItem.Text = "开始运行";
            this.StartRun_ToolStripMenuItem.Click += new System.EventHandler(this.StartRun_ToolStripMenuItem_Click);
            // 
            // dmTcpClient
            // 
            this.dmTcpClient.Isclosed = false;
            this.dmTcpClient.IsStartTcpthreading = false;
            this.dmTcpClient.Receivestr = null;
            this.dmTcpClient.ReConectedCount = 0;
            this.dmTcpClient.ReConnectionTime = 3000;
            this.dmTcpClient.ServerIp = "127.0.0.1";
            this.dmTcpClient.ServerPort = 3000;
            this.dmTcpClient.Tcpclient = null;
            this.dmTcpClient.Tcpthread = null;
            this.dmTcpClient.OnReceviceByte += new DMSkin.Socket.DMTcpClient.ReceviceByteEventHandler(this.dmTcpClient_OnReceviceByte);
            // 
            // dmTcpServer
            // 
            this.dmTcpServer.ServerIp = "127.0.0.1";
            this.dmTcpServer.ServerPort = 3000;
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxOutPut);
            this.groupBox2.Location = new System.Drawing.Point(720, 352);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 163);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出";
            // 
            // listBoxOutPut
            // 
            this.listBoxOutPut.FormattingEnabled = true;
            this.listBoxOutPut.HorizontalScrollbar = true;
            this.listBoxOutPut.ItemHeight = 12;
            this.listBoxOutPut.Location = new System.Drawing.Point(6, 20);
            this.listBoxOutPut.Name = "listBoxOutPut";
            this.listBoxOutPut.Size = new System.Drawing.Size(308, 136);
            this.listBoxOutPut.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listView);
            this.groupBox3.Location = new System.Drawing.Point(862, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(178, 147);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "机种选择";
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listView.Location = new System.Drawing.Point(3, 17);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(172, 127);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序列";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "机种编号";
            this.columnHeader2.Width = 141;
            // 
            // textBoxConfigName
            // 
            this.textBoxConfigName.Location = new System.Drawing.Point(868, 176);
            this.textBoxConfigName.Name = "textBoxConfigName";
            this.textBoxConfigName.ReadOnly = true;
            this.textBoxConfigName.Size = new System.Drawing.Size(165, 21);
            this.textBoxConfigName.TabIndex = 9;
            this.textBoxConfigName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hWindowControl1);
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 248);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "相机1";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.textBox17);
            this.groupBox7.Controls.Add(this.btnResertNum);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.textBox4);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.textBox3);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Controls.Add(this.textBox2);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.textBox1);
            this.groupBox7.Location = new System.Drawing.Point(720, 23);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(136, 182);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "结果";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(8, 153);
            this.textBox17.Name = "textBox17";
            this.textBox17.ReadOnly = true;
            this.textBox17.Size = new System.Drawing.Size(122, 21);
            this.textBox17.TabIndex = 9;
            // 
            // btnResertNum
            // 
            this.btnResertNum.Location = new System.Drawing.Point(8, 123);
            this.btnResertNum.Name = "btnResertNum";
            this.btnResertNum.Size = new System.Drawing.Size(122, 23);
            this.btnResertNum.TabIndex = 8;
            this.btnResertNum.Text = "清零";
            this.btnResertNum.UseVisualStyleBackColor = true;
            this.btnResertNum.Click += new System.EventHandler(this.btnResertNum_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "良率:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(47, 96);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(83, 21);
            this.textBox4.TabIndex = 6;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "NG数:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(47, 69);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(83, 21);
            this.textBox3.TabIndex = 4;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "OK数:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(47, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(83, 21);
            this.textBox2.TabIndex = 2;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "总数:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(47, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(83, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.tabControl);
            this.groupBox8.Location = new System.Drawing.Point(721, 211);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(319, 135);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "预留参数";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 17);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(313, 115);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.textBox6);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.textBox8);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.textBox10);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.textBox9);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.textBox7);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.textBox5);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(305, 89);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "统计量1";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(232, 60);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(67, 21);
            this.textBox6.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(159, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "相机2-良率:";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(232, 33);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(67, 21);
            this.textBox8.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(167, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "相机2-OK:";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(232, 6);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(67, 21);
            this.textBox10.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(155, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "相机2-总数:";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(79, 60);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(67, 21);
            this.textBox9.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "相机1-良率:";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(79, 33);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(67, 21);
            this.textBox7.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "相机1-OK:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(79, 6);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(67, 21);
            this.textBox5.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "相机1-总数:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.textBox11);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.textBox14);
            this.tabPage2.Controls.Add(this.textBox15);
            this.tabPage2.Controls.Add(this.textBox12);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.textBox13);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.textBox16);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(305, 89);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "统计量2";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(233, 60);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(67, 21);
            this.textBox11.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(156, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 12);
            this.label13.TabIndex = 22;
            this.label13.Text = "相机4-总数:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 20;
            this.label14.Text = "相机3-良率:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(160, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 26;
            this.label11.Text = "相机4-良率:";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(80, 60);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(67, 21);
            this.textBox14.TabIndex = 21;
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(80, 33);
            this.textBox15.Name = "textBox15";
            this.textBox15.ReadOnly = true;
            this.textBox15.Size = new System.Drawing.Size(67, 21);
            this.textBox15.TabIndex = 19;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(233, 33);
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(67, 21);
            this.textBox12.TabIndex = 25;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 36);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 12);
            this.label15.TabIndex = 18;
            this.label15.Text = "相机3-OK:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 12);
            this.label16.TabIndex = 16;
            this.label16.Text = "相机3-总数:";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(233, 6);
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(67, 21);
            this.textBox13.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(168, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 24;
            this.label12.Text = "相机4-OK:";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(80, 6);
            this.textBox16.Name = "textBox16";
            this.textBox16.ReadOnly = true;
            this.textBox16.Size = new System.Drawing.Size(67, 21);
            this.textBox16.TabIndex = 17;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(305, 89);
            this.tabPage3.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.hWindowControl3);
            this.groupBox4.Location = new System.Drawing.Point(13, 277);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(348, 238);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "相机3";
            // 
            // hWindowControl3
            // 
            this.hWindowControl3.BackColor = System.Drawing.Color.Black;
            this.hWindowControl3.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl3.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl3.Location = new System.Drawing.Point(3, 17);
            this.hWindowControl3.Name = "hWindowControl3";
            this.hWindowControl3.Size = new System.Drawing.Size(342, 218);
            this.hWindowControl3.TabIndex = 2;
            this.hWindowControl3.WindowSize = new System.Drawing.Size(342, 218);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.hWindowControl2);
            this.groupBox5.Location = new System.Drawing.Point(363, 23);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(348, 248);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "相机2";
            // 
            // hWindowControl2
            // 
            this.hWindowControl2.BackColor = System.Drawing.Color.Black;
            this.hWindowControl2.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl2.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl2.Location = new System.Drawing.Point(3, 17);
            this.hWindowControl2.Name = "hWindowControl2";
            this.hWindowControl2.Size = new System.Drawing.Size(342, 228);
            this.hWindowControl2.TabIndex = 2;
            this.hWindowControl2.WindowSize = new System.Drawing.Size(342, 228);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.hWindowControl4);
            this.groupBox6.Location = new System.Drawing.Point(367, 277);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(348, 238);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "相机4";
            // 
            // hWindowControl4
            // 
            this.hWindowControl4.BackColor = System.Drawing.Color.Black;
            this.hWindowControl4.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl4.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl4.Location = new System.Drawing.Point(3, 17);
            this.hWindowControl4.Name = "hWindowControl4";
            this.hWindowControl4.Size = new System.Drawing.Size(342, 218);
            this.hWindowControl4.TabIndex = 2;
            this.hWindowControl4.WindowSize = new System.Drawing.Size(342, 218);
            // 
            // CMainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 540);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxConfigName);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "CMainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainFrame-0000-00-00";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CMainFrame_FormClosing);
            this.Load += new System.EventHandler(this.CMainFrame_Load);
            this.SizeChanged += new System.EventHandler(this.CHalconTemplate_SizeChanged);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_X;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Y;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Grayvalue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Time;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 初始化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadConfig_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteConfig_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑参数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通信设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetSys_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetUser_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 串口设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 网口设置ToolStripMenuItem;
        private DMSkin.Socket.DMTcpClient dmTcpClient;
        private DMSkin.Socket.DMTcpServer dmTcpServer;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.ToolStripMenuItem 运行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 单次运行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 单次离线运行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartRun_ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBoxOutPut;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Cma1Statu;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_RunStatu;
        private System.Windows.Forms.ToolStripMenuItem Initial_ToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox textBoxConfigName;
        private System.Windows.Forms.ToolStripMenuItem Measure_ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnResertNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private HalconDotNet.HWindowControl hWindowControl3;
        private System.Windows.Forms.GroupBox groupBox5;
        private HalconDotNet.HWindowControl hWindowControl2;
        private System.Windows.Forms.GroupBox groupBox6;
        private HalconDotNet.HWindowControl hWindowControl4;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Cma2Statu;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Cma3Statu;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Cma4Statu;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.ToolStripMenuItem Param_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_User;
    }
}

