package com.kypt.c2pp.inside.processor.req;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.back.DownCommandSeqQueueMap;
import com.kypt.c2pp.back.DownCommandSummaryBean;
import com.kypt.c2pp.buffer.DownCommandBuffer;
import com.kypt.c2pp.inside.msg.req.QueryTerminalParamReq;
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
public final class QueryTerminalParamReqProcessor extends
		AbstractInsideProcessor<QueryTerminalParamReq, SupCommService> {

	private Logger log = LoggerFactory
			.getLogger(QueryTerminalParamReqProcessor.class);

	private static final QueryTerminalParamReqProcessor PROCESSOR = new QueryTerminalParamReqProcessor();

	private QueryTerminalParamReqProcessor() {
	}

	public static QueryTerminalParamReqProcessor getInstance() {
		return PROCESSOR;
	}

	public QueryTerminalParamReq parse(byte[] buf) throws ParseException {
		try {
			QueryTerminalParamReq resp = new QueryTerminalParamReq();
			super.parseHeader(buf, resp);
			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse QueryTerminalParamReq failed.", t);
		}
	}

	public void valid(QueryTerminalParamReq msg) throws InvalidMessageException {
		super.validHeader(msg);
	}

	public void handle(QueryTerminalParamReq msg, SupCommService supCommService)
			throws HandleException {
		try {
			log.info(LogFormatter.formatMsg("QueryTerminalParamReq",
					"receive a QueryTerminalParamReq request."));
			
			//向下行消息概要Map中加入本指令概要，等待消息回复。
			DownCommandSummaryBean dcsb = new DownCommandSummaryBean();
			dcsb.setCommand(msg.getCommand());
			dcsb.setSeqId(msg.getSeq());
			dcsb.setAddress(supCommService.getRemoteAddress());
			String key = supCommService.getRemoteAddress()+msg.getSeq();
			DownCommandSeqQueueMap.getInstance().put(key, dcsb);
			
		} catch (Throwable t) {
			throw new HandleException(
					"handle QueryTerminalParamReq request failed.", t);
		}
	}

	public QueryTerminalParamReq parse(String msg) throws ParseException {
		// TODO Auto-generated method stub
		try {
			QueryTerminalParamReq resp = new QueryTerminalParamReq();

			String message[] = msg.split("\\s+");

			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse QueryTerminalParamReq failed.", t);
		}
	}

	public QueryTerminalParamReq parse(String[] msg) throws ParseException {
		// TODO Auto-generated method stub
		QueryTerminalParamReq resp = new QueryTerminalParamReq();

		resp.setBody(msg);
		// 把设置终端参数命令对象放入下行命令队列中
		DownCommandBuffer.getInstance().add(resp);

		return resp;

	}
}
