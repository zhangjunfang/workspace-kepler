/******************************************************
 *  CopyRight: 北京中交兴路科技有限公司(2012-2015)
 *   FileName: mongosave.cpp
 *     Author: liubo  2012-10-30 
 *Description:
 *******************************************************/

#include "mongosave.h"
#include "proto_save.h"
#include "tools.h"
#include "comlog.h"
#include "mapconvert.h"
#include <sys/time.h>
#include "saverutil.h"
#include "event_analyse.h"
#include <stdlib.h>
#include <list>
#include <string>

static inline time_t ConvertUtcTime(string str_time);
static inline string lltostr(long long ll);
static inline string inttostr(int i);
static bool redis_value(string data, const char *key, string &value);
static inline string get_save_time(time_t t);

MongoSave::MongoSave()
{
	// TODO Auto-generated constructor stub
}

MongoSave::~MongoSave()
{
	// TODO Auto-generated destructor stub
}

void MongoSave::init(Inter2SaveConvert *inter2save)
{
    _inter2save = inter2save;
}

/*****************************************************
 1. table:
 字段名称	中文对照	别名	字段类型	是否索引	备注
 VID	车辆ID（手机号）	A	长整数	{"A":1} 	A分片
 TO_ONUTC	上线时间utc	B	整数
 TO_OFFUTC	下线utc	C	整数
 TO_FLAG	上下线标志
 1：上线
 0：下线	D	整数
 企业ID	J	整数	{"J":1}

 2. 协议:
 CAITS 0_0 E006_13264034801 0 U_REPT {TYPE:5,18:0/0/0/100001}
 *****************************************************/
bool MongoSave::mongo_off_online(InterProto *inter_proto)
{
	return true;

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::mongo);
	sql_obj->AddLongLong("A", inter_proto->phone_number);
	sql_obj->AddInteger("J", inter_proto->vehicle_info.corpid);
	sql_obj->AddInteger("K", inter_proto->vehicle_info.vid);

	CSqlWhere *sql_where = new CSqlWhere;
	//车辆ID，报警ID，报警开始时间共同决定。
	sql_where->AddWhere("A", lltostr(inter_proto->phone_number),
			CSqlWhere::OP_EQ, CSqlWhere::TYPE_AND);

	const char *data = inter_proto->kvmap["18"].c_str();
	if (data != NULL && data[0] == '1')
	{
		sql_obj->AddInteger("D", 1);
		sql_obj->AddInteger("B", time(0));
	}
	else
	{ //默认是下线
		sql_obj->AddInteger("D", 0);
		sql_obj->AddInteger("C", time(0));
	}

	_inter2save->add_pool(IDataPool::mongo, IDataPool::update, off_online_table,
			sql_obj, sql_where, inter_proto->vehicle_info.gid);

	return true;
}

bool MongoSave::mongo_gps_info(InterProto *inter_proto)
{
	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::mongo);
	long long car_id = inter_proto->phone_number;
	string str_gps_time;
	sql_obj->AddLongLong("A", car_id);

	Inter2SaveConvert::GpsInfo gps_info;
	bool valid = false;
	gps_info.car_id = car_id;
	gps_info.vid = inter_proto->vehicle_info.vid;
	map<string, string>::iterator iter = inter_proto->kvmap.begin();
	unsigned int alert_id = 0;

	for (; iter != inter_proto->kvmap.end(); ++iter)
	{
		int key = atoi(iter->first.c_str());
		switch (key)
		{
		case 1: //不做处理，不然一条数据会插入两次。
			break;
		case 2:
		{
			if (!inter_proto->kvmap["1"].empty()
					&& !inter_proto->kvmap["2"].empty())
			{
				//组成一个键值对。
				CSqlObj *obj = IDataPool::GetSqlObj(IDataPool::mongo);

				int x = atoi(inter_proto->kvmap["1"].c_str());
				int y = atoi(inter_proto->kvmap["2"].c_str());

				obj->AddInteger("X", x);
				obj->AddInteger("Y", y);
				sql_obj->AddCSqlObj("D", obj);
				IDataPool::Release(obj);

				MapConvert map_convert;
				Point *point = map_convert.getEncryPoint(x / 600000.000000,
						y / 600000.000000);
				if (point != NULL)
				{
					CSqlObj *mapobj = IDataPool::GetSqlObj(IDataPool::mongo);
					unsigned int map_lon = (unsigned int) (point->getX()
							* 600000);
					unsigned int map_lat = (unsigned int) (point->getY()
							* 600000);

					mapobj->AddInteger("X", map_lon);
					mapobj->AddInteger("Y", map_lat);
					gps_info.map_lon = map_lon;
					gps_info.map_lat = map_lat;

					sql_obj->AddCSqlObj("C", mapobj);
					IDataPool::Release(mapobj);
					delete point;
				}
			}
			valid = true;
		}
			break;
		case 3: //速度
			gps_info.ssp = atoi(inter_proto->kvmap["3"].c_str());
			sql_obj->AddInteger("E", gps_info.ssp);
			break;
		case 4:
			str_gps_time = inter_proto->kvmap["4"];
			gps_info.gps_time = ConvertUtcTime(inter_proto->kvmap["4"]);
			sql_obj->AddInteger("B", gps_info.gps_time);
			break;
		case 5: //方向
			gps_info.sdir = atoi(inter_proto->kvmap["5"].c_str());
			sql_obj->AddInteger("F", gps_info.sdir);
			break;
		case 6:
			gps_info.salt = atoi(inter_proto->kvmap["6"].c_str());
			sql_obj->AddInteger("G", gps_info.salt);
			break;
		case 7: //记录仪速度
			sql_obj->AddInteger("U", atoi(inter_proto->kvmap["7"].c_str()));
			break;
		case 8: //位置状态位
			sql_obj->AddInteger("P", atoi(inter_proto->kvmap["8"].c_str()));
			break;
		case 9: //里程
			gps_info.smil = atoi(inter_proto->kvmap["9"].c_str());
			sql_obj->AddInteger("L", gps_info.smil);
			break;
		case 19: //车辆信号状态
			sql_obj->AddInteger("Y", atoi(inter_proto->kvmap["19"].c_str()));
			break;
		case 20:
			alert_id = atoi(inter_proto->kvmap["20"].c_str());
			sql_obj->AddInteger("H", alert_id);
			break;
		case 24:
			sql_obj->AddInteger("AB", atoi(inter_proto->kvmap["24"].c_str()));
			break;
		case 31: //要加上的 位置类型|区域或路段ID --> AC {A: 位置类型，B:区域或路段ID }
		{
			const char *content = inter_proto->kvmap["31"].c_str();
			int a, b;
			sscanf(content, "%d|%d", &a, &b);
			CSqlObj *obj = IDataPool::GetSqlObj(IDataPool::mongo);
			obj->AddInteger("A", a);
			obj->AddInteger("B", b);
			sql_obj->AddCSqlObj("AC", obj);
			IDataPool::Release(obj);

		}
			break;
		case 32: //进出区域/路段报警， Q  位置类型｜区域或线路ID｜方向
		{
			const char *content = inter_proto->kvmap["32"].c_str();
			sscanf(content, "%d|%d|%d|%d", &gps_info.area_alert.area_type,
					&gps_info.area_alert.area_line_id,
					&gps_info.area_alert.trigger,
					&gps_info.area_alert.drive_time);
			gps_info.area_alert.isvalid = 1;
			CSqlObj *obj = IDataPool::GetSqlObj(IDataPool::mongo);
			obj->AddInteger("A", gps_info.area_alert.area_type);
			obj->AddInteger("B", gps_info.area_alert.area_line_id);
			obj->AddInteger("C", gps_info.area_alert.trigger);
			obj->AddInteger("D", gps_info.area_alert.drive_time);
			sql_obj->AddCSqlObj("Q", obj);
			IDataPool::Release(obj);
		}
			break;
		case 35: //  行驶路线不足，路段ID｜路段行驶时间｜结果-->｛A:路段ID，B：路段行驶时间，C：结果｝ AD
		{
			const char *content = inter_proto->kvmap["35"].c_str();
			int a, b, c;
			sscanf(content, "%d|%d|%d", &a, &b, &c);
			CSqlObj *obj = IDataPool::GetSqlObj(IDataPool::mongo);
			obj->AddInteger("A", a);
			obj->AddInteger("B", b);
			obj->AddInteger("C", c);
			sql_obj->AddCSqlObj("AD", obj);
			IDataPool::Release(obj);
		}
			break;
		case 210:
			sql_obj->AddInteger("O", atoi(inter_proto->kvmap["210"].c_str()));
			break;
		case 213: //累计油耗
			gps_info.stow = atoi(inter_proto->kvmap["209"].c_str());
			sql_obj->AddInteger("M", gps_info.stow);
			break;
		case 214:
			sql_obj->AddInteger("AG", atoi(inter_proto->kvmap["214"].c_str()));
			break;
		case 216:
			sql_obj->AddInteger("T", atoi(inter_proto->kvmap["216"].c_str()));
			break;
		case 215:
			sql_obj->AddInteger("V", atoi(inter_proto->kvmap["215"].c_str()));
			break;
		case 217:
			sql_obj->AddInteger("AJ", atoi(inter_proto->kvmap["217"].c_str()));
			break;
		case 218: //车速来源
			sql_obj->AddInteger("AA", atoi(inter_proto->kvmap["218"].c_str()));
			break;
		case 318:
		{
			/*
			 油量数据内容（油量类型 0 正常，1 漏油，2 加油 |当前采样值，单位为0.1|采样变化值，单位为0.1|开始时间，UTC|结束时间，UTC）
			 油量数据内容 ｛A:油量类型，B:当前采样值，C:采样变化值，D:开始时间，E:结束时间｝AL
			 */
			const char *content = inter_proto->kvmap["35"].c_str();
			int a, b, c, d, e;
			sscanf(content, "%d|%d|%d|%d|%d", &a, &b, &c, &d, &e);
			CSqlObj *obj = IDataPool::GetSqlObj(IDataPool::mongo);
			obj->AddInteger("A", a);
			obj->AddInteger("B", b);
			obj->AddInteger("C", c);
			obj->AddInteger("D", d);
			obj->AddInteger("E", e);
			sql_obj->AddCSqlObj("AL", obj);
			IDataPool::Release(obj);
		}
			break;
		case 500:
			sql_obj->AddInteger("Y", atoi(inter_proto->kvmap["500"].c_str()));
			break;
		case 503:
			sql_obj->AddInteger("X", atoi(inter_proto->kvmap["503"].c_str()));
			break;
		case 504:
			sql_obj->AddInteger("AE", atoi(inter_proto->kvmap["504"].c_str()));
			break;
		case 505:
			sql_obj->AddInteger("N", atoi(inter_proto->kvmap["505"].c_str()));
			break;
		case 506:
			sql_obj->AddInteger("AF", atoi(inter_proto->kvmap["506"].c_str()));
			break;
		case 507:
			sql_obj->AddInteger("S", atoi(inter_proto->kvmap["507"].c_str()));
			break;
		case 508:
			sql_obj->AddInteger("AH", atoi(inter_proto->kvmap["508"].c_str()));
			break;
		case 509:
			sql_obj->AddInteger("R", atoi(inter_proto->kvmap["509"].c_str()));
			break;
		case 510:
			sql_obj->AddInteger("AI", atoi(inter_proto->kvmap["510"].c_str()));
			break;
		case 511:
			sql_obj->AddInteger("W", atoi(inter_proto->kvmap["511"].c_str()));
			break;
		case 512:
			sql_obj->AddInteger("P", atoi(inter_proto->kvmap["512"].c_str()));
			break;
		case 519:
			sql_obj->AddInteger("AK", atoi(inter_proto->kvmap["519"].c_str()));
			break;
		default:
			break;
		}
	}

	sql_obj->AddInteger("Z", time(0));
	sql_obj->AddInteger("J", inter_proto->vehicle_info.corpid);
	sql_obj->AddInteger("K", inter_proto->vehicle_info.vid);

	//是否是补报
	if(inter_proto->msg_type == MSG_TERM_UP | 7
	    || inter_proto->msg_type == MSG_TERM_UP | 11
	    || inter_proto->msg_type == MSG_TERM_UP | 51)
	{
	    sql_obj->AddInteger("I", 1);
	}
	else
	{
		sql_obj->AddInteger("I", 0);
	}

	//	首先检测下有没有告警事件。

	//  先不处理21的告警。
	if (!inter_proto->kvmap["20"].empty())
	{

	}

	string table;
	if (check_gps_valid(&gps_info) != 0 || !valid)
	{
		table = gps_info_dump;
	}
	else
	{
		//20120429/233607
		table = gps_info_table;

		const char *time = str_gps_time.c_str();
		const char *pos = strstr(time, "/");
		string date(time, pos - time);
		if (pos != NULL)
		{
			table += "_";
			table += date;
		}
	}

	_inter2save->add_pool(IDataPool::mongo, IDataPool::insert, table, sql_obj, NULL,
			inter_proto->vehicle_info.gid);

//	save_alert(&gps_info, protosave::alert808, alert_id, gps_info.gps_time);

	return true;
}

void MongoSave::mongo_alert(void *g, int type,
		unsigned int alert_id, time_t gps_time)
{
	Inter2SaveConvert::GpsInfo *gps_info = (Inter2SaveConvert::GpsInfo *)g;

	list<AlertEvent> list_event = _alert_analyse.check_alert(gps_info->car_id, type, alert_id, gps_time);

	if (list_event.empty())
		return;
	list<AlertEvent>::iterator iter = list_event.begin();
	for (; iter != list_event.end(); ++iter)
	{
		if (iter->event == IDataPool::insert)
		{
			CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::mongo);
			sql_obj->AddLongLong("A", gps_info->car_id);
			sql_obj->AddInteger("J", gps_info->company_id);

			CSqlObj *mapobj = IDataPool::GetSqlObj(IDataPool::mongo);
			mapobj->AddInteger("X", gps_info->map_lon);
			mapobj->AddInteger("Y", gps_info->map_lat);
			sql_obj->AddCSqlObj("C", mapobj);
			IDataPool::Release(mapobj);

			sql_obj->AddInteger("E", gps_info->ssp);
			sql_obj->AddInteger("F", gps_info->sdir);
			sql_obj->AddInteger("G", gps_info->salt);
			sql_obj->AddInteger("L", gps_info->smil);
			sql_obj->AddInteger("M", gps_info->stow);
			sql_obj->AddInteger("B", iter->alert_info.begin_time);
			sql_obj->AddInteger("S", iter->single_alert_id);
			sql_obj->AddInteger("K", gps_info->vid);
			if (gps_info->area_alert.isvalid == 1)
			{
				CSqlObj *obj = IDataPool::GetSqlObj(IDataPool::mongo);

				obj->AddInteger("A", gps_info->area_alert.area_type);
				obj->AddInteger("B", gps_info->area_alert.area_line_id);
				obj->AddInteger("C", gps_info->area_alert.trigger);
				obj->AddInteger("D", gps_info->area_alert.drive_time);
				sql_obj->AddCSqlObj("Q", obj);
				IDataPool::Release(obj);
			}

			if (!gps_info->saa.empty())
			{
				sql_obj->AddString("Q", gps_info->saa);
			}

			_inter2save->add_pool(IDataPool::mongo, IDataPool::insert, alert_table, sql_obj);
		}
		else if (iter->event == IDataPool::update)
		{
			CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::mongo);
			CSqlObj *mapobj = IDataPool::GetSqlObj(IDataPool::mongo);
			mapobj->AddInteger("X", gps_info->map_lon);
			mapobj->AddInteger("Y", gps_info->map_lat);
			sql_obj->AddCSqlObj("EC", mapobj);
			IDataPool::Release(mapobj);

			sql_obj->AddInteger("EE", gps_info->ssp);
			sql_obj->AddInteger("EF", gps_info->sdir);
			sql_obj->AddInteger("EG", gps_info->salt);
			sql_obj->AddInteger("EL", gps_info->smil);
			sql_obj->AddInteger("EM", gps_info->stow);
			sql_obj->AddInteger("EB", iter->alert_info.end_time);
			if (!gps_info->saa.empty())
			{
				sql_obj->AddString("EQ", gps_info->saa);
			}

			CSqlWhere *sql_where = new CSqlWhere;
			//车辆ID，报警ID，报警开始时间共同决定。
			sql_where->AddWhere("A", lltostr(gps_info->car_id),
					CSqlWhere::OP_EQ, CSqlWhere::TYPE_AND);
			sql_where->AddWhere("S", inttostr(iter->single_alert_id),
					CSqlWhere::OP_EQ, CSqlWhere::TYPE_AND);
			sql_where->AddWhere("B", inttostr(iter->alert_info.begin_time),
					CSqlWhere::OP_EQ, CSqlWhere::TYPE_AND);

			_inter2save->add_pool(IDataPool::mongo, IDataPool::update, alert_table, sql_obj,
					sql_where);
		}
		else
		{

		}
	}
}

bool MongoSave::mongo_alert(InterProto *inter_proto)
{
	//不实现。
	return true;
}

//CAITS 0_0 E001_15290424185 0 U_REPT {TYPE:52,516:3|[16779492][66113976][122][652][21][20120430/083038]|[16780593][66114453][121][620][21][20120430/083050]}
bool MongoSave::mongo_drive_act(InterProto *inter_proto)
{
	int lon = 0;
	int lat = 0;
	int speed = 0;
	int alt = 0;
	int dir = 0;
	char gps_time[32] = { 0 };

	vector<string> vec;
	splitvector(inter_proto->kvmap["516"], vec, "|", 0);
	if (vec.size() != 3)
		return false;
	int driver_id = atoi(vec[0].c_str());

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::mongo);

	sscanf(vec[1].c_str(), "[%d][%d][%d][%d][%d][%s]", &lon, &lat, &speed, &alt,
			&dir, gps_time);
	long long car_id = inter_proto->phone_number;
	sql_obj->AddLongLong("A", car_id);
	sql_obj->AddInteger("B", ConvertUtcTime(gps_time));

	CSqlObj *mapobj = IDataPool::GetSqlObj(IDataPool::mongo);
	mapobj->AddInteger("X", lon);
	mapobj->AddInteger("Y", lat);
	sql_obj->AddCSqlObj("C", mapobj);
	IDataPool::Release(mapobj);

	sql_obj->AddInteger("D", driver_id);
	sql_obj->AddInteger("E", speed);
	sql_obj->AddInteger("F", dir);
	sql_obj->AddInteger("EG", alt);

	sscanf(vec[2].c_str(), "[%d][%d][%d][%d][%d][%s]", &lon, &lat, &speed, &alt,
			&dir, gps_time);
	sql_obj->AddInteger("EB", ConvertUtcTime(gps_time));

	CSqlObj *mapobj1 = IDataPool::GetSqlObj(IDataPool::mongo);
	mapobj1->AddInteger("X", lon);
	mapobj1->AddInteger("Y", lat);
	sql_obj->AddCSqlObj("EC", mapobj1);
	IDataPool::Release(mapobj1);

	sql_obj->AddInteger("ED", driver_id);
	sql_obj->AddInteger("EE", speed);
	sql_obj->AddInteger("EF", dir);
	sql_obj->AddInteger("EG", alt);

	sql_obj->AddInteger("K", inter_proto->vehicle_info.vid);
	sql_obj->AddInteger("O", time(0));

	string table = driver_action_table;
	const char *pos = strstr(gps_time, "/");
	if (pos != NULL)
	{
		string date(gps_time, pos - gps_time);
		table += "_";
		table += date;
	}

	_inter2save->add_pool(IDataPool::mongo, IDataPool::insert, table, sql_obj, NULL,
			inter_proto->vehicle_info.gid);

	return true;
}

/******************************s*******************************
 CAITS 0_0 E001_15290421974 0 U_REPT {TYPE:50,513:1,514:}
 车辆ID（手机号）	A	整数	{"A":1}	A，Z分片
 负荷数据	B	字符串		Base64转换
 系统时间	Z	整数	{"Z":1}
 **************************************************************/
bool MongoSave::mongo_acc_rate(InterProto *inter_proto)
{
	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::mongo);

	long long car_id = inter_proto->phone_number;
	sql_obj->AddLongLong("A", car_id);
	sql_obj->AddInteger("K", inter_proto->vehicle_info.vid);
	sql_obj->AddString("B", inter_proto->kvmap["513"]);
	sql_obj->AddInteger("Z", time(0));

	string table = engine_load_table;
	table += "_";
	table += get_save_time(time(0));

	_inter2save->add_pool(IDataPool::mongo, IDataPool::insert, table, sql_obj, NULL,
			inter_proto->vehicle_info.gid);

	return true;
}

/*******************************
 Map中KEY值：
 主键，自增序号	A	整数
 车辆报警处理流水表主键 	B	字符串
 报警操作人登陆名	C	整数
 ********************************/
bool MongoSave::mongo_alert_handle(InterProto *inter_proto)
{
	return true;
}

int MongoSave::check_gps_valid(void *gps_info)
{
	Inter2SaveConvert::GpsInfo *g = (Inter2SaveConvert::GpsInfo *)gps_info;
	if ( g->map_lon < 43200000 || g->map_lon > 81600000 ) { // 经度范围72-136(43200000-81600000)
		return 1;
	} else if ( g->map_lat < 10800000 || g->map_lat > 32400000 ) { // 纬度范围18-54(10800000-32400000)
		return 2;
	} else if ( abs( g->gps_time / 1000 - time( 0 ) / 1000 ) > 86400 ) { // 定位时间与当前服务器系统时间差不超过24小时
		return 3;
	} else if ( g->ssp < 0 || g->ssp > 1600 ) { // 车辆速度0~1600(单位：0.1km/h)
		return 4;
	} else if ( g->sdir < 0 || g->sdir > 360 ) { // 行驶方向0~360
		return 5;
	} else { // 合法
		return 0;
	}
}

static time_t ConvertUtcTime(string str_time)
{
	//20120429/233607
	if (str_time.length() != 15)
	{
		return 0;
	}

	char buffer[8] = { 0 };
	const char *char_time = str_time.c_str();
	struct tm utc_time;
	strncpy(buffer, &char_time[0], 4);
	utc_time.tm_year = atoi(buffer) - 1900;
	memset(buffer, 0, sizeof(buffer));
	strncpy(buffer, &char_time[4], 2);
	utc_time.tm_mon = atoi(buffer) - 1;
	strncpy(buffer, &char_time[6], 2);
	utc_time.tm_mday = atoi(buffer);
	strncpy(buffer, &char_time[9], 2);
	utc_time.tm_hour = atoi(buffer);
	strncpy(buffer, &char_time[11], 2);
	utc_time.tm_min = atoi(buffer);
	strncpy(buffer, &char_time[13], 2);
	utc_time.tm_sec = atoi(buffer);

	return kernel_mktime(&utc_time);
}

static string lltostr(long long ll)
{
	char buffer[32] = { 0 };
	sprintf(buffer, "%lld", ll);
	return buffer;
}

static string inttostr(int i)
{
	char buffer[32] = { 0 };
	sprintf(buffer, "%d", i);
	return buffer;
}

static bool redis_value(string data, const char *key, string &value)
{
//    data = "corpid:10791color:2vechile:?.18843termid:3542740oem:E002auth:1331563690432 ";
	const char *d = data.c_str();
	const char *begin = NULL;
	const char *end = NULL;
	int len = data.length();

	if ((begin = strstr(d, key)) == NULL)
	{
		return false;
	}

	if (begin < d + len)
		begin += strlen(key) + 1;
	else
		return false;

	if ((end = strstr(begin, ",")) == NULL)
	{
		value.assign(begin, d + len - begin);
	}
	else
	{
		value.assign(begin, end - begin);
	}

	return true;
}

static string get_save_time(time_t t)
{
	time(&t);
	struct tm *tm = localtime(&t);
	char buffer[32] = { 0 };
	snprintf(buffer, sizeof(buffer), "%04d%02d%02d", tm->tm_year + 1900,
			tm->tm_mon + 1, tm->tm_mday);
	return buffer;
}

