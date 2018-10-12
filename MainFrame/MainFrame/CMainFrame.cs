using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;


namespace MainFrame
{
    public partial class CMainFrame : Form
    {
        
        #region 非参数变量

        #region 相机句柄变量

        public HTuple m_hv_AcqHandle1 = new HTuple();
        public HTuple m_hv_AcqHandle2 = new HTuple();
        public HTuple m_hv_AcqHandle3 = new HTuple();
        public HTuple m_hv_AcqHandle4 = new HTuple();

        #endregion

        #region 相机状态变量

        private bool m_bIsConnectCam1;
        private bool m_bIsConnectCam2;
        private bool m_bIsConnectCam3;
        private bool m_bIsConnectCam4;


        #endregion

        #region 窗体ID变量

        private HTuple m_hv_hWindowControlID1 = new HTuple();
        private HTuple m_hv_hWindowControlID2 = new HTuple();
        private HTuple m_hv_hWindowControlID3 = new HTuple();
        private HTuple m_hv_hWindowControlID4 = new HTuple();

        #endregion

        #region 相机图像变量

        private HObject m_ho_Image1;
        private HObject m_ho_Image2;
        private HObject m_ho_Image3;
        private HObject m_ho_Image4;

        #endregion

        #region 相机结果队列

        private List<string> m_ListCam1 = new List<string>();
        private List<string> m_ListCam2 = new List<string>();
        private List<string> m_ListCam3 = new List<string>();
        private List<string> m_ListCam4 = new List<string>();


        #endregion

        #region 模拟程序运行状态变量

        private bool m_bIsRun = false;
        private bool m_bIsRunThread = false;
        private bool m_bCam1Run = false;
        private bool m_bCam2Run = false;
        private bool m_bCam3Run = false;
        private bool m_bCam4Run = false;
        private bool m_bOnLineRunMode = false;


        #endregion

        #region 程序配置文件路径

        private string m_strConfigDiretory = string.Empty;      //配置文件保存文件夹
        private string m_strConfig = string.Empty;              //配置文件名称

        #endregion

        #region 软件密码

        public string m_sAdminPassWord;
        public string m_sEngineerPassWord;

        #endregion

        #region 作业量参数

        private int m_TotalNum = 0;
        private int m_OKNum = 0;

        private int m_Cam1TotalNum = 0;
        private int m_Cam1OKNum = 0;

        private int m_Cam2TotalNum = 0;
        private int m_Cam2OKNum = 0;

        private int m_Cam3TotalNum = 0;
        private int m_Cam3OKNum = 0;

        private int m_Cam4TotalNum = 0;
        private int m_Cam4OKNum = 0;


        #endregion

        #region Enable控件集合定义

        private string[] m_strsEnableControl;

        #endregion

        #region 定时器变量

        private System.Threading.Timer m_Timer = null;
        private System.Threading.TimerCallback m_callback = null;
        private DelegateStr m_DelegateTimer = null; 

        #endregion

        private HTuple m_hv_Row1 = new HTuple(), m_hv_Column1 = new HTuple(), m_hv_Row2 = new HTuple(), m_hv_Column2 = new HTuple();
        private double m_dRatio = 1.0;
        private double m_dPreviousX = 0.0, m_dPreviousY = 0.0;
        private bool m_bPressDownLeft = false;
        private bool m_bInteract = false;
        private int m_iOriFormWidth = 0;
        private int m_iOriFormHeight = 0;
        private string[] m_strEnableControl;
              
	    #endregion

        #region 相机模板、测量句柄

        #region 相机1

        private HTuple m_hvCam1Shm = new HTuple();
        private HTuple m_hvCam1Ncm = new HTuple();
        private HTuple m_hvCam1Metrology = new HTuple();

        #endregion

        #region 相机2

        private HTuple m_hvCam2Shm = new HTuple();
        private HTuple m_hvCam2Ncm = new HTuple();
        private HTuple m_hvCam2Metrology = new HTuple();

        #endregion

        #region 相机3

        private HTuple m_hvCam3Shm = new HTuple();
        private HTuple m_hvCam3Ncm = new HTuple();
        private HTuple m_hvCam3Metrology = new HTuple();

        #endregion

        #region 相机4

        private HTuple m_hvCam4Shm = new HTuple();
        private HTuple m_hvCam4Ncm = new HTuple();
        private HTuple m_hvCam4Metrology = new HTuple();

        #endregion

        #endregion

        #region 相机参数变量

        public double[] m_dCam1Param = new double[100];
        public string m_sCam1Param = string.Empty;

        #endregion

        #region 软件参数变量

        public string m_sImageSavePath = System.Environment.CurrentDirectory + "\\ImageData";
        public int m_iDeleteImageTime = 3;
        public bool m_bIsSaveOKImage = false;
        public int m_iSysMode = 1;
        public string m_sFileSavePath = System.Environment.CurrentDirectory + "\\ResultData";

        #endregion

        #region 定义接收各种参数类型的委托

        private delegate void DelegateStr(params object[] args);
        private delegate void DelegateString(String str);
        private delegate void DelegateBool(bool b);
        private delegate void DelegateInt(int i);

        #endregion

        #region 定义各种控件调用委托的函数

        private void UpdateTimer(params object[] args)
        {
            try
            {
                if (statusStrip.InvokeRequired == true)
                {
                    m_DelegateTimer = new DelegateStr(UpdateTimer);
                    args = new object[] { args };
                    statusStrip.Invoke(m_DelegateTimer, args);

                }
                else
                {
                    toolStripStatusLabel_Time.Text = args[0].ToString();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdateTimer:" + ex.Message);
            }
        }

        private void UpdatelistBox(string str)
        {
            try
            {
                if (listBoxOutPut.InvokeRequired  == true)
                {
                    DelegateString del = new DelegateString(UpdatelistBox);
                    listBoxOutPut.Invoke(del, str);
                } 
                else
                {
                    string strData =  DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss]") + str+"\r\n";
                    listBoxOutPut.Items.Add(strData);
                    listBoxOutPut.SelectedIndex = listBoxOutPut.Items.Count - 1;

                    #region 将输出的数据保存到Log中

                    string strDiretory = System.Environment.CurrentDirectory + "\\Log";
                    if(Directory.Exists(strDiretory) == false)
                    {
                        Directory.CreateDirectory(strDiretory);
                    }

                    string strPath = strDiretory+"\\"+DateTime.Now.ToString("yyyy-MM-dd")+".txt";
                    using (StreamWriter sw = new StreamWriter(strPath,true))
                    {
                        sw.Write(strData);
                    }

                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("listBoxDelegate:" + ex.Message);
            }
        }

        private void UpdatetoolStripStatusLabel_Cma1Statu(bool b)
        {
            try
            {
                if (statusStrip.InvokeRequired == true)
                {
                    DelegateBool del = new DelegateBool(UpdatetoolStripStatusLabel_Cma1Statu);
                    statusStrip.Invoke(del, b);
                } 
                else
                {
                    if (b == true)
                    {
                        toolStripStatusLabel_Cma1Statu.BackColor = Color.Green;
                        toolStripStatusLabel_Cma1Statu.Text = "相机1连接";
                    } 
                    else
                    {
                        toolStripStatusLabel_Cma1Statu.BackColor = Color.Red;
                        toolStripStatusLabel_Cma1Statu.Text = "相机1断开";
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdatetoolStripStatusLabel_Cma1Statu:" + ex.Message);
            }
        }

        private void UpdatetoolStripStatusLabel_Cma2Statu(bool b)
        {
            try
            {
                if (statusStrip.InvokeRequired == true)
                {
                    DelegateBool del = new DelegateBool(UpdatetoolStripStatusLabel_Cma2Statu);
                    statusStrip.Invoke(del, b);
                }
                else
                {
                    if (b == true)
                    {
                        toolStripStatusLabel_Cma2Statu.BackColor = Color.Green;
                        toolStripStatusLabel_Cma2Statu.Text = "相机2连接";
                    } 
                    else
                    {
                        toolStripStatusLabel_Cma2Statu.BackColor = Color.Red;
                        toolStripStatusLabel_Cma2Statu.Text = "相机2断开";
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdatetoolStripStatusLabel_Cma2Statu:" + ex.Message);
            }
        }

        private void UpdatetoolStripStatusLabel_Cma3Statu(bool b)
        {
            try
            {
                if (statusStrip.InvokeRequired == true)
                {
                    DelegateBool del = new DelegateBool(UpdatetoolStripStatusLabel_Cma3Statu);
                    statusStrip.Invoke(del, b);
                }
                else
                {
                    if (b == true)
                    {
                        toolStripStatusLabel_Cma3Statu.BackColor = Color.Green;
                        toolStripStatusLabel_Cma3Statu.Text = "相机3连接";
                    }
                    else
                    {
                        toolStripStatusLabel_Cma3Statu.BackColor = Color.Red;
                        toolStripStatusLabel_Cma3Statu.Text = "相机3断开";
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdatetoolStripStatusLabel_Cma3Statu:" + ex.Message);
            }
        }

        private void UpdatetoolStripStatusLabel_Cma4Statu(bool b)
        {
            try
            {
                if (statusStrip.InvokeRequired == true)
                {
                    DelegateBool del = new DelegateBool(UpdatetoolStripStatusLabel_Cma4Statu);
                    statusStrip.Invoke(del, b);
                }
                else
                {
                    if (b == true)
                    {
                        toolStripStatusLabel_Cma4Statu.BackColor = Color.Green;
                        toolStripStatusLabel_Cma4Statu.Text = "相机4连接";
                    }
                    else
                    {
                        toolStripStatusLabel_Cma4Statu.BackColor = Color.Red;
                        toolStripStatusLabel_Cma4Statu.Text = "相机4断开";
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdatetoolStripStatusLabel_Cma4Statu:" + ex.Message);
            }
        }

        private void UpdatetoolStripStatusLabel_RunStatu(bool b)
        {
            try
            {
                if (statusStrip.InvokeRequired == true)
                {
                    DelegateBool del = new DelegateBool(UpdatetoolStripStatusLabel_RunStatu);
                    statusStrip.Invoke(del, b);
                } 
                else
                {
                    if (b == true)
                    {
                        toolStripStatusLabel_RunStatu.BackColor = Color.Green;
                        toolStripStatusLabel_RunStatu.Text = "开始运行";
                    } 
                    else
                    {
                        toolStripStatusLabel_RunStatu.BackColor = Color.Red;
                        toolStripStatusLabel_RunStatu.Text = "停止运行";
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdatetoolStripStatusLabel_RunStatu:" + ex.Message);
            }
        }

        private void UpdatetoolStripStatusLabel_User(int i)
        {
            try
            {
                if (statusStrip.InvokeRequired == true)
                {
                    DelegateInt del = new DelegateInt(UpdatetoolStripStatusLabel_User);
                    statusStrip.Invoke(del, i);
                }
                else
                {
                    toolStripStatusLabel_User.BackColor = Color.Green;
                    switch (i)
                    {
                        case 0:
                            toolStripStatusLabel_User.Text = "作业员";
                            EnableControl(false, m_strsEnableControl);
                            break;
                        case 1:
                            toolStripStatusLabel_User.Text = "管理员";
                            EnableControl(true, m_strsEnableControl);
                    	    break;
                        case 2:
                            toolStripStatusLabel_User.Text = "工程师";
                            EnableControl(true, m_strsEnableControl);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdatetoolStripStatusLabel_User:" + ex.Message);
            }
        }

        #endregion

        public CMainFrame()
        {
            InitializeComponent();

            try
            {
                #region 标记窗体原有尺寸

                ListControl(this);
                m_iOriFormWidth = this.Width;
                m_iOriFormHeight = this.Height;

                #endregion

                #region 初始化Enable控件

                m_strEnableControl = new string[] { "btnGrabImage", "btnReadImage", "btnSaveImage"};

                #endregion

                #region 初始化定时器

                m_callback = new System.Threading.TimerCallback(Timer_Target);
                m_Timer = new System.Threading.Timer(m_callback,null,Timeout.Infinite,1000);
                m_Timer.Change(0, 1000);

                #endregion

                #region 初始化配置参数文件夹路径

                m_strConfigDiretory = System.Environment.CurrentDirectory + "\\Config";

                #endregion

                #region 初始化相机图像变量

                HOperatorSet.GenEmptyObj(out m_ho_Image1);
                HOperatorSet.GenEmptyObj(out m_ho_Image2);
                HOperatorSet.GenEmptyObj(out m_ho_Image3);
                HOperatorSet.GenEmptyObj(out m_ho_Image4);

                #endregion

                #region 初始化窗体ID变量

                m_hv_hWindowControlID1 = hWindowControl1.HalconID;
                m_hv_hWindowControlID2 = hWindowControl2.HalconID;
                m_hv_hWindowControlID3 = hWindowControl3.HalconID;
                m_hv_hWindowControlID4 = hWindowControl4.HalconID;

                #endregion

                #region 初始化相机结果队列

                m_ListCam1.Clear();
                m_ListCam2.Clear();
                m_ListCam3.Clear();
                m_ListCam4.Clear();


                #endregion

                #region 初始化系统设置

                HOperatorSet.SetSystem("clip_region", "false");
                HOperatorSet.SetColor(m_hv_hWindowControlID1, "red");
                HOperatorSet.SetColor(m_hv_hWindowControlID2, "red");
                HOperatorSet.SetColor(m_hv_hWindowControlID3, "red");
                HOperatorSet.SetColor(m_hv_hWindowControlID4, "red");

                HOperatorSet.SetDraw(m_hv_hWindowControlID1, "margin");
                HOperatorSet.SetDraw(m_hv_hWindowControlID2, "margin");
                HOperatorSet.SetDraw(m_hv_hWindowControlID3, "margin");
                HOperatorSet.SetDraw(m_hv_hWindowControlID4, "margin");

                #endregion

                #region 创建配置参数文件夹

                if (Directory.Exists(m_strConfigDiretory) == false)
                {
                    Directory.CreateDirectory(m_strConfigDiretory);
                }

                #endregion

                #region 创建图像保存文件夹

                if (Directory.Exists(System.Environment.CurrentDirectory+"\\ImageData") == false)
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\ImageData");
                }

                #endregion

                #region 创建结果数据保存文件夹

                if (Directory.Exists(System.Environment.CurrentDirectory + "\\ResultData") == false)
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\ResultData");
                }

                #endregion

                #region 创建Log保存文件夹

                if (Directory.Exists(System.Environment.CurrentDirectory + "\\Log") == false)
                {
                    Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Log");
                }

                #endregion

                #region 创建说明文件夹及文件

                string strExplainPath = System.Environment.CurrentDirectory + "\\Explain\\";
                if (Directory.Exists(strExplainPath) == false)
                {
                    Directory.CreateDirectory(strExplainPath);
                }

                string strExplainFile = System.Environment.CurrentDirectory + "\\Explain\\说明.txt";
                using (StreamWriter sw = new StreamWriter(strExplainFile, true, Encoding.Default))
                {
                }

                #endregion

                #region 读取start.ini参数

                string filePath = System.Environment.CurrentDirectory + "\\Config\\Start.ini";
                string retVal = string.Empty;

                #region 读取软件密码

                GetPrivateProfileString("PassWord", "m_sAdminPassWord", "123456", out retVal, filePath);
                m_sAdminPassWord = retVal;
                WritePrivateProfileString("PassWord", "m_sAdminPassWord", m_sAdminPassWord, filePath);

                GetPrivateProfileString("PassWord", "m_sEngineerPassWord", "123456", out retVal, filePath);
                m_sEngineerPassWord = retVal;
                WritePrivateProfileString("PassWord", "m_sEngineerPassWord", m_sEngineerPassWord, filePath);

                #endregion

                #region 读取作业量

                GetWritePrivateIntParam("Num", "m_TotalNum", "0", filePath, m_TotalNum);
                GetWritePrivateIntParam("Num", "m_OKNum", "0", filePath, m_OKNum);

                GetWritePrivateIntParam("Num", "m_Cam1TotalNum", "0", filePath, m_Cam1TotalNum);
                GetWritePrivateIntParam("Num", "m_Cam1OKNum", "0", filePath, m_Cam1OKNum);

                GetWritePrivateIntParam("Num", "m_Cam2TotalNum", "0", filePath, m_Cam2TotalNum);
                GetWritePrivateIntParam("Num", "m_Cam2OKNum", "0", filePath, m_Cam2OKNum);

                GetWritePrivateIntParam("Num", "m_Cam3TotalNum", "0", filePath, m_Cam3TotalNum);
                GetWritePrivateIntParam("Num", "m_Cam3OKNum", "0", filePath, m_Cam3OKNum);

                GetWritePrivateIntParam("Num", "m_Cam4TotalNum", "0", filePath, m_Cam4TotalNum);
                GetWritePrivateIntParam("Num", "m_Cam4OKNum", "0", filePath, m_Cam4OKNum);

                #endregion

                #endregion

                #region Enable控件集合初始化

                m_strsEnableControl = new string[] { "btnResertNum" };

                #endregion

                #region 反序列化软件参数

                DeserializeSysParam();
                
                #endregion

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void CMainFrame_Load(object sender, EventArgs e)
        {
            try
            {

                #region 连接相机并跟新状态

                #region 相机1连接

                try
                {
                    HOperatorSet.OpenFramegrabber("File", 1, 1, 0, 0, 0, 0, "default", -1, "dafault", -1, "default", @"E:\Basic Template\HalconCode\MainFrame-Demo\Cam1", "default", -1, -1, out m_hv_AcqHandle1);
                    m_bIsConnectCam1 = true;
                    UpdatetoolStripStatusLabel_Cma1Statu(m_bIsConnectCam1);
                }
                catch
                {
                    m_bIsConnectCam1 = false;
                    UpdatetoolStripStatusLabel_Cma1Statu(m_bIsConnectCam1);
                }

                #endregion

                #region 相机2连接

                try
                {
                    HOperatorSet.OpenFramegrabber("File", 1, 1, 0, 0, 0, 0, "default", -1, "dafault", -1, "default", @"E:\Basic Template\HalconCode\MainFrame-Demo\Cam1", "default", -1, -1, out m_hv_AcqHandle2);
                    m_bIsConnectCam2 = true;
                    UpdatetoolStripStatusLabel_Cma2Statu(m_bIsConnectCam2);
                }
                catch
                {
                    m_bIsConnectCam2 = false;
                    UpdatetoolStripStatusLabel_Cma2Statu(m_bIsConnectCam2);
                }

                #endregion

                #region 相机3连接

                try
                {
                    HOperatorSet.OpenFramegrabber("File", 1, 1, 0, 0, 0, 0, "default", -1, "dafault", -1, "default", "default", "default", -1, -1, out m_hv_AcqHandle3);
                    m_bIsConnectCam3 = true;
                    UpdatetoolStripStatusLabel_Cma3Statu(m_bIsConnectCam3);
                }
                catch
                {
                    m_bIsConnectCam3 = false;
                    UpdatetoolStripStatusLabel_Cma3Statu(m_bIsConnectCam3);
                }

                #endregion

                #region 相机4连接

                try
                {
                    HOperatorSet.OpenFramegrabber("File", 1, 1, 0, 0, 0, 0, "default", -1, "dafault", -1, "default", "default", "default", -1, -1, out m_hv_AcqHandle4);
                    m_bIsConnectCam4 = true;
                    UpdatetoolStripStatusLabel_Cma4Statu(m_bIsConnectCam4);
                }
                catch
                {
                    m_bIsConnectCam4 = false;
                    UpdatetoolStripStatusLabel_Cma4Statu(m_bIsConnectCam4);
                }

                #endregion

                #endregion
                
                #region 跟新程序运行状态

                UpdatetoolStripStatusLabel_RunStatu(m_bIsRun);

                #endregion

                #region 加载机种信息

                listView.Items.Clear();

                List<string> listFilesName = new List<string>();
                FindDiretory(m_strConfigDiretory, out listFilesName);
                //FindFiles(m_strConfigDiretory, "xml", out listFilesName);

                for (int i = 0; i < listFilesName.Count; i++)
                {
                    ListViewItem value = new ListViewItem();
                    value.Text = (i + 1).ToString();
                    value.SubItems.Add(listFilesName[i]);
                    listView.Items.Add(value);
                }

                #endregion                

                #region tabControl页面初始化

                tabControl.SelectedIndex = 3;

                #endregion


            }
            catch (System.Exception ex)
            {
                MessageBox.Show("CMainFrame_Load:" + ex.Message);
            }
        }

        private void LoadConfig_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_bIsRun == true)
                {
                    MessageBox.Show("请先停止运行!");
                    return;
                }

                if (m_strConfig == string.Empty)
                {
                    MessageBox.Show("请选择机种!");
                    return;
                }

                //更新当前机种
                textBoxConfigName.Text = m_strConfig;

                #region 反序列化相机参数

                using (StreamReader sr = new StreamReader(System.Environment.CurrentDirectory + "\\Config\\"+m_strConfig+"\\Param.xml", Encoding.Default))
                {
                    CSerialize cSerialize = new CSerialize();
                    XmlSerializer xmlSerializer = new XmlSerializer(cSerialize.GetType());
                    cSerialize = (CSerialize)xmlSerializer.Deserialize(sr);
                    m_sCam1Param = cSerialize.m_sCam1Param;

                    CCamParam dlg = new CCamParam();
                    dlg.SetCamDoubleParam(m_sCam1Param, out m_dCam1Param);
                }


                #endregion

                #region 读取模板、测量句柄

                string strModelPath = string.Empty;

                #region 相机1读取

                strModelPath = System.Environment.CurrentDirectory + "\\Config\\"+m_strConfig+"\\Cam1";
                if (Directory.Exists(strModelPath) == false)
                {
                    Directory.CreateDirectory(strModelPath);
                }

                HOperatorSet.ReadShapeModel(strModelPath + "\\ModelId_1.shm", out m_hvCam1Shm);
                HOperatorSet.ReadMetrologyModel(strModelPath + "\\MetrologyHandle_1.mtr", out m_hvCam1Metrology);

                #endregion

                #region 相机2读取

                strModelPath = System.Environment.CurrentDirectory + "\\Config\\" + m_strConfig + "\\Cam2";
                if (Directory.Exists(strModelPath) == false)
                {
                    Directory.CreateDirectory(strModelPath);
                }

                HOperatorSet.ReadShapeModel(strModelPath + "\\ModelId_1.shm", out m_hvCam2Shm);
                HOperatorSet.ReadMetrologyModel(strModelPath + "\\MetrologyHandle_1.mtr", out m_hvCam2Metrology);

                #endregion

                #region 相机3读取

                strModelPath = System.Environment.CurrentDirectory + "\\Config\\" + m_strConfig + "\\Cam3";
                if (Directory.Exists(strModelPath) == false)
                {
                    Directory.CreateDirectory(strModelPath);
                }

                HOperatorSet.ReadShapeModel(strModelPath + "\\ModelId_1.shm", out m_hvCam3Shm);
                HOperatorSet.ReadMetrologyModel(strModelPath + "\\MetrologyHandle_1.mtr", out m_hvCam3Metrology);

                #endregion

                #region 相机3读取

                strModelPath = System.Environment.CurrentDirectory + "\\Config\\" + m_strConfig + "\\Cam4";
                if (Directory.Exists(strModelPath) == false)
                {
                    Directory.CreateDirectory(strModelPath);
                }

                HOperatorSet.ReadShapeModel(strModelPath + "\\ModelId_1.shm", out m_hvCam4Shm);
                HOperatorSet.ReadMetrologyModel(strModelPath + "\\MetrologyHandle_1.mtr", out m_hvCam4Metrology);

                #endregion

                #endregion

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("LoadConfig_ToolStripMenuItem_Click:" + ex.Message);
            }
        }

        private void DeleteConfig_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_bIsRun == true)
                {
                    MessageBox.Show("请先停止运行!");
                    return;
                }

                if(MessageBox.Show("是否要删除当前机种?","提示",MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (textBoxConfigName.Text == string.Empty)
                    {
                        MessageBox.Show("当前机种为空!");
                        return;
                    }

                    string path = System.Environment.CurrentDirectory + "\\Config\\" + textBoxConfigName.Text;
                    Directory.Delete(path, true);
                    textBoxConfigName.Text = string.Empty;

                    #region 加载机种信息

                    listView.Items.Clear();

                    List<string> listFilesName = new List<string>();
                    FindDiretory(m_strConfigDiretory, out listFilesName);

                    for (int i = 0; i < listFilesName.Count; i++)
                    {
                        ListViewItem value = new ListViewItem();
                        value.Text = (i + 1).ToString();
                        value.SubItems.Add(listFilesName[i]);
                        listView.Items.Add(value);
                    }

                    #endregion                

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("DeleteConfig_ToolStripMenuItem_Click:" + ex.Message);
            }
        }

        private void GetHWindowPart(HObject Image,int Height,int Width,
                                    out HTuple Row1,out HTuple Column1,out HTuple Row2,out HTuple Column2,out double Ratio)
        {
            Row1 = new HTuple();
            Column1 = new HTuple();
            Row2 = new HTuple();
            Column2 = new HTuple();
            Ratio = 1.0;
            try
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(Image, out width, out  height);
                double dRatioWidth = width[0].D / Width;
                double dRatioHeight = height[0].D / Height;
                if (dRatioWidth >= dRatioHeight)
                {
                    Row1 = -((dRatioWidth * Height) - height) / 2;
                    Column1 = 0;
                    Row2 = Row1 + dRatioWidth * Height;
                    Column2 = Column1 + dRatioWidth * Width;
                    Ratio = dRatioWidth;
                }
                else
                {                    
                    Row1 = 0;
                    Column1 = -((dRatioHeight * Width) - width) / 2;
                    Row2 = Row1 + dRatioHeight * Height;
                    Column2 = Column1 + dRatioHeight * Width;
                    Ratio = dRatioHeight;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("GetHWindowPart:" + ex.Message);
            }
            
        }

        private void ListControl(Control control,int Index=0,double WRatio=1.0,double HRatio=1.0)
        {
            try
            {
                int Count = control.Controls.Count;
                
                if (Count < 0)
                {
                    return;                    
                }
                
                for (int i = 0; i < Count; i++)
                {
                    switch (Index)
                    {
                        case 0:
                            
                            int width = control.Controls[i].Width;
                            int height = control.Controls[i].Height;
                            int left = control.Controls[i].Left;
                            int top = control.Controls[i].Top;
                            float size = control.Controls[i].Font.Size;
                            control.Controls[i].Tag = width.ToString() + "," + height.ToString() + "," +
                                                      left.ToString() + "," + top.ToString()+","+size.ToString();
                            ListControl(control.Controls[i],Index);
                            
                            break;   
                        case 1:

                            MessageBox.Show(control.Controls[i].ToString() + ":" + control.Controls[i].Tag.ToString());
                            ListControl(control.Controls[i],Index);
                            
                            break;

                        case 2:
                            //提取控件原始 width height
                            string tag = control.Controls[i].Tag.ToString();
                            string[] strs = tag.Split(',');
                            int iWidth = int.Parse(strs[0]);
                            int iHeight = int.Parse(strs[1]);
                            int iLeft = int.Parse(strs[2]);
                            int iTop = int.Parse(strs[3]);
                            float fSize = float.Parse(strs[4]);
                            control.Controls[i].Width = (int)(iWidth * WRatio);
                            control.Controls[i].Height = (int)(iHeight * HRatio);
                            control.Controls[i].Left = (int)(iLeft * WRatio);
                            control.Controls[i].Top = (int)(iTop * HRatio);
                            control.Controls[i].Font = new System.Drawing.Font("宋体", (float)(fSize * (WRatio + HRatio) / 2));
                            ListControl(control.Controls[i], Index, WRatio, HRatio);

                            break;

                        default:
                            break;
                    }
                    

                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("ListControl" + ex.Message);
            }
        }

        private void FlushWindow(HTuple hWindowControlID,int Index = 0)
        {
            try
            {
                switch (Index)
                {
                    case 0:

                        #region 显示原图

                        if (m_ho_Image1.CountObj() != 0)
                        {
                            HOperatorSet.ClearWindow(hWindowControlID);
                            HOperatorSet.DispObj(m_ho_Image1, hWindowControlID);
                        }

                        #endregion

                        break;
                    case 1:
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("FlushWindow:" + ex.Message);
            }
            
        }

        private void hWindowControl1_HMouseDown(object sender, HMouseEventArgs e)
        {
            try
            {
                if (m_ho_Image1.IsInitialized() == false)
                {
                    return;
                }

                if ( (m_ho_Image1.CountObj() == 0) || (m_bInteract == true) )
                {
                    return;
                }

                if (e.Button == MouseButtons.Left)
                {
                    m_dPreviousX = e.X;
                    m_dPreviousY = e.Y;
                    m_bPressDownLeft = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    GetHWindowPart(m_ho_Image1, hWindowControl1.Height, hWindowControl1.Width, out m_hv_Row1, out m_hv_Column1, out m_hv_Row2, out m_hv_Column2, out m_dRatio);
                    HOperatorSet.SetPart(m_hv_hWindowControlID1, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);
                    FlushWindow(m_hv_hWindowControlID1);
                }

            }
            catch (System.Exception ex)
            {

                MessageBox.Show("hWindowControl_HMouseDown"+ex.Message);
            }
        }

        private void hWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {
            try
            {
                if ((m_ho_Image1.IsInitialized() == false))
                {
                    return;
                }
                if ( m_ho_Image1.CountObj() == 0)
                {
                    return;
                }

                #region 更新图像坐标及灰度
                
                toolStripStatusLabel_X.Text = "X:" + ((int)e.X).ToString("D4");
                toolStripStatusLabel_Y.Text = "Y:" + ((int)e.Y).ToString("D4");
                HTuple grayval = new HTuple();
                try
                {
                    HOperatorSet.GetGrayval(m_ho_Image1, e.Y, e.X, out grayval);
                }
                catch
                {
                    grayval = 0;
                }
                toolStripStatusLabel_Grayvalue.Text = "G:" + grayval[0].I.ToString("D4");
                
                #endregion

                if (m_bInteract == true)
                {
                    return;
                }

                #region 移动更新图像
                if (m_bPressDownLeft == true)
                {
                    double DeltX = e.X - m_dPreviousX;
                    double DeltY = e.Y - m_dPreviousY;
                    m_hv_Row1 = m_hv_Row1 - DeltY;
                    m_hv_Column1 = m_hv_Column1 - DeltX;
                    m_hv_Row2 = m_hv_Row2 - DeltY;
                    m_hv_Column2 = m_hv_Column2 - DeltX;
                    HOperatorSet.SetPart(m_hv_hWindowControlID1, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);
                    FlushWindow(m_hv_hWindowControlID1);

                }
#endregion


                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("hWindowControl_HMouseMove"+ex.Message);
            }
        }

        private void hWindowControl1_HMouseUp(object sender, HMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    m_bPressDownLeft = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("hWindowControl_HMouseUp:"+ex.Message);
            }
        }

        private void hWindowControl1_HMouseWheel(object sender, HMouseEventArgs e)
        {
            try
            {
                if (m_ho_Image1.IsInitialized() == false)
                {
                    return;
                }

                if ( (m_ho_Image1.CountObj() == 0) || (m_bInteract == true))
                {
                    return;
                }

                //根据鼠标位置将图像分为左右  上下四部分
                HTuple LeftLength = new HTuple(), RightLength = new HTuple();
                HTuple UpLength = new HTuple(), DownLength = new HTuple();
                LeftLength = e.X - m_hv_Column1;
                RightLength = m_hv_Column2 - e.X;
                UpLength = e.Y - m_hv_Row1;
                DownLength = m_hv_Row2 - e.Y;

                if (e.Delta >= 0)
                {
                    #region 放大图像

                    m_hv_Row1 = m_hv_Row1 + (UpLength) / 10;
                    m_hv_Column1 = m_hv_Column1 + (LeftLength) / 10;
                    m_hv_Row2 = m_hv_Row2 - (DownLength) / 10;
                    m_hv_Column2 = m_hv_Column2 - (RightLength) / 10;

                    HOperatorSet.SetPart(m_hv_hWindowControlID1, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);
                    FlushWindow(m_hv_hWindowControlID1);

                    #endregion
                }
                else
                {
                    #region 缩小图像

                    m_hv_Row1 = m_hv_Row1 - (UpLength) / 10;
                    m_hv_Column1 = m_hv_Column1 - (LeftLength) / 10;
                    m_hv_Row2 = m_hv_Row2 + (DownLength) / 10;
                    m_hv_Column2 = m_hv_Column2 + (RightLength) / 10;

                    //判断是否小于原始尺寸
                    HTuple Row1 = new HTuple(), Column1 = new HTuple(), Row2 = new HTuple(), Column2 = new HTuple();
                    HTuple OriLengthRow = new HTuple(), LengthRow = new HTuple(), OriLengthColumn = new HTuple(), LengthColumn = new HTuple();
                    GetHWindowPart(m_ho_Image1, hWindowControl1.Height, hWindowControl1.Width, out Row1, out Column1, out Row2, out Column2, out m_dRatio);
                    OriLengthRow = Row2 - Row1;
                    OriLengthColumn = Column2 - Column1;
                    LengthRow = m_hv_Row2 - m_hv_Row1;
                    LengthColumn = m_hv_Column2 - m_hv_Column1;
                    if ((OriLengthRow[0].D <= LengthRow[0].D) || (OriLengthColumn[0].D <= LengthColumn[0].D))
                    {
                        m_hv_Row1 = Row1;
                        m_hv_Column1 = Column1;
                        m_hv_Row2 = Row2;
                        m_hv_Column2 = Column2;
                    }
                    
                    HOperatorSet.SetPart(m_hv_hWindowControlID1, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);
                    FlushWindow(m_hv_hWindowControlID1);

                    #endregion
                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("hWindowControl_HMouseWheel:" + ex.Message);
            }
        }

        private void CHalconTemplate_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                #region 根据窗体原有尺寸进行对应比例缩放

                int iFormWidth = this.Width;
                int iFormHeight = this.Height;
                double WRatio = 1.0 * iFormWidth / m_iOriFormWidth;
                double HRatio = 1.0 * iFormHeight / m_iOriFormHeight;

                ListControl(this, 2, WRatio, HRatio);

                Scale_ListView(listView);

                FlushWindow(m_hv_hWindowControlID1);

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("CHalconTemplate_SizeChanged:" + ex.Message);
            }
        }

        private void Timer_Target(object state)
        {
            try
            {
                UpdateTimer(new object[] {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")});                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Timer_Target:" + ex.Message);
            }
        }

        private void dmTcpClient_OnReceviceByte(byte[] date)
        {
            try
            {
                Thread.Sleep(10);
                string strData = Encoding.Default.GetString(date);
                UpdatelistBox("接收到数据:" + strData);

                if (strData == "1")
                {
                    m_bCam1Run = true;
                }
                else if(strData == "2")
                {
                    m_bCam2Run = true;
                }
                else if (strData == "3")
                {
                    m_bCam3Run = true;
                }
                else if (strData == "4")
                {
                    m_bCam4Run = true;
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("dmTcpClient_OnReceviceByte:" + ex.Message);
            }
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                Thread.Sleep(10);
                int count = serialPort.BytesToRead;
                byte[] buffer = new byte[count];
                serialPort.Read(buffer, 0, count);
                string strData = Encoding.Default.GetString(buffer);
                UpdatelistBox("接收到数据:" + strData);

                if (strData == "1")
                {
                    m_bCam1Run = true;
                }
                else if (strData == "2")
                {
                    m_bCam2Run = true;
                }
                else if (strData == "3")
                {
                    m_bCam3Run = true;
                }
                else if (strData == "4")
                {
                    m_bCam4Run = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("serialPort_DataReceived:" + ex.Message);
            }
        }

        private void Run()
        {
            try
            {
                while (m_bIsRunThread == true)
                {
                    Thread.Sleep(10);

                    #region 运行

                    if (m_bCam1Run == true)
                    {
                        m_bCam1Run = false;

                        #region 相机1运行过程

                        Thread threadCam1Run = new Thread(Cam1Run);
                        threadCam1Run.IsBackground = true;
                        threadCam1Run.Start();

                        #endregion

                    }
                    else if (m_bCam2Run == true)
                    {
                        m_bCam2Run = false;

                        #region 相机2运行过程

                        Thread threadCam2Run = new Thread(Cam2Run);
                        threadCam2Run.IsBackground = true;
                        threadCam2Run.Start();

                        #endregion

                    }
                    else if (m_bCam3Run == true)
                    {
                        m_bCam3Run = false;

                        #region 相机3运行过程

                        Thread threadCam3Run = new Thread(Cam3Run);
                        threadCam3Run.IsBackground = true;
                        threadCam3Run.Start();

                        #endregion

                        
                    }
                    else if (m_bCam4Run == true)
                    {
                        m_bCam4Run = false;

                        #region 相机4运行过程

                        Thread threadCam4Run = new Thread(Cam4Run);
                        threadCam4Run.IsBackground = true;
                        threadCam4Run.Start();

                        #endregion
                        
                    }

                    #endregion

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Run:" + ex.Message);
            }
        }

        private void DeleteImageRun()
        {
            try
            {
                Thread.Sleep(10);
                DeleteImage(m_sImageSavePath, m_iDeleteImageTime);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("DeleteImageRun:" + ex.Message);
            }
        }

        private void Cam1Run()
        {
            try
            {
                Thread.Sleep(10);

                #region 删除过期图像

                Thread thread = new Thread(DeleteImageRun);
                thread.IsBackground = true;
                thread.Start();

                #endregion

                lock (this)
                {
                    UpdatelistBox("接收到Cam1采图信号！");
                }

                if (m_bOnLineRunMode == true)
                {
                    #region OnLine运行
                    #endregion
                } 
                else
                {
                    #region OffLine运行

                    m_ho_Image1.Dispose();
                    HOperatorSet.GrabImage(out m_ho_Image1, m_hv_AcqHandle1);

                    lock (this)
                    {
                        UpdatelistBox("Cam1-采集完成！");
                    }

                    GetHWindowPart(m_ho_Image1, hWindowControl1.Height, hWindowControl1.Width, out m_hv_Row1, out m_hv_Column1, out m_hv_Row2, out m_hv_Column2, out m_dRatio);
                    HOperatorSet.SetPart(m_hv_hWindowControlID1, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);

                    Cam1Procedure(m_ho_Image1, m_hv_hWindowControlID1,m_hvCam1Shm,m_hvCam1Ncm,m_hvCam1Metrology);

                    #endregion                    
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Cam1运行过程出错:" + ex.Message);
            }
        }

        private void Cam2Run()
        {
            try
            {
                Thread.Sleep(10);

                lock (this)
                {
                    UpdatelistBox("接收到Cam2采图信号！");
                }

                if (m_bOnLineRunMode == true)
                {
                    #region OnLine运行
                    #endregion
                }
                else
                {
                    #region OffLine运行

                    m_ho_Image2.Dispose();
                    HOperatorSet.GrabImage(out m_ho_Image2, m_hv_AcqHandle2);

                    lock (this)
                    {
                        UpdatelistBox("Cam2-采集完成！");
                    }

                    GetHWindowPart(m_ho_Image2, hWindowControl2.Height, hWindowControl2.Width, out m_hv_Row1, out m_hv_Column1, out m_hv_Row2, out m_hv_Column2, out m_dRatio);
                    HOperatorSet.SetPart(m_hv_hWindowControlID2, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);

                    Cam2Procedure(m_ho_Image2, m_hv_hWindowControlID2, m_hvCam2Shm, m_hvCam2Ncm, m_hvCam2Metrology);

                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Cam2运行出错:" + ex.Message);
            }
        }

        private void Cam3Run()
        {
            try
            {
                Thread.Sleep(10);

                lock (this)
                {
                    UpdatelistBox("接收到Cam3采图信号！");
                }

                if (m_bOnLineRunMode == true)
                {
                    #region OnLine运行
                    #endregion
                }
                else
                {
                    #region OffLine运行

                    m_ho_Image3.Dispose();
                    HOperatorSet.GrabImage(out m_ho_Image3, m_hv_AcqHandle3);

                    lock (this)
                    {
                        UpdatelistBox("Cam3-采集完成！");
                    }


                    GetHWindowPart(m_ho_Image3, hWindowControl3.Height, hWindowControl3.Width, out m_hv_Row1, out m_hv_Column1, out m_hv_Row2, out m_hv_Column2, out m_dRatio);
                    HOperatorSet.SetPart(m_hv_hWindowControlID3, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);

                    Cam3Procedure(m_ho_Image3, m_hv_hWindowControlID3, m_hvCam3Shm, m_hvCam3Ncm, m_hvCam3Metrology);

                    #endregion
                }

                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Cam3运行出错:" + ex.Message);
            }
        }

        private void Cam4Run()
        {
            try
            {
                Thread.Sleep(10);

                lock (this)
                {
                    UpdatelistBox("接收到Cam4采图信号！");
                }

                if (m_bOnLineRunMode == true)
                {
                    #region OnLine运行
                    #endregion
                }
                else
                {
                    #region OffLine运行

                    m_ho_Image4.Dispose();
                    HOperatorSet.GrabImage(out m_ho_Image4, m_hv_AcqHandle4);

                    lock (this)
                    {
                        UpdatelistBox("Cam4-采集完成！");
                    }

                    GetHWindowPart(m_ho_Image4, hWindowControl4.Height, hWindowControl4.Width, out m_hv_Row1, out m_hv_Column1, out m_hv_Row2, out m_hv_Column2, out m_dRatio);
                    HOperatorSet.SetPart(m_hv_hWindowControlID4, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);

                    Cam4Procedure(m_ho_Image4, m_hv_hWindowControlID4, m_hvCam4Shm, m_hvCam4Ncm, m_hvCam4Metrology);

                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Cam4运行过程出错:" + ex.Message);
            }
        }

        private void StartRun_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxConfigName.Text == string.Empty)
                {
                    MessageBox.Show("请先加载机种信息!");
                    return;
                }

                if (m_bIsRun == false)
                {
                    dmTcpClient.StartConnection();
                    serialPort.Open();

                    StartRun_ToolStripMenuItem.Text="停止运行";
                    m_bIsRun = true;
                    UpdatetoolStripStatusLabel_RunStatu(m_bIsRun);

                    Thread threadRun = new Thread(Run);
                    threadRun.IsBackground = true;
                    m_bIsRunThread = true;
                    threadRun.Start();

                } 
                else
                {
                    dmTcpClient.StopConnection();
                    serialPort.Close();
                    
                    m_bIsRunThread = false;
                    Thread.Sleep(100);

                    StartRun_ToolStripMenuItem.Text="开始运行";
                    m_bIsRun = false;
                    UpdatetoolStripStatusLabel_RunStatu(m_bIsRun);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("StartRun_ToolStripMenuItem_Click:" + ex.Message);
            }
            
        }

        private void Scale_ListView(ListView listView)
        {
            try
            {
                if (listView != null)
                {
                    int width = listView.Width;
                    listView.Columns[0].Width = width * 1 / 4;
                    listView.Columns[1].Width = width - listView.Columns[0].Width;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Scale_ListView:" + ex.Message);
            }
        }

        private void FindFiles(string strDiretory, string strFormat, out List<string> listFilesName)
        {
            try
            {
                listFilesName = new List<string>();
                listFilesName.Clear();
                string[] strFiles = Directory.GetFiles(strDiretory);
                for (int i = 0; i < strFiles.Length; i++)
                {
                    int index = strFiles[i].LastIndexOf('.');
                    string format = strFiles[i].Substring(index + 1);
                    if (format == strFormat)
                    {
                        int startIndex = strFiles[i].LastIndexOf('\\');
                        listFilesName.Add(strFiles[i].Substring(startIndex + 1, index - startIndex-1));
                    }
                }
            }
            catch (System.Exception ex)
            {
                listFilesName = new List<string>();
                listFilesName.Clear();

                MessageBox.Show("FindFiles:" + ex.Message);
            }
            

        }

        private void FindDiretory(string strDiretory, out List<string> listFilesName)
        {
            listFilesName = new List<string>();
            listFilesName.Clear();
            try
            {
                string[] strDiretorys = Directory.GetDirectories(strDiretory, "*", SearchOption.TopDirectoryOnly);
                int count = strDiretorys.Length;
                for (int i = 0; i < count;i++ )
                {
                    string[] strNames = strDiretorys[i].Split(new string[] { "\\" }, StringSplitOptions.None);
                    listFilesName.Add(strNames[strNames.Length - 1]);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("FindDiretory:" + ex.Message);
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedIndices.Count != 0)
                {
                    ListViewItem value = listView.FocusedItem;
                    m_strConfig = value.SubItems[1].Text;
                }
                               
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("listView_SelectedIndexChanged:" + ex.Message);
            }
        }

        private void Initial_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                #region 创建配置参数文件夹

                if (Directory.Exists(m_strConfigDiretory) == false)
                {
                    Directory.CreateDirectory(m_strConfigDiretory);
                }

                #endregion

                #region 创建说明文件夹及文件

                string strExplainPath = System.Environment.CurrentDirectory + "\\Explain\\";
                if (Directory.Exists(strExplainPath) == false)
                {
                    Directory.CreateDirectory(strExplainPath);
                }

                string strExplainFile = System.Environment.CurrentDirectory + "\\Explain\\说明.txt";
                using (StreamWriter sw = new StreamWriter(strExplainFile, true, Encoding.Default))
                {
                }

                #endregion

                #region 读取模板、测量句柄

                string strModelPath = string.Empty;

                #region 相机1读取

                strModelPath = System.Environment.CurrentDirectory + "\\Config\\Cam1\\";
                if (Directory.Exists(strModelPath) == false)
                {
                    Directory.CreateDirectory(strModelPath);
                }

                #endregion



                #endregion

                #region 反序列化相机参数

                using (StreamReader sr = new StreamReader(System.Environment.CurrentDirectory + "\\Config\\Param.xml"))
                {
                    CSerialize cSerialize = new CSerialize();
                    XmlSerializer xmlSerializer = new XmlSerializer(cSerialize.GetType());
                    cSerialize = (CSerialize)xmlSerializer.Deserialize(sr);
                }


                #endregion

                #region 加载机种信息

                listView.Items.Clear();

                List<string> listFilesName = new List<string>();
                FindFiles(m_strConfigDiretory, "xml", out listFilesName);

                for (int i = 0; i < listFilesName.Count; i++)
                {
                    ListViewItem value = new ListViewItem();
                    value.Text = (i + 1).ToString();
                    value.SubItems.Add(listFilesName[i]);
                    listView.Items.Add(value);
                }

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Initial_ToolStripMenuItem_Click:" + ex.Message);
            }
        }

        private void Measure_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_bIsRun == true)
                {
                    MessageBox.Show("请先停止运行!");
                    return;
                }

                CMeasure measure = new CMeasure();
                measure.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Measure_ToolStripMenuItem_Click:" + ex.Message);
            }
        }

        private void Param_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                CCamParam dlg = new CCamParam();
                dlg.m_sCam1Param = m_sCam1Param; 
                dlg.ShowDialog();

                m_sCam1Param = dlg.m_sCam1Param;

                #region 序列化

                using (StreamWriter sw = new StreamWriter(System.Environment.CurrentDirectory+"\\Config\\Param.xml",false,Encoding.Default))
                {
                    CSerialize cSerialize = new CSerialize();
                    cSerialize.m_sCam1Param = m_sCam1Param;
                    XmlSerializer xmlSerializer = new XmlSerializer(cSerialize.GetType());
                    xmlSerializer.Serialize(sw, cSerialize);
                }

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Param_ToolStripMenuItem_Click:" + ex.Message);
            }
        }

        private void SetUser_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                SetUser dlg = new SetUser();
                dlg.m_sAdminPassWord = m_sAdminPassWord;
                dlg.m_sEngineerPassWord = m_sEngineerPassWord;
                dlg.ShowDialog();
                UpdatetoolStripStatusLabel_User(dlg.m_iUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetUser_ToolStripMenuItem_Click:" + ex.Message);
                return;
            }
        }

        private void SetSys_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CSetSys dlg = new CSetSys();
                dlg.m_sImageSavePath = m_sImageSavePath;
                dlg.m_iDeleteImageTime = m_iDeleteImageTime;
                dlg.m_bIsSaveOKImage = m_bIsSaveOKImage;
                dlg.m_iSysMode = m_iSysMode;
                dlg.m_sFileSavePath = m_sFileSavePath;
                dlg.ShowDialog();
                m_sImageSavePath = dlg.m_sImageSavePath;
                m_iDeleteImageTime = dlg.m_iDeleteImageTime;
                m_bIsSaveOKImage = dlg.m_bIsSaveOKImage;
                m_iSysMode = dlg.m_iSysMode;
                m_sFileSavePath = dlg.m_sFileSavePath;

                SerializeSysParam();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SetSys_ToolStripMenuItem_Click:" + ex.Message);
            }
        }

        private void btnResertNum_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("btnResertNum_Click:" + ex.Message);
            }
        }

        private void CMainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否退出程序!", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                #region 退出运行线程

                m_bIsRunThread = false;
                Thread.Sleep(100);

                #endregion

                #region 关闭网络通信
                if (m_bIsRun == true)
                {
                    dmTcpClient.StopConnection();
                    Thread.Sleep(100);
                }
                #endregion

                #region 关闭串口

                if (m_bIsRun == true)
                {
                    serialPort.Close();
                    Thread.Sleep(100);
                }

                #endregion

                #region 断开相机连接

                if (m_bIsConnectCam1 == true)
                {
                    HOperatorSet.CloseFramegrabber(m_hv_AcqHandle1);
                }

                if (m_bIsConnectCam2 == true)
                {
                    HOperatorSet.CloseFramegrabber(m_hv_AcqHandle2);
                }

                if (m_bIsConnectCam3 == true)
                {
                    HOperatorSet.CloseFramegrabber(m_hv_AcqHandle3);
                }

                if (m_bIsConnectCam4 == true)
                {
                    HOperatorSet.CloseFramegrabber(m_hv_AcqHandle4);
                }

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("CMainFrame_FormClosing:" + MessageBox.Show(ex.Message));
            }
        }

        #region 配置文件函数

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        public static extern long WritePrivateProfileString(string section, string key,
                                                             string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        public static extern int GetPrivateProfileString(string section, string key,
                                                           string def, byte[] retVal, int size, string filePath);

        public static void GetPrivateProfileString(string section, string key,
                                                           string def, out string retVal, string filePath)
        {
            Byte[] Buffer = new Byte[1024];
            int bufLen = GetPrivateProfileString(section, key, def, Buffer, Buffer.GetUpperBound(0), filePath);
            retVal = Encoding.GetEncoding(0).GetString(Buffer);
            retVal = retVal.Substring(0, bufLen);

        }

        #endregion

        #region 公用函数、经常重复的代码

        private void EnableControl(bool bEnable,string[] strsEnableControl)
        {
            try
            {
                int count = strsEnableControl.Length;
                for (int i = 0; i < count; i++)
                {
                    Control[] controls = Controls.Find(strsEnableControl[i], true);
                    foreach (var control in controls)
                    {
                        control.Enabled = bEnable;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("EnableControl:" + ex.Message);
            }
        }

        private void DeserializeSysParam()
        {
            try
            {
                using (StreamReader sr = new StreamReader(System.Environment.CurrentDirectory + "\\Config\\Sys.xml", Encoding.Default))
                {
                    CSysSerialize cSysSerialize = new CSysSerialize();

                    XmlSerializer xmlSerializer = new XmlSerializer(cSysSerialize.GetType());
                    cSysSerialize = (CSysSerialize)xmlSerializer.Deserialize(sr);

                    m_sImageSavePath = cSysSerialize.m_sImageSavePath;
                    m_iDeleteImageTime = cSysSerialize.m_iDeleteImageTime;
                    m_bIsSaveOKImage = cSysSerialize.m_bIsSaveOKImage;
                    m_iSysMode = cSysSerialize.m_iSysMode;
                    m_sFileSavePath = cSysSerialize.m_sFileSavePath;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("DeserializeSysParam:" + ex.Message);
            }
        }

        private void SerializeSysParam()
        {
            try
            {
                CSysSerialize cSysSerialize = new CSysSerialize();
                cSysSerialize.m_sImageSavePath = m_sImageSavePath;
                cSysSerialize.m_iDeleteImageTime = m_iDeleteImageTime;
                cSysSerialize.m_bIsSaveOKImage = m_bIsSaveOKImage;
                cSysSerialize.m_iSysMode = m_iSysMode;
                cSysSerialize.m_sFileSavePath = m_sFileSavePath;

                using(StreamWriter sw = new StreamWriter(System.Environment.CurrentDirectory+"\\Config\\Sys.xml",false,Encoding.Default))
	            {
		            XmlSerializer xmlSerializer = new XmlSerializer(cSysSerialize.GetType());
                    xmlSerializer.Serialize(sw, cSysSerialize);
	            }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SerializeSysParam:" + ex.Message);
            }
        }

        private void GetWritePrivateIntParam(string section,string key,string def,string filePath,int iVariable)
        {
            try
            {
                string retVal = string.Empty;
                GetPrivateProfileString(section, key, def, out retVal, filePath);
                iVariable = int.Parse(retVal);
                WritePrivateProfileString(section, key, iVariable.ToString(), filePath);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("GetWritePrivateIntParam:" + ex.Message);
            }
            
        }

        private void Cam1Procedure(HObject image, HTuple hWindowControlID, HTuple hvCamShm, HTuple hvCamNcm, HTuple hvCamMetrology)
        {
            try
            {
                #region 相机1Procedure

                HObject ho_ResultImage, ho_ResultObj;
                HTuple hv_ResultValue = new HTuple(), hv_ResultInfo = new HTuple(), hv_Result = new HTuple();

                HTuple hv_RatioPixelMaxX = m_dCam1Param[0], hv_RatioPixelMaxY = m_dCam1Param[1];
                HTuple hv_RatioPixelMax1X = m_dCam1Param[2], hv_RatioPixelMax1Y = m_dCam1Param[3];
                HTuple hv_RatioPixelMax2X = m_dCam1Param[4], hv_RatioPixelMax2Y = m_dCam1Param[5];
                HTuple hv_RatioPixelMax3X = m_dCam1Param[6], hv_RatioPixelMax3Y = m_dCam1Param[7];
                HTuple hv_RatioPixelRec = m_dCam1Param[8], hv_RatioPixel_d = m_dCam1Param[9];
                HTuple hv_X = m_dCam1Param[10], hv_Y = m_dCam1Param[11], hv_Diameter = m_dCam1Param[12];
                HTuple hv_GC_Circle1 = m_dCam1Param[13], hv_GC_Circle1_0 = m_dCam1Param[14];
                HTuple hv_GC_Circle2 = m_dCam1Param[15], hv_GC_Circle2_0 = m_dCam1Param[16];
                HTuple hv_GC_Circle3 = m_dCam1Param[17], hv_GC_Circle3_0 = m_dCam1Param[18];
                HTuple hv_GC_RC1 = m_dCam1Param[19], hv_GC_RC1_0 = m_dCam1Param[20];
                HTuple hv_GC_RC2 = m_dCam1Param[21], hv_GC_RC2_0 = m_dCam1Param[22];
                HTuple hv_GC_RC3 = m_dCam1Param[23], hv_GC_RC3_0 = m_dCam1Param[24];
                HTuple hv_GC_Rec = m_dCam1Param[25], hv_GC_Rec_0 = m_dCam1Param[26];
                HTuple hv_GC_Distance = m_dCam1Param[27], hv_GC_Distance_0 = m_dCam1Param[28];
                HTuple hv_GC_Angle = m_dCam1Param[29], hv_GC_Angle_0 = m_dCam1Param[30];
                HTuple hv_BC_Circle1 = m_dCam1Param[31], hv_BC_Circle2 = m_dCam1Param[32], hv_BC_Circle3 = m_dCam1Param[33];
                HTuple hv_BC_RC1 = m_dCam1Param[34], hv_BC_RC2 = m_dCam1Param[35], hv_BC_RC3 = m_dCam1Param[36];
                HTuple hv_BC_Rec = m_dCam1Param[37], hv_BC_Distance = m_dCam1Param[38], hv_BC_Angle = m_dCam1Param[39];
                HTuple hv_BC_Min = m_dCam1Param[40], hv_BC_Max = m_dCam1Param[41];

                Cam1_Procedure(image, out ho_ResultImage, out ho_ResultObj,
                               hWindowControlID, hvCamShm, hvCamMetrology,
                               hv_RatioPixelMaxX, hv_RatioPixelMaxY,
                               hv_RatioPixelMax1X, hv_RatioPixelMax1Y,
                               hv_RatioPixelMax2X, hv_RatioPixelMax2Y,
                               hv_RatioPixelMax3X, hv_RatioPixelMax3Y,
                               hv_RatioPixelRec, hv_RatioPixel_d,
                               hv_X, hv_Y, hv_Diameter,
                               hv_GC_Circle1, hv_GC_Circle1_0,
                               hv_GC_Circle2, hv_GC_Circle2_0,
                               hv_GC_Circle3, hv_GC_Circle3_0,
                               hv_GC_RC1, hv_GC_RC1_0,
                               hv_GC_RC2, hv_GC_RC2_0,
                               hv_GC_RC3, hv_GC_RC3_0,
                               hv_GC_Rec, hv_GC_Rec_0,
                               hv_GC_Distance, hv_GC_Distance_0,
                               hv_GC_Angle, hv_GC_Angle_0,
                               hv_BC_Circle1, hv_BC_Circle2, hv_BC_Circle3,
                               hv_BC_RC1, hv_BC_RC2, hv_BC_RC3,
                               hv_BC_Rec, hv_BC_Distance, hv_BC_Angle,
                               hv_BC_Min, hv_BC_Max,
                               out  hv_ResultValue, out  hv_ResultInfo, out  hv_Result);

                ManageResult("Cam1", 1,hv_Result, hv_ResultValue, image, ho_ResultImage);
                

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Cam1Procedure:" + ex.Message);
            }
        }

        private void Cam2Procedure(HObject image, HTuple hWindowControlID, HTuple hvCamShm, HTuple hvCamNcm, HTuple hvCamMetrology)
        {
            try
            {
                #region 相机2Procedure

                HObject ho_ResultImage, ho_ResultObj;
                HTuple hv_ResultValue = new HTuple(), hv_ResultInfo = new HTuple(), hv_Result = new HTuple();

                HTuple hv_RatioPixelMaxX = m_dCam1Param[0], hv_RatioPixelMaxY = m_dCam1Param[1];
                HTuple hv_RatioPixelMax1X = m_dCam1Param[2], hv_RatioPixelMax1Y = m_dCam1Param[3];
                HTuple hv_RatioPixelMax2X = m_dCam1Param[4], hv_RatioPixelMax2Y = m_dCam1Param[5];
                HTuple hv_RatioPixelMax3X = m_dCam1Param[6], hv_RatioPixelMax3Y = m_dCam1Param[7];
                HTuple hv_RatioPixelRec = m_dCam1Param[8], hv_RatioPixel_d = m_dCam1Param[9];
                HTuple hv_X = m_dCam1Param[10], hv_Y = m_dCam1Param[11], hv_Diameter = m_dCam1Param[12];
                HTuple hv_GC_Circle1 = m_dCam1Param[13], hv_GC_Circle1_0 = m_dCam1Param[14];
                HTuple hv_GC_Circle2 = m_dCam1Param[15], hv_GC_Circle2_0 = m_dCam1Param[16];
                HTuple hv_GC_Circle3 = m_dCam1Param[17], hv_GC_Circle3_0 = m_dCam1Param[18];
                HTuple hv_GC_RC1 = m_dCam1Param[19], hv_GC_RC1_0 = m_dCam1Param[20];
                HTuple hv_GC_RC2 = m_dCam1Param[21], hv_GC_RC2_0 = m_dCam1Param[22];
                HTuple hv_GC_RC3 = m_dCam1Param[23], hv_GC_RC3_0 = m_dCam1Param[24];
                HTuple hv_GC_Rec = m_dCam1Param[25], hv_GC_Rec_0 = m_dCam1Param[26];
                HTuple hv_GC_Distance = m_dCam1Param[27], hv_GC_Distance_0 = m_dCam1Param[28];
                HTuple hv_GC_Angle = m_dCam1Param[29], hv_GC_Angle_0 = m_dCam1Param[30];
                HTuple hv_BC_Circle1 = m_dCam1Param[31], hv_BC_Circle2 = m_dCam1Param[32], hv_BC_Circle3 = m_dCam1Param[33];
                HTuple hv_BC_RC1 = m_dCam1Param[34], hv_BC_RC2 = m_dCam1Param[35], hv_BC_RC3 = m_dCam1Param[36];
                HTuple hv_BC_Rec = m_dCam1Param[37], hv_BC_Distance = m_dCam1Param[38], hv_BC_Angle = m_dCam1Param[39];
                HTuple hv_BC_Min = m_dCam1Param[40], hv_BC_Max = m_dCam1Param[41];

                Cam2_Procedure(image, out ho_ResultImage, out ho_ResultObj,
                               hWindowControlID, hvCamShm, hvCamMetrology,
                               hv_RatioPixelMaxX, hv_RatioPixelMaxY,
                               hv_RatioPixelMax1X, hv_RatioPixelMax1Y,
                               hv_RatioPixelMax2X, hv_RatioPixelMax2Y,
                               hv_RatioPixelMax3X, hv_RatioPixelMax3Y,
                               hv_RatioPixelRec, hv_RatioPixel_d,
                               hv_X, hv_Y, hv_Diameter,
                               hv_GC_Circle1, hv_GC_Circle1_0,
                               hv_GC_Circle2, hv_GC_Circle2_0,
                               hv_GC_Circle3, hv_GC_Circle3_0,
                               hv_GC_RC1, hv_GC_RC1_0,
                               hv_GC_RC2, hv_GC_RC2_0,
                               hv_GC_RC3, hv_GC_RC3_0,
                               hv_GC_Rec, hv_GC_Rec_0,
                               hv_GC_Distance, hv_GC_Distance_0,
                               hv_GC_Angle, hv_GC_Angle_0,
                               hv_BC_Circle1, hv_BC_Circle2, hv_BC_Circle3,
                               hv_BC_RC1, hv_BC_RC2, hv_BC_RC3,
                               hv_BC_Rec, hv_BC_Distance, hv_BC_Angle,
                               hv_BC_Min, hv_BC_Max,
                               out  hv_ResultValue, out  hv_ResultInfo, out  hv_Result);

                ManageResult("Cam2", 2,hv_Result, hv_ResultValue, image, ho_ResultImage);


                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Cam2Procedure:" + ex.Message);
            }
        }

        private void Cam3Procedure(HObject image, HTuple hWindowControlID, HTuple hvCamShm, HTuple hvCamNcm, HTuple hvCamMetrology)
        {
            try
            {
                #region 相机3Procedure

                HObject ho_ResultImage, ho_ResultObj;
                HTuple hv_ResultValue = new HTuple(), hv_ResultInfo = new HTuple(), hv_Result = new HTuple();

                HTuple hv_RatioPixelMaxX = m_dCam1Param[0], hv_RatioPixelMaxY = m_dCam1Param[1];
                HTuple hv_RatioPixelMax1X = m_dCam1Param[2], hv_RatioPixelMax1Y = m_dCam1Param[3];
                HTuple hv_RatioPixelMax2X = m_dCam1Param[4], hv_RatioPixelMax2Y = m_dCam1Param[5];
                HTuple hv_RatioPixelMax3X = m_dCam1Param[6], hv_RatioPixelMax3Y = m_dCam1Param[7];
                HTuple hv_RatioPixelRec = m_dCam1Param[8], hv_RatioPixel_d = m_dCam1Param[9];
                HTuple hv_X = m_dCam1Param[10], hv_Y = m_dCam1Param[11], hv_Diameter = m_dCam1Param[12];
                HTuple hv_GC_Circle1 = m_dCam1Param[13], hv_GC_Circle1_0 = m_dCam1Param[14];
                HTuple hv_GC_Circle2 = m_dCam1Param[15], hv_GC_Circle2_0 = m_dCam1Param[16];
                HTuple hv_GC_Circle3 = m_dCam1Param[17], hv_GC_Circle3_0 = m_dCam1Param[18];
                HTuple hv_GC_RC1 = m_dCam1Param[19], hv_GC_RC1_0 = m_dCam1Param[20];
                HTuple hv_GC_RC2 = m_dCam1Param[21], hv_GC_RC2_0 = m_dCam1Param[22];
                HTuple hv_GC_RC3 = m_dCam1Param[23], hv_GC_RC3_0 = m_dCam1Param[24];
                HTuple hv_GC_Rec = m_dCam1Param[25], hv_GC_Rec_0 = m_dCam1Param[26];
                HTuple hv_GC_Distance = m_dCam1Param[27], hv_GC_Distance_0 = m_dCam1Param[28];
                HTuple hv_GC_Angle = m_dCam1Param[29], hv_GC_Angle_0 = m_dCam1Param[30];
                HTuple hv_BC_Circle1 = m_dCam1Param[31], hv_BC_Circle2 = m_dCam1Param[32], hv_BC_Circle3 = m_dCam1Param[33];
                HTuple hv_BC_RC1 = m_dCam1Param[34], hv_BC_RC2 = m_dCam1Param[35], hv_BC_RC3 = m_dCam1Param[36];
                HTuple hv_BC_Rec = m_dCam1Param[37], hv_BC_Distance = m_dCam1Param[38], hv_BC_Angle = m_dCam1Param[39];
                HTuple hv_BC_Min = m_dCam1Param[40], hv_BC_Max = m_dCam1Param[41];

                Cam3_Procedure(image, out ho_ResultImage, out ho_ResultObj,
                               hWindowControlID, hvCamShm, hvCamMetrology,
                               hv_RatioPixelMaxX, hv_RatioPixelMaxY,
                               hv_RatioPixelMax1X, hv_RatioPixelMax1Y,
                               hv_RatioPixelMax2X, hv_RatioPixelMax2Y,
                               hv_RatioPixelMax3X, hv_RatioPixelMax3Y,
                               hv_RatioPixelRec, hv_RatioPixel_d,
                               hv_X, hv_Y, hv_Diameter,
                               hv_GC_Circle1, hv_GC_Circle1_0,
                               hv_GC_Circle2, hv_GC_Circle2_0,
                               hv_GC_Circle3, hv_GC_Circle3_0,
                               hv_GC_RC1, hv_GC_RC1_0,
                               hv_GC_RC2, hv_GC_RC2_0,
                               hv_GC_RC3, hv_GC_RC3_0,
                               hv_GC_Rec, hv_GC_Rec_0,
                               hv_GC_Distance, hv_GC_Distance_0,
                               hv_GC_Angle, hv_GC_Angle_0,
                               hv_BC_Circle1, hv_BC_Circle2, hv_BC_Circle3,
                               hv_BC_RC1, hv_BC_RC2, hv_BC_RC3,
                               hv_BC_Rec, hv_BC_Distance, hv_BC_Angle,
                               hv_BC_Min, hv_BC_Max,
                               out  hv_ResultValue, out  hv_ResultInfo, out  hv_Result);

                ManageResult("Cam3", 3, hv_Result, hv_ResultValue, image, ho_ResultImage);


                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Cam3Procedure:" + ex.Message);
            }
        }

        private void Cam4Procedure(HObject image, HTuple hWindowControlID, HTuple hvCamShm, HTuple hvCamNcm, HTuple hvCamMetrology)
        {
            try
            {
                #region 相机4Procedure

                HObject ho_ResultImage, ho_ResultObj;
                HTuple hv_ResultValue = new HTuple(), hv_ResultInfo = new HTuple(), hv_Result = new HTuple();

                HTuple hv_RatioPixelMaxX = m_dCam1Param[0], hv_RatioPixelMaxY = m_dCam1Param[1];
                HTuple hv_RatioPixelMax1X = m_dCam1Param[2], hv_RatioPixelMax1Y = m_dCam1Param[3];
                HTuple hv_RatioPixelMax2X = m_dCam1Param[4], hv_RatioPixelMax2Y = m_dCam1Param[5];
                HTuple hv_RatioPixelMax3X = m_dCam1Param[6], hv_RatioPixelMax3Y = m_dCam1Param[7];
                HTuple hv_RatioPixelRec = m_dCam1Param[8], hv_RatioPixel_d = m_dCam1Param[9];
                HTuple hv_X = m_dCam1Param[10], hv_Y = m_dCam1Param[11], hv_Diameter = m_dCam1Param[12];
                HTuple hv_GC_Circle1 = m_dCam1Param[13], hv_GC_Circle1_0 = m_dCam1Param[14];
                HTuple hv_GC_Circle2 = m_dCam1Param[15], hv_GC_Circle2_0 = m_dCam1Param[16];
                HTuple hv_GC_Circle3 = m_dCam1Param[17], hv_GC_Circle3_0 = m_dCam1Param[18];
                HTuple hv_GC_RC1 = m_dCam1Param[19], hv_GC_RC1_0 = m_dCam1Param[20];
                HTuple hv_GC_RC2 = m_dCam1Param[21], hv_GC_RC2_0 = m_dCam1Param[22];
                HTuple hv_GC_RC3 = m_dCam1Param[23], hv_GC_RC3_0 = m_dCam1Param[24];
                HTuple hv_GC_Rec = m_dCam1Param[25], hv_GC_Rec_0 = m_dCam1Param[26];
                HTuple hv_GC_Distance = m_dCam1Param[27], hv_GC_Distance_0 = m_dCam1Param[28];
                HTuple hv_GC_Angle = m_dCam1Param[29], hv_GC_Angle_0 = m_dCam1Param[30];
                HTuple hv_BC_Circle1 = m_dCam1Param[31], hv_BC_Circle2 = m_dCam1Param[32], hv_BC_Circle3 = m_dCam1Param[33];
                HTuple hv_BC_RC1 = m_dCam1Param[34], hv_BC_RC2 = m_dCam1Param[35], hv_BC_RC3 = m_dCam1Param[36];
                HTuple hv_BC_Rec = m_dCam1Param[37], hv_BC_Distance = m_dCam1Param[38], hv_BC_Angle = m_dCam1Param[39];
                HTuple hv_BC_Min = m_dCam1Param[40], hv_BC_Max = m_dCam1Param[41];

                Cam2_Procedure(image, out ho_ResultImage, out ho_ResultObj,
                               hWindowControlID, hvCamShm, hvCamMetrology,
                               hv_RatioPixelMaxX, hv_RatioPixelMaxY,
                               hv_RatioPixelMax1X, hv_RatioPixelMax1Y,
                               hv_RatioPixelMax2X, hv_RatioPixelMax2Y,
                               hv_RatioPixelMax3X, hv_RatioPixelMax3Y,
                               hv_RatioPixelRec, hv_RatioPixel_d,
                               hv_X, hv_Y, hv_Diameter,
                               hv_GC_Circle1, hv_GC_Circle1_0,
                               hv_GC_Circle2, hv_GC_Circle2_0,
                               hv_GC_Circle3, hv_GC_Circle3_0,
                               hv_GC_RC1, hv_GC_RC1_0,
                               hv_GC_RC2, hv_GC_RC2_0,
                               hv_GC_RC3, hv_GC_RC3_0,
                               hv_GC_Rec, hv_GC_Rec_0,
                               hv_GC_Distance, hv_GC_Distance_0,
                               hv_GC_Angle, hv_GC_Angle_0,
                               hv_BC_Circle1, hv_BC_Circle2, hv_BC_Circle3,
                               hv_BC_RC1, hv_BC_RC2, hv_BC_RC3,
                               hv_BC_Rec, hv_BC_Distance, hv_BC_Angle,
                               hv_BC_Min, hv_BC_Max,
                               out  hv_ResultValue, out  hv_ResultInfo, out  hv_Result);

                ManageResult("Cam4", 4, hv_Result, hv_ResultValue, image, ho_ResultImage);


                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Cam4Procedure:" + ex.Message);
            }
        }

        #endregion

        #region 存储数据

        private bool WriteCamData(int index, int iRes, HTuple resultValue)
        {
            try
            {
                string strData = string.Empty;
                strData = strData + iRes.ToString();
                for (int i = 0; i <= resultValue.Length - 1;i++ )
                {
                    strData = strData +","+ resultValue[i].D.ToString();
                }

                switch (index)
                {
                    case 1:
                        m_ListCam1.Add(strData);
                        break;
                    case 2:
                        m_ListCam2.Add(strData);
                        break;
                    case 3:
                        m_ListCam3.Add(strData);
                        break;
                    case 4:
                        m_ListCam4.Add(strData);
                        break;
                    default:
                        break;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("WriteCamData:" + ex.Message);
                return false;
            }
        }

        private void WriteCam(string camIndex,int index)
        {
            try
            {
                string path = m_sFileSavePath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
                string strRes = "0";
                string[] strs = null;
                if ( (index == 1) && (camIndex=="Cam1"))
                {
                    string[] strCam1s = ManageList(m_ListCam1);
                    string[] strCam1sRes = CutStrings(strCam1s);
                    
                    strRes = strCam1s[0];
                    strs = strCam1sRes;

                    WriteCamTitle(path, strs);
                    WriteCamData(path, strRes, strs); 
                                       
                }
                else if ( (index == 2) && (camIndex == "Cam2"))
                {
                    string[] strCam1s = new string[]{};
                    string[] strCam1sRes = new string[] { };
                    string[] strCam2s = ManageList(m_ListCam2);
                    string[] strCam2sRes = CutStrings(strCam2s);

                    GetCamStringResult(m_ListCam1,out strCam1s,out strCam1sRes);
                    
                    if ((strCam1s[0] == "1") && (strCam2s[0] == "1"))
                    {
                        strRes = "1";
                    }
                    strs = new string[strCam1sRes.Length + strCam2sRes.Length];
                    strCam1sRes.CopyTo(strs, 0);
                    strCam2sRes.CopyTo(strs, strCam1sRes.Length);

                    WriteCamTitle(path, strs);
                    WriteCamData(path, strRes, strs); 

                }
                else if ((index == 3) && (camIndex == "Cam3"))
                {
                    string[] strCam1s = new string[] { };
                    string[] strCam1sRes = new string[] { };
                    string[] strCam2s = new string[] { };
                    string[] strCam2sRes = new string[] { };
                    string[] strCam3s = ManageList(m_ListCam3);
                    string[] strCam3sRes = CutStrings(strCam3s);

                    GetCamStringResult(m_ListCam1, out strCam1s, out strCam1sRes);
                    GetCamStringResult(m_ListCam2, out strCam2s, out strCam2sRes);

                    if ((strCam1s[0] == "1") && (strCam2s[0] == "1") && (strCam3s[0] == "1"))
                    {
                        strRes = "1";
                    }
                    strs = new string[strCam1sRes.Length + strCam2sRes.Length + strCam3sRes.Length];
                    strCam1sRes.CopyTo(strs, 0);
                    strCam2sRes.CopyTo(strs, strCam1sRes.Length);
                    strCam3sRes.CopyTo(strs, strCam1sRes.Length + strCam2sRes.Length);

                    WriteCamTitle(path, strs);
                    WriteCamData(path, strRes, strs); 
                    
                }
                else if ((index == 4) && (camIndex == "Cam4"))
                {
                    string[] strCam1s = new string[] { };
                    string[] strCam1sRes = new string[] { };
                    string[] strCam2s = new string[] { };
                    string[] strCam2sRes = new string[] { };
                    string[] strCam3s = new string[] { };
                    string[] strCam3sRes = new string[] { };
                    string[] strCam4s = ManageList(m_ListCam4);
                    string[] strCam4sRes = CutStrings(strCam4s);

                    GetCamStringResult(m_ListCam1, out strCam1s, out strCam1sRes);
                    GetCamStringResult(m_ListCam2, out strCam2s, out strCam2sRes);
                    GetCamStringResult(m_ListCam3, out strCam3s, out strCam3sRes);

                    if ((strCam1s[0] == "1") && (strCam2s[0] == "1") && (strCam3s[0] == "1") && (strCam4s[0] == "1"))
                    {
                        strRes = "1";
                    }
                    strs = new string[strCam1sRes.Length + strCam2sRes.Length + strCam3sRes.Length + strCam4sRes.Length];
                    strCam1sRes.CopyTo(strs, 0);
                    strCam2sRes.CopyTo(strs, strCam1sRes.Length);
                    strCam3sRes.CopyTo(strs, strCam1sRes.Length + strCam2sRes.Length);
                    strCam4sRes.CopyTo(strs, strCam1sRes.Length + strCam2sRes.Length + strCam3sRes.Length);

                    WriteCamTitle(path, strs);
                    WriteCamData(path, strRes, strs); 
                }

                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("WriteLog:" + ex.Message);
            }
        }

        private void ManageResult(string camIndex, int index, HTuple hvResult, HTuple hvResultValue, HObject image, HObject resultImage)
        {
            try
            {
                #region 结果处理(返回信号、存储数据、存储图像)

                if (hvResult[0].I == 1)
                {
                    lock (this)
                    {
                        UpdatelistBox(camIndex + "-OK！");
                    }

                    #region 存储OK数据

                    WriteCamData(index, 1, hvResultValue);
                    WriteCam(camIndex, m_iSysMode);

                    #endregion

                    lock (this)
                    {
                        UpdatelistBox(camIndex + "存储数据完成！");
                    }

                    #region 存储OK图像

                    if (m_bIsSaveOKImage == true)
                    {
                        string strWriteDiretory = m_sImageSavePath + "\\"
                                              + DateTime.Now.ToString("yyyy-MM-dd")
                                              + "\\" + camIndex + "\\OK";
                        WriteImage(image, resultImage, strWriteDiretory);
                    }

                    #endregion

                    lock (this)
                    {
                        UpdatelistBox(camIndex + "存储图像完成！");
                    }

                }
                else
                {
                    lock (this)
                    {
                        UpdatelistBox(camIndex + "-NG！");
                    }

                    #region 存储NG数据

                    WriteCamData(index, 1, hvResultValue);
                    WriteCam(camIndex, m_iSysMode);

                    #endregion

                    lock (this)
                    {
                        UpdatelistBox(camIndex + "存储数据完成！");
                    }

                    #region 存储NG图像

                    string strWriteDiretory = m_sImageSavePath + "\\"
                                              + DateTime.Now.ToString("yyyy-MM-dd")
                                              + "\\" + camIndex + "\\NG";
                    WriteImage(image, resultImage, strWriteDiretory);

                    #endregion

                    lock (this)
                    {
                        UpdatelistBox(camIndex + "存储图像完成！");
                    }

                }

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(camIndex + "处理结果出错:" + ex.Message);
            }
        }

        private void GetCamStringResult(List<string> listCam, out string[] strCam, out string[] strCamRes)
        {
            strCam = new string[] { };
            strCamRes = new string[] { };
            try
            {
                if (listCam.Count > 0)
                {
                    strCam = ManageList(listCam);
                }
                else
                {
                    strCam = ManageEmptyString(15, -100);

                }

                strCamRes = CutStrings(strCam);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("GetCamStringResult:" + ex.Message);
            }
        }

        private string[] ManageList(List<string> strList)
        {
            string[] strs = new string[] { };
            try
            {
                string str = strList[0];
                strList.RemoveAt(0);
                strs = str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                return strs;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("ManageList:" + ex.Message);
                return strs;
            }
        }

        private string[] ManageEmptyString(int count, int value)
        {
            string[] strs = new string[] { };
            try
            {
                string str = string.Empty;
                str = str + "0";
                for (int i = 0; i < count; i++)
                {
                    str = str + "," + value.ToString();
                }

                strs = str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                return strs;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("ManageEmptyString:" + ex.Message);
                return strs;
            }
        }

        private string[] CutStrings(string[] strs)
        {
            string[] returnStrs = new string[] { };
            try
            {
                returnStrs = null;
                returnStrs = new string[strs.Length - 1];
                for (int i = 0; i < strs.Length - 1; i++)
                {
                    returnStrs[i] = strs[i + 1];
                }

                return returnStrs;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("CutStrings:" + ex.Message);
                return returnStrs;
            }
        }

        private void WriteCamTitle(string path,string[] strs)
        {
            try
            {                
                if (File.Exists(path) == false)
                {
                    using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                    {
                        string sTitle = string.Empty;
                        sTitle = sTitle + "时间";
                        for (int i = 0; i < strs.Length; i++)
                        {
                            sTitle = sTitle + "," + "Result" + (i+1).ToString();
                        }
                        sTitle = sTitle +"," + "结果"+","+"\r\n";

                        sw.Write(sTitle);
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("WriteOneCamLog:" + ex.Message);
            }
        }

        private void WriteCamData(string path, string strRes,string[] strs)
        {
            try
            {
                string sRes = "FAIL";
                if (strRes == "1")
                {
                    sRes = "PASS";
                }

                string sTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
                using (StreamWriter sw = new StreamWriter(path,true,Encoding.Default))
                {
                    sw.Write(sTime);
                    for (int i = 0; i < strs.Length;i++ )
                    {
                        sw.Write("," + double.Parse(strs[i]).ToString("F3"));
                    }
                    sw.Write(","+sRes+","+"\r\n");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("WriteOneCamData:" + ex.Message);
            }
        }

        #endregion

        #region 存储-删除图像

        private bool WriteImage(HObject oriImage, HObject resImage, string strWriteDiretory)
        {
            try
            {
                if (Directory.Exists(strWriteDiretory) == false)
                {
                    Directory.CreateDirectory(strWriteDiretory);
                }

                DateTime dt = DateTime.Now;
                string strData = dt.ToString("yyyyMMdd");
                string strTime = dt.ToString("HHmmss");
                string strFile = strWriteDiretory + "\\" + strData + strTime + "_Ori.tif";
                HOperatorSet.WriteImage(oriImage, "tiff", 0, strFile);
                strFile = strWriteDiretory + "\\" + strData + strTime + "_Res.jpg";
                HOperatorSet.WriteImage(resImage, "jpeg", 0, strFile);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DeleteImage(string strListDiretory, int iDeleteDay)
        {
            try
            {
                if (Directory.Exists(strListDiretory) == false)
                {
                    Directory.CreateDirectory(strListDiretory);
                }

                string[] strsDiretory = Directory.GetDirectories(strListDiretory);

                for (int m = 0; m < strsDiretory.Length; m++)
                {
                    var item = strsDiretory[m];
                    int index = item.LastIndexOf("\\");
                    string strDiretoryName = item.Substring(index + 1);

                    for (int i = iDeleteDay; i <= iDeleteDay + 10; i++)
                    {
                        string strData = DateTime.Now.ToString("yyyy-MM-dd");
                        TimeSpan ts = new TimeSpan(i, 0, 0, 0);

                        string strDeleteData = DateTime.Now.Subtract(ts).ToString("yyyy-MM-dd");

                        if (strDiretoryName == strDeleteData)
                        {
                            Directory.Delete(item, true);
                        }
                    }

                }

                return true;
            }
            catch
            {
                return false;
            }


        }

        #endregion

        #region Halcon代码

        #region 相机1

        public void AlignImage(HObject ho_Image1, out HObject ho_RotateImage1, HTuple hv_LineParameter,
      HTuple hv_AlignAngle, out HTuple hv_HomMat2DInvert)
        {




            // Local iconic variables 

            // Local control variables 

            HTuple hv_Width = null, hv_Height = null, hv_Angle = null;
            HTuple hv_DeltAngle = null, hv_HomMat2DRotate = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_RotateImage1);
            HOperatorSet.GetImageSize(ho_Image1, out hv_Width, out hv_Height);
            HOperatorSet.AngleLx(hv_LineParameter.TupleSelect(0), hv_LineParameter.TupleSelect(
                1), hv_LineParameter.TupleSelect(2), hv_LineParameter.TupleSelect(3), out hv_Angle);
            hv_DeltAngle = (hv_AlignAngle.TupleRad()) - hv_Angle;
            HOperatorSet.HomMat2dIdentity(out hv_HomMat2DRotate);
            HOperatorSet.HomMat2dRotate(hv_HomMat2DRotate, hv_DeltAngle, hv_Height / 2, hv_Width / 2,
                out hv_HomMat2DRotate);
            ho_RotateImage1.Dispose();
            HOperatorSet.AffineTransImage(ho_Image1, out ho_RotateImage1, hv_HomMat2DRotate,
                "constant", "false");
            HOperatorSet.HomMat2dInvert(hv_HomMat2DRotate, out hv_HomMat2DInvert);

            return;
        }

        public void Cam1_Procedure(HObject ho_Image, out HObject ho_ResultImage, out HObject ho_ResultObj,
      HTuple hv_WindowHandle, HTuple hv_ModelId, HTuple hv_MetrologyHandle, HTuple hv_RatioPixelMaxX,
      HTuple hv_RatioPixelMaxY, HTuple hv_RatioPixelMax1X, HTuple hv_RatioPixelMax1Y,
      HTuple hv_RatioPixelMax2X, HTuple hv_RatioPixelMax2Y, HTuple hv_RatioPixelMax3X,
      HTuple hv_RatioPixelMax3Y, HTuple hv_RatioPixelRec, HTuple hv_RatioPixel_d,
      HTuple hv_X, HTuple hv_Y, HTuple hv_Diameter, HTuple hv_GC_Circle1, HTuple hv_GC_Circle1_0,
      HTuple hv_GC_Circle2, HTuple hv_GC_Circle2_0, HTuple hv_GC_Circle3, HTuple hv_GC_Circle3_0,
      HTuple hv_GC_RC1, HTuple hv_GC_RC1_0, HTuple hv_GC_RC2, HTuple hv_GC_RC2_0,
      HTuple hv_GC_RC3, HTuple hv_GC_RC3_0, HTuple hv_GC_Rec, HTuple hv_GC_Rec_0,
      HTuple hv_GC_Distance, HTuple hv_GC_Distance_0, HTuple hv_GC_Angle, HTuple hv_GC_Angle_0,
      HTuple hv_BC_Circle1, HTuple hv_BC_Circle2, HTuple hv_BC_Circle3, HTuple hv_BC_RC1,
      HTuple hv_BC_RC2, HTuple hv_BC_RC3, HTuple hv_BC_Rec, HTuple hv_BC_Distance,
      HTuple hv_BC_Angle, HTuple hv_BC_Min, HTuple hv_BC_Max, out HTuple hv_ResultValue,
      out HTuple hv_ResultInfo, out HTuple hv_Result)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_ModelContours, ho_CircleMaxContour;
            HObject ho_CircleOneContour, ho_CircleTwoContour, ho_CircleThreeContour;
            HObject ho_RectangleContour, ho_DownLineContour, ho_UpLineContour;
            HObject ho_LineContour, ho_Line2Contour, ho_ContourX, ho_ContourY;
            HObject ho_LineContours, ho_RotateImage, ho_CircleMaxContours;
            HObject ho_CircleOneContours, ho_CircleTwoContours, ho_CircleThreeContours;
            HObject ho_RectangleContours, ho_DownLineContours, ho_UpLineContours;
            HObject ho_Line2Contours;

            // Local control variables 

            HTuple hv_ModelRow = null, hv_ModelColumn = null;
            HTuple hv_ModelAngle = null, hv_LineParameter = null, hv_CircleMaxParameter = null;
            HTuple hv_CircleOneParameter = null, hv_CircleTwoParameter = null;
            HTuple hv_CircleThreeParameter = null, hv_Line2Parameter = null;
            HTuple hv_ResultC1 = null, hv_ResultDiameter1 = null, hv_ResultC2 = null;
            HTuple hv_ResultDiameter2 = null, hv_ResultC3 = null, hv_ResultDiameter3 = null;
            HTuple hv_ResultRec = null, hv_ResultDistance = null, hv_ResultAngle = null;
            HTuple hv_ResultMaxDiameter = null, hv_ResultMax1X = null;
            HTuple hv_ResultMax1Y = null, hv_ResultMax2X = null, hv_ResultMax2Y = null;
            HTuple hv_ResultMax3X = null, hv_ResultMax3Y = null, hv_ResultMax1X1 = null;
            HTuple hv_ResultMax1Y1 = null, hv_ResultMax2X1 = null;
            HTuple hv_ResultMax2Y1 = null, hv_ResultMax3X1 = null;
            HTuple hv_ResultMax3Y1 = null, hv_ModelScore = null, hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_DumpWindowHandle = new HTuple();
            HTuple hv_HomMat2D = null, hv_Row6 = null, hv_Column6 = null;
            HTuple hv_HomMat2DInvert = null, hv_RectangleParameter = null;
            HTuple hv_DownLineParameter = null, hv_UpLineParameter = null;
            HTuple hv_DistanceY = new HTuple(), hv_DistanceX = new HTuple();
            HTuple hv_RowProj = new HTuple(), hv_ColProj = new HTuple();
            HTuple hv_ResultMaxX = new HTuple(), hv_ResultMaxY = new HTuple();
            HTuple hv_Rx = new HTuple(), hv_Ry = new HTuple(), hv_deltX = new HTuple();
            HTuple hv_deltY = new HTuple(), hv_dR = new HTuple(), hv_deltR1 = new HTuple();
            HTuple hv_deltR2 = new HTuple(), hv_deltR3 = new HTuple();
            HTuple hv_DistanceMin = new HTuple(), hv_DistanceMax = new HTuple();
            HTuple hv_ResultMaxDistance = new HTuple(), hv_ResultMinDistance = new HTuple();
            HTuple hv_Angle = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ResultImage);
            HOperatorSet.GenEmptyObj(out ho_ResultObj);
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContour);
            HOperatorSet.GenEmptyObj(out ho_CircleOneContour);
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContour);
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContour);
            HOperatorSet.GenEmptyObj(out ho_RectangleContour);
            HOperatorSet.GenEmptyObj(out ho_DownLineContour);
            HOperatorSet.GenEmptyObj(out ho_UpLineContour);
            HOperatorSet.GenEmptyObj(out ho_LineContour);
            HOperatorSet.GenEmptyObj(out ho_Line2Contour);
            HOperatorSet.GenEmptyObj(out ho_ContourX);
            HOperatorSet.GenEmptyObj(out ho_ContourY);
            HOperatorSet.GenEmptyObj(out ho_LineContours);
            HOperatorSet.GenEmptyObj(out ho_RotateImage);
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContours);
            HOperatorSet.GenEmptyObj(out ho_CircleOneContours);
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContours);
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContours);
            HOperatorSet.GenEmptyObj(out ho_RectangleContours);
            HOperatorSet.GenEmptyObj(out ho_DownLineContours);
            HOperatorSet.GenEmptyObj(out ho_UpLineContours);
            HOperatorSet.GenEmptyObj(out ho_Line2Contours);

            //初始化输出变量
            ho_ResultImage.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ResultImage);
            ho_ResultObj.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ResultObj);
            hv_ResultValue = new HTuple();
            hv_ResultValue[0] = -100;
            hv_ResultValue[1] = -100;
            hv_ResultValue[2] = -100;
            hv_ResultValue[3] = -100;
            hv_ResultValue[4] = -100;
            hv_ResultValue[5] = -100;
            hv_ResultValue[6] = -100;
            hv_ResultValue[7] = -100;
            hv_ResultValue[8] = -100;
            hv_ResultValue[9] = -100;
            hv_ResultValue[10] = -100;
            hv_ResultValue[11] = -100;
            hv_ResultValue[12] = -100;
            hv_ResultValue[13] = -100;
            hv_ResultValue[14] = -100;
            hv_ResultInfo = "";
            hv_Result = 0;

            //初始化中间变量
            hv_ModelRow = new HTuple();
            hv_ModelColumn = new HTuple();
            hv_ModelAngle = new HTuple();
            hv_LineParameter = new HTuple();
            hv_CircleMaxParameter = new HTuple();
            hv_CircleOneParameter = new HTuple();
            hv_CircleTwoParameter = new HTuple();
            hv_CircleThreeParameter = new HTuple();
            hv_Line2Parameter = new HTuple();
            hv_ResultC1 = new HTuple();
            hv_ResultDiameter1 = new HTuple();
            hv_ResultC2 = new HTuple();
            hv_ResultDiameter2 = new HTuple();
            hv_ResultC3 = new HTuple();
            hv_ResultDiameter3 = new HTuple();
            hv_ResultRec = new HTuple();
            hv_ResultDistance = new HTuple();
            hv_ResultAngle = new HTuple();
            hv_ResultMaxDiameter = new HTuple();
            hv_ResultMax1X = new HTuple();
            hv_ResultMax1Y = new HTuple();
            hv_ResultMax2X = new HTuple();
            hv_ResultMax2Y = new HTuple();
            hv_ResultMax3X = new HTuple();
            hv_ResultMax3Y = new HTuple();
            hv_ResultMax1X1 = new HTuple();
            hv_ResultMax1Y1 = new HTuple();
            hv_ResultMax2X1 = new HTuple();
            hv_ResultMax2Y1 = new HTuple();
            hv_ResultMax3X1 = new HTuple();
            hv_ResultMax3Y1 = new HTuple();
            ho_ModelContours.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            ho_CircleMaxContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContour);
            ho_CircleOneContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleOneContour);
            ho_CircleTwoContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContour);
            ho_CircleThreeContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContour);
            ho_RectangleContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_RectangleContour);
            ho_DownLineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_DownLineContour);
            ho_UpLineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_UpLineContour);
            ho_LineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_LineContour);
            ho_Line2Contour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Line2Contour);
            ho_ContourX.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ContourX);
            ho_ContourY.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ContourY);

            try
            {

                //旋转矫正图像
                HOperatorSet.FindShapeModel(ho_Image, hv_ModelId, (new HTuple(-45)).TupleRad()
                    , (new HTuple(90)).TupleRad(), 0.5, 1, 0.5, "least_squares", (new HTuple(8)).TupleConcat(
                    1), 0.75, out hv_ModelRow, out hv_ModelColumn, out hv_ModelAngle, out hv_ModelScore);
                if ((int)(new HTuple((new HTuple(hv_ModelRow.TupleLength())).TupleEqual(0))) != 0)
                {
                    HOperatorSet.ClearWindow(hv_WindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
                    set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                    disp_message(hv_WindowHandle, "定位失败:NG", "image", 1600, 10, "red", "false");
                    hv_Result = 0;
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);(FAI-17);(FAI-18);(FAI-12);(FAI-19);(FAI-14);(FAI-3);(FAI-13);(FAI-11);";
                    //
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                    HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                    HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                    HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_DumpWindowHandle);
                    set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                    disp_message(hv_DumpWindowHandle, "定位失败:NG", "image", 1600, 10, "red",
                        "false");
                    ho_ResultImage.Dispose();
                    HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                    HOperatorSet.CloseWindow(hv_DumpWindowHandle);
                    //
                    ho_ModelContours.Dispose();
                    ho_CircleMaxContour.Dispose();
                    ho_CircleOneContour.Dispose();
                    ho_CircleTwoContour.Dispose();
                    ho_CircleThreeContour.Dispose();
                    ho_RectangleContour.Dispose();
                    ho_DownLineContour.Dispose();
                    ho_UpLineContour.Dispose();
                    ho_LineContour.Dispose();
                    ho_Line2Contour.Dispose();
                    ho_ContourX.Dispose();
                    ho_ContourY.Dispose();
                    ho_LineContours.Dispose();
                    ho_RotateImage.Dispose();
                    ho_CircleMaxContours.Dispose();
                    ho_CircleOneContours.Dispose();
                    ho_CircleTwoContours.Dispose();
                    ho_CircleThreeContours.Dispose();
                    ho_RectangleContours.Dispose();
                    ho_DownLineContours.Dispose();
                    ho_UpLineContours.Dispose();
                    ho_Line2Contours.Dispose();

                    return;
                }
                //
                //显示模板
                ho_ModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelId, 1);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_ModelRow, hv_ModelColumn, hv_ModelAngle,
                    out hv_HomMat2D);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.AffineTransContourXld(ho_ModelContours, out ExpTmpOutVar_0, hv_HomMat2D);
                    ho_ModelContours.Dispose();
                    ho_ModelContours = ExpTmpOutVar_0;
                }
                //
                //旋转图像
                HOperatorSet.AlignMetrologyModel(hv_MetrologyHandle, hv_ModelRow, hv_ModelColumn,
                    hv_ModelAngle);
                HOperatorSet.ApplyMetrologyModel(ho_Image, hv_MetrologyHandle);
                ho_LineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_LineContours, hv_MetrologyHandle,
                    0, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 0, "all", "result_type",
                    "all_param", out hv_LineParameter);
                ho_LineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_LineContour, hv_MetrologyHandle,
                    0, "all", 1.5);
                if ((int)(new HTuple((new HTuple(hv_LineParameter.TupleLength())).TupleEqual(
                    0))) != 0)
                {
                    HOperatorSet.ClearWindow(hv_WindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
                    HOperatorSet.DispObj(ho_ModelContours, hv_WindowHandle);
                    HOperatorSet.DispObj(ho_LineContours, hv_WindowHandle);
                    set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                    disp_message(hv_WindowHandle, "对齐失败:NG", "image", 1600, 10, "red", "false");
                    hv_Result = 0;
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);(FAI-17);(FAI-18);(FAI-12);(FAI-19);(FAI-14);(FAI-3);(FAI-13);(FAI-11);";
                    //
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                    HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                    HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                    HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_ModelContours, hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_LineContours, hv_DumpWindowHandle);
                    set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                    disp_message(hv_DumpWindowHandle, "对齐失败:NG", "image", 1600, 10, "red",
                        "false");
                    ho_ResultImage.Dispose();
                    HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                    HOperatorSet.CloseWindow(hv_DumpWindowHandle);
                    //
                    ho_ModelContours.Dispose();
                    ho_CircleMaxContour.Dispose();
                    ho_CircleOneContour.Dispose();
                    ho_CircleTwoContour.Dispose();
                    ho_CircleThreeContour.Dispose();
                    ho_RectangleContour.Dispose();
                    ho_DownLineContour.Dispose();
                    ho_UpLineContour.Dispose();
                    ho_LineContour.Dispose();
                    ho_Line2Contour.Dispose();
                    ho_ContourX.Dispose();
                    ho_ContourY.Dispose();
                    ho_LineContours.Dispose();
                    ho_RotateImage.Dispose();
                    ho_CircleMaxContours.Dispose();
                    ho_CircleOneContours.Dispose();
                    ho_CircleTwoContours.Dispose();
                    ho_CircleThreeContours.Dispose();
                    ho_RectangleContours.Dispose();
                    ho_DownLineContours.Dispose();
                    ho_UpLineContours.Dispose();
                    ho_Line2Contours.Dispose();

                    return;
                }
                //
                ho_RotateImage.Dispose();
                AlignImage(ho_Image, out ho_RotateImage, hv_LineParameter, 90, out hv_HomMat2DInvert);
                //
                //开始测量
                HOperatorSet.FindShapeModel(ho_RotateImage, hv_ModelId, (new HTuple(-45)).TupleRad()
                    , (new HTuple(90)).TupleRad(), 0.5, 1, 0.5, "least_squares", (new HTuple(8)).TupleConcat(
                    1), 0.75, out hv_ModelRow, out hv_ModelColumn, out hv_ModelAngle, out hv_ModelScore);
                HOperatorSet.AlignMetrologyModel(hv_MetrologyHandle, hv_ModelRow, hv_ModelColumn,
                    hv_ModelAngle);
                HOperatorSet.ApplyMetrologyModel(ho_RotateImage, hv_MetrologyHandle);
                //
                //大圆
                ho_CircleMaxContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleMaxContours, hv_MetrologyHandle,
                    1, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 1, "all", "result_type",
                    "all_param", out hv_CircleMaxParameter);
                ho_CircleMaxContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleMaxContour, hv_MetrologyHandle,
                    1, "all", 1.5);
                //
                //圆一
                ho_CircleOneContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleOneContours, hv_MetrologyHandle,
                    2, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 2, "all", "result_type",
                    "all_param", out hv_CircleOneParameter);
                ho_CircleOneContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleOneContour, hv_MetrologyHandle,
                    2, "all", 1.5);
                //
                //圆二
                ho_CircleTwoContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleTwoContours, hv_MetrologyHandle,
                    3, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 3, "all", "result_type",
                    "all_param", out hv_CircleTwoParameter);
                ho_CircleTwoContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleTwoContour, hv_MetrologyHandle,
                    3, "all", 1.5);
                //
                //圆三
                ho_CircleThreeContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleThreeContours, hv_MetrologyHandle,
                    4, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 4, "all", "result_type",
                    "all_param", out hv_CircleThreeParameter);
                ho_CircleThreeContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleThreeContour, hv_MetrologyHandle,
                    4, "all", 1.5);
                //
                //矩形
                ho_RectangleContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_RectangleContours, hv_MetrologyHandle,
                    5, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 5, "all", "result_type",
                    "all_param", out hv_RectangleParameter);
                ho_RectangleContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_RectangleContour, hv_MetrologyHandle,
                    5, "all", 1.5);
                //
                //下直线
                ho_DownLineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_DownLineContours, hv_MetrologyHandle,
                    6, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 6, "all", "result_type",
                    "all_param", out hv_DownLineParameter);
                ho_DownLineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_DownLineContour, hv_MetrologyHandle,
                    6, "all", 1.5);
                //
                //上直线
                ho_UpLineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_UpLineContours, hv_MetrologyHandle,
                    7, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 7, "all", "result_type",
                    "all_param", out hv_UpLineParameter);
                ho_UpLineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_UpLineContour, hv_MetrologyHandle,
                    7, "all", 1.5);
                //
                //基准边
                ho_LineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_LineContours, hv_MetrologyHandle,
                    0, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 0, "all", "result_type",
                    "all_param", out hv_LineParameter);
                ho_LineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_LineContour, hv_MetrologyHandle,
                    0, "all", 1.5);

                //竖直边
                ho_Line2Contours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_Line2Contours, hv_MetrologyHandle,
                    8, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 8, "all", "result_type",
                    "all_param", out hv_Line2Parameter);
                ho_Line2Contour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_Line2Contour, hv_MetrologyHandle,
                    8, "all", 1.5);

                //计算大圆直径
                if ((int)(new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    hv_ResultMaxDiameter = (2 * (hv_CircleMaxParameter.TupleSelect(2))) * hv_RatioPixelMax1X;

                    HOperatorSet.DistancePl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_DistanceY);
                    HOperatorSet.DistancePl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_LineParameter.TupleSelect(0), hv_LineParameter.TupleSelect(1),
                        hv_LineParameter.TupleSelect(2), hv_LineParameter.TupleSelect(3), out hv_DistanceX);

                    HOperatorSet.ProjectionPl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_RowProj,
                        out hv_ColProj);
                    ho_ContourY.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourY, ((hv_CircleMaxParameter.TupleSelect(
                        0))).TupleConcat(hv_RowProj), ((hv_CircleMaxParameter.TupleSelect(1))).TupleConcat(
                        hv_ColProj));

                    HOperatorSet.ProjectionPl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_LineParameter.TupleSelect(0), hv_LineParameter.TupleSelect(1),
                        hv_LineParameter.TupleSelect(2), hv_LineParameter.TupleSelect(3), out hv_RowProj,
                        out hv_ColProj);
                    ho_ContourX.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourX, ((hv_CircleMaxParameter.TupleSelect(
                        0))).TupleConcat(hv_RowProj), ((hv_CircleMaxParameter.TupleSelect(1))).TupleConcat(
                        hv_ColProj));

                    hv_ResultMaxX = hv_DistanceX * hv_RatioPixelMaxX;
                    hv_ResultMaxY = hv_DistanceY * hv_RatioPixelMaxY;

                }

                //计算圆一输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleOneParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax1X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleOneParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax1X;
                    hv_ResultMax1Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleOneParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax1Y;

                    hv_Rx = hv_X - hv_ResultMax1X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax1Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax1X1;
                    hv_deltY = hv_Y - hv_ResultMax1Y1;
                    hv_dR = ((2 * (hv_CircleOneParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle1;
                    hv_deltR1 = hv_dR - (hv_Diameter - 0.06);
                    hv_ResultC1 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC1)).TupleAbs();

                    hv_ResultDiameter1 = ((2 * (hv_CircleOneParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle1;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[0] = hv_ResultDiameter1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[1] = hv_ResultC1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[9] = hv_ResultMax1X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[10] = hv_ResultMax1Y1;
                }
                //
                //
                //计算圆二输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleTwoParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax2X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleTwoParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax2X;
                    hv_ResultMax2Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleTwoParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax2Y;

                    hv_Rx = hv_X - hv_ResultMax2X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax2Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax2X1;
                    hv_deltY = hv_Y - hv_ResultMax2Y1;
                    hv_dR = ((2 * (hv_CircleTwoParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle2;
                    hv_deltR2 = hv_dR - (hv_Diameter - 0.06);

                    hv_ResultC2 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC2)).TupleAbs();


                    hv_ResultDiameter2 = ((2 * (hv_CircleTwoParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle2;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[2] = hv_ResultDiameter2;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[3] = hv_ResultC2;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[11] = hv_ResultMax2X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[12] = hv_ResultMax2Y1;

                }
                //
                //
                //计算圆三输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleThreeParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax3X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleThreeParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax3X;
                    hv_ResultMax3Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleThreeParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax3Y;

                    hv_Rx = hv_X - hv_ResultMax3X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax3Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax3X1;
                    hv_deltY = hv_Y - hv_ResultMax3Y1;
                    hv_dR = ((2 * (hv_CircleThreeParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle3;
                    hv_deltR3 = hv_dR - (hv_Diameter - 0.06);
                    hv_ResultC3 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC3)).TupleAbs();

                    hv_ResultDiameter3 = ((2 * (hv_CircleThreeParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle3;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[4] = hv_ResultDiameter3;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[5] = hv_ResultC3;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[13] = hv_ResultMax3X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[14] = hv_ResultMax3Y1;
                }
                //
                //计算矩形输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_RectangleParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {
                    hv_ResultRec = (((((hv_RectangleParameter.TupleSelect(1)) - (hv_CircleMaxParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelRec) + hv_BC_Rec;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[6] = hv_ResultRec;
                }
                //
                //计算直线输出信息
                if ((int)((new HTuple((new HTuple(hv_DownLineParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_UpLineParameter.TupleLength())).TupleGreater(
                    0)))) != 0)
                {
                    //
                    HOperatorSet.DistanceLc(ho_DownLineContour, hv_UpLineParameter.TupleSelect(
                        0), hv_UpLineParameter.TupleSelect(1), hv_UpLineParameter.TupleSelect(
                        2), hv_UpLineParameter.TupleSelect(3), out hv_DistanceMin, out hv_DistanceMax);
                    hv_ResultMaxDistance = hv_DistanceMax * hv_RatioPixel_d;
                    hv_ResultMinDistance = hv_DistanceMin * hv_RatioPixel_d;
                    hv_ResultDistance = ((1.0 * (hv_ResultMaxDistance + hv_ResultMinDistance)) / 2) + hv_BC_Distance;
                    HOperatorSet.AngleLl(hv_DownLineParameter.TupleSelect(0), hv_DownLineParameter.TupleSelect(
                        1), hv_DownLineParameter.TupleSelect(2), hv_DownLineParameter.TupleSelect(
                        3), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_Angle);
                    //angle_ll (DownLineParameter[0], DownLineParameter[1], DownLineParameter[2], DownLineParameter[3], UpLineParameter[0], UpLineParameter[1], UpLineParameter[2], UpLineParameter[3], Angle)
                    HOperatorSet.TupleDeg(hv_Angle, out hv_ResultAngle);
                    HOperatorSet.TupleAbs(hv_ResultAngle, out hv_ResultAngle);
                    hv_ResultAngle = hv_ResultAngle + hv_BC_Angle;
                    //
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[7] = hv_ResultDistance;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[8] = hv_ResultAngle;
                }
                //
                //判断检测结果
                if ((int)((new HTuple((new HTuple((new HTuple((new HTuple((new HTuple((new HTuple(hv_ResultC1.TupleLength()
                    )).TupleGreater(0))).TupleAnd(new HTuple((new HTuple(hv_ResultC2.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultC3.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultRec.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultDistance.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultAngle.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter1.TupleLess(hv_GC_Circle1_0))).TupleOr(
                        new HTuple(hv_ResultDiameter1.TupleGreater(hv_GC_Circle1)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDiameter2.TupleLess(hv_GC_Circle2_0))).TupleOr(
                        new HTuple(hv_ResultDiameter2.TupleGreater(hv_GC_Circle2)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDiameter3.TupleLess(hv_GC_Circle3_0))).TupleOr(
                        new HTuple(hv_ResultDiameter3.TupleGreater(hv_GC_Circle3)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC1.TupleLess(hv_GC_RC1_0))).TupleOr(
                        new HTuple(hv_ResultC1.TupleGreater(hv_GC_RC1 + hv_deltR1)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC2.TupleLess(hv_GC_RC2_0))).TupleOr(
                        new HTuple(hv_ResultC2.TupleGreater(hv_GC_RC2 + hv_deltR2)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC3.TupleLess(hv_GC_RC3_0))).TupleOr(
                        new HTuple(hv_ResultC3.TupleGreater(hv_GC_RC3 + hv_deltR3)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultRec.TupleLess(hv_GC_Rec_0))).TupleOr(
                        new HTuple(hv_ResultRec.TupleGreater(hv_GC_Rec)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDistance.TupleLess(hv_GC_Distance_0))).TupleOr(
                        new HTuple(hv_ResultDistance.TupleGreater(hv_GC_Distance)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultAngle.TupleLess(hv_GC_Angle_0))).TupleOr(
                        new HTuple(hv_ResultAngle.TupleGreater(hv_GC_Angle)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else
                    {
                        hv_Result = 1;
                    }
                }
                else
                {
                    hv_Result = 0;
                }
                //
                //将显示对象添加到结果中
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ModelContours, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleMaxContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleOneContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleTwoContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleThreeContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_RectangleContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_DownLineContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_UpLineContour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_LineContour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_Line2Contour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ContourX, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ContourY, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }

                //显示结果
                HOperatorSet.ClearWindow(hv_WindowHandle);
                HOperatorSet.DispObj(ho_RotateImage, hv_WindowHandle);
                HOperatorSet.DispObj(ho_ResultObj, hv_WindowHandle);

                //
                HOperatorSet.GetImageSize(ho_RotateImage, out hv_Width, out hv_Height);
                HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                HOperatorSet.SetLineWidth(hv_DumpWindowHandle, 5);
                HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                HOperatorSet.DispObj(ho_RotateImage, hv_DumpWindowHandle);
                HOperatorSet.DispObj(ho_ResultObj, hv_DumpWindowHandle);

                //
                //打印检测结果
                set_display_font(hv_WindowHandle, 15, "mono", "true", "false");
                set_display_font(hv_DumpWindowHandle, 120, "mono", "true", "false");
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter1.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter1.TupleLess(hv_GC_Circle1_0))).TupleOr(
                        new HTuple(hv_ResultDiameter1.TupleGreater(hv_GC_Circle1)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-17);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆1直径:测量失败", "image", 50, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆1直径:测量失败", "image", 50, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-17);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter2.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter2.TupleLess(hv_GC_Circle2_0))).TupleOr(
                        new HTuple(hv_ResultDiameter2.TupleGreater(hv_GC_Circle2)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-12);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆2直径:测量失败", "image", 200, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆2直径:测量失败", "image", 200, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-12);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter3.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter3.TupleLess(hv_GC_Circle3_0))).TupleOr(
                        new HTuple(hv_ResultDiameter3.TupleGreater(hv_GC_Circle3)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-14);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆3直径:测量失败", "image", 350, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆3直径:测量失败", "image", 350, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-14);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC1.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC1.TupleLess(hv_GC_RC1_0))).TupleOr(new HTuple(hv_ResultC1.TupleGreater(
                        hv_GC_RC1 + hv_deltR1)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(".3f")),
                            "image", 500, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(
                            ".3f")), "image", 500, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-20);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(".3f")),
                            "image", 500, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(
                            ".3f")), "image", 500, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆1位置度:测量失败", "image", 500, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆1位置度:测量失败", "image", 500, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC2.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC2.TupleLess(hv_GC_RC2_0))).TupleOr(new HTuple(hv_ResultC2.TupleGreater(
                        hv_GC_RC2 + hv_deltR2)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(".3f")),
                            "image", 650, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(
                            ".3f")), "image", 650, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-18);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(".3f")),
                            "image", 650, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(
                            ".3f")), "image", 650, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                    //
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆2位置度:测量失败", "image", 650, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆2位置度:测量失败", "image", 650, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-18);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC3.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC3.TupleLess(hv_GC_RC3_0))).TupleOr(new HTuple(hv_ResultC3.TupleGreater(
                        hv_GC_RC3 + hv_deltR3)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(".3f")),
                            "image", 800, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(
                            ".3f")), "image", 800, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-19);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(".3f")),
                            "image", 800, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(
                            ".3f")), "image", 800, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆3位置度:测量失败", "image", 800, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆3位置度:测量失败", "image", 800, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-19);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultRec.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultRec.TupleLess(hv_GC_Rec_0))).TupleOr(new HTuple(hv_ResultRec.TupleGreater(
                        hv_GC_Rec)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(".3f")),
                            "image", 950, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(
                            ".3f")), "image", 950, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-3);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(".3f")),
                            "image", 950, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(
                            ".3f")), "image", 950, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "插头圆心距:测量失败", "image", 950, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "插头圆心距:测量失败", "image", 950, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-3);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDistance.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDistance.TupleLess(hv_GC_Distance_0))).TupleOr(
                        new HTuple(hv_ResultDistance.TupleGreater(hv_GC_Distance)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-13);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "FPC间距:测量失败", "image", 1100, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "FPC间距:测量失败", "image", 1100, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-13);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultAngle.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultAngle.TupleLess(hv_GC_Angle_0))).TupleOr(new HTuple(hv_ResultAngle.TupleGreater(
                        hv_GC_Angle)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(".3f")),
                            "image", 1250, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(
                            ".3f")), "image", 1250, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-11);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(".3f")),
                            "image", 1250, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(
                            ".3f")), "image", 1250, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "FPC角度:测量失败", "image", 1250, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "FPC角度:测量失败", "image", 1250, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-11);";
                }
                //
                //
                if ((int)(new HTuple((new HTuple(hv_ResultMaxDiameter.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, "大圆X:" + (hv_ResultMaxX.TupleString(".3f")),
                        "image", 1400, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "大圆X:" + (hv_ResultMaxX.TupleString(".3f")),
                        "image", 1400, 10, "green", "false");

                    disp_message(hv_WindowHandle, "大圆Y:" + (hv_ResultMaxY.TupleString(".3f")),
                        "image", 1550, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "大圆Y:" + (hv_ResultMaxY.TupleString(".3f")),
                        "image", 1550, 10, "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter1.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆1X:" + (hv_ResultMax1X.TupleString(".3f"))) + "-") + (hv_ResultMax1X1.TupleString(
                        ".3f")), "image", 1700, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆1X:" + (hv_ResultMax1X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax1X1.TupleString(".3f")), "image", 1700, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆1Y:" + (hv_ResultMax1Y.TupleString(".3f"))) + "-") + (hv_ResultMax1Y1.TupleString(
                        ".3f")), "image", 1850, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆1Y:" + (hv_ResultMax1Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax1Y1.TupleString(".3f")), "image", 1850, 10,
                        "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter2.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆2X:" + (hv_ResultMax2X.TupleString(".3f"))) + "-") + (hv_ResultMax2X1.TupleString(
                        ".3f")), "image", 2000, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆2X:" + (hv_ResultMax2X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax2X1.TupleString(".3f")), "image", 2000, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆2Y:" + (hv_ResultMax2Y.TupleString(".3f"))) + "-") + (hv_ResultMax2Y1.TupleString(
                        ".3f")), "image", 2150, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆2Y:" + (hv_ResultMax2Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax2Y1.TupleString(".3f")), "image", 2150, 10,
                        "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter3.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆3X:" + (hv_ResultMax3X.TupleString(".3f"))) + "-") + (hv_ResultMax3X1.TupleString(
                        ".3f")), "image", 2300, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆3X:" + (hv_ResultMax3X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax3X1.TupleString(".3f")), "image", 2300, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆3Y:" + (hv_ResultMax3Y.TupleString(".3f"))) + "-") + (hv_ResultMax3Y1.TupleString(
                        ".3f")), "image", 2450, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆3Y:" + (hv_ResultMax3Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax3Y1.TupleString(".3f")), "image", 2450, 10,
                        "green", "false");
                }

                //

                set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                //
                if ((int)(new HTuple(hv_Result.TupleEqual(0))) != 0)
                {
                    disp_message(hv_WindowHandle, "产品:NG", "image", 2550, 10, "red", "false");
                    disp_message(hv_DumpWindowHandle, "产品:NG", "image", 2550, 10, "red", "false");
                }
                else
                {
                    disp_message(hv_WindowHandle, "产品:OK", "image", 2550, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "产品:OK", "image", 2550, 10, "green",
                        "false");
                }
                //
                ho_ResultImage.Dispose();
                HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                HOperatorSet.CloseWindow(hv_DumpWindowHandle);

                //
                ho_ModelContours.Dispose();
                ho_CircleMaxContour.Dispose();
                ho_CircleOneContour.Dispose();
                ho_CircleTwoContour.Dispose();
                ho_CircleThreeContour.Dispose();
                ho_RectangleContour.Dispose();
                ho_DownLineContour.Dispose();
                ho_UpLineContour.Dispose();
                ho_LineContour.Dispose();
                ho_Line2Contour.Dispose();
                ho_ContourX.Dispose();
                ho_ContourY.Dispose();
                ho_LineContours.Dispose();
                ho_RotateImage.Dispose();
                ho_CircleMaxContours.Dispose();
                ho_CircleOneContours.Dispose();
                ho_CircleTwoContours.Dispose();
                ho_CircleThreeContours.Dispose();
                ho_RectangleContours.Dispose();
                ho_DownLineContours.Dispose();
                ho_UpLineContours.Dispose();
                ho_Line2Contours.Dispose();

                return;
            }
            catch
            {
                ho_ModelContours.Dispose();
                ho_CircleMaxContour.Dispose();
                ho_CircleOneContour.Dispose();
                ho_CircleTwoContour.Dispose();
                ho_CircleThreeContour.Dispose();
                ho_RectangleContour.Dispose();
                ho_DownLineContour.Dispose();
                ho_UpLineContour.Dispose();
                ho_LineContour.Dispose();
                ho_Line2Contour.Dispose();
                ho_ContourX.Dispose();
                ho_ContourY.Dispose();
                ho_LineContours.Dispose();
                ho_RotateImage.Dispose();
                ho_CircleMaxContours.Dispose();
                ho_CircleOneContours.Dispose();
                ho_CircleTwoContours.Dispose();
                ho_CircleThreeContours.Dispose();
                ho_RectangleContours.Dispose();
                ho_DownLineContours.Dispose();
                ho_UpLineContours.Dispose();
                ho_Line2Contours.Dispose();

                return;
            }
        }
        
        #endregion

        #region 相机2

        public void Cam2_Procedure(HObject ho_Image, out HObject ho_ResultImage, out HObject ho_ResultObj,
      HTuple hv_WindowHandle, HTuple hv_ModelId, HTuple hv_MetrologyHandle, HTuple hv_RatioPixelMaxX,
      HTuple hv_RatioPixelMaxY, HTuple hv_RatioPixelMax1X, HTuple hv_RatioPixelMax1Y,
      HTuple hv_RatioPixelMax2X, HTuple hv_RatioPixelMax2Y, HTuple hv_RatioPixelMax3X,
      HTuple hv_RatioPixelMax3Y, HTuple hv_RatioPixelRec, HTuple hv_RatioPixel_d,
      HTuple hv_X, HTuple hv_Y, HTuple hv_Diameter, HTuple hv_GC_Circle1, HTuple hv_GC_Circle1_0,
      HTuple hv_GC_Circle2, HTuple hv_GC_Circle2_0, HTuple hv_GC_Circle3, HTuple hv_GC_Circle3_0,
      HTuple hv_GC_RC1, HTuple hv_GC_RC1_0, HTuple hv_GC_RC2, HTuple hv_GC_RC2_0,
      HTuple hv_GC_RC3, HTuple hv_GC_RC3_0, HTuple hv_GC_Rec, HTuple hv_GC_Rec_0,
      HTuple hv_GC_Distance, HTuple hv_GC_Distance_0, HTuple hv_GC_Angle, HTuple hv_GC_Angle_0,
      HTuple hv_BC_Circle1, HTuple hv_BC_Circle2, HTuple hv_BC_Circle3, HTuple hv_BC_RC1,
      HTuple hv_BC_RC2, HTuple hv_BC_RC3, HTuple hv_BC_Rec, HTuple hv_BC_Distance,
      HTuple hv_BC_Angle, HTuple hv_BC_Min, HTuple hv_BC_Max, out HTuple hv_ResultValue,
      out HTuple hv_ResultInfo, out HTuple hv_Result)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_ModelContours, ho_CircleMaxContour;
            HObject ho_CircleOneContour, ho_CircleTwoContour, ho_CircleThreeContour;
            HObject ho_RectangleContour, ho_DownLineContour, ho_UpLineContour;
            HObject ho_LineContour, ho_Line2Contour, ho_ContourX, ho_ContourY;
            HObject ho_LineContours, ho_RotateImage, ho_CircleMaxContours;
            HObject ho_CircleOneContours, ho_CircleTwoContours, ho_CircleThreeContours;
            HObject ho_RectangleContours, ho_DownLineContours, ho_UpLineContours;
            HObject ho_Line2Contours;

            // Local control variables 

            HTuple hv_ModelRow = null, hv_ModelColumn = null;
            HTuple hv_ModelAngle = null, hv_LineParameter = null, hv_CircleMaxParameter = null;
            HTuple hv_CircleOneParameter = null, hv_CircleTwoParameter = null;
            HTuple hv_CircleThreeParameter = null, hv_Line2Parameter = null;
            HTuple hv_ResultC1 = null, hv_ResultDiameter1 = null, hv_ResultC2 = null;
            HTuple hv_ResultDiameter2 = null, hv_ResultC3 = null, hv_ResultDiameter3 = null;
            HTuple hv_ResultRec = null, hv_ResultDistance = null, hv_ResultAngle = null;
            HTuple hv_ResultMaxDiameter = null, hv_ResultMax1X = null;
            HTuple hv_ResultMax1Y = null, hv_ResultMax2X = null, hv_ResultMax2Y = null;
            HTuple hv_ResultMax3X = null, hv_ResultMax3Y = null, hv_ResultMax1X1 = null;
            HTuple hv_ResultMax1Y1 = null, hv_ResultMax2X1 = null;
            HTuple hv_ResultMax2Y1 = null, hv_ResultMax3X1 = null;
            HTuple hv_ResultMax3Y1 = null, hv_ModelScore = null, hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_DumpWindowHandle = new HTuple();
            HTuple hv_HomMat2D = null, hv_Row6 = null, hv_Column6 = null;
            HTuple hv_HomMat2DInvert = null, hv_RectangleParameter = null;
            HTuple hv_DownLineParameter = null, hv_UpLineParameter = null;
            HTuple hv_DistanceY = new HTuple(), hv_DistanceX = new HTuple();
            HTuple hv_RowProj = new HTuple(), hv_ColProj = new HTuple();
            HTuple hv_ResultMaxX = new HTuple(), hv_ResultMaxY = new HTuple();
            HTuple hv_Rx = new HTuple(), hv_Ry = new HTuple(), hv_deltX = new HTuple();
            HTuple hv_deltY = new HTuple(), hv_dR = new HTuple(), hv_deltR1 = new HTuple();
            HTuple hv_deltR2 = new HTuple(), hv_deltR3 = new HTuple();
            HTuple hv_DistanceMin = new HTuple(), hv_DistanceMax = new HTuple();
            HTuple hv_ResultMaxDistance = new HTuple(), hv_ResultMinDistance = new HTuple();
            HTuple hv_Angle = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ResultImage);
            HOperatorSet.GenEmptyObj(out ho_ResultObj);
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContour);
            HOperatorSet.GenEmptyObj(out ho_CircleOneContour);
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContour);
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContour);
            HOperatorSet.GenEmptyObj(out ho_RectangleContour);
            HOperatorSet.GenEmptyObj(out ho_DownLineContour);
            HOperatorSet.GenEmptyObj(out ho_UpLineContour);
            HOperatorSet.GenEmptyObj(out ho_LineContour);
            HOperatorSet.GenEmptyObj(out ho_Line2Contour);
            HOperatorSet.GenEmptyObj(out ho_ContourX);
            HOperatorSet.GenEmptyObj(out ho_ContourY);
            HOperatorSet.GenEmptyObj(out ho_LineContours);
            HOperatorSet.GenEmptyObj(out ho_RotateImage);
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContours);
            HOperatorSet.GenEmptyObj(out ho_CircleOneContours);
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContours);
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContours);
            HOperatorSet.GenEmptyObj(out ho_RectangleContours);
            HOperatorSet.GenEmptyObj(out ho_DownLineContours);
            HOperatorSet.GenEmptyObj(out ho_UpLineContours);
            HOperatorSet.GenEmptyObj(out ho_Line2Contours);

            //初始化输出变量
            ho_ResultImage.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ResultImage);
            ho_ResultObj.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ResultObj);
            hv_ResultValue = new HTuple();
            hv_ResultValue[0] = -100;
            hv_ResultValue[1] = -100;
            hv_ResultValue[2] = -100;
            hv_ResultValue[3] = -100;
            hv_ResultValue[4] = -100;
            hv_ResultValue[5] = -100;
            hv_ResultValue[6] = -100;
            hv_ResultValue[7] = -100;
            hv_ResultValue[8] = -100;
            hv_ResultValue[9] = -100;
            hv_ResultValue[10] = -100;
            hv_ResultValue[11] = -100;
            hv_ResultValue[12] = -100;
            hv_ResultValue[13] = -100;
            hv_ResultValue[14] = -100;
            hv_ResultInfo = "";
            hv_Result = 0;

            //初始化中间变量
            hv_ModelRow = new HTuple();
            hv_ModelColumn = new HTuple();
            hv_ModelAngle = new HTuple();
            hv_LineParameter = new HTuple();
            hv_CircleMaxParameter = new HTuple();
            hv_CircleOneParameter = new HTuple();
            hv_CircleTwoParameter = new HTuple();
            hv_CircleThreeParameter = new HTuple();
            hv_Line2Parameter = new HTuple();
            hv_ResultC1 = new HTuple();
            hv_ResultDiameter1 = new HTuple();
            hv_ResultC2 = new HTuple();
            hv_ResultDiameter2 = new HTuple();
            hv_ResultC3 = new HTuple();
            hv_ResultDiameter3 = new HTuple();
            hv_ResultRec = new HTuple();
            hv_ResultDistance = new HTuple();
            hv_ResultAngle = new HTuple();
            hv_ResultMaxDiameter = new HTuple();
            hv_ResultMax1X = new HTuple();
            hv_ResultMax1Y = new HTuple();
            hv_ResultMax2X = new HTuple();
            hv_ResultMax2Y = new HTuple();
            hv_ResultMax3X = new HTuple();
            hv_ResultMax3Y = new HTuple();
            hv_ResultMax1X1 = new HTuple();
            hv_ResultMax1Y1 = new HTuple();
            hv_ResultMax2X1 = new HTuple();
            hv_ResultMax2Y1 = new HTuple();
            hv_ResultMax3X1 = new HTuple();
            hv_ResultMax3Y1 = new HTuple();
            ho_ModelContours.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            ho_CircleMaxContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContour);
            ho_CircleOneContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleOneContour);
            ho_CircleTwoContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContour);
            ho_CircleThreeContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContour);
            ho_RectangleContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_RectangleContour);
            ho_DownLineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_DownLineContour);
            ho_UpLineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_UpLineContour);
            ho_LineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_LineContour);
            ho_Line2Contour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Line2Contour);
            ho_ContourX.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ContourX);
            ho_ContourY.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ContourY);

            try
            {

                //旋转矫正图像
                HOperatorSet.FindShapeModel(ho_Image, hv_ModelId, (new HTuple(-45)).TupleRad()
                    , (new HTuple(90)).TupleRad(), 0.5, 1, 0.5, "least_squares", (new HTuple(8)).TupleConcat(
                    1), 0.75, out hv_ModelRow, out hv_ModelColumn, out hv_ModelAngle, out hv_ModelScore);
                if ((int)(new HTuple((new HTuple(hv_ModelRow.TupleLength())).TupleEqual(0))) != 0)
                {
                    HOperatorSet.ClearWindow(hv_WindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
                    set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                    disp_message(hv_WindowHandle, "定位失败:NG", "image", 1600, 10, "red", "false");
                    hv_Result = 0;
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);(FAI-17);(FAI-18);(FAI-12);(FAI-19);(FAI-14);(FAI-3);(FAI-13);(FAI-11);";
                    //
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                    HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                    HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                    HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_DumpWindowHandle);
                    set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                    disp_message(hv_DumpWindowHandle, "定位失败:NG", "image", 1600, 10, "red",
                        "false");
                    ho_ResultImage.Dispose();
                    HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                    HOperatorSet.CloseWindow(hv_DumpWindowHandle);
                    //
                    ho_ModelContours.Dispose();
                    ho_CircleMaxContour.Dispose();
                    ho_CircleOneContour.Dispose();
                    ho_CircleTwoContour.Dispose();
                    ho_CircleThreeContour.Dispose();
                    ho_RectangleContour.Dispose();
                    ho_DownLineContour.Dispose();
                    ho_UpLineContour.Dispose();
                    ho_LineContour.Dispose();
                    ho_Line2Contour.Dispose();
                    ho_ContourX.Dispose();
                    ho_ContourY.Dispose();
                    ho_LineContours.Dispose();
                    ho_RotateImage.Dispose();
                    ho_CircleMaxContours.Dispose();
                    ho_CircleOneContours.Dispose();
                    ho_CircleTwoContours.Dispose();
                    ho_CircleThreeContours.Dispose();
                    ho_RectangleContours.Dispose();
                    ho_DownLineContours.Dispose();
                    ho_UpLineContours.Dispose();
                    ho_Line2Contours.Dispose();

                    return;
                }
                //
                //显示模板
                ho_ModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelId, 1);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_ModelRow, hv_ModelColumn, hv_ModelAngle,
                    out hv_HomMat2D);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.AffineTransContourXld(ho_ModelContours, out ExpTmpOutVar_0, hv_HomMat2D);
                    ho_ModelContours.Dispose();
                    ho_ModelContours = ExpTmpOutVar_0;
                }
                //
                //旋转图像
                HOperatorSet.AlignMetrologyModel(hv_MetrologyHandle, hv_ModelRow, hv_ModelColumn,
                    hv_ModelAngle);
                HOperatorSet.ApplyMetrologyModel(ho_Image, hv_MetrologyHandle);
                ho_LineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_LineContours, hv_MetrologyHandle,
                    0, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 0, "all", "result_type",
                    "all_param", out hv_LineParameter);
                ho_LineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_LineContour, hv_MetrologyHandle,
                    0, "all", 1.5);
                if ((int)(new HTuple((new HTuple(hv_LineParameter.TupleLength())).TupleEqual(
                    0))) != 0)
                {
                    HOperatorSet.ClearWindow(hv_WindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
                    HOperatorSet.DispObj(ho_ModelContours, hv_WindowHandle);
                    HOperatorSet.DispObj(ho_LineContours, hv_WindowHandle);
                    set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                    disp_message(hv_WindowHandle, "对齐失败:NG", "image", 1600, 10, "red", "false");
                    hv_Result = 0;
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);(FAI-17);(FAI-18);(FAI-12);(FAI-19);(FAI-14);(FAI-3);(FAI-13);(FAI-11);";
                    //
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                    HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                    HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                    HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_ModelContours, hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_LineContours, hv_DumpWindowHandle);
                    set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                    disp_message(hv_DumpWindowHandle, "对齐失败:NG", "image", 1600, 10, "red",
                        "false");
                    ho_ResultImage.Dispose();
                    HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                    HOperatorSet.CloseWindow(hv_DumpWindowHandle);
                    //
                    ho_ModelContours.Dispose();
                    ho_CircleMaxContour.Dispose();
                    ho_CircleOneContour.Dispose();
                    ho_CircleTwoContour.Dispose();
                    ho_CircleThreeContour.Dispose();
                    ho_RectangleContour.Dispose();
                    ho_DownLineContour.Dispose();
                    ho_UpLineContour.Dispose();
                    ho_LineContour.Dispose();
                    ho_Line2Contour.Dispose();
                    ho_ContourX.Dispose();
                    ho_ContourY.Dispose();
                    ho_LineContours.Dispose();
                    ho_RotateImage.Dispose();
                    ho_CircleMaxContours.Dispose();
                    ho_CircleOneContours.Dispose();
                    ho_CircleTwoContours.Dispose();
                    ho_CircleThreeContours.Dispose();
                    ho_RectangleContours.Dispose();
                    ho_DownLineContours.Dispose();
                    ho_UpLineContours.Dispose();
                    ho_Line2Contours.Dispose();

                    return;
                }
                //
                ho_RotateImage.Dispose();
                AlignImage(ho_Image, out ho_RotateImage, hv_LineParameter, 90, out hv_HomMat2DInvert);
                //
                //开始测量
                HOperatorSet.FindShapeModel(ho_RotateImage, hv_ModelId, (new HTuple(-45)).TupleRad()
                    , (new HTuple(90)).TupleRad(), 0.5, 1, 0.5, "least_squares", (new HTuple(8)).TupleConcat(
                    1), 0.75, out hv_ModelRow, out hv_ModelColumn, out hv_ModelAngle, out hv_ModelScore);
                HOperatorSet.AlignMetrologyModel(hv_MetrologyHandle, hv_ModelRow, hv_ModelColumn,
                    hv_ModelAngle);
                HOperatorSet.ApplyMetrologyModel(ho_RotateImage, hv_MetrologyHandle);
                //
                //大圆
                ho_CircleMaxContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleMaxContours, hv_MetrologyHandle,
                    1, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 1, "all", "result_type",
                    "all_param", out hv_CircleMaxParameter);
                ho_CircleMaxContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleMaxContour, hv_MetrologyHandle,
                    1, "all", 1.5);
                //
                //圆一
                ho_CircleOneContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleOneContours, hv_MetrologyHandle,
                    2, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 2, "all", "result_type",
                    "all_param", out hv_CircleOneParameter);
                ho_CircleOneContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleOneContour, hv_MetrologyHandle,
                    2, "all", 1.5);
                //
                //圆二
                ho_CircleTwoContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleTwoContours, hv_MetrologyHandle,
                    3, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 3, "all", "result_type",
                    "all_param", out hv_CircleTwoParameter);
                ho_CircleTwoContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleTwoContour, hv_MetrologyHandle,
                    3, "all", 1.5);
                //
                //圆三
                ho_CircleThreeContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleThreeContours, hv_MetrologyHandle,
                    4, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 4, "all", "result_type",
                    "all_param", out hv_CircleThreeParameter);
                ho_CircleThreeContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleThreeContour, hv_MetrologyHandle,
                    4, "all", 1.5);
                //
                //矩形
                ho_RectangleContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_RectangleContours, hv_MetrologyHandle,
                    5, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 5, "all", "result_type",
                    "all_param", out hv_RectangleParameter);
                ho_RectangleContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_RectangleContour, hv_MetrologyHandle,
                    5, "all", 1.5);
                //
                //下直线
                ho_DownLineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_DownLineContours, hv_MetrologyHandle,
                    6, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 6, "all", "result_type",
                    "all_param", out hv_DownLineParameter);
                ho_DownLineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_DownLineContour, hv_MetrologyHandle,
                    6, "all", 1.5);
                //
                //上直线
                ho_UpLineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_UpLineContours, hv_MetrologyHandle,
                    7, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 7, "all", "result_type",
                    "all_param", out hv_UpLineParameter);
                ho_UpLineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_UpLineContour, hv_MetrologyHandle,
                    7, "all", 1.5);
                //
                //基准边
                ho_LineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_LineContours, hv_MetrologyHandle,
                    0, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 0, "all", "result_type",
                    "all_param", out hv_LineParameter);
                ho_LineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_LineContour, hv_MetrologyHandle,
                    0, "all", 1.5);

                //竖直边
                ho_Line2Contours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_Line2Contours, hv_MetrologyHandle,
                    8, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 8, "all", "result_type",
                    "all_param", out hv_Line2Parameter);
                ho_Line2Contour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_Line2Contour, hv_MetrologyHandle,
                    8, "all", 1.5);

                //计算大圆直径
                if ((int)(new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    hv_ResultMaxDiameter = (2 * (hv_CircleMaxParameter.TupleSelect(2))) * hv_RatioPixelMax1X;

                    HOperatorSet.DistancePl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_DistanceY);
                    HOperatorSet.DistancePl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_LineParameter.TupleSelect(0), hv_LineParameter.TupleSelect(1),
                        hv_LineParameter.TupleSelect(2), hv_LineParameter.TupleSelect(3), out hv_DistanceX);

                    HOperatorSet.ProjectionPl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_RowProj,
                        out hv_ColProj);
                    ho_ContourY.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourY, ((hv_CircleMaxParameter.TupleSelect(
                        0))).TupleConcat(hv_RowProj), ((hv_CircleMaxParameter.TupleSelect(1))).TupleConcat(
                        hv_ColProj));

                    HOperatorSet.ProjectionPl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_LineParameter.TupleSelect(0), hv_LineParameter.TupleSelect(1),
                        hv_LineParameter.TupleSelect(2), hv_LineParameter.TupleSelect(3), out hv_RowProj,
                        out hv_ColProj);
                    ho_ContourX.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourX, ((hv_CircleMaxParameter.TupleSelect(
                        0))).TupleConcat(hv_RowProj), ((hv_CircleMaxParameter.TupleSelect(1))).TupleConcat(
                        hv_ColProj));

                    hv_ResultMaxX = hv_DistanceX * hv_RatioPixelMaxX;
                    hv_ResultMaxY = hv_DistanceY * hv_RatioPixelMaxY;

                }

                //计算圆一输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleOneParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax1X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleOneParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax1X;
                    hv_ResultMax1Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleOneParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax1Y;

                    hv_Rx = hv_X - hv_ResultMax1X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax1Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax1X1;
                    hv_deltY = hv_Y - hv_ResultMax1Y1;
                    hv_dR = ((2 * (hv_CircleOneParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle1;
                    hv_deltR1 = hv_dR - (hv_Diameter - 0.06);
                    hv_ResultC1 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC1)).TupleAbs();

                    hv_ResultDiameter1 = ((2 * (hv_CircleOneParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle1;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[0] = hv_ResultDiameter1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[1] = hv_ResultC1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[9] = hv_ResultMax1X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[10] = hv_ResultMax1Y1;
                }
                //
                //
                //计算圆二输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleTwoParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax2X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleTwoParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax2X;
                    hv_ResultMax2Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleTwoParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax2Y;

                    hv_Rx = hv_X - hv_ResultMax2X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax2Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax2X1;
                    hv_deltY = hv_Y - hv_ResultMax2Y1;
                    hv_dR = ((2 * (hv_CircleTwoParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle2;
                    hv_deltR2 = hv_dR - (hv_Diameter - 0.06);

                    hv_ResultC2 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC2)).TupleAbs();


                    hv_ResultDiameter2 = ((2 * (hv_CircleTwoParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle2;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[2] = hv_ResultDiameter2;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[3] = hv_ResultC2;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[11] = hv_ResultMax2X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[12] = hv_ResultMax2Y1;

                }
                //
                //
                //计算圆三输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleThreeParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax3X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleThreeParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax3X;
                    hv_ResultMax3Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleThreeParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax3Y;

                    hv_Rx = hv_X - hv_ResultMax3X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax3Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax3X1;
                    hv_deltY = hv_Y - hv_ResultMax3Y1;
                    hv_dR = ((2 * (hv_CircleThreeParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle3;
                    hv_deltR3 = hv_dR - (hv_Diameter - 0.06);
                    hv_ResultC3 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC3)).TupleAbs();

                    hv_ResultDiameter3 = ((2 * (hv_CircleThreeParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle3;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[4] = hv_ResultDiameter3;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[5] = hv_ResultC3;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[13] = hv_ResultMax3X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[14] = hv_ResultMax3Y1;
                }
                //
                //计算矩形输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_RectangleParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {
                    hv_ResultRec = (((((hv_RectangleParameter.TupleSelect(1)) - (hv_CircleMaxParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelRec) + hv_BC_Rec;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[6] = hv_ResultRec;
                }
                //
                //计算直线输出信息
                if ((int)((new HTuple((new HTuple(hv_DownLineParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_UpLineParameter.TupleLength())).TupleGreater(
                    0)))) != 0)
                {
                    //
                    HOperatorSet.DistanceLc(ho_DownLineContour, hv_UpLineParameter.TupleSelect(
                        0), hv_UpLineParameter.TupleSelect(1), hv_UpLineParameter.TupleSelect(
                        2), hv_UpLineParameter.TupleSelect(3), out hv_DistanceMin, out hv_DistanceMax);
                    hv_ResultMaxDistance = hv_DistanceMax * hv_RatioPixel_d;
                    hv_ResultMinDistance = hv_DistanceMin * hv_RatioPixel_d;
                    hv_ResultDistance = ((1.0 * (hv_ResultMaxDistance + hv_ResultMinDistance)) / 2) + hv_BC_Distance;
                    HOperatorSet.AngleLl(hv_DownLineParameter.TupleSelect(0), hv_DownLineParameter.TupleSelect(
                        1), hv_DownLineParameter.TupleSelect(2), hv_DownLineParameter.TupleSelect(
                        3), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_Angle);
                    //angle_ll (DownLineParameter[0], DownLineParameter[1], DownLineParameter[2], DownLineParameter[3], UpLineParameter[0], UpLineParameter[1], UpLineParameter[2], UpLineParameter[3], Angle)
                    HOperatorSet.TupleDeg(hv_Angle, out hv_ResultAngle);
                    HOperatorSet.TupleAbs(hv_ResultAngle, out hv_ResultAngle);
                    hv_ResultAngle = hv_ResultAngle + hv_BC_Angle;
                    //
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[7] = hv_ResultDistance;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[8] = hv_ResultAngle;
                }
                //
                //判断检测结果
                if ((int)((new HTuple((new HTuple((new HTuple((new HTuple((new HTuple((new HTuple(hv_ResultC1.TupleLength()
                    )).TupleGreater(0))).TupleAnd(new HTuple((new HTuple(hv_ResultC2.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultC3.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultRec.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultDistance.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultAngle.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter1.TupleLess(hv_GC_Circle1_0))).TupleOr(
                        new HTuple(hv_ResultDiameter1.TupleGreater(hv_GC_Circle1)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDiameter2.TupleLess(hv_GC_Circle2_0))).TupleOr(
                        new HTuple(hv_ResultDiameter2.TupleGreater(hv_GC_Circle2)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDiameter3.TupleLess(hv_GC_Circle3_0))).TupleOr(
                        new HTuple(hv_ResultDiameter3.TupleGreater(hv_GC_Circle3)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC1.TupleLess(hv_GC_RC1_0))).TupleOr(
                        new HTuple(hv_ResultC1.TupleGreater(hv_GC_RC1 + hv_deltR1)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC2.TupleLess(hv_GC_RC2_0))).TupleOr(
                        new HTuple(hv_ResultC2.TupleGreater(hv_GC_RC2 + hv_deltR2)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC3.TupleLess(hv_GC_RC3_0))).TupleOr(
                        new HTuple(hv_ResultC3.TupleGreater(hv_GC_RC3 + hv_deltR3)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultRec.TupleLess(hv_GC_Rec_0))).TupleOr(
                        new HTuple(hv_ResultRec.TupleGreater(hv_GC_Rec)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDistance.TupleLess(hv_GC_Distance_0))).TupleOr(
                        new HTuple(hv_ResultDistance.TupleGreater(hv_GC_Distance)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultAngle.TupleLess(hv_GC_Angle_0))).TupleOr(
                        new HTuple(hv_ResultAngle.TupleGreater(hv_GC_Angle)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else
                    {
                        hv_Result = 1;
                    }
                }
                else
                {
                    hv_Result = 0;
                }
                //
                //将显示对象添加到结果中
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ModelContours, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleMaxContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleOneContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleTwoContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleThreeContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_RectangleContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_DownLineContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_UpLineContour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_LineContour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_Line2Contour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ContourX, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ContourY, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }

                //显示结果
                HOperatorSet.ClearWindow(hv_WindowHandle);
                HOperatorSet.DispObj(ho_RotateImage, hv_WindowHandle);
                HOperatorSet.DispObj(ho_ResultObj, hv_WindowHandle);

                //
                HOperatorSet.GetImageSize(ho_RotateImage, out hv_Width, out hv_Height);
                HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                HOperatorSet.SetLineWidth(hv_DumpWindowHandle, 5);
                HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                HOperatorSet.DispObj(ho_RotateImage, hv_DumpWindowHandle);
                HOperatorSet.DispObj(ho_ResultObj, hv_DumpWindowHandle);

                //
                //打印检测结果
                set_display_font(hv_WindowHandle, 15, "mono", "true", "false");
                set_display_font(hv_DumpWindowHandle, 120, "mono", "true", "false");
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter1.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter1.TupleLess(hv_GC_Circle1_0))).TupleOr(
                        new HTuple(hv_ResultDiameter1.TupleGreater(hv_GC_Circle1)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-17);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆1直径:测量失败", "image", 50, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆1直径:测量失败", "image", 50, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-17);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter2.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter2.TupleLess(hv_GC_Circle2_0))).TupleOr(
                        new HTuple(hv_ResultDiameter2.TupleGreater(hv_GC_Circle2)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-12);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆2直径:测量失败", "image", 200, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆2直径:测量失败", "image", 200, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-12);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter3.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter3.TupleLess(hv_GC_Circle3_0))).TupleOr(
                        new HTuple(hv_ResultDiameter3.TupleGreater(hv_GC_Circle3)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-14);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆3直径:测量失败", "image", 350, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆3直径:测量失败", "image", 350, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-14);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC1.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC1.TupleLess(hv_GC_RC1_0))).TupleOr(new HTuple(hv_ResultC1.TupleGreater(
                        hv_GC_RC1 + hv_deltR1)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(".3f")),
                            "image", 500, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(
                            ".3f")), "image", 500, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-20);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(".3f")),
                            "image", 500, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(
                            ".3f")), "image", 500, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆1位置度:测量失败", "image", 500, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆1位置度:测量失败", "image", 500, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC2.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC2.TupleLess(hv_GC_RC2_0))).TupleOr(new HTuple(hv_ResultC2.TupleGreater(
                        hv_GC_RC2 + hv_deltR2)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(".3f")),
                            "image", 650, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(
                            ".3f")), "image", 650, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-18);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(".3f")),
                            "image", 650, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(
                            ".3f")), "image", 650, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                    //
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆2位置度:测量失败", "image", 650, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆2位置度:测量失败", "image", 650, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-18);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC3.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC3.TupleLess(hv_GC_RC3_0))).TupleOr(new HTuple(hv_ResultC3.TupleGreater(
                        hv_GC_RC3 + hv_deltR3)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(".3f")),
                            "image", 800, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(
                            ".3f")), "image", 800, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-19);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(".3f")),
                            "image", 800, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(
                            ".3f")), "image", 800, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆3位置度:测量失败", "image", 800, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆3位置度:测量失败", "image", 800, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-19);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultRec.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultRec.TupleLess(hv_GC_Rec_0))).TupleOr(new HTuple(hv_ResultRec.TupleGreater(
                        hv_GC_Rec)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(".3f")),
                            "image", 950, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(
                            ".3f")), "image", 950, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-3);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(".3f")),
                            "image", 950, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(
                            ".3f")), "image", 950, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "插头圆心距:测量失败", "image", 950, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "插头圆心距:测量失败", "image", 950, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-3);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDistance.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDistance.TupleLess(hv_GC_Distance_0))).TupleOr(
                        new HTuple(hv_ResultDistance.TupleGreater(hv_GC_Distance)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-13);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "FPC间距:测量失败", "image", 1100, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "FPC间距:测量失败", "image", 1100, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-13);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultAngle.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultAngle.TupleLess(hv_GC_Angle_0))).TupleOr(new HTuple(hv_ResultAngle.TupleGreater(
                        hv_GC_Angle)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(".3f")),
                            "image", 1250, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(
                            ".3f")), "image", 1250, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-11);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(".3f")),
                            "image", 1250, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(
                            ".3f")), "image", 1250, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "FPC角度:测量失败", "image", 1250, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "FPC角度:测量失败", "image", 1250, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-11);";
                }
                //
                //
                if ((int)(new HTuple((new HTuple(hv_ResultMaxDiameter.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, "大圆X:" + (hv_ResultMaxX.TupleString(".3f")),
                        "image", 1400, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "大圆X:" + (hv_ResultMaxX.TupleString(".3f")),
                        "image", 1400, 10, "green", "false");

                    disp_message(hv_WindowHandle, "大圆Y:" + (hv_ResultMaxY.TupleString(".3f")),
                        "image", 1550, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "大圆Y:" + (hv_ResultMaxY.TupleString(".3f")),
                        "image", 1550, 10, "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter1.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆1X:" + (hv_ResultMax1X.TupleString(".3f"))) + "-") + (hv_ResultMax1X1.TupleString(
                        ".3f")), "image", 1700, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆1X:" + (hv_ResultMax1X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax1X1.TupleString(".3f")), "image", 1700, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆1Y:" + (hv_ResultMax1Y.TupleString(".3f"))) + "-") + (hv_ResultMax1Y1.TupleString(
                        ".3f")), "image", 1850, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆1Y:" + (hv_ResultMax1Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax1Y1.TupleString(".3f")), "image", 1850, 10,
                        "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter2.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆2X:" + (hv_ResultMax2X.TupleString(".3f"))) + "-") + (hv_ResultMax2X1.TupleString(
                        ".3f")), "image", 2000, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆2X:" + (hv_ResultMax2X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax2X1.TupleString(".3f")), "image", 2000, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆2Y:" + (hv_ResultMax2Y.TupleString(".3f"))) + "-") + (hv_ResultMax2Y1.TupleString(
                        ".3f")), "image", 2150, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆2Y:" + (hv_ResultMax2Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax2Y1.TupleString(".3f")), "image", 2150, 10,
                        "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter3.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆3X:" + (hv_ResultMax3X.TupleString(".3f"))) + "-") + (hv_ResultMax3X1.TupleString(
                        ".3f")), "image", 2300, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆3X:" + (hv_ResultMax3X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax3X1.TupleString(".3f")), "image", 2300, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆3Y:" + (hv_ResultMax3Y.TupleString(".3f"))) + "-") + (hv_ResultMax3Y1.TupleString(
                        ".3f")), "image", 2450, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆3Y:" + (hv_ResultMax3Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax3Y1.TupleString(".3f")), "image", 2450, 10,
                        "green", "false");
                }

                //

                set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                //
                if ((int)(new HTuple(hv_Result.TupleEqual(0))) != 0)
                {
                    disp_message(hv_WindowHandle, "产品:NG", "image", 2550, 10, "red", "false");
                    disp_message(hv_DumpWindowHandle, "产品:NG", "image", 2550, 10, "red", "false");
                }
                else
                {
                    disp_message(hv_WindowHandle, "产品:OK", "image", 2550, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "产品:OK", "image", 2550, 10, "green",
                        "false");
                }
                //
                ho_ResultImage.Dispose();
                HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                HOperatorSet.CloseWindow(hv_DumpWindowHandle);

                //
                ho_ModelContours.Dispose();
                ho_CircleMaxContour.Dispose();
                ho_CircleOneContour.Dispose();
                ho_CircleTwoContour.Dispose();
                ho_CircleThreeContour.Dispose();
                ho_RectangleContour.Dispose();
                ho_DownLineContour.Dispose();
                ho_UpLineContour.Dispose();
                ho_LineContour.Dispose();
                ho_Line2Contour.Dispose();
                ho_ContourX.Dispose();
                ho_ContourY.Dispose();
                ho_LineContours.Dispose();
                ho_RotateImage.Dispose();
                ho_CircleMaxContours.Dispose();
                ho_CircleOneContours.Dispose();
                ho_CircleTwoContours.Dispose();
                ho_CircleThreeContours.Dispose();
                ho_RectangleContours.Dispose();
                ho_DownLineContours.Dispose();
                ho_UpLineContours.Dispose();
                ho_Line2Contours.Dispose();

                return;
            }
            catch
            {
                ho_ModelContours.Dispose();
                ho_CircleMaxContour.Dispose();
                ho_CircleOneContour.Dispose();
                ho_CircleTwoContour.Dispose();
                ho_CircleThreeContour.Dispose();
                ho_RectangleContour.Dispose();
                ho_DownLineContour.Dispose();
                ho_UpLineContour.Dispose();
                ho_LineContour.Dispose();
                ho_Line2Contour.Dispose();
                ho_ContourX.Dispose();
                ho_ContourY.Dispose();
                ho_LineContours.Dispose();
                ho_RotateImage.Dispose();
                ho_CircleMaxContours.Dispose();
                ho_CircleOneContours.Dispose();
                ho_CircleTwoContours.Dispose();
                ho_CircleThreeContours.Dispose();
                ho_RectangleContours.Dispose();
                ho_DownLineContours.Dispose();
                ho_UpLineContours.Dispose();
                ho_Line2Contours.Dispose();

                return;
            }
        }

        #endregion

        #region 相机3

        public void Cam3_Procedure(HObject ho_Image, out HObject ho_ResultImage, out HObject ho_ResultObj,
      HTuple hv_WindowHandle, HTuple hv_ModelId, HTuple hv_MetrologyHandle, HTuple hv_RatioPixelMaxX,
      HTuple hv_RatioPixelMaxY, HTuple hv_RatioPixelMax1X, HTuple hv_RatioPixelMax1Y,
      HTuple hv_RatioPixelMax2X, HTuple hv_RatioPixelMax2Y, HTuple hv_RatioPixelMax3X,
      HTuple hv_RatioPixelMax3Y, HTuple hv_RatioPixelRec, HTuple hv_RatioPixel_d,
      HTuple hv_X, HTuple hv_Y, HTuple hv_Diameter, HTuple hv_GC_Circle1, HTuple hv_GC_Circle1_0,
      HTuple hv_GC_Circle2, HTuple hv_GC_Circle2_0, HTuple hv_GC_Circle3, HTuple hv_GC_Circle3_0,
      HTuple hv_GC_RC1, HTuple hv_GC_RC1_0, HTuple hv_GC_RC2, HTuple hv_GC_RC2_0,
      HTuple hv_GC_RC3, HTuple hv_GC_RC3_0, HTuple hv_GC_Rec, HTuple hv_GC_Rec_0,
      HTuple hv_GC_Distance, HTuple hv_GC_Distance_0, HTuple hv_GC_Angle, HTuple hv_GC_Angle_0,
      HTuple hv_BC_Circle1, HTuple hv_BC_Circle2, HTuple hv_BC_Circle3, HTuple hv_BC_RC1,
      HTuple hv_BC_RC2, HTuple hv_BC_RC3, HTuple hv_BC_Rec, HTuple hv_BC_Distance,
      HTuple hv_BC_Angle, HTuple hv_BC_Min, HTuple hv_BC_Max, out HTuple hv_ResultValue,
      out HTuple hv_ResultInfo, out HTuple hv_Result)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_ModelContours, ho_CircleMaxContour;
            HObject ho_CircleOneContour, ho_CircleTwoContour, ho_CircleThreeContour;
            HObject ho_RectangleContour, ho_DownLineContour, ho_UpLineContour;
            HObject ho_LineContour, ho_Line2Contour, ho_ContourX, ho_ContourY;
            HObject ho_LineContours, ho_RotateImage, ho_CircleMaxContours;
            HObject ho_CircleOneContours, ho_CircleTwoContours, ho_CircleThreeContours;
            HObject ho_RectangleContours, ho_DownLineContours, ho_UpLineContours;
            HObject ho_Line2Contours;

            // Local control variables 

            HTuple hv_ModelRow = null, hv_ModelColumn = null;
            HTuple hv_ModelAngle = null, hv_LineParameter = null, hv_CircleMaxParameter = null;
            HTuple hv_CircleOneParameter = null, hv_CircleTwoParameter = null;
            HTuple hv_CircleThreeParameter = null, hv_Line2Parameter = null;
            HTuple hv_ResultC1 = null, hv_ResultDiameter1 = null, hv_ResultC2 = null;
            HTuple hv_ResultDiameter2 = null, hv_ResultC3 = null, hv_ResultDiameter3 = null;
            HTuple hv_ResultRec = null, hv_ResultDistance = null, hv_ResultAngle = null;
            HTuple hv_ResultMaxDiameter = null, hv_ResultMax1X = null;
            HTuple hv_ResultMax1Y = null, hv_ResultMax2X = null, hv_ResultMax2Y = null;
            HTuple hv_ResultMax3X = null, hv_ResultMax3Y = null, hv_ResultMax1X1 = null;
            HTuple hv_ResultMax1Y1 = null, hv_ResultMax2X1 = null;
            HTuple hv_ResultMax2Y1 = null, hv_ResultMax3X1 = null;
            HTuple hv_ResultMax3Y1 = null, hv_ModelScore = null, hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_DumpWindowHandle = new HTuple();
            HTuple hv_HomMat2D = null, hv_Row6 = null, hv_Column6 = null;
            HTuple hv_HomMat2DInvert = null, hv_RectangleParameter = null;
            HTuple hv_DownLineParameter = null, hv_UpLineParameter = null;
            HTuple hv_DistanceY = new HTuple(), hv_DistanceX = new HTuple();
            HTuple hv_RowProj = new HTuple(), hv_ColProj = new HTuple();
            HTuple hv_ResultMaxX = new HTuple(), hv_ResultMaxY = new HTuple();
            HTuple hv_Rx = new HTuple(), hv_Ry = new HTuple(), hv_deltX = new HTuple();
            HTuple hv_deltY = new HTuple(), hv_dR = new HTuple(), hv_deltR1 = new HTuple();
            HTuple hv_deltR2 = new HTuple(), hv_deltR3 = new HTuple();
            HTuple hv_DistanceMin = new HTuple(), hv_DistanceMax = new HTuple();
            HTuple hv_ResultMaxDistance = new HTuple(), hv_ResultMinDistance = new HTuple();
            HTuple hv_Angle = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ResultImage);
            HOperatorSet.GenEmptyObj(out ho_ResultObj);
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContour);
            HOperatorSet.GenEmptyObj(out ho_CircleOneContour);
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContour);
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContour);
            HOperatorSet.GenEmptyObj(out ho_RectangleContour);
            HOperatorSet.GenEmptyObj(out ho_DownLineContour);
            HOperatorSet.GenEmptyObj(out ho_UpLineContour);
            HOperatorSet.GenEmptyObj(out ho_LineContour);
            HOperatorSet.GenEmptyObj(out ho_Line2Contour);
            HOperatorSet.GenEmptyObj(out ho_ContourX);
            HOperatorSet.GenEmptyObj(out ho_ContourY);
            HOperatorSet.GenEmptyObj(out ho_LineContours);
            HOperatorSet.GenEmptyObj(out ho_RotateImage);
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContours);
            HOperatorSet.GenEmptyObj(out ho_CircleOneContours);
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContours);
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContours);
            HOperatorSet.GenEmptyObj(out ho_RectangleContours);
            HOperatorSet.GenEmptyObj(out ho_DownLineContours);
            HOperatorSet.GenEmptyObj(out ho_UpLineContours);
            HOperatorSet.GenEmptyObj(out ho_Line2Contours);

            //初始化输出变量
            ho_ResultImage.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ResultImage);
            ho_ResultObj.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ResultObj);
            hv_ResultValue = new HTuple();
            hv_ResultValue[0] = -100;
            hv_ResultValue[1] = -100;
            hv_ResultValue[2] = -100;
            hv_ResultValue[3] = -100;
            hv_ResultValue[4] = -100;
            hv_ResultValue[5] = -100;
            hv_ResultValue[6] = -100;
            hv_ResultValue[7] = -100;
            hv_ResultValue[8] = -100;
            hv_ResultValue[9] = -100;
            hv_ResultValue[10] = -100;
            hv_ResultValue[11] = -100;
            hv_ResultValue[12] = -100;
            hv_ResultValue[13] = -100;
            hv_ResultValue[14] = -100;
            hv_ResultInfo = "";
            hv_Result = 0;

            //初始化中间变量
            hv_ModelRow = new HTuple();
            hv_ModelColumn = new HTuple();
            hv_ModelAngle = new HTuple();
            hv_LineParameter = new HTuple();
            hv_CircleMaxParameter = new HTuple();
            hv_CircleOneParameter = new HTuple();
            hv_CircleTwoParameter = new HTuple();
            hv_CircleThreeParameter = new HTuple();
            hv_Line2Parameter = new HTuple();
            hv_ResultC1 = new HTuple();
            hv_ResultDiameter1 = new HTuple();
            hv_ResultC2 = new HTuple();
            hv_ResultDiameter2 = new HTuple();
            hv_ResultC3 = new HTuple();
            hv_ResultDiameter3 = new HTuple();
            hv_ResultRec = new HTuple();
            hv_ResultDistance = new HTuple();
            hv_ResultAngle = new HTuple();
            hv_ResultMaxDiameter = new HTuple();
            hv_ResultMax1X = new HTuple();
            hv_ResultMax1Y = new HTuple();
            hv_ResultMax2X = new HTuple();
            hv_ResultMax2Y = new HTuple();
            hv_ResultMax3X = new HTuple();
            hv_ResultMax3Y = new HTuple();
            hv_ResultMax1X1 = new HTuple();
            hv_ResultMax1Y1 = new HTuple();
            hv_ResultMax2X1 = new HTuple();
            hv_ResultMax2Y1 = new HTuple();
            hv_ResultMax3X1 = new HTuple();
            hv_ResultMax3Y1 = new HTuple();
            ho_ModelContours.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            ho_CircleMaxContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContour);
            ho_CircleOneContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleOneContour);
            ho_CircleTwoContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContour);
            ho_CircleThreeContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContour);
            ho_RectangleContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_RectangleContour);
            ho_DownLineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_DownLineContour);
            ho_UpLineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_UpLineContour);
            ho_LineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_LineContour);
            ho_Line2Contour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Line2Contour);
            ho_ContourX.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ContourX);
            ho_ContourY.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ContourY);

            try
            {

                //旋转矫正图像
                HOperatorSet.FindShapeModel(ho_Image, hv_ModelId, (new HTuple(-45)).TupleRad()
                    , (new HTuple(90)).TupleRad(), 0.5, 1, 0.5, "least_squares", (new HTuple(8)).TupleConcat(
                    1), 0.75, out hv_ModelRow, out hv_ModelColumn, out hv_ModelAngle, out hv_ModelScore);
                if ((int)(new HTuple((new HTuple(hv_ModelRow.TupleLength())).TupleEqual(0))) != 0)
                {
                    HOperatorSet.ClearWindow(hv_WindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
                    set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                    disp_message(hv_WindowHandle, "定位失败:NG", "image", 1600, 10, "red", "false");
                    hv_Result = 0;
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);(FAI-17);(FAI-18);(FAI-12);(FAI-19);(FAI-14);(FAI-3);(FAI-13);(FAI-11);";
                    //
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                    HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                    HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                    HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_DumpWindowHandle);
                    set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                    disp_message(hv_DumpWindowHandle, "定位失败:NG", "image", 1600, 10, "red",
                        "false");
                    ho_ResultImage.Dispose();
                    HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                    HOperatorSet.CloseWindow(hv_DumpWindowHandle);
                    //
                    ho_ModelContours.Dispose();
                    ho_CircleMaxContour.Dispose();
                    ho_CircleOneContour.Dispose();
                    ho_CircleTwoContour.Dispose();
                    ho_CircleThreeContour.Dispose();
                    ho_RectangleContour.Dispose();
                    ho_DownLineContour.Dispose();
                    ho_UpLineContour.Dispose();
                    ho_LineContour.Dispose();
                    ho_Line2Contour.Dispose();
                    ho_ContourX.Dispose();
                    ho_ContourY.Dispose();
                    ho_LineContours.Dispose();
                    ho_RotateImage.Dispose();
                    ho_CircleMaxContours.Dispose();
                    ho_CircleOneContours.Dispose();
                    ho_CircleTwoContours.Dispose();
                    ho_CircleThreeContours.Dispose();
                    ho_RectangleContours.Dispose();
                    ho_DownLineContours.Dispose();
                    ho_UpLineContours.Dispose();
                    ho_Line2Contours.Dispose();

                    return;
                }
                //
                //显示模板
                ho_ModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelId, 1);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_ModelRow, hv_ModelColumn, hv_ModelAngle,
                    out hv_HomMat2D);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.AffineTransContourXld(ho_ModelContours, out ExpTmpOutVar_0, hv_HomMat2D);
                    ho_ModelContours.Dispose();
                    ho_ModelContours = ExpTmpOutVar_0;
                }
                //
                //旋转图像
                HOperatorSet.AlignMetrologyModel(hv_MetrologyHandle, hv_ModelRow, hv_ModelColumn,
                    hv_ModelAngle);
                HOperatorSet.ApplyMetrologyModel(ho_Image, hv_MetrologyHandle);
                ho_LineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_LineContours, hv_MetrologyHandle,
                    0, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 0, "all", "result_type",
                    "all_param", out hv_LineParameter);
                ho_LineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_LineContour, hv_MetrologyHandle,
                    0, "all", 1.5);
                if ((int)(new HTuple((new HTuple(hv_LineParameter.TupleLength())).TupleEqual(
                    0))) != 0)
                {
                    HOperatorSet.ClearWindow(hv_WindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
                    HOperatorSet.DispObj(ho_ModelContours, hv_WindowHandle);
                    HOperatorSet.DispObj(ho_LineContours, hv_WindowHandle);
                    set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                    disp_message(hv_WindowHandle, "对齐失败:NG", "image", 1600, 10, "red", "false");
                    hv_Result = 0;
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);(FAI-17);(FAI-18);(FAI-12);(FAI-19);(FAI-14);(FAI-3);(FAI-13);(FAI-11);";
                    //
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                    HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                    HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                    HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_ModelContours, hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_LineContours, hv_DumpWindowHandle);
                    set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                    disp_message(hv_DumpWindowHandle, "对齐失败:NG", "image", 1600, 10, "red",
                        "false");
                    ho_ResultImage.Dispose();
                    HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                    HOperatorSet.CloseWindow(hv_DumpWindowHandle);
                    //
                    ho_ModelContours.Dispose();
                    ho_CircleMaxContour.Dispose();
                    ho_CircleOneContour.Dispose();
                    ho_CircleTwoContour.Dispose();
                    ho_CircleThreeContour.Dispose();
                    ho_RectangleContour.Dispose();
                    ho_DownLineContour.Dispose();
                    ho_UpLineContour.Dispose();
                    ho_LineContour.Dispose();
                    ho_Line2Contour.Dispose();
                    ho_ContourX.Dispose();
                    ho_ContourY.Dispose();
                    ho_LineContours.Dispose();
                    ho_RotateImage.Dispose();
                    ho_CircleMaxContours.Dispose();
                    ho_CircleOneContours.Dispose();
                    ho_CircleTwoContours.Dispose();
                    ho_CircleThreeContours.Dispose();
                    ho_RectangleContours.Dispose();
                    ho_DownLineContours.Dispose();
                    ho_UpLineContours.Dispose();
                    ho_Line2Contours.Dispose();

                    return;
                }
                //
                ho_RotateImage.Dispose();
                AlignImage(ho_Image, out ho_RotateImage, hv_LineParameter, 90, out hv_HomMat2DInvert);
                //
                //开始测量
                HOperatorSet.FindShapeModel(ho_RotateImage, hv_ModelId, (new HTuple(-45)).TupleRad()
                    , (new HTuple(90)).TupleRad(), 0.5, 1, 0.5, "least_squares", (new HTuple(8)).TupleConcat(
                    1), 0.75, out hv_ModelRow, out hv_ModelColumn, out hv_ModelAngle, out hv_ModelScore);
                HOperatorSet.AlignMetrologyModel(hv_MetrologyHandle, hv_ModelRow, hv_ModelColumn,
                    hv_ModelAngle);
                HOperatorSet.ApplyMetrologyModel(ho_RotateImage, hv_MetrologyHandle);
                //
                //大圆
                ho_CircleMaxContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleMaxContours, hv_MetrologyHandle,
                    1, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 1, "all", "result_type",
                    "all_param", out hv_CircleMaxParameter);
                ho_CircleMaxContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleMaxContour, hv_MetrologyHandle,
                    1, "all", 1.5);
                //
                //圆一
                ho_CircleOneContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleOneContours, hv_MetrologyHandle,
                    2, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 2, "all", "result_type",
                    "all_param", out hv_CircleOneParameter);
                ho_CircleOneContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleOneContour, hv_MetrologyHandle,
                    2, "all", 1.5);
                //
                //圆二
                ho_CircleTwoContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleTwoContours, hv_MetrologyHandle,
                    3, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 3, "all", "result_type",
                    "all_param", out hv_CircleTwoParameter);
                ho_CircleTwoContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleTwoContour, hv_MetrologyHandle,
                    3, "all", 1.5);
                //
                //圆三
                ho_CircleThreeContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleThreeContours, hv_MetrologyHandle,
                    4, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 4, "all", "result_type",
                    "all_param", out hv_CircleThreeParameter);
                ho_CircleThreeContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleThreeContour, hv_MetrologyHandle,
                    4, "all", 1.5);
                //
                //矩形
                ho_RectangleContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_RectangleContours, hv_MetrologyHandle,
                    5, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 5, "all", "result_type",
                    "all_param", out hv_RectangleParameter);
                ho_RectangleContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_RectangleContour, hv_MetrologyHandle,
                    5, "all", 1.5);
                //
                //下直线
                ho_DownLineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_DownLineContours, hv_MetrologyHandle,
                    6, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 6, "all", "result_type",
                    "all_param", out hv_DownLineParameter);
                ho_DownLineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_DownLineContour, hv_MetrologyHandle,
                    6, "all", 1.5);
                //
                //上直线
                ho_UpLineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_UpLineContours, hv_MetrologyHandle,
                    7, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 7, "all", "result_type",
                    "all_param", out hv_UpLineParameter);
                ho_UpLineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_UpLineContour, hv_MetrologyHandle,
                    7, "all", 1.5);
                //
                //基准边
                ho_LineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_LineContours, hv_MetrologyHandle,
                    0, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 0, "all", "result_type",
                    "all_param", out hv_LineParameter);
                ho_LineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_LineContour, hv_MetrologyHandle,
                    0, "all", 1.5);

                //竖直边
                ho_Line2Contours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_Line2Contours, hv_MetrologyHandle,
                    8, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 8, "all", "result_type",
                    "all_param", out hv_Line2Parameter);
                ho_Line2Contour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_Line2Contour, hv_MetrologyHandle,
                    8, "all", 1.5);

                //计算大圆直径
                if ((int)(new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    hv_ResultMaxDiameter = (2 * (hv_CircleMaxParameter.TupleSelect(2))) * hv_RatioPixelMax1X;

                    HOperatorSet.DistancePl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_DistanceY);
                    HOperatorSet.DistancePl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_LineParameter.TupleSelect(0), hv_LineParameter.TupleSelect(1),
                        hv_LineParameter.TupleSelect(2), hv_LineParameter.TupleSelect(3), out hv_DistanceX);

                    HOperatorSet.ProjectionPl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_RowProj,
                        out hv_ColProj);
                    ho_ContourY.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourY, ((hv_CircleMaxParameter.TupleSelect(
                        0))).TupleConcat(hv_RowProj), ((hv_CircleMaxParameter.TupleSelect(1))).TupleConcat(
                        hv_ColProj));

                    HOperatorSet.ProjectionPl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_LineParameter.TupleSelect(0), hv_LineParameter.TupleSelect(1),
                        hv_LineParameter.TupleSelect(2), hv_LineParameter.TupleSelect(3), out hv_RowProj,
                        out hv_ColProj);
                    ho_ContourX.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourX, ((hv_CircleMaxParameter.TupleSelect(
                        0))).TupleConcat(hv_RowProj), ((hv_CircleMaxParameter.TupleSelect(1))).TupleConcat(
                        hv_ColProj));

                    hv_ResultMaxX = hv_DistanceX * hv_RatioPixelMaxX;
                    hv_ResultMaxY = hv_DistanceY * hv_RatioPixelMaxY;

                }

                //计算圆一输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleOneParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax1X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleOneParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax1X;
                    hv_ResultMax1Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleOneParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax1Y;

                    hv_Rx = hv_X - hv_ResultMax1X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax1Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax1X1;
                    hv_deltY = hv_Y - hv_ResultMax1Y1;
                    hv_dR = ((2 * (hv_CircleOneParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle1;
                    hv_deltR1 = hv_dR - (hv_Diameter - 0.06);
                    hv_ResultC1 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC1)).TupleAbs();

                    hv_ResultDiameter1 = ((2 * (hv_CircleOneParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle1;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[0] = hv_ResultDiameter1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[1] = hv_ResultC1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[9] = hv_ResultMax1X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[10] = hv_ResultMax1Y1;
                }
                //
                //
                //计算圆二输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleTwoParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax2X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleTwoParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax2X;
                    hv_ResultMax2Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleTwoParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax2Y;

                    hv_Rx = hv_X - hv_ResultMax2X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax2Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax2X1;
                    hv_deltY = hv_Y - hv_ResultMax2Y1;
                    hv_dR = ((2 * (hv_CircleTwoParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle2;
                    hv_deltR2 = hv_dR - (hv_Diameter - 0.06);

                    hv_ResultC2 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC2)).TupleAbs();


                    hv_ResultDiameter2 = ((2 * (hv_CircleTwoParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle2;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[2] = hv_ResultDiameter2;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[3] = hv_ResultC2;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[11] = hv_ResultMax2X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[12] = hv_ResultMax2Y1;

                }
                //
                //
                //计算圆三输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleThreeParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax3X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleThreeParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax3X;
                    hv_ResultMax3Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleThreeParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax3Y;

                    hv_Rx = hv_X - hv_ResultMax3X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax3Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax3X1;
                    hv_deltY = hv_Y - hv_ResultMax3Y1;
                    hv_dR = ((2 * (hv_CircleThreeParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle3;
                    hv_deltR3 = hv_dR - (hv_Diameter - 0.06);
                    hv_ResultC3 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC3)).TupleAbs();

                    hv_ResultDiameter3 = ((2 * (hv_CircleThreeParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle3;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[4] = hv_ResultDiameter3;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[5] = hv_ResultC3;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[13] = hv_ResultMax3X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[14] = hv_ResultMax3Y1;
                }
                //
                //计算矩形输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_RectangleParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {
                    hv_ResultRec = (((((hv_RectangleParameter.TupleSelect(1)) - (hv_CircleMaxParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelRec) + hv_BC_Rec;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[6] = hv_ResultRec;
                }
                //
                //计算直线输出信息
                if ((int)((new HTuple((new HTuple(hv_DownLineParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_UpLineParameter.TupleLength())).TupleGreater(
                    0)))) != 0)
                {
                    //
                    HOperatorSet.DistanceLc(ho_DownLineContour, hv_UpLineParameter.TupleSelect(
                        0), hv_UpLineParameter.TupleSelect(1), hv_UpLineParameter.TupleSelect(
                        2), hv_UpLineParameter.TupleSelect(3), out hv_DistanceMin, out hv_DistanceMax);
                    hv_ResultMaxDistance = hv_DistanceMax * hv_RatioPixel_d;
                    hv_ResultMinDistance = hv_DistanceMin * hv_RatioPixel_d;
                    hv_ResultDistance = ((1.0 * (hv_ResultMaxDistance + hv_ResultMinDistance)) / 2) + hv_BC_Distance;
                    HOperatorSet.AngleLl(hv_DownLineParameter.TupleSelect(0), hv_DownLineParameter.TupleSelect(
                        1), hv_DownLineParameter.TupleSelect(2), hv_DownLineParameter.TupleSelect(
                        3), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_Angle);
                    //angle_ll (DownLineParameter[0], DownLineParameter[1], DownLineParameter[2], DownLineParameter[3], UpLineParameter[0], UpLineParameter[1], UpLineParameter[2], UpLineParameter[3], Angle)
                    HOperatorSet.TupleDeg(hv_Angle, out hv_ResultAngle);
                    HOperatorSet.TupleAbs(hv_ResultAngle, out hv_ResultAngle);
                    hv_ResultAngle = hv_ResultAngle + hv_BC_Angle;
                    //
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[7] = hv_ResultDistance;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[8] = hv_ResultAngle;
                }
                //
                //判断检测结果
                if ((int)((new HTuple((new HTuple((new HTuple((new HTuple((new HTuple((new HTuple(hv_ResultC1.TupleLength()
                    )).TupleGreater(0))).TupleAnd(new HTuple((new HTuple(hv_ResultC2.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultC3.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultRec.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultDistance.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultAngle.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter1.TupleLess(hv_GC_Circle1_0))).TupleOr(
                        new HTuple(hv_ResultDiameter1.TupleGreater(hv_GC_Circle1)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDiameter2.TupleLess(hv_GC_Circle2_0))).TupleOr(
                        new HTuple(hv_ResultDiameter2.TupleGreater(hv_GC_Circle2)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDiameter3.TupleLess(hv_GC_Circle3_0))).TupleOr(
                        new HTuple(hv_ResultDiameter3.TupleGreater(hv_GC_Circle3)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC1.TupleLess(hv_GC_RC1_0))).TupleOr(
                        new HTuple(hv_ResultC1.TupleGreater(hv_GC_RC1 + hv_deltR1)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC2.TupleLess(hv_GC_RC2_0))).TupleOr(
                        new HTuple(hv_ResultC2.TupleGreater(hv_GC_RC2 + hv_deltR2)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC3.TupleLess(hv_GC_RC3_0))).TupleOr(
                        new HTuple(hv_ResultC3.TupleGreater(hv_GC_RC3 + hv_deltR3)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultRec.TupleLess(hv_GC_Rec_0))).TupleOr(
                        new HTuple(hv_ResultRec.TupleGreater(hv_GC_Rec)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDistance.TupleLess(hv_GC_Distance_0))).TupleOr(
                        new HTuple(hv_ResultDistance.TupleGreater(hv_GC_Distance)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultAngle.TupleLess(hv_GC_Angle_0))).TupleOr(
                        new HTuple(hv_ResultAngle.TupleGreater(hv_GC_Angle)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else
                    {
                        hv_Result = 1;
                    }
                }
                else
                {
                    hv_Result = 0;
                }
                //
                //将显示对象添加到结果中
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ModelContours, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleMaxContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleOneContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleTwoContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleThreeContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_RectangleContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_DownLineContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_UpLineContour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_LineContour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_Line2Contour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ContourX, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ContourY, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }

                //显示结果
                HOperatorSet.ClearWindow(hv_WindowHandle);
                HOperatorSet.DispObj(ho_RotateImage, hv_WindowHandle);
                HOperatorSet.DispObj(ho_ResultObj, hv_WindowHandle);

                //
                HOperatorSet.GetImageSize(ho_RotateImage, out hv_Width, out hv_Height);
                HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                HOperatorSet.SetLineWidth(hv_DumpWindowHandle, 5);
                HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                HOperatorSet.DispObj(ho_RotateImage, hv_DumpWindowHandle);
                HOperatorSet.DispObj(ho_ResultObj, hv_DumpWindowHandle);

                //
                //打印检测结果
                set_display_font(hv_WindowHandle, 15, "mono", "true", "false");
                set_display_font(hv_DumpWindowHandle, 120, "mono", "true", "false");
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter1.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter1.TupleLess(hv_GC_Circle1_0))).TupleOr(
                        new HTuple(hv_ResultDiameter1.TupleGreater(hv_GC_Circle1)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-17);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆1直径:测量失败", "image", 50, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆1直径:测量失败", "image", 50, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-17);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter2.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter2.TupleLess(hv_GC_Circle2_0))).TupleOr(
                        new HTuple(hv_ResultDiameter2.TupleGreater(hv_GC_Circle2)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-12);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆2直径:测量失败", "image", 200, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆2直径:测量失败", "image", 200, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-12);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter3.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter3.TupleLess(hv_GC_Circle3_0))).TupleOr(
                        new HTuple(hv_ResultDiameter3.TupleGreater(hv_GC_Circle3)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-14);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆3直径:测量失败", "image", 350, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆3直径:测量失败", "image", 350, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-14);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC1.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC1.TupleLess(hv_GC_RC1_0))).TupleOr(new HTuple(hv_ResultC1.TupleGreater(
                        hv_GC_RC1 + hv_deltR1)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(".3f")),
                            "image", 500, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(
                            ".3f")), "image", 500, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-20);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(".3f")),
                            "image", 500, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(
                            ".3f")), "image", 500, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆1位置度:测量失败", "image", 500, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆1位置度:测量失败", "image", 500, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC2.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC2.TupleLess(hv_GC_RC2_0))).TupleOr(new HTuple(hv_ResultC2.TupleGreater(
                        hv_GC_RC2 + hv_deltR2)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(".3f")),
                            "image", 650, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(
                            ".3f")), "image", 650, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-18);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(".3f")),
                            "image", 650, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(
                            ".3f")), "image", 650, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                    //
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆2位置度:测量失败", "image", 650, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆2位置度:测量失败", "image", 650, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-18);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC3.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC3.TupleLess(hv_GC_RC3_0))).TupleOr(new HTuple(hv_ResultC3.TupleGreater(
                        hv_GC_RC3 + hv_deltR3)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(".3f")),
                            "image", 800, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(
                            ".3f")), "image", 800, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-19);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(".3f")),
                            "image", 800, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(
                            ".3f")), "image", 800, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆3位置度:测量失败", "image", 800, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆3位置度:测量失败", "image", 800, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-19);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultRec.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultRec.TupleLess(hv_GC_Rec_0))).TupleOr(new HTuple(hv_ResultRec.TupleGreater(
                        hv_GC_Rec)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(".3f")),
                            "image", 950, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(
                            ".3f")), "image", 950, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-3);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(".3f")),
                            "image", 950, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(
                            ".3f")), "image", 950, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "插头圆心距:测量失败", "image", 950, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "插头圆心距:测量失败", "image", 950, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-3);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDistance.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDistance.TupleLess(hv_GC_Distance_0))).TupleOr(
                        new HTuple(hv_ResultDistance.TupleGreater(hv_GC_Distance)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-13);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "FPC间距:测量失败", "image", 1100, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "FPC间距:测量失败", "image", 1100, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-13);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultAngle.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultAngle.TupleLess(hv_GC_Angle_0))).TupleOr(new HTuple(hv_ResultAngle.TupleGreater(
                        hv_GC_Angle)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(".3f")),
                            "image", 1250, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(
                            ".3f")), "image", 1250, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-11);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(".3f")),
                            "image", 1250, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(
                            ".3f")), "image", 1250, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "FPC角度:测量失败", "image", 1250, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "FPC角度:测量失败", "image", 1250, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-11);";
                }
                //
                //
                if ((int)(new HTuple((new HTuple(hv_ResultMaxDiameter.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, "大圆X:" + (hv_ResultMaxX.TupleString(".3f")),
                        "image", 1400, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "大圆X:" + (hv_ResultMaxX.TupleString(".3f")),
                        "image", 1400, 10, "green", "false");

                    disp_message(hv_WindowHandle, "大圆Y:" + (hv_ResultMaxY.TupleString(".3f")),
                        "image", 1550, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "大圆Y:" + (hv_ResultMaxY.TupleString(".3f")),
                        "image", 1550, 10, "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter1.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆1X:" + (hv_ResultMax1X.TupleString(".3f"))) + "-") + (hv_ResultMax1X1.TupleString(
                        ".3f")), "image", 1700, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆1X:" + (hv_ResultMax1X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax1X1.TupleString(".3f")), "image", 1700, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆1Y:" + (hv_ResultMax1Y.TupleString(".3f"))) + "-") + (hv_ResultMax1Y1.TupleString(
                        ".3f")), "image", 1850, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆1Y:" + (hv_ResultMax1Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax1Y1.TupleString(".3f")), "image", 1850, 10,
                        "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter2.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆2X:" + (hv_ResultMax2X.TupleString(".3f"))) + "-") + (hv_ResultMax2X1.TupleString(
                        ".3f")), "image", 2000, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆2X:" + (hv_ResultMax2X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax2X1.TupleString(".3f")), "image", 2000, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆2Y:" + (hv_ResultMax2Y.TupleString(".3f"))) + "-") + (hv_ResultMax2Y1.TupleString(
                        ".3f")), "image", 2150, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆2Y:" + (hv_ResultMax2Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax2Y1.TupleString(".3f")), "image", 2150, 10,
                        "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter3.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆3X:" + (hv_ResultMax3X.TupleString(".3f"))) + "-") + (hv_ResultMax3X1.TupleString(
                        ".3f")), "image", 2300, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆3X:" + (hv_ResultMax3X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax3X1.TupleString(".3f")), "image", 2300, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆3Y:" + (hv_ResultMax3Y.TupleString(".3f"))) + "-") + (hv_ResultMax3Y1.TupleString(
                        ".3f")), "image", 2450, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆3Y:" + (hv_ResultMax3Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax3Y1.TupleString(".3f")), "image", 2450, 10,
                        "green", "false");
                }

                //

                set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                //
                if ((int)(new HTuple(hv_Result.TupleEqual(0))) != 0)
                {
                    disp_message(hv_WindowHandle, "产品:NG", "image", 2550, 10, "red", "false");
                    disp_message(hv_DumpWindowHandle, "产品:NG", "image", 2550, 10, "red", "false");
                }
                else
                {
                    disp_message(hv_WindowHandle, "产品:OK", "image", 2550, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "产品:OK", "image", 2550, 10, "green",
                        "false");
                }
                //
                ho_ResultImage.Dispose();
                HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                HOperatorSet.CloseWindow(hv_DumpWindowHandle);

                //
                ho_ModelContours.Dispose();
                ho_CircleMaxContour.Dispose();
                ho_CircleOneContour.Dispose();
                ho_CircleTwoContour.Dispose();
                ho_CircleThreeContour.Dispose();
                ho_RectangleContour.Dispose();
                ho_DownLineContour.Dispose();
                ho_UpLineContour.Dispose();
                ho_LineContour.Dispose();
                ho_Line2Contour.Dispose();
                ho_ContourX.Dispose();
                ho_ContourY.Dispose();
                ho_LineContours.Dispose();
                ho_RotateImage.Dispose();
                ho_CircleMaxContours.Dispose();
                ho_CircleOneContours.Dispose();
                ho_CircleTwoContours.Dispose();
                ho_CircleThreeContours.Dispose();
                ho_RectangleContours.Dispose();
                ho_DownLineContours.Dispose();
                ho_UpLineContours.Dispose();
                ho_Line2Contours.Dispose();

                return;
            }
            catch
            {
                ho_ModelContours.Dispose();
                ho_CircleMaxContour.Dispose();
                ho_CircleOneContour.Dispose();
                ho_CircleTwoContour.Dispose();
                ho_CircleThreeContour.Dispose();
                ho_RectangleContour.Dispose();
                ho_DownLineContour.Dispose();
                ho_UpLineContour.Dispose();
                ho_LineContour.Dispose();
                ho_Line2Contour.Dispose();
                ho_ContourX.Dispose();
                ho_ContourY.Dispose();
                ho_LineContours.Dispose();
                ho_RotateImage.Dispose();
                ho_CircleMaxContours.Dispose();
                ho_CircleOneContours.Dispose();
                ho_CircleTwoContours.Dispose();
                ho_CircleThreeContours.Dispose();
                ho_RectangleContours.Dispose();
                ho_DownLineContours.Dispose();
                ho_UpLineContours.Dispose();
                ho_Line2Contours.Dispose();

                return;
            }
        }

        #endregion

        #region 相机4

        public void Cam4_Procedure(HObject ho_Image, out HObject ho_ResultImage, out HObject ho_ResultObj,
      HTuple hv_WindowHandle, HTuple hv_ModelId, HTuple hv_MetrologyHandle, HTuple hv_RatioPixelMaxX,
      HTuple hv_RatioPixelMaxY, HTuple hv_RatioPixelMax1X, HTuple hv_RatioPixelMax1Y,
      HTuple hv_RatioPixelMax2X, HTuple hv_RatioPixelMax2Y, HTuple hv_RatioPixelMax3X,
      HTuple hv_RatioPixelMax3Y, HTuple hv_RatioPixelRec, HTuple hv_RatioPixel_d,
      HTuple hv_X, HTuple hv_Y, HTuple hv_Diameter, HTuple hv_GC_Circle1, HTuple hv_GC_Circle1_0,
      HTuple hv_GC_Circle2, HTuple hv_GC_Circle2_0, HTuple hv_GC_Circle3, HTuple hv_GC_Circle3_0,
      HTuple hv_GC_RC1, HTuple hv_GC_RC1_0, HTuple hv_GC_RC2, HTuple hv_GC_RC2_0,
      HTuple hv_GC_RC3, HTuple hv_GC_RC3_0, HTuple hv_GC_Rec, HTuple hv_GC_Rec_0,
      HTuple hv_GC_Distance, HTuple hv_GC_Distance_0, HTuple hv_GC_Angle, HTuple hv_GC_Angle_0,
      HTuple hv_BC_Circle1, HTuple hv_BC_Circle2, HTuple hv_BC_Circle3, HTuple hv_BC_RC1,
      HTuple hv_BC_RC2, HTuple hv_BC_RC3, HTuple hv_BC_Rec, HTuple hv_BC_Distance,
      HTuple hv_BC_Angle, HTuple hv_BC_Min, HTuple hv_BC_Max, out HTuple hv_ResultValue,
      out HTuple hv_ResultInfo, out HTuple hv_Result)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_ModelContours, ho_CircleMaxContour;
            HObject ho_CircleOneContour, ho_CircleTwoContour, ho_CircleThreeContour;
            HObject ho_RectangleContour, ho_DownLineContour, ho_UpLineContour;
            HObject ho_LineContour, ho_Line2Contour, ho_ContourX, ho_ContourY;
            HObject ho_LineContours, ho_RotateImage, ho_CircleMaxContours;
            HObject ho_CircleOneContours, ho_CircleTwoContours, ho_CircleThreeContours;
            HObject ho_RectangleContours, ho_DownLineContours, ho_UpLineContours;
            HObject ho_Line2Contours;

            // Local control variables 

            HTuple hv_ModelRow = null, hv_ModelColumn = null;
            HTuple hv_ModelAngle = null, hv_LineParameter = null, hv_CircleMaxParameter = null;
            HTuple hv_CircleOneParameter = null, hv_CircleTwoParameter = null;
            HTuple hv_CircleThreeParameter = null, hv_Line2Parameter = null;
            HTuple hv_ResultC1 = null, hv_ResultDiameter1 = null, hv_ResultC2 = null;
            HTuple hv_ResultDiameter2 = null, hv_ResultC3 = null, hv_ResultDiameter3 = null;
            HTuple hv_ResultRec = null, hv_ResultDistance = null, hv_ResultAngle = null;
            HTuple hv_ResultMaxDiameter = null, hv_ResultMax1X = null;
            HTuple hv_ResultMax1Y = null, hv_ResultMax2X = null, hv_ResultMax2Y = null;
            HTuple hv_ResultMax3X = null, hv_ResultMax3Y = null, hv_ResultMax1X1 = null;
            HTuple hv_ResultMax1Y1 = null, hv_ResultMax2X1 = null;
            HTuple hv_ResultMax2Y1 = null, hv_ResultMax3X1 = null;
            HTuple hv_ResultMax3Y1 = null, hv_ModelScore = null, hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_DumpWindowHandle = new HTuple();
            HTuple hv_HomMat2D = null, hv_Row6 = null, hv_Column6 = null;
            HTuple hv_HomMat2DInvert = null, hv_RectangleParameter = null;
            HTuple hv_DownLineParameter = null, hv_UpLineParameter = null;
            HTuple hv_DistanceY = new HTuple(), hv_DistanceX = new HTuple();
            HTuple hv_RowProj = new HTuple(), hv_ColProj = new HTuple();
            HTuple hv_ResultMaxX = new HTuple(), hv_ResultMaxY = new HTuple();
            HTuple hv_Rx = new HTuple(), hv_Ry = new HTuple(), hv_deltX = new HTuple();
            HTuple hv_deltY = new HTuple(), hv_dR = new HTuple(), hv_deltR1 = new HTuple();
            HTuple hv_deltR2 = new HTuple(), hv_deltR3 = new HTuple();
            HTuple hv_DistanceMin = new HTuple(), hv_DistanceMax = new HTuple();
            HTuple hv_ResultMaxDistance = new HTuple(), hv_ResultMinDistance = new HTuple();
            HTuple hv_Angle = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ResultImage);
            HOperatorSet.GenEmptyObj(out ho_ResultObj);
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContour);
            HOperatorSet.GenEmptyObj(out ho_CircleOneContour);
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContour);
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContour);
            HOperatorSet.GenEmptyObj(out ho_RectangleContour);
            HOperatorSet.GenEmptyObj(out ho_DownLineContour);
            HOperatorSet.GenEmptyObj(out ho_UpLineContour);
            HOperatorSet.GenEmptyObj(out ho_LineContour);
            HOperatorSet.GenEmptyObj(out ho_Line2Contour);
            HOperatorSet.GenEmptyObj(out ho_ContourX);
            HOperatorSet.GenEmptyObj(out ho_ContourY);
            HOperatorSet.GenEmptyObj(out ho_LineContours);
            HOperatorSet.GenEmptyObj(out ho_RotateImage);
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContours);
            HOperatorSet.GenEmptyObj(out ho_CircleOneContours);
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContours);
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContours);
            HOperatorSet.GenEmptyObj(out ho_RectangleContours);
            HOperatorSet.GenEmptyObj(out ho_DownLineContours);
            HOperatorSet.GenEmptyObj(out ho_UpLineContours);
            HOperatorSet.GenEmptyObj(out ho_Line2Contours);

            //初始化输出变量
            ho_ResultImage.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ResultImage);
            ho_ResultObj.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ResultObj);
            hv_ResultValue = new HTuple();
            hv_ResultValue[0] = -100;
            hv_ResultValue[1] = -100;
            hv_ResultValue[2] = -100;
            hv_ResultValue[3] = -100;
            hv_ResultValue[4] = -100;
            hv_ResultValue[5] = -100;
            hv_ResultValue[6] = -100;
            hv_ResultValue[7] = -100;
            hv_ResultValue[8] = -100;
            hv_ResultValue[9] = -100;
            hv_ResultValue[10] = -100;
            hv_ResultValue[11] = -100;
            hv_ResultValue[12] = -100;
            hv_ResultValue[13] = -100;
            hv_ResultValue[14] = -100;
            hv_ResultInfo = "";
            hv_Result = 0;

            //初始化中间变量
            hv_ModelRow = new HTuple();
            hv_ModelColumn = new HTuple();
            hv_ModelAngle = new HTuple();
            hv_LineParameter = new HTuple();
            hv_CircleMaxParameter = new HTuple();
            hv_CircleOneParameter = new HTuple();
            hv_CircleTwoParameter = new HTuple();
            hv_CircleThreeParameter = new HTuple();
            hv_Line2Parameter = new HTuple();
            hv_ResultC1 = new HTuple();
            hv_ResultDiameter1 = new HTuple();
            hv_ResultC2 = new HTuple();
            hv_ResultDiameter2 = new HTuple();
            hv_ResultC3 = new HTuple();
            hv_ResultDiameter3 = new HTuple();
            hv_ResultRec = new HTuple();
            hv_ResultDistance = new HTuple();
            hv_ResultAngle = new HTuple();
            hv_ResultMaxDiameter = new HTuple();
            hv_ResultMax1X = new HTuple();
            hv_ResultMax1Y = new HTuple();
            hv_ResultMax2X = new HTuple();
            hv_ResultMax2Y = new HTuple();
            hv_ResultMax3X = new HTuple();
            hv_ResultMax3Y = new HTuple();
            hv_ResultMax1X1 = new HTuple();
            hv_ResultMax1Y1 = new HTuple();
            hv_ResultMax2X1 = new HTuple();
            hv_ResultMax2Y1 = new HTuple();
            hv_ResultMax3X1 = new HTuple();
            hv_ResultMax3Y1 = new HTuple();
            ho_ModelContours.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            ho_CircleMaxContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleMaxContour);
            ho_CircleOneContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleOneContour);
            ho_CircleTwoContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleTwoContour);
            ho_CircleThreeContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_CircleThreeContour);
            ho_RectangleContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_RectangleContour);
            ho_DownLineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_DownLineContour);
            ho_UpLineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_UpLineContour);
            ho_LineContour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_LineContour);
            ho_Line2Contour.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Line2Contour);
            ho_ContourX.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ContourX);
            ho_ContourY.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ContourY);

            try
            {

                //旋转矫正图像
                HOperatorSet.FindShapeModel(ho_Image, hv_ModelId, (new HTuple(-45)).TupleRad()
                    , (new HTuple(90)).TupleRad(), 0.5, 1, 0.5, "least_squares", (new HTuple(8)).TupleConcat(
                    1), 0.75, out hv_ModelRow, out hv_ModelColumn, out hv_ModelAngle, out hv_ModelScore);
                if ((int)(new HTuple((new HTuple(hv_ModelRow.TupleLength())).TupleEqual(0))) != 0)
                {
                    HOperatorSet.ClearWindow(hv_WindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
                    set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                    disp_message(hv_WindowHandle, "定位失败:NG", "image", 1600, 10, "red", "false");
                    hv_Result = 0;
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);(FAI-17);(FAI-18);(FAI-12);(FAI-19);(FAI-14);(FAI-3);(FAI-13);(FAI-11);";
                    //
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                    HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                    HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                    HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_DumpWindowHandle);
                    set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                    disp_message(hv_DumpWindowHandle, "定位失败:NG", "image", 1600, 10, "red",
                        "false");
                    ho_ResultImage.Dispose();
                    HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                    HOperatorSet.CloseWindow(hv_DumpWindowHandle);
                    //
                    ho_ModelContours.Dispose();
                    ho_CircleMaxContour.Dispose();
                    ho_CircleOneContour.Dispose();
                    ho_CircleTwoContour.Dispose();
                    ho_CircleThreeContour.Dispose();
                    ho_RectangleContour.Dispose();
                    ho_DownLineContour.Dispose();
                    ho_UpLineContour.Dispose();
                    ho_LineContour.Dispose();
                    ho_Line2Contour.Dispose();
                    ho_ContourX.Dispose();
                    ho_ContourY.Dispose();
                    ho_LineContours.Dispose();
                    ho_RotateImage.Dispose();
                    ho_CircleMaxContours.Dispose();
                    ho_CircleOneContours.Dispose();
                    ho_CircleTwoContours.Dispose();
                    ho_CircleThreeContours.Dispose();
                    ho_RectangleContours.Dispose();
                    ho_DownLineContours.Dispose();
                    ho_UpLineContours.Dispose();
                    ho_Line2Contours.Dispose();

                    return;
                }
                //
                //显示模板
                ho_ModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelId, 1);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_ModelRow, hv_ModelColumn, hv_ModelAngle,
                    out hv_HomMat2D);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.AffineTransContourXld(ho_ModelContours, out ExpTmpOutVar_0, hv_HomMat2D);
                    ho_ModelContours.Dispose();
                    ho_ModelContours = ExpTmpOutVar_0;
                }
                //
                //旋转图像
                HOperatorSet.AlignMetrologyModel(hv_MetrologyHandle, hv_ModelRow, hv_ModelColumn,
                    hv_ModelAngle);
                HOperatorSet.ApplyMetrologyModel(ho_Image, hv_MetrologyHandle);
                ho_LineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_LineContours, hv_MetrologyHandle,
                    0, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 0, "all", "result_type",
                    "all_param", out hv_LineParameter);
                ho_LineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_LineContour, hv_MetrologyHandle,
                    0, "all", 1.5);
                if ((int)(new HTuple((new HTuple(hv_LineParameter.TupleLength())).TupleEqual(
                    0))) != 0)
                {
                    HOperatorSet.ClearWindow(hv_WindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
                    HOperatorSet.DispObj(ho_ModelContours, hv_WindowHandle);
                    HOperatorSet.DispObj(ho_LineContours, hv_WindowHandle);
                    set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                    disp_message(hv_WindowHandle, "对齐失败:NG", "image", 1600, 10, "red", "false");
                    hv_Result = 0;
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);(FAI-17);(FAI-18);(FAI-12);(FAI-19);(FAI-14);(FAI-3);(FAI-13);(FAI-11);";
                    //
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                    HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                    HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                    HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_Image, hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_ModelContours, hv_DumpWindowHandle);
                    HOperatorSet.DispObj(ho_LineContours, hv_DumpWindowHandle);
                    set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                    disp_message(hv_DumpWindowHandle, "对齐失败:NG", "image", 1600, 10, "red",
                        "false");
                    ho_ResultImage.Dispose();
                    HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                    HOperatorSet.CloseWindow(hv_DumpWindowHandle);
                    //
                    ho_ModelContours.Dispose();
                    ho_CircleMaxContour.Dispose();
                    ho_CircleOneContour.Dispose();
                    ho_CircleTwoContour.Dispose();
                    ho_CircleThreeContour.Dispose();
                    ho_RectangleContour.Dispose();
                    ho_DownLineContour.Dispose();
                    ho_UpLineContour.Dispose();
                    ho_LineContour.Dispose();
                    ho_Line2Contour.Dispose();
                    ho_ContourX.Dispose();
                    ho_ContourY.Dispose();
                    ho_LineContours.Dispose();
                    ho_RotateImage.Dispose();
                    ho_CircleMaxContours.Dispose();
                    ho_CircleOneContours.Dispose();
                    ho_CircleTwoContours.Dispose();
                    ho_CircleThreeContours.Dispose();
                    ho_RectangleContours.Dispose();
                    ho_DownLineContours.Dispose();
                    ho_UpLineContours.Dispose();
                    ho_Line2Contours.Dispose();

                    return;
                }
                //
                ho_RotateImage.Dispose();
                AlignImage(ho_Image, out ho_RotateImage, hv_LineParameter, 90, out hv_HomMat2DInvert);
                //
                //开始测量
                HOperatorSet.FindShapeModel(ho_RotateImage, hv_ModelId, (new HTuple(-45)).TupleRad()
                    , (new HTuple(90)).TupleRad(), 0.5, 1, 0.5, "least_squares", (new HTuple(8)).TupleConcat(
                    1), 0.75, out hv_ModelRow, out hv_ModelColumn, out hv_ModelAngle, out hv_ModelScore);
                HOperatorSet.AlignMetrologyModel(hv_MetrologyHandle, hv_ModelRow, hv_ModelColumn,
                    hv_ModelAngle);
                HOperatorSet.ApplyMetrologyModel(ho_RotateImage, hv_MetrologyHandle);
                //
                //大圆
                ho_CircleMaxContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleMaxContours, hv_MetrologyHandle,
                    1, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 1, "all", "result_type",
                    "all_param", out hv_CircleMaxParameter);
                ho_CircleMaxContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleMaxContour, hv_MetrologyHandle,
                    1, "all", 1.5);
                //
                //圆一
                ho_CircleOneContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleOneContours, hv_MetrologyHandle,
                    2, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 2, "all", "result_type",
                    "all_param", out hv_CircleOneParameter);
                ho_CircleOneContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleOneContour, hv_MetrologyHandle,
                    2, "all", 1.5);
                //
                //圆二
                ho_CircleTwoContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleTwoContours, hv_MetrologyHandle,
                    3, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 3, "all", "result_type",
                    "all_param", out hv_CircleTwoParameter);
                ho_CircleTwoContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleTwoContour, hv_MetrologyHandle,
                    3, "all", 1.5);
                //
                //圆三
                ho_CircleThreeContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_CircleThreeContours, hv_MetrologyHandle,
                    4, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 4, "all", "result_type",
                    "all_param", out hv_CircleThreeParameter);
                ho_CircleThreeContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_CircleThreeContour, hv_MetrologyHandle,
                    4, "all", 1.5);
                //
                //矩形
                ho_RectangleContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_RectangleContours, hv_MetrologyHandle,
                    5, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 5, "all", "result_type",
                    "all_param", out hv_RectangleParameter);
                ho_RectangleContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_RectangleContour, hv_MetrologyHandle,
                    5, "all", 1.5);
                //
                //下直线
                ho_DownLineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_DownLineContours, hv_MetrologyHandle,
                    6, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 6, "all", "result_type",
                    "all_param", out hv_DownLineParameter);
                ho_DownLineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_DownLineContour, hv_MetrologyHandle,
                    6, "all", 1.5);
                //
                //上直线
                ho_UpLineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_UpLineContours, hv_MetrologyHandle,
                    7, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 7, "all", "result_type",
                    "all_param", out hv_UpLineParameter);
                ho_UpLineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_UpLineContour, hv_MetrologyHandle,
                    7, "all", 1.5);
                //
                //基准边
                ho_LineContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_LineContours, hv_MetrologyHandle,
                    0, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 0, "all", "result_type",
                    "all_param", out hv_LineParameter);
                ho_LineContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_LineContour, hv_MetrologyHandle,
                    0, "all", 1.5);

                //竖直边
                ho_Line2Contours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_Line2Contours, hv_MetrologyHandle,
                    8, "all", out hv_Row6, out hv_Column6);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 8, "all", "result_type",
                    "all_param", out hv_Line2Parameter);
                ho_Line2Contour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_Line2Contour, hv_MetrologyHandle,
                    8, "all", 1.5);

                //计算大圆直径
                if ((int)(new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    hv_ResultMaxDiameter = (2 * (hv_CircleMaxParameter.TupleSelect(2))) * hv_RatioPixelMax1X;

                    HOperatorSet.DistancePl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_DistanceY);
                    HOperatorSet.DistancePl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_LineParameter.TupleSelect(0), hv_LineParameter.TupleSelect(1),
                        hv_LineParameter.TupleSelect(2), hv_LineParameter.TupleSelect(3), out hv_DistanceX);

                    HOperatorSet.ProjectionPl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_RowProj,
                        out hv_ColProj);
                    ho_ContourY.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourY, ((hv_CircleMaxParameter.TupleSelect(
                        0))).TupleConcat(hv_RowProj), ((hv_CircleMaxParameter.TupleSelect(1))).TupleConcat(
                        hv_ColProj));

                    HOperatorSet.ProjectionPl(hv_CircleMaxParameter.TupleSelect(0), hv_CircleMaxParameter.TupleSelect(
                        1), hv_LineParameter.TupleSelect(0), hv_LineParameter.TupleSelect(1),
                        hv_LineParameter.TupleSelect(2), hv_LineParameter.TupleSelect(3), out hv_RowProj,
                        out hv_ColProj);
                    ho_ContourX.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourX, ((hv_CircleMaxParameter.TupleSelect(
                        0))).TupleConcat(hv_RowProj), ((hv_CircleMaxParameter.TupleSelect(1))).TupleConcat(
                        hv_ColProj));

                    hv_ResultMaxX = hv_DistanceX * hv_RatioPixelMaxX;
                    hv_ResultMaxY = hv_DistanceY * hv_RatioPixelMaxY;

                }

                //计算圆一输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleOneParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax1X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleOneParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax1X;
                    hv_ResultMax1Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleOneParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax1Y;

                    hv_Rx = hv_X - hv_ResultMax1X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1X1 = hv_ResultMax1X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax1Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax1Y1 = hv_ResultMax1Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax1X1;
                    hv_deltY = hv_Y - hv_ResultMax1Y1;
                    hv_dR = ((2 * (hv_CircleOneParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle1;
                    hv_deltR1 = hv_dR - (hv_Diameter - 0.06);
                    hv_ResultC1 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC1)).TupleAbs();

                    hv_ResultDiameter1 = ((2 * (hv_CircleOneParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle1;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[0] = hv_ResultDiameter1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[1] = hv_ResultC1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[9] = hv_ResultMax1X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[10] = hv_ResultMax1Y1;
                }
                //
                //
                //计算圆二输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleTwoParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax2X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleTwoParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax2X;
                    hv_ResultMax2Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleTwoParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax2Y;

                    hv_Rx = hv_X - hv_ResultMax2X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2X1 = hv_ResultMax2X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax2Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax2Y1 = hv_ResultMax2Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax2X1;
                    hv_deltY = hv_Y - hv_ResultMax2Y1;
                    hv_dR = ((2 * (hv_CircleTwoParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle2;
                    hv_deltR2 = hv_dR - (hv_Diameter - 0.06);

                    hv_ResultC2 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC2)).TupleAbs();


                    hv_ResultDiameter2 = ((2 * (hv_CircleTwoParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle2;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[2] = hv_ResultDiameter2;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[3] = hv_ResultC2;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[11] = hv_ResultMax2X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[12] = hv_ResultMax2Y1;

                }
                //
                //
                //计算圆三输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_CircleThreeParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {

                    hv_ResultMax3X = ((((hv_CircleMaxParameter.TupleSelect(1)) - (hv_CircleThreeParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelMax3X;
                    hv_ResultMax3Y = ((((hv_CircleMaxParameter.TupleSelect(0)) - (hv_CircleThreeParameter.TupleSelect(
                        0)))).TupleAbs()) * hv_RatioPixelMax3Y;

                    hv_Rx = hv_X - hv_ResultMax3X;

                    if ((int)(new HTuple(hv_Rx.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Rx.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Rx.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Rx.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Rx.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3X1 = hv_ResultMax3X.Clone();
                        }
                    }

                    hv_Ry = hv_Y - hv_ResultMax3Y;

                    if ((int)(new HTuple(hv_Ry.TupleGreater(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y + hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y + hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y.Clone();
                        }
                    }
                    else if ((int)(new HTuple(hv_Ry.TupleLess(0))) != 0)
                    {
                        if ((int)((new HTuple(((hv_Ry.TupleAbs())).TupleGreater(0.03))).TupleAnd(
                            new HTuple(((hv_Ry.TupleAbs())).TupleLess(0.04)))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y - hv_BC_Min;
                        }
                        else if ((int)(new HTuple(((hv_Ry.TupleAbs())).TupleGreaterEqual(
                            0.04))) != 0)
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y - hv_BC_Max;
                        }
                        else
                        {
                            hv_ResultMax3Y1 = hv_ResultMax3Y.Clone();
                        }
                    }

                    hv_deltX = hv_X - hv_ResultMax3X1;
                    hv_deltY = hv_Y - hv_ResultMax3Y1;
                    hv_dR = ((2 * (hv_CircleThreeParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle3;
                    hv_deltR3 = hv_dR - (hv_Diameter - 0.06);
                    hv_ResultC3 = (((2 * ((((hv_deltX.TuplePow(2)) + (hv_deltY.TuplePow(2)))).TuplePow(
                        0.5))) + hv_BC_RC3)).TupleAbs();

                    hv_ResultDiameter3 = ((2 * (hv_CircleThreeParameter.TupleSelect(2))) * hv_RatioPixel_d) + hv_BC_Circle3;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[4] = hv_ResultDiameter3;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[5] = hv_ResultC3;

                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[13] = hv_ResultMax3X1;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[14] = hv_ResultMax3Y1;
                }
                //
                //计算矩形输出信息
                if ((int)((new HTuple((new HTuple(hv_CircleMaxParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_RectangleParameter.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {
                    hv_ResultRec = (((((hv_RectangleParameter.TupleSelect(1)) - (hv_CircleMaxParameter.TupleSelect(
                        1)))).TupleAbs()) * hv_RatioPixelRec) + hv_BC_Rec;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[6] = hv_ResultRec;
                }
                //
                //计算直线输出信息
                if ((int)((new HTuple((new HTuple(hv_DownLineParameter.TupleLength())).TupleGreater(
                    0))).TupleAnd(new HTuple((new HTuple(hv_UpLineParameter.TupleLength())).TupleGreater(
                    0)))) != 0)
                {
                    //
                    HOperatorSet.DistanceLc(ho_DownLineContour, hv_UpLineParameter.TupleSelect(
                        0), hv_UpLineParameter.TupleSelect(1), hv_UpLineParameter.TupleSelect(
                        2), hv_UpLineParameter.TupleSelect(3), out hv_DistanceMin, out hv_DistanceMax);
                    hv_ResultMaxDistance = hv_DistanceMax * hv_RatioPixel_d;
                    hv_ResultMinDistance = hv_DistanceMin * hv_RatioPixel_d;
                    hv_ResultDistance = ((1.0 * (hv_ResultMaxDistance + hv_ResultMinDistance)) / 2) + hv_BC_Distance;
                    HOperatorSet.AngleLl(hv_DownLineParameter.TupleSelect(0), hv_DownLineParameter.TupleSelect(
                        1), hv_DownLineParameter.TupleSelect(2), hv_DownLineParameter.TupleSelect(
                        3), hv_Line2Parameter.TupleSelect(0), hv_Line2Parameter.TupleSelect(1),
                        hv_Line2Parameter.TupleSelect(2), hv_Line2Parameter.TupleSelect(3), out hv_Angle);
                    //angle_ll (DownLineParameter[0], DownLineParameter[1], DownLineParameter[2], DownLineParameter[3], UpLineParameter[0], UpLineParameter[1], UpLineParameter[2], UpLineParameter[3], Angle)
                    HOperatorSet.TupleDeg(hv_Angle, out hv_ResultAngle);
                    HOperatorSet.TupleAbs(hv_ResultAngle, out hv_ResultAngle);
                    hv_ResultAngle = hv_ResultAngle + hv_BC_Angle;
                    //
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[7] = hv_ResultDistance;
                    if (hv_ResultValue == null)
                        hv_ResultValue = new HTuple();
                    hv_ResultValue[8] = hv_ResultAngle;
                }
                //
                //判断检测结果
                if ((int)((new HTuple((new HTuple((new HTuple((new HTuple((new HTuple((new HTuple(hv_ResultC1.TupleLength()
                    )).TupleGreater(0))).TupleAnd(new HTuple((new HTuple(hv_ResultC2.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultC3.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultRec.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultDistance.TupleLength()
                    )).TupleGreater(0))))).TupleAnd(new HTuple((new HTuple(hv_ResultAngle.TupleLength()
                    )).TupleGreater(0)))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter1.TupleLess(hv_GC_Circle1_0))).TupleOr(
                        new HTuple(hv_ResultDiameter1.TupleGreater(hv_GC_Circle1)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDiameter2.TupleLess(hv_GC_Circle2_0))).TupleOr(
                        new HTuple(hv_ResultDiameter2.TupleGreater(hv_GC_Circle2)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDiameter3.TupleLess(hv_GC_Circle3_0))).TupleOr(
                        new HTuple(hv_ResultDiameter3.TupleGreater(hv_GC_Circle3)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC1.TupleLess(hv_GC_RC1_0))).TupleOr(
                        new HTuple(hv_ResultC1.TupleGreater(hv_GC_RC1 + hv_deltR1)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC2.TupleLess(hv_GC_RC2_0))).TupleOr(
                        new HTuple(hv_ResultC2.TupleGreater(hv_GC_RC2 + hv_deltR2)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultC3.TupleLess(hv_GC_RC3_0))).TupleOr(
                        new HTuple(hv_ResultC3.TupleGreater(hv_GC_RC3 + hv_deltR3)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultRec.TupleLess(hv_GC_Rec_0))).TupleOr(
                        new HTuple(hv_ResultRec.TupleGreater(hv_GC_Rec)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultDistance.TupleLess(hv_GC_Distance_0))).TupleOr(
                        new HTuple(hv_ResultDistance.TupleGreater(hv_GC_Distance)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else if ((int)((new HTuple(hv_ResultAngle.TupleLess(hv_GC_Angle_0))).TupleOr(
                        new HTuple(hv_ResultAngle.TupleGreater(hv_GC_Angle)))) != 0)
                    {
                        hv_Result = 0;
                    }
                    else
                    {
                        hv_Result = 1;
                    }
                }
                else
                {
                    hv_Result = 0;
                }
                //
                //将显示对象添加到结果中
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ModelContours, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleMaxContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleOneContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleTwoContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_CircleThreeContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_RectangleContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_DownLineContour, out ExpTmpOutVar_0
                        );
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_UpLineContour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_LineContour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_Line2Contour, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ContourX, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ResultObj, ho_ContourY, out ExpTmpOutVar_0);
                    ho_ResultObj.Dispose();
                    ho_ResultObj = ExpTmpOutVar_0;
                }

                //显示结果
                HOperatorSet.ClearWindow(hv_WindowHandle);
                HOperatorSet.DispObj(ho_RotateImage, hv_WindowHandle);
                HOperatorSet.DispObj(ho_ResultObj, hv_WindowHandle);

                //
                HOperatorSet.GetImageSize(ho_RotateImage, out hv_Width, out hv_Height);
                HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "invisible", "", out hv_DumpWindowHandle);
                HOperatorSet.SetDraw(hv_DumpWindowHandle, "margin");
                HOperatorSet.SetColor(hv_DumpWindowHandle, "red");
                HOperatorSet.SetLineWidth(hv_DumpWindowHandle, 5);
                HOperatorSet.ClearWindow(hv_DumpWindowHandle);
                HOperatorSet.DispObj(ho_RotateImage, hv_DumpWindowHandle);
                HOperatorSet.DispObj(ho_ResultObj, hv_DumpWindowHandle);

                //
                //打印检测结果
                set_display_font(hv_WindowHandle, 15, "mono", "true", "false");
                set_display_font(hv_DumpWindowHandle, 120, "mono", "true", "false");
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter1.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter1.TupleLess(hv_GC_Circle1_0))).TupleOr(
                        new HTuple(hv_ResultDiameter1.TupleGreater(hv_GC_Circle1)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-17);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆1直径:" + (hv_ResultDiameter1.TupleString(
                            ".3f")), "image", 50, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆1直径:测量失败", "image", 50, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆1直径:测量失败", "image", 50, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-17);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter2.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter2.TupleLess(hv_GC_Circle2_0))).TupleOr(
                        new HTuple(hv_ResultDiameter2.TupleGreater(hv_GC_Circle2)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-12);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆2直径:" + (hv_ResultDiameter2.TupleString(
                            ".3f")), "image", 200, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆2直径:测量失败", "image", 200, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆2直径:测量失败", "image", 200, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-12);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDiameter3.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDiameter3.TupleLess(hv_GC_Circle3_0))).TupleOr(
                        new HTuple(hv_ResultDiameter3.TupleGreater(hv_GC_Circle3)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-14);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆3直径:" + (hv_ResultDiameter3.TupleString(
                            ".3f")), "image", 350, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆3直径:测量失败", "image", 350, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆3直径:测量失败", "image", 350, 10, "red",
                        "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-14);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC1.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC1.TupleLess(hv_GC_RC1_0))).TupleOr(new HTuple(hv_ResultC1.TupleGreater(
                        hv_GC_RC1 + hv_deltR1)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(".3f")),
                            "image", 500, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(
                            ".3f")), "image", 500, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-20);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(".3f")),
                            "image", 500, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆1位置度:" + (hv_ResultC1.TupleString(
                            ".3f")), "image", 500, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆1位置度:测量失败", "image", 500, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆1位置度:测量失败", "image", 500, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-20);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC2.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC2.TupleLess(hv_GC_RC2_0))).TupleOr(new HTuple(hv_ResultC2.TupleGreater(
                        hv_GC_RC2 + hv_deltR2)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(".3f")),
                            "image", 650, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(
                            ".3f")), "image", 650, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-18);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(".3f")),
                            "image", 650, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆2位置度:" + (hv_ResultC2.TupleString(
                            ".3f")), "image", 650, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                    //
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆2位置度:测量失败", "image", 650, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆2位置度:测量失败", "image", 650, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-18);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultC3.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultC3.TupleLess(hv_GC_RC3_0))).TupleOr(new HTuple(hv_ResultC3.TupleGreater(
                        hv_GC_RC3 + hv_deltR3)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(".3f")),
                            "image", 800, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(
                            ".3f")), "image", 800, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-19);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(".3f")),
                            "image", 800, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "圆3位置度:" + (hv_ResultC3.TupleString(
                            ".3f")), "image", 800, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "圆3位置度:测量失败", "image", 800, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "圆3位置度:测量失败", "image", 800, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-19);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultRec.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultRec.TupleLess(hv_GC_Rec_0))).TupleOr(new HTuple(hv_ResultRec.TupleGreater(
                        hv_GC_Rec)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(".3f")),
                            "image", 950, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(
                            ".3f")), "image", 950, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-3);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(".3f")),
                            "image", 950, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "插头圆心距:" + (hv_ResultRec.TupleString(
                            ".3f")), "image", 950, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "插头圆心距:测量失败", "image", 950, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "插头圆心距:测量失败", "image", 950, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-3);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultDistance.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultDistance.TupleLess(hv_GC_Distance_0))).TupleOr(
                        new HTuple(hv_ResultDistance.TupleGreater(hv_GC_Distance)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-13);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "FPC间距:" + (hv_ResultDistance.TupleString(
                            ".3f")), "image", 1100, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "FPC间距:测量失败", "image", 1100, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "FPC间距:测量失败", "image", 1100, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-13);";
                }
                //
                if ((int)(new HTuple((new HTuple(hv_ResultAngle.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    if ((int)((new HTuple(hv_ResultAngle.TupleLess(hv_GC_Angle_0))).TupleOr(new HTuple(hv_ResultAngle.TupleGreater(
                        hv_GC_Angle)))) != 0)
                    {
                        disp_message(hv_WindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(".3f")),
                            "image", 1250, 10, "red", "false");
                        disp_message(hv_DumpWindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(
                            ".3f")), "image", 1250, 10, "red", "false");
                        hv_ResultInfo = hv_ResultInfo + "(FAI-11);";
                    }
                    else
                    {
                        disp_message(hv_WindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(".3f")),
                            "image", 1250, 10, "green", "false");
                        disp_message(hv_DumpWindowHandle, "FPC角度:" + (hv_ResultAngle.TupleString(
                            ".3f")), "image", 1250, 10, "green", "false");
                        hv_ResultInfo = hv_ResultInfo.Clone();
                    }
                }
                else
                {
                    disp_message(hv_WindowHandle, "FPC角度:测量失败", "image", 1250, 10, "red",
                        "false");
                    disp_message(hv_DumpWindowHandle, "FPC角度:测量失败", "image", 1250, 10,
                        "red", "false");
                    hv_ResultInfo = hv_ResultInfo + "(FAI-11);";
                }
                //
                //
                if ((int)(new HTuple((new HTuple(hv_ResultMaxDiameter.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, "大圆X:" + (hv_ResultMaxX.TupleString(".3f")),
                        "image", 1400, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "大圆X:" + (hv_ResultMaxX.TupleString(".3f")),
                        "image", 1400, 10, "green", "false");

                    disp_message(hv_WindowHandle, "大圆Y:" + (hv_ResultMaxY.TupleString(".3f")),
                        "image", 1550, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "大圆Y:" + (hv_ResultMaxY.TupleString(".3f")),
                        "image", 1550, 10, "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter1.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆1X:" + (hv_ResultMax1X.TupleString(".3f"))) + "-") + (hv_ResultMax1X1.TupleString(
                        ".3f")), "image", 1700, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆1X:" + (hv_ResultMax1X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax1X1.TupleString(".3f")), "image", 1700, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆1Y:" + (hv_ResultMax1Y.TupleString(".3f"))) + "-") + (hv_ResultMax1Y1.TupleString(
                        ".3f")), "image", 1850, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆1Y:" + (hv_ResultMax1Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax1Y1.TupleString(".3f")), "image", 1850, 10,
                        "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter2.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆2X:" + (hv_ResultMax2X.TupleString(".3f"))) + "-") + (hv_ResultMax2X1.TupleString(
                        ".3f")), "image", 2000, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆2X:" + (hv_ResultMax2X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax2X1.TupleString(".3f")), "image", 2000, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆2Y:" + (hv_ResultMax2Y.TupleString(".3f"))) + "-") + (hv_ResultMax2Y1.TupleString(
                        ".3f")), "image", 2150, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆2Y:" + (hv_ResultMax2Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax2Y1.TupleString(".3f")), "image", 2150, 10,
                        "green", "false");
                }

                if ((int)(new HTuple((new HTuple(hv_ResultDiameter3.TupleLength())).TupleGreater(
                    0))) != 0)
                {
                    disp_message(hv_WindowHandle, (("小圆3X:" + (hv_ResultMax3X.TupleString(".3f"))) + "-") + (hv_ResultMax3X1.TupleString(
                        ".3f")), "image", 2300, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆3X:" + (hv_ResultMax3X.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax3X1.TupleString(".3f")), "image", 2300, 10,
                        "green", "false");

                    disp_message(hv_WindowHandle, (("小圆3Y:" + (hv_ResultMax3Y.TupleString(".3f"))) + "-") + (hv_ResultMax3Y1.TupleString(
                        ".3f")), "image", 2450, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, (("小圆3Y:" + (hv_ResultMax3Y.TupleString(
                        ".3f"))) + "-") + (hv_ResultMax3Y1.TupleString(".3f")), "image", 2450, 10,
                        "green", "false");
                }

                //

                set_display_font(hv_WindowHandle, 40, "mono", "true", "false");
                set_display_font(hv_DumpWindowHandle, 320, "mono", "true", "false");
                //
                if ((int)(new HTuple(hv_Result.TupleEqual(0))) != 0)
                {
                    disp_message(hv_WindowHandle, "产品:NG", "image", 2550, 10, "red", "false");
                    disp_message(hv_DumpWindowHandle, "产品:NG", "image", 2550, 10, "red", "false");
                }
                else
                {
                    disp_message(hv_WindowHandle, "产品:OK", "image", 2550, 10, "green", "false");
                    disp_message(hv_DumpWindowHandle, "产品:OK", "image", 2550, 10, "green",
                        "false");
                }
                //
                ho_ResultImage.Dispose();
                HOperatorSet.DumpWindowImage(out ho_ResultImage, hv_DumpWindowHandle);
                HOperatorSet.CloseWindow(hv_DumpWindowHandle);

                //
                ho_ModelContours.Dispose();
                ho_CircleMaxContour.Dispose();
                ho_CircleOneContour.Dispose();
                ho_CircleTwoContour.Dispose();
                ho_CircleThreeContour.Dispose();
                ho_RectangleContour.Dispose();
                ho_DownLineContour.Dispose();
                ho_UpLineContour.Dispose();
                ho_LineContour.Dispose();
                ho_Line2Contour.Dispose();
                ho_ContourX.Dispose();
                ho_ContourY.Dispose();
                ho_LineContours.Dispose();
                ho_RotateImage.Dispose();
                ho_CircleMaxContours.Dispose();
                ho_CircleOneContours.Dispose();
                ho_CircleTwoContours.Dispose();
                ho_CircleThreeContours.Dispose();
                ho_RectangleContours.Dispose();
                ho_DownLineContours.Dispose();
                ho_UpLineContours.Dispose();
                ho_Line2Contours.Dispose();

                return;
            }
            catch
            {
                ho_ModelContours.Dispose();
                ho_CircleMaxContour.Dispose();
                ho_CircleOneContour.Dispose();
                ho_CircleTwoContour.Dispose();
                ho_CircleThreeContour.Dispose();
                ho_RectangleContour.Dispose();
                ho_DownLineContour.Dispose();
                ho_UpLineContour.Dispose();
                ho_LineContour.Dispose();
                ho_Line2Contour.Dispose();
                ho_ContourX.Dispose();
                ho_ContourY.Dispose();
                ho_LineContours.Dispose();
                ho_RotateImage.Dispose();
                ho_CircleMaxContours.Dispose();
                ho_CircleOneContours.Dispose();
                ho_CircleTwoContours.Dispose();
                ho_CircleThreeContours.Dispose();
                ho_RectangleContours.Dispose();
                ho_DownLineContours.Dispose();
                ho_UpLineContours.Dispose();
                ho_Line2Contours.Dispose();

                return;
            }
        }

        #endregion

        public void set_display_font(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font,
      HTuple hv_Bold, HTuple hv_Slant)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_OS = null, hv_Fonts = new HTuple();
            HTuple hv_Style = null, hv_Exception = new HTuple(), hv_AvailableFonts = null;
            HTuple hv_Fdx = null, hv_Indices = new HTuple();
            HTuple hv_Font_COPY_INP_TMP = hv_Font.Clone();
            HTuple hv_Size_COPY_INP_TMP = hv_Size.Clone();

            // Initialize local and output iconic variables 
            //This procedure sets the text font of the current window with
            //the specified attributes.
            //
            //Input parameters:
            //WindowHandle: The graphics window for which the font will be set
            //Size: The font size. If Size=-1, the default of 16 is used.
            //Bold: If set to 'true', a bold font is used
            //Slant: If set to 'true', a slanted font is used
            //
            HOperatorSet.GetSystem("operating_system", out hv_OS);
            // dev_get_preferences(...); only in hdevelop
            // dev_set_preferences(...); only in hdevelop
            if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
            {
                hv_Size_COPY_INP_TMP = 16;
            }
            if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
            {
                //Restore previous behaviour
                hv_Size_COPY_INP_TMP = ((1.13677 * hv_Size_COPY_INP_TMP)).TupleInt();
            }
            if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))) != 0)
            {
                hv_Fonts = new HTuple();
                hv_Fonts[0] = "Courier";
                hv_Fonts[1] = "Courier 10 Pitch";
                hv_Fonts[2] = "Courier New";
                hv_Fonts[3] = "CourierNew";
            }
            else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
            {
                hv_Fonts = new HTuple();
                hv_Fonts[0] = "Consolas";
                hv_Fonts[1] = "Menlo";
                hv_Fonts[2] = "Courier";
                hv_Fonts[3] = "Courier 10 Pitch";
                hv_Fonts[4] = "FreeMono";
            }
            else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
            {
                hv_Fonts = new HTuple();
                hv_Fonts[0] = "Luxi Sans";
                hv_Fonts[1] = "DejaVu Sans";
                hv_Fonts[2] = "FreeSans";
                hv_Fonts[3] = "Arial";
            }
            else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
            {
                hv_Fonts = new HTuple();
                hv_Fonts[0] = "Times New Roman";
                hv_Fonts[1] = "Luxi Serif";
                hv_Fonts[2] = "DejaVu Serif";
                hv_Fonts[3] = "FreeSerif";
                hv_Fonts[4] = "Utopia";
            }
            else
            {
                hv_Fonts = hv_Font_COPY_INP_TMP.Clone();
            }
            hv_Style = "";
            if ((int)(new HTuple(hv_Bold.TupleEqual("true"))) != 0)
            {
                hv_Style = hv_Style + "Bold";
            }
            else if ((int)(new HTuple(hv_Bold.TupleNotEqual("false"))) != 0)
            {
                hv_Exception = "Wrong value of control parameter Bold";
                throw new HalconException(hv_Exception);
            }
            if ((int)(new HTuple(hv_Slant.TupleEqual("true"))) != 0)
            {
                hv_Style = hv_Style + "Italic";
            }
            else if ((int)(new HTuple(hv_Slant.TupleNotEqual("false"))) != 0)
            {
                hv_Exception = "Wrong value of control parameter Slant";
                throw new HalconException(hv_Exception);
            }
            if ((int)(new HTuple(hv_Style.TupleEqual(""))) != 0)
            {
                hv_Style = "Normal";
            }
            HOperatorSet.QueryFont(hv_WindowHandle, out hv_AvailableFonts);
            hv_Font_COPY_INP_TMP = "";
            for (hv_Fdx = 0; (int)hv_Fdx <= (int)((new HTuple(hv_Fonts.TupleLength())) - 1); hv_Fdx = (int)hv_Fdx + 1)
            {
                hv_Indices = hv_AvailableFonts.TupleFind(hv_Fonts.TupleSelect(hv_Fdx));
                if ((int)(new HTuple((new HTuple(hv_Indices.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)(new HTuple(((hv_Indices.TupleSelect(0))).TupleGreaterEqual(0))) != 0)
                    {
                        hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(hv_Fdx);
                        break;
                    }
                }
            }
            if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(""))) != 0)
            {
                throw new HalconException("Wrong value of control parameter Font");
            }
            hv_Font_COPY_INP_TMP = (((hv_Font_COPY_INP_TMP + "-") + hv_Style) + "-") + hv_Size_COPY_INP_TMP;
            HOperatorSet.SetFont(hv_WindowHandle, hv_Font_COPY_INP_TMP);
            // dev_set_preferences(...); only in hdevelop

            return;
        }

        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
      HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_GenParamName = null, hv_GenParamValue = null;
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_CoordSystem_COPY_INP_TMP = hv_CoordSystem.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();

            // Initialize local and output iconic variables 
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Column: The column coordinate of the desired text position
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically...
            //   - if |Row| == |Column| == 1: for each new textline
            //   = else for each text position.
            //Box: If Box[0] is set to 'true', the text is written within an orange box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow
            //       otherwise -> use given string as color string for the shadow color
            //
            //It is possible to display multiple text strings in a single call.
            //In this case, some restrictions apply:
            //- Multiple text positions can be defined by specifying a tuple
            //  with multiple Row and/or Column coordinates, i.e.:
            //  - |Row| == n, |Column| == n
            //  - |Row| == n, |Column| == 1
            //  - |Row| == 1, |Column| == n
            //- If |Row| == |Column| == 1,
            //  each element of String is display in a new textline.
            //- If multiple positions or specified, the number of Strings
            //  must match the number of positions, i.e.:
            //  - Either |String| == n (each string is displayed at the
            //                          corresponding position),
            //  - or     |String| == 1 (The string is displayed n times).
            //
            //
            //Convert the parameters for disp_text.
            if ((int)((new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(new HTuple())))) != 0)
            {

                return;
            }
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            //
            //Convert the parameter Box to generic parameters.
            hv_GenParamName = new HTuple();
            hv_GenParamValue = new HTuple();
            if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(0))) != 0)
            {
                if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleEqual("false"))) != 0)
                {
                    //Display no box
                    hv_GenParamName = hv_GenParamName.TupleConcat("box");
                    hv_GenParamValue = hv_GenParamValue.TupleConcat("false");
                }
                else if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleNotEqual("true"))) != 0)
                {
                    //Set a color other than the default.
                    hv_GenParamName = hv_GenParamName.TupleConcat("box_color");
                    hv_GenParamValue = hv_GenParamValue.TupleConcat(hv_Box.TupleSelect(0));
                }
            }
            if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleEqual("false"))) != 0)
                {
                    //Display no shadow.
                    hv_GenParamName = hv_GenParamName.TupleConcat("shadow");
                    hv_GenParamValue = hv_GenParamValue.TupleConcat("false");
                }
                else if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleNotEqual("true"))) != 0)
                {
                    //Set a shadow color other than the default.
                    hv_GenParamName = hv_GenParamName.TupleConcat("shadow_color");
                    hv_GenParamValue = hv_GenParamValue.TupleConcat(hv_Box.TupleSelect(1));
                }
            }
            //Restore default CoordSystem behavior.
            if ((int)(new HTuple(hv_CoordSystem_COPY_INP_TMP.TupleNotEqual("window"))) != 0)
            {
                hv_CoordSystem_COPY_INP_TMP = "image";
            }
            //
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(""))) != 0)
            {
                //disp_text does not accept an empty string for Color.
                hv_Color_COPY_INP_TMP = new HTuple();
            }
            //
            HOperatorSet.DispText(hv_WindowHandle, hv_String, hv_CoordSystem_COPY_INP_TMP,
                hv_Row_COPY_INP_TMP, hv_Column_COPY_INP_TMP, hv_Color_COPY_INP_TMP, hv_GenParamName,
                hv_GenParamValue);

            return;
        }

        #endregion
       

    }
}
