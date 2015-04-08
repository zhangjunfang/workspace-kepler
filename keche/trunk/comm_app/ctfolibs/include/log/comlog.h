/**
 * author: Liubo
 * date: 2011-09-23
 * memo: <humingqing> 日志管理对象,日志可以根据级别来设置是否要处理，如果级别越高则数值越小
 * 		当设置级别越高时显示记录的日志就越少，当为零时关闭日志,1级日志只为错误和警告，2级为一常用数据流程，3级为调试级别
 */
#ifndef _HEAD_COMLOG_H
#define _HEAD_COMLOG_H

#include "cLog.h"

#ifdef _XDEBUG
#define OUT_DEBUG( fmt, ... )  		printf( fmt, ## __VA_ARGS__ )
#define DEBUG_PRT(fmt, ...)			debug_printf(__FILE__, __LINE__, fmt, ## __VA_ARGS__)
#else
#define OUT_DEBUG( fmt, ... )
#define DEBUG_PRT(fmt, ...)
#endif
#define INFO_PRT(fmt, ...)			info_printf(fmt, ## __VA_ARGS__)

#define CHGLOG(pathname)     CCLog::GetInstance()->set_log_file(pathname);
#define CHGLOGSIZE(newsize)  CCLog::GetInstance()->set_log_size(newsize);
#define CHGLOGNUM(log_num)   CCLog::GetInstance()->set_log_num(log_num);
#define FFLUSH
#define SETLOG(pathname)     CCLog::GetInstance()->set_log_file(pathname);
#define SETLOGSIZE(newsize)  CCLog::GetInstance()->set_log_size(newsize);
#define SETLOGNUM(log_num)   CCLog::GetInstance()->set_log_num(log_num);
#define SETLOGLEVEL(level)   CCLog::GetInstance()->set_log_level(level);
#define LOG_NUM_LEVEL(level) level, __FILE__, __LINE__, __FUNCTION__
#define LOG_NULL_LEVEL(level) level, NULL, 0, NULL

// 一级日志，错误报警，这类需要记录错误出现的所在文件行号以及函数名称
#define OUT_WARNING(ip,port,user_id,msg,...)\
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NUM_LEVEL(1),"WARN",ip,port,user_id,msg,  ## __VA_ARGS__)
#define OUT_ERROR(ip,port,user_id,msg,...)\
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NUM_LEVEL(1),"ERROR",ip,port,user_id,msg,  ## __VA_ARGS__)
#define OUT_ASSERT(ip,port,user_id,msg,...)\
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NUM_LEVEL(1),"ASSERT",ip,port,user_id,msg,  ## __VA_ARGS__)
#define OUT_RUNNING(ip,port,user_id,msg,...)\
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NUM_LEVEL(1),"RUNNING",ip,port,user_id,msg,  ## __VA_ARGS__)

// 二级日志，流程简单数据
#define OUT_INFO( ip, port, user_id, msg, ...)\
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NULL_LEVEL(2),"INFO",ip,port,user_id,msg,  ## __VA_ARGS__)
#define OUT_RECV( ip, port, user_id, msg, ...)\
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NULL_LEVEL(2),"RECV",ip,port,user_id,msg,  ## __VA_ARGS__)
#define OUT_SEND( ip, port, user_id, msg, ...)\
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NULL_LEVEL(2),"SEND",ip,port,user_id,msg,  ## __VA_ARGS__)
#define OUT_CONN( ip, port, user_id, msg, ...)\
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NULL_LEVEL(2),"CONN",ip,port,user_id,msg,  ## __VA_ARGS__)
#define OUT_DISCONN( ip, port, user_id, msg, ...)\
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NULL_LEVEL(2),"DISCON",ip,port,user_id,msg,  ## __VA_ARGS__)

// 三级日志为调试处理
#define OUT_RECV3( ip, port, user_id, msg, ...) \
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NULL_LEVEL(3),"RECV",ip,port,user_id,msg,  ## __VA_ARGS__)
#define OUT_SEND3( ip, port, user_id, msg, ... ) \
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NULL_LEVEL(3),"SEND",ip,port,user_id,msg,  ## __VA_ARGS__)
#define OUT_PRINT( ip, port, user_id,msg, ... ) \
		CCLog::GetInstance()->CCLog::print_net_msg(LOG_NULL_LEVEL(3),"PRINT",ip,port,user_id,msg,  ## __VA_ARGS__)
// 输出十六进制数据
#define OUT_HEX( ip, port, user_id, data, len )  \
		CCLog::GetInstance()->print_net_hex(LOG_NULL_LEVEL(3), ip, port, user_id, data, len )


#endif  // #ifndef _HEAD_COMLOG_H


