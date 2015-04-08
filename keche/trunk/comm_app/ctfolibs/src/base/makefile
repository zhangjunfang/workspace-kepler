############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = BaseClient.cpp BaseServer.cpp BaseTools.cpp Session.cpp crc16.cpp packer.cpp workthread.cpp
PROGRAM  = 
#CLIB     = libnetbaseex.a 
LIBS     = 
SONAME   = libnetbaseex.so.2
LNNAME   = libnetbaseex.so

SPECIAL_LIBS = -lutils -llog -lthshare -lhttpagent -lsocketex

#SPECIAL_LIBS = ${CTFOLIBS_HOME}/lib/libthshare.a ${CTFOLIBS_HOME}/lib/libsocketex.a \
	${CTFOLIBS_HOME}/lib/libutils.a ${CTFOLIBS_HOME}/lib/liblog.a

#WAS_HOME = /home/tcsd/waservice

include ${CTFOLIBS_HOME}/makefile.include

