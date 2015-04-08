/*
 * busloader.cpp
 *
 *  Created on: 2012-3-19
 *      Author: think
 */

#include "busloader.h"
#include "interface.h"
#include <comlog.h>
#include "mybase64.h"
#include <tools.h>

BusLoader::BusLoader():_inited(false)
{
	_index = 0 ;
}

BusLoader::~BusLoader()
{
	Stop() ;
}

// 初始化系统
bool BusLoader::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	char temp[1024] = {0};
	if ( ! _pEnv->GetString( "busfile", temp ) ) {
		OUT_ERROR( NULL, 0, "Bus", "get bus file path failed" ) ;
		return false ;
	}
	char buf[1024] = {0};
	getenvpath( temp, buf ) ;
	_sbusfile = buf ;

	if ( ! _thread.init( 1, this, this ) ) {
		OUT_ERROR( NULL, 0, "Bus", "init bus info thread failed" ) ;
		return false ;
	}
	_inited = true ;
	return true ;
}

// 启动线程
bool BusLoader::Start( void )
{
	_thread.start() ;
	return true ;
}

// 停止线程
void BusLoader::Stop( void )
{
	if ( ! _inited )
		return ;
	_inited = false ;
	_thread.stop() ;
}

// 加载车辆静态文件
void BusLoader::loadbusfile( void )
{
	if ( ! _pEnv->GetPasClient()->IsOnline() ) {
		OUT_WARNING( NULL, 0, "Bus" , "user not online" ) ;
		return ;
	}

	// 取得服务商的ID号
	const char *srvid = _pEnv->GetPasClient()->GetSrvId() ;
	if ( srvid == NULL )
		return ;

	char buf[1024] = {0};
	FILE *fp = NULL;
	fp = fopen( _sbusfile.c_str() , "r" );
	if (fp == NULL)
		return ;

	int count = 0 ;

	CBase64Ex base64 ;

	int i = 0, j = 0 ;
	std::vector<std::string> vec;
	while (fgets(buf, sizeof(buf), fp)){
		i = 0 ; j = 0 ;
		while (i < sizeof(buf)){
			if (!isspace(buf[i]))
				break;
			i++;
		}
		if (buf[i] == '#')
			continue;

		char temp[1024] = {0};
		for ( i = 0, j = 0; i < (int)strlen(buf); ++ i ){
			if (buf[i] != ' ' && buf[i] != '\r' && buf[i] != '\n'){
				temp[j++] = buf[i];
			}
		}

		vec.clear() ;

		string line = temp;
		if ( ! splitvector( line, vec, "|", 25 ) ) {
			continue ;
		}

		string scmd = "*";
		scmd.append( srvid ) ;
		scmd.append( "|" ) ;

		for ( i = 0; i < 25; ++ i ) {
			string &tmp = vec[i] ;
			if ( i == 1 || i == 5 || i == 6 || i == 18 || i == 21 ) {
				scmd.append( base64.EncodeEx( tmp.c_str(), tmp.length() ) ) ;
			} else {
				scmd.append( tmp ) ;
			}
			scmd.append( "|" ) ;
		}
		scmd.append( "#" ) ;

		// 发送静态数据
		_pEnv->GetPasClient()->HandleData( scmd.c_str(), scmd.length() ) ;

		++ count ;
	}
	fclose(fp);
	fp = NULL;

	char sname[1024] = {0};
	sprintf( sname, "%s.%lld-%d", _sbusfile.c_str(), time(NULL) , _index ) ;
	rename( _sbusfile.c_str(), sname ) ;
}

// 线程执行对象
void BusLoader::run( void *param )
{
	// 开始线程执行对象
	while( _inited ) {
		loadbusfile() ;
		sleep(10) ;
	}
}
