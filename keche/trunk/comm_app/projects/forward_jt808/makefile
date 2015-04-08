 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = ../share/coder/intercoder.cpp ../tools/utils.cpp systemenv.cpp main.cpp wasclient.cpp wasserver.cpp
PROGRAM  = forward_jt808
CLIB     = 

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../include -I../share/coder -I../rediscache
SPECIAL_LIB_PATH  = -L$(CTFOLIBS_HOME)/lib -L${DBLIBS_HOME}/redis/lib -L../rediscache

SPECIAL_LIBS = -lctfobase -lrediscache -lhiredis

include ${MTRANS_PRJ_HOME}/makefile.include


