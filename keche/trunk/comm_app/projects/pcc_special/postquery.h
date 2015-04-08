/*
 * postquery.h
 *
 *  Created on: 2012-7-5
 *      Author: humingqing
 *
 *  平台查岗自动应答处理
 */

#ifndef __POSTQUERY_H__
#define __POSTQUERY_H__

#include <map>
#include <vector>
#include <string>
#include <Mutex.h>

class CPostQueryMgr
{
	typedef std::vector<std::string> VecString ;
	struct _PostQuery
	{
		int 	  _index ;
		int 	  _size ;
		VecString _vec ;
	};
	typedef std::map<std::string,_PostQuery*> CMapPostQuery;
public:
	CPostQueryMgr() ;
	~CPostQueryMgr() ;
	// 加载平台查岗的数据
	bool LoadPostQuery( const char *path, int accesscode ) ;
	// 取得平台查岗的内容
	bool GetPostQuery( int accesscode, unsigned char type, std::string &content ) ;

private:
	// 清理平台查岗的数据
	void ClearPost( void ) ;
	// 拆分数据条项目
	bool SplitItem( int accesscode, char * p ) ;
	// 拆数据体内容
	bool SplitContent( const char *key, char *body ) ;

private:
	// 处理数据锁的操作
	share::Mutex   _mutex ;
	// 平台查岗自动应答的内容
	CMapPostQuery  _mpPost ;
};


#endif /* AUTOPOSTQUERY_H_ */
