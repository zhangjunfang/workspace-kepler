/*
 * msgmgr.cpp
 *
 *  Created on: 2011-11-3
 *      Author: humingqing
 */

#include "nodemgr.h"
#include "netutil.h"
#include <comlog.h>
#include <tools.h>
#include <msgbuilder.h>
//////////////////////////////// 组的管理对象 ////////////////////////////////////
CGroupMgr::CGroupMgr()
{
}

CGroupMgr::~CGroupMgr()
{
	share::Guard guard( _mutex ) ;
	if( _mapClient.empty() )
		return ;
	// 回收内存
	CMapClient::iterator  it ;
	for ( it = _mapClient.begin(); it != _mapClient.end(); ++ it ) {
		delete it->second ;
	}
	_mapClient.clear() ;
}

// 添加到当前的客户组里面
void CGroupMgr::AddGroup( CFdClient *p )
{
	share::Guard guard( _mutex ) ;
	CGroupClient *pGroup = NULL ;
	CMapClient::iterator it = _mapClient.find( p->_group ) ;
	if ( it == _mapClient.end() ) {
		pGroup = new CGroupClient;
		_mapClient.insert( make_pair( p->_group, pGroup ) ) ;
	} else {
		pGroup = it->second ;
	}
	// 添加对应的组
	pGroup->AddClient( p ) ;
}

// 根FD移除对应的Client
bool CGroupMgr::RemoveGroup( CFdClient *p )
{
	share::Guard guard( _mutex ) ;
	CMapClient::iterator it = _mapClient.find( p->_group ) ;
	if ( it == _mapClient.end() ) {
		return false ;
	}
	// 从组中删除元素
	CGroupClient *pGroup = it->second ;
	if ( ! pGroup->RemoveClient( p ) ) {
		return false ;
	}
	// 如果当前组的用户为零直接去掉当前组
	if ( pGroup->GetSize() == 0 ) {
		_mapClient.erase( it ) ;
		delete pGroup ;
	}
	return true ;
}

// 取得当前组的数据
int CGroupMgr::GetGroup( uint16_t group , CVecClient &vec )
{
	share::Guard guard( _mutex ) ;
	CMapClient::iterator it = _mapClient.find( group ) ;
	if ( it == _mapClient.end() ) {
		return 0 ;
	}
	CGroupClient *pGroup = it->second ;
	return pGroup->GetClients( vec ) ;
}

// 取得当前组的所有子组名称
int CGroupMgr::GetGroupIDs( uint16_t group, CVecGroupIds &vec )
{
	share::Guard guard( _mutex ) ;
	if ( _mapClient.empty() )
		return 0 ;

	std::set<uint16_t>  setGroup ;
	std::set<uint16_t>::iterator itx ;

	CMapClient::iterator it ;
	for ( it = _mapClient.begin(); it != _mapClient.end(); ++ it ) {
		uint16_t ngroup = it->first ;
		if ( !( ngroup & group ) ) {
			continue ;
		}

		// 查找组是否已经添加如果没有则添加
		itx = setGroup.find( ngroup ) ;
		if ( itx == setGroup.end() ) {
			vec.push_back( ngroup ) ;
			setGroup.insert( std::set<uint16_t>::value_type( ngroup ) ) ;
		}
	}
	return (int) vec.size() ;
}

/////////////////////////////// MSG管理对象 /////////////////////////////////

CMsgManager::CMsgManager(ISystemEnv *pEnv , CMsgBuilder *builder )
{
	_pEnv    = pEnv ;
	_builder = builder ;
}

CMsgManager::~CMsgManager()
{

}

// 添加MSG服务器
bool CMsgManager::AddMsgServer( CMsgServer *p )
{
	uint64_t id = netutil::strToAddr( p->_ip.c_str(), p->_port ) ;
	if ( id <= 0 ) return false;

	share::Guard guard( _msgmutex ) ;

	CMapMsgSrv::iterator it = _mapSrv.find( id ) ;
	if ( it != _mapSrv.end() ) {
		return false ;
	}
	// 添加MSG服务器
	_mapSrv.insert( make_pair( id, p ) ) ;

	return true ;
}

// 添加客户端
int CMsgManager::AddClient( uint64_t serverid , CFdClient *p )
{
	share::Guard guard( _msgmutex ) ;

	CMapMsgSrv::iterator it = _mapSrv.find( serverid ) ;
	if ( it == _mapSrv.end() ) {
		return 0 ;
	}

	CMsgServer *pMsg = it->second ;
	pMsg->AddClient( p ) ;

	// 这里返回添加该类型个数
	return pMsg->GetGroupSize( p->_group ) ; // 如果为前置机类型接入就需要处理添加存储
}

// 查找MSG
CMsgServer * CMsgManager::FindMsgNode( CFdClient *pmsg , bool erase )
{
	CMsgServer *pMsg = NULL ;

	uint64_t id = netutil::strToAddr( pmsg->_ip.c_str(), pmsg->_port ) ;

	CMapMsgSrv::iterator it = _mapSrv.find( id ) ;
	if ( it == _mapSrv.end() ) {
		return NULL ;
	}

	// 取得当前MSG
	pMsg = it->second ;
	if ( erase ) {
		// 先移除后发送通知
		_mapSrv.erase( it ) ;
	}

	return pMsg ;
}

bool CMsgManager::RemoveMsgNode( CFdClient *pmsg )
{
	share::Guard guard( _msgmutex ) ;
	CMsgServer *pMsg = FindMsgNode( pmsg, true ) ;
	if ( pMsg == NULL ){
		return false ;
	}

	CVecClient vec ;
	// 获取当前连接所有前置机
	int size = pMsg->GetClients( vec ) ;
	if ( size > 0 ) {

		AddrInfo addr ;
		safe_memncpy((char*) addr.ip, pmsg->_ip.c_str() , sizeof(addr.ip) ) ;
		addr.port  =  pmsg->_port ;
		// 构建通知消息
		MsgData *pdata = _builder->BuildMsgChgReq( NODE_MSG_DEL, &addr, 1 ) ;
		if ( ! _pEnv->GetWaitGroup()->AddGroup( pmsg->_fd, pdata->seq, pdata ) ){
			OUT_ERROR( NULL, 0, "MsgManger", "add group seq id %u failed" , pdata->seq ) ;
		}

		DataBuffer buf ;
		_builder->BuildMsgBuffer( buf, pdata ) ;

		// 处理MSG的变更消息
		CFdClient *pClient = NULL ;
		for ( int i = 0; i < size; ++ i ) {
			// 处理发送的消息
			pClient = vec[i] ;
			// 异步处理先添加到队列后处理
			_pEnv->GetWaitGroup()->AddQueue( pdata->seq, pClient->_fd ) ;
			// 处理MSG DOWN后，通知其所有前置机转移连接
			if ( ! _pEnv->GetNodeSrv()->HandleData( pClient->_fd, buf.getBuffer(), buf.getLength() )  ){
				// 如果清理失败则直接删除
				_pEnv->GetWaitGroup()->DelQueue( pdata->seq, pClient->_fd , false ) ;
			}
			// FD_CONN_PIPE,
			pClient->DelMsgClient( pmsg ) ;
		}
	}
	OUT_INFO( NULL, 0, "NodeMgr" , "remove msg node fd %d, ip %s, port %d , client size %d" ,
			pmsg->_fd, pmsg->_ip.c_str(), pmsg->_port , size ) ;
	return true ;
}

// 移除对应的客户端分为三类前置机，MSG以及存储
bool CMsgManager::RemoveClient( CFdClient *p )
{
	if ( p->_group & FD_NODE_MSG ) {
		// 如果为MSG节点
		return RemoveMsgNode( p ) ;
	}

	share::Guard guard( _msgmutex ) ;
	CVecClient &vec = p->GetMsgClients();
	if ( vec.empty() ) {
		return false ;
	}

	for ( int i = 0 ; i < (int) vec.size(); ++ i ) {
		// 取得当前MSG
		CMsgServer *pMsg = FindMsgNode( vec[i], false ) ;
		if ( pMsg == NULL )
			continue ;
		// 否则直接移除当前MSG的对象
		pMsg->RemoveClient( p ) ;
	}
	return true ;
}

// 纷发已添加的用户名
bool CMsgManager::DispatherUsers( socket_t *sock, UserInfo *p , int count )
{
	if ( count == 0 )
		return false ;

	// 构建消息
	MsgData *msg = _builder->BuildUserNotifyReq( p, count , 0 ) ;

	DataBuffer buf ;
	_builder->BuildMsgBuffer( buf, msg ) ;

	// 添加到等待队列中
	_pEnv->GetWaitGroup()->AddGroup( sock, msg->seq, msg ) ;
	// 发送已添加的用户列表给MSG
	return _pEnv->GetNodeSrv()->HandleData( sock, buf.getBuffer(), buf.getLength() ) ;
}

// 取得所有的MSG列表
bool CMsgManager::GetMsgList( CVecClient &vec )
{
	share::Guard guard( _msgmutex ) ;

	if ( _mapSrv.empty() )
		return false ;

	CMsgServer *p = NULL ;
	CMapMsgSrv::iterator it ;
	for ( it = _mapSrv.begin(); it != _mapSrv.end(); ++ it ) {
		p = it->second ;
		// 取得所有可用的MSG
		if ( ! p->IsOnline() ) {
			continue ;
		}
		vec.push_back( p ) ;
	}
	return ( ! vec.empty() ) ;
}

//////////////////// 针对Client的管理  /////////////////////////////
CFdClientMgr::CFdClientMgr(ISystemEnv *pEnv, CMsgBuilder *pBuilder )
{
	_pEnv 	   = pEnv ;
	_pBuilder  = pBuilder ;
	_pUserMgr  = new CUserMgr( pEnv->GetUserPath() );  // 取得当前用户数据保存位置
	// 处理MSG组的管理
	_pGroupMgr = new CGroupMgr;
	_pMsgMgr   = new CMsgManager( pEnv, pBuilder ) ;
}

CFdClientMgr::~CFdClientMgr()
{
	if ( _pGroupMgr != NULL ) {
		delete _pGroupMgr ;
		_pGroupMgr = NULL ;
	}
	if ( _pMsgMgr != NULL ) {
		delete _pMsgMgr ;
		_pMsgMgr = NULL ;
	}
	if ( _pUserMgr != NULL ) {
		delete _pUserMgr ;
		_pUserMgr = NULL ;
	}
}

// 登陆服务器平台
bool CFdClientMgr::LoginClient( socket_t *sock, unsigned int seq, NodeLoginReq *req )
{
	// 先将原来的连接客户端删除，主要重复排照
	RemoveFdClient( sock ) ;

	// 添加正式管理对象
	unsigned char result = RESULT_SUCCESS ;
	unsigned short group = ntohs( req->group ) ;
	unsigned int id = ntohl( req->id ) ;

	// 构建FdClient
	CFdClient *p = NULL ;
	//ToDo: check login id
	if ( id == 0 || !(group & 0xf000) ) {
		result = RESULT_FAILED ;
	} else {
		// 如果为MSG的服务器
		if ( group & FD_NODE_MSG ) {
			p = new CMsgServer ;
		} else { // 否则为客户端
			p = new CFdClient ;
		}
		p->_fd	   = sock ;
		p->_id     = id ;
		p->_group  = group ;
		p->_ip 	   = ( strlen((char*)req->addr.ip) == 0 ) ? "0.0.0.0" : (char*)req->addr.ip ;
		p->_port   = ntohs( req->addr.port ) ;
		sock->_ptr = p ;

		// 添加管理对象中
		if ( ! AddClient( p ) ) {
			delete p ;
			result = RESULT_FAILED ;
		}
	}

	DataBuffer buf ;
	// 构建应答回应
	_pBuilder->BuildLoginResp( buf, seq, result ) ;

	// 添加成功处理
	if ( result == RESULT_SUCCESS &&
			_pEnv->GetNodeSrv()->HandleData( sock, buf.getBuffer(), buf.getLength() ) ) {
		if ( group & FD_NODE_MSG ) {
			// 设置MSG的状态为等待回应
			((CMsgServer *)p)->SetWaitUser() ;
			// 如果为MSG需要纷发用户列表
			if ( ! DispatchUser( sock ) ) {
				// 设置MSG的状态为在线状态
				((CMsgServer *)p)->SetOnline() ;
				// 如果没用户数据就直接返回了
				return true ;
			}
			// 需要处理MSG的增加的通知，当后续成功应答再进一步处理
		}
		// 记录日志
		OUT_INFO( sock->_szIp, sock->_port, "NodeMgr" , "add %s node ip %s port %d , fd %d, id %d" ,
						(group&FD_NODE_MSG) ? "msg" : "client",  (char*)req->addr.ip, p->_port , sock->_fd , id ) ;

		return true ;
	} else {
		OUT_ERROR( sock->_szIp, sock->_port, "NodeMgr", "add %s node failed, ip %s , fd %d, id %d, result %d" ,
				(group&FD_NODE_MSG) ? "msg" : "client", (char*)req->addr.ip, sock->_fd , id , result ) ;
	}

	return false;
}

// 通知添加MSG的操作
void CFdClientMgr::NotifyAddMsg( CFdClient *p )
{
	CVecClient vec ;
	_pGroupMgr->GetGroup( FD_NODE_STORE, vec ) ;
	_pGroupMgr->GetGroup( FD_NODE_WEB  , vec ) ;

	// 如果没有节点直接返回成功
	if ( vec.empty() ) return ;

	// 取得孤立的存储和WEB节点
	AddrInfo addr ;
	safe_memncpy( (char*)addr.ip, p->_ip.c_str(), sizeof(addr.ip) ) ;
	addr.port = p->_port ;

	DataBuffer dbuf ;
	MsgData *msg = _pBuilder->BuildMsgChgReq( NODE_MSG_ADD, &addr, 1 ) ;
	_pBuilder->BuildMsgBuffer( dbuf, msg ) ;

	// 转化为服务器的ID号
	uint64_t msgid = netutil::strToAddr( p->_ip.c_str(), p->_port ) ;

	// 添加到等待组里面
	_pEnv->GetWaitGroup()->AddGroup( p->_fd, msg->seq, msg ) ;
	// 群发通知所有孤立的节点
	for ( int i = 0; i < (int)vec.size(); ++ i ) {
		CFdClient *pClient = vec[i] ;
		_pEnv->GetWaitGroup()->AddQueue( msg->seq, pClient->_fd ) ;
		if ( ! _pEnv->GetNodeSrv()->HandleData( pClient->_fd, dbuf.getBuffer(), dbuf.getLength() ) ){
			// 发送失败从队列中回收
			_pEnv->GetWaitGroup()->DelQueue( msg->seq, pClient->_fd, false ) ;
		} else {
			// 通知单节点时需要添加与MSG对应关系
			_pMsgMgr->AddClient( msgid, pClient ) ;
		}
	}
	OUT_PRINT( NULL, 0, "NodeMgr", "handle NODE_MSG_ADD message , ip %s port %d" , p->_ip.c_str() , p->_port ) ;
	OUT_HEX( NULL, 0, "NodeMgr" , dbuf.getBuffer() , dbuf.getLength() ) ;
}

// 注销登陆平台
bool CFdClientMgr::LogoutClient( socket_t *sock, unsigned int seq )
{
	unsigned char result = RESULT_SUCCESS ;
	if ( ! RemoveFdClient( sock ) ) {
		result = RESULT_FAILED ;
	}

	DataBuffer buf ;
	// 构建登陆响应
	_pBuilder->BuildLogoutResp( buf, seq, result ) ;

	return ( result == RESULT_SUCCESS &&
			_pEnv->GetNodeSrv()->HandleData( sock, buf.getBuffer(), buf.getLength() ) ) ;
}

// 链路测试
bool CFdClientMgr::LinkTest( socket_t *sock, unsigned int seq , NodeLinkTestReq *req )
{
	// 获取得当前FD对象
	CFdClient *p = GetFdClient( sock ) ;
	if ( p == NULL )  {
		OUT_ERROR( sock->_szIp, sock->_port, "FdClientMgr" , "LinkTest fd %d", sock->_fd ) ;
		return false ;
	}

	p->_last = time(NULL) ;
	p->_ncar = ntohl( req->num ) ;

	DataBuffer buf ;
	_pBuilder->BuildLinkTestResp( buf, seq ) ;

	// 返回心跳响应
	return ( _pEnv->GetNodeSrv()->HandleData( sock, buf.getBuffer(), buf.getLength() ) ) ;
}

// 请求分配用户名
bool CFdClientMgr::UserName( socket_t *sock, unsigned int seq )
{
	// 获取得当前FD对象
	CFdClient *pFd = GetFdClient( sock ) ;
	if ( pFd == NULL )  {
		OUT_ERROR( sock->_szIp, sock->_port, "FdClientMgr" , "UserName fd %d", sock->_fd ) ;
		return false ;
	}
	pFd->_last = time(NULL) ;

	CVecClient vec ;
	// 取得当前的MSG添加用户名
	if ( ! _pMsgMgr->GetMsgList( vec ) ) {
		// 如果没有MSG就不能获取用户名了
		OUT_WARNING( NULL, 0, "FdClientMgr" , "UserName Msg List empty" ) ;
		// return false ;
	}

	char user[13] = {0};
	char pwd[9]   = {0} ;

	char szid[1024] = {0} ;
	sprintf( szid, "%u_%u_%llu", pFd->_id, pFd->_group, netutil::strToAddr( sock->_szIp, pFd->_port ) ) ;
	bool exist =  _pUserMgr->GetUser( szid , user, pwd ) ;

	UserInfo info ;
	safe_memncpy( info.user ,user , sizeof(info.user) ) ;
	safe_memncpy( info.pwd  ,pwd  , sizeof(info.pwd ) ) ;

	DataBuffer buf ;
	MsgData *p = _pBuilder->BuildUserNotifyReq( &info, 1, seq ) ;
	_pBuilder->BuildMsgBuffer( buf, p ) ;

	_pEnv->GetWaitGroup()->AddGroup( sock, seq, p ) ;

	// 如果为新生成用户需要通知所有MSG更新,如果没有MSG就直接返回用户名
	if ( ! exist && ! vec.empty() ){
		for ( int i = 0; i < (int) vec.size(); ++ i ) {
			CFdClient *pClient = vec[i] ;
			// 异步处理先添加到队列后处理
			_pEnv->GetWaitGroup()->AddQueue( p->seq, pClient->_fd ) ;
			// printf( "add seq id %d fd %d\n" , p->seq, pClient->_fd ) ;
			// 如果为新添加的用户需要广播所有存在的MSG
			if ( ! _pEnv->GetNodeSrv()->HandleData( pClient->_fd, buf.getBuffer(), buf.getLength() )  ){
				// 如果清理失败则直接删除
				_pEnv->GetWaitGroup()->DelQueue( p->seq, pClient->_fd , false ) ;
			}
		}
	} else {  // 否则就直接返回存在的用户名
		// 如果已存就直从等待中去掉了，由队列回调处理通知节点
		_pEnv->GetWaitGroup()->DelQueue( seq, sock , true ) ;
	}
	return true ;
}

// 请求分配MSG列表
bool CFdClientMgr::GetMsgList( socket_t *sock, unsigned int seq )
{
	// 获取得当前FD对象
	CFdClient *p = GetFdClient( sock ) ;
	if ( p == NULL )  {
		OUT_ERROR( sock->_szIp, sock->_port, "FdClientMgr" , "GetMsgList fd %d", sock->_fd ) ;
		return false ;
	}
	p->_last = time(NULL) ;

	// 如果所属组错就不需要处理
	if ( !(p->_group & 0x7000) ) {
		OUT_ERROR( sock->_szIp, sock->_port, "FdClientMgr" , "GetMsgList fd %d group %d error" , sock->_fd, p->_group  ) ;
		return false ;
	}

	CVecClient vecMsg ;
	// 取得所有MSG
	if ( ! _pMsgMgr->GetMsgList(vecMsg) ) {
		OUT_ERROR( sock->_szIp, sock->_port, "CFdClientMgr" , "Get msg list empty fd %d" , sock->_fd ) ;
		return false ;
	}

	// 移除原来有的MSG的关系
	_pMsgMgr->RemoveClient( p ) ;
	// 清空原有的MSG数据列表
	p->ClearMsgClients() ;

	// 存放数据BUF
	DataBuffer buf ;
	// 如果为单一的存储或者WEB的处理需要添加所有MSG列表
	if ( p->_group ==  FD_NODE_STORE || p->_group == FD_NODE_WEB ) {
		// 取得MSG的个数
		int size = (int) vecMsg.size() ;
		AddrInfo *addrs = new AddrInfo[size] ;
		for ( int i = 0; i < size; ++ i ) {
			CFdClient * tmp = vecMsg[i] ;
			safe_memncpy( (char*)addrs[i].ip, tmp->_ip.c_str() , sizeof(addrs[i].ip) ) ;
			addrs[i].port = tmp->_port ;
			// 添加到对应的客户连接上
			_pMsgMgr->AddClient( netutil::strToAddr( tmp->_ip.c_str(), tmp->_port ) , p ) ;
		}
		// 返回MSG的服务器的列表
		_pBuilder->BuildGetMsgResp( buf, seq, addrs, size ) ;

		delete [] addrs ;

	} else if ( p->_group & FD_NODE_PIPE ) { // 如果管道则为前置机选一个可用的MSG负载轻的且有存储的
		// 取得当前组的前置机
		CVecClient vecgroup ;
		// 取得当前组的元素
		_pGroupMgr->GetGroup( p->_group, vecgroup ) ;

		CVecGroupIds ids ;
		_pGroupMgr->GetGroupIDs( FD_NODE_STORE, ids ) ;

		int min_store = 1024 , min_nostore = 1024 ;
		int index_store = 0, index_nostore = 0 ;
		int size = (int) vecMsg.size() ;
		for ( int i = 0 ; i < size; ++ i ) {
			CMsgServer *tmp = ( CMsgServer *) vecMsg[i] ;
			// 均衡分布前置机的简单算法
			int nstore = 0 ;
			if ( ! ids.empty() ) {
				for ( int k = 0; k < (int)ids.size(); ++ k ) {
					nstore += tmp->GetGroupSize( ids[k] ) ;
				}
			}
			// 取得当前前置机个数
			int npipe = tmp->GetGroupSize( p->_group ) ;
			if ( nstore > 0 ) {  // 有存储最小对象
				// 取得所有客户连接
				if ( npipe < min_store ) {
					min_store   = npipe ;
					index_store = i ;
				}
			} else { // 无存储对最小对象
				if ( npipe < min_nostore ) {
					min_nostore   = npipe ;
					index_nostore = i ;
				}
			}
		}

		// 这里分为有存储和没存储连接情况，优先有存储的处理
		int index = ( ! ids.empty() ) ? index_store : index_nostore ;

		// 取得FdClient对象
		CFdClient *pFd =  vecMsg[index] ;

		AddrInfo addr ;
		safe_memncpy( addr.ip, pFd->_ip.c_str(), sizeof(addr.ip) ) ;
		addr.port = pFd->_port ;
		//  取得对应的响应
		_pBuilder->BuildGetMsgResp( buf, seq, &addr, 1 ) ;
		// 添加到对应的客户连接上
		_pMsgMgr->AddClient( netutil::strToAddr( pFd->_ip.c_str(), pFd->_port ) , p ) ;

	} else { // 或者为分组的存储，优先分配新添加的MSG，再次分配负载相对较重的MSG
		float max = 0.0f ;
		int index = 0 , size = (int) vecMsg.size() ;
		// 处理权重找一个权重最大的处理
		for ( int i = 0 ; i < size; ++ i ) {
			CMsgServer *tmp = (CMsgServer*)vecMsg[i] ;
			int  nstore = tmp->GetGroupSize( p->_group ) ;
			float value = ( nstore == 0 ) ? 1.0f : (  (float)tmp->_ncar / (float)(nstore * 10000000 ) ) ;
			if ( value > max ) {
				max   = value ;
				index = i ;
			}
		}

		// 取得FdClient对象
		CFdClient *pFd =  vecMsg[index] ;

		AddrInfo addr ;
		safe_memncpy( addr.ip, pFd->_ip.c_str(), sizeof(addr.ip) ) ;
		addr.port = pFd->_port ;
		//  取得对应的响应
		_pBuilder->BuildGetMsgResp( buf, seq, &addr, 1 ) ;
		// 添加到对应的客户连接上
		_pMsgMgr->AddClient( netutil::strToAddr( pFd->_ip.c_str(), pFd->_port ) , p ) ;
	}

	// 返回发送是否成功
	if ( ! _pEnv->GetNodeSrv()->HandleData( sock, buf.getBuffer(), buf.getLength() ) ) {
		// 发送失败需要处理添加MSG与结点关系
		_pMsgMgr->RemoveClient( p ) ;
		return false ;
	}
	return true ;
}

// 处理申请用户名的结果
bool CFdClientMgr::ProcUserName( socket_t *sock, unsigned int seq , MsgData *p )  // 这个序号来自申请用户名的序号
{
	if ( p->buf.getLength() < (int)sizeof(UserInfo) ) {
		// 如果返回不正确直接返回
		OUT_ERROR( NULL, 0, "FdClientMgr", "ProcUserName data length error" ) ;
		return false ;
	}

	// 获取得当前FD对象
	CFdClient *pFd = GetFdClient( sock ) ;
	if ( pFd == NULL )  {
		OUT_ERROR( sock->_szIp, sock->_port, "FdClientMgr" , "GetFdClient failed fd %d" , sock->_fd ) ;
		return false ;
	}
	pFd->_last = time(NULL) ;

	// 如果为MSG就直接返回了
	if ( pFd->_group == FD_NODE_MSG ){
		// 将其转为MSG对象
		CMsgServer *pmsg = (CMsgServer*) pFd ;
		// 检测MSG是否为等待纷发请求状态
		if ( pmsg->IsWaitUser() ) {
			pmsg->SetOnline() ;   // 将MSG报为上线状态
			NotifyAddMsg( pFd ) ; // 通知所有单节点服务器加载此MSG
		}
		return true ;
	}

	// 用户名对象
	UserInfo info ;
	// 从缓存取得分配的用户名
	p->buf.fetchBlock( sizeof(NodeUserNotify), &info, sizeof(UserInfo) ) ;

	// 返回申请用户名的响应
	DataBuffer buf ;
	_pBuilder->BuildUserNameResp( buf, p->seq, &info, 1 ) ;

	// 返回对应数据
	return ( _pEnv->GetNodeSrv()->HandleData( sock, buf.getBuffer(), buf.getLength() ) ) ;
}

// 重新补发未收到的数据
bool CFdClientMgr::ResendMsg( socket_t *sock, MsgData *p , ListFd &fds )
{
	unsigned int count = _seqref.AddRef( p->seq ) ;
	// 如果重发次大于最大重发次数，就不再重发了
	if ( count > MAX_RESEND_TIME ) {
		_seqref.DelRef( p->seq ) ;
		return false ;
	}

	// 需要重新拷贝一下数据
	MsgData *temp = _pEnv->GetAllocMsg()->AllocMsg() ;
	temp->cmd     = p->cmd ;
	temp->seq	  = p->seq ;
	temp->buf.writeBlock( p->buf.getBuffer(), p->buf.getLength() ) ;

	DataBuffer buf ;
	_pBuilder->BuildMsgBuffer( buf, temp ) ;
	// 添加到等待的组里面
	_pEnv->GetWaitGroup()->AddGroup( sock, temp->seq , temp ) ;

	// 如果没需要发送的FD，则是主FD发送
	if ( fds.empty() ) {
		_pEnv->GetNodeSrv()->HandleData( sock, buf.getBuffer(), buf.getLength() ) ;
	} else {
		ListFd::iterator it ;
		// 如果群发消息，直接群发处理
		for ( it = fds.begin(); it != fds.end(); ++ it ) {
			socket_t *nfd = (*it) ;
			_pEnv->GetWaitGroup()->AddQueue( temp->seq, nfd ) ;
			_pEnv->GetNodeSrv()->HandleData( nfd, buf.getBuffer(), buf.getLength() )  ;
		}
	}
	return true ;
}

// 重新补发用户列表数据
bool CFdClientMgr::DispatchUser( socket_t *sock )
{
	int count = _pUserMgr->GetSize() ;
	if ( count <= 0 )
		return false ;

	// 如果已成功添加用户需处理用户列表
	UserInfo *pUsers = new UserInfo[count] ;
	_pUserMgr->GetUsers( pUsers, count ) ;
	// 如果MSG登陆成功需要纷发用户名
	_pMsgMgr->DispatherUsers( sock, pUsers, count ) ;

	delete [] pUsers;

	return true ;
}

// 移除客户端
bool CFdClientMgr::RemoveFdClient( socket_t *sock )
{
	if ( sock == NULL || sock->_ptr == NULL )
		return false ;

	return RemoveClient( (CFdClient *)sock->_ptr ) ;
}

// 检测当前FD是否存在
bool CFdClientMgr::CheckFdClient( socket_t *sock )
{
	if ( sock == NULL )
		return false ;
	// 检测是否已存在
	return ( sock->_ptr != NULL ) ;
}

// 添加客户端
bool CFdClientMgr::AddClient( CFdClient *p )
{
	share::RWGuard guard( _rwmutex , true ) ;
	{
		if ( p == NULL || p->_fd == NULL )
			return false ;

		// 添加到队列中
		_fdqueue.push( p ) ;

		if ( p->_group & 0xf000 ) {
			// 如果为MSG则添加为服务器
			if ( p->_group== FD_NODE_MSG ) {
				_pMsgMgr->AddMsgServer( (CMsgServer*) p ) ;
			} else {
				// 添加到组队列中
				_pGroupMgr->AddGroup( p ) ;
			}
		}
		return true ;
	}
}

// 移除客户端
bool CFdClientMgr::RemoveClient( CFdClient *p )
{
	share::RWGuard guard( _rwmutex , true ) ;
	{
		// 先从队列中移除
		p = _fdqueue.erase( p ) ;
		if ( p->_fd != NULL ) {
			p->_fd->_ptr = NULL ;
		}
		if ( p->_group & 0xf000 ) {
			// 删除对应数据
			if ( p->_group != FD_NODE_MSG ) {
				_pGroupMgr->RemoveGroup( p ) ;
			}
			_pMsgMgr->RemoveClient( p ) ;
		}

		// 最后释放对象
		delete p ;

		return true ;
	}
}

CFdClient * CFdClientMgr::GetFdClient( socket_t *sock )
{
	share::RWGuard guard( _rwmutex, false ) ;
	{
		if ( sock == NULL || sock->_ptr == NULL )
			return NULL ;
		// 返回对应的FD对象
		return ( CFdClient *) sock->_ptr ;
	}
}

//////////////// 节点管理对象管理所有结点情况 ////////////////
CNodeMgr::CNodeMgr(ISystemEnv *pEnv):_pEnv(pEnv)
{
	_builder    =  new CMsgBuilder(pEnv->GetAllocMsg());
	_clientmgr  =  new CFdClientMgr( pEnv , _builder ) ;
	// 设置等待队列的回调对象
	_pEnv->GetWaitGroup()->SetNotify( this ) ;
}

CNodeMgr::~CNodeMgr()
{
	if ( _clientmgr != NULL ) {
		delete _clientmgr ;
		_clientmgr = NULL ;
	}
	if ( _builder != NULL ) {
		delete _builder ;
		_builder = NULL ;
	}
}

// 处理所有数据
void CNodeMgr::Process( socket_t *sock, const char *data, int len )
{
	if ( len < (int)sizeof(NodeHeader) ) {
		OUT_ERROR( sock->_szIp, sock->_port, "Node", "recv fd %d data len %d error" , sock->_fd, len ) ;
		return ;
	}

	NodeHeader *header = (NodeHeader *) (data) ;
	unsigned int mlen = ntohl( header->len ) ;
	// 较验数据的正确性
	if ( (int)(mlen + sizeof(NodeHeader)) != len ) {
		OUT_ERROR( sock->_szIp, sock->_port, "Node", "recv fd %d data len %d error" , sock->_fd, len ) ;
		return ;
	}

	unsigned int   seq = ntohl( header->seq ) ;
	unsigned short cmd = ntohs( header->cmd ) ;
	switch( cmd ) {
	case NODE_CONNECT_REQ:
		_clientmgr->LoginClient( sock, seq, (NodeLoginReq*)(data+sizeof(NodeHeader))) ;
		break ;
	case NODE_DISCONN_REQ:
		_clientmgr->LogoutClient( sock, seq ) ;
		break ;
	case NODE_LINKTEST_REQ:
		_clientmgr->LinkTest( sock, seq, (NodeLinkTestReq*)(data+sizeof(NodeHeader))) ;
		break ;
	case NODE_USERNAME_REQ:
		_clientmgr->UserName( sock, seq ) ;
		break ;
	case NODE_GETMSG_REQ:
		_clientmgr->GetMsgList( sock, seq ) ;
		break ;
	case NODE_USERNOTIFY_RSP:
		// printf( "remove seq id %d fd %d\n" , seq, fd ) ;
	case NODE_MSGERROR_RSP:
	case NODE_MSGCHG_RSP:
		// 处理异步响应操作
		_pEnv->GetWaitGroup()->DelQueue( seq, sock, true ) ;
		break ;
	}
}

// 外面传入的断连事件
void CNodeMgr::Close( socket_t *sock )
{
	// 移除连接
	_clientmgr->RemoveFdClient( sock ) ;
}

// 处理结果回调对象
void CNodeMgr::NotifyMsgData( socket_t *sock , MsgData *p , ListFd &fds, unsigned int op )
{
	// 根据不同的消息不同的处理
	switch( p->cmd ) {
	case NODE_USERNOTIFY_REQ:  // 纷发用户名
		 // 如果为MSG登陆补发用户列表需要重新处理发送
		if ( fds.empty() && op == MSG_TIMEOUT ) {
			_clientmgr->ResendMsg( sock , p, fds ) ; // 重新补发数据
		} else {  // 如果向所有MSG纷发新增用户情况就应该返回用户名
			_clientmgr->ProcUserName( sock, p->seq , p ) ;
		}
		break ;
	case NODE_MSGERROR_REQ:
		break ;
	case NODE_MSGCHG_REQ:
		if ( op == MSG_TIMEOUT ) {
			// 重发数据
			_clientmgr->ResendMsg( sock, p, fds ) ;
		}
		break ;
	}
	OUT_INFO( sock->_szIp, sock->_port, "Notify" , "%s fd %d, cmd %04x , seq %d" ,
			(op==MSG_TIMEOUT) ? "timeout" : "success" , sock->_fd, p->cmd, p->seq ) ;
}
