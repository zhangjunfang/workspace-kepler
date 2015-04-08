package com.kypt.c2pp.inside.processor;

import java.util.concurrent.ConcurrentHashMap;

import com.kypt.c2pp.inside.msg.req.DownQuestionReq;
import com.kypt.c2pp.inside.msg.req.DownTextReq;
import com.kypt.c2pp.inside.msg.req.PhotoGraphReq;
import com.kypt.c2pp.inside.msg.req.QueryTerminalParamReq;
import com.kypt.c2pp.inside.msg.req.SetTerminalEventReq;
import com.kypt.c2pp.inside.msg.req.SetTerminalParamReq;
import com.kypt.c2pp.inside.msg.req.TerminalListenerSeq;
import com.kypt.c2pp.inside.msg.resp.ActiveTestResp;
import com.kypt.c2pp.inside.msg.resp.BindResp;
import com.kypt.c2pp.inside.msg.resp.LoginResp;
import com.kypt.c2pp.inside.processor.req.DownQuestionReqProcessor;
import com.kypt.c2pp.inside.processor.req.DownTextReqProcessor;
import com.kypt.c2pp.inside.processor.req.PhotoGraphReqProcessor;
import com.kypt.c2pp.inside.processor.req.QueryTerminalParamReqProcessor;
import com.kypt.c2pp.inside.processor.req.SetTerminalEventReqProcessor;
import com.kypt.c2pp.inside.processor.req.SetTerminalParamReqProcessor;
import com.kypt.c2pp.inside.processor.req.TerminalListenerSeqProcessor;
import com.kypt.c2pp.inside.processor.resp.ActiveTestRespProcessor;
import com.kypt.c2pp.inside.processor.resp.BindRespProcessor;
import com.kypt.c2pp.inside.processor.resp.LoginRespProcessor;


public final class InsideProcessorMap {

	private static final InsideProcessorMap PROCESSOR_MAP = new InsideProcessorMap();

	@SuppressWarnings("unchecked")
	private ConcurrentHashMap<String, IInsideProcessor> map = new ConcurrentHashMap<String, IInsideProcessor>();

	private InsideProcessorMap() {
		map.put(BindResp.COMMAND, BindRespProcessor.getInstance());
		map.put(LoginResp.COMMAND, LoginRespProcessor.getInstance());
		map.put(ActiveTestResp.COMMAND, ActiveTestRespProcessor.getInstance());
		map.put(SetTerminalParamReq.COMMAND, SetTerminalParamReqProcessor.getInstance());
		map.put(SetTerminalEventReq.COMMAND, SetTerminalEventReqProcessor.getInstance());
		map.put(QueryTerminalParamReq.COMMAND, QueryTerminalParamReqProcessor.getInstance());
		map.put(DownTextReq.COMMAND, DownTextReqProcessor.getInstance());
		map.put(DownQuestionReq.COMMAND, DownQuestionReqProcessor.getInstance());
		map.put(PhotoGraphReq.COMMAND, PhotoGraphReqProcessor.getInstance());
		map.put(TerminalListenerSeq.COMMAND, TerminalListenerSeqProcessor.getInstance());
	}

	public static InsideProcessorMap getInstance() {
		return PROCESSOR_MAP;
	}

	@SuppressWarnings("unchecked")
	public IInsideProcessor getProcessor(String command) throws Exception {
		if (!map.containsKey(command)) {
			throw new Exception("processor is not exist.");
		}
		return map.get(command);
	}

}
