#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <assert.h>

#include "protocol.h"
#include "list.h"

void free_packet(struct packet *p)
{
	if (p != NULL && p->data != NULL)
	{
		free(p->data);
		p->data = NULL;
	}
	if (p != NULL)
	{
		list_del((struct list_head *) p);
		free(p);
		p = NULL;
	}
}

// 使用DataBuffer进行分包
struct packet * get_packet_from_kfifo_br( DataBuffer *fifo ) {

	unsigned int len = fifo->getLength() ;
	if ( len <= 0 || len > MAX_PACK_LEN ){
		fifo->resetBuf() ;  // 超出大小就重置BUF
		return NULL;
	}

	char *p, *begin = NULL , *in_begin = NULL ;
	struct list_head *packet_list_ptr = NULL ;

	in_begin = p = (char *)fifo->getBuffer() ;

	bool bfind       = false ;
	unsigned int pos  = 0 ;
	unsigned int ndel = 0 ;

	while (p != NULL && pos < len )
	{
		if ( *p == '\r' && *(p + 1) == '\n') {
			// 如果没有拆分过则取头部作为开始
			begin = in_begin ;
			bfind = true ;
		}

		if ( ! bfind ) {
			++ p ;
			++ pos ;
			continue ;
		}

		int pack_len = p - begin + 2;

		struct packet *item = (struct packet *) malloc(sizeof(struct packet));
		if (item == NULL) break ;

		item->data = (unsigned char *) malloc(pack_len + 3);
		memset(item->data, 0, pack_len + 3);
		//!! begin copy data from the second '[' and end with first ']'
		memcpy(item->data, begin, pack_len);
		item->len = pack_len;

		if (packet_list_ptr == NULL)
		{
			packet_list_ptr = (struct list_head *) malloc(sizeof(struct list_head));
			if (packet_list_ptr == NULL)
				return NULL;

			INIT_LIST_HEAD(packet_list_ptr);
		}
		list_add_tail(&item->list, packet_list_ptr);
		begin = NULL;

		in_begin = p   + 2 ;
		p   	 = p   + 2 ;
		pos 	 = pos + 2 ;

		ndel     = pos ;
		bfind    = false ;
	};

	if ( ndel > 0 )
	{
		fifo->removePos( ndel ) ;
	}
	// printf("get packet total %d packets\n", counter);

	return (struct packet*) packet_list_ptr;
}

struct packet * get_packet_from_kfifo_v1( DataBuffer *fifo ) {

	unsigned int len = fifo->getLength() ;
	if ( len <= 0 || len > MAX_PACK_LEN ){
		fifo->resetBuf() ;  // 超出大小就重置BUF
		return NULL;
	}

	char *begin = NULL ;
	struct list_head *packet_list_ptr = NULL;

	unsigned int       ndel = 0 ;
	unsigned int       pos  = 0 ;
	unsigned short msg_len  = 0 ;

	char *p = (char *)fifo->getBuffer() ;
	while ( p != NULL && pos < len )
	{
		if (*p == '[' && *(p + 1) == '[')
		{
			begin = p ;
			// 取得消息长度
			memcpy( &msg_len, p+2, sizeof(short) ) ;
			// 转换长度的字节序
			msg_len = ntohs( msg_len ) ;
			// 如果非法长度处理
			if ( msg_len <= 0 || msg_len > len ) {
				p   = p + 2 ;
				pos = pos + 2 ;
				continue ;
			}

			// 判断长度是否结束标识
			if ( * (p + msg_len - 1) == ']' && *( p + msg_len -2 ) == ']' ) {
				p   = p   + msg_len - 1;
				pos = pos + msg_len - 1;
			} else { // 如果不为结束标识就继续查找下一个
				p   = p   + 2 ;
				pos = pos + 2 ;
				continue ;
			}
		} else {
			++ p ; ++ pos ;
			// 如果不为结束标识
			if ( ! (*(p-1) == ']' && *p == ']') ) {
				continue ;
			}
			// 无开始只有结束
			if ( begin == NULL ) {
				continue ;
			}
		}

		int pack_len = p - begin + 1 ;
		if ( pack_len > len || pack_len < 2 ) {
			break ;
		}
		struct packet *item = (struct packet *) malloc(sizeof(struct packet));
		if (item == NULL) break ;

		item->data = (unsigned char *) malloc(pack_len + 3);
		memset(item->data, 0, pack_len + 3);
		//!! begin copy data from the second '[' and end with first ']'
		memcpy(item->data, begin, pack_len);
		item->len = pack_len;

		if (packet_list_ptr == NULL)
		{
			packet_list_ptr = (struct list_head *) malloc(sizeof(struct list_head));
			if (packet_list_ptr == NULL)
				return NULL;

			INIT_LIST_HEAD(packet_list_ptr);
		}
		list_add_tail(&item->list, packet_list_ptr);

		begin 	= NULL    ;
		p 	    = p + 1   ;
		pos     = pos + 1 ;
		ndel    = pos ;
	};
	// 如果需要移除的数据不为零时
	if ( ndel > 0 ) {
		fifo->removePos( ndel ) ;
	}
	return (struct packet*) packet_list_ptr;
}

/* fetch a valid packet list from kfifo--loop buffer, last reset loop buffer offset tag */
struct packet *get_packet_from_kfifo(DataBuffer *fifo)
{
	unsigned int len = fifo->getLength() ;
	if ( len <= 0 || len > MAX_PACK_LEN ){
		fifo->resetBuf() ;
		return NULL;
	}

	char *out_begin = NULL, *p, *in_begin = NULL ;
	struct list_head *packet_list_ptr = NULL;

	unsigned int i = 0;
	int protocol = E_PROTO_IDLE; //1 1: out , 2: in
	in_begin = p = (char *) fifo->getBuffer() ;
	unsigned int ndel = 0 ;

	while (i < len)
	{
		if (*p == '[')
		{
			protocol = E_PROTO_OUT;
			out_begin = p;
		}
		else if (out_begin != NULL && *p == ']')
		{
			int pack_len = p - out_begin + 1;

			struct packet *item = (struct packet *) malloc(sizeof(struct packet));
			if (item == NULL) break ;

			item->data = (unsigned char *) malloc(pack_len);
			memset(item->data, 0, pack_len);
			//!! begin copy data from the second '[' and end with first ']'
			memcpy(item->data, out_begin, pack_len);
			item->len = pack_len;
			item->type = E_PROTO_OUT;

			if (packet_list_ptr == NULL)
			{
				packet_list_ptr = (struct list_head *) malloc(sizeof(struct list_head));
				if (packet_list_ptr == NULL)
					return NULL;

				INIT_LIST_HEAD(packet_list_ptr);
			}

			list_add_tail(&item->list, packet_list_ptr);
			out_begin = NULL;
			protocol = E_PROTO_IDLE;
			//跳转到']'的下一个字符。
			in_begin = p + 1;
			ndel     = i + 1 ;
		}
		else if (protocol != E_PROTO_OUT && *p == '\r' && *(p + 1) == '\n')
		{
			int pack_len = p - in_begin + 2;
			struct packet *item = (struct packet *) malloc(sizeof(struct packet));
			if (item == NULL) break ;

			item->data = (unsigned char *) malloc(pack_len + 3);
			memset(item->data, 0, pack_len + 3);
			//!! begin copy data from the second '[' and end with first ']'
			memcpy(item->data, in_begin, pack_len);
			item->len = pack_len;
			item->type = E_PROTO_IN;

			if (packet_list_ptr == NULL)
			{
				packet_list_ptr = (struct list_head *) malloc(sizeof(struct list_head));
				if (packet_list_ptr == NULL)
					return NULL;

				INIT_LIST_HEAD(packet_list_ptr);
			}

			list_add_tail(&item->list, packet_list_ptr);

			protocol = E_PROTO_IDLE;
			in_begin = p + 2;

			++ p ;
			++ i ;

			ndel = i + 1 ;
		}

		++ p ;
		++ i ;
	}

	if ( ndel > 0 ){
		fifo->removePos( ndel );
	}
	//printf("get packet total %d in packets , %d out packets \n", in_counter, out_counter);

	return (struct packet*) packet_list_ptr;
}

/////////////////////// CPacket处理对象　/////////////////////////////////
CPacket::~CPacket() {
	if ( _pack == NULL ) {
		return ;
	}

	struct packet* one;
	struct list_head *q, *next;
	struct list_head *head = (struct list_head *) _pack;

	list_for_each_safe(q, next, head) {
		one = list_entry(q, struct packet, list);
		// 回收内存处理
		free_packet( one ) ;
	}
	free( head ) ;
}

