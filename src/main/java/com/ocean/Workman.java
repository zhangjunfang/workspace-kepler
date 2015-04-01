package com.ocean;

public interface Workman {
	public boolean receive(WareHouse inhouse);

	public String getHost();

	public int getPort();
}