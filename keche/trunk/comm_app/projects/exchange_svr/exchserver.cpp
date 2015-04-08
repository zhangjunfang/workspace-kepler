/*
 * exchserver.cpp
 *
 *  Created on: 2014-5-23
 *      Author: ycq
 *  消息转发服务
 */

#include "exchprot.h"
#include "exchserver.h"
#include "../share/md5/md5.h"
#include <Mutex.h>
#include <comlog.h>
#include <tools.h>
#include <netutil.h>
#include <arpa/inet.h>

#include <map>
using std::map;
#include <vector>
using std::vector;
#include <string>
using std::string;

#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <unistd.h>

struct Subscribe {
	uint8_t flag1;
	uint8_t flag2;
	set<uint32_t> set1;
	set<uint16_t> set2;
	share::RWMutex mutex;

	Subscribe() {
		flag1 = 0;
		flag2 = 0;
		set1.clear();
		set2.clear();
	}
};

ExchServer::ExchServer()
{
	_pEnv = NULL;
	_thread_num = 1;
}

ExchServer::~ExchServer(void)
{
	Stop();
}

bool ExchServer::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv;

	char szip[128] = { 0 };
	if (!pEnv->GetString("exch_listen_ip", szip)) {
		OUT_ERROR( NULL, 0, NULL, "get photo3g_listen_ip failed");
		return false;
	}
	_listen_ip = szip;

	int port = 0;
	if (!pEnv->GetInteger("exch_listen_port", port)) {
		OUT_ERROR( NULL, 0, NULL, "get photo3g_listen_port failed");
		return false;
	}
	_listen_port = port;

	int nvalue = 8;
	if (pEnv->GetInteger("exch_tcp_thread", nvalue)) {
		_thread_num = nvalue;
	}

	// 设置数据分包对象
	_tcp_handle.setpackspliter(&_pack_spliter);

	return true;
}

bool ExchServer::Start(void)
{
	return StartServer(_listen_port, _listen_ip.c_str(), _thread_num);
}

// 重载STOP方法
void ExchServer::Stop(void) {
	StopServer();
}

void ExchServer::TimeWork() {
	vector<User> users;
	vector<User>::iterator ite;

	const char *ip;
	unsigned short port;

	while (Check()) {
		users = _online_user.GetOfflineUsers(180);

		for ( ite = users.begin(); ite != users.end(); ++ ite ) {
			ip = ite->_ip.c_str();
			port = ite->_port;
			CloseSocket( ite->_fd );
			OUT_INFO( ip, port, ite->_user_name.c_str(), "timeout and close");
		}

		sleep(10);
	}
}

void ExchServer::NoopWork() {
	OUT_INFO( NULL, 0, NULL, "void ExchServer::NoopWork()");

	while (Check()) {
		sleep(10);
	}
}

void ExchServer::on_data_arrived( socket_t *sock, const void *data, int len)
{
	const char *ip = sock->_szIp;
	unsigned short port = sock->_port;
	OUT_HEX(ip, port, "RECV", (char*)data, len);

	size_t msglen;
	uint8_t *msgptr;
	vector<uint8_t> msgbuf;

	msgbuf.resize(len);
	msgptr = &msgbuf[0];
	msglen = deCode((uint8_t*)data, len, &msgbuf[0]);

	if(msglen < sizeof(ExchHead) + sizeof(ExchTail)) {
		return;
	}

	ExchHead *headPtr;     //消息头部
	ExchPlatHead *platPtr; //平台消息
	uint32_t srcid; //消息来源
	uint32_t dstid; //消息目的地
	uint16_t msgid; //消息主类型
	uint16_t cmdid; //消息子类型
	uint32_t seqid; //消息序列号
	uint32_t length; //消息体长度

	uint8_t rspval;
	vector<uint8_t> rspbuf; //应答消息缓存

	headPtr = (ExchHead*)msgptr;
	srcid = ntohl(headPtr->srcid);
	dstid = ntohl(headPtr->dstid);
	msgid = ntohs(headPtr->msgid);
	seqid = ntohl(headPtr->seqid);
	length = ntohl(headPtr->length);

	if(msgid == 0x1100) {
		platPtr = (ExchPlatHead*)(msgptr + sizeof(ExchHead));
		cmdid = ntohs(platPtr->msgid);
		if(cmdid == 0x1101) {
			string strVal;
			char md5buf[64] = "";

			IRedisCache *redis = _pEnv->GetRedisCache();
			ExchLogin *login = (ExchLogin*)(msgptr + sizeof(ExchHead) + sizeof(ExchPlatHead));

			string username = string(login->username, strnlen(login->username, 16));
			string password = string(login->password, strnlen(login->password, 32));

			rspval = 1;
			strVal = "";
			if (redis->HGet("KCTX.EXCHSVR.PWD", username.c_str(), strVal) && !strVal.empty()) {
				md5_hex(strVal.c_str(), strVal.length(), md5buf);
				if(strcasecmp(password.c_str(), md5buf) == 0) {
					rspval = 0;
				}
			}

			_pEnv->GetProtParse()->buildExchGenReply(seqid, srcid, rspval, rspbuf);
			SendData(sock, (char*)&rspbuf[0], rspbuf.size());
			OUT_HEX(ip, port, "SEND", (char*)&rspbuf[0], rspbuf.size());

			if(rspval != 0) {
				CloseSocket(sock);
				return;
			}

			User user;
			user._user_id = username;
			user._user_name = username;
			user._user_pwd = password;
			user._access_code = srcid;
			user._fd = sock;
			user._ip = ip;
			user._port = port;
			user._user_state = User::ON_LINE;
			user._last_active_time = time(NULL);
			user._ext_ptr = new Subscribe;
			_online_user.AddUser(user._user_id, user);

			return;
		}

		User user = _online_user.GetUserBySocket(sock);
		if (user._user_id.empty() || user._ext_ptr == NULL) {
			return;
		}
		user._last_active_time = time(NULL);
		_online_user.SetUser(user._user_id, user);

		rspval = 1;
		if (cmdid == 0x1102) {
			rspval = 0;
		} else if (cmdid == 0x1103) {
			size_t pos = sizeof(ExchHead) + sizeof(ExchPlatHead);
			ExchSubscribeCenter *center = (ExchSubscribeCenter*)(msgptr +pos);
			Subscribe *sub = (Subscribe*)user._ext_ptr;
			share::RWGuard guard(sub->mutex, true);
			if (center->count == 0) {
				if(center->type == 0) {
					sub->flag1 = 0;
				} else {
					sub->flag1 = 1;
				}
				sub->set1.clear();
			} else {
				size_t i;
				if(center->type == 0) {
					for(i = 0; i < center->count; ++i) {
						sub->set1.erase(ntohl(center->ids[i]));
					}
				} else if(center->type == 1) {
					sub->set1.clear();
					for (i = 0; i < center->count; ++i) {
						sub->set1.insert(ntohl(center->ids[i]));
					}
				} else if(center->type == 2) {
					for (i = 0; i < center->count; ++i) {
						sub->set1.insert(ntohl(center->ids[i]));
					}
				}
			}
			rspval = 0;
		} else if (cmdid == 0x1104) {
			size_t pos = sizeof(ExchHead) + sizeof(ExchPlatHead);
			ExchSubscribeCommand *command = (ExchSubscribeCommand*)(msgptr +pos);
			Subscribe *sub = (Subscribe*)user._ext_ptr;
			share::RWGuard guard(sub->mutex, true);
			if (command->count == 0) {
				if (command->type == 0) {
					sub->flag2 = 0;
				} else {
					sub->flag2 = 1;
				}
				sub->set2.clear();
			} else {
				size_t i;
				if (command->type == 0) {
					for (i = 0; i < command->count; ++i) {
						sub->set2.erase(ntohs(command->ids[i]));
					}
				} else if (command->type == 1) {
					sub->set1.clear();
					for (i = 0; i < command->count; ++i) {
						sub->set2.insert(ntohs(command->ids[i]));
					}
				} else if (command->type == 2) {
					for (i = 0; i < command->count; ++i) {
						sub->set2.insert(ntohs(command->ids[i]));
					}
				}
			}
			rspval = 0;
		}

		_pEnv->GetProtParse()->buildExchGenReply(seqid, srcid, rspval, rspbuf);
		SendData(sock, (char*) &rspbuf[0], rspbuf.size());
		return;
	}

	User user = _online_user.GetUserBySocket(sock);
	if(user._user_id.empty()) {
		return;
	}
	user._last_active_time = time(NULL);
	_online_user.SetUser(user._user_id, user);

	if (msgid == 0x1001 || msgid == 0xc001) {
		//如果是应答cmdid赋0
		cmdid = 0;
	} else {
		cmdid = *(uint16_t*) (msgptr + sizeof(ExchHead));
		cmdid = ntohs(cmdid);
	}

	int i;
	vector<User> users = _online_user.GetOnlineUsers();
	for(i = 0; i < users.size(); ++i) {
		if(users[i]._fd == sock || users[i]._ext_ptr == NULL) {
			continue;
		}

		//如果是应答直接转发，不检查订阅
		if(cmdid == 0 && dstid == users[i]._access_code) {
			SendData(users[i]._fd, (char*)data, len);
			continue;
		}

		Subscribe *sub = (Subscribe*)users[i]._ext_ptr;
		share::RWGuard guard(sub->mutex, false);

		if (sub->flag1 == 0 && sub->set1.find(dstid) == sub->set1.end()) {
			continue;
		}

		if (sub->flag2 == 0 && sub->set2.find(cmdid) == sub->set2.end()) {
			continue;
		}

		SendData(users[i]._fd, (char*)data, len);
	}
}

void ExchServer::on_new_connection( socket_t *sock, const char* ip, int port)
{
}

void ExchServer::on_dis_connection( socket_t *sock )
{
	OUT_INFO(sock->_szIp, sock->_port, "CONN", "fd %d disconnect", sock->_fd);

	User user = _online_user.DeleteUser( sock );
	if(user._ext_ptr != NULL) {
		Subscribe *sub = (Subscribe*)user._ext_ptr;
		delete sub;
	}
}

// 从MSG转发过来的数据
void ExchServer::HandleData(const char *data, int len)
{
}

size_t ExchServer::enCode(const uint8_t *src, size_t srclen, uint8_t *dst)
{
	return 0;
}
size_t ExchServer::deCode(const uint8_t *src, size_t srclen, uint8_t *dst)
{
	size_t dstlen;

	dst[0] = 0x5b;
	dstlen = _pEnv->GetProtParse()->deCode(src + 1, srclen - 2, dst + 1) + 2;
	dst[dstlen - 1] = 0x5d;

	return dstlen;
}
