#!/bin/sh

LBS_HOME=`pwd`
LOGBACK="-Dlogback.configurationFile=$LBS_HOME/logback.xml"
CONFIG="${LBS_HOME}/statusservice.xml"
JVM_LOG_DIR="/logs/statusservice/jvm/"

PIDFILE="$LBS_HOME/PID"
SERVICE_CLASS="com.ctfo.statusservice.core.StatusServiceMain"

if [ "$JAVA_HOME" = "" ]; then
	JAVA_HOME="/opt/soft/jdk1.6.0_22"
fi
DATE_STRING="$(date +'%Y-%m-%d-%H-%M')"
JAVACMD="$JAVA_HOME/bin/java"
export LC_ALL=zh_CN
oldCP=$CLASSPATH
unset CLASSPATH

for i in ${LBS_HOME}/*.jar ; do
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
CLASSPATH=${CLASSPATH}:.:${LBS_HOME}
JVM_LOG="${JVM_LOG_DIR}jvm-${DATE_STRING}.log"

INIT_PARAM="-Xmx2024m -Xms1024m -Xmn584m -XX:PermSize=64m -XX:MaxPermSize=128m -XX:+UseConcMarkSweepGC -XX:+UseCMSCompactAtFullCollection -XX:CMSMaxAbortablePrecleanTime=50 -XX:+CMSClassUnloadingEnabled -XX:+PrintGC -XX:+PrintGCDateStamps -Xloggc:$JVM_LOG"

case "$1" in
	start)
		echo ${JAVACMD} ${LOGBACK} ${INIT_PARAM} -cp $CLASSPATH ${SERVICE_CLASS} -d ${LBS_HOME} start
		if [ -f $PIDFILE ]
                then
                        echo "$PIDFILE exists, Statusservice is already running or crashed."
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
    				echo "Starting Statusservice server..."
				${JAVACMD} $LOGBACK $INIT_PARAM -cp $CLASSPATH ${SERVICE_CLASS} -d ${LBS_HOME} start &
                        else 
                            echo "Current User Init Error! [su lbs]" ; exit 1
                        fi
                fi
                if [ "$?"="0" ]
                then
                        echo "Statusservice is Loading..."
                fi
                ;;
	stop)
                if [ ! -f $PIDFILE ]
                then
                        echo "$PIDFILE exists, Statusservice is not running."
                else
                        PID=$(cat $PIDFILE)
			echo "Statusservice Stopping..."
                        kill -9 $PID & rm -rf $PIDFILE &  echo "kill Statusservice ok!"
			sleep 1
                        while [ -x $PIDFILE ]
                        do
                                echo "Waiting for Statusservice to shutdown..."
                                sleep 1
                        done
                        echo "Statusservice stopped!"
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


