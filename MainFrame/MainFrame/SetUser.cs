using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainFrame
{
    public partial class SetUser : Form
    {
        public string m_sAdminPassWord;
        public string m_sEngineerPassWord;
        public int m_iUser;


        public SetUser()
        {
            InitializeComponent();

            m_sAdminPassWord = string.Empty;
            m_sEngineerPassWord = string.Empty;
            m_iUser = 0;

        }

        private void SetUser_Load(object sender, EventArgs e)
        {
            comboUser.SelectedIndex = 0;
            txtPassword.Enabled = false;
            this.KeyPreview = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //判断目前用户为哪种模式
                switch (comboUser.SelectedIndex)
                {
                    case 0:
                        //MessageBox.Show("登录成功!");
                        m_iUser = 0;
                        this.Close();
                        break;
                    case 1:
                        if (txtPassword.Text == m_sAdminPassWord)
                        {
                            //MessageBox.Show("登录成功!");
                            m_iUser = 1;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("密码错误,请从新输入!");
                            txtPassword.Text = string.Empty;
                        }
                        break;
                    case 2:
                        if (txtPassword.Text == m_sEngineerPassWord)
                        {
                            //MessageBox.Show("登录成功!");
                            m_iUser = 2;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("密码错误,请从新输入!");
                            txtPassword.Text = string.Empty;
                        }
                        break;
                }
            }

            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboUser.SelectedIndex;
            if (index != 0)
            {
                txtPassword.Enabled = true;
            }
            else
            {
                txtPassword.Enabled = false;
            }
        }

        private void SetUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }


    }
}
