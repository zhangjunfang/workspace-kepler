using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CloudPlatSocket.Protocol;
using System.Data;
using System.Collections;
using HXC_FuncUtility;
using SYSModel;
using Utility.Log;
using System.Threading;
using CloudPlatSocket.Model;

namespace CloudPlatSocket.Handler
{
    /// <summary>
    /// 数据上传类
    /// 创建日期：2014.11.11
    /// 修改日期：2014.11.12
    /// </summary>
    public class UploadDataHandler
    {
        #region --成员变量
        /// <summary> 【表名-子消息ID】散列表
        /// </summary>
        public static Hashtable htTable = new Hashtable();
        #endregion

        #region --构造函数
        public UploadDataHandler()
        {
            InitHashtable();
        }
        #endregion

        #region --初始化散列表
        /// <summary>
        /// 初始化散列表
        /// </summary>
        private static void InitHashtable()
        {
            if (htTable.Count > 0)
            {
                return;
            }

            //维修单 -- 蒋灿
            htTable.Add("tb_maintain_info", new ProtocolValue { MessageId = "U1_1", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //领料退货单信息表  -- 蒋灿
            htTable.Add("tb_maintain_refund_material", new ProtocolValue { MessageId = "U1_10", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //宇通旧件返厂单表 -- 蒋灿
            htTable.Add("tb_maintain_oldpart_recycle", new ProtocolValue { MessageId = "U1_11", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //宇通旧件返厂单明细表 -- 蒋灿
            htTable.Add("tb_maintain_oldpart_recycle_material_detail", new ProtocolValue { MessageId = "U1_12", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //旧件库存表 -- 蒋灿
            htTable.Add("tb_maintain_oldpart_inventory", new ProtocolValue { MessageId = "U1_13", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //旧件收/发明细表 -- 蒋灿
            htTable.Add("tb_maintain_oldpart_material_detail", new ProtocolValue { MessageId = "U1_14", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //旧件收/发货单 -- 蒋灿
            htTable.Add("tb_maintain_oldpart_receiv_send", new ProtocolValue { MessageId = "U1_15", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //三包结算单 -- 郭保强
            htTable.Add("tb_maintain_three_guaranty_settlement", new ProtocolValue { MessageId = "U1_16", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //其他费用结算单 -- 郭保强
            htTable.Add("tb_maintain_three_guaranty_settlement_oth", new ProtocolValue { MessageId = "U1_17", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //宇通三包服务单 -- 郭保强
            htTable.Add("tb_maintain_three_guaranty", new ProtocolValue { MessageId = "U1_19", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //预约维修用料表（预约单，维修单） -- 蒋灿
            htTable.Add("tb_maintain_material_detail", new ProtocolValue { MessageId = "U1_2", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //宇通三包维修用料表 -- 郭保强
            htTable.Add("tb_maintain_three_guaranty_material_detail", new ProtocolValue { MessageId = "U1_20", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //宇通三包维修项目表 -- 郭保强
            htTable.Add("tb_maintain_three_guaranty_item", new ProtocolValue { MessageId = "U1_21", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //旧件结算单 -- 郭保强
            htTable.Add("tb_maintain_three_guaranty_settlement_old", new ProtocolValue { MessageId = "U1_23", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //浏览结算单发票 -- 郭保强
            htTable.Add("tb_maintain_three_guaranty_settlement_inv", new ProtocolValue { MessageId = "U1_24", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //维修服务单 -- 郭保强
            htTable.Add("tb_maintain_three_guaranty_settlement_ser", new ProtocolValue { MessageId = "U1_25", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //领料，明细表 -- 蒋灿
            htTable.Add("tb_maintain_fetch_material_detai", new ProtocolValue { MessageId = "U1_27", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //派工人员表 -- 蒋灿
            htTable.Add("tb_maintain_dispatch_worker", new ProtocolValue { MessageId = "U1_28", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //其它收费项目表 -- 蒋灿
            htTable.Add("tb_maintain_other_toll", new ProtocolValue { MessageId = "U1_29", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //维修结算信息表 -- 蒋灿
            htTable.Add("tb_maintain_settlement_info", new ProtocolValue { MessageId = "U1_3", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //救援单信息表 -- 蒋灿
            htTable.Add("tb_maintain_rescue_info", new ProtocolValue { MessageId = "U1_30", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //援人员表 -- 蒋灿
            htTable.Add("tb_maintain_rescue_worker", new ProtocolValue { MessageId = "U1_31", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //宇通三包服务单审批状态详情 -- 郭保强
            htTable.Add("tb_maintain_three_material_approve", new ProtocolValue { MessageId = "U1_32", Operate = OperateType.A, PreTableName = "", Key = "", PreKey = "" });
            //操作记录信息表 -- 孙亚楠
            htTable.Add("tb_operating_record", new ProtocolValue { MessageId = "U1_33", Operate = OperateType.A, PreTableName = "", Key = "", PreKey = "" });
            //维修返修单 -- 蒋灿
            htTable.Add("tb_maintain_back_repair", new ProtocolValue { MessageId = "U1_4", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //维修项目 -- 蒋灿
            htTable.Add("tb_maintain_item", new ProtocolValue { MessageId = "U1_5", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //维修预约单 -- 蒋灿
            htTable.Add("tb_maintain_reservation", new ProtocolValue { MessageId = "U1_6", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //领料退货单明细表 -- 蒋灿
            htTable.Add("tb_maintain_refund_material_detai", new ProtocolValue { MessageId = "U1_8", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //领料单信息表 -- 蒋灿
            htTable.Add("tb_maintain_fetch_material", new ProtocolValue { MessageId = "U1_9", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //组织信息 -- 杨超逸
            htTable.Add("tb_organization", new ProtocolValue { MessageId = "U2_1", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //人员信息 -- 杨超逸
            htTable.Add("sys_user", new ProtocolValue { MessageId = "U2_2", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //角色信息 -- 杨超逸
            htTable.Add("sys_role", new ProtocolValue { MessageId = "U2_3", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //功能菜单 -- 孙亚楠
            htTable.Add("sys_function", new ProtocolValue { MessageId = "U2_4", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //人员与角色关系 -- 杨超逸
            htTable.Add("tr_user_role", new ProtocolValue { MessageId = "U2_5", Operate = OperateType.AD, PreTableName = "", Key = "", PreKey = "" });
            //人员与常用功能关系 -- 孙亚楠
            htTable.Add("tr_user_function", new ProtocolValue { MessageId = "U2_6", Operate = OperateType.AD, PreTableName = "", Key = "", PreKey = "" });
            //角色与系统功能关系 -- 杨超逸
            htTable.Add("tr_role_function", new ProtocolValue { MessageId = "U2_7", Operate = OperateType.AD, PreTableName = "", Key = "", PreKey = "" });
            //采购计划单 -- 唐春奎
            htTable.Add("tb_parts_purchase_plan", new ProtocolValue { MessageId = "U3_1", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //销售计划单 -- 唐春奎
            htTable.Add("tb_parts_sale_plan", new ProtocolValue { MessageId = "U3_11", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //销售计划单-配件 -- 唐春奎
            htTable.Add("tb_parts_sale_plan_p", new ProtocolValue { MessageId = "U3_12", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //销售订单 -- 唐春奎
            htTable.Add("tb_parts_sale_order", new ProtocolValue { MessageId = "U3_13", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //销售订单-配件 -- 唐春奎
            htTable.Add("tb_parts_sale_order_p", new ProtocolValue { MessageId = "U3_14", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //销售开单 -- 唐春奎
            htTable.Add("tb_parts_sale_billing", new ProtocolValue { MessageId = "U3_15", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //销售开单-配件 -- 唐春奎
            htTable.Add("tb_parts_sale_billing_p", new ProtocolValue { MessageId = "U3_16", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //出入库单 -- 赵学营
            htTable.Add("tb_parts_stock_inout", new ProtocolValue { MessageId = "U3_17", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //出入库单-配件 -- 赵学营
            htTable.Add("tb_parts_stock_inout_p", new ProtocolValue { MessageId = "U3_18", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //调拨单 -- 赵学营
            htTable.Add("tb_parts_stock_allot", new ProtocolValue { MessageId = "U3_19", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //采购计划单-配件 -- 唐春奎
            htTable.Add("tb_parts_purchase_plan_p", new ProtocolValue { MessageId = "U3_2", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //调拨单-配件 -- 赵学营
            htTable.Add("tb_parts_stock_allot_p", new ProtocolValue { MessageId = "U3_20", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //报损单 -- 赵学营
            htTable.Add("tb_parts_stock_loss", new ProtocolValue { MessageId = "U3_21", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //报损单-配件 -- 赵学营
            htTable.Add("tb_parts_stock_loss_p", new ProtocolValue { MessageId = "U3_22", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //盘点单 -- 赵学营
            htTable.Add("tb_parts_stock_check", new ProtocolValue { MessageId = "U3_23", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //盘点单-配件 -- 赵学营
            htTable.Add("tb_parts_stock_check_p", new ProtocolValue { MessageId = "U3_24", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //调价单 -- 赵学营
            htTable.Add("tb_parts_stock_modifyprice", new ProtocolValue { MessageId = "U3_25", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            // 调价单-配件 -- 赵学营
            htTable.Add("tb_parts_stock_modifyprice_p", new ProtocolValue { MessageId = "U3_26", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //其他发货单 -- 赵学营
            htTable.Add("tb_parts_stock_shipping", new ProtocolValue { MessageId = "U3_27", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //其他发货单-配件 -- 赵学营
            htTable.Add("tb_parts_stock_shipping_p", new ProtocolValue { MessageId = "U3_28", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //其他收货单 -- 赵学营
            htTable.Add("tb_parts_stock_receipt", new ProtocolValue { MessageId = "U3_29", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //采购订单 -- 唐春奎
            htTable.Add("tb_parts_purchase_order", new ProtocolValue { MessageId = "U3_3", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //其他收货单-配件 -- 赵学营
            htTable.Add("tb_parts_stock_receipt_p", new ProtocolValue { MessageId = "U3_30", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            // -- 赵学营
            htTable.Add("tb_parts_stock_p", new ProtocolValue { MessageId = "U3_31", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //配送单 -- 陈信仲
            htTable.Add("tb_distribution", new ProtocolValue { MessageId = "U3_32", Operate = OperateType.A, PreTableName = "", Key = "", PreKey = "" });
            //配送单-配件 -- 陈信仲
            htTable.Add("tb_distribution_parts", new ProtocolValue { MessageId = "U3_33", Operate = OperateType.A, PreTableName = "", Key = "", PreKey = "" });
            //采购订单-配件 -- 唐春奎
            htTable.Add("tb_parts_purchase_order_p", new ProtocolValue { MessageId = "U3_4", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //宇通采购订单 -- 唐春奎
            htTable.Add("tb_parts_purchase_order_2", new ProtocolValue { MessageId = "U3_5", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //宇通采购订单-配件 -- 唐春奎
            htTable.Add("tb_parts_purchase_order_p_2", new ProtocolValue { MessageId = "U3_6", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //采购开单 -- 唐春奎
            htTable.Add("tb_parts_purchase_billing", new ProtocolValue { MessageId = "U3_7", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //采购开单-配件 -- 唐春奎
            htTable.Add("tb_parts_purchase_billing_p", new ProtocolValue { MessageId = "U3_8", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            // -- 赵学营
            htTable.Add("tb_payment_detail", new ProtocolValue { MessageId = "U4_1", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //应收应付 -- 陈信仲
            htTable.Add("tb_bill_receivable", new ProtocolValue { MessageId = "U4_2", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //往来核销 -- 陈信仲
            htTable.Add("tb_account_verification", new ProtocolValue { MessageId = "U4_3", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //往来帐核销-业务单据 -- 陈信仲
            htTable.Add("tb_verificationn_documents", new ProtocolValue { MessageId = "U4_4", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //应收应付单据 -- 陈信仲
            htTable.Add("tb_balance_documents", new ProtocolValue { MessageId = "U4_5", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //会员信息 -- 郭保强
            htTable.Add("tb_CustomerSer_member", new ProtocolValue { MessageId = "U5_1", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //会员参数设置信息表 -- 杨超逸
            htTable.Add("tb_CustomerSer_member_setInfo", new ProtocolValue { MessageId = "U5_2", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            // 会员参数设置特殊维修项目折扣表 -- 杨超逸
            htTable.Add("tb_CustomerSer_member_setInfo_projrct", new ProtocolValue { MessageId = "U5_3", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //会员参数设置特殊配件折扣表 -- 杨超逸
            htTable.Add("tb_CustomerSer_member_setInfo_parts", new ProtocolValue { MessageId = "U5_4", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //客户回访记录表 -- 郭保强
            htTable.Add("tb_CustomerSer_Callback", new ProtocolValue { MessageId = "U5_5", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //信息反馈记录表 -- 郭保强
            htTable.Add("tb_CustomerSer_Feedback", new ProtocolValue { MessageId = "U5_6", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //信息反馈记录处理记录 -- 郭保强
            htTable.Add("tb_CustomerSer_Feedback_dispose", new ProtocolValue { MessageId = "U5_7", Operate = OperateType.A, PreTableName = "", Key = "", PreKey = "" });
            //信息反馈记录审批记录 -- 郭保强
            htTable.Add("tb_CustomerSer_Feedback_approve", new ProtocolValue { MessageId = "U5_8", Operate = OperateType.A, PreTableName = "", Key = "", PreKey = "" });
            //仓库档案 -- 杨超逸
            htTable.Add("tb_warehouse", new ProtocolValue { MessageId = "U6_1", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //配件档案 -- 杨超逸
            htTable.Add("tb_parts", new ProtocolValue { MessageId = "U6_10", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //配件价格信息 -- 杨超逸
            htTable.Add("tb_parts_price", new ProtocolValue { MessageId = "U6_11", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //替代配件 -- 杨超逸
            htTable.Add("tb_parts_replace", new ProtocolValue { MessageId = "U6_12", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //适用车型 -- 杨超逸
            htTable.Add("tb_parts_for_vehicle", new ProtocolValue { MessageId = "U6_13", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //字典码表 -- 杨超逸
            htTable.Add("sys_dictionaries", new ProtocolValue { MessageId = "U6_15", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //联系人 -- 杨超逸
            htTable.Add("tb_contacts", new ProtocolValue { MessageId = "U6_16", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            // 司机与车辆关系 -- 蒋灿
            htTable.Add("tr_driver_vehicle", new ProtocolValue { MessageId = "U6_18", Operate = OperateType.AD, PreTableName = "", Key = "", PreKey = "" });
            //签约信息 -- 孙亚楠
            htTable.Add("tb_signing_info", new ProtocolValue { MessageId = "U6_19", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //仓库货位档案 -- 杨超逸
            htTable.Add("tb_cargo_space", new ProtocolValue { MessageId = "U6_2", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            // 产品改进号 -- 陈信仲
            htTable.Add("tb_product_no", new ProtocolValue { MessageId = "U6_21", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //产品改进号-车辆 -- 陈信仲
            htTable.Add("tb_product_no_vehicle", new ProtocolValue { MessageId = "U6_22", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //产品改进号-配件 -- 陈信仲
            htTable.Add("tb_product_no_part", new ProtocolValue { MessageId = "U6_23", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //故障分类 -- 陈信仲
            htTable.Add("tb_fault_class", new ProtocolValue { MessageId = "U6_24", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //故障总成 -- 陈信仲
            htTable.Add("tb_fault_assembly", new ProtocolValue { MessageId = "U6_25", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //故障总成部件 -- 陈信仲
            htTable.Add("tb_fault_component", new ProtocolValue { MessageId = "U6_26", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //故障模式 -- 陈信仲
            htTable.Add("tb_fault_model", new ProtocolValue { MessageId = "U6_27", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //部件和故障模式关系 -- 陈信仲
            htTable.Add("tr_component_model", new ProtocolValue { MessageId = "U6_28", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //主数据和联系人关联表 -- 研发部
            htTable.Add("tr_base_contacts", new ProtocolValue { MessageId = "U6_29", Operate = OperateType.AD, PreTableName = "", Key = "", PreKey = "" });
            //供应商档案 -- 杨超逸
            htTable.Add("tb_supplier", new ProtocolValue { MessageId = "U6_3", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //全局配置表 -- 孙亚楠
            htTable.Add("sys_config", new ProtocolValue { MessageId = "U6_30", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //表单关系表 -- 唐春奎
            htTable.Add("tr_order_relation", new ProtocolValue { MessageId = "U6_31", Operate = OperateType.AD, PreTableName = "", Key = "", PreKey = "" });
            //银行账户 -- 杨超逸
            htTable.Add("tb_bank_account", new ProtocolValue { MessageId = "U6_33", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //结算方式 -- 杨超逸
            htTable.Add("tb_balance_way", new ProtocolValue { MessageId = "U6_34", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //打印设置 -- 陈信仲
            htTable.Add("tb_print_style", new ProtocolValue { MessageId = "U6_35", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //报表设置 -- 陈信仲
            htTable.Add("tb_report_set", new ProtocolValue { MessageId = "U6_36", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //公司档案 -- 杨超逸
            htTable.Add("tb_company", new ProtocolValue { MessageId = "U6_4", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //司机档案 -- 陈信仲
            htTable.Add("tb_driver", new ProtocolValue { MessageId = "U6_5", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //客户档案 -- 郭保强
            htTable.Add("tb_customer", new ProtocolValue { MessageId = "U6_6", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //工时档案 -- 杨超逸
            htTable.Add("tb_workhours", new ProtocolValue { MessageId = "U6_7", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //车型档案 -- 杨超逸
            htTable.Add("tb_vehicle_models", new ProtocolValue { MessageId = "U6_8", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //车辆档案 -- 蒋灿
            htTable.Add("tb_vehicle", new ProtocolValue { MessageId = "U6_9", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //单据编码生成规则 -- 杨超逸
            htTable.Add("sys_bill_code_rule", new ProtocolValue { MessageId = "U7_1", Operate = OperateType.U, PreTableName = "", Key = "", PreKey = "" });
            //财务业务参数 -- 孙亚楠
            htTable.Add("sys_financial_ser_param", new ProtocolValue { MessageId = "U7_10", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //业务设置-维修套餐设置 -- 杨超逸
            htTable.Add("sys_b_set_repair_package_set", new ProtocolValue { MessageId = "U7_12", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //业务设置-维修套餐设置-项目 -- 蒋灿
            htTable.Add("sys_b_set_repair_package_set_project", new ProtocolValue { MessageId = "U7_14", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //业务设置-维修套餐设置-维修用料 -- 杨超逸
            htTable.Add("sys_b_set_repair_package_set_materials", new ProtocolValue { MessageId = "U7_15", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //业务设置-维修套餐设置-其他收费项目 -- 杨超逸
            htTable.Add("sys_b_set_repair_package_set_other", new ProtocolValue { MessageId = "U7_16", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //公告 -- 杨超逸
            htTable.Add("sys_announcement", new ProtocolValue { MessageId = "U7_18", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //公告-接收组织 -- 杨超逸
            htTable.Add("sys_announcement_org", new ProtocolValue { MessageId = "U7_19", Operate = OperateType.AD, PreTableName = "", Key = "", PreKey = "" });
            //公告-接收人员 -- 杨超逸
            htTable.Add("sys_announcement_user", new ProtocolValue { MessageId = "U7_20", Operate = OperateType.AD, PreTableName = "", Key = "", PreKey = "" });
            // 提醒设置 -- 杨超逸
            htTable.Add("sys_reminding_set", new ProtocolValue { MessageId = "U7_21", Operate = OperateType.U, PreTableName = "", Key = "", PreKey = "" });
            //告警设置 -- 杨超逸
            htTable.Add("sys_alarm_set", new ProtocolValue { MessageId = "U7_22", Operate = OperateType.U, PreTableName = "", Key = "", PreKey = "" });
            //数据同步 -- 孙亚楠
            htTable.Add("sys_data_sync", new ProtocolValue { MessageId = "U7_27", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //数据同步日志 -- 孙亚楠
            htTable.Add("sys_data_sync_log", new ProtocolValue { MessageId = "U7_28", Operate = OperateType.A, PreTableName = "", Key = "", PreKey = "" });
            //登录时间限制 -- 孙亚楠
            htTable.Add("sys_login_time_limit", new ProtocolValue { MessageId = "U7_3", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //登录日志 -- 孙亚楠
            htTable.Add("sys_log_log", new ProtocolValue { MessageId = "U7_30", Operate = OperateType.A, PreTableName = "", Key = "", PreKey = "" });
            //自动备份设置 -- 孙亚楠
            htTable.Add("sys_auto_backup_set", new ProtocolValue { MessageId = "U7_32", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //登录电脑设置 -- 孙亚楠
            htTable.Add("sys_login_pc_set", new ProtocolValue { MessageId = "U7_4", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            // -- 孙亚楠
            htTable.Add("sys_setbook", new ProtocolValue { MessageId = "U7_5", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            // 维修业务参数 -- 孙亚楠
            htTable.Add("sys_repair_param", new ProtocolValue { MessageId = "U7_6", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //采购业务参数 -- 孙亚楠
            htTable.Add("sys_purchase_param", new ProtocolValue { MessageId = "U7_7", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //销售业务参数 -- 孙亚楠
            htTable.Add("sys_sale_param", new ProtocolValue { MessageId = "U7_8", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
            //库存业务参数 -- 孙亚楠
            htTable.Add("sys_stock_param", new ProtocolValue { MessageId = "U7_9", Operate = OperateType.AU, PreTableName = "", Key = "", PreKey = "" });
        }
        #endregion

        #region --私有方法
        /// <summary> 获取上传协议 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dbName">帐套</param>
        /// <returns></returns>
        private static List<UploadDataProtocol> GetProtocol(DataTable dt, string dbName)
        {
            List<UploadDataProtocol> udps = new List<UploadDataProtocol>();
            if (dt != null)
            {
                UploadDataProtocol udp = null;
                foreach (DataRow dr in dt.Rows)
                {
                    udp = GetProtocol(dr, dbName);
                    if (udp != null)
                    {
                        udps.Add(udp);
                    }
                }
            }
            return udps;
        }
        /// <summary> 获取上传协议
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="dbName">帐套</param>
        /// <returns></returns>
        private static UploadDataProtocol GetProtocol(DataRow dr, string dbName)
        {
            return GetProtocol(dr, dr.Table.TableName, dbName);
        }
        private static UploadDataProtocol GetProtocol(DataRow dr,String tbName, string dbName)
        {
            if (!htTable.ContainsKey(tbName))
            {
                return null;
            }
            UploadDataProtocol udp = new UploadDataProtocol();
            udp.StationId = GlobalStaticObj_Server.Instance.StationID;
            //根据表名得到子消息ID
            if (htTable[tbName] is ProtocolValue)
            {
                udp.SubMessageId = (htTable[tbName] as ProtocolValue).MessageId;
            }
            else
            {
                udp.SubMessageId = htTable[tbName].ToString();
            }
            udp.TimeSpan = TimeHelper.GetTimeInMillis();
            //Json对象
            string json = JsonHelper.DataTableToJson(dr, udp.StationId, dbName, true);
            json = BaseCodeHelper.EnCode(json);
            udp.Json = json;
            return udp;
        }

        /// <summary> 操作数据
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="operation">操作类别</param>
        /// <param name="tbName">数据表名称</param>
        /// <param name="dbName">帐套</param>
        private static void Operation(DataTable dt, string operation,String tbName, string dbName)
        {
            if (dt == null)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                var protocol = GetProtocol(dr,tbName, dbName);
                if (protocol == null) continue;
                var sb = new StringBuilder();
                foreach (var o in dr.ItemArray)
                {
                    sb.Append(o);
                    sb.Append("$");
                }
                LogAssistant.LogService.WriteLog(dbName + "--" + tbName, sb.ToString());
                protocol.Operation = operation;
                ServiceAgent.AddSendQueue(protocol);
            }
        }
        private static void Operation(DataTable dt, string operation, string dbName)
        {
            if (dt == null)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                var protocol = GetProtocol(dr, dbName);
                if (protocol == null) continue;
                var sb = new StringBuilder();
                foreach (var o in dr.ItemArray)
                {
                    sb.Append(o);
                    sb.Append("$");
                }
                LogAssistant.LogService.WriteLog(dbName + "\t" + dt.TableName, sb.ToString());
                protocol.Operation = operation;
                ServiceAgent.AddSendQueue(protocol);
            }
        }
        #endregion

        #region --公用方法
        /// <summary> 上传数据 
        /// </summary>
        /// <param name="dbName">帐套信息</param>
        /// <param name="time">下次上传数据时间</param>
        public static void UpLoadData(string dbName,string time)
        {
            InitHashtable();

            foreach (DictionaryEntry de in htTable)
            {
                var tableName = de.Key.ToString();
                if (!DataHelper.TableIsExists(dbName, tableName)) continue;
                ProtocolValue pv = null;
                if (de.Value is ProtocolValue)
                {
                    pv = de.Value as ProtocolValue;
                }
                if (pv == null) return;
                var lastTime = TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime);
                var currTime = TimeHelper.GetTimeInMillis(time);
                if (tableName == "sys_announcement")
                {
                    // 区分数据来源                    

                    #region --添加数据

                    Operation(
                        DataHelper.GetAddDataFromHXC(tableName, pv, dbName,
                            TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                            TimeHelper.GetTimeInMillis(time)),
                        DataSources.EnumOperationType.Add.ToString("d"), dbName);

                    #endregion

                    Thread.Sleep(100);

                    #region --修改数据

                    Operation(
                        DataHelper.GetUpdateDataFormHXC(tableName, pv, dbName,
                            TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                            TimeHelper.GetTimeInMillis(time)),
                        DataSources.EnumOperationType.Update.ToString("d"), dbName);

                    #endregion

                    Thread.Sleep(100);

                    #region --删除数据

                    Operation(
                        DataHelper.GetUpdateDataFormHXC(tableName, pv, dbName,
                            TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                            TimeHelper.GetTimeInMillis(time)),
                        DataSources.EnumOperationType.Delete.ToString("d"), dbName);

                    #endregion
                }
                else
                {
                    switch (pv.Operate)
                    {
                        case OperateType.A:
                            Operation(DataHelper.GetAddData(tableName, dbName, lastTime, currTime),
                                DataSources.EnumOperationType.Add.ToString("d"), dbName);
                            break;
                        case OperateType.D:
                            Operation(DataHelper.GetDeleteData(tableName, dbName, lastTime, currTime),
                                DataSources.EnumOperationType.Delete.ToString("d"), dbName);
                            break;
                        case OperateType.AU:
                            Operation(DataHelper.GetAddData(tableName, dbName, lastTime, currTime),
                                DataSources.EnumOperationType.Add.ToString("d"), dbName);
                            Operation(DataHelper.GetUpdateData(tableName, dbName, lastTime, currTime),
                                DataSources.EnumOperationType.Update.ToString("d"), dbName);
                            break;
                        case OperateType.AD:
                            Operation(DataHelper.GetAddData(tableName, dbName, lastTime, currTime),
                                DataSources.EnumOperationType.Add.ToString("d"), dbName);
                            Operation(DataHelper.GetDeleteData(tableName, dbName, lastTime, currTime),
                                DataSources.EnumOperationType.Delete.ToString("d"), dbName);
                            break;
                        case OperateType.PK:

                            break;
                        case OperateType.AUD:
                            Operation(DataHelper.GetAddData(tableName, dbName, lastTime, currTime),
                                DataSources.EnumOperationType.Add.ToString("d"), dbName);
                            Operation(DataHelper.GetUpdateData(tableName, dbName, lastTime, currTime),
                                DataSources.EnumOperationType.Update.ToString("d"), dbName);
                            Operation(DataHelper.GetDeleteData(tableName, dbName, lastTime, currTime),
                                DataSources.EnumOperationType.Delete.ToString("d"), dbName);
                            break;
                    }
                }
                Thread.Sleep(1000);
            }
        }
        /// <summary> 写入错误日志 
        /// </summary>        
        /// <param name="protocol">上传数据协议</param>
        public static void WriteErrorLog(UploadDataProtocol protocol)
        {
            string msg = string.Empty;
            if (htTable.ContainsKey(protocol.SubMessageId))
            {
                string tableName = string.Empty;
                //表名：
                foreach (DictionaryEntry de in htTable)
                {
                    if (de.Value.ToString() == protocol.SubMessageId)
                    {
                        tableName = de.Key.ToString();
                        break;
                    }
                }
                msg += "表名：" + tableName + "\r\n";
            }
            msg += "标识：" + protocol.StationId + protocol.SerialNumber + protocol.TimeSpan + "\r\n";
            msg += "时间：" + TimeHelper.MillisToTime(protocol.TimeSpan) + "\r\n";
            msg += "服务站ID：" + protocol.StationId + "\r\n";
            msg += "内容：" + ProtocolTranslator.SerilizeMessage(protocol);
            //写错误日志
            Log.writeLog(msg);
        }
        #endregion
    }
}