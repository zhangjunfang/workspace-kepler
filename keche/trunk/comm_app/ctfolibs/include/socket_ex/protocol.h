#ifndef __POROTOCOL__
#define __POROTOCOL__
#include <assert.h>

#include "list.h"
#include "databuffer.h"

#pragma pack(1)

#define MIN_PACKET_LEN 			4
#define MAX_PACK_LEN			10*1024*1024  // 最大数据长度为10M
// 将数据BUF定义为10K
#ifndef KFIFO_BUFF_SIZE
#define KFIFO_BUFF_SIZE  		10*1024
#endif

enum PROTO_TYPE {
	E_PROTO_IDLE = 0, 
	E_PROTO_OUT = 1,
	E_PROTO_IN = 2,
	E_PROTO_ERR
};


/* 
  * if find a whole packet, return packet ptr; 
  * if not foud, return NULL
  */
struct packet {
	struct list_head list;
	unsigned char *data;
	int len;
	char type ;
};


//message protocol head
struct msg_head {
	unsigned short len;			
	unsigned int sequence;
	unsigned short id;
	unsigned int access_code;			
};

#pragma pack()

void free_packet(struct packet *p);
struct packet * get_packet_from_kfifo( DataBuffer *fifo ) ;
struct packet * get_packet_from_kfifo_v1( DataBuffer *fifo ) ;
struct packet * get_packet_from_kfifo_br( DataBuffer *fifo ) ;

// 单独的一个数据包
class CPacket
{
public:
	CPacket( void *sock, void *pack ){
		_sock   = sock ;
		_next = NULL ;
		_pack = pack ;
	}
	~CPacket() ;
public:
	void 	* _sock ;
	void    * _pack ;
	CPacket * _next ;
};

#endif
