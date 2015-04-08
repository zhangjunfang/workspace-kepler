/*
 * mongodb_pool.h
 *
 *  Created on: 2012-5-28
 *      Author: xifengming
 */

#ifndef _MONGO_DB_H
#define _MONGO_DB_H

#include <BaseClient.h>
#include <OnlineUser.h>
#include <Session.h>
#include <Mutex.h>
#include <tools.h>
#include "idatapool.h"
#include "mongo/client/dbclient.h"

using namespace std;
using namespace mongo;
using namespace bson;

class MongoDBSqlObj : public CSqlObj
{
public:
	MongoDBSqlObj(){}
	virtual ~MongoDBSqlObj(){}

	BSONObjBuilder & GetBsonObj() ;
	void AddInteger( const string &key, int val) ;
	void AddLongLong(const string &key, long long val) ;
	void AddString( const string &key, const string &val) ;
	//增加本身，只有在MongoDB中实现。
	void AddCSqlObj(const string &key, CSqlObj *sql_obj) ;

	void AddVar(const string &key, const string &val){return;}
	// 清理所有數據
	void ClearObj( void ) ;
	// 序列化接口
	void Seralize( DataBuffer &buf ) ;
	// 反序列化处理
	void UnSeralize( const char *ptr, int len ) ;

private:
	BSONObjBuilder _bson_obj;
};

class CMongoDB: public IDBFace
{
public:
	CMongoDB(unsigned int id) : _id(id){};
	virtual ~CMongoDB(){};

	// 初始化化数据库连接
	bool Init(const char *ip, const unsigned short port, const char *user, const char *pwd,
			const char *dbname);
	// 取得连接对象字符串的HASH转化值
	unsigned int GetId(void);
	// 插入操作，return value, success : 0, error : err number defined
	virtual int Insert(const char *stable, const CSqlObj *obj);
	// 批量插入数据库操作
	virtual bool InsertBatch(const char *stable, const vector<CSqlObj*> &vec);
	// 更新操作
	virtual bool Update(const char *stable, const CSqlObj *obj, const CSqlWhere *where);
	// 带条件的删除操作
	virtual bool Delete(const char *stable, const CSqlWhere *where);
	// 根据SQL查询取数据,这里是针对关系型数据库的接口
	virtual CSqlResult * Select( const char *sql ) { return NULL; }

private:
	bool MongoDB_Insert(const char *stable, const vector<BSONObj> &pbulk_obj);
	// 检建WHERE的表达式
	bool BuildWhere(const CSqlWhere *where, Query &query);
private:
	// 数据库连接对象
	DBClientConnection  _conn;
	// 数据库连接对象ID
	unsigned int 		_id;
	// 数据库名
	string 				_mongo_db_name;
	// 错误信息
	string 				_errinfo;
};
#endif
