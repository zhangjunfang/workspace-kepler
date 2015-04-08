package com.kypt.c2pp.inside.msg.req;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;

import com.kypt.c2pp.inside.msg.InsideMsg;
import com.kypt.c2pp.inside.vo.TerminalEventVO;
import com.kypt.c2pp.util.Base64;
import com.kypt.c2pp.util.ValidationUtil;

/**
 * 监管平台设置终端参数指令
 */
public class SetTerminalEventReq extends InsideMsg {

	public static final String COMMAND = "0x8301";

	private String seq;

	private String oemId;
	
	private String deviceNo;

	private String commType;

	private String paramType;

	/**
	 * 事件操作类型 0 删除终端现有所有事件 1 更新事件 2 追加事件 3 修改事件 4 删除特定几项事件
	 */
	private String optType;

	private int retry = 0;

	private TerminalEventVO[] tEventVO;
	
	public SetTerminalEventReq(){
		super.setCommand(COMMAND);
	}

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

	public int getRetry() {
		return retry;
	}

	public void setRetry(int retry) {
		this.retry = retry;
	}

	public String toString() {
		return "";

	}

	public String getParamType() {
		return paramType;
	}

	public void setParamType(String paramType) {
		this.paramType = paramType;
	}

	public TerminalEventVO[] gettEventVO() {
		return tEventVO;
	}

	public void settEventVO(TerminalEventVO[] tEventVO) {
		this.tEventVO = tEventVO;
	}

	public String getOptType() {
		return optType;
	}

	public void setOptType(String optType) {
		this.optType = optType;
	}

	public void setBody(String[] msg) throws UnsupportedEncodingException {
		this.seq = msg[1];
		String macId[] = msg[2].split("_");
		this.oemId = macId[0];
		this.deviceNo = macId[1];
		this.commType = msg[3];

		String msgTemp[] = msg[5].substring(1, msg[5].length() - 1).split(",");

		for (int i = 0; i < msgTemp.length; i++) {
			String msgKV[] = msgTemp[i].split(":");

			if (msgKV[0].equals("TYPE")) {
				this.paramType = msgKV[1];
				continue;
			}

			if (msgKV[0].equals("160")) {
				this.optType = msgKV[1];
				continue;
			}

			if (msgKV[0].equals("161")) {
				String eventStr = msgKV[1];

				HashMap hm = ValidationUtil.splitString_(msgKV[1]);
				tEventVO = new TerminalEventVO[hm.size()];

				Object key[] = hm.keySet().toArray();

				for (int x = 0; x < key.length; x++) {
					TerminalEventVO vo = new TerminalEventVO();
					vo.setEventId((String) key[x]);
					vo.setContent(Base64.decode((String) hm.get(key[x])));
					tEventVO[x] = vo;
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
