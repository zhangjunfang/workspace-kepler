/**
 * [ 车队管理模块]                                                                                        resize(ch [description]
 * @return {[type]}         [description]
 */
CTFO.Model.VehicleTeamManage = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var cHeight = 0,
      minH = 520, // 本模块最低高度
      gridHeight = 300, // grid高度
      pageSize = 50,
      pageSizeOption = [10, 20, 30, 40, 50, 100],

      treeContainer = null,
      vehicleTeamForm = null,
      vehicleTeamGrid = null,
      vehicleTeamTerm = null,
      gridContainer = null,

      leftTree = null,
      grid = null,
      addFlag = true,
      currentEntName = '',

      vehicleTeamDetailTmpl = null,
      vehicleTeamModifyTmpl = null,

      updateRowAuth = 'FG_MEMU_SYSTEM_VEHICLETEAMMANAGE_U', // 修改记录权限
      startRowAuth = 'FG_MEMU_SYSTEM_VEHICLETEAMMANAGE_I', // 查看记录详情权限
      detailRowAuth = 'FG_MEMU_SYSTEM_VEHICLETEAMMANAGE_I', // 查看记录详情权限
      deleteRowAuth = 'FG_MEMU_SYSTEM_VEHICLETEAMMANAGE_D', // 删除记录权限
      addRowAuth = 'FG_MEMU_SYSTEM_VEHICLETEAMMANAGE_C', // 新增记录权限
      exportRowAuth = 'FG_MEMU_SYSTEM_VEHICLETEAMMANAGE_E', // 导出记录权限
      opLogRowAuth = 'FG_MEMU_SYSTEM_VEHICLETEAMMANAGE_O', // 操作记录
      isPassByValueMode = CTFO.config.globalObject.isPassByValueMode, //传值方式
      test = '';
    // grid展现列
    var columns = [{
      display: '组织编码',
      name: 'corpCode',
      width: 100,
      sortable: true,
      align: 'center',
      toggle: false
    }, {
      display: '组织名称',
      name: 'entName',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '所属公司',
      name: 'comName',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '联系人',
      name: 'orgCname',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '联系电话',
      name: 'orgCphone',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '创建人',
      name: 'createBy',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '创建时间',
      name: 'createTime',
      width: 160,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.dateFuns.utc2date(row.createTime);
      }
    }, {
      display: '状态',
      name: 'entState',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return +row.entState === 1 ? "启用" : "吊销";
      }
    }, {
      display: '备注',
      name: 'memo',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '操作',
      name: 'entType',
      width: 120,
      sortable: true,
      align: 'center',
      render: function(row) {
        var buttons = [];
        if ($.inArray(detailRowAuth, CTFO.cache.auth) > -1) {
            var delHtml = "<span class='cBlue'><font title='查看' class='hand' actionType='rowDetail'>查看</font></span>&nbsp;";
            delHtml = ($.inArray(updateRowAuth, CTFO.cache.auth) > 0) ? delHtml : '--';
            buttons.push(delHtml);
          }        
        if ($.inArray(detailRowAuth, CTFO.cache.auth) > -1) {
          var delHtml = "<span class='cBlue'><font title='编辑' class='hand' actionType='modifyRow'>编辑</font></span>&nbsp;";
          delHtml = ($.inArray(updateRowAuth, CTFO.cache.auth) > 0) ? delHtml : '--';
          buttons.push(delHtml);
        }
        if ($.inArray(detailRowAuth, CTFO.cache.auth) > -1) {
          var delHtml = "<span class='cBlue'><font title='删除' class='hand' actionType='deleteRow'>删除</font></span>&nbsp;";
          delHtml = ($.inArray(deleteRowAuth, CTFO.cache.auth) > 0) ? delHtml : '--';
          buttons.push(delHtml);
        }
        if (row.entState == 0) {
          var detailHtml = "<span class='cBlue'><font title='启用' class='hand' actionType='revokeEditOpen'>启用</font></span>&nbsp;";
          detailHtml = ($.inArray(detailRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
          buttons.push(detailHtml);
        }
        if (row.entState == 1) {
          var detailHtml = "<span class='cBlue'><font title='吊销' class='hand' actionType='revokeEdit'>吊销</font></span>&nbsp;";
          detailHtml = ($.inArray(detailRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
          buttons.push(detailHtml);
        }
        return buttons.join('');
      }
    }];
    // grid初始化参数
    var gridOptions = {
      columns: columns,
      sortName: 'createTime',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      url: CTFO.config.sources.vehicleTeamGrid, // 数据请求地址
      pageSize: pageSize,
      pageSizeOption: pageSizeOption,
      pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
      pagesizeParmName: 'requestParam.rows',
      width: '100%',
      height: gridHeight,
      delayLoad: false,
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
     * [onModifyRow 绑定grid行事件]
     */
    var onModifyRow = function(entId) {
      $(vehicleTeamForm).find('input[name="entId"]').val(entId);
      addFlag = false;
      $.ajax({
        url: CTFO.config.sources.orgDetail,
        type: 'POST',
        dataType: 'json',
        data: {
          'entId': entId
        },
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          resetThis();
          compileFormData(data);
          vehicleGridContent.addClass('none');
          vehicleFormContent.removeClass('none').find("li:gt(5)").removeClass('none').find('input').attr('disabled', 'true').end().find('select').attr('disabled', 'true');
          initCompanySelectList($(vehicleTeamForm).find('select[name=comId]'), data.comId);
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };
    /**
     * [onDeleteRow 绑定grid行事件]
     */
    var onDeleteRow = function(entId) {
      deleteVehicleTeam(entId);
    };
    /**
     * [revokeEditStop 绑定grid行事件]
     */
    var revokeEditStop = function(entId) {
        var entState = 0;
        $.ligerDialog.confirm('真的要执行吊销操作', '信息提示', function(yes) {
          if (yes) {
            $.ajax({
              url: CTFO.config.sources.revokeEditSysEnt + "?entId=" + entId + '&entState=' + entState,
              complete: function(xhr, textStatus) {
                //called when complete
              },
              success: function(data, textStatus, xhr) {
                $.ligerDialog.success("吊销成功!", "信息提示");
                grid.loadData(true);
              },
              error: function(xhr, textStatus, errorThrown) {
                //called when there is an error
              }
            });
          }
        });
      }
      /**
       * [revokeEditStart 绑定grid行事件]
       */
    var revokeEditStart = function(entId) {
        var entState = 1;
        $.ligerDialog.confirm('真的要执行启用操作', '信息提示', function(yes) {
          if (yes) {
            $.ajax({
              url: CTFO.config.sources.revokeEditSysEnt + "?entId=" + entId + '&entState=' + entState,
              complete: function(xhr, textStatus) {
                //called when complete
              },
              success: function(data, textStatus, xhr) {
                $.ligerDialog.success("启用成功!", "信息提示");
                grid.loadData(true);
              },
              error: function(xhr, textStatus, errorThrown) {
                //called when there is an error
              }
            });
          }
        });
      }
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
          onModifyRow(rowData.entId);
          break;
        case 'deleteRow':
          deleteVehicleTeam(rowData.entId);
          break;
        case 'rowDetail':
          showVehicleTeamDetail(rowData.entId);
          break;
        case 'revokeEditOpen':
          revokeEditStart(rowData.entId);
          break;
        case 'revokeEdit':
          revokeEditStop(rowData.entId);
          break;
      }
      return !actionType;
    };
    /**
     * 初始化赋值操作
     */
    var compileFormData = function(r) {
      var d = {};
      for (var n in r) {
        var key = n;
        if (key == 'createTime' || key == 'updateTime') {
          d[key] = (r[key] > 0) ? CTFO.utilFuns.dateFuns.utc2date(r[key]) : '';
        } else {
          d[key] = r[n];
        }
      }
      $(vehicleTeamForm).find('input[type=text]').each(function() {
        var key = $(this).attr('name');
        if (key && d[key])
          $(this).val(d[key]);
      }).end().find('select').each(function() {
        var key = $(this).attr('name');
        $(this).val(d[key]);
      }).end().find('input[type=hidden]').each(function() {
        var key = $(this).attr('name');
        if (key && d[key])
          $(this).val(d[key]);
      });
    };
    /**
     * [inisertVehicleTeam 新增车队]
     * @return {[type]} [description]
     */
    var inisertVehicleTeam = function() {
      var defaultData = {
          parentName: vehicleTeamForm.find('.parentCorpDesc').text(),
          corpCode: CTFO.cache.user.corpCode,
          parentId: vehicleTeamForm.find('input[name=parentId]').val()
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
          var grandpaId = vehicleTeamForm.find('input[name=grandpaId]').val();
          if (grandpaId !== "-1") {
            $(w).find("input[name='corpCode']").attr("disabled", true);
          }
          initVehicleDetailUpdate('c', w, d, g); // 填充新增弹窗内容
        }
      };
      CTFO.utilFuns.tipWindow(param);
    };
    /**
     * [deleteVehicleTeam 删除车队记录]
     * @param  {[String]}  orgId  [组织id]
     * @return {[type]} [description]
     */
    var deleteVehicleTeam = function(orgId) {
      $.ligerDialog.confirm('是否删除组织?', function(yes) {
        if (!yes) return false;
        $.ajax({
          url: CTFO.config.sources.deleteVehicleTeam,
          type: 'POST',
          dataType: 'json',
          data: {
            orgId: orgId
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
     * [showVehicleTeamDetail 显示组织详情]
     * @param  {[String]} orgId [组织id]
     * @return {[type]}       [description]
     */
    var showVehicleTeamDetail = function(orgId) {
      getVehicleTeamDetail(orgId, vehicleTeamDetailTmpl, function(content) {
        var param = {
          title: "组织详情",
          ico: 'ico226',
          width: 650,
          height: 150,
          content: content
        };
        CTFO.utilFuns.tipWindow(param);
      });
    };
    /**
     * [getVehicleTeamDetail 获取车队详情]
     * @param  {[String]}   orgId    [组织id]
     * @param  {[Object]}   tmpl     [详情模板对象]
     * @param  {Function} callback [回调函数]
     * @return {[type]}            [description]
     */
    var getVehicleTeamDetail = function(orgId, tmpl, callback) {
      $.get(CTFO.config.sources.orgDetail, {
        orgId: orgId
      }, function(data, textStatus, xhr) {
        var content = '';
        debugger;
        if (typeof(data) === 'string')
          data = JSON.parse(data);
        if (data && !data.error)
          content = compileVehicleTeamDetail(data, tmpl);
        if (callback)
          callback(content, data);
      });
    };
    /**
     * [compileVehicleTeamDetail 渲染车队详情弹窗]
     * @param  {[Object]} d    [数据对象]
     * @param  {[Object]} tmpl [详情模板对象]
     * @return {[type]}      [description]
     */
    var compileVehicleTeamDetail = function(d, tmpl) {
      var doTtmpl = doT.template(tmpl);
      d.entTypeDesc = (+d.entType === 1 ? '企业' : (+d.entType === 2 ? '车队' : ''));
      d.provinceDesc = CTFO.utilFuns.codeManager.getNameByCode('SYS_AREA_INFO', d.corpProvince);
      d.cityDesc = CTFO.utilFuns.codeManager.getCityProvcodeNameByCode('SYS_AREA_INFO', d.corpProvince, d.corpCity);
      d.corpQualeDesc = CTFO.utilFuns.codeManager.getNameByCode('SYS_CORP_BUSINESS_SCOPE', d.corpQuale);
      d.corpLevelDesc = CTFO.utilFuns.codeManager.getNameByCode('SYS_CORP_LEVEL', d.corpLevel);
      return doTtmpl(d);
    };
    /**
     * [modifyVehicleTeam 修改车队信息]
     * @param  {[String]} orgId [组织id]
     * @return {[type]}       [description]
     */
    var modifyVehicleTeam = function(orgId) {
      getVehicleTeamDetail(orgId, vehicleTeamModifyTmpl, function(content, data) {
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
            vehicleTeamGrid.find('.queryButton').trigger('click');
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
      debugger;
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
      vehicleTeamGrid.find('.addVehicleTeam').click(function(event) {
          if (vehicleFormContent.hasClass('none')) {
            vehicleFormContent.removeClass('none');
            vehicleGridContent.addClass('none');
            vehicleFormContent.find("li:gt(5)").addClass('none');
            resetThis();
            initCompanySelectList($(vehicleTeamForm).find('select[name=comId]'));
            $.ajax({
              url: CTFO.config.sources.queryAutoCodeOfEnt,
              dataType: 'json',
              complete: function(xhr, textStatus) {
                //called when complete
              },
              success: function(data, textStatus, xhr) {
                var code = $(vehicleTeamForm).find('input[name=corpCode]').attr("value", data)
              },
              error: function(xhr, textStatus, errorThrown) {
                //called when there is an error
              }
            });
          };
          //      inisertVehicleTeam();
        }).end()
        .find('.exportGrid').click(function(event) {
          CTFO.utilFuns.commonFuns.exportGrid({
            grid: grid,
            url: CTFO.config.sources.exportVehicleTeamExcelData
          });
        });
      vehicleTeamForm.find('span[name="cancelSave"]').click(function(event) {
        vehicleGridContent.removeClass('none');
        vehicleFormContent.addClass('none');
        vehicleTeamGrid.find('.queryButton').trigger('click');
        addFlag = true;
      });
      vehicleTeamForm.find('span[name="saveForm"]').click(function(event) {
        //表单前端验证
        var validate = $(vehicleTeamForm).validate({
          debug: false,
          errorClass: 'myselfError',
          messages: {},
          success: function() {}
        });
        if (!validate.form()) {
          return false;
        }
        if (addFlag && !userExist(vehicleTeamForm))
          return false;
        var parms = [];
        var d = $(vehicleTeamForm).serializeArray();
        $(d).each(function() {
          parms.push({
            name: this.name,
            value: $.trim(this.value)
          });
        });
        if (vehicleTeamForm.find('span[name="saveForm"]').attr('disabled')) {
          return false;
        }
        var newParms = {};
        newParms["corpCode"] = $("input[name=corpCode]").attr('value');
        $.each(parms, function(i, n) {
          var name = n.name;
          if (name.indexOf(".") > -1) {
            var name = name.split(".")[1];
          }
          newParms[name] = n.value;
        });
        disabledButton(true); // 控制按钮
        var param =
          $.ajax({
            url: addFlag ? CTFO.config.sources.insertVehicleTeam : CTFO.config.sources.updateVehicleTeam,
            type: 'post',
            dataType: 'json',
            data: isPassByValueMode ? JSON.stringify(newParms) : parms,
            contentType: isPassByValueMode ? 'application/json; charset=utf-8' : 'application/x-www-form-urlencoded;charset=UTF-8',
            error: function() {
              disabledButton(false); // 控制按钮
            },
            success: function(r) {
              disabledButton(false); // 控制按钮
              var text = addFlag ? "新增操作" : "修改操作";
              if (r && r.displayMessage == "success") {
                $.ligerDialog.success(text + '成功', '提示', function(y) {
                  if (y) {
                    vehicleGridContent.removeClass('none');
                    vehicleFormContent.addClass('none');
                    grid.loadData(true);
                    resetThis();
                  };
                  addFlag = true;
                });
              } else {
                $.ligerDialog.error(text + "失败");
              }
            }
          });
      });
    };
    /**
     * 用户是否存在验证
     */
    var userExist = function(container) {
      var Loginname = $(container).find('input[name="entName"]').val().toLowerCase();
      var flag = false;
      $.ajax({
        url: CTFO.config.sources.isExistEntName,
        type: 'POST',
        async: false,
        dataType: 'json',
        data: {
          "entName": Loginname
        },
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          if (data.displayMessage == "success") {
            $.ligerDialog.success("用户已经存在请重新输入");
            flag = false;
          } else {
            flag = true;
          }
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
      return flag;
    };
    /**
     * @description 处理按钮
     * @param boolean
     */
    var disabledButton = function(boolean) {
      if (boolean) {
        vehicleTeamForm.find('span[name="saveForm"]').attr('disabled', true);
      } else {
        vehicleTeamForm.find('span[name="saveForm"]').attr('disabled', false);
      }
    };
    /**
     * @description 清空表单
     */
    var resetThis = function() {
      $(vehicleFormContent).find('input[type="text"]').each(function() {
        $(this).val("");
      }).end().find('select').each(function() {
        $(this).val("");
      }).end().find('textarea').each(function() {
        $(this).val("");
      }).end().find('input[type="password"]').each(function() {
        $(this).val("");
      }).end();
      //错误标签
      $(vehicleFormContent).find('label[class="error"]').each(function() {
        $(this).remove();
      });
      $(vehicleFormContent).find('.error').removeClass('error');
    };
    /**
     * [initCompanySelectList 初始化发布公司 select]
     *
     * @param container
     *            发布部门容器
     *
     * @param defaultVal
     *            默认参数
     * @return {[type]} [description]
     */
    var initCompanySelectList = function(container, defaultVal) {
      $.ajax({
        url: CTFO.config.sources.querySysCompanyList,
        dataType: 'json',
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          CTFO.utilFuns.commonFuns.getSelectList(_.map(data, function(item) {
            return {
              "code": item.comId,
              "name": item.comName
            };
          }), container, defaultVal);
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
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
      vehicleTeamForm.find('.parentCorpDesc').text(d[columns.text]).end()
        .find('input[name=parentId]').val(d[columns.id]).end()
        .find('input[name=grandpaId]').val(d["parentId"]);
    };
    /**
     * [initForm 初始化查询条件form]
     * @return {[type]} [description]
     */
    var initForm = function() {
      $(vehicleTeamGrid).find('.queryButton').click(function(event) {
          searchGrid();
        }).end()
        .find('input[name=createTimeStart]').click(function(event) {
          WdatePicker({
            dateFmt: 'yyyy-MM-dd',
            isShowClear: false
          });
        }).end()
        .find('input[name=createTimeEnd]').click(function(event) {
          WdatePicker({
            dateFmt: 'yyyy-MM-dd',
            isShowClear: false
          });
        }).end()
        .find('.editButton').click(function(event) {
          onEdit();
        }).end()
        .find('.removeButton').click(function(event) {
          onRemove();
        }).end()
        .find('.startButton').click(function(event) {
          onStartData();
        }).end()
        .find('.stopButton').click(function(event) {
          onStopData();
        }).end()
        .find('.operaterLogButton').click(function(event) {
          CTFO.cache.frame.changeModel('operaterLogManage', '', null, 0);
        }).end()
        .find('.resetButton').click(function(event) {
          resetThisForm();
        });
    };
    /**
     * @description 清空表单
     */
    var resetThisForm = function() {
      $(vehicleTeamGrid).find('input[type="text"]').each(function() {
        $(this).val("");
      }).end().find('select').each(function() {
        $(this).val("");
      }).end().find('textarea').each(function() {
        $(this).val("");
      }).end();

    };
    /**
     * [onStopData 查看事件]
     * @return {[type]} [description]
     */
    var onStopData = function() {
      var record = grid.getCheckedRows();
      if (record.length <= 0) {
        $.ligerDialog.error('请勾选操作记录!');
        return;
      }
      var arr = _.map(record, function(item) {
        return item.entId
      });
      if (arr.length > 0) {
        revokeEditStop(arr.join(","));
      }
    };
    /**
     * [onStartData 查看事件]
     * @return {[type]} [description]
     */
    var onStartData = function() {
      var record = grid.getCheckedRows();
      if (record.length <= 0) {
        $.ligerDialog.error('请勾选操作记录!');
        return;
      }
      var arr = _.map(record, function(item) {
        return item.entId
      });
      if (arr.length > 0) {
        revokeEditStart(arr.join(","));
      }
    };
    /**
     * [initGrid 初始化grid对象]
     * @return {[type]} [description]
     */
    var onEdit = function() {
      var record = grid.getCheckedRows();
      if (record.length <= 0) {
        $.ligerDialog.error('请勾选单条操作记录!');
        return;
      } else if (record.length > 1) {
        $.ligerDialog.error('请勾选单条操作记录!');
        return;
      }
      var row = record[0];
      onModifyRow(row.entId);
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
        return item.entId
      });
      if (arr.length > 0) {
        onDeleteRow(arr.join(","));
      }
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
        data = vehicleTeamGrid.serializeArray();
      $(data).each(function(event) {
        var name = '';
        if (this.name === 'entName' || this.name === 'comName')
          name = 'requestParam.like.' + this.name;
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

      // 启用
      if ($.inArray(startRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.startButton').remove();
      }

      // 吊销
      if ($.inArray(startRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.stopButton').remove();
      }

      // 编辑
      if ($.inArray(updateRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.editButton').remove();
      }

      // 删除
      if ($.inArray(deleteRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.removeButton').remove();
      }

      // 增加
      if ($.inArray(addRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.addVehicleTeam').remove();
      }
      // 导出
      if ($.inArray(exportRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.exportGrid').remove();
      }
    };

    return {
      init: function(options) {
        p = $.extend({}, p || {}, options || {});
        pageLocation = p.mainContainer.find('.pageLocation');
        treeContainer = p.mainContainer.find('.leftTreeContainer');
        vehicleTeamTerm = p.mainContainer.find('.vehicleTeamTerm');
        vehicleTeamForm = p.mainContainer.find('form[name=vehicleTeamForm]');
        vehicleTeamGrid = p.mainContainer.find('form[name=vehicleTeamGrid]');
        gridContainer = p.mainContainer.find('.gridContainer');
        gridContainerWrap = p.mainContainer.find('.gridContainerWrap');

        vehicleTeamDetailTmpl = $('#vehicle_team_detail_tmpl').html();
        vehicleTeamModifyTmpl = $('#vehicle_team_update_tmpl').html();

        vehicleGridContent = p.mainContainer.find('.companyContent:eq(0)'); //查询条件以及表格容器
        vehicleFormContent = p.mainContainer.find('.companyContent:eq(1)');

        resize(p.cHeight);

        bindEvent();
        initTreeContainer();
        initForm();
        initGrid();
        initAuth(vehicleTeamGrid);


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