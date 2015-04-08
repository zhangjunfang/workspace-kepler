package com.kypt.c2pp.inside.processor.req;

import java.io.UnsupportedEncodingException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.back.DownCommandSeqQueueMap;
import com.kypt.c2pp.back.DownCommandSummaryBean;
import com.kypt.c2pp.buffer.DownCommandBuffer;
import com.kypt.c2pp.inside.msg.req.DownQuestionReq;
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
public final class DownQuestionReqProcessor extends
		AbstractInsideProcessor<DownQuestionReq, SupCommService> {

	private Logger log = LoggerFactory
			.getLogger(DownQuestionReqProcessor.class);

	private static final DownQuestionReqProcessor PROCESSOR = new DownQuestionReqProcessor();

	private DownQuestionReqProcessor() {
	}

	public static DownQuestionReqProcessor getInstance() {
		return PROCESSOR;
	}

	public DownQuestionReq parse(byte[] buf) throws ParseException {
		try {
			DownQuestionReq resp = new DownQuestionReq();
			super.parseHeader(buf, resp);
			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse DownQuestionReq failed.", t);
		}
	}

	public void valid(DownQuestionReq msg) throws InvalidMessageException {
		super.validHeader(msg);
	}

	public void handle(DownQuestionReq msg, SupCommService supCommService)
			throws HandleException {
		try {
			log.info(LogFormatter.formatMsg("DownQuestionReq",
					"receive a DownQuestionReq request."));
			
			//向下行消息概要Map中加入本指令概要，等待消息回复。
			DownCommandSummaryBean dcsb = new DownCommandSummaryBean();
			dcsb.setCommand(msg.getCommand());
			dcsb.setSeqId(msg.getSeq());
			dcsb.setAddress(supCommService.getRemoteAddress());
			String key = supCommService.getRemoteAddress()+msg.getSeq();
			DownCommandSeqQueueMap.getInstance().put(key, dcsb);
			
		} catch (Throwable t) {
			throw new HandleException("handle DownQuestionReq request failed.",
					t);
		}
	}

	public DownQuestionReq parse(String msg) throws ParseException {
		// TODO Auto-generated method stub
		try {
			DownQuestionReq resp = new DownQuestionReq();

			String message[] = msg.split("\\s+");

			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse DownQuestionReq failed.", t);
		}
	}

	public DownQuestionReq parse(String[] msg) throws ParseException {
		// TODO Auto-generated method stub
		DownQuestionReq resp = new DownQuestionReq();

		try {
			resp.setBody(msg);
			// 把设置终端参数命令对象放入下行命令队列中
			DownCommandBuffer.getInstance().add(resp);
			
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			throw new ParseException("parse DownQuestionReq failed.", e);
		}

		return resp;

	}
}
