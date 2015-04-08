/*
 * statinfo.h
 *
 *  Created on: 2012-7-27
 *      Author: humingqing
 *  数据统计处理,处理当前接入用户
 */

#ifndef __STATINFO_H__
#define __STATINFO_H__

#include <string>
#include <map>
#include <time.h>
#include <Mutex.h>

#define STAT_NO	    0x00  // 不需要操作
#define STAT_RECV   0x01  // 接收的数据
#define STAT_SEND   0x02  // 发送的数据
#define STAT_ERROR  0x04  // 错误数据

class CStatInfo
{
	// 在线车辆统计
	struct _CarInfo
	{
		time_t		  _time ;
		std::string   _macid ;
		unsigned int  _errcnt ;
		_CarInfo *_next, *_pre ;
	};

	typedef std::map<std::string,_CarInfo*> CMapCar ;
	// 在线的客户端统计
	struct _ClientInfo
	{
		// 接入省域ID
		unsigned int    _id ;
		// 最后一次时间
		time_t		    _time ;
		// 上传的数据个数
		unsigned int 	_send ;
		// 接收到的数据个数
		unsigned int 	_recv ;
		// 错误的的数据个数
		unsigned int 	_errcnt ;
		// 头指针
		_CarInfo 	  * _head ;
		// 尾指针
		_CarInfo 	  * _tail ;
		// 元素个数
		int 		    _size ;
		// 车辆对应的列表
		CMapCar		    _mpcar ;
		// 双向链表指针
		_ClientInfo    *_next, *_pre ;
	};

	typedef std::map<unsigned int,_ClientInfo*> CMapClient ;
public:
	CStatInfo( const char *name ) ;
	~CStatInfo() ;
	// 是否出错
	void AddVechile( unsigned int id, const char *macid , int flag = STAT_NO ) ;
	// 接收到的个数
	void AddRecv( unsigned int id ) ;
	// 发送出去的个数
	void AddSend( unsigned int id ) ;
	// 检测是否超时
	void Check( void ) ;

private:
	// 删除客户端数据
	void DelClient( _ClientInfo *p ) ;
	// 检测客户端数据
	int CheckClient( _ClientInfo *p , time_t now ) ;
	// 添加客户端
	_ClientInfo* AddClient( unsigned int id ) ;
	// 清除所有数据
	void Clear( void ) ;

private:
	// 当前在线总车辆数
	int 		 _ncar ;
	// 在线客户端链表
	_ClientInfo *_head ;
	// 在线客户端尾指针
	_ClientInfo *_tail ;
	// 在线元素的个数
	int 		 _size ;
	// 对就的MAP的映射
	CMapClient   _mpClient ;
	// 最后一次使用时间
	time_t 	     _lasttime ;
	// 统计服务的名称
	std::string  _name ;
	// 同步操作锁
	share::Mutex _mutex ;
};


#endif /* STATINFO_H_ */
