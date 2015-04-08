/*=================================================================
** Copyright (c)2006,智安邦科技股份有限公司
** All rights reserved.
**
** 文件名称：main_app.cpp
**
** 当前版本：1.1
** 作    者：zhaojianbin
** 完成日期：2006-11-24
**
===================================================================*/

#include <signal.h>
#include <string.h>
#include <stdio.h>
#include "comlog.h"
#include "main_app.h"
#include <stdlib.h>
#include <strings.h>
#include <string>
#include <map>
#include <time.h>
#include <utility.h>

// 全局对象
MainApp* MainApp::_appInstance = NULL;

typedef std::map<std::string,std::string> MapArgs ;
// 使用参数列表处理
static MapArgs  g_kvmps;

void HandleExitSignal(int sig)
{
    if (MainApp::_appInstance != NULL)
    {
        MainApp::_appInstance->OnExit();
    }
    else
    {
        exit(0);
    }
}

void HandleHupSignal(int sig)
{       
    signal(sig, SIG_IGN);

    if (MainApp::_appInstance != NULL)
    {
        MainApp::_appInstance->OnHup();
    }       

    signal(sig, HandleHupSignal);
}

void HandleUsr1Signal(int sig)
{    
    signal(sig, SIG_IGN);
    
    if (MainApp::_appInstance != 0)
    {
        MainApp::_appInstance->OnUsr1Sig();
    }
    
    signal(sig, HandleUsr1Signal);
}
    
void HandleUsr2Signal(int sig)
{    
    signal(sig, SIG_IGN);
    
    if (MainApp::_appInstance != 0)
    {
        MainApp::_appInstance->OnUsr2Sig();
    }
    
    signal(sig, HandleUsr2Signal);
}

int SignalInit()
{       
    for (int  sig = SIGHUP; sig < 100; sig++) 
    {
        switch (sig) 
        {
            case SIGKILL:
            case SIGSTOP:
            case SIGALRM:
            case SIGSEGV:   
            case SIGINT:
                // Don't care these signals
                break;
#ifndef _UNIX
            case SIGCLD:
#endif
            case SIGPIPE:
                // Socket api require to ignore these signals
                signal(sig, SIG_IGN);
                break;
            
            case SIGHUP:
                signal(sig, HandleHupSignal);
            case SIGUSR1:
                signal(sig, HandleUsr1Signal);
                break;
            default:
                signal(sig, HandleExitSignal);
                break;
        }
    }
    
    return 1;
}

MainApp::MainApp( const char *ver , const char *help )
{
    _appInstance = this;
    memset( _appName, 0, sizeof(_appName) ) ;
    _myPid = getpid();

	sprintf( _version, "%s", ver );
    strcpy( _help, help ) ;
}

MainApp::~MainApp()
{
    _appInstance = NULL;
}

void MainApp::OnExit()
{
	// OUT_ERROR(NULL, 0, NULL, "A exit signal arrived ...") ;
    // printf("A exit signal arrived ...\n") ;
}

void MainApp::StayBack()
{
    int     child_pid;

    child_pid = fork();

    if (child_pid == -1) 
    {
        perror("fork fail");
        exit(1);
    } 
    else if (child_pid > 0) 
    {
        exit(0); // parent exit
    }

    if (setpgid(0, 0) == -1) 
    {
        perror("setpgrp");
        exit(1);
    }   

    if ((child_pid = fork()) == -1) 
    {
        perror("fork fail");
        exit(1);
    } 
    else if (child_pid > 0) 
    {
        exit(0); // parent exit
    }
    
    _myPid = getpid();
}

char* MainApp::GetAppName(char* str)
{
    char*   ptr         = NULL;
    char*   app_name    = NULL;
    char    delimiters[32];
    char    buff[256];
    
    strcpy(delimiters, "/");
    strcpy(buff, str);
    
    ptr = strtok(buff, delimiters);

    while (ptr != NULL)
    {
        app_name = ptr;
   
        ptr = strtok(NULL, delimiters);
    }

    if (app_name != NULL)
    {
        strcpy(_appName, app_name);
    }
    
    return _appName;
}
    
int MainApp::InitMainApp( int argc, char** argv )
{    
	SignalInit() ;
	// 处理帮助显示信息和后台运行
	if ( Usage( argc, argv ) == 0 ) {
		StayBack();
	}

	// 设置开启coredump
	const char *sz = getArgv( "r" ) ;
	if ( sz == NULL ) {
		sz = "./" ;
	}
	//	设置工作目录，core文件会产生到这里
	char szbuf[256] = {0} ;
	sprintf( szbuf, "mkdir -p %s", sz ) ;
	system( szbuf ) ;
	chdir( sz );

#ifndef _UNIX
	set_stklim();
	__MAKE_CORE;
#endif

    return 0 ;
}

int MainApp::Usage( int argc, char **argv )
{
	if ( argc < 2 ) {
		return -1 ;
	}

	char *ptr = strchr( argv[1], '-' ) ;
	if ( ptr == NULL ) {
		return -1 ;
	}

	int ret = -1 ;

	MapArgs::iterator it ;

	// was  -r./
	int i = 1 ;
	while( i < argc ) {
		char *ptr = argv[i] ;
		if ( ptr[0] != '-' ){
			++ i ;
			continue ;
		}
		// 解析配置文件路径
		char *cmd = ptr + 1 ;

		if ( strncasecmp( cmd, "help", 4 ) == 0 || strcasecmp(cmd , "h") == 0 ) { // 帮助指令
			printf( "%s\n", _help ) ;
			exit(0) ;
		} else if ( strncasecmp( cmd, "daemon" , 6 ) == 0 || strcasecmp( cmd, "d" ) == 0 ) {  // 运行后台处理
			printf( "stay back process\n" ) ;
			ret = 0 ;
		} else if ( strncasecmp( cmd, "ver", 3 ) == 0 || strcasecmp( cmd, "v" ) == 0 ) {
			printf( "version: %s\n", _version ) ;
			exit(0) ;
		} else {
			std::string key( cmd, 1 ) ;
			std::string val( cmd+1 ) ;

			it = g_kvmps.find( key ) ;
			if ( it != g_kvmps.end() ) {
				it->second = val ;
			} else {
				g_kvmps.insert( make_pair( key, val ) ) ;
			}
		}

		++ i ;
	}
	return ret ;
}

// 取得通过命令行传过来的参数
const char * MainApp::getArgv( const char *key )
{
	MapArgs::iterator it = g_kvmps.find( key ) ;
	if ( it == g_kvmps.end() ) {
		return NULL ;
	}
	return it->second.c_str() ;
}
