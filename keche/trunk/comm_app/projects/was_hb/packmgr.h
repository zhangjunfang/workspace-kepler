/**
 * Author: humingqing
 * Date:   2011-10-14
 * Memo:   将分包的数据包合成一个数据包处理，这里设计为一个锁处理，可以会有点阻塞情况
 */
#ifndef __PACKMGR_H__
#define __PACKMGR_H__

#include <time.h>
#include <map>
#include <string>
#include <vector>
#include <databuffer.h>
#include <Mutex.h>
#include <TQueue.h>
using namespace std ;

#define LONG_PACK_CHECK    30
#define LONG_PACK_TIMEOUT  180  // 3分钟内如果没有完成上传就丢掉

class CPackMgr
{
	class CMemFile
	{
	public:
		CMemFile( const char *id ) ;
		~CMemFile() ;

		// 添加内存数据
		bool    AddBuffer( DataBuffer &pack, const int index, const int count, const char *buf, int len ) ;
		// 取得最后一次使用时间
		time_t  GetLastTime( void ) { return _last ; }
		// 取得对应的内存ID号
		const char *GetId( void ) { return _id.c_str(); }

	private:
		// 当前存放条数
		unsigned int 			_cur ;
		// 内存文件对象
		vector<DataBuffer *>    _vec ;
		// 最后一次时间
		time_t 					_last ;
		// 索引的ID号
		std::string 			_id ;

	public:
		// 内存文件对象链表指针
		CMemFile *				_next ;
		CMemFile *				_pre ;
	};

	typedef map<string,CMemFile*> 	CMapFile ;
public:
	CPackMgr() ;
	~CPackMgr() ;
	// 添加到数据包对象
	bool AddPack( DataBuffer &pack, const char *carid, const int msgid, const int index, const int count, const int seq, const char *buf, int len ) ;
	// 处理超时的数据包
	void CheckTimeOut( unsigned int timeout ) ;

private:
	// 移除数据对象
	void RemovePack( const string &key ) ;

private:
	// 数据资源锁
	share::Mutex 	 _mutex ;
	// 存放内存数据
	CMapFile 	 	 _mapPack ;
	// 最后一次检测时间
	time_t		 	 _lastcheck ;
	// 数据队列
	TQueue<CMemFile> _queue ;
};

#endif
