/**
  * Name: 
  * Copyright: 
  * Author: lizp.net@gmail.com
  * Date: 2009-11-3 下午 3:42:25
  * Description: 
  * Modification: 
  **/
#ifndef _UTILITYSOCKET_H_
#define _UTILITYSOCKET_H_

#if defined(_MSC_VER)
# pragma warning(disable:4786)    
#endif

#ifndef MAKEDEPEND
# include <string>

#if defined(_NI_WIN_)
# define EINPROGRESS	WSAEINPROGRESS
# define EWOULDBLOCK	WSAEWOULDBLOCK
# define ETIMEDOUT	    WSAETIMEDOUT
#endif

#endif

#define SOCKET_ERROR_OK				0
#define SOCKET_ERROR_PARAM			-1
#define SOCKET_ERROR_TIMEOUT		-2
#define SOCKET_ERROR_DISCONNECTED	-3
#define SOCKET_TIMEOUT_SELECT		5

namespace UtilitySocket {

  //! A platform-independent socket API.
  class BaseSocket {
  public:

	static void initSock();
	static void uninitSock();
    //! Creates a stream (TCP) socket. Returns -1 on failure.
    static int socket(int type = 1/*SOCK_STREAM*/);

    //! Closes a socket.
    static void close(int socket);

    //! Sets a stream (TCP) socket to perform non-blocking IO. Returns false on failure.
    static bool setNonBlocking(int socket);

    //! Read text from the specified socket. Returns false on error.
    static bool nbRead(int socket, std::string& s, bool *eof);
	static int nbRead(int s, char* pData, unsigned &nLen, unsigned nTimeout = 0);

    //! Write text to the specified socket. Returns false on error.
    static bool nbWrite(int socket, std::string& s, int *bytesSoFar);
	static int nbWrite(int s, const char* pData, unsigned &nLen, unsigned nTimeout = 0);

	static int write (int fd, const void *data, int len) ;
	static int read ( int fd , void *data, int len ) ;
    // The next four methods are appropriate for servers.

    //! Allow the port the specified socket is bound to to be re-bound immediately so 
    //! server re-starts are not delayed. Returns false on failure.
    static bool setReuseAddr(int socket);
#ifndef _UNIX
    // 设置不需要处理检效和
    static bool setNoCheckSum( int fd ) ;
#endif
    // 设置Socket调用关闭不等待直接回收资源
    static bool setLinger( int fd )  ;

    // 设置接收缓存区的大小
    static bool setRecvBuf( int fd, int max ) ;

    // 设置发送缓存区的大小
    static bool setSendBuf( int fd, int max ) ;

    //! Bind to a specified port
    static bool bind(int socket, int port, const char* pIp = NULL);

    //! Set socket in listen mode
    static bool listen(int socket, int backlog);

    //! Accept a client connection request
    static int accept(int socket, std::string& ip, int& port);

    //! Connect a socket to a server (from a client)
    static bool connect(int socket, const std::string& host, int port);
	static bool connectByIP(int socket, int port, const char* pIP);


    //! Returns last errno
    static int getError();

    //! Returns message corresponding to last error
    static std::string getErrorMsg();

    //! Returns message corresponding to error
    static std::string getErrorMsg(int error);
  };

} // namespace UtilitySocket

#endif
