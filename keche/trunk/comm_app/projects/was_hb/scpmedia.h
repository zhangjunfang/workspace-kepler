#ifndef __SCPMEDIA_H__
#define __SCPMEDIA_H__

#include "interface.h"
#include <inetfile.h>
#include <dataqueue.h>
#include <TQueue.h>
#include <map>
#include <vector>
#include <string>
using namespace std ;

#define SCP_MAX_TIMEOUT     60  // 1分钟数据超时

class CScpMedia : public IQueueHandler
{
	struct _stNetObj
	{
		INetFile    *_pobj ;
		share::Mutex _mutex ;
	};
	// 文件描述
	struct MultiDesc
	{
		int _fd    ;
		int _total ;
		int _id    ;
		int _type  ;
		int _ftype ;
		int _event ;
		int _channel ;
		string _macid ;
		string _name ;
		string _path ;
		string _gps  ;
		string _abspath ;  // 相对路径
		MultiDesc *_next;  // 下一个数据指针

		void Copy( const MultiDesc &desc ){
			_fd 	 = desc._fd ;
			_total   = desc._total ;
			_id      = desc._id ;
			_type    = desc._type ;
			_ftype   = desc._ftype ;
			_event   = desc._event ;
			_channel = desc._channel ;
			_macid   = desc._macid ;
			_name    = desc._name ;
			_path    = desc._path ;
			if ( ! desc._gps.empty() )
				_gps = desc._gps  ;
			_abspath = desc._abspath ;
		}
	};

	// 处理内存文件，这是主要针对需分包的文件处理
	class CScpFile
	{
		// 内存文件块对象
		struct MemFile
		{
			int      _index ;
			int      _len ;
			char    *_ptr ;
		};
	public:
		CScpFile( const char *key ) ;
		~CScpFile() ;
		// 初始化对象
		bool Init( int fd, const char *macid, int id, int total, int type, int ftype, int event,
				int channel , const char *curdir , const char *gps ) ;
		// 设置参数处理
		bool SetParam( int fd, const char *macid, int id, int total, int type, int ftype, int event,
				int channel , const char *curdir , const char *gps ) ;
		// 保存文件
		bool SaveData( const int index, const char * data, int len , MultiDesc *desc ) ;

	private:
		// 当前存放条数
		unsigned int 		_cur  ;
		// 存放描述数据
		MultiDesc  			_desc ;
		// 存放内存数据
		vector<MemFile *>   _vec ;
		// 设置当前时间
		time_t 				_now ;

	public:
		std::string 		_key ;
		// 最后一次时间
		time_t 				_last ;
		// 使用TQueue的指针对象
		CScpFile *  		_next ;
		CScpFile * 	 		_pre ;
	};

	typedef map<string, CScpFile *>   MapScpFile ;

	// 管理所有的SCP文件
	class CScpFileManager
	{
	public:
		CScpFileManager() ;
		~CScpFileManager() ;

		// 初始化对象
		bool Init( ISystemEnv *pEnv ) ;

		// 保存数据包处理
		bool SaveScpFile( int fd, const char *macid, int id, int total, int index, int type,
					int ftype,int event,int channel, const char * data, int len , const char *gps,  MultiDesc *desc ) ;
		// 检测超时的内存文件
		void CheckTimeOut( const int timeout ) ;
		// 清除指定的MAC车机的多媒数据
		void RemoveScpFile( const char *macid ) ;

	private:
		// 资源锁
		share::Mutex		    _mutex ;
		// SCP分块文件
		MapScpFile			    _index ;
		// 超时索引
		TQueue<CScpFile>		_queue ;
		// 存放文件目录
		std::string 			_cur_dir ;
		// 系统对象
		ISystemEnv			   *_pEnv ;
	};

public:
	CScpMedia() ;
	~CScpMedia() ;

	bool Init( ISystemEnv *pEnv ) ;
	bool Start( void ) ;
	void Stop( void ) ;
	// 保存数据包处理
	bool SavePackage( int fd, const char *macid, int id, int total, int index, int type,
			int ftype,int event,int channel, const char * data, int len , const char *gps ) ;
	// 清除车机的数据
	void RemovePackage( const char *macid ) ;
	// 检测超时处理
	void CheckTimeOut( void ) ;

public:
	// 线程执行对象方法
	virtual void HandleQueue( void *packet ) ;

private:
	// 写入远程文件
	bool writeFile( const char *dir, const char *name, const char *path ) ;

private:
	// 环境对象指针
	ISystemEnv			*  _pEnv ;
	// 数据发布队列
	CDataQueue<MultiDesc> *_mediaqueue ;
	// 数据线程队列
	CQueueThread 		 * _queuethread;
	// 线程个数
	unsigned int 		   _thread_num ;
	// FTP的IP
	string 				   _cfs_ip ;
	// FTP登陆的用户名
	string 				   _cfs_user ;
	// FTP登陆的用户名
	string 				   _cfs_pwd ;
	// FTP的端口
	unsigned int 		   _cfs_port ;
	// 最后一次检测时间
	time_t				   _last_check ;
	// 管理所有SCP文件
	CScpFileManager		   _scp_manager ;
	// 是否开启FTP
	bool 				   _cfs_enable ;
	// 是否开启日期水印
	bool 				   _water_enable;
	// 网络数据对象
	_stNetObj			   _netobj ;
};

#endif
