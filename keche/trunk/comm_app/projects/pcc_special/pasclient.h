/**********************************************
 * pasclient.h
 *
 *  Created on: 2011-07-28
 *      Author: humingqing
 *    Comments: 与监管平台对接处理
 *********************************************/

#ifndef __PASCLIENT_H__
#define __PASCLIENT_H__

#include "interface.h"
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <BaseClient.h>
#include "usermgr.h"
#include <ProtoParse.h>
#include <Session.h>
#include "servicecaller.h"
#include "filecacheex.h"
#include "postquery.h"
#include "statinfo.h"

#define PAS_USER_TAG 	"PASCLIENT"
#define PAS_TAG_LEN     9

class PasClient :
	public BaseClient , public IPasClient , public IOHandler , public IUserNotify
{
	struct SmsNotify {
		time_t btime;
		time_t ltime;
		int count;
	};
public:
	PasClient( CServiceCaller &srvCaller ) ;
	virtual ~PasClient() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始
	virtual bool Start( void ) ;
	// 停止
	virtual void Stop( void ) ;
	// 向PAS交数据
	virtual void HandleClientData( const char *code, const char *data, int len ) ;
	// 处理收着的来自省平台DOWN的数据
	virtual void HandlePasDownData( const int access, const char *data, int len ) ;
	// 向PAS交数据通过接入码
	virtual void HandlePasUpData( const int access, const char *data, int len ) ;
	// 添加MACID到SEQID的映射关系
	virtual void AddMacId2SeqId( unsigned short msgid, const char *macid, const char *seqid ) ;
	// 通过MACID和消息内容取得对应数据
	virtual bool GetMacId2SeqId( unsigned short msgid, const char *macid, char *seqid ) ;
	// 关闭主链路的连接请求
	virtual void Close( int accesscode ) ;
	// 更新当前连接状态
	virtual void UpdateSlaveConn( int accesscode, int state ) ;
	// 直接断开对应省的连接处理
	virtual void Enable( int areacode , int flag ) ;

protected:
	// 处理外部数据
	virtual int  HandleQueue( const char *id, void *buf, int len , int msgid = 0 ) ;

	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	virtual void on_dis_connection( socket_t *sock );
	//为服务端使用
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};

	virtual void TimeWork();
	virtual void NoopWork();

	// 通知用户状态变化
	virtual void NotifyUser( const _UserInfo &info , int op ) ;

private:
	// 发送到PAS数据
	bool SendDataToUser( const string &area_code, const char *data, int len);
	// 连接服务器处理
	bool ConnectServer(User &user, int time_out /*= 10*/);
	// 处理离线用户
	void HandleOfflineUsers( void ) ;
	// 处理在线用户
	void HandleOnlineUsers(int timeval) ;
	// 发送5B编码处理
	bool Send5BCodeData( socket_t *sock, const char *data, int len  , bool bflush = false ) ;
	// 发送循环码处理
	bool SendCrcData( socket_t *sock, const char* data, int len) ;
	// 处理数据包
	void HandleOnePacket( socket_t *sock, const char* data , int len ) ;
	// 取得当前用户的区域编码
	int  GetAreaCode( User &user ) ;
	// 从USERINFO转换为User处理
	void ConvertUser( const _UserInfo &info, User &user ) ;
	// 加密处理数据
	bool EncryptData( unsigned char *data, unsigned int len , bool encode ) ;
	// 发送短信、邮件报警
	void sendSmsNotify(const User &user);

private:
	// 环境指针处理
	ISystemEnv  *		_pEnv ;
	// 最后一次访问时间
	time_t				_last_handle_user_time ;
	// 协议解析
	ProtoParse 			_proto_parse;
	// 用户列表管理
	CUserMgr  			_online_user;
	// 下行IP地址
	string				_down_ip ;
	// 下行端口
	unsigned short      _down_port ;
	// MACID到SEQID的映射表
	CSessionMgr			_macid2seqid ;
	// 业务异步http回调
	CServiceCaller     &_srvCaller;
	// 文件缓存对象
	CFileCacheEx		_filecache ;
	// 平台查岗数据管理
	CPostQueryMgr		_postquerymgr ;
	// PCC数据统计服务
	CStatInfo			_statinfo ;
	// 平台查岗文件的路径
	std::string 		_postpath ;
	//
	map<string, SmsNotify> _smsNotifySet;
	//
	share::Mutex           _smsNotifyMtx;
	//
	string                 _smsNotifyUrl;
	//
	string                 _smsNotifyMail;
	//
	string                 _smsNotifyTitle;
};

#endif /* LISTCLIENT_H_ */
