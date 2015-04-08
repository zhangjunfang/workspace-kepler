/**************************************************************************************
 * ClientAccessServer.h
 *
 *  Created on: 2010-7-12
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments: 2011-07-24
 *     humingqing 框架问题，对于数据处理应该做及时实时处理方式，
 *     每一个环节上的问题都应该准确反应到问题的源头上来,而不应该将数据置于缓存中，
 *     如果后续处理不过来，而数据一直到来就会导致数据不断的积压的情况，最终必然会导致系统崩溃
 *     这种情况不应该这么处理,所以修改通过接收线程来驱动数据的处理
 **************************************************************************************/

#ifndef __CLIENTACCESSSERVER_H__
#define __CLIENTACCESSSERVER_H__

#include "interface.h"
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <BaseServer.h>
#include <list>
#include <protocol.h>
#include <Session.h>
#include "msguser.h"
#include <statflux.h>
#include <interpacker.h>

#ifndef BUF_LEN
#define BUF_LEN 		4096
#endif

class ClientAccessServer: public BaseServer ,
				public IMsgClientServer , public ISessionNotify
{
public:
	ClientAccessServer() ;
	~ClientAccessServer( void ) ;

	//////////////////////////ICasClientServer//////////////////////////////
	// 通过全局管理指针来处理
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始启动服务器
	virtual bool Start( void ) ;
	// STOP方法
	virtual void Stop( void ) ;
	////////////////////////////////////////////////////////////

	virtual void on_data_arrived( socket_t *sock, const void *data, int len);
	virtual void on_dis_connection( socket_t *sock);
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};

	virtual void TimeWork();
	virtual void NoopWork();
	virtual bool HasLogin(const string &user_id);

	// 添用户在线和离线的通知
	virtual void NotifyChange( const char *key, const char *val , const int op ) ;
	// 取得在车辆数的压力
	virtual int  GetOnlineSize( void ) ;
	// 添加在线用户列表
	virtual void AddNodeUser( const char *user, const char *pwd ) ;
	// 发送数据
	virtual bool Deliver( socket_t *sock, const char *data, int len )  ;
	// 发送数据
	virtual bool DeliverEx( const char *userid, const char *data, int len ) ;
	// 关闭连接
	virtual void Close( socket_t *sock ) ;
private:
	// 发送内部数据
	void HandleInterData( socket_t *sock, const char *data, int len, bool flag );

private:
	// 发送数据处理
	bool SendDataToUser(const char *data, int len , const string &userid);
	// 检测用户是否登陆
	int  check_user_info(const string &user_name,const string &user_password);
	// 重新加用户数据
	bool ReloadUserInfo();
	// 发送到所有用户
	bool SendAllUser( const char *data, int len ) ;
	// 发送用户数据
	bool SendUserData( User &user, const char *data, int len ) ;
	// 纷发数据处理
	void DispatchData( User &user, unsigned int cmd, InterData &data ) ;

private:
	// 环境对象指针
	ISystemEnv			  *_pEnv ;
	// 当前在线组用户
	IGroupUserMgr 		  *_pusermgr;
	// MSG当前可用的用户
	CMsgUser			   _msg_user ;
	// 处理线程数
	unsigned  int		   _thread_num ;
	// 用户数据路径
	std::string 		   _user_file ;
	// 数据分包对象
	CInterSpliterEx		   _pack_spliter ;
	// Session管理
	CSessionMgr		   	   _session ;
	// 节点号的ID
	unsigned int 		   _nodeid ;
	// 接收流量统计
	CStatFlux			   _recvstat ;
	// 最大超时时间
	unsigned int 		   _max_timeout ;
	// 位置上报统计
	CStatFlux			   _reportstat ;
	// 是否开启存储服务
	bool 				   _enbale_save ;
	// 是否开启插件服务
	bool 				   _enable_plugin ;
};

#endif /* CLIENTACCESSSERVER_H_ */
