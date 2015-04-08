package com.ocean.disruptor.test;

import com.lmax.disruptor.EventFactory;

/***
 * 
 * @author ocean
 *   
 */
public class LongEventFactory implements EventFactory<LongEvent>
{
    public static final LongEventFactory INSTANCE = new LongEventFactory();

    public LongEvent newInstance()
    {
        return new LongEvent();
    }
}