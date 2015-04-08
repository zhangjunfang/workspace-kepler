
orig_dir=$PWD
cmd=$1

del_CtrlM_in_file()
{
    vi -e -s $1 < $CTFOPRJ_HOME/mychg.vim
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


make_all()
{    
    cd $orig_dir

    for f in `find . -name makefile`
    do
        cd $orig_dir
       
        dir=`echo $f | sed 's/makefile//g'`
        echo $dir
        cd $dir 
        make
        make install
       
    done
}

auto_make()
{
	MAKEFILES="rediscache datapool netfile authsrv nodeserver pccv4 pushsrv wasv4"
	MAKEFILES="$MAKEFILES msg/nodeclient msg/publisher msg/datasaver msg/plugin msg/syndata msg/msgserver"
	MAKEFILES="$MAKEFILES mpas pccroute pcc_fujian pcc_shanghai pcc_jiangsu dispather dispather_pic"
	MAKEFILES="$MAKEFILES synserver dispatcher_808 roadinfo was_bd was_hb photo_3g proxy pcc_special"
	MAKEFILES="$MAKEFILES simply_forward pcc_wuhan pcc_shenzhen"
	for i in $MAKEFILES
	do 
		cd "$orig_dir/$i"
		make 
		make install
	done 
}

do_clean()
{    
    clean_make
    cd $orig_dir
    rm -fr ./bin/*
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
        rm -fr ./lib
        rm -fr core 
    done
    cd $orig_dir 
    rm -fr ./share/log/*
    rm -rf ./lbs/log/*
    rm -rf ./lbs/scp/*
    rm -rf ./lbs/data/*
}

make_install()
{
	rm -rf ./setup
	mkdir ./setup
	cp -R ./bin  ./setup
	mkdir  ./setup/lib
	cp -R ./libs/lib/*.so.*  ./setup/lib
	cp -R ./lbs/conf/	 	 ./setup/
	cp -R ./lbs/tempData/    ./setup/
	cp ./script/install.sh   ./setup/
	cp ./script/upgrade.sh   ./setup/
	cp ./script/top.sh       ./setup/
	cp ./script/start.sh     ./setup/
	cp ./script/stop.sh      ./setup/
	cp ./script/bin.sh       ./setup/
	cp ./script/edit.sh  	 ./setup/
	cp ./script/editconf.sh  ./setup/
}

Usage()
{
    echo "Please select what you want"
    echo "    0)  exit"
    echo "    1)  make all"
    echo "    2)  make clean"
    echo "    3)  clean all"
    echo "    4)  make install"
    echo "    5)  auto make"
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
            x0|x1|x2|x3|x4|x5)  return 0 ;;
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
            clean_make
            ;;
        x3)
            do_clean
            ;;
        x4) 
        	make_install
        	;;
        x5)
        	auto_make
        	;;
    esac
}

if [ "x$cmd" == "x" ]; then
	ask_what_to_do
	do_cmd x$ans
else
	do_cmd x$cmd
fi

