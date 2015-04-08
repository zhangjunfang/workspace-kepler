/*
 * truck.cpp
 *
 *  Created on: 2012-5-31
 *      Author: humingqing
 */

#include <stdio.h>
#include <truck.h>
#include <comlog.h>
#include "packfactory.h"

namespace CarService {

	CCarService::CCarService(): _pCaller(NULL)
	{
		_packfactory = new CPackFactory( &_unpacker ) ;
		_srvCaller   = new CSrvCaller ;
	}

	CCarService::~CCarService()
	{
		Stop() ;

		if ( _srvCaller != NULL ){
			delete _srvCaller ;
			_srvCaller = NULL ;
		}
	}

	// 需要初始化对象
	bool CCarService::Init( IPlugin *plug , const char *url, int sendthread , int recvthread , int queuesize )
	{
		_pCaller = plug ;

		if ( ! _srvCaller->Init( plug , url, sendthread, recvthread , queuesize ) ) {
			OUT_ERROR( NULL, 0, "CCarService", "init service caller http failed" ) ;
			return false ;
		}
		return true ;
	}

	// 初始化插件通道
	bool CCarService::Start( void )
	{
		if ( ! _srvCaller->Start() ){
			OUT_ERROR( NULL, 0, "CCarService", "start service caller http failed" ) ;
			return false ;
		}
		return true ;
	}

	// 停止插件通道
	bool CCarService::Stop( void )
	{
		_srvCaller->Stop() ;
		return true ;
	}

	// 处理透传的数据
	bool CCarService::Process( unsigned int fd, const char *data, int len , unsigned int cmd , const char *id )
	{
		// printf( "recv data length: %d\n" , len ) ;
		IPacket *msg = _packfactory->UnPack( data, len ) ;
		// 拆分消息头部
		if ( msg == NULL) {
			OUT_ERROR( NULL, 0, id , "Truck process data length error, len %d" , len ) ;
			return false ;
		}

		// 自动释放对象
		CAutoRelease autoRef(msg);

		// 解析数据类型
		unsigned short msg_type = msg->_header._type;
		// 接收着数据
		OUT_RECV( NULL, 0, id, "Truck process recv fd %d, msg type %4x" , fd, msg_type ) ;
		// 解析对应的协议
		switch( msg_type )
		{
		// 车后业务
		case UP_DISCOUNT_INFO_REQ:// 0x1029 查询打折优惠信息
			_srvCaller->getUpQueryDiscountInfoReq( fd, cmd, (CQueryDiscountInfoReq*) msg , id);
			break;
		case UP_DETAIL_DISCOUNT_INFO_REQ: //0x102A 查询具体打折优惠信息
			_srvCaller->getUpQueryDetailDiscountInfoReq( fd, cmd, (CQueryDetailiscountInfoReq*) msg , id);
			break;
		case UP_UNION_BUSINESS_INFO_REQ: //0x102C 查询联盟商家信息
			_srvCaller->getUpQueryUnionBusinessInfoReq( fd, cmd, (CQueryUnionBusinessInfoReq*) msg , id);
			break;
		case UP_DETAIL_UNION_BUSINESS_INFO_REQ: //0x102D 查询联盟具体商家信息
			_srvCaller->getUpQueryDetailUnionBusinessInfoReq( fd, cmd, (CQueryDetailUnionBusinessInfoReq*) msg , id);
			break;
		case TERMINAL_COMMON_RSP://终端通用应答
			_srvCaller->putTerminalCommonRsp( fd, cmd, (CTerminalCommonRsp*) msg , id);
			break;
		case UP_LOGIN_INFO_REQ://0x1001 用户登录
			_srvCaller->getUpLoginInfoReq( fd, cmd, (CLoginInfoReq*) msg , id);
			break;
		case UP_QUERY_BALLANCE_LIST_REQ://0x1002 联名卡余额查询
			_srvCaller->getUpQueryBallanceListReq( fd, cmd,(CQuery_Ballance_List_Req*)msg,id);
			break;
		case UP_QUERY_STORE_LIST_REQ://0x1003 查询门店
			_srvCaller->getUpQueryStoreListReq( fd, cmd,(CQuery_Store_List_Req*)msg,id);
			break;
		case UP_QUERY_VIEW_STORE_INFO_REQ://0x1004  查询门店详情
			_srvCaller->getUpQueryViewStoreInfoReq( fd, cmd,(CView_Store_Info_Req*)msg,id);
			 break;
		case UP_QUERY_DISCOUNT_LIST_REQ://0x1005  新版本查询优惠信息
			_srvCaller->getUpQueryDiscountListReq( fd, cmd,(CQuery_Discount_List_Req*)msg,id);
		   break;
		case UP_VIEW_DISCOUNT_INFO_REQ://0x1006  新版本查询优惠信息详细列表
			_srvCaller->getUpViewDiscountInfoReq( fd, cmd,(CView_Discount_Info_Req*)msg,id);
		   break;
		case UP_QUERY_TRADE_LIST_REQ://0x1007 历史交易记录查询
			_srvCaller->getUpQueryTradeListReq( fd, cmd,(CQuery_Trade_List_Req*)msg,id);
			break;
		case UP_QUERY_FAVORITE_LIST_REQ://0x1008 查询收藏列表
			_srvCaller->getUpQueryFavoriteListReq(fd, cmd,(CQuery_Favorite_List_Req*)msg,id);
			break;
		case UP_VIEW_FAVORITE_INFO_REQ://0x1009 查询收藏列表详情
			_srvCaller->getUpViewFavoriteInfoReq(fd, cmd,(CView_Favorite_Info_Req*)msg,id);
			break;
		case UP_ADD_FAVORITE_REQ://0x100A 添加收藏请求
			_srvCaller->getUpAddFavoriteReq(fd, cmd,(CAdd_Favorite_Req*)msg,id);
			break;
		case UP_DEL_FAVORITE_REQ ://0x100B 删除收藏请求
			_srvCaller->getUpDelFavoriteReq(fd, cmd,(CDel_Favorite_Req*)msg,id);
			break;
		case UP_GET_DESTINATION_REQ://0x100C 获取目的地
			_srvCaller->getUpGetDestinationReq(fd, cmd,(CGet_Destination_Req*)msg,id);
			break;
		default:
			break;
		}

		// 释放数据
		return true;
	}
} ;
//====================================== 动态库动态加载函数 ================================================
extern "C" IPlugWay* GetPlugObject( void )
{
	return new CarService::CCarService ;
}
//----------------------------------------------------------------------------------
extern "C" void FreePlugObject( IPlugWay* p )
{
	if ( p != NULL )
		delete p ;
}
//----------------------------------------------------------------------------------
