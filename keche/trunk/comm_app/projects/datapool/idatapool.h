/*
 * idatapool.h
 *
 *  Created on: 2012-5-22
 *      Author: think
 */

#ifndef __IDATAPOOL_H__
#define __IDATAPOOL_H__

#include <map>
#include <string>
#include <list>
#include <vector>
#include <Ref.h>
#include <packer.h>

using namespace std;

// 定义释放数据对象宏的操作
#define RELEASE_OBJ( p )   if( p != NULL ) { delete p ; p = NULL ;}

class CSqlObj
{
public:
	enum SQLTYPE{TYPE_INT = 0, TYPE_FLOAT = 1, TYPE_DOUBLE = 2, TYPE_STRING = 3, TYPE_MONGO = 4, TYPE_VAR = 5, TYPE_LONGLONG = 6};
	CSqlObj(){}
    virtual ~CSqlObj(){}
    // 序列化接口
    virtual void Seralize( DataBuffer &buf ) = 0 ;
    // 反序列化处理
    virtual void UnSeralize( const char *ptr, int len ) = 0 ;
	// 结构体对象
	virtual void AddInteger( const string &key, int val) = 0 ;
	// 添加长整形变量
	virtual void AddLongLong(const string &key, long long val) = 0 ;
	// 添加字符串处理
	virtual void AddString( const string &key, const string &val) = 0 ;

	//val作为一个变量插入，这个变量是从oracle数据中取出来的。
	virtual void AddVar(const string &key, const string &val) = 0;

	//增加本身，只有在MongoDB中实现。
	virtual void AddCSqlObj(const string &key, CSqlObj *sql_obj) = 0 ;
	// 重置所有數據
	virtual void ClearObj( void ) =  0;
};

// 条件WHERE的处理
class CSqlWhere
{
public:
	// 操作关系定义"", "AND", "OR"
	enum TWHERE{ TYPE_NO = 0, TYPE_AND = 1, TYPE_OR = 2 } ;
	// 操作符定义 "=","!=",">",">=","<","<=","like"
	enum TOPER{ OP_EQ = 0, OP_NEQ = 1, OP_MORE = 2, OP_EMORE = 3, OP_LESS = 4 , OP_ELESS= 5, OP_LIKE = 6 } ;
	// 结构体对象
	struct _WhereVal
	{
		TWHERE  _rel;
		string  _skey;  // 值
		TOPER   _op;
		string  _sval;
	};
	typedef list<_WhereVal> CWhereList ;
public:
	// 添加WHERE操作
	void AddWhere( const string &key, const string &val , TOPER op = OP_EQ, TWHERE rel = TYPE_NO ) ;
	// 取得关系对象
	bool GetWhereList( CWhereList &w ) const ;
	// 清除WEHERE的LIST
	void ClearWhereList() ;
	// 将SQLWHERE对象序列化
	void Seralize( DataBuffer &buf ) ;
	// 将数据对象反序列化
	void UnSeralize( const char *ptr, int len ) ;

private:
	// 条件列表
	CWhereList _wlst ;
};

// 查询取数据接口
class CSqlResult
{
public:
	virtual ~CSqlResult() {} ;
	// 取得结果记录条数
	virtual int GetCount( void )  =  0 ;
	// 取得列記錄個數
	virtual int GetColumn( void ) =  0 ;
	// 针对关系型数据库取字段取得结果集的数据
	virtual const char * GetValue( int row , int col ) = 0 ;
	// 针对Mongo取得指针字段的数据
	virtual const char * GetValue( int row , const char *name ) = 0 ;
};

//////////////////DATABASE AGENT OBJECT////////////////////////
#define DB_ERR_NOIMPLEMENT			   -1  // not implement error
#define DB_ERR_SUCCESS					0
#define DB_ERR_FAILED					1
#define DB_ERR_NOCONNECTION				2
#define DB_ERR_SQLERROR					3
#define DB_ERR_NOTIMPL					4
#define DB_ERR_TIMEOUT					5
#define DB_ERR_NOMEM					6
#define DB_ERR_PARAMERROR				7
#define DB_ERR_GETAUTOIDFAILED			8
#define DB_ERR_COMMITFAILED				9
#define DB_ERR_ROLLBACKFAILED			10
#define DB_ERR_STORERESULT				11
#define DB_ERR_SETSQLATTR				12
#define DB_ERR_ALLOCHANDLE				13

/*  网络错误   */
#define DB_ERR_SOCK                     14

// 数据库操作接口
class IDBFace: public share::Ref
{
public:
	virtual ~IDBFace() {}
	// 取得连接对象字符串的HASH转化值
	virtual unsigned int GetId( void ) = 0 ;
	// 数据库对象中写入数据
	virtual int  Insert( const char *stable, const CSqlObj *obj ) = 0 ;

	/*************************************
	 * 为了屏蔽oracle和mongoDB的区别，批量传入的CSqlObj应该时指针，使用完后自己释放。
	 * 还有一种方法就是重载两个接口，在接口中具体指明哪种SqlObj的类型。从接口和使用的角度，这样显然也不好。
	 *************************************/
	virtual bool InsertBatch( const char *stable, const vector<CSqlObj*> &vec ) = 0 ;
	// 更新操作
	virtual bool Update( const char *stable, const CSqlObj *obj, const CSqlWhere *where ) = 0 ;
	// 带条件的删除操作
	virtual bool Delete( const char *stable, const CSqlWhere *where ) = 0 ;
	// 根据SQL查询取数据,这里是针对关系型数据库的接口
	virtual CSqlResult* Select( const char *sql ) = 0 ;
	// 释放结果集
	virtual void FreeResult( CSqlResult *rs ) { RELEASE_OBJ(rs); };
};

// type=oracle;ip=;port=;user=;pwd=;db=;
// 数据库对象池的接口

// 数据对象池
class IDataPool
{
public:
    enum DB_TYPE{ oracle, mongo, mysql, redis };
    enum DB_OPRE{ dothing, insert, update };

    // 取得SQL的OBJ对象
    static CSqlObj* GetSqlObj(DB_TYPE type) ;
    // 释放数据对象
    static void Release(CSqlObj *obj) { RELEASE_OBJ(obj); }

	virtual ~IDataPool() {}
	// 初始化数据库连接对象
	virtual bool Init( void )  = 0 ;
	// 启动数据库连接对象
	virtual bool Start( void ) = 0 ;
	// 停止数据连接对象
	virtual bool Stop( void ) = 0 ;
	// 签出数据操作对象,根据连接串来创建军对象
	virtual IDBFace * CheckOut( const char *connstr ) = 0 ;
	// 签入数据操作对象
	virtual void CheckIn( IDBFace *obj ) = 0 ;
	// 删除数据操作对象
	virtual  void Remove(IDBFace *obj) = 0;
};

// 数据执行对象
class CSqlData
{
public:
	IDataPool::DB_OPRE 	oper;
	string 				table;
	CSqlObj   * 		sql_obj;
	CSqlWhere * 		sql_where;

	CSqlData()
	{
		oper 		= IDataPool::insert ;
		sql_obj 	= NULL;
		sql_where   = NULL;
	}

	~CSqlData()
	{
		IDataPool::Release(sql_obj) ;
		RELEASE_OBJ( sql_where ) ;
	}
};

// 数据库执行单元
class CSqlUnit
{
public:
	CSqlUnit( IDataPool::DB_TYPE type, CSqlData *data , unsigned int groupid = 0 ) ;
	CSqlUnit( const char *ptr, int len ) ;
	~CSqlUnit() ;
	// 序列化数据对象
	void Seralize( DataBuffer &buf ) ;

private:
	// 反序列数据对象
	void UnSeralize( const char *ptr, int len ) ;

public:
	// 数据库类型
	IDataPool::DB_TYPE  _dbtype ;
	// 组的ID号
	unsigned int        _groupid ;
	// 数据执行对象
	CSqlData	       *_sqldata ;
};

#endif /* IDATAPOOL_H_ */
