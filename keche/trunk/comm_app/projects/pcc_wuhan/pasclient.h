/**********************************************
 * pasclient.h
 *
 *  Created on: 2014-06-30
 *      Author: ycq
 *********************************************/

#ifndef _PASCLIENT_H_
#define _PASCLIENT_H_ 1

#include "interface.h"
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <BaseClient.h>
#include <protocol.h>

class PasClient : public BaseClient , public IPasClient
{
	// 分包对象
	struct CPackSpliter: public IPackSpliter {
		// 分包处理
		virtual struct packet * get_kfifo_packet(DataBuffer *fifo);
		// 释放数据包
		virtual void free_kfifo_packet(struct packet *packet) {
			free_packet(packet);
		}
	};

public:
	PasClient();
	virtual ~PasClient();

	// 初始化
	virtual bool Init(ISystemEnv *pEnv);
	// 开始
	virtual bool Start(void);
	// 停止
	virtual void Stop(void);
	// 向PAS交数据
	virtual bool HandleData(const char *data, int len);

	virtual void on_data_arrived(socket_t *sock, const void* data, int len);
	virtual void on_dis_connection(socket_t *sock);

	virtual void TimeWork();
	virtual void NoopWork();

	virtual int build_login_msg(User &user, char *buf, int buf_len);
private:
	// 环境指针处理
	ISystemEnv  *		_pEnv ;
	// 数据分包对象
	CPackSpliter        _packspliter;
	// 监管客户端线程数
	int                _threadnum;
};

#endif//_PASCLIENT_H_
