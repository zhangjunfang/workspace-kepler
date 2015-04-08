package com.kypt.c2pp.buffer;

import java.util.LinkedList;
import java.util.Queue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.inside.msg.InsideMsg;

public class DownCommandBuffer {

	private static Logger log = LoggerFactory
			.getLogger(DownCommandBuffer.class);

	private static final DownCommandBuffer downCommandBuffer = new DownCommandBuffer();

	private static final String NAME = "<DownCommandBuffer>";

	private Queue<InsideMsg> commandQueue;

	private DownCommandBuffer() {
		commandQueue = new LinkedList<InsideMsg>();
	}

	public static DownCommandBuffer getInstance() {
		return downCommandBuffer;
	}

	/**
	 * 向队列中加入一个下行命令对象
	 * 
	 * @param sql
	 */
	public synchronized void add(InsideMsg msg) {
		commandQueue.offer(msg);
	}

	/**
	 * 向队列中加入一组下行命令对象
	 * 
	 * @param sqlList
	 */
	public synchronized void add(InsideMsg msgs[]) {
		for (InsideMsg msg : msgs) {
			commandQueue.offer(msg);
		}
	}
	
	/**
	 * 从下行命令队列中提取对象
	 * @return
	 */
	public synchronized InsideMsg getDownMsg(){
		return commandQueue.poll();
	}

}
