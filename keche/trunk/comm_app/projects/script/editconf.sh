#! /bin/sh

if [ "$1" == "" ]; then
	echo "usage: [source string] [dest string]"
	exit    
fi

CURDIR=`pwd`
BIN_NAME='pccv4 wasv4 msg nodesrv sendsrv mpas mppas authsrv pcc_fujian pccroute pcc_jiangsu pushsrv pcc_shanghai vacftp filesrv filemgr'

edit_conf()
{
        for i in $BIN_NAME
        do
                DIRFILE="$1/conf/ws/$i.conf"
                if [ -f $DIRFILE ]; then
                        sed -i -e "s#$2#$3#" $DIRFILE
                        dos2unix $DIRFILE
                fi
        done
}
edit_conf $CURDIR $1 $2
echo "success"
