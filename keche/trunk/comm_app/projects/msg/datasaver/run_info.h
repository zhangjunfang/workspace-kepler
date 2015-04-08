/******************************************************
*  CopyRight: 北京中交兴路科技有限公司(2012-2015)
*   FileName: run_info.h
*     Author: liubo  2012-7-16 
*Description: 记录data Server的统计信息。并记录这些统计信息供msg_server使用
*******************************************************/

#ifndef RUN_INFO_H_
#define RUN_INFO_H_

#include "comlog.h"
#include "DataPool.h"
#include <sys/time.h>
#include "saverutil.h"

/**********************************************************
*要不单独为其开辟一个线程？？？？？？？？？？？？？已有的线程太多了,加入线程后变成主动对象。应该在底层实现一个定时器，在这里回调!!!!！
打印的样式：
================= DATASERVER RUN INFO ===========================
RUNTIME  : 0 day 12 hour  3 min 20 sec (100000 sec)
RECVINFO : total 1234144134 averge 1234  cur 2134
DBINFO   : insert (13241 2134 1234) update(234124, 1234, 1324)
DATAPOOL : max (100 * 10000) use 131451345 45%
=================================================================
***********************************************************/

#define DEFAULT_RUN_PATH  "./msg.runinfo"

class RunInfo
{
#define BUF_SIZE 1024

public:
	RunInfo():run_time(time(0))
	{
		gettimeofday(&btimeval, NULL);
		memset(buffer, BUF_SIZE, 0);
	}

	void init()
	{
	}

	/**检查是否需要输出，输出条件怎么定, 由具体的策略来决定 */
	bool check_show()
	{
	    return cur_insert_total.value() >= 10000;
	}

	void show(const void *save2file = (const void*)(DEFAULT_RUN_PATH))
	{
		time_t t = time(0) - run_time;
		if (t == 0)
			t++;
		struct timeval cur_timeval;
		gettimeofday(&cur_timeval, NULL);

		time_t ltm = time(0);
		struct tm *tm = localtime(&ltm);
		char localtime[128] = {0};
		snprintf(localtime, sizeof(localtime), "%04d%02d%02d-%02d:%02d:%02d", tm->tm_year + 1900,
				tm->tm_mon + 1, tm->tm_mday, tm->tm_hour, tm->tm_min, tm->tm_sec);

        //单位是ms
        long long timeval = (cur_timeval.tv_sec - btimeval.tv_sec) * 1000 + ((cur_timeval.tv_usec - btimeval.tv_usec) / 1000) + 1;

        int day  = t / (3600*24);
        int hour = t % (3600 * 24) / 3600;
        int min  = t % (3600 * 24) % 3600 / 60;
        int sec  = t % (3600 * 24) % 3600 % 60;

        if ( t == 0 )  t = 1 ;

        snprintf(buffer, BUF_SIZE - 1,
        		"================= DATASERVER RUN INFO %s  =========================== \n"
        		"RUNTIME  : %d day %d hour  %d min %d sec (%d sec) \n"
        		"RECVINFO : total %lld averge %d  cur-average %d \n"
        		"DBINFO   : insert (total:%lld errtotal: %lld average:%lld cur-average:%lld) \n"
        		"           update(total:%lld average:%lld cur-average:%lld) \n",
        		localtime, day, hour, min, sec, t,
        		(long long)recv_total.value(), (int)(recv_total.value() / (int)t), (int)(cur_recv_total.value() * 1000 / timeval),
        		(long long)insert_total.value(), (long long)insert_fail_total.value(), (long long)(insert_total.value() / t), (long long)(cur_insert_total.value() * 1000 / timeval),
        		(long long)update_total.value(), (long long)(update_total.value() / (int)t), (long long)(cur_update_total.value() * 1000 / timeval) );

        cur_recv_total.reset();
        cur_insert_total.reset();
        cur_update_total.reset();

        gettimeofday(&btimeval, NULL);

        FILE *fp = NULL;
        if(stdout != (FILE*)save2file)
        {
        	fp = fopen((const char *)save2file, "a+");
        	if(fp == NULL)
        	{
        		printf("fopen = NULL strerror : %s\n", strerror(errno));
        		fp = stdout;
        	}
        }
        fprintf(fp, "%s \n", buffer);
        if(fp != stdout)
        {
        	fclose(fp);
        	fp = NULL;
        }
	}

	//设置成public，具体的处理策略，是发消息还是写日志让调用者自己取去吧。
public:
	time_t run_time;
	SynValue<long long> recv_total;
	SynValue<long long> insert_total;
    SynValue<long long> insert_fail_total;
	SynValue<long long> update_total;

    //这个打印周期的数据
    struct timeval btimeval;
    SynValue<long long> cur_recv_total;
    SynValue<long long> cur_insert_total;
    SynValue<long long> cur_update_total;

    //保存打印结果。
    char buffer[BUF_SIZE];
};

#endif /* RUN_INFO_H_ */
