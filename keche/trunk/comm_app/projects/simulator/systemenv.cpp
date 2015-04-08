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
#include "systemenv.h"
#include "vechilemgr.h"


CSystemEnv::CSystemEnv():_initialed(false)
{
	_config             = NULL ;
	_vechile_client     = NULL ;
	_vechile_client     = new CVechileMgr ;
}

CSystemEnv::~CSystemEnv()
{
	Stop() ;

	if ( _vechile_client != NULL )
	{
		delete _vechile_client ;
		_vechile_client = NULL ;
	}

	if ( _config != NULL )
	{
		delete _config ;
		_config = NULL ;
	}
}

bool CSystemEnv::InitLog( const char * logpath )
{
	char szbuf[512] = {0} ;
	sprintf( szbuf, "mkdir -p %s", logpath ) ;
	system( szbuf );

	sprintf( szbuf, "%s/simulatorv4.1.log" , logpath ) ;
	CHGLOG( szbuf ) ;

	int log_num = 20;
	if ( ! GetInteger("log_num" , log_num ) )
	{
		printf( "get log number falied\n" ) ;
		log_num = 20 ;
	}

	int log_size = 16 ;
	if ( ! GetInteger("log_size" , log_size) )
	{
		printf( "get log size failed\n" ) ;
		log_size = 16 ;
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

bool CSystemEnv::Init( const char *file , const char *logpath , const char *userfile )
{
	_user_file_path = userfile ;

	_config = new CCConfig( file ) ;
	if ( _config == NULL ) {
		printf( "CSystemEnv::Init load config file %s failed\n", file ) ;
		return false ;
	}

	InitLog( logpath ) ;

	// 初始化MSG服务器
	if ( ! _vechile_client->Init( this ) )
	{
		printf( "CSystemEnv::Init cas server init failed\n" ) ;
		return false ;
	}

	return true ;
}

bool CSystemEnv::Start( void )
{
	if ( ! _vechile_client->Start() ){
		printf( "CSystemEnv::Start start cas server failed\n" ) ;
		return false ;
	}

	_initialed = true ;
	return true ;
}

void CSystemEnv::Stop( void )
{
	if ( ! _initialed )
		return ;

	_initialed = false ;

	_vechile_client->Stop() ;
}

// 取得整形值
bool CSystemEnv::GetInteger( const char *key , int &value )
{
	char buf[1024] = {0} ;
	if ( _config->fGetValue("COMMON" , key, buf ) == -1 )
	{
		return false ;
	}

	value = atoi( buf ) ;

	return true ;
}

// 取得字符串值
bool CSystemEnv::GetString( const char *key , char *value )
{
	return ( _config->fGetValue("COMMON", key , value) != -1 ) ;
}
