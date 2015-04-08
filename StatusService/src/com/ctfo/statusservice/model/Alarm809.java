package com.ctfo.statusservice.model;

/**
 * 809协议报警
 * 上报监管平台使用
 */
public class Alarm809{
	/**	报警信息来源 （1：车载终端，2：企业监控平台，3：政府监管平台，9：其它）	*/
	private int alarmSource = 1;
	/**	报警时间	*/
	private long alarmUtc;
	/**	报警编号 （报警类型）	*/
	private String alarmCode;
	/**	报警信息内容	*/
	private String alarmConnect; // 报警来源 :默认为终端上报
	/**	硬件识别码	*/
	private String macid;
	/**	报警附加信息	*/
	private String alarmAdd;
	
	/**
	 * <pre>
	 * 转换809协议报警信息
	 * 809报警协议报警编号生成规则
	 *   F       6789   86400
	 *  报警类型 手机尾号 UTC尾数
	 * @return
	 * </pre>
	 */
	public String toKeyValue(){
		if(macid == null || macid.length() == 0){
			return null;
		}
		StringBuffer sb = new StringBuffer();
		sb.append("CAITS 0 ").append(macid).append(" 0 L_PROV ");
		if(alarmCode.equals("20")){
			if(alarmAdd.matches(".*|.*|0|.*")){
				alarmCode = "4";
				alarmConnect = "进入指定区域报警";
			}else{
				alarmCode = "5";
				alarmConnect = "离开指定区域报警";
			}
		}
		String alarmId = getAlarmId(alarmCode, macid, alarmUtc);
		//		拼接报警信息内容  {TYPE:D_WARN,WARNINFO:报警信息来源|报警编号|报警UTC时间|报警信息编号|报警信息内容}
		sb.append("{TYPE:D_WARN,WARNINFO:"); // 报警类型，报警内容
		sb.append(alarmSource).append("|");	//报警信息来源
		sb.append(alarmCode).append("|");   //报警编号
		sb.append(alarmUtc).append("|");	//报警UTC时间
		sb.append(alarmId).append("|");		//报警信息编号
		sb.append(-1).append("} \r\n");//报警信息内容
		return sb.toString();
	}
	/**
	 * 生成报警编号
	 * @param alarmCode
	 * @param macid
	 * @param alarmUtc
	 * @return
	 */
	private String getAlarmId(String alarmCode2, String macid2, long utc) {
		StringBuilder alarmTempId = new StringBuilder();
		alarmTempId.append(alarmCode2);
		String utcStr = String.valueOf(utc);
		if(macid2.length() < 16){ 
			return utcStr.substring(0, 10);  // 如果硬件识别码错误就返回当前时间做ID
		}
		alarmTempId.append(macid2.substring(macid2.length() - 4)); // 取手机号最后4位
		alarmTempId.append(utcStr.substring(5)); // 取UTC时间秒数后5位
		String alarmId = alarmTempId.toString();
//		 消息ID 为4个字节，最大长度为10位
		if(alarmId.length() > 10){
			alarmId = alarmId.substring(0,10); 
		}
		return alarmId;
	}

	public int getAlarmSource() {
		return alarmSource;
	}
	public void setAlarmSource(int alarmSource) {
		this.alarmSource = alarmSource;
	}
	public long getAlarmUtc() {
		return alarmUtc;
	}
	public void setAlarmUtc(long alarmUtc) {
		this.alarmUtc = alarmUtc/1000; 
	}
	public String getAlarmCode() {
		return alarmCode;
	}
	public void setAlarmCode(String alarmCode) {
		String code = null;
		if(alarmCode == null || alarmCode.length() == 0){
			code = "F";
			alarmConnect = "其他报警";
		} else {
//			替换809协议中报警类型
			if(alarmCode.equals("0")){ 
				code = "3";
				alarmConnect = "紧急报警";
			} else if (alarmCode.equals("23")){
				code = "B";
				alarmConnect = "线路偏离报警";
			} else if (alarmCode.equals("28")){
				code = "C";
				alarmConnect = "非法移位报警";
			} else if (alarmCode.equals("18")){
				code = "D";
				alarmConnect = "累计驾驶超时报警";
			} else if (alarmCode.equals("20")){
				code = alarmCode;
			} else if (alarmCode.equals("1")){
				code = "1";
				alarmConnect = "超速报警";
			} else if (alarmCode.equals("2")){
				code = "2";
				alarmConnect = "疲劳驾驶报警";
			} else {
				code = "F";
				alarmConnect = "其他报警";
			} 
		}
		this.alarmCode = code;
	}
	public String getAlarmConnect() {
		return alarmConnect;
	}
	public void setAlarmConnect(String alarmConnect) {
		this.alarmConnect = alarmConnect;
	}
	public String getMacid() {
		return macid;
	}
	public void setMacid(String macid) {
		this.macid = macid;
	}
	public String getAlarmAdd() {
		return alarmAdd;
	}
	public void setAlarmAdd(String alarmAdd) {
		this.alarmAdd = alarmAdd;
	}
	
	public static void main(String[] args) {
		Alarm809 alarm809 = new Alarm809();
		alarm809.setAlarmCode("0");
		alarm809.setMacid("E001_18013010979");
		alarm809.setAlarmAdd("4|18435533|1");
		alarm809.setAlarmUtc(System.currentTimeMillis());
		System.out.println(alarm809.toKeyValue());
	}
}
