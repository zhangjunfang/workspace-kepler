/*
 * usermgr.cpp
 *
 *  Created on: 2014年1月20日
 *      Author: ycq
 */


#include "usermgr.h"

#include <comlog.h>
#include "../tools/utils.h"

#include <fstream>
using std::ifstream;

UserMgr::UserMgr()
{
	_pEnv = NULL;
	_corp_info_file = "";
}

UserMgr::~UserMgr()
{
}

bool UserMgr::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv ;

	char szbuf[512] = {0} ;

	// 第三方企业文件路径
	if ( ! pEnv->GetString("corp_data_path", szbuf)) {
		OUT_ERROR( NULL, 0, NULL, "get corp_data_path  fail" ) ;
		return false;
	}
	_corp_info_file = szbuf + string("/corp.user");

	if ( ! _time_thread.init( 1, NULL , this ))
	{
		printf( "timeout check thread init failed, %s:%d", __FILE__, __LINE__ ) ;
		return false ;
	}

	return true;
}


bool UserMgr::Start()
{
	_time_thread.start();

	return true;
}

void UserMgr::Stop()
{
	_time_thread.stop();
}

void UserMgr::run(void *param)
{
	set<string> corps;
	map<string, string> corp_detail;
	map<string, set<string> > route_detail;

	pair<map<string, set<string> >::iterator, bool> mret;

	vector<string> keys;
	string key;
	string val;

	ifstream ifs;
	string line;
	vector<string> fields;
	vector<string>::iterator itVs;

	sleep(1); // 等待redis对象初始化

	while(true) {
		corps.clear();
		corp_detail.clear();
		route_detail.clear();

		ifs.open(_corp_info_file.c_str());
		while(getline(ifs, line)) {
			if(line.empty() || line[0] == '#') {
				continue;
			}

			fields.clear();
			if(Utils::splitStr(line, fields, ':') != 5) {
				continue;
			}

			if(fields[0] != "WASCLIENT") {
				continue;
			}

			keys.clear();
			if( ! _pEnv->GetRedisCache()->HKeys(("KCPT." + fields[1]).c_str(), keys) || keys.empty()) {
				continue;
			}

			corps.clear();
			corps.insert(fields[1]);
			for(itVs = keys.begin(); itVs != keys.end(); ++itVs) {
				if(*itVs == "LAST_UPDATE_TIME") {
					continue;
				}

				mret = route_detail.insert(make_pair(*itVs, corps));
				if(mret.second == false) {
					mret.first->second.insert(fields[1]);
				}
			}

			key = fields[1];
			val = fields[2] + ":" + fields[3] + ":" + fields[4];
			corp_detail.insert(make_pair(key, val));
		}
		ifs.close();

		if( ! corp_detail.empty() && ! route_detail.empty()) {
			share::RWGuard guard(_rwMutex, true);

			_corp_detail = corp_detail;
			_route_detail = route_detail;
		}

		sleep(60);
	}
}

set<string> UserMgr::getRoute(const string &macid)
{
	map<string, set<string> >::iterator it;

	share::RWGuard guard(_rwMutex, false);

	it = _route_detail.find(macid);
	if(it != _route_detail.end()) {
		return it->second;
	}

	return set<string>();
}

string UserMgr::getCorpInfo(const string &channelId)
{
	map<string, string>::iterator it;

	share::RWGuard guard(_rwMutex, false);

	it = _corp_detail.find(channelId);
	if(it != _corp_detail.end()) {
		return it->second;
	}

	return string();
}

bool UserMgr::chkRoute(const string &userid)
{
	map<string, set<string> >::iterator itm;
	set<string>::iterator its;

	vector<string> fields;
	if(Utils::splitStr(userid, fields, '_') != 2) {
		return false;
	}

	share::RWGuard guard(_rwMutex, false);

	itm = _route_detail.find(fields[0]);
	if(itm == _route_detail.end()) {
		return false;
	}

	its = itm->second.find(fields[1]);
	if(its == itm->second.end()) {
		return false;
	}

	return true;
}

set<string> UserMgr::getAllRoute()
{
	set<string> routes;
	map<string, set<string> >::iterator itm;
	set<string>::iterator its;

	share::RWGuard guard(_rwMutex, false);

	for(itm = _route_detail.begin(); itm != _route_detail.end(); ++itm) {
		for(its = itm->second.begin(); its != itm->second.end(); ++its) {
			routes.insert(itm->first + "_" + *its);
		}
	}

	return routes;
}
