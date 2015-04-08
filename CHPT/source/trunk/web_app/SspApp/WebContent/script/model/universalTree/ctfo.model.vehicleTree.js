CTFO.Model.VehicleTree = (function () {
    // 静态私有变量和方法
    var treeDataColumn = {
        text: 'text',
        id: 'id',
        value: 'value'
      };
    // 构造器对象, 返回公共
    var constructor = function (options) {
      // 私有变量和方法
      var p = {hasCheckbox: true, beInit: true},
        checkedNodeCache = [], // 已选节点id缓存
        selectedNodeDataCache = null, // 当前选中节点数据缓存
        tree = null, // 树对象
        nodeCheckedLimit = 1000, // 最多勾选节点限制

        isFirstAppend = true, // 是否初始化树，只在初始化时有可能触发默认勾选事件
        treeSearchTimer = null, // 树的查询延时
        treeSearchTimerDelay = 500,
        treeRefreshTimer = null, // 树的刷新延时
        treeRefreshTimerDelay = 500,

        test = '';
      //左侧车辆树查询标示
      var queryFlag = false;
      /**
       * [searchTree 查询组织树]
       * @return {[type]}         [description]
       *
       */
      var searchTree = function () {
        if (treeSearchTimer) {
          clearTimeout(treeSearchTimer);
        }
        if (!tree) return false;
        treeSearchTimer = setTimeout(function () {
          var searchType = p.treeModelForm.find('select[name=keywordType]').val(),
            keyword = p.treeModelForm.find('input[name=keyword]').val(),
            param = [{
              name : 'vehicleState',
              value : '2'
            },{
              name : 'searchInfo',
              value :  keyword
            },{
              name : 'searchColumns',
              value : searchType
            }];
          if (CTFO.utilFuns.commonFuns.validateCharLength(keyword) < 3) {
            $.ligerDialog.alert("关键字至少需3个字符", "提示", "error");
            return false;
          }
          //查询则置为true
          queryFlag = true;
          tree.clear();
          tree.loadData(null, p.treeSearchUrl, param);
        }, treeSearchTimerDelay);
      };
      var refreshTree = function () {
        if (treeRefreshTimer) clearTimeout(treeRefreshTimer);
        treeRefreshTimer = setTimeout(function () {
          //刷新置为false
          queryFlag = false;
          tree.clear();
          tree.loadData(null, p.treeInitUrl, null);
        }, treeRefreshTimerDelay);
      };
      var clearSelectedData = function () {
        var selectedRows = tree.getChecked();
        $(selectedRows).each(function(event) {
          var target = this.target;
          tree.cancelSelect(target);
        });
      };
      var initForm = function (container) {
        CTFO.utilFuns.commonFuns.defaultFocusDesign([ {name:$(container).find('input[name=keyword]') ,value:'关键词'}]);
        $(container).find('input[name=keyword]').click(function (event) {
          var theEvent = window.event || event;
          var code = theEvent.keyCode || theEvent.which;
          if (code === 13) {
              searchTree();
          }
        }).end()
        .find('.queryButton').click(function(event) {
           $(container) .find('.queryButton').attr('disabled','disabled');
            var keyword = p.treeModelForm.find('input[name=keyword]').val();
            if(!keyword || '关键词' === keyword) {
                $(container) .find('.queryButton').removeAttr('disabled');
              return false;
            }
          searchTree();
        }).end()
        .find('.refreshButton').click(function(event) {
          refreshTree();
        }).end()
        .find('.clearButton').click(function(event) {
          if (p.handleCleanButton) p.handleCleanButton();
          clearSelectedData();
        });
      };
      /**
       * [initTree 初始化组织树]
       * @return {[type]}         [description]
       */
      var initTree = function (container) {
        var options = {
          childrenName: 'childrenList',
          width: '100%',
          height: '100%',
          checkbox: p.hasCheckbox,
          isCheckAll: false,
          onBeforeExpand : function (node) {
            if ($(node.target).find('ul > li').length < 1) {
              if (node.data.entType !== null && +node.data.entType === 2 && +node.data.nodeType === 2) {
                getVehiclesInOrg(node.target, node.data.id);
              }
            }
          },
          filterData: function(d, ischecked) {
            return filterTreeNodes(d, ischecked, treeDataColumn);
          },
          onSelect: function (node) {
            selectedNodeDataCache = node.data;
            if (p.onSelectNodeEvent) p.onSelectNodeEvent(selectedNodeDataCache, treeDataColumn);
          },
          onCheck: function (d, flag) {
            if (p.onCheckNode) p.onCheckNode(d, flag);
          },
          isLeaf: function (d) {
            return (+d.nodeType === 3) ? false : true;
          },
          onAfterAppend: function (pnode, data) { // 如果在别的模块已经加载过通用树，静态数据的加载完成会走这个方法
            if (p.beInit && isFirstAppend) setInitSelectedNode(container);
            isFirstAppend = false;
          },
          onSuccess: function (d, curnode, param) {
            p.treeModelForm.find('.queryButton').removeAttr('disabled');
           var htmlContent = container.html();
           container.html(htmlContent.replace("没有查询结果",""));
            if (!d || (d && d.length < 1)) {
              container.html('没有查询结果');
              return false;
            }
            if (!CTFO.cache.universalTreeInitData[p.treeType])
              CTFO.cache.universalTreeInitData[p.treeType] = d;
            if (isFirstAppend && p.beInit) setInitSelectedNode(container);
            isFirstAppend = false;
          }
        };
        if (CTFO.cache.universalTreeInitData[p.treeType]) {
          options.data = CTFO.cache.universalTreeInitData[p.treeType];
        } else if (p.vids && p.vids.length > 0) {
          queryFlag = true;
        } else{
          options.url = p.treeInitUrl + '&timestamp=' + new Date().getTime();
        }
        tree = container.ligerTree(options);

        if(p.vids && p.vids.length > 0) {
          searchTreeByVids(p.vids);
        }

      };
      /**
       * [filterTreeNodes 树加载完成后过滤节点]
       * @param  {[Object]} d       [数据对象]
       * @param  {[Boolean]} ischecked [节点是否选中状态]
       * @param  {[Object]} dp [不同的树节点的参数对象]
       * @return {[type]}         [description]
       */
      var filterTreeNodes = function(d, ischecked, dp) {
        if(!(d instanceof Array)) return d;
        // var newdata = [];
        $(d).each(function(index) {
          var item = this;
          item.ischecked = ischecked ? ischecked : ($.inArray(item[dp.id], checkedNodeCache) > -1);
          if (item.corpLevel) item.icon = 'script/plugin/ligerui/skins/ctfo/images/tree/firm.png';
          else item.icon = 'script/plugin/ligerui/skins/ctfo/images/tree/' + (+item.online === 1 ? 'car.png' : 'car-g.png');
          // newdata.push(item);
          if (item.childrenList) filterTreeNodes(item.childrenList, ischecked, dp);
        });
        return d;
      };
      // var filterTreeNodes = function(d, ischecked, dp) {
      //   if (+d[0].nodeType === 3) {
      //     if(!(d instanceof Array)) return d;
      //     var newdata = $.map(d, function(item, index) {
      //       item.ischecked = ischecked;
      //       if(+item[dp.level] === 1 || +item[dp.level] === 2) {
      //         item.icon = 'script/plugin/ligerui/skins/ctfo/images/tree/firm.png';
      //         item.isexpand = false;
      //       } else {
      //         item.icon = 'script/plugin/ligerui/skins/ctfo/images/tree/car.png';
      //       }
      //       return item;
      //     });
      //     return newdata;
      //   } else {
      //     return d;
      //   }
      // };
      var getCheckedNodes = function () {
        var selectedRows = tree.getChecked(), corpIds = [], teamIds = [], vids = [], vehicleNos = [];
        if (selectedRows.length >= nodeCheckedLimit) {
          $.ligerDialog.alert('不能勾选超过1000个节点', '提示','warn');
          return false;
        }
        $(selectedRows).each(function(event) {
          var node = this.target, data = this.data, nodeType = +data.nodeType || 3;
          switch (nodeType) {
            case 1:
              corpIds.push(data[treeDataColumn.id]);
              break;
            case 2:
              //如果没有通过左侧树查询，则获取所选车队ID的值，否则不获取所选车队ID的值
              if(!queryFlag) {
                teamIds.push(data[treeDataColumn.id]);
              }
              break;
            case 3:
              vids.push(data[treeDataColumn.id]);
              vehicleNos.push(data[treeDataColumn.text]);
              break;
          }
        });
        var rd = {};
        if (corpIds && corpIds.length > 0) rd.corpIds = corpIds;
        if (teamIds && teamIds.length > 0) rd.teamIds = teamIds;
        if (vids && vids.length > 0) rd.vids = vids;
        if (vehicleNos && vehicleNos.length > 0) rd.vehicleNos = vehicleNos;
        return rd;
      };
      /**
       * [getVehiclesInOrg 获取车队下的车辆]
       * @param  {[Object]} target [父节点]
       * @param  {[String]} teamId [车队id]
       * @return {[type]}        [description]
       */
      var getVehiclesInOrg = function (target, teamId) {
        $.getJSON(p.treeNodesUrl, {vehicleState: 2, teamId: teamId}, function(data) {
          var ischecked = $(target).find('.l-checkbox-checked').length > 0;
          var newdata = $.map(data, function(item, index) {
            item.ischecked = ischecked;
            return item;
          });
          if ($(target).find('ul > li').length < 1)
            tree.append(target, newdata, ischecked, null);
        });
      };
      var setInitSelectedNode = function(treeContainer) {
        if (+CTFO.cache.user.parentEntId === -1) treeContainer.find('.l-checkbox-unchecked').eq(1).trigger('click');
        else treeContainer.find('.l-checkbox-unchecked').eq(0).trigger('click');
      };
      /**
       * [ 根据车辆ID，查询车辆树]
       * @param  {[Array]} vids [车辆id数组]
       * @return {[type]}      [description]
       */
      searchTreeByVids = function (vids) {
        if (!tree) return false;
        var searchVals = vids || [];
          param = [{
            name : 'vehicleState',
            value : '2'
          },{
            name : 'searchInfo',
            value :  searchVals.join(",")
          },{
            name : 'searchColumns',
            value : 'vids'
          }];
        if (vids.length === 0) {
          $.ligerDialog.alert("查询车辆树时，车辆ID", "提示", "error");
          return false;
        }
        //查询则置为true
        queryFlag = true;
        tree.clear();
        tree.loadData(null, p.treeSearchUrl, param);
      };
      var resize = function (container, ch) {
        container.parents('.treeContentWrap').height(ch - 81 - 2);
        container.height(ch - 81 - 2);
      };
      // 特权方法
      this.init = function (options) {
        p = $.extend({}, p || {}, options || {});
        initForm(p.treeModelForm);
        initTree(p.treeModelContent);
        var ch = p.treeModelContainer.height();
        resize(p.treeModelContent, ch);
        // return this;
      };
      this.getSelectedData = function () {
        return getCheckedNodes();
      };
      this.getSelectedNodeData = function () {
        return selectedNodeDataCache;
      };

      this.getQueryFlag = function () {
        return queryFlag;
      };
      this.resize = resize;
      this.searchTreeByVids = searchTreeByVids;
      // 构造器执行代码
      this.init(options);
    };
    return constructor;
})();
