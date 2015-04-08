/*
 * util.h
 *
 *  Created on: 2012-6-26
 *      Author: humingqing
 *  将BCD时间转成UTC时间，以及将UTC时间转成BCD时间
 */

#ifndef __UTIL_H__
#define __UTIL_H__

#include <string>
#include <stdlib.h>
#include <stdio.h>
#include <BaseTools.h>
using namespace std ;

// 安全处理字符串
static string safestring( const char *ptr, int size )
{
	string s( ptr, size ) ;
	return s ;
}

// 将字符串时间转为BCD时间
static string bcd2utc( char bcd[6] )
{
	string stime = BCDtostr( bcd ) ;
	if ( stime.empty() )
		return 0 ;

	int nyear = 0 , nmon = 0, nday = 0, nhour = 0, nmin = 0, nsec = 0 ;
	sscanf( stime.c_str(), "%02d%02d%02d%02d%02d%02d", &nyear, &nmon, &nday, &nhour, &nmin , &nsec ) ;

	struct tm tm;
	tm.tm_year = nyear + 100 ;
	tm.tm_mon  = nmon - 1 ;
	tm.tm_mday = nday ;
	tm.tm_hour = nhour ;
	tm.tm_min  = nmin ;
	tm.tm_sec  = nsec ;

	time_t ntime = mktime(&tm);

	char buf[128] = {0};
	sprintf( buf, "%llu", (long long)ntime ) ;
	return buf ;
}

// 将字符串时间转为BCD时间
static void utc2bcd( const string &time, char bcd[6] )
{
	time_t ntime  = atoll( time.c_str() ) ;
	struct tm local_tm;
	struct tm *tm = localtime_r( &ntime, &local_tm ) ;

	char buf[128] = {0} ;
	sprintf( buf, "%02d%02d%02d%02d%02d%02d",
			( tm->tm_year + 1900 ) % 100 , tm->tm_mon + 1, tm->tm_mday, tm->tm_hour, tm->tm_min, tm->tm_sec) ;

	// 转换为BCD时间
	strtoBCD( buf, bcd ) ;
}
#endif /* UTIL_H_ */
