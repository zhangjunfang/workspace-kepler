#!/bin/sh

LOGIN_NAME=`whoami`
LOGIN_NAME="^"${LOGIN_NAME}"*"

SERVER_STR=`ps -ef | grep $LOGIN_NAME | grep -E '(msg|was|pcc)' | awk -F' ' '{print $1":"$8":"$2}'`

for k in $SERVER_STR
do
   ID=`awk -v var=$k 'BEGIN{print var}' | awk -F: '{print $3}'`
   NAME=`awk -v var=$k 'BEGIN{print var}' | awk -F: '{print $2}'`
   LOGIN_NAME=`awk -v var=$k 'BEGIN{print var}' | awk -F: '{print $1}'`
   if [ $NAME != 'grep' ]; then 
   		kill -9 $ID
   fi
   echo "Login_Name:"$LOGIN_NAME" Process:"$NAME" PID:"$ID" Status:" "KILL !!!"
   sleep 1

done
