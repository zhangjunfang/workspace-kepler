using System;

namespace HXCPcClient.UCForm
{
    ///*************************************************************************//
    /// System:  
    /// FileName:     DbDic2Enum         
    /// Author:       Kord
    /// Date:         2014/11/4 9:58:04
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	DbDic2Enum
    ///***************************************************************************//
    public static class DbDic2Enum
    {

        #region Field -- 字段

        #endregion

        // ReSharper disable InconsistentNaming
        #region Property -- 属性
        #region 是否
        /// <summary>
        /// 是否 -- 是
        /// </summary>
        public static String TRUE
        {
            get { return "1"; }
        }
        /// <summary>
        /// 是否 -- 否
        /// </summary>
        public static String FALSE
        {
            get { return "0"; }
        }
        #endregion

        #region 车辆改装情况
        /// <summary>
        /// 车辆改装情况 -- 有改动
        /// </summary>
        public static String REFIT_CASE_TRUE
        {
            get { return "DCD7034A0-CA39-7980-A3FB-62E47E747673"; }
        }
        /// <summary>
        /// 车辆改装情况 -- 无改动
        /// </summary>
        public static String REFIT_CASE_FALSE
        {
            get { return "71D4B437-7768-4B9D-2DDF-37CDB17AB0D9"; }
        }
        #endregion

        #region 服务站结算单状态
        /// <summary>
        /// 服务站结算单状态
        /// </summary>
        public static String SYS_STATION_SETTLEMENT_STATUS

        {
            get { return "8F10EE9D-95B9-A5FA-BAF7-182F7C3CEB6C"; }
        }
        /// <summary>
        /// 服务站结算单状态 -- 未确认
        /// </summary>
        public static String SYS_STATION_SETTLEMENT_STATUS_UNCONFIRM
        {
            get { return "D1FEDED3-23ED-EBB1-40EA-B613AD2F505A"; }
        }
        /// <summary>
        /// 服务站结算单状态 -- 已确认
        /// </summary>
        public static String SYS_STATION_SETTLEMENT_STATUS_CONFIRM
        {
            get { return "374DACB2-BF59-76A5-52AA-8D751B6831CD"; }
        }
        #endregion

        #region 宇通三包服务单类型
        /// <summary>
        /// 宇通三包服务单类型 -- 政策照顾
        /// </summary>
        public static String BILL_TYPE_YT_100000005
        {
            get { return "4E742010-D2F9-4E54-AF63-3C3F4611ADCC"; }
        }
        /// <summary>
        /// 宇通三包服务单类型 -- 产品改进
        /// </summary>
        public static String BILL_TYPE_YT_100000006
        {
            get { return "53645ADB-9E70-4EA8-8F04-B7CC15CE2C20"; }
        }
        /// <summary>
        /// 宇通三包服务单类型 -- 强保
        /// </summary>
        public static String BILL_TYPE_YT_100000002
        {
            get { return "53C9CC83-F497-493D-B661-B7F9FC547577"; }
        }
        /// <summary>
        /// 宇通三包服务单类型 -- 服务产品
        /// </summary>
        public static String BILL_TYPE_YT_100000008
        {
            get { return "5761CB97-424D-4A71-8411-5EFA458DA38A"; }
        }
        /// <summary>
        /// 宇通三包服务单类型 -- 配件三包
        /// </summary>
        public static String BILL_TYPE_YT_100000007
        {
            get { return "7E85F2BA-7E9E-4980-9D0C-77CEF07FF99F"; }
        }
        /// <summary>
        /// 宇通三包服务单类型 -- 走保
        /// </summary>
        public static String BILL_TYPE_YT_100000001
        {
            get { return "9448EB7C-31BA-4929-9A82-FB3654C00327"; }
        }
        /// <summary>
        /// 宇通三包服务单类型 -- 新车报到
        /// </summary>
        public static String BILL_TYPE_YT_100000000
        {
            get { return "94A03FDA-6761-41E1-B7A8-59B26BB0929D"; }
        }
        /// <summary>
        /// 宇通三包服务单类型 -- 服务活动
        /// </summary>
        public static String BILL_TYPE_YT_100000004
        {
            get { return "F921474B-BFBB-4340-844E-4EEBC77723A2"; }
        }
        /// <summary>
        /// 宇通三包服务单类型 -- 维修
        /// </summary>
        public static String BILL_TYPE_YT_100000003
        {
            get { return "FEBF8F65-4F3F-4AD4-818E-59AD2E5B5DA2"; }
        }
        #endregion

        #region 数据来源
        /// <summary>
        /// 数据来源 -- 宇通
        /// </summary>
        public static String SYS_DATA_SOURCE
        {
            get { return "5A66D07E-8771-407A-BD0C-FE42F523BF38"; }
        }
        /// <summary>
        /// 数据来源 -- 自建
        /// </summary>
        public static String SYS_DATA_SOURCE_ZIJIAN
        {
            get { return "CBCA5A88-9424-4FE1-93E1-C5E7ADAF2885"; }
        }
        #endregion

        #region 启用/停用
        /// <summary>
        /// 启用
        /// </summary>
        public static String SYS_DATA_STATUS_QI
        {
            get { return "58b325d2-0792-4847-8e4a-22b3f25628f3"; }
        }
        /// <summary>
        /// 停用
        /// </summary>
        public static String SYS_DATA_STATUS_JIN
        {
            get { return "bb79f9d7-38a8-430e-84f1-6e017fc107e0"; }
        }
        #endregion

        #region 外出方式
        /// <summary>
        /// 外出方式 -- 自驾
        /// </summary>
        public static String TRAFFIC_MODE_YT_100000000
        {
            get { return "4E1A40AA-F6E4-4BE5-8939-4E1090B08CE8"; }
        }
        /// <summary>
        /// 外出方式 -- 费自驾
        /// </summary>
        public static String TRAFFIC_MODE_YT_100000001
        {
            get { return "B11266E4-CC7F-49F3-ABEC-31F9349FCE26"; }
        }
        #endregion

        #region 工时类别
        /// <summary>
        /// 工时类别 -- 定时
        /// </summary>
        public static String SYS_WORKING_HOURS_CATEGORY_DS
        {
            get { return "9E896D4F-FB1A-4D3F-AF25-6EE21489E4F0"; }
        }
        /// <summary>
        /// 工时类别 -- 定额
        /// </summary>
        public static String SYS_WORKING_HOURS_CATEGORY_DE
        {
            get { return "BF794A9B-56F5-4161-AF6D-C337DDDC7E98"; }
        }
        #endregion

        #region 单据状态
        /// <summary>
        /// 单据状态 -- 草稿
        /// </summary>
        public static String SYS_SERVICE_INFO_STATUS_CG
        {
            get { return "4CE4693D-D8E9-8847-54FA-8B6C1318545A"; }
        }
        /// <summary>
        /// 单据状态 -- 已提交
        /// </summary>
        public static String SYS_SERVICE_INFO_STATUS_YTJ
        {
            get { return "4CE4693D-D8E9-8847-54FA-8B6C1318545B"; }
        }
        /// <summary>
        /// 单据状态 -- 审核未通过
        /// </summary>
        public static String SYS_SERVICE_INFO_STATUS_SHWTG
        {
            get { return "4CE4693D-D8E9-8847-54FA-8B6C1318545C"; }
        }
        /// <summary>
        /// 单据状态 -- 已作废
        /// </summary>
        public static String SYS_SERVICE_INFO_STATUS_YZF
        {
            get { return "4CE4693D-D8E9-8847-54FA-8B6C1318545D"; }
        }
        /// <summary>
        /// 单据状态 -- 审核通过
        /// </summary>
        public static String SYS_SERVICE_INFO_STATUS_SHTG
        {
            get { return "9F489B4C-3915-8FD3-CCC9-DD4262154537"; }
        }
        #endregion

        #region 宇通车辆类别
        /// <summary>
        /// 宇通车辆类别 -- A
        /// </summary>
        public static String VM_CLASS_100000000
        {
            get { return "6711292C-D41E-44CC-A7C7-1294734692B3"; }
        }
        /// <summary>
        /// 宇通车辆类别 -- B
        /// </summary>
        public static String VM_CLASS_100000001
        {
            get { return "2CFC40BC-A3FD-4D52-9AF2-678FA8E75FE7"; }
        }
        /// <summary>
        /// 宇通车辆类别 -- C
        /// </summary>
        public static String VM_CLASS_10000003
        {
            get { return "D67013C0-E529-4DAE-9CD8-CCF34816E96E"; }
        }
        #endregion
        #endregion
        // ReSharper restore InconsistentNaming

        #region Method -- 方法

        #endregion

        #region Event -- 事件

        #endregion
    }
}
