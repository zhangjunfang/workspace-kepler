/*
 * nodeheader.h
 *
 *  Created on: 2011-11-7
 *      Author: humingqing
 */

#ifndef __NODEHEADER_H__
#define __NODEHEADER_H__

#include <netorder.h>

#pragma pack(1)

#define NODE_CONNECT_REQ   		0x1001   // 连接请求处理
#define NODE_CONNECT_RSP   		0x8001   // 连接响应处理
#define NODE_DISCONN_REQ   		0x1002   // 注销请求消息数据体
#define NODE_DISCONN_RSP   		0x8002   // 注销请求响应
#define NODE_LINKTEST_REQ		0x1003   // 链路心跳测试请求
#define NODE_LINKTEST_RSP   	0x8003   // 链路心跳测试响应
#define NODE_USERNAME_REQ   	0x1004   // 分配用户名和请求
#define NODE_USERNAME_RSP   	0x8004   // 响应分配用户名
#define NODE_USERNOTIFY_REQ 	0x1005   // 纷发用户名的请求
#define NODE_USERNOTIFY_RSP     0x8005   // 纷发用户名的应答
#define NODE_GETMSG_REQ         0x1006   // 取得MSG服务器地址
#define NODE_GETMSG_RSP 		0x8006   // 取得MSG服务器地址列表
#define NODE_MSGERROR_REQ		0x1007   // MSG异常的情况
#define NODE_MSGERROR_RSP		0x8007   // MSG异常的响应
#define NODE_MSGCHG_REQ		    0x1008   // MSG更新操作主要处理MSG新增负载调整
#define NODE_MSGCHG_RSP			0x8008   // MSG更新操作应答

#define NODE_CTFO_TAG          "CTFO"
#define NODE_MSG_DEL			0		// 删除MSG服务器
#define NODE_MSG_ADD			1		// 添加MSG服务器

// 这里管理主要分三种类型 PIPE,STORE,MSG
#define FD_NODE_WEB     		0x1000 // WEB模式只能下发单工
#define FD_NODE_PIPE			0x2000 // 管道模式双工
#define FD_NODE_STORE   		0x4000 // 存储模式什么都接收
#define FD_NODE_MSG				0x8000 // 为MSG服务器模式

// 节点头部数据
struct NodeHeader
{
	unsigned char tag[4] ;   // 默认标记CTFO
	unsigned short cmd ;     // 指令码
	unsigned int   seq ;	 // 序号
	unsigned int   len ;	 // 长度
};

// 节点服务器地址
struct AddrInfo
{
	char ip[32] ;   // IP 地址
	unsigned short port  ;   // 端口
};

// 登陆消息体
struct NodeLoginReq
{
	unsigned int   id ;    // 登陆节点标识
	unsigned short group ; // 所属组的ID 0x1000 WEB , 0x2000 PIPE , 0x4000 STROE , 0x8000 MSG
	AddrInfo       addr ;  // 服务器地址
};

// 登陆应答
struct NodeLoginRsp
{
	unsigned char result ;  // 返回对应码，0成功，1 ID错误，2 IP不正确，3 端口不正确，4其它错误
};

// 注销应答
struct NodeLogoutRsp
{
	unsigned char result ;   // 应答结果成功为0，1失败
};

// 链路维持
struct NodeLinkTestReq
{
	unsigned int num ;  // 在线车辆数
};

// 用户信息
struct UserInfo
{
	char user[12] ;  // 用户名
	char pwd[8] ;    // 密码
};

// 返回成功添加的用户
struct NodeUserNameRsp
{
	UserInfo user ;  // 返回成功添加的用户
};

// 添加用户通知
struct NodeUserNotify
{
	unsigned short num ; // 后续的用户个数
	// 后续数据体为多个用户信息结构体
};

// 添加用户响应
struct NodeUserNotifyRsp
{
	unsigned short success ;// 添加成功的用户个数
};

// 获取MSG的服务的响应
struct NodeGetMsgRsp
{
	unsigned short num ;  // 后续服务器列表中服务器信息个数
};

// 节点错误的处理
struct NodeErrorRsp
{
	unsigned char result ;// 成功0，失败1
};

// MSG结点变更的通知
struct NodeMsgChg
{
	unsigned char  op ;  // 操作 0 表示删除，1表示添加
	unsigned short num ; // 后续服务器个数
};

#pragma pack()

#endif /* NODEHEADER_H_ */
