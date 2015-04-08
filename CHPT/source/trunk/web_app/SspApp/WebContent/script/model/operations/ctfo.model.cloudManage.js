/**
 * [ 业务管理 - 云备份管理]                                                                                        resize(ch [description]
 * @return {[type]}         [description]
 */
CTFO.Model.CloudManage = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var cHeight = 0,
      minH = 520, // 本模块最低高度
      gridHeight = 300, // grid高度
      pageSize = 30,
      pageSizeOption = [10, 20, 30, 40],

      treeContainer = null,
      cloudBackUpForm = null,
      vehicleTeamTerm = null,
      gridContainer = null,

      leftTree = null,
      grid = null,
      addFlag = null,
      currentEntName = '',
      clound_backup_detail_tmpl = null,
      vehicleTeamDetailTmpl = null,
      vehicleTeamModifyTmpl = null,
      gridContainerWrap = null,

      updateRowAuth = 'FG_MEMU_BUSINESS_CLOUDMANAGE_U', // 修改记录权限
      detailRowAuth = 'FG_MEMU_BUSINESS_CLOUDMANAGE_I', // 查看记录详情权限
      deleteRowAuth = 'FG_MEMU_BUSINESS_CLOUDMANAGE_D', // 删除记录权限
      addRowAuth = 'FG_MEMU_BUSINESS_CLOUDMANAGE_C', // 新增记录权限
      exportRowAuth = 'FG_MEMU_BUSINESS_CLOUDMANAGE_E', // 导出记录权限
      opLogRowAuth = 'FG_MEMU_BUSINESS_CLOUDMANAGE_O', // 操作记录
      isPassByValueMode = CTFO.config.globalObject.isPassByValueMode, //传值方式
      test = '';
    // grid展现列
    var columns = [{
        display: '公司编码',
        name: 'comCode',
        width: 100,
        sortable: true,
        align: 'center',
        toggle: false
      }, {
        display: '公司名称',
        name: 'comName',
        width: 100,
        sortable: true,
        align: 'center'
      }, {
        display: '帐套名称',
        name: 'setbookName',
        width: 100,
        sortable: true,
        align: 'center'
      }, {
        display: '云备份时间',
        name: 'createTime',
        width: 100,
        sortable: true,
        align: 'center',
        render: function(row) {
          return CTFO.utilFuns.dateFuns.utc2date(row.createTime);
        }
      }, {
        display: '云空间大小',
        name: 'cloudSize',
        width: 100,
        sortable: true,
        align: 'center',
        render: function(row) {
          return row.cloudSize + "G";
        }
      }, {
        display: '云空间有效期',
        name: 'cloudValidTime',
        width: 100,
        sortable: true,
        align: 'center',
        render: function(row) {
          return CTFO.utilFuns.dateFuns.utc2date(row.cloudValidTime);
        }
      }, {
        display: '已用空间',
        name: 'usedSpace',
        width: 100,
        sortable: true,
        align: 'center',
        render: function(row) {
          return row.usedSpace + "G";
        }
      }, {
        display: '可用空间',
        name: 'remainSpace',
        width: 160,
        sortable: true,
        align: 'center',
        render: function(row) {
          if (parseInt(row.cloudSize) > parseInt(row.usedSpace))
            return (parseInt(row.cloudSize) - parseInt(row.usedSpace)) + "G";
        }
      }, {
        display: '文件数目',
        name: 'fileNums',
        width: 100,
        sortable: true,
        align: 'center'
      },
      /* {
           display: '备注',
           name: 'remark',
           width: 100,
           sortable: true,
           align: 'center'
         },*/
      {
        display: '操作',
        name: 'entType',
        width: 120,
        sortable: true,
        align: 'center',
        render: function(row) {
          var buttons = [];

          if ($.inArray(detailRowAuth, CTFO.cache.auth) > -1) {
            var detailHtml = "<span class='cBlue'><font title='查看' class='hand' actionType='rowDetail'>查看</font></span>&nbsp;";
            detailHtml = ($.inArray(detailRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
            buttons.push(detailHtml);
          }
          if ($.inArray(detailRowAuth, CTFO.cache.auth) > -1) {
            var detailHtml = "<span class='cBlue'><font title='删除' class='hand' actionType='deleteRow'>删除</font></span>&nbsp;";
            detailHtml = ($.inArray(deleteRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
            buttons.push(detailHtml);
          }

          return buttons.join('');
        }
      }
    ];
    // grid初始化参数
    var gridOptions = {
      columns: columns,
      sortName: 'createTime',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      url: CTFO.config.sources.findCloudList, // 数据请求地址云备份查询
      pageSize: pageSize,
      pageSizeOption: pageSizeOption,
      pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
      pagesizeParmName: 'requestParam.rows',
      width: '100%',
      height: gridHeight,
      delayLoad: true,
      rownumbers: true,
      checkbox: true,
      allowUnSelectRow: true,
      onSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onUnSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onSuccess: function(data) {

      }
    };
    /**
     * [bindGridRowEvent 绑定grid行事件]
     * @param  {[Object]} rowData   [行数据]
     * @param  {[Integer]} rowIndex [行标]
     * @param  {[Object]} rowDom    [行dom对象]
     * @param  {[Object]} eDom      [点击触发dom对象]
     * @return {[Boolean]}          [是否继续]
     */
    var bindGridRowEvent = function(rowData, rowIndex, rowDom, eDom) {
      var actionType = $(eDom).attr('actionType');
      switch (actionType) {
        case 'modifyRow':
          modifyVehicleTeam(rowData.entId);

          //日志统计
          cLog.addOperatorLog($.extend({}, CTFO.cache.cLogInfo, {
            opType: '修改车队信息', // (必填)
            logTypeId: 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作(必填)
            logClass: 'CTFO.Model.VehicleTeamManage', //类名称
            logMethod: 'modifyVehicleTeam', // 执行方法
            executeTime: '', // 调用方法执行时间毫秒
            logDesc: '' // 操作成功/操作失败
          }));
          break;
        case 'deleteRow':
          //alert("delete!!!!" + rowData.cloudId);
          deleteCloudyBackup(rowData.cloudId);
          //日志统计
          cLog.addOperatorLog($.extend({}, CTFO.cache.cLogInfo, {
            opType: '删除车队信息', // (必填)
            logTypeId: 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作(必填)
            logClass: 'CTFO.Model.VehicleTeamManage', //类名称
            logMethod: 'deleteVehicleTeam', // 执行方法
            executeTime: '', // 调用方法执行时间毫秒
            logDesc: '' // 操作成功/操作失败
          }));

          break;
        case 'rowDetail':
          //        	var haha = rowData.cloudId;
          //        	alert("====" + haha); showVehicleTeamDetail
          showCloudBackUpDetail(rowData.cloudId); //该方法传入云备份主键id
          //日志统计
          cLog.addOperatorLog($.extend({}, CTFO.cache.cLogInfo, {
            opType: '查看云备份信息', // (必填)
            logTypeId: 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作(必填)
            logClass: 'CTFO.Model.VehicleTeamManage', //类名称
            showCloudBackUpDetail: 'showCloudBackUpDetail', // 执行方法
            executeTime: '', // 调用方法执行时间毫秒
            logDesc: '' // 操作成功/操作失败
          }));
          break;
      }
      return !actionType;
    };
    /**
     * [inisertVehicleTeam 新增车队]
     * @return {[type]} [description]
     */
    var inisertVehicleTeam = function() {
      var defaultData = {
          parentName: cloudBackUpForm.find('.parentCorpDesc').text(),
          corpCode: CTFO.cache.user.corpCode,
          parentId: cloudBackUpForm.find('input[name=parentId]').val()
        },
        doTtmpl = doT.template(vehicleTeamModifyTmpl),
        content = doTtmpl(defaultData);
      var param = {
        title: "新增组织",
        ico: 'ico226',
        width: 650,
        height: 410,
        content: content,
        onLoad: function(w, d, g) {
          //新增的时候，组织编码不能修改.默认为当前登录用户的编码
          var grandpaId = cloudBackUpForm.find('input[name=grandpaId]').val();
          if (grandpaId !== "-1") {
            $(w).find("input[name='corpCode']").attr("disabled", true);
          }
          initVehicleDetailUpdate('c', w, d, g); // 填充新增弹窗内容
        }
      };
      CTFO.utilFuns.tipWindow(param);
    };
    /**
     * [deleteCloudyBackup 删除云备份记录]
     * @param  {[String]}  orgId  [云备份id]
     * @return {[type]} [description]
     */
    var deleteCloudyBackup = function(orgId) {
      $.ligerDialog.confirm('是否删除该云备份?', function(yes) {
        if (!yes) return false;
        $.ajax({
          url: CTFO.config.sources.deleteCloudyBackup,
          type: 'POST',
          dataType: 'json',
          data: {
            cloudId: orgId
          },
          complete: function(xhr, textStatus) {
            //called when complete
          },
          success: function(data, textStatus, xhr) {
            if (data && data.displayMessage === "success") {
              grid.loadData(true);
              $.ligerDialog.success("删除操作成功");
            } else {
              $.ligerDialog.success(data.opInfo);
            }
          },
          error: function(xhr, textStatus, errorThrown) {
            $.ligerDialog.error(xhr.displayMessage);
          }
        });
      });
    };
    /**
     * [showCloudBackUpDetail 显示云备份详情]
     * @param  {[String]} orgId [组织id]
     * @return {[type]}       [description]
     */
    var showCloudBackUpDetail = function(orgId) {
      getCloudBackUpDetail(orgId, clound_backup_detail_tmpl, function(content) {
        var param = {
          title: "云备份详情",
          ico: 'ico226',
          width: 650,
          height: 285,
          content: content
        };
        CTFO.utilFuns.tipWindow(param);
      });
    };
    /**
     * [getCloudBackUpDetail 获取云备份详情]
     * @param  {[String]}   orgId    [云备份主键id]
     * @param  {[Object]}   tmpl     [详情模板对象]
     * @param  {Function} callback [回调函数]
     * @return {[type]}            [description]
     */
    var getCloudBackUpDetail = function(orgId, tmpl, callback) {
      //    	alert(orgId + "==传到后台的云备份主键值");
      $.get(CTFO.config.sources.findById, {
        cloudId: orgId
      }, function(data, textStatus, xhr) {
        var content = '';
        if (typeof(data) === 'string') data = JSON.parse(data);
        if (data && !data.error) content = compileCloudBackUpDetail(data, tmpl);
        if (callback) callback(content, data);
      });
    };
    /**
     * [compileCloudBackUpDetail] 渲染云备份详情弹窗]
     * @param  {[Object]} d    [数据对象]
     * @param  {[Object]} tmpl [详情模板对象]
     * @return {[type]}      [description]
     */
    var compileCloudBackUpDetail = function(d, tmpl) {
      var doTtmpl = doT.template(tmpl);
      //      alert("渲染模板：" + d.cloudId);
      //      d.entTypeDesc = (+d.entType === 1 ? '企业' : (+d.entType === 2 ? '车队' : ''));
      //      d.provinceDesc = CTFO.utilFuns.codeManager.getNameByCode('SYS_AREA_INFO', d.corpProvince);
      //      d.cityDesc = CTFO.utilFuns.codeManager.getCityProvcodeNameByCode('SYS_AREA_INFO', d.corpProvince, d.corpCity);
      //      d.corpQualeDesc = CTFO.utilFuns.codeManager.getNameByCode('SYS_CORP_BUSINESS_SCOPE', d.corpQuale);
      //      d.corpLevelDesc = CTFO.utilFuns.codeManager.getNameByCode('SYS_CORP_LEVEL', d.corpLevel);
      return doTtmpl(d);
    };
    /**
     * [modifyVehicleTeam 修改车队信息]
     * @param  {[String]} orgId [组织id]
     * @return {[type]}       [description]
     */
    var modifyVehicleTeam = function(orgId) {
      getCloudBackUpDetail(orgId, vehicleTeamModifyTmpl, function(content, data) {
        var param = {
          title: "修改组织信息",
          ico: 'ico226',
          width: 650,
          height: 410,
          content: content,
          onLoad: function(w, d, g) {
            initVehicleDetailUpdate('u', w, d, g); // 填充车队修改弹窗内容
            $(w).find('input[name=corpCode]').attr('readonly', 'readonly');
            $(w).find('select[name=entType]').attr('disabled', 'disabled');
          },
          data: data
        };
        CTFO.utilFuns.tipWindow(param);
      });
    };
    /**
     * [initVehicleDetailUpdate 绑定新增/更新弹窗事件]
     * @param  {[String]} t [类别，c:新增，u:更新]
     * @param  {[Object]} w [弹窗Dom对象]
     * @param  {[Object]} d [数据对象]
     * @param  {[Object]} g [弹窗对象]
     * @return {[type]}   [description]
     */
    var initVehicleDetailUpdate = function(t, w, d, g) {
      if (d) {
        currentEntName = d.entName;
      }
      var actionUrl = t === 'c' ? CTFO.config.sources.insertVehicleTeam : (t === 'u' ? CTFO.config.sources.updateVehicleTeam : '');
      if (!actionUrl) return false;
      initVehicleTeamDetailUpdate(w, d || {});
      var validator = validateFormParams(w);
      $(w).find('.saveButton').click(function(event) {
          var obj = this
          if ($(obj).attr("disabled")) {
            return false;
          }

          $(obj).attr("disabled", true);
          if (!validateEntName(t, w)) {
            $(obj).attr("disabled", false);
            return false;
          }

          var d = $(w).find('form[name=vehicleTeamModifyForm]').serializeArray();
          var corpCode = $(w).find('form[name=vehicleTeamModifyForm]').find("input[name='corpCode']").val();
          if (corpCode == "") {
            corpCode = CTFO.cache.user.entId;
          }
          var param = {};
          $(d).each(function(event) {
            param['viewOrgCorp.' + this.name] = this.value;
          });
          param['viewOrgCorp.corpCode'] = corpCode;

          var validated = validator.form();
          if (!validated) {
            $(obj).attr("disabled", false);
            return false;
          }

          saveVehicleTeamDetailFrom(actionUrl, param, function() {
            $(obj).attr("disabled", false);
            g.close();
            cloudBackUpForm.find('.queryButton').trigger('click');
          });
        }).end()
        .find('.cancelButton').click(function(event) {
          $(w).find('.saveButton').attr("disabled", false);
          g.close();
        });
    };
    /**
     * [initVehicleTeamDetailUpdate 初始化新增/更新弹窗的异步填充内容]
     * @param  {[Dom]} w [弹窗对象]
     * @param  {[Object]} d [数据对象]
     * @return {[type]}   [description]
     */
    var initVehicleTeamDetailUpdate = function(w, d) {
      var provinceOption = $(w).find('select[name=corpProvince]'),
        cityOption = $(w).find('select[name=corpCity]'),
        corpQuale = $(w).find('select[name=corpQuale]'),
        corpLevel = $(w).find('select[name=corpLevel]');
      $(w).find('select[name=entType]')[0].selectedIndex = d.entType - 1;
      CTFO.utilFuns.codeManager.getProvAndCity(provinceOption, cityOption, d.corpCity, d.corpProvince);
      CTFO.utilFuns.codeManager.getSelectList('SYS_CORP_BUSINESS_SCOPE', corpQuale, d.corpQuale);
      CTFO.utilFuns.codeManager.getSelectList('SYS_CORP_LEVEL', corpLevel, d.corpLevel);
    };
    var validateFormParams = function(w) {
      var validator = $(w).find('form[name=vehicleTeamModifyForm]').validate({
        // $('#vehicleTeamModifyForm').validate({
        errorClass: 'myselfError',
        rules: {
          corpCode: {
            required: true,
            specialchars: true,
            maxlength: 10
          },
          entName: {
            required: true,
            specialchars: true,
            maxlength: 50
          },
          orgShortname: {
            required: true,
            specialchars: true,
            maxlength: 20
          },
          entType: {
            required: true
          },
          corpProvince: {
            required: true
          },
          corpCity: {
            required: true
          },
          orgCmail: {
            emailExtend: true,
            maxlength: 40
          },
          orgCzip: {
            zipcode: true
          },
          corpQuale: {
            required: true
          },
          corpLevel: {
            required: true
          },
          orgAddress: {
            specialchars: true,
            maxlength: 40
          },
          url: {
            url: true,
            maxlength: 50
          },
          orgCname: {
            maxlength: 10,
            specialchars: true,
          },
          orgCphone: {
            mobilePhoneNum: true,
            maxlength: 20
          },
          orgCfax: {
            phonenumber: true
          },
          businessLicense: {
            maxlength: 15
          },
          orgMem: {
            maxlength: 100,
            specialchars: true,
          }
        },
        submitHandler: function() {
          //alert(123);
        }
      });
      return validator;
    };
    /**
     * [saveVehicleTeamDetailFrom 更新车队信息，提交服务器]
     * @param  {[String]}   url      [请求action]
     * @param  {[Object]}   param    [请求参数]
     * @param  {Function} callback [回调函数]
     * @return {[type]}            [description]
     */
    var saveVehicleTeamDetailFrom = function(url, parms, callback) {
      var newParms = {};
      $.each(parms, function(name, val) {
        if (name.indexOf(".") > -1) {
          newParms[name.split(".")[1]] = val;
        } else {
          newParms[name] = val;
        }
      });
      $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        data: isPassByValueMode ? JSON.stringify(newParms) : parms,
        contentType: isPassByValueMode ? 'application/json; charset=utf-8' : 'application/x-www-form-urlencoded;charset=UTF-8',
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if (data && callback) callback();
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };

    /**
     * 后台AJAX验证
     *
     * @param container
     *            数据dom对象
     */
    var validateEntName = function(t, container) {
      var parms = {};
      var entName = $(container).find('form[name=vehicleTeamModifyForm]').find("input[name='entName']")
        //var cardNo = $(container).find('input[name="icCard.cardNo"]');//驾驶员IC卡 卡号
      if (t != 'c' && currentEntName === $.trim(entName.val())) //修改状态 下的 驾驶员IC卡号 不同才做验证
        return true;
      var validateObj = new Object(); //验证对象
      parms["requestParam.equal.entName"] = $.trim(entName.val()); //驾驶证档案号 验证是否唯一
      validateObj.obj = [entName];
      validateObj.url = CTFO.config.sources.checkEntNameExist;
      validateObj.parms = parms;
      validateObj.message = "已经存在";
      if (0 < $.trim(entName.val()).length && !validateAjax(validateObj)) {
        return false; //返回 停止继续验证
      }

      return true;
    };

    /**
     * 验证Ajax抽象方法
     *
     * @param validateObj
     *            验证参数
     * @returns {Boolean}
     */
    var validateAjax = function(validateObj) {
      var v = false;
      $.ajax({
        url: validateObj.url,
        type: 'post',
        dataType: 'text',
        async: false,
        data: validateObj.parms,
        error: function() {
          $.ligerDialog.error("验证失败");
        },
        success: function(r) {
          r = JSON.parse(r);
          if (r && r.displayMessage) r = r.displayMessage;
          if ("success" === r) { //后台数据 返回 success 表示 该数据 已经存在
            for (var i = 0; i < validateObj.obj.length; i++) {
              var obj = validateObj.obj[i];
              obj.parent().find("label").remove();
              obj.after($("<label class=\"myselfError\">" + validateObj.message + "</label>"));
            }
          } else {
            v = true;
          }
        }
      });
      return v;
    };
    /**
     * [bindEvent 绑定全局事件]
     * @return {[type]} [description]
     */
    var bindEvent = function() {
      cloudBackUpForm.find('.addVehicleTeam').click(function(event) {
          inisertVehicleTeam();
          //日志统计
          cLog.addOperatorLog($.extend({}, CTFO.cache.cLogInfo, {
            opType: '新增车队信息', // (必填)
            logTypeId: 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作(必填)
            logClass: 'CTFO.Model.VehicleTeamManage', //类名称
            logMethod: 'inisertVehicleTeam', // 执行方法
            executeTime: '', // 调用方法执行时间毫秒
            logDesc: '' // 操作成功/操作失败
          }));
        }).end()
        .find('.exportGrid').click(function(event) {
          CTFO.utilFuns.commonFuns.exportGrid({
            grid: grid,
            url: CTFO.config.sources.exportCloudManageExcelData
          });

          //日志统计
          cLog.addOperatorLog($.extend({}, CTFO.cache.cLogInfo, {
            opType: '导出车队信息列表', // (必填)
            logTypeId: 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作(必填)
            logClass: 'CTFO.Model.VehicleTeamManage', //类名称
            logMethod: 'CTFO.utilFuns.commonFuns.exportGrid', // 执行方法
            executeTime: '', // 调用方法执行时间毫秒
            logDesc: '' // 操作成功/操作失败
          }));

        });
      $("select[name=corpQuale]").live('change', function() {
        var corpQualeVal = $("select[name=corpQuale]").val();
        if ('103' == corpQualeVal) {

          var selCol = $("select[name=entType]");
          $(selCol).empty();
          var downSel = "<option value=\"1\">企业</option>" + "<option value=\"2\">车队</option>" +
            "<option value=\"3\">门店</option>";
          $(selCol).append(downSel);
        } else {
          var selCol = $("select[name=entType]");
          $(selCol).empty();

          var downSel = "<option value=\"1\">企业</option>" + "<option value=\"2\">车队</option>";
          $(selCol).append(downSel);
        }
      });
    };
    /**
     * [initTreeContainer 初始化左侧树]
     * @return {[type]} [description]
     */
    var initTreeContainer = function() {
      var options = {
        container: treeContainer,
        menus: CTFO.cache.menus
      };
      leftTree = new CTFO.Model.Accordion(options);
    };
    var selectOrgTreeNode = function(d, columns) {
      cloudBackUpForm.find('.parentCorpDesc').text(d[columns.text]).end()
        .find('input[name=parentId]').val(d[columns.id]).end()
        .find('input[name=grandpaId]').val(d["parentId"]);
    };
    /**
     * [initForm 初始化查询条件form]
     * @return {[type]} [description]
     */
    var initForm = function() {
      //初始化时，需要将当前用户的企业信息添加到所属企业和parentId字段，用于查询
      cloudBackUpForm.find('.parentCorpDesc').text(CTFO.cache.user.entName).end()
        .find('input[name=parentId]').val(CTFO.cache.user.entId);
      $(cloudBackUpForm).find('input[name=createTimeStart]').click(function(event) {
        WdatePicker({
          dateFmt: 'yyyy-MM-dd',
          isShowClear: false
        });
      });
      $(cloudBackUpForm).find('input[name=createTimeEnd]').click(function(event) {
        WdatePicker({
          dateFmt: 'yyyy-MM-dd',
          isShowClear: false
        });
      });
      var provinceOption = cloudBackUpForm.find('select[name=corpProvince]'),
        cityOption = cloudBackUpForm.find('select[name=corpCity]');
      CTFO.utilFuns.codeManager.getProvAndCity(provinceOption, cityOption);
      cloudBackUpForm.find('.queryButton').click(function(event) {
        searchGrid();
      }).end().
      find('.resetButton').click(function(event) {
        resetThis();
      }).end().
      find('.removeButton').click(function(event) {
        onRemove();
      }).end().
      find('.operaterLogButton').click(function(event) {
        CTFO.cache.frame.changeModel('operaterLogManage', '', null, 0);
      });
    };
    /**
     * [initGrid 初始化grid对象]
     * @return {[type]} [description]
     */
    var onRemove = function() {
      var record = grid.getCheckedRows();
      if (record.length <= 0) {
        $.ligerDialog.error('请勾选操作记录!');
        return;
      }
      var arr = _.map(record, function(item) {
        return item.comId
      });
      if (arr.length > 0) {
        deleteCloudyBackup(arr.join(","));
      }
    };
    /**
     * @description 清空表单
     */
    var resetThis = function() {
      $(cloudBackUpForm).find('input[type="text"]').each(function() {
        $(this).val("");
      }).end().find('select').each(function() {
        $(this).val("");
      }).end().find('textarea').each(function() {
        $(this).val("");
      }).end();

    };
    /**
     * [initGrid 初始化grid对象]
     * @return {[type]} [description]
     */
    var initGrid = function() {
      grid = gridContainer.ligerGrid(gridOptions);
    };

    /**
     * [searchGrid 车队管理查询事件]
     * @return {[type]} [description]
     */
    var searchGrid = function() {
      if (!grid) return false;
      var op = validateParams();
      grid.setOptions({
        parms: op
      });
      grid.loadData(true);
    };
    /**
     * [validateParams 获取查询参数]
     * @return {[type]} [description]
     */
    var validateParams = function() {
      var param = [],
        data = cloudBackUpForm.serializeArray();
      $(data).each(function(event) {
        var name = '';
        if (this.name === 'corpCode' || this.name === 'entName') name = 'requestParam.like.' + this.name;
        else name = 'requestParam.equal.' + this.name;
        if (this.value) {
          if (name.indexOf("TimeStart") > 0) {
            this.value += " 00:00:00"
          }
          if (name.indexOf("TimeEnd") > 0) {
            this.value += " 23:59:59"
          }
          param.push({
            name: name,
            value: this.value
          });
        }
      });
      return param;
    };

    var resize = function(ch) {
      if (ch < minH) ch = minH;
      p.mainContainer.height(ch);
      gridContainerWrap.height(p.mainContainer.height() - pageLocation.outerHeight() - vehicleTeamTerm.outerHeight() - parseInt(gridContainerWrap.css('margin-top')) * 3 - parseInt(gridContainerWrap.css('border-top-width')) * 2);
      gridHeight = gridContainerWrap.height();
      gridOptions.height = gridHeight;
      if (grid) grid = gridContainer.ligerGrid({
        height: gridHeight
      });

      treeContainer.height(ch);

      if (leftTree) leftTree.resize();
    };

    /**
     * @description 初始化权限Button
     * @param container
     */
    var initAuth = function(container) {

      // 删除
      if ($.inArray(deleteRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.removeButton').remove();
      }
      // 导出
      if ($.inArray(exportRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.exportGrid').remove();
      }
      // 操作记录
      if ($.inArray(opLogRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.operaterLogButton').remove();
      }

    };

    return {
      init: function(options) {
        p = $.extend({}, p || {}, options || {});
        pageLocation = p.mainContainer.find('.pageLocation');
        treeContainer = p.mainContainer.find('.leftTreeContainer');
        vehicleTeamTerm = p.mainContainer.find('.vehicleTeamTerm');
        cloudBackUpForm = p.mainContainer.find('form[name=cloudBackUpForm]');
        gridContainer = p.mainContainer.find('.gridContainer');
        gridContainerWrap = p.mainContainer.find('.gridContainerWrap');
        //云备份详情
        clound_backup_detail_tmpl = $('#clound_backup_detail_tmpl').html(); //获取index.html页面的ID为clound_backup_detail_tmpl的script标签
        vehicleTeamModifyTmpl = $('#vehicle_team_update_tmpl').html();

        resize(p.cHeight);

        bindEvent();
        initTreeContainer();
        initForm();
        initGrid();
        initAuth(cloudBackUpForm);

        // 该功能暂时不用
        alert("对不起，该功能暂不可用!\n原因：慧修车软件数据云备份功能暂未开通...");

        cloudBackUpForm.find(".btn1").remove();
        cloudBackUpForm.find(".btn4").remove();
        return this;
      },
      resize: function(ch) {
        resize(ch);
      },
      showModel: function() {
        $(p.mainContainer).show();
      },
      hideModel: function() {
        p.mainContainer.remove();
      }
    };
  }
  return {
    getInstance: function() {
      /*if (!uniqueInstance) {
        uniqueInstance = constructor();
      }
      return uniqueInstance;*/
      return constructor();
    }
  };
})();