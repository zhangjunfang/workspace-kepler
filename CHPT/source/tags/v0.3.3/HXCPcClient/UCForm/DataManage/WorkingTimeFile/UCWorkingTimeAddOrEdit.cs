using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using Model;
using Utility.Common;
using SYSModel;

namespace HXCPcClient.UCForm.DataManage.WorkingTimeFile
{
    public partial class UCWorkingTimeAddOrEdit : UCBase
    {
        #region 变量
        WindowStatus status;
        string workingTimeId;
        UCWorkingTimeManager uc;
        private tb_workhours tb_workhours_Model = new tb_workhours();
        #endregion

        #region 初始化窗体
        public UCWorkingTimeAddOrEdit(WindowStatus status, string workingTimeId,UCWorkingTimeManager uc)
        {
            InitializeComponent();
            BindDllInfo();
            this.status = status;
            this.workingTimeId = workingTimeId;
            this.uc = uc;
            if (status == WindowStatus.Add)
            {
                radIsWorkTime.Checked = true;
            }
            if (status == WindowStatus.Edit || status == WindowStatus.Copy)
            {
                LoadInfo(workingTimeId);
            }
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            {
                lblproject_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.WorkingTime);
            }
            base.SaveEvent += new ClickHandler(UCWorkingTimeAddOrEdit_SaveEvent);
        }

        private void UCWorkingTimeAddOrEdit_Load(object sender, EventArgs e)
        {
            base.SetBtnStatus2(status);
        }
        #endregion

        #region 方法、函数
        /// <summary>
        /// 加载供应商档案信息
        /// </summary>
        private void LoadInfo(string workingTimeId)
        {
            if (!string.IsNullOrEmpty(workingTimeId))
            {
                //1.加载工时档案主信息
                DataTable dt = DBHelper.GetTable("查看一条工时档案信息", "tb_workhours", "*", " whours_id='" + workingTimeId + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
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
                }
            }
        }
        /// <summary>
        /// 绑定下拉框信息
        /// </summary>
        void BindDllInfo()
        {
            ddlrepair_type.Items.Clear();
            //绑定项目类别
            CommonFuncCall.BindComBoxDataSource(ddlrepair_type, "sys_repair_project_category", "请选择");
        }
        #endregion

        #region 事件
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWorkingTimeAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtproject_name.Caption.Trim()))
                {
                    MessageBoxEx.Show("请填写维修项目名称!");
                    txtproject_name.Focus();
                    return;
                }

                if (MessageBoxEx.Show("确认要保存吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    return;
                }

                Dictionary<string, string> dic = new Dictionary<string, string>();
                int datasorces = (int)SYSModel.DataSources.EnumDataSources.SELFBUILD;
                dic.Add("data_source", datasorces.ToString());//数据来源
                dic.Add("repair_type", ddlrepair_type.SelectedValue.ToString());//项目分类
                dic.Add("project_num", lblproject_num.Text.Trim());//维修项目编号
                dic.Add("project_name", txtproject_name.Caption.Trim());//维修项目名称
                dic.Add("project_remark", txtproject_remark.Caption.Trim());//项目备注
                if (radIsWorkTime.Checked)
                {
                    dic.Add("whours_type", "1");//工时类型，1工时，2定额
                    dic.Add("whours_num_a", txtwhours_num_a.Caption.Trim());//A类工时数
                    dic.Add("whours_num_b", txtwhours_num_b.Caption.Trim());//B类工时数
                    dic.Add("whours_num_c", txtwhours_num_c.Caption.Trim());//C类工时数
                }
                else if (radIsQuota.Checked)
                {
                    dic.Add("whours_type", "2");//工时类型，1工时，2定额
                    dic.Add("quota_price", txtquota_price.Caption.Trim());//定额单价(元)
                }

                //判断窗体状态
                if (status == WindowStatus.Add || status == WindowStatus.Copy)
                {
                    int StatusNum = (int)SYSModel.DataSources.EnumStatus.Start;//0停用，1启用
                    dic.Add("status", StatusNum.ToString());
                    dic.Add("enable_flag", "1");//标识未删除
                    dic.Add("whours_id", Guid.NewGuid().ToString());
                    dic.Add("create_by", GlobalStaticObj.UserID);
                    dic.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                    if (DBHelper.Submit_AddOrEdit("新增工时档案", "tb_workhours", "", "", dic))
                    {
                     
                        MessageBoxEx.Show("保存成功!");
                        deleteMenuByTag(this.Tag.ToString(), uc.Name);
                    }
                    else
                    {
                        MessageBoxEx.Show("保存失败!");
                    }
                }
                else if (status == WindowStatus.Edit)//编辑
                {
                    dic.Add("update_by", GlobalStaticObj.UserID);
                    dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                    if (DBHelper.Submit_AddOrEdit("修改工时档案", "tb_workhours", "whours_id", workingTimeId, dic))
                    {
                        MessageBoxEx.Show("修改成功!");
                        uc.BindgvWorkList();
                        deleteMenuByTag(this.Tag.ToString(), uc.Name);
                    }
                    else
                    {
                        MessageBoxEx.Show("修改失败!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败!");
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radIsWorkTime_CheckedChanged(object sender, EventArgs e)
        {
            txtwhours_num_a.Enabled = true;
            txtwhours_num_b.Enabled = true;
            txtwhours_num_c.Enabled = true;
            txtquota_price.Enabled = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radIsQuota_CheckedChanged(object sender, EventArgs e)
        {
            txtwhours_num_a.Enabled = false;
            txtwhours_num_b.Enabled = false;
            txtwhours_num_c.Enabled = false;
            txtquota_price.Enabled = true;
        } 
        #endregion

        private void txtwhours_num_a_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }

        private void txtwhours_num_b_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }

        private void txtwhours_num_c_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }

        private void txtquota_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }
    }
}
