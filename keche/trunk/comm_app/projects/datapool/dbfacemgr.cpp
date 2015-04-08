/*
 * dbfacemgr.cpp
 *
 *  Created on: 2012-5-26
 *      Author: humingqing
 */

#include "dbfacemgr.h"
#include <stdio.h>
#include <murhash.h>
#include <map>
#include <string>
#include "mongodb.h"
#include "oracledb.h"

using namespace std;

// 取得字符串的ID
unsigned int CDBFaceMgr::GetHash( const char *s )
{
	return mur_mur_hash2( s, strlen(s) , 0x0000ffff ) ;
}

// 拆分字符串
static void splitconn( const char *s, map<string,string> &mp )
{
	char *sz = strdup(s);
	char *tmp,*q = sz;
	char *p = (char *)strstr( q, ";" ) ;
	// 拆分字符串
	while ( p != NULL ){
		*p = 0 ;
		tmp = strstr( q, "=" ) ;
		if ( tmp != NULL ){
			*tmp = 0 ;
			mp.insert( make_pair( q, tmp+1 ) ) ;
		}
		q = p + 1 ;
		p = strstr( q, ";" ) ;
	}
	// 处理最后一个字符
	if ( q != NULL ) {
		tmp = strstr( q, "=" ) ;
		if ( tmp != NULL ){
			*tmp = 0 ;
			mp.insert( make_pair( q, tmp+1 ) ) ;
		}
	}
	free( sz ) ;
}

// 取得参数
static bool getstring( const char *key, map<string,string> &mp , string &val )
{
	map<string,string>::iterator it = mp.find( key ) ;
	if ( it == mp.end() )
		return false ;
	val = it->second ;
	return true ;
}

// 取得整形数据
static bool getinteger( const char *key, map<string,string> &mp, int &val )
{
	string sval ;
	if ( ! getstring( key, mp, sval ) )
		return false ;
	if ( sval.empty() )
		return false ;
	val = atoi( sval.c_str() ) ;
	return true ;
}

// orcale,mongo,mysql,db2
// 取得数据库对象 type=oracle;ip=;port=;user=;pwd=;db=;
IDBFace * CDBFaceMgr::GetDBFace( const char *s , unsigned int key )
{
	map<string,string> mp ;
	splitconn( s, mp ) ;
	if ( mp.empty() ) return NULL ;

	string type ;
	if ( ! getstring( "type", mp, type ) )
		return NULL ;

	string ip ;
	if ( ! getstring( "ip", mp , ip ) )
		return NULL ;

	int port ;
	if ( ! getinteger( "port", mp, port ) )
		return NULL ;

	IDBFace *pface = NULL ;
	if ( type == "oracle" ) {
		// 创建Oracle的连接对象
		string user , pwd , db ;
		if ( ! getstring( "user", mp, user ) )
			return NULL ;
		if ( ! getstring( "pwd" , mp ,pwd ) )
			return NULL ;
		if ( ! getstring( "db", mp ,db ) )
			return NULL;

		COracleDB *oracle = new COracleDB( key ) ;
		if ( ! oracle->Init( ip.c_str(), port, user.c_str(), pwd.c_str(), db.c_str() ) ) {
			delete oracle ;
			return NULL ;
		}
		oracle->AddRef() ;
		pface = oracle ;

	} else if ( type == "mongo" ) {
		string user , pwd , db ;
	    if ( ! getstring( "user", mp, user ) )
			 return NULL ;
		if ( ! getstring( "pwd" , mp ,pwd ) )
			 return NULL ;
		if ( ! getstring( "db", mp ,db ) )
			 return NULL ;

		CMongoDB *mongo = new CMongoDB( key ) ;
		if (! mongo->Init( ip.c_str(), port, user.c_str(), pwd.c_str(), db.c_str() ) ) {
			 delete mongo;
			 return NULL;
		}
		mongo->AddRef() ;
	    pface = mongo;

	} else if ( type == "mysql" ) {
		// ToDo:
	}

	return pface ;
}

