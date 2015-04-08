package com.ctfo.storage.dispatch.io;

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


import com.ctfo.storage.dispatch.model.ResponseModel;
import com.ctfo.storage.dispatch.parse.ResponseListen;
import com.ctfo.storage.dispatch.util.Tools;

public class ByteDecoder extends ProtocolDecoderAdapter {
	private static final Logger logger = LoggerFactory.getLogger(ByteDecoder.class);
	private final AttributeKey CONTEXT = new AttributeKey(getClass(), "context");
	private int bufferLength = 102400;
	private final Charset charset;
	private int maxLineLength = 1048576; // 1MB

	
	public ByteDecoder() { 
		this.charset = Charset.forName("utf-8");
	}
	public ByteDecoder(Charset charset) {
		this.charset = charset;
	}

	public void decode(IoSession session, IoBuffer in, ProtocolDecoderOutput out) throws Exception {
		Context ctx = getContext(session);
		ResponseListen.setSession(session);
		int matchCount = ctx.getMatchCount();
		int oldPos = in.position();
		int oldLimit = in.limit();

        while (in.hasRemaining()) {
            byte b = in.get();
           
            boolean matched = false;

            switch (b) {
            case 0x5d:
                // UNIX
                matchCount++;
                matched = true;
                break;

            default:
                matchCount = 0;
            }

            if (matched) {
                // Found a match.
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
//                		byte[] data = new byte[in.limit()];
//                		System.arraycopy(in.array(), 0, data, 0, in.limit());
//                		String content = Tools.bytesToHexStr(data);// 增加二进制转16进制字符串
                        
                        buf.get(data);
//                        CharsetDecoder decoder = ctx.getDecoder();
//                        ByteBuffer byteBuffer = ByteBuffer.wrap(data);
//                        byte start = byteBuffer.get();
//                        int source = byteBuffer.getInt();
//                        int destination = byteBuffer.getInt();
//                        short type = byteBuffer.getShort();
//                        int seri = byteBuffer.getInt();
//                        int length = byteBuffer.getInt();
//                        CharBuffer buffer = decoder.decode(ByteBuffer.wrap(data));
//                        String str = new String(buffer.array());
                        

                        String str = Tools.bytesToHexStr(data) + "5D";// 增加二进制转16进制字符串
                        if(str != null){
                        	str= Tools.getTransferContent(str);
                        	if(Tools.isRightDeviceCode(str)){
                        		writeText(session, str, out);
                        		
//                        		消息应答
                        		//ResponseModel response = new ResponseModel();
                        		//response.setSession(session);
                        		//response.setSourceStr(str);
                        		ResponseListen.put(str);
                        	}else{
                        		logger.error("----------------------------验证错误！"+str);
                        	}
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
        // Put remainder to buf.
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
