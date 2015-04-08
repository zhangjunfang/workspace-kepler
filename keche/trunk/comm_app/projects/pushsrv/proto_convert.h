/******************************************************
 *  CopyRight: 北京中交兴路科技有限公司(2012-2015)
 *   FileName: proto_convert.h
 *     Author: liubo  2012-5-14 
 *Description:
 *******************************************************/

#ifndef PROTO_CONVERT_H_
#define PROTO_CONVERT_H_

#include "proto_header.h"
#include "databuffer.h"
#include "proto_parse.h"

//#define  MSG_REQUEST_LOGIN                   0x0001 //登录
//#define  MSG_REQUEST_NOOP                    0x0002 //链路心跳
//#define  MSG_REQUEST_SUBSCRIBE_INFO          0x0003 //订阅请求
//#define  MSG_REQUEST_LOCATION_INFO_SERVICE   0x0004 //位置上报消息
//#define  MSG_REQUEST_ONLINE                  0x0005
//#define  MSG_REQUEST_LOCATION                0x0006
//#define  MSG_REQUEST_TRACE                   0x0007
//#define  MSG_REQUEST_TEXT                    0x0008
//#define  MSG_REQUEST_MONITOR                 0x0009
//#define  MSG_REQUEST_MEDIA_UP                0x000a
//#define  MSG_REQUEST_GET_PHOTO               0x000b
//#define  MSG_REQUEST_RECORD                  0x000c

class ConvertProto
{
public:
	typedef struct _NewProtoOut
	{
		string mac_id;
		DataBuffer data_buffer;
		MSG_TYPE msg_type;
	} NewProtoOut;

	typedef struct _InterProtoOut
	{
        string mac_id;
        string seq;
        string msg;
	} InterProtoOut;

	typedef bool (ConvertProto::*NewConvertFun)(NewProto *, InterProtoOut *);

	ConvertProto()
	{
		_msg_seq = 0;
	}

	~ConvertProto(){}

	void init();

	bool InterProto2NewProto(InterProto *inter_proto, NewProtoOut *out);

	bool NewProto2InterProto(NewProto *new_proto, InterProtoOut *out);

	bool BuildNewProto(unsigned short msg_type,  unsigned short msg_len,
			const char *msg_data, DataBuffer *data_buffer);

	bool BuildNewNoop(DataBuffer *data_buffer);

	bool BuildNewCommResp(unsigned short msg_type, unsigned int seq,  unsigned char ret, DataBuffer *data_buffer);

private:

	NewConvertFun GetNewConvertFun(unsigned short msg_type);


	//点名
	bool NewConvertReqGpsInfo(NewProto *new_proto, InterProtoOut *out);

	//监听
	bool NewConvertMonitor(NewProto *new_proto, InterProtoOut *out);

	//位置查询
	bool NewConvertTrace(NewProto *new_proto, InterProtoOut *out);

	//下发短信
	bool NewConvertText(NewProto *new_proto, InterProtoOut *out);

	//获取参数
	bool NewConvertGetParam(NewProto *new_proto, InterProtoOut *out);

	//设置参数
	bool NewConvertSetParam(NewProto *new_proto, InterProtoOut *out);

	//终端控制
	bool NewConvertTermControl(NewProto *new_proto, InterProtoOut *out);

	//事件设置
	bool NewConvertSetEvent(NewProto *new_proto, InterProtoOut *out);

	//提问下发
	bool NewConvertQuestionAsk(NewProto *new_proto, InterProtoOut *out);

	//信息点播菜单设置
	bool NewConvertInfoMenu(NewProto *new_proto, InterProtoOut *out);

	//信息服务下发
	bool NewConvertInfoSend(NewProto *new_proto, InterProtoOut *out);

	//设置电话本
	bool NewConvertPhoneBook(NewProto *new_proto, InterProtoOut *out);

	//车辆控制
	bool NewConvertCarControl(NewProto *new_proto, InterProtoOut *out);

	//设置圆形区域
	bool NewConvertSetCircle(NewProto *new_proto, InterProtoOut *out);

	//删除圆形区域
	bool NewConvertDelCircle(NewProto *new_proto, InterProtoOut *out);

	//设置矩形区域
	bool NewConvertSetRectangle(NewProto *new_proto, InterProtoOut *out);

	//删除矩形区域
	bool NewConvertDelRectangle(NewProto *new_proto, InterProtoOut *out);

	//设置多边形区域
	bool NewConvertSetPolygon(NewProto *new_proto, InterProtoOut *out);

	//删除多边形区域
	bool NewConvertDelPolygon(NewProto *new_proto, InterProtoOut *out);

	//设置路线
	bool NewConvertSetLine(NewProto *new_proto, InterProtoOut *out);

	//删除路线
	bool NewConvertDelLine(NewProto *new_proto, InterProtoOut *out);

	//行驶记录数据采集命令
	bool NewConvertDriveCollect(NewProto *new_proto, InterProtoOut *out);

	//行驶记录参数下传命令
	bool NewConvertDriveParam(NewProto *new_proto, InterProtoOut *out);

	//多媒体数据检索
	bool NewConvertMediaSearch(NewProto *new_proto, InterProtoOut *out);

	//指定多媒体数据上传
	bool NewConvertSingleUpload(NewProto *new_proto, InterProtoOut *out);

	//存储多媒体数据上传
	bool NewConvertMultiUpload(NewProto *new_proto, InterProtoOut *out);

	//拍照
	bool NewConvertGetPhoto(NewProto *new_proto, InterProtoOut *out);

	//录音
	bool NewConvertRecord(NewProto *new_proto, InterProtoOut *out);

	//通用回复
	bool InterConvertResp(InterProto *inter_proto, NewProtoOut *out);

	//内部位置信息的转换
	bool InterConvertLocation(InterProto *inter_proto, NewProtoOut *out);

	//上下线通知
	bool InterConvertQuery(InterProto *inter_proto, NewProtoOut *out);

	//多媒体上传
	bool InterConvertMedia(InterProto *inter_proto, NewProtoOut *out);

	//查询终端参数应答
	bool InterConvertGetParam(InterProto *inter_proto, NewProtoOut *out);

	//事件报告
	bool InterConvertEventReport(InterProto *inter_proto, NewProtoOut *out);

	//提问应答
	bool InterConvertQuestionAck(InterProto *inter_proto, NewProtoOut *out);

	//信息点播/取消
	bool InterConvertInfoResp(InterProto *inter_proto, NewProtoOut *out);

	//电子运单上报
	bool InterConvertListReport(InterProto *inter_proto, NewProtoOut *out);

	//驾驶员身份采集
	bool InterConvertIdentityCollect(InterProto *inter_proto, NewProtoOut *out);

	//多媒体事件上传
	bool InterConvertMediaEvent(InterProto *inter_proto, NewProtoOut *out);

	//存储多媒体数据检索应答
	bool InterConvertSearchResp(InterProto *inter_proto, NewProtoOut *out);

	//数据上行透传
	bool InterConvertTransDeliver(InterProto *inter_proto, NewProtoOut *out);

	unsigned int StringToInteger(string &s_value, int nType);

	unsigned int get_msg_seq()
	{
		return _msg_seq++;
	}

	bool parseItem(const string &text, vector<string> &res, char lc, char rc);

private:
    unsigned _msg_seq;

    CNewProtoParse _new_parse;
    CInterProtoParse _inter_parse;


    map<unsigned short, NewConvertFun> _new_convert_table;
};

#endif /* PROTO_CONVERT_H_ */
