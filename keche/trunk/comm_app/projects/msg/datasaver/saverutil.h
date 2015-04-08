/*
 * saverutil.h
 *
 *  Created on: 2012-7-23
 *      Author: think
 */

#ifndef __SAVERUTIL_H_
#define __SAVERUTIL_H_

#include <list>
#include <string>
#include <map>
#include <string.h>

using namespace std;

typedef struct _StrInfo
{
    const char *pos;
    int offset;
}StrInfo;

extern long kernel_mktime(struct tm * tm);

/***时间的样子 20120526/084334 *******/
inline string get_time(time_t t)
{
	char buff[128] = {0};
	struct tm *tm = localtime(&t);
	snprintf(buff, sizeof(buff), "%04d%02d%02d/%02d%02d%02d", tm->tm_year + 1900,
			tm->tm_mon + 1, tm->tm_mday, tm->tm_hour, tm->tm_min, tm->tm_sec);
	return buff;
}

//将str split成list的形式， StrInfo只是指明每一个分段的位置和偏移量，不会修改原始数据的值。
extern  bool split2list(const char *data, int data_len, std::list<StrInfo> &list,  const char *split);

/********************************
 * Input: StrInfo 分割出的字段为  "key:value" 的字符串形式。
 * Output: 构造出map, map中的string string 为新构造的，
 *****************************/
extern int split2map(std::list<StrInfo> &list, map<string , string> &kv_map, const char *split);

/****************************************
 * 1. 原子计数操作的类。
 * 2. 操作符重载的传入参数和返回值是Type型，不是SynValue, 所以不能实现链式表达式。
 ***************************************/
template <class Type>
class SynValue
{
public:
	SynValue(int i = 0):num(i) {}

    Type value()
    {
    	return __sync_fetch_and_add(&num, 0);
    }

    Type reset()
    {
    	return __sync_fetch_and_and(&num, 0);
    }
    //前缀++ 和 --, 后缀不是这种方式，使用时注意
	Type operator ++()
	{
		return __sync_add_and_fetch(&num, 1);
	}

	Type operator --()
	{
		return __sync_sub_and_fetch(&num , 1);
	}

	Type operator +=( Type i)
	{
		return __sync_add_and_fetch(&num, i);
	}

	Type operator -=(Type i)
	{
		return __sync_sub_and_fetch(&num, i);
	}

	bool operator == (Type i)
	{
		return value() == i;
	}

	bool operator > (Type i)
	{
		return value() > i;
	}

	bool operator < (Type i)
	{
		return value() < i;
	}

	bool operator >= (Type i)
	{
		return value() >= i;
	}

	bool operator <= (Type i)
	{
		return value() <= i;
	}
private:
	Type num;
};

inline int itostr(const short& n, char* buf)
{
	return sprintf(buf, "%hd", n);
}
inline int itostr(const unsigned short& n, char* buf)
{
	return sprintf(buf, "%hu", n);
}
inline int itostr(const int& n, char* buf)
{
	return sprintf(buf, "%d", n);
}
inline int itostr(const unsigned int & n, char* buf)
{
	return sprintf(buf, "%u", n);
}
inline int itostr(const long& n, char* buf)
{
	return sprintf(buf, "%ld", n);
}
inline int itostr(const unsigned long& n, char* buf)
{
	return sprintf(buf, "%lu", n);
}
inline int itostr(const long long & n, char* buf)
{
	return sprintf(buf, "%lld", n);
}
inline int itostr(const unsigned long long& n, char* buf)
{
	return sprintf(buf, "%llu", n);
}

inline string itostr(const short& n)
{
	char buf[32] = {0};
	sprintf(buf, "%hd", n);
	return buf;
}
inline string itostr(const unsigned short& n)
{
	char buf[32] = {0};
	sprintf(buf, "%hu", n);
	return buf;
}
inline string itostr(const int& n)
{
	char buf[32] = {0};
	sprintf(buf, "%d", n);
	return buf;
}
inline string itostr(const unsigned int & n)
{
	char buf[32] = {0};
	sprintf(buf, "%u", n);
	return buf;
}
inline string itostr(const long& n)
{
	char buf[32] = {0};
	sprintf(buf, "%ld", n);
	return buf;
}
inline string itostr(const unsigned long& n)
{
	char buf[32] = {0};
	sprintf(buf, "%lu", n);
	return buf;
}
inline string itostr(const long long & n)
{
	char buf[32] = {0};
	sprintf(buf, "%lld", n);
	return buf;
}
inline string itostr(const unsigned long long& n)
{
	char buf[32] = {0};
	sprintf(buf, "%llu", n);
	return buf;
}

inline int strtoi(const char* buf, short& n)
{
	return sscanf(buf, "%hd", &n);
}
inline int strtoi(const char* buf, unsigned short& n)
{
	return sscanf(buf, "%hu", &n);
}
inline int strtoi(const char* buf, int& n)
{
	return sscanf(buf, "%d", &n);
}
inline int strtoi(const char* buf, unsigned int & n)
{
	return sscanf(buf, "%u", &n);
}
inline int strtoi(const char* buf, long& n)
{
	return sscanf(buf, "%ld", &n);
}
inline int strtoi(const char* buf, unsigned long& n)
{
	return sscanf(buf, "%lu", &n);
}
inline int strtoi(const char* buf, long long& n)
{
	return sscanf(buf, "%lld", &n);
}
inline int strtoi(const char* buf, unsigned long long & n)
{
	return sscanf(buf, "%llu", &n);
}

#endif /* SAVERUTIL_H_ */
