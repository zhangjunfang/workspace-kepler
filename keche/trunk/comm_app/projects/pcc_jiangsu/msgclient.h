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
#include "pccsession.h"
#include <packspliter.h>
#include "statinfo.h"

#define MSG_SUB_TYPE  "DMDATA"
#define MSG_USER_TAG  "MSGCLIENT"

class PConvert ;
class MsgClient : public BaseClient , public IMsgClient
{
public:
	MsgClient( CStatInfo *stat ) ;
	virtual ~MsgClient() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv );
	// 开始服务
	virtual bool Start( void );
	// 停止服务
	virtual void Stop();
	// 向MSG上传消息
	virtual void HandleUpMsgData( const char *code, const char *data, int len ) ;
	// 根据车牌取得对应的手机MAC
	virtual bool GetCarMacId( const char *key, char *macid ) { return _session.GetCarMacId( key, macid); };

public:
	// 数据到来时处理
	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	// 断开连接处理
	virtual void on_dis_connection( socket_t *sock );
	//为服务端使用
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};

	virtual void TimeWork();
	virtual void NoopWork();
	// 构建登陆处理
	virtual int build_login_msg( User &user, char *buf,int buf_len ) ;

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
	// 加载订阅数据
	void LoadSubscribe( socket_t *sock , bool notify ) ;

private:
	// 环境指针
	ISystemEnv    *_pEnv ;
	// 最后一次访问时间
	time_t		   _last_handle_user_time ;
	// 在线用户处理
	OnlineUser     _online_user;
	// 协议转换部件
	PConvert	  *_convert ;
	// 会话管理,这是主要存放车牌和车机号的对应关系
	CPccSession    _session ;
	// 映射文件处理
	string 		   _carmapfile ;
	// 分包对象处理分包
	CBrPackSpliter _packspliter ;
	// 订阅的MAC的数据
	string 		   _submacids ;
	// 统计信息统计数据
	CStatInfo     *_statinfo ;
};

#endif /* LISTCLIENT_H_ */
