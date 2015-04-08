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
#include <packspliter.h>
#include <qstring.h>

#define MSG_SAVE_CLIENT   "SAVECLIENT"
#define MSG_PIPE_CLIENT   "PIPECLIENT"

class MsgClient : public BaseClient , public IMsgClient
{
public:
	MsgClient( const char *strtype ) ;
	virtual ~MsgClient() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv );
	// 开始服务
	virtual bool Start( void );
	// 停止服务
	virtual void Stop();
	// 数据到来时处理
	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	// 断开连接处理
	virtual void on_dis_connection( socket_t *sock );
	//为服务端使用
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};

	virtual void TimeWork();
	virtual void NoopWork();
	// 向MSG上传消息
	virtual void HandleMsgData( const char *macid, const char *data, int len ) ;
	// 设置MSG的回调对象
	virtual void SetMsgClient( IMsgClient *pClient ){ _pMsgClient = pClient; }
	// 构建登陆信息数据
	virtual int  build_login_msg(User &user, char *buf, int buf_len);

protected:
	// 发送到指定地区码的用户
	bool SendDataToUser( const string &area_code, const char *data, int len ) ;
	// 加载MSG的用户文件
	bool LoadMsgUser( const char *userfile ) ;
	// 纷发内部数据
	void HandleInnerData( socket_t *sock, const char *data, int len ) ;
	// 纷发登陆处理
	void HandleSession( socket_t *sock, const char *data, int len ) ;
	// 处理离线用户
	void HandleOfflineUsers( void ) ;
	// 处理在线用户
	void HandleOnlineUsers(int timeval) ;
	// 加载订阅关系列表
	void LoadSubscribe( User &user ) ;


private:
	// 环境指针
	ISystemEnv  *	_pEnv ;
	// 最后一次访问时间
	time_t		  	_last_handle_user_time ;
	// 在线用户处理
	OnlineUser   	_online_user;
	// 消息转发对象
	IMsgClient     *_pMsgClient ;
	// 会话管理对象
	CSessionMgr  	_session ;
	// 用户类型
	string      	_strclient ;
	// 分包对象处理
	CBrPackSpliter  _packspliter ;
	// 是否为数据路由
	bool 			_dataroute ;
	// 订阅关系列表
	CQString   	    _dmddir ;
};

#endif /* LISTCLIENT_H_ */
