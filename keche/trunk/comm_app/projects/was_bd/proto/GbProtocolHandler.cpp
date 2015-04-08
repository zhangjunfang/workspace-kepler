#include "GbProtocolHandler.h"
#include <BaseTools.h>
#include <tools.h>
#include <comlog.h>
#include <arpa/inet.h>

#include <Base64.h>

#include "../../tools/utils.h"

#define  MAX_DWORD_INVALID   0xFFFFFFFF
#define  MAX_WORD_INVALID    0xFFFF

///////////////////////////// GbProtocolHandler /////////////////////////////////
GbProtocolHandler * GbProtocolHandler::_instance = NULL ;
GbProtocolHandler * GbProtocolHandler::getInstance()
{
	if ( _instance == NULL ) {
		_instance = new GbProtocolHandler ;
	}
	_instance->AddRef() ;

	return _instance ;
}

void GbProtocolHandler::FreeHandler( GbProtocolHandler *inst )
{
	if ( inst == NULL ) {
		return ;
	}
	inst->Release() ;
}

PlatFormCommonResp GbProtocolHandler::BuildPlatFormCommonResp(const GBheader*reqheaderptr,
		unsigned short downreq,unsigned char result)
{
	PlatFormCommonResp pcommonresp;
    memcpy(&(pcommonresp.header),reqheaderptr,sizeof(GBheader));

	unsigned short prop  = 0x0005;
	unsigned short msgid = 0x8001;
	msgid = htons(msgid);
	prop  = htons(prop);

	//初始化通用回复消息头
	memcpy(&(pcommonresp.header.msgtype),&prop,sizeof(unsigned short));
	memcpy(&(pcommonresp.header.msgid),&msgid,sizeof(unsigned short));
	pcommonresp.header.seq = htons(downreq);

	pcommonresp.resp.resp_msg_id = reqheaderptr->msgid;
	pcommonresp.resp.resp_seq 	 = reqheaderptr->seq;
	pcommonresp.resp.result 	 = result;
	pcommonresp.check_sum 		 = get_check_sum((const char*)(&pcommonresp)+1, sizeof(PlatFormCommonResp) - 3);
	pcommonresp.end._end         = 0x7e ;

    return pcommonresp;
}

string GbProtocolHandler::ConvertEngeer( EngneerData *p )
{
	string dest ;
	if ( p == NULL )
		return dest ;
	dest +="4:"+ get_bcd_time(p->time) + ",";
	dest += "210:" + ustodecstr( ntohs(p->speed) ) +"," ;
	dest += "503:" + chartodecstr( p->torque )+"," ;
	dest += "504:" + chartodecstr( p->position ) ;

	return dest ;
}

// 转换驾驶行为事件
string GbProtocolHandler::ConvertEventGps( GpsInfo *gps )
{
	string dest ;
	if ( gps == NULL )
		return dest ;

	// [起始位置纬度][起始位置经度][起始位置高度][起始位置速度][起始位置方向][起始位置时间]
	unsigned int lon = 0;
	unsigned int lat = 0;
	lon = (unsigned int)ntohl(gps->longitude)*6 / 10 ;
	lat = (unsigned int)ntohl(gps->latitude)*6 / 10 ;

	dest +="[" + uitodecstr(lat) + "]" ;
	dest +="[" + uitodecstr(lon) + "]" ;
	dest +="[" + ustodecstr(ntohs(gps->heigth)) + "]" ;
	dest +="[" + ustodecstr(ntohs(gps->speed))  + "]" ;
	dest +="[" + ustodecstr(ntohs(gps->direction)) + "]" ;
	dest +="[" + get_bcd_time(gps->date_time) + "]" ;

	return dest ;
}

static unsigned int getdword( const char *buf )
{
	unsigned int dword = 0 ;
	memcpy( &dword, buf, 4 ) ;
	dword = ntohl( dword ) ;
	return dword ;
}

static unsigned short getword( const char *buf )
{
	unsigned short word = 0 ;
	memcpy( &word, buf, 2 ) ;
	word = ntohs( word ) ;
	return word ;
}

static char * getbuffer( const char *buf, char *sz , int len )
{
	memcpy( sz, buf, len ) ;
	sz[len] = 0 ;
	return sz ;
}

// 构建MAP的KEY和VALUE值的关系
static void addmapkey( const string &key, const string &val , map<string,string> &mp )
{
	if ( key.empty() ) {
		return ;
	}

	map<string,string>::iterator it = mp.find( key ) ;
	if ( it == mp.end() ) {
		mp.insert( pair<string,string>( key, val ) ) ;
	} else {
		it->second += "|" ;
		it->second += val ;
	}
}

static const string buildmapcommand( map<string,string> &mp )
{
	string sdata = "" ;
	if ( mp.empty() )
		return sdata ;

	map<string,string>::iterator it ;
	for ( it = mp.begin(); it != mp.end(); ++ it ) {
		if ( ! sdata.empty() ) {
			sdata += "," ;
		}
		sdata += it->first + ":" + it->second ;
	}
	return sdata ;
}

static string itodecstr(unsigned int intger,unsigned int max,bool bflag)
{
	char buf[128] = {0};

	if( max == intger || ( bflag && intger == 0xff) ) {
		sprintf(buf,"%d",-1);
	} else {
		sprintf(buf, "%u", intger);
	}
	return string(buf) ;
}

string GbProtocolHandler::getTermAttribute(const char *data, size_t len)
{
	string msg;
	size_t pos = sizeof(GBheader);
	uint8_t fieldSize;

	// 终端类型，2字节
	if(pos + 2 > len) {
		return msg;
	}
	msg +=  ",20000:" + uitodecstr(ntohs(*(uint16_t*)(data + pos)));
	pos += 2;

	// 制造商ID，5字节
	if(pos + 5 > len) {
		return msg;
	}
	msg += ",20001:" + string(data + pos, strnlen(data + pos, 5));
	pos += 5;

	// 终端型号，20字节
	if(pos + 20 > len) {
		return msg;
	}
	msg += ",20002:" + string(data + pos, strnlen(data + pos, 20));
	pos += 20;

	// 终端ID，7字节
	if(pos + 7 > len) {
		return msg;
	}
	msg += ",20003:" + string(data + pos, strnlen(data + pos, 7));
	pos += 7;

	// 终端SIM卡ICCID，10字节
	if(pos + 10 >len) {
		return msg;
	}
	msg += ",20004:" + BCDtostr((char*)data + pos, 10);
	pos += 10;

	// 终端硬件版本号长度，1字节
	if(pos + 1 > len) {
		return msg;
	}
	fieldSize = *(uint8_t*)(data + pos);
	pos += 1;

	// 终端硬件版本号，n字节
	if(pos + fieldSize > len) {
		return msg;
	}
	msg += ",20005:" + string(data + pos, strnlen(data + pos, fieldSize));
	pos += fieldSize;

	// 终端固件版本号长度，1字节
	if(pos + 1 > len) {
		return msg;
	}
	fieldSize = *(uint8_t*)(data + pos);
	pos += 1;

	// 终端固件版本号，n字节
	if(pos + fieldSize > len) {
		return msg;
	}
	msg += ",20006:" + string(data + pos, strnlen(data + pos, fieldSize));
	pos += fieldSize;

	// GNSS 模块属性，1字节
	if(pos + 1 > len) {
		return msg;
	}
	msg += ",20007:" + uitodecstr(*(uint8_t*)(data + pos));
	pos += 1;

	// 通信模块属性，1字节
	if(pos + 1 > len) {
		return msg;
	}
	msg += ",20008:" + uitodecstr(*(uint8_t*)(data + pos));
	pos += 1;

	return msg;
}

string GbProtocolHandler::ConvertGpsInfo(GpsInfo*gps_info, const char *append_data, const int append_data_len)
{
	string dest;
	if (gps_info == NULL)
		return dest;

	unsigned int lon = 0;
	unsigned int lat = 0;
	lon = (unsigned int)ntohl(gps_info->longitude)*6 / 10 ;
	lat = (unsigned int)ntohl(gps_info->latitude)* 6 / 10 ;

	map<string,string>  mp ;

	addmapkey( "1" , uitodecstr(lon) , mp ) ;
	addmapkey( "2" , uitodecstr(lat) , mp ) ;
	addmapkey( "3" , uitodecstr(ntohs(gps_info->speed)) , mp ) ;
	addmapkey( "4" , get_bcd_time(gps_info->date_time) , mp ) ;

	//正北方向为0，顺时针方向，单位为2度。
	addmapkey( "5" , uitodecstr(ntohs(gps_info->direction)) , mp ) ;
	addmapkey( "6" , uitodecstr(ntohs(gps_info->heigth)) , mp ) ;

    /*张鹤高增加 ***********************************************************/
	//DWORD,位置基本信息状态位，B0~B15,参考JT/T808-2011,Page15，表17
	unsigned int status =0;
	memcpy(&status,&(gps_info->state),sizeof(unsigned int));
	status = ntohl(status) ;
	// 处理状态
	addmapkey( "8" , uitodecstr(status) , mp ) ;

	//解析报警标志位
	int ala = 0;
	memcpy(&ala,&(gps_info->alarm),sizeof(int));
	ala = ntohl( ala ) ;

	// 处理成一个报警标志位
	addmapkey( "20", uitodecstr(ala) , mp ) ;

	//单独处理附加信息
	if (append_data != NULL && append_data_len > 2)
	{
		unsigned short cur   = 0;
		unsigned char  amid  = 0;
		unsigned char  amlen = 0;
		unsigned short word  = 0;
		unsigned int   dword = 0;

		while(cur +2 < append_data_len)
		{
			word = 0;
			dword = 0;
            amid =  append_data[cur];
            amlen = append_data[cur+1];
            if( cur+2+amlen > append_data_len )
            	break;
            //printf("amid:%x,amlen:%x \n",amid,amlen);
            switch(amid)
            {
            case 0x01://里程
            	addmapkey( "9",itodecstr(getdword(append_data+cur+2),MAX_DWORD_INVALID,false) , mp ) ;
            	break;
            case 0x02://油量，WORD，1/10L，对应车上油量表读数
            	addmapkey( "24" , itodecstr(getword(append_data+cur+2),MAX_WORD_INVALID,false) , mp ) ;
            	break;
            case 0x03://行驶记录功能获取的速度，WORD,1/10KM/h
            	addmapkey( "7" , uitodecstr(getword( append_data+cur+2 ) ) , mp ) ;
            	break;
            case 0x04: // 需要人工确认报警事件的ID，WORD,从1开始计数
            	addmapkey( "519" , ustodecstr(getword( append_data+cur+2)) , mp ) ; // 补页新增
            	break ;
            case 0x11://超速报警附加信息
            	if(amlen == 1)
            	{
            		word = append_data[cur+2];
                    if(word == 0) {
                    	addmapkey( "31" , "0|" , mp ) ;
                    }
            	}
            	else if(amlen == 5)
            	{
            		word = append_data[cur+2] ;
                    if(word >=1 && word<=4) {
                    	dword = getdword( append_data+cur+3 ) ;
                    	addmapkey( "31" , ustodecstr(word)+"|"+uitodecstr(dword) , mp ) ;
                    }
            	}
            	break;
            case 0x12://进出区域/路线报警附加信息
            	if(amlen == 6)
            	{
            	    word = append_data[cur+2];
            	    if(word >=1 && word<=4)
            	    {
            	        dword = getdword( append_data+cur+3 ) ;
            	        unsigned short d = append_data[cur+2+5];
            	        if( d == 0 || d == 1 ){
            	        	addmapkey( "32" , ustodecstr(word)+"|"+uitodecstr(dword)+"|"+ustodecstr(d) , mp ) ;
            	        }
            	    }
            	}
            	break;
            case 0x13://路段行驶时间不足/过长报警附加信息
            	if(amlen == 7)
            	{
            		dword = getdword( append_data+cur+2 ) ;
            		word  = getword( append_data+cur+6 ) ;

            		unsigned short d = append_data[cur+8];
            	    if( d == 0 || d == 1 ){
            	        addmapkey( "35" , uitodecstr(dword)+"|"+ustodecstr(word)+"|"+ustodecstr(d) , mp ) ;
            	    }
            	}
            	break;
            case 0x14: // 需人工确认的报警流水号，定义见表20-3  4字节
            	{
            		addmapkey( "520", ustodecstr(getdword( append_data+cur+2 )), mp ) ;
            	}
            	break ;
            case 0xE0: // 后继信息长度
            	// ToDo:
            	break ;
            case 0x20: //发动机转速
               {
            	   addmapkey( "210" , itodecstr(getword( append_data+cur+2),MAX_WORD_INVALID,false),mp) ;
               }
            	break;
            case 0x21: //瞬时油耗
               {
            	   addmapkey("216" , itodecstr(getword( append_data+cur+2),MAX_WORD_INVALID,false),mp) ;
               }
            	break;
            case 0x22: // 发动机扭矩百分比
            	{
            		addmapkey( "503" , itodecstr(getword( append_data+cur+2),MAX_WORD_INVALID,true) , mp ) ;
            	}
            	break ;
            case 0x23:  // 油门踏板位置
            	{
					addmapkey( "504" , itodecstr(getword( append_data+cur+2),MAX_WORD_INVALID,true) , mp ) ;
            	}
            	break ;
            case 0x24: // 扩展车辆报警标志位
            	{
            		// 最新协议的扩展标志位
            		addmapkey( "21" , uitodecstr(getdword( append_data+cur+2 )), mp );//ustodecstr
            	}
            	break;
            case 0x25: // 扩展车辆信号状态位
				{
					addmapkey("500", ustodecstr(getdword( append_data+cur+2 )) , mp ) ;
				}
				break ;
            case 0x26: // 累计油耗
				{
					addmapkey( "213" , itodecstr(getdword( append_data+cur+2),MAX_DWORD_INVALID,false) , mp ) ;
				}
				break ;
            case 0x27:  // 0x00：带速开门；0x01区域外开门；0x02：区域内开门；其他值保留；1字符
				{
					addmapkey( "217", uitodecstr( (unsigned char)(*(append_data+cur+2)) ) , mp ) ;
				}
				break ;
            case 0x28:  // 0x28 VSS还GPS 车速来源
            	{
            		addmapkey( "218", uitodecstr( (unsigned char)(*(append_data+cur+2))) , mp ) ;
            	}
            	break ;
            case 0x29: // 0x29 计量仪油耗，1bit=0.01L,0=0L
				{
					addmapkey( "219",  itodecstr(getdword( append_data+cur+2),MAX_DWORD_INVALID,false) , mp ) ;
				}
				break ;
            case 0x2a: // IO状态位
				addmapkey("580", itodecstr(getword(append_data + cur + 2), MAX_WORD_INVALID, false), mp);
            	break;
            case 0x2b: // 模拟量
            	addmapkey( "581",  itodecstr(getdword( append_data+cur+2),MAX_DWORD_INVALID,false) , mp ) ;
            	break;
            case 0x30: // 无线通信网络信号强度
            	addmapkey( "582", uitodecstr( (unsigned char)(*(append_data+cur+2))) , mp ) ;
            	break;
            case 0x31: // GNSS 定位卫星数
            	addmapkey( "583", uitodecstr( (unsigned char)(*(append_data+cur+2))) , mp ) ;
            	break;
            case 0x32:  // 远程锁车状态报告
            	{
            		addmapkey( "570", uitodecstr( (unsigned char)(*(append_data+cur+2))) , mp ) ;
            	}
            	break ;
            case 0x40: // 发动机运行总时长
				{
					addmapkey( "505" , itodecstr(getdword( append_data+cur+2),MAX_DWORD_INVALID,false) , mp ) ;
				}
				break ;
            case 0x41:  // 终端内置电池电压
				{
					addmapkey( "506" , itodecstr(getword( append_data+cur+2),MAX_WORD_INVALID,false) , mp ) ;
				}
				break ;
            case 0x42:  // 蓄电池电压
				{
					addmapkey( "507" , itodecstr(getword( append_data+cur+2),MAX_WORD_INVALID,false) , mp ) ;
				}
				break ;
            case 0x43:  // 发动机水温
				{
					addmapkey( "214" , itodecstr(getword( append_data+cur+2),MAX_WORD_INVALID,true) , mp ) ;
				}
				break ;
            case 0x44:  // 机油温度
				{
					addmapkey( "508" , itodecstr(getword(append_data+cur+2),MAX_WORD_INVALID,true),mp);
				}
				break ;
            case 0x45:  // 发动机冷却液温度
				{
					addmapkey( "509" , itodecstr(getword( append_data+cur+2),MAX_WORD_INVALID,true) , mp ) ;
				}
				break ;
            case 0x46:  // 进气温度
				{
					addmapkey( "510" , itodecstr(getword( append_data+cur+2),MAX_WORD_INVALID,false) , mp ) ;
				}
				break ;
            case 0x47:  // 机油压力
				{
					addmapkey( "215" , itodecstr(getword( append_data+cur+2),MAX_WORD_INVALID,true) , mp ) ;
				}
				break ;
            case 0x48:  // 大气压力
				{
					addmapkey( "511" , itodecstr(getword( append_data+cur+2),MAX_WORD_INVALID,true) , mp ) ;
				}
				break ;
            case 0x49:  // 路况订阅
				{
					addmapkey( "549" , itodecstr(getdword( append_data+cur+2),MAX_DWORD_INVALID,false) , mp ) ;
				}
				break;
			case 0xe1: // 公共自定义信息内容
				{
					getCommonExtend((unsigned char*) append_data + cur + 2, amlen, mp);
				}
				break;
            default:
            	break;
            }
            cur += 2+amlen;

		}
	}
/***********************************************************************/
	//dest = "{TYPE:0,RET:0," + dest;
	// 重组内部协议数据
	dest += buildmapcommand(mp) ;
	//dest += "}";
	/*if ( !astr.empty() )
		dest += astr ;
	*/
	return dest;
}

bool GbProtocolHandler::getCommonExtend(const unsigned char *ptr, int len, map<string, string> &mp)
{
	int pos;

	unsigned char extKey;
	unsigned char extLen;

	char chrVal;
	int intVal;
	string strVal;

	pos = 0;
	while (pos + 2 <= len) {
		extKey = ptr[pos++];
		extLen = ptr[pos++];

		if (pos + extLen > len) {
			break;
		}

		if (extKey == 0x01 && extLen == 1) {
			chrVal = ptr[pos];
			addmapkey("550", Utils::int2str(chrVal, strVal), mp);
		} else if (extKey == 0x02 && extLen == 1) {
			chrVal = ptr[pos];
			addmapkey("551", Utils::int2str(chrVal, strVal), mp);
		} else if (extKey == 0x03 && extLen == 1) {
			chrVal = ptr[pos];
			addmapkey("552", Utils::int2str(chrVal, strVal), mp);
		} else if (extKey == 0x04 && extLen == 4) {
			intVal = ntohl(*(int*) (ptr + pos));
			addmapkey("553", Utils::int2str(intVal, strVal), mp);
		} else if (extKey == 0x05 && extLen == 4) {
			intVal = ntohl(*(int*) (ptr + pos));
			addmapkey("554", Utils::int2str(intVal, strVal), mp);
		} else if (extKey == 0x06 && extLen == 4) {
			intVal = ntohl(*(int*) (ptr + pos));
			addmapkey("555", Utils::int2str(intVal, strVal), mp);
		}

		pos += extLen;
	}

	return true;
}

// 获取驾驶员身份信息
bool GbProtocolHandler::GetDriverInfo( const char *buf, int len, DRIVER_INFO &info )
{
	int fieldLen;
	unsigned char *fieldPtr = (unsigned char*) buf;
	unsigned char *fieldEnd = fieldPtr + len;

	if (fieldPtr + 1 > fieldEnd) {
		return false;
	}
	info.state = *fieldPtr;
	fieldPtr += 1;

	if (fieldPtr + 6 > fieldEnd) {
		return false;
	}
	if(memcmp(fieldPtr, "\xff\xff\xff\xff\xff\xff", 6) == 0) {
		info.actionTime = "-1";
	} else {
		info.actionTime = BCDtostr((char*)fieldPtr, 6);
	}
	fieldPtr += 6;

	if(info.state != 1) {
		return true;
	}

	if (fieldPtr + 1 > fieldEnd) {
		return false;
	}
	info.result = *fieldPtr;
	fieldPtr += 1;

	if (fieldPtr + 1 > fieldEnd) {
		return false;
	}
	fieldLen = *fieldPtr;
	fieldPtr += 1;

	if (fieldPtr + fieldLen > fieldEnd) {
		return false;
	}
	info.driverName = string((char*)fieldPtr, fieldLen);
	fieldPtr += fieldLen;

	if (fieldPtr + 20 > fieldEnd) {
		return false;
	}
	fieldLen = strnlen((char*)fieldPtr, 20);
	info.certificateID = string((char*)fieldPtr, fieldLen);
	fieldPtr += 20;

	if (fieldPtr + 1 > fieldEnd) {
		return false;
	}
	fieldLen = *fieldPtr;
	fieldPtr += 1;

	if (fieldPtr + fieldLen > fieldEnd) {
		return false;
	}
	info.organization = string((char*)fieldPtr, fieldLen);
	fieldPtr += fieldLen;

	if (fieldPtr + 4 > fieldEnd) {
		return false;
	}
	info.timeLimit = BCDtostr((char*)fieldPtr, 4);

	return true ;
}
/*
 * buf：消息体
 * buf_len:消息体长度
 */
string  GbProtocolHandler::ConvertDriverInfo(char *buf, int buf_len, unsigned char result)
{
	DRIVER_INFO info ;
	if ( ! GetDriverInfo( buf, buf_len , info ) ) {
		return "";
	}

	// 上报驾驶员身份识别结果
	string dstr = "{TYPE:8,RESULT:" ;
	dstr += uitodecstr(result) ;

	// IC卡插拔状态
	dstr += ",107:" + uitodecstr(info.state);

	// IC卡插拔时间
	dstr += ",108:" + info.actionTime;

	if(info.state != 1) {
		dstr += "}";
		return dstr;
	}

	// IC卡读取结果
	dstr += ",109:" + uitodecstr(info.result);

	if(info.result != 0) {
		dstr += "}";
		return dstr;
	}

	// 驾驶员姓名
	dstr += ",110:" + info.driverName;

    //从业资格证有效期
    dstr += ",114:" + info.timeLimit;

    //从业资格证编码
    dstr += ",112:" + info.certificateID;

    //发证机构名称
    dstr += ",113:" + info.organization;

    return dstr + "}";
}

//flag 0:读取，1设置
bool  GbProtocolHandler::ConvertGetPara(char *buf, int buf_len, string &data)
{
	/***********张鹤高修改8-9*************************************/
	int curn = sizeof(GBheader);

	//unsigned short seq = getword(buf+curn) ;
	curn += 2;

	CBase64 base64;
	unsigned int u32Val;
	string binStr;

	unsigned short pnum = 0;
	pnum = buf[curn++];

	unsigned long pid = 0;
	unsigned char plen = 0;

	map<string, string> mp;

	char pchar[257];
	pchar[0] = 0;

	for (int i = 0; i < pnum; ++i) {
		pid = getdword(buf + curn);
		curn += 4;
		plen = (unsigned char) buf[curn++];
		switch (pid) {
		case 0x0001:
			addmapkey("7", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0002:
			addmapkey("100", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0003:
			addmapkey("101", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0004:
			addmapkey("102", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0005:
			addmapkey("103", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0006:
			addmapkey("104", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0007:
			addmapkey("105", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0010: //APN
			addmapkey("3", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0011:
			addmapkey("4", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0012:
			addmapkey("5", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0013:
			addmapkey("2", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0014: //备份APN
			addmapkey("106", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0015:
			addmapkey("107", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0016:
			addmapkey("108", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0017: //备份IP
			addmapkey("109", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0018:
			addmapkey("1", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0019: // 根据内部协议与外部协议对应关系处理
			addmapkey("110", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x001a:
			addmapkey("1001a", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x001b:
			addmapkey("1001b", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x001c:
			addmapkey("1001c", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x001d:
			addmapkey("1001d", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0020:
			addmapkey("111", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0021:
			addmapkey("112", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0022:
			addmapkey("113", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0027:
			addmapkey("114", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0028:
			addmapkey("115", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0029:
			addmapkey("116", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x002C:
			addmapkey("117", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x002D:
			addmapkey("118", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x002E:
			addmapkey("119", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x002F:
			addmapkey("120", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0030:
			addmapkey("121", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0031: // 电子围栏半径
			addmapkey("31", uitodecstr(getword(buf + curn)), mp);
			break;
		case 0x0040: //监控平台电话号码
			addmapkey("10", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0041: //复位电话号码
			addmapkey("122", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0042:
			addmapkey("123", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0043:
			addmapkey("15", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0044:
			addmapkey("124", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0045:
			addmapkey("125", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0046:
			addmapkey("126", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0047:
			addmapkey("127", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0048:
			addmapkey("9", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0049: //监控平台特权短信号码
			addmapkey("141", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0050:
			addmapkey("142", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0051:
			addmapkey("143", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0052:
			addmapkey("144", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0053:
			addmapkey("145", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0054:
			addmapkey("146", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0055:
			addmapkey("128", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0056:
			addmapkey("129", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0057:
			addmapkey("130", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0058:
			addmapkey("131", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0059:
			addmapkey("132", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x005A:
			addmapkey("133", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x005B: // 超速报警预警差值
			addmapkey("300", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0x005C: // 特征系数
			addmapkey("301", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0x005d:
			addmapkey("1005d", uitodecstr(getword(buf + curn)), mp);
			break;
		case 0x005e:
			addmapkey("1005e", uitodecstr(getword(buf + curn)), mp);
			break;
		case 0x0064: //定时拍照
			u32Val = getdword(buf + curn);
			u32Val = ((u32Val & 0xfffe0000) >> 1) | (u32Val & 0xffff);
			addmapkey("180", uitodecstr(u32Val), mp);
			break;
		case 0x0065: //定距拍照
			u32Val = getdword(buf + curn);
			u32Val = ((u32Val & 0xfffe0000) >> 1) | (u32Val & 0xffff);
			addmapkey("181", uitodecstr(u32Val), mp);
			break;
		case 0x0070:
			addmapkey("136", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0071:
			addmapkey("137", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0072:
			addmapkey("138", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0073:
			addmapkey("139", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0074:
			addmapkey("140", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0080:
			addmapkey("147", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0081:
			addmapkey("134", uitodecstr(getword(buf + curn)), mp);
			break;
		case 0x0082:
			addmapkey("135", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0x0083:
			addmapkey("41", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0x0084: // 车牌颜色
			addmapkey("42", ustodecstr(buf[curn]), mp);
			break;
		case 0x0090:
			addmapkey("10090" , ustodecstr(buf[curn]) , mp);
			break;
		case 0x0091:
			addmapkey("10091" , ustodecstr(buf[curn]) , mp);
			break;
		case 0x0092:
			addmapkey("10092" , ustodecstr(buf[curn]) , mp);
			break;
		case 0x0093:
			addmapkey("10093" , uitodecstr(getdword(buf+curn)) , mp);
			break;
		case 0x0094:
			addmapkey("10094" , ustodecstr(buf[curn]) , mp) ;
			break;
		case 0x0095:
			addmapkey("10095" , uitodecstr(getdword(buf+curn)) , mp);
			break;
		case 0x0100:
			addmapkey("10100" , uitodecstr(getdword(buf+curn)) , mp);
			break;
		case 0x0101:
			addmapkey("10101" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0x0102:
			addmapkey("10102" , uitodecstr(getdword(buf+curn)) , mp);
			break;
		case 0x0103:
			addmapkey("10103", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0x0110:
			binStr = "";
			if (base64.Encode(buf + curn, plen)) {
				binStr.append(base64.GetBuffer(), base64.GetLength());
			}
			addmapkey("10110", binStr, mp);
			break;
		case 0xf000:
			addmapkey("207" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf001:
			addmapkey("208" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf002:
			addmapkey("209" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf003:
			addmapkey("210" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf004:
			addmapkey("211" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf005:
			addmapkey("212" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf006:
			addmapkey("213" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf007:
			addmapkey("214" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf008:
			addmapkey("1f008" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf009:
			addmapkey("1f009" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf00a:
			addmapkey("216" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf00b:
			addmapkey("217" , ustodecstr(getword(buf+curn)) , mp);
			break;
		case 0xf00c:
			binStr = "";
			if(base64.Encode(buf + curn, plen)) {
				binStr.append(base64.GetBuffer(), base64.GetLength());
			}
			addmapkey("201", binStr, mp);
			break;
		case 0xf00d:
			binStr = "";
			if (base64.Encode(buf + curn, plen)) {
				binStr.append(base64.GetBuffer(), base64.GetLength());
			}
			addmapkey("202", binStr, mp);
			break;
		case 0xf00e:
			addmapkey("1f00e" , ustodecstr(getword(buf+curn)) , mp );
			break;
		case 0xf00f:
			addmapkey("1f00f" , uitodecstr(getdword(buf+curn)) , mp );
			break;
		case 0xf010:
			addmapkey("218" , ustodecstr(getword(buf+curn)) , mp );
			break;
		case 0xf011:
			addmapkey("219" , ustodecstr(buf[curn]) , mp);
			break;
		case 0xf012:
			addmapkey("1f012" , uitodecstr(getdword(buf+curn)) , mp);
			break;
		case 0xf013:
			addmapkey("1f013" , uitodecstr(getdword(buf+curn)) , mp);
			break;
		case 0xf014:
			addmapkey("1f014" , uitodecstr(getdword(buf+curn)) , mp);
			break;
		case 0xf015:
			addmapkey("1f015", ustodecstr(buf[curn]), mp);
			break;
		case 0xf016:
			addmapkey("1f016" , uitodecstr(getdword(buf+curn)) , mp);
			break;
		case 0xf017:
			addmapkey("1f017" , uitodecstr(getdword(buf+curn)) , mp);
			break;
		case 0xf018:
			addmapkey("1f018", Utils::array2hex((uint8_t*) buf + curn, plen), mp);
			break;
		case 0xf019:
			addmapkey("1f019", Utils::array2hex((uint8_t*) buf + curn, plen), mp);
			break;
		case 0xf030:
			if (plen == 6) {
				addmapkey("1f030", Utils::array2hex((uint8_t*) buf + curn, plen).substr(1), mp);
			}
			break;
		case 0xf031:
			if (plen == 6) {
				addmapkey("1f031", Utils::array2hex((uint8_t*) buf + curn, plen).substr(1), mp);
			}
			break;
		case 0xf032:
			if (plen == 6) {
				addmapkey("1f032", Utils::array2hex((uint8_t*) buf + curn, plen).substr(1), mp);
			}
			break;
		case 0xf033:
			if (plen == 6) {
				addmapkey("1f033", Utils::array2hex((uint8_t*) buf + curn, plen).substr(1), mp);
			}
			break;
		case 0xf034:
			addmapkey("1f034", string(buf + curn, plen), mp);
			break;
		case 0xf100:
			addmapkey("200", ustodecstr(buf[curn]), mp);
			break;
		case 0xf101:
			addmapkey("302" , ustodecstr(getword(buf+curn)) , mp );
			break;
		case 0xf102:
			addmapkey("303" , ustodecstr(getword(buf+curn)) , mp );
			break;
		case 0xf103:
			addmapkey("1f103" , ustodecstr(getword(buf+curn)) , mp );
			break;
		case 0xf104:
			addmapkey("304" , ustodecstr(getword(buf+curn)) , mp );
			break;
		case 0xf105:
			binStr = "";
			if(base64.Encode(buf+curn, plen)) {
				binStr.append(base64.GetBuffer(), base64.GetLength());
			}
			addmapkey("305" , binStr, mp);
			break;
		case 0xf106:
			addmapkey("306" , uitodecstr(getdword(buf+curn)) , mp);
			break;
		case 0xf107:
			addmapkey("307" , uitodecstr(getdword(buf+curn)) , mp);
			break;
		case 0xf108:
			addmapkey("187", ustodecstr(buf[curn]), mp);
			break;
		case 0xf109:
			addmapkey("190" , ustodecstr(getword(buf+curn)) , mp );
			break;
		case 0xf10a:
			addmapkey("309" , ustodecstr(getword(buf+curn)) , mp );
			break;
		case 0xf10b:
			addmapkey("310", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0xf10c:
			addmapkey("203", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0xf10d:
			addmapkey("204", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0xf10e:
			addmapkey("205", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0xf10f:
			addmapkey("206", getbuffer(buf + curn, pchar, plen), mp);
			break;
		case 0xf110:
			addmapkey("1f110", string(buf + curn, plen), mp);
			break;
		case 0xf111:
			addmapkey("1f111", string(buf + curn, plen), mp);
			break;
		case 0xf112:
			addmapkey("1f112", string(buf + curn, plen), mp);
			break;
		case 0xf113:
			addmapkey("1f113", string(buf + curn, plen), mp);
			break;
		case 0xf114:
			addmapkey("1f114", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0xf120:
			addmapkey("1f120", string(buf + curn, plen), mp);
			break;
		case 0xf121:
			addmapkey("1f121", string(buf + curn, plen), mp);
			break;
		case 0xf122:
			addmapkey("1f122", string(buf + curn, plen), mp);
			break;
		case 0xf123:
			addmapkey("1f123", string(buf + curn, plen), mp);
			break;
		case 0xf124:
			addmapkey("1f124", string(buf + curn, plen), mp);
			break;
		case 0xf125:
			addmapkey("1f125", string(buf + curn, plen), mp);
			break;
		case 0xf126:
			addmapkey("1f126", string(buf + curn, plen), mp);
			break;
		case 0xf127:
			addmapkey("1f127", string(buf + curn, plen), mp);
			break;
		case 0xf128:
			addmapkey("1f128", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0xf129:
			addmapkey("1f129", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0xf12a: //双中心平台业务配置字
			addmapkey("1f12a", ustodecstr(buf[curn]), mp);
			break;
	  }
	  curn += plen;
	  if(curn >=buf_len)
		  break;
	}

	string items = buildmapcommand(mp);
	if(items.empty()) {
		data = "{TYPE:0,RET:0} \r\n";
	} else {
		data = "{TYPE:0,RET:0," + items+ "} \r\n";
	}

	return true;
}

static void setdword( DataBuffer *pbuf, unsigned int msgid, unsigned int dword )
{
	pbuf->writeInt32( msgid ) ;
	pbuf->writeInt8( 4 ) ;
	pbuf->writeInt32( dword ) ;
}

static void setword( DataBuffer *pbuf, unsigned int msgid, unsigned short word )
{
	pbuf->writeInt32( msgid ) ;
	pbuf->writeInt8( 2 ) ;
	pbuf->writeInt16( word ) ;
}

static void setstring( DataBuffer *pbuf ,unsigned int msgid, const char *data, int nlen )
{
	pbuf->writeInt32( msgid ) ;
	pbuf->writeInt8( (uint8_t)nlen ) ;

	if ( nlen > 0 ) {
		pbuf->writeBlock( (void*)data, nlen ) ;
	}
}

/*
static void setbytes( DataBuffer *pbuf ,unsigned int msgid, unsigned char *data, int nlen , int max )
{
	pbuf->writeInt32( msgid ) ;
	pbuf->writeInt8( max ) ;

	if ( nlen >= max ) {
		pbuf->writeBlock( data, max ) ;
	} else {
		pbuf->writeBlock( data , nlen ) ;
		pbuf->writeFill( 0, max-nlen ) ;
	}
}
*/

static void setbyte( DataBuffer *pbuf, unsigned int msgid, unsigned char c )
{
	pbuf->writeInt32( msgid ) ;
	pbuf->writeInt8( 1 ) ;
	pbuf->writeInt8( c ) ;
}

// 将内部协议的参数设置转为外部协议
bool GbProtocolHandler::buildParamSet( DataBuffer *pbuf , map<string,string> &p_kv_map, unsigned char &pnum )
{
	p_kv_map.erase("TYPE");
	p_kv_map.erase("RETRY");

	if ( p_kv_map.empty() ) {
		return false ;
	}

	CBase64 base64;
	unsigned int u32Val;
	string binStr;
	vector<uint8_t> binBuf;

	string k,v;
	int uskey = 0;

	pnum = 0 ;

	//用来保持参数列表
	vector<string> vec_v;
	typedef map<string, string>::iterator MapIter;

	for (MapIter p = p_kv_map.begin(); p != p_kv_map.end(); ++p) {
		k = (string) p->first;
		v = (string) p->second;
		if (v.size() > 256)
			continue;

		uskey = Utils::str2int(k.c_str(), uskey, ios::hex);
		switch(uskey) {
		case 0x1://tcp port
			setdword( pbuf, 0x0018, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x2:// master ip
			setstring(pbuf, 0x0013, v.c_str(), v.length());
			pnum++;
			break;
		case 0x3://APN
			setstring( pbuf, 0x0010, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x4://APN username
			setstring( pbuf, 0x0011, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x5://APN pwd
			setstring( pbuf, 0x0012, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x7://心跳间隔
			setdword( pbuf, 0x0001, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x9://监听号码
			setstring(pbuf, 0x0048, v.c_str(), v.length());
			++pnum;
			break;
		case 0x8:   //报警号码
		case 0x10:  //求助号码
			setstring(pbuf, 0x0040, v.c_str(), v.length());
			++pnum;
			break;
		case 0x15://中心短信号码
			setstring( pbuf, 0x0043, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x31:
			setword( pbuf, 0x0031, atoi(v.c_str()) ) ; // 补页新增电子围栏半径
			++ pnum ;
			break;
		case 0x41: // 设置车牌号
			setstring( pbuf, 0x0083, v.c_str(), v.length() ) ;
			++ pnum ;
			break;
		case 0x42:  // 设置车牌颜色
			setbyte( pbuf, 0x0084 , atoi(v.c_str()) ) ;
			pnum ++ ;
			break;
		case 0x100://TCP消息应答超时时间
			setdword( pbuf, 0x0002, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x101: // TCP重传次数
			setdword( pbuf, 0x0003, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x102://UDP
			setdword( pbuf, 0x0004, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x103: // UDP重传次数
			setdword( pbuf, 0x0005, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x104:// SMS应答超时时间
			setdword( pbuf, 0x0006, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x105: // SMS重传次数
			setdword( pbuf, 0x0007, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x106://备份APN
			setstring( pbuf, 0x0014, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x107:
			setstring( pbuf, 0x0015, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x108:
			setstring( pbuf, 0x0016, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x109://备份服务器IP
			setstring( pbuf, 0x0017, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x110://服务器UDP端口
			setdword( pbuf, 0x0019, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x111://汇报策略
			setdword( pbuf, 0x0020, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x112: // 位置汇报
			setdword( pbuf, 0x0021, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x113:
			setdword( pbuf, 0x0022, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x114://休眠时位置汇报时间间隔
			setdword( pbuf, 0x0027, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x115: // 紧急报警时汇报时间间隔
			setdword( pbuf, 0x0028, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x116: //
			setdword( pbuf, 0x0029, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x117://缺省距离汇报间隔，单位为米（m），>0
			setdword( pbuf, 0x002C, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x118:
			setdword( pbuf, 0x002D, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x119:
			setdword( pbuf, 0x002E, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x120:
			setdword( pbuf, 0x002F, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x121:
			setdword( pbuf, 0x0030, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x122://复位电话号码，可采用此电话号码拨打终端电话让终端复位
			setstring( pbuf, 0x0041, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x123://恢复出厂设置电话号码，可采用此电话号码拨打终端电话让终端恢复出厂设置
			setstring( pbuf, 0x0042, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x124: // 接收终端SMS文本报警号码
			setstring( pbuf, 0x0044, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x125: // 终端电话接听策略
			setdword( pbuf, 0x0045, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x126: // 每次最长通话时间
			setdword( pbuf, 0x0046, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x127: // 当月最长通话时间
			setdword( pbuf, 0x0047, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x128://最高时速
			setdword( pbuf, 0x0055, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x129:
			setdword( pbuf, 0x0056, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x130:
			setdword( pbuf, 0x0057, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x131://当天累计驾驶时间门限，单位为秒（s）
			setdword( pbuf, 0x0058, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x132:
			setdword( pbuf, 0x0059, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x133:
			setdword( pbuf, 0x005A, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x134://车辆所在省域ID
			setword( pbuf, 0x0081, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x135:
			setword( pbuf, 0x0082, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x136://图像/视频质量-1～10，1最好;
			setdword( pbuf, 0x0070, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x137:
			setdword( pbuf, 0x0071, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x138:
			setdword( pbuf, 0x0072, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x139:
			setdword( pbuf, 0x0073, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x140:
			setdword( pbuf, 0x0074, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x141://监管平台特权短信号码
			setstring( pbuf, 0x0049, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 0x142://报警屏蔽字
			setdword( pbuf, 0x0050, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x143://报警发送文本开关SMS开关
			setdword( pbuf, 0x0051, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x144://报警拍摄开关
			setdword( pbuf, 0x0052, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x145://报警拍摄存储标志
			setdword( pbuf, 0x0053, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x146://关键报警标志
			setdword( pbuf, 0x0054, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x147://车辆里程表读数
			setdword( pbuf, 0x0080, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x180: //定时拍照
			u32Val = atoi(v.c_str());
			u32Val = ((u32Val & 0xffff0000) << 1) | (u32Val & 0xffff);
			setdword( pbuf, 0x0064, u32Val) ;
		    pnum ++;
			break;
		case 0x181: //定距拍照
			u32Val = atoi(v.c_str());
			u32Val = ((u32Val & 0xffff0000) << 1) | (u32Val & 0xffff);
			setdword( pbuf, 0x0065, u32Val) ;
		    pnum ++;
			break;
		case 0x187: //速度来源设置
			setbyte(pbuf, 0xf108, atoi(v.c_str()));
			pnum++;
			break;
		case 0x190: //驾驶员登录拍照控制
			setword(pbuf, 0xf109, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 0x200: //事故疑点数据上报模式
			setbyte(pbuf, 0xf100, atoi(v.c_str()));
			pnum++;
			break;
		case 0x201: //急加速报警阀值
			binStr = "";
			if (base64.Decode(v.c_str(), v.length())) {
				binStr.assign(base64.GetBuffer(), base64.GetLength());
			}
			setstring(pbuf, 0xf00c, binStr.c_str(), binStr.length());
			pnum++;
			break;
		case 0x202: //急减速报警阀值
			binStr = "";
			if (base64.Decode(v.c_str(), v.length())) {
				binStr.assign(base64.GetBuffer(), base64.GetLength());
			}
			setstring(pbuf, 0xf00d, binStr.c_str(), binStr.length());
			pnum++;
			break;
		case 0x203: //车辆型号
			setstring(pbuf, 0xf10c, v.c_str(), v.length());
			pnum++;
			break;
		case 0x204: //VIN码
			setstring(pbuf, 0xf10d, v.c_str(), v.length());
			pnum++;
			break;
		case 0x205: //发动机号
			setstring(pbuf, 0xf10e, v.c_str(), v.length());
			pnum++;
			break;
		case 0x206: //发动机型号
			setstring(pbuf, 0xf10f, v.c_str(), v.length());
			pnum++;
			break;
		case 0x207: //空档滑行速度阀值
			setword(pbuf, 0xf000, atoi(v.c_str()));
			pnum++;
			break;
		case 0x208: //空档滑行时间阀值
			setword(pbuf, 0xf001, atoi(v.c_str()));
			pnum++;
			break;
		case 0x209: //空挡滑行转速阀值
			setword(pbuf, 0xf002, atoi(v.c_str()));
			pnum++;
			break;
		case 0x210: //发动机超转阀值
			setword(pbuf, 0xf003, atoi(v.c_str()));
			pnum++;
			break;
		case 0x211: //发动机超转持续时间阀值
			setword(pbuf, 0xf004, atoi(v.c_str()));
			pnum++;
			break;
		case 0x212: //超长怠速的时间阀值
			setword(pbuf, 0xf005, atoi(v.c_str()));
			pnum++;
			break;
		case 0x213: //怠速的定义阀值
			setword(pbuf, 0xf006, atoi(v.c_str()));
			pnum++;
			break;
		case 0x214: //怠速空调时间阀值
			setword(pbuf, 0xf007, atoi(v.c_str()));
			pnum++;
			break;
		case 0x216: //经济区转速上限
			setword(pbuf, 0xf00a, atoi(v.c_str()));
			pnum++;
			break;
		case 0x217: //经济区转速下限
			setword(pbuf, 0xf00b, atoi(v.c_str()));
			pnum++;
			break;
		case 0x218: //专属应用屏蔽字
			setword(pbuf, 0xf010, atoi(v.c_str()));
			pnum++;
			break;
		case 0x219: //特征系数自动修正开关
			setbyte(pbuf, 0xf011, atoi(v.c_str()));
			pnum++;
			break;
		case 0x300:  //超速报警预警差值 WORD
			setword( pbuf, 0x005b, atoi(v.c_str()) ) ;
			++ pnum ;
			break ;
		case 0x301: //疲劳驾驶预警差值
			setword( pbuf, 0x005c, atoi(v.c_str()) ) ;
			++ pnum ;
			break ;
		case 0x302: //特征系数
			setword(pbuf, 0xf101, atoi(v.c_str()));
			++pnum;
			break;
		case 0x303: //车轮每转脉冲数
			setword(pbuf, 0xf102, atoi(v.c_str()));
			++pnum;
			break;
		case 0x304: //油箱容量
			setword(pbuf, 0xf104, atoi(v.c_str()));
			++pnum;
			break;
		case 0x305: //位置信息汇报附加信息设置
			binStr.assign(32, '\0');
			if (base64.Decode(v.c_str(), v.length())) {
				binStr.insert(0, base64.GetBuffer(), base64.GetLength());
			}
			setstring(pbuf, 0xf105, binStr.c_str(), 32);
			++pnum;
			break;
		case 0x306: //门开关拍照控制
			setdword(pbuf, 0xf106, atoi(v.c_str()));
			pnum++;
			break;
		case 0x307: //终端外围传感配置
			setdword(pbuf, 0xf107, atoi(v.c_str()));
			pnum++;
			break;
		case 0x309: //分辨率
			setword(pbuf, 0xf10a, atoi(v.c_str()));
			pnum++;
			break;
		case 0x310: //车牌分类
			setstring(pbuf, 0xf10b, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1001a: //IC卡认证主服务器IP
			setstring(pbuf, 0x001a, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1001b: //IC卡认证主服务器TCP端口
			setdword(pbuf, 0x001b, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1001c: //IC卡认证主服务器UDP端口
			setdword(pbuf, 0x001c, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1001d: //IC卡认证备份服务器IP
			setstring(pbuf, 0x001d, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1005d: //碰撞报警参数
			setword(pbuf, 0x005d, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1005e: //侧翻报警参数
			setword(pbuf, 0x005e, atoi(v.c_str()));
			pnum++;
			break;
		case 0x10090: //GNSS 定位模式
			setbyte(pbuf, 0x0090, atoi(v.c_str()));
			pnum++;
			break;
		case 0x10091: //GNSS 波特率
			setbyte(pbuf, 0x0091, atoi(v.c_str()));
			pnum++;
			break;
		case 0x10092: //GNSS 模块详细定位数据输出频率
			setbyte(pbuf, 0x0092, atoi(v.c_str()));
			pnum++;
			break;
		case 0x10093: //GNSS 模块详细定位数据采集频率
			setdword(pbuf, 0x0093, atoi(v.c_str()));
			pnum++;
			break;
		case 0x10094: //GNSS 模块详细定位数据上传方式
			setbyte(pbuf, 0x0090, atoi(v.c_str()));
			pnum++;
			break;
		case 0x10095: //GNSS 模块详细定位数据上传设置
			setdword(pbuf, 0x0095, atoi(v.c_str()));
			pnum++;
			break;
		case 0x10100: //CAN 总线通道1 采集时间间隔
			setdword(pbuf, 0x0100, atoi(v.c_str()));
			pnum++;
			break;
		case 0x10101: //CAN 总线通道1 上传时间间隔
			setword(pbuf, 0x0101, atoi(v.c_str()));
			pnum++;
			break;
		case 0x10102: //CAN 总线通道2 采集时间间隔
			setdword(pbuf, 0x0102, atoi(v.c_str()));
			pnum++;
			break;
		case 0x10103: //CAN 总线通道2 上传时间间隔
			setword(pbuf, 0x0103, atoi(v.c_str()));
			pnum++;
			break;
		case 0x10110: //CAN 总线ID 单独采集设置
			binStr.assign(8, '\0');
			if (base64.Decode(v.c_str(), v.length())) {
				binStr.insert(0, base64.GetBuffer(), base64.GetLength());
			}
			setstring(pbuf, 0x0110, binStr.c_str(), 8);
			pnum++;
			break;
		case 0x1f008: //蓄电池电压报警上限阀值
			setword(pbuf, 0xf008, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f009: //蓄电池电压报警下限阀值
			setword(pbuf, 0xf009, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f00e: //蓄电池掉电报警阀值
			setword(pbuf, 0xf00e, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f00f: //终端订阅信息设置
			setdword(pbuf, 0xf00f, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f012: //扩展报警屏蔽字
			setdword(pbuf, 0xf012, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f013: //终端休眠持续时间
			setdword(pbuf, 0xf013, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f014: //终端休眠时间
			setdword(pbuf, 0xf014, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f015: //终端语音播报声音控制
			setbyte(pbuf, 0xf015, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f016: //夜间行车最高速度
			setdword(pbuf, 0xf016, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f017: //夜间连续驾驶时间门限
			setdword(pbuf, 0xf017, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f018: //夜间开始时间
			binBuf.resize(2);
			if (v.length() == 4 && Utils::hex2array(v, &binBuf[0]) == binBuf.size()) {
				setstring(pbuf, 0xf018, (char*)&binBuf[0], binBuf.size());
				pnum++;
			}
			break;
		case 0x1f019: //夜间结束时间
			binBuf.resize(2);
			if (v.length() == 4 && Utils::hex2array(v, &binBuf[0]) == binBuf.size()) {
				setstring(pbuf, 0xf019, (char*)&binBuf[0], binBuf.size());
				pnum++;
			}
			break;
		case 0x1f030: //运维短信权限电话号码1
			binBuf.resize(6);
			if (v.length() == 11 && Utils::hex2array(v, &binBuf[0]) == binBuf.size()) {
				setstring(pbuf, 0xf030, (char*)&binBuf[0], binBuf.size());
				pnum++;
			}
			break;
		case 0x1f031: //运维短信权限电话号码1
			binBuf.resize(6);
			if (v.length() == 11 && Utils::hex2array(v, &binBuf[0]) == binBuf.size()) {
				setstring(pbuf, 0xf031, (char*)&binBuf[0], binBuf.size());
				pnum++;
			}
			break;
		case 0x1f032: //运维短信权限电话号码1
			binBuf.resize(6);
			if (v.length() == 11 && Utils::hex2array(v, &binBuf[0]) == binBuf.size()) {
				setstring(pbuf, 0xf032, (char*)&binBuf[0], binBuf.size());
				pnum++;
			}
			break;
		case 0x1f033: //运维短信权限电话号码1
			binBuf.resize(6);
			if (v.length() == 11 && Utils::hex2array(v, &binBuf[0]) == binBuf.size()) {
				setstring(pbuf, 0xf033, (char*)&binBuf[0], binBuf.size());
				pnum++;
			}
			break;
		case 0x1f034: //短信平台特服号码
			setstring(pbuf, 0xf034, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f103: //脉冲系数
			setword(pbuf, 0xf103, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f110: //视频服务器APN
			setstring(pbuf, 0xf110, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f111: //视频服务器无线通信拨号用户名
			setstring(pbuf, 0xf111, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f112: //视频服务器无线通信拨号密码
			setstring(pbuf, 0xf112, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f113: //视频服务器地址，IP 或域名
			setstring(pbuf, 0xf113, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f114: //视频服务器端口
			setdword(pbuf, 0xf114, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f120: //第二平台主服务器APN
			setstring(pbuf, 0xf120, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f121: //第二中心平台主服务器无线通信拨号用户名
			setstring(pbuf, 0xf121, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f122: //第二中心平台主服务器无线通信拨号密码
			setstring(pbuf, 0xf122, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f123: //第二中心平台主服务器地址
			setstring(pbuf, 0xf123, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f124: //第二中心平台备份服务器APN
			setstring(pbuf, 0xf124, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f125: //第二中心平台备份服务器无线通信拨号用户名
			setstring(pbuf, 0xf125, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f126: //第二中心平台备份服务器无线通信拨号密码
			setstring(pbuf, 0xf126, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f127: //第二中心平台备份服务器地址,IP 或域名
			setstring(pbuf, 0xf127, v.c_str(), v.length());
			pnum++;
			break;
		case 0x1f128: //第二中心平台服务器TCP端口
			setdword(pbuf, 0xf128, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f129: //第二中心平台服务器UDP端口
			setdword(pbuf, 0xf129, atoi(v.c_str()));
			pnum++;
			break;
		case 0x1f12a: //双中心平台业务配置字
			setbyte(pbuf, 0xf12a, atoi(v.c_str()));
			pnum++;
			break;
		}
	}

	return true ;
}

unsigned char GbProtocolHandler::get_check_sum(const char *buf,int len)
{
	if(buf == NULL || len < 1)
		return 0;
	unsigned char check_sum = 0;
	for(int i = 0; i < len; i++)
	{
		check_sum ^= buf[i];
	}
	return check_sum;
}

string GbProtocolHandler::get_bcd_time(char bcd[6])
{
	string dest;
	char buf[4] = {0};

	sprintf(buf,"%02x",bcd[0]);

	// 20110308/150234
	unsigned int year = atoi(buf) + 2000;
	dest += uitodecstr(year);

	// memset(buf,0,4);
	sprintf(buf,"%02x",bcd[1]);
	dest += buf;

	sprintf(buf,"%02x",bcd[2]);
	dest += buf;
	dest += "/";

	sprintf(buf,"%02x",bcd[3]);
	dest += buf;
	sprintf(buf,"%02x",bcd[4]);
	dest += buf;
	sprintf(buf,"%02x",bcd[5]);
	dest += buf;
	return dest;
}

//20110304
string GbProtocolHandler::get_date()
{
	string str_date;
	char date[128] = {0};
	time_t t;
	time(&t);
	struct tm local_tm;
	struct tm *tm = localtime_r( &t, &local_tm ) ;

	sprintf(date, "%04d%02d%02d", tm->tm_year + 1900, tm->tm_mon + 1, tm->tm_mday);
	str_date = date;
	return str_date;
}
//050507
string GbProtocolHandler::get_time()
{
	string str_time;
	char time1[128] = {0};

	time_t t;
	time(&t);
	struct tm local_tm;
	struct tm *tm = localtime_r( &t, &local_tm ) ;

	sprintf(time1, "%04d%02d%02d%02d%02d%02d", tm->tm_year + 1900, tm->tm_mon + 1, tm->tm_mday,
			tm->tm_hour, tm->tm_min, tm->tm_sec);

	str_time = time1;
	return str_time;

}

void GbProtocolHandler::bin2hex(unsigned char *bin, size_t len, char *dst)
{
	size_t i;

	for(i = 0; i < len; ++i) {
		sprintf(dst + i * 2, "%02x", bin[i]);
	}
}
