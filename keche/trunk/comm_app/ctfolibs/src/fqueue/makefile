############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = fileutil.cpp filequeue.cpp filecache.cpp
PROGRAM  = 
#CLIB     = libfqueue.a 
LIBS     = 
SONAME   = libfqueue.so.2
LNNAME   = libfqueue.so

SPECIAL_LIBS = -lutils -llog -lthshare -lhttpagent -lsocketex

#SPECIAL_LIBS = ${CTFOLIBS_HOME}/lib/libthshare.a ${CTFOLIBS_HOME}/lib/libsocketex.a \
	${CTFOLIBS_HOME}/lib/libutils.a ${CTFOLIBS_HOME}/lib/liblog.a

#WAS_HOME = /home/tcsd/waservice

include ${CTFOLIBS_HOME}/makefile.include

