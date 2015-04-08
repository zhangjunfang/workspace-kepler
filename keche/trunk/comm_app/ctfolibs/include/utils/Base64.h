/***********************************************************************
 ** Copyright (c)2011
 ** All rights reserved.
 **
 ** File name  : CBase64
 ** Author     : humingqing ( qshuihu@gmail.com )
 ** Date       : 2011-07-26 下午 13:36:38
 ** Comments   : Base64编码和解码
 ***********************************************************************/

#ifndef __BASE64_H__
#define __BASE64_H__

class CBase64  
{
public:
	CBase64();
	virtual ~CBase64();

public:
	// 编码
    bool Encode( const char *data, const int len ) ;
    // 解码
	bool Decode( const char *data, const int len ) ;
	// 取得数据
	char * GetBuffer( void ) { return m_pData; }
	// 取得数据长度
	int    GetLength( void ) { return m_nSize; }

private:
    char* m_pData ;
    int   m_nSize ;
};

#endif
