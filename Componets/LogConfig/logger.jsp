<%@ page
	import="java.util.*,org.apache.log4j.LogManager,org.apache.log4j.Logger,org.apache.log4j.Level"
	pageEncoding="UTF-8"%>
<%
	String path = request.getContextPath();
	String basePath = request.getScheme() + "://" + request.getServerName() + ":" + request.getServerPort()
			+ path + "/";
	String thisLevel = "NONE";
	StringBuffer outputMessage = new StringBuffer();
	boolean showLoggerList = true;
	StringBuffer namesOption = null;
	if ("POST".equals(request.getMethod())) {
		//Perform the operation to the logger
		showLoggerList = false;
		String[] names = request.getParameterValues("loggerName");
		thisLevel = request.getParameter("loggerLevel");
		if ((names != null) && (thisLevel != null)) {
			Logger logger = null;
			for (String name : names) {
				logger = Logger.getLogger(name);
				logger.setLevel(Level.toLevel(thisLevel));
				outputMessage.append(name).append("<BR />");
			}
		}
	} else {
		//Generate a list of all the loggers and levels
		ArrayList<String> al = new ArrayList<String>();
		HashMap<String, Logger> hm = new HashMap<String, Logger>();
		//GetRootLogger
		Logger rootLogger = LogManager.getRootLogger();
		String rootLoggerName = rootLogger.getName();
		al.add(rootLoggerName);
		hm.put(rootLoggerName, rootLogger);
		//
		String query = request.getParameter("q");
		boolean list = (query != null && query.length() > 0);
		//All Other Loggers
		Enumeration<Logger> e = LogManager.getCurrentLoggers();
		while (e.hasMoreElements()) {
			Logger t1Logger = (Logger) e.nextElement();
			String loggerName = t1Logger.getName();
			al.add(loggerName);
			hm.put(loggerName, t1Logger);
			if (list && loggerName.indexOf(query) > -1) {
				out.write(loggerName);
				out.write("<br />");
			}
		}
		//
		if (list)
			return;
		//
		String[] alLoggerStr = ((String[]) al.toArray(new String[0]));
		Arrays.sort(alLoggerStr);
		namesOption = new StringBuffer("<SELECT NAME='loggerName' MULTIPLE SIZE='30' style='width: 100%'>");
		for (String name : alLoggerStr) {
			Logger t2Logger = (Logger) hm.get(name);
			String t2LoggerName = t2Logger.getName();
			String t2LoggerLevel = t2Logger.getEffectiveLevel().toString();
			String thisParent = "";
			if (t2Logger.getParent() != null) {
				thisParent = t2Logger.getParent().getName();
			}
			namesOption.append("<OPTION VALUE='").append(t2LoggerName).append("'>" + t2LoggerName).append(
					" [" + t2LoggerLevel + "] -> ").append(thisParent + "</OPTION>");
		}
		namesOption.append("</SELECT>");
	}
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<%
	if (showLoggerList) {
%>
<html>
	<head>
		<base href="<%=basePath%>">
		<title>Logger Manager</title>
		<meta http-equiv="pragma" content="no-cache">
		<meta http-equiv="cache-control" content="no-cache">
		<meta http-equiv="expires" content="0">
	</head>
	<body>
		<fieldset>
			<legend>
				User-Defined Log Set
			</legend>
			<form method="post">
				<input id="loggerName" name="loggerName" value=""
					style="width: 400px" />
				<SELECT NAME='loggerLevel'>
					<OPTION VALUE="ALL">
						All
					</OPTION>
					<OPTION VALUE="DEBUG">
						Debug
					</OPTION>
					<OPTION VALUE="INFO">
						Info
					</OPTION>
					<OPTION VALUE="WARN">
						Warn
					</OPTION>
					<OPTION VALUE="ERROR">
						Error
					</OPTION>
					<OPTION VALUE="FATAL">
						Fatal
					</OPTION>
					<OPTION VALUE="OFF">
						Off
					</OPTION>
				</SELECT>
				<INPUT TYPE="Submit" NAME='Submit' VALUE='Update'>
				<br />
			</form>
		</fieldset>
		<br />
		<fieldset>
			<legend>
				Log List
			</legend>
			<span>Please set the logger Level </span>
			<p>
				<form method="post">
					<TABLE CELLPADDING="5" CELLSPACING="0" BORDER="1" WIDTH="100%">
						<TR>
							<TD COLSPAN="2">
								<H2>
									Notice
								</H2>
							</TD>
						</TR>
						<TR>
							<TD WIDTH="70%">
								Choose Logger:
								<BR />
								Format: LoggerClass [Current Level] -- Parent Logger
								<BR />
								<%=namesOption%>
							</TD>
							<TD>
								Choose Level:
								<BR />
								<SELECT NAME='loggerLevel' style="width: 140px">
									<OPTION VALUE="ALL">
										All
									</OPTION>
									<OPTION VALUE="DEBUG">
										Debug
									</OPTION>
									<OPTION VALUE="INFO">
										Info
									</OPTION>
									<OPTION VALUE="WARN">
										Warn
									</OPTION>
									<OPTION VALUE="ERROR">
										Error
									</OPTION>
									<OPTION VALUE="FATAL">
										Fatal
									</OPTION>
									<OPTION VALUE="OFF">
										Off
									</OPTION>
								</SELECT>
								&nbsp;&nbsp;
								<INPUT TYPE="Submit" NAME='Submit' VALUE='Apply Changes' />
							</TD>
						</TR>
						<TR>
							<TD COLSPAN="2">
								<BR>
								<label>
									Log管理 by -- Alex
									<br />
									EMAIL:> j.zhongming@gmail.com
							</TD>
						</TR>
					</TABLE>
				</form>
			</p>
		</fieldset>
	</body>
</html>
<%
	} else {
%>
<html>
	<head>
		<base href="<%=basePath%>">
		<title>Logger Setup - Results</title>
		<meta http-equiv="pragma" content="no-cache">
		<meta http-equiv="cache-control" content="no-cache">
		<meta http-equiv="expires" content="0">
	</head>
	<body>
		Please choose the logger and the level:
		<FORM METHOD="Post">
			<TABLE CELLPADDING="0" CELLSPACING="0" BORDER="0">
				<TR>
					<TD COLSPAN="2">
						<H2>
							Enable Disable Logger
						</H2>
					</TD>
				</TR>
				<TR>
					<TD>
						The following Logger's were set to
						<%=thisLevel%>
						level:
						<BR>
						<%=outputMessage%></TD>
				</TR>
				<TR>
					<TD>
						<A HREF="<%=basePath%>logger.jsp">Return to list</A>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</html>
<%
	}
%>