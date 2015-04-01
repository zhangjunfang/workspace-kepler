package com.ocean;

public class FileException extends ServiceException {
	/**
	 * 
	 */
	private static final long serialVersionUID = -3077686181541820370L;

	public FileException() {
		super();
	}

	public FileException(String msg) {
		super(msg);
	}

	public FileException(String msg, Throwable cause) {
		super(msg, cause);
	}

	public FileException(Throwable cause) {
		super(cause);
	}
}