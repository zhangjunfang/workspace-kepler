/**
 * Author: humingqing
 * Date:   2011-10-14
 */
#include "packmgr.h"
#include <comlog.h>
#include <GBProtoParse.h>

CPackMgr::CPackMgr()
{
	_lastcheck = time(NULL) ;
}

CPackMgr::~CPackMgr()
{
	share::Guard g( _mutex ) ;
	{
		_mapPack.clear() ;
	}
}

// 添加到数据包对象
bool CPackMgr::AddPack( DataBuffer &pack, const char *carid, const int msgid,
		const int index, const int count, const int seq, const char *buf, int len )
{
	share::Guard g( _mutex ) ;
	{
		if ( count <= 0 || index <= 0 ) {
			OUT_ERROR( NULL, 0, "packmgr" , "msg id %x, add pack car id %s, count %d, index %d error" , msgid, carid, count, index ) ;
			return false ;
		}

		char key[1024] = {0} ;
		// 这里ID是由手机号以及消息ID和分包数
		sprintf( key, "%s-%d-%d-%d" , carid, msgid, count , (seq-index)+1 ) ;

		CMemFile *p = NULL ;

		CMapFile::iterator it = _mapPack.find( key ) ;
		if ( it == _mapPack.end() ) {
			p = new CMemFile(key) ;
			if ( p == NULL ) {
				OUT_ERROR( NULL, 0, "packmgr" , "msg id %x, add pack car id %s, count %d, index %d malloc data faild" , msgid, carid, count, index ) ;
				return false ;
			}
		} else {
			p = it->second ;
			// 移除不必要时间索引
			p = _queue.erase( p ) ;
		}

		// 如果成功将多个数据合成一个数据包则直接从内存删除
		if ( p->AddBuffer( pack, index, count, buf, len ) ) {
			// 打印数据信息
			OUT_INFO( NULL, 0, "packmgr" , "msg id %x, build msg pack success", msgid ) ;
			// 只有不为一个数据长消息才会有索引
			if ( it != _mapPack.end() ) {
				// 从队列中移除
				_mapPack.erase( it ) ;
			}
			delete p ;
			return true ;
		}

		// 只有第一次添加时才添加到队列中
		if ( it == _mapPack.end() ) {
			_mapPack.insert( pair<string,CMemFile*>( key, p ) ) ;
		}
		// 添加时间索引
		_queue.push( p ) ;

		return false ;
	}
}

// 处理超时的数据包
void CPackMgr::CheckTimeOut( unsigned int timeout )
{
	share::Guard g( _mutex ) ;
	{
		if ( _queue.size() == 0 ){
			return ;
		}

		time_t now = time(NULL) ;
		if ( now - _lastcheck < LONG_PACK_CHECK ) {
			return ;
		}
		_lastcheck = now ;

		time_t pass = now - timeout ;
		CMemFile *t,*p = _queue.begin() ;
		while( p != NULL ) {
			t = p ;
			p = _queue.next( p ) ;

			if ( t->GetLastTime() > pass ) {
				break ;
			}
			RemovePack( t->GetId() ) ;
		}
	}
}

// 移除数据对象
void CPackMgr::RemovePack( const string &key )
{
	CMapFile::iterator it = _mapPack.find( key ) ;
	if ( it == _mapPack.end() ) {
		return ;
	}
	CMemFile *p = it->second ;
	_mapPack.erase( it ) ;
	delete _queue.erase( p ) ;
}

CPackMgr::CMemFile::CMemFile( const char *id ):_cur( 0 )
{
	_last = time(NULL) ;
	_next = _pre = NULL ;
	_id   = id ;
}

CPackMgr::CMemFile::~CMemFile()
{
	if ( _vec.empty() ) {
		return ;
	}
	for ( size_t i = 0; i < _vec.size(); ++ i ) {
		delete _vec[i] ;
	}
	_vec.clear() ;
}

// 添加内存数据
bool CPackMgr::CMemFile::AddBuffer( DataBuffer &pack, const int index, const int count, const char *buf, int len )
{
	// 如果为一个数据就不需要处理组包了
	if ( count == 1 ) {
		pack.writeBlock( buf, len ) ;
		return true ;
	}

	// 如果还没有开辟数据空间
	if ( _vec.empty() ) {
		for ( int i = 0; i < count; ++ i ) {
			_vec.push_back( new DataBuffer ) ;
		}
	}

	// 如果不正确的INDEX就不处理了
	if ( index > (int) _vec.size() ) {
		return false ;
	}

	// 取得存放数据块的内存
	DataBuffer *p = _vec[index-1] ;
	assert( p != NULL ) ;
	if ( p->getLength() == 0 ) {
		++ _cur ;
	} else {
		p->resetBuf() ;
	}
	p->writeBlock( buf, len ) ;

	// 如果接收数据包成功
	if ( _cur >= (unsigned int) count ) {
		// 取得分包个数
		int size = _vec.size() ;
		// 写最后一个数据包的头部
		pack.writeBlock( p->getBuffer() , sizeof(GBheader) ) ;
		// 重新开始组包，这样数据就只有数据头和数据体
		for ( int i = 0; i < size ; ++ i ) {
			p = _vec[i] ;
			if ( p->getLength() < (int)sizeof(GBheader) ){
				continue ;
			}
			unsigned short mlen = p->fetchInt16( 3 ) & 0x03FF;
			if ( mlen > 0 ) {
				pack.writeBlock( p->getBuffer() + sizeof(GBheader) + 4 , mlen ) ;
			}
		}
		// 添加尾部数据
		GBFooter footer ;
		pack.writeBlock( &footer, sizeof(footer) ) ;

		return true ;
	}

	// 更新最后一次访问的时间
	_last = time(NULL) ;

	OUT_INFO( NULL, 0, "packmgr" , "save index file %d, current count %d total %d" , index, _cur, count ) ;

	return false ;
}
