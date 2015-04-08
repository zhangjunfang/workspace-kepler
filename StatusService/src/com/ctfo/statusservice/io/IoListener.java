package com.ctfo.statusservice.io;

import org.apache.mina.core.service.IoService;
import org.apache.mina.core.service.IoServiceListener;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;

public class IoListener implements IoServiceListener {

	@Override
	public void serviceActivated(IoService arg0) throws Exception {
	}

	@Override
	public void serviceDeactivated(IoService arg0) throws Exception {
	}

	@Override
	public void serviceIdle(IoService arg0, IdleStatus arg1) throws Exception {
	}

	@Override
	public void sessionCreated(IoSession arg0) throws Exception {
	}

	@Override
	public void sessionDestroyed(IoSession arg0) throws Exception {
	}

}
