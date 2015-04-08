package com.kypt.c2pp.inside.processor.req;

import java.io.UnsupportedEncodingException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.back.DownCommandSeqQueueMap;
import com.kypt.c2pp.back.DownCommandSummaryBean;
import com.kypt.c2pp.buffer.DownCommandBuffer;
import com.kypt.c2pp.inside.msg.req.DownTextReq;
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
public final class DownTextReqProcessor extends
		AbstractInsideProcessor<DownTextReq, SupCommService> {

	private Logger log = LoggerFactory.getLogger(DownTextReqProcessor.class);

	private static final DownTextReqProcessor PROCESSOR = new DownTextReqProcessor();

	private DownTextReqProcessor() {
	}

	public static DownTextReqProcessor getInstance() {
		return PROCESSOR;
	}

	public DownTextReq parse(byte[] buf) throws ParseException {
		try {
			DownTextReq resp = new DownTextReq();
			super.parseHeader(buf, resp);
			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse DownTextReq failed.", t);
		}
	}

	public void valid(DownTextReq msg) throws InvalidMessageException {
		super.validHeader(msg);
	}

	public void handle(DownTextReq msg, SupCommService supCommService)
			throws HandleException {
		try {
			log.info(LogFormatter.formatMsg("DownTextReq",
					"receive a DownTextReq request."));
			
			//向下行消息概要Map中加入本指令概要，等待消息回复。
			DownCommandSummaryBean dcsb = new DownCommandSummaryBean();
			dcsb.setCommand(msg.getCommand());
			dcsb.setSeqId(msg.getSeq());
			dcsb.setAddress(supCommService.getRemoteAddress());
			String key = supCommService.getRemoteAddress()+msg.getSeq();
			DownCommandSeqQueueMap.getInstance().put(key, dcsb);
			
		} catch (Throwable t) {
			throw new HandleException("handle DownTextReq request failed.", t);
		}
	}

	public DownTextReq parse(String msg) throws ParseException {
		// TODO Auto-generated method stub
		try {
			DownTextReq resp = new DownTextReq();

			String message[] = msg.split("\\s+");

			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse DownQuestionReq failed.", t);
		}
	}

	public DownTextReq parse(String[] msg) throws ParseException {
		// TODO Auto-generated method stub
		DownTextReq resp = new DownTextReq();
		try {
			resp.setBody(msg);
			// 把设置终端参数命令对象放入下行命令队列中
			DownCommandBuffer.getInstance().add(resp);
			log.info("把设置终端参数命令对象放入下行命令队列中");
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			throw new ParseException("parse DownQuestionReq failed.", e);
		}

		return resp;

	}
}
