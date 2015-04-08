#include <list.h>
#include "asynchttpclient.h"

//////////////////////////////////////////////////////////////////////////
CAsyncHttpClient::CAsyncHttpClient():_initalized(false)
{
	_maxsize     = 1000 ;
	_seq_id		 = 0 ;
	_pCallbacker = NULL ;
}

CAsyncHttpClient::~CAsyncHttpClient()
{
	Stop() ;
}

// 开始线程
bool CAsyncHttpClient::Start( unsigned int nsend , unsigned int nrecv )
{
	// 设置数据分包对象
	_tcp_handle.setpackspliter( this ) ;
	// 初始化线程
	_tcp_handle.init( nrecv, 60 );

	_initalized = true ;

	if ( !_check_thread.init( nsend , NULL , this ) )
	{
		printf( "start check thread failed, %s:%d", __FILE__, __LINE__ ) ;
		_initalized = false ;
		return false ;
	}
	_check_thread.start() ;

	return true ;
}

// 停止线程
void CAsyncHttpClient::Stop( void )
{
	if ( ! _initalized )
		return ;

	_initalized = false ;

	_check_thread.stop() ;

	_tcp_handle.uninit() ;
}

// 发送HTTP请求，实际上就先入处理队列
int CAsyncHttpClient::HttpRequest( CHttpRequest& request , unsigned int req_id )
{
	// 根据HTTP实际响应能力来进行调度管理
	if ( _maxsize > 0 && _list_req.GetQueueSize() >= _maxsize ) {
		// 如果请求队列中HTTP大于最大HTTP的长度就直接返回了
		return E_HTTPCLIENT_REQQUEUELENGTH ;
	}

	const char* send_data = NULL ;
	int len = 0 ;

	string server ;
	unsigned short port ;
	send_data = request.GetHTTPRequest( server , port , len ) ;

	if ( send_data == NULL || len == 0 )
		return E_HTTPCLIENT_REQUESTINVALID ;

	if ( strlen( server.c_str() ) == 0 )
		return E_HTTPCLIENT_SERVERINVALID ;

	_REQ_DATA *req = _list_req.AllocReq() ;
	req->_fd   = 0 ;
	req->_seq  = req_id ;
	req->_port = port ;
	req->_ref  = 0 ;
	req->_time = time(NULL) ;
	req->_ip.SetString( server.c_str() ) ;
	req->_senddata.SetString( send_data, len ) ;

	// 添加到处理队列中
	_list_req.PushReq( req ) ;

	return E_HTTPCLIENT_SUCCESS ;
}

// 处理请求，由外部线程来处理
void CAsyncHttpClient::ProcessWaitReq( void )
{
	_REQ_DATA *req = _list_req.PopReq() ;
	if ( req == NULL ) {
		return ;
	}
	// 修改HTTP调用阻塞的时间，不然大部HTTP调用都将阻塞的连接等待上面
	socket_t *sockfd = _tcp_handle.connect_nonb( (const char *)req->_ip , req->_port , 1 ) ;
	// 如果连接服务器失败则直接返回了
	if ( sockfd == NULL ) {
		if ( _pCallbacker ) {
			CHttpResponse resp ;
			_pCallbacker->ProcHTTPResponse( req->_seq, E_HTTPCLIENT_SERVERINVALID , resp ) ;
		}
		_list_req.FreeReq( req ) ;
		return ;
	}
	req->_fd = sockfd ;

	_mutex_ref.lock() ;
	sockfd->_ptr = req ;
	++ req->_ref ;
	_mutex_ref.unlock() ;

	// 发送HTTP请求数据
	if ( ! _tcp_handle.deliver_data( sockfd, (const void*) req->_senddata.GetBuffer(), req->_senddata.GetLength() ) ) {
		// 发送失败移除
		if ( _pCallbacker ) {
			CHttpResponse resp ;
			_pCallbacker->ProcHTTPResponse( req->_seq, E_HTTPCLIENT_REQUESTINVALID , resp ) ;
		}
		OUT_ERROR( (const char *)req->_ip , req->_port , "AsynHTTP" , "send data error %s" , req->_senddata.GetBuffer() ) ;

		_mutex_ref.lock() ;
		sockfd->_ptr = NULL ;
		_list_req.FreeReq( req ) ;
		_mutex_ref.unlock() ;

		// 关闭连接
		_tcp_handle.close_socket( sockfd ) ;
	}
}

// 运行线程
void CAsyncHttpClient::run( void *param )
{
	while( _initalized ) {
		// 处理等待队列中请求
		ProcessWaitReq() ;
	}
}

void CAsyncHttpClient::on_data_arrived(socket_t *sock, const void* data, int len)
{
	_mutex_ref.lock() ;
	// 接收到数据到来
	if ( sock->_ptr == NULL ) {
		_mutex_ref.unlock() ;
		return ;
	}

	_REQ_DATA *req = (_REQ_DATA*) sock->_ptr ;
	// 处理关闭连接是需要处理释放
	++ req->_ref ;
	int seqid = req->_seq ;
	sock->_ptr = NULL ;
	// 移除索引
	_list_req.FreeReq( req ) ;

	_mutex_ref.unlock() ;

	CHttpResponse resp ;
	// 分析HTTP服务器返回的数据
	int ret = resp.Parse( (const char *)data , len ) ;
	if ( _pCallbacker ) {
		// 回调接口处理数据
		_pCallbacker->ProcHTTPResponse( seqid , ( ret == HTTPPARSER_ERR_SUCCESS ) ? E_HTTPCLIENT_SUCCESS : E_HTTPCLIENT_FAILED , resp ) ;
	}

	// 发送成功关闭连接
	_tcp_handle.close_socket( sock ) ;
}

// 清理超时请求附带的SOCKET上面的数据
void CAsyncHttpClient::on_dis_connection( socket_t *fd )
{
	_mutex_ref.lock() ;
	if ( fd->_ptr == NULL ) {
		_mutex_ref.unlock() ;
		return ;
	}

	_REQ_DATA * p = (_REQ_DATA*) fd->_ptr ;
	if ( --p->_ref <= 0 ) {
		fd->_ptr = NULL ;
		_list_req.FreeReq( p ) ;
	}
	_mutex_ref.unlock() ;
}

// 取得请求序号
unsigned int CAsyncHttpClient::GetSequeue( void )
{
	share::Guard guard( _mutex_seq ) ;
	{
		if ( _seq_id == 0xffffffff ) {
			_seq_id = 0 ;
		}
		return ++ _seq_id ;
	}
}

// KIFO有什么数据全部交到外面处理
struct packet * CAsyncHttpClient::get_kfifo_packet( DataBuffer *fifo )
{
	int len = fifo->getLength() ;
	if ( len <= 0 || len > MAX_PACK_LEN ){
		fifo->resetBuf() ;
		return NULL;
	}

	char* begin = (char *) fifo->getBuffer() ;
	//printf("%s\n",pDeliverBuffer);
	int ret = CHttpParser::DetectCompleteReq( begin , len ) ;
	if ( ret != HTTPPARSER_ERR_SUCCESS ){
		// 如果数据没有完成，或者数据错误直接返回了
		return NULL ;
	}
	// printf( "result code: %d\n" , ret ) ;

	struct list_head *packet_list_ptr = NULL;
	struct packet *item = (struct packet *) malloc(sizeof(struct packet));
	if (item == NULL)
		return (struct packet *) packet_list_ptr;

	item->data = (unsigned char *) malloc( len + 1 );
	memset( item->data, 0, len+1 );
	//!! begin copy data from the second '[' and end with first ']'
	memcpy(item->data, begin, len);
	item->len  = len;
	item->type = E_PROTO_OUT;

	if (packet_list_ptr == NULL){
		packet_list_ptr = (struct list_head *) malloc(sizeof(struct list_head));
		if (packet_list_ptr == NULL)
			return NULL;

		INIT_LIST_HEAD(packet_list_ptr);
	}

	list_add_tail(&item->list, packet_list_ptr);

	fifo->resetBuf() ;
	//printf("get packet total %d in packets , %d out packets \n", in_counter, out_counter);

	return (struct packet*) packet_list_ptr;
}

////////////////////////////////////// 等待处理请求的队列  ///////////////////////////////////////////
CAsyncHttpClient::CWaitListReq::CWaitListReq()
{
	_size     = 0 ;
	_waitsize = 0 ;
}

CAsyncHttpClient::CWaitListReq::~CWaitListReq()
{
	share::Guard guard( _mutex ) ;
	if ( _list_req.empty() ) {
		return ;
	}

	CListReq::iterator it ;
	for ( it = _list_req.begin(); it != _list_req.end(); ++ it ) {
		delete *it ;
	}
	_list_req.clear() ;
}

void CAsyncHttpClient::CWaitListReq::PushReq( _REQ_DATA *data )
{
	// 存放数据
	_mutex.lock() ;
	_list_req.push_back( data ) ;
	++ _size ;
	_mutex.unlock() ;

	_monitor.notify() ;
}

CAsyncHttpClient::_REQ_DATA * CAsyncHttpClient::CWaitListReq::PopReq ( void )
{
	share::Synchronized s(_monitor) ;
	{
		_REQ_DATA *req = NULL ;

		_mutex.lock() ;
		if ( _list_req.empty() ) {
			_mutex.unlock() ;
			_monitor.wait( 3 ) ;

			_mutex.lock() ;
			if ( _list_req.empty() ) {
				_mutex.unlock() ;
				return NULL ;
			}
		}

		req = _list_req.front() ;
		_list_req.pop_front() ;
		-- _size ;
		++ _waitsize ;
		_mutex.unlock() ;

		return req ;
	}
}

// 取得等待发送和等待响应队列中元素个数和
int CAsyncHttpClient::CWaitListReq::GetQueueSize( void )
{
	share::Guard guard( _mutex ) ;
	// 返回数据队列发送和等响应之和
	return ( _size + _waitsize );
}

// 开辟空间
CAsyncHttpClient::_REQ_DATA *CAsyncHttpClient::CWaitListReq::AllocReq( void )
{
	_REQ_DATA *req = NULL ;

	_mutex.lock() ;
	req = _allocmgr.alloc() ;
	if ( req == NULL ) {
		req = new _REQ_DATA ;
	}
	_mutex.unlock() ;

	return req ;
}

// 回收对象
void CAsyncHttpClient::CWaitListReq::FreeReq( _REQ_DATA *req )
{
	_mutex.lock() ;
	if ( req == NULL ) {
		_mutex.unlock() ;
		return ;
	}
	_allocmgr.free( req ) ;
	-- _waitsize ;
	_mutex.unlock() ;
}

