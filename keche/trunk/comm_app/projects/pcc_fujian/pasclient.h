/**********************************************
 * pasclient.h
 *
 *  Created on: 2011-07-28
 *      Author: humingqing
 *    Comments: 与监管平台对接处理
 *********************************************/

#ifndef __PASCLIENT_H__
#define __PASCLIENT_H__

#include "interface.h"
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <BaseClient.h>
#include <protocol.h>
#include "pconvert.h"

class PasClient : public BaseClient , public IPasClient
{
	// 分包对象
	class CPackSpliter : public IPackSpliter
	{
	public:
		CPackSpliter() {}
		virtual ~CPackSpliter() {}
		// 分包处理
		virtual struct packet * get_kfifo_packet( DataBuffer *fifo ) ;
		// 释放数据包
		virtual void free_kfifo_packet( struct packet *packet ) {
			free_packet( packet ) ;
		}
	};

public:
	PasClient( PConvert *convert ) ;
	virtual ~PasClient() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始
	virtual bool Start( void ) ;
	// 停止
	virtual void Stop( void ) ;
	// 向PAS交数据
	virtual void HandleData( const char *data, int len ) ;

	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	virtual void on_dis_connection( socket_t *sock );
	//为服务端使用
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};

	virtual void TimeWork();
	virtual void NoopWork();

private:
	// 环境指针处理
	ISystemEnv  *		_pEnv ;
	// 数据分包对象
	CPackSpliter        _packspliter;
	// 最后一次心跳的时间
	time_t				_last_noop ;
	// 协议转换对象
	PConvert		   *_convert ;
};

#endif /* LISTCLIENT_H_ */
