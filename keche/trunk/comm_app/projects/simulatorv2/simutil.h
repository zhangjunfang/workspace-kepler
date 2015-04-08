#ifndef __SIMUTIL_H__
#define __SIMUTIL_H__

#include <tools.h>
#include <vector>
using namespace std ;

#define LOG_PATH 			"/var/lbs/log/ws"
#define CONF_PATH   		"/usr/local/lbs/conf/ws"
#define IN_LOG  			"simulatorv4.1.log"
#define CONFIG  			"simulatorv4.1.conf"
#define USER_INFO_FILE 		"simulatorv4.1_user.conf"
#define MTRANS_ENV			"MTRANS_PRJ_HOME"
#define ABS_LOG_PATH		"/lbs/log/ws"
#define ABS_CONF_PATH		"/lbs/conf/ws"

struct Point
{
	unsigned int lat ;
	unsigned int lon ;
};

static bool LoadGpsData( const char *szName , vector<Point> &vec )
{
	int   len = 0 ;
	char *buf = ReadFile( szName , len ) ;
	if ( buf == NULL || len == 0 ){
		printf( "read file %s failed\n", szName ) ;
		return false ;
	}

	int  j = 0 ;
	char tmp[1024] = {0} ;
	// 处理字符串的长度
	for( int i = 0; i < len; ++ i ) {
		if ( buf[i] == ' ' ) continue ;
		if ( buf[i] == '\r' || buf[i] == '\n' ) {
			if ( j < 2 ) {
				j = 0 ;
				continue ;
			}
			tmp[j] = 0 ;

			Point pt ;
			sscanf( tmp, "%d,%d",&pt.lon , &pt.lat ) ;
			pt.lon = pt.lon * 10 / 6 ;
			pt.lat = pt.lat * 10 / 6 ;
			vec.push_back( pt ) ;

			j = 0 ;
			continue ;
		}
		tmp[j++] = buf[i] ;
	}

	printf( "load gps data size %d\n", (int)vec.size() ) ;

	delete [] buf ;

	return true ;
}

#endif
