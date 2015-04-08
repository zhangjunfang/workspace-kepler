using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using System.Threading;
using Utility.Common;
using System.Collections;

namespace HXCPcClient.UCForm.SysManage.MemberPara
{
    /// <summary>
    /// 会员参数设置
    /// 孙明生
    /// 修改人：杨天帅
    /// </summary>
    public partial class UCMemberParaManage : UCBase
    {
        #region --成员变量
        private bool myLock = true;      
        #endregion

        #region --构造函数
        public UCMemberParaManage()
        {
            InitializeComponent();           
            DataGridViewEx.SetDataGridViewStyle(this.dgvRecord);   //设置数据表格样式,并将最后一列填充其余空白 
        }
        #endregion

        #region --窗体初始化
        private void UCMemberParaManage_Load(object sender, EventArgs e)
        {
            //base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏
            this.btnStatus.Visible = false;
            this.dgvRecord.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvRecord.Columns)
            {
                if (dgvc.Name == this.columnCheck.Name)
                {
                    continue;
                }
                dgvc.ReadOnly = true;
            }

            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);

            base.AddEvent += new ClickHandler(UCMemberParaManage_AddEvent);
            base.EditEvent += new ClickHandler(UCMemberParaManage_EditEvent);
            base.DeleteEvent += new ClickHandler(UCMemberParaManage_DeleteEvent);

            CommonCtrl.DgCmbBindDict(this.member_grade_id, "sys_member_grade");
          
            this.BindPageData(); 
            
        }
        #endregion       

        #region --按钮事件
        /// <summary>
        /// 删除
        /// </summary>
        void UCMemberParaManage_DeleteEvent(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRecord.Rows)
            {
                object isCheck = dr.Cells[this.columnCheck.Name].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells[this.columnId.Name].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.Deleted);

            ThreadPool.QueueUserWorkItem(new WaitCallback(this._Delete), listField);            
        }
        void _Delete(object obj)
        {
            if (!(obj is List<string>))
            {
                return;
            }

            List<string> listField = obj as List<string>;

            Dictionary<string, string> comField = new Dictionary<string, string>();
            comField.Add("enable_flag", DataSources.EnumEnableFlag.DELETED.ToString("d"));
            comField.Add("update_by", GlobalStaticObj.UserID);
            comField.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
            bool flag = DBHelper.BatchUpdateDataByIn("批量删除会员参数设置", "tb_CustomerSer_member_setInfo", comField, "setInfo_id", listField.ToArray());
            if (flag)
            {
                Dictionary<string, string> prField = new Dictionary<string, string>();
                prField.Add("enable_flag", DataSources.EnumEnableFlag.DELETED.ToString("d"));
                prField.Add("update_by", GlobalStaticObj.UserID);
                prField.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                flag = DBHelper.BatchUpdateDataByIn("批量删除会员参数设置特殊维修项目折扣表", "tb_CustomerSer_member_setInfo_projrct", prField, "setInfo_id", listField.ToArray());

                Dictionary<string, string> paField = new Dictionary<string, string>();
                paField.Add("enable_flag", DataSources.EnumEnableFlag.DELETED.ToString("d"));
                paField.Add("update_by", GlobalStaticObj.UserID);
                paField.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                flag = DBHelper.BatchUpdateDataByIn("批量删除会员参数设置特殊配件折扣表", "tb_CustomerSer_member_setInfo_parts", paField, "setInfo_id", listField.ToArray());
                                
            }

            this.Invoke(this.uiHandler, flag);            
        }
        void Deleted(object obj)
        {
            this.uiHandler -= new UiHandler(this.Deleted);
            this.uiHandler += new UiHandler(this.ShowBindData);

            bool flag = (bool)obj;
            if (flag)
            {
                this.BindPageData();
                MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        void UCMemberParaManage_EditEvent(object sender, EventArgs e)
        {
            if (dgvRecord.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择编辑记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string id = dgvRecord.CurrentRow.Cells[this.columnId.Name].Value.ToString();  //会员参数设置ID
            UCMemberParaAddOrEdit uc = new UCMemberParaAddOrEdit(this.Name);
            uc.windowStatus = WindowStatus.Edit;
            uc.id = id;
            uc.RefreshDataStart -= new UCMemberParaAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCMemberParaAddOrEdit.RefreshData(this.BindPageData);

            base.addUserControl(uc, "会员参数设置-编辑", "MemberParaEdit" + id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 添加
        /// </summary>
        void UCMemberParaManage_AddEvent(object sender, EventArgs e)
        {
            UCMemberParaAddOrEdit uc = new UCMemberParaAddOrEdit(this.Name);
            uc.windowStatus = WindowStatus.Add;
            uc.RefreshDataStart -= new UCMemberParaAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCMemberParaAddOrEdit.RefreshData(this.BindPageData);
            base.addUserControl(uc, "会员参数设置-新增", "MemberPara", this.Tag.ToString(), this.Name);
        }
        #endregion

        #region --数据查询
        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            if (this.myLock)
            {
                this.myLock = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData));
            }
        }
        public void _BindPageData(object obj)
        {
            DataTable dt = DBHelper.GetTable("查询会员参数设置", "tb_CustomerSer_member_setInfo", 
                "*", string.Format("enable_flag='{0}'",DataSources.EnumEnableFlag.USING.ToString("d")), "", "order by create_time desc");

            this.Invoke(this.uiHandler, dt);
        }
        private void ShowBindData(object obj)
        {
            this.dgvRecord.DataSource = obj;
            if (dgvRecord.Rows.Count > 0)
            {
                dgvRecord.CurrentCell = dgvRecord.Rows[0].Cells[0];
            }
            this.myLock = true;
        }
        #endregion

        #region --dgv事件
        private void dgvsetInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string setInfo_id = dgvRecord.Rows[e.RowIndex].Cells[this.columnId.Name].Value.ToString();
                UCMemberParaView uc = new UCMemberParaView();
                uc.uc = this;
                uc.wStatus = WindowStatus.View;
                uc.id = setInfo_id;
                base.addUserControl(uc, "会员参数设置-浏览", "emberParaView" + setInfo_id, this.Tag.ToString(), this.Name);
            }
        }
         #endregion
        
    }
}
