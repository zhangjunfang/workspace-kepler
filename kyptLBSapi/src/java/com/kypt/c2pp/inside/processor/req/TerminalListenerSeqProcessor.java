package com.kypt.c2pp.inside.processor.req;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.back.DownCommandSeqQueueMap;
import com.kypt.c2pp.back.DownCommandSummaryBean;
import com.kypt.c2pp.buffer.DownCommandBuffer;
import com.kypt.c2pp.inside.msg.req.TerminalListenerSeq;
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
public final class TerminalListenerSeqProcessor extends
		AbstractInsideProcessor<TerminalListenerSeq, SupCommService> {

	private Logger log = LoggerFactory
			.getLogger(TerminalListenerSeqProcessor.class);

	private static final TerminalListenerSeqProcessor PROCESSOR = new TerminalListenerSeqProcessor();

	private TerminalListenerSeqProcessor() {
	}

	public static TerminalListenerSeqProcessor getInstance() {
		return PROCESSOR;
	}

	public TerminalListenerSeq parse(byte[] buf) throws ParseException {
		try {
			TerminalListenerSeq resp = new TerminalListenerSeq();
			super.parseHeader(buf, resp);
			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse TerminalListenerSeq failed.", t);
		}
	}

	public void valid(TerminalListenerSeq msg) throws InvalidMessageException {
		super.validHeader(msg);
	}

	public void handle(TerminalListenerSeq msg, SupCommService supCommService)
			throws HandleException {
		try {
			log.info(LogFormatter.formatMsg("TerminalListenerSeq",
					"receive a TerminalListenerSeq request."));
			
			//向下行消息概要Map中加入本指令概要，等待消息回复。
			DownCommandSummaryBean dcsb = new DownCommandSummaryBean();
			dcsb.setCommand(msg.getCommand());
			dcsb.setSeqId(msg.getSeq());
			dcsb.setAddress(supCommService.getRemoteAddress());
			String key = supCommService.getRemoteAddress()+msg.getSeq();
			DownCommandSeqQueueMap.getInstance().put(key, dcsb);
			
		} catch (Throwable t) {
			throw new HandleException(
					"handle TerminalListenerSeq request failed.", t);
		}
	}

	public TerminalListenerSeq parse(String msg) throws ParseException {
		// TODO Auto-generated method stub
		try {
			TerminalListenerSeq resp = new TerminalListenerSeq();

			String message[] = msg.split("\\s+");

			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse TerminalListenerSeq failed.", t);
		}
	}

	public TerminalListenerSeq parse(String[] msg) throws ParseException {
		// TODO Auto-generated method stub
		TerminalListenerSeq resp = new TerminalListenerSeq();
		resp.setBody(msg);
		// 把设置终端参数命令对象放入下行命令队列中
		DownCommandBuffer.getInstance().add(resp);
		return resp;
	}
}
