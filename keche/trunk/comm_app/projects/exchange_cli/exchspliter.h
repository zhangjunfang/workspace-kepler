/*
 * exchspliter.h
 *
 *  Created on: 2014-5-15
 *      Author: ycq
 *
 */

#ifndef _EXCHSPLITER_H_
#define _EXCHSPLITER_H_ 1

#include <stdio.h>
#include <protocol.h>
#include <SocketHandle.h>
#include <comlog.h>

// 交换协议拆包处理
class CExchSpliter : public IPackSpliter {
	static const int PACK_MIN_SIZE = 21;
public:
	CExchSpliter() {};
	virtual ~CExchSpliter(){};

	// 分解交换协议消息包
	struct packet* get_kfifo_packet( DataBuffer *fifo ) {
		int   pos = 0;
		int   len = fifo->getLength();
		char *ptr = fifo->getBuffer();
		if(len < PACK_MIN_SIZE) {
			return NULL;
		}

		if(len > MAX_SOCKET_BUF * 2) {
			fifo->resetBuf();
			return NULL;
		}

		struct packet *itemPtr = NULL;
		int            itemLen = 0;
		struct list_head *listPtr = NULL;

		int i;
		unsigned char tag, crc, preCh, curCh;

		tag = crc = preCh = 0;
		for(i = 0; i < len; ++i) {
			curCh = ptr[i];

			if (curCh == 0x5b) {
				tag = 1;
				crc = preCh = 0;
				continue;
			} else if (curCh == 0x5a || curCh == 0x5e) {
				preCh = curCh;
				continue;
			} else if (curCh == 0x01 && preCh == 0x5a) {
				crc ^= 0x5b;
				preCh = 0;
				continue;
			} else if (curCh == 0x02 && preCh == 0x5a) {
				crc ^= 0x5a;
				preCh = 0;
				continue;
			} else if (curCh == 0x01 && preCh == 0x5e) {
				crc ^= 0x5d;
				preCh = 0;
				continue;
			} else if (curCh == 0x02 && preCh == 0x5e) {
				crc ^= 0x5e;
				preCh = 0;
				continue;
			} else if (curCh != 0x5d) {
				crc ^= curCh;
				preCh = 0;
				continue;
			}

			itemLen = i - pos + 1;
			if(itemLen < PACK_MIN_SIZE || tag == 0 || crc != 0) {
				OUT_HEX(NULL, 0, "ParseExchErr", ptr + pos, itemLen);
				pos = i + 1;
				tag = crc = preCh = 0;
				continue;
			}

			if(listPtr == NULL) {
				listPtr = (struct list_head*)malloc(sizeof(struct list_head));
				if(listPtr  == NULL) {
					break;
				}
				INIT_LIST_HEAD(listPtr);
			}

			itemPtr = (struct packet *) malloc(sizeof(struct packet));
			if(itemPtr == NULL) {
				break;
			}
			itemPtr->type = E_PROTO_OUT;
			itemPtr->len  = itemLen;
			itemPtr->data = (unsigned char*) malloc(itemLen);
			if(itemPtr->data == NULL) {
				free(itemPtr);
				break;
			}
			memcpy(itemPtr->data, ptr + pos, itemLen);
			list_add_tail(&itemPtr->list, listPtr);

			pos = i + 1;
			tag = crc = preCh = 0;
		}

		if(pos > 0) fifo->removePos(pos);

		return (struct packet*)listPtr;
	}

	// 释放数据
	void free_kfifo_packet( struct packet *packet ) {
		free_packet( packet ) ;
	}
};

#endif//_EXCHSPLITER_H_
