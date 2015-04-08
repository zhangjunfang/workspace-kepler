/**********************************************
 * ClientAccessServer.h
 *
 *  Created on: 2010-7-12
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/


#ifndef __CLIENTACCESSSERVER_H_
#define __CLIENTACCESSSERVER_H_

#include <std.h>
#include "interface.h"
#include <DataPool.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <BaseServer.h>
#include "GbProtocolHandler.h"
#include <protocol.h>
#include "scpmedia.h"
#include "packmgr.h"
#include <statflux.h>
#include "blacklist.h"
#include "iplist.h"
#include "queuemgr.h"
#include <interpacker.h>

#ifdef  BUF_LEN
#undef  BUF_LEN
#endif
#define BUF_LEN 		4096
#define CARID_OFFSET 	10000000000
#define STORAGE_SEQ 	"000000_0000000000_0"
#define SEND_TIME_OUT 	20
#define MAX_BUF_LEN     2048
#define MAX_CONN_TIME   60    // 如果1分钟内没有位置信息就要处理一下心跳

class ClientAccessServer : public BaseServer, public IServer, public IQCaller
{
public:
	ClientAccessServer( CacheDataPool &cache_data_pool ) ;
	~ClientAccessServer() ;
	// 初始化系统
	bool Init( ISystemEnv *pEnv ) ;
	// 开始启动系统
	bool Start( void ) ;
	// 停止系统
	void Stop( void ) ;
	// 纷发数据处理
	void HandleDownData( const char *userid, const char *data_ptr,int  data_len , unsigned int seq = 0 , bool send = true );
	// 取得在线车辆
	int  GetOnlineSize( void ) { return _online_count; }
	// 发送TTS语音播报
	void SendTTSMessage( const char *userid, const char *msg, int len ) ;
	// 调用超时重发数据
	virtual bool OnReSend( void *data ) ;
	// 调用超时后重发次数据删除数据
	virtual void Destroy( void *data ) ;

protected:
	virtual void on_data_arrived( socket_t *sock , const void *data, int len );
	virtual void on_dis_connection( socket_t *sock );
	virtual void on_new_connection( socket_t *sock , const char* ip, int port );
	// 检测IP是否为合法的有效的IP接入
	virtual bool check_ip( const char *ip ) ;

	// 处理一个数据包
	void handle_one_packet( socket_t *sock ,const char *data,int len);
	// 消息处理
	void processMsgGb808(socket_t *sock ,const char *data, int len, const string &str_car_id);
	// 发送到指定的用户
	bool SendDataToUser( const string &user_id,const char *data,int data_len);
	// 发送7E编码的数据
	bool Send7ECodeData( socket_t *sock , const char *data, int len ) ;
	// 发送响应数据，主要实现如果有TCP通道优先TCP
	bool SendResponse( socket_t *sock, const char *id , const char *data, int len ) ;
	// 处理自定义指令上传
	void sendOtherCmd(const string &macid, uint16_t msgid, const char* ptr, int len);

	virtual void TimeWork();
	virtual void NoopWork();
	virtual bool HasLogin(string &user_id);
private:
	// 环境对象
	ISystemEnv		    *_pEnv ;
	// 在线用户个数
	unsigned int 		_online_count ;
	// 对于_cache_data_pool的相关处理都是在这个类中，故将_cache_data_pool设成这个类私有的。
	CacheDataPool 		&_cache_data_pool;
	// 在线用户
	OnlineUser 			 _online_user;
	// 协议解析对象
	GbProtocolHandler 	*_gb_proto_handler;
	// 线程数
	unsigned int 		 _thread_num ;
	// 分包对象
	C808Spliter		 	 _pack_spliter ;
	// 多媒上传
	CScpMedia			 _scp_media ;
	// 长数据组包处理
	CPackMgr			 _pack_mgr ;
	// 简单流量统计
	CStatFlux			 _recvstat ;
	// 最大用户存活时间
	unsigned int 		 _max_timeout ;
	// 最长用户数据存活时间
	unsigned int 		 _max_pack_live;
	// 统计手机号
	string 				 _statphone ;
	// 黑名单列表
	CBlackList		     _blacklist ;
	// 黑名单加载文件路径
	string				 _blackpath ;
	// IP黑名单列表
	CIpList				 _ipblack ;
	// 加载IP的列表
	string 				 _ippath ;
	// 发送数据队列管理对象
	CQueueMgr			*_queuemgr ;
	// 0关闭注册鉴权，1开启注册鉴权
	int                  _secureType;
	// 默认的OEM，4C54
	string               _defaultOem;
};


#endif /* CLIENTACCESSSERVER_H_ */
