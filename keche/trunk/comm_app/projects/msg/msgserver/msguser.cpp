/*
 * msguser.cpp
 *
 *  Created on: 2011-11-10
 *      Author: humingqing
 */

#include "msguser.h"

CMsgUser::CMsgUser()
{

}

CMsgUser::~CMsgUser()
{

}

int CMsgUser::CheckUser( const char *user ,const char *pwd )
{
	share::Guard g( _mutex ) ;
	{
		CMapUser::iterator it = _user_map.find(user);
		if( it == _user_map.end()) {
			// 如果文件用户查找失败则直接从临时用户中查
			it = _temp_user.find(user) ;
			if ( it == _temp_user.end() )
				return -1;
		}

		if ( strcmp( it->second.c_str(), pwd ) == 0 ) {
			return 0;
		}
		return -2;
	}
}

// 添加用户名
bool CMsgUser::AddUser( const char *user , const char *pwd )
{
	if ( user == NULL  || pwd == NULL )
		return false ;

	share::Guard g( _mutex ) ;
	{
		CMapUser::iterator it = _temp_user.find( user ) ;
		if ( it != _temp_user.end() )
			return false ;

		_temp_user.insert( make_pair( user, pwd ) ) ;
		return true ;
	}
}

// 加载用户对象
bool CMsgUser::LoadUser( const char *szfile )
{
	if( szfile == NULL )
		return false;

	char buf[1024] = {0};
	FILE *fp = fopen( szfile, "r" ) ;
	if (fp == NULL){
		return false;
	}

	_mutex.lock() ;

	_user_map.clear();
	while (fgets(buf, sizeof(buf), fp))
	{
		unsigned int i = 0;
		while (i < sizeof(buf)) {
			if (!isspace(buf[i]))
				break;
			i++;
		}
		if (buf[i] == '#')
			continue;

		char temp[256] = {0};
		for (int i = 0, j = 0; i < (int) strlen(buf); i++){
			if (buf[i] != ' ' && buf[i] != '\r' && buf[i] != '\n') {
				temp[j++] = buf[i];
			}
		}

		string line = temp;

		string::size_type pos = line.find_last_of(":", string::npos);
		if (pos == string::npos)
			continue;

		string user_name     = line.substr(0,pos);
		string user_password = line.substr(pos +1 ,line.length() - pos);

		_user_map.insert(make_pair(user_name,user_password));
	}
	_mutex.unlock() ;

	fclose(fp);
	fp = NULL;

	return true;
}


