/*
 * packer.h
 *
 *  Created on: 2012-8-31
 *      Author: humingqing
 *
 *  數據打包與解包封狀，實現數據的序列化與反序列
 *
 */

#ifndef __PACKER_H__
#define __PACKER_H__

#include <qstring.h>
#include <databuffer.h>

// 数据解析对象
class CPacker: public DataBuffer
{
public:
	CPacker( const char *p, int len ) ;
	CPacker() {};
	~CPacker(){};
	// 读取Byte的数据
	unsigned char  readByte( void ) { return readInt8();}
	// 读聚Short的数据
	unsigned short readShort( void ){ return readInt16();};
	// 读取整形数据
	unsigned int   readInt( void ) { return readInt32(); } ;
	// 读字符串形的数据
	unsigned int   readString( CQString &s ) ;
	// 读数据块
	unsigned int   readBytes( void *buf, int len ) ;
	// 读取时间
	uint64_t 	   readTime( void ) { return readInt64(); }
	// 定位读写位置
	void 		   seekRead( int offset ) { seekPos(offset); }
	// 取得读数据长度
	int 		   GetReadLen( void ) { return getLength(); }
	// 写入一个字符
	void 		   writeByte( unsigned char c ) { writeInt8(c); }
	// 写入短整形的数据
	void 		   writeShort( unsigned short n ) { writeInt16(n); }
	// 写入整形的数据
	void 		   writeInt( unsigned int n ) { writeInt32(n); }
	// 写入时间的数据
	void		   writeTime( uint64_t n ) { writeInt64(n); }
	// 写入字符串形的数据
	void 		   writeString( CQString &s ) ;
	// 写入数据块
	void 		   writeBytes( void *buf, int len ) { writeBlock(buf,len); }
	// 写入固定长度数据
	void 		   writeFix( const char *sz, int len , int max ) ;
};


#endif /* PACKER_H_ */
