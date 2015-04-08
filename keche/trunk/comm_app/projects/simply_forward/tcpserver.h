#ifndef _SF_TCP_SERVER_
#define _SF_TCP_SERVER_ 1

#include "sfpacker.h"

#include "interface.h"
#include <BaseServer.h>
#include <OnlineUser.h>

#include "../tools/tqueue.h"

class TcpServer : public BaseServer, public ITcpServer
{
	// 环境指针处理
	ISystemEnv  *		_pEnv;
	// 接收线程数
	unsigned int 		_thread_num;
	// 最大用户存活时间
	unsigned int 		_max_timeout;
	// 数据分包对象
	CSFSpliter          _spliter;
	// 在线用户处理
	OnlineUser          _online_user;
	// 转发的目录地址
	vector<string>      _dstAddrs;
public:
	TcpServer(ISystemEnv *pEnv);
	~TcpServer();

	virtual bool Init(int port, const vector<string> &addrs);
	// 启动节点客户
	virtual bool Start( void );
	// 重载STOP方法
	virtual void Stop( void );

	virtual void on_data_arrived(socket_t *sock, const void* data, int len);
	virtual void on_dis_connection(socket_t *sock);
	virtual void on_new_connection(socket_t *sock, const char* ip, int port);

	virtual void TimeWork();
	virtual void NoopWork();

	// 删除连接通道
	virtual bool ChkChannel(const string &userid);
	// 向分发服务提交消息
	virtual bool HandleData(const string &userid, const void *data, int len);
private:
	// 检测连接状态
	void HandleOfflineUsers();
};

#endif//_SF_TCP_SERVER_
