/***
 * author: humingqing
 * date:   2011-09-07
 * memo:   处理HTTP请求
 */
#ifndef __HTTPCLIENT_H__
#define __HTTPCLIENT_H__

#include "httpparser.h"

// ERROR DEFINES
#define E_HTTPCLIENT_SUCCESS							2000
#define E_HTTPCLIENT_FAILED								E_HTTPCLIENT_SUCCESS + 1
#define E_HTTPCLIENT_REQUESTINVALID						E_HTTPCLIENT_SUCCESS + 2
#define E_HTTPCLIENT_SERVERINVALID						E_HTTPCLIENT_SUCCESS + 3
#define E_HTTPCLIENT_RESPONSEINVALID					E_HTTPCLIENT_SUCCESS + 4
#define E_HTTPCLIENT_RESPTIMEOUT						E_HTTPCLIENT_SUCCESS + 5
#define E_HTTPCLIENT_REQQUEUELENGTH						E_HTTPCLIENT_SUCCESS + 6

/**
 * HTTP请求对象
 */
class CHttpRequest
{
public:

	typedef enum{ HTTP_METHOD_GET = 0 , HTTP_METHOD_POST } HTTP_METHOD ;

	CHttpRequest();
	~CHttpRequest();

private:

	// URL
	string 		    _strURL ;

	// HTTP请求的方法，目前支持：GET ， POST
	HTTP_METHOD     _iMethod ;

	// USER , PASS
	string 		    _strUser ;
	string 		    _strPass ;

	// 代理服务器地址和端口
	string 			_strProxyServer ;
	unsigned short  _iProxyPort ;

	// 用户可以自己添加自动生成外的头信息。如果和自动生成的有重复，则覆盖自动生成的头
	typedef struct
	{
		string strHeader ;
		string strValue ;

	}HTTP_HEADER ;

	list<HTTP_HEADER*> _HeaderList ;

	// POST 数据
	char* 	_pData ;
	int 	_iDataSize ;

	// 生成的HTTP请求
	char* 	_pHttpReq ;
	int 	_iHttpReqlen ;


protected:

	void CleanHeaderList( void ) ;

	void CleanHttpReq()  ;

	// 从URL中分析出地址,参数,端口
	bool GetAddrAndParams ( const char* url  , string& addr , unsigned short& port , string& params ) ;

	/**
	 * 往hdr_list里添加头
	 * @param hdr_list list<HTTP_HEADER*>& 头列表
	 * @param header const char* 头
	 * @param value const char* 值
	 */
	void AddHeader( list<HTTP_HEADER*>& hdr_list , const char* header , const char* value ) ;

	/**
	 * 从hdr_list中删除header
	 * @param hdr_list list<HTTP_HEADER*>&
	 * @param header const char* 头
	 */
	void RemoveHeader( list<HTTP_HEADER*>& hdr_list , const char* header ) ;

	// 获取方法的字符串格式
	const char* GetMethodString( void ) ;

	// 查询到一个头的值
	const char* GetHeader( const char* header ) ;

	void my_strlwr( char* str )  ;

	// 检查URL参数中有没有空格，如果有，就转变为%20
	void CheckParam( const char* param , string& ret_params ) ;

public:

	void SetURL( const char* url ){ _strURL = url ;} ;
	const char* GetURL( void ){ return _strURL.c_str() ; };

	void SetProxy( const char* proxy_server , unsigned short proxy_port )
	{
		_strProxyServer = proxy_server ;
		_iProxyPort = proxy_port ;
	};

	void SetMethod( CHttpRequest::HTTP_METHOD method ){ _iMethod = method ; };
	CHttpRequest::HTTP_METHOD GetMethod(){ return _iMethod ; };

	void SetCookie( const char* cookie );
	const char* GetCookie( void );

	void SetAuthPass( const char* user , const char* pass , bool bProxy ) ;
	const char* GetUser( void ){ return _strUser.c_str() ; } ;
	const char* GetPass( void ){ return _strPass.c_str() ; } ;

	void SetUA( const char* ua );
	const char* GetUA( void );

	void SetContentType( const char* content_type );
	const char* GetContentType( void ) ;

	bool SetPostData( char* data , int size ) ;

	bool AddHeader( const char* header , const char* value ) ;

	// 创建发送HTTP请求
	const char* GetHTTPRequest( string& server , unsigned short& port , int& len ) ;

	CHttpRequest& operator= ( const CHttpRequest& req ) ;
};


class CHttpResponse : public CHttpParser
{
public:
	// 获得COOKIE
	const char* GetCookie( void ) const ;

	// 获得数据的尺寸
	int GetDataSize( void ) const ;

	// 获取数据
	const char* GetData( void ) const ;

	// 获取CONTENT-TYPE
	const char* GetContentType( void ) const ;

	// 获取HTTP响应的代码
	const char* GetRespCode( void ) const ;

	// 获取HTTP响应的消息
	const char* GetRespText( void ) const ;

	// 得到HTTP的版本
	const char* GetHttpVer( void ) const ;


};

// 同步处理HTTP请求
class CHttpClient
{
public:
	CHttpClient() ;
	~CHttpClient() ;
public:
	// 执行一个HTTP请求
	int HttpRequest( CHttpRequest& request , CHttpResponse& resp ) ;

private:
	// 连接服务器
	int Open( const char *ip, unsigned short port ) ;
	// 关闭连接
	void Close( int fd ) ;
};

#endif
