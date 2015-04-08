 ############################################################
## makefile of libunisql.a
############################################################
.SUFFIXES: .cpp
HAVE_TESTER=0
SOURCES  = ../../../share/xml/xmlparser.cpp ../../../share/xml/httpcaller.cpp ../../../share/plugin/msgqueue.cpp  \
	../../../share/pack/packfactory.cpp ../../../share/plugin/resultmgr.cpp srvcaller.cpp truck.cpp

ifneq (0,$(HAVE_TESTER))
SOURCES += systemenv.cpp main.cpp
PROGRAM  = tester
else
SONAME = libtruck.so.2
LNNAME = libtruck.so
endif

#SOURCES = testmain.cpp

SPECIAL_MACRO_FLAGS = -DRELEASE
SPECIAL_CCFLAGS   = 
SPECIAL_INCL_PATH = -I../../../include -I../../include -I../../../share/pack -I../../../share/plugin -I../../../share/xml
SPECIAL_LIB_PATH  = -L.

SPECIAL_LIBS = -lctfobase

include ${MTRANS_PRJ_HOME}/makefile.include


