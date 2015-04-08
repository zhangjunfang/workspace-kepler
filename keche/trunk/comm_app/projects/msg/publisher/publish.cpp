/*
 * publish.cpp
 *
 *  Created on: 2012-4-16
 *      Author: humingqing
 *
 */

#include "publish.h"
#include <comlog.h>
#include <tools.h>

Publisher::Publisher()
{
	_nthread    = 1 ;
	_pusermgr   = NULL ;
	_pmsgserver = NULL ;
	_pubqueue   = new CDataQueue<_pubData>(102400) ;
	_queuethread= new CQueueThread( _pubqueue, this ) ;
}

Publisher::~Publisher()
{
	Stop() ;

	if ( _queuethread != NULL ) {
		delete _queuethread ;
		_queuethread = NULL ;
	}

	if ( _pubqueue != NULL ) {
		delete _pubqueue ;
		_pubqueue = NULL ;
	}
}

// 初始化发布对象
bool Publisher::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	int nvalue = 0 ;
	if ( pEnv->GetInteger( "publish_thread", nvalue ) ) {
		_nthread = nvalue ;
	}
	_pusermgr   = pEnv->GetUserMgr() ;
	_pmsgserver = pEnv->GetMsgClientServer() ;

	return true ;
}

// 启动发布对象线程
bool Publisher::Start( void )
{
	if ( ! _queuethread->Init( _nthread ) ) {
		OUT_ERROR( NULL, 0, "Publish", "init publish thread failed" ) ;
		return false ;
	}
	OUT_INFO( NULL, 0, "Publish", "start publish thread success" ) ;

	return true ;
}

// 停止发布对象线程
bool Publisher::Stop( void )
{
	_queuethread->Stop() ;
	return true ;
}

// 开始发布数据
bool Publisher::Publish( InterData &data, unsigned int cmd , User &user )
{
	_pubData *p = new _pubData ;
	if ( p == NULL )
		return false ;

	p->_cmd  	 = cmd ;
	p->_user 	 = user ;
	p->_data 	 = data ;

	if ( ! _queuethread->Push( p ) ) {
		delete p ;
		return false ;
	}
	return true ;
}

// 解析监控平台的参数
static bool splitsubscribe( const std::string &param,  vector<string> &vec )
{
	size_t pos = param.find("{") ;
	if ( pos == string::npos ) {
		return false ;
	}

	size_t end = param.find("}", pos ) ;
	if ( end == string::npos || end < pos + 1 ) {
		return false ;
	}
	return splitvector( param.substr( pos+1, end-pos-1 ), vec, ",", 0 ) ;
}

// 处理数据订阅
bool Publisher::OnDemand( unsigned int cmd , unsigned int group, const char *szval, User &user )
{
	if ( szval == NULL )
		return false ;

	vector<string> vec ;
	if ( ! splitsubscribe( szval, vec) )
		return false ;

	int i = 0 , count = 0 ;
	switch( cmd )
	{
	case OP_SUBSCRIBE_DMD:
	case OP_SUBSCRIBE_ADD:
		{
			if ( cmd == OP_SUBSCRIBE_DMD )
				_submgr.Del( user._access_code ) ;

			int len = 0 ;
			if ( group == TYPE_SUBSCRIBE_MACID ) {
				len = vec.size() ;
				// 需要解析数据
				for ( i = 0; i < len; ++ i ) {
					if ( _submgr.Add( user._access_code, vec[i].c_str() ) ) {
						++ count ;
					}
				}
			} else {
				// 需要从缓存中查找
				std::set<string> kset ;
				vector<string> vecval ;

				len = vec.size() ;
				// 加载订阅的所有组的数据
				for( i = 0; i < len; ++ i ) {
					LoadSubscribe( vec[i].c_str(), vecval , kset ) ;
				}

				len = vecval.size() ;
				for ( i = 0; i< len ; ++ i ) {
					string &s = vecval[i] ;
					if ( strncmp( s.c_str(), "JMP:", 4 ) == 0 ) {
						continue ;
					}
					if ( _submgr.Add( user._access_code, s.c_str() ) ) {
						++ count ;
					}
				}
			}
		}
		break ;
	case OP_SUBSCRIBE_UMD:
		{
			if ( group == TYPE_SUBSCRIBE_MACID ) {
				count = vec.size() ;
				// 需要解析数据
				for ( i = 0; i < count; ++ i ) {
					_submgr.Remove( user._access_code, vec[i].c_str() ) ;
				}
			} else {
				// 需要从缓存中查找
				vector<string> vecval ;
				std::set<string> kset ;
				for( i = 0; i < (int)vec.size(); ++ i ) {
					LoadSubscribe( vec[i].c_str(), vecval , kset ) ;
				}

				count = vecval.size() ;
				for ( i = 0; i< count; ++ i ) {
					string &s = vecval[i] ;
					if ( strncmp( s.c_str(), "JMP:", 4 ) == 0 ) {
						continue ;
					}
					_submgr.Remove( user._access_code, s.c_str() ) ;
				}
			}
		}
		break ;
	}
	OUT_PRINT( NULL, 0, "Publish", "%s %s user: %s, macid count: %d", ( (OP_SUBSCRIBE_UMD == cmd ) ? "Remove" : "Load" ) ,
			( (group == TYPE_SUBSCRIBE_MACID ) ? "macid": "group") , user._user_id.c_str(), count ) ;
	return true ;
}

// 加订阅关系
void Publisher::LoadSubscribe( const char *key, std::vector<std::string> &vec, std::set<std::string> &kset )
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

// 线程执行对象方法
void Publisher::HandleQueue( void *packet )
{
	Deliver( ( _pubData *) packet ) ;
}

// 纷发数据
bool Publisher::Deliver( _pubData *p )
{
	// 计算HASH处理
	unsigned int hash = _pusermgr->GetHash( p->_data._macid.c_str(), p->_data._macid.length() ) ;
	// 重组内部协议
	string data = p->_data._transtype + " " + p->_data._seqid + " " + p->_data._macid + " " + p->_data._cmtype + " "
			+ p->_data._command + " " + p->_data._packdata  + " \r\n" ;

	// ToDo: 优化，这里调用太频繁，但对于每一个GPS数据都需要轮询，这样太占用CPU资源需要优化处理
	vector<User> vec_user ;
	if ( !_pusermgr->GetSendGroup( vec_user, hash , p->_cmd ) ) { // 如果没有用户就直接返回了
		// OUT_ERROR( NULL, 0, NULL, "user not online: %s" , data.c_str() ) ;
		return false ;
	}

	// 取得用户个数据
	int size = (int) vec_user.size();
	// 转发数据处理
	for(int i = 0; i < size; ++i ) {
		User &user = vec_user[i] ;
		// 如果相等为自己下的发送用户不需要转发
		if ( p->_user._access_code == user._access_code ) {
			continue ;
		}
		// 如果用户组不为空时
		if ( ! p->_user._user_name.empty() ) {
			// 如果当前下发数据的用户组与当前需要发送的组相同就不发送了
			if ( p->_user._user_type == user._user_type && p->_user._user_name == user._user_name )
				continue ;
		}
		// 如果为非订阅的数据就直接返回了,为订阅用户才进行数据过滤
		if ( ! _submgr.Check( user._access_code, p->_data._macid.c_str() , user._msg_seq & MSG_USER_DEMAND ) ) {
			continue ;
		}

		if ( ! _pmsgserver->Deliver( user._fd, data.c_str() , data.length() ) ) {
			OUT_ERROR( user._ip.c_str(), user._port, user._user_id.c_str(), "Send Data failed, close fd %d", user._fd ) ;
			_pmsgserver->Close( user._fd ) ;
		}
	}

	return true;
}

//////////////////////////////////////////// 处理数据订阅  ///////////////////////////////////////////////////////
Publisher::SubscribeMgr::SubscribeMgr()
{

}

Publisher::SubscribeMgr::~SubscribeMgr()
{
	Clear() ;
}

// 添加订阅车辆信息
bool Publisher::SubscribeMgr::Add( unsigned int ncode, const char *macid )
{
	share::RWGuard guard( _rwmutex, true ) ;

	MapSubscribe::iterator it = _mapSuber.find( ncode ) ;

	_macList *p = NULL ;
	if ( it == _mapSuber.end() ) {
		p = new _macList;
		_mapSuber.insert( make_pair( ncode, p ) ) ;
	} else {
		p = it->second ;
	}
	map<string,int>::iterator itx = p->_mkmap.find( macid ) ;
	if ( itx != p->_mkmap.end() ) {
		itx->second = itx->second + 1 ;  // 添加订阅的引用计数
		return true ;
	}
	p->_mkmap.insert( make_pair(macid , 1 ) ) ;
	return true ;
}

// 是否删除
bool Publisher::SubscribeMgr::Find( unsigned int ncode, const char *macid , bool erase )
{
	MapSubscribe::iterator it = _mapSuber.find( ncode ) ;
	if ( it == _mapSuber.end() )
		return false ;

	_macList *p = it->second ;
	map<string,int>::iterator itx = p->_mkmap.find( macid ) ;
	if ( itx == p->_mkmap.end() )
		return false ;

	if ( erase ) {
		// 减少引用记数
		itx->second = itx->second - 1 ;
		// 当用引用记数为零时才清理数据
		if ( itx->second <= 0 ) {
			p->_mkmap.erase( itx ) ;
			if ( p->_mkmap.empty() ) {
				_mapSuber.erase( it ) ;
				delete p ;
			}
		}
	}
	return true ;
}

// 删除订阅车辆信息
void Publisher::SubscribeMgr::Remove( unsigned int ncode, const char *macid )
{
	share::RWGuard guard( _rwmutex, true ) ;
	// 移除数据
	Find( ncode, macid, true ) ;
}

// 删除当前对象订阅信息
void Publisher::SubscribeMgr::Del( unsigned int ncode )
{
	share::RWGuard guard( _rwmutex, true ) ;

	MapSubscribe::iterator it = _mapSuber.find( ncode ) ;
	if ( it == _mapSuber.end() )
		return ;

	_macList *p = it->second ;
	_mapSuber.erase( it ) ;
	delete p ;
}

// 检测是否订阅成功
bool Publisher::SubscribeMgr::Check( unsigned int ncode, const char *macid , bool check )
{
	if ( ! check ) return true ;
	share::RWGuard guard( _rwmutex, false ) ;
	return Find( ncode, macid, false ) ;
}

void Publisher::SubscribeMgr::Clear( void )
{
	share::RWGuard guard( _rwmutex, true ) ;
	// 如果为空直接删除
	if ( _mapSuber.empty() ) {
		return ;
	}

	MapSubscribe::iterator it ;
	for ( it = _mapSuber.begin(); it != _mapSuber.end(); ++ it ) {
		delete it->second ;
	}
	_mapSuber.clear() ;
}
