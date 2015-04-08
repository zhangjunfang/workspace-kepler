/*
 * header.h
 *
 *  Created on: 2012-5-31
 *      Author: humingqing
 *   汽车后服务解析对象
 */

#ifndef __CAR_SERVICE_PACK_H__
#define __CAR_SERVICE_PACK_H__

#include <msgpack.h>
#include <packfactory.h>

#pragma pack(1)

namespace CarService {
	//车后服务
	#define TERMINAL_COMMON_RSP                   0x1000   //终端通用回复
	#define PLATFORM_COMMON_RSP                   0x8000   //平台通用回复

	#define UP_LOGIN_INFO_REQ                     0x1001   //用户登录
	#define UP_LOGIN_INFO_RSP                     0x8001   //用户登录响应

	#define UP_QUERY_BALLANCE_LIST_REQ            0x1002   //联名卡余额查询
	#define UP_QUERY_BALLANCE_LIST_RSP            0x8002   //联名卡余额查询响应

	#define UP_QUERY_STORE_LIST_REQ               0x1003   //查询门店
	#define UP_QUERY_STORE_LIST_RSP               0x8003   //查询门店响应

	#define UP_QUERY_VIEW_STORE_INFO_REQ          0x1004   //查询门店详情
	#define UP_QUERY_VIEW_STORE_INFO_RSP          0x8004   //查询门店详情响应

	#define UP_QUERY_DISCOUNT_LIST_REQ            0x1005   //新版本查询优惠信息
	#define UP_QUERY_DISCOUNT_LIST_RSP            0x8005   //新版本查询优惠信息响应

	#define UP_VIEW_DISCOUNT_INFO_REQ             0x1006   //新版本查询优惠信息详细列表
	#define UP_VIEW_DISCOUNT_INFO_RSP             0x8006   //新版本查询优惠信息详细列表响应

	#define UP_QUERY_TRADE_LIST_REQ               0x1007   //历史交易记录查询
	#define UP_QUERY_TRADE_LIST_RSP               0x8007   //历史交易记录查询响应

	#define UP_QUERY_FAVORITE_LIST_REQ            0x1008   //查询收藏列表
	#define UP_QUERY_FAVORITE_LIST_RSP            0x8008   //查询收藏列表响应

	#define UP_VIEW_FAVORITE_INFO_REQ             0x1009   //查询收藏列表详情
	#define UP_VIEW_FAVORITE_INFO_RSP             0x8009   //查询收藏列表详情响应

	#define UP_ADD_FAVORITE_REQ                   0x100A   //添加收藏请求

	#define UP_DEL_FAVORITE_REQ                   0x100B   //删除收藏请求

	#define UP_GET_DESTINATION_REQ                0x100C   //获取目的地

	#define SEND_DETAIL_DISCOUNT_INFO_REQ         0x102B //下发优惠信息

	#define UP_DISCOUNT_INFO_REQ                  0x1029 //查询打折优惠信息
	#define UP_DISCOUNT_INFO_RSP                  0x8029 //查询打折优惠信息响应

	#define UP_DETAIL_DISCOUNT_INFO_REQ           0x102A //查询具体打折优惠信息
	#define UP_DETAIL_DISCOUNT_INFO_RSP           0x802A //查询具体打折优惠信息响应

	#define UP_UNION_BUSINESS_INFO_REQ            0x102C //查询联盟商家信息
	#define UP_UNION_BUSINESS_INFO_RSP            0x802C //查询联盟商家信息响应

	#define UP_DETAIL_UNION_BUSINESS_INFO_REQ     0x102D //查询具体的联盟商家信息
	#define UP_DETAIL_UNION_BUSINESS_INFO_RSP     0x802D //查询具体的联盟商家响应

	#define SEND_DESTINATION_INFO_REQ             0x100D //下发目的地

	/*公共类*/
	class CCOMMON_REQ
	{
	public:
		CCOMMON_REQ( )
		{
			memset( _usercode, 0, sizeof ( _usercode ) );
			memset( _verifyCode, 0, sizeof ( _verifyCode ) );
		}
		virtual ~CCOMMON_REQ(){};

	public:
		unsigned char _usercode[32]; //用户编码
		unsigned char _verifyCode[30]; //身份认证识别码
	};

	//用户登录
	class CLoginInfoReq : public IPacket //0x1001 用户登录
	{
	public:
		CLoginInfoReq( )
		{
			_header._type = UP_LOGIN_INFO_REQ;
		}

		~CLoginInfoReq( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _phone, sizeof ( _phone ) );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _phone, sizeof ( _phone ) );
			return true;
		}
	public:
		unsigned char _phone[32]; //手机号码
	};
	/*用户登录响应*/
	class CLoginInfoRsp : public CCOMMON_REQ , public IPacket//0x8001 用户登录响应
	{
	public:
		CLoginInfoRsp( uint32_t seq = 0 )
		{
			_header._type = UP_LOGIN_INFO_RSP;
			_header._seq = seq;
		}

		~CLoginInfoRsp( )
		{
		}
		;

		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );

			return true;
		}

		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );
		}
	};

	//联名卡余额查询
	class CQuery_Ballance_List_Req : public CCOMMON_REQ , public IPacket  //0x1002
	{
	public:
		CQuery_Ballance_List_Req( )
		{
			_header._type = UP_QUERY_BALLANCE_LIST_REQ;
		}

		~CQuery_Ballance_List_Req( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );
			return true;
		}
	};

	//联名卡余额查询响应
	class CQuery_Ballance_List_Rsp : public IPacket // 0x8002
	{
	public:
		CQuery_Ballance_List_Rsp( uint32_t seq = 0 )
		{
			_header._type = UP_QUERY_BALLANCE_LIST_RSP;
			_header._seq = seq;
		}
		~CQuery_Ballance_List_Rsp( )
		{
		}
		;

		void Body( CPacker *pack )
		{
			pack->writeBytes( _car_num, sizeof ( _car_num ) );
			pack->writeBytes( _vehicle_num, sizeof ( _vehicle_num ) );
			pack->writeInt( _balance );
			pack->writeByte( _status );

		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _car_num, sizeof ( _car_num ) );
			pack->readBytes( _vehicle_num, sizeof ( _vehicle_num ) );

			_balance = pack->readInt();
			_status = pack->readByte();
			return true;
		}
	public:
		uint8_t _car_num[20];  //联名卡号
		uint8_t _vehicle_num[12];  //车牌号
		uint32_t _balance;  //联名卡余额
		uint8_t _status;  //联名卡状态
	};
	//查询门店
	class CQuery_Store_List_Req : public CCOMMON_REQ , public IPacket //0x1003
	{
	public:
		CQuery_Store_List_Req( )
		{
			_header._type = UP_QUERY_STORE_LIST_REQ;
		}
		~CQuery_Store_List_Req( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );
			pack->writeInt( _lon );
			pack->writeInt( _lat );
			pack->writeByte( _scope );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );

			_lon = pack->readInt();
			_lat = pack->readInt();
			_scope = pack->readByte();

			return true;
		}
	public:
		uint32_t _lon; //经度
		uint32_t _lat; //纬度
		uint8_t _scope; //查询范围(0:15；1:30；2:50；255:预留)单位:公里
	};

	/*门店列表*/
	class CStore_List : public share::Ref
	{
	public:
		CStore_List( )
		{
			memset( _storeCode, 0, sizeof ( _storeCode ) );
			memset( _phone, 0, sizeof ( _phone ) );

			_type = _lon = _lat = 0;
		}
		~CStore_List( )
		{
		}
		;
		void Pack( CPacker *pack )
		{
			pack->writeByte( _type );
			pack->writeBytes( _storeCode, sizeof ( _storeCode ) );
			pack->writeString( _storeName );
			pack->writeString( _address );
			pack->writeBytes( _phone, sizeof ( _phone ) );
			pack->writeInt( _lon );
			pack->writeInt( _lat );
		}
		bool UnPack( CPacker *pack )
		{
			_type = pack->readByte();
			pack->readBytes( _storeCode, sizeof ( _storeCode ) );
			pack->readString( _storeName );
			pack->readString( _address );
			pack->readBytes( _phone, sizeof ( _phone ) );
			_lon = pack->readInt();
			_lat = pack->readInt();
			return true;
		}
	public:
		uint8_t _type; //1.维修、2.救援、3.加油、4.保养、5.餐饮、6.住宿 0.全部
		uint8_t _storeCode[20]; //门店编码
		CQString _storeName; //门店名称
		CQString _address; //详细地址
		uint8_t _phone[20]; //门店电话
		uint32_t _lon; //经度
		uint32_t _lat; //纬度
	};

	class CQuery_Store_List_Rsp : public IPacket // 0x8003 //查询门店响应
	{
		typedef std::vector< CStore_List * > CStore_List_VEC;
	public:
		CQuery_Store_List_Rsp( uint32_t seq = 0 )
		{
			_header._type = UP_QUERY_STORE_LIST_RSP;
			_header._seq = seq;
		}
		~CQuery_Store_List_Rsp( )
		{
			Clear();
		}
		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();

			if ( _num == 0 ) return false;
			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				CStore_List *info = new CStore_List;
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
		unsigned char _num; //查询门店个数
		CStore_List_VEC _vec;
	};
	//查询门店详情
	class CView_Store_Info_Req : public CCOMMON_REQ , public IPacket //0x1004
	{
	public:
		CView_Store_Info_Req( )
		{
			_header._type = UP_QUERY_VIEW_STORE_INFO_REQ;
		}
		~CView_Store_Info_Req( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );
			pack->writeBytes( _storecode, sizeof ( _storecode ) );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );

			pack->readBytes( _storecode, sizeof ( _storecode ) );
			return true;
		}
	public:
		uint8_t _storecode[20]; //门店编码

	};

	//查询门店详情响应
	class CView_Store_List_Rsp : public IPacket // 0x8004 //查询门店详情响应
	{
	public:
		CView_Store_List_Rsp( uint32_t seq = 0 )
		{
			_header._type = UP_QUERY_VIEW_STORE_INFO_RSP;
			_header._seq = seq;
		}
		~CView_Store_List_Rsp( )
		{
		}
		;

		void Body( CPacker *pack )
		{
			pack->writeString( _allianceName );
			pack->writeByte( _type );
			pack->writeBytes( _storeCode, sizeof ( _storeCode ) );
			pack->writeString( _storeName );
			pack->writeString( _address );
			pack->writeBytes( _phone, sizeof ( _phone ) );
			pack->writeString( _desc );
			pack->writeInt( _lon );
			pack->writeInt( _lat );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readString( _allianceName );
			_type = pack->readByte();
			pack->readBytes( _storeCode, sizeof ( _storeCode ) );
			pack->readString( _storeName );
			pack->readString( _address );
			pack->readBytes( _phone, sizeof ( _phone ) );
			pack->readString( _desc );
			_lon = pack->readInt();
			_lat = pack->readInt();
			return true;
		}
	public:
		CQString _allianceName; //商家名称
		uint8_t _type; //1.维修、2.救援、3.加油、4.保养、5.餐饮、6.住宿 0.全部(按位操作)
		uint8_t _storeCode[20]; //门店编码
		CQString _storeName; //门店名称
		CQString _address; //详细地址
		uint8_t _phone[20]; //电话
		CQString _desc; //门店详情
		uint32_t _lon; //门店经度
		uint32_t _lat; //门店纬度
	};

	//新版本查询优惠信息
	class CQuery_Discount_List_Req : public CCOMMON_REQ , public IPacket //0x1005
	{
	public:
		CQuery_Discount_List_Req( )
		{
			_header._type = UP_QUERY_DISCOUNT_LIST_REQ;
		}
		~CQuery_Discount_List_Req( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );

			pack->writeInt( _lon );
			pack->writeInt( _lat );
			pack->writeByte( _type );
			pack->writeInt( _offset );
			pack->writeByte( _count );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );

			_lon = pack->readInt();
			_lat = pack->readInt();
			_type = pack->readByte();
			_offset = pack->readInt();
			_count = pack->readByte();

			return true;
		}
	public:
		uint32_t _lon; //车辆经度
		uint32_t _lat; //车辆纬度
		uint8_t _type; //类型
		uint8_t _scope; //查询范围
		uint32_t _offset; //读取开始位置，默认从0开始
		uint8_t _count; //一页记录条数
	};
	//新版本优惠信息列表
	class CDiscount_List_Info : public share::Ref
	{
	public:
		CDiscount_List_Info( )
		{
			memset( _discountCode, 0, sizeof ( _discountCode ) );
		}
		~CDiscount_List_Info( )
		{
		}
		;

		void Pack( CPacker *pack )
		{
			pack->writeBytes( _discountCode, sizeof ( _discountCode ) );
			pack->writeString( _title );
		}
		bool UnPack( CPacker *pack )
		{
			pack->readBytes( _discountCode, sizeof ( _discountCode ) );
			pack->readString( _title );

			return true;
		}
	public:
		uint8_t _discountCode[20]; //优惠信息编码
		CQString _title; //标题
	};
	/*优惠门店列表*/
	class CDiscount_Store_List_Info : public share::Ref
	{
		typedef std::vector< CDiscount_List_Info * > CDiscount_List_Info_VEC;
	public:
		CDiscount_Store_List_Info( )
		{
			memset( _storeCode, 0, sizeof ( _storeCode ) );
			memset( _phone, 0, sizeof ( _phone ) );

			_lon = _lat = _discount_num = 0;
		}
		~CDiscount_Store_List_Info( )
		{
			Clear();
		}

		void Pack( CPacker *pack )
		{
			pack->writeBytes( _storeCode, sizeof ( _storeCode ) );
			pack->writeString( _storeName );
			pack->writeBytes( _phone, sizeof ( _phone ) );
			pack->writeString( _address );
			pack->writeInt( _lon );
			pack->writeInt( _lat );
			pack->writeByte( _discount_num );

			if ( _discount_num == 0 ) return;

			for ( int i = 0 ; i < ( int ) _discount_num ; ++ i ) {
				_vec_discount[i]->Pack( pack );
			}
		}
		bool UnPack( CPacker *pack )
		{
			pack->readBytes( _storeCode, sizeof ( _storeCode ) );
			pack->readString( _storeName );
			pack->readBytes( _phone, sizeof ( _phone ) );
			pack->readString( _address );

			_lon = pack->readInt();
			_lat = pack->readInt();
			_discount_num = pack->readByte();

			if ( _discount_num == 0 ) return false;

			for ( int i = 0 ; i < _discount_num ; ++ i ) {
				CDiscount_List_Info *info = new CDiscount_List_Info;
				if ( ! info->UnPack( pack ) ) {
					delete info;
					continue;
				}
				info->AddRef();
				_vec_discount.push_back( info );
			}
			return true;
		}
	private:
		void Clear( void )
		{
			if ( _vec_discount.empty() ) return;
			for ( int i = 0 ; i < ( int ) _vec_discount.size() ; ++ i ) {
				_vec_discount[i]->Release();
			}
			_vec_discount.clear();
		}
	public:
		uint8_t _storeCode[20]; //门店编码
		CQString _storeName; //门店名称
		uint8_t _phone[20]; //门店电话
		CQString _address; //详细地址
		uint32_t _lon; //经度
		uint32_t _lat; //纬度
		uint8_t _discount_num; //优惠总数

		CDiscount_List_Info_VEC _vec_discount;
	};
	//新版本查询优惠信息列表响应
	class CQuery_Discount_List_Rsp : public IPacket // 0x8005
	{
		typedef std::vector< CDiscount_Store_List_Info * > CDiscount_Store_List_Info_VEC;
	public:
		CQuery_Discount_List_Rsp( uint32_t seq = 0 )
		{
			_header._type = UP_QUERY_DISCOUNT_LIST_RSP;
			_header._seq = seq;
		}
		~CQuery_Discount_List_Rsp( )
		{
			Clear();
		}
		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();

			if ( _num == 0 ) return false;
			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				CDiscount_Store_List_Info *info = new CDiscount_Store_List_Info;
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
		uint8_t _num; //优惠信息总数
		CDiscount_Store_List_Info_VEC _vec;
	};

	//新版本查询优惠信息详细列表
	class CView_Discount_Info_Req : public CCOMMON_REQ , public IPacket //0x1006
	{
	public:
		CView_Discount_Info_Req( )
		{
			_header._type = UP_VIEW_DISCOUNT_INFO_REQ;
		}
		~CView_Discount_Info_Req( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );
			pack->writeBytes( _discountCode, sizeof(_discountCode) );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );
			pack->readBytes( _discountCode, sizeof(_discountCode) );
			return true;
		}
	public:
		 uint8_t  _discountCode[20];//优惠信息编码
	};

	//新版本查询优惠信息详细列表响应
	class CView_Discount_Info_Rsp: public IPacket // 0x8006
	{
	public:
		CView_Discount_Info_Rsp( uint32_t seq = 0 )
		{
			_header._type = UP_VIEW_DISCOUNT_INFO_RSP;
			_header._seq  = seq;
		}
		~CView_Discount_Info_Rsp( ){};

		void Body( CPacker *pack )
		{
			pack->writeString(_title);
			pack->writeString(_content);

			pack->writeBytes(_beginTime,sizeof(_beginTime));
			pack->writeBytes(_endTime,sizeof(_endTime));
		}
		bool UnBody( CPacker *pack )
		{
			pack->readString(_title);
			pack->readString(_content);

			pack->readBytes(_beginTime,sizeof(_beginTime));
			pack->readBytes(_endTime,sizeof(_endTime));

			return true;
		}
	public:
		  CQString  _title;//标题
		  CQString  _content;//内容
		  uint8_t   _beginTime[6];//有效开始时间
		  uint8_t   _endTime[6];//有效结束时间
	};
	//历史交易记录查询
	class CQuery_Trade_List_Req : public CCOMMON_REQ , public IPacket //0x1007
	{
	public:
		CQuery_Trade_List_Req( )
		{
			_header._type = UP_QUERY_TRADE_LIST_REQ;
		}
		~CQuery_Trade_List_Req( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );
			pack->writeByte( _tradeType );

			pack->writeBytes( _beginTime, sizeof ( _beginTime ) );
			pack->writeBytes( _endTime, sizeof ( _endTime ) );

			pack->writeInt( _offset );
			pack->writeByte( _count );

		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );

			_tradeType = pack->readByte();

			pack->readBytes( _beginTime, sizeof ( _beginTime ) );
			pack->readBytes( _endTime, sizeof ( _endTime ) );

			_offset = pack->readInt();
			_count = pack->readInt();
			return true;
		}
	public:
		uint8_t _tradeType; //交易类型（0：全部；1：充值；2：消费）
		uint8_t _beginTime[6]; //有效开始时间
		uint8_t _endTime[6]; //有效截止时间
		uint32_t _offset; //读取开始位置，默认从0开始
		uint8_t _count; //一页记录条数
	};

	//交易记录列表
	class CTrade_List : public share::Ref
	{
	public:
		CTrade_List( )
		{
			memset( _card_num, 0, sizeof ( _card_num ) );
			memset( _tradeTime, 0, sizeof ( _tradeTime ) );

			_type = _money = _tradeState = 0;
		}
		~CTrade_List( )
		{
		}
		;
		void Pack( CPacker *pack )
		{
			pack->writeBytes( _card_num, sizeof ( _card_num ) );
			pack->writeString( _allianceName );
			pack->writeString( _storeName );
			pack->writeByte( _type );
			pack->writeString( _productName );

			pack->writeInt( _money );
			pack->writeBytes( _tradeTime, sizeof ( _tradeTime ) );
			pack->writeByte( _tradeState );
		}
		bool UnPack( CPacker *pack )
		{
			pack->readBytes( _card_num, sizeof ( _card_num ) );
			pack->readString( _allianceName );
			pack->readString( _storeName );
			_type = pack->readByte();
			pack->readString( _productName );

			_money = pack->readInt();

			pack->readBytes( _tradeTime, sizeof ( _tradeTime ) );
			_tradeState = pack->readByte();
			return true;
		}
	public:
		uint8_t _card_num[20]; //联名卡号
		CQString _allianceName; //商户名称
		CQString _storeName; //门店名称
		uint8_t _type; //交易类型
		CQString _productName; //商品名称
		uint32_t _money; //交易金额
		uint8_t _tradeTime[6]; //交易时间（yy-MM-dd hh:mm:ss）
		uint8_t _tradeState; //交易状态
	};

	class CQuery_Trade_List_Rsp : public IPacket // 0x8007 //历史交易记录查询响应
	{
		typedef std::vector< CTrade_List * > CTrade_List_VEC;
	public:
		CQuery_Trade_List_Rsp( uint32_t seq = 0 )
		{
			_header._type = UP_QUERY_TRADE_LIST_RSP;
			_header._seq = seq;
		}
		~CQuery_Trade_List_Rsp( )
		{
			Clear();
		}
		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();

			if ( _num == 0 ) return false;
			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				CTrade_List *info = new CTrade_List;
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
		unsigned char _num; //查询结果个数
		CTrade_List_VEC _vec;
	};
	//查询收藏列表
	class CQuery_Favorite_List_Req : public CCOMMON_REQ , public IPacket //0x1008
	{
	public:
		CQuery_Favorite_List_Req( )
		{
			_header._type = UP_QUERY_FAVORITE_LIST_REQ;
		}
		~CQuery_Favorite_List_Req( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );

			pack->writeByte( _type );
			pack->writeByte( _offset );
			pack->writeByte( _count );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );

			_type = pack->readByte();
			_offset = pack->readByte();
			_count = pack->readByte();
			return true;
		}
	public:
		uint8_t _type; //收藏类型（1：门店；2：优惠信息）
		uint8_t _offset; //读取开始位置，默认从0开始
		uint8_t _count; //一页记录条数
	};

	//收藏信息列表
	class CFavorite_List : public share::Ref
	{
	public:
		CFavorite_List( )
		{
			memset( _id, 0, sizeof ( _id ) );
			memset( _discountCode, 0, sizeof ( _discountCode ) );

			_type = _isDiscount = _discountState = 0;
		}
		~CFavorite_List( )
		{
		}
		;
		void Pack( CPacker *pack )
		{
			pack->writeBytes( _id, sizeof ( _id ) );
			pack->writeString( _storeCode );
			pack->writeBytes( _discountCode, sizeof ( _discountCode ) );
			pack->writeString( _favoriteName );
			pack->writeByte( _type );

			pack->writeByte( _isDiscount );
			pack->writeByte( _discountState );
		}
		bool UnPack( CPacker *pack )
		{
			pack->readBytes( _id, sizeof ( _id ) );
			pack->readString( _storeCode );
			pack->readBytes( _discountCode, sizeof ( _discountCode ) );
			pack->readString( _favoriteName );
			_type = pack->readByte();
			_isDiscount = pack->readByte();
			_discountState = pack->readByte();
			return true;
		}
	public:
		uint8_t _id[32]; //收藏id
		CQString _storeCode; //门店编码
		uint8_t _discountCode[32]; //优惠信息编码
		CQString _favoriteName; //收藏名称（门店名称、优惠信息标题）
		uint8_t _type; //收藏类型（1：门店；2：优惠信息
		uint8_t _isDiscount; //是否有优惠，在收藏类型为1时
		uint8_t _discountState; //优惠信息状态（0：正常；1：已过期；2：已失效）
	};

	class CQuery_Favorite_List_Rsp : public IPacket // 0x8008 //查询收藏列表响应
	{
	public:
		typedef std::vector< CFavorite_List * > CFavorite_List_VEC;
	public:
		CQuery_Favorite_List_Rsp( uint32_t seq = 0 )
		{
			_header._type = UP_QUERY_FAVORITE_LIST_RSP;
			_header._seq = seq;
		}
		~CQuery_Favorite_List_Rsp( )
		{
			Clear();
		}
		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();

			if ( _num == 0 ) return false;
			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				CFavorite_List *info = new CFavorite_List;
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
		uint8_t _num; //查询结果个数
		CFavorite_List_VEC _vec;
	};

	//查询收藏列表详情
	class CView_Favorite_Info_Req : public CCOMMON_REQ , public IPacket //0x1009
	{
	public:
		CView_Favorite_Info_Req( )
		{
			_header._type = UP_VIEW_FAVORITE_INFO_REQ;
		}
		~CView_Favorite_Info_Req( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );
			pack->writeString( _storeCode );
			pack->writeBytes( _discountCode, sizeof ( _discountCode ) );
			pack->writeByte( _type );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );

			pack->readString( _storeCode );
			pack->readBytes( _discountCode, sizeof ( _discountCode ) );

			_type = pack->readByte();
			return true;
		}
	public:
		CQString _storeCode;
		uint8_t _discountCode[32]; //优惠信息编码
		uint8_t _type; //收藏类型
	};

	//优惠信息列表(新)
	class CDiscount_List_New : public share::Ref
	{
	public:
		CDiscount_List_New( )
		{
			memset( _discountCode, 0, sizeof ( _discountCode ) );
			memset( _begin_time, 0, sizeof ( _begin_time ) );
			memset( _end_time, 0, sizeof ( _end_time ) );

		}
		~CDiscount_List_New( )
		{
		}
		;
		void Pack( CPacker *pack )
		{
			pack->writeBytes( _discountCode, sizeof ( _discountCode ) );
			pack->writeString( _title );
			pack->writeString( _content );

			pack->writeBytes( _begin_time, sizeof ( _begin_time ) );
			pack->writeBytes( _end_time, sizeof ( _end_time ) );
		}
		bool UnPack( CPacker *pack )
		{
			pack->readBytes( _discountCode, sizeof ( _discountCode ) );
			pack->readString( _title );
			pack->readString( _content );

			pack->readBytes( _begin_time, sizeof ( _begin_time ) );
			pack->readBytes( _end_time, sizeof ( _end_time ) );

			return true;
		}
	public:
		uint8_t _discountCode[32]; //优惠信息编码
		CQString _title; //标题
		CQString _content; //内容
		uint8_t _begin_time[6]; //生效时间(yyyy-MM-dd)(BCD)
		uint8_t _end_time[6]; //失效时间(yyyy-MM-dd)(BCD)
	};

	class CView_Favorite_Info_Rsp : public IPacket // 0x8009 //查询收藏列表详情响应
	{
	public:
		typedef std::vector< CDiscount_List_New * > CDiscount_List_New_VEC;
	public:
		CView_Favorite_Info_Rsp( uint32_t seq = 0 )
		{
			_header._type = UP_VIEW_FAVORITE_INFO_RSP;
			_header._seq = seq;
		}
		~CView_Favorite_Info_Rsp( )
		{
			Clear();
		}
		bool UnBody( CPacker *pack )
		{
			pack->readString( _allianceName );
			_allianceType = pack->readInt();
			pack->readBytes( _storeCode, sizeof ( _storeCode ) );
			pack->readString( _storeName );
			pack->readBytes( _phone, sizeof ( _phone ) );
			pack->readString( _address );
			pack->readString( _desc );

			_lon = pack->readInt();
			_lat = pack->readInt();

			_num = pack->readByte();

			if ( _num == 0 ) return false;
			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				CDiscount_List_New *info = new CDiscount_List_New;
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
			pack->writeString( _allianceName );
			pack->writeInt( _allianceType );
			pack->writeBytes( _storeCode, sizeof ( _storeCode ) );
			pack->writeString( _storeName );
			pack->writeBytes( _phone, sizeof ( _phone ) );
			pack->writeString( _address );
			pack->writeString( _desc );

			pack->writeInt( _lon );
			pack->writeInt( _lat );

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
		CQString _allianceName; //商家名称
		uint32_t _allianceType; //商户类型1.维修、2.救援、3.加油、4.保养、5.餐饮、6.住宿（按位操作)
		uint8_t _storeCode[20]; //门店编码
		CQString _storeName; //门店名称
		uint8_t _phone[20]; //门店电话
		CQString _address; //详细地址
		CQString _desc; //门店详情
		uint32_t _lon; //门店经度
		uint32_t _lat; //门店纬度
		uint8_t _num; //查询结果个数
		CDiscount_List_New_VEC _vec;
	};
	//添加收藏
	class CAdd_Favorite_Req : public CCOMMON_REQ , public IPacket //0x100A
	{
	public:
		CAdd_Favorite_Req( )
		{
			_header._type = UP_ADD_FAVORITE_REQ;
		}
		~CAdd_Favorite_Req( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );

			pack->writeBytes( _fauoriteCode, sizeof ( _fauoriteCode ) );
			pack->writeByte( _type );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );

			pack->readBytes( _fauoriteCode, sizeof ( _fauoriteCode ) );
			_type = pack->readByte();
			return true;
		}
	public:
		uint8_t _fauoriteCode[32]; //门店编码(或优惠信息编码)
		uint8_t _type; //收藏类型（1：门店；2：优惠信息）
	};

	//删除收藏
	class CDel_Favorite_Req : public CCOMMON_REQ , public IPacket //0x100B
	{
	public:
		CDel_Favorite_Req( )
		{
			_header._type = UP_DEL_FAVORITE_REQ;
		}
		~CDel_Favorite_Req( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );

			pack->writeBytes( _id, sizeof ( _id ) );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );

			pack->readBytes( _id, sizeof ( _id ) );
			return true;
		}
	public:
		uint8_t _id[32]; //收藏id
	};
	//获取目的地
	class CGet_Destination_Req : public CCOMMON_REQ , public IPacket //0x100C
	{
	public:
		CGet_Destination_Req( )
		{
			_header._type = UP_GET_DESTINATION_REQ;
		}
		~CGet_Destination_Req( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _usercode, sizeof ( _usercode ) );
			pack->writeBytes( _verifyCode, sizeof ( _verifyCode ) );
		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _usercode, sizeof ( _usercode ) );
			pack->readBytes( _verifyCode, sizeof ( _verifyCode ) );

			return true;
		}
	};

	//车后服务
	class CQueryDiscountInfoReq : public IPacket //0x1029 查询打折优惠信息
	{
	public:
		CQueryDiscountInfoReq( )
		{
			_header._type = UP_DISCOUNT_INFO_REQ;
		}
		~CQueryDiscountInfoReq( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeByte( _type );
			pack->writeByte( _range );
			pack->writeByte( _sort_type );
			pack->writeInt( _lon );
			pack->writeInt( _lat );
			pack->writeByte( _offset );
			pack->writeByte( _count );
		}
		bool UnBody( CPacker *pack )
		{
			_type = pack->readByte();
			_range = pack->readByte();
			_sort_type = pack->readByte();

			_lon = pack->readInt();
			_lat = pack->readInt();

			_offset = pack->readByte();
			_count = pack->readByte();

			return true;
		}
	public:
		uint8_t _type; //优惠类型(0.加油 1.维修 2.救援 3.食宿 4.保险)
		uint8_t _range; //查询范围(0.15 1.30 2. 50)单位:公里
		uint8_t _sort_type; //排序类型
		uint32_t _lon; //经度
		uint32_t _lat; //纬度;
		uint8_t _offset; //读取开始位置
		uint8_t _count; //记录条数
	};

	//优惠信息列表
	class CDiscount_List : public share::Ref
	{
	public:
		CDiscount_List( )
		{
		}
		;
		~CDiscount_List( )
		{
		}
		;

		void Pack( CPacker *pack )
		{
			pack->writeBytes( _discount_num, sizeof ( _discount_num ) );
			pack->writeByte( _type );
			pack->writeString( _title );
			pack->writeBytes( _time, sizeof ( _time ) );
		}
		bool UnPack( CPacker *pack )
		{
			pack->readBytes( _discount_num, sizeof ( _discount_num ) );
			_type = pack->readByte();
			pack->readString( _title );
			pack->readBytes( _time, sizeof ( _time ) );
			return true;
		}
	public:
		uint8_t _discount_num[20]; //优惠编号
		uint8_t _type;  //优惠类型(1.维修、2.救援、3.加油、4.保养、5.餐饮、6.住宿、0.全部)
		CQString _title; //标题
		uint8_t _time[6]; //有效时间(BCD)
	};

	//下发优惠信息
	class  CSend_Detail_DiscountInfoReq:public IPacket //0x102B 下发优惠信息
	{
			typedef std::vector<CDiscount_List *> CDiscount_List_VEC;
	public:
			CSend_Detail_DiscountInfoReq() {
					_header._type = SEND_DETAIL_DISCOUNT_INFO_REQ;
			 }
			 ~CSend_Detail_DiscountInfoReq() {
				  Clear();
			  }
			 bool UnBody(CPacker *pack) {
				_num    = pack->readByte();
				_action = pack->readByte();

				if (_num == 0)
				   return false;
				for (int i = 0; i < (int) _num; ++i) {
				   CDiscount_List *info = new CDiscount_List;
				   if (!info->UnPack(pack)) {
						delete info;
						continue;
					}
				   info->AddRef();
				   _vec.push_back(info);
				}
					_num = _vec.size();
					return true;
				}

				void Body(CPacker *pack) {
				   pack->writeByte(_num);
				   pack->writeByte(_action);

				   if (_num == 0)
						return;
				   for (int i = 0; i < (int) _num; ++i) {
						_vec[i]->Pack(pack);
				   }
				}
	private:
			 void Clear(void) {
				if (_vec.empty())
				   return;
				for (int i = 0; i < (int) _vec.size(); ++i) {
						_vec[i]->Release();
				}
				_vec.clear();
			 }
	public:
			  uint8_t    _num;//查询结果的个数
			  uint8_t    _action;//动作 (0.查询 1.推送)

			  CDiscount_List_VEC _vec;//优惠信息列表
	};

	class CQueryDiscountInfoRsp : public IPacket //0x8029 查询打折优惠信息响应
	{
		typedef std::vector< CDiscount_List * > CDiscount_List_VEC;
	public:
		CQueryDiscountInfoRsp( uint32_t seq = 0 )
		{

			_header._type = UP_DISCOUNT_INFO_RSP;
			_header._seq = seq;
		}
		~CQueryDiscountInfoRsp( )
		{
			Clear();
		}
		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();
			_action = pack->readByte();

			if ( _num == 0 ) return false;
			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				CDiscount_List *info = new CDiscount_List;
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
			pack->writeByte( _action );

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
		uint8_t _num; //查询结果的个数
		uint8_t _action; //动作 (0.查询 1.推送)

		CDiscount_List_VEC _vec; //优惠信息列表
	};

	class CQueryDetailiscountInfoReq : public IPacket //0x102A 查询具体打折优惠信息
	{
	public:
		CQueryDetailiscountInfoReq( )
		{

			_header._type = UP_DETAIL_DISCOUNT_INFO_REQ;
		}

		~CQueryDetailiscountInfoReq( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _discount_num, sizeof ( _discount_num ) );
		}

		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _discount_num, sizeof ( _discount_num ) );
			return true;
		}
	public:
		uint8_t _discount_num[20]; //优惠编号
	};

	class CQueryDetailiscountInfoRsp : public IPacket //0x802A 查询具体打折优惠信息
	{
	public:
		CQueryDetailiscountInfoRsp( uint32_t seq = 0 )
		{
			_header._type = UP_DETAIL_DISCOUNT_INFO_RSP;
			_header._seq = seq;

			memset( & _phone, 0, sizeof ( _phone ) );
		}
		~CQueryDetailiscountInfoRsp( )
		{

		}
		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _discount_num, sizeof ( _discount_num ) );
			pack->readBytes( _business_num, sizeof ( _business_num ) );

			pack->readString( _title );
			pack->readBytes( _time, sizeof ( _time ) );
			pack->readString( _name );
			pack->readString( _address );
			pack->readBytes( _phone, sizeof ( _phone ) );

			_service_level = pack->readByte();
			pack->readString( _detail );
			_lon = pack->readInt();
			_lat = pack->readInt();

			return true;
		}

		void Body( CPacker *pack )
		{
			pack->writeBytes( _discount_num, sizeof ( _discount_num ) );
			pack->writeBytes( _business_num, sizeof ( _business_num ) );

			pack->writeString( _title );
			pack->writeBytes( _time, sizeof ( _time ) );
			pack->writeString( _name );
			pack->writeString( _address );
			pack->writeBytes( _phone, sizeof ( _phone ) );
			pack->writeByte( _service_level );
			pack->writeString( _detail );
			pack->writeInt( _lon );
			pack->writeInt( _lat );
		}
	public:
		uint8_t _discount_num[20]; //优惠编号
		uint8_t _business_num[20]; //门店编号
		CQString _title; //详情
		uint8_t _time[6];
		; //有效日期(BCD)
		CQString _name; //商户名称
		CQString _address; //商户地址
		uint8_t _phone[6]; //电话
		uint8_t _service_level; //服务等级
		CQString _detail; //详情
		uint32_t _lon; //经度
		uint32_t _lat; //纬度

	};

	class CQueryUnionBusinessInfoReq : public IPacket //0x102C 查询联盟商家信息
	{
	public:
		CQueryUnionBusinessInfoReq( )
		{

			_header._type = UP_UNION_BUSINESS_INFO_REQ;
		}

		~CQueryUnionBusinessInfoReq( )
		{

		}

		void Body( CPacker *pack )
		{
			pack->writeByte( _type );
			pack->writeByte( _range );
			pack->writeByte( _service_level );
			pack->writeByte( _sort_type );
			pack->writeInt( _lon );
			pack->writeInt( _lat );
			pack->writeByte( _offset );
			pack->writeByte( _count );
		}

		bool UnBody( CPacker *pack )
		{

			_type = pack->readByte();
			_range = pack->readByte();

			_service_level = pack->readByte();
			_sort_type = pack->readByte();

			_lon = pack->readInt();
			_lat = pack->readInt();

			_offset = pack->readByte();
			_count = pack->readByte();

			return true;
		}
	public:
		uint8_t _type; //优惠类型(1.维修、2.救援、3.加油、4.保养、5.餐饮、6.住宿、0.全部
		uint8_t _range; //查询范围(0.15 1.30 2. 50)单位:公里
		uint8_t _service_level; //服务等级
		uint8_t _sort_type; //排序类型
		uint32_t _lon; //经度
		uint32_t _lat; //纬度
		uint8_t _offset; //读取开始位置，默认从0开始
		uint8_t _count; //记录条数
	};

	//商家信息列表
	class CBusiness_List : public share::Ref
	{
	public:
		CBusiness_List( )
		{
		}
		;
		~CBusiness_List( )
		{
		}
		;
	public:
		void Pack( CPacker *pack )
		{
			pack->writeBytes( _business_num, sizeof ( _business_num ) );
			pack->writeString( _name );
			pack->writeByte( _type );
			pack->writeByte( _service_level );
			pack->writeInt( _lon );
			pack->writeInt( _lat );
		}

		bool UnPack( CPacker *pack )
		{
			pack->readBytes( _business_num, sizeof ( _business_num ) );
			pack->readString( _name );
			_type = pack->readByte();
			_service_level = pack->readByte();
			_lon = pack->readInt();
			_lat = pack->readInt();
			return true;
		}
	public:
		uint8_t _business_num[20]; //门店编号
		CQString _name; //门店名称
		uint8_t _type; //优惠类型(1.维修、2.救援、3.加油、4.保养、5.餐饮、6.住宿、0.全部)
		uint8_t _service_level; //服务等级
		uint32_t _lon; //经度
		uint32_t _lat; //纬度
	};

	class CQueryUnionBusinessInfoRsp : public IPacket //0x802C 查询联盟商家信息响应
	{
		typedef std::vector< CBusiness_List * > CBusiness_List_VEC;
	public:
		CQueryUnionBusinessInfoRsp( uint32_t seq = 0 )
		{
			_header._type = UP_UNION_BUSINESS_INFO_RSP;
			_header._seq = seq;
		}

		~CQueryUnionBusinessInfoRsp( )
		{
			Clear();
		}
		bool UnBody( CPacker *pack )
		{
			_num = pack->readByte();
			_action = pack->readByte();

			if ( _num == 0 ) return false;
			for ( int i = 0 ; i < ( int ) _num ; ++ i ) {
				CBusiness_List *info = new CBusiness_List;
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
			pack->writeByte( _action );

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
		uint8_t _num; //查询结果的个数;
		uint8_t _action; //动作(0.查询 1.推送)
		CBusiness_List_VEC _vec;
	};

	class CQueryDetailUnionBusinessInfoReq : public IPacket //0x102D  查询具体联盟商家信息
	{
	public:
		CQueryDetailUnionBusinessInfoReq( )
		{

			_header._type = UP_DETAIL_UNION_BUSINESS_INFO_REQ;
		}

		~CQueryDetailUnionBusinessInfoReq( )
		{

		}
		void Body( CPacker *pack )
		{
			pack->writeBytes( _business_num, sizeof ( _business_num ) );
		}

		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _business_num, sizeof ( _business_num ) );
			return true;
		}
	public:
		uint8_t _business_num[20]; //商家编号
	};

	class CQueryDetailUnionBusinessInfoRsp : public IPacket //0x802D 查询具体联盟商家信息响应
	{
	public:
		CQueryDetailUnionBusinessInfoRsp( uint32_t seq = 0 )
		{
			_header._type = UP_DETAIL_UNION_BUSINESS_INFO_RSP;
			_header._seq = seq;

			memset( & _phone, 0, sizeof ( _phone ) );
		}
		~CQueryDetailUnionBusinessInfoRsp( )
		{

		}

		void Body( CPacker *pack )
		{
			pack->writeBytes( _business_num, sizeof ( _business_num ) );
			pack->writeString( _name );
			pack->writeByte( _service_level );
			pack->writeString( _address );
			pack->writeBytes( _phone, sizeof ( _phone ) );
			pack->writeString( _detail );
			pack->writeInt( _lon );
			pack->writeInt( _lat );
		}

		bool UnBody( CPacker *pack )
		{
			pack->readBytes( _business_num, sizeof ( _business_num ) );
			pack->readString( _name );
			_service_level = pack->readByte();
			pack->readString( _address );
			pack->readBytes( _phone, sizeof ( _phone ) );
			pack->readString( _detail );
			_lon = pack->readInt();
			_lat = pack->readInt();
			return true;
		}
	public:
		uint8_t _business_num[20]; //商家编号
		CQString _name; //门店名称
		uint8_t _service_level; //服务等级
		CQString _address; //地址
		uint8_t _phone[6]; //电话
		CQString _detail; //详情
		uint32_t _lon; //经度
		uint32_t _lat; //纬度
	};
	//下发目的地
	class  CSend_DestinationInfoReq:public IPacket //0x100D 下发目的地
	{
	public:
			CSend_DestinationInfoReq(){
				_header._type = SEND_DESTINATION_INFO_REQ;
			}
			~CSend_DestinationInfoReq(){}

			void Body( CPacker *pack )
			{
				pack->writeBytes(_destName,sizeof(_destName));
				pack->writeString(_address);
				pack->writeBytes(_phone,sizeof(_phone));
				pack->writeInt(_lon);
				pack->writeInt(_lat);
				pack->writeByte(_state);
			}

			bool UnBody( CPacker *pack )
			{
				pack->readBytes(_destName,sizeof(_destName));
				pack->readString(_address);
				pack->readBytes(_phone,sizeof(_phone));
				_lon = pack->readInt();
				_lat = pack->readInt();
				_state = pack->readByte();

				return true;
			}
	public:
			uint8_t   _destName[20];//目的地名称
			CQString  _address;//详细地址
			uint8_t   _phone[20];//联系电话
			uint32_t  _lon;//经度
			uint32_t  _lat;//纬度
			uint8_t   _state;//是否成功（0：成功；1失败）
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

	class CCarServiceUnPackMgr : public IUnPackMgr
	{
	public:
		  CCarServiceUnPackMgr(){}
		 ~CCarServiceUnPackMgr(){}

		// 实现数据解包接口方法
		IPacket * UnPack( unsigned short msgtype, CPacker &pack )
		{
			IPacket *msg = NULL ;
			switch( msgtype )
				{
				case UP_DISCOUNT_INFO_REQ://查询打折优惠信息
					msg = UnPacket<CQueryDiscountInfoReq>(pack,"CQueryDiscountInfoReq");
					break;
				case UP_DETAIL_DISCOUNT_INFO_REQ: //查询具体打折优惠信息
					msg = UnPacket<CQueryDetailiscountInfoReq>(pack,"CQueryDetailiscountInfoReq");
					break;
				case UP_UNION_BUSINESS_INFO_REQ: //查询联盟商家信息
					msg = UnPacket<CQueryUnionBusinessInfoReq>(pack,"CQueryUnionBusinessInfoReq");
					break;
				case UP_DETAIL_UNION_BUSINESS_INFO_REQ: //查询联盟具体商家信息
					msg = UnPacket<CQueryDetailUnionBusinessInfoReq>(pack,"CQueryDetailUnionBusinessInfoReq");
					break;
				case UP_LOGIN_INFO_REQ://0x1001 用户登录
					msg = UnPacket<CLoginInfoReq>(pack,"CLoginInfoReq");
					break;
				case UP_QUERY_BALLANCE_LIST_REQ://0x1002 联名卡余额查询
					msg = UnPacket<CQuery_Ballance_List_Req>(pack,"CQuery_Ballance_List_Req");
					break;
				case UP_QUERY_STORE_LIST_REQ://0x1003 查询门店
					msg = UnPacket<CQuery_Store_List_Req>(pack,"CQuery_Store_List_Req");
					break;
				case  UP_QUERY_VIEW_STORE_INFO_REQ://0x1004 查询门店详情
					msg = UnPacket<CView_Store_Info_Req>(pack,"CView_Store_Info_Req");
					break;
				case  UP_QUERY_DISCOUNT_LIST_REQ://0x1005 新版本查询优惠信息
					msg = UnPacket<CQuery_Discount_List_Req>(pack,"CQuery_Discount_List_Req");
					break;
				case  UP_VIEW_DISCOUNT_INFO_REQ://0x1006  新版本查询优惠信息详细列表
					msg = UnPacket<CView_Discount_Info_Req>(pack,"CView_Discount_Info_Req");
					break;
				case UP_QUERY_TRADE_LIST_REQ://0x1007 历史交易记录查询
					msg = UnPacket<CQuery_Trade_List_Req>(pack,"CQuery_Trade_List_Req");
					break;
				case UP_QUERY_FAVORITE_LIST_REQ://0x1008 查询收藏列表
					msg = UnPacket<CQuery_Favorite_List_Req>(pack,"CQuery_Favorite_List_Req");
					break;
				case UP_VIEW_FAVORITE_INFO_REQ://0x1009 查询收藏列表详情
					msg = UnPacket<CView_Favorite_Info_Req>(pack,"CView_Favorite_Info_Req");
					break;
				case UP_ADD_FAVORITE_REQ://0x100A 添加收藏请求
					msg = UnPacket<CAdd_Favorite_Req>(pack,"CAdd_Favorite_Req");
					break;
				case UP_DEL_FAVORITE_REQ ://0x100B 删除收藏请求
					msg = UnPacket<CDel_Favorite_Req>(pack,"CDel_Favorite_Req");
					break;
				case UP_GET_DESTINATION_REQ://0x100C 获取目的地
					msg = UnPacket<CGet_Destination_Req>(pack,"CGet_Destination_Req");
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
}

#pragma pack()

#endif
