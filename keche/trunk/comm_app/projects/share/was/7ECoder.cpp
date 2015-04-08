/*
 * C7ECoder.cpp
 *
 *  Created on: 2011-7-29
 *      Author: zhangdeke
 */
#include "7ECoder.h"
#include <string.h>
#include <iostream>
using namespace std;

C7eCoder::C7eCoder()
{
	_data = NULL ;
	_len  = 0 ;
	_temp = NULL ;
}

C7eCoder::~C7eCoder()
{
	if ( _temp != NULL ) {
		delete [] _temp ;
		_temp  = NULL ;
		_len   = 0 ;
	}
}

// 转换编码
bool C7eCoder::Encode( const char *data, const int len )
{
	if( data[0] != 0x7e || data[len-1] != 0x7e )
	{
		return false ;
	}

	_len  = len * 2 ;
	if ( _len > MAX_BUFF )
	{
		if ( _temp != NULL )
			delete [] _temp ;

		_temp = new char[ _len + 1 ] ;
		memset( _temp, 0 , _len + 1 ) ;

		_data = _temp ;
	}
	else
	{
		memset( _buf, 0 , MAX_BUFF ) ;
		_data = _buf ;
	}
	_data[0] = data[0] ;

	int offset = 1 ;
	for( int i = 1; i < len - 1; i++ )
	{
		if ( data[i] == 0x7e )
		{
			_data[offset++] = 0x7d ;
			_data[offset++] = 0x02 ;
		} else if ( data[i] == 0x7d )
		{
			_data[offset++] = 0x7d ;
			_data[offset++] = 0x01 ;
		} else
		{
			_data[offset++] = data[i] ;
		}
	}
	_data[offset] = data[len-1] ;

	_len = offset + 1 ;

	return true ;
}

// 解码
bool C7eCoder::Decode( const char *data, const int len )
{
	if( data[0] != 0x7e || data[len-1] != 0x7e )
	{
		return false ;
	}

	_len  = len ;

	if ( _len > MAX_BUFF )
	{
		if ( _temp != NULL )
			delete [] _temp ;

		_temp = new char[ _len + 1 ] ;
		memset( _temp, 0 , _len + 1 ) ;

		_data = _temp ;
	}
	else
	{
		memset( _buf, 0 , MAX_BUFF ) ;
		_data = _buf ;
	}
	_data[0] = data[0] ;

	int offset = 1 ;
	for(int i = 1; i < len - 1; i++)
	{
		if (data[i] == 0x7d && data[i+1] == 0x02)
		{
			_data[offset++] = 0x7e;
			i++;
		}
		else if (data[i] == 0x7d && data[i+1] == 0x01)
		{
			_data[offset++] = 0x7d;
			i++;
		}
		else
		{
			_data[offset++] = data[i];
		}
	}
	_data[offset] = data[len-1] ;

	_len = offset + 1 ;

	return true ;
}

// 取得解码的长度
const int C7eCoder::GetSize()
{
	return _len ;
}

// 取得数据
const char * C7eCoder::GetData()
{
	return _data ;
}
