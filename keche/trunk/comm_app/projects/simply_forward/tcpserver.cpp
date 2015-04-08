#include "tcpserver.h"

#include "../tools/utils.h"

#include <list>
using std::list;

TcpServer::TcpServer(ISystemEnv *pEnv) {
	_pEnv = pEnv;
	_thread_num = 1;
	_max_timeout = 180;
}

TcpServer::~TcpServer() {
	Stop();
}

bool TcpServer::Init(int port, const vector<string> &addrs) {
	_listen_port = port;
	_listen_ip = "0.0.0.0";
	_dstAddrs = addrs;

	int nvalue = 0;
	if (_pEnv->GetInteger("tcp_svr_thread", nvalue)) {
		_thread_num = nvalue;
	}

	// 取得连接空闲时间
	if (_pEnv->GetInteger("keepalive_time", nvalue)) {
		_max_timeout = nvalue;
	}

	setpackspliter(&_spliter);

	return true;
}

bool TcpServer::Start() {
	return StartServer(_listen_port, _listen_ip.c_str(), _thread_num, _max_timeout);
}

void TcpServer::Stop() {
	StopServer();
}

void TcpServer::TimeWork() {
	while (Check()) {
		HandleOfflineUsers();

		sleep(30);
	}
}

void TcpServer::NoopWork() {
}

void TcpServer::on_data_arrived(socket_t *sock, const void *data, int len)
{
	const char *ip    = sock->_szIp;
	unsigned int port = sock->_port;

	OUT_HEX( ip, port, "RECV", (char*)data, len ) ;

	User user = _online_user.GetUserBySocket(sock);
	if(user._user_id.empty()) {
		return;
	}
	user._last_active_time = time(NULL);
	_online_user.SetUser(user._user_id, user);

	if (user._ext_ptr != NULL) {
		vector<string>::iterator it;
		vector<string> *userids = (vector<string>*) user._ext_ptr;
		for (it = userids->begin(); it != userids->end(); ++it) {
			_pEnv->GetTcpClient()->HandleData(*it, data, len);
		}
	}
}

void TcpServer::on_new_connection(socket_t *sock, const char* ip, int port)
{
	char srcAddr[1024];
	string userid;

	vector<string>::iterator it;
	vector<string> fields;

	string dstAddr;
	int dstPort;

	OUT_INFO( sock->_szIp, sock->_port, "new", "Recv connect fd %d", sock->_fd ) ;

	if(ip == NULL || port <= 0) {
		return;
	}

	vector<string> *userids = new vector<string>;

	snprintf(srcAddr, 1024, "%lu:%s:%d", time(NULL), ip, port);
	for(it = _dstAddrs.begin(); it != _dstAddrs.end(); ++it) {
		fields.clear();
		if(Utils::splitStr(*it, fields, ':') != 2) {
			continue;
		}
		dstAddr = fields[0];
		dstPort = atoi(fields[1].c_str());

		userid = srcAddr + string("_") + *it;
		userids->push_back(userid);

		_pEnv->GetTcpClient()->AddChannel(this, userid, dstAddr.c_str(), dstPort);
	}

	User user;

	user._user_id = srcAddr;
	user._fd = sock;
	user._ip = ip;
	user._port = port;
	user._ext_ptr = userids;
	user._user_state = User::ON_LINE;
	user._last_active_time = time(NULL);
	_online_user.AddUser(user._user_id, user);
}

void TcpServer::on_dis_connection(socket_t *sock)
{
	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	OUT_INFO( sock->_szIp, sock->_port, "dis", "Recv disconnect fd %d", sock->_fd ) ;

	User user = _online_user.GetUserBySocket(sock);
	if(user._user_id.empty()) {
		OUT_WARNING(sock->_szIp, sock->_port, "dis", "get user fail, fd %d", sock->_fd);
		return;
	}

	//不直接而延后删除，防止使用user._ext_ptr野指针
	user._fd = NULL;
	user._user_state = User::OFF_LINE;
	_online_user.SetUser(user._user_id, user);
}

bool TcpServer::HandleData(const string &userid, const void *data, int len)
{
	string svrUserid;
	size_t pos;

	if((pos = userid.find('_')) == string::npos) {
		return false;
	}

	svrUserid = userid.substr(0, pos);
	User user = _online_user.GetUserByUserId(svrUserid);
	if(user._user_id.empty() || user._fd == NULL || user._user_state != User::ON_LINE) {
		return false;
	}

	if( ! SendData(user._fd, (char*)data, len)) {
		return false;
	}

	const char *ip = user._ip.c_str();
	unsigned short port = user._port;
	OUT_HEX( ip, port, "SEND", (char*)data, len ) ;

	return true ;
}

void TcpServer::HandleOfflineUsers() {
	vector<User> users = _online_user.GetOfflineUsers(_max_timeout);

	for (int i = 0; i < (int) users.size(); i++) {
		User &user = users[i];
		const char *ip = user._ip.c_str();
		unsigned short port = user._port;

		if (user._fd != NULL) {
			CloseSocket(user._fd);
		}

		if (user._ext_ptr != NULL) {
			vector<string>::iterator it;
			vector<string> *userids = (vector<string>*) user._ext_ptr;

			for (it = userids->begin(); it != userids->end(); ++it) {
				_pEnv->GetTcpClient()->DelChannel(*it);
			}

			if (time(NULL) - user._last_active_time < 1) {
				usleep(1000);
			}
			delete (userids);
		}

		OUT_INFO(ip, port, "STAT", "%s timeout, close it", user._user_id.c_str());
	}
}

bool TcpServer::ChkChannel(const string &userid) {
	string svrUserid;
	size_t pos;

	if ((pos = userid.find('_')) == string::npos) {
		return false;
	}

	svrUserid = userid.substr(0, pos);
	User user = _online_user.GetUserByUserId(svrUserid);
	if (user._user_id.empty() || user._fd == NULL || user._user_state != User::ON_LINE) {
		return false;
	}

	return true;
}
