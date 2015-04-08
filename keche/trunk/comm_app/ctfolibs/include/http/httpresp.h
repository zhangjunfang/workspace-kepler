/*
 * httpresp.h
 *
 *  Created on: 2012-2-29
 *      Author: humingqing
 *
 *  HTTP服务器响应对象
 */

#ifndef __HTTPRESP_H__
#define __HTTPRESP_H__

#include <map>
#include <string>
#include <qstring.h>

/**
 *  HTTP 响应对象
 */
class IHttpResponse
{
public:

	virtual ~IHttpResponse(){} ;

	/**
	 *  设置HTTP响应码 例如：200 ，500 ，404 等
	 */
	virtual void SetRespCode( int code ) = 0 ;

	/**
	 * 设置响应的头信息
	 */
	virtual void AddHeader( const char* header , const char* value ) = 0 ;

	/**
	 * 设置返回的数据
	 */
	virtual void SetRespData( const char* resp_data , const int resp_size ) = 0 ;

	/**
	 * 得到组装好的HTTP响应数据
	 */
	virtual const char* GetRespData( int& size ) = 0 ;
};

class CServerHttpResp : public IHttpResponse
{
	typedef std::map<CQString,CQString> CHeaderMap ;
public:

	CServerHttpResp() ;
	~CServerHttpResp() ;


private:
	// 处理HTTP错误
	int	_iError ;
	// 处理HTTP头部数据
	CHeaderMap _HeaderMap ;
	// 请求数据
	CQString   _sReqData  ;
	// 响应数据
	CQString   _sRespData ;

private:
	void RemoveHeader( const char* header ) ;

public:
	// 设置HTTP返回码
	void SetRespCode( int code ){ _iError = code ; } ;
	// 添加HTTP返回头部信息
	void AddHeader( const char* header , const char* value );
    // 设置响应数据
	void SetRespData( const char* resp_data , const int resp_size ) ;
	// 取得HTTP响应数据
	const char* GetRespData( int& size ) ;
};



#endif /* HTTPRESP_H_ */
