/******************************************************
 *  CopyRight: 北京中交兴路科技有限公司(2012-2015)
 *   FileName: proto_save.h
 *     Author: liubo  2012-6-5 
 *Description:
 *******************************************************/

#ifndef PROTO_SAVE_H_
#define PROTO_SAVE_H_

#include <interface.h>
#include "idatapool.h"
#include <map>
#include <string>
#include <vector>
#include "run_info.h"
#include "filecache.h"
#include "dbsave.h"

#define CAR_INFO_TIMEOUT (60 * 5)

using namespace std;

/* 缓冲池的最大长度 100 万条数据*/
#define MAX_DATA_POOL_NUM        (100 * 1000)

#define gps_info_table           "TH_VEHICLE_TRACK"
#define gps_info_dump            "TH_VEHICLE_DUMP"
#define alert_table              "TH_VEHICLE_ALARM"
#define off_online_table         "TH_VEHICLE_ONOFFLINE"
#define driver_action_table      "TH_VEHICLE_DRIVERACTION"
#define engine_load_table        "TH_VEHICLE_ENGINELOAD"
#define alert_log_table          "TH_VEHICLE_ALARM_LOG"
#define down_command_table       "TH_VEHICLE_COMMAND"
#define meadia_table             "TH_VEHICLE_MEDIA"

#define MSG_TERM_UP              0x0100  /* 终端数据上传         */
#define MSG_SET_TERM_PARAM       0x0200  /* 设置终端参数         */
#define MSG_GET_TERM_PARAM       0x0300  /* 获取终端参数        */
#define MSG_CTRL_TERM            0x0300  /* 终端控制                 */
#define MSG_SNDM_DOWN            0x0400  /* 短信下发                 */
#define MSG_PLAT_REQ_TERM        0x0500  /* 平台请求终端数据  */
#define MSG_TERM_REQ_PLAT        0x0600  /* 终端请求平台数据  */
#define MSG_SUBSCRIBE            0x0700  /* 订阅指令                  */
#define MSG_DCALL                0x0800  /* 控制指令回传          */

class InterProto;
class VehicleInfo;

#define DBSTATE_ONLINE    1   // 数据库可用
#define DBSTATE_OFFLINE   0   // 数据库不可用状态
#define DBSTATE_MODIFY    3   // 有改动的
#define DBSTATE_DELETE    4   // 需要删除的

class Inter2SaveConvert : public IOHandler
{
#define MAX_DBTYPE  2  // 最大的数据库类型
	// 数据库对象结构体
	struct DataBaseObj
	{
        DataBaseObj(){}
		DataBaseObj(string v): _dbvalues(v), _dbface(NULL), _last(0),
		_state(DBSTATE_OFFLINE){}

        string _dbvalues ;     // 从redis里面获得
        int _type;             // 数据库的类型
        int _groupid;          // 组ID
		unsigned int _state ;  // 状态是否为可用
		IDBFace     *_dbface;  // 当前可用数据
		time_t		 _last ;   // 最后一次访问的时间
	};

public:
#define max_num 16
	class AreaAlert; 	//区域报警
    class GpsInfo;      //由于GPS信息和报警信息是一块处理的，供给报警字段的使用的gps信息。

	typedef bool (DataBaseSave::*ConvertSqlFun)(InterProto *);

	Inter2SaveConvert() ;
	virtual ~Inter2SaveConvert();

	bool init(IDataPool *save_pool, ISystemEnv *pEnv);
	void stop() ;

	bool convert(InterData *data);

    //数据库的插入操作,调用者由单独的一个线程来执行
	void savedb();

	// 回调处理文件缓存中数据
	int HandleQueue( const char *sid , void *buf, int len , int msgid = 0 ) ;

    // 将数据添加文件队列中
    bool add_pool(IDataPool::DB_TYPE type, IDataPool::DB_OPRE oper, string table, CSqlObj *obj, CSqlWhere* where = NULL, int groupid = 0 );

	//生成测试数据, num 表示车机数目
    /**
	static GpsInfo create_test_gpsinfo(int num = 10000);
    static void create_test_msg(vector<string> &vec);
	*/
private:
	bool get_dbconfig(map<string, DataBaseObj> &map_dbobj);
    bool update_dbconfig();

	bool data2proto(InterData *data,  InterProto *inter_proto);
    bool get_vehicle_info(const char *car_id, VehicleInfo &car_info);

private:
    map<unsigned short, ConvertSqlFun> _map_mongo_fun;

    //要支撑多线程，这个缓冲管理需要加锁。
	share::Mutex  	     		_mutex ;
    share::Monitor  	    	_car_monitor ;
    map<long long, VehicleInfo> _map_car_info;

    CFileCache					_filecache ;
    //又dataserver对其进行控制
    IDataPool         		   *_save_pool;
    RunInfo 					_run_info;
	ISystemEnv 		  	       *_pEnv ;

    map<string, DataBaseObj>   _map_dbobj;
    // 是否停止
    bool 					   _bstop ;
    DataBaseSave               _db_save;
};

class VehicleInfo
{
public:
	//默认五分钟超时
	time_t update_time;

	unsigned int corpid;
    unsigned int gid;
	unsigned char corlor;
	string vechile;
	string termid;
	string oemcode;
	unsigned vid;

	VehicleInfo()
    {
    	update_time = time(0);
    }
};

/* 交给其他模块处理的时候， 先将需要的内容全部取到 */
class InterProto
{
public:
	unsigned short msg_type;
	VehicleInfo vehicle_info;
    long long phone_number;
	map<string, string> kvmap;
	InterData *meta_data;

	InterProto()
	{
        msg_type   = 0xffff;
        phone_number = 0;
	}
};

//区域报警
class Inter2SaveConvert::AreaAlert
{
public:
	  // 位置类型｜区域或线路ID｜方向  | 区域类型|区域线路ID|触发规格|行驶时间
    char isvalid ; // 0表示无效， 1表示有效
    unsigned char area_type;
    unsigned char area_line_id;
    unsigned char trigger;
    unsigned char drive_time;
    AreaAlert() : isvalid(0)
    {
    	area_type = area_line_id = trigger = drive_time = 0;
    }
};

/* ***********************************************
 * 1. 由于GPS信息和报警信息是一块处理的，供给报警字段的使用的gps信息。
 * 2. 这个类放到外面的原因是其他类的测试代码中用到它的定义。
 * ***********************************************/
class Inter2SaveConvert::GpsInfo
{
public:
	Inter2SaveConvert::AreaAlert area_alert;
	long long car_id;
    unsigned int vid;
    unsigned int company_id;
	unsigned int map_lon;
	unsigned int map_lat;
	time_t sys_time;
    int gps_time;
	unsigned int ssp;
	unsigned int sdir;
	unsigned int salt;
	unsigned int smil;
	unsigned int stow;
	string saa; //报警附加信息
};


#endif /* PROTO2SAVE_H_ */
