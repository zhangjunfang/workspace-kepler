 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = ../share/md5/md5.cpp ../tools/utils.cpp interface.cpp protparse.cpp exchserver.cpp systemenv.cpp main.cpp
PROGRAM  = exchange_svr
CLIB     = 

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = -g
SPECIAL_INCL_PATH = -I../include -I../rediscache
SPECIAL_LIB_PATH  = -L${DBLIBS_HOME}/redis/lib -L../rediscache

SPECIAL_LIBS = -lctfobase -lrediscache -lhiredis

include ${MTRANS_PRJ_HOME}/makefile.include