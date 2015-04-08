#include <stdio.h>
#include <assert.h>
#include <stdlib.h>
#include <string.h>

#include "kfifo.h"
#define min(a,b) ((a)<(b) ? (a):(b))

 /* kfifo_init - initialize a FIFO by malloc a new buffer */
struct kfifo *kfifo_init(unsigned int size  ){
	struct kfifo *f;
	if ( ( f = ( struct kfifo * ) malloc( sizeof(struct kfifo) ) ) == NULL )
		return (struct kfifo *)( 0 );

	if ( (  f->buffer = ( unsigned char * ) malloc( size ) ) == NULL ){
		free ( f );
		return 0;
	}

	f->size = size;
	f->in = f->out = 0;
	return f;
}

 /*
 * kfifo_add_in internal helper function for updating the in offset
 */
 void kfifo_add_in(struct kfifo *fifo, unsigned int off)
{
	fifo->in += off;
}

 /**
 * kfifo_initialized - Check if kfifo is initialized.
 * @fifo: fifo to check
 * Return %true if FIFO is initialized, otherwise %false.
 * Assumes the fifo was 0 before.
 */
bool kfifo_initialized(struct kfifo *fifo)
{
	return fifo->buffer != NULL;
}


 



/**
 * kfifo_reset - removes the entire FIFO contents
 * @fifo: the fifo to be emptied.
 */
void kfifo_reset(struct kfifo *fifo)
{
	fifo->in = fifo->out = 0;
}

/**
 * kfifo_reset_out - skip FIFO contents
 * @fifo: the fifo to be emptied.
 */
 void kfifo_reset_out(struct kfifo *fifo)
{
	fifo->out = fifo->in;
}

/**
 * kfifo_size - returns the size of the fifo in bytes
 * @fifo: the fifo to be used.
 */
 unsigned int kfifo_size(struct kfifo *fifo)
{
	return fifo->size;
}

/**
 * kfifo_len - returns the number of used bytes in the FIFO
 * @fifo: the fifo to be used.
 */
unsigned int kfifo_len(struct kfifo *fifo)
{
	register unsigned int	out;

	out = fifo->out;

	return fifo->in - out;
}

/**
 * kfifo_is_empty - returns true if the fifo is empty
 * @fifo: the fifo to be used.
 */
 int kfifo_is_empty(struct kfifo *fifo)
{
	return fifo->in == fifo->out;
}

/**
 * kfifo_is_full - returns true if the fifo is full
 * @fifo: the fifo to be used.
 */
  int kfifo_is_full(struct kfifo *fifo)
{
	return kfifo_len(fifo) == kfifo_size(fifo);
}






/*
 * kfifo_add_out internal helper function for updating the out offset
 */
void kfifo_add_out(struct kfifo *fifo,
				unsigned int off)
{

	fifo->out += off;
}



/*
 * kfifo_off internal helper function for calculating the index of a
 * given offeset
 */
 unsigned int kfifo_off(struct kfifo *fifo, unsigned int off)
{
	return off & (fifo->size - 1);
}



/**
 * kfifo_free - frees the FIFO internal buffer
 * @fifo: the fifo to be freed.
 */
void kfifo_free(struct kfifo *fifo)
{
	if  ( fifo == NULL )
		return;

	if (fifo->buffer)
		free(fifo->buffer);

	free(fifo);
}

 









void kfifo_in_data(struct kfifo *fifo,	const void *from, unsigned int len, unsigned int off)
{
	unsigned int l;

	/*
	 * Ensure that we sample the fifo->out index -before- we
	 * start putting bytes into the kfifo.
	 */

	off = kfifo_off(fifo, fifo->in + off);

	/* first put the data starting from fifo->in to buffer end */
	l = min(len, fifo->size - off);
	memcpy(fifo->buffer + off, from, l);

	/* then put the rest (if any) at the beginning of the buffer */
	memcpy(fifo->buffer, (const char*)from + l, len - l);
}

void kfifo_out_data(struct kfifo *fifo,
		void *to, unsigned int len, unsigned int off)
{
	unsigned int l;

	/*
	 * Ensure that we sample the fifo->in index -before- we
	 * start removing bytes from the kfifo.
	 */

	off = kfifo_off(fifo, fifo->out + off);

	/* first get the data from fifo->out until the end of the buffer */
	l = min(len, fifo->size - off);
	memcpy(to, fifo->buffer + off, l);

	/* then get the rest (if any) from the beginning of the buffer */
	memcpy((char *)to + l, fifo->buffer, len - l);
}


/**
 * kfifo_avail - returns the number of bytes available in the FIFO
 * @fifo: the fifo to be used.
 */
 unsigned int kfifo_avail(struct kfifo *fifo)
{
	return kfifo_size(fifo) - kfifo_len(fifo);
}

/**
 * kfifo_in - puts some data into the FIFO
 * @fifo: the fifo to be used.
 * @from: the data to be added.
 * @len: the length of the data to be added.
 *
 * This function copies at most @len bytes from the @from buffer into
 * the FIFO depending on the free space, and returns the number of
 * bytes copied.
 *
 * Note that with only one concurrent reader and one concurrent
 * writer, you don't need extra locking to use these functions.
 */
unsigned int kfifo_in(struct kfifo *fifo, const void *from, unsigned int len)
{
	len = min(kfifo_avail(fifo), len);

	kfifo_in_data(fifo, from, len, 0);
	kfifo_add_in(fifo, len);
	return len;
}

/**
 * kfifo_out - gets some data from the FIFO
 * @fifo: the fifo to be used.
 * @to: where the data must be copied.
 * @len: the size of the destination buffer.
 *
 * This function copies at most @len bytes from the FIFO into the
 * @to buffer and returns the number of copied bytes.
 *
 * Note that with only one concurrent reader and one concurrent
 * writer, you don't need extra locking to use these functions.
 */
unsigned int kfifo_out(struct kfifo *fifo, void *to, unsigned int len)
{
	len = min(kfifo_len(fifo), len);
	

	kfifo_out_data(fifo, to, len, 0);
	kfifo_add_out(fifo, len);

	return len;
}


/**
 * kfifo_put - puts some data into the FIFO
 * @fifo: the fifo to be used.
 * @from: the data to be added.
 * @len: the length of the data to be added.
 */
unsigned int kfifo_put(struct kfifo *fifo, unsigned char *buffer, unsigned int len)
{
	return kfifo_in(fifo, buffer,len);
}




/**
 * kfifo_get - gets some data from the FIFO
 * @fifo: the fifo to be used.
 * @to: where the data must be copied.
 * @len: the size of the destination buffer.
 */
unsigned int kfifo_get(struct kfifo *fifo, unsigned char *buffer, unsigned int len)
{
	return kfifo_out(fifo, buffer, len);
}


/**
 * kfifo_data - gets some data from the FIFO
 * @fifo: the fifo to be used.
 * @to: where the data must be copied.
 * @len: the size of the destination buffer, not cut in destination buffer
 *
 * This function readed at most @len bytes from the FIFO into the
 * @to buffer and returns the number of readed bytes.
 *
 * Note that with only one concurrent reader and one concurrent
 * writer, you don't need extra locking to use these functions.
 */
unsigned int kfifo_data(struct kfifo *fifo, void *data, unsigned int len)
{
	len = min(kfifo_len(fifo), len);
	kfifo_out_data(fifo, data, len, 0);
	return len;
}
