/**
 * [ 公司管理页面]                                                                                        resize(ch [description]
 * @return {[type]}         [description]
 */
CTFO.Model.CompanyManage = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var cHeight = 0,
      minH = 520, // 本模块最低高度
      gridHeight = 300, // grid高度
      pageSize = 50,
      pageSizeOption = [10, 20, 30, 40, 50, 100],

      treeContainer = null,
      companyGrid = null,
      vehicleTeamTerm = null,
      gridContainer = null,
      companyGridContent = null, //查询条件以及表格容器
      companyFormContent = null,
      addFlag = true,

      leftTree = null,
      grid = null,
      currentEntName = '',

      companyDetailTmpl = null,
      vehicleTeamModifyTmpl = null,
      companyManageDetailTmpl = null,
      gridContainerWrap = null,
      companyForm = null,

      updateRowAuth = 'FG_MEMU_SYSTEM_COMPANYMANAGE_U', // 修改记录权限
      detailRowAuth = 'FG_MEMU_SYSTEM_COMPANYMANAGE_I', // 查看记录详情权限
      deleteRowAuth = 'FG_MEMU_SYSTEM_COMPANYMANAGE_D', // 删除记录权限
      addRowAuth = 'FG_MEMU_SYSTEM_COMPANYMANAGE_C', // 新增记录权限
      exportRowAuth = 'FG_MEMU_SYSTEM_COMPANYMANAGE_E', // 导出记录权限
      opLogRowAuth = 'FG_MEMU_SYSTEM_COMPANYMANAGE_O', // 操作记录
      startRowAuth = 'FG_MEMU_SYSTEM_COMPANYMANAGE_S', // 启用/吊销
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
      display: '企业名称',
      name: 'comName',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '所在地',
      name: 'orgShortname',
      width: 150,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.codeManager.getCountyName(row.province, row.city, row.county);
      }
    }, {
      display: '联系人',
      name: 'comContact',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '联系电话',
      name: 'comTel',
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
      name: 'status',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return +row.status === 1 ? "启用" : "吊销";
      }
    }, {
      display: '备注',
      name: 'remark',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '操作',
      name: 'entType',
      width: 150,
      sortable: true,
      align: 'center',
      render: function(row) {
        var buttons = [];
        /*if ($.inArray(detailRowAuth, CTFO.cache.auth) > -1) {
          var detailHtml = "<span class='cBlue'><font title='查看' class='hand' actionType='rowDetail'>查看</font></span>&nbsp;";
          detailHtml = ($.inArray('FG_MEMU_OPERATIONS_DATA_TEAM_INFO', CTFO.cache.auth) > 0) ?  detailHtml : '--';
          buttons.push(detailHtml);
        }*/
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
        if ($.inArray(detailRowAuth, CTFO.cache.auth) > -1) {
          if (row.status == 0) {
            var detailHtml = "<span class='cBlue'><font title='启用' class='hand' actionType='revokeEditOpen'>启用</font></span>&nbsp;";
            detailHtml = ($.inArray(startRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
            buttons.push(detailHtml);
          }
          if (row.status == 1) {
            var detailHtml = "<span class='cBlue'><font title='停用' class='hand' actionType='revokeEdit'>吊销</font></span>&nbsp;";
            detailHtml = ($.inArray(startRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
            buttons.push(detailHtml);
          }
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
      url: CTFO.config.sources.companySearchGrid, // 数据请求地址
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
    var onModifyRow = function(comId) {
      addFlag = false;
      $.ajax({
        url: CTFO.config.sources.companyDetail,
        type: 'POST',
        dataType: 'json',
        data: {
          'comId': comId
        },
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          compileFormData(data);
          companyFormContent.removeClass('none');
          companyGridContent.addClass('none');
          companyFormContent.find("li:gt(13)").removeClass('none');
          //锁定公司姓名不可更改
          $(companyForm).find('input[name="comName"]').attr('disabled', 'true');
          $(companyForm).find('input[name="createBy"]').attr('disabled', 'true');
          $(companyForm).find('input[name="createTime"]').attr('disabled', 'true');
          $(companyForm).find('input[name="updateBy"]').attr('disabled', 'true');
          $(companyForm).find('input[name="updateTime"]').attr('disabled', 'true');
          $(companyForm).find('select[name="status"]').attr('disabled', 'true');
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };
    /**
     * [onDeleteRow 绑定grid行事件]
     */
    var onDeleteRow = function(comId) {
      var comDelete = 0;
      $.ligerDialog.confirm('真的要执行删除', '信息提示', function(yes) {
        if (yes) {
          $.ajax({
            dataType: 'json',
            url: CTFO.config.sources.deleteSysCom + "?comId=" + comId + '&comDelete=' + comDelete,
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
              //called when there is an error
            }
          });
        }
      });
    };
    /**
     * [revokeEditStop 绑定grid行事件]
     */
    var revokeEditStop = function(comId) {
        var status = 0;
        $.ligerDialog.confirm('真的要执行吊销操作', '信息提示', function(yes) {
          if (yes) {
            $.ajax({
              url: CTFO.config.sources.revokeEditSysCom + "?comId=" + comId + '&status=' + status,
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
    var revokeEditStart = function(comId) {
        var status = 1;
        $.ligerDialog.confirm('真的要执行启用操作', '信息提示', function(yes) {
          if (yes) {
            $.ajax({
              url: CTFO.config.sources.revokeEditSysCom + "?comId=" + comId + '&status=' + status,
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
          onModifyRow(rowData.comId);
          break;
        case 'deleteRow':
          onDeleteRow(rowData.comId);
          break;
        case 'rowDetail':
          showCompanyDetail(rowData.comId);
          break;
        case 'revokeEditOpen':
          revokeEditStart(rowData.comId);
          break;
        case 'revokeEdit':
          revokeEditStop(rowData.comId);
          break;
      }
      return !actionType;
    };
    /**
     * 初始化赋值操作
     */
    var compileFormData = function(d) {
      var d = d || {};
      var doTtmpl = doT.template(companyManageDetailTmpl);
      $(companyForm).find(".companyFormBox").html(doTtmpl(d));

      var provinceOption = $(companyForm).find('select[name=province]'),
        cityOption = $(companyForm).find('select[name=city]'),
        countyOption = $(companyForm).find('select[name=county]');
      CTFO.utilFuns.codeManager.initProvAndCityAndCounty(provinceOption, cityOption, countyOption, d.province, d.city, d.county);

      //initCompanySelectList( $(companyForm).find('select[name=parentComid]') , d.parentComid );

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
    var showCompanyDetail = function(comId) {
      getCompanyDetail(comId, companyDetailTmpl, function(content) {
        var param = {
          title: "公司详情",
          ico: 'ico226',
          width: 650,
          height: 285,
          content: content
        };
        CTFO.utilFuns.tipWindow(param);
      });
    };
    var getCompanyDetail = function(comId, tmpl, callback) {
      $.get(CTFO.config.sources.companyDetail, {
        comId: comId
      }, function(data, textStatus, xhr) {
        var content = '';
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
      d.county = CTFO.utilFuns.codeManager.getCountyName(d.province, d.city, d.county);
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
            companyGrid.find('.queryButton').trigger('click');
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
      companyGrid.find('.addVehicleTeam').click(function(event) {
        companyFormContent.removeClass('none');
        companyGridContent.addClass('none');
        compileFormData();
        companyFormContent.find("li:gt(13)").addClass('none');
        //initCompanySelectList( $(companyForm).find('select[name=parentComid]') );
        $.ajax({
          url: CTFO.config.sources.queryAutoCode,
          dataType: 'json',
          complete: function(xhr, textStatus) {
            //called when complete
          },
          success: function(data, textStatus, xhr) {
            var code = $(companyForm).find('input[name=comCode]').attr("value", data)
          },
          error: function(xhr, textStatus, errorThrown) {
            //called when there is an error
          }
        });
        var provinceOption = $(companyForm).find('select[name=province]'),
          cityOption = $(companyForm).find('select[name=city]'),
          countyOption = $(companyForm).find('select[name=county]');

        CTFO.utilFuns.codeManager.initProvAndCityAndCounty(provinceOption, cityOption, countyOption);

      });
      companyForm.find('span[name="cancelSave"]').click(function(event) {
        companyGridContent.removeClass('none');
        companyFormContent.addClass('none');
        addFlag = true;
        companyGrid.find('.queryButton').trigger('click');
      });
      companyForm.find('span[name="saveForm"]').click(function(event) {
        //表单前端验证
        var validate = $(companyForm).validate({
          debug: false,
          errorClass: 'myselfError',
          messages: {},
          success: function() {}
        });
        if (!validate.form()) {
          return false;
        }
        if (addFlag && !userExist(companyForm))
          return false;
        var parms = [];
        var d = $(companyForm).serializeArray();
        $(d).each(function() {
          parms.push({
            name: this.name,
            value: $.trim(this.value)
          });
        });
        if (companyForm.find('span[name="saveForm"]').attr('disabled')) {
          return false;
        }
        var newParms = {};
        newParms["comCode"] = $("input[name=comCode]").attr('value');
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
            url: addFlag ? CTFO.config.sources.addSysComInfo : CTFO.config.sources.modifySysComInfo,
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
                    companyGridContent.removeClass('none');
                    companyFormContent.addClass('none');
                    grid.loadData(true);
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
     * @description 处理按钮
     * @param boolean
     */
    var disabledButton = function(boolean) {
      if (boolean) {
        companyForm.find('span[name="saveForm"]').attr('disabled', true);
      } else {
        companyForm.find('span[name="saveForm"]').attr('disabled', false);
      }
    };
    /**
     * 用户是否存在验证
     */
    var userExist = function(container) {
      var Loginname = $(container).find('input[name="comName"]').val().toLowerCase();
      var flag = false;
      $.ajax({
        url: CTFO.config.sources.isExistComName,
        type: 'POST',
        async: false,
        dataType: 'json',
        data: {
          "comName": Loginname
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
    /**
     * [initForm 初始化查询条件form]
     * @return {[type]} [description]
     */
    var initForm = function() {
      var provinceOption = companyGrid.find('select[name=corpProvince]'),
        cityOption = companyGrid.find('select[name=corpCity]'),
        countyOption = companyGrid.find('select[name=corpCounty]');
      CTFO.utilFuns.codeManager.initProvAndCityAndCounty(provinceOption, cityOption, countyOption);
      companyGrid.find('.queryButton').click(function(event) {
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
        .find('.operaterLogButton').click(function(event) {
          CTFO.cache.frame.changeModel('operaterLogManage', '', null, 0);
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
        .find('.exportGrid').click(function(event) {
          CTFO.utilFuns.commonFuns.exportGrid({
            grid: grid,
            url: CTFO.config.sources.exportCompanyListExcelData
          });
        }).end()
        .find('.resetButton').click(function(event) {
          resetThis();
        });
    };
    /**
     * @description 清空表单
     */
    var resetThis = function() {
      $(companyGrid).find('input[type="text"]').each(function() {
        $(this).val("");
      }).end().find('select').each(function() {
        $(this).val("");
      }).end().find('textarea').each(function() {
        $(this).val("");
      }).end();

      var provinceOption = $(companyGrid).find('select[name=corpProvince]'),
        cityOption = $(companyGrid).find('select[name=corpCity]'),
        countyOption = $(companyGrid).find('select[name=corpCounty]');

      CTFO.utilFuns.codeManager.initProvAndCityAndCounty(provinceOption, cityOption, countyOption);

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
        return item.comId
      });
      if (arr.length > 0) {
        revokeEditStop(arr.join(","));
      }
    };
    /**
     * [onStopData 查看事件]
     * @return {[type]} [description]
     */
    var onStartData = function() {
      var record = grid.getCheckedRows();
      if (record.length <= 0) {
        $.ligerDialog.error('请勾选操作记录!');
        return;
      }
      var arr = _.map(record, function(item) {
        return item.comId
      });
      if (arr.length > 0) {
        revokeEditStart(arr.join(","));
      }
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
          onDeleteRow(arr.join(","));
        }
      }
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
        onModifyRow(row.comId);
      }
      /**
       * [initGrid 初始化grid对象]
       * @return {[type]} [description]
       */
    var initGrid = function() {
      grid = gridContainer.ligerGrid(gridOptions);
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
          getSelectList(data, container, defaultVal);
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };

    /**
     * [getSelectList 初始化发布公司 select]
     *
     * @param list
     *            发布公司数组
     *
     * @param container
     *            发布公司容器
     *
     * @param defaultVal
     *            默认参数
     * @return {[type]} [description]
     */

    var getSelectList = function(list, container, defaultValue) {
      var tip = tip || "请选择...";
      var options = ['<option value="" title="' + tip + '">' + tip + '</option>'];
      $(list).each(function() {
        var selected = (defaultValue && defaultValue == this.comId) ? 'selected' : '';
        options.push('<option value="' + this.comId + '" title="' + this.comName + '" ' + selected + '>' + this.comName + '</option>');

      });
      $(container).html('').append(options.join(''));
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
        data = companyGrid.serializeArray();
      $(data).each(function(event) {
        var name = '';
        if (this.name === 'comName' || this.name === 'comContact')
          name = 'requestParam.like.' + this.name;
        else
          name = 'requestParam.equal.' + this.name;
        if (this.value){
          if( name.indexOf("TimeStart") > 0 ){
            this.value += " 00:00:00"
          }
          if( name.indexOf("TimeEnd") > 0 ){
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
        companyGrid = p.mainContainer.find('form[name=companyGrid]');
        companyForm = p.mainContainer.find('form[name=companyForm]');
        gridContainer = p.mainContainer.find('.gridContainer');
        gridContainerWrap = p.mainContainer.find('.gridContainerWrap');

        companyDetailTmpl = $('#company_detail_tmpl').html();
        vehicleTeamModifyTmpl = $('#vehicle_team_update_tmpl').html();
        companyManageDetailTmpl = $('#company_manage_detail_tmpl').html();

        companyGridContent = p.mainContainer.find('.companyContent:eq(0)'); //查询条件以及表格容器
        companyFormContent = p.mainContainer.find('.companyContent:eq(1)');

        resize(p.cHeight);

        bindEvent();
        initTreeContainer();
        initForm();
        initGrid();
        initAuth(companyGrid);


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