package com.kypt.c2pp.inside.processor.req;

import java.io.UnsupportedEncodingException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.back.DownCommandSeqQueueMap;
import com.kypt.c2pp.back.DownCommandSummaryBean;
import com.kypt.c2pp.buffer.DownCommandBuffer;
import com.kypt.c2pp.inside.msg.req.SetTerminalEventReq;
import com.kypt.c2pp.inside.processor.AbstractInsideProcessor;
import com.kypt.c2pp.nio.SupCommService;
import com.kypt.exception.HandleException;
import com.kypt.exception.InvalidMessageException;
import com.kypt.exception.ParseException;
import com.kypt.log.LogFormatter;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-11-24 下午03:30:48
 */
public final class SetTerminalEventReqProcessor extends
		AbstractInsideProcessor<SetTerminalEventReq, SupCommService> {

	private Logger log = LoggerFactory
			.getLogger(SetTerminalEventReqProcessor.class);

	private static final SetTerminalEventReqProcessor PROCESSOR = new SetTerminalEventReqProcessor();

	private SetTerminalEventReqProcessor() {
	}

	public static SetTerminalEventReqProcessor getInstance() {
		return PROCESSOR;
	}

	public SetTerminalEventReq parse(byte[] buf) throws ParseException {
		try {
			SetTerminalEventReq resp = new SetTerminalEventReq();
			super.parseHeader(buf, resp);
			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse SetTerminalEventReq failed.", t);
		}
	}

	public void valid(SetTerminalEventReq msg) throws InvalidMessageException {
		super.validHeader(msg);
	}

	public void handle(SetTerminalEventReq msg, SupCommService supCommService)
			throws HandleException {
		try {
			log.info(LogFormatter.formatMsg("SetTerminalEventReq",
					"receive a SetTerminalEventReq request."));
			
			//向下行消息概要Map中加入本指令概要，等待消息回复。
			DownCommandSummaryBean dcsb = new DownCommandSummaryBean();
			dcsb.setCommand(msg.getCommand());
			dcsb.setSeqId(msg.getSeq());
			dcsb.setAddress(supCommService.getRemoteAddress());
			String key = supCommService.getRemoteAddress()+msg.getSeq();
			DownCommandSeqQueueMap.getInstance().put(key, dcsb);
			
		} catch (Throwable t) {
			throw new HandleException(
					"handle SetTerminalEventReq request failed.", t);
		}
	}

	public SetTerminalEventReq parse(String msg) throws ParseException {
		// TODO Auto-generated method stub
		try {
			SetTerminalEventReq resp = new SetTerminalEventReq();

			String message[] = msg.split("\\s+");

			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse SetTerminalEventReq failed.", t);
		}
	}

	public SetTerminalEventReq parse(String[] msg) throws ParseException {
		// TODO Auto-generated method stub
		SetTerminalEventReq resp = new SetTerminalEventReq();
		try {
			resp.setBody(msg);
			// 把设置终端参数命令对象放入下行命令队列中
			DownCommandBuffer.getInstance().add(resp);
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			throw new ParseException(
					"UnsupportedEncodingException,parse SetTerminalEventReq failed.",
					e);
		}
		return resp;

	}
}
