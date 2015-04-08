#!/bin/sh

LBS_HOME=`pwd`
LOGBACK="-Dlogback.configurationFile=$LBS_HOME/conf/logback.xml"
CONFIG="${LBS_HOME}/conf/config.xml"
PROPERTIES="${LBS_HOME}/conf/system.properties"
JVM_LOG_DIR="/logs/alarmstatistics/jvmlog/"

PIDFILE="$LBS_HOME/PID"
SERVICE_CLASS="com.ctfo.statistics.alarm.core.AlarmStatisticsMain"

if [ "$JAVA_HOME" = "" ]; then
	JAVA_HOME="/opt/soft/jdk1.6.0_22"
fi
DATE_STRING="$(date +'%Y-%m-%d-%H-%M')"
JAVACMD="$JAVA_HOME/bin/java"
export LC_ALL=zh_CN
oldCP=$CLASSPATH
unset CLASSPATH

for i in ${LBS_HOME}/lib/*.jar ; do
  if [ "$CLASSPATH" != "" ]; then
    CLASSPATH=${CLASSPATH}:$i
  else
    CLASSPATH=$i
  fi
done
CLASS_HOME=""
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

CLASSPATH=${CLASSPATH}:${LBS_HOME}
JVM_LOG="${JVM_LOG_DIR}jvm-${DATE_STRING}.log"

INIT_PARAM="-Xmx3048m -Xms3048m -XX:NewRatio=2 -XX:PermSize=64m -XX:MaxPermSize=128m -XX:+UseConcMarkSweepGC -XX:+UseCMSCompactAtFullCollection -XX:CMSMaxAbortablePrecleanTime=50 -XX:+CMSClassUnloadingEnabled -XX:+PrintGC -XX:+PrintGCDateStamps -Xloggc:$JVM_LOG"

case "$1" in
	start)
#		echo ${JAVACMD} ${LOGBACK} ${INIT_PARAM} -cp $CLASSPATH ${SERVICE_CLASS} -d ${CONFIG} ${PROPERTIES} start
		if [ -f $PIDFILE ]
		then
			echo "$PIDFILE exists, AlarmStatistics is already running or crashed."
		else
			if [ `whoami` = "lbs" ]; then
                if [ -d "$JVM_LOG_DIR" ]; then
			        echo "JVM_LOG_DIR=${JVM_LOG_DIR}"    
				else
				    mkdir -p $JVM_LOG_DIR
				fi
				if [ -f "$JVM_LOG" ]; then
					echo "JVM_LOG=${JVM_LOG}"
				else
				   	touch $JVM_LOG
					echo "Create JVM_LOG ${JVM_LOG}"
				fi
				echo "Starting AlarmStatistics server..."
				${JAVACMD} $LOGBACK $INIT_PARAM -cp $CLASSPATH ${SERVICE_CLASS} -d ${CONFIG} ${PROPERTIES} start &
            fi
        fi
        if [ "$?"="0" ]
        then
            echo "AlarmStatistics initialized!"
        fi
		;;
	stop)
		if [ ! -f $PIDFILE ]
		then
			echo "$PIDFILE exists, AlarmStatistics is not running."
        else
			echo "AlarmStatistics Stopping..."
			P_ID=`jps | grep AlarmStatistics | cut -d ' ' -f 1`
		    if [ -z $P_ID ];then
				echo "AlarmStatistics not running!"                 
        	else
				kill -9 $P_ID & echo "kill AlarmStatistics ok!"
			fi
			sleep 3
			rm -rf $PIDFILE
			while [ -x $PIDFILE ]
			do
				echo "Waiting for AlarmStatistics to shutdown..."
				sleep 1
			done
			echo "AlarmStatistics stopped!"                
		fi
        ;;
	restart)
		${0} stop 
		sleep 1
		${0} start
		;;	
	*)
		echo "Usage: $0 {start|stop|restart}"
esac
exit 0



