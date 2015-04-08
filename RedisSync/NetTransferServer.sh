#!/bin/bash

#######################################################################
# 服务启动脚本模板
# by jiangzhongming@ctfo.com
# 2012-06-28
#######################################################################

CUR_DIR=`pwd`
LIB_PATH="$CUR_DIR/lib"
CLASSPATH="$CLASSPATH:$CUR_DIR/bin"

LANG="zh_CN.UTF-8"

JAVA_OPS="-XX:MaxPermSize=128m -XX:-DisableExplicitGC"

APP_PACKAGE_NAME="com.ctfo.redissync.app.NetTransformMain"

export LANG

function startServer() {
	APP_PID=`jps -l | grep $APP_PACKAGE_NAME | cut -d' ' -f 1`
	if [ -n "$APP_PID" ]; then
		echo "Server already started !!  PID: $APP_PID    #_#"
		exit 1
	fi
	
	for _jar in $LIB_PATH/*.jar; do
		CLASSPATH=$CLASSPATH:${_jar}
	done

	#java $JAVA_OPS -cp $CLASSPATH $APP_PACKAGE_NAME >> console.log 2>&1 &
	java $JAVA_OPS -cp $CLASSPATH $APP_PACKAGE_NAME 2>&1 1>/dev/null &
	echo "redis sync server start successful   ^_^"
}

function stopServer() {
	APP_PID=`jps -l | grep $APP_PACKAGE_NAME | cut -d' ' -f 1`
	if [ -n "$APP_PID" ]; then
		echo "$APP_PID process is killed    *_*"
		kill -9 $APP_PID
	fi
}

function restartServer() {
	stopServer
	sleep 1
	startServer
}

function usage() {
	echo "Usage: $0 [start | stop | restart]    @_@"
	exit 1
}

if [  $# -ne 1  ]; then
	usage
fi

case $1 in
	start) startServer;;
	stop) stopServer;;
	restart) restartServer;;
	*) usage;;
esac
