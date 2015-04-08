/**********************************************
 * GBProtoParse.h
 *
 *  Created on: 2011-3-7
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/

#ifndef __GBPROTOPARSE_H_
#define __GBPROTOPARSE_H_

#pragma pack(1)

#include <string.h>
#include <arpa/inet.h>

//typedef struct _MsgType //信息消息体
//{
//	unsigned short retain:4;
//	unsigned char is_long_msg:1;
//	unsigned char rsa:1;
//	unsigned short msg_len:10;
//	//默认值全部为0，组包时只需要设置msg_len即可。
//	_MsgType()
//	{
//		rsa = 0;
//		is_long_msg = 0;
//		retain = 0;
//		msg_len = 0;
//	}
//}MsgType;
typedef struct _Begin
{
	char _begin;
	_Begin()
	{
		_begin = 0x7e;
	}
}Begin;

typedef struct _End
{
	char _end;
	_End()
	{
		_end = 0x7e;
	}
}End;

typedef struct _MsgType //信息消息体
{
	unsigned char retain:2;
	unsigned char is_long_msg:1;
	unsigned char rsa:3;
	//unsigned char res:2;
	unsigned short msg_len:10;

	//默认值全部为0，组包时只需要设置msg_len即可。
	_MsgType()
	{
		rsa = 0;
		is_long_msg = 0;
		retain = 0;
		msg_len = 0;
	}
}MsgType;

typedef struct _MsgPart//消息包封装项
{
	unsigned short msgsum;
	unsigned short msgorder;//从1开始
}MsgPart;

typedef struct _GBheader //国标消息头
{
	Begin begin;
	unsigned short msgid;
	MsgType msgtype;
	char car_id[6];
	unsigned short seq;
//	msgPart msgpart;
	_GBheader()
	{
		msgid = 0;
		memset(car_id,0,6);
		seq = 0;
	}
}GBheader;

typedef struct _GBFooter
{
	unsigned char check_sum ;
	End  ender ;
}GBFooter;

typedef struct _GBheader_test //国标消息头
{
	Begin begin;
	unsigned short msgid;
	unsigned short msgtype;
	char car_id[6];
	unsigned short seq;
//	msgPart msgpart;
	_GBheader_test()
	{
		msgid = 0;
		memset(car_id,0,6);
		seq = 0;
	}
}GBheader_test;

typedef struct _CommonResp
{
	unsigned short resp_seq;
	unsigned short resp_msg_id;
	unsigned char  result;
	_CommonResp()
	{
		resp_seq = 0;
		resp_msg_id = 0;
		result = 0;
	}
}CommonResp;


typedef struct _TermCommonResp
{
	GBheader   header;
	CommonResp resp;
	GBFooter   footer ;

	_TermCommonResp()
	{
		header.msgid = 0x0001;
		header.msgtype.msg_len = sizeof(struct _TermCommonResp);
	}

}TermCommonResp;

typedef struct _PlatFormCommonResp
{
	GBheader header;
	CommonResp resp;
	unsigned char check_sum;
	End end;
	_PlatFormCommonResp()
	{
		//header.msgid = 0x0180;
		//header.msgtype.msg_len = (sizeof(resp));
	}

}PlatFormCommonResp;


typedef struct _TermRegisterHeader //注册
{
	GBheader header;
	unsigned short province_id;
	unsigned short city_id;
	unsigned char corp_id[5];
	unsigned char termtype[20];  // 国标新增长度为20
	unsigned char termid[7];
	unsigned char carcolor;
	_TermRegisterHeader()
	{
		header.msgid = 0x0100;
		province_id = 0;
		city_id = 0;
		carcolor = 0;
	}
}TermRegisterHeader;


//终端注册回复
typedef struct _TermRegisterRespHeader
{
	GBheader header;
	unsigned short resp_seq;
	unsigned char result;
	//鉴权码；
	//unsigned char check_num;
	_TermRegisterRespHeader()
	{
		//终端注册应答
		//header.msgid = 0x8100;
	}

}TermRegisterRespHeader;


//回复为平台通用回复
typedef struct _TermAuthenticationHeader
{
	GBheader header;
	_TermAuthenticationHeader()
	{
		header.msgid = 0x0102;
	}
	//后面还会加上鉴权码和check_sum;

}TermAuthenticationHeader;



//告警标志位 对应报警信息
typedef struct _TermAlarm
{
	unsigned char  bit31:1;
	unsigned char  bit30:1;
	unsigned char  bit29:1;
	unsigned char  bit28:1;
	unsigned char  bit27:1;
	unsigned char  bit26:1;
	unsigned char  bit25:1;
	unsigned char  bit24:1;
	unsigned char  bit23:1;
	unsigned char  bit22:1;
	unsigned char  bit21:1;
	unsigned char  bit20:1;
	unsigned char  bit19:1;
	unsigned char  bit18:1;
	unsigned char  bit17:1;
	unsigned char  bit16:1;
	unsigned char  bit15:1;
	unsigned char  bit14:1;
	unsigned char  bit13:1;
	unsigned char  bit12:1;
	unsigned char  bit11:1;
	unsigned char  bit10:1;
	unsigned char  bit9:1;
	unsigned char  bit8:1;
	unsigned char  bit7:1;
	unsigned char  bit6:1;
	unsigned char  bit5:1;
	unsigned char  bit4:1;
	unsigned char  bit3:1;
	unsigned char  bit2:1;
	unsigned char  bit1:1;
	unsigned char  bit0:1;
}TermAlarm;


typedef struct _CarState
{
	unsigned char  bit31:1;
	unsigned char  bit30:1;
	unsigned char  bit29:1;
	unsigned char  bit28:1;
	unsigned char  bit27:1;
	unsigned char  bit26:1;
	unsigned char  bit25:1;
	unsigned char  bit24:1;
	unsigned char  bit23:1;
	unsigned char  bit22:1;
	unsigned char  bit21:1;
	unsigned char  bit20:1;
	unsigned char  bit19:1;
	unsigned char  bit18:1;
	unsigned char  bit17:1;
	unsigned char  bit16:1;
	unsigned char  bit15:1;
	unsigned char  bit14:1;
	unsigned char  bit13:1;
	unsigned char  bit12:1; //12	0：车门解锁     1：车门加锁
	unsigned char  bit11:1; //11	0：车辆电路正常     1：车辆电路断开
	unsigned char  bit10:1; // 10	0：车辆油路正常     1：车辆油路断开
	unsigned char  bit9:1; //9	0：空车         1：重车
	unsigned char  bit8:1; //8	0：ACC关      1：ACC开
	unsigned char  bit7:1;
	unsigned char  bit6:1; //保留
	unsigned char  bit5:1; //保留
	unsigned char  bit4:1; //0：未预约     1：预约(任务车)
	unsigned char  bit3:1; //0：运营状态     1：停运状
	unsigned char  bit2:1; //东西经度
	unsigned char  bit1:1; //南北纬度
	unsigned char  bit0:1;//Gps定位 未定位
}CarState;

typedef struct _VechileControl
{
	GBheader header;
	unsigned char a7:1;
	unsigned char a6:1;
	unsigned char a5:1;
	unsigned char a4:1;
	unsigned char a3:1;
	unsigned char a2:1;
	unsigned char a1:1;
	unsigned char a0:1;
	_VechileControl()
	{
		header.msgid = 0x8500;
		a0 = a1 = a2 = a3 = a4 = a5 = a6 =a7 = 0;
	}
}VechileControl;

typedef struct _GpsInfo
{
	TermAlarm alarm;
	CarState state;

	unsigned int latitude;
	unsigned int longitude;

	unsigned short heigth;
	unsigned short speed;
	unsigned short direction;
	char date_time[6];

	_GpsInfo()
	{
		memset(&alarm,0,sizeof(TermAlarm));
		memset(&state,0,sizeof(CarState));
		memset(date_time,0,6);
		longitude = 0;
		latitude = 0;
		heigth = 0;
		speed = 0;
		direction = 0;
	}
}GpsInfo;
//8.3.1　位置信息汇报
//终端根据参数设定周期性发送位置信息汇报(0x0200)消息，平台回复平台通用应答(0x8001)消息。
//根据参数控制，终端在判断到车辆拐弯时可发送位置信息汇报消息。
typedef struct _TermLocationHeader
{
	GBheader header;
	GpsInfo gpsinfo;

	//附加信息，可能有，也可能没有
	_TermLocationHeader()
	{
		header.msgid = 0x0200;
	}
}TermLocationHeader;




//8.3.2　位置信息查询
//平台通过发送位置信息查询(0x8201)消息查询指定车载终端当时位置信息，终端回复位置信息查询应答(0x0201)消息。
typedef struct _PlatformRequestLocation
{
	GBheader header;
	unsigned char check_num;
	End end;
	_PlatformRequestLocation()
	{
		header.msgtype.msg_len = 0;
		header.msgid = 0x8201;
	}

}PlatformRequestLocation;

//采集驾驶员身份信息数据8.8.1
//终端采集司机身份信息数据(0x8903)上传平台进行识别，平台回复成功与否消息(0x8001)。
typedef struct _PilotLocationHeader
{
	GBheader header;
	_PilotLocationHeader()
	{
		header.msgid = 0x8903;
	}
}PilotLocationHeader;

//设置终端参数8.2.3
//平台通过发送设置终端参数(0x8103)消息设置终端参数，终端回复终端通用应答(0x0001)消息。
typedef struct _SetGetTermParamHeader
{
	GBheader header;
	_SetGetTermParamHeader()
	{
		header.msgid = 0x8103;
	}
}SetGetTermParamHeader;

//查询终端参数8.2.3
//平台通过发送查询终端参数(0x8104)消息查询终端参数，终端回复查询终端参数应答(0x0104)消息。
typedef struct _QueryTermParamHeader
{
	GBheader header;
	_QueryTermParamHeader()
	{
		header.msgid = 0x8104;
	}

}QueryTermParamHeader;

struct PlatformTraceBody
{
	unsigned short timeval;
	unsigned int   period;
};

typedef struct _PlatformTraceLocation
{
	GBheader header;
	PlatformTraceBody body ;
	GBFooter footer ;
	_PlatformTraceLocation()
	{
		header.msgid = 0x8202;
	}
}PlatformTraceLocation;

typedef struct _PeripheryInfoInquiry
{
	GBheader header;
	unsigned char info_type;
}PeripheryInfoInquiry;

typedef struct _TermLocationRespHeader
{
	GBheader header;
	unsigned short reqseq;
	GpsInfo  gpsinfo;
	//附加信息，可能有，也可能没有
	_TermLocationRespHeader()
	{
		header.msgid = 0x0201;
	}
}TermLocationRespHeader;


/*
 *
平台通过发送位置跟踪控制(0x8202)消息启动/停止位置跟踪，位置跟踪要求终端停止之前的周期汇报，按消息指定时间间隔进行汇报。终端回复终端通用应答(0x0001)消息。
*/

typedef struct _PlatformLocationControl
{
	GBheader header;
	unsigned int time_interval;
	unsigned char check_num;

	_PlatformLocationControl()
	{
		header.msgid = 0x8202;
		//初始化的时候默认30s上传一次。
		time_interval = 30;
	}
}PlatformLocationControl;


/*
平台通过发送文本信息下发(0x8300)消息按指定方式通知驾驶员。终端回复终端通用应答(0x0001)消息。
 */

//位	标志
//0	1：紧急
//1	保留
//2	1：终端显示器显示
//3	1：终端TTS播读
//4	1：广告屏显示
//5～7	保留

typedef struct _TxtState
{
	unsigned char a7:1;
	unsigned char a6:1;
	unsigned char a5:1;
	unsigned char a4:1;
	unsigned char a3:1;
	unsigned char a2:1;
	unsigned char a1:1;
	unsigned char a0:1;

	_TxtState()
	{
		a0 = a1 = a2 = a3 = a4 = a5 = a6 = a7 = 1;
	}
}TxtState;

//回复平台通用应答
typedef struct _PlatformTxtMsgHeader
{
	GBheader header;
	TxtState state;
	_PlatformTxtMsgHeader()
	{
		header.msgid = 0x8300;
	}
}PlatformTxtMsgHeader;


//9.24　电话回拨(0x8400)消息体
//0	标志	BYTE	0:普通通话 1:监听
//1	电话号码	STRING	最长为20字节
typedef struct _PlatformTelRequestHeader
{
	GBheader header;
	unsigned char state; //0:普通通话 1:监听
//	unsigned char check_sum;

	_PlatformTelRequestHeader()
	{
		//header.msgid = 0x8400;
	}

}PlatformTelRequestHeader;


struct TakePhotoBody
{
	unsigned char  camera_id;  // >0
	unsigned short photo_num; //0表示停止拍摄；0xFFFF表示录像；其它表示拍照张数
	unsigned short time_interval; //连续拍摄时的时间间隔
	unsigned char  is_save; //1：保存；0：实时上传
	unsigned char  sense; //分辨率
	unsigned char  photo_quality; //1～10，1最好
	unsigned char  liangdu;
	unsigned char  duibidu;
	unsigned char  baohedu;
	unsigned char  sedu;
	// unsigned int   seq;

	TakePhotoBody()
	{
		camera_id 	  = 1;
		photo_num 	  = 1;
		is_save 	  = 0;
		sense 		  = 1;
		photo_quality = 1;
		liangdu 	  = 50;
		duibidu 	  = 50;
		baohedu 	  = 50;
		sedu 		  = 50;
		//seq           = 1;
	}
};

/*
0	图像/视频ID	DWORD	>0
4	摄像头ID	BYTE	0xFF表示音频文件
5	数据总包数	DWORD
9	包ID	DWORD	从1开始
13	位置图像/视频/音频数据包
 */
typedef struct _PhotoUploadHeader
{
	GBheader header;
	unsigned int photo_id;
	unsigned char camera_id;
	unsigned int total_num;
	unsigned int current_num;
}PhotoUploadHeader;

typedef struct _PhotoRespHeader
{
	GBheader header;
	unsigned int  photo_id;
	unsigned char retry_package_num;
	unsigned char check_num;
	End end;
}PhotoRespHeader;

// 分包数据处理
typedef struct _MediaPart
{
	unsigned short total ;
	unsigned short seq ;
}MediaPart;

// 多媒体数据上传
typedef struct _MediaUploadBody
{
	unsigned int id ;
	unsigned char type ;
	unsigned char mtype ;
	unsigned char event ;
	unsigned char chanel ;
	GpsInfo       gps ;
}MediaUploadBody;

struct MediaUploadBodyEx
{
	unsigned int id ;
	unsigned char type;
	unsigned char mtype;
	unsigned char event;
	unsigned char chanel;
	unsigned char bcd[6];
	GpsInfo       gps ;
};

typedef struct _EventReport//事件报告
{
	GBheader header;
	unsigned int  id ;     // 多媒体ID
	unsigned char type ;  // 多媒体类型 0 PIC, 1 AUDIO, 2 VIDEO
	unsigned char mtype ; // 文件件类型 0: JPEG , 1:TIF 2:MP3 3:WAV 4:WMV
	unsigned char event ; // 事件 0 消息发送 , 1 定时动作 , 2 抢劫报警 , 3 碰撞侧翻
	unsigned char chanel ; //
	GBFooter footer ;
}EventReort;

// 宇通的扩展处理字段
struct EventReportEx
{
	unsigned int  id ;     // 多媒体ID
	unsigned char type ;   // 多媒体类型 0 PIC, 1 AUDIO, 2 VIDEO
	unsigned char mtype ;  // 文件件类型 0: JPEG , 1:TIF 2:MP3 3:WAV 4:WMV
	unsigned char event ;  // 事件 0 消息发送 , 1 定时动作 , 2 抢劫报警 , 3 碰撞侧翻
	unsigned char chanel ; //
	unsigned char bcd[6] ; // bcd时间处理
	unsigned int  seq;//拍摄序号
};

typedef struct _QuestionReplyAck//提问应答
{
	GBheader header;
	unsigned short seq ;  // 应答流水号
	unsigned char  id;	  // 应答
	GBFooter footer ;

}QuestionReplyAsk;

typedef struct _InfoDemandCancleAck  // 信息点播/取消
{
	GBheader header;
	unsigned char info_type;  // 信息类型
	unsigned char info_mark;  // 点播或取消标志
	GBFooter footer ;
}InfoDemandCancleAck;

typedef struct _ElectronicSingle
{
	GBheader header;
	unsigned short single_len;
}ElectronicSingle;

// 车辆控制应答
struct CarControlAck
{
	GBheader header ;
	unsigned short seqid ;   // 应答流水号
	GpsInfo gpsinfo;
	GBFooter  footer ;
};

// 电子运单上报
struct EWayBillReportAckHeader
{
	GBheader header ;
	unsigned int length ;  // 数据长度
};

// 行驶记录仪
struct TachographBody
{
	unsigned short seq ;    // 应答流水号
	unsigned char  cmd ;    // 行驶记录仪命令字
};

/*
起始字节	字段	数据类型	说明
0	应答流水号	WORD	对应的摄像头图像/视频/音频上传消息的流水号
2	图像/视频/音频ID	DWORD	>0
6	重传包ID列表		不超过125项，无该字段则表明已收到全部数据包
 */
typedef struct _PhotoUploadResp
{
	GBheader header;
	unsigned int resp_seq;
	unsigned int photo_id;
	//重传列表暂时为空，不做重传
	unsigned char check_num;

	_PhotoUploadResp()
	{
		header.msgid = 0x8800;
		header.msgtype.msg_len = sizeof(struct _PhotoUploadResp);
	}
}PhotoUploadResp;

// 查询终端应答
struct QueryTermParamAckHeader
{
	GBheader header  ;
	unsigned short respseq ;
	unsigned char  nparam ;
};

// 存储多媒体数据检索应答消息
struct DataMediaHeader
{
	GBheader header ;
	unsigned short ackseq;  // 应答流水号
	unsigned short num ;    // 多媒体数据总项数
};
// 数据结构体
struct DataMediaBody
{
	unsigned int   mid  ;   // 多媒体ID，补页新增
	unsigned char  type ;   // 多媒体类型
	unsigned char  id ;		// 通道ID
	unsigned char  event ;  // 事件项编码
	GpsInfo	   	   gps ;	// 位置上报信息
};

// 数据结构体,宇通新增特别处理
struct DataMediaBodyEx
{
	unsigned int   mid  ;   // 多媒体ID，补页新增
	unsigned char  type ;   // 多媒体类型
	unsigned char  id ;		// 通道ID
	unsigned char  event ;  // 事件项编码
	unsigned char  bcd[6];  // bcd时间
	GpsInfo	   	   gps ;	// 位置上报信息
};

//录音开始命令
struct VoiceRecordBody
{
	unsigned char  command;
	unsigned short recordtime;
	unsigned char  saveflag;
	unsigned char  samplerates;
};

////////////////////// 设置围栏  ////////////////////////
// 区域设置头部
struct AreaSetHeader
{
	unsigned char op  ;
	unsigned char total ;
};

// 区域设置头
struct AreaHeader
{
	unsigned int    areaid ;  // 区域ID
	unsigned short  attr ;	  // 字段设置属性
};

// 经纬度结构体
struct LatLonPoint
{
	unsigned int lat ;  // 纬度
	unsigned int lon ;  // 经度
};

// 圆形区域
struct BoundAreaBody
{
 	LatLonPoint		local ;   // 位置
 	unsigned int    raduis ;   // 半径
};

// 矩形区域设置
struct RangleAreaBody
{
	LatLonPoint    left_top ;       // 左上顶
	LatLonPoint    right_bottom ;   // 右下顶
};

// 多边形设置
struct PolygonArea
{
	AreaHeader	area ;			// 区域属性头
};

// 区域时间
struct AreaTime
{
	char starttime[6] ;  // 开始时间
	char endtime[6] ;    // 结束时间
};

// 速度设置
struct AreaSpeed
{
	unsigned short speed ;  // 速度
	unsigned char  nlast ;  // 超速持续时间
};

////////////// 设置线路 ////////////////////////
struct RoadHeader
{
	unsigned int roadid ;  // 线路ID
	unsigned short attr ;  // 属性
};

struct BendPoint   // 拐点属性
{
	unsigned int  bendid ;  // 拐点ID
	unsigned int  segid ;   // 路段ID
	LatLonPoint   postion ; // 位置
	unsigned char width ;   // 路宽
	unsigned char battr ;   // 路度属性
};

struct Threshold  // 阈值
{
	unsigned short more ;  // 路段行驶过长阈值
	unsigned short less ;  // 路段行驶不足阈值
};

//////////////////// 事件设置 ////////////////////////
struct EventHeader
{
	unsigned char type ;  // 事件类型
	unsigned char total ; // 事件个数
};

// 事件内容的处理
struct EventContentBody
{
	unsigned char eventid ;  // 事件ID
	unsigned char length ;	 // 事件内容长度
};

///////////////////////信息点播菜单设置 /////////////////////////
struct DemandHeader  // 点播设置
{
	unsigned char type ;  // 设置类型
	unsigned char total ; // 个数
};

struct DemandContentBody
{
	unsigned char  mtype ;  // 菜单类型
	unsigned short length ; // 长度
};

///////////////////////设置电话本////////////////////////////
struct PhoneHeader
{
	unsigned char type ;  // 操作类型
	unsigned char total ; // 联系人总数
};

//////////////////////// 信息服务  ///////////////////////////
struct MsgServiceBody
{
	unsigned char type ;  // 信息类型
	unsigned short len ;  // 信息长度
};

//////////////////////// 提问下发 ///////////////////////////
struct QuestAskBody // 提问的内容
{
	unsigned char flag ;   // 提问下发标志位
	unsigned char len  ;   // 问题的长度
};

// 可选的答案
struct QuestAnswerBody
{
	unsigned char  aflag ;  // 答案志记
	unsigned short alen ;   // 答案长度
};

///////////////////////// 多媒体数据处理 //////////////////////////////
struct MediaDataBody  // 多媒体数据检索
{
	unsigned char type ;    // 类型
	unsigned char channel ; // 通道
	unsigned char event ;  // 编码
	char starttime[6] ;     // 开始时间
	char endtime[6] ;       // 结束时间
	MediaDataBody() {
		type    = 0 ;
		channel = 1 ;
		event   = 0 ;
	}
};

struct MediaDataUploadBody // 多媒体数据上传
{
	MediaDataBody body ;
	unsigned char flag ;  // 是否删除标志
};

// 宇通历史数据上传
struct HistoryDataUploadBody
{
	unsigned short seqid ;  // 对应的历史数据查询命令的流水号
	unsigned char  type  ;	// 历史数据类型
	char starttime[6] ;     // 历史数据起始时间
	char endtime[6] ;		// 历史数据结束时间
	unsigned char  num ;    // 历史数据项的个数
};

// 宇通上传历史数据应答
struct HistoryDataUploadAck
{
	GBheader header ;
	unsigned short seqid ;  // 对应序号
	unsigned char  num ;	// 重传个数
};

// 宇通发动机能效数据格式定义
struct EngneerData
{
	char time[6] ;  // 时间
	unsigned short speed ;   // 转速
	unsigned char  torque ;  // 扭矩
	unsigned char  position ; // 油门踏板位置
};

// 宇通历史数据查询
struct QueryHistoryDataBody
{
	unsigned char type ; // 类型
	char starttime[6] ;  // 开始时间
	char endtime[6] ;    // 结束时间
	unsigned short num ; // 个数
};

// 宇通驾驶行为事件
struct DriverActionEvent
{
	GBheader header ;
	unsigned char type ; // 行为事件ID
	GpsInfo       startgps ; // 开始位置
	GpsInfo 	  endgps ; // 结束位置
	GBFooter      footer ; //
};

typedef struct tagInfoVersion
{
	unsigned char id;//信息ID
	unsigned char len;//信息长度
	char          buf[0];//信息内容
}VERSION_IINFO;

struct ProgramVersionEvent
{
	GBheader header;
	unsigned char count;//信息条数
};

struct IdleSpeed //怠速协议
{
	GBheader header;
    unsigned char byStatus;//状态
    char byKey[64];//密钥
    unsigned short wVehicleTurnSpeed;//车辆转速
};

struct SpeedAdjustReport {
	unsigned char btime[6];
	unsigned char etime[6];
	unsigned short gpsSpeed;
	unsigned short vssSpeed;
	unsigned short recommend;
};

#pragma pack()
#endif /* GBPROTOPARSE_H_ */

