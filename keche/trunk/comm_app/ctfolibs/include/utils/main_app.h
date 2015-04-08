/*=================================================================
** Copyright (c)2006,智安邦科技股份有限公司
** All rights reserved.
**
** 文件名称：main_app.h
**
** 当前版本：1.1
** 作    者：zhaojianbin
** 完成日期：2006-11-24
**
===================================================================*/

#ifndef MAIN_APP_H
#define MAIN_APP_H

#include <getopt.h>

extern void HandleExitSignal(int sig);
extern void HandleHupSignal(int sig);
extern void HandleUsr1Signal(int sig);
extern void HandleUsr2Signal(int sig);

class MainApp
{
    friend void HandleExitSignal(int sig);
    friend void HandleHupSignal(int sig);
    friend void HandleUsr1Signal(int sig);        
    friend void HandleUsr2Signal(int sig);  
    
public:
    MainApp( const char *ver= "4.1.0_20121116_01" ,
    		const char *help = "running at stay back, using param: -daemon\n\t -v view current version\n" );
    virtual ~MainApp(); 
    // 初始化参数列表
    int InitMainApp( int argc, char** argv );
    // 取得解析的参数
    const char * getArgv( const char *key ) ;

    void OnExit();
    virtual void OnHup() {}
    virtual int  Usage( int argc, char **argv ) ;
    virtual void OnUsr1Sig() {}
    virtual void OnUsr2Sig() {}
	
protected:
    void StayBack();    
	char* GetAppName(char* str);
    
private:
    static MainApp* _appInstance;
    // 进程号
    int         _myPid;
    // 进程名称
    char 		_appName[256] ;
    // 添加版本号管理
    char 		_version[256] ;
    // 帮助信息显示
    char 		_help[10240] ;
};

#endif // MAIN_APP_H

