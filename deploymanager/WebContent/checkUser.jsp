<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title></title>
<meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<meta name="description" content="">
<meta name="author" content="">

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
		
		$("#btnSure").click(function(){
			if(operateType == 0){ toAdd(); }
			else if(operateType == 1){ toEdit(); }
			else if(operateType == 2){ toDelete(); }
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
</body>
</html>