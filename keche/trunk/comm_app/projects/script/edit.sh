#! /bin/sh

INDIR=$1
CURDIR=`pwd`
if [ $INDIR == $CURDIR ]; then
	echo "install direction is same source direction"
	exit
fi

NOWTIME=`date +%y%m%d%H%M%S`
BIN_NAME="pccv4 wasv4 msg nodesrv sendsrv mpas mppas authsrv pcc_fujian pccroute pcc_jiangsu pushsrv pcc_shanghai pcc_sichuan"
BIN_NAME="$BIN_NAME vacftp filesrv filemgr dispatcher dispather_pic synserver confsrv cachesvr wasv4_oil"

edit_conf()
{
	for i in $BIN_NAME
	do
		DIRFILE="$1/conf/ws/$i.conf"
		if [ -f $DIRFILE ]; then
			sed -i -e "s#env:MTRANS_PRJ_HOME/lbs#$1#" $DIRFILE 
			sed -i -e "s#env:MTRANS_PRJ_HOME/libs#$1/libs#" $DIRFILE 
			sed -i "s/^.*log_num.*/log_num=0/" $DIRFILE
			dos2unix $DIRFILE
		fi 
	done
}

edit_data()
{
	CONFPATH="$1/conf/ws/$2.conf"
	
	if [ ! -f $CONFPATH ]; then
		SRCCONF="$CURDIR/conf/ws/$2.conf"
		if [ -f $SRCCONF ]; then
			cp $SRCCONF $CONFPATH
		fi
	fi
	
	case "$2" in
		wasv4)
			txt1=`cat $CONFPATH | grep "auth_client_ip="`
			txt2=`cat $CONFPATH | grep "auth_data_file="`
			
			if [ "$txt1" == "" ] ; then
				if [ "$txt2" != "" ]; then
					if [ -f $CONFPATH ]; then
					  	sed -i "s/^.*auth_data_file.*//" $CONFPATH
					  	dos2unix $CONFPATH
					fi 
				fi
				
				echo "backupdate=1" >> $CONFPATH
				echo "backupenable=0" >> $CONFPATH
				echo "databackup=$1/backup" >> $CONFPATH
				echo "sendcache_speed=10000" >> $CONFPATH
				echo "base_filedir=$1/data" >> $CONFPATH
			
				echo "auth_client_ip=127.0.0.1" >> $CONFPATH
				echo "auth_client_port=8890" >> $CONFPATH
				echo "auth_data_file=$1/data/auth.dat" >> $CONFPATH
				
				if [ -f $1/data/auth.dat ]; then
					mv $1/data/auth.dat $1/data/auth.dat.bak
				fi
			fi
			txt3=`cat $CONFPATH | grep "ftp_enable"`
			if [ "$txt3" != "" ] ; then
				sed -i -e "s/ftp_enable/cfs_enable/g;s/ftp_ip/cfs_ip/g;s/ftp_port/cfs_port/g;s/ftp_user/cfs_user/g;s/ftp_pwd/cfs_pwd/" $CONFPATH
				dos2unix $CONFPATH	
			fi
			;;
		msg)
			txt=`cat $CONFPATH | grep "rediscluster="`
			if [ "$txt" == "" ]; then 
				echo "rediscluster=127.0.0.1:6379,127.0.0.1:6380" >> $CONFPATH
				echo "plugin_enable=0" >> $CONFPATH
				echo "plugin_root=$1/libs/lib" >> $CONFPATH
				echo "plugin_conf=$1/tempData/msg_so.conf" >> $CONFPATH
			fi
			txt=`cat $CONFPATH | grep "mongodbconfig"`
			if [ "$txt" == "" ]; then
				echo "mongodbconfig=type:mongo,ip:127.0.0.1,port:27017,user:,pwd:,db:test" >> $CONFPATH 
				echo "sendcache_speed=0" >> $CONFPATH
				echo "base_filedir=$1/data" >> $CONFPATH
			fi
			txt=`cat $CONFPATH | grep "syn_data"`
			if [ "$txt" == "" ]; then
				echo "syn_data=0" >> $CONFPATH
				echo "syn_server=58.83.210.16" >> $CONFPATH
				echo "syn_port=7005" >> $CONFPATH
			fi
			if [ ! -f "$1/tempData/msg_so.conf" ]; then
				cp "$CURDIR/tempData/msg_so.conf" "$1/tempData/"
			fi
			if [ ! -f "$1/conf/ws/msg_user.conf" ]; then
				cp "$CURDIR/conf/ws/msg_user.conf" "$1/conf/ws/"
			fi
			if [ -d "$1/lib" ]; then
				mv "$1/lib" "$1/lib.$NOWTIME"
			fi
			cp -r "$CURDIR/lib" "$1/lib"
			;;
		pushsrv)
			;;
		pccv4)
			if [ -d "$CURDIR/post" ]; then
				cp -R "$CURDIR/post" "$1"
			fi
			txt=`cat $CONFPATH | grep "pcc_postquery="`
			if [ "$txt" == "" ]; then 
				echo "pcc_postquery=$1/post" >> $CONFPATH
			fi
			txt=`cat $CONFPATH | grep "pcc_dev_name="`
			if [ "$txt" == "" ]; then
				echo "pcc_dev_name=eth0" >> $CONFPATH
				echo "rediscluster=127.0.0.1:6379,127.0.0.1:6380" >> $CONFPATH
			fi
			if [ ! -f "$1/tempData/pcc_user.user" ]; then
				cp "$CURDIR/tempData/pcc_user.user" "$1/tempData/"
			fi
			txt=`cat $CONFPATH | grep "http_cache_time"`
			if [ "$txt" == "" ]; then
				echo "http_cache_time=300" >> $CONFPATH 
			fi
			;;
		authsrv)
			txt1=`cat $CONFPATH | grep "rediscluster"`
			txt2=`cat $CONFPATH | grep "hessian_enable_auth"`
			
			if [ "$txt1" == "" ] ; then
				if [ "$txt2" != "" ]; then
					if [ -f $CONFPATH ]; then
						sed -i -e "s/hessian_call_url/auth_call_url/g;s/hessian_enable_auth/auth_enable/g;s/hessian_send_thread/auth_send_thread/g;s/hessian_recv_thread/auth_recv_thread/" $CONFPATH
						dos2unix $CONFPATH
					fi 
				fi
				echo "rediscluster=127.0.0.1:6379,127.0.0.1:6380" >> $CONFPATH
			fi
			txt1=`cat $CONFPATH | grep "auth_http_call"`
			if [ "$txt1" == "" ]; then
				echo "auth_http_call=0" >> $CONFPATH
				echo "auth_data_check=0" >> $CONFPATH
			fi
			;;
		nodesrv)
			;;
	esac
	
	DESTFILE="$1/bin/$2"
	if [ -f $DESTFILE ];then
		mv $DESTFILE "$DESTFILE.$NOWTIME"
	fi
	
	SRCFILE="$CURDIR/bin/$2"
	if [ -f $SRCFILE ]; then 
		cp $SRCFILE $DESTFILE
	fi
}

install_script()
{
	SHELL_NAME='bin.sh stop.sh start.sh top.sh'
	for i in $SHELL_NAME
	do
		DIRFILE="$1/$i"
		TMPFILE="$CURDIR/$i"
		if [ -f $TMPFILE ];then
			cp $TMPFILE $DIRFILE
			dos2unix $DIRFILE
		fi
	done
}

check_dir()
{
	if [ ! -d $1 ];then
		mkdir $1
	fi
	
	DIRS="bin conf data black submac scp tempData"
	for i in $DIRS
	do
		if [ ! -d "$1/$i" ]; then
			mkdir "$1/$i"
		fi 
		if [ "$i" == "conf" ]; then
			if [ ! -d "$1/$i/ws" ]; then
				mkdir "$1/$i/ws"
			fi
		fi
	done
}

install()
{
	check_dir $INDIR
	for i in $BIN_NAME
	do
		edit_data $INDIR $i
	done
	edit_conf $INDIR
	install_script $INDIR
}

if [ "$INDIR" == "" ]; then
	echo "not input install dir" 
	exit
fi
install