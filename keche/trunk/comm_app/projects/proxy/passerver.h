/**********************************************
 * PasServer.h
 *
 *  Created on: 2010-10-11
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments: 2011-07-24 humingqing
 *    修改，统一到环境对象部件中，主要去掉缓存队列，实现由接收线程驱动数据处理，这样省掉不必要的中间环节，提高效率，
 *    也是一个系统设计时必需考虑，一个好系统，当局部出现故障时应该能及时的反馈到源头，源头立即采用措施，这才是一个健壮的系统设计。
 *********************************************/

#ifndef __PASSERVER_H_
#define __PASSERVER_H_

#include "interface.h"
#include <NetHandle.h>
#include "ProtoParse.h"
#include <OnlineUser.h>
#include <crc16.h>
#include <BaseServer.h>
#include "filecacheex.h"
#include "corpinfohandle.h"
#include "logincheck.h"
#include "datastat.h"

class PasServer :
	public BaseServer, public IPasServer,public IOHandler
{
public:
	PasServer();
	~PasServer();
	// 初始化
	virtual bool Init(ISystemEnv *pEnv ) ;
	// 开始线程
	virtual bool Start( void ) ;
	// 停止处理
	virtual void Stop( void ) ;
	// 客户对内纷发数据
	virtual void HandleClientData( const char *data, const int len ) ;
	// 接收到数据
	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	// 断开连接
	virtual void on_dis_connection( socket_t *sock );
	// 处理数据的回调方法
	virtual int HandleQueue( const char *id , void *buf, int len , int msgid = 0 ) ;

private:
	// 处理数据包
	void HandleOnePacket( socket_t *sock,  const char *data, int data_len );
	// 根据接入码发送对应的用户
	bool SendDataToPasUser( unsigned int accesscode, const char *data, int len );

	bool ConnectServer(User &user, unsigned int timeout);

	virtual void TimeWork() ;
	virtual void NoopWork() ;
	// 加密或解密数据
	bool EncryptData( unsigned char *data, unsigned int len , bool encode ) ;
	// 发送需进行5B转码的数据
	bool Send5BCodeData( socket_t *sock, const char *data, int len ) ;
	// 发送重新处理循环码的数据
	bool SendCrcData( socket_t *sock, const char* data, int len) ;

	void HandleOnlineUsers(int timeval);
	void HandleOfflineUsers();
	// 重组CRC的CODE
	void  ResetCrcCode( char *data, int len ) ;

private:
	// 环境对象指针
	ISystemEnv		   * _pEnv ;
	// 协议解析
	ProtoParse 			 _proto_parse;
	// 在线用户队列
	OnlineUser 			 _online_user;
	// 最后访问的时间
	time_t 				 _last_check;
	// 较验码
	unsigned int 		 _verify_code;
	// 线程个数
	unsigned int 		 _thread_num ;
	// 处理批量
	int 				 _max_send_num ;
	// 文件数据缓存
	CFileCacheEx		 _filecache;
	// 存放车与接入码以及统计指令的队列
	CorpInfoHandle 		 _corp_info_handler;
	// 加载用户信息的对象
	LoginCheck 			*_login_check;
	// 数据流速统计
	CDataStat			 _datastat ;
	// 总流速统计
	CStat				 _allstat ;
};

#endif


