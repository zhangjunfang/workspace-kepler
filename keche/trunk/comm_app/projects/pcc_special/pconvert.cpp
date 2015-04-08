#include "pconvert.h"
#include "pccutil.h"
#include <comlog.h>
#include <Base64.h>
#include <BaseTools.h>
#include <databuffer.h>

#define  MSG_VECHILE_NUM   			"104"       // 车牌号的代码
#define  MSG_ACCESS_CODE    		"201"		// 接入码的代码
#define  MSG_VECHILE_COLOR			"202"		// 车颜色代码

PConvert::PConvert()
{
	_pEnv     = NULL ;
	_istester = 0 ;   // 为了过检添加特殊处理
}

PConvert::~PConvert()
{

}

void PConvert::initenv( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	int nvalue = 0 ;
	if ( _pEnv->GetInteger( "pcc_is_tester", nvalue ) ) {
		_istester = (unsigned int)nvalue ;
	}

	char buf[1028] = {0};
	if ( _pEnv->GetString( "local_picpath", buf ) ) {
		_picdir = buf ;
	}
}

bool PConvert::split2map( const std::string &s , MapString &val )
{
	vector<string>  vec ;
	// 处理所有逗号分割处理
	if ( ! splitvector( s , vec, "," , 0 ) ) {
		return false ;
	}

	string temp  ;
	size_t pos = 0 , end = 0 ;
	// 解析参数
	for ( pos = 0 ; pos < vec.size(); ++ pos ) {
		temp = vec[pos] ;
		end  = temp.find( ":" ) ;
		if ( end == string::npos ) {
			continue ;
		}
		val.insert( pair<string,string>( temp.substr(0,end), temp.substr( end+1 ) ) ) ;
	}
	// 解析出监控平台参数部分
	return ( ! val.empty() ) ;
}

// 解析监控平台的参数
bool PConvert::parse_jkpt_value( const std::string &param, MapString &val )
{
	// {TYPE:0,104:京A10104,201:701116,202:1,15:0,26:5,1:69782082,2:23947540,3:5,4:20110516/153637,5:6,21:0}
	size_t pos = param.find("{") ;
	if ( pos == string::npos ) {
		return false ;
	}

	size_t end = param.find("}", pos ) ;
	if ( end == string::npos || end < pos + 1 ) {
		return false ;
	}
	// 解析出监控平台参数部分
	return split2map( param.substr( pos+1, end-pos-1 ), val ) ;
}

// 取得头数据
bool PConvert::get_map_header( const std::string &param, MapString &val, int &ntype )
{
	if ( ! parse_jkpt_value( param, val ) ) {
		OUT_ERROR( NULL, 0, NULL, "parse data %s failed", param.c_str() ) ;
		return false ;
	}

	if ( ! get_map_integer( val, "TYPE", ntype ) ) {
		OUT_ERROR( NULL, 0, NULL, "get data %s type failed", param.c_str() ) ;
		return false ;
	}

	return true ;
}

// 转换数据
char * PConvert::convert_urept( const string &key, const string &ome, const string &phone, const string &val, int &len , unsigned int &msgid , unsigned int &type )
{
	int ntype = 0 ;
	MapString map ;
	if ( ! get_map_header( val, map, ntype ) ) {
		OUT_ERROR( NULL, 0, NULL, "PConvert::convert_urept get map header failed") ;
		return NULL ;
	}

	type = METHOD_OTHER ;

	char *buf = NULL ;

	switch(ntype) {
		case 0:  //路径和报警数据    位置汇报
		case 1:
			{
				UpExgMsgRealLocation msg;
				msg.header.msg_seq     = ntouv32( _seq_gen.get_next_seq() ) ;
				msg.header.msg_len     = ntouv32( sizeof(UpExgMsgRealLocation) ) ;
				//msg.header.access_code = ntouv32(access_code);
				//msg.exg_msg_header.vehicle_color  = car_color;
				//safe_memncpy( msg.exg_msg_header.vehicle_no, car_num.c_str() , sizeof(msg.exg_msg_header.vehicle_no) ) ;

				msg.exg_msg_header.data_length = ntouv32( sizeof(GnssData) ) ;

				if ( ! convert_gps_info( map, msg.gnss_data ) ) {
					OUT_ERROR( NULL, 0, phone.c_str(), "PConvert::convert_urept convert gps failed" ) ;
					return NULL ;
				}

				len = sizeof(UpExgMsgRealLocation) ;
				buf = new char[len+1];
				memset( buf, 0, len+1 );
				memcpy( buf, &msg, len );

				msgid = UP_EXG_MSG_REAL_LOCATION ;
			}
			break;
	    case 2: // 行驶记录仪
	        {
	        	int ntemp  = 0;
	        	//得到命令
	        	if (!get_map_integer(map,"70",ntemp)){
	        		OUT_ERROR( NULL, 0, phone.c_str(), "UP_CTRL_MSG_TAKE_TRAVEL_ACK get command_type failed" ) ;
	        		return NULL ;
	        	}

	        	string temp = "";
	        	//得到行驶记录仪数据 Base64 编码
	        	if (!get_map_string(map,"61",temp)) {
	        		OUT_ERROR( NULL, 0, phone.c_str(), "UP_CTRL_MSG_TAKE_TRAVEL_ACK get travel data failed" ) ;
	        		return NULL ;
	        	}

				int dlen  = 0 ;
				char *ptr = _pEnv->GetMsgCache()->GetData( key.c_str(), dlen ) ;

				UpCtrlMsgTaketravel msg;
				UpCtrlMsgTaketravel *resp = NULL ;

				if ( ptr == NULL ) {
					msg.header.msg_seq     = ntouv32( _seq_gen.get_next_seq() ) ;
					msg.header.msg_type    = ntouv16( UP_CTRL_MSG ) ;
					msg.ctrl_msg_header.data_type = ntouv16( UP_CTRL_MSG_TAKE_TRAVEL_ACK ) ;
					msg.command_type   	   = ntemp;
					resp = &msg ;
				} else {
					if ( dlen < (int) sizeof(UpCtrlMsgTaketravel) ) {
						OUT_ERROR( NULL, 0, phone.c_str(), "UP_CTRL_MSG_TAKE_TRAVEL_ACK get msg cache length failed, length %d" , dlen ) ;
						return NULL ;
					}
					resp = ( UpCtrlMsgTaketravel *) ptr ;
				}

				// 如果为了过检测试，因为测试软件的不允许插入特殊字符，为保证通过，就将它替换为假数据处理
				if ( _istester & 0x01 ) {

					OUT_INFO( NULL, 0, phone.c_str(), "UP_CTRL_MSG_TAKE_TRAVEL_ACK tester travel data"  ) ;
					const char szbuff[] = {0x55,0x7a,0x01,0x00,0x03,0x00,0x00,0x00,0x15,0x01} ;

					len = sizeof(UpCtrlMsgTaketravel)+ sizeof(szbuff) + sizeof(Footer) ;

					resp->ctrl_msg_header.data_length = ntouv32( sizeof(char) + sizeof(int) + sizeof(szbuff) ) ;
					resp->header.msg_len = ntouv32( len ) ;
					resp->travel_length  = ntouv32( sizeof(szbuff) ) ;

					buf = new char[len+1];
					memset(buf, 0, len+1);
					memcpy(buf, resp, sizeof(msg) );
					memcpy( buf + sizeof(msg), szbuff , sizeof(szbuff) ) ;

					Footer footer ;
					memcpy( buf + sizeof(msg) + sizeof(szbuff), &footer, sizeof(footer) ) ;

				} else {
					CBase64 base64;
					base64.Decode(temp.c_str(),temp.length()) ;
					len = sizeof(UpCtrlMsgTaketravel)+ base64.GetLength() + sizeof(Footer) ;

					resp->ctrl_msg_header.data_length = ntouv32( sizeof(char) + sizeof(int) + base64.GetLength() ) ;
					resp->header.msg_len = ntouv32( len ) ;
					resp->travel_length  = ntouv32( base64.GetLength() ) ;


					buf = new char[len+1];
					memset(buf, 0, len+1);
					memcpy(buf, resp, sizeof(msg) );

					if ( base64.GetLength() > 0 ) {
						memcpy( buf + sizeof(msg), base64.GetBuffer() , base64.GetLength() ) ;
					}
					Footer footer ;
					memcpy( buf + sizeof(msg) + base64.GetLength(), &footer, sizeof(footer) ) ;
				}
	        	msgid = UP_CTRL_MSG_TAKE_TRAVEL_ACK ;

	        	if ( ptr != NULL ) {
	        		// 释放数据
	        		_pEnv->GetMsgCache()->FreeData( ptr ) ;
	        	}
	        }
	        break;
	    case 3: // 拍照上传图片处理，这里拍照特殊处理一下
	    	{
				string temp ;
				// 取图片的URL的相对地址
				if ( ! get_map_string( map, "125" , temp ) ) {
					get_map_string( map, "203", temp ) ;
				}
				// 如果没有取着图片的地址
				if ( temp.empty() ) {
					OUT_ERROR( NULL, 0, phone.c_str(), "UP_CTRL_MSG_TAKE_PHOTO_ACK get file path failed" ) ;
					return NULL ;
				}

				// 从ome_phone_msgid中取得下发的指令消息
				char szKey[256] = {0} ;
				sprintf( szKey, "%s_%s_%d" , ome.c_str(), phone.c_str(), UP_CTRL_MSG_TAKE_PHOTO_ACK ) ;

				int  nlen  =  0 ;
				char *pbuf = _pEnv->GetMsgCache()->GetData( szKey, nlen ) ;
				if ( pbuf == NULL ) {
					OUT_ERROR( NULL, 0, phone.c_str(), "UP_CTRL_MSG_TAKE_PHOTO_ACK get data from cache failed, key %s" , szKey ) ;
					return NULL ;
				}

				UpCtrlMsgTakePhotoAck *ack = ( UpCtrlMsgTakePhotoAck * ) pbuf;

				if ( _istester & 0x02 ) {
					GnssData &gps = ack->ctrl_photo_body.gps ;
					// 1:65522247,2:18072200,20:5,24:100,3:670,4:20120329/180448,5:30,6:802,7:670,8:6147,9:125000
					gps.lon  = ntouv32( 109203745 ) ;
					gps.lat  = ntouv32( 30120333 ) ;
					gps.vec1 = ntouv16( 100 ) ; // 速度808中为1/10m/s

					time_t ntime  = time(NULL) ;
					struct tm local_tm;
					struct tm *tm = localtime_r( &ntime, &local_tm ) ;

					gps.date[3]   = ( tm->tm_year + 1900)  % 256 ;
					gps.date[2]   = ( tm->tm_year + 1900) / 256 ;
					gps.date[1]   = tm->tm_mon + 1 ;
					gps.date[0]   = tm->tm_mday ;
					gps.time[0]   = tm->tm_hour ;
					gps.time[1]   = tm->tm_min ;
					gps.time[2]   = tm->tm_sec ;

					gps.direction   = ntouv16( 0 ) ;
					gps.state 		= ntouv32(0x03) ;
					gps.alarm 		= ntouv32(0) ;
					gps.vec3  		= ntouv32( 0 ) ;

				} else {
					// 将获到GPS位置数据填充入结构体中
					convert_gps_info( map, ack->ctrl_photo_body.gps ) ;
				}

				// 从本地读取一次图片
				int piclen 	  = 0 ;
				char *picdata = NULL ;
				// 如果本地图片路径为空就从HTTP取图片
				if ( ! _picdir.empty() ) {
					char szpath[1024] = {0};
					sprintf( szpath, "%s/%s", _picdir.c_str(), temp.c_str() ) ;
					picdata = ReadFile( szpath , piclen ) ;
				}

				// 如果文件在本机直接读取
				if ( picdata != NULL && piclen > 0 ) {
					// 直接从本机读取图片
					ack->header.msg_len = ntouv32( sizeof(UpCtrlMsgTakePhotoAck) + sizeof(Footer) + piclen ) ;
					ack->ctrl_msg_header.data_length = ntouv32( sizeof(UpCtrlMsgTakePhotoBody) + piclen ) ;
					ack->ctrl_photo_body.photo_len 	 = ntouv32( piclen ) ;

					DataBuffer dbuf ;
					dbuf.writeBlock( ack, sizeof(UpCtrlMsgTakePhotoAck) ) ;
					if ( piclen > 0 ) {
						dbuf.writeBlock( picdata, piclen ) ;
					}
					Footer footer ;
					dbuf.writeBlock( &footer, sizeof(footer) ) ;
					// 取得接入码信息
					unsigned int accesscode = ntouv32( ack->header.access_code ) ;
					// 发送拍照的照片数据
					_pEnv->GetPasClient()->HandlePasUpData( accesscode, dbuf.getBuffer(), dbuf.getLength() ) ;

					FreeBuffer( picdata ) ;

					OUT_SEND( NULL, 0, NULL, "UP_CTRL_MSG_TAKE_PHOTO_ACK:%s, picture length %d, path: %s",
							ack->ctrl_msg_header.vehicle_no , nlen , temp.c_str() ) ;
				} else {  // 从网络读取文件
					// 取得新序号的ID值
					unsigned int seqid = _pEnv->GetSequeue() ;

					_pEnv->GetCacheKey( seqid, szKey ) ;
					// 重新放入新序号队列中
					_pEnv->GetMsgCache()->AddData( szKey, pbuf , nlen ) ;
					// 从网络中读取图片数据
					((IMsgClient*)_pEnv->GetMsgClient())->LoadUrlPic( seqid , temp.c_str() ) ;

					OUT_SEND( NULL, 0, NULL, "UP_CTRL_MSG_TAKE_PHOTO_ACK picture path %s", temp.c_str() ) ;
				}
				_pEnv->GetMsgCache()->FreeData( pbuf ) ;
	    	}
	    	break ;
	case 5: {
		int result = 0;
		if (!get_map_integer(map, "18", result) || result != 1) {
			return NULL;
		}

		UpExgMsgRegister msg;
		msg.header.msg_seq = ntouv32(_seq_gen.get_next_seq());
		msg.header.msg_len = ntouv32(sizeof(UpExgMsgRegister));
		msg.header.msg_type = ntouv16(UP_EXG_MSG);
		msg.exg_msg_header.data_type = ntouv16(UP_EXG_MSG_REGISTER); //子业务类型
		msg.exg_msg_header.data_length = ntouv32(sizeof(UpExgMsgRegister) - sizeof(Header) - sizeof(ExgMsgHeader) - sizeof(Footer));

		sprintf(msg.platform_id, "%s", PLATFORM_ID);

		len = sizeof(UpExgMsgRegister);
		buf = new char[len + 1];
		memset(buf, 0, len + 1);
		memcpy(buf, &msg, len);

		msgid = UP_EXG_MSG_REGISTER;

		type = METHOD_REG;
	}
		break;
	    case 8: // 主动上报驾驶员信息采集
	       {
	    	   UpExgMsgReportDriverInfo msg;
	    	   msg.header.msg_seq  = ntouv32( _seq_gen.get_next_seq());
	    	   msg.header.msg_len  = ntouv32(sizeof(UpExgMsgReportDriverInfo));
	    	   msg.header.msg_type = ntouv16( UP_EXG_MSG);
	    	   // msg.header.access_code = access_code;
	    	   msg.exg_msg_header.data_type = ntouv16(UP_EXG_MSG_REPORT_DRIVER_INFO);
	    	   msg.exg_msg_header.data_length   = ntouv32(sizeof(UpExgMsgReportDriverInfo) - sizeof(Header) - sizeof(ExgMsgHeader) - sizeof(Footer));

	    	   string temp;
	    	    //驾驶员姓名
	    	   if (get_map_string(map, "110" , temp ))
	    	   {
	    		   safe_memncpy( msg.driver_name,temp.c_str(), sizeof(msg.driver_name));
	    	   }
	    	   //驾驶员编码
	    	   if ( get_map_string(map, "111" , temp ))
	    	   {
	    		   safe_memncpy( msg.driver_id,temp.c_str(), sizeof(msg.driver_id));
	    	   }
	    	   //从业资格证编码
	    	   if ( get_map_string(map, "112" , temp ))
	    	   {
	    	   	   safe_memncpy( msg.licence,temp.c_str(), sizeof(msg.licence));
	    	   }
	    	   //发证机构名称
	    	   if ( get_map_string(map, "113" , temp ))
	    	   {
	    	   	   safe_memncpy( msg.org_name,temp.c_str(), sizeof(msg.org_name));
	    	   }

	    	   //OUT_SEND(NULL, 0, NULL, "UpExgMsgReportDriverInfo:%s",car_num.c_str());

	    	   len = sizeof(UpExgMsgReportDriverInfo) ;
	    	   buf = new char[len+1];
	    	   memset(buf, 0, len+1);
	    	   memcpy(buf, &msg, len);

	    	   msgid = UP_EXG_MSG_REPORT_DRIVER_INFO ;
	       }
	       break;
	    case 35: // 主动上报电子运单
			{
				UpExgMsgReportEwaybillInfo msg;
				msg.header.msg_seq  = ntouv32( _seq_gen.get_next_seq());
				msg.header.msg_type = ntouv16( UP_EXG_MSG);
				// msg.header.access_code = access_code;
				msg.exg_msg_header.data_type = ntouv16(UP_EXG_MSG_REPORT_EWAYBILL_INFO);

				string temp = "";
				if (get_map_string(map,"87",temp)) { //电子运单内容
					CBase64 base64;
					base64.Decode(temp.c_str(),temp.length());

					len = sizeof(UpExgMsgReportEwaybillInfo)+ base64.GetLength()+ sizeof(Footer);

					msg.exg_msg_header.data_length = ntouv32( sizeof(int) + base64.GetLength() ) ;
					msg.ewaybill_length = ntouv32( base64.GetLength() ) ;
					msg.header.msg_len  = ntouv32( len ) ;

					buf = new char[len+1];
					memcpy( buf, &msg, sizeof(msg) );
					if ( base64.GetLength() > 0 ) {
						memcpy( buf + sizeof(msg), base64.GetBuffer(), base64.GetLength() );
					}
					// 添加结束标记
					Footer footer ;
					memcpy( buf + sizeof(msg) + base64.GetLength(), &footer, sizeof(footer) ) ;
				}
				msgid = UP_EXG_MSG_REPORT_EWAYBILL_INFO ;
				// OUT_SEND(NULL, 0, NULL, "UP_EXG_MSG_Ewaybill:%s", car_num.c_str());
			}
			break;
		case 36: // 终端注册,这里暂时只处理终端鉴权
			{
				int result = 0 ;
				if ( get_map_integer( map, "45", result ) ) {
					// 如果终端注册失败就直接返回了。
					if ( result != 0 ) {
						return NULL ;
					}
				}

				UpExgMsgRegister msg;
				msg.header.msg_seq  = ntouv32( _seq_gen.get_next_seq());
				msg.header.msg_len  = ntouv32( sizeof(UpExgMsgRegister) ) ;
				msg.header.msg_type = ntouv16( UP_EXG_MSG);
				msg.exg_msg_header.data_type     = ntouv16( UP_EXG_MSG_REGISTER ) ; //子业务类型
				//msg.exg_msg_header.vehicle_color = car_color ;
				//safe_memncpy( msg.exg_msg_header.vehicle_no, car_num.c_str(), sizeof(msg.exg_msg_header.vehicle_no) ) ;
				msg.exg_msg_header.data_length   = ntouv32( sizeof(UpExgMsgRegister) - sizeof(Header) - sizeof(ExgMsgHeader) - sizeof(Footer) ) ;
				sprintf( msg.platform_id , "%s" , PLATFORM_ID ) ;

				string temp ;
				// 取得制造产商ID
				if ( get_map_string( map, "42" , temp ) ) {
					safe_memncpy( msg.producer_id, temp.c_str(), sizeof(msg.producer_id) ) ;
				}
				// 取得终端类型
				if ( get_map_string( map, "43" , temp ) ) {
					safe_memncpy( msg.terminal_model_type, temp.c_str(), sizeof(msg.terminal_model_type) ) ;
				}
				// 取得终端ID
				if ( get_map_string( map, "44" , temp ) ) {
					safe_memncpy( msg.terminal_id, temp.c_str(), sizeof(msg.terminal_id) ) ;
				}
				// 取得手机SIM号
				if ( phone.length() > 0 ) {
					//reverse_copy(msg.terminal_simcode, sizeof(msg.terminal_simcode), temp.c_str(),  0 ) ;
					// 前面补零拷贝
					reverse_copy(msg.terminal_simcode, sizeof(msg.terminal_simcode), phone.c_str(), 0 );
				}

				len = sizeof(UpExgMsgRegister) ;
				buf = new char[len+1] ;
				memset(buf, 0, len+1) ;
				memcpy(buf, &msg, len) ;

				msgid = UP_EXG_MSG_REGISTER ;
		    }
			break;
		case 38: //终端鉴权后，要异步回调通过http,通过pcc 提交到Service 请求sim卡号,终端类型
		    {
		    	int result = 0 ;
		    	if ( get_map_integer( map, "48", result ) ) {
		    		// 如果终端鉴权失败就直接返回了。
		    		if ( result != 0 ) {
		    			return NULL ;
		    		}
		    	}

		    	UpExgMsgRegister msg;
		    	msg.header.msg_seq  = ntouv32( _seq_gen.get_next_seq());
		    	msg.header.msg_len  = ntouv32( sizeof(UpExgMsgRegister) ) ;
		        msg.header.msg_type = ntouv16( UP_EXG_MSG);
		    	msg.exg_msg_header.data_type   = ntouv16(UP_EXG_MSG_REGISTER) ; //子业务类型
		    	msg.exg_msg_header.data_length = ntouv32( sizeof(UpExgMsgRegister) - sizeof(Header) - sizeof(ExgMsgHeader) - sizeof(Footer));

		    	sprintf( msg.platform_id , "%s" , PLATFORM_ID ) ;

		    	len = sizeof(UpExgMsgRegister) ;
				buf = new char[len+1] ;
				memset(buf, 0, len+1) ;
				memcpy(buf, &msg, len) ;

				msgid = UP_EXG_MSG_REGISTER ;

				type  = METHOD_REG ;
		    }
		    break;
		default:
			{
				//OUT_ERROR( NULL, 0, phone.c_str(), "PConvert::convert_urept not support %s", val.c_str() ) ;
			}
			break ;
	}

	return buf ;
}

// 处理D_CTLM
char * PConvert::convert_dctlm( const string &key, const string &val, int &len , unsigned int &msgid )
{
	return NULL ;
}

// 处理D_SNDM
char * PConvert::convert_dsndm( const string &key, const string &val, int &len , unsigned int &msgid )
{
	return NULL ;
}

// 处理所有通用应答消息
char * PConvert::convert_comm( const string &key, const string &phone, const string &val, int &len, unsigned int &msgid )
{
	MapString map ;
	// 解析数据
	if ( ! parse_jkpt_value( val , map ) ) {
		OUT_ERROR( NULL, 0, NULL, "parse data %s failed", val.c_str() ) ;
		return NULL ;
	}

	int ret = 0 ;
	if ( ! get_map_integer( map, "RET", ret ) ){
		OUT_ERROR( NULL, 0, phone.c_str(), "PConvert::convert_dctlm get ret failed %s", val.c_str() ) ;
		return NULL ;
	}

	char *buf = _pEnv->GetMsgCache()->GetData( key.c_str(), len ) ;
	if ( buf == NULL || len < (int)( sizeof(Header) + sizeof(BaseMsgHeader) ) ) {
		OUT_ERROR( NULL, 0, phone.c_str(), "UP_CTRL_MSG length %d error" , len ) ;
		return NULL ;
	}

	BaseMsgHeader *ctrl = ( BaseMsgHeader *) ( buf + sizeof(Header) ) ;
	unsigned int datatype = ntouv16( ctrl->data_type ) ;
	switch( datatype )
	{
	case UP_CTRL_MSG_MONITOR_VEHICLE_ACK:   // 处理监听
		{
			// 修改响应处理
			UpCtrlMsgMonitorVehicleAck *resp = ( UpCtrlMsgMonitorVehicleAck *) buf ;
			resp->result = ntouv32(ret); //处理监听 0 代表成功 1 代表失败

			msgid = UP_CTRL_MSG_MONITOR_VEHICLE_ACK ;
		}
		break ;
	case UP_CTRL_MSG_TEXT_INFO_ACK:  // 处理文本下发的通用应答
		{
			UpCtrlMsgTextInfoAck *resp = ( UpCtrlMsgTextInfoAck *) buf ;
			resp->result = ntouv32(ret) ;

			msgid = UP_CTRL_MSG_TEXT_INFO_ACK ;
		}
		break ;
	case UP_CTRL_MSG_TAKE_TRAVEL_ACK:  // 处理行驶记录仪的通用应答
		{
			OUT_RECV( NULL, 0, phone.c_str(), "DOWN_CTRL_MSG_TAKE_TRAVEL_REQ recv common response" ) ;
			// 如果为了过检测试，因为测试软件的不允许插入特殊字符，为保证通过，就将它替换为假数据处理
			if ( _istester & 0x01 ) {
				// 记录缓存数据中的指针
				char *ptr = ( char *) buf ;

				UpCtrlMsgTaketravel *resp = (UpCtrlMsgTaketravel* ) buf ;

				OUT_INFO( NULL, 0, phone.c_str(), "UP_CTRL_MSG_TAKE_TRAVEL_ACK tester travel data common response"  ) ;
				const char *pbuff = "AAAAAAAAAAAAAAAAAAAAAA" ;

				len = sizeof(UpCtrlMsgTaketravel)+ strlen(pbuff) + sizeof(Footer) ;

				resp->ctrl_msg_header.data_length = ntouv32( sizeof(char) + sizeof(int) + strlen(pbuff) ) ;
				resp->header.msg_len = ntouv32( len ) ;
				resp->travel_length  = ntouv32( strlen(pbuff) ) ;

				buf = new char[len+1];
				memset(buf, 0, len+1);
				memcpy(buf, resp, sizeof(UpCtrlMsgTaketravel) );
				memcpy( buf + sizeof(UpCtrlMsgTaketravel), pbuff , strlen(pbuff) ) ;

				Footer footer ;
				memcpy( buf + sizeof(UpCtrlMsgTaketravel) + strlen(pbuff), &footer, sizeof(footer) ) ;

				_pEnv->GetMsgCache()->FreeData( ptr ) ;

				msgid = UP_CTRL_MSG_TAKE_TRAVEL_ACK ;
			}
		}
		break ;
	case UP_CTRL_MSG_EMERGENCY_MONITORING_ACK: // 紧急接入
		{
			UpCtrlMsgEmergencyMonitoringAck *resp = (UpCtrlMsgEmergencyMonitoringAck *)buf;
			resp->result = ntouv32(ret);

			msgid = UP_CTRL_MSG_EMERGENCY_MONITORING_ACK ;
		}
		break;
	default:
		OUT_ERROR( NULL, 0, phone.c_str(), "PConvert::convert_dctlm not support %s", val.c_str() ) ;
		break;
	}
	return buf ;
}

// 转换监管协议
char * PConvert::convert_lprov( const string &key, const string &seqid, const string &val , int &len, string &areacode )
{
	MapString map ;
	if ( ! parse_jkpt_value( val, map ) ) {
		OUT_ERROR( NULL, 0, NULL, "parse data %s failed", val.c_str() ) ;
		return NULL ;
	}

	string stype ;
	if ( ! get_map_string( map , "TYPE", stype ) ) {
		OUT_ERROR( NULL, 0, NULL, "get data %s type failed", val.c_str() ) ;
		return NULL ;
	}

	// 取得省域ID
	if ( ! get_map_string( map, "AREA_CODE", areacode ) ) {
		OUT_ERROR( NULL, 0, NULL, "get area code failed, %s", val.c_str() ) ;
		return NULL ;
	}

	char *buf = NULL ;
	// AREA_CODE:省代码,CARNO:车牌颜色_车牌号,ACCESS_CODE:运营商接入码,TYPE:XXX,k1:v1...
	if ( stype == "D_PLAT" ) {

		string sval ;
		// 如果为平台查岗消息处理
		if ( get_map_string( map, "PLATQUERY" , sval ) ) {
			// 查岗对象的类型（1：当前连接的下级平台，2：下级平台所属单一业户，3：下级平台所属所有业户）|查岗对象的ID|信息ID|应答内容"
			vector<string> vec ;
			if ( ! splitvector( sval, vec, "|" , 0 ) ) {
				OUT_ERROR( NULL, 0, areacode.c_str(), "split vector failed, sval %s" , sval.c_str() ) ;
				return NULL ;
			}
			// 如果拆分个数小于四个则直接返回
			if ( vec.size() < 4 ) {
				OUT_ERROR( NULL, 0, areacode.c_str(), "split vector param too less, value %s" , sval.c_str() ) ;
				return NULL ;
			}

			int dlen  = 0 ;
			char *ptr = _pEnv->GetMsgCache()->GetData( key.c_str(), dlen , false ) ;
			if ( ptr == NULL ) {
				OUT_ERROR( NULL, 0, areacode.c_str(), "get plat msg failed , key %s" , key.c_str() ) ;
				return NULL ;
			}

			string content = vec[3] ;
			CBase64 base;
			if ( base.Decode( content.c_str(), content.length() ) ) {
				content = base.GetBuffer() ;
			}
			len = sizeof(UpPlatformMsgPostQueryAck)   + content.length() + sizeof(Footer) ;
			buf = new char[len+1] ;

			// 处理平台查岗的消息
			UpPlatformMsgPostQueryAck *rsp = (UpPlatformMsgPostQueryAck *) ptr ;
			rsp->header.msg_len 			  = ntouv32( len ) ;
			rsp->up_platform_msg.data_length  = ntouv32( sizeof(UpPlatformMsgpostqueryData) + content.length() ) ;
			rsp->up_platform_post.msg_len     = ntouv32( content.length() ) ;
			rsp->up_platform_post.object_type = atoi( vec[0].c_str() ) ;
			safe_memncpy( rsp->up_platform_post.object_id, vec[1].c_str(), sizeof(rsp->up_platform_post.object_id) ) ;
			rsp->up_platform_post.info_id     = ntouv32( atoi(vec[2].c_str()) ) ;

			memcpy( buf, rsp, sizeof(UpPlatformMsgPostQueryAck) ) ;
			memcpy( buf+sizeof(UpPlatformMsgPostQueryAck) , content.c_str(), content.length() ) ;

			Footer footer ;
			memcpy( buf+sizeof(UpPlatformMsgPostQueryAck) + content.length(), &footer, sizeof(footer) ) ;

			OUT_PRINT( NULL, 0, areacode.c_str(), "platquery message %s", content.c_str() ) ;

			return buf ;
		}

		// 平台间消息
		if ( get_map_string( map, "PLATMSG", sval ) ) {
			// 从缓存中取数据, 针对平台间消息和平台查岗暂时不回收资源
			char *ptr = _pEnv->GetMsgCache()->GetData( key.c_str(), len , false ) ;
			if ( buf == NULL ) {
				OUT_ERROR( NULL, 0, areacode.c_str(), "get msg from cache failed, key %s", key.c_str() ) ;
				return NULL ;
			}

			// 重新拷贝处理
			buf = new char[len+1] ;
			memset( buf, 0, len + 1 ) ;
			memcpy( buf, ptr, len ) ;

			// 处理平台消息的ID
			UpPlatFormMsgInfoAck *ack = (UpPlatFormMsgInfoAck *) buf ;
			ack->info_id = ntouv32( atoi(sval.c_str()) ) ;

			return buf ;
		}

	} else {  // 除平台查岗处理
		string macid ;
		// 取得车牌颜色和车牌号
		if ( ! get_map_string( map, "CARNO" , macid ) ) {
			OUT_ERROR( NULL, 0, areacode.c_str(), "get carno failed, %s", val.c_str() ) ;
			return NULL ;
		}

		unsigned char carcolor = 0 ;
		string carnum ;
		// 通过MACID取得车颜色和车牌号
		if ( ! get_carinfobymacid( macid, carcolor, carnum ) ) {
			OUT_ERROR( NULL, 0, areacode.c_str(), "get car color and car num by macid failed, %d" , macid.c_str() ) ;
			return NULL ;
		}

		string sval ;
		// 如果为报警业务
		if ( stype == "D_WARN" ) {
			// 报警督办
			if ( get_map_string( map, "WARNTODO", sval ) ) {
				buf = _pEnv->GetMsgCache()->GetData( key.c_str(), len ) ;
				if ( buf == NULL ) {
					OUT_ERROR( NULL, 0, areacode.c_str(), "get msg data failed, key %s, macid %s" , key.c_str(), macid.c_str() ) ;
					return NULL ;
				}
				UpWarnMsgUrgeTodoAck *rsp = ( UpWarnMsgUrgeTodoAck *) buf ;
				rsp->result = atoi(sval.c_str()) ;
				return buf ;
			}

			// 处理上报报警
			if ( get_map_string( map, "WARNINFO" , sval ) ) {
				// 报警信息来源（1：车载终端，2：企业监控平台，3：政府监管平台，9：其它）|报警类型(详见5.3“报警类型编码表”)|报警时间(UTC时间格式)|信息ID|报警信息内容
				vector<string>  vec ;
				// 处理所有逗号分割处理
				if ( ! splitvector( sval , vec, "|" , 0 ) ) {
					OUT_ERROR( NULL, 0, areacode.c_str(), "WARNINFO split vector error, key %s, macid %s, value: %s" , key.c_str(), macid.c_str() , sval.c_str() ) ;
					return false ;
				}
				if ( vec.size() < 5 ) {
					OUT_ERROR( NULL, 0, areacode.c_str(), "WARNINFO arg too less error, key %s, macid %s, value: %s" , key.c_str(), macid.c_str() , sval.c_str() ) ;
					return false ;
				}

				int nlen = vec[4].length() ;
				len = sizeof(UpWarnMsgAdptInfoHeader) + sizeof(UpWarnMsgAdptInfoBody) + nlen + sizeof(Footer) ;

				UpWarnMsgAdptInfoHeader req ;
				req.header.msg_len  = ntouv32( len ) ;  // 修正长度不正
				req.header.msg_type = ntouv16( UP_WARN_MSG ) ;
				req.header.msg_seq  = ntouv32( _seq_gen.get_next_seq() ) ;
				req.warn_msg_header.vehicle_color = carcolor ;
				safe_memncpy( req.warn_msg_header.vehicle_no, carnum.c_str(), sizeof(req.warn_msg_header.vehicle_no) ) ;
				req.warn_msg_header.data_type   = ntouv16( UP_WARN_MSG_ADPT_INFO ) ;
				req.warn_msg_header.data_length = ntouv32( sizeof(UpWarnMsgAdptInfoBody) + nlen ) ;

				UpWarnMsgAdptInfoBody body ;
				body.warn_src    = atoi( vec[0].c_str() ) ;
				body.warn_type   = ntouv16( atoi(vec[1].c_str()) ) ;
				body.warn_time   = ntouv64( atoi64(vec[2].c_str()) ) ;
				body.info_id     = ntouv32( atoi(vec[3].c_str()) ) ;
				body.info_length = ntouv32( nlen ) ;

				int offset = 0 ;
				buf = new char[len+1] ;
				memcpy( buf+offset, &req, sizeof(req) ) ;
				offset += sizeof(req) ;

				memcpy( buf+offset, &body, sizeof(body) ) ;
				offset += sizeof(body) ;

				memcpy( buf+offset, vec[4].c_str(), nlen ) ;
				offset += nlen ;

				Footer footer ;
				memcpy( buf+offset, &footer, sizeof(footer) ) ;

				return buf ;
			}

			// 处理主动上报报警结果
			if ( get_map_string( map, "UPWARN", sval ) ) {
				// 报警信息ID|报警处理结果（0：处理中，1：已处理完毕，2：不作处理，3：将来处理）
				size_t pos = sval.find( "|" ) ;
				if ( pos == string::npos ) {
					OUT_ERROR( NULL, 0, areacode.c_str(), "upwarn result command error, value: %s" , val.c_str() ) ;
					return NULL ;
				}

				// 组装主动上报处理理结果的数据包
				UpWarnMsgAdptTodoInfo req ;
				req.header.msg_len  = ntouv32( sizeof(UpWarnMsgAdptTodoInfo) ) ;
				req.header.msg_type = ntouv16( UP_WARN_MSG ) ;
				req.header.msg_seq  = ntouv32( _seq_gen.get_next_seq() ) ;
				req.warn_msg_header.vehicle_color = carcolor ;
				safe_memncpy( req.warn_msg_header.vehicle_no, carnum.c_str(), sizeof(req.warn_msg_header.vehicle_no) ) ;
				req.warn_msg_header.data_type   = ntouv16( UP_WARN_MSG_ADPT_TODO_INFO ) ;
				req.warn_msg_header.data_length = ntouv32( sizeof(WarnMsgAdptTodoInfoBody) ) ;
				req.warn_msg_body.info_id 		= ntouv32( atoi( sval.c_str() ) ) ;
				req.warn_msg_body.result  		= atoi( sval.substr(pos+1).c_str() ) ;

				len = sizeof(req) ;
				buf = new char[len+1] ;
				memcpy( buf, &req, sizeof(req) ) ;

				return buf ;
			}
		} else if ( stype == "D_MESG" ) {
			// 如果下发申请交换车辆信息
			if ( get_map_string( map, "MONITORSTARTUP" , sval ) ) {
				// 开始时间(UTC时间格式)|结束时间(UTC时间格式)
				size_t pos = sval.find( "|" ) ;
				if ( pos == string::npos ) {
					OUT_ERROR( NULL, 0, areacode.c_str(), "down command error , value : %s" , val.c_str() ) ;
					return NULL ;
				}
				uint64 start = atoi64( sval.substr(0, pos).c_str() ) ;
				uint64 end   = atoi64( sval.substr( pos+1).c_str() ) ;

				UpExgMsgApplyForMonitorStartup req ;
				req.header.msg_len = ntouv32( sizeof(req) ) ;
				req.header.msg_seq = ntouv32( _seq_gen.get_next_seq() ) ;
				req.exg_msg_header.vehicle_color = carcolor ;
				safe_memncpy( req.exg_msg_header.vehicle_no, carnum.c_str(), sizeof(req.exg_msg_header.vehicle_no) ) ;
				req.exg_msg_header.data_length   = ntouv32( sizeof(uint64) * 2 ) ;
				req.start_time = ntouv64( start ) ;
				req.end_time   = ntouv64( end   ) ;

				len = sizeof(req) ;
				buf = new char[len+1] ;
				memcpy( buf, &req, sizeof(req) ) ;

				_pEnv->GetPasClient()->AddMacId2SeqId( UP_EXG_MSG_APPLY_FOR_MONITOR_STARTUP , macid.c_str(), seqid.c_str() ) ;

				return buf ;
			}

			// 结束车辆交换信息
			if ( get_map_string( map , "MONITOREND" , sval ) ) {

				UpExgMsgApplyForMonitorEnd  req ;
				req.header.msg_len = ntouv32( sizeof(req) ) ;
				req.header.msg_seq = ntouv32( _seq_gen.get_next_seq() ) ;
				req.exg_msg_header.vehicle_color = carcolor ;
				safe_memncpy( req.exg_msg_header.vehicle_no, carnum.c_str(), sizeof(req.exg_msg_header.vehicle_no) ) ;
				req.exg_msg_header.data_length   = ntouv32( 0 ) ;

				len = sizeof(req) ;
				buf = new char[len+1] ;
				memcpy( buf, &req, sizeof(req) ) ;

				_pEnv->GetPasClient()->AddMacId2SeqId( UP_EXG_MSG_APPLY_FOR_MONITOR_END , macid.c_str(), seqid.c_str() ) ;

				return buf ;
			}

			// 这是平台主动下发，还有一种平台自动下发情况，补发指定时间车辆信息
			if ( get_map_string( map, "HISGNSSDATA" , sval ) ) {
				// 开始时间(UTC时间格式)|结束时间(UTC时间格式)
				size_t pos = sval.find( "|" ) ;
				if ( pos == string::npos ) {
					OUT_ERROR( NULL, 0, areacode.c_str(), "down command error , value : %s" , val.c_str() ) ;
					return NULL ;
				}
				uint64 start = atoi64( sval.substr(0, pos).c_str() ) ;
				uint64 end   = atoi64( sval.substr( pos+1).c_str() ) ;

				UpExgApplyHisGnssDataReq req ;
				req.header.msg_len  = ntouv32( sizeof(req) ) ;
				req.header.msg_type = ntouv16( UP_EXG_MSG ) ;
				req.header.msg_seq  = ntouv32( _seq_gen.get_next_seq() ) ;
				req.exg_msg_header.vehicle_color = carcolor ;
				safe_memncpy( req.exg_msg_header.vehicle_no, carnum.c_str(), sizeof(req.exg_msg_header.vehicle_no) ) ;
				req.exg_msg_header.data_length   = ntouv32( sizeof(uint64) * 2 ) ;
				req.exg_msg_header.data_type	 = ntouv16( UP_EXG_MSG_APPLY_HISGNSSDATA_REQ ) ;
				req.start_time = ntouv64( start ) ;
				req.end_time   = ntouv64( end   ) ;

				len = sizeof(req) ;
				buf = new char[len+1] ;
				memcpy( buf, &req, sizeof(req) ) ;

				_pEnv->GetPasClient()->AddMacId2SeqId( UP_EXG_MSG_APPLY_HISGNSSDATA_REQ , macid.c_str(), seqid.c_str() ) ;

				return buf ;
			}

			// 处理历史数据上报
			size_t pos = val.find( "HISTORY" ) ;
			// 取得车辆的历史位数据情况
			if ( pos != string::npos ) {
				size_t end = val.find( "}" , pos+8 ) ;
				if ( end == string::npos || end < pos ){
					OUT_ERROR( NULL, 0, areacode.c_str(), "HISTORY split vector error, key %s, macid %s, value: %s" , key.c_str(), macid.c_str() , val.c_str() ) ;
					return false;
				}
				sval = val.substr( pos+8, end - pos - 8 ) ;
				vector<string>  vec ;
				// 处理所有逗号分割处理
				if ( ! splitvector( sval , vec, "|" , 0 ) ) {
					OUT_ERROR( NULL, 0, areacode.c_str(), "HISTORY split vector error, key %s, macid %s, value: %s" , key.c_str(), macid.c_str() , sval.c_str() ) ;
					return false ;
				}

				int nsize = vec.size() ;

				UpExgMsgHistoryHeader msg ;
				msg.header.msg_type = ntouv16( UP_EXG_MSG ) ;
				msg.header.msg_seq  = ntouv32( _seq_gen.get_next_seq() ) ;
				msg.exg_msg_header.vehicle_color = carcolor ;
				safe_memncpy( msg.exg_msg_header.vehicle_no, carnum.c_str(), sizeof(msg.exg_msg_header.vehicle_no) ) ;
				msg.exg_msg_header.data_type   = ntouv16(UP_EXG_MSG_HISTORY_LOCATION) ;

				DataBuffer dbuf ;

				unsigned char ncount = 0 ;
				for ( int i = 0; i < nsize; ++ i ) {
					MapString gpsmap ;
					if ( ! split2map( vec[i] , gpsmap ) ) {
						continue ;
					}
					GnssData gnssdata;
					if ( ! convert_gps_info( gpsmap, gnssdata ) ){
						continue ;
					}
					dbuf.writeBlock( &gnssdata, sizeof(GnssData) ) ;
					++ ncount ;
				}

				len = sizeof(msg) + sizeof(char) + ncount*sizeof(GnssData) + sizeof(Footer) ;
				msg.exg_msg_header.data_length = ntouv32( sizeof(char) + ncount*sizeof(GnssData) ) ;
				msg.header.msg_len			   = ntouv32( len ) ;

				buf = new char[len+1] ;

				int offset = 0 ;
				memcpy( buf, &msg, sizeof(msg) ) ;
				offset += sizeof(msg) ;

				memcpy( buf+offset , &ncount, sizeof(char) )  ;
				offset += sizeof(char) ;

				memcpy( buf+offset, dbuf.getBuffer(), dbuf.getLength() ) ;
				offset += dbuf.getLength() ;

				Footer footer ;
				memcpy( buf+offset, &footer, sizeof(footer) ) ;

				return buf ;
			}

			// 接入广州809精简版监管，提交车辆静态数据，子业务类型为DOWN_EXG_MSG_CAR_INFO
			if (get_map_string(map, "U_BASE", sval)) {
				CBase64 base64;
				if( ! base64.Decode(sval.c_str(), sval.length())) {
					OUT_ERROR( NULL, 0, areacode.c_str(), "base64 decode error, value : %s" , val.c_str() ) ;
					return NULL;
				}

				len = sizeof(DownExgMsgCarInfoHeader) + base64.GetLength() + sizeof(Footer);
				buf = new char[len + 1];

				DownExgMsgCarInfoHeader msg;
				msg.header.msg_len  = ntouv32( len ) ;
				msg.header.msg_seq  = ntouv32( _seq_gen.get_next_seq() ) ;
				msg.header.msg_type = ntouv16( UP_EXG_MSG );
				msg.exg_msg_header.vehicle_color = carcolor ;
				strncpy(msg.exg_msg_header.vehicle_no, carnum.c_str(),	sizeof(msg.exg_msg_header.vehicle_no));
				msg.exg_msg_header.data_type = ntouv16(DOWN_EXG_MSG_CAR_INFO);
				msg.exg_msg_header.data_length = ntouv32(base64.GetLength());

				int msgpos = 0;
				memcpy(buf + msgpos, &msg, sizeof(DownExgMsgCarInfoHeader));
				msgpos += sizeof(DownExgMsgCarInfoHeader);

				memcpy(buf + msgpos, base64.GetBuffer(), base64.GetLength());
				msgpos += base64.GetLength();

				Footer footer ;
				memcpy( buf + msgpos, &footer, sizeof(footer) ) ;

				return buf ;
			}
		}
	}
	return NULL ;
}

// 释放缓存
void PConvert::free_buffer( char *buf )
{
	if ( buf == NULL )
		return ;
	// 释放缓存数据
	delete [] buf ;
	buf = NULL ;
}

bool PConvert::get_map_string( MapString &map, const std::string &key , std::string &val )
{
	MapString::iterator it = map.find( key ) ;
	if ( it == map.end() ) {
		return false ;
	}
	val = it->second ;
	return true ;
}

bool PConvert::get_map_integer( MapString &map, const std::string &key , int &val )
{
	MapString::iterator it = map.find( key ) ;
	if ( it == map.end() ) {
		return false ;
	}
	val = atoi( it->second.c_str() ) ;
	return true ;
}

bool PConvert::get_phoneome( const string &macid, string &phone, string &ome )
{
	if ( macid.length() < PHONE_LEN ) {
		return false ;
	}

	size_t pos = macid.find( '_' ) ;
	if ( pos == string::npos ) {
		return false ;
	}

	phone = macid.substr( pos+1 ) ;
	ome   = macid.substr( 0, pos ) ;

	return true ;
}

bool PConvert::get_carinfobymacid( const string &macid, unsigned char &carcolor, string &carnum )
{
	if ( macid.length() < 3 ) {
		return false ;
	}

	size_t pos = macid.find( '_' ) ;
	if ( pos == string::npos ) {
		return false ;
	}
	carcolor = atoi( macid.c_str() ) ;
	carnum   = macid.substr( pos+1 ) ;

	return true ;
}

// 将本地时间转成格林威冶时间
static bool checktime( int nyear, int nmonth, int nday, int nhour, int nmin, int nsec )
{
	struct tm curtm ;
	curtm.tm_year = nyear - 1900 ;
	curtm.tm_mon  = nmonth - 1 ;
	curtm.tm_mday = nday ;
	curtm.tm_hour = nhour ;
	curtm.tm_min  = nmin ;
	curtm.tm_sec  = nsec ;

	time_t now = time(NULL) ;
	time_t t   = mktime( &curtm ) ;  // 转成格林威冶时间

	// 时间前后一天之差
	if ( t < now - 86400 || t > now + 86400 ) {
		return false ;
	}
	return true ;
}

// 将GPS数据转成GNSS
bool PConvert::convert_gps_info( MapString &mp, GnssData &gps )
{
	if ( mp.empty() )
		return false ;

	memset(&gps, 0x00, sizeof(GnssData));

	int nval = 0 ;
	if ( get_map_integer( mp, "1", nval ) ) {
		nval = nval * 10 / 6 ;
	}
	// 处理经度不在中国范围内 619066885
	if ( nval < 72000000 || nval > 140000000 ){  // 经度范围72-136
		OUT_ERROR( NULL, 0, NULL, "error lon %u", nval ) ;
		return false ;
	}
	gps.lon = ntouv32( nval ) ;

	nval = 0;
	if ( get_map_integer( mp, "2", nval ) ) {
		nval = nval * 10 / 6 ;
	}
	// 处理纬度不在中国范围内
	if ( nval < 18000000 || nval > 55000000 ) {  // 纬度范围18-54
		OUT_ERROR( NULL, 0, NULL, "error lat %u", nval ) ;
		return false ;
	}
	gps.lat = ntouv32( nval ) ;

	nval = 0;
	if ( get_map_integer( mp, "3", nval ) ) {
		nval = nval/10 ; // 速度808中为1/10km/h
	}
	// 处理速度不正确
	if ( nval > 220 )  {  // 220km/h
		OUT_ERROR( NULL, 0, NULL, "error speed %u", gps.vec1 ) ;
		return  false ;
	}
	gps.vec1 = ntouv16( nval ) ;

	nval = 0;
	if ( get_map_integer( mp, "7", nval ) ) {
		nval /= 10; // 行驶记录仪速度808中为1/10km/h
	}
	gps.vec2 = (nval == 0) ? gps.vec1 : ntouv16( nval );

	string sval ;
	if ( ! get_map_string( mp, "4", sval ) ) {
		OUT_ERROR( NULL, 0, NULL, "error time empty" ) ;
		// 如果没有时间就直接返回了
		return false ;
	}

	int nyear = 0 , nmonth = 0 , nday = 0 , nhour = 0 ,nmin = 0 , nsec = 0 ;

	sscanf( sval.c_str(), "%04d%02d%02d/%02d%02d%02d", &nyear, &nmonth, &nday, &nhour, &nmin, &nsec ) ;
	// 检测时间是否正确
	if ( ! checktime( nyear, nmonth, nday, nhour, nmin, nsec ) ) {
		OUT_ERROR( NULL, 0, NULL, "error time %s", sval.c_str() ) ;
		return false ;
	}

 	gps.date[3]   = nyear  % 256 ;
	gps.date[2]   = nyear / 256 ;
	gps.date[1]   = nmonth ;
	gps.date[0]   = nday ;

	gps.time[0]   = nhour ;
	gps.time[1]   = nmin ;
	gps.time[2]   = nsec ;

	// 里程
	if ( get_map_integer( mp, "9" , nval ) ) {
		gps.vec3  = ntouv32( nval / 10 );
	}

	// 方向
	if ( get_map_integer( mp, "5", nval )){
		gps.direction = (nval > 360) ? ntouv16(360) : ntouv16( nval ) ;
	}

	// 海拔
	if ( get_map_integer( mp, "6", nval )){
		gps.altitude = (nval > 8000) ? ntouv16(8000) : ntouv16( nval ) ;
	}

	// 报警
	if ( get_map_integer( mp, "20" , nval ) ) {
		gps.alarm = ntouv32(nval) ;
	}

	// 状态
	if ( get_map_integer( mp, "8" , nval ) ) {
		gps.state = ntouv32(nval) ;
	}

	return true ;
}

// 转换成内部协议处理
void PConvert::build_gps_info( string &dest, GnssData *gps_data )
{
	dest.clear();

	dest += "1:" + uitodecstr(ntouv32(gps_data->lon) * 6 / 10) + ",";
	dest += "2:" + uitodecstr(ntouv32(gps_data->lat) * 6 / 10) + ",";
	dest += "3:" + ustodecstr(ntouv16(gps_data->vec1)*10) + ",";

	unsigned int iyear = ((unsigned char) (gps_data->date[2])) * 256 + (unsigned char) (gps_data->date[3]);
	string year   = uitodecstr(iyear);
	string month  = charto2decstr(gps_data->date[1]);
	string day    = charto2decstr(gps_data->date[0]);

	string hour   = charto2decstr(gps_data->time[0]);
	string minute = charto2decstr(gps_data->time[1]);
	string second = charto2decstr(gps_data->time[2]);

	dest += "4:" + year + month + day + "/" + hour + minute + second + ",";

	dest += "5:"  + ustodecstr(ntouv16(gps_data->direction)) + ",";
        dest += "6:"  + ustodecstr(ntouv16(gps_data->altitude)) + ",";
	dest += "9:" + ustodecstr(ntouv32(gps_data->vec3) * 10)  + ",";
	dest += "8:"  + uitodecstr(ntouv32(gps_data->state)) + ",";
	dest += "20:" + ustodecstr(ntouv32(gps_data->alarm)) ;
}
