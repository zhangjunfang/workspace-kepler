package com.ctfo.ypt.client;

import java.net.InetSocketAddress;

import org.apache.mina.core.RuntimeIoException;
import org.apache.mina.core.future.ConnectFuture;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolCodecFilter;
import org.apache.mina.transport.socket.nio.NioSocketConnector;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.context.CustomizedPropertyPlaceholderConfigurer;

public class YptClient {
	
	private static final Logger logger = LoggerFactory.getLogger(YptClient.class);
	
	private IoSession session = null;
	
	public YptClient(YptClientHandler yptClientHandler){
		this.connectorServer(yptClientHandler);
	}
	
	/**
	 * 
	 * @description:连接云平台服务端
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年11月17日下午4:55:08
	 * @modifyInformation：
	 */
	public void connectorServer(YptClientHandler yptClientHandler){
		
		//获取云平台服务ip，端口
		final String msgServerIp = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("msgServerIp");
		String msgServerPort = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("msgServerPort");
		final int msgServerPort_int = new Integer(msgServerPort).intValue();
		
		final NioSocketConnector nioSocketConnector = new NioSocketConnector();
		
		try {
			nioSocketConnector.setConnectTimeoutMillis(1000 * 1000);
			nioSocketConnector.getFilterChain().addLast("codec", new ProtocolCodecFilter(new ByteCodecFactory()));
			nioSocketConnector.getSessionConfig().setReceiveBufferSize(10240);
			// 设置输出缓冲区的大小
			nioSocketConnector.getSessionConfig().setSendBufferSize(10240);
			// 读写通道10s内无操作,则进入空闲状
			nioSocketConnector.getSessionConfig().setIdleTime(IdleStatus.READER_IDLE, 60);
			nioSocketConnector.setHandler(yptClientHandler);
			
			//添加通信链路监听，如果链路断开自动重连
			nioSocketConnector.addListener(new IoListener() {  
	            public void sessionDestroyed(IoSession arg0) throws Exception {  
	                for (;;) {  
	                    try {  
	                        Thread.sleep(9000);  
	                        ConnectFuture future = nioSocketConnector.connect(new InetSocketAddress(msgServerIp, msgServerPort_int));  
	                        future.awaitUninterruptibly();// 等待连接创建成功  
	                        session = future.getSession();// 获取会话  
	                        if (session.isConnected()) {  
	                            logger.info("断线重连[" + session.getRemoteAddress() + "]成功");  
	                            break;  
	                        }  
	                    } catch (Exception ex) {  
	                        logger.info("重连服务器登录失败,6秒再连接一次:" + ex.getMessage());  
	                    }  
	                }  
	            }  
	        }); 
			
		} catch (Exception e) {
			e.printStackTrace();
		}

		try {
			ConnectFuture future = nioSocketConnector.connect(new InetSocketAddress(msgServerIp, msgServerPort_int));
			future.awaitUninterruptibly(); // 等待连接创建成功  
			session = future.getSession(); // 获取会话  
		} catch (RuntimeIoException e) {
			e.printStackTrace();
		}  
	}

	public IoSession getSession() {
		return session;
	}

}
