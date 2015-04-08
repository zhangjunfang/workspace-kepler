#include <tools.h>
#include <comlog.h>
#include <cConfig.h>

#include "systemenv.h"
#include "tcpclient.h"
#include "tcpserver.h"
#include "udpclient.h"
#include "udpserver.h"
#include "../tools/utils.h"

#include <string>
using std::string;
#include <vector>
using std::vector;
#include <fstream>
using std::ifstream;

CSystemEnv::CSystemEnv():_initialed(false)
{
	_tcpclient      = new TcpClient(this);
	_udpclient      = new UdpClient(this);
}

CSystemEnv::~CSystemEnv()
{
	Stop() ;

	if( _tcpclient  != NULL ) {
		delete _tcpclient;
		_tcpclient = NULL;
	}

	if( _udpclient  != NULL ) {
		delete _udpclient;
		_udpclient = NULL;
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

	// 初始化tcp转发客户端对象
	if ( ! _tcpclient->Init() ) {
		printf( "CSystemEnv::Init tcp client failed\n" ) ;
		return false ;
	}

	// 初始化udp转发客户端对象
	if ( ! _udpclient->Init() ) {
		printf( "CSystemEnv::Init udp client failed\n" ) ;
		return false ;
	}

	if ( ! GetString( "rule_file", temp ) ) {
		printf( "CSystemEnv::Init get rule_file failed\n" ) ;
		return false;
	}

	int i;
	string line;
	ifstream ifs;
	vector<string> fields;
	vector<string> dstAddrs;

	ITcpServer *tcpSvr;
	IUdpServer *udpSvr;

	ifs.open(temp);
	while(getline(ifs, line)) {
		if(line.empty() || line[0] == '#') {
			continue;
		}

		fields.clear();
		if(Utils::splitStr(line, fields, '$') <3) {
			continue;
		}

		dstAddrs.clear();
		for(i = 2; i < fields.size(); ++i) {
			dstAddrs.push_back(fields[i]);
		}

		if(fields[0] == "tcp") {
			tcpSvr = new TcpServer(this);
			tcpSvr->Init(atoi(fields[1].c_str()), dstAddrs);
			tcpSvr->Start();
		} else if(fields[0] == "udp") {
			udpSvr = new UdpServer(this);
			udpSvr->Init(atoi(fields[1].c_str()), dstAddrs);
			udpSvr->Start();
		}
	}

	return true ;
}

bool CSystemEnv::Start( void )
{
	_initialed = true ;

	// 启动tcp转发客户端对象
	if( ! _tcpclient->Start() ) {
		return false;
	}

	// 启动udp转发客户端对象
	if( ! _udpclient->Start() ) {
		return false;
	}

	return true;
}

void CSystemEnv::Stop( void )
{
	if ( ! _initialed )
		return ;

	_initialed = false ;

	_udpclient->Stop();
	_tcpclient->Stop();
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
