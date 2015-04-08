#!/bin/sh

if [ "$JAVA_HOME" = "" ]; then
        JAVA_HOME="/opt/soft/jdk1.6.0_22"
fi

if [ "$LBS_HOME" = "" ]; then
        LBS_HOME="/home/kcpt"
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
DST_CONF="${LBS_HOME}/gpsdatanaly/DataAnaly.xml"

export LC_ALL=zh_CN

oldCP=$CLASSPATH

unset CLASSPATH
for i in ${LBS_HOME}/gpsdatanaly/*.jar ; do
  if [ "$CLASSPATH" != "" ]; then
    CLASSPATH=${CLASSPATH}:$i
  else
    CLASSPATH=$i
  fi
done

CLASSPATH=${CLASSPATH}:${LBS_HOME}/gpsdatanaly/java/classes

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
		if [ `whoami` = "kcpt" ]; then
			${JAVACMD} -Xmx3572m -cp $CLASSPATH ${SERVICE_CLASS} -f ${DST_CONF} -start &
		fi
		;;
	stop)
		if [ `whoami` = "lbs" ]; then
			${JAVACMD} -cp $CLASSPATH ${SERVICE_CLASS} -f ${DST_CONF} -stop
		fi
		;;
	manual)
            ${JAVACMD} -cp $CLASSPATH ${SERVICE_CLASS} -f ${DST_CONF} -manual
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
		if [ `whoami` = "lbs" ]; then
			${JAVACMD} -cp $CLASSPATH ${CFG_CLASS} $2 $3
		fi
		;;
	verify)
		if [ `whoami` = "lbs" ]; then
			$0 submit -v ${SRC_XCONF}
		fi
		;;
	convert)
		if [ `whoami` = "lbs" ]; then
			$0 submit -c ${SRC_XCONF}
		fi
		;;
	frcnv)
		if [ `whoami` = "lbs" ]; then
			$0 submit -f ${SRC_XCONF}
		fi
		;;
	debug)
		if [ `whoami` = "lbs" ]; then
			echo $CLASSPATH
			${JAVADBG} -cp $CLASSPATH ${SERVICE_CLASS} -f ${DST_CONF}&
		fi
		;;
	*)
		echo "Usage: $0 {start|stop|restart|status|verify|convert|frcnv}"
esac

exit 0
