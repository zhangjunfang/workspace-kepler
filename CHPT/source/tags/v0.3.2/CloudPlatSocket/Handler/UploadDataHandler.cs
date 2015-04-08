using System.Collections.Generic;
using CloudPlatSocket.Protocol;
using System.Data;
using System.Collections;
using HXC_FuncUtility;
using SYSModel;
using Utility.Log;
using System.Threading;

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

            #region --维修管理
            //维修单表
            htTable.Add("tb_maintain_info",
                new ProtocolValue() { MessageId = "U1_1", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //维修用料表-----------------
            htTable.Add("tb_maintain_material_detail",
                new ProtocolValue() { MessageId = "U1_2", Operate = OperateType.AUD, PreTableName = "tb_maintain_info", Key = "maintain_id", PreKey = "maintain_id" });
            //维修结算信息表
            htTable.Add("tb_maintain_settlement_info",
                new ProtocolValue() { MessageId = "U1_3", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //维修返修单表
            htTable.Add("tb_maintain_back_repair",
                new ProtocolValue() { MessageId = "U1_4", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //维修项目表------------------???
            htTable.Add("tb_maintain_item",
                new ProtocolValue() { MessageId = "U1_5", Operate = OperateType.AUD, PreTableName = "tb_maintain_info", Key = "maintain_id", PreKey = "maintain_id" });
            //维修预约单表
            htTable.Add("tb_maintain_reservation", "U1_6");
           

            //领料，退料明细表------------
            htTable.Add("tb_maintain_refund_material_detai",
                new ProtocolValue() { MessageId = "U1_8", Operate = OperateType.AUD, PreTableName = "tb_maintain_refund_material", Key = "refund_id", PreKey = "refund_id" });
            //领料单信息表
            htTable.Add("tb_maintain_fetch_material",
                new ProtocolValue() { MessageId = "U1_9", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //领料退货单信息表
            htTable.Add("tb_maintain_refund_material",
                new ProtocolValue() { MessageId = "U1_10", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            #endregion

            #region --旧件管理
            //宇通旧件返厂单表
            htTable.Add("tb_maintain_oldpart_recycle",
                new ProtocolValue() { MessageId = "U1_11", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //宇通旧件返厂单明细表----------------------
            htTable.Add("tb_maintain_oldpart_recycle_material_detail",
                new ProtocolValue() { MessageId = "U1_12", Operate = OperateType.AUD, PreTableName = "tb_maintain_oldpart_recycle", Key = "maintain_id", PreKey = "return_id" });
            //旧件库存表
            htTable.Add("tb_maintain_oldpart_inventory",
                new ProtocolValue() { MessageId = "U1_13", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //旧件收/发明细表-------------------------
            htTable.Add("tb_maintain_oldpart_material_detail",
                new ProtocolValue() { MessageId = "U1_14", Operate = OperateType.AUD, PreTableName = "tb_maintain_oldpart_inventory", Key = "maintain_id", PreKey = "inventory_id" });
            //旧件收/发货单
            htTable.Add("tb_maintain_oldpart_receiv_send",
                new ProtocolValue() { MessageId = "U1_15", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            #endregion

            #region --三包结算
            //三包结算单
            htTable.Add("tb_maintain_three_guaranty_settlement",
                new ProtocolValue() { MessageId = "U1_16", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //其他费用结算单---------------------------------st_id
            htTable.Add("tb_maintain_three_guaranty_settlement_oth",
                new ProtocolValue() { MessageId = "U1_17", Operate = OperateType.AUD, PreTableName = "tb_maintain_three_guaranty_settlement", Key = "st_id", PreKey = "st_id" });
            //宇通三包其它收费项目表-------------------------tg_id
            htTable.Add("tb_maintain_three_guaranty_toll",
                new ProtocolValue() { MessageId = "U1_18", Operate = OperateType.AUD, PreTableName = "tb_maintain_three_guaranty", Key = "tg_id", PreKey = "tg_id" });
            //宇通三包服务单表
            htTable.Add("tb_maintain_three_guaranty",
                new ProtocolValue() { MessageId = "U1_19", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //宇通三包维修用料表----------------------------tg_id
            htTable.Add("tb_maintain_three_guaranty_material_detail",
                new ProtocolValue() { MessageId = "U1_20", Operate = OperateType.AUD, PreTableName = "tb_maintain_three_guaranty", Key = "tg_id", PreKey = "tg_id" });
            //宇通三包维修项目表----------------------------tg_id
            htTable.Add("tb_maintain_three_guaranty_item",
                new ProtocolValue() { MessageId = "U1_21", Operate = OperateType.AUD, PreTableName = "tb_maintain_three_guaranty", Key = "tg_id", PreKey = "tg_id" });
            ////宇通三包附件信息----------------------------tg_id
            //htTable.Add("tb_maintain_three_guaranty_accessory",
            //    new ProtocolValue() { MessageId = "U1_22", PreTableName = "tb_maintain_three_guaranty", Key = "tg_id", PreKey = "tg_id" });
            //旧件结算单----------------------------------------st_id
            htTable.Add("tb_maintain_three_guaranty_settlement_old",
                new ProtocolValue() { MessageId = "U1_23", Operate = OperateType.AUD, PreTableName = "tb_maintain_three_guaranty_settlement", Key = "st_id", PreKey = "st_id" });
            //浏览结算单发票----------------------------------st_id
            htTable.Add("tb_maintain_three_guaranty_settlement_inv",
                new ProtocolValue() { MessageId = "U1_24", Operate = OperateType.AUD, PreTableName = "tb_maintain_three_guaranty_settlement", Key = "st_id", PreKey = "st_id" });
            //维修服务单--------------------------------------st_id
            htTable.Add("tb_maintain_three_guaranty_settlement_ser",
                new ProtocolValue() { MessageId = "U1_25", Operate = OperateType.AUD, PreTableName = "tb_maintain_three_guaranty_settlement", Key = "st_id", PreKey = "st_id" });

            //领料退货单明细表------------
            htTable.Add("tb_maintain_material_detail_fetch_refund",
                new ProtocolValue() { MessageId = "U1_8", Operate = OperateType.AUD, PreTableName = "tb_maintain_info", Key = "maintain_id", PreKey = "maintain_id" });
            //领料单明细表----------------
            htTable.Add("tb_maintain_fetch_material_detai",
                new ProtocolValue() { MessageId = "U1_27", Operate = OperateType.AUD, PreTableName = "tb_maintain_fetch_material", Key = "fetch_id", PreKey = "fetch_id" });
            //派工人员表----------------
            htTable.Add("tb_maintain_dispatch_worker",
                new ProtocolValue() { MessageId = "U1_28", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });            
            #endregion

            #region --权限管理
            //组织信息表
            htTable.Add("tb_organization",
                new ProtocolValue() { MessageId = "U2_1", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //人员信息表
            htTable.Add("sys_user",
                new ProtocolValue() { MessageId = "U2_2", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //角色信息表
            htTable.Add("sys_role",
                new ProtocolValue() { MessageId = "U2_3", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //功能菜单表
            htTable.Add("sys_function",
                new ProtocolValue() { MessageId = "U2_4", Operate = OperateType.AUD, PreTableName = "", Key = "", PreKey = "" });
            //人员与角色关系表---------------???
            htTable.Add("tr_user_role",
                new ProtocolValue() { MessageId = "U2_5", Operate = OperateType.A, PreTableName = "", Key = "", PreKey = "" });
            //人员与常用功能关系表-----------???
            htTable.Add("tr_user_function",
                new ProtocolValue() { MessageId = "U2_6", Operate = OperateType.A, PreTableName = "", Key = "", PreKey = "" });
            //角色与系统功能--------------???
            htTable.Add("tr_role_function",
                new ProtocolValue() { MessageId = "U2_7", Operate = OperateType.A, PreTableName = "", Key = "", PreKey = "" });
            #endregion

            #region --采购管理
            //采购计划单
            htTable.Add("tb_parts_purchase_plan",
                new ProtocolValue() { MessageId = "U3_1", PreTableName = "", Key = "", PreKey = "" });
            //采购计划单-配件
            htTable.Add("tb_parts_purchase_plan_p",
                new ProtocolValue() { MessageId = "U3_2", PreTableName = "", Key = "", PreKey = "" });
            //采购订单
            htTable.Add("tb_parts_purchase_order",
                new ProtocolValue() { MessageId = "U3_3", PreTableName = "", Key = "", PreKey = "" });
            //采购订单-配件
            htTable.Add("tb_parts_purchase_order_p",
                new ProtocolValue() { MessageId = "U3_4", PreTableName = "", Key = "", PreKey = "" });
            //宇通采购订单
            htTable.Add("tb_parts_purchase_order_2",
                new ProtocolValue() { MessageId = "U3_5", PreTableName = "", Key = "", PreKey = "" });
            //宇通采购订单-配件
            htTable.Add("tb_parts_purchase_order_p_2",
                new ProtocolValue() { MessageId = "U3_6", PreTableName = "", Key = "", PreKey = "" });
            //采购开单
            htTable.Add("tb_parts_purchase_billing",
                new ProtocolValue() { MessageId = "U3_7", PreTableName = "", Key = "", PreKey = "" });
            //采购开单-配件
            htTable.Add("tb_parts_purchase_billing_p",
                new ProtocolValue() { MessageId = "U3_8", PreTableName = "", Key = "", PreKey = "" });
            //采购调价单
            htTable.Add("tb_parts_purchase_price",
                new ProtocolValue() { MessageId = "U3_9", PreTableName = "", Key = "", PreKey = "" });
            //采购调价单-配件
            htTable.Add("tb_parts_purchase_price_p",
                new ProtocolValue() { MessageId = "U3_10", PreTableName = "", Key = "", PreKey = "" });
            #endregion

            #region --销售管理
            //销售计划单
            htTable.Add("tb_parts_sale_plan",
                new ProtocolValue() { MessageId = "U3_11", PreTableName = "", Key = "", PreKey = "" });
            //销售计划单-配件表
            htTable.Add("tb_parts_sale_plan_p",
                new ProtocolValue() { MessageId = "U3_12", PreTableName = "", Key = "", PreKey = "" });
            //销售订单表*************
            htTable.Add("tb_parts_sale_order",
                new ProtocolValue() { MessageId = "U3_13", PreTableName = "", Key = "", PreKey = "" });
            //销售订单-配件表
            htTable.Add("tb_parts_sale_order_p",
                new ProtocolValue() { MessageId = "U3_14", PreTableName = "", Key = "", PreKey = "" });
            //销售开单表
            htTable.Add("tb_parts_sale_billing",
                new ProtocolValue() { MessageId = "U3_15", PreTableName = "", Key = "", PreKey = "" });
            //销售开单-配件表
            htTable.Add("tb_parts_sale_billing_p",
                new ProtocolValue() { MessageId = "U3_16", PreTableName = "", Key = "", PreKey = "" });
            #endregion

            #region --库存管理
            //出入库单表
            htTable.Add("tb_parts_stock_inout",
                new ProtocolValue() { MessageId = "U3_17", PreTableName = "", Key = "", PreKey = "" });
            //出入库单-配件表
            htTable.Add("tb_parts_stock_inout_p",
                new ProtocolValue() { MessageId = "U3_18", PreTableName = "", Key = "", PreKey = "" });
            //调拨单表
            htTable.Add("tb_parts_stock_allot",
                new ProtocolValue() { MessageId = "U3_19", PreTableName = "", Key = "", PreKey = "" });
            //调拨单-配件表
            htTable.Add("tb_parts_stock_allot_p",
                new ProtocolValue() { MessageId = "U3_20", PreTableName = "", Key = "", PreKey = "" });
            //报损单表
            htTable.Add("tb_parts_stock_loss",
                new ProtocolValue() { MessageId = "U3_21", PreTableName = "", Key = "", PreKey = "" });
            //报损单-配件表
            htTable.Add("tb_parts_stock_loss_p",
                new ProtocolValue() { MessageId = "U3_22", PreTableName = "", Key = "", PreKey = "" });
            //盘点单表
            htTable.Add("tb_parts_stock_check",
                new ProtocolValue() { MessageId = "U3_23", PreTableName = "", Key = "", PreKey = "" });
            //盘点单-配件表
            htTable.Add("tb_parts_stock_check_p",
                new ProtocolValue() { MessageId = "U3_24", PreTableName = "", Key = "", PreKey = "" });
            //调价单表
            htTable.Add("tb_parts_stock_modifyprice",
                new ProtocolValue() { MessageId = "U3_25", PreTableName = "", Key = "", PreKey = "" });
            //调价单-配件
            htTable.Add("tb_parts_stock_modifyprice_p",
                new ProtocolValue() { MessageId = "U3_26", PreTableName = "", Key = "", PreKey = "" });
            //其他发货单
            htTable.Add("tb_parts_stock_shipping",
                new ProtocolValue() { MessageId = "U3_27", PreTableName = "", Key = "", PreKey = "" });
            //其他发货单-配件
            htTable.Add("tb_parts_stock_shipping_p",
                new ProtocolValue() { MessageId = "U3_28", PreTableName = "", Key = "", PreKey = "" });
            //其他收货单
            htTable.Add("tb_parts_stock_receipt",
                new ProtocolValue() { MessageId = "U3_29", PreTableName = "", Key = "", PreKey = "" });
            //其他收货单-配件
            htTable.Add("tb_parts_stock_receipt_p",
                new ProtocolValue() { MessageId = "U3_30", PreTableName = "", Key = "", PreKey = "" });
            #endregion

            #region --财务管理
            //应收付款明细表
            htTable.Add("tb_payment_detail",
                new ProtocolValue() { MessageId = "U4_1", PreTableName = "", Key = "", PreKey = "" });
            //应收应付款单
            htTable.Add("tb_bill_receivable",
                new ProtocolValue() { MessageId = "U4_2", PreTableName = "", Key = "", PreKey = "" });
            //往来帐核销
            htTable.Add("tb_account_verification",
                new ProtocolValue() { MessageId = "U4_3", PreTableName = "", Key = "", PreKey = "" });
            //往来帐核销-业务单据
            htTable.Add("tb_verificationn_documents",
                new ProtocolValue() { MessageId = "U4_4", PreTableName = "", Key = "", PreKey = "" });
            //结算单据
            htTable.Add("tb_balance_documents",
                new ProtocolValue() { MessageId = "U4_5", PreTableName = "", Key = "", PreKey = "" });
            #endregion

            #region --会员管理
            //会员信息 (客户服务功能)
            htTable.Add("tb_CustomerSer_member",
                new ProtocolValue() { MessageId = "U5_1", PreTableName = "", Key = "", PreKey = "" });
            //会员参数设置信息表
            htTable.Add("tb_CustomerSer_member_setInfo",
                new ProtocolValue() { MessageId = "U5_2", PreTableName = "", Key = "", PreKey = "" });
            //会员参数设置特殊维修项目折扣表
            htTable.Add("tb_CustomerSer_member_setInfo_projrct",
                new ProtocolValue() { MessageId = "U5_3", PreTableName = "", Key = "", PreKey = "" });
            //会员参数设置特殊配件折扣表
            htTable.Add("tb_CustomerSer_member_setInfo_parts",
                new ProtocolValue() { MessageId = "U5_4", PreTableName = "", Key = "", PreKey = "" });
            //客户回访记录表
            htTable.Add("tb_CustomerSer_Callback",
                new ProtocolValue() { MessageId = "U5_5", PreTableName = "", Key = "", PreKey = "" });
            //信息反馈记录表
            htTable.Add("tb_CustomerSer_Feedback",
                new ProtocolValue() { MessageId = "U5_6", PreTableName = "", Key = "", PreKey = "" });
            //信息反馈记录处理记录???????????????
            htTable.Add("tb_CustomerSer_Feedback_dispose",
                new ProtocolValue() { MessageId = "U5_7", PreTableName = "", Key = "", PreKey = "" });
            //信息反馈记录审批记录???????????????
            htTable.Add("tb_CustomerSer_Feedback_approve",
                new ProtocolValue() { MessageId = "U5_8", PreTableName = "", Key = "", PreKey = "" });
            #endregion

            #region --基础数据
            //仓库档案表
            htTable.Add("tb_warehouse",
                new ProtocolValue() { MessageId = "U6_1", PreTableName = "", Key = "", PreKey = "" });
            //仓库货位档案
            htTable.Add("tb_cargo_space",
                new ProtocolValue() { MessageId = "U6_2", PreTableName = "", Key = "", PreKey = "" });
            //供应商档案
            htTable.Add("tb_supplier",
                new ProtocolValue() { MessageId = "U6_3", PreTableName = "", Key = "", PreKey = "" });
            //公司档案
            htTable.Add("tb_company",
                new ProtocolValue() { MessageId = "U6_4", PreTableName = "", Key = "", PreKey = "" });
            //司机档案
            htTable.Add("tb_driver",
                new ProtocolValue() { MessageId = "U6_5", PreTableName = "", Key = "", PreKey = "" });
            //客户档案
            htTable.Add("tb_customer",
                new ProtocolValue() { MessageId = "U6_6", PreTableName = "", Key = "", PreKey = "" });
            //工时档案
            htTable.Add("tb_workhours",
                new ProtocolValue() { MessageId = "U6_7", PreTableName = "", Key = "", PreKey = "" });
            //车型档案
            htTable.Add("tb_vehicle_models",
                new ProtocolValue() { MessageId = "U6_8", PreTableName = "", Key = "", PreKey = "" });
            //车辆档案*********
            htTable.Add("tb_vehicle",
                new ProtocolValue() { MessageId = "U6_9", PreTableName = "", Key = "", PreKey = "" });
            ////配件档案
            htTable.Add("tb_parts",
                new ProtocolValue() { MessageId = "U6_10", PreTableName = "", Key = "", PreKey = "" });
            //配件价格信息
            htTable.Add("tb_parts_price",
                new ProtocolValue() { MessageId = "U6_11", PreTableName = "", Key = "", PreKey = "" });
            //替代配件
            htTable.Add("tb_parts_replace",
                new ProtocolValue() { MessageId = "U6_12", PreTableName = "", Key = "", PreKey = "" });
            //适用车型
            htTable.Add("tb_parts_for_vehicle",
                new ProtocolValue() { MessageId = "U6_13", PreTableName = "", Key = "", PreKey = "" });
            //单位设置
            htTable.Add("tb_parts_setup",
                new ProtocolValue() { MessageId = "U6_14", PreTableName = "", Key = "", PreKey = "" });
            //字典码表
            htTable.Add("sys_dictionaries",
                new ProtocolValue() { MessageId = "U6_15", PreTableName = "", Key = "", PreKey = "" });
            //联系人
            htTable.Add("tb_contacts",
                new ProtocolValue() { MessageId = "U6_16", PreTableName = "", Key = "", PreKey = "" });
            ////附件信息***********
            //htTable.Add("attachment_info",
            //    new ProtocolValue() { MessageId = "U6_17", PreTableName = "", Key = "", PreKey = "" });
            //司机与车辆关系
            htTable.Add("tr_driver_vehicle",
                new ProtocolValue() { MessageId = "U6_18", PreTableName = "", Key = "", PreKey = "" });
            //签约信息
            htTable.Add("tb_signing_info", "U6_19");
            //签约信息-宇通????????????????????
            htTable.Add("tb_signing_info_2",
                new ProtocolValue() { MessageId = "U6_20", PreTableName = "", Key = "", PreKey = "" });
            //产品改进号
            htTable.Add("tb_product_no",
                new ProtocolValue() { MessageId = "U6_21", PreTableName = "", Key = "", PreKey = "" });
            //产品改进号-车辆
            htTable.Add("tb_product_no_vehicle",
                new ProtocolValue() { MessageId = "U6_22", PreTableName = "", Key = "", PreKey = "" });
            //产品改进号-配件
            htTable.Add("tb_product_no_part",
                new ProtocolValue() { MessageId = "U6_23", PreTableName = "", Key = "", PreKey = "" });
            //故障分类????????????
            htTable.Add("tb_fault_class",
                new ProtocolValue() { MessageId = "U6_24", PreTableName = "", Key = "", PreKey = "" });
            //故障总成?????????????
            htTable.Add("tb_fault_assembly",
                new ProtocolValue() { MessageId = "U6_25", PreTableName = "", Key = "", PreKey = "" });
            //故障总成部件????????????????????
            htTable.Add("tb_fault_component",
                new ProtocolValue() { MessageId = "U6_26", PreTableName = "", Key = "", PreKey = "" });
            //故障模式??????????????
            htTable.Add("tb_fault_model",
                new ProtocolValue() { MessageId = "U6_27", PreTableName = "", Key = "", PreKey = "" });
            //部件和故障模式关系?????????????????
            htTable.Add("tr_component_model",
                new ProtocolValue() { MessageId = "U6_28", PreTableName = "", Key = "", PreKey = "" });
            //主数据和联系人关联表??????????????
            htTable.Add("tr_base_contacts",
                new ProtocolValue() { MessageId = "U6_29", PreTableName = "", Key = "", PreKey = "" });
            //登录日志??????????????
            htTable.Add("sys_config",
                new ProtocolValue() { MessageId = "U6_30", PreTableName = "", Key = "", PreKey = "" });
            #endregion

            #region --系统设置
            //单据编码生成规则
            htTable.Add("sys_bill_code_rule",
                new ProtocolValue() { MessageId = "U7_1", PreTableName = "", Key = "", PreKey = "" });
            //自动锁屏设置
            htTable.Add("sys_automatic_lock_screen",
                new ProtocolValue() { MessageId = "U7_2", PreTableName = "", Key = "", PreKey = "" });
            //登录时间限制
            htTable.Add("sys_login_time_limit",
                new ProtocolValue() { MessageId = "U7_3", PreTableName = "", Key = "", PreKey = "" });
            //登录电脑设置*********
            htTable.Add("sys_login_pc_set",
                new ProtocolValue() { MessageId = "U7_4", PreTableName = "", Key = "", PreKey = "" });
            //帐套
            htTable.Add("sys_setbook",
                new ProtocolValue() { MessageId = "U7_5", PreTableName = "", Key = "", PreKey = "" });
            //维修业务参数
            htTable.Add("sys_repair_param",
                new ProtocolValue() { MessageId = "U7_6", PreTableName = "", Key = "", PreKey = "" });
            //采购业务参数
            htTable.Add("sys_purchase_param",
                new ProtocolValue() { MessageId = "U7_7", PreTableName = "", Key = "", PreKey = "" });
            //销售业务参数
            htTable.Add("sys_sale_param",
                new ProtocolValue() { MessageId = "U7_8", PreTableName = "", Key = "", PreKey = "" });
            //库存业务参数
            htTable.Add("sys_stock_param",
                new ProtocolValue() { MessageId = "U7_9", PreTableName = "", Key = "", PreKey = "" });
            //财务业务参数
            htTable.Add("sys_financial_ser_param",
                new ProtocolValue() { MessageId = "U7_10", PreTableName = "", Key = "", PreKey = "" });
            //业务设置-单据
            htTable.Add("sys_business_set_bill",
                new ProtocolValue() { MessageId = "U7_11", PreTableName = "", Key = "", PreKey = "" });
            //业务设置-维修套餐设置
            htTable.Add("sys_b_set_repair_package_set",
                new ProtocolValue() { MessageId = "U7_12", PreTableName = "", Key = "", PreKey = "" });
            //业务设置-维修套餐设置-车型
            htTable.Add("sys_b_set_repair_package_set_v_model",
                new ProtocolValue() { MessageId = "U7_13", PreTableName = "", Key = "", PreKey = "" });
            //业务设置-维修套餐设置-维修项目
            htTable.Add("sys_b_set_repair_package_set_project",
                new ProtocolValue() { MessageId = "U7_14", PreTableName = "", Key = "", PreKey = "" });
            //业务设置-维修套餐设置-维修用料
            htTable.Add("sys_b_set_repair_package_set_materials",
                new ProtocolValue() { MessageId = "U7_15", PreTableName = "", Key = "", PreKey = "" });
            //业务设置-维修套餐设置-其他收费项目
            htTable.Add("sys_b_set_repair_package_set_other",
                new ProtocolValue() { MessageId = "U7_16", PreTableName = "", Key = "", PreKey = "" });
            //业务设置-库存
            htTable.Add("sys_b_set_stock",
                new ProtocolValue() { MessageId = "U7_17", PreTableName = "", Key = "", PreKey = "" });
            //公告
            htTable.Add("sys_announcement",
                new ProtocolValue() { MessageId = "U7_18", PreTableName = "", Key = "", PreKey = "" });
            //公告-接收组织???????????????
            htTable.Add("sys_announcement_org",
                new ProtocolValue() { MessageId = "U7_19", PreTableName = "", Key = "", PreKey = "" });
            //公告-接收人员??????????????????
            htTable.Add("sys_announcement_user",
                new ProtocolValue() { MessageId = "U7_20", PreTableName = "", Key = "", PreKey = "" });
            //提醒设置
            htTable.Add("sys_reminding_set",
                new ProtocolValue() { MessageId = "U7_21", PreTableName = "", Key = "", PreKey = "" });
            //告警设置
            htTable.Add("sys_alarm_set",
                new ProtocolValue() { MessageId = "U7_22", PreTableName = "", Key = "", PreKey = "" });
            //银行账户设置
            htTable.Add("sys_bank_account_set",
                new ProtocolValue() { MessageId = "U7_23", PreTableName = "", Key = "", PreKey = "" });
            //出纳账户设置
            htTable.Add("sys_cashier_account_set",
                new ProtocolValue() { MessageId = "U7_24", PreTableName = "", Key = "", PreKey = "" });
            //结算方式设置
            htTable.Add("sys_settlement_way_set",
                new ProtocolValue() { MessageId = "U7_25", PreTableName = "", Key = "", PreKey = "" });


            //服务端

            //CS服务端设置???????????????????
            htTable.Add("sys_cs_server_set",
                new ProtocolValue() { MessageId = "U7_26", PreTableName = "", Key = "", PreKey = "" });
            //数据同步?????????????????
            htTable.Add("sys_data_sync",
                new ProtocolValue() { MessageId = "U7_27", PreTableName = "", Key = "", PreKey = "" });
            //数据同步日志??????????????
            htTable.Add("sys_data_sync_log",
                new ProtocolValue() { MessageId = "U7_28", PreTableName = "", Key = "", PreKey = "" });
            //数据库设置
            htTable.Add("sys_database_set",
                new ProtocolValue() { MessageId = "U7_29", PreTableName = "", Key = "", PreKey = "" });
            //登录日志???????????????
            htTable.Add("sys_log_log",
                new ProtocolValue() { MessageId = "U7_30", PreTableName = "", Key = "", PreKey = "" });
            //服务运行日志??????????????????
            htTable.Add("sys_service_operation_log",
                new ProtocolValue() { MessageId = "U7_31", PreTableName = "", Key = "", PreKey = "" });
            #endregion
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
            if (!htTable.ContainsKey(dr.Table.TableName))
            {
                return null;
            }
            UploadDataProtocol udp = new UploadDataProtocol();
            udp.StationId = GlobalStaticObj_Server.Instance.StationID;
            //根据表名得到子消息ID
            if (htTable[dr.Table.TableName] is ProtocolValue)
            {
                udp.SubMessageId = (htTable[dr.Table.TableName] as ProtocolValue).MessageId;
            }
            else
            {
                udp.SubMessageId = htTable[dr.Table.TableName].ToString();
            }
            udp.TimeSpan = TimeHelper.GetTimeInMillis();
            //Json对象
            string json = JsonHelper.DataTableToJson(dr, udp.StationId, dbName,true);
            json = BaseCodeHelper.EnCode(json);
            udp.Json = json;
            return udp;
        }
        /// <summary> 操作数据
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="operation">操作类别</param>
        /// <param name="dbName">帐套</param>
        private static void Operation(DataTable dt, string operation, string dbName)
        {
            if (dt == null)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                UploadDataProtocol protocol = GetProtocol(dr, dbName);
                if (protocol != null)
                {
                    protocol.Operation = operation;
                    ServiceAgent.AddSendQueue(protocol);
                }
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
            string tableName = string.Empty;

            foreach (DictionaryEntry de in htTable)
            {
                tableName = de.Key.ToString();
                ProtocolValue pv = null;
                if (de.Value is ProtocolValue)
                {
                    pv = de.Value as ProtocolValue;
                }
                if (tableName == "tb_announcement")
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
                    #region --添加数据
                    Operation(
                        DataHelper.GetAddData(tableName, pv, dbName,
                        TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                        TimeHelper.GetTimeInMillis(time)),
                        DataSources.EnumOperationType.Add.ToString("d"), dbName);
                    #endregion

                    Thread.Sleep(100);

                    #region --修改数据
                    Operation(
                        DataHelper.GetUpdateData(tableName, pv, dbName,
                        TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                        TimeHelper.GetTimeInMillis(time)),
                        DataSources.EnumOperationType.Update.ToString("d"), dbName);
                    #endregion

                    Thread.Sleep(100);

                    #region --删除数据
                    Operation(
                        DataHelper.GetDeleteData(tableName, pv, dbName,
                        TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                        TimeHelper.GetTimeInMillis(time)),
                        DataSources.EnumOperationType.Delete.ToString("d"), dbName);
                    #endregion
                }
                Thread.Sleep(1000);
            }
        }
        /// <summary> 上传数据 
        /// </summary>
        /// <param name="dbName">帐套信息</param>
        /// <param name="time">下次上传数据时间</param>
        public static void UpLoadDataTest(string dbName, string time)
        {
            InitHashtable();
            string tableName = string.Empty;

            foreach (DictionaryEntry de in htTable)
            {
                tableName = de.Key.ToString();
                ProtocolValue pv = null;
                if (de.Value is ProtocolValue)
                {
                    pv = de.Value as ProtocolValue;
                }
                if (tableName == "tb_announcement")
                {
                    // 区分数据来源             
                    Operation(
                        DataHelper.GetDataTest(tableName, dbName,
                        TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                        TimeHelper.GetTimeInMillis(time)),
                        DataSources.EnumOperationType.Add.ToString("d"), dbName);                           
                }
                else
                {                    
                    Operation(
                        DataHelper.GetDataTest(tableName, dbName,
                        TimeHelper.GetTimeInMillis(GlobalStaticObj_Server.Instance.LastUploadTime),
                        TimeHelper.GetTimeInMillis(time)),
                        DataSources.EnumOperationType.Update.ToString("d"), dbName);                               
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