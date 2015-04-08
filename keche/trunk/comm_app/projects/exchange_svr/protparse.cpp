/*
 * protparse.cpp
 *
 *  Created on: 2014-5-19
 *      Author: ycq
 */

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
	_seqid = 0;
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
	_srcid = atoi(szbuf);

	return true;
}

void ProtParse::buildExchGenReply(uint32_t seqid, uint32_t dstid, uint8_t res, vector<uint8_t> &msg)
{
	size_t tmpLen;
	vector<unsigned char> tmpBuf;
	ExchGenRsp *rsp;

	// 容器预开辟1k空间
	tmpBuf.reserve(1024);
	tmpBuf.clear();

	// 预留消息头空间
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + EXCH_HEAD_LEN);

	// 构造子类型对象
	tmpLen = tmpBuf.size();
	tmpBuf.resize(tmpLen + sizeof(ExchGenRsp));
	rsp = (ExchGenRsp*)&tmpBuf[tmpLen];
	rsp->seq = htonl(seqid);
	rsp->res = res;

	buildExchCommon(0xc001, dstid, tmpBuf, msg);
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

void ProtParse::buildExchCommon(uint16_t msgid, uint32_t dstid, vector<uint8_t> &src, vector<uint8_t> &dst)
{
	size_t pos;
	size_t srclen;
	size_t dstlen;

	ExchHead *headPtr;
	ExchTail *tailPtr;

	if(src.size() < EXCH_HEAD_LEN) {
		return;
	}

	pos = 0;
	headPtr = (ExchHead*)(&src[pos]);
	headPtr->tag = 0x5b;
	headPtr->srcid = htonl(_srcid);
	headPtr->dstid = htonl(dstid);
	headPtr->msgid = htons(msgid);
	headPtr->seqid = htonl(getSeqid());
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
}
