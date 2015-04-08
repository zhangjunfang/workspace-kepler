 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES = ../share/md5/md5.cpp ../tools/utils.cpp interface.cpp systemenv.cpp amqclient.cpp msgclient.cpp exchclient.cpp protparse.cpp main.cpp
PROGRAM = exchange_cli
CLIB    = 

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I${DBLIBS_HOME}/amqcpp/include/activemq-cpp-3.8.2 -I${DBLIBS_HOME}/cabinet/include -I../include -I../share/coder -I../rediscache
SPECIAL_LIB_PATH  = -L${DBLIBS_HOME}/amqcpp/lib -lactivemq-cpp -L${DBLIBS_HOME}/cabinet/lib -ltokyocabinet -L$(CTFOLIBS_HOME)/lib -L${DBLIBS_HOME}/redis/lib -L../rediscache

SPECIAL_LIBS = -lctfobase -lrediscache -lhiredis

include ${MTRANS_PRJ_HOME}/makefile.include