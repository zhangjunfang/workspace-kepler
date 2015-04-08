/*
 * oracledb.h
 *
 *  Created on: 2012-5-26
 *      Author: humingqing
 */

#ifndef __ORACLEDB_H__
#define __ORACLEDB_H__

#include "idatapool.h"

// KEY和VALUE的接口对象
class OracleSqlObj : public CSqlObj
{
public:
	// 结构体对象
	struct _SqlVal
	{
		SQLTYPE _type;
		string _sval;
		int _nval;
        long long _llval;
	};
	typedef map< string, _SqlVal > CKVMap;

public:
	OracleSqlObj( ) { }
	virtual ~OracleSqlObj( ){ }

	// 添加整形数据
	void AddInteger( const string &key, int val ) ;
	// 添加字符串符数据
	void AddString( const string &key, const string &val ) ;

	void AddVar(const string &key, const string &val);
	//oracle 不实现这个借口
	void AddCSqlObj( const string &key, CSqlObj *sql_obj ) {}
	void AddLongLong( const string &key, long long val );
	// 是否为空
	bool IsEmpty( void ) const ;
	// 重置所有數據
	void ClearObj( void ) ;
	// 序列化接口
	void Seralize( DataBuffer &buf ) ;
	// 反序列化处理
	void UnSeralize( const char *ptr, int len ) ;

public:
	CKVMap _kv;
};

// Oracle查询的数据结果集
class OracleResult : public CSqlResult
{
	struct _colvalue
	{
		int    _col ;
		char **_val ;

		_colvalue( int col ) {
			_col = col ;
			_val = NULL ;
			_val = new char*[col] ;

			for ( int i = 0; i < col; ++ i ) {
				_val[i] = NULL ;
			}
		}

		~_colvalue() {
			if ( _val == NULL ) {
				return ;
			}
			for ( int i = 0; i < _col; ++ i ) {
				if ( _val[i] == NULL )
					continue ;
				delete [] _val[i] ;
			}
			delete [] _val ;
		}

		void setvalue( int index, const char* val ) {
			if ( _val[index] != NULL ) {
				delete [] _val[index] ;
				_val[index] = NULL ;
			}
			if ( val == NULL )
				return ;

			int len = strlen( val ) ;
			_val[index]  = new char[len+1] ;
			memset( _val[index], 0, len+1 ) ;
			memcpy( _val[index], val, len ) ;
		}
	};
	typedef std::vector<_colvalue*>  VecValue ;
public:
	OracleResult():_row(0),_col(0){}
	~OracleResult() ;
	// 取得结果记录条数
	int GetCount( void )  { return _row; }
	// 取得列記錄個數
	int GetColumn( void ) { return _col; }
	// 针对关系型数据库取字段取得结果集的数据
	const char * GetValue( int row , int col ) ;
	// 针对Mongo取得指针字段的数据
	const char * GetValue( int row , const char *name ) { return NULL; }

public:
	// 设置可用使用的列数
	void SetColumn( int col ) { _col = col; }
	// 添加值处理
	void AddValue( int index, int col, const char *value ) ;

private:
	// 取得记录数据
	int      _row ;
	// 取得行的个数记录
	int 	 _col ;
	// 取得结果集数据
	VecValue _vec ;
};

class COracleDB : public IDBFace
{
public:
	COracleDB( unsigned int id );
	~COracleDB( );

	// 初始化化数据库连接
	bool Init( const char *ip, const unsigned short port, const char *user, const char *pwd, const char *dbname );
	// 取得连接对象字符串的HASH转化值
	unsigned int GetId( void );
	// 数据库对象中写入数据
	int Insert( const char *stable, const CSqlObj *obj );
	// 批量插入数据库操作
	bool InsertBatch( const char *stable, const vector< CSqlObj* > &vec );
	// 更新操作
	bool Update( const char *stable, const CSqlObj *obj, const CSqlWhere *where );
	// 带条件的删除操作
	bool Delete( const char *stable, const CSqlWhere *where );
	// 根据SQL查询取数据,这里是针对关系型数据库的接口
	CSqlResult * Select( const char *sql ) ;

private:
	// 是否提交事务操作
	bool Execute( const char *stable, const OracleSqlObj *obj, bool commit );
	// 检建WHERE的表达式
	bool BuildWhere( const CSqlWhere *where, string &s );
	// 构建更新字符的表达式
	bool BuildUpdate( const OracleSqlObj *obj, string &s );

private:
	// 查询数据
	int	oracle_select( const char* sql , OracleResult *sql_result );
	// 执行命令
	int oracle_exec( const char* sql, bool commit = false );
	// 连接数据库
	void* oracle_connect( const char* addr, const unsigned short port, const char *user, const char* pass,
			const char* db_name );
	// 关闭连接
	int oracle_close( void );
	// 提交
	int oracle_commit( void );
	// 回滚
	int oracle_rollback( void );

private:
	// 数据库连接对象ID
	unsigned int _id;
	// 数据库连接对象
	void * _handle;
	// 错误信息
	string _errinfo;
};

#endif /* ORACLEDB_H_ */
