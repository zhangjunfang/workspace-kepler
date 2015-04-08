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
	size_t _srcid;  //主中心ID
	size_t _seqid;  //消息序列号
private:

	size_t getSeqid(size_t step = 1);
	unsigned char getExchCrc(const unsigned char *data, size_t size);
	void buildExchCommon(uint16_t msgid, uint32_t dstid, vector<uint8_t> &src, vector<uint8_t> &dst);
public:
	ProtParse();
	virtual ~ProtParse();

	bool Init(ISystemEnv *pEnv);

	size_t enCode(const unsigned char *src, size_t len, unsigned char *dst);
	size_t deCode(const unsigned char *src, size_t len, unsigned char *dst);

	void buildExchGenReply(uint32_t seqid, uint32_t dstid, uint8_t res, vector<uint8_t> &msg);
};

#endif//_EXCH_PROTOCOL_H_
