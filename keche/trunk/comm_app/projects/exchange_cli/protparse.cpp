/*
 * protparse.cpp
 *
 *  Created on: 2014-5-19
 *      Author: ycq
 */
#include <Base64.h>
#include <comlog.h>
#include "../tools/utils.h"
#include "exchprot.h"
#include "protparse.h"

#include <utility>
using std::make_pair;

#include <arpa/inet.h>
#include <fcntl.h>
#include <unistd.h>
#include <sys/stat.h>

const size_t ProtParse::EXCH_HEAD_LEN = sizeof(ExchHead);
const size_t ProtParse::EXCH_TAIL_LEN = sizeof(ExchTail);
const size_t ProtParse::EXCH_MIN_LEN = ProtParse::EXCH_HEAD_LEN + ProtParse::EXCH_TAIL_LEN;

ProtParse::ProtParse()
{
	_pEnv = NULL;
	_srcid = 0;
	_dstid = 0;
	_seqid = 0;
	_picpath = "";
}

ProtParse::~ProtParse()
{
}

bool ProtParse::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv ;

	char szbuf[512] = {0};

	if (!pEnv->GetString("master_center_id", szbuf)) {
		OUT_ERROR( NULL, 0, "conf", "master_center_id  empty" );
		return false;
	}
	_dstid = atoi(szbuf);

	if (!pEnv->GetString("slave_center_id", szbuf)) {
		OUT_ERROR( NULL, 0, "conf", "slave_center_id  empty" );
		return false;
	}
	_srcid = atoi(szbuf);

	if (!pEnv->GetString("pic_path", szbuf)) {
		OUT_ERROR( NULL, 0, "conf", "pic_path  empty" );
		return false;
	}
	_picpath = szbuf;

	return true;
}

uint32_t ProtParse::buildExchLogin(const string &un, const string &pw, vector<unsigned char> &resBuf)
{
	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchPlatHead *ephPtr;
	ExchLogin *login;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchPlatHead));
	ephPtr = (ExchPlatHead*)&tmpBuf[tmpLen];
	ephPtr->msgid = htons(0x1101);

	// 构造登录对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchLogin));
	login = (ExchLogin*)&tmpBuf[tmpLen];
	un.copy(login->username, 16);
	pw.copy(login->password, 32);

	return buildExchCommon(0x1100, tmpBuf, resBuf);
}

uint32_t ProtParse::buildExchHeartBeat(vector<unsigned char> &resBuf)
{
	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchPlatHead *ephPtr;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchPlatHead));
	ephPtr = (ExchPlatHead*)&tmpBuf[tmpLen];
	ephPtr->msgid = htons(0x1102);

	return buildExchCommon(0x1100, tmpBuf, resBuf);
}

uint32_t ProtParse::buildExchSubUnits(vector<unsigned char> &resBuf)
{
	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchPlatHead *ephPtr;
	ExchSubUnits *subUnits;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchPlatHead));
	ephPtr = (ExchPlatHead*)&tmpBuf[tmpLen];
	ephPtr->msgid = htons(0x1103);

	// 创建订阅对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchSubUnits) + sizeof(int));
	subUnits = (ExchSubUnits*)&tmpBuf[tmpLen];
	subUnits->cmd = 1;
	subUnits->num = 1;
	subUnits->ids[0] = htonl(_srcid);

	return buildExchCommon(0x1100, tmpBuf, resBuf);
}

uint32_t ProtParse::buildExchSubCmdid(vector<unsigned char> &resBuf)
{
	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchPlatHead *ephPtr;
	ExchSubCmdid *subCmdid;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchPlatHead));
	ephPtr = (ExchPlatHead*)&tmpBuf[tmpLen];
	ephPtr->msgid = htons(0x1104);

	// 创建订阅对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchSubCmdid));
	subCmdid = (ExchSubCmdid*) &tmpBuf[tmpLen];
	subCmdid->cmd = 1;
	subCmdid->num = 0;

	return buildExchCommon(0x1100, tmpBuf, resBuf);
}

uint32_t ProtParse::buildExchStatus(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf)
{
	map<string,string>::const_iterator itMss;

	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchTermHead *ethPtr;
	uint8_t *status;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTermHead));
	ethPtr = (ExchTermHead*)&tmpBuf[tmpLen];
	ethPtr->msgid = htons(0x1201);
	num.copy(ethPtr->mobile, 12);

	// 预留多媒体事件对象空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(char));
	status = &tmpBuf[tmpLen];
	for(itMss = params.begin(); itMss != params.end(); ++itMss) {
		const string &strKey = itMss->first;
		const string &strVal = itMss->second;

		tmpLen = tmpBuf.size();
		if(strKey == "18") {
			*status = atoi(strVal.c_str());
		}
	}

	return buildExchCommon(0x1200, tmpBuf, resbuf);
}

uint32_t ProtParse::buildExchTermReg(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf)
{
	map<string,string>::const_iterator itMss;

	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchTermHead *ethPtr;
	ExchTerminalReg *reg;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTermHead));
	ethPtr = (ExchTermHead*)&tmpBuf[tmpLen];
	ethPtr->msgid = htons(0x1202);
	num.copy(ethPtr->mobile, 12);

	// 预留多媒体事件对象空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTerminalReg));

	for(itMss = params.begin(); itMss != params.end(); ++itMss) {
		const string &strKey = itMss->first;
		const string &strVal = itMss->second;

		reg = (ExchTerminalReg*)&tmpBuf[EXCH_HEAD_LEN + sizeof(ExchTermHead)];
		if(strKey == "40") {
			reg->city = htons(atoi(strVal.c_str()));
		} else if(strKey == "41") {
			reg->province = htons(atoi(strVal.c_str()));
		} else if(strKey == "42") {
			strVal.copy((char*)reg->produceid, sizeof(reg->produceid));
		} else if(strKey == "43") {
			strVal.copy((char*)reg->termtype, sizeof(reg->termtype));
		} else if(strKey == "44") {
			strVal.copy((char*)reg->termid, sizeof(reg->termid));
		} else if(strKey == "45") {
			reg->result = atoi(strVal.c_str());
		} else if(strKey == "202") {
			reg->color = atoi(strVal.c_str());
		} else if(strKey == "104") {
			tmpLen = tmpBuf.size();
			tmpBuf.resize(tmpLen + strVal.length());
			reg = (ExchTerminalReg*) &tmpBuf[EXCH_HEAD_LEN + sizeof(ExchTermHead)];
			strVal.copy((char*)reg->plate, strVal.length());
		}
	}

	return buildExchCommon(0x1200, tmpBuf, resbuf);
}

uint32_t ProtParse::buildExchTermAuth(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf)
{
	map<string,string>::const_iterator itMss;

	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchTermHead *ethPtr;
	ExchTerminalAuth *auth;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTermHead));
	ethPtr = (ExchTermHead*)&tmpBuf[tmpLen];
	ethPtr->msgid = htons(0x1204);
	num.copy(ethPtr->mobile, 12);

	// 预留多媒体事件对象空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTerminalAuth));

	for(itMss = params.begin(); itMss != params.end(); ++itMss) {
		const string &strKey = itMss->first;
		const string &strVal = itMss->second;

		if(strKey == "47") {
			tmpLen = tmpBuf.size();
			tmpBuf.resize(tmpLen + strVal.length());
			auth = (ExchTerminalAuth*)&tmpBuf[EXCH_HEAD_LEN + sizeof(ExchTermHead)];
			memcpy(auth->akey, strVal.c_str(), strVal.length());
		} else if(strKey == "48") {
			auth = (ExchTerminalAuth*)&tmpBuf[EXCH_HEAD_LEN + sizeof(ExchTermHead)];
			auth->result = atoi(strVal.c_str());
		}
	}

	return buildExchCommon(0x1200, tmpBuf, resbuf);
}

uint32_t ProtParse::buildExchLocation(const string &num, const map<string,string> &params, vector<unsigned char> &resBuf)
{
	map<string,string>::const_iterator itMss;

	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchTermHead *ethPtr;
	ExchItem *itemPtr;
	size_t itemLen;
	uint16_t itemCnt;

	uint8_t u08Val;
	uint16_t u16Val;
	uint32_t u32Val;
	vector<string> fields;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 预留消息子类型空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTermHead));
	ethPtr = (ExchTermHead*)&tmpBuf[tmpLen];
	ethPtr->msgid = htons(0x1205);
	num.copy(ethPtr->mobile, 12);

	// 预留参数个数空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(short));

	itemCnt = 0;
	for(itMss = params.begin(); itMss != params.end(); ++itMss) {
		const string &strKey = itMss->first;
		const string &strVal = itMss->second;

		tmpLen = tmpBuf.size();
		if(strKey == "20") { //报警
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x01);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "8") { //状态
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x02);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "1") { //经度
			u32Val = htonl(atoi(strVal.c_str()) * 10 / 6);
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x03);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "2") { //纬度
			u32Val = htonl(atoi(strVal.c_str()) * 10 / 6);
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x04);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "6") { //海拔
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x05);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "3") { //gps速度
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x06);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "5") { //方向
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x07);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "4") { //gps时间
			string gpsdt = "00000000000000";
			if(strVal.length() == 15) {
				gpsdt = strVal.substr(0, 8) + strVal.substr(9);
			}

			itemLen = 14;
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x08);
			itemPtr->len = htonl(itemLen);
			gpsdt.copy((char*)itemPtr->val, itemLen);

			++itemCnt;
		} else if(strKey == "9") { //里程
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x09);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if (strKey == "24") { //油箱存有量
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0a);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if (strKey == "7") { //vss速度
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0b);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		}  else if (strKey == "519") { //需要人工确认报警事件的ID
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0c);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if (strKey == "31") { //超速报警
			AddInfoSpeed *speed;
			fields.clear();
			if(Utils::splitStr(strVal, fields, '|') == 2 && fields[1].size() > 0) {
				itemLen = 5;
			} else {
				itemLen = 1;
			}
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0d);
			itemPtr->len = htonl(itemLen);
			speed = (AddInfoSpeed*)itemPtr->val;
			speed->type = atoi(strVal.c_str());
			if(itemLen == 5) {
				*speed->id = htonl(atoi(fields[1].c_str()));
			}

			++itemCnt;
		} else if (strKey == "32") { //进出区域/路段报警
			AddInfoInout *inout;
			fields.clear();
			if (Utils::splitStr(strVal, fields, '|') != 3) {
				continue;
			}
			itemLen = sizeof(AddInfoInout);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0e);
			itemPtr->len = htonl(itemLen);
			inout = (AddInfoInout*)itemPtr->val;
			inout->type = atoi(fields[0].c_str());
			inout->id = htonl(atoi(fields[1].c_str()));
			inout->flag = atoi(fields[2].c_str());

			++itemCnt;
		} else if (strKey == "35") { //路线行驶时间不足/过长
			AddInfoLonger *longer;
			fields.clear();
			if (Utils::splitStr(strVal, fields, '|') != 3) {
				continue;
			}
			itemLen = sizeof(AddInfoLonger);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0f);
			itemPtr->len = htonl(itemLen);
			longer = (AddInfoLonger*)itemPtr->val;
			longer->id = htonl(atoi(fields[0].c_str()));
			longer->tv = htons(atoi(fields[1].c_str()));
			longer->flag = atoi(fields[2].c_str());

			++itemCnt;
		} else if(strKey == "210") { //发动机转速
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x10);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "216") { //瞬时油耗
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x11);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "503") { //发动机扭矩百分比
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x12);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "504") { //油门踏板位置
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x13);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "21") { //扩展报警
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x14);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "500") { //扩展状态
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x15);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "213") { //累计油耗
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x16);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if (strKey == "218") { //速度来源
			u08Val = atoi(strVal.c_str());
			itemLen = sizeof(u08Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x18);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u08Val, itemLen);

			++itemCnt;
		}
	}

	itemCnt = htons(itemCnt);
	tmpLen = EXCH_HEAD_LEN + sizeof(ExchTermHead);
	memcpy(&tmpBuf[tmpLen], &itemCnt, sizeof(short));

	return buildExchCommon(0x1200, tmpBuf, resBuf);
}

uint32_t ProtParse::buildExchHistory(const string &num, const map<string,string> &params, vector<unsigned char> &resBuf)
{
	map<string,string>::const_iterator itMss;

	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchTermHead *ethPtr;
	ExchItem *itemPtr;
	size_t itemLen;
	uint16_t itemCnt;

	uint8_t u08Val;
	uint16_t u16Val;
	uint32_t u32Val;
	vector<string> fields;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 预留消息子类型空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTermHead));
	ethPtr = (ExchTermHead*)&tmpBuf[tmpLen];
	ethPtr->msgid = htons(0x1206);
	num.copy(ethPtr->mobile, 12);

	// 预留参数个数空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(short));

	itemCnt = 0;
	for(itMss = params.begin(); itMss != params.end(); ++itMss) {
		const string &strKey = itMss->first;
		const string &strVal = itMss->second;

		tmpLen = tmpBuf.size();
		if(strKey == "20") { //报警
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x01);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "8") { //状态
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x02);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "1") { //经度
			u32Val = htonl(atoi(strVal.c_str()) * 10 / 6);
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x03);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "2") { //纬度
			u32Val = htonl(atoi(strVal.c_str()) * 10 / 6);
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x04);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "6") { //海拔
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x05);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "3") { //gps速度
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x06);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "5") { //方向
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x07);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "4") { //gps时间
			string gpsdt = "00000000000000";
			if(strVal.length() == 15) {
				gpsdt = strVal.substr(0, 8) + strVal.substr(9);
			}

			itemLen = 14;
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x08);
			itemPtr->len = htonl(itemLen);
			gpsdt.copy((char*)itemPtr->val, itemLen);

			++itemCnt;
		} else if(strKey == "9") { //里程
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x09);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if (strKey == "24") { //油箱存有量
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0a);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if (strKey == "7") { //vss速度
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0b);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if (strKey == "519") { //需要人工确认报警事件的ID
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0c);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if (strKey == "31") { //超速报警
			AddInfoSpeed *speed;
			fields.clear();
			if(Utils::splitStr(strVal, fields, '|') == 2 && fields[1].size() > 0) {
				itemLen = 5;
			} else {
				itemLen = 1;
			}
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0d);
			itemPtr->len = htonl(itemLen);
			speed = (AddInfoSpeed*)itemPtr->val;
			speed->type = atoi(strVal.c_str());
			if(itemLen == 5) {
				*speed->id = htonl(atoi(fields[1].c_str()));
			}

			++itemCnt;
		} else if (strKey == "32") { //进出区域/路段报警
			AddInfoInout *inout;
			fields.clear();
			if (Utils::splitStr(strVal, fields, '|') != 3) {
				continue;
			}
			itemLen = sizeof(AddInfoInout);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0e);
			itemPtr->len = htonl(itemLen);
			inout = (AddInfoInout*)itemPtr->val;
			inout->type = atoi(fields[0].c_str());
			inout->id = htonl(atoi(fields[1].c_str()));
			inout->flag = atoi(fields[2].c_str());

			++itemCnt;
		} else if (strKey == "35") { //路线行驶时间不足/过长
			AddInfoLonger *longer;
			fields.clear();
			if (Utils::splitStr(strVal, fields, '|') != 3) {
				continue;
			}
			itemLen = sizeof(AddInfoLonger);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x0f);
			itemPtr->len = htonl(itemLen);
			longer = (AddInfoLonger*)itemPtr->val;
			longer->id = htonl(atoi(fields[0].c_str()));
			longer->tv = htons(atoi(fields[1].c_str()));
			longer->flag = atoi(fields[2].c_str());

			++itemCnt;
		} else if(strKey == "210") { //发动机转速
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x10);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "216") { //瞬时油耗
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x11);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "503") { //发动机扭矩百分比
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x12);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "504") { //油门踏板位置
			u16Val = htons(atoi(strVal.c_str()));
			itemLen = sizeof(u16Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x13);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u16Val, itemLen);

			++itemCnt;
		} else if(strKey == "21") { //扩展报警
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x14);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "500") { //扩展状态
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x15);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if(strKey == "213") { //累计油耗
			u32Val = htonl(atoi(strVal.c_str()));
			itemLen = sizeof(u32Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x16);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u32Val, itemLen);

			++itemCnt;
		} else if (strKey == "218") { //速度来源
			u08Val = atoi(strVal.c_str());
			itemLen = sizeof(u08Val);
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*)&tmpBuf[tmpLen];
			itemPtr->key = htonl(0x18);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, &u08Val, itemLen);

			++itemCnt;
		}
	}

	itemCnt = htons(itemCnt);
	tmpLen = EXCH_HEAD_LEN + sizeof(ExchTermHead);
	memcpy(&tmpBuf[tmpLen], &itemCnt, sizeof(short));

	return buildExchCommon(0x1200, tmpBuf, resBuf);
}

uint32_t ProtParse::buildExchMMEvent(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf)
{
	map<string,string>::const_iterator itMss;

	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchTermHead *ethPtr;
	ExchMMEvent *event;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTermHead));
	ethPtr = (ExchTermHead*)&tmpBuf[tmpLen];
	ethPtr->msgid = htons(0x1207);
	num.copy(ethPtr->mobile, 12);

	// 预留多媒体事件对象空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchMMEvent));
	event = (ExchMMEvent*)&tmpBuf[tmpLen];
	for(itMss = params.begin(); itMss != params.end(); ++itMss) {
		const string &strKey = itMss->first;
		const string &strVal = itMss->second;

		tmpLen = tmpBuf.size();
		if(strKey == "120") {
			event->mmid = htonl(atoi(strVal.c_str()));
		} else if(strKey == "121") {
			event->mmtype = atoi(strVal.c_str());
		} else if(strKey == "122") {
			event->mmcode = atoi(strVal.c_str());
		} else if(strKey == "123") {
			event->event = atoi(strVal.c_str());
		} else if(strKey == "124") {
			event->cameraid = atoi(strVal.c_str());
		}
	}

	return buildExchCommon(0x1200, tmpBuf, resbuf);
}

uint32_t ProtParse::buildExchMMData(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf)
{
	map<string,string>::const_iterator itMss;

	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchTermHead *termHeadPtr;
	ExchMMData *mmd;

	// 容器预开辟80k空间
	tmpBuf.reserve(81920);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTermHead));
	termHeadPtr = (ExchTermHead*)&tmpBuf[tmpLen];
	termHeadPtr->msgid = htons(0x1208);
	num.copy(termHeadPtr->mobile, 12);

	// 预留多媒体事件对象空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchMMData));

	for(itMss = params.begin(); itMss != params.end(); ++itMss) {
		const string &strKey = itMss->first;
		const string &strVal = itMss->second;

		// 读取图片数据可能导致mmd指针失效，每次重新赋值
		mmd = (ExchMMData*)(&tmpBuf[EXCH_HEAD_LEN + sizeof(ExchTermHead)]);
		if(strKey == "120") {
			mmd->mmid = htonl(atoi(strVal.c_str()));
		} else if(strKey == "121") {
			mmd->mmtype = atoi(strVal.c_str());
		} else if(strKey == "122") {
			mmd->mmcode = atoi(strVal.c_str());
		} else if(strKey == "123") {
			mmd->event = atoi(strVal.c_str());
		} else if(strKey == "124") {
			mmd->cameraid = atoi(strVal.c_str());
		} else if(strKey == "125") {
			readMediaData(_picpath + "/" + strVal, tmpBuf);
		} else if(strKey == "20") {
			mmd->gps.warn = htonl(atoi(strVal.c_str()));
		} else if(strKey == "8") {
			mmd->gps.stat = htonl(atoi(strVal.c_str()));
		} else if(strKey == "1") {
			mmd->gps.lon = htonl(atoi(strVal.c_str()));
		} else if(strKey == "2") {
			mmd->gps.lat = htonl(atoi(strVal.c_str()));
		} else if(strKey == "3") {
			mmd->gps.speed = htons(atoi(strVal.c_str()));
		} else if(strKey == "4" && strVal.length() == 15) {
			Utils::hex2array(strVal.substr(2, 6) + strVal.substr(9, 6), mmd->gps.time);
		} else if(strKey == "5") {
			mmd->gps.direction = htons(atoi(strVal.c_str()));
		} else if(strKey == "6") {
			mmd->gps.height = htons(atoi(strVal.c_str()));
		}
	}

	return buildExchCommon(0x1200, tmpBuf, resbuf);
}

uint32_t ProtParse::buildExchDriverEvent(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf)
{
	map<string,string>::const_iterator itMss;

	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchTermHead *ethPtr;
	ExchDriverEvent *event;

	string gpsBegin;
	string gpsEnd;
	string gpsTime;

	vector<string> fields;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 预留消息子类型空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTermHead));
	ethPtr = (ExchTermHead*)&tmpBuf[tmpLen];
	ethPtr->msgid = htons(0x1209);
	num.copy(ethPtr->mobile, 12);

	// 预留驾驶行为事件对象空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchDriverEvent));

	event = (ExchDriverEvent*)&tmpBuf[EXCH_HEAD_LEN + sizeof(ExchTermHead)];
	event->begin.warn = 0;
	event->begin.stat = 0;
	event->end.warn = 0;
	event->end.stat = 0;

	for(itMss = params.begin(); itMss != params.end(); ++itMss) {
		const string &strKey = itMss->first;
		const string &strVal = itMss->second;

		if(strKey != "516") {
			continue;
		}

		fields.clear();
		if(Utils::splitStr(strVal, fields, '|') != 3) {
			continue;
		}

		event->type = atoi(fields[0].c_str());
		gpsBegin = fields[1];
		gpsEnd = fields[2];

		fields.clear();
		if (Utils::splitStr(gpsBegin, fields, '[', ']') != 6) {
			continue;
		}
		if(fields[5].length() != 15) {
			continue;
		}
		gpsTime = fields[5].substr(2, 6) + fields[5].substr(9, 6);

		event->begin.lat = htonl(atoi(fields[0].c_str()) * 10 / 6);
		event->begin.lon = htonl(atoi(fields[1].c_str()) * 10 / 6);
		event->begin.height = htons(atoi(fields[2].c_str()));
		event->begin.speed = htons(atoi(fields[3].c_str()));
		event->begin.direction = htons(atoi(fields[4].c_str()));
		Utils::hex2array(gpsTime, event->begin.time);

		fields.clear();
		if (Utils::splitStr(gpsEnd, fields, '[', ']') != 6) {
			continue;
		}
		if(fields[5].length() != 15) {
			continue;
		}
		gpsTime = fields[5].substr(2, 6) + fields[5].substr(9, 6);

		event->end.lat = htonl(atoi(fields[0].c_str()) * 10 / 6);
		event->end.lon = htonl(atoi(fields[1].c_str()) * 10 / 6);
		event->end.height = htons(atoi(fields[2].c_str()));
		event->end.speed = htons(atoi(fields[3].c_str()));
		event->end.direction = htons(atoi(fields[4].c_str()));
		Utils::hex2array(gpsTime, event->end.time);
	}

	return buildExchCommon(0x1200, tmpBuf, resbuf);
}

uint32_t ProtParse::buildExchTermVersion(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf)
{
	map<string,string>::const_iterator itMss;

	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchTermHead *ethPtr;
	ExchItem *itemPtr;
	size_t itemLen;
	uint16_t itemCnt;

	vector<string> fields;
	vector<string>::iterator itVs;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 预留消息子类型空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTermHead));
	ethPtr = (ExchTermHead*)&tmpBuf[tmpLen];
	ethPtr->msgid = htons(0x120a);
	num.copy(ethPtr->mobile, 12);

	// 预留参数个数空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(short));

	for(itMss = params.begin(); itMss != params.end(); ++itMss) {
		const string &strKey = itMss->first;
		const string &strVal = itMss->second;

		if(strKey != "518") {
			continue;
		}

		fields.clear();
		Utils::splitStr(strVal, fields, '|');

		itemCnt = 0;
		for(itVs = fields.begin(); itVs != fields.end(); ++itVs) {
			++itemCnt;
			itemLen = itVs->length();
			tmpLen = tmpBuf.size();
			tmpBuf.resize(tmpLen + sizeof(ExchItem) + itemLen);
			itemPtr = (ExchItem*) &tmpBuf[tmpLen];
			itemPtr->key = htonl(itemCnt);
			itemPtr->len = htonl(itemLen);
			memcpy(itemPtr->val, itVs->c_str(), itemLen);
		}
	}

	itemCnt = htons(itemCnt);
	tmpLen = EXCH_HEAD_LEN + sizeof(ExchTermHead);
	memcpy(&tmpBuf[tmpLen], &itemCnt, sizeof(short));

	return buildExchCommon(0x1200, tmpBuf, resbuf);
}

uint32_t ProtParse::buildExchTTData(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf)
{
	map<string,string>::const_iterator itMss;

	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchTermHead *ethPtr;
	ExchTTData *ttd;

	CBase64 base64;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTermHead));
	ethPtr = (ExchTermHead*)&tmpBuf[tmpLen];
	ethPtr->msgid = htons(0x120b);
	num.copy(ethPtr->mobile, 12);

	// 预留数据透传对象空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchTTData));

	for(itMss = params.begin(); itMss != params.end(); ++itMss) {
		const string &strKey = itMss->first;
		const string &strVal = itMss->second;

		if(strKey == "91") {
			ttd = (ExchTTData*)(&tmpBuf[EXCH_HEAD_LEN + sizeof(ExchTermHead)]);
			ttd->type = atoi(strVal.c_str());
		} else if(strKey == "90") {
			if(base64.Decode(strVal.c_str(), strVal.length())) {
				tmpLen = tmpBuf.size();
				tmpBuf.resize(tmpLen + base64.GetLength());
				ttd = (ExchTTData*)(&tmpBuf[EXCH_HEAD_LEN + sizeof(ExchTermHead)]);
				memcpy(ttd->data, base64.GetBuffer(), base64.GetLength());
			}
		}
	}

	return buildExchCommon(0x1200, tmpBuf, resbuf);
}

uint32_t ProtParse::buildExchBaseTable(const string &str, uint16_t msgid, vector<uint8_t> &resBuf)
{
	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchPlatHead *ephPtr;
	ExchBaseTab *baseTab;

	// 容器预开辟8k空间
	tmpBuf.reserve(8192);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchPlatHead));
	ephPtr = (ExchPlatHead*)&tmpBuf[tmpLen];
	ephPtr->msgid = htons(msgid);

	CBase64 base64;
	vector<string> fields;

	if(Utils::splitStr(str, fields, ':') != 2) {
		return false;
	}
	if(!base64.Decode(fields[1].c_str(), fields[1].length())) {
		return false;
	}

	// 构造基础资料对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchBaseTab) + base64.GetLength());
	baseTab = (ExchBaseTab*)&tmpBuf[tmpLen];
	baseTab->type = atoi(fields[0].c_str());
	strncpy((char*)baseTab->data, base64.GetBuffer(), base64.GetLength());

	return buildExchCommon(0x1600, tmpBuf, resBuf);
}

bool ProtParse::readMediaData(const string &file, vector<unsigned char> &data)
{
	size_t size = data.size();

	int fd;
	struct stat fs;

	if (stat(file.c_str(), &fs) != 0) {
		return false;
	}

	if ((fd = open(file.c_str(), O_RDONLY)) < 0) {
		return false;
	}

	data.resize(size + fs.st_size);
	if (read(fd, &data[size], fs.st_size) != fs.st_size) {
		close(fd);
		return false;
	}
	close(fd);

	return true;
}

size_t ProtParse::getSeqid(size_t step)
{
	return __sync_fetch_and_add(&_seqid, step);
}

unsigned char ProtParse::getExchCrc(const unsigned char *data, size_t size)
{
	size_t i;
	unsigned char xorVal = 0;

	for(i = 0; i < size; ++i) {
		xorVal ^= data[i];
	}

	return xorVal;
}

size_t ProtParse::enCode(const unsigned char *src, size_t len, unsigned char *dst)
{
	size_t i, j;

	for (i = j = 0; i < len; ++i) {
		switch (src[i]) {
		case 0x5b:
			dst[j++] = 0x5a;
			dst[j++] = 0x01;
			break;
		case 0x5a:
			dst[j++] = 0x5a;
			dst[j++] = 0x02;
			break;
		case 0x5d:
			dst[j++] = 0x5e;
			dst[j++] = 0x01;
			break;
		case 0x5e:
			dst[j++] = 0x5e;
			dst[j++] = 0x02;
			break;
		default:
			dst[j++] = src[i];
			break;
		}
	}

	return j;
}

size_t ProtParse::deCode(const unsigned char *src, size_t len, unsigned char *dst)
{
	size_t i, j;
	unsigned char prev;
	unsigned char curr;

	prev = 0;
	for (i = j = 0; i < len; ++i) {
		curr = src[i];
		if(curr == 0x5a || curr == 0x5e) {
			prev = curr;
			continue;
		} else if(prev == 0x5a && curr == 0x01) {
			dst[j++] = 0x5b;
		} else if(prev == 0x5a && curr == 0x02) {
			dst[j++] = 0x5a;
		} else if(prev == 0x5e && curr == 0x01) {
			dst[j++] = 0x5d;
		} else if(prev == 0x5e && curr == 0x02) {
			dst[j++] = 0x5e;
		} else {
			dst[j++] = src[i];
		}

		prev = 0;
	}

	return j;
}

uint32_t ProtParse::buildExchCommon(uint16_t msgid, vector<uint8_t> &src, vector<uint8_t> &dst)
{
	size_t pos;
	size_t srclen;
	size_t dstlen;

	ExchHead *headPtr;
	ExchTail *tailPtr;
	uint32_t seqid = 0;

	if(src.size() < EXCH_HEAD_LEN) {
		return seqid;
	}

	pos = 0;
	seqid = getSeqid();
	headPtr = (ExchHead*)(&src[pos]);
	headPtr->tag = 0x5b;
	headPtr->srcid = htonl(_srcid);
	headPtr->dstid = htonl(_dstid);
	headPtr->msgid = htons(msgid);
	headPtr->seqid = htonl(seqid);
	headPtr->length = htonl(src.size() - EXCH_HEAD_LEN);

	// 开辟消息尾部空间
	srclen = src.size();
	src.resize(srclen + EXCH_TAIL_LEN);

	srclen = src.size();
	pos = srclen - EXCH_TAIL_LEN;
	tailPtr = (ExchTail*)(&src[pos]);
	tailPtr->crc = getExchCrc(&src[1], srclen - 3);
	tailPtr->tag = 0x5d;

	dst.resize(srclen * 2);
	dst[0] = 0x5b;
	dstlen = enCode(&src[1], srclen - 2, &dst[1]);
	dst[dstlen + 1] = 0x5d;
	dst.resize(dstlen + 2);

	return seqid;
}

bool ProtParse::parseInnerParam(const string &detail, map<string, string> &params)
{
	if(detail.length() < 3) {
		return false;
	}

	if(*detail.begin() != '{' || *detail.rbegin() != '}') {
		return false;
	}

	size_t i;
	vector<string> fields;
	vector<string> keyVal;
	Utils::splitStr(detail.substr(1, detail.length() - 2), fields, ',');
	for(i = 0; i < fields.size(); ++i) {
		keyVal.clear();

		if(Utils::splitStr(fields[i], keyVal, ':') != 2) {
			continue;
		}

		params.insert(make_pair<string, string>(keyVal[0], keyVal[1]));
	}

	return true;
}
