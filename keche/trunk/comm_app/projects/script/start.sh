#!/bin/sh

LOGIN_NAME=`whoami`
LOGIN_NAME="^"${LOGIN_NAME}"*"

SERVER_STR=`ps -ef | grep $LOGIN_NAME | grep -E '(msg|wasv4|pcc|nodesrv)' | awk -F' ' '{print $1":"$8":"$2}'`

for k in $SERVER_STR
do
   ID=`awk -v var=$k 'BEGIN{print var}' | awk -F: '{print $3}'`
   NAME=`awk -v var=$k 'BEGIN{print var}' | awk -F: '{print $2}'`
   LOGIN_NAME=`awk -v var=$k 'BEGIN{print var}' | awk -F: '{print $1}'`
   if [ $NAME != 'grep' ]; then
   	kill -9 $ID
   	echo "Login_Name:"$LOGIN_NAME" Process:"$NAME" PID:"$ID" Status:" "KILL !!!"
   fi
   sleep 1
done

CURDIR=`pwd`
export MTRANS_PRJ_HOME=$CURDIR
export CTFOBIN_ROOT=$CURDIR
export LD_LIBRARY_PATH=$CTFOBIN_ROOT/lib:$CTFOBIN_ROOT/deps:$LD_LIBRARY_PATH
export PATH=$CTFOBIN_ROOT/bin:$PATH

SERVER_NAME='nodesrv wasv4 msg pccv4 authsrv pushsrv pccroute filemgr'

TEMP=""
PROGRAM_PATH="$CURDIR/bin/"
for i in $SERVER_NAME
do
   TEMP="$PROGRAM_PATH/$i -r$CURDIR"
   echo $TEMP
   $TEMP&
   sleep 1
done
