/**********************************************
 * interface.h
 *
 *  Created on: 2011-07-24
 *      Author: humingqing
 *       Email: qshuihu@gmail.com
 *    Comments: 环境对象接口类定义，主要实现类与类之间交互的接口定义
 *********************************************/
#ifndef _EXCH_INTERFACE_H_
#define _EXCH_INTERFACE_H_ 1

#include <icache.h>
#include <icontext.h>

#include <map>
using std::map;
#include <vector>
using std::vector;
#include <string>
using std::string;

class ISystemEnv ;

// 交换服务服务端
class IExchServer {
public:
	virtual ~IExchServer() = 0;

	virtual bool Init(ISystemEnv *pEnv) = 0;
	virtual bool Start(void) = 0;
	virtual void Stop(void) = 0;

	virtual void HandleData(const char *data, int len) = 0 ;
};

class IProtParse {
public:
	virtual ~IProtParse() = 0;

	virtual bool Init(ISystemEnv *pEnv) = 0;

	virtual size_t enCode(const unsigned char *src, size_t len, unsigned char *dst) = 0;
	virtual size_t deCode(const unsigned char *src, size_t len, unsigned char *dst) = 0;
	virtual void buildExchGenReply(uint32_t seqid, uint32_t dstid, uint8_t res, vector<uint8_t> &resbuf) = 0;
};

// 环境对象指针
class ISystemEnv : public IContext
{
public:
	virtual ~ISystemEnv() = 0;

	//
	virtual IProtParse  * GetProtParse(void) = 0;
	// 取得RedisCache
	virtual IRedisCache * GetRedisCache( void ) = 0 ;
	// 获取交换服务端对象
	virtual IExchServer * GetExchServer(void) = 0;
};

#endif//_EXCH_INTERFACE_H_
