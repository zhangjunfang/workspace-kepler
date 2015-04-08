/*
 * nodeclient.cpp
 *
 *  Created on: 2011-11-10
 *      Author: humingqing
 */

#include "nodeclient.h"
#include <netutil.h>
#include <waitgroup.h>
#include <tools.h>
#include <nodeparse.h>

CNodeClient::CNodeClient()
{
	_pEnv       = NULL ;
	_nodeid     = 0 ;
	_last_req   = 0 ;
	_enable     = false ;
	_pAlloc     = new CAllocMsg;
	_pBuilder   = new CMsgBuilder( _pAlloc ) ;
	_pWaitQueue = new CWaitGroup( _pAlloc ) ;
}

CNodeClient::~CNodeClient()
{
	Stop() ;

	if ( _pWaitQueue != NULL ) {
		delete _pWaitQueue ;
		_pWaitQueue = NULL ;
	}

	if ( _pAlloc != NULL ) {
		delete _pAlloc ;
		_pAlloc = NULL ;
	}
}

// 初始化
bool CNodeClient::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	//msg_node_id=100001
	//msg_node_server=127.0.0.1
	//msg_node_port=7555
	//msg_node_enable=0
	//msg_dev_name=eth0
	int nvalue = 0 ;
	if ( pEnv->GetInteger( "push_node_enable" , nvalue ) ) {
		_enable = ( nvalue == 1 ) ;
	}

	// 取得服务器IP和端口
	char buf[256] = {0} ;

	// 取得MSG的IP
	if ( pEnv->GetString( "msg_connect_ip", buf ) ) {
		_msg_ip = buf ;
	}

	// 取得MSG的端口
	if ( pEnv->GetInteger( "msg_connect_port", nvalue ) ) {
		_msg_port = nvalue ;
	}

	// MSG的用户名
	if ( pEnv->GetString( "msg_user_name" , buf ) ) {
		_msg_user = buf ;
	}
	// MSG的密码
	if ( pEnv->GetString( "msg_user_pwd", buf ) ) {
		_msg_pwd = buf ;
	}

	// 是否使用集群
	if ( !_enable ) {
		OUT_INFO( NULL, 0, "NodeClient" , "msg node disable" ) ;
		return true;
	}

	// 取得节点ID号
	if ( ! pEnv->GetInteger( "push_node_id" , nvalue ) ) {
		printf( "get msg node id failed\n" ) ;
		return false ;
	}
	_nodeid = nvalue ;

	if ( ! pEnv->GetString( "push_node_server" , buf ) ) {
		printf( "get msg node server failed\n" ) ;
		return false ;
	}
	_client_user._ip = buf ;

	// 取得节点服务器的端口
	if ( ! pEnv->GetInteger( "push_node_port" , nvalue ) ) {
		printf( "get msg node port failed\n" ) ;
		return false ;
	}
	_client_user._port = nvalue ;
	_client_user._login_time = 0 ;

	// 取得设备的名称
	if ( !pEnv->GetString( "push_dev_name" , buf ) ) {
		printf( "get msg dev name failed\n" ) ;
		return false ;
	}
	// 取得本机的IP的地址
	_push_ip = netutil::addrToString( netutil::getLocalAddr( buf ) ) ;

	// 取得MSG的监听端口
	if ( ! pEnv->GetInteger( "push_listen_port" , nvalue ) ) {
		printf( "get msg listen port failed\n" ) ;
		return false ;
	}
	_push_port = nvalue ;

	// 设置回调对象
	_pWaitQueue->SetNotify( this ) ;

	// 设置分包对象
	setpackspliter( &_pack_spliter ) ;

	return _pWaitQueue->Init() ;
}

// 开始线程
bool CNodeClient::Start( void )
{
	if ( ! _enable ) {
		// 如果非集群就直接添加一个就可以了
		_pEnv->GetMsgClient()->AddUser( _msg_ip.c_str(), (unsigned short)_msg_port, _msg_user.c_str(), _msg_pwd.c_str() ) ;
		return true ;
	}
	if ( ! StartClient( _client_user._ip.c_str() , _client_user._port , 1 ) ) {
		printf( "start node client failed\n" ) ;
		return false ;
	}
	return _pWaitQueue->Start() ;
}

// 停止线程
void CNodeClient::Stop( void )
{
	if ( ! _enable )
		return ;

	StopClient() ;
	// 停止等待队列
	_pWaitQueue->Stop() ;
}

// 数据到来时处理
void CNodeClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
	const char *ip      = sock->_szIp ;
	unsigned short port = sock->_port ;

	OUT_INFO( ip, port, "NodeClient" , "%s , length %d" , CNodeParser::Decode( (char*)data, len ) , len ) ;
	OUT_HEX( ip, port, NULL, (const char*)data , len ) ;

	if ( len < (int)sizeof(NodeHeader) ) {
		OUT_ERROR( ip, port , "NodeClient", "recv fd %d data len %d error" , sock->_fd, len ) ;
		return ;
	}

	NodeHeader *header = (NodeHeader *) (data) ;
	unsigned int mlen = ntohl( header->len ) ;
	// 较验数据的正确性
	if ( (int)(mlen + sizeof(NodeHeader)) != len ) {
		OUT_ERROR( ip, port, "NodeClient", "recv fd %d data len %d error" , sock->_fd, len ) ;
		return ;
	}

	// 记录最后一次回应的时间
	_client_user._last_active_time = time(NULL) ;

	unsigned int   seq = ntohl( header->seq ) ;
	unsigned short cmd = ntohs( header->cmd ) ;

	switch( cmd ) {
	case NODE_CONNECT_RSP:
		{
			if ( mlen == (uint32_t)sizeof(NodeLoginRsp) ) {
				NodeLoginRsp *rsp = (NodeLoginRsp*)( (char*)data + sizeof(NodeHeader) ) ;
				if ( rsp->result == 0 ) {
					_client_user._user_state = User::ON_LINE ;
				}

				// 取得当前时间
				time_t now = time(NULL) ;
				if ( now - _last_req > 60 ) {
					// 取得用户名和密码
					if ( _user_name.empty() && _user_pwd.empty() ) {
						// 申请用户名
						UserName() ;
					}
					_last_req = now ;
				}
			} else {
				OUT_ERROR( ip, port, "NodeClient" , "NodeLoginRsp size error" ) ;
			}
		}
		break ;
	case NODE_DISCONN_RSP:
	case NODE_LINKTEST_RSP:
		break ;
	case NODE_USERNAME_RSP:
		{
			// 如果返回的用户名成功
			if ( mlen == (uint32_t)sizeof(NodeUserNameRsp) ) {
				NodeUserNameRsp *rsp = ( NodeUserNameRsp *) ( (char*)data + sizeof(NodeHeader) ) ;

				char user[13] = {0}, pwd[9]   = {0} ;
				safe_memncpy( user, rsp->user.user, sizeof(rsp->user.user) ) ;
				safe_memncpy( pwd , rsp->user.pwd , sizeof(rsp->user.pwd ) ) ;
				// 添加用户操作
				_user_name = user ;  _user_pwd = pwd ;

				OUT_INFO( NULL, 0, "NODE" , "get login user %s pwd %s" , _user_name.c_str(), _user_pwd.c_str() ) ;

				// 收到用户名后开始变更服务器
				GetServerMsg() ;
			} else {
				// 取得用户名和密码
				if ( _user_name.empty() && _user_pwd.empty() ) {
					// 申请用户名
					UserName() ;
				}
				OUT_ERROR( NULL, 0, "NODE" , "get login user error" ) ;
			}
		}
		break ;
	case NODE_GETMSG_RSP:
		{
			if ( mlen > (uint32_t)sizeof(NodeGetMsgRsp) ) {
				char *p = ( char *) data + sizeof(NodeHeader) ;
				NodeGetMsgRsp *rsp = ( NodeGetMsgRsp *) p ;
				unsigned short num = ntohs( rsp->num ) ;
				// 检验长度的正确的性
				if ( mlen == sizeof(NodeGetMsgRsp) + num * sizeof(AddrInfo) ) {
					p += sizeof(NodeGetMsgRsp) ;

					for ( short i = 0; i < num; ++ i ) {
						AddrInfo *addr = ( AddrInfo *) p ;
						unsigned short port = ntohs( addr->port ) ;
						char ip[33] = {0} ;
						safe_memncpy( ip, addr->ip, sizeof(addr->ip) ) ;

						OUT_INFO( NULL, 0, "NODE" , "get msg server ip %s port %d" , ip, port ) ;
						// 变更服务器
						_pEnv->GetMsgClient()->AddUser( ip, port, _user_name.c_str(), _user_pwd.c_str() ) ;

						p += sizeof(AddrInfo) ;
					}
				} else {
					OUT_ERROR( NULL, 0, "NODE" , "get msg server length error" ) ;
				}
			} else {
				// 如果获取服务器失败则需要进一步发送请求直到成功
				GetServerMsg() ;
				OUT_ERROR( NULL, 0, "NODE" , "get msg server error" ) ;
			}
		}
		break ;
	case NODE_USERNOTIFY_REQ:
		break ;
	case NODE_MSGERROR_REQ:
		break ;
	case NODE_MSGCHG_REQ:
		{
			unsigned char result = 1 ;
			if ( mlen > (uint32_t) sizeof(NodeMsgChg) ) {
				char *p = (char*) data + sizeof(NodeHeader) ;
				NodeMsgChg *rsp = ( NodeMsgChg *) p ;

				unsigned short num = ntohs( rsp->num ) ;
				unsigned char  op  = rsp->op ;
				if ( mlen == sizeof(NodeMsgChg) + num * sizeof(AddrInfo) ) {
					p += sizeof(NodeMsgChg) ;
					if ( op == NODE_MSG_ADD ) { // 添加服务器
						for( short i = 0 ; i < num; ++ i ) {
							AddrInfo *addr = ( AddrInfo *) p ;
							unsigned short port = ntohs( addr->port ) ;
							char ip[33] = {0} ;
							safe_memncpy( ip, addr->ip, sizeof(addr->ip) ) ;
							_pEnv->GetMsgClient()->AddUser( ip, port, _user_name.c_str(), _user_pwd.c_str() ) ;
							p += sizeof(AddrInfo) ;
						}
					} else if ( op == NODE_MSG_DEL ) {  // 删除服务器
						for( short i = 0 ; i < num; ++ i ) {
							AddrInfo *addr = ( AddrInfo *) p ;
							unsigned short port = ntohs( addr->port ) ;
							char ip[33] = {0} ;
							safe_memncpy( ip, addr->ip, sizeof(addr->ip) ) ;
							_pEnv->GetMsgClient()->DelUser( ip, port ) ;
							p += sizeof(AddrInfo) ;
						}
					}
				}
				result = 0 ;
			}

			DataBuffer buf ;
			_pBuilder->BuildMsgChgResp( buf, seq, result ) ;
			if ( ! SendDataEx( sock, buf.getBuffer(), buf.getLength() ) ) {
				OUT_ERROR( NULL, 0, "NODE" , "fd %d Send msg chg resp failed", sock->_fd ) ;
			}
		}
		break ;
	}

	// 处理响应
	if ( cmd & 0x8000 ) {
		// 如果为响应需要处理等待队列
		_pWaitQueue->DelQueue( seq, sock, true ) ;
	}
}

// 断开连接处理
void CNodeClient::on_dis_connection( socket_t *sock )
{
	OUT_WARNING( sock->_szIp, sock->_port, _client_user._user_id.c_str(), "Disconnection fd %d" , sock->_fd );
	_client_user._user_state = User::OFF_LINE ;
}

// 定时线程
void CNodeClient::TimeWork()
{
	while(1) {
		if ( ! Check() ) break ;

		time_t now = time(NULL) ;
		if ( _client_user._user_state == User::OFF_LINE ) {
			if( now- _client_user._login_time > 60 ) {
				ConnectServer( _client_user, 60 ) ;
			}
		} else if ( _client_user._user_state == User::ON_LINE ) {
			// 处理心跳
			if ( now - _client_user._last_active_time > 180 ) {
				// 断开连接重连
				if ( _client_user._fd != NULL ) {
					CloseSocket( _client_user._fd ) ;
				}
				// 重连
				ConnectServer( _client_user , 60 ) ;

			}else if ( now - _client_user._last_active_time > 30 ) {
				// 发送心跳测试链路
				SendLinkTest() ;
			}
		}
		//这个地方，超时的同步有问题
		sleep(5);
	}
}

// 心跳线程
void CNodeClient::NoopWork()
{

}

// 构建登陆的消息
int CNodeClient::build_login_msg( User &user ,char *buf, int buf_len )
{
	AddrInfo addr ;
	safe_memncpy( addr.ip, _push_ip.c_str(), sizeof(addr.ip) ) ;
	addr.port = _push_port ;

	MsgData *msg = _pBuilder->BuildLoginReq( _nodeid, FD_NODE_STORE , addr ) ;

	DataBuffer mbuf ;
	_pBuilder->BuildMsgBuffer( mbuf, msg ) ;
	_pWaitQueue->AddGroup( _client_user._fd, msg->seq, msg ) ;

	memcpy( buf, mbuf.getBuffer(), mbuf.getLength() ) ;

	return mbuf.getLength() ;
}

// 发送心跳数据
void CNodeClient::SendLinkTest( void )
{
	// 取得用户在线压力
	int ncar = 0 ;  // ToDo: 需要取得当前压力上报

	MsgData *msg = _pBuilder->BuildLinkTestReq( ncar ) ;

	DataBuffer buf ;
	_pBuilder->BuildMsgBuffer( buf, msg ) ;
	_pWaitQueue->AddGroup( _client_user._fd, msg->seq, msg ) ;

	if ( ! SendDataEx( _client_user._fd, buf.getBuffer(), buf.getLength() ) ) {
		OUT_ERROR( NULL, 0, NULL, "SendLinkTest fd %d failed" , _client_user._fd ) ;
		_pWaitQueue->DelQueue( msg->seq,  _client_user._fd, true ) ;
		return ;
	}
}

// 申请用户名
void CNodeClient::UserName( void )
{
	MsgData *msg = _pBuilder->BuildUserNameReq() ;

	DataBuffer buf ;
	_pBuilder->BuildMsgBuffer( buf, msg ) ;
	_pWaitQueue->AddGroup( _client_user._fd, msg->seq, msg ) ;

	if ( ! SendDataEx( _client_user._fd, buf.getBuffer(), buf.getLength() ) ) {
		OUT_ERROR( NULL, 0,NULL, "Send UserName fd %d failed", _client_user._fd ) ;
		_pWaitQueue->DelQueue( msg->seq, _client_user._fd, true ) ;
		return ;
	}
}

// 申请分配可用的MSG
void CNodeClient::GetServerMsg( void )
{
	MsgData *msg = _pBuilder->BuildGetmsgReq() ;

	DataBuffer buf ;
	_pBuilder->BuildMsgBuffer( buf, msg ) ;
	_pWaitQueue->AddGroup( _client_user._fd, msg->seq, msg ) ;

	if ( ! SendDataEx( _client_user._fd, buf.getBuffer(), buf.getLength() ) ) {
		OUT_ERROR( NULL, 0,NULL, "Send Server Msg fd %d failed", _client_user._fd ) ;
		_pWaitQueue->DelQueue( msg->seq, _client_user._fd, true ) ;
		return ;
	}
}

void CNodeClient::NotifyMsgData( socket_t *sock , MsgData *p , ListFd &fds, unsigned int op )
{
	// 如果发送响应超时说明节点异常直接断连
	switch( p->cmd )
	{
	case NODE_USERNAME_REQ:
		if ( op == MSG_TIMEOUT ) {
			// 取得用户名和密码
			if ( _user_name.empty() && _user_pwd.empty() ) { // 如果超时需要重发
				// 申请用户名
				UserName() ;
			}
		}
		break ;
	case NODE_GETMSG_REQ:
		if ( op == MSG_TIMEOUT ) { // 如果超时需要重发
			GetServerMsg() ;
		}
		break ;
	}
	OUT_INFO( sock->_szIp, sock->_port, "NodeClient" , "%s seq %d, fd %d cmd %04x length %d" ,
			( op == MSG_TIMEOUT ) ? "timeout" : "success" , p->seq, sock->_fd, p->cmd, p->buf.getLength() ) ;

}

// 重载发送数据函数
bool CNodeClient::SendDataEx( socket_t *sock, const char *data, int len )
{
	if ( sock == NULL )
		return false;

	if ( ! SendData( sock, data, len ) ) {
		OUT_ERROR( sock->_szIp, sock->_port, "Send" , "send data error fd %d" , sock->_fd ) ;
		OUT_HEX( sock->_szIp, sock->_port, "Send", data, len ) ;
		return false ;
	}
	OUT_SEND( sock->_szIp, sock->_port,  "Send" , "%s , fd %d, length %d" , CNodeParser::Decode( data, len ) ,  sock->_fd, len ) ;
	OUT_HEX( sock->_szIp, sock->_port, "Send", data, len ) ;

	return true ;
}

