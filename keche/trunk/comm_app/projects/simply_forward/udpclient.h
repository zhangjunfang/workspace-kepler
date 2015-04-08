#ifndef _SF_UDP_CLIENT_
#define _SF_UDP_CLIENT_ 1

#include "sfpacker.h"

#include "interface.h"
#include <BaseClient.h>
#include <OnlineUser.h>
#include <Mutex.h>

class UdpClient : public BaseClient , public IUdpClient
{
	// 环境指针
	ISystemEnv  *	_pEnv ;
	// 接收线程数
	unsigned int    _thread_num ;
	// 最大用户存活时间
	unsigned int    _max_timeout;
	// 内部协议分包对象
	CSFSpliter      _spliter;
	// 在线用户处理
	OnlineUser      _online_user;
	//
	map<string, vector<char> > _cache;
	//
	share::Mutex    _mutex;
public:
	UdpClient( ISystemEnv *pEnv ) ;
	virtual ~UdpClient() ;

	// 初始化
	virtual bool Init(void);
	// 开始服务
	virtual bool Start( void );
	// 停止服务
	virtual void Stop();

	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	virtual void on_dis_connection( socket_t *sock );

	virtual void TimeWork();
	virtual void NoopWork();
	// 构建登陆信息数据
	virtual bool ConnectServer(User &user, unsigned int timeout);

	// 增加连接通道
	virtual bool AddChannel(IUdpServer *svr, const string &userid, const char *ip, int port);
	// 删除连接通道
	virtual bool DelChannel(const string &userid);
	// 提交数据处理
	virtual bool HandleData(const string &userid, const void *data, int len);
private:
	// 检测连接状态
	void HandleOfflineUsers(void);
};

#endif//_SF_UDP_CLIENT_
