#include "tcpclient.h"

#include "../tools/utils.h"

TcpClient::TcpClient(ISystemEnv *pEnv) {
	_pEnv = pEnv;
	_thread_num = 1;
	_max_timeout = 180;
}

TcpClient::~TcpClient(void) {
	Stop();
}

bool TcpClient::Init() {
	int nvalue = 0;
	if (_pEnv->GetInteger("tcp_cli_thread", nvalue)) {
		_thread_num = nvalue;
	}

	// 取得连接空闲时间
	if (_pEnv->GetInteger("keepalive_time", nvalue)) {
		_max_timeout = nvalue;
	}

	setpackspliter(&_spliter);

	return true;
}

void TcpClient::Stop(void) {
	StopClient();
}

bool TcpClient::Start(void) {
	return StartClient(_client_user._ip.c_str(), _client_user._port, _thread_num);
}

void TcpClient::on_data_arrived(socket_t *sock, const void* data, int len) {
	const char *ip = sock->_szIp;
	unsigned int port = sock->_port;
	OUT_HEX(ip, port, "RECV", (char* )data, len);

	User user = _online_user.GetUserBySocket(sock);
	if (user._user_id.empty() || user._ext_ptr == NULL) {
		return;
	}

	user._last_active_time = time(NULL);
	_online_user.SetUser(user._user_id, user);

	ITcpServer *svr = (ITcpServer*) user._ext_ptr;
	if (!svr->HandleData(user._user_id, data, len)) {
		OUT_INFO(sock->_szIp, sock->_port, "SEND", "%s, source connect invalid", user._user_id.c_str());

		_online_user.DeleteUser(user._user_id);
		CloseSocket(sock);
	}
}

void TcpClient::on_dis_connection(socket_t *sock) {
	OUT_INFO( sock->_szIp, sock->_port, "dis", "Recv disconnect fd %d", sock->_fd );

	User user = _online_user.GetUserBySocket(sock);
	if (user._user_id.empty()) {
		OUT_WARNING(sock->_szIp, sock->_port, "dis", "get user fail, fd %d", sock->_fd);
		return;
	}

	user._fd = NULL;
	user._user_state = User::OFF_LINE;
	_online_user.SetUser(user._user_id, user);
}

void TcpClient::TimeWork() {
	while (Check()) {
		HandleOfflineUsers();

		sleep(3);
	}
}

void TcpClient::NoopWork() {
}

bool TcpClient::ConnectServer(User &user, unsigned int timeout) {
	bool ret = false;

	if (user._fd != NULL) {
		CloseSocket((socket_t*) user._fd);
		user._fd = NULL;
	}

	user._fd = _tcp_handle.connect_nonb(user._ip.c_str(), user._port, timeout);
	ret = (user._fd) ? true : false;

	if (ret) {
		user._user_state = User::ON_LINE;
	} else {
		user._user_state = User::OFF_LINE;
	}

	time_t tv = time(NULL);
	user._last_active_time = tv;
	user._login_time = tv;
	user._connect_info.last_reconnect_time = tv;

	return ret;
}

bool TcpClient::HandleData(const string &userid, const void *data, int len) {
	User user = _online_user.GetUserByUserId(userid);
	if (user._user_id.empty()) {
		OUT_WARNING(NULL, 0, "HandleData", "get user %s fail", userid.c_str());
		return false;
	}

	const char *ip = user._ip.c_str();
	unsigned short port = user._port;

	if (user._fd == NULL || user._user_state != User::ON_LINE) {
		share::Guard guard(_mutex);

		map<string, vector<char> >::iterator it = _cache.find(user._user_id);
		if (it != _cache.end()) {
			if (it->second.size() > 10240) {
				return false;
			}

			it->second.insert(it->second.end(), (char*) data, ((char*) data) + len);
		} else {
			vector<char> cache;
			cache.assign((char*) data, ((char*) data) + len);
			_cache.insert(make_pair(user._user_id, cache));
		}

		return true;
	}

	if (!SendData(user._fd, (char*) data, len)) {
		share::Guard guard(_mutex);
		map<string, vector<char> >::iterator it = _cache.find(user._user_id);
		if (it != _cache.end()) {
			if (it->second.size() > 10240) {
				return false;
			}

			it->second.insert(it->second.end(), (char*) data, ((char*) data) + len);
		} else {
			vector<char> cache;
			cache.assign((char*) data, ((char*) data) + len);
			_cache.insert(make_pair(user._user_id, cache));
		}

		return true;
	}

	OUT_HEX( ip, port, "SEND", (char*)data, len );

	user._last_active_time = time(NULL);
	_online_user.SetUser(user._user_id, user);

	return true;
}

void TcpClient::HandleOfflineUsers()
{
	vector<User> users = _online_user.GetOfflineUsers(_max_timeout);

	for(int i = 0; i < (int)users.size(); i++) {
		User &user = users[i];
		const char *ip = user._ip.c_str();
		unsigned short port = user._port;

		ITcpServer *svr = (ITcpServer*)user._ext_ptr;
		if( ! svr->ChkChannel(user._user_id)) {
			continue;
		}

		if(ConnectServer(user, 3)) {
			share::Guard guard(_mutex);

			map<string, vector<char> >::iterator it = _cache.find(user._user_id);
			if(it != _cache.end()) {
				SendData(user._fd, &it->second[0], it->second.size());
				OUT_HEX(ip, port, "SEND", &it->second[0], it->second.size());

				_cache.erase(it);
			}
		}

		_online_user.AddUser(user._user_id, user);
	}
}

bool TcpClient::AddChannel(ITcpServer *svr, const string &userid, const char *ip, int port)
{
	User user;

	user._user_id = userid;
	user._fd = NULL;
	user._ip = ip;
	user._port = port;
	user._user_state = User::OFF_LINE;
	user._ext_ptr = svr;
	user._last_active_time = time(NULL);

	if( ! _online_user.AddUser(userid, user)) {
		return false;
	}

	return true;
}

bool TcpClient::DelChannel(const string &userid)
{
	_mutex.lock();
	map<string, vector<char> >::iterator it = _cache.find(userid);
	if (it != _cache.end()) {
		_cache.erase(it);
	}
	_mutex.unlock();

	User user = _online_user.GetUserByUserId(userid);
	if (user._user_id.empty()) {
		OUT_WARNING(NULL, 0, "DelChannel", "get user %s fail", userid.c_str());
		return false;
	}

	_online_user.DeleteUser(user._user_id);
	CloseSocket(user._fd);

	return true;
}
