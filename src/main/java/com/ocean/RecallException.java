package com.ocean;

public class RecallException extends ServiceException {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1051368536794672562L;
	private boolean recall;

	public RecallException() {
		super("The call has not been returned yet, you cant repeat call!");
	}

	public void setRecall(boolean recall) {
		this.recall = recall;
	}

	public boolean checkRecall() throws RecallException {
		if (recall)
			throw this;
		return recall;
	}

	public int tryRecall(WareHouse inhouse) {
		try {
			if (!checkRecall())
				setRecall(true);
		} catch (RecallException re) {
			LogUtil.info("[RecallException]", "[tryRecall]" + inhouse, re);
			return -1;
		}
		return 0;
	}
}