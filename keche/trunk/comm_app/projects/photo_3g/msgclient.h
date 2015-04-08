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
#include <time.h>
#include <interpacker.h>
#include <set>
#include <string>
using namespace std ;

#include "../tools/tqueue.h"

class MsgClient : public BaseClient , public IMsgClient
{
public:
	MsgClient( void ) ;
	virtual ~MsgClient() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv );
	// 开始服务
	virtual bool Start( void );
	// 停止服务
	virtual void Stop();
	// 数据到来时处理
	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	
public:
	// 断开连接处理
	virtual void on_dis_connection( socket_t *sock );
	//为服务端使用
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};

	virtual void TimeWork();
	virtual void NoopWork();
	// 构建登陆信息数据
	virtual int  build_login_msg(User &user, char *buf, int buf_len);
	//
	virtual void HandleData( const InnerMsg & msg );

protected:
	// 纷发内部数据
	void HandleInnerData( socket_t *sock, const char *data, int len ) ;
	// 纷发登陆处理
	void HandleSession( socket_t *sock, const char *data, int len ) ;
	//
	bool parseInnerMsg(const string &str, InnerMsg &msg);
	//
	bool spellInnerMsg(const InnerMsg &msg, string &str);
	//
	string getInnerMsgArg(const InnerMsg &msg, const string &key);
	//
	bool get3gby2g(const string &sim2g, string &sim3g, string &oem);

private:
	// 环境指针
	ISystemEnv  *	_pEnv ;
	// 内部协议分包对象
	CInterSpliter   _packspliter;
	// 手机号码查询接口
	string          _httpUrl;
	//
	int             _thread_num;
	//
	TimeQueue<string, int> _timeQueue;
};

#endif /* LISTCLIENT_H_ */
