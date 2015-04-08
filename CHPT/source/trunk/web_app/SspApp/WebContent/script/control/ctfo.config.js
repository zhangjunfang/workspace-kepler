var CTFO = window.CTFO || {};
var cLog = window.cLog || null; // 用户操作追踪, 用于记录日志的外部js引用后，全局初始化供调用
// 缓存
CTFO.cache = {
  cLogInfo: {}, // 当前展现模块用于操作统计的基本信息
  systemType: 1, // 统计时系统类别代码
  user: {}, // 用户信息
  auth: {}, // 权限信息，应用系统(0-支撑系统,1-客车平台,3-车厂系统，5-移动客户端，2- CS客户端)
  menu: {}, // 用户权限下的菜单列表
  menuMap: {}, // 用户权限下的菜单数据
  alarmType: {}, // 告警类型码表缓存
  tmpl: {}, // 模板文件缓存
  data: {}, // 数据文件缓冲缓存
  generalCode: {}, // 通用编码缓存
  userCorps: {}, // 用户下的组织
  schedulePreMessage: {}, // 预设调度信息
  alarmLevel: {}, // 告警级别
  alarmTypeDesc: {}, // 告警描述缓存
  frame: null,

  universalTreeInitData: {}, // 通用左侧树数据缓存
  batchTrackingLocationData: {}, // 批量监控弹窗车辆位置信息刷新缓存
  defaultOrgLogo: "img/frame/defaultImg.png", // 默认显示logo条

  vehicleMarkerType: 1, // 车辆监控图标类型，用户可以在系统参数设置中切换不同的图标
  // ... 其他缓存
  test: ""
};

// 全局变量定义/全局对象引用
CTFO.config = {
  globalObject: {
    isShowCheckCode: true, //  true 显示checkcode， false 
    isPassByValueMode: true, // 调服务的传值方式 ， true 对象模式， false 非对象模式
    isLoadAlarmTypeDesc: false, // 初始化的时候是否加载告警类型描述
    isLoadCorpList: false, // 初始化的时候是否加载车辆信息列表 
    isLoadSchedulePreMessage: false, // 初始化的时候是否加载预设调度信息
    isSupportMultiLabel: false, // 页面支持多标签   
    addMarkerFinishedFlag: true, // 批量加marker结束标识
    alarmVoice: true, // 底部状态栏告警提示音是否播放的开关
    isRichClient: true, //是否是富客户端
    terminalType: '', // 终端类型
    commandStatusContainer: $('#footer').find('.commandReturnStatus'),
    batchMonitorWinIdCache: [], // 批量跟踪id缓存
    vehicleLatestStatusWindows: {}, // 批量跟踪弹窗对象缓存
  },
  scriptTemplateArr: [
    /*下面添加扩展模块模板*/
  ],
  // javascript模板需要在html加载完成后再获取，在ctfo.frame.js的initUtilCache方法里执行获取模板字符串的过程,
  // 存入scriptTemplate变量中
  scriptTemplate: {},
  // ajax请求url缓存
  sources: {
    /**
     * 通用
     */
    updateCustomColumns: 'json/customReportColumn/addOrUpdateCustomColumn.action', // 更新自定义列
    // mreportUrl: 'http://192.168.100.73:8888/MReport/',//管理报表服务地址, 注释，通过接口从配置文件中读取
    // clusterUrl: 'http://192.168.100.71:8080/RTCarService/', // 聚合服务地址, 注释，通过接口从配置文件中读取
    getConfig: 'json/util/getConfiguratorInfo.action', // 获取java配置信息
    userInfo: 'sys/spOperator/online.do', // 用户信息
    menuList: '', // 菜单列表, TODO 改造为从后台获取菜单数据
    auth: 'sys/function/selectFunListByOpId.do', // 权限
    generalCode: 'baseinfo/findInitSysGeneralCode.do', // 通用编码
    logout: 'loginOut.do', //注销
    login: 'login.do', // 登陆
    rondamImage: 'checkCode.do', //验证码
    passwordModify: 'json/portal/retPassword.action', // 密码修改
    customColumns: 'json/customReportColumn/findReportColumn.action', // 自定义列功能列获取接口
    findPageDisColumn: 'json/customReportColumn/findPageDisColumn.action',
    delCustomColumns: 'json/customReportColumn/deleteUserCustomColumnsByReportId.action',

    commandStatusCode: 'json/monitor/findCommandStatusCode.action', // 指令状态
    commandType: 'json/monitor/findCommandTypeCode.action', // 指令类型编码
    alarmLevel: 'json/monitor/findAlarmLevel.action', // 告警级别
    alarmTypeDesc: 'json/entbusiness/findAllSysAlarmType.action', // 根据告警code获取描述
    preMessage: 'json/systemmng/findPredefinedMsgByParam.action', // 预设消息
    commitLog: 'json/monitor/findCommandByOpId.action', // 操作日志
    updateSpOperstorStyle: 'json/systemmng/updateSpOperstorStyle.action', //换肤
    commitLogExport: 'json/monitor/exportExcelDataActionRecord.action', // 操作日志导出
    findMediaUri: 'json/monitor/findMediaUri.action', // 操作日志列表查询图片

    orgTreeInit: 'sys/org/initOrgTree.do', // 组织树初始化
    orgTreeOnlySearch: 'sys/org/initOrgTree.do', // 组织树查询
    teamTreeInit: 'json/commonTrees/findTeamTree.action', // 车队树初始化
    teamTreeOnlySearch: 'json/commonTrees/searchTeamTree.action', // 车队树查询
    getVehiclesFromTeam: 'json/commonTrees/findVehicleFromTeam.action', // 根据车队id查询所属车辆
    vehicleTreeOnlySearch: 'json/commonTrees/searchVehicleTree.action', // 车辆树查询
    lineTreeInit: 'json/commonTrees/findCorpAndLineTree.action', // 线路树初始化
    lineTreeOnlySearch: 'json/commonTrees/searchCorpAndLineTree.action', // 线路树查询
    /**
     * 首页
     */
    basicInfo: 'homepage/basicInfo.do',
    todoAuth: 'homepage/todoAuth.do',
    sysInfo: 'homepage/sysInfo.do',
    countOnline: 'monitor/online/countOnline.do',

    /*下面添加扩展模块*/
    /**
     * 组织管理
     */
    vehicleTeamGrid: 'sys/org/queryList.do', // 查询车队管理grid数据
    deleteVehicleTeam: 'sys/org/deleteItem.do', // 删除车队
    orgDetail: 'sys/org/queryById.do', // 根据组织id查询组织详情
    updateVehicleTeam: 'sys/org/updateItem.do', // 更新组织信息
    insertVehicleTeam: 'sys/org/addItem.do', // 新增组织
    exportExcelDataCorp: 'sys/org/exportOrg.do',
    checkEntNameExist: 'json/systemmng/vehicleTeamManage/checkEntNameExist.action', //判断驾驶员IC卡是否存在
    queryAutoCodeOfEnt: 'sys/org/queryAutoCode.do', //生成公司编号
    revokeEditSysEnt: 'sys/org/revokeEditSysEnt.do', //启动/吊销
    isExistEntName: 'sys/org/isExistEntName.do', //判断组织名称是否存在
    querySysEntList: 'sys/org/queryEntList.do', //查询组织列表

    exportVehicleTeamExcelData: 'sys/org/exportVehicleTeamExcelData.do', //导出组织列表
    /**
     * 公司管理
     */
    companySearchGrid: 'sys/company/queryList.do',
    companyDetail: 'sys/company/queryById.do',
    querySysCompanyList: 'sys/company/queryCompanyList.do', //查询公司列表
    queryAutoCode: 'sys/company/queryAutoCode.do', //生成公司编号
    isExistComName: 'sys/company/isExistComName.do', //判断公司是否存在
    addSysComInfo: 'sys/company/addSysComInfo.do', //增加公司
    modifySysComInfo: 'sys/company/modifySysComInfo.do', //增加公司
    revokeEditSysCom: 'sys/company/revokeEditSysCom.do', //启动/吊销
    deleteSysCom: 'sys/company/deleteSysCom.do', //删除公司

    exportCompanyListExcelData: 'sys/company/exportCompanyListExcelData.do', //导出公司列表
    /**
     * 注册鉴权审批模块
     */
    findCloudList: 'operation/cloud/findCloudBackUpList.do', //查看云备份列表
    findById: 'operation/cloud/findById.do', //根据id查询云备份对象
    deleteCloudyBackup: 'operation/cloud/deleteCloudyBackupById.do', //根据ID删除云备份
    authManageGrid: 'operation/auth/queryList.do', //按条件查询符合的公司信息
    authDetail: 'operation/auth/queryById.do', //查询对应id的公司信息
    authAddapp: 'operation/auth/queryAddApp.do', //查询该公司增值应用
    insertDownAuthDetail: 'operation/auth/insertDownDetail.do', //添加公司信息
    revokeEditAuth: 'operation/auth/revokeOpen.do', //吊销
    updateAuthApproval: 'operation/auth/updateAuthApproval.do', //审批
    updateAuthManage: 'operation/auth/updateAuthManage.do', //管理
    revokeEditAuthCloud: 'operation/auth/revokeOpenCloud.do', //吊销
    getAddApp: 'operation/auth/getAddApp.do', //查询所有增值应用
    buildAuthentication: 'operation/auth/buildAuth.do', //根据机器码生成账号,密码,鉴权码
    getSessionIP: 'operation/auth/getSessionIP.do', //获取注册ip
    reAuthorizationCloud: 'operation/auth/reAuthorizationCloud.do', //增值应用重新授权

    exportAuthManageExcelData: 'operation/auth/exportAuthManageExcelData.do', //导出注册鉴权列表
    exportCloudManageExcelData: 'operation/cloud/exportCloudManageExcelData.do', //导出云备份
    /**
     * 数据分析
     */
    repairStatGrid: 'analysis/repair/queryList.do', //按条件查询维修单
    repairSingleGrid: 'analysis/repair/queryListForRepairSingle.do', //按条件查询维修单
    repairSingleDetail: 'analysis/repair/queryrepairSingleDetail.do', //查询详细
    exportExcelDataRepair: 'analysis/repair/exportRepair.do', //维修结算单导出
    exportExcelDataRepairDetail: 'analysis/repair/exportRepairDetail.do', //维修单明细导出
    repairProject: 'analysis/repair/repairProject.do', //维修项目
    repairMaterials: 'analysis/repair/repairMaterials.do', //维修用料
    repairCharge: 'analysis/repair/repairCharge.do', //其他项目收费
    repairAnnex: 'analysis/repair/repairAnnex.do', //附件信息

    exportRepairStatExcelData: 'analysis/repair/exportRepairStatExcelData.do', //导出维修单统计
    /**
     * 系统监控
     */
    monitorServer: 'monitor/server/queryList.do', //服务端在线状态
    monitorLink: 'monitor/link/queryList.do', //宇通链路
    monitorOnlineUsers: 'monitor/online/queryList.do', //在线用户
    monitorUserBehavior: 'monitor/userBehavior/queryList.do', //用户行为监控
    monitorVisit: 'monitor/visit/queryList.do', //访问统计

    exportOnlineUsersManageExcelData: 'monitor/online/exportOnlineUsersManageExcelData.do', //导出在线用户管理
    exportMonitorServerExcelData: 'monitor/server/exportMonitorServerExcelData.do', //导出服务端在线状态
    exportMonitorLinkExcelData: 'monitor/link/exportMonitorLinkExcelData.do', //导出宇通链路
    exportMonitorUserBehaviorExcelData: 'monitor/userBehavior/exportMonitorUserBehaviorExcelData.do', //导出用户行为监控
    exportMonitorVisitExcelData: 'monitor/visit/exportMonitorVisitExcelData.do', //导出访问统计
    /**
     * 用户档案
     */
    archivesGrid: 'archives/profiles/queryList.do', //用户档案
    userFileGrid: 'archives/profiles/queryDetailList.do', //用户档案列表
    userFileSingleDetail: 'archives/profiles/queryUserFileSingleDetail.do', //用户档案列表
    userFileCompany: 'archives/profiles/queryCompanyDetail.do', //查询公司明细

    exportArchivesGridExcelData: 'archives/profiles/exportArchivesGridExcelData.do', //导出用户档案
    exportUserFileExcelData: 'archives/profiles/exportUserFileExcelData.do', //导出用户档案列表
    /**
     * 系统管理模块-公告管理
     */
    findBulletinList: 'bulletinManage/findList.do', //分页查询公告列表
    findBulletinById: 'bulletinManage/findById.do', //根据ID查询
    modifyBulletinById: 'bulletinManage/modifyById.do', //根据Id修改公告
    deleteBulletinById: 'bulletinManage/deleteById.do', //根据Id删除公告
    insertBulletinManage: 'bulletinManage/insertBulletinMange.do', //添加公告
    queryCompanyList: 'bulletinManage/queryCompanyList.do', //查询公司列表
    queryAnnouceDept: 'bulletinManage/queryAnnouceDeptList.do', //查询发布部门列表
    queryAnnouceDeptEmployeeList: 'bulletinManage/queryAnnouceDeptEmployeeList.do', //根据部门ID查询人员列表
    queryCompanySetbookList: 'bulletinManage/queryCompanySetbookList.do', //根据公司编码查询公司所有的帐套信息
    deleteAnnouceFileById: 'bulletinManage/deleteAnnouceFileById.do', //修改公告时，删除附件
    publishAnnouce: 'bulletinManage/publishAnnouceInfo.do', //发布公告
    cancelAnnouce: 'bulletinManage/cancelPublishAnnouce.do', //撤销公告
    examineAnnouce: 'bulletinManage/examineAnnouce.do', //审核公告
    querySwitch: 'bulletinManage/querySwitch.do', //查询开关状态
    publishAnnouceByInsert: 'bulletinManage/publishAnnouceByInsert.do', //新增页面发布公告
    adjustOnOff: 'adjust/adjustOnOff.do', //调节开关
    queryOnOff: 'adjust/queryOnOff.do', //开关查询
    downloadFile: 'bulletinManage/downloadFile.do', //附件下载

    exportBulletinListExcelData: 'bulletinManage/exportBulletinListExcelData.do', //导出公告列表
    /**
     * 系统用户管理
     */
    findRoleList: 'sys/spRole/query.do', //系统用户列表
    findAllRoleList: 'sys/spRole/queryRoleList.do', //角色列表
    findSpOperator: 'sys/spOperator/queryList.do', //系统用户管理数据
    revokeEditSpOperator: 'sys/spOperator/revokeOpen.do', //吊销与启用
    findSpOperatorById: 'sys/spOperator/queryById.do', //获取用户详细信息
    addSpOperator: 'sys/spOperator/addItem.do', //添加用户
    isExistSpOperator: 'sys/spOperator/isExistSpOperator.do', //添加用户验证登录名称唯一性
    modifySpOperator: 'sys/spOperator/updateItem.do', //修改用户信息
    removeUser: 'sys/spOperator/deleteItem.do', //删除用户
    modifySpOperatorPassWord: 'sys/spOperator/updatePass.do', //重置用户密码
    queryOperatorList: 'sys/spOperator/queryOperatorList.do', //查询人员列表
    queryAutoCodeOfOp: 'sys/spOperator/queryAutoCodeOfOp.do', //生成人员编号

    exportUserManageExcelData: 'sys/spOperator/exportUserManageExcelData.do', //导出用户列表
    /**
     * 操作记录管理
     */
    selectOpLogQuery: 'sys/opLog/selectQuery.do', //下拉列表
    queryOpLogList: 'sys/opLog/queryOpLogList.do', //查询

    /**
     * 系统角色管理
     */
    findSpRoleForList: 'sys/spRole/queryList.do', //系统角色管理
    findSpRoletById: 'sys/spRole/queryById.do', //查询当前角色
    findSysFunForTreeByParam: 'sys/spRole/selectFunTreeRoleEdit.do', //查询权限结果树
    findSysFunForTree: 'sys/function/initFunctionTree.do', //添加载入权限树
    modifySpRolet: 'sys/spRole/updateItem.do', //修改权限保存
    addSpRole: 'sys/spRole/addItem.do', //添加角色
    isExistSysRole: 'sys/spRole/isExistRole.do', //验证用户名称的唯一性
    findSpRoletDetailInfoById: 'sys/spRole/selectFunTreeByRoleId.do', //用户角色权限查看树
    removeRole: 'sys/spRole/deleteItem.do', //删除
    queryAutoCodeOfRole: 'sys/spRole/queryAutoCodeOfRole.do', //生成角色编号
    revokeEditOpen: 'sys/spRole/revokeEditOpen.do', //启用
    test: '',

    exportRoleManageExcelData: 'sys/spRole/exportRoleManageExcelData.do', //导出角色列表
  },
  // html模板片段
  template: {
    "homePage": "model/home/home.html",
    "corpMessageManage": "model/operations/corpMessageManage.html",
    "vehicleTeamManage": "model/operations/vehicleTeamManage.html",
    "authManage": "model/operations/authManage.html",
    "cloudManage": "model/operations/cloudManage.html",
    "repairStat": "model/operations/repairStat.html",
    "saleStat": "model/operations/saleStat.html",
    "onlineStatusManage": "model/operations/onlineStatusManage.html",
    "linkManage": "model/operations/linkManage.html",
    "onlineUsersManage": "model/operations/onlineUsersManage.html",
    "userBehaviorManage": "model/operations/userBehaviorManage.html",
    "visitStat": "model/operations/visitStat.html",
    "archivesManage": "model/operations/archivesManage.html",
    "grantManage": "model/operations/grantManage.html",
    "userManage": "model/operations/userManage.html",
    "roleManage": "model/operations/roleManage.html",
    "userFileListManage": "model/operations/userFileListManage.html",
    "companyManage": 'model/operations/companyManage.html', //角色管理查看
    "companyProfile": "model/operations/companyProfile.html",
    "personalProfile": "model/operations/personalProfile.html",
    "operaterLogManage": 'model/operations/operaterLogManage.html',
    "sysSettings": 'model/operations/sysSettings.html', //角色管理查看
    exportWindow: 'model/template/exportWin.html', // 导出弹窗页面片段

    messageDetail: 'model/template/message.htm',
    passwordModify: 'model/template/password.htm', // 密码修改弹窗
    commitLog: 'model/template/commitLog.html', // 操作日志弹窗
    eurocargo: 'model/template/eurocargo.html', //换肤
    universalTree: 'model/template/universalTree.html', // 通用左侧树框架页面
    RetPassWordPage: 'model/template/RetPassWordPage.html', //管理员重置用用户密码
    roleManageInfo: 'model/template/roleManage.html', //角色管理查看

    /*下面添加扩展模块*/
    test: ''

  },
  // 模块处理对象名称定义
  modelNames: {
    "homePage": 'CTFO.Model.HomePage', // 首页
    "corpMessageManage": 'CTFO.Model.CorpMessageManage', //企业资讯管理
    /*下面添加扩展模块*/
    "vehicleTeamManage": 'CTFO.Model.VehicleTeamManage', //组织管理
    "userManage": 'CTFO.Model.userManage', //系统用户管理
    "roleManage": 'CTFO.Model.roleManage', //系统角色管理
    "authManage": "CTFO.Model.AuthManage",
    "cloudManage": "CTFO.Model.CloudManage", //云备份管理
    "repairStat": "CTFO.Model.RepairStat",
    "saleStat": "CTFO.Model.SaleStat",
    "onlineStatusManage": "CTFO.Model.OnlineStatusManage",
    "linkManage": "CTFO.Model.LinkManage",
    "onlineUsersManage": "CTFO.Model.OnlineUsersManage",
    "userBehaviorManage": "CTFO.Model.UserBehaviorManage",
    "visitStat": "CTFO.Model.VisitStat",
    "archivesManage": "CTFO.Model.ArchivesManage",
    "grantManage": "CTFO.Model.GrantManage",
    "userFileListManage": "CTFO.Model.UserFileListManage",
    "companyManage": "CTFO.Model.CompanyManage",
    "companyProfile": "CTFO.Model.CompanyProfile",
    "personalProfile": "CTFO.Model.PersonalProfile",
    "sysSettings": "CTFO.Model.SysSettings",
    "operaterLogManage": 'CTFO.Model.OperaterLogManage',
    test: ''
  },

  colors: ['#62becf', '#ffa651', '#6ea6ec', '#ff866e', '#ffd92c', '#60cb60', '#9e9bf2', '#fff275', '#4bcbcb', '#aad54b'],
  nodatPngUrl: 'url(img/global/nodata.png)'
};

CTFO.utilFuns = {
  codeManager: null, // 通用编码
  commandFuns: null, // 指令发送
  cMap: null,
  mapTool: null,
  tipWindow: null,
  commonFuns: null,
  poiSearch: null,
  treeManager: {}
};

CTFO.Model = {};

CTFO.Util = {};

if (location.href.indexOf("/WebContent/") > 0) {
  $.each(CTFO.config.sources, function(i, n) {
    CTFO.config.sources[i] = "json/" + CTFO.config.sources[i];
  });
}