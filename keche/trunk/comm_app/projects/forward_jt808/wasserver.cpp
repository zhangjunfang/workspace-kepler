/**********************************************
 * WasServer.cpp
 *
 *  Created on: 2014-8-19
 *      Author: ycq
 *********************************************/
#include "wasserver.h"
#include "comlog.h"
#include "jt808prot.h"

#include "../tools/utils.h"

WasServer::WasServer()
{
	_thread_num = 8;
	_max_timeout = 180;
}

WasServer::~WasServer()
{
	Stop() ;
}

bool WasServer::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	int nvalue = 0 ;
	char szbuf[1024] = {0};

	if (!pEnv->GetString("was_listen_ip", szbuf)) {
		OUT_ERROR( NULL, 0,"WasServer", "Get was_listen_ip failed\n" );
		return false;
	}
	_listen_ip = szbuf;

	int port = 0 ;
	if ( ! pEnv->GetInteger( "was_tcp_port", port ) )
	{
		OUT_ERROR( NULL, 0,"WasServer", "Get was_tcp_port failed\n" ) ;
		return false ;
	}
	_listen_port = port;

	// 如果开启了UDP的监听就使用UDP
	if (pEnv->GetInteger("was_udp_port", port)) {
		_udp_listen_port = port;
		_udp_listen_ip = _listen_ip;
	}

	if (pEnv->GetInteger("server_thread", nvalue)) {
		_thread_num = nvalue;
	}

	if ( pEnv->GetInteger( "was_user_time" , nvalue ) ) {
		_max_timeout = nvalue ;
	}

	// 设置分包对象
	setpackspliter( & _pack_spliter ) ;

	return true;
}

bool WasServer::Start( void )
{
	if ( _udp_listen_port > 0 ) {
		//  如果打开了UDP的数据传输就直接使用UDP处理
		 if ( ! StartUDP( _udp_listen_port , _udp_listen_ip.c_str() , _thread_num , _max_timeout ) ) {
			 OUT_ERROR( NULL, 0,"WasServer", "Start udp server failed, ip %s port %d" ,
					 _udp_listen_ip.c_str(), _udp_listen_port ) ;
			 return false ;
		 }
	}

	return StartServer( _listen_port, _listen_ip.c_str(), _thread_num , _max_timeout ) ;
}

void WasServer::Stop( void )
{
	StopServer() ;
}

void WasServer::TimeWork()
{
	while (Check()) {
		vector<User> users = _online_user.GetOfflineUsers(_max_timeout);

		for(int i = 0; i < (int)users.size(); i++) {
			User &user = users[i];

			CloseSocket(user._fd);
		}

		sleep(30);
	}
}

void WasServer::NoopWork()
{
	while(Check()) {
		sleep(5);
	}
}

// 从BCD码中取得手机号
static bool bcd2sim(uint8_t *bcd, string &sim)
{
    const char *TAB = "0123456789abcdef";
    const size_t BCDLEN = 6;
    char buf[BCDLEN * 2 + 1];
    size_t i;
    size_t begin = string::npos;
    size_t end = 0;

    for(i = 0; i < BCDLEN; ++i) {
        buf[end] = TAB[bcd[i] >> 4];
        if(begin == string::npos && buf[end] != '0') {
            begin = end;
        }
        ++end;

        buf[end] = TAB[bcd[i] & 0xf];
        if(begin == string::npos && buf[end] != '0') {
            begin = end;
        }
        ++end;
    }
    buf[end] = 0;

    if(begin == string::npos) {
        return false;
    }
    sim = buf + begin;

    return true;
}

static uint8_t get_check_sum(uint8_t *buf, size_t len)
{
	if(buf == NULL || len < 1)
		return 0;
	unsigned char check_sum = 0;
	for (int i = 0; i < (int) len; i++) {
		check_sum ^= buf[i];
	}
	return check_sum;
}

static void buildCommonResp(const JTHeader *header, uint16_t seqid, uint8_t result, uint8_t *buf)
{
	CommonResp *commonRsp = (CommonResp*)buf;
    commonRsp->header.delim = 0x7e;
    commonRsp->header.msgid = htons(0x8001);
    commonRsp->header.length = htons(5);
    memcpy(commonRsp->header.simid, header->simid, 6);
    commonRsp->header.seqid = htons(seqid);

    commonRsp->resp_seq = header->seqid;
    commonRsp->resp_msg_id = header->msgid;
    commonRsp->result = result;

    commonRsp->footer.check_sum = get_check_sum(buf + 1, sizeof(CommonResp) - 3);
    commonRsp->footer.delim = 0x7e;
}

// 当新连接到来时的处理
void WasServer::on_new_connection( socket_t *sock, const char* ip, int port)
{
	// 记录连接
	OUT_CONN( ip, port, "ENV", "Recv new connection fd %d", sock->_fd ) ;
	// 接收到新的连接的情况
}

void WasServer::on_data_arrived(socket_t *sock, const void *data, int len)
{
	const char *ptr = (const char*)data;
	const char *ip      = sock->_szIp;
	unsigned short port = sock->_port;
	OUT_HEX(ip, port, "RECV", ptr, len);

	const size_t min_len = sizeof(JTHeader) + sizeof(JTFooter);
	const size_t max_len = (min_len + sizeof(MsgPart) + 0x3ff) * 2;
	if(len < (int)min_len || len > (int)max_len) {
		OUT_WARNING(ip, port, "RECV", "invalid message, drop it! length %d", len) ;
		return;
	}

	vector<unsigned char> dstBuf;
	unsigned char *dstPtr;
	size_t dstLen;

	dstBuf.resize(len);
	dstPtr = &dstBuf[0];
	dstLen = Utils::deCode808((unsigned char*)data, len, dstPtr);

	JTHeader *header = (JTHeader*)dstPtr;
	uint16_t msgid = ntohs(header->msgid);
	uint16_t flags = ntohs(header->length);

	uint8_t ispack = flags & 0x2000; //第13位是否分包标识
	uint16_t length = flags & 0x3ff; //第0 ~ 9位消息体长度
	if(ispack == 1 && dstLen != length + min_len + sizeof(MsgPart)) {
		OUT_WARNING(ip, port, "RECV", "invalid message, drop it! length %d", len) ;
		return;
	} else if(ispack == 0 && dstLen != length + min_len) {
		OUT_WARNING(ip, port, "RECV", "invalid message, drop it! length %d", len) ;
		return;
	}

	string simid;
	if(!bcd2sim(header->simid, simid)) {
		OUT_WARNING(ip, port, "RECV", "invalid message, drop it! length %d", len) ;
		return;
	}

	if (msgid == 0x0100) { //终端注册
		if (dstLen < (int) (sizeof(TermRegister) + sizeof(JTFooter))) {
			OUT_ERROR( ip, port, simid.c_str(), "msg id %x, message length %d error" , msgid, len );
			return;
		}

		uint8_t result = 4;
		string akey = "";
		string valStr = "";
		_pEnv->GetRedisCache()->HGet("KCTX.FORWARD808.INFO", simid.c_str(), valStr);
		if(!valStr.empty()) {
			result = 0;
			akey.assign((char*)header->simid, 6);
		}

		vector<unsigned char> respBuf;
		TermRegisterResp *respPtr;
		JTFooter *footer;

		respBuf.resize(sizeof(TermRegisterResp) + akey.size() + sizeof(JTFooter));
		respPtr = (TermRegisterResp*)&respBuf[0];
		footer = (JTFooter*)&respBuf[sizeof(TermRegisterResp) + akey.size()];

		respPtr->header.delim = 0x7e;
		respPtr->header.msgid = htons(0x8100);
		respPtr->header.length = htons(3 + akey.size());
		memcpy(respPtr->header.simid, header->simid, 6);
		respPtr->header.seqid = 0;
		respPtr->resp_seq = header->seqid;
		respPtr->result = result;
		memcpy(respPtr->akey, akey.c_str(), akey.size());
		footer->check_sum = get_check_sum(&respBuf[0] + 1, respBuf.size() - 3);
		footer->delim = 0x7e;
		Send7ECodeData(sock, (char*)&respBuf[0], respBuf.size());

		return;
	} else if (msgid == 0x0102) { //终端鉴权
		int result = 1;

		string valStr = "";
		_pEnv->GetRedisCache()->HGet("KCTX.FORWARD808.INFO", simid.c_str(), valStr);
		if (valStr.empty()) {
			result = 0;
		}

		if (result == 0) {
			User user;
			user._user_id = simid;
			user._fd = sock;
			user._user_state = User::ON_LINE;
			user._ip = ip;
			user._port = port;
			user._login_time = time(NULL);
			user._last_active_time = time(NULL);

			if (!_online_user.AddUser(simid, user)) {
				User tmp = _online_user.DeleteUser(simid);
				if(tmp._fd != NULL && tmp._user_state == User::ON_LINE) {
					CloseSocket(tmp._fd);
				}
				_online_user.AddUser(simid, user);
			}

			_pEnv->GetWasClient()->AddTerminal(simid);
		}

		vector<unsigned char> respBuf;
		respBuf.resize(sizeof(CommonResp));
		buildCommonResp(header, 1, result, &respBuf[0]);
		Send7ECodeData(sock, (char*)&respBuf[0], respBuf.size());

		return;
	}

	User user = _online_user.GetUserBySocket(sock);
	if(user._user_id.empty()) {
		OUT_WARNING(ip, port, "RECV", "invalid message, drop it! length %d", len);
		return;
	}

	_pEnv->GetWasClient()->HandleData(simid, dstPtr, dstLen);
}

void WasServer::on_dis_connection(socket_t *sock)
{
	User user = _online_user.DeleteUser(sock);
	if (!user._user_id.empty()) {
		return;
	}

	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	OUT_INFO( sock->_szIp , sock->_port , user._user_id.c_str(), "ENV on_dis_connection fd %d", sock->_fd ) ;

	_pEnv->GetWasClient()->DelTerminal(user._user_id);
}

bool WasServer::Send7ECodeData( socket_t *sock, const char *data, int len )
{
	vector<unsigned char> msgBuf;
	unsigned char *msgPtr;
	size_t msgLen;

	msgBuf.resize(len * 2);
	msgPtr = &msgBuf[0];
	msgLen = Utils::enCode808((unsigned char*)data, len, msgPtr);

	OUT_HEX( sock->_szIp, sock->_port, "SEND" , (char*)msgPtr, msgLen );

	return SendData( sock, (char*)msgPtr, msgLen) ;
}

bool WasServer::HandleData(const string &sim, const uint8_t *ptr, size_t len)
{
	User user = _online_user.GetUserByUserId(sim);
	if(user._user_id.empty() || user._user_state == User::OFF_LINE || user._fd == NULL){
		OUT_HEX(NULL, 0, "FAIL", (const char*)ptr, len);
		return false;
	}

	return Send7ECodeData(user._fd, (char*)ptr, len);
}

bool WasServer::ChkTerminal(const string &sim)
{
	User user = _online_user.GetUserByUserId(sim);
	if(user._user_id.empty()) {
		return false;
	}

	return true;
}
