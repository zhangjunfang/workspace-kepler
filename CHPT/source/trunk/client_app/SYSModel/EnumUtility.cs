using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using ServiceStationClient.ComponentUI;
using System.Windows.Forms;

namespace SYSModel
{
    /// <summary>
    /// 数据源操作类
    /// </summary>
    public static class DataSources
    {
        /// <summary> 项目类型
        /// </summary>
        public enum EnumProjectType
        {
            [Description("码表")]
            Dictionaries = 1,
            [Description("角色")]
            Role = 2,
            [Description("供应商")]
            Supplier = 3,
            [Description("预约单")]
            Reserve = 4,
            [Description("工时")]
            WorkingTime = 5,
            [Description("仓库")]
            WareHouse = 6,
            [Description("采购计划单")]
            PurchasePlan = 7,
            [Description("维修单")]
            Repair = 8,
            [Description("应收")]
            RECEIVABLE = 9,
            [Description("应付")]
            PAYMENT = 10,
            [Description("采购订单")]
            PurchaseOrder = 11,
            [Description("采购开单")]
            PurchaseOpenOrder = 12,
            [Description("配件档案")]
            Parts = 13,
            [Description("往来核销")]
            AccountVerification = 14,


            [Description("人员管理")]
            User = 15,
            [Description("公司档案")]
            Company = 16,
            [Description("组织管理")]
            Organization = 17,
            [Description("客户档案")]
            Customer = 18,
            [Description("会员信息")]
            CustomerSer_member = 19,

            [Description("维修返修单")]
            RepairCallback = 22,
            [Description("销售计划单")]
            SalePlan = 23,
            [Description("销售订单")]
            SaleOrder = 24,
            [Description("销售开单")]
            SaleOpenOrder = 25,
            [Description("救援单")]
            Rescue = 26,
            [Description("领料单")]
            MaterialNo = 27,
            [Description("领料退货单")]
            ReturnMaterialNo = 28,
            [Description("旧件收货单")]
            PartsReceipt = 29,
            [Description("入库单")]
            InBill = 30,
            [Description("出库单")]
            OutBill = 31,
            [Description("宇通旧件返厂单")]
            OldPartsPalautus = 32,
            [Description("旧件发货单")]
            PartsSend = 33,
            [Description("宇通三包服务单")]
            ThreeGuarantyService = 34,  // add by kord

            [Description("宇通采购订单")]
            YTPurchaseOrder = 35,
            [Description("调拨单")]
            AllotBill = 36,
            [Description("其它收货单")]
            ReceiptBill = 37,
            [Description("其它发货单")]
            ShippingBill = 38,
            [Description("三包结算单")]
            ThreeGuarantyServiceSettlement = 39,    //add by kord
            [Description("车型")]
            VehicleModels = 40,
            [Description("盘点单")]
            CheckBill = 41,
            [Description("调价单")]
            ModifyBill = 42,
            [Description("报损单")]
            LossBill = 43
        }

        /// <summary> 删除状态
        /// </summary>
        public enum EnumEnableFlag
        {
            [Description("已删除")]
            DELETED = 0,
            [Description("使用中")]
            USING
        }

        /// <summary> 自动备份类型
        /// </summary>
        public enum EnumAutoBackupType
        {
            [Description("每天")]
            EveryDay = 0,
            [Description("每周")]
            EveryWeek,
            [Description("每月")]
            EveryMonth
        }

        /// <summary> 备份方式
        /// </summary>
        public enum EnumBackupMethod
        {
            [Description("手动备份")]
            ManualBackup = 0,
            [Description("自动备份")]
            AutoBackup,
        }

        /// <summary> 周
        /// </summary>
        public enum EnumWeek
        {
            [Description("一")]
            Mon = 1,
            [Description("二")]
            Tues,
            [Description("三")]
            Wed,
            [Description("四")]
            Thur,
            [Description("五")]
            Fri,
            [Description("六")]
            Sat,
            [Description("日")]
            Sun
        }

        /// <summary> 单位性质
        /// </summary>
        public enum EunumUnitNature
        {
            [Description("国营")]
            StateRun = 0,
            [Description("私营")]
            PrivatelyOwned,
            [Description("合资")]
            JointVenture,
            [Description("个人")]
            Personal,
            [Description("其他")]
            Other
        }

        /// <summary> 维修资质
        /// </summary>
        public enum EunumRepairQualification
        {
            [Description("3A")]
            ThreeA = 0,
            [Description("2A")]
            TwoA,
            [Description("1A")]
            OneA,
            [Description("快修")]
            QuickService,
            [Description("特许")]
            Franchise,
            [Description("其他")]
            Other
        }

        /// <summary> 货币类型
        /// </summary>
        public enum EnumCurrencyType
        {
            [Description("人民币")]
            RMB = 0,
            [Description("美元")]
            USD,
            [Description("英镑")]
            GBP
        }

        /// <summary> 登陆时间限制类型
        /// </summary>
        public enum EnumLoginTimeLimitType
        {
            [Description("按时间")]
            ByTime = 1,
            [Description("按星期")]
            ByWeek = 2

        }

        /// <summary> 数据来源
        /// </summary>
        public enum EnumDataSources
        {
            [Description("自建")]
            SELFBUILD = 1,
            [Description("宇通")]
            YUTONG = 2,
            [Description("系统")]
            SYS = 3

        }
        /// <summary>
        /// 是否1是0否
        /// </summary>
        public enum EnumYesNo
        {
            [Description("是")]
            Yes = 1,
            [Description("否")]
            NO = 0
        }
        /// <summary> 
        /// 数据状态
        /// </summary>
        public enum EnumStatus
        {
            /// <summary>
            /// 启用
            /// </summary>
            [Description("启用")]
            Start = 1,
            /// <summary>
            /// 停用
            /// </summary>
            [Description("停用")]
            Stop = 0
        }
        /// <summary>
        /// 作废/激活状体
        /// </summary>
        public enum EnumInvalidOrActivation
        {
            /// <summary>
            /// 作废
            /// </summary>
            [Description("作废")]
            Invalid = 0,
            /// <summary>
            /// 激活
            /// </summary>
            [Description("激活")]
            Activation = 1
        }

        /// <summary> 分隔符
        /// </summary>
        public enum EnumDelimiter
        {
            [Description("无")]
            NONE = 0,
            [Description("-")]
            LINE,
            [Description("_")]
            UNDERLINE
        }

        /// <summary> 编码方式
        /// </summary>
        public enum EnumBillCodeMethod
        {
            [Description("无")]
            NONE = 0,
            [Description("年月日")]
            YYYYMMDD = 1,
            [Description("月日时")]
            MMDDHH
        }
        /// <summary> 授权状态
        /// </summary>
        public enum EnumAuthenticationStatus
        {
            /// <summary> 未授权
            /// </summary>
            [Description("未授权")]
            UNAUTHORIZED,
            /// <summary> 已授权
            /// </summary>
            [Description("已授权")]
            AUTHORIZED
        }

        /// <summary>
        /// 应收应付单据类型
        /// </summary>
        public enum EnumOrderType
        {
            /// <summary>
            /// 应收
            /// </summary>
            [Description("应收")]
            RECEIVABLE = 0,
            /// <summary>
            /// 应付
            /// </summary>
            [Description("应付")]
            PAYMENT
        }

        /// <summary>
        /// 应收款类型
        /// </summary>
        public enum EnumReceivableType
        {
            /// <summary>
            /// 预收
            /// </summary>
            [Description("预收")]
            ADVANCES = 0,
            /// <summary>
            /// 应收
            /// </summary>
            [Description("应收")]
            RECEIVABLE
        }

        /// <summary>
        /// 应付款类型
        /// </summary>
        public enum EnumPaymentType
        {
            /// <summary>
            /// 预付
            /// </summary>
            [Description("预付")]
            ADVANCES = 0,
            /// <summary>
            /// 应付
            /// </summary>
            [Description("应付")]
            PAYMENT
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        public enum EnumAuditStatus
        {
            /// <summary>
            /// 草稿
            /// </summary>
            [Description("草稿")]
            DRAFT = 0,
            /// <summary>
            /// 已提交
            /// </summary>
            [Description("已提交")]
            SUBMIT = 1,
            /// <summary>
            /// 已审核
            /// </summary>
            [Description("已审核通过")]
            AUDIT = 2,
            /// <summary>
            /// 审核不通过
            /// </summary>
            [Description("已审核未通过")]
            NOTAUDIT = 3,
            /// <summary>
            /// 作废
            /// </summary>
            [Description("已作废")]
            Invalid = 4,
            /// <summary>
            /// 结算
            /// </summary>
            [Description("已结算")]
            Balance = 5
        }
        /// <summary>
        /// 调度状态
        /// </summary>
        public enum EnumDispatchStatus
        {

            ///// <summary>
            ///// 未派工
            ///// </summary>
            //[Description("未派工")]
            //NotWork = 0,
            /// <summary>
            /// 未开工
            /// </summary>
            [Description("未开工")]
            NotStartWork = 0,
            /// <summary>
            /// 已开工
            /// </summary>
            [Description("已开工")]
            StartWork = 1,
            /// <summary>
            /// 已完工
            /// </summary>
            [Description("已完工")]
            FinishWork = 2,
            /// <summary>
            /// 已停工
            /// </summary>
            [Description("已停工")]
            StopWork = 3,
            /// <summary>
            /// 已质检未通过
            /// </summary>
            [Description("已质检未通过")]
            NotPassed = 4,
            /// <summary>
            /// 已质检通过
            /// </summary>
            [Description("已质检通过")]
            HasPassed = 5
        }

        /// <summary>
        /// 项目调度状态
        /// </summary>
        public enum EnumProjectDisStatus
        {
            ///// <summary>
            ///// 未派工
            ///// </summary>
            //[Description("未派工")]
            //NotWork = 0,
            /// <summary>
            /// 未开工
            /// </summary>
            [Description("未开工")]
            NotStartWork = 0,
            /// <summary>
            /// 维修中
            /// </summary>
            [Description("维修中")]
            Maintenance,
            /// <summary>
            /// 暂停
            /// </summary>
            [Description("暂停")]
            Pause,
            /// <summary>
            /// 修理完毕
            /// </summary>
            [Description("修理完毕")]
            RepairsCompleted,
            /// <summary>
            /// 质检完毕
            /// </summary>
            [Description("质检完毕")]
            InspectionCompleted,
            /// <summary>
            /// 质检未通过
            /// </summary>
            [Description("质检未通过")]
            NotInspectionCompleted

        }

        /// <summary>
        /// 往来核销
        /// </summary>
        public enum EnumAccountVerification
        {
            /// <summary>
            /// 预收冲应收
            /// </summary>
            [Description("预收冲应收")]
            YuShouToYingShou = 0,
            /// <summary>
            /// 预付冲应付
            /// </summary>
            [Description("预付冲应付")]
            YuFuToYingFu,
            /// <summary>
            /// 应付转应付
            /// </summary>
            [Description("应付转应付")]
            YingFuToYingFu,
            /// <summary>
            /// 应收转应收
            /// </summary>
            [Description("应收转应收")]
            YingShouToYingShou,
            /// <summary>
            /// 应收冲应付
            /// </summary>
            [Description("应收冲应付")]
            YingShouToYingFu,
            /// <summary>
            /// 应付冲应收
            /// </summary>
            [Description("应付冲应收")]
            YingFuToYingShou,
            /// <summary>
            /// 预收转预收
            /// </summary>
            [Description("预收转预收")]
            YuShouToYuShou,
            /// <summary>
            /// 预付转预付
            /// </summary>
            [Description("预付转预付")]
            YuFuToYuFu
        }

        /// <summary>
        /// 账户类型
        /// </summary>
        public enum EnumAccountType
        {
            [Description("现金")]
            CASH = 0,
            [Description("银行")]
            BANK = 1,
            [Description("其他")]
            OTHER = 2
        }

        /// <summary>
        /// 导入状态
        /// </summary>
        public enum EnumImportStaus
        {
            /// <summary>
            /// 开放
            /// </summary>
            [Description("开放")]
            OPEN = 0,
            /// <summary>
            /// 占用
            /// </summary>
            [Description("占用")]
            OCCUPY,
            /// <summary>
            /// 锁定
            /// </summary>
            [Description("锁定")]
            LOCK,
        }

        /// <summary>
        /// 
        /// </summary>
        public enum EnumPurchaseOrderType
        {
            /// <summary>
            /// 采购收货
            /// </summary>
            [Description("采购收货")]
            PurchaseReceive = 1,
            /// <summary>
            /// 采购退货
            /// </summary>
            [Description("采购退货")]
            PurchaseBack = 2,
            /// <summary>
            /// 采购换货
            /// </summary>
            [Description("采购换货")]
            PurchaseExchange = 3
        }
        /// <summary>
        /// 
        /// </summary>
        public enum EnumSaleOrderType
        {
            /// <summary>
            /// 销售开单
            /// </summary>
            [Description("销售开单")]
            SaleBill = 1,
            /// <summary>
            /// 销售退货
            /// </summary>
            [Description("销售退货")]
            SaleBack = 2,
            /// <summary>
            /// 销售换货
            /// </summary>
            [Description("销售换货")]
            SaleExchange = 3
        }

        /// <summary>
        /// 库存管理单据类型
        /// </summary>
        public enum EnumAllocationBillType
        {
            /// <summary>
            /// 物料入库
            /// </summary>
            [Description("入库单")]
            Storage = 1,
            /// <summary>
            /// 物料出库
            /// </summary>
            [Description("出库单")]
            OutboundOrder = 2
        }
        /// <summary>
        ///库存管理开单类型
        /// </summary>
        public enum EnumAllocationBillingType
        {
            [Description("采购开单")]
            PurchaseBilling = 1,
            [Description("销售开单")]
            SaleBilling = 2,
            [Description("领料单")]
            MaterialRequisition = 3,
            [Description("调拨单")]
            RequisitionBill = 4,
            [Description("盘点单")]
            InventoryBill = 5


        }
        /// <summary>
        ///入库单开单类型
        /// </summary>
        public enum EnumInStockBillingType
        {
            [Description("采购开单")]
            PurchaseBilling = 1,
            [Description("销售开单")]
            SaleBilling = 2,
            [Description("领料退货单")]
            MaterialRequisition = 3,
            [Description("调拨单")]
            RequisitionBill = 4,
            [Description("盘点单")]
            InventoryBill = 5,
            [Description("其它收货单")]
            OtherReceiptBill = 6

        }

        public enum EnumOperateType
        {
            /// <summary>
            /// 保存
            /// </summary>
            [Description("保存")]
            save = 1,
            /// <summary>
            /// 提交
            /// </summary>
            [Description("提交")]
            submit = 2


        }

        /// <summary> 宇通订单类型 
        /// </summary>
        public enum YTOrderType
        {
            /// <summary>
            /// 配件需求订单
            /// </summary>
            [Description("配件需求订单")]
            RequireMent = 1,
            /// <summary>
            /// 产品升级订单
            /// </summary>
            [Description("产品升级订单")]
            Upgrade = 2,
            /// <summary>
            /// 新三包调件订单
            /// </summary>
            [Description("新三包调件订单")]
            ThreeBag = 3
        }
        /// <summary> 宇通紧急程度
        /// </summary>
        public enum YTEmergency_Level
        {
            /// <summary>
            /// 一般(36小时发货)
            /// </summary>
            [Description("一般(36小时发货)")]
            Level1 = 1,
            /// <summary>
            /// 紧急(24小时发货)
            /// </summary>
            [Description("紧急(24小时发货)")]
            Level2 = 2,
            /// <summary>
            /// 特急(12小时发货)
            /// </summary>
            [Description("特急(12小时发货)")]
            Level3 = 3
        }
        /// <summary> 宇通调拨类型
        /// </summary>
        public enum YTAllot_Type
        {
            /// <summary>
            /// 正常备货
            /// </summary>
            [Description("正常备货")]
            NormalStock = 1,
            /// <summary>
            /// 紧急调度
            /// </summary>
            [Description("紧急调度")]
            InterventionSchedule = 2
        }

        /// <summary> 宇通要求发货方式
        /// </summary>
        public enum YTReq_delivery
        {
            /// <summary>
            /// 客车
            /// </summary>
            [Description("客车")]
            Car = 1,
            /// <summary>
            /// 铁路
            /// </summary>
            [Description("铁路")]
            Railway = 2,
            /// <summary>
            /// 空运
            /// </summary>
            [Description("空运")]
            AirTrans = 3,
            /// <summary>
            /// 公路
            /// </summary>
            [Description("公路")]
            Road = 4,
            /// <summary>
            /// 自提
            /// </summary>
            [Description("自提")]
            Arayacak = 5,
            /// <summary>
            /// 送货
            /// </summary>
            [Description("送货")]
            DeliverGoods = 6,
            /// <summary>
            /// 水路
            /// </summary>
            [Description("水路")]
            WaterWay = 7,
            /// <summary>
            /// 快递
            /// </summary>
            [Description("快递")]
            Expressage = 8,
            /// <summary>
            /// 其他
            /// </summary>
            [Description("其他")]
            Other = 9
        }
        /// <summary> 外部系统
        /// </summary>
        public enum EnumExternalSys
        {
            /// <summary>
            /// 宇通CRM
            /// </summary>
            [Description("宇通CRM")]
            YTCRM = 1
        }

        /// <summary> 同步方向
        /// </summary>
        public enum EnumSyncDirection
        {
            /// <summary>
            /// 上传
            /// </summary>
            [Description("上传")]
            UpLoad = 0,
            /// <summary>
            /// 下载
            /// </summary>
            [Description("下载")]
            DownLoad = 1
        }

        /// <summary> 调用接口与数据同步记录id
        /// </summary>
        public enum EnumInterfaceType
        {
            #region
            /// <summary>
            /// 基础数据-服务站
            /// </summary>
            [Description("基础数据-服务站")]
            ServiceStation = 1,
            /// <summary>
            /// 基础数据-车辆
            /// </summary>
            [Description("基础数据-车辆")]
            Bus = 2,
            /// <summary>us
            /// 基础数据-车辆客户
            /// </summary>
            [Description("基础数据-车辆客户")]
            BusCustomer = 3,
            /// <summary>
            /// 基础数据-联系人
            /// </summary>
            [Description("基础数据-联系人")]
            Contact = 4,
            /// <summary>
            /// 基础数据-工时信息
            /// </summary>
            [Description("基础数据-工时信息")]
            RepairProject = 5,
            /// <summary>
            /// 基础数据-配件信息
            /// </summary>
            [Description("基础数据-配件信息")]
            Part = 6,
            /// <summary>
            /// 基础数据-车型信息
            /// </summary>
            [Description("基础数据-车型信息")]
            BusModel = 7,
            /// <summary>
            /// 故障模式
            /// </summary>
            [Description("故障模式")]
            HitchMode = 8,
            /// <summary>
            /// 产品改进号
            /// </summary>
            [Description("产品改进号")]
            ProdImprovement = 9
            #endregion

        }
        public enum YTActivityType
        {
            /// <summary>
            /// 活动
            /// </summary>
            [Description("活动")]
            Activity = 1,
            /// <summary>
            /// 非活动
            /// </summary>
            [Description("非活动")]
            UnActivity = 2
        }

        public enum PurchasePlanFinishStatus
        {
            /// <summary>
            /// 已完成
            /// </summary>
            [Description("已完成")]
            Finish = 1,
            /// <summary>
            /// 未完成
            /// </summary>
            [Description("未完成")]
            NotFinish = 2,
            /// <summary>
            /// 已超期
            /// </summary>
            [Description("已超期")]
            OverDue = 3
        }

        /// <summary> 采购订单查询 完成状态
        /// </summary>
        public enum PurchaseOrderFinishStatus
        {
            /// <summary>
            /// 已开单
            /// </summary>
            [Description("已开单")]
            AlertlyBill = 1,
            /// <summary>
            /// 开单中
            /// </summary>
            [Description("开单中")]
            Billing = 2,
            /// <summary>
            /// 未开单
            /// </summary>
            [Description("未开单")]
            NotBill = 3,
            /// <summary>
            /// 已中止
            /// </summary>
            [Description("已中止")]
            Stop = 4
        }
        /// <summary> 采购订单查询 开单状态
        /// </summary>
        public enum PurchaseOrderBillStatus
        {
            /// <summary>
            /// 已开单
            /// </summary>
            [Description("已开单")]
            AlertlyBill = 1,
            /// <summary>
            /// 开单中
            /// </summary>
            [Description("开单中")]
            Billing = 2
        }

        /// <summary> 配件信息 是否赠品
        /// </summary>
        public enum IsGift
        {
            /// <summary>
            /// 是
            /// </summary>
            [Description("是")]
            Gift = 1,
            /// <summary>
            /// 否
            /// </summary>
            [Description("否")]
            NotGift = 0
        }

        /// <summary>
        /// 联系人类型
        /// </summary>
        public enum ContactType
        {
            /// <summary>
            /// 联系人
            /// </summary>
            [Description("联系人")]
            Contact = 1,
            /// <summary>
            /// 网点人员
            /// </summary>
            [Description("网点人员")]
            Server = 2,
            /// <summary>
            /// 用户
            /// </summary>
            [Description("用户")]
            User = 3
        }
        /// <summary>
        /// 结算状态
        /// </summary>
        public enum EnumBalanceStatus
        {
            /// <summary>
            /// 已结算
            /// </summary>
            [Description("已结算")]
            Balance = 1,
            /// <summary>
            /// 未结算
            /// </summary>
            [Description("未结算")]
            NotBalance = 2,
            /// <summary>
            /// 结算中
            /// </summary>
            [Description("结算中")]
            Balanceing = 3
        }
        /// <summary>
        /// 旧件发货单配件处理方式
        /// </summary>
        public enum EnumOldSendProcessMode
        {
            /// <summary>
            /// 返回供应商
            /// </summary>
            [Description("返回供应商")]
            ReturnGYS = 0,
            /// <summary>
            /// 就地处理
            /// </summary>
            [Description("就地处理")]
            SituTreatment = 1
        }

        /// <summary>
        /// 健康状况
        /// </summary>
        public enum EnumHealthStatus
        {
            /// <summary>
            /// 良好
            /// </summary>
            [Description("良好")]
            Good = 1,
            /// <summary>
            /// 欠佳
            /// </summary>
            [Description("欠佳")]
            NotGood = 2
        }

        /// <summary>
        ///出库单开单类型
        /// </summary>
        public enum EnumOutStockBillingType
        {
            [Description("采购开单")]
            PurchaseBilling = 1,
            [Description("销售开单")]
            SaleBilling = 2,
            [Description("领料单")]
            MaterialRequisition = 3,
            [Description("调拨单")]
            RequisitionBill = 4,
            [Description("盘点单")]
            OtherReceiptBill = 5,
            [Description("其它发货单")]
            OtherSendBill = 6
        }

        public enum EnumFieldType
        {
            /// <summary>
            /// 字符串
            /// </summary>
            [Description("字符串")]
            StrType = 1,
            /// <summary>
            /// 长整型
            /// </summary>
            [Description("长整型")]
            LongType = 2
        }
        /// <summary> 入库状态
        /// </summary>
        public enum EnumIntoStockStatus
        {
            /// <summary>
            /// 已入库
            /// </summary>
            [Description("已入库")]
            AlertlyIntoStock = 1,
            /// <summary>
            /// 未入库
            /// </summary>
            [Description("未入库")]
            NotIntoStock = 2
        }
        /// <summary> 出库状态
        /// </summary>
        public enum EnumOutStockStatus
        {
            /// <summary>
            /// 已出库
            /// </summary>
            [Description("已出库")]
            AlertlyOutStock = 1,
            /// <summary>
            /// 未出库
            /// </summary>
            [Description("未出库")]
            NotOutStock = 2
        }
        /// <summary>
        /// 查询状态
        /// </summary>
        public enum SearchState
        {
            /// <summary>
            /// 初始化查询
            /// </summary>
            [Description("初始化查询")]
            InitSearch = 0,
            /// <summary>
            /// 多条件查询
            /// </summary>
            [Description("多条件查询")]
            WhereSearch = 1
        }

        /// <summary>
        /// 提醒类别
        /// </summary>
        public enum EnumReminderType
        {
            /// <summary>
            /// 预约到站提醒
            /// </summary>
            YYDZ = 1,
            /// <summary>
            /// 待派工
            /// </summary>
            DPG = 2,
            /// <summary>
            /// 质检未通过
            /// </summary>
            XJWTG = 3,
            /// <summary>
            /// 三包服务单驳回
            /// </summary>
            SBFWDBH = 4,
            /// <summary>
            /// 保养车辆到期
            /// </summary>
            BYCLDQ = 5,
            /// <summary>
            /// 保养项目到期
            /// </summary>
            BYXMDQ = 6,
            /// <summary>
            /// 超期应收款
            /// </summary>
            CQYSK = 7,
            /// <summary>
            /// 超期应付款
            /// </summary>
            CQYFK = 8,
            /// <summary>
            /// 会员到期
            /// </summary>
            HYDQ = 9,

            /// <summary>
            /// 缺货
            /// </summary>
            QH = 10,
            /// <summary>
            /// 库存高
            /// </summary>
            KCG = 11
        }
        /// <summary>
        /// 库存统计类型
        /// </summary>
        public enum EnumStatisticType
        {
            /// <summary>
            /// 账面库存
            /// </summary>
            [Description("账面库存")]
            PaperCount = 0,
            /// <summary>
            /// 实际库存
            /// </summary>
            [Description("实际库存")]
            ActualCount = 1,
            /// <summary>
            /// 占用库存
            /// </summary>
            [Description("占用库存")]
            OccupyCount = 2
        }

        #region --云平台通讯协议相关
        /// <summary>
        /// 控制类型
        /// </summary>
        public enum EnumControlType
        {
            /// <summary>
            /// 不可用
            /// </summary>
            [Description("不可用")]
            UnAvailble = 0,
            /// <summary>
            /// 可用
            /// </summary>
            [Description("可用")]
            Availble = 1
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public enum EnumOperationType
        {
            /// <summary>
            /// 删除
            /// </summary>
            [Description("删除")]
            Delete = 0,
            /// <summary>
            /// 添加
            /// </summary>
            [Description("添加")]
            Add = 1,
            /// <summary>
            /// 更新
            /// </summary>
            [Description("更新")]
            Update = 2
        }

        /// <summary> 结果类型
        /// </summary>
        public enum EnumResultType
        {
            /// <summary>
            /// 失败
            /// </summary>
            [Description("失败")]
            Fail = 0,
            /// <summary>
            /// 成功
            /// </summary>
            [Description("成功")]
            Success = 1,
            /// <summary>
            /// 密码错误
            /// </summary>
            [Description("密码错误")]
            ErrorPwd = 2,
            /// <summary>
            /// 用户名不存在
            /// </summary>
            [Description("用户名不存在")]
            ValidUser = 3,
            /// <summary>
            /// 已登录
            /// </summary>
            [Description("已登录")]
            Logined = 4
        }
        /// <summary> 任务状态
        /// </summary>
        public enum EnumTaskStatus
        {
            /// <summary>
            /// 未启动
            /// </summary>
            [Description("未启动")]
            Not_Started = 0,
            /// <summary>
            /// 运行中
            /// </summary>
            [Description("运行中")]
            Runing = 1,
            /// <summary>
            /// 暂停中
            /// </summary>
            [Description("暂停中")]
            Suspend = 2
        }
        /// <summary> 车厂数据操作类别
        /// </summary>
        public enum EnumOperateObj
        {
            /// <summary>
            /// 状态
            /// </summary>
            [Description("状态")]
            State = 1,
            /// <summary>
            /// 数据
            /// </summary>
            [Description("数据")]
            Data = 2
        }
        /// <summary> 单据类别
        /// </summary>
        public enum EnumBillType
        {
            /// <summary>
            /// 三包服务单
            /// </summary>
            [Description("三包服务单")]
            ServiceOrder = 1,
            /// <summary>
            /// "旧件返回单
            /// </summary>
            [Description("旧件返回单")]
            PartReturn = 2,
            /// <summary>
            /// 维修结算单
            /// </summary>
            [Description("维修结算单")]
            ServiceSettle = 3,
            /// <summary>
            /// 配件采购单
            /// </summary>
            [Description("配件采购单")]
            PartPurChase = 4,
            /// <summary>
            /// 配件入库单
            /// </summary>
            [Description("配件入库单")]
            PartStorageIn = 5
        }
        /// <summary>
        /// 调拨类型
        /// </summary>
        public enum EnumAllotType
        {
            /// <summary>
            /// 同价调拨
            /// </summary>
            [Description("同价调拨")]
            SamePrice = 1,
            /// <summary>
            /// 变价调拨
            /// </summary>
            [Description("变价调拨")]
            ChangePrice = 2
        }
        /// <summary>
        /// 出入库状态
        /// </summary>
        public enum EnumBillInOutStatus
        {
            /// <summary>
            /// 未开单
            /// </summary>
            [Description("未开单")]
            WithOutBilling = 1,
            /// <summary>
            /// 开单中
            /// </summary>
            [Description("开单中")]
            Billing = 2,
            /// <summary>
            /// 已开单
            /// </summary>
            [Description("已开单")]
            HasBilling = 3
        }
        /// <summary>
        /// 其它收发货单出入库类型
        /// </summary>
        public enum EnumOtherInoutType
        {
            /// <summary>
            /// 报废
            /// </summary>
            [Description("报废")]
            Scrap = 1,
            /// <summary>
            /// 返修
            /// </summary>
            [Description("返修")]
            Repair = 2,
            /// <summary>
            /// 盘点
            /// </summary>
            [Description("盘点")]
            Inventory = 3,
            /// <summary>
            /// 生产领用
            /// </summary>
            [Description("生产领用")]
            ProductRecipient = 4,
            /// <summary>
            /// 退回代管
            /// </summary>
            [Description("退回代管")]
            ReturnEscrow = 5,
            /// <summary>
            /// 旧件
            /// </summary>
            [Description("旧件")]
            OldParts = 6

        }
        /// <summary>
        /// 其它收发货单据类型
        /// </summary>
        public enum EnumOtherInoutBillType
        {
            /// <summary>
            /// 其它收货单
            /// </summary>
            [Description("其它收货单")]
            ReceiptBill = 1,
            /// <summary>
            /// 其它发货单
            /// </summary>
            [Description("其它发货单")]
            ShippingBill = 2
        }
        /// <summary>
        /// 库存统计查询类型
        /// </summary>
        public enum EnumStockQueryType
        {
            /// <summary>
            /// 全部仓库库存
            /// </summary>
            [Description("全部仓库库存")]
            AllStock = 0,
            /// <summary>
            /// 单个仓库库存
            /// </summary>
            [Description("单个仓库库存")]
            SingleStock = 1,
            /// <summary>
            /// 宇通仓库库存
            /// </summary>
            [Description("宇通仓库库存")]
            YuTongStock = 2
        }
        #endregion

        #region 枚举方法
        /// <summary> 把枚举转换为键值对集合,调用举例: EnumUtil.EnumToList(typeof(枚举类), e => Enum.GetName(typeof(枚举类), e))
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="isSelecter">是否是选择器</param>
        /// <returns>以枚举值为key，枚举文本为value的键值对集合</returns>
        public static List<ListItem> EnumToList(Type enumType, bool isSelecter)
        {

            if (!enumType.IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型！", "enumType");
            }
            List<ListItem> list = new List<ListItem>();
            if (isSelecter)
            {
                list.Add(new ListItem("", "全部"));
            }
            else
            {
                list.Add(new ListItem("", "请选择"));
            }
            Array enumValues = Enum.GetValues(enumType);
            foreach (Enum enumValue in enumValues)
            {
                int value = Convert.ToInt32(enumValue);
                //string value = getText(enumValue);
                string text = GetDescription(enumValue);
                list.Add(new ListItem(value, text));
            }
            return list;
        }
        /// <summary>
        /// 根据枚举信息加载到ComboBox控件中,注意的是，这个方法在加载项value时，会将int转换成string类型
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="isShowFrist">是否显示首项</param>
        /// <param name="typename">首项显示的内容</param>
        /// <returns></returns>
        public static List<ListItem> EnumToListByValueString(Type enumType, bool isShowFrist, string typename)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型！", "enumType");
            }
            List<ListItem> list = new List<ListItem>();
            if (isShowFrist)
            {
                list.Add(new ListItem("", typename));
            }
            Array enumValues = Enum.GetValues(enumType);
            foreach (Enum enumValue in enumValues)
            {
                string value = Convert.ToInt32(enumValue).ToString();
                string text = GetDescription(enumValue);
                list.Add(new ListItem(value, text));
            }
            return list;
        }
        /// <summary>
        ///  枚举类型绑定ComboBoxEx  把枚举转换为键值对集合,调用举例: DataSources.BindComBoxDataEnum(cbbis_operator, typeof(DataSources.EnumYesNo), true);
        /// </summary>
        /// <param name="ControlName">下拉框name</param>
        /// <param name="enumType">枚举类型</param>
        /// <param name="isSelecter">是否是选择器true为全部 false为请选择</param>
        public static void BindComBoxDataEnum(ComboBox ControlName, Type enumType, bool isSelecter)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型！", "enumType");
            }
            List<ListItem> list = new List<ListItem>();
            if (isSelecter)
            {
                list.Add(new ListItem("", "全部"));
            }
            else
            {
                list.Add(new ListItem("", "请选择"));
            }
            Array enumValues = Enum.GetValues(enumType);
            foreach (Enum enumValue in enumValues)
            {
                string value = Convert.ToInt32(enumValue).ToString();
                //string value = getText(enumValue);
                string text = GetDescription(enumValue);
                list.Add(new ListItem(value, text));
            }
            ControlName.DisplayMember = "Text";
            ControlName.ValueMember = "Value";
            ControlName.DataSource = list;
        }

        /// <summary>
        ///  枚举类型绑定ComboBoxEx  把枚举转换为键值对集合,调用举例: DataSources.BindComBoxDataEnum(cbbis_operator, typeof(DataSources.EnumYesNo), true);
        /// </summary>
        /// <param name="ControlName">下拉框name</param>
        /// <param name="enumType">枚举类型</param>
        /// <param name="isSelecter">是否是选择器true为全部 false为请选择</param>
        public static void BindComBoxDataEnum(ComboBox ControlName, Type enumType, bool isSelecter, bool isAsc)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型！", "enumType");
            }
            List<ListItem> list = new List<ListItem>();
            if (isSelecter)
            {
                list.Add(new ListItem("", "全部"));
            }
            else
            {
                list.Add(new ListItem("", "请选择"));
            }
            Array enumValues = Enum.GetValues(enumType);

            foreach (Enum enumValue in enumValues)
            {
                string value = Convert.ToInt32(enumValue).ToString();
                //string value = getText(enumValue);
                string text = GetDescription(enumValue);
                if (isAsc)
                {
                    list.Add(new ListItem(value, text));
                }
                else
                {
                    list.Insert(1, new ListItem(value, text));
                }
            }

            ControlName.DisplayMember = "Text";
            ControlName.ValueMember = "Value";
            ControlName.DataSource = list;
        }

        /// <summary>
        /// 枚举类型绑定DataGridViewComboBoxColumn
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="enumType"></param>
        /// <param name="isSelecter"></param>
        public static void BindComDataGridViewBoxColumnDataEnum(DataGridViewComboBoxColumn columnName, Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型！", "enumType");
            }
            List<ListItem> list = new List<ListItem>();
            Array enumValues = Enum.GetValues(enumType);
            foreach (Enum enumValue in enumValues)
            {
                string value = Convert.ToInt32(enumValue).ToString();
                //string value = getText(enumValue);
                string text = GetDescription(enumValue);
                list.Add(new ListItem(value, text));
            }
            columnName.DisplayMember = "Text";
            columnName.ValueMember = "Value";
            columnName.DataSource = list;
        }
        /// <summary> 扩展方法，获得枚举的Description
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="nameInstead">当枚举值没有定义DescriptionAttribute，是否使用枚举名代替，默认是使用</param>
        /// <returns>枚举的Description</returns>
        public static string GetDescription(this Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }
        /// <summary>
        /// 获取指定枚举类型的Descripton
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="obj">值</param>
        /// <returns>Descripton</returns>
        public static string GetDescription(Type type, object obj)
        {
            //如果不是枚举类型,则返回空
            if (type.BaseType.FullName != "System.Enum")
            {
                return string.Empty;
            }
            //如果值为空则返回空
            if (obj == null || obj == DBNull.Value)
            {
                return string.Empty;
            }
            string strObj = obj.ToString();
            if (strObj.Length == 0)
            {
                return string.Empty;
            }
            int intObj = 0;
            if (!int.TryParse(strObj, out intObj))
            {
                return strObj;
            }
            string name = Enum.ToObject(type, intObj).ToString();
            FieldInfo field = type.GetField(name);
            if (field == null) return null; //add by kord
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }
        #endregion
    }
}
