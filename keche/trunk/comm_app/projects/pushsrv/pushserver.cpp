/*
 * pushserver.cpp
 *
 *  Created on: 2012-4-28
 *      Author: humingqing
 *  添加简单服务器通信框架
 */

#include "msgclient.h"
#include "pushserver.h"
#include <comlog.h>
#include <tools.h>
#include <netutil.h>
#include <protocol.h>
#include <intercoder.h>
#include <arpa/inet.h>

PushServer::PushServer()
{
	_thread_num = 10;
}

PushServer::~PushServer(void)
{
	Stop();
}

bool PushServer::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv;

	char szip[128] = { 0 };
	if (!pEnv->GetString("push_listen_ip", szip)) {
		OUT_ERROR( NULL, 0, NULL, "get msg_listen_ip failed");
		return false;
	}
	_listen_ip = szip;

	int port = 0;
	if (!pEnv->GetInteger("push_listen_port", port)) {
		OUT_ERROR( NULL, 0, NULL, "get msg_listen_port failed");
		return false;
	}
	_listen_port = port;

	int nvalue = 10;
	if (pEnv->GetInteger("push_tcp_thread", nvalue)) {
		_thread_num = nvalue;
	}

	// 设置数据分包对象
	_tcp_handle.setpackspliter(&_pack_spliter);

	_proto_convert.init();

	return true;
}

bool PushServer::Start(void)
{
	return StartServer(_listen_port, _listen_ip.c_str(), _thread_num);
}

// 重载STOP方法
void PushServer::Stop(void) {
	StopServer();
}

void PushServer::TimeWork() {
	vector<User> users;
	vector<User>::iterator ite;

	OUT_INFO( NULL, 0, NULL, "void PushServer::TimeWork()");
	//首先做的一件事就是加载用户信息列表
	while (1) {
		users = _online_user.GetOfflineUsers(30);

		for ( ite = users.begin(); ite != users.end(); ++ ite ) {
			CloseSocket( ite->_fd );
			OUT_INFO( NULL, 0, ite->_user_name.c_str(), "%s:%d, TimeOut", ite->_ip.c_str(), ite->_port);
		}

		sleep(10);
	}
}

void PushServer::NoopWork() {
	OUT_INFO( NULL, 0, NULL, "void PushServer::NoopWork()");
}

void PushServer::on_data_arrived( socket_t *sock, const void *data, int len) //从客户端数据过来的数据
{
	string user_name;
	string corp_code;

	CInterCoder coder;
	// 对接收到的数据进行解码处理
	if (!coder.Decode((const char *) data, len)) {
		OUT_ERROR( sock->_szIp, sock->_port, NULL, "fd %d, recv error data:%d,%s", sock->_fd, len, ( const char *)data);
		return;
	}

	User user = _online_user.GetUserBySocket( sock );
	if (user._user_id.empty()) {
		return;
	}

	NewProto new_proto;
	_new_parse.ParseProto((const char*) coder.Buffer(), coder.Length(),
			&new_proto);

	if (new_proto.msg_type == MSG_REQUEST_LOGIN) {
		// printf("new_proto.msg_type == MSG_REQUEST_LOGIN \n");

		size_t i;
		set<string> macids;
		vector<string> macidVec;

		unsigned char loginRes = 0;

		DataBuffer data_buffer;
		RESP_LOGIN resp;

		_new_parse.ParseLoginMsg(&new_proto, corp_code, user_name);
		if(corp_code.empty() || user_name.empty()) {
			CloseSocket( sock );
			return;
		}

		std::set<std::string> hset ;
		// 加载订阅关系数据
		LoadSubscribe( ("pushserver." + corp_code).c_str(), macidVec, hset ) ;
		if ( macidVec.empty() ) {
			loginRes = 1;
		}

		// 读取订阅关系
		size_t nsize = macidVec.size() ;
		for ( i = 0; i < nsize ; ++i ) {
			std::string &s = macidVec[i] ;
			if ( strncmp( s.c_str(), "JMP:", 4 ) == 0 ) {
				continue ;
			}
			macids.insert( s ) ;
		}

		resp.result = loginRes;
		_proto_convert.BuildNewProto(MSG_REQUEST_LOGIN | 0x8000,
				sizeof(RESP_LOGIN), (const char*) &resp, &data_buffer);
		SendData( sock, data_buffer.getBuffer(), data_buffer.getLength());

		OUT_INFO( sock->_szIp, sock->_port, user_name.c_str() , "login msg compony_id: %s, user_name: %s, group_size: %d , result %d",
				corp_code.c_str(), user_name.c_str(), macids.size() , loginRes );

		if (loginRes != 0) {
			CloseSocket( sock );
			return;
		}

		user._user_name = user_name;
		user._user_pwd = corp_code;
		user._login_time = time(NULL);
		user._last_active_time = time(NULL);
		_online_user.SetUser(user._user_id, user);

		_subs_info.regUser(user._user_id, macids);
		_pEnv->GetMsgClient()->AddDemand(("pushserver." + corp_code).c_str(), DEMAND_GROUP);

		return;
	}

	if(user._user_name.empty() || user._user_pwd.empty()) {
		CloseSocket( sock );
		return;
	}
	new_proto.user_name = user._user_name;

	user._last_active_time = time(NULL);
	_online_user.SetUser(user._user_id, user);

	if (new_proto.msg_type == MSG_REQUEST_NOOP) {
		DataBuffer data_buffer;
		_proto_convert.BuildNewNoop(&data_buffer);
		SendData( sock , data_buffer.getBuffer(), data_buffer.getLength());
	} else if (new_proto.msg_type == MSG_REQUEST_SUBSCRIBE_INFO) {
		set<string> subs_macs;

		set<string>::iterator ite;

		_new_parse.ParseSubsInfo(&new_proto, subs_macs);

		_subs_info.update(user._user_id, subs_macs);

		DataBuffer data_buffer;
		_proto_convert.BuildNewCommResp(MSG_REQUEST_SUBSCRIBE_INFO | 0x8000,
				new_proto.msg_seq, 0, &data_buffer);
		SendData( sock, data_buffer.getBuffer(), data_buffer.getLength());
	} else if (new_proto.msg_type == MSG_REQUEST_SUBSCRIBE_APPEND) {
		set<string> subs_macs;

		set<string>::iterator ite;

		_new_parse.ParseSubsInfo(&new_proto, subs_macs);

		for(ite = subs_macs.begin(); ite != subs_macs.end(); ++ite) {
			_subs_info.apped(user._user_id, *ite);
		}

		DataBuffer data_buffer;
		_proto_convert.BuildNewCommResp(MSG_REQUEST_SUBSCRIBE_APPEND | 0x8000,
				new_proto.msg_seq, 0, &data_buffer);
		SendData(sock, data_buffer.getBuffer(), data_buffer.getLength());
	} else if (new_proto.msg_type == MSG_REQUEST_SUBSCRIBE_ERASE) {
		set<string> subs_macs;

		set<string>::iterator ite;

		_new_parse.ParseSubsInfo(&new_proto, subs_macs);

		for(ite = subs_macs.begin(); ite != subs_macs.end(); ++ite) {
			_subs_info.erase(user._user_id, *ite);
		}

		DataBuffer data_buffer;
		_proto_convert.BuildNewCommResp(MSG_REQUEST_SUBSCRIBE_ERASE | 0x8000,
				new_proto.msg_seq, 0, &data_buffer);
		SendData(sock, data_buffer.getBuffer(), data_buffer.getLength());
	} else {
		ConvertProto::InterProtoOut inter_out;
		DataBuffer data_buffer;

		if (_proto_convert.NewProto2InterProto(&new_proto, &inter_out)) {
			OUT_ERROR( NULL, 0, NULL, "Send inter_msg: %s", inter_out.msg.c_str());
			_pEnv->GetMsgClient()->HandleMsgData(inter_out.mac_id.c_str(),
					inter_out.msg.c_str(), inter_out.msg.length());

			switch (new_proto.msg_type) {
			case MSG_REQUEST_LOCATION:
			case MSG_REQUEST_TRACE:
				_subs_info.apped(user._user_id, inter_out.mac_id);
				break;
			case MSG_REQUEST_GET_PARAM:
			case MSG_REQUEST_MEDIA_SEARCH:
				break;
			default:
				_proto_convert.BuildNewCommResp(new_proto.msg_type | 0x8000,
						new_proto.msg_seq, 0, &data_buffer);
				SendData( sock, data_buffer.getBuffer(), data_buffer.getLength());
				break;
			}
		}
	}

}

// 加订阅关系
void PushServer::LoadSubscribe( const char *key, std::vector<std::string> &vec, std::set<std::string> &kset )
{
	if ( key == NULL )  return ;

	std::set<std::string>::iterator it = kset.find( key ) ;
	if ( it != kset.end() ) {
		return ;
	}
	kset.insert( std::set<std::string>::value_type(key) ) ;

	int size  = vec.size() ;
	int count = _pEnv->GetRedisCache()->GetList( key , vec ) ;
	if ( count == 0 ) return ;

	int len = vec.size() ;
	// 处理缓存中指令
	for ( int i = size; i < len; ++ i ) {
		string &s = vec[i] ;
		if ( s.empty() )
			continue ;
		// 这里使用跳转指令来处理数据，也就是数据组可以订阅子组
		if ( strncmp( s.c_str(), "JMP:", 4 ) == 0 ) {
			LoadSubscribe( s.substr(4).c_str(), vec, kset ) ;
		}
	}
}

void PushServer::on_new_connection( socket_t *sock, const char* ip, int port)
{
	char szid[256] = {0};
	sprintf(szid, "%lu", netutil::strToAddr(ip, port));

	User user = _online_user.GetUserByUserId(szid);
	if (!user._user_id.empty())
		return;

	user._user_id = szid;
	user._fd = sock ;
	user._ip = ip;
	user._port = port;
	user._user_state = User::ON_LINE;
	user._socket_type = User::TcpServer;
	user._last_active_time = time(NULL);

	// 添加到用户队列中
	_online_user.AddUser(user._user_id, user);
}

void PushServer::on_dis_connection( socket_t *sock )
{
	string user_name;
	string corp_code;

	User user = _online_user.DeleteUser( sock );
	if(user._user_id.empty() || user._user_name.empty() || user._user_pwd.empty()) {
		return;
	}

	user_name = user._user_name;
	corp_code = "pushserver." + user._user_pwd;

	_subs_info.unregUser(user._user_id);
	_pEnv->GetMsgClient()->DelDemand(corp_code.c_str(), DEMAND_GROUP);
}

void PushServer::NotifyMsgData(const string &mac_id, const char *data, int len)
{
	set<string> subs_users;
	if (_subs_info.getDefUser(mac_id, subs_users)) {
		if (subs_users.empty()) {
			OUT_ERROR( NULL, 0, mac_id.c_str(), "subscribe number is 0  mac_id : %s \n", mac_id.c_str());
			return ;
		}

		set<string>::iterator iter = subs_users.begin();
		for (; iter != subs_users.end(); ++iter) {
			// printf("SendDataToUser %s -> %s \n", mac_id.c_str(), (*iter).c_str());
			SendDataToUser(*iter, (char*) data, len);
		}
	} else {
		OUT_ERROR( NULL, 0, mac_id.c_str(), "get User fail,  mac_id: %s \n", mac_id.c_str());
	}
}

void PushServer::DispathMsgData(const string &mac_id, const char *data, int len)
{
	set<string> subs_users;
	if (_subs_info.getSubUser(mac_id, subs_users)) {
		if (subs_users.empty()) {
			OUT_ERROR( NULL, 0, mac_id.c_str(), "subscribe number is 0  mac_id : %s \n", mac_id.c_str());
			return ;
		}

		set<string>::iterator iter = subs_users.begin();
		for (; iter != subs_users.end(); ++iter) {
			// printf("SendDataToUser %s -> %s \n", mac_id.c_str(), (*iter).c_str());
			SendDataToUser(*iter, (char*) data, len);
		}
	} else {
		OUT_ERROR( NULL, 0, mac_id.c_str(), "get User fail,  mac_id: %s \n", mac_id.c_str());
	}
}

void PushServer::SendDataToUser(const string &user_id, char *data, int len)
{
	int i;
	int bufpos;
	int buflen;
	char buffer[BUFSIZ + 1];
	char *bufptr;

	User user = _online_user.GetUserByUserId(user_id);
	if(user._user_id.empty()) {
		return;
	}

	SendDataEx(user._fd, data, len);

	bufpos = 0;
	for (i = 0; bufpos < BUFSIZ && i < len; ++i) {
		buflen = BUFSIZ - bufpos;
		bufptr = buffer + bufpos;
		bufpos += snprintf(bufptr, buflen, "%02x", (unsigned char) data[i]);
	}

	OUT_SEND(NULL, 0, user._user_name.c_str(), "SendDataToUser: %.*s\n", bufpos, buffer);
}

// 从MSG转发过来的数据
void PushServer::HandleData(const char *data, int len)
{
	ConvertProto::NewProtoOut out;
	InterProto inter_proto;

	// printf("receive data: %s \n", data);
	if (_inter_parse.ParseProto(data, len, &inter_proto)) {
		if (_proto_convert.InterProto2NewProto(&inter_proto, &out)) {
			if (out.msg_type == alert) {
				NotifyMsgData(out.mac_id, out.data_buffer.getBuffer(),
						out.data_buffer.getLength());
			} else if (out.msg_type == gps_info) {
				//转发
				DispathMsgData(out.mac_id, out.data_buffer.getBuffer(),
						out.data_buffer.getLength());
			} else if (out.msg_type == resp) {
				//resp 转发
			} else {

			}
		}
	}
}

// 发送加密数据
bool PushServer::SendDataEx( socket_t *sock, const char *data, int len)
{
	if ( sock == NULL ) return false ;
	CInterCoder coder;
	if (!coder.Encode(data, len)) {
		return false;
	}
	return SendData( sock, coder.Buffer(), coder.Length());
}

