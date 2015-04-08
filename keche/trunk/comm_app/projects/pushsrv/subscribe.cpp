/*
 * subscribe.cpp
 *
 *  Created on: 2012-5-4
 *      Author: yuanchunqing
 */
#include "subscribe.h"

#include <utility>
using std::pair;

Subscribe::Subscribe()
{
}

Subscribe::~Subscribe()
{
}

bool Subscribe::regUser(const string &user, set<string> &macids)
{
	NODE node;
	map<string, NODE >::iterator msnIteMacid;
	pair<map<string, NODE >::iterator, bool> ret;

	set<string> *macidsPtr;
	set<string>::iterator ssIte;

	set<string> users;

	share::Guard guard(_mutex);

	//正式环境从redis中获取macids
	//macids.insert("E004_15810900000");

	node.def = macids;
	node.sub = macids;
	ret = _user2macid.insert(pair<string, NODE >(user, node));
	if(ret.second == false) {
		return false;
	}

	users.insert(user);
	node.def = users;
	node.sub = users;
	macidsPtr = &macids;
	for (ssIte = macidsPtr->begin(); ssIte != macidsPtr->end(); ++ssIte) {
		msnIteMacid = _macid2user.find(*ssIte);
		if (msnIteMacid == _macid2user.end()) { //终端首次被订阅
			_macid2user.insert(pair<string, NODE>(*ssIte, node));
		} else {
			msnIteMacid->second.def.insert(user);
			msnIteMacid->second.sub.insert(user);
		}
	}

	return true;
}

bool Subscribe::unregUser(const string &user)
{
	map<string, NODE >::iterator msnIteUser;
	map<string, NODE >::iterator msnIteMacid;

	set<string> *macidsPtr;
	set<string>::iterator ssIte;

	share::Guard guard(_mutex);

	msnIteUser = _user2macid.find(user);
	if(msnIteUser == _user2macid.end()) {
		return false;
	}

	macidsPtr = &msnIteUser->second.def;
	for(ssIte = macidsPtr->begin(); ssIte != macidsPtr->end(); ++ssIte) {
		msnIteMacid = _macid2user.find(*ssIte);
		if(msnIteMacid == _macid2user.end()) {
			continue;
		}

		msnIteMacid->second.sub.erase(user);
		msnIteMacid->second.def.erase(user);

		if(msnIteMacid->second.sub.empty() && msnIteMacid->second.def.empty()) {
			_macid2user.erase(msnIteMacid);
		}
	}

	_user2macid.erase(msnIteUser);

	return true;
}

//追加用户订阅，不影响以前的订阅
bool Subscribe::apped(const string &user, const string &macid)
{
	map<string, NODE >::iterator msnIteUser;
	map<string, NODE >::iterator msnIteMacid;

	set<string> *macidsDef;

	share::Guard guard(_mutex);

	msnIteUser = _user2macid.find(user);
	if(msnIteUser == _user2macid.end()) {
		return false;
	}

	macidsDef = &msnIteUser->second.def;
	if(macidsDef->find(macid) == macidsDef->end()) {
		return false;
	}

	msnIteMacid = _macid2user.find(macid);
	if(msnIteMacid == _macid2user.end()) {
		return false;
	}

	msnIteUser->second.sub.insert(macid);
	msnIteMacid->second.sub.insert(user);

	return true;
}

//删除单个终端订阅
bool Subscribe::erase(const string &user, const string &macid)
{
	map<string, NODE >::iterator msnIteUser;
	map<string, NODE >::iterator msnIteMacid;

	set<string> *macidsDef;
	set<string> *macidsSub;

	share::Guard guard(_mutex);

	msnIteUser = _user2macid.find(user);
	if(msnIteUser == _user2macid.end()) {
		return false;
	}

	macidsDef = &msnIteUser->second.def;
	if(macidsDef->find(macid) == macidsDef->end()) {
		return false;
	}

	macidsSub = &msnIteUser->second.sub;
	if(macidsSub->erase(macid) == 0) {
		return false;
	}

	msnIteMacid = _macid2user.find(macid);
	if(msnIteMacid == _macid2user.end()) {
		return false;
	}

	if(msnIteMacid->second.sub.erase(user) == 0) {
		return false;
	}

	return true;
}

//覆盖原来的订阅，如果取消所有订阅，则新订阅指令中数量为0
bool Subscribe::update(const string &user, const set<string> &macids)
{
	map<string, NODE >::iterator msnIteUser;
	map<string, NODE >::iterator msnIteMacid;

	set<string> *macidsSub;
	set<string> *macidsDef;
	set<string>::iterator ssIte;

	share::Guard guard(_mutex);

	msnIteUser = _user2macid.find(user);
	if(msnIteUser == _user2macid.end()) {
		return false;
	}

	macidsSub = &msnIteUser->second.sub;
	for(ssIte = macidsSub->begin(); ssIte != macidsSub->end(); ++ssIte) {
		msnIteMacid = _macid2user.find(*ssIte);
		if(msnIteMacid == _macid2user.end()) {
			continue;
		}

		msnIteMacid->second.sub.erase(user);
	}
	msnIteUser->second.sub.clear();

	macidsDef = &msnIteUser->second.def;
	for(ssIte = macids.begin(); ssIte != macids.end(); ++ssIte) {
		if(macidsDef->find(*ssIte) == macidsDef->end()) {
			continue;
		}

		msnIteMacid = _macid2user.find(*ssIte);
		if(msnIteMacid == _macid2user.end()) {
			continue;
		}

		msnIteUser->second.sub.insert(*ssIte);
		msnIteMacid->second.sub.insert(user);
	}

	return true;
}

//获取所有关联用户
bool Subscribe::getDefUser(const string &macid, set<string> &users)
{
	map<string, NODE >::iterator msnIteMacid;

	share::Guard guard(_mutex);

	msnIteMacid = _macid2user.find(macid);
	if(msnIteMacid == _macid2user.end()) {
		return true;
	}

	users = msnIteMacid->second.def;

	return true;
}

//获取所有订阅用户
bool Subscribe::getSubUser(const string &macid, set<string> &users)
{
	map<string, NODE >::iterator msnIteMacid;

	share::Guard guard(_mutex);

	msnIteMacid = _macid2user.find(macid);
	if(msnIteMacid == _macid2user.end()) {
		return true;
	}

	users = msnIteMacid->second.sub;

	return true;
}
