/*
 * photosvr.cpp
 *
 *  Created on: 2013-5-8
 *      Author: ycq
 *  消息转发服务
 */

#include "msgclient.h"
#include "photosvr.h"
#include <comlog.h>
#include <tools.h>
#include <netutil.h>
#include <arpa/inet.h>

#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <unistd.h>

#include "tinyxml.h"
#include "httpquery.h"
#include "../tools/utils.h"

#define DOMAIN_MAP "PHOTO3G_MAP"
#define DOMAIN_USR "PHOTO3G_USR"

PhotoSvr::PhotoSvr()
{
	_pEnv = NULL;
	_seqid = 0;
	_httpUrl = "";
	_thread_num = 8;
	_timeQueue.init(30);
}

PhotoSvr::~PhotoSvr(void)
{
	Stop();
}

bool PhotoSvr::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv;

	char szip[128] = { 0 };
	if (!pEnv->GetString("photo3g_listen_ip", szip)) {
		OUT_ERROR( NULL, 0, NULL, "get photo3g_listen_ip failed");
		return false;
	}
	_listen_ip = szip;

	int port = 0;
	if (!pEnv->GetInteger("photo3g_listen_port", port)) {
		OUT_ERROR( NULL, 0, NULL, "get photo3g_listen_port failed");
		return false;
	}
	_listen_port = port;

	char buf[1024];
	if ( ! pEnv->GetString("scp_path", buf)) {
		OUT_ERROR( NULL, 0, NULL, "get scp_path failed");
		return false;
	}
	_scp_path = buf;

	if( ! _pEnv->GetString("http_url", buf)) {
		OUT_ERROR( NULL, 0, NULL, "http_url empty" ) ;
		return false;
	}
	_httpUrl = buf;

	int nvalue = 8;
	if (pEnv->GetInteger("photo3g_tcp_thread", nvalue)) {
		_thread_num = nvalue;
	}

	// 设置数据分包对象
	_tcp_handle.setpackspliter(&_pack_spliter);

	return true;
}

bool PhotoSvr::Start(void)
{
	return StartServer(_listen_port, _listen_ip.c_str(), _thread_num);
}

// 重载STOP方法
void PhotoSvr::Stop(void) {
	StopServer();
}

void PhotoSvr::TimeWork() {
	vector<User> users;
	vector<User>::iterator ite;

	while (Check()) {
		users = _online_user.GetOfflineUsers(SESS_TIMEOUT);

		for ( ite = users.begin(); ite != users.end(); ++ ite ) {
			CloseSocket( ite->_fd );
			OUT_INFO( NULL, 0, ite->_user_name.c_str(), "%s:%d, TimeOut", ite->_ip.c_str(), ite->_port);
		}

		sleep(10);
	}
}

void PhotoSvr::NoopWork() {
	OUT_INFO( NULL, 0, NULL, "void PhotoSvr::NoopWork()");

	list<InnerMsg> msgs;

	while (Check()) {
		msgs.clear();
		_timeQueue.check(msgs);
		while( ! msgs.empty()) {
			InnerMsg &msg = msgs.front();

			msg.begin = "CAITR";
			msg.param.clear();
			msg.param.insert(make_pair("RET", "5"));
			_pEnv->GetMsgClient()->HandleData(msg);

			msgs.pop_front();
		}

		sleep(10);
	}
}

void PhotoSvr::on_data_arrived( socket_t *sock, const void *data, int len)
{
	OUT_HEX(sock->_szIp, sock->_port, "RECV", (char*)data, len);

	User user = _online_user.GetUserBySocket(sock);
	if(user._user_id.empty()) {
		return;
	}

	uint16_t msgcmd; // 消息类型
	uint32_t msglen; // 消息体长度
	uint32_t msgseq; // 消息序列号

	vector<unsigned char> bin; // 内存块，用于构造消息
	DVR_HEAD *headptr = (DVR_HEAD*)data;
	DVR_LOGIN *login;
	DVR_REPLY *reply;
	DVR_UPLOAD_PIC *upload_pic;

	msgcmd = ntohs(headptr->cmd);
	msglen = ntohl(headptr->len);
	msgseq = ntohl(headptr->seq);

	IRedisCache *redis = _pEnv->GetRedisCache();

	if(msgcmd == 0x0001 && msglen == sizeof(DVR_LOGIN)) {
		uint8_t result = 0;
		login = (DVR_LOGIN*)((char*)data + sizeof(DVR_HEAD));
		string hKey, hVal;
		string name, pass;

		name.assign(login->name, strnlen(login->name, 16));
		pass.assign(login->pass, strnlen(login->pass, 32));

		if( ! redis->HGet(DOMAIN_USR, name.c_str(), hVal) || pass != hVal) {
			// 在缓存PHOTO3G_USR域下没找到匹配的用户信息
			OUT_ERROR(sock->_szIp, sock->_port, "LOGIN", "check fail: %s, %s", name.c_str(), pass.c_str());
			result = 1;
		}

		bin.resize(sizeof(DVR_HEAD) + sizeof(DVR_REPLY));
		headptr = (DVR_HEAD*)&bin[0];
		reply = (DVR_REPLY*)(&bin[0] + sizeof(DVR_HEAD));

		headptr->ver = htons(0x0101);
		headptr->cmd = htons(0x8001);
		headptr->len = htonl(sizeof(DVR_REPLY));
		headptr->seq = htonl(getSeqid());
		reply->seq = htonl(msgseq);
		reply->res = result;

		SendData(sock, (char*)&bin[0], bin.size());

		if(result != 0) {
			CloseSocket(sock);
			return;
		}

		user._user_name = name;
		user._user_pwd  = pass;
		user._last_active_time = time(NULL);
		_online_user.SetUser(user._user_id, user);

		return;
	}

	if(user._user_name.empty() || user._user_pwd.empty()) {
		// 未成功登录的会话，不能发送其它消息
		OUT_ERROR( sock->_szIp, sock->_port, "LOGIN", "send msg before login, close fd: %d", sock->_fd);
		CloseSocket(user._fd);
		return;
	}

	char buffer[1024];
	InnerMsg imsg;

	if ((msgcmd == 0x8004 || msgcmd == 0x8005) && msglen == sizeof(DVR_REPLY)) {
		map<uint32_t, InnerMsg>::iterator iteAck;

		reply = (DVR_REPLY*)((char*)data + sizeof(DVR_HEAD));

		if(_timeQueue.erase(reply->seq, imsg) == false) {
			// 没有找到对应的等待应答数据
			return;
		}

		sprintf(buffer, "%u", reply->res);

		imsg.begin = "CAITR";
		imsg.param.clear();
		imsg.param.insert(make_pair("RET", buffer));
		imsg.param.insert(make_pair("CHANNEL_TYPE", "1"));
		_pEnv->GetMsgClient()->HandleData(imsg);
	} else if (msgcmd == 0x0002 && msglen == 0) {
		bin.resize(sizeof(DVR_HEAD) + sizeof(DVR_REPLY));
		headptr = (DVR_HEAD*) &bin[0];
		reply = (DVR_REPLY*) (&bin[0] + sizeof(DVR_HEAD));

		headptr->ver = htons(0x0101);
		headptr->cmd = htons(0x8002);
		headptr->len = htonl(sizeof(DVR_REPLY));
		headptr->seq = htonl(getSeqid());
		reply->seq = htonl(msgseq);
		reply->res = 0;

		SendData(sock, (char*) &bin[0], bin.size());
	} else if (msgcmd == 0x0003 && msglen > sizeof(DVR_UPLOAD_PIC)) {
		int fd;

		string picpath; // 图片本地保存路径
		string urlStr;  // 图片url地址名称
		size_t urlLen;  // 图片url地址长度
		string suffix;  // .jpeg后缀
		HttpQuery hquery; // http请求对象

		string oemCode;
		string macid;
		string sim2g, sim3g;
		uint32_t takeseq;

		upload_pic = (DVR_UPLOAD_PIC*) ((char*) data + sizeof(DVR_HEAD));
		takeseq = ntohl(upload_pic->takeseq);
		if(upload_pic->mmtype != 0 || upload_pic->mmfmt != 0) {
			// 目前只有jpeg格式的图片上传
			OUT_WARNING(sock->_szIp, sock->_port, NULL, "parameter error, seqid: %x", msgseq);
			return;
		} else if(upload_pic->mmevent == 0 && takeseq == 0) {
			// 立即拍照的拍摄序列号大于0
			OUT_WARNING(sock->_szIp, sock->_port, NULL, "parameter error, seqid: %x", msgseq);
			return;
		} else if(upload_pic->mmevent == 1 && takeseq > 0) {
			// 定时拍照的拍摄序列号等于0
			OUT_WARNING(sock->_szIp, sock->_port, NULL, "parameter error, seqid: %x", msgseq);
			return;
		}

		urlLen = msglen - sizeof(DVR_UPLOAD_PIC);   // 前面已经判断，长度大于0
		urlStr = "http://" + string(upload_pic->mmurl, urlLen);
		suffix = urlStr.substr(urlStr.size() - 5);  // 至少有http://，长度大于5

		// 检测有无正确的jpeg图片后缀，减少http调用
		if (suffix != ".jpeg" && suffix != ".JPEG") {
			OUT_WARNING(sock->_szIp, sock->_port, NULL, "parameter error, seqid: %x", msgseq);
			return;
		}

		if ( ! hquery.get(urlStr)) {
			OUT_WARNING(sock->_szIp, sock->_port, NULL, "get fail: %s", urlStr.c_str());
			return;
		}

		// 图片下载成功后发送应答
		bin.resize(sizeof(DVR_HEAD) + sizeof(DVR_REPLY));
		headptr = (DVR_HEAD*) &bin[0];
		reply = (DVR_REPLY*) (&bin[0] + sizeof(DVR_HEAD));

		headptr->ver = htons(0x0101);
		headptr->cmd = htons(0x8003);
		headptr->len = htonl(sizeof(DVR_REPLY));
		headptr->seq = htonl(getSeqid());
		reply->seq = htonl(msgseq);
		reply->res = 0;
		SendData(sock, (char*) &bin[0], bin.size());

		sim3g.assign(upload_pic->simid, 11);
		if( ! get2gby3g(sim3g, sim2g, oemCode)) {
			OUT_WARNING(sock->_szIp, sock->_port, "get2gby3g", "fail: %s", sim3g.c_str());
			return;
		}
		macid = oemCode + "_" + sim3g;

		snprintf(buffer, 1024, "%.4s/%.2s/%.2s/%.14s-%s-%d-%d-%d-%d-%d.jpeg",
				upload_pic->taketime, upload_pic->taketime + 4,
				upload_pic->taketime + 6, upload_pic->taketime,
				macid.c_str(), ntohl(upload_pic->mmid), upload_pic->mmevent,
				upload_pic->channel, upload_pic->mmtype, upload_pic->mmfmt);

		if ( ! createDir(buffer)) {
			OUT_ERROR( sock->_szIp, sock->_port, "createDir", "verfiy path fail: %s", buffer);
			return;
		}

		picpath = _scp_path + string("/") + buffer;
		if ((fd = open(picpath.c_str(), O_CREAT | O_WRONLY, 0644)) < 0) {
			OUT_ERROR( sock->_szIp, sock->_port, "OPEN", "create file fail: %s", picpath.c_str());
			return;
		}
		write(fd, hquery.data(), hquery.size());
		close(fd);

		imsg.begin = "CAITS";
		imsg.seqid = "0_0";
		imsg.macid = oemCode + "_" + sim2g;
		imsg.order = "U_REPT";
		imsg.param.insert(make_pair("TYPE", "3"));
		imsg.param.insert(make_pair("CHANNEL_TYPE", "1"));
		imsg.param.insert(make_pair("125", buffer));
		sprintf(buffer, "%d", ntohl(upload_pic->mmid));
		imsg.param.insert(make_pair("120", buffer));
		sprintf(buffer, "%d", upload_pic->mmtype);
		imsg.param.insert(make_pair("121", buffer));
		sprintf(buffer, "%d", upload_pic->mmfmt);
		imsg.param.insert(make_pair("122", buffer));
		sprintf(buffer, "%d", upload_pic->mmevent);
		imsg.param.insert(make_pair("123", buffer));
		sprintf(buffer, "%d", upload_pic->channel);
		imsg.param.insert(make_pair("124", buffer));
		sprintf(buffer, "%.12s", upload_pic->taketime + 2);
		imsg.param.insert(make_pair("126", buffer));
		sprintf(buffer, "%u", takeseq);
		imsg.param.insert(make_pair("127", buffer));

		_pEnv->GetMsgClient()->HandleData(imsg);
	} else {
		OUT_HEX( sock->_szIp, sock->_port, "ErrorMsg", (const char *)data, len ) ;
		return;
	}

	user._last_active_time = time(NULL);
	_online_user.SetUser(user._user_id, user);
}

void PhotoSvr::on_new_connection( socket_t *sock, const char* ip, int port)
{
	char szid[256] = {0};
	sprintf(szid, "%lu", netutil::strToAddr(ip, port));

	User user = _online_user.GetUserByUserId(szid);
	if ( ! user._user_id.empty())
		return;

	user._user_id = szid;
	user._fd = sock ;
	user._ip = ip;
	user._port = port;
	user._user_state = User::ON_LINE;
	user._socket_type = User::TcpServer;
	user._last_active_time = time(NULL);

	// 添加到用户队列中
	_online_user.AddUser(user._user_id, user);
}

void PhotoSvr::on_dis_connection( socket_t *sock )
{
	User user = _online_user.DeleteUser( sock );
}

bool PhotoSvr::get2gby3g(const string &sim3g, string &sim2g, string &oem)
{
	const char *chrVal;
	string strVal;
	vector<string> fields;

	IRedisCache *redis = _pEnv->GetRedisCache();
	time_t curTime = time(NULL);

	if (redis->HGet(DOMAIN_MAP, sim3g.c_str(), strVal)
			&& Utils::splitStr(strVal, fields, ':') == 3
			&& atoi(fields[0].c_str()) > curTime) {
		oem = fields[1];
		sim2g = fields[2];

		if (oem.length() != 4 || sim2g.length() != 11) {
			return false; // 长度不正确
		}

		return true;
	}

	char strTime[128];
	snprintf(strTime, 128, "%lu", curTime + 1800); // 30分钟超时更新
	strVal = strTime + string(":0:0"); // 获取号码失败时使用的默认值

	HttpQuery hq;
	string dat = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"\
			"<Request service=\"vehicleInforService\" "\
			"method=\"get2gBy3g\" id=\"0\"><Param><Item  "\
			"sim3=\"" + sim3g + "\" /></Param></Request>";

	if( ! hq.post(_httpUrl, dat)) {
		OUT_WARNING(NULL, 0, "get2gby3g", "http query %s fail", sim3g.c_str());
		redis->HSet(DOMAIN_MAP, sim3g.c_str(), strVal.c_str());
		return false;
	}

	string strXml((char*)hq.data(), hq.size());
    TiXmlDocument doc;
    TiXmlElement *root;
    TiXmlElement *node;

	doc.Parse(strXml.c_str(), 0, TIXML_ENCODING_UTF8);
	if ((root = doc.RootElement()) == NULL) {
		redis->HSet(DOMAIN_MAP, sim3g.c_str(), strVal.c_str());
		return false;
	}

	if ((node = root->FirstChildElement("Result")) == NULL) {
		redis->HSet(DOMAIN_MAP, sim3g.c_str(), strVal.c_str());
		return false;
	}

	if ((node = node->FirstChildElement("Item")) == NULL) {
		redis->HSet(DOMAIN_MAP, sim3g.c_str(), strVal.c_str());
		return false;
	}

	if ((chrVal = node->Attribute("oemcode")) == NULL) {
		redis->HSet(DOMAIN_MAP, sim3g.c_str(), strVal.c_str());
		return false;
	}
	oem = chrVal;

	if ((chrVal = node->Attribute("commaddr")) == NULL) {
		redis->HSet(DOMAIN_MAP, sim3g.c_str(), strVal.c_str());
		return false;
	}
	sim2g = chrVal;

	if (oem.length() != 4 || sim2g.length() != 11) {
		redis->HSet(DOMAIN_MAP, sim3g.c_str(), strVal.c_str());
		return false;
	}

	// 在缓存中更新3g号码查2g号码的映射
	strVal = strTime + string(":") + oem + ":" + sim2g;
	redis->HSet(DOMAIN_MAP, sim3g.c_str(), strVal.c_str());
	// 在缓存中更新2g号码查3g号码的映射
	strVal = strTime + string(":") + oem + ":" + sim3g;
	redis->HSet(DOMAIN_MAP, sim2g.c_str(), strVal.c_str());

	return true;
}

// 从MSG转发过来的数据
void PhotoSvr::HandleData(const InnerMsg &msg)
{
	size_t i;
	InnerMsg newMsg;

	vector<User> users = _online_user.GetOnlineUsers();
	if(users.empty()) {
		// 3g终端不在线，回复内部消息
		newMsg = msg;
		newMsg.begin = "CAITR";
		newMsg.param.clear();
		newMsg.param.insert(make_pair("RET", "4"));
		newMsg.param.insert(make_pair("CHANNEL_TYPE", "1"));
		_pEnv->GetMsgClient()->HandleData(newMsg);

		return;
	}

	string simid = msg.macid.substr(5);
	string type = getInnerMsgArg(msg, "TYPE");
	unsigned char *bIte;
	unsigned char *eIte;
	vector<unsigned char> bin;

	if(msg.order == "D_CTLM" && type == "10") {
		DVR_HEAD head;
		DVR_MANUAL_PIC body;

		vector<string> fields;
		string value = getInnerMsgArg(msg, "VALUE");

		if(Utils::splitStr(value, fields, '|') != 10) {
			return;
		}

		head.ver = htons(0x0101);
		head.cmd = htons(0x0004);
		head.len = htonl(sizeof(DVR_MANUAL_PIC));
		head.seq = htonl(getSeqid());

		strncpy(body.simid, simid.c_str(), 12);
		body.channel = atoi(fields[0].c_str());
		body.takesize = htons(atoi(fields[1].c_str()));
		body.taketime = htons(atoi(fields[2].c_str()));
		body.resolution = atoi(fields[4].c_str()) + 1;
		body.quality = atoi(fields[5].c_str());
		body.light = atoi(fields[6].c_str());
		body.contrast = atoi(fields[7].c_str());
		body.saturation = atoi(fields[8].c_str());
		body.chroma = atoi(fields[9].c_str());
		body.takeseq = htonl(atoi(getInnerMsgArg(msg, "191").c_str()));

		bin.clear();
		bIte = (unsigned char*)&head;
		eIte = bIte + sizeof(DVR_HEAD);
		bin.insert(bin.end(), bIte, eIte);
		bIte = (unsigned char*) &body;
		eIte = bIte + sizeof(DVR_MANUAL_PIC);
		bin.insert(bin.end(), bIte, eIte);

		_timeQueue.insert(head.seq, msg);
	} else if (msg.order == "D_SETP" && type == "0") {
		DVR_HEAD head;
		DVR_TIMING_PIC body;

		string   strInfo;
		uint32_t intInfo;
		string   btime;
		string   etime;

		strInfo = getInnerMsgArg(msg, "180");
		if(strInfo.empty()) {
			return;
		}
		intInfo = atoi(strInfo.c_str());

		bin.clear();
		// 四个通道
		for(i = 0; i < 4; ++i) {
			head.ver = htons(0x0101);
			head.cmd = htons(0x0005);
			head.len = htonl(sizeof(DVR_TIMING_PIC));
			head.seq = htonl(getSeqid());

			strncpy(body.simid, simid.c_str(), 12);
			body.channel = i + 1;
			body.resolution = atoi(getInnerMsgArg(msg, "309").c_str());
			body.quality = atoi(getInnerMsgArg(msg, "136").c_str());
			body.light = atoi(getInnerMsgArg(msg, "137").c_str());
			body.contrast = atoi(getInnerMsgArg(msg, "138").c_str());
			body.saturation = atoi(getInnerMsgArg(msg, "139").c_str());
			body.chroma = atoi(getInnerMsgArg(msg, "140").c_str());
			body.interval = htons((intInfo >> 16) & 0xffff);

			btime = getInnerMsgArg(msg, "610");
			etime = getInnerMsgArg(msg, "611");
			if(btime.length() != 14 || etime.length() != 14) {
				strncpy(body.btime, "00000000000000", 14);
				strncpy(body.etime, "00000000000000", 14);
			} else {
				strncpy(body.btime, btime.c_str(), 14);
				strncpy(body.etime, etime.c_str(), 14);
			}

			bIte = (unsigned char*)&head;
			eIte = bIte + sizeof(DVR_HEAD);
			bin.insert(bin.end(), bIte, eIte);
			bIte = (unsigned char*) &body;
			eIte = bIte + sizeof(DVR_TIMING_PIC);
			bin.insert(bin.end(), bIte, eIte);
		}

		newMsg = msg;
		newMsg.begin = "CAITR";
		newMsg.param.clear();
		newMsg.param.insert(make_pair("RET", "0"));
		newMsg.param.insert(make_pair("CHANNEL_TYPE", "1"));
		_pEnv->GetMsgClient()->HandleData(newMsg);
	}

	for(i = 0; i < users.size(); ++i) {
		if(users[i]._user_name.empty() || users[i]._user_pwd.empty()) {
			continue;
		}

		OUT_HEX(users[i]._fd->_szIp, users[i]._fd->_port, "SEND", (char*)&bin[0], bin.size());
		SendData(users[i]._fd, (char*)&bin[0], bin.size());
	}
}

string PhotoSvr::getInnerMsgArg(const InnerMsg &msg, const string &key)
{
	map<string, string>::const_iterator ite;

	ite = msg.param.find(key);
	if(ite == msg.param.end()) {
		return "";
	}

	return ite->second;
}

uint32_t PhotoSvr::getSeqid()
{
	return __sync_add_and_fetch(&_seqid, 1);
}

bool PhotoSvr::createDir(const string &file)
{
    string path;
    string::size_type posPrev;
    string::size_type posNext;

    posPrev = 0;
    while((posNext = file.find('/', posPrev)) != string::npos) {
        posPrev = posNext + 1;

        path = _scp_path + "/" + file.substr(0, posNext);
        if(access(path.c_str(), R_OK | W_OK | X_OK) == 0) {
            continue;
        }

       if(mkdir(path.c_str(), 0777) != 0) {
    	   return false;
       }
    }

    return true;
}
