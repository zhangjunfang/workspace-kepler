/**
 * [ 车队管理模块]                                                                                        resize(ch [description]
 * @return {[type]}         [description]
 */
CTFO.Model.CompanyProfile = (function() {
  var uniqueInstance;

  function constructor() {
    var p = {};
    var cHeight = 0,
      minH = 520, // 本模块最低高度
      gridHeight = 300, // grid高度
      pageSize = 30,
      pageSizeOption = [ 10, 20, 30, 40 ],

      treeContainer = null,
      vehicleTeamForm = null,
      vehicleTeamTerm = null,
      gridContainer = null,

      leftTree = null,
      grid = null,
      addFlag = null,
      currentEntName = '',

      vehicleTeamDetailTmpl = null,
      vehicleTeamModifyTmpl = null,
      companyManageDetailTmpl = null,
      companyForm = null,

      updateRowAuth = 'FG_MEMU_OPERATIONS_DATA_TEAM_U', // 修改记录权限
      detailRowAuth = 'FG_MEMU_OPERATIONS_DATA_TEAM_INFO', // 查看记录详情权限
      deleteRowAuth = 'FG_MEMU_OPERATIONS_DATA_TEAM_D', // 删除记录权限
      addRowAuth = 'FG_MEMU_OPERATIONS_DATA_TEAM_C', // 新增记录权限
      exportRowAuth = 'FG_MEMU_OPERATIONS_DATA_TEAM_IMP', // 导出记录权限
      isPassByValueMode = CTFO.config.globalObject.isPassByValueMode,//传值方式
      test = '';
    /**
     * [compileVehicleTeamDetail 渲染车队详情弹窗]
     * @param  {[Object]} d    [数据对象]
     * @param  {[Object]} tmpl [详情模板对象]
     * @return {[type]}      [description]
     */
    var compileVehicleTeamDetail = function (d, tmpl) {
      var doTtmpl = doT.template(tmpl);
      d.entTypeDesc = (+d.entType === 1 ? '企业' : (+d.entType === 2 ? '车队' : ''));
      d.provinceDesc = CTFO.utilFuns.codeManager.getNameByCode('SYS_AREA_INFO', d.corpProvince);
      d.cityDesc = CTFO.utilFuns.codeManager.getCityProvcodeNameByCode('SYS_AREA_INFO', d.corpProvince, d.corpCity);
      d.corpQualeDesc = CTFO.utilFuns.codeManager.getNameByCode('SYS_CORP_BUSINESS_SCOPE', d.corpQuale);
      d.corpLevelDesc = CTFO.utilFuns.codeManager.getNameByCode('SYS_CORP_LEVEL', d.corpLevel);
      return doTtmpl(d);
    };
    /**
     * [initVehicleDetailUpdate 绑定新增/更新弹窗事件]
     * @param  {[String]} t [类别，c:新增，u:更新]
     * @param  {[Object]} w [弹窗Dom对象]
     * @param  {[Object]} d [数据对象]
     * @param  {[Object]} g [弹窗对象]
     * @return {[type]}   [description]
     */
    var initVehicleDetailUpdate = function (t, w, d, g) {
      if(d){
    	  currentEntName = d.entName;
      }
      var actionUrl = t === 'c' ? CTFO.config.sources.insertVehicleTeam : (t === 'u' ? CTFO.config.sources.updateVehicleTeam : '');
      if (!actionUrl) return false;
      initVehicleTeamDetailUpdate(w, d || {});
      var validator = validateFormParams(w);
    };
    /**
     * [initVehicleTeamDetailUpdate 初始化新增/更新弹窗的异步填充内容]
     * @param  {[Dom]} w [弹窗对象]
     * @param  {[Object]} d [数据对象]
     * @return {[type]}   [description]
     */
    var initVehicleTeamDetailUpdate = function (w, d) {
      var provinceOption = $(w).find('select[name=corpProvince]'),
        cityOption = $(w).find('select[name=corpCity]'),
        corpQuale = $(w).find('select[name=corpQuale]'),
        corpLevel = $(w).find('select[name=corpLevel]');
        $(w).find('select[name=entType]')[0].selectedIndex=d.entType-1;
      CTFO.utilFuns.codeManager.getProvAndCity(provinceOption, cityOption, d.corpCity, d.corpProvince);
      CTFO.utilFuns.codeManager.getSelectList('SYS_CORP_BUSINESS_SCOPE', corpQuale, d.corpQuale);
      CTFO.utilFuns.codeManager.getSelectList('SYS_CORP_LEVEL', corpLevel, d.corpLevel);
    };
    var validateFormParams = function (w) {
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
        submitHandler: function () {
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
    var saveVehicleTeamDetailFrom = function (url, parms, callback) {
      var newParms = {};
      $.each(parms,function(name,val){
        if(name.indexOf(".")>-1){
          newParms[name.split(".")[1]]=val;
        }else{
          newParms[name]=val;
        }
      });
      $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        data : isPassByValueMode?JSON.stringify(newParms):parms,
        contentType : isPassByValueMode?'application/json; charset=utf-8':'application/x-www-form-urlencoded;charset=UTF-8',
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
	var validateEntName = function(t,container) {
		var parms = {};
		var entName = $(container).find('form[name=vehicleTeamModifyForm]').find("input[name='entName']")
		//var cardNo = $(container).find('input[name="icCard.cardNo"]');//驾驶员IC卡 卡号
		if(t != 'c' && currentEntName === $.trim(entName.val()))//修改状态 下的 驾驶员IC卡号 不同才做验证
			return true;
		var validateObj = new Object();//验证对象
		parms["requestParam.equal.entName"] = $.trim(entName.val());//驾驶证档案号 验证是否唯一
		validateObj.obj = [ entName ];
		validateObj.url = CTFO.config.sources.checkEntNameExist;
		validateObj.parms = parms;
		validateObj.message = "已经存在";
		if (0 < $.trim(entName.val()).length && !validateAjax(validateObj)) {
			return false;//返回 停止继续验证
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
			url : validateObj.url,
			type : 'post',
			dataType : 'text',
			async : false,
			data : validateObj.parms,
			error : function() {
				$.ligerDialog.error("验证失败");
			},
			success : function(r) {
				r = JSON.parse(r);
				if(r && r.displayMessage) r = r.displayMessage;
				if ("success" === r) {//后台数据 返回 success 表示 该数据 已经存在
					for ( var i = 0; i < validateObj.obj.length; i++) {
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
	
    var disabledButton = function() {
    	
    };
    /**
     * [bindEvent 绑定全局事件]
     * @return {[type]} [description]
     */
    var bindEvent = function() {
    	addFlag = false;
        $(companyForm).find('span[name="saveForm"]').click(function(event) {
      	  if(addFlag && !userExist(companyForm))
      		  return false;
            var parms = [];
            var d = $(companyForm).serializeArray();
            $(d).each(function() {
          	  parms.push({name:this.name, value:$.trim(this.value)});
            });
            if (companyForm.find('span[name="saveForm"]').attr('disabled')) {
          	  return false;
            }
            var newParms = {};
            newParms["comCode"] = $("input[name=comCode]").attr('value');
            $.each(parms,function(i,n){
              var name = n.name;
              if(name.indexOf(".")>-1){
                var name = name.split(".")[1];
              }
              newParms[name]=n.value;
            });
            disabledButton(true);// 控制按钮
            var param = 
            $.ajax({
                url : CTFO.config.sources.modifySysComInfo,
                type : 'post',
                dataType: 'json',
                data : isPassByValueMode?JSON.stringify(newParms):parms,
                contentType : isPassByValueMode?'application/json; charset=utf-8':'application/x-www-form-urlencoded;charset=UTF-8',
                error : function(){
                    disabledButton(false);// 控制按钮
                 },
                success : function(r) {
                    disabledButton(false);// 控制按钮
                    var text = addFlag ? "新增操作" : "修改操作";
                    if (r && r.displayMessage=="success") {
                    	$.ligerDialog.error( "修改数据成功" );
                    }else {
                        $.ligerDialog.error(text + "失败");
                    }
                 }
            });
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
     * 初始化赋值操作
     */
    var compileFormData = function(d) {
        var d = d || {};
        var doTtmpl = doT.template(companyManageDetailTmpl);
        $(companyForm).find(".companyFormBox").html(doTtmpl(d));

        var provinceOption = $(companyForm).find('select[name=province]'),
        cityOption = $(companyForm).find('select[name=city]'),
        countyOption = $(companyForm).find('select[name=county]');
        CTFO.utilFuns.codeManager.initProvAndCityAndCounty( provinceOption, cityOption,  countyOption , d.province, d.city, d.county );

        //initCompanySelectList( $(companyForm).find('select[name=parentComid]') , d.parentComid );
        
    };
    /**
     * [initForm 初始化查询条件form]
     * @return {[type]} [description]
     */
    var initForm = function() {
          var comId = CTFO.cache.user.comId;
          $.ajax({
              url: CTFO.config.sources.companyDetail,
              type: 'POST',
              dataType: 'json',
              data: {'comId':comId},
              complete: function(xhr, textStatus) {
                //called when complete
              },
              success: function(data, textStatus, xhr) {
                    compileFormData(data);
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
     * [validateParams 获取查询参数]
     * @return {[type]} [description]
     */
    var validateParams = function () {
      var param = [],
        data = vehicleTeamForm.serializeArray();
      $(data).each(function(event) {
        var name = '';
        if (this.name === 'corpCode' || this.name === 'entName') name = 'requestParam.like.' + this.name;
        else name = 'requestParam.equal.' + this.name;
        if (this.value) param.push({name: name, value: this.value});
      });
      return param;
    };

    var resize = function(ch) {
      if (ch < minH) ch = minH;
      p.mainContainer.height(ch);
      gridContainerWrap.height( p.mainContainer.height()- pageLocation.outerHeight() -vehicleTeamTerm.outerHeight() - parseInt(gridContainerWrap.css('margin-top'))*3 -parseInt(gridContainerWrap.css('border-top-width'))*2 );
      gridHeight =gridContainerWrap.height();

      treeContainer.height(ch);

      if(leftTree) leftTree.resize();
    };

    /**
     * @description 初始化权限Button
     * @param container
     */
    var initAuth = function(container) {

          // 增加
          if ($.inArray('FG_MEMU_OPERATIONS_DATA_TEAM_C', CTFO.cache.auth) < 0) {
            $(container).find('.addVehicleTeam').remove();
          }
          // 导出
          if ($.inArray('FG_MEMU_OPERATIONS_DATA_TEAM_IMP', CTFO.cache.auth) < 0) {
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
        gridContainer = p.mainContainer.find('.gridContainer');
        gridContainerWrap = p.mainContainer.find('.gridContainerWrap');
        companyForm = p.mainContainer.find('form[name=companyForm]');

        vehicleTeamDetailTmpl = $('#vehicle_team_detail_tmpl').html();
        vehicleTeamModifyTmpl = $('#vehicle_team_update_tmpl').html();
        companyManageDetailTmpl = $('#company_manage_detail_tmpl').html();

        resize(p.cHeight);

        bindEvent();
        initTreeContainer();
        initForm();
        initAuth(vehicleTeamForm);


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