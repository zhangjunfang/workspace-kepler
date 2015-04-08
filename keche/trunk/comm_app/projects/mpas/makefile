############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = ../share/coder/intercoder.cpp ../share/node/waitgroup.cpp ../share/node/msgbuilder.cpp ../share/TProto/ProtoParse.cpp 
SOURCES += pconvert.cpp passerver.cpp nodeclient.cpp msgclient.cpp systemenv.cpp main.cpp
#SOURCES = testmain.cpp
PROGRAM  = mpas
CLIB     = 

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS = 
SPECIAL_INCL_PATH = -I../share/node -I../share/coder -I../share/TProto -I../include -I../rediscache
SPECIAL_LIB_PATH  = -L../rediscache  -L${DBLIBS_HOME}/redis/lib

SPECIAL_LIBS = -lctfobase -lrediscache -lhiredis
#SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex -lhttpagent -ltinyxml

include ${MTRANS_PRJ_HOME}/makefile.include


