package com.ctfo.storage.init.model;



import java.io.Serializable;

@SuppressWarnings("serial")
public class TbProductType extends BaseModel implements Serializable{

	/**
	 * 编码序号
	 */
	private Long codeInx = -1l;
	/**
	 * 有效标记
	 */
	private String enableFlag = "";
	/**
	 * 车型编码
	 */
	private String prodCode = "";
	/**
	 * 车型描述
	 */
	private String prodDesc = "";
	/**
	 * 车型名称
	 */
	private String prodName = "";
	/**
	 * 所属品牌
	 */
	private String vbrandCode = "";
	/**
	 * 获取编码序号的值
	 * @return codeInx  
	 */
	public Long getCodeInx() {
		return codeInx;
	}
	/**
	 * 设置编码序号的值
	 * @param codeInx
	 */
	public void setCodeInx(Long codeInx) {
		this.codeInx = codeInx;
	}
	/**
	 * 获取有效标记的值
	 * @return enableFlag  
	 */
	public String getEnableFlag() {
		return enableFlag;
	}
	/**
	 * 设置有效标记的值
	 * @param enableFlag
	 */
	public void setEnableFlag(String enableFlag) {
		this.enableFlag = enableFlag;
	}
	/**
	 * 获取车型编码的值
	 * @return prodCode  
	 */
	public String getProdCode() {
		return prodCode;
	}
	/**
	 * 设置车型编码的值
	 * @param prodCode
	 */
	public void setProdCode(String prodCode) {
		this.prodCode = prodCode;
	}
	/**
	 * 获取车型描述的值
	 * @return prodDesc  
	 */
	public String getProdDesc() {
		return prodDesc;
	}
	/**
	 * 设置车型描述的值
	 * @param prodDesc
	 */
	public void setProdDesc(String prodDesc) {
		this.prodDesc = prodDesc;
	}
	/**
	 * 获取车型名称的值
	 * @return prodName  
	 */
	public String getProdName() {
		return prodName;
	}
	/**
	 * 设置车型名称的值
	 * @param prodName
	 */
	public void setProdName(String prodName) {
		this.prodName = prodName;
	}
	/**
	 * 获取所属品牌的值
	 * @return vbrandCode  
	 */
	public String getVbrandCode() {
		return vbrandCode;
	}
	/**
	 * 设置所属品牌的值
	 * @param vbrandCode
	 */
	public void setVbrandCode(String vbrandCode) {
		this.vbrandCode = vbrandCode;
	}
	
	
	

}
