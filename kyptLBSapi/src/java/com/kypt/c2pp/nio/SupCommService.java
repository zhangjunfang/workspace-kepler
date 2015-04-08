package com.kypt.c2pp.nio;

import com.kypt.configuration.C2ppCfg;
import com.kypt.nio.client.TcpNioService;


public class SupCommService extends TcpNioService {

	public SupCommService(String ip, int port) throws Exception {
        super(ip, port);
    }

    @Override
    protected void setDecoder() {
        this.decoder = SupDecoder.getInstance();
    }

    @Override
    protected void setNioHandler() {
        this.nioHandler = new SupCommunicateHandler(this);
    }

    @Override
    protected void setProcessorNum() {
        this.processorNum = Integer.parseInt(C2ppCfg.props.getProperty("processorNum"));
    }

    @Override
    protected void setReconnectInterval() {
        this.reconnectInterval = Integer.parseInt(C2ppCfg.props.getProperty("reconnectInterval"));
    }

    @Override
    protected void setThreadPoolSize() {
        this.threadPoolSize = Integer.parseInt(C2ppCfg.props.getProperty("threadPoolSize"));
    }
}
