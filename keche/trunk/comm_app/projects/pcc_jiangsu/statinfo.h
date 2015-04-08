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

class CStatInfo
{
	// 在线车辆统计
	struct _CarInfo
	{
		time_t		  _time ;
		std::string   _carnum   ;
		unsigned char _carcolor ;
		unsigned int  _errcnt   ;
		_CarInfo *_next, *_pre ;
	};

	typedef std::map<std::string,_CarInfo*> CMapCar ;
	// 在线的客户端统计
	struct _ClientInfo
	{
		std::string 	_ip ;
		// TCP的时间
		unsigned short 	_tcp ;
		// 服务的UDP端口
		unsigned short 	_udp ;
		// 最后一次时间
		time_t		    _time ;
		// 登陆时间
		time_t			_login ;
		// 上传数据出错次数
		unsigned int    _errcnt;
		// 上传的数据个数
		unsigned int 	_send ;
		// 接收到的数据个数
		unsigned int 	_recv ;
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

	typedef std::map<std::string,_ClientInfo*> CMapClient ;
public:
	CStatInfo() ;
	~CStatInfo() ;

	// 设置车辆总数
	void SetTotal( unsigned int total ) ;
	// 设置的客户的IP和端口
	void SetClient( const char *ip, unsigned short tcpport, unsigned short udpport ) ;
	// 是否出错
	void AddVechile( const char *ip, const char *carnum, unsigned char color, bool error ) ;
	// 接收到的个数
	void AddRecv( const char *ip ) ;
	// 发送出去的个数
	void AddSend( const char *ip ) ;
	// 检测是否超时
	void Check( void ) ;
	// 打印当前XML的数据
	void Print( const char *xml ) ;

private:
	// 删除客户端数据
	void DelClient( _ClientInfo *p ) ;
	// 检测客户端数据
	int CheckClient( _ClientInfo *p , time_t now ) ;
	// 添加客户端
	_ClientInfo* AddClient( const char *ip, unsigned short tcpport, unsigned short udpport ) ;
	// 清除所有数据
	void Clear( void ) ;

private:
	// 车辆总数
	unsigned int _ntotal ;
	// 在线车辆数
	unsigned int _nonline ;
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
	// 同步操作锁
	share::Mutex _mutex ;
};


#endif /* STATINFO_H_ */
