//#include "stdafx.h"
#include <string.h>
#include <stdarg.h>
#include <ctype.h>
#include <time.h>
#include <stdio.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <Monitor.h>
#include <Thread.h>
#include <map>
#include <string.h>

#ifndef WIN32
#include <unistd.h>
#include <dirent.h>
#include <fcntl.h>
#include <strings.h>
#include <errno.h>
#include <pthread.h>
extern int errno;
#include <sys/syscall.h>
#else
#include <process.h>
#include <windows.h>
#endif

#include "cLog.h"
#define BUF_LEN 10240
#define TIME_BUF_LEN 20
#define KEY_WORD_LEN 10
#define IP_LEN  18
#define PORT_LEN 8
#define USER_ID_LEN 20

#ifndef S_IRWXUGO
# define S_IRWXUGO (S_IRWXU | S_IRWXG | S_IRWXO)
#endif

static string ustodecstr_log(unsigned short us)
{
	string dest;
	char buf[16] = {0};
	sprintf(buf,"%u",us);
	dest += buf;
	return dest;
}

/**
 * 创建多级目录, 在父目录不存在一同创建
 */
static bool mkdirs_log(char *szDirPath)
{
	struct stat stats;
	if (lstat (szDirPath, &stats) == 0 && S_ISDIR (stats.st_mode))
		return true;

	mode_t umask_value = umask (0);
	umask (umask_value);
	mode_t mode = (S_IRWXUGO & (~ umask_value)) | S_IWUSR | S_IXUSR;

	char *slash = szDirPath;
	while (*slash == '/')
		slash++;

	while (1)
	{
		slash = strchr (slash, '/');
		if (slash == NULL)
			break;

		*slash = '\0';
		int ret = mkdir(szDirPath, mode);
		*slash++ = '/';
		if (ret && errno != EEXIST) {
			return false;
		}

		while (*slash == '/')
			slash++;
	}
	if (mkdir(szDirPath, mode)) {
		return false;
	}
	return true;
}

//========================== 日志线程  =============================
class CCLog ;
class CLogThread : public share::Runnable
{
public:
	CLogThread(CCLog *p):_inited(false) {
		_pLog = p ;
		start() ;
	}
	~CLogThread() {
		stop() ;
	}
	// 运行线程
	void run( void *param ){
		while( _inited ) {
			// 检测文件是否存在
			_pLog->checklogfile() ;
			// 使用同步锁
			share::Synchronized s(_monitor) ;
			// 每隔3秒将内存日志写入文件
			_monitor.wait( 5 ) ;
		}
	}

	// 发送通知信号
	void notify( void ) {
		_monitor.notify() ;
	}

private:
	void start( void ) {
		if ( ! _threadmgr.init( 1, this, this ) ) {
			printf( "start clear thread failed\n" ) ;
			return ;
		}
		_inited = true ;
		_threadmgr.start() ;
		printf( "start log check thread\n" ) ;
	}
	void stop( void ) {
		printf(" stop log check thread\n" ) ;
		if ( ! _inited )
			return ;
		_inited = false ;
		_monitor.notifyEnd() ;
		_threadmgr.stop() ;
	}
private:
	CCLog				 *_pLog ;
	// 线程管理对象
	share::ThreadManager  _threadmgr ;
	// 是否初始化
	bool 				  _inited ;
	// 信号来阻塞
	share::Monitor		  _monitor ;
};

//////////////////////////////////// 日志的类的处理  ///////////////////////////////////////////////
CCLog::CCLog()
{
	_log_size        = 2000 * 2000 ;
	_log_num         = 20 ;
	_log_level       = 3 ;
	_file_fd		 = -1 ;
	_check_time		 = time(NULL) ;
	_LogBlock.offset = 0 ;
	_LogBlock.size   = 0 ;
	// 初始化文件检测线程
	_pthread = new CLogThread(this) ;
}

CCLog::~CCLog(void)
{
	// 停止文件检测线程
	if( _pthread != NULL ) {
		delete _pthread ;
		_pthread = NULL ;
	}
	writedisk() ;
	closefile() ;
}

//安全性和效率的统一
bool CCLog::print_net_msg(unsigned short log_level, const char *file, int line, const char *function, const char *key_word,
		const char * ip, int port, const char *user_id, const char *format, ...)
{
	//日志级别，1-7，数字越小日志日志级别越高。
	if( _log_level == 0 )
		return false;
	if(log_level > _log_level )
		return false;

	char msg[MAX_LOG_LENGTH + 1] = {0};
	int pos = 0;

	time_t t;
	time(&t);
	struct tm local_tm;
	struct tm *tm = localtime_r( &t, &local_tm ) ;

	pos = snprintf(msg, MAX_LOG_LENGTH, "%04d%02d%02d-%02d:%02d:%02d", tm->tm_year + 1900,
			tm->tm_mon + 1, tm->tm_mday, tm->tm_hour, tm->tm_min, tm->tm_sec);

	key_word = (key_word != NULL) ? key_word : "NULL";
	pos += snprintf(msg + pos, MAX_LOG_LENGTH - pos, "--%.4s", key_word);

	ip = (ip != NULL) ? ip : "NULL";
	pos += snprintf(msg + pos, MAX_LOG_LENGTH - pos, "--%.15s:%d", ip, port);

	user_id = (user_id != NULL) ? user_id : "NULL";
	pos += snprintf(msg + pos, MAX_LOG_LENGTH - pos, "--%.16s--", user_id);

	if (format != NULL && pos < MAX_LOG_LENGTH){
		va_list ap;
		va_start(ap, format);
		vsnprintf(msg + pos, MAX_LOG_LENGTH - pos, format, ap);
		va_end(ap);
	}

	private_log( msg , file, line, function , ( strncmp( key_word, "RUNNING", 7 ) == 0 ) ) ;

	return true;
}

// 输出十六制的日志数据
void CCLog::print_net_hex( unsigned short log_level, const char *file, int line, const char *function, const char * ip, int port, const char *user_id, const char *data, const int len )
{
	// 如果关闭调试日志
	if( log_level > _log_level ) return ;
	// 先确定一下是否超出长度
	if ( 2*len+300 > MAX_LOG_LENGTH || len <= 0 || data == NULL ) {
		return  ;
	}

	// 使用堆上面空间效率会高一点
	char buf[MAX_LOG_LENGTH + 1] = {0} ;
	int pos = 0;
	int ret;

	time_t t;
	time(&t);
	struct tm local_tm;
	struct tm *tm = localtime_r( &t, &local_tm ) ;

	pos = snprintf(buf, MAX_LOG_LENGTH, "%04d%02d%02d-%02d:%02d:%02d", tm->tm_year + 1900,
			tm->tm_mon + 1, tm->tm_mday, tm->tm_hour, tm->tm_min, tm->tm_sec);

	pos += snprintf(buf + pos, MAX_LOG_LENGTH - pos, "--DUMP");

	ip = (ip != NULL) ? ip : "NULL";
	pos += snprintf(buf + pos, MAX_LOG_LENGTH - pos, "--%.15s:%d", ip, port);

	user_id = (user_id != NULL) ? user_id : "NULL";
	pos += snprintf(buf + pos, MAX_LOG_LENGTH - pos, "--%.16s--", user_id);

	const char *TAB = "0123456789abcdef";
	unsigned char uch;

	for (int i = 0; i < len; ++i) {
		uch = data[i];
		buf[pos++] = TAB[uch >> 4];
		buf[pos++] = TAB[uch & 0xf];
	}
	buf[pos] = '\0';

	//for(int i = 0; i < len; ++i){
	//	pos += sprintf( buf + pos,"%02x",(unsigned char)data[i]);
	//}
	// 写入十六进制数据
	private_log( buf , file, line, function , false ) ;
}

static int getfilesize( const char *filename )
{
	struct stat buf ;
	if ( lstat( filename , &buf ) != 0 ){
		// 如果文件因为无法访问，返回剩余空间为0，意思是再建空间
		return 0 ;
	}
	return buf.st_size ;
}

// 写入文件
void CCLog::private_log( const char *msg , const char *file, int line, const char *function , bool run )
{
	if ( msg == NULL ) {
		return ;
	}

	char buf[256] = {0} ;
	if ( file != NULL && line > 0 && function != NULL && ! run ) {
		sprintf( buf, ",%s,%s:%d\n" , function, file, line ) ;
	} else {
		sprintf( buf, "\n" ) ;
	}

	int nsize = strlen( msg ) ;
	int nbuff = strlen( buf ) ;

	// 是否为运行日志
	if ( run ) {
		// 如果为运行日志则直接打开文件直接写入数据
		int fd = open( _run_name.c_str() , O_CREAT|O_APPEND|O_RDWR , 0755 ) ;
		if ( fd != -1 ) {
			write( fd, msg, nsize ) ;
			write( fd, buf, nbuff ) ;
			close( fd ) ;
		}
	}

	_mutex.lock() ;

	// 检测写入的数据是否大于最大内存缓存
	if ( _LogBlock.offset + nsize + nbuff > DEFAULT_MAXLOGSIZE ){
		dumpfile() ;
	}

	// 将数据写入内存缓存中
	memcpy( _LogBlock.data + _LogBlock.offset, msg, nsize ) ;
	_LogBlock.offset = _LogBlock.offset + nsize ;

	memcpy( _LogBlock.data + _LogBlock.offset, buf, nbuff ) ;
	_LogBlock.offset = _LogBlock.offset + nbuff ;

	_LogBlock.size   = _LogBlock.size + nsize + nbuff ;

	_mutex.unlock() ;


}

bool CCLog::update_file()
{
	time_t t;
	time(&t);
	struct tm local_tm;
	struct tm *tm = localtime_r( &t, &local_tm ) ;

	char buf[128]={0};
	sprintf(buf, "%04d%02d%02d-%02d%02d%02d", tm->tm_year + 1900, tm->tm_mon
			+ 1, tm->tm_mday, tm->tm_hour, tm->tm_min, tm->tm_sec);

	// 对目录按照日期目录进行归类处理
	char szdir[256] = {0};
	sprintf( szdir, "%04d/%02d/%02d", tm->tm_year + 1900, tm->tm_mon + 1, tm->tm_mday ) ;

	string sfile = _file_name + "." ;
	sfile += buf ;
	size_t npos = _file_name.rfind( '/' ) ;
	if ( npos != string::npos ) {
		string sdir  = _file_name.substr( 0, npos+1 ) + szdir ;
		if( access( sdir.c_str(), 0 ) != 0 ) {
			mkdirs_log( (char*)sdir.c_str() ) ;
		}
		sfile = sdir + _file_name.substr( npos ) + ".";
		sfile += buf ;
	}

	string snew = sfile ;
	// 处理日志，将日志全部保留
	int pos = 0 ;
	while ( access( snew.c_str(), 0 ) == F_OK ) {
		snew  = sfile + "-" + ustodecstr_log(++pos) ;
	}
	rename( _file_name.c_str(), snew.c_str() ) ;
	// 重新打开一次文件操作
	openfile() ;
	/**
	unsigned short counter = _log_num;
	string new_file;
	string old_file;
	for(int i = counter; i > 0; i--)
	{
		new_file = _file_name + "." + ustodecstr_log(counter);
		old_file = _file_name +"." + ustodecstr_log(--counter);
		if(counter == 0)
			old_file = _file_name;
		rename(old_file.c_str(),new_file.c_str());
	}*/
	return true;
}

// 打文件操作
void CCLog::openfile( void )
{
	if ( _file_fd > 0 ) {
		close( _file_fd ) ;
	}
	// 打开文件FD处理日志
	_file_fd = open( _file_name.c_str() , O_CREAT|O_APPEND|O_RDWR , 0755 );
}

// 关闭文件FD
void CCLog::closefile( void )
{
	if ( _file_fd > 0 ) {
		close( _file_fd ) ;
	}
	_file_fd = -1 ;
}

void CCLog::set_log_file(const char *s)
{
	if(s == NULL)
		return;
	_file_name = s;
	size_t pos = _file_name.rfind( '/' ) ;
	if ( pos != string::npos ) {
		// 创建目录
		mkdirs_log( (char*)_file_name.substr( 0, pos ).c_str() ) ;
	}
	_run_name = _file_name + ".running" ;

	openfile() ;
}

/////////////////////////////////////////// CDirectoryFile //////////////////////////////////////////////
// 目录文件管理对象
class CDirectoryFile
{
public:
	CDirectoryFile(){}
	~CDirectoryFile(){}

	// 检测目录数据是否正常
	bool Check( const char *root, const char *name , int log_num )
	{
		map<string,string>  filemap ;
		// 取得文件个数据处理
		int count = GetLogFileList( root , name , filemap ) ;
		if( count <= log_num )
			return false ;

		int num = count - log_num ;
		// 遍历删除需要删除的文件
		map<string,string>::iterator it ;
		for ( it = filemap.begin(); it != filemap.end(); ++ it ) {
			unlink( (it->second).c_str() ) ;
			if ( --num <= 0 ) break ;
		}
		//filemap.clear() ;

		return true ;
	}

private:
	// 是否为目录
	bool isDirectory(const char *szDirPath)
	{
		struct stat stats;
		if (lstat (szDirPath, &stats) == 0 && S_ISDIR (stats.st_mode))
			return true;
		return false;
	}

	// 查找所有文件列表
	int GetLogFileList( const char* root_dir, const char *name, map<string,string> &filemap )
	{
		DIR* dir_handle = opendir( root_dir );
		if ( dir_handle == NULL )
			return 0;

		int   count = 0 ;
		char  buf[1024] = {0};
		struct dirent* entry = (struct dirent*)buf ;
		struct dirent* dir   = NULL ;

		while ( dir_handle ){
			int ret_code = readdir_r( dir_handle , entry , &dir );
			if ( ret_code != 0 || dir == NULL ){
				break ;
			}

			if ( strcmp( dir->d_name , "." ) == 0 || strcmp( dir->d_name , ".." ) == 0 ){
				continue ;
			}

			char szbuf[1024] = {0} ;
			sprintf( szbuf, "%s/%s", root_dir, dir->d_name ) ;
			// 如果是目录则直接递归遍历
			if ( isDirectory(szbuf) ) {
				count += GetLogFileList( szbuf, name, filemap ) ;
				continue ;
			}

			// 如果是本日志文件不添加处理
			if ( strcmp( dir->d_name ,name ) == 0 ) {
				continue ;
			}

			// 如果为历史日志文件添加处理队列中
			if ( strncmp( dir->d_name, name, strlen(name) ) != 0 ){
				continue ;
			}
			// printf( "add file: %s\n", szbuf ) ;
			filemap.insert( make_pair( dir->d_name, szbuf ) ) ;

			++ count ;
		}
		closedir( dir_handle ) ;

		return count ;
	}
};

// 将内存数据写入磁盘
void CCLog::writedisk( void )
{
	_mutex.lock() ;
	dumpfile() ;
	_mutex.unlock() ;
}

// 检测日志数据
void CCLog::checklogfile( void )
{
	writedisk() ;
	checkfile() ;
}

// 将内存日志写入文件
void CCLog::dumpfile( void )
{
	// 这里使用日志双缓存区操作，这样只需的切换一下缓存区就可以了
	if ( _LogBlock.offset == 0 ) {
		return ;
	}

	// 打开文件，如果该文件不能被打开，就返回错误
	if ( _file_fd <= 0 ) openfile() ;
	if ( _file_fd > 0 ) {
		int size = 0 , cnt = 0 ;
		// 如果写入失败重试两次处理
		while( size <= 0 && ++ cnt < 2 ) {
			// 写入消息
			size += write( _file_fd , _LogBlock.data , _LogBlock.offset ) ;
			if ( size <= 0 ) openfile() ;
			// 如果无法打开文件描述符就直接退出了
			if ( _file_fd <= 0 ) break ;
		}
	} else {
		printf( "%s\n" , _LogBlock.data ) ;
	}
	_LogBlock.offset = 0 ;

	// 如果记录大于最大值
	if ( _LogBlock.size > DEFAULT_MAXLOGSIZE ) {
		// 取得文件大小
		if ( getfilesize(_file_name.c_str()) > _log_size ) {
			// 重命名文件
			update_file() ;
		}
		_LogBlock.size = 0 ;
	}
}

// 检测日志个数是否超出限制
void CCLog::checkfile( void )
{
	// printf( "check log file\n" ) ;
	// 如果需要保留的文件数有限制则只保留这么长时间的文件数
	if ( _log_num <= 0 )
		return ;

	time_t now = time(NULL) ;
	// 如果没有到时间重新处理,每隔5分钟检测一次
	if ( now - _check_time < 300 ) {
		return ;
	}
	_check_time = now ;

	size_t pos = _file_name.rfind( '/' ) ;
	if ( pos == string::npos ) {
		return ;
	}

	string path = _file_name.substr( 0, pos ) ;
	string name = _file_name.substr( pos+1 )  ;

	// 目录文件管理对象
	CDirectoryFile dirfile ;
	dirfile.Check( path.c_str(), name.c_str() , _log_num ) ;
}

void debug_printf(const char *file, int line, const char *fmt, ...)
{
	va_list ap;

#ifdef  WIN32
	DWORD pid = GetCurrentThreadId();
#else
	//pthread_t pid = pthread_self();
#ifdef _UNIX
	pid_t pid = getpid() ;
#else
	pid_t pid =  (long)syscall(__NR_gettid);
#endif
#endif

	fprintf(stdout, "(%s:%d:PID %d:TID %d)\n", file, line, getpid(), pid);
	va_start(ap, fmt);
	vfprintf(stdout, fmt, ap);
	va_end(ap);
	fprintf(stdout, "\n");
	fflush(stdout);
}

void info_printf(const char *fmt, ...)
{
	va_list ap;

	va_start(ap, fmt);
	vfprintf(stdout, fmt, ap);
	va_end(ap);

	fprintf(stdout, "\n");
	fflush(stdout);
}
