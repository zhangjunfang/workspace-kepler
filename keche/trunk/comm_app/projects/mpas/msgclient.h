/**********************************************
 * msgclient.h
 *
 *  Created on: 2011-07-28
 *    Author:   humingqing
 *    Comments: 实现与消息服务中心通信以及数据转换
 *********************************************/

#ifndef __MsgCLIENT_H__
#define __MsgCLIENT_H__

#include "interface.h"
#include <BaseClient.h>
#include <OnlineUser.h>
#include <time.h>
#include <Session.h>
#include <interpacker.h>
#include <set>
#include <string>
#include "pconvert.h"
using namespace std ;

class MsgClient : public BaseClient , public IMsgClient
{
public:
	MsgClient( PConvert *convert ) ;
	virtual ~MsgClient() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始服务
	virtual bool Start( void ) ;
	// 停止服务
	virtual void Stop() ;
	// 数据到来时处理
	virtual void on_data_arrived( socket_t *sock, const void* data, int len ) ;
	// 向MSG上传消息
	virtual bool Deliver( const char *data, int len ) ;
	// 添加用户处理
	virtual void AddUser( const char *ip, unsigned short port, const char *user, const char *pwd ) ;
	// 处理删除服务
	virtual void DelUser( const char *ip, unsigned short port ) ;
	// 添加手机MAC
	virtual void AddMac2Car( const char *macid, const char *vechile ) ;

public:
	// 断开连接处理
	virtual void on_dis_connection( socket_t *sock );
	//为服务端使用
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};

	virtual void TimeWork();
	virtual void NoopWork();
	// 构建登陆信息数据
	virtual int  build_login_msg(User &user, char *buf, int buf_len);

protected:
	// 纷发内部数据
	void HandleInnerData( socket_t *sock, const char *data, int len ) ;
	// 纷发登陆处理
	void HandleSession( socket_t *sock, const char *data, int len ) ;
	// 处理离线用户
	void HandleOfflineUsers( void ) ;
	// 处理在线用户
	void HandleOnlineUsers(int timeval) ;

private:
	// 环境指针
	ISystemEnv  *	_pEnv ;
	// 最后一次访问时间
	time_t		  	_last_handle_user_time ;
	// 在线用户处理
	OnlineUser   	_online_user;
	// 消息转发对象
	IMsgClient     *_pMsgClient ;
	// 分包对象处理
	CInterSpliter   _packspliter ;
	// 协议转换对象
	PConvert 	   *_convert ;
	// 手机号与车牌对应关系
	CSessionMgr     _macid2carnum ;
};

#endif /* LISTCLIENT_H_ */
