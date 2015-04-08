/*
 * picclient.cpp
 *
 *  Created on: 2012-11-15
 *      Author: humingqing
 *  媒体数据图片下载分析对象
 */

#include "picclient.h"
#include <comlog.h>
#include <inetfile.h>
#include <tools.h>
#include <httpclient.h>

CPicClient::CPicClient()
{
	_picqueue   = new CDataQueue<_picData>(102400) ;
	_queuethread= new CQueueThread( _picqueue, this ) ;
	_pobj = NetFileMgr::getfileobj( NetFileMgr::ASYN_MODE ) ;
}

CPicClient::~CPicClient()
{
	if ( _queuethread != NULL ) {
		delete _queuethread ;
		_queuethread = NULL ;
	}

	if ( _picqueue != NULL ) {
		delete _picqueue ;
		_picqueue = NULL ;
	}
	// 初始化处理
	if ( _pobj != NULL ) {
		NetFileMgr::release( _pobj ) ;
		_pobj = NULL ;
	}
}

bool CPicClient::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	char buf[1024] = {0};
	// 取得图片地址
	if ( ! pEnv->GetString( "media_dir", buf ) ) {
		OUT_ERROR( NULL, 0, NULL, "get media_dir failed" ) ;
		return false ;
	}
	_basedir = buf ;

	// 媒体图片URL
	if ( !pEnv->GetString( "media_url", buf ) ) {
		OUT_ERROR( NULL, 0, NULL, "get media_url failed" ) ;
		return false ;
	}
	_baseurl = buf ;

	if ( ! pEnv->GetString( "cfs_ip" , buf ) ){
		OUT_ERROR( NULL, 0, NULL, "get cfs ip failed" ) ;
		return false ;
	}
	_cfs_ip = buf ;

	if ( ! pEnv->GetString( "cfs_user" , buf ) ) {
		OUT_ERROR( NULL, 0 , NULL, "get ftp save dir failed" ) ;
		return false ;
	}
	_cfs_user = buf ;

	if ( ! pEnv->GetString( "cfs_pwd" , buf ) ) {
		OUT_ERROR( NULL, 0, NULL, "get cfs pwd failed" ) ;
		return false ;
	}
	_cfs_pwd = buf ;

	int ntemp  = 0 ;
	if ( ! pEnv->GetInteger( "cfs_port" , ntemp ) ) {
		OUT_ERROR( NULL, 0, NULL, " get scp port failed" ) ;
		return false ;
	}
	_cfs_port = ntemp ;

	return true ;
}

bool CPicClient::Start( void )
{
	// 处理数据线程
	if ( ! _queuethread->Init( 1 ) ) {
		OUT_ERROR( NULL, 0, "MsgClient", "init pic thread failed" ) ;
		return false ;
	}
	return true ;
}

void CPicClient::Stop( void )
{
	// 停止线程处理
	_queuethread->Stop() ;
}

// 添加媒体数据
bool CPicClient::AddMedia( const char *data, int len )
{
	if ( len < 4 )
		return false ;

	// 如果为图片，先将图片上传到服务器，然后再处理
	_picData *p = new _picData;
	p->_data.assign( data, len ) ;
	p->_next = NULL ;

	if ( ! _queuethread->Push( p ) ) {
		delete p ;
		return false;
	}
	return true ;
}

// 处理数据队列缓存
void CPicClient::HandleQueue( void *packet )
{
	_picData *p = ( _picData *)packet ;
	// ToDo: 下载图片，上传到图片服务器
	size_t pos = p->_data.find("125:") ;
	// 如果没找着图片路径直接传送
	if ( pos == string::npos ) {
		// 加密处理数据
		_pEnv->GetMsgClient()->HandleData( p->_data.c_str(), p->_data.length(), false ) ;
		return ;
	}
	size_t end = p->_data.find( ",", pos ) ;
	if ( end == string::npos ) {
		end = p->_data.find( "}", pos ) ;
	}
	if ( end == string::npos ) {
		// 加密处理数据
		_pEnv->GetMsgClient()->HandleData( p->_data.c_str(), p->_data.length(), false ) ;
		return ;
	}

	// 无论上传远程服务器成功与否都传递内部协议
	string path = p->_data.substr( pos+4, end-pos-4 ) ;
	if ( ! path.empty() ) {
		// printf( "path %s\n", path.c_str() ) ;
		// 如果读取本地图片没有就从网络上面取
		if ( ! writeFile( path.c_str() ) ) {
			writeHttp( path.c_str() ) ;
		}
	}
	// 加密处理数据
	_pEnv->GetMsgClient()->HandleData( p->_data.c_str(), p->_data.length(), false ) ;
}

// 保存到服务器上处理
bool CPicClient::saveFile( const char *path, const char *ptr, int len )
{
	// 这里串行化处理图片上传
	int ret = _pobj->write( path, ptr, len ) ;
	if ( ret == FILE_RET_SUCCESS ) {
		return true ;
	}

	// 如果没有连接或者发送失败
	if ( ret == FILE_RET_NOCONN || ret == FILE_RET_SENDERR || ret == FILE_RET_READERR ) {
		ret = _pobj->open( _cfs_ip.c_str(), _cfs_port, _cfs_user.c_str(), _cfs_pwd.c_str() ) ;
		if ( ret != FILE_RET_SUCCESS ) {
			OUT_ERROR( _cfs_ip.c_str(), _cfs_port, "Pic", "open file mgr failed, result %d" , ret ) ;
			return false ;
		}
	}

	int retry = 0 ;
	// 如果发送文件失败重试几次
	while( ++ retry < 3 ) {
		ret = _pobj->write( path, ptr, len ) ;
		if ( ret == FILE_RET_SUCCESS ) {
			break ;
		}
	}
	// 返回是否写入文件成功
	return ( ret == FILE_RET_SUCCESS ) ;
}

// 写入远程文件
bool CPicClient::writeFile( const char *path )
{
	if ( _basedir.empty() )
		return false ;

	char szdir[1024] = {0} ;
	sprintf( szdir, "%s/%s", _basedir.c_str(), path ) ;

	int len = 0 ;
	char *ptr = ReadFile( szdir, len ) ;
	if ( ptr == NULL ) {
		OUT_WARNING( _cfs_ip.c_str(), _cfs_port, "Pic", "read dir %s pic failed", szdir ) ;
		return false ;
	}

	// 保存文件
	if ( ! saveFile( path, ptr, len ) ) {
		OUT_ERROR( _cfs_ip.c_str(), _cfs_port, "Pic", "save file %s failed" , path ) ;
		FreeBuffer(ptr) ;
		return false ;
	}
	FreeBuffer(ptr) ;
	//异常处理
	return true ;
}

// 通过HTTP读取图片来处理
bool CPicClient::writeHttp( const char *path )
{
	if ( _baseurl.empty() )
		return false ;

	char szurl[1024] = {0} ;
	sprintf( szurl, "%s/%s", _baseurl.c_str(), path ) ;

	CHttpRequest req ;
	req.SetURL( szurl ) ;

	CHttpResponse rsp ;
	CHttpClient httper ;

	// 通过网络HTTP来读取图片
	if ( httper.HttpRequest( req, rsp ) != E_HTTPCLIENT_SUCCESS ) {
		OUT_ERROR( NULL, 0, "http", "get pic from url %s failed", szurl ) ;
		return false ;
	}

	int len = 0 ;
	const char *ptr = rsp.GetBody( len ) ;
	if ( len == 0 ) {
		OUT_ERROR( NULL, 0, "http", "get pic from url %s failed, data length zero", szurl ) ;
		return false ;
	}
	// 保存文件
	if ( ! saveFile( path, ptr, len ) ) {
		OUT_ERROR( _cfs_ip.c_str(), _cfs_port, "Pic", "save file %s failed" , path ) ;
		return false ;
	}
	return true ;
}




