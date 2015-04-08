#! /bin/sh

stty erase ^H

SHELL_NAME=`echo $SHELL`

BASH_RC="/.bashrc"

CURUID=`whoami`
if [ $CURUID != 'root' ]; then
echo "only root user can run this script, please su to root";
exit
fi

CURDIR=`pwd`
LBSBASE="/usr/local/lbs"
NOWTIME=`date +%y%m%d%H%M%S`

check_dir()
{
	if [ ! -d $LBSBASE ];then
		echo "upgrade base dir not exits make sure installed"
		exit ;
	fi
	if [ ! -d "$LBSBASE/bin" ]; then
		mkdir "$LBSBASE/bin"
	fi 
	if [ ! -d "$LBSBASE/conf" ]; then
		mkdir "$LBSBASE/conf"
	fi
	if [ ! -d "$LBSBASE/conf/ws" ]; then
		mkdir "$LBSBASE/conf/ws"
	fi
}
check_dir

update_libs()
{
	LBSLIBS="$LBSBASE/lib"
	if [ -d $LBSLIBS ]; then
		mv $LBSLIBS "$LBSBASE/lib.$NOWTIME"
	fi
	cp -r "$CURDIR/lib" $LBSLIBS
}

change_env()
{
	CTFOPRJ_HOME=/root
		
	if [ "$SHELL_NAME" == "/bin/bash" ] ; then
	
		if [ ! -f $CTFOPRJ_HOME${BASH_RC} ] ; then
	    	touch $CTFOPRJ_HOME${BASH_RC}
	    	echo "# bashrc" >> $CTFOPRJ_HOME${BASH_RC}
		fi
		
		find="export LD_LIBRARY_PATH=/usr/local/lbs/lib:\$LD_LIBRARY_PATH"
		txt=`cat /root/.bashrc | grep "$find"`
		
		if [ "$txt" == "" ] ; then
			echo "umask 022" >> $CTFOPRJ_HOME${BASH_RC}
			echo "export LD_LIBRARY_PATH=$CTFOBIN_ROOT/lib:\$LD_LIBRARY_PATH" >> $CTFOPRJ_HOME${BASH_RC}
			echo "export PATH=$CTFOBIN_ROOT/bin:\$PATH" >> $CTFOPRJ_HOME${BASH_RC} 
			echo "echo 'hello bash'" >> $CTFOPRJ_HOME${BASH_RC}
			
			source $CTFOPRJ_HOME${BASH_RC}
		fi
	fi
}

change_conf()
{
	CONF_NAME='conf/ws/pccv4.conf conf/ws/wasv4.conf conf/ws/msg.conf conf/ws/nodesrv.conf conf/ws/authsrv.conf conf/ws/pushsrv.conf conf/ws/pccroute.conf conf/ws/pcc_fujian.conf conf/ws/pccv4.conf'
	
	TEMP_CONF="$CURDIR/temp.conf" 
	for i in $CONF_NAME
	do
		DIRFILE="$LBSBASE/$i"
		if [ -f $DIRFILE ]; then		
		  	sed -i "s/^.*log_num.*/log_num=0/" $DIRFILE
		  	dos2unix $DIRFILE
		fi 
	done
}

install_script()
{
	SHELL_NAME='bin.sh stop.sh start.sh top.sh'
	for i in $SHELL_NAME
	do
		DIRFILE="$LBSBASE/$i"
		TMPFILE="$CURDIR/$i"
		if [ -f $TMPFILE ];then
			cp $TMPFILE $DIRFILE
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
				echo "base_filedir=/usr/local/lbs/data" >> $CONFPATH
			fi
			txt=`cat $CONFPATH | grep "syn_data"`
			if [ "$txt" == "" ]; then
				echo "syn_data=0" >> $CONFPATH
				echo "syn_server=58.83.210.16" >> $CONFPATH
				echo "syn_port=7005" >> $CONFPATH
			fi
			;;
		pushsrv)
			;;
		pccv4)
			if [ ! -d "$1/post" ]; then
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
	cp $SRCFILE $DESTFILE
}

start_bin()
{
	BINNAME="$1"
	SERVER_STR=`ps -ef | grep $CURUID | grep $BINNAME | awk -F' ' '{print $1":"$8":"$2}'`
	
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
	
	edit_data "$LBSBASE" "$BINNAME"
	if [ -d $LBSBASE/bin ] ; then
		$LBSBASE/bin/$BINNAME&
	fi
	
	if [ $BINNAME == "wasv4" ]; then
		edit_data "$LBSBASE" "vacftp"
		WAS7709DIR="$LBSBASE/7709was"
		if [ -d $WAS7709DIR ]; then 
			edit_data "$WAS7709DIR" "wasv4"
			if [ -f $WAS7709DIR/bin/wasv4 ]; then 
				$WAS7709DIR/bin/wasv4 -r$WAS7709DIR&
			fi
		fi
		
		WAS7710DIR="$LBSBASE/7710was"
		if [ -d $WAS7710DIR ]; then
			edit_data "$WAS7710DIR" "wasv4"
			if [ -f $WAS7710DIR/bin/wasv4 ] ; then
				$WAS7710DIR/bin/wasv4 -r$WAS7710DIR&
			fi
		fi
	fi
}

update_file()
{
	UPDATEFILE='authsrv pccv4 wasv4 msg nodesrv mppas vacftp pushsrv sendsrv pccroute pcc_fujian mpas'
	for i in $UPDATEFILE
	do
		start_bin $i
	done
}

Usage()
{
    echo "Please select what you want"
    echo "    0)  exit"
    echo "    1)  upgrade was"
    echo "    2)  upgrade msg"
    echo "    3)  upgrade pushsrv"
    echo "    4)  upgrade nodesrv"
    echo "    5)  upgrade pccv4"
    echo "    6)  upgrade authsrv"
    echo "    7)  set run env"
    echo "    8)  modify conf"
    echo "    9)  install script"
    echo "   10)  update all"
    echo "   11)  upgrade mppas"
    echo "   12)  upgrade sendsrv"
    echo "   13)  upgrade mpas"
    echo "   14)  upgrade pccroute"
}

ans=xxx
ask_what_to_do()
{
    while [ 1 ]
    do
        Usage
        tput smso
        read ans
        tput rmso
        
        case "x$ans" in
            x0|x1|x2|x3|x4|x5|x6|x7|x8|x9|x10|x11|x12|x13|x14)  return 0 ;;
        esac        
    done    
}

do_cmd()
{
    case "$1" in
        x0)
            return 0
            ;;
        x1) 
            start_bin "wasv4"
            ;;
        x2)
        	update_libs
        	start_bin "msg"
        	;;
        x3)
        	start_bin "pushsrv"
        	;;
        x4)
        	start_bin "nodesrv"
        	;;
        x5)
        	start_bin "pccv4"
        	;;
        x6) 
        	start_bin "authsrv"
        	;;
       	x7)
       		change_env
       		;;
       	x8)
       		change_conf
       		;;
       	x9)
       		install_script
       		;;
       	x10)
       		update_libs
       		update_file
       		install_script
       		;;
       	x11)
       		start_bin "mppas"
       		;;
       	x12)
       		start_bin "sendsrv"
       		;;
       	x13)
       		start_bin "mpas"
       		;;
       	x14)
       		start_bin "pccroute"
       		;;
    esac
}

ask_what_to_do
do_cmd x$ans
 
