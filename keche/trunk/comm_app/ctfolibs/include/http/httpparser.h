/***
 * author: humingqing
 * date: 2011-09-07
 */
#ifndef __HTTPPARSER_H__
#define __HTTPPARSER_H__

#include <list>
#include <string>
using namespace std ;

// ERROR DEFINES
#define HTTPPARSER_ERR_SUCCESS				0
#define HTTPPARSER_ERR_FAILED				-1
#define HTTPPARSER_ERR_NODATA				-2
#define HTTPPARSER_ERR_NOMEM				-3
#define HTTPPARSER_ERR_NOHEADER				-4
#define HTTPPARSER_ERR_NOCONTENTLEN			-5
#define HTTPPARSER_ERR_DATAERROR			-6

// 定义自己的扩展头
#define EXT_HEADER_CODE			"x-resp-code"
#define EXT_HEADER_STATUSTEXT 	"x-resp_text"
#define EXT_HEADER_VERSION 		"x-version"
#define EXT_HEADER_METHOD       "x-req-method"  // 作为服务器端时解析需要
#define EXT_HEADER_URI			"x-req-uri"		// 作为服务器端解析时需要

class CParamList
{
public:
	CParamList() ;
	~CParamList() ;

private:

	// 头列表
	typedef struct _header_node
	{

		char* header ;
		char* value ;
		struct _header_node * next ;

	}HEADERNODE , *LPHEADERNODE;

	HEADERNODE 	_HeaderList ;

public:
	// 清楚头列表和数据
	void CleanHeaderList( void ) ;

	// 获取某参数的值
	const char* GetValue( const char* header ) const ;

	// 增加变量对
	void AddNode( const char* name , const char* value ) ;
};

class CHttpParser
{
public:

	CHttpParser() ;

	virtual ~CHttpParser() ;

protected:

	char* 		_pBody ;
	int 		_iBodySize ;

	CParamList  _ParamList ;

protected:

	// 清楚BODY数据
	void CleanBody( void ) ;

	static void my_strlwr( char* str ) ;

	/**
	 *  设置CHUNK 模式的BODY
	 */
	int SetChunkBody( const char* data , const int size ) ;

public:

	// 检测数据是否完整
	// 返回true表示数据接收完了,否则表示数据还没有接收完毕.
	// data_error返回true,表示在分析数据时,发现数据不正确
	static int DetectCompleteReq( const char* data , const int size ) ;

	// 判断CHUNK是否结束
	static int DetectCompleteChunk( const char* body , int body_size ) ;

	// 将16进制字符串转换为整数
	static int hex2int( const char* str ) ;

	// 分析HTTP REQUEST
	virtual int  Parse( const char* data , const int size ) ;

	int SetBody( const char* data , const int size ) ;

	const char* GetBody( int& size ) const
	{
		size = 	_iBodySize ;
		return 	_pBody ;
	};

	// 获取某个头的信息
	const char* GetHeader( const char* header ) const ;

};

#endif
