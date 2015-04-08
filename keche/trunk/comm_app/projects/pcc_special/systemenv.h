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

#include <Mutex.h>
#include "interface.h"
#include "servicecaller.h"

class CUserLoader ;
class CCConfig ;
class CSystemEnv : public ISystemEnv
{
	class CSequeue
	{
	public:
		CSequeue():_seq_id(0) {}
		~CSequeue() {}

		// 取得序列
		unsigned int get_next_seq( void ) {
			share::Guard g( _mutex ) ;
			if ( _seq_id >= 0xffffffff ) {
				_seq_id = 0 ;
			}
			return ++ _seq_id ;
		}

	private:
		// 序列生成锁
		share::Mutex  _mutex ;
		// 序列ID号
		unsigned int  _seq_id ;
	};
public:
	CSystemEnv() ;
	~CSystemEnv() ;

	// 初始化系统
	bool Init( const char *file , const char *logpath, const char *userfile, const char *logname ) ;

	// 开始系统
	bool Start( void ) ;

	// 停止系统
	void Stop( void ) ;

	// 取得整形值
	bool GetInteger( const char *key , int &value ) ;
	// 取得字符串值
	bool GetString( const char *key , char *value ) ;
	// 取得缓存的序号
	void GetCacheKey( unsigned int seq, char *key ) ;
	// 取得序号
	unsigned int GetSequeue( void ) { return _seq_gen.get_next_seq(); };
	// 添加组对象
	bool SetNotify( const char *tag, IUserNotify *notify );
	// 加载用户数据
	bool LoadUserData( void ) ;
	// 取得加密密钥
	bool GetUserKey( int accesscode, int &M1, int &IA1, int &IC1 ) ;
	// 清理会话处理
	void ClearSession( const char *key ) ;

	// 使用macid获取需要转发的监管平台
	bool getChannels(const string &macid, set<string> &channels);
	// 获取所有macid用于向msg订阅
	bool getSubscribe(list<string> &macids);
	// 使用车牌号码获取macid
	bool getMacid(const string &plate, string &macid);

	// 取得PAS对象
	IPasClient * GetPasClient( void ) { return _pas_client; }
	// 取得MSG Client 对象
	IMsgClient * GetMsgClient( void ) { return _msg_client; }
	// 取得MsgCache对象
	IMsgCache  * GetMsgCache( void ) { return _msg_cache ; }
	// 取得PCC服务器
	IPccServer * GetPccServer( void ){ return _pcc_server; }
	// 取得RedisCache
	IRedisCache * GetRedisCache( void ) { return _rediscache; }

	virtual const char * GetUserPath( void ) { return ""; }
private:
	// 初始化日志系统
	bool InitLog( const char *logpath, const char *logname ) ;
	// 初始化用户数据
	bool InitUser( void ) ;

private:
	// 配置文件类
	CCConfig		  *_config ;
	// 是否初始化
	bool 			   _initialed ;
	// 国标协议服务器
	IPasClient 		 * _pas_client ;
	// 实现监控平台转接协议
	IMsgClient		 * _msg_client ;
	// 数据缓存
	IMsgCache		 * _msg_cache ;
	// PCC服务端口
	IPccServer		 * _pcc_server ;
	// 序号产生类
	CSequeue		   _seq_gen ;
	// 用户数据加载对象
	CUserLoader		 * _userloader ;
	// 用户数据文件路径
	std::string 	   _user_file ;
	// 订阅文件路径
	std::string        _dmddir;
	// 业务异步http回调
	CServiceCaller     _srvCaller ;
	// RedisCache
	IRedisCache      * _rediscache ;
};

#endif
