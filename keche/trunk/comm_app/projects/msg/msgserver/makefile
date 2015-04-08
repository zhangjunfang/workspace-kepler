 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp

SOURCES  = ../../share/coder/intercoder.cpp msgserver.cpp msguser.cpp systemenv.cpp msgmain.cpp 
#SOURCES = testmain.cpp
PROGRAM  = msg
CLIB     = 

HAVE_SAVE = 0
HAVE_PLUG = 0

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH  = -I../../include -I../../share/node -I../../share/coder -I../include 
SPECIAL_INCL_PATH += -I../nodeclient -I../publisher -I../../rediscache -I../filecache -I../syndata
SPECIAL_LIB_PATH  = -L$(CTFOLIBS_HOME)/lib -L../nodeclient  -L../publisher -L../../rediscache  -L${DBLIBS_HOME}/redis/lib -L../../netfile -L../syndata
#SPECIAL_LIBS = -lutils -llog -lthshare -lsocketex -lnetbaseex -lhttpagent -lfqueue -lnodeclient -lpublisher -lrediscache -lhiredis -lsyndata -lnetfile
SPECIAL_LIBS = -lnodeclient -lpublisher -lrediscache -lhiredis -lsyndata -lnetfile -lctfobase 

ifneq (0,$(HAVE_PLUG))
SPECIAL_INCL_PATH 	+= -I../plugin
SPECIAL_LIB_PATH  	+= -L../plugin
SPECIAL_LIBS 	  	+= -lplugin 
SPECIAL_MACRO_FLAGS += -D_HAVE_PLUG
endif

ifneq (0,$(HAVE_SAVE))
SPECIAL_INCL_PATH += -I../datasaver
SPECIAL_LIB_PATH  += -L../datasaver -L../../datapool
SPECIAL_LIBS 	  += -ldbpool -ldatasaver 
SPECIAL_MACRO_FLAGS += -D_HAVE_SAVE
endif

include ${MTRANS_PRJ_HOME}/makefile.include


