#include "mongodb.h"
#include <assert.h>
#include <comlog.h>
#include <stdlib.h>

bool CMongoDB::Init(const char *ip, const unsigned short port, const char *user, const char *pwd,
		const char *dbname)
{
	char szbuf[1024] = {0} ;
	sprintf( szbuf, "%s:%d", ip, port ) ;

	string errmsg;
	if ( ! _conn.connect( szbuf, errmsg ) ) {
		OUT_ERROR( ip, port, user, "connect mongogdb failed,err_msg:%s\n!!", errmsg.c_str() );
		return false;
	} else {
		printf( "connect mongodb success \n" );
	}

	_mongo_db_name = dbname;

	if ( user == NULL || pwd == NULL )
		return true ;

	if ( strlen(user) == 0 ) return true ;
	if ( ! _conn.auth( _mongo_db_name, user, pwd, errmsg ) ) {
		OUT_ERROR( ip, port, user, "connect mongogdb failed,err_msg:%s\n!!", errmsg.c_str() );
		return false;
	}

	return true;
}
//-------------------------------------------------------------------------
// 取得连接对象字符串的HASH转化值
unsigned int CMongoDB::GetId(void)
{
	return _id;
}

//-------------------------------------------------------------------------
int CMongoDB::Insert(const char *stable, const CSqlObj *obj)
{
	if (stable == NULL || obj == NULL)
		return false;

	string sqlbuf = _mongo_db_name + "." + stable;

	try {
		_conn.insert( sqlbuf, ( ( MongoDBSqlObj* ) obj )->GetBsonObj().obj() );
	} catch ( DBException & e ) {
		std::string error = e.toString();
		printf( "conn insert errro info : %s  \n", error.c_str() );

		if ( e.getCode() == 9001 ) {
			return DB_ERR_SOCK;
		} else {
			return DB_ERR_FAILED;
		}
	}
	return 0;
}

bool CMongoDB::InsertBatch(const char *stable, const vector<CSqlObj*> &vec)
{
    /*****  测试 代码， 先不实现  *******************/
//	vector<BSONObj> pbulk_obj;
//	for(int i = 0; i < vec.size(); i++)
//	{
//	    pbulk_obj.push_back(((MongoDBSqlObj*) vec[i])->GetBsonObj().obj());
//	}
//
//	string sqlbuf = _mongo_db_name + "." + stable;
//
//	int num = 1000*1000;
//	time_t btime = time(0);
//	while(num--)
//	{
//	    _conn.insert(sqlbuf, pbulk_obj[0]);
//	}
//	printf("=======================batch insert : %d =================\n", time(0) - btime);
    return true;
}

//-------------------------------------------------------------------------
// 检建WHERE的表达式
bool CMongoDB::BuildWhere(const CSqlWhere *where, Query &query)
{
	CSqlWhere::CWhereList wl;

	if ( ! where->GetWhereList( wl ) )
		return false;

	CSqlWhere::CWhereList::iterator it;
	string out;
	BSONObjBuilder bson_obj;
	for ( it = wl.begin(); it != wl.end() ; ++ it ) {
		CSqlWhere::_WhereVal &val = * it;

		if ( ! out.empty() ) {
			switch ( val._rel )
			{
			case CSqlWhere::TYPE_OR:
				out += " || ";
				break;
			case CSqlWhere::TYPE_AND:
				out += " && ";
				break;
			default:
				out += " && ";
				break;
			}
		}

		out += "this." + val._skey;

		switch ( val._op )
		{
		case CSqlWhere::OP_EQ: // =
			out += " == " + val._skey;
			bson_obj.append( val._skey, val._sval );
			break;
		case CSqlWhere::OP_NEQ: // <>
			out += " != " + val._sval;
			break;
		case CSqlWhere::OP_MORE: // >
			out += " > " + val._sval;
			break;
		case CSqlWhere::OP_EMORE: // >=
			out += " >= " + val._sval;
			break;
		case CSqlWhere::OP_LESS: // <
			out += " < " + val._sval;
			break;
		case CSqlWhere::OP_ELESS: // <=
			out += " <= " + val._sval;
			break;
		case CSqlWhere::OP_LIKE: // like ''
			out += " / " + val._sval;
			break;
		default:
			break;
		}
	}
	query = Query( "{}" ).where( out, bson_obj.obj() );

	return true;
}
//------------------------------------------------------------------------
//-------------------------------------------------------------------------
bool CMongoDB::Update(const char *stable, const CSqlObj *obj, const CSqlWhere *where)
{
	BSONObjBuilder bson_obj_value_update;
	bson_obj_value_update.append( "$set", ( ( MongoDBSqlObj* ) obj )->GetBsonObj().obj() );
	Query query;

	if ( ! BuildWhere( where, query ) ) {
		return false;
	}
	string sqlbuf = _mongo_db_name + "." + stable;

	/***************************************
	 * liubo 2012-07-25
	 * （1）增加异常处理否则有些异常处理 如SocketException 到来时没有处理，导致程序直接退出
	 * （2）当检测到时Socket Exception时，应该立即重连，这样就增加了实时性。如果依靠超时来保连接的可靠性，实时性不好，而且增加了一个线程做超时检测。
     *  底层检测到的异常信息没有发挥到作用，影响效率。
	 *  ************************************/
    try {
		_conn.update( sqlbuf, query, bson_obj_value_update.obj(), true, true );
	} catch ( DBException & e ) {
		std::string error = e.toString();
		printf( "conn update errro info : %s  \n", error.c_str() );
	}
	return true;
}

//-------------------------------------------------------------------------
bool CMongoDB::Delete(const char *stable, const CSqlWhere *where)
{
	Query query;
	if ( ! BuildWhere( where, query ) ) {
		return false;
	}
	string sqlbuf = _mongo_db_name + "." + stable;

	try {
		_conn.remove( sqlbuf, query );
	} catch ( DBException & e ) {
		std::string error = e.toString();
		printf( "conn delete errro info : %s  \n", error.c_str() );
	}
	
/*
	if (!_conn.getLastError().empty())
	{
		OUT_ERROR(NULL, 0, NULL, "remove mongogdb failed,err_msg : %s\n!!", _conn.getLastError().c_str());
		return false;
	}
*/
	return true;
}
//------------------------------------------------------------------------

/////////////////////////////////// MongoSqlObj //////////////////////////////////////////


BSONObjBuilder & MongoDBSqlObj::GetBsonObj()
{
	return _bson_obj;
}

void MongoDBSqlObj::AddInteger( const string &key, int val)
{
	_bson_obj.append(key, val);
}

void MongoDBSqlObj::AddLongLong(const string &key, long long val)
{
	_bson_obj.append(key, val);
}

void MongoDBSqlObj::AddString( const string &key, const string &val)
{
    _bson_obj.append(key, val);
}

//增加本身，只有在MongoDB中实现。
void MongoDBSqlObj::AddCSqlObj(const string &key, CSqlObj *sql_obj)
{
    _bson_obj.append(key, ((MongoDBSqlObj*)sql_obj)->_bson_obj.obj());
}

// 清理所有數據
void MongoDBSqlObj::ClearObj( void )
{
//	assert( 1 == 0 ) ;  // MongoDB Disable
}

// 序列化接口
void MongoDBSqlObj::Seralize( DataBuffer &buf )
{
	BufBuilder &b = _bson_obj.bb() ;
	buf.writeInt32( b.len() ) ;
	if ( b.len() > 0 ) {
		buf.writeBlock( b.buf() , b.len() ) ;
	}
}

// 反序列化处理
void MongoDBSqlObj::UnSeralize( const char *ptr, int len )
{
	CPacker pack( ptr, len ) ;

	CQString ss ;
	unsigned int nlen = pack.readString( ss ) ;
	if ( nlen == 0 )
		return ;
	// 将Mongo数据还原回原始状态
	_bson_obj.bb().reset() ;
	_bson_obj.bb().appendBuf( ss.GetBuffer(), ss.GetLength() ) ;
}

