/*
 * pconvert.h
 *
 *  Created on: 2012-3-2
 *      Author: think
 *
 *  实现内部协议转成江苏监管平台协议
 */

#ifndef __PCONVERT_H__
#define __PCONVERT_H__

#include <string>
#include <vector>
#include <map>
#include <databuffer.h>
#include <Mutex.h>
#include "header.h"
#include <Session.h>

using namespace std ;

class ISystemEnv ;
class PConvert
{
	class CSequeueGen
	{
	public:
		CSequeueGen():_id(0){}
		~CSequeueGen(){}

		unsigned int get_next_seq(void) {
			unsigned int tmp = 0 ;
			_mutex.lock() ;
			tmp = ++ _id ;
			_mutex.unlock() ;
			return tmp ;
		}

	private:
		share::Mutex _mutex ;
		unsigned int _id ;
	};
	typedef map<string,string> MapString ;
public:
	PConvert() ;
	~PConvert() ;

	// 初始化环境对象指针
	bool initenv( ISystemEnv *pEnv ) ;
	// 转换U_REPT指令的数据
	void convert_urept( const string &macid , const string &val, DataBuffer &buf , bool bcall )  ;
	// 转换通用应答的处理
	void convert_comm( const string &seqid, const string &macid, const string &val, DataBuffer &buf ) ;
	// 取得下一个可用序列
	unsigned int get_next_seq( void ) { return _gen.get_next_seq(); }
	// 构建协议头部数据
	void buildheader( DataBuffer &buf, const char *phone, unsigned int len , unsigned int result = 0 , unsigned int seq = 0 ) ;
	// 转换点名处理
	bool build_caller( unsigned int seq, const char *phone, const char *data, int len ) ;
	// 转换拍照消息
	bool build_photo( unsigned int seq, const char *phone, const char *data, int len ) ;
	// 下发调度消息
	bool build_sendmsg( unsigned int seq, const char *phone, const char *data, int len ) ;
	// 发送取回的图片
	void sendpicture( unsigned int seq, const char *data, const int len ) ;

private:
	// 处理监控的参数
	bool parse_jkpt_value( const std::string &param, MapString &val ) ;
	// 取得对应MAP字符值
	bool get_map_string( MapString &map, const std::string &key , std::string &val ) ;
	// 取得对应MAP中整形值
	bool get_map_integer( MapString &map, const std::string &key , int &val ) ;
	// 取得头数据
	bool get_map_header( const std::string &param, MapString &val, int &ntype ) ;
	// 拆分数据到MAP
	bool split2map( const std::string &s , MapString &val ) ;
	// 转换上报的GPS的数据
	bool convertgps( const char *szphone, MapString &mp, DataBuffer &buf , bool bcall ) ;
	// 构建GPS的数据
	bool buildgps( MapString &mp, _Gps &gps ) ;
	// 通过MACID取得对应的终端口
	bool gettermidbymacid( const string &macid, char *szphone ) ;

private:
	// 系统环境对象
	ISystemEnv * _pEnv ;
	// 序列产生器
	CSequeueGen  _gen ;
	// 将MACID转成终端号对应关系
	CSessionMgr  _cache;
	// 本机存放文件路径
	string 		 _picdir ;
};


#endif /* PCONVERT_H_ */
