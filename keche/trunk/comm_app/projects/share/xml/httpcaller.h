/**
 * author: humingqing
 * date:   2011-09-08
 * memo:   处理终端鉴权,这里主要异步HTTP的调用处理，对于异步调用需要等待队列
 */
#ifndef __CARAUTHCALLER_H__
#define __CARAUTHCALLER_H__

#include <asynchttpclient.h>

#define TIMEOUT_LIVE   180

#define HTTP_CALL_SUCCESS  0

// 检测授权响应处理
class ICallResponse
{
public:
	virtual ~ICallResponse() {}
	// 处理HTTP的响应回调处理
	virtual void ProcessResp( unsigned int seqid, const char *data, const int len , const int err ) = 0 ;
};

class CHttpCaller : public IHttpCallbacker
{
public:
	CHttpCaller() ;
	~CHttpCaller() ;
	// 初始化
	bool Init( int sendthread , int recvthread , int queuesize ) ;
	// 启动
	bool Start( void ) ;
	// 停止
	void Stop( void );
	// 异步的HTTP的回调接口处理
	void ProcHTTPResponse( unsigned int seq_id , const int err , const CHttpResponse& resp ) ;
	// 处理XML请求服务
	bool Request( unsigned int seqid, const char *url, const char *data = NULL , const int len = 0 , const char *ctype = "application/xml" ) ;
	// 取得请求序号
	unsigned int GetSequeue( void ) ;
	//设置结果回调对象
	void SetReponse( ICallResponse *resp ) { _response = resp ;}

private:
	// 异步的HTTP的客户端
	CAsyncHttpClient  		_httpClient ;
	// HTTP发送线程
	unsigned int 			_send_thread;
	// HTTP接收线程
	unsigned int 		    _recv_thread;
	// HTTP最大请求队列
	int 					_queue_size ;
	//结果回调对象
    ICallResponse		   *_response ;
};

#endif
