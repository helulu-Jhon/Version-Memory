namespace MainFrame
{
    partial class CSetSys
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxImageDelTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonNOSaveImage = new System.Windows.Forms.RadioButton();
            this.radioButtonSaveImage = new System.Windows.Forms.RadioButton();
            this.btnSelectImagePath = new System.Windows.Forms.Button();
            this.textBoxImagePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonFourCam = new System.Windows.Forms.RadioButton();
            this.radioButtonThreeCam = new System.Windows.Forms.RadioButton();
            this.radioButtonTwoCam = new System.Windows.Forms.RadioButton();
            this.radioButtonOneCam = new System.Windows.Forms.RadioButton();
            this.btnSaveParam = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSelectFilePath = new System.Windows.Forms.Button();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDefaultImagePath = new System.Windows.Forms.Button();
            this.btnDefaultFilePath = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDefaultImagePath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxImageDelTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.radioButtonNOSaveImage);
            this.groupBox1.Controls.Add(this.radioButtonSaveImage);
            this.groupBox1.Controls.Add(this.btnSelectImagePath);
            this.groupBox1.Controls.Add(this.textBoxImagePath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图像设置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(174, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "天";
            // 
            // textBoxImageDelTime
            // 
            this.textBoxImageDelTime.Location = new System.Drawing.Point(93, 57);
            this.textBoxImageDelTime.Name = "textBoxImageDelTime";
            this.textBoxImageDelTime.Size = new System.Drawing.Size(75, 21);
            this.textBoxImageDelTime.TabIndex = 6;
            this.textBoxImageDelTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "图像保留时间:";
            // 
            // radioButtonNOSaveImage
            // 
            this.radioButtonNOSaveImage.AutoSize = true;
            this.radioButtonNOSaveImage.Location = new System.Drawing.Point(260, 60);
            this.radioButtonNOSaveImage.Name = "radioButtonNOSaveImage";
            this.radioButtonNOSaveImage.Size = new System.Drawing.Size(95, 16);
            this.radioButtonNOSaveImage.TabIndex = 4;
            this.radioButtonNOSaveImage.TabStop = true;
            this.radioButtonNOSaveImage.Text = "不存储OK图像";
            this.radioButtonNOSaveImage.UseVisualStyleBackColor = true;
            // 
            // radioButtonSaveImage
            // 
            this.radioButtonSaveImage.AutoSize = true;
            this.radioButtonSaveImage.Location = new System.Drawing.Point(361, 60);
            this.radioButtonSaveImage.Name = "radioButtonSaveImage";
            this.radioButtonSaveImage.Size = new System.Drawing.Size(83, 16);
            this.radioButtonSaveImage.TabIndex = 3;
            this.radioButtonSaveImage.TabStop = true;
            this.radioButtonSaveImage.Text = "存储OK图像";
            this.radioButtonSaveImage.UseVisualStyleBackColor = true;
            // 
            // btnSelectImagePath
            // 
            this.btnSelectImagePath.Location = new System.Drawing.Point(313, 18);
            this.btnSelectImagePath.Name = "btnSelectImagePath";
            this.btnSelectImagePath.Size = new System.Drawing.Size(63, 23);
            this.btnSelectImagePath.TabIndex = 2;
            this.btnSelectImagePath.Text = "选择路径";
            this.btnSelectImagePath.UseVisualStyleBackColor = true;
            this.btnSelectImagePath.Click += new System.EventHandler(this.btnSelectImagePath_Click);
            // 
            // textBoxImagePath
            // 
            this.textBoxImagePath.Location = new System.Drawing.Point(93, 18);
            this.textBoxImagePath.Name = "textBoxImagePath";
            this.textBoxImagePath.ReadOnly = true;
            this.textBoxImagePath.Size = new System.Drawing.Size(214, 21);
            this.textBoxImagePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "存储路径:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonFourCam);
            this.groupBox2.Controls.Add(this.radioButtonThreeCam);
            this.groupBox2.Controls.Add(this.radioButtonTwoCam);
            this.groupBox2.Controls.Add(this.radioButtonOneCam);
            this.groupBox2.Location = new System.Drawing.Point(13, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 50);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "系统模式";
            // 
            // radioButtonFourCam
            // 
            this.radioButtonFourCam.AutoSize = true;
            this.radioButtonFourCam.Location = new System.Drawing.Point(349, 21);
            this.radioButtonFourCam.Name = "radioButtonFourCam";
            this.radioButtonFourCam.Size = new System.Drawing.Size(59, 16);
            this.radioButtonFourCam.TabIndex = 3;
            this.radioButtonFourCam.TabStop = true;
            this.radioButtonFourCam.Text = "四相机";
            this.radioButtonFourCam.UseVisualStyleBackColor = true;
            // 
            // radioButtonThreeCam
            // 
            this.radioButtonThreeCam.AutoSize = true;
            this.radioButtonThreeCam.Location = new System.Drawing.Point(235, 21);
            this.radioButtonThreeCam.Name = "radioButtonThreeCam";
            this.radioButtonThreeCam.Size = new System.Drawing.Size(59, 16);
            this.radioButtonThreeCam.TabIndex = 2;
            this.radioButtonThreeCam.TabStop = true;
            this.radioButtonThreeCam.Text = "三相机";
            this.radioButtonThreeCam.UseVisualStyleBackColor = true;
            // 
            // radioButtonTwoCam
            // 
            this.radioButtonTwoCam.AutoSize = true;
            this.radioButtonTwoCam.Location = new System.Drawing.Point(121, 21);
            this.radioButtonTwoCam.Name = "radioButtonTwoCam";
            this.radioButtonTwoCam.Size = new System.Drawing.Size(59, 16);
            this.radioButtonTwoCam.TabIndex = 1;
            this.radioButtonTwoCam.TabStop = true;
            this.radioButtonTwoCam.Text = "双相机";
            this.radioButtonTwoCam.UseVisualStyleBackColor = true;
            // 
            // radioButtonOneCam
            // 
            this.radioButtonOneCam.AutoSize = true;
            this.radioButtonOneCam.Location = new System.Drawing.Point(7, 21);
            this.radioButtonOneCam.Name = "radioButtonOneCam";
            this.radioButtonOneCam.Size = new System.Drawing.Size(59, 16);
            this.radioButtonOneCam.TabIndex = 0;
            this.radioButtonOneCam.TabStop = true;
            this.radioButtonOneCam.Text = "单相机";
            this.radioButtonOneCam.UseVisualStyleBackColor = true;
            // 
            // btnSaveParam
            // 
            this.btnSaveParam.Location = new System.Drawing.Point(388, 254);
            this.btnSaveParam.Name = "btnSaveParam";
            this.btnSaveParam.Size = new System.Drawing.Size(75, 23);
            this.btnSaveParam.TabIndex = 2;
            this.btnSaveParam.Text = "保存";
            this.btnSaveParam.UseVisualStyleBackColor = true;
            this.btnSaveParam.Click += new System.EventHandler(this.btnSaveParam_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDefaultFilePath);
            this.groupBox3.Controls.Add(this.btnSelectFilePath);
            this.groupBox3.Controls.Add(this.textBoxFilePath);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(13, 167);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(450, 56);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "文件设置";
            // 
            // btnSelectFilePath
            // 
            this.btnSelectFilePath.Location = new System.Drawing.Point(314, 18);
            this.btnSelectFilePath.Name = "btnSelectFilePath";
            this.btnSelectFilePath.Size = new System.Drawing.Size(62, 23);
            this.btnSelectFilePath.TabIndex = 10;
            this.btnSelectFilePath.Text = "选择路径";
            this.btnSelectFilePath.UseVisualStyleBackColor = true;
            this.btnSelectFilePath.Click += new System.EventHandler(this.btnSelectFilePath_Click);
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(93, 20);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.ReadOnly = true;
            this.textBoxFilePath.Size = new System.Drawing.Size(214, 21);
            this.textBoxFilePath.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "存储路径:";
            // 
            // btnDefaultImagePath
            // 
            this.btnDefaultImagePath.Location = new System.Drawing.Point(382, 18);
            this.btnDefaultImagePath.Name = "btnDefaultImagePath";
            this.btnDefaultImagePath.Size = new System.Drawing.Size(62, 23);
            this.btnDefaultImagePath.TabIndex = 8;
            this.btnDefaultImagePath.Text = "默认路径";
            this.btnDefaultImagePath.UseVisualStyleBackColor = true;
            this.btnDefaultImagePath.Click += new System.EventHandler(this.btnDefaultImagePath_Click);
            // 
            // btnDefaultFilePath
            // 
            this.btnDefaultFilePath.Location = new System.Drawing.Point(382, 18);
            this.btnDefaultFilePath.Name = "btnDefaultFilePath";
            this.btnDefaultFilePath.Size = new System.Drawing.Size(62, 23);
            this.btnDefaultFilePath.TabIndex = 11;
            this.btnDefaultFilePath.Text = "默认路径";
            this.btnDefaultFilePath.UseVisualStyleBackColor = true;
            this.btnDefaultFilePath.Click += new System.EventHandler(this.btnDefaultFilePath_Click);
            // 
            // CSetSys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 289);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSaveParam);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CSetSys";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CSetSys_FormClosing);
            this.Load += new System.EventHandler(this.CSetSys_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelectImagePath;
        private System.Windows.Forms.TextBox textBoxImagePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonNOSaveImage;
        private System.Windows.Forms.RadioButton radioButtonSaveImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxImageDelTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonFourCam;
        private System.Windows.Forms.RadioButton radioButtonThreeCam;
        private System.Windows.Forms.RadioButton radioButtonTwoCam;
        private System.Windows.Forms.RadioButton radioButtonOneCam;
        private System.Windows.Forms.Button btnSaveParam;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSelectFilePath;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDefaultImagePath;
        private System.Windows.Forms.Button btnDefaultFilePath;
    }
}