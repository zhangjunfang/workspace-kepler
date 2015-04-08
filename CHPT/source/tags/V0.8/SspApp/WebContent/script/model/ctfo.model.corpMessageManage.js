/**
 * [ 系统管理 - 公告管理]                                                                                        resize(ch [description]
 * @return {[type]}         [description]
 */
CTFO.Model.CorpMessageManage = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var cHeight = 0,
      minH = 520, // 本模块最低高度
      gridHeight = 300, // grid高度
      pageSize = 50,
      pageSizeOption = [10, 20, 30, 40, 50, 100],

      treeContainer = null,
      announceManageForm = null,
      vehicleTeamTerm = null,
      gridContainer = null,
      announceManageForm = null,
      leftTree = null,
      grid = null,
      addFlag = null,
      currentEntName = '',
      editor = null,

      vehicleTeamDetailTmpl = null,
      vehicleTeamModifyTmpl = null,
      fileUploadForm = null,
      isRelease = '0',
      /*  0 :保存  1:发布 */


      //修改公告详情
      modify_bulletin_manage_detail = null,
      //审核公告详情
      examine_bulletin_manage_detail = null,
      //新增公告详情
      add_bulletin_manage_detail = null,
      bulletin_manage_detail = null,
      modify_bulletin_attachment_list = null,
      modify_bulletin_attachment_list_tr = null,
      bugTools = ['formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', '|',
        'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist', 'insertunorderedlist', '|',
        'removeformat', 'undo', 'redo', 'fullscreen', 'savetemplate', 'about'
      ],

      updateRowAuth = 'FG_MEMU_SYSTEM_CORPMESSAGEMANAGE_U', // 修改记录权限
      detailRowAuth = 'FG_MEMU_SYSTEM_CORPMESSAGEMANAGE_I', // 查看记录详情权限
      deleteRowAuth = 'FG_MEMU_SYSTEM_CORPMESSAGEMANAGE_D', // 删除记录权限
      addRowAuth = 'FG_MEMU_SYSTEM_CORPMESSAGEMANAGE_C', // 新增记录权限
      exportRowAuth = 'FG_MEMU_SYSTEM_CORPMESSAGEMANAGE_E', // 导出记录权限
      opLogRowAuth = 'FG_MEMU_SYSTEM_CORPMESSAGEMANAGE_O', // 操作记录
      publishRowAuth = 'FG_MEMU_SYSTEM_CORPMESSAGEMANAGE_P', // 发布
      recallRowAuth = 'FG_MEMU_SYSTEM_CORPMESSAGEMANAGE_R', // 撤回
      applayRowAuth = 'FG_MEMU_SYSTEM_CORPMESSAGEMANAGE_A', //审核
      isPassByValueMode = CTFO.config.globalObject.isPassByValueMode, //传值方式

      test = '';
    // grid展现列
    var columns = [{
      display: '标题',
      name: 'annoucSubject',
      width: 300,
      sortable: true,
      align: 'left',
      toggle: false,
      render: function(row) {
        return '<div class="pl5">' + row.annoucSubject + '</div>';
      }
    }, {
      display: '接收方',
      name: 'comName',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '状态',
      name: 'annouceStatus',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        if (row.annouceStatus == 0)
          return "草稿";
        if (row.annouceStatus == 1)
          return "已发布";
        if (row.annouceStatus == 2)
          return "待审核";
        if (row.annouceStatus == 3)
          return "已撤回";
        if (row.annouceStatus == 4)
          return "驳回";
      }
    }, {
      display: '发布时间',
      name: 'releaseDate',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.dateFuns.utc2date(row.releaseDate);
      }
    }, {
      display: '发布部门',
      name: 'annouceDeptName',
      width: 100,
      sortable: true,
      align: 'center',

    }, {
      display: '发布人',
      name: 'annoucePeople',
      width: 100,
      sortable: true,
      align: 'center',

    }, {
      display: '操作',
      name: 'entType',
      width: 120,
      sortable: true,
      align: 'center',
      render: function(row) {
        var buttons = [];

        if ($.inArray(detailRowAuth, CTFO.cache.auth) > -1) {
          var detailHtml = "<span class='cBlue'><font title='查看' class='hand' actionType='rowDetail'>查看</font></span>&nbsp;";
          buttons.push(detailHtml);
        }
        if ($.inArray(updateRowAuth, CTFO.cache.auth) > -1) {
          if (row.annouceStatus == 0 || row.annouceStatus == 3 || row.annouceStatus == 4) {
            var editHtml = "<span class='cBlue'><font title='编辑' class='hand' actionType='modifyRow'>编辑</font></span>&nbsp;";
            buttons.push(editHtml);
          }
        }
        if ($.inArray(publishRowAuth, CTFO.cache.auth) > -1) {
          if (row.annouceStatus == 0 || row.annouceStatus == 3 || row.annouceStatus == 4) {
            var editHtml = "<span class='cBlue'><font title='发布' class='hand' actionType='publishAnnouce'>发布</font></span>&nbsp;";
            buttons.push(editHtml);
          }
        }
        if ($.inArray(deleteRowAuth, CTFO.cache.auth) > -1) {
          if (row.annouceStatus == 0 || row.annouceStatus == 3 || row.annouceStatus == 4) {
            var editHtml = "<span class='cBlue'><font title='删除' class='hand' actionType='deleteAnnouce'>删除</font></span>&nbsp;";
            buttons.push(editHtml);
          }
        }
        if ($.inArray(recallRowAuth, CTFO.cache.auth) > -1) {
          if (row.annouceStatus == 1) {
            var editHtml = "<span class='cBlue'><font title='撤回' class='hand' actionType='withdraw'>撤回</font></span>&nbsp;";
            buttons.push(editHtml);
          }
        }
        if ($.inArray(applayRowAuth, CTFO.cache.auth) > -1) {
          if (row.annouceStatus == 2) {
            var editHtml = "<span class='cBlue'><font title='审核' class='hand' actionType='examine'>审核</font></span>&nbsp;";
            buttons.push(editHtml);
          }
        }
        return buttons.join('');
      }
    }];
    // grid初始化参数
    var gridOptions = {
      columns: columns,
      sortName: 'releaseDate',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      url: CTFO.config.sources.findBulletinList, // 数据请求地址查询
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
          //编辑
          modifyAnnounceManage(rowData.annoucId);
          break;
        case 'deleteAnnouce':
          //删除
          deleteVehicleTeam(rowData.annoucId);
          break;
        case 'rowDetail':
          //查看
          showBulletinManageDetail(rowData.annoucId); //该方法传入公告主键id
          break;
        case 'examine':
          //审核
          examineAnnounceManage(rowData.annoucId);
          break;
        case 'publishAnnouce':
          //发布
          operationNotice(rowData.annoucId, 'f');
          break;
        case 'withdraw':
          //撤回
          operationNotice(rowData.annoucId, 'c');
          break;
      }
      return !actionType;
    };
    /**
     * [deleteVehicleTeam 删除公告记录]
     * @param  {[String]}  orgId  [公告id]
     * @return {[type]} [description]
     */
    var deleteVehicleTeam = function(orgId) {
      $.ligerDialog.confirm('是否删除公告?', function(yes) {
        if (!yes) return false;
        $.ajax({
          url: CTFO.config.sources.deleteBulletinById,
          type: 'POST',
          dataType: 'json',
          data: {
            annoucId: orgId
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
    var operationNotice = function(orgId, t) {
      if (t == 'f') {
        $.ligerDialog.confirm('是否发布公告?', function(yes) {
          if (!yes) return false;
          $.ajax({
            url: CTFO.config.sources.publishAnnouce,
            type: 'POST',
            dataType: 'json',
            data: {
              annoucId: orgId
            },
            complete: function(xhr, textStatus) {
              //called when complete
            },
            success: function(data, textStatus, xhr) {
              if (data && data.displayMessage === "success") {
                grid.loadData(true);
                $.ligerDialog.success("发布操作成功");
              } else {
                $.ligerDialog.success(data.opInfo);
              }
            },
            error: function(xhr, textStatus, errorThrown) {
              $.ligerDialog.error(xhr.displayMessage);
            }
          });
        });
      } else if (t == 'c') {
        $.ligerDialog.confirm('是否撤回公告?', function(yes) {
          if (!yes) return false;
          $.ajax({
            url: CTFO.config.sources.cancelAnnouce,
            type: 'POST',
            dataType: 'json',
            data: {
              annoucId: orgId
            },
            complete: function(xhr, textStatus) {
              //called when complete
            },
            success: function(data, textStatus, xhr) {
              if (data && data.displayMessage === "success") {
                grid.loadData(true);
                $.ligerDialog.success("撤回操作成功");
              } else {
                $.ligerDialog.success(data.opInfo);
              }
            },
            error: function(xhr, textStatus, errorThrown) {
              $.ligerDialog.error(xhr.displayMessage);
            }
          });
        });
      }
    };
    /**
     * [showBulletinManageDetail 显示公告详情]
     * @param  {[String]} annoucId [公告主键id]
     * @return {[type]}       [description]
     */
    var showBulletinManageDetail = function(annoucId) {
      getBulletinManageDetail(annoucId, bulletin_manage_detail, function(content, data) {
        var param = {
          title: "公告详情",
          ico: 'ico226',
          width: 850,
          height: 580,
          content: content,
          data: data,
          onLoad: function(w, d, g) {
            //web页面编辑器
            editor = KindEditor.create($(w).find("textarea")[0], {
              allowFileManager: false,
              readonlyMode: true,
              items: bugTools
            });
            //初始化附件列表
            compileAttachmentList(w, d.list, modify_bulletin_attachment_list, modify_bulletin_attachment_list_tr);
            $(w).find(".emptyItem").addClass('none')
              .end().find(".addAttachmentButton").hide()
              .end().find(".removeAttachmentButton").hide();
          }
        };
        CTFO.utilFuns.tipWindow(param);
      });
    };
    /**
     * [getBulletinManageDetail 获取公告详情]
     * @param  {[String]}   annoucId    [公告主键id]
     * @param  {[Object]}   tmpl     [详情模板对象]
     * @param  {Function} callback [回调函数]
     * @return {[type]}            [description]
     */
    var getBulletinManageDetail = function(annoucId, tmpl, callback) {
      $.get(CTFO.config.sources.findBulletinById, {
        annoucId: annoucId
      }, function(data, textStatus, xhr) {
        var content = '';
        if (typeof(data) === 'string') data = JSON.parse(data);

        data.model.list = data.list;
        if (data && !data.error) content = compileBulletinManageDetail(data.model, tmpl);
        if (callback) callback(content, data);
      });
    };
    /**
     * [compileBulletinManageDetail] 渲染公告详情弹窗]
     * @param  {[Object]} d    [数据对象]
     * @param  {[Object]} tmpl [详情模板对象]
     * @return {[type]}      [description]
     */
    var compileBulletinManageDetail = function(d, tmpl) {
      var doTtmpl = doT.template(tmpl);
      if (d.annouceStatus == 0) {
        d.annouceStatus = '草稿';
      }
      if (d.annouceStatus == 1) {
        d.annouceStatus = '已发布';
      }
      if (d.annouceStatus == 2) {
        d.annouceStatus = '待审核';
      }
      if (d.annouceStatus == 3) {
        d.annouceStatus = '已撤回';
      }
      return doTtmpl(d);
    };

    /**
     * [compileAttachmentList] 渲染附件列表]
     * @param  {[Object]} w     [窗口对象]
     * @param  {[Object]} d     [数据对象]
     * @param  {[Object]} tmpl  [附件模板对象]
     * @param  {[Object]} tmpl2 [附件模板对象]
     * @return {[type]}      [description]
     */
    var compileAttachmentList = function(w, d, tmpl, tmpl2) {

      var doTtmpl = doT.template(tmpl);
      var content = doTtmpl(d);

      $(w).find(".attachment_list").html(content);

      for (var i = d.length || 0; i < 3; i++) {
        $(w).find(".attachment_list").find(".emptyItemBox").append(tmpl2);
        break;
      }

      $(w).find(".removeAttachmentButton").bind('click', function() {
        var node = $(this).parent().parent().parent();
        var annoucId = node.find("input[name='annoucId']").val();
        var attachId = node.find("input[name='attachId']").val();
        var url = CTFO.config.sources.deleteAnnouceFileById + "?attachId=" + attachId + "&annoucId=" + annoucId;
        if (annoucId && attachId) {
          $.ligerDialog.confirm("是否确认删除该附件吗？", function(yes) {
            if (yes) {
              $.ajax({
                url: url,
                complete: function(xhr, textStatus) {
                  //called when complete
                },
                success: function(data, textStatus, xhr) {
                  $(node).remove();
                },
                error: function(xhr, textStatus, errorThrown) {
                  //called when there is an error
                }
              });
            }
          });
        } else {
          $(node).remove();
        }
      })
      $(w).find(".addAttachmentButton").bind('click', function() {
        var count = $(w).find(".attachment_list").find('.item').length;
        if (count < 3) {
          //增加附件
          $(w).find(".attachment_list").find(".emptyItemBox").append(tmpl2);
          $(w).find(".removeAttachmentButton").unbind().bind('click', function() {
            $(this).parent().parent().parent().remove();
          });
        }
      });

    };

    /**
     * [modifyAnnounceManage 修改公告信息]
     * @param  {[String]} annoucId [公告id]
     * @return {[type]}       [description]
     */
    var modifyAnnounceManage = function(annoucId) {

      getBulletinManageDetail(annoucId, modify_bulletin_manage_detail, function(content, data) {
        var param = {
          title: "修改公告信息",
          ico: 'ico226',
          width: 850,
          height: 580,
          content: content,
          onLoad: function(w, d, g) {
            initAnnouceManageUpdate('u', w, d, g); //绑定更新公告
          },
          data: data
        };
        CTFO.utilFuns.tipWindow(param);
      });
    };

    /**
     * [initExamineDialog 修改公告信息]
     * @return {[type]}       [description]
     */
    var initExamineDialog = function(w, d, g) {
      $(w).find('.saveButton').click(function(event) {
        var d = null;
        d = $(w).find('form').serializeArray();
        var param = {};
        $(d).each(function(event) {
          param[this.name] = this.value;
        });

        var url = CTFO.config.sources.examineAnnouce + "?annoucId=" + param.annoucId + "&msgExamineType=" + param.msgExamineType;
        $.ajax({
          url: url,
          dataType: 'json',
          complete: function(xhr, textStatus) {
            //called when complete
          },
          success: function(data, textStatus, xhr) {
            g.close();
            if (data.opInfo == 1) {
              $.ligerDialog.success("审核成功！");
            } else {
              $.ligerDialog.success("更新至待审核状态！");
            }
            announceManageForm.find('.queryButton').trigger('click');
          },
          error: function(xhr, textStatus, errorThrown) {
            //called when there is an error
          }
        });
      }).end().find('.cancelButton').click(function(event) {
        $(w).find('.saveButton').attr("disabled", false);
        g.close();
      });
    };

    /**
     * [examineAnnounceManage 审核公告信息]
     * @param  {[String]}      annoucId [公告id]
     * @return {[type]}       [description]
     */
    var examineAnnounceManage = function(annoucId) {
      var data = {
        "annoucId": annoucId
      };
      var doTtmpl = doT.template(examine_bulletin_manage_detail);
      var content = doTtmpl(data);

      var param = {
        title: "审核意见",
        ico: 'ico226',
        width: 320,
        height: 180,
        content: content,
        onLoad: function(w, d, g) {
          initExamineDialog(w, d, g);
        },
        data: data
      };
      CTFO.utilFuns.tipWindow(param);
    };
    /**
     * [addAnnounceManage 新增公告信息]
     * @return {[type]}       [description]
     */
    var addAnnounceManage = function() {
      var data = {
        "annouceStatus": 0,
        "releaseDate": new Date().getTime()
      };
      var content = compileBulletinManageDetail(data, add_bulletin_manage_detail);
      var param = {
        title: "新增公告信息",
        ico: 'ico226',
        width: 850,
        height: 580,
        content: content,
        onLoad: function(w, d, g) {
          initAnnouceManageUpdate('c', w, d, g); //绑定新增公告
        },
        data: {
          "model": {},
          "list": []
        }
      };
      CTFO.utilFuns.tipWindow(param);
    };
    /**
     * [bindEventAnnounce 绑定公告事件]
     * @return {[type]}       [description]
     */
    var bindEventAnnounce = function(t, w, d, g) {

      var actionUrl = t === 'c' ? CTFO.config.sources.insertBulletinManage : (t === 'u' ? CTFO.config.sources.modifyBulletinById : CTFO.config.sources.deleteBulletinById);
      if (!actionUrl) return false;

      $(w).find('.saveButton').click(function(event) {

        fileUploadForm = $(w).find('form');
        isRelease = fileUploadForm.find('input[name="isRelease"]').val();
        fileUploadForm.find('input[name="comName"]').val(fileUploadForm.find('select[name="comCode"] option:selected').text());
        fileUploadForm.find('input[name="setbookName"]').val(fileUploadForm.find('select[name="setbookId"] option:selected').text());
        fileUploadForm.find('input[name="annouceDeptName"]').val(fileUploadForm.find('select[name="annouceDept"] option:selected').text());
        fileUploadForm.find('input[name="annoucePeople"]').val(fileUploadForm.find('select[name="annoucePeopleId"] option:selected').text());
        //表单前端验证
        var validate = $(fileUploadForm).validate({
          debug: false,
          errorClass: 'myselfError',
          messages: {},
          success: function() {}
        });
        if (!validate.form()) {
          return false;
        }

        var param = {};
        $(w).find('textarea').val(editor.html());
        saveVehicleTeamDetailFrom(actionUrl, param, function(data) {
          g.close();
          announceManageForm.find('.queryButton').trigger('click');
          if (t === 'c') {
            if (isRelease == '1') {
              if (data.opInfo == 1) {
                $.ligerDialog.success("更新至待审核状态！");
              } else {
                $.ligerDialog.success("发布成功！");
              }
            } else {
              $.ligerDialog.success("添加成功！");
            }
          } else if (t === 'u') {
            $.ligerDialog.success("修改成功！");
          } else {
            $.ligerDialog.success("删除成功！");
          }
        });
      }).end().find('.releaseButton').click(function(event) {
        fileUploadForm = $(w).find('form[name=addBulletinManageForm]');
        fileUploadForm.find('input[name="isRelease"]').val("1");
        fileUploadForm.find('.saveButton').trigger('click');
      }).end().find('.cancelButton').click(function(event) {
        $(w).find('.saveButton').attr("disabled", false);
        g.close();
      }).end().find('input[name=releaseDate]').click(function(event) {
        WdatePicker({
          //dateFmt:'yyyy-MM-dd',
          dateFmt: 'yyyy-MM-dd HH:MM:ss',
          isShowClear: false
        });
      });
    };
    /**
     * [initAnnouceManageUpdate 绑定公告新增/更新弹窗事件]
     * @param  {[String]} t [类别，c:新增，u:更新]
     * @param  {[Object]} w [弹窗Dom对象]
     * @param  {[Object]} d [数据对象]
     * @param  {[Object]} g [弹窗对象]
     * @return {[type]}   [description]
     */
    var initAnnouceManageUpdate = function(t, w, d, g) {

      //web页面编辑器
      editor = KindEditor.create($(w).find("textarea")[0], {
        allowFileManager: false,
        items: bugTools
      });

      //初始化 接收方和公司帐套 的下拉列表
      var comOption = $(w).find('select[name=comCode]');
      //初始化 发布部门和发布人 的下拉列表
      var deptOption = $(w).find('select[name=annouceDept]');
      var bookOption = $(w).find('select[name=setbookId]');
      var annouceOption = $(w).find('select[name=annoucePeopleId]');

      initComSelectList(comOption, d.model.comCode);
      //公司联动帐套 和 发布部门
      comOption.change(function() {
        var comId = comOption.val();
        initBookSelectList(comId, bookOption);
        initDeptSelectList(comId, deptOption);
        var deptId = deptOption.val();
        initAnnoucePeopleSelectList(comId, deptId, annouceOption);
      });
      initBookSelectList(d.model.comCode, bookOption, d.model.setbookId);

      initDeptSelectList(d.model.comCode, deptOption, d.model.annouceDept);
      //部门联动发布人
      deptOption.change(function() {
        var comId = comOption.val();
        var deptId = deptOption.val();
        initAnnoucePeopleSelectList(comId, deptId, annouceOption);
      });
      initAnnoucePeopleSelectList(d.model.comCode, d.model.annouceDept, annouceOption, d.model.annoucePeopleId);

      //初始化附件列表
      compileAttachmentList(w, d.list, modify_bulletin_attachment_list, modify_bulletin_attachment_list_tr);

      bindEventAnnounce(t, w, d, g);

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
        submitHandler: function() {}
      });
      return validator;
    };
    /**
     * [saveVehicleTeamDetailFrom 保存公告]
     * @param  {[String]}   url      [请求action]
     * @param  {[Object]}   param    [请求参数]
     * @param  {Function} callback [回调函数]
     * @return {[type]}            [description]
     */
    var saveVehicleTeamDetailFrom = function(url, parms, callback, w) {
      /*var newParms = {};
      $.each(parms,function(name,val){
        if(name.indexOf(".")>-1){
          newParms[name.split(".")[1]]=val;
        }else{
          newParms[name]=val;
        }
      });*/
      var options = { //文件上传参数
        url: url,
        type: "post",
        dataType: 'json',
        resetForm: false,
        contentType: 'application/x-www-form-urlencoded;charset=UTF-8',
        success: function(data) {
          callback(data);
        },
        error: function(data) {
          alert("失败");
          $.ligerDialog.warn("附件上传失败,请重试.");
        }
      };
      $(fileUploadForm).ajaxSubmit(options); //异步提交表单
    };


    /**
     * [releaseVehicleTeamDetailFrom 发布公告]
     * @param  {[String]}   url      [请求action]
     * @param  {[Object]}   param    [请求参数]
     * @param  {Function} callback [回调函数]
     * @return {[type]}            [description]
     */
    var releaseVehicleTeamDetailFrom = function(url, parms, callback, w) {
      var newParms = {};
      $.each(parms, function(name, val) {
        if (name.indexOf(".") > -1) {
          newParms[name.split(".")[1]] = val;
        } else {
          newParms[name] = val;
        }

      });
      var options = { //文件上传参数
        url: url,
        type: "post",
        dataType: 'json',
        resetForm: false,

        contentType: 'application/x-www-form-urlencoded;charset=UTF-8',
        success: function(data) {
          alert("success");
          callback(data);
        },
        error: function(data) {
          alert("失败");
          $.ligerDialog.warn("附件上传失败,请重试.");
        }
      };
      $(fileUploadForm).ajaxSubmit(options); //异步提交表单

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
      cloudBackUpForm.find('.exportGrid').click(function(event) {
        CTFO.utilFuns.commonFuns.exportGrid({
          grid: grid,
          url: CTFO.config.sources.exportExcelDataCorp
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
      //初始化时，需要将当前用户的企业信息添加到所属企业和parentId字段，用于查询
      $(announceManageForm)
        .find('input[name=releaseDateStart]').click(function(event) {
          WdatePicker({
            dateFmt: 'yyyy-MM-dd',
            isShowClear: false
          });
        }).end()
        .find('input[name=releaseDateEnd]').click(function(event) {
          WdatePicker({
            dateFmt: 'yyyy-MM-dd',
            isShowClear: false
          });
        }).end()
        .find('.queryButton').click(function(event) {
          searchGrid();
        }).end()
        .find('.addButton').click(function(event) {
          addAnnounceManage();
        }).end()
        .find('.publishButton').click(function(event) {
          onPublish();
        }).end()
        .find('.removeButton').click(function(event) {
          onRemove();
        }).end()
        .find('.applyButton').click(function(event) {
          onApply();
        }).end()
        .find('.returnButton').click(function(event) {
          onReturn();
        }).end()
        .find('.editButton').click(function(event) {
          onEdit();
        }).end()
        .find('.detailButton').click(function(event) {
          onDetail();
        }).end()
        .find('.operaterLogButton').click(function(event) {
          CTFO.cache.frame.changeModel('operaterLogManage', '', null, 0);
        }).end()
        .find('.exportGrid').click(function(event) {
          CTFO.utilFuns.commonFuns.exportGrid({
            grid: grid,
            url: CTFO.config.sources.exportBulletinListExcelData
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
      $(announceManageForm).find('input[type="text"]').each(function() {
        $(this).val("");
      }).end().find('select').each(function() {
        $(this).val("");
      }).end().find('textarea').each(function() {
        $(this).val("");
      }).end();
      $(announceManageForm).find('select[name=annoucePeopleId]').html('');
    };

    /**
     * [onReturn ]
     */
    var onReturn = function() {
      var record = grid.getCheckedRows();
      if (record.length <= 0) {
        $.ligerDialog.error('请勾选操作记录!');
        return;
      }
      var arr = _.map(record, function(item) {
        if (item.annouceStatus == 1) {
          return item.annoucId
        }
      });
      if (arr.length > 0) {
        operationNotice(arr.join(","), 'c');
      }
    }

    /**
     * [onApply ]
     */
    var onApply = function() {
      var record = grid.getCheckedRows();
      if (record.length <= 0) {
        $.ligerDialog.error('请勾选操作记录!');
        return;
      }
      var arr = _.map(record, function(item) {
        if (item.annouceStatus == 2) {
          return item.annoucId
        }
      });
      if (arr.length > 0) {
        examineAnnounceManage(arr.join(","));
      }
    }

    /**
     * [onRemove ]
     */
    var onRemove = function() {
      var record = grid.getCheckedRows();
      if (record.length <= 0) {
        $.ligerDialog.error('请勾选操作记录!');
        return;
      }
      var arr = _.map(record, function(item) {
        if (item.annouceStatus == 0 || item.annouceStatus == 3 || item.annouceStatus == 4) {
          return item.annoucId
        }
      });
      if (arr.length > 0) {
        deleteVehicleTeam(arr.join(","));
      }
    }

    /**
     * [onEdit ]
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
      if (row.annouceStatus == 0 || row.annouceStatus == 3 || row.annouceStatus == 4) {
        modifyAnnounceManage(row.annoucId);
      }
    };

    /**
     * [onDetail ]
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
      showBulletinManageDetail(row.annoucId);
    };

    /**
     * [onPublish ]
     */
    var onPublish = function() {
        var record = grid.getCheckedRows();
        if (record.length <= 0) {
          $.ligerDialog.error('请勾选单条操作记录!');
          return;
        } else if (record.length > 1) {
          $.ligerDialog.error('请勾选单条操作记录!');
          return;
        }
        var row = record[0];
        if (row.annouceStatus == 0 || row.annouceStatus == 3 || row.annouceStatus == 4) {
          operationNotice(row.annoucId, 'f');
        }
      }
      /**
       * [initComSelectList 初始化公司 select]
       *
       * @param container
       *            公司容器
       *
       * @param defaultVal
       *            默认参数
       * @return {[type]} [description]
       */
    var initComSelectList = function(container, defaultVal) {
      $.ajax({
        url: CTFO.config.sources.queryCompanyList,
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
     * [initBookSelectList 初始化公司帐套 select]
     *
     * @param comId
     *            公司Id
     *
     * @param container
     *            公司容器
     *
     * @param defaultVal
     *            默认参数
     * @return {[type]} [description]
     */
    var initBookSelectList = function(comId, container, defaultVal) {
      $.ajax({
        url: CTFO.config.sources.queryCompanySetbookList + "?comId=" + comId,
        dataType: 'json',
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          CTFO.utilFuns.commonFuns.getSelectList(_.map(data, function(item) {
            return {
              "code": item.setbookCode,
              "name": item.setbookName
            };
          }), container, defaultVal);
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };

    /**
     * [initDeptSelectList 初始化发布部门 select]
     *
     * @param comId
     *            发布公司Id
     *
     * @param container
     *            发布部门容器
     *
     * @param defaultVal
     *            默认参数
     * @return {[type]} [description]
     */
    var initDeptSelectList = function(comId, container, defaultVal) {
      $.ajax({
        url: CTFO.config.sources.queryAnnouceDept + "?comId=" + comId,
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

    /**
     * [initAnnoucePeopleSelectList 初始化发布人 select]
     *
     * @param comId
     *            发布公司Id
     *
     * @param deptId
     *            发布部门Id
     *
     *
     * @param container
     *            发布人容器
     *
     * @param defaultVal
     *            默认参数
     * @return {[type]} [description]
     */
    var initAnnoucePeopleSelectList = function(comId, deptId, container, defaultVal) {

      $.ajax({
        url: CTFO.config.sources.queryAnnouceDeptEmployeeList + "?entId=" + deptId,
        dataType: 'json',
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          CTFO.utilFuns.commonFuns.getSelectList(_.map(data, function(item) {
            return {
              "code": item.dicCode,
              "name": item.dicName
            };
          }), container, defaultVal);
        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    };

    /**
     * [initGrid 初始化grid对象]
     * @return {[type]} [description]
     */
    var initGrid = function() {
      grid = gridContainer.ligerGrid(gridOptions);


      //初始化 发布部门和发布人 的下拉列表
      var comOption = $(announceManageForm).find('select[name=comCode]');
      var deptOption = $(announceManageForm).find('select[name=annouceDept]');
      var annouceOption = $(announceManageForm).find('select[name=annoucePeopleId]');

      initComSelectList(comOption);

      //公司联动帐套 和 发布部门
      /*comOption.change(function() {
        var comId = comOption.val();
        initDeptSelectList(comId, deptOption);
        var deptId = deptOption.val();
        initAnnoucePeopleSelectList(comId, deptId, annouceOption);
      });*/
      initDeptSelectList('', deptOption);
      //部门联动发布人
      deptOption.change(function() {
        var comId = comOption.val();
        var deptId = deptOption.val();
        initAnnoucePeopleSelectList(comId, deptId, annouceOption);
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
        data = announceManageForm.serializeArray();
      $(data).each(function(event) {
        var name = 'requestParam.equal.' + this.name;
        if (this.value) {
          if (name.indexOf("DateStart") > 0) {
            this.value += " 00:00:00"
          }
          if (name.indexOf("DateEnd") > 0) {
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

      // 撤回
      if ($.inArray(detailRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.returnButton').remove();
      }

      // 审核
      if ($.inArray(applayRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.applyButton').remove();
      }

      // 发布
      if ($.inArray(publishRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.publishButton').remove();
      }

      // 删除
      if ($.inArray(deleteRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.removeButton').remove();
      }

      // 修改
      if ($.inArray(updateRowAuth, CTFO.cache.auth) < 0) {
        $(container).find('.editButton').remove();
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
        cloudBackUpForm = p.mainContainer.find('form[name=cloudBackUpForm]');
        announceManageForm = p.mainContainer.find('form[name=announceManageForm]');
        gridContainer = p.mainContainer.find('.gridContainer');
        gridContainerWrap = p.mainContainer.find('.gridContainerWrap');
        //公告详情
        bulletin_manage_detail = $('#bulletin_manage_detail').html(); //获取公告模板
        vehicleTeamModifyTmpl = $('#vehicle_team_update_tmpl').html();
        //修改公告详情
        modify_bulletin_manage_detail = $('#modify_bulletin_manage_detail').html();
        //修改公告详情附件列表
        modify_bulletin_attachment_list = $('#modify_bulletin_attachment_list').html();
        //审核公告详情
        examine_bulletin_manage_detail = $('#examine_bulletin_manage_detail').html();
        //新增公告详情
        add_bulletin_manage_detail = $('#add_bulletin_manage_detail').html();
        //新增公告详情附件
        modify_bulletin_attachment_list_tr = $('#modify_bulletin_attachment_list_tr').html();


        resize(p.cHeight);

        bindEvent();
        initTreeContainer();
        initForm();
        initGrid();
        initAuth(announceManageForm);


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