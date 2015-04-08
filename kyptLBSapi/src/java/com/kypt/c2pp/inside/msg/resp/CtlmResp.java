package com.kypt.c2pp.inside.msg.resp;

	import java.io.UnsupportedEncodingException;

	import com.kypt.c2pp.inside.msg.InsideMsg;
	import com.kypt.c2pp.inside.msg.utils.InsideMsgUtils;
	import com.kypt.c2pp.util.ValidationUtil.GENERAL_STATUS;
	import com.kypt.configuration.C2ppCfg;

	/**
	 * 终端控制指令通用应答
	 */
	public class CtlmResp extends InsideMsg {

		public static final String COMMAND = "ctlm_resp";

		private String seq;

		private String oemId;
		
		private String deviceNo;

		private String commType;

		private String status;

		public String getSeq() {
			return seq;
		}

		public void setSeq(String seq) {
			this.seq = seq;
		}

		public String getCommType() {
			return commType;
		}

		public void setCommType(String commType) {
			this.commType = commType;
		}

		public String getStatus() {
			return status;
		}

		public void setStatus(GENERAL_STATUS setStatus) {
			switch (setStatus) {
			case success:
				this.status = "0";
				break;
			case failure:
				this.status = "1";
				break;
			case sendfailure:
				this.status = "2";
				break;
			case nonsupport:
				this.status = "3";
				break;
			case notonline:
				this.status = "4";
				break;
			case timeout:
				this.status = "5";
				break;
			}
		}

		@Override
		public byte[] getBytes() throws UnsupportedEncodingException {

			String req = this.toString();
			if (C2ppCfg.props.getProperty("superviseEncoding") != null &&C2ppCfg.props.getProperty("superviseEncoding").length() > 0) {
				return req.getBytes(C2ppCfg.props.getProperty("superviseEncoding"));
			} else {
				return req.getBytes();
			}
		}

		public String toString() {
			return "CAITR " + this.seq + " " + C2ppCfg.props.getProperty("superviseOemId")+"_"+this.deviceNo
					+ " 0 " + "D_CTLM {RET:" + this.status
					+ "}\r\n";

		}

		public String getOemId() {
			return oemId;
		}

		public void setOemId(String oemId) {
			this.oemId = oemId;
		}

		public String getDeviceNo() {
			return deviceNo;
		}

		public void setDeviceNo(String deviceNo) {
			this.deviceNo = deviceNo;
		}

	}

