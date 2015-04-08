/*
 * interheader.h
 *
 *  Created on: 2012-4-26
 *      Author: think
 *  这里主要想实现通用的协议拆分所以就定义了通用协议头，其中type为协议大类主要区分不同格式的协议的
 */

#ifndef __INTERHEADER_H__
#define __INTERHEADER_H__

#pragma pack(1)

struct interheader
{
	unsigned char  tag ;   // 拆分头部标识 0x5b
	unsigned short len ;   // 数据长度，不包括数据头和尾
	unsigned char  flag ;  // 是否加密标识
	unsigned char  type ;  // 协议类型

	interheader()
	{
		tag = 0x5b ;
	}
};

struct interfooter
{
	unsigned char footer ;  // 拆分尾标识 0x5d

	interfooter()
	{
		footer = 0x5d ;
	}
};

////////////////////////////////// 文件管理  ///////////////////////////////////////
/**
 *  这里主要实现一个网络文件服务管理，主要实现类似本地文件存储方式的管理，
 *  实现统一的open、write、read、close四个接口来实现文件存储读写管理
 */
// 使用两个字符的版本号管理
#define BIG_VER_0    		0x01   		// 0字符值
#define BIG_VER_1	 		0x01    	// 1字符值
#define BIG_ERR_SUCCESS   	0x00		// 成功
#define BIG_ERR_FAILED		0x01		// 失败

// 文件管理命令码管理
#define BIG_OPEN_REQ  		0x0001  	// 登陆请求
#define BIG_OPEN_RSP  		0x8001  	// 登陆响应
#define BIG_WRITE_REQ  		0x0002  	// 写文件请求
#define BIG_WRITE_RSP  		0x8002  	// 写文件成功应答
#define BIG_READ_REQ		0x0003  	// 读文件请求
#define BIG_READ_RSP   		0x8003  	// 读文件应答
#define BIG_CLOSE_REQ  		0x0004 		// 退出登陆
#define BIG_CLOSE_RSP  		0x8004 		// 退出登陆成功应答

// 数据通用头部
struct bigheader
{
	unsigned char  	 ver[2] ;  // 版本号管理
	unsigned int   	 seq ;	   // 统一数据序号
	unsigned short   cmd ;     // 命令码管理
	unsigned int     len ;	   // 数据长度

	bigheader()
	{
		ver[0] = BIG_VER_0 ;
		ver[1] = BIG_VER_1 ;
		seq    = 0 ;
		cmd    = 0 ;
		len    = 0 ;
	}
};

// 登陆请求
struct bigloginreq
{
	unsigned char user[20] ;
	unsigned char pwd[20] ;
};

// 登陆响应
struct bigloginrsp
{
	unsigned char result ;
};

// 写数据请求
struct bigwritereq
{
	unsigned char path[256] ;
	unsigned int  data_len ;
	// 数据体内容
};

// 写数据响应
struct bigwritersp
{
	unsigned char result ;
};

// 读数据请求
struct bigreadreq
{
	unsigned char path[256] ;
};

// 读数据响应
struct bigreadrsp
{
	unsigned char result ;
	unsigned int  data_len ;
	// 响应数据内容
};

#pragma pack()

#endif /* INTERHEADER_H_ */
