#!/bin/sh

if [ "$JAVA_HOME" = "" ]; then
	JAVA_HOME="/usr/java/jdk1.6.0_31"
fi

if [ "$LBS_HOME" = "" ]; then
	LBS_HOME="/opt/chpt/supp_app"
fi

DATE_STRING="$(date +'%Y-%m-%d-%H-%M')"
JAVACMD="$JAVA_HOME/bin/java"
CLASS_HOME=""
SERVICE_CLASS="com.ctfo.storage.core.SspDataMain"
 
export LC_ALL=zh_CN

oldCP=$CLASSPATH

unset CLASSPATH
for i in ${LBS_HOME}/sspdispatchservice/lib/*.jar ; do
  if [ "$CLASSPATH" != "" ]; then
    CLASSPATH=${CLASSPATH}:$i
  else
    CLASSPATH=$i
  fi
done
LOGBACK=${LBS_HOME}/sspdispatchservice/conf/logback.xml

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

JVM_LOG_DIR="/opt/chpt/supp_app/sspdispatchservice/logs/"
JVM_LOG="${JVM_LOG_DIR}jvm-${DATE_STRING}.log"

if [ -d "$JVM_LOG_DIR" ] 
then 
    echo "JVM_LOG_DIR=${JVM_LOG_DIR}"    
else
    mkdir $JVM_LOG_DIR
fi

if [ -f "$JVM_LOG" ] 
then
   echo "JVM_LOG=${JVM_LOG}"
else    
   touch $JVM_LOG
fi

echo ${JAVACMD} ${LOGBACK}  -XX:+PrintGC -XX:+PrintGCDateStamps -Xloggc:$JVM_LOG  -cp $CLASSPATH ${SERVICE_CLASS} -start

case "$1" in
	start)
		if [ `whoami` = "root" ]; then
			${JAVACMD} -Dlogback.configurationFile=$LOGBACK -Xmx1512m -Xms1512m -Xmn784m -XX:PermSize=128m -XX:MaxPermSize=256m -XX:+UseConcMarkSweepGC -XX:+UseCMSCompactAtFullCollection -XX:CMSMaxAbortablePrecleanTime=50 -XX:+CMSClassUnloadingEnabled -XX:+PrintGC -XX:+PrintGCDateStamps -Xloggc:$JVM_LOG -cp $CLASSPATH ${SERVICE_CLASS} -d  conf -start &
		fi
		;;
	*)
		echo "Usage: $0 {start}"
esac

exit 0
