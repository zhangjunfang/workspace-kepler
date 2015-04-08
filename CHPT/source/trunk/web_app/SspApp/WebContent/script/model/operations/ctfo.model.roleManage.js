//系统角色管理
CTFO.Model.roleManage = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var cHeight = 0,
      pageSize = 50,
      pageSizeOption = [10, 20, 30, 40, 50, 100],
      TreeContainer = null, //
      leftTree = null,
      gridHeight = 300, // 表格展示区高度
      addFlag = true,
      grid = null,
      isPassByValueMode = CTFO.config.globalObject.isPassByValueMode, //传值方式
      minH = 520; // 本模块最低高度

    var roleManageTerm = null, //查询条件盒子
      editRoleManage = null, //添加表单
      roleManageform = null, //查询form
      gridListBox = null,
      roleManageBox = null, //grid表格展现盒子

      roleManageGridContent = null, //查询条件以及表格容器
      roleManageFormContent = null,

      updateRowAuth = 'FG_MEMU_SYSTEM_ROLEMANAGE_U', // 修改记录权限
      detailRowAuth = 'FG_MEMU_SYSTEM_ROLEMANAGE_I', // 查看记录详情权限
      deleteRowAuth = 'FG_MEMU_SYSTEM_ROLEMANAGE_D', // 删除记录权限
      addRowAuth = 'FG_MEMU_SYSTEM_ROLEMANAGE_C', // 新增记录权限
      exportRowAuth = 'FG_MEMU_SYSTEM_ROLEMANAGE_E', // 导出记录权限
      opLogRowAuth = 'FG_MEMU_SYSTEM_ROLEMANAGE_O', // 操作记录
      startRowAuth = 'FG_MEMU_SYSTEM_ROLEMANAGE_S', // 启用/吊销
      //
      limitsTree = null; //权限树


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
        $(container).find('.roleManageAddBtn').remove();
      }
      // 导出记录权限
      if ($.inArray(exportRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.exportGrid').remove();
      }
      // 操作记录
      if ($.inArray(opLogRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.operaterLogButton').remove();
      }
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
        return item.roleId
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
        return item.roleId
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
          return item.roleId
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
      onModifyRow(row.roleId);
    }

    var gridcolumns = [{
      display: '角色编码',
      name: 'roleCode',
      width: 100,
      sortable: true,
      align: 'center',
      toggle: false
    }, {
      display: '角色名称',
      name: 'roleName',
      width: 100,
      sortable: true,
      align: 'center',
      toggle: false
    }, {
      display: '创建人',
      name: 'createBy',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '创建时间',
      name: 'createTime',
      width: 200,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.dateFuns.utc2date(row.createTime);
      }
    }, {
      display: '状态',
      name: 'roleStatus',
      width: 50,
      sortable: true,
      align: 'center',
      render: function(row) {
        if (row.roleStatus == 1)
          return '启用';
        if (row.roleStatus == 0)
          return '停用';
      }
    }, {
      display: '操作',
      name: 'entState',
      width: 200,
      sortable: true,
      align: 'center',
      render: function(row) {
        var edit = '';
        var remove = '';
        var view = '';
        var revoke = '';
        if (row.roleCode != "10000") {
          if ($.inArray(updateRowAuth, CTFO.cache.auth) && row.roleType != 2) {
            edit = '<span class="ml10 hand cBlue" title="修改" name="updateSpOperator"  roleId="' + row.roleId + '">修改</span>';
          }
          if ($.inArray(deleteRowAuth, CTFO.cache.auth) && row.roleType != 2) {
            remove = '<span class="ml10 hand cBlue" title="删除" name="removeOperatorById"  roleId="' + row.roleId + '">删除</span>';
          }
          if ($.inArray(detailRowAuth, CTFO.cache.auth)) {
            view = '<span class="ml10 hand cBlue" title="查看" name="spRoleInfoById"  roleId="' + row.roleId + '">查看</span>';
          }
          if ($.inArray(startRowAuth, CTFO.cache.auth) && row.roleType != 2) {
            if (row.roleStatus == 0) {
              revoke = '<span class="ml10 hand cBlue" title="启用" name="revokeEditOpen"  roleId="' + row.roleId + '">启用</span>';
            }
            if (row.roleStatus == 1) {
              revoke = '<span class="ml10 hand cBlue" title="停用" name="revokeEdit"  roleId="' + row.roleId + '">停用</span>';
            }
          }
        }

        return edit + view + remove + revoke;
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
      url: CTFO.config.sources.findSpRoleForList,
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


    /**
     * [onModifyRow 绑定grid行事件]
     */
    var onModifyRow = function(roleId) {

      addFlag = false;
      $.ajax({
        url: CTFO.config.sources.findSpRoletById,
        type: 'POST',
        dataType: 'json',
        data: {
          'roleId': roleId
        },
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          resetThis();
          compileFormData(data);
          initTree(data, roleId);
          roleManageFormContent.removeClass('none');
          roleManageGridContent.addClass('none');
          //锁定用户姓名不可更改
          $(editRoleManage).find('input[name="sysSpRole.roleName"]').attr('disabled', 'true');
          $(editRoleManage).find('input[name="sysSpRole.roleId"]').val(roleId);

          //日志统计
          cLog.addOperatorLog($.extend({}, CTFO.cache.cLogInfo, {
            opType: '角色信息修改', // (必填)
            logTypeId: 'SYSOPERATE', // 目前只有登录(USERLOGIN)，登出(USERLOGOUT),进入模块(ENTERMODULE)，系统操作(SYSOPERATE),操作都归为系统操作(必填)
            logClass: 'CTFO.Model.roleManage', //类名称
            logMethod: 'compileFormData', // 执行方法
            executeTime: '', // 调用方法执行时间毫秒
            logDesc: '' // 操作成功/操作失败
          }));

        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };
    /**
     * [onDeleteRow 绑定grid行事件]
     */
    var onDeleteRow = function(roleId) {
      $.ligerDialog.confirm('真的要执行删除', '信息提示', function(yes) {
        if (yes) {
          $.ajax({
            url: CTFO.config.sources.removeRole,
            type: 'POST',
            dataType: 'json',
            data: {
              'roleId': roleId
            },
            complete: function(xhr, textStatus) {
              //called when complete
            },
            success: function(data, textStatus, xhr) {
              if (data.displayMessage === "success") {
                $.ligerDialog.success("操作成功！", "信息提示");
              } else {
                $.ligerDialog.success(data.opInfo, "信息提示");
              }
              roleManageGrid.loadData(true);
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
    var revokeEditStop = function(roleId) {
        var roleStatus = 0;
        $.ligerDialog.confirm('真的要执行停用', '信息提示', function(yes) {
          if (yes) {
            $.ajax({
              url: CTFO.config.sources.revokeEditOpen,
              type: 'POST',
              dataType: 'json',
              data: {
                'roleId': roleId,
                'roleStatus': roleStatus
              },
              complete: function(xhr, textStatus) {
                //called when complete
              },
              success: function(data, textStatus, xhr) {
                if (data.displayMessage === "success") {
                  $.ligerDialog.success("操作成功！", "信息提示");
                } else {
                  $.ligerDialog.success(data.opInfo, "信息提示");
                }
                roleManageGrid.loadData(true);
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
    var revokeEditStart = function(roleId) {
      var roleStatus = 1;
      $.ligerDialog.confirm('真的要执行启用', '信息提示', function(yes) {
        if (yes) {
          $.ajax({
            url: CTFO.config.sources.revokeEditOpen,
            type: 'POST',
            dataType: 'json',
            data: {
              'roleId': roleId,
              'roleStatus': roleStatus
            },
            complete: function(xhr, textStatus) {
              //called when complete
            },
            success: function(data, textStatus, xhr) {
              if (data.displayMessage === "success") {
                $.ligerDialog.success("操作成功！", "信息提示");
              } else {
                $.ligerDialog.success(data.opInfo, "信息提示");
              }
              roleManageGrid.loadData(true);
            },
            error: function(xhr, textStatus, errorThrown) {
              //called when there is an error
            }
          });
        }
      });
    }

    /**
     * grid操作按钮
     */
    var bindRowAction = function(eDom, rowData) {
      var actionType = $(eDom).attr('name');
      var roleId = $(eDom).attr('roleId');
      switch (actionType) {
        case 'updateSpOperator': //修改
          onModifyRow(roleId);
          break;
        case 'removeOperatorById':
          onDeleteRow(roleId);
          break;
        case 'revokeEditOpen':
          revokeEditStart(roleId);
          break;
        case 'revokeEdit':
          revokeEditStop(roleId);
          break;
        case 'spRoleInfoById':
          var roleId = roleId,
            userRoleId = CTFO.cache.user.roleId;
          if (!!roleId) {
            var param = {
              title: "查看权限信息",
              icon: 'ico227',
              width: 500,
              height: 435,
              data: {
                roleId: roleId,
                userRoleId: userRoleId
              },
              url: CTFO.config.template.roleManageInfo,
              onLoad: function(w, d) {
                $.ajax({
                  url: CTFO.config.sources.findSpRoletDetailInfoById,
                  type: 'POST',
                  dataType: 'json',
                  async: false,
                  data: {
                    'roleId': d.roleId,
                    'userRoleId': d.userRoleId
                  },
                  complete: function(xhr, textStatus) {
                    //called when complete
                  },
                  success: function(data, textStatus, xhr) {
                    var infodetail = data[0];
                    if (infodetail.error) {
                      $.ligerDialog.warn("获取详情失败：" + infodetail.error[0].errorMessage);
                    } else {
                      $(w).find('font.entIdName').text(infodetail.entName); //所属
                      $(w).find('p[name="sysSpRole.roleDesc"]').text(infodetail.roleDesc); //备注
                      $(w).find('div[name="sysSpRole.roleName"]').text(infodetail.roleName); //名称
                      var treeMap1 = infodetail.roleFunTree[0].childrenList; //修改增加旅游权限
                      //CS客户端权限：将CS客户端权限取出，放到单独的权限树下
                      var c = [];
                      for (var k = 0; k < treeMap1.length; k++) {
                        if (treeMap1[k].nodeId.indexOf("CS_MEMU") >= 0) {
                          c.push(treeMap1[k]);
                          treeMap1.splice(k, 1);
                          k--;
                        }
                      }
                      var a = intiTreeDataText(treeMap1);
                      //CS客户端权限树
                      var c1 = intiTreeDataText1(c);
                      a.push(c1[0]);
                      newTree = $(w).find('div.limitsTree').html('').ligerTree({
                        data: a,
                        width: '100%',
                        checkbox: false,
                        childrenName: 'childrenList',
                        height: '100%'
                      }); // json tree属性名
                    }
                  },
                  error: function(xhr, textStatus, errorThrown) {
                    //called when there is an error
                  }
                });

              }
            };
            CTFO.utilFuns.tipWindow(param);
          };

          break;
      };
    };


    /**
     * 初始化赋值操作
     */
    var compileFormData = function(r) {
      var beanName = 'sysSpRole.';
      var d = {};
      for (var n in r) {
        var key = beanName + n;
        d[key] = r[n];
      }
      $(editRoleManage).find('input[name="roleCode"]').val(r.roleCode);
      $(editRoleManage).find('input[type=text]').each(function() {
        var key = $(this).attr('name');
        if (key && d[key])
          $(this).val(d[key]);
      }).end().find('textarea').each(function() {
        var key = $(this).attr('name');
        $(this).val(d[key]);
      }).end();

      $(editRoleManage).find('font.entName').text(r.entName);
    };

    /**
     * [initTree 初始化权限树查询结果]
     *  修改增加旅游权限
     */
    var intiTreeData = function(roleId) {
      var getURL = addFlag ? CTFO.config.sources.findSysFunForTree : CTFO.config.sources.findSysFunForTreeByParam;
      var getData = addFlag ? {
        'funCbs': '1,6',
        'userRoleId': CTFO.cache.user.roleId
      } : {
        'funCbs': '1,6',
        'roleId': roleId,
        'userRoleId': CTFO.cache.user.roleId
      };
      var d = "";
      $.ajax({
        url: getURL,
        type: 'POST',
        dataType: 'json',
        data: getData,
        async: false,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          d = intiTreeDataText(data);
          return d;
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
      return d;
    };
    /**
     * [initTree 初始化权限树查询结果]
     *  修改增加旅游权限
     */
    var intiTreeData1 = function(roleId) {
      var getURL = addFlag ? CTFO.config.sources.findSysFunForTree : CTFO.config.sources.findSysFunForTreeByParam;
      var getData = addFlag ? {
        'funCbs': '2',
        'userRoleId': CTFO.cache.user.roleId
      } : {
        'funCbs': '2',
        'roleId': roleId,
        'userRoleId': CTFO.cache.user.roleId
      };
      var d = "";
      $.ajax({
        url: getURL,
        type: 'POST',
        dataType: 'json',
        data: getData,
        async: false,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          d = intiTreeDataText1(data);
          return d;
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
      return d;
    };
    //修改增加轨迹管理系统权限
    var intiTreeDataText = function(data1) {
      var d = [{
        text: '企业运营系统权限',
        isexpand: 'true',
        entType: '1',
        id: '1',
        childrenList: data1
      }];
      return d;
    };

    //修改增加轨迹管理系统权限
    var intiTreeDataText1 = function(data1) {
      var d = [{
        text: 'CS客户端权限',
        isexpand: 'true',
        entType: '1',
        id: '2',
        childrenList: data1
      }];
      return d;
    };

    var initTree = function(data, roleId) {
      var a = intiTreeData(roleId);
      //var b =intiTreeData1(roleId);
      // a.push(b[0]);
      var options = {
        data: a,
        childrenName: 'childrenList',
        width: '100%',
        height: '100%'
      };
      tree = limitsTree.html('').ligerTree(options)

    };


    /**
     * @description 清空表单
     */
    var resetThis = function() {
      $(roleManageFormContent).find('input[type=text]').each(function() {
        $(this).val("");
      }).end().find('textarea').each(function() {
        $(this).val("");
      }).end().find('input[name="sysSpRole.roleId"],input[name="sysSpRole.functionId"]').each(function() {
        $(this).val("");
      }).end();
      $(roleManageFormContent).find('label[class="error"]').each(function() {
        $(this).remove();
      });
      $(roleManageFormContent).find('.error').removeClass('error');
    };


    /**
     * 添加取消按钮
     */
    var pushON = function(container) {

      $(container).find('span[name=saveForm]').click(function(event) {
        saveFormFun(container);
      });
      // 取消按钮
      container.find('span[name="cancelSave"]').click(function(event) {
        roleManageGridContent.removeClass('none');
        roleManageFormContent.addClass('none');
        resetThis();
        addFlag = true;
      });
    };
    /**
     *添加提交按钮
     */
    var saveFormFun = function(container) {
      var notes = limitsTree.ligerGetTreeManager().getChecked();
      var notesA = limitsTree.ligerGetTreeManager().getIncomplete();
      var seletedNodeVal = [];
      for (var i = 0; i < notes.length; i++) {
        seletedNodeVal.push(notes[i].data.nodeId);
      }
      for (var i = 0; i < notesA.length; i++) {
        seletedNodeVal.push(notesA[i].data.nodeId);
      }
      $(editRoleManage).find('input[name="sysSpRole.functionId"]').val(seletedNodeVal); //赋值给隐藏域
      //表单前端验证
      var validate = $(container).validate({
        debug: false,
        errorClass: 'myselfError210',
        messages: {},
        success: function() {}
      });
      if (!validate.form()) {
        return false;
      };
      var seletedNodeVal = $(editRoleManage).find('input[name="sysSpRole.functionId"]').val();
      if (seletedNodeVal === "") {
        $.ligerDialog.warn("请选择权限！", "提示");
        return;
      }
      //验证登录名是否在企业中唯一
      if (addFlag && !userExist(container)) return false;

      //根据是否选中缺省角色，给input框赋值
      var roleStatus = $(editRoleManage).find('input[name="sysSpRole.roleStatus"]').prop("checked") == true ? true : false;
      if (roleStatus == true) {
        $(editRoleManage).find('input[name="sysSpRole.roleStatus"]').val('1');
      } else {
        $(editRoleManage).find('input[name="sysSpRole.roleStatus"]').val('0');
      }

      var parms = [];
      var d = $(container).serializeArray();
      $(d).each(function() {
        parms.push({
          name: this.name,
          value: $.trim(this.value)
        });
      });

      disabledButton(true); // 控制按钮

      var newParms = {};
      newParms["roleCode"] = $("input[name=roleCode]").attr('value');
      $.each(parms, function(i, n) {
        var name = n.name;
        if (name.indexOf(".") > -1) {
          newParms[name.split(".")[1]] = n.value;
        } else {
          newParms[name] = n.value;
        }
      });
      $.ajax({
        url: addFlag ? CTFO.config.sources.addSpRole : CTFO.config.sources.modifySpRolet,
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
          //处理结果
          if (r && r.displayMessage == "success") {
            $.ligerDialog.success(text + "成功", '提示', function(y) {
              if (y) {
                roleManageGridContent.removeClass('none');
                roleManageFormContent.addClass('none');
                roleManageGrid.loadData(true);
                resetThis();
              };
              addFlag = true;
            });
          } else {
            $.ligerDialog.error(text + "失败");
          }
        }
      });
    };



    /**
     * @description 处理按钮
     * @param boolean
     */
    var disabledButton = function(boolean) {
      if (boolean) {
        editRoleManage.find('span[name="saveForm"]').attr('disabled', true);
      } else {
        editRoleManage.find('span[name="saveForm"]').attr('disabled', false);
      }
    };

    /**
     * 用户是否存在验证
     */
    var userExist = function(container) {
      var roleName = $(container).find('input[name="sysSpRole.roleName"]').val().toLowerCase();
      var updateId = $(container).find('input[name="sysSpRole.entId"]').val();

      var flag = false;
      $.ajax({
        url: CTFO.config.sources.isExistSysRole,
        type: 'POST',
        async: false,
        dataType: 'json',
        data: {
          "requestParam.equal.roleName": roleName,
          "requestParam.noId": updateId
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
     * 装载grid列表
     */
    var initgrid = function() {
      roleManageGrid = grid = roleManageBox.ligerGrid(roleManageGridInit);
    };

    /**
     * [initRoleList 点击左侧树 动态加载该组织下的角色列表]
     * @return {[type]}
     */
    var initRoleList = function(node) {
      if (!addFlag) {
        $.ligerDialog.error("修改时请勿改变组织！");
        return false;
      };
      $(roleManageform).find('.parentCorpDesc').text(node.text);
      $(roleManageform).find('input[name=entIds]').val(node.id);
      $(editRoleManage).find('.entName').text(node.text);
      $(editRoleManage).find('input[name="sysSpRole.entId"]').val(node.id);
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
     * form表单提交
     */
    var initForm = function() {
      //初始化当前公司

      $(roleManageform).find('.parentCorpDesc').text(CTFO.cache.user.entName);
      $(roleManageform).find('input[name=entIds]').val(CTFO.cache.user.entId);
      $(editRoleManage).find('.entName').text(CTFO.cache.user.entName);
      $(editRoleManage).find('input[name="sysSpRole.entId"]').val(CTFO.cache.user.entId);
      $(roleManageform).find('input[name=createTimeStart]').click(function(event) {
        WdatePicker({
          dateFmt: 'yyyy-MM-dd',
          isShowClear: false
        });
      });
      $(roleManageform).find('input[name=createTimeEnd]').click(function(event) {
        WdatePicker({
          dateFmt: 'yyyy-MM-dd',
          isShowClear: false
        });
      });
      $(roleManageform).find('.searchGrid').click(function(event) {
        searchGrid();
      });

      //新增
      $(roleManageTerm)
        .find('.roleManageAddBtn').click(function(event) {
          onAdd();
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
            url: CTFO.config.sources.exportRoleManageExcelData
          });
        }).end()
        .find('.resetButton').click(function(event) {
          resetThisForm();
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
        data = $(roleManageform).serializeArray();
      $(data).each(function(event) {
        var name = '';
        if (this.name === 'roleName') name = 'requestParam.like.' + this.name;
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
     * @description 清空表单
     */
    var resetThisForm = function() {
      $(roleManageTerm).find('input[type="text"]').each(function() {
        $(this).val("");
      }).end().find('select').each(function() {
        $(this).val("");
      }).end().find('textarea').each(function() {
        $(this).val("");
      }).end();

    };

    var onAdd = function() {

      resetThis();
      addFlag = true;
      initTree();
      if (roleManageFormContent.hasClass('none')) {
        roleManageFormContent.removeClass('none');
        roleManageGridContent.addClass('none');
        //                        roleManageFormContent.find("li:gt(3)").addClass('none');
      };
      $.ajax({
        url: CTFO.config.sources.queryAutoCodeOfRole,
        dataType: 'json',
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          var code = $(editRoleManage).find('input[name=roleCode]').attr("value", data)
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
      //初始用户姓名输入框
      $(editRoleManage).find('input[name="sysSpRole.roleName"]').removeAttr('disabled')

    }

    var resize = function(ch) {
      if (ch < minH) ch = minH;
      p.mainContainer.height(ch);
      gridListBox.height(p.mainContainer.height() - pageLocation.outerHeight() - roleManageTerm.outerHeight() - parseInt(gridListBox.css('margin-top')) * 3 - parseInt(gridListBox.css('border-top-width')) * 2)

      gridHeight = gridListBox.height();
      if (roleManageGrid) roleManageGrid = roleManageBox.ligerGrid({
        height: gridHeight
      });

      TreeContainer.height(ch);
      if (leftTree) leftTree.resize();

    };

    return {
      init: function(options) {
        p = $.extend({}, p || {}, options || {});

        pageLocation = p.mainContainer.find('.pageLocation'); //当前位置
        roleManageTerm = p.mainContainer.find('.roleManageTerm'); //查询条件盒子
        roleManageform = p.mainContainer.find('form[name=roleManageform]'); //查询form

        gridListBox = p.mainContainer.find('.gridListBox');
        roleManageBox = p.mainContainer.find('.roleManageBox'); //grid表格展现盒子
        TreeContainer = p.mainContainer.find('.TreeContainer'); //左侧组织树

        roleManageGridContent = p.mainContainer.find('.roleManageContent:eq(0)'); //查询条件以及表格容器
        roleManageFormContent = p.mainContainer.find('.roleManageContent:eq(1)');
        editRoleManage = p.mainContainer.find('form[name="editRoleManage"]'); //添加表单
        limitsTree = p.mainContainer.find('.limitsTree'); //权限树

        initAuth(roleManageTerm);
        pushON(editRoleManage);
        initTreeContainer();
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