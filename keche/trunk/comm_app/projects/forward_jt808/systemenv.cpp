/**********************************************
 * systemenv.h
 *
 *  Created on: 2014-8-19
 *      Author: ycq
 *********************************************/
#include <comlog.h>
#include <cConfig.h>
#include <tools.h>
#include "systemenv.h"
#include "wasclient.h"
#include "wasserver.h"
#include "rediscache.h"

CSystemEnv::CSystemEnv():_initialed(false)
{
	_wasserver      = new WasServer ;
	_wasclient      = new WasClient ;
	_rediscache     = new RedisCache ;
}

CSystemEnv::~CSystemEnv()
{
	Stop() ;

	if( _wasclient  != NULL ) {
		delete _wasclient;
		_wasclient = NULL;
	}

	if ( _wasserver != NULL ) {
		delete _wasserver ;
		_wasserver = NULL ;
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

	// 初始化was客户端对象
	if ( ! _wasclient->Init( this ) ) {
		printf( "CSystemEnv::Init was client failed\n" ) ;
		return false ;
	}

	// 初始化was服务器对象
	if ( ! _wasserver->Init( this ) ) {
		printf( "CSystemEnv::Init was server failed\n" ) ;
		return false ;
	}

	return true ;
}

bool CSystemEnv::Start( void )
{
	_initialed = true ;

	if( ! _rediscache->Start()) {
		return false;
	}

	if( ! _wasclient->Start() ) {
		return false;
	}

	if( ! _wasserver->Start() ) {
		return false;
	}

	return true;
}

void CSystemEnv::Stop( void )
{
	if ( ! _initialed )
		return ;

	_initialed = false ;

	_wasserver->Stop() ;
	_wasclient->Stop() ;
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
