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

#include <map>
#include <string>
#include <Mutex.h>
#include "ProtoHeader.h"
#include <Session.h>
#include <databuffer.h>
using namespace std ;

#ifdef PHONE_LEN
#undef PHONE_LEN
#endif
#define PHONE_LEN 11

// 带有超时和锁处理的MAP队列，需要不需要处理超时则需要根据是否为闭合的系统，
// 如果输入和输出肯定相等则为闭合
class CReqMap
{
	struct _SEQ_DATA
	{
		string _seq ;
		time_t _now ;
	};
	typedef multimap<string,_SEQ_DATA*>  CSeqMap ;
	typedef multimap<time_t,string>		 CSeqIndex ;
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
		CSeqMap::iterator it = _map_seq.find(key) ;
		/**
		if ( it != _map_seq.end() ){
			_SEQ_DATA *p = it->second ;
			RemoveIndex( p->_seq, p->_now ) ;
			p->_now = time(NULL) ;
			p->_seq = val ;
			AddIndex( p->_seq, p->_now ) ;
		}else{
		*/
			_SEQ_DATA *p = new _SEQ_DATA;
			p->_now = time(NULL) ;
			p->_seq = val ;
			_map_seq.insert( pair<string,_SEQ_DATA*>( key, p ) ) ;
			AddIndex( p->_seq, p->_now ) ;
		/**}*/
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
		CSeqMap::iterator itx;
		CSeqIndex::iterator it ,itmp;
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
		CSeqMap::iterator it = _map_seq.find(key);
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
		CSeqIndex::iterator it = _map_index.find(now) ;
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
		CSeqMap::iterator it ;
		for ( it = _map_seq.begin(); it != _map_seq.end(); ++ it ){
			delete it->second ;
		}
		_map_seq.clear() ;
		_map_index.clear() ;
		_map_mutex.unlock() ;
	}

private:
	share::Mutex 	_map_mutex ;
	CSeqMap  		_map_seq ;    // MAP值
	CSeqIndex  	 	_map_index ;  // 时间索引
	unsigned  int	_seq_count ;  // 序列值生成器
	bool 			_bindex ;     // 是否需要索引
};

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
	// 转换控制指令对象
	bool convert_ctrl( const string &seq, const string &macid, const string &line, const string &vechile,
			DataBuffer &dbuf , string &acode ) ;
	// 处理发送文本消息
	bool convert_sndm( const string &seq, const string &macid, const string &line, const string &vechile,
			DataBuffer &dbuf, string &acode ) ;

	// 构建内部协议
	bool BuildMonitorVehicleResp( const string &macid, UpCtrlMsgMonitorVehicleAck *moni , string &data ) ;
	// 处理照片上传
	bool BuildUpCtrlMsgTakePhotoAck( const string &macid, const string &path, const char *data, int len , string &sdata ) ;
	// 转换下发文本应答
	bool BuildUpCtrlMsgTextInfoAck( const string &macid, UpCtrlMsgTextInfoAck *text, string &sdata ) ;
	// 构建位置数据
	bool BuildUpRealLocation( const string &macid, UpExgMsgRealLocation *upmsg, string &sdata ) ;
	// 处理历史数据上传
	bool BuildUpHistoryLocation( const string &macid, const char *data, int len , int num, string &sdata ) ;
	// 添加MACID到接入码的关系
	void AddMac2Access( const string &macid, const string &accessid ) { _macid2access.AddSession( macid, accessid ) ; }
	// 取得手机号和OME码
	bool get_phoneome( const string &macid, string &phone, string &ome ) ;
	// 通过车辆的MAC中取得车牌号和颜色
	bool get_carinfobymacid( const string &macid, unsigned char &carcolor, string &carnum ) ;
	// 取得序号对应关系
	unsigned int get_next_seq( void ) { return _seq_gen.get_next_seq(); }
	// 检测超时数据
	void CheckTime( int timeout ) { _reqmap.ClearKey(timeout); }
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
	// 本地存放图片路径
	string 		 _picdir ;
	// 序号与车牌对应关系
	CReqMap		 _reqmap ;
	// 手机号接入码
	CSessionMgr  _macid2access;
};

#endif
