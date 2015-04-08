#include <dbpool.h>

int test_oracle( CDbPool &pool )
{
	IDBFace *db = pool.CheckOut( "type=oracle;ip=192.168.100.53;port=1521;user=kcpt;pwd=kcpt;db=orcl" ) ;
	if ( db == NULL ) {
		printf( "check database failed\n" ) ;
		return -1 ;
	}
	printf( "get dbface id: %u \n", db->GetId() ) ;
	/**
	CSqlWhere *where = new CSqlWhere ;
	CSqlObj *obj = IDataPool::GetSqlObj( IDataPool::oracle ) ;
	obj->AddString( "qb_user", "humingqing" ) ;
	obj->AddString( "qb_pwd" , "qingshui" ) ;
	if ( db->Insert( "qb_test", obj ) == DB_ERR_SUCCESS ) {
		printf( "insert data success\n" ) ;
	}
	obj->ClearObj() ;

	obj->AddString( "qb_user", "humingqing" ) ;
	obj->AddString( "qb_pwd" , "testtest" ) ;

	where->AddWhere( "qb_user", "humingqing" , CSqlWhere::OP_EQ , CSqlWhere::TYPE_AND ) ;
	where->AddWhere( "qb_pwd", "qingshui", CSqlWhere::OP_EQ , CSqlWhere::TYPE_AND ) ;
	if ( db->Update( "qb_test", obj , where ) ) {
		printf( "update data success\n" ) ;
	}
	IDataPool::Release( obj ) ;
	*/

	// 更新车辆的数据的SQL
	/**
	static const char *gvechile_sql = "SELECT sm.commaddr,sm.ent_id,ve.plate_color,utl_raw.cast_to_raw(ve.vehicle_no),tl.tmac,"
			"tl.oem_code,tl.auth_code,ve.vid,(ve.reg_status+1),sm.city "
			"FROM tb_sim sm,tr_serviceunit su,tb_vehicle ve,tb_terminal tl WHERE "
	  "sm.enable_flag = 1 AND tl.enable_flag =1 AND ve.enable_flag =1 AND sm.sid = su.sid AND su.tid = tl.tid AND su.vid = ve.vid AND sm.ent_id=10001" ;
	*/
	//,utl_raw.cast_to_raw(v.vehicle_no)  AND s.ent_id=10001 WHERE s.ENABLE_FLAG = 1 AND t.ENABLE_FLAG =1 AND v.ENABLE_FLAG =1
	static const char *sql = "SELECT sm.commaddr,tl.oem_code,utl_raw.cast_to_raw(ve.vehicle_no)"
			" FROM tb_sim sm,tr_serviceunit su,tb_vehicle ve,tb_terminal tl WHERE "
	  "sm.enable_flag = 1 AND tl.enable_flag =1 AND ve.enable_flag =1 AND sm.sid = su.sid AND su.tid = tl.tid AND su.vid = ve.vid AND sm.ent_id=10001" ;

	CSqlResult *rs = db->Select( sql ) ;
	if ( rs != NULL ) {
		int col = rs->GetColumn() ;
		int row = rs->GetCount() ;

		printf( "get result count %d, column %d\n", row, col ) ;
		/**
		for ( int i = 0; i < row; ++ i ) {
			printf( "result %d: ", i ) ;
			for( int j = 0; j < col; ++ j ) {
				printf( "%d(%s),", j, rs->GetValue( i, j ) ) ;
			}
			printf( "\n" ) ;
		}*/
	} else {
		printf( "get result failed\n" ) ;
	}

	db->FreeResult( rs ) ;
	/**
	where->ClearWhereList() ;
	where->AddWhere( "qb_user", "humingqing" , CSqlWhere::OP_EQ , CSqlWhere::TYPE_AND ) ;
	if ( db->Delete( "qb_test", where ) ) {
		printf( "delete data success\n" ) ;
	}
	delete where ;
	*/
	pool.CheckIn( db ) ;

	return 0 ;
}

int test_mongo( CDbPool &pool )
{
	IDBFace *db = pool.CheckOut( "type=mongo;ip=192.168.5.45;port=27017;user=;pwd=;db=test" ) ;
	if ( db == NULL ) {
		printf( "check database failed\n" ) ;
		return -1 ;
	}
	printf( "get dbface id: %u \n", db->GetId() ) ;


	CSqlObj *obj = IDataPool::GetSqlObj( IDataPool::mongo ) ;
	obj->AddString( "qb_user", "humingqing" ) ;
	obj->AddString( "qb_pwd" , "qingshui" ) ;
	if ( db->Insert( "qb_test", obj ) == DB_ERR_SUCCESS ) {
		printf( "insert data success\n" ) ;
	}
	IDataPool::Release( obj ) ;

	obj = IDataPool::GetSqlObj( IDataPool::mongo ) ;
	obj->AddString( "qb_user", "humingqing" ) ;
	obj->AddString( "qb_pwd" , "testtest" ) ;

	CSqlWhere *where = new CSqlWhere ;
	where->AddWhere( "qb_user", "humingqing" , CSqlWhere::OP_EQ , CSqlWhere::TYPE_AND ) ;
	where->AddWhere( "qb_pwd", "qingshui", CSqlWhere::OP_EQ , CSqlWhere::TYPE_AND ) ;
	if ( db->Update( "qb_test", obj , where ) ) {
		printf( "update data success\n" ) ;
	}
	IDataPool::Release( obj ) ;

	where->ClearWhereList() ;
	where->AddWhere( "qb_user", "humingqing" , CSqlWhere::OP_EQ , CSqlWhere::TYPE_AND ) ;
	if ( db->Delete( "qb_test", where ) ) {
		printf( "delete data success\n" ) ;
	}
	delete where ;

	pool.CheckIn( db ) ;

	return 0 ;
}

int main( int argc, char ** argv )
{
	CDbPool pool ;
	if ( ! pool.Init() ){
		printf( "init database pool failed\n" ) ;
		return -1 ;
	}

	if ( ! pool.Start() ) {
		printf( "start database pool failed\n" ) ;
		return -1 ;
	}

	// test_mongo( pool ) ;
	test_oracle( pool ) ;

	pool.Stop() ;

	return 0 ;
}
