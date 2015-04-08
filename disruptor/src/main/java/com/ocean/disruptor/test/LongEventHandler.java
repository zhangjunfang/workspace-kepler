package com.ocean.disruptor.test;

import com.lmax.disruptor.EventHandler;
/**
 * 
 * @author ocean
 * 
 * 监听发布到ringbuffer中的事件：====
 *
 */
public class LongEventHandler implements EventHandler<LongEvent>
{
    public void onEvent(LongEvent event, long sequence, boolean endOfBatch)
    {
    	System.err.println("已经发布到ringbuffer中的事件名称"+event.getClass().getName()+"发布到ringbuffer中携带的数据"+event.toString());
    	System.err.println("发生事件获取到ringbuffer中的序列号:"+sequence+"是否是发布到ringbuffer中，最后的事件:"+endOfBatch);
          
    }
}