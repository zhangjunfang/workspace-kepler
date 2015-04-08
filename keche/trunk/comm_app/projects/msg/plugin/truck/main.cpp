/*
 * main.cpp
 *
 *  Created on: 2012-6-6
 *      Author: humingqing
 *
 */

#include <comlog.h>
#include <main_app.h>
#include <utility.h>
#include <sys/time.h>
#include <sys/resource.h>
#include "systemenv.h"
#include <tools.h>
#include <stdlib.h>
#include "packfactory.h"
#include <BaseTools.h>

#define LOG_PATH 			"/var/lbs/log/ws"
#define CONF_PATH   		"/usr/local/lbs/conf/ws"
#define CONFIG  			"msg.conf"
#define MTRANS_ENV			"MTRANS_PRJ_HOME"
#define ABS_LOG_PATH		"/lbs/log/ws"
#define ABS_CONF_PATH		"/lbs/conf/ws"
#define LOG_NAME			"msg.log"
#define RUN_LOG_PATH		"log/ws"
#define RUN_CONF_PATH		"conf/ws"

using namespace TruckSrv;

void SendMessage( IPlugWay *way , int n , CPackFactory &factory);
int main( int argc, char **argv )
{
	MainApp app;
	//	打包时记得因为测试而关闭的等待连接响应包部分
	app.InitMainApp(argc, argv);

	char *szrun = app.getArgv("r") ;
	char szlog[1024] = {0} ;
	if ( szrun != NULL )
		sprintf( szlog, "%s/%s", szrun, RUN_LOG_PATH ) ;
	else
		getrunpath( MTRANS_ENV, szlog, ABS_LOG_PATH , LOG_PATH ) ;

	char szconf[1024] = {0} ;
	if ( szrun != NULL )
		sprintf( szconf, "%s/%s/%s", szrun, RUN_CONF_PATH , CONFIG ) ;
	else
		getconfpath( MTRANS_ENV, szconf, ABS_CONF_PATH , CONF_PATH , CONFIG ) ;

	CSystemEnv env ;
	if ( ! env.Init( szconf, szlog , LOG_NAME ) ){
		printf( "CSystemEnv init failed\n" ) ;
		return 0 ;
	}

	if ( ! env.Start() ){
		printf( "CSystemEnv start failed\n" ) ;
		return 0 ;
	}

	int index = 0 ;

	CTruckUnPackMgr packer ;
	CPackFactory  factory(&packer) ;

	IPlugWay *way = env.GetPlugWay() ;
	while (1) {

		SendMessage( way, index , factory ) ;

		index = (index) % 11;
		printf("index:%d\n",index);
		index++;

		sleep(1) ;
	}

//	pipe_server.Stop();
	sleep(1);

	INFO_PRT("press enter to leave!");
	getchar();

	return 0;
}

void SendMessage( IPlugWay *way , int n , CPackFactory &factory)
{
	CPacker pack ;
	switch( n )
	{
	case 0:
		{
			CQueryCarDataReq req ;
			req._srcarea  = 10000 ;
			req._destarea = 10001 ;
			strtoBCD( "201206061428", (char*)req._time ) ;
			req._offset   = 0 ;
			req._count    = 10 ;
			factory.Pack( &req, pack ) ;
		}
		break ;
	case 1:
		{
			CSendScheduleRsp rsp ;
			rsp._result = 0 ;
			factory.Pack( &rsp, pack ) ;
		}
		break ;
	case 2:
		{
			CQueryScheduleReq req ;
			req._num = 10 ;
			factory.Pack( &req, pack ) ;
		}
		break ;
	case 3:
		{
			CUploadScheduleReq req ;
			safe_memncpy( (char*)req._scheduleid, "1", sizeof(req._scheduleid) ) ;
			req._matchstate = 0 ; 		//  UINT8	匹配状态(0成功，1失败)
			safe_memncpy( (char*)req._hangid, "1", sizeof(req._hangid));		//	String	挂车ID
			safe_memncpy( (char*)req._hangnum , "1", sizeof(req._hangnum));		//	String	挂车车牌号
			safe_memncpy( (char*)req._mtime , "201266" , sizeof(req._mtime)) ;			//  String	匹配时间
			req._info._alam  = 0 ;		//	4	UINT32	报警标志位
			req._info._state = 0 ;	//	4	UINT32	状态位
			req._info._lon   = 0 ;		//  4	UINT32	经度
			req._info._lat	 = 0 ;		//	4	UINT32	纬度
			req._info._height= 0 ;	//	2	UINT16	高程
			req._info._speed = 0 ;	//	2	UINT16	速度
			req._info._direction = 0 ;//	2	UINT16	方向
			safe_memncpy( (char*)req._info._time , "201266" , sizeof(req._info._time)) ;
			factory.Pack( &req, pack ) ;
		}
		break ;
	case 4:
		{
			CStateScheduleReq req ;

			safe_memncpy( (char*)req._scheduleid, "1", sizeof(req._scheduleid) ) ;
			req._action = 0 ; 		//  UINT8	匹配状态(0成功，1失败)
			safe_memncpy( (char*)req._hangid, "1", sizeof(req._hangid));		//	String	挂车ID
			safe_memncpy( (char*)req._hangnum , "1", sizeof(req._hangnum));		//	String	挂车车牌号

			req._info._alam  = 0 ;		//	4	UINT32	报警标志位
			req._info._state = 0 ;	//	4	UINT32	状态位
			req._info._lon   = 0 ;		//  4	UINT32	经度
			req._info._lat	 = 0 ;		//	4	UINT32	纬度
			req._info._height= 0 ;	//	2	UINT16	高程
			req._info._speed = 0 ;	//	2	UINT16	速度
			req._info._direction = 0 ;//	2	UINT16	方向
			safe_memncpy( (char*)req._info._time , "201266" , sizeof(req._info._time));

			factory.Pack( &req, pack);
		}
		break ;
	case 5:
		{
			CAlarmScheduleReq req ;
			safe_memncpy( (char*)req._scheduleid, "1", sizeof(req._scheduleid) ) ;
			req._alarmtype = 0 ; 		//  UINT8	匹配状态(0成功，1失败)
			safe_memncpy( (char*)req._hangid, "1", sizeof(req._hangid));		//	String	挂车ID
			safe_memncpy( (char*)req._hangnum , "1", sizeof(req._hangnum));		//	String	挂车车牌号

			req._info._alam  = 0 ;		//	4	UINT32	报警标志位
			req._info._state = 0 ;	//	4	UINT32	状态位
			req._info._lon   = 0 ;		//  4	UINT32	经度
			req._info._lat	 = 0 ;		//	4	UINT32	纬度
			req._info._height= 0 ;	//	2	UINT16	高程
			req._info._speed = 0 ;	//	2	UINT16	速度
			req._info._direction = 0 ;//	2	UINT16	方向
			safe_memncpy( (char*)req._info._time , "201266" , sizeof(req._info._time)) ;
			factory.Pack( &req, pack ) ;
		}
		break ;
	case 6:
	    {
	    	CUpMsgDataScheduleReq req;
	    	req._code = 1;
	    	req._data = "1234567";
	    	factory.Pack( &req, pack);
	    }
	    break;
	case 7:
	    {
	    	CAutoDataScheduleReq req;
	    	req._space = 1 ;
	    	req._state = 1 ;
	    	factory.Pack( &req, pack ) ;
	    }
	    break;
	case 8:
	    {
	    	CErrorScheduleReq req;
	    	req._code = 1 ;
	    	req._desc = "bad message" ;
	    	factory.Pack(&req, pack);
	    }
	    break;
	case 9:
	    {
	    	CQueryInfoReq req;
	    	req._ctype  = CQueryInfoReq::Area ;
	    	req._area_id = 1 ;
	    	factory.Pack(&req, pack);
	    }
 	    break;
	case 10:
		{
			CResultScheduleReq req ;
			safe_memncpy( (char*)req._scheduleid, "1", sizeof(req._scheduleid) ) ;
			req._result = 0 ;
			factory.Pack( &req, pack ) ;
		}
		break ;
	case 11:
		{

		}
		break ;
   }

	way->Process( 1, pack.getBuffer(),pack.getLength(),0x1001, "4C54_15001088478");
	pack.resetBuf();
}
