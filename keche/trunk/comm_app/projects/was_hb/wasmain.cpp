#include <comlog.h>
#include <main_app.h>
#include <utility.h>
#include <sys/time.h>
#include <sys/resource.h>
#include "systemenv.h"
#include <tools.h>

#define LOG_PATH 			"/var/lbs/log/ws"
#define CONF_PATH   		"/usr/local/lbs/conf/ws"
#define CONFIG  			"was_hb.conf"
#define USER_INFO_FILE 		"wasv4_gb_car_info.conf"
#define MTRANS_ENV			"MTRANS_PRJ_HOME"
#define ABS_LOG_PATH		"/lbs/log/ws"
#define ABS_CONF_PATH		"/lbs/conf/ws"
#define LOG_NAME 			"was_hb.log"
#define RUN_LOG_PATH		"log/ws"
#define RUN_CONF_PATH		"conf/ws"
#define WASV4_VERSION  		"V4.1.1_20121226_01"

#include <sys/socket.h>

int main(int argc, char**argv)
{
	MainApp app(WASV4_VERSION);
//	打包时记得因为测试而关闭的等待连接响应包部分
	app.InitMainApp(argc, argv);

	const char *szrun = app.getArgv( "r" ) ;
	char szlog[1024] = {0} ;
	if ( szrun != NULL )
		sprintf( szlog, "%s/%s", szrun, RUN_LOG_PATH ) ;
	else
		getrunpath(MTRANS_ENV, szlog, ABS_LOG_PATH,LOG_PATH) ;

	char szconf[1024] = {0} ;
	if ( szrun != NULL )
		sprintf( szconf, "%s/%s/%s", szrun, RUN_CONF_PATH , CONFIG ) ;
	else
		getconfpath( MTRANS_ENV, szconf, ABS_CONF_PATH , CONF_PATH , CONFIG ) ;

	char szuser[1024] = {0};
	if ( szrun != NULL )
		sprintf( szuser, "%s/%s/%s", szrun, RUN_CONF_PATH ,  USER_INFO_FILE ) ;
	else
		getconfpath( MTRANS_ENV, szuser, ABS_CONF_PATH , CONF_PATH , USER_INFO_FILE);

	printf("run conf file path:%s , log path: %s\n" , szconf, szlog);

	CSystemEnv env;
	if (!env.Init(szconf,szlog,szuser,LOG_NAME))
	{
		printf("CSystemEnv init failed\n");
		return 0;
	}

	if ( ! env.Start() )
	{
		printf( "CSystemEnv start failed\n" ) ;
		return 0 ;
	}

	while (1)
	{
		usleep(1000*1000*1000);
	}

//	pipe_server.Stop();
	sleep(1);
	
	INFO_PRT("press enter to leave!");
	getchar();
	
	return 0;
}
