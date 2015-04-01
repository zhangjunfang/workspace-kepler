package com.ocean;

@SuppressWarnings({ "rawtypes" })
public class FileResult<E> extends Result {
	/**
	 * 
	 */
	private static final long serialVersionUID = 8734846425501146682L;

	public FileResult() {
		super();
	}

	public FileResult(boolean ready) {
		super(ready);
	}

	static FileResult getExceptionResult() {
		FileResult fr = new FileResult(false);
		fr.setReady(WareHouse.EXCEPTION);
		return fr;
	}
}