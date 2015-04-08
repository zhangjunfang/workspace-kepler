 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  =  tcpserver.cpp tcpclient.cpp udpserver.cpp udpclient.cpp systemenv.cpp main.cpp ../tools/utils.cpp
PROGRAM  = simply_forward
CLIB     = 

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../include
SPECIAL_LIB_PATH  = -L$(CTFOLIBS_HOME)/lib

SPECIAL_LIBS = -lctfobase

include ${MTRANS_PRJ_HOME}/makefile.include


