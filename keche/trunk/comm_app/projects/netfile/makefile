############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

HAVE_TESTER = 0
SOURCES  = asynfile.cpp waitmgr.cpp synfile.cpp main.cpp

ifneq (0,$(HAVE_TESTER))
PROGRAM  = tester
SPECIAL_MACRO_FLAGS = -D__DEBUG_TEST__ -D__ASYN_MODE_ -D__TEST__
else
CLIB     = libnetfile.a
endif

SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../include -I../share/coder
SPECIAL_LIB_PATH  = 

SPECIAL_LIBS = -lctfobase

include ${MTRANS_PRJ_HOME}/makefile.include


