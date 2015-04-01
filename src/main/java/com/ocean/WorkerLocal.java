package com.ocean;

public interface WorkerLocal extends WorkerProxy {
	public WareHouse doTask(WareHouse inhouse);

	public WareHouse doTask(WareHouse inhouse, long timeoutseconds);

	public void interrupt();

	public String getHost();

	public int getPort();
}