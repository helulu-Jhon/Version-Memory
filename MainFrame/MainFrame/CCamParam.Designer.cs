namespace MainFrame
{
    partial class CCamParam
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBoxCam1 = new System.Windows.Forms.GroupBox();
            this.btnCam1Clear = new System.Windows.Forms.Button();
            this.textBoxCam1Index = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCam1Modification = new System.Windows.Forms.Button();
            this.btnCam1Remove = new System.Windows.Forms.Button();
            this.btnCam1Add = new System.Windows.Forms.Button();
            this.textBoxCam1Value = new System.Windows.Forms.TextBox();
            this.textBoxCam1Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewCam1Param = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExplain = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxCam1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(857, 336);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBoxCam1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(849, 310);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "相机1参数";
            // 
            // groupBoxCam1
            // 
            this.groupBoxCam1.Controls.Add(this.btnCam1Clear);
            this.groupBoxCam1.Controls.Add(this.textBoxCam1Index);
            this.groupBoxCam1.Controls.Add(this.label2);
            this.groupBoxCam1.Controls.Add(this.btnCam1Modification);
            this.groupBoxCam1.Controls.Add(this.btnCam1Remove);
            this.groupBoxCam1.Controls.Add(this.btnCam1Add);
            this.groupBoxCam1.Controls.Add(this.textBoxCam1Value);
            this.groupBoxCam1.Controls.Add(this.textBoxCam1Name);
            this.groupBoxCam1.Controls.Add(this.label1);
            this.groupBoxCam1.Controls.Add(this.listViewCam1Param);
            this.groupBoxCam1.Location = new System.Drawing.Point(6, 6);
            this.groupBoxCam1.Name = "groupBoxCam1";
            this.groupBoxCam1.Size = new System.Drawing.Size(837, 296);
            this.groupBoxCam1.TabIndex = 4;
            this.groupBoxCam1.TabStop = false;
            this.groupBoxCam1.Text = "相机1参数表";
            // 
            // btnCam1Clear
            // 
            this.btnCam1Clear.Location = new System.Drawing.Point(89, 259);
            this.btnCam1Clear.Name = "btnCam1Clear";
            this.btnCam1Clear.Size = new System.Drawing.Size(75, 23);
            this.btnCam1Clear.TabIndex = 9;
            this.btnCam1Clear.Text = "清空";
            this.btnCam1Clear.UseVisualStyleBackColor = true;
            this.btnCam1Clear.Click += new System.EventHandler(this.btnCam1Clear_Click);
            // 
            // textBoxCam1Index
            // 
            this.textBoxCam1Index.Location = new System.Drawing.Point(64, 172);
            this.textBoxCam1Index.Name = "textBoxCam1Index";
            this.textBoxCam1Index.ReadOnly = true;
            this.textBoxCam1Index.Size = new System.Drawing.Size(232, 21);
            this.textBoxCam1Index.TabIndex = 8;
            this.textBoxCam1Index.Text = "-1";
            this.textBoxCam1Index.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "当前项:";
            // 
            // btnCam1Modification
            // 
            this.btnCam1Modification.Location = new System.Drawing.Point(89, 230);
            this.btnCam1Modification.Name = "btnCam1Modification";
            this.btnCam1Modification.Size = new System.Drawing.Size(75, 23);
            this.btnCam1Modification.TabIndex = 6;
            this.btnCam1Modification.Text = "修改";
            this.btnCam1Modification.UseVisualStyleBackColor = true;
            this.btnCam1Modification.Click += new System.EventHandler(this.btnCam1Modification_Click);
            // 
            // btnCam1Remove
            // 
            this.btnCam1Remove.Location = new System.Drawing.Point(8, 259);
            this.btnCam1Remove.Name = "btnCam1Remove";
            this.btnCam1Remove.Size = new System.Drawing.Size(75, 23);
            this.btnCam1Remove.TabIndex = 5;
            this.btnCam1Remove.Text = "移除";
            this.btnCam1Remove.UseVisualStyleBackColor = true;
            this.btnCam1Remove.Click += new System.EventHandler(this.btnCam1Remove_Click);
            // 
            // btnCam1Add
            // 
            this.btnCam1Add.Location = new System.Drawing.Point(8, 230);
            this.btnCam1Add.Name = "btnCam1Add";
            this.btnCam1Add.Size = new System.Drawing.Size(75, 23);
            this.btnCam1Add.TabIndex = 4;
            this.btnCam1Add.Text = "添加";
            this.btnCam1Add.UseVisualStyleBackColor = true;
            this.btnCam1Add.Click += new System.EventHandler(this.btnCam1Add_Click);
            // 
            // textBoxCam1Value
            // 
            this.textBoxCam1Value.Location = new System.Drawing.Point(181, 199);
            this.textBoxCam1Value.Name = "textBoxCam1Value";
            this.textBoxCam1Value.Size = new System.Drawing.Size(115, 21);
            this.textBoxCam1Value.TabIndex = 3;
            this.textBoxCam1Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxCam1Name
            // 
            this.textBoxCam1Name.Location = new System.Drawing.Point(64, 199);
            this.textBoxCam1Name.Name = "textBoxCam1Name";
            this.textBoxCam1Name.Size = new System.Drawing.Size(111, 21);
            this.textBoxCam1Name.TabIndex = 2;
            this.textBoxCam1Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "序列:";
            // 
            // listViewCam1Param
            // 
            this.listViewCam1Param.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewCam1Param.FullRowSelect = true;
            this.listViewCam1Param.GridLines = true;
            this.listViewCam1Param.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewCam1Param.Location = new System.Drawing.Point(7, 21);
            this.listViewCam1Param.Name = "listViewCam1Param";
            this.listViewCam1Param.Size = new System.Drawing.Size(289, 147);
            this.listViewCam1Param.TabIndex = 0;
            this.listViewCam1Param.UseCompatibleStateImageBehavior = false;
            this.listViewCam1Param.View = System.Windows.Forms.View.Details;
            this.listViewCam1Param.SelectedIndexChanged += new System.EventHandler(this.listViewCam1Param_SelectedIndexChanged);
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
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "数值";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 120;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(849, 310);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "相机2参数";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(825, 296);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数表";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(849, 310);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "相机3参数";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(825, 296);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "参数表";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(849, 310);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "相机4参数";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(3, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(825, 296);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "参数表";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(98, 355);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExplain
            // 
            this.btnExplain.Location = new System.Drawing.Point(17, 355);
            this.btnExplain.Name = "btnExplain";
            this.btnExplain.Size = new System.Drawing.Size(75, 23);
            this.btnExplain.TabIndex = 6;
            this.btnExplain.Text = "说明";
            this.btnExplain.UseVisualStyleBackColor = true;
            this.btnExplain.Click += new System.EventHandler(this.btnExplain_Click);
            // 
            // CCamParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 388);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExplain);
            this.Controls.Add(this.tabControl1);
            this.Name = "CCamParam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.CCamParam_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBoxCam1.ResumeLayout(false);
            this.groupBoxCam1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBoxCam1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExplain;
        private System.Windows.Forms.ListView listViewCam1Param;
        private System.Windows.Forms.Button btnCam1Clear;
        private System.Windows.Forms.TextBox textBoxCam1Index;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCam1Modification;
        private System.Windows.Forms.Button btnCam1Remove;
        private System.Windows.Forms.Button btnCam1Add;
        private System.Windows.Forms.TextBox textBoxCam1Value;
        private System.Windows.Forms.TextBox textBoxCam1Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;

    }
}