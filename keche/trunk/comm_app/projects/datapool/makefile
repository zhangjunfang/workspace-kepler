 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

HAVE_TESTER = 0
SOURCES  = dbpool.cpp dbfacemgr.cpp mongodb.cpp oracledb.cpp
#SOURCES = testmain.cpp

ifneq (0,$(HAVE_TESTER))
SOURCES += main.cpp
PROGRAM  = data_tester
CLIB     = 
else
#CLIB     = libdatapool.a
SONAME  = libdbpool.so.2
LNNAME  = libdbpool.so
endif

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../share/node -I../include -I${DBLIBS_HOME}/mongo/include  -I${DBLIBS_HOME}/oracle/include
SPECIAL_LIB_PATH  = -L${DBLIBS_HOME}/mongo/lib  -L${DBLIBS_HOME}/oracle/lib

SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex -lhttpagent -lmongoclient -lociei

include ${MTRANS_PRJ_HOME}/makefile.include


