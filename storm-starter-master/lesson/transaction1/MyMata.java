package transaction1;

import java.io.Serializable;

public class MyMata implements Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	private long beginPoint;// 事务开始位置

	private int num;// batch 的tuple个数

	@Override
	public String toString() {
		return getBeginPoint() + "----" + getNum();
	}

	public long getBeginPoint() {
		return beginPoint;
	}

	public void setBeginPoint(long beginPoint) {
		this.beginPoint = beginPoint;
	}

	public int getNum() {
		return num;
	}

	public void setNum(int num) {
		this.num = num;
	}

}
