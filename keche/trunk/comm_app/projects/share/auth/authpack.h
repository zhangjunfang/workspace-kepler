/*
 * authpack.h
 *
 *  Created on: 2012-6-1
 *      Author: humingqing
 *  协议数据解析对象
 */

#ifndef __AUTHMSGPACK_H__
#define __AUTHMSGPACK_H__

#include <packer.h>
#include <packfactory.h>

#define SERVER_LOGIN_REQ  		0x1001   // 身份认证登陆
#define SERVER_LOGIN_RSP  		0x8001
#define SERVER_LOOP_REQ			0x1002   // 链路维护响应
#define SERVER_LOOP_RSP			0x8002
#define SERVER_REGISTER_REQ     0x1003   // 车辆注册请求
#define SERVER_REGISTER_RSP 	0x8003   // 应答
#define SERVER_TERMAUTH_REQ		0x1004   // 鉴权请求
#define SERVER_TERMAUTH_RSP		0x8004
#define SERVER_CKDRIVER_REQ		0x1005   // 司机身份识别
#define SERVER_CKDRIVER_RSP		0x8005
#define SERVER_TERMLOGO_REQ		0x1006 	 // 终端注销
#define SERVER_TERMLOGO_RSP		0x8006

// 登陆请求
class CLoginReq: public IPacket
{
public:
	CLoginReq( uint32_t seq = 0 ) {
		_header._type = SERVER_LOGIN_REQ ;
		_header._seq  = seq ;
	};
	~CLoginReq(){};
	// 解包数据体
	bool UnBody( CPacker *pack ) {
		if ( pack->readBytes( _uid, 32 ) == 0 )
			return false ;
		return true ;
	} ;

	void Body( CPacker *pack ) {
		pack->writeBytes( _uid, 32 ) ;
	}

public:
	// 登陆用户ID
	uint8_t   _uid[32] ;
};

// 登陆响应
class CLoginRsp: public IPacket
{
public:
	CLoginRsp( uint32_t seq = 0 ){
		_header._type = SERVER_LOGIN_RSP ;
		_header._seq  = seq ;
		_result       = 0 ;
	}
	~CLoginRsp(){};
	// 写入登陆成功与否应答
	void Body( CPacker *pack ) {
		pack->writeByte( _result ) ;
	}
	bool UnBody( CPacker *pack ) {
		_result = pack->readByte() ;
		return true ;
	}
public:
	uint8_t   _result ;
};

// 心跳请求
class CLoopReq: public IPacket
{
public:
	CLoopReq( uint32_t seq = 0 ){
		_header._type = SERVER_LOOP_REQ ;
		_header._seq  = seq ;
	}
	~CLoopReq(){};
	void Body( CPacker *pack ) {}
	bool UnBody( CPacker *pack ) { return true; }
};

// 心跳请求
class CLoopRsp: public IPacket
{
public:
	CLoopRsp( uint32_t seq = 0 ){
		_header._type = SERVER_LOOP_RSP ;
		_header._seq  = seq ;
	}
	~CLoopRsp(){};
	void Body( CPacker *pack ) {}
	bool UnBody( CPacker *pack ) { return true; }
};

// 终端注册请求
class CRegisterReq: public IPacket
{
public:
	CRegisterReq( uint32_t seq = 0 ) {
		_header._type = SERVER_REGISTER_REQ ;
		_header._seq  = seq ;
	}
	~CRegisterReq() {} ;

	// 解析数据
	bool UnBody( CPacker *pack ) {
		pack->readString( _vehicleColor ) ;
		pack->readString( _vehicleno ) ;
		pack->readString( _phone ) ;
		pack->readString( _terminaltype ) ;
		pack->readString( _terminalid ) ;
		pack->readString( _manufacturerid ) ;
		pack->readString( _cityid ) ;
		return true ;
	}

	void Body( CPacker *pack ) {
		pack->writeString( _vehicleColor ) ;
		pack->writeString( _vehicleno ) ;
		pack->writeString( _phone ) ;
		pack->writeString( _terminaltype ) ;
		pack->writeString( _terminalid ) ;
		pack->writeString( _manufacturerid ) ;
		pack->writeString( _cityid ) ;
	}

public:
	CQString _vehicleColor ;  	// 车牌颜色
	CQString _vehicleno ;		// 车牌号
	CQString _phone ;			// 手机号
	CQString _terminaltype ;	// 终端类型
	CQString _terminalid ;		// 终端ID
	CQString _manufacturerid ;	// 制造商ID
	CQString _cityid ;			// 城市ID
};

// 终端注册响应
class CRegisterRsp: public IPacket
{
public:
	CRegisterRsp( uint32_t seq = 0 ) {
		_header._type = SERVER_REGISTER_RSP ;
		_header._seq  = seq ;
	}
	~CRegisterRsp() {}

	void Body( CPacker *pack ) {
		pack->writeByte( _result ) ;
		pack->writeString( _ome ) ;
		pack->writeString( _auth ) ;
	}
	bool UnBody( CPacker *pack ) {
		_result = pack->readByte() ;
		pack->readString( _ome ) ;
		pack->readString( _auth ) ;
		return true ;
	}
public:
	uint8_t	 	_result ;   // 注册结果
	CQString 	_ome ;		// 设备OME
	CQString 	_auth ;		// 鉴权码
};

// 终端设备鉴权
class CTermAuthReq: public IPacket
{
public:
	CTermAuthReq( uint32_t seq = 0 ) {
		_header._type = SERVER_TERMAUTH_REQ ;
		_header._seq  = seq ;
	}
	~CTermAuthReq(){}

	bool UnBody( CPacker *pack ) {
		if ( pack->readString( _phone ) == 0 )
			return false ;
		pack->readString( _auth ) ;
		return true ;
	}
	void Body( CPacker *pack ) {
		pack->writeString( _phone ) ;
		pack->writeString( _auth ) ;
	}
public:
	CQString _phone ;   // 手机号
	CQString _auth ;	// 鉴权码
};

// 终端鉴权响应
class CTermAuthRsp: public IPacket
{
public:
	CTermAuthRsp( uint32_t seq = 0 ){
		_header._type = SERVER_TERMAUTH_RSP ;
		_header._seq  = seq ;
	}
	~CTermAuthRsp(){}

	void Body( CPacker *pack ) {
		pack->writeByte( _result ) ;
		pack->writeString( _ome ) ;
	}
	bool UnBody( CPacker *pack ) {
		_result = pack->readByte() ;
		pack->readString( _ome ) ;
		return true ;
	}
public:
	uint8_t  _result ;  // 结果
	CQString _ome ;	    // OEM码
};

// 司机身份识别
class CChkDriverReq: public IPacket
{
public:
	CChkDriverReq( uint32_t seq = 0 ){
		_header._type = SERVER_CKDRIVER_REQ ;
		_header._seq  = seq ;
	}
	~CChkDriverReq(){}

	bool UnBody( CPacker *pack ) {
		if ( pack->readString( _phone ) == 0 )
			return false ;
		pack->readString( _driverNo ) ;
		pack->readString( _driverCertificate ) ;
		return true ;
	}

	void Body( CPacker *pack ) {
		pack->writeString( _phone ) ;
		pack->writeString( _driverNo ) ;
		pack->writeString( _driverCertificate ) ;
	}

public:
	CQString _phone ; 				// 手机号
	CQString _driverNo ;			// 司机证号
	CQString _driverCertificate ;	// 身份证号
};

// 司机身份识别响应
class CChkDriverRsp: public IPacket
{
public:
	CChkDriverRsp( uint32_t seq = 0 ) {
		_header._type = SERVER_CKDRIVER_RSP ;
		_header._seq  = seq ;
	}
	~CChkDriverRsp(){}

	void Body( CPacker *pack ) {
		pack->writeByte( _result ) ;
		pack->writeString( _msg ) ;
	}
	bool UnBody( CPacker *pack ) {
		_result = pack->readByte() ;
		pack->readString( _msg ) ;
		return true ;
	}
public:
	uint8_t  	_result ;   // 识别结果
	CQString 	_msg ;		// 身份数据信息
};

// 终端设备注销请求
class CTermLogoReq: public IPacket
{
public:
	CTermLogoReq( uint32_t seq = 0 ){
		_header._type = SERVER_TERMLOGO_REQ ;
		_header._seq  = seq ;
	}
	~CTermLogoReq(){}

	bool UnBody( CPacker *pack ) {
		if ( pack->readString( _phone ) == 0 )
			return false ;
		return true ;
	}
	void Body( CPacker *pack ) {
		pack->writeString( _phone ) ;
	}
public:
	CQString _phone ;
};

// 终端设备注销响应
class CTermLogoRsp: public IPacket
{
public:
	CTermLogoRsp( uint32_t seq = 0 ){
		_header._type = SERVER_TERMLOGO_RSP ;
		_header._seq  = seq ;
	}
	~CTermLogoRsp() {}

	void Body( CPacker *pack ) {
		pack->writeByte( _result ) ;
	}
	bool UnBody( CPacker *pack ) {
		_result = pack->readByte() ;
		return true ;
	}

public:
	uint8_t _result ;
};

class CAuthUnPackMgr : public IUnPackMgr
{
public:
	CAuthUnPackMgr(){}
	~CAuthUnPackMgr(){}

	// 实现数据解包接口方法
	IPacket * UnPack( unsigned short msgtype, CPacker &pack )
	{
		IPacket *msg = NULL ;
		switch( msgtype )
		{
		case SERVER_LOGIN_REQ:
			msg = UnPacket<CLoginReq>( pack, "login" ) ;
			break ;
		case SERVER_LOGIN_RSP:
			msg = UnPacket<CLoginRsp>( pack, "login resp" ) ;
			break ;
		case SERVER_LOOP_REQ:
			msg = UnPacket<CLoopReq>( pack, "loop" ) ;
			break ;
		case SERVER_LOOP_RSP:
			msg = UnPacket<CLoopRsp>( pack, "loop resp" ) ;
			break ;
		case SERVER_REGISTER_REQ:
			msg = UnPacket<CRegisterReq>( pack, "term register" ) ;
			break ;
		case SERVER_REGISTER_RSP:
			msg = UnPacket<CRegisterRsp>( pack, "term register resp" );
			break ;
		case SERVER_TERMAUTH_REQ:
			msg = UnPacket<CTermAuthReq>( pack, "terminal auth" ) ;
			break ;
		case SERVER_TERMAUTH_RSP:
			msg = UnPacket<CTermAuthRsp>( pack, "terminal auth resp" ) ;
			break ;
		case SERVER_CKDRIVER_REQ:
			msg = UnPacket<CChkDriverReq>( pack, "check driver" ) ;
			break ;
		case SERVER_CKDRIVER_RSP:
			msg = UnPacket<CChkDriverRsp>( pack, "check driver resp" ) ;
			break ;
		case SERVER_TERMLOGO_REQ:
			msg = UnPacket<CTermLogoReq>( pack, "terminal logout" ) ;
			break ;
		case SERVER_TERMLOGO_RSP:
			msg = UnPacket<CTermLogoRsp>( pack, "terminal logout resp" ) ;
			break ;
		default:
			break ;
		}
		return msg ;
	}
};

#endif /* MSGPARSER_H_ */
