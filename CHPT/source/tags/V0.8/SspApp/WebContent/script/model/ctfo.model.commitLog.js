/*global CTFO: true, $: true */
/* devel: true, white: false */
/**
 * [ 操作日志功能模块包装器]                                                                                            if(!!data [description]
 * @return {[type]}             [description]
 */
CTFO.Model.CommitLog = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var logGrid = null;
    var commandType = null;
    var commandStatus = null;
    var pageSize = 30;
    var pageSizeOptions = [ 10, 20, 30, 40 ];
    /**
     * [initSelectOptions 初始化两个指令下拉框]
     * @return {[type]} [description]
     */
    var initSelectOptions = function() {
      $.ajax({
        url: CTFO.config.sources.commandStatusCode,
        type: 'POST',
        dataType: 'json',
        data: {},
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if(!!data) {
            commandStatus = data;
            compileOptions(data, $(p.winObj).find('select[name=coStatus]'));
          }
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
      $.ajax({
        url: CTFO.config.sources.commandType,
        type: 'POST',
        dataType: 'json',
        data: {},
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if(!!data) {
            commandType = data;
            compileOptions(data, $(p.winObj).find('select[name=commandTypeCode]'));
          }
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };
    /**
     * [compileOptions 渲染下拉框]
     * @param  {[type]} data      [数据]
     * @param  {[type]} container [容器]
     * @return {[type]}           [description]
     */
    var compileOptions = function(data, container) {
      var options = [];
      $(data).each(function(event) {
        if(this) options.push('<option value="' + this.id + '">' + this.text + '</option>');
      });
      container.append(options.join(''));
    };
    /**
     * [initForm 初始化查询表单]
     * @return {[type]} [description]
     */
    var initForm = function() {

      $(p.winObj).find('input[name=beginDateStr]').val(CTFO.utilFuns.dateFuns.dateFormat(new Date(),"yyyy-MM-dd")+' 00:00').click(function(){
            WdatePicker({
              dateFmt:'yyyy-MM-dd HH:mm',
              isShowClear:false,
              readOnly:true
            });
          });
      $(p.winObj).find('input[name=endDateStr]').val(CTFO.utilFuns.dateFuns.dateFormat(new Date(),"yyyy-MM-dd hh:mm")).click(function(){
            WdatePicker({
              dateFmt:'yyyy-MM-dd HH:mm',
              isShowClear:false,
              readOnly:true
            });
          });
      $(p.winObj).find('.queryButton').click(function(event) {
        searchGrid();
      }).end()
      .find('.exportButton').click(function(){//导出

          cLog.addOperatorLog($.extend({}, CTFO.cache.cLogInfo, {
              opType : '导出操作日志', // 登录/登出系统
              logTypeId : 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作
              logClass : 'CTFO.Model.FrameManager', // 类名称
              logMethod : 'CTFO.utilFuns.commonFuns.exportGrid', // 执行方法
              executeTime : '', // 调用方法执行时间毫秒
              logDesc : '操作成功' // 操作成功/操作失败
            })
          );
          CTFO.utilFuns.commonFuns.exportGrid({
              grid: logGrid,
              url: CTFO.config.sources.commitLogExport
            });
      });
      initSelectOptions();
    };
    var exportGrid = function() {

    };
    /**
     * [searchGrid 查询方法]
     * @return {[type]} [description]
     */
    var searchGrid = function() {
      var d = $(p.winObj).find('form[name=logFrom]').serializeArray(),
        op = [];
      $(d).each(function(event) {
        if(this.value) {
          op.push({name: 'requestParam.equal.' + this.name, value: this.value});
        }
      });
      cLog.addOperatorLog($.extend({}, CTFO.cache.cLogInfo, {
          opType : '查询操作日志', // 登录/登出系统
          logTypeId : 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作
          logClass : 'CTFO.Model.FrameManager', // 类名称
          logMethod : 'searchGrid', // 执行方法
          executeTime : '', // 调用方法执行时间毫秒
          logDesc : '操作成功' // 操作成功/操作失败
        })
      );
      logGrid.setOptions({parms: op});
      logGrid.loadData(true);
    };
    /**
     * [initGrid 初始化表格]
     * @return {[type]} [description]
     */
    var initGrid = function() {
      var gridOptions = {
        columns: [ {
        display: '车牌号',
      name: 'vehicleNo',
      width: 80
        }, {
          display: '指令类型',
          name: 'commandTypeCode',
          width: 100,
          render: function(row) {
            var desc = "";
            $(commandType).each(function() {
              if (this.id == row.commandTypeCode)
                desc = this.text;
            });
            return desc;
          }
        }, {
          display: '指令状态',
          name: 'coStatus',
          width: 110,
          render: function(row) {
            var desc = "";
            $(commandStatus).each(function() {
              if (this.id == row.coStatus)
                desc = this.text;
            });
            return desc;
          }
        }, {
          display: '下发时间',
          name: 'coSutc',
          width: 150,
          render: function(row) {
            return row.coSutc ? CTFO.utilFuns.dateFuns.dateFormat(new Date(+row.coSutc), 'yyyy-MM-dd hh:mm:ss') : '';
          }
        }, {
          display: '指令反馈时间',
          name: 'crTime',
          width: 150,
          render: function(row) {
            return row.crTime ? CTFO.utilFuns.dateFuns.dateFormat(new Date(+row.crTime), 'yyyy-MM-dd hh:mm:ss') : '';
          }
        }, {
          display: '指令描述',
          name: 'coText',
          width: 377,
          align: 'left',
          render: function(row) {
          var html = "";
          if ($.trim(row.commandTypeCode) == "D_CTLM/10") {
              switch (row.coStatus) {
                case "-1":
                  html = '等待回应';
                  break;
                case "0":
                html = '指令下发成功';
                break;
                case "1":
                html = '设备返回失败';
                break;
                case "2":
                html = '指令发送失败';
                break;
                case "3":
                html = '设备不支持此功能';
                break;
                case "4":
                html = '设备不在线';
                break;
                case "5":
                html = '响应超时';
                break;
                case "6":
                html = '开始上传照片';
                break;
                case "7":
                html = '<div><a href="javascript:void(0)" class="showPicture" title="点击查看图片" staffId="'+ row.staffId +'">查看图片(' + row.coText + ')</a></div>';
                break;
                case "8":
                html = '上传照片失败';
                break;
              }
          } else {
            html = row.coText;
          }
          return html;
          }
        } ],
        delayLoad: true,
        sortName: 'vehicleNo',
        sortnameParmName: 'requestParam.equal.sortname',
        sortorderParmName: 'requestParam.equal.sortorder',
        url: CTFO.config.sources.commitLog,
        pageSize: pageSize,
        pageSizeOptions: pageSizeOptions,
        pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
    pagesizeParmName: 'requestParam.rows',
        width: '100%',
        height: 325,
        allowUnSelectRow: true,
        onSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        bindRowAction(eDom, rowData);
    },
        onUnSelectRow: function(rowData, rowIndex, rowDom, eDom) {
      bindRowAction(eDom, rowData);
    }
      };
      logGrid = $(p.winObj).find('.logGrid').ligerGrid(gridOptions);
    };
    /**
     * [bindRowAction 绑定表格操作列的事件]
     * @param  {[type]} eDom      [数据]
     * @param  {[type]} rowData   [行数据]
     * @return {[type]} container [容器]
     */
    var bindRowAction = function(eDom, rowData, container) {
    var actionType = $(eDom).attr('class');
    switch (actionType) {
      case "showPicture":
        findMediaUri(rowData.vehicleNo, rowData.takingseq);
          break;
    }
    };
    /**
     * [findMediaUri 操作日志列表查询图片]
     * @return {[type]} [description]
     */
    var findMediaUri = function(vehicleNo, takingseq) {
      var param = {
        "requestParam.equal.seqStr": takingseq,
        "requestParam.equal.vehicleNo": vehicleNo
      };
      $.ajax({
        url: CTFO.config.sources.findMediaUri,
        type: 'POST',
        dataType: 'json',
        data: param,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if (data.displayMessage) {
          var param = {
              icon: 'ico227',
              title: vehicleNo,
              content: '<image style="width:320px;height:240px" src="' + data.displayMessage.split("@")[1] + '"/>',
              width: 320,
              height: 267,
              onLoad: function(w, d, g) {
              }
            };
          CTFO.utilFuns.tipWindow(param);
          }
        },
        error: function(xhr, textStatus, errorThrown) {
        }
      });
    };

    return {
      init: function(options) {
        p = $.extend({}, p || {}, options || {});
        initForm();
        initGrid();
        $(p.winObj).find('.queryButton').trigger('click');
        return this;
      },
      resize: function() {

      },
      showModel: function() {

      },
      hideModel: function() {

      }
    };
  }
  return {
    getInstance: function() {
      if(!uniqueInstance) {
        uniqueInstance = constructor();
      }
      return uniqueInstance;
    }
  };
})();