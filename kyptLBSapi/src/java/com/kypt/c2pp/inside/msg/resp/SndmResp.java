package com.kypt.c2pp.inside.msg.resp;

	import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsgResp;
import com.kypt.c2pp.util.ValidationUtil.GENERAL_STATUS;
import com.kypt.configuration.C2ppCfg;

	/**
	 * 信息下发类指令通用应答
	 */
	public class SndmResp extends InsideMsgResp {

		public static final String COMMAND = "sndm_resp";

		private String status;
		
		public SndmResp(){
			super.setCommand(COMMAND);
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
			if (this.getEncoding() != null &&this.getEncoding().length() > 0) {
				return req.getBytes(this.getEncoding());
			} else {
				return req.getBytes();
			}
		}

		public String toString() {
			return "CAITR " + this.getSeq() + " " + this.getOemId()+"_"+this.getDeviceNo()
					+ " 0 " + "D_SNDM {RET:" + this.status
					+ "}\r\n";

		}
	}

