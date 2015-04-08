#include "udpclient.h"

#include "../tools/utils.h"

UdpClient::UdpClient(ISystemEnv *pEnv) {
	_pEnv = pEnv;
	_thread_num = 1;
	_max_timeout = 180;
}

UdpClient::~UdpClient(void) {
	Stop();
}

bool UdpClient::Init() {
	int nvalue = 0;
	if (_pEnv->GetInteger("udp_cli_thread", nvalue)) {
		_thread_num = nvalue;
	}

	// 取得连接空闲时间
	if (_pEnv->GetInteger("keepalive_time", nvalue)) {
		_max_timeout = nvalue;
	}

	setpackspliter(&_spliter);

	return true;
}

void UdpClient::Stop(void) {
	StopClient();
}

bool UdpClient::Start(void) {
	return StartUDP(_client_user._ip.c_str(), _client_user._port, _thread_num);
}

void UdpClient::on_data_arrived(socket_t *sock, const void* data, int len) {
	const char *ip = sock->_szIp;
	unsigned int port = sock->_port;
	OUT_HEX(ip, port, "RECV", (char* )data, len);

	User user = _online_user.GetUserBySocket(sock);
	if (user._user_id.empty() || user._ext_ptr == NULL) {
		return;
	}

	user._last_active_time = time(NULL);
	_online_user.SetUser(user._user_id, user);

	IUdpServer *svr = (IUdpServer*) user._ext_ptr;
	if (!svr->HandleData(user._user_id, data, len)) {
		OUT_INFO(sock->_szIp, sock->_port, "SEND", "%s, source connect invalid", user._user_id.c_str());

		_online_user.DeleteUser(user._user_id);
		CloseSocket(sock);
	}
}

void UdpClient::on_dis_connection(socket_t *sock) {
	OUT_INFO(sock->_szIp, sock->_port, "dis", "Recv disconnect fd %d", sock->_fd);
}

void UdpClient::TimeWork() {
}

void UdpClient::NoopWork() {
}

bool UdpClient::ConnectServer(User &user, unsigned int timeout) {
	bool ret = false;

	if (user._fd != NULL) {
		CloseSocket((socket_t*) user._fd);
		user._fd = NULL;
	}

	user._fd = _udp_handle.connect_nonb(user._ip.c_str(), user._port, timeout);
	ret = (user._fd) ? true : false;

	if (ret) {
		user._user_state = User::ON_LINE;
	} else {
		user._user_state = User::OFF_LINE;
	}

	user._last_active_time = time(0);
	user._login_time = time(0);
	user._connect_info.last_reconnect_time = time(0);

	return ret;
}

bool UdpClient::HandleData(const string &userid, const void *data, int len) {
	User user = _online_user.GetUserByUserId(userid);
	if (user._user_id.empty()) {
		OUT_WARNING(NULL, 0, "HandleData", "get user %s fail", userid.c_str());
		return false;
	}

	const char *ip = user._ip.c_str();
	unsigned short port = user._port;

	if (user._fd == NULL || user._user_state != User::ON_LINE) {
		return false;
	}

	if( ! SendData(user._fd, (char*)data, len)) {
		return false;
	}

	OUT_HEX( ip, port, "SEND", (char*)data, len );

	user._last_active_time = time(NULL);
	_online_user.SetUser(user._user_id, user);

	return true;
}

void UdpClient::HandleOfflineUsers()
{
}

bool UdpClient::AddChannel(IUdpServer *svr, const string &userid, const char *ip, int port)
{
	User user;

	user._user_id = userid;
	user._fd = NULL;
	user._ip = ip;
	user._port = port;
	user._user_state = User::OFF_LINE;
	user._ext_ptr = svr;
	user._last_active_time = time(NULL);

	if (! ConnectServer(user, 3)) {
		return false;
	}

	if( ! _online_user.AddUser(userid, user)) {
		return false;
	}

	return true;
}

bool UdpClient::DelChannel(const string &userid)
{
	User user = _online_user.GetUserByUserId(userid);
	if (user._user_id.empty()) {
		OUT_WARNING(NULL, 0, "DelChannel", "get user %s fail", userid.c_str());
		return false;
	}

	_online_user.DeleteUser(user._user_id);
	CloseSocket(user._fd);
	user._fd = NULL;

	return true;
}
