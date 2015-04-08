#include "pconvert.h"
#include <comlog.h>
#include <Base64.h>
#include <BaseTools.h>
#include <databuffer.h>
#include <tools.h>
#include <fcntl.h>
#include <unistd.h>
#include <sys/stat.h>
#include <errno.h>

#define IMG_JPG   			0x01
#define IMG_GIF   			0x02
#define IMG_TIFF  			0x03

// 构建对应的序号关系
static const string buildkey( const char *vechile, unsigned char color, unsigned short msgid )
{
	char szbuf[128] = {0};
	sprintf( szbuf, "%d_%s_%d", color, vechile, msgid ) ;
	return szbuf ;
}

// 依次创建目录
static void reverse_mkdir( const char *root, const char *path )
{
	char buf[512] = {0} ;
	sprintf( buf, "%s" , path ) ;
	char *p = buf ;

	char szpath[1024] = {0} ;
	sprintf( szpath, "%s" , root ) ;

	char *q = strstr( p , "/" ) ;
	while ( q != NULL ) {
		*q = 0 ;
		strcat( szpath, "/" ) ;
		strcat( szpath, p   ) ;
		if ( access( szpath , 0 ) != F_OK ) {
			mkdir( szpath, 0777 ) ;
		}
		p = q + 1 ;
		q = strstr( p, "/" ) ;
	}
	if ( p != NULL ) {
		strcat( szpath, "/" ) ;
		strcat( szpath, p   ) ;
		if ( access( szpath , 0 ) != F_OK   ) {
			mkdir( szpath, 0777 ) ;
		}
	}
}
// 取得文件路径
static bool get_file_path( const char *szroot, string &path, const string &mac_id , char *sztime )
{
	char str_time[32] = {0};

	time_t t = time(0) ;

	struct tm *tm;
	tm = localtime(&t);
	sprintf(str_time, "%04d/%02d/%02d", tm->tm_year + 1900,tm->tm_mon + 1, tm->tm_mday);

	path = str_time;

	reverse_mkdir( szroot, str_time ) ;

	sprintf(sztime,"%02d%02d%02d",tm->tm_hour,tm->tm_min,tm->tm_sec ) ;

	return true ;
}
// 移动文件
static bool mv_file( const string &old_file_name, const string &new_file_name)
{
	if(rename(old_file_name.c_str(),new_file_name.c_str()) == 0)
		return true;

	OUT_ERROR(NULL,0,NULL,"rename %s to %s error:%s", old_file_name.c_str(),new_file_name.c_str(),strerror(errno));
	return false ;
}
// 取得文件名
static bool get_file_name( const char *szroot, string &file_name, const string &mac_id, const unsigned char type , bool bnew )
{
	string path ;

	char sztime[32] = {0} ;
	if ( ! get_file_path( szroot, path, mac_id, sztime ) )
		return false ;

	path += "/";
	path += mac_id ;

	if ( bnew ) {
		path += "_" ;
		path += sztime ;
	}

	switch( type )
	{
	case IMG_JPG:
		path += ".jpg";
		break;
	case IMG_GIF:
		path += ".gif";
		break;
	case IMG_TIFF:
		path += ".tiff";
		break;
	default:
		path += ".jpg";
		break;
	}
	file_name = path;

	return true;
}

// 保存照片文件数据
static bool save_photofile( string &new_file_name, const char *szpath, const string &mac_id, unsigned char type ,
		const char *photo, const int len , const int photo_len )
{
	//应答：应答标识（0：不支持拍照，1：完成拍照，2：完成拍照，照片数据稍后传送，3：未拍照（不在线），4：未拍照（无法使用指定镜头），5：未拍照（其它原因），9：车牌号码错误）
	// |拍照位置地点|镜头ID|图片长度|照片大小（1:320x240；2:640x480；
	//3:800x600；4:1024x768；5:176x144[Qcif]；
	//6:352*288[Cif]；7:704*288[HALF D1]；
	//8:704*576[D1]）|图像格式（1：jpg，2：gif，3：tiff，4：png）|图片url"

	string file_name ;
	if ( ! get_file_name( szpath, file_name, mac_id, type , false ) )
		return false ;

	char szname[512] = {0};
	sprintf( szname, "%s/%s", szpath, file_name.c_str() ) ;

	FILE *fp = fopen( szname, "a+" ) ;
	if(fp == NULL){
		OUT_ERROR( NULL, 0, NULL, "open file %s failed", szname ) ;
		return false ;
	}

	fwrite( photo, sizeof(char), len , fp ) ;
	fseek(fp,0,SEEK_END);

	if(ftell(fp) < photo_len ) { //说明最后一个包已经写入文件当中了
		fclose( fp ) ;
		OUT_ERROR(NULL,0,NULL," mac id %s , photo length %d len %d more than data length", mac_id.c_str() , photo_len , len );
		return false ;
	}
	//若是照片没有上传完全，返回false,不组包发给客户端。
	fclose(fp);

	// 新文件名称
	if( ! get_file_name( szpath, new_file_name, mac_id, type , true ) ){
		OUT_ERROR( NULL, 0, NULL, "get new file name %s failed", new_file_name.c_str() ) ;
		return false ;
	}

	char sznew[512] = {0};
	sprintf( sznew, "%s/%s", szpath, new_file_name.c_str() ) ;
	if( ! mv_file( szname, sznew ) ){
		OUT_ERROR( NULL, 0, NULL, "mv file %s to new file %s failed", szname , sznew ) ;
		return false ;
	}

	return true ;
}

PConvert::PConvert()
{
}

PConvert::~PConvert()
{

}

// 转换控制指令对象
bool PConvert::convert_ctrl( const string &seq, const string &macid, const string &line, const string &vechile,
		DataBuffer &dbuf , string &acode )
{
	if ( ! _macid2access.GetSession(macid, acode, false) ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "get macid access failed" ) ;
		return false ;
	}

	unsigned char carcolor = 0 ;
	string carnum ;
	if ( ! get_carinfobymacid( vechile , carcolor, carnum ) ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "parser vechile mac %s failed", vechile.c_str() ) ;
		return false ;
	}

	MapString kvmap ;
	if ( ! parse_jkpt_value( line, kvmap ) ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "split command error, line:%s" , line.c_str() ) ;
		return false ;
	}

	map<string, string>::iterator it ;

	it = kvmap.find("TYPE");
	if ( it == kvmap.end()){
		OUT_ERROR( NULL, 0, macid.c_str(), "command error, line:%s" , line.c_str() ) ;
		return false ;
	}
	string type = it->second ;

	it = kvmap.find("VALUE") ;
	if ( it == kvmap.end()){
		OUT_ERROR( NULL, 0, macid.c_str(), "command error, line:%s" , line.c_str() ) ;
		return false ;
	}
	string value = it->second ;

	string cache_key = "";

	//格式正确
	unsigned short ustype = atoi(type.c_str());
	switch (ustype)
	{
	case 9: //监听,value为电话号码;
	case 16://通话
		{
			DownCtrlMsgMonitorVehicleReq req ;
            req.header.msg_len     = ntouv32(sizeof(req));//xifengming
			req.header.access_code = ntouv32( atoi(acode.c_str()) ) ;
			req.header.msg_seq     = ntouv32( _seq_gen.get_next_seq() ) ;
			req.header.msg_type	   = ntouv16( DOWN_CTRL_MSG ) ;
			req.ctrl_msg_header.data_type = ntouv16( DOWN_CTRL_MSG_MONITOR_VEHICLE_REQ ) ;
			req.ctrl_msg_header.vehicle_color = carcolor ;
			safe_memncpy( req.ctrl_msg_header.vehicle_no, carnum.c_str(),  sizeof(req.ctrl_msg_header.vehicle_no) ) ;
			safe_memncpy( req.monitor_tel, value.c_str(), sizeof(req.monitor_tel) ) ;
			req.ctrl_msg_header.data_length = ntouv32( sizeof(req.monitor_tel) ) ;

			dbuf.writeBlock( &req, sizeof(req) ) ;

			// 构建查找的KEY值
			string skey = buildkey( carnum.c_str(), carcolor, DOWN_CTRL_MSG_MONITOR_VEHICLE_REQ ) ;
			// 添加序号的对应关系
			_reqmap.AddReqMap( skey, seq ) ;

			return true ;
		}
		break;
	case 10://_TakePhoto
		{
			DownCtrlMsgTakePhotoReq req ;

            req.header.msg_len     = ntouv32(sizeof(req));//xifengming
			req.header.access_code = ntouv32( atoi(acode.c_str()) ) ;
			req.header.msg_seq     = ntouv32( _seq_gen.get_next_seq() ) ;
			req.header.msg_type	   = ntouv16( DOWN_CTRL_MSG ) ;
			req.ctrl_msg_header.data_type = ntouv16( DOWN_CTRL_MSG_TAKE_PHOTO_REQ  ) ;
			req.ctrl_msg_header.vehicle_color = carcolor ;
			safe_memncpy( req.ctrl_msg_header.vehicle_no, carnum.c_str(),  sizeof(req.ctrl_msg_header.vehicle_no) ) ;
			req.ctrl_msg_header.data_length = ntouv32( sizeof(char)*2 ) ;

			vector<string> vec ;
			splitvector(value, vec, "|", 0 );
			req.lens_id =  (vec.size()> 0 &&vec[0].length() > 0 ) ? atoi(vec[0].c_str()) : 1  ;
			req.size    =  (vec.size()> 5 &&vec[5].length() > 0 ) ? atoi(vec[5].c_str()) : 5  ;

			dbuf.writeBlock( &req, sizeof(req) ) ;

			// 构建查找的KEY值
			string skey = buildkey( carnum.c_str(), carcolor, DOWN_CTRL_MSG_TAKE_PHOTO_REQ ) ;
			// 添加序号的对应关系
			_reqmap.AddReqMap( skey, seq ) ;

			return true ;
		}
		break;
	default:
		break ;
	}
	return false ;
}

// 下发文本消息数据
bool PConvert::convert_sndm( const string &seq, const string &macid, const string &line, const string &vechile,
		DataBuffer &dbuf, string &acode )
{
	if ( ! _macid2access.GetSession(macid, acode, false) ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "get macid access failed" ) ;
		return false ;
	}

	unsigned char carcolor = 0 ;
	string carnum ;
	if ( ! get_carinfobymacid( vechile , carcolor, carnum ) ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "parser vechile mac %s failed", vechile.c_str() ) ;
		return false ;
	}

	MapString kvmap ;
	if ( ! parse_jkpt_value( line, kvmap ) ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "split command error, line:%s" , line.c_str() ) ;
		return false ;
	}

	map<string, string>::iterator it = kvmap.find("TYPE");
	if ( it == kvmap.end())
	{
		OUT_ERROR( NULL, 0, macid.c_str(), "command error, line:%s" , line.c_str() ) ;
		return false;
	}
	string type = it->second ;

	if ( type == "1" ) {  // 文本下发

		it = kvmap.find( "2" ) ;//  BASE64文本内容
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0, macid.c_str(), "send text empty, line:%s" ,line.c_str() ) ;
			return false ;
		}

		CBase64 base64;
		if ( ! base64.Decode( it->second.c_str(), it->second.length()) ) {
			OUT_ERROR( NULL, 0, macid.c_str(), "base64 decode failed, %s", line.c_str() ) ;
			return false ;
		}
		if ( base64.GetLength() > 1024 ) {
			OUT_ERROR( NULL, 0, macid.c_str(), "send text message length more than 1024" ) ;
			return false ;
		}

		DownCtrlMsgTextInfo req ;
        req.header.msg_len     = ntouv32(sizeof(req)+base64.GetLength()+sizeof(Footer));
		req.header.access_code = ntouv32( atoi(acode.c_str()) ) ;
		req.header.msg_seq     = ntouv32( _seq_gen.get_next_seq() ) ;
		req.header.msg_type    = ntouv16( DOWN_CTRL_MSG ) ;
		req.ctrl_msg_header.vehicle_color = carcolor ;
		safe_memncpy( req.ctrl_msg_header.vehicle_no, carnum.c_str(),  sizeof(req.ctrl_msg_header.vehicle_no) ) ;
		req.ctrl_msg_header.data_length = ntouv32( sizeof(char) + 2 * sizeof(int) + base64.GetLength() ) ;
		req.msg_priority       = 0x01 ;
		req.msg_sequence       = req.header.msg_seq ;
		req.msg_length  	   = ntouv32( base64.GetLength() ) ;
		req.ctrl_msg_header.data_type = ntouv16( DOWN_CTRL_MSG_TEXT_INFO ) ;

		dbuf.writeBlock( &req, sizeof(req) ) ;
		dbuf.writeBlock( base64.GetBuffer(), base64.GetLength() ) ;

		Footer  end ;
		dbuf.writeBlock( &end, sizeof(end) ) ;

		// 构建查找的KEY值
		string skey = buildkey( carnum.c_str(), carcolor, DOWN_CTRL_MSG_TEXT_INFO ) ;
		// 添加序号的对应关系
		_reqmap.AddReqMap( skey, seq ) ;

		return true ;
	}
	return false ;
}

// 构建内部协议
bool PConvert::BuildMonitorVehicleResp( const string &macid, UpCtrlMsgMonitorVehicleAck *moni , string &data )
{
	string val ;
	string key = buildkey( moni->ctrl_msg_header.vehicle_no , moni->ctrl_msg_header.vehicle_color , DOWN_CTRL_MSG_MONITOR_VEHICLE_REQ ) ;
	if ( ! _reqmap.FindReqMap( key, val, true ) ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "get sequeue id DOWN_CTRL_MSG_MONITOR_VEHICLE_REQ failed" ) ;
		return false;
	}
	data = "CAITR "+val+" "+ macid + " 5 D_CTLM {RET:"+ chartodecstr(moni->result) +"} \r\n" ;

	return true;
}

// 处理照片上传
bool PConvert::BuildUpCtrlMsgTakePhotoAck( const string &macid, const string &path, const char *data, int len , string &sdata )
{
	UpCtrlMsgTakePhotoAck *header = (UpCtrlMsgTakePhotoAck *)data ;

	string val ;
	string key = buildkey( header->ctrl_msg_header.vehicle_no, header->ctrl_msg_header.vehicle_color, DOWN_CTRL_MSG_TAKE_PHOTO_REQ ) ;
	if ( _reqmap.FindReqMap( key, val, true ) ) {
		sdata = "CAITR "+val+" "+ macid + " 5 D_CTLM {RET:0} \r\n" ;
	}
	// 如果拍照不成功返回RET:1
	if ( header->ctrl_photo_body.rsp_flag != 0x01 && header->ctrl_photo_body.rsp_flag != 0x02 ) {
		sdata = "CAITR "+val+" "+ macid + " 5 D_CTLM {RET:1} \r\n" ;
		return true ;
	}

	string new_file_name ;

	int photo_len = ntouv32( header->ctrl_photo_body.photo_len ) ;
	int save_len  = len - sizeof(UpCtrlMsgTakePhotoAck) - sizeof(Footer) ;
	char *ptr = (char *) ( data + sizeof(UpCtrlMsgTakePhotoAck) ) ;

	// 如果相片的长度大于数据长度
	if ( ! save_photofile( new_file_name, path.c_str() , macid, header->ctrl_photo_body.size_type , ptr , save_len , photo_len ) ){
		OUT_INFO( NULL, 0, macid.c_str(), "save pic file photo length %d, save len %d" , photo_len , save_len ) ;
		return false ;
	}

	string gps ;
	build_gps_info( gps, &header->ctrl_photo_body.gps ) ;

	char buf[2048] = {0} ;
	sprintf( buf, "CAITS 0_0 %s 5 U_REPT {TYPE:3,120:%d,124:%d,121:%d,122:%d,123:%d,125:%s,%s} \r\n",
				macid.c_str(), header->ctrl_photo_body.lens_id, header->ctrl_photo_body.lens_id, header->ctrl_photo_body.size_type,
				header->ctrl_photo_body.type, 0 , new_file_name.c_str() , gps.c_str() ) ;

	sdata += buf ;

	return true ;
}

// 转换下发文本应答
bool PConvert::BuildUpCtrlMsgTextInfoAck( const string &macid, UpCtrlMsgTextInfoAck *text, string &sdata )
{
	string val ;
	string key = buildkey( text->ctrl_msg_header.vehicle_no, text->ctrl_msg_header.vehicle_color, DOWN_CTRL_MSG_TEXT_INFO ) ;
	if ( ! _reqmap.FindReqMap( key, val, true ) ) {
		OUT_ERROR( NULL, 0, macid.c_str(), "get sequeue id DOWN_CTRL_MSG_TEXT_INFO failed" ) ;
		return false;
	}
	sdata = "CAITR "+val+" "+ macid + " 5 D_CTLM {RET:"+ chartodecstr(text->result) +"} \r\n" ;

	return true;
}

// 构建位置数据
bool PConvert::BuildUpRealLocation( const string &macid, UpExgMsgRealLocation *upmsg, string &sdata )
{
	string gps ;
	build_gps_info( gps, &upmsg->gnss_data ) ;
	sdata = "CAITS 0_0 " + macid + " 5 U_REPT {TYPE:0,RET:0," + gps + "} \r\n" ;
	return true ;
}

// 处理历史数据上传
bool PConvert::BuildUpHistoryLocation( const string &macid, const char *data, int len , int num, string &sdata )
{
	sdata = "" ;
	// 转换GPS的数据
	const char *ptr = ( const char *) ( data + sizeof(UpExgMsgHistoryHeader) + sizeof(char) ) ;
	for ( int i = 0 ; i < num; ++ i ) {

		GnssData  *pgps = ( GnssData *)( ptr + sizeof(GnssData)*i ) ;

		string gps ;
		// Gps转成Gnss
		build_gps_info( gps, pgps ) ;

		sdata += "CAITS 0_0 " + macid + " 5 U_REPT {" + gps + "} \r\n" ;
	}
	return true ;
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

bool PConvert::get_phoneome( const string &macid, string &phone, string &ome )
{
	if ( macid.length() < PHONE_LEN ) {
		return false ;
	}

	size_t pos = macid.find( '_' ) ;
	if ( pos == string::npos ) {
		return false ;
	}

	phone = macid.substr( pos+1 ) ;
	ome   = macid.substr( 0, pos ) ;

	return true ;
}

bool PConvert::get_carinfobymacid( const string &macid, unsigned char &carcolor, string &carnum )
{
	if ( macid.length() < 3 ) {
		return false ;
	}

	size_t pos = macid.find( '_' ) ;
	if ( pos == string::npos ) {
		return false ;
	}
	carcolor = atoi( macid.c_str() ) ;
	carnum   = macid.substr( pos+1 ) ;

	return true ;
}

// 将GPS数据转成GNSS
bool PConvert::convert_gps_info( MapString &mp, GnssData &gps )
{
	if ( mp.empty() )
		return false ;

	gps.state = 0x00 ;
	gps.alarm = 0x00 ;

	int nval = 0 ;
	if ( get_map_integer( mp, "1", nval ) ) {
		gps.lon = ntouv32( nval * 10 / 6 ) ;
	}

	if ( get_map_integer( mp, "2", nval ) ) {
		gps.lat = ntouv32( nval *10 / 6 ) ;
	}

	if ( get_map_integer( mp, "3", nval ) ) {
		gps.vec1 = ntouv16( nval/10 ) ; // 速度808中为1/10m/s
	}

	string sval ;
	if ( get_map_string( mp, "4", sval ) ) {

		int nyear = 0 , nmonth = 0 , nday = 0 , nhour = 0 ,nmin = 0 , nsec = 0 ;

		sscanf( sval.c_str(), "%04d%02d%02d/%02d%02d%02d", &nyear, &nmonth, &nday, &nhour, &nmin, &nsec ) ;

		gps.date[3]   = nyear  % 256 ;
		gps.date[2]   = nyear / 256 ;
		gps.date[1]   = nmonth ;
		gps.date[0]   = nday ;

		gps.time[0]   = nhour ;
		gps.time[1]   = nmin ;
		gps.time[2]   = nsec ;
	}

	if ( get_map_integer( mp, "5", nval ) ){
		gps.direction = ntouv16( nval ) ;
	}

	if ( get_map_integer( mp, "15", nval ) ) {
		gps.state = (nval) ? gps.state | 0x02 : gps.state ;
	}

	// 报警
	if ( get_map_integer( mp, "20" , nval ) ) {
		gps.alarm = nval ;
	}
	// 行驶记录仪的速度
	if ( get_map_integer( mp, "7" , nval ) ) {
		gps.vec2  = ntouv32( nval ) ;
	}
	// 当前里程数
	if ( get_map_integer( mp, "9", nval ) ) {
		gps.vec3 = ntouv32( nval ) ;
	}

	// 状态
	if ( get_map_integer( mp, "8" , nval ) ) {
		gps.state = nval ;
	}

	gps.state = ntouv32( gps.state ) ;
	gps.alarm = ntouv32( gps.alarm ) ;

	return true ;
}

// 转换成内部协议处理
void PConvert::build_gps_info( string &dest, GnssData *gps_data )
{
	dest.clear();

	// {TYPE:0,RET:0,1:64505177,2:18258174,3:8310,4:20120816/110527,5:62,7:0,8:1662189591,9:0,20:0}
	dest += "1:" + uitodecstr(ntouv32(gps_data->lon) * 6 / 10) + ",";
	dest += "2:" + uitodecstr(ntouv32(gps_data->lat) * 6 / 10) + ",";
	dest += "3:" + uitodecstr(ntouv16(gps_data->vec1)*10) + ",";

	unsigned int iyear = ((unsigned char) (gps_data->date[2])) * 256 + (unsigned char) (gps_data->date[3]);
	string year   = uitodecstr(iyear);
	string month  = charto2decstr(gps_data->date[1]);
	string day    = charto2decstr(gps_data->date[0]);

	string hour   = charto2decstr(gps_data->time[0]);
	string minute = charto2decstr(gps_data->time[1]);
	string second = charto2decstr(gps_data->time[2]);

	dest += "4:" + year + month + day + "/" + hour + minute + second + ",";

	dest += "5:"  + ustodecstr(ntouv16(gps_data->direction)) + ",";
	dest += "6:"  + ustodecstr(ntouv16(gps_data->altitude)) + "," ;
	dest += "7:"  + ustodecstr(ntouv16(gps_data->vec2)*10)  + ",";
    dest += "8:"  + uitodecstr(ntouv32(gps_data->state)) + ",";
	dest += "9:"  + uitodecstr(ntouv32(gps_data->vec3)) + "," ;
	dest += "20:" + uitodecstr(ntouv32(gps_data->alarm)) ;
}
