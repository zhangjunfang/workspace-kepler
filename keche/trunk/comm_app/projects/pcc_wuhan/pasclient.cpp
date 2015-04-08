#include "pasclient.h"
#include <Base64.h>
#include <comlog.h>
#include <tools.h>
#include "../share/md5/md5.h"
#include "../tools/utils.h"

#include <time.h>

PasClient::PasClient() {
	_threadnum = 1;
}

PasClient::~PasClient() {
	Stop();
}

bool PasClient::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	int valInt;
	// msg客户端线程数
	if(_pEnv->GetInteger("msg_thread_num", valInt)) {
		_threadnum = valInt;
	}

	char valBuf[1024] = { 0 };
	if (!_pEnv->GetString("pas_connect_ip", valBuf)) {
		OUT_ERROR("PasClient", 0, "INIT", "pas_connect_ip is empty");
		return false;
	}
	_client_user._ip = valBuf;

	if (!_pEnv->GetInteger("pas_connect_port", valInt)) {
		OUT_ERROR("PasClient", 0, "INIT", "pas_connect_port is empty");
		return false;
	}
	_client_user._port = valInt;

	if (!_pEnv->GetString("pas_user_name", valBuf)) {
		OUT_ERROR("PasClient", 0, "INIT", "pas_user_name is empty");
		return false;
	}
	_client_user._user_name = valBuf;

	if (!_pEnv->GetString("pas_user_pwd", valBuf)) {
		OUT_ERROR("PasClient", 0, "INIT", "pas_user_pwd is empty");
		return false;
	}
	_client_user._user_pwd = valBuf;

	_client_user._last_active_time = 0;
	_client_user._connect_info.timeval = 0;

	// 设置分包对象对数据进行分包处理
	setpackspliter( &_packspliter ) ;

	// 加载车辆静态信息
	return true ;
}

void PasClient::Stop( void )
{
	StopClient() ;
}

bool PasClient::Start( void )
{
	return StartClient( _client_user._ip.c_str() , _client_user._port , _threadnum ) ;
}

void PasClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
	vector<string> fields;
	const char *ptr = (const char*)data;

	OUT_RECV(sock->_szIp, sock->_port, "RECV", "%.*s", len, ptr);

	if(len < 2) return;

	string req = string(ptr + 1, len -2);

	fields.clear();
	if(Utils::splitStr(req, fields, ',') == 0) {
		return;
	}

	//更新活跃时间
	_client_user._last_active_time = time(NULL);

	if(fields[0] == "TESTLINK") {
		string rsp = "(TESTLINKRESP)";
		OUT_SEND(sock->_szIp, sock->_port, "SEND", "%.*s", rsp.length(), rsp.c_str());
		SendData(sock, rsp.c_str(), rsp.length() + 1);
	} else if(fields[0] == "BINDRESP" && fields.size() == 2) {
		if(atoi(fields[1].c_str()) == 1) {
			OUT_INFO(sock->_szIp, sock->_port, "STAT", "login success");
			_client_user._user_state = User::ON_LINE;
		} else {
			OUT_INFO(sock->_szIp, sock->_port, "STAT", "login failure");
			CloseSocket(sock);
		}
	} else if(fields[0] == "CONTROL" && fields.size() >= 4) {
		_pEnv->GetMsgClient()->HandleData(req.c_str(), req.length());
	}
}

void PasClient::on_dis_connection( socket_t *sock )
{
	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	OUT_INFO( sock->_szIp, sock->_port, "Conn", "Recv disconnect fd %d", sock->_fd ) ;

	_client_user._fd = NULL;
	_client_user._user_state = User::OFF_LINE ;
	_client_user._login_time = 0;
}

void PasClient::TimeWork()
 {
	while (Check()) {
		time_t tv = time(NULL);
		User &user = _client_user;

		if(user._fd == NULL || user._user_state != User::ON_LINE || tv - user._last_active_time > 180) {
			ConnectServer(_client_user, 30);
		}

		sleep(10);
	}
}

void PasClient::NoopWork()
{
}

int PasClient::build_login_msg(User &user, char *buf, int len)
{
	int akeylen;
	char akeybuf[BUFSIZ + 1];
	const int md5len = 32;
	char md5buf[md5len + 1] = "";
	const int varlen = 15;
	char varbuf[varlen + 1] = "";

	//首先计算密码md5值
	md5_hex(_client_user._user_pwd.c_str(), _client_user._user_pwd.length(), md5buf);

	//获取变化的当前时间
	time_t tvInt = time(NULL);
	struct tm *tmPtr = localtime(&tvInt);
	snprintf(varbuf, varlen, "%04d%02d%02d%02d%02d%02d", tmPtr->tm_year + 1900,
			tmPtr->tm_mon + 1, tmPtr->tm_mday, tmPtr->tm_hour, tmPtr->tm_min, tmPtr->tm_sec);

	akeylen = snprintf(akeybuf, BUFSIZ, "%s%s", md5buf, varbuf);

	//首先md5加密密码
	md5_hex(akeybuf, akeylen, md5buf);

	const char *username = _client_user._user_name.c_str();
	return snprintf(buf, len, "(BIND,%s,%s,%s)", username, varbuf, md5buf) + 1;
}

bool PasClient::HandleData( const char *data, int len )
{
	User &user = _client_user;

	if(data == NULL && len == 0) { //用于判断链接状态
		if (user._user_state != User::ON_LINE || user._fd == NULL) {
			return false;
		}

		return true;
	}

	// 发送数据到监管平台,这里是通过数据通道来发送数据
	if (user._user_state != User::ON_LINE || user._fd == NULL) {
		OUT_WARNING( user._ip.c_str() , user._port , "FAIL", "%.*s" , len, data);
		return false;
	}

	if (!SendData(user._fd, data, len)) {
		OUT_WARNING( user._ip.c_str() , user._port , "FAIL", "%.*s" , len, data);
		return false;
	}

	OUT_SEND( user._ip.c_str() , user._port , "SEND", "%.*s" , len, data);
	return true;
}

struct packet * PasClient::CPackSpliter::get_kfifo_packet( DataBuffer *fifo )
{
	const size_t MIN_LEN = 3; //()\0
	unsigned int len = fifo->getLength() ;
	if ( len < MIN_LEN || len > MAX_PACK_LEN ){
		fifo->resetBuf() ;
		return NULL;
	}

	struct packet *item = NULL;
	struct list_head *packet_list_ptr = NULL;

	size_t i;
	size_t n;
	char *ptr;
	char *tagb;
	char *tage;

	n = 0;
	tagb = tage = NULL;
	ptr = fifo->getBuffer() ;
	for (i = 0; i < len; ++i) {
		switch(ptr[i]) {
		case '(':
			tagb = ptr + i;
			break;
		case ')':
			tage = ptr + i;
			break;
		}

		if(ptr[i] != 0 || tagb == NULL || tage == NULL || tage <= tagb) {
			continue;
		}
		n = i + 1;

		item = (struct packet *) malloc(sizeof(struct packet));
		if (item == NULL) break;

		size_t pack_len = tage - tagb + 1;
		item->data = (unsigned char *) malloc(pack_len);
		memcpy(item->data, tagb, pack_len);
		item->len = pack_len;
		item->type = E_PROTO_OUT;

		if (packet_list_ptr == NULL) {
			packet_list_ptr = (struct list_head *) malloc(sizeof(struct list_head));
			if (packet_list_ptr == NULL) {
				free(item->data);
				free(item);
				return NULL;
			}

			INIT_LIST_HEAD(packet_list_ptr);
		}
		list_add_tail(&item->list, packet_list_ptr);
	}

	if (n > 0) fifo->removePos(n);

	return (struct packet*) packet_list_ptr;
}

