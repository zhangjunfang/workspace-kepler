/**********************************************
 * PasServer.cpp
 *
 *  Created on: 2010-10-11
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/

#include "passerver.h"
#include "BaseServer.h"
#include "ProtoHeader.h"
#include "BaseTools.h"
#include <tools.h>

PasServer::PasServer() : _filecache(this)
{
	_last_check = 0;
	_thread_num = 10;
	_max_send_num = 10000;
	_last_check = time(NULL);
	_login_check = NULL;
	_verify_code = 123456;
}

PasServer::~PasServer()
{
	Stop();

	if (_login_check != NULL) {
		delete _login_check;
		_login_check = NULL;
	}
}

// 初始化
bool PasServer::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv;

	int thread = 0;
	if (!_pEnv->GetInteger("pas_recv_thread", thread)) {
		thread = 10;
	}
	_thread_num = thread;

	char value[1024] = { 0 };
	if (!_pEnv->GetString("pas_listen_ip", value)) {
		printf("get pas_listen_ip failed!\n");
		return false;
	}
	_listen_ip = value;

	int port = 0;
	if (!_pEnv->GetInteger("pas_listen_port", port)) {
		printf("get pas_listen_port failed!\n");
		return false;
	}
	_listen_port = port;

	int max_send_num = 0;
	if (!_pEnv->GetInteger("pas_max_send_num", max_send_num)) {
		max_send_num = 10000;
	}
	if (max_send_num >= 1)
		_max_send_num = max_send_num;

	char szbuf[1024] = { 0 };
	if (!_pEnv->GetString("login_check_file", szbuf)) {
		printf("get login_check_file failed , %s:%d\n", __FILE__, __LINE__);
		return false;
	}

	_login_check = new LoginCheck(szbuf);
	if (!_login_check->Reload(0)) {
		OUT_ERROR(NULL, 0, NULL, "Reload CheckInfo error,file name is %s",
				szbuf);
		return false;
	}

	return _filecache.Init(pEnv, "pas");
}

// 开始线程
bool PasServer::Start(void) {
	if (!_filecache.Start()) {
		printf("PasServer::Start filecache failed\n");
		return false;
	}
	return StartServer(_listen_port, _listen_ip.c_str(), _thread_num);
}

// 停止处理
void PasServer::Stop(void) {
	StopServer();

	_filecache.Stop();
}

void PasServer::on_data_arrived(socket_t *sock, const void* data, int len) {
	_allstat.AddFlux(len); // 统计流量

	C5BCoder coder;
	if (!coder.Decode((const char *) data, len)) {
		OUT_WARNING( sock->_szIp , sock->_port, NULL, "Except packet header or tail" );
		OUT_HEX( sock->_szIp , sock->_port, NULL, (const char *) data, len );
		return;
	}

	HandleOnePacket(sock, (const char*) coder.GetData(), coder.GetSize());
}

bool PasServer::EncryptData(unsigned char *data, unsigned int len, bool encode)
{
	Header *header = (Header *) data;
	// 是否需要加密处理
	if (!header->encrypt_flag && !encode) {
		return false;
	}

	unsigned int M1 = 0, IA1 = 0, IC1 = 0;
	// 取得对应用户的解密密钥
	if (!_login_check->GetEncryptKey(ntouv32(header->access_code), M1, IA1,
			IC1)) {
		// 如果用户没密钥就不需要处理加密了
		return false;
	}

	// 如果为加密处理
	if (encode) {
		// 设置加密标志位
		header->encrypt_flag = 1;
		// 添加加密密钥
		header->encrypt_key = ntouv32( CEncrypt::rand_key() );
	}

	// 解密数据
	return CEncrypt::encrypt(M1, IA1, IC1, (unsigned char *) data,
			(unsigned int) len);
}

//src_data为转码前的数据，发送给mas和pas的。
void PasServer::HandleOnePacket(socket_t *sock, const char *data, int len)
{
	if (data == NULL || len < (int) sizeof(Header) + (int) sizeof(Footer))
		return;

	Header *header = (Header *) data;
	unsigned int access_code = ntouv32(header->access_code);
	string str_access_code = uitodecstr(access_code);

	// 针对接入码进行统计处理
	_datastat.AddFlux(access_code, len);

	unsigned int msg_len = ntouv32(header->msg_len);
	if (msg_len != (unsigned int) len) {
		OUT_ERROR( NULL, 0, NULL,
				"PasServer::HandleOnePacket packet len error");
		return;
	}

	// 是否需要加密处理
	if (EncryptData((unsigned char *) data, len, false)) {
		// 将加密标志清空
		header->encrypt_flag = 0;
	}

	unsigned short msg_type = ntouv16(header->msg_type);
	string mac_id = _proto_parse.GetMacId(data, len);

	const char *ip = sock->_szIp;
	unsigned int port = sock->_port;

	OUT_RECV3( ip, port, str_access_code.c_str(), "fd %d, %s", sock->_fd, _proto_parse.Decoder(data,len).c_str() );
//	OUT_HEX( ip, port, str_access_code.c_str(), data, len ) ;

	// 处理车号对应的接入码的关系
	if (!mac_id.empty()) {
		// 记录车号对应的接入码的关系
		_corp_info_handler.update_car_info(mac_id, access_code);
	}

	if (msg_type == UP_CONNECT_REQ) {
		User user = _online_user.GetUserByUserId("U_" + str_access_code);
		UpConnectReq *out_login = (UpConnectReq *) data;
		OUT_RECV(ip, port, str_access_code.c_str(),
				"fd %d, UP_CONNECT_REQ,down-link listen port:%d",
				sock->_fd, ntouv16(out_login->down_link_port));
		//判定是不是同一个socket发上来的UP_CONNECT_REQ。
		if (!user._user_id.empty()) {
			//说明在线列表中已有次用户，
			if (user._fd == sock) {
				OUT_WARNING( ip, port, str_access_code.c_str(), "fd %d, sock already in user queue" , sock->_fd );
				return;
			} else {
				OUT_WARNING(ip, port, str_access_code.c_str(),
						"fd %d, one user already login", sock->_fd);
				CloseSocket(sock);
				return;
			}
		}

		char szuser[128] = { 0 };
		char szpwd[128] = { 0 };
		sprintf(szuser, "%u", ntouv32(out_login->user_id));
		strncpy(szpwd, (const char*) out_login->password,
				sizeof(out_login->password));

		unsigned int flag = 0;
		unsigned char ret = _login_check->CheckUser(access_code, szuser, szpwd,
				ip, flag);
		UpConnectRsp resp;
		resp.header.access_code = ntouv32(access_code);
		resp.header.msg_len = ntouv32(sizeof(UpConnectRsp));
		resp.header.msg_type = ntouv16(UP_CONNECT_RSP);
		resp.header.msg_seq = ntouv32(_proto_parse.get_next_seq());
		resp.verify_code = ntouv32(_verify_code);
		resp.result = ret;

		switch (ret) {
		case 0:
			OUT_INFO(ip, port, str_access_code.c_str(),
					"login check success,access_code:%d  up-link ON_LINE",
					access_code);
			break;
		case 1:
			OUT_WARNING(ip, port, str_access_code.c_str(),
					"login check fail,ip is invalid");
			break;
		case 2:
			OUT_WARNING(ip, port, str_access_code.c_str(),
					"login check fail,%d accesscode is invalid,close it",
					access_code);
			break;
		case 3:
			OUT_WARNING(ip, port, str_access_code.c_str(),
					"login check fail,user_name:%s is invalid,close it",
					szuser);
			break;
		case 4:
			OUT_WARNING(ip, port, str_access_code.c_str(),
					"login check fail,user_password:%s is invalid,close it",
					szpwd);
			break;
		default:
			OUT_WARNING(ip, port, str_access_code.c_str(),
					"login check fail,other error,close it");
			break;
		}
		// 对于登陆的连接的响应处理
		if (SendCrcData(sock, (const char*) &resp, sizeof(resp)))
			OUT_SEND(ip,port,str_access_code.c_str(),"UP_CONNECT_RSP");
		else
			OUT_ERROR(ip,port,str_access_code.c_str(),"UP_CONNECT_RSP");

		// 如果用户认证不通过则直接关闭连接处理
		if (ret != 0) {
			OUT_WARNING( ip, port, str_access_code.c_str(), "result %d, close socket user fd %d", ret , sock->_fd );
			CloseSocket(sock);
			return;
		}

		//睡眠一毫秒等待数据发送出去。
		user._access_code = access_code;
		user._user_name = szuser;
		user._user_id = "U_" + str_access_code; //表明是上行链路
		user._login_time = time(0);
		user._msg_seq = flag; // 处理地市级跨域数据的处理

		user._socket_type = User::TcpClient;
		user._fd = sock;
		user._ip = ip;
		user._port = port;
		user._last_active_time = time(0);

		unsigned short down_link_port = ntouv16( out_login->down_link_port );
		char szdownip[64] = { 0 };
		strncpy(szdownip, (char*) out_login->down_link_ip,
				sizeof(out_login->down_link_ip));

		OUT_CONN( ip, port, str_access_code.c_str() , "fd %d, down connection down ip %s , down port %d", sock->_fd, szdownip, down_link_port );
		if (ret == 0) {
			user._user_state = User::ON_LINE;
			User down_user = _online_user.GetUserByUserId(
					"D_" + str_access_code);
			if (!down_user._user_id.empty()) { // 修正从链接切换地址的BUG
				if (down_user._fd != NULL) { // 如果已建立连接则关闭
					CloseSocket(down_user._fd);
				}
			}
			if (down_link_port > 0 && check_addr(szdownip)) {
				//说明下行链路还没有建立起来。
				down_user._user_id = "D_" + str_access_code;
				down_user._access_code = access_code;
				down_user._user_name = szuser;
				down_user._login_time = time(0);

				down_user._socket_type = User::TcpConnClient;
				down_user._fd = NULL;
				down_user._user_state = User::OFF_LINE;
				down_user._ip = szdownip;
				down_user._port = down_link_port;
				down_user._last_active_time = time(0);
				down_user._msg_seq = flag;

				//下行链路只重连3次。重连三次连不上时就将这个user erase掉。
				down_user._connect_info.keep_alive = ReConnTimes;
				down_user._connect_info.last_reconnect_time = 0;
				down_user._connect_info.timeval = 10;
				down_user._connect_info.reconnect_times = 3;

				if (!_online_user.AddUser(down_user._user_id, down_user)) {
					// 如果已存在则直接替换原来的会话
					_online_user.SetUser(down_user._user_id, down_user);
				}
			}
			// 只登陆成功才添加到用户队列中
			_online_user.AddUser(user._user_id, user);
		} else
			user._user_state = User::OFF_LINE;

		user._last_active_time = time(0);
		_online_user.SetUser(user._user_id, user);

		return;
	}

	User user = _online_user.GetUserBySocket(sock);
	if (user._user_id.empty()) {
		OUT_ERROR(ip, port, str_access_code.c_str(),
				"fd %d, msg type %04x, user havn't login,close it",
				sock->_fd, msg_type);
		CloseSocket(sock);
		return;
	}

	//未登录成功数据上来做单独处理，处理策略
	if (msg_type == UP_DISCONNECT_REQ) {
		OUT_RECV(ip, port, user._user_id.c_str(), "UP_DISCONNECT_REQ");

		//  发送从链路注销请求
		DownDisconnectReq req;
		req.header.msg_type = ntouv16(DOWN_DISCONNECT_REQ);
		req.header.msg_len = ntouv32(sizeof(DownDisconnectReq));
		req.header.msg_seq = ntouv32(_proto_parse.get_next_seq());
		req.header.access_code = header->access_code;
		req.verify_code = ntouv32(_verify_code);

		char buf[200];
		sprintf(buf, "D_%u", user._access_code);
		//  取从链路fd
		User sub_user = _online_user.GetUserByUserId(buf);

		if (!sub_user._user_id.empty()) {
			if (SendCrcData(sub_user._fd, (const char*) &req, sizeof(req))) {
				OUT_SEND(sub_user._ip.c_str(), sub_user._port,
						user._user_id.c_str(), "DOWN_DISCONNECT_REQ");
			} else
				OUT_ERROR(sub_user._ip.c_str(),sub_user._port,user._user_id.c_str(),"DOWN_DISCONNECT_REQ");
		}

		UpDisconnectRsp resp;
		resp.header.msg_seq = ntouv32(_proto_parse.get_next_seq());
		resp.header.access_code = header->access_code;

		if (SendCrcData(sock, (const char*) &resp, sizeof(resp)))
			OUT_SEND(ip, port, user._user_id.c_str(), "UP_DISCONNECT_RSP");
		else
			OUT_ERROR(ip,port,user._user_id.c_str(),"UP_DISCONNECT_RSP");
	} else if (msg_type == UP_LINKTEST_REQ) {
		OUT_RECV(ip, port, user._user_id.c_str(), "UP_LINKTEST_REQ");
		UpLinkTestRsp resp;
		resp.header.access_code = header->access_code;
		resp.header.msg_seq = ntouv32(_proto_parse.get_next_seq());

		if (SendCrcData(sock, (const char*) &resp, sizeof(resp)))
			OUT_SEND(ip, port, user._user_id.c_str(), "UP_LINKTEST_RSP");
		else
			OUT_ERROR(ip,port,user._user_id.c_str(),"UP_LINKTEST_RSP");
	} else if (msg_type == UP_CLOSELINK_INFORM) {
		//  不需要处理，由定时处理流程解决
		OUT_RECV(ip, port, user._user_id.c_str(), "fd %d, UP_CLOSELINK_INFORM",
				sock->_fd);

		char buf[200] = { 0 };
		sprintf(buf, "U_%u", user._access_code);
		//  取主链路fd
		User main_user = _online_user.GetUserByUserId(buf);
		if (!main_user._user_id.empty()) {
			{
				DownDisconnectInform req;
				req.header.access_code = ntouv32(user._access_code);
				req.header.msg_len = ntouv32(sizeof(DownDisconnectInform));
				req.header.msg_type = ntouv16(DOWN_DISCONNECT_INFORM);
				req.header.msg_seq = ntouv32(_proto_parse.get_next_seq());

				if (SendCrcData(main_user._fd, (const char*) &req, sizeof(req)))
					OUT_SEND(ip, port, user._user_id.c_str(),
							"fd %d, DOWN_DISCONNECT_INFORM", sock->_fd);
				else
					OUT_ERROR(ip, port, user._user_id.c_str(),
							"fd %d, DOWN_DISCONNECT_INFORM", sock->_fd);
			}

			{
				DownCloselinkInform req;
				req.header.access_code = ntouv32(user._access_code);
				req.header.msg_len = ntouv32(sizeof(DownCloselinkInform));
				req.header.msg_type = ntouv16(DOWN_CLOSELINK_INFORM);
				req.header.msg_seq = ntouv32(_proto_parse.get_next_seq());

				if (SendCrcData(main_user._fd, (const char*) &req, sizeof(req)))
					OUT_SEND(ip, port, user._user_id.c_str(),
							"fd %d, DOWN_CLOSELINK_INFORM", sock->_fd);
				else
					OUT_ERROR(ip, port, user._user_id.c_str(),
							"fd %d, DOWN_CLOSELINK_INFORM", sock->_fd);
			}

			main_user._last_active_time = time(0);
			_online_user.SetUser(main_user._user_id, main_user);

			user._user_state = User::OFF_LINE;
		}
	} else if (msg_type == UP_DISCONNECT_INFORM) {
		//  不需要处理，由定时处理流程解决
		OUT_RECV(ip, port, user._user_id.c_str(), "fd %d, UP_DISCONNECT_INFORM",
				sock->_fd);
	} else if (msg_type == UP_CLOSELINK_INFORM) {
		//  不需要处理，由定时处理流程解决
		OUT_RECV(ip, port, user._user_id.c_str(), "fd %d, UP_CLOSELINK_INFORM",
				sock->_fd);
		//  取主链路fd
		{
			DownDisconnectInform req;
			req.header.msg_len = ntouv32( sizeof(req) );
			req.header.msg_type = ntouv16( DOWN_DISCONNECT_INFORM );
			req.header.access_code = ntouv32(user._access_code);
			req.header.msg_seq = ntouv32(_proto_parse.get_next_seq());

			if (SendCrcData(sock, (const char*) &req, sizeof(req)))
				OUT_SEND(ip, port, user._user_id.c_str(),
						"fd %d, DOWN_DISCONNECT_INFORM", sock->_fd);
			else
				OUT_ERROR(ip, port, user._user_id.c_str(),
						"fd %d, DOWN_DISCONNECT_INFORM", sock->_fd);
		}

		{
			DownCloselinkInform req;
			req.header.msg_len = ntouv32( sizeof(req) );
			req.header.msg_type = ntouv16( DOWN_CLOSELINK_INFORM );
			req.header.access_code = ntouv32(user._access_code);
			req.header.msg_seq = ntouv32(_proto_parse.get_next_seq());

			if (SendCrcData(sock, (const char*) &req, sizeof(req)))
				OUT_SEND(ip, port, user._user_id.c_str(),
						"fd %d, DOWN_CLOSELINK_INFORM", sock->_fd);
			else
				OUT_ERROR(ip, port, user._user_id.c_str(),
						"fd %d, DOWN_CLOSELINK_INFORM", sock->_fd);
		}
	} else if (msg_type == UP_DISCONNECT_INFORM) {
		//  不需要处理，由定时处理流程解决
		OUT_RECV(ip, port, user._user_id.c_str(), "fd %d, UP_DISCONNECT_INFORM",
				sock->_fd);
	} else if (msg_type == DOWN_CONNECT_RSP) {
		DownConnectRsp *_down_resp = (DownConnectRsp*) data;
		if (_down_resp->result == 0) {
			OUT_RECV(ip, port, user._user_id.c_str(),
					"fd %d, DOWN_CONNECT_RSP,Down-Link ON_LINE", sock->_fd);
			user._connect_info.reconnect_times = 3;
			user._user_state = User::ON_LINE;
		} else {
			OUT_RECV(ip, port, user._user_id.c_str(),
					"fd %d, DOWN_CONNECT_RSP,connect fail,result=%d",
					sock->_fd, _down_resp->result);
		}
	} else if (msg_type == DOWN_LINKTEST_RSP) {
		// 这里处理有些运营商回复下行链路心跳时使用上行来回应
		OUT_RECV(ip, port, user._user_id.c_str(), get_type(msg_type));
		/**
		 if ( user._user_id.at(0) == 'U' ) {
		 User down = _online_user.GetUserByUserId("D_" + str_access_code);
		 if ( ! down._user_id.empty() ) {
		 down._last_active_time = time(0) ;
		 _online_user.SetUser( down._user_id, down ) ;
		 }
		 }*/
	} else if (msg_type == DOWN_DISCONNECT_RSP) {
		OUT_RECV(ip, port, user._user_id.c_str(), get_type(msg_type));
		//	user._user_state = User::OFF_LINE;
		if (user._user_state == User::ON_LINE && user._fd != NULL) {
			CloseSocket(user._fd);
		}
	} else if (msg_type == DOWN_DISCONNECT_INFORM) {
		//不管它，由定时线程来完成。
		OUT_RECV(ip, port, user._user_id.c_str(), get_type(msg_type));
	} else if (msg_type == DOWN_DISCONNECT_RSP) {
		OUT_RECV(ip, port, user._user_id.c_str(), get_type(msg_type));
		user._user_state = User::OFF_LINE;
	} else {
		if (msg_type == UP_EXG_MSG) {
			ExgMsgHeader *msg_header = (ExgMsgHeader*) (data + sizeof(Header));
			if (msg_header->vehicle_no[0] == (char) 0) {
				OUT_ERROR(ip, port, user._user_id.c_str(), "vehicle is null");
				return;
			}

			unsigned short data_type = ntouv16( msg_header->data_type );
			switch (data_type) {
			case UP_EXG_MSG_REAL_LOCATION:
				{
					OUT_RECV3(ip, port, user._user_id.c_str(), "UP_EXG_MSG_REAL_LOCATION:%s", msg_header->vehicle_no);

					CorpInfoHandle::_CorpInfo info = _corp_info_handler.update_corp_map(access_code, 1, _max_send_num);
					if (info.send_num < 1) {
						Header * pHeader = (Header *) (data);

						//给运营商回复DOWN_TOTAL_RECV_BACK_MSG
						DownTotalRecvBackMsg resp;
						resp.header.access_code = pHeader->access_code;
						resp.DynamicInfoTotal = ntouv32(_max_send_num);
						resp.start_time = ntouv64(info.send_time);
						resp.end_time = ntouv64(time(0));
						resp.header.msg_seq = ntouv32(_proto_parse.get_next_seq());

						char buf[200] = { 0 };
						sprintf(buf, "D_%u", user._access_code);
						User sub_user = _online_user.GetUserByUserId(buf);

						socket_t *isock = sock;
						if (!(sub_user._user_id.empty()
								|| sub_user._user_state != User::ON_LINE))
							isock = sub_user._fd;

						if (SendCrcData(isock, (const char*) &resp, sizeof(resp))) {
							OUT_SEND( ip, port, user._user_id.c_str(), "DOWN_TOTAL_RECV_BACK_MSG" );
						} else {
							OUT_ERROR( ip, port, user._user_id.c_str(), "DOWN_TOTAL_RECV_BACK_MSG" );
						}
					}
				}
				break;
			case UP_EXG_MSG_HISTORY_LOCATION:
				{
					OUT_RECV3(ip, port, user._user_id.c_str(), "UP_EXG_MSG_HISTORY_LOCATION:%s", msg_header->vehicle_no);
					UpExgMsgHistoryLocation * pReq = (UpExgMsgHistoryLocation *) (data);
					unsigned int num = pReq->gnss_cnt;

					CorpInfoHandle::_CorpInfo info = _corp_info_handler.update_corp_map(access_code, num, _max_send_num);
					if (info.send_num < num) {
						//给运营商回复DOWN_TOTAL_RECV_BACK_MSG
						DownTotalRecvBackMsg resp;
						resp.header.access_code = pReq->header.header.access_code;
						resp.DynamicInfoTotal = ntouv32(_max_send_num);
						resp.start_time = ntouv64(info.send_time);
						resp.end_time = ntouv64(time(0));
						resp.header.msg_seq = ntouv32(_proto_parse.get_next_seq());

						char buf[200];
						sprintf(buf, "D_%u", user._access_code);
						User sub_user = _online_user.GetUserByUserId(buf);

						socket_t *isock = sock;
						if (!(sub_user._user_id.empty()
								|| sub_user._user_state != User::ON_LINE))
							isock = sub_user._fd;

						if (SendCrcData(isock, (const char*) &resp, sizeof(resp))) {
							OUT_SEND( ip, port, user._user_id.c_str(), "DOWN_TOTAL_RECV_BACK_MSG" );
						} else {
							OUT_ERROR( ip, port, user._user_id.c_str(), "DOWN_TOTAL_RECV_BACK_MSG" );
						}
					}
				}
				break;
			case UP_EXG_MSG_APPLY_HISGNSSDATA_REQ: // 补报数据请求
				{
					if (len < (int) sizeof(UpExgApplyHisGnssDataReq)) {
						OUT_ERROR( ip, port, user._user_id.c_str(), "UP_EXG_MSG_APPLY_HISGNSSDATA_REQ data length errro, len %d" , len );
						return;
					}

					UpExgApplyHisGnssDataReq *req = (UpExgApplyHisGnssDataReq *) data;

					DownExgMsgApplyHisgnssdataAck resp;
					resp.header.msg_len = ntouv32( sizeof(DownExgMsgApplyHisgnssdataAck) );
					resp.header.access_code = req->header.access_code;
					resp.header.msg_type = ntouv16( DOWN_EXG_MSG );
					memcpy(resp.exg_msg_header.vehicle_no, req->exg_msg_header.vehicle_no, sizeof(resp.exg_msg_header.vehicle_no));
					resp.exg_msg_header.vehicle_color = req->exg_msg_header.vehicle_color;
					resp.exg_msg_header.data_type = ntouv16( DOWN_EXG_MSG_APPLY_HISGNSSDATA_ACK );
					resp.exg_msg_header.data_length = ntouv32( sizeof(char) );
					resp.result = 0x00;

					// 补报数据自动应答
					SendCrcData(sock, (const char *) &resp, sizeof(resp));

					// 设置用户为在线处理开始补报数据
					_filecache.Online(user._access_code);
				}
				break;
			default:
				OUT_RECV(ip, port, user._user_id.c_str(), get_type(data_type));
				break;
			}
		} else if (msg_type == UP_PLATFORM_MSG) {

		} else if (msg_type == UP_CTRL_MSG) {

		} else if (msg_type == UP_BASE_MSG) {

		} else if (msg_type == UP_WARN_MSG) {

		} else {
			OUT_ERROR(ip, port, user._user_id.c_str(), "msg_type=%04x,received an invalid message,len %d", msg_type, len);
			OUT_HEX( ip, port, user._user_id.c_str(), data, len );
		}
		_pEnv->GetMasServer()->HandleMasUpData((const char *) data, len);
	}
	user._last_active_time = time(0);
	_online_user.SetUser(user._user_id, user);
}

void PasServer::on_dis_connection(socket_t *sock)
{
	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	User user = _online_user.GetUserBySocket(sock);
	if (user._user_id.empty()) {
		return;
	}OUT_WARNING( user._ip.c_str(), user._port, user._user_id.c_str(),
			"Disconnection fd %d", sock->_fd);

	char szbuf[128] = { 0 };
	if (user._user_id.at(0) == 'D') { // 如果为从链路断开连接
		sprintf(szbuf, "U_%u", user._access_code);
		User mainuser = _online_user.GetUserByUserId(szbuf);

		// 如果主链路存在，从链路重连次不为零就再继续让它建立从链路
		if (!mainuser._user_id.empty()
				&& user._connect_info.reconnect_times > 0) {
			user._user_state = User::OFF_LINE;
			user._connect_info.last_reconnect_time = time(NULL);
			_online_user.SetUser(user._user_id, user);
			return;
		}
	} else { // 主链路断开连接
		sprintf(szbuf, "D_%u", user._access_code);
		User subuser = _online_user.GetUserByUserId(szbuf);

		// 如果主链路断开操作，则将从链路也直接断开
		if (!subuser._user_id.empty()) {
			_online_user.DeleteUser(szbuf);
			CloseSocket(subuser._fd);
		}
	}
	_datastat.Remove(user._access_code);

	_online_user.DeleteUser(sock);
}

bool PasServer::ConnectServer(User &user, unsigned int timeout)
{
	if (time(0) - user._connect_info.last_reconnect_time
			< user._connect_info.timeval)
		return false;

	bool ret = false;
	if (user._fd != NULL) {
		OUT_INFO(NULL, 0, NULL, "fd %d close socket", user._fd->_fd);
		CloseSocket(user._fd);
	}
	user._fd = _tcp_handle.connect_nonb(user._ip.c_str(), user._port, timeout);
	ret = (user._fd != NULL) ? true : false;

	if (ret) {
		user._user_state = User::WAITING_RESP;
		DownConnectReq req;
		req.header.msg_seq = ntouv32(_proto_parse.get_next_seq());
		req.header.access_code = ntouv32(user._access_code);
		req.verify_code = ntouv32(_verify_code);
		OUT_INFO(user._ip.c_str(), user._port, user._user_id.c_str(),
				"Send DownConnectReq,down-link state:CONNECT_WAITING_RESP");

		SendCrcData(user._fd, (const char*) &req, sizeof(req));
	} else {
		user._user_state = User::OFF_LINE;
	}
	user._last_active_time = time(0);
	user._login_time = time(0);
	user._connect_info.last_reconnect_time = time(0);
	if (user._connect_info.keep_alive == ReConnTimes)
		user._connect_info.reconnect_times--;

	return true;
}

// 客户对内纷发数据
void PasServer::HandleClientData(const char *data, const int len)
{
	if (len < (int) sizeof(Header) + (int) sizeof(Footer)) {
		OUT_ERROR( NULL, 0, NULL, "PasServer::HandleCasDownData packet len %d error", len);
		return;
	}

	Header *header = (Header *) data;
	unsigned int access_code = ntouv32(header->access_code);
	string str_access_code = uitodecstr(access_code);

	unsigned int msg_len = ntouv32(header->msg_len);
	if (msg_len != (unsigned int) len) {
		OUT_ERROR( NULL, 0, str_access_code.c_str(), "PasServer::HandleCasDownData packet len %d, msg length %d error",
				len, msg_len);
		return;
	}

	// 针对平台查岗类消息广播给所有平台
	if (ntouv16(header->msg_type) == DOWN_PLATFORM_MSG) {
		// 下发给所有平台
		vector<User> vec = _online_user.GetOnlineUsers();

		int size = vec.size();
		for (int i = 0; i < size; ++i) {
			User &user = vec[i];
			if (user._user_id.at(0) == 'U') {
				SendDataToPasUser(user._access_code, data, len);
			}
		}
	} else {
		// 通过WEB平台下发过来也需要记录对应接入码情况
		string mac_id = _proto_parse.GetMacId( data, len );
		if (!mac_id.empty()) {
			// 记录车号对应用接入码关系
			access_code = _corp_info_handler.get_access_code( mac_id ) ;
		}
		string info = _proto_parse.Decoder( data, len );

		if (SendDataToPasUser(access_code, data, len )) {
			OUT_SEND( NULL, 0, str_access_code.c_str(), "CAS:%s", info.c_str());
		} else {
			OUT_ERROR( NULL, 0, str_access_code.c_str(), "CAS:%s", info.c_str());
		}
	}
}

void PasServer::NoopWork()
{
	OUT_INFO( NULL, 0, NULL, "void	PasServer::NoopWork()");
	while (1) {
		if (!Check())
			break;

		HandleOnlineUsers(30);

		if (_login_check->Reload(30 * 3)) {
			OUT_INFO(NULL, 0, NULL, "Reload config file:%s success",
					_login_check->get_config_file().c_str());
		}
		sleep(3);
	}
}

void PasServer::TimeWork()
{
	OUT_INFO( NULL, 0, NULL, "void	PasServer::TimeWork()");

	time_t last = time(NULL);
	//做超时检测使用
	while (1) {
		if (!Check())
			break;

		time_t now = time(NULL);
		if (now - last > 30) {
			float count = 0, speed = 0;
			_allstat.GetFlux(count, speed);
			OUT_RUNNING( NULL, 0, "ONLINE", "total speed: %f, flux: %fkb, %s",
					count, (float)speed/(float)DF_KB, _datastat.GetFlux().c_str() );
			last = now;
		}

		HandleOfflineUsers();

		sleep(3);
	}
}

void PasServer::HandleOfflineUsers()
{
	vector<User> vec_users = _online_user.GetOfflineUsers(3 * 60);
	for (int i = 0; i < (int) vec_users.size(); i++) {
		User user = vec_users[i];
		if (user._socket_type == User::TcpClient) {
			// 移除统计功能的用户
			_datastat.Remove(user._access_code);

			if (user._fd != NULL) {
				OUT_WARNING( user._ip.c_str(), user._port,
						user._user_id.c_str(),
						"HandleOfflineUsers close socket fd %d", user._fd->_fd);
				CloseSocket(user._fd);
			}
		} else if (user._socket_type == User::TcpConnClient) {
			if (user._fd != NULL) {
				OUT_WARNING( user._ip.c_str(), user._port,
						user._user_id.c_str(),
						"TcpConnClient close socket fd %d", user._fd->_fd);
				//	user.show();
				CloseSocket(user._fd);
				user._fd = NULL;
			}

			if (ConnectServer(user, 3)) {
				//连接成功，重新加到在线列表当中。
				if (!_online_user.AddUser(user._user_id, user))
					_online_user.SetUser(user._user_id, user);
			} else if (user._connect_info.keep_alive == AlwaysReConn
					|| user._connect_info.reconnect_times > 0) {
				_online_user.AddUser(user._user_id, user);
			} else {
				if (user._connect_info.reconnect_times <= 0) {
					//  如果用户是下级平台，应该向下级平台发送从链路断开消息
					DownDisconnectInform msg;
					msg.header.msg_len = ntouv32(sizeof(DownDisconnectInform));
					msg.header.access_code = ntouv32(user._access_code);
					msg.header.msg_seq = ntouv32(_proto_parse.get_next_seq());
					msg.header.msg_type = ntouv16(DOWN_DISCONNECT_INFORM);
					msg.error_code = 0x0; //  原因:从链路断开

					char buf[200];
					sprintf(buf, "U_%u", user._access_code);
					//  取主链路fd
					User main_user = _online_user.GetUserByUserId(buf);

					if (!main_user._user_id.empty()) {
						if (SendCrcData(main_user._fd, (const char*) &msg, sizeof(msg))) {
							OUT_INFO( main_user._ip.c_str(), main_user._port, "pas", "DOWN_DISCONNECT_INFORM");
						} else {
							OUT_ERROR( main_user._ip.c_str(), main_user._port, "pas", "DOWN_DISCONNECT_INFORM");
						}
					}
				}
			}
		}
	}
}

void PasServer::HandleOnlineUsers(int timeval)
{
	if (time(0) - _last_check < timeval)
		return;
	_last_check = time(0);

	vector<User> vec_users = _online_user.GetOnlineUsers();
	for (int i = 0; i < (int) vec_users.size(); i++) {
		User &user = vec_users[i];
		if (user._socket_type == User::TcpConnClient) {
			DownLinkTestReq req;
			req.header.access_code = ntouv32(user._access_code);
			req.header.msg_seq = ntouv32(_proto_parse.get_next_seq());

			if (SendCrcData(user._fd, (const char*) &req, sizeof(req)))
				OUT_SEND(user._ip.c_str(),user._port,user._user_id.c_str(),"%s",_proto_parse.Decoder((const char*)&req,sizeof(req)).c_str() );
			else
				OUT_ERROR(user._ip.c_str(),user._port,user._user_id.c_str(),"%s",_proto_parse.Decoder((const char*)&req,sizeof(req)).c_str() );
		}
	}
}

bool PasServer::SendDataToPasUser(unsigned int access_code, const char *data, int len)
{
	char szid[128] = { 0 };
	sprintf(szid, "D_%u", access_code);
	User user = _online_user.GetUserByUserId(szid);
	if (user._user_id.empty() || user._user_state != User::ON_LINE) {
		sprintf(szid, "U_%u", access_code);
		user = _online_user.GetUserByUserId(szid);
	}

	//  判断消息中是否包含定位信息
	Header * pHeader = (Header *) data;
	unsigned short msg_type = ntouv16( pHeader->msg_type );
	// 修改接入码的信息
	pHeader->msg_seq = ntouv32( _proto_parse.get_next_seq() );
	pHeader->access_code = ntouv32( access_code );

	if (msg_type == DOWN_EXG_MSG) {
		ExgMsgHeader * pExgMsgHeader = (ExgMsgHeader *) (data + sizeof(Header));
		unsigned short usDataType = ntouv16(pExgMsgHeader->data_type);
		// 检测是否为地市级平台，如果不为市级平台不需要处理跨域
		switch (usDataType) {
		case DOWN_EXG_MSG_CAR_LOCATION: {
			if (user._user_state != User::ON_LINE) { // 如果用户不在线就直接保存了
				// 将下发失败的位置数据保存到文件中
				_filecache.WriteCache(access_code, (void*) data, len);
			}
		}
			break;
		}
	}

	// 转换编码后数据处理
	string info = _proto_parse.Decoder(data, len);
	// 发送数据
//	OUT_HEX( user._ip.c_str() , user._port , user._user_id.c_str(), data, len ) ;
	if (user._user_state == User::ON_LINE) {
		// 发送数据重新添加循环码的处理
		if (SendCrcData(user._fd, data, len)) {
			OUT_SEND( user._ip.c_str() , user._port , user._user_id.c_str(), "SendDataToPasUser %s ,access code %u" ,
					info.c_str() , access_code );
			return true;
		}
	}

	OUT_ERROR( user._ip.c_str() , user._port , user._user_id.c_str(), "SendDataToPasUser %s access code %u failed" ,
			info.c_str() , access_code );

	return false;
}

// 发送数据进行5B编码处理
bool PasServer::Send5BCodeData(socket_t *sock, const char *data, int len)
{
	C5BCoder coder;
	if (!coder.Encode(data, len)) {
		OUT_ERROR( sock->_szIp, sock->_port, NULL, "Send5BCodeData failed , socket fd %d", sock->_fd );
		return false;
	}
	return SendData(sock, coder.GetData(), coder.GetSize());
}

// 重组CRC的CODE
void PasServer::ResetCrcCode(char *data, int len)
{
	// 统一附加循环码的验证
	unsigned short crc_code = ntouv16( GetCrcCode( (char*)data, len ) );
	unsigned int offset = len - sizeof(Footer);

	// 替换循环码内存的位置数据
	memcpy(data + offset, &crc_code, sizeof(short));
}

// 发送重新处理循环码的数据
bool PasServer::SendCrcData(socket_t *sock, const char* data, int len)
{
	// 处理循环码
	char *buf = new char[len + 1];
	memset(buf, 0, len + 1);
	memcpy(buf, data, len);

	// 处理加密数据
	EncryptData((unsigned char*) buf, len, true);

	// 计算循环码处理,这里需要先加密后处理循环码校验
	ResetCrcCode(buf, len);

	// 添加5B的处理
	bool bSend = Send5BCodeData(sock, buf, len);

	delete[] buf;

	return bSend;
}

//-----------------------------缓存数据处理接口-----------------------------------------
// 处理外部数据
int PasServer::HandleQueue(const char *uid, void *buf, int len, int msgid)
{
	OUT_PRINT( NULL, 0, NULL, "HanldeQueue msg id %d , accesscode %s , data length %d" , msgid, uid, len );

	switch (msgid) {
	case DATA_FILECACHE: // 用户不在线数据缓存
	{
		// 如果数据长度不正确直接返回
		if (len < (int) sizeof(Header)) {
			OUT_ERROR( NULL, 0, NULL, "HandleQueue filecache data length %d error", len );
			return IOHANDLE_ERRDATA;
		}

		OUT_PRINT( NULL, 0, NULL, "HandleQueue %s" , _proto_parse.Decoder( (const char *)buf, len ).c_str() );

		Header *header = (Header *) buf;
		unsigned short msg_type = ntouv16(header->msg_type);
		// 如果不为扩展类消息
		if (msg_type != DOWN_EXG_MSG) {
			return IOHANDLE_ERRDATA;
		}

		// 如果为扩展类消息
		ExgMsgHeader *exg = (ExgMsgHeader*) ((const char *) (buf) + sizeof(Header));
		unsigned short data_type = ntouv16( exg->data_type );
		if (data_type != DOWN_EXG_MSG_CAR_LOCATION) {
			return IOHANDLE_ERRDATA;
		}

		DownExgMsgCarLocation *req = (DownExgMsgCarLocation *) buf;

		DownExgMsgHistoryArcossareaHeader msg;
		memcpy(&msg, buf, sizeof(msg));
		msg.exg_msg_header.data_length = ntouv32( sizeof(char) + sizeof(GnssData) );
		msg.exg_msg_header.data_type = ntouv16(DOWN_EXG_MSG_HISTORY_ARCOSSAREA);
		msg.header.msg_len = ntouv32( sizeof(msg) + sizeof(char) + sizeof(GnssData) + sizeof(Footer) );
		msg.cnt_num = 0x01;

		DataBuffer dbuf;
		dbuf.writeBlock(&msg, sizeof(msg));
		dbuf.writeBlock(&req->gnss, sizeof(GnssData));

		Footer footer;
		dbuf.writeBlock(&footer, sizeof(footer));

		if (!SendDataToPasUser(atoi(uid), dbuf.getBuffer(), dbuf.getLength())) {
			OUT_ERROR( NULL, 0, NULL, "DOWN_EXG_MSG_HISTORY_ARCOSSAREA:%s" , msg.exg_msg_header.vehicle_no );
			return IOHANDLE_FAILED;
		}

		OUT_SEND( NULL, 0, NULL, "DOWN_EXG_MSG_HISTORY_ARCOSSAREA:%s" , msg.exg_msg_header.vehicle_no );
	}
		break;
	}

	return IOHANDLE_SUCCESS;
}

