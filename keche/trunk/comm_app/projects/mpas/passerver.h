/**********************************************
 * PasServer.h
 *
 *  Created on: 2011-08-04
 *      Author: huminqing
 *       Email:qshuihu@gmail.com
 *    Comments: 使用分包接口实现任何数据协议分包处理
 *********************************************/

#ifndef _PASSERVER_H_
#define _PASSERVER_H_

#include <map>
#include <string>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <BaseServer.h>
#include "interface.h"
#include <OnlineUser.h>
#include <protocol.h>
#include <time.h>
#include <Session.h>
#include "pconvert.h"

#define BUF_LEN  			1024
#define USER_TIMEOUT		60*3
#define USER_WAITTIME		60

class PasServer : public BaseServer, public IPasServer
{
	class CKeyMgr
	{
		struct _Key
		{
			unsigned int M1  ;  	 // 加密密钥1  M1_IA1_IC1
			unsigned int IA1 ;  	 // 加密密钥2
			unsigned int IC1 ;  	 // 加密密钥3
		};
		typedef std::map<unsigned int,_Key>  CMapKey ;
	public:
		CKeyMgr() ;
		~CKeyMgr() ;
		// 添加到加密密钥管理中
		void AddEncryptKey( unsigned int access, const char *key ) ;
		// 取得加密密钥
		bool GetEncryptKey( unsigned int access, unsigned int &M1 , unsigned int &IA1 , unsigned int &IC1 ) ;

	private:
		// 当前记录数据个数
		int 		  _size ;
		// 管理加密的KEY
		CMapKey   	  _keys ;
		// 同步管理锁操作
		share::Mutex  _mutex ;
	};
public:
	PasServer( PConvert *convert );
	~PasServer();

	// 初始化
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始线程
	virtual bool Start( void ) ;
	// 停止处理
	virtual void Stop( void ) ;
	// 客户对内纷发数据
	virtual void HandlePasDown( const char *code , const char *data, const int len ) ;

protected:
	// 处理数据包
	void HandleOnePacket( socket_t *sock,  const char *data, int data_len );

	virtual void on_data_arrived( socket_t *sock, const void* data, int len );
	virtual void on_dis_connection( socket_t *sock );
	virtual void on_new_connection( socket_t *sock, const char* ip, int port );

private:
	bool ConnectServer( User &user, unsigned int timeout );

	virtual void TimeWork() ;
	virtual void NoopWork() ;

	// 数据加解密
	bool EncryptData( unsigned char *data, unsigned int len , bool encode ) ;
	// 发送指定的接入码用户
	bool SendDataToPasUser( const string &access_code ,const char *data, int len ) ;
	// 在线用户
	void HandleOnlineUsers(int timeval);
	// 离线用户
	void HandleOfflineUsers();
	// 取得车牌和手机号之间对应关系
	bool GetMacIdByVechicle( const char *vechicle, unsigned char color, string &macid ) ;
	// 检测用户是否合法
	int  CheckLogin( unsigned int accesscode, const char *username=NULL, const char *password=NULL ) ;
	// 发送5B编码的数据
	bool Send5BCodeData( socket_t *sock, const char *data, int len ) ;
	// 重置循环
	void ResetCrcCode( char *data, int len ) ;
	// 发送加密数据
	bool SendCrcData( socket_t *sock, const char* data, int len) ;

private:
	time_t				_last_handle_user_time ;
	// 在线用户队列
	OnlineUser 			_online_user;
	// 处理线程
	unsigned int 		_thread_num ;
	// 服务器监听IP
	std::string 		_ip ;
	// 端口处理
	unsigned short      _port ;
	// 环境对象指针
	ISystemEnv		   *_pEnv ;
	// 接入码
	unsigned int 	    _verify_code ;
	// 车牌号到手机号的转换关系
	CSessionMgr		    _carnum2phone ;
	// 协议转换对象
	PConvert		   *_convert ;
	// 图片保存路径
	string 				_rootpath ;
	// 管理加密密钥
	CKeyMgr				_keymgr ;
};

#endif


