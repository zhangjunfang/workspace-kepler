package com.ctfo.trackservice.model;

import java.util.ArrayList;
import java.util.List;

public class StationHistory {
	/** 第一次经过站点		 */
	private List<Station> firstStation;
	/** 第二次经过站点		 */
	private List<Station> twoStation;
	/** 第三次经过站点		 */
	private List<Station> threeStation;
	/** 第四次经过站点		 */
	private List<Station> fourStation;
	/** 站点字符串列表		 */
	private List<String> stationStr;
	public StationHistory(){
		stationStr = new ArrayList<String>();
		firstStation = new ArrayList<Station>();
		twoStation = new ArrayList<Station>();
		threeStation = new ArrayList<Station>();
		fourStation = new ArrayList<Station>();
	}
	/**
	 * @return the 第一次经过站点
	 */
	public List<Station> getFirstStation() {
		return firstStation;
	}
	/**
	 * @param 第一次经过站点 the firstStation to set
	 */
	public void setFirstStation(List<Station> first) {
		this.firstStation = first;
	}
	/**
	 * @return the 第二次经过站点
	 */
	public List<Station> getTwoStation() {
		return twoStation;
	}
	/**
	 * @param 第二次经过站点 the twoStation to set
	 */
	public void setTwoStation(List<Station> two) {
		this.twoStation = two;
		for(Station fi : this.firstStation){
			for(Station tw : this.twoStation){
				stationStr.add(fi.getStationId() + "_"+ tw.getStationId());
			}
		}
	}
	/**
	 * @return the 第三次经过站点
	 */
	public List<Station> getThreeStation() {
		return threeStation;
	}
	/**
	 * @param 第三次经过站点 the threeStation to set
	 */
	public void setThreeStation(List<Station> three) {
		this.threeStation = three;
		if(this.firstStation != null && this.threeStation != null && this.firstStation.size() > 0 && this.threeStation.size() > 0){
			for(Station fi : this.firstStation){
				for(Station th : this.threeStation){
					stationStr.add(fi.getStationId() + "_"+ th.getStationId());
				}
			}
		}
		if(this.twoStation != null && this.threeStation != null && this.twoStation.size() > 0 && this.threeStation.size() > 0){
			for(Station tw : this.twoStation){
				for(Station th : this.threeStation){
					stationStr.add(tw.getStationId() + "_"+ th.getStationId());
				}
			}
		}
	}
	/**
	 * @return the 第四次经过站点
	 */
	public List<Station> getFourStation() {
		return fourStation;
	}
	/**
	 * @param 第四次经过站点 the fourStation to set
	 */
	public void setFourStation(List<Station> fourStation) {
		this.fourStation = fourStation;
		if(this.firstStation != null && this.fourStation != null && this.firstStation.size() > 0 && this.fourStation.size() > 0){
			for(Station fi : this.firstStation){
				for(Station fo : this.fourStation){
					stationStr.add(fi.getStationId() + "_"+ fo.getStationId());
				}
			}
		}
		if(this.twoStation != null && this.fourStation != null && this.twoStation.size() > 0 && this.fourStation.size() > 0){
			for(Station tw : this.twoStation){
				for(Station fo : this.fourStation){
					stationStr.add(tw.getStationId() + "_"+ fo.getStationId());
				}
			}
		}
		if(this.threeStation != null && this.fourStation != null && this.threeStation.size() > 0 && this.fourStation.size() > 0){
			for(Station th : this.threeStation){
				for(Station fo : this.fourStation){
					stationStr.add(th.getStationId() + "_"+ fo.getStationId());
				}
			}
		}
	}
	/**
	 * 缓存站点列表
	 * @param stationList
	 */
	public void saveStationList(List<Station> stationList) {
		if(this.firstStation == null){
			setFirstStation(stationList); 
		}else if(this.twoStation == null){
			setTwoStation(stationList);
		}else if(this.threeStation == null){
			setThreeStation(stationList);
		}else if(this.fourStation == null){
			setFourStation(stationList); 
		}
	}
	/**
	 * @return the 站点字符串列表
	 */
	public List<String> getStationStr() {
		return stationStr;
	}
	/**
	 * @param 站点字符串列表 the stationStr to set
	 */
	public void setStationStr(List<String> stationStr) {
		this.stationStr = stationStr;
	}
	/**
	 * 清空列表
	 */
	public void clearList() {
		this.stationStr.clear();
	}
	
}
