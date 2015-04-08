/******************************************************
 *  CopyRight: 北京中交兴路科技有限公司(2012-2015)
 *   FileName: proto_parse.cpp
 *     Author: liubo  2012-5-16 
 *Description:
 *******************************************************/

#include "proto_parse.h"
#include "tools.h"
#include "BaseTools.h"
#include <arpa/inet.h>

static bool splitvectorex( const string &str, vector<string> &vec, char sign , int max )
{
    unsigned  int i;
    string::size_type len;
    const char *ptr;

    bool safe;

    string::size_type bpos;

    len = str.size();
    ptr = str.c_str();

    bpos = 0;
    safe = false;
    for(i = 0; i < len; ++i) {
        if(safe == false && ptr[i] == sign) {
            vec.push_back(str.substr(bpos, i - bpos));
            bpos = i + 1;
            continue;
        }
        else if(safe == false && ptr[i] == '[') {
            safe = true;
        }
        else if(safe == true && ptr[i] == ']') {
            safe = false;
        }
    }

    if(bpos < len) {
        vec.push_back(str.substr(bpos, len - bpos));
    }

    return true;
}

bool CInterProtoParse::ParseProto(const char *data, int len, InterProto *inter_proto)
{
    string line;
	line.assign(data, len);

	vector<string> vec_temp;
	splitvector(line, vec_temp, " ", 0);

	if (vec_temp.size() < 6)
		return false;

	inter_proto->msg_seq = vec_temp[1];
	inter_proto->mac_id = vec_temp[2];
	inter_proto->command = vec_temp[4];
	inter_proto->content = vec_temp[5];

	string::size_type pos = inter_proto->mac_id.find('_', 0);
	if (pos == string::npos)
	{
		return false;
	}

	// 取得车号
	inter_proto->car_id = inter_proto->mac_id.substr(pos + 1);
    inter_proto->oem_code = inter_proto->mac_id.substr(0,pos);

	//去掉两边的大括号
    inter_proto->content.assign(inter_proto->content, 1, inter_proto->content.length() - 2);

	vector<string> vk;
	//splitvector(inter_proto->content, vk, ",", 0);
	splitvectorex(inter_proto->content, vk, ',', 0);
	//解析参数保存至p_kv_map中
	if(split2map(vk, inter_proto->kvmap, ":") <= 0)
	{
		return false;
	}

	return true;
}

bool  CNewProtoParse::ParseLoginMsg(NewProto *proto, string &compony_id, string &user_name)
{
	char id[64] = {0};
	char name[32] = {0};

	LOGIN_DATA * login_data = (LOGIN_DATA*)proto->msg_data;

	snprintf(id, sizeof(login_data->compony_code), "%s", login_data->compony_code);
	snprintf(name, sizeof(login_data->user_name), "%s", login_data->user_name);

	compony_id = id;
	user_name = name;

	return true;
}

bool  CNewProtoParse::ParseSubsInfo(NewProto *proto, set<string> &set_subs)
{
	SUBSCRIBE_DATA *subs_data = (SUBSCRIBE_DATA*)proto->msg_data;
	unsigned short num = ntohs(subs_data->subs_num);
	if(proto->msg_len <  num * sizeof(MAC_LIST))
	{
		return false;
	}

	MAC_LIST *mac_list = &(subs_data->mac_list);
	for(int i = 0; i < num; i++)
	{
        string mac_id = convert_macid(mac_list + i);
        set_subs.insert(mac_id);
	}

	return true;
}

string CNewProtoParse::convert_macid(MAC_LIST *mac_list)
{
	char ome_code[8] = {0};
    snprintf(ome_code, sizeof(mac_list->oem_code), "%s", mac_list->oem_code);

    string phone = BCDtostr(mac_list->phone);
    phone = phone.substr(1);
    string mac_id = ome_code;
    mac_id += "_";
    mac_id += phone;

    return mac_id;
}

int CInterProtoParse::split2map(vector<string> &vec, map<string, string> &mp, const char *split)
{
	mp.clear();

	int count = 0;
	int len = strlen(split);

	map<string, string>::iterator it;

	for (int i = 0; i < (int) vec.size(); ++i)
	{
		size_t pos = vec[i].find(split, 0);

		if (pos == string::npos)
		{
			continue;
		}

		string key = vec[i].substr(0, pos);
		string value = vec[i].substr(pos + len);
		if (!value.empty())
		{
			++count;
		}
		it = mp.find(key);
		if (it != mp.end())
		{ // 如果存在多个同名则并列处理
			it->second += "|";
			it->second += value;
		}
		else
		{
			mp.insert(make_pair(key, value));
		}
	}

	return count;
}


bool CNewProtoParse::ParseProto(const char *data, int len, NewProto *new_proto)
{
    if(len < sizeof(COMM_HEADER))
    {
    	return false;
    }

    COMM_HEADER *header = (COMM_HEADER*)data;
    new_proto->msg_len = ntohs(header->msg_len);
    new_proto->msg_type = ntohs(header->msg_type);
    new_proto->msg_seq = ntohl(header->msg_seq);

    if(len < new_proto->msg_len)
    {
        return false;
    }

    new_proto->msg_data = (char*)(data + sizeof(COMM_HEADER));

    return true;
}
