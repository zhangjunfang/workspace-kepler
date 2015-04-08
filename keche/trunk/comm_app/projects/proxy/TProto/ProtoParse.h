/**********************************************
 * ProtoParse.h
 *
 *  Created on: 2010-7-8
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/

#ifndef __PROTOPARSE_H_
#define __PROTOPARSE_H_

#include "ProtoHeader.h"
#include "BaseTools.h"
#include <iostream>
using namespace std;

#define TRANS_ALL 1
#define TRANS_CAS 2
#define TRANS_PAS 3

#define FROM_CAS 0
#define FROM_PAS 1

const char *get_type(unsigned short msg_type);

class ProtoParse
{
public:
	ProtoParse()
	{
		seq = 0;
	}
	unsigned int get_next_seq()
	{
		return seq++;
	}
	//分析外部数据相关信息。accesscode:数据类型：业务类型：车辆ID
	static string Decoder(const char*data,int len);
	string get_mac_id(	char vehicle_no[21],unsigned char vehicle_color)
	{
		char buffer[32] = {0};
		sprintf(buffer,"%d_",vehicle_color);
		strncpy(buffer+strlen(buffer),vehicle_no,21);
		string str(buffer);
		return str;
	}
	string GetMacId(const char*data,int len);
	string GetCommandId(const char *data,int len);
	// 构建数据
	static void BuildHeader( Header &header, unsigned int msg_len, unsigned int msg_seq, unsigned int msg_type , unsigned int access_code ) ;

private:
	unsigned int seq;
};

// 5B编码转换器
#define  MAX_BUFF  1024 // 定义最大缓存空间
class C5BCoder
{
public:
	C5BCoder() ;
	virtual ~C5BCoder() ;
	// 转换编码，添加密处理的算法
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

#define BUFF_MB  1024*1024  // 最在处理加密的长度
// 加密处理类
class CEncrypt
{
public:
	// 加密处理
	static bool encrypt( unsigned int M1, unsigned int IA1, unsigned int IC1, unsigned char *buffer, unsigned int size ) ;
	// 产生密钥处理
	static unsigned int rand_key( void ) ;
};

#endif /* PROTOPARSE_H_ */
