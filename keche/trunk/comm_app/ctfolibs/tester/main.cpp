/*
 * main.cpp
 *
 *  Created on: 2012-7-10
 *      Author: think
 */


#include <stdio.h>
#include <list>
#include <set>
using namespace std ;
#include "groupuser.h"
#include <OnlineUser.h>
#include "tester.h"
#include <Session.h>
#include <workthread.h>

void test_queue()
{
#ifdef _TESTTHREAD
	CTestThread test ;
#else
	CQueueTest  test ;
#endif
	int64 begin = gettime64() ;
	for ( int i = 0; i < max_data_pool_num; ++ i ) {
		if ( i == max_data_pool_num / 2 ) {
			test.start() ;
		}
		test.add( i ) ;
	}
	int64 end = gettime64() ;
	printf( "push data span time: %f\n", (double)(end - begin) / (double)TV_USEC) ;

	while( test.count() < max_data_pool_num ) {
		sleep(1) ;
	}
	test.stop() ;

	printf( "get result count: %d\n", test.count() ) ;
}

void test_group()
{
	char *sz[] = { "SAVE", "WEB", "PAS" } ;
	int i = 0 ;
	char szid[128] = {0};

	CGroupUserMgr group ;

	for ( i = 0; i < 1000; ++ i ) {
		sprintf( szid, "%d", i + 1 ) ;

		User new_user;
		new_user._fd               = new socket_t ;
		new_user._user_id          = szid ;
		if ( i % 5 == 0 ) {
			new_user._user_name	   = "2" ;
		}
		new_user._ip               = "127.0.0.1";
		new_user._login_time       = time(0);
		new_user._user_type        = sz[i%3];
		new_user._user_state       = User::ON_LINE;
		new_user._last_active_time = time(0);

		group.AddUser( szid, new_user ) ;
	}

	vector<User> vec ;
	for ( i = 0; i < 1000000 ; ++ i ) {
		vec.clear() ;
		int count = group.GetSendGroup( vec, i, ( i % 2 == 0 ) ? ( SAVE_SEND_CMD | UWEB_SEND_CMD ) : ( UPAS_SEND_CMD ) ) ;
		for ( int k = 0; k < vec.size(); ++ k ) {
			User &user = vec[k] ;
		}
	}
}

void test_string()
{
	const char *str ="TYPE:0,RET:0,1:61816921,2:22243260,20:134217728,21:0,210:9288,213:27387,216:222,24:0,3:722,4:20120429/233607,5:313,500:2,503:155,504:82,520:1,6:2597,7:697,8:3,9:58141";

	map<string,string> kvmap ;

	int len = strlen(str) ;

	list< StrInfo > ldata;
	for ( int i = 0; i < 100000; ++ i ) {

		ConvertUtcTime( "20120429/233607" ) ;
		//20120429/233607
		// split2kvmap( str, kvmap, ",", ":" ) ;
		/**
		ldata.clear() ;
		split2list( str, len , ldata, "," );
		split2map( ldata, kvmap, ":" );
		*/
	}
}

class MyNotify: public ISessionNotify
{
public:
	// 添加数据通知
	void NotifyChange( const char *key, const char *val , const int op )
	{
		printf( "notify key %s value %s op %d\n", key, val, op ) ;
	}
};

void test_session()
{
	MyNotify notify ;
	CSessionMgr mgr(true) ;
	mgr.SetNotify( &notify ) ;

	mgr.AddSession( "test", "test session" ) ;
	mgr.AddSession( "test1", "test1 session" ) ;

	while( mgr.GetSize() > 0 ) {
		mgr.CheckTimeOut( 20 ) ;
		sleep(1) ;
	}
}

class CTestWork: public share::Runnable
{
public:
	CTestWork(){}
	~CTestWork(){}

	void run( void *param )
	{
		printf( "running time %ld\n", time(NULL) ) ;
	}
};

#define MAX_COUNT   1000*1000*10

void test_list()
{
	list<int> lst ;
	for ( int i = 0; i < MAX_COUNT; ++ i ) {
		lst.push_back( i ) ;
	}

	int begin = gettime64() ;
	list<int>::iterator it ;
	for ( it = lst.begin(); it != lst.end(); ++ it ) {

	}
	int end = gettime64() ;

	printf( "list span time %f",  (double)(end - begin) / (double)TV_USEC ) ;
}

void test_set()
{
	set<int> st ;
	for ( int i = 0; i < MAX_COUNT; ++ i ) {
		st.insert( set<int>::value_type(i) ) ;
	}

	int begin = gettime64() ;
	set<int>::iterator it ;
	for ( it = st.begin(); it != st.end(); ++ it ) {
	}
	int end = gettime64() ;

	printf( "set span time %f", (double)(end - begin) / (double)TV_USEC ) ;
}

#include <qstring.h>

int main( int argc, char ** argv )
{
	/**
	int64 begin = gettime64() ;

	//test_session() ;

	// test_queue() ;

	// test_set() ;

	// test_list() ;
	static const char *XML_HEAD_FMT="<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
		"<zwy><head><accessCode>%s</accessCode><identify>%s</identify><businessType>%s</businessType></head>"
		"<body><reqMsg><msgSerNo>%u</msgSerNo>" ;
	static const char *XML_FOOT_FMT="</reqMsg></body></zwy>" ;

	char szbuf[1024] = {0} ;
	sprintf( szbuf, XML_HEAD_FMT, "ZWY" , "11110" , "test", 1 ) ;

	CQString str ;
	str.AppendBuffer( szbuf ) ;
	str.AppendBuffer( XML_FOOT_FMT ) ;

	str.Replace( "&", "&amp;" ) ;
	str.Replace( "<", "&lt;"  ) ;
	str.Replace (">", "&gt;"  ) ;
	str.Replace( "\"", "&quot;" ) ;
	str.Replace( " ", "&nbsp;" ) ;

	//const char *s[] = {"&amp;","&lt;", "&gt;", "&quot;", "&nbsp;"} ;
	const char *c[] = {"&", "<", ">", "\"", " "} ;
	const char *s[] = {"", "", "", "", "" } ;
	str.NReplace( 5, c, s ) ;

	printf( "xml:%s\n", str.GetBuffer() ) ;

	int64 end = gettime64() ;
	printf( "span time: %f \n", (double)(end - begin) / (double)TV_USEC ) ;
	*/

	CTestWork test;
	CWorkThread work;
	work.Init( 2 ) ;

	work.Register( &test, NULL, 5 ) ;

	while( true ) {
		sleep(1) ;
	}

	work.Stop() ;

	return 0 ;
}
