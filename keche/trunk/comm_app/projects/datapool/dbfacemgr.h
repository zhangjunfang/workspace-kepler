/*
 * dbfacemgr.h
 *
 *  Created on: 2012-5-26
 *      Author: humingqing
 */

#ifndef __DBFACEMGR_H__
#define __DBFACEMGR_H__

#include "idatapool.h"
// 数据库连接开辟对象
class CDBFaceMgr
{
public:
	// 取得字符串的ID
	static unsigned int GetHash( const char *s ) ;
	// 取得数据库对象
	static IDBFace * GetDBFace( const char *s , unsigned int key ) ;
};

#endif /* DATAALLOC_H_ */
