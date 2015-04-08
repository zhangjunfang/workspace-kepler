#include "main_app.h"
#include "utility.h"
#include "systemenv.h"
#include <sys/time.h>
#include <sys/resource.h>
#include <tools.h>

#define LOG_PATH 			"/var/lbs/log/ws"
#define CONF_PATH   		"/usr/local/lbs/conf/ws/"
#define RUN_PATH			"/usr/local/lbs"
#define IN_LOG  			"proxy.log"
#define CONFIG  			"proxy.conf"
#define GTRANS_ENV			"MTRANS_PRJ_HOME"
#define ABS_LOG_PATH		"/lbs/log/ws"
#define ABS_CONF_PATH		"/lbs/conf/ws"
#define MAS_VERSION   	    "V4.2.1 " __DATE__ " " __TIME__

int main(int argc, char**argv)
{
	MainApp app( MAS_VERSION ) ;
//	打包时记得因为测试而关闭的等待连接响应包部分
	app.InitMainApp(argc, argv);

	const char *szrun = app.getArgv( "r" ) ;
	char szlog[1024] = {0};
	if ( szrun != NULL )
		sprintf(szlog, "%s/%s", szrun, "log/ts");
	else
		getrunpath(GTRANS_ENV, szlog, ABS_LOG_PATH, LOG_PATH);

	char szconf[1024] = {0};
	if ( szrun != NULL )
		sprintf(szconf, "%s/%s/%s", szrun, "conf/ts", CONFIG);
	else
		getconfpath(GTRANS_ENV, szconf, ABS_CONF_PATH, CONF_PATH, CONFIG);

	printf("run conf file path:%s , log path: %s\n", szconf, szlog);

	char szuser[1024] = {0};
	if ( szrun != NULL )
		sprintf( szuser, "%s/%s", szrun, "/lbs" ) ;
	else
		getrunpath( GTRANS_ENV, szuser, "/lbs", RUN_PATH ) ;


	CSystemEnv env ;
	if ( ! env.Init( szconf, szlog , szuser ) )
	{
		printf( "CSystemEnv init failed\n" ) ;
		return 0 ;
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


