
#include "UtilitySocket.h"
#include "utility.h"


#ifndef MAKEDEPEND

#if defined(WIN32)
# include <stdio.h>
# include <winsock2.h>
# include <errno.h>

#else
extern "C" {
# include <unistd.h>
# include <stdio.h>
# include <sys/types.h>
# include <sys/socket.h>
# include <netinet/in.h>
# include <netdb.h>
# include <errno.h>
# include <fcntl.h>
}
#endif  // WIN32

#endif // MAKEDEPEND

using namespace UtilitySocket;
 
//static bool wsInit = false;

void BaseSocket::initSock()
{
#if defined(WIN32)
  if (! wsInit)
  {
    WORD wVersionRequested = MAKEWORD( 2, 2 );
    WSADATA wsaData;
    WSAStartup(wVersionRequested, &wsaData);
    wsInit = true;
  }
#endif // WIN32
}

void BaseSocket::uninitSock()
{
#if defined(WIN32)
	if (wsInit)
	{
		WSACleanup();

		wsInit = false;
	}
#endif // WIN32
}

// These errors are not considered fatal for an IO operation; the operation will be re-tried.
static inline bool
nonFatalError()
{
  int err = BaseSocket::getError();
  return (err == EINPROGRESS || err == EAGAIN || err == EWOULDBLOCK || err == EINTR);
}

int
BaseSocket::socket(int type /*= SOCK_STREAM*/)
{
  return (int) ::socket(AF_INET, type, 0);
}


void
BaseSocket::close(int fd)
{
	// 先关闭SOCKET资源
	shutdown( fd, 2 ) ;
//  XmlRpcUtil::log(4, "UtilitySocket::close: fd %d.", fd);
#if defined(WIN32)
	closesocket(fd);
#else
	::close(fd);
#endif // WIN32
}

bool
BaseSocket::setNonBlocking(int fd)
{
#if defined(WIN32)
  unsigned long flag = 1;
  return (ioctlsocket((SOCKET)fd, FIONBIO, &flag) == 0);
#else
  return (fcntl(fd, F_SETFL, O_NONBLOCK) == 0);
#endif // WIN32
}


bool
BaseSocket::setReuseAddr(int fd)
{
  // Allow this port to be re-bound immediately so server re-starts are not delayed
  int sflag = 1;
  return (setsockopt(fd, SOL_SOCKET, SO_REUSEADDR, (const char *)&sflag, sizeof(sflag)) == 0);
}

#ifndef _UNIX
bool
BaseSocket::setNoCheckSum( int fd )
{
	int optval = 1 ;
	return ( setsockopt( fd, SOL_SOCKET, SO_NO_CHECK, (void *)&optval, sizeof(int) ) == 0 ) ;
}
#endif

bool
BaseSocket::setLinger( int fd )
{
	// 设置Socket调用关闭不等待直接回收资源
	struct linger ld ;
	ld.l_onoff  = 1 ;
	ld.l_linger = 0 ;

	return ( setsockopt( fd, SOL_SOCKET , SO_LINGER , (char*)&ld , sizeof(ld) ) == 0 ) ;
}

bool
BaseSocket::setRecvBuf( int fd, int max )   // 16777216
{
	return ( setsockopt( fd , SOL_SOCKET, SO_RCVBUF, (char *) &max , sizeof(int) )  == 0 ) ;
}

bool
BaseSocket::setSendBuf( int fd, int max )
{
	return ( setsockopt( fd , SOL_SOCKET, SO_SNDBUF, (char*) &max, sizeof(int) ) == 0  ) ;
}

// Bind to a specified port
bool 
BaseSocket::bind(int fd, int port, const char* pIp /*= NULL*/)
{
  struct sockaddr_in saddr;
  memset(&saddr, 0, sizeof(saddr));
  saddr.sin_family = AF_INET;
  if ( pIp == NULL || *pIp == 0 )
	  saddr.sin_addr.s_addr = INADDR_ANY ;
  else
	  saddr.sin_addr.s_addr = inet_addr(pIp);

  saddr.sin_port = htons((u_short) port);
  return (::bind(fd, (struct sockaddr *)&saddr, sizeof(saddr)) == 0);
}


// Set socket in listen mode
bool 
BaseSocket::listen(int fd, int backlog)
{
  return (::listen(fd, backlog) == 0);
}


int
BaseSocket::accept(int fd, std::string& ip, int& port)
{
  struct sockaddr_in addr;
#if defined(WIN32)
  int
#else
  socklen_t
#endif
    addrlen = sizeof(addr);

  int nRet = (int) ::accept(fd, (struct sockaddr*)&addr, &addrlen);

  ip = inet_ntoa(addr.sin_addr);
  port = ntohs(addr.sin_port);
  return nRet;
}

   
// Connect a socket to a server (from a client)
bool
BaseSocket::connect(int fd, const std::string& host, int port)
{
  struct sockaddr_in saddr;
  memset(&saddr, 0, sizeof(saddr));
  saddr.sin_family = AF_INET;

  // 先判是否为IP，如果为IP则直接处理
  in_addr_t in_addr = inet_addr(host.c_str()) ;
  if ( in_addr == (in_addr_t)-1 ) {  // 不为IP时取得域名对应的IP
	  struct hostent *hp = NULL;
#ifdef _WIN32
	  hp = gethostbyname(host.c_str());
#else
#ifdef _UNIX
	  hp = gethostbyname(host.c_str());
#else
	  int  ret = 0 ;
	  char buf[8193] = {0};
	  struct hostent hostinfo ;
	  if ( gethostbyname_r( host.c_str() , &hostinfo , buf , sizeof(buf) , &hp , &ret ) ){
		return false ;
	  }
#endif
#endif
	  if (hp == 0) return false;

	  saddr.sin_family = hp->h_addrtype;
	  memcpy(&saddr.sin_addr, hp->h_addr, hp->h_length);
  } else {
	  saddr.sin_addr.s_addr = in_addr;
  }
  saddr.sin_port = htons((u_short) port);

  // For asynch operation, this will return EWOULDBLOCK (windows) or
  // EINPROGRESS (linux) and we just need to wait for the socket to be writable...
  int result = ::connect(fd, (struct sockaddr *)&saddr, sizeof(saddr));
  return ( result == 0 /*|| nonFatalError()*/ ) ;
}

bool 
BaseSocket::connectByIP(int socket, int port, const char* pIP)
{
	if ( NULL == pIP )
		return false;

	struct sockaddr_in saddr;
	memset(&saddr, 0, sizeof(saddr));
	saddr.sin_family = AF_INET;
	saddr.sin_addr.s_addr = inet_addr(pIP);
	saddr.sin_port = htons((u_short) port);

	// For asynch operation, this will return EWOULDBLOCK (windows) or
	// EINPROGRESS (linux) and we just need to wait for the socket to be writable...
	int result = ::connect(socket, (struct sockaddr *)&saddr, sizeof(saddr));
	return ( result == 0 /*|| nonFatalError()*/ ) ;
}

// Read available text from the specified socket. Returns false on error.
bool 
BaseSocket::nbRead(int fd, std::string& s, bool *eof)
{
  const int READ_SIZE = 4096;   // Number of bytes to attempt to read at a time
  char readBuf[READ_SIZE];

  bool wouldBlock = false;
  *eof = false;

  while ( ! wouldBlock && ! *eof) {
#if defined(WIN32)
    int n = recv(fd, readBuf, READ_SIZE-1, 0);
#else
    int n = read(fd, readBuf, READ_SIZE-1);
#endif
	// OUT_INFO(NULL, 0, NULL, "UtilitySocket::nbRead: read/recv returned %d.", n);

    if (n > 0) {
      readBuf[n] = 0;
      s.append(readBuf, n);
    } else if (n == 0) {
      *eof = true;
    } else if (nonFatalError()) {
      wouldBlock = true;
    } else {
      return false;   // Error
    }
  }
  return true;
}

///////////////////////////////////////////////////////////////////////////////////////////////////
//nlen 返回实际读取的字节数
int 
BaseSocket::nbRead(int s, char* pData, unsigned &nLen, unsigned nTimeout/* = 0*/)
{
	if (s == (int)INVALID_SOCKET)
		return SOCKET_ERROR_DISCONNECTED;
	
	if ((!pData) || (nLen == 0))
		return SOCKET_ERROR_PARAM;
	
	int iRecv = 0;
	unsigned nRecved = 0;
	int iRet = 0;
	
	time_t begin = time(NULL);
	
	fd_set fds[1];
	timeval timeout;
	timeout.tv_sec = SOCKET_TIMEOUT_SELECT;
	timeout.tv_usec = 0;
	
	int iRes = 0;
	
	do
	{
#if defined(WIN32)
		iRecv = recv(s, pData + nRecved, nLen - nRecved, 0);
#else
		iRecv = read(s, pData + nRecved, nLen - nRecved);
#endif
		
		if (iRecv == 0 )
		{
			iRet = SOCKET_ERROR_DISCONNECTED;
			goto Ext;
		}
		else if (iRecv > 0)
		{
			nRecved += iRecv;
		}
		else if (iRecv == SOCKET_ERROR)
		{
			if (getError() != EWOULDBLOCK)
			{
				iRet = SOCKET_ERROR_DISCONNECTED;
				goto Ext;
			}
			else
			{
				while ((unsigned)(time(NULL) - begin) < nTimeout);
				{
					FD_ZERO(fds);
					FD_SET(s, fds);
					
					iRes = select(1, fds, NULL, NULL, &timeout);
					if (iRes == 1)
					{
						if (FD_ISSET(s, fds))
							break; //可读了，重新读取
					}
					else if (iRes == SOCKET_ERROR)
					{
						iRet = SOCKET_ERROR_DISCONNECTED;
						goto Ext;
					}
				} 		
			}
		}
	} while ((nRecved < nLen) && ((unsigned)(time(NULL) - begin) < nTimeout));
	
	if (nRecved < nLen)
	{
		iRet = SOCKET_ERROR_TIMEOUT;
		goto Ext;
	}

	iRet = SOCKET_ERROR_OK;
Ext:	
	nLen = nRecved;
	return iRet;
}


// Write text to the specified socket. Returns false on error.
bool 
BaseSocket::nbWrite(int fd, std::string& s, int *bytesSoFar)
{
  int nToWrite = int(s.length()) - *bytesSoFar;
  char *sp = const_cast<char*>(s.c_str()) + *bytesSoFar;
  bool wouldBlock = false;

  while ( nToWrite > 0 && ! wouldBlock ) {
#if defined(WIN32)
    int n = send(fd, sp, nToWrite, 0);
#else
    int n = write(fd, sp, nToWrite);
#endif
	
	// OUT_INFO(NULL, 0, NULL, "UtilitySocket::nbWrite: send/write returned %d.", n);

    if (n > 0) {
      sp += n;
      *bytesSoFar += n;
      nToWrite -= n;
    } else if (nonFatalError()) {
      wouldBlock = true;
    } else {
      return false;   // Error
    }
  }
  return true;
}

///////////////////////////////////////////////////////////////////////////////////////////////////

int 
BaseSocket::nbWrite(int s, const char* pData, unsigned &nLen, unsigned nTimeout /*= 0*/)
{
	if (s == (int)INVALID_SOCKET)
		return SOCKET_ERROR_DISCONNECTED; //连接无效

	if ((!pData) || (nLen == 0))
		return SOCKET_ERROR_PARAM; //参数无效

	int iSend = 0;
	unsigned nSend = 0;

	time_t begin = time(NULL);

	fd_set fds[1];
	timeval timeout;
	timeout.tv_sec = SOCKET_TIMEOUT_SELECT;
	timeout.tv_usec = 0;

	int iRes = 0;

	do
	{
#if defined(WIN32)
		iSend = send(s, pData + nSend, nLen - nSend, 0);
#else
		iSend = write(s, pData + nSend, nLen - nSend);
#endif
		
		if (iSend > 0)
		{
			nSend += iSend;
		}
		else if (iSend == SOCKET_ERROR)
		{
			if (getError() != EWOULDBLOCK)
			{
				return SOCKET_TIMEOUT_SELECT;
			}
			else
			{
				do
				{
					FD_ZERO(fds);
					FD_SET(s, fds);

					iRes = select(1, NULL, fds, NULL, &timeout);
					if (iRes == 1)
					{
						if (FD_ISSET(s, fds))
							break;
					}
					else if (iRes == SOCKET_ERROR)
					{
						return SOCKET_ERROR_DISCONNECTED;
					}
				} while ((unsigned)(time(NULL) - begin) < nTimeout);
			}
		}
	} while ((nSend < nLen) && ((unsigned)(time(NULL) - begin) < nTimeout));

	if (nSend < nLen)
		return SOCKET_ERROR_TIMEOUT;

	nLen = nSend;

	return SOCKET_ERROR_OK;
}


///////////////////////////////////////////////////////////////////////////////////////////////////

// Returns last errno
int 
BaseSocket::getError()
{
#if defined(WIN32)
  return WSAGetLastError();
#else
  return errno;
#endif
}


// Returns message corresponding to last errno
std::string 
BaseSocket::getErrorMsg()
{
  return getErrorMsg(getError());
}

// Returns message corresponding to errno... well, it should anyway
std::string 
BaseSocket::getErrorMsg(int error)
{
  char err[60];
  snprintf(err,sizeof(err),"error %d", error);
  return std::string(err);
}

/*
* 写数据
*/
int
BaseSocket::write (int fd, const void *data, int len) {
   int res;
   do {
	   res = ::write( fd, data, len ) ;
	   if (res > 0) {
		   //TBSYS_LOG(INFO, "写出数据, fd=%d, addr=%d", _socketHandle, res);
	   }
   } while (res < 0 && errno == EINTR);
   return res;
}

/*
* 读数据
*/
int
BaseSocket::read ( int fd , void *data, int len ) {
   int res;
   do {
	   res = ::read( fd , data, len );
	   if (res > 0) {
		   //TBSYS_LOG(INFO, "读入数据, fd=%d, addr=%d", _socketHandle, res);
	   }
   } while (res < 0 && errno == EINTR);
   return res;
}
