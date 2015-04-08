/*
 * userloader.h
 *
 *  Created on: 2011-12-29
 *      Author: Administrator
 */

#ifndef __USERLOADER_H__
#define __USERLOADER_H__

#include <string>
#include <map>
#include <Mutex.h>
#include "interface.h"

// 用户文件加载管理对象
class CUserLoader
{
	// 监管平台加解密密钥处理
	struct _EncryptKey
	{
		int _M1 ;   // M1密钥
		int _IA1 ;  // IA1密钥
		int _IC1 ;	// IC1密钥
	};

	// 处理用户密钥
	class CUserKey
	{
		typedef std::map<int, _EncryptKey>  CMapKey ;
	public:
		CUserKey() {}
		~CUserKey() {}

		// 添加用户密钥
		void AddKey( int accesscode , int M1, int IA1, int IC1 ){
			share::Guard guard( _mutex ) ;
			CMapKey::iterator it = _mapkey.find( accesscode ) ;
			if ( it == _mapkey.end() ) {
				_EncryptKey key ;
				key._M1  = M1  ;
				key._IA1 = IA1 ;
				key._IC1 = IC1 ;
				_mapkey.insert( make_pair(accesscode, key) ) ;
			} else {
				it->second._M1  = M1  ;
				it->second._IA1 = IA1 ;
				it->second._IC1 = IC1 ;
			}
			//printf( "Add M1: %d, IA1: %d, IC1: %d\n" , M1, IA1, IC1 ) ;
		}

		// 删除KEY的值
		void DelKey( int accesscode ) {
			share::Guard guard( _mutex ) ;
			CMapKey::iterator it = _mapkey.find( accesscode ) ;
			if ( it == _mapkey.end() ) {
				return ;
			}
			_mapkey.erase( it ) ;
		}

		// 取得KEY的数据
		bool GetKey( int accesscode, int &M1, int &IA1, int &IC1 ) {
			share::Guard guard( _mutex ) ;
			CMapKey::iterator it = _mapkey.find( accesscode ) ;
			if ( it == _mapkey.end() ) {
				return false ;
			}

			M1  = it->second._M1 ;
			IA1 = it->second._IA1 ;
			IC1 = it->second._IC1 ;

			return true ;
		}

	private:
		// 密钥对应的MAP处理
		CMapKey      _mapkey ;
		share::Mutex _mutex ;
	};

	typedef std::map<std::string, std::set<string> > Macid2Channel;

	typedef std::map<std::string,IUserNotify::_UserInfo>  CUserMap ;
	class CUserMgr
	{
	public:
		CUserMgr() :_notify(NULL){ }
		~CUserMgr() {}
		// 添加用户数据
		void Add( CUserMap &users ) ;
		// 设置通知回调队象
		void SetNotify( IUserNotify *notify ) ;
	private:
		// 用户处理队列
		CUserMap  	 _users ;
		// 用户变更通知对象
		IUserNotify *_notify ;
	};

	typedef std::map<std::string,CUserMap>    CGroupUsers ;
	typedef std::map<std::string,CUserMgr> 	  CGroupMap ;
public:
	CUserLoader() {};
	~CUserLoader(){};

	bool Init( ISystemEnv *pEnv );

	// 添加组对象
	bool SetNotify( const char *tag, IUserNotify *notify ) ;
	// 加载用户数据
	bool LoadUser( const char *file, const char *path ) ;
	// 取得当前接入码的密码
	bool GetUserKey( int accesscode, int &M1, int &IA1, int &IC1 ) {
		// 返回密钥处理
		return _userkey.GetKey( accesscode, M1, IA1, IC1 ) ;
	}

	// 使用macid获取需要转发的监管平台
	bool getChannels(const string &macid, set<string> &channels);
	// 获取所有macid用于向msg订阅
	bool getSubscribe(list<string> &macids);
private:
	// 加载文件数据
	bool LoadFile( const char *file, const char *path, CGroupUsers &users ) ;

	// 加载订阅数据
	bool loadSubscribe(const string &code, const string &path, Macid2Channel &macid2Channel);
private:
	// 环境对象指针
	ISystemEnv		   *_pEnv ;
	// 用户密钥的管理处理
	CUserKey	   _userkey ;
	// 根据用户标识分组处理
	CGroupMap      _groupuser ;
	// 加锁处理操作
	share::Mutex   _mutex ;

	// 映射表读写锁
	share::RWMutex  _rwMutex;
	// 使用macid查找转发的通道、车牌号码
	Macid2Channel  _macid2channel;
};


#endif /* USERLOADER_H_ */
