/*
 * Protocol.h
 *
 *  Created on: 2013-3-29
 *      Author: Administrator
 */

#ifndef _PROTOCOL_H_
#define _PROTOCOL_H_ 1

#include <map>
#include <list>
#include <vector>
#include <string>

using std::map;
using std::list;
using std::vector;
using std::string;

//#define GB808_MSG_DATA_LEN 1023
#define GB808_BCD_LEN 6

#define GB808_CMD_TERM_GENERAL_RSP       0x0001
#define GB808_CMD_PLAT_GENERAL_RSP       0x8001
#define GB808_CMD_TERM_HEART_BEAT        0x0002
#define GB808_CMD_TERM_REG_REQ           0x0100
#define GB808_CMD_TERM_REG_RSP           0x0100
#define GB808_CMD_TERM_LOGIN             0x0102
#define GB808_CMD_TERM_LOCATION          0x0200
#define GB808_CMD_PLAT_TEXT              0x8300
#define GB808_CMD_PLAT_CALL              0x8400
#define GB808_CMD_PLAT_CAR_CTRL_REQ      0x8500
#define GB808_CMD_TERM_CAR_CTRL_RSP      0x0500
#define GB808_CMD_TERM_DRIVER_INFO       0x0702
#define GB808_CMD_TERM_MEDIA_EVENT       0x0800
#define GB808_CMD_TERM_MEDIA_DATA_REQ    0x0801
#define GB808_CMD_PLAT_MEDIA_DATA_RSP    0x8800
#define GB808_CMD_PLAT_TACK_PHOTO        0x8801

#pragma pack(1)
struct Gb808Head {
	unsigned char  tag;
	unsigned short cmd;
	unsigned short len;
	unsigned char  num[GB808_BCD_LEN];
	unsigned short seq;
};

struct Gb808Tail {
	unsigned char crc;
	unsigned char tag;
};

struct Gb808Part {
	unsigned short total;
	unsigned short index;
};

struct Gb808GeneralRsp {
	unsigned short seq;
	unsigned short cmd;
	unsigned char  res;
};

struct Gb808String {
	unsigned char len;
	unsigned char val[0];
};

struct Gb808Register {
	unsigned short area;
	unsigned short city;
	char vendor[5];
	char type[20];
	char termid[7];
	char color;
	char plate[0];
};

struct Gb808Reg_Rsp {
	unsigned short seq;
	unsigned char res;
	char code[0];
};

struct Gb808Login {
	char code[0];
};

struct Gb808LocationMust {
	unsigned int   warn;
	unsigned int   stat;
	unsigned int   lat;
	unsigned int   lon;
	unsigned short height;
	unsigned short speed;
	unsigned short angle;
	unsigned char  time[GB808_BCD_LEN];
};

struct Gb808LocationItem {
	unsigned char key;
	unsigned char len;
	unsigned char val[0];
};

struct Gb808Text {
	unsigned char flag;
	unsigned char data[0];
};

struct Gb808Call {
	unsigned char flag;
	unsigned char data[0];
};

struct Gb808CarCtrlReq {
	unsigned char flag;
};

struct Gb808CarCtrlRsp {
	unsigned short    replyseq;
	Gb808LocationMust location;
};

#define DRV_NAME_LEN 20
#define ORG_NAME_LEN 40

struct Gb808DriverInfo {
	unsigned char drvNameLen;
	unsigned char drvNameStr[DRV_NAME_LEN];
	unsigned char drvCertificate[DRV_NAME_LEN];
	unsigned char orgCertificate[ORG_NAME_LEN];
	unsigned char orgNameLen;
	unsigned char orgNmaeStr[ORG_NAME_LEN];
};

struct Gb808MultiMediaEvent {
	unsigned int  mmid;
	unsigned char mmtype;
	unsigned char mmfmt;
	unsigned char event;
	unsigned char channel;
};

struct Gb808MultiMediaDataReq {
	unsigned int      mmid;
	unsigned char     mmtype;
	unsigned char     mmfmt;
	unsigned char     event;
	unsigned char     channel;
	Gb808LocationMust location;
	unsigned char     data[0];
};

struct Gb808MultiMediaDataRsp {
	unsigned int     mmid;
	unsigned char    size;
	unsigned short   index[0];
};

struct Gb808TakePhoto {
	unsigned char  channel;
	unsigned short command;
	unsigned short interval;
	unsigned char save;
	unsigned char ratio;
	unsigned char quality;
	unsigned char light;
	unsigned char contrast;
	unsigned char saturation;
	unsigned char chroma;
};

#pragma pack()

struct Gb808SimpleMsg {
	unsigned short msgid;
	unsigned short length;
	unsigned short seqid;
	string phone;
	vector<unsigned char> body;
};

struct MultiMediaData {
	unsigned short seqid;
	int count;
};

class Protocol {
	static const size_t MSG808_MIN_HEAD_LEN;
	static const size_t MSG808_MAX_HEAD_LEN;
	static const size_t MSG808_MAX_BODY_LEN;
	static const size_t MSG808_MIN_LEN;
	static const size_t MSG808_MAX_LEN;
	static const size_t TEL_NUM_LEN;
private:
	unsigned int  _seqid808;
	string        _mediaUrl;

	unsigned short getSeq808(size_t step = 1);
	unsigned char getCrc808(unsigned char *data, size_t size);

	size_t enCode808(const unsigned char *src, size_t len, unsigned char *dst);
	size_t deCode808(const unsigned char *src, size_t len, unsigned char *dst);

	bool readMediaData(const string &file, vector<unsigned char> &body);
	bool fixGb808Msg(const string &num, unsigned short msgid, const vector<unsigned char> &body,\
			map<string, vector<unsigned char> > &msgs, unsigned short &seqid);
public:
	Protocol();
	virtual ~Protocol();

	void init(const string &path);

	bool parseInnerParam(const string &detail, map<string, string> &params);

	bool buildGb808GeneralRsp(const Gb808SimpleMsg &simple, map<string, vector<unsigned char> > &msgs, unsigned char res);
	bool buildGb808HeartBeat(const string &num, map<string, vector<unsigned char> > &msg);
	bool buildGb808Register(const string &num, map<string,string> &params, map<string, vector<unsigned char> > &msgs);
	bool buildGb808Login(const string &num, const string &pwd, map<string, vector<unsigned char> > &msgs);
	bool buildGb808Location(const string &num, map<string,string> &params,\
			map<string, vector<unsigned char> > &msgs);
	bool buildGb808MultiMediaData(const string &num, map<string,string> &params,\
			map<string, vector<unsigned char> > &msgs, MultiMediaData &mmd);
	bool buildGb808MultiMediaEvent(const string &num, map<string,string> &params,\
			map<string, vector<unsigned char> > &msgs);

	bool prepareParseMsg(const string &userid, const unsigned char *data, size_t len, Gb808SimpleMsg &simpleMsg);
};

#endif /* PROTOCOL_H_ */
