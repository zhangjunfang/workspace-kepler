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

#include <inodeface.h>
#include <OnlineUser.h>
#include <icontext.h>
#include <icache.h>

#define MSG_MSGPROC  	0    // 数据处理对象
#define MSG_PLUGIN    	1	 // 插件管理对象

// 这里暂时只有两个处理部件
#define MAX_MSGHANDLE   2    // 暂时定最大的处理部件对象

class ISystemEnv ;
// 内部协议的数据
class InterData
{
public:
	std::string  _transtype ;  // 传输类型
	std::string  _seqid ;	  // 序列号
	std::string  _macid ;	  // MACID
	std::string  _cmtype ;     // 通信类型
	std::string  _command ;	  // 命令模式
	std::string  _packdata ;   // 数据内容
};

#define OP_SUBSCRIBE_DMD			0    // 订阅数据
#define OP_SUBSCRIBE_ADD			1    // 新增数据
#define OP_SUBSCRIBE_UMD			2    // 取消订阅

#define TYPE_SUBSCRIBE_MACID     	0   // 通过MACID订阅
#define TYPE_SUBSCRIBE_GROUP     	1   // 通过组来订阅
// 数据发布对象接口
class IPublisher
{
public:
	virtual ~IPublisher() {} ;
	// 初始化发布对象
	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	// 启动发布对象线程
	virtual bool Start( void ) = 0 ;
	// 停止发布对象线程
	virtual bool Stop( void ) = 0 ;
	// 开始发布数据
	virtual bool Publish( InterData &data, unsigned int cmd , User &user ) = 0 ;
	// 处理数据订阅
	virtual bool OnDemand( unsigned int cmd , unsigned int group, const char *szval, User &user ) = 0 ;
};

// 数据处理对象
class IMsgHandler
{
public:
	virtual ~IMsgHandler() {} ;
	// 初始化
	virtual bool Init( ISystemEnv * pEnv ) = 0 ;
	// 启动服务
	virtual bool Start( void ) = 0 ;
	// 停止服务
	virtual bool Stop( void ) = 0 ;
	// 处理数据
	virtual bool Process( InterData &data , User &user ) = 0 ;
};

// 节点管理服务
class INodeClient
{
public:
	virtual ~INodeClient() {} ;

	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	virtual bool Start( void ) = 0 ;
	// 重载STOP方法
	virtual void Stop( void ) = 0 ;
};

// 同步到中心的MSG的客户端处理连接
class IMsgClient
{
public:
	virtual ~IMsgClient() {} ;
	// 初始化
	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	// 开始服务
	virtual bool Start( void ) = 0 ;
	// 停止服务
	virtual void Stop() = 0 ;
	// 上传数据
	virtual void HandleData( const char *data, int len , bool pic ) = 0 ;
};

// 消息服务管理对象
class IMsgClientServer
{
public:
	virtual ~IMsgClientServer() {} ;

	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	virtual bool Start( void ) = 0 ;
	// 重载STOP方法
	virtual void Stop( void ) = 0 ;
	// 取得在车辆数的压力
	virtual int  GetOnlineSize( void ) = 0 ;
	// 添加在线用户列表
	virtual void AddNodeUser( const char *user, const char *pwd ) = 0 ;
	// 发送数据
	virtual bool Deliver( socket_t *sock, const char *data, int len ) = 0 ;
	// 通过用户ID来发送数据
	virtual bool DeliverEx( const char *userid, const char *data, int len ) = 0 ;
	// 关闭连接
	virtual void Close( socket_t *sock ) = 0 ;
};

/**
登录类型USAVE：存储服务	上行：所有内部协议指令
登录类型UWEB：监管平台	上行：不传输指令
登录类型UANLY：分析服务	上行：D_REPT(位置信息)，U_REPT(跨域位置信息）
*/

#define COMPANY_TYPE 	"PIPE"
#define WEB_TYPE 		"WEB"
#define STORAGE_TYPE 	"SAVE"
#define PROXY_TYPE		"PROXY"
#define SEND_TYPE		"SEND"  // 下发服务
#define MSG_DATA_DM     "DM"  // 数据订阅方式

#define PIPE_SEND_CMD   	0x00000001   // 发送到PIPE
#define UWEB_SEND_CMD   	0x00000002   // 发送到WEB
#define SAVE_SEND_CMD   	0x00000004   // 发送到SAVE
#define SEND_SEND_CMD		0x00000008   // 发送到发送服务

#define MSG_USER_ENCODE     0x0001		 // 是否为加密
#define MSG_USER_DEMAND     0x0002  	 // 是否为定制

// 用户管理对象
class IGroupUserMgr
{
public:
	virtual ~IGroupUserMgr() {} ;
	//0 success; -1,此用户用已经存在,且不论其是否在线。
	virtual bool AddUser(const std::string &user_id,const User &user) = 0;
	// 取得用户通过FD
	virtual bool GetUserBySocket( socket_t *sock , User &user) = 0 ;
	// 通过用户ID来取得用户
	virtual bool GetUserByUserId( const std::string &user_id, User &user ,  bool bgroup = false ) = 0 ;
	// 删除用户通过FD
	virtual void DeleteUser( socket_t *sock ) = 0 ;
	// 删除用户
	virtual void DeleteUser(const std::string &user_id) = 0 ;
	// 取得在线用户
	virtual bool GetOnlineUsers( std::vector<User> &vec ) = 0 ;
	// 更改用户的状态
	virtual bool SetUser(const std::string &user_id,User &user) = 0 ;
	// 检测是否需要发送的数据
	virtual bool GetSendGroup( std::vector<User> &vec, unsigned int hash , unsigned int cmd ) = 0 ;
	// 取得Hash值
	virtual unsigned int GetHash( const char *key , int len ) = 0 ;
	// 当前所有连接数
	virtual int GetSize( void ) = 0 ;
};

class ISystemEnv : public IContext
{
public:
	virtual ~ISystemEnv() {} ;
	// 取得CAS对象
	virtual IMsgClientServer * GetMsgClientServer( void ) = 0 ;
	// 取得用户管理对象
	virtual IGroupUserMgr * GetUserMgr( void ) = 0 ;
	// 取得发布服务对象
	virtual IPublisher *GetPublisher( void ) = 0 ;
	// 取得RedisCache
	virtual IRedisCache * GetRedisCache( void ) = 0 ;
	// 取得对应处理器的对象
	virtual IMsgHandler * GetMsgHandler( unsigned int id ) = 0 ;
	// 取得中心同步msgclient
	virtual IMsgClient  * GetMsgClient( void ) =  0 ;
#ifdef _GPS_STAT
	// 这里处理GPS的数据统计计数器
	virtual void AddGpsCount( unsigned int count ) = 0 ;
	virtual void SetGpsState( bool enable ) = 0 ;
	virtual bool GetGpsState( void ) = 0 ;
	virtual int  GetGpsCount( void ) = 0 ;
#endif
};

#endif
