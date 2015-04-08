
#ifndef _EXCH_CLIENT_
#define _EXCH_CLIENT_ 1

#include "exchspliter.h"
#include "tqueue.h"

#include <interface.h>
#include <BaseClient.h>
#include <tchdb.h>

class ExchClient : public BaseClient, public IExchClient
{
	struct DbValue {
		uint16_t cnt;    //重传次数
		uint8_t dat[0];  //重传数据
	};
public:
	ExchClient();
	~ExchClient();

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
	virtual bool HandleData(uint32_t seqid, const char *data, int len);
private:
	size_t enCode(const uint8_t *src, size_t len, uint8_t *dst);
	size_t deCode(const uint8_t *src, size_t len, uint8_t *dst);
	//bool popCacheData(uint32_t seqid, vector<uint8_t> res);
private:
	// 环境指针处理
	ISystemEnv  *		_pEnv ;
	// 接收线程数
	unsigned int 		_thread_num ;
	// 数据分包对象
	CExchSpliter         _spliter_exch;
	// 交换数据超时管理对象
	TimeQueue<uint32_t>  _exch_pack_tqueue;
	// 交换数据缓存对象
	TCHDB               *_exch_pack_tchdb;
	// 交换数据重传次数
	int                  _exch_pack_retries;
	// 交换数据超时同步对象
	share::Mutex         _exch_pack_mutex;
};

#endif//_EXCH_CLIENT_
