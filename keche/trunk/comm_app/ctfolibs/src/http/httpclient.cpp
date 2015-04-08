#include "httpclient.h"
#include <stdlib.h>
#include <Base64.h>
#include "qstring.h"
#include "UtilitySocket.h"
#include <errno.h>
#include <comlog.h>
#ifndef _WIN32
#include <strings.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netdb.h>
#define stricmp  strcasecmp
#endif

CHttpRequest::CHttpRequest()
{
	_pHttpReq 	 = NULL ;
	_iHttpReqlen = 0 ;
	_pData 		 = NULL ;
	_iDataSize   = 0 ;

	// 先增加一些缺省的头
	SetUA( "my_httpclient" ) ;
	AddHeader( _HeaderList , "Accept" , "*/*" ) ;
}

CHttpRequest::~CHttpRequest()
{
	CleanHeaderList();
	CleanHttpReq() ;

	if ( _pData != NULL )
		delete _pData ;
}

void CHttpRequest::my_strlwr( char* str )
{
	int i;
	int count = strlen( str ) ;
	for ( i=0;i<count;i++ )
	{
		if ( str[i] <= 'Z' && str[i] >= 'A' )
		{
			str[i] += 32 ;
		}
	}
}

bool CHttpRequest::GetAddrAndParams ( const char* url  , string& addr , unsigned short& port , string& params )
{
	if ( !url )
		return false ;

	if ( strlen( url ) == 0 )
		return false ;

	CQString stemp ;
	stemp.SetString( url ) ;
	stemp.ToLower() ;

	const char *temp_url = stemp.GetBuffer() ;
	char* p = (char*)strstr( temp_url , "http://" ) ;
	if ( !p )
	{
		return false ;
	}

	p+= strlen("http://");
	char* server = p;

	p = strchr( server , '/' );

	if ( p != NULL )
		*p = 0;

	CQString  sp ;
	char* q = strchr( server , ':' );
	if ( !q )
	{
		sp.SetString( url + (server - temp_url) , strlen(server) ) ;
		addr = sp.GetBuffer() ;
		port = 80;
	}
	else
	{
		*q = 0;
		sp.SetString( url + (server - temp_url) , strlen(server) );
		addr = sp.GetBuffer() ;
		q++;
		port = atoi(q);
	}

	if ( p == NULL )
	{
		params = "/" ;
	}
	else
	{
		sp.SetString( url + (p - temp_url) , strlen(url) + 1 - ( p - temp_url ) );
		params = sp.GetBuffer() ;
	}

	return true ;

}


void CHttpRequest::RemoveHeader( list<HTTP_HEADER*>& hdr_list , const char* header )
{
	if ( header == NULL )
		return ;

	list<HTTP_HEADER*>::iterator iter ;
	for ( iter=hdr_list.begin();iter!=hdr_list.end();++iter )
	{
		HTTP_HEADER* pHeader = (*iter) ;

		if ( stricmp( pHeader->strHeader.c_str() , header ) == 0 )
		{
			// 替换这个头的value
			delete pHeader ;
			hdr_list.erase( iter ) ;
			break ;
		}

	}
}

void CHttpRequest::AddHeader( list<HTTP_HEADER*>& hdr_list , const char* header , const char* value )
{
	if ( header == NULL || value == NULL )
		return ;

	if ( strlen(header) == 0 || strlen(value) == 0 )
		return;

	list<HTTP_HEADER*>::iterator iter ;
	for ( iter=hdr_list.begin();iter!=hdr_list.end();++iter )
	{
		HTTP_HEADER* pHeader = (*iter) ;
#ifdef _WIN32
		if ( stricmp( pHeader->strHeader.c_str() , header ) == 0 )
#else
		if ( strcasecmp( pHeader->strHeader.c_str() , header ) == 0 )
#endif
		{
			// 替换这个头的value
			pHeader->strValue = value ;
			return ;
		}

	}

	HTTP_HEADER* pHeader = new HTTP_HEADER ;
	pHeader->strHeader = header ;
	pHeader->strValue = value;

	hdr_list.push_back( pHeader ) ;

}

void CHttpRequest::CleanHeaderList( void )
{
	list<HTTP_HEADER*>::iterator iter ;
	for ( iter=_HeaderList.begin();iter!=_HeaderList.end();++iter )
	{
		HTTP_HEADER* pHeader = (*iter) ;
		delete pHeader ;
	}

	_HeaderList.erase( _HeaderList.begin() , _HeaderList.end() ) ;

}

void CHttpRequest::CleanHttpReq()
{
	if ( _pHttpReq != NULL )
	{
		delete _pHttpReq ;
		_iHttpReqlen = 0 ;
		_pHttpReq = NULL ;
	}
}

const char* CHttpRequest::GetMethodString( void )
{
	switch( GetMethod() )
	{
	case HTTP_METHOD_POST:
		return "POST" ;
	case HTTP_METHOD_GET:
	default:
		return "GET" ;
	}

	return NULL ;
}

void CHttpRequest::SetCookie( const char* cookie )
{
	if ( cookie == NULL )
		RemoveHeader( _HeaderList , "Cookie" ) ;
	else
		AddHeader( _HeaderList , "Cookie" , cookie ) ;
}

const char* CHttpRequest::GetCookie( void )
{
	return GetHeader( "Cookie" ) ;
}


void CHttpRequest::SetAuthPass( const char* user , const char* pass , bool bProxy )
{
	_strUser = user;
	_strPass = pass ;

	if ( user == NULL || pass == NULL )
	{
		RemoveHeader( _HeaderList , "Authorization" ) ;
	}
	else
	{
		string user_pwd = user ;
		user_pwd += ":" ;
		user_pwd += pass ;

		// base64 encode
		CBase64  coder ;
		coder.Encode( (char*)user_pwd.c_str() , strlen( user_pwd.c_str() ) ) ;

		int len = coder.GetLength() ;
		char* str = new char[ len+1 ] ;
		memcpy( str , coder.GetBuffer() , len );
		str[len] = 0;
		string temp = "Basic " ;
		temp += str ;
		delete[] str ;

		if ( bProxy )
			AddHeader( _HeaderList , "Proxy-Authorization" , temp.c_str() ) ;
		else
			AddHeader( _HeaderList , "Authorization" , temp.c_str() ) ;

	}
}

void CHttpRequest::SetUA( const char* ua )
{
	if ( ua == NULL )
		RemoveHeader( _HeaderList , "USER-AGENT" ) ;
	else
		AddHeader( _HeaderList , "USER-AGENT" , ua ) ;
}

const char* CHttpRequest::GetUA( void )
{
	return GetHeader( "USER-AGENT" ) ;
}

void CHttpRequest::SetContentType( const char* content_type )
{
	if ( content_type == NULL )
		RemoveHeader( _HeaderList , "CONTENT-TYPE" ) ;
	else
		AddHeader( _HeaderList , "CONTENT-TYPE" , content_type ) ;
}

const char* CHttpRequest::GetContentType( void )
{
	return GetHeader( "CONTENT-TYPE" ) ;
}


bool CHttpRequest::SetPostData( char* data , int size )
{
	if ( _pData != NULL )
	{
		delete[] _pData ;
		_pData = NULL ;
		_iDataSize = 0;
	}

	if ( data == NULL || size == 0 )
	{
		RemoveHeader( _HeaderList , "Content-Length" ) ;
		return true ;
	}

	_pData = new char[ size ] ;
	if ( _pData == NULL )
		return false ;

	memcpy( _pData , data , size ) ;
	_iDataSize = size ;

	char buf[100] ;
	sprintf( buf,"%d",size );
	AddHeader( _HeaderList , "Content-Length" , buf ) ;

	return true ;

}

bool CHttpRequest::AddHeader( const char* header , const char* value )
{
	if ( header == NULL || value == NULL )
		return false ;

	if ( strlen(header) == 0 || strlen(value) == 0 )
		return false ;

	AddHeader( _HeaderList , header , value ) ;

	return true ;
}

const char* CHttpRequest::GetHeader( const char* header )
{
	if ( header == NULL )
		return NULL ;

	list<HTTP_HEADER*>::iterator iter ;
	for ( iter=_HeaderList.begin();iter!=_HeaderList.end();++iter )
	{
		HTTP_HEADER* pHeader = (*iter) ;
#ifdef _WIN32
		if ( stricmp( pHeader->strHeader.c_str() , header ) == 0 )
#else
		if ( strcasecmp( pHeader->strHeader.c_str() , header ) == 0 )
#endif
		{
			return pHeader->strValue.c_str() ;
		}
	}

	return NULL ;
}

const char* CHttpRequest::GetHTTPRequest( string& server , unsigned short& port , int& len )
{
	len = 0 ;

	CleanHttpReq() ;

	if ( strlen( _strURL.c_str() ) == 0 )
		return NULL ;

	// 然后根据_HeaderList构造HTTP的头
	string http_hdr ;

	const char* method_str = GetMethodString() ;

	// 先构造第一行

	if ( strlen(_strProxyServer.c_str()) != 0 )
	{
		server = _strProxyServer.c_str() ;
		port   = _iProxyPort ;

		char* buf = new char[ strlen( _strURL.c_str() ) + 100 ];
		sprintf( buf , "%s %s HTTP/1.1" , method_str , _strURL.c_str() ) ;
		http_hdr = buf ;
		delete[] buf ;

	}
	else
	{
		string params;
		if ( !GetAddrAndParams( _strURL.c_str() , server , port , params ) )
			return NULL ;


		string chk_params ;
		CheckParam( params.c_str() , chk_params ) ;
		params = chk_params ;
		char* buf = new char[ strlen( params.c_str() ) + 100 ];
		sprintf( buf , "%s %s HTTP/1.1" , method_str , params.c_str() ) ;
		http_hdr = buf ;
		delete[] buf ;

		string host;
		host = server ;
		if ( port != 80 )
		{
			host += ":" ;
			char port_buf[10] ;
			sprintf( port_buf , "%d" , port ) ;
			host += port_buf ;
		}
		AddHeader( _HeaderList , "Host" , host.c_str() ) ;
	}

	http_hdr += "\r\n" ;


	list<HTTP_HEADER*>::iterator iter ;
	for( iter=_HeaderList.begin();iter!=_HeaderList.end();++iter )
	{
		HTTP_HEADER* pHeader = (*iter) ;

		if ( strlen( pHeader->strHeader.c_str() ) != 0 &&
			 strlen( pHeader->strValue.c_str() ) != 0 )
		{
			http_hdr += pHeader->strHeader;
			http_hdr += ": " ;
			http_hdr += pHeader->strValue ;
			http_hdr += "\r\n" ;
		}
	}

	http_hdr += "\r\n" ;

	// 然后创建BODY,如果有的话
	len = strlen( http_hdr.c_str() ) + _iDataSize ;

	_pHttpReq = new char[ len ] ;

	memcpy( _pHttpReq , http_hdr.c_str() , strlen( http_hdr.c_str() ) ) ;

	if ( _pHttpReq != NULL )
	{
		_iHttpReqlen = len ;

		if ( _pData != NULL )
		{
			memcpy( _pHttpReq+strlen( http_hdr.c_str() ) , _pData , _iDataSize ) ;
		}
	}

	// 返回
	len = _iHttpReqlen ;

	return _pHttpReq ;
}

CHttpRequest& CHttpRequest::operator= ( const CHttpRequest& req )
{
	CleanHeaderList();
	CleanHttpReq() ;

	if ( _pData != NULL )
	{
		delete _pData ;
		_pData = NULL ;
		_iDataSize = 0 ;
	}

	_iDataSize = req._iDataSize ;
	if ( _iDataSize != 0 )
	{
		_pData = new char[ _iDataSize ];
		memcpy( _pData , req._pData , _iDataSize ) ;
	}

	_iMethod = req._iMethod ;

	_iHttpReqlen = req._iHttpReqlen;
	_pHttpReq = new char[ _iHttpReqlen ];
	memcpy( _pHttpReq , req._pHttpReq , _iHttpReqlen ) ;

	_strProxyServer = req._strProxyServer ;
	_strURL = req._strURL ;
	_strUser = req._strURL ;
	_strPass = req._strPass ;
	_iProxyPort = req._iProxyPort ;

	list<HTTP_HEADER*>::const_iterator iter ;

	for ( iter=req._HeaderList.begin();iter!=req._HeaderList.end();++iter )
	{
		HTTP_HEADER* p = (*iter) ;
		AddHeader( _HeaderList , p->strHeader.c_str() , p->strValue.c_str() ) ;
	}

	return *this ;
}

void CHttpRequest::CheckParam( const char* param , string& ret_params )
{
	char* chk_param = (char*)malloc( strlen(param)*3 + 1 ) ;
	memset( chk_param , 0 , strlen(param)*3+1 );
	int i;
	char* p = chk_param ;
	for ( i=0;i<(int)strlen(param);i++ )
	{
		if ( param[i] == ' ' )
		{
			strcpy( p , "%%20" ) ;
			p += strlen( p );
		}
		else
		{
			*p++ = param[i];
		}

	}

	ret_params = chk_param ;

	free( chk_param ) ;

}

const char* CHttpResponse::GetCookie( void ) const
{
	return GetHeader( "Set-Cookie" ) ;
}

int CHttpResponse::GetDataSize( void ) const
{
	int sz ;
	GetBody( sz ) ;
	return sz ;
}

const char* CHttpResponse::GetHttpVer( void ) const
{
	return GetHeader( EXT_HEADER_VERSION ) ;
}

const char* CHttpResponse::GetData( void ) const
{
	int sz ;
	return GetBody( sz ) ;
}

const char* CHttpResponse::GetContentType( void ) const
{
	return GetHeader( "Content-Type" ) ;
}

const char* CHttpResponse::GetRespCode( void ) const
{
	return GetHeader( EXT_HEADER_CODE ) ;
}

const char* CHttpResponse::GetRespText( void ) const
{
	return GetHeader( EXT_HEADER_STATUSTEXT ) ;
}

/////////////////////////////////////////////////////////////////////////
CHttpClient::CHttpClient()
{
}

CHttpClient::~CHttpClient()
{
}

int CHttpClient::Open( const char *ip, unsigned short port )
{
	int sockfd = -1;
	if((sockfd=UtilitySocket::BaseSocket::socket(SOCK_STREAM))<0)
	{
		OUT_ERROR( ip, port, "CONN", "socket error:%s", strerror(errno) );
		return(-1);
	}

	if ( ! UtilitySocket::BaseSocket::connect( sockfd, ip , port ) )
	{
		OUT_ERROR( ip, port, "CONN", "connect:%s", strerror(errno) );
		UtilitySocket::BaseSocket::close( sockfd ) ;
		return -1 ;
	}

	return sockfd;
}

// 关闭连接
void CHttpClient::Close( int fd )
{
	// 关闭连接处理
	UtilitySocket::BaseSocket::close( fd ) ;
}

int CHttpClient::HttpRequest( CHttpRequest& request , CHttpResponse& resp )
{
	string server ;
	unsigned short port ;

	int  len = 0 ;
	const char *data = request.GetHTTPRequest( server , port , len ) ;
	if ( data == NULL || len == 0 )
		return E_HTTPCLIENT_REQUESTINVALID ;

	if ( strlen( server.c_str() ) == 0 )
		return E_HTTPCLIENT_SERVERINVALID ;

	// 打开服务器连接
	int sockfd = Open( server.c_str(), port ) ;
	if ( sockfd == -1 ) {
		OUT_ERROR( NULL, 0 , NULL, "Connection server %s port %d failed" , server.c_str(), port ) ;
		return E_HTTPCLIENT_FAILED ;
	}

	int ret , offset = 0 , left_len = len ;
	// 发送HTTP请求
	while( left_len > 0 ) {
		ret = write( sockfd, data+offset, left_len ) ;
		if ( ret < 0 ) {
			break ;
		}
		left_len = left_len - ret ;
		offset   = offset + ret ;
	}

	CQString  rbuffer ;
	if ( left_len == 0 ) {

		char buffer[2048] = {0} ;
		int error = 0 ;
		// 接收数据处理
		while( ( ret = read( sockfd, buffer, 2048 ) ) > 0 ) {
			// 如果接收着数据则放入缓存中
			rbuffer.AppendBuffer( buffer, ret ) ;

			error = CHttpParser::DetectCompleteReq( rbuffer.GetBuffer() , rbuffer.GetLength() ) ;
			if ( error == HTTPPARSER_ERR_SUCCESS ) {
				break ;
			}
		}
	}
	// 关闭连接
	Close( sockfd ) ;

	// 分析HTTP服务器返回的数据
	ret = resp.Parse( rbuffer.GetBuffer() , rbuffer.GetLength() ) ;
	if ( ret != HTTPPARSER_ERR_SUCCESS )
	{
		// 说明HTTP响应的数据有错误了
		return E_HTTPCLIENT_RESPONSEINVALID;
	}

	return E_HTTPCLIENT_SUCCESS ;

}
