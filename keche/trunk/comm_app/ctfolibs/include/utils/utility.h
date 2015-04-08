/**
  * Name: 
  * Copyright: 
  * Author: lizp.net@gmail.com
  * Date: 2009-11-3 ÏÂÎç 3:42:58
  * Description: 
  * Modification: 
  **/
#ifndef _UTILITY__H___
#define _UTILITY__H___

#if defined(_MSC_VER)
# pragma warning(disable:4786)    // identifier was truncated in debug info
# pragma warning(disable:4996)    // identifier was truncated in debug info
#endif

#ifndef MAKEDEPEND
# include <string>
#endif

#ifdef WIN32
#include <winsock2.h>
#include <Ws2tcpip.h>
#include <time.h>
//#include <IPHlpApi.h>

#pragma comment(lib,"ws2_32.lib")
//#pragma   comment(lib, "IPHlpApi.Lib")  

#else

#include <stdio.h>
#include <string.h>		 
#include <sys/types.h>
#include <sys/socket.h>
#include <sys/ioctl.h>
#include <net/if.h>
#include <net/if_arp.h>
#include <arpa/inet.h>
#include <unistd.h>

#include <sys/resource.h>

#ifdef _DEBUG
#define __MAKE_CORE		struct rlimit sLimit;	\
	sLimit.rlim_cur = -1; \
	sLimit.rlim_max = -1; \
	setrlimit(RLIMIT_CORE, &sLimit); 
#else
#define __MAKE_CORE
#endif

#define PGD_BOUND (4 * 1024 * 1024)
#define STACKSIZE (258 * 1024 * 1024)
#define THREAD_NAME_LEN (256)

void set_stklim(void);

#define INVALID_SOCKET  (unsigned int)(~0)

#endif

#include "comlog.h"

#if defined(_MSC_VER)
# define snprintf	    _snprintf
# define vsnprintf    _vsnprintf
# define strcasecmp	  _stricmp
# define strncasecmp	_strnicmp
#elif defined(__BORLANDC__)
# define strcasecmp stricmp
# define strncasecmp strnicmp
#endif


#define SOCKET_ERROR            (-1)

#endif


