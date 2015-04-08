#include "vechilemgr.h"
#include <comlog.h>
#include <BaseTools.h>
#include <7ECoder.h>
#include <tools.h>
#include <databuffer.h>
#include <stdlib.h>


#define MAX_SPLITPACK_LEN   900
#define SIM_USECOND 		1000000
#define MAX_USECOND 	   30000000

CVechileMgr::CVechileMgr() : _vechile_inited(false)
{
	_thread_num     = 0 ;
	_time_span      = MAX_USECOND ;
	_vechile_num    = 20000 ;
	_gps_filepath   = "/root/data/lonlat.dat" ;
	_logistics_path = "/root/data/logistics.dat";
	_phone_numpre  = 159 ;
	_deluser_span  = 0 ;
	_start_pos     = 1 ;
	_last_deluser  = share::Util::currentTimeUsec() ;
	_alam_time	   = MAX_USECOND ;
	_deluser_num   = 0 ;
	_deluser_count = 0 ; // 记录当前掉线用户数
	_connect_mode  = TCP_MODE ; // TCP 模式
	_pic_url       = "/root/data/1.jpg" ;
	_simfirst_char = 'A' ;
	_logistics     = new CLogistics;
}

CVechileMgr::~CVechileMgr()
{
	Stop() ;

	if ( _logistics != NULL ) {
		delete _logistics ;
		_logistics = NULL ;
	}
}

bool CVechileMgr::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	char ip[128] = {0} ;
	if ( ! pEnv->GetString( "sim_connect_ip" , ip ) )
	{
		INFO_PRT( NULL, 0, NULL, "get sim_connect_ip failed" ) ;
		return false ;
	}
	_server_ip  = ip ;

	int port = 0 ;
	if ( ! pEnv->GetInteger("sim_connect_port" , port ) )
	{
		INFO_PRT( NULL, 0, NULL, "get sim_connect_port failed" ) ;
		return false ;
	}
	_server_port = port ;

	int mode  = 0 ;
	if ( pEnv->GetInteger( "sim_connect_mode" , mode ) ) {
		_connect_mode = mode ;
	}

	// 接收和发送线程数
	int thread = 0 ;
	if ( pEnv->GetInteger("sim_thread_count" , thread) ) {
		_thread_num = thread ;
	}

	// 上报位置的时间
	int nvalue = 0 ;
	if ( pEnv->GetInteger("sim_vechile_span" , nvalue ) ) {
		_time_span = nvalue * SIM_USECOND ;
	}

	// 模拟车的个数
	if ( pEnv->GetInteger( "sim_vechile_num" , nvalue ) ) {
		_vechile_num = nvalue ;
	}

	// 开始处理位置
	if ( pEnv->GetInteger( "sim_vechile_start" , nvalue ) ) {
		_start_pos = nvalue ;
	}

	// 号码前缀
	if ( pEnv->GetInteger( "sim_vechile_phone" , nvalue ) ) {
		_phone_numpre = nvalue ;
	}

	// 掉线用户时间间隔
	if ( pEnv->GetInteger( "sim_vechile_del" , nvalue ) ) {
		_deluser_span = nvalue * SIM_USECOND ;
	}

	// 一次用户掉线个数
	if ( pEnv->GetInteger( "sim_vechile_dnum" , nvalue ) ) {
		_deluser_num = nvalue ;
	}

	// 上传图片时间
	if ( pEnv->GetInteger( "sim_vechile_upload" , nvalue) ) {
		_upload_time = nvalue * SIM_USECOND ;
	}

	// 上传CAN数据时间
	if ( pEnv->GetInteger( "sim_vechile_candata", nvalue ) ) {
		_candata_time = nvalue * SIM_USECOND ;
	}
	// 报警上报时间间隔
	if ( pEnv->GetInteger( "sim_vechile_alam", nvalue ) ) {
		_alam_time = nvalue * SIM_USECOND ;
	}

	// 读取GPS数据文件路径
	char path[512] = {0} ;
	if ( pEnv->GetString( "sim_vechile_path" , path ) ) {
		_gps_filepath = path ;
	}

	// 读取物流参数数文件路径
	if ( pEnv->GetString( "sim_logistics_path" , path ) ) {
		_logistics_path = path ;
	}

	// 读取图片路径
	if ( pEnv->GetString( "sim_vechile_pic" , path ) ) {
		_pic_url = path ;
	}

	// 取得第一个字符
	if ( pEnv->GetString( "sim_first_char", path ) ) {
		_simfirst_char = ( unsigned char ) path[0] ;
	}

	// 用户数据文件
	if ( pEnv->GetString( "sim_car_user", path ) ) {
		_sim_car_user = path ;
	}

	setpackspliter( &_pack_spliter ) ;

	if ( ! LoadGpsData( _gps_filepath.c_str() , _gps_vec ) ){
		printf( "load gps data failed, %s \n"  , _gps_filepath.c_str()  ) ;
		OUT_ERROR( NULL, 0, "VMGR" , "load gps data failed, %s" , _gps_filepath.c_str() ) ;
		return false ;
	}

	if (!_logistics->LoadInitFile(_logistics_path.c_str()))
	{
		printf( "load logistics data failed, %s \n", _logistics_path.c_str()  ) ;
		OUT_ERROR( NULL, 0, "VMGR" , "load logistics data failed, %s" , _logistics_path.c_str() ) ;
	    return false ;
	}

	// 从文件中加载
	if ( ! LoadUserFromPath( _sim_car_user.c_str() ) ) {
		LoadAllUser() ;
	}
	
	int i;
	int kInt, vInt;
	char speedStr[1024] = ""; 
	vector<string> speedItem;
	vector<string> speedPair;
	if( pEnv->GetString( "car_speed", speedStr ) ) {
		speedItem.clear(); 
		splitvector(speedStr, speedItem, ",", 0);
		for(i = 0; i < speedItem.size(); ++i) {
			speedPair.clear();
			splitvector(speedItem[i], speedPair, ":", 0);
			if(speedPair.size() != 2) {
				continue;
			}

			kInt = atoi(speedPair[0].c_str()); 
			vInt = atoi(speedPair[1].c_str());

			_car_speed.insert(make_pair(kInt, vInt));
		}
	}

	return _bench.Init() ;
}

// 转换在整型变量
static int my_atoi( const char *ptr ) {
	if ( ptr == NULL ) {
		return 0 ;
	}
	int len = strlen( ptr ) ;
	for ( int i = 0; i < len; ++ i ) {
		if ( ptr[i] >= '0' && ptr[i] <= '9' ) {
			continue ;
		}
		return 0 ;
	}
	return atoi( ptr ) ;
}

static void my_strrcpy( char *dest, int len, const char *num )
{
	int n = strlen(num) ;
	if ( len < n ) {
		return ;
	}

	int i =  0;
	for ( i = 0; i < (len -n); ++ i ) {
		dest[i] = '0' ;
	}
	memcpy( dest+i, num , n ) ;
}

// 从文件中加载用户数据
bool CVechileMgr::LoadUserFromPath( const char *file )
{
	if ( file == NULL )
		return false ;

	char buf[1024] = {0};
	FILE *fp = NULL;
	fp = fopen( file, "r" );
	if (fp == NULL) {
		OUT_ERROR( NULL, 0, NULL, "Load sim user file %s failed", file ) ;
		return false;
	}

	int pos = _gps_vec.size() ;
	// 手机号: 省域：市域：车牌颜色：车牌号码：终端ID：终端型号
	int count = 0 ;
	while (fgets(buf, sizeof(buf), fp)) {
		unsigned int i = 0;
		while (i < sizeof(buf)) {
			if (!isspace(buf[i]))
				break;
			i++;
		}
		if (buf[i] == '#')
			continue;

		char temp[1024] = {0};
		for (int i = 0, j = 0; i < (int)strlen(buf); ++ i ) {
			if (buf[i] != ' ' && buf[i] != '\r' && buf[i] != '\n') {
				temp[j++] = buf[i];
			}
		}

		string line = temp;
		vector<string> vec;
		if ( ! splitvector( line, vec, ":" , 7 ) ){
			continue ;
		}

		_stVechile *p = new _stVechile ;

		srand( count ) ;

		p->ufd_         = 0 ;
		p->fd_          = 0 ;
		p->gps_pos_	    = rand() % pos ;
		p->last_access_ = share::Util::currentTimeUsec() ;
		p->last_gps_	= share::Util::currentTimeUsec() ;
		p->last_pic_	= 0 ;
		p->seq_id_		= 0 ;
		p->last_conn_   = 0 ;
		p->gps_count_   = 0 ;
		p->lgs_time_    = share::Util::currentTimeUsec() ;
		p->car_state_	= OFF_LINE ;
		// 手机号: 省域：市域：车牌颜色：车牌号码：终端ID：终端型号

		char phone[13] = {0} ;
		my_strrcpy( phone, 12, vec[0].c_str() ) ;

		sprintf( p->carnum_  , "%s" , vec[4].c_str() ) ;
		sprintf( p->termid_  , "%s" , vec[5].c_str() ) ;
		sprintf( p->phone    , "%s" , vec[0].c_str() ) ;
		sprintf( p->termtype , "%s" , vec[6].c_str() ) ;
		strtoBCD( phone, p->car_id_ ) ;

		p->_next = p->_pre = NULL ;
		p->carcolor		= my_atoi( vec[3].c_str() );
		p->cityid		= my_atoi( vec[2].c_str() );
		p->proid		= my_atoi( vec[1].c_str() );

		// 添加离线用户
		_car_queue.push( p ) ;

		++ count ;
	}
	fclose(fp);
	fp = NULL;

	_bench.IncBench( BENCH_ALL_USER, count ) ;

	return ( count > 0 ) ;
}

// 加载所有用户
void CVechileMgr::LoadAllUser( void )
{
	if ( _vechile_num == 0 ) {
		return ;
	}

	int pos = _gps_vec.size() ;

	char phone[13] = {0} ;

	// 手机号: 省域：市域：车牌颜色：车牌号码：终端ID：终端型号
	for ( int i = 0; i < (int)_vechile_num; ++ i ) {
		_stVechile *p = new _stVechile ;

		srand( i ) ;

		p->ufd_         = 0 ;
		p->fd_          = 0 ;
		p->gps_pos_	    = rand() % pos ;
		p->last_access_ = share::Util::currentTimeUsec() ;
		p->last_gps_	= share::Util::currentTimeUsec() ;
		p->last_pic_	= 0 ;
		p->seq_id_		= 0 ;
		p->last_conn_   = 0 ;
		p->gps_count_   = 0 ;
		p->lgs_time_    = share::Util::currentTimeUsec() ;
		p->car_state_	= OFF_LINE ;
		p->_next = p->_pre = NULL ;
		p->carcolor		= 2 ;
		p->cityid		= 2000 ;
		p->proid		= 43 ;

		sprintf( p->termid_  , "%c%05d" ,_simfirst_char, i+_start_pos ) ;
		sprintf( p->carnum_  , "京%c%05d" , _simfirst_char, i + _start_pos ) ;
		sprintf( phone, "0%03d%08d" , _phone_numpre , i+_start_pos ) ;
		sprintf( p->phone, "%03d%08d" , _phone_numpre , i+_start_pos ) ;
		sprintf( p->termtype, "%s", phone ) ;
		strtoBCD( phone, p->car_id_ ) ;

		// 添加离线用户
		_car_queue.push( p ) ;
	}
	_bench.IncBench( BENCH_ALL_USER, _vechile_num ) ;
}

bool CVechileMgr::Start( void )
{
	if ( ! _send_thread.init( 1 , (void*) THREAD_SEND, this ) ) {
		printf( "init send thread failed\n" ) ;
		return false ;
	}

	_vechile_inited  = true ;

	_send_thread.start() ;

	_bench.Start() ;

	// 是否为UDP模式
	if ( _connect_mode & UDP_MODE ) {
		if ( ! StartUDP( _server_ip.c_str(), _server_port, _thread_num ) ) {
			printf( "start udp client failed\n" ) ;
			return false ;
		}
	}
	// 启动处理线程
	return StartClient( _server_ip.c_str(), _server_port , _thread_num ) ;
}

void CVechileMgr::Stop( void )
{
	if ( ! _vechile_inited )
		return ;
	_vechile_inited = false ;

	StopClient() ;

	_send_thread.stop() ;

	_bench.Stop() ;
}

void CVechileMgr::on_data_arrived( socket_t *sock, const void* data, int len )
{
	OUT_HEX( sock->_szIp , sock->_port , "RECV" , (const char*) data, len ) ;
	C7eCoder coder ;
	if ( ! coder.Decode( (const char*)data, len ) ) {
		OUT_ERROR( sock->_szIp , sock->_port , "VMGR", "dat decode error" ) ;
		OUT_HEX( sock->_szIp , sock->_port , "VMGR" , (const char*) data, len ) ;
		return ;
	}
	// 处理一个数据包
	HandleOnePacket( sock, (const char *)coder.GetData(), coder.GetSize() ) ;
}

// 处理一个数据包
void CVechileMgr::HandleOnePacket( socket_t *sock, const char *data, int len )
{
	GBheader *header = (GBheader*)data;
	unsigned short _s = 0x03FF;
	unsigned short nmsg = 0 ;
	memcpy(&nmsg,&(header->msgtype),sizeof(unsigned short));
	nmsg = ntohs( nmsg ) ;

	// 取得前10位值为长度
	unsigned short msg_len = nmsg & _s;
	unsigned short msg_id  = ntohs(header->msgid);
	string str_car_id 	   = BCDtostr(header->car_id).substr(1,11);
	// unsigned short seq     = ntohs(header->seq);

	const char *ip 		= sock->_szIp;
	unsigned short port = sock->_port;

	// 将到来的数据写入日志中
	OUT_INFO( ip, port, str_car_id.c_str(), "******************fd %d on data arrrived***************", sock->_fd );
	OUT_HEX( ip, port, str_car_id.c_str() , data , len ) ;
	OUT_INFO( ip, port, str_car_id.c_str(), "********************************************************");

	int need_len = (int)(msg_len  + sizeof(GBFooter) + sizeof(GBheader)) ;
	// 优先计算长度是否正确
	if ( msg_len == 0 || len < need_len ) {
		OUT_ERROR( ip, port, str_car_id.c_str(), "msg id %x, data length error, len %d, msg len %d, need length %d" ,
				msg_id, len , msg_len , need_len ) ;
		return ;
	}
	// 如果第13位为1则为分包消息
	if ( nmsg & 0x2000 ) {
		// 如果为分包消息
		if ( len != (int)(msg_len + sizeof(GBFooter) + sizeof(GBheader) + sizeof(MediaPart)) ){
			OUT_ERROR( ip, port, str_car_id.c_str(), "msg id %x, long message data length error, len %d, msg len %d,need length %d" ,
					msg_id, len , msg_len ,need_len + sizeof(MediaPart) ) ;
			return ;
		}
	} else { // 如果是普通消息
		if ( len != (int)(msg_len  + sizeof(GBFooter) + sizeof(GBheader))  ) {
			OUT_ERROR( ip, port, str_car_id.c_str(), "msg id %x, message length error, len %d, msg len %d, need length %d" ,
					msg_id, len , msg_len , need_len ) ;
			return ;
		}
	}

	//终端注册,鉴权等与登录相关的东西，单独处理。
	if ( msg_id == 0x8100 )
	{
		TermRegisterRespHeader *resp = ( TermRegisterRespHeader *) data ;
		// 注册成功的响应返回鉴权码，发送鉴权处理
		if ( resp->result == 0x00 ) {

			const int  data_len = msg_len - 3 ;
			if ( data_len <= 0 ) {
				OUT_ERROR( ip, port, str_car_id.c_str(), "authcode length zero, data len: %d", data_len ) ;
				return ;
			}

			const char *authcode = ( const char *) ( data + sizeof(TermRegisterRespHeader) ) ;

			TermAuthenticationHeader req ;

			int nlen = sizeof(TermAuthenticationHeader) + sizeof(GBFooter) + data_len ;

			req.header.msgid = htons( 0x0102 ) ;
			req.header.seq   = resp->header.seq ;
			memcpy( req.header.car_id , resp->header.car_id , sizeof(req.header.car_id ) ) ;

			unsigned short dlen = htons( data_len & 0x03FF ) ;
			memcpy( &req.header.msgtype , &dlen , sizeof(short) ) ;

			int offset = 0 ;

			char *buf = new char[nlen+1] ;
			memcpy( buf, &req, sizeof(req) ) ;
			offset += sizeof(req) ;

			// 添加返回授权码
			memcpy( buf + offset, authcode , data_len ) ;
			offset += data_len ;

			GBFooter  footer ;
			memcpy( buf + offset, &footer, sizeof(footer) ) ;

			if ( ! Send5BData( sock,  buf, nlen ) ) {
				OUT_ERROR( ip, port, str_car_id.c_str(), "send term auth failed" ) ;
				OUT_HEX( ip, port, str_car_id.c_str(), buf, nlen ) ;
			}

			delete [] buf ;
		}
	}
	else if ( msg_id == 0x8001 )
	{
		PlatFormCommonResp *resp = ( PlatFormCommonResp *) data ;
		unsigned short rsp_msgid = ntohs( resp->resp.resp_msg_id )  ;
		if ( rsp_msgid == 0x0102 ) { // 如果是终端鉴权
			if ( resp->resp.result == 0x00 ) {
				if ( sock->_ptr != NULL ) {
					// 将离线用户转为在线用户
					((_stVechile*)sock->_ptr)->car_state_ = ON_LINE ;
				}
				// 记录登陆日志
				OUT_CONN( ip , port, str_car_id.c_str() , "Term auth success , fd %d" , sock->_fd ) ;
			}
		}
	}
	else if (msg_id == 0x8900) //数据下行透传
	{
		const int  data_len = msg_len - 3 ;
		if ( data_len <= 0 ) {
		   OUT_ERROR( ip, port, str_car_id.c_str(), "transparent transmission length zero, data len: %d", data_len ) ;
		   return;
	    }

	    if (data[sizeof(TransHeader)] == 0x01){ //透传类型

          DataBuffer pBuf;
	      int nLength = _logistics->ParseTransparentMsgData((unsigned char *)&data[sizeof(TransHeader)+1],
			                                                data_len-1,pBuf);
	      if (nLength >0){ //下发返回
	    	  TermCommonResp req ;
	    	  int nlen = sizeof(TermCommonResp) ;

	    	  req.header.msgid = htons( 0x0001 ) ;
	    	  req.header.seq   = header->seq ;
	    	  memcpy( req.header.car_id , header->car_id , sizeof(req.header.car_id ) ) ;

	    	  unsigned short dlen = nlen - sizeof(GBheader) - sizeof(GBFooter) ;
	    	  dlen = htons( dlen & 0x03FF ) ;
	    	  memcpy( &req.header.msgtype , &dlen , sizeof(short) ) ;

	    	  req.resp.resp_msg_id = htons( msg_id ) ;
	    	  req.resp.resp_seq	   = header->seq ;
	    	  req.resp.result      = 0x00 ;

	    	  // 其它统一发送通用应答
	    	  Send5BData( sock, (const char *)&req, sizeof(req));

	    	  DataBuffer   repBuffer;
	    	  TransHeader  theader;
	    	  GBFooter     footer;

	    	  theader.header.msgid = htons(0x0900);
	    	  theader.header.seq   = header->seq;

	    	  unsigned short mlen = (unsigned short)pBuf.getLength();
	    	  mlen = htons( mlen & 0x03FF ) ;
			  memcpy( &theader.header.msgtype , &mlen , sizeof(short) ) ;

	    	  memcpy(theader.header.car_id,header->car_id , sizeof(req.header.car_id));

	    	  repBuffer.writeBlock(&theader,sizeof(theader));
	    	  repBuffer.writeBlock(pBuf.getBuffer(),pBuf.getLength());
	    	  repBuffer.writeBlock(&footer,sizeof(footer));

	    	  Send5BData( sock ,repBuffer.getBuffer(),repBuffer.getLength());//发送物流通用应答
	       }
	    }
	}
	else
	{
		TermCommonResp req ;

		int nlen = sizeof(TermCommonResp) ;

		req.header.msgid = htons( 0x0001 ) ;
		req.header.seq   = header->seq ;
		memcpy( req.header.car_id , header->car_id , sizeof(req.header.car_id ) ) ;

		unsigned short dlen = nlen - sizeof(GBheader) - sizeof(GBFooter) ;
		dlen = htons( dlen & 0x03FF ) ;
		memcpy( &req.header.msgtype , &dlen , sizeof(short) ) ;

		req.resp.resp_msg_id = htons( msg_id ) ;
		req.resp.resp_seq	 = header->seq ;
		req.resp.result		 = 0x00 ;

		if ( msg_id == 0x8801 ) {
			if ( sock->_ptr != NULL ) {
				OUT_PRINT( NULL, 0, NULL, "add pic %s", str_car_id.c_str() ) ;
				// 添加需要发送拍图片请求中
				_piclist.push_back( ( _stVechile*)sock->_ptr ) ;
				_picmonitor.notify() ;
			}
		}

		// 其它统一发送通用应答
		Send5BData( sock, (const char *)&req, sizeof(req) ) ;
	}

	// 记录发送数据
	_bench.IncBench( BENCH_MSGRECV ) ;
}

void CVechileMgr::on_dis_connection( socket_t *sock )
{
	// 断开连接处理
	OUT_CONN( sock->_szIp, sock->_port, "DIS" , "on dis connection fd %d" , sock->_fd ) ;
	// 移除用户队列
	if ( sock->_ptr != NULL ) {
		_stVechile *p = (_stVechile*)sock->_ptr ;
		p->car_state_ = OFF_LINE ;
		_bench.IncBench( BENCH_DISCONN, 1 ) ;
		sock->_ptr = NULL ;
	}
}

int CVechileMgr::get_car_speed()
{
	time_t t;
	time(&t);
	struct tm local_tm;
	struct tm *tm = localtime_r( &t, &local_tm ) ;

	map<int,int>::iterator it;
	it = _car_speed.find(tm->tm_hour);
	if(it != _car_speed.end()) {
		return it->second * 10;
	}

	return 0;
}

//050507
static void get_bcd_time( char bcd[6] )
{
	char time1[128] = {0};
	time_t t;
	time(&t);
	struct tm local_tm;
	struct tm *tm = localtime_r( &t, &local_tm ) ;

	sprintf(time1, "%02d%02d%02d%02d%02d%02d", (tm->tm_year + 1900)%100 , tm->tm_mon + 1, tm->tm_mday,
			tm->tm_hour, tm->tm_min, tm->tm_sec);
	strtoBCD( time1, bcd ) ;
}

static void write_dword( DataBuffer &buf , unsigned char cmd ,unsigned int n )
{
	buf.writeInt8( cmd ) ;
	buf.writeInt8( 4 ) ;
	buf.writeInt32( n ) ;
}

static void write_word( DataBuffer &buf, unsigned char cmd , unsigned short n )
{
	buf.writeInt8( cmd ) ;
	buf.writeInt8( 2 ) ;
	buf.writeInt16( n ) ;
}

static void write_byte( DataBuffer &buf, unsigned char cmd , unsigned char n )
{
	buf.writeInt8( cmd ) ;
	buf.writeInt8( 1 ) ;
	buf.writeInt8( n ) ;
}

static void write_block( DataBuffer &buf, unsigned char cmd ,unsigned char *data, unsigned char len )
{
	buf.writeInt8( cmd ) ;
	buf.writeInt8( len ) ;
	buf.writeBlock( data, len ) ;
}

/*发送透明传输数据*/
bool CVechileMgr::SendTransparentMsg(_stVechile *p , int ncount,unsigned short wType)
{
	if( ncount <= 0 ) {
		return false ;
	}

	DataBuffer transport_buf;
	TransHeader  header;

	int nLen = _logistics->BuildTransportData(wType,transport_buf);

	OUT_HEX(NULL, 0, "Transport", transport_buf.getBuffer(),transport_buf.getLength());

	BuildHeader(header.header, 0x900,nLen, p);

	DataBuffer sendbuf;

	sendbuf.writeBlock(&header, sizeof(header));
	sendbuf.writeBlock(transport_buf.getBuffer(), transport_buf.getLength());

	unsigned short  mlen = (sendbuf.getLength()-sizeof(GBheader)) & 0x03FF ;
	sendbuf.fillInt16( mlen, 3 ) ;

	GBFooter footer ;
	sendbuf.writeBlock(&footer, sizeof(footer) ) ;

	if ( ! Send5BData( p->fd_, sendbuf.getBuffer(), sendbuf.getLength() ) ) {
		p->car_state_ = OFF_LINE ;
		return false ;
	}
	p->lgs_time_ = share::Util::currentTimeUsec() ;

    return true ;
}

bool CVechileMgr::SendLocationPos( _stVechile *p , int ncount )
{
	if( ncount <= 0 || p == NULL ) {
		return false ;
	}

	time_t now = share::Util::currentTimeUsec() ;
	srand( now ) ;
	int nrand = rand() ;  // 产生随机报警

	DataBuffer buf ;

	TermLocationHeader  header ;
	BuildHeader( header.header, 0x200 , sizeof(GpsInfo), p ) ;

	unsigned int alarm = 0 ;
	if ( now - p->last_alam_  > _alam_time && _alam_time > 0 ) {
		alarm = htonl( nrand ) ;
		p->last_alam_ = now ;
	}
	memcpy( &header.gpsinfo.alarm, &alarm, sizeof(unsigned int) ) ;

	unsigned int state = 0 ;
	memcpy( &header.gpsinfo.state , &state, sizeof(unsigned int) ) ;

	int pos = p->gps_pos_ ;
	pos = pos + 1 ;
	pos = pos % ncount ;

	Point &pt = _gps_vec[pos] ;

	header.gpsinfo.heigth    = htons(nrand%100) ;
	header.gpsinfo.speed     = htons(get_car_speed());//htons(nrand%1000)  ;
	header.gpsinfo.direction = htons(nrand%360) ;
	// 取得当前BCD的时间
	get_bcd_time( header.gpsinfo.date_time ) ;

	if(header.gpsinfo.speed == 0) {
		header.gpsinfo.state.bit6 = 0;
		header.gpsinfo.state.bit7 = 0;
		header.gpsinfo.longitude = 0 ;
		header.gpsinfo.latitude  = 0 ;
	} else {
		header.gpsinfo.state.bit6 = 1;
		header.gpsinfo.state.bit7 = 1;
		header.gpsinfo.longitude = htonl( pt.lon ) ;
		header.gpsinfo.latitude  = htonl( pt.lat ) ;
	}

	buf.writeBlock( &header, sizeof(header) ) ;

	// 需要上传CAN的数据
	if ( now - p->last_candata_ > _candata_time && _candata_time > 0 ) {

		p->last_candata_ = now ;

		int index = nrand % 5;

		write_dword( buf, 0x01, 2000 ) ;
		write_word(  buf, 0x02, 100 ) ;
		write_word(  buf, 0x03, 800 ) ;
		if ( index != 0 ) {
			struct _B1{
				unsigned char cmd ;
				unsigned int  val ;
			};
			_B1 b1 ;
			b1.cmd = ( index - 1 ) ;
			b1.val = htonl( nrand ) ;

			write_block( buf, 0x11, (unsigned char*)&b1 , sizeof(_B1) ) ;
		} else {
			write_byte( buf, 0x11, 0 ) ;
		}

		if ( nrand % 2 == 0 ) {
			struct _B2{
				unsigned char type;
				unsigned int  val ;
				unsigned char dir ;
			};
			_B2 b2 ;
			b2.type = (( index + 1 ) % 5 ) ;
			b2.val  = htonl( index ) ;
			b2.dir  = ( index%2 == 0 ) ;

			write_block( buf, 0x12, (unsigned char*)&b2, sizeof(_B2) ) ;
		}
		if ( nrand % 3 == 0 ) {
			struct _B3{
				unsigned int   id ;
				unsigned short time ;
				unsigned char  result ;
			};
			_B3 b3 ;
			b3.id     = htonl( index ) ;
			b3.time   = htons( nrand % 100 ) ;
			b3.result = ( index % 2 == 0 ) ;

			write_block( buf , 0x13, (unsigned char*)&b3, sizeof(_B3) ) ;
		}

		switch( index ) {
		case 0:
			write_word(  buf, 0x20, nrand ) ;
			write_word(  buf, 0x21, nrand % 100 ) ;
			write_word(  buf, 0x22, nrand % 1000 ) ;
			write_word(  buf, 0x23, nrand % 80 ) ;
		case 1:
			write_dword( buf, 0x24, nrand % 157887 ) ;
			write_dword( buf, 0x25, nrand % 233555 ) ;
			write_dword( buf, 0x26, nrand % 200 ) ;
			write_dword( buf, 0x40, nrand % 3000 ) ;
		case 2:
			write_word(  buf, 0x41, nrand % 130 ) ;
			write_word(  buf, 0x42, nrand % 120 ) ;
			write_word(  buf, 0x43, nrand % 200 ) ;
			write_word(  buf, 0x44, nrand % 300 ) ;
		case 3:
			write_word(  buf, 0x45, nrand % 150 ) ;
			write_word(  buf, 0x46, nrand % 100 ) ;
		case 4:
			write_word(  buf, 0x47, nrand % 150 ) ;
			write_word(  buf, 0x48, nrand % 200 ) ;
			break ;
		}
	}

	// 长度为消息内容的长度去掉头和尾
	unsigned short  mlen = (buf.getLength()-sizeof(GBheader)) & 0x03FF ;
	buf.fillInt16( mlen, 3 ) ;

	GBFooter footer ;
	buf.writeBlock( &footer, sizeof(footer) ) ;

	if ( Send5BData( p->fd_, buf.getBuffer(), buf.getLength() ) ) {
		p->gps_pos_ = pos ;
		return true ;
	}
	p->car_state_ = OFF_LINE ;
	// 发送位置包
	return false ;
}

// 发送图片数据
bool CVechileMgr::SendLocationPic( _stVechile *p , int ncount )
{
	if( ncount <= 0 || p == NULL )
		return false ;

	if ( access( _pic_url.c_str(), 0 ) != 0 ) {
		OUT_ERROR( NULL, 0, NULL, "check pic dir %s failed", _pic_url.c_str() ) ;
		return false ;
	}

	int  photo_len = 0 ;
	char *pfile = ReadFile( _pic_url.c_str() , photo_len ) ;

	GBFooter  footer ;
	GBheader  header ;
	header.msgid = htons(0x0801) ;
	memcpy( header.car_id , p->car_id_ , sizeof(header.car_id) ) ;

	MediaUploadBody  body ;
	body.id     = 1 ;
	body.type   = 0 ;
	body.mtype  = 0 ;
	body.event  = 1 ;
	body.chanel = 1 ;

	unsigned int alarm = 0 ;
	memcpy( &body.gps.alarm, &alarm, sizeof(unsigned int) ) ;

	unsigned int state = 0 ;
	memcpy( &body.gps.state , &state, sizeof(unsigned int) ) ;

	int pos = p->gps_pos_ ;
	pos = ++ pos % ncount ;

	Point &pt = _gps_vec[pos] ;

	body.gps.heigth    = htons(100) ;
	body.gps.speed     = htons(get_car_speed());//htons(60)  ;
	body.gps.direction = htons(0) ;

	if(body.gps.speed == 0) {
		body.gps.state.bit0 = 0;
		body.gps.state.bit1 = 0;
		body.gps.longitude = 0 ;
		body.gps.latitude  = 0 ;
	} else {
		body.gps.state.bit0 = 1;
		body.gps.state.bit1 = 1;
		body.gps.longitude = htonl( pt.lon ) ;
		body.gps.latitude  = htonl( pt.lat ) ;
	}
	// 取得当前BCD的时间
	get_bcd_time( body.gps.date_time ) ;

	DataBuffer buffer ;
	buffer.writeBlock( &body, sizeof(body) ) ;
	buffer.writeBlock( pfile, photo_len ) ;

	FreeBuffer( pfile ) ;

	// 取得需要发送的数据体
	char *ptr = buffer.getBuffer() ;
	int len   = buffer.getLength() ;
	// 计算分包数
	int count = ( len / MAX_SPLITPACK_LEN ) + ( ( len % MAX_SPLITPACK_LEN == 0 && len > 0 ) ? 0 : 1 ) ;

	int offset = 0 , left = len , readlen = 0 ;

	// 根据分包的序号需要连续处理
	unsigned short seqid = 0 ;
	{
		_seq_mutex.lock() ;
		p->seq_id_ = p->seq_id_ + count ;
		if ( p->seq_id_ < count ) {
			p->seq_id_ = count + 1 ;
			seqid      = 1 ;
		} else {
			seqid      = p->seq_id_ - count ;
		}
		_seq_mutex.unlock() ;
	}

	// 发送FD的数据
	socket_t *fd = ( p->ufd_ != NULL ) ? p->ufd_ : p->fd_ ;
	// 处理分包发送数据
	for ( int i = 0 ; i < count; ++ i ) {
		// 计算能处理长度
		readlen = ( left > MAX_SPLITPACK_LEN ) ? MAX_SPLITPACK_LEN : left ;

		// 长度为消息内容的长度去掉头和尾
		unsigned short  mlen = 0 ;
		if ( count > 1 ) { // 如果为多包数据，需要设置分包位标志
			mlen = htons(  ( readlen & 0x23FF ) | 0x2000 ) ;
		} else {
			mlen = htons(  readlen & 0x03FF ) ;
		}
		memcpy( &(header.msgtype), &mlen, sizeof(short) );

		unsigned short downseq = seqid + i + 1 ;
		header.seq = htons(downseq) ;

		DataBuffer buf ;
		// 写入头部数据
		buf.writeBlock( &header, sizeof(header) ) ;

		// 如果为分包数据需要处理分包的包数以及包序号
		if ( count > 1 ) {
			buf.writeInt16( count ) ;
			buf.writeInt16( i + 1 ) ;
		}
		// 读取长度
		if ( readlen > 0 ) {
			// 读取每个数据长度
			buf.writeBlock( ptr + offset, readlen ) ;
			// 处理读取长度和偏移
			offset += readlen ;  left = left - readlen ;
		}
		buf.writeBlock( &footer, sizeof(footer) ) ;

		if ( Send5BData( fd , buf.getBuffer(), buf.getLength() )  ) {
			// 打印发送的数据
			printf( " fd %d, send pic %d\n", fd->_fd, i ) ;
		} else {
			p->car_state_ = OFF_LINE ;
		}
	}

	p->gps_pos_  = pos ;

	OUT_PRINT( NULL, 0, NULL, "send pic %s", p->phone ) ;
	// 发送位置包
	return true ;
}

// 取得用户列表
int CVechileMgr::GetUser( list<_stVechile*> &lst , int state )
{
	_carmutex.lock() ;

	int size = _car_queue.size() ;
	if ( size == 0 ) {
		_carmutex.unlock() ;
		return 0 ;
	}

	int count = 0 ;

	_stVechile *p = _car_queue.begin() ;
	while( p != NULL ) {
		if ( p->car_state_ == state ) {
			lst.push_back( p ) ;
			++ count ;
		}
		p = p->_next ;
	}
	_carmutex.unlock() ;

	if ( state == OFF_LINE ) {
		_bench.IncBench( BENCH_OFF_LINE, count ) ;
		_bench.IncBench( BENCH_ON_LINE, size - count ) ;
	} else {
		_bench.IncBench( BENCH_ON_LINE, count ) ;
		_bench.IncBench( BENCH_OFF_LINE, size - count ) ;
	}

	return count ;
}

// 检测在线车辆
int64_t CVechileMgr::CheckOnlineUser( int ncount )
{
	int64_t ntime = _time_span;

	list<_stVechile*> lst ;
	int nsize = GetUser( lst, ON_LINE ) ;
	if ( nsize == 0 ) {
		return ntime ;
	}

	time_t now = share::Util::currentTimeUsec() ;

	list<_stVechile*>::iterator it ;
	for ( it = lst.begin(); it != lst.end(); ++ it ) {
		// 处理数据发送
		_stVechile *p = *it ;

		int64_t nspan = _time_span + p->last_gps_ - now ;
		if ( nspan > 0 ) {  // 计算最近的执行的时间
			if ( nspan < ntime )
				ntime = nspan ;
			continue ;
		}

		if (!(p->lgs_time_ % 3))
		  SendTransparentMsg(p,ncount,UP_CARDATA_INFO_REQ);//上传配货信息
		if (!(p->lgs_time_ % 5))
		  SendTransparentMsg(p,ncount,UP_ORDER_FORM_INFO_REQ);//上传货运订单号
		if (!(p->lgs_time_ % 7))
		  SendTransparentMsg(p,ncount,UP_TRANSPORT_FORM_INFO_REQ);//上传货运订单号
		if (!(p->lgs_time_ % 8))
		  SendTransparentMsg(p,ncount,UPLOAD_DATAINFO_REQ);//上传货运订单号

		// 发送位置信息
		if ( SendLocationPos( p , ncount ) ) {
			// 更新最后次处理的时间
			p->last_gps_  = now ;
			p->gps_count_ = 0 ;
			// 记录发送数据
			_bench.IncBench( BENCH_MSGSEND ) ;

		} else {
			++ p->gps_count_ ;
			if ( p->gps_count_ > 3 ) {
				// 如果发送三次失败则直接断连
				p->car_state_ = OFF_LINE ;
				CloseSocket( p->fd_ ) ;
			}
		}

		// 如果到指定掉线时间就让它掉线
		if ( now - _last_deluser > _deluser_span
				&& _deluser_span > 0 && _deluser_num > 0 ) {

			// 添加到掉线队列中
			p->car_state_ = OFF_LINE ;
			// 记录当前已掉线用户数
			++ _deluser_count ;

			// 如果掉用户达到当前最大能掉线用户数
			if ( _deluser_count >= _deluser_num ) {
				_last_deluser  = now ;
				_deluser_count = 0 ;
			}
			CloseSocket( p->fd_ ) ;
		}
	}

	time_t end = share::Util::currentTimeUsec() ;

	return ( ntime + now - end ) ;
}

void CVechileMgr::BuildHeader( GBheader &header, unsigned short msgid, unsigned short len , _stVechile *p )
{
	_seq_mutex.lock() ;

	header.msgid = htons(msgid) ;
	memcpy( header.car_id , p->car_id_ , sizeof(header.car_id) ) ;

	int nlen = htons( len & 0x03FF ) ;
	memcpy( &header.msgtype , &nlen , sizeof(short) ) ;

	header.seq = htons( ++ p->seq_id_ ) ;

	_seq_mutex.unlock() ;
}

// 登陆服务器
bool CVechileMgr::LoginServer(  _stVechile *pUser )
{
	if ( pUser->fd_ ) {
		CloseSocket( pUser->fd_ ) ;
	}
	if ( pUser->ufd_ ) {
		CloseSocket( pUser->ufd_ ) ;
	}

	// 如果连接模式为TCP
	if ( _connect_mode & TCP_MODE ) {
		pUser->fd_ = _tcp_handle.connect_nonb( _server_ip.c_str() , _server_port , 10 ) ;
		if ( pUser->fd_ == NULL ) {
			return false ;
		}
		if ( _connect_mode & UDP_MODE ) {  // 如果为UDP和TCP混合模式
			pUser->ufd_ = _udp_handle.connect_nonb( _server_ip.c_str() , _server_port , 10 ) ;
			if ( pUser->ufd_ ) {
				_bench.IncBench( BENCH_CONNECT, 1 ) ;
			}
		}
	}else if( _connect_mode & UDP_MODE ){ // 如果为UDP模式
		pUser->fd_ = _udp_handle.connect_nonb( _server_ip.c_str() , _server_port , 10 ) ;
	}

	if( pUser->fd_ != NULL )
	{
		_bench.IncBench( BENCH_CONNECT, 1 ) ;
		// 登陆将SOCK的数据放到里机
		pUser->fd_->_ptr = pUser ;

		unsigned short len  = sizeof(TermRegisterHeader) + sizeof(GBFooter) + strlen(pUser->carnum_);

		TermRegisterHeader  req ;

		BuildHeader( req.header, 0x0100 , (len-sizeof(GBFooter)-sizeof(GBheader)) , pUser ) ;

		req.carcolor    = pUser->carcolor ;
		req.province_id = htons( pUser->proid ) ;
		req.city_id 	= htons( pUser->cityid ) ;

		safe_memncpy( (char*)req.corp_id , pUser->termid_ , sizeof(req.corp_id ) ) ;
		safe_memncpy( (char*)req.termtype, pUser->termtype, sizeof(req.termtype ) ) ;
		safe_memncpy( (char*)req.termid ,  pUser->termid_ , sizeof(req.termid ) ) ;

		int offset = 0 ;
		char *buf = new char[len+1] ;

		memcpy( buf, &req, sizeof(req) ) ;
		offset += sizeof(req) ;

		memcpy( buf+offset, pUser->carnum_, strlen(pUser->carnum_) ) ;
		offset += strlen(pUser->carnum_) ;

		GBFooter footer ;
		memcpy( buf + offset, &footer, sizeof(footer) ) ;

		if ( Send5BData( pUser->fd_, buf, len ) ) {
			OUT_CONN( NULL , 0 , "VMGR" , "connect success,send register message, phone num: %s" ,
					BCDtostr(pUser->car_id_).c_str() ) ;
		} else {
			OUT_ERROR( NULL, 0 ,  "VMGR" , "connect success,send register message, phone num: %s" ,
					BCDtostr(pUser->car_id_).c_str() ) ;
		}

		delete [] buf ;
	}

	return ( pUser->fd_ != NULL ) ;
}

static unsigned char get_check_sum(const char *buf,int len)
{
	if(buf == NULL || len < 1)
		return 0;
	unsigned char check_sum = 0;
	for(int i = 0; i < len; i++)
	{
		check_sum ^= buf[i];
	}
	return check_sum;
}

// 发送5B数据
bool CVechileMgr::Send5BData( socket_t *sock , const char *data, const int len )
{
	char *buf = new char[len+1] ;
	memcpy( buf, data, len ) ;
	buf[len] = 0 ;
	// 处理校验码
	buf[len-2] = get_check_sum( data + 1 , len - 3 ) ;

	C7eCoder coder ;
	if ( ! coder.Encode( buf, len ) ) {
		OUT_ERROR( NULL, 0, "VMGR" , "convert code failed, fd %d , len %d" , sock->_fd, len ) ;
		delete []  buf ;
		return false ;
	}

	bool bSend = SendData( sock, coder.GetData(), coder.GetSize() ) ;

	delete [] buf ;

	return bSend ;
}

// 检测离线车辆
bool CVechileMgr::CheckOfflineUser( void )
{
	list<_stVechile*> lst ;
	int nsize = GetUser( lst, OFF_LINE ) ;
	if ( nsize == 0 ) {
		return false ;
	}

	time_t now = share::Util::currentTimeUsec() ;

	list<_stVechile*>::iterator it ;

	for ( it = lst.begin(); it != lst.end(); ++ it ) {
		// 简单遍历算法
		_stVechile *temp = *it ;
		if ( now - temp->last_conn_ < MAX_USECOND ){
			continue ;
		}
		temp->last_conn_ = now ;
		// 如果登陆不服务器就直接返回了
		if ( ! LoginServer( temp ) ) {
			return false ;
		}
	}
	return true ;
}

void CVechileMgr::SendWork()
{
	int ngps = _gps_vec.size() ;

	while( _vechile_inited ) {
		// 检测在线车辆
		int64_t ntime = CheckOnlineUser( ngps ) ;
		if ( ntime <=0 ) {
			continue ;
		}
		usleep( ntime ) ;
	}
}

//
void CVechileMgr::TimeWork()
{
	while( true ){
		// 检测是否停止线程状态
		if ( ! Check() )
			break ;

		time_t start = share::Util::currentTimeUsec() ;
		// 检测离线车辆
		while( CheckOfflineUser() ) {
		}

		int end = (int)(share::Util::currentTimeUsec() - start ) ;

		printf( "login span usecond time: %d\n" , end ) ;

		sleep(5) ;
	}
}

void CVechileMgr::run( void *param )
{
	int n = 0 ;
	memcpy( &n , &param, sizeof(int) ) ;

	switch( n )
	{
	case THREAD_TIME:
		TimeWork() ;
		break ;
	case THREAD_NOOP:
		NoopWork() ;
		break ;
	case THREAD_SEND:
		SendWork() ;
		break ;
	}
}

void CVechileMgr::NoopWork()
{
	int ngps = _gps_vec.size() ;
	if ( ngps <= 0 )
		return ;

	while( Check() ){
		_picmonitor.wait() ;
		std::list<_stVechile*>::iterator it ;
		for ( it = _piclist.begin(); it != _piclist.end(); ++ it ) {
			SendLocationPic( *it, ngps ) ;
		}
		_piclist.clear() ;
	}
}
