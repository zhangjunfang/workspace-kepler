/******************************************************
 *  CopyRight: 北京中交兴路科技有限公司(2012-2015)
 *   FileName: oraclesave.cpp
 *     Author: liubo  2012-10-24 
 *Description:
 *******************************************************/

#include "oraclesave.h"
#include "./proto_save.h"
#include "idatapool.h"
#include "mapconvert.h"
#include "tools.h"

OracleSave::OracleSave()
{
	// TODO Auto-generated constructor stub
}

OracleSave::~OracleSave()
{
	// TODO Auto-generated destructor stub
}

string itostring(int i)
{
	char buffer[16] = { 0 };
	sprintf(buffer, "%d", i);
	return buffer;
}

string lltostring(long long i)
{
	char buffer[16] = { 0 };
	sprintf(buffer, "%lld", i);
	return buffer;
}

string get_user_from_seq(string &seq)
{
	vector<string> vec_seq;
	splitvector(seq, vec_seq, "_", 0);
	return vec_seq[0];
}

/**********************************************************************
 OP_ID	NUMBER(15,0)	Yes	1	用户编号	1
 VEHICLE_NO	VARCHAR2(40 BYTE)	Yes	2	车牌号	京A63753
 CO_SUTC	NUMBER(15,0)	Yes	3	发送时间utc	1321946554065
 CO_TYPE	VARCHAR2(30 BYTE)	Yes	4	指令类型Call	D_CTLM
 CO_FROM	NUMBER(2,0)	Yes	5	指令来源（0本平台 1监管平台）	0
 CO_SEQ	VARCHAR2(100 BYTE)	No	6	主键 业务序列号	1_1321946554_63
 CO_CHANNEL	VARCHAR2(20 BYTE)	Yes	7	通讯方式	0
 CO_PARM	VARCHAR2(1000 BYTE)	Yes	8	指令参数，键值对形式	TYPE:10,RETRY:1,VALUE:1|1|1|0|640*480|100|100|100|100|100
 CO_COMMAND	VARCHAR2(1000 BYTE)	Yes	9	原始指令字符串	CAITS 1_1321946554_63 4C54_15313563753 0 D_CTLM {TYPE:10,RETRY:1,VALUE:1|1|1|0|640*480|100|100|100|100|100}
 CO_STATUS	NUMBER(2,0)	Yes	10	发送状态-1等待回应  0:成功	0
 CR_RESULT	VARCHAR2(1000 BYTE)	Yes	11	指令回应结果描述	RET:0
 CR_TIME	NUMBER(15,0)	Yes	12	响应时间utc(s)	1321946554610
 CO_OEMCODE	VARCHAR2(20 BYTE)	Yes	13	终端类型代码	4C54
 CO_SENDTIMES	NUMBER(2,0)	Yes	14	已发送次数	1
 CO_TRYTIMES	NUMBER(2,0)	Yes	15	尝试次数	1
 CO_SUBTYPE	VARCHAR2(20 BYTE)	Yes	16	指令子类型	10
 CREATE_BY	VARCHAR2(20 BYTE)	Yes	17	创建人	1
 CREATE_TIME	NUMBER(15,0)	Yes	18	创建时间	1321946554065
 VID	NUMBER(15,0)	Yes	19	车辆编号	104
 CO_TEXT	VARCHAR2(500 BYTE)	Yes	20	指令页面显示内容	抓拍指令
 AUTO_ID	NUMBER(15,0)	No	21	自增序列	5855
 UPDATE_BY	VARCHAR2(20 BYTE)	Yes	22	更新用户	null
 UPDATE_TIME	NUMBER(15,0)	Yes	23	更新时间	null
 AREA_ID	VARCHAR2(32 BYTE)	Yes	24	省域编码	null

测试结果： insert into TH_VEHICLE_COMMAND(AUTO_ID,CO_CHANNEL,CO_COMMAND,CO_FROM,CO_OEMCODE,CO_PARM,CO_SENDTIMES,CO_SEQ,CO_STATUS,CO_SUBTYPE,CO_TRYTIMES,CO_TYPE,CREATE_BY,CREATE_TIME,OP_ID,VEHICLE_NO,VID)values(seq_auto_id.nextval,'CAITS','',0,'','TYPE:24,RETRY:0,VALUE:-1}',1,'10206_1352190205_45224',0,'',1,'0','10206',1352190440,10206,'湘N41799',529), result 0,Execute,oracledb.cpp:95
 **********************************************************************/

bool OracleSave::oracle_down_command(InterProto *inter_proto)
{
    // printf("into OracleSave::oracle_down_command \n");
	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	string command; //原始指令字符串

	sql_obj->AddInteger("OP_ID",
			atoi(get_user_from_seq(inter_proto->meta_data->_seqid).c_str()));
	sql_obj->AddString("VEHICLE_NO", inter_proto->vehicle_info.vechile);
	sql_obj->AddLongLong("CO_SUTC", time(0) * 1000);
	sql_obj->AddString("CO_TYPE", inter_proto->meta_data->_cmtype);
	sql_obj->AddInteger("CO_FROM", 0);
	sql_obj->AddString("CO_SEQ", inter_proto->meta_data->_seqid);
	sql_obj->AddString("CO_CHANNEL", inter_proto->meta_data->_transtype);
	sql_obj->AddString("CO_PARM", inter_proto->meta_data->_packdata.substr(1,inter_proto->meta_data->_packdata.length() - 1));
    sql_obj->AddVar("AUTO_ID", "seq_auto_id.nextval");
	sql_obj->AddString("CO_COMMAND", command);
	sql_obj->AddInteger("CO_STATUS", 0); //处于等待回应状态。
//	sql_obj->AddString("CR_RESULT", command);
//	sql_obj->AddString("CR_TIME", command); //	NUMBER(15,0)	Yes	12	响应时间utc(s)	1321946554610
	sql_obj->AddString("CO_OEMCODE", inter_proto->vehicle_info.oemcode);
	sql_obj->AddInteger("CO_SENDTIMES", 1);
	sql_obj->AddInteger("CO_TRYTIMES", 1);
	sql_obj->AddString("CO_SUBTYPE", inter_proto->kvmap["type"]);
	sql_obj->AddString("CREATE_BY",
    get_user_from_seq(inter_proto->meta_data->_seqid));
	sql_obj->AddInteger("CREATE_TIME", time(0));
	sql_obj->AddInteger("VID", inter_proto->vehicle_info.vid);
//	TODO sql_obj->AddString("CO_TEXT", command);

//	sql_obj->AddString("UPDATE_BY", command);
//	sql_obj->AddInteger("UPDATE_TIME", command); //暂时不填，由指令回复的时候填写
//	sql_obj->AddString("AREA_ID	", command);

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TH_VEHICLE_COMMAND, sql_obj);

	return true;
}

/*******************************
 //U_REPT 0x0800多媒体事件信息上传{TYPE:39,120:多媒体数据ID,121:多媒体类型,122:多媒体格式编码,123:事件项编码,124:通道ID}

 PID	VARCHAR2(100 BYTE)	No	1	表唯一标识	723e4df3-0790-4572-a458-cc2ab7178dee
 VID	VARCHAR2(32 BYTE)	No	2	车辆标识	233723
 MULTIMEDIA_TYPE	NUMBER(1,0)	Yes	3	多媒体类型 0:图像，1：音频，2：视频	0
 MULTIMEDIA_FORMAT	NUMBER(1,0)	Yes	4	多媒体格式 0 JPEG 1: TIF; 2: MP3; 3: WAV; 4: WMV	0
 EVENT_NUM	NUMBER(1,0)	Yes	5	事件编码0 平台下发 1 定时动作 2 抢劫报警 3 碰撞侧翻报警触发	2
 CHANNEL_ID	NUMBER(2,0)	Yes	6	通道标识	1

测试结果： insert into TH_VEHICLE_MULTIMEDIA_EVENT(CHANNEL_ID,EVENT_NUM,MULTIMEDIA_FORMAT,MULTIMEDIA_TYPE,PID,VID)values(2,1,0,0,sys_guid(),'319')
 ******************************/

bool OracleSave::oracle_vehicle_multimedia_event(InterProto *inter_proto)
{
    // printf("into OracleSave::oracle_vehicle_multimedia_event \n");
    CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);

    //自己生成一个guid的函数，还是让数据库自己生成。
	sql_obj->AddVar("PID", "sys_guid()");
	sql_obj->AddString("VID", itostring(inter_proto->vehicle_info.vid));
	sql_obj->AddInteger("MULTIMEDIA_TYPE",
			atoi(inter_proto->kvmap["121"].c_str()));
	sql_obj->AddInteger("MULTIMEDIA_FORMAT",
			atoi(inter_proto->kvmap["122"].c_str()));
	sql_obj->AddInteger("EVENT_NUM", atoi(inter_proto->kvmap["123"].c_str()));
	sql_obj->AddInteger("CHANNEL_ID", atoi(inter_proto->kvmap["124"].c_str()));

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TH_VEHICLE_MULTIMEDIA_EVENT, sql_obj);

	return true;
}

/*
 MEDIA_ID	VARCHAR2(100 BYTE)	No	1	多媒体编号，自增序列	4e569d30-e920-4af0-9818-4b4764cd5644
 VID	NUMBER(15,0)	No	2	车辆ID	736
 DEVICE_NO	VARCHAR2(20 BYTE)	Yes	3	手机号码	15290424768
 MTYPE_CODE	VARCHAR2(20 BYTE)	Yes	4	多媒体类型	0
 MFORMAT_CODE	VARCHAR2(20 BYTE)	Yes	5	多媒体格式	0
 EVENT_TYPE	VARCHAR2(20 BYTE)	Yes	6	事件项编码 参见多媒体事件项编码表	7
 UTC	NUMBER(15,0)	Yes	7	多媒体上传时间UTC	1.35003E+12
 MEDIA_URI	VARCHAR2(200 BYTE)	Yes	8	多媒体URL	2012/10/12/20121012164618-E001_15290424768-2537-7-1-0-0.jpeg
 LENS_NO	VARCHAR2(10 BYTE)	Yes	9	通道号	1
 FILE_SIZE	NUMBER(10,0)	Yes	10	多媒体文件大小字节	NULL
 DIMENSION	VARCHAR2(20 BYTE)	Yes	11	图片尺寸规格(1:320x240, 2:640x480, 3:800x600, 4:1024x768)	NULL
 FILE_TYPE	VARCHAR2(20 BYTE)	Yes	12	文件类型 1:jpg;2:gif;3:tiff;4:其它	NULL
 SAMPLE_RATE	NUMBER(5,0)	Yes	13	音频采样频率(音频类多媒体信息需要)	NULL
 LAT	NUMBER	Yes	14	纬度（单位：十万分之一度）	16533939
 LON	NUMBER	Yes	15	经度（单位：十万分之一度）	65969722
 MAPLON	NUMBER	Yes	16	地图偏移后GPS经度	65972445
 MAPLAT	NUMBER	Yes	17	地图偏移后GPS纬度	16531874
 ELEVATION	NUMBER(10,0)	Yes	18	海拔高度（单位：米）	249
 DIRECTION	NUMBER(10,0)	Yes	19	方向（单位：度）	238
 GPS_SPEED	NUMBER(10,0)	Yes	20	速度(单位：米/小时)	0
 STATUS_CODE	VARCHAR2(200 BYTE)	Yes	21	状态信息, 多值用逗号分隔	3
 ALARM_CODE	VARCHAR2(200 BYTE)	Yes	22	报警信息，多值用逗号分隔	NULL
 SYSUTC	NUMBER(15,0)	Yes	23	入库时间utc	NULL
 IS_OVERLOAD	NUMBER(2,0)	Yes	24	是否超载(0 否 1 是)	0
 EVENT_STATUS	NUMBER(2,0)	Yes	25	事件状态（0 成功1 失败 2执行中）	NULL
 ENABLE_FLAG	VARCHAR2(2 BYTE)	Yes	26	有效标记 1:有效 0:无效 默认为1	1
 SEQ	VARCHAR2(100 BYTE)	Yes	27	SEQ指令唯一的标识码	NULL
 SEND_USER	NUMBER(10,0)	Yes	28	发送人ID	NULL
 EVENTID	VARCHAR2(10 BYTE)	Yes	29	0：平台下发指令，1：定时动作，2：抢劫报警触发，3：碰撞侧翻报警触发，4：门开拍照，5：门关拍照，6：车门由开变关，时速从＜20公里超过20公里	NULL
 MEMO	VARCHAR2(500 BYTE)	Yes	30	备注	NULL
 MULT_MEDIA_ID	VARCHAR2(100 BYTE)	Yes	31	多媒体数据ID（合规新加)	2537

 //U_REPT 0x0801多媒体数据上传{TYPE:3,120:多媒体数据ID,121:多媒体类型,122:多媒体格式编码,123:事件项编码,124:通道ID ,
 * 125:url地址,1:经度,2:纬度,3:速度,4:时间,5:方向,6:海拔,20:报警标志,8:基本位置信息
insert into TH_VEHICLE_MEDIA(ALARM_CODE,DEVICE_NO,DIRECTION,ELEVATION,ENABLE_FLAG,EVENT_TYPE,GPS_SPEED,LAT,LENS_NO,LON,MEDIA_ID,MEDIA_URI,MFORMAT_CODE,MTYPE_CODE,VID)values('','15294614042',0,0,'1','4',0,0,'4',0,'939','','0','0',140150)
 */
bool OracleSave::oracle_vehicle_media(InterProto *inter_proto)
{
    // printf("into OracleSave::oracle_vehicle_media \n");

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);

	sql_obj->AddString("MEDIA_ID", inter_proto->kvmap["120"]);
	sql_obj->AddInteger("VID", inter_proto->vehicle_info.vid);
	sql_obj->AddString("DEVICE_NO", lltostring(inter_proto->phone_number));
	sql_obj->AddString("MTYPE_CODE", inter_proto->kvmap["121"]);
	sql_obj->AddString("MFORMAT_CODE", inter_proto->kvmap["122"]);
	sql_obj->AddString("EVENT_TYPE", inter_proto->kvmap["123"]);
	sql_obj->AddLongLong("UTC", time(0) * 1000);
	sql_obj->AddString("MEDIA_URI", inter_proto->kvmap["125"]);
	sql_obj->AddString("LENS_NO", inter_proto->kvmap["124"]);
//  下面三条在传上的数据中都没有
//    sql_obj->AddInteger("FILE_SIZE", );
//    sql_obj->AddString("DIMENSION", );
//    sql_obj->AddString("FILE_TYPE", );
//    sql_obj->AddInteger("SAMPLE_RATE", );
	sql_obj->AddInteger("LAT", atoi(inter_proto->kvmap["2"].c_str()));
	sql_obj->AddInteger("LON", atoi(inter_proto->kvmap["1"].c_str()));
//    sql_obj->AddInteger("MAPLON", );
//    sql_obj->AddInteger("MAPLAT", );
	sql_obj->AddInteger("ELEVATION", atoi(inter_proto->kvmap["6"].c_str()));
	sql_obj->AddInteger("DIRECTION", atoi(inter_proto->kvmap["5"].c_str()));
	sql_obj->AddInteger("GPS_SPEED", atoi(inter_proto->kvmap["3"].c_str()));
//    sql_obj->AddString("STATUS_CODE", );
	sql_obj->AddString("ALARM_CODE", inter_proto->kvmap["20"]);
	sql_obj->AddLongLong("SYSUTC", time(0) * 1000);
//    sql_obj->AddInteger("IS_OVERLOAD", );
//    sql_obj->AddInteger("EVENT_STATUS", );
	sql_obj->AddString("ENABLE_FLAG", "1");

//    sql_obj->AddString("SEQ", );
//    sql_obj->AddInteger("SEND_USER", );
//    sql_obj->AddString("EVENTID", );
//    sql_obj->AddString("MEMO", );
//TODO  多媒体ID是如何获得的    sql_obj->AddString("MULT_MEDIA_ID", );

	int x = atoi(inter_proto->kvmap["1"].c_str());
	int y = atoi(inter_proto->kvmap["2"].c_str());

	MapConvert map_convert;
	Point *point = map_convert.getEncryPoint(x / 600000.000000, y / 600000.000000);
	if (point != NULL)
	{
		unsigned int map_lon = (unsigned int) (point->getX() * 600000);
		unsigned int map_lat = (unsigned int) (point->getY() * 600000);
		sql_obj->AddInteger("MAPLON", map_lon);
		sql_obj->AddInteger("MAPLAT", map_lat);
	}

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TH_VEHICLE_MEDIA, sql_obj);

	return true;
}

/*
 PID	VARCHAR2(64 BYTE)	No	1	主键	958eeb24-5c04-4b8c-bad4-397cfe9aaafb
 VID	NUMBER(15,0)	Yes	2	车辆ID	211262
 TYPE	VARCHAR2(16 BYTE)	Yes	3	信息类型	1
 UTC	NUMBER(15,0)	Yes	4	上传时间	1.35061E+12
 STATUS	NUMBER(1,0)	Yes	5	状态	0
 U_REPT 0x0303信息点播/取消{TYPE:33,83:信息类型|*}(0:取消,1:点播)
 */
bool OracleSave::oracle_vehicle_infoplay(InterProto *inter_proto)
{
    // printf("into OracleSave::oracle_vehicle_infoplay \n");

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	sql_obj->AddVar("PID", "sys_guid()");
	sql_obj->AddInteger("VID", inter_proto->vehicle_info.vid);
	sql_obj->AddString("DEVICE_NO", lltostring(inter_proto->phone_number));

	char type[16] = {0};
	int state = 0;
	sscanf(inter_proto->kvmap["83"].c_str(), "%s|%d", type, &state);
	sql_obj->AddString("TYPE", type);
	sql_obj->AddLongLong("UTC", time(0) * 1000);
	sql_obj->AddInteger("STATUS", state); //状态默认成0

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TH_VEHICLE_INFOPLAY, sql_obj);

	return true;
}

/*
 PID	VARCHAR2(64 BYTE)	No	1	主键	1418babe-052e-46a0-898d-a403790e5ff2
 VID	NUMBER(15,0)	Yes	2	车辆ID	141141
 CONTENT	VARCHAR2(1024 BYTE)	Yes	3	电子运单内容	电子运单数据：1111
 UTC	NUMBER(15,0)	Yes	4	上传时间	1335520953619
 */

//U_REPT 0x0701电子运单上报{TYPE:35,87:BASE64编码}
bool OracleSave::oracle_vehicle_eticket(InterProto *inter_proto)
{
    // printf("into OracleSave::oracle_vehicle_eticket \n");

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);

	sql_obj->AddVar("PID", "sys_guid()");
	sql_obj->AddInteger("VID", inter_proto->vehicle_info.vid);
	sql_obj->AddString("CONTENT", inter_proto->kvmap["87"]);
	sql_obj->AddLongLong("UTC", time(0) * 1000);

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TH_VEHICLE_ETICKET, sql_obj);

	return true;
}

/******************************************************
 PID	VARCHAR2(100 BYTE)	No	1	表唯一标识	8ac0d1a3-90c7-4dad-8f75-ca5c4d60e077
 VID	VARCHAR2(32 BYTE)	No	2	车辆标识	140128
 DRIVER_NAME	VARCHAR2(64 BYTE)	Yes	3	驾驶员姓名	雷恒刚
 DRIVER_NO	VARCHAR2(64 BYTE)	Yes	4	驾驶员身份证编码	NULL
 DRIVER_CERTIFICATE	VARCHAR2(128 BYTE)	Yes	5	驾驶员从业资格证	141002727380
 CERTIFICATE_AGENCY	VARCHAR2(256 BYTE)	Yes	6	发证机构	临汾市出租汽车管理处
 UTC	NUMBER(15,0)	Yes	7	上传时间	1331606508968
 STATUS	NUMBER(1,0)	Yes	8	驾驶员身份识别状态，0识别成功，-1识别失败	1
 UP_STATUS	NUMBER(1,0)	Yes	9	上报状态1.上线 2.下线	NULL
 ******************************************************/
//U_REPT 0x0702驾驶员身份采集上传{TYPE:8,RESLUT:结果,110:驾驶员姓名,111:驾驶员编码,112:从业资格证编码,113:发证机构名称}
bool OracleSave::oracle_vehicle_driver(InterProto *inter_proto)
{
    // printf("OracleSave::oracle_vehicle_driver \n");

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	sql_obj->AddVar("PID", "sys_guid()");
	sql_obj->AddInteger("VID", inter_proto->vehicle_info.vid);
	sql_obj->AddString("DRIVER_NAME", inter_proto->kvmap["110"]);
	sql_obj->AddString("DRIVER_NO", inter_proto->kvmap["111"]);
	sql_obj->AddString("DRIVER_CERTIFICATE", inter_proto->kvmap["112"]);
	sql_obj->AddString("CERTIFICATE_AGENCY", inter_proto->kvmap["113"]);
	sql_obj->AddLongLong("UTC", time(0) * 1000);
	sql_obj->AddInteger("STATUS", 0); //默认识别成功

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TH_VEHICLE_DRIVER, sql_obj);

    return true;
}

/*
 PID	VARCHAR2(64 BYTE)	No	1	主键	2bbb1d4f-a17d-41d0-a153-056e22a68c29
 VID	NUMBER(15,0)	Yes	2	车辆ID	10238
 CONTENT	VARCHAR2(3500 BYTE)	Yes	3	行驶记录仪内容	###########(BASE64的数据内容）
 UTC	NUMBER(15,0)	Yes	4	上传时间	1332983923410
 ISPARSE	NUMBER(2,0)	Yes	5	?????1???0??	1
 PARSENUM	NUMBER(2,0)	Yes	6	解析次数,超过3次未解析成功则不再解析	1
 CO_SEQ	VARCHAR2(100 BYTE)	Yes	7	seq	NULL

 车辆行驶记录仪数据(70-99)
 */
bool OracleSave::oracle_vehicle_recorder(InterProto *inter_proto)
{
    //printf("OracleSave::oracle_vehicle_recorder \n");

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);

	sql_obj->AddVar("PID", "sys_guid()");
	sql_obj->AddInteger("VID", inter_proto->vehicle_info.vid);
	sql_obj->AddString("CONTENT", inter_proto->kvmap["61"]);
	sql_obj->AddLongLong("UTC", time(0) * 1000);
	sql_obj->AddInteger("ISPARSE", 1); //TODO 是否解析，默认是1
	sql_obj->AddInteger("PARSENUM", 1);
	sql_obj->AddString("CO_SEQ", inter_proto->meta_data->_seqid);

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TH_VEHICLE_RECORDER, sql_obj);

    return true;
}

/*
 DMSG_ID	NUMBER(15,0)	No	1	息编号，自增序列	43040
 VID	NUMBER(15,0)	No	2	车辆ID	478
 VEHICLE_NO	VARCHAR2(40 BYTE)	No	3	车牌号码	湘N41136
 DMSG_UTC	NUMBER(15,0)	No	4	消息产生的UTC时间	1326697140953
 DMSG_SRTIME	NUMBER(15,0)	Yes	5	发送时间	1326697140953
 DMSG_FLAG	NUMBER(15,0)	Yes	6	消息标志位；见808协议	12
 DMSG_TYPE	VARCHAR2(20 BYTE)	Yes	7	短信类别:1:天气消息2:路线信息等备用	NULL
 SEND_FLAG	NUMBER(2,0)	Yes	8	发送标志1：上行短信0：下行短信	0
 DMSG_CONTENT	VARCHAR2(300 BYTE)	Yes	9	下行短信内容	请正常行驶！！！
 SEND_RESULT	NUMBER(2,0)	Yes	10	发送结果（0. 成功 1.设备返回失败 2. 发送失败 3. 设备不支持此功能 4. 设备不在线 5. 超时）	-1
 DMSG_STATUS	NUMBER(2,0)	Yes	11	是否已读 1-未读 0-已读	1
 SEQ	VARCHAR2(40 BYTE)	No	12	SEQ指令唯一的标识码	10206_1326697140_229
 VEHICLE_COLOR	VARCHAR2(20 BYTE)	Yes	13	车牌颜色	2
 UMSG_SRTIME	NUMBER(15,0)	Yes	14	接收时间	NULL
 UMSG_CONTENT	VARCHAR2(300 BYTE)	Yes	15	上行短信内容	NULL
 DSEND_USER_ID	NUMBER(10,0)	Yes	16	下行发送人	10206
 USEND_USER_ID	NUMBER(10,0)	Yes	17	上行发送人	NULL

 */

bool OracleSave::oracle_vehicle_dispatch_msg(InterProto *inter_proto)
{
    // printf("OracleSave::oracle_vehicle_dispatch_msg \n");

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	sql_obj->AddVar("DMSG_ID", "SEQ_DISPATCH_ID.nextval");
	sql_obj->AddInteger("VID", inter_proto->vehicle_info.vid);
	sql_obj->AddString("VEHICLE_NO", inter_proto->vehicle_info.vechile);
	sql_obj->AddLongLong("DMSG_UTC", time(0) * 1000);
	sql_obj->AddLongLong("DMSG_SRTIME", time(0) * 1000);
	if (inter_proto->meta_data->_cmtype == "D_SNDM") //下行短信
	{
		sql_obj->AddInteger("SEND_FALG", 0);
	}
	else
	{
		sql_obj->AddInteger("SEND_FALG", 1);
	}

	sql_obj->AddString("DMSG_CONTENT", inter_proto->kvmap["2"]);

	sql_obj->AddInteger("SEND_RESULT", 0);
	sql_obj->AddInteger("DMSG_STATUS", 1);
	sql_obj->AddString("SEQ", inter_proto->meta_data->_seqid);
	sql_obj->AddInteger("VEHICLE_COLOR", inter_proto->vehicle_info.corlor);
//  UMSG_SRTIME	NUMBER(15,0)	Yes	14	接收时间	NULL
//	UMSG_CONTENT	VARCHAR2(300 BYTE)	Yes	15	上行短信内容	NULL
	string user_id = get_user_from_seq(inter_proto->meta_data->_seqid);
	sql_obj->AddString("DSEND_USER_ID", user_id);
//	DSEND_USER_ID	NUMBER(10,0)	Yes	16	下行发送人	10206
//	USEND_USER_ID	NUMBER(10,0)	Yes	17	上行发送人	NULL

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TH_VEHICLE_DISPATCH_MSG, sql_obj);

	return true;
}

/*
 AUTO_ID	NUMBER(15,0)	No	1	???????	8751945
 MODIFY_DATE	NUMBER(15,0)	No	2	???? utc??	1331005758260
 TID	NUMBER(15,0)	No	3	????	10338
 T_MAC	VARCHAR2(40 BYTE)	Yes	4	?????	11C1056
 PARAM_TYPE	VARCHAR2(100 BYTE)	Yes	5	???? ?????????	130
 PARAM_VALUE	VARCHAR2(20 BYTE)	Yes	6	???	14400
 SET_RESULT	NUMBER(2,0)	Yes	7	?????-1???? 0 ?? 1:??????  2:????	0
 CREATE_BY	NUMBER(15,0)	Yes	8	???	118460
 CREATE_TIME	NUMBER(15,0)	Yes	9	????	1331005758260
 UPDATE_BY	NUMBER(15,0)	Yes	10	???	118460
 UPDATE_TIME	NUMBER(15,0)	Yes	11	????	1331005774276
 SEQ	VARCHAR2(100 BYTE)	Yes	12	SEQ????????	118460_1331005758_461
 PARENT_CODE	VARCHAR2(20 BYTE)	Yes	13	????????	gaojing

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

 */
bool OracleSave::oracle_terminal_history_param(InterProto *inter_proto)
{
	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	string user_id = get_user_from_seq(inter_proto->meta_data->_seqid);
    sql_obj->AddInteger("AUTO_ID", 0);
	sql_obj->AddLongLong("MODIFY_DATE", time(0) * 1000);
	sql_obj->AddInteger("TID", inter_proto->vehicle_info.vid);
//    sql_obj->AddInteger("T_MAC", 0);

//  每一个参数设置一次,很复杂
//  sql_obj->AddString("PARAM_TYPE", 0);
//  sql_obj->AddString("PARAM_VALUE", 0);
	sql_obj->AddInteger("SET_RESULT", 0);
	sql_obj->AddInteger("CREATE_BY", atoi(user_id.c_str()));
	sql_obj->AddInteger("CREATE_TIME", time(0) * 1000);
	sql_obj->AddInteger("UPDATE_BY", atoi(user_id.c_str()));
	sql_obj->AddInteger("UPDATE_TIME", time(0) * 1000);
	sql_obj->AddString("SEQ", inter_proto->meta_data->_seqid);
//    sql_obj->AddString("PARENT_CODE", user_id);

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TB_TERMINAL_HISTORY_PARAM, sql_obj);

	return true;
}

/****************************************************
 PID	VARCHAR2(32 BYTE)	No	1	UUID	2897
 CLASS_LINE_ID	NUMBER(15,0)	Yes	2	????(TB_CLASS_LINE)??	NULL
 VID	NUMBER(15,0)	Yes	3	????ID	11845
 SEND_COMMAND_STATUS	NUMBER	Yes	4	????????	-1
 SEQ	VARCHAR2(32 BYTE)	Yes	5	????SEQ	NULL
 CREATE_TIME	NUMBER(15,0)	Yes	6	????	NULL
 UPDATE_TIME	NUMBER(15,0)	Yes	7	????	NULL
 LINE_STATUS	NUMBER	Yes	8	???????????1??2??3??	NULL
 JUDGMENT	NUMBER(2,0)	Yes	9	判断类型:0平台判断 1车机判断	NULL
 USETYPE	VARCHAR2(50 BYTE)	Yes	10	业务类型,1-限时,2-限速,3-进报警判断,4-进报警给终端,5-出报警判断,6-出报警给终端 7进报警给平台8出报警给平台	NULL
 LINE_BEGINTIME	NUMBER(15,0)	Yes	11	区域有效开始时间点，从凌晨0:00的分钟数	NULL
 LINE_ENDTIME	NUMBER(15,0)	Yes	12	区域有效结束时间点，从凌晨0:00的分钟数无日期	NULL
 PERIOD_BEGINTIME	VARCHAR2(8 BYTE)	Yes	13	起始时间周期（hh:mm:ss）	NULL
 PERIOD_ENDTIME	VARCHAR2(8 BYTE)	Yes	14	结束时间周期（hh:mm:ss）	NULL
 线路围栏

 155	"设置类型（取值范围：0|1|2|3）：
0：表示删除全部线路
1：删除指定ID的线路
2：更新线路
3：追加线路
4：修改线路"
156	"{TYPE:15,156:[线路数据1][线路数据2][线路数据N]}
路线描述结构：
[
1:路线ID，
2:路线属性，
3:开始时间，
4:结束时间，
5:路段描述结构，
（1=拐点ID|2=路段ID|3=拐点纬度|4=拐点精度|5=路段宽度|6=路段属性|7=路段行驶过长阀值|8=路段行驶过短阀值|
9=路段最高速度|10=路段超速持续时间）（路段2）（路段3）（路段4）
]"
	路线属性  			路段属性
	B0	1：根据时间		B0	1：行驶时间
	B1	保留		B1	1：限速
	B2	1：进路线报警给驾驶员		B2	0：北纬，1：南纬
	B3	1：进路线报警给平台		B3	0：东经，1：西经
	B4	1：出路线报警给驾驶员		B4-B31	保留
	B5	1：出路线报警给平台
	B6-B31	保留
 *****************************************************/

bool OracleSave::oracle_line_vehicle(InterProto *inter_proto)
{
    // printf("OracleSave::oracle_line_vehicle \n");

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	sql_obj->AddVar("PID", "sys_guid()");
	sql_obj->AddInteger("CLASS_LINE_ID", 0);
	sql_obj->AddInteger("VID", inter_proto->vehicle_info.vid);
	sql_obj->AddInteger("SEND_COMMAND_STATUS", 0);
	sql_obj->AddString("SEQ", inter_proto->meta_data->_seqid);
	sql_obj->AddInteger("CREATE_TIME", time(0) * 1000);
	sql_obj->AddInteger("UPDATE_TIME", time(0) * 1000);
	sql_obj->AddInteger("LINE_STATUS", 0);
	sql_obj->AddInteger("JUDGMENT", 0);
	sql_obj->AddString("USETYPE", 0);
	//解析出时间来
	sql_obj->AddInteger("LINE_BEGINTIME", 0);
	sql_obj->AddInteger("LINE_ENDTIME", 0);
	sql_obj->AddString("PERIOD_BEGINTIME", 0);
	sql_obj->AddString("PERIOD_ENDTIME", 0);

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TR_LINE_VEHICLE, sql_obj);

	return true;
}

/**
 ID	NUMBER(15,0)	No	1	???	8840186
 AREA_ID	NUMBER(15,0)	No	2	??ID	8840117
 VID	NUMBER(15,0)	No	3	??ID	140169
 UTC	NUMBER(15,0)	Yes	4	????utc	NULL
 AREA_BEGINTIME	NUMBER(15,0)	Yes	5	?????????????0:00????	1332340597000
 AREA_ENDTIME	NUMBER(15,0)	Yes	6	?????????????0:00???????	1395412599000
 AREA_USETYPE	VARCHAR2(50 BYTE)	Yes	7	????,1-??,2-??,3-??????,4-???????,5-??????,6-????????????????	2
 AREA_UPDATETIME	NUMBER(15,0)	Yes	8	????utc	1332340633396
 AREA_ENABLE	NUMBER(2,0)	Yes	9	????1-?? 0-???	0
 AREA_DECIDE	NUMBER(1,0)	Yes	10	1???? 2????	1
 SEQ	VARCHAR2(50 BYTE)	No	11	?????	test
 AREA_STATUS	NUMBER(1,0)	Yes	12	??1??2??3??	3
 SEND_STATUS	NUMBER(1,0)	Yes	13	"????
 -1????
 0:??"	-1
 SEND_UTC	NUMBER(15,0)	Yes	14	????	1332340633396
 RECEIVE_UTC	NUMBER(15,0)	Yes	15	????	NULL
 PERIOD_BEGINTIME	VARCHAR2(8 BYTE)	Yes	16	起始时间周期（hh:mm:ss）	NULL
 PERIOD_ENDTIME	VARCHAR2(8 BYTE)	Yes	17	结束时间周期（hh:mm:ss）	NULL

 电子围栏
 */
bool OracleSave::oracle_vehicle_area(InterProto *inter_proto)
{
	return true;

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	sql_obj->AddInteger("ID", 0);
	sql_obj->AddInteger("AREA_ID", 0);
	sql_obj->AddInteger("VID", 0);
	sql_obj->AddInteger("UTC", 0);
	sql_obj->AddInteger("AREA_BEGINTIME", 0);
	sql_obj->AddInteger("AREA_ENDTIME", 0);
	sql_obj->AddString("AREA_USETYPE", 0);
	sql_obj->AddInteger("AREA_UPDATETIME", 0);
	sql_obj->AddInteger("AREA_ENABLE", 0);
	sql_obj->AddInteger("AREA_DECIDE", 0);
	sql_obj->AddString("SEQ", 0);
	sql_obj->AddInteger("AREA_STATUS", 0);
	sql_obj->AddInteger("SEND_STATUS", 0);
	sql_obj->AddInteger("SEND_UTC", 0);
	sql_obj->AddInteger("RECEIVE_UTC", 0);
	sql_obj->AddString("PERIOD_BEGINTIME", 0);
	sql_obj->AddString("PERIOD_ENDTIME", 0);

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TR_VEHICLE_AREA, sql_obj);


	return true;
}

/****
 TID	NUMBER(15,0)	No	1	??ID	10178
 PARAM_ID	NUMBER(15,0)	No	2	????ID	41
 T_MAC	VARCHAR2(40 BYTE)	Yes	3	?????	NULL
 PARAM_TYPE	VARCHAR2(20 BYTE)	Yes	4	???? ?????????	NULL
 PARAM_VALUE	VARCHAR2(100 BYTE)	Yes	5	???	湘N20622
 CREATE_BY	NUMBER(15,0)	Yes	6	???	NULL
 CREATE_TIME	NUMBER(15,0)	Yes	7	????	NULL
 UPDATE_BY	NUMBER(15,0)	Yes	8	???	NULL
 UPDATE_TIME	NUMBER(15,0)	Yes	9	????	NULL
 SEQ	VARCHAR2(100 BYTE)	Yes	10	SEQ????????	NULL
 PARENT_CODE	VARCHAR2(20 BYTE)	Yes	11	????????	NULL
 */
bool OracleSave::oracle_terminal_param(InterProto *inter_proto)
{
	return true;

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	sql_obj->AddInteger("TID", 0);
	sql_obj->AddInteger("PARAM_ID", 0);
	sql_obj->AddString("T_MAC", 0);
	sql_obj->AddString("PARAM_TYPE", 0);
	sql_obj->AddString("PARAM_VALUE", 0);
	sql_obj->AddInteger("CREATE_BY", 0);
	sql_obj->AddInteger("CREATE_TIME", 0);
	sql_obj->AddInteger("UPDATE_BY", 0);
	sql_obj->AddInteger("UPDATE_TIME", 0);
	sql_obj->AddString("SEQ", 0);
	sql_obj->AddString("PARENT_CODE", 0);

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TB_TERMINAL_PARAM, sql_obj);

	return true;
}

/****
 INFO_ID	NUMBER(15,0)	No	1	??	2137
 VID	NUMBER(15,0)	No	2	??VID	819
 VEHICLE_NO	VARCHAR2(40 BYTE)	No	3	???	湘N07015
 OEM_CODE	VARCHAR2(20 BYTE)	No	4	???ID(?????????????)	E001
 HARDWARE_VERSION	VARCHAR2(40 BYTE)	Yes	5	?????	1.02
 OLD_HARDWARE_VERSION	VARCHAR2(40 BYTE)	Yes	6	??????	NULL
 FIRMWARE_VERSION	VARCHAR2(40 BYTE)	Yes	7	????	1.00.20120121.025800-patch
 OLD_FIRMWARE_VERSION	VARCHAR2(40 BYTE)	Yes	8	??????	NULL
 CONNECT_TIMES	NUMBER(5,0)	No	9	???????????	10
 URL_ADDRESS	VARCHAR2(100 BYTE)	Yes	10	URL??	192.168.111.106
 DIAL_NAME	VARCHAR2(40 BYTE)	Yes	11	?????	192.168.111.106
 DIAL_USER	VARCHAR2(40 BYTE)	Yes	12	??????	ftpuser
 DIAL_PASSWORD	VARCHAR2(40 BYTE)	Yes	13	????	ftpuser
 IP	VARCHAR2(40 BYTE)	Yes	14	IP??	192.168.111.106
 TCP_PORT	NUMBER(10,0)	Yes	15	TCP??	21
 UDP_PORT	NUMBER(10,0)	Yes	16	UDP??	21
 CREATE_BY	NUMBER(15,0)	No	17	???id	10024
 CREATE_NAME	VARCHAR2(40 BYTE)	No	18	?????	韩东
 CREATE_TIME	NUMBER(15,0)	No	19	????	1330306562468
 SEND_FLAG	NUMBER(2,0)	No	20	?????0-?????1-?????	0
 FINISH_TIME	NUMBER(15,0)	Yes	21	??????	NULL
 FINSIH_FLAG	NUMBER(2,0)	No	22	???????-1????0???1?? ?	-1
 COMMADDR	VARCHAR2(100 BYTE)	Yes	23	????	NULL
 ********/
bool OracleSave::oracle_terminal_updateinfo(InterProto *inter_proto)
{
    return true;

	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	sql_obj->AddInteger("INFO_ID", 0);
	sql_obj->AddInteger("VID", 0);
	sql_obj->AddString("VEHICLE_NO", 0);
	sql_obj->AddString("OEM_CODE", 0);
	sql_obj->AddString("HARDWARE_VERSION", 0);
	sql_obj->AddString("OLD_HARDWARE_VERSION", 0);
	sql_obj->AddString("FIRMWARE_VERSION", 0);
	sql_obj->AddString("OLD_FIRMWARE_VERSION", 0);
	sql_obj->AddInteger("CONNECT_TIMES", 0);
	sql_obj->AddString("URL_ADDRESS", 0);
	sql_obj->AddString("DIAL_NAME", 0);
	sql_obj->AddString("DIAL_USER", 0);
	sql_obj->AddString("DIAL_PASSWORD", 0);
	sql_obj->AddString("IP", 0);
	sql_obj->AddInteger("TCP_PORT", 0);
	sql_obj->AddInteger("UDP_PORT", 0);
	sql_obj->AddInteger("CREATE_BY", 0);
	sql_obj->AddString("CREATE_NAME", 0);
	sql_obj->AddInteger("CREATE_TIME", 0);
	sql_obj->AddInteger("SEND_FLAG", 0);
	sql_obj->AddInteger("FINISH_TIME", 0);
	sql_obj->AddInteger("FINSIH_FLAG", 0);
	sql_obj->AddString("COMMADDR", 0);

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TB_TERMINAL_UPDATEINFO, sql_obj);

	return true;
}

bool OracleSave::oracle_vehicle_media_idx(InterProto *inter_proto)
{
	return true;
}

/*
PID	VARCHAR2(64 BYTE)	No		1	表唯一标识
VID	NUMBER(15,0)	No		2	车辆标识
UTC	NUMBER(15,0)	Yes		3	透传时间
CONTENT	VARCHAR2(1024 BYTE)	Yes	4	透传内容
TYPE	NUMBER(1,0)	Yes	0	5	透传类型，0：上行透传，1：下行透传
MSGTYPE	NUMBER(5,0)	Yes		6	消息类型


 */
bool OracleSave::oracle_vehicle_bridge(InterProto *inter_proto)
{
	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	sql_obj->AddVar("PID", "sys_guid()");
	sql_obj->AddInteger("VID", inter_proto->vehicle_info.vid);
	sql_obj->AddInteger("UTC", time(0) * 1000);
	sql_obj->AddInteger("TYPE", atoi(inter_proto->kvmap["91"].c_str()));
	sql_obj->AddString("CONTENT", inter_proto->kvmap["90"]);

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TH_VEHICLE_BRIDGE, sql_obj);

	return true;
}

/*
PID	VARCHAR2(64 BYTE)	No	        1	主键
VID	NUMBER(15,0)	Yes		        2	车辆标识
UTC	NUMBER(15,0)	Yes		        3	上传时间
CONTENT	VARCHAR2(1024 BYTE)	Yes		4           压缩消息体
 "CAITS 0_0 E013_14784324206 0 U_REPT {TYPE:14,90:1, 92:Q00tMTBB, 93:8} \r\n",
 */
bool OracleSave::oracle_vehicle_compress(InterProto *inter_proto)
{
	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	sql_obj->AddVar("PID", "sys_guid()");
	sql_obj->AddInteger("VID", inter_proto->vehicle_info.vid);
	sql_obj->AddInteger("UTC", time(0) * 1000);
	sql_obj->AddString("CONTENT", inter_proto->kvmap["92"]);

	_inter2save->add_pool(IDataPool::oracle, IDataPool::insert, TH_VEHICLE_BRIDGE, sql_obj);
	return true;
}

/*
 AUTO_ID	NUMBER(15,0)	No	1	主键，自增序列
 VID	NUMBER(15,0)	No	2	车辆ID
 DEVICE_NO	VARCHAR2(20 BYTE)	Yes	3	手机号码
 STAFF_ID	NUMBER(15,0)	Yes	4	驾驶员编号
 STAFF_NAME	VARCHAR2(40 BYTE)	Yes	5	驾驶员名称
 ASK_UTC	NUMBER(15,0)	Yes	6	提问时间UTC
 TTYPE_CODE	VARCHAR2(20 BYTE)	Yes	7	问题标志 参见文本信息类别表
 QUESTION_CONTENT	VARCHAR2(200 BYTE)	Yes	8	提问问题
 CANDIDATE_ANSWER	VARCHAR2(1000 BYTE)	Yes	9	候选答案（采用 答案ID1：答案内容1；答案ID2：答案内容2；格式存储）
 REPLY_UTC	NUMBER(15,0)	Yes	10	回答时间
 ANSWER_CONTENT	VARCHAR2(20 BYTE)	Yes	11	终端答案
 QUESTION_RESULT	NUMBER(2,0)	Yes	12	提问结果（0 正确 1 错误 2回答中）
 MAC_ID	VARCHAR2(10 BYTE)	Yes	13
 SEQ	VARCHAR2(64 BYTE)	Yes	14	业务唯一标识
 OP_ID	VARCHAR2(32 BYTE)	Yes	15	操作员

 //提问下发
 "CAITS 10206_1352187406_24719 E001_15249677400 0 D_SNDM {TYPE:1,2:4+TB6rfWuavLvszh0NHE+qOsxPrS0bOsy9nQ0Mq7o6zH67z1y9nC/dDQo6E=,1:2} \r\n",

 //提问应答
 "CAITS 10206_1352187406_24719 E001_15249677400 0 U_REPT {TYPE:32,82:id,84:seq} \r\n"
 UPDATE TH_QUESTION_ANSWER T SET T.REPLY_UTC = ?, T.ANSWER_CONTENT = ? WHERE T.SEQ = ?

 */
bool OracleSave::oracle_question_answer(InterProto *inter_proto)
{
	CSqlObj *sql_obj = IDataPool::GetSqlObj(IDataPool::oracle);
	sql_obj->AddInteger("REPLY_UTC", time(0) * 1000);
	sql_obj->AddString("ANSWER_CONTENT", inter_proto->kvmap["85"]);

	CSqlWhere *where = new CSqlWhere;
	where->AddWhere("SEQ", inter_proto->kvmap["84"]);

	_inter2save->add_pool(IDataPool::oracle, IDataPool::update, TH_QUESTION_ANSWER, sql_obj, where);


//	CSqlWhere *where = IDataPool::GetSqlObj()
    //存储服务只做更新操作
//    sql_obj->AddInteger("AUTO_ID", 0);
//    sql_obj->AddInteger("VID", inter_proto->vid);
//    sql_obj->AddString("DEVICE_NO", inter_proto->vehicle_number);
//    sql_obj->AddInteger("STAFF_ID", 0);
//    sql_obj->AddString("STAFF_NAME", 0);
//    sql_obj->AddInteger("ASK_UTC", 0);
//    sql_obj->AddString("TTYPE_CODE", 0);
//    sql_obj->AddString("QUESTION_CONTENT", 0);
//    sql_obj->AddString("CANDIDATE_ANSWER", 0);
//    sql_obj->AddInteger("REPLY_UTC", 0);
//    sql_obj->AddString("ANSWER_CONTENT", 0);
//    sql_obj->AddInteger("QUESTION_RESULT", 0);
//    sql_obj->AddString("MAC_ID", 0);
//    sql_obj->AddInteger("SEQ", 0);
//    sql_obj->AddString("OP_ID", 0);
	return true;
}

