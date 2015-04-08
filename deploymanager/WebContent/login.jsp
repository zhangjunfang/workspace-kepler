<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title>自动部署系统--登陆</title>
<meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<meta name="description" content="">
<meta name="author" content="">

<link rel="stylesheet" type="text/css" href="lib/bootstrap/css/bootstrap.css">
<link rel="stylesheet" type="text/css" href="lib/bootstrap/css/bootstrap-responsive.css">
<link rel="stylesheet" type="text/css" href="stylesheets/theme.css">
<link rel="stylesheet" href="lib/font-awesome/css/font-awesome.css">
<script src="lib/jquery-1.8.1.min.js" type="text/javascript"></script>
<!-- <script type="text/javascript" src="lib/jqplot/jquery.jqplot.min.js"></script> -->
<!-- <script type="text/javascript" charset="utf-8" src="javascripts/graphDemo.js"></script> -->

<%-- Demo page code --%>

<style type="text/css">
#line-chart {
	height: 300px;
	width: 800px;
	margin: 0px auto;
	margin-top: 1em;
}

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

#accountInfo,#passInfo{color:red;}
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

<script type="text/javascript">
	$(document).ready(function(){
		$("#loginInfoForm").find("input[name=userId]").trigger("focus");
		$("#loginInfoForm").find("#loginlink").click(function(){
			var uname = $("#loginInfoForm").find("input[name=userId]").val();
			var passwd = $("#loginInfoForm").find("input[name=password]").val();
			if(uname == ""){
				alert("请输入用户名!");
				return;
			}
			if(passwd == ""){
				alert("请输入密码!");
				return;
			}
			
			$.ajax({ 
				type:"POST",
				url:"login.do",
				data:{userId:uname, password:passwd},
				success:function(data){
					if(data == ""){
						$("#accountInfo").text("用户名不存在");
					}
					else if(data == "freeze"){
						$("#passInfo").text("密码错误");
					}
					else if(data == "online"){
						$("#accountInfo").text("用户已登录");
					}
					else{
						window.location.href = "index.jsp";
					}
				}
			});
		});
		
		$("#loginInfoForm").find("input[name=userId],input[name=password]").focus(function(){
			$("#accountInfo").text("");
			$("#passInfo").text("");
		});
		
	});
</script>
</head>

<%--[if lt IE 7 ]> <body class="ie ie6"> <![endif]--%>
<%--[if IE 7 ]> <body class="ie ie7"> <![endif]--%>
<%--[if IE 8 ]> <body class="ie ie8"> <![endif]--%>
<%--[if IE 9 ]> <body class="ie ie9"> <![endif]--%>
<%--[if (gt IE 9)|!(IE)]><%--%>
<body>
	<%--<![endif]--%>

	<div class="navbar">
		<div class="navbar-inner">
			<div class="container-fluid">
				<ul class="nav pull-right">

				</ul>
				<a class="brand" href="javascript:void(0)"><span class="second">部署管理平台</span></a>
			</div>
		</div>
	</div>
	<div class="row-fluid">
		<div class="dialog span4">
			<div class="block">
				<div class="block-heading">用户登录</div>
				<div class="block-body">
					<form id="loginInfoForm">
						<label>账号<span id="accountInfo" style="float: right;"></span></label>
						<input type="text" name="userId" value="gemin" class="span12">
					    <label>密码<span id="passInfo" style="float: right;"></span></label>
					    <input type="password" name="password" value="123456" class="span12">
					    <a id="loginlink" href="javascript:void(0)" class="btn btn-primary pull-right">登录</a>
						<label class="remember-me"><input type="checkbox"> 记住账号</label>
						<div class="clearfix"></div>
					</form>
				</div>
			</div>
		</div>
	</div>

	<script src="lib/bootstrap/js/bootstrap.js"></script>
</body>
</html>