using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace MainFrame
{
    public partial class CCamParam : Form
    {

        #region 参数定义

        public string m_sCam1Param = string.Empty;
        private string sCam1Param = string.Empty;
        private List<string> m_sListCam1 = new List<string>();

        #endregion
        
        public CCamParam()
        {
            InitializeComponent();

            #region 初始化相机参数队列变量

            m_sListCam1 = new List<string>();

            #endregion

        }

        private void CCamParam_Load(object sender, EventArgs e)
        {
            try
            {
                #region 解析相机参数到队列变量中

                #region 相机1参数解析

                SetCamParam(m_sCam1Param, out m_sListCam1);
                sCam1Param = m_sCam1Param;
                UpdateListView(m_sListCam1, listViewCam1Param);

                #endregion

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("CCamParam_Load:" + ex.Message);
            }
        }

        private void btnExplain_Click(object sender, EventArgs e)
        {
            try
            {
                string strExplainFile = System.Environment.CurrentDirectory+"\\Explain\\说明.txt";
                using(StreamWriter sw = new StreamWriter(strExplainFile,true,Encoding.Default))
	            {		 
	            }
                System.Diagnostics.Process.Start("notepad.exe", strExplainFile);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnExplain_Click:" + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                m_sCam1Param = sCam1Param;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnSave_Click:" + ex.Message);
            }
        }

        private void btnCam1Add_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBoxCam1Name.Text == string.Empty) || (textBoxCam1Value.Text == string.Empty))
                {
                    MessageBox.Show("请先填写参数名称或参数名称!");
                    return;
                }

                AddCamFirstSecondParam(m_sListCam1, textBoxCam1Name.Text, textBoxCam1Value.Text);
                GetCamParam(m_sListCam1, out sCam1Param);
                UpdateListView(m_sListCam1, listViewCam1Param);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnCam1Add_Click:" + ex.Message);
            }
        }

        private void btnCam1Modification_Click(object sender, EventArgs e)
        {
            try
            {
                int index = int.Parse(textBoxCam1Index.Text);               

                SetCamFirstSecondParam(m_sListCam1, index, textBoxCam1Name.Text, textBoxCam1Value.Text);
                GetCamParam(m_sListCam1, out sCam1Param);
                UpdateListView(m_sListCam1, listViewCam1Param);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnCam1Modification_Click:" + ex.Message);
            }
        }

        private void btnCam1Remove_Click(object sender, EventArgs e)
        {
            try
            {
                int index = int.Parse(textBoxCam1Index.Text);

                RemoveCamFirstSecondParam(m_sListCam1, index);
                GetCamParam(m_sListCam1, out sCam1Param);
                UpdateListView(m_sListCam1, listViewCam1Param);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnCam1Remove_Click:" + ex.Message);
            }
        }

        private void btnCam1Clear_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否确定清空参数?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    ClearCamFirstSecondParam(m_sListCam1);
                    GetCamParam(m_sListCam1,out sCam1Param);
                    UpdateListView(m_sListCam1, listViewCam1Param);
                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnCam1Clear_Click:" + ex.Message);
            }
        }

        private void listViewCam1Param_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listViewCam1Param.SelectedIndices.Count != 0)
                {
                    ListViewItem value = listViewCam1Param.FocusedItem;
                    textBoxCam1Name.Text = value.SubItems[1].Text;
                    textBoxCam1Value.Text = value.SubItems[2].Text;
                    textBoxCam1Index.Text = listViewCam1Param.FocusedItem.Index.ToString();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("listViewCam1Param_SelectedIndexChanged:" + ex.Message);
            }
        }

        #region 更新界面相关函数

        private void UpdateListView(List<string> sListCam, ListView listViewCamParam)
        {
            try
            {
                listViewCamParam.Items.Clear();
                int count = sListCam.Count;
                string firstParam, secondParam;
                for (int i = 0; i < count;i++ )
                {
                    GetCamFirstSecondParam(sListCam, i,out firstParam,out secondParam);
                    ListViewItem value = new ListViewItem();
                    value.Text = (i + 1).ToString();
                    value.SubItems.Add(firstParam);
                    value.SubItems.Add(secondParam);
                    listViewCamParam.Items.Add(value);
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdateListView:" + ex.Message);
            }
        }

        #endregion

        #region 参数处理相关函数

        /// <summary>
        /// 将相机string参数解析到与之对应的double[] 数组中
        /// </summary>
        /// <param name="input"></param>
        /// <param name="doubleArray"></param>
        /// <returns></returns>
        public bool SetCamDoubleParam(string input,out double[] doubleArray)
        {
            doubleArray = new double[100];
            try
            {
                string pattern = @"[/(][^/(]*,[\+\-]?\d+(\.[\d]+)?[\)]";
                MatchCollection matchCollection = Regex.Matches(input, pattern);
                int count = matchCollection.Count;
                List<string> sListCam = new List<string>();
                sListCam.Clear();
                for (int i = 0; i < count; i++)
                {
                    sListCam.Add(matchCollection[i].Value);
                }

                string firstParam, secondParam;
                for (int i = 0; i < count;i++ )
                {
                    GetCamFirstSecondParam(sListCam, i, out firstParam, out secondParam);
                    doubleArray[i] = double.Parse(secondParam);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SetCamDoubleParam:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 将相机string参数解析到与之对应的队列当中
        /// </summary>
        /// <param name="input"></param>
        /// <param name="sListCam"></param>
        /// <returns></returns>
        private bool SetCamParam(string input, out List<string> sListCam)
        {
            sListCam = new List<string>();
            try
            {
                string pattern = @"[/(][^/(]*,[\+\-]?\d+(\.[\d]+)?[\)]";

                MatchCollection matchCollection = Regex.Matches(input, pattern);
                int count = matchCollection.Count;

                sListCam.Clear();
                for (int i = 0; i < count; i++)
                {
                    sListCam.Add(matchCollection[i].Value);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SetCamParam:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 将相机队列参数解析到与之对应的相机string中
        /// </summary>
        /// <param name="sListCam"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        private bool GetCamParam(List<string> sListCam,out string output)
        {
            output = string.Empty;
            try
            {
                int count = sListCam.Count;
                output = output + "\r\n";
                for (int i = 0; i < count; i++)
                {
                    output = output + sListCam[i] + "\r\n";
                }

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("GetCamParam:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取相机参数中索引为index的名称(第一个)和值(第二个)
        /// </summary>
        /// <param name="sListCam"></param>
        /// <param name="index"></param>
        /// <param name="firstParam"></param>
        /// <param name="secondParam"></param>
        /// <returns></returns>
        private bool GetCamFirstSecondParam(List<string> sListCam,int index,out string firstParam,out string secondParam)
        {
            firstParam = string.Empty;
            secondParam = string.Empty;
            try
            {
                string item = sListCam[index];
                string[] strSplit = item.Split(',');
                string strFirst = strSplit[0];
                string strSecond = strSplit[1];
                firstParam = strFirst.Remove(0, 1);
                secondParam = strSecond.Remove(strSecond.Length - 1, 1);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("GetCamFirstParam:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 设置相机参数中索引为index的名称(第一个)和值(第二个)
        /// </summary>
        /// <param name="sListCam"></param>
        /// <param name="index"></param>
        /// <param name="firstParam"></param>
        /// <param name="secondParam"></param>
        /// <returns></returns>
        private bool SetCamFirstSecondParam(List<string> sListCam, int index, string firstParam, string secondParam)
        {
            try
            {
                int count = sListCam.Count;

                //判断数据项是否为数值类型
                double value = double.Parse(secondParam);

                if (index == -1)
                {
                    MessageBox.Show("请先选择要处理的参数行!");
                    return false;
                }

                if (count < index)
                {
                    MessageBox.Show("相机参数队列索引小于所要修改的索引!");
                    return false;
                }

                string item = "(" + firstParam + "," + secondParam + ")";
                sListCam[index] = item;
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SetCamFirstSecondParam:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 设置相机参数中索引为index的名称(第一个)和值(第二个)
        /// </summary>
        /// <param name="sListCam"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool RemoveCamFirstSecondParam(List<string> sListCam, int index)
        {
            try
            {
                int count = sListCam.Count;

                if (index == -1)
                {
                    MessageBox.Show("请先选择要处理的参数行!");
                    return false;
                }

                if (count < index)
                {
                    MessageBox.Show("相机参数队列索引小于所要修改的索引!");
                    return false;
                }

                sListCam.RemoveAt(index);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SetCamFirstSecondParam:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 添加相机参数中索引为index的名称(第一个)和值(第二个)
        /// </summary>
        /// <param name="sListCam"></param>
        /// <param name="index"></param>
        /// <param name="firstParam"></param>
        /// <param name="secondParam"></param>
        /// <returns></returns>
        private bool AddCamFirstSecondParam(List<string> sListCam, string firstParam, string secondParam)
        {
            try
            {
                int count = sListCam.Count;

                //判断数据项是否为数值类型
                double value = double.Parse(secondParam);

                string item = "(" + firstParam + "," + secondParam + ")";
                sListCam.Add(item);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SetCamFirstSecondParam:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 清空相机参数中索引为index的名称(第一个)和值(第二个)
        /// </summary>
        /// <param name="sListCam"></param>
        /// <param name="firstParam"></param>
        /// <param name="secondParam"></param>
        /// <returns></returns>
        private bool ClearCamFirstSecondParam(List<string> sListCam)
        {
            try
            {
                sListCam.Clear();
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SetCamFirstSecondParam:" + ex.Message);
                return false;
            }
        }

        #endregion

    }
}
