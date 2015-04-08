/*
 * servicecaller.h
 *
 *  Created on: 2011-12-12
 *      Author: humingqing
 */

#ifndef __SERVICECALLER_H_
#define __SERVICECALLER_H_

#include <map>
#include <iplugin.h>
#include <vector>
#include <string>
#include <Mutex.h>
#include "httpcaller.h"
using namespace std ;

#include "truckpack.h"
#include "msgqueue.h"
#include "resultmgr.h"

#include <Thread.h>

namespace TruckSrv{
	// 业务调用模块负责实现HTTP调用
	class CSrvCaller:
		public ICallResponse , public IMsgCaller, public share::Runnable
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
				// 处理NULL数据引起的coredump
				if ( key == NULL || val == NULL )
					return ;
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

		typedef bool (CSrvCaller::*ServiceFun)( unsigned int seq, const char *xml ) ;
		typedef map<unsigned int , ServiceFun>  ServiceTable;

	public:
		CSrvCaller() ;
		~CSrvCaller() ;
		// 初始化
		bool Init( IPlugin *pEnv , const char *url, int sendthread , int recvthread , int queuesize ) ;
		// 启动
		bool Start( void ) ;
		// 停止
		void Stop( void );
		// 订阅消息查询
		bool getQueryInfoReq(unsigned int fd, unsigned int cmd, CQueryInfoReq *msg, const char *id);
		// 车队聊天
		bool getSendTeamMediaReq(unsigned int fd, unsigned int cmd, CSendTeamMediaReq *msg, const char *id);
		// 车友聊天
		bool getSendMediaDataReq(unsigned int fd, unsigned int cmd, CSendMediaDataReq *msg, const char *id);
		// 接收头车的信息
		bool getInfoPriMcarReq(unsigned int fd, unsigned int cmd, CInfoPriMcarReq  *msg, const char *id);
		//设置本车为头车
		bool getSetPriMcarReq(unsigned int fd, unsigned int cmd, CSetPriMcarReq *msg, const char *id);
		// 车队成员邀请
		bool getInviteNumberReq(unsigned int fd, unsigned int cmd, CInviteNumberReq *msg, const char *id);
		// 建立车队
		bool getAddCarTeamReq(unsigned int fd, unsigned int cmd, CAddCarTeamReq *msg, const char *id);
		// 获取车友列表
		bool getGetFriendlistReq(unsigned int fd, unsigned int cmd, CGetFriendListReq *msg, const char *id);
		// 邀请车友
		bool getInviteFriendReq(unsigned int fd, unsigned int cmd, CInviteFriendReq *msg, const char *id);
		// 添加好友
		bool getAddFriendsReq(unsigned int fd, unsigned int cmd, CAddFriendsReq *msg, const char *id);
		// 查找附近车友
		bool getQueryFriendsReq(unsigned int fd, unsigned int cmd, CQueryFriendsReq *msg, const char *id);
		// 司机身份注销
		bool getDriverLoginOutReq(unsigned int fd, unsigned int cmd, CDriverLoginOutReq *msg, const char *id);
		// 司机注册登录
		bool getDriverLoginReq(unsigned int fd, unsigned int cmd, CDriverLoginReq *msg, const char *id);
		// 取得注册信息，这里只处理鉴权时数据
		bool getQueryCarDataReq( unsigned int fd, unsigned int cmd , CQueryCarDataReq *msg , const char *id ) ;
		// 处理下发调度单响应操作结果
		bool getResultScheduleReq( unsigned int fd, unsigned int cmd, CResultScheduleReq *msg , const char *id ) ;
		// 处理下发调度单自动应答
		bool putSendScheduleRsp( unsigned int fd, unsigned int cmd, CSendScheduleRsp *msg , const char *id );
		//终端通用应答
		bool putTerminalCommonRsp(unsigned int fd, unsigned int cmd, CTerminalCommonRsp *msg , const char *id);
		// 查询当前最近的调度单
		bool getQueryScheduleReq( unsigned int fd, unsigned int cmd, CQueryScheduleReq *msg , const char *id ) ;
		// 上传调度单的信息
		bool putUploadScheduleReq( unsigned int fd, unsigned int cmd, CUploadScheduleReq *msg , const char *id ) ;
		// 上报车辆调度状态
		bool putStateScheduleReq( unsigned int fd, unsigned int cmd, CStateScheduleReq *msg , const char *id ) ;
		// 上报告警状态
		bool putAlarmScheduleReq( unsigned int fd, unsigned int cmd, CAlarmScheduleReq *msg , const char *id ) ;
		// 订阅天气
		bool putScheduleReq(unsigned int fd, unsigned int cmd, CSubscrbeReq *msg, const char *id);
		// 上传车辆故障
		bool putErrorScheduleReq(unsigned int fd, unsigned int cmd,CErrorScheduleReq *msg , const char *id );
		// 自动上报配货信息
		bool putAutoDataScheduleReq(unsigned int fd, unsigned int cmd,CAutoDataScheduleReq *msg , const char *id );
		// 终端透传
		bool getMsgDataScheduleReq(unsigned int fd, unsigned int cmd,CUpMsgDataScheduleReq *msg , const char *id );
		// 上传配货信息
		bool getCarDataInfoReq(unsigned int fd, unsigned int cmd, CUpCarDataInfoReq *msg, const char *id);
		//上传运单号
		bool getUpTransportFormInfoReq(unsigned int fd, unsigned int cmd, CTransportFormInfoReq *msg, const char *id);
		// 订单详细查询
		bool getQueryOrderFormInfoReq(unsigned int fd, unsigned int cmd, CQueryOrderFromInfoReq *msg, const char *id);
		// 上传货运订单号
		bool getUpOrderFormInfoReq(unsigned int fd, unsigned int cmd, CUpOrderFromInfoReq *msg, const char *id);
		// 处理HttpCaller回调
		void ProcessResp( unsigned int seqid, const char *xml, const int len , const int err ) ;
		// 处理请求超时的请求
		void OnTimeOut( unsigned int seq, unsigned int fd, unsigned int cmd, const char *id, IPacket *msg )  ;
		// 检测超时数据
		void CheckTimeOut( int timeout ) { _resultmgr.Check(timeout) ;  }

	public:
		// 线程执行对象
		void run( void *param ) ;
	private:
		// 设计为一个统一调用方式处理
		bool ProcessMsg( unsigned int msgid, unsigned int seq , const char *service, const char *method , CKeyValue &item ) ;
		// 创建调用XML数据
		bool CreateRequestXml( CQString &sXml, const char *id, const char *service, const char *method , CKeyValue &item ) ;

	protected:
		//终端通用应答自动响应
		bool Proc_TERMINAL_COMMON_RSP( unsigned int seqid, const char *xml );
		//上传运单状态
		bool Proc_TRANSPORT_ORDER_FROM_INFO_REQ(unsigned int seqid, const char *xml);
		//上传货运订单状态
		bool Proc_ORDER_FROM_INFO_REQ(unsigned int seqid, const char *xml);
		//上传配货信息
		bool Proc_CARDATA_INFO_REQ(unsigned int seqid, const char *xml);
		// 订单详细查询
		bool Proc_QUERY_ORDER_FORM_INFO_REQ(unsigned int seqid, const char *xml);
		// 订阅消息查询
		bool Proc_QUERY_INFO_REQ( unsigned int seqid, const char *xml);
		// 车队聊天
		bool Proc_SEND_TEAMMEDIA_REQ(unsigned int seqid, const char *xml);
		// 车友聊天
		bool Proc_SEND_MEDIADATA_REQ(unsigned int seqid, const char *xml);
		// 接收头车的信息
		bool Proc_INFO_PRIMCAR_REQ(unsigned int seqid, const char *xml);
		// 设置本车为头车
		bool Proc_SET_PRIMCAR_REQ(unsigned int seqid, const char *xml);
		// 车队成员邀请
		bool Proc_INVITE_NUMBER_REQ(unsigned int seqid, const char *xml);
		// 建立车队
		bool Proc_ADD_CARTEAM_REQ(unsigned int seqid, const char *xml);
		// 获取车友列表
		bool Proc_GET_FRIENDLIST_REQ(unsigned int seqid, const char *xml);
		//邀请好友
		bool Proc_INVITE_FRIEND_REQ(unsigned int seqid, const char *xml);
		// 添加好友
		bool Proc_ADD_FRIENDS_REQ(unsigned int seqid, const char *xml);
		// 查找附近车友
		bool Proc_QUERY_FRIENDS_REQ(unsigned int seqid, const char *xml);
		// 司机身份注销
		bool Proc_DRIVER_LOGINOUT_REQ(unsigned int seqid, const char *xml);
		// 司机身份登陆认证
		bool Proc_DRIVER_LOGIN_REQ(unsigned int seqid, const char *xml);
		// 处理对应的XML的数据
		bool Proc_QUERY_CARDATA_REQ( unsigned int seqid, const char *xml ) ;
		// 处理数据上报
		bool Proc_UPLOAD_DATAINFO_REQ( unsigned int seqid, const char *xml ) ;
		// 处理下发调度单响应
		bool Proc_RESULT_SCHEDULE_REQ( unsigned int seqid, const char *xml ) ;
		// 下发调度单自动应答上报
		bool Proc_SEND_SCHEDULE_RSP( unsigned int seqid, const char *xml ) ;
		// 查询调度单
		bool Proc_QUERY_SCHEDULE_REQ( unsigned int seqid, const char *xml ) ;
		// 上传调度单
		bool Proc_UPLOAD_SCHEDULE_REQ( unsigned int seqid, const char *xml ) ;
		// 上报调度单状态
		bool Proc_STATE_SCHEDULE_REQ( unsigned int seqid, const char *xml ) ;
		// 上报报警状态
		bool Proc_ALARM_SCHEDULE_REQ( unsigned int seqid, const char *xml ) ;
		// 订阅响应
		bool Proc_SCHEDULE_REQ( unsigned int seqid, const char *xml ) ;
		// 上传车辆故障响应
		bool Proc_ERROR_SCHEDULE_REQ( unsigned int seqid, const char *xml ) ;
		// 自动上报配货信息响应
		bool Proc_AUTO_DATA_SCHEDULE_REQ( unsigned int seqid, const char *xml ) ;
		// 终端透传数据响应
		bool Proc_MSG_DATA_SCHEDULE_REQ( unsigned int seqid, const char *xml );
		// 发送数据
		void DeliverPacket( unsigned int fd, unsigned int cmd, IPacket *msg);
	private:
		// 处理错误的情况
		void ProcessError( unsigned int seq , bool remove);
	private:
		// 处理环境对象
		IPlugin			   		*_pEnv ;
		// 处理XML调用的HTTP对象
		CHttpCaller				_httpcaller ;
		// 调用
		ServiceTable			_srv_table ;
		// 处理序号消息对应关系
		CSeq2Msg				_seq2msgid;
		// 调用服务URL地址
		CQString 				_callUrl;
		// 数据等待队列
		CMsgQueue				_msgqueue;
		// 结果集管理对象
		CResultMgr			    _resultmgr;
		// 线程执行对象
		share::ThreadManager 	_thread ;
		// 信号等待对象
		share::Monitor		 	_monitor;
		// 是否初始化
		bool 				 	_inited ;
		// 数据解包对象
		CTruckUnPackMgr 		_unpacker ;
		// 解包工厂
		CPackFactory *			_packfactory;
	};
};

#endif /* SERVICECALLER_H_ */
