if [ "$JAVA_HOME" = "" ]; then
	JAVA_HOME="/usr/java/jdk1.6.0_45"
fi

if [ "$LBS_HOME" = "" ]; then
	LBS_HOME="/opt"
fi

DATE_STRING="$(date +'%Y-%m-%d')"
JAVACMD="$JAVA_HOME/bin/java"
CLASS_HOME=""
SERVICE_CLASS="com.ctfo.trackservice.core.StatusStatistics"
 
export LC_ALL=zh_CN

oldCP=$CLASSPATH

unset CLASSPATH
for i in ${LBS_HOME}/statusstatistics/lib/*.jar ; do
  if [ "$CLASSPATH" != "" ]; then
    CLASSPATH=${CLASSPATH}:$i
  else
    CLASSPATH=$i
  fi
done
LOGBACK=${LBS_HOME}/statusstatistics/conf/logback.xml

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

JVM_LOG_DIR="${LBS_HOME}/statusstatistics/logs/"
JVM_LOG="${JVM_LOG_DIR}jvm.${DATE_STRING}.log"

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
		if [ `whoami` = "lbs" ]; then
			${JAVACMD} -Dlogback.configurationFile=$LOGBACK -Xmx3550m -Xms3550m -Xmn2g -XX:PermSize=128m -XX:MaxPermSize=256m -XX:+UseConcMarkSweepGC -XX:+UseCMSCompactAtFullCollection -XX:CMSMaxAbortablePrecleanTime=50 -XX:+CMSClassUnloadingEnabled -XX:+PrintGC -XX:+PrintGCDateStamps -Xloggc:$JVM_LOG -cp $CLASSPATH ${SERVICE_CLASS} -d /opt/statusstatistics/conf start & 
		fi
		;;
	restore)
		if [ `whoami` = "lbs" ]; then
			${JAVACMD} -Dlogback.configurationFile=$LOGBACK -Xmx3550 -Xms3550m -Xmn2g -XX:PermSize=128m -XX:MaxPermSize=256m -XX:+UseConcMarkSweepGC -XX:+UseCMSCompactAtFullCollection -XX:CMSMaxAbortablePrecleanTime=50 -XX:+CMSClassUnloadingEnabled -XX:+PrintGC -XX:+PrintGCDateStamps -Xloggc:$JVM_LOG -cp $CLASSPATH ${SERVICE_CLASS} -d /opt/statusstatistics/conf restore
		fi
		;;
	autoRestore)
		if [ `whoami` = "lbs" ]; then
			${JAVACMD} -Dlogback.configurationFile=$LOGBACK -Xmx3550m -Xms3550m -Xmn2g -XX:PermSize=128m -XX:MaxPermSize=256m -XX:+UseConcMarkSweepGC -XX:+UseCMSCompactAtFullCollection -XX:CMSMaxAbortablePrecleanTime=50 -XX:+CMSClassUnloadingEnabled -XX:+PrintGC -XX:+PrintGCDateStamps -Xloggc:$JVM_LOG -cp $CLASSPATH ${SERVICE_CLASS} -d /opt/statusstatistics/conf autoRestore &
		fi
		;;
	*)
		echo "Usage: $0 {start}"
esac

exit 0

