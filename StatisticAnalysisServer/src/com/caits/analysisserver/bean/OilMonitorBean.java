package com.caits.analysisserver.bean;

import java.util.ArrayDeque;
import java.util.Deque;
import java.util.Iterator;

public class OilMonitorBean {
	private boolean isAccOpen = false; // 标记ACC开
	
	private long accOpenGpsTime = 0; // 标记ACC开GpsTime
	
	private Deque<OilmassChangedDetail> accOpenOilQeque = new ArrayDeque<OilmassChangedDetail>(3); 
	
	private boolean isAccClose = false; // 标记ACC关
	
	private long accCloseGpsTime = 0; // 标记ACC关GpsTime 

	private Deque<OilmassChangedDetail> accCloseOilQeque = new ArrayDeque<OilmassChangedDetail>(3); 
	
	public Deque<OilmassChangedDetail> getAccOpenOilQeque() {
		return accOpenOilQeque;
	}

	public void setAccOpenOilQeque(Deque<OilmassChangedDetail> accOpenOilQeque) {
		this.accOpenOilQeque = accOpenOilQeque;
	}
	
	public void cloneAccOpenDeque(Deque<OilmassChangedDetail> qeque){
		accOpenOilQeque.clear();
		Iterator<OilmassChangedDetail> it = qeque.iterator();
		while(it.hasNext()){
			accOpenOilQeque.addLast(it.next());
		}
	}

	public Deque<OilmassChangedDetail> getAccCloseOilQeque() {
		return accCloseOilQeque;
	}

	public void setAccCloseOilQeque(Deque<OilmassChangedDetail> accCloseOilQeque) {
		this.accCloseOilQeque = accCloseOilQeque;
	}

	public boolean isAccOpen() {
		return isAccOpen;
	}

	public void setAccOpen(boolean isAccOpen) {
		this.isAccOpen = isAccOpen;
	}

	public long getAccOpenGpsTime() {
		return accOpenGpsTime;
	}

	public void setAccOpenGpsTime(long accOpenGpsTime) {
		this.accOpenGpsTime = accOpenGpsTime;
	}

	public boolean isAccClose() {
		return isAccClose;
	}

	public void setAccClose(boolean isAccClose) {
		this.isAccClose = isAccClose;
	}

	public long getAccCloseGpsTime() {
		return accCloseGpsTime;
	}

	public void setAccCloseGpsTime(long accCloseGpsTime) {
		this.accCloseGpsTime = accCloseGpsTime;
	}
	
	public int getAccCloseAvgOil(){
		int temp =0;
		Iterator<OilmassChangedDetail> oilIt = accCloseOilQeque.iterator();
		while(oilIt.hasNext()){
			temp = temp + oilIt.next().getOil();
		}// End while
		
		return temp / accCloseOilQeque.size();
	}
	
	public int getAccOpenAvgOil(){
		int temp =0;
		Iterator<OilmassChangedDetail> oilIt = accOpenOilQeque.iterator();
		while(oilIt.hasNext()){
			temp = temp + oilIt.next().getOil();
		}// End while
	
		return temp / accOpenOilQeque.size();
	}
	
	/****
	 * 根据一分钟所有点获取ACC开状态下点。
	 * 判断逻辑：一分钟油箱油量值不为0的点，去掉最大值和最小值，求平均值
	 * 
	 * 
	 * @return
	 */
	public OilmassChangedDetail getAccOpenOilPoint(){
		int temp =0;
		int minV = 0;
		int maxV = 0;
		int count = 0;
		Iterator<OilmassChangedDetail> oilIt = accOpenOilQeque.iterator();
		OilmassChangedDetail oil = null;
		
		while(oilIt.hasNext()){
			oil = oilIt.next();
			int v = 0;//oil.getCurr_oilmass();
			if(count != 0){
				minV = Math.min(v, minV);
				maxV = Math.max(v, maxV);
				
			}else{
				maxV = minV = v;
			}
			temp = temp + v;
			count++;
		}// End while
		
		if(count >2){
			temp = (temp - maxV - minV)/(count -2); 
		}else{
			temp = temp/count;	
		}
		
		if(oil != null){ 
			oil.setCurr_oilmass(temp/10); 
		}
		
		return oil;
	}
	
	/****
	 * 根据一分钟所有点获取ACC开状态下点。
	 * 判断逻辑：一分钟油箱油量值不为0的点，去掉最大值和最小值，求平均值
	 * 
	 * 
	 * @return
	 */
	public OilmassChangedDetail getCloseOpenOilPoint(){
		int temp =0;
		int minV = 0;
		int maxV = 0;
		int count = 0;
		Iterator<OilmassChangedDetail> oilIt = accCloseOilQeque.iterator();
		OilmassChangedDetail oil = null;
		
		while(oilIt.hasNext()){
			oil = oilIt.next();
			int v = 0;//oil.getCurr_oilmass();
			if(count != 0){
				minV = Math.min(v, minV);
				maxV = Math.max(v, maxV);
				
			}else{
				maxV = minV = v;
			}
			temp = temp + v;
			count++;
		}// End while
		
		if(count >2){
			temp = (temp - maxV - minV)/(count -2); 
		}else{
			temp = temp/count;	
		}
		if(oil != null){
			oil.setCurr_oilmass(temp/10);
		}
		return oil;
	}
	
	
	public void resetValue(){
		isAccOpen = false; // 标记ACC开
		
		accOpenGpsTime = 0; // 标记ACC开GpsTime 
		
		isAccClose = false; // 标记ACC关
		
		accCloseGpsTime = 0; // 标记ACC关GpsTime 
		
		accOpenOilQeque.clear();
		
		accCloseOilQeque.clear();
	}
}

