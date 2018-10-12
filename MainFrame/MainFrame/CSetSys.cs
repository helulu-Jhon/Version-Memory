using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MainFrame
{
    public partial class CSetSys : Form
    {
        #region 软件参数变量

        public string m_sImageSavePath = System.Environment.CurrentDirectory + "\\ImageData";
        public int m_iDeleteImageTime = 3;
        public bool m_bIsSaveOKImage = false;
        public int m_iSysMode = 1;
        public string m_sFileSavePath = System.Environment.CurrentDirectory + "\\ResultData";

        #endregion

        public CSetSys()
        {
            InitializeComponent();
        }

        private void CSetSys_Load(object sender, EventArgs e)
        {
            try
            {
                #region 图像设置控件初始化

                textBoxImagePath.Text = m_sImageSavePath;
                textBoxImageDelTime.Text = m_iDeleteImageTime.ToString();

                if (m_bIsSaveOKImage == true)
                {
                    radioButtonSaveImage.Checked = true;
                }
                else
                {
                    radioButtonNOSaveImage.Checked = true;
                }

                #endregion

                #region 系统模式控件初始化

                switch (m_iSysMode)
                {
                    case 1:
                        radioButtonOneCam.Checked = true;
                        break;
                    case 2:
                        radioButtonTwoCam.Checked = true;
                        break;
                    case 3:
                        radioButtonThreeCam.Checked = true;
                        break;
                    case 4:
                        radioButtonFourCam.Checked = true;
                        break;
                    default:
                        break;
                }

                #endregion

                #region 文件设置初始化

                textBoxFilePath.Text = m_sFileSavePath;

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("CSetSys_Load:" + ex.Message);
            }
        }

        private void btnSelectImagePath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBoxImagePath.Text = dlg.SelectedPath;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnSelectImagePath_Click:" + ex.Message);
            }
        }

        private void btnSelectFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    textBoxFilePath.Text = dlg.SelectedPath;
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnSelectFilePath_Click:" + ex.Message);
            }
        }

        private void btnDefaultImagePath_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxImagePath.Text = System.Environment.CurrentDirectory + "\\ImageData";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnDefaultImagePath_Click:" + ex.Message);
            }
        }

        private void btnDefaultFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxFilePath.Text = System.Environment.CurrentDirectory + "\\ResultData";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnDefaultFilePath_Click:" + ex.Message);
            }
        }

        private void btnSaveParam_Click(object sender, EventArgs e)
        {
            try
            {
                #region 图像设置参数保存

                m_sImageSavePath = textBoxImagePath.Text;
                m_iDeleteImageTime = int.Parse(textBoxImageDelTime.Text);
                if (m_iDeleteImageTime < 1)
                {
                    MessageBox.Show("保存失败:图像保留时间最小为1天!");
                    return;
                }

                m_bIsSaveOKImage = radioButtonSaveImage.Checked;

                #endregion

                #region 系统模式参数保存

                if (radioButtonOneCam.Checked == true)
                {
                    m_iSysMode = 1;
                }
                else if (radioButtonTwoCam.Checked == true)
                {
                    m_iSysMode = 2;
                }
                else if (radioButtonThreeCam.Checked == true)
                {
                    m_iSysMode = 3;
                }
                else if (radioButtonFourCam.Checked == true)
                {
                    m_iSysMode = 4;
                }

                #endregion

                #region 文件设置参数保存

                m_sFileSavePath = textBoxFilePath.Text;

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnSaveParam_Click:" + ex.Message);
            }
        }

        private void CSetSys_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否要保存参数?","提示",MessageBoxButtons.OKCancel) == DialogResult.OK)  
                {
                    btnSaveParam.PerformClick();
                    this.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("CSetSys_FormClosing:" + ex.Message);
            }
        }


    }
}
