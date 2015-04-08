/*
 * truck.cpp
 *
 *  Created on: 2012-5-31
 *      Author: humingqing
 */

#include <stdio.h>
#include <truck.h>
#include <comlog.h>

namespace TruckSrv{
	// 使用
	CTruck::CTruck(): _pCaller(NULL)
	{
		_packfactory = new CPackFactory( &_unpacker ) ;
		_srvCaller = new CSrvCaller ;
	}

	CTruck::~CTruck()
	{
		Stop() ;

		if ( _srvCaller != NULL ){
			delete _srvCaller ;
			_srvCaller = NULL ;
		}
		if ( _packfactory != NULL ) {
			delete _packfactory ;
			_packfactory = NULL ;
		}
	}

	// 需要初始化对象
	bool CTruck::Init( IPlugin *plug , const char *url, int sendthread , int recvthread , int queuesize )
	{
		_pCaller = plug ;

		if ( ! _srvCaller->Init( plug , url, sendthread, recvthread , queuesize ) ) {
			OUT_ERROR( NULL, 0, "CTruck", "init service caller http failed" ) ;
			return false ;
		}
		return true ;
	}

	// 初始化插件通道
	bool CTruck::Start( void )
	{
		if ( ! _srvCaller->Start() ){
			OUT_ERROR( NULL, 0, "CTruck", "start service caller http failed" ) ;
			return false ;
		}
		return true ;
	}

	// 停止插件通道
	bool CTruck::Stop( void )
	{
		_srvCaller->Stop() ;
		return true ;
	}

	// 处理透传的数据
	bool CTruck::Process( unsigned int fd, const char *data, int len , unsigned int cmd , const char *id )
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
		case SEND_TEAMMEDIA_REQ:// 车队聊天
			_srvCaller->getSendTeamMediaReq(fd, cmd, (CSendTeamMediaReq *)msg, id);
			 break;
		case SEND_MEDIADATA_REQ://车友聊天
			_srvCaller->getSendMediaDataReq(fd, cmd, (CSendMediaDataReq *)msg, id);
			 break;
		case INFO_PRIMCAR_REQ://接收头车的信息
			_srvCaller->getInfoPriMcarReq(fd, cmd, (CInfoPriMcarReq *)msg, id);
			 break;
		case SET_PRIMCAR_REQ://设置本车为头车
			_srvCaller->getSetPriMcarReq(fd, cmd, (CSetPriMcarReq *)msg, id);
			 break;
		case INVITE_NUMBER_REQ://车队成员邀请
			_srvCaller->getInviteNumberReq(fd, cmd, (CInviteNumberReq *)msg, id);
			break;
		case ADD_CARTEAM_REQ: //建立车队
			_srvCaller->getAddCarTeamReq(fd, cmd, (CAddCarTeamReq *)msg, id);
			 break;
		case GET_FRIENDLIST_REQ://获取车友列表
			 _srvCaller->getGetFriendlistReq(fd, cmd, (CGetFriendListReq *)msg, id);
			 break;
		case INVITE_FRIEND_REQ://邀请车友
			_srvCaller->getInviteFriendReq(fd, cmd, (CInviteFriendReq *)msg, id);
			break;
		case ADD_FRIEND_REQ://添加好友
			_srvCaller->getAddFriendsReq(fd, cmd, (CAddFriendsReq *)msg, id);
			break;
		case QUERY_FRIENDS_REQ://查找附近的好友
			_srvCaller->getQueryFriendsReq(fd, cmd, (CQueryFriendsReq *) msg , id ) ;
			break;
		case DRIVER_LOGOUT_REQ://司机注销
			_srvCaller->getDriverLoginOutReq(fd, cmd, (CDriverLoginOutReq *) msg , id ) ;
			break;
		case DRIVER_LOGIN_REQ://司机登录验证
			_srvCaller->getDriverLoginReq(fd, cmd, ( CDriverLoginReq *) msg , id ) ;
			break;
		case QUERY_CARDATA_REQ:// 查询配货信息
			_srvCaller->getQueryCarDataReq(fd, cmd, ( CQueryCarDataReq *) msg , id ) ;
			break;
		case UPLOAD_DATAINFO_REQ:// 配货状态上报
			_srvCaller->putAutoDataScheduleReq(fd,cmd,(CAutoDataScheduleReq *)msg , id );
			break;
		case SUBSCRIBE_REQ: //订阅管理
			_srvCaller->putScheduleReq(fd , cmd , (CSubscrbeReq*)msg , id ) ;
			break;
		case QUERY_INFO_REQ://订阅消息查询
			_srvCaller->getQueryInfoReq(fd, cmd, (CQueryInfoReq *) msg , id);
			break;
		case UP_REPORTERROR_REQ:  	// 上传车辆故障
			_srvCaller->putErrorScheduleReq(fd,cmd,(CErrorScheduleReq*)msg , id );
			break;
			// 下面部分为甩挂业务的处理
		case SEND_SCHEDULE_RSP:  // 处理下发调度单自动应答响应
			_srvCaller->putSendScheduleRsp( fd, cmd, (CSendScheduleRsp*) msg, id ) ;
			break ;
		case RESULT_SCHEDULE_REQ:	 	// 0x1045  下发调度单响应结果
			_srvCaller->getResultScheduleReq( fd , cmd , (CResultScheduleReq*) msg , id ) ;
			break ;
		case QUERY_SCHEDULE_REQ:	// 0x1041  查询调度单请求
			_srvCaller->getQueryScheduleReq( fd, cmd, (CQueryScheduleReq*) msg , id ) ;
			break ;
		case UPLOAD_SCHEDULE_REQ: 	// 0x1042 上传调度单
			_srvCaller->putUploadScheduleReq( fd, cmd, (CUploadScheduleReq*) msg , id ) ;
			break ;
		case STATE_SCHEDULE_REQ: 	// 0x1043  上报调度单状态
			_srvCaller->putStateScheduleReq( fd, cmd, (CStateScheduleReq *) msg , id ) ;
			break ;
		case ALARM_SCHEDULE_REQ: 	// 0x1044  车挂告警
			_srvCaller->putAlarmScheduleReq( fd, cmd, (CAlarmScheduleReq *) msg , id ) ;
			break ;
		case UP_MSGDATA_REQ:        // 0x1060  数据透传
			_srvCaller->getMsgDataScheduleReq( fd, cmd, (CUpMsgDataScheduleReq *) msg , id );
			break;
		//货运业务
		case UP_CARDATA_INFO_REQ:   //0x1023 上传配货信息
			_srvCaller->getCarDataInfoReq( fd, cmd, (CUpCarDataInfoReq *) msg , id );
			break;
		case UP_QUERY_ORDER_FORM_INFO_REQ: //0x1026  订单详细查询
			_srvCaller->getQueryOrderFormInfoReq(fd, cmd, (CQueryOrderFromInfoReq*) msg , id);
			break;
		case UP_ORDER_FORM_INFO_REQ: //0x1027 上传货运订单状态
			_srvCaller->getUpOrderFormInfoReq( fd, cmd, (CUpOrderFromInfoReq*)msg, id);
			break;
		case UP_TRANSPORT_FORM_INFO_REQ:// 0x1028 上传运单状态
			_srvCaller->getUpTransportFormInfoReq( fd, cmd, (CTransportFormInfoReq*)msg,id);
			break;
		case TERMINAL_COMMON_RSP://终端通用应答
			_srvCaller->putTerminalCommonRsp( fd, cmd, (CTerminalCommonRsp*) msg , id);
			break;
		default:
			break ;
		}

		// 释放数据
		return true;
	}
} ;
//====================================== 动态库动态加载函数 ================================================
extern "C" IPlugWay* GetPlugObject( void )
{
	return new TruckSrv::CTruck ;
}
//----------------------------------------------------------------------------------
extern "C" void FreePlugObject( IPlugWay* p )
{
	if ( p != NULL )
		delete p ;
}
//----------------------------------------------------------------------------------



