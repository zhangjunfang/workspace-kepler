/*
 * oracledb.cpp
 *
 *  Created on: 2012-5-26
 *      Author: humingqing
 */

#include "oracledb.h"
#include <oci.h>
#include <time.h>
#include <comlog.h>

COracleDB::COracleDB( unsigned int id ) :
		_id( id )
{
	_handle = NULL;
}

COracleDB::~COracleDB( )
{
	if ( _handle != NULL ) {
		oracle_close();
		_handle = NULL;
	}
}

// 初始化化数据库连接
bool COracleDB::Init( const char *ip, const unsigned short port, const char *user, const char *pwd, const char *dbname )
{
	void *handle = oracle_connect( ip, port, user, pwd, dbname );
	if ( handle == NULL ) {
		OUT_ERROR( NULL, 0, "Oracle", "connect db: %s:%d user:%s pwd:%s dbname:%s failed",
				ip, port, user, pwd, dbname );
		return false;
	}
	_handle = handle;
	return true;
}

// 取得连接对象字符串的HASH转化值
unsigned int COracleDB::GetId( void )
{
	return _id;
}

static const string Int2Str( int n )
{
	char buf[128] = { 0 };
	sprintf( buf, "%d", n );
	return buf;
}
static const string ll2Str( long long n )
{
	char buf[128] = { 0 };
	sprintf( buf, "%lld", n );
	return buf;
}

// 是否提交事务操作
bool COracleDB::Execute( const char *stable, const OracleSqlObj *obj, bool commit )
{
	OracleSqlObj::CKVMap kv = obj->_kv;

	if ( kv.empty() )
		return false;

	string sqlbuf = "insert into ";
	sqlbuf += stable;
	sqlbuf += "(";

	string key, val;
	OracleSqlObj::CKVMap::iterator it;
	for ( it = kv.begin(); it != kv.end() ; ++ it ) {
		const string &skey = it->first;
		OracleSqlObj::_SqlVal &oval = it->second;
		if ( skey.empty() )
			continue;

		if ( ! key.empty() ) {
			key += ",";
			val += ",";
		}

		key += skey;
		if ( oval._type == CSqlObj::TYPE_STRING ) {
			val += "'" + oval._sval + "'";
		} else if ( oval._type == CSqlObj::TYPE_VAR ) {
			val += oval._sval;
		} else if ( oval._type == CSqlObj::TYPE_INT ) {
			val += Int2Str( oval._nval );
		}else if ( oval._type == CSqlObj::TYPE_LONGLONG){
            val += ll2Str( oval._llval);
		}
	}

	sqlbuf += key + ")values(" + val + ")";

	int ret = oracle_exec( sqlbuf.c_str(), commit ) ;
	if ( ret != DB_ERR_SUCCESS ) {
		OUT_ERROR( NULL, 0, "Oracle", "Execute sql: %s, result %d", sqlbuf.c_str(), ret ) ;
		return false;
	}

	return true;
}

// 数据库对象中写入数据
int COracleDB::Insert( const char *stable, const CSqlObj *obj )
{
	return Execute( stable, ( OracleSqlObj* ) obj, true ) ? 0 : DB_ERR_FAILED;
}

// 批量插入数据库操作
bool COracleDB::InsertBatch( const char *stable, const vector< CSqlObj* > &vec )
{
	if ( vec.empty() )
		return false;

	int count = 0;
	int size  = vec.size() ;
	for ( int i = 0 ; i < size ; ++ i ) {
		if ( ! Execute( stable, ( OracleSqlObj* ) vec[i], false ) )
			continue;
		count = count + 1;
	}
	if ( count > 0 )
		oracle_commit();

	return ( count > 0 );
}

// 检建WHERE的表达式
bool COracleDB::BuildWhere( const CSqlWhere *where, string &s )
{
	CSqlWhere::CWhereList wl;
	if ( ! where->GetWhereList( wl ) )
		return false;

	CSqlWhere::CWhereList::iterator it;
	for ( it = wl.begin(); it != wl.end() ; ++ it ) {
		CSqlWhere::_WhereVal &val = * it;
		if ( ! s.empty() ) {
			switch ( val._rel )
			{
			case CSqlWhere::TYPE_OR:
				s += " OR ";
				break;
			case CSqlWhere::TYPE_AND:
				s += " AND ";
				break;
			default:
				s += " AND ";
				break;
			}
		}

		s += val._skey;
		switch ( val._op )
		{
		case CSqlWhere::OP_EQ: // =
			s += "=";
			break;
		case CSqlWhere::OP_NEQ: // <>
			s += "<>";
			break;
		case CSqlWhere::OP_MORE: // >
			s += ">";
			break;
		case CSqlWhere::OP_EMORE: // >=
			s += ">=";
			break;
		case CSqlWhere::OP_LESS: // <
			s += "<";
			break;
		case CSqlWhere::OP_ELESS: // <=
			s += "<=";
			break;
		case CSqlWhere::OP_LIKE: // like ''
			s += " like ";
			break;
		default:
			s += "=";
			break;
		}
		s += "'" + val._sval + "'";
	}
	return ( ! s.empty() );
}

// 构建更新字符的表达式
bool COracleDB::BuildUpdate( const OracleSqlObj *obj, string &s )
{
	OracleSqlObj::CKVMap kv = obj->_kv;

	OracleSqlObj::CKVMap::iterator it;
	for ( it = kv.begin(); it != kv.end() ; ++ it ) {
		const string &skey = it->first;
		OracleSqlObj::_SqlVal &oval = it->second;
		if ( skey.empty() )
			continue;

		if ( ! s.empty() )
			s += ",";

		s += skey + "=";
		if ( oval._type == CSqlObj::TYPE_STRING ) {
			s += "'" + oval._sval + "'";
		} else if ( oval._type == CSqlObj::TYPE_VAR ) {
			s += oval._sval;
		} else if ( oval._type == CSqlObj::TYPE_INT ) {
			s += Int2Str( oval._nval );
		}
	}

	return ( ! s.empty() );
}

// 更新操作
bool COracleDB::Update( const char *stable, const CSqlObj *obj, const CSqlWhere *where )
{
	string supdate;
	// 构建更新操作
	if ( ! BuildUpdate( ( OracleSqlObj* ) obj, supdate ) ) {
		return false;
	}

	string sql = "update ";
	sql += stable;
	sql += " set ";
	sql += supdate;

	string swhere;
	// 构建的条件
	if ( BuildWhere( where, swhere ) ) {
		sql += " where ";
		sql += swhere;
	}

	// 执行更新操作
	int ret = oracle_exec( sql.c_str(), true )  ;
	if ( ret != DB_ERR_SUCCESS ) {
		OUT_ERROR( NULL, 0, "Oracle", "Exceute sql: %s, result %d", sql.c_str() , ret );
		return false;
	}

	return true;
}

// 带条件的删除操作
bool COracleDB::Delete( const char *stable, const CSqlWhere *where )
{
	string sql = "delete from ";
	sql += stable;

	string swhere;
	// 构建删除的条件
	if ( BuildWhere( where, swhere ) ) {
		sql += " where ";
		sql += swhere;
	}

	// 执行删除操作
	int ret = oracle_exec( sql.c_str(), true ) ;
	if ( ret != DB_ERR_SUCCESS ) {
		OUT_ERROR( NULL, 0, "Oracle", "Exceute sql: %s, result %d", sql.c_str() , ret );
		return false;
	}
	return true;
}

// 根据SQL查询取数据,这里是针对关系型数据库的接口
CSqlResult *COracleDB::Select( const char *sql )
{
	OracleResult *result = new OracleResult;
	if ( result == NULL )
		return NULL ;

	int ret = oracle_select( sql, result );
	if ( ret != DB_ERR_SUCCESS ) {
		OUT_ERROR( NULL, 0, "Oracle", "Select sql: %s, result %d", sql, ret );
		delete result ;
		return NULL ;
	}
	return result ;
}

static int checkerr( OCIError *errhp, sword status, string & err_info, const char* sql = NULL )
{
	text errbuf[512] = { 0 };
	sb4 errcode = - 1;

	string &temp = err_info;

	switch ( status )
	{
	case OCI_SUCCESS:
	{
		errcode = 0;
		break;
	}
	case OCI_SUCCESS_WITH_INFO:
	{
		errcode = 0;
		temp = "Error - OCI_SUCCESS_WITH_INFO";
		break;
	}
	case OCI_NEED_DATA:
	{
		temp = "Error - OCI_NEED_DATA";
		break;
	}
	case OCI_NO_DATA:
	{
		temp = "Error - OCI_NODATA";
		break;
	}
	case OCI_ERROR:
	{
		OCIErrorGet( ( dvoid * ) errhp, ( ub4 ) 1, ( text * ) NULL, & errcode, errbuf, ( ub4 ) sizeof ( errbuf ),
				OCI_HTYPE_ERROR );

		temp.assign( ( char* ) errbuf, sizeof ( errbuf ) );

		break;
	}
	case OCI_INVALID_HANDLE:
	{
		temp = "Error - OCI_INVALID_HANDLE";
		break;
	}
	case OCI_STILL_EXECUTING:
	{
		temp = "Error - OCI_STILL_EXECUTE";
		break;
	}
	case OCI_CONTINUE:
	{
		temp = "Error - OCI_CONTINUE";
		break;
	}
	default:
		break;
	}

	if ( status != OCI_SUCCESS ) {
		if ( sql != NULL ) {
			temp += " SQL statement : \n";
			temp += sql;
		}
		OUT_ERROR( NULL, 0, "Oracle", "error code: %d, error message: %s", status, temp.c_str() );
	}

	return errcode;
}

void my_usleep( void )
{
#ifdef _WIN32
	Sleep(1);
#else
	int n = 1000000;
	struct timespec rqtp;
	rqtp.tv_sec = 0;
	rqtp.tv_nsec = n;
	nanosleep( & rqtp, NULL );
#endif
}

typedef struct __oracle_db_handle
{
	OCIError *errhp;
	OCIEnv *envhp;
	OCIStmt *stmthp;
	OCIServer *srvhp;
	OCISvcCtx *svchp;
	OCISession *authp;

} ORACLE_HANDLE;

int COracleDB::oracle_close( void )
{
	ORACLE_HANDLE* handle = ( ORACLE_HANDLE* ) _handle;

	if ( handle == NULL )
		return DB_ERR_SUCCESS;

	int status;
	if ( handle->svchp != NULL ) {
		while ( ( status = OCISessionEnd( handle->svchp, handle->errhp, handle->authp, OCI_DEFAULT ) )
				== OCI_STILL_EXECUTING ) {
			my_usleep();
		}
	}

	if ( handle->srvhp != NULL ) {
		while ( ( status = OCIServerDetach( handle->srvhp, handle->errhp, OCI_DEFAULT ) ) == OCI_STILL_EXECUTING ) {
			my_usleep();
		}
	}

	if ( handle->envhp != NULL ) {
		OCIHandleFree( ( dvoid * ) ( handle->envhp ), OCI_HTYPE_ENV );
	}

	if ( handle != NULL ) {
		delete handle;
		handle = NULL;
	}

	return 0;
}

int COracleDB::oracle_commit( void )
{

	ORACLE_HANDLE* handle = ( ORACLE_HANDLE* ) _handle;

	int status = 0;

	int errcode = 0;

	time_t t1 = time( NULL );

	while ( ( status = OCITransCommit( handle->svchp, handle->errhp, 0 ) ) == OCI_STILL_EXECUTING ) {
		if ( time( NULL ) - t1 > 60 ) {
			// timeout
			errcode = DB_ERR_TIMEOUT;
			break;
		}
		my_usleep();
	}

	if ( errcode != DB_ERR_TIMEOUT && status ) {
		errcode = checkerr( handle->errhp, status, _errinfo );

		if ( errcode == 1012 || errcode == 12705 || errcode == 3113 || errcode == 3114 || errcode == 12541
				|| errcode == 12547 ) {
			errcode = DB_ERR_NOCONNECTION;
		} else {
			errcode = DB_ERR_FAILED;
		}
	}

	return errcode;

}

int COracleDB::oracle_rollback( void )
{
	ORACLE_HANDLE* handle = ( ORACLE_HANDLE* ) _handle;

	int status = 0;

	int errcode = 0;

	time_t t1 = time( NULL );

	while ( ( status = OCITransRollback( handle->svchp, handle->errhp, 0 ) ) == OCI_STILL_EXECUTING ) {
		if ( time( NULL ) - t1 > 60 ) {
			// timeout
			errcode = DB_ERR_TIMEOUT;
			break;
		}
		my_usleep();
	}

	if ( errcode != DB_ERR_TIMEOUT && status ) {
		errcode = checkerr( handle->errhp, status, _errinfo );

		if ( errcode == 1012 || errcode == 12705 || errcode == 3113 || errcode == 3114 || errcode == 12541
				|| errcode == 12547 ) {
			errcode = DB_ERR_NOCONNECTION;
		} else {
			errcode = DB_ERR_FAILED;
		}
	}

	return errcode;
}

void* COracleDB::oracle_connect( const char* addr, const unsigned short port, const char *user, const char* pass,
		const char* db_name )
{
	char service[256] = {0};
	sprintf( service, "//%s:%d/%s", addr, port, db_name );

	ORACLE_HANDLE* handle = new ORACLE_HANDLE;

	memset( handle, 0, sizeof(ORACLE_HANDLE) );

	int status = 0;

	OCIEnvCreate( ( OCIEnv ** ) & ( handle->envhp ), ( ub4 ) OCI_THREADED, ( dvoid * ) 0,
			( dvoid * (*)( dvoid *, size_t )) 0, (dvoid * (*)(dvoid *, dvoid *, size_t))0,
			(void (*)(dvoid *, dvoid *)) 0 , (size_t) 0, (dvoid **) 0 );

	( void ) OCIHandleAlloc( ( dvoid * ) ( handle->envhp ), ( dvoid ** ) & ( handle->errhp ), OCI_HTYPE_ERROR,
			( size_t ) 0, ( dvoid ** ) 0 );

	/* server contexts */
	( void ) OCIHandleAlloc( ( dvoid * ) ( handle->envhp ), ( dvoid ** ) & ( handle->srvhp ), OCI_HTYPE_SERVER,
			( size_t ) 0, ( dvoid ** ) 0 );

	// service context
	( void ) OCIHandleAlloc( ( dvoid * ) ( handle->envhp ), ( dvoid ** ) & ( handle->svchp ), OCI_HTYPE_SVCCTX,
			( size_t ) 0, ( dvoid ** ) 0 );

	status = OCIServerAttach( handle->srvhp, handle->errhp, ( text * ) service, strlen( ( const char * ) service ), 0 );

	status = checkerr( handle->errhp, status, _errinfo );

	if ( status != 0 ) {
		_handle = handle;
		oracle_close();
		_handle = NULL;

		return NULL;
	}

	/* set attribute server context in the service context */
	OCIAttrSet( ( dvoid * ) handle->svchp, OCI_HTYPE_SVCCTX, ( dvoid * ) handle->srvhp, ( ub4 ) 0, OCI_ATTR_SERVER,
			( OCIError * ) handle->errhp );

	OCIHandleAlloc( ( dvoid * ) handle->envhp, ( dvoid ** ) & ( handle->authp ), ( ub4 ) OCI_HTYPE_SESSION,
			( size_t ) 0, ( dvoid ** ) 0 );

	( void ) OCIAttrSet( ( dvoid * ) handle->authp, ( ub4 ) OCI_HTYPE_SESSION, ( dvoid * ) user,
			( ub4 ) strlen( ( char * ) user ), ( ub4 ) OCI_ATTR_USERNAME, handle->errhp );

	OCIAttrSet( ( dvoid * ) handle->authp, ( ub4 ) OCI_HTYPE_SESSION, ( dvoid * ) pass,
			( ub4 ) strlen( ( char * ) pass ), ( ub4 ) OCI_ATTR_PASSWORD, handle->errhp );

	status = OCISessionBegin( handle->svchp, handle->errhp, handle->authp, OCI_CRED_RDBMS, ( ub4 ) OCI_DEFAULT );

	status = checkerr( handle->errhp, status, _errinfo );

	if ( status != 0 ) {
		_handle = handle;
		oracle_close();
		_handle = NULL;

		return NULL;
	}

	status = OCIAttrSet( ( dvoid * ) ( handle->svchp ), ( ub4 ) OCI_HTYPE_SVCCTX, ( dvoid * ) ( handle->authp ),
			( ub4 ) 0, ( ub4 ) OCI_ATTR_SESSION, handle->errhp );

	status = checkerr( handle->errhp, status, _errinfo );

	if ( status ) {
		_handle = handle;
		oracle_close();
		_handle = NULL;

		return NULL;
	}

	status = OCIHandleAlloc( ( dvoid * ) ( handle->envhp ), ( dvoid ** ) & ( handle->stmthp ), OCI_HTYPE_STMT,
			( size_t ) 0, ( dvoid ** ) 0 );

	status = checkerr( handle->errhp, status, _errinfo );

	if ( status ) {
		_handle = handle;
		oracle_close();
		_handle = NULL;

		return NULL;
	}

	/*
	 OCIAttrSet ( handle->srvhp, (ub4) OCI_HTYPE_SERVER,
	 (dvoid *) 0, (ub4) 0,
	 (ub4) OCI_ATTR_NONBLOCKING_MODE, handle->errhp) ;
	 */
	return ( void* ) handle;

}

int COracleDB::oracle_exec( const char* sql, bool commit )
{
	if ( _handle == NULL )
		return DB_ERR_NOCONNECTION;

	if ( sql == NULL )
		return DB_ERR_PARAMERROR;

	int status = 0;

	int errcode = DB_ERR_SUCCESS;

	ORACLE_HANDLE* handle = ( ORACLE_HANDLE* ) _handle;

	status = OCIStmtPrepare( handle->stmthp, handle->errhp, ( text * ) sql, ( ub4 ) strlen( sql ),
			( ub4 ) OCI_NTV_SYNTAX, ( ub4 ) OCI_DEFAULT );

	if ( status == OCI_STILL_EXECUTING ) {
		my_usleep();
	}

	time_t t1 = time( NULL );

	status = OCIStmtExecute( handle->svchp, handle->stmthp, handle->errhp, 1, 0, NULL, NULL, OCI_DEFAULT );

	if ( status == OCI_STILL_EXECUTING ) {
		my_usleep();
	}

	if ( time( NULL ) - t1 > 60 ) {
		// timeout
		errcode = DB_ERR_TIMEOUT;
	}

	if ( errcode == DB_ERR_SUCCESS && status != OCI_SUCCESS_WITH_INFO && status != OCI_SUCCESS ) {
		errcode = checkerr( handle->errhp, status, _errinfo, sql );
		if ( errcode == 1012 || errcode == 12705 || errcode == 3113 || errcode == 3114 || errcode == 12541
				|| errcode == 12547 ) {
			errcode = DB_ERR_NOCONNECTION;
		} else {
			errcode = DB_ERR_FAILED;
		}
	}

	if ( commit )
		oracle_commit();

	// printf("%s ret: %d \n", sql, errcode);

	return errcode;
}

#define  MAX_COL  1024  // 最多的列数据行数
// 数据库查询操作
int COracleDB::oracle_select( const char *sql, OracleResult *sql_result )
{
	int ret = DB_ERR_SUCCESS;

	if ( _handle == NULL )
		return DB_ERR_NOCONNECTION;

	ORACLE_HANDLE* handle = ( ORACLE_HANDLE* ) _handle;

	sb2 ind[MAX_COL];
	ub2 collen[MAX_COL];

	text * colbuf[MAX_COL];
	OCIDefine *defhp[MAX_COL];

	int i = 0;
	int status = 0;
	ub4 col_num = 0;

	OCIParam * colhp;

	while ( 1 ) {
		status = OCIStmtPrepare( handle->stmthp, handle->errhp, ( text * ) sql, ( ub4 ) strlen( ( const char * ) sql ),
				( ub4 ) OCI_NTV_SYNTAX, ( ub4 ) OCI_DEFAULT );
		if ( status != OCI_STILL_EXECUTING )
			break;

		my_usleep();
	}

	time_t t1 = time( NULL );

	while ( 1 ) {
		status = OCIStmtExecute( handle->svchp, handle->stmthp, handle->errhp, 0, 0, NULL, NULL, OCI_DEFAULT );

		if ( status != OCI_STILL_EXECUTING )
			break;

		if ( time( NULL ) - t1 > 60 ) {
			// timeout
			ret = DB_ERR_TIMEOUT;
			break;
		}

		my_usleep();

	}

	if ( ret == DB_ERR_SUCCESS && status != OCI_SUCCESS_WITH_INFO && status != OCI_SUCCESS ) {
		if ( status == OCI_NO_DATA ) {

		} else {
			int errcode = checkerr( handle->errhp, status, _errinfo, sql );

			if ( errcode == 1012 || errcode == 12705 || errcode == 3113 || errcode == 3114 || errcode == 12541
					|| errcode == 12547 ) {
				ret = DB_ERR_NOCONNECTION;
			} else {
				ret = DB_ERR_FAILED;
			}

		}
	}

	if ( ret != DB_ERR_SUCCESS )
		return ret;

	while ( 1 ) {
		status = OCIAttrGet( handle->stmthp, OCI_HTYPE_STMT, & col_num, 0, OCI_ATTR_PARAM_COUNT, handle->errhp );

		if ( status != OCI_STILL_EXECUTING )
			break;

		my_usleep();

	}

	if ( status ) {
		int errcode = checkerr( handle->errhp, status, _errinfo, sql );

		if ( errcode == 1012 || errcode == 12705 || errcode == 3113 || errcode == 3114 || errcode == 12541
				|| errcode == 12547 ) {
			ret = DB_ERR_NOCONNECTION;
		} else {
			ret = DB_ERR_FAILED;
		}
	}

	if ( ret != DB_ERR_SUCCESS )
		return ret;

	for ( i = 1; i <= (int)col_num ; i ++ ) {
		while ( 1 ) {
			status = OCIParamGet( handle->stmthp, OCI_HTYPE_STMT, handle->errhp, ( dvoid ** ) & colhp, i );

			if ( status != OCI_STILL_EXECUTING )
				break;

			my_usleep();
		}

		text *name;
		ub4 namelen = 128;

		while ( 1 ) {
			status = OCIAttrGet( ( dvoid* ) colhp, OCI_DTYPE_PARAM, & name, & namelen, OCI_ATTR_NAME, handle->errhp );

			if ( status != OCI_STILL_EXECUTING )
				break;

			my_usleep();
		}

		while ( 1 ) {
			status = OCIAttrGet( ( dvoid* ) colhp, OCI_DTYPE_PARAM, & collen[i - 1], 0, OCI_ATTR_DATA_SIZE,
					handle->errhp );

			if ( status != OCI_STILL_EXECUTING )
				break;

			my_usleep();
		}

		colbuf[i - 1] = ( text * ) malloc( ( int ) collen[i - 1] + 1 );

		while ( 1 ) {
			status = OCIDefineByPos( handle->stmthp, & defhp[i - 1], handle->errhp, i, ( ub1* ) colbuf[i - 1],
					collen[i - 1] + 1, SQLT_STR, & ind[i - 1], 0, ( ub2* ) 0, OCI_DEFAULT );

			if ( status != OCI_STILL_EXECUTING )
				break;

			my_usleep();
		}

	}

	int j = 0;

	t1 = time( NULL );

	int count = 0;

	// 设置取数据列数
	sql_result->SetColumn( col_num );

	while ( 1 ) {
		status = OCIStmtFetch( handle->stmthp, handle->errhp, 1, OCI_FETCH_NEXT, OCI_DEFAULT );
		if ( status == OCI_NO_DATA ) {
			break;
		}
		if ( status == OCI_STILL_EXECUTING ) {
			if ( time( NULL ) - t1 > 60 ) {
				ret = DB_ERR_TIMEOUT;
				break;
			}

			count ++;
			if ( count > 100 ) {
				my_usleep();
				count = 0;
			}
			continue;
		}

		int errcode = 0;
		errcode = checkerr( handle->errhp, status, _errinfo, sql );
		if ( 0 == errcode || 1406 == errcode ) {
			for ( i = 0; i < (int)col_num ; i ++ ) {
				if ( ind[i] == - 1 )
					colbuf[i][0] = '\0';

				sql_result->AddValue( j, i, ( const char * ) colbuf[i] );
			}
			j ++;

			t1 = time( NULL );

		} else {
			if ( errcode == 1012 || errcode == 12705 || errcode == 3113 || errcode == 3114 || errcode == 12541
					|| errcode == 12547 ) {
				ret = DB_ERR_NOCONNECTION;
			} else {
				ret = DB_ERR_FAILED;
			}

			break;
		}
	}

	// 释放所有列名称
	for ( i = 0; i < (int)col_num ; i ++ ) {
		if ( colbuf[i] )
			free( colbuf[i] );
	}

	return ret;
}

//======================================= OracleSqlObj ===============================================

// 添加整形数据
void OracleSqlObj::AddInteger( const string &key, int val )
{
	_SqlVal obj;
	obj._type = TYPE_INT;
	obj._nval = val;
	_kv.insert( make_pair( key, obj ) );
}

void OracleSqlObj::AddLongLong( const string &key, long long val )
{
	_SqlVal obj;
	obj._type = TYPE_LONGLONG;
	obj._llval = val;
	_kv.insert( make_pair( key, obj ) );
}

// 添加字符串符数据
void OracleSqlObj::AddString( const string &key, const string &val )
{
	_SqlVal obj;
	obj._type = TYPE_STRING;
	obj._sval = val;
	_kv.insert( make_pair( key, obj ) );
}

void OracleSqlObj::AddVar(const string &key, const string &val)
{
	_SqlVal obj;
	obj._type = TYPE_VAR;
	obj._sval = val;
	_kv.insert( make_pair( key, obj ) );
}

// 是否为空
bool OracleSqlObj::IsEmpty( void ) const
{
	return _kv.empty();
}

void OracleSqlObj::ClearObj( )
{
	if ( ! _kv.empty() ) {
		_kv.clear();
	}
}

// 序列化接口
void OracleSqlObj::Seralize( DataBuffer &buf )
{
	int nsize = _kv.size();
	// 写入字段个数
	buf.writeInt16( nsize );
	// 写入插入的字段
	if ( nsize > 0 ) {
		CKVMap::iterator it;
		for ( it = _kv.begin(); it != _kv.end() ; ++ it ) {
			buf.writeInt16( it->first.length() );
			buf.writeBlock( it->first.c_str(), it->first.length() );

			_SqlVal &v = it->second;
			buf.writeInt8( v._type );

			switch( v._type ) {
			case TYPE_INT:
				buf.writeInt32( v._nval );
				break ;
			case TYPE_LONGLONG:
				buf.writeInt64(v._llval);
				break;
			case TYPE_STRING:
			case TYPE_VAR:
				buf.writeInt16( v._sval.length() );
				buf.writeBlock( v._sval.c_str(), v._sval.length() );
				break ;
			default:
				break ;
			}
		}
	}
}

// 反序列化处理
void OracleSqlObj::UnSeralize( const char *ptr, int len )
{
	CPacker pack( ptr, len );
	// 读取个数据
	unsigned short nsize = pack.readShort();
	if ( nsize > 0 ) {
		unsigned short nlen = 0;
		char szbuf[1024] = { 0 };
		for ( unsigned short i = 0 ; i < nsize ; ++ i ) {
			nlen = pack.readShort();
			if ( nlen == 0 ) {
				continue;
			}
			pack.readBytes( szbuf, nlen );

			string k( szbuf, nlen );
			_SqlVal v;
			v._type = ( SQLTYPE ) pack.readByte();
			switch ( v._type )
			{
			case TYPE_INT:
				v._nval = pack.readInt();
				break;
			case TYPE_LONGLONG:
				//todo Cpacker中没有readlonglong的接口，所以用个这个接口代替，以后需要在base中添加这个接口。
				v._llval = pack.readTime();
				break;
			case TYPE_STRING:
			case TYPE_VAR:
				nlen = pack.readShort();
				pack.readBytes( szbuf, nlen );
				v._sval.assign( szbuf, nlen );
				break;
			default:
				break;
			}
			_kv.insert( make_pair( k, v ) );
		}
	}
}

////////////////////////////////// OracleResult ////////////////////////////////////////
OracleResult::~OracleResult( )
{
	if ( _row == 0 )
		return;

	for ( int i = 0 ; i < _row ; ++ i ) {
		delete _vec[i];
	}
	_vec.clear();
}

// 针对关系型数据库取字段取得结果集的数据
const char * OracleResult::GetValue( int row, int col )
{
	if ( row >= _row )
		return NULL;
	if ( _vec[row]->_col <= col )
		return NULL;

	return _vec[row]->_val[col];
}

// 添加值处理
void OracleResult::AddValue( int index, int col, const char *value )
{
	int size = index + 1 ;
	if ( _row < size ) {
		for ( int i = _row ; i < size ; ++ i ) {
			_vec.push_back( new _colvalue( _col ) ) ;
		}
		_row = size ;
	}
	_vec[index]->setvalue( col, value );
}

