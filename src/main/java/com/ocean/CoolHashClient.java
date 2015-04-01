package com.ocean;

public interface CoolHashClient extends CoolHash {
	public void begin();

	public void rollback();

	public void commit();

	public void exit();
}