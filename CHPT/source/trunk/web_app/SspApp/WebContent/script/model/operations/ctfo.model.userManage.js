//系统用户管理
CTFO.Model.userManage = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var cHeight = 0,
      pageSize = 50,
      pageSizeOption = [10, 20, 30, 40, 50, 100],
      gridHeight = 300, // 表格展示区高度
      TreeContainer = null, //
      leftTree = null,
      addFlag = true,
      corpId = "",
      fuelManageSearchform = null, //缓存添加表格form
      isPassByValueMode = CTFO.config.globalObject.isPassByValueMode, //传值方式
      userManageDetailTmpl = null,

      minH = 520, // 本模块最低高度
      gridListBox = null,
      userManageform = null, //查询form
      userManageBox = null, //grid表格展现盒子
      userManageGridContent = null, //查询条件以及表格容器
      userManageFormContent = null,
      grid = null,

      updateRowAuth = 'FG_MEMU_SYSTEM_USERMANAGE_U', // 修改记录权限
      detailRowAuth = 'FG_MEMU_SYSTEM_USERMANAGE_I', // 查看记录详情权限
      deleteRowAuth = 'FG_MEMU_SYSTEM_USERMANAGE_D', // 删除记录权限
      addRowAuth = 'FG_MEMU_SYSTEM_USERMANAGE_C', // 新增记录权限
      exportRowAuth = 'FG_MEMU_SYSTEM_USERMANAGE_E', // 导出记录权限
      opLogRowAuth = 'FG_MEMU_SYSTEM_USERMANAGE_O', // 操作记录
      startRowAuth = 'FG_MEMU_SYSTEM_USERMANAGE_S', // 启用/吊销
      pushUserManage = null; //添加表单

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

      // 添 加
      if ($.inArray(addRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.userManageAddBtn').remove();
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


    var gridcolumns = [{
      display: '人员编码',
      name: 'opCode',
      width: 100,
      sortable: true,
      align: 'center',
      toggle: false
    }, {
      display: '账号',
      name: 'opLoginname',
      width: 100,
      sortable: true,
      align: 'center',
      toggle: false
    }, {
      display: '姓名',
      name: 'opName',
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
      display: '所属组织',
      name: 'entName',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '所属角色',
      name: 'roleName',
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
      display: '创建人',
      name: 'createBy',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '状态',
      name: 'opStatus',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return row.opStatus == 1 ? "启用" : "吊销";
      }
    }, {
      display: '备注',
      name: 'opMem',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '操作',
      name: 'entState',
      width: 200,
      sortable: true,
      align: 'center',
      render: function(row) {
        var revoke = "";
        var edit = "";
        var remove = "";
        var passeword = "";
        if ($.inArray(updateRowAuth, CTFO.cache.auth) && row.opId != CTFO.cache.user.opId) {
            select = '<span class=" mr10 cBlue hand" title="查看" name="rowDetail" opId="' + row.opId + '">查看</span>';
          }
        if ($.inArray(updateRowAuth, CTFO.cache.auth) && row.opId != CTFO.cache.user.opId) {
          edit = '<span class=" mr10 cBlue hand" title="修改" name="updateSpOperator" opId="' + row.opId + '">修改</span>';
        }
        if ($.inArray(startRowAuth, CTFO.cache.auth) && row.opId != CTFO.cache.user.opId) {
          if (row.opStatus == 1) {
            revoke = '<span class=" mr10 cBlue hand" title="吊销" name="revokeEdit" opId="' + row.opId + '">吊销</span>';
          } else {
            revoke = '<span class=" mr10 cBlue hand" title="启用" name="revokeEditOpen" opId="' + row.opId + '">启用</span>';
          }
        }
        /*if ($.inArray("FG_MEMU_MANAGER_USER_REVOKE", CTFO.cache.auth) > -1) {
            passeword = '<span class=" mr10 cBlue hand" title="重置密码" name="showRetPassWordPage" opId="'+ row.opId +'">重置密码</span>';
        }*/
        if ($.inArray(deleteRowAuth, CTFO.cache.auth) && row.opId != CTFO.cache.user.opId) {
          remove = '<span class=" mr10 cBlue hand" title="删除" name="removeOperatorById" opId="' + row.opId + '">删除</span>';
        }
        //  TODO
        return select + edit + revoke + remove + passeword;
      }
    }];

    /**
     * grid 参数设置
     */
    var roleManageGridInit = {
      pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
      pagesizeParmName: 'requestParam.rows',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      columns: gridcolumns,
      sortName: 'opUnName',
      url: CTFO.config.sources.findSpOperator,
      pageSize: pageSize,
      pageSizeOption: pageSizeOption,
      width: '100%',
      height: 450,
      delayLoad: false,
      usePager: true,
      rownumbers: true,
      checkbox: true,
      allowUnSelectRow: true,
      onSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindRowAction(eDom, rowData);
      },
      onUnSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindRowAction(eDom, rowData);
      }
    };
    var onDetailRow = function(opId) {
        getDetail(opId, DetailTmpl, function(content) {
          var param = {
            title: "用户详情",
            ico: 'ico226',
            width: 650,
            height: 270,
            content: content
          };
          CTFO.utilFuns.tipWindow(param);
        });
      };
      var getDetail = function(opId, tmpl, callback) {
          $.get(CTFO.config.sources.findSpOperatorById, {
        	  opId: opId
          }, function(data, textStatus, xhr) {
            var content = '';
            debugger;
            if (typeof(data) === 'string')
              data = JSON.parse(data);
            if (data && !data.error)
              content = compileDetail(data, tmpl);
            if (callback)
              callback(content, data);
          });
        };
      var compileDetail = function(d, tmpl) {
        var doTtmpl = doT.template(tmpl);
        d.isOperator = (+d.isOperator === 0 ? '普通用户' : (+d.isOperator === 1 ? '操作员' : ''));
        d.opSex = (+d.opSex === 0 ? '女' : (+d.opSex === 1 ? '男' : ''));
        if(d.documentType == 1){
        	d.documentType = '身份证';
        }
        if(d.documentType == 2){
        	d.documentType = '军官证';
        }
        if(d.documentType == 3){
        	d.documentType = '驾驶证';
        }       
        d.county = CTFO.utilFuns.codeManager.getCountyName(d.opProvince, d.opCity, d.opCountry);
        return doTtmpl(d);
      };        
    /**
     * [onModifyRow 绑定grid行事件]
     */
    var onModifyRow = function(opId) {
      addFlag = false;
      $.ajax({
        url: CTFO.config.sources.findSpOperatorById,
        type: 'POST',
        dataType: 'json',
        data: {
          'opId': opId
        },
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          resetThis();
          compileFormData(data);
          userManageFormContent.removeClass('none');
          userManageGridContent.addClass('none');

          //锁定用户姓名不可更改
          $(pushUserManage)
            .find('input[name="opLoginname"]').attr('disabled', 'true').end()
            .find('select[name="isOperator"]').parent().remove().end().end()
            .find('.passwordBox').addClass('none').find('input').attr('disabled', 'true').end().end()
            .find("li:gt(19)")
            .find('input').attr('disabled', 'true').end()
            .find('select').attr('disabled', 'true');

        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };
    /**
     * [onDeleteRow 绑定grid行事件]
     */
    var onDeleteRow = function(opId) {

      $.ligerDialog.confirm('真的要执行删除', '信息提示', function(yes) {
        if (yes) {
          $.ajax({
            url: CTFO.config.sources.removeUser + "?opId=" + opId,
            type: 'POST',
            dataType: 'json',
            data: opId,
            complete: function(xhr, textStatus) {
              //called when complete
            },
            success: function(data, textStatus, xhr) {
              $.ligerDialog.success("删除成功!", "信息提示");
              grid.loadData(true);
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
    var revokeEditStop = function(opId) {
        var opStatus = 0;

        $.ligerDialog.confirm('真的要执行吊销操作', '信息提示', function(yes) {
          if (yes) {
            $.ajax({
              url: CTFO.config.sources.revokeEditSpOperator + "?opId=" + opId + '&status=' + opStatus,
              type: 'POST',
              dataType: 'json',
              data: opId,
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
    var revokeEditStart = function(opId) {
      var opStatus = 1;
      $.ligerDialog.confirm('执行启用操作', '信息提示', function(yes) {
        if (yes) {
          $.ajax({
            url: CTFO.config.sources.revokeEditSpOperator + "?opId=" + opId + '&status=' + opStatus,
            type: 'POST',
            dataType: 'json',
            data: opId,
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
     * 操作按钮
     */
    var bindRowAction = function(eDom, rowData) {
      var actionType = $(eDom).attr('name');
      var opId = $(eDom).attr('opId');
      switch (actionType) {
      case 'rowDetail': // 查看
          onDetailRow(opId);
          break;      
        case 'updateSpOperator': // 修改
          onModifyRow(opId);
          break;
        case 'revokeEdit': //吊销
          revokeEditStop(opId);
          break;
        case 'revokeEditOpen': //启用
          revokeEditStart(opId);
          break;
        case 'showRetPassWordPage': //重置密码
          var opId = opId,
            userName = rowData.opLoginname;
          if (!!opId) {
            CTFO.Model.passwordSetWindow.getInstance().popResetPasswordWin({
              opId: opId,
              userName: userName,
              grid: grid
            });
          };
          break;
        case 'removeOperatorById': //删除
          onDeleteRow(opId);
          break;
      }
    };

    /**
     * 重置密码弹窗
     * 该功能已经被下边的弹框模块取代 guoyaunhua 2014.01.26
     */
    var RetPassWordPageForm = function(win, data) {
      var validate = win.find('form[name=RetPassWordPageForm]').validate({
        debug: false,
        errorClass: 'myselfError',
        messages: {},
        success: function() {}
      });
      win.find('input[name=rePasswordOperId]').val(data.userName);
      win.find('form[name=RetPassWordPageForm]').find('span[name=updatePassword]').click(function() {
        //验证
        if (!validate.form()) return false;
        var opId = data.opId;
        var opPass = hex_sha1(win.find('form[name=RetPassWordPageForm]').find('input[name=newPassword]').val()).toLowerCase();
        $.ligerDialog.confirm('确定要修改密码！', function(yes) {
          if (yes) {
            $.ajax({
              url: CTFO.config.sources.modifySpOperatorPassWord,
              type: 'POST',
              dataType: 'json',
              data: {
                'spOperator.opId': opId,
                'spOperator.opPass': opPass
              },
              complete: function(xhr, textStatus) {
                //called when complete
              },
              success: function(data, textStatus, xhr) {
                $(".l-dialog-close").click();
                $.ligerDialog.success("修改成功!", "信息提示");
                grid.loadData(true);
              },
              error: function(xhr, textStatus, errorThrown) {
                //called when there is an error
              }
            });
          }
        });
      });
    };


    /**
     * 装载grid列表
     */
    var initgrid = function() {
      grid = userManageBox.ligerGrid(roleManageGridInit);
    };

    /**
     * [initLeftTree 左侧树]
     * @return {[type]}
     */
    var initTreeContainer = function() {
      var options = {
        container: TreeContainer,
        menus: CTFO.cache.menus
      };
      leftTree = new CTFO.Model.Accordion(options);
    };


    /**
     * @description 清空表单
     */
    var resetThis = function() {
      $(userManageFormContent).find('input[type="text"]').each(function() {
        $(this).val("");
      }).end().find('select').each(function() {
        $(this).val("");
      }).end().find('textarea').each(function() {
        $(this).val("");
      }).end().find('input[type="password"]').each(function() {
        $(this).val("");
      }).end();
      //错误标签
      $(userManageFormContent).find('label[class="error"]').each(function() {
        $(this).remove();
      });
      $(userManageFormContent).find('.error').removeClass('error');
    };

    /**
     * 初始化新增页面
     */
    var initAddOrUpdateForm = function(container) {
      //生成省
      CTFO.utilFuns.codeManager.getProvinceList($(container).find("select[name='spOperator.opProvince']"));

      //联动市
      $(container).find('select[name="spOperator.opProvince"]').change(function() {
        CTFO.utilFuns.codeManager.getCityList($(container).find('select[name="spOperator.opProvince"]').val(), $(container).find('select[name="spOperator.opCity"]'));
      });
      //创建者
      $(container).find('input[name="spOperator.createBy"]').val(CTFO.cache.user.opId);

      $(userManageform).find('input[name=entIds]').val(CTFO.cache.user.entId);
      $(userManageform).find('.parentCorpDesc').text(CTFO.cache.user.entName);

      $(container).find('input[name="spOperator.opStartutc"]').click(function() {
        WdatePicker({
          dateFmt: 'yyyy-MM-dd',
          isShowClear: false,
          readOnly: true
        })
      });
      $(container).find('input[name="spOperator.opEndutc"]').click(function() {
        WdatePicker({
          dateFmt: 'yyyy-MM-dd',
          isShowClear: false,
          readOnly: true
        })
      });
    };


    /**
     * 添加取消按钮
     */
    var pushON = function(container) {

      initAddOrUpdateForm(pushUserManage); //初始化新增页面表单个文本域参数

      $(container).find('span[name=saveForm]').click(function(event) {

        //表单前端验证
        var validate = $(container).validate({
          debug: false,
          errorClass: 'myselfError',
          messages: {},
          success: function() {}
        });
        if (!validate.form()) {
          return false;
        }
        //验证登录名是否在企业中唯一
        if (addFlag && !userExist(container)) return false;
        var parms = [];
        var d = $(container).serializeArray();
        var psw = null;
        $(d).each(function() {
          if (this.name == "opPass" && this.value != '') {
            psw = this.value;
            parms.push({
              name: this.name,
              value: hex_sha1(this.value).toLowerCase()
            });
          } else {
            parms.push({
              name: this.name,
              value: $.trim(this.value)
            });
          }
        });

        /* if(psw && !CTFO.utilFuns.commonFuns.checkPwdMode(psw)){
                	  $.ligerDialog.error("密码格式错误,字符,字母和数字，至少有两种");
                	  return false;
                  }*/
        if (pushUserManage.find('span[name="saveForm"]').attr('disabled')) {
          return false;
        }
        var newParms = {};
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
            url: addFlag ? CTFO.config.sources.addSpOperator : CTFO.config.sources.modifySpOperator,
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
                    userManageGridContent.removeClass('none');
                    userManageFormContent.addClass('none');
                    grid.loadData(true);
                    resetThis();
                  };
                  addFlag = true;
                });

                //日志统计
                cLog.addOperatorLog($.extend({}, CTFO.cache.cLogInfo, {
                  opType: addFlag ? "新增操作" : "修改操作", // (必填)
                  logTypeId: 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作(必填)
                  logClass: 'CTFO.Model.userManage', //类名称
                  logMethod: 'grid.loadData', // 执行方法
                  executeTime: '', // 调用方法执行时间毫秒
                  logDesc: '' // 操作成功/操作失败
                }));

              } else {
                $.ligerDialog.error(text + "失败");
              }
            }
          });
      });


      // 取消按钮
      container.find('span[name="cancelSave"]').click(function(event) {
        userManageGridContent.removeClass('none');
        userManageFormContent.addClass('none');
        resetThis();
        $(userManageform).find('.queryButton').trigger('click');
        addFlag = true;
      });
    };


    /**
     * @description 处理按钮
     * @param boolean
     */
    var disabledButton = function(boolean) {
      if (boolean) {
        pushUserManage.find('span[name="saveForm"]').attr('disabled', true);
      } else {
        pushUserManage.find('span[name="saveForm"]').attr('disabled', false);
      }
    };

    /**
     * 用户是否存在验证
     */
    var userExist = function(container) {
      var Loginname = $(container).find('input[name="opLoginname"]').val().toLowerCase();
      var updateId = $(container).find('input[name="opId"]').val();

      var flag = false;
      $.ajax({
        url: CTFO.config.sources.isExistSpOperator,
        type: 'POST',
        async: false,
        dataType: 'json',
        data: {
          "opLoginname": Loginname,
          "noId": updateId
        },
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {

          if (data.displayMessage == "success") {
            $.ligerDialog.success("账号已经存在请重新输入");
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
     * 初始化赋值操作
     */
    var compileFormData = function(d) {
      var d = d || {};
      var doTtmpl = doT.template(userManageDetailTmpl);
      $(userManageFormContent).find(".userFormBox").html(doTtmpl(d));
      

      var comSelect = $(userManageFormContent).find('select[name=comId]');
      var entSelect = $(userManageFormContent).find('select[name=entId]');

      initCompanySelectList(comSelect, d.comId);
      initEntSelectList(entSelect, d.comId , d.entId);
      comSelect.change(function() {
          initEntSelectList(entSelect, $(comSelect).val() );
      });
      initRoleSelectList($(userManageFormContent).find('select[name=roleId]'), d.roleId);


      var provinceOption = $(pushUserManage).find('select[name=opProvince]'),
        cityOption = $(pushUserManage).find('select[name=opCity]'),
        countyOption = $(pushUserManage).find('select[name=opCountry]');

      CTFO.utilFuns.codeManager.initProvAndCityAndCounty(provinceOption, cityOption, countyOption, d.opProvince, d.opCity, d.opCountry);

    };

    /**
     *
     */
    var onAdd = function() {

      resetThis();
      addFlag = true;
      userManageFormContent.removeClass('none');
      userManageGridContent.addClass('none');
      compileFormData();
      userManageFormContent
      .find('select[name="isOperator"]').parent().remove().end().end()
      .find("li:gt(19)").addClass('none');

      $.ajax({
        url: CTFO.config.sources.queryAutoCodeOfOp,
        dataType: 'json',
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          $(pushUserManage).find('input[name=opCode]').val(data);
          $(pushUserManage).find('input[name=opCodeTxt]').val(data);
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });

      //初始用户姓名输入框
      $(pushUserManage).find('input[name="opLoginname"]').removeAttr('disabled');
      //初始密码输入框
      $(pushUserManage).find('li.passwordBox').show().find('input[type=password]').removeAttr('disabled');


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
        data = userManageform.serializeArray();
      $(data).each(function(event) {
        var name = '';
        if (this.name == 'opLoginname' || this.me == 'opName') name = 'requestParam.like.' + this.name;
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

    /**
     * form表单提交
     */
    var initForm = function() {

      var comSelect = $(userManageform).find('select[name=comId]');
      var entSelect = $(userManageform).find('select[name=entId]');
      initCompanySelectList(comSelect);
      comSelect.change(function() {
          initEntSelectList(entSelect, $(comSelect).val() );
      });
      initRoleSelectList($(userManageform).find('select[name=roleId]'));

      $(userManageform)
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
        .find('.queryButton').click(function(event) {
          searchGrid();
        }).end()
        .find('.userManageAddBtn').click(function(event) {
          onAdd();
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
        .find('.exportGrid').click(function(event) {
          CTFO.utilFuns.commonFuns.exportGrid({
            grid: grid,
            url: CTFO.config.sources.exportUserManageExcelData
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
      $(userManageform).find('input[type="text"]').each(function() {
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
        if (item.registerAuthentication == 1) {
          return item.opId
        }
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
        if (item.registerAuthentication == 1) {
          return item.opId
        }
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
          return item.opId
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
      onModifyRow(row.opId);
    }
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
    var initEntSelectList = function(container , comVal , defaultVal) {
      $.ajax({
        url: CTFO.config.sources.querySysEntList+"?comId="+comVal,
        dataType: 'json',
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          CTFO.utilFuns.commonFuns.getSelectList(_.map(data, function(item) {
            return {
              "code": item.entId,
              "name": item.entName
            };
          }), container, defaultVal);
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };
    var initRoleSelectList = function(container, defaultVal) {
      $.ajax({
        url: CTFO.config.sources.findAllRoleList,
        dataType: 'json',
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          CTFO.utilFuns.commonFuns.getSelectList(_.map(data, function(item) {
            return {
              "code": item.roleId,
              "name": item.roleName
            };
          }), container, defaultVal);
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };
    var resize = function(ch) {
      if (ch < minH) ch = minH;
      p.mainContainer.height(ch);
      gridListBox.height(p.mainContainer.height() - pageLocation.outerHeight() - userManageTerm.outerHeight() - parseInt(gridListBox.css('margin-top')) * 3 - parseInt(gridListBox.css('border-top-width')) * 2)

      gridHeight = gridListBox.height();
      if (grid) grid = userManageBox.ligerGrid({
        height: gridHeight
      });

      TreeContainer.height(ch);
      if (leftTree) leftTree.resize();

    };

    return {
      init: function(options) {
        p = $.extend({}, p || {}, options || {});
        pageLocation = p.mainContainer.find('.pageLocation'); //当前位置
        userManageTerm = p.mainContainer.find('.userManageTerm'); //查询条件盒子
        userManageform = p.mainContainer.find('form[name=userManageform]'); //查询form

        gridListBox = p.mainContainer.find('.gridListBox');
        userManageBox = p.mainContainer.find('.userManageBox'); //grid表格展现盒子
        TreeContainer = p.mainContainer.find('.TreeContainer');

        userManageGridContent = p.mainContainer.find('.userManageContent:eq(0)'); //查询条件以及表格容器
        userManageFormContent = p.mainContainer.find('.userManageContent:eq(1)');
        pushUserManage = p.mainContainer.find('form[name="pushUserManage"]'); //添加表单

        userManageDetailTmpl = $('#user_manage_detail_tmpl').html();
        DetailTmpl = $('#user_manage_detail_tmpl_slelet').html();


        initTreeContainer();

        initAuth(userManageTerm);
        pushON(pushUserManage);
        initForm();
        initgrid();


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
        $(p.mainContainer).remove();
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


/**
 * 密码修改弹窗控件
 */
CTFO.Model.passwordSetWindow = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var $form = null;
    var validateForm = null;

    /**
     * 初始化密码修改判断条件
     */
    var initValidate = function() {
      validateForm = $form.validate({
        rules: {
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
        return CTFO.utilFuns.commonFuns.checkPwdMode(value)
      }, '字符,字母和数字，至少有两种');
    };

    /**
     * 绑定表单提交事件
     */
    var bindSubmitFormEvent = function() {
      $form.find('span[name=updatePassword]').bind('click', function(evt) {
        var isPass = validateForm.form();
        if (isPass) {
          var opId = p.opId;
          var opPass = hex_sha1($form.find('input[name=newPassword]').val()).toLowerCase();
          $.ligerDialog.confirm('确定要修改密码？', function(yes) {
            if (yes) {
              $.ajax({
                url: CTFO.config.sources.modifySpOperatorPassWord,
                type: 'POST',
                dataType: 'json',
                data: {
                  'spOperator.opId': opId,
                  'spOperator.opPass': opPass
                },
                complete: function(xhr, textStatus) {
                  //called when complete
                },
                success: function(data, textStatus, xhr) {
                  $(".l-dialog-close").click();
                  $.ligerDialog.success("修改成功!", "信息提示");
                  p.grid.loadData(true);
                },
                error: function(xhr, textStatus, errorThrown) {
                  //called when there is an error
                }
              });
            }
          });
        }
      });
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
        url: CTFO.config.template.RetPassWordPage,
        onLoad: function(w, d) {
          $form = $(w).find('form[name=RetPassWordPageForm]');
          $form.find('input[name=rePasswordOperId]').val(p.userName);
          initValidate();
          bindSubmitFormEvent();

          //日志统计
          cLog.addOperatorLog($.extend({}, CTFO.cache.cLogInfo, {
            opType: '用户重置密码', // (必填)
            logTypeId: 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作(必填)
            logClass: 'CTFO.Model.userManage', //类名称
            logMethod: 'RetPassWordPageForm', // 执行方法
            executeTime: '', // 调用方法执行时间毫秒
            logDesc: '' // 操作成功/操作失败
          }));
        }
      };
      CTFO.utilFuns.tipWindow(param);
    };

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