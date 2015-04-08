 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = datasaver.cpp proto_save.cpp saverutil.cpp  oraclesave.cpp mongosave.cpp event_analyse.cpp
#SOURCES = main.cpp
#SOURCES = testmain.cpp
#PROGRAM  = test
 CLIB     = libdatasaver.a 
##SONAME   = libdatasaver.so.1

###__DEBUG_SAVEDB只有在调试模式下开放。
SPECIAL_MACRO_FLAGS = -DRELEASE -D__DEBUG_SAVEDB 
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../../include -I../../share/node -I../include -I../../datapool
SPECIAL_LIB_PATH  = -L../../datapool


SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex -lfqueue -ldbpool
include ${MTRANS_PRJ_HOME}/makefile.include
