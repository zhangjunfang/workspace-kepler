 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = ../../share/coder/intercoder.cpp picclient.cpp msgclient.cpp 
#SOURCES = main.cpp
#SOURCES = testmain.cpp
#PROGRAM  = test
 CLIB     = libsyndata.a 
##SONAME   = libdatasaver.so.1

###__DEBUG_SAVEDB只有在调试模式下开放。
SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../../include -I../../share/node -I../../share/coder -I../include -I../../netfile
SPECIAL_LIB_PATH  = -L../../netfile


#SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex -lfqueue -lhttpagent -lnetfile
SPECIAL_LIBS = -lctfobase
include ${MTRANS_PRJ_HOME}/makefile.include
