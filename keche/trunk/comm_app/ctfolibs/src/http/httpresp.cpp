/*
 * httpresp.cpp
 *
 *  Created on: 2012-2-29
 *      Author: humingqing
 */

#include "httpresp.h"

#include "httpresp.h"
#include <qstring.h>

typedef struct
{
	int err ;
	const char* info ;

}ERR_INFO ;

static const ERR_INFO ERRINFO_MAP[] = {
	{ 200 , "OK" } ,
	{ 404 , "NOT FOUND" } ,
	{ 500 , "INTERNAL ERROR" } ,
};

static const char* GENERAL_ERROR = "UNKOWN ERROR" ;

static const char* FindErrorInfo( int err )
{
	int count = sizeof( ERRINFO_MAP ) / sizeof( ERR_INFO ) ;
	int i;
	for ( i=0;i<count;i++ )
	{
		if ( ERRINFO_MAP[i].err == err )
		{
			return ERRINFO_MAP[i].info ;
		}
	}

	return GENERAL_ERROR ;
}

CServerHttpResp::CServerHttpResp()
{
	_iError = -1;
}

CServerHttpResp::~CServerHttpResp()
{
}

void CServerHttpResp::AddHeader( const char* header , const char* value )
{
	CQString temp;
	temp = header ;
	temp.ToLower() ;

	_HeaderMap.insert( std::pair<CQString,CQString>( temp , value ) ) ;
}

void CServerHttpResp::RemoveHeader( const char* header )
{
	CQString temp;
	temp = header ;
	temp.ToLower() ;

	CHeaderMap::iterator iter ;
	iter = _HeaderMap.find( temp ) ;
	if ( iter != _HeaderMap.end() )
		_HeaderMap.erase( iter ) ;
}

void CServerHttpResp::SetRespData( const char* resp_data , const int resp_size )
{
	// 移除content-length头
	RemoveHeader( "content-length" ) ;

	if ( resp_data == NULL || resp_size == 0 )
	{
		return ;
	}

	_sReqData.SetString( resp_data, resp_size ) ;

	char value_buf[100] ;
	sprintf( value_buf , "%d" , resp_size ) ;
	AddHeader( "content-length" , value_buf ) ;
}


const char* CServerHttpResp::GetRespData( int& size )
{
	// 再根据错误码、头信息、数据组装出响应数据
	_sRespData.SetString( NULL ) ;

	// 加版本
	CQString first_line ;
	first_line = "HTTP/1.0 " ;

	// 加错误码
	char value_buf[20] ;
	sprintf( value_buf , "%d " , _iError ) ;
	first_line += value_buf ;


	// 加错误信息
	first_line += FindErrorInfo( _iError ) ;

	// 组装头
	CQString headers ;

	CHeaderMap::iterator iter;

	for( iter= _HeaderMap.begin(); iter != _HeaderMap.end(); ++iter ){
		headers += iter->first ;
		headers += ": " ;
		headers += iter->second;
		headers += "\r\n" ;
	}

	_sRespData.AppendBuffer( first_line.GetBuffer() ) ;
	_sRespData.AppendBuffer( "\r\n" ) ;

	_sRespData.AppendBuffer( headers.GetBuffer() ) ;
	_sRespData.AppendBuffer( "\r\n" ) ;

	if ( ! _sReqData.IsEmpty() ){
		_sRespData.AppendBuffer( _sReqData.GetBuffer(), _sReqData.GetLength() ) ;
	}

	size = _sRespData.GetLength() ;

	return _sRespData.GetBuffer() ;
}

