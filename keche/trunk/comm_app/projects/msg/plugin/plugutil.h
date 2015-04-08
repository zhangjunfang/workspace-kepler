/*
 * plugutil.h
 *
 *  Created on: 2012-5-30
 *      Author: humingqing
 *  Memo: 一些常用的字符拆分的函数
 */

#ifndef PLUGUTIL_H_
#define PLUGUTIL_H_

#include <map>
#include <string>
#include <tools.h>

class CPlugUtil
{
	typedef std::map<std::string,std::string>  MapString ;
public:
	CPlugUtil(){}
	~CPlugUtil(){}

	// 解析数据
	bool parse( const char *param ) ;
	// 取得整形的数据
	bool getinteger( const char *key, int &value ) ;
	// 取得字符串形的数据
	bool getstring( const char *key, string &value ) ;

private:
	// 解析监控平台的参数
	bool parsevalue( const std::string &param, MapString &val ) ;
	// 拆分KEY-VALUE的数据
	bool split2map( const std::string &s , MapString &val ) ;

private:
	// 取得对应值
	MapString  _kv ;
} ;


#endif /* PLUGUTIL_H_ */
