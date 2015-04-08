/**********************************************
 * pccutil.h
 *
 *  Created on: 2011-08-04
 *      Author: humingqing
 *       Email: qshuihu@gmail.com
 *    Comments:
 *********************************************/

#ifndef __PCONVERT_H__
#define __PCONVERT_H__

#include "interface.h"
#include <map>
#include <string>
#include <Mutex.h>
#include <ProtoHeader.h>

#ifdef  PHONE_LEN
#undef  PHONE_LEN
#endif
#define CAR_TYPE        4 //4C54
#define PHONE_LEN   	11
#define PLATFORM_ID  	"ctfov00001"

#define METHOD_OTHER     0
#define METHOD_REG   	 1

class PConvert
{
	class CSequeue
	{
	public:
		CSequeue():_seq_id(0) {}
		~CSequeue() {}

		// 取得序列
		unsigned int get_next_seq( void ) {
			share::Guard g( _mutex ) ;
			if ( _seq_id >= 0xffffffff ) {
				_seq_id = 0 ;
			}
			return ++ _seq_id ;
		}

	private:
		// 序列生成锁
		share::Mutex  _mutex ;
		// 序列ID号
		unsigned int  _seq_id ;
	};
	typedef std::map<std::string,std::string>   MapString;
public:
	PConvert() ;
	~PConvert() ;

	// 初始化环境对象
	void   initenv( ISystemEnv *pEnv ) ;
	// 转换数据
	char * convert_urept( const string &key, const string &ome, const string &phone, const string &val, int &len , unsigned int &msgid , unsigned int &type ) ;
	// 处理D_CTLM
	char * convert_dctlm( const string &key, const string &val, int &len , unsigned int &msgid ) ;
	// 处理D_SNDM
	char * convert_dsndm( const string &key, const string &val, int &len , unsigned int &msgid ) ;
	// 处理通用应答
	char * convert_comm( const string &key, const string &phone, const string &val, int &len, unsigned int &msgid ) ;
	// 转换监管协议
	char * convert_lprov( const string &key, const string &seqid, const string &val , int &len, string &areacode ) ;
	// 释放缓存
	void   free_buffer( char *buf ) ;
	// 取得手机号和OME码
	bool get_phoneome( const string &macid, string &phone, string &ome ) ;
	// 通过车辆的MAC中取得车牌号和颜色
	bool get_carinfobymacid( const string &macid, unsigned char &carcolor, string &carnum ) ;

public:
	// 将GnssData转成监控数据
	static void build_gps_info( string &dest, GnssData *gps_data ) ;

private:
	// 转换GPS数据
	bool convert_gps_info( MapString &map, GnssData &gps ) ;
	// 处理监控的参数
	bool parse_jkpt_value( const std::string &param, MapString &val ) ;
	// 取得对应MAP字符值
	bool get_map_string( MapString &map, const std::string &key , std::string &val ) ;
	// 取得对应MAP中整形值
	bool get_map_integer( MapString &map, const std::string &key , int &val ) ;
	// 取得头数据
	bool get_map_header( const std::string &param, MapString &val, int &ntype ) ;
	// 拆分数据到MAP
	bool split2map( const std::string &s , MapString &val ) ;

private:
	// 序列生存器
	CSequeue  	 _seq_gen ;
	// 环境对象
	ISystemEnv  *_pEnv ;
	// 是否过检测，因为模拟终端数据特殊字符原因
	unsigned int _istester ;
	// 本地存放图片路径
	string 		 _picdir ;
};

#endif
