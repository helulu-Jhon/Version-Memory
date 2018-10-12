namespace MainFrame
{
    partial class CMeasure
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnContiGrabImage = new System.Windows.Forms.Button();
            this.btnGrabImage = new System.Windows.Forms.Button();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnReadImage = new System.Windows.Forms.Button();
            this.hWindowControl = new HalconDotNet.HWindowControl();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_X = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Y = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Grayvalue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Time = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnContiGrabImage);
            this.groupBox1.Controls.Add(this.btnGrabImage);
            this.groupBox1.Controls.Add(this.btnSaveImage);
            this.groupBox1.Controls.Add(this.btnReadImage);
            this.groupBox1.Location = new System.Drawing.Point(475, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 78);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图像获取";
            // 
            // btnContiGrabImage
            // 
            this.btnContiGrabImage.Location = new System.Drawing.Point(86, 20);
            this.btnContiGrabImage.Name = "btnContiGrabImage";
            this.btnContiGrabImage.Size = new System.Drawing.Size(75, 23);
            this.btnContiGrabImage.TabIndex = 3;
            this.btnContiGrabImage.Text = "连续采集";
            this.btnContiGrabImage.UseVisualStyleBackColor = true;
            this.btnContiGrabImage.Click += new System.EventHandler(this.btnContiGrabImage_Click);
            // 
            // btnGrabImage
            // 
            this.btnGrabImage.Location = new System.Drawing.Point(7, 20);
            this.btnGrabImage.Name = "btnGrabImage";
            this.btnGrabImage.Size = new System.Drawing.Size(75, 23);
            this.btnGrabImage.TabIndex = 2;
            this.btnGrabImage.Text = "采集图像";
            this.btnGrabImage.UseVisualStyleBackColor = true;
            this.btnGrabImage.Click += new System.EventHandler(this.btnGrabImage_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(86, 49);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(75, 23);
            this.btnSaveImage.TabIndex = 1;
            this.btnSaveImage.Text = "保存图像";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnReadImage
            // 
            this.btnReadImage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReadImage.Location = new System.Drawing.Point(7, 49);
            this.btnReadImage.Name = "btnReadImage";
            this.btnReadImage.Size = new System.Drawing.Size(75, 23);
            this.btnReadImage.TabIndex = 0;
            this.btnReadImage.Text = "离线读取";
            this.btnReadImage.UseVisualStyleBackColor = true;
            this.btnReadImage.Click += new System.EventHandler(this.btnReadImage_Click);
            // 
            // hWindowControl
            // 
            this.hWindowControl.BackColor = System.Drawing.Color.Black;
            this.hWindowControl.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl.Location = new System.Drawing.Point(12, 10);
            this.hWindowControl.Name = "hWindowControl";
            this.hWindowControl.Size = new System.Drawing.Size(457, 337);
            this.hWindowControl.TabIndex = 2;
            this.hWindowControl.WindowSize = new System.Drawing.Size(457, 337);
            this.hWindowControl.HMouseMove += new HalconDotNet.HMouseEventHandler(this.hWindowControl_HMouseMove);
            this.hWindowControl.HMouseDown += new HalconDotNet.HMouseEventHandler(this.hWindowControl_HMouseDown);
            this.hWindowControl.HMouseUp += new HalconDotNet.HMouseEventHandler(this.hWindowControl_HMouseUp);
            this.hWindowControl.HMouseWheel += new HalconDotNet.HMouseEventHandler(this.hWindowControl_HMouseWheel);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_X,
            this.toolStripStatusLabel_Y,
            this.toolStripStatusLabel_Grayvalue,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel_Time});
            this.statusStrip.Location = new System.Drawing.Point(0, 403);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(855, 22);
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
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(588, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel_Time
            // 
            this.toolStripStatusLabel_Time.Name = "toolStripStatusLabel_Time";
            this.toolStripStatusLabel_Time.Size = new System.Drawing.Size(126, 17);
            this.toolStripStatusLabel_Time.Text = "2018-06-12 07:06:30";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCircle);
            this.groupBox2.Controls.Add(this.btnLine);
            this.groupBox2.Location = new System.Drawing.Point(476, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "添加测量工具";
            // 
            // btnCircle
            // 
            this.btnCircle.Location = new System.Drawing.Point(85, 21);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(75, 23);
            this.btnCircle.TabIndex = 1;
            this.btnCircle.Text = "圆";
            this.btnCircle.UseVisualStyleBackColor = true;
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(7, 21);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(75, 23);
            this.btnLine.TabIndex = 0;
            this.btnLine.Text = "直线";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(3, 17);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(188, 164);
            this.listView.TabIndex = 5;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序列";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "名称";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 144;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listView);
            this.groupBox3.Location = new System.Drawing.Point(649, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(194, 184);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "测量列表";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(561, 375);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "保存配置";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(652, 377);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(197, 21);
            this.textBox1.TabIndex = 8;
            // 
            // CMeasure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 425);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.hWindowControl);
            this.Controls.Add(this.groupBox1);
            this.Name = "CMeasure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "测量设置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CMeasure_Load);
            this.SizeChanged += new System.EventHandler(this.CHalconTemplate_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReadImage;
        private HalconDotNet.HWindowControl hWindowControl;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_X;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Y;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Grayvalue;
        private System.Windows.Forms.Button btnGrabImage;
        private System.Windows.Forms.Button btnContiGrabImage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Time;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

