 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  =  ../share/coder/intercoder.cpp msgclient.cpp systemenv.cpp main.cpp \
	tinystr.cpp tinyxml.cpp tinyxmlerror.cpp tinyxmlparser.cpp httpquery.cpp
PROGRAM  = roadinfo
CLIB     = 

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = -g
SPECIAL_INCL_PATH = -I../include -I../share/coder
SPECIAL_LIB_PATH  = -L$(CTFOLIBS_HOME)/lib

SPECIAL_LIBS = -lctfobase -lcurl

include ${MTRANS_PRJ_HOME}/makefile.include


