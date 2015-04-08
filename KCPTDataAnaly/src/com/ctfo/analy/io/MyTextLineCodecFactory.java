package com.ctfo.analy.io;

import java.nio.charset.Charset;

import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolCodecFactory;
import org.apache.mina.filter.codec.ProtocolDecoder;
import org.apache.mina.filter.codec.ProtocolEncoder;
import org.apache.mina.filter.codec.textline.LineDelimiter;

public class MyTextLineCodecFactory
  implements ProtocolCodecFactory
{
  private final MyTextLineEncoder encoder;
  private final MyTextLineDecoder decoder;

  public MyTextLineCodecFactory()
  {
    this(Charset.defaultCharset());
  }

  public MyTextLineCodecFactory(Charset charset)
  {
    this.encoder = new MyTextLineEncoder(charset, LineDelimiter.UNIX);
    this.decoder = new MyTextLineDecoder(charset, LineDelimiter.AUTO);
  }

  public MyTextLineCodecFactory(Charset charset, String encodingDelimiter, String decodingDelimiter)
  {
    this.encoder = new MyTextLineEncoder(charset, encodingDelimiter);
    this.decoder = new MyTextLineDecoder(charset, decodingDelimiter);
  }

  public MyTextLineCodecFactory(Charset charset, LineDelimiter encodingDelimiter, LineDelimiter decodingDelimiter)
  {
    this.encoder = new MyTextLineEncoder(charset, encodingDelimiter);
    this.decoder = new MyTextLineDecoder(charset, decodingDelimiter);
  }

  public ProtocolEncoder getEncoder(IoSession session) {
    return this.encoder;
  }

  public ProtocolDecoder getDecoder(IoSession session) {
    return this.decoder;
  }

  public int getEncoderMaxLineLength()
  {
    return this.encoder.getMaxLineLength();
  }

  public void setEncoderMaxLineLength(int maxLineLength)
  {
    this.encoder.setMaxLineLength(maxLineLength);
  }

  public int getDecoderMaxLineLength()
  {
    return this.decoder.getMaxLineLength();
  }

  public void setDecoderMaxLineLength(int maxLineLength)
  {
    this.decoder.setMaxLineLength(maxLineLength);
  }
}