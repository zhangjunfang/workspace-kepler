/*
 * servicecaller.cpp
 *
 *  Created on: 2011-12-12
 *      Author: humingqing
 */

#include "srvcaller.h"
#include <xmlparser.h>
#include <comlog.h>
#include "BaseTools.h"
#include "murhash.h"
#include <tools.h>
#include "comutil.h"

#ifndef _WIN32
#include <strings.h>
#define stricmp strcasecmp
#endif

namespace CarService{

	#define HASH_SEED    0xffff

	static void ReverParseParamOfType(string &line, unsigned int type )
	{
		uint32_t temp = 0;
		vector <uint32_t> vec_type;

		for (unsigned int i = 0 ; i <sizeof(unsigned short)*8; i ++ ) {
			if ( ( type & ( 1 << i ) ) > 0 ) {
				temp = 1 << i;
				vec_type.push_back(temp);
			}
		}

		if ( vec_type.size() == 1 )
			line = uitodecstr(vec_type[0]).c_str();
		else {
			for (unsigned int i = 0 ; i < vec_type.size() ; i ++ ) {
				if ( i == vec_type.size() - 1 )
				{
					line += uitodecstr(vec_type[i]);
				}
				else
					line += uitodecstr(vec_type[i]) + "|";
			}
		}
	}

	static unsigned int ParseParamOfType( const char *line )
	{
		unsigned char type = 0;
		unsigned char temp = 0;

		vector < string > vec_temp;
		splitvector( line, vec_temp, "|", 0 );

		for (unsigned int i = 0 ; i < vec_temp.size() ; i ++ ) {
			temp = atoi( vec_temp[i].c_str() );
			switch ( temp )
			{
			case 2: //维修
				type |= 1 << 1;
				break;
			case 4: //救援
				type |= 1 << 2;
				break;
			case 8: //加油
				type |= 1 << 3;
				break;
			case 16: //保养
				type |= 1 << 4;
				break;
			case 32: //餐饮
				type |= 1 << 5;
				break;
			case 64: //住宿
				type |= 1 << 6;
				break;
			default:
				break;
			}
		}
		return type;
	}

	CSrvCaller::CSrvCaller( ) :
			_msgqueue( this ), _inited( false )
	{
		_packfactory = new CPackFactory( &_unpacker);

		_srv_table[UP_DISCOUNT_INFO_REQ]        = & CSrvCaller::Proc_UPDISCOUNT_INFO_REQ;
		_srv_table[UP_DETAIL_DISCOUNT_INFO_REQ] = & CSrvCaller::Proc_UPDETAIL_DISCOUNT_INFO_REQ;
		_srv_table[UP_UNION_BUSINESS_INFO_REQ]  = & CSrvCaller::Proc_UPUNION_BUSINESS_INFO_REQ;
		_srv_table[UP_DETAIL_UNION_BUSINESS_INFO_REQ] = & CSrvCaller::Proc_UPDETAIL_UNION_BUSINESS_INFO_REQ;

		_srv_table[UP_LOGIN_INFO_REQ]          = & CSrvCaller::Proc_UPLOGIN_INFO_REQ;
		_srv_table[UP_QUERY_BALLANCE_LIST_REQ] = & CSrvCaller::Proc_UPQUERY_BALLANCE_LIST_REQ;
		_srv_table[UP_QUERY_STORE_LIST_REQ]    = & CSrvCaller::Proc_UPQUERY_STORE_LIST_REQ;
		_srv_table[UP_QUERY_TRADE_LIST_REQ]    = & CSrvCaller::Proc_UPQUERY_TRADE_LIST_REQ;
		_srv_table[UP_QUERY_FAVORITE_LIST_REQ] = & CSrvCaller::Proc_UPQUERY_FAVORITE_LIST_REQ;
		_srv_table[UP_VIEW_FAVORITE_INFO_REQ]  = & CSrvCaller::Proc_UPVIEW_FAVORITE_INFO_REQ;
		_srv_table[UP_ADD_FAVORITE_REQ]        = & CSrvCaller::Proc_UPADD_FAVORITE_REQ;
		_srv_table[UP_DEL_FAVORITE_REQ]        = & CSrvCaller::Proc_UPDEL_FAVORITE_REQ;
		_srv_table[UP_GET_DESTINATION_REQ]     = & CSrvCaller::Proc_UPGET_DESTINATION_REQ;

		_srv_table[UP_QUERY_VIEW_STORE_INFO_REQ] = & CSrvCaller::Proc_UPQUERY_VIEW_STORE_INFO_REQ;
		_srv_table[UP_QUERY_DISCOUNT_LIST_REQ]   = & CSrvCaller::Proc_UPQUERY_DISCOUNT_LIST_REQ;
		_srv_table[UP_VIEW_DISCOUNT_INFO_REQ]    = & CSrvCaller::Proc_UPVIEW_DISCOUNT_INFO_REQ;
	}

	CSrvCaller::~CSrvCaller( )
	{
		Stop();

		if ( _packfactory != NULL ) {
		   delete _packfactory ;
		   _packfactory = NULL ;
		}
	}

	// 初始化
	bool CSrvCaller::Init( IPlugin *pEnv, const char *url, int sendthread, int recvthread, int queuesize )
	{
		_pEnv = pEnv;

		if ( ! _thread.init( 1, NULL, this ) ) {
			OUT_ERROR( NULL, 0, NULL, "init CSrvCaller queue thread failed" );
			return false;
		}
		_inited = true;

		// 记录调用hession的URL
		_callUrl.SetString( url );

		_httpcaller.SetReponse( this );

		return _httpcaller.Init( sendthread, recvthread, queuesize );
	}

	// 启动
	bool CSrvCaller::Start( void )
	{
		_thread.start();

		return _httpcaller.Start();
	}

	// 停止
	void CSrvCaller::Stop( void )
	{
		if ( ! _inited ) return;
		_inited = false;
		_monitor.notifyEnd();
		_thread.stop();

		_httpcaller.Stop();
	}

	//终端通用应答
	bool CSrvCaller::putTerminalCommonRsp( unsigned int fd, unsigned int cmd, CTerminalCommonRsp *msg, const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );
		item.SetVal( "result", ( msg->_result == 0 ) ? "0" : "1" );

		return ProcessMsg( msg->_header._type, seq, "TruckService", "putTerminalCommResponse", item );
	}

	// 查询打折优惠信息
	bool CSrvCaller::getUpQueryDiscountInfoReq( unsigned int fd, unsigned int cmd, CQueryDiscountInfoReq *msg,
			const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "type", chartodecstr( msg->_type ).c_str() ); //UINT8  优惠类型
		item.SetVal( "range", chartodecstr( msg->_range ).c_str() ); //查询范围

		item.SetVal( "sort_type", chartodecstr( msg->_sort_type ).c_str() ); //排序类型

		item.SetVal( "lon", uitodecstr( msg->_lon ).c_str() ); //经度
		item.SetVal( "lat", uitodecstr( msg->_lat ).c_str() ); //纬度

		item.SetVal( "offset", chartodecstr( msg->_offset ).c_str() ); //读取开始位置
		item.SetVal( "count", chartodecstr( msg->_count ).c_str() ); //记录条数

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getQueryDiscountInfoReq", item );
	}

	// 查询具体打折优惠信息
	bool CSrvCaller::getUpQueryDetailDiscountInfoReq( unsigned int fd, unsigned int cmd, CQueryDetailiscountInfoReq *msg,
			const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "discount_num",
				safestring( ( const char* ) msg->_discount_num, sizeof ( msg->_discount_num ) ).c_str() );

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getQueryDetailDiscountInfoReq", item );
	}

	//查询联盟商家信息
	bool CSrvCaller::getUpQueryUnionBusinessInfoReq( unsigned int fd, unsigned int cmd, CQueryUnionBusinessInfoReq *msg,
			const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;

		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "type", chartodecstr( msg->_type ).c_str() ); //UINT8  优惠类型
		item.SetVal( "range", chartodecstr( msg->_range ).c_str() ); //查询范围
		item.SetVal( "service_level", chartodecstr( msg->_service_level ).c_str() ); //服务等级
		item.SetVal( "sort_type", chartodecstr( msg->_sort_type ).c_str() ); //排序类型

		item.SetVal( "lon", uitodecstr( msg->_lon ).c_str() ); //经度
		item.SetVal( "lat", uitodecstr( msg->_lat ).c_str() ); //纬度

		item.SetVal( "offset", chartodecstr( msg->_offset ).c_str() ); //读取开始位置
		item.SetVal( "count", chartodecstr( msg->_count ).c_str() ); //记录条数

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getQueryUnionBusinessInfoReq", item );
	}

	//查询联盟具体商家信息
	bool CSrvCaller::getUpQueryDetailUnionBusinessInfoReq( unsigned int fd, unsigned int cmd,
			CQueryDetailUnionBusinessInfoReq *msg, const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "business_num",
				safestring( ( const char* ) msg->_business_num, sizeof ( msg->_business_num ) ).c_str() );

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getQueryDetailUnionBusinessInfoReq", item );
	}
	//用户登录
	bool CSrvCaller::getUpLoginInfoReq( unsigned int fd, unsigned int cmd, CLoginInfoReq *msg, const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "phone", safestring( ( const char* ) msg->_phone, sizeof ( msg->_phone ) ).c_str() );
		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpLoginInfoReq", item );
	}

	//联名卡余额查询
	bool CSrvCaller::getUpQueryBallanceListReq( unsigned int fd, unsigned int cmd, CQuery_Ballance_List_Req *msg,
			const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "usercode", safestring( ( const char* ) msg->_usercode, sizeof ( msg->_usercode ) ).c_str() );
		item.SetVal( "verifyCode", safestring( ( const char* ) msg->_verifyCode, sizeof ( msg->_verifyCode ) ).c_str() );

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpQueryBallanceListReq", item );
	}
	//查询门店
	bool CSrvCaller::getUpQueryStoreListReq( unsigned int fd, unsigned int cmd, CQuery_Store_List_Req *msg, const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "usercode", safestring( ( const char* ) msg->_usercode, sizeof ( msg->_usercode ) ).c_str() );
		item.SetVal( "verifyCode", safestring( ( const char* ) msg->_verifyCode, sizeof ( msg->_verifyCode ) ).c_str() );

		item.SetVal( "lon", uitodecstr( msg->_lon ).c_str() ); //经度
		item.SetVal( "lat", uitodecstr( msg->_lat ).c_str() ); //纬度
		item.SetVal( "scope", chartodecstr( msg->_scope ).c_str() ); //查询范围

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpQueryStoreListReq", item );
	}
	//查询门店详情
	bool CSrvCaller::getUpQueryViewStoreInfoReq( unsigned int fd, unsigned int cmd, CView_Store_Info_Req *msg,
			const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "usercode", safestring( ( const char* ) msg->_usercode, sizeof ( msg->_usercode ) ).c_str() );
		item.SetVal( "verifyCode", safestring( ( const char* ) msg->_verifyCode, sizeof ( msg->_verifyCode ) ).c_str() );

		item.SetVal( "storecode", safestring( ( const char* ) msg->_storecode, sizeof ( msg->_storecode ) ).c_str() );

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpQueryViewStoreInfoReq", item );
	}

	//新版本查询优惠信息
	bool CSrvCaller::getUpQueryDiscountListReq( unsigned int fd, unsigned int cmd, CQuery_Discount_List_Req *msg,
			const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "usercode", safestring( ( const char* ) msg->_usercode, sizeof ( msg->_usercode ) ).c_str() );
		item.SetVal( "verifyCode", safestring( ( const char* ) msg->_verifyCode, sizeof ( msg->_verifyCode ) ).c_str() );

		item.SetVal( "lon", uitodecstr( msg->_lon ).c_str() ); //经度
		item.SetVal( "lat", uitodecstr( msg->_lat ).c_str() ); //纬度

		string s_type = "";
		ReverParseParamOfType(s_type,msg->_type);
		item.SetVal( "type", s_type.c_str()); //收藏类型
		item.SetVal( "offset", chartodecstr( msg->_offset ).c_str() ); //读取开始位置
		item.SetVal( "count", chartodecstr( msg->_count ).c_str() ); //记录条数

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpQueryDiscountListReq", item );
	}

	//新版本查询优惠信息详细列表
	bool CSrvCaller::getUpViewDiscountInfoReq( unsigned int fd, unsigned int cmd, CView_Discount_Info_Req *msg,
			const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "usercode", safestring( ( const char* ) msg->_usercode, sizeof ( msg->_usercode ) ).c_str() );
		item.SetVal( "verifyCode", safestring( ( const char* ) msg->_verifyCode, sizeof ( msg->_verifyCode ) ).c_str() );

		item.SetVal( "discountCode",
				safestring( ( const char* ) msg->_discountCode, sizeof ( msg->_discountCode ) ).c_str() );

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpViewDiscountInfoReq", item );
	}
	//历史交易记录查询
	bool CSrvCaller::getUpQueryTradeListReq( unsigned int fd, unsigned int cmd, CQuery_Trade_List_Req *msg, const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "usercode", safestring( ( const char* ) msg->_usercode, sizeof ( msg->_usercode ) ).c_str() );
		item.SetVal( "verifyCode", safestring( ( const char* ) msg->_verifyCode, sizeof ( msg->_verifyCode ) ).c_str() );
		item.SetVal( "tradeType", chartodecstr( msg->_tradeType ).c_str() ); //交易类型

		item.SetVal( "beginTime", bcd2utc( ( char* ) msg->_beginTime ).c_str() ); //开始时间
		item.SetVal( "endTime", bcd2utc( ( char* ) msg->_endTime ).c_str() ); //结束时间

		item.SetVal( "offset", uitodecstr( msg->_offset ).c_str() ); //读取开始位置
		item.SetVal( "count", chartodecstr( msg->_count ).c_str() ); //记录条数

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpQueryTradeListReq", item );
	}

	//查询收藏列表
	bool CSrvCaller::getUpQueryFavoriteListReq( unsigned int fd, unsigned int cmd, CQuery_Favorite_List_Req *msg,
			const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "usercode", safestring( ( const char* ) msg->_usercode, sizeof ( msg->_usercode ) ).c_str() );
		item.SetVal( "verifyCode", safestring( ( const char* ) msg->_verifyCode, sizeof ( msg->_verifyCode ) ).c_str() );

		item.SetVal( "type", chartodecstr( msg->_type ).c_str() ); //收藏类型
		item.SetVal( "offset", chartodecstr( msg->_offset ).c_str() ); //读取开始位置
		item.SetVal( "count", chartodecstr( msg->_count ).c_str() ); //记录条数

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpQueryFavoriteListReq", item );

	}
	//查询收藏列表详情
	bool CSrvCaller::getUpViewFavoriteInfoReq( unsigned int fd, unsigned int cmd, CView_Favorite_Info_Req *msg,
			const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "usercode", safestring( ( const char* ) msg->_usercode, sizeof ( msg->_usercode ) ).c_str() );
		item.SetVal( "verifyCode", safestring( ( const char* ) msg->_verifyCode, sizeof ( msg->_verifyCode ) ).c_str() );

		item.SetVal( "_storeCode", ( const char * ) msg->_storeCode ); //门店编码
		item.SetVal( "discountCode",
				safestring( ( const char* ) msg->_discountCode, sizeof ( msg->_discountCode ) ).c_str() ); //优惠信息编码

		item.SetVal( "type", chartodecstr( msg->_type ).c_str() ); //收藏类型

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpViewFavoriteInfoReq", item );
	}
	//添加收藏请求
	bool CSrvCaller::getUpAddFavoriteReq( unsigned int fd, unsigned int cmd, CAdd_Favorite_Req *msg, const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "usercode", safestring( ( const char* ) msg->_usercode, sizeof ( msg->_usercode ) ).c_str() );
		item.SetVal( "verifyCode", safestring( ( const char* ) msg->_verifyCode, sizeof ( msg->_verifyCode ) ).c_str() );

		item.SetVal( "fauoriteCode",
				safestring( ( const char* ) msg->_fauoriteCode, sizeof ( msg->_fauoriteCode ) ).c_str() ); //门店编码(或优惠信息编码)

		item.SetVal( "type", chartodecstr( msg->_type ).c_str() ); //收藏类型

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpAddFavoriteReq", item );
	}
	//删除收藏请求
	bool CSrvCaller::getUpDelFavoriteReq( unsigned int fd, unsigned int cmd, CDel_Favorite_Req *msg, const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "usercode", safestring( ( const char* ) msg->_usercode, sizeof ( msg->_usercode ) ).c_str() );
		item.SetVal( "verifyCode", safestring( ( const char* ) msg->_verifyCode, sizeof ( msg->_verifyCode ) ).c_str() );

		item.SetVal( "id", safestring( ( const char* ) msg->_id, sizeof ( msg->_id ) ).c_str() ); //收藏id

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpDelFavoriteReq", item );
	}
	//获取目的地
	bool CSrvCaller::getUpGetDestinationReq( unsigned int fd, unsigned int cmd,CGet_Destination_Req *msg, const char *id )
	{
		unsigned int seq = _httpcaller.GetSequeue();
		_msgqueue.AddObj( seq, fd, cmd, id, msg );

		CKeyValue item;
		item.SetVal( "seq", uitodecstr( msg->_header._seq ).c_str() );
		item.SetVal( "macid", id );

		item.SetVal( "usercode", safestring( ( const char* ) msg->_usercode, sizeof ( msg->_usercode ) ).c_str() );
		item.SetVal( "verifyCode", safestring( ( const char* ) msg->_verifyCode, sizeof ( msg->_verifyCode ) ).c_str() );

		return ProcessMsg( msg->_header._type, seq, "TruckService", "getUpGetDestinationReq", item );
	}
	//  添加各个服务的处理方法，这里设计为一个服务对应一个方法 , %d
	bool CSrvCaller::ProcessMsg( unsigned int msgid, unsigned int seq, const char *service, const char *method,
			CKeyValue &item )
	{
		if ( msgid == 0 ) return false;

		char key[256] = { 0 };
		sprintf( key, "%d", seq );
		// 添加映射表中
		_seq2msgid.AddSeqMsg( seq, msgid );

		CQString sXml;
		// 创建XML请求
		if ( ! CreateRequestXml( sXml, key, service, method, item ) ) {
			OUT_ERROR( NULL, 0, "ProcMsg", "create request xml failed, msg id %x, seq id %d", msgid, seq );
			ProcessError( seq, true );
			return false;
		}
		OUT_INFO( NULL, 0, "Caller", "Request msg id %x,seqid %d,service %s,method %s", msgid, seq, service, method );
		// 调用XML处理
		if ( ! _httpcaller.Request( seq, ( const char * ) _callUrl, sXml.GetBuffer(), sXml.GetLength() ) ) {
			OUT_ERROR( NULL, 0, "ProcMsg", "caller http request failed , msg id %x, seq id %d", msgid, seq );
			ProcessError( seq, true );
			return false;
		}
		return true;
	}

	// 本地编码转成UTF-8
	static bool locale2utf8( const char *szdata, const int nlen, CQString &out )
	{
		int len = nlen * 4 + 1;
		char *buf = new char[len];
		memset( buf, 0, len );

		if ( g2u( ( char * ) szdata, nlen, buf, len ) == - 1 ) {
			OUT_ERROR( NULL, 0, "auth", "locale2utf8 query %s failed", szdata );
			delete[] buf;
			return false;
		}
		buf[len] = 0;
		out.SetString( buf );
		delete[] buf;

		return true;
	}
	// 创建XML数据的处理
	bool CSrvCaller::CreateRequestXml( CQString &sXml, const char *id, const char *service, const char *method,
			CKeyValue &item )
	{
		CXmlBuilder XmlOb( "Request", "Param", "Item" );

		XmlOb.SetRootAttribute( "id", id );
		XmlOb.SetRootAttribute( "service", service );
		XmlOb.SetRootAttribute( "method", method );

		CQString stemp;
		// 取得是否有请求参数
		int count = item.GetSize();
		if ( count > 0 ) {
			for ( int i = 0 ; i < count ; ++ i ) {
				_KeyValue &vk = item.GetVal( i );
				// 如果数据为空的话就不添加处理了
				if ( vk.key.empty() || vk.val.empty() ) {
					continue;
				}
				// 转成UTF-8的编码处理
				if ( ! locale2utf8( vk.val.c_str(), vk.val.length(), stemp ) ) {
					// 如果数据为空则不传
					continue;
				}
				// 处理数据
				XmlOb.SetItemAttribute( vk.key.c_str(), stemp.GetBuffer() );
			}
			XmlOb.InsertItem();
		}
		// 处理XML数据
		XmlOb.GetXmlText( sXml );
		// 处理的XML的数据
		OUT_PRINT( NULL, 0, "XML", "Request GBK XML:%s", sXml.GetBuffer() );

		return ( ! sXml.IsEmpty() );
	}

	// 处理HttpCaller回调
	void CSrvCaller::ProcessResp( unsigned int seqid, const char *xml, const int len, const int err )
	{
		// 解析对应的XML结果处理
		if ( xml == NULL || err != HTTP_CALL_SUCCESS || len == 0 ) {
			OUT_ERROR( NULL, 0, "Caller", "Process seqid %u, error %d, %s:%d", seqid, err, __FILE__, __LINE__ );
			ProcessError( seqid, true );
			return;
		}

		// 如果数据格式不正确
		if ( strstr( xml, "Response" ) == NULL || strstr( xml, "Result" ) == NULL || strstr( xml, "Item" ) == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "ProcessResponse seqid %d , data error, xml %s", seqid, xml );
			ProcessError( seqid, true );
			return;
		}

		// 取得对应序号ID
		unsigned int msgid = 0;
		if ( ! _seq2msgid.GetSeqMsg( seqid, msgid ) ) {
			OUT_ERROR( NULL, 0, "Caller", "ProcessResponse seqid %d , msg id empty", seqid );
			ProcessError( seqid, false );
			return;
		}

		ServiceTable::iterator it = _srv_table.find( msgid );
		// 处理消息对应ID回调方法
		if ( it == _srv_table.end() ) {
			OUT_ERROR( NULL, 0, "Caller", "ProcessResponse seqid %d , call method empty", seqid );
			ProcessError( seqid, false );
			return;
		}

		// 处理XML的数据
		OUT_PRINT( NULL, 0, NULL, "xml:%s", xml );
		// 调用处理方法
		if ( ! ( this->* ( it->second ) )( seqid, xml ) ) {
			// 如果数据不正确
			ProcessError( seqid, false );
			return;
		}
	}

	//查询打折优惠信息
	bool CSrvCaller::Proc_UPDISCOUNT_INFO_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CQueryDiscountInfoReq *req = ( CQueryDiscountInfoReq * ) obj->_msg;
		CQueryDiscountInfoRsp rsp( req->_header._seq );

		rsp._num    = parser.GetInteger( "Response::Result::Items:num", 0 );
		rsp._action = parser.GetInteger( "Response::Result::Items:action", 0 );

		if ( rsp._num > 0 ) {

			for ( int i = 0 ; i < rsp._num ; i ++ ) {
				CDiscount_List *info = new CDiscount_List;
				safe_memncpy( ( char* ) info->_discount_num,
						parser.GetString( "Response::Result::Items::Item:discount_num", i ),
						sizeof ( info->_discount_num ) );
				info->_type = parser.GetInteger( "Response::Result::Items::Item:type", i );
				info->_title = parser.GetString( "Response::Result::Items::Item:title", i );
				utc2bcd( parser.GetString( "Response::Result::Items::Item:time", i ), ( char* ) info->_time );

				info->AddRef();
				rsp._vec.push_back( info );
			}
		}

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPDISCOUNT_INFO_REQ num %d", rsp._num );

		_msgqueue.FreeObj( obj );

		return true;
	}
	//查询具体打折优惠信息
	bool CSrvCaller::Proc_UPDETAIL_DISCOUNT_INFO_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CQueryDetailiscountInfoReq *req = (CQueryDetailiscountInfoReq * ) obj->_msg;
		CQueryDetailiscountInfoRsp rsp( req->_header._seq );

		safe_memncpy( ( char* ) rsp._discount_num, parser.GetString( "Response::Result::Item:discount_num", 0 ),
				sizeof ( rsp._discount_num ) );

		safe_memncpy( ( char* ) rsp._business_num, parser.GetString( "Response::Result::Item:business_num", 0 ),
				sizeof ( rsp._business_num ) );

		rsp._title = parser.GetString( "Response::Result::Item:title", 0 );

		utc2bcd( parser.GetString( "Response::Result::Item:time", 0 ), ( char* ) rsp._time );

		rsp._name = parser.GetString( "Response::Result::Item:name", 0 );
		rsp._address = parser.GetString( "Response::Result::Item:address", 0 );

		strtoBCD( parser.GetString( "Response::Result::Item:phone", 0 ),( char* ) rsp._phone);

		rsp._service_level = parser.GetInteger( "Response::Result::Item:service_level", 0 );
		rsp._detail = parser.GetString( "Response::Result::Item:detail", 0 );

		rsp._lon = parser.GetInteger( "Response::Result::Item:lon", 0 );
		rsp._lat = parser.GetInteger( "Response::Result::Item:lat", 0 );

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPDETAIL_DISCOUNT_INFO_REQ title %s", rsp._title.GetBuffer() );

		_msgqueue.FreeObj( obj );

		return true;
	}

	//查询联盟商家信息
	bool CSrvCaller::Proc_UPUNION_BUSINESS_INFO_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CQueryUnionBusinessInfoReq *req = (CQueryUnionBusinessInfoReq * ) obj->_msg;
		CQueryUnionBusinessInfoRsp rsp( req->_header._seq );

		rsp._num    = parser.GetInteger( "Response::Result::Items:num", 0 );
		rsp._action = parser.GetInteger( "Response::Result::Items:action", 0 );

		if ( rsp._num > 0 ) {

			for ( int i = 0 ; i < rsp._num ; i ++ ) {
				CBusiness_List *info = new CBusiness_List;

				safe_memncpy( ( char* ) info->_business_num,
						parser.GetString( "Response::Result::Items::Item:business_num", i ),
						sizeof ( info->_business_num ) );

				info->_name = parser.GetString( "Response::Result::Items::Item:name", i );
				info->_type = parser.GetInteger( "Response::Result::Items::Item:type", i );
				info->_service_level = parser.GetInteger( "Response::Result::Items::Item:service_level", i );
				info->_lon = parser.GetInteger( "Response::Result::Items::Item:lon", i );
				info->_lat = parser.GetInteger( "Response::Result::Items::Item:lat", i );

				info->AddRef();
				rsp._vec.push_back( info );
			}
		}

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPUNION_BUSINESS_INFO_REQ num %d", rsp._num );

		_msgqueue.FreeObj( obj );

		return true;
	}

	//查询具体联盟商家信息
	bool CSrvCaller::Proc_UPDETAIL_UNION_BUSINESS_INFO_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CQueryDetailUnionBusinessInfoReq *req = (CQueryDetailUnionBusinessInfoReq * ) obj->_msg;
		CQueryDetailUnionBusinessInfoRsp rsp( req->_header._seq );

		safe_memncpy( ( char* ) rsp._business_num, parser.GetString( "Response::Result::Item:business_num", 0 ),
				sizeof ( rsp._business_num ) );

		rsp._name = parser.GetString( "Response::Result::Item:name", 0 );

		rsp._service_level = parser.GetInteger( "Response::Result::Item:service_level", 0 );
		rsp._address = parser.GetString( "Response::Result::Item:address", 0);

		strtoBCD(parser.GetString("Response::Result::Item:phone", 0),(char*)rsp._phone);

		rsp._detail = parser.GetString( "Response::Result::Item:detail", 0 );

		rsp._lon = parser.GetInteger( "Response::Result::Item:lon", 0 );
		rsp._lat = parser.GetInteger( "Response::Result::Item:lat", 0 );

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPDETAIL_UNION_BUSINESS_INFO_REQ name %s", rsp._name.GetBuffer() );

		_msgqueue.FreeObj(obj);

		return true;
	}
	//用户登录
	bool CSrvCaller::Proc_UPLOGIN_INFO_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CLoginInfoReq *req = (CLoginInfoReq * ) obj->_msg;
		CLoginInfoRsp rsp( req->_header._seq );

		safe_memncpy( ( char* ) rsp._usercode, parser.GetString( "Response::Result::Item:usercode", 0 ),
				sizeof ( rsp._usercode ) );

		safe_memncpy( ( char* ) rsp._verifyCode, parser.GetString( "Response::Result::Item:verifyCode", 0 ),
				sizeof ( rsp._verifyCode ) );

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPLOGIN_INFO_REQ name %s", rsp._usercode );

		_msgqueue.FreeObj( obj );

		return true;
	}

	//联名卡余额查询
	bool CSrvCaller::Proc_UPQUERY_BALLANCE_LIST_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CQuery_Ballance_List_Req *req = (CQuery_Ballance_List_Req * ) obj->_msg;
		CQuery_Ballance_List_Rsp rsp( req->_header._seq );

		safe_memncpy( ( char* ) rsp._car_num, parser.GetString( "Response::Result::Item:car_num", 0 ),
				sizeof ( rsp._car_num ) );

		safe_memncpy( ( char* ) rsp._vehicle_num, parser.GetString( "Response::Result::Item:vehicle_num", 0 ),
				sizeof ( rsp._vehicle_num ) );

		rsp._balance = parser.GetInteger( "Response::Result::Item:balance", 0 );
		rsp._status = parser.GetInteger( "Response::Result::Item:status", 0 );

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPQUERY_BALLANCE_LIST_REQ name %s", rsp._car_num );

		_msgqueue.FreeObj( obj );
		return true;
	}

	//查询门店
	bool CSrvCaller::Proc_UPQUERY_STORE_LIST_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CQuery_Store_List_Req *req = (CQuery_Store_List_Req * ) obj->_msg;
		CQuery_Store_List_Rsp rsp( req->_header._seq );

		rsp._num = parser.GetInteger( "Response::Result::Items:num", 0 );

		if ( rsp._num > 0 ) {
			for ( int i = 0 ; i < rsp._num ; i ++ ) {
				CStore_List *info = new CStore_List;
				info->_type = parser.GetInteger( "Response::Result::Items::Item:type", i );

				safe_memncpy( ( char* ) info->_storeCode, parser.GetString( "Response::Result::Items::Item:storeCode", i ),
						sizeof ( info->_storeCode ) );

				info->_storeName = parser.GetString( "Response::Result::Items::Item:storeName", i );
				info->_address = parser.GetString( "Response::Result::Items::Item:address", i );

				safe_memncpy( ( char* ) info->_phone, parser.GetString( "Response::Result::Items::Item:phone", i ),
						sizeof ( info->_phone ) );

				info->_lon = parser.GetInteger( "Response::Result::Items::Item:lon", i );
				info->_lat = parser.GetInteger( "Response::Result::Items::Item:lat", i );

				info->AddRef();
				rsp._vec.push_back( info );
			}
		}

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );
		OUT_INFO( NULL, 0, "Caller", "Proc_UPQUERY_STORE_LIST_REQ num %d", rsp._num );
		_msgqueue.FreeObj( obj );
		return true;
	}

	//查询门店详情
	bool CSrvCaller::Proc_UPQUERY_VIEW_STORE_INFO_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CView_Store_Info_Req *req = (CView_Store_Info_Req * ) obj->_msg;
		CView_Store_List_Rsp rsp( req->_header._seq );

		rsp._allianceName = parser.GetString( "Response::Result::Item:allianceName", 0 );

		rsp._type = ParseParamOfType( parser.GetString( "Response::Result::Item:type", 0 ) );

		safe_memncpy( ( char* ) rsp._storeCode, parser.GetString( "Response::Result::Item:storeCode", 0 ),
				sizeof ( rsp._storeCode ) );

		rsp._storeName = parser.GetString( "Response::Result::Item:storeName", 0 );
		rsp._address = parser.GetString( "Response::Result::Item:address", 0 );

		safe_memncpy( ( char* ) rsp._phone, parser.GetString( "Response::Result::Item:phone", 0 ), sizeof ( rsp._phone ) );

		rsp._desc = parser.GetString( "Response::Result::Item:desc", 0 );
		rsp._lon = parser.GetInteger( "Response::Result::Item:lon", 0 );
		rsp._lat = parser.GetInteger( "Response::Result::Item:lat", 0 );

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPQUERY_VIEW_STORE_INFO_REQ allianceName %s", rsp._allianceName.GetBuffer() );

		_msgqueue.FreeObj( obj );
		return true;
	}

	//新版本查询优惠信息
	bool CSrvCaller::Proc_UPQUERY_DISCOUNT_LIST_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CQuery_Discount_List_Req *req = (CQuery_Discount_List_Req * ) obj->_msg;
		CQuery_Discount_List_Rsp rsp( req->_header._seq );

		rsp._num = parser.GetInteger( "Response::Result::Items:num", 0 );

		int nOffset = 0;
		if ( rsp._num > 0 ) {
			for ( int i = 0 ; i < rsp._num ; ++ i ) {
				CDiscount_Store_List_Info *info = new CDiscount_Store_List_Info;
				safe_memncpy( ( char* ) info->_storeCode, parser.GetString( "Response::Result::Items::Item:storeCode", i ),
						sizeof ( info->_storeCode ) );

				info->_storeName = parser.GetString( "Response::Result::Items::Item:storeName", i );
				safe_memncpy( ( char* ) info->_phone, parser.GetString( "Response::Result::Items::Item:phone", i ),
						sizeof ( info->_phone ) );
				info->_address = parser.GetString( "Response::Result::Items::Item:address", i );
				info->_lon = parser.GetInteger( "Response::Result::Items::Item:lon", i );
				info->_lat = parser.GetInteger( "Response::Result::Items::Item:lat", i );
				info->_discount_num = parser.GetInteger( "Response::Result::Items::Item:discount_num", i );

				if ( info->_discount_num > 0 ) {
					for ( int j = 0 ; j < info->_discount_num ; j ++ ) {
						CDiscount_List_Info *child_info = new CDiscount_List_Info;

						safe_memncpy( ( char* ) child_info->_discountCode,
								parser.GetString( "Response::Result::Items::Item::ChileItem:discountCode", nOffset + j ),
								sizeof ( child_info->_discountCode ) );

						child_info->_title = parser.GetString( "Response::Result::Items::Item::ChileItem:title",
								nOffset + j );

						child_info->AddRef();
						info->_vec_discount.push_back( child_info );
					}
					nOffset += info->_discount_num;
				}
				info->AddRef();
				rsp._vec.push_back( info );
			}
		}
		DeliverPacket( obj->_fd, obj->_cmd, & rsp );
		OUT_INFO( NULL, 0, "Caller", "Proc_UPQUERY_DISCOUNT_LIST_REQ num %d", rsp._num );

		_msgqueue.FreeObj( obj );

		return true;
	}

	//新版本查询优惠信息详细列表
	bool CSrvCaller::Proc_UPVIEW_DISCOUNT_INFO_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CView_Discount_Info_Req *req = (CView_Discount_Info_Req * ) obj->_msg;
		CView_Discount_Info_Rsp rsp( req->_header._seq );

		rsp._title = parser.GetString( "Response::Result::Item:title", 0 );
		rsp._content = parser.GetString( "Response::Result::Item:content", 0 );

		utc2bcd( parser.GetString( "Response::Result::Item:beginTime", 0 ), ( char* ) rsp._beginTime );
		utc2bcd( parser.GetString( "Response::Result::Item:endTime", 0 ), ( char* ) rsp._endTime );

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPVIEW_DISCOUNT_INFO_REQ title %s", rsp._title.GetBuffer() );
		_msgqueue.FreeObj( obj );
		return true;
	}
	//历史交易记录查询
	bool CSrvCaller::Proc_UPQUERY_TRADE_LIST_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CQuery_Trade_List_Req *req = (CQuery_Trade_List_Req * ) obj->_msg;
		CQuery_Trade_List_Rsp rsp( req->_header._seq );

		rsp._num = parser.GetInteger( "Response::Result::Items:num", 0 );

		if ( rsp._num > 0 ) {

			for ( int i = 0 ; i < rsp._num ; i ++ ) {
				CTrade_List *info = new CTrade_List;

				safe_memncpy( ( char* ) info->_card_num, parser.GetString( "Response::Result::Items::Item:card_num", i ),
						sizeof ( info->_card_num ) );

				info->_allianceName = parser.GetString( "Response::Result::Items::Item:allianceName", i );
				info->_storeName = parser.GetString( "Response::Result::Items::Item:storeName", i );
				info->_type = parser.GetInteger( "Response::Result::Items::Item:type", i );
				info->_productName = parser.GetString( "Response::Result::Items::Item:productName", i );
				info->_money = parser.GetInteger( "Response::Result::Items::Item:money", i );

				utc2bcd( parser.GetString( "Response::Result::Items::Item:trade_Time", 0 ), ( char* ) info->_tradeTime );

				info->_tradeState = parser.GetInteger( "Response::Result::Items::Item:tradeState", i );
				info->AddRef();
				rsp._vec.push_back( info );
			}
		}

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPQUERY_TRADE_LIST_REQ num %d", rsp._num );

		_msgqueue.FreeObj( obj );

		return true;
	}
	//查询收藏列表
	bool CSrvCaller::Proc_UPQUERY_FAVORITE_LIST_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CQuery_Favorite_List_Req *req = (CQuery_Favorite_List_Req * ) obj->_msg;
		CQuery_Favorite_List_Rsp rsp( req->_header._seq );

		rsp._num = parser.GetInteger( "Response::Result::Items:num", 0 );
		if ( rsp._num > 0 ) {
			for ( int i = 0 ; i < rsp._num ; i ++ ) {
				CFavorite_List *info = new CFavorite_List;

				safe_memncpy( ( char* ) info->_id, parser.GetString( "Response::Result::Items::Item:id", i ),
						sizeof ( info->_id ) );

				info->_storeCode = parser.GetString( "Response::Result::Items::Item:storeCode", i );

				safe_memncpy( ( char* ) info->_discountCode,
						parser.GetString( "Response::Result::Items::Item:discountCode", i ),
						sizeof ( info->_discountCode ) );

				info->_favoriteName = parser.GetString( "Response::Result::Items::Item:favoriteName", i );
				info->_type = parser.GetInteger( "Response::Result::Items::Item:type", i );
				info->_isDiscount = parser.GetInteger( "Response::Result::Items::Item:isDiscount", i );
				info->_discountState = parser.GetInteger( "Response::Result::Items::Item:discountState", i );

				info->AddRef();
				rsp._vec.push_back( info );
			}
		}

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPQUERY_TRADE_LIST_REQ num %d", rsp._num );

		_msgqueue.FreeObj( obj );

		return true;
	}
	//查询收藏列表详情
	bool CSrvCaller::Proc_UPVIEW_FAVORITE_INFO_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CView_Favorite_Info_Req *req = (CView_Favorite_Info_Req * ) obj->_msg;
		CView_Favorite_Info_Rsp rsp( req->_header._seq );

		rsp._allianceName = parser.GetString( "Response::Result::Items:allianceName", 0 );
		rsp._allianceType = ParseParamOfType(parser.GetString( "Response::Result::Items:allianceType", 0 ));

		safe_memncpy( ( char* ) rsp._storeCode, parser.GetString( "Response::Result::Items:storeCode", 0 ),
				sizeof ( rsp._storeCode ) );

		rsp._storeName = parser.GetString( "Response::Result::Items:storeName", 0 );

		safe_memncpy( ( char* ) rsp._phone, parser.GetString( "Response::Result::Items:phone", 0 ), sizeof ( rsp._phone ) );

		rsp._address = parser.GetString( "Response::Result::Items:address", 0 );
		rsp._desc = parser.GetString( "Response::Result::Items:desc", 0 );

		rsp._lon = parser.GetInteger( "Response::Result::Items:lon", 0 );
		rsp._lat = parser.GetInteger( "Response::Result::Items:lat", 0 );

		rsp._num = parser.GetInteger( "Response::Result::Items:num", 0 );

		if ( rsp._num > 0 ) {

			for ( int i = 0 ; i < rsp._num ; i ++ ) {
				CDiscount_List_New *info = new CDiscount_List_New;

				safe_memncpy( ( char* ) info->_discountCode,
						parser.GetString( "Response::Result::Items::Item:discountCode", i ),
						sizeof ( info->_discountCode ) );

				info->_title = parser.GetString( "Response::Result::Items::Item:title", i );
				info->_content = parser.GetString( "Response::Result::Items::Item:content", i );

				utc2bcd( parser.GetString( "Response::Result::Items::Item:beginTime", i ), ( char* ) info->_begin_time );
				utc2bcd( parser.GetString( "Response::Result::Items::Item:endTime", i ), ( char* ) info->_end_time );

				info->AddRef();
				rsp._vec.push_back( info );
			}
		}

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPVIEW_FAVORITE_INFO_REQ num %d", rsp._num );

		_msgqueue.FreeObj( obj );

		return true;
	}
	//添加收藏请求
	bool CSrvCaller::Proc_UPADD_FAVORITE_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CAdd_Favorite_Req *req = (CAdd_Favorite_Req * ) obj->_msg;
		CPlatFormCommonRsp rsp( req->_header._seq );

		rsp._type = UP_ADD_FAVORITE_REQ;
		rsp._result = parser.GetInteger( "Response::Result::Item:result", 0 );

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPADD_FAVORITE_REQ num %02x", rsp._type );

		_msgqueue.FreeObj( obj );

		return true;
	}
	//删除收藏请求
	bool CSrvCaller::Proc_UPDEL_FAVORITE_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CDel_Favorite_Req *req = (CDel_Favorite_Req * ) obj->_msg;
		CPlatFormCommonRsp rsp( req->_header._seq );

		rsp._type = UP_DEL_FAVORITE_REQ;
		rsp._result = parser.GetInteger( "Response::Result::Item:result", 0 );

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPDEL_FAVORITE_REQ num %02x", rsp._type );

		_msgqueue.FreeObj( obj );

		return true;
	}
	//获取目的地
	bool CSrvCaller::Proc_UPGET_DESTINATION_REQ( unsigned int seqid, const char *xml )
	{
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );

		if ( obj == NULL ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d is time out remove it", seqid );
			return false;
		}

		CXmlParser parser;

		// 加载XML的数据
		if ( ! parser.LoadXml( xml ) ) {
			OUT_ERROR( NULL, 0, "Caller", "seqid %d parser xml error, xml:%d", seqid, xml );
			_msgqueue.FreeObj( obj );
			return false;
		}

		CDel_Favorite_Req *req = (CDel_Favorite_Req * ) obj->_msg;
		CPlatFormCommonRsp rsp( req->_header._seq );

		rsp._type = UP_GET_DESTINATION_REQ;
		rsp._result = parser.GetInteger( "Response::Result::Item:result", 0 );

		DeliverPacket( obj->_fd, obj->_cmd, & rsp );

		OUT_INFO( NULL, 0, "Caller", "Proc_UPGET_DESTINATION_REQ num %02x", rsp._type );

		_msgqueue.FreeObj( obj );

		return true;
	}
	// 发送数据
	void CSrvCaller::DeliverPacket( unsigned int fd, unsigned int cmd, IPacket *msg )
	{
		CPacker pack;
		_packfactory->Pack( msg, pack);

		_pEnv->OnDeliver( fd, pack.getBuffer(), pack.getLength(), cmd );
	}

	// 处理请求超时的请求
	void CSrvCaller::OnTimeOut( unsigned int seq, unsigned int fd, unsigned int cmd, const char *id, IPacket *msg )
	{
		// ToDo:  超时处理
		OUT_INFO( NULL, 0, id, "seq id %d, fd %d, cmd %04x msg_type %04x time out", seq, fd, cmd, msg->_header._type );
	}

	// 线程执行对象
	void CSrvCaller::run( void *param )
	{
		while ( _inited ) {
			// 检测超时消息
			_msgqueue.Check( 120 );
			_resultmgr.Check( 300 );

			share::Synchronized syn( _monitor );
			{
				_monitor.wait( 30 );
			}
		}
	}

	// 处理错误的情况
	void CSrvCaller::ProcessError( unsigned int seqid, bool remove )
	{
		// 清理回调TABLE中SEQ对应MAP
		if ( remove ) _seq2msgid.RemoveSeq( seqid );

		// 添加队列中处理
		CMsgQueue::_MsgObj *obj = _msgqueue.GetObj( seqid );
		if ( obj == NULL ) {
			return;
		}

		IPacket * req = obj->_msg;
		// 处理发送数据超时或者错误的情况

		switch ( req->_header._type )
		{
		case UP_LOGIN_INFO_REQ:
		case UP_QUERY_BALLANCE_LIST_REQ:
		case UP_QUERY_STORE_LIST_REQ:
		case UP_QUERY_VIEW_STORE_INFO_REQ:
		case UP_QUERY_DISCOUNT_LIST_REQ:
		case UP_VIEW_DISCOUNT_INFO_REQ:
		case UP_QUERY_TRADE_LIST_REQ:
		case UP_QUERY_FAVORITE_LIST_REQ:
		case UP_VIEW_FAVORITE_INFO_REQ:
		case UP_ADD_FAVORITE_REQ:
		case UP_DEL_FAVORITE_REQ:
		case UP_GET_DESTINATION_REQ:
		case UP_DISCOUNT_INFO_REQ:
		case UP_DETAIL_DISCOUNT_INFO_REQ:
		case UP_UNION_BUSINESS_INFO_REQ:
		case UP_DETAIL_UNION_BUSINESS_INFO_REQ:
		{
			CPlatFormCommonRsp rsp( req->_header._seq );
			rsp._result = 0x01;
			rsp._type = req->_header._type;
			DeliverPacket( obj->_fd, obj->_cmd, & rsp );
		}
			break;
		default:
			break;
		}
		_msgqueue.FreeObj( obj );
	}
} ;
