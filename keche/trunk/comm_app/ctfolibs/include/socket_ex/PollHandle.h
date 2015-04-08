/*
 * PollHandle.h
 *
 *  Created on: 2011-7-1
 *      Author: humingqing
 *
 *  整体服务器框架设计中都是使用序列化的形式来操作poll，
 *  所以poll不需要处理同步问题
 */

#ifndef __POLLHANDLE_H__
#define __POLLHANDLE_H__

#include <SocketHandle.h>

#ifdef _USE_POLL
#include <poll.h>
#include <map>
#include <vector>

//  Pollset to pass to the poll function.
typedef std::vector<pollfd>   pollset_t ;
typedef std::vector<socket_t*> vecsocket_t ;

class pollarray_t ;
class CPollHandle : public CSocketHandle
{
public:
	CPollHandle();
	virtual ~CPollHandle();

public:
	//创建
    bool create(int max_socket_num = MAX_SOCKET_NUM);
	bool destroy();
	//返回fd
    bool add( socket_t *sock , unsigned int events);
	bool del( socket_t *sock , unsigned int events);
	bool modify( socket_t *sock, unsigned int events);
	int  poll(int timeout = 5000);

    virtual void on_event( socket_t *sock ,int events ){}

	virtual bool is_read(int events);
	virtual bool is_write(int events);
	virtual bool is_excep(int events);

private:
	// 取得当前可用FD
	int pollarray( pollset_t &fdarray , vecsocket_t &vec ) ;

private:
	// FD向量
	pollarray_t   *_pollarray ;
};
#endif

#endif /* POLLHANDLE_H_ */
