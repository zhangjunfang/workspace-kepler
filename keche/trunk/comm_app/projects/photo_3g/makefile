 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  =  ../share/coder/intercoder.cpp photosvr.cpp msgclient.cpp systemenv.cpp main.cpp \
	tinystr.cpp tinyxml.cpp tinyxmlerror.cpp tinyxmlparser.cpp httpquery.cpp
PROGRAM  = photo_3g
CLIB     = 

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = -g
SPECIAL_INCL_PATH = -I../include -I../share/coder -I../rediscache
SPECIAL_LIB_PATH  = -L${DBLIBS_HOME}/redis/lib -L../rediscache

SPECIAL_LIBS = -lctfobase -lcurl -lrediscache -lhiredis

include ${MTRANS_PRJ_HOME}/makefile.include


