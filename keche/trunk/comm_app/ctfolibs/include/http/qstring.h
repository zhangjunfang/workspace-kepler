#ifndef __QSTRING_H__
#define __QSTRING_H__

#include <string.h>
/************************************************************************/
/* author: humingqing													*/
/* date: 2011-03-10                                                     */
/* memo: CQString类为通用字符串类，主要实现数据赋值和格式化输入数据,	*/
/*		字符串连接操作以及常见字符串查找,取子串,替换子串,去除首尾不可	*/
/*		见字符,移除指定字符以字符串大小写转换,							*/
/*		这避免麻烦的内存分配和释放过程									*/
/************************************************************************/
class CQString
{
public:
	// 构造函数
	CQString( const char *sz ) ;
	// 默认函数
	CQString( const CQString &ss ) ;
	// 构造函数
	CQString( void ) ;
	// 析构函数
	virtual ~CQString( void ) ;
	// 重载等号运算
	const CQString &operator = ( const char *sz ) ;
	// 重载符号运算
	const CQString &operator = ( const CQString &ss ) ;
	// 实现字符串连接
	const CQString &operator + ( const char *sz ) ;
	// 实现字符串连接
	const CQString &operator + ( const CQString &ss ) ;
	// 添加二元运算
	const CQString &operator +=( const char *sz ) ;
	// 添加二元运算
	const CQString &operator +=( const CQString &ss ) ;
	// 重载运算
	operator const char* ( void ) ;
	// 重载运算
	operator char* ( void ) ;
	// 设置数据
	void SetString( const char *data, const int len = 0 ) ;
	// 取得数据长度
	const int  GetLength( void ) const ;
	// 取得数据
	const char* GetBuffer( void ) const ;
	// 转换成小写
	const char* ToLower( void ) ;
	// 转换成大写
	const char* ToUpper( void ) ;
	// 移除数据
	const char* Remove( const char *sz ) ;
	// 去掉首尾的空白"\r\n\t"
	const char* Trim( void ) ;
	// 替换字符串
	const char* Replace( const char *src, const char *dest ) ;
	// 多个字符替换成多字符
	const char* NReplace( int n, const char *c[], const char *s[] ) ;
	// 从哪个位置开始查找
	const int   Find( const char *sz, const int pos = 0 ) ;
	// 取子串,当len为零或者负同取到结尾,或者大于本身串长
	const char* SubString( const int pos, const int len ) ;
	// 添加数据
	void   AppendBuffer( const char *data, const int len = 0 ) ;
	// 是否为空串
	bool  IsEmpty( void ) ;
	// 从内存左边去掉多少个字符
	void  MemTrimLeft( const int count ) ;
	// 从内存右边去掉多少个字符
	void  MemTrimRight( const int count ) ;
	// 只留下多长数据
	void  MemTrimLength( const int count ) ;
	// 转成16进制显示
	const char *ToHex( void ) ;
	// 清空数据
	void Clear( void ) ;

private:
	// 开辟内存
	void Expand( const int len ) ;
	// 复制到内存中
	void Memcopy( const char *ptr, int len ) ;

private:
	// 数据长度
	int		_nLength;
	// 数据BUFFER
	char*	_szBuffer;
	// 临时数据BUFFER
	char*   _szTemp;
	// 开辟内存长度
	int     _nMemLen;
};

inline bool operator == (const CQString & a, const CQString & b)
{
	return    ( a.GetLength() == b.GetLength() )				// optimization on some platforms
	       && ( strcmp(a.GetBuffer(), b.GetBuffer()) == 0 );	// actual compare
}
inline bool operator < (const CQString & a, const CQString & b)
{
	return strcmp(a.GetBuffer(), b.GetBuffer()) < 0;
}

inline bool operator != (const CQString & a, const CQString & b) { return !(a == b); }
inline bool operator >  (const CQString & a, const CQString & b) { return b < a; }
inline bool operator <= (const CQString & a, const CQString & b) { return !(b < a); }
inline bool operator >= (const CQString & a, const CQString & b) { return !(a < b); }

inline bool operator == (const CQString & a, const char* b) { return strcmp(a.GetBuffer(), b) == 0; }
inline bool operator == (const char* a, const CQString & b) { return b == a; }
inline bool operator != (const CQString & a, const char* b) { return !(a == b); }
inline bool operator != (const char* a, const CQString & b) { return !(b == a); }

#endif
