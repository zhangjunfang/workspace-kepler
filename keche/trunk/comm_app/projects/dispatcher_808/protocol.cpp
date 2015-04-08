/*
 * Protocol.cpp
 *
 *  Created on: 2013-3-29
 *      Author: ycq
 */
#include "httpquery.h"

#include "utils.h"
#include "protocol.h"

#include <utility>
using std::make_pair;

#include <arpa/inet.h>
#include <fcntl.h>
#include <unistd.h>
#include <sys/stat.h>

const size_t Protocol::MSG808_MIN_HEAD_LEN = sizeof(Gb808Head);
const size_t Protocol::MSG808_MAX_HEAD_LEN = sizeof(Gb808Head) + sizeof(Gb808Part);
const size_t Protocol::MSG808_MAX_BODY_LEN = 1023;
const size_t Protocol::MSG808_MIN_LEN = MSG808_MIN_HEAD_LEN + sizeof(Gb808Tail);
const size_t Protocol::MSG808_MAX_LEN = MSG808_MAX_HEAD_LEN + MSG808_MAX_BODY_LEN + sizeof(Gb808Tail);
const size_t Protocol::TEL_NUM_LEN = 11;

Protocol::Protocol() :_seqid808(0)
{
}

Protocol::~Protocol() {
}

void Protocol::init(const string &url)
{
	_mediaUrl = url;
}

bool Protocol::prepareParseMsg(const string &userid, const unsigned char *data, size_t len, Gb808SimpleMsg &simpleMsg)
{
	if (len < MSG808_MIN_LEN) {
		return false;
	}

	size_t msgLen;
	vector<unsigned char> msgBuf;
	msgBuf.resize(len);
	msgLen = deCode808(data, len, &msgBuf[0]);

	if (msgLen < MSG808_MIN_LEN) {
		return false;
	}

	Gb808Head *head = (Gb808Head*)&msgBuf[0];
	simpleMsg.msgid = htons(head->cmd);
	simpleMsg.length = htons(head->len);
	simpleMsg.seqid = htons(head->seq);
	simpleMsg.phone = Utils::array2hex(head->num, GB808_BCD_LEN).substr(1);

	if(simpleMsg.length & 0x2000) {
		return false;
	}

	if(simpleMsg.length + MSG808_MIN_LEN != msgLen) {
		return false;
	}

	simpleMsg.body.resize(simpleMsg.length);
	memcpy(&simpleMsg.body[0], &msgBuf[0] + MSG808_MIN_HEAD_LEN, simpleMsg.length);

	return true;
}

bool Protocol::buildGb808GeneralRsp(const Gb808SimpleMsg &simple, map<string, vector<unsigned char> > &msgs,  unsigned char res)
{
	const unsigned short msgid = GB808_CMD_TERM_GENERAL_RSP;

	unsigned short seqid;

	size_t lenBody;
	vector<unsigned char> msgBody(MSG808_MAX_BODY_LEN);

	Gb808GeneralRsp *genRsp;
	string num;

	lenBody = 0;
	msgBody.resize(sizeof(Gb808GeneralRsp));
	genRsp = (Gb808GeneralRsp*) &msgBody[lenBody];

	genRsp->seq = htons(simple.seqid);
	genRsp->cmd = htons(simple.msgid);
	genRsp->res = res;
	num = simple.phone;

	return fixGb808Msg(num, msgid, msgBody, msgs, seqid);
}

bool Protocol::buildGb808HeartBeat(const string &num, map<string, vector<unsigned char> > &msgs)
{
	const unsigned short msgid = GB808_CMD_TERM_HEART_BEAT;

	unsigned short seqid;

	return fixGb808Msg(num, msgid, vector<unsigned char>(), msgs, seqid);
}

bool Protocol::buildGb808Register(const string &num, map<string,string> &params, map<string, vector<unsigned char> > &msgs)
{
	const unsigned short msgid = GB808_CMD_TERM_REG_REQ;

	unsigned short seqid;

	size_t lenBody;
	vector<unsigned char> msgBody;

	string termID;
	string color;
	string plate;

	Gb808Register *reg;

	if(plate.length() > 128) {
		return false;
	}

	msgBody.reserve(MSG808_MAX_BODY_LEN);

	termID = params["44"];
	color  = params["202"];
	plate  = params["104"];

	lenBody = 0;
	msgBody.resize(sizeof(Gb808Register) + plate.length());
	reg = (Gb808Register*)&msgBody[lenBody];

	termID.copy(reg->termid, 7);
	reg->color = atoi(color.c_str());
	plate.copy(reg->plate, plate.length());

	return fixGb808Msg(num, msgid, msgBody, msgs, seqid);
}

bool Protocol::buildGb808Login(const string &num, const string &pwd, map<string, vector<unsigned char> > &msgs)
{
	const unsigned short msgid = GB808_CMD_TERM_LOGIN;

	unsigned short seqid;

	size_t lenBody;
	vector<unsigned char> msgBody;

	if(pwd.length() > MSG808_MAX_BODY_LEN) {
		return false;
	}

	Gb808Login *login;

	msgBody.reserve(MSG808_MAX_BODY_LEN);

	lenBody = 0;
	msgBody.resize(pwd.size());
	login = (Gb808Login*)&msgBody[lenBody];

	memcpy(login->code, pwd.c_str(), pwd.length());

	return fixGb808Msg(num, msgid, msgBody, msgs, seqid);
}

bool Protocol::buildGb808Location(const string &num, map<string,string> &params, map<string, vector<unsigned char> > &msgs)
{
	const unsigned short msgid = GB808_CMD_TERM_LOCATION;

	unsigned short seqid;

	size_t lenBody;
	vector<unsigned char> msgBody;

	string strVal;
	unsigned int uint32Val;
	unsigned short uint16Val;

	Gb808LocationMust *locMust;
	Gb808LocationItem *locItem;

	msgBody.reserve(MSG808_MAX_BODY_LEN);

	lenBody = 0;
	msgBody.resize(lenBody + sizeof(Gb808LocationMust));
	locMust = (Gb808LocationMust*)&msgBody[lenBody];

	strVal = params["20"];
	if (strVal.empty()) {
		return false;
	}
	locMust->warn = htonl(atoi(strVal.c_str()));

	strVal = params["8"];
	if (strVal.empty()) {
		return false;
	}
	locMust->stat = htonl(atoi(strVal.c_str()));

	strVal = params["2"];
	if (strVal.empty()) {
		return false;
	}
	locMust->lat = htonl(int(atoi(strVal.c_str()) * 10.0 / 6));

	strVal = params["1"];
	if (strVal.empty()) {
		return false;
	}
	locMust->lon = htonl(int(atoi(strVal.c_str()) * 10.0 / 6));

	strVal = params["6"];
	if (strVal.empty()) {
		return false;
	}
	locMust->height = htons(atoi(strVal.c_str()));

	strVal = params["3"];
	if (strVal.empty()) {
		return false;
	}
	locMust->speed = htons(atoi(strVal.c_str()));

	strVal = params["5"];
	if (strVal.empty()) {
		return false;
	}
	locMust->angle = htons(atoi(strVal.c_str()));

	strVal = params["4"];
	if (strVal.length() != 15) {
		return false;
	}
	strVal = strVal.substr(2, 6) + strVal.substr(9, 6);
	Utils::hex2array(strVal, locMust->time);

	strVal = params["9"];
	if (!strVal.empty()) {
		lenBody = msgBody.size();
		msgBody.resize(lenBody + sizeof(Gb808LocationItem) + sizeof(int));
		locItem = (Gb808LocationItem*)&msgBody[lenBody];
		locItem->key = 1;
		locItem->len = sizeof(int);
		uint32Val = htonl(atoi(strVal.c_str()));
		memcpy(locItem->val, &uint32Val, sizeof(int));
	}

	strVal = params["24"];
	if (!strVal.empty()) {
		lenBody = msgBody.size();
		msgBody.resize(lenBody + sizeof(Gb808LocationItem) + sizeof(short));
		locItem = (Gb808LocationItem*)&msgBody[lenBody];
		locItem->key = 2;
		locItem->len = sizeof(short);
		uint16Val = htons(atoi(strVal.c_str()));
		memcpy(locItem->val, &uint16Val, sizeof(short));
	}

	strVal = params["7"];
	if (!strVal.empty()) {
		lenBody = msgBody.size();
		msgBody.resize(lenBody + sizeof(Gb808LocationItem) + sizeof(short));
		locItem = (Gb808LocationItem*)&msgBody[lenBody];
		locItem->key = 3;
		locItem->len = sizeof(short);
		uint16Val = htons(atoi(strVal.c_str()));
		memcpy(locItem->val, &uint16Val, sizeof(short));
	}

	return fixGb808Msg(num, msgid, msgBody, msgs, seqid);
}

bool Protocol::buildGb808MultiMediaEvent(const string &num, map<string,string> &params, map<string, vector<unsigned char> > &msgs)
{
	const unsigned short msgid = GB808_CMD_TERM_MEDIA_EVENT;

	unsigned short seqid;

	size_t lenBody;
	vector<unsigned char> msgBody;

	string         strVal;

	Gb808MultiMediaEvent *mmEvent;

	msgBody.reserve(1024 * 1024);

	lenBody = 0;
	msgBody.resize(lenBody + sizeof(Gb808MultiMediaEvent));
	mmEvent = (Gb808MultiMediaEvent*)&msgBody[lenBody];

	strVal = params["120"];
	if(strVal.empty()) {
		return false;
	}
	mmEvent->mmid = htonl(atoi(strVal.c_str()));

	strVal = params["121"];
	if(strVal.empty()) {
		return false;
	}
	mmEvent->mmtype = atoi(strVal.c_str());

	strVal = params["122"];
	if(strVal.empty()) {
		return false;
	}
	mmEvent->mmfmt = atoi(strVal.c_str());

	strVal = params["123"];
	if(strVal.empty()) {
		return false;
	}
	mmEvent->event = atoi(strVal.c_str()) & 0x7f;

	strVal = params["124"];
	if(strVal.empty()) {
		return false;
	}
	mmEvent->channel = atoi(strVal.c_str());

	return fixGb808Msg(num, msgid, msgBody, msgs, seqid);
}

bool Protocol::buildGb808MultiMediaData(const string &num, map<string,string> &params, map<string, vector<unsigned char> > &msgs, MultiMediaData &mmd)
{
	const unsigned short msgid = GB808_CMD_TERM_MEDIA_DATA_REQ;

	unsigned short seqid;

	size_t lenBody;
	vector<unsigned char> msgBody;

	string         strVal;

	Gb808MultiMediaDataReq *mmDataReq;

	msgBody.reserve(1024 * 1024);

	lenBody = 0;
	msgBody.resize(lenBody + sizeof(Gb808MultiMediaDataReq));
	mmDataReq = (Gb808MultiMediaDataReq*)&msgBody[lenBody];

	strVal = params["120"];
	if(strVal.empty()) {
		return false;
	}
	mmDataReq->mmid = htonl(atoi(strVal.c_str()));

	strVal = params["121"];
	if(strVal.empty()) {
		return false;
	}
	mmDataReq->mmtype = atoi(strVal.c_str());

	strVal = params["122"];
	if(strVal.empty()) {
		return false;
	}
	mmDataReq->mmfmt = atoi(strVal.c_str());

	strVal = params["123"];
	if(strVal.empty()) {
		return false;
	}
	mmDataReq->event = atoi(strVal.c_str()) & 0x7f;

	strVal = params["124"];
	if(strVal.empty()) {
		return false;
	}
	mmDataReq->channel = atoi(strVal.c_str());

	strVal = params["20"];
	if (strVal.empty()) {
		return false;
	}
	mmDataReq->location.warn = htonl(atoi(strVal.c_str()));

	strVal = params["8"];
	if (strVal.empty()) {
		return false;
	}
	mmDataReq->location.stat = htonl(atoi(strVal.c_str()));

	strVal = params["2"];
	if (strVal.empty()) {
		return false;
	}
	mmDataReq->location.lat = htonl(int(atoi(strVal.c_str()) * 10.0 / 6));

	strVal = params["1"];
	if (strVal.empty()) {
		return false;
	}
	mmDataReq->location.lon = htonl(int(atoi(strVal.c_str()) * 10.0 / 6));

	strVal = params["6"];
	if (strVal.empty()) {
		return false;
	}
	mmDataReq->location.height = htons(atoi(strVal.c_str()));

	strVal = params["3"];
	if (strVal.empty()) {
		return false;
	}
	mmDataReq->location.speed = htons(atoi(strVal.c_str()));

	strVal = params["5"];
	if (strVal.empty()) {
		return false;
	}
	mmDataReq->location.angle = htons(atoi(strVal.c_str()));

	strVal = params["4"];
	if (strVal.length() != 15) {
		return false;
	}
	strVal = strVal.substr(2, 6) + strVal.substr(9, 6);
	Utils::hex2array(strVal, mmDataReq->location.time);

	strVal = params["125"];
	if(strVal.empty()) {
		return false;
	}

	if( ! readMediaData(_mediaUrl + "/" + strVal, msgBody)) {
		return false;
	}

	if( ! fixGb808Msg(num, msgid, msgBody, msgs, seqid)) {
		return false;
	}

	mmd.count = msgs.size();
	mmd.seqid = seqid;

	return true;
}

bool Protocol::readMediaData(const string &file, vector<unsigned char> &body)
{
	size_t size = body.size();

	if(file.compare(0, 7, "http://") == 0) {
		HttpQuery httpQuery;

		if (!httpQuery.get(file)) {
			return false;
		}

		body.resize(size + httpQuery.size());
		memcpy(&body[size], httpQuery.data(), httpQuery.size());
	} else {
		int fd;
		struct stat fs;

		if(stat(file.c_str(), &fs) != 0) {
			return false;
		}

		if((fd = open(file.c_str(), O_RDONLY)) < 0) {
			return false;
		}

		body.resize(size + fs.st_size);
		if(read(fd, &body[size], fs.st_size) != fs.st_size) {
			close(fd);
			return false;
		}
		close(fd);
	}

	return true;
}

bool Protocol::fixGb808Msg(const string &num, unsigned short msgid, const vector<unsigned char> &body, map<string, vector<unsigned char> > &msgs, unsigned short &seqid)
{
	string pid = num;
	string sim = "0" + num; //BCD长度不够前面补0

	vector<string> fields;
	if(Utils::splitStr(num, fields, '_') == 2) {
		pid = fields[1];
		sim = "0" + fields[1]; //BCD长度不够前面补0
	}

	if(sim.length() != TEL_NUM_LEN + 1) {
		return false;
	}

	size_t lenSrc;
	size_t lenDst;
	vector<unsigned char> msgSrc;
	vector<unsigned char> msgDst;

	unsigned short index;
	unsigned short total;

	Gb808Head *head;
	Gb808Tail *tail;
	Gb808Part *part;

	size_t partLen;
	size_t bodyPos = 0;
	const size_t bodyLen = body.size();

	total = 1;
	if(bodyLen > 0) {
		total = bodyLen / MSG808_MAX_BODY_LEN + ((bodyLen % MSG808_MAX_BODY_LEN) ? 1 : 0);
	}
	seqid = getSeq808(total);

	string key;
	string valStr;

	msgDst.reserve(MSG808_MAX_LEN * 2);
	msgSrc.reserve(MSG808_MAX_LEN);
	for(index = 0; index < total; ++index ) {
		partLen = bodyLen - bodyPos;
		if(bodyPos + MSG808_MAX_BODY_LEN < bodyLen) {
			partLen = MSG808_MAX_BODY_LEN;
		}

		lenSrc = 0;
		msgSrc.resize(MSG808_MIN_HEAD_LEN, 0);
		head = (Gb808Head*)&msgSrc[lenSrc];
		head->tag = 0x7e;
		head->cmd = htons(msgid);
		head->len = htons(partLen);
		Utils::hex2array(sim, head->num);
		head->seq = htons(seqid + index);

		if(total > 1) {
			lenSrc = msgSrc.size();
			msgSrc.resize(lenSrc + sizeof(Gb808Part));
			part = (Gb808Part*)&msgSrc[lenSrc];
			part->total = htons(total);
			part->index = htons(index + 1);

			head->len = htons(partLen | (1u << 13));
		}

		lenSrc = msgSrc.size();
		msgSrc.resize(lenSrc + partLen, 0);
		memcpy(&msgSrc[lenSrc], &body[bodyPos], partLen);

		lenSrc = msgSrc.size();
		msgSrc.resize(lenSrc + sizeof(Gb808Tail), 0);
		tail = (Gb808Tail*)&msgSrc[lenSrc];
		tail->crc = getCrc808(&msgSrc[0], msgSrc.size());
		tail->tag = 0x7e;

		lenSrc = msgSrc.size();
		msgDst.resize(lenSrc * 2, 0);
		lenDst = enCode808(&msgSrc[0], lenSrc, &msgDst[0]);
		msgDst.resize(lenDst);

		//msgs.push_back(msgDst);
		key = pid + "_" + Utils::int2str(seqid + index, valStr);
		msgs.insert(make_pair(key, msgDst));

		bodyPos += partLen;
	}

	return true;
}

unsigned short Protocol::getSeq808(size_t step)
{
	return __sync_fetch_and_add(&_seqid808, step) % 0xffff;
}

unsigned char Protocol::getCrc808(unsigned char *data, size_t size)
{
	size_t i;
	unsigned char xorVal = 0;

	for(i = 1; i < size; ++i) {
		xorVal ^= data[i];
	}

	return xorVal;
}

size_t Protocol::enCode808(const unsigned char *src, size_t len, unsigned char *dst)
{
	size_t i, j;

	dst[0] = src[0];
	for (i = j = 1; i < len - 1; ++i) {
		switch (src[i]) {
		case 0x7d:
			dst[j++] = 0x7d;
			dst[j++] = 0x01;
			break;
		case 0x7e:
			dst[j++] = 0x7d;
			dst[j++] = 0x02;
			break;
		default:
			dst[j++] = src[i];
			break;
		}
	}
	dst[j++] = src[i];

	return j;
}

size_t Protocol::deCode808(const unsigned char *src, size_t len, unsigned char *dst)
{
	size_t i, j;
	int flag;

	flag = 0;
	dst[0] = src[0];
	for (i = j = 1; i < len - 1; ++i) {
		if(src[i] == 0x7d) {
			flag = 1;
			continue;
		}

		if(flag == 0) {
			dst[j++] = src[i];
		} else if(flag == 1 && src[i] == 0x01) {
			dst[j++] = 0x7d;
		} else if(flag == 1 && src[i] == 0x02) {
			dst[j++] = 0x7e;
		}
		flag = 0;
	}
	dst[j++] = src[i];

	return j;
}

bool Protocol::parseInnerParam(const string &detail, map<string, string> &params)
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
