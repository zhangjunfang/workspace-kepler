/*global CTFO: true, $: true */
/* devel: true, white: false */

/**
 * @author fanxuean
 * @link fanshine124@gmail.com
 * @description 框架管理器
 * @return Object {}
 */

CTFO.Model.FrameManager = (function() {
  var uniqueInstance,
    isLoaded = false,
    selectedTabClass = ' h25 tit5 lineS_l lineS_r lineS_t selectedLabel', // 选中tab标签样式
    unSelectedTabClass = ' h24 unSelectedLabel'; // 未选中tab标签样式
  function constructor() {
    var p = {};
    var models = {};
    var that = null; //全局对象
    var currentShowModel = ''; // 当前展现的模块
    var contentWidth = 0; // 中间内容区宽度
    var contentHeight = 0; // 中间内容区高度
    var contentTabHeight = 0; // 中间Tab高度
    var arrHrefPrama = null; // href 参数
    /**
     * [getUserInfo 获取用户登录信息]
     * @param  {[String]} userid [用户id]
     */
    var getUserInfo = function(d) {
      if (d) {
        // alert('查询用户登录信息成功!');
        CTFO.cache.user = d;

        cLog.addOperatorLog({
          opId: CTFO.cache.user.opId, // 操作用户ID(必填)
          opName: CTFO.cache.user.opName, // (必填)
          entId: CTFO.cache.user.entId, // 所属组织Id//(必填)
          entName: CTFO.cache.user.entName, // 组织名称(必填)
          funCbs: CTFO.cache.systemType,
          funId: '', // 如果登录后进入默认模块时需要有模块ID
          opType: '登录', // 登录/登出系统
          logTypeId: 'USERLOGIN', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作
          logClass: 'CTFO.Model.FrameManager', // 类名称
          logMethod: 'initLoginInfo', // 执行方法
          logDesc: '操作成功' // 操作成功/操作失败
        });

        // 初始化在登录成功以后 TIP
        commitLog();
        eurocargo();
        initUtilFuns();
        initMenu();
        initUtilCache();
        compileLoginInfo(d);
        initBottomStatus();
        if (d.businessLicense) initPlatformInspect();

      }
    };
    /**
     * [menuIteration 菜单迭代加载]
     * @param  {[Object]} d         [数据]
     * @param  {[Object]} doTtmpl   [模板对象]
     * @param  {[Object]} container [模板生成的html加载的容器]
     */
    var menuIteration = function(d, doTtmpl, container) {
      $(container).append(doTtmpl(d));
      $(d.c).each(function(i) {
        var m = this;
        if (m.c && m.c.length > 0) {
          menuIteration(m, doTtmpl, $(p.mainDiv).find('.nav .li_' + m.l + "_" + i));
        }
      });
    };
    /**
     * [initMenu 初始化菜单]
     */
    var initMenu = function() {
      var tmpl = p.menu_tmpl.html();
      var doTtmpl = doT.template(tmpl);
      var menus = CTFO.cache.menus = filterMenuList(menuList, 0);
      menuIteration(menus, doTtmpl, p.menuContainer);
      bindMenuEvent();
      $.each(CTFO.cache.menuMap, function(i, n) {
        n.domOl = p.headerDiv.find("[mid='" + n.mid + "']");
      });
      $.each(customColumns, function(n, v) {
        $.each(v, function(nn, vv) {
          if (vv && vv.name) {
            C[vv.name] = vv.text;
          }
        });
      });
    };
    var filterMenuList = function(data, level) {
      level = level + 1;
      $(data.c).each(function(index) {
        var auth = this.auth,
          children = this.c;
        C[this.mid] = this.name;
        if (auth) {
          if ($.inArray(auth, CTFO.cache.auth) < 0) delete data.c[index];
        }
        CTFO.cache.menuMap[this.mid] = this;
        this.l = level;
        if (children && children.length > 0) filterMenuList(this, level);
      });
      return data;
    };
    /**
     * [bindMenuEvent 绑定菜单事件]
     */
    var bindMenuEvent = function() {
      var subHover = p.headerDiv.find('ul.nav li');
      subHover.find('ul.subnav').addClass('none');
      /*subHover.hover(
        function(){
          $(this).find('ul.subnav').removeClass('none');
          IE6 subnav:hover
          $(this).find('ul.subnav ol').hover(
            function(){
                $(this).addClass('ol-hover');
            },
            function(){
                $(this).removeClass('ol-hover');
            });
        },
        function(){
          $(this).find('ul.subnav').addClass('none');
        }
      );*/
      p.headerDiv.find('.firstLevelNav').click(function(event) {
        // Act on the event
      });
      p.headerDiv.find('ul.nav > li, ul.subnav > ol').bind('click', function(e) {
        var mid = $(this).attr('mid'),
          auth = $(this).attr('auth');
        if (!$(this).hasClass('menu_selected')) {
          $(this).addClass('menu_selected').siblings().removeClass('menu_selected');
        }
        if (currentShowModel === mid) return false;
        if (mid) { // 这里通过判断是否有mid属性来判断是否是可加载子模块的菜单，点击一级菜单通常默认出发其下的第一个子菜单，首页除外
          cLog.addMenuLog({
            opId: CTFO.cache.user.opId, // 操作用户ID(必填)
            opName: CTFO.cache.user.opName, // (必填)
            entId: CTFO.cache.user.entId, // 所属组织Id//(必填)
            entName: CTFO.cache.user.entName, // 组织名称(必填)
            funCbs: CTFO.cache.systemType, // 应用系统(0-支撑系统,1-客车平台,3-车厂系统，5-移动客户端，2- CS客户端）)(必填)
            funId: auth // (必填)
          });
          //location.replace("#"+mid);
          if (isLoaded && !CTFO.config.globalObject.isRichClient) {
            $('body').loadMask('');
            location.reload();
          } else {
            changeModel(mid, '', null, 0);
          }
          isLoaded = true;
        } else $(this).find('ol:eq(0)').trigger('click');
        e.stopPropagation();
      });
      // 根据 #mid 进行跳转指定模块  begin
      var flag = false,
        url = location.href,
        arr = url.split("#");
      if (arr.length > 1) {
        arrHrefPrama = arr;
        var mid = arr[1];
        if (mid) {
          subHover.find("ol[mid='" + mid + "']:last").each(function() {
            flag = true;
            $(this).trigger('click');
          });
        }
      }
      if (!flag) {
        subHover.eq(0).trigger('click');
        subHover.eq(0).find('ol:eq(0)').trigger('click');
      }
      // 根据 #mid 进行跳转指定模块 end
    };
    /* checkTabLen   计算tab标签的长度， 如果比实际的长度，做相应的动作。*/
    var checkTabLen = function() {
        setTimeout(function() {
          var sum = 0,
            i = 0;
          p.mutiLableDiv.find(".mutiLableText").each(function() {
            sum += $(this).parent().outerWidth();
            i++;
          });
          if (sum > contentWidth) {
            var tabList = p.mutiLableDiv.find(".mutiLableText");
            p.mutiLableDiv.find(".mutiLableCloseBtn").eq(1).trigger('click');
            checkTabLen();
          }
        }, 10);
      }
      /**
       * [createMutiLabel 创建多标签]
       * @param  {[String]} mid [模块id]
       */
    var createMutiLabel = function(mid) {
      if (CTFO.config.globalObject.isSupportMultiLabel) {
        var obj = CTFO.cache.menuMap[mid];
        if (obj.isHomePage) {
          p.mutiLableDiv.addClass('none');
        } else {
          p.mutiLableDiv.removeClass('none');
        }
        p.mutiLableDiv.find("div").removeClass(selectedTabClass).addClass(unSelectedTabClass);
        if (p.mutiLableDiv.find("." + mid).length > 0) {
          p.mutiLableDiv.find("." + mid).removeClass(unSelectedTabClass).addClass(selectedTabClass);
          if (obj.isHomePage) {
            p.mutiLableDiv.addClass('none');
          } else {
            p.mutiLableDiv.removeClass('none');
          }
        } else {
          if (obj.isHomePage) {
            var none = "none";
            var icon = "ico1170";
          } else {
            var none = "";
            var icon = obj.icon;
          }
          var name = obj.name;
          var tabHtml =
            '<div mid="' + mid + '" class=" moduleLable ' + mid + ' w130 mr1 pl5 pr5 fl hand radius3-t ' + selectedTabClass + '">' +
            '<span class="mutiLableText fl" mid="' + mid + '">' +
            '<span class="' + icon + '"></span>' + name + '</span>' +
            '<span class="pr fr ' + none + '">' +
            '<span title="' + C.CLOSE + '" class="closeBtnPos mutiLableCloseBtn mt2 abs_right ico1166"></span>' +
            '</span>'
          '</div>';
          p.mutiLableDiv.append(tabHtml);
          checkTabLen();
        }
      }
    };
    /**
     * [changeModel 切换/初始化模块]
     * @param  {[String]} mid [模块id]
     * @param  {[String]} url [模块加载静态页url]
     * @param  {[Object]} ep  [额外传入的参数, 主要用于模块间的带参数跳转功能]
     */
    var changeModel = function(mid, url, ep, isFrame) {
      hideModel();
      createMutiLabel(mid);
      CTFO.cache.currentShowModel = mid;
      if ($('#' + mid).length > 0 && (isFrame || models[mid])) {
        currentShowModel = mid;
        if (models[mid]) models[mid].showModel(ep);
        else if (isFrame) $('#' + mid).show();
        // resize();
        changeCurrentShowModelCss(currentShowModel);
        return false;
      }
      var subContent = $('<div>');
      subContent.attr('id', mid);
      subContent.addClass("modelDiv")
        .css({
          height: p.mutiLableDiv.hasClass('none') ? contentHeight : (contentHeight - contentTabHeight)
        });
      $(subContent).appendTo(p.contentDiv);
      if (ep) {
        var targetMenu = null;
        p.headerDiv.find('ul.nav li, ol').each(function(index) {
          var menuMid = $(this).attr('mid'),
            menuUrl = $(this).attr('url');
          if (mid === menuMid) {
            targetMenu = $(this);
            url = url || menuUrl;
          }
        });
        if (!$(targetMenu).hasClass('menu_selected')) {
          $(targetMenu).addClass('menu_selected').siblings().removeClass('menu_selected');
        }
      }
      CTFO.utilFuns.commonFuns.initTmpl(CTFO.config.template[mid], function(tmpl) {
        $(subContent).append(doT.template(tmpl));
        var nf = (new Function('return ' + CTFO.config.modelNames[mid] + '.getInstance()'))();
        var param = {
          mid: mid,
          cWidth: contentWidth,
          cHeight: p.mutiLableDiv.hasClass('none') ? contentHeight : (contentHeight - contentTabHeight),
          contentDiv: p.contentDiv,
          mainContainer: $(subContent),
          menuContainer: p.menuContainer,
          ep: ep
        };
        if (nf) {
          models[mid] = nf.init(param);
          currentShowModel = mid;
          changeCurrentShowModelCss(currentShowModel);
        }
      });
    }
    var changeCurrentShowModelCss = function(mid) {
      var auth = p.headerDiv.find('ul.nav > li[mid=' + mid + '], ul.subnav > ol[mid=' + mid + ']').attr('auth');
      if (mid == 'homePage' || mid == 'travelPage') {
        p.headerDiv.find('ul.nav li[mid=' + mid + ']').addClass('hover').siblings().removeClass('hover');
      } else {
        p.headerDiv.find('ul.nav ol[mid=' + mid + ']').parentsUntil('ul.nav li:eq(0)').addClass('hover').siblings().removeClass('hover');
      }
      CTFO.cache.cLogInfo = {
        auth: auth,
        mid: mid,
        opId: CTFO.cache.user.opId, // 操作用户ID(必填)
        opName: CTFO.cache.user.opName, // (必填)
        entId: CTFO.cache.user.entId, // 所属组织Id//(必填)
        entName: CTFO.cache.user.entName, // 组织名称(必填)
        funCbs: CTFO.cache.systemType
      };
    };
    /**
     * [hideModel 隐藏模块]
     */
    var hideModel = function() {
      var modelDivs = p.contentDiv.find('.modelDiv');
      // if ($(p.mainDiv).find('.welcome').css('display') != 'none') {
      //     $(p.mainDiv).find('.welcome').hide().siblings('.frame_content').removeClass('hidden');
      // }
      if (modelDivs.length === 0) return false;
      modelDivs.each(function() {
        if ($(this).attr('display') !== 'none') {
          var mid = $(this).attr('id');
          if (models[mid]) models[mid].hideModel();
          else $('#' + mid).hide();
        }
      });
    };
    /**
     * [initLoginInfo 初始化用户信息和权限信息]
     *  [无返回,实际对CTFO.cache.user和CTFO.cache.auth对象赋了值]
     */
    var initLoginInfo = function(data) {
      if (!data || data.error) {
        var err = data ? data.error[0].errorMessage : '查询用户权限错误';
        if (err) $.ligerDialog.error(err);

        if (window.location.href !== "login.html") window.location.replace("login.html");
      } else {
        CTFO.cache.auth = data;
      }
    };
    /**
     * [compileLoginInfo 渲染登录信息]
     * @param  {[Object]} userInfo [用户信息]
     * @return {[type]}          [description]
     */
    var compileLoginInfo = function(userInfo) {
      var opEndutc = userInfo.opEndutc;
      var validityTimeStr = C.NEVER + C.VALID;
      if (!!opEndutc) {
        validityTimeStr = CTFO.utilFuns.dateFuns.dateFormat(new Date(opEndutc), 'yyyy-MM-dd');
        // var opEndutc = validityTime + 86400000;
        var nowUtc = new Date().getTime();
        if ((opEndutc > nowUtc) && ((opEndutc - nowUtc) < (5 * 24 * 3600 * 1000))) {
          validityTimeStr = "<font color='red'>" + validityTimeStr + "</font>";
        }
      }
      //p.headerDiv.find('.logo').css('background', 'url(' + (userInfo.orgLogo||CTFO.cache.defaultOrgLogo) + ') center no-repeat');

      p.headerDiv
        .find('.tnone').html(C.SITE_NAME).end()
        .find('.user_info span:eq(0)').html(C.VALID + C.QI + '：').addClass('none').end()
        .find('.user_info span:eq(1)').html(validityTimeStr).addClass('none').end()
        .find('.user_info span:eq(2)').html(C.WELCOME + '：').addClass('none').end()
        .find('.user_info span:eq(4)').text(userInfo.opName.substring(0, 5) + (userInfo.opName.length > 6 ? "..." : "")).attr('title', userInfo.opName).click(function() {
          CTFO.cache.frame.changeModel('personalProfile', '', null, 0);
        }).end()
        .find('.user_info span:eq(3)').html(C.MODIFY + C.PASSWORD).addClass('none').click(function() {
          CTFO.Model.passwordWindow.getInstance().popResetPasswordWin();
        }).end()
        .find('.user_info span:eq(5)').html(' ').end()
        .find('.user_info span:eq(6)').html('[退出]').click(function() {
          logout();
        }).end()
        /*.find('.user_info span:eq(7)').html('|').end()
        .find('.user_info span:eq(8)').html(C.HELP).click(function(){
            showHelpWindow();
        }).end()*/
      ;
      p.footerDiv
        .find('.commitLog span:eq(1)').html(C.OPERATE + C.LOG).end()
        .find('.alarmMore').html(C.MORE).end();
    };
    /**
     * [logout 注销]
     * @return {[type]} [description]
     */
    var logout = function() {
      $.ligerDialog.confirm('您确认要退出运营支撑系统吗？', function(yes) {
        if (yes) {
          $.ajax({
            url: CTFO.config.sources.logout,
            type: 'POST',
            dataType: 'json',
            data: {
              param1: 'value1'
            },
            complete: function(xhr, textStatus) {
              //called when complete
            },
            success: function(data, textStatus, xhr) {
              cLog.addOperatorLog({
                opId: CTFO.cache.user.opId, // 操作用户ID(必填)
                opName: CTFO.cache.user.opName, // (必填)
                entId: CTFO.cache.user.entId, // 所属组织Id//(必填)
                entName: CTFO.cache.user.entName, // 组织名称(必填)
                funCbs: CTFO.cache.systemType,
                funId: '', // 如果登录后进入默认模块时需要有模块ID
                opType: '登出', // 登录/登出系统
                logTypeId: 'USERLOGOUT', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作
                logClass: 'CTFO.Model.FrameManager', // 类名称
                logMethod: 'logout', // 执行方法
                logDesc: '操作成功' // 操作成功/操作失败
              });
              window.location.replace("login.html");
            },
            error: function(xhr, textStatus, errorThrown) {
              cLog.addOperatorLog({
                opId: CTFO.cache.user.opId, // 操作用户ID(必填)
                opName: CTFO.cache.user.opName, // (必填)
                entId: CTFO.cache.user.entId, // 所属组织Id//(必填)
                entName: CTFO.cache.user.entName, // 组织名称(必填)
                funCbs: CTFO.cache.systemType,
                funId: '', // 如果登录后进入默认模块时需要有模块ID
                opType: '登出', // 登录/登出系统
                logTypeId: 'USERLOGOUT', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作
                logClass: 'CTFO.Model.FrameManager', // 类名称
                logMethod: 'logout', // 执行方法
                logDesc: '操作失败' // 操作成功/操作失败
              });
              $.ligerDialog.success(JSON.stringify(errorThrown));
            }
          });

        }
      });
    };
    /**
     * [showHelpWindow 显示帮助文档窗口]
     * @return {[type]} [description]
     */
    var showHelpWindow = function() {
      var param = {
        url: 'help.html',
        title: '帮助',
        ico: 'ico221',
        width: 600,
        height: 400
      };
      CTFO.utilFuns.tipWindow(param);
    };
    /**
     * [commitLog 操作日志弹窗]
     * @return {[type]} [description]
     */
    var commitLog = function() {
      /*根据用户登录entType;不是1或2的；隐藏掉*/
      if ($.inArray(+CTFO.cache.user.entType, [1, 2]) < 0) {
        p.footerDiv.find('.commitLog').remove();
      }
      p.footerDiv.find('.commitLog').click(function() {
        var param = {
          icon: 'ico227',
          title: '操作日志',
          url: CTFO.config.template.commitLog,
          width: 950,
          height: 400,
          onLoad: function(w, d, g) {
            CTFO.Model.CommitLog.getInstance().init({
              winObj: w,
              dataObj: d
            });
          }
        };
        CTFO.utilFuns.tipWindow(param);
      });
    };
    /**
     * [initBottomStatus 底部状态栏]
     * @return {[type]} [description]
     */
    var initBottomStatus = function() {
      CTFO.Model.BottomStatusMessage.getInstance().init({
        container: p.footerDiv,
        menuContainer: p.menuContainer,
        contentDiv: p.contentDiv
      });
    };
    /**
     * [initPlatformInspect 定时查岗]
     * @return {[type]} [description]
     */
    var initPlatformInspect = function() {
      //if (null != CTFO.cache.user.businessLicense && '' != CTFO.cache.user.businessLicense) {
      CTFO.Model.PlatformInspect.getInstance().init({
        contentDiv: p.contentDiv
      });
      //}
    };
    /**
     * [initUtilFuns 初始化公共函数]
     * @return {[type]} [description]
     */
    var initUtilFuns = function() {
      // 弹窗
      CTFO.utilFuns.tipWindow = function(p) {
        p = p || {};
        if (p.url || p.content) {
          var win = $('<div>');
          $(win).applyCtfoWindow($.extend({}, p));
        }
      };
      CTFO.utilFuns.dateFuns = new CTFO.Util.Date();
      CTFO.utilFuns.throttle = CTFO.Util.throttle;

      // 指令下发函数
      CTFO.utilFuns.commandFuns = new CTFO.Util.Commands();
    };
    /**
     * [initUtilCache 初始化通用预加载信息]
     * @return {[type]} [description]
     */
    var initUtilCache = function() {
      if (CTFO.config.globalObject.isLoadSchedulePreMessage) {
        $.get(CTFO.config.sources.preMessage + '?timestamp=' + new Date().getTime(), null, function(data, textStatus, xhr) {
          CTFO.cache.schedulePreMessage = data;
        }, 'json');
      }
      $(CTFO.config.scriptTemplateArr).each(function(index) {
        var n = this.name,
          value = this.value;
        CTFO.config.scriptTemplate[n] = $(value).html();
      });
    };
    /**
     * [ 从配置文件中读取配置信息]
     */
    var initDefaultConfig = function(data) {
      if (!data) return false;
      /*CTFO.config.sources.mreportUrl = data.mReportUrl;
      CTFO.config.sources.clusterUrl = data.clusterUrl;
      CTFO.config.sources.travelPageUrl = data.travelPageUrl;*/
      if (data.isRichClient) {
        CTFO.config.globalObject.isRichClient = data.isRichClient === "true" ? true : false;
      }
      if (data.isSupportMultiLabel) {
        CTFO.config.globalObject.isSupportMultiLabel = data.isSupportMultiLabel === "true" ? true : false;
      }
      if (!CTFO.config.globalObject.isRichClient) {
        CTFO.config.globalObject.isSupportMultiLabel = false;
      }
      //初始化日志服务
      if (window.CommonsLog) {
        cLog = window.CommonsLog.getInstance();
      } else {
        cLog = {
          addOperatorLog: function() {},
          addMenuLog: function() {}
        };
      }
    };
    /**
     * [resize 监听浏览器窗口宽高调整]
     * @return {[type]} [description]
     */
    var resize = function() {
      var dw = $.browser.opera ? document.body.clientWidth : document.documentElement.clientWidth,
        dh = $.browser.opera ? document.body.clientHeight : document.documentElement.clientHeight,
        // TODO 这里ie8下改变body下的容器的宽高，使body的高度发生变化时，也会触发resize事件，这里采用计算中间内容高度时保持和模块内的
        // resize方法计算的高度一致的方式来确保表现一致性，需要再研究下，可以阻止ie8下的这种resize
        ch = dh - (p.headerDiv.css('display') !== 'none' ? p.headerDiv.height() : 0) - (p.footerDiv.css('display') !== 'none' ? p.footerDiv.height() : 0);
      contentHeight = ch < 520 ? 520 : ch; // 580是内容区的最小高度
      contentTabHeight = p.mutiLableDiv.outerHeight();
      contentWidth = dw;
      p.contentDiv.height(contentHeight);
      if (models[currentShowModel]) models[currentShowModel].resize(ch);
    };

    /**
     *皮肤选择配置
     */
    var skinFun = function(d) {
      if (d == 1) {
        $('link[name=styleLink]').removeAttr('href').attr('href', 'css/base.css');
      } else if (d == 2) {
        $('link[name=styleLink]').removeAttr('href').attr('href', 'css/navy/base.css');
      } else if (d == 3) {
        $('link[name=styleLink]').removeAttr('href').attr('href', 'css/Black/base.css');
      } else if (d == 4) {
        $('link[name=styleLink]').removeAttr('href').attr('href', 'css/wood/base.css');
      }
    };

    /**
     *换肤
     */
    var eurocargo = function() {
      //初始化皮肤
      CTFO.cache.user.skinStyle = CTFO.cache.user.skinStyle || 1;
      var skin = CTFO.cache.user.skinStyle;
      skinFun(skin);

      $('.eurocargoBox').click(function(event) {
        var param = {
          title: '选择皮肤',
          url: CTFO.config.template.eurocargo,
          width: 500,
          height: 255,
          onLoad: function(w, d, g) {
            //初始化皮肤
            $(w).find('.skinBox').removeClass('focus').end().find('.btn').addClass('none');
            $(w).find('.skinBox').eq(skin - 1).addClass('focus').end().eq(skin - 1).find('.btn').removeClass('none');

            //切换皮肤
            $(w).find('.eurocargoList li').click(function(event) {
              $(w).find('.skinBox').removeClass('focus').end().find('.btn').addClass('none');
              $(this).find('.skinBox').addClass('focus').end().find('.btn').removeClass('none');
              var skinNum = $(this).index() + 1
              $(w).find("input[name=skinStyle]").val(skinNum);
              skinFun(skinNum);
            });
            //提交操作
            $(w).find('form[name=eurocargoForm]').find('span[name=update]').click(function(event) {
              skin = $(w).find("input[name=skinStyle]").val();
              var skinParam = {
                'opId': CTFO.cache.user.opId, // 告警id
                'skinStyle': skin // 备注
              };
              $.ajax({
                  url: CTFO.config.sources.updateSpOperstorStyle,
                  type: 'POST',
                  dataType: 'json',
                  data: skinParam,
                })
                .done(function() {
                  $('.l-dialog-close').trigger('click');
                  skinFun(skin);
                })
                .fail(function() {
                  //console.log("error");
                })
                .always(function() {
                  //console.log("complete");
                });

            });
            //取消操作
            $(w).find('form[name=eurocargoForm]').find('span[name=cancel]').click(function(event) {
              skinFun(skin);
              $('.l-dialog-close').trigger('click');
            });
          },
          onCloseWin: function(w, d, g) {
            skinFun(skin);
          }
        };
        CTFO.utilFuns.tipWindow(param);
      });

    };

    /**
     * 关闭窗口或浏览器时，记录登出日志
     */
    var bfunload = function() {
      cLog.addOperatorLog({
        opId: CTFO.cache.user.opId, // 操作用户ID(必填)
        opName: CTFO.cache.user.opName, // (必填)
        entId: CTFO.cache.user.entId, // 所属组织Id//(必填)
        entName: CTFO.cache.user.entName, // 组织名称(必填)
        funCbs: CTFO.cache.systemType,
        funId: '', // 如果登录后进入默认模块时需要有模块ID
        opType: '登出', // 登录/登出系统
        logTypeId: 'USERLOGOUT', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作
        logClass: '', // 类名称
        logMethod: 'logout', // 执行方法
        logDesc: '操作成功' // 操作成功/操作失败
      });
    };

    /**
     * [bindEvent 绑定全局事件]
     * @return {[type]} [description]
     */
    var bindEvent = function() {
      $(window).resize(function() {
        // CTFO.utilFuns.throttle(resize, 50, 100);
        that.resize();
      });

      //浏览器或窗口关闭前
      $(window).bind("beforeunload", function() {
        bfunload();
      });

      if (CTFO.config.globalObject.isSupportMultiLabel) {
        $(p.mutiLableDiv)
          .delegate(".moduleLable", "mouseenter", function() {
            if ($(this).hasClass('h24')) {
              $(this).addClass('tit5').removeClass('unSelectedLabel');
            }
          })
          .delegate(".moduleLable", "mouseleave", function() {
            if ($(this).hasClass('h24')) {
              $(this).addClass('unSelectedLabel').removeClass('tit5');
            }
          })
          .delegate(".moduleLable", "click", function() {
            var mid = $(this).attr("mid");
            if (mid) {
              CTFO.cache.menuMap[mid].domOl.trigger('click');
            }
          })
          .delegate(".mutiLableCloseBtn", "click", function() {
            var div = $(this).parent().parent();
            if (div.hasClass(selectedTabClass)) {
              var prevDiv = div.prev();
              if (prevDiv.length > 0) {
                var mid = prevDiv.attr("mid");
                CTFO.cache.menuMap[mid].domOl.trigger('click');
              } else {
                hideModel();
              }
            }
            div.remove();
            return false;
          })
          .delegate(".mutiLableCloseBtn", "mouseenter", function() {
            $(this).addClass('ico1167 closeBtnPosHove').removeClass('ico1166 closeBtnPos');
          })
          .delegate(".mutiLableCloseBtn", "mouseleave", function() {
            $(this).addClass('ico1166 closeBtnPos').removeClass('ico1167 closeBtnPosHove');
          });
      }
    };
    /*初始化配置数据*/
    initConfigData = function() {
      $.when(
          /* $.ajax( {
             url: CTFO.config.sources.getConfig,
             type: 'POST',
             dataType: 'json'
           }),*/
          $.ajax({
            url: CTFO.config.sources.auth,
            type: 'POST',
            dataType: 'json'
          }),
          $.ajax({
            url: CTFO.config.sources.userInfo,
            type: 'POST',
            dataType: 'json'
          }),
          $.ajax({
            url: CTFO.config.sources.generalCode,
            type: "POST",
            dataType: "json"
          })
        )
        .done(function(a1, a2, a3) {
          //initDefaultConfig(a0[0]);
          initDefaultConfig({});
          initLoginInfo(a1[0]);
          getUserInfo(a2[0]);
          CTFO.cache.generalCode = $.extend({}, generalCode || {}, (a3[0]) || {});
          // 通用编码器
          CTFO.utilFuns.codeManager = CTFO.Util.CodeManager.getInstance().init();
        })
        .fail(function() {
          if (err) $.ligerDialog.error('查询用户配置数据错误');
          if (window.location.href !== "login.html") window.location.replace("login.html");
        });
      //通用函数
      CTFO.utilFuns.commonFuns = new CTFO.Util.CommonFuns();
      // 密码编码验证
      $.validator.addMethod("isPasswordCode", function(value, element) {
        var num = CTFO.utilFuns.commonFuns.checkStrong(value);
        if (num < 2) {
          return false;
        } else {
          return true;
        }
      }, "密码太过简单");
      /*
      if(CTFO.config.globalObject.isRichClient){
        setTimeout(function(){
          $.each(CTFO.config.template,function(key,value){
              CTFO.utilFuns.commonFuns.initTmpl(value, function(tmpl) {});
          });
        },1000);
      }
      */
    };

    return {
      init: function(options) {
        that = this;
        document.title = C.SITE_NAME;
        p = $.extend({}, p || {}, options || {});
        if (CTFO.config.globalObject.isMiniHeader) {
          p.headerDiv.addClass("miniheardbox");
          p.headerDiv.find(".user_info").find("p:lt(2)").hide();
        } else {
          p.headerDiv.addClass("heardbox");
        }
        initConfigData();
        //initDefaultConfig();
        //initLoginInfo();
        resize();
        bindEvent();
        return this;
      },
      getHrefParam: function() {
        return arrHrefPrama;
      },
      resize: function() {
        resize();
      },
      changeModel: function(mid, url, ep, isFrame) {
        changeModel(mid, url, ep, isFrame);
      }
    };
  }
  return {
    getInstance: function() {
      if (!uniqueInstance) {
        uniqueInstance = constructor();
      }
      return uniqueInstance;
    }
  };
})();