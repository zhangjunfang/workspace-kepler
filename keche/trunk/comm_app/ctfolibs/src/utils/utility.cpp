#include "utility.h"

#include <stdlib.h>
#include <stdio.h>
#include <errno.h>

void set_stklim(void)
{
	struct rlimit lim;
	
	if(getrlimit(RLIMIT_STACK, &lim) < 0)
	{
		perror("getrlimit");
		exit(1);
	}
	printf("stack size:%lu\n",lim.rlim_cur);
	//if((lim.rlim_cur == RLIM_INFINITY) || (lim.rlim_cur > STACKSIZE))
	if(lim.rlim_cur < STACKSIZE)
	{
		lim.rlim_cur = STACKSIZE;
		if(setrlimit(RLIMIT_STACK, &lim) < 0)
		{
			perror("setrlimit");
			exit(1);
		}
	}
	if(getrlimit(RLIMIT_STACK, &lim) < 0)
	{
		perror("getrlimit");
		exit(1);
	}
	printf("stack size:%lu\n",lim.rlim_cur);
}


