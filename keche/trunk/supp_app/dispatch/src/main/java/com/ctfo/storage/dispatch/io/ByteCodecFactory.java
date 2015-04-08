package com.ctfo.storage.dispatch.io;

import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolCodecFactory;
import org.apache.mina.filter.codec.ProtocolDecoder;
import org.apache.mina.filter.codec.ProtocolEncoder;

public class ByteCodecFactory implements ProtocolCodecFactory {

	public ProtocolDecoder getDecoder(IoSession session) throws Exception {
		return new ByteDecoder();
	}

	public ProtocolEncoder getEncoder(IoSession session) throws Exception {
		return new ByteEncoder(); 
	}

}
