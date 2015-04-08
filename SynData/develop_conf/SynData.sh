#!/bin/sh


date
echo "start sysdata.sh"
#JAVA_HOME=/opt/soft/jdk1.6.0_22
JAVACMD="$JAVA_HOME/bin/java"
JAVADBG="$JAVA_HOME/bin/java -Xdebug -Xnoagent -Djava.compiler=NONE -Xrunjdwp:transport=dt_socket,address=11001,server=y,suspend=n"
CONF_DIR=$1
 
APP_CLASS="com.ctfo.syn.kcpt_oracle2mysql.SynMain"
 
export LC_ALL=zh_CN
 
oldCP=$CLASSPATH
 
unset CLASSPATH
for i in `dirname $0`/*.jar ; do
  if [ "$CLASSPATH" != "" ]; then
    CLASSPATH=${CLASSPATH}:$i
  else
    CLASSPATH=$i
  fi
done
 
 
if [ "$oldCP" != "" ]; then
    CLASSPATH=${CLASSPATH}:${oldCP}
fi
 
echo `dirname $0`:$CLASSPATH 
 
                exec ${JAVACMD} -Xmx1024m -cp `dirname $0`:$CLASSPATH ${APP_CLASS} /home/kcpt/supp_app/syndata/syndata.properties & 

exit 0
