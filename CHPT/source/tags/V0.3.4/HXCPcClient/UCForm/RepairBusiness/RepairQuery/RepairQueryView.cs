using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;

namespace HXCPcClient.UCForm.RepairBusiness.RepairQuery
{
    /// <summary>
    /// 维修管理-维修单查询预览
    /// Author：JC
    /// AddTime：2014.10.20
    /// </summary>
    public partial class RepairQueryView : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 维修单Id
        /// </summary>
        string strRepairId = string.Empty;
        #endregion
        public RepairQueryView(string strReId)
        {
            InitializeComponent();
            TopButtonSeting();
            SetDgvAnchor();
            strRepairId = strReId;
            GetRepairInfo();
            SetTopbuttonShow();
        }

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnAdd.Visible = false;
            base.btnCopy.Visible = false;
            base.btnDelete.Visible = false;
            base.btnEdit.Visible = false;        
            base.btnSave.Visible = false;
            base.btnCancel.Visible = false;
            base.btnImport.Visible = false;
            base.btnStatus.Visible = false;
            base.btnSubmit.Visible = false;
            base.btnVerify.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 根据维修单Id获取维修单信息
        private void GetRepairInfo()
        {
            DataTable dt = DBHelper.GetTable("维修单详情", "tb_maintain_info a left join tb_maintain_settlement_info b on a.maintain_id=b.maintain_id ", "*", string.Format(" a.maintain_id='{0}'", strRepairId), "", "");
            if (dt.Rows.Count > 0)
            {
                #region 基本信息
                DataRow dr = dt.Rows[0];
                labMaintain_noS.Text = CommonCtrl.IsNullToString(dr["maintain_no"]);//维修单号
                string strReTime = CommonCtrl.IsNullToString(dr["reception_time"]);//接待时间
                if (!string.IsNullOrEmpty(strReTime))
                {
                    labRTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strReTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labRTimeS.Text = string.Empty;
                }
                labCustomNOS.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                labCustomNameS.Text = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称
                labContactS.Text = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                labContactPhoneS.Text = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人电话
                labCarNOS.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                labCarTypeS.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型
                labCarBrandS.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_brand"]));//车辆品牌
                labVINS.Text = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                labEngineNoS.Text = CommonCtrl.IsNullToString(dr["engine_no"]);//发动机号
                labColorS.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_color"]));//颜色
                labDriverS.Text = CommonCtrl.IsNullToString(dr["driver_name"]);//司机
                labDriverPhoneS.Text = CommonCtrl.IsNullToString(dr["driver_mobile"]);//司机手机
                labRepTypeS.Text = GetDicName(CommonCtrl.IsNullToString(dr["maintain_type"]));//维修类别
                labPayTypeS.Text = GetDicName(CommonCtrl.IsNullToString(dr["maintain_payment"]));//维修付费方式                
                labMlS.Text = CommonCtrl.IsNullToString(dr["oil_into_factory"]);//进场油量
                labMilS.Text = CommonCtrl.IsNullToString(dr["travel_mileage"]);//行驶里程
                labDescS.Text = CommonCtrl.IsNullToString(dr["fault_describe"]);//故障描述 
                labRemarkS.Text = CommonCtrl.IsNullToString(dr["remark"]);//备注
                if (CommonCtrl.IsNullToString(dr["orders_source"]) == "3")
                {
                    labStatusS.Text = "维修单结算" + DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dr["info_status"]))); ;
                }
                else
                {
                    if (CommonCtrl.IsNullToString(dr["info_status"]) != Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString())
                    {
                        labStatusS.Text = "接待" + DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(CommonCtrl.IsNullToString(dr["info_status"])));
                    }
                    else
                    {
                        labStatusS.Text = DataSources.GetDescription(typeof(DataSources.EnumDispatchStatus), int.Parse(CommonCtrl.IsNullToString(dr["dispatch_status"])));//单据状态                
                    }
                }
               
                labDepartS.Text = GetDepartmentName(CommonCtrl.IsNullToString(dr["org_name"]));//部门
                labAttnS.Text =GetUserSetName(CommonCtrl.IsNullToString(dr["responsible_name"]));//经办人
                labCreatePersonS.Text = CommonCtrl.IsNullToString(dr["create_name"]);//创建人
                string strCreateTime = CommonCtrl.IsNullToString(dr["create_time"]); //创建时间
                if (!string.IsNullOrEmpty(strCreateTime))
                {
                    labCreateTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strCreateTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labCreateTimeS.Text = string.Empty;
                }
                labFinallyPerS.Text = CommonCtrl.IsNullToString(dr["update_name"]);//最后编辑人
                string strFinallyTime = CommonCtrl.IsNullToString(dr["update_time"]); //最后编辑时间
                if (!string.IsNullOrEmpty(strFinallyTime))
                {
                    labFinallyTimeS.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(strFinallyTime)).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    labFinallyTimeS.Text = string.Empty;
                }
                labmaintain_manS.Text = CommonCtrl.IsNullToString(dr["maintain_man"]);//服务顾问

                #region 会员信息
                string strMemnerID = CommonCtrl.IsNullToString(dr["member_id"]);//会员信息Id
                if (!string.IsNullOrEmpty(strMemnerID))
                {
                    DataTable dct = DBHelper.GetTable("获取会员信息", "tb_customer", "member_number,member_class,accessories_discount,workhours_discount", " is_member='1' and cust_id='" + strMemnerID + "'", "", "");
                    labMemberNoS.Text = CommonCtrl.IsNullToString(dr["member_number"]);//会员卡号
                    labMemberGradeS.Text = CommonCtrl.IsNullToString(dr["member_class"]);//会员等级
                    labMemberPZkS.Text = CommonCtrl.IsNullToString(dr["workhours_discount"]);//会员项目折扣
                    labMemberLZkS.Text = CommonCtrl.IsNullToString(dr["accessories_discount"]);//会员用料折扣

                }
                else
                {
                    labMemberNoS.Text = string.Empty;//会员卡号
                    labMemberGradeS.Text = string.Empty;//会员等级
                    labMemberPZkS.Text = string.Empty;//会员项目折扣
                    labMemberLZkS.Text = string.Empty;
                }
                #endregion
                labGshkS.Text = CommonCtrl.IsNullToString(dr["man_hour_sum_money"]);//工时货款
                labGsslS.Text = CommonCtrl.IsNullToString(dr["man_hour_tax_rate"]);//工时税率
                labGsseS.Text = CommonCtrl.IsNullToString(dr["man_hour_tax_rate"]);//工时税额
                labGssjhjS.Text = CommonCtrl.IsNullToString(dr["man_hour_sum"]);//工时税价合计
                labPjhkS.Text = CommonCtrl.IsNullToString(dr["fitting_sum_money"]);//配件货款
                labPjslS.Text = CommonCtrl.IsNullToString(dr["fitting_tax_rate"]);//配件税率
                labPjseS.Text = CommonCtrl.IsNullToString(dr["fitting_tax_cost"]);//配件税额
                labPjsjhjS.Text = CommonCtrl.IsNullToString(dr["fitting_sum"]);//配件税价合计
                labQtflS.Text = CommonCtrl.IsNullToString(dr["other_item_sum_money"]);//其他项目费用
                QtslS.Text = CommonCtrl.IsNullToString(dr["other_item_tax_rate"]);//其他项目税率
                labQtseS.Text = CommonCtrl.IsNullToString(dr["other_item_tax_cost"]);//其他项目税额
                labQthjS.Text = CommonCtrl.IsNullToString(dr["other_item_sum"]);//其他项目价税合计
                labYH.Text = CommonCtrl.IsNullToString(dr["privilege_cost"]);//优惠费用
                labShould.Text = CommonCtrl.IsNullToString(dr["should_sum"]);//应收总额
                labReceive.Text = CommonCtrl.IsNullToString(dr["received_sum"]);//实收总额
                labDebt.Text = CommonCtrl.IsNullToString(dr["debt_cost"]);//欠款金额
                labInvoiceType.Text = CommonCtrl.IsNullToString(dr["make_invoice_type"]);//开票类型
                labPayment.Text = CommonCtrl.IsNullToString(dr["payment_terms"]);//结算方式
                labSetAccount.Text = CommonCtrl.IsNullToString(dr["settlement_account"]);//结算账户
                labSetCompany.Text = CommonCtrl.IsNullToString(dr["settle_company"]);//结算单位
                #endregion

                #region 底部datagridview数据

                #region 维修项目数据
                //维修项目数据     
                decimal dcPmoney = 0;
                DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id='{0}'", strRepairId), "", ""); ;
                if (dpt.Rows.Count > 0)
                {
                    if (dpt.Rows.Count > dgvproject.Rows.Count)
                    {
                        dgvproject.Rows.Add(dpt.Rows.Count - dgvproject.Rows.Count + 1);
                    }
                    for (int i = 0; i < dpt.Rows.Count; i++)
                    {
                        DataRow dpr = dpt.Rows[i];
                        dgvproject.Rows[i].Cells["item_id"].Value = CommonCtrl.IsNullToString(dpr["item_id"]);
                        dgvproject.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dpr["three_warranty"]) == "1" ? "是" : "否";
                        dgvproject.Rows[i].Cells["man_hour_type"].Value = CommonCtrl.IsNullToString(dpr["man_hour_type"]);
                        dgvproject.Rows[i].Cells["item_no"].Value = CommonCtrl.IsNullToString(dpr["item_no"]);
                        dgvproject.Rows[i].Cells["item_name"].Value = CommonCtrl.IsNullToString(dpr["item_name"]);
                        dgvproject.Rows[i].Cells["item_type"].Value = CommonCtrl.IsNullToString(dpr["item_type"]);
                        dgvproject.Rows[i].Cells["man_hour_quantity"].Value = CommonCtrl.IsNullToString(dpr["man_hour_quantity"]);
                        dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"]);
                        dgvproject.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dpr["remarks"]);
                        dgvproject.Rows[i].Cells["sum_money_goods"].Value = CommonCtrl.IsNullToString(dpr["sum_money_goods"]);
                        dgvproject.Rows[i].Cells["member_discount"].Value = CommonCtrl.IsNullToString(dpr["member_discount"]);
                        dgvproject.Rows[i].Cells["member_price"].Value = CommonCtrl.IsNullToString(dpr["member_price"]);
                        dgvproject.Rows[i].Cells["member_sum_money"].Value = CommonCtrl.IsNullToString(dpr["member_sum_money"]);

                    }                   
                }
                #endregion

                #region 维修用料数据
                //维修用料数据   
                decimal dcMmoney = 0;
                DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}'", strRepairId), "", "");
                if (dmt.Rows.Count > 0)
                {

                    if (dmt.Rows.Count > dgvMaterials.Rows.Count)
                    {
                        dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 1);
                    }
                    for (int i = 0; i < dmt.Rows.Count; i++)
                    {
                        DataRow dmr = dmt.Rows[i];
                        dgvMaterials.Rows[i].Cells["material_id"].Value = CommonCtrl.IsNullToString(dmr["material_id"]);
                        dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                        dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                        dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dmr["norms"]);
                        dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                        dgvMaterials.Rows[i].Cells["quantity"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                        dgvMaterials.Rows[i].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
                        dgvMaterials.Rows[i].Cells["Mmember_discount"].Value = CommonCtrl.IsNullToString(dmr["member_discount"]);
                        dgvMaterials.Rows[i].Cells["Mmember_price"].Value = CommonCtrl.IsNullToString(dmr["member_price"]);
                        dgvMaterials.Rows[i].Cells["sum_money"].Value = CommonCtrl.IsNullToString(dmr["sum_money"]);
                        dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                        dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                        dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                        dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";

                    }                   
                }
                #endregion

                #region 其他项目收费数据
                //其他项目收费数据
                decimal doMmoney = 0;
                DataTable dot = DBHelper.GetTable("其他项目收费数据", "tb_maintain_other_toll", "*", string.Format(" maintain_id='{0}'", strRepairId), "", "");
                if (dot.Rows.Count > 0)
                {
                    if (dot.Rows.Count > dgvOther.Rows.Count)
                    {
                        dgvOther.Rows.Add(dot.Rows.Count - dgvOther.Rows.Count + 1);
                    }
                    for (int i = 0; i < dot.Rows.Count; i++)
                    {
                        DataRow dor = dot.Rows[i];
                        dgvOther.Rows[i].Cells["toll_id"].Value = CommonCtrl.IsNullToString(dor["toll_id"]);
                        dgvOther.Rows[i].Cells["Osum_money"].Value = CommonCtrl.IsNullToString(dor["sum_money"]);
                        dgvOther.Rows[i].Cells["Oremarks"].Value = CommonCtrl.IsNullToString(dor["remarks"]);
                        dgvOther.Rows[i].Cells["cost_types"].Value = GetDicName(CommonCtrl.IsNullToString(dor["cost_types"]));
                    }                                   
                }
                
                #endregion

                #region 附件信息数据
                //附件信息数据
                ucAttr.TableName = "tb_maintain_info";
                ucAttr.TableNameKeyValue = strRepairId;
                ucAttr.BindAttachment();
                #endregion
                #endregion
            }
        }
        #endregion

        #region 根据码表ID获取其对应的名称
        /// <summary>
        /// 根据码表ID获取其对应的名称
        /// </summary>
        /// <param name="strId">码表Id值</param>
        private string GetDicName(string strId)
        {
            return DBHelper.GetSingleValue("获取码表值", "sys_dictionaries", "dic_name", "dic_id='" + strId + "'", "");
        }
        #endregion

        #region 设置底部DataGridView的Anchor属性
        /// <summary>
        /// 设置底部DataGridView的Anchor属性
        /// </summary>
        private void SetDgvAnchor()
        {
            dgvMaterials.Dock = DockStyle.Fill;
            //dgvMaterials.Columns["MCheck"].HeaderText = "选择";
            dgvOther.Dock = DockStyle.Fill;
            //dgvOther.Columns["OCheck"].HeaderText = "选择";
            dgvproject.Dock = DockStyle.Fill;
            //dgvproject.Columns["colCheck"].HeaderText = "选择";
        }
        #endregion

        #region 顶部按钮显示设置
        /// <summary>
        /// 顶部按钮显示设置
        /// </summary>
        private void TopButtonSeting()
        {
            base.btnAdd.Visible = false;
            base.btnCopy.Visible = false;
            base.btnDelete.Visible = false;
            base.btnEdit.Visible = false;
            base.btnImport.Visible = false;
            base.btnSave.Visible = false;
            base.btnSubmit.Visible = false;
            base.btnVerify.Visible = false;
            base.btnStatus.Visible = false;
            base.btnCancel.Visible = false;
        }
        #endregion

        #region 获取部门名称
        private string GetDepartmentName(string strDId)
        {
            return DBHelper.GetSingleValue("获得部门名称", "tb_organization", "org_name", "org_id='" + strDId + "'", "");
        }
        #endregion

        #region 获得人员名称
        private string GetUserSetName(string strPid)
        {
            return DBHelper.GetSingleValue("获得人员名称", "sys_user", "user_name", "user_id='" + strPid + "'", "");
        }
        #endregion
    }
}
