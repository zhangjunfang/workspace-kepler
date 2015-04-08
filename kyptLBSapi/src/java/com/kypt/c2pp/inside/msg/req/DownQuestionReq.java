package com.kypt.c2pp.inside.msg.req;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;

import com.kypt.c2pp.inside.msg.InsideMsg;
import com.kypt.c2pp.inside.vo.AnswerVO;
import com.kypt.c2pp.inside.vo.FlagVO;
import com.kypt.c2pp.util.ValidationUtil;
import com.kypt.c2pp.util.Base64;

/**
 * 监管平台提问下发指令
 */
public class DownQuestionReq extends InsideMsg {

	public static final String COMMAND = "0x8302";

	//private String seq;

	private String oemId;
	
	private String deviceNo;

	private String commType;

	private FlagVO flagVO = new FlagVO();

	private String questionTitle;

	private AnswerVO[] answerVO;
	
	public DownQuestionReq(){
		super.setCommand(COMMAND);
	}

/*	public String getSeq() {
		return seq;
	}

	public void setSeq(String seq) {
		this.seq = seq;
	}*/

	public String getCommType() {
		return commType;
	}

	public void setCommType(String commType) {
		this.commType = commType;
	}

	public void setBody(String[] msg) throws UnsupportedEncodingException {
		//this.seq = msg[1];
		String macId[] = msg[2].split("_");
		this.oemId=macId[0];
		this.deviceNo=macId[1];
		this.commType = msg[3];

		String msgTemp[] = msg[5].substring(1, msg[5].length() - 1).split(",");

		for (int i = 0; i < msgTemp.length; i++) {
			String msgKV[] = msgTemp[i].split(":");
			if (msgKV[0].equals("16")) {
				this.flagVO.SetBody(msgKV[1]);
				continue;
			}
			if (msgKV[0].equals("17")) {
				this.questionTitle = msgKV[1];
				continue;
			}

			if (msgKV[0].equals("18")) {
				String eventStr = msgKV[1];

				HashMap hm = ValidationUtil.splitString_(msgKV[1]);

				answerVO = new AnswerVO[hm.size()];

				Object key[] = hm.keySet().toArray();

				for (int x = 0; x < key.length; x++) {
					AnswerVO vo = new AnswerVO();
					vo.setAnswerId((String) key[x]);

					vo.setContent(Base64.decode((String) hm.get(key[x])));
					answerVO[i] = vo;
				}

				continue;
			}
		}
	}

	@Override
	public byte[] getBytes() throws UnsupportedEncodingException {

		String req = this.toString();
		if (this.getEncoding() != null && this.getEncoding().length() > 0) {
			return req.getBytes(this.getEncoding());
		} else {
			return req.getBytes();
		}

	}

	public String toString() {
		return "";

	}

	public FlagVO getFlagVO() {
		return flagVO;
	}

	public void setFlagVO(FlagVO flagVO) {
		this.flagVO = flagVO;
	}

	public String getQuestionTitle() {
		return questionTitle;
	}

	public void setQuestionTitle(String questionTitle) {
		this.questionTitle = questionTitle;
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
