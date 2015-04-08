/**********************************************
 * systemenv.h
 *
 *  Created on: 2011-07-24
 *      Author: humingqing
 *       Email: qshuihu@gmail.com
 *    Comments: 环境对象类，主要管理需要交互的对象，对象与对象交互之间的中界，
 *    这样，任何两个对象之间的交互都可以通过对象管理部件来实现直接交互，使各部件之间透明化，
 *    也使得结构合理化，对于每一个部件实现，都必需实现Init Start Stop三个功能主要实现系统
 *    之间的统一规范化处理。
 *********************************************/
#ifndef __SYSTEMENV_H__
#define __SYSTEMENV_H__

#include <std.h>
#include "interface.h"
#include <DataPool.h>
#ifdef _GPS_STAT
#include <gpsstat.h>
#endif

class CCConfig ;
class CSystemEnv : public ISystemEnv
{
	// 处理用户序列
	class CSequeueGen
	{
	public:
		CSequeueGen() {}
		~CSequeueGen() {}

		void ResetSequeue( const string &seq ) {
			share::Guard g( _mutex ) ;
			map<string,unsigned short>::iterator it = _map_seq.find(seq) ;
			if ( it != _map_seq.end() ) {
				it->second = 0 ;
			}
		}

		unsigned short  GetSequeue( const string &seq , int len ) {
			share::Guard g( _mutex ) ;

			unsigned short seq_id = 0 ;

			map<string,unsigned short>::iterator it = _map_seq.find(seq) ;
			if ( it == _map_seq.end() ) {
				seq_id = seq_id + len ;
				_map_seq.insert( pair<string,unsigned short>( seq, seq_id ) ) ;
			} else {
				seq_id = it->second ;
				seq_id = seq_id + len ;
				if ( seq_id >= 0xffff ) {
					seq_id = len ;
				}
				it->second = seq_id ;
			}
			return seq_id ;
		}

	private:
		share::Mutex  			    _mutex ;
		map<string,unsigned short>  _map_seq ;
	};
public:
	CSystemEnv() ;
	~CSystemEnv() ;

	// 初始化系统
	bool Init( const char *file , const char *logpath , const char *userfile,const char *logname);

	// 开始系统
	bool Start( void ) ;

	// 停止系统
	void Stop( void ) ;

	// 取得整形值
	bool GetInteger( const char *key , int &value ) ;
	// 取得字符串值
	bool GetString( const char *key , char *value ) ;
	// 取得用户数据位置
	const char * GetUserPath( void )  { return _user_file_path.c_str(); }
	// 取得用户序列
	unsigned short  GetSequeue( const char *key , int len = 1 )  { return _seq_gen.GetSequeue(key, len);  }
	// 重置用户序列
	void ResetSequeue( const char *key ) { _seq_gen.ResetSequeue(key); }

	// 取得Msg处理对象
	IClient * GetMsgClient( void ) { return _msg_client; }
	// 取得WAS对象
	IServer * GetClientServer( void ) { return _was_server; }
	// 取得RedisCache
	IRedisCache * GetRedisCache( void ) { return _rediscache; }

#ifdef _GPS_STAT
	// 这里处理GPS的数据统计计数器
	void AddGpsCount( unsigned int count )  { _gpsstat.Add(count); 			}
	void SetGpsState( bool enable ) 		{ _gpsstat.SetEnable(enable) ;  }
	bool GetGpsState( void ) 				{ return _gpsstat.Enable(); 	}
	int  GetGpsCount( void )				{ return _gpsstat.Size(); 		}
#endif

private:
	// 初始化日志系统
	bool InitLog(const char *logpath,const char *logname);

private:
	// 数据缓存池对象
	CacheDataPool	   _cache_pool ;
	// 配置文件类
	CCConfig		  *_config ;
	// 用户文件路径
	std::string 	   _user_file_path ;
	// 消息中心客户端
	IClient		      *_msg_client ;
	// WAS服务器和客户端
	IServer       	 * _was_server ;
	//
	IRedisCache      * _rediscache ;
	// 是否初始化
	bool 			   _initialed ;
	// 用户序列生成器
	CSequeueGen		   _seq_gen ;

#ifdef _GPS_STAT
	// 统计位置数据上报
	CGpsStat 		   _gpsstat ;
#endif
};

#endif
