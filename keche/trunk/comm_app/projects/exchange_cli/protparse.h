/*
 * protparse.h
 *
 *  Created on: 2014-5-19
 *      Author: ycq
 */

#ifndef _EXCH_PROTOCOL_H_
#define _EXCH_PROTOCOL_H_ 1

#include "interface.h"

#include <map>
#include <vector>
#include <string>

using std::map;
using std::vector;
using std::string;

class ProtParse : public IProtParse {
	static const size_t EXCH_HEAD_LEN;
	static const size_t EXCH_TAIL_LEN;
	static const size_t EXCH_MIN_LEN;

	// 环境指针处理
	ISystemEnv  *_pEnv ;
	size_t _srcid;  //分中心ID
	size_t _dstid;  //主中心ID
	size_t _seqid;  //消息序列号
	string _picpath; //图片存放路径
private:

	size_t getSeqid(size_t step = 1);
	unsigned char getExchCrc(const unsigned char *data, size_t size);

	uint32_t buildExchCommon(uint16_t msgid, vector<uint8_t> &src, vector<uint8_t> &dst);
	bool readMediaData(const string &file, vector<unsigned char> &data);
public:
	ProtParse();
	virtual ~ProtParse();

	bool Init(ISystemEnv *pEnv);

	size_t enCode(const unsigned char *src, size_t len, unsigned char *dst);
	size_t deCode(const unsigned char *src, size_t len, unsigned char *dst);

	bool parseInnerParam(const string &detail, map<string, string> &params);

	uint32_t buildExchLogin(const string &un, const string &pw, vector<unsigned char> &resBuf);
	uint32_t buildExchHeartBeat(vector<unsigned char> &resbuf);
	uint32_t buildExchSubUnits(vector<unsigned char> &resbuf);
	uint32_t buildExchSubCmdid(vector<unsigned char> &resbuf);

	uint32_t buildExchStatus(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf);
	uint32_t buildExchTermReg(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf);
	uint32_t buildExchTermAuth(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf);
	uint32_t buildExchLocation(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf);
	uint32_t buildExchHistory(const string &num, const map<string,string> &params, vector<unsigned char> &resBuf);
	uint32_t buildExchMMData(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf);
	uint32_t buildExchMMEvent(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf);
	uint32_t buildExchDriverEvent(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf);
	uint32_t buildExchTermVersion(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf);
	uint32_t buildExchTTData(const string &num, const map<string,string> &params, vector<unsigned char> &resbuf);

	uint32_t buildExchBaseTable(const string &str, uint16_t msgid, vector<uint8_t> &resbuf);
};

#endif//_EXCH_PROTOCOL_H_
