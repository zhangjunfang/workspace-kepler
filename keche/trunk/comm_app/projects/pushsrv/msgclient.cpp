#include "msgclient.h"
#include <tools.h>
#include <netutil.h>
#include <intercoder.h>

MsgClient::MsgClient(void) :
		_session(true)
{
	_pMsgClient = NULL;
	_last_handle_user_time = time(NULL);
}

MsgClient::~MsgClient(void)
{
	Stop();
}

// 添加用户处理
void MsgClient::AddUser(const char *ip, unsigned short port, const char *username, const char *pwd)
{
	char szid[256] = {0};
	sprintf(szid, "%llu", netutil::strToAddr(ip, port));

	User user = _online_user.GetUserByUserId(szid);
	if (!user._user_id.empty())
		return;

	user._user_id = szid;
	user._ip = ip;
	user._port = port;
	user._user_name = username;
	user._user_pwd = pwd;
	user._user_type = "SAVE";
	user._user_state = User::OFF_LINE;
	user._socket_type = User::TcpConnClient;
	user._connect_info.keep_alive = AlwaysReConn;
	user._connect_info.timeval = 30;

	// 添加到用户队列中
	_online_user.AddUser(user._user_id, user);
}

// 处理删除服务
void MsgClient::DelUser(const char *ip, unsigned short port)
{
	char szid[256] = {0};
	sprintf(szid, "%llu", netutil::strToAddr(ip, port));

	User user = _online_user.GetUserByUserId(szid);
	if (user._user_id.empty())
		return;

	CloseSocket(user._fd);

	_online_user.DeleteUser(szid);
}

bool MsgClient::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv;

	setpackspliter(&_packspliter);

	return true;
}

void MsgClient::Stop(void)
{
	OUT_INFO("Msg", 0, "MsgClient", "stop");

	StopClient();
}

bool MsgClient::Start(void)
{
	return StartClient("0.0.0.0", 0, 1);
}

void MsgClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
//	FUNTRACE("void ClientAccessServer::HandleInterData(int fd, const void *data, int len)");
	if (len < 4)
	{
		OUT_ERROR( sock->_szIp, sock->_port, NULL, "recv data error length: %d", len );
		return;
	}

	OUT_RECV( sock->_szIp, sock->_port,  NULL, "on_data_arrived:[%d]%s", len, (const char*)data);

	CInterCoder coder;
	// 数据解密处理
	if (!coder.Decode((const char *) data, len))
	{
		OUT_ERROR( sock->_szIp, sock->_port, "Decode", "fd %d, MsgClient recv error data:%d,%s", sock->_fd, len, ( const char *)data );
		return;
	}

	const char *ptr = (const char *) coder.Buffer();
	if (strncmp(ptr, "CAIT", 4) == 0)
	{
		// 纷发处理数据
		HandleInnerData( sock, ptr, coder.Length());
	}
	else
	{
		// 处理登陆相关
		HandleSession( sock, ptr, coder.Length());
	}
}

void MsgClient::on_dis_connection( socket_t *sock )
{
	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	User user = _online_user.GetUserBySocket( sock );
	OUT_WARNING( sock->_szIp, sock->_port, user._user_id.c_str(), "Disconnection fd %d", sock->_fd );
	user._user_state = User::OFF_LINE;
	_online_user.SetUser(user._user_id, user);
}

void MsgClient::TimeWork()
{
	/*
	 * 1.将超时的连接去掉；
	 * 2.定时发送NOOP消息
	 * 3.Reload配置文件中的新的连接。
	 * 4.
	 */
	while (1)
	{
		if (!Check())
			break;

		HandleOfflineUsers();
		HandleOnlineUsers(30);

		// 处理超时的对象
		_session.CheckTimeOut(120);
		//这个地方，超时的同步有问题
		sleep(5);
	}
}

void MsgClient::NoopWork()
{

}

// 向MSG上传消息
void MsgClient::HandleMsgData(const char *macid, const char *data, int len)
{
	OUT_INFO( NULL, 0, "msg", "HandleUpMsgData %s", data );

	string val;
	if (!_session.GetSession(macid, val))
	{
		SendOnlineData(data, len);
		//OUT_ERROR( NULL, 0, macid, "Get Session Failed, Data %s" , data );
		return;
	}
	User user = _online_user.GetUserByUserId(val);
	if (user._user_id.empty() || user._fd == NULL )
	{
		OUT_ERROR( NULL, 0, macid, "Get User empty , User id: %s, data %s" , val.c_str() , data );
		return;
	}
	// 发送数据
	SendData(user._fd, data, len);
}

int MsgClient::build_login_msg(User &user, char *buf, int buf_len)
{
	sprintf(buf, "LOGI SAVE %s %s DM \r\n", user._user_name.c_str(), user._user_pwd.c_str());
	return (int) strlen(buf);
}

void MsgClient::HandleSession( socket_t *sock, const char *data, int len)
{
	string line(data, len - 2);

	vector<string> vec_temp;
	if (!splitvector(line, vec_temp, " ", 1))
	{
		return;
	}

	string head = vec_temp[0];
	if (head == "LACK")
	{
		/*
		 RESULT
		 >=0:权限值
		 -1:密码错误
		 -2:帐号已经登录
		 -3:帐号已经停用
		 -4:帐号不存在
		 -5:sql查询失败
		 -6:未登录数据库
		 */
		int ret = atoi(vec_temp[1].c_str());
		switch (ret)
		{
		case 0:
		case 1:
		case 2:
		case 3:
		{
			User user = _online_user.GetUserBySocket( sock );
			if (user._user_id.empty())
			{
				OUT_WARNING(sock->_szIp, sock->_port,  NULL, "Can't find the syn_user");
				return;
			}
			user._user_state = User::ON_LINE;
			user._last_active_time = time(NULL);
			// 重新处理用户状态
			_online_user.SetUser(user._user_id, user);

			OUT_CONN( sock->_szIp, sock->_port, user._user_name.c_str(), "Login success, fd %d access code %d" ,
					sock->_fd, user._access_code );

			// 如果登陆成就发送当前订阅的数据
			SendDemandData( sock );
		}
			break;
		case -1:
		{
			OUT_ERROR(sock->_szIp, sock->_port, NULL, "LACK,password error!");
		}
		break;
		case -2:
		{
			OUT_ERROR(sock->_szIp, sock->_port, NULL, "LACK,the user has already login!");
		}
		break;
		case -3:
		{
			OUT_ERROR(sock->_szIp, sock->_port, NULL, "LACK,user name is invalid!");
		}
		break;
		default:
		{
			OUT_ERROR( sock->_szIp, sock->_port, NULL, "unknow result" );
		}
		break;
		}

		// 如果返回错误则直接处理
		if (ret < 0)
		{
			_tcp_handle.close_socket( sock );
		}
	}
	else if (head == "NOOP_ACK")
	{
		User user = _online_user.GetUserBySocket( sock );
		user._last_active_time = time(NULL);
		_online_user.SetUser(user._user_id, user);

		OUT_INFO( sock->_szIp, sock->_port, user._user_name.c_str(), "NOOP_ACK");
	}
	else
	{
		OUT_WARNING( sock->_szIp, sock->_port, NULL, "except message:%s", (const char*)data);
	}
}

void MsgClient::HandleInnerData( socket_t *sock, const char *data, int len)
{
	User user = _online_user.GetUserBySocket( sock );
	if (user._user_id.empty() || sock != user._fd )
	{
		OUT_ERROR( sock->_szIp, sock->_port, "CAIS" , "find fd %d user failed, data %s", sock->_fd, data );
		return;
	}

	string line(data, len);
	vector<string> vec;
	if (!splitvector(line, vec, " ", 6))
	{
		OUT_ERROR( sock->_szIp, sock->_port, user._user_name.c_str() , "fd %d data error: %s", sock->_fd, data );
		return;
	}

	string macid = vec[2];
	// if store type need save session
	_session.AddSession(macid, user._user_id);

	// ToDo: 需要处理数据将数据递交到发布模块进行处理
	_pEnv->GetPushServer()->HandleData(data, len);

	user._last_active_time = time(NULL);
	_online_user.SetUser(user._user_id, user);
}

// 添加订阅机制
void MsgClient::AddDemand(const char *name, int type)
{
	if (name == NULL)
		return;

	char buf[1024] = {0};
	switch (type)
	{
	case DEMAND_MACID:
		if (_macidsubmgr.AddSubId(name))
		{
		}
		sprintf(buf, "ADD 0 {%s} \r\n", name);
		break;
	case DEMAND_GROUP:
		if (_groupsubmgr.AddSubId(name))
		{
		}
		sprintf(buf, "ADD 1 {%s} \r\n", name);
		break;
	}

	// 如果需要发送通知
	int len = strlen(buf);
	if (len > 0)
	{
		SendOnlineData(buf, len);
	}
}

// 取消定订阅
void MsgClient::DelDemand(const char *name, int type)
{
	if (name == NULL)
		return;

	char buf[1024] = {0};
	switch (type)
	{
	case DEMAND_MACID:
		if (_macidsubmgr.DelSubId(name))
		{
		}
		sprintf(buf, "UMD 0 {%s} \r\n", name);
		break;
	case DEMAND_GROUP:
		if (_groupsubmgr.DelSubId(name))
		{
		}
		sprintf(buf, "UMD 1 {%s} \r\n", name);
		break;
	}

	// 如果需要发送通知
	int len = strlen(buf);
	if (len > 0)
	{
		SendOnlineData(buf, len);
	}
}

// 通知所有用户
void MsgClient::SendOnlineData(const char *data, int len)
{
	vector<User> vec = _online_user.GetOnlineUsers();
	if (vec.empty())
		return;

	for (int i = 0; i < vec.size(); ++i)
	{
		User &user = vec[i];
		SendData(user._fd, data, len);
	}
}

// 纷发登陆用
void MsgClient::SendDemandData( socket_t *sock )
{
	set<string>::iterator it;
	// 当前组不空需要处理
	if (!_groupsubmgr.IsEmpty())
	{
		string groups;
		set<string> &temp = _groupsubmgr.GetSubIds();
		for (it = temp.begin(); it != temp.end(); ++it)
		{
			if (!groups.empty())
			{
				groups += ",";
			}
			groups += *it;
		}

		string scmd = "DMD 1 {" + groups + "} \r\n";
		SendData( sock, scmd.c_str(), scmd.length());
		OUT_INFO( sock->_szIp, sock->_port, NULL, "fd %d, Send group cmd %s", sock->_fd, scmd.c_str() );
	}


	// 如果当前MACID不为空也需要处理
	if (!_macidsubmgr.IsEmpty())
	{
		string macids;
		set<string> &temp = _macidsubmgr.GetSubIds();
		for (it = temp.begin(); it != temp.end(); ++it)
		{
			if (!macids.empty())
			{
				macids += ",";
			}
			macids += *it;
		}

		string scmd = "ADD 0 {" + macids + "} \r\n";
		SendData( sock, scmd.c_str(), scmd.length());
		OUT_INFO( sock->_szIp, sock->_port, NULL, "fd %d, Send macid cmd %s", sock->_fd, scmd.c_str() );
	}
}

void MsgClient::HandleOfflineUsers()
{
	vector<User> vec_users = _online_user.GetOfflineUsers(3 * 60);
	for (int i = 0; i < (int) vec_users.size(); i++)
	{
		User &user = vec_users[i];
		if (user._socket_type == User::TcpClient)
		{
			if (user._fd != NULL )
			{
				OUT_WARNING( user._ip.c_str(), user._port, user._user_name.c_str(), "fd %d close socket", user._fd->_fd );
				CloseSocket(user._fd);
			}
		}
		else if (user._socket_type == User::TcpConnClient)
		{
			if (user._fd != NULL )
			{
				OUT_WARNING( user._ip.c_str(), user._port, user._user_name.c_str(),
						"client fd %d close socket", user._fd->_fd );
				user.show();
				CloseSocket(user._fd);
				user._fd = NULL ;
			}
			if (ConnectServer(user, 10))
			{
				// 添加列表中。
				_online_user.AddUser(user._user_id, user);
			}
			else if (user._connect_info.keep_alive == AlwaysReConn)
			{
				// 添加用户
				_online_user.AddUser(user._user_id, user);
			}
		}
	}
}

void MsgClient::HandleOnlineUsers(int timeval)
{
	time_t now = time(NULL);
	if (now - _last_handle_user_time < timeval)
	{
		return;
	}
	_last_handle_user_time = now;

	vector<User> vec_users = _online_user.GetOnlineUsers();
	for (int i = 0; i < (int) vec_users.size(); i++)
	{
		User &user = vec_users[i];
		if ( user._socket_type == User::TcpConnClient && user._fd != NULL )
		{
			string loop = "NOOP \r\n";
			SendData(user._fd, loop.c_str(), loop.length());
			OUT_SEND( user._ip.c_str(), user._port, user._user_id.c_str(), "NOOP");
		}
	}
}

