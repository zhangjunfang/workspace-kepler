using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;
using ServiceStationClient.ComponentUI;
using HXCPcClient.Chooser;
using System.Collections.ObjectModel;

namespace HXCPcClient.UCForm.FinancialManagement.Receivable
{
    public partial class UCReceivableAdd : UCBase
    {
        #region 属性
        string orderID = string.Empty;//账单ID
        UCReceivableManage uc;//管理界面
        WindowStatus windowStatus;//窗体状态
        DataSources.EnumOrderType orderType;//单据类型
        string orderTypeName = string.Empty;//单据类型
        string cust_code = string.Empty;
        DataSources.EnumInvalidOrActivation enumActivation = DataSources.EnumInvalidOrActivation.Activation;//激活状态
        DataGridViewComboBoxEditingControl cboPaymentAccount = null;//付款账户
        DataGridViewComboBoxEditingControl cboBalanceWay = null;//结算方式
        #endregion
        public UCReceivableAdd(WindowStatus status, string orderId, UCReceivableManage uc, DataSources.EnumOrderType orderType)
        {
            InitializeComponent();
            this.dgvBalanceDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right | AnchorStyles.Bottom)));
            this.windowStatus = status;
            this.orderID = orderId;
            this.uc = uc;
            this.orderType = orderType;
            this.orderTypeName = DataSources.GetDescription(orderType, true);
            colDepositRate.ValueType = typeof(decimal);
            colBillingMoney.ValueType = typeof(decimal);
            colSettledMoney.ValueType = typeof(decimal);
            colSettlementMoney.ValueType = typeof(decimal);
            colWaitSettledMoney.ValueType = typeof(decimal);
            colPaidMoney.ValueType = typeof(decimal);
            colDepositRate.ValueType = typeof(decimal);
            //colDeduction.ValueType = typeof(decimal);
            colMoney.ValueType = typeof(decimal);
            base.SaveEvent += new ClickHandler(UCReceivableAdd_SaveEvent);
            base.ImportEvent += new ClickHandler(UCReceivableAdd_ImportEvent);
            base.SubmitEvent += new ClickHandler(UCReceivableAdd_SubmitEvent);
            base.CancelEvent += new ClickHandler(UCReceivableAdd_CancelEvent);
            base.VerifyEvent += new ClickHandler(UCReceivableAdd_VerifyEvent);
            base.InvalidOrActivationEvent += new ClickHandler(UCReceivableAdd_InvalidOrActivationEvent);
        }
        //窗体加载
        private void UCReceivableAdd_Load(object sender, EventArgs e)
        {
            //SetBtnStatus(windowStatus);
            dtpOrderDate.Value = DateTime.Now;
            BindOrderType();
            BindDepartment();
            BindBalanceWay();
            BindAccount();
            SetLable();
            string[] total = { colBillingMoney.Name, colSettledMoney.Name, colSettlementMoney.Name, colWaitSettledMoney.Name, colPaidMoney.Name };
            ControlsConfig.DatagGridViewTotalConfig(dgvBalanceDocuments, total);
            string[] detailTotal = { colMoney.Name };
            ControlsConfig.DatagGridViewTotalConfig(dgvPaymentDetail, detailTotal);
            if (windowStatus != WindowStatus.View)
            {
                dgvPaymentDetail.AllowUserToAddRows = true;
                dgvPaymentDetail.ReadOnly = false;
                colBankAccount.ReadOnly = true;
                colBankOfDeposit.ReadOnly = true;
                dgvBalanceDocuments.ReadOnly = false;
                colDocumentsName.ReadOnly = true;
                colDocumentsNum.ReadOnly = true;
                colDocumentsDate.ReadOnly = true;
                colBillingMoney.ReadOnly = true;
                colSettledMoney.ReadOnly = true;
                colWaitSettledMoney.ReadOnly = true;
                colDeduction.ReadOnly = true;
                colDepositRate.ReadOnly = true;
                //设置页面按钮可见性
                var btnCols = new ObservableCollection<ButtonEx_sms>
                {
                    btnSubmit,btnSave,btnCancel,btnImport,btnExport,btnSet,btnView,btnPrint
                };
                UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            }
            else
            {
                SetControlEnable(this, false);
                //设置页面按钮可见性
                var btnCols = new ObservableCollection<ButtonEx_sms>
                {
                    btnCopy,btnEdit,btnDelete,btnActivation,btnSubmit,btnVerify,btnExport,btnSet,btnView,btnPrint
                };
                UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            }
            lblOperator.Text = GlobalStaticObj.UserName;//操作人
            //当是编辑或复制
            if ((this.windowStatus == WindowStatus.Edit || this.windowStatus == WindowStatus.Copy || this.windowStatus == WindowStatus.View) && orderID.Length > 0)
            {
                BindData();
            }
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                if (orderType == DataSources.EnumOrderType.RECEIVABLE)
                {
                    //    txtOrderNum.Caption = CommonUtility.GetNewNo(DataSources.EnumProjectType.RECEIVABLE);
                    cboOrderType.SelectedValue = (int)DataSources.EnumReceivableType.RECEIVABLE;
                }
                else
                {
                    //    txtOrderNum.Caption = CommonUtility.GetNewNo(DataSources.EnumProjectType.PAYMENT);
                    cboOrderType.SelectedValue = (int)DataSources.EnumPaymentType.PAYMENT;
                }
                dtpOrderDate.Value = DateTime.Now;
                txtOrderStatus.Caption = "草稿";
                btnVerify.Enabled = false;//审核按钮不可用
                lblCreateBy.Text = GlobalStaticObj.UserName;//创建人
                lblCreateTime.Text = DateTime.Now.ToString();//创建时间
                lblUpdateBy.Text = string.Empty;
                lblUpdateTime.Text = string.Empty;
            }
        }
        #region 工具栏事件
        //审核
        void UCReceivableAdd_VerifyEvent(object sender, EventArgs e)
        {
            if (!MessageBoxEx.ShowQuestion("是否要审核当前单据！"))
            {
                return;
            }
            UCVerify frmVerify = new UCVerify();
            if (frmVerify.ShowDialog() == DialogResult.OK)
            {
                SysSQLString sql = new SysSQLString();
                sql.cmdType = CommandType.Text;
                sql.sqlString = string.Format("update tb_bill_receivable set order_status='{0}',Verify_advice='{1}' where payable_single_id ='{2}' and order_status='{3}';",
                    (int)frmVerify.auditStatus, frmVerify.Content, orderID, (int)DataSources.EnumAuditStatus.SUBMIT);
                sql.Param = new Dictionary<string, string>();
                List<SysSQLString> listSql = new List<SysSQLString>();
                listSql.Add(sql);
                //如果是审核不通过，则将没有提交或审核状态的单据设为正常
                if (frmVerify.auditStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                {
                    if (orderType == DataSources.EnumOrderType.RECEIVABLE)
                    {
                        SysSQLString receivableSql = new SysSQLString();
                        receivableSql.cmdType = CommandType.Text;
                        receivableSql.sqlString = string.Format(@"update tb_parts_sale_billing set is_occupy=@is_occupy where where sale_billing_id in (select documents_id from tb_balance_documents where order_id='{0}') and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =sale_billing_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=sale_billing_id
);
update tb_parts_sale_order set is_occupy=@is_occupy where where sale_order_id in (select documents_id from tb_balance_documents where order_id in ({0})) and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =sale_order_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=sale_order_id
);
update tb_maintain_settlement_info set is_occupy=@is_occupy where where settlement_id in (select documents_id from tb_balance_documents where order_id in ({0})) and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =settlement_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=settlement_id
);
update tb_maintain_three_guaranty_settlement_yt set is_occupy=@is_occupy where where st_id in (select documents_id from tb_balance_documents where order_id in ({0})) and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =st_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=st_id
);", orderID);
                        receivableSql.Param = new Dictionary<string, string>();
                        receivableSql.Param.Add("is_occupy", ((int)DataSources.EnumImportStaus.OPEN).ToString());
                        //receivableSql.Param.Add("@id", files);
                    }
                    else
                    {
                        SysSQLString purchaseBillingSql = new SysSQLString();
                        purchaseBillingSql.cmdType = CommandType.Text;
                        purchaseBillingSql.sqlString = string.Format(@"update tb_parts_purchase_billing set is_occupy=@is_occupy where purchase_billing_id in (select documents_id from tb_balance_documents where order_id='{0}') and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =purchase_billing_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=purchase_billing_id
);
                                                  update tb_parts_purchase_order set is_occupy=@is_occupy where order_id in (select documents_id from tb_balance_documents where order_id in ({0})) and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =order_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=order_id
);  ", orderID);
                        purchaseBillingSql.Param = new Dictionary<string, string>();
                        purchaseBillingSql.Param.Add("is_occupy", ((int)DataSources.EnumImportStaus.OPEN).ToString());
                        //purchaseBillingSql.Param.Add("@id", files);
                        listSql.Add(purchaseBillingSql);
                    }
                }
                if (DBHelper.BatchExeSQLStringMultiByTrans("审核应收应付", listSql))
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
        /// <summary>
        /// 激活/作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCReceivableAdd_InvalidOrActivationEvent(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(orderID))
            {
                return;
            }
            if (!MessageBoxEx.ShowQuestion("确认要" + btnActivation.Caption + "吗？"))
            {
                return;
            }
            List<SysSQLString> listSql = new List<SysSQLString>();
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.Param.Add("payable_single_id", orderID);
            if (enumActivation == DataSources.EnumInvalidOrActivation.Activation)
            {
                sql.sqlString = "update tb_bill_receivable set order_status=old_order_status where payable_single_id=@payable_single_id";
            }
            else
            {
                sql.sqlString = "update tb_bill_receivable set old_order_status=order_status,order_status=@order_status where payable_single_id=@payable_single_id";
                sql.Param.Add("order_status", ((int)DataSources.EnumAuditStatus.Invalid).ToString());
            }
            listSql.Add(sql);
            if (DBHelper.BatchExeSQLStringMultiByTrans(btnActivation.Caption + "应收应付", listSql))
            {
                MessageBoxEx.ShowInformation(btnActivation.Caption + "成功！");
                uc.BindData(orderID);
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.ShowError(btnActivation.Caption + "失败！");
            }
        }
        //取消事件
        void UCReceivableAdd_CancelEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确定要关闭当前窗体吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }
            UnlockDocument(false);
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        //提交事件
        void UCReceivableAdd_SubmitEvent(object sender, EventArgs e)
        {
            Save(DataSources.EnumAuditStatus.SUBMIT);
        }
        //导入事件
        void UCReceivableAdd_ImportEvent(object sender, EventArgs e)
        {
            ImportDocument();
        }
        //保存事件
        void UCReceivableAdd_SaveEvent(object sender, EventArgs e)
        {
            Save(DataSources.EnumAuditStatus.DRAFT);
        }
        #endregion

        #region 工具栏方法
        /// <summary>
        /// 解除锁定单据
        /// </summary>
        void UnlockDocument(bool isSelected)
        {
            List<SysSQLString> listSql = new List<SysSQLString>();
            SetDocumentImportStatus("is_lock", DataSources.EnumImportStaus.OPEN, listSql, isSelected);
            if (listSql.Count > 0)
            {
                if (!DBHelper.BatchExeSQLStringMultiByTrans("解除应收应付前置单据锁定", listSql))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 设置单据导入状态
        /// </summary>
        void SetDocumentImportStatus(string statusName, DataSources.EnumImportStaus importStaus, List<SysSQLString> listSql, bool isSelected)
        {
            if (dgvBalanceDocuments.Rows.Count == 0)
            {
                return;
            }
            string ids = string.Empty;
            foreach (DataGridViewRow dgvr in dgvBalanceDocuments.Rows)
            {
                if (isSelected)
                {
                    object check = dgvr.Cells[colCheckDocument.Name].EditedFormattedValue;
                    if (check != null && (bool)check)
                    {
                        ids += string.Format("'{0}',", dgvr.Cells[colDocumentsID.Name].Value);
                    }
                }
                else
                {
                    ids += string.Format("'{0}',", dgvr.Cells[colDocumentsID.Name].Value);
                }
            }
            if (ids.Length == 0)
            {
                return;
            }
            ids = ids.TrimEnd(',');
            string strImportStatus = ((int)importStaus).ToString();
            if (orderType == DataSources.EnumOrderType.RECEIVABLE)
            {
                DataSources.EnumReceivableType type = (DataSources.EnumReceivableType)cboOrderType.SelectedValue;
                if (type == DataSources.EnumReceivableType.RECEIVABLE)
                {
                    listSql.Add(Financial.GetSaleBilling(statusName, strImportStatus, ids));
                    listSql.Add(Financial.GetMaintainSettlement(statusName, strImportStatus, ids));
                    listSql.Add(Financial.GetMaintainThree(statusName, strImportStatus, ids));
                }
                else
                {
                    listSql.Add(Financial.GetSaleOrder(statusName, strImportStatus, ids));
                }
            }
            else
            {
                DataSources.EnumPaymentType type = (DataSources.EnumPaymentType)cboOrderType.SelectedValue;
                if (type == DataSources.EnumPaymentType.PAYMENT)
                {
                    listSql.Add(Financial.GetPurchaseBilling(statusName, strImportStatus, ids));
                }
                else
                {
                    listSql.Add(Financial.GetPurchaseOrder(statusName, strImportStatus, ids));
                }
            }
        }

        //导入结算单据
        void ImportDocument()
        {
            if (windowStatus == WindowStatus.View)
            {
                return;
            }
            if (txtcCustName.Tag == null)//客户
            {
                MessageBoxEx.Show(string.Format("请选择{0}！", lblCustName.Text.Replace("：", "")));
                txtcCustName.Focus();
                return;
            }
            string paymentType = CommonCtrl.IsNullToString(cboOrderType.SelectedValue);//收付款类型
            if (paymentType.Length == 0)
            {
                return;
            }
            if (orderType == DataSources.EnumOrderType.RECEIVABLE)
            {
                #region 应收
                DataSources.EnumReceivableType enumType = (DataSources.EnumReceivableType)int.Parse(paymentType);
                if (enumType == DataSources.EnumReceivableType.RECEIVABLE)
                {
                    cmsImport.Show(base.btnImport, 0, base.btnImport.Height);
                    return;
                }
                else
                {
                    //预收
                    frmReceivableByAdvances frm = new frmReceivableByAdvances(txtcCustName.Tag.ToString());
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        BindDocument(frm.listRows, true);
                    }
                }
                #endregion
            }
            else
            {
                #region 应付
                StringBuilder sbWhere = new StringBuilder();
                DataSources.EnumPaymentType enumType = (DataSources.EnumPaymentType)int.Parse(paymentType);
                if (enumType == DataSources.EnumPaymentType.PAYMENT)
                {
                    frmPayment frm = new frmPayment(txtcCustName.Tag.ToString());
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        BindDocument(frm.listRows, false);
                    }
                }
                else
                {
                    frmPaymentByAdvances frm = new frmPaymentByAdvances(txtcCustName.Tag.ToString());
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        BindDocument(frm.listRows, true);
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        void Save(DataSources.EnumAuditStatus auditStatus)
        {
            //if (windowStatus == WindowStatus.View)
            //{
            //    return;
            //}
            epError.Clear();
            string saveName = "保存";
            if (auditStatus == DataSources.EnumAuditStatus.SUBMIT)
            {
                saveName = "提交";
            }
            dgvBalanceDocuments.EndEdit();
            dgvPaymentDetail.EndEdit();
            if (!CheckReceivable() || !CheckDetail() || !CheckBalanceDocuments())
            {
                return;
            }
            #region 验证单据与明细的和
            //如果是提交，则验证
            if (auditStatus != DataSources.EnumAuditStatus.DRAFT)
            {
                //当应收款单为应收检查收款明细合计与单据合计是否相同
                if (orderType == DataSources.EnumOrderType.RECEIVABLE)
                {
                    if (((DataSources.EnumReceivableType)cboOrderType.SelectedValue) == DataSources.EnumReceivableType.RECEIVABLE)
                    {
                        if (!CheckTotal())
                        {
                            return;
                        }
                    }
                }
                else
                {
                    //当应付款单为应付检查收款明细合计与单据合计是否相同
                    if (((DataSources.EnumPaymentType)cboOrderType.SelectedValue) == DataSources.EnumPaymentType.PAYMENT)
                    {
                        if (!CheckTotal())
                        {
                            return;
                        }
                    }
                }
                if (!CheckDocuments())
                {
                    return;
                }
            }
            #endregion
            if (MessageBoxEx.Show("确认要" + saveName + "吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }
            List<SysSQLString> list = new List<SysSQLString>();
            //如果是新增，则重新生成一个ID
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                orderID = Guid.NewGuid().ToString();
            }
            string opName = orderTypeName + "操作";
            if (windowStatus == WindowStatus.Edit)
            {
                opName = "修改" + orderTypeName;
            }
            else if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                opName = "新增" + orderTypeName;
            }
            AddReceivableSqlString(list, auditStatus);
            AddDetailSqlString(list);
            AddBalanceDocumentsSqlString(list);
            SetDocumentImportStatus("is_lock", DataSources.EnumImportStaus.OPEN, list, false);
            if (auditStatus == DataSources.EnumAuditStatus.SUBMIT)
            {
                SetDocumentImportStatus("is_occupy", DataSources.EnumImportStaus.OCCUPY, list, false);
                Financial.DocumentSettlementByBill(orderType, orderID, list);
            }
            if (DBHelper.BatchExeSQLStringMultiByTrans(opName, list))
            {
                MessageBoxEx.Show(saveName + "成功！");
                uc.BindData(orderID);
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.Show(saveName + "失败！");
            }
        }
        #endregion

        #region 检查数据的准确性
        /// <summary>
        /// 检查收付款数据
        /// </summary>
        /// <returns></returns>
        bool CheckReceivable()
        {
            bool check = true;
            //string orderNum = txtOrderNum.Caption.Trim();//单号
            //if (orderNum.Length == 0)
            //{
            //    MessageBoxEx.Show("单号不能为空！");
            //    txtOrderNum.Focus();
            //    check = false;
            //    return false;
            //}
            if (cboOrderType.SelectedValue == null)
            {
                epError.SetError(cboOrderType, "请选择单据类型！");
                cboOrderType.Focus();
                check = false;
                return false;
            }
            string custCode = CommonCtrl.IsNullToString(txtcCustName.Tag);//往来单位
            if (custCode.Length == 0)
            {
                epError.SetError(txtcCustName, string.Format("请选择{0}！", lblCustName.Text.Replace("：", "")));
                txtcCustName.Focus();
                check = false;
                return false;
            }
            string orgID = CommonCtrl.IsNullToString(cboOrgId.SelectedValue);//部门
            if (orgID.Length == 0)
            {
                epError.SetError(cboOrgId, "请选择部门！");
                cboOrgId.Focus();
                check = false;
                return false;
            }
            string handle = CommonCtrl.IsNullToString(cboHandle.SelectedValue);//经办人
            if (handle.Length == 0)
            {
                epError.SetError(cboHandle, "请选择经办人！");
                cboHandle.Focus();
                check = false;
                return false;
            }
            if (txtBankOfDeposit.Caption.Trim().Length > 40)
            {
                epError.SetError(txtBankOfDeposit, "开户银行不能大于40个字符！");
                txtBankOfDeposit.Focus();
                check = false;
                return false;
            }
            if (txtBankAccount.Caption.Trim().Length > 40)
            {
                epError.SetError(txtBankAccount, "银行账号不能大于40个字符！");
                txtBankAccount.Focus();
                check = false;
                return false;
            }
            if (txtRemark.Caption.Trim().Length > 100)
            {
                epError.SetError(txtRemark, "备注不能大于100个字符！");
                txtRemark.Focus();
                check = false;
                return false;
            }
            return check;
        }

        /// <summary>
        /// 检查收付款明细
        /// </summary>
        /// <returns></returns>
        bool CheckDetail()
        {
            bool check = true;
            foreach (DataGridViewRow dgvr in dgvPaymentDetail.Rows)
            {
                if (dgvr.IsNewRow)
                {
                    continue;
                }
                if (!CheckIsDecimalOrInt(dgvPaymentDetail, dgvr, colMoney))//金额
                {
                    check = false;
                    break;

                }
                if (!CheckIsNull(dgvPaymentDetail, dgvr, colBalanceWay))//结算方式
                {
                    check = false;
                    break;
                }
                if (!CheckIsNull(dgvPaymentDetail, dgvr, colPaymentAccount))//收款账户
                {
                    check = false;
                    break;
                }
                if (CommonCtrl.IsNullToString(dgvr.Cells[colCheckNumber.Name].Value).Length > 40)
                {
                    MessageBoxEx.Show("票号字符长度不能大于40！");
                    dgvPaymentDetail.CurrentCell = dgvr.Cells[colCheckNumber.Name];
                    check = false;
                    break;
                }
                if (CommonCtrl.IsNullToString(dgvr.Cells[colRemark.Name].Value).Length > 300)
                {
                    MessageBoxEx.Show("备注字符长度不能大于300！");
                    dgvPaymentDetail.CurrentCell = dgvr.Cells[colRemark.Name];
                    check = false;
                    break;
                }
            }
            return check;
        }

        /// <summary>
        /// 检查结算单据
        /// </summary>
        /// <returns></returns>
        bool CheckBalanceDocuments()
        {
            bool check = true;
            foreach (DataGridViewRow dgvr in dgvBalanceDocuments.Rows)
            {
                if (!CheckIsNull(dgvBalanceDocuments, dgvr, colDocumentsName))//单据编码
                {
                    check = false;
                    break;
                }
                if (!CheckIsDecimalOrInt(dgvBalanceDocuments, dgvr, colSettlementMoney))//本次结算
                {
                    check = false;
                    break;
                }
                if (!CheckIsDecimalOrInt(dgvBalanceDocuments, dgvr, colPaidMoney))//实收金额
                {
                    check = false;
                    break;
                }
            }
            return check;
        }
        bool CheckIsNull(DataGridView dgv, DataGridViewRow dgvr, DataGridViewColumn dgvc)
        {
            string num = CommonCtrl.IsNullToString(dgvr.Cells[dgvc.Name].Value);
            if (num.Length == 0)
            {
                MessageBoxEx.Show(dgvc.HeaderText + "不能为空！");
                dgv.CurrentCell = dgvr.Cells[dgvc.Name];
                return false;
            }
            return true;
        }
        bool CheckIsDecimalOrInt(DataGridView dgv, DataGridViewRow dgvr, DataGridViewColumn dgvc)
        {
            string num = CommonCtrl.IsNullToString(dgvr.Cells[dgvc.Name].Value);
            if (num.Length == 0)
            {
                MessageBoxEx.Show(dgvc.HeaderText + "不能为空！");
                dgv.CurrentCell = dgvr.Cells[dgvc.Name];
                return false;
            }
            else if (!RegexCheck.IsDecimalOrInt(num))
            {
                MessageBoxEx.Show(dgvc.HeaderText + "必须未数字！");
                dgv.CurrentCell = dgvr.Cells[dgvc.Name];
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查合计明细和结算单据和是否相同
        /// </summary>
        /// <returns></returns>
        bool CheckTotal()
        {
            decimal detailTotal = 0;//明细合计
            decimal documentTotal = 0;//单据合计
            foreach (DataGridViewRow dgvr in dgvPaymentDetail.Rows)
            {
                string detail = CommonCtrl.IsNullToString(dgvr.Cells[colMoney.Name].Value);
                if (detail.Length > 0)
                {
                    detailTotal += Convert.ToDecimal(detail);
                }
            }
            foreach (DataGridViewRow dgvr in dgvBalanceDocuments.Rows)
            {
                string paid = CommonCtrl.IsNullToString(dgvr.Cells[colPaidMoney.Name].Value);
                if (paid.Length > 0)
                {
                    documentTotal += Convert.ToDecimal(paid);
                }
            }
            if (detailTotal != documentTotal)
            {
                MessageBoxEx.Show("收/付款明细金额不等于结算单据实收金额！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证单据是否超出金额
        /// </summary>
        /// <returns></returns>
        bool CheckDocuments()
        {
            bool check = true;//验证结果
            foreach (DataGridViewRow dgvr in dgvBalanceDocuments.Rows)
            {
                string documentID = CommonCtrl.IsNullToString(dgvr.Cells[colDocumentsID.Name].Value);//单据ID
                decimal settlementMoney = Convert.ToDecimal(dgvr.Cells[colSettlementMoney.Name].Value);//本次结算
                DataTable dt = new DataTable();
                if (orderType == DataSources.EnumOrderType.RECEIVABLE)
                {
                    DataSources.EnumReceivableType type = (DataSources.EnumReceivableType)cboOrderType.SelectedValue;
                    if (type == DataSources.EnumReceivableType.RECEIVABLE)
                    {
                        dt = DBHelper.GetTable("", "v_parts_sale_billing_receivable", "*", string.Format("sale_billing_id='{0}'", documentID), "", "");
                    }
                }
                else
                {
                    DataSources.EnumPaymentType type = (DataSources.EnumPaymentType)cboOrderType.SelectedValue;
                    if (type == DataSources.EnumPaymentType.PAYMENT)
                    {
                        dt = DBHelper.GetTable("", "v_parts_purchase_billing_payment", "*", string.Format("purchase_billing_id='{0}'", documentID), "", "");
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    decimal waitMoney = Convert.ToDecimal(dt.Rows[0]["wait_money"]);
                    if (waitMoney < settlementMoney)
                    {
                        check = false;
                        MessageBoxEx.Show("超出待结算金额！");
                        dgvBalanceDocuments.CurrentCell = dgvr.Cells[colSettlementMoney.Name];
                        break;
                    }
                }
            }
            return check;
        }
        #endregion

        #region 生成sql
        /// <summary>
        /// 添加收付款sql
        /// </summary>
        /// <param name="list"></param>
        void AddReceivableSqlString(List<SysSQLString> list, DataSources.EnumAuditStatus auditStatus)
        {
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.Param.Add("payable_single_id", orderID);
            if (auditStatus == DataSources.EnumAuditStatus.DRAFT)
            {
                sql.Param.Add("order_num", null);//单号
            }
            else
            {
                if (txtOrderNum.Caption.Trim().Length == 0)
                {
                    if (orderType == DataSources.EnumOrderType.RECEIVABLE)
                    {
                        sql.Param.Add("order_num", CommonUtility.GetNewNo(DataSources.EnumProjectType.RECEIVABLE));
                    }
                    else
                    {
                        sql.Param.Add("order_num", CommonUtility.GetNewNo(DataSources.EnumProjectType.PAYMENT));
                    }
                }
                else
                {
                    sql.Param.Add("order_num", txtOrderNum.Caption.Trim());
                }
            }
            sql.Param.Add("order_date", Common.LocalDateTimeToUtcLong(dtpOrderDate.Value).ToString());//时间
            sql.Param.Add("order_status", ((int)auditStatus).ToString());//单据状态
            sql.Param.Add("order_type", ((int)orderType).ToString());//单据类型
            sql.Param.Add("cust_id", txtcCustName.Tag.ToString());
            sql.Param.Add("cust_code", cust_code);//往来单位
            sql.Param.Add("cust_name", txtcCustName.Text);//
            sql.Param.Add("payment_type", CommonCtrl.IsNullToString(cboOrderType.SelectedValue));//收付款类型
            sql.Param.Add("payment_money", txtAdvance.Caption.Trim());
            sql.Param.Add("dealings_balance", txtBalance.Caption.Trim());
            sql.Param.Add("bank_of_deposit", txtBankOfDeposit.Caption.Trim());//开户银行
            sql.Param.Add("bank_account", txtBankAccount.Caption.Trim());//银行账户
            sql.Param.Add("org_id", CommonCtrl.IsNullToString(cboOrgId.SelectedValue));//部门
            sql.Param.Add("handle", CommonCtrl.IsNullToString(cboHandle.SelectedValue));//经办人
            sql.Param.Add("operator", GlobalStaticObj.UserID);//操作人
            sql.Param.Add("remark", txtRemark.Caption);
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {

                sql.Param.Add("create_by", GlobalStaticObj.UserID);
                sql.Param.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
                sql.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                sql.sqlString = @"INSERT INTO [tb_bill_receivable]
           (payable_single_id,order_num ,order_date,order_status,order_type,cust_id,cust_code,cust_name,payment_type,payment_money,dealings_balance
,bank_of_deposit,bank_account,org_id,handle,operator,create_by,create_time,status,enable_flag,remark)
     VALUES
           (@payable_single_id,@order_num,@order_date,@order_status,@order_type,@cust_id,@cust_code,@cust_name,@payment_type,@payment_money,@dealings_balance
,@bank_of_deposit,@bank_account,@org_id,@handle,@operator,@create_by,@create_time,@status,@enable_flag,@remark);";
            }
            else if (windowStatus == WindowStatus.Edit)
            {
                sql.Param.Add("update_by", GlobalStaticObj.UserID);
                sql.Param.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                sql.sqlString = @"UPDATE [tb_bill_receivable] SET order_num =@order_num
,order_date = @order_date,order_status = @order_status,order_type = @order_type,cust_id=@cust_id,cust_code = @cust_code,cust_name = @cust_name
,payment_type = @payment_type,payment_money=@payment_money,dealings_balance=@dealings_balance,bank_of_deposit = @bank_of_deposit
,bank_account = @bank_account,org_id = @org_id,handle = @handle,operator = @operator,update_by = @update_by,update_time = @update_time
,remark = @remark where payable_single_id = @payable_single_id;";
            }
            list.Add(sql);
        }

        /// <summary>
        /// 添加收付款明细sql
        /// </summary>
        /// <param name="list"></param>
        void AddDetailSqlString(List<SysSQLString> list)
        {
            SysSQLString deleteSql = new SysSQLString();
            deleteSql.cmdType = CommandType.Text;
            deleteSql.sqlString = string.Format("delete tb_payment_detail where order_id='{0}'", orderID);
            deleteSql.Param = new Dictionary<string, string>();
            list.Add(deleteSql);
            foreach (DataGridViewRow dgvr in dgvPaymentDetail.Rows)
            {
                if (dgvr.IsNewRow)
                {
                    continue;
                }
                SysSQLString sql = new SysSQLString();
                sql.cmdType = CommandType.Text;
                sql.Param = new Dictionary<string, string>();
                sql.Param.Add("order_id", orderID);
                sql.Param.Add("money", CommonCtrl.IsNullToString(dgvr.Cells[colMoney.Name].Value));//金额
                sql.Param.Add("balance_way", CommonCtrl.IsNullToString(dgvr.Cells[colBalanceWay.Name].Value));//结算方式
                sql.Param.Add("payment_account", CommonCtrl.IsNullToString(dgvr.Cells[colPaymentAccount.Name].Value));//付款账户
                sql.Param.Add("bank_of_deposit", CommonCtrl.IsNullToString(dgvr.Cells[colBankOfDeposit.Name].Value));//开户银行
                sql.Param.Add("bank_account", CommonCtrl.IsNullToString(dgvr.Cells[colBankAccount.Name].Value));//银行账户
                sql.Param.Add("check_number", CommonCtrl.IsNullToString(dgvr.Cells[colCheckNumber.Name].Value));//票号
                sql.Param.Add("remark", CommonCtrl.IsNullToString(dgvr.Cells[colRemark.Name].Value));//备注
                string paymentDetailID = CommonCtrl.IsNullToString(dgvr.Cells[colPaymentDetailID.Name].Value);
                //if (paymentDetailID.Length == 0)
                //{
                sql.Param.Add("create_by", GlobalStaticObj.UserID);
                sql.Param.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                paymentDetailID = Guid.NewGuid().ToString();
                sql.sqlString = @"INSERT INTO [tb_payment_detail](payment_detail_id,order_id,money
,balance_way,payment_account,bank_of_deposit,bank_account,check_number,remark,create_by,create_time)
     VALUES
(@payment_detail_id,@order_id,@money,@balance_way,@payment_account,@bank_of_deposit,@bank_account,@check_number,@remark
,@create_by,@create_time)";
                //                }
                //                else
                //                {
                //                    sql.Param.Add("update_by", GlobalStaticObj.UserID);
                //                    sql.Param.Add("update_time", DateTime.UtcNow.Ticks.ToString());
                //                    sql.sqlString = @"UPDATE [hxc].[dbo].[tb_payment_detail]
                //   SET order_id = @order_id
                //      ,money = @money
                //      ,balance_way = @balance_way
                //      ,payment_account = @payment_account
                //      ,bank_of_deposit = @bank_of_deposit
                //      ,bank_account = @bank_account
                //      ,check_number = @check_number
                //      ,remark = @remark
                //      ,update_by = @update_by
                //      ,update_time = @update_time
                // WHERE payment_detail_id = @payment_detail_id;";
                //                }
                sql.Param.Add("payment_detail_id", paymentDetailID);
                list.Add(sql);
            }
        }

        /// <summary>
        /// 添加结算单据sql
        /// </summary>
        /// <param name="list"></param>
        void AddBalanceDocumentsSqlString(List<SysSQLString> list)
        {
            SysSQLString deleteSql = new SysSQLString();
            deleteSql.cmdType = CommandType.Text;
            deleteSql.Param = new Dictionary<string, string>();
            deleteSql.sqlString = string.Format("delete tb_balance_documents where order_id='{0}'", orderID);
            list.Add(deleteSql);
            foreach (DataGridViewRow dgvr in dgvBalanceDocuments.Rows)
            {
                SysSQLString sql = new SysSQLString();
                sql.cmdType = CommandType.Text;
                sql.Param = new Dictionary<string, string>();
                sql.Param.Add("order_id", orderID);
                sql.Param.Add("documents_name", CommonCtrl.IsNullToString(dgvr.Cells[colDocumentsName.Name].Value));//单据名称
                sql.Param.Add("documents_id", CommonCtrl.IsNullToString(dgvr.Cells[colDocumentsID.Name].Value));//单据ID
                sql.Param.Add("documents_num", CommonCtrl.IsNullToString(dgvr.Cells[colDocumentsNum.Name].Value));//单据号
                string orderDate = CommonCtrl.IsNullToString(dgvr.Cells[colDocumentsDate.Name].Value);
                if (orderDate.Length == 0)
                {
                    sql.Param.Add("documents_date", null);//单据日期
                }
                else
                {
                    sql.Param.Add("documents_date", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(orderDate)).ToString());
                }
                string billingMoney = CommonCtrl.IsNullToString(dgvr.Cells[colBillingMoney.Name].Value);
                sql.Param.Add("billing_money", billingMoney.Length == 0 ? null : billingMoney);//开单金额
                string settledMoney = CommonCtrl.IsNullToString(dgvr.Cells[colSettledMoney.Name].Value);
                sql.Param.Add("settled_money", settledMoney.Length == 0 ? null : settledMoney);//已结算金额
                string waitSettledMoney = CommonCtrl.IsNullToString(dgvr.Cells[colWaitSettledMoney.Name].Value);
                sql.Param.Add("wait_settled_money", waitSettledMoney.Length == 0 ? null : waitSettledMoney);//待结算金额
                string settledmentMoney = CommonCtrl.IsNullToString(dgvr.Cells[colSettlementMoney.Name].Value);
                sql.Param.Add("settlement_money", settledmentMoney.Length == 0 ? null : settledmentMoney);//本次结算
                bool gathering = Convert.ToBoolean(dgvr.Cells[colGathering.Name].Value);
                sql.Param.Add("gathering", gathering ? "1" : "0");//收款
                string paidMoney = CommonCtrl.IsNullToString(dgvr.Cells[colPaidMoney.Name].Value);
                sql.Param.Add("paid_money", paidMoney.Length == 0 ? null : paidMoney);//实收金额
                string depositRate = CommonCtrl.IsNullToString(dgvr.Cells[colDepositRate.Name].Value);
                sql.Param.Add("deposit_rate", depositRate.Length == 0 ? null : depositRate);//折扣率
                string deduction = CommonCtrl.IsNullToString(dgvr.Cells[colDeduction.Name].Value);
                sql.Param.Add("deduction", deduction.Length == 0 ? null : deduction);//折扣金额
                sql.Param.Add("remark", CommonCtrl.IsNullToString(dgvr.Cells[colDocumentRemark.Name].Value));//备注
                string balanceDocumentsID = CommonCtrl.IsNullToString(dgvr.Cells[colBalanceDocumentsID.Name].Value);//ID
                //if (balanceDocumentsID.Length == 0)
                //{
                sql.Param.Add("create_by", GlobalStaticObj.UserID);
                sql.Param.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                balanceDocumentsID = Guid.NewGuid().ToString();
                sql.sqlString = @"INSERT INTO [tb_balance_documents]
           (balance_documents_id
           ,order_id
           ,documents_name
           ,documents_id
           ,documents_num
           ,documents_date
           ,billing_money
           ,settled_money
           ,wait_settled_money
           ,settlement_money
           ,gathering
           ,paid_money
           ,deposit_rate
           ,deduction
           ,remark
           ,create_by
           ,create_time)
     VALUES
           (@balance_documents_id
           ,@order_id
           ,@documents_name
           ,@documents_id
           ,@documents_num
           ,@documents_date
           ,@billing_money
           ,@settled_money
           ,@wait_settled_money
           ,@settlement_money
           ,@gathering
           ,@paid_money
           ,@deposit_rate
           ,@deduction
           ,@remark
           ,@create_by
           ,@create_time)";
                //                }
                //                else
                //                {
                //                    sql.Param.Add("update_by", GlobalStaticObj.UserID);
                //                    sql.Param.Add("update_time", DateTime.UtcNow.Ticks.ToString());
                //                    sql.sqlString = @"UPDATE [hxc].[dbo].[tb_balance_documents]
                //   SET order_id = @order_id
                //      ,documents_name = @documents_name
                //      ,documents_num = @documents_num
                //      ,documents_date = @documents_date
                //      ,billing_money = @billing_money
                //      ,settled_money = @settled_money
                //      ,wait_settled_money = @wait_settled_money
                //      ,settlement_money = @settlement_money
                //      ,gathering = @gathering
                //      ,paid_money = @paid_money
                //      ,deposit_rate = @deposit_rate
                //      ,deduction = @deduction
                //      ,remark = @remark
                //      ,update_by = @update_by
                //      ,update_time = @update_time
                // WHERE balance_documents_id = @balance_documents_id;";
                //                }
                sql.Param.Add("balance_documents_id", balanceDocumentsID);
                list.Add(sql);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 设置显示
        /// </summary>
        void SetLable()
        {
            if (orderType == DataSources.EnumOrderType.PAYMENT)
            {
                lblOrderType.Text = "付款类型：";
                lblAdvance.Text = "预付金额：";
                colPaymentAccount.HeaderText = "付款账户";
                colGathering.HeaderText = "付款";
                colPaidMoney.HeaderText = "实付金额";
                lblCustName.Text = "供应商：";
            }
            else
            {
                lblCustName.Text = "客户：";
            }
        }
        /// <summary>
        /// 绑定收付款类型
        /// </summary>
        void BindOrderType()
        {
            cboOrderType.ValueMember = "Value";
            cboOrderType.DisplayMember = "Text";
            List<ListItem> listOrderType = new List<ListItem>();
            if (orderType == DataSources.EnumOrderType.RECEIVABLE)
            {
                listOrderType = DataSources.EnumToList(typeof(DataSources.EnumReceivableType), true);
            }
            else
            {
                listOrderType = DataSources.EnumToList(typeof(DataSources.EnumPaymentType), true);
            }
            listOrderType.RemoveAt(0);
            cboOrderType.DataSource = listOrderType;
        }

        /// <summary>
        /// 绑定部门
        /// </summary>
        void BindDepartment()
        {
            cboOrgId.DataSource = DBHelper.GetTable("", "tb_organization", "org_id,org_name", "", "", "");
            cboOrgId.ValueMember = "org_id";
            cboOrgId.DisplayMember = "org_name";
        }

        /// <summary>
        /// 绑定结算方式
        /// </summary>
        void BindBalanceWay()
        {
            colBalanceWay.DataSource = DBHelper.GetTable("", "tb_balance_way", "balance_way_id,balance_way_name,default_account", "", "", "");
            colBalanceWay.ValueMember = "balance_way_id";
            colBalanceWay.DisplayMember = "balance_way_name";
        }

        /// <summary>
        /// 绑定付款账户
        /// </summary>
        void BindAccount()
        {
            colPaymentAccount.DataSource = DBHelper.GetTable("", "v_cashier_account", "cashier_account,account_name,bank_name,bank_account", "", "", "");
            colPaymentAccount.ValueMember = "cashier_account";
            colPaymentAccount.DisplayMember = "account_name";
        }

        /// <summary>
        /// 绑定原数据
        /// </summary>
        void BindData()
        {
            DataTable dt = DBHelper.GetTable("", "v_bill_receivable", "*", string.Format("payable_single_id='{0}'", orderID), "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            if (windowStatus != WindowStatus.Copy)
            {
                txtOrderNum.Caption = CommonCtrl.IsNullToString(dr["order_num"]);
            }
            if (dr["order_date"] != null && dr["order_date"] != DBNull.Value)
            {
                dtpOrderDate.Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["order_date"]));
            }
            txtOrderStatus.Caption = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), dr["order_status"]);
            txtcCustName.Text = CommonCtrl.IsNullToString(dr["cust_name"]);
            txtcCustName.Tag = dr["cust_id"];
            cust_code = CommonCtrl.IsNullToString(dr["cust_code"]);
            txtRemark.Caption = CommonCtrl.IsNullToString(dr["remark"]);
            cboOrderType.SelectedValue = Convert.ToInt32(dr["payment_type"]);
            txtAdvance.Caption = CommonCtrl.IsNullToString(dr["payment_money"]);
            txtBalance.Caption = CommonCtrl.IsNullToString(dr["dealings_balance"]);
            txtBankOfDeposit.Caption = CommonCtrl.IsNullToString(dr["bank_of_deposit"]);
            txtBankAccount.Caption = CommonCtrl.IsNullToString(dr["bank_account"]);
            cboOrgId.SelectedValue = dr["org_id"];
            cboHandle.SelectedValue = dr["handle"];
            if (windowStatus == WindowStatus.Copy)
            {
                lblCreateBy.Text = GlobalStaticObj.UserName;
                lblCreateTime.Text = DateTime.Now.ToString();
                lblUpdateBy.Text = string.Empty;
                lblUpdateTime.Text = string.Empty;
            }
            else if (windowStatus == WindowStatus.Edit || windowStatus == WindowStatus.View)
            {
                lblCreateBy.Text = CommonCtrl.IsNullToString(dr["create_by_name"]);
                string createTime = CommonCtrl.IsNullToString(dr["create_time"]);
                if (createTime.Length > 0)
                {
                    lblCreateTime.Text = Common.UtcLongToLocalDateTime(Int64.Parse(createTime)).ToString();
                }
                lblUpdateBy.Text = CommonCtrl.IsNullToString(dr["update_by_name"]);
                string updateTime = CommonCtrl.IsNullToString(dr["update_time"]);
                if (updateTime.Length > 0)
                {
                    lblUpdateTime.Text = Common.UtcLongToLocalDateTime(Int64.Parse(updateTime)).ToString();
                }
                else
                {
                    lblUpdateTime.Text = string.Empty;
                }
            }
            string auditStatus = CommonCtrl.IsNullToString(dr["order_status"]);
            DataSources.EnumAuditStatus enumAuditStatus = DataSources.EnumAuditStatus.DRAFT;
            if (auditStatus != "")
            {
                enumAuditStatus = (DataSources.EnumAuditStatus)Convert.ToInt32(auditStatus);
            }
            switch (enumAuditStatus)
            {
                case DataSources.EnumAuditStatus.DRAFT:
                case DataSources.EnumAuditStatus.NOTAUDIT:
                    btnVerify.Enabled = false;
                    enumActivation = DataSources.EnumInvalidOrActivation.Invalid;
                    break;
                case DataSources.EnumAuditStatus.SUBMIT:
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnActivation.Enabled = false;
                    btnSubmit.Enabled = false;
                    break;
                case DataSources.EnumAuditStatus.AUDIT:
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnActivation.Enabled = false;
                    btnSubmit.Enabled = false;
                    btnVerify.Enabled = false;
                    break;
                case DataSources.EnumAuditStatus.Invalid:
                    btnCopy.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnActivation.Caption = "激活";
                    btnSubmit.Enabled = false;
                    btnVerify.Enabled = false;
                    btnExport.Enabled = false;
                    btnSet.Enabled = false;
                    btnView.Enabled = false;
                    btnPrint.Enabled = false;
                    break;
            }
            //判断是提交
            //if (((int)DataSources.EnumAuditStatus.SUBMIT).ToString() == CommonCtrl.IsNullToString(dr["order_status"]))
            //{
            //    btnVerify.Enabled = true;
            //}
            //else
            //{
            //    btnVerify.Enabled = false;
            //}
            //不复制明细
            if (windowStatus != WindowStatus.Copy)
            {
                BindDetail();
                BindBalanceDocuments();
            }
        }

        /// <summary>
        /// 绑定应收应付明细
        /// </summary>
        void BindDetail()
        {
            DataTable dt = DBHelper.GetTable("", "tb_payment_detail", "*", string.Format("order_id='{0}'", orderID), "", "order by create_time");
            dgvPaymentDetail.Rows.Clear();
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow dgvr = dgvPaymentDetail.Rows[dgvPaymentDetail.Rows.Add()];
                dgvr.Cells[colPaymentDetailID.Name].Value = dr["payment_detail_id"];
                dgvr.Cells[colMoney.Name].Value = dr["money"];
                dgvr.Cells[colBalanceWay.Name].Value = dr["balance_way"];
                dgvr.Cells[colPaymentAccount.Name].Value = dr["payment_account"];
                dgvr.Cells[colBankOfDeposit.Name].Value = dr["bank_of_deposit"];
                dgvr.Cells[colBankAccount.Name].Value = dr["bank_account"];
                dgvr.Cells[colCheckNumber.Name].Value = dr["check_number"];
                dgvr.Cells[colRemark.Name].Value = dr["remark"];
            }
        }

        /// <summary>
        /// 绑定结算单据
        /// </summary>
        void BindBalanceDocuments()
        {
            DataTable dt = DBHelper.GetTable("", "tb_balance_documents", "*", string.Format("order_id='{0}'", orderID), "", "");
            dgvBalanceDocuments.Rows.Clear();
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow dgvr = dgvBalanceDocuments.Rows[dgvBalanceDocuments.Rows.Add()];
                dgvr.Cells[colBalanceDocumentsID.Name].Value = dr["balance_documents_id"];
                dgvr.Cells[colDocumentsName.Name].Value = dr["documents_name"];
                dgvr.Cells[colDocumentsID.Name].Value = dr["documents_id"];
                dgvr.Cells[colDocumentsNum.Name].Value = dr["documents_num"];
                string orderDate = CommonCtrl.IsNullToString(dr["documents_date"]);
                if (orderDate.Length == 0)
                {
                    dgvr.Cells[colDocumentsDate.Name].Value = null;
                }
                else
                {
                    dgvr.Cells[colDocumentsDate.Name].Value = Common.UtcLongToLocalDateTime(orderDate);
                }
                dgvr.Cells[colBillingMoney.Name].Value = dr["billing_money"];
                dgvr.Cells[colSettledMoney.Name].Value = dr["settled_money"];
                dgvr.Cells[colWaitSettledMoney.Name].Value = dr["wait_settled_money"];
                dgvr.Cells[colSettlementMoney.Name].Value = dr["settlement_money"];
                dgvr.Cells[colGathering.Name].Value = CommonCtrl.IsNullToString(dr["gathering"]) == "1";
                dgvr.Cells[colPaidMoney.Name].Value = dr["paid_money"];
                dgvr.Cells[colDepositRate.Name].Value = dr["deposit_rate"];
                dgvr.Cells[colDeduction.Name].Value = dr["deduction"];
                dgvr.Cells[colDocumentRemark.Name].Value = dr["remark"];
            }
        }
        #endregion

        #region 事件
        //往来单位选择
        private void txtcCustName_ChooserClick(object sender, EventArgs e)
        {
            //应收选择客户
            if (orderType == DataSources.EnumOrderType.RECEIVABLE)
            {
                frmCustomerInfo frm = new frmCustomerInfo();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtcCustName.Text = frm.strCustomerName;
                    txtcCustName.Tag = frm.strCustomerId;
                    cust_code = frm.strCustomerNo;
                    //字动带出账户信息
                    DataTable dt = DBHelper.GetTable("", "tb_customer", "*", string.Format("cust_id='{0}'", frm.strCustomerId), "", "");
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        return;
                    }
                    txtBankAccount.Caption = CommonCtrl.IsNullToString(dt.Rows[0]["bank_account"]);
                    txtBankOfDeposit.Caption = CommonCtrl.IsNullToString(dt.Rows[0]["open_bank"]);
                    txtAdvance.Caption = DBOperation.WaiLaiYuE(frm.strCustomerId, orderType).ToString();
                    //txtBalance.Caption = DBOperation.WaiLaiYuE(frm.strCustomerId, orderType).ToString();
                }
            }
            else if (orderType == DataSources.EnumOrderType.PAYMENT)//应付选择供应商
            {
                frmSupplier frmSupp = new frmSupplier();
                if (frmSupp.ShowDialog() == DialogResult.OK)
                {
                    txtcCustName.Text = frmSupp.supperName;
                    txtcCustName.Tag = frmSupp.supperID;
                    cust_code = frmSupp.supperCode;
                    //txtBalance.Caption = DBOperation.WaiLaiYuE(frmSupp.supperID, orderType).ToString();
                    txtAdvance.Caption = DBOperation.WaiLaiYuE(frmSupp.supperID, orderType).ToString();
                }
            }
        }

        //单据类型选择
        private void cboOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = CommonCtrl.IsNullToString(cboOrderType.SelectedValue);
            if (type.Length <= 0)
            {
                return;
            }
            bool isVisible = true;
            if (orderType == DataSources.EnumOrderType.RECEIVABLE)
            {
                DataSources.EnumReceivableType enumOrderType = (DataSources.EnumReceivableType)int.Parse(type);
                if (enumOrderType == DataSources.EnumReceivableType.RECEIVABLE)
                {
                    isVisible = true;
                }
                else if (enumOrderType == DataSources.EnumReceivableType.ADVANCES)
                {
                    isVisible = false;
                }
            }
            else if (orderType == DataSources.EnumOrderType.PAYMENT)
            {
                DataSources.EnumPaymentType enumType = (DataSources.EnumPaymentType)int.Parse(type);
                switch (enumType)
                {
                    case DataSources.EnumPaymentType.ADVANCES:
                        isVisible = false;
                        break;
                    case DataSources.EnumPaymentType.PAYMENT:
                        isVisible = true;
                        break;
                }
            }
            colSettledMoney.Visible = isVisible;
            colWaitSettledMoney.Visible = isVisible;
            colGathering.Visible = isVisible;
            colPaidMoney.Visible = isVisible;
            colDepositRate.Visible = isVisible;
            colDeduction.Visible = isVisible;
            dgvBalanceDocuments.Rows.Clear();
            dgvPaymentDetail.Rows.Clear();
        }
        //部门选择
        private void cboOrgId_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgID = CommonCtrl.IsNullToString(cboOrgId.SelectedValue);
            if (orgID.Length == 0)
            {
                return;
            }
            DataTable dt = DBHelper.GetTable("", "sys_user", "user_id,user_name", string.Format("org_id='{0}'", orgID), "", "");
            cboHandle.DataSource = dt;
            cboHandle.ValueMember = "user_id";
            cboHandle.DisplayMember = "user_name";
        }

        //结算方式更改
        void cboBalanceWay_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboBalanceWay == null)
            {
                return;
            }
            if (cboBalanceWay.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cboBalanceWay.SelectedItem;
                dgvPaymentDetail.CurrentRow.Cells[colPaymentAccount.Name].Value = drv["default_account"];
                DataGridViewComboBoxCell dgcbc = (DataGridViewComboBoxCell)dgvPaymentDetail.CurrentRow.Cells[colPaymentAccount.Name];
                DataTable dt = (DataTable)dgcbc.DataSource;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["cashier_account"].Equals(drv["default_account"]))
                    {
                        dgvPaymentDetail.CurrentRow.Cells[colBankAccount.Name].Value = dr["bank_account"];
                        dgvPaymentDetail.CurrentRow.Cells[colBankOfDeposit.Name].Value = dr["bank_name"];
                        break;
                    }
                }
            }
        }
        //付款账户更改
        void cboPaymentAccount_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboPaymentAccount == null)
            {
                return;
            }
            if (cboPaymentAccount.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)cboPaymentAccount.SelectedItem;
                dgvPaymentDetail.CurrentRow.Cells[colBankAccount.Name].Value = dr["bank_account"];
                dgvPaymentDetail.CurrentRow.Cells[colBankOfDeposit.Name].Value = dr["bank_name"];
            }
        }

        //添加明细
        private void dgvPaymentDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvPaymentDetail.CurrentCell.OwningColumn == colPaymentAccount)
            {
                cboPaymentAccount = (DataGridViewComboBoxEditingControl)e.Control;
                cboPaymentAccount.SelectedIndexChanged += new EventHandler(cboPaymentAccount_SelectedValueChanged);
            }
            else if (dgvPaymentDetail.CurrentCell.OwningColumn == colBalanceWay)
            {
                cboBalanceWay = (DataGridViewComboBoxEditingControl)e.Control;
                cboBalanceWay.SelectedIndexChanged += new EventHandler(cboBalanceWay_SelectedValueChanged);
            }
        }
        //结束编辑
        private void dgvPaymentDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (cboPaymentAccount != null)
            {
                cboPaymentAccount.SelectedValueChanged -= new EventHandler(cboPaymentAccount_SelectedValueChanged);
                cboPaymentAccount = null;
            }
            if (cboBalanceWay != null)
            {
                cboBalanceWay = null;
            }
        }
        //单据单元格修改
        private void dgvBalanceDocuments_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (dgvBalanceDocuments.Rows[e.RowIndex].Cells[colDocumentsName.Name].Value == null)
            {
                return;
            }
            switch (dgvBalanceDocuments.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name)
            {
                case "colPaidMoney"://实收金额
                    string settlementMoney = CommonCtrl.IsNullToString(dgvBalanceDocuments.Rows[e.RowIndex].Cells[colSettlementMoney.Name].Value);//本次结算金额
                    string paidMoney = CommonCtrl.IsNullToString(dgvBalanceDocuments.Rows[e.RowIndex].Cells[colPaidMoney.Name].Value);//实收金额
                    if (settlementMoney.Length == 0 || paidMoney.Length == 0)
                    {
                        dgvBalanceDocuments.Rows[e.RowIndex].Cells[colDepositRate.Name].Value = null;
                        dgvBalanceDocuments.Rows[e.RowIndex].Cells[colDeduction.Name].Value = null;
                        break;
                    }
                    dgvBalanceDocuments.Rows[e.RowIndex].Cells[colDeduction.Name].Value = decimal.Parse(settlementMoney) - decimal.Parse(paidMoney);
                    dgvBalanceDocuments.Rows[e.RowIndex].Cells[colDepositRate.Name].Value = settlementMoney == "0" ? 0 : (decimal.Parse(paidMoney) / decimal.Parse(settlementMoney)) * 100;
                    break;
                case "colSettlementMoney"://本次结算
                    //实收金额等于本次结算金额
                    dgvBalanceDocuments.Rows[e.RowIndex].Cells[colPaidMoney.Name].Value = dgvBalanceDocuments.Rows[e.RowIndex].Cells[colSettlementMoney.Name].Value;
                    //折扣额为空
                    dgvBalanceDocuments.Rows[e.RowIndex].Cells[colDeduction.Name].Value = null;
                    //折扣率为100%
                    dgvBalanceDocuments.Rows[e.RowIndex].Cells[colDepositRate.Name].Value = 100;
                    break;
            }
        }
        //点击收款
        private void dgvBalanceDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBalanceDocuments.ReadOnly)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == colGathering.Index)//点击收款
            {
                bool gathering = Convert.ToBoolean(dgvBalanceDocuments.Rows[e.RowIndex].Cells[colGathering.Name].EditedFormattedValue);
                if (gathering)
                {
                    object waitMoney = dgvBalanceDocuments.Rows[e.RowIndex].Cells[colWaitSettledMoney.Name].Value;
                    dgvBalanceDocuments.Rows[e.RowIndex].Cells[colSettlementMoney.Name].Value = waitMoney;
                    dgvBalanceDocuments.Rows[e.RowIndex].Cells[colPaidMoney.Name].Value = waitMoney;
                }
                else
                {
                    dgvBalanceDocuments.Rows[e.RowIndex].Cells[colSettlementMoney.Name].Value = null;
                    dgvBalanceDocuments.Rows[e.RowIndex].Cells[colPaidMoney.Name].Value = null;
                }
            }
        }
        #endregion

        #region 导入
        /// <summary>
        /// 维修导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRepair_Click(object sender, EventArgs e)
        {
            frmRepairByFinance frmRepair = new frmRepairByFinance(txtcCustName.Tag.ToString());
            if (frmRepair.ShowDialog() == DialogResult.OK)
            {
                BindDocument(frmRepair.listRows, false);
            }
        }
        /// <summary>
        /// 销售开单导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSale_Click(object sender, EventArgs e)
        {
            frmReceivable frm = new frmReceivable(txtcCustName.Tag.ToString());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                BindDocument(frm.listRows, false);
            }

        }
        /// <summary>
        /// 三包单导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiThree_Click(object sender, EventArgs e)
        {
            frmThreeByFinance frm = new frmThreeByFinance(txtcCustName.Tag.ToString());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                BindDocument(frm.listRows, false);
            }
        }

        /// <summary>
        /// 绑定单据
        /// </summary>
        /// <param name="listRows"></param>
        /// <param name="isYu">是否是预收</param>
        void BindDocument(List<DataGridViewRow> listRows, bool isYu)
        {
            dgvBalanceDocuments.Rows.Clear();
            foreach (DataGridViewRow dgvrd in listRows)
            {
                DataGridViewRow dgvr = dgvBalanceDocuments.Rows[dgvBalanceDocuments.Rows.Add()];
                dgvr.Cells[colDocumentsName.Name].Value = dgvrd.Cells["colBillsName"].Value;//单据名称
                dgvr.Cells[colDocumentsID.Name].Value = dgvrd.Cells["colID"].Value;//单据ID
                dgvr.Cells[colDocumentsNum.Name].Value = dgvrd.Cells["colBillsCode"].Value;//单据编号
                dgvr.Cells[colDocumentsDate.Name].Value = dgvrd.Cells["colReceivablesDate"].Value;//单据日期
                dgvr.Cells[colBillingMoney.Name].Value = dgvrd.Cells["colTotalMoney"].Value;//开单金额
                dgvr.Cells[colGathering.Name].Value = true;//默认选择
                dgvr.Cells[colSettlementMoney.Name].Value = dgvrd.Cells["colWaitMoney"].Value;//本次结算
                dgvr.Cells[colPaidMoney.Name].Value = dgvrd.Cells["colWaitMoney"].Value;//实收金额
                if (!isYu)
                {
                    dgvr.Cells[colSettledMoney.Name].Value = dgvrd.Cells["colBalanceMoney"].Value;//已结算金额
                    dgvr.Cells[colWaitSettledMoney.Name].Value = dgvrd.Cells["colWaitMoney"].Value;//未结算金额
                }
            }
        }
        #endregion

        #region 删除明细
        //删除明细
        private void tsmiDeleteDetail_Click(object sender, EventArgs e)
        {
            DeleteRow(dgvPaymentDetail, colCheckDetail);
        }
        //删除单据
        private void tsmiDeleteDocument_Click(object sender, EventArgs e)
        {
            UnlockDocument(true);
            DeleteRow(dgvBalanceDocuments, colCheckDocument);
        }
        /// <summary>
        /// 删除DataGridView选择的行
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="dgvcc">选择的复选框</param>
        void DeleteRow(DataGridView dgv, DataGridViewCheckBoxColumn dgvcc)
        {
            dgv.EndEdit();
            for (int i = dgv.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow dgvr = dgv.Rows[i];
                object check = dgvr.Cells[dgvcc.Name].Value;
                if (check != null && (bool)check)
                {
                    dgv.Rows.Remove(dgvr);
                }
            }
        }
        #endregion
    }
}
