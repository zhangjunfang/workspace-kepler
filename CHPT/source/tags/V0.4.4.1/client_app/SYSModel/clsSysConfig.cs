/*----------------------------------------------------------------
 * Copyright (C) 2012 北京北大千方科技信息有限公司 版权所有。
 * 保留所有权利

 * 文件名称: clsSysConfig.cs 
 * 编程语言: C# 
 * 文件说明: 系统相关配置参数静态变量

 * 功能: 
 * 系统静态变量

 * 当前版本: 1.0.0.0
 * 替换版本：

 * 创建人: 项载杰 
 * EMail: xiangzaijie@ctfo.com
 * 创建日期: 2012-03-29 
 * 最后修改日期: 

 * 历史修改记录: 
 * 修改人：
 * 修改时间: 
 * 修改内容: 
 * 1.
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
//using ServiceStationClient.Socket;
namespace SYSModel
{
    public enum FilterType
    {
        isAll,
        isOnline,
        isRun,
        isPositioning,
        isDvr
    }
    public enum MEMU_NAME
    {
        //Monitor,//监控
        //Track,//轨迹
        //PictureManage,//照片管理
        //DvrManage,//3G视频
        //HistoryAlarm,//历史告警
        //OperateStatReport,//运营违规
        //CaptureConfigManage,//触发拍照设置

        STR_CS_MEMU_USERMANAGE,//用户
        STR_CS_MEMU_ROLEMANAGE,//角色
        STR_CS_MEMU_FUNCTIONMANAGE,//功能

        None
    }

    public static class clsSysConfig
    {

        /// <summary>
        /// 当前机器码
        /// </summary>
        public static string _Key;

        /// <summary>
        /// 程序所用平台 客车(Bus)/货车(Truck)
        /// </summary>
        public static string _exe_Type;

        /// <summary>
        /// 演示帐号 登录名
        /// </summary>
        public static string _DemoAccount;

        /// <summary>
        /// 程序配置文件地址
        /// </summary>
        public static string _systemConfigPath;

        /// <summary>
        /// 当前程序版本号
        /// </summary>
        public static string _Version;

        /// <summary>
        /// GIS显示使用哪种方法（WEB、WIN）
        /// </summary>
        public static string _Gis;

        /// <summary>
        /// 多媒体地址头（http）
        /// </summary>
        public static string _MultimediaUrl;

        /// <summary>
        /// 登录用户名
        /// </summary>
        public static string _OpLoginname;

        /// <summary>
        /// 登陆密码
        /// </summary>
        public static string _OpPass;

        /// <summary>
        /// 企业编码
        /// </summary>
        public static string _CorpCode;

        /// <summary>
        /// 验证码
        /// </summary>
        public static string _ImgCode;

        /// <summary>
        /// action地址
        /// </summary>
        public static string _actionUrl;

        /// <summary>
        /// 报表地址
        /// </summary>
        public static string _statistiUrl;

        /// <summary>
        /// B/S登录 Cookie（CookieContainer对象）
        /// </summary>
        public static System.Net.CookieContainer _actionCookie;

        /// <summary>
        /// 用户实体ID（企业ID）
        /// </summary>
        public static string _entId;

        /// <summary>
        /// 帐号ID，使用序列SEQ_OP_ID 
        /// </summary>
        public static string OpId;

        /// <summary>
        /// （组织类型）实体类型，1为企业，2为车队
        /// </summary>
        //public static string _entType;

        /// <summary>
        /// 用户类型（0平台管理员，1企业用户，2代理商用户，3车厂用户，4车主用户）
        /// </summary>
        public static string _opType;

        /// <summary>
        /// 企业父ID（父节点ID）
        /// </summary>
        //public static string _parentEntId;

        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string _opName;

        /// <summary>
        /// 用户单位
        /// </summary>
        public static string _entName;

        /// <summary>
        /// 用户电话
        /// </summary>
        public static string _opPhone;

        /// <summary>
        /// 创建用户的人
        /// </summary>
        public static string _opUnName;

        /// <summary>
        /// 创建用户时间
        /// </summary>
        public static string _createTime;

        /// <summary>
        /// 帐号有效期
        /// </summary>
        public static string _opEndutc;

        /// <summary>
        /// 要连接到的实时服务器Socket IP地址
        /// </summary>
        public static string _SocketIP;

        /// <summary>
        /// 要连接到的实时服务器Socket 端口
        /// </summary>
        public static int _SocketPort;

        /// <summary>
        /// 连接实时服务器Socket 客户端
        /// </summary>
        //public static ServiceStationClient.Socket.clsTcpClient _TcpClient;

        /// <summary>
        /// 系统所有静态表的DataSet，包括（所有车辆列表，已选车辆列表，公告表，实时告警列表，告警排行表，多媒体列表，车辆实时上行、手动记录的事件列表）
        /// </summary>
        public static System.Data.DataSet _SystemDataSet;

        /// <summary>
        /// 多媒体信息DataTable
        /// </summary>
        public static System.Data.DataTable _mediaDataTable;

        /// <summary>
        /// 已选车辆列表表头DataTable
        /// </summary>
        public static System.Data.DataTable _ColumnDataTable;

        /// <summary>
        /// 初始化轨迹自定义显示事件DataTable
        /// </summary>
        public static System.Data.DataTable _ColumnEventDataTable;

        /// <summary>
        /// 多媒体信息最多存放多少条记录
        /// </summary>
        public static int _mediaCount = 10000;

        /// <summary>
        /// 车辆树是否已经加载完成
        /// </summary>
        public static bool _LoadTreeStatus = false;

        /// <summary>
        /// 是否弹出上下线提示框
        /// </summary>
        public static bool _OffTheAssemblyLineMedia = true;

        /// <summary>
        /// 是否播放提示音
        /// </summary>
        public static bool _OffTheAssemblyLineMedia_Sound = true;

        /// <summary>
        /// 请求车辆树时，每一页请求多少辆车
        /// </summary>
        public static int _getTreeRowsCount = 400;

        /// <summary>
        /// 实时服务是否是登录成功状态
        /// </summary>
        public static bool _SocketLoginTrue = false;

        /// <summary>
        /// 是否已经注册过，单车跟踪，更新照片事件
        /// </summary>
        public static bool _UpPhoto = false;

        /// <summary>
        /// 当前监控车车版号
        /// </summary>
        public static string CurrentVehicleNo = string.Empty;

        /// <summary>
        /// 是否已经注册过，更新车辆详细信息 事件。（ctlCatInfo）
        /// </summary>
        public static bool _isRefreshLoad = false;

        /// <summary>
        /// 单车重点监控，是否已经注册事件了
        /// </summary>
        public static bool _isRefreshLoadTrack = false;

        /// <summary>
        /// 上下线或告警信息
        /// </summary>
        public static string _strAlertControl = "";

        /// <summary>
        /// 线路详细信息DataTable
        /// </summary>
        public static System.Data.DataTable _LineDataTable;

        /// <summary>
        /// 收到照片数据包总数
        /// </summary>
        public static int _ImageCount = 0;

        /// <summary>
        /// 收到位置（告警）数据包总数
        /// </summary>
        public static int _ReadCount = 0;

        /// <summary>
        /// 告警类型视图，用于对告警类型上报筛选
        /// </summary>
        public static System.Data.DataView _AlarmTypeDataView;

        /// <summary>
        /// 树筛选
        /// </summary>
        public static string _treeRowFilter = "";

        //public static FilterType treeFilter = FilterType.isAll;

        /// <summary>
        /// 已选车辆列表HaShTable
        /// </summary>
        public static System.Collections.Hashtable _Hash_MonCarTable;

        /// <summary>
        /// 如果已选车辆超过1000辆，启用此Hash
        /// </summary>
        public static System.Collections.Hashtable _Hash_MonCarTableFor;

        public static string MeidaFileSavePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');

        public static int DebugImageTrue = 0;
        public static int DebugImageFalse = 0;
        public static int DebugLocationTrue = 0;
        public static int DebugLocationFalse = 0;

        public static string STR_FLAG_SUCCESS = "success";
        public static string STR_FLAG_FAIL = "fail";

       // public static Dictionary<string, ServiceStationClient.Model.Json.clsJsonLineInfo> LineDictionary = new Dictionary<string, ServiceStationClient.Model.Json.clsJsonLineInfo>();
        public static Dictionary<string, string> AlarmTypeDictionary = new Dictionary<string, string>();

        public static bool BOOL_VEHICLE_TRACK_CANNCEL = false;


        #region 操作日志用
        //车辆监控
        public static string STR_CS_MEMU_MONITOR = "CS_MEMU_MONITOR";//"车辆监控";
        public static string STR_CS_MEMU_MONITOR_TIME = "CS_MEMU_MONITOR_TIME";//"实时监控";
        public static string STR_CS_MEMU_MONITOR_TIME_BATCH_TRACK = "CS_MEMU_MONITOR_TIME_BATCH_TRACK";//"批量跟踪";
        public static string STR_CS_MEMU_MONITOR_TIME_BATCH_PHOTO = "CS_MEMU_MONITOR_TIME_BATCH_PHOTO";//"批量拍照";
        public static string STR_CS_MEMU_MONITOR_TIME_BATCH_NAME = "CS_MEMU_MONITOR_TIME_BATCH_NAME";//"批量点名";
        public static string STR_CS_MEMU_MONITOR_TIME_BATCH_NEWS = "CS_MEMU_MONITOR_TIME_BATCH_NEWS";//"批量消息";
        public static string STR_CS_MEMU_MONITOR_TIME_VEHICLE_DETAIL = "CS_MEMU_MONITOR_TIME_VEHICLE_DETAIL";//"车辆详情";
        public static string STR_CS_MEMU_MONITOR_TIME_SINGLE_NEWS = "CS_MEMU_MONITOR_TIME_SINGLE_NEWS";//"单车消息";
        public static string STR_CS_MEMU_MONITOR_TIME_SINGLE_PHOTO = "CS_MEMU_MONITOR_TIME_SINGLE_PHOTO";//"单车拍照";
        public static string STR_CS_MEMU_MONITOR_TIME_SINGLE_VIDEO = "CS_MEMU_MONITOR_TIME_SINGLE_VIDEO";//"单车视频";
        public static string STR_CS_MEMU_MONITOR_TIME_TIME_DATA = "CS_MEMU_MONITOR_TIME_TIME_DATA";//"实时数据";
        public static string STR_CS_MEMU_MONITOR_TIME_TIME_ALARM = "CS_MEMU_MONITOR_TIME_TIME_ALARM";//"实时告警";
        public static string STR_CS_MEMU_MONITOR_TIME_OPER_RECORD = "CS_MEMU_MONITOR_TIME_OPER_RECORD";//"操作记录";
        public static string STR_CS_MEMU_MONITOR_PHOTO = "CS_MEMU_MONITOR_PHOTO";//"照片管理";
        public static string STR_CS_MEMU_MONITOR_PHOTO_OVERVIEW = "CS_MEMU_MONITOR_PHOTO_OVERVIEW";//"照片总览";
        public static string STR_CS_MEMU_MONITOR_PHOTO_CHECK = "CS_MEMU_MONITOR_PHOTO_CHECK";//"照片巡检";
        public static string STR_CS_MEMU_MONITOR_TRACK = "CS_MEMU_MONITOR_TRACK";//"轨迹分析";
        public static string STR_CS_MEMU_MONITOR_TRACK_FIND = "CS_MEMU_MONITOR_TRACK_FIND";//"查询";
        public static string STR_CS_MEMU_MONITOR_TRACK_POINT = "CS_MEMU_MONITOR_TRACK_POINT";//"事件点";
        public static string STR_CS_MEMU_MONITOR_TRACK_EXPORT = "CS_MEMU_MONITOR_TRACK_EXPORT";//"导出";
        public static string STR_CS_MEMU_MONITOR_TRACK_SAVE = "CS_MEMU_MONITOR_TRACK_SAVE";//"保存";
        //车辆监控
        public static string STR_CS_MEMU_3GVIDEO = "CS_MEMU_3GVIDEO";//"3G视频";
        public static string STR_CS_MEMU_3GVIDEO_MONITOR = "CS_MEMU_3GVIDEO_MONITOR";//"视频监控";
        public static string STR_CS_MEMU_3GVIDEO_PLAY = "CS_MEMU_3GVIDEO_PLAY";//"视频回放";
        //统计管理
        public static string STR_CS_MEMU_STATISTICS = "CS_MEMU_STATISTICS";//"统计管理";
        public static string STR_CS_MEMU_STATISTICS_ALARM = "CS_MEMU_STATISTICS_ALARM";//"历史告警明细";
        public static string STR_CS_MEMU_STATISTICS_ALARM_FIND = "CS_MEMU_STATISTICS_ALARM_FIND";//"查询";
        public static string STR_CS_MEMU_STATISTICS_ALARM_EXPORT = "CS_MEMU_STATISTICS_ALARM_EXPORT";//"导出";
        public static string STR_CS_MEMU_STATISTICS_ILLEGAL = "CS_MEMU_STATISTICS_ILLEGAL";//"运营违规统计";
        public static string STR_CS_MEMU_STATISTICS_ILLEGAL_FIND = "CS_MEMU_STATISTICS_ILLEGAL_FIND";//"查询";
        public static string STR_CS_MEMU_STATISTICS_ILLEGAL_EXPORT = "CS_MEMU_STATISTICS_ILLEGAL_EXPORT";//"导出";
        public static string STR_CS_MEMU_STATISTICS_ILLEGAL_DATA_DETAIL = "CS_MEMU_STATISTICS_ILLEGAL_DATA_DETAIL";//"数据详情";
        public static string STR_CS_MEMU_STATISTICS_ILLEGAL_DETAIL_EXPORT = "CS_MEMU_STATISTICS_ILLEGAL_DETAIL_EXPORT";//"详情导出";
        //参数设置
        public static string STR_CS_MEMU_PARAMETER = "CS_MEMU_PARAMETER";//"参数设置";
        public static string STR_CS_MEMU_PARAMETER_TRIGGER_PHOTO = "CS_MEMU_PARAMETER_TRIGGER_PHOTO";//"触发拍照设置";

        public static string STR_CS_MEMU_USERMANAGE = "UUCUserManage";//系统用户管理
        public static string STR_CS_MEMU_ROLEMANAGE = "UUCRoleManage";//系统角色管理
        public static string STR_CS_MEMU_FUNCTIONMANAGE = "UUCFunctionManage";//系统功能管理
        public static string STR_CS_MEMU_HOMEMANAGE = "UUCHomeManage";//首页
        //public static string STR_CS_MEMU_UserControl1 = "UserControl1.cs";//测试
        /// <summary>
        /// 当前一级菜单
        /// </summary>
        public static string STR_CURR_MAINMEMU = "";
        /// <summary>
        /// 当前二级菜单
        /// </summary>
        public static string STR_CURR_TWOMEMU = "";
        /// <summary>
        /// 当前选择的菜单窗体(三级菜单)
        /// </summary>
        public static string STR_CURR_MEMU = "";
       

        //public static string STR_FG_MEMU_REALTIME_MONITOR = "FG_MEMU_REALTIME_MONITOR";//"实时监控"
        //public static string STR_FG_MEMU_STATIC_SELECT_PIC = "FG_MEMU_STATIC_SELECT_PIC";//"照片管理"
        //public static string STR_FG_MEMU_OPERATIONS_TRACKANALYSIS = "FG_MEMU_OPERATIONS_TRACKANALYSIS";//"轨迹分析"
        //public static string STR_FG_MEMU_MONITOR_VIEW = "FG_MEMU_MONITOR_VIEW";//"视频监控"
        //public static string STR_FG_MEMU_STATIC_STATIC_ILLEGAL = "FG_MEMU_STATIC_STATIC_ILLEGAL";//"运营违规统计"
        //public static string STR_FG_MEMU_ALARM_DETAIL_HIS = "FG_MEMU_ALARM_DETAIL_HIS";//"历史告警明细"
        //public static string STR_FG_MEMU_OPERATIONS_TRIGGER = "FG_MEMU_OPERATIONS_TRIGGER";//"触发拍照设置"

        public static string STR_LOG_TYPE_ID_USERLOGIN = "USERLOGIN";
        public static string STR_LOG_TYPE_ID_USERLOGOUT = "USERLOGOUT";
        public static string STR_LOG_TYPE_ID_ENTERMODULE = "ENTERMODULE";
        public static string STR_LOG_TYPE_ID_SYSOPERATE = "SYSOPERATE";

        //public static string STR_LOG_MODULE_LOGIN = "登录";
        //public static string STR_LOG_MODULE_MENU = "菜单栏";
        //public static string STR_LOG_MODULE_MONITOR = "车辆监控";
        //public static string STR_LOG_MODULE_VIDEO = "3G视频";
        //public static string STR_LOG_MODULE_STATISTIC = "统计管理";
        //public static string STR_LOG_MODULE_CONFIG = "参数设置";

        public static string STR_LOG_OPERATE_OK = "操作成功";
        public static string STR_LOG_OPERATE_FAIL = "操作失败";

        public static string STR_LOG_FUNPROCESSID=string.Empty;
        public static string STR_LOG_FUNID = string.Empty;
        public static string STR_LOG_FROM_IP = string.Empty;
        public static string LogActionUrl;
        #endregion

        /// <summary>
        /// 是否接收解析实时数据
        /// </summary>
        public static bool BOOL_RECEVIE_SOCKET_DATA = true;

        /// <summary>
        /// 监控树列表
        /// </summary>
        public static System.Data.DataTable MonitorTreeTable;

        public static System.Data.DataTable VideoTreeTable;

        /// <summary>
        /// 是否退出系统
        /// </summary>
        public static bool BOOL_SYSTEM_QUIT = false;

        /// <summary>
        /// 用户权限对象
        /// </summary>
       // public static Json.CLSJsonUserInfo USERPERMISSION = new Json.CLSJsonUserInfo();

        #region 系统配置
        /// <summary>
        /// 系统LOGO
        /// </summary>
        public static string SYSTEM_LOGO_PATH = string.Empty;
        /// <summary>
        /// 车辆标记类型
        /// </summary>
        public static string SYSTEM_VEHICLE_MARK_TYPE = string.Empty;


        #endregion
    }
}
