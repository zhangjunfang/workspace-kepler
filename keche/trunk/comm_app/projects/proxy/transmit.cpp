/***********************************************************************
** Copyright (c)2009, 北京千方科技集团有限公司
** All rights reserved.
** 
** File name  : transmit_service.cpp
** Author     : Liubo
** Date       :
** Comments   : 连接全国平台类
***********************************************************************/
#include "transmit.h"
#include "ProtoHeader.h"
#include <tools.h>

Transmit::Transmit()
	: _filecache(this)
{
	_listen_port 		 = 0;
	//定义主动连接的verify_code,暂定123456，具体如何定根据实际情况作出改进
	_verify_code 		 = 123456;
	_client._socket_type = User::TcpConnClient;
	_client._connect_info.keep_alive = AlwaysReConn;
	_client._connect_info.last_reconnect_time = 0;
	_client._connect_info.timeval = 20;   //默认20s重连一次。

	_M1  = _IA1 = _IC1 = 0 ;
}

Transmit::~Transmit()
{
	Stop() ;
}


bool Transmit::Init(ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	char value[512] = {0};
	if ( ! pEnv->GetString( "transmit_listen_ip", value) )
	{
		INFO_PRT("get transmit_listen_ip failed!\n");
		ERRLOG(NULL, 0, "get transmit_listen_ip failed!");
		return false ;
	}
	_down_ip = _listen_ip  = value ;

	// 下行的IP主要监听的IP可以为0.0.0.0
	if ( pEnv->GetString("transmit_down_ip" , value ) ) {
		_down_ip = value ;
	}

	int port = 0 ;
	if ( ! pEnv->GetInteger( "transmit_listen_port", port ) )
	{
		INFO_PRT("get transmit_listen_port failed!\n");
		ERRLOG(NULL, 0, "get transmit_listen_port failed!");
		return false ;
	}
	_listen_port = port ;

	if ( ! pEnv->GetString( "transmit_connect_ip", value) )
	{
		INFO_PRT("get transmit_connect_ip failed!\n");
		ERRLOG(NULL, 0, "get transmit_connect_ip failed!");
		return false ;
	}
	_client._ip  = value ;

	if ( ! pEnv->GetInteger( "transmit_connect_port", port ) )
	{
		INFO_PRT("get transmit_connect_port failed!\n");
		ERRLOG(NULL, 0, "get transmit_connect_port failed!");
		return false ;
	}
	_client._port = port ;

	if ( ! pEnv->GetString( "user_id", value ) )
	{
		printf("get user_id failed!\n");
		ERRLOG(NULL, 0, "get user_id failed!");
		return false ;
	}
	_client._user_id = value ;
	_user_name 		=  atoi(value) ;

	if ( ! pEnv->GetString("user_password", value) )
	{
		printf("get user_password failed!\n");
		ERRLOG(NULL, 0, "get user_password failed!");
		return false ;
	}
	_user_password = value ;

	int code = 0 ;
	if ( ! pEnv->GetInteger( "access_code", code ) )
	{
		printf("get access_code failed!\n");
		ERRLOG(NULL, 0,0, "get access_code failed!");
		return false ;
	}
	_access_code 		 = code ;
	_client._access_code = code ;

	// 取得密钥
	if ( pEnv->GetInteger( "M1" , code ) ) {
		_M1 = code ;
	}
	if ( pEnv->GetInteger( "IA1" , code ) ) {
		_IA1 = code ;
	}
	if ( pEnv->GetInteger( "IC1" , code ) ) {
		_IC1 = code ;
	}
	printf( "m1 %d,ia1 %d,ic1 %d\n", _M1, _IA1, _IC1 ) ;

	return _filecache.Init( pEnv , "mas" ) ;
}

bool Transmit::Start( void )
{
	if ( ! _filecache.Start() ) {
		OUT_ERROR( NULL, 0, NULL,  "start filecache failed" ) ;
		return false ;
	}
	return StartServer( _listen_port, _listen_ip.c_str() , 1  ) ;
}

void Transmit::Stop( void )
{
	StopServer() ;
	_filecache.Stop() ;
}

bool Transmit::ConnectServer(User &user, unsigned int timeout)
{
	if(time(0) - user._connect_info.last_reconnect_time < user._connect_info.timeval)
		return false;

	bool ret = false;
	if ( user._fd != NULL ) {
		OUT_INFO( user._ip.c_str(), user._port, user._user_id.c_str(), "Transmit::ConnectServer fd %d close socket",
				user._fd->_fd );
		CloseSocket( user._fd );
	}

	user._fd = _tcp_handle.connect_nonb( user._ip.c_str(), user._port, timeout );
	ret = ( user._fd != NULL ) ? true : false;

	if ( ret ) {
		//发送登录包
		UpConnectReq req;
		req.header.msg_seq = ntouv32( _proto_parse.get_next_seq());
		req.header.access_code = ntouv32(_access_code);
		req.user_id = ntouv32(_user_name);
		req.down_link_port = ntouv16(_listen_port);
		strncpy( ( char* ) ( req.down_link_ip ), _down_ip.c_str(), _down_ip.length() );
		strncpy( ( char * ) ( req.password ), _user_password.c_str(), 8 );
		// req.crc_code = ntouv16(GetCrcCode((const char*)&req,sizeof(req)));

		if ( SendCrcData( user._fd, ( const char* ) & req, sizeof ( req ) ) ) {
			OUT_CONN( user._ip.c_str(), user._port, user._user_id.c_str(), "connect success,send login message" );
		} else {
			OUT_ERROR( user._ip.c_str(), user._port, user._user_id.c_str(),
					"connect success,send login message failed" );
		}
		user._last_active_time = time( 0 );
		user._user_state = User::WAITING_RESP;
	} else {
		user._user_state = User::OFF_LINE;
	}
	user._last_active_time = time( 0 );
	user._login_time = time( 0 );
	user._connect_info.last_reconnect_time = time( 0 );
	if ( user._connect_info.keep_alive == ReConnTimes )
		user._connect_info.reconnect_times --;

	return ret;
}

// 加密处理数据
bool Transmit::EncryptData( unsigned char *data, unsigned int len , bool encode )
{
	// 密钥是否为空如果为空不需要处理
	if ( _M1 == 0  && _IA1 == 0 && _IC1 == 0 ) {
		return false ;
	}

	Header *header = ( Header *) data ;
	// 是否需要加密处理
	if ( ! header->encrypt_flag && ! encode ) {
		return false;
	}

	// 如果为加密处理
	if ( encode ) {
		// 设置加密标志位
		header->encrypt_flag =  1 ;
		// 添加加密密钥
		header->encrypt_key  =  ntouv32( CEncrypt::rand_key() ) ;
	}

	// 解密数据
	return CEncrypt::encrypt( _M1, _IA1, _IC1, (unsigned char *)data, (unsigned int) len ) ;
}

void Transmit::on_data_arrived( socket_t *sock, const void* data, int len)
{
	_recvstat.AddFlux( len ) ;

	C5BCoder coder ;
	if( !coder.Decode( (const char *) data, len ) )
	{
		OUT_WARNING( sock->_szIp, sock->_port, NULL, "Except packet header or tail");
		return;
	}

	const char *data_src = coder.GetData() ;
	const int   data_len = coder.GetSize() ;

	if (data_len < (int)sizeof(Header) + (int)sizeof(Footer))
	{
		OUT_ERROR( NULL, 0, NULL, "Transmit::on_data_arrived packet len error");
		return;
	}	

	Header *header = (Header *) data_src ;
	unsigned short msg_type = ntouv16(header->msg_type);
	unsigned int msg_len = ntouv32(header->msg_len);

	if (msg_len!=(unsigned int)data_len)
	{
		OUT_ERROR( NULL, 0, NULL, "Transmit::on_data_arrived packet len error" );
		return;	
	}
	
	unsigned int access_code = ntouv32(header->access_code);
	const char *ip    = sock->_szIp ;
	unsigned int port = sock->_port ;
	string str_access_code = uitodecstr(access_code);

	// 解密前数据
//	OUT_HEX( NULL , 0 , str_access_code.c_str(), data_src , data_len ) ;
	// 处理解密数据
	EncryptData( (unsigned char *) coder.GetData(), (unsigned int) coder.GetSize(), false ) ;


	string msg_data = _proto_parse.Decoder((const char*)data_src , data_len ) ;
	OUT_RECV3( ip, port, str_access_code.c_str(), "%s", msg_data.c_str() ) ;
//	OUT_HEX( ip, port, str_access_code.c_str(), data_src, data_len ) ;

	time_t now = time(NULL) ;
	if( sock == _client._fd)
		_client._last_active_time = now ;
	else if( sock == _server._fd)
		_server._last_active_time = now ;

	//INFO_PRT("Parse a server packet, msg_type = %x \n",msg_type);
	//全国平台的主动连接
	if (msg_type == UP_CONNECT_RSP)
	{
		UpConnectRsp *resp = (UpConnectRsp*)(data_src);
		switch(resp->result)
		{
		case 0:
		{
			_client._user_state = User::ON_LINE;
			OUT_INFO(ip,port,str_access_code.c_str(),"login check success,access_code:%d  up-link ON_LINE",access_code);
			if ( _client._fd == sock ) _filecache.Online( _client._access_code ) ;
			break;
		}
		case 1:
			OUT_WARNING(ip,port,str_access_code.c_str(),"login check fail,ip is invalid");
			break;
		case 2:
			OUT_WARNING(ip,port,str_access_code.c_str(),"login check fail,accesscode is invalid,close it");
			break;
		case 3:
			OUT_WARNING(ip,port,str_access_code.c_str(),"login check fail,user_name:%s is invalid,close it",_client._user_name.c_str());
			break;
		case 4:
			OUT_WARNING(ip,port,str_access_code.c_str(),"login check fail,user_password:%s is invalid,close it",_user_password.c_str());
			break;
		default:
			OUT_WARNING(ip,port,str_access_code.c_str(),"login check fail,other error,close it");
			break ;
		}
	}
	//服务器下行链路的连接请求
	else if (msg_type == DOWN_CONNECT_REQ)
	{
		DownConnectReq* req = (DownConnectReq*) data_src;
		_server._fd         = sock ;
		_server._ip         = ip ;
		_server._port       = port ;
		_server._login_time = now ;
		_server._msg_seq    = 0;
		_server._user_state = User::ON_LINE;
		//一定要设置这个，因为在下行链路还没有建立起来的时候，第一次登录的时候上的那个是不能执行的。
		_server._last_active_time = now ;

		DownConnectRsp resp;
		resp.header.msg_seq = ntouv32( _proto_parse.get_next_seq() );
	
		resp.header.access_code = req->header.access_code;
		resp.result = 0;
		// resp.crc_code = GetCrcCode((const char*)&resp,sizeof(resp));

		if ( SendCrcData(_server._fd,(const char *)&resp,sizeof(resp) ) ) {
			OUT_INFO(ip,port,str_access_code.c_str(),"DOWN_CONNECT_REQ: send DOWN_CONNECT_RSP downlink is online");
		} else {
			OUT_ERROR(ip,port,str_access_code.c_str(),"DOWN_CONNECT_REQ: send DOWN_CONNECT_RSP downlink is online failed");
		}

		static bool bFirst = true;
		if (bFirst){
			bFirst = false;
			int iStandard_test = 0;
			_pEnv->GetInteger("standard_test", iStandard_test);
			if (iStandard_test == 1)
			{
				UpDisconnectInform test_req;
				test_req.header.msg_seq = ntouv32( _proto_parse.get_next_seq() );
				test_req.header.access_code = ntouv32(_access_code);
				test_req.header.msg_type = ntouv16(UP_DISCONNECT_INFORM);
				test_req.header.msg_len = ntouv32(sizeof(UpDisconnectInform));            
				test_req.error_code = 0;

				if ( SendCrcData(_server._fd,(const char *)&test_req,sizeof(test_req) ) ) {
					OUT_INFO(ip,port,str_access_code.c_str(),"UP_DISCONNECT_INFORM successed");
				} else {
					OUT_ERROR(ip,port,str_access_code.c_str(),"UP_DISCONNECT_INFORM failed");
				}
			}
            
			if (iStandard_test == 1)
			{
				UpCloselinkInform test_req;
				test_req.header.msg_seq = ntouv32( _proto_parse.get_next_seq() );
				test_req.header.access_code = ntouv32(_access_code);
				test_req.header.msg_type = ntouv16(UP_CLOSELINK_INFORM);
				test_req.header.msg_len = ntouv32(sizeof(UpCloselinkInform));
				test_req.reason_code = 0;

				if ( SendCrcData(_server._fd,(const char *)&test_req,sizeof(test_req) ) ) {
					OUT_INFO(ip,port,str_access_code.c_str(),"UP_CLOSELINK_INFORM successed");
				} else {
					OUT_ERROR(ip,port,str_access_code.c_str(),"UP_CLOSELINK_INFORM failed");
				}
			}

			if (iStandard_test == 1)
			{
				//  发送主链路注销请求
				UpDisconnectReq test_req;

				test_req.header.msg_seq = ntouv32( _proto_parse.get_next_seq() );
				test_req.header.access_code = ntouv32(_access_code);
				test_req.user_id = ntouv32(_user_name);
				safe_memncpy( (char*)test_req.password,  _user_password.c_str(), sizeof(test_req.password) ) ;
				//    strncpy((char *)(test_req.password),_user_password.c_str(),8);

				if ( SendCrcData(_client._fd,(const char *)&test_req,sizeof(test_req) ) ) {
					OUT_INFO(ip,port,str_access_code.c_str(),"UP_DISCONNECT_REQ successed");
				} else {
					OUT_ERROR(ip,port,str_access_code.c_str(),"UP_DISCONNECT_REQ failed");
				}
			}
		}
	}
	else if (msg_type == UP_DISCONNECT_RSP)
	{
		OUT_RECV3( ip, port, "mas", "Recv UP_DISCONNECT_RSP");

		CloseSocket(_client._fd);
		_client._fd = NULL;
	}
	else if(msg_type == UP_LINKTEST_REQ)
	{
//		UpLinkTestRsp resp;
//		resp.header.access_code = header->access_code;
//		resp.header.msg_seq = ntouv32(_proto_parse.get_next_seq());
//		SendCrcData(fd,(const char*)&resp,sizeof(resp));
		return ;
	}
	else if(msg_type == UP_LINKTEST_RSP)
	{
        //  测试时使用的代码,从链路建立后发送主链路断开消息
		OUT_INFO( ip, port, str_access_code.c_str(), "UP_LINKTEST_RSP" ) ;
	}
	else if(msg_type == DOWN_LINKTEST_REQ)
	{
		DownLinkTestRsp resp;
		resp.header.access_code = header->access_code;
		resp.header.msg_seq = ntouv32( _proto_parse.get_next_seq() );
		// resp.crc_code = ntouv16(GetCrcCode((const char*)&resp,sizeof(resp)));
		if ( SendCrcData( sock, (const char*)&resp, sizeof(resp) ) ) {
			OUT_INFO( ip, port, str_access_code.c_str(), "DownLinkTestRsp: success" ) ;
		} else {
			OUT_ERROR( ip , port, str_access_code.c_str() , "DownLinkTestRsp: failed" ) ;
		}
	}
	else if (msg_type == DOWN_LINKTEST_RSP)
	{
	}
	else if (msg_type == DOWN_DISCONNECT_RSP)
	{
	}
	else if (msg_type == DOWN_DISCONNECT_INFORM)
	{
		//不管它，由定时线程来完成。
		OUT_RECV3( ip, port, "mas", "Recv DOWN_DISCONNECT_INFORM");

		DownDisconnectInform * pReq = (DownDisconnectInform *)(data_src);

		if (pReq->error_code == (unsigned char)0){
			OUT_ERROR( ip, port, "mas", "DOWN_DISCONNECT_INFORM: 无法连接下级平台指定的服务IP与端口" ) ;
		}
		else if (pReq->error_code == (unsigned char)1){
		    OUT_ERROR( ip, port, "mas", "DOWN_DISCONNECT_INFORM:上级平台客户端与下级平台服务端断开");
		}
		else if (pReq->error_code == (unsigned char)2){
		    OUT_ERROR( ip, port, "mas", "DOWN_DISCONNECT_INFORM:其他原因");
		}
		else {
		    OUT_ERROR( ip, pReq->error_code, "mas", "DOWN_DISCONNECT_INFORM:未定义错误码");
		}
	}
	else if (msg_type == DOWN_CLOSELINK_INFORM)
	{
		//  上级平台主动关闭主从链路通知消息
		OUT_RECV3( ip, port, "mas", "Recv DOWN_CLOSELINK_INFORM" ) ;
        
		DownCloselinkInform * pReq = (DownCloselinkInform *)(data_src);
		switch(pReq->reason_code){
		case 0:
			OUT_ERROR( ip, port, "mas", "DOWN_CLOSELINK_INFORM:网关重启") ;            
		    break;            
		case 1:
			OUT_ERROR( ip, port, "mas", "DOWN_CLOSELINK_INFORM:其他原因") ;            
		    break;
		default:
		    break;
		}
	}
	else if (msg_type == DOWN_DISCONNECT_REQ)
	{
		OUT_RECV3( ip, port, "mas", "Recv DOWN_DISCONNECT_REQ" ) ;
        
		DownDisconnectRsp req;
		req.header.access_code = header->access_code;
		req.header.msg_seq = ntouv32( _proto_parse.get_next_seq() );
		req.header.msg_len = ntouv32(sizeof(DownDisconnectRsp));
		req.header.msg_type = ntouv16(DOWN_DISCONNECT_RSP);

		if ( SendCrcData(_server._fd,(const char *)&req,sizeof(req) ) ) {
			OUT_INFO(ip,port,str_access_code.c_str(),"DOWN_DISCONNECT_RSP successed");
		} else {
			OUT_ERROR(ip,port,str_access_code.c_str(),"DOWN_DISCONNECT_RSP failed");
		}
	}
	else
	{
		if (msg_type == UP_EXG_MSG)
		{
			// ExgMsgHeader *msg_header = (ExgMsgHeader*) (data_src + sizeof(Header));
		}
		else if(msg_type == DOWN_EXG_MSG)
		{
			//跨域等需要处理的数据。
			ExgMsgHeader *msg_header = (ExgMsgHeader*) (data_src + sizeof(Header));

			if (msg_header->vehicle_no[0]==(char)0)
			{
				OUT_ERROR(ip,port,str_access_code.c_str(),"vehicle is null");
				return;
			}
			switch(ntouv16(msg_header->data_type))
			{
			case DOWN_EXG_MSG_CAR_INFO: //4.5.3.2.4 交换车辆静态信息消息  上级平台主动下发跨域车辆的静态信息
			case DOWN_EXG_MSG_CAR_LOCATION:
				break ;

			case DOWN_EXG_MSG_RETURN_STARTUP://部平台判断跨域并下发
			{
				OUT_RECV3( ip, port, "mas", "Recv DOWN_EXG_MSG_RETURN_STARTUP" ) ;
				//  跨域车辆车牌号持久化
				char vehicle_no[23] = {0} ;
				safe_memncpy( vehicle_no, msg_header->vehicle_no, sizeof(msg_header->vehicle_no) ) ;
				_filecache.AddArcoss( _access_code, msg_header->vehicle_color, vehicle_no ) ;
				break ;
			}
			case DOWN_EXG_MSG_RETURN_END:
			{
				OUT_RECV3( ip, port, "mas", "Recv DOWN_EXG_MSG_RETURN_END" ) ;
				//  取消跨域车辆车牌号
				char vehicle_no[23] = {0} ;
				safe_memncpy( vehicle_no, msg_header->vehicle_no, sizeof(msg_header->vehicle_no) ) ;
				_filecache.DelArcoss( _access_code, msg_header->vehicle_color, vehicle_no ) ;
				break ;
			}
			case DOWN_EXG_MSG_APPLY_FOR_MONITOR_END_ACK:
			case DOWN_EXG_MSG_APPLY_FOR_MONITOR_STARTUP_ACK://4.5.3.2.7 申请交换指定车辆定位信息应答消息
				break ;
			case DOWN_EXG_MSG_HISTORY_ARCOSSAREA:
			case DOWN_EXG_MSG_REPORT_DRIVER_INFO:
			case DOWN_EXG_MSG_APPLY_HISGNSSDATA_ACK://上级平台响应
			case DOWN_EXG_MSG_TAKE_WAYBILL_REQ: //4.5.3.2.11 上报车辆电子运单请求消息
				break;
			default:
				break ;
			}
		}
		else if (msg_type == UP_PLATFORM_MSG)
		{

		}
		else if(msg_type == DOWN_PLATFORM_MSG)//4.5.4.2 从链路平台间信息交互业务
		{
		}
		else if (msg_type == UP_WARN_MSG_ADPT_INFO)
		{
		}
		else if(msg_type == DOWN_WARN_MSG)
		{
			//部平台下行的报警督办请求，直接转发给pas
		}
		else if (msg_type == UP_CTRL_MSG)
		{

		}
		else if(msg_type == DOWN_CTRL_MSG)
		{

		}
		else if (msg_type == UP_BASE_MSG)
		{

		}
		else if(msg_type == DOWN_BASE_MSG)
		{
		}
		else if(msg_type == DOWN_TOTAL_RECV_BACK_MSG)//4.5.2.1 接收车辆定位信息数量通知消息
		{
		}
		else
		{
			OUT_ERROR(ip,port,str_access_code.c_str(),"msg_type=%04x,received an invalid message",msg_type);
		}
		_pEnv->GetPasServer()->HandleClientData( coder.GetData(), coder.GetSize() ) ;
	}
}

void Transmit::on_dis_connection( socket_t *sock )
{
	if ( sock == _client._fd )
	{
		_client._user_state = User::OFF_LINE;
		OUT_WARNING(_client._ip.c_str(),_client._port,_client._user_id.c_str(),"uplink:on_dis_connnection");
		_filecache.Offline( _client._access_code ) ;
	}
	else if ( sock == _server._fd )
	{
		_server._user_state = User::OFF_LINE;
		OUT_WARNING(_server._ip.c_str(),_server._port,_server._user_id.c_str(),"downlink:on_dis_connnection");
	}
}

// 这里过来的数据是从内部过来不存的加密情况，不需要处理解密
void Transmit::HandleMasUpData( const char *data, int len )
{
	if ( len < (int)sizeof(Header) + (int)sizeof(Footer)){
		OUT_ERROR( NULL, 0, NULL, "Transmit::HandleMasUpData packet len error" );
		return;
	}
	
	//将CAS所上传上来的数据的seq和access_code重新设置
	Header *header 			 = (Header *) data ;
	unsigned short msg_type  = ntouv16(header->msg_type);
	header->msg_seq 		 = ntouv32( _proto_parse.get_next_seq() );
	header->access_code 	 = ntouv32(_access_code);

	// 如果下发失败
	if ( ! SendMasData( data, len ) ) {
		//  判断消息中是否包含定位信息
		if ( msg_type == UP_EXG_MSG ) {
			_filecache.WriteCache( (int)_client._access_code, (void*)data, len ) ;
		}
		return ;
	}
	OUT_SEND3(_client._ip.c_str(),_client._port,_client._user_id.c_str(),"%s", _proto_parse.Decoder( data, len ).c_str());
}

// 发送MAS的数据
bool Transmit::SendMasData( const char *data, int len )
{
	if ( _client._user_state != User::ON_LINE && _server._user_state != User::ON_LINE ) {
		return false ;
	}

	socket_t *sock = NULL ;
	// 还原成原来版本，优先主链路发送数据
	if ( _client._user_state == User::ON_LINE ) {
		sock = _client._fd;
	} else if(_server._user_state == User::ON_LINE) {
		sock = _server._fd;
	}

	if ( ! SendCrcData( sock, data , len ) ) {
		return false ;
	}
	// 添加上传流量统计
	_sendstat.AddFlux( len ) ;
	return true ;
}

void Transmit::TimeWork()
{
	OUT_INFO( NULL, 0, NULL, "void	Transmit::TimeWork()");

	time_t cur_time;
	time_t last_noop_time = time(NULL);
	time_t last_running   = time(NULL) ;
	while (1)
	{
		if ( ! Check() ) break ;

		/*每隔三秒钟做一次定时检测，若是超时的，的断开重新连接*/
		cur_time = time(0);

		bool conn = false;
		//as server
		if (_client._user_state != User::OFF_LINE && cur_time - _client._last_active_time > 3*60)
		{
			OUT_WARNING(NULL,0,"TimeWork","uplink timeout");
			_client._user_state = User::OFF_LINE;
		}

		if (_server._user_state != User::OFF_LINE && cur_time - _server._last_active_time > 3*60)
		{
			OUT_WARNING(NULL,0,"TimeWork","downlink timeout");
			_server._user_state = User::OFF_LINE;
		}

		if(_client._user_state == User::ON_LINE && cur_time - last_noop_time > 30) {
			//发送NOOP消息。
			UpLinkTestReq req;
			req.header.access_code = ntouv32(_access_code);
			req.header.msg_seq = ntouv32(_proto_parse.get_next_seq());
			if ( SendCrcData(_client._fd,(const char*)&req,sizeof(req)) ) {
				last_noop_time = cur_time ;
				OUT_INFO( _client._ip.c_str() , _client._port ,  NULL, "UP_LINKTEST_REQ" ) ;
			} else {
				OUT_ERROR( _client._ip.c_str() , _client._port ,  NULL, "UP_LINKTEST_REQ" ) ;
			}
		} else if( _client._user_state == User::OFF_LINE && cur_time - _client._login_time > 30 ) {

			if ( _client._fd != NULL ) {
				OUT_WARNING(_client._ip.c_str(), _client._port, _client._user_id.c_str(),
						"Client %d close socket", _client._fd->_fd );
				CloseSocket(_client._fd);
			}
			_client._fd = NULL;
			_client._user_state = User::OFF_LINE;
			_client._msg_seq = 0;
			_client._last_active_time = cur_time;
			_client._connect_info.keep_alive = AlwaysReConn;
			if(cur_time - _client._connect_info.last_reconnect_time > _client._connect_info.timeval){
				ConnectServer(_client,10);
			}
		}

		if(_server._user_state == User::OFF_LINE && _server._fd != NULL ) {
			OUT_INFO( _server._ip.c_str() , _server._port, _server._user_id.c_str(),
					"Server %d close socket", _server._fd->_fd );
			CloseSocket(_server._fd);
			_server._fd = NULL;
		}
		if(_server._user_state == User::ON_LINE || _client._user_state == User::ON_LINE)
			conn = true;

		if ( cur_time - last_running > 30  ) {

			_recvstat.Check( 30 ) ;
			_sendstat.Check( 30 ) ;

			last_running = cur_time ;
			float count = 0, speed = 0 ;
			_recvstat.GetFlux( count, speed ) ;

			float nsend = 0, nspeed = 0 ;
			_sendstat.GetFlux( nsend, nspeed ) ;

			OUT_RUNNING( NULL, 0, "ONLINE", "mas recv down count %f, speed %fkb, up count %f, speed %fkb",
					count, (float)speed/(float)DF_KB , nsend, (float)nspeed/(float)DF_KB ) ;
		}

		sleep(3);
	}
}

void Transmit::NoopWork()
{

}

bool Transmit::SendCrcData( socket_t *sock, const char* data, int len )
{
	// 处理循环码
	char *buf = new char[len+1] ;
	memset( buf, 0 , len+1 ) ;
	memcpy( buf, data, len ) ;

	// 处理加密数据
	EncryptData( (unsigned char *) buf , (unsigned int) len , true ) ;

	// 统一附加循环码的验证
	unsigned short crc_code = ntouv16( GetCrcCode( buf, len ) ) ;
	unsigned int   offset   = len - sizeof(Footer) ;
	// 替换循环码内存的位置数据
	memcpy( buf + offset , &crc_code, sizeof(short) ) ;

	C5BCoder coder ;
	coder.Encode( buf , len ) ;

	delete [] buf ;
	//OUT_HEX( sock->_szIp, sock->_port, "Send", coder.GetData(), coder.GetSize() ) ;

	return SendData( sock, coder.GetData(), coder.GetSize() ) ;
}

//-----------------------------缓存数据处理接口-----------------------------------------
// 处理外部数据
int Transmit::HandleQueue( const char *uid , void *buf, int len , int msgid )
{
	OUT_PRINT( NULL, 0, NULL, "HanldeQueue msg id %d , accesscode %s , data length %d" , msgid, uid, len ) ;

	switch( msgid ) {
	case DATA_FILECACHE:  // 用户不在线数据缓存
		{
			// 如果数据长度不正确直接返回
			if ( len < (int)sizeof(Header) ) {
				OUT_ERROR( NULL, 0, NULL, "HandleQueue filecache data length %d error", len ) ;
				return IOHANDLE_ERRDATA;
			}

			// OUT_INFO( NULL, 0, NULL, "HandleQueue %s" , _proto_parse.Decoder( (const char *)buf, len ).c_str() ) ;

			Header *header = (Header *) buf;
			unsigned short msg_type  = ntouv16(header->msg_type);
			// 如果不为扩展类消息
			if ( msg_type != UP_EXG_MSG ) {
				return IOHANDLE_ERRDATA;
			}

			// 如果为扩展类消息
			ExgMsgHeader *exg = (ExgMsgHeader*) ((const char *)(buf) + sizeof(Header) ) ;
			unsigned short data_type = ntouv16( exg->data_type ) ;
			if ( data_type != UP_EXG_MSG_REAL_LOCATION ) {
				return IOHANDLE_ERRDATA;
			}

			UpExgMsgRealLocation *req = ( UpExgMsgRealLocation *) buf ;

			UpExgMsgHistoryHeader msg ;
			memcpy( &msg, buf, sizeof(msg) ) ;
			msg.exg_msg_header.data_length = ntouv32( sizeof(char) + sizeof(GnssData) ) ;
			msg.exg_msg_header.data_type   = ntouv16(UP_EXG_MSG_HISTORY_LOCATION) ;
			msg.header.msg_len			   = ntouv32( sizeof(msg) + sizeof(char) + sizeof(GnssData) + sizeof(Footer) ) ;

			DataBuffer dbuf ;
			dbuf.writeBlock( &msg, sizeof(msg) ) ;
			dbuf.writeInt8( 1 ) ;
			dbuf.writeBlock( &req->gnss_data, sizeof(GnssData) ) ;

			Footer footer ;
			dbuf.writeBlock( &footer, sizeof(footer) ) ;

			if ( ! SendMasData( dbuf.getBuffer(), dbuf.getLength() ) ) {
				OUT_ERROR( NULL, 0, NULL, "UP_EXG_MSG_HISTORY_LOCATION:%s" , msg.exg_msg_header.vehicle_no ) ;
				return IOHANDLE_FAILED ;
			}
			// 添加上传流量统计
			_sendstat.AddFlux( dbuf.getLength() ) ;

			OUT_SEND3( NULL, 0, NULL, "UP_EXG_MSG_HISTORY_LOCATION:%s" , msg.exg_msg_header.vehicle_no ) ;
		}
		break;
	case DATA_ARCOSSDAT: // 跨数据补发消息
		{
			// 小于指定的数据长度直接返回了
			if ( len < (int)sizeof(ArcossData) ) {
				OUT_ERROR( NULL, 0, NULL, "HandleQueue arcossdat data length %d error", len ) ;
				return IOHANDLE_ERRDATA;
			}

			// 补发车辆定位信息请求消息
			ArcossData *p = ( ArcossData *) buf ;

			UpExgApplyHisGnssDataReq req ;
			req.header.msg_len  = ntouv32( sizeof(req) ) ;
			req.header.msg_seq  = ntouv32( _proto_parse.get_next_seq()) ;
			req.header.msg_type = ntouv16( UP_EXG_MSG ) ;
			req.header.access_code = ntouv32( _access_code ) ;
			req.exg_msg_header.vehicle_color = p->color ;
			safe_memncpy( req.exg_msg_header.vehicle_no, p->vechile, sizeof(req.exg_msg_header.vehicle_no) ) ;
			req.exg_msg_header.data_length   = ntouv32( sizeof(uint64) * 2 ) ;
			req.exg_msg_header.data_type	 = ntouv16( UP_EXG_MSG_APPLY_HISGNSSDATA_REQ ) ;
			req.start_time = ntouv64( p->time ) ;
			req.end_time   = ntouv64( time(NULL) ) ;

			if ( ! SendMasData( (const char *)&req, sizeof(req) ) ) {
				OUT_ERROR( NULL, 0, NULL, "UP_EXG_MSG_APPLY_HISGNSSDATA_REQ:%s" , p->vechile ) ;
				return IOHANDLE_FAILED ;
			}

			OUT_SEND3( NULL, 0, NULL, "UP_EXG_MSG_APPLY_HISGNSSDATA_REQ:%s" , p->vechile ) ;
		}
		break;
	}

	return IOHANDLE_SUCCESS ;
}



