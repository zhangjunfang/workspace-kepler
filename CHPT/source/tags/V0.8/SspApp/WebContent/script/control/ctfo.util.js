/**
 * [ 通用编码管理器]
 * @auth fanshine124@gmail.com                                                                                                alert("初始化codeManager数据失败 " +                e);                                }                                                } [description]
 * @return {[type]}          [description]
 */
CTFO.Util.CodeManager = (function() {
  var uniqueInstance;

  function constructor() {
    var areaKey = "SYS_AREA_INFO";
    var loaded = false;
    var codeCache = null;

    var queryGeneralCode = function() {
      loaded = true;
      codeCache = CTFO.cache.generalCode;
      /*
      $.ajax({
        url: CTFO.config.sources.generalCode,
        type: "POST",
        data: {
          timestamp: new Date().getTime()
        },
        dataType: "json",
        cache: false,
        success: function(data, err) {
          codeCache = $.extend({}, generalCode || {},  (data) || {});
          CTFO.cache.generalCode = codeCache;
          loaded = true;
        },
        error: function(e, s) {
          alert("初始化codeManager数据失败 " + e);
        }
      });*/
    };

    var queryAlarmDesc = function() {
      if (!CTFO.config.globalObject.isLoadAlarmTypeDesc) return;
      $.get(CTFO.config.sources.alarmTypeDesc + '?timestamp=' + new Date().getTime(), null, function(data, textStatus, xhr) {
        if (data && data.length > 0) {
          $(data).each(function(event) {
            CTFO.cache.alarmTypeDesc[this.alarmCode] = this.alarmName;
          });
        }
      }, 'json');
    };
    /**
     * [queryUserCorps 查询企业信息列表]
     * @return {[type]} [description]
     */
    var queryUserCorps = function() {
      if (!CTFO.config.globalObject.isLoadCorpList) return;
      $.ajax({
        url: CTFO.config.sources.drivdingCorpList,
        type: 'POST',
        dataType: 'json',
        data: null,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          CTFO.cache.userCorps = data;
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };


    return {
      init: function() {
        queryGeneralCode();
        queryAlarmDesc();
        queryUserCorps();
        return this;
      },
      getAlarmDesc: function(code) {
        return CTFO.cache.alarmTypeDesc[code] || '';

      },
      /**
       * [queryAlarmLevel 查询告警级别]
       * @param  {[String/Object]}   param     [告警级别编码或查询参数对象, 如果是参数对象, 则必须有code属性]
       * @param  {Function} callback [回调函数]
       * @return {[type]}            [description]
       */
      queryAlarmLevel: function(param, callback) {
        var code = param;
        if (typeof(param) === 'object') {
          code = param.code.toString();
        }
        if (!code) return false;
        if (CTFO.cache.alarmType[code]) {
          if (callback) callback(CTFO.cache.alarmType[code], param);
          return false;
        }
        $.ajax({
          url: CTFO.config.sources.alarmLevel,
          type: 'POST',
          dataType: 'json',
          data: {
            'requestParam.equal.levelId': +code,
            'requestParam.equal.entId': ($.inArray(+CTFO.cache.user.entType, [1, 2]) > -1) ? CTFO.cache.user.entId : 1,
          },
          complete: function(xhr, textStatus) {
            //called when complete
          },
          success: function(data, textStatus, xhr) {
            if (!!data) {
              CTFO.cache.alarmType[code] = data;
              if (callback) callback(data, param);
            }
          },
          error: function(xhr, textStatus, errorThrown) {
            //called when there is an error
          }
        });
      },
      // TODO 考虑是否提供直接渲染告警类型dom的方法
      // compileAlarmTypeDom: function(code, fillObj, callback) {
      //   var data = CTFO.cache.alarmLevel[code];
      //   if (data) {
      //     this.compileAlarmTypeHtml(data, fillObj);
      //   } else {
      //     this.queryAlarmLevel(code, function() {
      //       compileAlarmTypeHtml(code, fillObj);
      //     });
      //   }
      // },
      // compileAlarmTypeHtml: function(data, fillObj) {
      //   var options = [],
      //     alarmCodeArr = [];
      //   $(data).each(function(event) {
      //     var op = '<option value="' + this.alarmCode + '">' + this.alarmName + '</option>';
      //     options.push(op);
      //     alarmCodeArr.push(this.alarmCode);
      //   });
      //   options.unshift('<option value="' + alarmCodeArr.join(',') + '">全部</option>');
      //   $(fillObj).html(options.join(''));
      // },
      /**
       * @description 生成下拉框选项
       * @param {Object}
       *            key
       * @param {Object}
       *            container
       */
      getSelectList: function(key, container, defaultValue, tip) {
        if (loaded && key && codeCache[key]) {
          var tip = tip || "请选择...";
          var options = ['<option value="" title="' + tip + '">' + tip + '</option>'];
          $(codeCache[key]).each(function() {
            var selected = (defaultValue && defaultValue == this.code) ? 'selected' : '';
            options.push('<option value="' + this.code + '" title="' + this.name + '" ' + selected + '>' + this.name + '</option>');
          });
          $(container).html('').append(options.join(''));
        }
      },
      /**
       * @description 根据编码查询对应名字
       * @param {Object}
       *            key
       * @param {Object}
       *            code
       */
      getNameByCode: function(key, code) {
        var text = '';
        key = key ? key : areaKey;
        if (loaded && key && code && codeCache[key]) {
          $(codeCache[key]).each(function() {
            if (code == this.code) {
              text = this.name;
            }
          });
        }
        return text;
      },
      /**
       * @description 根据编码查询对应城市名
       * @param {Object}
       *            key
       * @param {Object}
       *            pCode
       * @param {Object}
       *            cCode
       */
      getCityProvcodeNameByCode: function(key, pCode, cCode) {
        var text = '';
        key = key ? key : areaKey;
        if (loaded && key && pCode && cCode && codeCache[key]) {
          $(codeCache[key]).each(function() {
            if (pCode == this.code) {
              var c = this.children;
              $(c).each(function() {
                if (cCode == this.code) {
                  text = this.name;
                }
              });
            }
          });
        }
        return text;
      },
      /**
       * @description 获取省份以及城市的名称 省+城市
       * @param {Object}
       *            pCode
       * @param {Object}
       *            cCode
       * @param {Object}
       *            areaGeneralCode
       */
      getCityAndProvcodeNameByCode: function(pCode, cCode, areaGeneralCode) {
        var str = "--";
        if (pCode && cCode && areaGeneralCode) {
          $(areaGeneralCode).each(function() {
            if (pCode == this.code) {
              str = this.name;
              var c = this.children;
              $(c).each(function() {
                if (cCode == this.code) {
                  str += this.name;
                }
              });
            }
          });
        }
        return str;
      },
      /**
       * @description 根据编码查询对应城市名
       * @param {Object}
       *            key
       * @param {Object}
       *            code
       */
      getCityNameByCode: function(key, code) {
        var text = '';
        key = key ? key : areaKey;
        if (loaded && key && code && codeCache[key]) {
          $(codeCache[key]).each(function() {
            var c = this.children;
            $(c).each(function() {
              if (code == this.code) {
                text = this.name;
              }
            });
          });
        }
        return text;
      },
      /**
       * @description 生成省份下拉框
       * @param {Object}
       *            container
       */
      getProvinceList: function(container, pCode) {
        if (codeCache[areaKey]) {
          var options = ['<option value="" title="请选择...">请选择...</option>'];
          $(codeCache[areaKey]).each(function() {
            var selected = (pCode == this.code) ? 'selected' : '';
            options.push('<option value="' + this.code + '" title="' + this.name + '" ' + selected + '>' + this.name + '</option>');
          });
          $(container).html('').append(options.join(''));
        }
      },
      /**
       * @description 生成城市下拉框
       * @param {Object}
       *            pCode
       * @param {Object}
       *            container
       */
      getCityList: function(pCode, container, cityval) {
        var options = ['<option value="" title="请选择...">请选择...</option>'];
        if (pCode && codeCache[areaKey]) {
          $(codeCache[areaKey]).each(function() {
            if (pCode == this.code) {
              var c = this.children;
              $(c).each(function() {
                var selected = (cityval == this.code) ? 'selected' : '';
                options.push('<option value="' + this.code + '" title="' + this.name + '" ' + selected + '>' + this.name + '</option>');
              });
            }
          });
        }
        $(container).html('').append(options.join(''));
      },
      /**
       * @description 生成县城下拉框
       * @param {Object} pCode     [省份 Id ]
       * @param {Object} cityval   [城市 Id]
       * @param {Object} container [dom 节点]
       * @param {Object} countyval   [县城 Id]
       */
      getCountyList: function(pCode, cityval, container, countyval) {
        var options = ['<option value="" title="请选择...">请选择...</option>'];
        if (pCode && cityval && codeCache[areaKey]) {
          $(codeCache[areaKey]).each(function() {
            if (pCode == this.code) { // 如果有这个省份
              var c = this.children;
              $(c).each(function() {
                if (cityval == this.code) { // 如果有这个城市
                  var c = this.children;
                  $(c).each(function() {
                    var selected = (countyval == this.code) ? 'selected' : '';
                    options.push('<option value="' + this.code + '" title="' + this.name + '" ' + selected + '>' + this.name + '</option>');
                  });
                }
              });
            }
          });
        }
        $(container).html('').append(options.join(''));
      },
      /**
       * @description 取县城名称
       * @param {Object} pCode     [省份 Id ]
       * @param {Object} cityval   [城市 Id]
       * @param {Object} countyval   [县城 Id]
       */
      getCountyName: function(pCode, cityval, countyval) {
        var text = '';
        if (pCode && cityval && codeCache[areaKey]) {
          $(codeCache[areaKey]).each(function() {
            if (pCode == this.code) { // 如果有这个省份
              text += this.name;
              var c = this.children;
              $(c).each(function() {
                if (cityval == this.code) { // 如果有这个城市
                  text += this.name;
                  var c = this.children;
                  $(c).each(function() {
                    if (countyval == this.code) {
                      text += this.name;
                    }
                  });
                }
              });
            }
          });
        }
        return text;
      },
      /**
       * @description 生成联动的省市县下拉框
       * @param {dom}		provinceOption	省
       * @param {dom}		cityOption		市
       * @param {dom}		countyOption	县
       * @param {string}	provinceVal		省值
       * @param {string}	cityVal			市值
       * @param {string}	countyVal		县值
       */
      initProvAndCityAndCounty: function(provinceOption, cityOption, countyOption, provinceVal, cityVal, countyVal) {

        provinceOption.html('');
        cityOption.html('');
        countyOption.html('');
        //生成省
        this.getProvinceList(provinceOption, provinceVal);

        //联动市
        if (provinceVal && cityVal) {
          this.getCityList(provinceVal, cityOption, cityVal);
        }
        provinceOption.change(function() {
          CTFO.utilFuns.codeManager.getCityList(provinceOption.val(), cityOption);
          CTFO.utilFuns.codeManager.getCountyList(provinceOption.val(), cityOption.val(), countyOption);
        });

        //联动县
        if (provinceVal && cityVal && countyVal) {
          this.getCountyList(provinceVal, cityVal, countyOption, countyVal);
        }

        cityOption.change(function() {
          CTFO.utilFuns.codeManager.getCountyList(provinceOption.val(), cityOption.val(), countyOption);
        });

      },
      /**
       * @description 生成联动的省市下拉框
       * @param {Object}
       *            pContainer
       * @param {Object}
       *            cContainer
       * @param {String}
       *            cityval
       * @param {String}
       *            provinceval
       */
      getProvAndCity: function(pContainer, cContainer, cityval, provinceval) {
        var that = this;
        if (codeCache[areaKey]) {
          this.getProvinceList(pContainer, provinceval);
          if (cContainer && provinceval && cityval) this.getCityList(provinceval, cContainer, cityval);
          if (cContainer)
            $(pContainer).change(function() {
              var pCode = $(this).val();
              that.getCityList(pCode, cContainer, cityval);
            });
        }
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

/**
 * [Date 日期转换函数集合]
 */
CTFO.Util.Date = function() {
  /*把秒转换成天:小时:分钟:秒的js方法*/
  this.secondToDate = function(second, displaycount) {
    if (!second) {
      return 0;
    }
    var time = '';
    if (second >= 24 * 3600) {
      time += parseInt((second / 24) / 3600) + ':';
      second %= 24 * 3600;
    }
    if (second >= 3600) {
      time += parseInt(second / 3600) + ':';
      second %= 3600;
    }
    if (second >= 60) {
      time += parseInt(second / 60) + ':';
      second %= 60;
    }
    if (second > 0) {
      time += second;
    }
    var arr = time.split(":");
    var day = "",
      h = "",
      min = "",
      sec = "";
    var str = "";
    if (arr.length == 1) {
      sec = arr[0] < 10 ? "0" + arr[0] : arr[0];
      //str=displaycount==4?("00:00:00:"+sec):("00:00:"+sec);
      str = sec + "秒";
    }
    if (arr.length == 2) {
      min = arr[0] < 10 ? "0" + arr[0] : arr[0];
      sec = arr[1] < 10 ? "0" + arr[1] : arr[1];
      //str=displaycount==4?("00:00:"+min+":"+sec):("00:"+min+":"+sec);
      str = min + "分" + sec + "秒";
    }
    if (arr.length == 3) {
      h = arr[0] < 10 ? "0" + arr[0] : arr[0];
      min = arr[1] < 10 ? "0" + arr[1] : arr[1];
      sec = arr[2] < 10 ? "0" + arr[2] : arr[2];
      //str=displaycount==4?("00:"+h+":"+min+":"+sec):(h+":"+min+":"+sec);
      str = h + "小时" + min + "分" + sec + "秒";
    }
    if (arr.length == 4) {
      day = arr[0] < 10 ? "0" + arr[0] : arr[0];
      h = arr[1] < 10 ? "0" + arr[1] : arr[1];
      min = arr[2] < 10 ? "0" + arr[2] : arr[2];
      sec = arr[3] < 10 ? "0" + arr[3] : arr[3];
      //str=displaycount==4?(day+":"+h+":"+min+":"+sec):h+":"+min+":"+sec;
      str = day + "天" + h + "小时" + min + "分" + sec + "秒";
    }
    return str;
  };
  //获取当前日期的上个月
  this.getFirstMonth = function() {
    var date = new Date();
    var mon = date.getMonth();
    var lastmon = 0;
    var year = parseInt(date.getFullYear(), 10);
    if (mon == 0) {
      year = year - 1;
      lastmon = 12;
    } else {
      lastmon = mon;
    }
    if (lastmon < 10) {
      lastmon = "0" + lastmon;
    }
    return year + "-" + lastmon;
  };
  //获取当前日期的上个月
  this.getNowMonth = function() {
    var date = new Date();
    var mon = date.getMonth();
    var lastmon = 0;
    if (mon == 0) {
      lastmon = 1;
    } else {
      lastmon = mon + 1;
    }
    var year = date.getFullYear();
    if (lastmon < 10) {
      lastmon = "0" + lastmon;
    }
    return year + "-" + lastmon;
  };
  //获取月的最后一天日期
  this.getMonthLastDay = function(dat) {
    var year = dat.substring(0, 4);
    var month = dat.substring(5, 7);
    var new_year = Number(year);
    var new_month = Number(month);
    new_month++;
    if (new_month > 12) {
      new_month -= 12;
      new_year++;
    }
    var new_date = new Date(new_year, month, 1, 0, 0, 0);
    var date = (new Date(new_date.getTime() - 1000 * 60)).getDate();
    if (date.length == 1) {
      date = "0" + date;
    }
    dat = dat + "-" + date;
    return dat;
  };
  //获取月的第一天日期
  this.getMonthFirstDay = function(dat) {
    var year = dat.substring(0, 4);
    var month = dat.substring(5, 7);
    var new_year = Number(year);
    var new_month = Number(month);
    new_month++;
    if (new_month > 12) {
      new_month -= 12;
      new_year++;
    }
    var new_date = new Date(new_year, month, 1, 0, 0, 0);
    var date = (new Date(new_date.getTime())).getDate();
    if (date == 1) {
      date = "0" + date;
    }
    dat = dat + "-" + date;
    return dat;
  };
  //获取当前日期的前1个月
  this.getFirstMonthDate = function() {
    var date = new Date();
    var mon = date.getMonth();
    var lastmon = mon;
    var year = date.getFullYear();
    var date = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
    if (lastmon == 0) {
      lastmon = 12;
      year = year - 1;
    } else if (lastmon < 0) {
      lastmon = 12 + lastmon;
      year = year - 1;
    }
    if (lastmon < 10) {
      lastmon = '0' + lastmon;
    }
    return year + "-" + lastmon + "-" + date;
  };
  //获取当前日期的前3个月
  this.getFirstThreeMonth = function() {
    var date = new Date();
    var mon = date.getMonth();
    var lastmon = mon - 2;
    var year = date.getFullYear();
    var date = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
    if (lastmon == 0) {
      lastmon = 12;
      year = year - 1;
    } else if (lastmon < 0) {
      lastmon = 12 + lastmon;
      year = year - 1;
    }
    if (lastmon < 10) {
      lastmon = '0' + lastmon;
    }
    return year + "-" + lastmon + "-" + date;
  };
  //取今天 前一周的时间
  this.getTodayPrecedeWeek = function() {
    var now = new Date();
    var ls = new Date(now.getTime() - 1000 * 60 * 60 * 24 * 7);
    var date = ls.getFullYear() + "-" + (ls.getMonth() + 1) + "-" + ls.getDate();
    return date;
  };
  //取今天前15天的时间
  this.getTodayTenWeek = function() {
    var now = new Date();
    var ls = new Date(now.getTime() - 1000 * 60 * 60 * 24 * 14);
    var date = ls.getFullYear() + "-" + (ls.getMonth() + 1) + "-" + ls.getDate();
    return date;
  };
  //获取昨天的月初时间
  this.getYesterdayPrecedeMonth = function() {
    var now = new Date();
    var ls = new Date(now.getTime() - 1000 * 60 * 60 * 24);
    //兼容foxfail，必须使用getFullYear()；
    var date = ls.getFullYear() + "-" + (ls.getMonth() + 1) + "-1";
    return date;
  };
  //获取下一年的当天时间
  this.getNextYearCurrentDay = function() {
    var now = new Date();
    var ls = new Date(now.getTime() + 1000 * 60 * 60 * 24 * 365);
    var date = ls.getFullYear() + "-" + (ls.getMonth() + 1) + "-" + ls.getDate();
    return date;
  };
  //获取下一年的下一天时间
  // num 秒数
  this.getNextYearTommorow = function(num) {
    if (num) {
      var now = new Date(num);
    } else {
      var now = new Date();
    }
    var ls = new Date(now.getTime() + 1000 * 60 * 60 * 24 * 365 + 1000 * 60 * 60 * 24);
    var date = ls.getFullYear() + "-" + (ls.getMonth() + 1) + "-" + ls.getDate();
    return date;
  };

  //毫秒转换成**小时**分**秒
  this.MillisecondToDate = function(msd) {
    var time = parseFloat(msd) / 1000;
    if (null != time && "" != time) {
      if (time > 60 && time < 60 * 60) {
        var minuts = parseInt(time / 60.0);
        minuts = minuts > 10 ? minuts : '0' + minuts;
        var second = parseInt((parseFloat(time / 60.0) -
          parseInt(time / 60.0)) * 60);
        second = second > 10 ? second : '0' + second;
        time = "00:" + minuts + ":" + second;
      } else if (time >= 60 * 60 && time < 60 * 60 * 24) {
        var hour = parseInt(time / 3600.0);
        hour = hour > 10 ? hour : '0' + hour;
        var minuts = parseInt((parseFloat(time / 3600.0) -
          parseInt(time / 3600.0)) * 60);
        minuts = minuts > 10 ? minuts : '0' + minuts;
        var second = parseInt((parseFloat((parseFloat(time / 3600.0) - parseInt(time / 3600.0)) * 60) -
          parseInt((parseFloat(time / 3600.0) - parseInt(time / 3600.0)) * 60)) * 60);
        second = second > 10 ? second : '0' + second;
        time = hour + ":" + minuts + ":" + second;
      } else {
        var second = parseInt(time);
        second = second > 10 ? second : '0' + second;
        time = "00:00:" + second;
      }
    } else {
      time = "00:00:00";
    }
    return time;
  };
  //获取明天时间
  this.getTomorrow = function(num) {
    if (num) {
      var now = new Date(num);
    } else {
      var now = new Date();
    }
    var ls = new Date(now.getTime() + 1000 * 60 * 60 * 24);
    var date = ls.getFullYear() + "-" + (ls.getMonth() + 1) + "-" + ls.getDate();
    return date;
  };
  //获取昨天时间所在月的1号
  this.getCurMonthFirstDay = function() {
    var now = new Date();
    var ls = new Date(now.getTime() - 1000 * 60 * 60 * 24);
    var date = ls.getFullYear() + "-" + (ls.getMonth() + 1) + "-01";
    return date;
  };
  //获取昨天时间
  this.getYesterday = function() {
    var now = new Date();
    var ls = new Date(now.getTime() - 1000 * 60 * 60 * 24);
    var date = ls.getFullYear() + "-" + (ls.getMonth() + 1) + "-" + ls.getDate();
    return date;
  };
  //获取今天时间
  this.getToday = function() {
    var ls = new Date();
    var mon = ls.getMonth() + 1;
    var date = ls.getDate() < 10 ? '0' + ls.getDate() : ls.getDate();
    if (mon < 10) {
      mon = '0' + mon;
    }
    var date = ls.getFullYear() + "-" + (mon) + "-" + date;
    return date;
  };
  /*
   * DATE时间格式化 创建时间：2011/11/22 10:12 创建者：zhangming
   *
   * 示例： var dateUTC = strToDate("2011-11-22 10:12");
   * dateFormat(dateUTC,"yyyy-MM-dd hh:mm"); 返回：2011-11-22 10:12
   */
  this.dateFormat = function(date, format) {
    var o = {
      "M+": date.getMonth() + 1,
      "d+": date.getDate(),
      "h+": date.getHours(),
      "m+": date.getMinutes(),
      "s+": date.getSeconds(),
      "q+": Math.floor((date.getMonth() + 3) / 3),
      "S": date.getMilliseconds()
    };
    if (/(y+)/.test(format)) {
      format = format.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
      if (new RegExp("(" + k + ")").test(format)) {
        format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
      }
    }
    return format;
  };
  this.date2utc = function(date) {
    if (!date) {
      return '';
    }
    var d = new Date(date.replace(/-/g, "/"));
    if (!d) {
      return '';
    }
    return d.getTime();
  };
  this.utc2date = function(n_utc) {
    if (!n_utc || n_utc === "null" || n_utc === "无" || +n_utc === 0) return "";
    var date = new Date();
    date.setTime((parseInt(n_utc, 10) + (8 * 3600 * 1000)));
    var s = date.getUTCFullYear() + "-";
    if ((date.getUTCMonth() + 1) < 10) {
      s += "0" + (date.getUTCMonth() + 1) + "-";
    } else {
      s += (date.getUTCMonth() + 1) + "-";
    }
    if (date.getUTCDate() < 10) {
      s += "0" + date.getUTCDate();
    } else {
      s += date.getUTCDate();
    }
    if (date.getUTCHours() < 10) {
      s += " 0" + date.getUTCHours() + ":";
    } else {
      s += " " + date.getUTCHours() + ":";
    }
    if (date.getMinutes() < 10) {
      s += "0" + date.getUTCMinutes() + ":";
    } else {
      s += date.getUTCMinutes() + ":";
    }
    if (date.getUTCSeconds() < 10) {
      s += "0" + date.getUTCSeconds();
    } else {
      s += date.getUTCSeconds();
    }

    return s;
  };

  this.formatDateToTxt = function() {
    var date = new Date();
    var s = date.getUTCFullYear() + "年";
    if ((date.getUTCMonth() + 1) < 10) {
      s += "0" + (date.getUTCMonth() + 1) + "月";
    } else {
      s += (date.getUTCMonth() + 1) + "月";
    }
    if (date.getUTCDate() < 10) {
      s += "0" + date.getUTCDate() + "日";
    } else {
      s += date.getUTCDate() + "日";
    }
    if (date.getUTCHours() < 10) {
      s += " 0" + date.getUTCHours() + ":";
    } else {
      s += " " + date.getUTCHours() + ":";
    }
    if (date.getMinutes() < 10) {
      s += "0" + date.getUTCMinutes();
    } else {
      s += date.getUTCMinutes();
    }
    /*if (date.getUTCSeconds() < 10) {
      s += ":" + "0" + date.getUTCSeconds();
    } else {
      s += ":" + date.getUTCSeconds();
    }*/


    var week;
    if (date.getDay() == 0) week = "星期日";
    if (date.getDay() == 1) week = "星期一";
    if (date.getDay() == 2) week = "星期二";
    if (date.getDay() == 3) week = "星期三";
    if (date.getDay() == 4) week = "星期四";
    if (date.getDay() == 5) week = "星期五";
    if (date.getDay() == 6) week = "星期六";

    s += ' ';
    s += week;
    return s;
  };
  this.time2utc = function(c_date) {
    var tempArray = c_date.split(":");
    var hour = tempArray[0] * 3600000;
    var minute = tempArray[1] * 60000;
    var second = tempArray[2] * 1000;
    return parseInt(hour, 10) + parseInt(minute, 10) + parseInt(second, 10);
  };
  /**
   * [daysBetween 获得两个日期字符串之间的天数差]
   * @param  {[String]} startDate [传入的开始日期]
   * @param  {[String]} endDate [传入的结束日期]
   * @param  {[boolean]} requiredAbs [是否需要取绝对值,false 用于判断时间先后]
   * @param  {[float]} ratio [时间差系数,默认是8640000,表示一天]
   * @author liulin 2013/01/29
   * @return {[Integer]}     [差值]
   */
  this.daysBetween = function(startDate, endDate, requiredAbs, ratio) {
    //系数,默认为天数
    var quotient = 86400000;
    if (!!ratio && parseFloat(ratio) > 0) {
      quotient = ratio;
    }
    var cha = (Date.parse(startDate.replace(/\-/g, "/")) - Date.parse(endDate.replace(/\-/g, "/")));
    if (requiredAbs) {
      return Math.abs(cha) / quotient;
    } else {
      return cha / quotient;
    }
  };
  this.startDateBigerThanEndDate = function(startDate, endDate) {
    if (!startDate || startDate.length < 1 || !endDate || endDate.length < 1) return true;
    var startDateTemp = startDate.split(" "),
      endDateTemp = endDate.split(" "),
      arrStartDate = startDateTemp[0].split("-"),
      arrEndDate = endDateTemp[0].split("-"),
      arrStartTime = startDateTemp[1].split(":"),
      arrEndTime = endDateTemp[1].split(":"),

      allStartDate = new Date(arrStartDate[0], parseInt(arrStartDate[1], 10) - 1, parseInt(arrStartDate[2], 10), arrStartTime[0], arrStartTime[1], arrStartTime[2]),
      allEndDate = new Date(arrEndDate[0], parseInt(arrEndDate[1], 10) - 1, parseInt(arrEndDate[2], 10), arrEndTime[0], arrEndTime[1], arrEndTime[2]);

    return allStartDate.getTime() >= allEndDate.getTime();
  };
};

/**
 * [CommonFuns 通用函数集合]
 */
CTFO.Util.CommonFuns = function() {
  this.FilterData = function(data, isTime) {
    var result = "";
    var NumData = Number(data);
    if (NumData == 999999999) {
      result = "--";
    } else {
      if (isTime) {
        result = this.formatTime(data);
      } else {
        result = data;
      }
    }
    return result;
  };
  /**
   * [checkPwdMode 验证的格式]
   * @param {[type]} d []
   */
  this.checkPwdMode = function(value) {
    var reg = new RegExp("^[_#!$@\%\^\&\*\(\)\>\<\/\\da-zA-Z0-9]*$", "g");
    var reg2 = new RegExp("^[0-9]*$", "g");
    var reg3 = new RegExp("^[a-zA-Z]*$", "g");
    var reg4 = new RegExp("^[_#!@$\%\^\&\*\(\)\>\<\/]*$", "g");
    var rs = value.search(reg);
    var rs2 = value.search(reg2);
    var rs3 = value.search(reg3);
    var rs4 = value.search(reg4);
    return (rs != -1 && rs2 == -1 && rs3 == -1 && rs4 == -1) ? true : false;
  };
  //判断输入密码的类型
  this.CharMode = function(iN) {
      if (iN >= 48 && iN <= 57) //数字
        return 1;
      if (iN >= 65 && iN <= 90) //大写
        return 2;
      if (iN >= 97 && iN <= 122) //小写
        return 4;
      else
        return 8;
    }
    //bitTotal函数
    //计算密码模式
  this.bitTotal = function(num) {
      modes = 0;
      for (i = 0; i < 4; i++) {
        if (num & 1) modes++;
        num >>>= 1;
      }
      return modes;
    }
    //返回强度级别
  this.checkStrong = function(sPW) {
    if (sPW.length < 6)
      return 0; //密码太短
    Modes = 0;
    for (i = 0; i < sPW.length; i++) {
      //密码模式
      Modes |= this.CharMode(sPW.charCodeAt(i));
    }
    return this.bitTotal(Modes);
  }

  /**
   * [getTmpl 取得模板文件字符串]
   * @param {[type]} d []
   */
  this.getTmpl = function(url) {
    var tmpl = CTFO.cache.tmpl[url];
    if (!tmpl) {
      var tmpl = $.ajax({
        url: url,
        async: false,
        cache: true
      }).responseText;
      CTFO.cache.tmpl[url] = tmpl;
    }
    return tmpl;
  };
  /**
   * [getTmpl 取得模板文件字符串]
   * @param {[type]} d []
   */
  this.initTmpl = function(url, callback) {
    var tmpl = CTFO.cache.tmpl[url];
    if (!tmpl) {
      $.ajax({
        url: url,
        async: true,
        cache: true,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(tmpl) {
          CTFO.cache.tmpl[url] = tmpl;
          if (callback) callback(tmpl);
        },
        error: function(xhr, textStatus, errorThrown) {}
      });
    } else {
      callback(tmpl);
    }
  };
  /**
   * [initData 取得模板文件字符串]
   * @param {[type]} d []
   */
  this.initData = function(param) {
    var d = CTFO.cache.data[param.url];
    if (!d || param.reload) {
      $.ajax({
        url: param.url,
        type: 'POST',
        dataType: 'json',
        data: param.data,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if (data && data.Rows && data.Rows.length > 0) {
            CTFO.cache.data[param.url] = data.Rows;
            if (param.success) param.success(data.Rows);
          }
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    } else {
      param.success(d);
    }
  };
  /**
   * [checkUpFileSize 检查上传文件的大小]
   * @param {[type]} d []
   */
  this.checkUpFileSize = function(fileInput, limitSize) {
    if (!fileInput) return;
    var limitSize = limitSize || 1024 * 1024,
      size = 0;
    if (fileInput.files && fileInput.files[0]) {
      var size = fileInput.files[0].size;
    } else {
      try {
        fileInput.select();
        var url = document.selection.createRange().text;
        var fso = new ActiveXObject("Scripting.FileSystemObject");
        var size = fso.GetFile(url).size;
      } catch (e) {
        //alert('如果你用的是ie 请将安全级别调低！');  
      }
    }
    if (size >= limitSize) {
      return true;
    } else {
      return false;
    }
  };
  /**
   * [getClinetHeight 得到浏览器的高度]
   * @param  {[int]} offet [传入的参数]
   * @return {[type]}     [description]
   */
  this.getClinetHeight = function(offet) {
    var clientHeight = $.browser.opera ? document.body.clientHeight : document.documentElement.clientHeight;
    return clientHeight - offet;
  };
  /**
   * [isTransEntUser 是否运输企业用户]
   * @return {[type]}     [description]
   */
  this.isTransEntUser = function() {
    return $.inArray(+CTFO.cache.user.entType, [1, 2]) > -1;
  };
  /**
   * [clearString 去除脚本中的特殊字符，对字符串进行过滤是在数据前后台交互中必备的。]
   * @param  {[String]} s [传入的参数]
   * @return {[type]}     [description]
   */
  this.clearString = function(s) {
    var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）&;|{}【】‘；：”“'。，、？]");
    var rs = "";
    for (var i = 0, l = s.length; i < l; i++) {
      rs = rs + s.substr(i, 1).replace(pattern, '');
    }
    return rs;
  };
  /**
   * [hasForbiddenChar 检查脚本中的特殊字符]
   * @param  {[String]} s [传入的参数]
   * @return {[type]}     [description]
   */
  this.hasForbiddenChar = function(s) {
    var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）&;|{}【】‘；：”“'。，、？]");
    var flag = false;
    for (var i = 0, l = s.length; i < l; i++) {
      flag = pattern.test(s.substr(i, 1));
      if (flag) break;
    }
    return flag;
  };
  /**
   * [validateCharLength 判断中英文混合字符串的长度]
   * @param  {[String]} str [传入的参数]
   * @author liulin 2013/01/16
   * @return {[type]}     [description]
   */
  this.validateCharLength = function(str) {
    var l = 0;
    var chars = str.split("");
    for (var i = 0, len = chars.length; i < len; i++) {
      if (chars[i].charCodeAt(0) < 299) l++;
      else l += 2;
    }
    return l;
  };
  this.isTel = function(str) {
    var istel = /^[0-9]\d{10}$/g.test(str);
    return istel;
  };
  /**
   * [isInt 是否整型数字验证]
   * @param  {[String]}  str [传入的参数]
   * @return {Boolean}     [是否整型数字]
   */
  this.isInt = function(str) {
    if (!str) return false;
    var reg = /^(-|\+)?\d+$/;
    return reg.test(str);
  };
  /**
   * [isInt 是否浮点型数字验证]
   * @param  {[String]}  str [传入的参数]
   * @return {Boolean}     [是否浮点型数字]
   */
  this.isFloat = function(str) {
    if (!str) return false;
    var reg = /^(-|\+)?\d+(\.\d+)?$/;
    return reg.test(str);
  };
  /**
   * [isTime 短时间格式验证]
   * @param  {[String]}  str [传入的参数]
   * @return {Boolean}     [是否形如:12:12:12格式的时间字符串]
   */
  this.isTime = function(str) {
    if (!str) return false;
    var a = str.match(/^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/);
    //var a = str.match(/^\d{1,2}:\d{1,2}:\d{1,2}$/);
    if (!a) {
      return false;
    }
    if (a[1] > 24 || a[3] > 60 || a[4] > 60) {
      return false;
    }
    return true;
  };
  //格式化时间,将秒格式化为：时分秒格式
  this.formatTime = function(Stime) {
    var result = "";
    var isNegative = false;
    var NumTime = Number(Stime);
    if (NumTime < 0) {
      NumTime = Math.abs(NumTime);
      isNegative = true;
    }
    var hours = NumTime / 3600;
    hours = Math.floor(hours);
    if (hours != 0) {
      if (hours < 10) {
        result = "0" + hours + ":";
      } else {
        result = hours + ":";
      }
    } else {
      result = "00:";
    }
    var minus = NumTime % 3600;
    var minu = minus / 60;
    minu = Math.floor(minu);
    if (minu != 0) {
      if (minu < 10) {
        result = result + "0" + minu + ":";
      } else {
        result = result + minu + ":";
      }
    } else {
      result = result + "00:";
    }
    var second = minus - minu * 60;
    if (second != 0) {
      if (second < 10) {
        result = result + "0" + second;
      } else {
        result = result + second;
      }
    } else {
      result = result + "00";
    }
    if (isNegative) {
      result = "-" + result;
    }
    return result;
  };
  /**
   * [isDate 日期格式验证]
   * @param  {[String]}  str [传入的参数]
   * @return {Boolean}     [是否形如:2013-01-01格式的时间字符串]
   */
  this.isDate = function(str) {
    var r = str.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
    if (!r) return false;
    var d = new Date(r[1], r[3] - 1, r[4]);
    return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4]);
  };
  /**
   * [isTimeStamp 日期加时间格式验证]
   * @param  {[String]}  str [传入的参数]
   * @return {Boolean}     [是否形如:2013-01-01 12:12:12格式的时间字符串]
   */
  this.isTimeStamp = function(str) {
    var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
    var r = str.match(reg);
    if (!r) return false;
    var d = new Date(r[1], r[3] - 1, r[4], r[5], r[6], r[7]);
    return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4] && d.getHours() == r[5] && d.getMinutes() == r[6] && d.getSeconds() == r[7]);
  };
  /**
   * [hoursBetween 获取两个时间字符串之间的分钟差]
   * @param  {[String]} startDate [被减数]
   * @param  {[String]} endDate   [减数]
   * @return {[int]}           [返回整形分钟数]
   */
  this.hoursBetween = function(startDate, endDate) {
    if (!startDate) {
      return -1;
    }
    if (!endDate) {
      return -1;
    }
    var sdate = startDate.split(":"),
      edate = endDate.split(":"),
      sHour = parseInt(sdate[0], 10),
      sMinute = parseInt(sdate[1], 10),
      sSecond = parseInt(sdate[2], 10),
      eHour = parseInt(edate[0], 10),
      eMinute = parseInt(edate[1], 10),
      eSecond = parseInt(edate[2], 10),

      sminuts = new Date(0, 0, 0, sHour, sMinute, sSecond).getTime(),
      // 开始的时间
      eminuts = new Date(0, 0, 0, eHour, eMinute, eSecond).getTime(); // 结束的时间
    return parseInt((eminuts - sminuts) / 60000, 10); // 返回整形分钟数
  };

  /***
   * 根据当前日期，获得前15天的日期
   */
  this.getFifteenDay = function() {
    var now = new Date();
    var ls = new Date(now.getTime() - 1000 * 60 * 60 * 24 * 14);
    var month = ls.getMonth() + 1;
    var date = ls.getDate();
    var s = "";
    if (month < 10) {
      month = "0" + month;
    }
    if (date < 10) {
      date = "0" + date;
    }
    var date = ls.getYear() + "-" + month + "-" + date;
    return date;
  };

  this.trim = function(str) { //删除左右两端的空格
    return str.replace(/(^\s*)|(\s*$)/g, "");
  };

  this.ltrim = function(str) { //删除左边的空格
    return str.replace(/(^\s*)/g, "");
  };

  this.rtrim = function(str) { //删除右边的空格
    return str.replace(/(\s*$)/g, "");
  };

  /**
   * 初始化查询后台得到的下拉框选项
   *
   * @param {Object}
   *            p
   * @param {String}
   *            p.url 请求后台的url
   * @param {String}
   *            p.container 注入的容器
   * @param {String}
   *            p.code code字段名
   * @param {String}
   *            p.name 描述字段名
   * @param {String}
   *            p.key
   *            返回json对象的关键字，如：{"SYS_TERMINAL_OEM":[{"children":null,"code":"E005","name":"成都亿盟恒信科技有限公司"},{"children":null,"code":"E006","name":"深圳市华宝电子科技有限公司"}]}
   *            "SYS_TERMINAL_OEM"就是key，如果没有key该参数可以不传
   */
  this.initSelectsFromServer = function(p) {
    $.ajax({
      url: p.url,
      type: 'post',
      dataType: 'json',
      data: p.params,
      error: function() {
        // alert('Error loading json document');
      },
      success: function(r) {
        var options = ['<option value="" title="请选择...">请选择...</option>'];
        var d = (r && r[p.key]) ? r[p.key] : r;
        $(d).each(function() {
          var selectedStr = '';
          if (this[p.code || 'code'] == p.selectedCode)
            selectedStr = 'selected';
          options.push('<option value="' + this[p.code || 'code'] + '" title="' + this[p.name || 'name'] + '" ' + selectedStr + '>' + this[p.name || 'name'] + '</option>');
        });

        $(p.container).html('').append(options.join(''));
      }
    });
  };
  /***
   * 去除请选择的下拉菜单
   */
  this.initNoSelectsFromServer = function(p) {
    $.ajax({
      url: p.url,
      type: 'post',
      dataType: 'json',
      data: p.params,
      error: function() {
        // alert('Error loading json document');
      },
      success: function(r) {
        var options = [];
        var d = r[p.key] ? r[p.key] : r;
        $(d).each(function() {
          var selectedStr = '';
          if (this[p.code || 'code'] == p.selectedCode)
            selectedStr = 'selected';
          options.push('<option value="' + this[p.code || 'code'] + '" title="' + this[p.name || 'name'] + '" ' + selectedStr + '>' + this[p.name || 'name'] + '</option>');
        });

        $(p.container).html('').append(options.join(''));
      }
    });
  };

  this.getCityByLngLat = function(lng, lat, rType, callback) {
    if (!lng || !lat) return false;
    var request = {
        location: lng + ' ' + lat,
        type: 1
      },
      gc = new TMServiceGC();
    gc.rgcEncoding(request, function(r) {
      if (+r.resultCode !== 1) return false;
      var city = (rType === 'code' ? r.resultInfo.city.code : r.resultInfo.city.name);
      if (city && callback) callback(city);
    });
  };
  // 根据城市名获取行政编码
  // this.getCityCodeByName = function (city) {
  //   if (!city) return false;
  //   var request = {
  //       city: city
  //     },
  //     cs = new TMServiceCity();
  //   cs.onLoadCity(request, function (r) {
  //     r.getPlace();
  //   });
  // };
  /**
   * [ 根据经纬度获取位置描述公用方法, 加fillObjId参数，是因为在批量监控窗口中，循环获取多个车辆的位置信息，填充的dom对象会混淆，暂时用id替代]
   * @param  {[type]}  lng        [description]
   * @param  {[type]}  lat        [description]
   * @param  {[type]}  type       [description]
   * @param  {[type]}  fillObj         [description]
   * @param  {Boolean} isJustFill [description]
   * @param  {[type]}  fillObjId  [description]
   * @return {[type]}             [description]
   */
  this.getAddressByLngLat = function(lng, lat, type, fillObj, isJustFill, vid) {
    if (!lng || !lat) return false;
    type = type || 6;
    var request = {
      location: lng + ' ' + lat,
      type: type
    };
    var gc = new TMServiceGC();
    (function(_vid) {
      gc.rgcEncoding(request, function(result) {
        if (+result.resultCode === 1) {
          var text = '';
          var r = result.resultInfo;
          if (r.point) {
            if (r.point.name) text += r.point.address + r.point.name;
          } else {
            if (r.district) text += r.district.city.name + r.district.county.name;
            if (r.road) text += r.road.name;
          }
          if (!text) text = "未知位置";
          if (_vid) {
            CTFO.cache.batchTrackingLocationData[_vid] = text;
          } else {
            if (isJustFill) {
              $(fillObj).attr("title", text).html(text);
            } else {
              $(fillObj).append('<span></span>').addClass("cutText").text(text).attr("title", text);

              var showTimer = setTimeout(function() {
                $(fillObj).find('span').remove();
                $(fillObj).text('获取位置');
                $(fillObj).show();
                clearTimeout(showTimer);
              }, 300000);
            }
          }
        }
      });
    })(vid);
  };
  this.getAddressByLngLat2 = function(lng, lat, type, eDom) {
    $.ajax({
      url: CTFO.config.sources.findAddressBySync,
      type: 'POST',
      dataType: 'json',
      data: {
        'requestParam.equal.lon': lng,
        'requestParam.equal.lat': lat
      },
      complete: function(xhr, textStatus) {
        //called when complete
      },
      success: function(data, textStatus, xhr) {
        $(eDom).append('<span></span>').addClass("cutText").text(data.address).attr("title", data.address);
      },
      error: function(xhr, textStatus, errorThrown) {
        //called when there is an error
      }
    });
  };
  this.getVehicleLatestInfo = function(p) {
    var param = {
      'requestParam.equal.enableFlag': 1
    };
    if (p.vid)
      param['requestParam.equal.vid'] = p.vid;
    if (p.vehicleNo)
      param['requestParam.equal.vehicleNo'] = p.vehicleNo;


    if (p.vehicleNo == "车牌号" || p.vehicleNo == "" || CTFO.utilFuns.commonFuns.hasForbiddenChar(p.vehicleNo)) {
      $.ligerDialog.alert("请输入正确车牌号", "提示", "error");
      return false;
    }

    $.ajax({
      url: CTFO.config.sources.getVehicleLatestInfo,
      type: 'POST',
      dataType: 'json',
      data: param,
      complete: function(xhr, textStatus) {
        //called when complete
      },
      success: function(data, textStatus, xhr) {
        if (data && data.length > 0 && data[0].maplon && p.callback) p.callback(data[0]);
        else $.ligerDialog.error('地图范围内没有找到该车辆');
      },
      error: function(xhr, textStatus, errorThrown) {
        //called when there is an error
      }
    });

  };
  /**
   * [getAlarmLevelIcon 获取告警级别图标]
   * @param  {[String]} code [告警级别编码]
   * @return {[String]}      [告警级别图标路径]
   */
  this.getAlarmLevelIcon = function(code) {
    var alarmTypeImg = "img/alarmLevel/level" + code + ".png";
    if (!code || ('012').indexOf(code) < 0)
      alarmTypeImg = "img/alarmLevel/level0.png";
    if (code == "stop")
      alarmTypeImg = "img/alarmLevel/stopPoint.png";
    return alarmTypeImg;
  };
  /**
   * [getColorDesc 获取车牌颜色描述]
   * @param  {[String]} color [颜色值]
   * @return {[String]}       [颜色描述]
   */
  this.getColorDesc = function(color) {
    var ca = ["蓝色", "黄色", "黑色", "白色"];
    return ca[color - 1] || (+color === 9 ? "其他" : "");
  };
  this.oldDirection = 0; // 上一次的轨迹方向
  this.getCarDirectionIconByLngLat = function(lonlatArr, direction, ifOnline) {
    var icon = "img/vehicleDirection/";
    var x1 = parseFloat(lonlatArr[0]),
      y1 = parseFloat(lonlatArr[1]),
      x2 = parseFloat(lonlatArr[2]),
      y2 = parseFloat(lonlatArr[3]);
    if (ifOnline === "true" || +ifOnline === 1)
      icon += "online-";
    else if (ifOnline === "false" || +ifOnline === 0)
      icon += "offline-";

    if (Math.abs(direction - this.oldDirection) < 30) {
      direction = this.oldDirection;
    }
    this.oldDirection = direction;
    if (15 > direction || direction >= 345)
      icon += "0";
    else if (15 <= direction && direction < 45)
      icon += "30";
    else if (45 <= direction && direction < 75)
      icon += "60";
    else if (75 <= direction && direction < 105)
      icon += "90";
    else if (105 <= direction && direction < 135)
      icon += "120";
    else if (135 <= direction && direction < 165)
      icon += "150";
    else if (165 <= direction && direction < 195)
      icon += "180";
    else if (195 <= direction && direction < 225)
      icon += "210";
    else if (225 <= direction && direction < 255)
      icon += "240";
    else if (255 < direction && direction < 285)
      icon += "270";
    else if (285 <= direction && direction < 315)
      icon += "300";
    else if (315 <= direction && direction < 345)
      icon += "330";
    else
      icon += "0";
    icon += ".png";
    return icon;
  };
  /**
   * [getCarDirectionIcon 获取车辆行驶方向图标]
   * @param  {[Integer]} direction      [方向值]
   * @param  {[Integer]} ifOnline       [是否在线, 0:离线,1:在线]
   * @param  {[String]} markerIconType [图标类型] * 注释该参数
   * @param  {[String]} alarmStatus    [告警状态]
   * @param  {[Integer]} speed          [速度]
   * @return {[String]}                [图标路径]
   */
  this.getCarDirectionIcon = function(direction, ifOnline, alarmStatus, speed, location, markerIconType) {
    var icon = "img/vehicleDirection/";
    if (CTFO.cache.vehicleMarkerType === 2) icon += 'v-'; // 切换车辆图标
    var d = parseFloat(direction);
    if (d === 360) d = 0;
    // if(d >= 0 && d <= 90)
    //   direction =  90 - d;
    // else
    //   direction = 90 - d + 360;

    // if (markerIconType === 'cluster') icon += "c-";

    if (ifOnline === 'false' || +ifOnline === 0) {
      icon += "offline-";
    } else if (ifOnline === 'true' || +ifOnline === 1) {
      if (!location && typeof(location) === 'undefined') icon += "c-online-";
      else if (+alarmStatus === 1) icon += "alarm-";
      else if (+speed > 5) icon += "online-";
      else icon += "stopping-";
    }

    if (15 > direction || direction >= 345)
      icon += "0";
    else if (15 <= direction && direction < 45)
      icon += "30";
    else if (45 <= direction && direction < 75)
      icon += "60";
    else if (75 <= direction && direction < 105)
      icon += "90";
    else if (105 <= direction && direction < 135)
      icon += "120";
    else if (135 <= direction && direction < 165)
      icon += "150";
    else if (165 <= direction && direction < 195)
      icon += "180";
    else if (195 <= direction && direction < 225)
      icon += "210";
    else if (225 <= direction && direction < 255)
      icon += "240";
    else if (255 < direction && direction < 285)
      icon += "270";
    else if (285 <= direction && direction < 315)
      icon += "300";
    else if (315 <= direction && direction < 345)
      icon += "330";
    else
      icon += "0";
    icon += ".png";
    return icon;
  };
  /**
   * [getCarDirectionDesc 获取车辆方向描述]
   * @param  {[Integer]} direction  [方向值]
   * @param  {[Boolean]} deflection [纠偏,有2种方向值,0度指向正北和指向正东]
   * @return {[type]}            [description]
   */
  this.getCarDirectionDesc = function(direction, deflection) {
    var desc = "";
    direction = parseFloat(direction, 10);
    if (direction === 360) direction = 0;
    // if(deflection){
    //   if(direction >= 0 &&  direction <= 90)
    //     direction =  90 - direction;
    //   else
    //     direction = 90 - direction + 360;
    // }
    if (255 < direction && direction <= 285)
      desc = "正西";
    else if (285 < direction && direction <= 345)
      desc = "西北";
    else if (345 < direction || direction <= 15)
      desc = "正北";
    else if (15 < direction && direction <= 75)
      desc = "东北";
    else if (75 < direction && direction <= 105)
      desc = "正东";
    else if (105 < direction && direction <= 165)
      desc = "东南";
    else if (165 < direction && direction <= 195)
      desc = "正南";
    else if (195 < direction && direction <= 255)
      desc = "西南";
    else
      desc = "未知";
    return desc;
  };
  /**
   * [initScheduleMessage 初始化预设消息]
   * @param  {[Object]} fillObj [填充Dom对象]
   * @return {[type]}         [description]
   */
  this.initScheduleMessage = function(fillObj) {
    if (!CTFO.cache.schedulePreMessage || CTFO.cache.schedulePreMessage.length < 1) return false;
    var options = ["<option value='' >请选择...</option>"];
    $(CTFO.cache.schedulePreMessage).each(function(event) {
      var op = "<option value='" + this.msgBody + "' >" + this.msgIdx + "</option>";
      options.push(op);
    });
    $(fillObj).append(options.join(''));
  };
  this.validateText = function(text) {
    var flag = true,
      reg = '/,{,},(,)'.split(',');
    if (!text) flag = false;
    for (var l = reg.length - 1; l > 0; l--) {
      if (text.indexOf(reg[l]) > -1) {
        flag = false;
        break;
      }
    }
    return flag;
  };
  /**
   * [null2blank 字符串格式化]
   * @param  {[String]} [字符串]
   * @return {[String]} [字符串]
   */
  this.null2blank = function(value) {
    return (!value || value.toLowerCase() === "null") ? '' : value;
  };
  this.getFlashObj = function(movieName) {
    var chartRef = null;
    if (navigator.appName.indexOf("Microsoft Internet") == -1) {
      if (document.embeds && document.embeds[movieName])
        chartRef = document.embeds[movieName];
      else
        chartRef = window.document[movieName];
    } else {
      chartRef = window[movieName];
    }
    if (!chartRef)
      chartRef = document.getElementById(movieName);
    return chartRef;
  };
  /**
   * [Commands grid查询时清空分页查询条件]
   */
  this.resetGrid = function(gridName) {
    gridName.options.newPage = 1;
    gridName.options.page = 1;
  };

  /**
   * [ 导出表格]
   * @param  {[Object]} grid [grid对象，用来获取grid的相关参数]
   * @return {[type]}       [description]
   */
  this.exportGrid = function(eparam) {
    if (!eparam.grid || typeof eparam.grid !== 'object') return false;
    if (eparam.grid.getData().length === 0) {
      $.ligerDialog.alert("请确认查询结果不为空", "信息提示", 'warn');
      return false;
    }
    var maxRows = 1500, // 最多导出条数限制
      isLoading = false;
    //   param = {
    //     param: eparam.grid.options.parms,
    //     columns: eparam.grid.options.columns,
    //     pageSize: eparam.grid.options.pageSize,
    //     sortName: eparam.grid.options.sortName,
    //     sortOrder: eparam.grid.options.sortOrder
    //   };
    // if (eparam.isActiveMonitorVehicles)
    //   param = {
    //     "requestParam.equal.idArrayStr": eparam.vids
    //     // "requestParam.equal.DataHeader": ''
    //   };
    var winParam = {
      width: 300,
      height: 120,
      title: '导出向导',
      url: CTFO.config.template.exportWindow,
      onLoad: function(w, d, g) {
        initExportGridWindow(w, d, g);
      },
      data: eparam
    };
    var win = $('<div>');
    $(win).applyCtfoWindow(winParam);

    var initExportGridWindow = function(w, d, g) {
      var param = {
        param: d.grid.options.parms,
        columns: d.grid.options.columns,
        pageSize: d.grid.options.pageSize,
        sortName: d.grid.options.sortName,
        sortOrder: d.grid.options.sortOrder
      };
      $(w).find('.exportButton').click(function(event) {
        if (isLoading) return false;

        var errorTip = $(w).find('.errorTip'),
          fromPage = $(w).find('input[name=fromPage]').val(),
          toPage = $(w).find('input[name=toPage]').val(),
          pages = (+toPage) - (+fromPage) + 1,
          rows = pages * param.pageSize;
        toPage = parseInt(toPage, 10);
        fromPage = parseInt(fromPage, 10);
        if (!fromPage || !toPage || isNaN(fromPage) || isNaN(toPage)) {
          errorTip.text('输入的页码值需为整数');
          return false;
        }
        if (toPage < fromPage) {
          errorTip.text('结束页码值需大于起始页码值');
          return false;
        }
        if (rows > maxRows) {
          errorTip.text('导出数据过量, 请重新输入导出范围<br>请不要一次性导出' + maxRows + '条以上的数据');
          return false;
        }
        isLoading = true;
        errorTip.text('正在导出, 请耐心等待...');
        var ep = [{
          name: 'requestParam.page',
          value: fromPage
        }, {
          name: 'requestParam.pagesize',
          value: pages
        }, {
          name: 'requestParam.rows',
          value: param.pageSize
        }, {
          name: param.sortName,
          value: param.sortOrder || 'asc'
        }, {
          name: 'requestParam.equal.sortname',
          value: param.sortName
        }, {
          name: 'requestParam.equal.sortorder',
          value: param.sortOrder || 'asc'
        }];
        var columnsText = d.columnsTextWanted || getExportParam(param.columns);
        if (d.isActiveMonitorVehicles) {
          ep = {
            "requestParam.equal.idArrayStr": d.vids,
            "requestParam.equal.DataHeader": columnsText
          };
        } else {
          ep.push({
            name: 'exportDataHeader',
            value: columnsText
          });
          $.merge(ep, param.param);
        }
        exportGridEvent(d.url, errorTip, ep);
      });
    };
    var exportGridEvent = function(url, errorTip, ep) {
      $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        data: ep,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if ((data && data.error) || (data && data.msg === 'error')) {
            errorTip.text('导出出错');
            return false;
          }
          var path = data.msg || '';
          if (!path) errorTip.text('导出出错');
          errorTip.html('<a href="javascript:void(0)" onclick="document.getElementById(\'exportGridFrame\').src=\'' + path + '\';">请点击下载</a>');
        },
        error: function(xhr, textStatus, errorThrown) {
          errorTip.text('导出出错');
        },
        timeout: 300000
      });
    };
    var getExportParam = function(columns) {
      var columnsText = [];
      $(columns).each(function(event) {
        var forbidenColumns = ['操作', '图表', '序号', '违规照片', '位置/轨迹', '油量变化详情'],
          ctext = this.display;
        if (!this.hide) {
          if ($.inArray(ctext, forbidenColumns) < 0) {
            // 处理合并表头的第二列，_1代表第一行，_2代表第二行
            var multiColumns = this.columns,
              clip = (multiColumns ? '_1=' : '=');
            columnsText.push(this.name + clip + ctext);
            if (multiColumns) {
              $(multiColumns).each(function(event) {
                if (!this.hide) {
                  columnsText.push(this.name + '_2=' + this.display);
                }
              });
            }
          }
        }
      });
      return columnsText.join('&');
      // return {name: 'exportDataHeader', value: columnsText.join('&')};
    };
  };
  //登录页面第一次加载grid时调用该方法，目的是：查询数据库获取该用户保存的自定义列数据，如果该用户已经保存过，则按照数据库的数据显示自定义列；
  //如果数据库未保存，则显示grid中所有的列，排除frozen不是true，showType是hidden的列；
  this.getCustomColumns = function(id, gridColumns) {
    var columnArray = null;
    var columnNow = [];
    $.ajax({
      url: CTFO.config.sources.findPageDisColumn + '?reportId=' + id,
      type: 'POST',
      dataType: 'json',
      async: false, // 同步
      complete: function(xhr, textStatus) {
        //called when complete
      },
      success: function(data, textStatus, xhr) {
        columnArray = data;
        var columnCustom = [];
        var columnOper = [];
        if (columnArray && columnArray.length > 0) {
          for (var i = 0; i < gridColumns.length; i++) {
            if (gridColumns[i].frozen) {
              columnNow.push(gridColumns[i]);
            } else if (gridColumns[i].operate) {
              columnOper.push(gridColumns[i]);
            } else {
              for (var j = 0; j < columnArray.length; j++) {
                if (gridColumns[i] && gridColumns[i].name && columnArray[j].codeId && gridColumns[i].name === columnArray[j].codeId) {
                  columnCustom.push(gridColumns[i]);
                  break;
                }
              }
            }
          }
          var isOk = false;
          var index = 0;
          var pointer = 0;
          for (var m = 0; m < columnArray.length; m++) {
            for (var n = 0; n < columnCustom.length; n++) {
              if (columnArray[m].codeId && columnCustom[n] && columnCustom[n].name && columnArray[m].codeId === columnCustom[n].name) {
                isOk = true;
                index = n;
                break;
              }
            }

            if (isOk) {
              var linshi = columnCustom[pointer];
              columnCustom[pointer] = columnCustom[index];
              columnCustom[index] = linshi;
              pointer++;
              isOk = false;
            }
          }
          columnNow = columnNow.concat(columnCustom, columnOper);
        } else {
          for (var i = 0; i < gridColumns.length; i++) {
            if (gridColumns[i].frozen) {
              columnNow.push(gridColumns[i]);
            } else if (gridColumns[i].operate) {
              columnNow.push(gridColumns[i]);
            } else {
              if (gridColumns[i].showType && gridColumns[i].showType === 'hidden') {
                continue;
              } else {
                columnNow.push(gridColumns[i]);
              }
            }
          }
        }
      },
      error: function(xhr, textStatus, errorThrown) {
        //called when there is an error
      }
    });
    return columnNow;
  };
  /**
   * [ 设置自定义列]
   * @param  {[String]} id [模块id]
   * @return {[type]}    [description]
   */
  this.setCustomColumns = function(id, grid, allColumns) {
    if (!id) return false;
    var winParam = {
      width: 450,
      height: 360,
      title: '自定义列',
      url: CTFO.config.sources.customColumns + '?reportId=' + id,
      onLoad: function(w, d, g) {
        initCustomColumnsWindow(w, d, g);
      },
      data: {
        id: id,
        grid: grid,
        allColumns: allColumns
      }
    };
    var win = $('<div>');
    $(win).applyCtfoWindow(winParam);

    var initCustomColumnsWindow = function(w, d, g) {
      if (!customColumns) return false;
      var columns = customColumns[d.id];
      compileSelectDom(columns, d.allColumns);
      bindWindowEvent(w, d, g, columns);
    };
    /**
     * [ 自定义列恢复默认]
     * @param  {[String]} id [模块id]
     * @return {[isDel]}    [清除结果]
     */
    var compileSelectDomForReset = function(id) {
      var isDel = false;
      $.ajax({
        url: CTFO.config.sources.delCustomColumns + '?reportId=' + id,
        type: 'POST',
        dataType: 'json',
        async: false, // 同步
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if ('1' == data) {
            isDel = true;
          }
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
      return isDel;
    };
    var compileSelectDom = function(columns, allColumns) {
      var leftColumns = $('#leftSel').children() || [],
        rightColumns = $('#rightSel').children() || [],
        leftColumnsLen = leftColumns.length,
        rightColumnsLen = rightColumns.length,
        newOptions = [],
        newSelectOptions = [];
      $.merge(leftColumns, rightColumns);
      var columnsText = $.map(leftColumns, function(item, index) {
        if (!item) return "";
        var s = item.innerHTML;
        var s = s.replace(/[\r\n]/g, "");
        var s = $.trim(s);
        return s;
      });
      $(columns).each(function(event) {
        var text = this.text,
          name = this.name,
          cid = this.id || '',
          showType = this.showType;
        //从所有列中获取该列是否是需要隐藏的字段showtype，如果是隐藏，则放置在左侧未选择列表；
        for (var i = 0; i < allColumns.length; i++) {
          if (name == allColumns[i].name) {
            showType = allColumns[i].showType;
            break;
          }
        }
        var isRight = false;
        if (rightColumnsLen > 0) {
          for (var i = 0; i < rightColumnsLen.length; i++) {
            if (name == rightColumnsLen[i].name) {
              isRight = true;
              break;
            }
          }
        }
        if ($.inArray(text, columnsText) < 0) {
          var option = '<option id="' + cid + '" name="' + name + '">' + text + '</option>';
          if (leftColumnsLen == 0 && rightColumnsLen == 0) {
            if (showType === 'hidden') {
              newOptions.push(option);
            } else {
              newSelectOptions.push(option);
            }
          } else {
            if (!isRight) {
              newOptions.push(option);
            }
          }
        }
      });
      $("#leftSel").append(newOptions.join(''));
      $('#rightSel').append(newSelectOptions.join(''));

    };

    var bindWindowEvent = function(w, d, g, columns) {
      $(w).find('.moveToSelect').click(function(event) {
          $("#leftSel option:selected").appendTo("#rightSel");
        }).end()
        .find('.moveToUnSelect').click(function(event) {
          $("#rightSel option:selected").appendTo("#leftSel");
        }).end()
        .find('.moveUp').click(function(event) {
          var target = $("#rightSel option:selected");
          $(target[0]).insertBefore($(target[0]).prev());
        }).end()
        .find('.moveDown').click(function(event) {
          var target = $("#rightSel option:selected");
          $(target[0]).insertAfter($(target[0]).next());
        }).end()
        .find('.saveButton').click(function(event) {
          saveColumns(w, d, g);
        }).end()
        .find('.resetButton').click(function(event) {
          //解决点击“恢复默认”之后，其他按钮依然可以使用的问题。
          $(w).find('.moveToSelect').attr('disabled', 'disabled');
          $(w).find('.moveToUnSelect').attr('disabled', 'disabled');
          $(w).find('.moveUp').attr('disabled', 'disabled');
          $(w).find('.moveDown').attr('disabled', 'disabled');

          $(w).find('.saveButton').attr('disabled', 'disabled');
          $(w).find('.resetButton').attr('disabled', 'disabled');
          $(w).find('.cancelButton').attr('disabled', 'disabled');
          $(w).find('.l-dialog-winbtn').attr('disabled', 'disabled');

          resetColumns(columns, d.allColumns, d.id, d.grid, w);
        }).end()
        .find('.cancelButton').click(function(event) {
          g.close();
        }).end();
    };
    //恢复默认是根据reportid删除数据库中该用户的自定义列信息，成功后，grid刷新，展示的列排除showtype为hidden的列；
    var resetColumns = function(columns, allColumns, id, grid, w) {
      $.ligerDialog.confirm("确定要恢复默认值吗？", function(yes) {
        if (yes) {
          $("#leftSel").html('');
          $('#rightSel').html('');
          var isDel = compileSelectDomForReset(id);
          if (isDel) {
            compileSelectDom(columns, allColumns);
            var columnFilter = [];
            for (var i = 0; i < allColumns.length; i++) {
              if (allColumns[i].showType != 'hidden') {
                columnFilter.push(allColumns[i]);
              }
            }
            grid.setOptions({
              columns: columnFilter
            });
            grid.loadData();
          }
        }
        $(w).find('.moveToSelect').removeAttr('disabled');
        $(w).find('.moveToUnSelect').removeAttr('disabled');
        $(w).find('.moveUp').removeAttr('disabled');
        $(w).find('.moveDown').removeAttr('disabled');

        $(w).find('.saveButton').removeAttr('disabled');
        $(w).find('.resetButton').removeAttr('disabled');
        $(w).find('.cancelButton').removeAttr('disabled');
        $(w).find('.l-dialog-winbtn').removeAttr('disabled');

      });

    };

    var getChangedColumns = function(w) {
      var unSelectColumns = $('#leftSel').children(),
        selectedColumns = $('#rightSel').children(),
        arr1 = [],
        arr2 = [],
        showColumns = [];
      $(unSelectColumns).each(function(i) {
        var text = $.trim($(this).text()),
          name = $(this).attr('name'),
          cid = $(this).attr('id');
        arr1.push(cid + ':' + name + ':' + text + ':0:' + (i + 1));
      });
      $(selectedColumns).each(function(i) {
        var text = $.trim($(this).text()),
          name = $(this).attr('name'),
          cid = $(this).attr('id');
        arr2.push(cid + ':' + name + ':' + text + ':1:' + (i + 1));
        showColumns.push(name);
      });
      $.merge(arr1, arr2);
      return [arr1, showColumns];
    };

    var saveColumns = function(w, d, g) {
      var columns = getChangedColumns(w),
        param = {
          reportId: d.id,
          columnArr: columns[0].join('!')
        };
      $.ajax({
        url: CTFO.config.sources.updateCustomColumns,
        type: 'POST',
        dataType: 'json',
        async: false, // 同步
        data: param,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if (+data !== 1) {
            $.ligerDialog.success("保存失败！");
            return false;
          }
          $.ligerDialog.success("保存成功！");
          var columnArray = columns[1];
          var columnNow = [];

          var columnCustom = [];
          var columnOper = [];
          if (columnArray) {
            for (var i = 0; i < d.allColumns.length; i++) {
              if (true == d.allColumns[i].frozen) {
                columnNow.push(d.allColumns[i]);
              } else if (d.allColumns[i].operate == true) {
                columnOper.push(d.allColumns[i]);
              } else {
                for (var j = 0; j < columnArray.length; j++) {
                  if (d.allColumns[i].name == columnArray[j]) {
                    columnCustom.push(d.allColumns[i]);
                    break;
                  }
                }
              }
            }
            //按照自定义列顺序显示列表，现添加排序功能
            var isOk = false;
            var index = 0;
            var pointer = 0;
            for (var m = 0; m < columnArray.length; m++) {
              for (var n = 0; n < columnCustom.length; n++) {
                if (columnArray[m] == columnCustom[n].name) {
                  isOk = true;
                  index = n;
                  break;
                }
              }

              if (isOk) {
                var linshi = columnCustom[pointer];
                columnCustom[pointer] = columnCustom[index];
                columnCustom[index] = linshi;
                pointer++;
                isOk = false;
              }
            }
            columnNow = columnNow.concat(columnCustom, columnOper);

          } else {
            for (var i = 0; i < d.allColumns.length; i++) {
              if (true == d.allColumns[i].frozen) {
                columnNow.push(d.allColumns[i]);
              } else if (d.allColumns[i].operate == true) {
                columnNow.push(d.allColumns[i]);
              }
            }
          }
          d.grid.setOptions({
            columns: columnNow
          });
          d.grid.loadData();
          g.close();
        },
        error: function(xhr, textStatus, errorThrown) {
          $.ligerDialog.success("保存失败！");
        }
      });

    };

  };

  /**
   *input输入框focus 事件
   */
  this.defaultFocusDesign = function(Darray) {
    for (var i = 0; i < Darray.length; i++) {
      var name = Darray[i].name;
      var text = Darray[i].value;
      (function(name, text) {
        $(name).focus(function(event) {
          var val = $(this).val();
          if (!val || val == text) $(this).val('');
        }).blur(function(event) {
          var val = $(this).val();
          if (!val || val == text) $(this).val(text);
        });
      })(name, text);
    };
  };

  /**
   * @description 生成下拉框选项
   * @param {Object}
   *            key
   * @param {Object}
   *            container
   */
  this.getSelectList = function(list, container, defaultValue, tip) {
    var tip = tip || "请选择...";
    var options = ['<option value="" title="' + tip + '">' + tip + '</option>'];
    $(list).each(function() {
      var selected = (defaultValue && defaultValue == this.code) ? 'selected' : '';
      options.push('<option value="' + this.code + '" title="' + this.name + '" ' + selected + '>' + this.name + '</option>');
    });
    $(container).html('').append(options.join(''));
  };

  /**
   * @description 根据编码查询对应名字
   * @param {Object}
   *            key
   * @param {Object}
   *            code
   */
  this.getNameByCode = function(list, code) {
    var text = '';
    $(list).each(function() {
      if (code == this.code) {
        text = this.name;
      }
    });
    return text;
  };



};
/**
 * [Commands 指令下发函数集合对象]
 */
CTFO.Util.Commands = function() {
  this.sendCommands = function(ctype, qp, cp) {
    if (!ctype) return false;
    var url = '';
    switch (ctype) {
      case 'message':
        url = (cp.isBatchMessage ? CTFO.config.sources.batchMessageCommand : CTFO.config.sources.singleMessageCommand);
        break;
      case 'photo':
        url = (cp.isBatchPhoto ? CTFO.config.sources.batchPhotoCommand : CTFO.config.sources.photoCommand);
        break;
      case 'calling':
        url = CTFO.config.sources.callingCommand;
        break;
      case 'taping':
        url = CTFO.config.sources.tapingCommand;
        break;
      case 'tracking':
        url = CTFO.config.sources.emphasisCommand;
        break;
      case 'checkroll':
        url = CTFO.config.sources.checkrollCommand;
        break;
      case 'unchainAlarm':
        url = CTFO.config.sources.unchainAlarm;
        break;
    }
    // $.get(url, qp, function(data, textStatus, xhr) {
    //   if (cp.callback) cp.callback(data, cp);
    // }, 'json');
    $.ajax({
      url: url,
      type: 'POST',
      dataType: 'json',
      data: qp,
      complete: function(xhr, textStatus) {
        //called when complete
      },
      success: function(data, textStatus, xhr) {
        if (cp.callback) cp.callback(data, cp);
      },
      error: function(xhr, textStatus, errorThrown) {
        if (cp.callback) cp.callback(null, cp);
      }
    });

  };
  this.getCommandStatus = function(seq, fillObj) {
    $.get(CTFO.config.sources.commandStatus, {
      "requestParam.equal.coSeq": seq
    }, function(data, textStatus, xhr) {
      if (data && data.error) return false;
      else $(fillObj).html(data[0].seq);
    }, 'json');

  };
};
/**
 * [throttle 函数节流]
 * @param  {Function} fn           [待执行函数]
 * @param  {[type]}   delay        [延时执行时间]
 * @param  {[type]}   mustRunDelay [必须执行一次的时间间隔]
 * @return {[type]}                [description]
 * @ version 2.0
 */
// CTFO.Util.throttle = function(fn, delay, mustRunDelay) {
//   var timer = null;
//   var t_start;
//   return function() {
//     var context = this,
//       args = arguments,
//       t_curr = +new Date();
//     clearTimeout(timer);
//     if(!t_start) {
//       t_start = t_curr;
//     }
//     if(t_curr - t_start >= mustRunDelay) {
//       fn.apply(context, args);
//       t_start = t_curr;
//     } else {
//       timer = setTimeout(function() {
//         fn.apply(context, args);
//       }, delay);
//     }
//   };
// };
// version 3.0
CTFO.Util.throttle = function(fn, delay, mustRunDelay) {
  var throttle = {
    timer: null,
    t_start: 0,
    context: null,
    args: null,
    act: function(context, arguments) {
      var t_curr = +new Date();
      clearTimeout(this.timer);
      if (!this.t_start) {
        this.t_start = t_curr;
      }
      this.context = context;
      this.args = arguments;
      if (t_curr - this.t_start >= mustRunDelay) {
        this.done();
      } else {
        this.timer = setTimeout(this.timerHandler, delay);
      }
    },
    done: function() {
      fn.apply(this.context, this.args);
      this.t_start = 0;
    },
    timerHandler: function() {
      throttle.done();
    }
  };
  return function() {
    throttle.act(this, arguments);
  };
};
/**
 * [Map 地图对象封装]
 * @author [fanshine124@gmail.com]
 * @todo [考虑支持多个地图api的封装方式]
 * @param {[Object]} p [参数对象]
 * @return {[Object]} [地图对象封装]
 */
CTFO.Util.Map = function(p) {
  var defaults = {
    center: [116.29376, 39.95776], // 默认北京
    level: 4
  };
  // return {
  var pbf = {
    map: null,
    pointMarkerObj: {},
    // pointMarker缓存
    markerObj: {},
    // marker缓存
    markerLblObj: {},
    // label对象缓存
    markerLockObj: {},
    // 被锁定的marker缓存
    markerTipObj: {},
    // 自定义tip缓存，用label标注实现
    currentTipId: "",
    // 当前显示的tip对象引用缓存
    polyLineObj: {},
    // polyline缓存
    rectObj: {},
    // rect缓存
    polygonObj: {},
    // polygon缓存
    circleObj: {},
    // circle缓存
    ellipseObj: {},
    // ellipse缓存
    markerIdCache: [],
    // marker id 缓存
    polylineIdCache: [],
    // polyline id 缓存
    rectIdCache: [],
    // rect id 缓存
    polygonIdCache: [],
    // polygon id 缓存
    circleIdCache: [],
    // circle id 缓存
    ellipseIdCache: [],
    // ellipse id 缓存
    /**
     * [init 地图初始化]
     * @param  {[Object]} p [参数对象, p.container 地图容器 p.center 地图中心点坐标 p.level 地图级别]
     * @return {[Object]}   [Map对象]
     */
    init: function(p) {
      if (!p.container || $('#' + p.container).length < 1) return false;

      var that = this;
      p = $.extend({}, defaults || {}, p || {});
      this.map = new TMMaps(p.container);
      this.map.closeHighlight(); // 去掉突出显示特效
      this.map.centerAndZoom(new TMLngLat(p.center[0], p.center[1]), +p.level);
      this.map.handleMouseScroll(true);
      this.map.enableDoubleClickZoom();
      this.map.addControl(new TMMapControl(1, true));
      // 添加右键菜单
      var menus = [];
      var arr = [{
        id: 'zoomIn',
        text: '放大',
        fun: function() {
          that.map.zoomIn();
        }
      }, {
        id: 'zoomOut',
        text: '缩小',
        fun: function() {
          that.map.zoomOut();
        }
      }];
      $(arr).each(function(i) {
        var mi = new TMMenuItem();
        mi.id = this.id;
        mi.menuText = this.text;
        mi.functionName = this.fun;
        if (i > 0) mi.separateLine = true;
        menus.push(mi);
      });
      this.map.addMenuControl(new TMMenuControl(menus));
      return this;
    },
    /**
     * [addMapControl 添加骨头棒控件]
     * @param {[Int]} type [type 类型(可选),默认值为0
     *                     0(默认):显示移动按钮、缩放按钮和缩放等级条
     *                     1:显示移动按钮和缩放按钮,不显示缩放等级条
     *                     2:只显示缩放按钮(竖排)
     *                     3:只显示缩放按钮(横排)
     *                     4:只显示缩放按钮和缩放等级条]
     * @return {[Null]} [无返回]
     */
    addMapControl: function(type) {
      var mc;
      if (type && '1,2,3,4'.indexOf(type) > -1) {
        mc = new TMMapControl(type);
      } else {
        mc = new TMMapControl();
      }
      // 添加骨头棒控件至地图
      this.map.addControl(mc);
    },
    /**
     * [addScaleControl 添加地图比例尺]
     * @return {[Null]} [无返回]
     */
    addScaleControl: function() {
      this.map.addControl(new TMScaleControl());
    },
    /**
     * [addCenterCrossControl 添加地图的中心十字]
     * @return {[Null]} [无返回]
     */
    addCenterCrossControl: function() {
      this.map.addControl(new TMCenterCrossControl());
    },
    /**
     * [addMapCopyRight 添加地图版权信息]
     * @param {[Object]} p [参数对象, p.right 距离右边值 p.bottom 距离底边值]
     * @return {[Null]} [无返回]
     */
    addMapCopyRight: function(p) {
      var cr = new TMCopyrightControl();
      cr.addCopyright({
        id: 1,
        content: "<span style='color:blue;font-size:12px;'>&copy;2012 TransWiseway - 审图号GS(2010)1367号  - 甲测资质11002076</span>",
        bounds: new TMLngLatBounds([new TMLngLat(10, 30), new TMLngLat(160, 80)])
      });
      cr.setRight(p.right ? p.right : 160);
      cr.setBottom(p.bottom ? p.bottom : 10);
      this.map.addControl(cr);
    },
    /**
     * [addOverviewMapControl 添加地图鹰眼控制]
     * @param {[Boolean]} openFlag [初始化是否打开]
     * @return {[Null]} [无返回]
     */
    addOverviewMapControl: function(openFlag) {
      if (!this.minmap) this.minmap = new TMOverviewMapControl(null, [200, 150], null, null, 3);
      this.map.addControl(this.minmap);
      if (!openFlag) {
        // 切换鹰眼地图的开关
        this.minmap.changeView();
      }

    },
    /**
     * [addMarker 添加marker对象]
     * @param {[Object]} p [参数对象]
     * @param {[String]} p.id [marker对象id]
     * @param {[Int]} p.lng [x坐标]
     * @param {[Int]} p.lat [y坐标]
     * @param {[String]} p.icon [marker图标]
     * @param {[Array]} p.anchor [偏移]
     * @return {[Object]} [TMMarkerOverlay对象]
     */
    addMarker: function(p) {
      var that = this;
      if (!this.markerObj) this.markerObj = {};
      if (!this.markerLblObj) this.markerLblObj = {};

      var defaults = {
        iconUrl: 'img/monitor/map/markerIcon/marker.png',
        iconW: 20,
        iconH: 20,
        anchor: [10, 10],
        labelAnchor: [0, 0],
        labelFontSize: 10,
        labelBgColor: '#545454',
        labelFontColor: '#FFFFFF',
        isDefaultTip: true,
        isOpen: false,
        isEdit: false,
        isMultipleTip: false,
        eventType: 'click'
      };
      p = $.extend({}, defaults || {}, p || {});
      var marker = null,
        label = null,
        tip = null,
        selfDefineTip = null,

        icon = new TMIcon(p.iconUrl, new TMSize(p.iconW, p.iconH)),
        lnglat = new TMLngLat(p.lng, p.lat);
      // 创建Marker
      if (!this.markerObj[p.id]) {
        // 标记对象,并保持在标记数组中
        var option = new TMMarkerOptions();
        option.lnglat = lnglat;
        option.icon = icon;
        option.offset = new TMPoint(p.anchor[0], p.anchor[1]);
        marker = new TMMarkerOverlay(option);
        this.markerObj[p.id] = marker;
        if (p.zIndex) {
          marker.setZindex(p.zIndex, p.zIndex);
        }
        if (p.onDragend) {
          marker.setEditable(true);
          TMEvent.addListener(marker, "dragend", p.onDragend);
        }
      } else {
        return this.markerObj[p.id];
      }
      // marker.setAnchorPer(p.anchor);
      this.addOverLay(marker);
      this.markerIdCache.push(p.id);

      if (p.label) {
        var option1 = new TMTextOptions(); // 创建点标注参数对象
        option1.lnglat = lnglat; // 设置标注的经纬度
        option1.text = p.title ? p.title : p.label; // 文本
        option1.editable = false; // 设置文本标注可编辑
        option1.fontSize = p.labelFontSize; // 设置字体的大小
        option1.bgColor = p.labelBgColor; // 设置文本的背景色
        option1.offset = new TMPoint(p.labelAnchor[0], p.labelAnchor[1]);
        label = new TMTextOverlay(option1); // 创建标注
        if (p.zIndex) {
          label.setZindex(p.zIndex, p.zIndex);
        }
        this.markerLblObj[p.id] = label;
        this.addOverLay(label);
      }
      if (p.isDefaultTip && p.tip) { // 默认风格
        selfDefineTip = p.tip;
      } else if (!p.isDefaultTip && p.tip) { // 自定义风格
        var option2 = new TMHtmlOptions();
        option2.lnglat = lnglat;
        option2.content = p.tip;
        selfDefineTip = new TMHtmlOverlay(option2);
      }
      // 如果没有缓存该tip，则加入缓存
      if (!this.markerTipObj[p.id] && selfDefineTip) this.markerTipObj[p.id] = selfDefineTip;
      // 注册锚点的click事件
      if (p.handler) { // 自定义marker点击事件
        TMEvent.addListener(marker, p.eventType, p.handler);
      } else if (selfDefineTip) { // 默认marker点击事件
        if (p.isDefaultTip) { // 默认风格的点击事件,closeInfoWindow/openInfoWindow
          TMEvent.addListener(marker, p.eventType, function(obj) {
            if (that.markerTipObj[that.currentTipId] && !p.isMultipleTip) that.markerObj[that.currentTipId].closeInfoWindow();
            var tip = obj.openInfoWindow(p.title || p.label || '', selfDefineTip);
            if (p.tipCallback) {
              var tipPlace = $(tip.getObject());
              p.tipCallback(tipPlace, p.lng, p.lat);
            }
          });
        } else { // 自定义风格的点击事件,增删PointOverlay
          TMEvent.addListener(marker, p.eventType, function(obj) {
            if (that.markerTipObj[p.id]) that.map.overlayManager.removeOverLay(that.markerTipObj[p.id]);
            if (that.markerTipObj[that.currentTipId] && !p.isMultipleTip) that.map.overlayManager.removeOverLay(that.markerTipObj[that.currentTipId]);
            that.markerTipObj[p.id] = selfDefineTip;
            that.map.overlayManager.addOverLay(selfDefineTip);
            that.currentTipId = p.id;
          });
        }
      }
      if (selfDefineTip && p.isOpen && !p.isDefaultTip) { // 自定义风格tip
        if (this.markerTipObj[this.currentTipId] && !p.isMultipleTip) this.removeOverLay(that.markerTipObj[that.currentTipId]);
        this.markerTipObj[p.id] = selfDefineTip;
        this.addOverLay(selfDefineTip);
        this.currentTipId = p.id;
      } else if (selfDefineTip && p.isOpen && p.isDefaultTip) { // 默认风格tip
        if (this.markerTipObj[that.currentTipId] && !p.isMultipleTip) this.markerObj[that.currentTipId].closeInfoWindow();
        marker.openInfoWindow(p.title ? p.title : p.label, selfDefineTip);
      }
      return this.markerObj[p.id];
    },
    /**
     * [removeMarker 根据ID删除Marker]
     * @param  {[String]} id [marker对象id]
     * @return {[Null]}    [无返回]
     */
    removeMarker: function(id) {
      if (!(this.markerObj && this.markerObj[id])) return false;

      if (this.markerTipObj && this.markerTipObj[id]) {
        if (typeof(this.markerTipObj[id]) == "object") { // 如果是自定义tip,则removeOverLay
          this.removeOverLay(this.markerTipObj[id], true);
        } else { // 如果是默认风格tip,则关闭infoWindow
          this.markerObj[id].closeInfoWindow();
        }
        delete this.markerTipObj[id];
      }

      // 删除label标注
      if (this.markerLblObj && this.markerLblObj[id]) {
        this.removeOverLay(this.markerLblObj[id], true);
        delete this.markerLblObj[id];
      }

      // 删除锁定的marker缓存
      if (this.markerLockObj && this.markerLockObj[id]) {
        delete this.markerLockObj[id];
      }

      this.removeOverLay(this.markerObj[id], true);
      delete this.markerObj[id];
      this.markerIdCache.splice($.inArray(id, this.markerIdCache), 1);

    },
    /**
     * [removeAllMarkers 删除所有marker]
     * @return {[Null]} [无返回]
     */
    removeAllMarkers: function() {
      for (var i = this.markerIdCache.length; i--;) {
        this.removeMarker(this.markerIdCache[i]);
      }
    },
    /**
     * [moveMarker 移动marker]
     * @param  {[type]} m [参数集合]
     * @param {[String]} m.id [marker唯一标识]
     * @param {[Number]} m.lng [经度]
     * @param {[Number]} m.lat [纬度]
     * @param {[String]} m.iconUrl [marker图片路径]
     * @param {[String]} m.iconW [marker图片宽]
     * @param {[String]} m.iconH [marker图片高]
     * @return {[Null]}   [无返回]
     */
    moveMarker: function(m) {
      if (!(this.markerObj && this.markerObj[m.id])) return false;
      m.anchor = m.anchor || [2, 2];
      var _marker = this.markerObj[m.id],
        _lngLat = new TMLngLat(m.lng, m.lat);

      _marker.setLngLat(_lngLat);
      // 如果是自定义tip,则removeOverLay
      if (this.markerTipObj && this.markerTipObj[m.id]) {
        var _tipObj = this.markerTipObj[m.id];
        if (typeof(_tipObj) == "object") _tipObj.setLngLat(_lngLat);
        else _marker.closeInfoWindow();
      }
      if (m.iconUrl) {
        var icon = new TMIcon(m.iconUrl, new TMSize(m.iconW, m.iconH));
        icon.setSrc(m.iconUrl);
        _marker.setIcon(icon);
      }
      if (m.anchor) {
        var offset = new TMPoint(m.anchor[0], m.anchor[1]);
        _marker.setOffset(offset);
      }
      if (this.markerLblObj && this.markerLblObj[m.id]) {
        this.markerLblObj[m.id].setLngLat(_lngLat);
        if (m.label) this.markerLblObj[m.id].setText(m.label);
      }

      if (this.markerLockObj && this.markerLockObj[m.id]) {
        var _tmpLvl = this.getLevel();
        this.map.setCenter(_lngLat);
      }
      return this.markerObj[m.id];
    },
    /**
     * [getMarkerLabel 获取marker文字标注]
     * @param  {[String]} id [marker对象id]
     * @return {[Null]}    [无返回]
     */
    getMarkerLabel: function(id) {
      return this.markerLblObj[id];
    },
    /**
     * [getMarker 获取marker对象]
     * @param  {[String]} id [marker对象id]
     * @return {[Null]}    [无返回]
     */
    getMarker: function(id) {
      return this.markerObj[id];
    },
    /**
     * [hideMarkersByIds 根据ID数组隐藏Markers]
     * @param  {[Array]} arrIds [marker的id数组]
     * @return {[Null]}        [无返回]
     */
    hideMarkersByIds: function(arrIds) {
      for (var i = arrIds.length; i--;) {
        var id = arrIds[i];
        (that.markerObj[id]).setOpacity(0);
        if (that.markerLblObj[id]) that.markerLblObj[id].setOpacity(0);
        if (that.markerTipObj[id]) that.markerTipObj[id].setOpacity(0);
      }
    },
    /**
     * [showMarkersByIds 根据ID数组显示Markers]
     * @param  {[Array]} arrIds [marker的id数组]
     * @return {[Null]}        [无返回]
     */
    showMarkersByIds: function(arrIds) {
      for (var i = arrIds.length; i--;) {
        var id = arrIds[i];
        (that.markerObj[id]).setOpacity(50);
        if (that.markerLblObj[id]) that.markerLblObj[id].setOpacity(50);
        if (that.markerTipObj[id]) that.markerTipObj[id].setOpacity(50);
      }
    },
    /**
     * [lockMarker 锁定marker在地图中心]
     * @param  {[String]} id [marker对象id]
     * @return {[Null]}    [无返回]
     */
    lockMarker: function(id) {
      if (!this.markerLockObj) this.markerLockObj = {};

      this.markerLockObj[id] = true;
    },
    /**
     * [unlockMarker 解锁marker在地图中心]
     * @param  {[String]} id [marker对象id]
     * @return {[Null]}    [无返回]
     */
    unlockMarker: function(id) {
      if (this.markerLockObj[id]) this.markerLockObj[id] = false;
    },
    /**
     * [addPolyLine 新增线对象PolyLine]
     * @param {[type]} pl [参数集合]
     * @param {[String]} pl.id [唯一标识]
     * @param {[Array]} pl.arrLngLat [坐标数组]
     * @param {[String]} pl.strColor [填充颜色]
     * @param {[Number]} pl.numWidth [宽度]
     * @param {[Number]} pl.numOpacity [透明度]
     * @return {[Object]} [TMLineOverlay对象]
     */
    addPolyLine: function(pl) {
      var defaultParam = {
        strColor: "blue",
        numWidth: 3,
        numOpacity: 0.5
      };
      if (this.getPolyLine(pl.id)) return false;
      pl = $.extend({}, defaultParam, pl || {});
      if (!pl.arrLngLat.length) return false;
      if ((pl.arrLngLat.length % 2)) throw "arrLngLat%2 != 0";
      if (!this.polyLineObj) this.polyLineObj = {};

      var _arrSE_LngLat = [];
      while (pl.arrLngLat.length) {
        var _arrTmp = pl.arrLngLat.splice(0, 2);
        _arrSE_LngLat.push(new TMLngLat(_arrTmp[0], _arrTmp[1]));
      }

      var option = new TMLineOptions();
      option.lnglats = _arrSE_LngLat;
      option.color = pl.strColor;
      option.weight = pl.numWidth;
      option.opacity = pl.numOpacity;
      var _pl = new TMLineOverlay(option);
      this.polyLineObj[pl.id] = _pl;

      this.addOverLay(_pl);
      this.polylineIdCache.push(pl.id);

      return _pl;
    },
    /**
     * [removePolyLine 根据ID删除PolyLine]
     * @param  {[type]} id [PolyLine唯一标识]
     * @return {[Null]}    [无返回]
     */
    removePolyLine: function(id) {
      if (this.polyLineObj && this.polyLineObj[id]) {
        this.removeOverLay(this.polyLineObj[id], true);
        delete this.polyLineObj[id];
        if (this.polylineIdCache instanceof Array) this.polylineIdCache.splice($.inArray(id, this.polylineIdCache), 1);
      }
    },
    getPolyLine: function(id) {
      return this.polyLineObj[id];
    },
    /**
     * [removeAllPolyLines 删除所有PolyLine]
     * @return {[Null]} [无返回]
     */
    removeAllPolyLines: function() {
      for (var i = this.polylineIdCache.length; i--;) {
        var id = this.polylineIdCache[i];
        this.removePolyLine(id);
      }
    },
    /**
     * [addRect 添加矩形叠加呜]
     * @param {[Object]} pr [参数]
     */
    addRect: function(pr) {
      var defaultParam = {

      };
      if (this.getRect(pr.id)) return false;
      pr = $.extend({}, defaultParam, pr || {});
      if (!pr.arrLngLat.length) return false;
      if ((pr.arrLngLat.length % 2)) throw "arrLngLat%2 != 0";
      if (!this.rectObj) this.rectObj = {};

      var rectOption = new TMRectOptions(),
        bounds = new TMLngLatBounds(pr.arrLngLat[0], pr.arrLngLat[1], pr.arrLngLat[2], pr.arrLngLat[3]);
      rectOption.bounds = bounds;
      if (pr.weight) rectOption.weight = pr.weight;
      if (pr.lineColor) rectOption.lineColor = pr.lineColor;
      if (pr.fillColor) rectOption.fillColor = pr.fillColor;
      if (pr.lineStyle) rectOption.lineStyle = pr.lineStyle;
      if (pr.opacity) rectOption.opacity = pr.opacity;
      if (pr.editable) rectOption.editable = pr.editable;
      var _rect = new TMRectOverlay(rectOption);
      this.rectObj[pr.id] = _rect;
      this.addOverLay(_rect);
      this.rectIdCache.push(pr.id);
      return _rect;
    },
    removeRect: function(id) {
      if (this.rectObj && this.rectObj[id]) {
        this.removeOverLay(this.rectObj[id], true);
        delete this.rectObj[id];
        this.rectIdCache.splice($.inArray(id, this.rectIdCache), 1);
      }
    },
    getRect: function(id) {
      return this.rectObj[id];
    },
    removeAllRects: function() {
      for (var i = this.rectIdCache.length; i--;) {
        var id = this.rectIdCache[i];
        this.removeRect(id);
      }
    },
    /**
     * [addPolygon 新增面对象]
     * @param {[Object]} pg [参数集合]
     * @param {[String]} pg.id Polygon [唯一标识]
     * @param {[Array]} pg.arrLngLat Polygon [坐标数组]
     * @param {[String]} pg.strColor Polygon [边框颜色]
     * @param {[String]} pg.strBgColor Polygon [背景填充颜色]
     * @param {[Number]} pg.numWidth Polygon [边框宽度]
     * @param {[Number]} pg.numOpacity Polygon [填充透明度]
     * @return {[Object]} [TMPolygonOverlay对象]
     */
    addPolygon: function(pg) {
      var defaultParam = {
        strColor: "blue",
        strBgColor: '',
        numWidth: 3,
        numOpacity: 0.5
      };
      if (this.getPolygon(pg.id)) return false;
      pg = $.extend({}, defaultParam, pg || {});

      if (!pg.arrLngLat.length) return false;
      if ((pg.arrLngLat.length % 2)) throw "arrLngLat%2 != 0";
      if (!this.polygonObj) this.polygonObj = {};

      var _arrSE_LngLat = [];
      while (pg.arrLngLat.length) {
        var _arrTmp = pg.arrLngLat.splice(0, 2);
        _arrSE_LngLat.push(new TMLngLat(_arrTmp[0], _arrTmp[1]));
      }
      var option = new TMPolygonOptions();
      option.lnglats = _arrSE_LngLat; // 设置折线点
      option.color = pg.strColor; // 设置多边形边框颜色
      option.bgcolor = pg.strBgColor; // 设置多边形的填充色
      option.opacity = pg.numOpacity; // 设置透明度
      // option.lineStyle = ;//设置边框线的样式为点状
      var _pg = new TMPolygonOverlay(option);
      this.polygonObj[pg.id] = _pg;

      this.addOverLay(_pg);
      this.polygonIdCache.push(pg.id);
      return _pg;
    },
    /**
     * [removePolygon 根据ID删除Polygon]
     * @param  {[String]} id [面对象id]
     * @return {[Null]}    [无返回]
     */
    removePolygon: function(id) {
      if (this.polygonObj && this.polygonObj[id]) {
        this.removeOverLay(this.polygonObj[id], true);
        delete this.polygonObj[id];
        this.polygonIdCache.splice($.inArray(id, this.polygonIdCache), 1);
      }
    },
    getPolygon: function(id) {
      return this.polygonObj[id];
    },
    /**
     * [removeAllPolygons 删除所有面对象]
     * @return {[Null]} [无返回]
     */
    removeAllPolygons: function() {
      for (var i = this.polygonIdCache.length; i--;) {
        var id = this.polygonIdCache[i];
        this.removePolygon(id);
      }
    },
    /**
     * [addCircle 新增圆对象]
     * @param {[Object]} c [参数集合]
     * @param {[String]} c.id circleOverlay [唯一标识]
     * @param {[Int]} c.lng [中心点x坐标]
     * @param {[Int]} c.lat [中心点y坐标]
     * @param {[Number]} c.numRadius [圆半径(可选，默认500)]
     * @param {[String]} c.strColor [圆边框颜色(可选，默认蓝色)]
     * @param {[String]} c.strBgColor [圆背景色或填充色(可选，默认黄色)]
     * @param {[Number]} c.numWeight [圆边框宽度(可选，默认1)]
     * @param {[Number]} c.numOpacity [圆背景透明度(可选，默认0.8)]
     * @return {[Object]} [TMCircleOverlay 对象]
     */
    addCircle: function(c) {
      var defaults = {
        lng: null,
        lat: null,
        numRadius: 500,
        strColor: "blue",
        strBgColor: "yellow",
        numWeight: 1,
        numOpaity: 0.8
      };
      //if (this.ellipseObj[c.id]) return false;
      c = $.extend({}, defaults, c);

      if (!c.lng || !c.lat) return false;
      if (!this.circleObj) this.circleObj = {};

      var option = new TMCircleOptions();
      option.centerLngLat = new TMLngLat(c.lng, c.lat); // 设置圆中心点
      option.radius = c.numRadius; // 设置圆半径
      option.units = 'meter'; // 半径类型
      option.lineColor = c.strColor; // 设置圆边框线的颜色
      option.fillColor = c.strBgColor; // 设置圆填充颜色
      option.opacity = c.numOpaity; // 设置圆透明度
      option.weight = c.numWeight; //边框宽
      var circle = new TMCircleOverlay(option); // 实例化椭圆对象
      this.addOverLay(circle);
      this.circleObj[c.id] = circle;
      this.circleIdCache.push(c.id);
      return circle;
    },
    /**
     * [removeCircle 删除圆对象]
     * @param  {[type]} id [圆对象id,对应Map对象中缓存的圆对象]
     * @return {[Null]}    [无返回]
     */
    removeCircle: function(id) {
      if (this.circleObj && this.circleObj[id]) {
        this.removeOverLay(this.circleObj[id], true);
        delete this.circleObj[id];
        this.circleIdCache.splice($.inArray(id, this.circleIdCache), 1);
      }
    },
    /**
     * [removeAllCircle 删除所有圆对象]
     * @return {[Null]} [无返回]
     */
    removeAllCircle: function() {
      for (var i = this.circleIdCache.length; i--;) {
        var id = this.circleIdCache[i];
        this.removeCircle(id);
      }
    },
    /**
     * [getLevel 获取当前地图缩放级别]
     * @return {[Number]} [当前地图级别数字]
     */
    getLevel: function() {
      return this.map.getCurrentZoom();
    },
    /**
     * [changeSize 重置地图宽高]
     * @return {[Null]} [无返回]
     */
    changeSize: function() {
      this.map.resizeMapDiv();
    },
    /**
     * [setLevel 设置地图缩放级别]
     * @param {[Number]} level [地图级别数字]
     * @return {[Null]} [无返回]
     */
    setLevel: function(level) {
      this.map.zoomTo(level);
    },
    /**
     * [setCenter 根据坐标设置地图中心点]
     * @param {[Number]} numLng [经度]
     * @param {[Number]} numLat [纬度]
     * @return {[Null]} [无返回]
     */
    setCenter: function(numLng, numLat) {
      this.map.setCenterAtLngLat(new TMLngLat(parseFloat(numLng, 10).toFixed(5), parseFloat(numLat, 10).toFixed(5)));
    },
    /**
     * [setCenterByLngLat 根据坐标对象设置中心点]
     * @param {[Object]} lnglat [TMLngLat对象]
     * @return {[Null]} [无返回]
     */
    setCenterByLngLat: function(lnglat) {
      this.map.setCenter(lnglat);
    },
    /**
     * [zoomIn 放大地图]
     * @return {[Null]} [无返回]
     */
    zoomIn: function() {
      this.map.zoomIn();
    },
    /**
     * [zoomOut 缩小地图]
     * @return {[Null]} [无返回]
     */
    zoomOut: function() {
      this.map.zoomOut();
    },
    /**
     * [mouseWheelEnabled 是否允许使用鼠标滚轮缩放地图]
     * @param  {[Boolean]} b [标识状态]
     * @return {[Null]}   [无返回]
     */
    mouseWheelEnabled: function(b) {
      if (b) this.map.enableHandleMouseScroll(true);
      else this.map.disableDragHandleMouseScroll(false);
    },
    /**
     * [getBestMap 获取多个坐标在地图上的最佳视野范围]
     * @param  {[Array]} array [经度，纬度数组或TMLngLat坐标对象数组]
     * @return {[Null]}       [无返回]
     */
    getBestMap: function(array) {
      if ($.type(array) !== 'array' || ($.type(array[0]) === 'string' && array.length % 2 !== 0)) return false;
      var arrLngLat = [];
      if (typeof array[0] === 'string' || typeof array[0] === 'number') {
        while (array.length) {
          var arrTmp = array.splice(0, 2);
          arrLngLat.push(new TMLngLat(parseFloat(arrTmp[0]), parseFloat(arrTmp[1])));
        }
      } else {
        arrLngLat = array;
      }

      this.map.getBestMap(arrLngLat);
    },
    /**
     * [getCenterPoint 获取当前地图中心点对象]
     * @return {[Object]} [TMLngLat对象]
     */
    getCenterPoint: function() {
      return this.map.getCenterPoint();
    },
    /**
     * [getLngLatBounds 获取当前地图显示的地理区域范围]
     * @return {[String]} ["xmin,ymin;xmax,ymax"字符串]
     */
    getLngLatBounds: function() {
      var bound = this.map.getLngLatBounds();
      var XminNTU = bound.XminNTU / 100000; // 12365443
      var YminNTU = bound.YminNTU / 100000; // 3663097
      var XmaxNTU = bound.XmaxNTU / 100000; // 12365443
      var YmaxNTU = bound.YmaxNTU / 100000; // 3663097
      return XminNTU + "," + YminNTU + ";" + XmaxNTU + "," + YmaxNTU;
    },
    /**
     * [removeAll 删除所有OverLay]
     * @return {[Null]} [无返回]
     */
    removeAll: function() {
      this.map.clearOverLays();
    },
    /**
     * [changeView 切换鹰眼显示/隐藏]
     * @return {[Null]} [无返回]
     */
    changeView: function() {
      this.minmap.changeView();
    },
    /**
     * [panTo 移动地图中心到指定坐标]
     * @param  {[Number]} lon [经度]
     * @param  {[Number]} lat [纬度]
     * @return {[Null]}     [无返回]
     */
    panTo: function(lon, lat) {
      var lonlat = new TMLngLat(lon, lat);
      this.map.moveToCenter(lonlat);
    },
    /**
     * [fromLngLatToContainerPixel 将地理坐标转化为地图上点的像素坐标，相对于container左上角]
     * @param  {[Object]} lnglat [TMLnglat对象]
     * @return {[Object]}        [TMPoint对象]
     */
    fromLngLatToContainerPixel: function(lnglat) {
      var point = this.map.fromLngLatToContainerPixel(lnglat);
      return point;
    },
    /**
     * [addOverLay 添加OverLay通用方法]
     * @param {[Object]} overlay [TMOverLay及其继承类对象]
     * @return {[Null]}         [无返回]
     */
    addOverLay: function(overlay) {
      this.map.overlayManager.addOverLay(overlay);
    },
    /**
     * [removeOverLay 删除OverLay通用方法]
     * @param  {[Object]} overlay [TMOverLay及其继承类对象]
     * @return {[Null]}         [无返回]
     */
    removeOverLay: function(overlay) {
      this.map.overlayManager.removeOverLay(overlay, true);
    },
    /**
     * [containsPoint 判断当前地图视野范围是否包含传入的坐标点]
     * @param  {[Object]} lnglat [TMLnglat对象]
     * @return {[Boolean]}        [是否包含的布尔值]
     */
    containsPoint: function(lnglat) {
      return this.map.getLngLatBounds().containsBounds(lnglat);
    }
  };

  return pbf.init(p);
};

/**
 * [MapTool 地图工具条对象封装]
 * @author [fanshine124@gmail.com]
 * @param {[Object]} options [参数对象]
 * @return {[Object]} [地图工具条对象封装]
 */
CTFO.Util.MapTool = (function() {
  var constructor = function(options) {
    var p = {},
      map = null,
      activeButton = null,
      control = null,
      activeControl = null,
      mainContainer = null,
      normalButtonsContainer = null,
      extendButtonsContainer = null,
      moreButtonsContainer = null,
      moreButtonsInHide = true;
    var defaultButtons = [{
      buttonType: 'movemap',
      icon: 'ico157',
      name: '拖动',
      title: '拖动地图',
      appendToType: 0,
      callback: mapToolButtonClick
    }, {
      buttonType: 'zoomin',
      icon: 'ico158',
      name: '拉框放大',
      title: '拉框放大',
      appendToType: 0,
      callback: mapToolButtonClick
    }, {
      buttonType: 'zoomout',
      icon: 'ico159',
      name: '拉框缩小',
      title: '拉框缩小',
      appendToType: 0,
      callback: mapToolButtonClick
    }, {
      buttonType: 'cover',
      icon: 'ico160',
      name: '测面',
      title: '测面',
      appendToType: 0,
      callback: mapToolButtonClick
    }, {
      buttonType: 'distance',
      icon: 'ico161',
      name: '测距',
      title: '测距',
      appendToType: 0,
      callback: mapToolButtonClick
    }, {
      buttonType: 'savemap',
      icon: 'ico162',
      name: '保存',
      title: '保存',
      appendToType: 0,
      callback: mapToolButtonClick
        // }, { // 新版本地图api，保存即可打印，所以去掉打印
        //   buttonType: 'printmap',
        //   icon: 'ico163',
        //   name: '打印',
        //   title: '打印',
        //   appendToType: 0,
        //   callback: mapToolButtonClick
    }];
    var createButtonHtml = function(param) {
      var html = [];
      if (param.appendToType === 0) {
        html = ['<li class=" cl h20 lh25 hand ' + param.buttonType + '" btype="' + param.buttonType + '" title="' + param.title + '">', '<span class="' + param.icon + '"></span><font>', param.name, '</font></li>'];
      } else if (param.appendToType === 2) {
        html = ['<div class="w120 lh25 fl hand ' + param.buttonType + '" title="' + param.title + '">', '<span class="' + param.icon + '"></span><font>', param.name, '</font></div>'];
      } else if (param.appendToType === 1) {
        html = ['<div class="w80 lh25 fl hand mr10 ' + param.buttonType + '" title="' + param.title + '">', '<span class="' + param.icon + '"></span><font>', param.name, '</font></div>'];
      } else if (param.appendToType === 3) {
        html = ['<div class=" fr h25 lh25 pt3 hand mr20 w70 ' + param.buttonType + '" title="' + param.title + '">', '<span class="' + param.icon + '"></span><font>', param.name, '</font></div>'];
      }
      return html.join('');
    };
    /**
     * [addButton 添加自定义地图工具条按钮]
     * @param {Object} b 传入的参数，结构为
     * @param {String} b.buttonType 按钮类型
     * @param {String} b.icon 按钮图标路径
     * @param {String} b.name 按钮名称
     * @param {String} b.title 按钮描述
     * @param {String} b.appendToType 按钮格式类型
     * @param {Object} b.appendToContainer 加载到哪个容器
     * @param {Function} b.callback 回调函数，在这里定义该按钮的事件
     */
    var addButton = function(b) {
      b.appendToContainer.prepend(createButtonHtml(b));
      var dom = p.maptoolContainer.find('.' + b.buttonType);
      hoverCreateButton(dom);
      if (b.callback) b.callback(dom, b.buttonType);
    };
    var hoverCreateButton = function(i) {
      i.hover(function() {
        $(this).addClass('cBlue');
      }, function() {
        $(this).removeClass('cBlue');
      });
    };

    var initDefaultButtons = function(container) {
      $(defaultButtons.reverse()).each(function(i, n) {
        this.appendToContainer = p.defaultButtonsContainer;
        this.appendToType = p.defaultButtonType || 0;
        this.callback = mapToolButtonClick;
        var d = this;
        addButton(d);
        // $(container).find('ul').append(createButtonHtml(this));
        // moreButtonsContainer.find('.' + this.buttonType).click(function() {
        //   mapToolButtonClick(n.buttonType);
        // });
      });
      p.maptoolContainer.find('.moreButton').click(function() {
        showHideDefaultButtons(this);
      });
      // hoverCreateButton(moreButtonsContainer.find('ul li'));
    };
    var mapToolButtonClick = function(dom, type) {
      $(dom).click(function(event) {
        // var target = event.target || srcElement;
        // if (!$(target).hasClass(type)) target = $(target).parents('.' + type);
        mapToolButtonClickEvent(type);
      });
    };
    var mapToolButtonClickEvent = function(type) {
      // 当点击的是当前正激活的工具时则不执行操作并返回
      if (type == activeButton) {
        return;
      }
      // 先结束先一次激活的操作
      if (control) {
        control.close();
        control = null;
      }
      switch (type) {
        case 'movemap':
          if (control) control.close();
          // map.setMapCursor(_map_cur[0], _map_cur[1]);
          break;
        case 'zoomin':
          // 拉框放大
          control = new TMZoomTool(map, false);
          // control.setCursor("crosshair");
          TMEvent.bind(control, "draw", control, removeControl);
          break;
        case 'distance':
          // 测距
          var option1 = new TMLineToolOptions();
          option1.map = map;
          option1.editable = true;
          control = new TMDistanceTool(option1);
          // control.setCursor("images/map/ruler.cur");
          // TMEvent.bind(control, "draw", map, removeControl);
          break;
        case 'zoomout':
          // 拉框缩小
          control = new TMZoomTool(map, true);
          // control.setCursor("crosshair");
          TMEvent.bind(control, "draw", control, removeControl);
          break;
        case 'cover':
          // 测面
          var option = new TMAreaToolOptions();
          option.map = map;
          control = new TMAreaTool(option);
          // control.setCursor("images/map/ruler.cur");
          // TMEvent.bind(control, "draw", map, removeControl);
          break;
        case 'savemap':
          // 截图
          control = new TMMapSnap(map);
          // TMMapSnap.bind(map);
          // TMEvent.bind(TMMapSnap.snapCtrl, "draw", TMMapSnap.snapCtrl, function(obj) {
          //   TMEvent.bind(obj, "btnclick", obj, function(type) { // 1-close 2-preview 3-save as
          //     map.setMapCursor(_map_cur[0], _map_cur[1]);
          //     if(type != 1) {
          //       map.removeControl(obj);
          //     }
          //   });
          // });
          break;
      }
      if (control) {
        control.open();
        activeButton = type;
        activeControl = control;
      } else {
        resetMapToolButton();
      }
      hiddMoreMapbtn();
    };
    var removeControl = function(ctrl) {
      if (control) {
        control.close();
        control = null;
        map.setMapCursor(_map_cur[0], _map_cur[1]);
      }
    };
    var resetMapToolButton = function() {
      activeButton = 'movemap';
      $(document.body).unbind("click");
    };
    var showHideDefaultButtons = function(o) {
      if (moreButtonsInHide) {
        p.moreButtonsContainer.removeClass("none");
        moreButtonsInHide = false;
        if (o) {
          o = $(o);
          p.moreButtonsContainer.css({
            top: o.height()
              // left : o.offset().left - 30
          });
          //当弹出提示框在添加关闭事件
          setTimeout(function() {
            $(document.body).click(function(e) {
              if ($(e.target).hasClass('moreButton')) return false;
              p.moreButtonsContainer.addClass("none");
              if (!moreButtonsInHide) {
                moreButtonsInHide = true;
              }
              $(document.body).unbind("click");
            });
          }, 10);
        }
      }
    };
    var hiddMoreMapbtn = function() {
      if (!moreButtonsInHide) {
        p.moreButtonsContainer.addClass("none");
        moreButtonsInHide = true;
      }
    };
    /**
     * [init 初始化默认工具条]
     * @param  {[Object]} p [参数对象]
     * @return {[type]}   [description]
     */
    var init = function(op) {
      p = $.extend({}, p || {}, op || {});
      map = p.cMap;
      initDefaultButtons(p.moreButtonsContainer);

      return this;
    };

    this.addButton = addButton;

    init(options);
  };
  return constructor;
})();


/**
 * [Events 观察者模式,发布者对象]
 *
 * @return {[Object]} [发布者对象]
 * @example 订阅者例子
 *          var adultTv = Events();
 *          adultTv.listen( 'play',  function( data ){
 *              alert ( "今天是谁的电影" + data.name );
 *          });
 *          //发布者
 *          adultTv.trigger('play', {'name': '麻生希'})
 */
CTFO.Util.Events = function() {
  var listen, log, obj, one, remove, trigger, __this;
  obj = {};
  __this = this;

  listen = function(key, eventfn) { //把简历扔盒子, key就是联系方式.
    var stack, _ref; //stack是盒子
    stack = (_ref = obj[key]) != null ? _ref : obj[key] = [];

    return stack.push(eventfn);
  };

  one = function(key, eventfn) {
    remove(key);
    return listen(key, eventfn);
  };

  remove = function(key) {
    var _ref;
    return (_ref = obj[key]) != null ? _ref.length = 0 : void 0;
  };

  trigger = function() { //面试官打电话通知面试者
    var fn, stack, _i, _len, _ref, key;
    key = Array.prototype.shift.call(arguments);
    stack = (_ref = obj[key]) != null ? _ref : obj[key] = [];

    for (_i = 0, _len = stack.length; _i < _len; _i++) {
      fn = stack[_i];
      if (fn.apply(__this, arguments) === false) {
        return false;
      }
    }
    return {
      listen: listen,
      one: one,
      remove: remove,
      trigger: trigger
    };
  };
};
/**
 * [MessageListBox 消息列表组件封装]
 * @author [fanshine124@gmail.com]
 * @param {[Object]} p [参数对象]
 * @return {[Object]} [消息列表组件对象]
 */
(function($) {
  $.fn.applyCtfoMessageListBox = function(p) {
    var defaults = {
      htmlFrame: 'model/template/message_list_box.htm',
      header: true,
      footer: true,
      header_title: '提示信息',
      // 企业资讯
      footer_title: '更多',
      footer_action: 'javascript:void(0);',
      header_icon: 'ioc110',
      header_css: 0,
      header_css_options: ['lineS06c radius3 bcFFF mb10', 'lineS69c radius3 bcFFF mb10'],
      title_css: 0,
      title_css_options: ['tit1 h30 lh25 pl10', 'tit2 h30 lh25 pl10'],
      title_desc: 0,
      title_desc_options: ['cFFF pt3 fb', 'pt3 fb'],
      tabs: ''
    };
    this.each(function(event) {
      p = $.extend({}, defaults, p || {});
      var g = {
        init: function() {
          var bottom = p.footer ? '<div class=" pr10 h25 lh25 tr cC00"><a href="' + p.footer_action + '">' + p.footer_title + '</a></div>' : '';
          var html = ['<div class="' + p.header_css_options[p.header_css] + ' messageListHeader">', '<div class="' + p.title_css_options[p.title_css] + '">', '<h3 class="' + p.title_desc_options[p.title_desc] + '"><span class="' + p.header_icon + '"></span>' + p.header_title + '</h3>', '</div>', '<div class="messageListContent">', '</div>', bottom, '</div>'];
          g.window = $(html.join(''));
          g.window.content = $(".messageListContent", g.window);
          g.window.header = $(".messageListHeader", g.window);
        }
      };
      $(this).append(g.window);

    });
  };
})(jQuery);

/**
 * [applyCtfoWindow 加载弹窗,top/left/bottom/right只需传入一对值,例如top,left]
 * @param  {[Object]} p [参数对象]
 * @param {[Number]} p.width [宽度]
 * @param {[Number]} p.height [高度]
 * @param {[Number]} p.top [距离浏览器上边的高度]
 * @param {[Number]} p.left [距离浏览器左边的高度]
 * @param {[Number]} p.bottom [距离浏览器下边的高度]
 * @param {[Number]} p.right [距离浏览器右边的高度]
 * @param {[String]} p.url [将要加载的静态页片段地址]
 * @param {[String]} [p.content] [将要加载的html字符串]
 * @param {[String]} p.title [弹窗标题]
 * @param {[String]} p.icon [弹窗icon]
 * @param {[String]} p.footer [弹窗底部按钮]
 * @param {[Object]} p.onLoad [加载完成后的回调函数,会将弹窗对象传入,可以据此比例弹窗内的dom并初始化内容]
 * @param {[Object]} p.onCloseWin [关闭弹窗的自定义事件]
 * @param {[Object]} p.data [传给弹窗的数据]
 * @return {[Null]}   [description]
 */
(function($) {
  $.fn.applyCtfoWindow = function(p) {
    var defaults = {
      htmlFrame: 'model/template/window.html',
      title: '提示信息',
      ico: '',
      width: 800,
      height: 600,
      top: document.body.offsetHeight / 2,
      left: document.body.offsetWidth / 2,
      footer: '',
      container: 'body',
      // 挂到哪个容器下
      mask: true //是否有遮罩
    };
    this.each(function() {
      p = $.extend({}, defaults, p || {});
      var html_content = this;
      // 公共方法
      var g = {
        /**
         * [loading 加载弹窗页面]
         * @return {[Null]} [无返回]
         */
        loading: function() {
          // $(html_content).load(p.htmlFrame, {}, function(){
          // , "<div class='tip_window_header'>", "<div class='tip_window_header_left'>", "<img src='" + p.icon + "' />", "</div>", "<div class='tip_window_header_center'>", "<label>" + p.title + "</label>", "</div>", "<div class='tip_window_header_right'>", "</div>", "</div>", "<div class='tip_window_content'></div>", "<div class='tip_window_footer'>", "<div class='tip_window_footer_left'></div>", "<div class='tip_window_footer_center'>" + p.footer + "</div>", "<div class='tip_window_footer_right'></div>", "</div>", "</div>"
          // var dialog = $('<div class="mauto bcFFF windowBox l-dialog"><div class="tit1 cFFF overh l-dialog-tc-inner"><div class="l-dialog-icon ' + p.ico + '"></div><h3 class="l-dialog-title"></h3><div class="l-dialog-winbtns"><div class="l-dialog-winbtn l-dialog-close"></div></div></div><div class="l-dialog-body"><div class="l-dialog-image"></div><div class="l-dialog-content"></div><div class=" bcBlue l-dialog-buttons"><div class="l-dialog-buttons-inner"></div></div></div></div>');
          var html = $('<div class="windowBox l-dialog user-defined-dialog"></div>');
          g.window = html;
          g.window.append('<div class="tit1 cFFF overh l-dialog-tc-inner"><div class="l-dialog-icon ' + p.icon + '"></div><h3 class="l-dialog-title">' + p.title + '</h3><div class="l-dialog-winbtns"><div class="l-dialog-winbtn l-dialog-close"></div></div></div><div class="l-dialog-body"><div class="l-dialog-image"></div><div class="l-dialog-content"></div><div class=" bcBlue l-dialog-buttons"><div class="l-dialog-buttons-inner"></div></div></div>');

          g.window.content = $(".l-dialog-body", g.window);
          g.window.header = $(".l-dialog-tc-inner", g.window);

          if (p.url) {
            $(g.window.content).load(p.url, {}, function() {
              var w = $(g.window);
              g.init(w);
              if (p.onLoad) p.onLoad(w, p.data, g);
            });
          } else if (p.content) {
            $(g.window.content).html(p.content);
            var w = $(g.window);
            g.init(w);
            if (p.onLoad) p.onLoad(w, p.data, g);
          }
          // });
        },
        /**
         * [init 初始化弹窗样式和基本事件]
         * @param  {[Object]} window [弹窗对象]
         * @return {[Null]}        [无返回]
         */
        init: function(window) {
          g.switchWindow(window[0]);
          $(p.container).append(window);

          g.window.show();

          //遮罩层
          if (p.mask) {
            g.mask(window);
          }

          // css
          if (p.left) window.css('left', p.left - p.width / 2);
          if (p.right) window.css('right', p.right);
          if (p.top) window.css('top', p.top - p.height / 2);
          if (p.bottom) window.css('bottom', p.bottom);
          if (p.width) window.width(p.width).find('.l-dialog-title').width(p.width - 75).end().find('.tip_window_content').width(p.width - 2).end().find('.tip_window_footer_center').width(p.width - 16);
          if (p.height) g.window.content.height(p.height - 28);
          if (p.title) $(".l-dialog-title", g.window.header).html(p.title);

          // 拖动支持
          if ($.fn.ligerDrag) {
            window.ligerDrag({
              handler: '.l-dialog-title',
              onStartDrag: function() {
                g.switchWindow(window[0]);
                window.addClass("l-window-dragging");
                g.window.content.children().hide();
              },
              onStopDrag: function() {
                window.removeClass("l-window-dragging");
                g.window.content.children().show();
              }
            });
          }
          // 关闭事件
          $('.l-dialog-close', g.window.header).click(function() {
            g.close();
            // g.unmask(window);
          });
        },
        /**
         * [switchWindow 允许多个弹窗的存在,可以切换弹窗在最上层展示]
         * @param  {[type]} window [弹窗对象]
         * @return {[Null]}        [无返回]
         */
        switchWindow: function(window) {
          $(window).css("z-index", "65537").siblings(".tip_window").css("z-index", "65536"); // transmap的地图认证信息的层次是65535
        },
        /**
         * @description 关闭弹窗
         * @param {Object}
         *            window
         */
        /**
         * [close 关闭弹窗]
         * @param  {[Object]} window [弹窗对象]
         * @return {[Null]}        [无返回]
         */
        close: function() {
          if (p.onCloseWin) p.onCloseWin(g.window, p.data, g);
          g.window.remove();
          g.unmask(g.window);
        },

        mask: function(win) {
          var maskObj = $('body > .user-defined-mask:visible');
          if (maskObj.length > 0) {
            return false;
          }

          function setHeight() {
            if (maskObj.length < 1) return;
            var h = $(window).height() + $(window).scrollTop();
            maskObj.height(h);
          }
          if (maskObj.length < 1) {
            maskObj = $("<div class='l-window-mask user-defined-mask' style='display: block;'></div>").appendTo('body');
            $(window).bind('resize.ligeruiwin', setHeight);
            $(window).bind('scroll', setHeight);
          }
          maskObj.show();
          setHeight();
        },

        //取消遮罩
        unmask: function(win) {
          var jwins = $("body > .user-defined-dialog:visible");
          var maskObj = $('body > .user-defined-mask:visible');
          if (maskObj && jwins.length === 0) maskObj.remove();
        }
      };
      // 私有方法
      var po = {

      };
      g.loading();
      // if (this.id == undefined || this.id == "") this.id = "CTFO_UI_" +
      // new Date().getTime();
      // CTFO.Util.UIManagers[this.id + "_Window"] = g;
    });
  };
})(jQuery);

/**
 * [模糊匹配车牌号]
 * @param  {[type]} $ [description]
 * @return {[type]}   [description]
 */
(function($) {
  var initMatchingVehicles = function(dom, param) {
    $(dom).bind('input propertychange', function() {
      if (param.eventTimer) clearTimeout(param.eventTimer);
      param.eventTimer = setTimeout(function() {
        queryMatchingVehicles(dom, param);
      }, param.eventTimerDelay);
    });
    $(param.container).on('click', function(event) {
      var target = event.target || window.srcElement;
      var vid = $(target).attr('vid'),
        vehicleno = $(target).text();
      if (vid) {
        $(dom).val(vehicleno);
        $(dom).siblings('input[name=' + (param.parseDom || 'vehicleId') + ']').val(vid);
        $(param.container).parent().addClass('none');
      }
    });
  };
  var queryMatchingVehicles = function(dom, param) {
    var keyword = $(dom).val(),
      params = {
        'requestParam.equal.entId': param.entId,
        'requestParam.like.vehicleNo': keyword
      };
    if (keyword.split('').length < 3) return false;
    $.ajax({
        url: param.url,
        type: 'POST',
        dataType: 'json',
        data: params,
      })
      .done(function(data) {
        if (!data || (data && !data.Rows) || (data && data.Rows && data.Rows.length < 1)) {
          //$.ligerDialog.error('请输入正确车牌号！');
          return false;
        }
        compileMatchingVehicles(data.Rows, param.container);
      })
      .fail(function(err) {
        //console.log("error");
      })
      .always(function(data) {
        //console.log("complete");
      });
  };
  var compileMatchingVehicles = function(data, container) {
    var optionsDom = [];
    $(data).each(function(index, el) {
      //modify by guoyuanhua 2014.1.20
      //如果找到匹配车牌,将第一个查询到的vid赋值到隐藏文本框里 <input type="text" class="" name="vehicleId" value="">
      //如果找到匹配车牌,将第一个查询到的vehicleNo赋值到车牌号码文本框里 <input type="text" class="w140" name="vehicleNo" value="">
      if (index == 0) {
        //console.log($(container).parent().siblings('input[name=vehicleId]'));
        $(container).parent().siblings('input[name=vehicleId]').val(this.vid);
        $(container).parent().siblings('input[name=vehicleNo]').val(this.vehicleNo);
      }
      optionsDom.push('<li vid="' + this.vid + '">' + this.vehicleNo + '</li>');
    });

    $(container).parent().removeClass('none');
    $(container).html(optionsDom.join(''));
    $(container).find('li').hover(function() {
      $(this).addClass('bcBlue');
    }, function() {
      $(this).removeClass('bcBlue');
    });
    //关闭模糊匹配
    setTimeout(function() {
      $(document.body).click(function(e) {
        $(".fuzzyMatchingVehicles").addClass('none');
        $(document.body).unbind("click");
      });
    }, 10);
  };
  // 通过字面量创造一个对象，存储我们需要的共有方法
  var methods = {
    // 在字面量对象中定义每个单独的方法
    init: function(options) {
      // 为了更好的灵活性，对来自主函数，并进入每个方法中的选择器其中的每个单独的元素都执行代码
      return this.each(function(i) {
        // 为每个独立的元素创建一个jQuery对象
        var $this = $(this);
        // 尝试去获取settings，如果不存在，则返回“undefined”
        var settings = $this.data('fuzzyMatchingVehicles');
        // 如果获取settings失败，则根据options和default创建它
        options = options || {};
        if (!options.container) {
          $this.after('<div class="h100 w150 lineS bcFFF  overa abs_left none fuzzyMatchingVehicles" style=" top:25px;z-index:1250;"><ul class="hand"></ul></div><input type="hidden" class="" name="vehicleId" value="">');
          options.container = $this.siblings('.fuzzyMatchingVehicles').find('ul');
        }
        if (typeof(settings) == 'undefined') {
          var defaults = {
            url: CTFO.config.sources.getMatchingVehicles,
            entId: CTFO.cache.user.entId,
            eventTimer: null,
            eventTimerDelay: 1200
          };
          settings = $.extend({}, defaults, options);
          // 保存我们新创建的settings
          $this.data('fuzzyMatchingVehicles', settings);
        } else {
          // 如果我们获取了settings，则将它和options进行合并（这不是必须的，你可以选择不这样做）
          settings = $.extend({}, settings, options);
          // 如果你想每次都保存options，可以添加下面代码：
          // $this.data('fuzzyMatchingVehicles', settings);
        }
        // 执行代码
        initMatchingVehicles($this, settings);
      });
    },
    destroy: function() {
      // 对选择器每个元素都执行方法
      return this.each(function() {
        // 执行代码
        // 删除元素对应的数据
        $this.removeData('fuzzyMatchingVehicles');
      });
    }
  };
  $.fn.fuzzyMatchingVehicles = function() {
    var method = arguments[0];
    // 检验方法是否存在
    if (methods[method]) {
      // 如果方法存在，存储起来以便使用
      // 注意：我这样做是为了等下更方便地使用each（）
      method = methods[method];
      // 如果方法不存在，检验对象是否为一个对象（JSON对象）或者method方法没有被传入
    } else if (typeof(method) == 'object' || !method) {
      // 如果我们传入的是一个对象参数，或者根本没有参数，init方法会被调用
      method = methods.init;
    } else {
      // 如果方法不存在或者参数没传入，则报出错误。需要调用的方法没有被正确调用
      $.error('Method ' + method + ' does not exist on jQuery.fuzzyMatchingVehicles');
      return this;
    }
    return method.apply(this, arguments);
  };
})(jQuery);

/**
 * 密码修改
 */
CTFO.Model.passwordWindow = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var $form = null;
    var validateForm = null;

    /**
     * [checkPwdMode 验证的格式]
     * @param {[type]} d []
     */
    var checkPwdMode = function(value) {
      var reg = new RegExp("^[_#!$@\%\^\&\*\(\)\>\<\/\\da-zA-Z0-9]*$", "g");
      var reg2 = new RegExp("^[0-9]*$", "g");
      var reg3 = new RegExp("^[a-zA-Z]*$", "g");
      var reg4 = new RegExp("^[_#!@$\%\^\&\*\(\)\>\<\/]*$", "g");
      var rs = value.search(reg);
      var rs2 = value.search(reg2);
      var rs3 = value.search(reg3);
      var rs4 = value.search(reg4);
      return (rs != -1 && rs2 == -1 && rs3 == -1 && rs4 == -1) ? true : false;
    };

    //初始化日志服务
    if (window.CommonsLog) {
      cLog = CommonsLog.getInstance();
    } else {
      cLog = {
        addOperatorLog: function() {},
        addMenuLog: function() {}
      };
    }

    /**
     * 初始化密码修改判断条件
     */
    var initValidate = function() {
      validateForm = $form.validate({
        rules: {
          oldPassword: {
            required: true
          },
          newPassword: {
            required: true,
            rangelength: [7, 16],
            checkInput: []
          },
          newPasswordRepeat: {
            required: true,
            equalTo: '#newPassword'
          }
        },
        messages: {
          oldPassword: {
            required: '请输入密码',
          },
          newPassword: {
            required: '请输入新密码',
            rangelength: '输入字符需要7-16个'
          },
          newPasswordRepeat: {
            required: '请重复输入新密码',
            equalTo: '两次输入的密码不一致'
          }
        }
      });

      $.validator.addMethod('checkInput', function(value, element, params) {
        return checkPwdMode(value);
      }, '字符,字母和数字，至少有两种');
    };

    /**
     * 弹出重置密码框
     */
    var popResetPasswordWin = function() {
      var param = {
        title: '重置密码',
        icon: 'icon221',
        width: 400,
        height: 180,
        url: CTFO.config.template.passwordModify,
        onLoad: function(w, d, g) {
          $form = $(w).find('form[name=passwordModify]');
          initValidate();
          bindSubmitFormEvent(w, g);
        }
      };

      param = param || {};
      if (param.url || param.content) {
        var win = $('<div>');
        $(win).applyCtfoWindow($.extend({}, param));
      }
    };

    /**
     * 提交表单
     */
    var bindSubmitFormEvent = function(w, g) {
      $form.find('span[name=updatePassword]').bind('click', function(evt) {
        var isPass = validateForm.form();
        if (isPass) {
          g.close(w);
          $form.find('.l-dialog-close').trigger('click');
          $.ligerDialog.confirm('确定要修改密码？', function(yes) {
            if (yes) {
              var oldPw = $.trim($form.find('input[name=oldPassword]').val()),
                newPw = ($.trim($form.find('input[name=newPassword]').val().toLowerCase())),
                newPwRepeat = ($.trim($form.find('input[name=newPasswordRepeat]').val().toLowerCase()));
              var param = {
                "oldPassword": hex_sha1(oldPw),
                "retNewPassword": hex_sha1(newPwRepeat)
              };
              $.ajax({
                url: CTFO.config.sources.passwordModify,
                type: 'POST',
                dataType: 'json',
                data: param,
                complete: function(xhr, textStatus) {},
                success: function(data, textStatus, xhr) {
                  if (data && data.error && data.error[0].errorMessage) {
                    cLog.addOperatorLog({
                      opId: CTFO.cache.user.opId, // 操作用户ID(必填)
                      opName: CTFO.cache.user.opName, // (必填)
                      entId: CTFO.cache.user.entId, // 所属组织Id//(必填)
                      entName: CTFO.cache.user.entName, // 组织名称(必填)
                      funCbs: CTFO.cache.systemType,
                      funId: '', // 如果登录后进入默认模块时需要有模块ID
                      opType: '修改密码', // 登录/登出系统
                      logTypeId: 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作
                      logClass: 'CTFO.Model.FrameManager', // 类名称
                      logMethod: 'modifyPassword', // 执行方法
                      logDesc: '操作失败' // 操作成功/操作失败
                    });
                    $.ligerDialog.error(data.error[0].errorMessage);
                  } else {
                    cLog.addOperatorLog({
                      opId: CTFO.cache.user.opId, // 操作用户ID(必填)
                      opName: CTFO.cache.user.opName, // (必填)
                      entId: CTFO.cache.user.entId, // 所属组织Id//(必填)
                      entName: CTFO.cache.user.entName, // 组织名称(必填)
                      funCbs: CTFO.cache.systemType,
                      funId: '', // 如果登录后进入默认模块时需要有模块ID
                      opType: '修改密码', // 登录/登出系统
                      logTypeId: 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作
                      logClass: 'CTFO.Model.FrameManager', // 类名称
                      logMethod: 'modifyPassword', // 执行方法
                      logDesc: '操作成功' // 操作成功/操作失败
                    });
                    $.ligerDialog.success(data.displayMessage);
                  }
                  g.close(w);
                },
                error: function(xhr, textStatus, errorThrown) {
                  cLog.addOperatorLog({
                    opId: CTFO.cache.user.opId, // 操作用户ID(必填)
                    opName: CTFO.cache.user.opName, // (必填)
                    entId: CTFO.cache.user.entId, // 所属组织Id//(必填)
                    entName: CTFO.cache.user.entName, // 组织名称(必填)
                    funCbs: CTFO.cache.systemType,
                    funId: '', // 如果登录后进入默认模块时需要有模块ID
                    opType: '修改密码', // 登录/登出系统
                    logTypeId: 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作
                    logClass: 'CTFO.Model.FrameManager', // 类名称
                    logMethod: 'modifyPassword', // 执行方法
                    logDesc: '操作失败' // 操作成功/操作失败
                  });
                  $.ligerDialog.error('修改密码失败');
                  g.close(w);
                }
              });
            }
          });
        }
      });
    }

    return {
      popResetPasswordWin: function(options) {
        p = $.extend({}, p || {}, options || {});
        popResetPasswordWin();
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