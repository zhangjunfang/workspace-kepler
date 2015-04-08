/*
 * usermgr.h
 *
 *  Created on: 2011-11-9
 *      Author: humingqing
 */

#ifndef __USERMGR_H__
#define __USERMGR_H__

#include <map>
#include <string>
#include <Mutex.h>
#include "nodeheader.h"
#include <tools.h>
#include <databuffer.h>

class CUserMgr
{
	typedef std::map<std::string, UserInfo*>  CMapUser ;
public:
	CUserMgr( const char *path ):_userid(0)
	{
		printf( "user dir: %s\n", path ) ;
		sprintf( _szdir, "%s", path ) ;
		Restore();
	} ;
	~CUserMgr(){ ClearAll(); };

	// 取得用户，当index为-1时生成新的用户ID
	bool GetUser( const char *id, char user[13] , char pwd[9] )
	{
		share::Guard guard( _mutex ) ;
		{
			CMapUser::iterator it = _mapuser.find( id ) ;
			if ( it != _mapuser.end() ) {
				// 如果已存在就直接返回
				UserInfo *info = it->second ;
				safe_memncpy( user, info->user, sizeof(info->user) ) ;
				safe_memncpy( pwd , info->pwd , sizeof(info->pwd ) ) ;
				// 如果已存在直接返回成功
				return true ;
			}

			// 如果未存在就生成一个ID
			int userid = GenUserId() ;
			sprintf( user, "ctfo%08d" , userid ) ;
			sprintf( pwd , "%08d" , userid ) ;

			UserInfo *info = new UserInfo;
			safe_memncpy( info->user, user , sizeof(info->user) ) ;
			safe_memncpy( info->pwd , pwd,   sizeof(info->pwd ) ) ;
			_mapuser.insert( make_pair(id,info) ) ;

			// 备份添加的用户数据
			Backup( id, info ) ;

			// 生成一个新用户返回不存在
			return false ;
		}
	}

	// 取得当前用户数
	bool GetUsers( UserInfo *arr , int count ) {
		share::Guard guard( _mutex ) ;
		{
			if ( _mapuser.empty() || count > (int) _mapuser.size() ) {
				return false ;
			}

			int i = 0 ;
			CMapUser::iterator it ;
			for ( it = _mapuser.begin(); it != _mapuser.end(); ++ it ) {
				UserInfo *p = it->second ;
				memcpy( &arr[i] , p, sizeof(UserInfo) ) ;
				++ i ;
			}
			return true ;
		}
	}

	// 取得用户个数
	int  GetSize( void )
	{
		share::Guard guard( _mutex ) ;
		{
			return (int)_mapuser.size() ;
		}
	}

protected:
	// 备份生成的用户
	void Backup( const char *id, UserInfo *info )  {
		DataBuffer buf ;

		uint16_t nlen = (uint16_t)strlen(id) ;
		buf.writeInt16( nlen ) ;
		buf.writeBlock( id, nlen ) ;
		buf.writeBlock( info, sizeof(UserInfo) ) ;
		// 追加数据
		AppendFile( _szdir , buf.getBuffer(), buf.getLength() ) ;
	}

	// 从备份文件中恢复数据
	void Restore() {
		int   len = 0 ;
		char *ptr = ReadFile( _szdir, len ) ;
		if ( ptr == NULL || len == 0 )
			return ;

		DataBuffer buf ;
		buf.writeBlock( ptr, len ) ;
		FreeBuffer( ptr ) ;

		char szid[1024] = {0} ;
		int pos = 0 ;
		while ( pos < len ) {
			uint16_t nlen = buf.readInt16() ;
			if ( nlen > 1024 || nlen > len || nlen == 0 ) {
				break ;
			}
			pos = pos + sizeof(uint16_t) ;

			if ( ! buf.readBlock( szid, nlen ) ) {
				break ;
			}
			szid[nlen] = 0 ;
			pos = pos + nlen ;

			UserInfo *info = new UserInfo;
			if ( ! buf.readBlock( info, sizeof(UserInfo) ) ){
				delete info ;
				break ;
			}
			pos = pos + sizeof(UserInfo) ;
			// 查找用户ID是否存在
			if ( _mapuser.find(szid) != _mapuser.end() )
				continue ;
			_mapuser.insert( make_pair(szid,info) ) ;

			// 自动增加计数的ID号
			++ _userid ;
		}
	}

private:
	// 产生用户ID
	int  GenUserId( void ) { return ++ _userid ; }
	// 回收内存
	void ClearAll( void ) {
		share::Guard guard( _mutex ) ;
		{
			if ( _mapuser.empty() )
				return ;

			CMapUser::iterator it ;
			for( it = _mapuser.begin(); it != _mapuser.end(); ++ it ) {
				delete it->second ;
			}
			_mapuser.clear() ;
		}
	}

private:
	// 产生临时用户的ID锁
	share::Mutex _mutex ;
	// 用户ID对象
	unsigned int _userid ;
	// 用户列表
	CMapUser	 _mapuser ;
	// 用户数据保存路径
	char 		 _szdir[1024] ;
};

#endif /* USERMGR_H_ */
