/*
 * icontext.h
 *
 *  Created on: 2012-4-28
 *      Author: humingqing
 *  启动和停止
 */

#ifndef __ICONTEXT_H__
#define __ICONTEXT_H__

class IContext
{
public:
	// 取得上下文指口
	virtual ~IContext(){}
	// 初始化系统
	virtual bool Init( const char *file , const char *logpath , const char *userfile  , const char *logname ) = 0 ;
	// 开始系统
	virtual bool Start( void ) = 0 ;
	// 停止系统
	virtual void Stop( void ) = 0 ;
	// 取得整形值
	virtual bool GetInteger( const char *key , int &value ) = 0 ;
	// 取得字符串值
	virtual bool GetString( const char *key , char *value ) = 0 ;
	// 取得用户数据路径
	virtual const char * GetUserPath( void ) = 0 ;
};


#endif /* ICONTEXT_H_ */
