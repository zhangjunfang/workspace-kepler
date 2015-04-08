/**
 * [ 业务管理 - 维修单统计]                                                                                        resize(ch [description]
 * @return {[type]}         [description]
 */
CTFO.Model.RepairStat = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var cHeight = 0,
      minH = 520, // 本模块最低高度
      gridHeight = 300, // grid高度
      pageSize = 50,
      pageSizeOption = [10, 20, 30, 40, 50, 100],

      repairGridContent = null,
      repairFormContent = null,
      treeContainer = null,
      repairStatForm = null,
      repairStatTerm = null,
      gridContainer = null,

      leftTree = null,
      grid = null,
      gridDetail = null,
      gridProject = null,
      gridMaterials = null,
      gridCharge = null,
      gridAnnex = null,
      addFlag = null,
      currentEntName = '',
      htmlObj = null,
      repairDetailTmpl = null,
      ManHourQuantitySum = null,
      SumMoneyGoodsSum = null,
      QuantitySum = null,
      MemberPriceSum = null,
      SumMoneySum = null,
      Charge_SumMoneySum = null,
      repairDeail = null,

      updateRowAuth = 'FG_MEMU_DATAANALSIS_REPAIRSTAT_U', // 修改记录权限
      detailRowAuth = 'FG_MEMU_DATAANALSIS_REPAIRSTAT_I', // 查看记录详情权限
      deleteRowAuth = 'FG_MEMU_DATAANALSIS_REPAIRSTAT_D', // 删除记录权限
      addRowAuth = 'FG_MEMU_DATAANALSIS_REPAIRSTAT_C', // 新增记录权限
      exportRowAuth = 'FG_MEMU_DATAANALSIS_REPAIRSTAT_E', // 导出记录权限
      opLogRowAuth = 'FG_MEMU_DATAANALSIS_REPAIRSTAT_O', // 操作记录
      isPassByValueMode = CTFO.config.globalObject.isPassByValueMode, //传值方式
      current_maintain_id = null, //当前维修单id
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
      display: '所在地',
      name: 'corpProvince',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.codeManager.getCountyName(row.province, row.city, row.county);
      }
    },{
        display: '开始时间',
        name: 'startTime',
        width: 100,
        sortable: true,
        align: 'center',
        render: function(row) {
          return CTFO.utilFuns.dateFuns.utc2date(row.startTime);
      }
   },{
      display: '结算时间',
      name: 'endTime',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.dateFuns.utc2date(row.endTime);
      }
    }, {
      display: '维修单数量',
      name: 'repairCount',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return '<span class="mr10 hand cBlue" actionType="showDetailStatement">' + row.repairCount + '</span>';
      }
    }, {
      display: '维修单项目数',
      name: 'repairProject',
      width: 160,
      sortable: true,
      align: 'center'
    }, {
      display: '维修结算金额',
      name: 'repairSettlement',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return row.repairSettlement.toFixed(2) + "元";
      }
    }, {
      display: '工时费用',
      name: 'manHourCost',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return row.manHourCost.toFixed(2) + "元";
      }
    }, {
      display: '配件数量',
      name: 'accessories',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '配件结算金额',
      name: 'accessoriesSettlement',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return row.accessoriesSettlement.toFixed(2) + "元";
      }
    }];

    var columnsDetail = [{
      display: '维修单号',
      name: 'settlementId',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return '<span class="mr10 hand cBlue" actionType="showDetailExplicit">' + row.settlementId + '</span>';
      }
    }, {
      display: '工时价税合计',
      name: 'manHourSum',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '配件价税合计',
      name: 'fittingSum',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '其他项目价税合计',
      name: 'otherItemSum',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '优惠费用',
      name: 'privilegeCost',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '应收总额',
      name: 'shouldSum',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '实收总额',
      name: 'receivedSum',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '本次欠款金额',
      name: 'debtCost',
      width: 160,
      sortable: true,
      align: 'center'
    }, {
      display: '单据状态',
      name: 'documentStatus',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '结算时间',
      name: 'createTime',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return CTFO.utilFuns.dateFuns.utc2date(row.createTime);
      }
    }, {
      display: '车牌号',
      name: 'vehicleNo',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '客户编号',
      name: 'customerCode',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '客户姓名',
      name: 'customerName',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '维修付费方式',
      name: 'maintainType',
      width: 100,
      sortable: true,
      align: 'center'
    }];

    var columnsDetailProject = [{
      display: '项目编码',
      name: 'itemId',
      width: 100,
      sortable: true,
      align: 'center',
      frozen: true,
      totalSummary: {
        render: function(column, cell) {
          return '合计';
        }
      }
    }, {
      display: '维修项目类别',
      name: 'itemType',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '项目名称',
      name: 'itemName',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '工时类别',
      name: 'manHourType',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '工时数量',
      name: 'manHourQuantity',
      width: 100,
      sortable: true,
      align: 'center',
      totalSummary: {
        render: function(column, cell) {
          var r = CTFO.utilFuns.commonFuns.isFloat(ManHourQuantitySum) ? ManHourQuantitySum : '--';
          return r;
        }
      }
    }, {
      display: '原工时单价',
      name: 'manHourNormUnitprice',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '会员折扣%',
      name: 'memberDiscount',
      width: 160,
      sortable: true,
      align: 'center'
    }, {
      display: '会员工时费',
      name: 'memberPrice',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return row.memberPrice + "元";
      }
    }, {
      display: '折扣额',
      name: 'memberSumMoney',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '货款',
      name: 'sumMoneyGoods',
      width: 100,
      sortable: true,
      align: 'center',
      totalSummary: {
        render: function(column, cell) {
          var r = CTFO.utilFuns.commonFuns.isFloat(SumMoneyGoodsSum) ? SumMoneyGoodsSum : '--';
          return r;
        }
      }
    }, {
      display: '是否三包',
      name: 'threeWarranty',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '备注',
      name: 'remarks',
      width: 100,
      sortable: true,
      align: 'center'
    }];



    var columnsDetailMaterials = [{
      display: '配件编码',
      name: 'partsCode',
      width: 100,
      sortable: true,
      align: 'center',
      frozen: true,
      totalSummary: {
        render: function(column, cell) {
          return '合计';
        }
      }
    }, {
      display: '配件名称',
      name: 'partsName',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '规格',
      name: 'norms',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '单位',
      name: 'corpProvince',
      width: 100,
      sortable: true,
      align: 'center',
      render: function(row) {
        return "个";
      }
    }, {
      display: '进口',
      name: 'whetherImported',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '数量',
      name: 'quantity',
      width: 100,
      sortable: true,
      align: 'center',
      totalSummary: {
        render: function(column, cell) {
          var r = CTFO.utilFuns.commonFuns.isInt(QuantitySum) ? QuantitySum : '--';
          return r;
        }
      }
    }, {
      display: '原始单价',
      name: 'unitPrice',
      width: 160,
      sortable: true,
      align: 'center'
    }, {
      display: '会员折扣%',
      name: 'memberDiscount',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '会员单价',
      name: 'memberPrice',
      width: 100,
      sortable: true,
      align: 'center',
      totalSummary: {
        render: function(column, cell) {
          var r = CTFO.utilFuns.commonFuns.isFloat(MemberPriceSum) ? MemberPriceSum : '--';
          return r;
        }
      }
    }, {
      display: '货款',
      name: 'sumMoney',
      width: 100,
      sortable: true,
      align: 'center',
      totalSummary: {
        render: function(column, cell) {
          var r = CTFO.utilFuns.commonFuns.isFloat(SumMoneySum) ? SumMoneySum : '--';
          return r;
        }
      }
    }, {
      display: '图号',
      name: 'drawnNo',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '适用车型',
      name: 'vehicleModel',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '品牌',
      name: 'vehicleBrand',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '是否三包',
      name: 'threeWarranty',
      width: 100,
      sortable: true,
      align: 'center'
    }];


    var columnsDetailCharge = [{
      display: '其他项目费用类别',
      name: 'costTypes',
      width: 100,
      sortable: true,
      align: 'center',
      frozen: true,
      totalSummary: {
        render: function(column, cell) {
          return '合计';
        }
      }
    }, {
      display: '其他费用金额',
      name: 'sumMoney',
      width: 100,
      sortable: true,
      align: 'center',
      totalSummary: {
        render: function(column, cell) {
          var r = CTFO.utilFuns.commonFuns.isFloat(Charge_SumMoneySum) ? Charge_SumMoneySum : '--';
          return r;
        }
      }
    }, {
      display: '备注',
      name: 'remarks',
      width: 100,
      sortable: true,
      align: 'center'
    }];



    var columnsDetailAnnex = [{
      display: '附件名称',
      name: 'accessoryName',
      width: 100,
      sortable: true,
      align: 'center',
      toggle: false
    }, {
      display: '类别',
      name: 'accessoryType',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '文件详情',
      name: 'accessoryDetails',
      width: 100,
      sortable: true,
      align: 'center'
    }, {
      display: '备注',
      name: 'remarks',
      width: 100,
      sortable: true,
      align: 'center'
    }];
    // grid初始化参数
    var gridOptions = {
      columns: columns,
      sortName: 'createTime',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      url: CTFO.config.sources.repairStatGrid, // 数据请求地址
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
    var gridDetailOptions = {
      columns: columnsDetail,
      sortName: 'createTime',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      pageSize: pageSize,
      pageSizeOption: pageSizeOption,
      pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
      pagesizeParmName: 'requestParam.rows',
      width: '100%',
      height: gridHeight,
      delayLoad: true,
      rownumbers: true,
      allowUnSelectRow: true,
      onSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onUnSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onSuccess: function(data) {}
    };

    var gridDetailProject = {
      columns: columnsDetailProject,
      sortName: 'createTime',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      pageSize: pageSize,
      pageSizeOption: pageSizeOption,
      pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
      pagesizeParmName: 'requestParam.rows',
      width: '100%',
      height: gridHeight,
      delayLoad: false,
      rownumbers: true,
      allowUnSelectRow: true,
      onSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onUnSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onSuccess: function(data) {
        ManHourQuantitySum = data.ManHourQuantitySum;
        SumMoneyGoodsSum = data.SumMoneyGoodsSum;
      }
    };
    var gridDetailMaterials = {
      columns: columnsDetailMaterials,
      sortName: 'createTime',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      pageSize: pageSize,
      pageSizeOption: pageSizeOption,
      pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
      pagesizeParmName: 'requestParam.rows',
      width: '100%',
      height: gridHeight,
      delayLoad: false,
      rownumbers: true,
      allowUnSelectRow: true,
      onSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onUnSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onSuccess: function(data) {
        QuantitySum = data.QuantitySum;
        MemberPriceSum = data.MemberPriceSum;
        SumMoneySum = data.SumMoneySum;
      }
    };
    var gridDetailCharge = {
      columns: columnsDetailCharge,
      sortName: 'createTime',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      pageSize: pageSize,
      pageSizeOption: pageSizeOption,
      pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
      pagesizeParmName: 'requestParam.rows',
      width: '100%',
      height: gridHeight,
      delayLoad: false,
      rownumbers: true,
      allowUnSelectRow: true,
      onSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onUnSelectRow: function(rowData, rowIndex, rowDom, eDom) {
        return bindGridRowEvent(rowData, rowIndex, rowDom, eDom);
      },
      onSuccess: function(data) {
        Charge_SumMoneySum = data.Charge_SumMoneySum;
      }
    };
    var gridDetailAnnex = {
      columns: columnsDetailAnnex,
      sortName: 'createTime',
      sortnameParmName: 'requestParam.equal.sortname', // 页排序列名(提交给服务器)
      sortorderParmName: 'requestParam.equal.sortorder',
      pageSize: pageSize,
      pageSizeOption: pageSizeOption,
      pageParmName: 'requestParam.page', // 页索引参数名，(提交给服务器)
      pagesizeParmName: 'requestParam.rows',
      width: '100%',
      height: gridHeight,
      delayLoad: false,
      rownumbers: true,
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
        case 'showDetailStatement':
          showDetailStatement(rowData);
          break;
        case 'showDetailExplicit':
          showRepairDetail(rowData);
          break;
      }
      return !actionType;
    };
    /**
     * [showDetailStatement 展示维修单详细表格信息]
     * @param  {[Object]} rowData   [行数据]
     */
    var showDetailStatement = function(rowData) {
        var maintainStatisId = rowData.maintainId;
        if (repairFormContent.hasClass('none')) {
          //展示维修单详细表格信息
          repairFormContent.removeClass('none');
          repairGridContent.addClass('none');

          var wrap = gridContainerWrap.eq(1);
          wrap.height(p.mainContainer.height() - pageLocation.outerHeight() - repairStatTerm.eq(1).outerHeight() - parseInt(wrap.css('margin-top')) * 3 - parseInt(wrap.css('border-top-width')) * 2);
          gridDetailOptions.height = wrap.height();
          gridDetailOptions.url = CTFO.config.sources.repairSingleGrid + "?maintainStatisId=" + maintainStatisId;
          gridDetail = gridContainerDetial.ligerGrid(gridDetailOptions);
          gridDetail.loadData(true);

        };
      }
      /**
       * [showRepairDetail 展示维修单详细]
       * @param  {[Object]} rowData   [行数据]
       */
    var showRepairDetail = function(rowData) {
      var maintain_id = current_maintain_id = rowData.maintainId;
      $.ajax({
        url: CTFO.config.sources.repairSingleDetail,
        type: 'POST',
        dataType: 'json',
        data: {
          'maintain_id': maintain_id
        },
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          repairFormContent.addClass('none');
          repairGridContent.addClass('none');
          repairFormDeailContent.removeClass('none');

          resetThis();
          compileFormData(data);

          var tmpl = $("#repair_stat_bottom_tmpl").html();
          var content = compileRepairDetail(data, tmpl);
          repairFormDeailContent.find(".repairStatBottom").html(content);
          htmlObj.deviceStatusTab.find(".isTab").eq(0).trigger('click');


        },
        error: function(xhr, textStatus, errorThrown) {
          //called when there is an error
        }
      });
    }
    var resetThis = function() {
      $(repairFormDeailContent).find('input[type="text"]').each(function() {
        $(this).val("");
      }).end().find('select').each(function() {
        $(this).val("");
      }).end().find('textarea').each(function() {
        $(this).val("");
      }).end().find('input[type="password"]').each(function() {
        $(this).val("");
      }).end();
      //错误标签
      $(repairDeail).find('label[class="error"]').each(function() {
        $(this).remove();
      });
      $(repairDeail).find('.error').removeClass('error');
    };
    /**
     * 初始化赋值操作
     */
    var compileFormData = function(r) {
      var d = {};
      for (var n in r) {
        var key = n;
        if (key == 'completeWorkTime' || key == 'maintainTime' || key == 'createTime' || key == 'integral') {
          d[key] = (key > 0) ? CTFO.utilFuns.dateFuns.dateFormat(new Date(r[n]), 'yyyy-MM-dd') : '';
        } else {
          d[key] = r[n];
        }
        $(repairFormDeailContent).find('input[type=text]').each(function() {
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
      }
    };

    var getRepairDetail = function(maintain_id, tmpl, callback) {
      $.get(CTFO.config.sources.repairSingleDetail, {
        maintain_id: maintain_id
      }, function(data, textStatus, xhr) {
        var content = '';
        if (typeof(data) === 'string')
          data = JSON.parse(data);
        if (data && !data.error)
          content = compileRepairDetail(data, tmpl);
        if (callback)
          callback(content, data);
      });
    };
    var compileRepairDetail = function(d, tmpl) {
      var doTtmpl = doT.template(tmpl);
      return doTtmpl(d);
    };

    /**
     * [bindEvent 绑定全局事件]
     * @return {[type]} [description]
     */

    var bindEvent = function() {
      htmlObj.deviceStatusTab.unbind("click").click(function(event) {
        var clickDom = $(event.target),
          selectedClass = ' tit1 lineS69c_l lineS69c_r lineS69c_t cFFF ',
          fixedClass = ' tit2 lineS_l lineS_r lineS_t ';
        if (!clickDom.hasClass('isTab')) return false;
        changeTab(clickDom, htmlObj.tagContent, selectedClass, fixedClass);
      }).end();
    };

    /*切换公用方法*/
    var changeTab = function(clickDom, container, selectedClass, fixedClass) {
      var index = clickDom.index();
      if (clickDom.hasClass(selectedClass)) return false;
      $(clickDom).addClass(selectedClass).removeClass(fixedClass).siblings().removeClass(selectedClass).addClass(fixedClass);
      $(container).hide().eq(index).show();



      var wrap = gridContainerWrap.eq(2);
      wrap.height(p.mainContainer.height() - pageLocation.outerHeight() - repairStatTerm.eq(2).outerHeight() - parseInt(wrap.css('margin-top')) * 3 - parseInt(wrap.css('border-top-width')) * 2 - 10 - $(repairFormDeailContent).find(".repairDeailBox").outerHeight() - $(repairFormDeailContent).find(".repairStatBottom").outerHeight());
      var gridHeight = wrap.height() - htmlObj.deviceStatusTab.outerHeight();
      if (index == "0") {
        gridDetailProject.height = gridHeight;
        gridDetailProject.url = CTFO.config.sources.repairProject + "?maintain_id=" + current_maintain_id;
        gridProject = gridContainerProject.ligerGrid(gridDetailProject);
      } else if (index == "1") {
        gridDetailMaterials.height = gridHeight;
        gridDetailMaterials.url = CTFO.config.sources.repairMaterials + "?maintain_id=" + current_maintain_id;
        gridMaterials = gridContainerMaterials.ligerGrid(gridDetailMaterials);
      } else if (index == "2") {
        gridDetailCharge.height = gridHeight;
        gridDetailCharge.url = CTFO.config.sources.repairCharge + "?maintain_id=" + current_maintain_id;
        gridCharge = gridContainerCharge.ligerGrid(gridDetailCharge);
      } else {
        gridDetailAnnex.height = gridHeight;
        gridDetailAnnex.url = CTFO.config.sources.repairAnnex + "?maintain_id=" + current_maintain_id;
        gridAnnex = gridContainerAnnex.ligerGrid(gridDetailAnnex);
      }
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

      var formContainer = repairStatForm.eq(0);
      $(formContainer).find('input[name=settlementTimeStart]').click(function(event) {
        WdatePicker({
          dateFmt: 'yyyy-MM-dd',
          isShowClear: false
        });
      });
      $(formContainer).find('input[name=settlementTimeEnd]').click(function(event) {
        WdatePicker({
          dateFmt: 'yyyy-MM-dd',
          isShowClear: false
        });
      });
      var provinceOption = $(formContainer).find('select[name=corpProvince]'),
        cityOption = $(formContainer).find('select[name=corpCity]'),
        countyOption = $(formContainer).find('select[name=corpCounty]');
      CTFO.utilFuns.codeManager.initProvAndCityAndCounty(provinceOption, cityOption, countyOption);

      $(formContainer).find('.queryButton').click(function(event) {
          searchGrid();
        }).end()
        .find('.resetButton').click(function(event) {
          resetThis();
        });

      var formContainer = repairStatForm.eq(1);
      $(formContainer).find('span[name="cancelSave"]').click(function(event) {
        repairGridContent.removeClass('none');
        repairFormContent.addClass('none');
      });

      $(repairStatForm).find('.exportGrid').click(function(event) {
        CTFO.utilFuns.commonFuns.exportGrid({
          grid: grid,
          url: CTFO.config.sources.exportRepairStatExcelData
        });
      });

      var formContainer = repairStatForm.eq(2);
      $(formContainer).find('span[name="cancelSave"]').click(function(event) {
        repairGridContent.removeClass('none');
        repairFormDeailContent.addClass('none');
      });

    };
    /**
     * @description 清空表单
     */
    var resetThis = function() {
      var formContainer = repairStatForm.eq(0);
      $(formContainer).find('input[type="text"]').each(function() {
        $(this).val("");
      }).end().find('select').each(function() {
        $(this).val("");
      }).end().find('textarea').each(function() {
        $(this).val("");
      }).end();

      var provinceOption = $(formContainer).find('select[name=corpProvince]'),
        cityOption = $(formContainer).find('select[name=corpCity]'),
        countyOption = $(formContainer).find('select[name=corpCounty]');

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
        data = repairStatForm.serializeArray();
      $(data).each(function(event) {
        var name = '';
        if (this.name === 'comName' || this.name === 'setbookName')
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

      var wrap = gridContainerWrap.eq(0);
      wrap.height(p.mainContainer.height() - pageLocation.outerHeight() - repairStatTerm.eq(0).outerHeight() - parseInt(wrap.css('margin-top')) * 3 - parseInt(wrap.css('border-top-width')) * 2);
      gridHeight = wrap.height();
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
        $(container).find('.exportGrid').remove();
      }

    };

    return {
      init: function(options) {
        p = $.extend({}, p || {}, options || {});
        htmlObj = {
          deviceStatusTab: p.mainContainer.find('.deviceStatusTab'), //切换标签盒
          tagContent: p.mainContainer.find('.tagContent') //切换块
        };
        pageLocation = p.mainContainer.find('.pageLocation');
        treeContainer = p.mainContainer.find('.leftTreeContainer');
        repairStatTerm = p.mainContainer.find('.repairStatTerm');
        repairStatForm = p.mainContainer.find('form[name=repairStatForm]');
        gridContainer = p.mainContainer.find('.gridContainer');
        gridContainerDetial = p.mainContainer.find('.gridContainerDetial');
        gridContainerProject = p.mainContainer.find('.gridContainerProject');
        gridContainerMaterials = p.mainContainer.find('.gridContainerMaterials');
        gridContainerCharge = p.mainContainer.find('.gridContainerCharge');
        gridContainerAnnex = p.mainContainer.find('.gridContainerAnnex');
        gridContainerWrap = p.mainContainer.find('.gridContainerWrap');

        repairGridContent = p.mainContainer.find('.userManageContent:eq(0)'); //查询条件以及表格容器
        repairFormContent = p.mainContainer.find('.userManageContent:eq(1)');
        repairFormDeailContent = p.mainContainer.find('.userManageContent:eq(2)');

        repairDeail = p.mainContainer.find('form[name="repairDeail"]');
        repairDetailTmpl = $('#repair_detail_tmpl').html();

        resize(p.cHeight);

        bindEvent();
        initTreeContainer();
        initForm();
        initGrid();
        initAuth(repairStatForm);


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