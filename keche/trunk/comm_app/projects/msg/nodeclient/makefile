 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = ../../share/node/waitgroup.cpp ../../share/node/msgbuilder.cpp nodeclient.cpp
#SOURCES = testmain.cpp
PROGRAM  = 
CLIB     = libnodeclient.a 
##SONAME   = libnodeclient.so.1

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../../include -I../../share/node -I../../share/command -I../include
SPECIAL_LIB_PATH  = 

#SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex
SPECIAL_LIBS = -lctfobase

include ${MTRANS_PRJ_HOME}/makefile.include


