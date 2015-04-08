package com.ctfo.storage.media.io;

import org.apache.mina.core.buffer.IoBuffer;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolEncoderAdapter;
import org.apache.mina.filter.codec.ProtocolEncoderOutput;

import com.ctfo.storage.media.util.Tools;

public class ByteEncoder extends ProtocolEncoderAdapter {

	public void encode(IoSession session, Object message, ProtocolEncoderOutput out) throws Exception {
		IoBuffer buf = null; 	
		byte[] content = Tools.hexStrToBytes(message.toString());//增加16进制转二进制
		buf = IoBuffer.allocate(content.length);
		buf.put(content); 
		
		buf.flip();
		out.write(buf);
		out.flush(); 
	}

}
