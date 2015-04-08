/*
 * picclient.h
 *
 *  Created on: 2012-11-15
 *      Author: humingqing
 *  媒体图片数据上传部件
 */

#ifndef __PICCLIENT_H__
#define __PICCLIENT_H__

#include <interface.h>
#include <dataqueue.h>

class INetFile;
class CPicClient : public IQueueHandler
{
	struct _picData
	{
		std::string _data ;
		_picData *  _next ;
	};
public:
	CPicClient() ;
	~CPicClient() ;

	bool Init( ISystemEnv *pEnv ) ;
	bool Start( void ) ;
	void Stop( void ) ;

	// 添加媒体数据
	bool AddMedia( const char *data, int len ) ;
	// 处理数据队列缓存
	void HandleQueue( void *packet ) ;

private:
	// 写入远程文件服务
	bool writeFile( const char *path ) ;
	// 保存到服务器上处理
	bool saveFile( const char *path, const char *ptr, int len ) ;
	// 通过HTTP读取图片处理
	bool writeHttp( const char *path ) ;

private:
	// 系统环境对象
	ISystemEnv *		  _pEnv ;
	// 数据发布队列
	CDataQueue<_picData> *_picqueue ;
	// 数据线程队列
	CQueueThread 		 *_queuethread;
	// 图片数据读取路径
	std::string 		  _basedir ;
	// 下载图片数据路径
	std::string 		  _baseurl ;
	// 图片文件上传处理
	INetFile    		 *_pobj ;
	// FTP的IP
	std::string 		  _cfs_ip ;
	// FTP登陆的用户名
	std::string 		  _cfs_user ;
	// FTP登陆的用户名
	std::string 		  _cfs_pwd ;
	// FTP的端口
	unsigned int 		  _cfs_port ;
};


#endif /* PICCLIENT_H_ */
