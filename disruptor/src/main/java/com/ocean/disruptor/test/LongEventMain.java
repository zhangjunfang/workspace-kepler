package com.ocean.disruptor.test;

import java.nio.ByteBuffer;
import java.util.concurrent.Executor;
import java.util.concurrent.Executors;

import com.lmax.disruptor.RingBuffer;
import com.lmax.disruptor.dsl.Disruptor;
import com.lmax.disruptor.dsl.EventHandlerGroup;

public class LongEventMain {
	@SuppressWarnings("unchecked")
	public static void main(String[] args) throws Exception {
		// Executor that will be used to construct new threads for consumers
		Executor executor = Executors.newCachedThreadPool();

		// The factory for the event
		LongEventFactory factory = new LongEventFactory();

		// Specify the size of the ring buffer, must be power of 2.
		int bufferSize = 1024;

		// Construct the Disruptor
		Disruptor<LongEvent> disruptor = new Disruptor<LongEvent>(factory,
				bufferSize, executor);
		EventHandlerGroup<LongEvent> handleEventsWith =(EventHandlerGroup<LongEvent>) disruptor
				.handleEventsWith(new LongEventHandler());
		System.err.println("--------"+handleEventsWith);
		// Start the Disruptor, starts all threads running
		 disruptor.start();

		// Get the ring buffer from the Disruptor to be used for publishing.
		RingBuffer<LongEvent> ringBuffer = disruptor.getRingBuffer();
		LongEventProducer producer = new LongEventProducer(ringBuffer);

		ByteBuffer bb = ByteBuffer.allocate(8);
//		for (long l = 0; true; l++) {
//			bb.putLong(0, l);
//			producer.onData(bb);
//			//Thread.sleep(1000);
//		}
		for (long l = 0; l<100; l++) {
			bb.putLong(0, l);
			producer.onData(bb);
			//Thread.sleep(1000);
		}
	}
}