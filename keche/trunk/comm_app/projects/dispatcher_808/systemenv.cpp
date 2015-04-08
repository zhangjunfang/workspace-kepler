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
#include "msgclient.h"
#include "wasclient.h"
#include "rediscache.h"
#include "usermgr.h"

CSystemEnv::CSystemEnv():_initialed(false)
{
	_msgclient      = new MsgClient ;
	_wasclient      = new WasClient ;
	_userMgr        = new UserMgr;
	_rediscache     = new RedisCache ;
}

CSystemEnv::~CSystemEnv()
{
	Stop() ;

	if( _wasclient  != NULL ) {
		delete _wasclient;
		_wasclient = NULL;
	}

	if ( _msgclient != NULL ) {
		delete _msgclient ;
		_msgclient = NULL ;
	}

	if(_userMgr != NULL) {
		delete _userMgr;
		_userMgr = NULL;
	}

	if(_rediscache != NULL) {
		delete _rediscache;
		_rediscache = NULL;
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
	_config = new CCConfig( file ) ;

	_user_file_path = userfile;

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

	if( ! _rediscache->Init(this)) {
		printf( "CSystemEnv::Init redis cache failed\n" ) ;
		return false;
	}

	// 初始化MSG的对象
	if ( ! _msgclient->Init( this ) ) {
		printf( "CSystemEnv::Init msg client failed\n" ) ;
		return false ;
	}

	// 初始化was的对象
	if ( ! _wasclient->Init( this ) ) {
		printf( "CSystemEnv::Init was client failed\n" ) ;
		return false ;
	}

	if( ! _userMgr->Init(this)) {
		printf("CSystemEnv::Init user manage failed\n");
		return false;
	}

	return true ;
}

bool CSystemEnv::Start( void )
{
	_initialed = true ;

	if( ! _rediscache->Start()) {
		return false;
	}

	// 启动MSGClient对象
	if( ! _msgclient->Start() ) {
		return false;
	}

	if( ! _wasclient->Start() ) {
		return false;
	}

	if( ! _userMgr->Start()) {
		return false;
	}

	return true;
}

void CSystemEnv::Stop( void )
{
	if ( ! _initialed )
		return ;

	_initialed = false ;

	_msgclient->Stop() ;
	_wasclient->Stop() ;
	_userMgr->Stop();
	_rediscache->Stop();
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
