/*
 * pconvert.cpp
 *
 *  Created on: 2012-3-2
 *      Author: think
 */

#include <stdio.h>
#include "pconvert.h"
#include <comlog.h>
#include <tools.h>
#include "pccutil.h"
#include "interface.h"
#include <Base64.h>

PConvert::PConvert()
{
	_pEnv = NULL ;
}

PConvert::~PConvert()
{

}

bool PConvert::split2map( const std::string &s , MapString &val )
{
	vector<string>  vec ;
	// 处理所有逗号分割处理
	if ( ! splitvector( s , vec, "," , 0 ) ) {
		return false ;
	}

	string temp  ;
	size_t pos = 0 , end = 0 ;
	// 解析参数
	for ( pos = 0 ; pos < vec.size(); ++ pos ) {
		temp = vec[pos] ;
		end  = temp.find( ":" ) ;
		if ( end == string::npos ) {
			continue ;
		}
		val.insert( pair<string,string>( temp.substr(0,end), temp.substr( end+1 ) ) ) ;
	}
	// 解析出监控平台参数部分
	return ( ! val.empty() ) ;
}

// 解析监控平台的参数
bool PConvert::parse_jkpt_value( const std::string &param, MapString &val )
{
	// {TYPE:0,104:京A10104,201:701116,202:1,15:0,26:5,1:69782082,2:23947540,3:5,4:20110516/153637,5:6,21:0}
	size_t pos = param.find("{") ;
	if ( pos == string::npos ) {
		return false ;
	}

	size_t end = param.find("}", pos ) ;
	if ( end == string::npos || end < pos + 1 ) {
		return false ;
	}
	// 解析出监控平台参数部分
	return split2map( param.substr( pos+1, end-pos-1 ), val ) ;
}

// 取得头数据
bool PConvert::get_map_header( const std::string &param, MapString &val, int &ntype )
{
	if ( ! parse_jkpt_value( param, val ) ) {
		OUT_ERROR( NULL, 0, NULL, "parse data %s failed", param.c_str() ) ;
		return false ;
	}

	if ( ! get_map_integer( val, "TYPE", ntype ) ) {
		OUT_ERROR( NULL, 0, NULL, "get data %s type failed", param.c_str() ) ;
		return false ;
	}

	return true ;
}

bool PConvert::get_map_string( MapString &map, const std::string &key , std::string &val )
{
	MapString::iterator it = map.find( key ) ;
	if ( it == map.end() ) {
		return false ;
	}
	val = it->second ;
	return true ;
}

bool PConvert::get_map_integer( MapString &map, const std::string &key , int &val )
{
	MapString::iterator it = map.find( key ) ;
	if ( it == map.end() ) {
		return false ;
	}
	val = atoi( it->second.c_str() ) ;
	return true ;
}

bool PConvert::initenv( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	char szbuf[1024] = {0};
	// 读取图片的路径
	if ( pEnv->GetString( "local_picpath", szbuf ) ) {
		_picdir = szbuf ;
	}

	return true ;
}

// 转换U_REPT指令的数据
void PConvert::convert_urept( const string &macid, const string &val, DataBuffer &buf , bool bcall )
{
	char szphone[17] = {0} ;
	if ( ! gettermidbymacid( macid, szphone ) ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "get termid by macid failed" ) ;
		return ;
	}

	int ntype = 0 ;
	MapString map ;
	if ( ! get_map_header( val, map, ntype ) ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "PConvert::convert_urept get map header failed" ) ;
		return;
	}

	// 记录手机号与终端号的对应关系
	_cache.AddSession( szphone, macid ) ;

	switch(ntype) {
		case 0:  //路径和报警数据    位置汇报
		case 1:
			{
				// ToDo: 将位置上报信息转为对应的协议
				convertgps( szphone , map, buf , bcall ) ;
			}
			break;
		case 2: // 行驶记录仪
			{
				// ToDo:
			}
			break;
		case 3: // 拍照上传图片处理，这里拍照特殊处理一下
			{
				string temp ;
				// 取图片的URL的相对地址
				if ( ! get_map_string( map, "125" , temp ) ) {
					get_map_string( map, "203", temp ) ;
				}
				// 如果没有取着图片的地址
				if ( temp.empty() ) {
					OUT_ERROR( NULL, 0, macid.c_str(), "upload take photo get file path failed" ) ;
					return ;
				}
				int nval = 0 ;
				if ( ! get_map_integer( map, "124",  nval ) ) {
					OUT_ERROR( NULL, 0, macid.c_str(), "get camera id failed" ) ;
					return ;
				}

				_PhotoRsp rsp ;

				unsigned int camerid = 0 ;
				// 按位进行处理
				if ( nval > 0 && nval < 9 ) {
					setbit( camerid, nval-1 )  ;
				} else {
					camerid = 1 ;
				}

				rsp.cmd = htons( 0x0381 ) ;
				rsp.cameraid = camerid ;
				// 将获到GPS位置数据填充入结构体中
				buildgps( map, rsp.gps ) ;

				// 从本地读取一次图片
				int piclen 	  = 0 ;
				char *picdata = NULL ;
				// 如果本地图片路径为空就从HTTP取图片
				if ( ! _picdir.empty() ) {
					char szpath[1024] = {0};
					sprintf( szpath, "%s/%s", _picdir.c_str(), temp.c_str() ) ;
					picdata = ReadFile( szpath , piclen ) ;
				}

				// 消息序号ID
				unsigned int seqid = get_next_seq() ;
				// 构建消息头部
				buildheader( buf, szphone, piclen + sizeof(_PhotoRsp) , 0 , seqid ) ;

				// 直接从本机读取图片
				rsp.data_len = htons( piclen ) ;
				buf.writeBlock( &rsp, sizeof(_PhotoRsp) ) ;

				// 如果文件在本机直接读取
				if ( picdata != NULL && piclen > 0 ) {
					// 保存文件数据
					buf.writeBlock( picdata, piclen ) ;
					FreeBuffer( picdata ) ;
					OUT_SEND( NULL, 0, macid.c_str() , "upload pic picture length %d, path: %s" , piclen , temp.c_str() ) ;
				} else {  // 从网络读取文件
					// 取得新序号的ID值
					char szid[128] = {0};
					sprintf( szid, "%u", seqid ) ;
					// 将需要的数据保存对应的缓存中，等取到照片数据就返回
					_pEnv->GetMsgCache()->AddData( szid, buf.getBuffer(), buf.getLength() ) ;
					buf.resetBuf() ;

					// 重新放入新序号队列中
					// 从网络中读取图片数据
					((IMsgClient*)_pEnv->GetMsgClient())->LoadUrlPic( seqid , temp.c_str() ) ;

					OUT_SEND( NULL, 0, macid.c_str() , "get picture path %s", temp.c_str() ) ;
				}
			}
			break ;
		case 8: // 主动上报驾驶员信息采集
		   {
			   // ToDo:
		   }
		   break;
		case 35: // 主动上报电子运单
			{
				// ToDo:
			}
			break;
		case 36: // 终端注册,这里暂时只处理终端鉴权
			{
				// ToDo:
			}
			break;
		case 38: //终端鉴权后，要异步回调通过http,通过pcc 提交到Service 请求sim卡号,终端类型
			{
				// ToDo:
			}
			break;
		default:
			{
				OUT_ERROR( NULL, 0, macid.c_str(), "PConvert::convert_urept not support %s", val.c_str() ) ;
			}
			break ;
	}
}

// 处理所有通用应答消息
void PConvert::convert_comm( const string &seqid, const string &macid, const string &val, DataBuffer &buf )
{
	char szphone[17] = {0} ;
	if ( ! gettermidbymacid( macid, szphone ) ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "get termid by macid failed" ) ;
		return ;
	}

	MapString map ;
	// 解析数据
	if ( ! parse_jkpt_value( val , map ) ) {
		OUT_ERROR( NULL, 0, NULL, "parse data %s failed", val.c_str() ) ;
		return;
	}

	int ret = 0 ;
	if ( ! get_map_integer( map, "RET", ret ) ){
		OUT_ERROR( NULL, 0, macid.c_str(), "PConvert::convert_comm get ret failed %s", val.c_str() ) ;
		return ;
	}

	int len = 0;
	char *pbuf = _pEnv->GetMsgCache()->GetData( seqid.c_str(), len ) ;
	if ( pbuf == NULL ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "get cache length %d error" , len ) ;
		return;
	}

	unsigned short cmd = 0 ;
	memcpy( &cmd, pbuf, sizeof(short) ) ;
	cmd = ntohs( cmd ) ;

	switch( cmd )
	{
	case 0x0102:  // 点名指令
		break ;
	case 0x0301:  // 拍照指令
		break ;
	case 0x0401:  // 调度指令
		{
			buildheader( buf, szphone, sizeof(short) ) ;
			buf.writeInt16( 0x0481 ) ;
		}
		break ;
	default:
		OUT_ERROR( NULL, 0, macid.c_str(), "PConvert::convert_dctlm not support %s ", val.c_str() ) ;
		break;
	}
	_pEnv->GetMsgCache()->FreeData( pbuf ) ;
}

// 构建协议头部数据
void PConvert::buildheader( DataBuffer &buf, const char *phone, unsigned int len , unsigned int result, unsigned int seq )
{
	_Header header ;
	header.tag[0] = '&' ;
	header.tag[1] = '&' ;
	header.flag   = 0x0 ;
	header.mid    = 0x0 ;
	header.result = htonl( result ) ;
	header.seq 	  = htonl( ( seq == 0 ) ? get_next_seq() : seq ) ;
	header.len 	  = htonl( len ) ;

	// 将手机号转到终端号
	str2bcd( phone, (char*)header.termid, 8 ) ;

	buf.writeBlock( &header, sizeof(_Header) ) ;
}

// 转换点名处理
bool PConvert::build_caller( unsigned int seq, const char *phone, const char *data, int len )
{
	if ( len < (int)sizeof(_CallReq) ) {
		OUT_ERROR( NULL, 0, phone, "build caller data length %d error", len ) ;
		return false ;
	}

	string macid ;
	// 从缓存中取得终端号与手机号的对应关系
	if ( ! _cache.GetSession( phone, macid ) ) {
		OUT_ERROR( NULL, 0, phone, "get macid failed" ) ;
		return false ;
	}

	char szbuf[1024] = {0};
	// 将点名协议转换成字段回传
	sprintf( szbuf, "CAITS PCCFJ_%u %s 0 D_CALL {TYPE:2} \r\n", seq, macid.c_str() ) ;

	char szseq[128] = {0} ;
	sprintf( szseq, "PCCFJ_%u", seq ) ;
	_pEnv->GetMsgCache()->AddData( szseq, data, len ) ;

	// 发送给MSG来处理
	return _pEnv->GetMsgClient()->HandleUpMsgData( macid.c_str(), szbuf, strlen(szbuf) ) ;
}

// 转换拍照消息
bool PConvert::build_photo( unsigned int seq, const char *phone, const char *data, int len )
{
	if ( len < (int)sizeof(_PhotoReq) ) {
		OUT_ERROR( NULL, 0, phone, "build photo req data length %d error", len ) ;
		return false ;
	}

	string macid ;
	// 从缓存中取得终端号与手机号的对应关系
	if ( ! _cache.GetSession( phone, macid ) ) {
		OUT_ERROR( NULL, 0, phone, "get macid failed" ) ;
		return false ;
	}

	_PhotoReq *req = ( _PhotoReq *) (data) ;

	string sz ;
	// 多路拍照
	for ( int i = 0; i < 8; ++ i ) {
		if ( ! isset( req->cameraid, i ) ) {
			continue ;
		}
		// 摄像头通道ID|拍摄命令|录像时间|保存标志|分辨率|照片质量|亮度|对比度|饱和度|色度
		char szbuf[1024] = {0} ;
		sprintf( szbuf, "CAITS PCCFJ_%u %s 0 D_CTLM {TYPE:10,RETRY:0,VALUE:%u|%u|%u|0|%u|10|%u|%u|%u|%u} \r\n",
				seq, macid.c_str(), i+1, ntohs(req->num), ntohs(req->time) , req->quality,
				req->brightness, req->contrast, req->saturation, req->chroma ) ;

		char szseq[128] = {0} ;
		sprintf( szseq, "PCCFJ_%u_%u", seq , i ) ;
		_pEnv->GetMsgCache()->AddData( szseq, data, len ) ;

		sz += szbuf ;
	}
	// 下发拍照
	return _pEnv->GetMsgClient()->HandleUpMsgData( macid.c_str(), sz.c_str(), sz.length() ) ;
}

// 下发调度消息
bool PConvert::build_sendmsg( unsigned int seq, const char *phone, const char *data, int len )
{
	if ( len < (int)sizeof(_ScheduleReq) || len > 4096 ) {
		OUT_ERROR( NULL, 0, phone, "build send message data length %d error", len ) ;
		return false ;
	}

	string macid ;
	// 从缓存中取得终端号与手机号的对应关系
	if ( ! _cache.GetSession( phone, macid ) ) {
		OUT_ERROR( NULL, 0, phone, "get macid failed" ) ;
		return false ;
	}

	CBase64 base64 ;
	base64.Encode( (const char *)(data+ sizeof(_ScheduleReq)) , len - (int)sizeof(_ScheduleReq) ) ;

	char szbuf[10240] = {0} ;
	sprintf( szbuf, "CAITS PCCFJ_%u %s 0 D_SNDM {TYPE:1,1:255,2:%s} \r\n", seq, macid.c_str(), base64.GetBuffer() ) ;

	char szseq[128] = {0} ;
	sprintf( szseq, "PCCFJ_%u", seq ) ;
	_pEnv->GetMsgCache()->AddData( szseq, data, len ) ;

	return _pEnv->GetMsgClient()->HandleUpMsgData( macid.c_str(), szbuf, strlen(szbuf) ) ;
}

// 通过MACID取得对应的终端口
bool PConvert::gettermidbymacid( const string &macid, char *szphone )
{
	size_t pos = macid.find( "_" ) ;
	if ( pos == string::npos ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "mac id error" ) ;
		return false ;
	}
	string phone = macid.substr( pos+1 ) ;
	if ( phone.length() < 11 ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "phone length too short" ) ;
		return false ;
	}
	safe_phone( phone.c_str(), phone.length(), szphone, 16 ) ;

	return true ;
}

// 转换上报的GPS的数据
bool PConvert::convertgps( const char *szphone, MapString &mp, DataBuffer &buf , bool bcall )
{
	if ( mp.empty() )
		return false ;

	_UploadGps info ;
	info.cmd = ( bcall ) ? htons(0x0182) : htons( 0x0181 ) ;  // 上报GPS的位置数据

	// 组建GPS的位置数据包
	if ( ! buildgps( mp, info.gps ) ) {
		OUT_ERROR( NULL, 0, szphone, "build gps data error" ) ;
		return false ;
	}
	// 构建头部数据
	buildheader( buf, szphone , sizeof(_UploadGps) ) ;
	// 写入数据体数据
	buf.writeBlock( &info, sizeof(_UploadGps) ) ;

	return true ;
}

// 将本地时间转成格林威冶时间
static void gettime( const char *src, char *sztime )
{
	int nyear = 0 , nmonth = 0 , nday = 0 , nhour = 0 ,nmin = 0 , nsec = 0 ;
	sscanf( src, "%04d%02d%02d/%02d%02d%02d", &nyear, &nmonth, &nday, &nhour, &nmin, &nsec ) ;

	struct tm curtm ;
	curtm.tm_year = nyear - 1900 ;
	curtm.tm_mon  = nmonth - 1 ;
	curtm.tm_mday = nday ;
	curtm.tm_hour = nhour ;
	curtm.tm_min  = nmin ;
	curtm.tm_sec  = nsec ;

	time_t t = mktime( &curtm ) - 8 * 3600 ;  // 转成格林威冶时间
	struct tm local_tm;
	struct tm *tm = localtime_r( &t, &local_tm ) ;

	sprintf( sztime, "%02d%02d%02d%02d%02d%02d",
			( tm->tm_year + 1900 ) % 100, tm->tm_mon + 1, tm->tm_mday , tm->tm_hour, tm->tm_min, tm->tm_sec ) ;
}
// 构建GPS的数据
bool PConvert::buildgps( MapString &mp, _Gps &gps )
{
	string sval ;
	if ( ! get_map_string( mp, "4", sval ) ) {
		OUT_ERROR( NULL, 0, NULL , "get time error" ) ;
		return false ;
	}

	char sztime[18] = {0} ;
	gettime( sval.c_str(), sztime ) ;
	str2bcd( sztime, (char*)gps.time, 6 ) ;

	int nval = 0 ;
	if ( ! get_map_integer( mp, "1", nval ) ) {
		return false ;
	}
	gps.lon = degree2int( nval ) ;

	if ( ! get_map_integer( mp, "2", nval ) ) {
		return false ;
	}
	gps.lat = degree2int( nval ) ;

	if ( get_map_integer( mp, "3", nval ) ) { // 速度808中为1/10m/s
		nval = nval / 10 ;
	}
	int nspeed = nval * 36;

	char sz[2] = {0} ;
	sz[0] = (int) (nspeed / 100) ;
	sz[1] = (int) (nspeed % 100) ;
	memcpy( &gps.speed, sz, sizeof(short) ) ;

	gps.mile = 0x00 ;
	if ( get_map_integer( mp, "9", nval ) ) { // 里程
		gps.mile = htonl( nval / 10 ) ;
	}
	gps.distance = 0x00 ;

	get_map_integer( mp, "6", nval ) ;  // 高度
	gps.height = htons( nval ) ;

	get_map_integer( mp, "5", nval ) ;  // 方向
	gps.direction = nval / 2 ;  // 表示为2度

	int alam = 0 ;
	get_map_integer( mp, "20", alam ) ;
	get_map_integer( mp, "8", nval ) ;

	unsigned int state = 0 ;
	if ( isset( nval, 1  ) ) setbit( state, 0 ) ;
	if ( isset( nval, 4  ) ) setbit( state, 2 ) ;  // 运营状态,这里实际与协议相反
	if ( isset( alam, 28 ) ) setbit( state, 5 ) ;
	if ( isset( alam, 19 ) ) setbit( state, 6 ) ;
	if ( isset( alam, 18 ) ) setbit( state, 7 ) ;
	if ( isset( alam, 7  ) ) setbit( state, 8 ) ;
	if ( isset( alam, 8  ) ) setbit( state, 9 ) ;

	string s ;
	if ( get_map_string( mp, "32", s ) ) { 	// 进出区域报警
		if ( s.find( "|0" ) != string::npos )
			setbit( state, 10 ) ;
		else
			setbit( state, 11 ) ;
	}
	if ( isset( alam, 1  ) ) setbit( state, 12 ) ;
	if ( isset( alam, 23 ) ) setbit( state, 13 ) ;
	if ( isset( nval, 0  ) ) setbit( state, 14 ) ;
	if ( isset( alam, 26 ) ) setbit( state, 15 ) ;
	if ( isset( alam, 27 ) ) setbit( state, 16 ) ;
	if ( isset( nval, 13 ) ) setbit( state, 17 ) ;
	if ( isset( nval, 14 ) ) setbit( state, 18 ) ;
	if ( isset( nval, 15 ) ) setbit( state, 19 ) ;
	if ( isset( nval, 16 ) ) setbit( state, 20 ) ;
	if ( isset( alam, 6  ) ) setbit( state, 21 ) ;
	// gps.state = htonl(state) ;
	gps.state = state ;  // 不需要处理字符序的问题

	return true ;
}

// 发送取回的图片
void PConvert::sendpicture( unsigned int seq, const char *data, const int len )
{
	char szid[128] = {0};
	sprintf( szid, "%u", seq ) ;

	int nlen = 0 ;
	char *pbuf = _pEnv->GetMsgCache()->GetData( szid, nlen ) ;
	if ( pbuf == NULL ) {
		return ;
	}
	DataBuffer buf ;
	buf.writeBlock( pbuf, nlen ) ;
	_pEnv->GetMsgCache()->FreeData( pbuf ) ;

	buf.fillInt32( sizeof(_PhotoRsp) + len, sizeof(_Header) - sizeof(int) ) ;
	buf.fillInt16( len, sizeof(_Header) + sizeof(_PhotoRsp) - sizeof(short) ) ;
	buf.writeBlock( data, len ) ;

	// 将数据发送回拍照
	_pEnv->GetPasClient()->HandleData( buf.getBuffer(), buf.getLength() ) ;

	OUT_INFO( NULL, 0, "Photo", "send take photo ack seq %d len %d", seq, len ) ;
}
