/*
 * sfpacker.h
 *
 *  Created on: 2013年12月20日
 *      Author: ycq
 */

#ifndef _SFPACKER_H_
#define _SFPACKER_H_ 1

#include <protocol.h>
#include <SocketHandle.h>

struct CSFSpliter: public IPackSpliter {
	struct packet * get_kfifo_packet(DataBuffer *fifo) {
		char *ptr = fifo->getBuffer();
		unsigned int len = fifo->getLength();

		struct packet *item = NULL;
		struct list_head *packet_list_ptr = NULL;

		packet_list_ptr = (struct list_head*)malloc(sizeof(struct list_head));
		if (packet_list_ptr == NULL) {
			return NULL;
		}
		INIT_LIST_HEAD (packet_list_ptr);

		item = (struct packet *)malloc(sizeof(struct packet));
		if(item == NULL) {
			free(packet_list_ptr);
			return NULL;
		}

		item->type = E_PROTO_IN;
		item->len = len;
		item->data = (unsigned char*)malloc(len);
		if(item->data == NULL) {
			free(packet_list_ptr);
			free(item);
			return NULL;
		}

		memcpy(item->data, ptr, len);

		list_add_tail(&item->list, packet_list_ptr);

		fifo->resetBuf();

		return (struct packet*)packet_list_ptr;
	}

	// 释放数据包
	void free_kfifo_packet(struct packet *packet) {
		free_packet(packet);
	}
};



#endif//_SFPACKER_H_
