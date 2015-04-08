/*
 * httpserver.cpp
 *
 *  Created on: 2012-2-29
 *      Author: humingqing
 */

#include "httpserver.h"
#include "httpparser.h"

static const char* FAILED_RESPONSE  = "HTTP/1.0 500 Request Illegal\r\n\r\n" ;
static const char* SUCCESS_RESPONSE = "HTTP/1.0 200 OK\r\n\r\n" ;

CHttpServer::CHttpServer(IHTTPServerContainer* pContainer):_initalized(false)
{
	_pContainer = pContainer ;
}

CHttpServer::~CHttpServer()
{
	Stop() ;
}

/**
 * 启动服务器
 */
int CHttpServer::Start( unsigned int thread, unsigned short port )
{
	if ( _initalized )
		return HTTPSVR_ERR_ALREADYSTART ;

	// 服务器参数错误
	if ( thread == 0 || port == 0 )
		return HTTPSVR_ERR_PARAMERROR ;

	// 设置HTTP数据分包对象
	_tcp_handle.setpackspliter( this ) ;

	// 初始化HTTP服务器的线程
	if ( ! _tcp_handle.init( thread , 60 ) ) {
		return HTTPSVR_ERR_STARTTHREADPOOLFAILED ;
	}

	// 开始服务器监听
	if( !_tcp_handle.start_server( port, "0.0.0.0" ) ){
		return HTTPSVR_ERR_LISTENFAILED;
	}

	_initalized = true ;

	return HTTPSVR_ERR_SUCCESS ;
}

/**
 * 停止服务器
 */
void CHttpServer::Stop( void )
{
	if ( ! _initalized )
			return ;

	_initalized = false ;
	_tcp_handle.uninit() ;
}

// 数据到来
void CHttpServer::on_data_arrived( socket_t *sock,  const void* data, int len )
{
	// 交给服务器容器处理
	CServerHttpResp http_resp ;

	const char* resp = SUCCESS_RESPONSE ;
	int resp_size = strlen( resp ) ;
	int ret = _pContainer->ProcRequest( (const char *)data , len , &http_resp ) ;
	if ( ret != SERVERCONTAINER_ERR_SUCCESS ){
		resp = FAILED_RESPONSE ;
		resp_size = strlen( resp ) ;
	}else{
		resp = http_resp.GetRespData( resp_size ) ;
	}
	// 给客户机回送响应
	_tcp_handle.deliver_data( sock, resp, resp_size ) ;
}

// 分包处理
struct packet * CHttpServer::get_kfifo_packet( DataBuffer *fifo )
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




