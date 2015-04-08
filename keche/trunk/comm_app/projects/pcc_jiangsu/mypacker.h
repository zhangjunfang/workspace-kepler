/*
 * mypacker.h
 *
 *  Created on: 2012-5-14
 *      Author: think
 */

#ifndef __MYPACKER_H_
#define __MYPACKER_H_

#include <SocketHandle.h>
#include <protocol.h>
// 分包对象
class CMyPackSpliter : public IPackSpliter
{
public:
	CMyPackSpliter() {}
	virtual ~CMyPackSpliter() {}
	// 分包处理
	virtual struct packet * get_kfifo_packet( DataBuffer *fifo )
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

		while (i < len)
		{
			if (*p == '*')
			{
				protocol = E_PROTO_OUT;
				out_begin = p;
			} else if (out_begin != NULL && *p == '#') {

				int pack_len = p - out_begin + 1;

				struct packet *item = (struct packet *) malloc(sizeof(struct packet));
				if (item == NULL)
					return (struct packet *) packet_list_ptr;
				item->data = (unsigned char *) malloc(pack_len+1);
				memset(item->data, 0, pack_len+1);
				//!! begin copy data from the second '*' and end with first '#'
				memcpy(item->data, out_begin, pack_len);
				item->len  = pack_len;
				item->type = E_PROTO_OUT;

				if (packet_list_ptr == NULL){
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

			}else if (protocol != E_PROTO_OUT && *p == '\r' && *(p + 1) == '\n'){

				int pack_len = p - in_begin + 2;
				struct packet *item = (struct packet *) malloc(sizeof(struct packet));
				if (item == NULL)
					return (struct packet *) packet_list_ptr;
				item->data = (unsigned char *) malloc(pack_len + 3);
				memset(item->data, 0, pack_len + 3);
				//!! begin copy data from the second '[' and end with first ']'
				memcpy(item->data, in_begin, pack_len);
				item->len = pack_len;
				item->type = E_PROTO_IN;

				if (packet_list_ptr == NULL){
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

			}

			++ p ;
			++ i ;
		}

		unsigned int del_len = (unsigned int)( in_begin - (char *) fifo->getBuffer() ) ;
		if (del_len > 0 && (unsigned int) del_len <= len){
			fifo->removePos( del_len );
		}
		//printf("get packet total %d in packets , %d out packets \n", in_counter, out_counter);

		return (struct packet*) packet_list_ptr;
	}
	// 释放数据包
	virtual void free_kfifo_packet( struct packet *packet ) {
		free_packet( packet ) ;
	}
};

#endif /* MYPACKER_H_ */
