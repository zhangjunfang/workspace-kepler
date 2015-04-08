
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using Model;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     ThreeGuarantyManager         
    /// Author:       Kord
    /// Date:         2014/10/29 10:59:16
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	维修服务-宇通三包-三包服务单
    ///***************************************************************************//
    public partial class UCMaintainThreeGuarantyManager : UCBase
    {
        #region Constructor -- 构造函数
        public UCMaintainThreeGuarantyManager()
        {
            InitializeComponent();

            UIAssistants.SetButtonStyle4QueryAndClear(btn_query, btn_clear, true);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(dgv_table, drtxt_remarks);   //设置数据表格样式,并将最后一列填充其余空白

            Init();

            Load += (sender, args) => UIAssistants.SetDataGridViewCheckColumn(dgv_table, drchk_check);
            SetContentMenuScrip(dgv_table);
            dgv_table.MultiSelect = false;
        }
        #endregion

        #region Field -- 字段
        public const String TableName = "tb_maintain_three_guaranty";
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        private void Init() //初始化
        {
            SetFuncationButtonVisible();
            InitQueryControlDataSource();
            FuncationRegiste();
        }
        private void SetFuncationButtonVisible() //设置功能按钮可见性
        {
            var btnCols = new ObservableCollection<ButtonEx_sms>
            {
                btnAdd, btnCopy, btnEdit, btnDelete, btnActivation, btnVerify, btnSubmit, btnCommit, btnCancel, btnExport, btnImport, btnRevoke, btnView, btnPrint, btnSet
            };
            btnCopy.Enabled = btnEdit.Enabled = btnDelete.Enabled = btnActivation.Enabled = btnVerify.Enabled = btnSave.Enabled = btnCommit.Enabled = btnRevoke.Enabled = false;
            UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
        }
        private void InitQueryControlDataSource()   //初始化查询控件数据项
        {
            dtp_repairs_time_s.Value = DateTime.Now.AddMonths(-1);
            dtp_repairs_time_e.Value = DateTime.Now.AddDays(1);
            dtp_approval_date_s.Value = DateTime.Now.AddMonths(-1);
            dtp_approval_date_e.Value = DateTime.Now.AddDays(1);

            txt_vehicle_no.ChooserClick += delegate //车牌号
            {
                var vechicleChooser = new frmVehicleGrade();
                var result = vechicleChooser.ShowDialog();
                if (result != DialogResult.OK) return;
                txt_vehicle_no.Text = vechicleChooser.strLicensePlate;
            };
            txt_customer_code.ChooserClick += delegate  //客户编码(同时会更新客户名称)
            {
                var custChooser = new frmCustomerInfo();
                var result = custChooser.ShowDialog();
                if (result != DialogResult.OK) return;
                txt_customer_code.Text = custChooser.strCustomerNo;
                txt_customer_name.Caption = custChooser.strCustomerName;
            };
            CommonCtrl.CmbBindDict(cbo_bill_type_yt, "bill_type_yt", true);    //单据类型
            CommonCtrl.CmbBindDict(cbo_info_status, "sys_service_info_status", true);  //单据状态
            CommonCtrl.CmbBindDict(cbo_approve_status_yt, "service_single_status_yt", true);  //宇通单据状态
        }
        private void FuncationRegiste()  //注册控件相关事件
        {
            #region 窗体加载事件
            Load += delegate
            {
                dgv_table.ReadOnly = false;
                foreach (DataGridViewColumn dgvc in dgv_table.Columns)
                {
                    if (dgvc == drchk_check)
                    {
                        continue;
                    }
                    dgvc.ReadOnly = true;
                }
            };
            #endregion

            #region 添加按钮事件
            AddEvent += delegate
            {
                var uc = new UCMaintainThreeGuarantyEdit { UCForm = this, windowStatus = WindowStatus.Add };
                uc.addUserControl(uc, "三包服务单-新增", "UCMaintainThreeGuarantyAdd", Tag.ToString(), Name);
            };
            #endregion

            #region 复制按钮事件
            CopyEvent += delegate
            {
                var tgid = "";
                var dataView = GetCheckRows();
                if (dataView != null)
                {
                    foreach (DataRowView rowView in GetSelectedRowData())
                    {
                        tgid = rowView["tg_id"].ToString();
                        break;
                    }
                }
                var uc = new UCMaintainThreeGuarantyEdit { UCForm = this, TgId = tgid, windowStatus = WindowStatus.Copy };
                uc.addUserControl(uc, "三包服务单-复制", "UCMaintainThreeGuarantyCopy", Tag.ToString(), Name);
            };
            #endregion

            #region 编辑按钮事件
            EditEvent += delegate
            {
                var tgid = "";
                var dataView = GetCheckRows();
                if (dataView != null && dataView.Count > 0)
                {
                    foreach (DataRowView rowView in GetSelectedRowData())
                    {
                        tgid = rowView["tg_id"].ToString();
                        break;
                    }
                }
                var uc = new UCMaintainThreeGuarantyEdit { UCForm = this, TgId = tgid, windowStatus = WindowStatus.Edit };
                uc.addUserControl(uc, "三包服务单-编辑", "UCMaintainThreeGuarantyEdit", Tag.ToString(), Name);
            };
            #endregion

            #region 删除按钮事件
            DeleteEvent += delegate
            {
                var selectedRows = GetCheckRows();
                if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                var comField = new Dictionary<string, string> {{"enable_flag", "0"}};
                var flag = DBHelper.BatchUpdateDataByIn("删除三包服务单", TableName, comField, "tg_id", selectedRows.ToArray());
                if (flag)
                {
                    BindPageData();
                    if (dgv_table.Rows.Count > 0)
                    {
                        dgv_table.CurrentCell = dgv_table.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            #endregion

            #region 激活作废按钮事件
            InvalidOrActivationEvent += delegate
            {
                if (MessageBoxEx.Show("确认要" + btnActivation.Caption + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                var selectedRows = GetCheckRows();
                var flag = false;
                foreach (var selectedRow in selectedRows)
                {
                    var dvt = DBHelper.GetTable("获得三包服务单前一个状态", "tb_maintain_three_guaranty_BackUp", "info_status", "tg_id='" + selectedRow + "'", "", "order by Record_id_UpdateTime desc");
                    var oldStatus = DbDic2Enum.SYS_SERVICE_INFO_STATUS_CG;
                    if (dvt.Rows.Count > 0)
                    {
                        var dr = dvt.Rows[0];
                        oldStatus = CommonCtrl.IsNullToString(dr["info_status"]);
                    }
                    flag = DBHelper.Submit_AddOrEdit("作废或激活三包服务单", TableName, "tg_id", selectedRow,
                        new Dictionary<string, string>
                        {
                            {
                                "info_status",  btnActivation.Caption == "作废"  ? DbDic2Enum.SYS_SERVICE_INFO_STATUS_YZF : oldStatus
                            },
                            {
                                "update_by", GlobalStaticObj.UserID
                            },
                            {
                                "update_name", GlobalStaticObj.UserName
                            },
                            {
                                "update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString()
                            }
                        });
                }
                if (flag)
                {
                    BindPageData();
                    if (dgv_table.Rows.Count > 0)
                    {
                        dgv_table.CurrentCell = dgv_table.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show(btnActivation.Caption + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBoxEx.Show(btnActivation.Caption + "失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            #endregion

            #region 提交按钮事件
            btnSubmit.Click += delegate
            {
                if (!GlobalStaticObj.IsDefaultAcc)
                {
                    Common.ShowWarning("不是主账套信息,不允许进行操作!");
                    return;
                }
                var dataView = GetCheckRows();
                if (dataView != null)
                {
                    var cg = 0;
                    var sb = 0;
                    foreach (DataRowView rowView in GetSelectedRowData())
                    {
                        var tgid = CommonCtrl.IsNullToString(rowView["tg_id"]);
                        var status = CommonCtrl.IsNullToString(rowView["info_status"]);
                        var serviceNo = CommonCtrl.IsNullToString(rowView["service_no"]);
                        var dicFields = new Dictionary<string, string>();
                        dicFields.Add("info_status", DbDic2Enum.SYS_SERVICE_INFO_STATUS_YTJ);
                        if (status == DbDic2Enum.SYS_SERVICE_INFO_STATUS_CG && String.IsNullOrEmpty(serviceNo))
                            dicFields.Add("service_no", CommonUtility.GetNewNo(DataSources.EnumProjectType.ThreeGuarantyService));
                        var result = DBHelper.Submit_AddOrEdit("三包服务单提交", "tb_maintain_three_guaranty", "tg_id", tgid, dicFields);
                        if (result) cg++;
                        else sb++;
                    }
                    if (cg != 0)
                    {
                        BindPageData();
                    }
                    var msg = "三包服务单提交操作成功";
                    if (sb != 0 && cg == 0)
                    {
                        msg = "三包服务单提交失败";
                    }
                    else if (cg != 0 && sb == 0)
                    {
                        msg = "三包服务单提交成功";
                    }
                    else
                    {
                        msg += ",但存在"+ sb +"条提交失败的单据";
                    }
                    MessageBoxEx.Show(msg, "操作提示");
                }
                else
                {
                    MessageBoxEx.Show("无法获取到选择的数据,请选择需要操作的数据", "操作提示");
                }
            };
            #endregion

            #region 审核按钮事件
            VerifyEvent += delegate
            {
                var tgid = "";
                var dataView = GetCheckRows();
                if (dataView != null)
                {
                    foreach (DataRowView rowView in GetSelectedRowData())
                    {
                        tgid = rowView["tg_id"].ToString();
                        break;
                    }
                }
                var form = new UCMaintainThreeGuarantyVerify(tgid);
                form.UcForm = this;
                var result = form.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    BindPageData();
                }
            };
            #endregion

            #region 上报厂家/总公司按钮事件
            CommitEvent += delegate
            {
                if (!GlobalStaticObj.IsDefaultAcc)
                {
                    Common.ShowWarning("不是主账套信息,不允许进行操作!");
                    return;
                }
                if (MessageBoxEx.Show("确认要将三包服务单上报宇通吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                var tgid = "";
                var dataView = GetCheckRows();
                if (dataView != null)
                {
                    foreach (DataRowView rowView in GetSelectedRowData())
                    {
                        tgid = rowView["tg_id"].ToString();
                        break;
                    }
                }
                else
                {
                    return;
                }
                var resultStr = Submit2Company(tgid, "100000001");
                MessageBoxEx.Show(String.IsNullOrEmpty(resultStr) ? "三包服务单上报厂家成功!" : resultStr, "操作提示");
                BindPageData();
            };
            #endregion

            #region 撤销按钮事件
            RevokeEvent += delegate
            {
                if (!GlobalStaticObj.IsDefaultAcc)
                {
                    Common.ShowWarning("不是主账套信息,不允许进行操作!");
                    return;
                }
                if (MessageBoxEx.Show("确认要撤销已提交的单据吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                var selectedRowsYt = GetCheckRowsByYt();
                var selectedRows = GetCheckRows();
                var canSubmit2Company = false;
                if (selectedRowsYt != null && selectedRowsYt.Count > 0)
                {
                    var orderStatus = DBHelper.GetSingleValue("获取三包服务单状态", TableName, "info_status", String.Format("tg_id = '{0}'", selectedRows[0]), "");
                    if (orderStatus == DbDic2Enum.SYS_SERVICE_INFO_STATUS_YTJ)
                    {
                        bool flag = DBHelper.Submit_AddOrEdit("撤销三包服务单", "tb_maintain_three_guaranty", "tg_id", selectedRows[0],
                            new Dictionary<string, string>
                            {
                                {
                                    "info_status", DbDic2Enum.SYS_SERVICE_INFO_STATUS_CG
                                 }
                            });
                        if (flag)
                        {
                            BindPageData("enable_flag = '1'");
                            MessageBoxEx.Show("撤销成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBoxEx.Show("撤销失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else if (orderStatus == DbDic2Enum.SYS_SERVICE_INFO_STATUS_SHTG)
                    {
                        var resultStr = DBHelper.WebServHandlerByObj("查询三包服务单状态", EnumWebServFunName.SearchOrderStatus, selectedRowsYt[0]);
                        if (resultStr == "100000001" || String.IsNullOrEmpty(resultStr))
                        {
                            var subResultStr = Submit2Company(selectedRows[0], selectedRowsYt[0], "100000002");
                            var a = subResultStr;
                            var flag = DBHelper.Submit_AddOrEdit("撤销三包服务单", "tb_maintain_three_guaranty", "tg_id", selectedRows[0],
                            new Dictionary<string, string>
                            {
                                {
                                    "info_status", DbDic2Enum.SYS_SERVICE_INFO_STATUS_YTJ
                                }
                            });
                            if (flag)
                            {
                                BindPageData("enable_flag = '1'");
                                MessageBoxEx.Show("撤销成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBoxEx.Show("撤销失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBoxEx.Show("此单据已被引用，无法撤销！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBoxEx.Show("请选择需要撤销的单据信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            #endregion

            #region 导入按钮事件
            ImportEvent += delegate
            {
                //var callbackImport = new UCRepairCallbackImport();
                //callbackImport.Fetchuc = this;
                //callbackImport.strTag = "1";
                //callbackImport.ShowDialog();
            };
            #endregion

            #region 查询按钮事件
            btn_query.Click += delegate
            {
                BindPageData();
            };
            #endregion

            #region 清除按钮事件
            btn_clear.Click += delegate
            {
                txt_service_no.Caption = txt_service_no_yt.Caption = String.Empty;
                txt_vehicle_no.Text = String.Empty;
                txt_depot_no.Caption = String.Empty;
                txt_customer_code.Text = String.Empty;
                txt_customer_name.Caption = String.Empty;
                cbo_bill_type_yt.SelectedIndex = 0;
                cbo_info_status.SelectedIndex = 0;
                cbo_approve_status_yt.SelectedIndex = 0;
                dtp_repairs_time_s.Value = DateTime.Now.AddMonths(-1);
                dtp_repairs_time_e.Value = DateTime.Now.AddDays(1);
                dtp_approval_date_s.Value = DateTime.Now.AddMonths(-1);
                dtp_approval_date_e.Value = DateTime.Now.AddDays(1);
            };
            #endregion

            #region 数据翻页
            pageQ.PageIndexChanged += delegate
            {
                BindPageData();
            };
            #endregion

            #region 数据表数据选择
            dgv_table.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs args)
            {

                if (args.RowIndex < 0) return;
                var tgid = dgv_table.Rows[args.RowIndex].Cells["drtxt_tg_id"].Value.ToString();
                if (String.IsNullOrEmpty(tgid))
                {
                    MessageBoxEx.Show("无效的结算单信息", "操作提示");
                    return;
                }
                var uc = new UCMaintainThreeGuarantyViewDetail {TgId = tgid, UCForm = this};
                uc.addUserControl(uc, "三包服务单-详细信息", "UCMaintainThreeGuarantyViewDetail" + uc.TgId, Tag.ToString(), Name);
            };
            #endregion

            #region 数据表格单元格数据转换
            dgv_table.CellFormatting += (sender, args) => ConvertDataGridColumnsData(args);
            #endregion

            #region 根据选择的数据判断功能按钮的显示状态
            dgv_table.CellMouseUp += delegate
            {
                var dataView = GetSelectedRowData();
                var listField = GetCheckRows();
                if (dataView == null || listField.Count == 0)
                {
                    //btnAdd, btnCopy, btnDelete, btnInvalidOrActivation, btnVerify, btnSave, btnCommit, btnCancel, btnExport, btnImport, btn_Revoke, btnView, btnPrint, btnSet
                    btnCopy.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnActivation.Enabled = false;
                    btnCommit.Enabled = btnSubmit.Enabled = btnVerify.Enabled = false;
                }
                else
                {
                    var cg = 0;
                    var shwtg = 0;
                    var shtg = 0;
                    var ytj = 0;
                    var yzf = 0;
                    foreach (DataRowView drv in dataView)
                    {
                        var status = drv["info_status"].ToString();
                        if (status == DbDic2Enum.SYS_SERVICE_INFO_STATUS_CG) cg++;
                        else if (status == DbDic2Enum.SYS_SERVICE_INFO_STATUS_SHWTG) shwtg++;
                        else if (status == DbDic2Enum.SYS_SERVICE_INFO_STATUS_SHTG) shtg++;
                        else if (status == DbDic2Enum.SYS_SERVICE_INFO_STATUS_YTJ) ytj++;
                        else if (status == DbDic2Enum.SYS_SERVICE_INFO_STATUS_YZF) yzf++;
                    }

                    #region 编辑
                    if (listField.Count == 1 && (cg+shwtg) == listField.Count)
                    {
                        btnEdit.Enabled = true;
                    }
                    else
                    {
                        btnEdit.Enabled = false;
                    }
                    //提交
                    if (listField.Count >= 1 && (cg + shwtg) == listField.Count)
                    {
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        btnSubmit.Enabled = false;
                    }
                    #endregion

                    #region 复制
                    if (listField.Count == 1 )
                    {
                        btnCopy.Enabled = true;
                    }
                    #endregion

                    #region 删除
                    if (listField.Count >= 1 && (cg+shwtg+yzf) == listField.Count)
                    {
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                    }
                    #endregion

                    #region 作废/激活
                    var zf = 0;
                    var jh = 0;
                    if (listField.Count == 1)
                    {
                        foreach (DataRowView rowView in dataView) //作废/激活
                        {
                            if (rowView["info_status"].ToString() == DbDic2Enum.SYS_SERVICE_INFO_STATUS_CG ||
                                rowView["info_status"].ToString() == DbDic2Enum.SYS_SERVICE_INFO_STATUS_SHWTG)
                            {
                                zf ++;
                            }
                            else if (rowView["info_status"].ToString() == DbDic2Enum.SYS_SERVICE_INFO_STATUS_YZF)
                            {
                                jh++;
                            }
                        }
                        if (zf == 0 && jh != 0)
                        {
                            btnActivation.Enabled = true;
                            btnActivation.Caption = "激活";
                            btnActivation.Width = 60;
                        }
                        else if (zf != 0 && jh == 0)
                        {
                            btnActivation.Enabled = true;
                            btnActivation.Caption = "作废";
                            btnActivation.Width = 60;
                        }
                        else
                        {
                            btnActivation.Enabled = false;
                            btnActivation.Caption = "作废/激活";
                            btnActivation.Width = 90;
                        }
                    }
                    else
                    {
                        btnActivation.Enabled = false;
                        btnActivation.Caption = "作废/激活";
                        btnActivation.Width = 90;
                    }
                    #endregion

                    #region 审核
                    if (UIAssistants.ThreeServiceAudit && listField.Count == 1)
                    {
                        btnVerify.Enabled = ytj == listField.Count;
                    }
                    else
                    {
                        btnVerify.Enabled = false;
                    }
                    #endregion

                    #region 上报厂家/总公司

                    if (UIAssistants.ThreeServiceAudit)
                    {
                        if (listField.Count == 1 && listField.Count == shtg)
                        {
                            btnCommit.Enabled = true;
                        }
                        else
                        {
                            btnCommit.Enabled = false;
                        }
                    }
                    else
                    {
                        if (listField.Count == 1 && listField.Count == ytj)
                        {
                            btnCommit.Enabled = true;
                        }
                        else
                        {
                            btnCommit.Enabled = false;
                        }
                    }
                    #endregion

                    #region 撤销
                    if (listField.Count == 1 && cg == 0)
                    {
                        btnRevoke.Enabled = true;
                    }
                    else
                    {
                        btnRevoke.Enabled = false;
                    }
                    #endregion
                }
            };
            #endregion
        }

        private String GetDatetime(Object obj)
        {
            var str = Common.UtcLongToLocalDateTime(CommonCtrl.IsNullToString(obj));
            if (String.IsNullOrEmpty(str))
            {
                return "";
            }
            return Convert.ToDateTime(str).ToString("yyyy-MM-dd HH:mm:ss");
        }
        private void ConvertDataGridColumnsData(DataGridViewCellFormattingEventArgs args) //转换数据列数据
        {
            UIAssistants.DgvCellDataConvert2DicData(dgv_table, args, "info_status");   //单据状态
            UIAssistants.DgvCellDataConvert2DicData(dgv_table, args, "service_single_status_yt"); //宇通同步状态
            UIAssistants.DgvCellDataConvert2Datetime(dgv_table, args, "approve_time"); //审批通过时间
            UIAssistants.DgvCellDataConvert2Datetime(dgv_table, args, "repairs_time"); //报修时间
            UIAssistants.DgvCellDataConvert2DicData(dgv_table, args, "receipt_type");   //单据类型
            UIAssistants.DgvCellDataConvert2DicData(dgv_table, args, "approve_state");   //单据类型
            //UIAssistants.DgvCellDataConvert2Table(dgv_table, args, "vehicle_model", "tb_vehicle_models", "vm_id", "vm_name");   //车型
        }
        /// <summary> 
        /// 绑定数据
        /// </summary>
        public void BindPageData(string where = "")
        {
            try
            {
                if (String.IsNullOrEmpty(where))
                {
                    where = "enable_flag = '1'";
                    if (dtp_repairs_time_s.Value.AddYears(1) < dtp_repairs_time_e.Value)
                    {
                        MessageBoxEx.Show("报修日期选择范围最大为1年!", "操作提示");
                        return;
                    }
                    if (dtp_approval_date_s.Value.AddYears(1) < dtp_approval_date_e.Value)
                    {
                        MessageBoxEx.Show("审批通过时间选择范围最大为1年!", "操作提示");
                        return;
                    }
                    var startTicks4Repairs = dtp_repairs_time_s.Value.Date.ToUniversalTime().Ticks;
                    var endTicks4Repairs = dtp_repairs_time_e.Value.Date.ToUniversalTime().Ticks;
                    where += " and repairs_time >= " + startTicks4Repairs;
                    where += " and repairs_time <= " + endTicks4Repairs;

                    //var startTicks4Approve = dtp_approval_date_s.Value.Date.ToUniversalTime().Ticks;
                    //var endTicks4Approve = dtp_approval_date_e.Value.Date.ToUniversalTime().Ticks;
                    //where += " and approval_date >= " + startTicks4Approve;
                    //where += " and approval_date <= " + endTicks4Approve;

                    if (txt_service_no.Caption != null && !string.IsNullOrEmpty(txt_service_no.Caption))
                    {
                        where += string.Format(" and service_no like '%{0}%'", txt_service_no.Caption);
                    }
                    if (txt_service_no_yt.Caption != null && !string.IsNullOrEmpty(txt_service_no_yt.Caption))
                    {
                        where += string.Format(" and service_no_yt like '%{0}%'", txt_service_no_yt.Caption);
                    }
                    if (txt_vehicle_no.Text != null && !string.IsNullOrEmpty(txt_vehicle_no.Text))
                    {
                        where += string.Format(" and vehicle_no like '%{0}%'", txt_vehicle_no.Text);
                    }
                    if (txt_depot_no.Caption != null && !string.IsNullOrEmpty(txt_depot_no.Caption))
                    {
                        where += string.Format(" and depot_no like '%{0}%'", txt_depot_no.Caption);
                    }
                    if (txt_customer_code.Text != null && !string.IsNullOrEmpty(txt_customer_code.Text))
                    {
                        where += string.Format(" and customer_code like '%{0}%'", txt_customer_code.Text);
                    }
                    if (txt_customer_name.Caption != null && !string.IsNullOrEmpty(txt_customer_name.Caption))
                    {
                        where += string.Format(" and customer_name like '%{0}%'", txt_customer_name.Caption);
                    }
                    if (cbo_bill_type_yt.SelectedValue != null && !string.IsNullOrEmpty(cbo_bill_type_yt.SelectedValue.ToString()))
                    {
                        where += string.Format(" and receipt_type like '%{0}%'", CommonCtrl.IsNullToString(cbo_bill_type_yt.SelectedValue));
                    }
                    if (cbo_approve_status_yt.SelectedValue != null && !string.IsNullOrEmpty(cbo_approve_status_yt.SelectedValue.ToString()))
                    {
                        where += string.Format(" and approve_status_yt like '%{0}%'", CommonCtrl.IsNullToString(cbo_approve_status_yt.SelectedValue));
                    }
                    if (cbo_info_status.SelectedValue != null && !string.IsNullOrEmpty(cbo_info_status.SelectedValue.ToString()))
                    {
                        where += string.Format(" and info_status like '%{0}%'", CommonCtrl.IsNullToString(cbo_info_status.SelectedValue));
                    }
                }
                int recordCount;
                var dt = DBHelper.GetTableByPage("分页查询三包服务单信息", "v_maintain_three_guaranty_approve", "*", where, "", "repairs_time desc", pageQ.PageIndex, pageQ.PageSize, out recordCount);
                dgv_table.DataSource = dt;
                pageQ.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "错误的操作！");
            }

            SetFuncationButtonVisible();
        }
        private List<string> GetCheckRows()
        {
            return (from DataGridViewRow dr in dgv_table.Rows let isCheck = dr.Cells["drchk_check"].EditedFormattedValue where isCheck != null && (bool) isCheck select dr.Cells["drtxt_tg_id"].Value.ToString()).ToList();
        }
        private List<string> GetCheckRowsByYt()
        {
            return (from DataGridViewRow dr in dgv_table.Rows let isCheck = dr.Cells["drchk_check"].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells[drtxt_service_no_yt.Name].Value.ToString()).ToList();
        }
        private DataView GetSelectedRowData()
        {
            var selectedRows = (from DataGridViewRow dr in dgv_table.Rows let isCheck = dr.Cells["drchk_check"].EditedFormattedValue where isCheck != null && (bool)isCheck select dr.Cells["drtxt_tg_id"].Value.ToString()).ToList();
            if (selectedRows.Count > 0)
            {
                var whereSqlSb = new StringBuilder();
                whereSqlSb.Append(" 1=1");
                foreach (var selectedRow in selectedRows)
                {
                    whereSqlSb.Append(" or tg_id ='");
                    whereSqlSb.Append(selectedRow);
                    whereSqlSb.Append("'");
                }
                var whereSql = whereSqlSb.ToString().Replace(" 1=1 or ", "");
                var data = DBHelper.GetTable("根据ID获取三包服务单信息", "tb_maintain_three_guaranty", "*", whereSql, "", "");
                if (data != null && data.DefaultView != null)
                {
                    return data.DefaultView;
                }
            }
            return null;
        }

        public String Submit2Company(String tgid, String ytServiceStatus)
        {
            return Submit2Company(tgid, null, ytServiceStatus);

        }
        public String Submit2Company(String tgid, String ytServiceOrderNo, String ytServiceStatus)
        {
            serviceorder serviceOrder = null;

            #region 基础信息
            var dmt4 = DBHelper.GetTable("维修项目数据", "tb_maintain_three_guaranty", "*", string.Format(" tg_id='{0}'", tgid), "", "");
            if (dmt4.Rows.Count > 0)
            {
                var dr = dmt4.Rows[0];
                serviceOrder = new serviceorder
                {
                    tg_id = CommonCtrl.IsNullToString(dr["tg_id"]),
                    crm_service_bill_code = String.IsNullOrEmpty(ytServiceOrderNo) ? CommonCtrl.IsNullToString(dr["service_no_yt"]) : ytServiceOrderNo,
                    //sap_code = "155125",//CommonCtrl.IsNullToString(dr["service_station_code"]),
                    service_station_name = CommonCtrl.IsNullToString(dr["service_station_name"]),
                    bill_type_yt = CommonCtrl.IsNullToString(dr["receipt_type"]),
                    whether_go_out = CommonCtrl.IsNullToString(dr["whether_go_out"]),
                    refit_case = "",//CommonCtrl.IsNullToString(dr["refit_case"]), //不处理 -- 改装情况
                    dsn_service_bill_code = CommonCtrl.IsNullToString(dr["series_num_yt"]),
                    create_time = GetDatetime(dr["create_time"]),
                    depot_no = CommonCtrl.IsNullToString(dr["depot_no"]),
                    vehicle_no = CommonCtrl.IsNullToString(dr["vehicle_no"]),
                    travel_mileage = CommonCtrl.IsNullToString(dr["travel_mileage"]),
                    vehicle_use = CommonCtrl.IsNullToString(dr["customer_property"]),   //不处理 -- 用户性质(车辆用途)
                    vehicle_location = "",//CommonCtrl.IsNullToString(dr["vehicle_location"]), //不处理 -- 车辆所在地代码
                    vehicle_use_corp = CommonCtrl.IsNullToString(dr["vehicle_use_corp"]),
                    linkman = CommonCtrl.IsNullToString(dr["linkman"]),
                    link_man_mobile = CommonCtrl.IsNullToString(dr["link_man_mobile"]),
                    repairer_name = CommonCtrl.IsNullToString(dr["repairer_name"]),
                    repairer_mobile = CommonCtrl.IsNullToString(dr["repairer_mobile"]),
                    driver_name = CommonCtrl.IsNullToString(dr["driver_name"]),
                    driver_mobile = CommonCtrl.IsNullToString(dr["driver_mobile"]),
                    start_work_time = GetDatetime(dr["start_work_time"]),
                    complete_work_time = GetDatetime(dr["complete_work_time"]),
                    appraiser_name = CommonCtrl.IsNullToString(dr["appraiser_id"]),
                    repair_man = CommonCtrl.IsNullToString(dr["repair_man_id"]),
                    travel_desc = CommonCtrl.IsNullToString(dr["goout_cause"]),
                    approve_by_people = CommonCtrl.IsNullToString(dr["goout_approver"]),
                    apply_rescue_place = CommonCtrl.IsNullToString(dr["goout_place"]),
                    travel_lookup_code = CommonCtrl.IsNullToString(dr["means_traffic"]),
                    depart_time = GetDatetime(dr["goout_time"]),
                    back_time = GetDatetime(dr["goout_back_time"]),
                    rescue_mileage = CommonCtrl.IsNullToString(dr["goout_mileage"]),
                    travel_employees = CommonCtrl.IsNullToString(dr["goout_people_num"]),
                    hour_amount = CommonCtrl.IsNullToString(dr["man_hour_subsidy"]),
                    mile_allowance = CommonCtrl.IsNullToString(dr["journey_subsidy"]),
                    hotel_fee = "",//CommonCtrl.IsNullToString(dr["driver_name"]),       //不处理 -- 住宿费
                    traffic_fee = CommonCtrl.IsNullToString(dr["traffic_fee"]), //车船费
                    travel_amount = CommonCtrl.IsNullToString(dr["travel_cost"]),
                    other_item_sum_money = CommonCtrl.IsNullToString(dr["other_item_sum_money"]),
                    other_remarks = "",//CommonCtrl.IsNullToString(dr["remarks"]),   //数据库暂无此字段 -- 其他费用说明
                    should_sum = CommonCtrl.IsNullToString(dr["service_sum_cost"]),
                    remarks = CommonCtrl.IsNullToString(dr["remarks"]),
                    approve_status_yt = String.IsNullOrEmpty(ytServiceStatus) ? CommonCtrl.IsNullToString(dr["approve_status_yt"]): ytServiceStatus,
                    vehicle_location_name = "",//CommonCtrl.IsNullToString(dr["customer_address"]),  //不处理 -- 车辆所在地名称
                    fault_class_code = CommonCtrl.IsNullToString(dr["fault_system"]),
                    fault_class_name = CommonCtrl.IsNullToString(dr["fault_describe"]),
                    fault_assembly_code = CommonCtrl.IsNullToString(dr["fault_assembly"]),
                    fault_assembly_name = CommonCtrl.IsNullToString(dr["fault_assembly_name"]), //不处理 -- 故障总成名称
                    fault_part_code = CommonCtrl.IsNullToString(dr["fault_part"]),
                    fault_part_name = CommonCtrl.IsNullToString(dr["fault_part_name"]), //不处理 -- 故障总成部件名称
                    fault_mode_code = CommonCtrl.IsNullToString(dr["fault_schema"]),
                    fault_mode_name = CommonCtrl.IsNullToString(dr["fault_schema_name"]),   //不处理 -- 故障模式名称
                    fault_cause = CommonCtrl.IsNullToString(dr["fault_cause"]),
                    reason_analysis = CommonCtrl.IsNullToString(dr["reason_analysis"]),
                    dispose_result = CommonCtrl.IsNullToString(dr["dispose_result"]),
                    approver_name_yt = CommonCtrl.IsNullToString(dr["approver_id_yt"]),
                    policy_approval_no = CommonCtrl.IsNullToString(dr["policy_approval_no"]),
                    policy_cost_type = CommonCtrl.IsNullToString(dr["cost_type_policy"]),
                    describe = CommonCtrl.IsNullToString(dr["describes"]),
                    product_notice_no = CommonCtrl.IsNullToString(dr["product_notice_no"]),
                    luxury_cost_type = "",//CommonCtrl.IsNullToString(dr["cost_type_service"]),  //数据库暂无此字段 -- 费用类型高档车
                    activity_cost_type = CommonCtrl.IsNullToString(dr["cost_type_service"]),
                    fault_duty_corp = CommonCtrl.IsNullToString(dr["fault_duty_corp"]),
                    fault_describe = CommonCtrl.IsNullToString(dr["fault_describe"]),
                    man_hour_cost = CommonCtrl.IsNullToString(dr["man_hour_sum_money"]),
                    parts_cost = CommonCtrl.IsNullToString(dr["fitting_sum_money"]),
                    parts_buy_time = GetDatetime(dr["parts_buy_time"]),
                    parts_buy_corp = CommonCtrl.IsNullToString(dr["parts_buy_corp"]),

                    contain_man_hour_cost = CommonCtrl.IsNullToString(dr["contain_man_hour_cost"]),
                    car_parts_code = CommonCtrl.IsNullToString(dr["parts_code"]),
                    parts_name = "",//CommonCtrl.IsNullToString(dr["man_hour_sum_money"]),   //不处理 -- 配件名称
                    first_install_station = CommonCtrl.IsNullToString(dr["first_install_station"]),
                    part_guarantee_period = CommonCtrl.IsNullToString(dr["part_guarantee_period"]),
                    part_lot = "",//CommonCtrl.IsNullToString(dr["parts_buy_corp"]), //不处理 -- 配件批次号
                    feedback_num = CommonCtrl.IsNullToString(dr["feedback_num"]),
                    is_special_agreed_warranty = CommonCtrl.IsNullToString(dr["promise_guarantee"]),
                    repairs_time = GetDatetime(dr["repairs_time"])
                };

            #endregion

                #region 维修用料数据

                var dmt1 = DBHelper.GetTable("维修用料数据", "tb_maintain_three_guaranty_material_detail", "*",
                    string.Format(" tg_id='{0}'", tgid), "", "");
                var flFlag = 0; //判断辅料金额 不添加辅料:0 --- A,B车型:40 --- C车型:50
                if ((serviceOrder.bill_type_yt == DbDic2Enum.BILL_TYPE_YT_100000000 ||
                        serviceOrder.bill_type_yt == DbDic2Enum.BILL_TYPE_YT_100000002 ||
                        serviceOrder.bill_type_yt == DbDic2Enum.BILL_TYPE_YT_100000001))
                {
                    if (CommonCtrl.IsNullToString(dr["vehicle_model"]) == DbDic2Enum.VM_CLASS_100000000 ||
                        CommonCtrl.IsNullToString(dr["vehicle_model"]) == DbDic2Enum.VM_CLASS_100000001)
                    {
                        flFlag = 40;
                    }
                    else if (CommonCtrl.IsNullToString(dr["vehicle_model"]) == DbDic2Enum.VM_CLASS_10000003)
                    {
                        flFlag = 50;
                    }
                }
                serviceOrder.ChangePartsDetails = new ChangePartsDetail[flFlag == 0 ? dmt1.Rows.Count : dmt1.Rows.Count + 1];
                if (dmt1.Rows.Count > 0)
                {
                    for (var i = 0; i < dmt1.Rows.Count; i++)
                    {
                        var dmr = dmt1.Rows[i];
                        serviceOrder.ChangePartsDetails[i] = new ChangePartsDetail();
                        serviceOrder.ChangePartsDetails[i].car_parts_code =
                            CommonCtrl.IsNullToString(dmr["depot_code"]);
                        serviceOrder.ChangePartsDetails[i].parts_name = CommonCtrl.IsNullToString(dmr["parts_name"]);
                        serviceOrder.ChangePartsDetails[i].parts_source =
                            CommonCtrl.IsNullToString(dmr["parts_source"]);
                        //serviceOrder.ChangePartsDetails[i]. = CommonCtrl.IsNullToString(dmr["norms"]);
                        serviceOrder.ChangePartsDetails[i].unit = CommonCtrl.IsNullToString(dmr["unit"]);
                        serviceOrder.ChangePartsDetails[i].quantity = CommonCtrl.IsNullToString(dmr["quantity"]);
                        serviceOrder.ChangePartsDetails[i].unit_price = CommonCtrl.IsNullToString(dmr["unit_price"]);
                        serviceOrder.ChangePartsDetails[i].sum_money = CommonCtrl.IsNullToString(dmr["sum_money"]);
                        //serviceOrder.ChangePartsDetails[i]. = CommonCtrl.IsNullToString(dmr["redeploy_no"]);
                        if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["remarks"])))
                        {
                            serviceOrder.ChangePartsDetails[i].remarks = CommonCtrl.IsNullToString(dmr["parts_name"]);
                        }
                        serviceOrder.ChangePartsDetails[i].remarks = CommonCtrl.IsNullToString(dmr["remarks"]);

                    }
                    //三包单中“新车报道”“走保”“强保”类型，服务站上报宇通时可以多添加一个“辅料”，来源于宇通数据，A、B车型金额：40.00元，C车型（高档车）金额50.00元！
                    if (flFlag != 0)
                    {
                        var changePartsDetail = new ChangePartsDetail();
                        serviceOrder.ChangePartsDetails[serviceOrder.ChangePartsDetails.Count() - 1] = changePartsDetail;
                        changePartsDetail.sum_money = flFlag.ToString();
                        changePartsDetail.remarks = "辅料";
                    }
                }

                #endregion

                #region 维修项目

                var dmt2 = DBHelper.GetTable("维修项目数据", "tb_maintain_three_guaranty_item", "*",
                    string.Format(" tg_id='{0}'", tgid), "", "");
                serviceOrder.RepairItemsDetails = new RepairItems[dmt2.Rows.Count];
                if (dmt2.Rows.Count > 0)
                {
                    for (var i = 0; i < dmt2.Rows.Count; i++)
                    {
                        var dmr = dmt2.Rows[i];
                        serviceOrder.RepairItemsDetails[i] = new RepairItems();
                        serviceOrder.RepairItemsDetails[i].item_no = CommonCtrl.IsNullToString(dmr["item_no"]);
                        //serviceOrder.RepairItemsDetails[i]. = CommonCtrl.IsNullToString(dmr["item_type"]);
                        serviceOrder.RepairItemsDetails[i].item_name = CommonCtrl.IsNullToString(dmr["item_name"]);
                        serviceOrder.RepairItemsDetails[i].man_hour_type = CommonCtrl.IsNullToString(dmr["man_hour_quantity"]);
                        serviceOrder.RepairItemsDetails[i].man_hour_unitprice = CommonCtrl.IsNullToString(dmr["man_hour_unitprice"]);
                        //serviceOrder.RepairItemsDetails[i].unit_price = CommonCtrl.IsNullToString(dmr["man_hour_unitprice"]);
                        serviceOrder.RepairItemsDetails[i].sum_money = CommonCtrl.IsNullToString(dmr["sum_money"]);
                        serviceOrder.RepairItemsDetails[i].remarks = CommonCtrl.IsNullToString(dmr["remarks"]);
                    }
                }
                #endregion

                #region 附件信息

                var dmt3 = DBHelper.GetTable("三包服务单附件数据", "attachment_info", "*",
                    string.Format(" relation_object_id='{0}'", tgid), "", "");
                serviceOrder.FilesDetails = new Files[dmt3.Rows.Count];
                if (dmt3.Rows.Count > 0)
                {
                    for (var i = 0; i < dmt3.Rows.Count; i++)
                    {
                        var dmr = dmt3.Rows[i];
                        string path = CommonCtrl.IsNullToString(dmr["att_path"]);
                        var fs = FileOperation.DownLoadString16(path);
                        serviceOrder.FilesDetails[i] = new Files();
                        serviceOrder.FilesDetails[i].Doc = fs;
                        serviceOrder.FilesDetails[i].doc_name = CommonCtrl.IsNullToString(dmr["att_id"]) + Path.GetExtension((path));
                        serviceOrder.FilesDetails[i].accessory_name = CommonCtrl.IsNullToString(dmr["att_id"]) + Path.GetExtension(path);
                    }
                }

                #endregion

            }
            return DBHelper.WebServHandler("三包服务单上报厂家", EnumWebServFunName.UpLoadServiceOrder, serviceOrder);

        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
