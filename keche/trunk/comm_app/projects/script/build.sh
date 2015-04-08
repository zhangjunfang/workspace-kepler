#!/bin/sh

CURDIR=`pwd`
echo "export MTRANS_PRJ_HOME=$CURDIR" > $1
echo "export CTFOBIN_ROOT=$CURDIR" >> $1
echo "export LD_LIBRARY_PATH=$CURDIR/lib:\$LD_LIBRARY_PATH" >> $1
echo "export PATH=$CURDIR/bin:\$PATH" >> $1

echo "while true" >> $1
echo "do" >> $1
echo "  stillRunning=\$(ps -ef |grep \"$2\" |grep -v \"grep\")" >> $1
echo "  if [ \"\$stillRunning\" ] ; then" >> $1
echo "    echo \"$2 service was already started\" >> $2.log" >> $1
echo "  else" >> $1
echo "    echo \"$2 service was not started\" >> $2.log" >> $1
echo "    echo \"Starting service ...\" >> $2.log" >> $1
echo "    $CURDIR/bin/$2 -r$CURDIR&" >> $1
echo "  fi" >> $1
echo "  sleep 10" >> $1
echo "done" >> $1
##echo "$CURDIR/$1" >> "/etc/rc.local"

##$CURDIR/$1&