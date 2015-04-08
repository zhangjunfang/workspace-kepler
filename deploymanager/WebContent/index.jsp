<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title>自动部署系统</title>
<meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<meta name="description" content="">
<meta name="author" content="">

<link rel="stylesheet" type="text/css" href="lib/bootstrap/css/bootstrap.css">
<link rel="stylesheet" type="text/css" href="lib/bootstrap/css/bootstrap-responsive.css">
<link rel="stylesheet" type="text/css" href="stylesheets/theme.css">
<link rel="stylesheet" href="lib/font-awesome/css/font-awesome.css">

<script src="lib/jquery-1.8.1.min.js" type="text/javascript"></script>

<%-- Demo page code --%>

<style type="text/css">
.brand {
	font-family: georgia, serif;
}

.brand .first {
	color: #ccc;
	font-style: italic;
}

.brand .second {
	color: #fff;
	font-weight: bold;
}
</style>

<%-- Le HTML5 shim, for IE6-8 support of HTML5 elements --%>
<%--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]--%>

<%-- Le fav and touch icons --%>
<link rel="shortcut icon" href="../assets/ico/favicon.ico">
<link rel="apple-touch-icon-precomposed" sizes="144x144" href="../assets/ico/apple-touch-icon-144-precomposed.png">
<link rel="apple-touch-icon-precomposed" sizes="114x114" href="../assets/ico/apple-touch-icon-114-precomposed.png">
<link rel="apple-touch-icon-precomposed" sizes="72x72" href="../assets/ico/apple-touch-icon-72-precomposed.png">
<link rel="apple-touch-icon-precomposed" href="../assets/ico/apple-touch-icon-57-precomposed.png">
<script type="text/javascript" src="lib/operatorInfo.js"></script>
<script type="text/javascript" src="lib/util.js"></script>
<script type="text/javascript">
	var editObj = null;
	$(document).ready(function(){
		if(!<%=session.getAttribute("opId") %>){
			window.location.href = "login.jsp";
		}
		operator.opId = "<%=session.getAttribute("opId") %>";
		operator.opName = "<%=session.getAttribute("opName") %>";
		operator.roleId = "<%=session.getAttribute("roleId") %>";
		operator.realName = "<%=session.getAttribute("realName") %>";
		operator.funs = "";
		
		$("body").width($(window).width());
		$("#opNameShow").text(operator.realName);
		$("#mainRight").load("rightHome.html");
		//$("#mainDiv").load("index.html");
		
		$("#menucontainer").find("li").click(function(){
			$("#menucontainer").find("li").removeClass();
			$(this).addClass("active");
		});
		
		$("#btnSure").click(function(){
			if(operateType == 0){ toAdd(); }
			else if(operateType == 1){ toEdit(); }
			else if(operateType == 2){ toDelete(); }
		});
		$("#alertInfo").css({
			"width" : "160px",
			"margin-left" : "-5%"
		});

		////////////////////////////////////////
	});
	
	var currentPage = "rightHome";
	function toMainRight(page, obj) {
		if(obj){
			editObj = obj;
		}
		if(currentPage != page){
			currentPage = page;
			$("#mainRight").load(page + ".html");
		}
	}
	
	function logout(){
		tip_div.tipTitle = "退出程序";
		tip_div.tipContent = "确定退出么？";
		setTipDivContent();
		$("#common_a").trigger("click");
		$("#myModal").find("#btnSure").click(function(){
			$.ajax({ 
				type:"POST",
				url:"logout.do",
				data:{},
				success:function(data){
					if(data == "exit"){
						window.location.href = "login.jsp";
					}
					else{
						
					}
				}
			});
		});
	}
</script>
</head>

<%--[if lt IE 7 ]> <body class="ie ie6"> <![endif]--%>
<%--[if IE 7 ]> <body class="ie ie7"> <![endif]--%>
<%--[if IE 8 ]> <body class="ie ie8"> <![endif]--%>
<%--[if IE 9 ]> <body class="ie ie9"> <![endif]--%>
<%--[if (gt IE 9)|!(IE)]><%--%>
<body style="overflow-x:hidden;">
	
	<div class="navbar">
		<div class="navbar-inner">
			<div class="container-fluid">
				<ul class="nav navbar-nav pull-right">
					<li><a href="javascript:void(0)" onclick="toMainRight('rightPlatFormList')"><i class="icon-home"></i>平台管理</a></li>
					<li><a href="javascript:void(0)" onclick="toMainRight('rightServerList')"><i class="icon-sitemap"></i>服务器管理</a></li>
					<li><a href="javascript:void(0)" onclick="toMainRight('rightServiceList')"><i class="icon-globe"></i>服务管理</a></li>
					<li><a href="javascript:void(0)" onclick="toMainRight('rightVersionList')"><i class="icon-book"></i>版本管理</a></li>

					<li id="fat-menu" class="dropdown"><a href="#" id="drop3"
						role="button" class="dropdown-toggle" data-toggle="dropdown">
							<i class="icon-cog"></i>系统管理 <i class="icon-caret-down"></i>
					</a>

						<ul class="dropdown-menu">
							<li><a tabindex="-1" href="javascript:void(0)" onclick="toMainRight('rightUserList')"><i
									class="icon-user"></i>用户管理</a></li>
							<li class="divider"></li>
							<li><a tabindex="-1" href="javascript:void(0)" onclick="toMainRight('rightRoleList')"><i
									class="icon-group"></i>角色管理</a></li>
						</ul></li>
					<li id="fat-menu" class="dropdown"><a href="#" id="drop3"
						role="button" class="dropdown-toggle" data-toggle="dropdown">
							<i class="icon-user"></i> <span id="opNameShow">杨晓光</span> <i
							class="icon-caret-down"></i>
					</a>

						<ul class="dropdown-menu">
							<li><a tabindex="-1" href="javascript:void(0)"><i class="icon-pencil"></i>修改密码</a></li>
							<li class="divider"></li>
							<li><a tabindex="-1" href="javascript:void(0)" onclick="logout()"><i
									class="icon-signout"></i>退出系统</a></li>
						</ul></li>

				</ul>
				<a class="brand" href="javascript:void(0)" onclick="toMainRight('rightHome')"><span
					class="second">部署管理平台</span></a>
			</div>
		</div>
	</div>


	<div class="container-fluid">

		<div class="row-fluid">
			<div class="span3">
				<div id="menucontainer" class="sidebar-nav">
					<div class="nav-header" data-toggle="collapse"
						data-target="#dashboard-menu">
						<i class="icon-home"></i>平台管理
					</div>
					<ul id="dashboard-menu" class="nav nav-list collapse in">
						<li><a href="javascript:void(0)" onclick="toMainRight('rightHome')">主页</a></li>
						<li><a href="javascript:void(0)" onclick="toMainRight('rightUserList')">用户管理</a></li>
						<li><a href="javascript:void(0)" onclick="toMainRight('rightRoleList')">角色管理</a></li>
						<li><a href="javascript:void(0)" onclick="toMainRight('rightPlatFormList')">平台管理</a></li>
						<li><a href="javascript:void(0)" onclick="toMainRight('rightProjectList')">项目管理</a></li>
						<li><a href="javascript:void(0)" onclick="toMainRight('rightServerList')">服务器管理</a></li>
						<li><a href="javascript:void(0)" onclick="toMainRight('rightServiceList')">服务管理</a></li>
						<li><a href="help.html">Help</a></li>
					</ul>
					<div class="nav-header" data-toggle="collapse"
						data-target="#accounts-menu">
						<i class="icon-sitemap"></i>服务器管理<span class="label label-info">+10</span>
					</div>
					<ul id="accounts-menu" class="nav nav-list collapse in">
						<li><a href="sign-in.html">Sign In</a></li>
						<li><a href="sign-up.html">Sign Up</a></li>
						<li><a href="reset-password.html">Reset Password</a></li>
					</ul>

					<div class="nav-header" data-toggle="collapse"
						data-target="#settings-menu">
						<i class="icon-globe"></i>服务管理
					</div>
					<ul id="settings-menu" class="nav nav-list collapse in">
						<li><a href="403.html">403 page</a></li>
						<li><a href="404.html">404 page</a></li>
						<li><a href="500.html">500 page</a></li>
						<li><a href="503.html">503 page</a></li>
					</ul>

					<div class="nav-header" data-toggle="collapse"
						data-target="#legal-menu">
						<i class="icon-book"></i>版本管理
					</div>
					<ul id="legal-menu" class="nav nav-list collapse in">
						<li><a href="privacy-policy.html">Privacy Policy</a></li>
						<li><a href="terms-and-conditions.html">Terms and
								Conditions</a></li>
					</ul>
					<div class="nav-header" data-toggle="collapse"
						data-target="#legal-menu">
						<i class="icon-user"></i>用户管理
					</div>
					<ul id="legal-menu" class="nav nav-list collapse in">
						<li><a href="privacy-policy.html">Privacy Policy</a></li>
						<li><a href="terms-and-conditions.html">Terms and
								Conditions</a></li>
					</ul>
					<div class="nav-header" data-toggle="collapse"
						data-target="#legal-menu">
						<i class="icon-group"></i>角色管理
					</div>
					<ul id="legal-menu" class="nav nav-list collapse in">
						<li><a href="privacy-policy.html">Privacy Policy</a></li>
						<li><a href="terms-and-conditions.html">Terms and
								Conditions</a></li>
					</ul>
				</div>
			</div>
			<div id="mainRight" class="span9">


				<!-- <div class="stats">
    <p class="stat"><span class="number">53</span>tickets</p>
    <p class="stat"><span class="number">27</span>tasks</p>
    <p class="stat"><span class="number">15</span>waiting</p>
</div>
<h1 class="page-title">Dashboard</h1> -->


			</div>
		</div>
	</div>


	<footer>
		<hr>
		<!-- Purchase a site license to remove this link from the footer: http://www.portnine.com/bootstrap-themes -->
		<center>
		<p>
			A <a href="http://www.portnine.com/bootstrap-themes" target="_blank">Free
				Bootstrap Theme</a> by <a href="http://www.portnine.com" target="_blank">Portnine</a>
		</p>
		<p>
			&copy; 2012 <a href="http://www.portnine.com">Portnine</a>
		</p>
		</center>
	</footer>
	<div class="modal small hide fade" id="myModal" tabindex="-1"role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-header">
			<button id="btnCancelx" type="button" class="close" data-dismiss="modal"ria-hidden="true">×</button>
			<h3 id="myModalLabel">Delete Confirmation</h3>
		</div>
		<div class="modal-body">
			<p class="error-text">
				<i class="icon-warning-sign modal-icon"></i>
				<span>Are you sure you want to delete the user?</span>
			</p>
		</div>
		<div class="modal-footer">
			<button id="btnSure" class="btn btn-danger" data-dismiss="modal">确定</button>
			<button id="btnCancel" class="btn" data-dismiss="modal" aria-hidden="true">取消</button>
		</div>
	</div>
	
	<div class="modal small hide fade" id="alertInfo" tabindex="-1"role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-header">
			<h3 id="myModalLabel">　</h3>
		</div>
		<div class="modal-body">
			<p class="error-text">
				<span></span>
			</p>
		</div>
		<div class="modal-footer">
			<button id="alertSure" data-dismiss="modal" >确定</button>
		</div>
	</div>
	<a id="common_a" href="#myModal" role="button" data-toggle="modal" style="display: none;"><i class="icon-remove"></i></a>
	<a id="common_alert" href="#alertInfo" role="button" data-toggle="modal" style="display: none;"><i class="icon-remove"></i></a>
	<script src="lib/bootstrap/js/bootstrap.js"></script>
</body>
</html>