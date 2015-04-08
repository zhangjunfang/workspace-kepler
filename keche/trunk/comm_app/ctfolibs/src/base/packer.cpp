/*
 * packer.cpp
 *
 *  Created on: 2012-8-31
 *      Author: humingqing
 *
 *  实现一个数据拆解包管理对象，一部分为只读数据属性，一部分为内部数据属性
 *
 */

#include <stdio.h>
#include "packer.h"

CPacker::CPacker( const char *p, int len )
{
	_ronly = 1 ; // 这里通过外部赋值来处理数据
	_pbuf  = _pdata = (unsigned char*)p ;
	_pend  = _pfree = (unsigned char*)(p+len);
}

// 读字符串形的数据
unsigned int CPacker::readString( CQString &buf )
{
	int len = readInt();
	// 如果读取的数据长度大于最大长度就直接返回了
	if ( len <= 0 || len > getLength() )
		return 0;

	// 开辟内存空间
	buf.AppendBuffer( NULL, len );
	// 直接将内存数据拷贝到对象缓存中
	return readBytes( ( char* ) buf.GetBuffer(), len );
}

// 读数据块
unsigned int CPacker::readBytes( void *buf, int len )
{
	memset( buf, 0, len );

	if ( ! readBlock( buf, len ) )
		return 0;
	return len;
}

// 写入字符串形的数据
void CPacker::writeString( CQString &s )
{
	writeInt32( s.GetLength() );
	if ( s.GetLength() > 0 ) {
		writeBlock( s.GetBuffer(), s.GetLength() );
	}
}

// 写入固定长度数据
void CPacker::writeFix( const char *sz, int len , int max )
{
	if ( len > max ) {
		writeBlock( sz, max ) ;
	} else {
		writeBlock( sz, len ) ;
		writeFill( 0, max-len ) ;
	}
}
