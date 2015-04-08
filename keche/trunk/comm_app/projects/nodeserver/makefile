############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = ../share/node/waitgroup.cpp ../share/node/msgbuilder.cpp nodemgr.cpp nodesrv.cpp systemenv.cpp main.cpp 
#SOURCES = testmain.cpp
PROGRAM  = nodesrv
CLIB     = 

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../share/node
SPECIAL_LIB_PATH  = 

##SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex
SPECIAL_LIBS = -lctfobase

include ${MTRANS_PRJ_HOME}/makefile.include


