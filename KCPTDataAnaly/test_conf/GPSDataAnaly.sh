#!/bin/sh

if [ "$JAVA_HOME" = "" ]; then
        JAVA_HOME="/opt/soft/jdk1.6.0_22"
fi

if [ "$LBS_HOME" = "" ]; then
        LBS_HOME="/opt"
fi

#if [ "$XCONF_HOME" = "" ]; then
#	XCONF_HOME="/usr/local/tomcat/webapps/Xconf"
#fi

JAVACMD="$JAVA_HOME/bin/java"
JAVADBG="$JAVA_HOME/bin/java -Xdebug -Xnoagent -Djava.compiler=NONE -Xrunjdwp:transport=dt_socket,address=11001,server=y,suspend=n"
CLASS_HOME=""
SERVICE_CLASS="com.ctfo.analy.DataAnalyMain"
CFG_CLASS="com.lingtu.mcc.traceagent.xconf.SubmitCfg"
SRC_XCONF="${XCONF_HOME}/components/TraceAgent/TraceAgent.xconf"
DST_CONF="${LBS_HOME}/gpsdataanaly/DataAnaly.xml"
CONSOLE_CONF=" -Djava.rmi.server.hostname=10.8.3.163 -Dcom.sun.management.jmxremote.port=9991 -Dcom.sun.management.jmxremote.authenticate=false -Dcom.sun.management.jmxremote.ssl=false "

export LC_ALL=zh_CN

oldCP=$CLASSPATH

unset CLASSPATH
for i in ${LBS_HOME}/gpsdataanaly/*.jar ; do
  if [ "$CLASSPATH" != "" ]; then
    CLASSPATH=${CLASSPATH}:$i
  else
    CLASSPATH=$i
  fi
done

CLASSPATH=${CLASSPATH}:${LBS_HOME}/gpsdataanaly/java/classes

if [ "$CLASS_HOME" != "" ]; then
  for i in ${CLASS_HOME}/* ; do
    if [ "$CLASSPATH" != "" ]; then
      CLASSPATH=${CLASSPATH}:$i
    else
      CLASSPATH=$i
    fi
  done
fi

if [ "$oldCP" != "" ]; then
    CLASSPATH=${CLASSPATH}:${oldCP}
fi

echo ${JAVACMD} -cp $CLASSPATH ${SERVICE_CLASS} -f ${DST_CONF} -start
case "$1" in
	start)
		if [ `whoami` = "root" ]; then
			${JAVACMD} ${CONSOLE_CONF} -Xmx3572m -cp $CLASSPATH ${SERVICE_CLASS} -f ${DST_CONF} -start &
		fi
		;;
	stop)
		if [ `whoami` = "root" ]; then
			${JAVACMD} -cp $CLASSPATH ${SERVICE_CLASS} -f ${DST_CONF} -stop
		fi
		;;
	restart)
		$0 stop
		sleep 3
		$0 start
		;;
	status)
		${JAVACMD} -cp $CLASSPATH ${SERVICE_CLASS} -f ${DST_CONF} -status
		;;
	version)
		${JAVACMD} -cp $CLASSPATH ${SERVICE_CLASS} -f ${DST_CONF} -version
		;;
	submit)
		if [ `whoami` = "root" ]; then
			${JAVACMD} -cp $CLASSPATH ${CFG_CLASS} $2 $3
		fi
		;;
	verify)
		if [ `whoami` = "root" ]; then
			$0 submit -v ${SRC_XCONF}
		fi
		;;
	convert)
		if [ `whoami` = "root" ]; then
			$0 submit -c ${SRC_XCONF}
		fi
		;;
	frcnv)
		if [ `whoami` = "root" ]; then
			$0 submit -f ${SRC_XCONF}
		fi
		;;
	debug)
		if [ `whoami` = "root" ]; then
			echo $CLASSPATH
			${JAVADBG} -cp $CLASSPATH ${SERVICE_CLASS} -f ${DST_CONF}&
		fi
		;;
	*)
		echo "Usage: $0 {start|stop|restart|status|verify|convert|frcnv}"
esac

exit 0
