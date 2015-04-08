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
#include <qstring.h>
#include <interpacker.h>
#include <filecache.h>

class CPicClient ;
class MsgClient :
	public BaseClient , public IMsgClient, public IOHandler
{
public:
	MsgClient() ;
	virtual ~MsgClient() ;
	// 初始化
	virtual bool Init( ISystemEnv *pEnv );
	// 开始服务
	virtual bool Start( void );
	// 停止服务
	virtual void Stop();
	// 上传数据
	virtual void HandleData( const char *data, int len , bool pic ) ;

public:
	// 数据到来时处理
	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	// 断开连接处理
	virtual void on_dis_connection( socket_t *sock );
	//为服务端使用
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};

	virtual void TimeWork();
	virtual void NoopWork();
	// 构建登陆信息数据
	virtual int  build_login_msg(User &user, char *buf, int buf_len);
	// 回调处理文件缓存中数据
	virtual int HandleQueue( const char *sid , void *buf, int len , int msgid = 0 ) ;

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
	ISystemEnv  *		  _pEnv ;
	// 在线用户处理
	OnlineUser   		  _online_user;
	// 用户类型
	CInterSpliter   	  _packspliter ;
	 // 文件数据缓存
	CFileCache	    	  _filecache ;
	// 是否开启中心同步
	bool 				  _enable ;
	// 图片服务对象
	CPicClient			 *_picclient;
};

#endif /* LISTCLIENT_H_ */
