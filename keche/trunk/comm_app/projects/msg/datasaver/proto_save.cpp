/******************************************************
 *  CopyRight: 北京中交兴路科技有限公司(2012-2015)
 *   FileName: proto_save.cpp
 *     Author: liubo  2012-6-5
 *Description:
 *******************************************************/
#include "proto_save.h"
#include "tools.h"
#include "comlog.h"
#include "mapconvert.h"
#include <sys/time.h>
#include "saverutil.h"
#include <stdlib.h>
#include <list>
#include <string>

using namespace std;
#define LBS_PHONEAREA   "lbs.phone"
#define LBS_DATABASE    "lbs.dbconfig"

static bool redis_value( string data, const char *key, string &value );

// 从指定的数据库类型中读取数据
static const char *g_dbtype[] = { "oracle", "mongo", "mysql", "redis" };
map<string, unsigned short> g_map_msg_type; //命令关键字跟msg type之间的对应关系。

static unsigned short get_msg_type(const char *command)
{
    return g_map_msg_type[command];
}

static string get_dbkey(int type, int groupid)
{
    char buffer[16] = {0};
    sprintf(buffer, "%s-%d", g_dbtype[type], groupid);
    return buffer;
}

static int get_type_index(const char *type)
{
    for(int i = 0 ; i < (int)(sizeof(g_dbtype) / sizeof(char*)); i++)
    {
    	if(strcmp(type, g_dbtype[i]) == 0)
    	{
    		return i;
    	}
    }
    return -1;
}

Inter2SaveConvert::Inter2SaveConvert( ) :
		_filecache( this ), _bstop( false )
{
	_save_pool = NULL;
}

Inter2SaveConvert::~Inter2SaveConvert( )
{
}

bool Inter2SaveConvert::init( IDataPool *save_pool, ISystemEnv *pEnv )
{
	g_map_msg_type.insert(make_pair("U_REPT", MSG_TERM_UP));
	g_map_msg_type.insert(make_pair("D_SETP", MSG_SET_TERM_PARAM));
	g_map_msg_type.insert(make_pair("D_GETP", MSG_GET_TERM_PARAM));

	g_map_msg_type.insert(make_pair("D_CTLM", MSG_CTRL_TERM));
	g_map_msg_type.insert(make_pair("D_SNDM", MSG_SNDM_DOWN));
	g_map_msg_type.insert(make_pair("D_REQD", MSG_PLAT_REQ_TERM));
//	_map_msg_type.insert(make_pair("U_REPT", MSG_TERM_REQ_PLAT));
	g_map_msg_type.insert(make_pair("D_ADDT", MSG_SUBSCRIBE));
	g_map_msg_type.insert(make_pair("D_DELT", MSG_SUBSCRIBE));
	g_map_msg_type.insert(make_pair("D_CALL", MSG_DCALL));

	_map_mongo_fun.insert(make_pair(MSG_TERM_UP | 0,  &DataBaseSave::mongo_gps_info));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP | 7,  &DataBaseSave::mongo_gps_info));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP | 11, &DataBaseSave::mongo_gps_info));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP | 51, &DataBaseSave::mongo_gps_info));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP | 52, &DataBaseSave::mongo_drive_act));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP | 50, &DataBaseSave::mongo_acc_rate));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP | 5,  &DataBaseSave::mongo_off_online));

    //下面这四种指令，都要存储到指令下发表当中
	_map_mongo_fun.insert(make_pair(MSG_SET_TERM_PARAM | 0xff,  &DataBaseSave::oracle_down_command));
	_map_mongo_fun.insert(make_pair(MSG_CTRL_TERM      | 0xff,  &DataBaseSave::oracle_down_command));
	_map_mongo_fun.insert(make_pair(MSG_SNDM_DOWN      | 0xff,  &DataBaseSave::oracle_down_command));
	_map_mongo_fun.insert(make_pair(MSG_DCALL          | 0xff,  &DataBaseSave::oracle_down_command));

	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 3,  &DataBaseSave::oracle_vehicle_multimedia_event));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 39, &DataBaseSave::oracle_vehicle_media));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 33, &DataBaseSave::oracle_vehicle_infoplay));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 35, &DataBaseSave::oracle_vehicle_eticket));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 8,  &DataBaseSave::oracle_vehicle_driver));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 2,  &DataBaseSave::oracle_vehicle_recorder));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 14, &DataBaseSave::oracle_vehicle_compress));

	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 4,  &DataBaseSave::oracle_vehicle_dispatch_msg));
	_map_mongo_fun.insert(make_pair(MSG_SNDM_DOWN      | 1,  &DataBaseSave::oracle_vehicle_dispatch_msg));
	_map_mongo_fun.insert(make_pair(MSG_SNDM_DOWN      | 2,  &DataBaseSave::oracle_vehicle_dispatch_msg));
	_map_mongo_fun.insert(make_pair(MSG_SNDM_DOWN      | 3,  &DataBaseSave::oracle_vehicle_dispatch_msg));
	_map_mongo_fun.insert(make_pair(MSG_SNDM_DOWN      | 4,  &DataBaseSave::oracle_vehicle_dispatch_msg));

	_map_mongo_fun.insert(make_pair(MSG_SET_TERM_PARAM | 14,  &DataBaseSave::oracle_vehicle_area));
	_map_mongo_fun.insert(make_pair(MSG_SET_TERM_PARAM | 15,  &DataBaseSave::oracle_line_vehicle));

	_map_mongo_fun.insert(make_pair(MSG_GET_TERM_PARAM | 14,  &DataBaseSave::oracle_vehicle_area));
	_map_mongo_fun.insert(make_pair(MSG_GET_TERM_PARAM | 15,  &DataBaseSave::oracle_line_vehicle));

	_map_mongo_fun.insert(make_pair(MSG_SET_TERM_PARAM | 0,  &DataBaseSave::oracle_terminal_param));
	_map_mongo_fun.insert(make_pair(MSG_GET_TERM_PARAM | 0,  &DataBaseSave::oracle_terminal_history_param));

	_map_mongo_fun.insert(make_pair(MSG_TERM_UP | 31,  &DataBaseSave::oracle_vehicle_bridge));

	_map_mongo_fun.insert(make_pair(MSG_SET_TERM_PARAM | 9,  &DataBaseSave::oracle_vehicle_bridge));
	_map_mongo_fun.insert(make_pair(MSG_GET_TERM_PARAM | 9,  &DataBaseSave::oracle_vehicle_bridge));

	//提问下发和提问应答
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 32, &DataBaseSave::oracle_question_answer));
	_map_mongo_fun.insert(make_pair(MSG_SNDM_DOWN      | 5,  &DataBaseSave::oracle_question_answer));


//	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 5,  &DataBaseSave::oracle_terminal_history_param));
//	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 5,  &DataBaseSave::oracle_line_vehicle));
//	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 5,  &DataBaseSave::oracle_vehicle_area));
//	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 5,  &DataBaseSave::oracle_terminal_param));
//	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 5,  &DataBaseSave::oracle_terminal_updateinfo));
//	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 5,  &DataBaseSave::oracle_vehicle_media_idx));
//	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 5,  &DataBaseSave::oracle_vehicle_bridge));
	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 14, &DataBaseSave::oracle_vehicle_compress));
//	_map_mongo_fun.insert(make_pair(MSG_TERM_UP        | 5,  &DataBaseSave::oracle_question_answer));

	_save_pool = save_pool;
	_run_info.init();
	_pEnv = pEnv;

    _db_save.init(this);

    if( ! get_dbconfig( _map_dbobj ) ) {
    	return false;
    }

    char buffer[256] = { 0 };
    if ( ! pEnv->GetString( "base_filedir" , buffer ) ) {
		printf( "load base_filedir failed\n" ) ;
		return false ;
	}

	int nvalue = 0 ;
	pEnv->GetInteger( "sendcache_speed", nvalue ) ;

	char temp[1024] = {0} ;
	sprintf( temp, "%s/dbdata", buffer ) ;

	// 初始化文件数据列队
	return _filecache.Init( temp, nvalue );
}

bool Inter2SaveConvert::get_dbconfig(map<string, DataBaseObj> &map_dbobj)
{
	string dbconfig;
    vector<string> dbkeys;
	if (!_pEnv->GetRedisCache()->HKeys( LBS_DATABASE , dbkeys))
	{
        printf("get lbs.dbconfig false \n");
		return false;
	}

	for (int i = 0; i < dbkeys.size(); i++)
	{
		string onedb;
		if (!_pEnv->GetRedisCache()->HGet( LBS_DATABASE , dbkeys[i].c_str(), onedb))
		{
			continue;
		}
		DataBaseObj dbobj(onedb);

		printf("get one dbconfig config from redis %s \n", dbkeys[i].c_str());
        string::size_type pos = dbkeys[i].find("-");
        if(pos != string::npos)
        {
        	dbobj._type = get_type_index(dbkeys[i].substr(0, pos).c_str());
        	dbobj._groupid = atoi(dbkeys[i].substr(pos + 1).c_str());
        	map_dbobj.insert(make_pair(dbkeys[i], dbobj));
        }
	}

	return true;
}

bool Inter2SaveConvert::update_dbconfig()
{
	map<string, DataBaseObj> map_dbobj;
	if(!get_dbconfig(map_dbobj))
	{
		return false;
	}

	map<string, DataBaseObj>::iterator iter;
	map<string, DataBaseObj>::iterator tmp;
	for(iter = map_dbobj.begin(); iter != map_dbobj.end(); ++iter)
	{
	    tmp = _map_dbobj.find(iter->first);
	    if(tmp != _map_dbobj.end())
	    {
	    	//完全相同的两个连接, 删除掉这种重复关系
	    	if(iter->second._dbvalues != tmp->second._dbvalues)
	    	{
	    		OUT_INFO( NULL, 0, "DataBase", "dbconfig modify %s %s", iter->first.c_str(), iter->second._dbvalues.c_str());
                printf("tmp->second._state = DBSTATE_MODIFY; %s %s  \n", iter);
	    		tmp->second._state = DBSTATE_MODIFY;
                tmp->second._dbface = iter->second._dbface; // 修改为新的dbface;
	    	}
	    }
	    else  //找不着说明是新增的, 新增的默认是OFFLINE的
	    {
//          printf("add one dbconfig %s %s \n", iter->first.c_str(), iter->second._dbvalues.c_str());
            OUT_INFO( NULL, 0, "DataBase", "add one dbconfig %s %s", iter->first.c_str(), iter->second._dbvalues.c_str());
            _map_dbobj.insert(make_pair(iter->first, iter->second));
	    }
	}

	for(iter = _map_dbobj.begin(); iter != _map_dbobj.end(); ++iter )
	{
		if(map_dbobj.find(iter->first) == map_dbobj.end())
		{
			OUT_INFO( NULL, 0, "DataBase", "dbconfig delete %s", iter->first.c_str());
			iter->second._state = DBSTATE_DELETE;
		}
	}

	return true;
}

bool Inter2SaveConvert::get_vehicle_info( const char *car_id, VehicleInfo &car_info )
{
    long long phone_number = atoll(car_id);
	_car_monitor.lock();
	map< long long, VehicleInfo >::iterator iter = _map_car_info.find( phone_number );
	if ( iter != _map_car_info.end() ) {
		// 检测是否超时
		if ( time(NULL) - iter->second.update_time < CAR_INFO_TIMEOUT ) {
            car_info = iter->second;
        	_car_monitor.unlock();
            return true;
		}
	}

	_car_monitor.unlock();

	string value;
	if ( ! _pEnv->GetRedisCache()->HGet( LBS_PHONEAREA, car_id, value ) ) {
		return false;
	}

//	"corpid:155527,color:2,vechile:\xb8\xd3C31841,termid:1124087,oem:E001,auth:2656918
	string corpid, gid, corlor, vechile, termid, oemcode, vid;

	redis_value(value, "corpid", corpid );
	redis_value(value, "color", corlor  );
	redis_value(value, "vechile", vechile);
	redis_value(value, "termid", termid);
	redis_value(value, "oemcode", oemcode);
	redis_value(value, "vid", vid);
    redis_value(value, "gid", gid);

    car_info.corpid = atoi(corpid.c_str());
    car_info.corlor = atoi(corlor.c_str());
    car_info.gid = atoi(gid.c_str());
    car_info.vid = atoi(vid.c_str());
    car_info.vechile = vechile;
    car_info.termid = termid;
    car_info.update_time = time(0);

	_car_monitor.lock();
	_map_car_info.erase( phone_number ) ;
	_map_car_info.insert( make_pair( phone_number, car_info ) );
	_car_monitor.unlock();

	return true;
}

bool Inter2SaveConvert::data2proto( InterData *data, InterProto *inter_proto )
{
	inter_proto->meta_data = data;

	const char *mac_id = data->_macid.c_str();
	const char *pos = strstr( mac_id, "_" );
	inter_proto->phone_number = strtoll( pos + 1, NULL, 0 );

	//如果取得不了company_id的话，默认为0处理。
	if ( ! get_vehicle_info( pos + 1, inter_proto->vehicle_info) ) {
		return false;
	}

	//去掉两边的大括号
	const char *content = data->_packdata.c_str();
	list< StrInfo > list_info;

	//content + 1, data->_packdata.length() - 2作用是去掉两边的大括号。
	if ( ! split2list( content + 1, data->_packdata.length() - 2, list_info, "," ) ) {
		return false;
	}
	if ( split2map( list_info, inter_proto->kvmap, ":" ) <= 0 ) {
		return false;
	}

	//获取文件类型
	unsigned short msg_type = get_msg_type(data->_command.c_str());
	msg_type |=  atoi( inter_proto->kvmap["TYPE"].c_str());

	inter_proto->msg_type = msg_type;

	return true;
}

bool Inter2SaveConvert::convert( InterData *data )
{
	InterProto inter_proto;
	if ( ! data2proto( data, & inter_proto ) ) {
		return false;
	}
    bool ret = false;

	ConvertSqlFun  fun = _map_mongo_fun[inter_proto.msg_type | 0xff];
	if(fun != NULL)
	{
		ret = (_db_save.*fun)(&inter_proto);
	}

	fun = _map_mongo_fun[inter_proto.msg_type];
	if(fun != NULL)
	{
		//ret被重复赋值，inter_proto.msg_type | 0xff 和inter_proto.msg_type 只要有一个执行成功，就认为返回结果是正确的。
		(_db_save.*fun)(&inter_proto);
	}

	++ _run_info.recv_total;
	++ _run_info.cur_recv_total;
	return  ret;
}

// 回调处理文件缓存中数据,这里是单线程操作不需要加锁处理
int Inter2SaveConvert::HandleQueue( const char *sid, void *buf, int len , int msgid )
{
	CSqlUnit unit( ( const char * ) buf, len );

	// 数据库不存就不处理了
	if ( unit._dbtype >= MAX_DBTYPE )
		return IOHANDLE_ERRDATA;

    string dbkey = get_dbkey(unit._dbtype, unit._groupid);
    /**
    if( dbkey == "oracle-0" ) {
        printf("Handle oracle data \n");
    }*/
	// 比较数据
	if ( strcmp( dbkey.c_str(), sid ) != 0 ) {
		return IOHANDLE_ERRDATA; // 无效数据
	}

	// 查找需要使用数据库处理对象
	map<string, DataBaseObj>::iterator iter = _map_dbobj.find(dbkey);
	if(iter == _map_dbobj.end() || iter->second._dbface == NULL)
		return IOHANDLE_FAILED;

	IDBFace *pface = iter->second._dbface;

	int state = DBSTATE_ONLINE;
	// 处理数据操作
	if ( unit._sqldata->oper == IDataPool::insert ) {
		int ret = pface->Insert( unit._sqldata->table.c_str(), unit._sqldata->sql_obj );
		if ( ret != 0 ) {
			if ( ret == DB_ERR_SOCK ) {
				/**  如果是因为中间出现了网络问题，将这个链接移除，后面再重新连接  ******/
				state = DBSTATE_OFFLINE;
			}
			++ _run_info.insert_fail_total;
		}
		++ _run_info.insert_total;
		++ _run_info.cur_insert_total;
		// printf( "save data success %s\n", unit._sqldata->table.c_str() ) ;
	} else if ( unit._sqldata->oper == IDataPool::update ) {
		pface->Update( unit._sqldata->table.c_str(), unit._sqldata->sql_obj, unit._sqldata->sql_where );
		++ _run_info.update_total;
		++ _run_info.cur_update_total;
	}

    iter->second._state = state;
    iter->second._last = time(0);

	// 根据数据入库的状态来处理数据是否还要继续处理
	return ( state == DBSTATE_ONLINE ) ? IOHANDLE_SUCCESS : IOHANDLE_FAILED;  // 返回发送数据成功
}

// 处理数据操作
void Inter2SaveConvert::savedb()
{
	time_t lastcheck = 0;
	while (!_bstop)
	{
		time_t now = time(NULL);
		// 每隔三分检测状态
		if (now - lastcheck > 60)
		{
			update_dbconfig(); /*  重新从redis中加载dbconfig 并进行比对 */
            map<string, DataBaseObj>::iterator iter = _map_dbobj.begin();
            for(; iter != _map_dbobj.end(); ) {
            	// 修改或者需要重连数据库
                if (iter->second._state == DBSTATE_OFFLINE || iter->second._state == DBSTATE_MODIFY) {
					_save_pool->Remove(iter->second._dbface);
					iter->second._dbface = _save_pool->CheckOut( (iter->second)._dbvalues.c_str() );
					if (iter->second._dbface != NULL) {
						iter->second._state = DBSTATE_ONLINE;
						iter->second._last = now;
						OUT_INFO( NULL, 0, "DataBase", "success get data instance db %s", iter->second._dbvalues.c_str());
						_filecache.Online(get_dbkey(iter->second._type, iter->second._groupid).c_str());
					}
					++iter;
					continue ;
				}

                // 如果移除数据节点
                if(iter->second._state == DBSTATE_DELETE) {
                	OUT_INFO( NULL, 0, "DataBase", "remove database db %s", iter->second._dbvalues.c_str() ) ;
                	_save_pool->Remove(iter->second._dbface);
                    _map_dbobj.erase(iter++);
                	continue;
                }

                // 如果超过3分钟没有数据处理就断开数据库的连接
                if(now - iter->second._last > 180) {
                	_save_pool->Remove(iter->second._dbface);
                	iter->second._dbface = NULL;
                	iter->second._state = DBSTATE_OFFLINE;
                }
                ++ iter;
            }
			lastcheck = now;

			// 第隔一分钟显示一次统计
			_run_info.show();
		}

		if (!_filecache.Check())
		{
            // printf("_filecache --sleep--------------------------\n");
			sleep(1);
		}
	}
    map<string, DataBaseObj>::iterator iter = _map_dbobj.begin();
    for(; iter != _map_dbobj.end(); ++iter)
    {
    	_save_pool->Remove(iter->second._dbface);
    	iter->second._state = DBSTATE_OFFLINE;
    	iter->second._dbface = NULL;
    }
}

void Inter2SaveConvert::stop( )
{
	_bstop = true;
}

// 添加到数据组中
bool Inter2SaveConvert::add_pool( IDataPool::DB_TYPE dbtype, IDataPool::DB_OPRE oper, string table, CSqlObj *obj,
		CSqlWhere* where /* = NULL */, int groupid /* = 0 */)
{
	CSqlData *data = new CSqlData;
	data->oper = oper;
	data->table = table;
	data->sql_obj = obj;
	data->sql_where = where;

	CSqlUnit unit( dbtype, data, groupid );  // ToDo: 暂时无数据组的概念

	DataBuffer buf;
	unit.Seralize( buf );

	// 将数据序列化的MONOGO中
	return _filecache.WriteCache(get_dbkey(dbtype, groupid).c_str(), buf.getBuffer(), buf.getLength() );
}

/***
Inter2SaveConvert::GpsInfo Inter2SaveConvert::create_test_gpsinfo( int num )
{
	// 13000000001-13000020000
	GpsInfo info;
	info.car_id = 15290424735;
	info.company_id = info.car_id % 100;
	info.map_lon = 69871261;
	info.map_lat = 23971435;
	info.sys_time = time( 0 );
	info.gps_time = time( 0 );
	info.ssp = 10;
	info.sdir = 10;
	info.salt = 12;
	info.stow = 213;
	return info;
}

void Inter2SaveConvert::create_test_msg(vector<string> &vec)
{
    string test_msg[] =
    {
        "CAITS 156692_1352187327_45093 E001_15290424056 0 D_SNDM {TYPE:1,2:x+vNo7O10N3PoqOsxPrS0cajwM283cq7,1:15} \r\n",
        "CAITR 156692_1352187327_45093 E001_15290424056 0 D_SNDM {RET:0} \r\n",
        "CAITS 0_00000_00001 E013_14784324208 0 D_CTLM {TYPE:10,VALUE:1|1|1|0|1|10|128|50|50|128} \r\n",
        "CAITR 0_00000_00001 E013_14784324208 0 D_CTLM {RET:5} \r\n",
        "CAITS 0_220305144 E013_14784324206 0 D_SETP {TYPE:9,91:1,90:AAEQQA0hlvgAAAB9VDIwMTIxMTA3MjA1OTM0Njk1AAAACNaj1t3N8sDvMTExMTExMTExMTExMTExMTExEhEHISg11KVBTDU5NbnSAAAANDM0MjUAAAAAAAAAAAAAAAAAAAAIzvewss3ywO8xMTExMTExMTExMTExMTExMTESEQgIAAASEQchKDU= \r\n",
        "CAITR 0_220305144 E013_14784324206 0 D_SETP {RET:5} \r\n",
        "CAITS 157836_1352187628_24729 E001_13803848421 0 D_CALL {TYPE:0,RETRY:1,1:10,0:10} \r\n",
        "CAITR 156336_1352187588_24725 E001_18012071634 0 D_CALL {RET:5} \r\n",

        "CAITS 153916_1352187820_1002 E001_15255554455 0 D_GETP {TYPE:0}",
        "CAITR 153916_1352187817_1001 E001_15255554455 0 D_GETP {TYPE:0,RET:0,"
        "0:M2M.YUTONG.COM,1:7709,10:13559963589,100:30,101:3,102:30,103:3,104:30,"
        "105:3,106:ZZYTBJ.HA,107:WAP,108:WAP,109:58.83.210.8,110:7709,111:0,112:0,"
        "113:30,114:1800,115:10,116:10,117:500,118:1000,119:1000,120:100,121:45,"
        "122:13559963588,123:13559963500,124:13559963544,125:1,126:600,127:18000,"
        "128:100,129:10,130:14400,131:57600,132:1200,133:0,134:0,135:0,136:5,137:70,"
        "138:50,139:70,140:70,141:,142:0,143:1,144:0,145:0,146:7,147:0,15:13559963533,"
        "180:0,181:0,187:0,3:ZZYTBJ.HA,300:100,301:1800,302:0,303:8,"
        "304:400,305:1|3|5|7|9|11|13|15|17|19|21|23|25|27|29|31|33|35|37|39|41|43,"
        "306:1|3|5|7|9|11|13|15|17|19|21|23|25|27|29|31,307:1|3|5|7|9|11|13|16|18|20|22|23|25|27|29|1|3|5|7|9|11|13|16|18|20|22|23|25|27|29,"
        "308:2,31:0,310:,4:WAP,41:湘C55554,42:2,5:WAP,7:30,9:13999655668} \r\n",


        "CAITS 0_1 E001_15290404429 0 U_REPT {TYPE:0,RET:0,1:62427479,2:18388043,20:0,21:2097157,210:0,213:18296,216:0,24:0,3:0,4:20121106/152936,5:0,500:1024,503:156,504:0,520:28544,6:486,7:0,8:3,9:406706} \r\n",
        //信息点播
        "CAITS 0_1 E001_15290404429 0 U_REPT {TYPE:33,83:10|1} \r\n",
        //电子运单上报
        "CAITS 0_0 E013_14784324206 0 U_REPT {TYPE:35,87:Q00tMTBB} \r\n",
        //驾驶员身份上传
        "CAITS 0_0 E004_13587654321 0 U_REPT {TYPE:8,RESULT:1,110:000000,111:10002101,112:1111111111111111111111111111111111111111,113:上海航盛实业有限公司} \r\n",

        //车辆记录仪
        "CAITS 0_00000_00001 4C54_13901234559 0 U_REPT {TYPE:2,70:5,61:VXoFW2} \r\n",

        //数据透传(自定义的数据格式)
        "CAITS 0_0 E013_14784324206 0 U_REPT {TYPE:31,90:1, 91:Q00tMTBB} \r\n",

        //数据压缩上传(自定义的数据格式)
        "CAITS 0_0 E013_14784324206 0 U_REPT {TYPE:14,90:1, 92:Q00tMTBB, 93:8} \r\n",

        //提问下发
        "CAITS 10206_1352187406_24719 E001_15249677400 0 D_SNDM {TYPE:1,2:4+TB6rfWuavLvszh0NHE+qOsxPrS0bOsy9nQ0Mq7o6zH67z1y9nC/dDQo6E=,1:2} \r\n",

        //提问应答
        "CAITS 10206_1352187406_24719 E001_15249677400 0 U_REPT {TYPE:32,82:id,84:seq} \r\n"

    };
}
*/

static bool redis_value( string data, const char *key, string &value )
{
//  data = "corpid:10791color:2vechile:?.18843termid:3542740oem:E002auth:1331563690432 ";
	const char *d = data.c_str();
	const char *begin = NULL;
	const char *end = NULL;
	int len = data.length();

	if ( ( begin = strstr( d, key ) ) == NULL ) {
		return false;
	}

	if ( begin < d + len )
		begin += strlen( key ) + 1;
	else
		return false;

	if ( ( end = strstr( begin, "," ) ) == NULL ) {
		value.assign( begin, d + len - begin );
	} else {
		value.assign( begin, end - begin );
	}

	return true;
}



