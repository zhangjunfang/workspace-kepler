/*
 * msguser.h
 *
 *  Created on: 2011-11-10
 *      Author: humingqing
 */

#ifndef __MSGUSER_H__
#define __MSGUSER_H__

#include <map>
#include <string>
#include <Mutex.h>
using namespace std ;

class CMsgUser
{
	typedef std::map<std::string,std::string>  CMapUser ;
public:
	CMsgUser() ;
	~CMsgUser() ;

	// 加载用户数据
	bool LoadUser( const char *szfile ) ;
	// 检测用户是否登陆
	int  CheckUser( const char *user ,const char *pwd ) ;
	// 添加用户名
	bool AddUser( const char *user , const char *pwd ) ;

private:
	// 用户锁
	share::Mutex 	       _mutex ;
	// 用户信息
	CMapUser     		   _user_map ;
	// 临时用信息
	CMapUser			   _temp_user ;
};

#endif /* MSGUSER_H_ */
