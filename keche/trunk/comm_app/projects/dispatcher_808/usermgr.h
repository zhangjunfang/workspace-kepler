/*
 * usermgr.h
 *
 *  Created on: 2014年1月20日
 *      Author: ycq
 */

#ifndef _USERMGR_H_
#define _USERMGR_H_ 1

#include <Thread.h>
#include "interface.h"

#include <set>
using std::set;
#include <map>
using std::map;
#include <string>
using std::string;

class UserMgr :  public share::Runnable, public IUserMgr {
	// 环境指针处理
	ISystemEnv  *		      _pEnv ;
	// 超时检测线程
	share::ThreadManager  	  _time_thread ;
	// 容器读写锁
	share::RWMutex            _rwMutex;
	// 对接企业平台配置文件
	string                    _corp_info_file;
	// 对接平台信息
	map<string, string>       _corp_detail;   //转发平台的接入信息
	// 路由映射表
	map<string, set<string> > _route_detail;  //一个手机号码对应多个转发平台
public:
	UserMgr();
	virtual ~UserMgr();

	// 初始化
	virtual bool Init(ISystemEnv *pEnv);
	// 开始
	virtual bool Start(void);
	// 停止
	virtual void Stop(void);

	// 超时检测线程
	virtual void run( void *param );

	// 查询分发路由
	virtual set<string> getRoute(const string &macid);
	// 查询通道信息
	virtual string getCorpInfo(const string &channelId);
	// 确认分发路由
	virtual bool chkRoute(const string &userid);
	// 获取所有路由
	virtual set<string> getAllRoute();
};

#endif//_USERMGR_H_
