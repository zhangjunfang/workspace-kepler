require.config({
  paths: {
    i18n: 'i18n/i18n',
    jquery: 'plugin/jquery/jquery-1.8.1.min',
    cookie: 'plugin/jquery/jquery.cookie',
    mask: 'plugin/jquery/jquery_loadmask/jquery.loadmask',
    ligerui: 'plugin/ligerui/js/ligerui.all.ctfo',
    domReady: 'plugin/requirejs/domReady', // requirejs domReady插件
    sha1: 'plugin/sha1',
    json2: 'plugin/json2', // 对低版本浏览器没有JSON对象的支持
    validate: 'plugin/jquery/jquery_validation/jquery.validate.min', // 表单验证插件
    initConfig: 'control/ctfo.config', // 配置参数
    util: 'control/ctfo.util', // 公共组件
    login: 'control/ctfo.login' 
  },
  shim: {
    'mask': ['jquery'],
    'cookie': ['jquery'],
    'ligerui': ['jquery'],
    'validate': ['jquery'],
    'initConfig': ['jquery'],
    'util': ['jquery', 'initConfig' ],
    'login': ['util']
  },
  waitSeconds: 60,
  buildVersion: buildVersion //编译版本号，用于解决发版时客户端缓存js问题
});

require([ 
    'jquery',
    'domReady', 
    'i18n', 
    'ligerui', 
    'json2', 
    'sha1', 
    'cookie', 
    'validate', 
    'mask', 
    'login'
    ], function($, domReady) {

  domReady(function() {
    $.ajaxSetup({cache: false}); // 设置全局ajax请求不缓存
    var param = {
      loginForm: $('form[name=loginForm]')
    };
    CTFO.Model.Login.init(param);
    
  });
});