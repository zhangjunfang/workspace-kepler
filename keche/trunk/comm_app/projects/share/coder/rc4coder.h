/*
 * r4coder.h
 *
 *  Created on: 2012-4-26
 *      Author: humingqing
 *  R4加密算法，一般用于无线加密
 */

#ifndef __R4CODER_H__
#define __R4CODER_H__

class CRC4Coder
{
public:
	// 默认加密的码表
	CRC4Coder(const char *key="1q2w3e123!@#") ;
	~CRC4Coder( void ) ;
	// 加密和解密处理
	unsigned char* r4code( unsigned char *val, unsigned int len ) ;

private:
	// 初始化表值
	void rc4_init( unsigned char *k , unsigned int n ) ;
	// 产生异或值对象
	unsigned char rc4_key() ;
	// 数据交换
	void rc4_swap( unsigned char *s, unsigned int i , unsigned int j ) ;

private:
	unsigned char _sv[256] ;  // 加密结果
	unsigned int  _i ;		  // 位置记录
	unsigned int  _j ;		  // 位置记录
	unsigned char _sk[256] ;  // 原始码表
	unsigned int  _klen;	  // 码表的长度
};


#endif /* R4CODER_H_ */
