package com.ocean;

import java.util.EventObject;

public class LastestEvent extends EventObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = 7992407348325870643L;

	public LastestEvent(Object source) {
		super(source);
	}

	public void setSource(Object source) {
		this.source = source;
	}
}