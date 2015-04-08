#include <BaseServer.h>
#include <protocol.h>

#include <iostream>
using namespace std;

#include <signal.h>
#include <pthread.h>

#pragma pack(1)

struct interheader {
	unsigned char  tag ;   // 拆分头部标识 0x5b
	unsigned short len ;   // 数据长度，不包括数据头和尾
	unsigned char  flag ;  // 是否加密标识
	unsigned char  type ;  // 协议类型
};
struct interfooter {
	unsigned char footer ;  // 拆分尾标识 0x5d
};

static volatile unsigned long g_recv = 0;
static volatile unsigned long g_free = 0;
static volatile unsigned long g_used = 0;

static pthread_mutex_t g_mutex = PTHREAD_MUTEX_INITIALIZER;

#pragma pack()

class CInterSpliter : public IPackSpliter
{
public:
	struct packet * get_kfifo_packet( DataBuffer *fifo ) {
		unsigned int len = fifo->getLength() ;
		if ( len <= 0 || len > MAX_PACK_LEN ){
			fifo->resetBuf() ;
			return NULL;
		}

		++g_recv;

		char *p = NULL, *in_begin = NULL ;
		struct list_head *packet_list_ptr = NULL;

		unsigned int i = 0;
		in_begin = p = (char *) fifo->getBuffer() ;
		while ( i < len && len > 2 ) {
			if (*p == 0x5b ) {  // 0x5d

				interheader *header = (interheader *) p ;
				unsigned short nlen = ntohs( header->len ) ;
				int pack_len = nlen + sizeof(interheader) + 1 ;

				if ( (i+pack_len) > len || *(p+pack_len-1) != 0x5d ) {
					++ p ; ++ i ;
					continue ;
				}

				struct packet *item = (struct packet *) malloc(sizeof(struct packet));
				if (item == NULL)
					return (struct packet *) packet_list_ptr;
				item->data = (unsigned char *) malloc(pack_len+1);
				memset(item->data, 0, pack_len+1);
				memcpy(item->data, p, pack_len);
				item->len = pack_len;
				item->type = E_PROTO_OUT;

				if (packet_list_ptr == NULL) {
					packet_list_ptr = (struct list_head *) malloc(sizeof(struct list_head));
					if (packet_list_ptr == NULL)
						return NULL;

					INIT_LIST_HEAD(packet_list_ptr);
				}
				list_add_tail(&item->list, packet_list_ptr);
				in_begin = p + pack_len ;

				p = p + pack_len - 1 ;  // 直接转到0x5d结束标识上面
				i = i + pack_len - 1 ;
			}

			++ p ;
			++ i ;
		}


		unsigned int del_len = (unsigned int)( in_begin - (char *) fifo->getBuffer() ) ;
		if (del_len > 0 && (unsigned int) del_len <= len){
			fifo->removePos( del_len );
		}


		return (struct packet*) packet_list_ptr;
	}

	void free_kfifo_packet( struct packet *packet ) {
		pthread_mutex_lock(&g_mutex);
		++g_free;
		pthread_mutex_unlock(&g_mutex);

		free_packet( packet ) ;
	}
};

class CServer : public BaseServer {
	//CMyPackSpliter _spliter;
	CInterSpliter _spliter ;
	time_t _flag;
public:
	CServer() {
		_flag = 0;
	}

	~CServer(){
	}

	bool Start(void) {
		_tcp_handle.setpackspliter(&_spliter);

		return StartServer(54321, "0.0.0.0", 8, 10);
	}

	void on_data_arrived(int fd, const void* data, int len) {
		//printf("on_data_arrived: %d, %.*s", len, len, (char*)data);

		SendData(fd, "\0", 1);
		pthread_mutex_lock(&g_mutex);
		++g_used;

		/*
		   if(time(NULL) - _flag > 3) {
		   printf("start sleep on_data_arrived: %d\n", len);
		   sleep(1);
		   printf("finish sleep\n");
		   _flag = time(NULL);
		   }
		 */
		pthread_mutex_unlock(&g_mutex);
	}

	void on_dis_connection(int fd) {
	}

	void TimeWork() {
	}

	void NoopWork () {
	}
};

CServer srv;

int main(int argc, char **argv[])
{
	signal(SIGPIPE, SIG_IGN);

	srv.Start();

	while(1) {
		printf("%lu, %lu, %lu\n", g_recv, g_free, g_used);
		sleep(1);
	}

	return 0;
}
