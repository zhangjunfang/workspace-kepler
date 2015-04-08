############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = 	TProto/ProtoParse.cpp datastat.cpp
SOURCES += filecacheex.cpp logincheck.cpp transmit.cpp passerver.cpp systemenv.cpp main.cpp
#SOURCES = testmain.cpp           
PROGRAM  = proxy
CLIB     = 

#SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS = 
SPECIAL_INCL_PATH = -ITProto -I../share/file
SPECIAL_LIB_PATH =
##SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex
SPECIAL_LIBS = -lctfobase

include ${MTRANS_PRJ_HOME}/makefile.include
