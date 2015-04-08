/**********************************************
 * systemenv.h
 *
 *  Created on: 2011-07-24
 *      Author: humingqing
 *       Email: qshuihu@gmail.com
 *    Comments: 环境对象类，主要管理需要交互的对象，对象与对象交互之间的中界，
 *    这样，任何两个对象之间的交互都可以通过对象管理部件来实现直接交互，使各部件之间透明化，
 *    也使得结构合理化，对于每一个部件实现，都必需实现Init Start Stop三个功能主要实现系统
 *    之间的统一规范化处理。
 *********************************************/

#include <comlog.h>
#include <cConfig.h>
#include <tools.h>
#include "systemenv.h"
#include "msgserver.h"
#include "nodeclient.h"
#include "groupuser.h"
#include "publish.h"
#include "rediscache.h"
#ifdef _HAVE_SAVE
#include "datasaver.h"
#endif
#ifdef _HAVE_PLUG
#include "plugin.h"
#endif
#include "msgclient.h"

CSystemEnv::CSystemEnv():_initialed(false)
{
	_config         = NULL ;
	_msg_server     = NULL ;

	// 初始化管理部件
	for ( int i = 0; i < MAX_MSGHANDLE; ++ i ) {
		_msghandler[i] = NULL ;
	}

	_rediscache     = new RedisCache ;
	_usermgr		= new CGroupUserMgr ;
	_msg_server     = new ClientAccessServer ;
	_node_client	= new CNodeClient;
	_publisher		= new Publisher;

#ifdef _HAVE_SAVE
	_msghandler[MSG_MSGPROC]  = new CDataSaver ;
#endif
#ifdef _HAVE_PLUG
	_msghandler[MSG_PLUGIN]   = new CPlugin ;
#endif
	_msgclient 		= new MsgClient;
}

CSystemEnv::~CSystemEnv()
{
	Stop() ;

	// 释放所有开辟的部件
	for ( int i = 0; i < MAX_MSGHANDLE; ++ i ) {
		if ( _msghandler[i] == NULL )
			continue ;
		delete _msghandler[i] ;
		_msghandler[i] = NULL ;
	}

	if ( _publisher != NULL ) {
		delete _publisher ;
		_publisher = NULL ;
	}
	if ( _node_client != NULL ){
		delete _node_client ;
		_node_client = NULL ;
	}
	if ( _msg_server != NULL ){
		delete _msg_server ;
		_msg_server = NULL ;
	}
	if ( _msgclient != NULL ) {
		delete _msgclient ;
		_msgclient = NULL ;
	}
	if ( _usermgr != NULL ) {
		delete _usermgr ;
		_usermgr = NULL ;
	}
	if ( _rediscache != NULL ) {
		delete _rediscache ;
		_rediscache = NULL ;
	}
	if ( _config != NULL ){
		delete _config ;
		_config = NULL ;
	}
}

bool CSystemEnv::InitLog( const char * logpath  , const char *logname )
{
	char szbuf[512] = {0} ;
	sprintf( szbuf, "mkdir -p %s", logpath ) ;
	system( szbuf );

	sprintf( szbuf, "%s/%s" , logpath , logname ) ;
	CHGLOG( szbuf ) ;

	int log_num = 20;
	if ( ! GetInteger("log_num" , log_num ) ){
		printf( "get log number falied\n" ) ;
		log_num = 0 ;
	}

	int log_size = 20 ;
	if ( ! GetInteger("log_size" , log_size) ){
		printf( "get log size failed\n" ) ;
		log_size = 20 ;
	}
	// 取得日志级别
	int log_level = 3 ;
	if ( ! GetInteger("log_level" , log_level) ) {
		log_level = 3 ;
	}
	// 设置日志级别
	SETLOGLEVEL(log_level) ;
	CHGLOGSIZE(log_size);
	CHGLOGNUM(log_num);

	return true ;
}

bool CSystemEnv::Init( const char *file , const char *logpath , const char *userfile , const char *logname )
{
	_user_file_path = userfile ;

	_config = new CCConfig( file ) ;
	if ( _config == NULL ) {
		printf( "CSystemEnv::Init load config file %s failed\n", file ) ;
		return false ;
	}

	char temp[256] = {0} ;
	// 如果配置文件配置了工作日志目录
	if ( GetString( "log_dir", temp ) ) {
		InitLog( temp, logname ) ;
	} else {
		InitLog( logpath , logname ) ;
	}

	if ( ! _rediscache->Init(this) ) {
		printf( "CSystemEnv::Init redis cache failed\n" ) ;
		return false ;
	}

	if ( ! _node_client->Init(this) ) {
		printf( "CSystemEnv::Init node client failed\n" ) ;
		return false ;
	}
	// 初始化MSG服务器
	if ( ! _msg_server->Init( this ) ){
		printf( "CSystemEnv::Init msg server init failed\n" ) ;
		return false ;
	}
	// 初始化发布服务
	if ( ! _publisher->Init(this) ) {
		printf( "CSystemEnv::Init publisher server failed\n" ) ;
		return false ;
	}

	// 初始化管理部件
	for ( int i = 0; i < MAX_MSGHANDLE; ++ i ) {
		if ( _msghandler[i] == NULL )
			continue ;
		_msghandler[i]->Init( this ) ;
	}

	// 初始MSG与中心同步客户
	if ( ! _msgclient->Init(this) ) {
		printf( "CSystemEnv::Init syn client failed\n" ) ;
		return false ;
	}

	return true ;
}

bool CSystemEnv::Start( void )
{
	if ( ! _msgclient->Start() ) {
		printf( "CSystemEnv::Start start syn client failed\n" ) ;
		return false ;
	}

	if ( ! _rediscache->Start() ) {
		printf( "CSystemEnv::Start start redis cache failed\n" ) ;
		return false ;
	}
	if ( ! _msg_server->Start() ){
		printf( "CSystemEnv::Start start cas server failed\n" ) ;
		_rediscache->Stop() ;
		return false ;
	}
	if ( ! _publisher->Start() ) {
		printf( "CSystemEnv::Start publisher server failed\n" ) ;
		_rediscache->Stop() ;
		_msg_server->Stop() ;
		return false ;
 	}
	_initialed = true ;

	// 初始化管理部件
	for ( int i = 0; i < MAX_MSGHANDLE; ++ i ) {
		if ( _msghandler[i] == NULL )
			continue ;
		if ( ! _msghandler[i]->Start() ) {
			printf( "CSystemEnv::Start msg handler %d failed\n", i ) ;
		}
	}

	return _node_client->Start() ;
}

void CSystemEnv::Stop( void )
{
	if ( ! _initialed )
		return ;

	_initialed = false ;

	_node_client->Stop() ;

	// 初始化管理部件
	for ( int i = 0; i < MAX_MSGHANDLE; ++ i ) {
		if ( _msghandler[i] == NULL )
			continue ;
		_msghandler[i]->Stop() ;
	}

	_msg_server->Stop() ;
	_publisher->Stop() ;
	_rediscache->Stop() ;
	_msgclient->Stop() ;
}

// 取得整形值
bool CSystemEnv::GetInteger( const char *key , int &value )
{
	char buf[1024] = {0} ;
	if ( _config->fGetValue("COMMON" , key, buf ) == -1 ){
		return false ;
	}

	value = atoi( buf ) ;

	return true ;
}

// 取得字符串值
bool CSystemEnv::GetString( const char *key , char *value )
{
	char buf[512] = {0} ;
	if ( _config->fGetValue("COMMON", key , buf ) == -1 ){
		return false ;
	}
	return getenvpath( buf , value );
}
