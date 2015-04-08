
cmd=$1

orig_dir=`pwd`

make_msg()
{
	MAKEFILES="publisher nodeclient datasaver plugin syndata msgserver plugin/truck"
	for i in $MAKEFILES
	do 
		cd "$orig_dir/$i"
		if [ "x$1" == "x1" ]; then
			make 
			make install
		else
			make clean
		fi
	done 
}

Usage()
{
    echo "Please select what you want"
    echo "    0)  exit"
    echo "    1)  make all"
    echo "    2)  make clean"
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
            x0|x1|x2)  return 0 ;;
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
            make_msg 1
            ;;
        x2)
            make_msg 2
            ;;
    esac
}

if [ "x$cmd" == "x" ]; then
	ask_what_to_do
	do_cmd x$ans
else
	do_cmd x$cmd
fi