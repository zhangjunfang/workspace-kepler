
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

// 转换手机号不足位补零
static void safe_phone( const char *src, int len, char *dest, int dlen )
{
	safe_memncpy( dest, src, len ) ;
	for ( int i = len; i < dlen; ++ i ) {
		dest[i] = '0' ;
	}
}

// 检测位是否有值
static bool isset( unsigned int nval, unsigned int pos )
{
	return ( nval & ( 1 << pos ) ) ;
}

// 设置位数据
static void setbit( unsigned int &nval, unsigned int pos )
{
	nval = nval | ( 1 << pos ) ;
}

// 将字符串转成BCD码
static void str2bcd( const char *src, char *dest , int len )
{
	for( int i=0; i< len; ++i )
	{
		dest[i]  = ( ( (src[2*i] - 48 ) << 4 ) & 0xf0 ) ;
		dest[i] |= ( ( src[2*i+1]- 48 ) & 0x0f ) ;
	}
}

// 将BCD码转成字符串
static void bcd2str( const char *src, int len, char *dest )
{
	for( int i = 0; i < len; ++ i )
	{
		dest[2*i]   = (( src[i] & 0xf0 ) >> 4 ) + 48 ;
		dest[2*i+1] = ( src[i] & 0x0f ) + 48 ;
	}
}

// 将度的格式转换成对应需要格式
static unsigned int degree2int( unsigned int n )
{
	/*
	P1  119 表示为 0x77,
	P2  4.81854=0.080309 x 60 表示为 0x04
	P3  81.854=0.81854 x 100 表示为 0x51
	P4  85.4=0.854 x 100 表示为 0x55  */
	float s0 = (float)( n * 10 / 6 ) / (float) 1000000;

	char szbuf[4] = {0};
	int tmp = (int) s0 ;
	szbuf[0] = (unsigned char)tmp ;

	float s1 = (float) ( s0 - (float) tmp ) * 60 ;
	tmp = (int) s1 ;
	szbuf[1] = (unsigned char)tmp ;

	float s2 = (float) ( s1 - (float) tmp ) * 100 ;
	tmp = (int) s2 ;
	szbuf[2] = (unsigned char)tmp ;

	float s3 = (float) ( s2 -( float) tmp ) * 100 ;
	szbuf[3] = (int)s3 ;

	unsigned int c = 0 ;
	// 将转换后的数据拷贝到整数中
	memcpy( &c, szbuf, sizeof(int) ) ;

	return c ;
}

// 将对应的格式转换成度分格式
static unsigned int int2degree( unsigned int c )
{
	char szbuf[4] = {0} ;
	memcpy( szbuf, &c, sizeof(int) ) ;

	float s0 = (float) szbuf[3] / (float)100.0 ;
	s0 = ( s0 + (float) szbuf[2] ) / (float)100.0 ;
	s0 = ( s0 + (float) szbuf[1] ) / (float)60.0 ;
	s0 = ( s0 + (float) szbuf[0] ) ;

	return (unsigned int) ( s0 * 6000000.0 ) ;
}


#endif
