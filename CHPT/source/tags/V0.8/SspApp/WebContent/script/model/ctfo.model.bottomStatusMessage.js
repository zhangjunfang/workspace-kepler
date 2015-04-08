/**
 * [页面底部状态栏包装器对象]
 * @author xhui@ctfo.com
 * @return {[Object]}   [页面底部状态栏包装器对象]
 */
CTFO.Model.BottomStatusMessage = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {},
      alarmQueryTimerDelay = 2*60*1000, // 2分钟 告警提示框轮询时间
      loginVehicleQueryTimerDelay = 3*60*1000, // 查询车辆上下线轮询时间
      alarmQueryTimer = null, // 告警提示和在线率轮询对象
      loginVehicleQueryTimer = null, // 车辆上下线轮询对象
      loginVehicleList = [], // 车辆上下线信息数组
      loginVehicleRollTimer = null,
      loginVehicleRollTimerDelay = 2000, // 上下线车辆结果轮询展示时间
      alarmListArr = [], // 告警数据数组
      alarmRollTimer = null,
      alarmRollTimerDelay = 3000, // 告警提示框显示告警信息的轮询时间
      alarmVoiceSwtObj = null,
      alarmVoiceSpan = null,
      alarmState = false,
      alarmButtonsInHide = true;

    /**
     * [initQueryTimer 初始化底部轮询方法]
     * @return {[type]} [description]
     */
    var initQueryTimer = function() {
      startAlarmQueryTimer();
      startLoginVehicleQueryTimer();
    };
    /**
     * [startAlarmQueryTimer 告警提示和在线率轮询]
     * @return {[type]} [description]
     */
    var startAlarmQueryTimer = function() {
      alarmQueryTimer = setInterval(function() {
        getAlarmMessage();
        getVehicleCount();
      }, alarmQueryTimerDelay);
    };
    /**
     * [startLoginVehicleQueryTimer 车辆上下线轮询]
     * @return {[type]} [description]
     */
    var startLoginVehicleQueryTimer = function() {
      loginVehicleQueryTimer = setInterval(function() {
        getLoginVehicle();
      }, loginVehicleQueryTimerDelay);
    };
    /**
     * [getAlarmMessage 查询告警提示框]
     * @return {[type]} [description]
     */
    var getAlarmMessage = function() {
      var param = {
        "requestParam.equal.corpId": CTFO.cache.user.opId,
        "requestParam.rows":5
      };
      $.ajax({
        url: CTFO.config.sources.alarmRealTime,
        type: 'POST',
        dataType: 'json',
        data: param,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if (data && data.Rows && data.Rows.length > 0) {
            alarmListArr = data.Rows;
            alarmState = true;
            alarmBtn();
             rollAlarmList(); // 告警提示框轮询显示告警信息
          } else if (!data || (data.error && data.error.length > 0)) {
            return false;
          }
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };

    /**
     *告警状态按钮
     */
    var alarmBtn = function(){
      if(!alarmState) return false;
      p.container.find('.alarmBox').find('span').removeClass('ico1145').addClass('ico1124');
      p.container.find('.alarmBox').click(function(){
        $(this).find('span').removeClass('ico1124').addClass('ico1145');
        showHideDefaultButtons(this);
      });
    };
    var showHideDefaultButtons = function(o) {
        if(alarmButtonsInHide) {
          $(o).find('.alarmContent').removeClass("none");
          alarmButtonsInHide = false;
          if(o) {
            //当弹出告警提示框在添加关闭事件
            setTimeout(function(){
              $(document.body).click(function(e) {
                  $(o).find('.alarmContent').addClass("none");
                  if(!alarmButtonsInHide) {
                    alarmButtonsInHide = true;
                  }
                  $(document.body).unbind("click");
              });
            },10);
          }
        }
        alarmState = false;
      };
    /**
     * [rollAlarmList 告警提示框轮询显示告警信息]
     * @return {[type]} [description]
     */
    var rollAlarmList = function() {
      
      if (alarmRollTimer) {
        clearInterval(alarmRollTimer);
        alarmRollTimer = null;
      }
      alarmRollTimer = setInterval(function() {
        if (alarmListArr.length > 0) {
          var _alarm = alarmListArr.slice(0,5);
          //if (1 == _alarm.popPoint) {
            p.container.find('.alarmList').html('');
            rollAlarmText(_alarm);
          //}
          $(_alarm).each(function() {
            if (this.voicePoint!="0") {
              if (CTFO.config.globalObject.alarmVoice) {
                playVoice();
              }
            }
          });
        }
      }, alarmRollTimerDelay);
    };
    var rollAlarmText = function(alarm) {
      if (!alarm) return false;
      $(alarm).each(function(index, el) {
        var vNo = CTFO.utilFuns.commonFuns.null2blank(alarm[index].vehicleNo),
        alarmType = CTFO.cache.alarmTypeDesc[alarm[index].alarmCode],
        alarmLevel = alarm[index].alarmLevel;
        if (!vNo) vNo = "未知";
        if (!alarmType) alarmType = "";

        var alarmTime = alarm[index].alarmTime ? CTFO.utilFuns.dateFuns.utc2date(alarm[index].alarmTime).split(' ')[1] : "未知时间";
        
        if( alarmLevel == "0" ){
            var alarmDesc ='<li class="alarmEnd"><span class=" ico1146"></span>' + alarmTime + "&nbsp;&nbsp" + vNo + "&nbsp;&nbsp" + alarmType +'</li>';
        }else{
            var alarmDesc ='<li class="alarmEnd">&nbsp;&nbsp&nbsp;&nbsp&nbsp;' + alarmTime + "&nbsp;&nbsp" + vNo + "&nbsp;&nbsp" + alarmType +'</li>';
        }
        
        p.container.find('.alarmList').append(alarmDesc);
        
      });
      toVehicleMonitor();
    };
    /**
     * [playVoice 声音提示]
     * @return {[type]} [description]
     */
    var playVoice = function() {
    	if(alarmVoiceSwtObj.alarmPrompt){
    		alarmVoiceSwtObj.alarmPrompt(10);
    	}
    };
    /**
     * [toVehicleMonitor 跳转到监控页面]
     * @return {[type]} [description]
     */
    var toVehicleMonitor = function() {
      p.container.find('.alarmContent').find('.alarmMore').unbind("click").bind("click", function() {
        p.menuContainer.find('ol[mid=alarmDetail]').trigger('click');
        var contentDiv = p.contentDiv.find('#alarmDetail');
      setTimeout(function(){
        contentDiv.find('form[name="alarmDetailForm"]').find('span.searchGrid').trigger('click');
      },1000);
      });
    };
    /**
     * [getVehicleCount 查询车辆统计数]
     * @return {[type]} [description]
     */
    var getVehicleCount = function() {
      var param = {
        "entId": CTFO.cache.user.entId
      };
      $.ajax({
        url: CTFO.config.sources.enterpriseStatistic,
        type: 'POST',
        dataType: 'json',
        data: param,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if (!data) return false;
          refreshOnlinePerInfo(data); // 渲染在线车辆信息
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };
    /**
     * [refreshOnlinePerInfo 渲染车辆统计数据]
     * @return {[type]} [description]
     */
    var refreshOnlinePerInfo = function(d) {
      var networkNum = 0, // 入网车辆数
        onlineNum = 0, // 在线车辆数
        drivingNum = 0; // 行驶车辆数
      onlinePer = null; // 在线率
      if (d && d[0]) {
        networkNum = d[0].corpVehicleNetworkNum;
        onlineNum = d[0].corpVehicleOnlineNum;
        drivingNum = d[0].corpVehicleOnlineDrivingNum;
      };
      if (networkNum == 0 || !networkNum) {
        onlinePer = "0%";
      } else {
        onlinePer = (Math.round(onlineNum / networkNum * 1000)) / 10 + "";
        onlinePer = onlinePer.substring(0, (onlinePer.indexOf(".") > 0) ? (onlinePer.indexOf(".") + 2) : onlinePer.length) + "%";
      }
      var vehiclesInfo = "<span class='ico1122'></span><a href='javaScript:void(0);' id='onlineVehicleSearch' title='点击进入【在线车辆查询】' style='color:#fff'>当前在线车辆数：  " + onlineNum + "  &nbsp;&nbsp;在线率：  <span class='c0C0'>" + onlinePer + "</span></a>";
      if( $.inArray( +CTFO.cache.user.entType , [1,2]) > -1 ){
        p.container.find('.onlineVehicleCount').html(vehiclesInfo);
      }
      vehicleToOnline();
    };
    /**
     * [vehicleToOnline 跳转到在线车辆查询页面事件绑定]
     * @return {[type]} [description]
     */
    var vehicleToOnline = function() {
      $('#onlineVehicleSearch').click(function() {
        p.menuContainer.find('ol[mid=onlineVehicleQuery]').trigger('click');
      });
    };
    /**
     * [getLoginVehicle 查询车辆上下线情况]
     * @return {[type]} [description]
     */
    var getLoginVehicle = function() {
      var param = {
        "requestParam.equal.entId": CTFO.cache.user.entId
      };
      $.ajax({
        url: CTFO.config.sources.vehicleOnOffLine,
        type: 'POST',
        dataType: 'json',
        data: param,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if (data && data.Rows && data.Rows.length > 0) {
            $.merge(loginVehicleList, data.Rows);
            rollLoginVehicle();
          } else if (!data || (data.error && data.error.length > 0)) {
            return false;
          }
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };
    /**
     * [rollLoginVehicle 车辆上下线数据轮询]
     * @return {[type]} [description]
     */
    var rollLoginVehicle = function() {
      if (loginVehicleRollTimer) {
        clearInterval(loginVehicleRollTimer);
        loginVehicleRollTimer = null;
      }
      loginVehicleRollTimer = setInterval(function() {
        // 每次轮询将上一次的数据删掉
        rollLoginVehicleText(loginVehicleList.splice(0, 1));
      }, loginVehicleRollTimerDelay);
    };
    var rollLoginVehicleText = function(loginVehicle) {
      if (!loginVehicle || loginVehicle.length < 1) return false;
      var longinText = '' + (loginVehicle[0]["onOffLineInfo"].length !== 0 ? loginVehicle[0]["onOffLineInfo"] : "");//<span class="ico1124"></span>
      p.container.find('.onOffLineText').html(longinText);
    };
    /**
     * [buildAlarmVoice 初始化警报提示音]
     * @return {[isPlay]} [是否开启提示音]
     * @return {[type]} [description]
     */
    var initAlarmVoiveIcon = function( isPlay ){
      if( isPlay ){
          alarmVoiceSpan.removeClass('ico143').addClass('ico142');
          CTFO.config.globalObject.alarmVoice = true;
      }else{
          alarmVoiceSpan.removeClass('ico142').addClass('ico143');
          CTFO.config.globalObject.alarmVoice = false;
      }
    };
    /**
     * [buildAlarmVoice 初始化flash]
     * @return {[type]} [description]
     */
    var buildAlarmVoice = function() {
        var so = new SWFObject("script/util/AlarmPlugins.swf", "alarmVoice", "100%", "100%", "8", "#FF6600");
        so.addVariable("flashVarText", "这里是一个flash,请您下载最新的插件");
        so.addParam("wmode", "transparent");
        so.write("flashVoiceDiv");
        if (so && so.attributes.id) {
          alarmVoiceSwtObj = CTFO.utilFuns.commonFuns.getFlashObj(so.attributes.id);
        }
        // 全局告警声音的控制，放在全局状态栏应该是更合理的 TODO
        alarmVoiceSpan = p.container.find('.alarmVoice').find('span');
        alarmVoiceSpan.click(function() {
          if( $(this).hasClass('ico143') ){
            initAlarmVoiveIcon(true);
          }else{
            initAlarmVoiveIcon(false);
          }
        });

        initAlarmVoiveIcon( CTFO.config.globalObject.alarmVoice );
    };
    /**
     * [initBottomTxt 初始化底部文本]
     * @return {[type]} [description]
     */
    var initBottomTxt = function() {
        p.container.find('.txt1').html( '中交慧联车后业务运营支撑平台 V1.0.0.0' );
        var txt = CTFO.utilFuns.dateFuns.formatDateToTxt();
        p.container.find('.txt2').html( txt );
        var txt = '在线人数' ;
        p.container.find('.txt3').html( txt );
        var txt = CTFO.cache.user.opName + ' ( '+ CTFO.cache.user.opLoginname +' ) '
        p.container.find('.txt5').html( txt );
        getCountOnline( function(onlineCount){
            p.container.find('.txt4').html( onlineCount );
        });
    };


    /**
     * [getCountOnline 获取在线数量]
     * @return {[type]} [description]
     */
    var getCountOnline = function( callback ) {
      $.ajax({
        url: CTFO.config.sources.countOnline,
        dataType: 'json',
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
            callback( data.onlineCount );
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };

    return {
      init: function(options) {
        p = $.extend({}, p || {}, options || {});
        //buildAlarmVoice();
        //initQueryTimer();
        initBottomTxt();
        return this;
      },
      resize: function() {

      },
      showModel: function() {

      },
      hideModel: function() {

      }
    };
  };

  return {
    getInstance: function() {
      if (!uniqueInstance) {
        uniqueInstance = constructor();
      }
      return uniqueInstance;
    }
  };
})();