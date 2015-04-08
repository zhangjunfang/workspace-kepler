#include "GbProtocolHandler.h"
#include <BaseTools.h>
#include <tools.h>
#include <comlog.h>
#include <arpa/inet.h>

#include <Base64.h>

#include "../tools/utils.h"

#define  MAX_DWORD_INVALID   0xFFFFFFFF
#define  MAX_WORD_INVALID    0xFFFF

static const string InnerKeyWords = string("{} ,:\0", 6);

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
					getCommonExtend((unsigned char*)append_data + cur + 2, amlen, mp);
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
	const char *ptr    = buf ;
	unsigned char nlen = (unsigned char)(*ptr) ;
	ptr += 1 ;

	safe_memncpy( info.drivername, ptr , nlen ) ;
	ptr += nlen ;
	if ( ptr > buf+len ) {
		return false ;
	}

	safe_memncpy( info.driverid , ptr , 20 ) ;
	ptr += 20 ;
	if ( ptr > buf+len ) {
		return false ;
	}

	safe_memncpy( info.driverorgid , ptr, 40 ) ;
	ptr += 40 ;
	if ( ptr > buf+len ) {
		return false ;
	}

	nlen = (unsigned char)(*ptr ) ;
	ptr += 1 ;

	safe_memncpy( info.orgname, ptr, nlen ) ;
	ptr += nlen ;
	if ( ptr > buf+len ) {
		return false ;
	}
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

	// 驾驶员姓名
	dstr += ",110:" ;
	dstr += info.drivername;

    //驾驶员身份证编码
    dstr += ",111:" ;
    dstr += info.driverid;

    //从业资格证编码
    dstr += ",112:" ;
    dstr +=info.driverorgid ;

    //发证机构名称
    dstr += ",113:";
    dstr += info.orgname;
    dstr += "}";

    return dstr;
}

//flag 0:读取，1设置
bool GbProtocolHandler::ConvertGetPara(char *buf, int buf_len, string &data) {
	/***********张鹤高修改8-9*************************************/
	int curn = sizeof(GBheader) + sizeof(short);

	CBase64 base64;
	string binStr;

	unsigned short pnum = 0;

	unsigned long pkey = 0;
	unsigned char plen = 0;
	char * pbuf;

	map<string, string> mp;

	pnum = buf[curn++];
	for (int i = 0; i < pnum && curn < buf_len; ++i) {
		pkey = getdword(buf + curn);
		curn += sizeof(int);
		plen = (unsigned char) buf[curn++];
		pbuf = buf + curn;

		switch (pkey) {
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
		case 0x0010:	//APN
			addmapkey("3", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0011:
			addmapkey("4", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0012:
			addmapkey("5", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0013:
			addmapkey("2", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0014:	//备份APN
			addmapkey("106", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0015:
			addmapkey("107", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0016:
			addmapkey("108", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0017:	//备份IP
			addmapkey("109", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0018:
			addmapkey("1", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0019:  // 根据内部协议与外部协议对应关系处理
			addmapkey("110", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0020:
			addmapkey("111", uitodecstr(getdword(buf + curn)), mp);
			break;
	   case 0x0021:
		   addmapkey( "112" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0022:
		   addmapkey( "113" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0027:
		   addmapkey( "114" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0028:
		   addmapkey( "115" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0029:
		   addmapkey( "116" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x002C:
		   addmapkey( "117" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x002D:
		   addmapkey( "118" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x002E:
		   addmapkey( "119" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x002F:
		   addmapkey( "120" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0030:
		   addmapkey( "121" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0031:  // 电子围栏半径
		   addmapkey( "31"  , uitodecstr(getword(buf+curn))  , mp ) ;
		   break ;
	   case 0x0040://监控平台电话号码
		   addmapkey("10" , string(pbuf, strnlen(pbuf, plen)), mp);
		   break;
	   case 0x0041://复位电话号码
		   addmapkey("122" , string(pbuf, strnlen(pbuf, plen)), mp);
		   break;
	   case 0x0042:
		   addmapkey("123" , string(pbuf, strnlen(pbuf, plen)), mp);
		   break;
	   case 0x0043:
		   addmapkey("15" , string(pbuf, strnlen(pbuf, plen)), mp);
		   break;
	   case 0x0044:
		   addmapkey("124" , string(pbuf, strnlen(pbuf, plen)), mp);
		   break;
	   case 0x0045:
		   addmapkey( "125" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0046:
		   addmapkey( "126" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0047:
		   addmapkey( "127" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0048:
		   addmapkey("9" , string(pbuf, strnlen(pbuf, plen)), mp);
		   break;
	   case 0x0049://监控平台特权短信号码
		   addmapkey("141" , string(pbuf, strnlen(pbuf, plen)), mp);
		   break;
	   case 0x0050:
		   addmapkey( "142" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0051:
		   addmapkey( "143" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0052:
		   addmapkey( "144" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0053:
		   addmapkey( "145" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0054:
		   addmapkey( "146" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0055:
		   addmapkey( "128" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0056:
		   addmapkey( "129" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0057:
		   addmapkey( "130" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0058:
		   addmapkey( "131" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0059:
		   addmapkey( "132" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x005A:
		   addmapkey( "133" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0064: //定时拍照
		   addmapkey( "180" , uitodecstr(getdword(buf+curn)) , mp ) ;
		   break;
	   case 0x0065: //定距拍照
		   addmapkey( "181" , uitodecstr(getdword(buf+curn)) , mp ) ;
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
			addmapkey("41", Utils::filter(string(pbuf, plen), InnerKeyWords), mp);
			break;
		case 0x0084: // 车牌颜色
			addmapkey("42", ustodecstr(buf[curn]), mp);
			break;
		case 0x005B: // 超速报警预警差值
			addmapkey("300", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0x005C: // 特征系数
			addmapkey("301", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0x005D: // 特征系数
			addmapkey("302", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0x005E: // 车轮每转脉冲数
			addmapkey("303", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0x005F: // 油箱容量
			addmapkey("304", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0x0060: // 位置信息汇报附加信息设置
			binStr = "";
			if (base64.Encode(pbuf, plen)) {
				binStr.append(base64.GetBuffer(), base64.GetLength());
			}
			addmapkey("305", binStr, mp);
			break;
		case 0x0061: // 门开关拍照控制
			addmapkey("306", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0062: // 终端外围传感配置
			addmapkey("307", uitodecstr(getdword(buf + curn)), mp);
			break;
		case 0x0063: // 盲区补报模式
			addmapkey("308", ustodecstr(getdword(buf + curn) + 1), mp);
			break;
		case 0x0066: // 速度来源VSS还是GPS 一个位的数据
			addmapkey("187", ustodecstr((unsigned char) (*(buf + curn))), mp);
			break;
		case 0x0067: // ToDo: 驾驶员登陆控制信息，宇通新增
			addmapkey("190", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0x0075: // 分辩率
			addmapkey("309", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0x0085: // 车牌分类
			addmapkey("310", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0023: // 事故疑点数据上报模式
			addmapkey("200", ustodecstr((unsigned char) (*(buf + curn))), mp);
			break;
		case 0xf00b: // 急加速报警阀值
			binStr = "";
			if (base64.Encode(pbuf, plen)) {
				binStr.append(base64.GetBuffer(), base64.GetLength());
			}
			addmapkey("201", binStr, mp);
			break;
		case 0xf00c: // 急减速报警阀值
			binStr = "";
			if (base64.Encode(pbuf, plen)) {
				binStr.append(base64.GetBuffer(), base64.GetLength());
			}
			addmapkey("202", binStr, mp);
			break;
		case 0x0086: // 车辆型号（工信部公告目录）
			addmapkey("203", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0087: // VIN码
			addmapkey("204", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0088: // 发动机号
			addmapkey("205", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0x0089: // 发动机型号
			addmapkey("206", string(pbuf, strnlen(pbuf, plen)), mp);
			break;
		case 0xf000: // 空档滑行速度阀值
			addmapkey("207", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf001: // 空档滑行时间阀值
			addmapkey("208", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf002: // 空挡滑行转速阀值
			addmapkey("209", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf003: // 发动机超转阀值
			addmapkey("210", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf004: // 发动机超转持续时间阀值
			addmapkey("211", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf005: // 超长怠速的时间阀值
			addmapkey("212", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf006: // 怠速的定义阀值
			addmapkey("213", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf007: // 怠速空调时间阀值
			addmapkey("214", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf008: // 蓄电池电压报警电压阀值
			addmapkey("215", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf009: // 经济区转速上限
			addmapkey("216", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf00a: // 经济区转速下限
			addmapkey("217", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf010:
			addmapkey("218", ustodecstr(getword(buf + curn)), mp);
			break;
		case 0xf011:
			addmapkey("219", ustodecstr((unsigned char) (*(buf + curn))), mp);
			break;
		default:
			break;
		}

		curn += plen;
	}
	data = "{TYPE:0,RET:0," + buildmapcommand(mp) + "} \r\n";

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
	string binStr;

	string strVal;
	string k,v;
	int uskey = 0;

	pnum = 0 ;

	//用来保持参数列表
	vector<string> vec_v;
	typedef map<string, string>::iterator MapIter;

	for (MapIter p = p_kv_map.begin(); p != p_kv_map.end(); ++p)
	{
		k = (string) p->first;
		v = (string) p->second;
		if(v.size()>256)
			continue;

		uskey = atoi(k.c_str());
		switch(uskey)
		{
		case 1://tcp port
			setdword( pbuf, 0x0018, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 2://IP
			setstring(pbuf, 0x0013, v.c_str(), v.length());
			pnum++;
			break;
		case 3://APN
			setstring( pbuf, 0x0010, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 4://APN username
			setstring( pbuf, 0x0011, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 5://APN pwd
			setstring( pbuf, 0x0012, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 7://心跳间隔
			setdword( pbuf, 0x0001, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 9://监听号码
			setstring( pbuf, 0x0048, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 8:
		case 10:  //监控平台电话号码
			setstring(pbuf, 0x0040, v.c_str(), v.length());
			pnum++;
			break;
		case 15://中心短信号码
			setstring( pbuf, 0x0043, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 18://设置GPS数据回传间隔
			setdword( pbuf, 0x0029, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 31:
			setword( pbuf, 0x0031, atoi(v.c_str()) ) ; // 补页新增电子围栏半径
			++ pnum ;
			break;
		case 41: // 设置车牌号
			setstring( pbuf, 0x0083, v.c_str(), v.length() ) ;
			++ pnum ;
			break;
		case 42:  // 设置车牌颜色
			setbyte( pbuf, 0x0084 , atoi(v.c_str()) ) ;
			pnum ++ ;
			break;
		case 100://TCP消息应答超时时间
			setdword( pbuf, 0x0002, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 101: // TCP重传次数
			setdword( pbuf, 0x0003, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 102://UDP
			setdword( pbuf, 0x0004, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 103: // UDP重传次数
			setdword( pbuf, 0x0005, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 104:// SMS应答超时时间
			setdword( pbuf, 0x0006, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 105: // SMS重传次数
			setdword( pbuf, 0x0007, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 106://备份APN
			setstring( pbuf, 0x0014, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 107:
			setstring( pbuf, 0x0015, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 108:
			setstring( pbuf, 0x0016, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 109://备份服务器IP
			setstring( pbuf, 0x0017, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 110://服务器UDP端口
			setdword( pbuf, 0x0019, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 111://汇报策略
			setdword( pbuf, 0x0020, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 112: // 位置汇报
			setdword( pbuf, 0x0021, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 113:
			setdword( pbuf, 0x0022, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 114://休眠时位置汇报时间间隔
			setdword( pbuf, 0x0027, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 115: // 紧急报警时汇报时间间隔
			setdword( pbuf, 0x0028, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 116: //
			setdword( pbuf, 0x0029, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 117://缺省距离汇报间隔，单位为米（m），>0
			setdword( pbuf, 0x002C, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 118:
			setdword( pbuf, 0x002D, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 119:
			setdword( pbuf, 0x002E, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 120:
			setdword( pbuf, 0x002F, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 121:
			setdword( pbuf, 0x0030, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 122://复位电话号码，可采用此电话号码拨打终端电话让终端复位
			setstring( pbuf, 0x0041, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 123://恢复出厂设置电话号码，可采用此电话号码拨打终端电话让终端恢复出厂设置
			setstring( pbuf, 0x0042, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 124: // 接收终端SMS文本报警号码
			setstring( pbuf, 0x0044, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 125: // 终端电话接听策略
			setdword( pbuf, 0x0045, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 126: // 每次最长通话时间
			setdword( pbuf, 0x0046, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 127: // 当月最长通话时间
			setdword( pbuf, 0x0047, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 128://最高时速
			setdword( pbuf, 0x0055, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 129:
			setdword( pbuf, 0x0056, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 130:
			setdword( pbuf, 0x0057, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 131://当天累计驾驶时间门限，单位为秒（s）
			setdword( pbuf, 0x0058, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 132:
			setdword( pbuf, 0x0059, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 133:
			setdword( pbuf, 0x005A, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 134://车辆所在省域ID
			setword( pbuf, 0x0081, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 135:
			setword( pbuf, 0x0082, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 136://图像/视频质量-1～10，1最好;
			setdword( pbuf, 0x0070, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 137:
			setdword( pbuf, 0x0071, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 138:
			setdword( pbuf, 0x0072, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 139:
			setdword( pbuf, 0x0073, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 140:
			setdword( pbuf, 0x0074, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 141://监管平台特权短信号码
			setstring( pbuf, 0x0049, v.c_str(), v.length() ) ;
			pnum ++;
			break;
		case 142://报警屏蔽字
			setdword( pbuf, 0x0050, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 143://报警发送文本开关SMS开关
			setdword( pbuf, 0x0051, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 144://报警拍摄开关
			setdword( pbuf, 0x0052, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 145://报警拍摄存储标志
			setdword( pbuf, 0x0053, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 146://关键报警标志
			setdword( pbuf, 0x0054, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 147://车辆里程表读数
			setdword( pbuf, 0x0080, atoi(v.c_str()) ) ;
			pnum ++;
			break;
		case 180: //定时拍照
			setdword( pbuf, 0x0064, atoi(v.c_str()) ) ;
		    pnum ++;
			break;
		case 181://定距拍照
			setdword( pbuf, 0x0065, atoi(v.c_str()) ) ;
		    pnum ++;
			break;
		case 187: // 设置VSS速度优先还是GPS优先
			setbyte( pbuf, 0x0066 , atoi(v.c_str()) ) ;
			++ pnum ;
			break ;
		case 190: // ToDo: 设置驾驶员登陆拍照控制,宇通新增
			setword( pbuf, 0x0067, atoi(v.c_str()) ) ;
			++ pnum ;
			break ;
		case 200: // 事故疑点
			setbyte( pbuf, 0x0023 , atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 201: // 急加速报警阀值
			binStr = "";
			if (base64.Decode(v.c_str(), v.length())) {
				binStr.append(base64.GetBuffer(), base64.GetLength());
			}
			setstring(pbuf, 0xf00b, binStr.c_str(), binStr.length());
			pnum++;
			break;
		case 202: // 急减速报警阀值
			binStr = "";
			if (base64.Decode(v.c_str(), v.length())) {
				binStr.append(base64.GetBuffer(), base64.GetLength());
			}
			setstring(pbuf, 0xf00c, binStr.c_str(), binStr.length());
			pnum++;
			break;
		case 203: // 车辆型号
			setstring(pbuf, 0x0086, v.c_str(), v.length());
			++pnum;
			break;
		case 204: // VIN码
			setstring(pbuf, 0x0087, v.c_str(), v.length());
			++pnum;
			break;
		case 205: // 发动机号
			setstring(pbuf, 0x0088, v.c_str(), v.length());
			++pnum;
			break;
		case 206: // 发动机型号
			setstring(pbuf, 0x0089, v.c_str(), v.length());
			++pnum;
			break;
		case 207: // 空档滑行速度阀值
			setword( pbuf, 0xf000, atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 208: // 空档滑行时间阀值
			setword( pbuf, 0xf001, atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 209: // 空挡滑行转速阀值
			setword( pbuf, 0xf002, atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 210: // 发动机超转阀值
			setword( pbuf, 0xf003, atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 211: // 发动机超转持续时间阀值
			setword( pbuf, 0xf004, atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 212: // 超长怠速的时间阀值
			setword( pbuf, 0xf005, atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 213: // 怠速的定义阀值
			setword( pbuf, 0xf006, atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 214: // 怠速空调时间阀值
			setword( pbuf, 0xf007, atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 215: // 蓄电池电压报警电压阀值（V）
			setword( pbuf, 0xf008, atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 216: // 经济区转速上限
			setword( pbuf, 0xf009, atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 217: // 经济区转速下限
			setword( pbuf, 0xf00a, atoi(v.c_str()) ) ;
			++ pnum ;
			break;
		case 300:  // 超速报警预警差值 WORD
			setword( pbuf, 0x005B, atoi(v.c_str()) ) ;
			++ pnum ;
			break ;
		case 301: // 疲劳驾驶预警差值
			setword( pbuf, 0x005C, atoi(v.c_str()) ) ;
			++ pnum ;
			break ;
		case 302: // 特征系数
			setword( pbuf, 0x005D, atoi(v.c_str()) ) ;
			++ pnum ;
			break ;
		case 303: // 车轮每转脉冲数
			setword( pbuf, 0x005E, atoi(v.c_str()) ) ;
			++ pnum ;
			break ;
		case 304: // 油箱容量
			setword( pbuf, 0x005F, atoi(v.c_str()) ) ;
			++ pnum ;
			break ;
		case 305:  // 位置信息汇报附加设置
			binStr.assign(32, '\0');
			if (base64.Decode(v.c_str(), v.length())) {
				binStr.insert(0, base64.GetBuffer(), base64.GetLength());
			}
			setstring(pbuf, 0x0060, binStr.c_str(), 32);
			++pnum;
			break ;
		case 306: // 门开关拍照控制
			setdword( pbuf, 0x0061, atoi(v.c_str())) ;
			++ pnum ;
			break ;
		case 307: // 终端外围传感配置
			setdword( pbuf, 0x0062, atoi(v.c_str())) ;
			++ pnum ;
			break ;
		case 308: // 盲区补报模式
			setdword( pbuf, 0x0063, atoi(v.c_str())-1 ) ;
			++ pnum ;
			break ;
		case 309: // 分辩率
			setword( pbuf, 0x0075, atoi(v.c_str()) ) ;
			++ pnum ;
			break ;
		case 310: // 车牌分类
			setbytes( pbuf, 0x0085, (unsigned char*)v.c_str(), v.length(), 12 ) ;
			++ pnum ;
			break ;
		default:
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
