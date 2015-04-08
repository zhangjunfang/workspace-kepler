/**********************************************
 * pccutil.h
 *
 *  Created on: 2011-08-04
 *      Author: humingqing
 *       Email: qshuihu@gmail.com
 *    Comments:
 *********************************************/
#ifndef __MUTIL_H__
#define __MUTIL_H__

#include <std.h>
#include <tools.h>

/**
// 分割数据到MAP中
static bool splitmap( const string &str, map<string,string> &mp )
{
	if ( str.empty() ) {
		return false ;
	}

	size_t begin = str.find('{') ;
	size_t end   = str.find( '}' , begin ) ;
	if ( begin == string::npos || end == string::npos ) {
		return false ;
	}

	string sresult = str.substr( begin+1, end - begin -1 ) ;


	vector<string> vec ;
	if ( ! splitvector( sresult, vec, "," , 0 ) ) {
		return false ;
	}

	for ( size_t i = 0; i < vec.size(); ++ i ) {
		string &temp = vec[i] ;
		size_t  pos  = temp.find(':') ;
		if ( pos == string::npos ) {
			continue ;
		}
		mp.insert( pair<string,string>( temp.substr(0,pos) , temp.substr(pos+1) ) ) ;
	}

	return ( ! mp.empty() ) ;
}
*/
// 转换的字符串类型
static const string safe2string( const char *src, int len )
{
	char buf[1024] = {0} ;
	safe_memncpy( buf, src, len ) ;

	string s = buf ;
	return s ;
}

// 安全拷贝数字
static void safenumber( char *dest, const char *src, int len )
{
	safe_memncpy( dest, src, len ) ;
	// 处理非数字字符
	for ( int i = len-1 ; i >= 0; -- i ) {
		if ( dest[i] >='0' && dest[i] <='9' ) {
			break ;
		}
		dest[i] = 0 ;
	}
}

// 从后拷贝，不足填充字符
static void reverse_copy( char *buf , int len, const char *src , const char fix )
{
	int nlen   = (int) strlen( src ) ;
	int offset = len - nlen ;
	if ( offset < 0 ) {
		offset = 0 ;
	}
	if ( offset > 0 ) {
		for ( int i = 0; i < offset; ++ i ) {
			buf[i] = fix ;
		}
	}
	memcpy( buf + offset, src, nlen ) ;
}


#endif
