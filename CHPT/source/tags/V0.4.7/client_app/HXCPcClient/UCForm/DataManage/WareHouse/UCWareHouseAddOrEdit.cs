using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using SYSModel;
using Utility.Common;
using System.Reflection;
using HXCPcClient.Chooser;

namespace HXCPcClient.UCForm.DataManage.WareHouse
{
    public partial class UCWareHouseAddOrEdit : UCBase
    {

        #region 变量

        private int CurrentRowIndex = -1;
        private int EditIndex = -1;
        private string wareHouseId = string.Empty;
        private tb_warehouse tb_warehouse_Model = new tb_warehouse();
        UCWareHouseManager uc;
        #endregion

        #region 初始化窗体
        public UCWareHouseAddOrEdit(WindowStatus status, string wareHouseId, UCWareHouseManager uc)
        {
            InitializeComponent();
            this.dgvWareHouseList.ReadOnly = false;
            this.windowStatus = status;
            this.wareHouseId = wareHouseId;
            this.uc = uc;
            BindDllInfo();
            if (status == WindowStatus.Edit || status == WindowStatus.Copy)
            {
                LoadInfo(wareHouseId);
                GetAllCargoSpace(wareHouseId);

                ucAttr.TableName = "tb_warehouse";
                ucAttr.TableNameKeyValue = wareHouseId;
                ucAttr.BindAttachment();
            }
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            {
                lblwh_code.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.WareHouse);
            }
            base.SaveEvent += new ClickHandler(UCWareHouseAddOrEdit_SaveEvent);


        }

        private void UCWareHouseAddOrEdit_Load(object sender, EventArgs e)
        {
            ucAttr.Size = tabPage2.Size;
            //base.SetBtnStatus2(status);
            SetBtnStatus();
            SetQuick();
        }

        /// <summary>
        /// 设置页面按钮状态
        /// </summary>
        private void SetBtnStatus()
        {
            if (windowStatus == WindowStatus.Edit)
            {
                SetDataEditBtn();
            }
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                SetDataAddBtn();
            }
        }
        #endregion

        #region 方法、函数
        /// <summary>
        /// 加载仓库档案信息
        /// </summary>
        private void LoadInfo(string wareHouseId)
        {
            if (!string.IsNullOrEmpty(wareHouseId))
            {
                //1.加载仓库档案主信息
                DataTable dt = DBHelper.GetTable("查看一条仓库档案信息", "v_warehouse_companyname_username", "*", " wh_id='" + wareHouseId + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonFuncCall.SetModlByDataTable(tb_warehouse_Model, dt);
                    CommonFuncCall.SetShowControlValue(this, tb_warehouse_Model, "");
                    txtChooseCompanty.Text = dt.Rows[0]["com_name"].ToString();
                    txtChooseCompanty.Tag = dt.Rows[0]["com_id"].ToString();
                    txtChooseWhHead.Text = dt.Rows[0]["wh_head"].ToString();
                }
                //2.加载仓库货位信息


                //3.加载附件信息

            }
        }
        /// <summary>
        /// 验证数据信息完整性
        /// </summary>
        private bool CheckDataInfo()
        {
            //仓库名称
            if (string.IsNullOrEmpty(txtwh_name.Caption.ToString()))
            {
                MessageBoxEx.Show("请填写仓库名称!");
                return false;
            }

            if (!string.IsNullOrEmpty(txtwh_tel.Caption.Trim()))
            { if (!Validator.IsTel(txtwh_tel.Caption.Trim())) { MessageBoxEx.Show("公司电话格式不正确!"); return false; } }
            if (!string.IsNullOrEmpty(txtwh_fax.Caption.Trim()))
            { if (!Validator.IsTel(txtwh_fax.Caption.Trim())) { MessageBoxEx.Show("公司传真格式不正确!"); return false; } }
            if (!string.IsNullOrEmpty(txtzip_code.Caption.Trim()))
            { if (!Validator.IsPostCode(txtzip_code.Caption.Trim())) { MessageBoxEx.Show("公司邮编格式不正确!"); return false; } }


            if (!ucAttr.CheckAttachment())
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 绑定下拉框信息
        /// </summary>
        private void BindDllInfo()
        {
            CommonFuncCall.BindProviceComBox(ddlprovince, "请选择");
            CommonFuncCall.BindCityComBox(ddlcity, "", "请选择");
            CommonFuncCall.BindCountryComBox(ddlcounty, "", "请选择");
        }
        /// <summary>
        /// 获取该仓库所关联的所有仓库库位信息
        /// </summary>
        /// <param name="wh_id"></param>
        /// <returns></returns>
        private void GetAllCargoSpace(string wh_id)
        {
            string conId = string.Empty;
            DataTable dt_cargo_space = DBHelper.GetTable("查询关联的仓库库位信息", "tb_cargo_space", "*", " wh_id='" + wh_id + "' and enable_flag=1", "", "");
            if (dt_cargo_space != null && dt_cargo_space.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_cargo_space.Rows)
                {
                    DataGridViewRow dgvr = dgvWareHouseList.Rows[dgvWareHouseList.Rows.Add()];
                    dgvr.Cells["colcs_code"].Value = dr["cs_code"];
                    dgvr.Cells["colcs_name"].Value = dr["cs_name"];
                    dgvr.Cells["colstock_u_limit"].Value = dr["stock_u_limit"];
                    dgvr.Cells["colstock_l_limit"].Value = dr["stock_l_limit"];
                    dgvr.Cells["colremark"].Value = dr["remark"];
                }
            }
        }
        /// <summary>
        /// 添加仓库档案的sql语句
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="partID"></param>
        private void AddWareHouseSqlString(List<SysSQLString> listSql, string whId)
        {
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数

            tb_warehouse model = new tb_warehouse();
            CommonFuncCall.SetModelObjectValue(this, model);
            model.wh_id = whId;
            int datasorces = (int)SYSModel.DataSources.EnumDataSources.SELFBUILD;//1自建，2宇通
            model.data_source = datasorces.ToString();
            int StatusNum = (int)SYSModel.DataSources.EnumStatus.Start;//0停用，1启用
            model.status = StatusNum.ToString();
            model.enable_flag = "1";
            model.create_by = GlobalStaticObj.UserID;
            model.create_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
            model.com_id = txtChooseCompanty.Tag == null ? "" : txtChooseCompanty.Tag.ToString();
            model.wh_head = txtChooseWhHead.Text;
            if (model != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" Insert Into tb_warehouse( ");
                StringBuilder sp = new StringBuilder();
                StringBuilder sb_prame = new StringBuilder();
                foreach (PropertyInfo info in model.GetType().GetProperties())
                {
                    string name = info.Name;
                    object value = info.GetValue(model, null);
                    sb_prame.Append("," + name);
                    sp.Append(",@" + name);
                    dicParam.Add(name, value == null ? "" : value.ToString());
                }
                sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
                sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")").Append(";");
                sysStringSql.sqlString = sb.ToString();
                sysStringSql.Param = dicParam;
                listSql.Add(sysStringSql);
            }
        }
        /// <summary>
        /// 编辑供应商档案的sql语句
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="supID"></param>
        /// <param name="model"></param>
        private void EditWareHouseSqlString(List<SysSQLString> listSql, string whId, tb_warehouse model)
        {
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数
            CommonFuncCall.SetModelObjectValue(this, model);
            model.update_by = GlobalStaticObj.UserID;
            model.update_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
            model.com_id = txtChooseCompanty.Tag == null ? "" : txtChooseCompanty.Tag.ToString();
            model.wh_head = txtChooseWhHead.Text;
            if (model != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" Update tb_warehouse Set ");
                bool isFirstValue = true;
                foreach (PropertyInfo info in model.GetType().GetProperties())
                {
                    string name = info.Name;
                    object value = info.GetValue(model, null);
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(name);
                        sb.Append("=");
                        sb.Append("@" + name);
                    }
                    else
                    {
                        sb.Append("," + name);
                        sb.Append("=");
                        sb.Append("@" + name);
                    }
                    dicParam.Add(name, value == null ? "" : value.ToString());
                }
                sb.Append(" where wh_id='" + whId + "';");
                sysStringSql.sqlString = sb.ToString();
                sysStringSql.Param = dicParam;
                listSql.Add(sysStringSql);
            }
        }
        /// <summary>
        /// 在编辑和添加时对主数据和仓库货位表中的信息进行操作
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="supID"></param>
        private void DealCargospace(List<SysSQLString> listSql, string wh_id)
        {
            dgvWareHouseList.EndEdit();
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (dgvWareHouseList.Rows.Count > 0)
            {
                string sql1 = "delete from tb_cargo_space where wh_id=@wh_id;";
                dic.Add("wh_id", wh_id);
                sysStringSql.sqlString = sql1;
                sysStringSql.Param = dic;
                listSql.Add(sysStringSql);

                foreach (DataGridViewRow dr in dgvWareHouseList.Rows)
                {
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    dic.Add("cs_id", Guid.NewGuid().ToString());
                    dic.Add("wh_id", wh_id);
                    dic.Add("cs_code", dr.Cells["colcs_code"].Value == null ? "" : dr.Cells["colcs_code"].Value.ToString());
                    dic.Add("cs_name", dr.Cells["colcs_name"].Value == null ? "" : dr.Cells["colcs_name"].Value.ToString());
                    dic.Add("stock_u_limit", dr.Cells["colstock_u_limit"].Value == null ? "" : dr.Cells["colstock_u_limit"].Value.ToString());
                    dic.Add("stock_l_limit", dr.Cells["colstock_l_limit"].Value == null ? "" : dr.Cells["colstock_l_limit"].Value.ToString());
                    dic.Add("remark", dr.Cells["colremark"].Value == null ? "" : dr.Cells["colremark"].Value.ToString());
                    dic.Add("status", SYSModel.DataSources.EnumStatus.Start.ToString());
                    dic.Add("enable_flag", "1");
                    dic.Add("create_by", GlobalStaticObj.UserID);
                    dic.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());

                    string sql2 = "Insert Into tb_cargo_space(cs_id,wh_id,cs_code,cs_name,stock_u_limit,stock_l_limit,remark,status,enable_flag,create_by,create_time) "
                    + " values(@cs_id,@wh_id,@cs_code,@cs_name,@stock_u_limit,@stock_l_limit,@remark,@status,@enable_flag,@create_by,@create_time);";
                    sysStringSql.sqlString = sql2;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
            }
        }

        /// <summary>
        /// 关闭判断
        /// </summary>
        /// <returns>是否关闭</returns>
        public override bool CloseMenu()
        {
            //return base.CloseMenu();
            return MessageBoxEx.ShowQuestion("是否关闭?");

        }
        #endregion

        #region 控件事件
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWareHouseAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                if (CheckDataInfo())
                {
                    if (!IsValidated)
                    {
                        MessageBoxEx.Show("请先修订错误");
                        return;
                    }

                    if (MessageBoxEx.Show("确认要保存吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    {
                        return;
                    }
                    string opName = "供应商档案操作";
                    List<SysSQLString> listSql = new List<SysSQLString>();

                    if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
                    {
                        wareHouseId = Guid.NewGuid().ToString();
                        AddWareHouseSqlString(listSql, wareHouseId);
                        opName = "新增仓库档案";
                    }
                    else if (windowStatus == WindowStatus.Edit)
                    {
                        EditWareHouseSqlString(listSql, wareHouseId, tb_warehouse_Model);
                        opName = "修改仓库档案";
                    }
                    DealCargospace(listSql, wareHouseId);

                    ucAttr.TableName = "tb_warehouse";
                    ucAttr.TableNameKeyValue = wareHouseId;
                    ucAttr.GetAttachmentSql(listSql);

                    if (DBHelper.BatchExeSQLStringMultiByTrans(opName, listSql))
                    {
                        MessageBoxEx.Show("保存成功！");
                        uc.BindgvWareHouseList();
                        deleteMenuByTag(this.Tag.ToString(), uc.Name);
                    }
                    else
                    {
                        MessageBoxEx.Show("保存失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！");
            }
        }
        /// <summary>
        /// 新增事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiWareHouseAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tsmiWareHouseAdd.Text == "新增")
                {
                    if (dgvWareHouseList.Rows.Count > 0)
                    {
                        DataGridViewRow gvd = dgvWareHouseList.Rows[dgvWareHouseList.Rows.Count - 1];
                        if (gvd.Cells["colcs_code"].Value == null 
                            && gvd.Cells["colcs_name"].Value == null 
                            && gvd.Cells["colstock_u_limit"].Value == null 
                            && gvd.Cells["colstock_l_limit"].Value == null
                            && gvd.Cells["colremark"].Value == null)
                        {
                            MessageBoxEx.Show("已经存在最新行，不可以再次添加!");
                            return;
                        }
                    }
                    dgvWareHouseList.Rows.Add();
                    int CurrentRowIndex = dgvWareHouseList.Rows.Count - 1;
                    EditIndex = CurrentRowIndex;
                    DataGridViewRow dr = dgvWareHouseList.Rows[CurrentRowIndex];
                    this.tsmiWareHouseAdd.Text = "完成";
                    this.tsmiWareHouseEdit.Enabled = false;
                    this.tsmiWareHouseDelete.Enabled = false;
                }
                else if (this.tsmiWareHouseAdd.Text == "完成")
                {
                    ValidateJudge();
                    this.tsmiWareHouseAdd.Text = "新增";
                    this.tsmiWareHouseEdit.Enabled = true;
                    this.tsmiWareHouseDelete.Enabled = true;
                    EditIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！");
            }
        }



        private bool IsValidated = true;

        /// <summary>
        /// 验证是否有重复项
        /// </summary>
        protected void ValidateJudge()
        {
            if (!IsValidated)
            {
               MessageBoxEx.ShowWarning("在增加新数据之前，需要先将之前出现的错误操作给修订");
               return;
            }
            DataGridViewRowCollection ds = dgvWareHouseList.Rows;

            List<DataGridViewRow> list = new List<DataGridViewRow>();
            foreach (DataGridViewRow item in ds)
            {
                list.Add(item);
            }
            var ak47 = list.GroupBy(q => q.Cells[0].Value).Where(s => s.Count() > 1).Select(v => v);
            if (ak47.Count()>0)
            {
                //只要有一行相等就退出
                MessageBoxEx.ShowError("新增加的“货位编号”已经和“在此之前”添加的“货物编号”重复!");
                IsValidated = false;
            }
        }
        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiWareHouseEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentRowIndex > -1)
                {
                    if (this.tsmiWareHouseEdit.Text == "编辑")
                    {
                        EditIndex = CurrentRowIndex;
                        //DataGridViewRow dr = dgvWareHouseList.Rows[EditIndex];
                        //CommonUtility.SetDgvEditCellBgColor(dr, true);
                        this.tsmiWareHouseEdit.Text = "完成";
                        this.tsmiWareHouseAdd.Enabled = false;
                        this.tsmiWareHouseDelete.Enabled = false;
                    }
                    else if (this.tsmiWareHouseEdit.Text == "完成")
                    {

                        ValidateJudge();
                        //DataGridViewRow dr = dgvWareHouseList.Rows[EditIndex];
                        //CommonUtility.SetDgvEditCellBgColor(dr, false);
                        this.tsmiWareHouseEdit.Text = "编辑";
                        EditIndex = -1;
                        this.tsmiWareHouseAdd.Enabled = true;
                        this.tsmiWareHouseDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！");
            }
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiWareHouseDelete_Click(object sender, EventArgs e)
        {
            if (CurrentRowIndex > -1)
            {
                dgvWareHouseList.Rows.Remove(dgvWareHouseList.Rows[CurrentRowIndex]);
            }
        }
        /// <summary>
        /// 编辑文本框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvWareHouseList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //判断是否编辑列
            if (e.RowIndex != EditIndex)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 鼠标进入文本框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvWareHouseList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                CurrentRowIndex = e.RowIndex;
                dgvWareHouseList.Rows[CurrentRowIndex].Selected = true;
            }
        }
        /// <summary>
        /// 选择负责人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWhHead_ChooserClick(object sender, EventArgs e)
        {
            frmUsers frm = new frmUsers();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtChooseWhHead.Text = frm.User_Name;
                txtChooseWhHead.Tag = frm.User_ID;
            }
        }
        /// <summary>
        /// 选择所属公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtChooseCompanty_ChooserClick(object sender, EventArgs e)
        {
            frmChooseCompany frm = new frmChooseCompany();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtChooseCompanty.Text = frm.Company_Name;
                txtChooseCompanty.Tag = frm.Company_ID;
            }
        }

        private void tabPage2_ClientSizeChanged(object sender, EventArgs e)
        {
            ucAttr.Size = tabPage2.Size;
        }

        private void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlprovince.SelectedValue.ToString()))
            {
                CommonFuncCall.BindCityComBox(ddlcity, ddlprovince.SelectedValue.ToString(), "请选择");
                CommonFuncCall.BindCountryComBox(ddlcounty, ddlcity.SelectedValue.ToString(), "请选择");
            }
            else
            {
                CommonFuncCall.BindCityComBox(ddlcity, "", "请选择");
                CommonFuncCall.BindCountryComBox(ddlcounty, "", "请选择");
            }
        }

        private void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlcity.SelectedValue.ToString()))
            {
                CommonFuncCall.BindCountryComBox(ddlcounty, ddlcity.SelectedValue.ToString(), "请选择");
            }
            else
            {
                CommonFuncCall.BindCountryComBox(ddlcounty, "", "请选择");
            }
        }

        void dgvWareHouseList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvWareHouseList.CurrentCell.OwningColumn == colstock_l_limit || dgvWareHouseList.CurrentCell.OwningColumn == colstock_u_limit)
            {
                OnlyNum(e);
            }
        }

        //编辑完后校验库存上限和库存下限
        private void gvWareHouseList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //colstock_l_limit库存下限,colstock_u_limit库存上限
            if (dgvWareHouseList.Rows[e.RowIndex].Cells[colstock_l_limit.Name].Value != null && dgvWareHouseList.Rows[e.RowIndex].Cells[colstock_u_limit.Name].Value != null && (e.ColumnIndex == colstock_l_limit.Index || e.ColumnIndex == colstock_u_limit.Index))
            {
                if (int.Parse(dgvWareHouseList.Rows[e.RowIndex].Cells[colstock_l_limit.Name].Value.ToString()) > int.Parse(dgvWareHouseList.Rows[e.RowIndex].Cells[colstock_u_limit.Name].Value.ToString()))
                {
                    dgvWareHouseList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;

                    MessageBoxEx.ShowError("库存下限不能超过库存上限!");
                }
            }
        }

        #endregion

        #region 设置速查功能
        /// <summary>
        /// 设置速查功能
        /// </summary>
        private void SetQuick()
        {
            //设置所属公司速查
            txtChooseCompanty.SetBindTable("tb_company", "com_name");
            txtChooseCompanty.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(txtBelongCompany_GetDataSourced);
            txtChooseCompanty.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtBelongCompany_DataBacked);
        }

        void txtBelongCompany_GetDataSourced(ServiceStationClient.ComponentUI.TextBox.TextChooser tc, string sqlString)
        {
            sqlString = string.Format("select * from tb_company where com_name like '%{0}%' and enable_flag=1 and status=1", txtChooseCompanty.Text);
            DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dvt);
            if (dvt != null)
            {
                tc.Search();
            }
        }

        void txtBelongCompany_DataBacked(DataRow dr)
        {
            txtChooseCompanty.Text = dr["com_name"].ToString();
            txtChooseCompanty.Tag = dr["com_id"].ToString();
        }


        #endregion

    }
}
