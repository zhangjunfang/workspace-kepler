/**********************************************
 * InterProtoConvert.h
 *
 *  Created on: 2010-7-17
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments: 2011-06-15 humingqing 重写此对象
 *********************************************/

#ifndef INTERPROTOCONVERT_H_
#define INTERPROTOCONVERT_H_

#include "ProtoHeader.h"
#include "ProtoParse.h"
#include <vector>
#include <tools.h>
#include <map>
#include <Mutex.h>

#define DEFAULT_SEQ 		"000000_0000000000_0"
#define DEFAULT_MAC_ID 		"0_00000000"
#define MAS_ACCESS_CODE 	"00000000"

#define IMG_JPG   			0x01
#define IMG_GIF   			0x02
#define IMG_TIFF  			0x03

// 带有超时和锁处理的MAP队列，需要不需要处理超时则需要根据是否为闭合的系统，
// 如果输入和输出肯定相等则为闭合
class CReqMap
{
	struct _SEQ_DATA
	{
		string _seq ;
		time_t _now ;
	};
public:
	CReqMap(bool bIndex=false):_seq_count(0) {
		_bindex = bIndex ;
	} ;
	~CReqMap(){
		ClearData() ;
	} ;

	// 添加无符号整数的序列值
	void AddNumReqMap( const unsigned int key, const string &val ){
		char buf[128] = {0} ;
		sprintf( buf, "%d", key ) ;
		AddReqMap( buf, val ) ;
	}

	// 添加字符串型值
	void AddReqMap( const string &key, const string &val )
	{
#ifdef _XDEBUG
		printf( "Start add key %s value %s\n", key.c_str(), val.c_str() ) ;
#endif
		_map_mutex.lock() ;
		map<string,_SEQ_DATA*>::iterator it = _map_seq.find(key) ;
		if ( it != _map_seq.end() ){
			_SEQ_DATA *p = it->second ;
			RemoveIndex( p->_seq, p->_now ) ;
			p->_now = time(NULL) ;
			p->_seq = val ;
			AddIndex( p->_seq, p->_now ) ;
		}else{
			_SEQ_DATA *p = new _SEQ_DATA;
			p->_now = time(NULL) ;
			p->_seq = val ;
			_map_seq.insert( pair<string,_SEQ_DATA*>( key, p ) ) ;
			AddIndex( p->_seq, p->_now ) ;
		}
		_map_mutex.unlock() ;
#ifdef _XDEBUG
		printf( "Add map key %s value %s\n", key.c_str(), val.c_str() ) ;
#endif
	}

	bool RemoveNumKey( const unsigned int key ) {
		char buf[128] = {0} ;
		sprintf( buf, "%d", key ) ;
		return RemoveKey( buf ) ;
	}

	bool RemoveKey( const string &key ){
		string val;
		return FindReqMap( key, val, true ) ;
	}

	void ClearKey( const unsigned int timeout ){
		// 如果不需要索引，它属于闭合处理则不需要超时处理
		if ( ! _bindex ) return ;

		time_t now = time(NULL) ;
		_map_mutex.lock() ;
		if ( _map_index.empty() ){
			// 如果为空则直接返回了
			_map_mutex.unlock() ;
			return ;
		}

		// 这里使用时间做为索引MAP具有自动排序的功能这样早的时间就自然会在前面，因为MAP遍历比较慢
		map<string,_SEQ_DATA*>::iterator itx;
		multimap<time_t,string>::iterator it ,itmp;
		for ( it = _map_index.begin(); it != _map_index.end(); ){
			if ( now - it->first < timeout ){
				break ;
			}
			itmp = it ;
			++ it ;

			// 移除超时的数据
			itx = _map_seq.find( itmp->second ) ;
			if ( itx != _map_seq.end() ){
				delete itx->second ;
				_map_seq.erase( itx ) ;
			}

			_map_index.erase( itmp ) ;
		}
		_map_mutex.unlock() ;
	}

	bool FindNumReqMap( const unsigned int key, string &val , bool berase ){
		char buf[128] = {0} ;
		sprintf( buf, "%d", key ) ;
		return FindReqMap( buf , val , berase ) ;
	}

	bool FindReqMap( const string &key, string &val , bool berase )
	{
#ifdef _XDEBUG
		printf( "start find key %s\n", key.c_str() ) ;
#endif
		bool bFind = false ;
		_map_mutex.lock() ;
		if ( _map_seq.empty() ){
			_map_mutex.unlock() ;
			return false ;
		}
		map<string, _SEQ_DATA*>::iterator it = _map_seq.find(key);
		if (it != _map_seq.end()){
			_SEQ_DATA *p = it->second ;
			val = p->_seq ;
			if ( berase ){
				// 移除索引
				RemoveIndex( p->_seq, p->_now ) ;
				// 移动原数据
				_map_seq.erase( it ) ;
				// 回收内存
				delete p ;
			}
			bFind = true;
		}
		_map_mutex.unlock() ;
#ifdef _XDEBUG
		printf( "find key %s val %s\n", key.c_str(), val.c_str() ) ;
#endif
		return bFind;
	}

	unsigned int GetSequeue( void ) {
		// 需要加锁序列化处理
		_map_mutex.lock() ;
		if (_seq_count > 1000 * 10000)
			_seq_count = 0;
		++ _seq_count ;
		_map_mutex.unlock() ;
		return _seq_count;
	}

private:
	void AddIndex( const string &key, const time_t now ){
		if( !_bindex ) return ;
		// 添加时间索引
		_map_index.insert(pair<time_t,string>( now, key ) ) ;
	}
	void RemoveIndex( const string &key, const time_t now ){
		if ( !_bindex )
			return ;
		multimap<time_t,string>::iterator it = _map_index.find(now) ;
		if ( it == _map_index.end() ){
			return  ;
		}
		for ( ; it != _map_index.end(); ){
			if ( it->first != now ) {
				break ;
			}
			if ( it->second == key ){
				_map_index.erase( it ) ;
				break ;
			}
			++ it ;
		}
	}
	void ClearData( void ) {
		_map_mutex.lock() ;
		if ( _map_seq.empty() ){
			_map_mutex.unlock() ;
			return ;
		}
		map<string, _SEQ_DATA*>::iterator it ;
		for ( it = _map_seq.begin(); it != _map_seq.end(); ++ it ){
			delete it->second ;
		}
		_map_seq.clear() ;
		_map_index.clear() ;
		_map_mutex.unlock() ;
	}

private:
	share::Mutex 		 	 _map_mutex ;
	map<string,_SEQ_DATA*> 	 _map_seq ;    // MAP值
	multimap<time_t,string>  _map_index ;  // 时间索引
	unsigned  int			 _seq_count ;  // 序列值生成器
	bool 					 _bindex ;     // 是否需要索引
};

class InterProtoConvert
{
public:
	InterProtoConvert() ;
	virtual ~InterProtoConvert() ;
	// 构建内部的GPS头部
	bool build_inter_header(string &destheader, const string &header, const string &seq, const string &mac_id,
			const string &command, const string &com_access_code );
	// 构建内部的GPS数据
	bool build_inter_proto( string &dest, const string &header, const string &seq, const string &mac_id,
			const string &command, const string &com_access_code, const string &command_value );
	// 将内部的数据转换成GPS类型处理
	bool build_gps_info( string &dest, GnssData *gps_data ) ;
	// 将GPS数据转成GNSS
	bool convert_gps_info( const string &dest, GnssData &gps ) ;
	// 构建yootu的数据
	bool build_yutoo_gps(string &dest, const string &mac_id, GnssData* gps_data);

	// 取得车辆的唯一标识
	string get_mac_id( const char *device_id, unsigned char device_color ) ;

	// 下发车辆报文应答消息
	bool build_ctrl_msg_text_info_ack( string &dest, string &in_seq, const unsigned int data_type, const string &mac_id,
			const string &access_code, const int msgid, const unsigned char result ) ;

	// 车辆单向监听应答消息
	bool build_ctrl_msg_monitor_vehicle_ack( string &dest, string &in_seq, const unsigned int data_type, const string &mac_id,
			const string &access_code, const unsigned char result ) ;

	// 车辆拍照应答消息
	bool build_ctrl_msg_take_photo_ack( string &dest, string &in_seq, const  unsigned int data_type, const string &mac_id,
			const string &access_code, const unsigned char photo_rsp_flag, const char * gnss_data, const unsigned char lens_id,
			const unsigned int photo_len, const unsigned char size_type, const unsigned char type, const char * photo , const int data_len, const char *szpath ) ;

	// 上报车辆行驶记录应答消息
	bool build_ctrl_msg_take_travel_ack( string &dest, string &in_seq, const unsigned int data_type, const string &mac_id,
			const string &access_code, unsigned char command_type,const char *data ) ;

	// 车辆应急接入监管平台应答消息
	bool build_ctrl_msg_emergency_monitoring_ack( string &dest, string &in_seq, const unsigned int data_type, const string &mac_id ,
			const string &access_code, const unsigned char result ) ;

	// 构建平台查岗应答
	bool build_platform_msg_post_query_ack( string &dest, string &in_seq, const unsigned int data_type, const string &mac_id,
			const string &com_access_code, UpPlatformMsgpostqueryData * pMsg, const char *data , const int len ) ;
	// 下发平台间报文应答消息PLATFORMMSG
	bool build_platform_msg_info_ack( string &dest, string &in_seq, const unsigned int data_type, const string &mac_id,
			const string &access_code, const int msgid ) ;
	// 补报车辆静态信息应答消息 UP_BASE_MSG_VEHICLE_ADDED_ACK
	bool build_base_msg_vehicle_added_ack( string &dest, string &in_seq, const unsigned int data_type, const string &mac_id ,
				const string &access_code , const char *data ) ;

	// 报警督办应答消息UP_WARN_MSG_URGE_TODO_ACK
	bool build_warn_msg_urge_todo_ack( string &dest, string &in_seq, const unsigned int data_type, const string &mac_id,
			const string &access_code , const unsigned char result ) ;
	// 上报报警信息消息
	bool build_warn_msg_adpt_info( string &dest, const string &in_seq, const string &mac_id, const string &access_code, const unsigned char warn_src,
			const unsigned short warn_type , const unsigned long long warn_time , const char *data ) ;

	// 主动上报报警处理结果信息消息
	bool build_warn_msg_adpt_todo_info( string &dest, const string &seq, const string &mac_id,
			const string &access_code, const unsigned int info_id, const unsigned char result);

	// 注册车辆信息 "请求：
	// 平台唯一编码|车载终端厂商唯一编码|车载终端型号|车载终端编号|车载终端SIM卡电话号码"
	bool build_exg_msg_register( string &dest, const string &seq , const string &mac_id , const string &access_code, const string &platform_id,
			const string &producer_id, const string &terminal_model_type, const string &terminal_id, const string &terminal_simcode ) ;

	// 启动跨域车辆定位信息交换应答消息
	bool build_exg_msg_arcossarea_startup_ack( string &dest, string &seq, const unsigned int data_type, const string &mac_id,
			const string &access_code, const string &device_id ) ;

	// 结束跨域车辆定位信息交换应答消息
	bool build_exg_msg_arcossarea_end_ack( string &dest, string &seq, const unsigned int data_type, const string &mac_id,
			const string &access_code, const string &device_id ) ;

	// 启动车辆定位信息交换应答消息
	bool build_exg_msg_return_startup_ack( string &dest, string &seq, const unsigned int data_type, const string &mac_id ,
			const string &access_code ) ;
	// 结束车辆定位信息效换应答消息
	bool build_exg_msg_return_end_ack( string &dest, string &seq, const unsigned int data_type, const string &mac_id ,
				const string &access_code ) ;

	// 申请交换指定车辆定位信息请求消息
	bool build_exg_msg_apply_for_monitor_startup( string &dest, const string &seq, const string &mac_id, const string &access_code,
			const unsigned long long start, const unsigned long long end ) ;

	// 取消交换指定车辆定位信息请求消息
	bool build_exg_msg_apply_for_monitor_end( string &dest, const string &seq, const string &mac_id, const string &access_code ) ;

	// 补发车辆定位信息请求消息
	bool build_exg_msg_apply_hisgnssdata_req( string &dest, const string &seq, const string &mac_id, const string &access_code,
			const unsigned long long start, const unsigned long long end ) ;

	// 上报司机身份识别信息应答消息
	bool build_exg_msg_report_driver_info_ack( string &dest, string &in_seq, const unsigned int data_type, const string &mac_id,
			const string &access_code, const string &driver_name ,  const string &driver_id , const string &licence , const string &org_name ) ;

	//	主动上报驾驶员身份信息消息
	bool build_exg_msg_report_driver_info(string &dest, string &in_seq, const unsigned int data_type, const string &mac_id,
			const string &access_code, const string &driver_name ,  const string &driver_id , const string &licence , const string &org_name );


	// 上报车辆电子运单应答消息
	bool build_exg_msg_take_waybill_ack( string &dest, string &in_seq, const unsigned int data_type, const string &mac_id,
			const string &access_code, const char *data, const int len ) ;

	//	主动上报车辆电子运单信息
	bool build_exg_msg_report_waybill_info( string &dest, string &in_seq, const unsigned int data_type, const string &mac_id,
			const string &access_code , const char *data, const int len );

	// 处理MAS请求的平台查岗
	bool build_mas_platform_msg_post_query_req( string &dest, const string &seq, const string &mac_id,
			const string &access_code, DownPlatformMsgPostQueryBody * pBody, const char *data , const int len ) ;
	// 处理下发平台间报文
	bool build_mas_platform_msg_info_req( string &dest, const string &seq, const string &mac_id,
				const string &access_code, DownPlatformMsgInfoReq * pReq, const char *data , const int len ) ;

	// 处理MAS交换车辆静态信息
	bool build_mas_exg_msg_car_info( string &dest, const string &seq, const string &mac_id, const string &access_code, const char *car_info ) ;

	// 处理MAS启动车辆定位信息交换
	bool build_mas_exg_msg_return_startup( string &dest, const string &seq, const string &mac_id, const string &access_code, const unsigned char result ) ;

	// 处理MAS结束车辆定位信息交换
	bool build_mas_exg_msg_return_end( string &dest, const string &seq, const string &mac_id, const string &access_code, const unsigned char result ) ;

	// 处理MAS申请交换指定车辆定位信息
	bool build_mas_exg_msg_apply_for_monitor_startup_ack( string &dest, string &seq, const uint16 data_type, const string &mac_id,
			const string &access_code, const uint8 result ) ;
	// 处理MAS取消交换指定车辆定位信
	bool build_mas_exg_msg_apply_for_monitor_end_ack( string &dest, string &seq, const uint16 data_type, const string &mac_id,
			const string &access_code , const uint8 result ) ;
	// 发送补发静态数据处理
	bool build_mas_down_base_msg_vehicle_added( string &dest, string &seq, const string &mac_id, const string &access_code ) ;

	// 处理MAS转发过来平台查岗数据
	char * convert_mas_dplat( const string &seq, const string &mac_id, const string &company_id, const string &operator_key,
					const string &operate_value, int &len ) ;
	// 处理MAS转过来D_MESG消息
	char * convert_mas_dmesg( const string &seq, const string &mac_id, const string &company_id, const string &operator_key,
			const string &operate_value, int &len ) ;

	// 转换数据MAS对应协议数据D_BASE
	char * convert_mas_dbase( const string &seq, const string &mac_id, const string &company_id , const string &operator_key,
			const string &operate_value, int &len ) ;

	//	转换数据，处理发往mas的D_CTLM
	char * convert_mas_dctlm(const string &seq, const string &mac_id, const string &company_id,
			const string &operator_key, const string &operate_value, int &len );	

	// 转换数据对应协议数据D_CTLM
	char * convert_dctlm( const string &seq, const string &mac_id, const string &company_id , const string &operator_key,
			const string &operate_value, int &len ) ;

	// 转换数据对应协议数据D_BASE
	char * convert_dbase( const string &seq, const string &mac_id, const string &company_id , const string &operator_key,
			const string &operate_value, int &len ) ;

	// 转换数据对应协议数据D_WARN
	char * convert_dwarn( const string &seq, const string &mac_id, const string &company_id , const string &operator_key,
			const string &operate_value, int &len ) ;

	// 转换数据对应协议数据D_PLAT
	char * convert_dplat( const string &seq, const string &mac_id, const string &company_id , const string &operator_key,
			const string &operate_value, int &len ) ;

	// 转换数据对应协议数据D_MESG
	char * convert_dmesg( const string &seq, const string &mac_id, const string &company_id , const string &operator_key,
				const string &operate_value, int &len ) ;

	// 释放数据
	void release( char *&data ) ;

	// 清理超时序列数据
	void clear_timeout_sequeue( const unsigned int timeout ) ;

private:
	// 构建平台内部序号
	const string build_platform_out_seq( const string &mac_id, unsigned short data_type ) ;

	// 添加GPS类型数据
	void append_gps( string &dest, const string &data ) ;

	// 跨域车辆定位信息交换应答消息
	bool build_exg_msg_arcossarea_ack( string &dest, string &seq, const unsigned int data_type, const string &mac_id,
			const string &access_code, const string &device_id , const int ack_type ) ;
	// 构建通用数据格式
	bool build_common_dctlm_data( string &dest, const string &header, string &in_seq, const string &mac_id, const unsigned int msg_type,
			const string &access_code, const string &command, const string &data ) ;

private:
	// 内部平台与外部序列对应转换关系
	CReqMap      _seq_map ;
};

#endif /* INTERPROTOCONVERT_H_ */
