using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsTagPrint
{
    /// <summary>
    /// 维修管理-宇通旧件标签打印
    /// Author：JC
    /// AddTime：2014.11.05
    /// </summary>
    public partial class UCYTOldPartsTagPrintManager : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 服务单号
        /// </summary>
        string strServerNo = string.Empty;
        /// <summary>
        /// 配件Id
        /// </summary>
        string strPartsId = string.Empty;
        #endregion
        public UCYTOldPartsTagPrintManager()
        {
            InitializeComponent();           
            base.ImportEvent += new ClickHandler(UCYTOldPartsTagPrintManager_ImportEvent);
        }

        #region 导入事件
        void UCYTOldPartsTagPrintManager_ImportEvent(object sender, EventArgs e)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            ShowContextMenuStrip(x, y);
        }
        /// <summary> 点击导入按钮，显示导入方式的下拉选项
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void ShowContextMenuStrip(int X, int Y)
        {
            contextMenuM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            contextMenuM.Show();
            contextMenuM.Location = new System.Drawing.Point(X, Y);
        }
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnAdd.Visible = false;
            base.btnCopy.Visible = false;
            base.btnEdit.Visible = false;
            base.btnDelete.Visible = false;
            base.btnStatus.Visible = false;
            base.btnVerify.Visible = false;
            base.btnSave.Visible = false;           
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnSubmit.Visible = false;
            base.btnCancel.Visible = false;
            base.btnExport.Visible = false;          
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 宇通旧件返厂单导入功能
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UCYTOldPartsImport PartsImport = new UCYTOldPartsImport();
            DialogResult result = PartsImport.ShowDialog();
            if (result == DialogResult.OK)
            {
                strServerNo = PartsImport.strServersNo;
                strPartsId = PartsImport.strPartsId;
                BindCoValue();        
            }
        }
        #endregion

        #region 绑定控件的值
        private void BindCoValue()
        {
            strServerNo = strServerNo.Substring(0, strServerNo.Length - 1);
            strPartsId = strPartsId.Substring(0, strPartsId.Length - 1);
            string[] ArrayId = strPartsId.Split(',');
            string[] ArrayNo = strServerNo.Split(',');
            if (ArrayNo.Length > 0)
            {
                //labTotalS.Text = ArrayNo.Length.ToString();
                //labTotalPageS.Text = (ArrayNo.Length / 4 + 1).ToString();
                int count = ArrayNo.Length;
                switch (count)
                {
                    case 1:
                        ucOldPartsTagTL.BindServiceOrderInfo(ArrayNo[0], ArrayId[0]);
                        break;
                    case 2:
                        ucOldPartsTagTL.BindServiceOrderInfo(ArrayNo[0], ArrayId[0]);
                        ucOldPartsTagTR.BindServiceOrderInfo(ArrayNo[1], ArrayId[1]);
                        break;
                    case 3:
                        ucOldPartsTagTL.BindServiceOrderInfo(ArrayNo[0], ArrayId[0]);
                        ucOldPartsTagTR.BindServiceOrderInfo(ArrayNo[1], ArrayId[1]);
                        ucOldPartsTagBL.BindServiceOrderInfo(ArrayNo[2], ArrayId[2]);
                        break;
                    case 4:
                        ucOldPartsTagTL.BindServiceOrderInfo(ArrayNo[0], ArrayId[0]);
                        ucOldPartsTagTR.BindServiceOrderInfo(ArrayNo[1], ArrayId[1]);
                        ucOldPartsTagBL.BindServiceOrderInfo(ArrayNo[2], ArrayId[2]);
                        ucOldPartsTagBR.BindServiceOrderInfo(ArrayNo[3], ArrayId[3]);
                        break;
                }
            }
        }
        #endregion

        #region 窗体Load事件
        private void UCYTOldPartsTagPrintManager_Load(object sender, EventArgs e)
        {
            SetTopbuttonShow();
        }
        #endregion

    }
}
