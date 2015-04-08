############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = EpollHandle.cpp TcpHandle.cpp TcpHandleEx.cpp UtilitySocket.cpp \
			list.cpp protocol.cpp kfifo.cpp queuethread.cpp UdpHandle.cpp UdpHandleEx.cpp NetHandle.cpp PollHandle.cpp KQueueHandle.cpp
#cpppcap.cpp
PROGRAM  = 
#CLIB     = libsocketex.a 
SONAME	 = libsocketex.so.2
LNNAME   = libsocketex.so
LIBS     = 

SPECIAL_LIBS = -llog -lthshare

include ${CTFOLIBS_HOME}/makefile.include

