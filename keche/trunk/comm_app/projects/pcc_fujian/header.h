/*
 * header.h
 *
 *  Created on: 2012-8-13
 *      Author: humingqing
 */

#ifndef __HEADER_H__
#define __HEADER_H__

#define PACK_TAG   "&&"

#pragma pack(1)

struct _Header
{
	unsigned char  tag[2] ;  	// && 用两个“&”表示一个报文的开始
	unsigned short flag ;    	// 0位表示消息经过RSA加密,1位表示消息体中的经纬度经过加密，其他位保留
	unsigned short mid ; 	 	// 标识接入设备的厂家，如无定义则填两个0x0
	unsigned char  termid[8] ;  // 用于标示终端号，用压缩BCD码表示，通常终端用SIM号表示，位数不足后补0x0
	unsigned int   result ; 	// 由平台定义响应内容，上下行响应保持一一对应
	unsigned int   seq ;  		// 按发送顺序从0开始循环累加，或用户自定义代号，下行及上行一一对应
	unsigned int   len ;    	// 报文体的长度，高位在前
};

struct _Gps
{
	unsigned char  time[6] ; 	// GPS的BCD的时间
	unsigned int   state ; 		// 状态
	unsigned int   lon ;   		// 经度
	unsigned int   lat ;	  	// 纬度
	unsigned short speed ; 		// 速度
	unsigned char  direction ;  // 方向
	unsigned short height ;  	// 海拔
	unsigned short distance ;  	// 行驶距离
	unsigned int   mile ;  	 	// 总里程
};

struct _CallReq
{
	unsigned short cmd ;  // 0x0102
};

struct _CallRsp
{
	unsigned short cmd ;  // 0x0182
	_Gps 		   gps ;
};

struct _PhotoReq
{
	unsigned short cmd ; 		// 0x0301
	unsigned char  cameraid ;  	// 摄像头ID	1Byte	用于多路控制，单路时填 1，多路按位开启相应摄像头
	unsigned short num ; 		// 次数	1Word	0表示持续监控
	unsigned short time; 		// 间隔	1Word	单位：秒
	unsigned char  quality;    // 图像质量	1Byte	0~5, 数字越大，图片质量越好，传输的文件也越大
	unsigned char  brightness; // 图像亮度	1Byte	0~255
	unsigned char  contrast;   // 对比度	1Byte	0~127
	unsigned char  saturation; // 饱和度	1Byte	0~127
	unsigned char  chroma;     // 色度	1Byte	0~255
};

struct _PhotoRsp
{
	unsigned short cmd ;  	  // 0x0381
	unsigned char  cameraid ; // 摄像头ID(1)
	_Gps		   gps ;      // GPS报文(29)
	unsigned short data_len;  // 图像数据大小(2)+图像数据
};

struct _ScheduleReq
{
	unsigned short cmd ;  // 0x0401
	// 后续调度文本信息
};

struct _ScheduleRsp
{
	unsigned short cmd ;  // 0x0481  调度响应
};

struct _UploadGps
{
	unsigned short cmd ;  // 0x0181 GPS位置上传
	_Gps		   gps ;  // 位置数据信息
};

#pragma pack()


#endif /* HEADER_H_ */
