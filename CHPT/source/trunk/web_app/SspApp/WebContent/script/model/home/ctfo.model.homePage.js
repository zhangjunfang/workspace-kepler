/*global CTFO: true, $: true */
/* devel: true, white: false */
/**
 * [ 首页功能模块包装器]
 * @return {[type]}     [description]
 */
CTFO.Model.HomePage = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var minH = 645; // 本模块最低高度
    var MessageGrid = null; //信息反馈列表
    var SystemNoticeGrid = null; //系统公告列表
    var entId = CTFO.cache.user.entId;
    var opProvince = CTFO.cache.user.opProvince;
    var boxList = null;


    /**
     * [initBoxContent 初始化首页5个box]
     * @param  {[Array]} boxObjList [box数组]
     * @return {[Null]}            [无返回]
     */

    var initBoxContent = function() {
        //if(!CTFO.cache.user.entId) return false;
        queryData(boxList[0]);
        queryData(boxList[1]);
        queryData(boxList[2]);
      };
    /**
     * [queryData 查询数据]
     * @param  {[Object]} p [参数对象]
     * @return {[Null]}   [无返回]
     */
    var queryData = function(pd) {
        $.ajax({
          url: pd.url,
          type: 'POST',
          dataType: 'json',
          data: pd.param,
          complete: function(xhr, textStatus) {
            //called when complete
          },
          success: function(data, textStatus, xhr) {
            if(typeof data != 'undefined' && !(data instanceof Array)) data = [data];
            if(data && pd.fn_query) pd.fn_query(data, pd.tmpl, pd.tmplContainer, pd.boxContainer, pd.fn_bind);
          },
          error: function(xhr, textStatus, errorThrown) {
            //called when there is an error
            //$.ligerDialog.error( pd.url + textStatus);
          }
        });
      };
    /**
     * [compileBox 通过模板生成box的内容]
     * @param  {[Object]}   d             [数据]
     * @param  {[Object]}   tmpl          [模板html]
     * @param  {[Object]}   tmplContainer [模板内容填充对象]
     * @param  {[Object]}   boxContainer  [box容器对象]
     * @param  {Function}   fn            [box事件绑定回调函数]
     * @return {[Null]}                   [无返回]
     */
    var compileBox = function(d, tmpl, tmplContainer, boxContainer, fn) {
        if(!d || !tmpl || !tmplContainer) return false;
        // tmpl = tmpl.html();
        var doTtmpl = doT.template(tmpl);
        tmplContainer.append(doTtmpl(d[0]));
        if(fn) fn(boxContainer);
      };

      /**
       * [bindTodoEvent 绑定待办事项事件]
       * @param  {[Object]} boxContainer [box容器对象]
       * @return {[Null]}              [无返回]
       */
      var bindSysInfo = function(boxContainer) {
          boxContainer.find('.itemBox').unbind('click').click(function(event) {
            //var noId = $(this).attr('noId');
            //var isQuery = $(this).attr('isQuery');
            var mid = $(this).attr('mid');
            //var param={"noId":noId,"isQuery":isQuery};
            //CTFO.cache.frame.changeModel(mid,'',param,false);
            CTFO.cache.frame.changeModel( mid, '', null, 0);
          });
        };

      /**
       * [bindTodoEvent 绑定待办事项事件]
       * @param  {[Object]} boxContainer [box容器对象]
       * @return {[Null]}              [无返回]
       */
      var bindTodoEvent = function(boxContainer) {
          boxContainer.find('.itemBox').unbind('click').click(function(event) {
            //var noId = $(this).attr('noId');
            //var isQuery = $(this).attr('isQuery');
            var mid = $(this).attr('mid');
            //var param={"noId":noId,"isQuery":isQuery};
            //CTFO.cache.frame.changeModel(mid,'',param,false);
            CTFO.cache.frame.changeModel( mid, '', null, 0);
          });
        };
    /**
     * [showDetail 显示box中的记录的详情]
     * @return {[Null]} [无返回]
     */
    var showDetail = function(param) {
        // todo
        $.ajax({
          url: param.url,
          type: 'POST',
          dataType: 'json',
          data: {
            id: param.id,
            // timestamp: new Date().getTime(),
            replyId: param.replyId
          },
          complete: function(xhr, textStatus) {
            //called when complete
          },
          success: function(data, textStatus, xhr) {
            if(!data || data.error) return false;
            d = data[0];
            var p = {
              title: param.title,
              url: param.template,
              data: d, 
              width: param.width,
              height: param.height,
              onLoad: param.onLoad
            };
            CTFO.utilFuns.tipWindow(p);

          },
          error: function(xhr, textStatus, errorThrown) {
            //called when there is an error
          }
        });


      };

    /**
     * [bindCorpNewsBoxEvent 绑定企业资讯box事件]
     * @param  {[Object]} boxContainer [box容器对象]
     * @return {[Null]}              [无返回]
     */
    var bindCorpNewsBoxEvent = function(boxContainer) {
          boxContainer.find('.itemBox').unbind('click').click(function(event) {
            //var noId = $(this).attr('noId');
            //var isQuery = $(this).attr('isQuery');
            var mid = $(this).attr('mid');
            //var param={"noId":noId,"isQuery":isQuery};
            //CTFO.cache.frame.changeModel(mid,'',param,false);
            CTFO.cache.frame.changeModel( mid, '', null, 0);
          });
      };

    var bindEvent = function() {
      };
    var resize = function(ch) {
        if(ch < minH) ch = minH;
        p.mainContainer.height(ch);
      };
    return {
      init: function(options) {
        p = $.extend({}, p || {}, options || {});
        /**
         *数据渲染内容塔器
         */
        boxList = [
        { // 企业资讯 0
           url: CTFO.config.sources.basicInfo,
           param: {
        	   opId: CTFO.cache.user.opId
           },
           tmpl: $('#corp_basic_info_tmpl').html(),
           tmplContainer: $(p.mainContainer).find('.corpNewsContent'),
           boxContainer: $(p.mainContainer).find('.corpNews'),
           fn_query: function(d, tmpl, tmplContainer, boxContainer, fn_bind) {
             compileBox(d, tmpl,  tmplContainer, boxContainer, fn_bind); // $('#corp_news_tmpl')
           },
           fn_bind: function(boxContainer) {
             bindCorpNewsBoxEvent(boxContainer);
           }
         },{ // 车辆节能排行 1 
           url: CTFO.config.sources.sysInfo,
           param: {
        	   opId: CTFO.cache.user.opId
           },
           tmpl: $('#corp_system_info_tmpl').html(),
           tmplContainer: $(p.mainContainer).find('.vehicleRankingContent'),
           boxContainer: $(p.mainContainer).find('.vehicleRanking'),
           fn_query: function(d, tmpl, tmplContainer, boxContainer, fn_bind) {
             compileBox(d, tmpl, tmplContainer, boxContainer, fn_bind); // $("#vehicle_ranking_tmpl")
           },
           fn_bind: function(boxContainer) {
             bindSysInfo(boxContainer);
           }
         }, { // 待办事项 2
            url: CTFO.config.sources.todoAuth,
            param: {
         	   opId: CTFO.cache.user.opId
            },
            tmpl: $('#corp_todo_tmpl').html(),
            tmplContainer: $(p.mainContainer).find('.systemNoticeContent'),
            boxContainer: $(p.mainContainer).find('.systemNotice'),
            fn_query: function(d, tmpl, tmplContainer, boxContainer, fn_bind) {
             tmplContainer.html('');
             compileBox(d, tmpl, tmplContainer, boxContainer, fn_bind);
            },
            fn_bind: function(boxContainer) {
             bindTodoEvent(boxContainer);
            }
         }];


        bindEvent();
        initBoxContent();
        resize(p.cHeight);
        return this;
      },
      resize: function(ch) {
        resize(ch);
      },
      showModel: function() {
        $(p.mainContainer).show();
      },
      hideModel: function() {
        //$(p.mainContainer).remove();
        $(p.mainContainer).hide();
      }
    };
  }
  return {
    getInstance: function() {
      /*if(!uniqueInstance) {
        uniqueInstance = constructor();
      }
      return uniqueInstance;*/
      return constructor();
    }
  };
})();