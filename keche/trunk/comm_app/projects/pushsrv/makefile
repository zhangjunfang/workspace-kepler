 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  =  ../share/node/waitgroup.cpp ../share/node/msgbuilder.cpp ../share/coder/intercoder.cpp \
            msgclient.cpp nodeclient.cpp  proto_parse.cpp proto_convert.cpp pushserver.cpp \
             subscribe.cpp systemenv.cpp main.cpp
#SOURCES = testmain.cpp
PROGRAM  = pushsrv
CLIB     = 

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../include -I../share/node -I../share/coder -I../rediscache
SPECIAL_LIB_PATH  = -L$(CTFOLIBS_HOME)/lib -L$(DBLIBS_HOME)/redis/lib -L$(MTRANS_PRJ_HOME)/rediscache

SPECIAL_LIBS = -lctfobase -lrediscache -lhiredis

include ${MTRANS_PRJ_HOME}/makefile.include


