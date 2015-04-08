/*
 * publish.h
 *
 *  Created on: 2012-4-16
 *      Author: humingqing
 */

#ifndef __PUBLISH_H__
#define __PUBLISH_H__

#include <interface.h>
#include <dataqueue.h>
#include <set>
#include <vector>

class Publisher :
	public IPublisher , public IQueueHandler
{
	// 发布数据对象处理
	struct _pubData
	{
		unsigned int _cmd ;  	 // 需要发送的用户类型
		User		 _user ;	 // 数据来源用户
		InterData    _data ;	 // 发送数据
		_pubData    *_next ;
	};

	// 订阅处理
	class SubscribeMgr
	{
		struct _macList
		{
			std::map<std::string,int> _mkmap ;
		};
	public:
		SubscribeMgr() ;
		~SubscribeMgr() ;

		// 添加订阅车辆信息
		bool Add( unsigned int ncode, const char *macid ) ;
		// 删除订阅车辆信息
		void Remove( unsigned int ncode, const char *macid ) ;
		// 删除当前对象订阅信息
		void Del( unsigned int ncode ) ;
		// 检测是否订阅成功
		bool Check( unsigned int ncode, const char *macid , bool check ) ;

	private:
		// 是否删除
		bool Find( unsigned int ncode, const char *macid , bool erase ) ;
		// 回收所有数据
		void Clear( void ) ;

	private:
		typedef std::map<unsigned int, _macList *>  MapSubscribe;
		// 读写锁处理
		share::RWMutex  _rwmutex ;
		// 订阅处理
		MapSubscribe	_mapSuber ;
	};

public:
	Publisher() ;
	~Publisher() ;
	// 初始化发布对象
	bool Init( ISystemEnv *pEnv ) ;
	// 启动发布对象线程
	bool Start( void ) ;
	// 停止发布对象线程
	bool Stop( void ) ;
	// 开始发布数据
	bool Publish( InterData &data, unsigned int cmd , User &user ) ;
	// 处理数据订阅
	bool OnDemand( unsigned int cmd , unsigned int group, const char *szval, User &user ) ;

public:
	// 线程执行对象方法
	virtual void HandleQueue( void *packet ) ;

private:
	// 发送数据处理
	bool Deliver( _pubData *p ) ;
	// 加订阅关系
	void LoadSubscribe( const char *key, std::vector<std::string> &vec, std::set<std::string> &kset ) ;

private:
	// 环境对象指针
	ISystemEnv * 		  _pEnv ;
	// 订阅数据管理
	SubscribeMgr		  _submgr ;
	// 订阅处理线程个数
	int 				  _nthread ;
	// 用户管理对象
	IGroupUserMgr 		 *_pusermgr ;
	// 消息发送服务
	IMsgClientServer     *_pmsgserver;
	// 数据发布队列
	CDataQueue<_pubData> *_pubqueue ;
	// 数据线程队列
	CQueueThread 		 *_queuethread;
};


#endif /* PUBLISH_H_ */
