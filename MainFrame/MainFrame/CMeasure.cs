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


namespace MainFrame
{
    public partial class CMeasure : Form
    {
        
        #region 非参数变量
        
        private HObject m_ho_Image;
        private HObject m_ho_Region;
        
        public HTuple m_hv_AcqHandle = new HTuple();
        private HTuple m_hv_hWindowControlID = new HTuple();
        private HTuple m_hv_Row1 = new HTuple(), m_hv_Column1 = new HTuple(), m_hv_Row2 = new HTuple(), m_hv_Column2 = new HTuple();
        
        private double m_dRatio = 1.0;
        private double m_dPreviousX = 0.0, m_dPreviousY = 0.0;
        
        private bool m_bPressDownLeft = false;
        private bool m_bInteract = false;
        private bool m_bContinusGrab = false;
        
        private int m_iOriFormWidth = 0;
        private int m_iOriFormHeight = 0;
        public int m_iListViewIndex = 0;
        
        private string[] m_strEnableControl;
        private System.Threading.Timer m_Timer = null;
        private System.Threading.TimerCallback m_callback = null;
        private delegate void DelegateStr(params object[] args);
        private DelegateStr m_DelegateTimer = null;

	    #endregion

        #region 参数变量

        public List<MLine> m_listMLine = new List<MLine>();

        #endregion

        public CMeasure()
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

                #region 初始化参数变量

                m_listMLine.Clear();

                #endregion

                //变量初始化
                HOperatorSet.GenEmptyObj(out m_ho_Image);
                HOperatorSet.GenEmptyObj(out m_ho_Region);
                m_hv_hWindowControlID = hWindowControl.HalconID;
                
                //初始化系统参数
                HOperatorSet.SetSystem("clip_region", "false");
                HOperatorSet.SetColor(m_hv_hWindowControlID, "red");               

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void CMeasure_Load(object sender, EventArgs e)
        {
            try
            {
                listView.Items.Clear();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("CClibration_Load:" + ex.Message);
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

        private void FlushWindow(HTuple hWindowControlID,int Index=0)
        {
            try
            {
                switch (Index)
                {
                    case 0:

                        #region 显示原图

                        if (m_ho_Image.CountObj() != 0)
                        {
                            HOperatorSet.ClearWindow(hWindowControlID);
                            HOperatorSet.DispObj(m_ho_Image, hWindowControlID);
                        }

                        #endregion

                        break;
                    case 1:

                        #region 显示原图不刷新图形

                        if (m_ho_Image.CountObj() != 0)
                        {
                            HOperatorSet.DispObj(m_ho_Image, hWindowControlID);
                        }
                        #endregion

                        break;
                    case 2:

                        #region 显示区域

                        if (m_ho_Image.CountObj() != 0)
                        {
                            HOperatorSet.ClearWindow(hWindowControlID);
                            HOperatorSet.DispObj(m_ho_Image, hWindowControlID);
                        }
                        if (m_ho_Region.CountObj() != 0)
                        {
                            HOperatorSet.DispObj(m_ho_Region, hWindowControlID);
                        }


                        #endregion

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

        private void EnableControl(bool bEnable)
        {
            try
            {
                int count = m_strEnableControl.Length;
                for (int i = 0; i < count;i++ )
                {
                    Control[] controls = Controls.Find(m_strEnableControl[i], true);
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
        
        private void btnReadImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "所有文件|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    m_ho_Image.Dispose();
                    HOperatorSet.ReadImage(out m_ho_Image, dlg.FileName);
                    GetHWindowPart(m_ho_Image, hWindowControl.Height, hWindowControl.Width, out m_hv_Row1, out m_hv_Column1, out m_hv_Row2, out m_hv_Column2,out m_dRatio);
                    HOperatorSet.SetPart(m_hv_hWindowControlID, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);
                    FlushWindow(m_hv_hWindowControlID);
                }
                                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnReadImage_Click:"+ex.Message);
            }

        }

        private void hWindowControl_HMouseDown(object sender, HMouseEventArgs e)
        {
            try
            {
                if (m_ho_Image.IsInitialized() == false)
                {
                    return;
                }

                if ( (m_ho_Image.CountObj() == 0) || (m_bInteract == true) )
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
                    GetHWindowPart(m_ho_Image, hWindowControl.Height, hWindowControl.Width, out m_hv_Row1, out m_hv_Column1, out m_hv_Row2, out m_hv_Column2, out m_dRatio);
                    HOperatorSet.SetPart(m_hv_hWindowControlID, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);
                    FlushWindow(m_hv_hWindowControlID);
                }

            }
            catch (System.Exception ex)
            {

                MessageBox.Show("hWindowControl_HMouseDown"+ex.Message);
            }
        }

        private void hWindowControl_HMouseMove(object sender, HMouseEventArgs e)
        {
            try
            {
                if ((m_ho_Image.IsInitialized() == false))
                {
                    return;
                }
                if ( m_ho_Image.CountObj() == 0)
                {
                    return;
                }

                #region 更新图像坐标及灰度
                
                toolStripStatusLabel_X.Text = "X:" + ((int)e.X).ToString("D4");
                toolStripStatusLabel_Y.Text = "Y:" + ((int)e.Y).ToString("D4");
                HTuple grayval = new HTuple();
                try
                {
                    HOperatorSet.GetGrayval(m_ho_Image, e.Y, e.X, out grayval);
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
                    HOperatorSet.SetPart(m_hv_hWindowControlID, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);
                    FlushWindow(m_hv_hWindowControlID);

                }
#endregion


                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("hWindowControl_HMouseMove"+ex.Message);
            }
        }

        private void hWindowControl_HMouseUp(object sender, HMouseEventArgs e)
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

        private void hWindowControl_HMouseWheel(object sender, HMouseEventArgs e)
        {
            try
            {
                if (m_ho_Image.IsInitialized() == false)
                {
                    return;
                }

                if ( (m_ho_Image.CountObj() == 0) || (m_bInteract == true))
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

                    HOperatorSet.SetPart(m_hv_hWindowControlID, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);
                    FlushWindow(m_hv_hWindowControlID);

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
                    GetHWindowPart(m_ho_Image, hWindowControl.Height, hWindowControl.Width, out Row1, out Column1, out Row2, out Column2, out m_dRatio);
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
                    
                    HOperatorSet.SetPart(m_hv_hWindowControlID, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);
                    FlushWindow(m_hv_hWindowControlID);

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

                FlushWindow(m_hv_hWindowControlID);

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("CHalconTemplate_SizeChanged:" + ex.Message);
            }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_ho_Image.IsInitialized() == false)
                {
                    MessageBox.Show("没有图像!");
                    return;
                }
                
                if (m_ho_Image.CountObj() == 0)
                {
                    MessageBox.Show("没有图像!");
                    return;
                }
                SaveFileDialog dlg = new SaveFileDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    HOperatorSet.WriteImage(m_ho_Image, "tiff", 0, dlg.FileName);
                }

                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnSaveImage_Click:" + ex.Message);
            }
        }

        private void btnGrabImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_hv_AcqHandle.Length <= 0)
                {
                    MessageBox.Show("相机没有连接!");
                    return;
                }

                m_ho_Image.Dispose();
                HOperatorSet.GrabImage(out m_ho_Image, m_hv_AcqHandle);
                if (m_hv_Row1.Length == 0)
                {
                    GetHWindowPart(m_ho_Image, hWindowControl.Height, hWindowControl.Width,
                               out m_hv_Row1, out m_hv_Column1, out m_hv_Row2, out m_hv_Column2, out m_dRatio);
                    HOperatorSet.SetPart(m_hv_hWindowControlID, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);
                }
                FlushWindow(m_hv_hWindowControlID);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnGrabImage_Click:" + ex.Message);
            }
        }

        private void btnContiGrabImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_bContinusGrab == false)
                {
                    m_bContinusGrab = true;
                    m_bInteract = true;
                    btnContiGrabImage.Text = "停止采集";
                    EnableControl(false);
                    Thread grabThread = new Thread(GrabThreadRun);
                    grabThread.Start();

                } 
                else
                {
                    m_bContinusGrab = false;
                    m_bInteract = false;
                    btnContiGrabImage.Text = "连续采集";
                    EnableControl(true);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("btnContiGrabImage_Click:" + ex.Message);
            }
        }

        private void GrabThreadRun()
        {
            try
            {
                while (m_bContinusGrab)
                {
                    Thread.Sleep(10);   //很重要进入所有线程必须 Sleep(10) 否则其他消息无法响应 即鼠标移动不能显示坐标
                    m_ho_Image.Dispose();
                    HOperatorSet.GrabImage(out m_ho_Image, m_hv_AcqHandle);
                    if (m_hv_Row1.Length == 0)
                    {
                        GetHWindowPart(m_ho_Image, hWindowControl.Height, hWindowControl.Width,
                               out m_hv_Row1, out m_hv_Column1, out m_hv_Row2, out m_hv_Column2, out m_dRatio);
                        HOperatorSet.SetPart(m_hv_hWindowControlID, m_hv_Row1, m_hv_Column1, m_hv_Row2, m_hv_Column2);
                    }

                    FlushWindow(m_hv_hWindowControlID);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("GrabThreadRun:"+ex.Message);
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

        private void btnLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_ho_Image.CountObj() == 0)
                {
                    MessageBox.Show("没有图像!");
                    return;
                }

                hWindowControl.Focus();
                m_bInteract = true;

                HTuple row1, column1, row2, column2;
                HObject ho_ObjectContour, ho_MeasureContour, ho_ResultContour;
                HTuple hv_RowEdges, hv_ColumnEdges, hv_Parameter, hv_Line = new HTuple();
                HOperatorSet.DrawLineMod(m_hv_hWindowControlID, 100,100,200,200,out row1, out column1, out row2, out column2);
                MLine line = new MLine(row1, column1, row2, column2);
                hv_Line = hv_Line.TupleConcat(line.m_Row1).TupleConcat(line.m_Column1).TupleConcat(line.m_Row2).TupleConcat(line.m_Column2);
                bool bRes = FindLines(m_ho_Image, out ho_ObjectContour, out ho_MeasureContour, out ho_ResultContour, out hv_RowEdges, out hv_ColumnEdges, out hv_Parameter, hv_Line,
                                      line.m_length1, line.m_length2, line.m_distance, line.m_select, line.m_sigma,
                                      line.m_threshold, line.m_transition, line.m_score, line.m_instance);
                if (bRes == false)
                {
                    MessageBox.Show("直线测量失败!");
                }
                else
                {
                    m_listMLine.Add(line);
                }

                UpdateListView();

                HOperatorSet.SetColor(m_hv_hWindowControlID, "red");
                HOperatorSet.DispObj(ho_MeasureContour, m_hv_hWindowControlID);
                HOperatorSet.DispObj(ho_ResultContour, m_hv_hWindowControlID);

                m_bInteract = false;
            }
            catch (System.Exception ex)
            {
                m_bInteract = false;
                MessageBox.Show("btnLine_Click:" + ex.Message);                
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

        private void UpdateListView()
        {
            try
            {
                listView.Items.Clear();

                #region 更新直线工具列表
                
                for (int i = 0; i < m_listMLine.Count;i++ )
                {
                    ListViewItem value = new ListViewItem();
                    value.Text = (i + 1).ToString();
                    value.SubItems.Add("直线");
                    listView.Items.Add(value);
                }
                
                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdateListView:" + ex.Message);
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedIndices.Count != 0)
                {

                    ListViewItem value = listView.FocusedItem;
                    if (value.SubItems[1].Text == "直线")
                    {
                        MessageBox.Show("list");
                        UpdateLine(int.Parse(value.SubItems[0].Text));
                    }
                    
                }

                UpdateListView();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("listView_SelectedIndexChanged:" + ex.Message);
            }
        }

        private void UpdateLine(int index)
        {
            try
            {
                if (m_ho_Image.CountObj() == 0)
                {
                    MessageBox.Show("没有图像!");
                    return;
                }

                FlushWindow(m_hv_hWindowControlID);
                HOperatorSet.SetColor(m_hv_hWindowControlID, "green");

                hWindowControl.Focus();
                m_bInteract = true;

                HTuple row1, column1, row2, column2;
                HObject ho_ObjectContour, ho_MeasureContour, ho_ResultContour;
                HTuple hv_RowEdges, hv_ColumnEdges, hv_Parameter, hv_Line = new HTuple();

                MLine line = m_listMLine[index - 1];
                HOperatorSet.DrawLineMod(m_hv_hWindowControlID, line.m_Row1, line.m_Column1, line.m_Row2, line.m_Column2, out row1, out column1, out row2, out column2);
                hv_Line = hv_Line.TupleConcat(row1).TupleConcat(column1).TupleConcat(row2).TupleConcat(column2);
                bool bRes = FindLines(m_ho_Image, out ho_ObjectContour, out ho_MeasureContour, out ho_ResultContour, out hv_RowEdges, out hv_ColumnEdges, out hv_Parameter, hv_Line,
                                      line.m_length1, line.m_length2, line.m_distance, line.m_select, line.m_sigma,
                                      line.m_threshold, line.m_transition, line.m_score, line.m_instance);

                if (bRes == false)
                {
                    MessageBox.Show("直线测量失败!");
                }
                else
                {
                    line.m_Row1 = row1;
                    line.m_Column1 = column1;
                    line.m_Row2 = row2;
                    line.m_Column2 = column2;
                    m_listMLine[index - 1] = line;
                }

                

                HOperatorSet.SetColor(m_hv_hWindowControlID, "red");
                HOperatorSet.DispObj(ho_MeasureContour, m_hv_hWindowControlID);
                HOperatorSet.DispObj(ho_ResultContour, m_hv_hWindowControlID);

                m_bInteract = false;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("UpdateLine:" + ex.Message);
                m_bInteract = false;
            }
        }

        #region Halcon测量函数

        public bool FindLines(HObject ho_Image,
                              out HObject ho_ObjectContour, out HObject ho_MeasureContour,
                              out HObject ho_ResultContour,
                              out HTuple hv_RowEdges, out HTuple hv_ColumnEdges,
                              out HTuple hv_Parameter,
                              HTuple hv_Line,
                              double hv_length1 = 20, double hv_length2 = 5,
                              double hv_distance = 10, string hv_select = "all",
                              double hv_sigma = 1, double hv_threshold = 30,
                              string hv_transition = "all", double hv_score = 0.5,
                              int hv_instances = 1
                             )
        {

            HTuple hv_MetrologyHandle = new HTuple(), hv_Width = null;
            HTuple hv_Height = null, hv_Index = null;

            try
            {
                // Initialize local and output iconic variables 
                HOperatorSet.GenEmptyObj(out ho_ObjectContour);
                HOperatorSet.GenEmptyObj(out ho_MeasureContour);
                HOperatorSet.GenEmptyObj(out ho_ResultContour);
                HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandle, hv_Width, hv_Height);

                HOperatorSet.AddMetrologyObjectGeneric(hv_MetrologyHandle, "line", hv_Line, 20,
                    5, 1, 30, new HTuple(), new HTuple(), out hv_Index);
                ho_ObjectContour.Dispose();
                HOperatorSet.GetMetrologyObjectModelContour(out ho_ObjectContour, hv_MetrologyHandle,
                    "all", 1.5);

                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_length1",
                    hv_length1);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_length2",
                    hv_length2);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_distance",
                    hv_distance);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_select",
                    hv_select);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_sigma",
                    hv_sigma);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_threshold",
                    hv_threshold);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_transition",
                    hv_transition);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "min_score",
                    hv_score);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "num_instances",
                    hv_instances);

                HOperatorSet.ApplyMetrologyModel(ho_Image, hv_MetrologyHandle);
                ho_MeasureContour.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_MeasureContour, hv_MetrologyHandle,
                    "all", "all", out hv_RowEdges, out hv_ColumnEdges);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, "all", "all", "result_type",
                    "all_param", out hv_Parameter);
                ho_ResultContour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_ResultContour, hv_MetrologyHandle,
                    "all", "all", 1.5);
                HOperatorSet.ClearMetrologyModel(hv_MetrologyHandle);

                if (hv_Parameter.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                if (hv_MetrologyHandle.Length > 0)
                {
                    HOperatorSet.ClearMetrologyModel(hv_MetrologyHandle);
                }

                HOperatorSet.GenEmptyObj(out ho_ObjectContour);
                HOperatorSet.GenEmptyObj(out ho_MeasureContour);
                HOperatorSet.GenEmptyObj(out ho_ResultContour);
                hv_RowEdges = new HTuple();
                hv_ColumnEdges = new HTuple();
                hv_Parameter = new HTuple();

                return false;
            }


        }

        #endregion

        #region 测量工具定义

        public struct MLine
        {
            public MLine(double Row1, double Column1, double Row2, double Column2,
                         double length1 = 20, double length2 = 5, double distance = 10,
                         string select = "all", double sigma = 1, double threshold = 30,
                         string transition = "all", double score = 0.5, int instance = 1
                        )
            {
                this.m_Row1 = Row1;
                this.m_Column1 = Column1;
                this.m_Row2 = Row2;
                this.m_Column2 = Column2;
                this.m_length1 = length1;
                this.m_length2 = length2;
                this.m_distance = distance;
                this.m_select = select;
                this.m_sigma = sigma;
                this.m_threshold = threshold;
                this.m_transition = transition;
                this.m_score = score;
                this.m_instance = instance;

            }

            public double m_Row1;
            public double m_Column1;
            public double m_Row2;
            public double m_Column2;
            public double m_length1;
            public double m_length2;
            public double m_distance;
            public string m_select;
            public double m_sigma;
            public double m_threshold;
            public string m_transition;
            public double m_score;
            public int m_instance;

        }

        #endregion

    }
}
