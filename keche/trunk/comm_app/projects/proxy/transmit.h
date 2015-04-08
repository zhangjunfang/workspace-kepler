/***********************************************************************
 ** Copyright (c)2009,北京千方科技集团有限公司
 ** All rights reserved.
 **
 ** File name  : CountryServer.h
 ** Author     : lizp (lizp.net@gmail.com)
 ** Date       : 2010-1-7 下午 02:29:25
 ** Comments   : 连接全国平台类
 ** 2011-07-24 humingqing
 *    修改，统一到环境对象部件中，主要去掉缓存队列，实现由接收线程驱动数据处理
 ***********************************************************************/

#ifndef _TRANSMIT_SERVICE_H
#define _TRANSMIT_SERVICE_H

#include "interface.h"
#include "ProtoParse.h"
#include "OnlineUser.h"
#include "crc16.h"
#include "BaseTools.h"
#include <BaseServer.h>
#include "filecacheex.h"
#include "datastat.h"

class Transmit :
	public BaseServer , public IMasServer, public IOHandler
{
public:
	Transmit();
	~Transmit() ;

	virtual bool Init(ISystemEnv *pEnv ) ;
	virtual bool Start( void ) ;
	virtual void Stop( void ) ;
	// 纷发上传MAS数据
	virtual void HandleMasUpData( const char *data, int len ) ;
	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	virtual void on_dis_connection( socket_t *sock );
	// 处理数据的回调方法
	virtual int HandleQueue( const char *uid , void *buf, int len , int msgid = 0 ) ;

protected:
	bool ConnectServer(User &user, unsigned int timeout = 5);

private:

	virtual void TimeWork() ;
	virtual void NoopWork() ;

	// 发送重新处理循环码的数据
	bool SendCrcData( socket_t *sock, const char* data, int len ) ;
	// 加密解密算法
	bool EncryptData( unsigned char *data, unsigned int len , bool encode ) ;
	// 发送MAS的数据
	bool SendMasData( const char *data, int len ) ;

private:
	ProtoParse 		_proto_parse;

	User 			_client;
	User 			_server;  //对方连过过跟它对话的user，而不是listen的User

	int         	_verify_code;
	unsigned int 	_access_code;
	unsigned int 	_user_name;
	string 			_user_password;
	string 			_down_ip ;

	unsigned int 	_M1 ;
	unsigned int 	_IA1 ;
	unsigned int    _IC1 ;

	// 文件缓存对象
	CFileCacheEx	_filecache ;
	// 环境对象
	ISystemEnv     *_pEnv ;
	// 下发流速统计
	CStat			_recvstat ;
	// 发送流统计
	CStat		    _sendstat ;
};

#endif

