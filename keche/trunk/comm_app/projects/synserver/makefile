 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = synserver.cpp systemenv.cpp main.cpp
#SOURCES = testmain.cpp
PROGRAM  = synserver
CLIB     = 

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../include -I../rediscache -I../datapool
SPECIAL_LIB_PATH  = -L../rediscache  -L${DBLIBS_HOME}/redis/lib  -L../datapool

SPECIAL_LIBS = -lctfobase -lrediscache -lhiredis -ldbpool
##SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex -ltinyxml -lhttpagent

include ${MTRANS_PRJ_HOME}/makefile.include


