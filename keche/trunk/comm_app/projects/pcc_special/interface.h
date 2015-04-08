/**********************************************
 * interface.h
 *
 *  Created on: 2011-07-24
 *      Author: humingqing
 *       Email: qshuihu@gmail.com
 *    Comments: 环境对象接口类定义，主要实现类与类之间交互的接口定义
 *********************************************/

#ifndef __INTERFACE_H__
#define __INTERFACE_H__


#include <OnlineUser.h>
#include <icache.h>
#include <icontext.h>
#include <imsgcache.h>

#include <string>
using std::string;
#include <set>
using std::set;
#include <list>
using std::list;

#define SEQ_HEAD_LEN     11
#define SEQ_HEAD   		 "USERID_UTC_"
#define SEND_ALL   		 "SEND_ALL"
#define DATA_FILECACHE   0  // 数据缓存
#define DATA_ARCOSSDAT	 1  // 跨域控制

#define CONN_MASTER         0    // 主链路处理
#define CONN_SLAVER         1    // 从链路处理
#define CONN_CONNECT      	0	 // 链接处理
#define CONN_DISCONN    	-1   // 断开连接

class ISystemEnv ;

#define USER_ADDED   1  // 添加新用户
#define USER_DELED   2  // 删除用户
#define USER_CHGED   3  // 修改用户
// 处理用户变化的通知对象
class IUserNotify
{
public:
	// PASCLIENT:110:192.168.5.45:9880:701115:701115:701115:M1_IA1_IC1
	// MSGCLIENT:1:10.1.99.115:8880:user_name:user_password:A3
	struct _UserInfo{
		std::string tag  ;
		std::string code ;
		std::string ip   ;
		short 		port ;
		std::string user ;
		std::string pwd  ;
		std::string type ;
	};
	// 比较用户
	static bool Compare( _UserInfo &u1, _UserInfo &u2 ) {
		if ( u1.ip != u2.ip || u1.port != u2.port ||
				u1.user != u2.user || u1.pwd != u2.pwd || u1.type != u2.type ){
			return false ;
		}
		return true ;
	}
public:
	virtual ~IUserNotify() {} ;
	// 通知用户状态变化
	virtual void NotifyUser( const _UserInfo &info , int op ) = 0  ;
};

#define PAS_SUBLINK_ERROR     0x01    // 处理从链路异常
#define PAS_MAINLINK_LOGOUT   0x02    // 处理主链路断连
#define PAS_USERLINK_ONLINE   0x04    // 处理用户连接断连
#define PAS_MAINLINK_ERROR    0x08    // 处理用户主链路异常的情况

// 实现标准国标协议接入
class IPasClient
{
public:
	virtual ~IPasClient() {} ;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv ) = 0 ;
	// 开始线程
	virtual bool Start( void ) = 0 ;
	// 停止处理
	virtual void Stop( void ) = 0 ;
	// 客户对内纷发数据
	virtual void HandleClientData( const char *code, const char *data, const int len ) = 0 ;
	// 处理收着的来自省平台DOWN的数据
	virtual void HandlePasDownData( const int access, const char *data, int len ) = 0 ;
	// 向PAS交数据通过接入码
	virtual void HandlePasUpData( const int access, const char *data, int len ) = 0 ;
	// 添加MACID到SEQID的映射关系
	virtual void AddMacId2SeqId( unsigned short msgid, const char *macid, const char *seqid ) = 0 ;
	// 通过MACID和消息内容取得对应数据
	virtual bool GetMacId2SeqId( unsigned short msgid, const char *macid, char *seqid ) = 0 ;
	// 关闭主链路的连接请求
	virtual void Close( int accesscode ) = 0 ;
	// 更新当前连接状态
	virtual void UpdateSlaveConn( int accesscode, int state ) = 0 ;
	// 直接断开对应省的连接处理，断开从链路处理操作
	virtual void Enable( int areacode , int flag ) = 0 ;
};

// 实现协议转换部件
class IMsgClient
{
public:
	virtual ~IMsgClient() {} ;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv ) = 0 ;
	// 开始
	virtual bool Start( void ) = 0 ;
	// 停止
	virtual void Stop( void ) = 0 ;
	// 向MSG上传消息
	virtual void HandleUpMsgData( const char *code, const char *data, int len )  = 0 ;
	//发送用户数据
	virtual void HandleUserData( const User &user, const char *data, int len ) = 0 ;
	// 加图片数据
	virtual void LoadUrlPic( unsigned int seqid , const char *path ) = 0 ;
};

// PCC服务器
class IPccServer
{
public:
	virtual ~IPccServer() {} ;
	// 初始化
	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	// 开始线程
	virtual bool Start( void ) = 0 ;
	// 停止处理
	virtual void Stop( void ) = 0 ;
	// 从链路主动发起断开主从链的请求
	virtual void Close( int accesscode , unsigned short msgid, int reason ) = 0 ;
	// 更新当前PCC管理省份路由
	virtual void updateAreaids(const string &areaids) = 0;
};

class ISystemEnv : public IContext
{
public:
	virtual ~ISystemEnv() {} ;
	// 添加组对象
	virtual bool SetNotify( const char *tag, IUserNotify *notify ) = 0 ;
	// 加载用户数据
	virtual bool LoadUserData( void ) = 0 ;
	// 取得加密密钥
	virtual bool GetUserKey( int accesscode, int &M1, int &IA1, int &IC1 ) = 0 ;
	// 取得Cache的KEY值
	virtual void GetCacheKey( unsigned int seq, char *key ) = 0 ;
	// 清理会话处理
	virtual void ClearSession( const char *key ) = 0 ;
	// 取得序号处理
	virtual unsigned int GetSequeue( void ) = 0 ;
	// 取得PAS的对象
	virtual IPasClient * GetPasClient( void ) = 0 ;
	// 取得MSG Client对象
	virtual IMsgClient * GetMsgClient( void ) =  0 ;
	// 取得MsgCache对象
	virtual IMsgCache  * GetMsgCache( void ) = 0 ;
	// 取得PCC服务器
	virtual IPccServer * GetPccServer( void ) = 0 ;
	// 取得RedisCache
	virtual IRedisCache * GetRedisCache( void ) = 0 ;

	// 使用macid获取需要转发的监管平台
	virtual bool getChannels(const string &macid, set<string> &channels) = 0;
	// 获取所有macid用于向msg订阅
	virtual bool getSubscribe(list<string> &macids) = 0;
	// 使用车牌号码获取macid
	virtual bool getMacid(const string &plate, string &macid) = 0;
};

#endif
