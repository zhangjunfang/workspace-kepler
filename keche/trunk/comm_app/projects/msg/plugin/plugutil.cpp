/*
 * plugutil.cpp
 *
 *  Created on: 2012-5-30
 *      Author: humingqing
 *
 */

#include "plugutil.h"

// 解析数据
bool CPlugUtil::parse( const char *param )
{
	return parsevalue( param, _kv ) ;
}

// 取得整形的数据
bool CPlugUtil::getinteger( const char *key, int &value )
{
	string sval ;
	if ( ! getstring( key, sval ) )
		return false ;
	if ( sval.empty() )
		return false ;

	value = atoi( sval.c_str() ) ;
	return true ;
}

// 取得字符串形的数据
bool CPlugUtil::getstring( const char *key, string &value )
{
	MapString::iterator it = _kv.find( key ) ;
	if ( it == _kv.end() )
		return false ;
	value = it->second ;
	return true ;
}

bool CPlugUtil::split2map( const std::string &s , MapString &val )
{
	std::vector<std::string>  vec ;
	// 处理所有逗号分割处理
	if ( ! splitvector( s , vec, "," , 0 ) ) {
		return false ;
	}

	string temp  ;
	size_t pos = 0 , end = 0 ;
	// 解析参数
	for ( pos = 0 ; pos < vec.size(); ++ pos ) {
		temp = vec[pos] ;
		end  = temp.find( ":" ) ;
		if ( end == string::npos ) {
			continue ;
		}
		val.insert( pair<string,string>( temp.substr(0,end), temp.substr( end+1 ) ) ) ;
	}
	// 解析出监控平台参数部分
	return ( ! val.empty() ) ;
}

// 解析监控平台的参数
bool CPlugUtil::parsevalue( const std::string &param, MapString &val )
{
	// {TYPE:0,104:京A10104,201:701116,202:1,15:0,26:5,1:69782082,2:23947540,3:5,4:20110516/153637,5:6,21:0}
	size_t pos = param.find("{") ;
	if ( pos == string::npos ) {
		return false ;
	}

	size_t end = param.find("}", pos ) ;
	if ( end == string::npos || end < pos + 1 ) {
		return false ;
	}
	// 解析出监控平台参数部分
	return split2map( param.substr( pos+1, end-pos-1 ), val ) ;
}



