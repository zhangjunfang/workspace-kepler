package com.kypt.c2pp.inside.processor.req;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.back.DownCommandSeqQueueMap;
import com.kypt.c2pp.back.DownCommandSummaryBean;
import com.kypt.c2pp.buffer.DownCommandBuffer;
import com.kypt.c2pp.inside.msg.req.PhotoGraphReq;
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
public final class PhotoGraphReqProcessor extends
		AbstractInsideProcessor<PhotoGraphReq, SupCommService> {

	private Logger log = LoggerFactory
			.getLogger(SetTerminalParamReqProcessor.class);

	private static final PhotoGraphReqProcessor PROCESSOR = new PhotoGraphReqProcessor();

	private PhotoGraphReqProcessor() {
	}

	public static PhotoGraphReqProcessor getInstance() {
		return PROCESSOR;
	}

	public PhotoGraphReq parse(byte[] buf) throws ParseException {
		try {
			PhotoGraphReq resp = new PhotoGraphReq();
			super.parseHeader(buf, resp);
			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse PhotoGraphReq failed.", t);
		}
	}

	public void valid(PhotoGraphReq msg) throws InvalidMessageException {
		super.validHeader(msg);
	}

	public void handle(PhotoGraphReq msg, SupCommService supCommService)
			throws HandleException {
		try {
			log.info(LogFormatter.formatMsg("PhotoGraphReq",
					"receive a PhotoGraphReq request."));
			
			//向下行消息概要Map中加入本指令概要，等待消息回复。
			DownCommandSummaryBean dcsb = new DownCommandSummaryBean();
			dcsb.setCommand(msg.getCommand());
			dcsb.setSeqId(msg.getSeq());
			dcsb.setAddress(supCommService.getRemoteAddress());
			String key = supCommService.getRemoteAddress()+msg.getSeq();
			DownCommandSeqQueueMap.getInstance().put(key, dcsb);
		} catch (Throwable t) {
			throw new HandleException("handle PhotoGraphReq request failed.", t);
		}
	}

	public PhotoGraphReq parse(String msg) throws ParseException {
		// TODO Auto-generated method stub
		try {
			PhotoGraphReq resp = new PhotoGraphReq();

			String message[] = msg.split("\\s+");

			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse PhotoGraphReq failed.", t);
		}
	}

	public PhotoGraphReq parse(String[] msg) throws ParseException {
		// TODO Auto-generated method stub
		PhotoGraphReq resp = new PhotoGraphReq();
		resp.setBody(msg);
		// 把设置终端参数命令对象放入下行命令队列中
		DownCommandBuffer.getInstance().add(resp);
		return resp;
	}
}
