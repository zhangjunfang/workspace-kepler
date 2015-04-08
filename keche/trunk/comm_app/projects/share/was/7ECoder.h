/*
 * C7ECoder.h
 *
 *  Created on: 2011-7-29
 *      Author: zhangdeke
 */

#ifndef __7ECODER_H_
#define __7ECODER_H_

#ifndef  MAX_BUFF
#define  MAX_BUFF  1500 // 定义最大缓存空间
#endif

class C7eCoder
{
public:
	C7eCoder() ;
	virtual ~C7eCoder() ;
	// 转换编码
	bool Encode( const char *data, const int len ) ;
	// 解码
	bool Decode( const char *data, const int len ) ;

	// 取得解码的长度
	const int    GetSize() ;
	// 取得数据
	const char * GetData() ;

private:
	// 数据长度
	int   _len  ;
	// 解析数据
	char *_data ;
	// 数据BUF
	char _buf[MAX_BUFF] ;
	// 数据临时指针
	char *_temp ;
};

#endif /* C7ECODER_H_ */
