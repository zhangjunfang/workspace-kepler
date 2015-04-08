/*
 * waitobjmgr.h
 *
 *  Created on: 2012-9-18
 *      Author: humingqing
 */

#ifndef __WAITOBJMGR_H__
#define __WAITOBJMGR_H__

#include <map>
#include <Monitor.h>
#include <databuffer.h>

struct _waitobj
{
	unsigned char  _result ;
	DataBuffer 	   _inbuf  ;
	DataBuffer 	  *_outbuf ;
	share::Monitor _monitor ;
};

class CWaitObjMgr
{
	class CSequeue
	{
	public:
		CSequeue():_seq(0){}
		~CSequeue(){}
		// 取得序号
		unsigned int gensequeue( void ) {
			share::Guard guard( _mutex ) ;
			return ++ _seq ;
		}

	private:
		share::Mutex  _mutex ;
		unsigned int  _seq ;
	};
	typedef std::map<unsigned int, _waitobj*> CMapObj ;
public:
	CWaitObjMgr() ;
	~CWaitObjMgr() ;

	// 开辟序号对象
	_waitobj * AllocObj( unsigned int seq , DataBuffer *out = NULL ) ;
	// 更新对象数据
	void ChangeObj( unsigned int seq, unsigned char result, void *data, int len ) ;
	// 移除对象
	void RemoveObj( unsigned int seq ) ;
	// 返回取得序号对象
	unsigned int GenSequeue( void ) { return _genseq.gensequeue(); }
	// 通知所有对象处理
	void NotfiyAll( void ) ;

private:
	// 清理所有对象
	void Clear( void ) ;

private:
	// 序号生成对象
	CSequeue  	  _genseq ;
	// 数据对象
	CMapObj   	  _mapobj ;
	// 对象操作锁
	share::Mutex  _mutex ;
};


#endif /* WAITOBJMGR_H_ */
