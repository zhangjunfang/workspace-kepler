using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{
    public partial class OpButton : UserControl
    {
        public delegate void BtnClickDeleate(object sender, EventArgs e);

        [Description("新增"), Category("自定义属性")]
        public bool AddVisible { get; set; }
        [Description("编辑"), Category("自定义属性")]
        public bool EditVisible { get; set; }
        [Description("保存"), Category("自定义属性")]
        public bool SaveVisible { get; set; }
        [Description("取消"), Category("自定义属性")]
        public bool CancelVisible { get; set; }
        [Description("删除"), Category("自定义属性")]
        public bool DeleteVisible { get; set; }
        [Description("详细"), Category("自定义属性")]
        public bool DetailVisible { get; set; }
        [Description("审核"), Category("自定义属性")]
        public bool AuditVisible { get; set; }
        [Description("调整"), Category("自定义属性")]
        public bool AdjustVisible { get; set; }
        [Description("关闭"), Category("自定义属性")]
        public bool CloseVisible { get; set; }


        [Description("新增"), Category("自定义事件")]
        public event BtnClickDeleate AddClick;
        [Description("编辑"), Category("自定义事件")]
        public event BtnClickDeleate EditClick;
        [Description("保存"), Category("自定义事件")]
        public event BtnClickDeleate SaveClick;
        [Description("取消"), Category("自定义事件")]
        public event BtnClickDeleate CancelClick;
        [Description("删除"), Category("自定义事件")]
        public event BtnClickDeleate DeleteClick;
        [Description("详细"), Category("自定义事件")]
        public event BtnClickDeleate DetailClick;
        [Description("审核"), Category("自定义事件")]
        public event BtnClickDeleate AuditClick;
        [Description("调整"), Category("自定义事件")]
        public event BtnClickDeleate AdjustClick;
        [Description("关闭"), Category("自定义事件")]
        public event BtnClickDeleate CloseClick;

        public OpButton()
        {
            InitializeComponent();
            this.AddVisible = true;
            this.EditVisible = true;
            this.SaveVisible = true;
            this.CancelVisible = true;
            this.DeleteVisible = true;
            this.DetailVisible = true;
            this.AuditVisible = true;
            this.AdjustVisible = true;
            this.CloseVisible = true;
        }

        //新增
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (AddClick != null)
            {
                AddClick(sender, e);
            }
        }
        //编辑
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (EditClick != null)
            {
                EditClick(sender, e);
            }
        }
        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveClick != null)
            {
                SaveClick(sender, e);
            }
        }
        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CancelClick != null)
            {
                CancelClick(sender, e);
            }
        }
        //删除
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (DeleteClick != null)
            {
                DeleteClick(sender, e);
            }
        }
        //详细
        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (DetailClick != null)
            {
                DetailClick(sender, e);
            }
        }

        //审核
        private void btnAudit_Click(object sender, EventArgs e)
        {
            if (AuditClick != null)
            {
                AuditClick(sender, e);
            }
        }

        //调整
        private void btnAdjust_Click(object sender, EventArgs e)
        {
            if (AdjustClick != null)
            {
                AdjustClick(sender, e);
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            if (CloseClick != null)
            {
                CloseClick(sender, e);
            }
        }

        //绘制控件Visitble属性
        private void OpButton_Paint(object sender, PaintEventArgs e)
        {
            this.btnAdd.Visible = this.AddVisible;
            this.btnEdit.Visible = this.EditVisible;
            this.btnSave.Visible = this.SaveVisible;
            this.btnCancel.Visible = this.CancelVisible;
            this.btnDel.Visible = this.DeleteVisible;
            this.btnDetail.Visible = this.DetailVisible;
            this.btnAudit.Visible = this.AuditVisible;
            this.btnAdjust.Visible = this.AdjustVisible;
            this.btnClose.Visible = this.CloseVisible;
        }

    }
}
