#include "iplist.h"
#include <comlog.h>

CIpList::CIpList()
{
	_index = 0 ;
	_size  = 0 ;
}

CIpList::~CIpList()
{

}

// 加载IP的黑名单
bool CIpList::LoadIps( const char *filename )
{
	if ( filename == NULL )
		return false ;

	char buf[1024] = {0};
	FILE *fp = NULL;
	fp = fopen( filename, "r" );
	if (fp == NULL) {
		// OUT_ERROR( NULL, 0,"iplist", "Load ip list file %s failed", filename ) ;
		return false;
	}

	int index = 0 ;
	_mutex.lock() ;
	index = ( _index + 1 ) % BACK_IPNUM ;
	_mutex.unlock() ;

	_ips[index].clear() ;

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

		IpInfo info ;
		strcpy( info._szip, temp ) ;
		info._len = strlen(temp) ;

		if ( info._len > 0 ) {
			// 添加到Set的集合中
			_ips[index].push_back( info ) ;
			++ count ;
		}
	}
	fclose(fp);
	fp = NULL;

	// 加载完成数据切换
	_mutex.lock() ;
	_index = index ;
	_size  = count ;
	_mutex.unlock() ;

	return true ;
}

// 检测IP是否在黑名单中
bool CIpList::Check( const char *ip )
{
	_mutex.lock() ;
	if ( _size == 0 || ip == NULL ){
		_mutex.unlock() ;
		return false ;
	}

	IpList::iterator it ;
	// 检测是否在IP禁用列表中
	for ( it = _ips[_index].begin(); it != _ips[_index].end(); ++ it ) {
		IpInfo &info = *it ;
		if( strncmp( ip, info._szip, info._len ) == 0 ) {
			_mutex.unlock() ;
			return true ;
		}
	}

	_mutex.unlock() ;

	return false ;
}
