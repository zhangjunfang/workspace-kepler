/*
 * plugin.cpp
 *
 *  Created on: 2012-5-30
 *      Author: humingqing
 */

#include "plugin.h"
#include "plugutil.h"
#include <comlog.h>
#include <Base64.h>

CPlugin::CPlugin()
	: _enable(false)
{
	_waymgr = NULL ;
}

CPlugin::~CPlugin()
{
	Stop() ;

	if ( _waymgr != NULL ) {
		delete _waymgr ;
		_waymgr = NULL ;
	}
}

// 初始化
bool CPlugin::Init( ISystemEnv * pEnv )
{
	_pEnv = pEnv ;

	int nvalue = 0 ;
	if ( _pEnv->GetInteger( "plugin_enable", nvalue ) ) {
		_enable = ( nvalue == 1 ) ;
	}
	if ( ! _enable )
		return true ;

	// 只允许开启时才启动
	_waymgr = new CWayMgr(this) ;

	return _waymgr->Init() ;
}

// 启动服务
bool CPlugin::Start( void )
{
	if ( ! _enable )
		return true ;

	return _waymgr->Start() ;
}

// 停止服务
bool CPlugin::Stop( void )
{
	if ( ! _enable )
		return true ;

	return _waymgr->Stop() ;
}

// 处理数据
bool CPlugin::Process( InterData &data , User &user )
{
	// if ( ! _enable ) return true ;
	// U_REPT  91:20,90:base64.GetBuffer()
	// D_SETP  91:20,90:base64.GetBuffer()
	CPlugUtil util ;
	if ( ! util.parse( data._packdata.c_str() ) ) {
		OUT_ERROR( NULL, 0, "Plugin", "parser kv map string %s failed", data._packdata.c_str() ) ;
		return false ;
	}

	int cmd = 0 ;
	if( ! util.getinteger( "91", cmd ) ){
		OUT_ERROR( NULL, 0, "Plugin" , "get command key failed" ) ;
		return false ;
	}

	//  查找处理通道，如果查找失败就直接返回了，如果成功就继续下一步
	CPlugWay *way = _waymgr->CheckOut( cmd , true ) ;
	if ( way == NULL ) {
		OUT_ERROR( NULL, 0, "Plugin", "get plug way failed" ) ;
		return false ;
	}

	string sval ;
	if ( ! util.getstring( "90", sval ) ) {
		OUT_ERROR( NULL, 0, "Plugin" , "get content data failed" ) ;
		_waymgr->CheckIn( way ) ;
		return false ;
	}

	CBase64 base ;
	if ( ! base.Decode( sval.c_str(), sval.length() ) ) {
		OUT_ERROR( NULL, 0, "Plugin" , "base64 decode failed" ) ;
		_waymgr->CheckIn( way ) ;
		return false ;
	}

	// 处理透传的命令字
	cmd = cmd & 0xff ;
	if ( data._command == "U_REPT" ) {
		cmd = cmd | MSG_PLUG_IN ; // 上行数据
	} else {
		cmd = cmd | MSG_PLUG_OUT ; // 下行数据
	}
	// 取得对应的FD值
	unsigned int fd = _fdmgr.AddUser( user._user_id.c_str(), data._macid.c_str() ) ;

	// 提交对应的通道进行处理数据
	bool result = way->Process( fd, base.GetBuffer(), base.GetLength(), cmd , data._macid.c_str() ) ;

	_waymgr->CheckIn( way ) ;

	return result ;
}

// 取得配置文件字符串形数据
bool CPlugin::GetString( const char *key, char *buf )
{
	return _pEnv->GetString( key, buf ) ;
}
// 取得配置文件整形的数据
bool CPlugin::GetInteger( const char *key , int &value )
{
	return _pEnv->GetInteger( key, value ) ;
}

// 需要回调外部接发送的数据
void CPlugin::OnDeliver( unsigned int fd, const char *data, int len , unsigned int cmd )
{
	string userid, macid ;
	if ( ! _fdmgr.GetUser( fd, userid, macid ) ) {
		OUT_ERROR( NULL, 0, "Plugin", "get fd %u user failed", fd ) ;
		return ;
	}

	CBase64 base ;
	base.Encode( data, len ) ;

	char scmd[512] = {0};
	sprintf( scmd, "CAITS 0_%u %s 0 D_SETP {TYPE:9,91:%d,90:", fd, macid.c_str(), ( 0xff & cmd ) ) ;

	string sdata = scmd ;
	sdata += base.GetBuffer() ;
	sdata += "} \r\n" ;

	// 发送数据出去
	if ( ! _pEnv->GetMsgClientServer()->DeliverEx( userid.c_str(), sdata.c_str(), sdata.length() ) ) {
		_fdmgr.DelUser( fd ) ;
	}
}

//=====================================================================
CPlugin::CPlugUserMgr::CPlugUserMgr()
{
	_id = 0 ;
}

CPlugin::CPlugUserMgr::~CPlugUserMgr()
{
	Clear() ;
}

// 添加到用户对象
unsigned int CPlugin::CPlugUserMgr::AddUser( const char *userid, const char *macid )
{
	share::Guard guard( _mutex ) ;

	if ( userid == NULL || macid == NULL )
		return 0 ;

	CMapMacId::iterator itx = _macids.find( macid ) ;
	if ( itx != _macids.end() ){
		itx->second->_userid = userid ;
		itx->second->_macid  = macid  ;
		return itx->second->_id ;
	}

	_id = _id + 1 ;

	_UserInfo *info = new _UserInfo ;
	info->_id 	  = _id ;
	info->_userid = userid ;
	info->_macid  = macid ;

	_userids.insert( make_pair( _id,  info ) ) ;
	_macids.insert( make_pair( macid, info ) ) ;

	return _id ;
}

// 取得用户对象
bool CPlugin::CPlugUserMgr::GetUser( unsigned int id , string &userid, string &macid )
{
	share::Guard guard( _mutex ) ;
	if ( _userids.empty() )
		return false ;

	CMapUserId::iterator it = _userids.find( id ) ;
	if ( it == _userids.end() )
		return false ;

	userid = it->second->_userid ;
	macid  = it->second->_macid ;

	return true ;
}

// 删除数据
bool CPlugin::CPlugUserMgr::DelUser( unsigned int id )
{
	share::Guard guard( _mutex ) ;
	if ( _userids.empty() )
		return false ;

	CMapUserId::iterator it = _userids.find( id ) ;
	if ( it == _userids.end() )
		return false ;
	_UserInfo *info = it->second ;
	_userids.erase( it ) ;

	CMapMacId::iterator itx = _macids.find( info->_macid ) ;
	if ( itx != _macids.end() ){
		_macids.erase( itx ) ;
	}
	delete info ;

	return true ;
}

// 清理所有对象
void CPlugin::CPlugUserMgr::Clear( void )
{
	share::Guard guard( _mutex ) ;
	if ( _userids.empty() )
		return ;
	CMapUserId::iterator it ;
	for ( it = _userids.begin(); it != _userids.end(); ++ it ) {
		delete it->second ;
	}
	_userids.clear() ;
	_macids.clear() ;
}





