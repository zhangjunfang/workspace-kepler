/*
 * intercoder.h
 *
 *  Created on: 2012-4-26
 *      Author: humingqing
 */

#ifndef __INTERCODER_H__
#define __INTERCODER_H__

#define INTER_MAX_LEN  65535
// 前置机数据加解密
class CInterCoder
{
public:
	CInterCoder() ;
	~CInterCoder() ;
	// 加密算法
	bool Encode( const char *data, int len ) ;
	// 解密处理
	bool Decode( const char *data, int len ) ;
	// 取得缓存
	const char * Buffer( void ) { return _ptr; }
	// 取得长度
	int Length( void ) { return _len; }

private:
	// 数据指针
	char *_ptr ;
	// 数据缓存
	char  _buf[INTER_MAX_LEN] ;
	// 数据长度
	int   _len ;
};


#endif /* WASCODER_H_ */
