 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = waymgr.cpp plugutil.cpp plugin.cpp 
#SOURCES = testmain.cpp
PROGRAM  = 
CLIB     = libplugin.a 
##SONAME   = libplugin.so.1

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../../include -I../../share/node -I../include
SPECIAL_LIB_PATH  = 

SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex

include ${MTRANS_PRJ_HOME}/makefile.include


