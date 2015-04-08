 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp
HAVE_TESTER = 0
SOURCES  = redispool.cpp rediscache.cpp 
#SOURCES = testmain.cpp

ifneq (0,$(HAVE_TESTER))
SOURCES += main.cpp
PROGRAM  = tester
else
CLIB     = librediscache.a
endif

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I${DBLIBS_HOME}/redis/include -I../share/node -I../include
SPECIAL_LIB_PATH  = -L${DBLIBS_HOME}/redis/lib -L.

SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex -lhiredis

include ${MTRANS_PRJ_HOME}/makefile.include


