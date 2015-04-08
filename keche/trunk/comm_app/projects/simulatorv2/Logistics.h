/**
 * Author: xifengming
 * date:   2012-11-17
 * memo:   物流模拟数据
 */
#ifndef _LOGISTICS_H
#define _LOGISTICS_H

#include <tools.h>
#include <databuffer.h>
#include <vector>
#include <arpa/inet.h>
#include <GBProtoParse.h>

using namespace std;

#define UPLOAD_DATAINFO_REQ	          0x1022 //配货自动上报
#define UP_CARDATA_INFO_REQ           0x1023 //上传配货信息
#define UP_ORDER_FORM_INFO_REQ        0x1027 //上传货运订单号
#define UP_TRANSPORT_FORM_INFO_REQ    0x1028 //上传运单调度单
#define TERMINAL_COMMON_RSP           0x1000 //终端通用回复
#define PLATFORM_COMMON_RSP           0x8000 //平台通用回复
#define SEND_CARDAT_INFO_REQ                 0x1021   //下发配货信息
#define SEND_TRANSPORT_ORDER_FORM_INFO_REQ   0x1025   //下发运单信息
#define SEND_CARDATA_INFO_CONFIRM_REQ        0x1024   //配货成交状态确认(下发)

#pragma pack(1)

struct TransHeader  // 数据透传的808头部处理
{
	GBheader header ;
	uint8_t  type;

	TransHeader() {
		type = 0x01 ;
	}
};

typedef struct tagMsgHeader
{
	unsigned short wMsgVer; //协议内部版本号
	unsigned short wMsgType; //报文的类型
	unsigned int dwMsgSeq; //报文序号
	unsigned int dwDataLen; //数据体长度
} MSG_HEADER;

/*配货状态上报*/
typedef struct tagAutoDataSchedule
{
	unsigned char byState; //状态
	unsigned char bySpace; //载重空间
	unsigned short w_weight; //载重量
	unsigned char byTime[6]; //出发时间
	unsigned int dwSarea; //出发地
	unsigned int dwDarea; //目的地
} AUTO_DATA_SCHEDULE;

/*上传配货信息*/
typedef struct tagCarDataInfo
{
	unsigned char _sid[20]; //String  配货单号
	unsigned char _status; //状态(0.已报价 1.拒绝 2.取消)
	unsigned int _price; //报价(精度0.01 元),如果状态为0，有该字段
} CAR_DATA_INFO;

/*上传订单状态*/
typedef struct tagOrderFormInfo
{
	unsigned char _sid[20]; //String 陆运流水号
	unsigned char _order_form[20]; //String 订单单号
	unsigned char _action; //调度单动作(0.提货 1.签收)
	unsigned char _status; //状态(0.正常 1.异常)
	unsigned int _lon; //经度
	unsigned int _lat; //纬度
} ORDER_FORM_INFO;

/*上传运单状态*/
typedef struct tagTransportFromInfo
{
	unsigned char _sid[20]; //String 陆运流水号
	unsigned char _action; //调度单动作(0.提货 1.签收)
	unsigned char _status; //状态(0.正常 1.异常)
	unsigned int _lon; //经度
	unsigned int _lat; //纬度
} TRANSPORT_FORM_INFO;

/*平台通用应答*/
typedef struct tagPlatFormCommonRsp
{
	unsigned short wType; //报文类型
	unsigned char byResult; //报文结果
} RSP_PLATFORM_COMMON;
#pragma pack()

//------------------------------------------------------------------------
class CLogistics  //物流
{
public:
	CLogistics( );
	~CLogistics( );

	bool LoadInitFile( const char *szName );
	int BuildTransportData( unsigned short wType, DataBuffer &pBuf );
	/*解析下发数据包*/
	int ParseTransparentMsgData( unsigned char *lpData, int nLen, DataBuffer &pRetuBuf );
private:
	void ParseParam( vector< string > &vec_param );
	int CreateRequestFrame( unsigned short wDataType, unsigned char *lpData, unsigned short nLen, unsigned int nSeq,
			DataBuffer &pBuf );
	/*0x1022 初始化配货上报*/
	void Init_Auto_Data_Schedule( vector< string > &vec_param );
	/*0x1023 初始化上传配货信息*/
	void Init_Car_Data_Info( vector< string > &vec_param );
	/*0x1027 初始化上传订单状态*/
	void Init_Order_Form_Info( vector< string > &vec_param );
	/*0x1028 初始化上传运单状态*/
	void Init_Transport_Form_Info( vector< string > &vec_param );
	/*0x8000 解析平台通用应答回复*/
	void Parse0x8000Frame( unsigned char *lpData );
	/*0x1021 下发配货信息*/
	int Parse0x1021Frame( unsigned char *lpSrcData, int msglen, unsigned int nSeq, DataBuffer &pRetBuf );
	//下发运单信息
	int Parse0x1025Frame( unsigned char *lpSrcData, int msglen, unsigned int nSeq, DataBuffer &pRetuBuf );
	//配货成交状态确认
	int Parse0x1024Frame( unsigned char *lpSrcData, int msglen, unsigned int nSeq, DataBuffer &pRetuBuf );
	/*终端通用回复*/
	void CreateTerminalCommRspFrame( unsigned short wMsgType, unsigned int nSeq, DataBuffer &pBuf );

private:
	unsigned int seq;

	AUTO_DATA_SCHEDULE _auto_data_schedule;
	CAR_DATA_INFO _car_data_info;
	ORDER_FORM_INFO _order_form_info;
	TRANSPORT_FORM_INFO _transport_form_info;
};
#endif
