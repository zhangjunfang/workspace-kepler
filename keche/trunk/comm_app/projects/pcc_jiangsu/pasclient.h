/**********************************************
 * pasclient.h
 *
 *  Created on: 2011-07-28
 *      Author: humingqing
 *    Comments: 与监管平台对接处理
 *********************************************/

#ifndef __PASCLIENT_H__
#define __PASCLIENT_H__

#include "interface.h"
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <BaseClient.h>
#include <protocol.h>
#include "busloader.h"
#include "mypacker.h"
#include "statinfo.h"

#define PCC_USER_LOGIN    0x01
#define PCC_USER_ACTVIE   0x02
#define PCC_USER_LOOP 	  0x04

class PasClient : public BaseClient , public IPasClient
{
	class PccUser
	{
		enum USERSTATE { OFF_LINE= 0, ON_LINE=1 , WAIT_RESP=2 } ;
	public:
		PccUser(){
			_fd = NULL ;
			_login_time = _active_time = _loop_time = 0 ;
			_user_state = OFF_LINE ;
			_tcp        = false ;
		}

		// 更新最后使用时间
		void Update( int flag ) {
			if ( flag & PCC_USER_LOGIN )
				_login_time = time(NULL);
			if ( flag & PCC_USER_ACTVIE )
				_active_time = time(NULL) ;
			if ( flag & PCC_USER_LOOP )
				_loop_time = time(NULL) ;
		}

		// 检测是否超时
		bool Check( int timeout , int flag ){
			time_t now = time(NULL) ;
			if ( flag & PCC_USER_LOGIN ) {
				if ( now - _login_time > timeout )
					return true;
			}
			if ( flag & PCC_USER_ACTVIE ) {
				if ( now - _active_time > timeout )
					return true ;
			}
			if ( flag & PCC_USER_LOOP ) {
				if ( now - _loop_time > timeout )
					return true ;
			}
			return false ;
		}

		// 用户状态处理
		bool IsOnline( void ) {  return ( _user_state == ON_LINE && _fd != NULL ) ; }
		bool IsOffline( void ) { return ( _user_state == OFF_LINE ); }
		void SetOnline( void ) { _user_state = ON_LINE ; }
		void SetOffline( void ) { _user_state = OFF_LINE; }
		void SetWaitResp( void ){ _user_state = WAIT_RESP; }

	public:
		socket_t *_fd ;  			// 用户连接FD
		string   _srv_key ;      // 成功后返回的KEY值
		string   _srv_ip ; 		// 登陆服务IP地址
		short 	 _srv_port ; 	// 登陆陆服务器端口
		string   _username;		// 登陆用户名
		string   _password;		// 登陆的密码
		bool 	 _tcp ;			// 是否为TCP连接

	private:
		time_t  	_login_time ;   // 最后一次登陆的时间
		time_t  	_active_time ;  // 最后一次使用的时间
		time_t		_loop_time;     // 心跳的时间
		USERSTATE	_user_state ;   // 用户状态是否在线
	};

	// 用户会话管理
	class UserSession
	{
	public:
		UserSession(){
			_tcp._tcp = true  ;
			_udp._tcp = false ;
		}
		~UserSession(){}

		// 是否在线
		bool IsOnline( bool tcp ) {
			share::Guard guard( _mutex ) ;
			return (tcp) ? _tcp.IsOnline(): _udp.IsOnline();
		}

		// 是否离线
		bool IsOffline( bool tcp ) {
			share::Guard guard( _mutex ) ;
			return (tcp) ? _tcp.IsOffline() : _udp.IsOffline() ;
		}

		// 是否在线
		void SetState( bool online, bool tcp ) {
			share::Guard guard( _mutex ) ;
			if( tcp ){
				( online ) ? _tcp.SetOnline() : _tcp.SetOffline() ;
			} else {
				( online ) ? _udp.SetOnline() : _udp.SetOffline() ;
			}
		}

		void Update( int flag , bool tcp ) {
			share::Guard guard( _mutex ) ;
			if ( tcp )
				_tcp.Update( flag ) ;
			else
				_udp.Update( flag ) ;
		}

		bool Check( int timeout, int flag, bool tcp ){
			share::Guard gurad( _mutex ) ;
			if ( tcp )
				return _tcp.Check( timeout, flag ) ;

			return _udp.Check( timeout, flag ) ;
		}

		void SetUser( PccUser &user, bool tcp ) {
			share::Guard guard( _mutex ) ;
			if ( tcp ){
				_tcp 	  = user ;
				_tcp._tcp = true ;
			}else{
				_udp 	  = user ;
				_udp._tcp = false ;
			}
		}

		PccUser & GetUser( bool tcp ) {
			share::Guard guard( _mutex ) ;
			if ( tcp )
				return _tcp ;
			return _udp ;
		}

		bool IsKey( bool tcp ) {
			share::Guard guard( _mutex ) ;
			if ( tcp )
				return (!_tcp._srv_key.empty()) ;

			return (!_udp._srv_key.empty()) ;
		}

		const char * GetKey( bool tcp ) {
			share::Guard guard( _mutex ) ;
			if ( tcp )
				return _tcp._srv_key.c_str() ;
			return _udp._srv_key.c_str() ;
		}

		void SetSrvId( const char *srvid ){
			share::Guard guard( _mutex ) ;
			_serverid = srvid ;
		}

		const char * GetSrvId( void ) {
			share::Guard guard( _mutex ) ;
			return _serverid.c_str() ;
		}

		void DisConnect( socket_t *sock ) {
			share::Guard guard( _mutex ) ;

			if ( _tcp._fd == sock ) {
				_tcp._fd = NULL ;
				_tcp.SetOffline() ;
			} else if ( _udp._fd == sock ){
				_udp._fd = NULL ;
				_udp.SetOffline() ;
			}
		}

		// 根据FD的值来取得是TCP还是UDP
		bool GetUser( socket_t *fd , PccUser &user ) {
			share::Guard guard( _mutex ) ;

			if ( _tcp._fd == fd ) {
				user = _tcp ;
				return true ;
			}

			if ( _udp._fd == fd ) {
				user = _udp ;
				return true ;
			}
			return false ;
		}

	private:
		// 当前TCP的用户
		PccUser 		_tcp ;
		// 当前UDP的用户
		PccUser			_udp ;
		// 用户状态管理锁
		share::Mutex    _mutex ;
		// 传通数据数据ID
		string 			_serverid ;
	};

public:
	PasClient( CStatInfo *stat ) ;
	virtual ~PasClient() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始
	virtual bool Start( void ) ;
	// 停止
	virtual void Stop( void ) ;
	// 向PAS交数据
	virtual bool HandleData( const char *data, int len ) ;

	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	virtual void on_dis_connection( socket_t *sock );
	//为服务端使用
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};

	virtual void TimeWork();
	virtual void NoopWork();
	// 取得服务器的IP
	virtual const char * GetSrvId( void ) {  return _user.GetSrvId(); }
	// 初始化UDP的服务
	virtual bool StartUDP( const char * ip, int port, int thread ) ;
	// 是否在线
	virtual bool IsOnline( void ) { return (_user.IsOnline(false) && _user.IsOnline(true)) ; }

private:
	// 纷发接收到的数据
	void HandleRecvData( socket_t *sock, const char *data, int len ) ;
	// 纷发控制数据
	void HandleCtrlData( socket_t *sock, const char *data, int len ) ;
	// 检测用户状态
	void CheckUserState( UserSession &user ) ;
	// 检测用户心跳
	void CheckUserLoop( UserSession &user ) ;
	// 重连控制连接还是数据连接
	void ConnectServer( UserSession &user, bool tcp ) ;

private:
	// 环境指针处理
	ISystemEnv  *		_pEnv ;
	// 当前用户对象
	UserSession		    _user ;
	// 数据分包对象
	CMyPackSpliter      _packspliter;
	// UDP服务器用参数
	unsigned short      _port ;
	// 服务器IP地址
	string 				_ip ;
	// 车辆静态信息加载对象
	BusLoader  			_busloader ;
	// 服务器连接IP
	string 			    _srvip ;
	// 处理统计
	CStatInfo		   *_statinfo ;
};

#endif /* LISTCLIENT_H_ */
