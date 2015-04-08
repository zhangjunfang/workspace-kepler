/*
 * userloader.cpp
 *
 *  Created on: 2011-12-29
 *      Author: Administrator
 */

#include <stdio.h>
#include "userloader.h"
#include <comlog.h>
#include <tools.h>

#include <vector>
using std::vector;
#include <fstream>
using std::ifstream;

#include "../tools/utils.h"

// 转换在整型变量
static int my_atoi( const char *ptr ) {
	if ( ptr == NULL ) {
		return 0 ;
	}
	int len = strlen( ptr ) ;
	for ( int i = 0; i < len; ++ i ) {
		if ( ptr[i] >= '0' && ptr[i] <= '9' ) {
			continue ;
		}
		return 0 ;
	}
	return atoi( ptr ) ;
}

bool CUserLoader::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv;

	return true;
}

//
bool CUserLoader::loadSubscribe(const string &code, const string &path,
		Macid2Channel &macid2Channel, Plate2Macid &plate2macid)
{
	ifstream ifs;
	string line;
	vector<string> fields;

	int i;
	string macid;
	string value;

	set<string> channels;
	Macid2Channel::iterator m2cite;
	pair<Macid2Channel::iterator, bool> ret;

	ifs.open((path + "/" + code).c_str());
	while(getline(ifs, line)) {
		fields.clear();
		if(Utils::splitStr(line, fields, ':') < 3) {
			continue;
		}

		macid = fields[0];
		value = code;
		for(i = 1; i < fields.size(); ++i) {
			value += ":" + fields[i];
		}

		channels.clear();
		channels.insert(value);

		ret = macid2Channel.insert(make_pair(macid, channels));
		if(ret.second == false) {
			ret.first->second.insert(value);
		}

		plate2macid.insert(make_pair(fields[2], macid));
	}
	ifs.close();

	return true;
}

// 加载文件数据
bool CUserLoader::LoadFile( const char *file, const char *path, CGroupUsers &users )
{
	if ( file == NULL )
		return false ;

	char buf[1024] = {0};
	FILE *fp = NULL;
	fp = fopen( file, "r" );
	if (fp == NULL) {
		OUT_ERROR( NULL, 0, NULL, "Load pcc user file %s failed", file ) ;
		return false;
	}

	CGroupUsers::iterator it ;

	Macid2Channel macid2channel;
	Plate2Macid   plate2macid;

	int count = 0 ;
	while (fgets(buf, sizeof(buf), fp)) {
		unsigned int i = 0;
		while (i < sizeof(buf)) {
			if (!isspace(buf[i]))
				break;
			i++;
		}
		if (buf[i] == '#')
			continue;

		char temp[1024] = {0};
		for (int i = 0, j = 0; i < (int)strlen(buf); ++ i ) {
			if (buf[i] != ' ' && buf[i] != '\r' && buf[i] != '\n') {
				temp[j++] = buf[i];
			}
		}

		string line = temp;
		//1:10.1.99.115:8880:user_name:user_password:A3
		vector<string> vec_line ;
		if ( ! splitvector( line, vec_line, ":" , 0 ) ){
			continue ;
		}
		if ( vec_line.size() < 7 ) continue ;
		// PASCLIENT:110:192.168.5.45:9880:701115:701115:701115
		IUserNotify::_UserInfo  info ;
		info.tag  	 = vec_line[0] ;
		info.code 	 = vec_line[1] ;
		info.ip   	 = vec_line[2] ;
		info.port 	 = atoi( vec_line[3].c_str() ) ;
		info.user 	 = vec_line[4] ;
		info.pwd  	 = vec_line[5] ;
		info.type 	 = vec_line[6] ;

		// 处理接入码操作
		int accesscode = my_atoi( info.type.c_str() ) ;
		if ( accesscode > 0 ) {
			bool encrpty = false ;
			// 参数个数是否大于7个参数
			if ( vec_line.size() > 7 ) {
				// 解析密钥 M1_IA1_IC1
				vector<string> vec2 ;
				splitvector( vec_line[7], vec2, "_" , 0 ) ;
				if ( vec2.size() == 3 ) { // 主要针对同步程序的导致的数据错误
					if ( ! vec2[0].empty() && ! vec2[1].empty() && ! vec2[2].empty() ) {

						_userkey.AddKey( accesscode,
								atoi( vec2[0].c_str() ) ,
								atoi( vec2[1].c_str() ) ,
								atoi( vec2[2].c_str() ) ) ;

						encrpty = true ;
					}
				}
			}
			// 如果为不加密就直接去掉了
			if ( ! encrpty ) {
				// 如果去掉加密处理就清除密钥
				_userkey.DelKey( accesscode ) ;
			}

			loadSubscribe(info.code, path, macid2channel, plate2macid);
		}

		string key = info.tag + info.code ;
		// 添加到用户队列中
		it = users.find( vec_line[0] ) ;
		if ( it != users.end() ) {
			CUserMap &mp = it->second ;
			CUserMap::iterator itx = mp.find( key ) ;
			if ( itx != mp.end() ) {
				itx->second = info ;
			} else {
				mp.insert( make_pair( key, info ) ) ;
			}
		} else {
			CUserMap mp ;
			mp.insert( make_pair( key, info )  ) ;
			users.insert( make_pair( info.tag, mp ) ) ;
		}

		++ count ;
	}
	fclose(fp);
	fp = NULL;

	if(macid2channel.empty() == false && plate2macid.empty() == false) {
		share::RWGuard guard(_rwMutex, true);

		_macid2channel = macid2channel;
		_plate2macid = plate2macid;
	}

	// OUT_PRINT( NULL, 0, NULL, "load pcc user success %s, count %d" , file , count ) ;

	return true ;
}

// 添加组对象
bool CUserLoader::SetNotify( const char *tag, IUserNotify *notify )
{
	share::Guard guard( _mutex ) ;

	// 查找是否已经存在
	CGroupMap::iterator it = _groupuser.find( tag ) ;
	if ( it == _groupuser.end() ) {
		CUserMgr mgr ;
		mgr.SetNotify( notify ) ;
		// 处理添加用户操作
		_groupuser.insert( make_pair(tag, mgr) ) ;
	} else {
		it->second.SetNotify( notify ) ;
	}

	return true ;
}

// 加载用户数据
bool CUserLoader::LoadUser( const char *file, const char *path )
{
	CGroupUsers users ;
	if ( ! LoadFile( file, path, users) ) {
		return false ;
	}

	share::Guard guard( _mutex ) ;
	if ( users.empty() )
		return false ;

	// 处理所有用户处理
	CGroupUsers::iterator it ;
	CGroupMap::iterator  itx ;
	CUserMap::iterator itxx;

	for ( it = users.begin(); it != users.end(); ++ it ) {
		CUserMap &user = it->second ;

		itx = _groupuser.find( it->first ) ;
		if ( itx != _groupuser.end() ) {
			itx->second.Add( user ) ;
		} else {
			CUserMgr mgr ;
			mgr.Add( user ) ;
			_groupuser.insert( make_pair(it->first, mgr ) ) ;
		}

		//使用PASCLIENT配置数据更新路由数据
		if(it->first != "PASCLIENT") {
			continue;
		}

		string sareaids ;
		for(itxx = user.begin(); itxx != user.end(); ++itxx) {
			if ( ! sareaids.empty() ) {
				sareaids += "," ;
			}
			sareaids += itxx->second.code ;
		}
		// 如果省域不为空则直接组包处理
		if( ! sareaids.empty() ) {
			string sbuf = "AREA_CODE " + sareaids + " \r\n" ;
			_pEnv->GetPccServer()->updateAreaids( sbuf );
		}
	}

	return true ;
}

// 添加用户数据
void CUserLoader::CUserMgr::Add( CUserMap &users )
{
	if ( users.empty() )
		return ;

	int flag = 0 ;

	CUserMap::iterator it , itx ;
	// 处理添加和修改用户
	for ( it = users.begin(); it != users.end(); ++ it ) {
		itx = _users.find( it->first ) ;
		if ( itx != _users.end() ) {
			// 如果用户数据发生变化
			if ( IUserNotify::Compare( itx->second, it->second ) ) {
				continue ;
			}
			itx->second = it->second ;
			flag = USER_CHGED ;
		} else {
			_users.insert( make_pair( it->first, it->second ) ) ;
			flag = USER_ADDED ;
		}

		if( _notify ) {
			// 通知外部新增用户处理
			_notify->NotifyUser( it->second, flag ) ;
		}
	}

	// 处得删除用户
	for ( it = _users.begin(); it != _users.end(); ) {
		itx = users.find( it->first ) ;
		if ( itx != users.end() ) {
			++ it ;
			continue ;
		}
		if( _notify ) {
			// 通知外部删除用户处理
			_notify->NotifyUser( it->second, USER_DELED ) ;
		}
		_users.erase( it ++ ) ;
	}
}

// 设置通知回调队象
void CUserLoader::CUserMgr::SetNotify( IUserNotify *notify )
{
	// 更改回调对象要处理其中添加的用户
	_notify = notify ;

	if ( _users.empty() )
		return ;

	CUserMap::iterator it ;
	for ( it = _users.begin(); it != _users.end(); ++ it ) {
		// 通知外部新增用户处理
		_notify->NotifyUser( it->second, USER_ADDED ) ;
	}
}


bool CUserLoader::getChannels(const string &macid, set<string> &channels)
{
	share::RWGuard guard(_rwMutex, false);

	Macid2Channel::iterator it;
	if((it = _macid2channel.find(macid)) == _macid2channel.end()) {
		return false;
	}
	channels = it->second;

	return true;
}

bool CUserLoader::getSubscribe(list<string> &macids)
{
	share::RWGuard guard(_rwMutex, false);

	Macid2Channel::iterator it;
	for(it = _macid2channel.begin(); it != _macid2channel.end(); ++it) {
		macids.push_back(it->first);
	}

	return true;
}

bool CUserLoader::getMacid(const string &plate, string &macid)
{
	share::RWGuard guard(_rwMutex, false);

	Plate2Macid::iterator it;
	if((it = _plate2macid.find(plate)) == _plate2macid.end()) {
		return false;
	}
	macid = it->second;

	return true;
}
