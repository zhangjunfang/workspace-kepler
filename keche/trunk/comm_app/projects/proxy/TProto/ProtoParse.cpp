#include "ProtoParse.h"
#include "comlog.h"
#include <stdlib.h>

const char *get_type(unsigned short msg_type)
{
	switch(msg_type)
	{
	case	0x1001: return "UP_CONNECT_REQ";
	case	0x1002: return "UP_CONNECT_RSP";
	case	0x1003: return "UP_DISCONNECT_REQ";
	case	0x1004: return "UP_DISCONNECT_RSP";
	case	0x1005: return "UP_LINKTEST_REQ";
	case	0x1006: return "UP_LINKTEST_RSP";
	case	0x1007: return "UP_DISCONNECT_INFORM";
	case	0x1008: return "UP_CLOSELINK_INFORM";
	case	0x9001: return "DOWN_CONNECT_REQ";
	case	0x9002: return "DOWN_CONNECT_RSP";
	case	0x9003: return "DOWN_DISCONNECT_REQ";
	case	0x9004: return "DOWN_DISCONNECT_RSP";
	case	0x9005: return "DOWN_LINKTEST_REQ";
	case	0x9006: return "DOWN_LINKTEST_RSP";
	case	0x9007: return "DOWN_DISCONNECT_INFORM";
	case	0x9008: return "DOWN_CLOSELINK_INFORM ";
	case	0x9101: return "DOWN_TOTAL_RECV_BACK_MSG";
	case	0x1200: return "UP_EXG_MSG";
	case	0x9200: return "DOWN_EXG_MSG";
	case	0x1300: return "UP_PLATFORM_MSG";
	case	0x9300: return "DOWN_PLATFORM_MSG";
	case	0x1400: return "UP_WARN_MSG";
	case	0x9400: return "DOWN_WARN_MSG";
	case	0x1500: return "UP_CTRL_MSG";
	case	0x9500: return "DOWN_CTRL_MSG";
	case	0x1600: return "UP_BASE_MSG";
	case	0x9600: return "DOWN_BASE_MSG";
	case    0x1201: return "UP_EXG_MSG_REGISTER" ;
	case	0x1202: return "UP_EXG_MSG_REAL_LOCATION ";
	case	0x1203: return "UP_EXG_MSG_HISTORY_LOCATION";
	case	0x1205: return "UP_EXG_MSG_RETURN_STARTUP_ACK";
	case	0x1206: return "UP_EXG_MSG_RETURN_END_ACK";
	case	0x1207: return "UP_EXG_MSG_APPLY_FOR_MONITOR_STARTUP";
	case	0x1208: return "UP_EXG_MSG_APPLY_FOR_MONITOR_END";
	case	0x1209: return "UP_EXG_MSG_APPLY_HISGNSSDATA_REQ";
	case	0x120A: return "UP_EXG_MSG_REPORT_DRIVER_INFO_ACK";
	case	0x120B: return "UP_EXG_MSG_TAKE_EWAYBILL_ACK";
	case	0x120C: return "UP_EXG_MSG_REPORT_DRIVER_INFO";
	case	0x120D: return "UP_EXG_MSG_REPORT_EWAYBILL_INFO";
	case	0x9202: return "DOWN_EXG_MSG_CAR_LOCATION";
	case	0x9203: return "DOWN_EXG_MSG_HISTORY_ARCOSSAREA";
	case	0x9204: return "DOWN_EXG_MSG_CAR_INFO";
	case	0x9205: return "DOWN_EXG_MSG_RETURN_STARTUP";
	case	0x9206: return "DOWN_EXG_MSG_RETURN_END";
	case	0x9207: return "DOWN_EXG_MSG_APPLY_FOR_MONITOR_STARTUP_ACK";
	case	0x9208: return "DOWN_EXG_MSG_APPLY_FOR_MONITOR_END_ACK";
	case	0x9209: return "DOWN_EXG_MSG_APPLY_HISGNSSDATA_ACK";
	case	0x920A: return "DOWN_EXG_MSG_REPORT_DRIVER_INFO ";
	case	0x920B: return "DOWN_EXG_MSG_TAKE_EWAYBILL_REQ";
	case	0x1301: return "UP_PLATFORM_MSG_POST_QUERY_ACK";
	case	0x1302: return "UP_PLATFORM_MSG_INFO_ACK";
	case	0x9301: return "DOWN_PLATFORM_MSG_POST_QUERY_REQ";
	case	0x9302: return "DOWN_PLATFORM_MSG_INFO_REQ";
	case	0x1401: return "UP_WARN_MSG_URGE_TODO_ACK";
	case	0x1402: return "UP_WARN_MSG_ADPT_INFO";
	case	0x1403: return "UP_WARN_MSG_ ADPT_TODO_INFO";
	case	0x9401: return "DOWN_WARN_MSG_URGE_TODO_REQ";
	case	0x9402: return "DOWN_WARN_MSG_INFORM_TIPS";
	case	0x9403: return "DOWN_WARN_MSG_EXG_INFORM";
	case	0x1501: return "UP_CTRL_MSG_MONITOR_VEHICLE_ACK";
	case	0x1502: return "UP_CTRL_MSG_TAKE_PHOTO_ACK ";
	case	0x1503: return "UP_CTRL_MSG_TEXT_INFO_ACK";
	case	0x1504: return "UP_CTRL_MSG_TAKE_TRAVEL_ACK";
	case    0x1505: return "UP_CTRL_MSG_EMERGENCY_MONITORING_ACK" ;
	case	0x9501: return "DOWN_CTRL_MSG_MONITOR_VEHICLE_REQ";
	case	0x9502: return "DOWN_CTRL_MSG_TAKE_PHOTO_REQ";
	case	0x9503: return "DOWN_CTRL_MSG_TEXT_INFO";
	case	0x9504: return "DOWN_CTRL_MSG_TAKE_TRAVEL_REQ";
	case    0x9505: return "DOWN_CTRL_MSG_EMERGENCY_MONITORING_REQ" ;
	case	0x1601: return "UP_BASE_MSG_VEHICLE_ADDED_ACK";
	case	0x9601: return "DOWN_BASE_MSG_VEHICLE_ADDED";
	default:
		return "NULL";
	}
}

string ProtoParse::Decoder(const char*data,int len)
{
	string info;
	if(len < (int)sizeof(Header))
		return info;
	Header *header = (Header*)data;
	unsigned short msg_type = ntouv16(header->msg_type);
	info += get_type(ntouv16(header->msg_type));

	if(msg_type == UP_EXG_MSG || msg_type == DOWN_EXG_MSG)
	{
		ExgMsgHeader *msgheader = (ExgMsgHeader*) (data + sizeof(Header));
		info += ":";
		info += get_type(ntouv16(msgheader->data_type));
		//这样做的主要目的是为了防止有的运营商上行的数据不符合规定，导致内存越界。
		string car_no((const char*) msgheader->vehicle_no, 20);
		info += ":" + car_no;
	}
	else if(msg_type == UP_PLATFORM_MSG || msg_type == DOWN_PLATFORM_MSG)
	{
		UpPlatformMsg *header = (UpPlatformMsg*)(data + sizeof(Header));
		info += ":";
		info += get_type(ntouv16(header->data_type));
	}
	else if(msg_type == UP_WARN_MSG || msg_type == DOWN_WARN_MSG)
	{
		WarnMsgHeader *header = (WarnMsgHeader*) (data + sizeof(Header));
		info += ":";
		info += get_type(ntouv16(header->data_type));
		string car_no((const char*) header->vehicle_no, 20);
		info += ":" + car_no;

	}
	else if(msg_type == UP_CTRL_MSG || msg_type == DOWN_CTRL_MSG
			||  msg_type == UP_BASE_MSG ||  msg_type == DOWN_BASE_MSG )
	{
		BaseMsgHeader *header  = (BaseMsgHeader*) (data + sizeof(Header));
		info += ":";
		info += get_type(ntouv16(header->data_type));
		string car_no((const char*) header->vehicle_no, 20);
		info += ":" + car_no;
	}

	return info;
}

string ProtoParse::GetMacId(const char*data, int len)
{
	Header *header = (Header*) data;
	unsigned short msg_type = ntouv16(header->msg_type);
	string mac_id;

	if (msg_type == UP_EXG_MSG || msg_type == DOWN_EXG_MSG)
	{
		ExgMsgHeader *msgheader = (ExgMsgHeader*) (data + sizeof(Header));
		mac_id = get_mac_id(msgheader->vehicle_no, msgheader->vehicle_color);
	}
	else if (msg_type == UP_PLATFORM_MSG || msg_type == DOWN_PLATFORM_MSG)
	{

	}
	else if (msg_type == UP_WARN_MSG || msg_type == DOWN_WARN_MSG)
	{
		WarnMsgHeader *header = (WarnMsgHeader*) (data + sizeof(Header));
		mac_id = get_mac_id(header->vehicle_no, header->vehicle_color);
	}
	else if (msg_type == UP_CTRL_MSG || msg_type == DOWN_CTRL_MSG
			|| msg_type == UP_BASE_MSG || msg_type == DOWN_BASE_MSG )
	{
		BaseMsgHeader *header = (BaseMsgHeader*) (data + sizeof(Header));
		mac_id = get_mac_id(header->vehicle_no, header->vehicle_color);
	}

	return mac_id;
}

// 将请求和应答对应起来
static string GetMsgType( const unsigned short type )
{
	unsigned short data_type =  ntouv16( type ) ;
	// 处理响应和请求生成序列,0x8000
	if ( ! ( data_type & 0x8000 ) ) {
		data_type |= 0x8000 ;
	}
	return uitodecstr( data_type ) ;
}

string ProtoParse::GetCommandId(const char *data,int len)
{
	Header *header = (Header*) data;
	unsigned short msg_type = ntouv16(header->msg_type);
	string info;

	if (msg_type == UP_EXG_MSG || msg_type == DOWN_EXG_MSG)
	{
		ExgMsgHeader *msgheader = (ExgMsgHeader*) (data + sizeof(Header));
		info = get_mac_id(msgheader->vehicle_no, msgheader->vehicle_color);
		info += "_" + GetMsgType( msgheader->data_type ) ;
	}
	else if (msg_type == UP_PLATFORM_MSG || msg_type == DOWN_PLATFORM_MSG)
	{

	}
	else if (msg_type == UP_WARN_MSG || msg_type == DOWN_WARN_MSG)
	{
		WarnMsgHeader *header = (WarnMsgHeader*) (data + sizeof(Header));
		info = get_mac_id(header->vehicle_no, header->vehicle_color);
		info += "_" + GetMsgType( header->data_type );
	}
	else if (msg_type == UP_CTRL_MSG || msg_type == DOWN_CTRL_MSG
			|| msg_type == UP_BASE_MSG || msg_type == DOWN_BASE_MSG )
	{
		BaseMsgHeader *header = (BaseMsgHeader*) (data + sizeof(Header));
		info = get_mac_id(header->vehicle_no, header->vehicle_color);
		info += "_" + GetMsgType( header->data_type );
	}

	return info;
}

void ProtoParse::BuildHeader( Header &header, unsigned int msg_len, unsigned int msg_seq, unsigned int msg_type , unsigned int access_code )
{
	header.msg_len  	= ntouv32(msg_len) ;
	header.msg_seq  	= ntouv32(msg_seq) ;
	header.msg_type 	= ntouv16(msg_type) ;
	header.access_code  = ntouv32(access_code) ;
}

//////////////////////////////////////////// C5BCoder /////////////////////////////////////////////////
// 5b编转换
C5BCoder::C5BCoder()
{
	_data = NULL ;
	_len  = 0 ;
	_temp = NULL ;
}

C5BCoder::~C5BCoder()
{
	if ( _temp != NULL ) {
		delete [] _temp ;
		_temp  = NULL ;
		_len   = 0 ;
	}
}

// 转换编码
bool C5BCoder::Encode( const char *data, const int len )
{
	if( data[0] != 0x5b || data[len-1] != 0x5d )
	{
		return false ;
	}

	_len  = len * 2 ;
	if ( _len > MAX_BUFF )
	{
		if ( _temp != NULL )
			delete [] _temp ;

		_temp = new char[ _len + 1 ] ;
		memset( _temp, 0 , _len + 1 ) ;

		_data = _temp ;
	}
	else
	{
		memset( _buf, 0 , MAX_BUFF ) ;
		_data = _buf ;
	}
	_data[0] = data[0] ;

	int offset = 1 ;
	for( int i = 1; i < len - 1; i++ )
	{
		if ( data[i] == 0x5b ) {
			_data[offset++] = 0x5a ;
			_data[offset++] = 0x01 ;
		} else if ( data[i] == 0x5a ) {
			_data[offset++] = 0x5a ;
			_data[offset++] = 0x02 ;
		} else if ( data[i] == 0x5d ) {
			_data[offset++] = 0x5e ;
			_data[offset++] = 0x01 ;
		} else if ( data[i] == 0x5e ) {
			_data[offset++] = 0x5e ;
			_data[offset++] = 0x02 ;
		} else {
			_data[offset++] = data[i] ;
		}
	}
	_data[offset] = data[len-1] ;

	_len = offset + 1 ;

	return true ;
}

// 解码
bool C5BCoder::Decode( const char *data, const int len )
{
	if( data[0] != 0x5b || data[len-1] != 0x5d )
	{
		return false ;
	}

	_len  = len ;

	if ( _len > MAX_BUFF )
	{
		if ( _temp != NULL )
			delete [] _temp ;

		_temp = new char[ _len + 1 ] ;
		memset( _temp, 0 , _len + 1 ) ;

		_data = _temp ;
	}
	else
	{
		memset( _buf, 0 , MAX_BUFF ) ;
		_data = _buf ;
	}
	_data[0] = data[0] ;

	int offset = 1 ;
	for(int i = 1; i < len - 1; i++)
	{
		if (data[i] == 0x5a && data[i+1] == 0x01)
		{
			_data[offset++] = 0x5b;
			i++;
		}
		else if (data[i] == 0x5a && data[i+1] == 0x02)
		{
			_data[offset++] = 0x5a;
			i++;
		}
		else if (data[i] == 0x5e && data[i+1] == 0x01)
		{
			_data[offset++] = 0x5d;
			i++;
		}
		else if (data[i] == 0x5e && data[i+1] == 0x02)
		{
			_data[offset++] = 0x5e;
			i++;
		}
		else
		{
			_data[offset++] = data[i];
		}
	}
	_data[offset] = data[len-1] ;

	_len = offset + 1 ;

	return true ;
}

// 取得解码的长度
const int C5BCoder::GetSize()
{
	return _len ;
}

// 取得数据
const char * C5BCoder::GetData()
{
	return _data ;
}

/////////////////////////////////  针对循环加密处理数据 ///////////////////////////////////////////
bool CEncrypt::encrypt( unsigned int M1, unsigned int IA1, unsigned int IC1, unsigned char *buffer, unsigned int size )
{
	// 处理加密数据
	Header *header = ( Header *) buffer ;
	if ( header->encrypt_flag == 0 ) {
		return false ;
	}

	// 加密只加密数据体
	int len = size - sizeof(Header) - sizeof(Footer) ;
	if ( len <= 0 || len > BUFF_MB ) {
		return false ;
	}

	// 取得加密的KEY值
	unsigned int key = ntouv32( header->encrypt_key ) ;
	if ( key == 0 ) {
		key = 1 ;
	}

	// 去掉头部数据
	unsigned char *ptr = ( unsigned char *)( buffer + sizeof(Header) ) ;
	// 处理加密
	unsigned int mkey = M1;
	if (0==mkey) mkey=1;
	
	int i = 0 ;
	// 开始加密处理
	while ( i < len ) {
		key = IA1 * ( key % mkey) + IC1  ;
		ptr[i++] ^= (unsigned char)( (key>>20) & 0xFF ) ;
	}
	return true ;
}

unsigned int CEncrypt::rand_key( void )
{
	srand( time(NULL) ) ;
	// 产生随机数
	return rand() ;
}
