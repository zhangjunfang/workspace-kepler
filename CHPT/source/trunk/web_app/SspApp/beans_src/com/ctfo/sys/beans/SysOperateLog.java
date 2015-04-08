package com.ctfo.sys.beans;

public class SysOperateLog {
	
		/** */
		private String oplId;//操作记录ID

		/** */
		private String opType;//操作类型

		/** */
		private String funId;//功能ID
		
		/** */
		private String funName;//功能名称

		/** */
		private String content;//操作内容

		/** */
		private String valBefore;//之前的值
		
		/** */
		private String valAfter;//之后的值

		/** */
		private String opName;//用户姓名

		/** */
		private String opTime;//操作时间

		/** */
		private String opRole;//用户角色

		/** */
		private String entName;//用户所属部门

		/** */
		private String comName;//用户所属公司

		public String getOplId() {
			return oplId;
		}

		public void setOplId(String oplId) {
			this.oplId = oplId;
		}

		public String getOpType() {
			return opType;
		}

		public void setOpType(String opType) {
			this.opType = opType;
		}

		public String getFunId() {
			return funId;
		}

		public void setFunId(String funId) {
			this.funId = funId;
		}

		public String getFunName() {
			return funName;
		}

		public void setFunName(String funName) {
			this.funName = funName;
		}

		public String getContent() {
			return content;
		}

		public void setContent(String content) {
			this.content = content;
		}

		public String getValBefore() {
			return valBefore;
		}

		public void setValBefore(String valBefore) {
			this.valBefore = valBefore;
		}

		public String getValAfter() {
			return valAfter;
		}

		public void setValAfter(String valAfter) {
			this.valAfter = valAfter;
		}

		public String getOpName() {
			return opName;
		}

		public void setOpName(String opName) {
			this.opName = opName;
		}

		public String getOpTime() {
			return opTime;
		}

		public void setOpTime(String opTime) {
			this.opTime = opTime;
		}

		public String getOpRole() {
			return opRole;
		}

		public void setOpRole(String opRole) {
			this.opRole = opRole;
		}

		public String getEntName() {
			return entName;
		}

		public void setEntName(String entName) {
			this.entName = entName;
		}

		public String getComName() {
			return comName;
		}

		public void setComName(String comName) {
			this.comName = comName;
		}
}
