/*
 * msgbuilder.h
 *
 *  Created on: 2011-11-8
 *      Author: humingqing
 */

#ifndef __MSGBUILDER_H__
#define __MSGBUILDER_H__

#include <inodeface.h>
#include <nodeheader.h>
#include <Mutex.h>

// 消息构建对象
class CMsgBuilder
{
public:
	CMsgBuilder(IAllocMsg *pAlloc) ;
	~CMsgBuilder() ;

	// 构建登陆处理
	MsgData *BuildLoginReq( unsigned int id, unsigned short group, AddrInfo &info ) ;
	// 构建退出登陆请求
	MsgData *BuildLogoutReq( void ) ;
	// 返回对应的处理在线车辆数
	MsgData *BuildLinkTestReq( unsigned int num ) ;
	// 发送获取用户名的请求
	MsgData *BuildUserNameReq( void ) ;
	// 获取可用的MSG的列表
	MsgData *BuildGetmsgReq( void ) ;
	// 构建用户列表通知消息
	MsgData *BuildUserNotifyReq( UserInfo *p, int count , unsigned int seq ) ;
	// 构建MSG服务变更消息
	MsgData *BuildMsgChgReq( unsigned char op, AddrInfo *p, int count ) ;

	// 构建登陆成功响应
	void BuildLoginResp( DataBuffer &buf, unsigned int seq, unsigned char result ) ;
	// 构建退出登陆响应
	void BuildLogoutResp( DataBuffer &buf, unsigned int seq , unsigned char result ) ;
	// 构建心跳响应的
	void BuildLinkTestResp( DataBuffer &buf, unsigned int seq ) ;
	// 返回请求用户的响应
	void BuildUserNameResp( DataBuffer &buf, unsigned int seq, UserInfo *p , int count ) ;
	// 返回取得服务器
	void BuildGetMsgResp( DataBuffer &buf, unsigned int seq, AddrInfo *p, int count ) ;
	// 构建用户通知响应
	void BuildUserNotifyResp( DataBuffer &buf, unsigned int seq , unsigned short success ) ;
	// 构建MSG更新响应
	void BuildMsgChgResp( DataBuffer &buf, unsigned int seq, unsigned char result ) ;
	// 构建发送的消息
	void BuildMsgBuffer( DataBuffer &buf, MsgData *p ) ;
	// 释放数据
	void FreeMsgData( MsgData *p ) ;

private:
	// 取得序号
	unsigned int GetSequeue( void ) ;

private:
	// 环境对象
	IAllocMsg	 *_pAlloc ;
	// 锁对象
	share::Mutex  _mutex ;
	// 序列号
	unsigned int  _seqid ;
};

#endif /* NODEBUILDER_H_ */
