#ifndef _WAS_CLIENT_
#define _WAS_CLIENT_ 1

#include "protocol.h"

#include <interface.h>
#include <BaseClient.h>
#include <OnlineUser.h>
#include <interpacker.h>

class WasClient : public BaseClient, public IWasClient
{
	typedef struct HostInfo {
		string ip;
		uint16_t port;
	} HostInfo;

	typedef struct TermInfo {
		string sim;
		string color;
		string plate;
		string termMfrs;
		string termType;
		string termID;
	} TermInfo;
public:
	WasClient();
	~WasClient();

	virtual bool Init( ISystemEnv *pEnv );
	// 启动节点客户
	virtual bool Start( void );
	// 重载STOP方法
	virtual void Stop( void );

	virtual void on_data_arrived(socket_t *sock, const void* data, int len);
	virtual void on_dis_connection(socket_t *sock);
	virtual void on_new_connection(socket_t *sock, const char* ip, int port) {};
	virtual int build_login_msg( User &user, char *buf,int buf_len ) ;

	virtual void TimeWork();
	virtual void NoopWork();

	// 向分发服务提交消息
	virtual bool HandleData(const string &sim, const uint8_t *ptr, size_t len);
	// 新增模拟终端
	virtual bool AddTerminal(const string &sim);
	// 删除模拟终端
	virtual bool DelTerminal(const string &sim);
private:
	void HandleOnlineUsers();
	void HandleOfflineUsers();
	void registerTerm(); //模拟终端注册

	// 创建终端注册消息
	bool buildTerm0100(const TermInfo &term, vector<uint8_t> &msg);
	// 创建终端鉴权消息
	bool buildTerm0102(const string &userid, vector<uint8_t> &msg);
	// 导入转发目的前置机信息
	void loadHost();
	// 发送808数据
	bool Send7ECodeData(socket_t *sock, const uint8_t *ptr, size_t len);
	// 从BCD码中取得手机号
	bool bcd2sim(uint8_t *bcd, string &sim);
	// 计算jt808校验码
	uint8_t get_check_sum(uint8_t *buf, size_t len);

	void sendDataToUser(const User &user, const map<string, vector<unsigned char> > &msgs);

	// 添加模拟终端
	bool addActiveUser(const string &userid, const string &macid);
private:
	// 环境指针处理
	ISystemEnv  *		 _pEnv ;
	// 接收线程数
	unsigned int 		 _thread_num ;
	// 数据分包对象
	C808Spliter          _spliter808 ;
	// 在线用户处理
	OnlineUser           _online_user;
	// 链接空闲时间，秒
	unsigned int       _max_timeout;
	// 第三方前置机配置文件
	string                _hostfile;
	// 第三方前置机配置参数
	map<string, HostInfo> _hostInfo;
};

#endif//_WAS_CLIENT_
