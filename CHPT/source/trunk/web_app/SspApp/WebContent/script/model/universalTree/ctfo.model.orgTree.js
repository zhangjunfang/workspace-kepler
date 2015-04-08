CTFO.Model.OrgTree = (function () {
    // 静态私有变量和方法
    var treeDataColumn = {
        text: 'text',
        id: 'id',
        value: 'value'
      };
    // 构造器对象, 返回公共
    var constructor = function (options) {
      // 私有变量和方法
      var p = null,
        // checkedNodeCache = [], // 已选节点id缓存
        tree = null, // 树对象

        test = '';
      /**
       * [searchTree 查询组织树]
       * @param  {[String]} keyword [查询关键字]
       * @return {[type]}         [description]
       */
      var searchTree = function (keyword) {
        if (!tree) return false;
        var param = {
          name : 'orgNameString',
          value : keyword
        };
        tree.clear();
        tree.loadData(null, p.treeSearchUrl, param);
      };
      /**
       * [initTree 初始化组织树]
       * @return {[type]}         [description]
       */
      var initTree = function (container) {
        var options = {
          childrenName: 'childrenList',
          url: p.treeInitUrl,
          width: '100%',
          height: '100%',
          checkbox: false,
          onClick: function(node) {
            if (p.treeClick) p.treeClick(node);
          }
        };
        tree = container.ligerTree(options);
      };

      var resize = function (container, ch) {
        container.parents('.treeContentWrap').height(ch - 81 - 2);
        container.height(ch - 81 - 2);
      };
      // 特权方法
      this.init = function (options) {
        p = $.extend({}, p || {}, options || {});

        initTree(p.treeModelContent);
        var ch = p.treeModelContainer.height();
        resize(p.treeModelContent, ch);
        // return this;
      };
      
      //获取树对象
      this.getTreeObj = function(){
    	  return tree || {};
      };

      // 构造器执行代码
      this.init(options);
    };
    return constructor;
})();
