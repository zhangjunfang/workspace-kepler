package com.kypt.c2pp.inside.vo;

import com.kypt.c2pp.util.Converser;

public class FlagVO {

	/**
	 * 紧急 1紧急 0不紧急
	 */
	private boolean isInstancy = false;

	/**
	 * 终端显示器显示 1显示 0不显示
	 */
	private boolean isCrtDisplay = false;

	/**
	 * 终端TTS播读 1播读 0不播读
	 */
	private boolean isTts = false;

	/**
	 * 终端广告屏显示 1播读 0不播读
	 */
	private boolean isScreenDisplay = false;

	public boolean isInstancy() {
		return isInstancy;
	}

	public void setInstancy(boolean isInstancy) {
		this.isInstancy = isInstancy;
	}

	public boolean isCrtDisplay() {
		return isCrtDisplay;
	}

	public void setCrtDisplay(boolean isCrtDisplay) {
		this.isCrtDisplay = isCrtDisplay;
	}

	public boolean isTts() {
		return isTts;
	}

	public void setTts(boolean isTts) {
		this.isTts = isTts;
	}

	public boolean isScreenDisplay() {
		return isScreenDisplay;
	}

	public void setScreenDisplay(boolean isScreenDisplay) {
		this.isScreenDisplay = isScreenDisplay;
	}
	
	public void SetBody(String num){
		String str=Converser.int2Binary(num);
		char[] b = str.toCharArray();
		
		if (b[7]=='1'){
			this.isInstancy=true;
		}
		if (b[5]=='1'){
			this.isCrtDisplay=true;
		}
		
		if (b[4]=='1'){
			this.isTts=true;
		}
		
		if (b[3]=='1'){
			this.isScreenDisplay=true;
		}
		
	}
	
	
	public String toString(){
		StringBuffer sb=new StringBuffer();
		sb.append("000");
		if (this.isScreenDisplay){
			sb.append("1");
		}else{
			sb.append("0");
		}
		if (this.isTts){
			sb.append("1");
		}else{
			sb.append("0");
		}

		if (this.isCrtDisplay){
			sb.append("1");
		}else{
			sb.append("0");
		}
		sb.append("0");
		if (this.isInstancy){
			sb.append("1");
		}else{
			sb.append("0");
		}

		return sb.toString();
	}
	
	public String questionFlag(){
		StringBuffer sb=new StringBuffer();
		sb.append("000");
		if (this.isScreenDisplay){
			sb.append("1");
		}else{
			sb.append("0");
		}
		
		if (this.isTts){
			sb.append("1");
		}else{
			sb.append("0");
		}
		
		if (this.isCrtDisplay){
			sb.append("1");
		}else{
			sb.append("0");
		}
		sb.append("0");
		if (this.isInstancy){
			sb.append("1");
		}else{
			sb.append("0");
		}
		
		return sb.toString();
	}

}
