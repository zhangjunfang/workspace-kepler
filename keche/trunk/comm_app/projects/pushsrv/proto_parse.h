/******************************************************
 *  CopyRight: 北京中交兴路科技有限公司(2012-2015)
 *   FileName: proto_parse.h
 *     Author: liubo  2012-5-16 
 *Description:
 *******************************************************/

#ifndef PROTO_PARSE_H_
#define PROTO_PARSE_H_

#include "proto_header.h"
#include <string>
#include <set>
#include <list>
#include <map>
#include <vector>

using namespace std;

typedef map<string, string> InternalTypeMap;
typedef struct _InterProto
{
	MSG_TYPE msg_type;
	string mac_id;
	string msg_seq;
	string oem_code;
	string car_id;
	string command;
	string type;
	string content;
	InternalTypeMap kvmap;
} InterProto;

typedef struct _NewProto
{
	//命令关键字
	unsigned short msg_type;
	unsigned short msg_len;
	unsigned int msg_seq;
	char * msg_data;
	string user_name;

} NewProto;

class CProtoTools
{
public:
};

class CInterProtoParse
{
public:
	CInterProtoParse(){}
	~CInterProtoParse(){}

	bool ParseProto(const char *data, int len, InterProto *inter_proto);


private:
	int split2map(vector<string> &vec, map<string, string> &mp, const char *split);

};

class CNewProtoParse
{
public:

	CNewProtoParse(){};
	virtual ~CNewProtoParse(){};

	bool ParseLoginMsg(NewProto *proto, string &compony_id, string &user_name);
    bool ParseSubsInfo(NewProto *proto, set<string> &set_subs);
	bool ParseProto(const char *data, int len, NewProto *new_proto);

	 string convert_macid(MAC_LIST *mac_list);
};

#endif /* PROTO_PARSE_H_ */
