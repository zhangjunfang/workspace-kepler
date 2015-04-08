/*
 * packfactory.h
 *
 *  Created on: 2012-6-1
 *      Author: humingqing
 */

#ifndef __PACKFACTORY_H__
#define __PACKFACTORY_H__

#include "msgpack.h"

// 通用解析数据模板类
template <typename T>
IPacket * UnPacket( CPacker &pack, const char *name )
{
	T *req = new T;
	if ( ! req->UnPack( &pack ) ) {
		delete req ;
		return NULL ;
	}
	req->AddRef() ;
	return req ;
}

// 解析协议对象类
class IUnPackMgr
{
public:
	virtual ~IUnPackMgr() {} ;
	// 实现数据解包接口方法
	virtual IPacket * UnPack( unsigned short msgtype, CPacker &pack ) = 0 ;
};

// 序列产生器
class CSequeueGen;
class CPackFactory
{
public:
	// 初始化工厂类时需要处理对应的实现解包
	CPackFactory( IUnPackMgr *packmgr ) ;
	// 析构对象处理
	~CPackFactory() ;
	// 解包数据
	IPacket * UnPack( const char *data, int len ) ;
	// 打包数据
	void Pack( IPacket *packet , CPacker &pack ) ;
	// 取得序号
	unsigned int GetSequeue( void ) ;

private:
	// 数据解包对象
	IUnPackMgr 	*_packmgr ;
	// 序号产生对象
	CSequeueGen *_seqgen ;
};


#endif /* PACKFACTORY_H_ */
