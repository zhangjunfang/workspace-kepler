/**********************************************
 * msgclient.h
 *
 *  Created on: 2011-07-28
 *    Author:   humingqing
 *    Comments: 实现与消息服务中心通信以及数据转换
 *********************************************/

#ifndef __MsgCLIENT_H__
#define __MsgCLIENT_H__

#include <httpclient.h>
#include <asynchttpclient.h>

#include "interface.h"
#include <BaseClient.h>
#include <OnlineUser.h>
#include <time.h>
#include <Session.h>
#include <packspliter.h>
#include <qstring.h>

#define MSG_SAVE_CLIENT   "SAVECLIENT"
#define MSG_PIPE_CLIENT   "PIPECLIENT"

struct WAIT_RSP_DATA {
	string       file;
	string       inner;
};

struct WAIT_RSP_TIME {
	time_t       time;
	unsigned int seqid;
};

class MsgClient : public BaseClient , public IMsgClient, public IHttpCallbacker
{
	typedef map<unsigned int, WAIT_RSP_DATA> WAIT_MAP;
public:
	MsgClient() ;
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

	// 构建登陆信息数据
	virtual int  build_login_msg(User &user, char *buf, int buf_len);

	//
	void ProcHTTPResponse( unsigned int seq , const int err , const CHttpResponse& resp );

protected:
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
	//
	int split2map(vector<string> &vec, map<string, string> &mp, const string &split);
	//
	bool processPic(const string &userid, const string &data, const string &content);
	//
	unsigned int getSeqid() { return __sync_add_and_fetch(&_seqid, 1); };
	//
	bool createDir(const string &file);

private:
	// 环境指针
	ISystemEnv *          _pEnv ;
	// 最后一次访问时间
	time_t                _last_handle_user_time ;
	// 在线用户处理
	OnlineUser            _online_user;
	// 会话管理对象
	CSessionMgr           _session ;
	// 唯一PIPE类型的userid
	string                _pipe_uid;
	// 多个SAVE类型的URL
	map<string, string>   _save_url;
	//
	unsigned int          _seqid;
	//
	WAIT_MAP              _wait_data;
	//
	list<WAIT_RSP_TIME>   _wait_time;
	//
	share::Mutex          _wait_mutex;
	//
	CAsyncHttpClient      _httpClient ;
	// 分包对象处理
	CBrPackSpliter        _packspliter ;
	// 订阅关系列表
	string                _dmddir ;
	//
	string                _pic_path;
	// 是否为单向数据路由
	bool                  _dataroute;
};

#endif /* LISTCLIENT_H_ */
