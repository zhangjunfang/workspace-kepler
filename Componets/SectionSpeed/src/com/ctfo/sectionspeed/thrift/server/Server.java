package com.ctfo.sectionspeed.thrift.server;

import org.apache.thrift.protocol.TBinaryProtocol;
import org.apache.thrift.server.TThreadPoolServer;
import org.apache.thrift.server.TThreadPoolServer.Args;
import org.apache.thrift.transport.TServerSocket;
import org.apache.thrift.transport.TServerTransport;

import com.ctfo.sectionspeed.thrift.SectionSpeed;
import com.ctfo.sectionspeed.thrift.SectionSpeed.Iface;


public class Server {
	public static void main(String[] args) throws Exception {
		SectionSpeed.Processor<Iface> processor = new SectionSpeed.Processor<Iface>(new SectionSpeedHandler());
		TServerTransport transport = new TServerSocket(3344);
		Args arg = new TThreadPoolServer.Args(transport);
		arg.processor(processor);
		arg.protocolFactory(new TBinaryProtocol.Factory());
		TThreadPoolServer server = new TThreadPoolServer(arg);
		System.out.println("服务端启动");
		server.serve();
	}
}
