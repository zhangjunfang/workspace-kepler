/**
 * [ 业务管理 - 注册鉴权审批-查询]                                                                                        resize(ch [description]
 * @return {[type]}         [description]
 */
CTFO.Model.AuthManage = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var pDeail = {};
    var cHeight = 0,
      minH = 520, // 本模块最低高度
      gridHeight = 300, // grid高度
      pageSize = 50,
      pageSizeOption = [10, 20, 30, 40, 50, 100],

      treeContainer = null,
      authManageForm = null,
      addAppForm = null,
      vehicleTeamTerm = null,
      gridContainer = null,
      gridContainerDeail = null,
      gridContainerDeailManage = null,
      gridAddApp = null,

      leftTree = null,
      grid = null,
      gridDeail = null,
      addFlag = null,
      currentEntName = '',

      vehicleTeamDetailTmpl = null,
      vehicleTeamManualTmpl = null,
      vehicleTeamModifyTmpl = null,
      vehicleTeamApprovalTmpl = null,

      approverData = null,

      updateRowAuth = 'FG_MEMU_BUSINESS_AUTHMANAGE_U', // 修改记录权限
      detailRowAuth = 'FG_MEMU_BUSINESS_AUTHMANAGE_I', // 查看记录详情权限
      deleteRowAuth = 'FG_MEMU_BUSINESS_AUTHMANAGE_D', // 删除记录权限
      addRowAuth = 'FG_MEMU_BUSINESS_AUTHMANAGE_C', // 新增记录权限
      exportRowAuth = 'FG_MEMU_BUSINESS_AUTHMANAGE_E', // 导出记录权限
      opLogRowAuth = 'FG_MEMU_BUSINESS_AUTHMANAGE_O', // 操作记录权限
      applyRowAuth = 'FG_MEMU_BUSINESS_AUTHMANAGE_A', // 审批权限
      reAppalyRowAuth = 'FG_MEMU_BUSINESS_AUTHMANAGE_RA', //重新授权
      startRowAuth = 'FG_MEMU_BUSINESS_AUTHMANAGE_S', //启用/吊销

      isPassByValueMode = CTFO.config.globalObject.isPassByValueMode, //传值方式
      test = '';
    // grid展现列
    var columns = [{
      display: '公司名称',
      name: 'comName',
      width: 100,
      sortable: true,
      align: 'center',
      toggle: false
    }, {
      display: '机器序列号',
      name: 'machineSerial',
      width: 150,
      sortable: true,
      align: 'center'
    }, {
      display: '注册时间',
      name: 'registTime',
      width: 150,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.dateFuns.utc2date(row.registTime);
      }
    }, {
      display: '注册IP',
      name: 'registIp',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '注册鉴权情况',
      name: 'registerAuthentication',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        if (row.registerAuthentication == 0)
          return "授权过期";
        if (row.registerAuthentication == 1)
          return "已授权";
        if (row.registerAuthentication == 2)
          return "<span class='cF00'>待授权</span>";
        if (row.registerAuthentication == 3)
          return "拒绝授权";
      }
    }, {
      display: '状态',
      name: 'status',
      width: 80,
      sortable: true,
      align: 'center',
      render: function(row) {
        if (row.registerAuthentication == 1) {
          if (row.status == 0)
            return "吊销";
          if (row.status == 1)
            return "启用";
        }
      }
    }, {
      display: '授权码',
      name: 'authorizationCode',
      width: 250,
      sortable: true,
      align: 'center'
    }, {
      display: '有效期',
      name: 'validDate',
      width: 150,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.dateFuns.utc2date(row.validDate);
      }
    }, {
      display: '审批人',
      name: 'approver',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.commonFuns.getNameByCode(approverData, row.approver);
      }
    }, {
      display: '审批时间',
      name: 'approvalTime',
      width: 150,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.dateFuns.utc2date(row.approvalTime);
      }
    }, {
      display: '备注',
      name: 'remark',
      width: 120,
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
        if (row.registerAuthentication != 5) {
          var detailHtml = "<span class='cBlue'><font title='查看' class='hand' actionType='rowDetail'>查看</font></span>&nbsp;";
          detailHtml = ($.inArray(detailRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
          buttons.push(detailHtml);
        }
        if (row.registerAuthentication == 2) {
          var detailHtml = "<span class='cBlue'><font title='审批' class='hand' actionType='rowApproval'>审批</font></span>&nbsp;";
          detailHtml = ($.inArray(applyRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
          buttons.push(detailHtml);
        }
        if (row.registerAuthentication == 1) {
          var detailHtml = "<span class='cBlue'><font title='编辑' class='hand' actionType='rowManual'>管理</font></span>&nbsp;";
          detailHtml = ($.inArray(updateRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
          buttons.push(detailHtml);
        }
        if (row.registerAuthentication == 0 || row.registerAuthentication == 3) {
          var detailHtml = "<span class='cBlue'><font title='重新授权' class='hand' actionType='rowManual'>重新授权</font></span>&nbsp;";
          detailHtml = ($.inArray(reAppalyRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
          buttons.push(detailHtml);
        }
        /*if (row.registerAuthentication == 1) {
          if (row.status == 0) {
            var detailHtml = "<span class='cBlue'><font title='启用' class='hand' actionType='revokeEditOpen'>启用</font></span>&nbsp;";
            detailHtml = ($.inArray(startRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
            buttons.push(detailHtml);
          }
          if (row.status == 1) {
            var detailHtml = "<span class='cBlue'><font title='吊销' class='hand' actionType='revokeEdit'>吊销</font></span>&nbsp;";
            detailHtml = ($.inArray(startRowAuth, CTFO.cache.auth) > 0) ? detailHtml : '--';
            buttons.push(detailHtml);
          }
        }*/

        return buttons.join('');
      }
    }];
    // 增值应用（详细）
    var columnsDeail = [{
      display: '应用',
      name: 'bizName',
      width: 100,
      sortable: true,
      align: 'center',
      toggle: false
    }, {
      display: '注册鉴权情况',
      name: 'registerAuthentication',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        if (row.registerAuthentication == 0)
          return "授权过期";
        if (row.registerAuthentication == 1)
          return "已授权";
        if (row.registerAuthentication == 2)
          return "待授权";
        if (row.registerAuthentication == 3)
          return "拒绝授权";
      }
    }, {
      display: '状态',
      name: 'status',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        if (row.status == 0)
          return "吊销";
        if (row.status == 1)
          return "启用";
      }
    }, {
      display: '有效期',
      name: 'validDate',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.dateFuns.utc2date(row.validDate);
      }
    }, {
      display: '备注',
      name: 'remark',
      width: 300,
      sortable: true,
      align: 'center'
    }];
    // 增值应用（管理）
    var columnsDeailManage = [{
      display: '应用',
      name: 'bizName',
      width: 100,
      sortable: true,
      align: 'center',
      toggle: false
    }, {
      display: '注册鉴权情况',
      name: 'registerAuthentication',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        if (row.registerAuthentication == 0)
          return "授权过期";
        if (row.registerAuthentication == 1)
          return "已授权";
        if (row.registerAuthentication == 2)
          return "待授权";
        if (row.registerAuthentication == 3)
          return "拒绝授权";
      }
    }, {
      display: '状态',
      name: 'status',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        if (row.registerAuthentication == 1) {
          if (row.status == 0)
            return "吊销";
          if (row.status == 1)
            return "启用";
        }
      }
    }, {
      display: '处理',
      name: 'processingStatus',
      width: 150,
      sortable: true,
      align: 'center',
      render: function(row) {
        var buttons = [];
        if (row.registerAuthentication == 1) {
          if (row.status == 0) {
            var html = "<span class='cBlue'><font title='启用' class='hand' actionType='revokeEditOpenCloud'>启用</font></span>&nbsp;";
            html = ($.inArray(startRowAuth, CTFO.cache.auth) > 0) ? html : '--';
            buttons.push(html);
          } else {
            var html = "<span class='cBlue'><font title='吊销' class='hand' actionType='revokeEditCloud'>吊销</font></span>&nbsp;";
            html = ($.inArray(startRowAuth, CTFO.cache.auth) > 0) ? html : '--';
            buttons.push(html);
          }
        }
        if (row.registerAuthentication == 0 || row.registerAuthentication == 3 || row.registerAuthentication == 2) {
          var html = "<span class='cBlue'><font title='重新授权' class='hand' actionType='reAuthorizationCloud'>重新授权</font></span>&nbsp;";
          html = ($.inArray(reAppalyRowAuth, CTFO.cache.auth) > 0) ? html : '--';
          buttons.push(html);
        }
        return buttons.join('');
      }
    }, {
      display: '有效期',
      name: 'validDate',
      width: 120,
      sortable: true,
      align: 'center',
      render: function(row) {
        var validDate = CTFO.utilFuns.dateFuns.utc2date(row.validDate);
        if (row.registerAuthentication == 0 || row.registerAuthentication == 3) {
          return "<input type='text' class='Wdate' name='addValidDate' value='" + validDate + "' />";
        } else {
          return validDate;
        }
      }
    }, {
      display: '备注',
      name: 'remark',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        if (row.registerAuthentication == 0 || row.registerAuthentication == 3) {
          return "<input type='text' maxlength='50' style='width:200px;height:20px' name=addAppRemark_" + row.autoId + ">";
        } else {
          return row.remark;
        }
      }
    }];
    //增值应用（新增离线）
    var columnsAddApp = [{
      display: '应用',
      name: 'bizName',
      width: 100,
      sortable: true,
      align: 'center',
      toggle: false
    }, {
      display: '注册鉴权情况',
      name: 'registerAuthentication',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        if (row.registerAuthentication == 0)
          return "授权过期";
        if (row.registerAuthentication == 1)
          return "已授权";
        if (row.registerAuthentication == 2)
          return "待授权";
        if (row.registerAuthentication == 3)
          return "拒绝授权";
      }
    }, {
      display: '处理',
      name: 'processingStatus',
      width: 150,
      sortable: true,
      align: 'center',
      render: function(row) {
        return "<span class='cBlue'><input type='radio' value='1' name=a_" + row.autoId + " checked >授权</span>&nbsp;<span class='cBlue'><input type='radio' value='3' name=a_" + row.autoId + " >拒绝</span>";
        //return '<p class="lh25 h25 tr"><span class="fl w50 checkbox"><input class="w20" name="photoSharpness" value="1" type="radio">授权&nbsp;&nbsp;</span><span class="fl w50 checkbox"><input class="w20" name="photoSharpness" value="1" type="radio">拒绝</span></p>';
      }
    }, {
      display: '有效期',
      name: 'validDate',
      width: 120,
      sortable: true,
      align: 'center',
      render: function(row) {
        var validDate = CTFO.utilFuns.dateFuns.dateFormat(new Date(), 'yyyy-MM-dd')
        return "<input type='text' class='Wdate' name='addValidDate' value='" + validDate + "' />";
      }
    }, {
      display: '备注',
      name: 'remark',
      width: 300,
      sortable: true,
      align: 'center',
      render: function(row) {
        return "<input type='text' maxlength='50' style='width:200px;height:20px' name=addAppRemark_" + row.autoId + ">";
      }
    }];

    // grid初始化参数
    var gridOptions = {
      columns: columns,
      sortName: 'create_time',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      url: CTFO.config.sources.authManageGrid, // 数据请求地址
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
    // gridDeail初始化参数
    var gridDeailOptions = {
      columns: columnsDeail,
      sortName: 'createTime',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      pageSize: 10,
      pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
      pagesizeParmName: 'requestParam.rows',
      width: '100%',
      height: '100',
      delayLoad: false,
      rownumbers: true
    };
    var gridDeailManageOptions = {
      columns: columnsDeailManage,
      sortName: 'createTime',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      pageSize: 5,
      pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
      pagesizeParmName: 'requestParam.rows',
      width: '100%',
      height: '100',
      delayLoad: false,
      rownumbers: true,
      allowUnSelectRow: true,
      onSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindManageGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onUnSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindManageGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onAfterShowData: function() {
        $(gridContainerDeailManage).find('input[name="addValidDate"]').click(function(event) {
          WdatePicker({
            dateFmt: 'yyyy-MM-dd',
            isShowClear: false
          });
        });
      },
      onSuccess: function(data) {

      }
    };
    var gridDetailAddApp = {
      columns: columnsAddApp,
      sortName: 'createTime',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      pageSize: 5,
      pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
      pagesizeParmName: 'requestParam.rows',
      width: '100%',
      height: '100',
      delayLoad: false,
      rownumbers: true,
      allowUnSelectRow: true,
      onSelectRow: function(rowData, rowIndex, rowDom, eDom) {},
      onUnSelectRow: function(rowData, rowIndex, rowDom, eDom) {},
      onAfterShowData: function() {},
      onAfterShowData: function() {
        $(gridAddApp).find('input[name="addValidDate"]').click(function(event) {
          WdatePicker({
            dateFmt: 'yyyy-MM-dd',
            isShowClear: false
          });
        });
      },
      onSuccess: function(data) {

      }
    };
    /**
     * [revokeEditStop 吊销]
     */
    var revokeEditStop = function(comId) {
        var status = 0;
        $.ligerDialog.confirm('真的要执行吊销操作', '信息提示', function(yes) {
          if (yes) {
            $.ajax({
              url: CTFO.config.sources.revokeEditAuth + "?comId=" + comId + '&status=' + status,
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
       * [revokeEditStart 启用]
       */
    var revokeEditStart = function(comId) {
        var status = 1;
        $.ligerDialog.confirm('真的要执行启用操作', '信息提示', function(yes) {
          if (yes) {
            $.ajax({
              url: CTFO.config.sources.revokeEditAuth + "?comId=" + comId + '&status=' + status,
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
      var name;
      switch (actionType) {
        case 'rowDetail':
          showVehicleTeamDetail(rowData.comId, actionType);
          break;
        case 'rowManual':
          showVehicleTeamDetail(rowData.comId, actionType);
          break;
        case 'rowApproval':
          showVehicleTeamDetail(rowData.comId, actionType);
          break;
        case 'revokeEdit': //吊销
          revokeEditStop(rowData.comId);
          break;
        case 'revokeEditOpen': //启用
          revokeEditStart(rowData.comId);
          break;
      }
      return !actionType;
    };
    var bindManageGridRowEvent = function(rowData, rowIndex, rowDom, eDom) {
      var actionType = $(eDom).attr('actionType');
      switch (actionType) {
        case 'revokeEditCloud': //吊销
          var comId = rowData.comId;
          var valueAddId = rowData.valueAddId;
          var status = 0;
          $.ligerDialog.confirm('真的要执行吊销操作', '信息提示', function(yes) {
            if (yes) {
              $.ajax({
                url: CTFO.config.sources.revokeEditAuthCloud + "?comId=" + comId + '&status=' + status + '&valueAddId=' + valueAddId,
                complete: function(xhr, textStatus) {
                  //called when complete
                },
                success: function(data, textStatus, xhr) {
                  $.ligerDialog.success("吊销成功!", "信息提示");
                  gridDeail.loadData(true);
                },
                error: function(xhr, textStatus, errorThrown) {
                  //called when there is an error
                }
              });
            }
          });
          break;
        case 'revokeEditOpenCloud': //启用
          var comId = rowData.comId;
          var valueAddId = rowData.valueAddId;
          var status = 1;
          $.ligerDialog.confirm('真的要执行吊销操作', '信息提示', function(yes) {
            if (yes) {
              $.ajax({
                url: CTFO.config.sources.revokeEditAuthCloud + "?comId=" + comId + '&status=' + status + '&valueAddId=' + valueAddId,
                complete: function(xhr, textStatus) {
                  //called when complete
                },
                success: function(data, textStatus, xhr) {
                  $.ligerDialog.success("启用成功!", "信息提示");
                  gridDeail.loadData(true);
                },
                error: function(xhr, textStatus, errorThrown) {
                  //called when there is an error
                }
              });
            }
          });
          break;
        case 'reAuthorizationCloud': //重新授权
          var comId = rowData.comId;
          var valueAddId = rowData.valueAddId;
          var addAppValidDate;
          var addAppRemark;
          var addValidDate = $(rowDom).find("input[name='addValidDate']").val();
          var addAppRemark = $(rowDom).find("input[name^='addAppRemark_']").val();
          if (addValidDate) {
            $.ligerDialog.confirm('真的要执行重新授权操作', '信息提示', function(yes) {
              if (yes) {
                $.ajax({
                  url: CTFO.config.sources.reAuthorizationCloud,
                  type: "POST",
                  data: {
                    'comId': comId,
                    'valueAddId': valueAddId,
                    'addValidDate': addValidDate,
                    'addAppRemark': addAppRemark
                  },
                  complete: function(xhr, textStatus) {
                    //called when complete
                  },
                  success: function(data, textStatus, xhr) {
                    $.ligerDialog.success("重新授权成功!", "信息提示");
                    gridDeail.loadData(true);
                  },
                  error: function(xhr, textStatus, errorThrown) {
                    //called when there is an error
                  }
                });
              }
            });
          } else {
            $.ligerDialog.error("请填写有效期！", "信息提示");
          }
          break;
      }
      return !actionType;
    };
    var initWindow = function(t, w, d, g) {

      initApproverSelectList($(w).find("select[name='approver']"), d.approver);

      if (t === 'm') {
        $(w).find('input[name=machineSerial]').bind("blur", function(event) {
          var machineSerial = $(w).find("input[name=machineSerial]").attr('value')
          $.ajax({
            url: CTFO.config.sources.buildAuthentication + "?machineSerial=" + machineSerial,
            complete: function(xhr, textStatus) {
              //called when complete
            },
            success: function(data, textStatus, xhr) {
              if ("" == data.result) {

              } else {
                var result = data.result;
                var array = result.split(":");
                $(w).find('input[name=comAccount]').val(array[0]);
                $(w).find('input[name=comPassWord]').val(array[1]);
                $(w).find('input[name=authCode]').val(array[2]);
                $(w).find('input[name=authorizationCode]').val(array[3]);
              }
            },
            error: function(xhr, textStatus, errorThrown) {
              //called when there is an error
            }
          });
        });
        $(w).find('input[name="validDate"]').click(function(event) {
          WdatePicker({
            dateFmt: 'yyyy-MM-dd HH:mm:ss',
            isShowClear: false
          });
        });
      }
      var actionUrl = t === 'a' ? CTFO.config.sources.updateAuthApproval : (t === 'm' ? CTFO.config.sources.updateAuthManage : '');
      $(w).find('.saveButton').click(function(event) {
          var formContainer = $(w).find('form[name=authApprovalForm]');
          //表单前端验证
          var validate = formContainer.validate({
            debug: false,
            errorClass: 'myselfError',
            messages: {},
            success: function() {}
          });
          if (!validate.form()) {
            return false;
          }
          var obj = this;
          if ($(obj).attr("disabled")) {
            return false;
          }
          $(obj).attr("disabled", true);
          var d = formContainer.serializeArray();
          var param = {};
          $(d).each(function(event) {
            param[this.name] = this.value;
            if (this.name == "validDate") {
              param[this.name] = CTFO.utilFuns.dateFuns.date2utc(this.value);
            }
          });
          param["authorizationCode"] = $("input[name=authorizationCode]").attr('value');
          updateAuthApprovalForm(actionUrl, param, t, function() {
            $(obj).attr("disabled", false);
            g.close();
            authManageForm.find('.queryButton').trigger('click');
          });
        }).end()
        .find('.cancelButton').click(function(event) {
          $(w).find('.saveButton').attr("disabled", false);
          g.close();
        });
    };
    /**
     * [showVehicleTeamDetail 显示组织详情]
     * @param  {[String]} orgId [组织id]
     * @return {[type]}       [description]
     */
    var showVehicleTeamDetail = function(comId, name) {
      if (name == "rowDetail") {
        getVehicleTeamDetail(comId, vehicleTeamDetailTmpl, function(content, data) {
          var param = {
            title: "详情",
            ico: 'ico226',
            width: 850,
            height: 580,
            data: data,
            onLoad: function(w, d, g) {
              initWindow('t', w, d, g);
              initGridDeail(w, comId);
            },
            content: content
          };
          CTFO.utilFuns.tipWindow(param);
        });
      } else if (name == "rowManual") {
        getVehicleTeamDetail(comId, vehicleTeamManualTmpl, function(content, data) {
          var param = {
            title: "管理",
            ico: 'ico226',
            width: 850,
            height: 580,
            data: data,
            onLoad: function(w, d, g) {
              initWindow('m', w, d, g);
              initGridManage(w, comId);
            },
            content: content
          };
          CTFO.utilFuns.tipWindow(param);
        });
      } else if (name == "rowApproval") {
        getVehicleTeamDetail(comId, vehicleTeamApprovalTmpl, function(content, data) {
          var param = {
            title: "审批",
            ico: 'ico226',
            width: 850,
            height: 580,
            data: data,
            onLoad: function(w, d, g) {
              initWindow('a', w, d, g);
              initGridApproval(w, comId);
            },
            content: content
          };
          CTFO.utilFuns.tipWindow(param);
        });
      }
    };
    /**
     * [getVehicleTeamDetail 获取车队详情]
     * @param  {[String]}   orgId    [组织id]
     * @param  {[Object]}   tmpl     [详情模板对象]
     * @param  {Function} callback [回调函数]
     * @return {[type]}            [description]
     */
    var getVehicleTeamDetail = function(comId, tmpl, callback) {
      $.get(CTFO.config.sources.authDetail, {
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
      if (d.repairQualification == 1) {
        d.repairQualification = "3A";
      }
      if (d.repairQualification == 2) {
        d.repairQualification = "2A";
      }
      if (d.repairQualification == 3) {
        d.repairQualification = "1A";
      }
      if (d.repairQualification == 4) {
        d.repairQualification = "快修";
      }
      if (d.repairQualification == 5) {
        d.repairQualification = "特许";
      }
      if (d.unitProperties == 1) {
        d.unitProperties = "国营";
      }
      if (d.unitProperties == 2) {
        d.unitProperties = "民营";
      }
      if (d.unitProperties == 3) {
        d.unitProperties = "合资";
      }
      if (d.unitProperties == 4) {
        d.unitProperties = "其它";
      }
      d.approverTxt = CTFO.utilFuns.commonFuns.getNameByCode(approverData, d.approver);
      if(d.approvalAdvice == ''){
      }
      else if (d.approvalAdvice == 0) {
        d.approvalAdvice = "授权通过";
      }
      else if (d.approvalAdvice == 1) {
        d.approvalAdvice = "授权拒绝";
      }
      if (d.registerAuthentication == 0) {
        d.registerAuthenticationTxt = "授权过期";
        d.status = '';
      }
      if (d.registerAuthentication == 1) {
        d.registerAuthenticationTxt = "已授权";
        if (d.status == 0)
          d.status = "吊销";
        if (d.status == 1)
          d.status = "启用";
      }
      if (d.registerAuthentication == 2) {
        d.registerAuthenticationTxt = "待授权";
        d.status = '';
      }
      if (d.registerAuthentication == 3) {
        d.registerAuthenticationTxt = "拒绝授权";
        d.status = '';
      }
      return doTtmpl(d);
    };
    var validateFormParams = function(w) {
      var validator = $(w).find('form[name=addAppForm]').validate({
        errorClass: 'myselfError',
        rules: {
          comName: {
            required: true,
            maxlength: 12
          },
          comContact: {
            required: true,
            maxlength: 12
          },
          comAddress: {
            required: true,
            maxlength: 50
          },
          comTel: {
            required: true,
            maxlength: 12
          },
          province: {
            required: true
          },
          city: {
            required: true
          },
          comEmail: {
            emailExtend: true,
            maxlength: 40
          },
          machineSerial: {
            required: true,
            maxlength: 32
          },
          zipCode: {
            zipcode: true
          },
          legalPerson: {
            maxlength: 12
          },
          hotLtel: {
            maxlength: 12
          },
          authorizationCode: {
            required: true
          },
          validDate: {
            required: true
          },
          approvalAdvice: {
            required: true
          },
          approver: {
            required: true
          }
        },
        submitHandler: function() {
          //alert(123);
        }
      });
      return validator;
    };
    /**
     * [saveDownAuthDetailFrom 新增离线授权信息]
     * @param  {[String]}   url      [请求action]
     * @param  {[Object]}   param    [请求参数]
     * @param  {Function} callback [回调函数]
     * @return {[type]}            [description]
     */
    var saveDownAuthDetailFrom = function(url, parms, callback) {
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

    var updateAuthApprovalForm = function(url, parms, t, callback) {
      var newParms = {};
      $.each(parms, function(name, val) {
        if (name.indexOf(".") > -1) {
          newParms[name.split(".")[1]] = val;
        } else {
          newParms[name] = val;
        }
      });
      if(t === 'a'){
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
                if(data.displayMessage == "success"){
                	alert("软件授权码:"+data.opInfo);
                }
              },
              error: function(xhr, textStatus, errorThrown) {
                //called when there is an error
              }
            });
      }else{
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
      }
    };

    /**
     * 后台AJAX验证
     *
     * @param container
     *            数据dom对象
     */
    var validateEntName = function(t, container) {
      var parms = {};
      var entName = $(container).find('form[name=vehicleTeamModifyForm]').find("input[name='entName']");
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
      authManageForm
        .find('.addVehicleTeam').click(function(event) {
          inisertVehicleTeam();
        }).end()
        .find('.detailButton').click(function(event) {
          onDetail();
        }).end()
        .find('.editButton').click(function(event) {
          onEdit();
        }).end()
        .find('.approvalButton').click(function(event) {
          onApproval();
        }).end()
        .find('.startButton').click(function(event) {
          onStartData();
        }).end()
        .find('.stopButton').click(function(event) {
          onStopData();
        }).end()
        .find('.reAppalyButton').click(function(event) {
          onReAppaly();
        }).end()
        .find('.resetButton').click(function(event) {
          resetThis();
        });
    };
    /**
     * @description 清空表单
     */
    var resetThis = function() {
      $(authManageForm).find('input[type="text"]').each(function() {
        $(this).val("");
      }).end().find('select').each(function() {
        $(this).val("");
      }).end().find('textarea').each(function() {
        $(this).val("");
      }).end();

      var provinceOption = $(authManageForm).find('select[name=corpProvince]'),
        cityOption = $(authManageForm).find('select[name=corpCity]'),
        countyOption = $(authManageForm).find('select[name=corpCounty]');

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
        if (item.registerAuthentication == 1) {
          return item.comId
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
          return item.comId
        }
      });
      if (arr.length > 0) {
        revokeEditStart(arr.join(","));
      }
    };
    /**
     * [onReAppaly 查看事件]
     * @return {[type]} [description]
     */
    var onReAppaly = function() {
      var record = grid.getCheckedRows();
      if (record.length <= 0) {
        $.ligerDialog.error('请勾选单条操作记录!');
        return;
      } else if (record.length > 1) {
        $.ligerDialog.error('请勾选单条操作记录!');
        return;
      }
      var row = record[0];
      if (row.registerAuthentication == 0 || row.registerAuthentication == 3) {
        showVehicleTeamDetail(row.comId, 'rowManual');
      }
    };
    /**
     * [onDetail 查看事件]
     * @return {[type]} [description]
     */
    var onApproval = function() {
      var record = grid.getCheckedRows();
      if (record.length <= 0) {
        $.ligerDialog.error('请勾选单条操作记录!');
        return;
      } else if (record.length > 1) {
        $.ligerDialog.error('请勾选单条操作记录!');
        return;
      }
      var row = record[0];
      if (row.registerAuthentication == 2) {
        showVehicleTeamDetail(row.comId, 'rowApproval');
      }
    };
    /**
     * [onDetail 查看事件]
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
      if (row.registerAuthentication == 1) {
        showVehicleTeamDetail(row.comId, 'rowManual');
      }
    };
    /**
     * [onDetail 查看事件]
     * @return {[type]} [description]
     */
    var onDetail = function() {
      var record = grid.getCheckedRows();
      if (record.length <= 0) {
        $.ligerDialog.error('请勾选单条操作记录!');
        return;
      } else if (record.length > 1) {
        $.ligerDialog.error('请勾选单条操作记录!');
        return;
      }
      var row = record[0];
      if (row.registerAuthentication != 5) {
        showVehicleTeamDetail(row.comId, 'rowDetail');
      }
    };
    /**
     * [inisertVehicleTeam 新增离线授权]
     * @return {[type]} [description]
     */
    var inisertVehicleTeam = function() {
      var defaultData = {},
        doTtmpl = doT.template(vehicleTeamModifyTmpl),
        content = doTtmpl(defaultData);
      var param = {
        title: "新增离线授权",
        ico: 'ico226',
        width: 850,
        height: 580,
        content: content,
        onLoad: function(w, d, g) {
          initDownAuthUpdate('c', w, d, g); // 填充新增弹窗内容
          initAddApp(w);
        }
      };
      CTFO.utilFuns.tipWindow(param);
    };
    /**
     * [initVehicleDetailUpdate 绑定新增事件]
     * @param  {[String]} t [类别，c:新增，u:更新]
     * @param  {[Object]} w [弹窗Dom对象]
     * @param  {[Object]} d [数据对象]
     * @param  {[Object]} g [弹窗对象]
     * @return {[type]}   [description]
     */
    var initDownAuthUpdate = function(t, w, d, g) {

      initApproverSelectList($(w).find("select[name='approver']"));

      $.ajax({
        dataType: 'json',
        url: CTFO.config.sources.getSessionIP,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          var authIP = data.authIP;
          $(w).find('input[name=registIp]').val(authIP);
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });

      $(w).find('input[name=machineSerial]').bind("blur", function(event) {
        var machineSerial = $(w).find('input[name=machineSerial]').val();
        $.ajax({
          dataType: 'json',
          url: CTFO.config.sources.buildAuthentication + "?machineSerial=" + machineSerial,
          complete: function(xhr, textStatus) {
            //called when complete
          },
          success: function(data, textStatus, xhr) {
            if (data.result) {
              var result = data.result;
              var array = result.split(":");
              $(w).find('input[name=comAccount]').val(array[0]);
              $(w).find('input[name=comPassWord]').val(array[1]);
              $(w).find('input[name=authCode]').val(array[2]);
              $(w).find('input[name=authorizationCode]').val(array[3]);
            }
          },
          error: function(xhr, textStatus, errorThrown) {
            //called when there is an error
          }
        });
      });
      $(w).find('input[name=validDate]').click(function(event) {
        WdatePicker({
          dateFmt: 'yyyy-MM-dd HH:mm:ss',
          isShowClear: false
        });
      });
      $(w).find('input[name=registTime]').click(function(event) {
        WdatePicker({
          dateFmt: 'yyyy-MM-dd HH:mm:ss',
          isShowClear: false
        });
      });
      $(w).find('input[name=validDate]').val(CTFO.utilFuns.dateFuns.dateFormat(new Date(), 'yyyy-MM-dd hh:mm:ss'));
      $(w).find('input[name=registTime]').val(CTFO.utilFuns.dateFuns.dateFormat(new Date(), 'yyyy-MM-dd hh:mm:ss'));
      /**
       * 默认加载初始化数据
       */
      initDownAuthDetailUpdate(w, d || {});
      var actionUrl = CTFO.config.sources.insertDownAuthDetail;
      $(w).find('.saveButton').click(function(event) {
          var formContainer = $(w).find('form[name=addAppForm]');
          //表单前端验证
          var validate = formContainer.validate({
            debug: false,
            errorClass: 'myselfError',
            messages: {},
            success: function() {}
          });
          if (!validate.form()) {
            return false;
          }
          var obj = this
          if ($(obj).attr("disabled")) {
            return false;
          }
          $(obj).attr("disabled", true);
          if (!validateEntName(t, w)) {
            $(obj).attr("disabled", false);
            return false;
          }
          var d = formContainer.serializeArray();
          var param = {};
          var addApp;
          var addAppPlus = '';
          $(d).each(function(event) {
            if (this.name.indexOf("a_") >= 0) {

            } else if (this.name == "rp") {

            } else if (this.name.indexOf("addAppRemark_") >= 0) {

            } else if (this.name.indexOf("addValidDate") >= 0) {

            } else {
              param[this.name] = this.value;
              if (this.name == "registTime") {
                param[this.name] = CTFO.utilFuns.dateFuns.date2utc(this.value);
              }
              if (this.name == "validDate") {
                param[this.name] = CTFO.utilFuns.dateFuns.date2utc(this.value);
              }
            }
          });
          var isValidDate = true;
          for (var i = 0; i < gridDeail.getData().length; i++) {
            var bizName = (gridDeail.getData()[i]["bizName"]);
            var processingStatus = $(w).find("input[name=a_" + (gridDeail.getData()[i].autoId) + "]:checked").val();
            var addAppRemark = $(w).find("input[name=addAppRemark_" + (gridDeail.getData()[i].autoId) + "]").val();
            var addValidDate = $(w).find("input[name='addValidDate']").eq(i).val();
            addAppPlus += bizName + "," + processingStatus + "," + addValidDate + "," + addAppRemark + ";";
          }
          param["comAccount"] = $(w).find("input[name=comAccount]").val();
          param["comPassWord"] = $(w).find("input[name=comPassWord]").val();
          param["authCode"] = $(w).find("input[name=authCode]").val();
          param["registIp"] = $(w).find("input[name=registIp]").val();
          param["authorizationCode"] = $(w).find("input[name=authorizationCode]").val();
          param["addApp"] = addAppPlus;

          saveDownAuthDetailFrom(actionUrl, param, function() {
            $(obj).attr("disabled", false);
            g.close();
            authManageForm.find('.queryButton').trigger('click');
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
    var initDownAuthDetailUpdate = function(w, d) {

      var provinceOption = $(w).find('select[name=province]'),
        cityOption = $(w).find('select[name=city]'),
        countyOption = $(w).find('select[name=county]');

      CTFO.utilFuns.codeManager.initProvAndCityAndCounty(provinceOption, cityOption, countyOption);

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
      authManageForm.find('.parentCorpDesc').text(d[columns.text]).end()
        .find('input[name=parentId]').val(d[columns.id]).end()
        .find('input[name=grandpaId]').val(d["parentId"]);
    };
    /**
     * [initForm 初始化查询条件form]
     * @return {[type]} [description]
     */
    var initForm = function() {
      //初始化时，需要将当前用户的企业信息添加到所属企业和parentId字段，用于查询
      $(authManageForm)
        .find('input[name=registTimeStart]').click(function(event) {
          WdatePicker({
            dateFmt: 'yyyy-MM-dd',
            isShowClear: false
          });
        }).end()
        .find('input[name=registEndTimeEnd]').click(function(event) {
          WdatePicker({
            dateFmt: 'yyyy-MM-dd',
            isShowClear: false
          });
        }).end()
        .find('input[name=validTimeStart]').click(function(event) {
          WdatePicker({
            dateFmt: 'yyyy-MM-dd',
            isShowClear: false
          });
        }).end()
        .find('input[name=validEndTimeEnd]').click(function(event) {
          WdatePicker({
            dateFmt: 'yyyy-MM-dd',
            isShowClear: false
          });
        }).end()
        .find('.queryButton').click(function(event) {
          searchGrid();
        }).end()
        .find('.operaterLogButton').click(function(event) {
          CTFO.cache.frame.changeModel('operaterLogManage', '', null, 0);
        }).end()
        .find('.exportGrid').click(function(event) {
          CTFO.utilFuns.commonFuns.exportGrid({
            grid: grid,
            url: CTFO.config.sources.exportAuthManageExcelData
          });
        });

      var provinceOption = authManageForm.find('select[name=corpProvince]'),
        cityOption = authManageForm.find('select[name=corpCity]'),
        countyOption = authManageForm.find('select[name=corpCounty]');

      CTFO.utilFuns.codeManager.initProvAndCityAndCounty(provinceOption, cityOption, countyOption);

    };
    /**
     * [initGrid 初始化grid对象]
     * @return {[type]} [description]
     */
    var initGrid = function() {
      grid = gridContainer.ligerGrid(gridOptions);
    };
    /**
     * [initGridDeail 初始化grid对象]
     * @return {[type]} [description]
     */
    var initAddApp = function(w) {
      gridAddApp = $(w).find(".gridAddApp");
      gridDetailAddApp.height = 150;
      gridDetailAddApp.url = CTFO.config.sources.getAddApp;
      gridDeail = gridAddApp.ligerGrid(gridDetailAddApp);
    };
    var initGridDeail = function(w, comId) {
      gridContainerDeail = $(w).find(".gridContainerDeail");
      gridDeailOptions.height = 190;
      gridDeailOptions.url = CTFO.config.sources.authAddapp + "?comId=" + comId;
      gridDeail = gridContainerDeail.ligerGrid(gridDeailOptions);
    };
    var initGridManage = function(w, comId) {
      gridContainerDeailManage = $(w).find(".gridContainerDeailManage");
      gridDeailManageOptions.height = 150;
      gridDeailManageOptions.url = CTFO.config.sources.authAddapp + "?comId=" + comId;
      gridDeail = gridContainerDeailManage.ligerGrid(gridDeailManageOptions);
    };
    var initGridApproval = function(w, comId) {
      gridContainerDeail = $(w).find(".gridContainerDeail");
      gridDeailManageOptions.url = CTFO.config.sources.authAddapp + "?comId=" + comId;
      gridDeailManageOptions.height = 140;
      gridDeail = gridContainerDeail.ligerGrid(gridDeailManageOptions);
    };


    /**
     * [initApproverSelectList 初始化审批人 select]
     *
     *
     * @param container
     *            审批人容器
     *
     * @param defaultVal
     *            默认参数
     * @return {[type]} [description]
     */
    var initApproverSelectList = function(container, defaultVal) {

      CTFO.utilFuns.commonFuns.getSelectList(approverData, container, defaultVal);
    };
    /**
     * [initApproverData 初始化审批人数据]
     *
     *
     * @param container
     *            审批人容器
     *
     * @param defaultVal
     *            默认参数
     * @return {[type]} [description]
     */
    var initApproverData = function() {

      $.ajax({
        url: CTFO.config.sources.queryOperatorList,
        dataType: 'json',
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          approverData = _.map(data, function(item) {
            return {
              "code": item.opId,
              "name": item.opName
            };
          });
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
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
        data = authManageForm.serializeArray();
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

      // 重新授权
      if ($.inArray(reAppalyRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.reAppalyButton').remove();
      }

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

      // 审批
      if ($.inArray(applyRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.approvalButton').remove();
      }

      // 查看
      if ($.inArray(detailRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.detailButton').remove();
      }

      // 增加
      if ($.inArray(addRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.addVehicleTeam').remove();
      }
      // 导出
      if ($.inArray(exportRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.exportGrid').remove();
      }
      // 操作日志
      if ($.inArray(opLogRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.operaterLogButton').remove();
      }

    };

    return {
      init: function(options) {
        p = $.extend({}, p || {}, options || {});
        pageLocation = p.mainContainer.find('.pageLocation');
        treeContainer = p.mainContainer.find('.leftTreeContainer');
        vehicleTeamTerm = p.mainContainer.find('.authManageTerm');
        authManageForm = p.mainContainer.find('form[name=authManageForm]');
        addAppForm = p.mainContainer.find('form[name=addAppForm]');
        gridContainer = p.mainContainer.find('.gridContainer');
        gridContainerWrap = p.mainContainer.find('.gridContainerWrap');

        vehicleTeamDetailTmpl = $('#auth_detail_tmpl').html();
        vehicleTeamManualTmpl = $('#auth_manual_tmpl').html();
        vehicleTeamApprovalTmpl = $('#auth_approval_tmpl').html();
        vehicleTeamModifyTmpl = $('#auth_down_auth_tmpl').html();

        initApproverData();
        resize(p.cHeight);
        bindEvent();
        initTreeContainer();
        initForm();
        initGrid();
        initAuth(authManageForm);


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