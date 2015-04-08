#include <stdio.h>
#include "redispool.h"

void test_keys( RedisObj *p )
{
	std::vector<std::string> vec;
	int count = p->GetKeys( "test" , vec ) ;
	printf( "get keys %s count %d", "test", count ) ;

	for ( int i = 0; i < count; ++ i ) {
		printf( "%d) %s\n", i+1, vec[i].c_str() ) ;
	}
}

void test_set( RedisObj *p )
{
	std::vector<std::string> vec ;
	p->SAdd( "test", "test" ) ;

	int count = p->SMembers( "test", vec ) ;
	for ( int i = 0; i < count; ++ i ) {
		printf( "%d) %s\n", i+1, vec[i].c_str() ) ;
	}

	std::string val ;
	p->SPop( "test", val ) ;
	printf( "get key test val %s\n", val.c_str() ) ;
}

void test_hash( RedisObj *p )
{
	p->HSet( "test.hash", "test1", "test1" ) ;
	p->HSet( "test.hash", "test2", "test2" ) ;
	p->HSet( "test.hash", "test3", "test3" ) ;
	p->HSet( "test.hash", "test4", "test4" ) ;

	std::vector<std::string> vec ;
	int count = p->HKeys( "test.hash", vec ) ;
	printf( "get test.hash keys count: %d", count ) ;

	std::string val ;
	for ( int i = 0; i < count; ++ i ) {
		if ( p->HGet( "test.hash", vec[i].c_str() , val ) ) {
			printf( "keys: %s, value: %s\n", vec[i].c_str(), val.c_str() ) ;
		}
	}
}

void test_update( RedisObj *p , const char *fix )
{
	char szkey[128] = {0} ;
	char szval[1024] = {0};
	time_t begin = time(NULL) ;
	for ( int i = 0; i < 20000; ++ i ) {
		sprintf( szkey, "%s%07d", fix, i ) ;
		sprintf( szval, "corpid:%d,color:2,vechile:¾©A%06d,termid:A%06d,oem:4C54,auth:%s,vid:%s,flag:0", i % 100, i, i, szkey, szkey ) ;
		p->HSet( "lbs.phone", szkey, szval ) ;
	}

	time_t end = time(NULL) ;

	printf( "set 20000 count span time %d\n", end - begin ) ;
}

int main( int argc, char ** argv )
{
	RedisObj *p = new RedisObj;
	if ( ! p->InitObj( "192.168.100.110", 6379 , "192.168.100.110", 6380 ) ) {
		delete p ;
		printf( "init obj failed " ) ;
		return -1 ;
	}

	test_update( p , argv[1] ) ;

	delete p ;

    return 0;
}
