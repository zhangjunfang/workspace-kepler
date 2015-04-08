#!/bin/sh

if [ "$1" == "" ]; then
echo "no start process"
exit
fi

BIN_NAME="$1"
LOGIN_NAME=`whoami`
LOGIN_NAME="^"${LOGIN_NAME}"*"

SERVER_STR=`ps -ef | grep $LOGIN_NAME | grep $BIN_NAME | awk -F' ' '{print $1":"$8":"$2}'`

for k in $SERVER_STR
do
   ID=`awk -v var=$k 'BEGIN{print var}' | awk -F: '{print $3}'`
   NAME=`awk -v var=$k 'BEGIN{print var}' | awk -F: '{print $2}'`
   LOGIN_NAME=`awk -v var=$k 'BEGIN{print var}' | awk -F: '{print $1}'`
   if [ $NAME != '/bin/sh' -a $NAME != 'grep' ]; then 
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

TEMP="$CURDIR/bin/$BIN_NAME -r$CURDIR"
echo $TEMP
$TEMP&




