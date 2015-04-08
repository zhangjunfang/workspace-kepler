#ifndef __INTERFACE_H__
#define __INTERFACE_H__

#include <icontext.h>

class ISystemEnv ;

#include <utility>
using std::pair;
#include <string>
using std::string;
#include <vector>
using std::vector;

// tcp转发服务端
class ITcpServer {
public:
	virtual ~ITcpServer() {} ;
	// 初始化
	virtual bool Init(int port, const vector<string> &addrs) = 0;
	// 开始
	virtual bool Start( void ) = 0;
	// 停止
	virtual void Stop( void ) = 0;
	// 删除连接通道
	virtual bool ChkChannel(const string &userid) = 0;
	// 提交数据处理
	virtual bool HandleData(const string &userid, const void *data, int len) = 0;
};

// tcp转发客户端
class ITcpClient
{
public:
	virtual ~ITcpClient() {} ;
	// 初始化
	virtual bool Init(void) = 0 ;
	// 开始
	virtual bool Start( void ) = 0 ;
	// 停止
	virtual void Stop( void ) = 0 ;
	// 增加连接通道
	virtual bool AddChannel(ITcpServer *svr, const string &userid, const char *ip, int port) = 0;
	// 删除连接通道
	virtual bool DelChannel(const string &userid) = 0;
	// 提交数据处理
	virtual bool HandleData(const string &userid, const void *data, int len) = 0;
};

// udp转发服务端
class IUdpServer {
public:
	virtual ~IUdpServer() {} ;
	// 初始化
	virtual bool Init(int port, const vector<string> &addrs) = 0;
	// 开始
	virtual bool Start( void ) = 0;
	// 停止
	virtual void Stop( void ) = 0;
	// 删除连接通道
	virtual bool ChkChannel(const string &userid) = 0;
	// 提交数据处理
	virtual bool HandleData(const string &userid, const void *data, int len) = 0;
};

// udp转发客户端
class IUdpClient
{
public:
	virtual ~IUdpClient() {} ;
	// 初始化
	virtual bool Init(void) = 0 ;
	// 开始
	virtual bool Start( void ) = 0 ;
	// 停止
	virtual void Stop( void ) = 0 ;
	// 增加连接通道
	virtual bool AddChannel(IUdpServer *svr, const string &userid, const char *ip, int port) = 0;
	// 删除连接通道
	virtual bool DelChannel(const string &userid) = 0;
	// 提交数据处理
	virtual bool HandleData(const string &userid, const void *data, int len) = 0;
};

// 环境对象指针
class ISystemEnv : public IContext
{
public:
	virtual ~ISystemEnv() {} ;
	// 取得tcp转发客户端
	virtual ITcpClient * GetTcpClient( void ) =  0;
	// 取得udp转发客户端
	virtual IUdpClient * GetUdpClient( void ) =  0;
};

#endif
