/**********************************************
 * MsgClient.cpp
 *
 *  Created on: 2010-7-12
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/

#include "msgclient.h"
#include "comlog.h"
#include <GBProtoParse.h>
#include <Base64.h>
#include <tools.h>
#include <intercoder.h>

#include "../tools/utils.h"
#include "httpquery.h"

MsgClient::MsgClient( CacheDataPool &cache_data_pool ) :
		_cache_data_pool( cache_data_pool ), _dataqueue( this )
{
	_gb_proto_handler = GbProtocolHandler::getInstance();
	_thread_num = 8;
}

MsgClient::~MsgClient( )
{
	Stop();
	if ( _gb_proto_handler != NULL ) {
		GbProtocolHandler::FreeHandler( _gb_proto_handler );
		_gb_proto_handler = NULL;
	}
}

bool MsgClient::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv;

	char ip[128] = { 0 };
	if ( ! pEnv->GetString( "msg_connect_ip", ip ) ) {
		INFO_PRT( NULL, 0,"MsgClient", "Get msg_connect_ip failed" );
		return false;
	}
	_client_user._ip = ip;

	int port = 0;
	if ( ! pEnv->GetInteger( "msg_connect_port", port ) ) {
		INFO_PRT( NULL, 0,"MsgClient", "Get msg_connect_port failed" );
		return false;
	}
	_client_user._port = port;

	int thread = 0;
	if ( pEnv->GetInteger( "msg_thread_count", thread ) ) {
		_thread_num = thread;
	}

	char buf[1024] = { 0 };
	if ( pEnv->GetString( "msg_user_name", buf ) ) {
		_client_user._user_name = buf;
	}

	if ( pEnv->GetString( "msg_user_pwd", buf ) ) {
		_client_user._user_pwd = buf;
	}

	_client_user._fd = NULL;
	_client_user._user_type = "PIPE";
	_client_user._user_state = User::OFF_LINE;
	_client_user._connect_info.reconnect_times = 0;
	_client_user._socket_type = User::TcpConnClient;
	_client_user._connect_info.keep_alive = AlwaysReConn;
	_client_user._connect_info.timeval = 3;



	// 设置分包对象
	setpackspliter( & _pack_spliter );

	if ( ! pEnv->GetString( "base_filedir" , buf ) ) {
		printf( "load base_filedir failed\n" ) ;
		return false ;
	}

	int nvalue = 0;
	pEnv->GetInteger( "sendcache_speed", nvalue ) ;

	char temp[1024] = {0} ;
	sprintf( temp, "%s/wasv4", buf ) ;

	return _dataqueue.Init( temp, nvalue );
}

bool MsgClient::Start( void )
{
	return StartClient( _client_user._ip.c_str(), _client_user._port, _thread_num );
}

void MsgClient::Stop( void )
{
	StopClient();
}

void MsgClient::HandleUpData( const char *data, int len )
{
	if(len < 3) { //空格\r\n三个字节
		return;
	}

	User &user = _client_user;
	if ( user._user_state != User::ON_LINE || user._fd == NULL ) {
		// 添加暂时缓存队列中
		_dataqueue.WriteCache( WAS_CLIENT_ID, ( void* ) data, len );
		return;
	}

	// 实际这里下发应该是上传才对
	if ( ! SendRC4Data( user._fd, data, len ) ) {
		OUT_ERROR( user._ip.c_str(), user._port, user._user_id.c_str() , "Send data failed");
		_dataqueue.WriteCache( WAS_CLIENT_ID, ( void * ) data, len );
		return;
	}

	OUT_SEND3( user._ip.c_str(), user._port, "SEND" , "%.*s" , len -3 , data );
}

void MsgClient::on_data_arrived( socket_t *sock, const void* data, int len )
{
	if ( len < 4 ) return;

	const char *ip = sock->_szIp ;
	int port = sock->_port ;

	string line( ( const char * ) data, len - 2);

	OUT_RECV3( ip, port, "MsgClient", "%.*s", line.size(), line.c_str() );

	User &user = _client_user;

	vector< string > vec_temp;
	if ( ! splitvector( line, vec_temp, " ", 0 ) )
		return;

	string head = vec_temp[0];

	if ( head == "LACK" ) {
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
		if ( vec_temp.size() < 2 )
			return;

		string result = vec_temp[1];
		if ( result == "0" ) {//登录成功
			user._user_state = User::ON_LINE;
			user._last_active_time = time( 0 );
			OUT_WARNING( ip, port, "MsgClient", "LOGI received LACK,login was success!" );
		} else if ( result == "-2" ) {
			OUT_WARNING( ip, port, "MsgClient", "LOGI received LACK,login fail -2, user already login" );
		} else {  // 其它为登陆认证失败
			OUT_WARNING( ip, port, "MsgClient", "LOGI received LACK,login fail %s" , result.c_str() );
		}

	} else if ( head == "NOOP_ACK" ) {
		// 更新用户最后一次时间
		user._last_active_time = time( 0 );
	} else if ( head == "CAITS" ) {
		//OUT_RECV( NULL, 0,"MsgClient", "MsgClient:%s", line.c_str() );
		//解析下行指令。
		if ( vec_temp.size() < 6 )
			return;

		if ( vec_temp[4] == "D_CALL" ) {
			HandleDcallMsg( line );
		} else if ( vec_temp[4] == "D_SETP" ) {
			HandleDsetpMsg( line );
		} else if ( vec_temp[4] == "D_GETP" )  // 取得查询参数
				{
			HandleDgetpMsg( line );
		} else if ( vec_temp[4] == "D_CTLM" ) {
			HandleDctlmMsg( line );
		} else if ( vec_temp[4] == "D_SNDM" ) {
			HandleDsndmMsg( line );
		} else if ( vec_temp[4] == "D_REQD" )  // 操作多媒体指令
				{
			HandleReqdMsg( line );
		}
	}
#ifdef _GPS_STAT
	else if ( head == "START" || head == "STOP" )
	{
		// 通过指令来开启是否计数
		_pEnv->SetGpsState( (head == "START") );
	}
#endif
}

// 拆分数据
static int split2map( vector< string > &vec, map< string, string > &mp, const char *split )
{
	mp.clear();

	int count = 0;
	int len = strlen( split );

	map< string, string >::iterator it;
	for ( int i = 0 ; i < ( int ) vec.size() ; ++ i ) {
		size_t pos = vec[i].find( split, 0 );
		if ( pos == string::npos ) {
			continue;
		}

		string key = vec[i].substr( 0, pos );
		string value = vec[i].substr( pos + len );
		if ( ! value.empty() ) {
			++ count;
		}
		it = mp.find( key );
		if ( it != mp.end() ) {  // 如果存在多个同名则并列处理
			it->second += "|";
			it->second += value;
		} else {
			mp.insert( make_pair( key, value ) );
		}
	}
	return count;
}

// 解析151：[]数据
static bool parsemultidata( const string &content, const string &key, vector< string > &vec, const char cbegin,
		const char cend )
{
	vec.clear();

	size_t pos = content.find( key.c_str() );
	if ( pos == string::npos ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Command param empty, line:%s" , content.c_str() );
		return false;
	}
	string sval = content.substr( pos + key.length() );
	if ( sval.empty() ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Command param %s error, line:%s" , key.c_str(), content.c_str() );
		return false;
	}

	size_t end = 0;
	pos = sval.find( cbegin, 0 );
	while ( pos != string::npos ) {
		end = sval.find( cend, pos + 1 );
		if ( end == string::npos ) {
			break;
		}
		// 存放解析出来的数据[]分割数据
		vec.push_back( sval.substr( pos + 1, end - pos - 1 ) );
		pos = sval.find( cbegin, end + 1 );
	}
	if ( vec.empty() ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Command %s empty, line %s" , key.c_str(), content.c_str() );
		return false;
	}
	return true;
}

// 将字符串时间转为BCD时间
static void convert2bcd( const string &time, char bcd[6] )
{
	time_t ntime = atoll( time.c_str() );
	struct tm local_tm;
	struct tm *tm = localtime_r( & ntime, & local_tm );

	char buf[128] = { 0 };
	sprintf( buf, "%02d%02d%02d%02d%02d%02d", ( tm->tm_year + 1900 ) % 100, tm->tm_mon + 1, tm->tm_mday, tm->tm_hour,
			tm->tm_min, tm->tm_sec );

	// 转换为BCD时间
	strtoBCD( buf, bcd );
}

// 折分数据
static bool splitmsgheader( const string &line, string &seq, string &macid, string &carid, string &command,
		string &content, map< string, string > &mp )
{
	vector< string > vec_temp;
	splitvector( line, vec_temp, " ", 0 );
	if ( vec_temp.size() < 6 )
		return false;

	seq = vec_temp[1];
	macid = vec_temp[2];
	command = vec_temp[4];
	content = vec_temp[5];

	if ( content.empty() )
		return false;

	string::size_type pos = macid.find( '_', 0 );
	if ( pos == string::npos ) {
		return false;
	}
	// 取得车号
	carid = macid.substr( pos + 1 );

	//去掉两边的大括号
	content.assign( content, 1, content.length() - 2 );

	vector< string > vk;
	splitvector( content, vk, ",", 0 );
	//解析参数保存至p_kv_map中
	split2map( vk, mp, ":" );

	return ( ! mp.empty() );
}

// 参数查询，暂时实现只有一个
void MsgClient::HandleDgetpMsg( string &line )
{
	string seq, mac_id, command, content, car_id;
	map< string, string > kvmap;
	if ( ! splitmsgheader( line, seq, mac_id, car_id, command, content, kvmap ) ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Split command error, line:%s" , line.c_str() );
		return;
	}

	string type = "";
	map< string, string >::iterator it = kvmap.find( "TYPE" );
	if ( it == kvmap.end() ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Command error, line:%s" , line.c_str() );
		return;
	}

	// 设置数处理集合
	_SetData dval;

	string cache_key = "";
	type = ( string ) it->second;
	if ( type == "0" ) { // 终端参数查询
		//读取参数
		it = kvmap.find( "VALUE" );
		if(it == kvmap.end()) {
			dval.msgid = 0x8104; // 查询全部终端参数
		} else {
			dval.msgid = 0x8106; // 查询指定终端参数

			size_t i;
			uint32_t  idVal;
			vector<uint32_t> gb808Ids;
			vector<string> innerIds;

			Utils::splitStr(it->second, innerIds, '|');
			for (i = 0; i < innerIds.size(); ++i) {
				Utils::str2int(innerIds[i].c_str(), idVal, ios::hex);
				switch (idVal) {
				case 0x1: //tcp port
					gb808Ids.push_back(htonl(0x0018));
					break;
				case 0x2: //IP
					gb808Ids.push_back(htonl(0x0013));
					break;
				case 0x3: //APN
					gb808Ids.push_back(htonl(0x0010));
					break;
				case 0x4: //APN username
					gb808Ids.push_back(htonl(0x0011));
					break;
				case 0x5: //APN pwd
					gb808Ids.push_back(htonl(0x0012));
					break;
				case 0x7: //心跳间隔
					gb808Ids.push_back(htonl(0x0001));
					break;
				case 0x10:  //求助号码
					gb808Ids.push_back(htonl(0x0040));
					break;
				case 0x9:  //监听号码
					gb808Ids.push_back(htonl(0x0048));
					break;
				case 0x15:  //中心短信号码
					gb808Ids.push_back(htonl(0x0043));
					break;
				case 0x31:
					gb808Ids.push_back(htonl(0x0031));
					break;
				case 0x41: // 设置车牌号
					gb808Ids.push_back(htonl(0x0083));
					break;
				case 0x42:  // 设置车牌颜色
					gb808Ids.push_back(htonl(0x0084));
					break;
				case 0x100:  //TCP消息应答超时时间
					gb808Ids.push_back(htonl(0x0002));
					break;
				case 0x101: // TCP重传次数
					gb808Ids.push_back(htonl(0x0003));
					break;
				case 0x102: //UDP
					gb808Ids.push_back(htonl(0x0004));
					break;
				case 0x103: // UDP重传次数
					gb808Ids.push_back(htonl(0x0005));
					break;
				case 0x104: // SMS应答超时时间
					gb808Ids.push_back(htonl(0x0006));
					break;
				case 0x105: // SMS重传次数
					gb808Ids.push_back(htonl(0x0007));
					break;
				case 0x106: //备份APN
					gb808Ids.push_back(htonl(0x0014));
					break;
				case 0x107:
					gb808Ids.push_back(htonl(0x0015));
					break;
				case 0x108:
					gb808Ids.push_back(htonl(0x0016));
					break;
				case 0x109: //备份服务器IP
					gb808Ids.push_back(htonl(0x0017));
					break;
				case 0x110: //服务器UDP端口
					gb808Ids.push_back(htonl(0x0019));
					break;
				case 0x111: //汇报策略
					gb808Ids.push_back(htonl(0x0020));
					break;
				case 0x112: // 位置汇报
					gb808Ids.push_back(htonl(0x0021));
					break;
				case 0x113:
					gb808Ids.push_back(htonl(0x0022));
					break;
				case 0x114: //休眠时位置汇报时间间隔
					gb808Ids.push_back(htonl(0x0027));
					break;
				case 0x115: // 紧急报警时汇报时间间隔
					gb808Ids.push_back(htonl(0x0028));
					break;
				case 0x116: //
					gb808Ids.push_back(htonl(0x0029));
					break;
				case 0x117: //缺省距离汇报间隔，单位为米（m），>0
					gb808Ids.push_back(htonl(0x002c));
					break;
				case 0x118:
					gb808Ids.push_back(htonl(0x002d));
					break;
				case 0x119:
					gb808Ids.push_back(htonl(0x002e));
					break;
				case 0x120:
					gb808Ids.push_back(htonl(0x002f));
					break;
				case 0x121:
					gb808Ids.push_back(htonl(0x0030));
					break;
				case 0x122: //复位电话号码，可采用此电话号码拨打终端电话让终端复位
					gb808Ids.push_back(htonl(0x0041));
					break;
				case 0x123: //恢复出厂设置电话号码，可采用此电话号码拨打终端电话让终端恢复出厂设置
					gb808Ids.push_back(htonl(0x0042));
					break;
				case 0x124: // 接收终端SMS文本报警号码
					gb808Ids.push_back(htonl(0x0044));
					break;
				case 0x125: // 终端电话接听策略
					gb808Ids.push_back(htonl(0x0045));
					break;
				case 0x126: // 每次最长通话时间
					gb808Ids.push_back(htonl(0x0046));
					break;
				case 0x127: // 当月最长通话时间
					gb808Ids.push_back(htonl(0x0047));
					break;
				case 0x128: //最高时速
					gb808Ids.push_back(htonl(0x0055));
					break;
				case 0x129:
					gb808Ids.push_back(htonl(0x0056));
					break;
				case 0x130:
					gb808Ids.push_back(htonl(0x0057));
					break;
				case 0x131: //当天累计驾驶时间门限，单位为秒（s）
					gb808Ids.push_back(htonl(0x0058));
					break;
				case 0x132:
					gb808Ids.push_back(htonl(0x0059));
					break;
				case 0x133:
					gb808Ids.push_back(htonl(0x005a));
					break;
				case 0x134: //车辆所在省域ID
					gb808Ids.push_back(htonl(0x0081));
					break;
				case 0x135:
					gb808Ids.push_back(htonl(0x0082));
					break;
				case 0x136: //图像/视频质量-1～10，1最好;
					gb808Ids.push_back(htonl(0x0070));
					break;
				case 0x137:
					gb808Ids.push_back(htonl(0x0071));
					break;
				case 0x138:
					gb808Ids.push_back(htonl(0x0072));
					break;
				case 0x139:
					gb808Ids.push_back(htonl(0x0073));
					break;
				case 0x140:
					gb808Ids.push_back(htonl(0x0074));
					break;
				case 0x141: //监管平台特权短信号码
					gb808Ids.push_back(htonl(0x0049));
					break;
				case 0x142: //报警屏蔽字
					gb808Ids.push_back(htonl(0x0050));
					break;
				case 0x143: //报警发送文本开关SMS开关
					gb808Ids.push_back(htonl(0x0051));
					break;
				case 0x144: //报警拍摄开关
					gb808Ids.push_back(htonl(0x0052));
					break;
				case 0x145: //报警拍摄存储标志
					gb808Ids.push_back(htonl(0x0053));
					break;
				case 0x146: //关键报警标志
					gb808Ids.push_back(htonl(0x0054));
					break;
				case 0x147: //车辆里程表读数
					gb808Ids.push_back(htonl(0x0080));
					break;
				case 0x180: //定时拍照
					gb808Ids.push_back(htonl(0x0064));
					break;
				case 0x181: //定距拍照
					gb808Ids.push_back(htonl(0x0065));
					break;
				case 0x187:
					gb808Ids.push_back(htonl(0xf108));
					break;
				case 0x190:
					gb808Ids.push_back(htonl(0xf109));
					break;
				case 0x200: //事故疑点数据上报模式
					gb808Ids.push_back(htonl(0xf100));
					break;
				case 0x201: //急加速报警阀值
					gb808Ids.push_back(htonl(0xf00c));
					break;
				case 0x202: //急减速报警阀值
					gb808Ids.push_back(htonl(0xf00d));
					break;
				case 0x203: //车辆型号
					gb808Ids.push_back(htonl(0xf10c));
					break;
				case 0x204: //VIN码
					gb808Ids.push_back(htonl(0xf10d));
					break;
				case 0x205: //发动机号
					gb808Ids.push_back(htonl(0xf10e));
					break;
				case 0x206: //发动机型号
					gb808Ids.push_back(htonl(0xf10f));
					break;
				case 0x207: //空档滑行速度阀值
					gb808Ids.push_back(htonl(0xf000));
					break;
				case 0x208: //空档滑行时间阀值
					gb808Ids.push_back(htonl(0xf001));
					break;
				case 0x209: //空挡滑行转速阀值
					gb808Ids.push_back(htonl(0xf002));
					break;
				case 0x210: //发动机超转阀值
					gb808Ids.push_back(htonl(0xf003));
					break;
				case 0x211: //发动机超转持续时间阀值
					gb808Ids.push_back(htonl(0xf004));
					break;
				case 0x212: //超长怠速的时间阀值
					gb808Ids.push_back(htonl(0xf005));
					break;
				case 0x213: //怠速的定义阀值
					gb808Ids.push_back(htonl(0xf006));
					break;
				case 0x214: //怠速空调时间阀值
					gb808Ids.push_back(htonl(0xf007));
					break;
				case 0x216: //经济区转速上限
					gb808Ids.push_back(htonl(0xf00a));
					break;
				case 0x217: //经济区转速下限
					gb808Ids.push_back(htonl(0xf00b));
					break;
				case 0x218: //专属应用屏蔽字
					gb808Ids.push_back(htonl(0xf010));
					break;
				case 0x219: //特征系数自动修正开关
					gb808Ids.push_back(htonl(0xf011));
					break;
				case 0x300:  //超速报警预警差值 WORD
					gb808Ids.push_back(htonl(0x005b));
					break;
				case 0x301: //疲劳驾驶预警差值
					gb808Ids.push_back(htonl(0x005c));
					break;
				case 0x302: //特征系数
					gb808Ids.push_back(htonl(0xf101));
					break;
				case 0x303: //车轮每转脉冲数
					gb808Ids.push_back(htonl(0xf102));
					break;
				case 0x304: //油箱容量，单位为L
					gb808Ids.push_back(htonl(0xf104));
					break;
				case 0x305: //位置信息汇报附加信息设置
					gb808Ids.push_back(htonl(0xf105));
					break;
				case 0x306: //门开关拍照控制
					gb808Ids.push_back(htonl(0xf106));
					break;
				case 0x307: //终端外围传感配置
					gb808Ids.push_back(htonl(0xf107));
					break;
				case 0x309: //分辨率
					gb808Ids.push_back(htonl(0xf10a));
					break;
				case 0x310: //车牌分类
					gb808Ids.push_back(htonl(0xf10b));
					break;
				case 0x1001a: //道路运输证IC卡认证主服务器IP地址或域名
					gb808Ids.push_back(htonl(0x001a));
					break;
				case 0x1001b: //道路运输证IC卡认证主服务器TCP端口
					gb808Ids.push_back(htonl(0x001b));
					break;
				case 0x1001c: //道路运输证IC卡认证主服务器UDP端口
					gb808Ids.push_back(htonl(0x001c));
					break;
				case 0x1001d: //道路运输证IC卡认证备份服务器IP地址或域名
					gb808Ids.push_back(htonl(0x001d));
					break;
				case 0x1005d: //碰撞报警参数设置
					gb808Ids.push_back(htonl(0x005d));
					break;
				case 0x1005e: //侧翻报警参数设置
					gb808Ids.push_back(htonl(0x005e));
					break;
				case 0x10090: //GNSS 定位模式
					gb808Ids.push_back(htonl(0x0090));
					break;
				case 0x10091: //GNSS 波特率
					gb808Ids.push_back(htonl(0x0091));
					break;
				case 0x10092: //GNSS 模块详细定位数据输出频率
					gb808Ids.push_back(htonl(0x0092));
					break;
				case 0x10093: //GNSS 模块详细定位数据采集频率
					gb808Ids.push_back(htonl(0x0093));
					break;
				case 0x10094: //GNSS 模块详细定位数据上传方式
					gb808Ids.push_back(htonl(0x0094));
					break;
				case 0x10095: //GNSS 模块详细定位数据上传设置
					gb808Ids.push_back(htonl(0x0095));
					break;
				case 0x10100: //CAN 总线通道1 采集时间间隔
					gb808Ids.push_back(htonl(0x0100));
					break;
				case 0x10101: //CAN 总线通道1 上传时间间隔
					gb808Ids.push_back(htonl(0x0101));
					break;
				case 0x10102: //CAN 总线通道2 采集时间间隔
					gb808Ids.push_back(htonl(0x0102));
					break;
				case 0x10103: //CAN 总线通道2 上传时间间隔
					gb808Ids.push_back(htonl(0x0103));
					break;
				case 0x10110: //CAN 总线ID 单独采集设置
					gb808Ids.push_back(htonl(0x0110));
					break;
				case 0x1f008: //蓄电池电压报警上限阀值
					gb808Ids.push_back(htonl(0xf008));
					break;
				case 0x1f009: //蓄电池电压报警下限阀值
					gb808Ids.push_back(htonl(0xf009));
					break;
				case 0x1f012: //扩展报警屏蔽字
					gb808Ids.push_back(htonl(0xf012));
					break;
				case 0x1f103: //脉冲系数
					gb808Ids.push_back(htonl(0xf103));
					break;
				}
			}

			dval.buffer.writeInt8(gb808Ids.size());
			dval.buffer.writeBlock(&gb808Ids[0], gb808Ids.size() * sizeof(uint32_t));
		}
	} else if (type == "1") {
		// 查询终端属性
		dval.msgid = 0x8107;
	} else if (type == "2") {
		// 查询驾驶员身份信息
		dval.msgid = 0x8702;
	}

	if ( dval.msgid > 0 ) {
		// 发送数据处理
		SendMsgData( dval, car_id, mac_id, command, seq );
	}
}

//	 * 0x0001:车机通用应答-->0x8103终端参数设置
//	 *                      0x8105终端控制
//	 *                      0x8202临时位置跟踪控制
//	 *                      0x8300文本信息下发
//	 *                      0x8301事件设置
//	 *                      0x8302提问下发
//	 *                      0x8303：信息点播菜单设置
//	 *                      0x8304：信息服务
//	 *                      0x8400：电话回拨
//	 *                      0x8401：设置电话本
//	 *                      0x8600：设置圆形区域
//	 *                      0x8601：删除圆形区域
//	 *                      0x8602：设置矩形区域
//	 *                      0x8603：删除矩形区域
//	 *                      0x8604：设置多边形区域
//	 *                      0x8605：删除多边形区域
//	 *                      0x8606：设置路线
//	 *                      0x8607：删除路线
//	 *                      0x8701：形式记录参数下传
//	 *                      0x8801：摄像头立即拍摄命令
//	 *                      0x8803: 存储多媒体数据上传指令
//	 *                      0x8804：录音开始命令
//	 *                      0x8900：数据下行透传
//	 *0x0104：查询终端参数应答-->0x8104
//	 *0x0201：位置信息查询应答-->0x8201
//	 *0x0500：车辆控制应答          -->0x8500
//	 *0x0700：行驶记录仪数据上传-->0x8700
//	 *0x0802：存储多媒体数据检索应答-->0x8802
//	 *0x0A00: 终端RSA公钥-->0x8A00平台RSA公钥
void MsgClient::HandleDsetpMsg( string &line )
{
	string seq, mac_id, command, content, car_id;
	map< string, string > p_kv_map;
	if ( ! splitmsgheader( line, seq, mac_id, car_id, command, content, p_kv_map ) ) {
		OUT_ERROR( NULL,0,"MsgClient", "Split command error, line:%s" , line.c_str() );
		return;
	}

	string type = "";
	map< string, string >::iterator p = p_kv_map.find( "TYPE" );
	if ( p == p_kv_map.end() ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Command error, line:%s" , line.c_str() );
		return;
	}

	// 设置数处理集合
	_SetData dval;

	string cache_key = "";
	type = ( string ) p->second;
	if ( type == "0" ) {
		DataBuffer buf;
		unsigned char pnum = 0;
		//设置参数
		if ( ! _gb_proto_handler->buildParamSet( & buf, p_kv_map, pnum ) ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Param set empty, line: %s" , line.c_str() );
			return;
		}

		// 长度需要加上一个参数个数的处理
		dval.msgid = 0x8103;
		dval.buffer.writeInt8( pnum );
		dval.buffer.writeBlock( ( void* ) buf.getBuffer(), buf.getLength() );
	} else if ( type == "9" ) {
		dval.msgid = 0x8900;

		p = p_kv_map.find( "91" );
		if ( p == p_kv_map.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient","DSET 0x8900 error not found 91 param: %s",line.c_str() );
			return;
		}

		string cmd = p->second;
		if ( cmd.empty() ) {
			OUT_ERROR( NULL, 0,"MsgClient","DSET 0x8900 cmd param error: %s", line.c_str() );
			return;
		}

		p = p_kv_map.find( "90" );
		if ( p == p_kv_map.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient","DSET 0x8900 not found 90 error: %s", line.c_str() );
			return;
		}

		// 透明传输
		string value = p->second;
		dval.buffer.writeInt8( ( unsigned char ) atoi( cmd.c_str() ) );
		// 转为透传数据
		CBase64 base64;
		if ( ! base64.Decode( value.c_str(), value.length() ) ) {
			OUT_ERROR( NULL, 0,"MsgClient","Base64 base decode %s failed" , value.c_str() );
			return;
		}
		// 处理数据透传下行
		dval.buffer.writeBlock( base64.GetBuffer(), base64.GetLength() );
	} else if ( type == "10" ) {
		p = p_kv_map.find( "95" );
		if ( p == p_kv_map.end() ) {
			OUT_ERROR( NULL, 0, "MsgClient","custom command not found 92 param: %s",line.c_str() );
			return;
		}

		string cmd = p->second;
		if ( cmd.empty() ) {
			OUT_ERROR( NULL, 0, "MsgClient","custom command not found 92 param: %s", line.c_str() );
			return;
		}
		dval.msgid = atoi(cmd.c_str());

		p = p_kv_map.find( "96" );
		if ( p == p_kv_map.end() ) {
			OUT_ERROR( NULL, 0, "MsgClient","custom command not found 93 param: %s", line.c_str() );
			return;
		}

		// 透明传输
		string value = p->second;
		CBase64 base64;
		if ( ! base64.Decode( value.c_str(), value.length() ) ) {
			OUT_ERROR( NULL, 0,"MsgClient","Base64 base decode %s failed" , value.c_str() );
			return;
		}

		seq = "";
		mac_id = "";
		command = "";

		// 处理数据透传下行
		dval.buffer.writeBlock( base64.GetBuffer(), base64.GetLength() );
	} else if ( type == "11" ) // 事件设置
			{
		p = p_kv_map.find( "160" ); // 事件设置
		if ( p == p_kv_map.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Command param, line:%s" , line.c_str() );
			return;
		}
		/**
		 * 设置类型（取值范围：0|1|2|3|4）：
		 0：删除终端现有所有事件，如果160值为0，则无161数据
		 1：更新事件
		 2：追加事件
		 3：修改事件
		 4：删除特定几项事件，161值中无需带事项内容
		 */

		/**
		 事件设置格式：{TYPE:11,161:[事件1][事件2][事件3][事件N]}
		 事件项描述结构：
		 [
		 1:事件ID,
		 2:事件内容(BASE64(GBK))
		 ]*/

		vector< string > vec;
		// 操作命令
		int cmd = atoi( p->second.c_str() );
		if ( cmd > 0 ) {
			if ( ! parsemultidata( content, "161:", vec, '[', ']' ) ) {
				OUT_ERROR( NULL, 0,"MsgClient", "Parse data error, line:%s" , line.c_str() );
				return;
			}
		}

		switch ( cmd )
		{
		case 0:
		{
			dval.msgid = 0x8301;
			dval.buffer.writeInt8( 0 );
		}
			break;
		case 1:
		case 2:
		case 3:
		case 4:
		{
			dval.msgid = 0x8301;

			EventHeader eheader;
			eheader.type = cmd;

			int count = 0;
			dval.buffer.writeFill( 0, ( int ) sizeof(EventHeader) );

			map< string, string > smp;
			map< string, string >::iterator it;
			for ( size_t i = 0 ; i < vec.size() ; ++ i ) {
				if ( vec[i].empty() ) {
					continue;
				}
				// 拆分字项
				vector< string > temp;
				splitvector( vec[i], temp, ",", 0 );
				if ( split2map( temp, smp, ":" ) < 0 ) {
					continue;
				}

				EventContentBody ebody;
				it = smp.find( "1" );  // 1:事件ID,
				if ( it == smp.end() ) {
					continue;
				}
				ebody.eventid = atoi( it->second.c_str() );
				if ( cmd == 4 ) {
					ebody.length = 0x00;
					dval.buffer.writeBlock( ( void* ) & ebody, ( int ) sizeof ( ebody ) );
					//dval.buffer.writeInt8( ebody.eventid ) ;  // 删除特定的项时只需要添加事件ID
				} else {
					it = smp.find( "2" );  // 2:事件内容(BASE64(GBK))
					if ( it == smp.end() ) {
						continue;
					}

					CBase64 coder;
					// 如果BASE64解码失败则不处理
					if ( ! coder.Decode( it->second.c_str(), it->second.length() ) ) {
						continue;
					}
					ebody.length = coder.GetLength();
					dval.buffer.writeBlock( ( void* ) & ebody, sizeof ( ebody ) );
					dval.buffer.writeBlock( ( void* ) coder.GetBuffer(), ( int ) coder.GetLength() );
				}
				++ count;
			}
			// 如果有数据则处理
			if ( count > 0 ) {
				eheader.total = count;
				// 重新设置头部数据
				dval.buffer.fillBlock( ( void* ) & eheader, sizeof ( eheader ), 0 );
			}
		}
			break;
		}
	} else if ( type == "12" )  // 信息点播菜单设置
			{
		p = p_kv_map.find( "165" ); // 事件设置
		if ( p == p_kv_map.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Command param, line:%s" , line.c_str() );
			return;
		}
		/**
		 * 设置类型（取值范围：0|1|2|3）：
		 0：表示删除终端全部菜单项
		 1：更新菜单
		 2：追加菜单
		 3：修改菜单
		 */

		/**
		 菜单设置格式：{TYPE:11,166:[菜单1][菜单2][菜单3][菜单N]}
		 菜单项描述结构：
		 [
		 1:菜单项类型ID（相同类型ID的菜单项将会被覆盖），
		 2:菜单项名称（ BASE64(GBK编码) ）
		 ]*/

		vector< string > vec;
		// 操作命令
		int cmd = atoi( p->second.c_str() );
		if ( cmd > 0 ) {
			if ( ! parsemultidata( content, "166:", vec, '[', ']' ) ) {
				OUT_ERROR( NULL, 0,"MsgClient", "Parse data error, line:%s" , line.c_str() );
				return;
			}
		}

		switch ( cmd )
		{
		case 0:
		{
			dval.msgid = 0x8303;
			dval.buffer.writeInt8( 0 );
		}
			break;
		case 1:
		case 2:
		case 3:
		{
			dval.msgid = 0x8303;

			DemandHeader eheader;
			eheader.type = cmd;

			int count = 0;
			dval.buffer.writeFill( 0, ( int ) sizeof(DemandHeader) );

			map< string, string > smp;
			map< string, string >::iterator it;
			for ( size_t i = 0 ; i < vec.size() ; ++ i ) {
				if ( vec[i].empty() ) {
					continue;
				}
				// 拆分字项
				vector< string > temp;
				splitvector( vec[i], temp, ",", 0 );
				if ( split2map( temp, smp, ":" ) < 0 ) {
					continue;
				}

				DemandContentBody ebody;
				it = smp.find( "1" );  // 1:菜单项类型ID,
				if ( it == smp.end() ) {
					continue;
				}
				ebody.mtype = atoi( it->second.c_str() );

				it = smp.find( "2" );  // 2:菜单项名称
				if ( it == smp.end() ) {
					continue;
				}

				CBase64 coder;
				// 如果BASE64解码失败则不处理
				if ( ! coder.Decode( it->second.c_str(), it->second.length() ) ) {
					continue;
				}
				ebody.length = htons( coder.GetLength() );
				dval.buffer.writeBlock( ( void* ) & ebody, ( int ) sizeof ( ebody ) );
				dval.buffer.writeBlock( ( void* ) coder.GetBuffer(), coder.GetLength() );

				++ count;
			}
			// 如果有数据则处理
			if ( count > 0 ) {
				eheader.total = count;
				dval.buffer.fillBlock( ( void* ) & eheader, sizeof ( eheader ), 0 );
			}
		}
			break;
		}
	} else if ( type == "13" ) // 电话本设置
			{
		p = p_kv_map.find( "170" ); // 事件设置
		if ( p == p_kv_map.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Command param, line:%s" , line.c_str() );
			return;
		}
		/**
		 * 设置类型（取值范围：0|1|2|3）：
		 0：删除终端上所有的联系人
		 1：更新电话本（删除终端所有联系人，并追加消息中的联系人）
		 2：追加电话本
		 3：修改电话本
		 */

		/**
		 联系人设置格式：{TYPE:13,171:[电话本联系人1][电话本联系人2][电话本联系人3][电话本联系人N]}
		 事件项描述结构：
		 [
		 1:呼入呼出设置（1呼入，2呼出，3呼入呼出）
		 2:电话号码
		 3:联系人（ BASE64(GBK编码) ）
		 ]*/

		vector< string > vec;
		// 操作命令
		int cmd = atoi( p->second.c_str() );
		if ( cmd > 0 ) {
			if ( ! parsemultidata( content, "171:", vec, '[', ']' ) ) {
				OUT_ERROR( NULL, 0,"MsgClient", "Parse data error, line:%s" , line.c_str() );
				return;
			}
		}

		switch ( cmd )
		{
		case 0:
		{
			dval.msgid = 0x8401;
			dval.buffer.writeInt8( 0 );
		}
			break;
		case 1:
		case 2:
		case 3:
		{
			dval.msgid = 0x8401;

			PhoneHeader eheader;
			eheader.type = cmd;

			int count = 0;
			dval.buffer.writeFill( 0, sizeof(PhoneHeader) );

			map< string, string > smp;
			map< string, string >::iterator it;
			for ( size_t i = 0 ; i < vec.size() ; ++ i ) {
				if ( vec[i].empty() ) {
					continue;
				}
				// 拆分字项
				vector< string > temp;
				splitvector( vec[i], temp, ",", 0 );
				if ( split2map( temp, smp, ":" ) < 0 ) {
					continue;
				}

				it = smp.find( "1" );  // 1:呼入呼出设置
				if ( it == smp.end() ) {
					continue;
				}
				dval.buffer.writeInt8( ( unsigned char ) atoi( it->second.c_str() ) );

				it = smp.find( "2" ); // 2:电话号码
				if ( it == smp.end() ) {
					continue;
				}
				dval.buffer.writeInt8( ( unsigned char ) it->second.length() );
				dval.buffer.writeBlock( ( void * ) it->second.c_str(), it->second.length() );

				it = smp.find( "3" );  // 3:联系人（ BASE64(GBK编码) ）
				if ( it == smp.end() ) {
					continue;
				}

				CBase64 coder;
				// 如果BASE64解码失败则不处理
				if ( ! coder.Decode( it->second.c_str(), it->second.length() ) ) {
					continue;
				}
				dval.buffer.writeInt8( ( unsigned char ) coder.GetLength() );
				dval.buffer.writeBlock( ( void* ) coder.GetBuffer(), coder.GetLength() );

				++ count;
			}
			// 如果有数据则处理
			if ( count > 0 ) {
				eheader.total = count;
				dval.buffer.fillBlock( ( void* ) & eheader, sizeof ( eheader ), 0 );
			}
		}
			break;
		}
	} else if ( type == "14" ) { // 设置电子围栏
		p = p_kv_map.find( "150" ) ;
		if ( p == p_kv_map.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Command param, line:%s" , line.c_str() );
			return;
		}

		// 操作命令
		int cmd = atoi( p->second.c_str() );

		// 设置类型
		p = p_kv_map.find( "151" );
		if ( p == p_kv_map.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Command param type error, line:%s" , line.c_str() );
			return;
		}
		int type = atoi( p->second.c_str() );
		if ( type < 1 || type > 3 ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Set type error, type %d, line:%s" , type, line.c_str() );
			return;
		}

		// 解析存放围栏数据
		vector< string > vec;
		if ( cmd > 0 ) {
			if ( ! parsemultidata( content, "152:", vec, '[', ']' ) ) {
				OUT_ERROR( NULL, 0,"MsgClient", "Parse data error, line:%s" , line.c_str() );
				return;
			}
		}

		switch ( cmd )
		{
		case 0:  // 删除全部电子围栏
		{
			if ( type == 1 ) {  // 圆形
				dval.msgid = 0x8601;
			} else if ( type == 2 ) { // 方形
				dval.msgid = 0x8603;
			} else { // 多边形
				dval.msgid = 0x8605;
			}
			dval.buffer.writeInt8( 0 );
		}
			break;
		case 1: // 删除指定的电子围栏
		{
			vector< int > ids;
			int count = 0;

			map< string, string > smp;
			map< string, string >::iterator it;
			for ( size_t i = 0 ; i < vec.size() ; ++ i ) {
				if ( vec[i].empty() ) {
					continue;
				}
				vector< string > temp;
				splitvector( vec[i], temp, ",", 0 );
				if ( split2map( temp, smp, ":" ) > 0 ) {
					it = smp.find( "1" );
					if ( it == smp.end() ) {
						continue;
					}
					// 存放电子围栏的ID
					ids.push_back( atoi( it->second.c_str() ) );
					++ count;
				}
			}

			if ( count > 0 ) {
				if ( type == 1 ) {  // 圆形
					dval.msgid = 0x8601;
				} else if ( type == 2 ) { // 方形
					dval.msgid = 0x8603;
				} else { // 多边形
					dval.msgid = 0x8605;
				}
				dval.buffer.writeInt8( count );  // 第一个字节为个数

				// 后面为32位整ID号
				for ( int index = 0 ; index < count ; ++ index ) {
					dval.buffer.writeInt32( ids[index] );
				}
			}
		}
			break;
		case 2:  // 更新
		case 3:  // 追加
		case 4:  // 修改
		{
			int op = cmd - 2;  // 外部协议中对应为0,1,2

			if ( type == 1 ) {  // 圆形
				dval.msgid = 0x8600;
			} else if ( type == 2 ) { // 方形
				dval.msgid = 0x8602;
			} else { // 多边形
				dval.msgid = 0x8604;
			}

			AreaSetHeader ahead;
			ahead.op = op;
			ahead.total = 0;

			// 如果不为多边形
			if ( dval.msgid != 0x8604 ) {
				// 跳过前面头部数据
				dval.buffer.writeFill( 0, sizeof ( ahead ) );
			}

			int areacount = 0;

			map< string, string > smp;
			map< string, string >::iterator it;
			for ( size_t i = 0 ; i < vec.size() ; ++ i ) {
				if ( vec[i].empty() ) {
					continue;
				}
				/**
				 1:区域ID,
				 2:区域属性（32位二进制表示，如下：）,
				 3:开始时间，
				 4:结束时间，
				 5:最高速度，
				 6:超速持续时间，
				 20:多边形区域的边界（x1|y1|x2|y2|x3|y3|...|xn|yn）,
				 21:圆形区域项格式（中心点纬度|中心点经度|半径m）,
				 22:矩形区域项格式（左上点纬度|左上点经度|右下点纬度|右下点经度）
				 */
				vector< string > temp;
				splitvector( vec[i], temp, ",", 0 );
				if ( split2map( temp, smp, ":" ) <= 0 ) {
					continue;
				}

				it = smp.find( "1" );  // 1:区域ID,
				if ( it == smp.end() ) {
					continue;
				}

				int areaid = atoi( it->second.c_str() );
				DataBuffer hbuf;  // 记录区域的BUF

				AreaHeader bhead;
				// 存放电子围栏的ID
				bhead.areaid = htonl( areaid );
				it = smp.find( "2" );  // 2:区域属性（32位二进制表示，如下：）,
				if ( it == smp.end() ) {
					continue;
				}
				int attrid = atoi( it->second.c_str() );
				bhead.attr = htons( attrid );

				// 添加一个区域头数据
				hbuf.writeBlock( ( void* ) & bhead, sizeof ( bhead ) );

				DataBuffer tbuf;  // 记录附加项的BUF
				// 是否添加时间
				if ( attrid & 0x0001 ) {
					AreaTime atime;
					it = smp.find( "3" ); // 3:开始时间，
					if ( it != smp.end() ) {
						// 将UTC转为BCD时间
						convert2bcd( it->second, atime.starttime );
					}
					it = smp.find( "4" ); // 4:结束时间，
					if ( it != smp.end() ) {
						// 将UTC转为BCD时间
						convert2bcd( it->second, atime.endtime );
					}
					// 添加时间
					tbuf.writeBlock( ( void* ) & atime, sizeof ( atime ) );
				}
				// 是否添加速度
				if ( attrid & 0x0002 ) {
					AreaSpeed aspeed;
					it = smp.find( "5" ); // 5:最高速度，
					if ( it != smp.end() ) {
						aspeed.speed = htons( atoi( it->second.c_str() ) ); //htonl
					}
					it = smp.find( "6" ); // 6:超速持续时间，
					if ( it != smp.end() ) {
						aspeed.nlast = atoi( it->second.c_str() );
					}
					tbuf.writeBlock( ( void* ) & aspeed, sizeof ( aspeed ) );
				}

				switch ( type )
				{
				case 1:  // 圆形
				{
					BoundAreaBody abody;
					it = smp.find( "21" );  //圆形区域项格式（中心点纬度|中心点经度|半径m）
					if ( it == smp.end() ) {
						continue;
					}

					vector< string > vpoint;
					splitvector( it->second, vpoint, "|", 0 );
					if ( vpoint.size() < 3 ) {
						// 如果解析不正确则说明格式不正确
						continue;
					}
					abody.local.lat = htonl( atoi( vpoint[0].c_str() ) );
					abody.local.lon = htonl( atoi( vpoint[1].c_str() ) );
					abody.raduis = htonl( atoi( vpoint[2].c_str() ) );

					hbuf.writeBlock( ( void* ) & abody, sizeof ( abody ) );

					// 如果有附加项
					if ( tbuf.getLength() > 0 ) {
						// 添加附加项
						hbuf.writeBuffer( tbuf );
					}
				}
					break;
				case 2:  // 方形
				{
					RangleAreaBody abody;
					it = smp.find( "22" );  // 22:矩形区域项格式（左上点纬度|左上点经度|右下点纬度|右下点经度）
					if ( it == smp.end() ) {
						continue;
					}
					vector< string > vpoint;
					splitvector( it->second, vpoint, "|", 0 );
					if ( vpoint.size() < 4 ) {
						continue;
					}
					abody.left_top.lat = htonl( atoi( vpoint[0].c_str() ) );
					abody.left_top.lon = htonl( atoi( vpoint[1].c_str() ) );
					abody.right_bottom.lat = htonl( atoi( vpoint[2].c_str() ) );
					abody.right_bottom.lon = htonl( atoi( vpoint[3].c_str() ) );

					hbuf.writeBlock( ( void* ) & abody, sizeof ( abody ) );

					// 如果存在附加项则添加附加项
					if ( tbuf.getLength() > 0 ) {
						hbuf.writeBuffer( tbuf );
					}
				}
					break;
				case 3:  // 多边形
				{
					it = smp.find( "20" );  // 20:多边形区域的边界（x1|y1|x2|y2|x3|y3|...|xn|yn）,
					if ( it == smp.end() ) {
						continue;
					}

					vector< string > vpoint;
					splitvector( it->second, vpoint, "|", 0 );
					if ( vpoint.size() % 2 != 0 ) {
						continue;
					}

					DataBuffer vbuf;

					int vcount = 0;
					for ( size_t vpos = 0 ; vpos < vpoint.size() ; vpos = vpos + 2 ) {
						LatLonPoint pt;
						pt.lat = htonl( atoi( vpoint[vpos].c_str() ) );
						pt.lon = htonl( atoi( vpoint[vpos + 1].c_str() ) );

						// 处理多个顶点值
						vbuf.writeBlock( ( void* ) & pt, sizeof ( pt ) );

						++ vcount;
					}
					// 如果存在附加项则添加
					if ( tbuf.getLength() > 0 ) {
						hbuf.writeBuffer( tbuf );
					}

					// 记录顶点个数
					hbuf.writeInt16( vcount );

					// 拷贝顶点值
					hbuf.writeBuffer( vbuf );
				}
					break;
				}

				// 拷贝一个围栏数据
				dval.buffer.writeBuffer( hbuf );

				++ areacount;
			}

			// 如果不为多边形处理
			if ( dval.msgid != 0x8604 ) {
				ahead.total = areacount;
				dval.buffer.fillBlock( & ahead, sizeof ( ahead ), 0 );
			}
		}
			break;
		default:
			OUT_ERROR( NULL, 0,"MsgClient", "Command operator error, line:%s" , line.c_str() );
			break;
		}
	} else if ( type == "15" ) {  // 设置路线

		p = p_kv_map.find( "155" );
		if ( p == p_kv_map.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Command param, line:%s" , line.c_str() );
			return;
		}

		// 操作命令
		int cmd = atoi( p->second.c_str() );

		// 解析线路数据
		vector< string > vec;
		if ( cmd > 0 ) {
			// 解析线程数据
			if ( ! parsemultidata( content, "156:", vec, '[', ']' ) ) {
				OUT_ERROR( NULL, 0,"MsgClient", "Parse data error, line:%s" , line.c_str() );
				return;
			}
		}

		switch ( cmd )
		{
		case 0:  // 删除全部线路
		{
			dval.msgid = 0x8607;
			dval.buffer.writeInt8( 0 );
		}
			break;
		case 1: // 删除指定的线路
		{
			vector< int > ids;
			int count = 0;

			map< string, string > smp;
			map< string, string >::iterator it;
			for ( size_t i = 0 ; i < vec.size() ; ++ i ) {
				if ( vec[i].empty() ) {
					continue;
				}
				vector< string > temp;
				splitvector( vec[i], temp, ",", 0 );
				if ( split2map( temp, smp, ":" ) > 0 ) {
					it = smp.find( "1" );
					if ( it == smp.end() ) {
						continue;
					}
					// 存放电子围栏的ID
					ids.push_back( atoi( it->second.c_str() ) );
					++ count;
				}
			}

			if ( count > 0 ) {
				dval.msgid = 0x8607;
				dval.buffer.writeInt8( count ); // 第一个字节为个数

				// 后面为32位整ID号
				for ( int index = 0 ; index < count ; ++ index ) {
					dval.buffer.writeInt32( ids[index] );
				}
			}
		}
			break;
		default: // 设置线路的处理
		{
			map< string, string > smp;
			map< string, string >::iterator it;
			for ( size_t i = 0 ; i < vec.size() ; ++ i ) {
				if ( vec[i].empty() ) {
					continue;
				}
				/**
				 1:路线ID，
				 2:路线属性，
				 3:开始时间，
				 4:结束时间，
				 5:路段描述结构，
				 （1=拐点ID|2=路段ID|3=拐点纬度|4=拐点精度|5=路段宽度|6=路段属性|7=路段行驶过长阀值|8=路段行驶过短阀值|
				 9=路段最高速度|10=路段超速持续时间）（路段2）（路段3）（路段4）
				 */
				vector< string > temp;
				splitvector( vec[i], temp, ",", 0 );
				if ( split2map( temp, smp, ":" ) < 0 ) {
					continue;
				}

				it = smp.find( "1" );  // 1:路线ID，
				if ( it == smp.end() ) {
					continue;
				}

				_SetData dvalue;
				RoadHeader rheader;
				rheader.roadid = htonl( atoi( it->second.c_str() ) );

				it = smp.find( "2" );  // 2:路线属性，
				if ( it == smp.end() ) {
					continue;
				}
				int attrid = atoi( it->second.c_str() );
				rheader.attr = htons( attrid );
				dvalue.buffer.writeBlock( ( void* ) & rheader, sizeof ( rheader ) );

				if ( attrid & 0x0001 ) {
					AreaTime rtime;
					it = smp.find( "3" );  // 3:开始时间，
					if ( it != smp.end() ) {
						// 将UTC转为BCD时间
						convert2bcd( it->second, rtime.starttime );
					}
					it = smp.find( "4" );  // 4:结束时间，
					if ( it != smp.end() ) {
						// 将UTC转为BCD时间
						convert2bcd( it->second, rtime.endtime );
					}
					dvalue.buffer.writeBlock( ( void* ) & rtime, sizeof ( rtime ) );
				}

				/**
				 5:路段描述结构，
				 （1=拐点ID|2=路段ID|3=拐点纬度|4=拐点精度|5=路段宽度|6=路段属性|7=路段行驶过长阀值|8=路段行驶过短阀值|
				 9=路段最高速度|10=路段超速持续时间）（路段2）（路段3）（路段4）
				 */
				unsigned short rcount = 0;
				vector< string > vecbend;
				if ( ! parsemultidata( vec[i], "5:", vecbend, '(', ')' ) ) {
					// 解析拐点数据
					continue;
				}

				DataBuffer tbuf;
				map< string, string > bmap;
				for ( size_t index = 0 ; index < vecbend.size() ; ++ index ) {
					// 解析数据
					vector< string > vecpt;
					splitvector( vecbend[index], vecpt, "|", 0 );
					// 解析对应值
					if ( split2map( vecpt, bmap, "=" ) <= 0 ) {
						continue;
					}

					BendPoint rbendpt;
					it = bmap.find( "1" );  // 拐点ID
					if ( it == bmap.end() ) {
						continue;
					}
					rbendpt.bendid = htonl( atoi( it->second.c_str() ) );

					it = bmap.find( "2" );
					if ( it == bmap.end() ) {  // 路段ID
						continue;
					}
					rbendpt.segid = htonl( atoi( it->second.c_str() ) );

					it = bmap.find( "3" );
					if ( it == bmap.end() ) {  // 拐点纬度
						continue;
					}
					rbendpt.postion.lat = htonl( atoi( it->second.c_str() ) );

					it = bmap.find( "4" );
					if ( it == bmap.end() ) {  // 拐点经度
						continue;
					}
					rbendpt.postion.lon = htonl( atoi( it->second.c_str() ) );

					it = bmap.find( "5" );
					if ( it == bmap.end() ) {  // 路段宽度
						continue;
					}
					rbendpt.width = atoi( it->second.c_str() );

					it = bmap.find( "6" );
					if ( it == bmap.end() ) {  // 路段属性
						continue;
					}
					int rbendattr = atoi( it->second.c_str() );
					rbendpt.battr = rbendattr;

					tbuf.writeBlock( ( void* ) & rbendpt, sizeof ( rbendpt ) );

					// 如果有阈值限制则处理
					if ( rbendattr & 0x0001 ) {
						Threshold thold;
						it = bmap.find( "7" );  // 7=路段行驶过长阀值
						if ( it != bmap.end() ) {
							thold.more = htons( atoi( it->second.c_str() ) );
						}
						it = bmap.find( "8" );  // 8=路段行驶过短阀值
						if ( it != bmap.end() ) {
							thold.less = htons( atoi( it->second.c_str() ) );
						}
						tbuf.writeBlock( ( void* ) & thold, sizeof ( thold ) );
					}

					//  如果速度处理则处理
					if ( rbendattr & 0x0002 ) {
						AreaSpeed rspeed;
						it = bmap.find( "9" );  // 9=路段最高速度
						if ( it != bmap.end() ) {
							rspeed.speed = htons( atoi( it->second.c_str() ) );
						}
						it = bmap.find( "10" ); // 10=路段超速持续时间
						if ( it != bmap.end() ) {
							rspeed.nlast = atoi( it->second.c_str() );
						}
						tbuf.writeBlock( ( void* ) & rspeed, sizeof ( rspeed ) );
					}

					++ rcount;
				}

				dvalue.msgid = 0x8606;
				dvalue.buffer.writeInt16( rcount );  // 写记录个数
				dvalue.buffer.writeBuffer( tbuf );   // 记录数据

				// 发送数，这里可能存在多个数据的情况
				SendMsgData( dvalue, car_id, mac_id, command, seq );
			}
		}
			break;
		}

	} else if ( type == "30" ) {
		p = p_kv_map.find( "311" ); //历史数据类型
		if ( p != p_kv_map.end() ) {
			dval.buffer.writeInt8(atoi(p->second.c_str()));
		} else {
			dval.buffer.writeInt8(0);
		}

		char bcdtime[6];

		memset(bcdtime, 0x00, 6);
		p = p_kv_map.find("312"); //查询起始时间
		if (p != p_kv_map.end() && p->second.length() == 12) {
			strtoBCD(p->second.c_str(), bcdtime, 6);
		}
		dval.buffer.writeBlock(bcdtime, 6);

		memset(bcdtime, 0x00, 6);
		p = p_kv_map.find("313"); //查询结束时间
		if (p != p_kv_map.end() && p->second.length() == 12) {
			strtoBCD(p->second.c_str(), bcdtime, 6);
		}
		dval.buffer.writeBlock(bcdtime, 6);

		p = p_kv_map.find("314"); //查询个数
		if(p != p_kv_map.end()) {
			dval.buffer.writeInt16(atoi(p->second.c_str()));
		} else {
			dval.buffer.writeInt16(0);
		}

		dval.msgid = 0x8f12;
	}

	// 一次处理所有数据
	if ( dval.msgid > 0 ) {
		// 发送数据处理
		SendMsgData( dval, car_id, mac_id, command, seq );
	}
}
/**点名***/
void MsgClient::HandleDcallMsg( string &line )
{
	string seq, mac_id, command, content, car_id;
	map< string, string > kvmap;
	if ( ! splitmsgheader( line, seq, mac_id, car_id, command, content, kvmap ) ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Split command error, line:%s" , line.c_str() );
		return;
	}

	map< string, string >::iterator it = kvmap.find( "TYPE" );
	if ( it == kvmap.end() ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Command error, line:%s" , line.c_str() );
		return;
	}
	string type = it->second;

	string cache_key = "";
	if ( car_id.length() > 12 ) {
		OUT_ERROR( NULL, 0,"MsgClient", car_id.c_str(), "Param error, data: %s" , line.c_str() );
		//指令错误
		return;
	}

	// 处理数据
	_SetData dval;

	if ( type == "0" ) {   //定时定次
		it = kvmap.find( "0" );
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", car_id.c_str(), "Param error , data: %s" , line.c_str() );
			return;
		}
		string intervalstr = it->second;

		it = kvmap.find( "1" );
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", car_id.c_str(), "Param error ,, data: %s" , line.c_str() );
			return;
		}
		string timesstr = it->second;

		unsigned short intervalus = atoi( intervalstr.c_str() );
		unsigned short timesus = atoi( timesstr.c_str() );

		dval.msgid = 0x8202; // 临时位置跟踪控制

		PlatformTraceBody body;
		body.period = htonl( intervalus * timesus );
		body.timeval = htons( intervalus );

		// 复制数据
		dval.buffer.writeBlock( ( void* ) & body, sizeof ( body ) );
	} else if ( type == "1" ) { //定距
		it = kvmap.find( "2" );
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", car_id.c_str(), "Param error , data: %s" , line.c_str() );
			return;
		}

		string distancestr = it->second;
		if ( distancestr == "0" ) {
			dval.msgid = 0x8202; // 停止跟踪
			dval.buffer.writeInt16( 0 );
		}
	} else if ( type == "2" ) {   //位置信息查询
		dval.msgid = 0x8201;
	} else {   //参数错误
//        	char a[10];
//        	AddUpData(a,10,1);
	}
	if ( dval.msgid > 0 ) {
		// 发送数据处理
		SendMsgData( dval, car_id, mac_id, command, seq );
	}
}

void MsgClient::HandleDctlmMsg( string &line )
{
	string seq, mac_id, command, content, car_id;
	map< string, string > kvmap;
	if ( ! splitmsgheader( line, seq, mac_id, car_id, command, content, kvmap ) ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Split command error, line:%s" , line.c_str() );
		return;
	}

	map< string, string >::iterator it;

	it = kvmap.find( "TYPE" );
	if ( it == kvmap.end() ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Command error, line:%s" , line.c_str() );
		return;
	}
	string type = it->second;

	it = kvmap.find( "VALUE" );
	if ( it == kvmap.end() ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Command error, line:%s" , line.c_str() );
		return;
	}
	string value = it->second;

	_SetData dval;

	//格式正确
	unsigned short ustype = atoi( type.c_str() );
	switch ( ustype ) {
	case 4:  // 终端控制关机
	case 15: // 值: 3终端关机，4终端复位，5终端恢复出厂设置，6关闭数据通信，7关闭所有无线通信
		dval.msgid = 0x8105;  // 终端控制
		dval.buffer.writeInt8((value.empty()) ? 3 : atoi(value.c_str())); // 终端关机命令字
		break;
	case 7:  // 锁车
	case 8:  // 开锁
		dval.msgid = 0x8500;
		dval.buffer.writeInt8((ustype == 7) ? 0x01 : 0x00); // 锁车和开锁的控制
		break;
	case 9: //监听,value为电话号码;
	case 16: //通话
		dval.msgid = 0x8400;  // 电话回拨
		if (ustype == 9)  //监听
			dval.buffer.writeInt8(1);
		else
			//通话
			dval.buffer.writeInt8(0);
		dval.buffer.writeBlock(value.c_str(), value.length());
		break;
	case 10: //_TakePhoto
		{
			dval.msgid = 0x8801;  // 拍照处理

			TakePhotoBody photo;

			vector< string > vec_p;
			splitvector( value, vec_p, "|", 0 );
			photo.camera_id = vec_p.size() > 0 && vec_p[0].size() > 0 ? atoi( vec_p[0].c_str() ) : 1;
			photo.photo_num = vec_p.size() > 1 && vec_p[1].size() > 0 ? htons( atoi( vec_p[1].c_str() ) ) : htons( 1 );
			photo.time_interval = vec_p.size() > 2 && vec_p[2].size() > 0 ? htons( atoi( vec_p[2].c_str() ) ) : 0;
			photo.is_save = vec_p.size() > 3 && vec_p[3].size() > 0 ? atoi( vec_p[3].c_str() ) : 0;
			photo.sense = vec_p.size() > 4 && vec_p[4].size() > 0 ? (atoi( vec_p[4].c_str() ) + 1) : 2;
			photo.photo_quality = vec_p.size() > 5 && vec_p[5].size() > 0 ? atoi( vec_p[5].c_str() ) : 5;
			photo.liangdu = vec_p.size() > 6 && vec_p[6].size() > 0 ? atoi( vec_p[6].c_str() ) : 128;
			photo.duibidu = vec_p.size() > 7 && vec_p[7].size() > 0 ? atoi( vec_p[7].c_str() ) : 64;
			photo.baohedu = vec_p.size() > 8 && vec_p[8].size() > 0 ? atoi( vec_p[8].c_str() ) : 64;
			photo.sedu = vec_p.size() > 9 && vec_p[9].size() > 0 ? atoi( vec_p[9].c_str() ) : 128;

			dval.buffer.writeBlock( & photo, sizeof ( photo ) );

			// ToDo: 处理宇通扩展拍照下发带过来的序号
			it = kvmap.find( "191" ) ;
			if ( it != kvmap.end() ) {
				dval.buffer.writeInt32( atoi( it->second.c_str() ) ) ;
			}
		}
		break;
	case 11:  //录音开始命令
		{
			dval.msgid = 0x8804;   // 录音开始

			VoiceRecordBody body;

			vector< string > vec;
			splitvector( value, vec, "|", 0 );
			body.command = atoi( vec[0].c_str() );
			body.recordtime = ( vec.size() > 1 ) ? htons( atoi( vec[1].c_str() ) ) : htons( 0 );
			body.saveflag = ( vec.size() > 2 ) ? atoi( vec[2].c_str() ) : 0;
			body.samplerates = ( vec.size() > 3 ) ? atoi( vec[3].c_str() ) : 0;

			dval.buffer.writeBlock( & body, sizeof ( body ) );
		}
		break;
	case 20:  // 升级处理
		{
			// 完整URL地址；APN名称；APN用户名；APN密码；服务器地址；服务器TCP端口；服务器UDP端口；制造商ID；硬件版本；固件版本；连接到指定服务器时限
			dval.msgid = 0x8105;  // 无线升级处理
			dval.buffer.writeInt8( 1 );

			vector< string > vec;
			splitvector( value, vec, ";", 0 );
			if ( vec.size() < 11 ) {
				OUT_ERROR( NULL, 0, "MsgClient","CTML split value %s failed" , value.c_str() );
				return;
			}

			// 处理升级参数
			for ( int i = 0 ; i < ( int ) vec.size() ; ++ i ) {
				string &temp = vec[i];
				if ( i == 5 || i == 6 || i == 10 ) { // TCP端口和UDP端口, 连接到时间 WORD
					int port = 0;
					if (!temp.empty()) {
						port = atoi(temp.c_str());
					}
					dval.buffer.writeInt16(port);
				} else if ( i == 7 ) {  // 制造商BYTE[5]
					if ( ! temp.empty() ) {
						dval.buffer.writeBytes( ( void * ) temp.c_str(), temp.length(), 5 );
					} else {
						dval.buffer.writeFill( 0, 5 );
					}
				} else {  // 字符串类型
					if ( ! temp.empty() ) {
						dval.buffer.writeBlock( temp.c_str(), temp.length() );
					}
				}
				if ( i != 10 ) {
					dval.buffer.writeInt8( ';' );
				}
			}
		}
		break;
	case 21:  // 连接控制
		{
			// 连接控制;监管平台鉴权码;拨号点名称;拨号用户名;拨号密码;地址;TCP端口;UDP端口;连接到服务器指定的时限
			dval.msgid = 0x8105; // 连接控制应急接入
			dval.buffer.writeInt8( 2 );

			vector< string > vec;
			splitvector( value, vec, ";", 0 );
			if ( vec.size() < 9 ) {
				OUT_ERROR( NULL, 0, "MsgClient", "CTML split value %s failed", value.c_str() );
				return;
			}

			for ( int i = 0 ; i < ( int ) vec.size() ; ++ i ) {
				string &temp = vec[i];
				if ( i == 0 ) {
					int flag = atoi( temp.c_str() );
					dval.buffer.writeInt8( flag );
					if ( flag == 1 ) {
						break;
					}
				} else if ( i >= 6 ) {  // 数据处理
					unsigned short word = 0;
					if ( ! temp.empty() ) {
						word = atoi( temp.c_str() );
					}
					dval.buffer.writeInt16( word );
				} else {
					dval.buffer.writeBlock( temp.c_str(), temp.length() );
				}
				if ( i != 8 ) {
					dval.buffer.writeInt8( ';' );
				}
			}
		}
		break;
	case 24: // 报警确认应答
	{
		vector<string> vec;
		splitvector(value, vec, "|", 0);

		if(vec.size() > 0 && atoi(vec[0].c_str()) == -1) {
			dval.msgid = 0x8001; // 回应通应应答
			dval.buffer.writeInt16(0);  // 应答序号
			dval.buffer.writeInt16(0x0200); // 位置上报通用应答
			dval.buffer.writeInt8(4);  // 报警确认

			seq = "";
			mac_id = "";
			command = "";
		} else if(vec.size() == 2) {
			dval.msgid = 0x8203; // 人工确认报警消息
			dval.buffer.writeInt16(atoi(vec[1].c_str())); // 报警消息流水号
			dval.buffer.writeInt32(atoi(vec[0].c_str())); // 人工确认报警类型
		}
	}
		break;
	case 25: //下发终端升级包
	{
		vector<string> fields;

		if(Utils::splitStr(value, fields, '|') != 4) {
			OUT_WARNING(NULL, 0, NULL, "inner msg error: %s", line.c_str());
			break;
		}

		CBase64 base64;
		if( ! base64.Decode(fields[3].c_str(), fields[3].length())) {
			OUT_WARNING(NULL, 0, NULL, "inner msg error: %s", line.c_str());
			break;
		}

		HttpQuery hQuery;
		if( ! hQuery.get(string(base64.GetBuffer(), base64.GetLength()))) {
			OUT_WARNING(NULL, 0, NULL, "inner msg error: %s", line.c_str());
			break;
		}

		dval.msgid = 0x8108;
		dval.buffer.writeInt8(atoi(fields[0].c_str()));
		dval.buffer.writeBlock((fields[1] + string(5, '\0')).c_str(), 5);
		dval.buffer.writeInt8(fields[2].length());
		dval.buffer.writeBlock(fields[2].c_str(), fields[2].length());
		dval.buffer.writeInt32(hQuery.size());
		dval.buffer.writeBlock(hQuery.data(), hQuery.size());
	}
		break;
	case 26:
	{
		vector<string> fields;

		if (Utils::splitStr(value, fields, '|') != 5) {
			OUT_WARNING(NULL, 0, NULL, "inner msg error: %s", line.c_str());
			break;
		}

		if((fields[3] != "-1" && fields[3].length() != 14) ||
				(fields[4] != "-1" && fields[4].length() != 14)) {
			OUT_WARNING(NULL, 0, NULL, "inner msg error: %s", line.c_str());
			break;
		}

		char bcdtime[6];

		dval.msgid = 0x8f16;
		dval.buffer.writeInt16(1);
		dval.buffer.writeInt8(atoi(fields[0].c_str()));

		if(fields[1] == "-1") {
			dval.buffer.writeInt8(0xff);
		} else {
			dval.buffer.writeInt8(atoi(fields[1].c_str()));
		}

		if(fields[2] == "-1") {
			dval.buffer.writeInt16(0xffff);
		} else {
			dval.buffer.writeInt16(atoi(fields[2].c_str()));
		}

		if(fields[3] == "-1") {
			memset(bcdtime, 0xff, 6);
		} else {
			strtoBCD(fields[3].substr(2).c_str(), bcdtime, 6);
		}
		dval.buffer.writeBlock(bcdtime, 6);

		if(fields[4] == "-1") {
			memset(bcdtime, 0xff, 6);
		} else {
			strtoBCD(fields[4].substr(2).c_str(), bcdtime, 6);
		}
		dval.buffer.writeBlock(bcdtime, 6);
	}
		break;
	case 27:
		dval.msgid = 0x8f16;
		dval.buffer.writeInt16(2);
		break;
	default:
		break;
	}
	if ( dval.msgid > 0 ) {
		// 发送数据处理
		SendMsgData( dval, car_id, mac_id, command, seq );
	}
}

void MsgClient::HandleDsndmMsg( string &line )
{
	string seq, mac_id, command, content, car_id;
	map< string, string > kvmap;
	if ( ! splitmsgheader( line, seq, mac_id, car_id, command, content, kvmap ) ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Split command error, line:%s" , line.c_str() );
		return;
	}

	string type = "";
	map< string, string >::iterator it = kvmap.find( "TYPE" );
	if ( it == kvmap.end() ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Command error, line:%s" , line.c_str() );
		return;
	}
	type = it->second;

	_SetData dval;

	if ( type == "1" ) {  // 文本下发

		it = kvmap.find( "1" );  // 文本下发标记位
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Command tag type error,line :%s" , line.c_str() );
			return;
		}
		unsigned char cflag = atoi( it->second.c_str() );

		it = kvmap.find( "2" );  //  BASE64文本内容
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Send text empty, line:%s" ,line.c_str() );
			return;
		}

		CBase64 base64;
		if ( ! base64.Decode( it->second.c_str(), it->second.length() ) ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Base64 decode failed, %s", line.c_str() );
			return;
		}

		dval.msgid = 0x8300;
		// 处理第一个位
		dval.buffer.writeInt8( cflag );
		dval.buffer.writeBlock( base64.GetBuffer(), base64.GetLength() );

	} else if ( type == "4" )  // 信息服务
			{
		it = kvmap.find( "11" ); // 信息服务的类型ID，与信息服务设置时定义ID相对应
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Message server id error, line: %s" , line.c_str() );
			return;
		}

		MsgServiceBody body;
		body.type = atoi( it->second.c_str() );

		it = kvmap.find( "12" ); // 信息服务的内容，BASE64编码（信息内容采用GBK编码）
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient","Message content error, line:%s" , line.c_str() );
			return;
		}
		CBase64 base64;
		if ( ! base64.Decode( it->second.c_str(), it->second.length() ) ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Base64 decode failed, %s", line.c_str() );
			return;
		}

		dval.msgid = 0x8304; // 信息服务
		// 处理第一个位
		body.len = htons( base64.GetLength() );
		dval.buffer.writeBlock( & body, sizeof ( body ) );
		dval.buffer.writeBlock( base64.GetBuffer(), base64.GetLength() );
	} else if ( type == "5" )  // 提问下发
			{
		it = kvmap.find( "16" );  // 问答属性描述
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Message ask attrib error, line: %s" , line.c_str() );
			return;
		}

		QuestAskBody body;
		body.flag = atoi( it->second.c_str() );

		it = kvmap.find( "17" ); // 问题的内容，BASE64编码（问题内容采用GBK编码）
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient","Message content empty, line:%s" , line.c_str() );
			return;
		}

		CBase64 base64;
		if ( ! base64.Decode( it->second.c_str(), it->second.length() ) ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Base64 decode failed, %s", line.c_str() );
			return;
		}
		body.len = base64.GetLength();

		dval.msgid = 0x8302; // 提问下发
		dval.buffer.writeBlock( & body, sizeof ( body ) );
		dval.buffer.writeBlock( base64.GetBuffer(), base64.GetLength() );

		vector< string > vecanswer;
		if ( parsemultidata( content, "18:", vecanswer, '[', ']' ) ) {
			map< string, string > amap;
			// 解析问题答案数据
			for ( size_t i = 0 ; i < vecanswer.size() ; ++ i ) {
				vector< string > vkey;
				splitvector( vecanswer[i], vkey, ",", 0 );
				if ( vkey.empty() ) {
					continue;
				}

				// 解析数据值
				if ( split2map( vkey, amap, ":" ) <= 0 ) {
					continue;
				}

				it = amap.find( "1" );  // 答案ID
				if ( it == amap.end() ) {
					continue;
				}
				QuestAnswerBody abody;
				abody.aflag = atoi( it->second.c_str() );

				it = amap.find( "2" ); // 答案内容
				if ( it == amap.end() ) {
					continue;
				}
				CBase64 coder;
				if ( ! coder.Decode( it->second.c_str(), it->second.length() ) ) {
					continue;
				}
				abody.alen = htons( coder.GetLength() );

				// 拷贝头部
				dval.buffer.writeBlock( & abody, sizeof ( abody ) );
				// 拷贝内容
				dval.buffer.writeBlock( coder.GetBuffer(), coder.GetLength() );
			}
		}
	}

	if ( dval.msgid > 0 ) {
		// 发送数据处理
		SendMsgData( dval, car_id, mac_id, command, seq );
	}
}

void MsgClient::HandleReqdMsg( string &line )
{
	string seq, mac_id, command, content, car_id;
	map< string, string > kvmap;
	if ( ! splitmsgheader( line, seq, mac_id, car_id, command, content, kvmap ) ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Split command error, line:%s" , line.c_str() );
		return;
	}

	string type = "";
	map< string, string >::iterator it = kvmap.find( "TYPE" );
	if ( it == kvmap.end() ) {
		OUT_ERROR( NULL, 0,"MsgClient", "Command error, line:%s" , line.c_str() );
		return;
	}

	type = it->second;

	_SetData dval;
	if ( type == "1" || type == "2" ) {
		/**
		 下行请求指令:
		 1:多媒体类型，
		 2:通道ID，
		 3:事件项编码，
		 4:开始时间，
		 5:结束时间，
		 6:删除标志(0：保留，1：删除)
		 */

		MediaDataBody body;
		it = kvmap.find( "1" );  // 1:多媒体类型，
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Media type error,line %s" , line.c_str() );
			return;
		}
		body.type = atoi( it->second.c_str() );

		it = kvmap.find( "2" );
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Media channel error,line %s" , line.c_str() );
			return;
		}
		body.channel = atoi( it->second.c_str() );

		it = kvmap.find( "3" ); // 3:事件项编码，
		if ( it != kvmap.end() ) {
			body.event = atoi( it->second.c_str() );
		}

		it = kvmap.find( "4" );  // 开始时间
		if ( it != kvmap.end() ) {
			// 将UTC转为BCD时间
			convert2bcd( it->second, body.starttime );
		}

		it = kvmap.find( "5" );  // 结束时间
		if ( it != kvmap.end() ) {
			// 将UTC转为BCD时间
			convert2bcd( it->second, body.endtime );
		}

		if ( type == "1" ) {  // 多媒数据检索
			dval.msgid = 0x8802;
			dval.buffer.writeBlock( & body, sizeof ( body ) );
		} else if ( type == "2" ) { //  多媒体数据上传

			dval.msgid = 0x8803;
			dval.buffer.writeBlock( & body, sizeof ( body ) );

			unsigned char flag = 0x00;
			it = kvmap.find( "6" );
			if ( it != kvmap.end() ) {
				flag = atoi( it->second.c_str() );
			}
			dval.buffer.writeInt8( flag );
		}
	} else if ( type == "3" ) {  // 单条存储多媒体数据检索上传命令
		dval.msgid = 0x8805;

		unsigned int mid = 0;
		it = kvmap.find( "7" );  // 多媒体ID，DWORD，补页新增
		if ( it != kvmap.end() ) {
			mid = atoi( it->second.c_str() );
		}

		unsigned char flag = 0x00;
		it = kvmap.find( "6" );  // 删除或者保留
		if ( it != kvmap.end() ) {
			flag = atoi( it->second.c_str() );
		}
		dval.buffer.writeInt32( mid );  // 多媒体ID
		dval.buffer.writeInt8( flag );	 // 0保留，1删除
	} else if ( type == "4" ) { //采集车辆行驶记录
		it = kvmap.find( "30" ); //采集命令
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,"MsgClient", "Get travel record data cmd error" );
			return;
		}
		string scmd = it->second;

		dval.msgid = 0x8700;  // 采集行驶记录仪命令
		dval.buffer.writeInt8( atoi( scmd.c_str() ) );

		it = kvmap.find( "31" ); // 行驶记录的数据
		if ( it != kvmap.end() ) {
			string sdata = it->second;
			CBase64 base64;  // 下传的数据块
			if ( ! base64.Decode( sdata.c_str(), sdata.length() ) ) {
				OUT_ERROR( NULL, 0, "MsgClient", "Base64 decode %s failed", sdata.c_str() );
				return;
			}
			dval.buffer.writeBlock( base64.GetBuffer(), base64.GetLength() );
		}
	} else if ( type == "5" ) { //上报车辆行驶记录
		it = kvmap.find( "30" ); //采集命令
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,NULL, "Get travel record data cmd error" );
			return;
		}
		string scmd = it->second;

		it = kvmap.find( "31" ); // 行驶记录的数据
		if ( it == kvmap.end() ) {
			OUT_ERROR( NULL, 0,NULL, "Get travel record data block error" );
			return;
		}
		string sdata = it->second;

		dval.msgid = 0x8701;  // 行驶记录仪下传命令
		dval.buffer.writeInt8( atoi( scmd.c_str() ) ); // 处理命令字操作

		CBase64 base64;  // 下传的数据块
		if (!base64.Decode(sdata.c_str(), sdata.length())) {
			OUT_ERROR(NULL, 0, NULL, "Base64 decode %s failed",sdata.c_str());
			return;
		}
		dval.buffer.writeBlock(base64.GetBuffer(), base64.GetLength());
	} else {
		OUT_ERROR( NULL, 0,"MsgClient", "Msg type error type %s, line %s" , type.c_str(), line.c_str() );
		return;
	}

	if ( dval.msgid > 0 ) {
		// 发送数据处理
		SendMsgData( dval, car_id, mac_id, command, seq );
	}
}

// 从后拷贝，不足填充字符
static void reverse_copy( char *buf, int len, const char *src, const char fix )
{
	int nlen = ( int ) strlen( src );
	int offset = len - nlen;
	if ( offset < 0 ) {
		offset = 0;
	}
	if ( offset > 0 ) {
		for ( int i = 0 ; i < offset ; ++ i ) {
			buf[i] = fix;
		}
	}
	memcpy( buf + offset, src, nlen );
}

// 发送处理数据
void MsgClient::SendMsgData( _SetData &val, const string &car_id, const string &mac_id, const string &command,
		const string &seq )
{
	GBheader header;
	GBFooter footer;

	char key[128] = { 0 };
	reverse_copy( key, 12, car_id.c_str(), '0' );
	strtoBCD( key, header.car_id );

	header.msgid = htons( val.msgid );

	// 取得需要发送的数据体
	char *ptr = val.buffer.getBuffer();
	int len = val.buffer.getLength();
	// 计算分包数
	int count = ( len / MAX_SPLITPACK_LEN ) + ( ( len % MAX_SPLITPACK_LEN == 0 && len > 0 ) ? 0 : 1 );

	int offset = 0, left = len, readlen = 0;

	// 根据分包的序号需要连续处理
	unsigned short seqid = _pEnv->GetSequeue( car_id.c_str(), count ) - count;

	// 处理分包发送数据
	for ( int i = 0 ; i < count ; ++ i ) {
		// 计算能处理长度
		readlen = ( left > MAX_SPLITPACK_LEN ) ? MAX_SPLITPACK_LEN : left;

		// 长度为消息内容的长度去掉头和尾
		unsigned short mlen = 0;
		if ( count > 1 ) { // 如果为多包数据，需要设置分包位标志
			mlen = htons( ( readlen & 0x23FF ) | 0x2000 );
		} else {
			mlen = htons( readlen & 0x03FF );
		}
		memcpy( & ( header.msgtype ), & mlen, sizeof(short) );

		unsigned short downseq = seqid + i + 1;
		if(val.msgid == 0x8001) {
			downseq = 0; //平台通用应答的解警功能不需要终端应答
		}
		header.seq = htons( downseq );

		DataBuffer buf;
		// 写入头部数据
		buf.writeBlock( & header, sizeof ( header ) );

		// 如果为分包数据需要处理分包的包数以及包序号
		if ( count > 1 ) {
			buf.writeInt16( count );
			buf.writeInt16( i + 1 );
		}
		// 读取长度
		if ( readlen > 0 ) {
			// 读取每个数据长度
			buf.writeBlock( ptr + offset, readlen );
			// 处理读取长度和偏移
			offset += readlen;
			left = left - readlen;
		}

		footer.check_sum = _gb_proto_handler->get_check_sum( buf.getBuffer() + 1, buf.getLength() - 1 );
		buf.writeBlock( & footer, sizeof ( footer ) );

		// 下发数据时，直接入等待响应队列中，如果没收着就重发，如果有多个数据包，则只有每一个包直接下发，其它数据包等待终端响应触发，或者超时触发
		_pEnv->GetClientServer()->HandleDownData( car_id.c_str(), buf.getBuffer(), buf.getLength() , downseq, ( i == 0 ) ) ;

		if(mac_id.empty() && command.empty() && seq.empty()) {
			continue; //不需要给应用服务应答的指令
		}

		string cache_key = car_id + "_" + ustodecstr( downseq );

		CacheData cache_data;
		cache_data.str_send_msg = "EXIT";
		cache_data.command = command;
		cache_data.seq = seq;
		cache_data.mac_id = mac_id;
		cache_data.send_time = time( 0 );
		_cache_data_pool.add( cache_key, cache_data );
	}
}

void MsgClient::on_dis_connection( socket_t *sock )
{
	User &user = _client_user;

	OUT_WARNING( user._ip.c_str() , user._port , user._user_id.c_str() , "fd %d on_dis_connection", sock->_fd );

	user._fd = NULL;
	user._user_state = User::OFF_LINE;
	user._last_active_time = time( 0 );
}

void MsgClient::TimeWork( )
{
	while ( Check() ) {
		// 检测数据异常备份
		if ( ! _dataqueue.Check() ) {
			sleep( 1 );
		}

		list<CacheData> lst;
		list<CacheData>::iterator it;
		if (_cache_data_pool.timeoutData(120, lst)) {
			for (it = lst.begin(); it != lst.end(); ++it) {
				CacheData &req = *it;
				// 处理超时数据
				string caits = "CAITR " + req.seq + " " + req.mac_id + " 0 "
						+ req.command + " {RET:5} \r\n";
				// 发送超时响应处理
				HandleUpData(caits.c_str(), caits.length());
			}
		}

		sleep(10);
	}
}

void MsgClient::NoopWork( )
{
	while ( Check() ) {
		HandleUserStatus();

		sleep( 10 );
	}
}

// 发送失败的数据
void MsgClient::on_send_failed( socket_t *sock , void* data, int len )
{
	OUT_INFO( NULL, 0, "MsgClient", "Failed:add fd %d length %d, data:%s" , sock->_fd, len , (const char*)data );
	// 如果发送失败的数据不为传输数据不需要处理缓存中
	if ( strncmp((const char*)data, "CAIT", 4 ) != 0 && len < 50 ) {
		return ;
	}
	// 放入缓存队列中
	_dataqueue.WriteCache( WAS_CLIENT_ID, ( void* ) data, len );
}

int MsgClient::build_login_msg( User &user, char *buf, int buf_len )
{
	string login_msg = "LOGI " + user._user_type + " " + user._user_name + " " + user._user_pwd + " \r\n";

	memcpy( buf, login_msg.c_str(), login_msg.length() );
	return login_msg.length();
}

void MsgClient::HandleUserStatus( )
{
	time_t now = time( NULL );
	User &user = _client_user;

	if(user._fd == NULL || user._user_state != User::ON_LINE || now - user._last_active_time > MAX_TIMEOUT_USER) {
		_dataqueue.Offline( WAS_CLIENT_ID);

		if ( ConnectServer( user, 10 ) ) {
			OUT_WARNING( user._ip.c_str(), user._port, user._user_name.c_str(), "fd %d connect server" , user._fd->_fd);
		} else {
			OUT_WARNING( user._ip.c_str(), user._port, user._user_name.c_str(), "connect msg server failed");
		}
	} else {
		string loop = "NOOP \r\n";
		SendRC4Data(user._fd, loop.c_str(), loop.length());
		_dataqueue.Online( WAS_CLIENT_ID );
		OUT_INFO( user._ip.c_str(), user._port, user._user_id.c_str(), "NOOP" );
	}
}

//=============================================================================
// 缓存数据回调接口
int MsgClient::HandleQueue( const char *sid, void *buf, int len , int msgid )
{
	// 如果发送失败的数据不为传输数据不需要处理缓存中
	if ( strncmp((const char*)buf, "CAIT", 4 ) != 0 && len < 50 ) {
		return IOHANDLE_SUCCESS;
	}

	User &user = _client_user;
	if(user._user_state != User::ON_LINE || user._fd == NULL) {
		return IOHANDLE_FAILED;
	}

	// 实际这里下发应该是上传才对
	if ( ! SendRC4Data( user._fd, ( const char * ) buf, len ) ) {
		return IOHANDLE_FAILED;
	}
	OUT_SEND3( user._ip.c_str(), user._port, user._user_id.c_str() , "from cache msg send:%s" , (const char *)buf );

	return IOHANDLE_SUCCESS;
}

// 发送RC4数据
bool MsgClient::SendRC4Data( socket_t *sock , const char *data, int len )
{
	if ( ! SendData( sock , data, len ) ) {
		return false ;
	}
	// 添加统计
	_sendstat.AddFlux( len );
	return true ;
}
