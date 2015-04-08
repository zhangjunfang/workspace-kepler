/**********************************************
 * amqclient.h
 *
 *  Created on: 2014-05-19
 *    Author:   ycq
 *********************************************/

#ifndef _AMQCLIENT_H_
#define _AMQCLIENT_H_ 1

#include <Thread.h>
#include "interface.h"

#include <string>
using std::string;

class AmqClient : public IAmqClient,  public share::Runnable
{

public:
	AmqClient( void ) ;
	virtual ~AmqClient() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv );
	// 开始服务
	virtual bool Start( void );
	// 停止服务
	virtual void Stop();

protected:
	virtual void run(void *param);
private:
	// 环境指针
	ISystemEnv  *_pEnv ;
	// mq服务器地址
	string      _brokerUrl;
	// mq主题名称
	string      _topicFile;
	// 接收mq数据工作线程
	share::ThreadManager  	_recv_thread ;
};

#endif//_AMQCLIENT_H_
