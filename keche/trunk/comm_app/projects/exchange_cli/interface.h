/**********************************************
 * interface.h
 *
 *  Created on: 2014-05-15
 *      Author: ycq
 *    Comments: 环境对象接口类定义，主要实现类与类之间交互的接口定义
 *********************************************/
#ifndef __INTERFACE_H__
#define __INTERFACE_H__

#include <icontext.h>
#include <icache.h>

class ISystemEnv ;

#include <set>
using std::set;
#include <map>
using std::map;
#include <vector>
using std::vector;
#include <string>
using std::string;

#include <stdint.h>

class IAmqClient {
public:
	virtual ~IAmqClient() = 0;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv) = 0;
	// 开始
	virtual bool Start(void) = 0;
	// 停止
	virtual void Stop(void) = 0;
};

// MSG数据处理部件
class IMsgClient {
public:
	virtual ~IMsgClient() = 0;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv) = 0;
	// 开始
	virtual bool Start(void) = 0;
	// 停止
	virtual void Stop(void) = 0;
	// 下行数据处理函数
	virtual bool HandleData(const char *data, int len) = 0;
};

// 数据推送服务
class IExchClient {
public:
	virtual ~IExchClient() = 0;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv) = 0;
	// 开始
	virtual bool Start(void) = 0;
	// 停止
	virtual void Stop(void) = 0;
	// 上行数据处理函数
	virtual bool HandleData(uint32_t seqid, const char *data, int len) = 0;
};

//
class IProtParse {
public:
	virtual ~IProtParse() = 0;

	virtual bool Init(ISystemEnv *pEnv) = 0;

	virtual size_t enCode(const unsigned char *src, size_t len, unsigned char *dst) = 0;
	virtual size_t deCode(const unsigned char *src, size_t len, unsigned char *dst) = 0;

	virtual bool parseInnerParam(const string &detail, map<string, string> &params) = 0;

	virtual uint32_t buildExchLogin(const string &un, const string &pw, vector<unsigned char> &resBuf) = 0;
	virtual uint32_t buildExchHeartBeat(vector<unsigned char> &resbuf) = 0;
	virtual uint32_t buildExchSubUnits(vector<unsigned char> &resbuf) = 0;
	virtual uint32_t buildExchSubCmdid(vector<unsigned char> &resbuf) = 0;

	virtual uint32_t buildExchStatus(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf) = 0;
	virtual uint32_t buildExchTermReg(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf) = 0;
	virtual uint32_t buildExchTermAuth(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf) = 0;
	virtual uint32_t buildExchLocation(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf) = 0;
	virtual uint32_t buildExchHistory(const string &num, const map<string,string> &params, vector<unsigned char> &resBuf) = 0;
	virtual uint32_t buildExchMMData(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf) = 0;
	virtual uint32_t buildExchMMEvent(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf) = 0;
	virtual uint32_t buildExchDriverEvent(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf) = 0;
	virtual uint32_t buildExchTermVersion(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf) = 0;
	virtual uint32_t buildExchTTData(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf) = 0;

	virtual uint32_t buildExchBaseTable(const string &str, uint16_t msgid, vector<uint8_t> &resbuf) = 0;
};

// 环境对象指针
class ISystemEnv: public IContext {
public:
	virtual ~ISystemEnv() = 0;
	// 取得MSG的部件
	virtual IMsgClient * GetMsgClient(void) = 0;
	//
	virtual IExchClient * GetExchClient(void) = 0;
	//
	virtual IProtParse  * GetProtParse(void) = 0;
	// 取得RedisCache
	virtual IRedisCache * GetRedisCache(void) = 0;
};

#endif
