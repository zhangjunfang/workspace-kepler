/*
 * servicecaller.cpp
 *
 *  Created on: 2011-12-12
 *      Author: humingqing
 */


#include "servicecaller.h"
#include <xmlparser.h>
#include <comlog.h>
#include <ProtoHeader.h>
#include <tools.h>
#include "pccutil.h"
#include <string.h>

#ifndef _WIN32
#include <strings.h>
#define stricmp strcasecmp
#endif

CServiceCaller::CServiceCaller():
	_datacache(true), _istester(0), _livetime(0)
{
	_srv_table[UP_EXG_MSG_REGISTER]							= &CServiceCaller::Proc_UP_EXG_MSG_REGISTER ;
	_srv_table[UP_EXG_MSG_REAL_LOCATION] 					= &CServiceCaller::Proc_UP_EXG_MSG_REAL_LOCATION ;
	_srv_table[UP_EXG_MSG_REPORT_DRIVER_INFO]				= &CServiceCaller::Proc_UP_EXG_MSG_REPORT_DRIVER_INFO ;
	_srv_table[UP_EXG_MSG_REPORT_EWAYBILL_INFO]		    	= &CServiceCaller::Proc_UP_EXG_MSG_REPORT_EWAYBILL_INFO ;
	_srv_table[UP_CTRL_MSG_MONITOR_VEHICLE_ACK]				= &CServiceCaller::Proc_UP_CTRL_MSG_MONITOR_VEHICLE_ACK ;
	_srv_table[UP_CTRL_MSG_TEXT_INFO_ACK]					= &CServiceCaller::Proc_UP_CTRL_MSG_TEXT_INFO_ACK ;
	_srv_table[UP_CTRL_MSG_TAKE_TRAVEL_ACK]					= &CServiceCaller::Proc_UP_CTRL_MSG_TAKE_TRAVEL_ACK ;
	_srv_table[UP_CTRL_MSG_EMERGENCY_MONITORING_ACK]    	= &CServiceCaller::Proc_UP_CTRL_MSG_EMERGENCY_MONITORING_ACK ;
	_srv_table[DOWN_CTRL_MSG_MONITOR_VEHICLE_REQ]			= &CServiceCaller::Proc_DOWN_CTRL_MSG ;
	_srv_table[DOWN_CTRL_MSG_TAKE_PHOTO_REQ]				= &CServiceCaller::Proc_DOWN_CTRL_MSG_TAKE_PHOTO_REQ ;  // 拍照特殊处理
	_srv_table[DOWN_CTRL_MSG_TEXT_INFO]						= &CServiceCaller::Proc_DOWN_CTRL_MSG ;
	_srv_table[DOWN_CTRL_MSG_TAKE_TRAVEL_REQ]				= &CServiceCaller::Proc_DOWN_CTRL_MSG ;
	_srv_table[DOWN_CTRL_MSG_EMERGENCY_MONITORING_REQ]  	= &CServiceCaller::Proc_DOWN_CTRL_MSG ;
	_srv_table[DOWN_PLATFORM_MSG_POST_QUERY_REQ]			= &CServiceCaller::Proc_DOWN_PLATFORM_MSG ;
	_srv_table[DOWN_PLATFORM_MSG_INFO_REQ]					= &CServiceCaller::Proc_DOWN_PLATFORM_MSG_INFO_REQ ;
	_srv_table[DOWN_WARN_MSG_URGE_TODO_REQ]					= &CServiceCaller::Proc_DOWN_WARN_MSG_URGE_TODO_REQ ;  // 报警督办自动应答
	_srv_table[DOWN_WARN_MSG_INFORM_TIPS]					= &CServiceCaller::Proc_DOWN_WARN_MSG ;
	_srv_table[DOWN_WARN_MSG_EXG_INFORM]					= &CServiceCaller::Proc_DOWN_WARN_MSG ;
	_srv_table[DOWN_EXG_MSG_CAR_LOCATION]					= &CServiceCaller::Proc_DOWN_EXG_MSG_CAR_LOCATION ;
	_srv_table[DOWN_EXG_MSG_HISTORY_ARCOSSAREA]				= &CServiceCaller::Proc_DOWN_EXG_MSG_HISTORY_ARCOSSAREA ;
	_srv_table[DOWN_EXG_MSG_CAR_INFO]						= &CServiceCaller::Proc_DOWN_EXG_MSG_CAR_INFO ;
	_srv_table[DOWN_EXG_MSG_RETURN_STARTUP]					= &CServiceCaller::Proc_DOWN_EXG_MSG_RETURN_STARTUP ;
	_srv_table[DOWN_EXG_MSG_RETURN_END]						= &CServiceCaller::Proc_DOWN_EXG_MSG_RETURN_END ;
	_srv_table[DOWN_EXG_MSG_APPLY_FOR_MONITOR_STARTUP_ACK]  = &CServiceCaller::Proc_DOWN_EXG_MSG_ACK ;
	_srv_table[DOWN_EXG_MSG_APPLY_FOR_MONITOR_END_ACK]  	= &CServiceCaller::Proc_DOWN_EXG_MSG_ACK ;
	_srv_table[DOWN_EXG_MSG_APPLY_HISGNSSDATA_ACK]			= &CServiceCaller::Proc_DOWN_EXG_MSG_ACK ;
	_srv_table[DOWN_EXG_MSG_REPORT_DRIVER_INFO]				= &CServiceCaller::Proc_DOWN_EXG_MSG_REPORT_DRIVER_INFO ;
	_srv_table[DOWN_EXG_MSG_TAKE_WAYBILL_REQ]				= &CServiceCaller::Proc_DOWN_EXG_MSG_TAKE_WAYBILL_REQ ;
	_srv_table[DOWN_BASE_MSG_VEHICLE_ADDED]					= &CServiceCaller::Proc_DOWN_BASE_MSG_VEHICLE_ADDED ;
}

CServiceCaller::~CServiceCaller()
{
	Stop() ;
}

// 初始化
bool CServiceCaller::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	char buf[1024] = {0} ;
	if ( ! pEnv->GetString( "http_call_url" , buf ) ) {
		OUT_ERROR( NULL, 0, "auth" , "get hessian url failed" ) ;
		return false ;
	}
	// 取得HTTP错误禁用时间
	int nvalue = 0 ;
	if ( pEnv->GetInteger( "http_error_time", nvalue ) ) {
		_macref.SetTimeOut( nvalue ) ;
	}
	// 取得HTTP的缓存存活时间
	if ( pEnv->GetInteger( "http_cache_time", nvalue ) ) {
		_livetime = nvalue ;
	}
	// 是否为测试状态
	if ( _pEnv->GetInteger( "pcc_is_tester", nvalue ) ) {
		_istester = (unsigned int)nvalue ;
	}

	int send_thread = 10 ;
	// 发送线程
	if ( pEnv->GetInteger( "http_send_thread" , nvalue ) ) {
		send_thread = nvalue ;
	}

	int recv_thread = 10 ;
	// 接收线程
	if ( pEnv->GetInteger( "http_recv_thread" , nvalue ) ) {
		recv_thread = nvalue ;
	}

	int queue_size = 1000 ;
	// HTTP请求最大的队列长度
	if ( pEnv->GetInteger( "http_queue_size" , nvalue ) ) {
		queue_size = nvalue ;
	}
	// 记录调用hession的URL
	_callUrl.SetString(buf);

	_httpcaller.SetReponse( this ) ;

	return _httpcaller.Init( send_thread, recv_thread, queue_size ) ;
}

// 启动
bool CServiceCaller::Start( void )
{
	return _httpcaller.Start() ;
}

// 停止
void CServiceCaller::Stop( void )
{
	return _httpcaller.Stop() ;
}

// 清理缓存
void CServiceCaller::RemoveCache( const char *key )
{
	if ( key == NULL ) {
		// 如果为NULL则清理所有数缓存
		_datacache.RecycleAll() ;
		return ;
	}
	// 清除某一个值的缓存处理
	_datacache.RemoveSession( key ) ;

	OUT_PRINT( NULL, 0, key, "Remove Cache data" ) ;
}

// 检测是超时的缓存数据
void CServiceCaller::CheckTimeOut( void )
{
	if ( _livetime <= 0 )
		return ;

	// 检测数据超时
	_datacache.CheckTimeOut( _livetime ) ;

	OUT_PRINT( NULL, 0, "SrvCaller", "check data cache time out %d", _livetime ) ;
}

// 取得注册信息，这里只处理鉴权时数据
bool CServiceCaller::getRegVehicleInfo( unsigned int msgid, unsigned int seq, const char *phoneNum, const char *terminaltype )
{
	_seq2key2.AddSeqKey( seq, phoneNum ) ;

	CKeyValue item ;
	item.SetVal( "phoneNum"    , phoneNum ) ;
	item.SetVal( "terminaltype", terminaltype ) ;

	return ProcessMsg( msgid, seq, "vehicleInforService", "getRegVehicleInfo", item ) ;
}

// 通用查询，通过车牌号和车牌颜色取得手机号与ome码的对应关系
bool CServiceCaller::getTernimalByVehicleByType( unsigned int msgid, unsigned int seq, const char *vehicleno, const char *vehicleColor )
{
	char key[256]= {0};
	sprintf( key, "%s_%s", vehicleno, vehicleColor ) ;

	_seq2key2.AddSeqKey( seq, key ) ;

	string val ;
	if ( _datacache.GetSession(key,val,false) ) {
		// 添加映射表中
		_seq2msgid.AddSeqMsg( seq, msgid ) ;
		// 如果存在缓存中直接处理
		ProcessResp( seq, val.c_str(), val.length(), HTTP_CALL_SUCCESS ) ;

		return true ;
	}

	// 上一次处理记录调用错误，这处理一旦调用出错就对于此车在一段时间停对HTTP的调用
	if ( ! _macref.Add( key, seq ) ) {
		OUT_ERROR( NULL, 0, "Caller", "macid %s call before error, seq %d", key, seq ) ;
		ProcessError( seq, false ) ;
		return false ;
	}

	CKeyValue item ;
	item.SetVal( "vehicleno"   , vehicleno ) ;
	item.SetVal( "vehicleColor", vehicleColor ) ;

	_seq2key.AddSeqKey( seq, key ) ;

	return ProcessMsg( msgid, seq, "vehicleInforService", "getTernimalByVehicleByType" , item ) ;
}

bool CServiceCaller::getTernimalByVehicleByTypeEx( unsigned int msgid, unsigned int seq, const char *vehicleno, const char *vehicleColor , const char *text )
{
	if ( text == NULL )
		return false ;

	char key[256]= {0};
	_pEnv->GetCacheKey( seq, key ) ;

	// 保存当前部分内部协议
	_innerdata.AddSession( key, text ) ;

	// 处理发送数据
	if ( ! getTernimalByVehicleByType( msgid, seq, vehicleno, vehicleColor ) ) {
		ProcessError( seq, false ) ;
		return false ;
	}

	return true ;
}

// 通用查询，通过手机号和oem码取得对应车牌以及省域关系
bool CServiceCaller::getBaseVehicleInfo( unsigned int msgid, unsigned int seq, const char *phone  , const char *ome )
{
	char key[256]= {0};
	sprintf( key, "%s_%s", ome, phone ) ;

	_seq2key2.AddSeqKey( seq, key ) ;

	string val ;
	if ( _datacache.GetSession(key,val,false) ) {
		// 添加映射表中
		_seq2msgid.AddSeqMsg( seq, msgid ) ;
		// 如果存在缓存中直接处理
		ProcessResp( seq, val.c_str(), val.length(), HTTP_CALL_SUCCESS ) ;

		return true ;
	}

	// 上一次处理记录调用错误，这处理一旦调用出错就对于此车在一段时间停对HTTP的调用
	if ( ! _macref.Add( key, seq ) ) {
		OUT_ERROR( NULL, 0, "Caller", "macid %s call before error, seq %d", key, seq ) ;
		ProcessError( seq, false ) ;
		return false ;
	}

	CKeyValue item ;
	item.SetVal( "phoneNum"    , phone ) ;
	item.SetVal( "terminaltype", ome ) ;

	_seq2key.AddSeqKey( seq, key ) ;

	return ProcessMsg( msgid, seq, "vehicleInforService", "getBaseVehicleInfo" , item ) ;
}

// 获取得驾驶员识别信息
bool CServiceCaller::getDriverOfVehicleByType( unsigned int msgid, unsigned int seq, const char *vehicleno , const char *vehicleColor )
{
	CKeyValue item ;
	item.SetVal( "vehicleno"   , vehicleno ) ;
	item.SetVal( "vehicleColor", vehicleColor ) ;

	return ProcessMsg( msgid, seq, "vehicleInforService", "getDriverOfVehicleByType", item ) ;
}

// 获取得电子运单数据
bool CServiceCaller::getEticketByVehicle( unsigned int msgid, unsigned int seq, const char *vehicleno , const char *vehicleColor )
{
	CKeyValue item ;
	item.SetVal( "vehicleno"   , vehicleno ) ;
	item.SetVal( "vehicleColor", vehicleColor ) ;

	return ProcessMsg( msgid, seq, "vehicleInforService", "getEticketByVehicle", item ) ;
}

// 处理车的数据
bool CServiceCaller::getDetailOfVehicleInfo( unsigned int msgid, unsigned int seq, const char *vehicleno, const char *vehicleColor )
{
	CKeyValue item ;
	item.SetVal( "vehicleno"   , vehicleno ) ;
	item.SetVal( "vehicleColor", vehicleColor ) ;

	return ProcessMsg( msgid, seq, "vehicleInforService", "getDetailOfVehicleInfo", item ) ;
}

// 处理平台查岗的消息
bool CServiceCaller::addForMsgPost( unsigned int msgid, unsigned int seq, const char *messageContent , const char * messageId ,
		const char *objectId, const char *objectType , const char *areaId )
{
	CKeyValue item ;

	item.SetVal( "messageContent" , messageContent ) ;
	item.SetVal( "messageId" 	  , messageId ) ;
	item.SetVal( "objectId"		  , objectId ) ;
	item.SetVal( "objectType"	  , objectType ) ;
	item.SetVal( "areaId"		  , areaId ) ;

	return ProcessMsg( msgid, seq, "thPlatInfosRmi", "addForMsgPost" , item ) ;
}

// 处理下发平台的报文
bool CServiceCaller::addForMsgInfo(unsigned int msgid, unsigned int seq, const char *messageContent , const char * messageId ,
			const char *objectId, const char *objectType , const char *areaId )
{
	CKeyValue item ;

	item.SetVal( "messageContent" , messageContent ) ;
	item.SetVal( "messageId" 	  , messageId ) ;
	item.SetVal( "objectId"		  , objectId ) ;
	item.SetVal( "objectType"	  , objectType ) ;
	item.SetVal( "areaId"		  , areaId ) ;

	return ProcessMsg( msgid, seq, "thPlatInfosRmi", "addForMsgInfo" , item ) ;
}

// 处理报警督办
bool CServiceCaller::addMsgUrgeTodo( unsigned int msgid, unsigned int seq, const char *supervisionEndUtc , const char *supervisionId ,
			const char * supervisionLevel , const char * supervisor , const char *supervisorEmail , const char *supervisorTel ,
			const char * vehicleColor, const char *vehicleNo , const char *wanSrc , const char *wanType , const char *warUtc )
{
	CKeyValue item ;

	item.SetVal( "supervisionEndUtc" , supervisionEndUtc ) ;
	item.SetVal( "supervisionId"	 , supervisionId ) ;
	item.SetVal( "supervisionLevel"  , supervisionLevel ) ;
	item.SetVal( "supervisor"		 , supervisor ) ;
	item.SetVal( "supervisorEmail"	 , supervisorEmail ) ;
	item.SetVal( "supervisorTel" 	 , supervisorTel ) ;
	item.SetVal( "vehicleColor"      , vehicleColor ) ;
	item.SetVal( "vehicleNo"         , vehicleNo ) ;
	item.SetVal( "wanSrc" 			 , wanSrc ) ;
	item.SetVal( "wanType" 			 , wanType ) ;
	item.SetVal( "warUtc" 			 , warUtc ) ;

	return ProcessMsg( msgid, seq, "thAlarmTodoRmi", "add" , item ) ;
}

// 处理报警预警
bool CServiceCaller::addMsgInformTips( unsigned int msgid, unsigned int seq, const char *alarmDescr, const char *alarmFrom,
			const char *alarmTime, const char *alarmType , const char *vehicleColor , const char *vehicleNo )
{
	CKeyValue item ;

	item.SetVal( "alarmDescr"	, alarmDescr ) ;
	item.SetVal( "alarmFrom" 	, alarmFrom  ) ;
	item.SetVal( "alarmTime" 	, alarmTime  ) ;
	item.SetVal( "alarmType" 	, alarmType  ) ;
	item.SetVal( "vehicleColor" , vehicleColor ) ;
	item.SetVal( "vehicleNo"	, vehicleNo) ;

	return ProcessMsg( msgid, seq, "thVehicleEarlywarningRmi", "add" , item ) ;
}

// 更新连接的状态
bool CServiceCaller::updateConnectState( unsigned int msgid, unsigned int seq, int areaId , int linkType , int status )
{
	CKeyValue item ;

	char szareaId[128] = {0} ;
	sprintf( szareaId, "%d", areaId ) ;

	char szutc[128] = {0};
	sprintf( szutc, "%llu", (long long)time(NULL) ) ;

	char szType[12] = {0};
	sprintf( szType, "%d", linkType ) ;

	char szStatus[12] = {0};
	sprintf( szStatus, "%d", status ) ;

	// areaId="" utc="" linkType="" status="0:连接，-1断开"/
	item.SetVal( "areaId"	, szareaId  ) ;
	item.SetVal( "utc" 		, szutc     ) ;
	item.SetVal( "linkType" , szType    ) ;
	item.SetVal( "status" 	, szStatus  ) ;

	return ProcessMsg( msgid, seq, "thLinkStatusServiceRmi", "add" , item ) ;
}

// ToDo: 添加各个服务的处理方法，这里设计为一个服务对应一个方法 , USERID_UTC_%d
bool CServiceCaller::ProcessMsg( unsigned int msgid, unsigned int seq , const char *service, const char *method , CKeyValue &item )
{
	if ( msgid == 0 )return false ;

	char key[256] = {0};
	_pEnv->GetCacheKey( seq, key ) ;
	// 添加映射表中
	_seq2msgid.AddSeqMsg( seq, msgid ) ;

	CQString sXml ;
	// 创建XML请求
	if ( ! CreateRequestXml( sXml, key, service, method, item ) ) {
		OUT_ERROR( NULL, 0, "ProcMsg" , "create request xml failed, msg id %x, seq id %d" , msgid, seq ) ;
		ProcessError( seq, true ) ;
		return false ;
	}
	OUT_INFO( NULL, 0, "Caller", "Request msg id %x,seqid %d,service %s,method %s" , msgid, seq, service, method ) ;
	// 调用XML处理
	if ( ! _httpcaller.Request( seq, (const char *)_callUrl, sXml.GetBuffer(), sXml.GetLength() ) ) {
		OUT_ERROR( NULL, 0, "ProcMsg" , "caller http request failed , msg id %x, seq id %d" , msgid, seq ) ;
		ProcessError( seq, true ) ;
		return false ;
	}

	return true ;
}

// 本地编码转成UTF-8
static bool locale2utf8( const char *szdata, const int nlen , CQString &out )
{
	int   len = nlen*4 + 1 ;
	char *buf = new char[ len ] ;
	memset( buf, 0 , len ) ;

	if( g2u( (char *)szdata , nlen , buf, len ) == -1 ){
		OUT_ERROR( NULL, 0, "auth" , "locale2utf8 query %s failed" , szdata ) ;
		delete [] buf ;
		return false ;
	}
	buf[len] = 0 ;
	out.SetString( buf ) ;
	delete [] buf ;

	return true ;
}

// 创建XML数据的处理
bool CServiceCaller::CreateRequestXml( CQString &sXml, const char *id, const char *service, const char *method , CKeyValue &item )
{
	CXmlBuilder XmlOb("Request","Param","Item");

	XmlOb.SetRootAttribute( "id", id ) ;
	XmlOb.SetRootAttribute( "service", service );
	XmlOb.SetRootAttribute( "method", method );

	CQString stemp ;
	// 取得是否有请求参数
	int count = item.GetSize() ;
	if ( count > 0 ) {
		for ( int i = 0; i < count; ++ i ) {
			_KeyValue &vk = item.GetVal( i ) ;
			// 如果数据为空的话就不添加处理了
			if ( vk.key.empty() || vk.val.empty() ) {
				continue ;
			}
			// 转成UTF-8的编码处理
			if ( ! locale2utf8( vk.val.c_str() , vk.val.length() , stemp ) ){
				// 如果数据为空则不传
				continue ;
			}
			// 处理数据
			XmlOb.SetItemAttribute( vk.key.c_str(), stemp.GetBuffer() ) ;
		}
		XmlOb.InsertItem();
	}

	// 处理XML数据
	XmlOb.GetXmlText(sXml);
	// 处理的XML的数据
	OUT_PRINT( NULL, 0, "XML" , "Request GBK XML:%s" , sXml.GetBuffer() ) ;

	return ( !sXml.IsEmpty() ) ;
}

// 处理错误的情况
void CServiceCaller::ProcessError( unsigned int seqid , bool remove )
{
	// 清理回调TABLE中SEQ对应MAP
	if ( remove )
		_seq2msgid.RemoveSeq( seqid ) ;

	string skey ;
	// 清理需要缓存的数据标志，如果有值但出错的情况失添加一个错误数据
	if ( _seq2key.GetSeqKey( seqid , skey ) ) {
		// 添加错误的数据缓存
		_datacache.AddSession( skey, "error response" ) ;
	}
	_seq2key2.GetSeqKey( seqid, skey ) ;

	char key[256]= {0};
	_pEnv->GetCacheKey( seqid , key ) ;
	// 清理内部协议缓存数据
	_innerdata.RemoveSession( key ) ;
	// 清理数据缓存
	_pEnv->GetMsgCache()->Remove( key ) ;

	// 需要处理调用当前出错时间
	_macref.Dec( seqid, true ) ;
}

// 处理HttpCaller回调
void CServiceCaller::ProcessResp( unsigned int seqid, const char *xml, const int len , const int err )
{
	// 解析对应的XML结果处理
	if ( xml == NULL || err != HTTP_CALL_SUCCESS || len == 0 ) {
		OUT_ERROR( NULL, 0, "Caller" , "Process seqid %u, error %d" , seqid, err ) ;
		ProcessError( seqid , true ) ;
		return ;
	}

	// 处理XML的响应情况
	const char *ptr = strstr( xml , "Response" ) ;
	// 如果数据格式不正确
	if ( ptr == NULL || strstr( xml , "Result") == NULL || strstr( xml, "Item" ) == NULL ) {
		OUT_ERROR( NULL, 0, "Caller" , "ProcessResponse seqid %d , data error, xml %s", seqid , xml ) ;
		ProcessError( seqid , true ) ;
		return ;
	}

	// 取得对应序号ID
	unsigned int msgid = 0;
	if ( ! _seq2msgid.GetSeqMsg( seqid , msgid ) ) {
		OUT_ERROR( NULL, 0, "Caller" , "ProcessResponse seqid %d , msg id empty", seqid ) ;
		ProcessError( seqid , false ) ;
		return;
	}

	ServiceTable::iterator it = _srv_table.find( msgid ) ;
	// 处理消息对应ID回调方法
	if ( it == _srv_table.end() ) {
		OUT_ERROR( NULL, 0, "Caller" , "ProcessResponse seqid %d , call method empty", seqid ) ;
		ProcessError( seqid , false ) ;
		return ;
	}

	string key ;
	_seq2key2.GetSeqKey( seqid, key ) ;

	// 处理XML的数据,方便查找手机号与车牌号之间的对应关系日志的显示
	OUT_PRINT( NULL, 0, key.c_str(), "xml:%s" , (const char*)(ptr-1) ) ;
	// 调用处理方法
	if ( ! (this->*(it->second)) ( seqid , xml ) ) {
		// 如果数据不正确
		ProcessError( seqid , false ) ;
		return ;
	}
	// 正常就移掉记录
	_macref.Dec( seqid, false ) ;

	// 处理需要缓存的数据,无论正确与否都需要从CACHE中移除
	// 如果取得KEY值不为空则需要缓存数据
	if ( _seq2key.GetSeqKey( seqid , key ) ) {
		// 添加缓存中
		_datacache.AddSession( key, xml ) ;
	}
}

// 处理注册的数据
bool CServiceCaller::Proc_UP_EXG_MSG_REGISTER( unsigned int seq, const char *xml )
{
	// XML解析对象解析XML文本
	CXmlParser parser ;
	if ( ! parser.LoadXml( xml ) ){
		OUT_ERROR( NULL, 0, "XmlParser" , "UP_EXG_MSG_REGISTER Parser xml Error, xml: %s" , xml ) ;
		return false;
	}

	char key[256] = {0} ;
	_pEnv->GetCacheKey( seq, key ) ;

	int   len = 0 ;
	char *msg = _pEnv->GetMsgCache()->GetData( key, len ) ;
	if ( msg == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "UP_EXG_MSG_REGISTER failed, seq id %d" ,seq ) ;
		return false;
	}

	if ( len <= 0 || len < (int)sizeof(UpExgMsgRegister) ) {
		OUT_ERROR( NULL, 0, NULL, "UP_EXG_MSG_REGISTER failed, seq id %d" ,seq ) ;
		_pEnv->GetMsgCache()->FreeData( msg ) ;
		return false;
	}

	/** <Item vehicleno="瀹.00001" phoneNum="15000000001" terminalid="0000001" vehicleColor="2" cityid="6401" terminaltype="15000000001"/> */
	UpExgMsgRegister *req = ( UpExgMsgRegister *) msg ;

	unsigned char car_color = parser.GetInteger( "Response::Result::Item:vehicleColor" , 0 ) ;
	const char *  car_num   = parser.GetString( "Response::Result::Item:vehicleno", 0 ) ;
	const char *  city_id   = parser.GetString( "Response::Result::Item:cityid" , 0 ) ;

	if ( car_num == NULL || city_id == NULL ) {
		OUT_ERROR( NULL, 0 , NULL, "UP_EXG_MSG_REGISTER get car num failed, seq id %d" , seq ) ;
		_pEnv->GetMsgCache()->FreeData( msg ) ;
		return false;
	}

	req->exg_msg_header.vehicle_color = car_color;
	safe_memncpy( req->exg_msg_header.vehicle_no, car_num , sizeof(req->exg_msg_header.vehicle_no) ) ;

	const char *ptr = NULL ;

	// 处理两种情况注册数据
	ptr = parser.GetString( "Response::Result::Item:manufacturerid" , 0 ) ;
	if ( ptr != NULL ) {
		safe_memncpy( req->producer_id, ptr, sizeof(req->producer_id) ) ;
	}

	ptr = parser.GetString( "Response::Result::Item:terminalid" , 0 ) ;
	if ( ptr != NULL ) {
		safe_memncpy( req->terminal_id, ptr, sizeof(req->terminal_id) ) ;
	}

	ptr = parser.GetString( "Response::Result::Item:terminaltype" , 0 ) ;
	if ( ptr != NULL ) {
		safe_memncpy( req->terminal_model_type, ptr, sizeof(req->terminal_model_type) ) ;
	}

	ptr = parser.GetString( "Response::Result::Item:phoneNum" , 0 ) ;
	if ( ptr != NULL ) {
		// 这里从后往前拷贝，前面不足补零
		reverse_copy( req->terminal_simcode, sizeof(req->terminal_simcode), ptr, 0 ) ;
	}

	// 根据对应省发送给对应省处理
	_pEnv->GetPasClient()->HandleClientData( city_id , msg, len ) ;//发往PAS

	OUT_SEND( NULL, 0, city_id, "UP_EXG_MSG_REGISTER:%s, car color:%d", car_num , car_color );

	_pEnv->GetMsgCache()->FreeData( msg ) ;

	return true ;
}

bool CServiceCaller::Proc_UP_EXG_MSG_REAL_LOCATION( unsigned int seq, const char *xml )
{
	return ProcUpMsg<UpExgMsgRealLocation>( seq, xml , "UP_EXG_MSG_REAL_LOCATION") ;
}

// 主动上报驾驶员身份识别
bool CServiceCaller::Proc_UP_EXG_MSG_REPORT_DRIVER_INFO( unsigned int seq, const char *xml )
{
	return ProcUpMsg<UpExgMsgReportDriverInfo>( seq, xml , "UP_EXG_MSG_REPORT_DRIVER_INFO") ;
}

// 主动上报电子运单
bool CServiceCaller::Proc_UP_EXG_MSG_REPORT_EWAYBILL_INFO( unsigned int seq, const char *xml )
{
	return ProcUpMsg<UpExgMsgReportEwaybillInfo>( seq, xml , "UP_EXG_MSG_REPORT_EWAYBILL_INFO") ;
}

// 处理扩展消息模块
template<typename T>
bool CServiceCaller::ProcUpMsg( unsigned int seq, const char *xml , const char *name )
{
	// XML解析对象解析XML文本
	CXmlParser parser ;
	if ( ! parser.LoadXml( xml ) ){
		OUT_ERROR( NULL, 0, "XmlParser" , "%s Parser xml Error, xml: %s" , name, xml ) ;
		return false ;
	}

	char key[256] = {0} ;
	_pEnv->GetCacheKey( seq, key ) ;

	int   len = 0 ;
	char *msg = _pEnv->GetMsgCache()->GetData( key, len ) ;
	if ( msg == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "%s failed, seq id %d" , name, seq ) ;
		return false ;
	}

	if ( len <= 0 || len < (int)sizeof(T) ) {
		OUT_ERROR( NULL, 0, NULL, "%s failed, seq id %d" , name, seq ) ;
		_pEnv->GetMsgCache()->FreeData( msg ) ;
		return false ;
	}

	/** <Reponse><Result><Item vehicleno="" vehicleColor=""/></Result></Reponse> */
	// T *req = ( T *) msg ;

	unsigned char car_color = parser.GetInteger( "Response::Result::Item:vehicleColor" , 0 ) ;
	const char *  car_num   = parser.GetString( "Response::Result::Item:vehicleno", 0 ) ;
	const char *  city_id   = parser.GetString( "Response::Result::Item:cityid" , 0 ) ;

	// ycq 2013-11-14 waifujinjing
	city_id = "999999";

	if ( car_num == NULL || city_id == NULL ) {
		OUT_ERROR( NULL, 0 , NULL, "%s get car num failed, seq id %d, xml %s" , name, seq , xml ) ;
		_pEnv->GetMsgCache()->FreeData( msg ) ;
		return false ;
	}

	BaseMsgHeader *msgheader = (BaseMsgHeader*) (msg + sizeof(Header));
	msgheader->vehicle_color = car_color;
	safe_memncpy( msgheader->vehicle_no, car_num , sizeof(msgheader->vehicle_no) ) ;

	// 根据对应省发送给对应省处理
	_pEnv->GetPasClient()->HandleClientData( city_id , msg, len ) ;//发往PAS

	OUT_SEND( NULL, 0, city_id , "%s:%s, color:%d", name, car_num , car_color );

	_pEnv->GetMsgCache()->FreeData( msg ) ;

	return true ;
}

// 处理事件监听
bool CServiceCaller::Proc_UP_CTRL_MSG_MONITOR_VEHICLE_ACK( unsigned int seq, const char *xml )
{
	return ProcUpMsg<UpCtrlMsgMonitorVehicleAck>( seq, xml , "UP_CTRL_MSG_MONITOR_VEHICLE_ACK" ) ;
}

// 处理文本下发应答
bool CServiceCaller::Proc_UP_CTRL_MSG_TEXT_INFO_ACK( unsigned int seq, const char *xml )
{
	return ProcUpMsg<UpCtrlMsgTextInfoAck>( seq, xml , "UP_CTRL_MSG_TEXT_INFO_ACK" ) ;
}

// 处理行驶记录仪数据
bool CServiceCaller::Proc_UP_CTRL_MSG_TAKE_TRAVEL_ACK( unsigned int seq, const char *xml )
{
	return ProcUpMsg<UpCtrlMsgTaketravel>( seq, xml , "UP_CTRL_MSG_TAKE_TRAVEL_ACK" ) ;
}

// 处理车辆紧急接入
bool CServiceCaller::Proc_UP_CTRL_MSG_EMERGENCY_MONITORING_ACK( unsigned int seq, const char *xml )
{
	return ProcUpMsg<UpCtrlMsgEmergencyMonitoringAck>( seq, xml, "UP_CTRL_MSG_EMERGENCY_MONITORING_ACK" ) ;
}

// 构建内部协议数据
static const string buildsenddata( const string &cmd, const string &szseq, const string &macid, const string &inner )
{
	string sdata = cmd ;
	sdata += " " ;
	sdata += szseq ;
	sdata += " " ;
	sdata += macid ;
	sdata += inner ;
	return sdata ;
}

// 处理下发车辆控制请求
bool CServiceCaller::Proc_DOWN_CTRL_MSG( unsigned int seq, const char *xml )
{
	// 处理MACID
	char key[256]   = {0} ;
	char macid[128] = {0} ;

	string inner ;
	if ( ! ParsePhoneXml( seq, xml, key, macid, inner ) ) {
		OUT_ERROR( NULL, 0, "Caller" , "DOWN_CTRL_MSG error" ) ;
		return false ;
	}

	string senddata = buildsenddata("CAITS" , key, macid, inner ) ;

	// 根据MACID的路由进行数据发送
	((IMsgClient*)_pEnv->GetMsgClient())->HandleUpMsgData( macid , senddata.c_str(), senddata.length() ) ;

	return true ;
}

// 针对拍照特殊处理
bool CServiceCaller::Proc_DOWN_CTRL_MSG_TAKE_PHOTO_REQ( unsigned int seq, const char *xml )
{
	// 处理MACID
	char key[256]   = {0} ;
	char macid[128] = {0} ;

	string inner ;
	if ( ! ParsePhoneXml( seq, xml, key, macid, inner ) ) {
		OUT_ERROR( NULL, 0, "Caller" , "DOWN_CTRL_MSG_TAKE_PHOTO_REQ error" ) ;
		return false ;
	}

	int   len = 0 ;
	char *msg = _pEnv->GetMsgCache()->GetData( key, len ) ;
	if ( msg == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "DOWN_CTRL_MSG_TAKE_PHOTO_REQ failed, seq id %d" ,seq ) ;
		return false ;
	}

	char szKey[512] = {0};
	// 重组拍的键值，转成ome_phone_msgid的KEY存放到CACHE，方便异步从HTTP读取图片处理
	sprintf( szKey, "%s_%d" , macid, UP_CTRL_MSG_TAKE_PHOTO_ACK ) ;
	// 重新存放数据
	_pEnv->GetMsgCache()->AddData( szKey, msg , len ) ;
	// 释放原来数据
	_pEnv->GetMsgCache()->FreeData( msg ) ;

	string senddata = buildsenddata("CAITS" , key, macid, inner ) ;

	// 根据MACID的路由进行数据发送
	((IMsgClient*)_pEnv->GetMsgClient())->HandleUpMsgData( macid , senddata.c_str(), senddata.length() ) ;

	return true ;
}

// 处理平台类
bool CServiceCaller::Proc_DOWN_PLATFORM_MSG( unsigned int seq, const char *xml )
{
	// 这里不需处理任何
	OUT_SEND( NULL, 0, NULL, "DOWN_PLATFORM_MSG , seq id %d" , seq );
	return true ;
}

// 处理平台间报文自动应答处理
bool CServiceCaller::Proc_DOWN_PLATFORM_MSG_INFO_REQ( unsigned int seq, const char *xml )
{
	// 处理报警办自动应答处理
	char key[256] = {0} ;
	_pEnv->GetCacheKey( seq, key ) ;

	int len = 0 ;
	char *buf = _pEnv->GetMsgCache()->GetData( key, len ) ;
	if ( buf == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "Proc_DOWN_PLATFORM_MSG_INFO_REQ get message seq %d cache error" , seq ) ;
		return false ;
	}

	// 如果取得缓存的数据长度小于平台间应答
	if ( len < (int)sizeof(UpPlatFormMsgInfoAck) ) {
		OUT_ERROR( NULL, 0, NULL, "Proc_DOWN_PLATFORM_MSG_INFO_REQ length %d, less than need length %d" , len , sizeof(UpPlatFormMsgInfoAck) ) ;
		_pEnv->GetMsgCache()->FreeData( buf ) ;
		return false ;
	}

	UpPlatFormMsgInfoAck *ack = (UpPlatFormMsgInfoAck *) buf ;
	int accesscode = ntouv32( ack->header.access_code ) ;

	// 自动回应处理
	_pEnv->GetPasClient()->HandlePasUpData( accesscode, buf, len ) ;

	OUT_PRINT( NULL, 0, NULL, "UP_PLATFORM_MSG_INFO_ACK, accesscode: %d" , accesscode ) ;

	_pEnv->GetMsgCache()->FreeData( buf ) ;

	return true ;
}

// 处理报警类
bool CServiceCaller::Proc_DOWN_WARN_MSG( unsigned int seq, const char *xml )
{
	OUT_SEND( NULL, 0, NULL, "DOWN_WARN_MSG , seq id %d", seq ) ;
	return true ;
}

// 处理报警督办自动应答
bool CServiceCaller::Proc_DOWN_WARN_MSG_URGE_TODO_REQ( unsigned int seq, const char *xml )
{
	// 处理报警办自动应答处理
	char key[256] = {0} ;
	_pEnv->GetCacheKey( seq, key ) ;

	int len = 0 ;
	char *buf = _pEnv->GetMsgCache()->GetData( key, len ) ;
	if ( buf == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "Proc_DOWN_WARN_MSG_URGE_TODO_REQ get message seq %d cache error" , seq ) ;
		return false ;
	}
	// 校验长度是否正确
	if ( len < (int)sizeof(UpWarnMsgUrgeTodoAck) ) {
		OUT_ERROR( NULL, 0, NULL, "Proc_DOWN_WARN_MSG_URGE_TODO_REQ  data length %d, less than need length: %d" , len, sizeof(UpWarnMsgUrgeTodoAck) ) ;
		_pEnv->GetMsgCache()->FreeData( buf ) ;
		return false ;
	}

	UpWarnMsgUrgeTodoAck *resp = ( UpWarnMsgUrgeTodoAck *) buf ;
	int access_code = ntouv32( resp->header.access_code ) ;
	// 根据对应省发送给对应省处理
	_pEnv->GetPasClient()->HandlePasUpData( access_code , buf, len ) ;//发往PAS
	// 自动应答处理报警督办
	OUT_SEND( NULL, 0, NULL, "UP_WARN_MSG_URGE_TODO_ACK:%s, color:%d, accesscode:%d",
			resp->warn_msg_header.vehicle_no , resp->warn_msg_header.vehicle_color, access_code );

	_pEnv->GetMsgCache()->FreeData( buf ) ;

	return true;
}

// 处理位置上报和消息
bool CServiceCaller::Proc_DOWN_EXG_MSG_CAR_LOCATION( unsigned int seq, const char *xml )
{
	// 处理MACID
	char key[256]   = {0} ;
	char macid[128] = {0} ;

	string inner ;
	if ( ! ParsePhoneXml( seq, xml, key, macid, inner ) ) {
		OUT_ERROR( NULL, 0, "Caller" , "DOWN_EXG_MSG_CAR_LOCATION error" ) ;
		return false ;
	}

	string senddata = buildsenddata("CAITS" , "0_0", macid, inner ) ;

	// 根据MACID的路由进行数据发送
	((IMsgClient*)_pEnv->GetMsgClient())->HandleUpMsgData( macid , senddata.c_str(), senddata.length() ) ;

	return true ;
}

// 车辆定位信息交换
bool CServiceCaller::Proc_DOWN_EXG_MSG_HISTORY_ARCOSSAREA( unsigned int seq, const char *xml )
{
	// 处理MACID
	char key[256]   = {0} ;
	char macid[128] = {0} ;

	string inner ;
	if ( ! ParsePhoneXml( seq, xml, key, macid, inner ) ) {
		OUT_ERROR( NULL, 0, "Caller" , "DOWN_EXG_MSG_HISTORY_ARCOSSAREA error" ) ;
		return false;
	}

	vector<string> vec ;
	if ( ! splitvector( inner, vec, "|", 0 ) ) {
		OUT_ERROR( NULL, 0, "Caller" , "DOWN_EXG_MSG_HISTORY_ARCOSSAREA get inner gps failed" ) ;
		return false;
	}

	char head[512] = {0} ;
	sprintf( head, "CAITS 0_0 %s 4 U_REPT {TYPE:0," , macid ) ;

	// 处理多个下发的数据
	for( int i = 0; i < (int) vec.size(); ++ i ) {
		string sdata = head ;
		sdata += vec[i] ;
		sdata += "} \r\n" ;

		// 根据MACID的路由进行数据发送
		((IMsgClient*)_pEnv->GetMsgClient())->HandleUpMsgData( macid , sdata.c_str(), sdata.length() ) ;
	}
	return true ;
}

// 交换车辆的静态信息
bool CServiceCaller::Proc_DOWN_EXG_MSG_CAR_INFO( unsigned int seq, const char *xml )
{
	// 处理MACID
	char key[256]   = {0} ;
	char macid[128] = {0} ;

	string inner ;
	if ( ! ParsePhoneXml( seq, xml, key, macid, inner ) ) {
		OUT_ERROR( NULL, 0, "Caller" , "DOWN_EXG_MSG_CAR_INFO error" ) ;
		return false ;
	}

	string sdata = buildsenddata( "CAITS" , key , macid, inner ) ;
	// 根据MACID的路由进行数据发送
	((IMsgClient*)_pEnv->GetMsgClient())->HandleUpMsgData( macid , sdata.c_str(), sdata.length() ) ;

	return true ;
}

// 如果调用服务失败，直接回应成功
template<typename T>
bool CServiceCaller::ProcDownExgReturnMsg( unsigned int seq, const char *id )
{
	int   len = 0 ;
	char key[128] = {0};
	_pEnv->GetCacheKey( seq, key ) ;

	char *msg = _pEnv->GetMsgCache()->GetData( key, len ) ;
	if ( msg == NULL ) {
		OUT_ERROR( NULL, 0, "Caller", "Send %s", id ) ;
		return false ;
	}

	T *ack = ( T * ) msg ;

	int accesscode = ntouv32( ack->header.access_code ) ;
	// 自动应答处理
	_pEnv->GetPasClient()->HandlePasUpData( accesscode, msg , len ) ;

	_pEnv->GetMsgCache()->FreeData( msg ) ;

	OUT_SEND( NULL, 0, "Caller", "Send %s:%s, color:%d, accesscode:%d",
			id , ack->exg_msg_header.vehicle_no, ack->exg_msg_header.vehicle_color , accesscode ) ;

	return true ;
}

// 启动跨域请求
bool CServiceCaller::Proc_DOWN_EXG_MSG_RETURN_STARTUP( unsigned int seq, const char *xml )
{
	// 处理MACID
	char key[256]   = {0} ;
	char macid[128] = {0} ;

	string inner ;
	if ( ! ParsePhoneXml( seq, xml, key, macid, inner ) ) {
		OUT_ERROR( NULL, 0, "Caller" , "DOWN_EXG_MSG_RETURN_STARTUP error" ) ;
		return ProcDownExgReturnMsg<UpExgMsgReturnStartupAck>( seq, "UP_EXG_MSG_RETURN_STARTUP_ACK" ) ;
	}

	string sdata = buildsenddata( "CAITS" , key , macid, inner ) ;
	// 根据MACID的路由进行数据发送
	((IMsgClient*)_pEnv->GetMsgClient())->HandleUpMsgData( macid , sdata.c_str(), sdata.length() ) ;

	int   len = 0 ;
	char *msg = _pEnv->GetMsgCache()->GetData( key, len ) ;
	assert( msg != NULL ) ;
	UpExgMsgReturnStartupAck *ack = ( UpExgMsgReturnStartupAck * ) msg ;

	int accesscode = ntouv32( ack->header.access_code ) ;
	// 自动应答处理
	_pEnv->GetPasClient()->HandlePasUpData( accesscode, msg , len ) ;

	size_t pos = inner.find( "RETURNSTARTUP:" ) ;
	assert( pos != string::npos ) ;

	sdata = inner.substr( 0, pos ) ;
	sdata += "RETURNSTARTUP:} \r\n" ;

	string sends = buildsenddata( "CAITR" , key , macid, sdata ) ;

	// 发送内部协议
	((IMsgClient*)_pEnv->GetMsgClient())->HandleUpMsgData( macid, sends.c_str(), sends.length() ) ;

	_pEnv->GetMsgCache()->FreeData( msg ) ;

	OUT_SEND( NULL, 0, macid, "Send UP_EXG_MSG_RETURN_STARTUP_ACK:%s, color:%d",
			ack->exg_msg_header.vehicle_no, ack->exg_msg_header.vehicle_color ) ;

	return true ;
}

// 结束跨域请求
bool CServiceCaller::Proc_DOWN_EXG_MSG_RETURN_END( unsigned int seq, const char *xml )
{
	// 处理MACID
	char key[256]   = {0} ;
	char macid[128] = {0} ;

	string inner ;
	if ( ! ParsePhoneXml( seq, xml, key, macid, inner ) ) {
		OUT_ERROR( NULL, 0, "Caller" , "DOWN_EXG_MSG_RETURN_END error" ) ;
		return ProcDownExgReturnMsg<UpExgMsgReturnEndAck>( seq, "UP_EXG_MSG_RETURN_END_ACK" ) ;
	}

	string sdata = buildsenddata( "CAITR" , key , macid, inner ) ;
	// 根据MACID的路由进行数据发送
	((IMsgClient*)_pEnv->GetMsgClient())->HandleUpMsgData( macid , sdata.c_str(), sdata.length() ) ;

	int   len = 0 ;
	char *msg = _pEnv->GetMsgCache()->GetData( key, len ) ;
	assert( msg != NULL ) ;
	UpExgMsgReturnEndAck *ack = ( UpExgMsgReturnEndAck * ) msg ;

	int accesscode = ntouv32( ack->header.access_code ) ;
	// 自动应答处理
	_pEnv->GetPasClient()->HandlePasUpData( accesscode, msg , len ) ;

	size_t pos = inner.find( "RETURNEND:" ) ;
	assert( pos != string::npos ) ;

	sdata = inner.substr( 0, pos ) ;
	sdata += "RETURNEND:} \r\n" ;

	string sends = buildsenddata( "CAITR" , key , macid, sdata ) ;

	// 发送内部协议
	((IMsgClient*)_pEnv->GetMsgClient())->HandleUpMsgData( macid, sends.c_str(), sends.length() ) ;

	_pEnv->GetMsgCache()->FreeData( msg ) ;

	OUT_SEND( NULL, 0, macid, "Send UP_EXG_MSG_RETURN_END_ACK:%s, color:%d",
			ack->exg_msg_header.vehicle_no, ack->exg_msg_header.vehicle_color ) ;

	return true ;
}

// 处理补发数据应答
bool CServiceCaller::Proc_DOWN_EXG_MSG_ACK( unsigned int seq, const char *xml )
{
	// 处理MACID
	char key[256]   = {0} ;
	char macid[128] = {0} ;

	string inner ;
	if ( ! ParsePhoneXml( seq, xml, key, macid, inner ) ) {
		OUT_ERROR( NULL, 0, "Caller" , "DOWN_EXG_MSG_ACK error" ) ;
		return false ;
	}

	string sdata = inner ;
	// 处理两种情况一种是直接发送的情况，一种需要重新组成内部协议的情况
	if ( strncmp( inner.c_str(), "CAIT", 4 ) != 0 ) {
		sdata = buildsenddata( "CAITR" , key , macid, inner ) ;
	} else {
		size_t pos = sdata.find( "MACID" ) ;
		if ( pos != string::npos ) {
			// 重新处理MACID
			sdata.replace( pos, 5 , macid ) ;
		}
	}
	// 根据MACID的路由进行数据发送
	((IMsgClient*)_pEnv->GetMsgClient())->HandleUpMsgData( macid , sdata.c_str(), sdata.length() ) ;

	return true ;
}

// 解析手机号XML的值
bool CServiceCaller::ParsePhoneXml( unsigned int seq, const char *xml , char *key, char *macid,  string &inner )
{
	// XML解析对象解析XML文本
	CXmlParser parser ;
	if ( ! parser.LoadXml( xml ) ){
		OUT_ERROR( NULL, 0, "XmlParser" , "Parser xml Error, xml: %s" , xml ) ;
		return false;
	}

	_pEnv->GetCacheKey( seq, key ) ;

	// 需要将内部协议发送MSG处理
	if ( ! _innerdata.GetSession( key, inner ) ) {
		OUT_ERROR( NULL, 0, "GetSession", "Get Inner failed" ) ;
		return false ;
	}
	_innerdata.RemoveSession( key ) ;

	const char *  ome_code    = parser.GetString( "Response::Result::Item:terminaltype" , 0 ) ;
	const char *  phone_num   = parser.GetString( "Response::Result::Item:phoneNum", 0 ) ;

	if ( ome_code == NULL || phone_num == NULL ) {
		OUT_ERROR( NULL, 0, "Xml" , "parser xml result get phoneNum and terminal type failed" ) ;
		return false;
	}

	// 处理MACID
	sprintf( macid, "%s_%s" , ome_code, phone_num ) ;

	return true ;
}

// 上报驾驶员身份识别
bool CServiceCaller::Proc_DOWN_EXG_MSG_REPORT_DRIVER_INFO( unsigned int seq, const char *xml )
{
	// XML解析对象解析XML文本
	CXmlParser parser ;
	if ( ! parser.LoadXml( xml ) ){
		OUT_ERROR( NULL, 0, "XmlParser" , "UP_EXG_MSG_REPORT_DRIVER_INFO Parser xml Error, xml: %s" , xml ) ;
		return false ;
	}

	char key[256] = {0} ;
	_pEnv->GetCacheKey( seq, key ) ;

	int   len = 0 ;
	char *msg = _pEnv->GetMsgCache()->GetData( key, len ) ;
	if ( msg == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "UP_EXG_MSG_REPORT_DRIVER_INFO failed, seq id %d" ,seq ) ;
		return false ;
	}

	if ( len <= 0 || len < (int)sizeof(UpExgMsgReportDriverInfo) ) {
		OUT_ERROR( NULL, 0, NULL, "UP_EXG_MSG_REPORT_DRIVER_INFO failed, seq id %d" ,seq ) ;
		_pEnv->GetMsgCache()->FreeData( msg ) ;
		return false ;
	}

	/** <Item vehicleno="" vehicleColor="" cityid="" driverName="" driverNo="" driverCertificate="" certificateAgency=""/> */
	const char *cityid  = parser.GetString( "Response::Result::Item:cityid" , 0 ) ;
	const char *car_num = parser.GetString( "Response::Result::Item:vehicleno" , 0 ) ;

	// 如果取得当前数据为空则直接返回
	if ( cityid == NULL || car_num == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "UP_EXG_MSG_REPORT_DRIVER_INFO get cityid and car_num null, seq %d, xml %s" , seq , xml ) ;
		_pEnv->GetMsgCache()->FreeData( msg ) ;
		return false ;
	}

	UpExgMsgReportDriverInfo *rsp = ( UpExgMsgReportDriverInfo *) msg ;

	safe_memncpy( rsp->exg_msg_header.vehicle_no, car_num, sizeof(rsp->exg_msg_header.vehicle_no) ) ;
	rsp->exg_msg_header.vehicle_color = parser.GetInteger( "Response::Result::Item:vehicleColor" , 0 ) ;
	safe_memncpy( rsp->driver_name, parser.GetString( "Response::Result::Item:driverName" , 0 ) , sizeof(rsp->driver_name) ) ;
	safe_memncpy( rsp->driver_id  , parser.GetString( "Response::Result::Item:driverNo", 0 )   , sizeof(rsp->driver_id) ) ;
	safe_memncpy( rsp->org_name   , parser.GetString( "Response::Result::Item:driverCertificate", 0 )  , sizeof(rsp->org_name) ) ;
	safe_memncpy( rsp->licence  , parser.GetString( "Response::Result::Item:certificateAgency", 0 )  , sizeof(rsp->licence) ) ;

	_pEnv->GetPasClient()->HandleClientData( cityid, msg, len ) ;

	OUT_SEND( NULL, 0, cityid, "UP_EXG_MSG_REPORT_DRIVER_INFO:%s, color:%d", car_num , rsp->exg_msg_header.vehicle_color );

	_pEnv->GetMsgCache()->FreeData( msg ) ;

	return true ;
}

// 上报电子运单数据
bool CServiceCaller::Proc_DOWN_EXG_MSG_TAKE_WAYBILL_REQ( unsigned int seq, const char *xml )
{
	// XML解析对象解析XML文本
	CXmlParser parser ;
	if ( ! parser.LoadXml( xml ) ){
		OUT_ERROR( NULL, 0, "XmlParser" , "UP_EXG_MSG_TAKE_WAYBILL_ACK Parser xml Error, xml: %s" , xml ) ;
		return false ;
	}

	char key[256] = {0} ;
	_pEnv->GetCacheKey( seq, key ) ;

	int   len = 0 ;
	char *msg = _pEnv->GetMsgCache()->GetData( key, len ) ;
	if ( msg == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "UP_EXG_MSG_TAKE_WAYBILL_ACK failed, seq id %d" ,seq ) ;
		return false ;
	}

	if ( len <= 0 || len < (int)sizeof(UpExgMsgReportEwaybillInfo) ) {
		OUT_ERROR( NULL, 0, NULL, "UP_EXG_MSG_TAKE_WAYBILL_ACK failed, seq id %d" ,seq ) ;
		_pEnv->GetMsgCache()->FreeData( msg ) ;
		return false ;
	}

	/** <Item vehicleno="" vehicleColor="" cityid="" eticketContent=""/> */
	const char *cityid  = parser.GetString( "Response::Result::Item:cityid" , 0 ) ;
	const char *car_num = parser.GetString( "Response::Result::Item:vehicleno" , 0 ) ;

	// 如果取得当前数据为空则直接返回
	if ( cityid == NULL || car_num == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "UP_EXG_MSG_TAKE_WAYBILL_ACK get cityid and car_num null, seq %d, xml %s" , seq , xml ) ;
		_pEnv->GetMsgCache()->FreeData( msg ) ;
		return false ;
	}

	UpExgMsgReportEwaybillInfo *rsp = ( UpExgMsgReportEwaybillInfo *) msg ;

	safe_memncpy( rsp->exg_msg_header.vehicle_no, car_num , sizeof(rsp->exg_msg_header.vehicle_no) ) ;
	rsp->exg_msg_header.vehicle_color = parser.GetInteger( "Response::Result::Item:vehicleColor" , 0 ) ;

	CQString content = parser.GetString( "Response::Result::Item:eticketContent" , 0  ) ;
	rsp->exg_msg_header.data_length = ntouv32( sizeof(int) + content.GetLength() ) ;
	rsp->ewaybill_length 		    = ntouv32( content.GetLength() ) ;
	rsp->header.msg_len	 			= ntouv32( content.GetLength() + len + sizeof(Footer) ) ;

	DataBuffer buf ;
	buf.writeBlock( msg, len ) ;
	buf.writeBlock( content.GetBuffer(), content.GetLength() ) ;

	Footer footer ;
	buf.writeBlock( &footer, sizeof(footer) ) ;

	_pEnv->GetPasClient()->HandleClientData( cityid, buf.getBuffer(), buf.getLength() ) ;

	OUT_SEND( NULL, 0, cityid, "UP_EXG_MSG_TAKE_WAYBILL_ACK:%s, color:%d", car_num , rsp->exg_msg_header.vehicle_color);

	_pEnv->GetMsgCache()->FreeData( msg ) ;

	return true ;
}

// 取得车辆静态信息，自动服务处理
bool CServiceCaller::Proc_DOWN_BASE_MSG_VEHICLE_ADDED( unsigned int seq, const char *xml )
{
	// XML解析对象解析XML文本
	CXmlParser parser ;
	if ( ! parser.LoadXml( xml ) ){
		OUT_ERROR( NULL, 0, "XmlParser" , "DOWN_BASE_MSG_VEHICLE_ADDED Parser xml Error, xml: %s" , xml ) ;
		return false ;
	}

	char key[256] = {0} ;
	_pEnv->GetCacheKey( seq, key ) ;

	int   len = 0 ;
	char *msg = _pEnv->GetMsgCache()->GetData( key, len ) ;
	if ( msg == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "UP_BASE_MSG_VEHICLE_ADDED_ACK failed, seq id %d" ,seq ) ;
		return false ;
	}

	if ( len <= 0 || len < (int)sizeof(UpbaseMsgVehicleAddedAck) ) {
		OUT_ERROR( NULL, 0, NULL, "UP_BASE_MSG_VEHICLE_ADDED_ACK failed, seq id %d" ,seq ) ;
		_pEnv->GetMsgCache()->FreeData( msg ) ;
		return false ;
	}

	/** <Item vehicleno="" plateColorId="" vehicletypeId="" typeName="" transTypeDesc="" city="" corpId="" corpName="" linkPhone="" spId="" spName=""/> */
	const char *car_num = parser.GetString( "Response::Result::Item:vehicleno" , 0 ) ;
	if ( car_num == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "UP_BASE_MSG_VEHICLE_ADDED_ACK get cityid and car_num null, seq %d, xml %s" , seq , xml ) ;
		_pEnv->GetMsgCache()->FreeData( msg ) ;
		return false ;
	}

	// <Item vehicleno="京A8888" plateColorId="1" vehicletypeId="111" typeName="11" transTypeDesc="12" city="1000" corpId="12" corpName="test" linkPhone="15000000001" spId="11" spName="test"/>
	UpbaseMsgVehicleAddedAck *rsp = ( UpbaseMsgVehicleAddedAck *) msg ;
	// 构建静态数据内容  "VIN","VEHICLE_COLOR","VEHICLE_TYPE","TRANS_TYPE","VEHICLE_NATIONALITY","OWERS_NAME"
	CQString sBaseData ;
	sBaseData.AppendBuffer( "VIN:=" ) ;
	sBaseData.AppendBuffer( parser.GetString( "Response::Result::Item:vehicleno" , 0 ) ) ;
	sBaseData.AppendBuffer( ";" ) ;
	sBaseData.AppendBuffer( "VEHICLE_COLOR:=" ) ;
	sBaseData.AppendBuffer( parser.GetString( "Response::Result::Item:plateColorId", 0 ) ) ;
	sBaseData.AppendBuffer( ";" ) ;

	if ( _istester & 0x04 ) {
		// VIN:=测A77889;VEHICLE_COLOR:=2;VEHICLE_TYPE:=轿车;TRANS_TYPE:=010;VEHICLE_NATIONALITY:=640000;OWERS_ID:=;OWERS_NAME:=中交测试;OWERS_ORIG_ID:=;OWERS_ORIG_NAME:=;OWERS_TEL:=;
		sBaseData.AppendBuffer( "VEHICLE_TYPE:=轿车;TRANS_TYPE:=010;VEHICLE_NATIONALITY:=640000;OWERS_ID:=;OWERS_NAME:=中交测试;OWERS_ORIG_ID:=1;OWERS_ORIG_NAME:=中交测试;OWERS_TEL:=13801088478;" ) ;
	} else {
		sBaseData.AppendBuffer( "VEHICLE_TYPE:=" ) ;
		sBaseData.AppendBuffer( parser.GetString( "Response::Result::Item:typeName" , 0 ) ) ;
		sBaseData.AppendBuffer( ";" ) ;
		sBaseData.AppendBuffer( "TRANS_TYPE:=" ) ;
		sBaseData.AppendBuffer( parser.GetString( "Response::Result::Item:transTypeDesc" , 0 ) ) ;
		sBaseData.AppendBuffer( ";" ) ;
		sBaseData.AppendBuffer( "VEHICLE_NATIONALITY:=" ) ;
		sBaseData.AppendBuffer( parser.GetString( "Response::Result::Item:city" , 0 ) ) ;
		sBaseData.AppendBuffer( ";" ) ;
		sBaseData.AppendBuffer( "OWERS_ID:=" ) ;
		sBaseData.AppendBuffer( parser.GetString( "Response::Result::Item:corpId" , 0 ) ) ;
		sBaseData.AppendBuffer( ";" ) ;
		sBaseData.AppendBuffer( "OWERS_NAME:=" ) ;
		sBaseData.AppendBuffer( parser.GetString( "Response::Result::Item:corpName" , 0 ) ) ;
		sBaseData.AppendBuffer( ";" ) ;
		sBaseData.AppendBuffer( "OWERS_ORIG_ID:=" ) ;
		sBaseData.AppendBuffer( parser.GetString( "Response::Result::Item:spId" , 0 ) ) ;
		sBaseData.AppendBuffer( ";" ) ;
		sBaseData.AppendBuffer( "OWERS_ORIG_NAME:=" ) ;
		sBaseData.AppendBuffer( parser.GetString( "Response::Result::Item:spName" , 0 ) ) ;
		sBaseData.AppendBuffer( ";" ) ;
		sBaseData.AppendBuffer( "OWERS_TEL:=" ) ;
		sBaseData.AppendBuffer( parser.GetString( "Response::Result::Item:linkPhone" , 0 ) ) ;
		sBaseData.AppendBuffer( ";" ) ;
	}

	rsp->header.msg_len  		= ntouv32( sBaseData.GetLength() + len + sizeof(Footer) ) ;
	rsp->msg_header.data_length = ntouv32( sBaseData.GetLength() ) ;

	DataBuffer buf ;
	buf.writeBlock( msg , len ) ;
	buf.writeBlock( sBaseData.GetBuffer(), sBaseData.GetLength() ) ;

	Footer footer ;
	buf.writeBlock( &footer, sizeof(footer) ) ;

	int access_code = ntouv32( rsp->header.access_code ) ;
	// 根据对应省发送给对应省处理
	_pEnv->GetPasClient()->HandlePasUpData( access_code , buf.getBuffer(), buf.getLength() ) ;//发往PAS

	OUT_SEND( NULL, 0, NULL, "UP_BASE_MSG_VEHICLE_ADDED_ACK:%s, color:%d, accesscode:%d",
			car_num , rsp->msg_header.vehicle_color, access_code );

	_pEnv->GetMsgCache()->FreeData( msg ) ;

	return true ;
}
