/**
 * memo:   重新设计了TCP数据处理对象,使用新的线程对象整体想法，模块化，单一化设计
 * date:   2011/07/21
 * author: humingqing
 *
 * <2012/06/20> 将网络数据接收和发送使用单线程模型来处理数据，这样的前提基于网络上传输的速度远小于CPU的处理能力这个假设下，但实际的这个假设也是存在，
 * 如果出现CPU处理弱于网络传输说明在整个单线程中有比较费时操作，对于数据递交的外部使用流水的方式传输，收到网络数据后批量分包到数据队列中，
 * 然后将数据递交到线程队列中，由线程队列自身向外部递交数据。
 *
 * 整体性能测试，这种方案相对外界出现阻塞时处理性能和效率相对较高，但如果外界应用阻塞严重阻塞自然会出现丢弃情况，但如果短时间内大数据量等数据量减少时恢复正常处理相对较快。
 *
 */
#include "TcpHandle.h"
#include "list.h"
#include <assert.h>
#include "UtilitySocket.h"
#include "protocol.h"
#include <errno.h>
#include <poll.h>
#include <arpa/inet.h>
#ifndef _UNIX
#include <sys/time.h>
#include <sys/resource.h>
#endif

#define MAXQUEUE_LENGTH        102400    // 最大队列长度
#define SOCKET_CLOSE    		0      	// 禁用状态
#define SOCKET_LIVE     		1      	// 启用状态
#define SOCKET_WAITCLOSE 		2       // SOCKET等待关闭状态
#define SOCKET_SENDBUFFER       3		// 发送数据不出
#define SOCKET_EWOULDBLOCK      2		// 发送缓存区满

////////////////////////////CConnection 连接管理对象是 ////////////////////
CSockManager::nodequeue::nodequeue(){
	_head = _tail = NULL ;
	_size = _dref =  0   ;
};
CSockManager::nodequeue::~nodequeue(){
	clear() ;
};

// 添加到头部
void CSockManager::nodequeue::addhead( CSockManager::_node *p )
{
	if ( _head == NULL ) {
		_head = _tail = p ;
		_tail->_next  = NULL ;
		_size         = 1 ;
	} else {
		p->_next  = _head ;
		_head     = p ;
		_size     = _size + 1 ;
	}
}

// 添加节点数
bool CSockManager::nodequeue::addtail( const char *buf, int size )
{
	if ( buf == NULL || size == 0 )
		return false ;

	if( _size >= MAXQUEUE_LENGTH ) {
		// printf( "over max data queue length 102400\n" ) ;
		// 如果缓存的数据大于10M，则通知发送失败, 或者连接被释放
		return false;
	}

	_node * dn = (_node *)malloc(sizeof(_node));
	memset(dn, 0, sizeof(_node));

	dn->_dptr = (char*)malloc(size+1);
	memset(dn->_dptr, 0, size);
	memcpy(dn->_dptr, buf, size);
	dn->_len  = size;
	dn->_next = NULL;

	_size = _size + 1;

	if( _head != NULL ) {
		_tail->_next = dn;
		_tail = dn;
	} else {
		_head = dn;
		_tail = dn;
	}
	return true;
}

// 弹出数据
CSockManager::_node *CSockManager::nodequeue::popnode( void )
{
	if ( _size == 0 )
		return NULL ;

	CSockManager::_node *p = NULL ;
	if ( _head == _tail ) {
		p = _head ;
		_head = _tail = NULL ;
		_size = 0 ;
	} else {
		p = _head ;
		_head = p->_next  ;
		_size = _size - 1 ;
	}
	return p ;
}

// 移到新链上
void CSockManager::nodequeue::moveto( nodequeue *node )
{
	// 如果两个链上的数据不一致需要清理上一次连接数据
	if ( _dref != node->_dref ) {
		clear() ;
	}
	// 如果数据不一致先清理上一次的数据，再判断数据大小
	if ( node->_size == 0 || _size > 1024 )
		return ;

	if ( _tail == NULL ) {
		_tail = node->_tail ;
		_head = node->_head ;
		_size = node->_size ;
	} else {
		_tail->_next = node->_head ;
		_tail        = node->_tail ;
		_size = _size + node->_size ;
	}
	_dref = node->_dref ;

	// 取完就清空数据
	node->reset() ;
}

// 重置
void CSockManager::nodequeue::reset(void)
{
	_head = _tail = NULL;
	_size = 0;
}

// 清理数据
void CSockManager::nodequeue::clear( void )
{
	if ( _size == 0 || _head == NULL )
		return ;

	_node* p 	 = NULL;
	_node* next  = NULL;

	p = _head;
	while( p != NULL ) {
		next = p->_next;
		free(p->_dptr);
		p->_dptr = NULL;
		p->_len = 0;
		free(p);
		p = next;
	}
	_head = NULL ;
	_tail = NULL ;
	_size = 0 ;
}

//////////////////////////  Connection ////////////////////////////////////
// 初始化系统数据
void CSockManager::connect_t::beginsock( void )
{
	_fd        = 0;
	_status    = SOCKET_LIVE;
	_inqueue   = new nodequeue ;
	_outqueue  = new nodequeue ;
	_read_buff = new DataBuffer ;
}

// 析构数据
void CSockManager::connect_t::endsock( void )
{
	if ( _read_buff != NULL ) {
		delete _read_buff ;
		_read_buff = NULL ;
	}

	if ( _inqueue != NULL ){
		delete _inqueue ;
		_inqueue = NULL ;
	}
	if ( _outqueue != NULL ) {
		delete _outqueue ;
		_outqueue = NULL ;
	}
}

// 取得链接数据
CSockManager::_node * CSockManager::connect_t::readlist( void )
{
	CSockManager::_node *p = NULL;

	_mutex.lock() ;
	if ( _inqueue->size() <= 0 ) {
		_mutex.unlock() ;
		return NULL ;
	}
	p = _inqueue->getnodes() ;
	_inqueue->reset() ;
	_mutex.unlock() ;

	return p ;
}

// 是否已经释放连接
bool CSockManager::connect_t::close( void )
{
	if ( _fd <= 0 ) {
		return false  ;
	}
	_status = SOCKET_CLOSE ;
	return true ;
}

// 发送数据
int  CSockManager::connect_t::write( int &nerr )
{
	if ( _fd <= 0 || _status != SOCKET_LIVE ) {
		return SOCKET_FAILED ;
	}
	// 将需要写的数据链移到需要发送的链上来
	_mutex.lock() ;
	_outqueue->moveto( _inqueue ) ;
	_mutex.unlock() ;

	if ( _outqueue->size() == 0 ) {
		// 清空写事件处理
		_mutex.lock() ;
		_eventer->modify( this, ReadableEvent ) ;
		_mutex.unlock() ;

		return SOCKET_SUCCESS ;
	}

	// 记录写入数据的长度
	int wsize = 0 , nsize = 0 ;
	_node *p = _outqueue->popnode() ;
	while( p != NULL && _status == SOCKET_LIVE ) {
	// printf( "send fd %d length %d data %s\n", fd,  p->_len , (const char *) p->_dptr ) ;
		int sendlen = UtilitySocket::BaseSocket::write( _fd, (char*)p->_dptr, p->_len );
		if ( sendlen == p->_len ) {
			// printf( "socket fd %d size %d send length %d\n", pitem->_fd, pitem->_size, sendlen ) ;
			free(p->_dptr);
			free(p);

			// 累加写入的数据
			wsize = wsize + sendlen ;
			// 取得等待发送数据队列长度
			nsize = _outqueue->size() ;
			// 如果写入的数据大于缓存区大小则等待下一个写事件来触发
			if ( wsize < MAX_SOCKET_BUF ) {
				// 如果还有数据需要发送则应该继续处理
				if ( nsize > 0 ) {
					p = _outqueue->popnode() ;
					continue ;
				}
			}

			// 检测一下是否还有数据需要发送
			_mutex.lock() ;
			// 两队列长度累加处理
			nsize = nsize + _inqueue->size() ;
			if ( nsize == 0 ) { // 如果没有数据就将写事件清空
				_eventer->modify( this, ReadableEvent ) ;
			}
			_mutex.unlock() ;

			// 如果还有数据要发送就返回需要发送的数据个数
			return ( nsize > 0 ) ? nsize: SOCKET_SUCCESS ;

		} else if(sendlen > 0 && sendlen < p->_len) {
			// 如果发送部分数据出队列，计算出长度
			p->_len = p->_len - sendlen ;
			// 移动发送出去的数据
			memmove( p->_dptr, p->_dptr+sendlen, p->_len ) ;
			// 放回到头部
			_outqueue->addhead( p ) ;

			// printf( "socket fd %d size %d send length %d\n", pitem->_fd, pitem->_size, sendlen ) ;
			nerr = errno ;

			// 数据没有发送完成，继续处理
			//return SOCKET_CONTINUE ;
			return SOCKET_SENDBUFFER ;

		} else if (sendlen < 0) {
			// 如果发送失败直接添加回队列处理
			_outqueue->addhead( p ) ;
			nerr = errno ;
			if ( errno == EWOULDBLOCK || errno == EAGAIN ){
				//缓冲区满了
				OUT_WARNING( _szIp , _port , NULL, "Send EAGAIN, send buffer is full" ) ;
				// 继续处理
				// return SOCKET_CONTINUE ;
				return SOCKET_EWOULDBLOCK ;
			}
			_status = SOCKET_WAITCLOSE ;
			// 收到断连接请求
			return SOCKET_DISCONN ;
		}
		p = _outqueue->popnode() ;
	}

	// 清空写事件
	_mutex.lock() ;
	_eventer->modify( this, ReadableEvent ) ;
	_mutex.unlock() ;

	return SOCKET_SUCCESS ;
}

// 读取数据
struct packet * CSockManager::connect_t::read( int &ret , int &nerr, IPackSpliter *pack )
{
	struct packet *pdata = NULL ;

	ret = SOCKET_SUCCESS ;

	if ( _fd <= 0 || _status != SOCKET_LIVE ) {
		ret = SOCKET_FAILED ;
		return NULL ;
	}

	while( true ) {
		// 确保有足够的空间
		_read_buff->ensureFree( READ_BUFFER_SIZE ) ;
		// 可用的长度
		int nfreelen = _read_buff->getFreeLen() ;
		// 读取到缓存对象中
		int recvlen = UtilitySocket::BaseSocket::read( _fd, _read_buff->getFree(), nfreelen ) ;
		if( recvlen > 0 ) {
			// LOGDEBUG( NULL, 0 , "recv fd %d length %d data %s\n", fd, recvlen, (const char *) buffer ) ;
			// 处理数据指针
			_read_buff->pourData( recvlen ) ;
			// 如果接收到的数据为整个缓存区就继续读一次
			if ( recvlen == nfreelen ) {
				// 这种情况说明缓存区中还有数据没有接收完成
				// ret = SOCKET_CONTINUE ;
				// return pdata ;
				if ( _read_buff->getLength() < MAX_SOCKET_BUF ) continue ;
			}

			// 拆包处理
			pdata = pack->get_kfifo_packet( _read_buff ) ;

		} else if( recvlen == 0 ) {

			nerr 	= errno ;
			ret  	= SOCKET_DISCONN ;
			_status = SOCKET_WAITCLOSE ;
			return NULL ;

		} else if(recvlen < 0) {

			nerr = errno ;
			if ( errno == EWOULDBLOCK || errno == EAGAIN ) {
				// OUT_WARNING( _szIp , _port, NULL, "RECV EWOULDBLOCK, recv buffer is empty" );
				ret = SOCKET_SUCCESS ;
				return NULL ;
			}

			_status = SOCKET_WAITCLOSE ;
			ret = SOCKET_RECVERR ;

			return NULL ;
		}

		// 只处理拆包成功有效时间的更新
		_last = time(NULL) ;

		return pdata ;
	}
	return pdata ;
}

// 添加需要发送的数据
bool CSockManager::connect_t::deliver( const char *buf, const int len )
{
	_mutex.lock() ;
	// 如果不为存活状态就直接返回了
	if ( _status != SOCKET_LIVE || len <= 0 ) {
		_mutex.unlock() ;
		OUT_ERROR( _szIp, _port , "Env", "fd %u, message len %d, socket state %d", _fd, len, _status ) ;
		return false ;
	}

	bool success = _inqueue->addtail( buf, (int) len ) ;
	if ( ( !(_events & WritableEvent ) ) && success ) {
		// 如果有数据就将状态设为有数据要写
		_eventer->modify( this,  ReadableEvent|WritableEvent ) ;
	}
	_mutex.unlock() ;
	// 打印入队列错误的日志
	if ( ! success ) {
		OUT_ERROR( _szIp, _port, "Env", "fd %d, inqueue over max send length" , _fd ) ;
		return false ;
	}
	return true ;
}

// 设置IP和端口
void CSockManager::connect_t::init( unsigned int fd, const char *szip , unsigned short port )
{
	_fd    = fd ;
	_port  = port ;
	if ( szip != NULL ) {
		// 添加IP地址
		strcpy( _szIp , szip ) ;
	}
	_status = SOCKET_LIVE;
	// 清理掉原来所有数据
	_read_buff->resetBuf() ;

	// 将需要写的数据链移到需要发送的链上来
	_mutex.lock() ;
	_inqueue->clear() ;
	_inqueue->addref() ;
	_mutex.unlock() ;
}

// 是否超时没用任保操作
bool CSockManager::connect_t::check( void )
{
	return ( _status == SOCKET_LIVE ) ;
}

//////////////////////////////////////////// 连接管理对象重复使用内存  /////////////////////////////////////////
// 签出数据
socket_t * CSockManager::get( int sockfd, const char *ip, unsigned short port , bool queue )
{
	connect_t *p = NULL ;

	_mutex.lock() ;
	p = (connect_t *) _queue.pop() ;

	if ( p == NULL ) {
		p = new connect_t(_eventer) ;
	}
	p->_type = FD_TCP ;
	p->init( sockfd, ip, port ) ;
	p->_last = time(NULL) ;  // 最后一次使用时间
	p->_ptr  = NULL ; // 第三方扩展数据
	p->_next = NULL ;
	p->_pre  = NULL ;

	// 分配资源的是否需要由线程对象统一管理
	if ( queue ) {
		// 将用户添加到在线队列中管理
		_online.push( p ) ;
		_index.insert( std::set<socket_t*>::value_type(p) ) ;
	}
	_mutex.unlock() ;

	return p ;
}

// 签入数据
void CSockManager::put( socket_t *fd )
{
	_mutex.lock() ;
	// 先从在线队列索引里面查找
	std::set<socket_t*>::iterator it = _index.find( fd ) ;
	if ( it != _index.end() ) {
		_index.erase( it ) ;
		_online.erase( fd ) ;
	}
	_queue.push( fd ) ;
	_mutex.unlock() ;
}

// 回收所有资源
void CSockManager::clear( void )
{
	_mutex.lock() ;
	_index.clear() ;
	_mutex.unlock() ;
}

// 关闭连接
bool CSockManager::close( socket_t *sock )
{
	_mutex.lock() ;
	// 先从在线队列索引里面查找
	std::set<socket_t*>::iterator it = _index.find( sock ) ;
	if ( it == _index.end() ) {
		_mutex.unlock() ;
		return false ;
	}
	_index.erase( it ) ;

	_online.erase( sock ) ;
	_recyle.push( sock ) ;
	_mutex.unlock() ;

	return true ;
}

// 取得所有回收连接对象
socket_t * CSockManager::recyle()
{
	socket_t *p = NULL ;
	_mutex.lock() ;
	int size = 0 ;
	p = _recyle.move( size ) ;
	if ( size == 0 ) {
		p = NULL ;
	}
	_mutex.unlock() ;
	return p ;
}

// 检测超时连接对象
int CSockManager::check( int timeout, std::list<socket_t*> &lst )
{
	int count = 0 ;

	_mutex.lock() ;
	if ( _online.size() == 0 ) {
		_mutex.unlock() ;
		return false ;
	}

	time_t now = time(NULL) - timeout ;
	socket_t *p = _online.begin() ;
	while( p != NULL ) {
		if ( now > p->_last ) {
			lst.push_back( p ) ;
			++ count ;
		}
		p = p->_next ;
	}
	_mutex.unlock() ;

	return count ;
}

/////////////////////////////////////// 处理网络数据CTcpHandle对象 //////////////////////////////////////////

CTcpHandle::CTcpHandle():
	_tcp_init(false) , _pack_spliter(&_defaultspliter), _socktimeout(SOCKET_TIMEOUT)
{
	_server_fd   = NULL ;
	// 将每个线程分配不同处理数据队列
	_packqueue   = new CDataQueue<CPacket>(MAXQUEUE_LENGTH);
	_queuethread = new CQueueThread( _packqueue, this ) ;
	_socketmgr   = new CSockManager(this) ;
}

CTcpHandle::~CTcpHandle()
{
	uninit() ;
	// 放回对象,具有自动回收能力,当引用为零时自动回收对象
	if ( _socketmgr != NULL ) {
		delete _socketmgr ;
		_socketmgr = NULL ;
	}

	if ( _queuethread != NULL ) {
		delete _queuethread ;
		_queuethread = NULL ;
	}
	// 释放数据队列
	if ( _packqueue != NULL ) {
		delete _packqueue ;
		_packqueue = NULL ;
	}
	if ( _server_fd != NULL ) {
		delete _server_fd ;
		_server_fd = NULL ;
	}
}

//初始化线程池，异步socket，使用前必须调用该函数
//windows下一定要大于5000，因为windows套接字不是从1开始的，需要改正
bool CTcpHandle::init( unsigned int nthread , unsigned int timeout )
{
	FUNTRACE("bool CTcpHandle::init(int max_fd /*= MAX_SOCKET_NUM*/)");

	/*
	* 设置每个进程允许打开的最大文件数
	*/
#ifndef _UNIX
	struct rlimit rt;
	rt.rlim_max = rt.rlim_cur = MAX_FD_OFFSET ;
	if (setrlimit(RLIMIT_NOFILE, &rt) == -1)
	{
		perror("setrlimit\n");
	}
#endif

	destroy();

	bool ret = create(MAX_FD_OFFSET);
	if ( !ret )
	{
		OUT_ERROR(NULL, 0, NULL, "CEpollHandle::create(max_fd:%d);", MAX_FD_OFFSET );
		return ret;
	}

	// 检测TCP连接超时时间
	_socktimeout  = timeout ;
	_tcp_init 	  = true ;
	// 单线程处理模型
	if ( ! _thread_conn.init( 1 , this, this ) ){
		OUT_ERROR( NULL, 0, NULL, "CTcpHandle::init connect thread failed" ) ;
		return false ;
	}
	// 检测连接超时处理
	if ( ! _thread_check.init( 1, NULL, this ) ) {
		OUT_ERROR( NULL, 0, NULL, "CTcpHandle::init connect check thread failed" ) ;
		return false ;
	}
	// 数据处理线程
	if ( !_queuethread->Init(nthread) ) {
		OUT_ERROR( NULL, 0, NULL, "CTcpHandle::init data queue thread failed" ) ;
		return false ;
	}
	_thread_conn.start() ;
	_thread_check.start() ;

	return true ;
}

bool CTcpHandle::uninit()
{
	FUNTRACE("bool CTcpHandle::uninit_fds()");

	if ( ! _tcp_init )
		return false ;

	_tcp_init = false ;
	_thread_check.stop() ;
	_thread_conn.stop() ;
	_queuethread->Stop() ;

	if ( _server_fd != NULL ) {
		::close( _server_fd->_fd ) ;
	}

	destroy();

	return true ;
}

bool CTcpHandle::start_server( int port , const char* ip )
{
	FUNTRACE("bool CTcpHandle::start_server(int port, const char* ip, int max_fd = MAX_SOCKET_NUM)");

	bool ret = false;

	if ( !_tcp_init ){
		OUT_ERROR( NULL, 0, NULL, "CTcpHandle::start_server not init" ) ;
		return false;
	}
	// 如果端口为零则为客户端
	if ( port == 0  ) return true ;

	int fd = UtilitySocket::BaseSocket::socket() ;
	if ( fd == -1 )
	{
		OUT_ERROR(NULL,0,NULL,"BaseSocket::socket:%s",strerror(errno));
		return false;
	}

	UtilitySocket::BaseSocket::setNonBlocking(fd) ;
	UtilitySocket::BaseSocket::setReuseAddr(fd) ;
	UtilitySocket::BaseSocket::setLinger( fd ) ;
	// UtilitySocket::BaseSocket::setNoCheckSum( _server_fd ) ;
	UtilitySocket::BaseSocket::setRecvBuf( fd, MAX_SOCKET_BUF ) ;
	UtilitySocket::BaseSocket::setSendBuf( fd, MAX_SOCKET_BUF ) ;

	ret = UtilitySocket::BaseSocket::bind( fd , port, ip);
	if ( !ret )
	{
		OUT_ERROR(NULL,0,NULL,"BaseSocket::bind:%s",strerror(errno));
		UtilitySocket::BaseSocket::close( fd );
		return false;
	}

	ret = UtilitySocket::BaseSocket::listen( fd , 10);
	if ( !ret )
	{
		OUT_ERROR(NULL,0,NULL,"BaseSocket::listen:%s",strerror(errno));
		UtilitySocket::BaseSocket::close( fd );
		return false;
	}

	_server_fd = _socketmgr->get( fd , ( ip == NULL ) ? "0.0.0.0" : ip , port , false ) ;
	if ( _server_fd == NULL ) {
		OUT_ERROR(NULL,0,NULL,"init server fd object failed" );
		UtilitySocket::BaseSocket::close( fd );
		return false;
	}
	add(_server_fd, ReadableEvent/*|WritableEvent*/);//

	OUT_PRINT(NULL, 0, NULL, "tcp, start_server ok, fd:%d", _server_fd);
	INFO_PRT("CTcpHandle start server %s:%d.........success",ip,port);

	return true;
}

bool CTcpHandle::stop_server()
{
	FUNTRACE("bool CTcpHandle::stop_server()");

	UtilitySocket::BaseSocket::close(_server_fd->_fd);

	return true;
}

//将应用层数据投递到fd缓冲区，等待线程池的调度发送。
bool CTcpHandle::deliver_data( socket_t *sock, const void* data, int len )
{
	if ( sock == NULL ) return false ;
	// 添加到发送数据队列中
	return ((CSockManager::connect_t*)sock)->deliver( (const char*)data, len ) ;
}

//调用on_disconnection
void CTcpHandle::close_socket( socket_t *sock , bool notify )
{
	if ( sock == NULL ) return ;
	// 先入队列，然后再由单线程来回收
	if ( ! _socketmgr->close( sock ) ){
		return ;
	}
	/**
	// 处理连接前没有发送出去的数据
	CSockManager::_node *p = ((CSockManager::connect_t*)sock)->readlist() ;
	if ( p != NULL ) {
		CSockManager::_node *tmp ;
		while( p != NULL ) {
			tmp = p ;
			p   = p->_next ;
			if ( tmp->_dptr != NULL ) {
				// 处理关闭连前没有发送出去的数据
				on_send_failed( sock, tmp->_dptr, tmp->_len ) ;
				free(tmp->_dptr) ;
			}
			free(tmp) ;
		}
	}*/

	// 回调断连
	if ( notify )
		on_dis_connection( sock ) ;
}

//不调用on_disconnection
bool CTcpHandle::close_fd( socket_t *sock )
{
	//放到锁外面，否则是死锁
	if ( ! del( sock , ReadableEvent|WritableEvent ) ) {
		OUT_ERROR( sock->_szIp , sock->_port , "ENV", "epoll del fd %d failed" , sock->_fd ) ;
		return false ;
	}

	// 关闭对象
	if ( !((CSockManager::connect_t*)sock)->close() ) {
		OUT_ERROR( sock->_szIp, sock->_port , "ENV", "fd %d socket manager delete failed", sock->_fd ) ;
		return false ;
	}

	// 关闭SOCKET连接的处理
	UtilitySocket::BaseSocket::close( sock->_fd ) ;
	// 打印日志记录断开连接
	OUT_INFO( sock->_szIp , sock->_port , "ENV", "close_socket fd:%d closed", sock->_fd );

	// 处理断开的连接
	return true ;
}

// 处理事件函数
void CTcpHandle::on_event( socket_t *sock, int events )
{
	if( is_read(events) )
	{
		//新连接到来
		if( sock == _server_fd ) {
			sockaddr_in sa;

			while(1)  { //可能有同时有多个连接上来，需要轮询
				memset(&sa, 0, sizeof(sa));
				socklen_t salen = sizeof(sa);
				int client = accept(_server_fd->_fd, (struct sockaddr *)&sa, &salen);
				if( client < 0 ) {
					// OUT_ERROR(NULL, 0, NULL, "accept, errno:%d, %s, ret is %d", errno, strerror(errno), client);
					return;
				}

				const  char* ip     = inet_ntoa(sa.sin_addr); // 117.136.26  221.7.7
				unsigned short port = ntohs(sa.sin_port);
				// 是否可以接入的IP的或者IP段
				if ( ! invalidate_ip( ip ) ) {
					// 在IP黑名单内
					OUT_ERROR( ip, port, "ENV", "in black ip list fd %d", client ) ;
					UtilitySocket::BaseSocket::close( client ) ;
					return ;
				}
				OUT_CONN( ip , port, "ENV", "one new connection come from %s, port %d, fd is %d", ip, port, client ) ;

				UtilitySocket::BaseSocket::setNonBlocking(client);

				socket_t *csock = _socketmgr->get( client , ip , port ) ;
				if ( csock == NULL ) {
					OUT_ERROR( ip , port , "ENV", "connection over max fd , fd %d" , client ) ;
					UtilitySocket::BaseSocket::close(  client ) ;
					return ;
				}

				// 先添加到事件队列中
				if( ! add( csock , ReadableEvent/*|WritableEvent*/) ) {
					OUT_ERROR( ip, port, "ENV", "add epoll ctrl failed , fd %d" , client ) ;
					_socketmgr->put( csock ) ;
					UtilitySocket::BaseSocket::close( client ) ;
					return ;
				}
				// 添加管理对队列中
				on_new_connection( csock, ip, port ) ;
			}

		} else { //socket上数据到来。
			// printf("tcp fd:%d can be read!\n", fd);
			read_data( sock ) ;
		}
	}

	//通知Socket fd 可写。
	if(  is_write(events) ) { //当TCP发送缓冲区满时，此处执行地可能会比较频繁。
		//LOGDEBUG(NULL, 0, "tcp fd:%d can be written!", fd);
		write_data( sock ) ;
	}
}

// 交出数据回调接口
void CTcpHandle::HandleQueue( void *packet )
{
	CPacket *tmp = ( CPacket *) packet ;
	struct list_head *head = (struct list_head *) tmp->_pack ;
	struct packet* one;
	struct list_head *q, *next;

	list_for_each_safe(q, next, head) {
		one = list_entry(q, struct packet, list);
		// 交出数据包，处理数据
		on_data_arrived( (socket_t*)tmp->_sock , (void *)one->data, (int) one->len ) ;
		_pack_spliter->free_kfifo_packet( one ) ;
	}
	free( head ) ;
	tmp->_pack = NULL ;
}

// 读数据
void CTcpHandle::read_data( socket_t *sock )
{
	int err = 0 , ret = 0 ;
	CSockManager::connect_t *conn = ( CSockManager::connect_t *)sock ;
	do {
		struct packet *lst = conn->read( ret, err, _pack_spliter ) ;
		if ( lst == NULL ) {
			continue ;
		}
		CPacket *pack = new CPacket( sock, lst ) ;
		if ( ! _queuethread->Push( pack ) ) {
			delete ( pack ) ;
		}
	} while( ret == SOCKET_CONTINUE ) ;

	if ( ret != SOCKET_SUCCESS ){
		write_errlog( sock, ret, err ) ;
		close_socket( sock ) ;
	}
}

// 写数据
bool CTcpHandle::write_data( socket_t *sock )
{
	int err = 0 ;
	CSockManager::connect_t *conn = ( CSockManager::connect_t *) sock ;
	// printf( "begin write fd %d\n", fd ) ;
	int ret = conn->write( err ) ;
	while ( ret == SOCKET_CONTINUE ) {
		ret = conn->write( err ) ;
	}
	if ( ret < SOCKET_SUCCESS ){
		write_errlog( sock, ret, err ) ;
		close_socket( sock ) ;
	}
	return ( ret == SOCKET_SUCCESS ) ;
}

void CTcpHandle::write_errlog( socket_t *sock, int err , int nerr )
{
	const char *serr = "Unknow Error" ;
	switch( err ) {
		case SOCKET_FAILED:
			serr = "Send Data Failed" ;
			break;
		case SOCKET_DISCONN:
			serr = "Recv Disconnection" ;
			break;
		case SOCKET_RECVERR:
			serr = "Recv data error" ;
			break;
		case SOCKET_SENDERR:
			serr = "Send data error" ;
			break;
	}
	OUT_ERROR( sock->_szIp , sock->_port , "ENV", "%s, tcp fd %d, errno %d, errmsg %s" , serr, sock->_fd , nerr , strerror(nerr) ) ;
}

// 处是连接状检测线程
void  CTcpHandle::process_check( void )
{
	time_t last_check = time(NULL) ;
	while (_tcp_init ){
		// 连接状态检测线程
		time_t now = time(NULL) ;
		if ( now - last_check < SOCKET_CHECK ) {
			sleep(3) ;
			continue ;
		}
		last_check = now ;

		std::list<socket_t *> lst ;
		int count = _socketmgr->check( _socktimeout, lst ) ;
		if ( count == 0 )  {
			sleep(3) ;
			continue ;
		}

		std::list<socket_t *>::iterator it ;
		for ( it = lst.begin(); it != lst.end(); ++ it ) {
			close_socket( *it ) ;
		}
		lst.clear() ;

		sleep(3) ;
	}
}

// 处理用户连接数据线程
void  CTcpHandle::process_data( void )
{
	while (_tcp_init ){
		// 单连接管理线程对象，处理连接数据的接收
		int n = poll(1);
		if ( n < 0 ){
			usleep(5*1000*1000);
		}

		// 处理需要关闭的连接
		socket_t *p = _socketmgr->recyle() ;
		if ( p != NULL ) {
			socket_t *tmp = p  ;
			while( p != NULL ) {
				tmp = p ;
				p   = p->_next ;

				close_fd( tmp ) ;

				_socketmgr->put( tmp ) ;
			}
		}
	}
}

// 线程运行对象
void CTcpHandle::run( void *param )
{
	if ( param == this ) {
		process_data() ;
	} else {
		process_check() ;
	}
}

//返回fd
socket_t * CTcpHandle::connect_nonb(const char* ip, int port, int nsec )
{
	int sockfd = -1 , ret = 0 ;

	if((sockfd=UtilitySocket::BaseSocket::socket(SOCK_STREAM))<0){
		perror("socket error:");
		return NULL ;
	}

	// 获取当前socket的属性， 并设置 noblocking 属性
	errno = 0;

	UtilitySocket::BaseSocket::setNonBlocking( sockfd );
	// UtilitySocket::BaseSocket::setNoCheckSum( sockfd ) ;

	if ( UtilitySocket::BaseSocket::connect( sockfd, ip , port ) )
		goto done ;

	errno = UtilitySocket::BaseSocket::getError() ;
	if ( errno != EINPROGRESS ) {
		OUT_ERROR(ip,port,NULL,"connect:%s",strerror(errno) );
		UtilitySocket::BaseSocket::close( sockfd ) ;
		return NULL ;
	}

	struct pollfd fds[1];
	memset(fds, 0 , sizeof(fds));
	fds[0].fd 	  = sockfd ;
	fds[0].events = POLLOUT | POLLIN | POLLERR | POLLHUP | POLLNVAL | POLLPRI;

	ret = ::poll(fds, 1, nsec*1000 ) ;
	// 可以做任何其它的操作
	if ( ret > 0 ) {
		// Ensure the socket is connected and that there are no errors set
		int val;
		socklen_t lon = sizeof(int);
		int ret2 = getsockopt(sockfd, SOL_SOCKET, SO_ERROR, (void *)&val, &lon);
		if (ret2 == -1) {
		  OUT_ERROR( ip, port, "CONN", "CTcpHandle::connect_nonb() getsockopt() errno %d, %s" , errno, strerror(errno) );
		}
		// no errors on socket, go to town
		if (val == 0) {
		  goto done;
		}
		OUT_ERROR( ip, port , "CONN","CTcpHandle::connect_nonb() error on socket (after poll) , errno %d, %s" , errno, strerror(errno) ) ;
	} else if ( ret == 0) {
		// socket timed out
		OUT_ERROR( ip, port, "CONN","CTcpHandle::connect_nonb() timed out ,errno %d, %s" , errno, strerror(errno) ) ;
	} else {
		// error on poll()
		OUT_ERROR( ip, port, "CONN","CTcpHandle::connect_nonb() poll() , errno %d, %s" , errno, strerror(errno) ) ;
	}

	UtilitySocket::BaseSocket::close( sockfd ) ;
	return  NULL;

done:
	socket_t *sock = _socketmgr->get( sockfd, ip, port ) ;
	if ( sock == NULL ) {
		printf( "Connect to server OK, fd:%d, init fd failed\n" , sockfd ) ;
		UtilitySocket::BaseSocket::close( sockfd ) ;
		return NULL;
	}
	OUT_PRINT( sock->_szIp, sock->_port, NULL, "Connect to TCP server OK, fd:%d", sockfd );
	add( sock , ReadableEvent/*|WritableEvent*/ ) ;

	return sock ;
}
