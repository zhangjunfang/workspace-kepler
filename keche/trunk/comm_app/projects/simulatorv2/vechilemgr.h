/**
 * Author: humingqing
 * date:   2011-09-19
 * memo:  车机模拟器管理对象
 */
#ifndef __VECHILEMGR_H__
#define __VECHILEMGR_H__

#include "interface.h"
#include "bench.h"
#include "simutil.h"
#include <string>
#include <map>
#include <list>
#include <vector>
#include <BaseClient.h>
#include <protocol.h>
#include <GBProtoParse.h>
#include <Thread.h>
#include <TQueue.h>
#include <Util.h>
#include <interpacker.h>

#include "Logistics.h"

using namespace std ;

#define THREAD_SEND    0x0100
#define TCP_MODE	   0x0001
#define UDP_MODE	   0x0002
#define OFF_LINE       0x0001
#define ON_LINE		   0x0002

class CVechileMgr : public BaseClient, public IVechileClient
{
	// 模拟车的信息
	struct _stVechile
	{
		socket_t   *fd_ ;		   // TCP的FD
		socket_t   *ufd_ ;		   // UDP的FD
		int    gps_pos_ ;      // GPS数据的位置
		char   car_id_[6] ;    // BCD车号信息
		short  seq_id_ ;       // 序号ID
		char   termid_[7] ;    // 终端ID
		char   termtype[21] ;  // 终端类型
		char   carnum_[30] ;   // 车牌
		char   phone[12] ;     // 手机号
		char   carcolor ;      // 车牌颜色
		short  proid ;		   // 省域ID
		short  cityid ;		   // 市域ID
		int64_t last_gps_ ;     // 最后一次上报位置的时间
		int64_t last_access_ ;  // 最后一次访问的时间
		int64_t last_conn_ ;    // 最后一次登陆的时间
		int     gps_count_ ;    // 发送GPS数据次数
		int64_t last_pic_  ;    // 最后一次发送PIC
		int64_t last_alam_ ;     // 最后一次告警
		int64_t last_candata_ ;  // 最后一次上传CAN数据
		int64_t lgs_time_ ;		// 最后一次发送时间
		int	   car_state_ ;     // 车辆状态
		_stVechile *_next ;     // 后续指针
		_stVechile *_pre ;      // 前驱指针
	};

public:
	CVechileMgr() ;
	~CVechileMgr() ;

	bool Init( ISystemEnv *pEnv ) ;
	bool Start( void ) ;
	void Stop( void ) ;

public:
	// 数据到来
	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	// 断开连接
	virtual void on_dis_connection( socket_t *sock );
	// 新连接到来
	virtual void on_new_connection( socket_t *sock , const char* ip, int port){};
	// 运行线程对象
	virtual void run( void *param ) ;

protected:
	// 定时线程
	virtual void TimeWork();
	// 心跳线程
	virtual void NoopWork() ;
	// 发送线程
	virtual void SendWork() ;
private:
	// 从文件中加载用户数据
	bool LoadUserFromPath( const char *path ) ;
	// 处理一个数据包
	void HandleOnePacket( socket_t *sock , const char *data, int len ) ;
	// 加载所有用户
	void LoadAllUser( void ) ;
	// 检测在线车辆
	int64_t CheckOnlineUser( int ncount ) ;
	// 检测离线车辆
	bool CheckOfflineUser( void ) ;
	// 登陆服务器
	bool LoginServer(  _stVechile *pVechile ) ;
	// 发送5B数据
	bool Send5BData( socket_t *sock , const char *data, const int len ) ;
	// 构建消息头部
	void BuildHeader( GBheader &header, unsigned short msgid, unsigned short len , _stVechile *p ) ;
	// 发送位置信息
	bool SendLocationPos( _stVechile *p , int ncount ) ;
	// 发送图片数据
	bool SendLocationPic( _stVechile *p , int ncount ) ;
	/*发送透传数据*/
	bool SendTransparentMsg( _stVechile *p , int ncount,unsigned short wType);
	// 取得用户列表
	int  GetUser( list<_stVechile*> &lst , int state ) ;
	// 根据时间段获取速度
	int get_car_speed();

private:
	// 环境对象指针
	ISystemEnv			* _pEnv ;
	// 服务器IP
	string 				  _server_ip ;
	// 服务器端口
	unsigned short 		  _server_port ;
	// 线程池对象
	share::ThreadManager  _send_thread ;
	// 线程数据
	unsigned int 		  _thread_num ;
	// 发送位置包时间间隔
	int64_t 		  	  _time_span ;
	// 模拟车的个数
	unsigned int 		  _vechile_num ;
	// 号码前缀
	unsigned int 		  _phone_numpre ;
	// 开始位置
	unsigned int 		  _start_pos ;
	// 加载GPS数据
	string				  _gps_filepath ;
	// GPS数据
	vector<Point> 		  _gps_vec ;
	// 分包
	C808Spliter		  	  _pack_spliter ;
	// 是否初始化
	bool				  _vechile_inited ;
	// 处理数据模式
	unsigned int 		  _connect_mode ;
	// 序号生成锁
	share::Mutex		  _carmutex ;
	// 所有车辆列表
	TQueue<_stVechile>    _car_queue ;
	// 统计参数对象
	CBench				  _bench ;
	// 掉线用户时间间隔
	int64_t		 		  _deluser_span ;
	// 一次掉线的用户个数
	unsigned int 		  _deluser_num ;
	// 当前已掉线的用户数
	unsigned int		  _deluser_count ;
	// 告警时间间隔
	int64_t 		  	  _alam_time ;
	// 上传图片时间间隔
	int64_t 		  	  _upload_time ;
	// 上传CAN数据时间
	int64_t		  		  _candata_time ;
	// 车牌号第一个字母
	unsigned char		  _simfirst_char ;
	// 最后一次删除用户的时间
	int64_t			  	  _last_deluser ;
	// 上传的图片的URL
	string 				  _pic_url ;
	/*物流*/
	CLogistics        	 *_logistics;
	//加载物流模拟数据路径
	string            	  _logistics_path;
	// 序号产生锁
	share::Mutex          _seq_mutex ;
	// 用户文件路径
	string 				  _sim_car_user ;
	// 图片列表锁
	share::Monitor		   _picmonitor;
	// 下发图片列表
	std::list<_stVechile*> _piclist ;

	map<int,int>          _car_speed;
};

#endif
