#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

#include <sys/wait.h>
#include <pthread.h>

#include <time.h>
#include <signal.h>
#include <fcntl.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>

#define PAIRSIZE 2
#define CORPSIZE 32
#define NAMESIZE 16
#define OEMSIZE 6
#define PHONESIZE 6

static uint8_t keyb = 0x5b;
static uint8_t keye = 0x5d;
static uint8_t flag = 0x00;
static uint32_t msgseq = 0;

#define LOGINREQ   0x0001
#define LOGINRESP  0x8001
#define NOOPREQ    0x0002
#define NOOPRESP   0x8002
#define SUBREQ     0x0003
#define SUBRESP    0x8003
#define REPORTREQ  0x0004

#pragma pack(1)
//通用消息头
typedef struct GenHead {
	uint16_t len;
	uint8_t flag;
	uint8_t type;
} GenHead;

//数据消息头
typedef struct SpeHead {
	uint8_t msg_ver;
	uint16_t msg_type;
	uint16_t msg_len;
	uint32_t msg_seq;
} SpeHead;

typedef struct LoginReq {
	char corp[CORPSIZE];
	char name[NAMESIZE];
} LoginReq;

typedef struct LoginResp {
	uint8_t result;
} LoginResp;

typedef struct Response {
	uint32_t resp_seq;
	uint8_t result;
} Response;
#pragma pack()

#define GenHeadSize    sizeof(struct GenHead)
#define SpeHeadSize    sizeof(struct SpeHead)
#define LoginReqSize   sizeof(struct LoginReq)
#define LoginRespSize  sizeof(struct LoginResp)
#define SubItemSize    sizeof(struct SubItem)
#define SubBodySize    sizeof(struct SubBody)
#define ReportSize     sizeof(struct Report)
#define ResponseSize   sizeof(struct Response)

static int sendNoop(int fd)
{
	int ret;

	unsigned int bufLen;
	unsigned int msgLen;
	unsigned char msgBuf[BUFSIZ];
	unsigned char *gHeadPtr;
	unsigned char *sHeadPtr;

	GenHead *gHead;
	SpeHead *sHead;
	
	gHeadPtr = msgBuf + 1;
	sHeadPtr = gHeadPtr + GenHeadSize;

	gHead = (GenHead*)(gHeadPtr);
	sHead = (SpeHead*)(sHeadPtr);

	gHead->len = htons(SpeHeadSize);
	gHead->flag = 0;
	gHead->type = 0;
	sHead->msg_ver = 1;
	sHead->msg_type = htons(NOOPREQ);
	sHead->msg_len = htons(htons(gHead->len) - SpeHeadSize);
	sHead->msg_seq = htonl(msgseq++);

	msgLen = GenHeadSize + SpeHeadSize + 1;
	msgBuf[0] = keyb;
	msgBuf[msgLen++] = keye;

	bufLen = 0;
	while((ret = write(fd, msgBuf + bufLen, msgLen - bufLen)) > 0) {
		bufLen += ret;
	}
	if(bufLen != msgLen) {
		return 1;
	}

	return 0;
}

static int sendLogin(int fd, const char *corp, const char *name)
{
	int ret;

	unsigned int bufLen;
	unsigned int msgLen;
	unsigned char msgBuf[BUFSIZ];
	unsigned char *gHeadPtr;
	unsigned char *sHeadPtr;
	unsigned char *cBodyPtr;

	GenHead *gHead;
	SpeHead *sHead;
	LoginReq *loginReq;
	LoginResp *loginResp;
	
	gHeadPtr = msgBuf + 1;
	sHeadPtr = gHeadPtr + GenHeadSize;
	cBodyPtr = sHeadPtr + SpeHeadSize;

	gHead = (GenHead*)(gHeadPtr);
	sHead = (SpeHead*)(sHeadPtr);

	loginReq = (LoginReq*)(cBodyPtr);

	gHead->len = htons(SpeHeadSize + LoginReqSize);
	gHead->flag = flag;
	gHead->type = 0;
	sHead->msg_ver = 1;
	sHead->msg_type = htons(LOGINREQ);
	sHead->msg_len = htons(htons(gHead->len) - SpeHeadSize);
	sHead->msg_seq = htonl(msgseq++);
	strncpy(loginReq->corp, corp, CORPSIZE);
	strncpy(loginReq->name, name, NAMESIZE);

	msgLen = GenHeadSize + SpeHeadSize + LoginReqSize + 1;
	msgBuf[0] = keyb;
	msgBuf[msgLen++] = keye;

	bufLen = 0;
	while((ret = write(fd, msgBuf + bufLen, msgLen - bufLen)) > 0) {
		bufLen += ret;
	}
	if(bufLen != msgLen) {
		return 1;
	}

	msgLen = GenHeadSize + SpeHeadSize + LoginRespSize + 2;
	bufLen = 0;
	while((ret = read(fd, msgBuf + bufLen, msgLen - bufLen)) > 0) {
		bufLen += ret;
	}
	if(bufLen != msgLen) {
		return 1;
	}

	loginResp = (LoginResp*)(cBodyPtr);
	if(msgBuf[0] != keyb || msgBuf[msgLen - 1] != keye) {
		return 1;
	}

	return 0;
}

void* noop_worker(void *param)
{
	int i;
	int fd;

	fd = *(int*)param;

	
	while(1) {
		if(sendNoop(fd) != 0) {
			break;
		}
		//usleep(1000);
	}
	
	/*
	for(i = 0; i < 10; ++i) {
		if(sendNoop(fd) != 0) {
			break;
		}

		if(i > 10000000) {
			printf("finish\n");
			sleep(3);
		}
	}
	*/

	return NULL;
}

static int child_worker(const char *corp, const char *name)
{
	int ret;
	unsigned char buffer[BUFSIZ + 1];

	int fd;
	socklen_t sinlen;
	struct sockaddr_in sinbuf;
	struct sockaddr *sinptr;

	pthread_t tid;
	time_t oldTime;
	time_t newTime;

	unsigned long sum;
	unsigned long total;

	signal(SIGPIPE, SIG_IGN);

	sinptr = (struct sockaddr*)&sinbuf;
	sinlen = sizeof(struct sockaddr_in);
	memset(sinptr, 0x00, sinlen);

	sinbuf.sin_family = AF_INET;
	sinbuf.sin_addr.s_addr = inet_addr("127.0.0.1");
	sinbuf.sin_port = htons(54321);

	fd = socket(AF_INET, SOCK_STREAM, 0);
	if(fd < 0) {
		return 1;
	}

	ret = connect(fd, sinptr, sinlen);
	if(ret != 0) {
		close(fd);
		return 1;
	}

	/*
	if(sendLogin(fd, corp, name) != 0) {
		close(fd);
		return 1;
	}
	*/

	//sleep(3600);
	
	ret = pthread_create(&tid, NULL, noop_worker, &fd);

	sum = 0;
	total = 0;

	oldTime = time(NULL);
	while((ret = read(fd, buffer, BUFSIZ)) > 0) {
		//printf("read %d bytes\n", ret);
		sum += ret;

		newTime = time(NULL);
		if(newTime - oldTime > 3) {
			oldTime = newTime;
			total += sum;
			sum = 0;
			printf("%s: %lu\n", name, total);
		}
	}

	pthread_join(tid, NULL);

	return 0;
}

int main(int argc, char *argv[])
{
	
	int i;
	int ret;

	int size;

	char buffer[BUFSIZ + 1];

	size = argc == 1 ? 1 : atoi(argv[1]);
	for(i = 0; i < size; ++i) {
		if((ret = fork()) > 0) {
			continue;
		}

		snprintf(buffer, BUFSIZ, "user%04d", i);

		return  child_worker(buffer, buffer);
	}

	for(i = 0; i < size; ++i) {
		wait(NULL);
	}
	
	//child_worker("user0003", "user0003");

	return 0;
}
