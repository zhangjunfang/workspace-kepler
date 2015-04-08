/**********************************************
 * GbProtocolHandler.h
 *
 *  Created on: 2011-09-22
 *      Author: humingqing
 *    Comments: 实现新的808协议部份处理
 *********************************************/

#ifndef __GBPROTOCOLHANDLER_H_
#define __GBPROTOCOLHANDLER_H_

#include <std.h>
#include "GBProtoParse.h"
#include "BaseTools.h"
#include <iostream>
#include <stdlib.h>
#include <vector>
#include <string.h>
#include <Ref.h>
#include <databuffer.h>

#define   GB_PACK_SIZE             500

//判断某位是不是为1，如果是大于0，不是为0，不修改value值
#define  IS_BIT(value,num)   (value &(0x1<<num))

//某位赋值为1，修改value值
#define  S_BIT(value,num)   (value |=(0x1<<num))

//某位赋值为0，修改value值
#define  C_BIT(value,num)   (value &=(~(0x1<<num)) )

using namespace std;

// 驾驶员身份信息
struct DRIVER_INFO
{
	char drivername[256] ;
	char driverid[21] ;
	char driverorgid[41] ;
	char orgname[256] ;
};

class GbProtocolHandler : public share::Ref
{
	 /*
	 *   把字符串转换成十六机制。如 "FFFFFFFF" 转换成  0xffffffff
	 */
	static int atohex(char *str);
	static GbProtocolHandler * _instance ;

public:
	static GbProtocolHandler * getInstance() ;
	static void FreeHandler( GbProtocolHandler *inst ) ;

public:
	GbProtocolHandler(){}

	virtual ~GbProtocolHandler(){} ;

	//打印头的相关信息
	// void HeaderDecode(GBheader *header, int len, int flag);

	PlatFormCommonResp BuildPlatFormCommonResp(const GBheader*reqheaderptr,
			unsigned short downreq,unsigned char result);
	//转换为内部的 位置包
	string ConvertGpsInfo(GpsInfo*gps_info, const char *append_data, int append_data_len);

	//把设置参数转换为内部协议
	bool  ConvertGetPara(char *buf, int buf_len,string &data);

	// 获取驾驶员身份信息
	bool  GetDriverInfo( const char *buf, int len, DRIVER_INFO &info ) ;

	//把驾驶员信息转换为内部协议
	string  ConvertDriverInfo(char *buf, int buf_len, unsigned char result );

	// 转换发动机能效数据
	string ConvertEngeer( EngneerData *p ) ;
	// 转换驾驶行为事件
	string ConvertEventGps( GpsInfo *gps ) ;

	// 构建参数设制
	bool  buildParamSet( DataBuffer *pbuf , map<string,string> &map, unsigned char &pnum ) ;

public:
	unsigned char get_check_sum( const char *buf, int len ) ;

	string get_bcd_time(char bcd[6]);
	//20110304
	string get_date();
	//050507
	string get_time();

private:
	// 解析位置数据中共有自定义信息内容
	bool getCommonExtend(const unsigned char *ptr, int len, map<string, string> &mp);
};

#endif /* GBPROTOCOLHANDLER_H_ */
