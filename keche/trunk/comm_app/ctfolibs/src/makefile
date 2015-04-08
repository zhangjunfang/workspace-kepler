############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = ./utils/cConfig.cpp ./utils/tools.cpp ./utils/utility.cpp ./utils/main_app.cpp ./utils/Base64.cpp \
	./thread/Mutex.cpp ./thread/Monitor.cpp ./thread/Ref.cpp ./thread/Util.cpp ./thread/Thread.cpp ./log/cLog.cpp \
	./socket_ex/EpollHandle.cpp ./socket_ex/TcpHandle.cpp ./socket_ex/TcpHandleEx.cpp ./socket_ex/UtilitySocket.cpp \
	./socket_ex/list.cpp ./socket_ex/protocol.cpp ./socket_ex/queuethread.cpp ./socket_ex/kfifo.cpp \
	./socket_ex/UdpHandle.cpp ./socket_ex/UdpHandleEx.cpp ./socket_ex/NetHandle.cpp ./socket_ex/PollHandle.cpp ./socket_ex/KQueueHandle.cpp \
	./http/qstring.cpp ./http/httpparser.cpp ./http/httpclient.cpp ./http/asynchttpclient.cpp ./http/httpresp.cpp ./http/httpserver.cpp \
	./base/BaseClient.cpp ./base/BaseServer.cpp ./base/BaseTools.cpp ./base/Session.cpp ./base/crc16.cpp ./base/packer.cpp \
	./base/workthread.cpp ./fqueue/fileutil.cpp ./fqueue/filequeue.cpp ./fqueue/filecache.cpp \
	./tinyxml/tinystr.cpp ./tinyxml/tinyxml.cpp ./tinyxml/tinyxmlerror.cpp  ./tinyxml/tinyxmlparser.cpp 
	
#mem_alloc.cpp
PROGRAM  = 
CLIB     = libctfobase.a 
LIBS     =
#SONAME   = libctfobase.so.1
SPECIAL_INCL_PATH = -I/usr/local/include
SPECIAL_LIBS = 

include ${CTFOLIBS_HOME}/makefile.include

