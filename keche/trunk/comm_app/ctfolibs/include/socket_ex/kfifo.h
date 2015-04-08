/*
 * A generic kernel FIFO implementation.
 *
 * Copyright (C) 2009 Stefani Seibold <stefani@seibold.net>
 * Copyright (C) 2004 Stelian Pop <stelian@popies.net>
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
 *
 */

/*
 * Howto porting drivers to the new generic fifo API:
 *
 * - Modify the declaration of the "struct kfifo *" object into a
 *   in-place "struct kfifo" object
 * - Init the in-place object with kfifo_alloc() or kfifo_init()
 *   Note: The address of the in-place "struct kfifo" object must be
 *   passed as the first argument to this functions
 * - Replace the use of kfifo_put into kfifo_in and kfifo_get
 *   into kfifo_out
 * - Replace the use of kfifo_put into kfifo_in_locked and kfifo_get
 *   into kfifo_out_locked
 *   Note: the spinlock pointer formerly passed to kfifo_init/kfifo_alloc
 *   must be passed now to the kfifo_in_locked and kfifo_out_locked
 *   as the last parameter.
 * - All formerly name kfifo_* functions has been renamed into kfifo_*
 */

#ifndef __KFIFO_H__
#define __KFIFO_H__

// 将数据BUF定义为10K
#define KFIFO_BUFF_SIZE  10*1024

struct kfifo {
	unsigned int  fd ; 		/* KFIFO扩展字段，记录数据来自于哪个FD */
	unsigned char *buffer;	/* the buffer holding the data */
	unsigned int size;	/* the size of the allocated buffer */
	unsigned int in;	/* data is added at offset (in % size) */
	unsigned int out;	/* data is extracted from off. (out % size) */
};

/**
 * kfifo_init - initialize a FIFO by malloc a new buffer
 * @fifo: the fifo to assign the buffer
 * @size: the size of the internal buffer
 *
 */

struct kfifo *kfifo_init( unsigned int size);

unsigned int kfifo_put(struct kfifo *fifo, unsigned char *buffer, unsigned int len);
unsigned int kfifo_get(struct kfifo *fifo, unsigned char *buffer, unsigned int len);
void kfifo_in_data(struct kfifo *fifo,	const void *from, unsigned int len, unsigned int off);
unsigned int kfifo_in(struct kfifo *fifo, const void *from, unsigned int len);
unsigned int kfifo_out(struct kfifo *fifo, void *to, unsigned int len);
unsigned int kfifo_data(struct kfifo *fifo, void *data, unsigned int len);
unsigned int kfifo_avail(struct kfifo *fifo);
void kfifo_add_in(struct kfifo *fifo, unsigned int off);
unsigned int kfifo_len(struct kfifo *fifo);
void kfifo_add_out(struct kfifo *fifo, unsigned int off);
void kfifo_out_data(struct kfifo *fifo, void *to, unsigned int len, unsigned int off);


/**
 * kfifo_free - frees the FIFO internal buffer
 * @fifo: the fifo to be freed.
 */
void kfifo_free(struct kfifo *fifo);

unsigned int kfifo_in(struct kfifo *fifo, const void *from, unsigned int len);
unsigned int kfifo_out(struct kfifo *fifo, void *to, unsigned int len);

#endif
