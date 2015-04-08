#!/bin/sh

orig_dir=$PWD
cmd=$1

del_CtrlM_in_file()
{
    vi -e -s $1 < $CTFOLIBS_HOME/mychg.vim
}

del_CtrlM()
{    
    cd $orig_dir

    for f in `find . -name "*.h"`
    do
        echo "del CtrlM in $f"
        del_CtrlM_in_file $f
    done

    for f in `find . -name "*.c"`
    do
        echo "del CtrlM in $f"
        del_CtrlM_in_file $f
    done

    for f in `find . -name "*.cpp"`
    do
        echo "del CtrlM in $f"
        del_CtrlM_in_file $f
    done

    for f in `find . -name "makefile"`
    do
        echo "del CtrlM in $f"
        del_CtrlM_in_file $f
    done
}


make_lib()
{    
    for f in `find . -name makefile`
    do
        cd $orig_dir
        echo $f | grep "\./src"

        if [ $? == 0 ]; then 
            dir=`echo $f | sed 's/makefile//g'`
            echo $dir
            cd $dir 
            echo $CTFOLIBS_HOME
            make
            make install
            rm -fr ./lib
        fi
    done
}

make_so()
{    
    cd "$orig_dir/lib"
	
	rm -rf *.so
	
    for f in `find *.so.2`
    do
        echo $f | grep "\.so.2"

        if [ $? == 0 ]; then 
            so=${f//.2/}
            ln -s $f $so
            echo $so
        fi
    done
}

make_all()
{
	MAKEFILES="tinyxml utils thread log socket_ex http base fqueue"
	for i in $MAKEFILES
	do
		cd "$orig_dir/src/$i"
    	make
    	make install
###    	make_so
    done
    
    cd "$orig_dir/src"
    make 
    make install
}

clean_make()
{    
    for f in `find . -name makefile`
    do
        cd $orig_dir
        dir=`echo $f | sed 's/makefile//g'`
        echo $dir
        cd $dir 
        make clean
        rm -fr *.log*
        rm -rf ./lib/*
        rm -fr core 
    done
}

do_clean()
{    
    clean_make
    cd $orig_dir
    rm -rf ./lib/*
}

make_install()
{
	if [ -d $MTRANS_PRJ_HOME/libs ]; then
		cp -R  ./include   $MTRANS_PRJ_HOME/libs/
		cp -R  ./lib   	   $MTRANS_PRJ_HOME/libs/
	fi
	
	if [ -d  $GTRANS_PRJ_HOME/libs/ ]; then
		cp -R  ./include   $GTRANS_PRJ_HOME/libs/ 
		cp -R  ./lib   	   $GTRANS_PRJ_HOME/libs/
	fi 
	
	if [ -d $TOOLS_PRJ_HOME/libs/ ]; then
		cp -R  ./include   $TOOLS_PRJ_HOME/libs/ 
		cp -R  ./lib   	   $TOOLS_PRJ_HOME/libs/
	fi
}

clear_install()
{
	if [ -d $MTRANS_PRJ_HOME/libs/ ]; then
		rm -rf $MTRANS_PRJ_HOME/libs/*
	fi
	if [ -d $GTRANS_PRJ_HOME/libs/ ]; then
		rm -rf $GTRANS_PRJ_HOME/libs/*
	fi
	if [ -d $TOOLS_PRJ_HOME/libs/ ]; then
		rm -rf $TOOLS_PRJ_HOME/libs/*
	fi
}


Usage()
{
    echo "Please select what you want"
    echo "    0)  exit"
    echo "    1)  make all"
    echo "    2)  make so"
    echo "    3)  make clean"
    echo "    4)  clean all"
    echo "    5)  make install"
    echo "    6)  clear install"
}

cd $orig_dir

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
            x0|x1|x2|x3|x4|x5|x6)  return 0 ;;
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
            make_all
            ;;
        x2)
            make_so
            ;;
        x3)
            clean_make   
            ;;
        x4) 
        	do_clean
        	;;
        x5)
            make_install
            ;;
        x6)
        	clear_install
        	;;
    esac
}

if [ "x$cmd" == "x" ]; then
	ask_what_to_do
	do_cmd x$ans
else
	do_cmd x$cmd
fi
