/*
 * rc4coder.cpp
 *
 *  Created on: 2012-4-26
 *      Author: humingqing
 *
 *  RC4 加密算法
 */
#include "rc4coder.h"
#include <string.h>
// 默认加密的码表
CRC4Coder::CRC4Coder(const char *key)
{
	int len = (int)strlen(key) ;
	for( int i = 0; i < len; ++ i )
	{
		_sk[i] = (unsigned char)key[i] ;
	}
	_klen  = len ;
	_i= _j = 0 ;
}

CRC4Coder::~CRC4Coder( void )
{
}

// 加密和解密处理
unsigned char* CRC4Coder::r4code( unsigned char *val, unsigned int len )
{
	rc4_init( _sk , _klen ) ;
	for( unsigned int i = 0; i < len; ++ i )
	{
		val[i] = val[i]^rc4_key() ;
	}
	return val ;
}

// 初始化表值
void CRC4Coder::rc4_init( unsigned char *k , unsigned int n )
{
	for( _i = 0; _i < 256; _i ++ )
		_sv[_i] = _i ;
	for( _i = _j = 0; _i < 256; _i ++ ){
		_j = ( _j + k[_i%n] + _sv[_i] ) & 255 ;
		rc4_swap( _sv, _i, _j ) ;
	}
	_i = _j = 0 ;
}

// 产生异或值对象
unsigned char CRC4Coder::rc4_key()
{
	_i = ( _i + 1 ) & 255 ;
	_j = ( _j + _sv[_i] ) & 255 ;
	rc4_swap( _sv, _i , _j ) ;
	return _sv[(_sv[_i] + _sv[_j])&255] ;
}

// 数据交换
void CRC4Coder::rc4_swap( unsigned char *s, unsigned int i , unsigned int j )
{
	unsigned char temp = s[i] ;
	s[i] = s[j] ;
	s[j] = temp ;
}




