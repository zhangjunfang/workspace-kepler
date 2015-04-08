/**
 * date:   2011/07/21
 * author: humingqing
 * memo:   引用计数对象，当引用值为零时释放对象
 *  2012.12.10 修改引用计数据对象，将锁使用全局锁，因为它的作用时间很短，整个程序可以共用一个锁
 *  这样可以减少应用中锁的资源占用
 */

#include <Ref.h>
#include <Mutex.h>
#include <assert.h>

namespace share{

/**
 * 引用记数锁
 */
static Mutex _gmutex__ ;


Ref::Ref():_ref__(0)
{
}

/**
 * 添加引用
 */
int Ref::AddRef()
{
	_gmutex__.lock() ;
	++ _ref__ ;
	_gmutex__.unlock() ;

	return _ref__ ;
}

/**
 * 取得引用
 */

int Ref::GetRef()
{
	Guard g( _gmutex__ ) ;

	return _ref__ ;
}

/**
 * 释放引用
 */
void Ref::Release()
{
	bool destory = false ;
	{
		_gmutex__.lock() ;
		-- _ref__ ;
		assert( _ref__ >= 0 ) ;

		if ( _ref__ == 0 ) {
			destory = true ;
		}
		_gmutex__.unlock() ;
	}
	if ( destory ) {
		delete this ;
	}
}


}
