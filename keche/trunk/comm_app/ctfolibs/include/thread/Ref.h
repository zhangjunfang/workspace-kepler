/**
 * date:   2011/07/21
 * author: humingqing
 * memo:   引用计数对象，当引用值为零时释放对象
 */

#ifndef __SHARE_REF_H__
#define __SHARE_REF_H__

namespace share{

class Ref{
public:
	Ref() ;
	virtual ~Ref() {} ;

	/**
	 * 添加引用
	 */
	int AddRef() ;

	/**
	 * 取得引用
	 */
	int GetRef() ;

	/**
	 * 释放引用
	 */
	void Release() ;

private:
	/**
	 * 引用记数值
	 */
	unsigned int _ref__ ;
};

}

#endif
