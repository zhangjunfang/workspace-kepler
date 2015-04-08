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
#include <InterProtoConvert.h>
#include "systemenv.h"
#include "pasclient.h"
#include "msgclient.h"
#include "msgcache.h"
#include "pccserver.h"
#include "userloader.h"
#include "rediscache.h"

CSystemEnv::CSystemEnv():_initialed(false)
{
	_config         = NULL ;
	_rediscache     = new RedisCache ;
	_userloader     = new CUserLoader ;
	_msg_cache      = new MsgCache() ;
	_pas_client     = new PasClient(_srvCaller);
	_msg_client     = new MsgClient(_srvCaller);
	_pcc_server		= new CPccServer;
}

CSystemEnv::~CSystemEnv()
{
	Stop() ;

	if ( _pcc_server != NULL ) {
		delete _pcc_server ;
		_pcc_server = NULL ;
	}

	if ( _msg_client != NULL ) {
		delete _msg_client ;
		_msg_client = NULL ;
	}
	if ( _pas_client != NULL ){
		delete _pas_client ;
		_pas_client = NULL ;
	}

	if ( _msg_cache != NULL ) {
		delete _msg_cache ;
		_msg_cache = NULL ;
	}

	if ( _userloader != NULL ) {
		delete _userloader ;
		_userloader = NULL ;
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

bool CSystemEnv::InitLog( const char * logpath, const char *logname )
{
	char szbuf[512] = {0} ;
	sprintf( szbuf, "mkdir -p %s", logpath ) ;
	system( szbuf );

	sprintf( szbuf, "%s/%s" , logpath, logname ) ;
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

bool CSystemEnv::InitUser( void )
{
	char temp[512] = {0};
	if ( ! GetString( "user_filepath" , temp ) ) {
		printf( "load user file failed\n" ) ;
		return false ;
	}
	char buf[1024] = {0} ;
	getenvpath( temp, buf );

	_user_file = buf ;

	// 加载基本订阅列表路径
	if ( ! GetString( "base_dmddir" ,temp ) ) {
		printf( "load base_dmddir failed\n" ) ;
		return false ;
	}
	getenvpath( temp, buf );
	_dmddir = buf;

	return true ;
}

bool CSystemEnv::Init( const char *file , const char *logpath , const char *userfile, const char *logname )
{
	_config = new CCConfig( file ) ;
	if ( _config == NULL ) {
		printf( "CSystemEnv::Init load config file %s failed\n", file ) ;
		return false ;
	}
	// 初始化日志
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

	// 初始用户
	if ( ! InitUser() ) {
		printf( "CSystemEnv::Init pcc user file failed, %s:%d\n" , __FILE__, __LINE__ ) ;
		return false ;
	}

	//srvCaller 初始化
	if ( ! _srvCaller.Init( this ) ){
		printf( "CSystemEnv::Init srv caller failed , %s:%d\n", __FILE__, __LINE__ ) ;
		return false ;
	}
	// PCC服务
	if ( ! _pcc_server->Init( this ) ) {
		printf( "CSystemEnv::Init pcc server failed, %s:%d\n" , __FILE__ , __LINE__ ) ;
		return false ;
	}

	// MSG客户端
	if ( ! _msg_client->Init( this ) ) {
		printf( "CSystemEnv::Init msg client failed , %s:%d\n", __FILE__ , __LINE__ ) ;
		return false ;
	}

	// PAS客户端
	if ( ! _pas_client->Init( this ) ){
		printf( "CSystemEnv::Init pas client failed , %s:%d\n", __FILE__, __LINE__ ) ;
		return false ;
	}

	//MSG、PAS连接配置导入
	if( !_userloader->Init(this) ) {
		printf( "CSystemEnv::Init userloader failed , %s:%d\n", __FILE__, __LINE__ ) ;
		return false ;
	}

	return true ;
}

bool CSystemEnv::Start( void )
{
	if ( ! _rediscache->Start() ) {
		printf( "CSystemEnv::Start start redis cache failed\n" ) ;
		return false ;
	}

	if ( ! _srvCaller.Start() ) {
		printf( "CSystemEnv::Start srv caller failed, %s:%d\n", __FILE__, __LINE__ ) ;
		return false ;
	}
	if ( ! _pcc_server->Start() ) {
		printf( "CSystemEnv::Start pcc server failed, %s:%d\n", __FILE__, __LINE__ ) ;
		return false ;
	}

	if ( ! _pas_client->Start() ){
		printf( "CSystemEnv::Start pas client failed, %s:%d\n" , __FILE__, __LINE__ ) ;
		return false ;
	}
	if ( ! _msg_client->Start() ){
		printf( "CSystemEnv::Start msg client failed , %s:%d\n", __FILE__, __LINE__ ) ;
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

	_msg_client->Stop() ;
	_pas_client->Stop() ;
	_pcc_server->Stop() ;
	_srvCaller.Stop() ;
	_rediscache->Stop() ;
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
	char buf[512] = {0} ;
	if ( _config->fGetValue("COMMON", key , buf ) == -1 ){
		return false ;
	}
	return getenvpath( buf , value );
}

// 取得缓存的序号
void CSystemEnv::GetCacheKey( unsigned int seq, char *key )
{
	// MAC和消息类型两种对应产生的序号
	sprintf( key , "%s%u" , SEQ_HEAD, seq ) ;
}

// 添加组对象
bool CSystemEnv::SetNotify( const char *tag, IUserNotify *notify )
{
	return _userloader->SetNotify( tag, notify ) ;
}

// 加载用户数据
bool CSystemEnv::LoadUserData( void )
{
	return _userloader->LoadUser( _user_file.c_str(), _dmddir.c_str() ) ;
}

// 返回加密密钥
bool CSystemEnv::GetUserKey( int accesscode, int &M1, int &IA1, int &IC1 )
{
	return _userloader->GetUserKey( accesscode, M1, IA1, IC1 ) ;
}

// 清理会话处理
void CSystemEnv::ClearSession( const char *key )
{
	_srvCaller.RemoveCache( key ) ;
}

bool CSystemEnv::getChannels(const string &macid, set<string> &channels)
{
	return _userloader->getChannels(macid, channels);
}

bool CSystemEnv::getSubscribe(list<string> &macids)
{
	return _userloader->getSubscribe(macids);
}

bool CSystemEnv::getMacid(const string &plate, string &macid)
{
	return _userloader->getMacid(plate, macid);
}
