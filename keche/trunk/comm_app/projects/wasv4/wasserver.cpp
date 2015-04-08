/**********************************************
 * ClientAccessServer.cpp
 *
 *  Created on: 2010-7-12
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/
#include "wasserver.h"
#include "comlog.h"
#include <Base64.h>
#include <GBProtoParse.h>

#include "../tools/utils.h"

ClientAccessServer::ClientAccessServer( CacheDataPool &cache_data_pool )
	: _cache_data_pool(cache_data_pool),_recvstat(5)
{
	_gb_proto_handler = NULL ;
	_gb_proto_handler = GbProtocolHandler::getInstance() ;
	_queuemgr		  = NULL ;
	_queuemgr		  = NULL ;
	_thread_num 	  = 10 ;
	_online_count     = 0 ;
	_max_timeout      = 180 ;
	_max_pack_live    = 180 ;
	_secureType       = 1;
	_defaultOem       = "4C54";
}

ClientAccessServer::~ClientAccessServer()
{
	Stop() ;
	if ( _queuemgr != NULL ) {
		delete _queuemgr ;
		_queuemgr = NULL ;
	}
	if ( _gb_proto_handler != NULL ) {
		GbProtocolHandler::FreeHandler( _gb_proto_handler ) ;
		_gb_proto_handler = NULL ;
	}
}

bool ClientAccessServer::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	int nvalue = 0 ;

	// 添加对数据重发处理
	int nsend = 2 , ntime = 5 ;
	// 数据包重发次数
	if ( pEnv->GetInteger( "was_pack_resend", nvalue ) ) {
		nsend = nvalue ;
	}
	// 等待响应数据包超时时间
	if ( pEnv->GetInteger( "was_pack_timeout", nvalue ) ) {
		ntime = nvalue ;
	}
	_queuemgr = new CQueueMgr(this, ntime, nsend ) ;

	char szbuf[512] = {0};

	char szip[128] = {0} ;
	if ( ! pEnv->GetString("was_listen_ip" , szip) )
	{
		OUT_ERROR( NULL, 0,"ClientAccessServer", "Get was_listen_ip failed\n" ) ;
		return false ;
	}

	_listen_ip = szip;

	int port = 0 ;
	if ( ! pEnv->GetInteger( "was_listen_port", port ) )
	{
		OUT_ERROR( NULL, 0,"ClientAccessServer", "Get was_listen_port failed\n" ) ;
		return false ;
	}
	_listen_port = port;

	// 如果开启了UDP的监听就使用UDP
	if ( pEnv->GetInteger( "was_udp_port" , port ) ) {
		_udp_listen_port = port ;
		_udp_listen_ip   = szip ;
	}

	nvalue = 10 ;
	if ( pEnv->GetInteger( "was_tcp_thread", nvalue )  ){
		_thread_num = nvalue ;
	}

	// 取得用最大存活时间
	if ( pEnv->GetInteger( "was_user_time" , nvalue ) ) {
		_max_timeout = nvalue ;
	}

	// 添加数据包存活时间配置项
	if ( pEnv->GetInteger( "was_pack_live",  nvalue ) ) {
		_max_pack_live = nvalue ;
	}
	// 加载黑名单文件的路径
	if ( pEnv->GetString( "was_black_list", szbuf ) ) {
		_blackpath = szbuf ;
	}
	// 加载IP的列表的文件名
	if ( pEnv->GetString( "was_black_ip", szbuf ) ) {
		_ippath = szbuf ;
	}

	// 是否开启订阅数据
	if ( pEnv->GetInteger("was_tcpauth", nvalue ) ) {
		_secureType = nvalue;
	}

	if (pEnv->GetString("default_ome", szbuf)) {
		_defaultOem = szbuf;
	}

	// 初始化SCP的上传
	if ( ! _scp_media.Init( pEnv ) ) {
		OUT_ERROR( NULL, 0,"ClientAccessServer", "Init scp media failed" ) ;
		return false ;
	}

	// 设置分包对象
	setpackspliter( & _pack_spliter ) ;

	return true;
}

bool ClientAccessServer::Start( void )
{
	if ( ! _scp_media.Start() ) {
		OUT_ERROR( NULL, 0, "ClientAccessServer", "Start scp thread failed") ;
		return false ;
	}

	if ( _udp_listen_port > 0 ) {
		//  如果打开了UDP的数据传输就直接使用UDP处理
		 if ( ! StartUDP( _udp_listen_port , _udp_listen_ip.c_str() , _thread_num , _max_timeout ) ) {
			 OUT_ERROR( NULL, 0,"ClientAccessServer", "Start udp server failed, ip %s port %d" ,
					 _udp_listen_ip.c_str(), _udp_listen_port ) ;
			 return false ;
		 }
	}
	return StartServer( _listen_port, _listen_ip.c_str(), _thread_num , _max_timeout ) ;
}

void ClientAccessServer::Stop( void )
{
	StopServer() ;
	// 停止SCP线程
	_scp_media.Stop() ;
}

void ClientAccessServer::TimeWork()
{
	//首先做的一件事就是加载用户信息列表
	while (1)
	{
		if ( ! Check() ) break ;

		if ( ! _blackpath.empty() ) {
			// 加载黑名单的数据
			_blacklist.LoadBlack( _blackpath.c_str() ) ;
		}
		// 加载IP黑名单列表
		if ( ! _ippath.empty() ) {
			_ipblack.LoadIps( _ippath.c_str() ) ;
		}
		// 处理长数包的超时
		_pack_mgr.CheckTimeOut(_max_pack_live) ;
		// 检测媒体数据超时管理
		_scp_media.CheckTimeOut() ;

		sleep(5);
	}
}

void ClientAccessServer::NoopWork()
{
	time_t last_show = 0 ;
	while(1) {

		if ( ! Check() ) break ;

		time_t now = time(NULL) ;

		vector<User> vec = _online_user.GetOnlineUsers();
		if ( vec.empty()) {
#ifdef _GPS_STAT
			if ( now - last_show > 30 ) {
				last_show = now ;
				// 统计当前在线车辆数
				OUT_RUNNING( NULL, 0, "ONLINE" , "online user count 0, recv flux 0kb, gps data count: %d" , _pEnv->GetGpsCount() ) ;
			}
#endif
			sleep(5);
			continue;
		}

		map<socket_t*,int>  mapref ;  // 记录连接使用情况
		map<socket_t*,int>::iterator it ;

		// 在线用户统计
		int count = 0 ;
		// 检测连接状态
		for ( int i = 0 ; i < (int)vec.size(); ++ i ) {
			User &user = vec[i] ;

			int flag = 0 ;
			if ( now - user._last_active_time > _max_timeout ) {
				// 断开连接打印一条日志
				OUT_WARNING( user._ip.c_str(), user._port, user._user_id.c_str(), "Delete User fd %d" , user._fd->_fd ) ;

				// 2013-02-20,ycq
				bool bnotify = true ;  // 是否要发送下线通知
				if (strncmp(user._user_id.c_str(), "UDP_", 4) == 0) {
					// 如果为UDP先检测TCP连接是否存在如果存就不发下线通知
					User temp = _online_user.GetUserByUserId(user._user_id.substr(4));
					if ( ! temp._user_id.empty()) {
						bnotify = false;
					}
				}
				if (bnotify) {
					string caits = "CAITS 0_0 " + user._user_name + " 0 D_CONN {TYPE:0} \r\n";
					_pEnv->GetMsgClient()->HandleUpData(caits.c_str(), caits.length());
				}

				// 删除用户的ID
				_online_user.DeleteUser(user._user_id);
			} else {
				flag = 1 ; ++ count ;
			}

			// 处理用户链接
			if ( user._fd != NULL ) {
				// 如果处理连接是否还在使用
				it = mapref.find( user._fd ) ;
				if ( it == mapref.end() ) {
					mapref.insert( pair<socket_t*,int>(user._fd, flag ) ) ;
				} else {
					// 如果连接使用情况大于零则说有其它在用
					it->second = it->second + flag ;
				}
			}
		}

#ifdef _GPS_STAT
		if ( now - last_show > 30 ) {
			last_show = now ;
			 // 处理GPS统计数据
			// 统计当前在线车辆数
			OUT_RUNNING( NULL, 0, "ONLINE" , "online user count %d, recv flux %fkb, gps data count: %d" ,
						count , _recvstat.GetFlux() / FLUX_KB , _pEnv->GetGpsCount());
		}
#else
		// 统计当前在线车辆数
		//OUT_WARNING( NULL, 0, "ONLINE" , "online user count %d, recv flux %fkb" , count , _recvstat.GetFlux() / FLUX_KB ) ;
#endif
		// 在线车辆个数
		_online_count = count ;

		if ( mapref.empty() )  continue ;
		// 回收连接
		for ( it = mapref.begin(); it != mapref.end(); ++ it ) {
			// 如果连接有其它在使用
			if ( it->second > 0 )
				continue ;
			// 如果引用数为１则直接关闭连接
			CloseSocket( it->first ) ;
		}
		sleep(5);
	}
}

// 从BCD码中取得手机号
static bool getphonenum( char *bcd , string &phone )
{
	string temp = BCDtostr(bcd) ;
	if ( temp.length() == 0 || temp.length() < 12 )
		return false ;
	phone = temp.substr( 1, 11 ) ;
	return true ;
}

void ClientAccessServer::on_data_arrived( socket_t *sock , const void *data, int len)
{
	OUT_HEX( sock->_szIp, sock->_port, "RECV", (const char *)data, len ) ;
	// 添加到流量统计中
	_recvstat.AddFlux( len ) ;
	/*
	 * 首先能够处理多个整包的情况，而不能处理那种半截包的情况，这个需要后续的完善。
	 */
	const char *ip      = sock->_szIp ;
	unsigned short port = sock->_port ;

	const int min_len = int(sizeof(GBheader) + sizeof(GBFooter));
	const int max_len = int((min_len + sizeof(MsgPart) + 0x3ff) * 2);
	if(len < min_len || len > max_len) {
		OUT_WARNING(ip, port, NULL, "invalid message, drop it! length %d", len) ;
		return;
	}

	vector<unsigned char> dstBuf;
	unsigned char *dstPtr;
	size_t dstLen;

	dstBuf.resize(len);
	dstPtr = &dstBuf[0];
	dstLen = Utils::deCode808((unsigned char*)data, len, dstPtr);

	handle_one_packet( sock, (char*)dstPtr, dstLen);
}

void ClientAccessServer::handle_one_packet( socket_t *sock ,const char *data,int len)
{
	GBheader *header = (GBheader*)data;
	const char *ip 		= sock->_szIp ;
	unsigned short port = sock->_port ;

	string str_car_id ;
	if ( ! getphonenum( header->car_id, str_car_id ) ) {
		OUT_ERROR( ip, port ,"ClientAccessServer", "Handle_one_packet get phone num failed" ) ;
		OUT_HEX( ip, port, str_car_id.c_str() , data , len ) ;
		// 检测FD对象
		return ;
	}

	// 如果在黑名单的用户中，就直接关闭连接不处理数据
	if ( _blacklist.OnBlack( str_car_id.c_str() ) ) {
		OUT_ERROR( ip, port, str_car_id.c_str(), "user in black list close socket fd %d", sock->_fd ) ;
		CloseSocket( sock ) ;
		return ;
	}

	unsigned short _s = 0x03FF;
	unsigned short nmsg = 0 ;
	memcpy(&nmsg,&(header->msgtype),sizeof(unsigned short));
	nmsg = ntohs( nmsg ) ;

	// 取得前10位值为长度
	unsigned short msg_len = nmsg & _s;
	unsigned short msg_id  = ntohs(header->msgid);
	unsigned short seq     = ntohs(header->seq);

	int need_len = msg_len  + sizeof(GBFooter) + sizeof(GBheader) ;
	// 优先计算长度是否正确
	if ( len < need_len ) {
		OUT_ERROR( ip, port, str_car_id.c_str(), "msg id %x, data length error, len %d, msg len %d, need length %d" ,
				msg_id, len , msg_len , need_len ) ;
		// 检测FD对象
		return ;
	}

	// 长数据包的BUF
	DataBuffer dbuf ;
	// 如果第13位为1则为分包消息
	if ( nmsg & 0x2000 ) {
		// 如果为分包消息
		if ( len != (int)( msg_len + sizeof(GBFooter) + sizeof(GBheader) + sizeof(MediaPart)) ){
			OUT_ERROR( ip, port, str_car_id.c_str(), "msg id %x, long message data length error, len %d, msg len %d,need length %d" ,
					msg_id, len , msg_len ,need_len + sizeof(MediaPart) ) ;
			// 检测FD对象
			return ;
		}

		// 分包结构
		MediaPart *part = (MediaPart *) ( data + sizeof(GBheader) ) ;
		// 内存中组包处理
		if ( ! _pack_mgr.AddPack( dbuf, str_car_id.c_str(), msg_id, ntohs( part->seq ) , ntohs( part->total ) , seq , data, len )  ) {
			// 如果数据包还没组完就只返回成功的响应
			unsigned short downseq = _pEnv->GetSequeue(str_car_id.c_str());
			if(downseq!=0xffff){
				PlatFormCommonResp resp = _gb_proto_handler->BuildPlatFormCommonResp( header, downseq, 0 ) ;
				SendResponse( sock, str_car_id.c_str(), (const char *)&resp, sizeof(resp) ) ;
			}
			return ;
		}
	} else { // 如果是普通消息
		if ( len != (int)( msg_len  + sizeof(GBFooter) + sizeof(GBheader)) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "msg id %x, message length error, len %d, msg len %d, need length %d" ,
					msg_id, len , msg_len , need_len ) ;
			// 检测FD对象
			return ;
		}
	}

	if(dbuf.getLength() > 0) {
		data = dbuf.getBuffer();
		len = dbuf.getLength();
	}

	processMsgGb808(sock, data, len, str_car_id);
}

void ClientAccessServer::processMsgGb808(socket_t *sock ,const char *data, int len, const string &str_car_id)
{
	const char *ip 		= sock->_szIp ;
	unsigned short port = sock->_port ;

	GBheader *header       = (GBheader*) data ;
	unsigned short msg_id  = ntohs(header->msgid);
	unsigned int   msg_len = len - sizeof(GBheader) - sizeof(GBFooter) ;

	// 取得当前时间
	time_t now = time( NULL ) ;
	string caits_car_id = "" ;

	User user;
	string key = str_car_id ;
	// 如果为UDP用户数据则加上前缀码
	if ( check_udp_fd( sock ) ) {
		//收到udp数据，更新tcp模式会话超时时间
		user = _online_user.GetUserByUserId(key);
		if ( ! user._user_id.empty()) {
			user._last_active_time = time(NULL);
			_online_user.SetUser(key, user);
		}

		key = "UDP_" + str_car_id ;
	}

	if (msg_id == 0x0100) { //终端注册
		TermRegisterHeader *reg_h = (TermRegisterHeader*) data;
		if (len < (int) (sizeof(TermRegisterHeader) + sizeof(GBFooter))) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "msg id %x, message length %d error" , msg_id, len );
			return;
		}

		int carnolen = msg_len + sizeof(GBheader) - sizeof(TermRegisterHeader);
		if (carnolen < 0 || carnolen > 32) {
			return;
		}
		carnolen = strnlen(data + sizeof(TermRegisterHeader), carnolen);

		string provinceID;
		string cityID;
		string producerID;
		string termType;
		string termID;
		string plateColor;
		string plateNum;

		Utils::int2str(ntohs(reg_h->province_id), provinceID);
		Utils::int2str(ntohs(reg_h->city_id), cityID);
		producerID.assign((char*) reg_h->corp_id, strnlen((char*) reg_h->corp_id, 5));
		termType.assign((char*) reg_h->termtype, strnlen((char*) reg_h->termtype, 20));
		termID.assign((char*) reg_h->termid, strnlen((char*) reg_h->termid, 7));
		Utils::int2str(reg_h->carcolor, plateColor);
		plateNum.assign(data + sizeof(TermRegisterHeader), carnolen);

		string filter = string(":,{} \r\n\0", 8);
		producerID = Utils::filter(producerID, filter);
		termType = Utils::filter(termType, filter);
		termID = Utils::filter(termID, filter);
		plateNum = Utils::filter(plateNum, filter);

		string oem = "0000";
		unsigned char result = 2;
		string verifyCode = "";

		if (_secureType == 0) {
			oem = _defaultOem;
			result = 0;
			verifyCode = "1234567890";
		} else {
			string fieldStr;
			vector<string> fieldVec;

			_pEnv->GetRedisCache()->HGet("KCTX.SECURE", str_car_id.c_str(), fieldStr);
			Utils::splitStr(fieldStr, fieldVec, ',');
			if (fieldVec.size() < 8 || fieldVec[7] != "0") {
				result = 4;
			} else if(_secureType == 1 && termID == fieldVec[2]) {
				oem = fieldVec[6];
				result = 0;
				verifyCode = fieldVec[0];
			} else if(_secureType == 2 && reg_h->carcolor != 0 && plateNum == fieldVec[4]) {
				oem = fieldVec[6];
				result = 0;
				verifyCode = fieldVec[0];
			} else if(_secureType == 2 && reg_h->carcolor == 0 && plateNum == fieldVec[5]) {
				oem = fieldVec[6];
				result = 0;
				verifyCode = fieldVec[0];
			} else if(_secureType == 3 && plateColor == fieldVec[3] && plateNum == fieldVec[4]) {
				oem = fieldVec[6];
				result = 0;
				verifyCode = fieldVec[0];
			} else if(_secureType == 3 && reg_h->carcolor == 0 && plateNum == fieldVec[5]) {
				oem = fieldVec[6];
				result = 0;
				verifyCode = fieldVec[0];
			} else if(_secureType == 4 && termID == fieldVec[2] && reg_h->carcolor != 0 && plateNum == fieldVec[4]) {
				oem = fieldVec[6];
				result = 0;
				verifyCode = fieldVec[0];
			} else if(_secureType == 4 && termID == fieldVec[2] && reg_h->carcolor == 0 && plateNum == fieldVec[5]) {
				oem = fieldVec[6];
				result = 0;
				verifyCode = fieldVec[0];
			} else if(_secureType == 5) {
				oem = fieldVec[6];
				result = 0;
				verifyCode = fieldVec[0];
			}
		}

		caits_car_id = oem + "_" + str_car_id;
		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT {TYPE:36";
		caits += ",40:" + provinceID + ",41:" + cityID + ",42:" + producerID;
		caits += ",43:" + termType + ",44:" + termID + ",45:" + string(1, result + '0');
		caits += ",202:" + plateColor + ",104:" + plateNum + "} \r\n";

		_pEnv->GetMsgClient()->HandleUpData(caits.c_str(), caits.length());

		vector<unsigned char> respBuf;
		TermRegisterRespHeader *respPtr;
		GBFooter *footer;

		respBuf.resize(sizeof(TermRegisterRespHeader) + verifyCode.size() + sizeof(GBFooter));
		respPtr = (TermRegisterRespHeader*) &respBuf[0];
		footer = (GBFooter*) (&respBuf[0] + sizeof(TermRegisterRespHeader) + verifyCode.size());

		uint16_t msgType = htons(3 + verifyCode.size());

		respPtr->header.begin._begin = 0x7e;
		respPtr->header.msgid = htons(0x8100);
		memcpy(&respPtr->header.msgtype, &msgType, sizeof(uint16_t));
		memcpy(respPtr->header.car_id, header->car_id, 6);
		respPtr->header.seq = 0;
		respPtr->resp_seq = header->seq;
		respPtr->result = result;
		memcpy(&respBuf[0] + sizeof(TermRegisterRespHeader), verifyCode.c_str(), verifyCode.size());
		footer->check_sum = _gb_proto_handler->get_check_sum((char*) &respBuf[0] + 1, respBuf.size() - 3);
		footer->ender._end = 0x7e;

		/*******发送数据****************/
		SendResponse(sock, str_car_id.c_str(), (char*) &respBuf[0], respBuf.size());

		return;
	} else if (msg_id == 0x0102) { //终端鉴权
		string oem = "0000";
		int result = 1;
		string authCode(data + sizeof(GBheader), strnlen(data + sizeof(GBheader), msg_len));

		if (_secureType == 0) {
			oem = _defaultOem;
			result = 0;
		} else {
			string fieldStr;
			vector<string> fieldVec;

			_pEnv->GetRedisCache()->HGet("KCTX.SECURE", str_car_id.c_str(), fieldStr);
			Utils::splitStr(fieldStr, fieldVec, ',');
			if (fieldVec.size() >= 8 && fieldVec[7] == "0" && authCode == fieldVec[0]) {
				oem = fieldVec[6];
				result = 0;
			}
		}

		caits_car_id = oem + "_" + str_car_id;
		if (result == 0) {
			user._fd = sock;
			user._user_id = key;
			user._user_name = caits_car_id; // 用户名为带OEM码的值
			user._ip = (ip == NULL) ? "0.0.0.0" : ip;
			user._port = port;
			user._login_time = now;
			user._user_state = User::ON_LINE;
			user._last_active_time = now;

			if (!_online_user.AddUser(key, user)) {
				_online_user.DeleteUser(key);
				_online_user.AddUser(key, user);
			}
		}

		_pEnv->ResetSequeue( str_car_id.c_str() ) ;

		unsigned short downseq = _pEnv->GetSequeue(str_car_id.c_str());
		PlatFormCommonResp resp = _gb_proto_handler->BuildPlatFormCommonResp(header, downseq, result);
		SendResponse(sock, str_car_id.c_str(), (const char *) &resp, sizeof(resp));

		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT {TYPE:38";
		caits += ",47:" + authCode + ",48:" + string(1, result + '0') + "} \r\n";
		_pEnv->GetMsgClient()->HandleUpData(caits.c_str(), caits.length());

		return;
	}

	// 直接通过用户的KEY来查找提高效率
	user = _online_user.GetUserByUserId(key);
	if (!user._user_id.empty()) {
		user._last_active_time = now;

		// 处理FD的连接
		if (user._fd != sock) {
			// 关闭上一个用户的连接
			// CloseSocket( user._fd ) ;
			OUT_ERROR( ip, port, str_car_id.c_str(), "fd %d not equal socket old fd %d" , sock->_fd, user._fd->_fd );
			_online_user.DeleteUser(user._fd);

			// 添加新的连接
			user._fd = sock;
			user._login_time = now;
			user._ip = sock->_szIp;
			user._port = sock->_port;
		}
		// 添加新用户处理
		_online_user.SetUser(key, user);
	} else if (_secureType == 0) {
		user._fd = sock;
		user._user_id = key;
		user._user_name = _defaultOem + "_" + str_car_id; // 用户名为带OEM码的值
		user._ip = (ip == NULL) ? "0.0.0.0" : ip;
		user._port = port;
		user._login_time = now;
		user._user_state = User::ON_LINE;
		user._last_active_time = now;

		_online_user.AddUser(key, user);
	} else if(check_udp_fd( sock )) {
		user = _online_user.GetUserByUserId(str_car_id);
		if(user._user_id.empty()) {
			OUT_WARNING( ip, port, str_car_id.c_str(), "term no auth");
			return;
		}

		user._fd = sock;
		user._user_id = key;
		user._ip = (ip == NULL) ? "0.0.0.0" : ip;
		user._port = port;
		user._login_time = now;
		user._user_state = User::ON_LINE;
		user._last_active_time = now;

		_online_user.AddUser(key, user);
	} else {
		OUT_WARNING( ip, port, str_car_id.c_str(), "term no auth");
		return;
	}

	unsigned char result   = 0;
	bool bresp 			   = false ;
	unsigned short respid  = 0 ;

	caits_car_id = user._user_name;

	if ( msg_id == 0x0101 ||  msg_id == 0x0003 ) {
		// 终端注销
		_online_user.DeleteUser( key ) ;

		OUT_PRINT( ip, port, str_car_id.c_str(), "user unregister fd %d" , sock->_fd ) ;
		// 需要转发存储

		char buf[1024] = {0};
		sprintf( buf, "CAITS 0_0 %s 0 U_REPT {TYPE:37,46:%d} \r\n" , caits_car_id.c_str() , result ) ;

		// 处理注销事件
		_pEnv->GetMsgClient()->HandleUpData( buf , strlen(buf) ) ;

		result = 0x00 ;
		// 需要处理通用应答
		bresp  = true ;
	} else if(msg_id == 0x0200) { //终端位置信息汇报
		if ( msg_len < (unsigned int)sizeof(GpsInfo) || len < (int)sizeof(TermLocationHeader) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "TermLocationHeader data length %d error" , msg_len ) ;
			return ;
		}

		TermLocationHeader *_location_header = (TermLocationHeader*)data;
		// 处理位置上报数据
		string caits = "CAITS 0_1 " + caits_car_id + " 0 U_REPT {TYPE:0," ;

		//数据长度正确
		char *append_data = NULL;
		int append_data_len = 0;

		append_data = (char *)data+sizeof(TermLocationHeader);
		append_data_len = len -  sizeof(TermLocationHeader) - 2;
		caits += _gb_proto_handler->ConvertGpsInfo(&(_location_header->gpsinfo),append_data,append_data_len) ;
		// 添加结束
		caits += "} \r\n";

		_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;

		result = 0x00 ;
		bresp  = true ;
	} else if (msg_id == 0x0702) { //驾驶员身份信息采集上报
		if ( msg_len < 60 ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "0x0702 mesage length %d error", len ) ;
			return ;
		}

		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT ";

		//数据长度正确
		char * msgbody_data = NULL;
		string dstr = "";
		msgbody_data = (char *)data+sizeof(GBheader);
		dstr = _gb_proto_handler->ConvertDriverInfo(msgbody_data,msg_len, result);
		if( dstr.length() > 5 ){
			caits +=  dstr + " \r\n";

			OUT_DEBUG("updata:%s\n",caits.c_str());

			_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;
		}
		// 针对驾驶员身份识别特殊处理，
		result = 0 ;
		// 需要处理通用应答
		bresp  = true ;
	} else if (msg_id == 0x0104) { //查询终端参数应答
		if ( len < (int)sizeof(QueryTermParamAckHeader) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "QueryTermParamAckHeader mesage length %d error", len ) ;
			return ;
		}

		string caitr = "";
		QueryTermParamAckHeader *rsp =  ( QueryTermParamAckHeader *) data ;

		respid = ntohs(rsp->respseq) ;

		string key = str_car_id+"_"+uitodecstr(0x8104)+"_"+uitodecstr( respid );
		CacheData cache_data = _cache_data_pool.checkData(key);
		if( respid == 0xffff) {
			string content = "";
			/******未超时*******/
			if(_gb_proto_handler->ConvertGetPara( (char*)data, len, content)) {
				/*****数据解析正确****/
				//OUT_WARNING(ip,port,str_car_id.c_str(),"ConvertGetPara is fail car_id=%d",car_id);//处理carid
				caitr = "CAITR 0_0 " + caits_car_id + " 0 D_GETP "+content+" \r\n";
			} else {
				/*****数据解析错误****/
				caitr = "CAITR 0_0 " + caits_car_id + " 0 D_GETP {RET:1} \r\n";
			}
			_pEnv->GetMsgClient()->HandleUpData( caitr.c_str(), caitr.length() );
		} else if (cache_data.str_send_msg == "EXIT" ) {
			string content = "" ;
			/******未超时*******/
			if(_gb_proto_handler->ConvertGetPara( (char*)data, len, content)){
				/*****数据解析正确****/
				//OUT_WARNING(ip,port,str_car_id.c_str(),"ConvertGetPara is fail car_id=%d",car_id);//处理carid
				caitr = "CAITR " + cache_data.seq + " " + cache_data.mac_id + " 0 D_GETP "+content+" \r\n";
			} else {
				/*****数据解析错误****/
				caitr = "CAITR " + cache_data.seq + " " + cache_data.mac_id + " 0 D_GETP {RET:1} \r\n";
			}
			_pEnv->GetMsgClient()->HandleUpData( caitr.c_str(), caitr.length() ) ;
		} else {
			// 超时
			//OUT_WARNING(ip,port,str_car_id.c_str(),"msg type: %x, msg id : 0x8104, cant's find seq:%x in cache_data_pool", msg_id, respid );
			sendOtherCmd(caits_car_id, msg_id, data + sizeof(GBheader), msg_len);
		}

		result = 0x00 ;
		// 需要处理通用应答
		bresp  = true ;
	} else if ( msg_id == 0x0002 ) { //9.3　终端心跳
		// 如果只要有心跳则正常
		string caits = "CAITS 0_0 " + user._user_name + " 0 U_CONN {TYPE:1} \r\n";
		_pEnv->GetMsgClient()->HandleUpData( caits.c_str(),caits.length() );

		// 需要处理通用应答
		result = 0x00 ;
		bresp  = true ;
	} else if(msg_id==0x0201) { //位置信息查询
		if ( len < (int)sizeof(_TermLocationRespHeader) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "_TermLocationRespHeader mesage length %d error", len ) ;
			return ;
		}

		_TermLocationRespHeader *_location_resp_header = (_TermLocationRespHeader*)data;
		char *append_data = NULL;
		int append_data_len = 0;
		/***判断是否有附加数据***/
		if(msg_len > sizeof(GpsInfo) + 2) {
			append_data = (char *)data+sizeof(_TermLocationRespHeader);
			append_data_len = msg_len -  (sizeof(GpsInfo) + 2);
		}

		respid = ntohs(_location_resp_header->reqseq) ;

		//printf("get location：C\n");
		string caitr   = "";
		string key = str_car_id+"_"+uitodecstr(0x8201)+"_"+uitodecstr(respid);
		//printf("get loc keyid :%s\n",key.c_str());
		CacheData cache_data = _cache_data_pool.checkData(key);
		if (cache_data.str_send_msg == "EXIT" ){
			//printf("get location：D\n");
			/******未超时*******/
			string content = _gb_proto_handler->ConvertGpsInfo(&(_location_resp_header->gpsinfo),append_data,append_data_len);
			if(content.length() > 0) {
				//printf("get location：E\n");
				/*****数据解析正确****/
				caitr = "CAITS " + cache_data.seq + " " + cache_data.mac_id + " 201 U_REPT {TYPE:0,RET:0,"+content+"} \r\n";
			} else {
				/*****数据解析错误****/
				caitr = "CAITS " + cache_data.seq + " " + cache_data.mac_id + " 0 U_REPT {TYPE:0,RET:1} \r\n";
			}

			OUT_DEBUG("get location：%s\n",caitr.c_str());

			_pEnv->GetMsgClient()->HandleUpData( caitr.c_str(), caitr.length() ) ;
		} else {
			//OUT_WARNING(ip,port,str_car_id.c_str(),"msg type: %x, msg id: 0x8201, cant's find seq:%x in cache_data_pool", msg_id, respid );
			sendOtherCmd(caits_car_id, msg_id, data + sizeof(GBheader), msg_len);
		}
		// 需要处理通用应答
		result = 0x00 ;
		bresp  = true ;
	}
	else if(msg_id == 0x0001) { //终端通用应答
		if ( len < (int)sizeof(TermCommonResp) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "TermCommonResp mesage length %d error", len ) ;
			return ;
		}

		TermCommonResp *resp = (TermCommonResp*)data;
		//key=carid_msgid_seq
		unsigned short rmsgid  = ntohs(resp->resp.resp_msg_id) ;
		respid  = ntohs(resp->resp.resp_seq ) ;

		string key = str_car_id+"_"+ustodecstr(rmsgid) + "_" + uitodecstr(respid);
		CacheData cache_req = _cache_data_pool.checkData(key);
		if(cache_req.str_send_msg == "EXIT" ) {
			//组成通用回复包。
			string caitr = "CAITR " + cache_req.seq + " " + cache_req.mac_id + " 0 " + cache_req.command + " ";
			caitr += "{RET:"+ chartodecstr(resp->resp.result) +"} \r\n";
			_pEnv->GetMsgClient()->HandleUpData( caitr.c_str(), caitr.length() ) ;

			OUT_DEBUG("terminal response:%s\n",caitr.c_str());

		} else {
			//OUT_WARNING(ip,port,str_car_id.c_str(),"msg type: %x, msg id: %x, cant's find seq:%x in cache_data_pool", msg_id, rmsgid, respid );
			sendOtherCmd(caits_car_id, msg_id, data + sizeof(GBheader), msg_len);
		}
	}
	else if(msg_id == 0x0301) { //事件报告
		if ( msg_len < 1 ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "0x0301 mesage length %d error", len ) ;
			return ;
		}

		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT ";

		//数据长度正确
		unsigned char  eventch = data[sizeof(GBheader)];
		caits = caits +"{TYPE:31,81:"+ chartodecstr(eventch)+"} \r\n";

		OUT_DEBUG("updata:%s\n",caits.c_str());

		_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;

		result = 0x00 ;

		// 需要处理通用应答
		bresp  = true ;
	} else if(msg_id == 0x0302) { //提问应答
		if ( len < (int)sizeof(QuestionReplyAsk) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "QuestionReplyAck mesage length %d error", len ) ;
			return ;
		}

		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT ";
		//数据长度正确
		QuestionReplyAsk *ack = ( QuestionReplyAsk *)( data ) ;

		respid = ntohs( ack->seq );

		caits = caits +"{TYPE:32,84:"+ uitodecstr(respid)+",82:"+chartodecstr(ack->id)+"} \r\n";

		OUT_DEBUG("updata:%s",caits.c_str());

		_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;

		result = 0x00 ;

		// 需要处理通用应答
		bresp  = true ;
	} else if(msg_id == 0x0303) { //信息点播/取消
		if ( len < (int)sizeof(InfoDemandCancleAck) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "InfoDemandCancleAck mesage length %d error", len ) ;
			return ;
		}

		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT ";

		InfoDemandCancleAck *ack = ( InfoDemandCancleAck * ) data ;
		//数据长度正确
		caits = caits +"{TYPE:33,83:"+ chartodecstr( ack->info_type )+"|"+chartodecstr( ack->info_mark )+"} \r\n";

		OUT_DEBUG("updata:%s",caits.c_str());

		_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;

		result = 0x00 ;

		// 需要处理通用应答
		bresp  = true ;
	} else if ( msg_id == 0x0500 ) { // 车辆控制应答，主要车门锁与不锁
		if ( len < (int)sizeof(TermLocationRespHeader) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "TermLocationRespHeader mesage length %d error", len ) ;
			return ;
		}

		TermLocationRespHeader *rsp = ( TermLocationRespHeader *) data ;

		respid = ntohs( rsp->reqseq ) ;
		// 处理车辆控制应答
		string content = _gb_proto_handler->ConvertGpsInfo(&(rsp->gpsinfo),NULL, 0 );
		if(content.length() > 0) {
			/*****回应数据车门是否锁了****/
			string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT {TYPE:39,RET:0,"+content+"} \r\n";
			// 上传应答处理
			_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;

			// 需要处理通用应答
			result = 0x00 ;
		}

		bresp  = true ;
	} else if(msg_id == 0x0901) { //数据压缩上传
		if ( msg_len < 4 ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "0x0901 mesage length %d error", len ) ;
			return ;
		}

		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT ";
		unsigned int datalen = 0;

		//数据长度正确
		memcpy(&datalen,data+sizeof(GBheader),4);
		datalen = ntohl(datalen);

		string dstr = "";

		char transdata[1500] = {0};
		memcpy(transdata,data+sizeof(GBheader)+4,msg_len-4);
		transdata[msg_len-4]=0;

		OUT_DEBUG("GZipdata:%s\n",transdata);

		CBase64 base64;
		base64.Encode( transdata, msg_len-4 );

		caits = caits +"{TYPE:14,93:"+uitodecstr(datalen)+",92:"+ base64.GetBuffer()+"} \r\n";

		_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;

		result = 0x00 ;

		// 需要处理通用应答
		bresp  = true ;
	} else if(msg_id == 0x0700) { //行驶记录仪数据上传
		if ( msg_len < (unsigned int)sizeof(TachographBody) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "TachographBody mesage length %d error", len ) ;
			return ;
		}

		TachographBody *body = ( TachographBody *) ( data + sizeof(GBheader) ) ;

		respid = ntohs(body->seq) ;

		string sseq = "0_0" ;
		string key  = str_car_id+"_"+ustodecstr(0x8700) + "_" + uitodecstr( respid );
		CacheData cache_req = _cache_data_pool.checkData(key);
		if(cache_req.str_send_msg == "EXIT" ) {
			sseq = cache_req.seq ;
		} else {
			sendOtherCmd(caits_car_id, msg_id, data + sizeof(GBheader), msg_len);
		}

		// ToDo: 上传行驶记录	2	记录仪数据
		string caits = "CAITS "+sseq+" " + caits_car_id + " 0 U_REPT ";

		const char *pdata = ( const char *) ( data + sizeof(GBheader) + sizeof(TachographBody) ) ;

		CBase64 base64 ;
		base64.Encode( pdata , msg_len - 3 ) ;

		// 针对行驶记录记录暂时打包上传就行
		caits = caits +"{TYPE:2,70:"+uitodecstr(body->cmd)+",61:"+ base64.GetBuffer()+"} \r\n";

		_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;
	} else if(msg_id == 0x0701) { //电子运单上报
		if ( len < (int)sizeof(EWayBillReportAckHeader) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "EWayBillReportAckHeader mesage length %d error", len ) ;
			return ;
		}

		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT ";

		//数据长度正确
		EWayBillReportAckHeader *ack = ( EWayBillReportAckHeader *) data ;
		unsigned int datalen = ntohl( ack->length );
		if( datalen + 4 == msg_len )
		{
			string dstr = "";

			char transdata[1500] = {0};
			memcpy( transdata, data+sizeof(EWayBillReportAckHeader), datalen ) ;
			transdata[datalen]=0;

			OUT_DEBUG("transdata:%s\n",transdata);

			CBase64 base64;
			base64.Encode( transdata, datalen );

			caits = caits +"{TYPE:35,87:"+ base64.GetBuffer()+"} \r\n";

			OUT_DEBUG("updata:%s",caits.c_str());

			_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;

			result = 0x00 ;
		}

		// 需要处理通用应答
		bresp  = true ;
	} else if( msg_id == 0x0900 ) {//数据透传-----
		if ( msg_len < 1 ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "0x0900 mesage length %d error", msg_len ) ;
			return ;
		}

		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT ";  // ToDo:

		//数据长度正确
		unsigned short dlen = msg_len -1;
		unsigned char dtype = 0;

		string dstr = "";
		dtype = data[sizeof(GBheader)];

		vector<char> datBuf(dlen + 1);
		char *transdata = &datBuf[0];
		//char transdata[1500] = {0};
		memcpy(transdata,data+sizeof(GBheader)+1,dlen);
		transdata[dlen]=0;
		//printf("transdata:%s\n",transdata);

		CBase64 base64;
		base64.Encode(transdata,dlen) ;
		caits = caits +"{TYPE:9,91:"+ustodecstr(dtype)+",90:"+ base64.GetBuffer()+"} \r\n";

		//printf("convert transdata:%s\n",transdata);
		_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;

		result = 0x00 ;

		// 需要处理通用应答
		bresp  = true ;
	} else if(msg_id == 0x0800) { //多媒体事件信息上传
		if ( len < (int)sizeof(EventReort) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "EventReort mesage length %d error", len ) ;
			return ;
		}

		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT {TYPE:39,120:";

		//数据长度正确
		EventReportEx *report = ( EventReportEx *) ( data + sizeof(GBheader) ) ;

		//多媒体事件ID
		unsigned int dword = ntohl(report->id);
		caits += uitodecstr(dword)+",";
		//多媒体类型
		caits += "121:"+ustodecstr(report->type)+",";
		//多媒体格式编码
		caits += "122:"+ustodecstr(report->mtype)+",";
		//事件项编码
		caits += "123:"+ustodecstr( ( report->event & 0x7f ) )+",";
		//通道ID
		caits += "124:"+ustodecstr(report->chanel);

		// 如果事件最高位为1时则为扩展数据的处理
		if ( (report->event & 0x80) && report->event != 0xFE) {
			// ToDo: 这里针对宇通扩展的特殊处理,0xFE兼容位置信息汇报指令（0x0200）告警位发生相应告警时触发拍摄的照片，在使用0x0800和0x0801上传图片时会把事件项编码项固定写为FE
			caits += ",126:" + BCDtostr( (char*)report->bcd);
			if ((report->event & 0x7f) == 0 && msg_len >= 18){
				caits += ",127:" + uitodecstr(ntohl(report->seq));
			}
		}

		caits += "} \r\n";

		OUT_DEBUG("updata:%s",caits.c_str());

		_pEnv->GetMsgClient()->HandleUpData( caits.c_str(),caits.length() ) ;

		result = 0x00 ;

		// 清除原来没有上传成功的内存文件
		_scp_media.RemovePackage( str_car_id.c_str() ) ;

		// 需要处理通用应答
		bresp = true ;
	} else if ( msg_id == 0x0801 ) { //0x0801：多媒体数据上传，0x8800：多媒体数据上传应答
		if ( msg_len < (unsigned int)sizeof(MediaUploadBody) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "MediaUploadBody mesage length %d error", len ) ;
			return ;
		}

		unsigned short ntotal = 1 ;
		unsigned short ncur   = 1 ;

		char *ptr = ( char *)( data + sizeof(GBheader) ) ;

		MediaUploadBody *body = ( MediaUploadBody *) ptr ;
		// 这个唯一标识
		int   id 	= -1 ;
		int   mdlen = 0  ;
		char* mdptr = NULL ;
		string gps  = "" ;
		if( ncur == 0x01 ) {
			id    = ntohl( body->id ) ;
			if((body->event & 0x80) && body->event != 0xFE ) { // ToDo: 处理宇通多媒体数据扩展,0xFE兼容位置信息汇报指令（0x0200）告警位发生相应告警时触发拍摄的照片，在使用0x0800和0x0801上传图片时会把事件项编码项固定写为FE
				mdptr = ( char *) ( ptr + sizeof(MediaUploadBodyEx) ) ;
				mdlen = msg_len - sizeof(MediaUploadBodyEx) ;
				// 处理多媒体扩展处理
				MediaUploadBodyEx *bodyex = ( MediaUploadBodyEx *) ptr ;
				gps = "126:" + BCDtostr( (char*)bodyex->bcd ) + "," ;
				gps = gps + _gb_proto_handler->ConvertGpsInfo( &bodyex->gps, NULL, 0 ) ;  // 补页中新增的GPS数据信息
			} else {
				mdptr = (char *)( ptr + sizeof(MediaUploadBody) ) ;
				mdlen = msg_len - sizeof(MediaUploadBody) ;
				gps   = _gb_proto_handler->ConvertGpsInfo( &body->gps, NULL, 0 ) ;  // 补页中新增的GPS数据信息
			}
		} else {

			mdptr = (char *)ptr ;
			mdlen = msg_len ;
		}

		// 需要处理分多个包的情况
		bool uploadok = _scp_media.SavePackage( sock->_fd, caits_car_id.c_str(), id , ntotal , ncur ,
				body->type , body->mtype , ( body->event & 0x7f ) , body->chanel , mdptr, mdlen , gps.c_str() ) ;

		if( uploadok )//完成了一次多媒体文件的传输
		{
			PhotoRespHeader resp;
			memcpy(&(resp.header),header,sizeof(GBheader));
			unsigned short downseq = _pEnv->GetSequeue(str_car_id.c_str());
			resp.header.msgid = htons(0x8800);

			unsigned short prop = htons(0x0005);
			memcpy(&(resp.header.msgtype),&prop,2);
			resp.header.seq 		= htons(downseq);
			resp.retry_package_num  = 0;
			resp.photo_id           = htonl(id);
			// 添加数据检效和
			resp.check_num = _gb_proto_handler->get_check_sum( (const char *) &resp + 1, sizeof(resp) - 3 ) ;

			SendResponse( sock, str_car_id.c_str(), (const char *)&resp, sizeof(resp));
		} else { // 最后一个包不需要通用应答，合规检测中不需要<2012.12.17>
			// 分包上传需要回应通用应答  <2012.11.05> 调整添加最后一个包的通用应答处理
			result  = 0x00 ;
			bresp   = true ;
		}
	} else if ( msg_id == 0x0802 ) {
		if ( len < (int)sizeof(DataMediaHeader) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "DataMediaHeader mesage length %d error", len ) ;
			return ;
		}

		// 存储多媒体数据检索应答
		DataMediaHeader *rsp =  ( DataMediaHeader *) data;
		// 处理多媒检索应答
		unsigned short num = ntohs( rsp->num ) ;
		if ( len < (int)( num * sizeof(DataMediaBody) + sizeof(DataMediaHeader) + sizeof(GBFooter) ) ){
			OUT_ERROR( ip, port, str_car_id.c_str(), "DataMedia length %d error, num %d", len, num ) ;
			return ;
		}

		respid = ntohs(rsp->ackseq) ;

		string key = str_car_id + "_" + ustodecstr(0x8802) + "_" + uitodecstr(respid);
		CacheData cache_req = _cache_data_pool.checkData(key);
		if(cache_req.str_send_msg == "EXIT" ) {
			/*｛TYPE:1，1:[120:|0||1|1,1:精度,2:纬度,3:速度,4:时间,5:方向,…][多媒体数据项2][多媒体数据项N]｝
			[参考：终端主动上传类指令中的key：value定义]*/
			string caits = "CAITR "+ cache_req.seq + " " + caits_car_id + " 0 "+cache_req.command+" {TYPE:1,20:" ;

			string temp = "" ;
			char szbuf[1024] = {0} ;
			char *ptr = ( char *) ( data + sizeof(DataMediaHeader));

			for ( unsigned short i = 0; i < num; ++ i ) {
				// 多媒体数据ID|多媒体类型|多媒体格式编码|事件项编码|通道ID
				temp = "" ;
				if ( ptr[6] & 0x80 ) {
					// ToDo: 处理宇通的媒体检索扩展指令
					DataMediaBodyEx *bodyex = ( DataMediaBodyEx *) ptr ;
					// 处理多媒体ID,补页新增 , 120:多媒体数据ID,121:多媒体类型,124:通道ID,123:事件项编码
					sprintf( szbuf, "120:%u,121:%u,123:%u,124:%u,126:%s," ,
							ntohl(bodyex->mid), bodyex->type, ( bodyex->event & 0x7f ), bodyex->id , BCDtostr((char*)bodyex->bcd).c_str()) ;
					temp += szbuf ;
					temp += _gb_proto_handler->ConvertGpsInfo( &(bodyex->gps), NULL, 0 ) ;
					ptr += sizeof(DataMediaBodyEx) ;

				} else {
					DataMediaBody *body = ( DataMediaBody *) ptr ;

					// 处理多媒体ID,补页新增 , 120:多媒体数据ID,121:多媒体类型,124:通道ID,123:事件项编码
					sprintf( szbuf, "120:%u,121:%u,123:%u,124:%u," , ntohl(body->mid), body->type, body->event, body->id ) ;
					temp += szbuf ;
					temp += _gb_proto_handler->ConvertGpsInfo( &(body->gps), NULL, 0 ) ;

					ptr += sizeof(DataMediaBody) ;
				}
				caits += "[" + temp + "]" ;
			}
			caits += "} \r\n" ;

			_pEnv->GetMsgClient()->HandleUpData( caits.c_str(),caits.length() ) ;
		} else {
			//OUT_WARNING(ip,port,str_car_id.c_str(),"msg type: %x, msg id: 0x8802, cant's find seq:%x in cache_data_pool", msg_id, respid );
			sendOtherCmd(caits_car_id, msg_id, data + sizeof(GBheader), msg_len);
		}

		// 需要处理通用应答
		result = 0x00 ;
		bresp  = true ;
	} else if ( msg_id == 0x0f10 ) {  // 盲区补传数据
		string heads = "CAITS 0_0 " + caits_car_id + " 0 U_REPT {TYPE:7,";
		string ends  = "} \r\n" ;

		int offset = sizeof(GBheader) ;
		unsigned char num = data[offset] ;
		if ( num == 0 ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "0x0f10 number zero" ) ;
			return ;
		}

		offset += 1 ;  // 去掉上传包个数

		if ( msg_len < num*sizeof(GpsInfo) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "0x0f10 mesage length %d error", len ) ;
			return ;
		}

		string content ,sdata ;
		unsigned short word = 0 ;

		for ( unsigned char i = 0; i < num; ++ i ) {
			memcpy( &word, data+offset, sizeof(unsigned short) ) ;
			word = ntohs( word ) ;
			offset += sizeof(unsigned short) ;  // 去掉长度

			if ( word == 0 || word > msg_len ) {
				break ;
			}

			GpsInfo *gps = ( GpsInfo *) ( data + offset ) ;  // 取实际数据项

			content = _gb_proto_handler->ConvertGpsInfo( gps , data+offset+sizeof(GpsInfo) , word - sizeof(GpsInfo)) ;
			// 发送盲补报的数据
			if ( ! content.empty() ) {
				sdata = heads + content + ends  ;
				_pEnv->GetMsgClient()->HandleUpData( sdata.c_str(), sdata.length() ) ;
			} else {
				OUT_ERROR( NULL, 0, caits_car_id.c_str(), "msg id 0x0f10 data error gps content error" ) ;
			}
			offset += word ;
		}

		result = 0x00 ;
		bresp  = true ;
	} else if ( msg_id == 0x0f11 ) {  // 车辆分析数据上传
		if ( msg_len < sizeof(char)+sizeof(short) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "0x0f11 mesage length %d error", len ) ;
			return ;
		}
		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT {TYPE:50,513:";

		int offset = sizeof(GBheader) ;
		unsigned char type  = data[offset] ;  // 数据块类型
		offset += 1 ;

		caits += chartodecstr(type+1) + "," ;  // 处理上传类型, 内部协议解析与808不一致

		unsigned short nlen = 0 ;
		memcpy( &nlen , data+offset, sizeof(short) ) ; // 取得数据长度
		nlen = ntohs( nlen ) ;
		data += sizeof(short) ;

		if ( nlen == 0 ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "0x0f11 data length zero" ) ;
			return  ;
		}

		CBase64 base64;

		if ( ! base64.Encode( data +offset, nlen ) ){
			OUT_ERROR( ip, port, str_car_id.c_str(), "0x0f11 base64 encode failed" ) ;
			return ;
		}

		caits += "514:" ;  caits += base64.GetBuffer() ;
		caits += "} \r\n" ;  // 组建数据

		_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;

		result = 0x00 ;
		bresp  = true ;
	} else if ( msg_id == 0x0f13 ) {  // 历史数据上传
		if ( msg_len < sizeof(HistoryDataUploadBody) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "HistoryDataUploadBody mesage length %d error", len ) ;
			return ;
		}

		HistoryDataUploadBody *body = ( HistoryDataUploadBody *) ( data + sizeof(GBheader) ) ;

		respid = ntohs(body->seqid) ;
		if ( body->num == 0 ) {
			OUT_ERROR( ip, port, str_car_id.c_str() , "history data error, number zero" ) ;
			return ;
		}

		string key = str_car_id + "_" + ustodecstr(0x8f12) + "_" + uitodecstr(respid) ;
		CacheData cache_req = _cache_data_pool.checkData(key);

		if( cache_req.str_send_msg == "EXIT" ) {
			string caits = "CAITR "+ cache_req.seq + " " + caits_car_id + " 0 U_REPT {TYPE:51,515:" ;
			// 消息流水号|历史数据类型|历史数据起始时间|历史数据结束时间|历史数据项个数|历史数据项内容
			caits += chartodecstr( respid ) + "|" ;
			caits += chartodecstr( body->type+1 ) + "|" ;
			caits += _gb_proto_handler->get_bcd_time( body->starttime ) + "|" ;
			caits += _gb_proto_handler->get_bcd_time( body->endtime ) + "|" ;
			caits += chartodecstr( body->num ) + "|" ;

			const char * ptr = ( const char *) ( data + sizeof(GBheader) + sizeof(HistoryDataUploadBody) ) ;

			string stemp ;
			switch( body->type )
			{
			case 0:	// 位置汇报备份数据
				{
					if ( msg_len < sizeof(HistoryDataUploadBody) + body->num * sizeof(GpsInfo) ) {
						OUT_ERROR( ip, port, str_car_id.c_str(), "HistoryDataUploadBody GpsInfo mesage length %d , num %d error", len , body->num ) ;
						return ;
					}

					int offset = 0 ;
					for ( unsigned char i = 0 ; i < body->num; ++ i ) {
						GpsInfo *gps = ( GpsInfo *) ( ptr + offset ) ;
						stemp = _gb_proto_handler->ConvertGpsInfo( gps , NULL , 0 ) ;
						if ( ! stemp.empty() ) {
							caits += "["+stemp+"]" ;
						}
						offset += sizeof(GpsInfo) ;
					}
				}
				break ;
			case 1: // 发动机能效数据
				{
					if ( msg_len < sizeof(HistoryDataUploadBody) + body->num * sizeof(EngneerData) ) {
						OUT_ERROR( ip, port, str_car_id.c_str(), "HistoryDataUploadBody EngneerData mesage length %d , num %d error", len , body->num ) ;
						return ;
					}

					int offset = 0 ;
					for ( unsigned char i = 0; i < body->num; ++ i ) {
						EngneerData *p = ( EngneerData *) ( ptr + offset ) ;
						stemp = _gb_proto_handler->ConvertEngeer( p ) ;
						if ( ! stemp.empty() ) {
							caits += "["+stemp+"]" ;
						}
						offset += sizeof(EngneerData) ;
					}
				}
				break ;
			case 2: // 车辆分析备份数据
				{
					if ( msg_len <= sizeof(HistoryDataUploadBody) ) {
						OUT_ERROR( ip, port, str_car_id.c_str(), "HistoryData back data mesage length %d error", len ) ;
						return ;
					}

					CBase64 base64 ;
					if ( ! base64.Encode( ptr , len- sizeof(GBheader) - sizeof(HistoryDataUploadBody) ) ){
						OUT_ERROR( ip, port, str_car_id.c_str(), "0x0f11 base64 encode failed" ) ;
						return ;
					}
					caits += base64.GetBuffer() ;
				}
				break ;
			}
			caits += "} \r\n" ;

			_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;

			HistoryDataUploadAck ack ; // 上传历史数据应答
			ack.header.msgid = htons( 0x8f13 ) ;
			ack.header.seq   = htons( _pEnv->GetSequeue(str_car_id.c_str()) ) ;
			memcpy( ack.header.car_id , header->car_id, sizeof(header->car_id) ) ;

			unsigned short len = sizeof(short) + sizeof(char) ;
			len = htons( len & 0x3ff ) ;
			memcpy( &ack.header.msgtype, &len, sizeof(short) ) ;

			ack.seqid  = header->seq ;
			ack.num    = 0x00 ;

			char buf[1024] = {0} ;
			memcpy( buf, &ack, sizeof(ack) ) ;

			GBFooter footer ;
			footer.check_sum = _gb_proto_handler->get_check_sum( buf + 1, sizeof(ack)-1 ) ;
			memcpy( buf + sizeof(ack) , &footer, sizeof(footer) ) ;

			// 发送响应应答
			SendResponse( sock, str_car_id.c_str(),  buf, sizeof(ack)+ sizeof(footer) ) ;
		} else {
			OUT_WARNING(ip,port,str_car_id.c_str(),"msg type: %x, msg id: 0x8f12, cant's find seq:%x in cache_data_pool", msg_id, respid );
		}
	} else if ( msg_id == 0x0f14 ) { //驾驶行为事件
		if ( len < (int)sizeof(DriverActionEvent) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "DriverActionEvent mesage length %d error", len ) ;
			return ;
		}

		DriverActionEvent *req  = ( DriverActionEvent *)data ;
		//516:事件类型|[起始位置纬度][起始位置经度][起始位置高度][起始位置速度][起始位置方向][起始位置时间]|[结束位置纬度][结束位置经度][结束位置高度][结束位置速度][结束位置方向][结束位置
		string caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT {TYPE:52,516:";

		caits += chartodecstr( req->type+1 ) + "|" ;
		caits += _gb_proto_handler->ConvertEventGps( &req->startgps ) + "|" ;
		caits += _gb_proto_handler->ConvertEventGps( &req->endgps )  ;

		caits += "} \r\n" ;

		_pEnv->GetMsgClient()->HandleUpData( caits.c_str(), caits.length() ) ;

		// 需要处理通用应答
		result = 0x00 ;
		bresp  = true ;
	} else if (msg_id == 0x0f15) { //终端版本信息
		if ( len < (int)sizeof(ProgramVersionEvent) ) {
			 OUT_ERROR( ip, port, str_car_id.c_str(), "ProgramVersionEvent mesage length %d error", len) ;
			 return ;
		}

		ProgramVersionEvent *req = (ProgramVersionEvent *)data;
		int nOffset = sizeof(ProgramVersionEvent);

		VERSION_IINFO *lpInfoVersion = NULL;

		int info_count = req->count;
		unsigned char info_id;
		unsigned char info_len;
		char         *info_buf;

		int i;
		vector<string> infos(0xe);
		const string filter("| ,:\0", 5);

		for(i = 0; i < info_count; ++i) {
	    	if(nOffset + (int)sizeof(VERSION_IINFO) > len) {
	    		break;
	    	}
	    	lpInfoVersion = (VERSION_IINFO *)(data + nOffset);
	    	nOffset += sizeof(VERSION_IINFO);

	    	info_id  = lpInfoVersion->id;
	    	info_len = lpInfoVersion->len;
	    	info_buf = lpInfoVersion->buf;

	    	if(nOffset + info_len > len) {
	    		break;
	    	}

	    	if(info_len == 0) {
	    		continue;
	    	}

	    	if(info_id >= 1 && info_id <= 0xe) {
	    		infos[info_id - 1] = Utils::filter(string(info_buf, info_len), filter);
	    	}

            nOffset  += info_len;
		}

	    string caits;
	    caits.reserve(1024);

	    caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT {TYPE:53,518:";
	    for(i = 0; i < (int)infos.size(); ++i) {
	    	if(i > 0) {
	    		caits += "|";
	    	}

	    	caits += infos[i];
	    }

	    caits += "} \r\n";

		OUT_DEBUG("version:%s\n",caits.c_str());

		_pEnv->GetMsgClient()->HandleUpData(caits.c_str(), caits.length());
		//需要处理通用应答
		result = 0x00;
		bresp  = true;
	} else if (msg_id == 0x0ff1) { //特征系数校正结果上报
		if ( msg_len < sizeof(SpeedAdjustReport) ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "speed adjust report mesage length %d error", msg_len);
			return;
		}

		SpeedAdjustReport *report = (SpeedAdjustReport*)(data + sizeof(GBheader));

		string field;
	    string caits;
	    caits.reserve(1024);

	    caits = "CAITS 0_0 " + caits_car_id + " 0 U_REPT {TYPE:65";

		caits += ",600:" + BCDtostr((char*)report->btime);
	    caits += ",601:" + BCDtostr((char*)report->etime);
	    caits += ",602:" + Utils::int2str(ntohs(report->gpsSpeed), field);
	    caits += ",603:" + Utils::int2str(ntohs(report->vssSpeed), field);
	    caits += ",604:" + Utils::int2str(ntohs(report->recommend), field);
		caits += "} \r\n";

		OUT_DEBUG("Idle Speed:%s\n",caits.c_str());
		_pEnv->GetMsgClient()->HandleUpData(caits.c_str(), caits.length());

	    //需要处理通用应答
		result = 0x00;
		bresp  = true;
	} else {
		sendOtherCmd(caits_car_id, msg_id, data + sizeof(GBheader), msg_len);

		result  = 0x00 ;
		bresp   = true ;
	}
	// 移除平台下发等待响应的数据
	if ( respid > 0 ) {
		OUT_PRINT( sock->_szIp, sock->_port, str_car_id.c_str(), "msg id %04x, remove sequeue id %d", msg_id, respid ) ;
		_queuemgr->Remove( str_car_id.c_str(), respid, true ) ;
	}
	// 如果不需要回应则直接返回了
	if ( ! bresp ) return ;

	unsigned short downseq = _pEnv->GetSequeue(str_car_id.c_str());
	if(downseq!=0xffff) {
		PlatFormCommonResp resp = _gb_proto_handler->BuildPlatFormCommonResp( header, downseq, result ) ;
		SendResponse( sock, str_car_id.c_str(), (const char *)&resp, sizeof(resp) ) ;
	}
}

void ClientAccessServer::on_dis_connection( socket_t *sock )
{
	User user = _online_user.GetUserBySocket( sock ) ;
	if( ! user._user_id.empty() ){
		OUT_WARNING( sock->_szIp , sock->_port , user._user_id.c_str(), "ENV on_dis_connection fd %d", sock->_fd ) ;
		bool bnotify = true ;  // 是否要发送下线通知
		if ( strncmp( user._user_id.c_str(), "UDP_" , 4 ) == 0  ){
			// 如果为UDP先检测TCP连接是否存在如果存就不发下线通知
			User temp = _online_user.GetUserByUserId( user._user_id.substr(4) ) ;
			if ( ! temp._user_id.empty() ) {
				bnotify = false ;
			}
		}
		if ( bnotify ) {
			string caits = "CAITS 0_0 " + user._user_name + " 0 D_CONN {TYPE:0} \r\n";
			_pEnv->GetMsgClient()->HandleUpData( caits.c_str(),caits.length() ) ;

			// 如果终端断开连接就直接清除当前终端所有需要下发的数据
			_queuemgr->Del( user._user_id.c_str() ) ;
		}
		_online_user.DeleteUser( sock ) ;
	} else {
		OUT_CONN( sock->_szIp , sock->_port , "ENV", "on_dis_connection fd %d", sock->_fd ) ;
	}
}

// 发送响应数据，主要实现如果有TCP通道优先TCP
bool ClientAccessServer::SendResponse( socket_t *sock, const char *id , const char *data, int len )
{
	// 如果为UDP的FD则优先查找TCP通道，如果不存在就使用UDP通道下发
	if ( check_udp_fd( sock ) && id ) {
		User user = _online_user.GetUserByUserId( id ) ;
		if ( ! user._user_id.empty() && user._fd->_fd > 0 ) {
			sock = user._fd ;
		}
	}
	//OUT_SEND3( sock->_szIp , sock->_port , id , "fd %d send response len %d", sock->_fd , len ) ;
	//OUT_HEX( sock->_szIp, sock->_port, id , data , len );
	// 发送数据
	return Send7ECodeData( sock, data, len ) ;
}

bool ClientAccessServer::Send7ECodeData( socket_t *sock, const char *data, int len )
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

bool ClientAccessServer::SendDataToUser( const string &user_id,const char *data,int data_len)
{
	User user = _online_user.GetUserByUserId( user_id ) ;
	if ( user._user_id.empty() || user._fd == NULL ) {
		user = _online_user.GetUserByUserId( "UDP_" + user_id ) ;
	}

	if( user._user_id.empty() || user._user_state == User::OFF_LINE || user._fd == NULL ){
		//设备不在线，返回一个设备不在线的包，在此暂且先不做处理，设备不在线的标识。
		OUT_WARNING( NULL,0,user_id.c_str(), "user is not login!" );
		OUT_HEX( user._ip.c_str() , user._port , user._user_id.c_str() , data , data_len );

		return false;
	}

	OUT_SEND3( user._ip.c_str() , user._port , user._user_id.c_str() , "fd %d send data len %d", user._fd->_fd , data_len ) ;
	//OUT_HEX( user._ip.c_str() , user._port , user._user_id.c_str() , data , data_len );

	return Send7ECodeData( user._fd, data, data_len );
}

// 数据结构体
struct _qmsgdata
{
	char  id[13];
	char *ptr ;
	int   len ;
};

// 调用超时重发数据
bool ClientAccessServer::OnReSend( void *data )
{
	if ( data == NULL )
		return false ;
	_qmsgdata *p = ( _qmsgdata *) data ;

	return SendDataToUser( p->id, (const char *)p->ptr, p->len ) ;
}

// 调用超时后重发次数据删除数据
void ClientAccessServer::Destroy( void *data )
{
	if ( data == NULL )
		return ;
	_qmsgdata *p = ( _qmsgdata *) data ;
	if ( p->ptr != NULL )
		delete [] p->ptr ;
	delete p ;
}

void ClientAccessServer::HandleDownData( const char *userid, const char *data, int  len , unsigned int seq , bool send )
{
	//处理下行缓冲池中的数据，因为下行缓冲池中的数据已经是解析完毕的下行数据，所以只需将其发送出去即可。
	if ( seq > 0 ) {
		_qmsgdata *p = new _qmsgdata;
		safe_memncpy( p->id, userid, sizeof(p->id) ) ;
		p->ptr = new char[len+1] ;
		memcpy( p->ptr, data, len ) ;
		p->len = len ;

		OUT_PRINT( NULL, 0, "QueueMgr", "add user %s seq id %u", userid, seq ) ;
		_queuemgr->Add( userid, seq, p, !send ) ;

		if ( ! send ) return ;
	}
	// 发送数据
	if ( ! SendDataToUser( userid , data, len ) ) {
		if ( seq > 0 )
			_queuemgr->Remove( userid, seq , false ) ;
	}
}

bool ClientAccessServer::HasLogin(string &user_id)
{
	User user = _online_user.GetUserByUserId( user_id ) ;
	if ( user._user_id.empty() )
		user = _online_user.GetUserByUserId( "UDP_" + user_id ) ;
	if ( user_id.empty() )
		return false;
	return true;
}

// 从后拷贝，不足填充字符
static void reverse_copy( char *buf , int len, const char *src , const char fix )
{
	int nlen   = (int) strlen( src ) ;
	int offset = len - nlen ;
	if ( offset < 0 ) {
		offset = 0 ;
	}
	if ( offset > 0 ) {
		for ( int i = 0; i < offset; ++ i ) {
			buf[i] = fix ;
		}
	}
	memcpy( buf + offset, src, nlen ) ;
}

// 发送TTS语音播报
void ClientAccessServer::SendTTSMessage( const char *userid, const char *msg, int len )
{
	GBheader header ;
	GBFooter footer ;

	char key[128] = {0} ;
	reverse_copy( key, 12 , userid, '0' ) ;
	strtoBCD( key , header.car_id ) ;

	// 长度为消息内容的长度去掉头和尾
	unsigned short mlen = htons( (len+1) & 0x03FF ) ;
	memcpy( &(header.msgtype), &mlen, sizeof(short) );

	unsigned short seqid = _pEnv->GetSequeue( userid ) ;
	header.seq 	 = htons(seqid) ;
	header.msgid = htons(0x8300) ;

	DataBuffer buf ;
	// 写入头部数据
	buf.writeBlock( &header, sizeof(header) ) ;
	// 处理第一个位
	buf.writeInt8( 0x08 ); // TTS播报
	buf.writeBlock( msg , len ) ;

	footer.check_sum = _gb_proto_handler->get_check_sum( buf.getBuffer()+1 , buf.getLength() - 1 ) ;
	buf.writeBlock( &footer, sizeof(footer) ) ;

	SendDataToUser( userid,  buf.getBuffer(), buf.getLength() ) ;
}

// 当新连接到来时的处理
void ClientAccessServer::on_new_connection( socket_t *sock, const char* ip, int port)
{
	// 记录连接
	OUT_CONN( ip, port, "ENV", "Recv new connection fd %d", sock->_fd ) ;
	// 接收到新的连接的情况
}

// 检测IP是否为合法的有效的IP接入
bool ClientAccessServer::check_ip( const char *ip )
{
	if ( ! _ipblack.Check( ip ) ) {
		return true ;
	}
	return false ;
}

void ClientAccessServer::sendOtherCmd(const string &macid, uint16_t msgid, const char* ptr, int len)
{
	string field;
	string caits;
	caits.reserve(1024);

	CBase64 base64;
	base64.Encode(ptr, len);

	caits = "CAITS 0_0 " + macid + " 0 U_REPT {TYPE:10";
	caits += ",95:" + Utils::int2str(msgid, field) + ",96:" + base64.GetBuffer() + "} \r\n";
	_pEnv->GetMsgClient()->HandleUpData(caits.c_str(), caits.length());
}
