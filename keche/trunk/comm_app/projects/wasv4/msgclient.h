/**********************************************
 * MinitoryClient.h
 *
 *  Created on: 2010-7-12
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/

#ifndef __MSGCLIENT_H_
#define __MSGCLIENT_H_

#include "interface.h"
#include <DataPool.h>
#include <BaseClient.h>
#include <GbProtocolHandler.h>
#include <protocol.h>
#include <Monitor.h>
#include <statflux.h>
#include "filecache.h"
#include <interpacker.h>

#define MAX_TIMEOUT_USER     90
#ifdef  MAX_SPLITPACK_LEN
#undef  MAX_SPLITPACK_LEN
#endif
#define MAX_SPLITPACK_LEN    1000   // 最大数据长度
#define WAS_CLIENT_ID        "wascache"

class MsgClient :
	public BaseClient , public IClient , public IOHandler
{
	struct _SetData
	{
		unsigned short msgid  ;
		DataBuffer     buffer ;

		_SetData() {
			msgid  = 0 ;
		}
	};
public:
	MsgClient(CacheDataPool &cache_data_pool) ;
	~MsgClient();

	// 启动
	bool Init( ISystemEnv *pEnv ) ;
	// 开始
	bool Start( void ) ;
	// 停止
	void Stop( void ) ;
	// 纷发数据
	void HandleUpData( const char *data, int len ) ;
	// 缓存数据纷发接口
	int HandleQueue( const char *sid , void *buf, int len , int msgid = 0 ) ;

protected:
	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	virtual void on_dis_connection( socket_t *sock );
	virtual void TimeWork();
	virtual void NoopWork();
	virtual int  build_login_msg(User &user, char *buf, int buf_len);
	// 发送失败的回收数据
	virtual void on_send_failed( socket_t *sock , void* data, int len) ;

private:
	// 发送数据处理
	void SendMsgData( _SetData &val, const string &car_id, const string &mac_id, const string &command , const string &seq ) ;
	// 维护用户连接状态
	void HandleUserStatus();
	// 发送RC4数据
	bool SendRC4Data( socket_t *sock , const char *data, int len ) ;

public:
	void HandleDsetpMsg(string &line);
	void HandleDcallMsg(string &line);
	void HandleDctlmMsg(string &line);
	void HandleDsndmMsg(string &line);
	void HandleReqdMsg( string &line) ;
	void HandleDgetpMsg(string &line) ;

private:
	// 环境对象
	ISystemEnv		  * _pEnv ;
	// 接收线程数
	unsigned int 		_thread_num ;
	// 协议解析对象
	GbProtocolHandler * _gb_proto_handler;
	// 数据对象池
	CacheDataPool 	   &_cache_data_pool;
	// 分包对象
	CInterSpliter		_pack_spliter ;
	// 发送流量统计
	CStatFlux			_sendstat ;
	// 数据队列
	CFileCache			_dataqueue ;
};

#endif /* MINITORYCLIENT_H_ */
