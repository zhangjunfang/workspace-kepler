/**********************************************
 * jt808prot.h
 *
 *  Created on: 2014-8-19
 *      Author: ycq
 *********************************************/

#ifndef __JT808PROT_H_
#define __JT808PROT_H_

#include <string.h>
#include <stdint.h>
#include <arpa/inet.h>

#pragma pack(1)
typedef struct _MsgPart {
	uint16_t count;
	uint16_t index; //从1开始
} MsgPart;

typedef struct _JTHeader {
	uint8_t delim;
	uint16_t msgid;
	uint16_t length;
	uint8_t simid[6];
	uint16_t seqid;
	_JTHeader() {
		delim = 0x7e;
		msgid = 0;
		length = 0;
		memset(simid, 0, 6);
		seqid = 0;
	}
} JTHeader;

typedef struct _JTFooter {
	uint8_t check_sum;
	uint8_t delim;

	_JTFooter() {
		delim = 0x7e;
	}
} JTFooter;

typedef struct _CommonResp
{
	JTHeader header;
	unsigned short resp_seq;
	unsigned short resp_msg_id;
	unsigned char  result;
	JTFooter footer;
	_CommonResp()
	{
		resp_seq = 0;
		resp_msg_id = 0;
		result = 0;
	}
} CommonResp;

typedef struct _TermRegister { //注册
	JTHeader header;
	unsigned short province_id;
	unsigned short city_id;
	char corp_id[5];
	char termtype[20]; // 国标新增长度为20
	char termid[7];
	unsigned char carcolor;
	char carplate[0];
	_TermRegister() {
		header.msgid = 0x0100;
		province_id = 0;
		city_id = 0;
		carcolor = 0;
	}
} TermRegister;

//终端注册回复
typedef struct _TermRegisterResp {
	JTHeader header;
	unsigned short resp_seq;
	unsigned char result;
	unsigned char akey[0];
	_TermRegisterResp() {
		//终端注册应答
		//header.msgid = 0x8100;
	}
} TermRegisterResp;
#pragma pack()
#endif//__JT808PROT_H__

