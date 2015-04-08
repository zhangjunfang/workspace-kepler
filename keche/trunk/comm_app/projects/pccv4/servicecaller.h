/*
 * servicecaller.h
 *
 *  Created on: 2011-12-12
 *      Author: humingqing
 */

#ifndef __SERVICECALLER_H_
#define __SERVICECALLER_H_

#include "interface.h"
#include <map>
#include <vector>
#include <string>
#include <Mutex.h>
#include <httpcaller.h>
#include <Session.h>

using namespace std ;

// 业务调用模块负责实现HTTP调用
class CServiceCaller: public ICallResponse
{
	// 处理KEY VALUE
	struct _KeyValue{
		string key ;
		string val ;
	};

	// KEY VALUE对象
	class CKeyValue
	{
	public:
		CKeyValue() {}
		~CKeyValue(){}
		// 取得元素的个数
		int GetSize() { return (int)_vec.size(); }
		// 取得KEY VALUE的值
		_KeyValue &GetVal(int index) { return _vec[index]; }
		// 设置值处理
		void SetVal( const char *key, const char *val ) {
			_KeyValue vk ;
			vk.key = key ;
			vk.val = val ;
			_vec.push_back( vk ) ;
		}
	private:
		// 存放数据的容器
		vector<_KeyValue> _vec ;
	};

	// 处理消息ID对应请求ID映射
	class CSeq2Msg
	{
		// 前一个为请求ID，对应的消息ID
		typedef map<unsigned int , unsigned int>  MapSeq2Msg;
	public:
		CSeq2Msg() {};
		~CSeq2Msg(){};

		// 添加序号对应关系
		void AddSeqMsg( unsigned int seq, unsigned int msgid ) {
			share::Guard guard( _mutex ) ;
			_seq2msgid[seq] = msgid ;
		}

		// 取得序号对应消息映射关系
		bool GetSeqMsg( unsigned int seq , unsigned int &msgid ) {
			share::Guard guard( _mutex ) ;

			MapSeq2Msg::iterator it = _seq2msgid.find( seq ) ;
			if ( it == _seq2msgid.end() ) {
				return false ;
			}
			msgid = it->second ;
			_seq2msgid.erase( it ) ;

			return true ;
		}

		// 移除映射
		void RemoveSeq( unsigned int seq ) {
			share::Guard guard( _mutex ) ;

			MapSeq2Msg::iterator it = _seq2msgid.find( seq ) ;
			if ( it == _seq2msgid.end() ) {
				return;
			}
			_seq2msgid.erase( it ) ;
		}

	private:
		// 请求序列号对应消息调用类型
		MapSeq2Msg				_seq2msgid ;
		// 处理锁的操作
		share::Mutex			_mutex ;
	};

	// 需要处理缓存的对数据
	class CSeq2Key
	{
		typedef map<unsigned int,string> MapSeq2Key ;
	public:
		CSeq2Key(){}
		~CSeq2Key(){}

		// 添加序号对应关系
		void AddSeqKey( unsigned int seq, const string &key ) {
			share::Guard guard( _mutex ) ;
			_seq2key[seq] = key ;
		}

		// 取得序号对应消息映射关系
		bool GetSeqKey( unsigned int seq , string &key ) {
			share::Guard guard( _mutex ) ;

			MapSeq2Key::iterator it = _seq2key.find( seq ) ;
			if ( it == _seq2key.end() ) {
				return false ;
			}
			key = it->second ;
			_seq2key.erase( it ) ;

			return true ;
		}

	private:
		MapSeq2Key    _seq2key ;
		share::Mutex  _mutex ;
	};

	// 处错误的HTTP调用次机制
	class CSeqMacRef
	{
		typedef map<unsigned int,string> MapSeq2Mac ;
		typedef map<string,time_t>		 MapMac2Ref ;
	public:
		CSeqMacRef():_timeout(120){};
		~CSeqMacRef(){};

		// 添加索引
		bool Add( const char *macid, unsigned int seq ) {
			share::Guard guard( _mutex ) ;
			// 如果时间小零则不处理
			if ( _timeout < 0 ) return true ;
			// 处理HTTP调用错误禁用时间
			MapMac2Ref::iterator it = _mac2ref.find( macid ) ;
			if ( it != _mac2ref.end() ) {
				// 如果出错，120秒内不允许重复调用
				if ( time(NULL) - it->second  < _timeout ) {
					return false ;
				}
				// 否则超时则不管它，继续处理
				_mac2ref.erase( it ) ;
			}
			// 否则添加索引
			_seq2mac.insert( make_pair(seq, macid) ) ;

			return true ;
		}

		// 处理是否调用出错
		void Dec( unsigned int seq , bool err ){
			share::Guard guard( _mutex ) ;
			// 如果时间小于零不处理
			if ( _timeout < 0 ) return ;
			// 查找当前序号对应MAC
			MapSeq2Mac::iterator it = _seq2mac.find( seq ) ;
			if ( it == _seq2mac.end() )
				return ;

			// 如果出错了是需处理添加时间索引的
			if ( err )
				_mac2ref.insert( make_pair( it->second, time(NULL) ) ) ;
			// 移除数据
			_seq2mac.erase( it ) ;
		}
		// 设置HTTP调用错误时间
		void SetTimeOut( int timeout ) { _timeout = timeout ;}

	private:
		// 记录SEQ对应的MAC
		MapSeq2Mac	 _seq2mac ;
		// 记录MAC对应的引用次数
		MapMac2Ref	 _mac2ref ;
		// 设置HTTP错误禁用时间
		int 		 _timeout ;
		// 同步处理锁操作
		share::Mutex _mutex ;
	};

	typedef bool (CServiceCaller::*ServiceFun)( unsigned int seq, const char *xml ) ;
	typedef map<unsigned int , ServiceFun>  ServiceTable;

public:
	CServiceCaller() ;
	~CServiceCaller() ;
	// 初始化
	bool Init( ISystemEnv *pEnv ) ;
	// 启动
	bool Start( void ) ;
	// 停止
	void Stop( void );
	// 清理缓存
	void RemoveCache( const char *key ) ;
	// 检测是超时的缓存数据
	void CheckTimeOut( void ) ;
	// 取得注册信息，这里只处理鉴权时数据
	bool getRegVehicleInfo( unsigned int msgid, unsigned int seq, const char *phoneNum, const char *terminaltype ) ;
	// 通用查询，通过车牌号和车牌颜色取得手机号与ome码的对应关系
	bool getTernimalByVehicleByType( unsigned int msgid, unsigned int seq, const char *vehicleno, const char *vehicleColor ) ;
	// 通用查询，通过车牌号和车牌颜色取得手机号与ome码的对应关系,下发消息处理
	bool getTernimalByVehicleByTypeEx( unsigned int msgid, unsigned int seq, const char *vehicleno, const char *vehicleColor , const char *text ) ;
	// 通用查询，通过手机号和oem码取得对应车牌以及省域关系
	bool getBaseVehicleInfo( unsigned int msgid, unsigned int seq, const char *phone  , const char *ome ) ;
	// 获取得驾驶员识别信息
	bool getDriverOfVehicleByType( unsigned int msgid, unsigned int seq, const char *vehicleno , const char *vehicleColor ) ;
	// 获取得电子运单数据
	bool getEticketByVehicle( unsigned int msgid, unsigned int seq, const char *vehicleno , const char *vehicleColor ) ;
	// 获取车辆静态信息的数据
	bool getDetailOfVehicleInfo( unsigned int msgid, unsigned int seq, const char *vehicleno, const char *vehicleColor ) ;
	// 更新连接的状态
	bool updateConnectState( unsigned int msgid, unsigned int seq, int areaId , int linkType , int status ) ;

	// 处理平台查岗的消息
	bool addForMsgPost( unsigned int msgid, unsigned int seq, const char *messageContent , const char * messageId ,
			const char *objectId, const char *objectType , const char *areaId ) ;
	// 处理下发平台的报文
	bool addForMsgInfo(unsigned int msgid, unsigned int seq, const char *messageContent , const char * messageId ,
			const char *objectId, const char *objectType , const char *areaId ) ;
	// 处理报警督办
	bool addMsgUrgeTodo( unsigned int msgid, unsigned int seq, const char *supervisionEndUtc , const char *supervisionId ,
			const char * supervisionLevel , const char * supervisor , const char *supervisorEmail , const char *supervisorTel ,
			const char * vehicleColor, const char *vehicleNo , const char *wanSrc , const char *wanType , const char *warUtc ) ;
	// 处理报警预警
	bool addMsgInformTips( unsigned int msgid, unsigned int seq, const char *alarmDescr, const char *alarmFrom,
			const char *alarmTime, const char *alarmType , const char *vehicleColor , const char *vehicleNo ) ;

	// 处理HttpCaller回调
	void ProcessResp( unsigned int seqid, const char *xml, const int len , const int err ) ;

private:
	// 设计为一个统一调用方式处理
	bool ProcessMsg( unsigned int msgid, unsigned int seq , const char *service, const char *method , CKeyValue &item ) ;
	// 创建调用XML数据
	bool CreateRequestXml( CQString &sXml, const char *id, const char *service, const char *method , CKeyValue &item ) ;

protected:
	// 处理注册的数据
	bool Proc_UP_EXG_MSG_REGISTER( unsigned int seq, const char *xml ) ;
	// 处理位置上报处理
	bool Proc_UP_EXG_MSG_REAL_LOCATION( unsigned int seq, const char *xml ) ;
	// 主动上报驾驶员身份识别
	bool Proc_UP_EXG_MSG_REPORT_DRIVER_INFO( unsigned int seq, const char *xml ) ;
	// 主动上报电子运单
	bool Proc_UP_EXG_MSG_REPORT_EWAYBILL_INFO( unsigned int seq, const char *xml ) ;
	// 处理事件监听
	bool Proc_UP_CTRL_MSG_MONITOR_VEHICLE_ACK( unsigned int seq, const char *xml ) ;
	// 处理文本下发应答
	bool Proc_UP_CTRL_MSG_TEXT_INFO_ACK( unsigned int seq, const char *xml ) ;
	// 处理行车记录仪
	bool Proc_UP_CTRL_MSG_TAKE_TRAVEL_ACK( unsigned int seq, const char *xml ) ;
	// 处理车辆紧急接入
	bool Proc_UP_CTRL_MSG_EMERGENCY_MONITORING_ACK( unsigned int seq, const char *xml ) ;
	// 处理控制请求处理
	bool Proc_DOWN_CTRL_MSG( unsigned int seq, const char *xml ) ;
	// 处理拍照，针对拍照特殊处理
	bool Proc_DOWN_CTRL_MSG_TAKE_PHOTO_REQ( unsigned int seq, const char *xml ) ;
	// 处理平台间消息
	bool Proc_DOWN_PLATFORM_MSG( unsigned int seq, const char *xml ) ;
	// 处理平台间的报文自动应答
	bool Proc_DOWN_PLATFORM_MSG_INFO_REQ( unsigned int seq, const char *xml ) ;
	// 处理报警消息
	bool Proc_DOWN_WARN_MSG( unsigned int seq, const char *xml ) ;
	// 处理报警督办自动应答
	bool Proc_DOWN_WARN_MSG_URGE_TODO_REQ( unsigned int seq, const char *xml ) ;
	// 取得车辆静态信息
	bool Proc_DOWN_BASE_MSG_VEHICLE_ADDED( unsigned int seq, const char *xml ) ;
	// 处理位置上报和消息
	bool Proc_DOWN_EXG_MSG_CAR_LOCATION( unsigned int seq, const char *xml ) ;
	// 车辆定位信息交换
	bool Proc_DOWN_EXG_MSG_HISTORY_ARCOSSAREA( unsigned int seq, const char *xml ) ;
	// 交换车辆的静态信息
	bool Proc_DOWN_EXG_MSG_CAR_INFO( unsigned int seq, const char *xml ) ;
	// 启动跨域请求
	bool Proc_DOWN_EXG_MSG_RETURN_STARTUP( unsigned int seq, const char *xml ) ;
	// 结束跨域请求
	bool Proc_DOWN_EXG_MSG_RETURN_END( unsigned int seq, const char *xml ) ;
	// 处理数据应答
	bool Proc_DOWN_EXG_MSG_ACK( unsigned int seq, const char *xml ) ;
	// 上报驾驶员身份识别
	bool Proc_DOWN_EXG_MSG_REPORT_DRIVER_INFO( unsigned int seq, const char *xml ) ;
	// 上报电子运单数据
	bool Proc_DOWN_EXG_MSG_TAKE_WAYBILL_REQ( unsigned int seq, const char *xml ) ;

private:
	// 自动处理失败的跨域处理
	template<typename T>
	bool ProcDownExgReturnMsg( unsigned int seq, const char *id ) ;
	// 处理扩展消息模块
	template<typename T>
	bool ProcUpMsg( unsigned int seq, const char *xml , const char *msg ) ;
	// 解析手机号XML的值
	bool ParsePhoneXml( unsigned int seq, const char *xml , char *key, char *macid,  string &inner ) ;
	// 处理错误的情况
	void ProcessError( unsigned int seq , bool remove ) ;

private:
	// 处理环境对象
	ISystemEnv			   *_pEnv ;
	// 处理XML调用的HTTP对象
	CHttpCaller				_httpcaller ;
	// 调用
	ServiceTable			_srv_table ;
	// 处理序号消息对应关系
	CSeq2Msg				_seq2msgid;
	// 存放内部协议数据
	CSessionMgr				_innerdata ;
	// 数据缓存对象
	CSessionMgr 			_datacache ;
	// 序号对应KEY关系
	CSeq2Key				_seq2key ;
	// 调用索引记当处理
	CSeqMacRef				_macref ;
	// 调用服务URL地址
	CQString 				_callUrl ;
	// 是否过检测试
	unsigned int			_istester ;
	// 缓存存活时间
	int 			  		_livetime ;
	// 主要方便问题排查
	CSeq2Key				_seq2key2;
};


#endif /* SERVICECALLER_H_ */
