
var operate_param = {
	"actionName" : "",
	"toPage" : "",
	"operateMod" : "",
	"operateId" : "",
	"username" : "",
	"password" : "",
	"realName" : "",
	"roleId" : "",
	"roleName" : "",
	"roleDesc" : "",
    "platName" : "",
    "remark" : "",
    "projectName" : "",
    "projectVersion" : "",
    "compileDate" : "",
    "branchName" : "",
    "branchPath" : "",
    "dbscriptPath" : "",
    "deployDesc" : "",
    "serverName" : "",
    "sshIp" : "",
    "sshPort" : "",
    "sshUsername" : "",
    "sshUserpwd" : "",
    "pid" : "",
    "serviceName" : "",
    "serviceType" : "",
    "launchType" : "",
    "launchShell" : ""
};

var tip_div = {
	"tipTitle" : "",
	"tipContent" : "",
	"alert" : "",
	"btnSure" : "确定",
	"btnCancel" : "取消"
};

var pageInfo = {
	"curPage" : 1,
	"total" : "",
	"totalPage" : "",
	"pageSize" :10,
	"pageGroup" : 1,
	"pageGroupCount" : ""
};

var initPageInfo = function(){
	pageInfo.curPage = 1;
	pageInfo.pageSize = 20;
};

var operateType = 0; //0 增加，1 修改，2 删除

function addInfo(addMod){
	operateType = 0;
	operate_param.operateMod = addMod;
	if(addMod == "user"){
		tip_div.tipTitle = "添加用户";
	}
	else if(addMod == "role"){
		tip_div.tipTitle = "添加角色";
	}
	else if(addMod == "plat"){
		tip_div.tipTitle = "新建平台";
	}
	else if(addMod == "project"){
		tip_div.tipTitle = "添加项目";
	}
	else if(addMod == "server"){
		tip_div.tipTitle = "新增服务器";
	}
	else if(addMod == "service"){
		tip_div.tipTitle = "添加服务";
	}
	tip_div.tipContent = "确定添加数据么？";
}

function toAdd(){
	if(operate_param.operateMod == "user"){ userAdd(); }
	else if(operate_param.operateMod == "role"){ roleAdd(); }
	else if(operate_param.operateMod == "plat"){ platAdd(); }
	else if(operate_param.operateMod == "project"){ projectAdd(); }
	else if(operate_param.operateMod == "server"){ serverAdd(); }
	else if(operate_param.operateMod == "service"){ serviceAdd(); }
}

function getEditInfo(editId,editMod){
	var actionName = "";
	var toPage = "";
	operateType = 1;
	operate_param.operateid = editId;
	operate_param.operateMod = editMod;
	if(editMod == "user"){
		actionName = "getEditUser";
		toPage = "rightUserEdit";
		tip_div.tipTitle = "用户修改";
	}
	else if(editMod == "role"){
		actionName = "getEditRole";
		toPage = "rightRoleOperate";
		tip_div.tipTitle = "角色修改";
	}
	else if(editMod == "plat"){
		actionName = "getEditPlatForm";
		toPage = "rightPlatFormOperate";
		tip_div.tipTitle = "平台修改";
	}
	else if(editMod == "project"){
		actionName = "getEditProject";
		toPage = "rightProjectOperate";
		tip_div.tipTitle = "项目修改";
	}
	else if(editMod == "server"){
		actionName = "getEditServer";
		toPage = "rightServerOperate";
		tip_div.tipTitle = "服务器修改";
	}
	else if(editMod == "service"){
		actionName = "getEditService";
		toPage = "rightServiceOperate";
		tip_div.tipTitle = "服务修改";
	}
	tip_div.tipContent = "确定提交修改后的数据么？";
	$.ajax({ 
		type:"POST",
		url:actionName + ".do",
		dataType : "json", 
		data:{editId: editId},
		success:function(data){
			if(!data){
				tip_div.tipTitle = "系统错误"; 
				tip_div.alert = "获取数据失败";
				alert_();
				$("#common_alert").trigger("click");
				return;
			} 
			/*var res = eval("("+data+")");
			var datas = res.data;*/
			toMainRight(toPage,data.data);
		}
	});
}

function toEdit(){
	if(operate_param.operateMod == "user"){ userEdit(); }
	else if(operate_param.operateMod == "role"){ roleEdit(); }
	else if(operate_param.operateMod == "plat"){ platEdit(); }
	else if(operate_param.operateMod == "project"){ projectEdit(); }
	else if(operate_param.operateMod == "server"){ serverEdit(); }
	else if(operate_param.operateMod == "service"){ serviceEdit(); }
}

function deleteInfo(delId,delMod){
	operateType = 2;
	operate_param.operateid = delId;
	if(delMod == "user"){
		operate_param.actionName = "delUser";
		operate_param.toPage = "rightUserList";
		tip_div.tipTitle = "删除用户";
	}
	else if(delMod == "role"){
		operate_param.actionName = "delRole";
		operate_param.toPage = "rightRoleList";
		tip_div.tipTitle = "删除角色";
	}
	else if(delMod == "plat"){
		operate_param.actionName = "delPlatForm";
		operate_param.toPage = "rightPlatFormList";
		tip_div.tipTitle = "删除平台";
	}
	else if(delMod == "project"){
		operate_param.actionName = "delProject";
		operate_param.toPage = "rightProjectList";
		tip_div.tipTitle = "删除项目";
	}
	else if(delMod == "server"){
		operate_param.actionName = "delServer";
		operate_param.toPage = "rightServerList";
		tip_div.tipTitle = "删除服务器";
	}
	else if(delMod == "service"){
		operate_param.actionName = "delService";
		operate_param.toPage = "rightServiceList";
		tip_div.tipTitle = "删除项目";
	}
	tip_div.tipContent = "确定删除么？";
	setTipDivContent();
	$("#common_a").trigger("click");
}

function toDelete(){
	$.ajax({ 
		type:"POST",
		url:operate_param.actionName + ".do",
		data:{"delId": operate_param.operateid},
		success:function(data){
			if(data == "done"){
				tip_div.tipContent = "";
				tip_div.alert = "删除成功";
				operateType = -1;
			} 
			else{
				tip_div.alert = "删除失败";
			}
			alert_();
			$("#common_alert").trigger("click");
			$("#mainRight").load(operate_param.toPage + ".html");
		}
	});
}


var getSysOptions = function(){
	$.ajax({ 
		type:"POST",
		url:"getSysOptions.do",
		dataType : "json", 
		data:{id:"id"},
		success:function(datas){
			/*if(!data){return;}
			var datas = eval("("+data+")");*/
			
			$(datas.roleOptions).each(function(){
				var roleInfo = {};
				roleInfo["code"] = this.roleid;
				roleInfo["name"] = this.rolename;
				roleOptions.push(roleInfo);
			});
			
			$(datas.platOptions).each(function(){
				var platInfo = {};
				platInfo["code"] = this.pid;
				platInfo["name"] = this.platname;
				platOptions.push(platInfo);
			});
			
			$(datas.serviceTypeOptions).each(function(){
				var serviceTypeInfo = {};
				serviceTypeInfo["code"] = this.stid;
				serviceTypeInfo["name"] = this.stname;
				serviceTypeOptions.push(serviceTypeInfo);
			});
			
			$(datas.serviceLaunchOptions).each(function(){
				var launchInfo = {};
				launchInfo["code"] = this.ltid;
				launchInfo["name"] = this.ltname;
				serviceLaunchOptions.push(launchInfo);
			});
		}
	});
};
getSysOptions();

var setOptions = function(formId,selId,type){
	var options = null;
	if(type == "role"){ 
		options = roleOptions; 
	}
	else if(type == "plat"){ 
		options = platOptions; 
	}
	else if(type == "serviceType"){
		options = serviceTypeOptions; 
	}
	else if(type == "launchType"){
		options = serviceLaunchOptions; 
	}
	
	var optionStr = "";
	for(var i = 0; i < options.length; i++){
		optionStr += "<option value='" + options[i].code + "'>" + options[i].name + "</option>";
	}
	$("#"+formId).find("#"+selId).html(optionStr);
};

var setTipDivContent = function(){
	$("#myModal").find("#myModalLabel").text(tip_div.tipTitle);
	$("#myModal").find("span").text(tip_div.tipContent);
};
var alert_ = function(){
	$("#alertInfo").find("#myModalLabel").text(tip_div.tipTitle);
	$("#alertInfo").find("span").text(tip_div.alert);
};

var calcPages = function(cur,total,pageSize){
	
	var totalPage = Math.ceil(total/pageSize);
	var pagesGroupCount = Math.ceil(totalPage/10);
	var lastPageGroup = totalPage%10;
	var i_begin = 0, i_max = 0;
	pageInfo.total = total;
	pageInfo.totalPage = totalPage;
	pageInfo.pageGroup = Math.ceil(cur/10);
	pageInfo.pageGroupCount = pagesGroupCount;
	
	if(pageInfo.pageGroup == 1){
		i_begin = 1;
		i_max = 10;
		if(pagesGroupCount == 1){
			i_max = lastPageGroup;
		}
	}
	else{
		i_begin = 1 + (pageInfo.pageGroup-1)*10;
		if(pageInfo.pageGroup == pagesGroupCount){
			i_max = i_begin + lastPageGroup - 1;
		}
		else{
			i_max = i_begin + 9;
		}
	}
	setPages_span();
	if(total != 0){
		var pageList = "<li><a href='javascript:void(0)' id='first' style='display:none' onclick='toFirstPage();'>首页</a></li>"
		+ "<li><a href='javascript:void(0)' id='prev' style='display:none' onclick='toPrevPage();'>上一页</a></li>";
		for(var i = i_begin; i <= i_max; i++){
			if(i == cur){
				pageList += "<li><a href='javascript:void(0)' name='num' style='background-color:#f5f5f5'>" + i + "</a></li>";
			}
			else{
				pageList += "<li><a href='javascript:void(0)' name='num' onclick='to_page(" + i + ");'>" + i + "</a></li>";
			}
		}
		pageList += "<li><a href='javascript:void(0)' id='next' onclick='toNextPage();'>下一页</a></li>"
			+ "<li><a href='javascript:void(0)' id='last' onclick='toLastPage();'>尾页</a></li>";
		$("#pages").find("ul").empty();
		$("#pages").find("ul").append(pageList);
		
		if(cur == 1){
			$("#pages").find("#first,#prev").hide();
		}
		else{
			$("#pages").find("#first,#prev").show();
		}
		if(cur == totalPage){
			$("#pages").find("#next,#last").hide();
		}
		else{
			$("#pages").find("#next,#last").show();
		}
	}
};

var toFirstPage = function(){
	pageInfo.curPage = 1;
	pageInfo.pageGroup = 1;
	setPages_span();
	getList();
	$("#pages").find("#next,#last").show();
	$("#pages").find("#first,#prev").hide();
};

var toLastPage = function(){
	pageInfo.curPage = pageInfo.totalPage;
	pageInfo.pageGroup = pageInfo.pageGroupCount;
	setPages_span();
	getList();
	$("#pages").find("#next,#last").hide();
	$("#pages").find("#first,#prev").show();
};

var toPrevPage = function(){
	pageInfo.curPage = pageInfo.curPage - 1;
	setPages_span();
	if(pageInfo.curPage == 2){
		$("#pages").find("#first,#prev").hide();
	}
	else if(pageInfo.curPage == pageInfo.totalPage){
		$("#pages").find("#next,#last").show();
	}
	else{
		$("#pages").find("#first,#prev").show();
		$("#pages").find("#next,#last").show();
	}
	getList();
};

var toNextPage = function(){
	pageInfo.curPage = pageInfo.curPage + 1;
	setPages_span();
	if(pageInfo.curPage == 1){
		$("#pages").find("#first,#prev").show();
	}
	else if(pageInfo.curPage == pageInfo.totalPage-1){
		$("#pages").find("#next,#last").hide();
	}
	else{
		$("#pages").find("#first,#prev").show();
		$("#pages").find("#next,#last").show();
	}
	getList();
};

var to_page = function(p){
	pageInfo.curPage = p;
	setPages_span();
	if(p == 1){
		$("#pages").find("#first,#prev").hide();
	}
	else if(p == pageInfo.totalPage){
		$("#pages").find("#next,#last").hide();
	}
	else{
		$("#pages").find("#first,#prev").show();
		$("#pages").find("#next,#last").show();
	}
	getList();
};

var setPages_span = function(){
	$("#pages").find("#curpage").text(pageInfo.total == 0 ? 0 : pageInfo.curPage);
	$("#pages").find("#totalpage").text(pageInfo.totalPage);
	$("#pages").find("#totalcount").text(pageInfo.total);
};

Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
    if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

var getnowTime = function() {
	var date = new Date(); // 日期对象
	return date.Format("yyyy-MM-dd hh:mm:ss");
};

var date2utc = function(c_date) {
	if (!c_date)
		return "";
	var tempArray = c_date.split("-");
	if (tempArray.length != 3) {
		alert("你输入的日期格式不正确,正确的格式:2000-05-01 02:54:12");
		return 0;
	}
	var dateArr = c_date.split(" ");
	var date = null;
	if (dateArr.length == 2) {
		var yymmdd = dateArr[0].split("-");
		var hhmmss = dateArr[1].split(":");
		date = new Date(yymmdd[0], yymmdd[1] - 1, yymmdd[2], hhmmss[0],
				hhmmss[1], hhmmss[2]);
	} else {
		date = new Date(tempArray[0], tempArray[1] - 1, tempArray[2], 00, 00,
				01);
	}
	return parseInt("" + date.getTime() / 1000);
};

var utc2Date = function(n_utc) {
	if (!n_utc || n_utc == null || n_utc == "null" || n_utc == "无")
		return "";
	var date = new Date();
	date.setTime((parseInt(n_utc) + 8 * 3600) * 1000);
	var s = date.getUTCFullYear() + "-";
	if ((date.getUTCMonth() + 1) < 10) {
		s += "0" + (date.getUTCMonth() + 1) + "-";
	} else {
		s += (date.getUTCMonth() + 1) + "-";
	}
	if (date.getUTCDate() < 10) {
		s += "0" + date.getUTCDate();
	} else {
		s += date.getUTCDate();
	}
	if (date.getUTCHours() < 10) {
		s += " 0" + date.getUTCHours() + ":";
	} else {
		s += " " + date.getUTCHours() + ":";
	}
	if (date.getMinutes() < 10) {
		s += "0" + date.getUTCMinutes() + ":";
	} else {
		s += date.getUTCMinutes() + ":";
	}
	if (date.getUTCSeconds() < 10) {
		s += "0" + date.getUTCSeconds();
	} else {
		s += date.getUTCSeconds();
	}

	return s;
};

var subContent = function(v){
	if(v.length > 26){
		return v.substr(0,26) + "...";
	}
	else{
		return v;
	}
};




















