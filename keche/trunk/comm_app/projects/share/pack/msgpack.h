/*
 * msgpack.h
 *
 *  Created on: 2012-6-1
 *      Author: humingqing
 *  协议数据解析对象
 */

#ifndef __MSGPACK_H__
#define __MSGPACK_H__

#include <Ref.h>
#include <packer.h>

#pragma pack(1)

#define MSG_VERSION  0x0001              // 消息版本号
// 消息数据头
class CMsgHeader
{
public:
	CMsgHeader( )
	{
		_ver = MSG_VERSION;
		_type = 0;
		_seq = _len = 0;
	}
	~CMsgHeader( ){}

	// 解包数据
	bool UnPack( CPacker *pack )
	{
		if ( ( int ) sizeof(CMsgHeader) > pack->GetReadLen() )
			return false;

		_ver  = pack->readShort();
		_type = pack->readShort();
		_seq  = pack->readInt();
		_len  = pack->readInt();

		if ( ( int ) _len + ( int ) sizeof(CMsgHeader) > pack->GetReadLen() )
			return false;
		return true;
	}

	// 打包数据
	void Pack( CPacker *pack )
	{
		pack->writeShort( _ver );
		pack->writeShort( _type );
		pack->writeInt( _seq );
		pack->writeInt( _len );
	}

public:
	// 协议的版本号
	uint16_t _ver;
	// 协议的类型
	uint16_t _type;
	//　协议的序号
	uint32_t _seq;
	// 数据长度
	uint32_t _len;
};

// 数据解包对象接口
class IPacket : public share::Ref
{
public:
	IPacket() {}
	// 虚析构函数
	virtual ~IPacket(){}
	// 打包消息体
	virtual void Body( CPacker *body ) = 0 ;
	// 解包数据体
	virtual bool UnBody( CPacker *pack ) = 0 ;

public:
	// 解包数据
	bool UnPack( CPacker *pack )
	{
		if ( ! _header.UnPack( pack ) )
			return false;

		return UnBody( pack );
	}

	// 打包数据
	void Pack( CPacker *pack )
	{
		_header.Pack( pack );
		Body( pack );
		int len = pack->getLength() - ( int ) sizeof(CMsgHeader);
		pack->fillInt32( len, 8 );
	}

public:
	// 基本的数据头
	CMsgHeader _header;
};

#pragma pack()

// 自动释放引用对象
class CAutoRelease
{
public:
	CAutoRelease( share::Ref *ref )
	{
		_ref = ref;
	}
	~CAutoRelease( )
	{
		if ( _ref != NULL ) {
			_ref->Release();
			_ref = NULL;
		}
	}
private:
	share::Ref *_ref;
};

#endif /* MSGPARSER_H_ */
