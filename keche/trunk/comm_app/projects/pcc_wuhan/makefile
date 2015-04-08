############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = 	../share/md5/md5.cpp pasclient.cpp msgclient.cpp systemenv.cpp pccmain.cpp
PROGRAM  = pcc_wuhan
CLIB     = 

SPECIAL_MACRO_FLAGS = 
SPECIAL_CCFLAGS = 
SPECIAL_INCL_PATH = -I../include -I../share/coder -I../rediscache
SPECIAL_LIB_PATH =  -L${DBLIBS_HOME}/redis/lib -L../rediscache
SPECIAL_MACRO_FLAGS = 

SPECIAL_LIBS = -lctfobase  -lrediscache -lhiredis

include ${MTRANS_PRJ_HOME}/makefile.include
