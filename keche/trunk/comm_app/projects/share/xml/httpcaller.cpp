#include <tools.h>
#include <comlog.h>
#include "httpcaller.h"

CHttpCaller::CHttpCaller()
{
	_send_thread = 1 ;
	_recv_thread = 1 ;
	_response    = NULL ;
	_queue_size  = 10000 ;
}

CHttpCaller::~CHttpCaller()
{
	Stop() ;
}

// 初始化
bool CHttpCaller::Init( int sendthread , int recvthread , int queuesize )
{
	// 发送线程
	_send_thread = sendthread ;
	// 接收线程
	_recv_thread = recvthread ;
	// HTTP请求最大的队列长度
	_queue_size = queuesize ;
	// 设置HTTP队列最大长度
	_httpClient.SetQueueSize( _queue_size ) ;
	// 设置数据回调对象
	_httpClient.SetDataProcessor(this);

	return true ;
}

// 启动
bool CHttpCaller::Start( void )
{
	return _httpClient.Start( _send_thread, _recv_thread ) ;
}

// 停止
void CHttpCaller::Stop( void )
{
	_httpClient.Stop() ;
}

static bool BuildHttpRequest( CHttpRequest &req, const char *sUrl, const char *sData, const int len , const char *ctype )
{
	req.SetMethod( CHttpRequest::HTTP_METHOD_POST ) ;
	req.SetURL( (char*)sUrl ) ;
	req.SetContentType( ctype ) ;
	req.SetPostData( (char *)sData, (int)len ) ;

	return true ;
}

// 处理XML请求服务
bool CHttpCaller::Request( unsigned int seqid, const char *url, const char *data , const int len , const char *ctype )
{
	CHttpRequest req;
	// text/xml; charset=utf-8
	if ( data != NULL ) {  // 如果POST数据不为空
		BuildHttpRequest( req, url , data , len , ctype ) ;
	} else {
		req.SetMethod( CHttpRequest::HTTP_METHOD_GET ) ;
		req.SetURL( url ) ;
	}
	//OUT_INFO(NULL, 0, "HTTP Test",xml_strl.c_str());

	int ret = _httpClient.HttpRequest(req, seqid) ;
	if ( ret != E_HTTPCLIENT_SUCCESS ){
		OUT_ERROR( NULL, 0, NULL, "send sequeue %d url %s failed, http error: %d", seqid , url , ret ) ;
		return false ;
	}
	OUT_PRINT( NULL, 0, NULL, "Request seq id %d, data length: %d", seqid , len ) ;

	return true;
}

// 取得请求序号
unsigned int CHttpCaller::GetSequeue( void )
{
	return _httpClient.GetSequeue();
}

//-----------------------------------------------------------------------------------------------------
// 异步的HTTP的回调接口处理
void CHttpCaller::ProcHTTPResponse( unsigned int seq , const int err , const CHttpResponse& resp )
{
	OUT_PRINT( NULL, 0, NULL, "ProcHTTPResponse seq id %d, http response code: %d", seq , err ) ;
	if ( _response == NULL )
		return ;

	const char *ptr = resp.GetData() ;
	const int len   = resp.GetDataSize() ;

	if ( ptr == NULL || len == 0 || err != E_HTTPCLIENT_SUCCESS ) {
		OUT_ERROR( NULL, 0, "Call" , "result emtpy, %s:%d" , __FILE__, __LINE__ ) ;
		// 处理响应数据
		_response->ProcessResp( seq , ptr , len, err - E_HTTPCLIENT_SUCCESS ) ;
		return ;
	}

	//处理结果
	_response->ProcessResp( seq , ptr , len , HTTP_CALL_SUCCESS );
}
