/*
 * pconvert.h
 *
 *  Created on: 2012-3-2
 *      Author: think
 *
 *  实现内部协议转成江苏监管平台协议
 */

#ifndef __PCONVERT_H__
#define __PCONVERT_H__

#include <qstring.h>
#include "pccsession.h"
#include <string>
#include <vector>
#include <map>

using namespace std ;

static float my_atof( const char *p )
{
	if ( p == NULL )
		return 0 ;
	return atof( p ) ;
}

static int my_atoi( const char *p )
{
	if ( p == NULL )
		return 0 ;
	return atoi( p ) ;
}

class ISystemEnv ;
class PConvert
{
	typedef map<string,string> MapString ;
public:
	PConvert() ;
	~PConvert() ;

	// 初始化环境对象指针
	bool initenv( ISystemEnv *pEnv ) ;
	// 转换U_REPT指令的数据
	void convert_urept( _stCarInfo &info, const string &val, CQString &buf )  ;
	// 将苏汽协议转换成内部协议
	static bool buildintergps( std::vector<std::string> &vec , const char *macid, CQString &buf , CQString &msg ) ;

private:
	// 处理监控的参数
	bool parse_jkpt_value( const std::string &param, MapString &val ) ;
	// 取得对应MAP字符值
	bool get_map_string( MapString &map, const std::string &key , std::string &val ) ;
	// 取得对应MAP中整形值
	bool get_map_integer( MapString &map, const std::string &key , int &val ) ;
	// 取得头数据
	bool get_map_header( const std::string &param, MapString &val, int &ntype ) ;
	// 拆分数据到MAP
	bool split2map( const std::string &s , MapString &val ) ;
	// 转换上报的GPS的数据
	bool convertgps( _stCarInfo &info, MapString &mp, CQString &buf ) ;

private:
	// 系统环境对象
	ISystemEnv * _pEnv ;
};


#endif /* PCONVERT_H_ */
