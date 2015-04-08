/*
 * waymgr.cpp
 *
 *  Created on: 2012-5-31
 *      Author: humingqing
 *
 *  插件管理对象
 */

#include "waymgr.h"
#include <comlog.h>
#include <dlfcn.h>
#include <tools.h>
#include <assert.h>
#include <set>

// 动态加载处理对象
extern "C" {

	typedef IPlugWay* ( *GETOBJECT ) ( void );

	typedef void ( *FREEOBJ ) ( IPlugWay* );
}

// 动态库通道管理对象
CWayMgr::CWayMgr( IPlugin *handler )
	: _pHandler( handler ), _inited(false)
{

}

CWayMgr::~CWayMgr()
{
	Stop() ;
	Clear() ;
}

// 初始化对象
bool CWayMgr::Init( void )
{
	char szbuf[256] = {0};
	if ( ! _pHandler->GetString( "plugin_conf", szbuf ) ) {
		OUT_ERROR( NULL, 0, NULL, "get plug way path failed" ) ;
		return false ;
	}
	_dllconf = szbuf ;

	if ( ! _pHandler->GetString( "plugin_root", szbuf ) ) {
		OUT_ERROR( NULL, 0, NULL, "get plug way root failed" ) ;
		return false ;
	}
	_dllroot = szbuf ;

	if ( ! _thread.init( 1, NULL, this ) ) {
		OUT_ERROR( NULL, 0, NULL, "init way manager thread failed" ) ;
		return false ;
	}
	_inited = true ;
	return true ;
}

// 启动程序
bool CWayMgr::Start( void )
{
	if ( ! _inited )
		return false ;

	_thread.start() ;
	return true ;
}

// 停止程序
bool CWayMgr::Stop( void )
{
	if ( ! _inited )
		return false ;

	_inited = false ;
	_monitor.notify() ;
	_thread.stop() ;

	StopWay() ;
	return true ;
}

// 解析所有配置文件到MAP中
static bool split2conf( const std::string &s , std::map<std::string,std::string> &val )
{
	std::vector<std::string>  vec ;
	// 处理所有逗号分割处理
	if ( ! splitvector( s , vec, ";" , 0 ) ) {
		return false ;
	}

	string temp  ;
	size_t pos = 0 , end = 0 ;
	// 解析参数
	for ( pos = 0 ; pos < vec.size(); ++ pos ) {
		temp = vec[pos] ;
		end  = temp.find( "=" ) ;
		if ( end == string::npos ) {
			continue ;
		}
		val.insert( pair<string,string>( temp.substr(0,end), temp.substr( end+1 ) ) ) ;
	}
	// 解析出监控平台参数部分
	return ( ! val.empty() ) ;
}

// 加载文件
bool CWayMgr::LoadFile( const char *file, CMapN2Ids &mpids )
{
	FILE *fp = NULL;
	fp = fopen( file , "r" );
	if (fp == NULL) {
		OUT_ERROR( NULL, 0, NULL, "Load pcc user file %s failed", file ) ;
		return false ;
	}

	std::set<unsigned int> idset ;

	char buf[1024] = {0};
	int count = 0 ;
	while (fgets(buf, sizeof(buf), fp)) {
		unsigned int i = 0;
		while (i < sizeof(buf)) {
			if (!isspace(buf[i]))
				break;
			i++;
		}
		if (buf[i] == '#')
			continue;

		char temp[1024] = {0};
		for (int i = 0, j = 0; i < (int)strlen(buf); ++ i ) {
			if (buf[i] != ' ' && buf[i] != '\r' && buf[i] != '\n') {
				temp[j++] = buf[i];
			}
		}

		std::map<std::string,std::string> mp ;
		if ( ! split2conf( temp, mp ) ) {
			continue ;
		}

		// id=0;cmd=0;path=libtruck.so.1;url=http://192.168.5.45:8880/service.php;size=1000;send=15;recv=15
		if ( mp["id"].empty() || mp["cmd"].empty() || mp["url"].empty() ) {
			continue ;
		}

		// 处理串放到检测链上面
		unsigned int nid = atoi( mp["id"].c_str() ) ;
		if ( idset.find(nid) != idset.end() ) {
			// 索引ID重复
			continue ;
		}
		idset.insert( std::set<unsigned int>::value_type(nid) ) ;

		std::vector<std::string> vecid ;
		if ( ! splitvector( mp["cmd"], vecid, "," , 0 ) ) {
			continue ;
		}

		_PlugInfo info ;
		info._id   = nid ;
		info._url  = mp["url"] ;
		info._path = mp["path"] ;

		if ( ! mp["recv"].empty() ) {
			info._nrecv = atoi( mp["recv"].c_str() ) ;
		}
		if ( ! mp["send"].empty() ) {
			info._nsend = atoi( mp["send"].c_str() ) ;
		}
		if ( ! mp["size"].empty() ) {
			info._nqueue = atoi( mp["size"].c_str() ) ;
		}

		for( int i = 0 ; i < (int)vecid.size(); ++ i ) {
			if ( vecid[i].empty() )
				continue ;
			unsigned int cmd = atoi( vecid[i].c_str() ) ;
			info._vec.push_back( cmd ) ;
		}
		sprintf( buf, "%s_%s", info._path.c_str(), info._url.c_str() ) ;
		mpids.insert( make_pair( buf, info ) ) ;

		++ count ;
	}
	fclose(fp);
	fp = NULL;

	OUT_PRINT( NULL, 0, NULL, "load dllconf success %s, count %d" , file , count ) ;

	return true ;
}

void CWayMgr::CheckConf( void )
{
	// id=0;cmd=0;path=libtruck.so.2;url=http://192.168.5.45:8880/service.php;size=1000;send=15;recv=15
	if ( _dllconf.empty() ) return ;

	CMapN2Ids  mpid ;
	if ( ! LoadFile( _dllconf.c_str(), mpid ) ) {
		_mutex.lock() ;
		_wayc2id.clear() ;
		_mutex.unlock() ;
		return ;
	}

	CPlugCmdMap wayc2id ;
	CPlugNameMap::iterator itx ;

	CPlugNameMap wayn2id ;
	CPlugWayVec  wayvec ;

	int i = 0 ;
	CMapN2Ids::iterator it ;
	for ( it = mpid.begin(); it != mpid.end(); ++ it ) {

		_PlugInfo &info = it->second ;

		string name = it->first ;
		string path = _dllroot + "/" + info._path ;

		int size = (int)wayvec.size() ;
		if ( size <= (int)info._id ) {
			for ( i = size; i <=(int)info._id; ++ i ) {
				wayvec.push_back( NULL ) ;
			}
		}

		// 查找
		itx = _wayn2id.find( name ) ;
		// 取得索引
		CPlugWay *pway = NULL ;
		// 如果不存在
		if ( itx == _wayn2id.end() ) {
			// 创建通道
			pway = new CPlugWay( path.c_str() ) ;
			if ( ! pway->Init(_pHandler, info._url.c_str(), info._nsend, info._nrecv, info._nqueue ) ) {
				delete pway ;
				continue ;
			}
			if ( ! pway->Start() ) {
				delete pway ;
				continue ;
			}
			OUT_INFO( NULL, 0 , "Plug", "Init dll %s url %s success", path.c_str(), info._url.c_str() ) ;
		} else {
			pway = _wayvec[itx->second] ;
		}
		assert( pway != NULL ) ;

		pway->AddRef() ;
		wayvec[info._id] = pway ;
		wayn2id.insert( make_pair(name, info._id ) ) ;

		// 处理指令与SO的对应关系
		std::vector<unsigned int> &vecid = info._vec ;
		for ( i = 0; i < (int)vecid.size(); ++ i ) {
			wayc2id.insert( make_pair( vecid[i], info._id ) ) ;
		}
	}

	// 更新一下通道与命令之间的数据
	_mutex.lock() ;
	CPlugWayVec tmp = _wayvec ;
	_wayc2id = wayc2id ;
	_wayvec  = wayvec ;
	_wayn2id = wayn2id ;
	_mutex.unlock() ;

	// 清理数据
	if ( ! tmp.empty() ) {
		for ( i = 0; i < (int)tmp.size(); ++ i ) {
			if ( tmp[i] == NULL )
				continue ;
			tmp[i]->Release() ;
		}
		tmp.clear() ;
	}
}

// 线程执行对象
void CWayMgr::run( void *param )
{
	// 动态加载插件
	while( _inited ) {
		// id=0;cmd=0,1,2,3,4;path=libtruck.so.1;url=http://192.168.5.45:8880/service.php;size=1000;send=15;recv=15
		CheckConf() ;

		share::Synchronized syn( _monitor) ;
		{
			_monitor.wait( 30 ) ;
		}
	}
}

// 停止所有通道
void CWayMgr::StopWay( void )
{
	share::Guard guard( _mutex ) ;
	if ( _wayvec.empty() )
		return ;

	for ( int i = 0; i < (int)_wayvec.size(); ++ i ) {
		if ( _wayvec[i] == NULL )
			continue ;
		_wayvec[i]->Stop() ;
	}
}

// 根据命令字取得插件
CPlugWay * CWayMgr::CheckOut( unsigned int cmd , bool flag )
{
	share::Guard guard( _mutex ) ;
	if ( _wayvec.empty() )
		return NULL ;

	CPlugWay *pway = NULL ;
	CPlugCmdMap::iterator it = _wayc2id.find( cmd ) ;
	if ( it == _wayc2id.end() ) {
		if ( ! flag )
			return NULL;
		pway = _wayvec[0] ;
	} else {
		pway = _wayvec[it->second] ;
	}
	// 如果为实体添加引用
	if ( pway ) pway->AddRef() ;

	return pway ;
}

// 将插件签入
void CWayMgr::CheckIn( CPlugWay *plug )
{
	if( plug == NULL )
		return ;
	plug->Release() ;
}

// 清理所有数据
void CWayMgr::Clear( void )
{
	share::Guard guard( _mutex ) ;
	if ( _wayvec.empty() )
		return ;

	for ( int i = 0; i < (int)_wayvec.size(); ++ i ) {
		if ( _wayvec[i] == NULL )
			continue ;
		_wayvec[i]->Release() ;
	}
	_wayvec.clear() ;
	_wayn2id.clear() ;
	_wayc2id.clear() ;
}

//================================== 插件通道对象 =============================================
CPlugWay::CPlugWay( const char *path )
{
	_pWay 	 = NULL ;
	_hModule = NULL ;

	if ( ! LoadWay(path) ){
		OUT_ERROR( NULL, 0, "PlugWay", "load %s failed", path ) ;
		return;
	}
	OUT_INFO( NULL, 0, "PlugWay", "load %s success", path ) ;
}

CPlugWay::~CPlugWay()
{
	FreeWay() ;
}

// 需要初始化对象
bool CPlugWay::Init( IPlugin *handler , const char *url, int sendthread, int recvthread, int queuesize  )
{
	if ( _pWay == NULL )
		return false ;

	return _pWay->Init( handler, url, sendthread, recvthread, queuesize ) ;
}

// 初始化插件通道
bool CPlugWay::Start( void )
{
	return _pWay->Start() ;
}

// 停止插件通道
bool CPlugWay::Stop( void )
{
	return _pWay->Stop() ;
}

// 处理透传的数据
bool CPlugWay::Process( unsigned int fd, const char *data, int len , unsigned int cmd , const char *id )
{
	return _pWay->Process( fd, data, len , cmd , id ) ;
}

// 加载通道
bool CPlugWay::LoadWay( const char *path )
{
	// 如果文件不存在则直接返回
	if ( access( path , 0 ) != 0 ) {
		OUT_ERROR( NULL, 0, "LoadWay", "not found so %s file", path ) ;
		return false ;
	}
	// 是动态库了

	_hModule = dlopen( path , RTLD_NOW ) ;
	if ( _hModule != NULL ){
		GETOBJECT func_getobj = (GETOBJECT)dlsym( _hModule , "GetPlugObject" ) ;
		if ( func_getobj ){
			_pWay = func_getobj() ;
		}
	}
	return ( _pWay != NULL ) ;
}

// 释放通道
void CPlugWay::FreeWay( void )
{
	if ( _hModule == NULL ){
		return ;
	}

	// 如果模块不为空则失停止掉模块再释放
	if ( _pWay != NULL ){
		_pWay->Stop() ;
		FREEOBJ func_freeobj = (FREEOBJ)dlsym( _hModule , "FreePlugObject" ) ;
		if ( func_freeobj ) {
			func_freeobj( _pWay ) ;
			_pWay = NULL ;
		} else {
			delete _pWay ;
			_pWay = NULL ;
		}
	}
	dlclose( _hModule );
	_hModule = NULL ;
}




