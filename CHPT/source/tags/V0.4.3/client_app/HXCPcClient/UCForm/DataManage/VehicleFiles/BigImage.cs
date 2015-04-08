using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient.UCForm.DataManage.VehicleFiles
{
    /// <summary>
    /// 数据管理-图片放大
    /// Author：JC
    /// AddTime：2014.12.25
    /// </summary>
    public partial class BigImage : Form
    {
        public BigImage(Image img)
        {
            InitializeComponent();
            //Image image = new Bitmap(strImageUrl);
            pibBigImage.Image = img;
        }
    }
}
