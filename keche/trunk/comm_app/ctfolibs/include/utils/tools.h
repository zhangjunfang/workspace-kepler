/**
  * Name: 
  * Copyright: 
  * Author: lizp.net@gmail.com
  * Date: 2009-11-3 下午 3:42:52
  * Description: 
  * Modification: 
  **/
#ifndef __TOOLS_H__
#define __TOOLS_H__

#include <string>
#include <vector>
#include <stdio.h>

using namespace std;

//UNICODE码转为GB2312码
int u2g(char *inbuf, int inlen, char *outbuf, int& outlen);

//GB2312码转为UNICODE码
int g2u(char *inbuf, size_t inlen, char *outbuf, int& outlen);

// 检测IP的有效性
bool check_addr( const char *ip ) ;
// 安全内存拷贝
char * safe_memncpy( char *dest, const char *src, int len ) ;

// 自动拆分前多个分割符处理
bool splitvector( const string &str, std::vector<std::string> &vec, const std::string &split , const int count ) ;

// 这里主要处理分析路径中带有 env:LBS_ROOT/lbs 之类的路径
bool getenvpath( const char *value, char *szbuf ) ;

/**
 *  取得当前环境对象路径,
 *	env 为环境对象名称，buf 存放路径的缓存, sz 为附加后缀, def 默认的中径
 */
const char * getrunpath( const char *env, char *buf, const char *sz, const char *def ) ;

// 取得默认的CONF路径
const char * getconfpath( const char *env, char *buf, const char *sz, const char *def, const char *conf ) ;

// 追加写入文件操作
bool AppendFile( const char *szName, const char *szBuffer, const int nLen ) ;

// 创建新文件写入
bool WriteFile( const char *szName, const char *szBuffer, const int nLen ) ;

// 读取文件
char *ReadFile( const char *szFile , int &nLen ) ;

// 释放数据
void  FreeBuffer( char *buf ) ;

#endif
