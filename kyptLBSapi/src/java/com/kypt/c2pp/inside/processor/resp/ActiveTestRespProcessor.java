package com.kypt.c2pp.inside.processor.resp;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.inside.msg.resp.ActiveTestResp;
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
public final class ActiveTestRespProcessor extends
		AbstractInsideProcessor<ActiveTestResp, SupCommService> {
	
	 private Logger log = LoggerFactory.getLogger(ActiveTestRespProcessor.class);

	private static final ActiveTestRespProcessor PROCESSOR = new ActiveTestRespProcessor();

	private ActiveTestRespProcessor() {
	}

	public static ActiveTestRespProcessor getInstance() {
		return PROCESSOR;
	}

	public ActiveTestResp parse(byte[] buf) throws ParseException {
		try {
			ActiveTestResp resp = new ActiveTestResp();
			super.parseHeader(buf, resp);
			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse bind response failed.", t);
		}
	}

	public void valid(ActiveTestResp msg) throws InvalidMessageException {
		super.validHeader(msg);
	}

	public void handle(ActiveTestResp msg, SupCommService supCommService)
			throws HandleException {
		try {
            log.info(LogFormatter.formatMsg("ActiveTestRespProcessor", "receive a active test response."));
        } catch (Throwable t) {
            throw new HandleException("handle active test response failed.", t);
        }
	}

	public ActiveTestResp parse(String msg) throws ParseException {
		// TODO Auto-generated method stub
		try {
			ActiveTestResp resp = new ActiveTestResp();

			String message[] = msg.split("\\s+");

			return resp;
		} catch (Throwable t) {
			throw new ParseException("parse bind response failed.", t);
		}
	}

	public ActiveTestResp parse(String[] msg) throws ParseException {
		// TODO Auto-generated method stub
		ActiveTestResp resp = new ActiveTestResp();
		return resp;
	}
}
