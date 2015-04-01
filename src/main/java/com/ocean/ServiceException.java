package com.ocean;

public class ServiceException extends Exception {
	/**
	 * 
	 */
	private static final long serialVersionUID = 385796972126486327L;

	public ServiceException() {
		super();
	}

	public ServiceException(String msg) {
		super(msg);
	}

	public ServiceException(String msg, Throwable cause) {
		super(msg, cause);
	}

	public ServiceException(Throwable cause) {
		super(cause);
	}
}