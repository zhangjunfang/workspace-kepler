/**********************************************
 * msgclient.h
 *
 *  Created on: 2014-06-30
 *    Author:   ycq
 *********************************************/

#ifndef _MSMCLIENT_H_
#define _MSMCLIENT_H_ 1

#include "interface.h"
#include <BaseClient.h>
#include <interpacker.h>
#include <Mutex.h>

#include <time.h>

class MsgClient : public BaseClient , public IMsgClient {
public:
	MsgClient() ;
	virtual ~MsgClient() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv );
	// 开始服务
	virtual bool Start( void );
	// 停止服务
	virtual void Stop();
	// 向MSG上传消息
	virtual bool HandleData(const char *data, int len);
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
	// 加载订阅关系列表
	void LoadSubscribe() ;
	// 生成内部协议的序列号
	string getSeqid(const string &sim);
	// 查询redis生成macid
	string getMacid(const string &sim);
	// 转换主动上传消息
	bool converUrpt(const string &macid, const string &seqid, const string &param, DataBuffer &buf);
	// 转换回复应答消息
	bool converResp(const string &macid, const string &seqid, const string &param, DataBuffer &buf);
	// 解析内部协议消息体
	void parseParam(const string &param, map<string, string> &detail);
	// 查询获取参数值
	string queryParam(const string & key, const map<string, string> &detail);
private:
	// 环境指针
	ISystemEnv      *_pEnv;
	// 分包对象处理分包
	CInterSpliter    _packspliter;
	// 订阅关系列表
	string           _dmdfile;
	// 图片保存路径
	string           _scppath;
	// 静态数据文件
	string           _datfile;
	// msg客户端工作线程数
	int             _threadnum;
	// 内部协议序列号
	unsigned int   _seqid;
	// 应答缓存容器
	map<string, string> _replycache;
	// 应答缓存同步锁
	share::Mutex        _replymutex;
	// 订阅清单，用于获取macid
	map<string, string> _macidquery;
	// 订阅清单同步锁
	share::Mutex        _macidmutex;
};

#endif//_MSMCLIENT_H_
