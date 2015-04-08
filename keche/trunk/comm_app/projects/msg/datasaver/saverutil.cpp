/******************************************************
*  CopyRight: 北京中交兴路科技有限公司(2012-2015)
*   FileName: saverutil.cpp
*     Author: liubo  2012-7-23
*Description:
*******************************************************/
#include "saverutil.h"

#define OUTLEN 255


#define MINUTE 60
#define HOUR (60*MINUTE)
#define DAY (24*HOUR)
#define YEAR (365*DAY)

/* interestingly, we assume leap-years */
static int month[12] = {
    0,
    DAY*(31),
    DAY*(31+29),
    DAY*(31+29+31),
    DAY*(31+29+31+30),
    DAY*(31+29+31+30+31),
    DAY*(31+29+31+30+31+30),
    DAY*(31+29+31+30+31+30+31),
    DAY*(31+29+31+30+31+30+31+31),
    DAY*(31+29+31+30+31+30+31+31+30),
    DAY*(31+29+31+30+31+30+31+31+30+31),
    DAY*(31+29+31+30+31+30+31+31+30+31+30)
};

long kernel_mktime(struct tm * tm)
{
    long res;
    int year;

    year = tm->tm_year - 70;

/* magic offsets (y+1) needed to get leapyears right.*/

    res = YEAR*year + DAY*((year+1)/4);
    res += month[tm->tm_mon];

/* and (y+2) here. If it wasn't a leap-year, we have to adjust */

    if (tm->tm_mon>1 && ((year+2)%4))
        res -= DAY;
    res += DAY*(tm->tm_mday-1);
    res += HOUR*tm->tm_hour;
    res += MINUTE*tm->tm_min;
    res += tm->tm_sec;

    //减去八个小时的时间
    return res - (8 * 3600);
}

//将str split成list的形式， StrInfo只是指明每一个分段的位置和偏移量，不会修改原始数据的值。
bool split2list(const char *data, int data_len, std::list<StrInfo> &list,  const char *split)
{
	int dlen = data_len;
    int slen = strlen(split);
    int offset = 0;

    const  char *pos = strstr(data + offset, split);
    if(pos == NULL)
    	return false;

    StrInfo str_info;
    //至少有一个split.
    while(offset < dlen && (NULL != (pos = strstr(data + offset, split))))
    {
    	str_info.pos = data + offset;
    	str_info.offset = pos - data - offset;
        offset += (pos - data - offset + slen);
        list.push_back(str_info);
    }

    str_info.pos = data + offset;
    str_info.offset =  dlen - offset;
    list.push_back(str_info);

    return true;
}

/********************************
 * Input: StrInfo 分割出的字段为  "key:value" 的字符串形式。
 * Output: 构造出map, map中的string string 为新构造的，
 *****************************/
int split2map(std::list<StrInfo> &list, map<string , string> &kv_map, const char *split)
{
	kv_map.clear();
    int ret = 0;
    int slen = strlen(split);
    //	int count = 0;
	map<string, string>::iterator it;
	std::list<StrInfo>::iterator iter = list.begin();

	const char *pos = NULL;
	for (; iter != list.end(); ++iter)
	{
		if ((pos = strstr(iter->pos, split)) == NULL)
			continue;
		// 如果不按照key:value的规则话就会引起coredump
		int n = iter->offset - (pos - iter->pos) - slen ;
		if ( n <= 0 ) continue ;

		string key(iter->pos, pos - iter->pos) ;
		string value(pos + slen, n ) ;

		it = kv_map.find(key);
		if (it != kv_map.end())
		{ // 如果存在多个同名则并列处理
			it->second += "|";
			it->second += value;
		}
		else
		{
			kv_map.insert(make_pair(key, value));
			ret ++;
		}
	}
	return ret;
}
