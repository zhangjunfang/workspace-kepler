/*
 * Author     : yuanchunqing
 * Email      : yuanchunqing@ctfo.com
 * Date       : 2012-5-3
 * Description: 会话管理，在线用户与注册终端映射查找
 *
 */

#ifndef __SUBSCRIBE_H__
#define __SUBSCRIBE_H__ 1

#include "Mutex.h"

#include <string>
#include <map>
#include <set>

using std::string;
using std::map;
using std::set;

struct NODE {
	set<string> def;
	set<string> sub;
};

class Subscribe {
	share::Mutex _mutex;
	map<string, NODE > _user2macid;      //stl容器默认分配器使用堆空间
	map<string, NODE > _macid2user;      //stl容器默认分配器使用堆空间
public:
	Subscribe();
	~Subscribe();

	//企业平台管理员登陆时，默认订阅权限组所有终端
	bool regUser(const string &user, set<string> &macids);

	//企业平台管理员退出时，取消所有订阅
	bool unregUser(const string &user);

	//在线管理员不定时设置订阅，累加之前订阅
	bool apped(const string &user, const string &macid);

	//删除单个终端订阅
	bool erase(const string &user, const string &macid);

	//在线管理员不定时设置订阅，覆盖之前订阅
	bool update(const string &user, const set<string> &macids);

	//收到车载终端紧急消息时，查找关联所有用户
	bool getDefUser(const string &macid, set<string> &users);

	//收到车载终端一般消息时，查找订阅用户
	bool getSubUser(const string &macid, set<string> &users);
};

#endif//__SUBSCRIBE_H__
