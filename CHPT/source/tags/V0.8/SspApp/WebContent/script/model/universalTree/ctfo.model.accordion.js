/**
 * [ 手风琴模块]
 * @param {[Object]} 参数对象
 *        {
 *          container: 树容器,
 *          frameHtml: 框架html
 *        }
 * @return {[type]}   [description]
 */
CTFO.Model.Accordion = (function() {
  var constructor = function(options) {
    var p = {
        frameHtml: 'model/template/accordion.html'
      },
      tabContainer = null, // 标签容器对象
      treeContainer = null, // 树内容对象

      orgTreeForm = null, // 组织树form
      vehicleTreeForm = null, // 车辆树form
      previousOpenedAccordion = null, //上一次打开的手风琴

      test = '';

    /**
     * [initMenu 初始化菜单]
     */
    var initMenu = function() {
      var doTtmpl = doT.template($('#accordion_tmpl').html());
      menuList.l = 0;
      var menus = CTFO.cache.menus;
      menuIteration(menus, doTtmpl, p.menuContainer);
      bindMenuEvent();
    };

    /**
     * [bindMenuEvent 绑定菜单事件]
     */
    var bindMenuEvent = function() {
      var subHover = p.headerDiv.find('ul.accordion li');
      subHover.find('ul.subaccordion').addClass('none');
      var currentShowModel = CTFO.cache.currentShowModel;
      //展示当前模块
      setTimeout(function() {
        subHover
          .find('ul.subaccordion')
          .find("ol[mid='" + currentShowModel + "']")
          .addClass('tit4_twins')
          .parent()
          .prev()
          .trigger('click');

        var node = subHover
          .find('ul.subaccordion')
          .find("ol[mid='" + currentShowModel + "']");
        var node = node.parent().parent();
        node.siblings('li').addClass('none');

      }, 0);

      subHover.find('ul.subaccordion ol').hover(
        function() {
          $(this).addClass('tit4');
        },
        function() {
          $(this).removeClass('tit4');
        }
      );
      /*
      p.headerDiv.find('.firstLevelAccordion').toggle(function(event) {
        $(this).next().removeClass('none');
        $(this).find("span").removeClass('ico146').addClass('ico145');
      },function(event) {
        $(this).next().addClass('none');
        $(this).find("span").removeClass('ico145').addClass('ico146');
      });*/
      p.headerDiv.find('.firstLevelAccordion').bind('click', function() {

        if ($(this).find("span").hasClass('ico146')) {
          if (previousOpenedAccordion && previousOpenedAccordion != $(this)) {
            $(previousOpenedAccordion).next().addClass('none');
            $(previousOpenedAccordion).find("span").removeClass('ico145').addClass('ico146');
          }
          $(this).next().removeClass('none');
          $(this).find("span").removeClass('ico146').addClass('ico145');
          previousOpenedAccordion = $(this);
        } else {
          $(this).next().addClass('none');
          $(this).find("span").removeClass('ico145').addClass('ico146');
        }

      });
      p.headerDiv.find('ul.subaccordion > ol').bind('click', function(e) {
        var mid = $(this).attr('mid'),
          auth = $(this).attr('auth');
        if (currentShowModel === mid) return false;
        if (mid) { // 这里通过判断是否有mid属性来判断是否是可加载子模块的菜单，点击一级菜单通常默认出发其下的第一个子菜单，首页除外
          CTFO.cache.frame.changeModel(mid, '', null, 0);
        }
        e.stopPropagation();
      });
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
          menuIteration(m, doTtmpl, $(p.mainDiv).find('.accordion .li_' + m.l + "_" + i));
        }
      });
    };

    var loadFrameHtml = function() {
      CTFO.utilFuns.commonFuns.initTmpl(p.frameHtml, function(tmpl) {
        var doTtmpl = doT.template(tmpl);
        p.container.append(doTtmpl())
        p.menuContainer = p.mainDiv = p.headerDiv = p.container.find('.treeContentWrap');
        initMenu();
        resize();
        bindEvent();
      });
    };
    var bindEvent = function() {};
    var resize = function() {
      var th = $(p.container).outerHeight() - $(tabContainer).outerHeight();
      $(treeContainer).height(th);
    };
    this.init = function(options) {
      p = $.extend({}, p || {}, options || {});
      loadFrameHtml();
    };
    this.resize = resize;
    this.showModel = function() {

    };
    this.hideModel = function() {

    };
    // 构造器执行代码
    this.init(options);
  };

  return constructor;

})();