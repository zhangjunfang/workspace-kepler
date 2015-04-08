#include "msgclient.h"
#include <tools.h>
#include <netutil.h>
#include <intercoder.h>
#include <Base64.h>

#include <curl/curl.h>
#include "tinyxml.h"

#include "httpquery.h"

#pragma pack(1)
typedef struct RoadInfo {
	unsigned short h_ver;
	unsigned short h_cmd;
	unsigned int   h_seq;
	unsigned int   h_len;
    unsigned char level;
    unsigned char speed;
    unsigned char type;
    unsigned char gpsdt[6];
} RoadInfo;
#pragma pack()

int roadType(const char *strXml)
{
    TiXmlDocument doc;
    TiXmlElement *root;
    TiXmlElement *node;
    const char *code;

    doc.Parse(strXml, 0, TIXML_ENCODING_UTF8);
    root = doc.RootElement();
    if(root == NULL) {
        return 0;
    }

    code = root->Attribute("code");
    if(code == NULL || atoi(code) == 0) {
        return 0;
    }

    node = root->FirstChildElement();
    if(node == NULL) {
        return 0;
    }

    node = node->FirstChildElement("nr");
    if(node == NULL || node->FirstChild() == NULL || node->FirstChild()->Value() == NULL) {
        return 0;
    }

    return atoi(node->FirstChild()->Value());
}

bool str2bcd(const string &str, unsigned char *bcd, size_t len)
{
    size_t n;
    size_t i, j;
    unsigned char h, l;

    n = str.size();
    if(n % 2) {
        return false;
    }

    for(i = j = 0; i < n && j < len; ++j) {
        h = str[i++] - '0';
        l = str[i++] - '0';

        bcd[j] = (h << 4) | l;
    }

    return true;
}

MsgClient::MsgClient(void)
{
	_seqid = 0;
}

MsgClient::~MsgClient(void)
{
	Stop();
}

bool MsgClient::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv;

	char buf[1024] = {0};

	if ( ! _pEnv->GetString( "msg_user_name", buf ) ) {
		OUT_ERROR( NULL, 0, NULL, "msg_user_name empty" ) ;
		return false ;
	}
	_client_user._user_name = buf;

	if ( ! _pEnv->GetString( "msg_user_pwd", buf ) ) {
		OUT_ERROR( NULL, 0, NULL, "msg_user_pwd empty" ) ;
		return false ;
	}
	_client_user._user_pwd  = buf;

	if ( ! _pEnv->GetString( "msg_connect_ip", buf ) ) {
		OUT_ERROR( NULL, 0, NULL, "msg_connect_ip empty" ) ;
		return false ;
	}
	_client_user._ip = buf ;

	if ( ! _pEnv->GetString( "msg_connect_port", buf ) ) {
		OUT_ERROR( NULL, 0, NULL, "msg_connect_port empty" ) ;
		return false ;
	}
	_client_user._port = atoi(buf) ;

	if (!_pEnv->GetString("map_url", buf)) {
		OUT_ERROR( NULL, 0, NULL, "map_url empty" );
		return false;
	}
	_map_url = buf;

	setpackspliter(&_packspliter);

	_client_user._user_state = User::OFF_LINE;

	curl_global_init(CURL_GLOBAL_ALL);

	return true;
}

void MsgClient::Stop(void)
{
	OUT_INFO("Msg", 0, "MsgClient", "stop");

	StopClient();
}

bool MsgClient::Start(void)
{
	return StartClient(_client_user._ip.c_str(), _client_user._port, 16);
}

void MsgClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
	const char *ptr = (const char *) data;

	if (len < 4)
	{
		OUT_ERROR( sock->_szIp, sock->_port, NULL, "recv data error length: %d", len );
		return;
	}

	//OUT_RECV( sock->_szIp, sock->_port,  NULL, "on_data_arrived:[%d]%s", len, ptr);

	if (strncmp(ptr, "CAIT", 4) == 0) {
		// 纷发处理数据
		HandleInnerData( sock, ptr, len);
	} else {
		// 处理登陆相关
		HandleSession( sock, ptr, len);
	}
}

void MsgClient::on_dis_connection( socket_t *sock )
{
	_client_user._user_state = User::OFF_LINE;
}

void MsgClient::TimeWork()
{
	while (Check()) {
		if(_client_user._user_state != User::ON_LINE || time(NULL) - _client_user._last_active_time > 30) {
			ConnectServer(_client_user, 3);
		} else {
			SendData(_client_user._fd, "NOOP \r\n", 7);
		}

		sleep(10);
	}
}

void MsgClient::NoopWork()
{
}

int MsgClient::build_login_msg(User &user, char *buf, int buf_len)
{
	sprintf(buf, "LOGI SAVE %s %s \r\n", user._user_name.c_str(), user._user_pwd.c_str());
	return (int) strlen(buf);
}

void MsgClient::HandleSession( socket_t *sock, const char *data, int len)
{
	string line(data, len - 2);

	vector<string> vec_temp;
	if (!splitvector(line, vec_temp, " ", 1))
	{
		return;
	}

	string head = vec_temp[0];
	if (head == "LACK")
	{
		/*
		 RESULT
		 >=0:权限值
		 -1:密码错误
		 -2:帐号已经登录
		 -3:帐号已经停用
		 -4:帐号不存在
		 -5:sql查询失败
		 -6:未登录数据库
		 */
		int ret = atoi(vec_temp[1].c_str());
		switch (ret)
		{
		case 0:
		case 1:
		case 2:
		case 3:
			_client_user._user_state = User::ON_LINE;
			OUT_INFO(sock->_szIp, sock->_port, NULL, "LACK,login success!");
			break;
		case -1:
			OUT_ERROR(sock->_szIp, sock->_port, NULL, "LACK,password error!");
			break;
		case -2:
			OUT_ERROR(sock->_szIp, sock->_port, NULL, "LACK,the user has already login!");
			break;
		case -3:
			OUT_ERROR(sock->_szIp, sock->_port, NULL, "LACK,user name is invalid!");
			break;
		default:
			OUT_ERROR( sock->_szIp, sock->_port, NULL, "unknow result" );
			break;
		}

		// 如果返回错误则直接处理
		if (ret < 0)
		{
			_tcp_handle.close_socket( sock );
		}
	}
	else if (head == "NOOP_ACK") {
		_client_user._last_active_time = time(NULL);
		OUT_INFO( sock->_szIp, sock->_port, _client_user._user_name.c_str(), "NOOP_ACK");
	} else {
		OUT_WARNING( sock->_szIp, sock->_port, NULL, "except message:%s", (const char*)data);
	}
}

void MsgClient::HandleInnerData( socket_t *sock, const char *data, int len)
{
	int i;
	User &user = _client_user;

	vector<string> v1;
	vector<string> v2;
	vector<string> v3;

	string type;
	string lonStr;
	string latStr;
	string gpsdt;
	string speed;
	string angle;
	string subInfo;

	string body;
	string macid;
	string value;

	double lonDbl;
	double latDbl;

	char req[10240];
	vector<char> rsp;

	string line(data, len);

	v1.clear();
	if (!splitvector(line, v1, " ", 6))
	{
		OUT_ERROR( sock->_szIp, sock->_port, user._user_name.c_str() , "fd %d data error: %s", sock->_fd, data );
		return;
	}

	if (v1[0] != "CAITS" || v1[4] != "U_REPT") {
		return;
	}

	macid = v1[2];
	value = v1[5];
	if(value.length() < 70) {
		// {TYPE:0,1:600000,2:600000,20:0,3:0,4:20130327/142127,5:0,6:0,8:0,549:0}
		// 位置最少数据项长度
		return;
	}
	body = value.substr(1, value.size() - 2);

	type = lonStr = latStr = gpsdt = speed = angle = subInfo = "";

	v2.clear();
	splitvector(body, v2, ",", 0);
	for (i = 0; i < v2.size(); ++i) {
		v3.clear();
		if( ! splitvector(v2[i], v3, ":", 2)) {
			continue;
		}

		if (v3[0] == "TYPE") {
			type = v3[1];
		} else if (v3[0] == "1") {
			// 经度
			lonStr = v3[1];
		} else if (v3[0] == "2") {
			// 纬度
			latStr = v3[1];
		} else if (v3[0] == "3") {
			// 速度
			speed = v3[1];
		} else if (v3[0] == "4") {
			// 时间
			gpsdt = v3[1];
		} else if (v3[0] == "5") {
			// 方向
			angle = v3[1];
		} else if(v3[0] == "549") {
			// 订阅
			subInfo = v3[1];
		}
	}

	if(type != "0" || subInfo.empty() || gpsdt.size() != 15) {
		// 不是位置数据、没有路况订阅、时间长度不对
		return;
	}

	lonDbl = atoi(lonStr.c_str()) / 600000.0;
	latDbl = atoi(latStr.c_str()) / 600000.0;
	if (lonDbl < 1 || latDbl < 1 || angle.empty() || atoi(speed.c_str()) < 50) {
		// 没有定位或速度太慢
		OUT_INFO(user._fd->_szIp, user._fd->_port, "Parameter error", line.c_str());
		return;
	}

	OUT_INFO(user._fd->_szIp, user._fd->_port, "road info query", "%s", line.c_str());

	snprintf(req, 10240, "%s?coord=%.6f%%20%.6f&angle=%s", _map_url.c_str(), lonDbl, latDbl, angle.c_str());

	RoadInfo ri;
	HttpQuery hQuery;

	ri.h_ver = htons(0x0001);
	ri.h_cmd = htons(0x0001);
	ri.h_seq = htonl(++_seqid);
	ri.h_len = htonl(0x0009);
	ri.level = 0;
	ri.speed = 0;
	ri.type  = 0;


	if(hQuery.get(req) == true && hQuery.data() != NULL && hQuery.size() > 0) {
		string rspXml((char*)hQuery.data(), hQuery.size());

		switch (roadType(rspXml.c_str())) {
		case 1:
		case 3:
			ri.level = 1;
			break;
		case 5:
		case 6:
		case 7:
			ri.level = 2;
			break;
		case 2:
		case 4:
		case 8:
		case 9:
		case 10:
			ri.level = 3;
			break;
		}
	}


	gpsdt = gpsdt.substr(2, 6) + gpsdt.substr(9, 6);
	str2bcd(gpsdt, ri.gpsdt, 6);

	CBase64 base64;
	base64.Encode((char*)&ri, sizeof(RoadInfo));

	string inner = "CAITS 0_0 " + macid + " 0 D_SETP {TYPE:9,RETRY:0,91:240,90:";
	inner += base64.GetBuffer();
	inner += "} \r\n";

	if(user._user_state == User::ON_LINE && user._fd != NULL) {
		SendData(user._fd, inner.c_str(), inner.length());
		OUT_INFO(user._fd->_szIp, user._fd->_port, "reply success", inner.c_str());
	} else {
		OUT_ERROR(user._fd->_szIp, user._fd->_port, "reply failure", inner.c_str());
	}

}
