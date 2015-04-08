/*******************************************************************************
 * @(#)Decode.java 2008-10-24
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.nio;

import org.apache.mina.common.ByteBuffer;
import org.apache.mina.common.IoSession;
import org.apache.mina.filter.codec.ProtocolDecoder;
import org.apache.mina.filter.codec.ProtocolDecoderOutput;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.nio.client.Decoder;
import com.kypt.c2pp.inside.msg.InsideMsg;

//import com.neusoft.communicate.Decoder;
//import com.neusoft.constant.Module;
//import com.neusoft.log.Logformater;
//import com.neusoft.tag.app.inside.msg.InsideMsg;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-10-24 11:55:33
 */
public final class SupDecoder extends Decoder {

    private static final Logger LOG = LoggerFactory.getLogger(SupDecoder.class);

    private ProtocolDecoder decoder;

    private static final String KEY = "key";

    private static final SupDecoder OTADECODER = new SupDecoder();

    private SupDecoder() {
        decoder = new Decoder();
    }

    public static SupDecoder getInstance() {
        return OTADECODER;
    }

    static class Decoder implements ProtocolDecoder {

        public void decode(IoSession session, ByteBuffer buf, ProtocolDecoderOutput out)
                throws Exception {
            int limit = buf.limit();

            System.out.println("decode begin");
            byte[] bytes = new byte[limit];
            buf.get(bytes,0,limit);
            buf.position(0);
            
            String sbuf=new String(bytes);
            LOG.debug("::::"+sbuf);
          
            System.out.println("decode begin"+"::::"+sbuf+"+++"+session.getAttribute(KEY));
            
            if (session.getAttribute(KEY) == null) {
                Data data = new Data();
                session.setAttribute(KEY, data);
            }
            System.out.println("decode begin2"+"::::"+sbuf);
            Data data = (Data) session.getAttribute(KEY);
            if (data.getStatus().equals(Data.RIGHT)) {
                doMsg(buf, out, limit, data);
            } else if (data.getStatus().equals(Data.LESS)) {
                assemble4Less(buf, out, limit, data);
            } else if (data.getStatus().equals(Data.LENGTH_NOT_ENOUGH)) {
                doMsgLen(buf, out, limit, data);
            } else {
                LOG.error("CLW ", "the data's status is invalid.");
            }
        }

        private void doMsgLen(ByteBuffer buf, ProtocolDecoderOutput out, int limit, Data data) {
            if (data.getLeftLen() == 0) {
                if (limit < InsideMsg.MSGLENSIZE) {
                    byte[] bytes = new byte[limit];
                    buf.get(bytes);
                    data.setBuf(bytes);
                    data.setLeftLen(InsideMsg.MSGLENSIZE - limit);
                    data.setStatus(Data.LENGTH_NOT_ENOUGH);
                }
            } else {
                if (limit < data.getLeftLen()) {
                    byte[] bytes = new byte[limit];
                    buf.get(bytes);
                    data.setBuf(bytes);
                    data.setLeftLen(data.getLeftLen() - limit);
                    data.setStatus(Data.LENGTH_NOT_ENOUGH);
                } else if (limit > data.getLeftLen()) {
                    byte[] bytes = new byte[data.getLeftLen()];
                    buf.get(bytes);
                    data.setBuf(bytes);
                    byte[] msgLen = new byte[InsideMsg.MSGLENSIZE];
                    data.getBuf().get(msgLen);
                    int len = Integer.parseInt(new String(msgLen));
                    int moreLen = limit - data.getLeftLen();
                    data.setLeftLen(len - InsideMsg.MSGLENSIZE);
                    data.setStatus(Data.LESS);
                    assemble4Less(buf, out, moreLen, data);
                } else {
                    byte[] bytes = new byte[limit];
                    buf.get(bytes);
                    data.setBuf(bytes);
                    byte[] msgLen = new byte[InsideMsg.MSGLENSIZE];
                    data.getBuf().get(msgLen);
                    int len = Integer.parseInt(new String(msgLen));
                    data.setLeftLen(len - InsideMsg.MSGLENSIZE);
                    data.setStatus(Data.LESS);
                }
            }
        }

        private void doMsg(ByteBuffer buf, ProtocolDecoderOutput out, int limit, Data data) {
            if (data.getLeftLen() == 0 && limit < InsideMsg.MSGLENSIZE) {
                doMsgLen(buf, out, limit, data);
                return;
            }
            byte[] len = new byte[InsideMsg.MSGLENSIZE];
            buf.get(len);
            data.setBuf(len);
            int msgLength = Integer.parseInt(new String(len));
            int logicalLeftLen = msgLength - InsideMsg.MSGLENSIZE;
            int actualLeftLen = limit - InsideMsg.MSGLENSIZE;
            if (actualLeftLen < logicalLeftLen) {
                byte[] bytes = new byte[actualLeftLen];
                buf.get(bytes);
                data.setBuf(bytes);
                data.setLeftLen(logicalLeftLen - actualLeftLen);
                data.setStatus(Data.LESS);
            } else if (actualLeftLen > logicalLeftLen) {
                byte[] bytes = new byte[logicalLeftLen];
                buf.get(bytes);
                data.setBuf(bytes);
                out.write(data.getBytes());
                data.clean();
                int moreLen = actualLeftLen - logicalLeftLen;
                doMsg(buf, out, moreLen, data);
            } else {
                byte[] bytes = new byte[logicalLeftLen];
                buf.get(bytes);
                data.setBuf(bytes);
                out.write(data.getBytes());
                data.clean();
            }

        }

        private void assemble4Less(ByteBuffer buf, ProtocolDecoderOutput out, int limit, Data data) {
            if (limit < data.getLeftLen()) {
                byte[] bytes = new byte[limit];
                buf.get(bytes);
                data.setBuf(bytes);
                data.setLeftLen(data.getLeftLen() - limit);
                data.setStatus(Data.LESS);
            } else if (limit > data.getLeftLen()) {
                byte[] bytes = new byte[data.getLeftLen()];
                buf.get(bytes);
                data.setBuf(bytes);
                out.write(data.getBytes());
                int moreLen = limit - data.getLeftLen();
                data.clean();
                doMsg(buf, out, moreLen, data);
            } else {
                byte[] bytes = new byte[data.getLeftLen()];
                buf.get(bytes);
                data.setBuf(bytes);
                out.write(data.getBytes());
                data.clean();
            }
        }

        public void dispose(IoSession arg0) throws Exception {
        }

        public void finishDecode(IoSession arg0, ProtocolDecoderOutput arg1) throws Exception {
        }
    }

    static class Data {

        private static final int BLOCKSIZE = 512;

        private ByteBuffer buf = ByteBuffer.allocate(BLOCKSIZE).setAutoExpand(true);

        private int leftLen = 0;

        private String status = RIGHT;

        public static final String LESS = "LESS";

        public static final String RIGHT = "RIGHT";

        public static final String LENGTH_NOT_ENOUGH = "LENGTH_NOT_ENOUGH";

        public Data() {
            clean();
        }

        public void clean() {
            buf.limit(0);
            buf.position(0);
            leftLen = 0;
            status = RIGHT;
        }

        private byte[] getBytes() {
            byte[] bytes = new byte[getBuf().limit()];
            buf.get(bytes);
            return bytes;
        }

        public void setBuf(byte[] buf) {
            this.buf.put(buf);
        }

        public void setBuf(byte[] data, int offset, int length) {
            this.buf.put(data, offset, length);
        }

        public ByteBuffer getBuf() {
            this.buf.flip();
            return this.buf;
        }

        public void setLeftLen(int leftLen) {
            this.leftLen = leftLen;
        }

        public int getLeftLen() {
            return leftLen;
        }

        public void setStatus(String status) {
            this.status = status;
        }

        public String getStatus() {
            return status;
        }
    }

    public ProtocolDecoder getDecoder() throws Exception {
        return decoder;
    }
}
