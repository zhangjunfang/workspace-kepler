package com.ctfo.analy.io;

import java.nio.charset.Charset;
import java.nio.charset.CharsetEncoder;

import org.apache.mina.core.buffer.IoBuffer;
import org.apache.mina.core.session.AttributeKey;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolEncoderAdapter;
import org.apache.mina.filter.codec.ProtocolEncoderOutput;
import org.apache.mina.filter.codec.textline.LineDelimiter;

public class MyTextLineEncoder extends ProtocolEncoderAdapter
{
  private final AttributeKey ENCODER = new AttributeKey(getClass(), "encoder");
  private final Charset charset;
  private final LineDelimiter delimiter;
  private int maxLineLength = 2147483647;

  public MyTextLineEncoder()
  {
    this(Charset.defaultCharset(), LineDelimiter.UNIX);
  }

  public MyTextLineEncoder(String delimiter)
  {
    this(new LineDelimiter(delimiter));
  }

  public MyTextLineEncoder(LineDelimiter delimiter)
  {
    this(Charset.defaultCharset(), delimiter);
  }

  public MyTextLineEncoder(Charset charset)
  {
    this(charset, LineDelimiter.UNIX);
  }

  public MyTextLineEncoder(Charset charset, String delimiter)
  {
    this(charset, new LineDelimiter(delimiter));
  }

  public MyTextLineEncoder(Charset charset, LineDelimiter delimiter)
  {
    if (charset == null) {
      throw new IllegalArgumentException("charset");
    }
    if (delimiter == null) {
      throw new IllegalArgumentException("delimiter");
    }
    if (LineDelimiter.AUTO.equals(delimiter)) {
      throw new IllegalArgumentException("AUTO delimiter is not allowed for encoder.");
    }

    this.charset = charset;
    this.delimiter = delimiter;
  }

  public int getMaxLineLength()
  {
    return this.maxLineLength;
  }

  public void setMaxLineLength(int maxLineLength)
  {
    if (maxLineLength <= 0) {
      throw new IllegalArgumentException("maxLineLength: " + maxLineLength);
    }

    this.maxLineLength = maxLineLength;
  }

  public void encode(IoSession session, Object message, ProtocolEncoderOutput out) throws Exception
  {
    CharsetEncoder encoder = (CharsetEncoder)session.getAttribute(this.ENCODER);

    if (encoder == null) {
      encoder = this.charset.newEncoder();
      session.setAttribute(this.ENCODER, encoder);
    }

    String value = message == null ? "" : message.toString();
    IoBuffer buf = IoBuffer.allocate(value.length()).setAutoExpand(true);

    buf.putString(value, encoder);

    if (buf.position() > this.maxLineLength) {
      throw new IllegalArgumentException("Line length: " + buf.position());
    }

    buf.putString(this.delimiter.getValue(), encoder);
    buf.flip();
    out.write(buf);
  }

  public void dispose()
    throws Exception
  {
  }
}