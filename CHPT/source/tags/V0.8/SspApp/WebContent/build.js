{
    // Important: a temporary build directory location outside the static folder
    dir: "temp/",
    baseUrl: "./script",
    fileExclusionRegExp: /^(r|build|doT.min)\.js$/,
    optimizeCss: "standard",
    removeCombined: false,
    // mainConfigFile: "script/main.js",
    paths: {
        i18n: 'i18n/i18n',
        generalCode: 'util/generalCode',
        customColumns: 'util/customColumns',
        menus: 'util/menus',
        jquery: 'plugin/jquery/jquery-1.8.1.min',
        cookie: 'plugin/jquery/jquery.cookie',
        datePicker: 'plugin/DatePicker/WdatePicker', //日期控件
        tiptip: 'plugin/jquery/jquery_tiptip/jquery.tipTip.minified',
        //jui: 'plugin/jquery/jquery.ui.1.9.0/jquery-ui-1.9.0.custom.min',
        //timepicker: 'plugin/jquery/jquery_timepicker/jquery.timepicker.min',
        mask: 'plugin/jquery/jquery_loadmask/jquery.loadmask',
        pagination: 'plugin/jquery/jquery_pagination/jquery.pagination',
        amcharts: 'plugin/amcharts/swfobject', //amcharts控件(趋势图插件)
        ligerui: 'plugin/ligerui/js/ligerui.all.ctfo',
        highcharts: 'plugin/highcharts3.0.9/js/highcharts.src',
        kindeditor: 'plugin/kindeditor-4.1.10/kindeditor-min',
        kindeditor_lang: 'plugin/kindeditor-4.1.10/lang/zh_CN',
        domReady: 'plugin/requirejs/domReady', // requirejs domReady插件
        sha1: 'plugin/sha1',
        json2: 'plugin/json2', // 对低版本浏览器没有JSON对象的支持
        underscore: 'plugin/underscore',
        validate: 'plugin/jquery/jquery_validation/jquery.validate.min', // 表单验证插件
        validate_extend: 'plugin/jquery/jquery_validation/jquery.vextend', // 表单验证插件扩展,add by ctfo
        validate_message: 'plugin/jquery/jquery_validation/messages_cn',
        validate_metadata: 'plugin/jquery/jquery_validation/jquery.metadata',
        ajaxform: 'plugin/jquery/jquery.form', //异步表单提交 文件上传
        swfobject: 'plugin/swfobject_2.2',

        initConfig: 'control/ctfo.config', // 配置参数
        frame: 'control/ctfo.frame', // 框架模型
        util: 'control/ctfo.util', // 公共组件
        commitLog: 'model/ctfo.model.commitLog', // 操作日志
        bottomStatusMessage: 'model/ctfo.model.bottomStatusMessage', // 底部状态栏

        accordion: 'model/universalTree/ctfo.model.accordion', // 手风琴
        universalTree: 'model/universalTree/ctfo.model.universalTree', // 通用左侧树
        treeModel: 'model/universalTree/ctfo.model.treeModel', // 通用左侧树 > 组织树
        vehicleTree: 'model/universalTree/ctfo.model.vehicleTree', // 通用左侧树 > 车辆树
        orgTree: 'model/universalTree/ctfo.model.orgTree', // 通用左侧树 > 组织树(无checkbox)
        orgVehicleTree: 'model/universalTree/ctfo.model.orgVehicleTree', //绑定车辆对话框

        homePage: 'model/home/ctfo.model.homePage', // 首页
        vehicleTeamManage: 'model/operations/ctfo.model.vehicleTeamManage', //组织管理
        operaterLogManage: 'model/operations/ctfo.model.operaterLogManage', //组织管理
        authManage: 'model/operations/ctfo.model.authManage',
        cloudManage: 'model/operations/ctfo.model.cloudManage', //云备份管理
        repairStat: 'model/operations/ctfo.model.repairStat',
        saleStat: 'model/operations/ctfo.model.saleStat',
        onlineStatusManage: 'model/operations/ctfo.model.onlineStatusManage',
        linkManage: 'model/operations/ctfo.model.linkManage',
        onlineUsersManage: 'model/operations/ctfo.model.onlineUsersManage',
        userBehaviorManage: 'model/operations/ctfo.model.userBehaviorManage',
        userFileListManage: 'model/operations/ctfo.model.userFileListManage',
        visitStat: 'model/operations/ctfo.model.visitStat',
        archivesManage: 'model/operations/ctfo.model.archivesManage',
        grantManage: 'model/operations/ctfo.model.grantManage',
        roleManage: 'model/operations/ctfo.model.roleManage', //系统角色管理
        userManage: 'model/operations/ctfo.model.userManage', //系统用户管理
        corpMessageManage: 'model/ctfo.model.corpMessageManage', //企业公告管理
        companyManage: 'model/operations/ctfo.model.companyManage',
        personalProfile: 'model/operations/ctfo.model.personalProfile',
        companyProfile: 'model/operations/ctfo.model.companyProfile',
        sysSettings: 'model/operations/ctfo.model.sysSettings'
            /*下面添加扩展模块*/
    },
    shim: {
        // 'ligerui_core': ['jquery'],
        'tiptip': ['jquery'],
        //'jui': ['jquery'],
        //'timepicker': ['jui'],
        'mask': ['jquery'],
        'pagination': ['jquery'],
        'cookie': ['jquery'],
        'ligerui': ['jquery'],
        'validate': ['jquery'],
        'validate_extend': ['validate'],
        'validate_message': ['validate'],
        'validate_metadata': ['validate'],
        'ajaxform': ['jquery'],
        'kindeditor_lang': ['kindeditor'],

        'initConfig': ['jquery'],
        'util': ['jquery', 'initConfig'],
        'frame': ['util'],
        'commitLog': ['util'],
        'bottomStatusMessage': ['util'],

        'universalTree': ['util', 'treeModel', 'vehicleTree'],
        'accordion': ['util'],
        'treeModel': ['util', 'ligerui'],
        'vehicleTree': ['util', 'ligerui'],
        'orgTree': ['util', 'ligerui'],
        'orgVehicleTree': ['util', 'ligerui'],

        'homePage': ['jquery', 'initConfig'],
        'vehicleTeamManage': ['jquery', 'initConfig'],
        'operaterLogManage': ['jquery', 'initConfig'],
        'authManage': ['jquery', 'initConfig'],
        'cloudManage': ['jquery', 'initConfig'],
        'repairStat': ['jquery', 'initConfig'],
        'saleStat': ['jquery', 'initConfig'],
        'onlineStatusManage': ['jquery', 'initConfig'],
        'linkManage': ['jquery', 'initConfig'],
        'onlineUsersManage': ['jquery', 'initConfig'],
        'userBehaviorManage': ['jquery', 'initConfig'],
        'userFileListManage': ['jquery', 'initConfig'],
        'visitStat': ['jquery', 'initConfig'],
        'archivesManage': ['jquery', 'initConfig'],
        'grantManage': ['jquery', 'initConfig'],
        'roleManage': ['jquery', 'initConfig'],
        'userManage': ['jquery', 'initConfig'],
        'corpMessageManage': ['jquery', 'initConfig'],
        'companyManage': ['jquery', 'initConfig'],
        'personalProfile': ['jquery', 'initConfig'],
        'companyProfile': ['jquery', 'initConfig'],
        'sysSettings': ['jquery', 'initConfig']
            /*下面添加扩展模块*/
    },
    waitSeconds: 60,
    "modules": [{
        "name": "main"
    }]
}