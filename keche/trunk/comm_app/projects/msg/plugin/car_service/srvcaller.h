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

using namespace std;

#include "carservicepack.h"
#include "msgqueue.h"
#include "resultmgr.h"

#include <Thread.h>

namespace CarService{
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
		//终端通用应答
		bool putTerminalCommonRsp(unsigned int fd, unsigned int cmd, CTerminalCommonRsp *msg , const char *id);
		// 查询打折优惠信息
		bool getUpQueryDiscountInfoReq(unsigned int fd, unsigned int cmd, CQueryDiscountInfoReq *msg, const char *id);
		// 查询具体打折优惠信息
		bool getUpQueryDetailDiscountInfoReq(unsigned int fd, unsigned int cmd, CQueryDetailiscountInfoReq *msg, const char *id);
		// 查询联盟商家信息
		bool getUpQueryUnionBusinessInfoReq(unsigned int fd, unsigned int cmd, CQueryUnionBusinessInfoReq *msg, const char *id);
		// 查询具体联盟商家信息
		bool getUpQueryDetailUnionBusinessInfoReq(unsigned int fd, unsigned int cmd, CQueryDetailUnionBusinessInfoReq *msg, const char *id);
		//用户登录
		bool getUpLoginInfoReq( unsigned int fd, unsigned int cmd,CLoginInfoReq *msg, const char *id );
		//联名卡余额查询
		bool getUpQueryBallanceListReq( unsigned int fd, unsigned int cmd,CQuery_Ballance_List_Req *msg, const char *id );
		//查询门店
		bool getUpQueryStoreListReq(unsigned int fd, unsigned int cmd,CQuery_Store_List_Req *msg, const char *id);
		//查询门店详情
		bool getUpQueryViewStoreInfoReq(unsigned int fd, unsigned int cmd,CView_Store_Info_Req *msg, const char *id);
		//新版本查询优惠信息
		bool getUpQueryDiscountListReq(unsigned int fd, unsigned int cmd,CQuery_Discount_List_Req *msg, const char *id);
		//新版本查询优惠信息详细列表
		bool getUpViewDiscountInfoReq( unsigned int fd, unsigned int cmd,CView_Discount_Info_Req *msg, const char *id);
		//历史交易记录查询
		bool getUpQueryTradeListReq(unsigned int fd, unsigned int cmd,CQuery_Trade_List_Req *msg, const char *id);
		//查询收藏列表
		bool getUpQueryFavoriteListReq(unsigned int fd, unsigned int cmd,CQuery_Favorite_List_Req *msg, const char *id);
		//查询收藏列表详情
		bool getUpViewFavoriteInfoReq(unsigned int fd, unsigned int cmd,CView_Favorite_Info_Req *msg, const char *id);
		//添加收藏请求
		bool getUpAddFavoriteReq(unsigned int fd, unsigned int cmd,CAdd_Favorite_Req *msg, const char *id);
		//删除收藏请求
		bool getUpDelFavoriteReq(unsigned int fd, unsigned int cmd,CDel_Favorite_Req *msg, const char *id);
		//获取目的地
		bool getUpGetDestinationReq(unsigned int fd, unsigned int cmd,CGet_Destination_Req *msg, const char *id);
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
		bool ProcessMsg( unsigned int msgid, unsigned int seq , const char *service, const char *method , CKeyValue &item);
		// 创建调用XML数据
		bool CreateRequestXml( CQString &sXml, const char *id, const char *service, const char *method , CKeyValue &item);
	protected:
		//查询打折优惠信息
		bool Proc_UPDISCOUNT_INFO_REQ( unsigned int seqid, const char *xml );
		//查询具体打折优惠信息
		bool Proc_UPDETAIL_DISCOUNT_INFO_REQ( unsigned int seqid, const char *xml);
		//查询联盟商家信息
		bool Proc_UPUNION_BUSINESS_INFO_REQ( unsigned int seqid, const char *xml);
		//查询具体联盟商家信息
		bool Proc_UPDETAIL_UNION_BUSINESS_INFO_REQ( unsigned int seqid, const char *xml );
		//用户登录
		bool Proc_UPLOGIN_INFO_REQ( unsigned int seqid, const char *xml );
		//联名卡余额查询
		bool Proc_UPQUERY_BALLANCE_LIST_REQ( unsigned int seqid, const char *xml );
		//查询门店
		bool Proc_UPQUERY_STORE_LIST_REQ( unsigned int seqid, const char *xml );
		//查询门店详情
		bool Proc_UPQUERY_VIEW_STORE_INFO_REQ(unsigned int seqid, const char *xml );
		//新版本查询优惠信息
		bool Proc_UPQUERY_DISCOUNT_LIST_REQ(unsigned int seqid, const char *xml );
		//新版本查询优惠信息详细列表
		bool Proc_UPVIEW_DISCOUNT_INFO_REQ(unsigned int seqid, const char *xml );
		//历史交易记录查询
		bool Proc_UPQUERY_TRADE_LIST_REQ( unsigned int seqid, const char *xml );
		//查询收藏列表
		bool Proc_UPQUERY_FAVORITE_LIST_REQ( unsigned int seqid, const char *xml );
		//查询收藏列表详情
		bool Proc_UPVIEW_FAVORITE_INFO_REQ( unsigned int seqid, const char *xml );
		//添加收藏请求
		bool Proc_UPADD_FAVORITE_REQ( unsigned int seqid, const char *xml );
		//删除收藏请求
		bool Proc_UPDEL_FAVORITE_REQ( unsigned int seqid, const char *xml );
		//获取目的地
		bool Proc_UPGET_DESTINATION_REQ( unsigned int seqid, const char *xml );
		// 发送数据
		void DeliverPacket( unsigned int fd, unsigned int cmd, IPacket *msg);
	private:
		// 处理错误的情况
		void ProcessError( unsigned int seq , bool remove);
	private:
		// 处理环境对象
		IPlugin			   		*_pEnv;
		// 处理XML调用的HTTP对象
		CHttpCaller				_httpcaller;
		// 调用
		ServiceTable			_srv_table;
		// 处理序号消息对应关系
		CSeq2Msg				_seq2msgid;
		// 调用服务URL地址
		CQString 				_callUrl;
		// 数据等待队列
		CMsgQueue				_msgqueue;
		// 结果集管理对象
		CResultMgr			    _resultmgr;
		// 线程执行对象
		share::ThreadManager 	_thread;
		// 信号等待对象
		share::Monitor		 	_monitor;
		// 是否初始化
		bool 				 	_inited;
		// 数据解包对象
		CCarServiceUnPackMgr    _unpacker;
		// 解包工厂
		CPackFactory *			_packfactory;
	};
};
#endif /* SERVICECALLER_H_ */
