#include "scpmedia.h"
#include <comlog.h>
#include <BaseTools.h>
#include <assert.h>
#include <fcntl.h>
#include <tools.h>
#include <unistd.h>
#include <sys/stat.h>

#include <gd.h>

// 获取图片触发事件中文名称
static string getEventText(int event)
{
    const char *text[] = {
        "\xe5\xb9\xb3\xe5\x8f\xb0", "\xe5\xae\x9a\xe6\x97\xb6",
        "\xe5\x8a\xab\xe8\xad\xa6", "\xe7\xa2\xb0\xe6\x92\x9e",
        "\xe9\x97\xa8\xe5\xbc\x80", "\xe9\x97\xa8\xe5\x85\xb3", "OTC",
        "\xe5\xae\x9a\xe8\xb7\x9d", "\xe7\x99\xbb\xe5\xbd\x95"};
    const int size = sizeof(text) / sizeof(void*);

    event &= 0xf;
    if(event < 0 || event > size - 1) {
        return "\xe6\x9c\xaa\xe7\x9f\xa5";
    }

    return text[event];
}


// 依次创建目录
static void reverse_mkdir( const char *root, const char *path )
{
	char buf[512] = {0} ;
	sprintf( buf, "%s" , path ) ;
	char *p = buf ;

	char szpath[1024] = {0} ;
	sprintf( szpath, "%s" , root ) ;

	char *q = strstr( p , "/" ) ;
	while ( q != NULL ) {
		*q = 0 ;
		strcat( szpath, "/" ) ;
		strcat( szpath, p   ) ;
		if ( access( szpath , 0 ) != F_OK ) {
			mkdir( szpath, 0777 ) ;
		}
		p = q + 1 ;
		q = strstr( p, "/" ) ;
	}
	if ( p != NULL ) {
		strcat( szpath, "/" ) ;
		strcat( szpath, p   ) ;
		if ( access( szpath , 0 ) != F_OK   ) {
			mkdir( szpath, 0777 ) ;
		}
	}
}

CScpMedia::CScpMedia() : _cfs_enable(false)
{
	_thread_num   = 1 ;
	_last_check   = 0 ;
	_netobj._pobj = NULL ;
	_mediaqueue   = new CDataQueue<MultiDesc>(102400) ;
	_queuethread= new CQueueThread( _mediaqueue, this ) ;
}

CScpMedia::~CScpMedia()
{
	Stop() ;

	if ( _queuethread != NULL ) {
		delete _queuethread ;
		_queuethread = NULL ;
	}

	if ( _mediaqueue != NULL ) {
		delete _mediaqueue ;
		_mediaqueue = NULL ;
	}

	// 释放网络文件对象
	if ( _netobj._pobj != NULL ) {
		NetFileMgr::release( _netobj._pobj ) ;
		_netobj._pobj = NULL ;
	}
}

bool CScpMedia::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	char buf[1024] = {0};
	if ( ! pEnv->GetString( "cfs_ip" , buf ) ){
		printf( "get ftp ip failed, %s:%d\n", __FILE__, __LINE__ ) ;
		return false ;
	}
	_cfs_ip = buf ;

	if ( ! pEnv->GetString( "cfs_user" , buf ) ) {
		printf( "get ftp save dir failed, %s:%d\n" , __FILE__, __LINE__ ) ;
		return false ;
	}
	_cfs_user = buf ;

	if ( ! pEnv->GetString( "cfs_pwd" , buf ) ) {
		printf( "get ftp pwd failed, %s:%d\n" , __FILE__, __LINE__ ) ;
		return false ;
	}
	_cfs_pwd = buf ;

	int ntemp  = 0 ;
	if ( ! pEnv->GetInteger( "cfs_port" , ntemp ) ) {
		printf( " get scp port failed, %s:%d\n" , __FILE__, __LINE__ ) ;
		return false ;
	}
	_cfs_port = ntemp ;

	if ( pEnv->GetInteger( "cfs_thread" , ntemp ) ) {
		_thread_num = ntemp ;
	}
	// 是否开启FTP文件处理
	if ( pEnv->GetInteger( "cfs_enable" , ntemp ) ) {
		_cfs_enable = ( ntemp == 1 ) ;
	}
	// 是否开启日期水印,这里系统必须安装了imagemagick软件
	if ( pEnv->GetInteger( "water_enable", ntemp ) ) {
		_water_enable = ( ntemp == 1 ) ;
	}

	gdFTUseFontConfig(1);

	// 如果开启远程文件处理就初始化对象
	if ( _cfs_enable ) {
#ifdef _SYN_MODE_
		_netobj._pobj = NetFileMgr::getfileobj( NetFileMgr::SYN_MODE ) ;
#else
		_netobj._pobj = NetFileMgr::getfileobj( NetFileMgr::ASYN_MODE ) ;
#endif
	}
	// 初始SCP文件管理对象
	return _scp_manager.Init( pEnv ) ;
}

bool CScpMedia::Start( void )
{
	return _queuethread->Init( _thread_num ) ;
}

void CScpMedia::Stop( void )
{
	_queuethread->Stop() ;
}

// 取得文件后缀
static const char * getfileext( int ntype )
{
	 switch(ntype)
	 {
	 case 0:
		return ".jpeg";
	 case 1:
		return ".tif";
	 case 2:
		return ".mp3";
	 case 3:
		return ".wav";
	 case 4:
		return ".wmv";
	 default:
		break;
	 }
	 return ".data" ;
}

// 保存数据包处理
bool CScpMedia::SavePackage( int fd, const char *macid, int id, int total, int index, int type,
		int ftype,int event,int channel, const char * data, int len , const char *gps )
{
	if ( data == NULL || len == 0 ) {
		return false ;
	}

	MultiDesc  *pdesc = new MultiDesc;
	// 保存SCP文件数据，如果分块文件，则直接保存在内存中处理
	if ( ! _scp_manager.SaveScpFile( fd, macid, id ,total, index, type, ftype, event, channel, data, len , gps , pdesc ) ){
		OUT_INFO( NULL, 0, "scpmedia" , "save macid %s , id %d , data len %d , index %d , total %d , type %d , ftype %d, event %d ,channel %d , %s:%d" ,
				macid, id, len, index, total, type, ftype, event, channel, __FILE__, __LINE__ ) ;
		delete pdesc ;
		return false ;
	}

	// 存放数据，交以后线程上传FTP
	if ( ! _queuethread->Push( pdesc ) ) {
		delete pdesc ;
		return false ;
	}
	return true ;
}

// 清除车机的数据
void CScpMedia::RemovePackage( const char *macid )
{
	if ( macid == NULL ) {
		return ;
	}
	// 移除上一次车机没有上传完成的图片文件
	_scp_manager.RemoveScpFile( macid ) ;
}

// 将字符串时间转为BCD时间
static const string getstrtime( void )
{
	time_t ntime  = time(NULL) ;
	struct tm local_tm;
	struct tm *tm = localtime_r( &ntime, &local_tm ) ;

	char buf[256] = {0} ;
	sprintf( buf, "%04d.%02d.%02d %02d:%02d:%02d",
			( tm->tm_year + 1900 ) , tm->tm_mon + 1, tm->tm_mday, tm->tm_hour, tm->tm_min, tm->tm_sec) ;

	// 转换为BCD时间
	return string(buf) ;
}

// 执行进程
static bool executeprocess( char *cmd )
{
	// 执行进程
	FILE *fp = NULL;
	if( ( fp=popen( cmd, "r" ) ) == NULL ) {
		//异常处理
		return false ;
	}
	pclose(fp);

	return true ;
}

// 写入远程文件
bool CScpMedia::writeFile( const char *dir, const char *name, const char *path )
{
	if ( ! _cfs_enable || _netobj._pobj == NULL )
		return true ;

	int   len = 0 ;
	char *ptr = ReadFile( path, len ) ;
	if ( ptr == NULL ) {
		OUT_ERROR( _cfs_ip.c_str(), _cfs_port, "scpmedia", "read file %s failed" , path ) ;
		//异常处理
		return false ;
	}

	char szfile[256] = {0} ;
	sprintf( szfile, "%s/%s", dir, name ) ;

	// 这里串行化处理图片上传
	share::Guard guard( _netobj._mutex ) ;

#ifdef _SYN_MODE_
	// 如果没有连接或者发送失败
	int ret = _netobj._pobj->open( _cfs_ip.c_str(), _cfs_port, _cfs_user.c_str(), _cfs_pwd.c_str() ) ;
	if ( ret != FILE_RET_SUCCESS ) {
		OUT_ERROR( _cfs_ip.c_str(), _cfs_port, "scpmedia", "open file mgr failed, result %d" , ret ) ;
		FreeBuffer( ptr ) ;
		return false ;
	}
#else
	int ret = _netobj._pobj->write( szfile, ptr, len ) ;
	if ( ret == FILE_RET_SUCCESS ) {
		FreeBuffer( ptr ) ;
		return true ;
	}

	// 如果没有连接或者发送失败
	if ( ret == FILE_RET_NOCONN || ret == FILE_RET_SENDERR || ret == FILE_RET_READERR ) {
		ret = _netobj._pobj->open( _cfs_ip.c_str(), _cfs_port, _cfs_user.c_str(), _cfs_pwd.c_str() ) ;
		if ( ret != FILE_RET_SUCCESS ) {
			OUT_ERROR( _cfs_ip.c_str(), _cfs_port, "scpmedia", "open file mgr failed, result %d" , ret ) ;
			FreeBuffer( ptr ) ;
			return false ;
		}
	}
#endif

	int retry = 0 ;
	// 如果发送文件失败重试几次
	while( ++ retry < 3 ) {
		ret = _netobj._pobj->write( szfile, ptr, len ) ;
		if ( ret == FILE_RET_SUCCESS ) {
			break ;
		}
	}
	FreeBuffer( ptr ) ;

	// 返回是否写入文件成功
	return ( ret == FILE_RET_SUCCESS ) ;
}

// 检测超时处理
void CScpMedia::CheckTimeOut( void )
{
	time_t now = time(NULL) ;
	// 每隔三十秒检测一次
	if ( now - _last_check > 30 ) {
		_last_check = now ;
		// 处理SCP的超时数据
		_scp_manager.CheckTimeOut( SCP_MAX_TIMEOUT ) ;
	}
}

static bool picNote(const char *path, char *text)
{
	FILE *fp;
	int c;
	int x, y, w, h;
	int rect[8] = { 0 };
	gdImagePtr im;

	if ((fp = fopen(path, "rb")) == NULL) {
		return false;
	}

	im = gdImageCreateFromJpeg(fp);
	fclose(fp);
	if (im == NULL) {
		return false;
	}

	c = gdImageColorAllocate(im, 255, 255, 255);
	w = gdImageSX(im);
	h = gdImageSY(im);

	memset(rect, 0x00, sizeof(rect));
	gdImageStringFT(NULL, rect, c, "SimHei", 13, 0.0, 0, 0, text);
	x = w - rect[4] - 5;
	y = h - 10;
	gdImageStringFT(im, NULL, c, "SimHei", 13, 0.0, x, y, text);

	if ((fp = fopen(path, "wb")) == NULL) {
		gdImageDestroy(im);
		return false;
	}

	gdImageJpeg(im, fp, -1);
	fclose(fp);

	gdImageDestroy(im);

	return true;
}

// 线程执行对象方法
void CScpMedia::HandleQueue( void *packet )
{
	MultiDesc *elem = (MultiDesc*) packet ;
	// mogrify -pointsize 16 -fill white -weight bolder -gravity southeast -annotate +5+5 "2012.03.03 16:59 #1" 1.jpg
	if ( _water_enable && elem->_type == 0 ) {  // 如果打开水印且为图片时才开启水印操作
		string stime = getstrtime() ;
		char buffer[1024] = {0};

		snprintf(buffer, 1024, "%s #%d%s", stime.c_str(), elem->_channel, getEventText(elem->_event).c_str());
		if ( ! picNote(elem->_path.c_str(), buffer) ) {
			OUT_ERROR( NULL, 0, "scpmedia", "Water %s , file path %s, water date %s failed", buffer, elem->_path.c_str(), stime.c_str() ) ;
		} else {
			OUT_PRINT( NULL, 0, "scpmedia",  "Water %s, file path %s, water date %s success", buffer, elem->_path.c_str(), stime.c_str() ) ;
		}
	}

	// 写入远程服务器
	if ( ! writeFile( elem->_abspath.c_str(), elem->_name.c_str(), elem->_path.c_str() ) ) {
		OUT_ERROR( NULL, 0, "scpmedia", "upload file to server failed, %s" , elem->_name.c_str() ) ;
		return ;
	}

	char buf[2048] = {0} ;
	sprintf( buf, "CAITS 0_0 %s 0 U_REPT {TYPE:3,120:%d,124:%d,121:%d,122:%d,123:%d,125:%s/%s,%s,CHANNEL_TYPE:0} \r\n",
			elem->_macid.c_str(), elem->_id, elem->_channel, elem->_type, elem->_ftype, elem->_event, elem->_abspath.c_str(), elem->_name.c_str() , elem->_gps.c_str() ) ;
	// 上传数据
	_pEnv->GetMsgClient()->HandleUpData( buf, strlen(buf) ) ;
}
///////////////////////////////////CScpFileManager////////////////////////////////////////

CScpMedia::CScpFileManager::CScpFileManager()
{

}

CScpMedia::CScpFileManager::~CScpFileManager()
{
	share::Guard g( _mutex ) ;

	int size = 0 ;
	CScpFile *p = _queue.move(size) ;
	if ( size == 0 ) {
		return ;
	}

	while( p != NULL ) {
		p = p->_next ;
		delete p->_pre ;
	}
	_index.clear() ;
}

// 初始化对象
bool CScpMedia::CScpFileManager::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	char buf[512] = {0} ;
	if ( ! pEnv->GetString( "cur_dir" , buf ) ) {
		printf( "get current save file failed, %s:%d\n" , __FILE__, __LINE__ ) ;
		return false ;
	}

	_cur_dir = buf ;
	// 取得环境路径
	getenvpath( _cur_dir.c_str(), buf ) ;
	// 如果目录不存在则创建它
	mkdir( buf, 0777 ) ;
	// 重新处理路径
	_cur_dir = buf ;

	return true ;
}

// 保存数据包处理
bool CScpMedia::CScpFileManager::SaveScpFile( int fd, const char *macid, int id, int total, int index, int type,
			int ftype,int event,int channel, const char * data, int len , const char *gps , MultiDesc *desc )
{
	share::Guard g( _mutex ) ;
	{
		//构建唯一的KEY值 macid ;
		string key = macid ;
		key += "_" ;
		key += uitodecstr(total) ;

		bool binsert = false ;

		CScpFile *p = NULL ;
		MapScpFile::iterator it = _index.find( key ) ;
		if ( it == _index.end() ) {
			p = new CScpFile(key.c_str());
			// 初始化参数设置
			binsert = p->Init( fd, macid, id, total, type, ftype, event, channel, _cur_dir.c_str() , gps ) ;
		} else {
			p = it->second ;
			// 如果第一个包不是第一个到达则需要重新设置文件名
			if ( index == 0x01 ) {
				p->SetParam( fd, macid, id, total, type, ftype, event, channel, _cur_dir.c_str() , gps ) ;
			}
			p = _queue.erase(p) ;
		}

		// 如果为保存文件数据成功
		if ( p->SaveData( index, data, len , desc ) ) {
			if( ! binsert )
				_index.erase( key ) ;
			delete p ;
			return true;
		}

		OUT_INFO( NULL, 0, "scpmedia", "save data macid %s , total %d , index %d , len %d" , macid, total, index, len ) ;

		_queue.push( p ) ;
		if ( binsert ) {
			// 如果为多包处理
			_index.insert( pair<string,CScpFile*>( key, p ) ) ;
		}
		return false ;
	}
}


// 清除指定的MAC车机的多媒数据
void CScpMedia::CScpFileManager::RemoveScpFile( const char *macid )
{
	share::Guard g( _mutex ) ;
	{
		string key = macid ;
		MapScpFile::iterator it = _index.find( key ) ;
		if ( it == _index.end() ) {
			return ;
		}

		CScpFile *p = it->second ;
		_index.erase( it ) ;

		delete _queue.erase(p) ;
	}
}

// 检测超时的内存文件
void CScpMedia::CScpFileManager::CheckTimeOut( const int timeout )
{
	share::Guard g( _mutex ) ;
	{
		if ( _queue.size() == 0 ){
			return ;
		}
		time_t now = time(NULL) - timeout ;

		CScpFile *t = NULL ;
		CScpFile *p = _queue.begin() ;
		while( p != NULL ) {
			t = p ;
			p = p->_next ;
			if ( t->_last > now )
				break ;

			_index.erase( t->_key ) ;
			delete _queue.erase(t) ;
		}
	}
}

/////////////////////////////////////// CScpFile /////////////////////////////////////
CScpMedia::CScpFile::CScpFile( const char *key )
	: _cur(0), _key(key)
{
}

CScpMedia::CScpFile::~CScpFile()
{
	if ( _vec.empty() ) {
		return ;
	}

	// 清理内存文件
	MemFile *p = NULL ;
	for ( size_t i = 0; i < _vec.size(); ++ i ){
		p = _vec[i] ;
		if ( p->_ptr != NULL && p->_len != 0 ){
			delete [] p->_ptr ;
		}
		delete p ;
	}
	_vec.clear() ;
}

// 设置参数处理
bool CScpMedia::CScpFile::SetParam( int fd, const char *macid, int id, int total, int type, int ftype, int event,
				int channel , const char *curdir , const char *gps )
{
	time_t cur_t = _now ;
	struct tm local_tm;
	struct tm *nowtms = localtime_r( &cur_t, &local_tm ) ;

	char tbuf[256];
	strftime(tbuf,256,"%Y%m%d%H%M%S",nowtms);

	string filename = tbuf ;
	//filename = datetime-macid-mid-mevent-mchannel-mtype-mfiletype;
	filename += "-" ;
	filename += macid ;
	filename += "-"+uitodecstr(id) ;
	filename += "-"+ustodecstr(event) ;
	filename += "-"+ustodecstr(channel) ;
	filename += "-"+ustodecstr(type) ;
	filename += "-"+ustodecstr(ftype) ;
	filename += getfileext(ftype) ;

	strftime(tbuf,256,"%Y/%m/%d" , nowtms ) ;

	string filepath = curdir ;
	filepath += "/" ;
	filepath += tbuf ;
	filepath += "/" ;
	filepath += filename ;

	// 需要依次创建目录
	reverse_mkdir( curdir , tbuf ) ;

	// 存放数据，交以后线程上传FTP
	_desc._fd      = fd ;
	_desc._macid   = macid ;
	_desc._id      = id ;
	_desc._total   = total ;
	_desc._type    = type ;
	_desc._ftype   = ftype ;
	_desc._event   = event ;
	_desc._channel = channel ;
	_desc._name    = filename ;
	_desc._path    = filepath ;
	_desc._abspath = tbuf ;
	if ( gps )
		_desc._gps = gps ;

	return true ;
}
// 初始化对象
bool CScpMedia::CScpFile::Init( int fd, const char *macid, int id, int total, int type, int ftype, int event, int channel , const char *curdir , const char *gps )
{
	time_t cur_t = time(0);
	_now = cur_t ;

	// 设置参数
	SetParam( fd, macid, id, total, type, ftype, event, channel, curdir , gps ) ;

	// 没有分包数据
	if ( total == 1 )
		return true;

	// 如果分包数据
	for ( int i = 0; i < total; ++ i )
	{
		MemFile *p = new MemFile ;
		p->_index  = 0 ;
		p->_len	   = 0 ;
		p->_ptr    = NULL ;
		_vec.push_back( p ) ;
	}

	return true ;
}

// 保存文件
bool CScpMedia::CScpFile::SaveData( const int index, const char * data, int len , MultiDesc *desc )
{
	// 先检测数据的正确性
	if ( data == NULL || len == 0 || index > _desc._total ){
		OUT_ERROR( NULL, 0, "ftp" , "file index %d total %d len %d" , index, _desc._total , len ) ;
		return false ;
	}

	_last = time(NULL) ;
	// 如果只有一个数据包直接保存
	if ( _desc._total == 1 ) {
		// 保存到本地文件目录
		if ( ! AppendFile( _desc._path.c_str(), data, len ) ) {
			OUT_ERROR( NULL, 0, "scpmedia" , "ftp save file to path %s failed" , _desc._path.c_str() ) ;
			return false ;
		}
		// 拷贝返回数据
		desc->Copy( _desc ) ;

		OUT_INFO( NULL, 0, "scpmedia" , "ftp save file path %s" , _desc._path.c_str() ) ;

		return true ;
	}
	// 如果索引值超过了最大包数则直接返回
	if ( index > (int)_vec.size() ) {
		OUT_ERROR( NULL, 0, "scpmedia" , "ftp save file index %d over max total %d" , index, _vec.size()  ) ;
		return false ;
	}
	// 取得索引值
	MemFile *p = _vec[index-1] ;
	if ( p->_ptr == NULL ) {
		// 如第一上传的数据才记录上传正确的包数
		++ _cur ;
		// 开辟新的空间
		p->_ptr    = new char[len] ;

	} else {
		// 如果这一次传上数据长度要大于上一次上传长度
		if ( p->_len < len ) {
			// 释放原来的内存
			delete [] p->_ptr ;
			p->_ptr   = new char[len] ;
		}
	}

	p->_len    = len ;
	p->_index  = index ;
	memcpy( p->_ptr, data, len ) ;

	if ( (int)_cur != _desc._total ) {
		OUT_INFO( NULL, 0, "scpmedia" , "scp save index file %d, current count %d total %d" , index, _cur, _desc._total ) ;
		return false ;
	}

	// 如果所有数据包都保存成功，则直接保存成文件
	for ( size_t i = 0; i < _vec.size(); ++ i ) {
		p = _vec[i] ;
		if ( p->_ptr == NULL || p->_len == 0 ){
			continue;
		}
		// 保存文件，处理文件各块
		AppendFile( _desc._path.c_str(), p->_ptr, p->_len ) ;
	}
	// 拷贝返回数据
	desc->Copy( _desc ) ;

	OUT_INFO( NULL, 0, "scpmedia" , "ftp save file path %s" , _desc._path.c_str() ) ;

	return true ;
}
