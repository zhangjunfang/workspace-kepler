/*
 * main.cpp
 *
 *  Created on: 2012-3-6
 *      Author: think
 */

#include <inetfile.h>
#include "asynfile.h"
#include "synfile.h"

// 取得文件读取对萌
INetFile * NetFileMgr::getfileobj( NetFileMgr::NET_MODE mode )
{
	INetFile *p = NULL ;
	switch( mode ) {
	case SYN_MODE:
		p = new CSynFile ;
		break ;
	case ASYN_MODE:
		p = new CAsynFile ;
		break ;
	}
	return p ;
}

// 释放文件对象
void NetFileMgr::release( INetFile *p )
{
	if ( p == NULL )
		return ;
	delete p ;
}

#ifdef __DEBUG_TEST__
#include <stdio.h>
#include <tools.h>
#include <comlog.h>
#include <time.h>
#include <sys/time.h>
typedef long long int64 ;

using namespace std ;

#define TV_USEC  1000000

static int64 gettime64( void )
{
	struct timeval val ;
	gettimeofday( &val, NULL ) ;
	return val.tv_sec * TV_USEC +  val.tv_usec ;
}

int main( int argc, char ** argv )
{
	SETLOGLEVEL(0) ;

	INetFile *file = NULL ;
#ifdef __SYN_MODE__
	file = NetFileMgr::getfileobj( NetFileMgr::SYN_MODE ) ;
#else
	file = NetFileMgr::getfileobj( NetFileMgr::ASYN_MODE ) ;
#endif
	if ( file == NULL ) {
		printf( "get net file instance failed\n" ) ;
		return -1 ;
	}

	int ret = file->open( "192.168.100.112", 8809, "admin", "123456" ) ;
	if ( ret != FILE_RET_SUCCESS ) {
		printf( "open filemgr failed\n" ) ;
		return -1 ;
	}

	int len   = 0 ;
	char *ptr = ReadFile( "./test.jpeg", len ) ;

	int count = 0 ;

	int64 now = gettime64() ;
#ifdef __TEST__
	for ( int i = 0; i < 10000; ++ i ) {
		char sz[256] = {0} ;
		sprintf( sz, "test%d.jpg", i ) ;

		ret = file->write( sz , ptr , len ) ;
		if ( ret != FILE_RET_SUCCESS ){
			++ count ;
		}
	}
#else
	ret = file->write( "2012/09/17/test0.jpg" , ptr , len ) ;
#endif
	int64 end = gettime64() ;
	FreeBuffer( ptr ) ;

	printf( "write file span time: %f, failed: %d\n", (float)(end - now)/(float)TV_USEC , count ) ;

#ifdef __TEST__
	now = gettime64() ;
	for ( int i = 0; i < 10000; ++ i ) {
		char sz[256] = {0} ;
		sprintf( sz, "test%d.jpg", i ) ;

		DataBuffer buf ;
		ret = file->read(  sz , buf ) ;
		if ( ret != FILE_RET_SUCCESS ) {
			++ count ;
		}
	}
	end = gettime64() ;

	printf( "read file span time: %f, failed: %d\n", (float)(end - now)/(float)TV_USEC, count ) ;
#else
	now = gettime64() ;
	DataBuffer buf ;
	file->read(  "2012/09/17/test0.jpg" , buf ) ;
	end = gettime64() ;

	// WriteFile( "./my.jpeg", buf.getBuffer(), buf.getLength() ) ;
	printf( "read file length %d, span time %f\n", buf.getLength(), (float)(end - now)/(float)TV_USEC ) ;
#endif
	file->close() ;

	NetFileMgr::release( file ) ;

	return 0 ;
}
#endif
