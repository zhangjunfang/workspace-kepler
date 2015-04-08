package com.ctfo.storage.io;

import java.nio.charset.Charset;
import java.nio.charset.CharsetDecoder;

import org.apache.mina.core.buffer.IoBuffer;
import org.apache.mina.core.session.AttributeKey;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolDecoderAdapter;
import org.apache.mina.filter.codec.ProtocolDecoderOutput;
import org.apache.mina.filter.codec.RecoverableProtocolDecoderException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.parse.FileResponseListen;
import com.ctfo.storage.util.Constant;
import com.ctfo.storage.util.Tools;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 解码<br>
 * 描述： 解码<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-10-23</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class FileByteDecoder extends ProtocolDecoderAdapter {

	private static final Logger logger = LoggerFactory.getLogger(FileByteDecoder.class);
	private final AttributeKey CONTEXT = new AttributeKey(getClass(), "context");
	private int bufferLength = 102400;
	private final Charset charset;
	private int maxLineLength = 1048576; // 1MB

	public FileByteDecoder() {
		this.charset = Charset.forName("utf-8");
	}

	public FileByteDecoder(Charset charset) {
		this.charset = charset;
	}

	@Override
	public void decode(IoSession session, IoBuffer in, ProtocolDecoderOutput out) throws Exception {
		Context ctx = getContext(session);
		int matchCount = ctx.getMatchCount();
		int oldPos = in.position();
		int oldLimit = in.limit();
		// 如果有消息
		while (in.hasRemaining()) {
			byte b = in.get();
			boolean matched = false;
			// 判断消息是否有结束符"]"
			switch (b) {
			case 93:
				matchCount++;
				matched = true;
				break;
			default:
				matchCount = 0;
			}
			if (matched) {
				int pos = in.position();
				in.limit(pos);
				in.position(oldPos);

				ctx.append(in);

				in.limit(oldLimit);
				in.position(pos);
				if (ctx.getOverflowPosition() == 0) {
					IoBuffer buf = ctx.getBuffer();

					buf.flip();
					buf.limit(buf.limit() - matchCount);
					try {
						byte[] data = new byte[buf.limit()];
						buf.get(data);
						String message = new String(data, "utf-8") + Constant.RIGHT_BRACKET;
						if (null != message) {
							if (Tools.isRightDeviceCode(message)) { // 判断协议格式是否正确
								logger.info("RECEIVED:{}", message);
								writeText(session, message, out); // 写数据
								FileResponseListen.setSession(session);
								FileResponseListen.put(message); // 应答
							} else {
								logger.error("验证错误");
							}
						} else {
							logger.error("数据为空");
						}
					} finally {
						buf.clear();
					}
				} else {
					int overflowPosition = ctx.getOverflowPosition();
					ctx.reset();
					throw new RecoverableProtocolDecoderException("Line is too long: " + overflowPosition);
				}
				oldPos = pos;
				matchCount = 0;
			}
		}
		in.position(oldPos);
		ctx.append(in);
		ctx.setMatchCount(matchCount);
	}

	protected void writeText(IoSession session, String text, ProtocolDecoderOutput out) {
		out.write(text);
	}

	/**
	 * Return the context for this session
	 */
	private Context getContext(IoSession session) {
		Context ctx;
		ctx = (Context) session.getAttribute(CONTEXT);
		if (ctx == null) {
			ctx = new Context(bufferLength);
			session.setAttribute(CONTEXT, ctx);
		}
		return ctx;
	}

	private class Context {

		/** The decoder */
		private final CharsetDecoder decoder;

		/** The temporary buffer containing the decoded line */
		private final IoBuffer buf;

		/** The number of lines found so far */
		private int matchCount = 0;

		/** A counter to signal that the line is too long */
		private int overflowPosition = 0;

		/** Create a new Context object with a default buffer */
		private Context(int bufferLength) {
			decoder = charset.newDecoder();
			buf = IoBuffer.allocate(bufferLength).setAutoExpand(true);
		}

		@SuppressWarnings("unused")
		public CharsetDecoder getDecoder() {
			return decoder;
		}

		public IoBuffer getBuffer() {
			return buf;
		}

		public int getOverflowPosition() {
			return overflowPosition;
		}

		public int getMatchCount() {
			return matchCount;
		}

		public void setMatchCount(int matchCount) {
			this.matchCount = matchCount;
		}

		public void reset() {
			overflowPosition = 0;
			matchCount = 0;
			decoder.reset();
		}

		public void append(IoBuffer in) {
			if (overflowPosition != 0) {
				discard(in);
			} else if (buf.position() > maxLineLength - in.remaining()) {
				overflowPosition = buf.position();
				buf.clear();
				discard(in);
			} else {
				getBuffer().put(in);
			}
		}

		private void discard(IoBuffer in) {
			if (Integer.MAX_VALUE - in.remaining() < overflowPosition) {
				overflowPosition = Integer.MAX_VALUE;
			} else {
				overflowPosition += in.remaining();
			}
			in.position(in.limit());
		}
	}
}
