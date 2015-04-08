#! /bin/sh

stty erase ^H

CURUID=`whoami`
if [ $CURUID != 'root' ]; then 
echo "only root user can run this script, please su to root";
exit
fi

SHELL_NAME=`echo $SHELL`

BASH_RC="/.bashrc"

SRCDIR=`pwd`

CTFOLOG_ROOT=/var/lbs

if [ ! -d $CTFOLOG_ROOT ]; then mkdir $CTFOLOG_ROOT; fi

CTFOBIN_ROOT=/usr/local/lbs

check_dir()
{
	if [ ! -d $CTFOBIN_ROOT ]; then
		mkdir $CTFOBIN_ROOT
	else 
		CTFOBIN_BAK="$CTFOBIN_ROOT/bak"
		CTFOBIN_TMP="$CTFOBIN_BAK/`date +%y%m%d%H%M%S`"
		
		if [ ! -d $CTFOBIN_BAK ]; then mkdir $CTFOBIN_BAK; fi
		
		mkdir $CTFOBIN_TMP
		
		if [ -d $CTFOBIN_ROOT/bin ]; then cp -R $CTFOBIN_ROOT/bin  $CTFOBIN_TMP; fi
		if [ -d $CTFOBIN_ROOT/conf ]; then cp -R $CTFOBIN_ROOT/conf  $CTFOBIN_TMP; fi
		if [ -d $CTFOBIN_ROOT/tempData ]; then cp -R $CTFOBIN_ROOT/tempData $CTFOBIN_TMP; fi
		if [ -d $CTFOBIN_ROOT/lib ]; then mv $CTFOBIN_ROOT/lib 	$CTFOBIN_TMP; fi
		
		echo "bak old system data path: $CTFOBIN_TMP"
	fi
}

install_bin()
{
	INSTALL_BIN="vacftp wasv4 msg nodesrv pccv4 pushsrv authsrv"
	if [ -d $CTFOBIN_ROOT ]; then
		if [ -d $CTFOBIN_ROOT/bin ]; then
		
		    bindir="$SRCDIR/bin"
		    incdir="$CTFOBIN_ROOT/bin"
		    echo "$bindir $incdir" ;
		    
		    for i in $INSTALL_BIN
			do
				if [ -f $bindir/$i ]; then 
					if [ -f $incdir/$i ]; then rm -rf $incdir/$i; fi
					cp $bindir/$i $incdir
				fi
			done
		else
			cp -R $SRCDIR/bin  $CTFOBIN_ROOT
		fi
		cp -R $SRCDIR/lib  $CTFOBIN_ROOT
	fi
	
	if [ ! -d $CTFOBIN_ROOT/conf ]; then
		cp -R ./conf  $CTFOBIN_ROOT
		cp -R ./tempData $CTFOBIN_ROOT
	else
		if [ ! -d $CTFOBIN_ROOT/conf/ws ]; then mkdir $CTFOBIN_ROOT/conf/ws/; fi
		for i in $INSTALL_BIN
		do
			if [ -f ./conf/ws/$i.conf ]; then
				if [ ! -f $CTFOBIN_ROOT/conf/ws/$i.conf ]; then 
					cp ./conf/ws/$i.conf  $CTFOBIN_ROOT/conf/ws/
				fi
			fi
		done
	fi
}

check_env()
{
	CTFOPRJ_HOME=/root
		
	if [ "$SHELL_NAME" = "/bin/bash" ] ; then
		
		echo "/bin/bash"
		
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
		fi
	
		source $CTFOPRJ_HOME${BASH_RC}
	fi
}

install_shell()
{
	SHELL_NAME='bin.sh stop.sh start.sh top.sh'
	for i in $SHELL_NAME
	do
		DIRFILE="$CTFOBIN_ROOT/$i"
		TMPFILE="$SRCDIR/$i"
		if [ -f $TMPFILE ];then
			cp $TMPFILE $DIRFILE
			dos2unix $DIRFILE
		fi
	done
}

check_dir
install_bin
check_env
install_shell


