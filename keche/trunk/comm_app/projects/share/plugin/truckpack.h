/*
 * header.h
 *
 *  Created on: 2012-5-31
 *      Author: humingqing
 *  货运甩挂协议解析对象
 */

#ifndef __TRUCKPACK_H__
#define __TRUCKPACK_H__

#include <msgpack.h>
#include <packfactory.h>

#pragma pack(1)

namespace TruckSrv{
	// 物流与甩挂
	#define DRIVER_LOGIN_REQ  		0x1001   // 司机身份登陆
	#define DRIVER_LOGIN_RSP  		0x8001
	#define DRIVER_LOGOUT_REQ		0x1002   // 司机身份注销
	#define DRIVER_LOGOUT_RSP		0x8002	 // 司机身份响应
	#define QUERY_FRIENDS_REQ		0x1010	 // 查找附近的好友
	#define QUERY_FRIENDS_RSP		0x8010   // 查找好友响应
	#define ADD_FRIEND_REQ			0x1011   // 添加好友
	#define ADD_FRIEND_RSP          0x8011   // 添加好友响应
	#define INVITE_FRIEND_REQ 		0x1012	 // 邀请好友
	#define INVITE_FRIEND_RSP       0x8012   // 邀请好友响应
	#define GET_FRIENDLIST_REQ		0x1013   // 获取车友列表
	#define GET_FRIENDLIST_RSP      0x8013   // 获取车友列表响应

	#define ADD_CARTEAM_REQ         0x1014   //建立车队
	#define ADD_CARTEAM_RSP         0x8014   //建立车队响应

	#define INVITE_NUMBER_REQ       0x1015   //车队成员邀请
	#define INVITE_NUMBER_RSP       0x8015   //车队成员邀请响应

	#define SET_PRIMCAR_REQ         0x1016    //设置本车为头车
	#define SET_PRIMCAR_RSP         0x8016    //设置本车为头车响应

	#define INFO_PRIMCAR_REQ        0x1017    //接收头车的信息
	#define INFO_PRIMCAR_RSP        0x8017    //接收头车的信息响应

	#define SEND_MEDIADATA_REQ      0x1018    //车友聊天
	#define SEND_MEDIADATA_RSP      0x8018    //车友聊天响应

	#define SEND_TEAMMEDIA_REQ      0x1019    //车队聊天
	#define SEND_TEAMMEDIA_RSP      0x8019    //车队聊天响应

	#define QUERY_CARDATA_REQ		0x1020   // 配货自动查询
	#define QUERY_CARDATA_RSP		0x8020   // 配货自动查询响应
	#define UPLOAD_DATAINFO_REQ	    0x1022   // 配货状态上报
	#define UPLOAD_DATAINFO_RSP		0x8022

	#define SEND_TEXT_MSG_REQ       0x1030    // 文本信息下发
	#define SEND_TEXT_MSG_RSP       0x8030    // 文本信息下发响应

	// 甩挂的定义
	#define SEND_SCHEDULE_REQ 		0x1040   // 下发调度单请求
	#define SEND_SCHEDULE_RSP 		0x8040	 // 下发调度单响应
	#define RESULT_SCHEDULE_REQ 	0x1045	 // 上报调度单下发结果
	#define RESULT_SCHEDULE_RSP	 	0x8045   // 响应上报结果应答
	#define QUERY_SCHEDULE_REQ 		0x1041	 // 查询调度单请求
	#define QUERY_SCHEDULE_RSP		0x8041	 // 查询调度单响应
	#define UPLOAD_SCHEDULE_REQ 	0x1042	 // 上传调度单
	#define UPLOAD_SCHEDULE_RSP		0x8042	 // 上传调度单响应
	#define STATE_SCHEDULE_REQ 		0x1043	 // 上报调度单状态
	#define STATE_SCHEDULE_RSP		0x8043	 // 上报调度单响应
	#define ALARM_SCHEDULE_REQ 		0x1044	 // 车挂告警
	#define ALARM_SCHEDULE_RSP		0x8044	 //

	#define SUBSCRIBE_REQ           0x1050   // 订阅管理
	#define SUBSCRIBE_RSP	        0x8050   // 订阅管理回复

	#define UP_MSGDATA_REQ          0x1060   // 终端透传
	#define UP_MSGDATA_RSP          0x8060
	#define UP_REPORTERROR_REQ 		0x1070	 //上传车辆故障
	#define UP_REPORTERROR_RSP 		0x8070

	#define QUERY_INFO_REQ          0x1090   //查询订阅信息上报
	#define QUERY_INFO_RSP          0x8090   //查询订阅信息回复

	/*货运二期新增加的协议*/
	#define TERMINAL_COMMON_RSP                   0x1000   //终端通用回复
	#define PLATFORM_COMMON_RSP                   0x8000   //平台通用回复

	#define UP_CARDATA_INFO_REQ                   0x1023   //上传配货信息
	#define UP_CARDATA_INFO_CONFIRM_REQ           0x1024   //配货成交状态确认

	#define UP_QUERY_ORDER_FORM_INFO_REQ          0x1026   //订单详细查询
	#define UP_QUERY_ORDER_FORM_INFO_RSP          0x8026   //订单详细查询响应
	#define UP_ORDER_FORM_INFO_REQ                0x1027   //上传货运订单号
	#define UP_TRANSPORT_FORM_INFO_REQ            0x1028   //上传运单状态

	#define SEND_CARDATA_INFO_REQ	       		  0x1021 //下发配货信息
	#define SEND_CARDATA_INFO_CONFIRM_REQ  		  0x1024 //配货成交状态确认
	#define SEND_ORDER_FORM_REQ            		  0x102E //推送详细订单
	#define SEND_TRANSPORT_ORDER_FORM_REQ  		  0x1025 //下发运单信息

	// 车友列表
	class CFriendDataInfo : public share::Ref
	{
	public:
		CFriendDataInfo( )
		{
		}
		;
		~CFriendDataInfo( )
		{
		}
		;
		bool UnPack( CPacker *pack )
		{
			_avatar = pack->readInt();
			_userid = pack->readInt();
			pack->readBytes( _username, sizeof ( _username ) );
			pack->readString( _dest );
			_Type = pack->readShort();
			_bulk = pack->readShort();
			_weight = pack->readShort();
			_model = pack->readByte();

			if (pack->readString( _org ) ==0)
				return false;
			if (pack->readString( _desc )==0)
				return false;

			return true;
		}
		void Pack( CPacker *pack )
		{
			pack->writeInt32( _avatar );
			pack->writeInt32( _userid );
			pack->writeBytes( _username, sizeof ( _username ) );
			pack->writeInt16( _Type );
			pack->writeInt16( _bulk );
			pack->writeInt16( _weight );
			pack->writeByte( _model );
			pack->writeString( _org );
			pack->writeString( _desc );
		}
	public:
		uint32_t _avatar; //司机头像
		uint32_t _userid; //司机ID
		uint8_t _username[12]; //司机姓名
		CQString _dest; //货车目的地
		uint16_t _Type; //货车车型
		uint16_t _bulk; //货车体积
		uint16_t _weight; //车辆载重量
		uint8_t _model; //长高宽(规格)
		CQString _org; //所属机构
		CQString _desc; //备注
	};

	// 车队聊天
	class CSendTeamMediaReq : public IPacket
	{
	public:
		CSendTeamMediaReq( uint32_t seq = 0 )
		{
			_header._type = SEND_TEAMMEDIA_REQ;
			_header._seq = seq;
		}
		;
		~CSendTeamMediaReq( )
		{
		}
		;
		bool UnBody( CPacker *pack )
		{
			_ownid  = pack->readInt();
			_teamid = pack->readInt();

			if (pack->readString( _voice ) ==0)
			   return false;

			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _ownid );
			pack->writeInt32( _teamid );
			pack->writeString( _voice );
		}
	public:
		uint32_t _ownid; //司机ID
		uint32_t _teamid; //车队ID
		CQString _voice; //语音数据

	};

	// 车队聊天响应
	class CSendTeamMediaRsp : public IPacket
	{
	public:
		CSendTeamMediaRsp( uint32_t seq = 0 )
		{
			_header._type = SEND_TEAMMEDIA_RSP;
			_header._seq = seq;
		}
		;
		~CSendTeamMediaRsp( )
		{
		}
		;
		bool UnBody( CPacker *pack )
		{
			_teamid = pack->readInt();
			_state = pack->readByte();
			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _teamid );
			pack->writeByte( _state );
		}
	public:
		uint32_t _teamid; //车队ID
		uint8_t _state; //语音数据发送状态
	};
	// 车友聊天
	class CSendMediaDataReq : public IPacket
	{
	public:
		CSendMediaDataReq( )
		{
			_header._type = SEND_MEDIADATA_REQ;
		}
		;
		~CSendMediaDataReq( )
		{
		}
		;

		bool UnBody( CPacker *pack )
		{
			_ownid = pack->readInt();
			_userid = pack->readInt();

			if (pack->readString( _voice) == 0)
				return false;

			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _ownid );
			pack->writeInt32( _userid );
			pack->writeString( _voice );
		}
	public:
		uint32_t _ownid; //司机ID
		uint32_t _userid; //车友ID
		CQString _voice; //语音数据
	};
	// 车友聊天响应
	class CSendMediaDataRsp : public IPacket
	{
	public:
		CSendMediaDataRsp( uint32_t seq = 0 )
		{
			_header._type = SEND_MEDIADATA_RSP;
			_header._seq = seq;
		}
		;
		~CSendMediaDataRsp( )
		{
		}
		;
		bool UnBody( CPacker *pack )
		{
			_ownid = pack->readInt();
			_userid = pack->readInt();

			if (pack->readString( _voice ) == 0)
				return false;

			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _ownid );
			pack->writeInt32( _userid );
			pack->writeString( _voice );
		}
	public:
		uint32_t _ownid; //司机ID
		uint32_t _userid; //车友ID
		CQString _voice; //语音数据
	};
	// 接收头车的信息
	class CInfoPriMcarReq : public IPacket
	{
	public:
		CInfoPriMcarReq( )
		{
			_header._type = SET_PRIMCAR_REQ;
		}
		;
		~CInfoPriMcarReq( )
		{
		}
		;

		bool UnBody( CPacker *pack )
		{
			_userid = pack->readInt();

			if (pack->readString( _name ) == 0)
				return false;

			_type   = pack->readShort();
			_weight = pack->readShort();

			if (pack->readString( _carnum ) ==0)
				 return false;
			if (pack->readString( _dest ) == 0)
				 return false;

			_speed = pack->readShort();

			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _userid );
			pack->writeInt16( _type );
			pack->writeInt16( _weight );
			pack->writeString( _name );
			pack->writeString( _carnum );
			pack->writeString( _dest );
			pack->writeInt16( _speed );
		}
	public:
		uint32_t _userid; //司机身份ID
		CQString _name; //司机姓名
		uint16_t _type; //车辆型号
		uint16_t _weight; // 车辆载重量
		CQString _carnum; //车牌号
		CQString _dest; //车辆目的地
		uint16_t _speed; //速度
	};

	// 接收头车的信息响应
	class CInfoPriMcarRsp : public IPacket
	{
	public:
		CInfoPriMcarRsp( uint32_t seq = 0 )
		{
			_header._type = INFO_PRIMCAR_REQ;
			_header._seq = seq;
		}
		;
		~CInfoPriMcarRsp( )
		{
		}
		;

		bool UnBody( CPacker *pack )
		{
			_state = pack->readByte();
			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeByte( _state );
		}
	public:
		uint8_t _state; //接收状态(0 成功，1失败)
	};
	// 设置本车为头车
	class CSetPriMcarReq : public IPacket
	{
	public:
		CSetPriMcarReq( )
		{
			_header._type = SET_PRIMCAR_REQ;
		}
		;
		~CSetPriMcarReq( )
		{
		}
		;

		bool UnBody( CPacker *pack )
		{
			_teamid = pack->readInt();
			_userid = pack->readInt();
			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _teamid );
			pack->writeInt32( _userid );
		}
	public:
		uint32_t _teamid; //车队ID
		uint32_t _userid; //车友ID
	};

	// 设置本车为头车响应
	class CSetPriMcarRsp : public IPacket
	{
	public:
		CSetPriMcarRsp( uint32_t seq = 0 )
		{
			_header._type = ADD_CARTEAM_RSP;
			_header._seq = seq;
		}
		;
		~CSetPriMcarRsp( )
		{
		}
		;

		bool UnBody( CPacker *pack )
		{
			_userid = pack->readInt();
			_state = pack->readByte();
			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _userid );
			pack->writeByte( _state );
		}
	public:
		uint32_t _userid; //车友ID
		uint8_t _state; //头车设置状态(0 成功，1失败)
	};
	// 车队成员邀请
	class CInviteNumberReq : public IPacket
	{
	public:
		CInviteNumberReq( )
		{
			_header._type = INVITE_NUMBER_REQ;
		}
		;
		~CInviteNumberReq( )
		{
		}
		;

		bool UnBody( CPacker *pack )
		{
			_teamid = pack->readInt();
			_userid = pack->readInt();
			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _teamid );
			pack->writeInt32( _userid );
		}
	public:
		uint32_t _teamid; //车队ID
		uint32_t _userid; //车友ID
	};

	// 车队成员邀请响应
	class CInviteNumberRsp : public IPacket
	{
	public:
		CInviteNumberRsp( uint32_t seq = 0 )
		{
			_header._type = ADD_CARTEAM_RSP;
			_header._seq = seq;
		}
		;
		~CInviteNumberRsp( )
		{
		}
		;
		bool UnBody( CPacker *pack )
		{
			_userid = pack->readInt();
			_teamid = pack->readInt();
			_state = pack->readByte();
			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _userid );
			pack->writeInt32( _teamid );
			pack->writeByte( _state );
		}
	public:
		uint32_t _teamid; //车队ID
		uint32_t _userid; //车友ID
		uint8_t _state; //邀请状态(0 成功，1拒绝，2 不在线)
	};
	// 建立车队
	class CAddCarTeamReq : public IPacket
	{
	public:
		CAddCarTeamReq( )
		{
			_header._type = ADD_CARTEAM_REQ;
		}
		;
		~CAddCarTeamReq( )
		{
		}
		;

		bool UnBody( CPacker *pack )
		{
			_id = pack->readInt();
			if (pack->readString( _teamname ) == 0)
				return false;

			_teamnum = pack->readShort();
			pack->readString( _teamdesc );
			_teamtype = pack->readByte();
			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _id );
			pack->writeString( _teamname );
			pack->writeInt16( _teamnum );
			pack->writeString( _teamdesc );
			pack->writeByte( _teamtype );
		}
	public:
		uint32_t _id; //司机身份ID号
		CQString _teamname; //车队名称
		uint16_t _teamnum; //车队人数限额
		CQString _teamdesc; //车队说明
		uint8_t _teamtype; //车队模式
	};
	// 建立车队响应
	class CAddCarTeamRsp : public IPacket
	{
	public:
		CAddCarTeamRsp( uint32_t seq = 0 )
		{
			_header._type = ADD_CARTEAM_RSP;
			_header._seq = seq;
		}
		;
		~CAddCarTeamRsp( )
		{
		}
		;
		bool UnBody( CPacker *pack )
		{
			_userid = pack->readInt();
			_teamid = pack->readInt();
			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _userid );
			pack->writeInt32( _teamid );
		}
	public:
		uint32_t _userid; //司机身份ID
		uint32_t _teamid; //车队创建状态(0 失败，其它为车队ID)
	};
	// 获取车友列表
	class CGetFriendListReq : public IPacket
	{
	public:
		CGetFriendListReq( )
		{
			_header._type = GET_FRIENDLIST_REQ;
		}
		;
		~CGetFriendListReq( )
		{
		}
		;

		bool UnBody( CPacker *pack )
		{
			_id = pack->readInt();
			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _id );
		}
	public:
		uint32_t _id; //司机身份ID号
	};
	// 获取车友响应
	class CGetFriendListRsp : public IPacket
	{
		typedef std::vector< CFriendDataInfo* > CFriendDataInfoVec;
	public:
		CGetFriendListRsp( uint32_t seq = 0 )
		{
			_header._type = GET_FRIENDLIST_RSP;
			_header._seq = seq;
		}
		;
		~CGetFriendListRsp( )
		{
			Clear();
		}
		;

		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();
			if ( _num == 0 ) return false;
			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				CFriendDataInfo *info = new CFriendDataInfo;
				if ( ! info->UnPack( pack ) ) {
					delete info;
					continue;
				}
				info->AddRef();
				_vec.push_back( info );
			}
			_num = _vec.size();
			return true;
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _num );
			if ( _num == 0 ) return;

			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				_vec[i]->Pack( pack );
			}
		}

	private:
		void Clear( void )
		{
			if ( _vec.empty() ) return;
			for ( int i = 0 ; i < ( int ) _vec.size() ; ++ i ) {
				_vec[i]->Release();
			}
			_vec.clear();
		}
	public:
		uint16_t _num; //车友个数(没有则返回0)
		CFriendDataInfoVec _vec;
	};
	// 邀请车友
	class CInviteFriendReq : public IPacket
	{
	public:
		CInviteFriendReq( )
		{
			_header._type = INVITE_FRIEND_REQ;
		}
		;
		~CInviteFriendReq( )
		{
		}
		;

		bool UnBody( CPacker *pack )
		{
			_ownid = pack->readInt();
			_userid = pack->readInt();

			return true;
		}

		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _ownid );
			pack->writeInt32( _userid );
		}
	public:
		uint32_t _ownid; //司机身份ID号
		uint32_t _userid; //车友身份ID号
	};

	// 邀请车友回应
	class CInviteFriendRsp : public IPacket
	{
		typedef std::vector< CFriendDataInfo* > CFriendDataInfoVec;
	public:
		CInviteFriendRsp( uint32_t seq = 0 )
		{
			_header._type = ADD_FRIEND_RSP;
			_header._seq = seq;
		}
		;
		~CInviteFriendRsp( )
		{

		}
		;
		bool UnBody( CPacker *pack )
		{
			_ownid = pack->readInt();
			_userid = pack->readInt();
			_state = pack->readByte();
			return true;
		}
		void Body( CPacker *pack )
		{
			pack->writeInt32( _ownid );
			pack->writeInt32( _userid );
			pack->writeByte( _state );
		}
	public:
		uint32_t _ownid; //司机身份ID号
		uint32_t _userid; //车友身份ID号
		uint8_t _state; //邀请状态(0 成功，1拒绝，2 不在线)
	};
	// 添加车友
	class CAddFriendsReq : public IPacket
	{
	public:
		CAddFriendsReq( )
		{
			_header._type = ADD_FRIEND_REQ;
		}
		;
		~CAddFriendsReq( )
		{
		}
		;
	// 解包
		bool UnBody( CPacker *pack )
		{
			_ownid = pack->readInt();
			_userid = pack->readInt();
			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _ownid );
			pack->writeInt32( _userid );
		}
	public:
		uint32_t _ownid; //司机身份ID号
		uint32_t _userid; //车友身份ID号
	};
	// 添加车友响应
	class CAddFriendsRsp : public IPacket
	{
	public:
		CAddFriendsRsp( uint32_t seq = 0 )
		{
			_header._type = ADD_FRIEND_RSP;
			_header._seq = seq;
		}
		;
		~CAddFriendsRsp( )
		{

		}
		;
		bool UnBody( CPacker *pack )
		{
			_state = pack->readByte();
			_userid = pack->readInt();
			return true;
		}
		void Body( CPacker *pack )
		{
			pack->writeByte( _state );
			pack->writeInt32( _userid );
		}
	public:
		uint8_t _state; //操作状态(0 接受，1拒绝，2不在线)
		uint32_t _userid; //车友ID号
	};

	// 查找附近好友
	class CQueryFriendsReq : public IPacket
	{
	public:
		CQueryFriendsReq( )
		{
			_header._type = QUERY_FRIENDS_REQ;
		}
		;
		~CQueryFriendsReq( )
		{
		}
		;
		// 解包
		bool UnBody( CPacker *pack )
		{
			_id = pack->readInt();
			_Lon = pack->readInt();
			_Lat = pack->readInt();

			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeInt32( _id );
			pack->writeInt32( _Lon );
			pack->writeInt32( _Lat );
		}
	public:
		uint32_t _id; //终端用户唯一标识码
		uint32_t _Lon; //GPS的经度
		uint32_t _Lat; //GPS的纬度
	};

	// 返回查找附近好友
	class CQueryFriendsRsp : public IPacket
	{
		typedef std::vector< CFriendDataInfo* > CFriendDataInfoVec;
	public:
		CQueryFriendsRsp( uint32_t seq = 0 )
		{
			_header._type = QUERY_FRIENDS_RSP;
			_header._seq = seq;
		}
		;
		~CQueryFriendsRsp( )
		{
			Clear();
		}
		;
		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();
			if ( _num == 0 ) return false;
			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				CFriendDataInfo *info = new CFriendDataInfo;
				if ( ! info->UnPack( pack ) ) {
					delete info;
					continue;
				}
				info->AddRef();
				_vec.push_back( info );
			}
			_num = _vec.size();
			return true;
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _num );
			if ( _num == 0 ) return;

			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				_vec[i]->Pack( pack );
			}
		}

	private:
		void Clear( void )
		{
			if ( _vec.empty() ) return;
			for ( int i = 0 ; i < ( int ) _vec.size() ; ++ i ) {
				_vec[i]->Release();
			}
			_vec.clear();
		}
	public:
		uint16_t _num; //查找到的车友个数，如果没有则为
		CFriendDataInfoVec _vec;

	};
	// 司机身份注销
	class CDriverLoginOutReq : public IPacket
	{
	public:
		CDriverLoginOutReq( )
		{
			_header._type = DRIVER_LOGOUT_REQ;
		}
		;
		~CDriverLoginOutReq( )
		{
		}
		;
		// 解包
		bool UnBody( CPacker *pack )
		{
			if ( pack->readString( _identify ) == 0 ) return false;
			if ( pack->readString( _driverid ) == 0 ) return false;
			if ( pack->readString( _carid ) == 0 ) return false;
			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeString( _identify );
			pack->writeString( _driverid );
			pack->writeString( _carid );
		}
	public:
		CQString _identify; //	Variant	   身份证号码
		CQString _driverid; //	Variant	   联名卡号
		CQString _carid; //	   Variant	   车机编号
	};
	// 返回司机的注销信息 xfm
	class CDriverLoginOutRsp : public IPacket
	{
	public:
		CDriverLoginOutRsp( uint32_t seq = 0 )
		{
			_header._type = DRIVER_LOGOUT_RSP;
			_header._seq = seq;
		}
		;
		~CDriverLoginOutRsp( )
		{

		}
		;
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _time, sizeof ( _time ) );
			_state = pack->readByte();
			pack->readBytes( _name, sizeof ( _name ) );
			return true;
		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _time, sizeof ( _time ) );
			pack->writeByte( _state );
			pack->writeBytes( _name, sizeof ( _name ) );
		}
	public:
		uint8_t _time[6]; //注销时间(BCD,YY-mm-dd HH-ii-ss)
		uint8_t _state; //注销状态
		uint8_t _name[12]; //司机姓名
	};

	// 司机身份登陆认证
	class CDriverLoginReq : public IPacket
	{
	public:
		CDriverLoginReq( )
		{
			_header._type = DRIVER_LOGIN_REQ;
		}
		;
		~CDriverLoginReq( )
		{
		}
		;
		// 解包
		bool UnBody( CPacker *pack )
		{
			if ( pack->readString( _identify ) == 0 ) return false;
			if ( pack->readString( _driverid ) == 0 ) return false;
			if ( pack->readString( _phonenum ) == 0 ) return false;
			if ( pack->readString( _simnum ) == 0 ) return false;
			if ( pack->readString( _carid ) == 0 ) return false;

			return true;
		}
		// 打包数据体
		void Body( CPacker *pack )
		{
			pack->writeString( _identify );
			pack->writeString( _driverid );
			pack->writeString( _phonenum );
			pack->writeString( _simnum );
			pack->writeString( _carid );
		}
	public:
		CQString _identify; //	Variant	   身份证号码
		CQString _driverid; //	Variant	    联名卡号
		CQString _phonenum; //	Variant	    司机手机号码(BCD手机号)
		CQString _simnum; //	Variant	  SIM卡串号
		CQString _carid; //	Variant  车机编号
	};
	// 配货查询
	class CQueryCarDataReq : public IPacket
	{
	public:
		CQueryCarDataReq( )
		{
			_header._type = QUERY_CARDATA_REQ;
		}
		;
		~CQueryCarDataReq( )
		{
		}
		;

		// 解包
		bool UnBody( CPacker *pack )
		{

			_destarea = pack->readInt();
			_srcarea = pack->readInt();

			if ( pack->readBytes( _time, 6 ) == 0 ) return false;

			_offset = pack->readByte();
			_count = pack->readByte();

			return true;
		}

		// 打包数据体
		void Body( CPacker *pack )
		{

			pack->writeInt32( _destarea );
			pack->writeInt32( _srcarea );

			pack->writeBytes( _time, 6 );
			pack->writeByte( _offset );
			pack->writeByte( _count );
		}

	public:
		uint32_t _destarea; // 目的地
		uint32_t _srcarea; //   出发地
		uint8_t _time[6]; //	String	配货时间
		uint8_t _offset; //	1	UINT8	读取开始位置，默认从0开始
		uint8_t _count; //  1	UINT8	记录条数
	};

	// 直接使用对象进行转换处理 dtxi
	class CCarDataInfo : public share::Ref
	{
	public:
		CCarDataInfo( )
		{
		}
		;
		~CCarDataInfo( )
		{
		}
		;

		bool UnPack( CPacker *pack )
		{
			if ( pack->readBytes( _sid, 20 ) == 0 ) return false;
			if ( pack->readBytes( _time, 6 ) == 0 ) return false;

			_sarea = pack->readInt();
			_darea = pack->readInt();

			pack->readString(_stype);
			_model = pack->readInt();
			_bulk = pack->readInt();
			_nnum = pack->readShort();

			pack->readBytes( _contact, sizeof ( _contact ) );
			if ( pack->readBytes( _phone, 6 ) == 0 ) return false;
			pack->readString( _info );

			return true;
		}

		void Pack( CPacker *pack )
		{
			pack->writeBytes( _sid, 20 );
			pack->writeBytes( _time, 6 );

			pack->writeInt( _sarea );
			pack->writeInt( _darea );

			pack->writeString( _stype );
			pack->writeInt( _model );
			pack->writeInt( _bulk );
			pack->writeShort( _nnum );
			pack->writeBytes( _contact, sizeof ( _contact ) );

			pack->writeBytes( _phone, 6 );
			pack->writeString( _info );
		}

	public:
		uint8_t _sid[20]; //  String	配货编号
		uint8_t _time[6]; //  String	配货时间
		uint32_t _sarea; //	 UINT32  	配货出发地
		uint32_t _darea; //	 UINT32	           配货目的地
		CQString _stype; //	 Variant    货物类型
		uint32_t _model; //   UINT32	货物规格
		uint32_t _bulk; //    UINT32	货物体积
		uint16_t _nnum; //    UINT16	车辆要求O
		uint8_t _contact[12]; //
		uint8_t _phone[6]; //  String	货主电话(BCD，前面不够补零)
		CQString _info; //  Variant 	备注用户说明
	};

	// 返回司机的认证信息 xfm
	class CDriverLoginRsp : public IPacket
	{
	public:
		CDriverLoginRsp( uint32_t seq = 0 )
		{
			_header._type = DRIVER_LOGIN_RSP;
			_header._seq = seq;
		}
		;
		~CDriverLoginRsp( )
		{

		}
		;
		bool UnBody( CPacker *pack )
		{

			pack->readBytes( _time, sizeof ( _time ) );

			_userid = pack->readInt();
			if ( pack->readString( _pic ) == 0 ) return false;

			pack->readBytes( _name, sizeof ( _name ) );

			pack->readBytes( _simnum, sizeof ( _simnum ) );

			_grade = pack->readShort();
			_score = pack->readInt();

			pack->readBytes( _carnum, sizeof ( _carnum ) );

			return true;
		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _time, sizeof ( _time ) );
			pack->writeInt32( _userid );
			pack->writeString( _pic );
			pack->writeBytes( _name, sizeof ( _name ) );
			pack->writeBytes( _simnum, sizeof ( _simnum ) );
			pack->writeInt16( _grade );
			pack->writeInt32( _score );
			pack->writeBytes( _carnum, sizeof ( _carnum ) );
		}
	public:
		uint8_t _time[6]; //认证时间(BCD时间)
		uint32_t _userid; //认证状态数据(返回用户的ID)
		CQString _pic; //司机照片
		uint8_t _name[12]; //司机姓名
		uint8_t _simnum[18]; //SIM卡串号
		uint16_t _grade; //会员等级
		uint32_t _score; //会员积分
		uint8_t _carnum[12]; //车牌号
	};
	// 查询配货信息的响应
	class CQueryCarDataRsp : public IPacket
	{
		typedef std::vector< CCarDataInfo* > CCarDataInfoVec;
	public:
		CQueryCarDataRsp( uint32_t seq = 0 ) :
				_num( 0 )
		{
			_header._type = QUERY_CARDATA_RSP;
			_header._seq = seq;
		}
		;
		~CQueryCarDataRsp( )
		{
			Clear();
		}
		;

		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();
			if ( _num == 0 ) return false;
			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				CCarDataInfo *info = new CCarDataInfo;
				if ( ! info->UnPack( pack ) ) {
					delete info;
					continue;
				}
				info->AddRef();
				_vec.push_back( info );
			}
			_num = _vec.size();
			return true;
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _num );
			if ( _num == 0 ) return;

			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				_vec[i]->Pack( pack );
			}
		}

	private:
		void Clear( void )
		{
			if ( _vec.empty() ) return;
			for ( int i = 0 ; i < ( int ) _vec.size() ; ++ i ) {
				_vec[i]->Release();
			}
			_vec.clear();
		}

	public:
		// 查询返回的信息个数
		uint8_t _num;
		// 存放数据的对象
		CCarDataInfoVec _vec;
	};

	// 甩挂的定义
	// GPS基本信息结构对象
	class GpsInfo : public share::Ref
	{
	public:
		GpsInfo( )
		{
		}
		;
		~GpsInfo( )
		{
		}
		;

		void Pack( CPacker *pack )
		{
			pack->writeInt( _alam );
			pack->writeInt( _state );
			pack->writeInt( _lat );
			pack->writeInt( _lon );
			pack->writeShort( _height );
			pack->writeShort( _speed );
			pack->writeShort( _direction );
			pack->writeBytes( _time, 6 );
		}

		bool UnPack( CPacker *pack )
		{

			_alam = pack->readInt();
			_state = pack->readInt();
			_lat = pack->readInt();
			_lon = pack->readInt();
			_height = pack->readShort();
			_speed = pack->readShort();
			_direction = pack->readShort();

			if ( pack->readBytes( _time, 6 ) == 0 ) return false;

			return true;
		}

	public:

		uint32_t _alam; //	4	UINT32	报警标志位
		uint32_t _state; //	4	UINT32	状态位
		uint32_t _lat; //	4	UINT32	纬度
		uint32_t _lon; //  4	UINT32	经度
		uint16_t _height; //	2	UINT16	高程
		uint16_t _speed; //	2	UINT16	速度
		uint16_t _direction; //	2	UINT16	方向
		uint8_t _time[6]; //	String	YY-MM-DD-hh-mm-ss(BCD时间)

	};

	// 调度单基本信息
	class CScheduleInfo : public share::Ref
	{
	public:
		CScheduleInfo( )
		{
			memset( _carnum, 0, sizeof ( _carnum ) );
		}
		~CScheduleInfo( )
		{
		}

		void Pack( CPacker *pack )
		{
			pack->writeBytes( _scheduleid, 18 );
			pack->writeString( _sarea );
			pack->writeBytes( _srcid, 18 );
			pack->writeBytes( _start, 6 );
			pack->writeBytes( _carnum, 12 );
			pack->writeBytes( _hangid, 17 );
			pack->writeString( _darea );
			pack->writeBytes( _destid, 18 );
			pack->writeBytes( _atime, 6 );
			pack->writeBytes( _stime, 6 );
		}

		bool UnPack( CPacker *pack )
		{
			if ( pack->readBytes( _scheduleid, 18 ) == 0 ) return false;
			pack->readString( _sarea );

			if ( pack->readBytes( _srcid, 18 ) == 0 ) return false;
			if ( pack->readBytes( _start, 6 ) == 0 ) return false;
			if ( pack->readBytes( _carnum, 12 ) == 0 ) return false;
			if ( pack->readBytes( _hangid, 17 ) == 0 ) return false;
			pack->readString( _darea );

			if ( pack->readBytes( _destid, 18 ) == 0 ) return false;
			if ( pack->readBytes( _atime, 6 ) == 0 ) return false;
			if ( pack->readBytes( _stime, 6 ) == 0 ) return false;
			return true;
		}

	public:
		uint8_t _scheduleid[18]; // String	调度单号
		CQString _sarea; // Variant	出发地
		uint8_t _srcid[18]; // String	出发车位
		uint8_t _start[6]; // String	出发时间
		uint8_t _carnum[12]; // String	挂车车牌号
		uint8_t _hangid[17]; // String	挂车ID
		CQString _darea; // Variant	目的地
		uint8_t _destid[18]; // String	目的车位
		uint8_t _atime[6]; // String	到达时间
		uint8_t _stime[6]; // String	调度时间
	};

	// #define SEND_SCHEDULE_REQ 		0x1040   // 下发调度单请求
	class CSendScheduleReq : public IPacket
	{
	public:
		CSendScheduleReq( )
		{
			_header._type = SEND_SCHEDULE_REQ;
		}
		~CSendScheduleReq( )
		{
		}

		void Body( CPacker *pack )
		{
			_info.Pack( pack );
		}

		bool UnBody( CPacker *pack )
		{
			return _info.UnPack( pack );
		}

	public:
		CScheduleInfo _info; // 调度单信息
	};

	// #define SEND_SCHEDULE_RSP 		0x8040	 // 下发调度单响应
	class CSendScheduleRsp : public IPacket
	{
	public:
		CSendScheduleRsp( uint32_t seq = 0 )
		{
			_header._type = SEND_SCHEDULE_RSP;
			_header._seq = seq;
		}
		~CSendScheduleRsp( )
		{
		}

		bool UnBody( CPacker *pack )
		{
			_result = pack->readByte();
			return true;
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _result );
		}

	public:
		uint8_t _result; //1	UINT8 0成功，1失败
	};

	// 上报调度单下发响应的结果  RESULT_SCHEDULE_REQ  0x1045
	class CResultScheduleReq : public IPacket
	{
	public:
		CResultScheduleReq( )
		{
			_header._type = RESULT_SCHEDULE_REQ;
		}
		~CResultScheduleReq( )
		{
		}

		bool UnBody( CPacker *pack )
		{
			if ( pack->readBytes( _scheduleid, 18 ) == 0 ) return false;
			_result = pack->readByte();
			return true;
		}

		void Body( CPacker *pack )
		{
			pack->writeBytes( _scheduleid, 18 );
			pack->writeByte( _result );
		}

	public:
		uint8_t _scheduleid[18]; // String	调度单号
		uint8_t _result; //1	UINT8	接受拒绝（0 接受，1拒绝）
	};

	// #define RESULT_SCHEDULE_RSP 		0x8045	 // 上报下发调度单响应应答
	class CResultScheduleRsp : public IPacket
	{
	public:
		CResultScheduleRsp( uint32_t seq = 0 )
		{
			_header._type = RESULT_SCHEDULE_RSP;
			_header._seq = seq;
		}
		~CResultScheduleRsp( )
		{
		}

		bool UnBody( CPacker *pack )
		{
			_result = pack->readByte();
			return true;
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _result );
		}

	public:
		uint8_t _result; //1	UINT8	0成功，1失败
	};

	//#define QUERY_SCHEDULE_REQ 		0x1041	 // 查询调度单请求
	class CQueryScheduleReq : public IPacket
	{
	public:
		CQueryScheduleReq( )
		{
			_header._type = QUERY_SCHEDULE_REQ;
		}
		~CQueryScheduleReq( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _num );
		}

		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();
			return true;
		}

	public:
		uint8_t _num; // 查询返回调度单个数
	};

	//#define QUERY_SCHEDULE_RSP		0x8041	 // 查询调度单响应
	class CQueryScheduleRsp : public IPacket
	{
		typedef std::vector< CScheduleInfo* > ScheduleVec;
	public:
		CQueryScheduleRsp( uint8_t seq = 0 )
		{
			_header._type = QUERY_SCHEDULE_RSP;
			_header._seq = seq;
		}
		~CQueryScheduleRsp( )
		{
			Clear();
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _num );
			// 打包所有数据体
			for ( int i = 0 ; i < _num ; ++ i ) {
				_vec[i]->Pack( pack );
			}
		}

		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();
			for ( int i = 0 ; i < _num ; ++ i ) {
				CScheduleInfo *info = new CScheduleInfo;
				if ( ! info->UnPack( pack ) ) {
					delete info;
					continue;
				}
				info->AddRef();
				_vec.push_back( info );
			}
			return true;
		}

	private:
		// 清理所有对象数据
		void Clear( void )
		{
			if ( _vec.empty() ) return;

			for ( int i = 0 ; i < ( int ) _vec.size() ; ++ i ) {
				_vec[i]->Release();
			}
			_vec.clear();
		}

	public:
		uint8_t _num;
		ScheduleVec _vec;
	};

	// #define UPLOAD_SCHEDULE_REQ 	0x1042	 // 上传调度单
	class CUploadScheduleReq : public IPacket
	{
	public:
		CUploadScheduleReq( )
		{
			_header._type = UPLOAD_SCHEDULE_REQ;
		}
		~CUploadScheduleReq( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeBytes( _scheduleid, 18 );
			pack->writeByte( _matchstate );
			pack->writeBytes( _hangid, 17 );
			pack->writeBytes( _hangnum, 12 );
			pack->writeBytes( _mtime, 6 );
			_info.Pack( pack );
		}

		bool UnBody( CPacker *pack )
		{
			if ( pack->readBytes( _scheduleid, 18 ) == 0 ) return false;

			_matchstate = pack->readByte();

			if ( pack->readBytes( _hangid, 17 ) == 0 ) return false;
			if ( pack->readBytes( _hangnum, 12 ) == 0 ) return false;
			if ( pack->readBytes( _mtime, 6 ) == 0 ) return false;
			return _info.UnPack( pack );
		}

	public:
		uint8_t _scheduleid[18]; //  String	调度单号
		uint8_t _matchstate; //  UINT8	匹配状态(0成功，1失败)
		uint8_t _hangid[17]; //	String	挂车ID
		uint8_t _hangnum[12]; //	String	挂车车牌号
		uint8_t _mtime[6]; //  String	匹配时间
		GpsInfo _info; // GPS
	};

	// #define UPLOAD_SCHEDULE_RSP		0x8042	 // 上传调度单响应
	class CUploadScheduleRsp : public IPacket
	{
	public:
		CUploadScheduleRsp( uint32_t seq = 0 )
		{
			_header._type = UPLOAD_SCHEDULE_RSP;
			_header._seq = seq;
		}
		~CUploadScheduleRsp( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _result );
		}

		bool UnBody( CPacker *pack )
		{
			_result = pack->readByte();
			return true;
		}

	public:
		uint8_t _result; // 成功与否
	};

	// #define STATE_SCHEDULE_REQ 		0x1043	 // 上报调度单状态
	class CStateScheduleReq : public IPacket
	{
	public:
		CStateScheduleReq( )
		{
			_header._type = STATE_SCHEDULE_REQ;
		}
		~CStateScheduleReq( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeBytes( _scheduleid, 18 );
			pack->writeByte( _action );
			pack->writeBytes( _hangid, 17 );
			pack->writeBytes( _hangnum, 12 );
			_info.Pack( pack );
		}

		bool UnBody( CPacker *pack )
		{
			if ( pack->readBytes( _scheduleid, 18 ) == 0 ) return false;
			_action = pack->readByte();
			if ( pack->readBytes( _hangid, 17 ) == 0 ) return false;
			if ( pack->readBytes( _hangnum, 12 ) == 0 ) return false;
			return _info.UnPack( pack );
		}

	public:
		uint8_t _scheduleid[18]; // String	调度单号
		uint8_t _action; // UINT8	调度动作（ 0出发，1到达）
		uint8_t _hangid[17]; // String	挂车ID
		uint8_t _hangnum[12]; // String	挂车车牌号
		GpsInfo _info; // GPS位置信息，见表2.5.6
	};

	//#define STATE_SCHEDULE_RSP		0x8043	 // 上报调度单响应
	class CStateScheduleRsp : public IPacket
	{
	public:
		CStateScheduleRsp( uint32_t seq = 0 )
		{
			_header._type = STATE_SCHEDULE_RSP;
			_header._seq = seq;
		}
		~CStateScheduleRsp( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _result );
		}

		bool UnBody( CPacker *pack )
		{
			_result = pack->readByte();
			return true;
		}

	public:
		uint8_t _result; // 上报调度单结果
	};

	// #define ALARM_SCHEDULE_REQ 		0x1044	 // 车挂告警
	class CAlarmScheduleReq : public IPacket
	{
	public:
		CAlarmScheduleReq( )
		{
			_header._type = ALARM_SCHEDULE_REQ;
		}

		~CAlarmScheduleReq( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeBytes( _scheduleid, 18 );
			pack->writeBytes( _hangid, 17 );
			pack->writeBytes( _hangnum, 12 );
			pack->writeInt( _alarmtype );
			_info.Pack( pack );
		}

		bool UnBody( CPacker *pack )
		{
			if ( pack->readBytes( _scheduleid, 18 ) == 0 ) return false;
			if ( pack->readBytes( _hangid, 17 ) == 0 ) return false;
			if ( pack->readBytes( _hangnum, 12 ) == 0 ) return false;
			_alarmtype = pack->readInt();

			return _info.UnPack( pack );
		}

	public:
		uint8_t _scheduleid[18]; //String	调度单号
		uint8_t _hangid[17]; //String	挂车ID
		uint8_t _hangnum[12]; //String	挂车车牌号
		uint32_t _alarmtype; //UINT32	暂定义按位来算，（0位为车挂脱离）
		GpsInfo _info; //GPS位置信息，见表2.5.6
	};

	//#define ALARM_SCHEDULE_RSP		0x8044	 //
	class CAlarmScheduleRsp : public IPacket
	{
	public:
		CAlarmScheduleRsp( uint32_t seq = 0 )
		{
			_header._type = ALARM_SCHEDULE_RSP;
			_header._seq = seq;
		}
		~CAlarmScheduleRsp( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _result );
		}

		bool UnBody( CPacker *pack )
		{
			_result = pack->readByte();
			return true;
		}

	public:
		uint8_t _result; // 处理结果
	};

	//订阅列表 xfm
	class CSubscribeList : public share::Ref
	{
	public:
		CSubscribeList( )
		{

		}
		~CSubscribeList( )
		{

		}
		void Pack( CPacker *pack )
		{
			pack->writeInt16( _ctype );
		}

		bool UnPack( CPacker *pack )
		{
			_ctype = pack->readShort();
			return true;
		}
	public:
		uint16_t _ctype; //订阅类型

	};
	class CSubscrbeReq : public IPacket //0x1050 订阅管理
	{
	public:
		typedef std::vector< CSubscribeList* > SubscribeListVec;

		CSubscrbeReq( )
		{
			_header._type = SUBSCRIBE_REQ;
		}
		~CSubscrbeReq( )
		{
			Clear();
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _cmd );
			pack->writeByte( _num );
			// 打包所有数据体
			for ( int i = 0 ; i < _num ; ++ i ) {
				_vec[i]->Pack( pack );
			}
		}

		bool UnBody( CPacker *pack )
		{
			_cmd = pack->readByte();
			_num = pack->readByte();

			for ( int i = 0 ; i < _num ; ++ i ) {
				CSubscribeList *info = new CSubscribeList;
				if ( ! info->UnPack( pack ) ) {
					delete info;
					continue;
				}
				info->AddRef();
				_vec.push_back( info );
			}
			return true;
		}
	private:
		// 清理所有对象数据
		void Clear( void )
		{
			if ( _vec.empty() ) return;
			for ( int i = 0 ; i < ( int ) _vec.size() ; ++ i ) {
				_vec[i]->Release();
			}
			_vec.clear();
		}
	public:
		uint8_t _cmd; //订阅命令
		uint8_t _num; //订阅个数
		SubscribeListVec _vec;
	};
	//订阅管理回复
	class CSubscrbeRsp : public IPacket //0x8050
	{
	public:
		typedef std::vector< CSubscribeList* > SubscribeListVec;

		CSubscrbeRsp( uint32_t seq = 0 )
		{
			_header._type = SUBSCRIBE_RSP;
			_header._seq = seq;
		}
		~CSubscrbeRsp( )
		{
			Clear();
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _num );
			// 打包所有数据体
			for ( int i = 0 ; i < _num ; ++ i ) {
				_vec[i]->Pack( pack );
			}
		}

		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();
			for ( int i = 0 ; i < _num ; ++ i ) {
				CSubscribeList *info = new CSubscribeList;
				if ( ! info->UnPack( pack ) ) {
					delete info;
					continue;
				}
				info->AddRef();
				_vec.push_back( info );
			}
			return true;
		}
	private:
		// 清理所有对象数据
		void Clear( void )
		{
			if ( _vec.empty() ) return;

			for ( int i = 0 ; i < ( int ) _vec.size() ; ++ i ) {
				_vec[i]->Release();
			}
			_vec.clear();
		}
	public:
		uint8_t _num; // 处理结果
		SubscribeListVec _vec;
	};

	//订阅消息查询
	class CQueryInfoReq : public IPacket //0x1090
	{
	public:
		typedef enum
		{
			Area = 0,
			Roea,
			Bad_Weather,
			Real_Time_Weather,
			Maintenance_Reminders,
			Health_Care_Reminder,
			Annual_Reminder,
			Operation_To_Remind,
			Integrity_Aler_Reminder,
			Illegal_To_Remind,
			Notice_Notice,
			Accident_Black_Spots
		} SUBSCRIBE_TYPE;

		CQueryInfoReq( )
		{
			_header._type = QUERY_INFO_REQ;
		}
		~CQueryInfoReq( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeByte( _ctype );
			if ( _ctype == Area ) pack->writeInt32( _area_id );
			if ( _ctype == Roea ) pack->writeInt32( _road_id );
			if ( _ctype == Real_Time_Weather ) {
				pack->writeInt32( _lon );
				pack->writeInt32( _lat );
			}
		}
		bool UnBody( CPacker *pack )
		{
			_ctype = pack->readByte();
			if ( _ctype == Area ) {
				_area_id = pack->readInt();
			}
			if ( _ctype == Roea ) {
				_road_id = pack->readInt();
			}
			if ( _ctype == Real_Time_Weather ) {
				_lon = pack->readInt();
				_lat = pack->readInt();
			}
			return true;
		}
	public:
		uint8_t _ctype; //订阅类型
		uint32_t _area_id; //行政区域id
		uint32_t _road_id; //路段id
		uint32_t _lon; //经度
		uint32_t _lat; //纬度
	};

	//订阅消息回复
	class CQueryInfoRsp : public IPacket //0x8090
	{
	public:
		CQueryInfoRsp( uint32_t seq = 0 )
		{
			_header._type = QUERY_INFO_RSP;
			_header._seq = seq;
		}
		~CQueryInfoRsp( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _ctype );
			if ( _ctype == CQueryInfoReq::Area ) pack->writeInt32( _area_id );
			if ( _ctype == CQueryInfoReq::Roea ) pack->writeInt32( _road_id );

			pack->writeString( _data );
		}

		bool UnBody( CPacker *pack )
		{
			_ctype = pack->readByte();

			if ( _ctype == CQueryInfoReq::Area ) {
				_area_id = pack->readInt();
			}

			if ( _ctype == CQueryInfoReq::Roea ) {
				_road_id = pack->readInt();
			}
			pack->readString( _data );
			return true;
		}
	public:

		uint8_t _ctype; //订阅类型
		uint32_t _area_id; //行政区域id
		uint32_t _road_id; //路段id
		CQString _data; //	Variant	内容
	};

	class CErrorScheduleReq : public IPacket //0x1070
	{
	public:
		CErrorScheduleReq( )
		{
			_header._type = UP_REPORTERROR_REQ;
		}
		~CErrorScheduleReq( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeInt32( _code );
			pack->writeString( _desc);
		}

		bool UnBody( CPacker *pack )
		{
			_code = pack->readInt();

			if (pack->readString(_desc) == 0)
				return false;

			return true;
		}

	public:
		uint32_t _code; // 故障原因
		CQString _desc; //故障描述
	};

	class CErrorScheduleRsp : public IPacket //0x8070
	{
	public:
		CErrorScheduleRsp( uint32_t seq = 0 )
		{
			_header._type = UP_REPORTERROR_RSP;
			_header._seq = seq;
		}
		~CErrorScheduleRsp( )
		{
		}

		void Body( CPacker *pack )
		{

			pack->writeByte( _result );
		}

		bool UnBody( CPacker *pack )
		{

			_result = pack->readByte();
			return true;
		}
	public:
		uint8_t _result; // 处理结果
	};

	class CAutoDataScheduleReq : public IPacket //0x1022
	{
	public:
		CAutoDataScheduleReq( )
		{
			_header._type = UPLOAD_DATAINFO_REQ;
		}
		~CAutoDataScheduleReq( )
		{
		}

		void Body( CPacker *pack )
		{
			//pack->writeString(_sid);
			pack->writeByte( _state );

			if ( _state == 2 ) { //半载
				pack->writeByte( _space );
				pack->writeShort( _weight );
			}

			pack->writeBytes( _stime, 6 );
			pack->writeInt( _srcarea );
			pack->writeInt( _destarea );
		}
		bool UnBody( CPacker *pack )
		{

			//pack->readString(_sid);
			_state = pack->readByte();

			if ( _state == 2 ) { //半载
				_space = pack->readByte();
				_weight = pack->readShort();
			}

			pack->readBytes( _stime, 6 );

			_srcarea = pack->readInt();
			_destarea = pack->readInt();

			return true;
		}
	public:
		//CQString   _sid; // 配货编号
		uint8_t _state; //状态
		uint8_t _space; //载重空间
		uint16_t _weight; //载重量
		uint8_t _stime[6]; //String	出发时间
		uint32_t _srcarea; //出发地
		uint32_t _destarea; //目的地
	};

	class CAutoDataScheduleRsp : public IPacket //0x8022
	{
	public:
		CAutoDataScheduleRsp( uint32_t seq = 0 )
		{
			_header._type = UPLOAD_DATAINFO_RSP;
			_header._seq = seq;
		}

		~CAutoDataScheduleRsp( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _result );
		}

		bool UnBody( CPacker *pack )
		{
			_result = pack->readByte();
			return true;
		}
	public:
		uint8_t _result; // 处理结果
	};

	class CUpMsgDataScheduleReq : public IPacket // 0x1060 终端透传
	{
	public:
		CUpMsgDataScheduleReq( )
		{
			_header._type = UP_MSGDATA_REQ;
		}

		~CUpMsgDataScheduleReq( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _code );
			pack->writeString( _data );
		}

		bool UnBody( CPacker *pack )
		{
			_code = pack->readByte();
			pack->readString( _data );
			return true;
		}
	public:
		uint8_t _code; //透传类型
		CQString _data; //透传数据

	};

	class CUpMsgDataScheduleRsp : public IPacket // 0x8060 终端透传回复
	{
	public:
		CUpMsgDataScheduleRsp( uint32_t seq = 0 )
		{
			_header._type = UP_MSGDATA_RSP;
			_header._seq = seq;
		}

		~CUpMsgDataScheduleRsp( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _code );
			pack->writeString( _data );
		}

		bool UnBody( CPacker *pack )
		{

			_code = pack->readByte();
			pack->readString( _data );

			return true;
		}
	public:
		uint8_t _code; //透传类型
		CQString _data; //透传数据
	};

	class CUpCarDataInfoReq : public IPacket //0x1023   //上传配货信息
	{
	public:
		CUpCarDataInfoReq( )
		{
			_header._type = UP_CARDATA_INFO_REQ;
		}

		~CUpCarDataInfoReq( )
		{
		}

		void Body( CPacker *pack )
		{
			pack->writeBytes( _sid, sizeof ( _sid ) );
			pack->writeByte( _status );

			if ( _status == 0 ) {
				pack->writeInt( _price );
			}
		}
		bool UnBody( CPacker *pack )
		{

			pack->readBytes( _sid, sizeof ( _sid ) );

			_status = pack->readByte();

			if ( _status == 1 ) {
				_price = pack->readInt();
			}
			return true;
		}
	public:
		uint8_t _sid[20]; // String	配货单号
		uint8_t _status; //状态(0.已报价 1.拒绝 2.取消)
		uint32_t _price; //报价(精度0.01 元),如果状态为0，有该字段
	};

	class CSendTextMsgReq : public IPacket // 0x1030 文本消息下发
	{
	public:
		CSendTextMsgReq( )
		{
			_header._type = SEND_TEXT_MSG_REQ;
		}
		~CSendTextMsgReq( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeByte( _type );
			pack->writeString( _content );
			pack->writeBytes( _time, sizeof ( _time ) );
		}

		bool UnBody( CPacker *pack )
		{
			_type = pack->readByte();
			pack->readString( _content );
			pack->readBytes( _time, sizeof ( _time ) );
			return true;
		}

	public:
		unsigned char _type; //发送类型
		CQString _content; //发送内容
		unsigned char _time[6]; //推送时间
	};

	class CSendTextMsgRsp : public IPacket // 0x8030 文本消息回复
	{
	public:
		CSendTextMsgRsp( uint32_t seq = 0 )
		{
			_header._type = SEND_TEXT_MSG_RSP;
			_header._seq = seq;
		}
		~CSendTextMsgRsp( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeByte( _result );
		}

		bool UnBody( CPacker *pack )
		{
			_result = pack->readByte();
			return true;
		}

	public:
		uint8_t _result; // 处理结果
	};

	//下发配货信息
	class CSendCarDataInfoMsgReq:public IPacket //0x1021 下发配货信息
	{
	public:
			CSendCarDataInfoMsgReq(){
				_header._type = SEND_CARDATA_INFO_REQ;
			}

			~CSendCarDataInfoMsgReq()
			{

			}

			void Body( CPacker *pack )
			{
			   pack->writeBytes(_sid,sizeof(_sid));
			   pack->writeString(_stype);
			   pack->writeShort(_num);
			   pack->writeInt(_model);
			   pack->writeInt(_bulk);
			   pack->writeInt(_sarea);
			   pack->writeInt(_darea);
			   pack->writeBytes(_submit_time,sizeof(_submit_time));
			   pack->writeBytes(_arrival_time,sizeof(_arrival_time));
			   pack->writeInt(_price);
			   pack->writeBytes(_send_time,sizeof(_send_time));
			   pack->writeBytes(_end_time,sizeof(_end_time));
			}

			bool UnBody( CPacker *pack )
			{
			   pack->readBytes(_sid,sizeof(_sid));
			   pack->readString(_stype);
			  _num   = pack->readShort();
			  _model = pack->readInt();
			  _bulk  = pack->readInt();

			  _sarea = pack->readInt();
			  _darea = pack->readInt();

			  pack->readBytes(_submit_time,sizeof(_submit_time));
			  pack->readBytes(_arrival_time,sizeof(_arrival_time));

			  _price = pack->readInt();

			  pack->readBytes(_send_time,sizeof(_send_time));
			  pack->readBytes(_end_time,sizeof(_end_time));

			  return true;
			}
	public:
			uint8_t   _sid[20];//配货单号
			CQString  _stype;//货物类型
			uint16_t  _num;//件数
			uint32_t  _model;//总重量(吨)
			uint32_t  _bulk;//总体积(立方米 )
			uint32_t  _sarea;//出发地
			uint32_t  _darea;//目的地
			uint8_t   _submit_time[6];//提货时间(BCD)
			uint8_t   _arrival_time[6];//到达时间(BCD)
			uint32_t  _price;//报价(精度 0.01元)
			uint8_t   _send_time[6];//发送时间(BCD)
			uint8_t   _end_time[6];//有效截止时间(BCD)
	};
	//配货成交状态确认
	class CCarData_Info_Confirm_Req:public IPacket //0x1024 配货成交状态确认
	{
	public:
			CCarData_Info_Confirm_Req(){
				_header._type = SEND_CARDATA_INFO_CONFIRM_REQ;
			}
			~CCarData_Info_Confirm_Req()
			{

			}
			void Body( CPacker *pack )
			{
				pack->writeBytes(_sid,sizeof(_sid));
				pack->writeByte(_status);
			}
			bool UnBody( CPacker *pack )
			{
				pack->readBytes(_sid,sizeof(_sid));
				_status = pack->readByte();

				return true;
			}
	public:
			uint8_t   _sid[20];//配货单号
			uint8_t   _status;//状态
	};

	/*订单列表 dtxi*/
	class COrderList : public share::Ref
	{
	public:
			COrderList() {

			}

			~COrderList() {

			}

			bool UnPack(CPacker *pack) {

			  pack->readBytes(_sid,sizeof(_sid));
			  pack->readBytes(_dsid,sizeof(_dsid));

			  pack->readString(_stype);
			  _num   = pack->readShort();
			  _model = pack->readInt();
			  _bulk  = pack->readInt();

			  pack->readString(_company);
			  pack->readBytes(_s_contact,sizeof(_s_contact));
			  pack->readBytes(_sphone,sizeof(_sphone));
			  pack->readBytes(_s_submit_time,sizeof(_s_submit_time));
			  pack->readString(_s_address);

			  _s_lon = pack->readInt();
			  _s_lat = pack->readInt();

			  pack->readBytes(_r_contact,sizeof(_r_contact));
			  pack->readBytes(_rphone,sizeof(_rphone));
			  pack->readBytes(_s_arrival_time,sizeof(_s_arrival_time));

			  pack->readString(_r_address);

			  _r_lon  = pack->readInt();
			  _r_lat  = pack->readInt();
			  _status = pack->readByte();

			  return true;
			}

			void Pack(CPacker *pack) {
			  pack->writeBytes(_sid,sizeof(_sid));
			  pack->writeBytes(_dsid,sizeof(_dsid));
			  pack->writeString(_stype);
			  pack->writeShort(_num);
			  pack->writeInt(_model);
			  pack->writeInt(_bulk);

			  pack->writeString(_company);
			  pack->writeBytes(_s_contact,sizeof(_s_contact));
			  pack->writeBytes(_sphone,sizeof(_sphone));
			  pack->writeBytes(_s_submit_time,sizeof(_s_submit_time));
			  pack->writeString(_s_address);
			  pack->writeInt(_s_lon);
			  pack->writeInt(_s_lat);

			  pack->writeBytes(_r_contact,sizeof(_r_contact));
			  pack->writeBytes(_rphone,sizeof(_rphone));
			  pack->writeBytes(_s_arrival_time,sizeof(_s_arrival_time));

			  pack->writeString(_r_address);

			  pack->writeInt(_r_lon);
			  pack->writeInt(_r_lat);
			  pack->writeByte(_status);

			}
	public:
			  uint8_t   _sid[20];//String 陆运流水号
			  uint8_t   _dsid[20];//订单号
			  CQString  _stype;//货物类型
			  uint16_t  _num;//件数
			  uint32_t  _model;//订单重量(吨)
			  uint32_t  _bulk;//订单体积(立方米)
			  CQString  _company;//发货单位
			  uint8_t   _s_contact[12];//提货联系人
			  uint8_t   _sphone[6];//提货手机号
			  uint8_t   _s_submit_time[6];//提货时间
			  CQString  _s_address;//提货地址
			  uint32_t  _s_lon;//提货经度
			  uint32_t  _s_lat;//提货纬度
			  uint8_t   _r_contact[12];//收货联系人
			  uint8_t   _rphone[6];//收货手机号
			  uint8_t   _s_arrival_time[6];//要求到达时间
			  CQString  _r_address;//收货地址
			  uint32_t  _r_lon;//收货经度
			  uint32_t  _r_lat;//收货纬度
			  uint8_t   _status;//订单状态(0.待提货 1.待发运)
	};

	//订单详细查询列表
	class CQueryOrderFormList : public share::Ref
	{
	public:
		CQueryOrderFormList( )
		{
		}
		;
		~CQueryOrderFormList( )
		{
		}
		;

		void Pack( CPacker *pack )
		{
			pack->writeBytes( _sid, sizeof ( _sid ) );
			pack->writeBytes( _order_form_sid, sizeof ( _order_form_sid ) );
		}

		bool UnPack( CPacker *pack )
		{
			pack->readBytes( _sid, sizeof ( _sid ) );
			pack->readBytes( _order_form_sid, sizeof ( _order_form_sid ) );
			return true;
		}
	public:
		unsigned char _sid[20]; //String 运单号
		unsigned char _order_form_sid[20]; //订单号
	};

	class CQueryOrderFromInfoReq : public IPacket // 1026 订单详细查询
	{

	public:
		CQueryOrderFromInfoReq( )
		{
			_header._type = UP_QUERY_ORDER_FORM_INFO_REQ;
		}

		~CQueryOrderFromInfoReq( )
		{

		}

		void Body( CPacker *pack )
		{
			pack->writeBytes( _sid, sizeof ( _sid ) );
			pack->writeBytes( _order_form_sid, sizeof ( _order_form_sid ) );
		}

		bool UnBody( CPacker *pack )
		{

			pack->readBytes( _sid, sizeof ( _sid ) );
			pack->readBytes( _order_form_sid, sizeof ( _order_form_sid ) );
			return true;
		}
	public:
		uint8_t _sid[20]; //String 运单号
		uint8_t _order_form_sid[20]; //订单号
	};

	class CQueryOrderFromInfoRsp : public IPacket //8026 订单详细查询响应
	{
	public:
		CQueryOrderFromInfoRsp( uint32_t seq = 0 )
		{
			_header._type = UP_QUERY_ORDER_FORM_INFO_RSP;
			_header._seq = seq;
		}

		~CQueryOrderFromInfoRsp( )
		{
			if ( lpOrderList != NULL ) {
				delete lpOrderList;
				lpOrderList = NULL;
			}
		}

		void Body( CPacker *pack )
		{
			lpOrderList->Pack( pack );
		}

		bool UnBody( CPacker *pack )
		{
			lpOrderList = new COrderList;
			if ( ! lpOrderList->UnPack( pack ) ) {
				delete lpOrderList;
				return false;
			}
			return true;
		}
	public:
		COrderList *lpOrderList;
	};

	class CUpOrderFromInfoReq : public IPacket //0x1027   上传货运订单状态
	{

	public:
		CUpOrderFromInfoReq( )
		{

			_header._type = UP_ORDER_FORM_INFO_REQ;

		}
		~CUpOrderFromInfoReq( )
		{

		}

		void Body( CPacker *pack )
		{

			pack->writeBytes( _sid, sizeof ( _sid ) );
			pack->writeBytes( _order_form, sizeof ( _order_form ) );
			pack->writeByte( _action );
			pack->writeByte( _status );
			pack->writeInt( _lon );
			pack->writeInt( _lat );
		}

		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _sid, sizeof ( _sid ) );
			pack->readBytes( _order_form, sizeof ( _order_form ) );

			_action = pack->readByte();
			_status = pack->readByte();
			_lon = pack->readInt();
			_lat = pack->readInt();

			return true;
		}
	public:
		uint8_t  _sid[20]; //String 陆运流水号
		uint8_t  _order_form[20]; //String 订单单号
		uint8_t  _action; //调度单动作(0.提货 1.发运 2.完好签收 3.异常签收 4.异常)
		uint8_t  _status; //状态(0.正常 1.异常)
		uint32_t _lon; //经度
		uint32_t _lat; //纬度
	};

	class CTransportFormInfoReq : public IPacket //0x1028   上传运单状态
	{
	public:
		CTransportFormInfoReq( )
		{
			_header._type = UP_TRANSPORT_FORM_INFO_REQ;

		}
		~CTransportFormInfoReq( )
		{

		}

		void Body( CPacker *pack )
		{
			pack->writeBytes( _sid, sizeof ( _sid ) );
			pack->writeByte( _action );
			pack->writeByte( _status );
			pack->writeInt( _lon );
			pack->writeInt( _lat );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _sid, sizeof ( _sid ) );

			_action = pack->readByte();
			_status = pack->readByte();
			_lon = pack->readInt();
			_lat = pack->readInt();

			return true;
		}
	public:
		uint8_t  _sid[20]; //String 陆运流水号
		uint8_t  _action; //调度单动作(0.提货 1.发运 2.完好签收 3.异常签收 4.异常)
		uint8_t  _status; //状态(0.正常 1.异常)
		uint32_t _lon; //经度
		uint32_t _lat; //纬度
	};

	class CSend_Order_Form_Info_Req:public IPacket //0x102E 推送详细订单
	{

	public:
		CSend_Order_Form_Info_Req(){
			_header._type = SEND_ORDER_FORM_REQ;
			lpOrderList   = NULL;
		}

		~CSend_Order_Form_Info_Req(){

		  if (lpOrderList != NULL){
			  delete lpOrderList;
			  lpOrderList = NULL;
		  }
		}
		void Body(CPacker *pack) {
			lpOrderList->Pack(pack);
		}

		bool UnBody(CPacker *pack) {
			lpOrderList = new COrderList;
			if (!lpOrderList->UnPack(pack)) {
			  delete lpOrderList;
			  return false;
			}
			return true;
		}

	public:
		 COrderList *lpOrderList;
	};

	//下发运单信息
	class CSend_Transport_Order_Form_Info_Req:public IPacket  //0x1025 下发运单信息
	{
		   typedef std::vector<COrderList*> COrderListVec;
	public:
			CSend_Transport_Order_Form_Info_Req(){
				_header._type = SEND_TRANSPORT_ORDER_FORM_REQ;
			}

			~CSend_Transport_Order_Form_Info_Req(){
				Clear();
			}
			void Body(CPacker *pack) {

				pack->writeBytes(_sid,sizeof(_sid));
				pack->writeInt(_sarea);
				pack->writeInt(_darea);
				pack->writeByte(_status);
				pack->writeBytes(_send_time,sizeof(_send_time));
				pack->writeByte(_num);

				// 打包所有数据体
				for (int i = 0; i < _num; ++i) {
				   _vec[i]->Pack(pack);
				}
			}
			bool UnBody(CPacker *pack) {

				pack->readBytes(_sid,sizeof(_sid));

				_sarea  = pack->readInt();
				_darea  = pack->readInt();
				_status = pack->readByte();
				pack->readBytes(_send_time,sizeof(_send_time));
				_num    = pack->readByte();

				for (int i = 0; i < _num; ++i) {
					COrderList *info = new COrderList;
				   if (!info->UnPack(pack)) {
					  delete info;
					  continue;
				   }
				   info->AddRef();
				   _vec.push_back(info);
				}
				return  true;
			}
	private:
		// 清理所有对象数据
		void Clear(void) {
			if (_vec.empty())
				return;
			for (int i = 0; i < (int) _vec.size(); ++i) {
				_vec[i]->Release();
			}
			_vec.clear();
		}
	public:
			uint8_t    _sid[20];//运单号
			uint32_t   _sarea;//出发地
			uint32_t   _darea;//目的地
			uint8_t    _status;//运单状态(0.待提货 1.待发运)
			uint8_t    _send_time[6];//发货时间
			uint8_t    _num;//订单数

			COrderListVec _vec;
	};

	class CPlatFormCommonRsp : public IPacket //8000   平台给终端的通用应答
	{
	public:
		CPlatFormCommonRsp( uint32_t seq = 0 )
		{
			_header._type = PLATFORM_COMMON_RSP;
			_header._seq  = seq;
		}
		~CPlatFormCommonRsp( )
		{

		}
		bool UnBody( CPacker *pack )
		{
			_type = pack->readShort();
			_result = pack->readByte();
			return true;
		}
		void Body( CPacker *pack )
		{
			pack->writeShort( _type );
			pack->writeByte( _result );
		}
	public:
		uint16_t _type; //报文类型
		uint8_t _result; //结果
	};
	class CTerminalCommonRsp : public IPacket //0x1000 终端通用应答
	{
	public:
		CTerminalCommonRsp( uint32_t seq = 0 )
		{

			_header._type = TERMINAL_COMMON_RSP;
			_header._seq  = seq;
		}
		~CTerminalCommonRsp( )
		{

		}
		bool UnBody( CPacker *pack )
		{
			_type = pack->readShort();
			_result = pack->readByte();
			return true;
		}

		void Body( CPacker *pack )
		{
			pack->writeShort( _type );
			pack->writeByte( _result );
		}
	public:
		uint16_t _type; //报文类型
		uint8_t _result; //结果
	};

	class CTruckUnPackMgr : public IUnPackMgr
	{
	public:
		CTruckUnPackMgr(){}
		~CTruckUnPackMgr(){}

		// 实现数据解包接口方法
		IPacket * UnPack( unsigned short msgtype, CPacker &pack )
		{
			IPacket *msg = NULL ;
			switch( msgtype )
			{
			case SEND_TEXT_MSG_REQ:   // 文本信息下发
				msg = UnPacket<CSendTextMsgReq>( pack, "CSendTextMsgReq" ) ;
				break;
			case SEND_TEXT_MSG_RSP:   // 文本信息响应
				msg = UnPacket<CSendTextMsgRsp>( pack, "CSendTextMsgRsp" ) ;
				break;
			case QUERY_CARDATA_REQ:   // 查询配货信息
				msg = UnPacket<CQueryCarDataReq>( pack, "CQueryCarDataReq" ) ;
				break ;
			case QUERY_CARDATA_RSP:
				msg = UnPacket<CQueryCarDataRsp>( pack, "CQueryCarDataRsp" ) ;
				break ;
			case UPLOAD_DATAINFO_REQ: // 自动上报配货信息
				msg = UnPacket<CAutoDataScheduleReq>(pack,"CAutoDataScheduleReq");
				break ;
			case UPLOAD_DATAINFO_RSP:// 0x8022
				msg = UnPacket<CAutoDataScheduleRsp>(pack,"CAutoDataScheduleRsp");
				break;
			case SUBSCRIBE_REQ: //0x1050    订阅管理
				msg = UnPacket<CSubscrbeReq>(pack,"CSubscrbeReq");
				break ;
			case SUBSCRIBE_RSP:// 0x8050
				msg = UnPacket<CSubscrbeRsp>(pack,"CSubscrbeRsp");
				break;
			case QUERY_INFO_REQ : //0x1090    订阅消息查询
				msg = UnPacket<CQueryInfoReq>(pack,"CQueryInfoReq");
				break ;
			case QUERY_INFO_RSP:// 0x8090
				msg = UnPacket<CQueryInfoRsp>(pack,"CQueryInfoRsp");
				break;
			case UP_REPORTERROR_REQ:  // 上传车辆故障
				msg = UnPacket<CErrorScheduleReq>(pack,"CErrorScheduleReq");
				break;
			case UP_REPORTERROR_RSP: //0x8070
				msg = UnPacket<CErrorScheduleRsp>(pack,"CErrorScheduleRsp");
				break;
			// 下面部分为甩挂业务的处理
			case SEND_SCHEDULE_REQ:  // 0x1040   // 下发调度单请求
				msg = UnPacket<CSendScheduleReq>( pack, "CSendScheduleReq" ) ;
				break ;
			case SEND_SCHEDULE_RSP:	 // 0x8040	 // 下发调度单响应
				msg = UnPacket<CSendScheduleRsp>( pack, "CSendScheduleRsp" ) ;
				break ;
			case RESULT_SCHEDULE_REQ: // 0x1045 // 主动上报下发调度请求应答
				msg = UnPacket<CResultScheduleReq>( pack, "CResultScheduleReq" ) ;
				break ;
			case RESULT_SCHEDULE_RSP: // 0x8045 // 主动上报下发调度单的响应
				msg = UnPacket<CResultScheduleRsp>( pack, "CResultScheduleRsp" ) ;
				break ;
			case QUERY_SCHEDULE_REQ:	// 0x1041	 // 查询调度单请求
				msg = UnPacket<CQueryScheduleReq>( pack, "CQueryScheduleReq" ) ;
				break ;
			case QUERY_SCHEDULE_RSP:	// 0x8041	 // 查询调度单响应
				msg = UnPacket<CQueryScheduleRsp>( pack, "CQueryScheduleRsp" ) ;
				break ;
			case UPLOAD_SCHEDULE_REQ: 	// 0x1042	 // 上传调度单
				msg = UnPacket<CUploadScheduleReq>( pack, "CUploadScheduleReq" ) ;
				break ;
			case UPLOAD_SCHEDULE_RSP:	// 0x8042	 // 上传调度单响应
				msg = UnPacket<CUploadScheduleRsp>( pack, "CUploadScheduleRsp" ) ;
				break ;
			case STATE_SCHEDULE_REQ: 	//	0x1043	 // 上报调度单状态
				msg = UnPacket<CStateScheduleReq>( pack, "CStateScheduleReq" ) ;
				break ;
			case STATE_SCHEDULE_RSP:	//	0x8043	 // 上报调度单响应
				msg = UnPacket<CStateScheduleRsp>( pack, "CStateScheduleRsp" ) ;
				break ;
			case ALARM_SCHEDULE_REQ: 	//	0x1044	 // 车挂告警
				msg = UnPacket<CAlarmScheduleReq>(pack, "CAlarmScheduleReq");
				break ;
			case ALARM_SCHEDULE_RSP:	//	0x8044	 //
				msg = UnPacket<CAlarmScheduleRsp>(pack, "CAlarmScheduleRsp");
				break ;
			case UP_MSGDATA_REQ: //0x1060 透传
				msg = UnPacket<CUpMsgDataScheduleReq >( pack, "CUpMsgDataScheduleReq");
				break;
			case UP_MSGDATA_RSP:// 0x8060
				msg = UnPacket<CUpMsgDataScheduleRsp >( pack, "CUpMsgDataScheduleRsp");
				break;
			case SEND_TEAMMEDIA_REQ://0x1019     车队聊天
				msg = UnPacket<CSendTeamMediaReq>(pack,"CSendTeamMediaReq");
				break;
			case SEND_TEAMMEDIA_RSP://0x8019  车队聊天响应
				msg = UnPacket<CSendTeamMediaRsp>(pack,"CSendTeamMediaRsp");
				break;
			case SEND_MEDIADATA_REQ: //0x1018    车友聊天
				msg = UnPacket<CSendMediaDataReq>(pack,"CSendMediaDataReq");
				break;
			case SEND_MEDIADATA_RSP: //0x8018 车友聊天响应
				msg = UnPacket<CSendMediaDataRsp>(pack,"CSendMediaDataRsp");
				break;
			case INFO_PRIMCAR_REQ: //0x1017 接收头车信息
				msg = UnPacket<CInfoPriMcarReq>(pack,"CInfoPriMcarReq");
				break;
			case INFO_PRIMCAR_RSP://0x8017 接收头车信息响应
				msg = UnPacket<CInfoPriMcarRsp>(pack,"CInfoPriMcarRsp");
				break;
			case SET_PRIMCAR_REQ://0x1016 设置本车为头车
				msg = UnPacket<CSetPriMcarReq>(pack,"CSetPriMcarReq");
				break;
			case SET_PRIMCAR_RSP://0x8016 设置本车为头车响应
				msg = UnPacket<CSetPriMcarRsp>(pack,"CSetPriMcarRsp");
				break;
			case INVITE_NUMBER_REQ://0x1015 车队成员邀请
				msg = UnPacket<CInviteNumberReq>(pack,"CInviteNumberReq");
				break;
			case INVITE_NUMBER_RSP://0x8015 车队成员邀请响应
				msg = UnPacket<CInviteNumberRsp>(pack,"CInviteNumberRsp");
				break;
			case ADD_CARTEAM_REQ: //0x1014 建立车队
				msg = UnPacket<CAddCarTeamReq>(pack,"CAddCarTeamReq");
				break;
			case ADD_CARTEAM_RSP://0x8014  建立车队响应
				msg = UnPacket<CAddCarTeamRsp>(pack,"CAddCarTeamRsp");
				break;
			case GET_FRIENDLIST_REQ://获取车友列表
				msg = UnPacket<CGetFriendListReq>(pack,"CGetFriendListReq");
				break;
			case GET_FRIENDLIST_RSP://获取车友列表响应
				msg = UnPacket<CGetFriendListRsp>(pack,"CGetFriendListRsp");
				break;
			case INVITE_FRIEND_REQ://邀请车友
				msg = UnPacket<CInviteFriendReq>(pack,"CInviteFriendReq");
				break;
			case INVITE_FRIEND_RSP://邀请车友响应
				msg = UnPacket<CInviteFriendRsp>(pack,"CInviteFriendRsp");
				break;
			case ADD_FRIEND_REQ://添加好友
				msg = UnPacket<CAddFriendsReq>(pack,"CAddFriendsReq");
				break;
			case ADD_FRIEND_RSP://添加好友响应
				msg = UnPacket<CAddFriendsRsp>(pack,"CAddFriendsRsp");
				break;
			case QUERY_FRIENDS_REQ://查找附近的好友
				msg = UnPacket<CQueryFriendsReq>(pack,"CQueryFriendsReq");
				break;
			case QUERY_FRIENDS_RSP://查找附近的好友响应
				msg = UnPacket<CQueryFriendsRsp>(pack,"CQueryFriendsRsp");
				break;
			case DRIVER_LOGOUT_REQ://司机注销
				msg = UnPacket<CDriverLoginOutReq>(pack,"CDriverLoginOutReq");
				break;
			case DRIVER_LOGOUT_RSP://司机注销响应
				msg = UnPacket<CDriverLoginOutRsp>(pack,"CDriverLoginOutRsp");
				break;
			case DRIVER_LOGIN_REQ://司机登录验证
				msg = UnPacket<CDriverLoginReq>(pack,"CDriverLoginReq");
				break;
			case DRIVER_LOGIN_RSP://司机登录验证响应
				msg = UnPacket<CDriverLoginRsp>(pack,"CDriverLoginRsp");
				break;
			case UP_CARDATA_INFO_REQ://上传配货信息
				msg = UnPacket<CUpCarDataInfoReq>(pack,"CUpCarDataInfoReq");
				break;
			case UP_QUERY_ORDER_FORM_INFO_REQ://订单详细查询
				msg = UnPacket<CQueryOrderFromInfoReq>(pack,"CQueryOrderFromInfoReq");
				break;
			case UP_ORDER_FORM_INFO_REQ://上传货运订单状态
				msg = UnPacket<CUpOrderFromInfoReq>(pack,"CUpOrderFromInfoReq");
				break;
			case UP_TRANSPORT_FORM_INFO_REQ://上传运单状态
				msg = UnPacket<CTransportFormInfoReq>(pack,"CTransportFormInfoReq");
				break;
			case TERMINAL_COMMON_RSP://终端通用应答
				msg = UnPacket<CTerminalCommonRsp>(pack,"CTerminalCommonRsp");
				break;

			default:
				break ;
			}
			return msg;
		}
	};
};

#pragma pack()

#endif /* HEADER_H_ */
