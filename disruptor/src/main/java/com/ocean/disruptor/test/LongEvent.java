package com.ocean.disruptor.test;
/**
 * 
 * @author ocean
 * 
 * 
 *即将存入ringbuffer中的数据，触发的事件
 *
 */

public class LongEvent
{
	private long value=0;
	
    public void set(long value)
    {
    	 this.value=value;
    	 System.err.println("LongEvent  set: "+ value);
    }
    
    @Override
    public String toString() {
    	
    	return "LongEvent---即将插入的数据： "+value;
    }
}