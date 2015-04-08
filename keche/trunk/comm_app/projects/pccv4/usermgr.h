/*
 * usermgr.h
 *
 *  Created on: 2011-11-28
 *      Author: humingqing
 */

#ifndef __USERMGR_H__
#define __USERMGR_H__

#include <OnlineUser.h>

class CUserMgr
{
	typedef std::map<int,string>   CMapCode ;
public:
	CUserMgr(){}
	~CUserMgr(){}

	//0 success; -1,此用户用已经存在,且不论其是否在线。
	bool AddUser(const string &user_id, User &user) {
		if ( ! _onlineuser.AddUser( user_id, user )  ) {
			// 添加失败直接返回
			return false ;
		}
		// 添加到接入码对应用户中
		AddMapCode( user_id, user ) ;

		return true ;
	}

	// 取得用户通过FD
	User GetUserBySocket( socket_t *sock ) {
		// 取得用户通过用户的fd
		return _onlineuser.GetUserBySocket( sock ) ;
	}

	// 通过用户ID来取得用户
	User GetUserByUserId(const string &user_id){
		// 取得用户通过用户ID
		return _onlineuser.GetUserByUserId( user_id ) ;
	}

	// 通过接入码取得用户
	User GetUserByAccessCode( int accesscode ){
		User user ;
		string key = GetMapUserId( accesscode ) ;
		if ( key.empty() ) {
			return user ;
		}
		return _onlineuser.GetUserByUserId( key ) ;
	}

	// 删除用户通过FD
	void DeleteUser( socket_t *sock ){
		// 删除接入码的关系
		User user = _onlineuser.DeleteUser( sock ) ;
		if ( user._user_id.empty() )
			return ;
		DelMapCode( user._access_code ) ;
	}

	// 删除用户
	void DeleteUser(const string &user_id){
		// 删除接入码关系
		User user =  _onlineuser.DeleteUser( user_id ) ;
		if ( user._user_id.empty() )
			return ;
		DelMapCode( user._access_code ) ;
	}

	// 取得在线用户
	vector<User> GetOnlineUsers(){
		// 取得在线用户
		return _onlineuser.GetOnlineUsers() ;
	}

	// 取得当前离线用户
	vector<User> GetOfflineUsers(int timeout){
		vector<User> vec = _onlineuser.GetOfflineUsers(timeout) ;
		if ( vec.empty() ) {
			return vec;
		}

		for ( int i = 0; i < (int)vec.size(); ++ i ) {
			DelMapCode( vec[i]._access_code ) ;
		}
		return vec ;
	}

	// 更改用户的状态
	bool SetUser(const string &user_id,User &user){
		if ( user._user_state == User::OFF_LINE ) {
			// 需要添加接入码的关系
			AddMapCode( user_id, user ) ;
		}
		return _onlineuser.SetUser( user_id, user ) ;
	}
private:
	// 添加接入码
	void AddMapCode( const string &userid, const User &user ){
		share::Guard g( _mutex ) ;
		if ( user._access_code <= 0 )
			return ;

		CMapCode::iterator it = _mapcode.find( user._access_code ) ;
		if ( it == _mapcode.end() ) {
			_mapcode.insert( make_pair( user._access_code, userid ) ) ;
		} else {
			it->second = userid ;
		}
	}

	// 删除接入码
	void DelMapCode( const int accesscode ){
		share::Guard g( _mutex ) ;

		CMapCode::iterator it = _mapcode.find( accesscode ) ;
		if ( it == _mapcode.end() )
			return ;
		_mapcode.erase( it ) ;
	}

	// 根据接入码取得KEYID
	const string GetMapUserId( const int accesscode ){
		share::Guard g( _mutex ) ;
		CMapCode::iterator it = _mapcode.find( accesscode ) ;
		if ( it == _mapcode.end() )
			return "";
		return it->second ;
	}

private:
	// 在线用户列表
	OnlineUser   _onlineuser;
	// 接入码对应关系
	CMapCode   	 _mapcode ;
	// 接入码锁
	share::Mutex _mutex ;
};

#endif /* USERMGR_H_ */
