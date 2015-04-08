using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using SYSModel;
using HXCPcClient.CommonClass;
using Utility.Common;
using HXCPcClient.Chooser;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using Utility.CommonForm;

namespace HXCPcClient.UCForm.FinancialManagement.AccountVerification
{
    public partial class UCAccountVerificationManage : UCBase
    {
        #region 属性
        protected bool isSearch = false;//是否是查询页面
        List<string> listIDs = new List<string>();//已选择ID列表
        /// <summary>
        /// 往来核销ID
        /// </summary>
        private string ID
        {
            get
            {
                //if (dgvVerification.CurrentRow == null)
                //{
                //    return string.Empty;
                //}
                //object id = dgvVerification.CurrentRow.Cells[colAccountVerificationID.Name].Value;
                //if (id == null)
                //{
                //    return string.Empty;
                //}
                //else
                //{
                //    return id.ToString();
                //}
                if (listIDs.Count == 1)
                {
                    return listIDs[0];
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        DataTable dtData;//当前查出的数据
        bool isPreview = false;//是否预览
        BusinessPrint businessPrint;//业务打印功能
        #endregion
        public UCAccountVerificationManage()
        {
            InitializeComponent();
            this.AddEvent += new ClickHandler(UCAccountVerificationManage_AddEvent);
            this.EditEvent += new ClickHandler(UCAccountVerificationManage_EditEvent);
            this.CopyEvent += new ClickHandler(UCAccountVerificationManage_CopyEvent);
            this.ViewEvent += new ClickHandler(UCAccountVerificationManage_ViewEvent);
            this.DeleteEvent += new ClickHandler(UCAccountVerificationManage_DeleteEvent);
            this.VerifyEvent += new ClickHandler(UCAccountVerificationManage_VerifyEvent);
            this.SubmitEvent += new ClickHandler(UCAccountVerificationManage_SubmitEvent);
            this.PrintEvent += new ClickHandler(UCAccountVerificationManage_PrintEvent);
            this.ExportEvent += new ClickHandler(UCAccountVerificationManage_ExportEvent);
            dgvVerification.ReadOnly = false;
            dgvVerification.HeadCheckChanged += new DataGridViewEx.DelegateOnClick(dgvVerification_HeadCheckChanged);
            foreach (DataGridViewColumn dgvc in dgvVerification.Columns)
            {
                if (dgvc.Name == colChk.Name)
                {
                    continue;
                }
                dgvc.ReadOnly = true;
            }
            List<string> listNotPrint = new List<string>();
            listNotPrint.Add(colOrgId.Name);
            listNotPrint.Add(colHandle.Name);
            PaperSize paperSize = new PaperSize();
            paperSize.Width = 297;
            paperSize.Height = 210;
            businessPrint = new BusinessPrint(dgvVerification, "tb_account_verification", "往来核销", paperSize, listNotPrint);
        }

        //页面加载
        private void UCAccountVerificationManage_Load(object sender, EventArgs e)
        {
            dtInterval.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString(dtInterval.customFormat);
            dtInterval.EndDate = DateTime.Now.ToString(dtInterval.customFormat);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(dgvVerification, colOrderStatus);
            if (isSearch)//如果是查询
            {
                //设置页面按钮可见性
                var btnCols = new ObservableCollection<ButtonEx_sms>
                {
                    btnView,btnPrint,btnSet,btnExport
                };
                UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
                #region 隐藏右键
                tsmiAddF.Visible = false;
                tsmiEditF.Visible = false;
                tsmiCopyF.Visible = false;
                tsmiDeleteF.Visible = false;
                tsmiSubmitF.Visible = false;
                tsmiVerifyF.Visible = false;
                tss1.Visible = false;
                tss2.Visible = false;
                #endregion
            }
            else
            {
                #region 设置右键权限
                tsmiAddF.Visible = btnAdd.Visible;
                tsmiEditF.Visible = btnEdit.Visible;
                tsmiCopyF.Visible = btnCopy.Visible;
                tsmiDeleteF.Visible = btnDelete.Visible;
                tsmiSubmitF.Visible = btnSubmit.Visible;
                tsmiVerifyF.Visible = btnVerify.Visible;
                tsmiView.Visible = btnView.Visible;
                tsmiPrint.Visible = btnPrint.Visible;
                #endregion
                //设置页面按钮可见性
                //var btnCols = new ObservableCollection<ButtonEx_sms>
                //{
                //    btnAdd, btnCopy, btnEdit, btnDelete,btnSubmit,btnVerify, btnExport, btnPrint,btnSet,btnView,btnRevoke
                //};
                //UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            }
            BindSearch();
            BindData();
        }
        #region 工具栏事件

        //导出
        void UCAccountVerificationManage_ExportEvent(object sender, EventArgs e)
        {
            try
            {
                string dir = Application.StartupPath + @"\ExportFile";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = dir;
                sfd.Title = "导出文件";
                sfd.DefaultExt = "xls";
                sfd.Filter = "Microsoft Office Excel 文件(*.xls;*.xlsx)|*.xls;*.xlsx|Microsoft Office Excel 文件(*.xls)|*.xls|Microsoft Office Excel 文件(*.xlsx)|*.xlsx";
                sfd.FileName = dir + @"\往来核销" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                DialogResult result = sfd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string strWhere = GetWhere();
                    DataTable dt = DBHelper.GetTable("查询往来核销", "tb_account_verification", "*", strWhere, "", "order by create_time desc");
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBoxEx.ShowInformation("没有可导出的数据！");
                        return;
                    }
                    dt.DataTableToDate("order_date");
                    dt.DateTableToEnum("order_type", typeof(DataSources.EnumAccountVerification));
                    dt.DateTableToEnum("order_status", typeof(DataSources.EnumAuditStatus));
                    PercentProcessOperator process = new PercentProcessOperator();
                    #region 匿名方法，后台线程执行完调用
                    process.BackgroundWork =
                        delegate(Action<int> percent)
                        {
                            dt = ExcelHandler.HandleDataTableForExcel(dt, dgvVerification);
                            ExcelHandler.ExportDTtoExcel(dt, "", sfd.FileName, percent);
                        };
                    #endregion
                    process.MessageInfo = "正在执行中";
                    process.Maximum = dt.Rows.Count;
                    #region 匿名方法，后台线程执行完调用
                    process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(
                            delegate(object osender, BackgroundWorkerEventArgs be)
                            {
                                if (be.BackGroundException == null)
                                {
                                    MessageBoxEx.ShowInformation("导出成功！");
                                }
                                else
                                {
                                    Utility.Log.Log.writeLineToLog("【往来核销】" + be.BackGroundException.Message, "client");
                                    MessageBoxEx.ShowWarning("导出出现异常");
                                }
                            }
                        );
                    #endregion
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【往来核销】" + ex.Message, "client");
                MessageBoxEx.ShowWarning("导出失败！");
            }

        }

        //打印事件
        void UCAccountVerificationManage_PrintEvent(object sender, EventArgs e)
        {
            //ViewData();
            if (!isPreview)
            {
                dtData.DataTableToDate("order_date");
                dtData.DateTableToEnum("order_type", typeof(DataSources.EnumAccountVerification));
                dtData.DateTableToEnum("order_status", typeof(DataSources.EnumAuditStatus));
                isPreview = true;
            }
            businessPrint.Print(dtData);
        }
        //提交事件
        void UCAccountVerificationManage_SubmitEvent(object sender, EventArgs e)
        {
            SubmitData();
        }

        //审核事件
        void UCAccountVerificationManage_VerifyEvent(object sender, EventArgs e)
        {
            VerifyData();
        }

        //删除事件
        void UCAccountVerificationManage_DeleteEvent(object sender, EventArgs e)
        {
            DeleteData();
        }
        //预览事件
        void UCAccountVerificationManage_ViewEvent(object sender, EventArgs e)
        {
            //ViewData();
            if (!isPreview)
            {
                dtData.DataTableToDate("order_date");
                dtData.DateTableToEnum("order_type", typeof(DataSources.EnumAccountVerification));
                dtData.DateTableToEnum("order_status", typeof(DataSources.EnumAuditStatus));
                isPreview = true;
            }
            businessPrint.Preview(dtData);
        }
        //复制事件
        void UCAccountVerificationManage_CopyEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Copy);
        }
        //编辑事件
        void UCAccountVerificationManage_EditEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Edit);
        }
        //新增事件
        void UCAccountVerificationManage_AddEvent(object sender, EventArgs e)
        {
            AddData();
        }
        #endregion

        #region 工具栏方法
        //审核
        void VerifyData()
        {
            string files = string.Empty;
            foreach (DataGridViewRow dgvr in dgvVerification.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colChk.Name].EditedFormattedValue))
                {
                    files += string.Format("'{0}',", dgvr.Cells[colAccountVerificationID.Name].Value);
                }
            }
            if (files.Length == 0)
            {
                MessageBoxEx.Show("请选择要审核的数据！");
                return;
            }
            UCVerify frmVerify = new UCVerify();
            if (frmVerify.ShowDialog() == DialogResult.OK)
            {
                files = files.TrimEnd(',');
                SysSQLString sql = new SysSQLString();
                sql.cmdType = CommandType.Text;
                sql.sqlString = string.Format("update tb_account_verification set order_status='{0}',Verify_advice='{3}' where account_verification_id in ({1}) and order_status='{2}';",
                    ((int)frmVerify.auditStatus), files, (int)DataSources.EnumAuditStatus.SUBMIT, frmVerify.Content);
                sql.Param = new Dictionary<string, string>();
                List<SysSQLString> listSql = new List<SysSQLString>();
                listSql.Add(sql);

                //如果是审核不通过，则将没有提交或审核状态的单据设为正常
                if (frmVerify.auditStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                {
                    #region 将没有提交或审核状态的单据设为正常
                    SysSQLString receivableSql = new SysSQLString();
                    receivableSql.cmdType = CommandType.Text;
                    receivableSql.sqlString = string.Format(@"update tb_parts_sale_billing set is_lock=@is_lock where where sale_billing_id in (select order_id from tb_verificationn_documents where account_verification_id in ({0})) and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =sale_billing_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=sale_billing_id
);
update tb_maintain_settlement_info set is_lock=@is_lock where where settlement_id in (select order_id from tb_verificationn_documents where account_verification_id in ({0})) and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =settlement_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=settlement_id
);
update tb_maintain_three_guaranty_settlement_yt set is_lock=@is_lock where where st_id in (select order_id from tb_verificationn_documents where account_verification_id in ({0})) and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =st_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=st_id
);
update tb_parts_purchase_billing set is_lock=@is_lock where where purchase_billing_id in (select order_id from tb_verificationn_documents where account_verification_id in ({0})) and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =purchase_billing_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=purchase_billing_id
);", files);
                    receivableSql.Param = new Dictionary<string, string>();
                    receivableSql.Param.Add("is_lock", ((int)DataSources.EnumImportStaus.OPEN).ToString());
                    //receivableSql.Param.Add("@id", files);
                    listSql.Add(receivableSql);
                    #endregion
                    //审核失败需重新计算已结算金额
                    foreach (DataGridViewRow dgvr in dgvVerification.Rows)
                    {
                        if (Convert.ToBoolean(dgvr.Cells[colChk.Name].EditedFormattedValue))
                        {
                            DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(dgvr.Cells[colOrderType.Name].Tag);
                            string order_id = dgvr.Cells[colAccountVerificationID.Name].Value.ToString();
                            Financial.DocumentSettlementByVerification(enumAccount, order_id, listSql);
                        }
                    }
                }
                if (DBHelper.BatchExeSQLStringMultiByTrans("审核往来核销", listSql))
                {
                    MessageBoxEx.Show("审核成功！");
                    BindData();
                }
                else
                {
                    MessageBoxEx.Show("审核失败！");
                }
            }
        }
        //提交,只有草稿状态才可提交
        void SubmitData()
        {

            dgvVerification.EndEdit();
            List<SysSQLString> listSql = new List<SysSQLString>();
            string submit = ((int)DataSources.EnumAuditStatus.SUBMIT).ToString();//提交
            string draft = ((int)DataSources.EnumAuditStatus.DRAFT).ToString();//草稿
            string notAudit = ((int)DataSources.EnumAuditStatus.NOTAUDIT).ToString();//审核失败
            foreach (DataGridViewRow dgvr in dgvVerification.Rows)
            {
                object isCheck = dgvr.Cells[colChk.Name].Value;
                string status = CommonCtrl.IsNullToString(dgvr.Cells[colOrderStatus.Name].Tag);
                if (isCheck != null && (bool)isCheck && (status == draft || status==notAudit))
                {
                    SysSQLString sql = new SysSQLString();
                    sql.cmdType = CommandType.Text;
                    sql.Param = new Dictionary<string, string>();
                    sql.Param.Add("submit", submit);
                    sql.Param.Add("draft", draft);
                    sql.Param.Add("notAudit", notAudit);
                    sql.Param.Add("order_num", CommonUtility.GetNewNo(DataSources.EnumProjectType.PAYMENT));
                    string order_id = dgvr.Cells[colAccountVerificationID.Name].Value.ToString();
                    sql.Param.Add("account_verification_id", order_id);
                    sql.sqlString = "update tb_account_verification set order_status=@submit,order_num=@order_num where account_verification_id=@account_verification_id and (order_status=@draft or order_status=@notAudit)";
                    listSql.Add(sql);
                    SetDocumentImportStatus("is_lock", DataSources.EnumImportStaus.LOCK, listSql, order_id, dgvr.Cells[colOrderType.Name].Tag.ToString());
                    DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(dgvr.Cells[colOrderType.Name].Tag);
                    Financial.DocumentSettlementByVerification(enumAccount, order_id, listSql);
                }
            }
            if (listSql.Count == 0)
            {
                MessageBoxEx.Show("请选择要提交的数据！");
                return;
            }
            if (!MessageBoxEx.ShowQuestion("是否要提交选择的数据！"))
            {
                return;
            }
            if (DBHelper.BatchExeSQLStringMultiByTrans("提交应收应付", listSql))
            {
                MessageBoxEx.Show("提交成功！");
                BindData();
            }
            else
            {
                MessageBoxEx.ShowWarning("提交失败！");
            }
        }
        /// <summary>
        /// 设置单据导入状态
        /// </summary>
        void SetDocumentImportStatus(string statusName, DataSources.EnumImportStaus importStaus, List<SysSQLString> listSql, string order_id, string order_type)
        {
            DataTable dt = DBHelper.GetTable("", "tb_verificationn_documents", "order_id,order_name", string.Format("account_verification_id='{0}'", order_id), "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            Preposition pre = new Preposition();
            foreach (DataRow dr in dt.Rows)
            {
                pre.AddID(dr["order_id"], dr["order_name"]);
            }
            listSql.AddRange(pre.GetSql(statusName, importStaus));
        }

        //删除
        void DeleteData()
        {
            dgvVerification.EndEdit();
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dgvr in dgvVerification.Rows)
            {
                object isCheck = dgvr.Cells[colChk.Name].Value;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dgvr.Cells[colAccountVerificationID.Name].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择要删除的数据!");
                return;
            }
            if (MessageBoxEx.Show("是否要删除当前数据？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("enable_flag", ((int)DataSources.EnumEnableFlag.DELETED).ToString());
            if (DBHelper.BatchUpdateDataByIn("批量删除往来核销", "tb_account_verification", dic, "account_verification_id", listField.ToArray()))
            {
                MessageBoxEx.Show("删除成功！");
                BindData();
            }
            else
            {
                MessageBoxEx.Show("删除失败！");
            }
        }
        //预览
        void ViewData()
        {

            if (dgvVerification.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择要预览的数据!");
                return;
            }
            string id = ID;
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            UCAccountVerificationAdd view = new UCAccountVerificationAdd(WindowStatus.View, id, this);
            base.addUserControl(view, "往来核销-预览", "UCAccountVerificationView" + id, this.Tag.ToString(), this.Name);
        }
        //编辑
        void EditData(WindowStatus status)
        {
            if (status != WindowStatus.Edit && status != WindowStatus.Copy)
            {
                return;
            }
            string title = "编辑";
            string menuId = "UCAccountVerificationEdit";
            if (status == WindowStatus.Copy)
            {
                title = "复制";
                menuId = "UCAccountVerificationCopy";
            }
            if (dgvVerification.CurrentRow == null)
            {
                MessageBoxEx.Show(string.Format("请选择要{0}的数据!", title));
                return;
            }
            string id = ID;
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            UCAccountVerificationAdd add = new UCAccountVerificationAdd(status, id, this);
            base.addUserControl(add, string.Format("往来核销-{0}", title), menuId + id, this.Tag.ToString(), this.Name);
        }
        //新增
        void AddData()
        {
            UCAccountVerificationAdd add = new UCAccountVerificationAdd(WindowStatus.Add, null, this);
            this.addUserControl(add, string.Format("往来核销-新增"), "UCAccountVerificationAdd", this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        string GetWhere()
        {
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat("enable_flag='{0}'", (int)DataSources.EnumEnableFlag.USING);//查询标记未删除的数据
            string orderType = CommonCtrl.IsNullToString(cboOrderType.SelectedValue);//单据类型
            if (orderType.Length > 0)
            {
                sbWhere.AppendFormat(" and order_type='{0}'", orderType);
            }
            string order_num = txtOrderNum.Caption.Trim();//单据编号
            if (order_num.Length > 0)
            {
                sbWhere.AppendFormat(" and order_num like '%{0}%'", order_num);
            }
            string custName1 = CommonCtrl.IsNullToString(txtcCustName1.Text);//往来单位1
            if (custName1.Length > 0)
            {
                sbWhere.AppendFormat(" and cust_name1 like '%{0}%'", custName1);
            }
            string custName2 = CommonCtrl.IsNullToString(txtcCustName2.Text);//往来单位2
            if (custName2.Length > 0)
            {
                sbWhere.AppendFormat(" and cust_name2 like '%{0}%'", custName2);
            }
            string comId = CommonCtrl.IsNullToString(cboCompany.SelectedValue);//公司
            if (comId.Length > 0)
            {
                sbWhere.AppendFormat(" and com_id='{0}'", comId);
            }
            string orgID = CommonCtrl.IsNullToString(cboOrgId.SelectedValue);//部门
            if (orgID.Length > 0)
            {
                sbWhere.AppendFormat(" and org_id='{0}'", orgID);
            }
            string handle = CommonCtrl.IsNullToString(cboHandle.SelectedValue);//经办人
            if (handle.Length > 0)
            {
                sbWhere.AppendFormat(" and handle='{0}'", handle);
            }
            if (!isSearch)
            {
                string orderStatus = CommonCtrl.IsNullToString(cboOrderStatus.SelectedValue);//单据状态
                if (orderStatus.Length > 0)
                {
                    sbWhere.AppendFormat(" and order_status='{0}'", orderStatus);
                }
            }
            else
            {
                sbWhere.Append(" and order_status ='2'");
            }
            if (!string.IsNullOrEmpty(dtInterval.StartDate) && !string.IsNullOrEmpty(dtInterval.EndDate))
            {
                sbWhere.AppendFormat(" and create_time between {0} and {1}", Common.LocalDateTimeToUtcLong(DateTime.Parse(dtInterval.StartDate).Date),
                   Common.LocalDateTimeToUtcLong(DateTime.Parse(dtInterval.EndDate).Date.AddDays(1).AddMilliseconds(-1)));
            }
            return sbWhere.ToString();
        }
        //绑定数据
        void BindData()
        {
            string strWhere = GetWhere();
            int recordCount = 0;
            isPreview = false;
            dtData = DBHelper.GetTableByPage("查询往来核销", "tb_account_verification", "*", strWhere, "", "create_time desc", page.PageIndex, page.PageSize, out recordCount);
            dgvVerification.RowCount = 0;
            foreach (DataRow dr in dtData.Rows)
            {
                DataGridViewRow dgvr = dgvVerification.Rows[dgvVerification.Rows.Add()];
                dgvr.Cells[colAccountVerificationID.Name].Value = dr["account_verification_id"];//ID
                dgvr.Cells[colOrderType.Name].Value = DataSources.GetDescription(typeof(DataSources.EnumAccountVerification), dr["order_type"]);//单据类型
                dgvr.Cells[colOrderType.Name].Tag = dr["order_type"];
                dgvr.Cells[colOrderNum.Name].Value = CommonCtrl.IsNullToString(dr["order_num"]);//单据编号
                dgvr.Cells[colOrderDate.Name].Value = Common.UtcLongToLocalDateTime(dr["order_date"], "yyyy-MM-dd");//单据日期
                dgvr.Cells[colCustName1.Name].Value = CommonCtrl.IsNullToString(dr["cust_name1"]);//往来单位1
                dgvr.Cells[colCustName2.Name].Value = CommonCtrl.IsNullToString(dr["cust_name2"]);//往来单位2
                dgvr.Cells[colAdvanceBalance.Name].Value = CommonCtrl.IsNullToString(dr["verification_money"]);//冲销金额
                dgvr.Cells[colOrgId.Name].Value = CommonCtrl.IsNullToString(dr["org_name"]);//部门
                dgvr.Cells[colHandle.Name].Value = CommonCtrl.IsNullToString(dr["handle_name"]);//经办人
                dgvr.Cells[colOperator.Name].Value = CommonCtrl.IsNullToString(dr["operator_name"]);//操作人
                dgvr.Cells[colRemark.Name].Value = CommonCtrl.IsNullToString(dr["remark"]);//备注
                dgvr.Cells[colOrderStatus.Name].Value = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), dr["order_status"]);//单据状态
                dgvr.Cells[colOrderStatus.Name].Tag = dr["order_status"];
            }
            if (dgvVerification.RowCount > 0)
            {
                dgvVerification.CurrentCell = dgvVerification.Rows[0].Cells[colOrderNum.Name];
                dgvVerification.CurrentCell.Selected = true;
                dgvVerification.ClearSelection();
                //dgvVerification.Rows[0].Selected = true;
            }
            page.RecordCount = recordCount;
            page.SetBtnState();
        }
        public void BindData(string orderID)
        {
            BindData();
            if(string.IsNullOrEmpty(orderID))
            {
                return;
            }
            foreach (DataGridViewRow dgvr in dgvVerification.Rows)
            {
                if (CommonCtrl.IsNullToString(dgvr.Cells[colAccountVerificationID.Name].Value) == orderID)
                {
                    dgvVerification.CurrentCell = dgvr.Cells[colOrderType.Name];
                    break;
                }
            }
        }

        /// <summary>
        /// 清空查询面板
        /// </summary>
        void ClearSearch()
        {
            ClearSearch(pnlSearch);
            dtInterval.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString(dtInterval.customFormat);
            dtInterval.EndDate = DateTime.Now.ToString(dtInterval.customFormat);
        }

        //绑定查询控件
        void BindSearch()
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");
            DataSources.BindComBoxDataEnum(cboOrderStatus, typeof(DataSources.EnumAuditStatus), true);
            DataSources.BindComBoxDataEnum(cboOrderType, typeof(DataSources.EnumAccountVerification), true);
            if (!isSearch)
            {
                DataSources.BindComBoxDataEnum(cboOrderStatus, typeof(DataSources.EnumAuditStatus), true);
            }
            else
            {
                lblStatus.Visible = false;
                cboOrderStatus.Visible = false;
            }
        }

        #endregion

        #region 查询面板事件
        //清空
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        //翻页
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        //选择公司，绑定公司
        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue == null)
            {
                return;
            }
            CommonFuncCall.BindDepartment(cboOrgId, cboCompany.SelectedValue.ToString(), "全部");
        }
        //选择部门，绑定经办人
        private void cboOrgId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOrgId.SelectedValue == null)
            {
                return;
            }
            CommonFuncCall.BindHandle(cboHandle, cboOrgId.SelectedValue.ToString(), "全部");
        }
        //选择单据类型
        private void cboOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CommonCtrl.IsNullToString(cboOrderType.SelectedValue) == "")
            {
                lblCustName1.Text = "往来单位1：";
                lblCustName2.Text = "往来单位2：";
                return;
            }
            #region 清空项
            txtcCustName1.Tag = null;
            txtcCustName1.Text = string.Empty;
            txtcCustName2.Tag = null;
            txtcCustName2.Text = string.Empty;
            #endregion
            DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(cboOrderType.SelectedValue);
            #region 各个状态的显示
            switch (enumAccount)
            {
                case DataSources.EnumAccountVerification.YuShouToYingShou:
                    lblCustName1.Text = "预收客户：";
                    lblCustName2.Text = "应收客户：";
                    break;
                case DataSources.EnumAccountVerification.YuFuToYingFu:
                    lblCustName1.Text = "预收供应商：";
                    lblCustName2.Text = "应收供应商：";
                    break;
                case DataSources.EnumAccountVerification.YingFuToYingFu:
                    lblCustName1.Text = "转出供应商：";
                    lblCustName2.Text = "转入供应商：";
                    break;
                case DataSources.EnumAccountVerification.YingShouToYingShou:
                    lblCustName1.Text = "转出客户：";
                    lblCustName2.Text = "转入客户：";
                    break;
                case DataSources.EnumAccountVerification.YingShouToYingFu:
                    lblCustName1.Text = "客户：";
                    lblCustName2.Text = "供应商：";
                    break;
                case DataSources.EnumAccountVerification.YingFuToYingShou:
                    lblCustName1.Text = "供应商：";
                    lblCustName2.Text = "客户：";
                    break;
                case DataSources.EnumAccountVerification.YuShouToYuShou:
                    lblCustName1.Text = "转出客户：";
                    lblCustName2.Text = "转入客户：";
                    break;
                case DataSources.EnumAccountVerification.YuFuToYuFu:
                    lblCustName1.Text = "转出供应商：";
                    lblCustName2.Text = "转入供应商：";
                    break;
                default:
                    lblCustName1.Text = "往来单位1：";
                    lblCustName2.Text = "往来单位2：";
                    break;
            }
            #endregion
        }
        //往来单位1选择器
        private void txtcCustName1_ChooserClick(object sender, EventArgs e)
        {
            //if (CommonCtrl.IsNullToString(cboOrderType.SelectedValue) == "")
            //{
            //    return;
            //}
            frmBtype frmUints = new frmBtype();
            if (frmUints.ShowDialog() == DialogResult.OK)
            {
                txtcCustName1.Text = frmUints.BtypeName;
            }
            return;
            DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(cboOrderType.SelectedValue);
            switch (enumAccount)
            {
                case DataSources.EnumAccountVerification.YuShouToYingShou://预收冲应收
                case DataSources.EnumAccountVerification.YingShouToYingShou://应收转应收
                case DataSources.EnumAccountVerification.YingShouToYingFu://应收冲应付
                case DataSources.EnumAccountVerification.YuShouToYuShou://预收转预收
                    frmCustomerInfo frmCustomer = new frmCustomerInfo();
                    if (frmCustomer.ShowDialog() == DialogResult.OK)
                    {
                        txtcCustName1.Tag = frmCustomer.strCustomerId;
                        txtcCustName1.Text = frmCustomer.strCustomerName;
                        if (txtcCustName2.Tag == null && enumAccount != DataSources.EnumAccountVerification.YingShouToYingFu)//没有选择应收客户，默认选择与预收客户相同
                        {
                            txtcCustName2.Tag = frmCustomer.strCustomerId;
                            txtcCustName2.Text = frmCustomer.strCustomerName;
                        }
                    }
                    break;
                case DataSources.EnumAccountVerification.YuFuToYingFu://预付冲应付
                case DataSources.EnumAccountVerification.YingFuToYingFu://应付转应付
                case DataSources.EnumAccountVerification.YingFuToYingShou://应付冲应收
                case DataSources.EnumAccountVerification.YuFuToYuFu://预付转预付
                    frmSupplier frmSupp = new frmSupplier();
                    if (frmSupp.ShowDialog() == DialogResult.OK)
                    {
                        txtcCustName1.Tag = frmSupp.supperID;
                        txtcCustName1.Text = frmSupp.supperName;
                        if (txtcCustName2.Tag == null && enumAccount != DataSources.EnumAccountVerification.YingFuToYingShou)
                        {
                            txtcCustName2.Tag = frmSupp.supperID;
                            txtcCustName2.Text = frmSupp.supperName;
                        }
                    }
                    break;
            }
        }
        //往来单位2选择器
        private void txtcCustName2_ChooserClick(object sender, EventArgs e)
        {
            frmBtype frmUints = new frmBtype();
            if (frmUints.ShowDialog() == DialogResult.OK)
            {
                txtcCustName2.Text = frmUints.BtypeName;
            }
            return;
            if (CommonCtrl.IsNullToString(cboOrderType.SelectedValue) == "")
            {
                return;
            }
            DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(cboOrderType.SelectedValue);
            switch (enumAccount)
            {
                case DataSources.EnumAccountVerification.YuShouToYingShou://预收冲应收
                case DataSources.EnumAccountVerification.YingShouToYingShou://应收转应收
                case DataSources.EnumAccountVerification.YingFuToYingShou://应付冲应收
                case DataSources.EnumAccountVerification.YuShouToYuShou://预收转预收
                    frmCustomerInfo frmCustomer = new frmCustomerInfo();
                    if (frmCustomer.ShowDialog() == DialogResult.OK)
                    {
                        txtcCustName2.Tag = frmCustomer.strCustomerId;
                        txtcCustName2.Text = frmCustomer.strCustomerName;
                    }
                    break;
                case DataSources.EnumAccountVerification.YuFuToYingFu://预付冲应付
                case DataSources.EnumAccountVerification.YingFuToYingFu://应付转应付
                case DataSources.EnumAccountVerification.YingShouToYingFu://应收冲应付
                case DataSources.EnumAccountVerification.YuFuToYuFu://预付转预付
                    frmSupplier frmSupp = new frmSupplier();
                    if (frmSupp.ShowDialog() == DialogResult.OK)
                    {
                        txtcCustName2.Tag = frmSupp.supperID;
                        txtcCustName2.Text = frmSupp.supperName;
                    }
                    break;
            }
        }
        #endregion

        #region DataGridView事件
        //双击查看
        private void dgvVerification_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewData();
        }
        //选择复选框
        private void dgvVerification_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvVerification.CurrentCell == null)
            {
                return;
            }

            if (dgvVerification.CurrentCell.OwningColumn.Name == colChk.Name)
            {
                SetSelectedStatus();
            }
        }

        /// <summary>
        /// 选择，设置工具栏状态
        /// </summary>
        void SetSelectedStatus()
        {
            listIDs.Clear();
            //已选择状态列表
            List<string> listFiles = new List<string>();

            foreach (DataGridViewRow dgvr in dgvVerification.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colChk.Name].EditedFormattedValue))
                {
                    listFiles.Add(dgvr.Cells[colOrderStatus.Name].Tag.ToString());
                    listIDs.Add(dgvr.Cells[colAccountVerificationID.Name].Value.ToString());
                }
            }

            //提交
            string submitStr = ((int)DataSources.EnumAuditStatus.SUBMIT).ToString();
            //审核
            string auditStr = ((int)DataSources.EnumAuditStatus.AUDIT).ToString();
            //草稿
            string draftStr = ((int)DataSources.EnumAuditStatus.DRAFT).ToString();
            //审核未通过
            string noAuditStr = ((int)DataSources.EnumAuditStatus.NOTAUDIT).ToString();
            //作废
            string invalid = ((int)DataSources.EnumAuditStatus.Invalid).ToString();
            //复制按钮，只选择一个并且不是作废，可以复制
            if (listFiles.Count == 1 && !listFiles.Contains(invalid))
            {
                btnCopy.Enabled = true;
                tsmiCopyF.Enabled = true;
            }
            else
            {
                btnCopy.Enabled = false;
                tsmiCopyF.Enabled = false;
            }
            //编辑按钮，只选择一个并且是草稿或未通过状态，可以编辑
            if (listFiles.Count == 1 && (listFiles.Contains(draftStr) || listFiles.Contains(noAuditStr)))
            {
                btnEdit.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnEdit.Enabled = false;
            }
            //判断”审核“按钮是否可用
            if (listFiles.Contains(auditStr) || listFiles.Contains(draftStr) || listFiles.Contains(noAuditStr) || listFiles.Contains(invalid))
            {
                btnVerify.Enabled = false;
                btnVerify.Enabled = false;
            }
            else
            {
                btnVerify.Enabled = true;
                btnVerify.Enabled = true;
            }
            //包含已审核、已提交、已作废状态，提交、删除按钮不可用
            if (listFiles.Contains(auditStr) || listFiles.Contains(submitStr) || listFiles.Contains(invalid))
            {
                btnSubmit.Enabled = false;
                btnDelete.Enabled = false;
                tsmiSubmitF.Enabled = false;
                tsmiDeleteF.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
                btnDelete.Enabled = true;
                tsmiSubmitF.Enabled = true;
                tsmiDeleteF.Enabled = true;
            }

            if (listFiles.Contains(invalid))
            {

            }

        }
        //单击行，选择当前行复选框
        private void dgvVerification_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            //if (e.ColumnIndex == colChk.Index)
            //{
            //    return;
            //}
            ////清空已选择框
            //foreach (DataGridViewRow dgvr in dgvVerification.Rows)
            //{
            //    object check = dgvr.Cells[colChk.Name].EditedFormattedValue;
            //    if (check != null && (bool)check)
            //    {
            //        dgvr.Cells[colChk.Name].Value = false;
            //    }
            //}
            //if ((bool)dgvVerification.CurrentRow.Cells[colChk.Name].EditedFormattedValue)
            //{
            //    dgvVerification.CurrentRow.Cells[colChk.Name].Value = false;
            //}
            //else
            //{
            //    dgvVerification.CurrentRow.Cells[colChk.Name].Value = true;
            //}
            SetSelectedStatus();
        }
        //全选
        void dgvVerification_HeadCheckChanged()
        {
            SetSelectedStatus();
        }
        #endregion

        #region 右键事件
        //查询
        private void tsmiSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        //清除查询面板
        private void tsmiClear_Click(object sender, EventArgs e)
        {
            ClearSearch(pnlSearch);
        }
        //新增
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            AddData();
        }
        //编辑
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            EditData(WindowStatus.Edit);
        }
        //复制
        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            EditData(WindowStatus.Copy);
        }
        //删除
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }
        //提交
        private void tsmiSubmit_Click(object sender, EventArgs e)
        {
            SubmitData();
        }
        //审核
        private void tsmiVerify_Click(object sender, EventArgs e)
        {
            VerifyData();
        }
        //操作
        private void tsmiOperation_Click(object sender, EventArgs e)
        {

        }
        //预览
        private void tsmiView_Click(object sender, EventArgs e)
        {
            ViewData();
        }
        //打印
        private void tsmiPrint_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
