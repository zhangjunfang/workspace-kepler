using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using SYSModel;
using HXCPcClient.Chooser;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using ServiceStationClient.ComponentUI.TextBox;
using System.Collections.ObjectModel;

namespace HXCPcClient.UCForm.FinancialManagement.AccountVerification
{
    public partial class UCAccountVerificationAdd : UCBase
    {
        #region 属性
        string id = string.Empty;//当前单据ID
        UCAccountVerificationManage uc;
        bool isBind = false;//是否是自动绑定
        string cust_code1 = string.Empty;//往来单位1的编码
        string cust_code2 = string.Empty;//往来单位2的编码
        DataSources.EnumInvalidOrActivation enumActivation = DataSources.EnumInvalidOrActivation.Activation;//激活状态
        bool isAutoClose = false;//是否自动关闭
        #endregion
        public UCAccountVerificationAdd(WindowStatus status, string id, UCAccountVerificationManage uc)
        {
            InitializeComponent();
            this.windowStatus = status;
            this.id = id;
            this.uc = uc;
            colVerificationMoney.ValueType = typeof(decimal);
            colMoney.ValueType = typeof(decimal);
            colSettledMoney.ValueType = typeof(decimal);
            colWaitMoney.ValueType = typeof(decimal);
            colVerificationMoney2.ValueType = typeof(Decimal);
            colMoney2.ValueType = typeof(decimal);
            colSettledMoney2.ValueType = typeof(decimal);
            colWaitMoney2.ValueType = typeof(decimal);
            base.SaveEvent += new ClickHandler(UCAccountVerificationAdd_SaveEvent);
            base.ImportEvent += new ClickHandler(UCAccountVerificationAdd_ImportEvent);
            base.SubmitEvent += new ClickHandler(UCAccountVerificationAdd_SubmitEvent);
            base.CancelEvent += new ClickHandler(UCAccountVerificationAdd_CancelEvent);
            base.VerifyEvent += new ClickHandler(UCAccountVerificationAdd_VerifyEvent);
            base.InvalidOrActivationEvent += new ClickHandler(UCAccountVerificationAdd_InvalidOrActivationEvent);
            base.CopyEvent += new ClickHandler(UCAccountVerificationAdd_CopyEvent);
            base.EditEvent += new ClickHandler(UCAccountVerificationAdd_EditEvent);
            base.DeleteEvent += new ClickHandler(UCAccountVerificationAdd_DeleteEvent);
            DataGridViewEx.SetDataGridViewStyle(dgvDocuments, colRemark);
            DataGridViewEx.SetDataGridViewStyle(dgvDocuments2, colRemark2);
            dgvDocuments.RowHeadersVisible = true;
            dgvDocuments2.RowHeadersVisible = true;
        }


        //加载事件
        private void UCAccountVerificationAdd_Load(object sender, EventArgs e)
        {
            string[] total = { colVerificationMoney.Name };
            ControlsConfig.DatagGridViewTotalConfig(dgvDocuments, total);
            ControlsConfig.DatagGridViewTotalConfig(dgvDocuments2, null);
            dtpOrderDate.Value = DateTime.Now;
            //base.SetBtnStatus(windowStatus);
            SetLable();
            CommonFuncCall.BindDepartment(cboOrgId, GlobalStaticObj.CurrUserCom_Id, "请选择");
            BindOrderType();
            if (windowStatus != WindowStatus.View)
            {
                dgvDocuments.ReadOnly = false;
                //dgvDocuments.AllowUserToAddRows = true;
                colOrderDate.ReadOnly = true;
                colOrderName.ReadOnly = true;
                colOrderNum.ReadOnly = true;
                colMoney.ReadOnly = true;
                colSettledMoney.ReadOnly = true;
                colWaitMoney.ReadOnly = true;

                dgvDocuments2.ReadOnly = false;
                //dgvDocuments2.AllowUserToAddRows = true;
                colOrderDate2.ReadOnly = true;
                colOrderName2.ReadOnly = true;
                colOrderNum2.ReadOnly = true;
                colMoney2.ReadOnly = true;
                colSettledMoney2.ReadOnly = true;
                colWaitMoney2.ReadOnly = true;
                //设置页面按钮可见性
                var btnCols = new ObservableCollection<ButtonEx_sms>
                {
                    btnSubmit,btnSave,btnCancel,btnImport,btnExport,btnSet,btnView,btnPrint
                };
                UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            }
            else
            {
                dgvDocuments.ContextMenuStrip = null;
                dgvDocuments2.ContextMenuStrip = null;
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
            if ((this.windowStatus == WindowStatus.Edit || this.windowStatus == WindowStatus.Copy || this.windowStatus == WindowStatus.View) && id.Length > 0)
            {
                isBind = true;
                BindData();
                isBind = false;
            }
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                //txtOrderNum.Caption = CommonUtility.GetNewNo(DataSources.EnumProjectType.AccountVerification);
                dtpOrderDate.Value = DateTime.Now;
                txtOrderStatus.Caption = "草稿";
                lblCreateBy.Text = GlobalStaticObj.UserName;//创建人
                lblCreateTime.Text = DateTime.Now.ToString();//创建时间
                lblUpdateBy.Text = string.Empty;
                lblUpdateTime.Text = string.Empty;
            }
        }
        #region 工具栏事件

        public override bool CloseMenu()
        {
            if (isAutoClose)
            {
                return true;
            }
            if (MessageBoxEx.Show("确定要关闭当前窗体吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return false;
            }
            if (windowStatus != WindowStatus.View)
            {
                UnOccupyDocument(false);
            }
            return true;
        }

        void UCAccountVerificationAdd_DeleteEvent(object sender, EventArgs e)
        {
            DeleteData();
        }

        void UCAccountVerificationAdd_EditEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Edit);
        }

        void UCAccountVerificationAdd_CopyEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Copy);
        }
        //审核
        void UCAccountVerificationAdd_VerifyEvent(object sender, EventArgs e)
        {
            VerifyData();
        }
        //激活/作废
        void UCAccountVerificationAdd_InvalidOrActivationEvent(object sender, EventArgs e)
        {
            InvalidOrActivation();
        }
        //取消
        void UCAccountVerificationAdd_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        //提交
        void UCAccountVerificationAdd_SubmitEvent(object sender, EventArgs e)
        {
            Save(DataSources.EnumAuditStatus.SUBMIT);
        }
        //导入
        void UCAccountVerificationAdd_ImportEvent(object sender, EventArgs e)
        {
            Import();
        }
        //保存
        void UCAccountVerificationAdd_SaveEvent(object sender, EventArgs e)
        {
            Save(DataSources.EnumAuditStatus.DRAFT);
        }
        //导入应收款单
        private void tsmiCust1_Click(object sender, EventArgs e)
        {
            DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(cboOrderType.SelectedValue);
            switch (enumAccount)
            {
                case DataSources.EnumAccountVerification.YingShouToYingFu://应收冲应付
                    ImportYingShou(txtcCustName1, lblCustName1);
                    break;
                case DataSources.EnumAccountVerification.YingFuToYingShou://应付冲应收
                    ImportYingShou2(txtcCustName2, lblCustName2);
                    break;
            }
        }
        //导入应付款单
        private void tsmiCust2_Click(object sender, EventArgs e)
        {
            DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(cboOrderType.SelectedValue);
            switch (enumAccount)
            {
                case DataSources.EnumAccountVerification.YingFuToYingShou://应付冲应收
                    ImportYingFu(txtcCustName1, lblCustName1);
                    break;
                case DataSources.EnumAccountVerification.YingShouToYingFu://应收冲应付
                    ImportYingFu2(txtcCustName2, lblCustName2);
                    break;
            }
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入
        /// </summary>
        void Import()
        {
            if (windowStatus == WindowStatus.View)
            {
                return;
            }
            if (cboOrderType.SelectedValue == null)
            {
                MessageBoxEx.Show("请选择单据类型！");
                cboOrderType.Focus();
                return;
            }
            DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(cboOrderType.SelectedValue);
            switch (enumAccount)
            {
                case DataSources.EnumAccountVerification.YuShouToYingShou://预收冲应收
                case DataSources.EnumAccountVerification.YingShouToYingShou://应收转应收
                    ImportYingShou(txtcCustName2, lblCustName2);
                    break;
                //case DataSources.EnumAccountVerification.YuShouToYuShou://预收转预收
                //    ImportYuShou(txtcCustName2, lblCustName2);
                //break;
                case DataSources.EnumAccountVerification.YingShouToYingFu://应收冲应付
                case DataSources.EnumAccountVerification.YingFuToYingShou://应付冲应收
                    this.cmsImport.Show(base.btnImport, 0, btnImport.Height);
                    break;
                case DataSources.EnumAccountVerification.YuFuToYingFu://预付冲应付
                case DataSources.EnumAccountVerification.YingFuToYingFu://应付转应付
                    ImportYingFu(txtcCustName2, lblCustName2);
                    break;
                //case DataSources.EnumAccountVerification.YuFuToYuFu://预付冲预付
                //    ImportYuFu(txtcCustName2, lblCustName2);
                //break;
            }
        }

        /// <summary>
        /// 导入预收
        /// </summary>
        void ImportYuShou(TextChooser txtcCust, Label lblCust)
        {
            if (txtcCust.Tag == null)
            {
                MessageBoxEx.Show(string.Format("请选择{0}！", lblCust.Text.TrimEnd('：')));
                txtcCust.Focus();
                return;
            }
            //frmBalanceDocuments frmDocumentsYu = new frmBalanceDocuments(DataSources.EnumOrderType.RECEIVABLE, txtcCust.Tag.ToString(), ((int)DataSources.EnumReceivableType.ADVANCES).ToString());
            frmReceivableByAdvances frmDocumentsYu = new frmReceivableByAdvances(txtcCust.Tag.ToString());
            if (frmDocumentsYu.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sbWhere = new StringBuilder();
                sbWhere.AppendFormat("sale_order_id in ({0})", frmDocumentsYu.ids);
                sbWhere.AppendFormat(" and wait_money>0");
                DataTable dt = DBHelper.GetTable("", "v_parts_sale_order_receivable", "*", sbWhere.ToString(), "", "");
                foreach (DataRow dr in dt.Rows)
                {
                    DataGridViewRow dgvr = dgvDocuments.Rows[dgvDocuments.Rows.Add()];
                    dgvr.Cells[colOrderName.Name].Value = "销售订单";//单据名称
                    dgvr.Cells[colOrderNum.Name].Value = dr["order_num"];//单据编号
                    dgvr.Cells[colOrderID.Name].Value = dr["sale_billing_id"];//单据ID
                    //dgvr.Cells[colOrderDate.Name].Value = Common.UtcLongToLocalDateTime(dr["receivables_date"]);//单据日期
                    dgvr.Cells[colMoney.Name].Value = dr["advance_money"];//开单金额
                    dgvr.Cells[colSettledMoney.Name].Value = dr["money"];//已结算金额
                    dgvr.Cells[colWaitMoney.Name].Value = dr["wait_money"];//未结算金额
                }
            }
        }
        /// <summary>
        /// 导入应收
        /// </summary>
        /// <param name="txtcCust"></param>
        /// <param name="lblCust"></param>
        void ImportYingShou(TextChooser txtcCust, Label lblCust)
        {
            if (txtcCust.Tag == null)
            {
                MessageBoxEx.Show(string.Format("请选择{0}！", lblCust.Text.TrimEnd('：')));
                txtcCust.Focus();
                return;
            }
            //frmReceivable frmDocuments = new frmReceivable(txtcCust.Tag.ToString());
            frmReceivableByVerification frmDocuments = new frmReceivableByVerification(txtcCust.Tag.ToString());
            if (frmDocuments.ShowDialog() == DialogResult.OK)
            {
                BindDocuments(frmDocuments.listRows);
            }
        }

        /// <summary>
        /// 导入应收
        /// </summary>
        /// <param name="txtcCust"></param>
        /// <param name="lblCust"></param>
        void ImportYingShou2(TextChooser txtcCust, Label lblCust)
        {
            if (txtcCust.Tag == null)
            {
                MessageBoxEx.Show(string.Format("请选择{0}！", lblCust.Text.TrimEnd('：')));
                txtcCust.Focus();
                return;
            }
            //frmReceivable frmDocuments = new frmReceivable(txtcCust.Tag.ToString());
            frmReceivableByVerification frmDocuments = new frmReceivableByVerification(txtcCust.Tag.ToString());
            if (frmDocuments.ShowDialog() == DialogResult.OK)
            {
                BindDocuments2(frmDocuments.listRows);
            }
        }
        /// <summary>
        /// 导入预付
        /// </summary>
        /// <param name="txtcCust"></param>
        /// <param name="lblCust"></param>
        void ImportYuFu(TextChooser txtcCust, Label lblCust)
        {
            if (txtcCust.Tag == null)
            {
                MessageBoxEx.Show(string.Format("请选择{0}！", lblCust.Text.TrimEnd('：')));
                txtcCust.Focus();
                return;
            }
            //frmBalanceDocuments frmDocuments = new frmBalanceDocuments(DataSources.EnumOrderType.PAYMENT, txtcCust.Tag.ToString(), ((int)DataSources.EnumPaymentType.ADVANCES).ToString());
            frmPaymentByAdvances frmDocuments = new frmPaymentByAdvances(txtcCust.Tag.ToString());
            if (frmDocuments.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sbWhere = new StringBuilder();
                sbWhere.AppendFormat("order_id in ({0})", frmDocuments.ids);
                sbWhere.AppendFormat(" and wait_money>0");
                DataTable dt = DBHelper.GetTable("", "v_parts_purchase_order_payment", "*", sbWhere.ToString(), "", "");
                foreach (DataRow dr in dt.Rows)
                {
                    DataGridViewRow dgvr = dgvDocuments.Rows[dgvDocuments.Rows.Add()];
                    dgvr.Cells[colOrderName.Name].Value = "采购订单";//单据名称
                    dgvr.Cells[colOrderNum.Name].Value = dr["order_num"];//单据编号
                    dgvr.Cells[colOrderID.Name].Value = dr["sale_billing_id"];//单据ID
                    //dgvr.Cells[colOrderDate.Name].Value = Common.UtcLongToLocalDateTime(dr["receivables_date"]);//单据日期
                    dgvr.Cells[colMoney.Name].Value = dr["prepaid_money"];//开单金额
                    dgvr.Cells[colSettledMoney.Name].Value = dr["money"];//已结算金额
                    dgvr.Cells[colWaitMoney.Name].Value = dr["wait_money"];//未结算金额
                }
            }
        }
        /// <summary>
        /// 导入应付
        /// </summary>
        /// <param name="txtcCust"></param>
        /// <param name="lblCust"></param>
        void ImportYingFu(TextChooser txtcCust, Label lblCust)
        {
            if (txtcCust.Tag == null)
            {
                MessageBoxEx.Show(string.Format("请选择{0}！", lblCust.Text.TrimEnd('：')));
                txtcCust.Focus();
                return;
            }
            //frmBalanceDocuments frmDocuments = new frmBalanceDocuments(DataSources.EnumOrderType.PAYMENT, txtcCust.Tag.ToString(), ((int)DataSources.EnumPaymentType.PAYMENT).ToString());
            frmPayment frmDocuments = new frmPayment(txtcCust.Tag.ToString());
            if (frmDocuments.ShowDialog() == DialogResult.OK)
            {
                BindDocuments(frmDocuments.listRows);
            }
        }
        /// <summary>
        /// 导入应付
        /// </summary>
        /// <param name="txtcCust"></param>
        /// <param name="lblCust"></param>
        void ImportYingFu2(TextChooser txtcCust, Label lblCust)
        {
            if (txtcCust.Tag == null)
            {
                MessageBoxEx.Show(string.Format("请选择{0}！", lblCust.Text.TrimEnd('：')));
                txtcCust.Focus();
                return;
            }
            //frmBalanceDocuments frmDocuments = new frmBalanceDocuments(DataSources.EnumOrderType.PAYMENT, txtcCust.Tag.ToString(), ((int)DataSources.EnumPaymentType.PAYMENT).ToString());
            frmPayment frmDocuments = new frmPayment(txtcCust.Tag.ToString());
            if (frmDocuments.ShowDialog() == DialogResult.OK)
            {
                BindDocuments2(frmDocuments.listRows);
            }
        }

        void BindDocuments(List<DataGridViewRow> listRows)
        {
            //dgvDocuments.Rows.Clear();
            foreach (DataGridViewRow dgvrd in listRows)
            {
                DataGridViewRow dgvr = dgvDocuments.Rows[dgvDocuments.Rows.Add()];
                dgvr.Cells[colOrderName.Name].Value = dgvrd.Cells["colBillsName"].Value;//单据名称
                dgvr.Cells[colOrderID.Name].Value = dgvrd.Cells["colID"].Value;//单据ID
                dgvr.Cells[colOrderNum.Name].Value = dgvrd.Cells["colBillsCode"].Value;//单据编号
                dgvr.Cells[colOrderDate.Name].Value = dgvrd.Cells["colOrderDate"].Value;//单据日期
                dgvr.Cells[colMoney.Name].Value = dgvrd.Cells["colTotalMoney"].Value;//开单金额
                dgvr.Cells[colSettledMoney.Name].Value = dgvrd.Cells["colBalanceMoney"].Value;//已结算金额
                dgvr.Cells[colWaitMoney.Name].Value = dgvrd.Cells["colWaitMoney"].Value;//未结算金额
            }
        }

        void BindDocuments2(List<DataGridViewRow> listRows)
        {
            //dgvDocuments2.Rows.Clear();
            foreach (DataGridViewRow dgvrd in listRows)
            {
                DataGridViewRow dgvr = dgvDocuments2.Rows[dgvDocuments2.Rows.Add()];
                dgvr.Cells[colOrderName2.Name].Value = dgvrd.Cells["colBillsName"].Value;//单据名称
                dgvr.Cells[colOrderID2.Name].Value = dgvrd.Cells["colID"].Value;//单据ID
                dgvr.Cells[colOrderNum2.Name].Value = dgvrd.Cells["colBillsCode"].Value;//单据编号
                dgvr.Cells[colOrderDate2.Name].Value = dgvrd.Cells["colOrderDate"].Value;//单据日期
                dgvr.Cells[colMoney2.Name].Value = dgvrd.Cells["colTotalMoney"].Value;//开单金额
                dgvr.Cells[colSettledMoney2.Name].Value = dgvrd.Cells["colBalanceMoney"].Value;//已结算金额
                dgvr.Cells[colWaitMoney2.Name].Value = dgvrd.Cells["colWaitMoney"].Value;//未结算金额
            }
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 验证核销单
        /// </summary>
        /// <returns></returns>
        bool CheckAccountVerification()
        {
            string orderType = CommonCtrl.IsNullToString(cboOrderType.SelectedValue);
            if (orderType.Length == 0)
            {
                epError.SetError(cboOrderType, "请选择单据类型！");
                cboOrderType.Focus();
                return false;
            }
            if (txtcCustName1.Tag == null)
            {
                epError.SetError(txtcCustName1, string.Format("请选择{0}！", lblCustName1.Text.Replace("：", "")));
                txtcCustName1.Focus();
                return false;
            }
            if (txtcCustName2.Tag == null)
            {
                epError.SetError(txtcCustName2, string.Format("请选择{0}！", lblCustName2.Text.Replace("：", "")));
                txtcCustName2.Focus();
                return false;
            }
            string orgId = CommonCtrl.IsNullToString(cboOrgId.SelectedValue);
            if (orgId.Length == 0)
            {
                epError.SetError(cboOrgId, "请选择部门！");
                cboOrgId.Focus();
                return false;
            }
            string handle = CommonCtrl.IsNullToString(cboHandle.SelectedValue);
            if (handle.Length == 0)
            {
                epError.SetError(cboHandle, "请选择经办人！");
                cboHandle.Focus();
                return false;
            }
            if (txtRemark.Caption.Trim().Length > 300)
            {
                epError.SetError(txtRemark, "备注字符长度不能大于300！");
                txtRemark.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查明细是否输入本次核销
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="colName">本次核销列名</param>
        /// <param name="colWaitMoneyName">待核销列名</param>
        /// <param name="verification_money">冲销金额</param>
        /// <returns></returns>
        bool CheckDetail(DataGridViewEx dgv, string colName, string colWaitMoneyName, ref decimal verification_money)
        {
            if (!dgv.Visible)
            {
                return true;
            }
            if (dgv.Rows.Count == 0)
            {
                MessageBoxEx.Show("请导入往来单据！");
                dgv.Focus();
                return false;
            }
            bool check = true;
            foreach (DataGridViewRow dgvr in dgv.Rows)
            {
                if (dgvr.IsNewRow)
                {
                    continue;
                }
                string waitMoney = CommonCtrl.IsNullToString(dgvr.Cells[colWaitMoneyName].Value);//待结算
                if (waitMoney.Length == 0)
                {
                    return true;
                }
                string nameNum = CommonCtrl.IsNullToString(dgvr.Cells[colName].Value);
                if (nameNum.Length == 0)
                {
                    MessageBoxEx.Show("请输入本次核销！");
                    dgv.CurrentCell = dgvr.Cells[colName];
                    check = false;
                    break;
                }
                if (decimal.Parse(waitMoney) < decimal.Parse(nameNum))
                {
                    MessageBoxEx.Show("本次核销不能大于未结算金额！");
                    dgv.CurrentCell = dgvr.Cells[colName];
                    check = false;
                    break;
                }
                //只合计第一个
                if (colName == colVerificationMoney.Name)
                {
                    verification_money += Convert.ToDecimal(dgvr.Cells[colName].Value);
                }
            }
            return check;
        }

        /// <summary>
        /// 检查应收应付本次核销合计是否一致
        /// </summary>
        /// <returns></returns>
        bool CheckTotal()
        {
            if (!dgvDocuments2.Visible)
            {
                return true;
            }
            decimal money1 = 0;
            decimal money2 = 0;
            foreach (DataGridViewRow dgvr in dgvDocuments.Rows)
            {
                money1 += Convert.ToDecimal(dgvr.Cells[colVerificationMoney.Name].Value);
            }
            foreach (DataGridViewRow dgvr in dgvDocuments2.Rows)
            {
                money2 += Convert.ToDecimal(dgvr.Cells[colVerificationMoney2.Name].Value);
            }
            if (money1 != money2)
            {
                MessageBoxEx.Show("应收应付本次核销合计不一致！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查余额
        /// </summary>
        /// <param name="auditStatus"></param>
        /// <returns></returns>
        bool CheckYuE(DataSources.EnumAuditStatus auditStatus)
        {
            if (auditStatus == DataSources.EnumAuditStatus.DRAFT || txtcCustName1.Tag == null || cboOrderType.SelectedValue == null)
            {
                return true;
            }
            DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)cboOrderType.SelectedValue;
            if (enumAccount == DataSources.EnumAccountVerification.YuShouToYingShou || enumAccount == DataSources.EnumAccountVerification.YuFuToYingFu
                || enumAccount == DataSources.EnumAccountVerification.YuShouToYuShou || enumAccount == DataSources.EnumAccountVerification.YuFuToYuFu)
            {
                decimal yuE = 0;//余额
                if (enumAccount == DataSources.EnumAccountVerification.YuShouToYingShou || enumAccount == DataSources.EnumAccountVerification.YuShouToYuShou)
                {
                    yuE = DBOperation.GetAdvance(txtcCustName1.Tag.ToString(), DataSources.EnumOrderType.RECEIVABLE);
                }
                else
                {
                    yuE = DBOperation.GetAdvance(txtcCustName1.Tag.ToString(), DataSources.EnumOrderType.PAYMENT);
                }
                decimal documentTotal = 0;//本次核销合计
                foreach (DataGridViewRow dgvr in dgvDocuments.Rows)
                {
                    string paid = CommonCtrl.IsNullToString(dgvr.Cells[colVerificationMoney.Name].Value);
                    if (paid.Length > 0)
                    {
                        documentTotal += Convert.ToDecimal(paid);
                    }
                }
                string strYuE = "预收余额";
                if (enumAccount == DataSources.EnumAccountVerification.YuFuToYingFu || enumAccount == DataSources.EnumAccountVerification.YuFuToYuFu)
                {
                    strYuE = "预付余额";
                }
                if (yuE - documentTotal < 0)
                {
                    MessageBoxEx.Show(string.Format("本次核销合计超过了【{0}】的{1}！", lblCustName1.Text.TrimEnd('：'), strYuE));
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 生成sql
        /// <summary>
        /// 往来核销sql
        /// </summary>
        /// <param name="list">sql组合列表</param>
        /// <param name="auditStatus">保存状态</param>
        /// <param name="verification_money">冲销金额</param>
        void AddAccountVerificationSqlString(List<SysSQLString> list, DataSources.EnumAuditStatus auditStatus, decimal verification_money)
        {
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.Param.Add("account_verification_id", id);
            if (auditStatus == DataSources.EnumAuditStatus.DRAFT)
            {
                sql.Param.Add("order_num", null);
            }
            else
            {
                if (txtOrderNum.Caption.Trim().Length > 0)
                {
                    sql.Param.Add("order_num", txtOrderNum.Caption.Trim());//单号
                }
                else
                {
                    sql.Param.Add("order_num", CommonUtility.GetNewNo(DataSources.EnumProjectType.AccountVerification));
                }
            }
            sql.Param.Add("order_date", Common.LocalDateTimeToUtcLong(dtpOrderDate.Value).ToString());//时间
            sql.Param.Add("order_status", ((int)auditStatus).ToString());//单据状态
            sql.Param.Add("order_type", ((int)cboOrderType.SelectedValue).ToString());//单据类型
            string advance = txtAdvanceBalance.Caption.Trim();
            sql.Param.Add("advance_balance", string.IsNullOrEmpty(advance) ? null : advance);//预收余额
            sql.Param.Add("cust_id1", txtcCustName1.Tag.ToString());
            sql.Param.Add("cust_code1", cust_code1);//往来单位
            sql.Param.Add("cust_name1", txtcCustName1.Text);//
            sql.Param.Add("cust_id2", txtcCustName2.Tag.ToString());
            sql.Param.Add("cust_code2", cust_code2);//往来单位
            sql.Param.Add("cust_name2", txtcCustName2.Text);//
            sql.Param.Add("com_id", GlobalStaticObj.CurrUserCom_Id);
            sql.Param.Add("com_name", GlobalStaticObj.CurrUserCom_Name);//公司
            sql.Param.Add("org_id", CommonCtrl.IsNullToString(cboOrgId.SelectedValue));//部门
            sql.Param.Add("org_name", cboOrgId.Text);
            sql.Param.Add("handle", CommonCtrl.IsNullToString(cboHandle.SelectedValue));//经办人
            sql.Param.Add("handle_name", cboHandle.Text);
            sql.Param.Add("operator", GlobalStaticObj.UserID);//操作人
            sql.Param.Add("operator_name", GlobalStaticObj.UserName);
            sql.Param.Add("remark", txtRemark.Caption);
            sql.Param.Add("verification_money", verification_money.ToString());
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                sql.Param.Add("create_by", GlobalStaticObj.UserID);
                sql.Param.Add("create_name", GlobalStaticObj.UserName);
                sql.Param.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
                sql.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                sql.sqlString = @"INSERT INTO [tb_account_verification]
           (account_verification_id,order_num ,order_date,order_status,order_type,advance_balance,cust_id1,cust_code1,cust_name1,cust_id2,cust_code2,cust_name2
,com_id,com_name,org_id,org_name,handle,handle_name,operator,operator_name,create_by,create_name,create_time,status,enable_flag,remark,verification_money)
     VALUES
           (@account_verification_id,@order_num,@order_date,@order_status,@order_type,@advance_balance,@cust_id1,@cust_code1,@cust_name1,@cust_id2,@cust_code2,@cust_name2
,@com_id,@com_name,@org_id,@org_name,@handle,@handle_name,@operator,@operator_name,@create_by,@create_name,@create_time,@status,@enable_flag,@remark,@verification_money);";
            }
            else if (windowStatus == WindowStatus.Edit)
            {
                sql.Param.Add("update_by", GlobalStaticObj.UserID);
                sql.Param.Add("update_name", GlobalStaticObj.UserName);
                sql.Param.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                sql.sqlString = @"UPDATE [tb_account_verification] SET order_num =@order_num
,order_date = @order_date,order_status = @order_status,order_type = @order_type,advance_balance=@advance_balance,cust_id1=@cust_id1,cust_code1 = @cust_code1,
cust_name1 = @cust_name1,cust_id2=@cust_id2,cust_code2 = @cust_code2,cust_name2 = @cust_name2
,com_id=@com_id,com_name=@com_name,org_id = @org_id,org_name=@org_name,handle = @handle,handle_name=@handle_name,operator = @operator,operator_name=@operator_name
,update_by = @update_by,update_name=@update_name,update_time = @update_time,remark=@remark,verification_money=@verification_money
 where account_verification_id = @account_verification_id;";
            }
            list.Add(sql);
        }
        //往来核销明细1sql
        void AddDocument1SqlString(List<SysSQLString> list)
        {
            foreach (DataGridViewRow dgvr in dgvDocuments.Rows)
            {
                if (dgvr.IsNewRow)
                {
                    continue;
                }
                SysSQLString sql = new SysSQLString();
                sql.cmdType = CommandType.Text;
                sql.Param = new Dictionary<string, string>();
                sql.Param.Add("account_verification_id", id);
                sql.Param.Add("order_name", CommonCtrl.IsNullToString(dgvr.Cells[colOrderName.Name].Value));//业务单据名称
                string money = CommonCtrl.IsNullToString(dgvr.Cells[colMoney.Name].Value);
                sql.Param.Add("money", string.IsNullOrEmpty(money) ? null : money);//金额
                sql.Param.Add("order_num", CommonCtrl.IsNullToString(dgvr.Cells[colOrderNum.Name].Value));//业务单据编号
                sql.Param.Add("order_id", CommonCtrl.IsNullToString(dgvr.Cells[colOrderID.Name].Value));//业务单据ID
                sql.Param.Add("order_index", "1");
                string orderDate = CommonCtrl.IsNullToString(dgvr.Cells[colOrderDate.Name].Value);
                if (orderDate.Length == 0)
                {
                    sql.Param.Add("order_date", null);//业务单据日期
                }
                else
                {
                    sql.Param.Add("order_date", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(orderDate)).ToString());
                }
                string settledMoney = CommonCtrl.IsNullToString(dgvr.Cells[colSettledMoney.Name].Value);
                sql.Param.Add("settled_money", string.IsNullOrEmpty(settledMoney) ? null : settledMoney);//已结算金额
                string waitMoney = CommonCtrl.IsNullToString(dgvr.Cells[colWaitMoney.Name].Value);//
                sql.Param.Add("wait_settled_money", string.IsNullOrEmpty(waitMoney) ? null : waitMoney);//未结算金额
                sql.Param.Add("verification_money", CommonCtrl.IsNullToString(dgvr.Cells[colVerificationMoney.Name].Value));//本次核销
                bool isVerification = Convert.ToBoolean(dgvr.Cells[colIsVerification.Name].Value);
                sql.Param.Add("is_verification", isVerification ? "1" : "0");//是否核销
                sql.Param.Add("remark", CommonCtrl.IsNullToString(dgvr.Cells[colRemark.Name].Value));//备注
                sql.Param.Add("create_by", GlobalStaticObj.UserID);
                sql.Param.Add("create_time", DateTime.UtcNow.Ticks.ToString());
                sql.sqlString = @"INSERT INTO [tb_verificationn_documents](verificationn_documents_id,account_verification_id,order_name,money,order_id
,order_num,order_date,order_index,settled_money,wait_settled_money,verification_money,is_verification,remark,create_by,create_time)
     VALUES
(@verificationn_documents_id,@account_verification_id,@order_name,@money,@order_id,@order_num,@order_date,@order_index,@settled_money,@wait_settled_money,@verification_money,@is_verification,@remark
,@create_by,@create_time)";
                sql.Param.Add("verificationn_documents_id", Guid.NewGuid().ToString());
                list.Add(sql);
            }
        }
        //往来核销明细2sql
        void AddDocument2SqlString(List<SysSQLString> list)
        {
            foreach (DataGridViewRow dgvr in dgvDocuments2.Rows)
            {
                if (dgvr.IsNewRow)
                {
                    continue;
                }
                SysSQLString sql = new SysSQLString();
                sql.cmdType = CommandType.Text;
                sql.Param = new Dictionary<string, string>();
                sql.Param.Add("account_verification_id", id);
                sql.Param.Add("order_name", CommonCtrl.IsNullToString(dgvr.Cells[colOrderName2.Name].Value));//业务单据名称
                string money = CommonCtrl.IsNullToString(dgvr.Cells[colMoney2.Name].Value);
                sql.Param.Add("money", string.IsNullOrEmpty(money) ? null : money);//金额
                sql.Param.Add("order_num", CommonCtrl.IsNullToString(dgvr.Cells[colOrderNum2.Name].Value));//业务单据编号
                sql.Param.Add("order_id", CommonCtrl.IsNullToString(dgvr.Cells[colOrderID2.Name].Value));//业务单据ID
                sql.Param.Add("order_index", "2");
                string orderDate = CommonCtrl.IsNullToString(dgvr.Cells[colOrderDate2.Name].Value);
                if (orderDate.Length == 0)
                {
                    sql.Param.Add("order_date", null);//业务单据日期
                }
                else
                {
                    sql.Param.Add("order_date", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(orderDate)).ToString());
                }
                string settledMoney = CommonCtrl.IsNullToString(dgvr.Cells[colSettledMoney2.Name].Value);
                sql.Param.Add("settled_money", string.IsNullOrEmpty(settledMoney) ? null : settledMoney);//已结算金额
                string waitMoney = CommonCtrl.IsNullToString(dgvr.Cells[colWaitMoney2.Name].Value);//
                sql.Param.Add("wait_settled_money", string.IsNullOrEmpty(waitMoney) ? null : waitMoney);//未结算金额
                sql.Param.Add("verification_money", CommonCtrl.IsNullToString(dgvr.Cells[colVerificationMoney2.Name].Value));//本次核销
                bool isVerification = Convert.ToBoolean(dgvr.Cells[colIsVerification2.Name].Value);
                sql.Param.Add("is_verification", isVerification ? "1" : "0");//是否核销
                sql.Param.Add("remark", CommonCtrl.IsNullToString(dgvr.Cells[colRemark2.Name].Value));//备注
                sql.Param.Add("create_by", GlobalStaticObj.UserID);
                sql.Param.Add("create_time", DateTime.UtcNow.Ticks.ToString());
                sql.sqlString = @"INSERT INTO [tb_verificationn_documents](verificationn_documents_id,account_verification_id,order_name,money,order_id
,order_num,order_date,order_index,settled_money,wait_settled_money,verification_money,is_verification,remark,create_by,create_time)
     VALUES
(@verificationn_documents_id,@account_verification_id,@order_name,@money,@order_id,@order_num,@order_date,@order_index,@settled_money,@wait_settled_money,@verification_money,@is_verification,@remark
,@create_by,@create_time)";
                sql.Param.Add("verificationn_documents_id", Guid.NewGuid().ToString());
                list.Add(sql);
            }
        }
        /// <summary>
        /// 设置单据导入状态
        /// </summary>
        void SetDocumentImportStatus(string statusName, DataSources.EnumImportStaus importStaus, List<SysSQLString> listSql, bool isSelected)
        {
            if (dgvDocuments.RowCount == 0 && dgvDocuments2.RowCount == 0)
            {
                return;
            }
            Preposition pre = new Preposition();
            foreach (DataGridViewRow dgvr in dgvDocuments.Rows)
            {
                if (isSelected)
                {
                    object check = dgvr.Cells[colChk.Name].EditedFormattedValue;
                    if (check != null && (bool)check)
                    {
                        pre.AddID(dgvr.Cells[colOrderID.Name].Value, dgvr.Cells[colOrderName.Name].Value);
                    }
                }
                else
                {
                    pre.AddID(dgvr.Cells[colOrderID.Name].Value, dgvr.Cells[colOrderName.Name].Value);
                }
            }
            if (dgvDocuments2.Visible)
            {
                foreach (DataGridViewRow dgvr in dgvDocuments2.Rows)
                {
                    if (isSelected)
                    {
                        object check = dgvr.Cells[colChk2.Name].EditedFormattedValue;
                        if (check != null && (bool)check)
                        {
                            pre.AddID(dgvr.Cells[colOrderID2.Name].Value, dgvr.Cells[colOrderName2.Name].Value);
                        }
                    }
                    else
                    {
                        pre.AddID(dgvr.Cells[colOrderID2.Name].Value, dgvr.Cells[colOrderName2.Name].Value);
                    }
                }
            }
            listSql.AddRange(pre.GetSql(statusName, importStaus));
        }

        #endregion

        #region 方法
        /// <summary>
        /// 解除占用前置单据
        /// </summary>
        void UnOccupyDocument(bool isSelected)
        {
            List<SysSQLString> listSql = new List<SysSQLString>();
            SetDocumentImportStatus("is_occupy_finance", DataSources.EnumImportStaus.OPEN, listSql, isSelected);
            if (listSql.Count > 0)
            {
                if (!DBHelper.BatchExeSQLStringMultiByTrans("解除往来核销前置单据占用", listSql))
                {
                    return;
                }
            }
        }
        //保存
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
            dgvDocuments.EndEdit();
            dgvDocuments2.EndEdit();
            decimal verification_money = 0;//冲销金额
            if (!CheckAccountVerification() || !CheckDetail(dgvDocuments, colVerificationMoney.Name, colWaitMoney.Name, ref verification_money)
                || !CheckDetail(dgvDocuments2, colVerificationMoney2.Name, colWaitMoney2.Name, ref verification_money) || !CheckTotal() || !CheckYuE(auditStatus))
            {
                return;
            }
            if (MessageBoxEx.Show("确认要" + saveName + "吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }
            List<SysSQLString> list = new List<SysSQLString>();
            //如果是新增，则重新生成一个ID
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                id = Guid.NewGuid().ToString();
            }
            else
            {
                SysSQLString deleteSql = new SysSQLString();
                deleteSql.cmdType = CommandType.Text;
                deleteSql.Param = new Dictionary<string, string>();
                deleteSql.sqlString = string.Format("delete tb_verificationn_documents where account_verification_id='{0}'", id);
                list.Add(deleteSql);
            }
            string opName = "往来核销操作";
            if (windowStatus == WindowStatus.Edit)
            {
                opName = "修改往来核销";
            }
            else if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                opName = "新增往来核销";
            }
            AddAccountVerificationSqlString(list, auditStatus, verification_money);
            AddDocument1SqlString(list);
            AddDocument2SqlString(list);
            SetDocumentImportStatus("is_occupy_finance", DataSources.EnumImportStaus.OPEN, list, false);
            if (auditStatus == DataSources.EnumAuditStatus.SUBMIT)
            {
                SetDocumentImportStatus("is_lock", DataSources.EnumImportStaus.LOCK, list, false);
            }
            if (auditStatus == DataSources.EnumAuditStatus.SUBMIT)
            {
                DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)cboOrderType.SelectedValue;
                Financial.DocumentSettlementByVerification(enumAccount, id, list);
            }
            if (DBHelper.BatchExeSQLStringMultiByTrans(opName, list))
            {
                MessageBoxEx.Show(saveName + "成功！");
                if (uc != null)
                {
                    uc.BindData(id);
                }
                isAutoClose = true;
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.Show(saveName + "失败！");
            }
        }
        //审核
        void VerifyData()
        {
            UCVerify frmVerify = new UCVerify();
            if (frmVerify.ShowDialog() == DialogResult.OK)
            {
                SysSQLString sql = new SysSQLString();
                sql.cmdType = CommandType.Text;
                sql.sqlString = string.Format("update tb_account_verification set order_status='{0}',Verify_advice='{3}' where account_verification_id='{1}' and order_status='{2}';",
                    ((int)frmVerify.auditStatus), id, (int)DataSources.EnumAuditStatus.SUBMIT, frmVerify.Content);
                sql.Param = new Dictionary<string, string>();
                List<SysSQLString> listSql = new List<SysSQLString>();
                listSql.Add(sql);
                //如果是审核不通过，则将没有提交或审核状态的单据设为正常
                if (frmVerify.auditStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                {
                    #region 将没有提交或审核状态的单据设为正常
                    SysSQLString receivableSql = new SysSQLString();
                    receivableSql.cmdType = CommandType.Text;
                    receivableSql.sqlString = string.Format(@"update tb_parts_sale_billing set is_lock=@is_lock where where sale_billing_id in (select order_id from tb_verificationn_documents where account_verification_id='{0}') and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =sale_billing_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=sale_billing_id
);
update tb_maintain_settlement_info set is_lock=@is_lock where where settlement_id in (select order_id from tb_verificationn_documents where account_verification_id='{0}') and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =settlement_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=settlement_id
);
update tb_maintain_three_guaranty_settlement set is_lock=@is_lock where where st_id in (select order_id from tb_verificationn_documents where account_verification_id='{0}') and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =st_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=st_id
);
update tb_parts_purchase_billing set is_lock=@is_lock where where purchase_billing_id in (select order_id from tb_verificationn_documents where account_verification_id='{0}') and  exists (
select a.documents_id from tb_balance_documents a inner join tb_bill_receivable b on a.order_id=b.payable_single_id
where order_status in ('1','2') and a.documents_id =purchase_billing_id
union all
select b.order_id from tb_account_verification a inner join tb_verificationn_documents b on a.account_verification_id=b.account_verification_id
where a.order_status in ('1','2') and b.order_id=purchase_billing_id
);", id);
                    receivableSql.Param = new Dictionary<string, string>();
                    receivableSql.Param.Add("is_lock", ((int)DataSources.EnumImportStaus.OPEN).ToString());
                    //receivableSql.Param.Add("@id", id);
                    listSql.Add(receivableSql);
                    #endregion
                    //审核失败需重新计算已结算金额
                    DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(cboOrderType.SelectedValue);
                    Financial.DocumentSettlementByVerification(enumAccount, id, listSql);
                }
                if (DBHelper.BatchExeSQLStringMultiByTrans("审核往来核销", listSql))
                {
                    MessageBoxEx.Show("审核成功！");
                    if (uc != null)
                    {
                        uc.BindData(id);
                        isAutoClose = true;
                        deleteMenuByTag(this.Tag.ToString(), uc.Name);
                    }
                }
                else
                {
                    MessageBoxEx.Show("审核失败！");
                }
            }
        }
        //激活/作废
        void InvalidOrActivation()
        {
            if (string.IsNullOrEmpty(id))
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
            sql.Param.Add("account_verification_id", id);
            if (enumActivation == DataSources.EnumInvalidOrActivation.Activation)
            {
                sql.sqlString = "update tb_account_verification set order_status=old_order_status where account_verification_id=@account_verification_id";
            }
            else
            {
                sql.sqlString = "update tb_account_verification set old_order_status=order_status,order_status=@order_status where account_verification_id=@account_verification_id";
                sql.Param.Add("order_status", ((int)DataSources.EnumAuditStatus.Invalid).ToString());
            }
            listSql.Add(sql);
            if (DBHelper.BatchExeSQLStringMultiByTrans(btnActivation.Caption + "往来核销", listSql))
            {
                MessageBoxEx.ShowInformation(btnActivation.Caption + "成功！");
                uc.BindData(id);
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.ShowError(btnActivation.Caption + "失败！");
            }
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
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            UCAccountVerificationAdd add = new UCAccountVerificationAdd(status, id, uc);
            base.addUserControl(add, string.Format("往来核销-{0}", title), menuId + id, this.Tag.ToString(), this.Name);
        }
        //删除
        void DeleteData()
        {
            if (MessageBoxEx.Show("是否要删除当前数据？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("enable_flag", ((int)DataSources.EnumEnableFlag.DELETED).ToString());
            if (DBHelper.Submit_AddOrEdit("删除往来核销", "tb_account_verification", "account_verification_id", id, dic))
            {
                MessageBoxEx.Show("删除成功！");
                if (uc != null)
                {
                    uc.BindData(null);
                }
                isAutoClose = true;
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.Show("删除失败！");
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData()
        {
            DataTable dt = DBHelper.GetTable("", "tb_account_verification", "*", string.Format("account_verification_id='{0}'", id), "", "");
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
            txtcCustName1.Tag = dr["cust_id1"];
            cust_code1 = CommonCtrl.IsNullToString(dr["cust_code1"]);
            txtcCustName1.Text = CommonCtrl.IsNullToString(dr["cust_name1"]);
            txtcCustName2.Tag = dr["cust_id2"];
            cust_code2 = CommonCtrl.IsNullToString(dr["cust_code2"]);
            txtcCustName2.Text = CommonCtrl.IsNullToString(dr["cust_name2"]);
            txtAdvanceBalance.Caption = CommonCtrl.IsNullToString(dr["advance_balance"]);
            txtRemark.Caption = CommonCtrl.IsNullToString(dr["remark"]);
            cboOrderType.SelectedValue = Convert.ToInt32(dr["order_type"]);
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
                lblCreateBy.Text = CommonCtrl.IsNullToString(dr["create_name"]);
                string createTime = CommonCtrl.IsNullToString(dr["create_time"]);
                if (createTime.Length > 0)
                {
                    lblCreateTime.Text = Common.UtcLongToLocalDateTime(Int64.Parse(createTime)).ToString();
                }
                lblUpdateBy.Text = CommonCtrl.IsNullToString(dr["update_name"]);
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
            //复制或编辑，计算往来余额
            if (windowStatus == WindowStatus.Copy || windowStatus == WindowStatus.Edit)
            {
                string custID = CommonCtrl.IsNullToString(dr["cust_id1"]);//往来单位
                DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(dr["order_type"]);
                switch (enumAccount)
                {
                    case DataSources.EnumAccountVerification.YuShouToYingShou://预收冲应收
                    case DataSources.EnumAccountVerification.YuShouToYuShou://预收转预收
                        if (txtAdvanceBalance.Visible)
                        {
                            txtAdvanceBalance.Caption = DBOperation.GetAdvance(custID, DataSources.EnumOrderType.RECEIVABLE).ToString();
                        }
                        break;
                    case DataSources.EnumAccountVerification.YuFuToYingFu://预付冲应付
                    case DataSources.EnumAccountVerification.YuFuToYuFu://预付转预付
                        if (txtAdvanceBalance.Visible)
                        {
                            txtAdvanceBalance.Caption = DBOperation.GetAdvance(custID, DataSources.EnumOrderType.PAYMENT).ToString();
                        }
                        break;
                }
            }
            string auditStatus = CommonCtrl.IsNullToString(dr["order_status"]);
            DataSources.EnumAuditStatus enumAuditStatus = DataSources.EnumAuditStatus.DRAFT;
            if (auditStatus != "" && windowStatus != WindowStatus.Copy)
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
            //不复制明细
            if (windowStatus != WindowStatus.Copy)
            {
                BindDetail();
            }
        }
        //绑定明细
        void BindDetail()
        {
            if (cboOrderType.SelectedValue == null)
            {
                return;
            }
            DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(cboOrderType.SelectedValue);
            DataTable dt = DBHelper.GetTable("", "tb_verificationn_documents", "*", string.Format("account_verification_id='{0}'", id), "", "");
            if (enumAccount == DataSources.EnumAccountVerification.YingShouToYingFu)
            {
                List<DataRow> listYS = dt.Select("order_index='1'").ToList();
                DataRow[] listYF = dt.Select("order_index='2'");
                BindDocuments(listYS);
                BindDocuments2(listYF);
            }
            else if (enumAccount == DataSources.EnumAccountVerification.YingFuToYingShou)
            {
                DataRow[] listYS = dt.Select("order_index='2'");
                List<DataRow> listYF = dt.Select("order_index='1'").ToList();
                BindDocuments(listYF);
                BindDocuments2(listYS);
            }
            else
            {
                List<DataRow> list = dt.Rows.Cast<DataRow>().ToList();
                BindDocuments(list);
            }
        }
        /// <summary>
        /// 绑定第一个
        /// </summary>
        /// <param name="drs"></param>
        void BindDocuments(List<DataRow> drs)
        {
            dgvDocuments.Rows.Clear();
            foreach (DataRow dr in drs)
            {
                DataGridViewRow dgvr = dgvDocuments.Rows[dgvDocuments.Rows.Add()];
                dgvr.Cells[colOrderName.Name].Value = dr["order_name"];
                dgvr.Cells[colOrderNum.Name].Value = dr["order_num"];
                dgvr.Cells[colOrderID.Name].Value = dr["order_id"];
                dgvr.Cells[colOrderDate.Name].Value = Common.UtcLongToLocalDateTime(dr["order_date"], "yyyy-MM-dd");
                dgvr.Cells[colMoney.Name].Value = dr["money"];
                dgvr.Cells[colSettledMoney.Name].Value = dr["settled_money"];
                decimal money = 0;//总金额
                if (dr["money"] != null && dr["money"] != DBNull.Value)
                {
                    money = Convert.ToDecimal(dr["money"]);
                }
                decimal settledMoney = 0;//已结算金额
                if (dr["settled_money"] != null && dr["settled_money"] != DBNull.Value)
                {
                    settledMoney = Convert.ToDecimal(dr["settled_money"]);
                }
                dgvr.Cells[colWaitMoney.Name].Value = money - settledMoney;
                dgvr.Cells[colVerificationMoney.Name].Value = dr["verification_money"];
                dgvr.Cells[colIsVerification.Name].Value = CommonCtrl.IsNullToString(dr["is_verification"]) == "1";
                dgvr.Cells[colRemark.Name].Value = dr["remark"];
            }
        }
        /// <summary>
        /// 绑定第二个
        /// </summary>
        /// <param name="drs"></param>
        void BindDocuments2(DataRow[] drs)
        {
            dgvDocuments2.Rows.Clear();
            foreach (DataRow dr in drs)
            {
                DataGridViewRow dgvr = dgvDocuments2.Rows[dgvDocuments2.Rows.Add()];
                dgvr.Cells[colOrderName2.Name].Value = dr["order_name"];
                dgvr.Cells[colOrderNum2.Name].Value = dr["order_num"];
                dgvr.Cells[colOrderID2.Name].Value = dr["order_id"];
                dgvr.Cells[colOrderDate2.Name].Value = Common.UtcLongToLocalDateTime(dr["order_date"], "yyyy-MM-dd");
                dgvr.Cells[colMoney2.Name].Value = dr["money"];
                dgvr.Cells[colSettledMoney2.Name].Value = dr["settled_money"];
                decimal money = 0;//总金额
                if (dr["money"] != null && dr["money"] != DBNull.Value)
                {
                    money = Convert.ToDecimal(dr["money"]);
                }
                decimal settledMoney = 0;//已结算金额
                if (dr["settled_money"] != null && dr["settled_money"] != DBNull.Value)
                {
                    settledMoney = Convert.ToDecimal(dr["settled_money"]);
                }
                dgvr.Cells[colWaitMoney2.Name].Value = money - settledMoney;
                dgvr.Cells[colVerificationMoney2.Name].Value = dr["verification_money"];
                dgvr.Cells[colIsVerification2.Name].Value = CommonCtrl.IsNullToString(dr["is_verification"]) == "1";
                dgvr.Cells[colRemark2.Name].Value = dr["remark"];
            }
        }
        /// <summary>
        /// 设置显示
        /// </summary>
        void SetLable()
        {
            if (cboOrderType.SelectedValue == null)
            {
                return;
            }
            #region 清空项
            if (!isBind)
            {
                dgvDocuments.Rows.Clear();
                dgvDocuments2.Rows.Clear();
                txtcCustName1.Tag = null;
                txtcCustName1.Text = string.Empty;
                txtcCustName2.Tag = null;
                txtcCustName2.Text = string.Empty;
                txtAdvanceBalance.Caption = string.Empty;
            }
            #endregion
            DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(cboOrderType.SelectedValue);
            #region 各个状态的显示
            switch (enumAccount)
            {
                case DataSources.EnumAccountVerification.YuShouToYingShou:
                    lblCustName1.Text = "预收客户：";
                    lblCustName2.Text = "应收客户：";
                    lblAdvance.Text = "当前预收余额：";
                    lblAdvance.Visible = true;
                    txtAdvanceBalance.Visible = true;
                    break;
                case DataSources.EnumAccountVerification.YuFuToYingFu:
                    lblCustName1.Text = "预付供应商：";
                    lblCustName2.Text = "应付供应商：";
                    lblAdvance.Text = "当前预付余额：";
                    lblAdvance.Visible = true;
                    txtAdvanceBalance.Visible = true;
                    break;
                case DataSources.EnumAccountVerification.YingFuToYingFu:
                    lblCustName1.Text = "转出供应商：";
                    lblCustName2.Text = "转入供应商：";
                    lblAdvance.Visible = false;
                    txtAdvanceBalance.Visible = false;
                    break;
                case DataSources.EnumAccountVerification.YingShouToYingShou:
                    lblCustName1.Text = "转出客户：";
                    lblCustName2.Text = "转入客户：";
                    lblAdvance.Visible = false;
                    txtAdvanceBalance.Visible = false;
                    break;
                case DataSources.EnumAccountVerification.YingShouToYingFu:
                    lblCustName1.Text = "客户：";
                    lblCustName2.Text = "供应商：";
                    lblAdvance.Visible = false;
                    txtAdvanceBalance.Visible = false;
                    break;
                case DataSources.EnumAccountVerification.YingFuToYingShou:
                    lblCustName1.Text = "供应商：";
                    lblCustName2.Text = "客户：";
                    lblAdvance.Visible = false;
                    txtAdvanceBalance.Visible = false;
                    break;
                case DataSources.EnumAccountVerification.YuShouToYuShou:
                    lblCustName1.Text = "转出客户：";
                    lblCustName2.Text = "转入客户：";
                    lblAdvance.Text = "当前预收余额";
                    lblAdvance.Visible = true;
                    txtAdvanceBalance.Visible = true;
                    break;
                case DataSources.EnumAccountVerification.YuFuToYuFu:
                    lblCustName1.Text = "转出供应商：";
                    lblCustName2.Text = "转入供应商：";
                    lblAdvance.Text = "当前预付余额";
                    lblAdvance.Visible = true;
                    txtAdvanceBalance.Visible = true;
                    break;
            }
            MainResize(enumAccount);
            if (enumAccount == DataSources.EnumAccountVerification.YuShouToYuShou || enumAccount == DataSources.EnumAccountVerification.YuFuToYuFu)
            {
                dgvDocuments.AllowUserToAddRows = true;
            }
            else
            {
                dgvDocuments.AllowUserToAddRows = false;
            }
            #endregion
        }
        /// <summary>
        /// 设值主体内DataGridView大小
        /// </summary>
        /// <param name="enumAccount"></param>
        void MainResize(DataSources.EnumAccountVerification enumAccount)
        {
            if (enumAccount == DataSources.EnumAccountVerification.YingShouToYingFu || enumAccount == DataSources.EnumAccountVerification.YingFuToYingShou)
            {
                int pnlHeight = pnlMan.Height / 2 - 3;
                pnlDocument1.Height = pnlHeight;
                pnlDocument2.Height = pnlHeight;
                pnlDocument2.Top = pnlHeight + 6;
                pnlDocument2.Visible = true;
                dgvDocuments2.Width = dgvDocuments.Width;
            }
            else
            {
                pnlDocument2.Visible = false;
                pnlDocument1.Height = pnlMan.Height - pnlDocument1.Top;
            }
        }

        /// <summary>
        /// 绑定单据类型
        /// </summary>
        void BindOrderType()
        {
            cboOrderType.ValueMember = "Value";
            cboOrderType.DisplayMember = "Text";
            List<ListItem> list = DataSources.EnumToList(typeof(DataSources.EnumAccountVerification), true);
            list.RemoveAt(0);
            cboOrderType.DataSource = list;
        }
        #endregion

        #region 事件
        //选择部门，加载经办人
        private void cboOrgId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOrgId.SelectedValue == null)
            {
                return;
            }
            epError.Clear();
            CommonFuncCall.BindHandle(cboHandle, cboOrgId.SelectedValue.ToString(), "请选择");
        }
        //选择经办人
        private void cboHandle_SelectedIndexChanged(object sender, EventArgs e)
        {
            epError.Clear();
        }
        //往来单位1选择器
        private void txtcCustName1_ChooserClick(object sender, EventArgs e)
        {
            if (cboOrderType.SelectedValue == null)
            {
                return;
            }
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
                        cust_code1 = frmCustomer.strCustomerNo;
                        txtcCustName1.Text = frmCustomer.strCustomerName;
                        epError.Clear();
                        if (txtcCustName2.Tag == null && enumAccount != DataSources.EnumAccountVerification.YingShouToYingFu)//没有选择应收客户，默认选择与预收客户相同
                        {
                            txtcCustName2.Tag = frmCustomer.strCustomerId;
                            cust_code2 = frmCustomer.strCustomerNo;
                            txtcCustName2.Text = frmCustomer.strCustomerName;
                        }
                        if (txtAdvanceBalance.Visible)
                        {
                            txtAdvanceBalance.Caption = DBOperation.GetAdvance(frmCustomer.strCustomerId, DataSources.EnumOrderType.RECEIVABLE).ToString();
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
                        cust_code1 = frmSupp.supperCode;
                        txtcCustName1.Text = frmSupp.supperName;
                        epError.Clear();
                        if (txtcCustName2.Tag == null && enumAccount != DataSources.EnumAccountVerification.YingFuToYingShou)
                        {
                            txtcCustName2.Tag = frmSupp.supperID;
                            cust_code2 = frmSupp.supperCode;
                            txtcCustName2.Text = frmSupp.supperName;
                        }
                        if (txtAdvanceBalance.Visible)
                        {
                            txtAdvanceBalance.Caption = DBOperation.GetAdvance(frmSupp.supperID, DataSources.EnumOrderType.PAYMENT).ToString();
                        }
                    }
                    break;
            }
        }
        //往来单位2选择器
        private void txtcCustName2_ChooserClick(object sender, EventArgs e)
        {
            if (cboOrderType.SelectedValue == null)
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
                        cust_code2 = frmCustomer.strCustomerNo;
                        txtcCustName2.Text = frmCustomer.strCustomerName;
                        epError.Clear();
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
                        cust_code2 = frmSupp.supperCode;
                        txtcCustName2.Text = frmSupp.supperName;
                        epError.Clear();
                    }
                    break;
            }
        }
        //选择单据类型
        private void cboOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            epError.Clear();
            UnOccupyDocument(false);
            SetLable();
        }
        //字动调整明细大小
        private void pnlMan_Resize(object sender, EventArgs e)
        {
            if (cboOrderType.SelectedValue == null)
            {
                return;
            }
            DataSources.EnumAccountVerification enumAccount = (DataSources.EnumAccountVerification)Convert.ToInt32(cboOrderType.SelectedValue);
            MainResize(enumAccount);
        }

        //点击核销
        private void dgvDocuments2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDocuments2.ReadOnly)
            {
                return;
            }
            if (e.ColumnIndex == colIsVerification2.Index)//点击核销
            {

                bool gathering = Convert.ToBoolean(dgvDocuments2.Rows[e.RowIndex].Cells[colIsVerification2.Name].EditedFormattedValue);
                if (gathering)
                {
                    object waitMoney = dgvDocuments2.Rows[e.RowIndex].Cells[colWaitMoney2.Name].Value;
                    dgvDocuments2.Rows[e.RowIndex].Cells[colVerificationMoney2.Name].Value = waitMoney;//本次核销等于为结算金额
                }
                else
                {
                    dgvDocuments2.Rows[e.RowIndex].Cells[colVerificationMoney2.Name].Value = null;
                }
            }
        }
        //点击核销
        private void dgvDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDocuments.ReadOnly)
            {
                return;
            }
            if (e.ColumnIndex == colIsVerification.Index)//点击核销
            {

                bool gathering = Convert.ToBoolean(dgvDocuments.Rows[e.RowIndex].Cells[colIsVerification.Name].EditedFormattedValue);
                if (gathering)
                {
                    object waitMoney = dgvDocuments.Rows[e.RowIndex].Cells[colWaitMoney.Name].Value;
                    dgvDocuments.Rows[e.RowIndex].Cells[colVerificationMoney.Name].Value = waitMoney;//本次核销等于为结算金额
                }
                else
                {
                    dgvDocuments.Rows[e.RowIndex].Cells[colVerificationMoney.Name].Value = null;
                }
            }
        }
        //删除明细
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            DataGridViewEx dgv = (DataGridViewEx)cmsMenu.SourceControl;
            dgv.EndEdit();
            if (dgv.Name == dgvDocuments.Name)
            {
                DeleteDataGridViewRow(dgv, colChk.Name);
            }
            else
            {
                DeleteDataGridViewRow(dgv, colChk2.Name);
            }
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="checkName"></param>
        private void DeleteDataGridViewRow(DataGridView dgv, string checkName)
        {
            UnOccupyDocument(true);
            for (int i = 0; i < dgv.RowCount; i++)
            {
                DataGridViewRow dgvr = dgv.Rows[i];
                object isCheck = dgvr.Cells[checkName].Value;
                //将选中的删除
                if (isCheck != null && (bool)isCheck)
                {
                    dgv.Rows.RemoveAt(i--);
                }
            }
        }
        #endregion

    }
}
