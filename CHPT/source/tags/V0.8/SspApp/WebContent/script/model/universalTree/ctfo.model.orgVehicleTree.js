CTFO.Model.OrgVehicleTree = (function () {
  var treeDataColumn = {
    online: 'ov',
    level: 't',
    text: 'n',
    id: 'i',
    total: 's',
    children: 'c'
  };
  var constructor = function (options) {
    var p = null,
      simpleTree = null,
      checkedNodeCache = [], // 已选节点id缓存
      vehiclesInOrg = {}, // 根据组织id查询出的车辆缓存
      selectedNodeDataCache = null, // 当前选中节点
      selectedVehicles = {},
      queryFinishedFlag = true,
      treeWidth = 438,
      test = '';
    var initTree = function () {
      var treeOptions = {
        url : CTFO.config.sources.orgTree,
        treeWidth: treeWidth,
        width: '100%',
        height: '100%',
        param : {
            "requestParam.equal.entId" : CTFO.cache.user.entId,
            "requestParam.equal.treeType" : CTFO.cache.user.entType,
            "requestParam.equal.root" : "true",
            "requestParam.equal.entType" : -1
        },
        checkbox : true,
        textFieldName : 'n',
        idFieldName: 'i',
        iconFieldName: 'icon',
        childrenName : 'c',
        onCheck: function(d, flag) {
          followVehicle(d, flag, treeDataColumn);
        },
        onBeforeExpand : getTreeLeaves,
        filterData: function(d, ischecked) {
          return filterTreeNodes(d, ischecked, treeDataColumn);
        },
        isLeaf: function(nodedata) {
          return (+nodedata.t !== 3) ? true : false;
        },
        onAfterAppend: function (curnode, data) {
          // if ($(curnode).find("ul > li").length > 0)
          if (data && $.isArray(data)) {
            var d = filterCheckedVehicles(data);
            resetCheckedNode(d, treeDataColumn, simpleTree, 'changeClass');
          }
        },
        onSuccess: function (data, curnode, param) {
          if (!curnode) simpleTree.expandRootNode(0);
        }
      };
      simpleTree = p.treeModeContent.ligerTree(treeOptions);
    };
    /**
     * [followVehicle 添加/删除已选车辆]
     * @param  {[Object]} note [节点对象,包括节点DOM对象target,节点数据对象data]
     * @param  {[Boolean]} flag [是否选中, true: 选中, false: 未选中]
     * @param  {[Object]} dp [其他参数,Boolean, true/false: 是否获取最佳地图视野, String, 车辆id]
     * @return {[type]}      [description]
     */
    var followVehicle = function(note, flag, dp) {
      var d = note.data,
        nid = d[dp.id],
        nlev = +d[dp.level],
        nodeId = nid + (nlev === 3 ? "_checkbox_tree_leaf" : "_checkbox_tree"),
        pos = $.inArray(nodeId, checkedNodeCache);
      if(flag) {
        d["status"] = "1";
        d["cid"] = nodeId;
        if (pos < 0) checkedNodeCache.push(nodeId);
        queryFinishedFlag = false;
      }else {
        if(pos >= 0) checkedNodeCache.splice(pos, 1);
      }
      if (nlev === 3) {
        var node=simpleTree.getNodeDom(d);
        d.pentName=$(node).parent().prev().find(".cliptext").text();
        alterVehicleCache(flag, [d], dp);
      }
      if (nlev === 2) {
        alterVehicleCache(flag, d.c, dp);
      }
      if (nlev === 1) {
        // 如果节点已经展开过, 获取子节点的nodeid属性值,此即车辆vid
        var node=simpleTree.getNodeDom(d);
        var pentName=$(node).find(".cliptext:eq(0)").text();
        if ($(note.target).find('ul.l-children').length > 0) {
          var vehicles = [];
          $(note.target).find('.l-children:eq(0) > li').each(function(index) {
            var vid = $(this).find('.l-checkbox').attr('nodeid');
            if (vid) {
              var data = simpleTree.getDataByID(vid);
              data.pentName=pentName;
              vehicles.push(data);
            }
          });
          if (vehicles.length > 0) alterVehicleCache(flag, vehicles, dp);
          else findTreeEntVehicles(nid, flag, dp,pentName);
        } else {
          findTreeEntVehicles(nid, flag, dp,pentName);
        }
      }
    };
    /**
     * [alterVehicleCache 改变已选车辆缓存]
     * @param  {[Boolean]} flag [是否选中, true: 选中, false: 未选中]
     * @param  {[Array]} d [车辆数据对象数组]
     * @param  {[Object]} dp [数据参数对象]
     * @return {[type]}      [description]
     */
    var alterVehicleCache = function(flag, d, dp) {
      var vids = [], idName = dp ? dp.id : 'vid', vnoName = dp ? dp.text : 'vehicleNo';
      if (!d || (d && d.length < 0)) {
        queryFinishedFlag = true;
        return false;
      }
      $(d).each(function(event) {
        var vid = this[idName] || this['vid'], // 勾选组织返回的车辆数据属性是vid,vehicleno
          vno = this[vnoName] || this['vehicleno'];
        if (flag && !selectedVehicles[vid]) selectedVehicles[vid] = this;
        else if (!flag && selectedVehicles[vid]) delete selectedVehicles[vid];
      });
    };
    /**
     * [findTreeEntVehicles 勾选/去勾选组织节点触发的事件]
     * @param  {[String]} entId      [组织id]
     * @param  {[Boolean]} flag      [勾选状态, true: 选中, false: 未选中]
     * @param  {[Object]} dp          [数据参数对象]
     * @return {[type]}            [description]
     */
    var findTreeEntVehicles = function(entId, flag, dp,pentName) {
      var param = {
            "requestParam.equal.entId": entId
          };
      if (vehiclesInOrg[entId]) {
        alterVehicleCache(flag, vehiclesInOrg[entId], dp);
        if (!flag) delete vehiclesInOrg[entId];
      } else {
        getVehiclesByEntId(param, function(d) {
          if(!d || (d && d.error) || (d && d.length < 1)) {
            queryFinishedFlag = true;
            return false;
          }
          vehiclesInOrg[entId] = d.slice(0); // 每次查询覆盖缓存
          $.each(d,function(i,n){
            n.pentName=pentName;
          });
          alterVehicleCache(flag, d, dp);
        });
      }
    };
    /**
     * [getVehiclesByEntId 根据组织id查询车辆]
     * @param  {[Object]}   param    [参数对象]
     * @param  {Function} callback [回调函数]
     * @return {[type]}            [description]
     */
    var getVehiclesByEntId = function(param, callback) {
      $.ajax({
        url: CTFO.config.sources.getVehiclesByEntId,
        type: 'POST',
        dataType: 'json',
        data: param,
        complete: function(xhr, textStatus) {
          //called when complete
        },
        success: function(data, textStatus, xhr) {
          // if(!data || (data && data.error)) return false;
          if(callback) callback(data);
        },
        error: function(xhr, textStatus, errorThrown) {
          queryFinishedFlag = true;
        }
      });
    };
    /**
     * [getTreeLeaves 获取子节点]
     * @param  {[Object]} note [节点对象,包括节点DOM对象target,节点数据对象data]
     * @return {[type]}      [description]
     */
    var getTreeLeaves = function(note) {
      var param = {
        "requestParam.equal.entId" : note.data.i,
        "requestParam.equal.treeType" : CTFO.cache.user.entType,
        "requestParam.equal.entType" : note.data.t,
        "requestParam.equal.all" : ''
      };

      if($(note.target).find("ul > li").length < 1) {
        var ischecked = $(note.target).find('.l-body > .l-checkbox').hasClass('l-checkbox-checked'); // 根节点是否选中状态
        simpleTree.loadData(note.target, CTFO.config.sources.orgTree, param, ischecked);
      }
    };
    /**
     * [filterTreeNodes 树加载完成后过滤节点]
     * @param  {[Object]} d       [数据对象]
     * @param  {[Boolean]} ischecked [节点是否选中状态]
     * @param  {[Object]} dp []
     * @return {[type]}         [description]
     */
    var filterTreeNodes = function(d, ischecked, dp) {
      if(!$.isArray(d)) return d;
      $(d).each(function(index) {
        var item = this;
        item.ischecked = ischecked ? ischecked : ($.inArray(item[dp.id], checkedNodeCache) > -1);
        var node=simpleTree.getNodeDom(this.treedataindex);
        if (+item[dp.level] === 1 || +item[dp.level] === 2) item.icon = 'script/plugin/ligerui/skins/ctfo/images/tree/firm.png';
        else item.icon = 'script/plugin/ligerui/skins/ctfo/images/tree/' + (+item[dp.online] === 1 ? 'car.png' : 'car-g.png');
        // 我的关注根节点
        if (item[dp.id] === 'root') item.icon = 'script/plugin/ligerui/skins/ctfo/images/tree/star.png';
        if (item[dp.children]) filterTreeNodes(item[dp.children], ischecked, dp);
      });
      return d;
    };

    var filterCheckedVehicles = function (data) {
      var d = [];
      var callFun = function (dd) {
        $(dd).each(function(event) {
          if (+this.t === 3) d.push(this);
          else if (+this.t !== 3 && this.c) callFun(this.c);
        });
      };
      callFun(data);
      return d;
    };

    var resetCheckedNode = function (d, dp, tree, eventType) {
      $(d).each(function(event) {
        if (selectedVehicles[dp.id]) {
          var node = tree.getNodeDom(this.treedataindex);
          var checkbox = $(node).find('.l-checkbox');
          if (!checkbox.hasClass('l-checkbox-checked')) {
            setTimeout(function() {
              eventType === 'click' ? checkbox.trigger('click') : checkbox.removeClass('l-checkbox-unchecked').addClass('l-checkbox-checked');
            }, 40);
          }
        }
      });
    };

    this.searchTree = function (param) {
        simpleTree.clear();
        simpleTree.loadData(null, CTFO.config.sources.orgTree, param);
    };


    this.init = function () {
      p = $.extend({}, p || {}, options || {});
      initTree(p);
    };
    this.getSelectedVehicles = function () {
      var vehicles = [];
      for (var n in selectedVehicles) {
        if (this) vehicles.push(selectedVehicles[n]);
      }
      return vehicles;
    };
    this.init(options);
  };
  return constructor;
})();