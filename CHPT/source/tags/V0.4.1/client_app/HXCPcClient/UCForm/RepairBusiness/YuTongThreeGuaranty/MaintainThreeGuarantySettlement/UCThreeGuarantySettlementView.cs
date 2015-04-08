using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;

namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuarantySettlement
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     UCThreeGuarantySettlementView         
    /// Author:       Kord
    /// Date:         2014/10/30 16:35:03
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	UCThreeGuarantySettlementView
    ///***************************************************************************//
    public partial class UCThreeGuarantySettlementView : UCBase
    {

        #region Constructor -- 构造函数
        public UCThreeGuarantySettlementView()
        {
            InitializeComponent();

            Load += (sender, args) => Init();
        }
        #endregion

        #region Field -- 字段
        #endregion

        #region Property -- 属性
        /// <summary>
        /// 结算单号
        /// </summary>
        public String SettlementId = String.Empty;
        #endregion

        #region 方法

        private void Init()
        {
            SetFuncationButtonVisible();
            SetSettlementInfo();
            SetServiceInfo();
            SetSettlementOldInfo();
            SetSettlementOtherInfo();
            SetSettlementInventoryInfo();

            #region 确认

            btnConfirm.Click += delegate
            {
                var info = DBHelper.GetTable("查询结算单信息", "tb_maintain_three_guaranty_settlement", "*",
                    "st_id = '" + SettlementId + "'", "", "");

                if (info != null && info.DefaultView.Count != 0)
                {
                    foreach (DataRowView dr in info.DefaultView)
                    {
                        if (dr["info_status"].ToString() == DbDic2Enum.SYS_STATION_SETTLEMENT_STATUS_CONFIRM) continue;
                        var stId = dr["st_id"].ToString();
                        var sqlObjList = new List<SQLObj>();
                        var sqlObj = new SQLObj
                        {
                            cmdType = CommandType.Text,
                            sqlString =
                                "update tb_maintain_three_guaranty_settlement set info_status=@info_status where st_id=@st_id",
                            Param = new Dictionary<string, ParamObj>
                            {
                                {
                                    "info_status",
                                    new ParamObj
                                    {
                                        name = "info_status",
                                        value = DbDic2Enum.SYS_STATION_SETTLEMENT_STATUS_CONFIRM
                                    }
                                },
                                {"st_id", new ParamObj {name = "st_id", value = stId}}
                            }
                        };
                        UIAssistants.SetParamObjInfoByDB("tb_maintain_three_guaranty_settlement", sqlObj.Param);
                        sqlObjList.Add(sqlObj);

                        DBHelper.BatchExeSQLMultiByTrans("结算单信息确认", sqlObjList);
                    }
                }
            };
            #endregion
        }
        private void SetFuncationButtonVisible()    //设置功能按钮可见性
        {
            var btnCols = new ObservableCollection<ButtonEx_sms>()
            {
                btnConfirm,btnExport,btnView,btnPrint,btnPrint,btnSet
            };
            UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
        }
        private void SetSettlementInfo()
        {
            var info = DBHelper.GetTable("查询结算单信息", "tb_maintain_three_guaranty_settlement", "*", "st_id = '" + SettlementId + "'", "", "");
            if (info != null && info.DefaultView.Count != 0)
            {
                lbl_station_settlement_no.Text = info.DefaultView[0]["settlement_no"].ToString();
                lbl_service_station_name.Text = info.DefaultView[0]["service_station_name"].ToString();
                lbl_finance_voucher_no.Text = info.DefaultView[0]["finance_voucher_no"].ToString();
                lbl_accountant_annual.Text = info.DefaultView[0]["accountant_annual"].ToString();
                lbl_company_code.Text = info.DefaultView[0]["company_code"].ToString();
                lbl_sum_cost.Text = info.DefaultView[0]["sum_cost"].ToString();
                lbl_bill_sum_cost.Text = info.DefaultView[0]["bill_sum_cost"].ToString();
                lbl_oldpart_sum_cost.Text = info.DefaultView[0]["oldpart_sum_cost"].ToString();
                lbl_other_sum_cost.Text = info.DefaultView[0]["other_sum_cost"].ToString();
                lbl_settlement_cycle.Text = info.DefaultView[0]["settlement_cycle_start"].ToString();
            }
        }
        private void SetServiceInfo()
        {
            dgv_tb_maintain_three_guaranty_settlement_ser.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs args)
            {
                UIAssistants.DgvCellDataConvert2Datetime(dgv_tb_maintain_three_guaranty_settlement_ser, args, "create_time");
                UIAssistants.DgvCellDataConvert2DicData(dgv_tb_maintain_three_guaranty_settlement_ser, args, "receipt_type");
                UIAssistants.DgvCellDataConvert2Datetime(dgv_tb_maintain_three_guaranty_settlement_ser, args, "approve_time");
            };
            var info = DBHelper.GetTable("查询维修项目结算单信息", "tb_maintain_three_guaranty_settlement_ser", "*", "st_id = '" + SettlementId + "'", "", "");
            if (info != null && info.DefaultView.Count != 0)
            {
                dgv_tb_maintain_three_guaranty_settlement_ser.DataSource = info;
            }
        }
        private void SetSettlementOldInfo()
        {
            var info = DBHelper.GetTable("查询旧件结算信息", "tb_maintain_three_guaranty_settlement_old", "*", "st_id = '" + SettlementId + "'", "", "");
            if (info != null && info.DefaultView.Count != 0)
            {
                dgv_tb_maintain_three_guaranty_settlement_old.DataSource = info;
            }
        }
        private void SetSettlementOtherInfo()
        {
            dgv_tb_maintain_three_guaranty_settlement_oth.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs args)
            {
                UIAssistants.DgvCellDataConvert2Datetime(dgv_tb_maintain_three_guaranty_settlement_oth, args, "tally_time");
                //UIAssistants.DgvCellDataConvert2DicData(dgv_tb_maintain_three_guaranty_settlement_oth, args, "cost_type");
            };

            var info = DBHelper.GetTable("查询旧件其他费用结算信息", "tb_maintain_three_guaranty_settlement_oth", "*", "st_id = '" + SettlementId + "'", "", "");
            if (info != null && info.DefaultView.Count != 0)
            {
                dgv_tb_maintain_three_guaranty_settlement_oth.DataSource = info;
            }
        }
        private void SetSettlementInventoryInfo()
        {
            dgv_tb_maintain_three_guaranty_settlement_inv.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs args)
            {
                UIAssistants.DgvCellDataConvert2Datetime(dgv_tb_maintain_three_guaranty_settlement_inv, args, "create_time_invoice");
                UIAssistants.DgvCellDataConvert2DicData(dgv_tb_maintain_three_guaranty_settlement_inv, args, "invoice_type");
                UIAssistants.DgvCellDataConvert2DicData(dgv_tb_maintain_three_guaranty_settlement_inv, args, "cost_type_invoice");
            };

            var info = DBHelper.GetTable("查询旧件结算发票信息", "tb_maintain_three_guaranty_settlement_inv", "*", "st_id = '" + SettlementId + "'", "", "");
            if (info != null && info.DefaultView.Count != 0)
            {
                dgv_tb_maintain_three_guaranty_settlement_inv.DataSource = info;
            }
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
