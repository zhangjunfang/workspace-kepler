using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Model;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using SYSModel;

namespace HXCPcClient.UCForm.DataManage.WorkingTimeFile
{
    public partial class UCWorkingTimeView : UCBase
    {
        #region 字段属性
        tb_workhours tb_workhours_Model;

        UCWorkingTimeManager uc;
        #endregion

        #region 构造载入方法

        public UCWorkingTimeView(string workingTimeId, UCWorkingTimeManager ParentFrm)
        {
            InitializeComponent();
            LoadInfo(workingTimeId);
            SubscribeEvent();
            uc = ParentFrm;
        }

        private void UCWorkingTimeView_Load(object sender, EventArgs e)
        {
            //base.SetBtnStatus(WindowStatus.View);
            SetDataViewBtn();
            SetBtnStatus();
        }

        #endregion

        #region 事件订阅处理
        private void SubscribeEvent()
        {
            EditEvent += new ClickHandler(UCWorkingTimeView_EditEvent);
            DeleteEvent += new ClickHandler(UCWorkingTimeView_DeleteEvent);
            StatusEvent += new ClickHandler(UCWorkingTimeView_StatusEvent);
        }

        void UCWorkingTimeView_StatusEvent(object sender, EventArgs e)
        {
            SetStatus();
        }

        void UCWorkingTimeView_DeleteEvent(object sender, EventArgs e)
        {
            Delete();
        }

        void UCWorkingTimeView_EditEvent(object sender, EventArgs e)
        {
            Edit();
        }

        private void SetStatus()
        {
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (tb_workhours_Model.status.ToString() == ((int)DataSources.EnumStatus.Start).ToString())
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }
                return;
            }

            if (StatusSql())
            {
                MessageBoxEx.Show(btnStatus.Caption + "成功！");
                uc.BindgvWorkList();
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (tb_workhours_Model.status.ToString() == ((int)DataSources.EnumStatus.Start).ToString())
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }
            }
        }

        private bool StatusSql()
        {
            Dictionary<string, string> status = new Dictionary<string, string>();
            if (tb_workhours_Model.status.ToString() == ((int)DataSources.EnumStatus.Start).ToString())
            {
                status.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
            }
            else
            {
                status.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
            }
            return DBHelper.Submit_AddOrEdit(btnStatus.Caption + "工时", "tb_workhours", "whours_id", tb_workhours_Model.whours_id, status);
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (MessageBoxEx.Show("是否要删除当前数据？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Dictionary<string, string> updateField = new Dictionary<string, string>();
                updateField.Add("enable_flag", "0");
                if (DBHelper.Submit_AddOrEdit("删除工时", "tb_workhours", "whours_id", tb_workhours_Model.whours_id, updateField))
                {
                    MessageBoxEx.Show("删除成功！");
                    uc.BindgvWorkList();
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！");
                }
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void Edit()
        {
            UCWorkingTimeAddOrEdit partsEdit = new UCWorkingTimeAddOrEdit(WindowStatus.Edit, tb_workhours_Model.whours_id, uc);
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
            base.addUserControl(partsEdit, "工时档案-编辑", "UCWorkingTimeEdit" + tb_workhours_Model.whours_id, this.Tag.ToString(), this.Name);
        }

        #endregion

        #region 按钮状态

        /// <summary>
        /// 设置按钮状态
        /// </summary>
        private void SetBtnStatus()
        {
            if (tb_workhours_Model.data_source != ((int)DataSources.EnumDataSources.SELFBUILD).ToString())
            {
                btnStatus.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                return;
            }
            if (tb_workhours_Model.status.ToString() == ((int)DataSources.EnumStatus.Start).ToString())
            {
                btnStatus.Caption = "停用";
            }
            else
            {
                btnStatus.Caption = "启用";
            }
        }

        #endregion

        #region 绑定信息

        /// <summary>
        /// 加载供应商档案信息
        /// </summary>
        private void LoadInfo(string workingTimeId)
        {
            if (!string.IsNullOrEmpty(workingTimeId))
            {
                //1.加载工时档案主信息
                DataTable dt = DBHelper.GetTable("查看一条工时档案信息", "v_workhours_users", "*", " whours_id='" + workingTimeId + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    tb_workhours_Model = new tb_workhours();
                    CommonFuncCall.SetModlByDataTable(tb_workhours_Model, dt);
                    CommonFuncCall.SetShowControlValue(this, tb_workhours_Model, "");
                    if (tb_workhours_Model.whours_type == "1")
                    {
                        radIsWorkTime.Checked = true;
                    }
                    else if (tb_workhours_Model.whours_type == "2")
                    {
                        radIsQuota.Checked = true;
                    }
                    lblcreate_by.Text = dt.Rows[0]["create_username"].ToString();
                    lblupdate_by.Text = dt.Rows[0]["update_username"].ToString();

                    if (!string.IsNullOrEmpty(lblcreate_time.Text))
                    {
                        long ticks = (long)Convert.ToInt64(lblcreate_time.Text);
                        lblcreate_time.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    if (!string.IsNullOrEmpty(lblupdate_time.Text))
                    {
                        long ticks = (long)Convert.ToInt64(lblupdate_time.Text);
                        lblupdate_time.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }

                    DataTable dt_bill = CommonFuncCall.GetDataTable();
                    if (!string.IsNullOrEmpty(tb_workhours_Model.repair_type))
                    {
                        lblrepair_type.Text = CommonFuncCall.GetBillNameByBillCode(dt_bill, tb_workhours_Model.repair_type);
                    }
                }
            }
        }
        #endregion

    }
}
