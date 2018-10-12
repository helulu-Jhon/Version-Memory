using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainFrame
{
    [Serializable]
    public class CSysSerialize
    {
        #region 软件参数变量

        public string m_sImageSavePath = System.Environment.CurrentDirectory+"\\ImageData";
        public int m_iDeleteImageTime = 3;
        public bool m_bIsSaveOKImage = false;
        public int m_iSysMode = 1;
        public string m_sFileSavePath = System.Environment.CurrentDirectory + "\\ResultData";

        #endregion
    }
}
