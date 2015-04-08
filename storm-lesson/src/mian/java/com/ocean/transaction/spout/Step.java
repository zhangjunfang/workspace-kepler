/**
 * 
 */
package com.ocean.transaction.spout;

import java.io.Serializable;

/**
 * @author ocean
 * date：2015年1月22日
 * description：
 */
public class Step implements Serializable {

	private static final long serialVersionUID = -967104032970464698L;
    /**
     * 一个批次数据的大小==数据偏移量
     */
	private  int  batchSize=10;
	/**
	 * 数据开始提取位置===数据提取起始位置
	 */
    private int  startPoint=0;
	/**
	 * @return the batchSize
	 */
	public int getBatchSize() {
		return batchSize;
	}
	/**
	 * @param batchSize the batchSize to set
	 */
	public void setBatchSize(int batchSize) {
		this.batchSize = batchSize;
	}
	/**
	 * @return the startPoint
	 */
	public int getStartPoint() {
		return startPoint;
	}
	/**
	 * @param startPoint the startPoint to set
	 */
	public void setStartPoint(int startPoint) {
		this.startPoint = startPoint;
	}
	
	@Override
	public String toString() {
		StringBuilder builder = new StringBuilder();
		builder.append("Step [batchSize=");
		builder.append(batchSize);
		builder.append(", startPoint=");
		builder.append(startPoint);
		builder.append("]");
		return builder.toString();
	}
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + batchSize;
		result = prime * result + startPoint;
		return result;
	}
	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Step other = (Step) obj;
		if (batchSize != other.batchSize)
			return false;
		if (startPoint != other.startPoint)
			return false;
		return true;
	}
    
    
}
