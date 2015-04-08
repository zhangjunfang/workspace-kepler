package com.ctfo.annouce.beans;
/**
 * 
 * @author 于博洋
 *
 * 2014-11-7
 * 公告附件bean
 */
public class TbAttachment {
	
		public TbAttachment(){} 
		
		private String attachId;//公告管理附件表id
		private String annoucId;//公告管理id，关联公告管理表
		private String attachName;//附件名称
		private String attachAliasName;//附件上传处理后名称
		private String attachCategory;//附件类别，关联字典码表：图片 ，文档等
		private String filePath;//上传文件保存路径
		private String remark;//备注
		
		public String getAttachId() {
			return attachId;
		}
		public void setAttachId(String attachId) {
			this.attachId = attachId;
		}
		public String getAnnoucId() {
			return annoucId;
		}
		public void setAnnoucId(String annoucId) {
			this.annoucId = annoucId;
		}
		public String getAttachName() {
			return attachName;
		}
		public void setAttachName(String attachName) {
			this.attachName = attachName;
		}
		public String getAttachAliasName() {
			return attachAliasName;
		}
		public void setAttachAliasName(String attachAliasName) {
			this.attachAliasName = attachAliasName;
		}
		public String getAttachCategory() {
			return attachCategory;
		}
		public void setAttachCategory(String attachCategory) {
			this.attachCategory = attachCategory;
		}
		public String getFilePath() {
			return filePath;
		}
		public void setFilePath(String filePath) {
			this.filePath = filePath;
		}
		public String getRemark() {
			return remark;
		}
		public void setRemark(String remark) {
			this.remark = remark;
		}
		
		
		
}
