/*
 * whitelist.cpp
 *
 *  Created on: 2012-5-18
 *      Author: humingqing
 *  白名单
 */

#include "whitelist.h"
#include <tools.h>
#include <vector>
#include <qstring.h>

using namespace std ;

CWhiteList::CWhiteList()
{
}

CWhiteList::~CWhiteList()
{
	Clear() ;
}

// 加载文件
bool CWhiteList::LoadList( const char *filename )
{
	int  len  = 0 ;
	char *ptr = ReadFile( filename, len ) ;
	if ( ptr == NULL )
		return false ;

	vector<string>  vec ;
	if ( ! splitvector( ptr , vec, "|" , 0 )  ){
		FreeBuffer( ptr ) ;
		return false ;
	}

	// 处理数据
	for ( int i = 0; i < (int)vec.size(); ++ i ) {
		string &tmp = vec[i] ;
		size_t pos = tmp.find( ":" ) ;
		if ( pos == string::npos && pos > 0 ){
			continue ;
		}
		vector<string> vk ;
		if ( ! splitvector( tmp.substr(pos+1) , vk, "," , 0 ) ){
			continue ;
		}

		int id = atoi( tmp.substr(0,pos).c_str() ) ;
		if ( id < 0 ) {
			continue ;
		}

		_mutex.lock() ;
		_WhiteIds *p = new _WhiteIds ;
		for ( int k = 0; k < (int)vk.size(); ++ k ) {
			CQString sz( vk[k].c_str() ) ;
			p->_lst.insert( SetString::value_type( sz.Trim() ) ) ;
		}
		_macList.insert( make_pair(id, p ) ) ;
		_mutex.unlock() ;
	}
	FreeBuffer( ptr ) ;

	return true ;
}

// 是否在白名单内
bool CWhiteList::OnWhite( int id, const char *key )
{
	share::Guard guard( _mutex ) ;

	if ( _macList.empty() )
		return true ;

	CMacList::iterator it = _macList.find( id ) ;
	if ( it == _macList.end() )
		return true ;

	_WhiteIds *p = it->second ;
	if ( p->_lst.find(key) != p->_lst.end() )
		return true ;

	return false ;
}

void CWhiteList::Clear()
{
	share::Guard guard( _mutex ) ;
	if ( _macList.empty() )
		return ;

	CMacList::iterator it ;
	for ( it = _macList.begin(); it != _macList.end(); ++ it )
	{
		delete it->second ;
	}
	_macList.clear() ;
}


