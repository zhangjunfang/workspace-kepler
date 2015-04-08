package com.ocean.transilink.mina;

import java.nio.charset.Charset;
import java.nio.charset.CharsetDecoder;

import org.apache.mina.core.buffer.IoBuffer;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.CumulativeProtocolDecoder;
import org.apache.mina.filter.codec.ProtocolDecoderOutput;

/**
 * @Description: 解码工具类
 * @author ocean
 * @date 2015年1月1日
 */
public class HDecoder extends CumulativeProtocolDecoder {
	private final Charset charset;

	public HDecoder(Charset charset) {
		this.charset = charset;
	}

	@Override
	public boolean doDecode(IoSession session, IoBuffer in,
			ProtocolDecoderOutput out) throws Exception {

		// System.out.println("-------doDecode----------");

		CharsetDecoder cd = charset.newDecoder();
		String mes = in.getString(cd);
		out.write(mes);
		return true;

		/*
		 * if (in.remaining() > 4) {// 有数据时，读取字节判断消息长度 in.mark();//
		 * 标记当前位置，以便reset int size = in.getInt();
		 * 
		 * // 如果消息内容不够，则重置，相当于不读取size if (size > in.remaining()) { in.reset();
		 * return false;// 接收新数据，以拼凑成完整数据 } else if (size != 0 && (size - 4 >=
		 * 0)) { byte[] bytes = new byte[size - 4]; //int protocol =
		 * in.getInt(); // 拿到客户端发过来的数据组装成基础包写出去 in.get(bytes, 0, size - 4);
		 * //in.get(bytes, size - 4, size);
		 * 
		 * PackageBeanFactory beanFactory = (PackageBeanFactory) session
		 * .getAttribute(ServerHandler.BEAN_FACTORY);
		 * //out.write(beanFactory.getPackage(protocol, size, bytes));
		 * 
		 * String mes = in.getString(cd);
		 * 
		 * out.write(mes); // 如果读取内容后还粘了包，就让父类再给读取进行下次解析 if (in.remaining() > 0)
		 * { return true; } } }
		 * 
		 * return false;// 处理成功，让父类进行接收下个包
		 */

	}
}
