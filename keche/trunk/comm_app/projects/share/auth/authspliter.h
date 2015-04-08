/*
 * authspliter.h
 *
 *  Created on: 2012-6-5
 *      Author: think
 */

#ifndef __AUTHSPLITER_H__
#define __AUTHSPLITER_H__

#include <protocol.h>
#include <SocketHandle.h>
#include <interheader.h>

class CAuthSpliter : public IPackSpliter
{
public:
	CAuthSpliter(){}
	~CAuthSpliter(){}
	// 分包处理
	struct packet * get_kfifo_packet( DataBuffer *fifo ) {
		unsigned int len = fifo->getLength() ;
		if ( len <= 0 || len > 10240 ){
			fifo->resetBuf() ;
			return NULL;
		}

		char *p = NULL ;
		struct list_head *packet_list_ptr = NULL;
		unsigned int ndel = 0 ;
		unsigned int i = 0;
		p = (char *) fifo->getBuffer() ;
		// OUT_HEX( NULL, 0, "Spliter", p, len ) ;
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
				//!! begin copy data from the second '[' and end with first ']'
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
				// OUT_INFO( NULL, 0, "Spliter", "split pack pack length: %d" , pack_len ) ;
				//跳转到']'的下一个字符。
				ndel = i + pack_len ;

				p = p + pack_len - 1 ;  // 直接转到0x5d结束标识上面
				i = i + pack_len - 1 ;
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

	// 释放数据包
	void free_kfifo_packet( struct packet *packet ) {
		free_packet( packet ) ;
	}
};


#endif /* AUTHSPLITER_H_ */
