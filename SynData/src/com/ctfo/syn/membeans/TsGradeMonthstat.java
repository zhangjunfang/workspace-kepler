package com.ctfo.syn.membeans;

import java.io.Serializable;

@SuppressWarnings("serial")
public class TsGradeMonthstat implements Serializable {

	private Long seqId;

	private Long statYear;

	private Long statMonth;

	private String yearMonth;

	private Long vid;

	private String cVin;

	private String vehicleNo;

	private String vbrandCode;

	private String prodCode;

	private String prodName;

	private String emodelCode;

	private String vlineId;

	private String vlineName;

	private Long corpId;

	private String corpName;

	private Long teamId;

	private String teamName;

	private Long travelMileage;

	private Long overspeedSum;

	private Long overspeedTime;

	private Long oilOverspeedScore;

	private Long safeOverspeedScore;

	private Long overrpmSum;

	private Long overrpmTime;

	private Long oilOverrpmScore;

	private Long gearGlideSum;

	private Long gearGlideTime;

	private Long oilGearGlideScore;

	private Long safeGearGlideScore;

	private Long longIdleSum;

	private Long longIdleTime;

	private Long oilLongIdleScore;

	private Long fatigueSum;

	private Long fatigueTime;

	private Long safeFatigueScore;

	private Long economicRunSum;

	private Long economicRunTime;

	private Long oilEconomicRunScore;

	private Long urgentSum;

	private Long oilUrgentScore;

	private Long safeUrgentScore;

	private Long airConditionSum;

	private Long airConditionTime;

	private Long oilAirConditionScore;

	private Long engineRotateTime;

	private Long oilScoreSum;

	private Long safeScoreSum;

	private Long factOilwear;

	private Long checkOilwear;

	private Long saveoilSum;

	private Long saveoilRatio;

	private Long oilwearScore;

	private Long allScoreSum;

	public Long getStatYear() {
		return statYear;
	}

	public void setStatYear(Long statYear) {
		this.statYear = statYear;
	}

	public Long getStatMonth() {
		return statMonth;
	}

	public void setStatMonth(Long statMonth) {
		this.statMonth = statMonth;
	}

	public Long getVid() {
		return vid;
	}

	public void setVid(Long vid) {
		this.vid = vid;
	}

	public String getcVin() {
		return cVin;
	}

	public void setcVin(String cVin) {
		this.cVin = cVin == null ? null : cVin.trim();
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo == null ? null : vehicleNo.trim();
	}

	public String getVbrandCode() {
		return vbrandCode;
	}

	public void setVbrandCode(String vbrandCode) {
		this.vbrandCode = vbrandCode == null ? null : vbrandCode.trim();
	}

	public String getProdCode() {
		return prodCode;
	}

	public void setProdCode(String prodCode) {
		this.prodCode = prodCode == null ? null : prodCode.trim();
	}

	public String getEmodelCode() {
		return emodelCode;
	}

	public void setEmodelCode(String emodelCode) {
		this.emodelCode = emodelCode == null ? null : emodelCode.trim();
	}

	public Long getCorpId() {
		return corpId;
	}

	public void setCorpId(Long corpId) {
		this.corpId = corpId;
	}

	public String getCorpName() {
		return corpName;
	}

	public void setCorpName(String corpName) {
		this.corpName = corpName == null ? null : corpName.trim();
	}

	public Long getTeamId() {
		return teamId;
	}

	public void setTeamId(Long teamId) {
		this.teamId = teamId;
	}

	public String getTeamName() {
		return teamName;
	}

	public void setTeamName(String teamName) {
		this.teamName = teamName == null ? null : teamName.trim();
	}

	public Long getTravelMileage() {
		return travelMileage;
	}

	public void setTravelMileage(Long travelMileage) {
		this.travelMileage = travelMileage;
	}

	public Long getOverspeedSum() {
		return overspeedSum;
	}

	public void setOverspeedSum(Long overspeedSum) {
		this.overspeedSum = overspeedSum;
	}

	public Long getOverspeedTime() {
		return overspeedTime;
	}

	public void setOverspeedTime(Long overspeedTime) {
		this.overspeedTime = overspeedTime;
	}

	public Long getOilOverspeedScore() {
		return oilOverspeedScore;
	}

	public void setOilOverspeedScore(Long oilOverspeedScore) {
		this.oilOverspeedScore = oilOverspeedScore;
	}

	public Long getSafeOverspeedScore() {
		return safeOverspeedScore;
	}

	public void setSafeOverspeedScore(Long safeOverspeedScore) {
		this.safeOverspeedScore = safeOverspeedScore;
	}

	public Long getOverrpmSum() {
		return overrpmSum;
	}

	public void setOverrpmSum(Long overrpmSum) {
		this.overrpmSum = overrpmSum;
	}

	public Long getOverrpmTime() {
		return overrpmTime;
	}

	public void setOverrpmTime(Long overrpmTime) {
		this.overrpmTime = overrpmTime;
	}

	public Long getOilOverrpmScore() {
		return oilOverrpmScore;
	}

	public void setOilOverrpmScore(Long oilOverrpmScore) {
		this.oilOverrpmScore = oilOverrpmScore;
	}

	public Long getGearGlideSum() {
		return gearGlideSum;
	}

	public void setGearGlideSum(Long gearGlideSum) {
		this.gearGlideSum = gearGlideSum;
	}

	public Long getGearGlideTime() {
		return gearGlideTime;
	}

	public void setGearGlideTime(Long gearGlideTime) {
		this.gearGlideTime = gearGlideTime;
	}

	public Long getOilGearGlideScore() {
		return oilGearGlideScore;
	}

	public void setOilGearGlideScore(Long oilGearGlideScore) {
		this.oilGearGlideScore = oilGearGlideScore;
	}

	public Long getSafeGearGlideScore() {
		return safeGearGlideScore;
	}

	public void setSafeGearGlideScore(Long safeGearGlideScore) {
		this.safeGearGlideScore = safeGearGlideScore;
	}

	public Long getLongIdleSum() {
		return longIdleSum;
	}

	public void setLongIdleSum(Long longIdleSum) {
		this.longIdleSum = longIdleSum;
	}

	public Long getLongIdleTime() {
		return longIdleTime;
	}

	public void setLongIdleTime(Long longIdleTime) {
		this.longIdleTime = longIdleTime;
	}

	public Long getOilLongIdleScore() {
		return oilLongIdleScore;
	}

	public void setOilLongIdleScore(Long oilLongIdleScore) {
		this.oilLongIdleScore = oilLongIdleScore;
	}

	public Long getFatigueSum() {
		return fatigueSum;
	}

	public void setFatigueSum(Long fatigueSum) {
		this.fatigueSum = fatigueSum;
	}

	public Long getFatigueTime() {
		return fatigueTime;
	}

	public void setFatigueTime(Long fatigueTime) {
		this.fatigueTime = fatigueTime;
	}

	public Long getSafeFatigueScore() {
		return safeFatigueScore;
	}

	public void setSafeFatigueScore(Long safeFatigueScore) {
		this.safeFatigueScore = safeFatigueScore;
	}

	public Long getEconomicRunSum() {
		return economicRunSum;
	}

	public void setEconomicRunSum(Long economicRunSum) {
		this.economicRunSum = economicRunSum;
	}

	public Long getEconomicRunTime() {
		return economicRunTime;
	}

	public void setEconomicRunTime(Long economicRunTime) {
		this.economicRunTime = economicRunTime;
	}

	public Long getOilEconomicRunScore() {
		return oilEconomicRunScore;
	}

	public void setOilEconomicRunScore(Long oilEconomicRunScore) {
		this.oilEconomicRunScore = oilEconomicRunScore;
	}

	public Long getUrgentSum() {
		return urgentSum;
	}

	public void setUrgentSum(Long urgentSum) {
		this.urgentSum = urgentSum;
	}

	public Long getOilUrgentScore() {
		return oilUrgentScore;
	}

	public void setOilUrgentScore(Long oilUrgentScore) {
		this.oilUrgentScore = oilUrgentScore;
	}

	public Long getSafeUrgentScore() {
		return safeUrgentScore;
	}

	public void setSafeUrgentScore(Long safeUrgentScore) {
		this.safeUrgentScore = safeUrgentScore;
	}

	public Long getAirConditionSum() {
		return airConditionSum;
	}

	public void setAirConditionSum(Long airConditionSum) {
		this.airConditionSum = airConditionSum;
	}

	public Long getAirConditionTime() {
		return airConditionTime;
	}

	public void setAirConditionTime(Long airConditionTime) {
		this.airConditionTime = airConditionTime;
	}

	public Long getOilAirConditionScore() {
		return oilAirConditionScore;
	}

	public void setOilAirConditionScore(Long oilAirConditionScore) {
		this.oilAirConditionScore = oilAirConditionScore;
	}

	public Long getEngineRotateTime() {
		return engineRotateTime;
	}

	public void setEngineRotateTime(Long engineRotateTime) {
		this.engineRotateTime = engineRotateTime;
	}

	public Long getOilScoreSum() {
		return oilScoreSum;
	}

	public void setOilScoreSum(Long oilScoreSum) {
		this.oilScoreSum = oilScoreSum;
	}

	public Long getSafeScoreSum() {
		return safeScoreSum;
	}

	public void setSafeScoreSum(Long safeScoreSum) {
		this.safeScoreSum = safeScoreSum;
	}

	public Long getFactOilwear() {
		return factOilwear;
	}

	public void setFactOilwear(Long factOilwear) {
		this.factOilwear = factOilwear;
	}

	public Long getCheckOilwear() {
		return checkOilwear;
	}

	public void setCheckOilwear(Long checkOilwear) {
		this.checkOilwear = checkOilwear;
	}

	public Long getSaveoilSum() {
		return saveoilSum;
	}

	public void setSaveoilSum(Long saveoilSum) {
		this.saveoilSum = saveoilSum;
	}

	public Long getSaveoilRatio() {
		return saveoilRatio;
	}

	public void setSaveoilRatio(Long saveoilRatio) {
		this.saveoilRatio = saveoilRatio;
	}

	public Long getOilwearScore() {
		return oilwearScore;
	}

	public void setOilwearScore(Long oilwearScore) {
		this.oilwearScore = oilwearScore;
	}

	public Long getAllScoreSum() {
		return allScoreSum;
	}

	public void setAllScoreSum(Long allScoreSum) {
		this.allScoreSum = allScoreSum;
	}

	public Long getSeqId() {
		return seqId;
	}

	public void setSeqId(Long seqId) {
		this.seqId = seqId;
	}

	public String getVlineId() {
		return vlineId;
	}

	public void setVlineId(String vlineId) {
		this.vlineId = vlineId;
	}

	public String getVlineName() {
		return vlineName;
	}

	public void setVlineName(String vlineName) {
		this.vlineName = vlineName;
	}

	public String getProdName() {
		return prodName;
	}

	public void setProdName(String prodName) {
		this.prodName = prodName;
	}

	public String getYearMonth() {
		return yearMonth;
	}

	public void setYearMonth(String yearMonth) {
		this.yearMonth = yearMonth;
	}

}
