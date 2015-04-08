/*
 * photosvr.h
 *
 *  Created on: 2013-5-8
 *      Author: ycq
 *  消息转发服务
 */

#ifndef _PHOTOSVR_H_
#define _PHOTOSVR_H_ 1

#include "interface.h"
#include <BaseServer.h>
#include <interpacker.h>

#include <set>
#include <vector>
#include <string>

#include "../tools/tqueue.h"

using std::set;
using std::vector;
using std::string;

#pragma pack(1)
struct DVR_HEAD {
	uint16_t ver;
	uint16_t cmd;
	uint32_t len;
	uint32_t seq;
};

struct DVR_REPLY {
	uint32_t seq;
	uint8_t res;
};

struct DVR_LOGIN {
	char name[16];
	char pass[32];
};

// 图片上传
struct DVR_UPLOAD_PIC {
	char simid[12];
	uint32_t mmid;
	uint8_t mmtype;
	uint8_t mmfmt;
	uint8_t mmevent;
	uint8_t channel;
	uint32_t takeseq;
	char taketime[14];
	char mmurl[0];
};

// 手工拍照
struct DVR_MANUAL_PIC {
	char simid[12];
	uint8_t channel;
	uint16_t takesize;
	uint16_t taketime;
	uint8_t resolution;
	uint8_t quality;
	uint8_t light;
	uint8_t contrast;
	uint8_t saturation;
	uint8_t chroma;
	uint32_t takeseq;
};

// 定时拍照
struct DVR_TIMING_PIC {
	char simid[12];
	uint8_t channel;
	uint8_t resolution;
	uint8_t quality;
	uint8_t light;
	uint8_t contrast;
	uint8_t saturation;
	uint8_t chroma;
	char btime[14];
	char etime[14];
	uint16_t interval;
};

#pragma pack()

class CDvrSpliter : public IPackSpliter
{
	static const uint16_t PROT_CUR_VER = 0x0101;
	static const uint32_t PACK_MIN_SIZE = 12;
public:
	CDvrSpliter() {};
	virtual ~CDvrSpliter(){};

	// 拆解808处理
	struct packet* get_kfifo_packet( DataBuffer *fifo ) {
		uint32_t pos = 0;
		uint32_t len = fifo->getLength();
		char     *ptr = fifo->getBuffer();
		if(len < PACK_MIN_SIZE) {
			return NULL;
		}

		if(len > MAX_SOCKET_BUF * 2) {
			fifo->resetBuf();
			return NULL;
		}

		struct packet *itemPtr = NULL;
		struct list_head *listPtr = NULL;

		DVR_HEAD *headptr;
		uint16_t msgver;
		uint16_t msgcmd;
		uint32_t msglen;

		while(pos + PACK_MIN_SIZE <= len) {
			headptr = (DVR_HEAD*)(ptr + pos);
			msgver = ntohs(headptr->ver);
			msgcmd = ntohs(headptr->cmd);
			msglen = ntohl(headptr->len) + PACK_MIN_SIZE;

			if(msgver != PROT_CUR_VER || msglen > MAX_SOCKET_BUF) {
				fifo->resetBuf();
				OUT_INFO(NULL, 0, "resetBuf", "ver = %x, cmd = %x, len = %u", msgver, msgcmd, msglen);
				break;
			}

			bool checkCmd = false;
			switch(msgcmd) {
			case 0x0001:
			case 0x0002:
			case 0x0003:
			case 0x8004:
			case 0x8005:
				checkCmd = true;
				break;
			}

			if(checkCmd == false) {
				fifo->resetBuf();
				OUT_INFO(NULL, 0, "resetBuf", "ver = %x, cmd = %x, len = %u", msgver, msgcmd, msglen);
				break;
			}

			if(pos + msglen > len) {
				break;
			}

			if (listPtr == NULL) {
				listPtr = (struct list_head*) malloc(sizeof(struct list_head));
				if (listPtr == NULL) {
					break;
				}
				INIT_LIST_HEAD(listPtr);
			}

			itemPtr = (struct packet*) malloc(sizeof(struct packet));
			if (itemPtr == NULL) {
				break;
			}
			itemPtr->type = E_PROTO_OUT;
			itemPtr->len = msglen;
			itemPtr->data = (unsigned char*) malloc(msglen);
			if (itemPtr->data == NULL) {
				free(itemPtr);
				break;
			}
			memcpy(itemPtr->data, ptr + pos, msglen);
			list_add_tail(&itemPtr->list, listPtr);

			pos += msglen;
		}

		if(pos > 0) fifo->removePos(pos);

		return (struct packet*)listPtr;
	}

	// 释放数据
	void free_kfifo_packet( struct packet *packet ) {
		free_packet( packet ) ;
	}
};

//会话超时时间
#define SESS_TIMEOUT  180

class PhotoSvr : public BaseServer , public IPhotoSvr {
	// 环境对象指针
	ISystemEnv			    *_pEnv ;
	// 处理线程数
	unsigned  int		     _thread_num ;
	// 数据分包对象
	CDvrSpliter   		     _pack_spliter ;

	// 在线用户处理
	OnlineUser   		     _online_user;
	//
	uint32_t                 _seqid;
	//
	string                   _scp_path;
	// 手机号码查询接口
	string                   _httpUrl;
	//
	TimeQueue<uint32_t, InnerMsg> _timeQueue;
public:
	PhotoSvr() ;
	~PhotoSvr() ;

	// 服务初始化
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始启动服务器
	virtual bool Start( void ) ;
	// STOP方法
	virtual void Stop( void ) ;
	////////////////////////////////////////////////////////////
	virtual void on_data_arrived( socket_t *sock, const void *data, int len);
	virtual void on_new_connection( socket_t *sock, const char* ip, int port);
	virtual void on_dis_connection( socket_t *sock );


	virtual void TimeWork();
	virtual void NoopWork();

	virtual void HandleData( const InnerMsg & msg ) ;
private:
	string getInnerMsgArg(const InnerMsg &msg, const string &key);
	uint32_t getSeqid();
	bool createDir(const string &file);
	bool get2gby3g(const string &sim3g, string &sim2g, string &oem);
};

#endif//_PHOTOSVR_H_
