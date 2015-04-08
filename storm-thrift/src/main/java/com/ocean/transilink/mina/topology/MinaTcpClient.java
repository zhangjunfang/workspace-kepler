package com.ocean.transilink.mina.topology;

import java.net.InetSocketAddress;
import java.nio.charset.Charset;
import java.util.UUID;

import org.apache.mina.core.buffer.IoBuffer;
import org.apache.mina.core.future.ConnectFuture;
import org.apache.mina.core.service.IoConnector;
import org.apache.mina.core.service.IoHandlerAdapter;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolCodecFilter;
import org.apache.mina.filter.codec.textline.LineDelimiter;
import org.apache.mina.filter.codec.textline.TextLineCodecFactory;
import org.apache.mina.transport.socket.nio.NioSocketConnector;

/**
 * @说明 Mina TCP客户端
 * @author 崔素强
 * @version 1.0
 * @since
 */
public class MinaTcpClient extends IoHandlerAdapter {
	private IoConnector connector;
	private static IoSession session;

	public MinaTcpClient() {
		connector = new NioSocketConnector();
		connector.getFilterChain().addLast(
				"encode",
				new ProtocolCodecFilter(new TextLineCodecFactory(Charset
						.forName("UTF-8"), LineDelimiter.WINDOWS.getValue(),
						LineDelimiter.WINDOWS.getValue())));

		connector.getSessionConfig().setReadBufferSize(2048);
		connector.getSessionConfig().setIdleTime(IdleStatus.BOTH_IDLE, 10);
		connector.setConnectTimeoutMillis(30000);
		connector.setHandler(this);
		ConnectFuture connFuture = connector.connect(new InetSocketAddress(
				"localhost", 4444));
		connFuture.awaitUninterruptibly();
		session = connFuture.getSession();
		System.out.println("TCP 客户端启动");
	}

	public static void main(String[] args) throws Exception {
		MinaTcpClient client = new MinaTcpClient();
		for (int j = 0; j < 2; j++) { // 发送两遍
			StringBuffer buffer = new StringBuffer();
			buffer.append(UUID.randomUUID().toString());
			session.write(buffer.toString());
			Thread.sleep(2000);
		}
		// 关闭会话，待所有线程处理结束后
		client.connector.dispose(true);
	}

	@Override
	public void messageReceived(IoSession iosession, Object message)
			throws Exception {
		IoBuffer bbuf = (IoBuffer) message;
		byte[] byten = new byte[bbuf.limit()];
		bbuf.get(byten, bbuf.position(), bbuf.limit());
		System.out.println("客户端收到消息" + new String(byten));
	}

	@Override
	public void exceptionCaught(IoSession session, Throwable cause)
			throws Exception {
		System.out.println("客户端异常");
		super.exceptionCaught(session, cause);
	}

	@Override
	public void messageSent(IoSession iosession, Object obj) throws Exception {
		System.out.println("客户端消息发送");
		super.messageSent(iosession, obj);
	}

	@Override
	public void sessionClosed(IoSession iosession) throws Exception {
		System.out.println("客户端会话关闭");
		super.sessionClosed(iosession);
	}

	@Override
	public void sessionCreated(IoSession iosession) throws Exception {
		System.out.println("客户端会话创建");
		super.sessionCreated(iosession);
	}

	@Override
	public void sessionIdle(IoSession iosession, IdleStatus idlestatus)
			throws Exception {
		System.out.println("客户端会话休眠");
		super.sessionIdle(iosession, idlestatus);
	}

	@Override
	public void sessionOpened(IoSession iosession) throws Exception {
		System.out.println("客户端会话打开");
		super.sessionOpened(iosession);
	}
}