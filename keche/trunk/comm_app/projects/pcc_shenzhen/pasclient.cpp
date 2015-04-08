#include "pasclient.h"
#include <Base64.h>
#include <comlog.h>
#include <tools.h>
#include "../share/md5/md5.h"
#include "../tools/utils.h"
#include "urlcoding.h"

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

	const char *ptr = (const char*)data;

	OUT_RECV(sock->_szIp, sock->_port, "RECV", "%.*s", len, ptr);

	if(len < 1) return;

	string req = string(ptr, len);

	vector<string> fields;
	Utils::splitStr(req, fields, ',');

	if(fields[0] == "1" && fields.size() == 2) {
		switch(atoi(fields[1].c_str())) {
		case 1:
			_client_user._user_state = User::ON_LINE;
			_client_user._last_active_time = time(NULL);
			OUT_INFO(sock->_szIp, sock->_port, "STAT", "login success");
			break;
		default:
			CloseSocket(sock);
			_client_user._fd = NULL;
			OUT_INFO(sock->_szIp, sock->_port, "STAT", "login failure");
			break;
		}

		return;
	}

	if(_client_user._fd == NULL || _client_user._user_state != User::ON_LINE) {
		return;
	}

	if(fields[0] == "4" && fields.size() == 2) {
		if(fields[1].compare(0, 5, "14411") != 0 || fields[1].length() < 9) {
			return;
		}

		string keystr = fields[1].substr(9);
		string valstr = "";

		vector<string> args;
		_pEnv->GetRedisCache()->HGet("KCTX.SZINFO", keystr.c_str(), valstr);
		if(valstr.empty() || Utils::splitStr(valstr, args, ',') != 5) {
			OUT_WARNING("", 0, "REDIS", "KCTX.SZINFO %s not found", keystr.c_str());
			return;
		}

		string color = args[0];
		string plate = args[1];
		string owner = args[3];
		string car_type = args[2];
		string db44_code = args[4];
		switch(atoi(color.c_str())) {
		case 1:
			plate += "\xc0\xb6";
			break;
		case 2:
			plate += "\xbb\xc6";
			break;
		case 3:
			plate += "\xba\xda";
			break;
		case 4:
			plate += "\xb0\xd7";
			break;
		}

		UrlCoding urlcoding('%');
		plate = urlcoding.encode(plate);
		owner = urlcoding.encode(owner);

		valstr = "6," + fields[1] + "$" + plate + "$" + car_type + "$%CE%DE$0$$$$$$";
		valstr += owner + "$$$$$$$$$$$$$" + db44_code;
		vector<char> rspbuf(valstr.size() + 2);
		*(unsigned short*)&rspbuf[0] = htons(valstr.size());
		memcpy(&rspbuf[2], valstr.c_str(), valstr.length());
		SendData(sock, &rspbuf[0], rspbuf.size());
		OUT_HEX( _client_user._ip.c_str() , _client_user._port , "SEND", &rspbuf[0], rspbuf.size());
	} else if(fields[0] == "5") {
		_client_user._last_active_time = time(NULL);
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
		} else if(tv - user._last_active_time > 30) {
			string noop = string("\x00\x01\x37", 3);
			SendData(user._fd, noop.c_str(), noop.length()); //心跳包
			OUT_HEX( user._ip.c_str() , user._port , "SEND", noop.c_str(), noop.length());
		}

		sleep(10);
	}
}

void PasClient::NoopWork()
{
}

int PasClient::build_login_msg(User &user, char *buf, int len)
{
	unsigned short akeylen;
	const int OFFSET = sizeof(short);
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

	const char *username = _client_user._user_name.c_str();
	akeylen = snprintf(buf + OFFSET, len - OFFSET, "1,%s,%s,%s", username, varbuf, md5buf);
	*(unsigned short*)buf = htons(akeylen);

	return akeylen + OFFSET;
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
		OUT_WARNING( user._ip.c_str() , user._port , "FAIL", "%.*s" , len - 2, data + 2);
		return false;
	}

	if (!SendData(user._fd, data, len)) {
		OUT_WARNING( user._ip.c_str() , user._port , "FAIL", "%.*s" , len - 2, data + 2);
		return false;
	}

	OUT_HEX( user._ip.c_str() , user._port , "SEND", data, len);

	return true;
}

struct packet * PasClient::CPackSpliter::get_kfifo_packet( DataBuffer *fifo )
{
	const size_t MIN_LEN = 3; //2字节长度 + 1字节消息号
	unsigned int len = fifo->getLength();
	if (len < MIN_LEN) {
		return NULL;
	}

	if (len > MAX_PACK_LEN) {
		fifo->resetBuf();
		return NULL;
	}

	struct packet *item = NULL;
	struct list_head *packet_list_ptr = NULL;

	size_t i;
	char *ptr;
	unsigned short item_len;

	i = 0;
	ptr = fifo->getBuffer() ;
	while(i < len) {
		item_len = ntohs(*(unsigned short*)(ptr + i));
		if(item_len + sizeof(short) > len) {
			break; //当前内容不够长
		}

		item = (struct packet *) malloc(sizeof(struct packet));
		if (item == NULL) {
			break;
		}
		item->data = (unsigned char *) malloc(item_len);
		if (item->data == NULL) {
			free(item);
			break;
		}

		memcpy(item->data, ptr + sizeof(short) + i, item_len);
		item->len = item_len;
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

		i += sizeof(short) + item_len;
	}

	if (i > 0) fifo->removePos(i);

	return (struct packet*) packet_list_ptr;
}

