#include "proto_convert.h"
#include "BaseTools.h"
#include "Base64.h"
#include <time.h>

static unsigned char * xorcode(unsigned char c, unsigned char *s, unsigned int len)
{
	for (unsigned int i = 0; i < len; ++i)
	{
		s[i] = s[i] ^ c;
	}
	return s;
}

static int splitStr(char *str, char *signs, char **set, int max)
{
	int n;
	int pos;
	int off;

	char *prev;
	char *next;
	unsigned char ch;
	unsigned char buf[32];

	memset(buf, 0x00, 32);
	for(next = signs; *next; ++next) {
		ch = *next;
		pos = ch / 8;
		off = ch % 8;
		buf[pos] |= 1u << off;
	}

	n = 0;
	for(prev = next = str; n < max && *next; ++next) {
		ch = *next;
		pos = ch / 8;
		off = ch % 8;
		if((buf[pos] & (1u << off)) == 0) {
			continue;
		}

		if(prev == next) {
			++prev;
			continue;
		}

		*next = 0;
		set[n++] = prev;
		prev = next + 1;
	}

	if(n < max && *prev) {
		set[n++] = prev;
	}

	return n;
}

static uint8_t _hexstrtab[256] = {
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
};

void get_hex_from_str(unsigned char &byValue, string &s_str)
{
	//sscanf(s_str.c_str(), "%02x", &byValue);

	uint8_t h, l;
	const char *str;

	h = 0, l = 0;
	str = s_str.c_str();
	switch(s_str.length()) {
	case 1:
		l = str[0];
		break;
	case 2:
		h = str[0];
		l = str[1];
		break;
	}

	byValue = _hexstrtab[h] << 4 | _hexstrtab[l];
}

void get_bcdphone_from_internalproto(unsigned char *lpPhone, string &str_phone)
{
	string dest_str = "";
	string temp = "";
	temp = "0" + str_phone;

	int pos = 0;

	for (size_t i = 0; i < temp.length(); i += 2)
	{
		dest_str = temp.substr(i, 2);
		get_hex_from_str(lpPhone[pos], dest_str);
		pos++;
	}
}

void get_bcdtime_from_internalproto(unsigned char *lptime, string &str_time)
{
	string dest_str = "";
	size_t pos = str_time.find("/");

	if (pos == string::npos)
	{
		return;
	}
	else
	{

		dest_str = uitodecstr(atoi(str_time.substr(0, 4).c_str()) - 2000); //年
		get_hex_from_str(lptime[0], dest_str);

		dest_str = str_time.substr(4, 2); //月
		get_hex_from_str(lptime[1], dest_str);

		dest_str = str_time.substr(6, 2); //日
		get_hex_from_str(lptime[2], dest_str);

		dest_str = str_time.substr(pos + 1, 2); //小时
		get_hex_from_str(lptime[3], dest_str);

		dest_str = str_time.substr(pos + 1 + 2, 2); //分钟
		get_hex_from_str(lptime[4], dest_str);

		dest_str = str_time.substr(pos + 1 + 4, 2); //秒
		get_hex_from_str(lptime[5], dest_str);

	}
}

void ConvertProto::init()
{
	_new_convert_table.insert(make_pair(MSG_REQUEST_LOCATION, &ConvertProto::NewConvertReqGpsInfo));
	_new_convert_table.insert(make_pair(MSG_REQUEST_TRACE, &ConvertProto::NewConvertTrace));
	_new_convert_table.insert(make_pair(MSG_REQUEST_TEXT, &ConvertProto::NewConvertText));
	_new_convert_table.insert(make_pair(MSG_REQUEST_MONITOR, &ConvertProto::NewConvertMonitor));
//	_new_convert_table.insert(make_pair(MSG_REQUEST_MEDIA_UP, NewConvertTrace));
	_new_convert_table.insert(make_pair(MSG_REQUEST_GET_PHOTO, &ConvertProto::NewConvertGetPhoto));
	_new_convert_table.insert(make_pair(MSG_REQUEST_RECORD, & ConvertProto::NewConvertRecord));
	_new_convert_table.insert(make_pair(MSG_REQUEST_SET_PARAM, &ConvertProto::NewConvertSetParam));
	_new_convert_table.insert(make_pair(MSG_REQUEST_GET_PARAM, &ConvertProto::NewConvertGetParam));
	_new_convert_table.insert(make_pair(MSG_REQUEST_TERM_CTRL, &ConvertProto::NewConvertTermControl));
	_new_convert_table.insert(make_pair(MSG_REQUEST_SET_EVENT, &ConvertProto::NewConvertSetEvent));
	_new_convert_table.insert(make_pair(MSG_REQUEST_QUESTION_ASK, &ConvertProto::NewConvertQuestionAsk));
	_new_convert_table.insert(make_pair(MSG_REQUEST_INFO_MENU, &ConvertProto::NewConvertInfoMenu));
	_new_convert_table.insert(make_pair(MSG_REQUEST_INFO_SEND, &ConvertProto::NewConvertInfoSend));
	_new_convert_table.insert(make_pair(MSG_REQUEST_PHONE_BOOK, &ConvertProto::NewConvertPhoneBook));
	_new_convert_table.insert(make_pair(MSG_REQUEST_CAR_CONTROL, &ConvertProto::NewConvertCarControl));
	_new_convert_table.insert(make_pair(MSG_REQUEST_SET_CIRCLE, &ConvertProto::NewConvertSetCircle));
	_new_convert_table.insert(make_pair(MSG_REQUEST_DEL_CIRCLE, &ConvertProto::NewConvertDelCircle));
	_new_convert_table.insert(make_pair(MSG_REQUEST_SET_RECTANGLE, &ConvertProto::NewConvertSetRectangle));
	_new_convert_table.insert(make_pair(MSG_REQUEST_DEL_RECTANGLE, &ConvertProto::NewConvertDelRectangle));
	_new_convert_table.insert(make_pair(MSG_REQUEST_SET_POLYGON, &ConvertProto::NewConvertSetPolygon));
	_new_convert_table.insert(make_pair(MSG_REQUEST_DEL_POLYGON, &ConvertProto::NewConvertDelPolygon));
	_new_convert_table.insert(make_pair(MSG_REQUEST_SET_LINE, &ConvertProto::NewConvertSetLine));
	_new_convert_table.insert(make_pair(MSG_REQUEST_DEL_LINE, &ConvertProto::NewConvertDelLine));
	_new_convert_table.insert(make_pair(MSG_REQUEST_DRIVE_COLLECT, &ConvertProto::NewConvertDriveCollect));
	_new_convert_table.insert(make_pair(MSG_REQUEST_DRIVE_PARAM, &ConvertProto::NewConvertDriveParam));
	_new_convert_table.insert(make_pair(MSG_REQUEST_MEDIA_SEARCH, &ConvertProto::NewConvertMediaSearch));
	_new_convert_table.insert(make_pair(MSG_REQUEST_SINGLE_UPLOAD, &ConvertProto::NewConvertSingleUpload));
	_new_convert_table.insert(make_pair(MSG_REQUEST_MULTI_UPLOAD, &ConvertProto::NewConvertMultiUpload));
}

ConvertProto::NewConvertFun ConvertProto::GetNewConvertFun(unsigned short msg_type)
{
	map<unsigned short, NewConvertFun>::iterator iter = _new_convert_table.find(msg_type);

	if(iter == _new_convert_table.end())
	{
		return NULL;
	}

	return iter->second;
}

bool ConvertProto::InterProto2NewProto(InterProto *inter_proto, NewProtoOut *out)
{
	int type;
	map<string, string>::iterator ite;

	ite = inter_proto->kvmap.find("TYPE");
	if (ite == inter_proto->kvmap.end()) {
		return false;
	}
	type = atoi(ite->second.c_str());

	if (inter_proto->command == "U_REPT") {
		switch (type) {
		case 0:
			return InterConvertLocation(inter_proto, out);
		case 1:
			return InterConvertLocation(inter_proto, out);
		case 3:
			return InterConvertMedia(inter_proto, out);
		case 5:
			return InterConvertQuery(inter_proto, out);
		case 8:
			return InterConvertIdentityCollect(inter_proto, out);
		case 9:
			return InterConvertTransDeliver(inter_proto, out);
		case 31:
			return InterConvertEventReport(inter_proto, out);
		case 32:
			return InterConvertQuestionAck(inter_proto, out);
		case 33:
			return InterConvertInfoResp(inter_proto, out);
		case 35:
			return InterConvertListReport(inter_proto, out);
		case 39:
			return InterConvertMediaEvent(inter_proto, out);
		}
	}
	else if(inter_proto->command == "D_GETP") {
		switch(type) {
		case 0:
			return InterConvertGetParam(inter_proto, out);
		}
	}
	else if(inter_proto->command == "D_REQD") {
		switch(type) {
		case 1:
			return InterConvertSearchResp(inter_proto, out);
		}
	}

    return false;
}

bool ConvertProto::NewProto2InterProto(NewProto *new_proto, InterProtoOut *out)
{
	map<unsigned short, NewConvertFun>::iterator iter = _new_convert_table.find(new_proto->msg_type);

	if(iter == _new_convert_table.end())
	{
		return false;
	}

	return (this->*(iter->second))(new_proto, out);
}

bool ConvertProto::NewConvertReqGpsInfo(NewProto *new_proto, InterProtoOut *out)
{
    MAC_LIST *mac_list = (MAC_LIST*)new_proto->msg_data;
	out->seq  = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
    out->mac_id    = _new_parse.convert_macid(mac_list);

    out->msg = "CAITS " + out->seq  + " " + out->mac_id  + " 0 D_CALL {TYPE:2} \r\n";

    return true;
}

bool ConvertProto::NewConvertMonitor(NewProto *new_proto, InterProtoOut *out)
{
	PHONE_MONITOR *monitor = (PHONE_MONITOR*)new_proto->msg_data;
	out->seq  = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
    out->mac_id    = _new_parse.convert_macid(&(monitor->mac_list));

    char phone[32] = {0};
    snprintf(phone, sizeof(monitor->phone), "%s", monitor->phone);

    out->msg = "CAITS " + out->seq + " " + out->mac_id + " 0 D_CTLM {TYPE:";

    if(monitor->flag == 0) //普通通话
        out->msg += "16";
    else //监听
        out->msg += "9";

    out->msg += ",VALUE:";
    out->msg += phone;
    out->msg += "} \r\n";

    return true;
}

bool ConvertProto::NewConvertTrace(NewProto *new_proto, InterProtoOut *out)
{
    // 转成定时定次回传 {TYPE:0,0:interval}
	LOCATION_TRACE *trace = (LOCATION_TRACE*)new_proto->msg_data;

	out->seq  = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
    out->mac_id    = _new_parse.convert_macid(&(trace->mac_list));
    string interval = uitodecstr(ntohs(trace->interval));
    string valid_time = uitodecstr(ntohl(trace->valid_time));

    string num = uitodecstr( ntohl(trace->valid_time) / ntohs(trace->interval));

    out->msg = "CAITS " + out->seq  + " " + out->mac_id  + " 0 D_CALL {TYPE:0,0:" + interval
    		+ ",1:" + num + "} \r\n";

	return true;
}

bool ConvertProto::NewConvertText(NewProto *new_proto, InterProtoOut *out)
{
	//下发文本： CAITS 0_00000_00001 4C54_15010426784 0 D_SNDM {TYPE:1,1:0,2:QkFTRTY0}
	CBase64 base;

	DOWN_TEXT *down_text = (DOWN_TEXT *)new_proto->msg_data;
	char text[1024] = {0};

    string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
    string mac_id = _new_parse.convert_macid(&(down_text->mac_list));

    int len = new_proto->msg_len - 7;
    snprintf(text, len, "%s", down_text->text);
    base.Encode(text, strlen(text));
    out->msg += "CAITS " + msg_seq + " " + mac_id  + " 0 D_SNDM {TYPE:1,1:0,2:";
    out->msg += base.GetBuffer();
    out->msg += "} \r\n";

    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

typedef union _ITEM_DATA {
	unsigned char u08;
	unsigned short u16;
	unsigned int u32;
} ITEM_DATA;

bool ConvertProto::NewConvertGetParam(NewProto *new_proto, InterProtoOut *out)
{
	GET_PARAM *param = (GET_PARAM*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&param->macid);

	out->msg = "CAITS " + msg_seq + " " + mac_id + " 0 D_GETP {TYPE:0} \r\n";
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertSetParam(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];
	int dataLen;
	ITEM_DATA dataPtr[1];

	int pos;
	PARAM_ITEM *item;
	SET_PARAM *param = (SET_PARAM*)new_proto->msg_data;

	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&param->macid);
	out->msg = "CAITS " + msg_seq + " " + mac_id + " 0 D_SETP {TYPE:0";

	dataLen = sizeof(ITEM_DATA);

	buflen = 0;
	pos = sizeof(SET_PARAM);
	for(i = 0; i < param->num && pos < new_proto->msg_len; ++i) {
		item = (PARAM_ITEM*)(new_proto->msg_data + pos);
		item->type = ntohl(item->type);

		if(dataLen >= item->size) {
			memcpy(dataPtr, item->date, item->size);
		}

		buffer[0] = '\0';
		switch(item->type) {
		case 0x0001:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",7:%u", ntohl(dataPtr->u32));
			break;
		case 0x0002:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",100:%u", ntohl(dataPtr->u32));
			break;
		case 0x0003:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",101:%u", ntohl(dataPtr->u32));
			break;
		case 0x0004:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",102:%u", ntohl(dataPtr->u32));
			break;
		case 0x0005:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",103:%u", ntohl(dataPtr->u32));
			break;
		case 0x0006:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",104:%u", ntohl(dataPtr->u32));
			break;
		case 0x0007:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",105:%u", ntohl(dataPtr->u32));
			break;
		case 0x0010:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",3:%.*s", item->size, item->date);
			break;
		case 0x0011:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",4:%.*s", item->size, item->date);
			break;
		case 0x0012:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",5:%.*s", item->size, item->date);
			break;
		case 0x0013:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",0:%.*s", item->size, item->date);
			break;
		case 0x0014:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",106:%.*s", item->size, item->date);
			break;
		case 0x0015:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",107:%.*s", item->size, item->date);
			break;
		case 0x0016:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",108:%.*s", item->size, item->date);
			break;
		case 0x0017:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",109:%.*s", item->size, item->date);
			break;
		case 0x0018:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",1:%u", ntohl(dataPtr->u32));
			break;
		case 0x0019:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",110:%u", ntohl(dataPtr->u32));
			break;
		case 0x0020:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",111:%u", ntohl(dataPtr->u32));
			break;
		case 0x0021:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",112:%u", ntohl(dataPtr->u32));
			break;
		case 0x0022:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",113:%u", ntohl(dataPtr->u32));
			break;
		case 0x0027:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",114:%u", ntohl(dataPtr->u32));
			break;
		case 0x0028:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",115:%u", ntohl(dataPtr->u32));
			break;
		case 0x0029:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",116:%u", ntohl(dataPtr->u32));
			break;
		case 0x002c:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",117:%u", ntohl(dataPtr->u32));
			break;
		case 0x002d:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",118:%u", ntohl(dataPtr->u32));
			break;
		case 0x002e:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",119:%u", ntohl(dataPtr->u32));
			break;
		case 0x002f:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",120:%u", ntohl(dataPtr->u32));
			break;
		case 0x0030:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",121:%u", ntohl(dataPtr->u32));
			break;
		case 0x0040:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",10:%.*s", item->size, item->date);
			break;
		case 0x0041:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",122:%.*s", item->size, item->date);
			break;
		case 0x0042:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",123:%.*s", item->size, item->date);
			break;
		case 0x0043:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",15:%.*s", item->size, item->date);
			break;
		case 0x0044:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",124:%.*s", item->size, item->date);
			break;
		case 0x0045:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",125:%u", ntohl(dataPtr->u32));
			break;
		case 0x0046:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",126:%u", ntohl(dataPtr->u32));
			break;
		case 0x0047:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",127:%u", ntohl(dataPtr->u32));
			break;
		case 0x0048:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",9:%.*s", item->size, item->date);
			break;
		case 0x0049:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",141:%.*s", item->size, item->date);
			break;
		case 0x0050:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",142:%u", ntohl(dataPtr->u32));
			break;
		case 0x0051:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",143:%u", ntohl(dataPtr->u32));
			break;
		case 0x0052:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",144:%u", ntohl(dataPtr->u32));
			break;
		case 0x0053:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",145:%u", ntohl(dataPtr->u32));
			break;
		case 0x0054:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",146:%u", ntohl(dataPtr->u32));
			break;
		case 0x0055:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",128:%u", ntohl(dataPtr->u32));
			break;
		case 0x0056:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",129:%u", ntohl(dataPtr->u32));
			break;
		case 0x0057:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",130:%u", ntohl(dataPtr->u32));
			break;
		case 0x0058:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",131:%u", ntohl(dataPtr->u32));
			break;
		case 0x0059:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",132:%u", ntohl(dataPtr->u32));
			break;
		case 0x005a:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",133:%u", ntohl(dataPtr->u32));
			break;
		case 0x005b:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",300:%u", ntohl(dataPtr->u16));
			break;
		case 0x005c:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",301:%u", ntohl(dataPtr->u16));
			break;
		case 0x005d:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",302:%u", ntohl(dataPtr->u16));
			break;
		case 0x005e:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",303:%u", ntohl(dataPtr->u16));
			break;
		case 0x005f:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",304:%u", ntohl(dataPtr->u16));
			break;
		case 0x0063:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",308:%u", ntohl(dataPtr->u32));
			break;
		case 0x0070:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",136:%u", ntohl(dataPtr->u32));
			break;
		case 0x0071:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",137:%u", ntohl(dataPtr->u32));
			break;
		case 0x0072:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",138:%u", ntohl(dataPtr->u32));
			break;
		case 0x0073:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",139:%u", ntohl(dataPtr->u32));
			break;
		case 0x0074:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",140:%u", ntohl(dataPtr->u32));
			break;
		case 0x0080:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",147:%u", ntohl(dataPtr->u32));
			break;
		case 0x0081:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",134:%u", ntohl(dataPtr->u16));
			break;
		case 0x0082:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",135:%u", ntohl(dataPtr->u16));
			break;
		case 0x0083:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",41:%.*s", item->size, item->date);
			break;
		case 0x0084:
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",42:%u", dataPtr->u08);
			break;
		}

		pos += sizeof(PARAM_ITEM) + item->size;
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg += buffer;
	out->mac_id = mac_id;
	out->seq = msg_seq;

	printf("NewConvertSetParam: %s\n", out->msg.c_str());

	return true;
}

bool ConvertProto::NewConvertTermControl(NewProto *new_proto, InterProtoOut *out)
{
	int buflen;
	char buffer[BUFSIZ + 1];

	int argLen;
	char *argPtr;
	unsigned char type;
	TERM_CONTROL *ctrl = (TERM_CONTROL*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&ctrl->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_CTLM ", msg_seq.c_str(), mac_id.c_str());
	//D_CTLM {TYPE:20,VALUE:param}   升级
	//D_CTLM {TYPE:21,VALUE:param}   连接
	//D_CTLM {TYPE:15,VALUE:3-7}     3终端关机，4终端复位，5终端恢复出厂设置，6关闭数据通信，7关闭所有无线通信
	switch(ctrl->cmd) {
	case 1:
	case 2:
		type = 20 + ctrl->cmd;
		argLen = new_proto->msg_len - sizeof(TERM_CONTROL);
		argPtr = ctrl->msg;
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "TYPE:%u,VALUE:%.*s", type, argLen, argPtr);
		break;
	case 3:
	case 4:
	case 5:
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "TYPE:15,VALUE:%u", ctrl->cmd);
		break;
	default:
		return false;
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
	out->mac_id = mac_id;
	out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertSetEvent(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	CBase64 base64;

	int pos;
	EVENT_ITEM *item;
	SET_EVENT *event = (SET_EVENT*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&event->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SETP ", msg_seq.c_str(), mac_id.c_str());
	//{TYPE:11,160:2,161:[1:11,2:tPLA1w==][1:22,2:z8LT6g==][1:33,2:uc635w==]}
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:11,160:%u", event->cmd);

	pos = sizeof(SET_EVENT);
	for (i = 0; pos < new_proto->msg_len && event->cmd != 0 && i < event->num; ++i) {
		item = (EVENT_ITEM*) (new_proto->msg_data + pos);

		if (i == 0) {
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",161:");
		}

		switch (event->cmd) {
		case 1:
		case 2:
		case 3:
			pos += sizeof(EVENT_ITEM) + item->len;
			base64.Encode(item->buf, item->len);
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "[1:%u,2:%s]", item->id, base64.GetBuffer());
			break;
		case 4:
			pos += sizeof(char);
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "[1:%u]", item->id);
			break;
		}
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
	out->mac_id = mac_id;
	out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertQuestionAsk(NewProto *new_proto, InterProtoOut *out)
{
	int buflen;
	char buffer[BUFSIZ + 1];

	CBase64 base64;

	int pos;
	QUESTION_ITEM *qItem;
	ANSWER_ITME *aItem;
	QUESTION_ASK *ask = (QUESTION_ASK*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&ask->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SNDM ", msg_seq.c_str(), mac_id.c_str());
	//D_SNDM {TYPE:5,16:1,17:xOPKx8uto78=,18:[1:1,2:1cXI/Q==][1:2,2:wO7LxA==][1:3,2:zfW2/g==]}
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:5,16:%u", ask->attr);

	pos = sizeof(QUESTION_ASK);
	qItem = (QUESTION_ITEM*)(new_proto->msg_data + pos);
	pos += sizeof(QUESTION_ITEM) + qItem->len;
	base64.Encode(qItem->msg, qItem->len);
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",17:%s,18:", base64.GetBuffer());

	while(pos < new_proto->msg_len) {
		aItem = (ANSWER_ITME*)(new_proto->msg_data + pos);
		aItem->len = ntohs(aItem->len);
		pos += sizeof(ANSWER_ITME) + aItem->len;

		base64.Encode(aItem->msg, aItem->len);
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "[1:%u,2:%s]", aItem->id, base64.GetBuffer());
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertInfoMenu(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	CBase64 base64;

	int pos;
	MENU_ITEM *item;
	INFO_MENU *menu = (INFO_MENU*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&menu->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SETP ", msg_seq.c_str(), mac_id.c_str());
	//D_SETP {TYPE:12,165:2,166:[1:1,2:1cXI/Q==][1:2,2:wO7LxA==][1:3,2:zfW2/g==]}
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:12,165:%u", menu->cmd);

	pos = sizeof(INFO_MENU);
	for (i = 0;  pos < new_proto->msg_len && menu->cmd != 0 && i < menu->num; ++i) {
		item = (MENU_ITEM*) (new_proto->msg_data + pos);
		item->len = ntohs(item->len);
		pos += sizeof(MENU_ITEM) + item->len;

		if(i == 0) {
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",166:");
		}

		base64.Encode(item->msg, item->len);
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "[1:%u,2:%s]", item->type, base64.GetBuffer());
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertInfoSend(NewProto *new_proto, InterProtoOut *out)
{
	int buflen;
	char buffer[BUFSIZ + 1];

	CBase64 base64;

	INFO_SEND *send = (INFO_SEND*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&send->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SNDM ", msg_seq.c_str(), mac_id.c_str());
	//D_SNDM {TYPE:4,11:147,12:zfW2/g==}
	send->len = ntohs(send->len);
	base64.Encode(send->msg, send->len);
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:4,11:%u,12:%s", send->type, base64.GetBuffer());

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertPhoneBook(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	CBase64 base64;

	int pos;
	unsigned char type;
	STRING *number;
	STRING *person;
	PHONE_BOOK *book = (PHONE_BOOK*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&book->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SETP ", msg_seq.c_str(), mac_id.c_str());
	//D_SETP {TYPE:13,170:3,171:[1:1,2:13512345678,3:1cXI/Q==][1:2,2:13612345678,3:wO7LxA==][1:3,2:13712345678,3:zfW2/g==]}
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:13,170:%u", book->cmd);

	pos = sizeof(PHONE_BOOK);
	for(i = 0; pos < new_proto->msg_len && book->cmd != 0 && i < book->num; ++i) {
		type = *(unsigned char*)(new_proto->msg_data + pos);
		pos += sizeof(char);
		number = (STRING*)(new_proto->msg_data + pos);
		pos += sizeof(STRING) + number->len;
		person = (STRING*)(new_proto->msg_data + pos);
		pos += sizeof(STRING) + person->len;
		base64.Encode(person->msg, person->len);

		if(i == 0) {
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",171:");
		}

		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "[1:%u,2:%.*s,3:%s]", \
				type, number->len, number->msg, base64.GetBuffer());
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
	out->mac_id = mac_id;
	out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertCarControl(NewProto *new_proto, InterProtoOut *out)
{
	int buflen;
	char buffer[BUFSIZ + 1];

	CAR_CONTROL *ctl = (CAR_CONTROL*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&ctl->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_CTLM ", msg_seq.c_str(), mac_id.c_str());
	//D_CTLM {TYPE:7,VALUE:null}
	//D_CTLM {TYPE:8,VALUE:null}
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:%u,VALUE:null", 8 - ctl->flag);

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
	out->mac_id = mac_id;
	out->seq = msg_seq;

	return true;
}

static time_t bcd2utc_time(const unsigned char *bcd)
{
	struct tm tm;

	tm.tm_year = (bcd[0] >> 4) * 10 + (bcd[0] & 0x0f) + 100;
	tm.tm_mon  = (bcd[1] >> 4) * 10 + (bcd[1] & 0x0f) - 1;
	tm.tm_mday = (bcd[2] >> 4) * 10 + (bcd[2] & 0x0f);
	tm.tm_hour = (bcd[3] >> 4) * 10 + (bcd[3] & 0x0f);
	tm.tm_min  = (bcd[4] >> 4) * 10 + (bcd[4] & 0x0f);
	tm.tm_sec  = (bcd[5] >> 4) * 10 + (bcd[5] & 0x0f);

	return mktime(&tm);
}

bool ConvertProto::NewConvertSetCircle(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	TIME_SEGMENT *ts;
	SPEED_LIMIT *sl;
	time_t beginTime;
	time_t endTime;

	int pos;
	CIRCLE_ITEM *item;
	SET_CIRCLE *circle = (SET_CIRCLE*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&circle->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SETP ", msg_seq.c_str(), mac_id.c_str());
	//{TYPE:14,150:2,151:1,152:[1:1,2:3,3:$btime,4:$etime,5:321,6:123,21:7654321|1234567|1001]}
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:14,150:%u,151:1,152:", 2 + circle->cmd);

	pos = sizeof(SET_CIRCLE);
	for(i = 0; i < circle->num && pos < new_proto->msg_len; ++i) {
		item = (CIRCLE_ITEM*)(new_proto->msg_data + pos);
		pos += sizeof(CIRCLE_ITEM);

		item->areaid = ntohl(item->areaid);
		item->areaattr = ntohs(item->areaattr);
		item->lat = ntohl(item->lat);
		item->lon = ntohl(item->lon);
		item->rad = ntohl(item->rad);

		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "[1:%u,2:%u", item->areaid, item->areaattr);
		if(item->areaattr & 0x0001) {
			ts = (TIME_SEGMENT*)(new_proto->msg_data + pos);
			pos += sizeof(TIME_SEGMENT);

			beginTime = bcd2utc_time(ts->bt);
			endTime   = bcd2utc_time(ts->et);
			if(beginTime < 0 || endTime < 0) {
				return false;
			}

			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",3:%ld,4:%ld", beginTime, endTime);
		}
		if(item->areaattr & 0x0002) {
			sl = (SPEED_LIMIT*)(new_proto->msg_data + pos);
			pos += sizeof(SPEED_LIMIT);

			sl->ts = ntohs(sl->ts);
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",5:%u,6:%u", sl->ts, sl->ht);
		}

		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",21:%u|%u|%u]", item->lat, item->lon, item->rad);
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertDelCircle(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	int pos;
	DEL_GRAPHID *graph = (DEL_GRAPHID*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&graph->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SETP ", msg_seq.c_str(), mac_id.c_str());
	//{TYPE:14,150:1,151:1,152:[1:1][1:2][1:3]}
	if(graph->num == 0) {
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:14,150:0,151:1");
	}
	else {
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:14,150:1,151:1,152:");
	}

	pos = sizeof(DEL_GRAPHID) + sizeof(int);
	for(i = 0; pos <= new_proto->msg_len && i < graph->num; ++i) {
		graph->areaid[i] = ntohl(graph->areaid[i]);
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",[1:%u]", graph->areaid[i]);

		pos += sizeof(int);
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertSetRectangle(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	TIME_SEGMENT *ts;
	SPEED_LIMIT *sl;
	time_t beginTime;
	time_t endTime;

	int pos;
	RECTANGLE_ITEM *item;
	SET_RECTANGLE *rect = (SET_RECTANGLE*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&rect->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SETP ", msg_seq.c_str(), mac_id.c_str());
	//{TYPE:14,150:2,151:2,152:[1:1,2:3,3:$btime,4:$etime,5:321,6:123,22:7654321|1234567|8654321|2234567]}
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:14,150:%u,151:2,152:", 2 + rect->cmd);

	pos = sizeof(SET_RECTANGLE);
	for(i = 0; i < rect->num && pos < new_proto->msg_len; ++i) {
		item = (RECTANGLE_ITEM*)(new_proto->msg_data + pos);
		pos += sizeof(RECTANGLE_ITEM);

		item->areaid = ntohl(item->areaid);
		item->areaattr = ntohs(item->areaattr);
		item->leftlat = ntohl(item->leftlat);
		item->leftlon = ntohl(item->leftlon);
		item->rightlat = ntohl(item->rightlat);
		item->rightlon = ntohl(item->rightlon);

		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "[1:%u,2:%u", item->areaid, item->areaattr);
		if(item->areaattr & 0x0001) {
			ts = (TIME_SEGMENT*)(new_proto->msg_data + pos);
			pos += sizeof(TIME_SEGMENT);

			beginTime = bcd2utc_time(ts->bt);
			endTime   = bcd2utc_time(ts->et);
			if(beginTime < 0 || endTime < 0) {
				return false;
			}

			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",3:%ld,4:%ld", beginTime, endTime);
		}
		if(item->areaattr & 0x0002) {
			sl = (SPEED_LIMIT*)(new_proto->msg_data + pos);
			pos += sizeof(SPEED_LIMIT);

			sl->ts = ntohs(sl->ts);
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",5:%u,6:%u", sl->ts, sl->ht);
		}

		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",22:%u|%u|%u|%u]", item->leftlat, item->leftlon, item->rightlat, item->rightlon);
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertDelRectangle(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	int pos;
	DEL_GRAPHID *graph = (DEL_GRAPHID*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&graph->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SETP ", msg_seq.c_str(), mac_id.c_str());
	//{TYPE:14,150:1,151:2,152:[1:1][1:2][1:3]}
	if(graph->num == 0) {
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:14,150:0,151:2");
	}
	else {
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:14,150:1,151:2,152:");
	}

	pos = sizeof(DEL_GRAPHID) + sizeof(int);
	for(i = 0; pos <= new_proto->msg_len && i < graph->num; ++i) {
		graph->areaid[i] = ntohl(graph->areaid[i]);
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",[1:%u]", graph->areaid[i]);

		pos += sizeof(int);
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertSetPolygon(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	TIME_SEGMENT *ts;
	SPEED_LIMIT *sl;
	time_t beginTime;
	time_t endTime;

	int num;
	int pos;
	POLYGON_ITEM *item;
	SET_POLYGON *polygon = (SET_POLYGON*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&polygon->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SETP ", msg_seq.c_str(), mac_id.c_str());
	//{TYPE:14,150:2,151:3,152:[1:1,2:3,3:$btime,4:$etime,5:321,6:123,20:7654321|1234567|8654321|2234567|9654321|5234567]}
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:14,150:2,151:3,152:");

	polygon->areaid = ntohl(polygon->areaid);
	polygon->areaattr = ntohs(polygon->areaattr);
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "[1:%u,2:%u", polygon->areaid, polygon->areaattr);

	pos = sizeof(SET_POLYGON);
	if(polygon->areaattr & 0x0001) {
		ts = (TIME_SEGMENT*)(new_proto->msg_data + pos);
		pos += sizeof(TIME_SEGMENT);

		beginTime = bcd2utc_time(ts->bt);
		endTime   = bcd2utc_time(ts->et);
		if(beginTime < 0 || endTime < 0) {
			return false;
		}

		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",3:%ld,4:%ld", beginTime, endTime);
	}
	if(polygon->areaattr & 0x0002) {
		sl = (SPEED_LIMIT*)(new_proto->msg_data + pos);
		pos += sizeof(SPEED_LIMIT);

		sl->ts = htons(sl->ts);
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",5:%u,6:%u", sl->ts, sl->ht);
	}

	num = ntohs(*(unsigned short*)(new_proto->msg_data + pos));
	pos += sizeof(unsigned short);
	for(i = 0; i < num && pos < new_proto->msg_len; ++i) {
		item = (POLYGON_ITEM*)(new_proto->msg_data + pos);
		pos += sizeof(POLYGON_ITEM);

		item->lat = ntohl(item->lat);
		item->lon = ntohl(item->lon);

		if(i == 0) {
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",20:%u|%u", item->lat, item->lon);
		}
		else {
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "|%u|%u", item->lat, item->lon);
		}
	}

	strncpy(buffer + buflen, "]} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertDelPolygon(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	int pos;
	DEL_GRAPHID *graph = (DEL_GRAPHID*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&graph->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SETP ", msg_seq.c_str(), mac_id.c_str());
	//{TYPE:14,150:1,151:3,152:[1:1][1:2][1:3]}
	if(graph->num == 0) {
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:14,150:0,151:3");
	}
	else {
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:14,150:1,151:3,152:");
	}

	pos = sizeof(DEL_GRAPHID) + sizeof(int);
	for(i = 0; pos <= new_proto->msg_len && i < graph->num; ++i) {
		graph->areaid[i] = ntohl(graph->areaid[i]);
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",[1:%u]", graph->areaid[i]);

		pos += sizeof(int);
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertSetLine(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	TIME_SEGMENT *ts;
	TIME_LIMIT *tl;
	SPEED_LIMIT *sl;
	time_t beginTime;
	time_t endTime;

	int num;
	int pos;
	LINE_ITEM *item;
	SET_LINE *line = (SET_LINE*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&line->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SETP ", msg_seq.c_str(), mac_id.c_str());
	//{TYPE:15,155:2,156:[1:1,2:1,3:$btime,4:$etime,5:(1=1|2=1|3=232222|4=6333333|5=1|6=3|7=100|8=10|9=100|10=100)]}
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:15,155:2,156:");

	pos = sizeof(SET_LINE);
	line->areaid = ntohl(line->areaid);
	line->attr = ntohs(line->attr);
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "[1:%u,2:%u", line->areaid, line->attr);
	if(line->attr & 0x0001) {
		ts = (TIME_SEGMENT*)(new_proto->msg_data + pos);
		pos += sizeof(TIME_SEGMENT);

		beginTime = bcd2utc_time(ts->bt);
		endTime   = bcd2utc_time(ts->et);
		if(beginTime < 0 || endTime < 0) {
			return false;
		}

		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",3:%ld,4:%ld", beginTime, endTime);
	}

	strncpy(buffer + buflen, ",5:", BUFSIZ - buflen);
	buflen += 3;

	num = ntohs(*(unsigned short*)(new_proto->msg_data + pos));
	pos += sizeof(unsigned short);
	for(i = 0; pos < new_proto->msg_len && i < num; ++i) {
		item = (LINE_ITEM*)(new_proto->msg_data + pos);
		pos += sizeof(LINE_ITEM);

		item->pointid = ntohl(item->pointid);
		item->roadid = ntohl(item->roadid);
		item->lat = ntohl(item->lat);
		item->lon = ntohl(item->lon);
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "(1=%u|2=%u|3=%u|4=%u|5=%u|6=%u", \
				item->pointid, item->roadid, item->lat, item->lon, item->width, item->attr);

		if(item->attr & 0x01) {
			tl = (TIME_LIMIT*)(new_proto->msg_data + pos);
			pos += sizeof(TIME_LIMIT);

			tl->max = ntohs(tl->max);
			tl->min = ntohs(tl->min);
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "|7=%u|8=%u", tl->max, tl->min);
		}

		if(item->attr & 0x02) {
			sl = (SPEED_LIMIT*)(new_proto->msg_data + pos);
			pos += sizeof(SPEED_LIMIT);

			sl->ts = ntohs(sl->ts);
			buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "|9=%u|10=%u", sl->ts, sl->ht);
		}

		strncpy(buffer + buflen, ")", BUFSIZ - buflen);
		buflen += 1;
	}

	strncpy(buffer + buflen, "]} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertDelLine(NewProto *new_proto, InterProtoOut *out)
{
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	int pos;
	DEL_GRAPHID *graph = (DEL_GRAPHID*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&graph->macid);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_SETP ", msg_seq.c_str(), mac_id.c_str());
	//{TYPE:14,150:1,151:3,152:[1:1][1:2][1:3]}
	if(graph->num == 0) {
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:15,155:0");
	}
	else {
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:15,155:1,156:");
	}

	pos = sizeof(DEL_GRAPHID) + sizeof(int);
	for(i = 0; pos <= new_proto->msg_len && i < graph->num; ++i) {
		graph->areaid[i] = ntohl(graph->areaid[i]);
		buflen += snprintf(buffer + buflen, BUFSIZ - buflen, ",[1:%u]", graph->areaid[i]);

		pos += sizeof(int);
	}

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
    out->mac_id = mac_id;
    out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertDriveCollect(NewProto *new_proto, InterProtoOut *out)
{
	int buflen;
	char buffer[BUFSIZ + 1];

	DRIVE_COLLECT *collect = (DRIVE_COLLECT*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&collect->macid);

	//D_REQD {TYPE:1,1:1,2:1,3:1,4:1241802983,5:1241803223}

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_REQD ", msg_seq.c_str(), mac_id.c_str());
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:4,30:%u} \r\n", collect->cmd);

	out->msg = buffer;
	out->mac_id = mac_id;
	out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertDriveParam(NewProto *new_proto, InterProtoOut *out)
{
	int buflen;
	char buffer[BUFSIZ + 1];

	int dataLen;
	char *dataPtr;
	CBase64 base;

	DRIVE_PARAM *param = (DRIVE_PARAM*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&param->macid);

	//D_REQD {TYPE:1,1:1,2:1,3:1,4:1241802983,5:1241803223}

	dataPtr = param->data;
	dataLen = dataPtr - new_proto->msg_data;

	base.Encode(dataPtr, dataLen);

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_REQD ", msg_seq.c_str(), mac_id.c_str());
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:4,30:%u,31:%s} \r\n", \
			param->cmd, base.GetBuffer());

	out->msg = buffer;
	out->mac_id = mac_id;
	out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertMediaSearch(NewProto *new_proto, InterProtoOut *out)
{
	int buflen;
	char buffer[BUFSIZ + 1];

	MEDIA_SEARCH *search = (MEDIA_SEARCH*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&search->macid);

	time_t bt, et;

	bt = bcd2utc_time(search->begintime);
	et = bcd2utc_time(search->endtime);
	//D_REQD {TYPE:1,1:1,2:1,3:1,4:1241802983,5:1241803223}

	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_REQD ", msg_seq.c_str(), mac_id.c_str());
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:1,1:%u,2:%u,3:%u,4:%ld,5:%ld} \r\n", \
			search->mediatype, search->channleid, search->eventtype, bt, et);

	out->msg = buffer;
	out->mac_id = mac_id;
	out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertSingleUpload(NewProto *new_proto, InterProtoOut *out)
{
	int buflen;
	char buffer[BUFSIZ + 1];

	SINGLE_UPLOAD *single = (SINGLE_UPLOAD*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&single->macid);

	//D_REQD {TYPE:3,7:1,6:0}
	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_REQD ", msg_seq.c_str(), mac_id.c_str());
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:3,7:%u,6:%u", single->id, single->flag);

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
	out->mac_id = mac_id;
	out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertMultiUpload(NewProto *new_proto, InterProtoOut *out)
{
	int buflen;
	char buffer[BUFSIZ + 1];

	MULTI_UPLOAD *multi = (MULTI_UPLOAD*)new_proto->msg_data;
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&multi->macid);

	time_t bt, et;

	bt = bcd2utc_time(multi->begintime);
	et = bcd2utc_time(multi->endtime);

	//D_REQD {TYPE:2,1:1,2:1,3:1,4:$btime,5:$etime,6:0}
	buflen = snprintf(buffer, BUFSIZ, "CAITS %s %s 0 D_REQD ", msg_seq.c_str(), mac_id.c_str());
	buflen += snprintf(buffer + buflen, BUFSIZ - buflen, "{TYPE:2,1:%u,2:%u,3:%u,4:%ld,5:%ld,6:%u",\
			multi->mediatype, multi->channleid, multi->eventtype, bt, et, multi->flag);

	strncpy(buffer + buflen, "} \r\n", BUFSIZ - buflen);
	out->msg = buffer;
	out->mac_id = mac_id;
	out->seq = msg_seq;

	return true;
}

bool ConvertProto::NewConvertGetPhoto(NewProto *new_proto, InterProtoOut *out)
{
    //D_CTLM 0x8801{TYPE:10,VALUE:1摄像头通道ID|2拍摄命令|3录像时间|4保存标志|5分辨率|6照片质量|7亮度|8对比度|9饱和度|10色度}
	REQUEST_PHOTO *photo = (REQUEST_PHOTO*) new_proto->msg_data;
    /*
    unsigned char channel_id;
    unsigned short command;
    unsigned short interval;
    unsigned char save_flag;
    unsigned char sens;
    unsigned char quality;

    unsigned char brightness; //亮度
    unsigned char contrast;   //对比度
    unsigned char saturation; //饱和度
    unsigned char chroma;     //色度
     */
	string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
	string mac_id = _new_parse.convert_macid(&(photo->mac_list));

	out->msg = "CAITS " + msg_seq + " " + mac_id  + " 0 D_CTLM {TYPE:10,VALUE:";
    out->msg += chartodecstr(photo->channel_id) + "|"
    		    + ustodecstr(ntohs(photo->command)) + "|"
    		    + ustodecstr(ntohs(photo->interval)) + "|"
    		    + chartodecstr(photo->save_flag) + "|"
    		    + chartodecstr(photo->sens) + "|"
    		    + chartodecstr(photo->quality) + "|"
    		    + chartodecstr(photo->brightness) + "|"
    		    + chartodecstr(photo->contrast) + "|"
    		    + chartodecstr(photo->saturation) + "|"
    		    + chartodecstr(photo->chroma)
    		    + "} \r\n";

	return true;
}

bool ConvertProto::NewConvertRecord(NewProto *new_proto, InterProtoOut *out)
{
	// D_CTLM 0x8804录音{TYPE:11,VALUE:0表示停止录音;1表示开始录音|录音时间秒0表示一直录音|保存标志1：保存0：实时上传}

	RECORD *record = (RECORD *)new_proto->msg_data;
    string msg_seq = new_proto->user_name + "_" + uitodecstr(new_proto->msg_seq);
    string mac_id = _new_parse.convert_macid(&(record->mac_list));

    string command = chartodecstr(record->command);
    string time = ustodecstr(ntohs(record->time));
    string save_flag = chartodecstr(record->save_flag);
    string rate = ustodecstr(record->sampling_rate);

    out->msg = "CAITS " + msg_seq + " " + mac_id  + " 0 D_CTLM {TYPE:11,VALUE:"
    		+ command + "|" + time + "|" + save_flag + "|" + rate + "} \r\n";

	return true;
}

bool ConvertProto::InterConvertResp(InterProto *inter_proto, NewProtoOut *out)
{
	return true;
}

bool ConvertProto::BuildNewProto(unsigned short msg_type,  unsigned short msg_len,
		const char *msg_data, DataBuffer *data_buffer)
{
	COMM_HEADER msg_header;
	msg_header.msg_version = MSG_VER;
	msg_header.msg_type = ntohs(msg_type);
	msg_header.msg_len = ntohs(msg_len);
    msg_header.msg_seq = ntohl(get_msg_seq());

	PROTO_HEADER inter;
	inter.tag = 0x5b;
	inter.len = htons(msg_len + sizeof(COMM_HEADER));
	inter.flag = (unsigned char) (rand() % 256);

	data_buffer->writeBlock((const void*) &inter, sizeof(inter));
    data_buffer->writeBlock((const void*) &msg_header, sizeof(msg_header));
	if (msg_len > 0 && msg_data != NULL)
	{
		data_buffer->writeBlock((const void*) msg_data, msg_len);
	}

	data_buffer->writeInt8(0x5d);
	// 进行简单加密处理
	xorcode(inter.flag, (unsigned char*) data_buffer->getBuffer() + sizeof(inter),
			sizeof(msg_header) + msg_len);

	return true;
}


bool ConvertProto::BuildNewNoop(DataBuffer *data_buffer)
{
	return BuildNewProto(MSG_REQUEST_NOOP | 0x8000, 0, NULL, data_buffer);
}

bool ConvertProto::BuildNewCommResp(unsigned short msg_type, unsigned int seq,
		unsigned char ret, DataBuffer *data_buffer)
{
	COMM_RESPONSE resp;
	resp.resp_seq = ntohl(seq);
	resp.result = ret;

    return 	BuildNewProto(msg_type, (unsigned short) sizeof(COMM_RESPONSE),
			(const char*) (&resp), data_buffer);
}

bool ConvertProto::InterConvertQuery(InterProto *inter_proto, NewProtoOut *out) {
	CAR_ONLINE online;
	memset(&online, 0x00, sizeof(CAR_ONLINE));

	memcpy(online.mac_list.oem_code, inter_proto->oem_code.c_str(),
				inter_proto->oem_code.length());
	get_bcdphone_from_internalproto((unsigned char *) online.mac_list.phone,
				inter_proto->car_id);

	online.online = atoi(inter_proto->kvmap["18"].c_str());

	out->msg_type = alert;
	out->mac_id = inter_proto->mac_id;
	BuildNewProto(MSG_REQUEST_ONLINE, (unsigned short) sizeof(CAR_ONLINE),
				(const char*) (&online), &(out->data_buffer));

	return true;
}

bool ConvertProto::InterConvertMedia(InterProto *inter_proto, NewProtoOut *out) {
	UP_MEDIA media;
	memset(&media, 0x00, sizeof(UP_MEDIA));

	media.type = atoi(inter_proto->kvmap["121"].c_str());
	media.format = atoi(inter_proto->kvmap["122"].c_str());
	media.event = atoi(inter_proto->kvmap["123"].c_str());;

	memcpy(media.mac_list.oem_code, inter_proto->oem_code.c_str(), inter_proto->oem_code.length());
	get_bcdphone_from_internalproto((unsigned char *) media.mac_list.phone, inter_proto->car_id);

	memset(media.url, 0x00, 512);
	strncpy(media.url, inter_proto->kvmap["125"].c_str(), 512);

	out->msg_type = alert;
	out->mac_id = inter_proto->mac_id;
	BuildNewProto(MSG_REQUEST_MEDIA_UP, (unsigned short) sizeof(UP_MEDIA) - 512 + strnlen(media.url, 512),
				(const char*) (&media), &(out->data_buffer));

	return true;
}

bool ConvertProto::InterConvertGetParam(InterProto *inter_proto, NewProtoOut *out) {
	int msglen;
	char msgbuf[BUFSIZ + 1];

	unsigned char itemLen;
	PARAM_ITEM *itemPtr;
	GET_PARAM_RESP *resp;

	map<string, string> *mssPtr;
	map<string, string>::iterator mssIte;

	string::size_type strPos;

	uint16_t key;
	uint8_t valu1;
	uint16_t valu2;
	uint32_t valu4;

	msglen = 0;
	resp = (GET_PARAM_RESP*)msgbuf + msglen;
	msglen += sizeof(GET_PARAM_RESP);
	strncpy(resp->macid.oem_code, inter_proto->oem_code.c_str(), 6);
	get_bcdphone_from_internalproto((unsigned char *) resp->macid.phone, inter_proto->car_id);

	strPos = inter_proto->msg_seq.find('_');
	if(strPos == string::npos) {
		return false;
	}
	valu2 = atoi(inter_proto->msg_seq.substr(strPos + 1).c_str());
	resp->seq = htons(valu2);

	resp->num = 0;
	mssPtr = &inter_proto->kvmap;
	for(mssIte = mssPtr->begin(); mssIte != mssPtr->end(); ++mssIte) {
		if(mssIte->first[0] < '0' || mssIte->first[0] > '9') {
			continue;
		}

		if(msglen >= BUFSIZ) {
			break;
		}

		itemLen = 0;
		itemPtr = (PARAM_ITEM*)(msgbuf + msglen);

		key = atoi(mssIte->first.c_str());
		switch(key) {
		case 0:
			itemPtr->type = htonl(0x0013);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 1:
			itemPtr->type = htonl(0x0018);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 3:
			itemPtr->type = htonl(0x0010);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 4:
			itemPtr->type = htonl(0x0011);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 5:
			itemPtr->type = htonl(0x0012);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 7:
			itemPtr->type = htonl(0x0001);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 9:
			itemPtr->type = htonl(0x0048);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 10:
			itemPtr->type = htonl(0x0040);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 15:
			itemPtr->type = htonl(0x0043);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 41:
			itemPtr->type = htonl(0x0083);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 42:
			itemPtr->type = htonl(0x0084);
			itemLen = sizeof(char);
			valu1 = atoi(mssIte->second.c_str());
			memcpy(itemPtr->date, &valu1, itemLen);
			break;
		case 100:
			itemPtr->type = htonl(0x0002);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 101:
			itemPtr->type = htonl(0x0003);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 102:
			itemPtr->type = htonl(0x0004);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 103:
			itemPtr->type = htonl(0x0005);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 104:
			itemPtr->type = htonl(0x0006);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 105:
			itemPtr->type = htonl(0x0007);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 106:
			itemPtr->type = htonl(0x0014);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 107:
			itemPtr->type = htonl(0x0015);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 108:
			itemPtr->type = htonl(0x0016);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 109:
			itemPtr->type = htonl(0x0017);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 110:
			itemPtr->type = htonl(0x0019);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 111:
			itemPtr->type = htonl(0x0020);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 112:
			itemPtr->type = htonl(0x0021);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 113:
			itemPtr->type = htonl(0x0022);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 114:
			itemPtr->type = htonl(0x0027);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 115:
			itemPtr->type = htonl(0x0028);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 116:
			itemPtr->type = htonl(0x0029);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 117:
			itemPtr->type = htonl(0x002c);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 118:
			itemPtr->type = htonl(0x002d);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 119:
			itemPtr->type = htonl(0x002e);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 120:
			itemPtr->type = htonl(0x002f);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 121:
			itemPtr->type = htonl(0x0030);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 122:
			itemPtr->type = htonl(0x0041);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 123:
			itemPtr->type = htonl(0x0042);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 124:
			itemPtr->type = htonl(0x0044);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 125:
			itemPtr->type = htonl(0x0045);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 126:
			itemPtr->type = htonl(0x0046);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 127:
			itemPtr->type = htonl(0x0047);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 128:
			itemPtr->type = htonl(0x0055);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 129:
			itemPtr->type = htonl(0x0056);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 130:
			itemPtr->type = htonl(0x0057);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 131:
			itemPtr->type = htonl(0x0058);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 132:
			itemPtr->type = htonl(0x0059);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 133:
			itemPtr->type = htonl(0x005a);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 134:
			itemPtr->type = htonl(0x0081);
			itemLen = sizeof(short);
			valu2 = htons(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu2, itemLen);
			break;
		case 135:
			itemPtr->type = htonl(0x0082);
			itemLen = sizeof(short);
			valu2 = htons(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu2, itemLen);
			break;
		case 136:
			itemPtr->type = htonl(0x0070);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 137:
			itemPtr->type = htonl(0x0071);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 138:
			itemPtr->type = htonl(0x0072);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 139:
			itemPtr->type = htonl(0x0073);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 140:
			itemPtr->type = htonl(0x0074);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 141:
			itemPtr->type = htonl(0x0049);
			itemLen = mssIte->second.size();
			strcpy(itemPtr->date, mssIte->second.c_str());
			break;
		case 142:
			itemPtr->type = htonl(0x0050);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 143:
			itemPtr->type = htonl(0x0051);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 144:
			itemPtr->type = htonl(0x0052);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 145:
			itemPtr->type = htonl(0x0053);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 146:
			itemPtr->type = htonl(0x0054);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 147:
			itemPtr->type = htonl(0x0080);
			itemLen = sizeof(int);
			valu4 = htonl(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu4, itemLen);
			break;
		case 300:
			itemPtr->type = htonl(0x005b);
			itemLen = sizeof(short);
			valu2 = htons(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu2, itemLen);
			break;
		case 301:
			itemPtr->type = htonl(0x005c);
			itemLen = sizeof(short);
			valu2 = htons(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu2, itemLen);
			break;
		case 302:
			itemPtr->type = htonl(0x005d);
			itemLen = sizeof(short);
			valu2 = htons(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu2, itemLen);
			break;
		case 303:
			itemPtr->type = htonl(0x005e);
			itemLen = sizeof(short);
			valu2 = htons(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu2, itemLen);
			break;
		case 304:
			itemPtr->type = htonl(0x005f);
			itemLen = sizeof(short);
			valu2 = htons(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu2, itemLen);
			break;
		case 308:
			itemPtr->type = htonl(0x0063);
			itemLen = sizeof(short);
			valu2 = htons(atoi(mssIte->second.c_str()));
			memcpy(itemPtr->date, &valu2, itemLen);
			break;
		}

		if(itemLen == 0) {
			continue;
		}
		itemPtr->size = itemLen;

		++resp->num;
		msglen += sizeof(PARAM_ITEM) + itemPtr->size;
	}

	out->msg_type = alert;
	out->mac_id = inter_proto->mac_id;

	return BuildNewProto(MSG_REQUEST_GET_PARAM | 0X8000, msglen, msgbuf, &out->data_buffer);
}

bool ConvertProto::InterConvertEventReport(InterProto *inter_proto, NewProtoOut *out) {
	int msglen;
	char *msgptr;

	EVENT_REPORT report;

	strncpy(report.macid.oem_code, inter_proto->oem_code.c_str(), 6);
	get_bcdphone_from_internalproto((unsigned char *) report.macid.phone, inter_proto->car_id);

	report.id = atoi(inter_proto->kvmap["81"].c_str());

	out->msg_type = alert;
	out->mac_id = inter_proto->mac_id;

	msglen = sizeof(EVENT_REPORT);
	msgptr = (char*)&report;
	return BuildNewProto(MSG_REQUEST_EVENT_REPORT, msglen, msgptr, &out->data_buffer);
}

bool ConvertProto::InterConvertQuestionAck(InterProto *inter_proto, NewProtoOut *out) {
	int msglen;
	char *msgptr;

	QUESTION_ACK ack;

	uint16_t val16;

	strncpy(ack.macid.oem_code, inter_proto->oem_code.c_str(), 6);
	get_bcdphone_from_internalproto((unsigned char *) ack.macid.phone, inter_proto->car_id);

	val16 = atoi(inter_proto->kvmap["84"].c_str());
	ack.seq = htons(val16);
	ack.res = atoi(inter_proto->kvmap["84"].c_str());

	out->msg_type = alert;
	out->mac_id = inter_proto->mac_id;

	msglen = sizeof(QUESTION_ACK);
	msgptr = (char*)&ack;
	return BuildNewProto(MSG_REQUEST_QUESTION_ACK, msglen, msgptr, &out->data_buffer);
}

bool ConvertProto::InterConvertInfoResp(InterProto *inter_proto, NewProtoOut *out)
{
	char temp[BUFSIZ + 1];
	const int MAXITEM = 2;

	int itemsize;
	char *itemset[MAXITEM];

	int msglen;
	char *msgptr;
	INFO_RESP resp;

	strncpy(resp.macid.oem_code, inter_proto->oem_code.c_str(), 6);
	get_bcdphone_from_internalproto((unsigned char *) resp.macid.phone, inter_proto->car_id);

	strncpy(temp, inter_proto->kvmap["83"].c_str(), BUFSIZ);
	itemsize = splitStr(temp, "|", itemset, MAXITEM);
	if(itemsize != MAXITEM) {
		return false;
	}

	resp.type = atoi(itemset[0]);
	resp.flag = atoi(itemset[1]);

	out->msg_type = alert;
	out->mac_id = inter_proto->mac_id;

	msglen = sizeof(INFO_RESP);
	msgptr = (char*)&resp;
	return BuildNewProto(MSG_REQUEST_INFO_RESP, msglen, msgptr, &out->data_buffer);
}

bool ConvertProto::InterConvertListReport(InterProto *inter_proto, NewProtoOut *out) {
	int buflen;
	char buffer[BUFSIZ + 1];
	const char *bufptr;

	CBase64 base64;
	int listLen;
	char *listPtr;

	MAC_LIST *macid;
	STRING4 *list;

	buflen = 0;

	macid = (MAC_LIST*)(buffer + buflen);
	strncpy(macid->oem_code, inter_proto->oem_code.c_str(), 6);
	get_bcdphone_from_internalproto((unsigned char *) macid->phone, inter_proto->car_id);
	buflen += sizeof(MAC_LIST);

	bufptr = inter_proto->kvmap["87"].c_str();
	base64.Decode(bufptr, strlen(bufptr));
	listLen = base64.GetLength();
	listPtr = base64.GetBuffer();

	list = (STRING4*)(buffer + buflen);
	list->len = htonl(listLen);
	strncpy(list->msg, listPtr, listLen);
	buflen += sizeof(STRING4) + listLen;

	out->msg_type = alert;
	out->mac_id = inter_proto->mac_id;

	return BuildNewProto(MSG_REQUEST_LIST_REPORT, buflen, buffer, &out->data_buffer);
}

bool ConvertProto::InterConvertIdentityCollect(InterProto *inter_proto, NewProtoOut *out) {
	int buflen;
	char buffer[BUFSIZ + 1];
	const char *bufptr;

	MAC_LIST *macid;
	STRING *item;

	buflen = 0;

	macid = (MAC_LIST*)(buffer + buflen);
	strncpy(macid->oem_code, inter_proto->oem_code.c_str(), 6);
	get_bcdphone_from_internalproto((unsigned char *) macid->phone, inter_proto->car_id);
	buflen += sizeof(MAC_LIST);

	bufptr = inter_proto->kvmap["110"].c_str();
	item = (STRING*)(buffer + buflen);
	item->len = strlen(bufptr);
	strncpy(item->msg, bufptr, item->len);
	buflen += sizeof(STRING) + item->len;

	bufptr = inter_proto->kvmap["111"].c_str();
	strncpy(buffer + buflen, bufptr, 20);
	buflen += 20;

	bufptr = inter_proto->kvmap["112"].c_str();
	strncpy(buffer + buflen, bufptr, 40);
	buflen += 40;

	bufptr = inter_proto->kvmap["113"].c_str();
	item = (STRING*) (buffer + buflen);
	item->len = strlen(bufptr);
	strncpy(item->msg, bufptr, item->len);
	buflen += sizeof(STRING) + item->len;

	out->msg_type = alert;
	out->mac_id = inter_proto->mac_id;

	return BuildNewProto(MSG_REQUEST_IDENTITY_COLLECT, buflen, buffer, &(out->data_buffer));
}

bool ConvertProto::InterConvertMediaEvent(InterProto *inter_proto, NewProtoOut *out) {
	MEDIA_EVENT event;

	strncpy(event.macid.oem_code, inter_proto->oem_code.c_str(), 6);
	get_bcdphone_from_internalproto((unsigned char *) event.macid.phone, inter_proto->car_id);

	event.id = htonl(atoi(inter_proto->kvmap["120"].c_str()));
	event.type = atoi(inter_proto->kvmap["121"].c_str());
	event.code = atoi(inter_proto->kvmap["122"].c_str());
	event.event = atoi(inter_proto->kvmap["123"].c_str());
	event.channel = atoi(inter_proto->kvmap["124"].c_str());

	out->msg_type = alert;
	out->mac_id = inter_proto->mac_id;

	return BuildNewProto(MSG_REQUEST_MEDIA_EVENT, sizeof(MEDIA_EVENT), (char*)&event, &out->data_buffer);
}

bool ConvertProto::InterConvertSearchResp(InterProto *inter_proto, NewProtoOut *out) {
	int i;
	int buflen;
	char buffer[BUFSIZ + 1];

	SEARCH_ITEM *item;
	SEARCH_RESP *resp;

	int key;
	uint16_t val16;
	uint32_t val32;

	const int MAXPAIR = 2;
	const int MAXLINE = 1024;

	char temp[BUFSIZ + 1];
	int pairsize;
	char *pairset[MAXPAIR];
	int linesize;
	char *lineset[MAXLINE];

	string text;
	vector<string> items;
	vector<string>::iterator vIte;
	string::size_type strPos;

	resp = (SEARCH_RESP*)buffer;
	strncpy(resp->macid.oem_code, inter_proto->oem_code.c_str(), 6);
	get_bcdphone_from_internalproto((unsigned char *) resp->macid.phone, inter_proto->car_id);

	if((strPos = inter_proto->msg_seq.find('_')) == string::npos) {
		return false;
	}
	resp->seq = htons(atoi(inter_proto->msg_seq.substr(strPos + 1).c_str()));

	text = inter_proto->kvmap["20"];
	if(text.empty()) {
		return false;
	}
	parseItem(text, items, '[', ']');

	string bcdStr;

	resp->num = 0;
	buflen = sizeof(SEARCH_RESP);
	for(vIte = items.begin(); vIte != items.end(); ++vIte) {
		item = (SEARCH_ITEM*)(buffer + buflen);
		buflen += sizeof(SEARCH_ITEM);

		strncpy(temp, vIte->c_str(), BUFSIZ);
		linesize = splitStr(temp, ",", lineset, MAXLINE);
		for(i = 0; i < linesize; ++i) {
			pairsize = splitStr(lineset[i], ":", pairset, MAXPAIR);
			if(pairsize != MAXPAIR) {
				continue;
			}

			key = atoi(pairset[0]);
			switch(key) {
			case 120:
				val32 = atoi(pairset[1]);
				item->id = htonl(val32);
				break;
			case 121:
				item->type = atoi(pairset[1]);
				break;
			case 123:
				item->event = atoi(pairset[1]);
				break;
			case 124:
				item->chnnel = atoi(pairset[1]);
				break;
			case 1:
				val32 = atoi(pairset[1]);
				item->lon = htonl(val32);
				break;
			case 2:
				val32 = atoi(pairset[1]);
				item->lat = htonl(val32);
				break;
			case 3:
				val16 = atoi(pairset[1]);
				item->speed = htons(val16);
				break;
			case 4:
				bcdStr = pairset[1];
				get_bcdtime_from_internalproto(item->time, bcdStr);
				break;
			case 5:
				val16 = atoi(pairset[1]);
				item->direction = htons(val16);
				break;
			case 6:
				val16 = atoi(pairset[1]);
				item->hight = htons(val16);
				break;
			case 8:
				val32 = atoi(pairset[1]);
				item->state = htonl(val32);
				break;
			case 20:
				val32 = atoi(pairset[1]);
				item->alerm = htonl(val32);
				break;
			}
		}

		++resp->num;
	}
	resp->num = htons(resp->num);

	out->msg_type = alert;
	out->mac_id = inter_proto->mac_id;

	return BuildNewProto(MSG_REQUEST_MEDIA_SEARCH | 0x8000, buflen, buffer, &out->data_buffer);
}

bool ConvertProto::InterConvertTransDeliver(InterProto *inter_proto, NewProtoOut *out) {
	int buflen;
	const char *bufptr;
	char buffer[BUFSIZ + 1];
	TRANS_DELIVER *deliver;

	CBase64 base64;

	deliver = (TRANS_DELIVER*)buffer;
	strncpy(deliver->macid.oem_code, inter_proto->oem_code.c_str(), 6);
	get_bcdphone_from_internalproto((unsigned char *) deliver->macid.phone, inter_proto->car_id);

	deliver->type = atoi(inter_proto->kvmap["91"].c_str());
	bufptr = inter_proto->kvmap["90"].c_str();

	base64.Decode(bufptr, strlen(bufptr));
	deliver->data.len = base64.GetLength();
	strncpy(deliver->data.msg, base64.GetBuffer(), deliver->data.len);
	buflen = sizeof(TRANS_DELIVER) + deliver->data.len;

	out->msg_type = alert;
	out->mac_id = inter_proto->mac_id;

	return BuildNewProto(MSG_REQUEST_TRANS_DELIVER, buflen, buffer, &out->data_buffer);
}

bool ConvertProto::InterConvertLocation(InterProto *inter_proto, NewProtoOut *out)
{
	unsigned int msgLen;
	unsigned char *msgPtr;

	uint8_t cmdLen;
	uint16_t cmn2;
	uint32_t cmn4;

	char valStr[128];
	size_t eleSize;
	char *eleSet[32];

	map<string, string>::iterator mssIte;

	LOCATION_INFO location_info;
	memset(&location_info, 0, sizeof(LOCATION_INFO));

	location_info.dwLon = StringToInteger(inter_proto->kvmap["1"], sizeof(unsigned int)); //经度
	location_info.dwLat = StringToInteger(inter_proto->kvmap["2"], sizeof(unsigned int)); //纬度
	location_info.wSpeed = StringToInteger(inter_proto->kvmap["3"], sizeof(unsigned short)); //速度
	location_info.wDirection = StringToInteger(inter_proto->kvmap["5"], sizeof(unsigned short)); //方向
	location_info.wHight = StringToInteger(inter_proto->kvmap["6"], sizeof(unsigned short)); //高度
	location_info.dwState = StringToInteger(inter_proto->kvmap["8"], sizeof(unsigned int)); //状态

	memset(location_info.alarm_id, 0x00, 32);
	mssIte = inter_proto->kvmap.find("130");
	if(mssIte != inter_proto->kvmap.end()) {
		strncpy(location_info.alarm_id, mssIte->second.c_str(), 32);
	}

	location_info.dwAlerm = 0;
	mssIte = inter_proto->kvmap.find("131");
	if(mssIte != inter_proto->kvmap.end()) {
		location_info.dwAlerm = atoi(mssIte->second.c_str());
	}

	location_info.alarm_flag = 0;
    mssIte = inter_proto->kvmap.find("132");
    if(mssIte != inter_proto->kvmap.end()) {
    	location_info.alarm_flag = atoi(mssIte->second.c_str()) + 1;
    }

	memcpy(location_info.mac_list.oem_code, inter_proto->oem_code.c_str(), inter_proto->oem_code.length());

	get_bcdphone_from_internalproto((unsigned char *) location_info.mac_list.phone, inter_proto->car_id);
	get_bcdtime_from_internalproto(&location_info.byTime[0], inter_proto->kvmap["4"]);

	msgLen = sizeof(LOCATION_INFO) - 128;
	msgPtr = (unsigned char*)&location_info;

	mssIte = inter_proto->kvmap.find("9");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x01;
		msgPtr[msgLen++] = 0x04;
		cmn4 = htonl(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn4, 4);
		msgLen += 4;
	}

	mssIte = inter_proto->kvmap.find("24");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x02;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("7");
	if(mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x03;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("519");
	if(mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x04;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("31");
	if (mssIte != inter_proto->kvmap.end()) {
		SPEED_ALARM speed_alarm;

		strncpy(valStr, mssIte->second.c_str(), 128);
		eleSize = splitStr(valStr, "|", eleSet, 32);
		if(eleSize == 0) {
			return false;
		}

		speed_alarm.type = atoi(eleSet[0]);
		switch(speed_alarm.type) {
		case 0:
			if(eleSize != 1) {
				return false;
			}
			cmdLen = 1;
			speed_alarm.name = 0;
			break;
		default:
			if(eleSize != 2) {
				return false;
			}
			cmdLen = 5;
			speed_alarm.name = htonl(atoi(eleSet[1]));
			break;
		}

		msgPtr[msgLen++] = 0x11;
		msgPtr[msgLen++] = cmdLen;
		memcpy(msgPtr + msgLen, &speed_alarm, cmdLen);
		msgLen += cmdLen;
	}

	mssIte = inter_proto->kvmap.find("32");
	if (mssIte != inter_proto->kvmap.end()) {
		AREA_ALARM area_alarm;

		strncpy(valStr, mssIte->second.c_str(), 128);
		eleSize = splitStr(valStr, "|", eleSet, 32);
		if (eleSize != 3) {
			return false;
		}

		area_alarm.type = atoi(eleSet[0]);
		area_alarm.name = htonl(atoi(eleSet[1]));
		area_alarm.flag = atoi(eleSet[2]);

		cmdLen = sizeof(AREA_ALARM);
		msgPtr[msgLen++] = 0x12;
		msgPtr[msgLen++] = cmdLen;
		memcpy(msgPtr + msgLen, &area_alarm, cmdLen);
		msgLen += cmdLen;
	}

	mssIte = inter_proto->kvmap.find("35");
	if (mssIte != inter_proto->kvmap.end()) {
		ROAD_ALARM road_alarm;

		strncpy(valStr, mssIte->second.c_str(), 128);
		eleSize = splitStr(valStr, "|", eleSet, 32);
		if (eleSize != 3) {
			return false;
		}

		road_alarm.name = htonl(atoi(eleSet[0]));
		road_alarm.time = htons(atoi(eleSet[1]));
		road_alarm.flag = atoi(eleSet[2]) > 0 ? 1 : 0;

		cmdLen = sizeof(ROAD_ALARM);
		msgPtr[msgLen++] = 0x13;
		msgPtr[msgLen++] = cmdLen;
		memcpy(msgPtr + msgLen, &road_alarm, cmdLen);
		msgLen += cmdLen;
	}

	mssIte = inter_proto->kvmap.find("520");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x14;
		msgPtr[msgLen++] = 0x04;
		cmn4 = htonl(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn4, 4);
		msgLen += 4;
	}

	mssIte = inter_proto->kvmap.find("210");
	if(mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x20;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("216");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x21;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("218");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x28;
		msgPtr[msgLen++] = 0x01;
		msgPtr[msgLen++] = atoi(mssIte->second.c_str());
	}

	mssIte = inter_proto->kvmap.find("503");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x22;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("504");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x23;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("500");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x25;
		msgPtr[msgLen++] = 0x04;
		cmn4 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn4, 4);
		msgLen += 4;
	}

	mssIte = inter_proto->kvmap.find("213");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x26;
		msgPtr[msgLen++] = 0x04;
		cmn4 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn4, 4);
		msgLen += 4;
	}

	mssIte = inter_proto->kvmap.find("217");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x27;
		msgPtr[msgLen++] = 0x01;
		msgPtr[msgLen++] = atoi(mssIte->second.c_str());
	}

	mssIte = inter_proto->kvmap.find("505");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x40;
		msgPtr[msgLen++] = 0x04;
		cmn4 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn4, 4);
		msgLen += 4;
	}

	mssIte = inter_proto->kvmap.find("506");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x41;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("507");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x42;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("214");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x43;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("508");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x44;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("509");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x45;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("510");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x46;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("215");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x47;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	mssIte = inter_proto->kvmap.find("511");
	if (mssIte != inter_proto->kvmap.end()) {
		msgPtr[msgLen++] = 0x48;
		msgPtr[msgLen++] = 0x02;
		cmn2 = htons(atoi(mssIte->second.c_str()));
		memcpy(msgPtr + msgLen, &cmn2, 2);
		msgLen += 2;
	}

	if (location_info.alarm_flag != 0) {
		out->msg_type = alert;
	} else {
		out->msg_type = gps_info;
	}

	out->mac_id = inter_proto->mac_id;

	BuildNewProto(MSG_REQUEST_LOCATION_INFO_SERVICE, msgLen,
			(const char*) (&location_info), &(out->data_buffer));

	return true;
}

unsigned int ConvertProto::StringToInteger(string &s_value, int nType)
{
	unsigned int dw_value = 0xFFFFFFFF;

	if (s_value.empty())
	{
		return dw_value;
	}

	for (int i = 0; i < s_value.length(); i++)
	{
		if (!isdigit(s_value[i]))
		{
			return dw_value;
		}
	}

	if (nType == sizeof(unsigned int))
	{
		dw_value = htonl(atoi(s_value.c_str()));
	}
	else if (nType == sizeof(unsigned short))
	{
		dw_value = htons(atoi(s_value.c_str()));
	}

	return dw_value;
}

bool ConvertProto::parseItem(const string &text, vector<string> &res, char lc, char rc)
{
    string item;
    string::size_type len;
    string::size_type bPos;
    string::size_type lPos;
    string::size_type rPos;

    len = text.size();
    for(bPos = 0; bPos < len; bPos = rPos + 1) {
        if((lPos = text.find(lc, bPos)) == string::npos) {
            break;
        }
        ++lPos;

        if((rPos = text.find(rc, lPos)) == string::npos) {
            break;
        }

        if(lPos == rPos) {
            continue;
        }

        item = text.substr(lPos, rPos - lPos);
        res.push_back(item);
    }

    return true;
}
