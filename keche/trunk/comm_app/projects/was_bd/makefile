############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  =  ../share/pack/packfactory.cpp ../share/xml/xmlparser.cpp \
			proto/GbProtocolHandler.cpp  ../share/coder/intercoder.cpp ../share/queue/queuemgr.cpp \
			../tools/utils.cpp packmgr.cpp scpmedia.cpp wasserver.cpp \
			msgclient.cpp iplist.cpp blacklist.cpp systemenv.cpp wasmain.cpp httpquery.cpp
PROGRAM  = was_bd
CLIB     = 

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS = 
SPECIAL_INCL_PATH  = -I../include -Iproto -I../share/node -I../share/coder -I../share/pack
SPECIAL_INCL_PATH += -I../netfile -I../share/queue -I../rediscache
SPECIAL_LIB_PATH  = -L../netfile -L${DBLIBS_HOME}/redis/lib -L../rediscache

SPECIAL_LIBS = -lctfobase -lnetfile -lgd -lcurl -lrediscache -lhiredis
#SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex -lhttpagent -ltinyxml

include ${MTRANS_PRJ_HOME}/makefile.include


