/*
 * mybase64.h
 *
 *  Created on: 2012-3-2
 *      Author: humingqing
 *
 *  处理江苏平台特殊的BASE64编码
 */

#ifndef __MYBASE64_H__
#define __MYBASE64_H__

#include <string>

class CBase64Ex
{
public:
	CBase64Ex() ;
	~CBase64Ex() ;
	// 加密处理
	bool Encode( const char *data, int len ) ;
	// 解密处理
	bool Decode( const char *data, int len ) ;
	// 取得数据
	char * GetBuffer( void ) { return _pData; }
	// 取得长度
	int    GetLength() { return _size; }
	// 加密数据
	const std::string  EncodeEx( const char *data, int len ) ;
	// 解密数据
	const std::string  DecodeEx( const char *data, int len ) ;

private:
	// 数据对象
	char *_pData ;
	// 数据大小
	int   _size  ;
};


#endif /* MYBASE64_H_ */
