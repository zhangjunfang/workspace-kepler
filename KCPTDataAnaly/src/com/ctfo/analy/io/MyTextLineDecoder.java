package com.ctfo.analy.io;

import java.nio.charset.CharacterCodingException;
import java.nio.charset.Charset;
import java.nio.charset.CharsetDecoder;
import java.nio.charset.MalformedInputException;
import java.nio.charset.UnmappableCharacterException;

import org.apache.mina.core.buffer.IoBuffer;
import org.apache.mina.core.session.AttributeKey;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolDecoder;
import org.apache.mina.filter.codec.ProtocolDecoderException;
import org.apache.mina.filter.codec.ProtocolDecoderOutput;
import org.apache.mina.filter.codec.RecoverableProtocolDecoderException;
import org.apache.mina.filter.codec.textline.LineDelimiter;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class MyTextLineDecoder
  implements ProtocolDecoder
{
  private static final Logger logger = LoggerFactory.getLogger(MyTextLineDecoder.class);
  private final AttributeKey CONTEXT = new AttributeKey(getClass(), "context");
  private final Charset charset;
  private final LineDelimiter delimiter;
  private IoBuffer delimBuf;
  private int maxLineLength = 1024;

  private int bufferLength = 128;

  public MyTextLineDecoder()
  {
    this(LineDelimiter.AUTO);
  }

  public MyTextLineDecoder(String delimiter)
  {
    this(new LineDelimiter(delimiter));
  }

  public MyTextLineDecoder(LineDelimiter delimiter)
  {
    this(Charset.defaultCharset(), delimiter);
  }

  public MyTextLineDecoder(Charset charset)
  {
    this(charset, LineDelimiter.AUTO);
  }

  public MyTextLineDecoder(Charset charset, String delimiter)
  {
    this(charset, new LineDelimiter(delimiter));
  }

  public MyTextLineDecoder(Charset charset, LineDelimiter delimiter)
  {
    if (charset == null) {
      throw new IllegalArgumentException("charset parameter shuld not be null");
    }

    if (delimiter == null) {
      throw new IllegalArgumentException("delimiter parameter should not be null");
    }

    this.charset = charset;
    this.delimiter = delimiter;

    if (this.delimBuf == null) {
      IoBuffer tmp = IoBuffer.allocate(2).setAutoExpand(true);
      try
      {
        tmp.putString(delimiter.getValue(), charset.newEncoder());
      }
      catch (CharacterCodingException cce)
      {
      }
      tmp.flip();
      this.delimBuf = tmp;
    }
  }

  public int getMaxLineLength()
  {
    return this.maxLineLength;
  }

  public void setMaxLineLength(int maxLineLength)
  {
    if (maxLineLength <= 0) {
      throw new IllegalArgumentException("maxLineLength (" + maxLineLength + ") should be a positive value");
    }

    this.maxLineLength = maxLineLength;
  }

  public void setBufferLength(int bufferLength)
  {
    if (bufferLength <= 0) {
      throw new IllegalArgumentException("bufferLength (" + this.maxLineLength + ") should be a positive value");
    }

    this.bufferLength = bufferLength;
  }

  public int getBufferLength()
  {
    return this.bufferLength;
  }

  public void decode(IoSession session, IoBuffer in, ProtocolDecoderOutput out)
    throws Exception
  {
    Context ctx = getContext(session);

    if (LineDelimiter.AUTO.equals(this.delimiter))
      decodeAuto(ctx, session, in, out);
    else
      decodeNormal(ctx, session, in, out);
  }

  private Context getContext(IoSession session)
  {
    Context ctx = (Context)session.getAttribute(this.CONTEXT);

    if (ctx == null) {
      ctx = new Context(this.bufferLength);
      session.setAttribute(this.CONTEXT, ctx);
    }

    return ctx;
  }

  public void finishDecode(IoSession session, ProtocolDecoderOutput out)
    throws Exception
  {
  }

  public void dispose(IoSession session)
    throws Exception
  {
    Context ctx = (Context)session.getAttribute(this.CONTEXT);

    if (ctx != null)
      session.removeAttribute(this.CONTEXT);
  }

  private void decodeAuto(Context ctx, IoSession session, IoBuffer in, ProtocolDecoderOutput out)
    throws CharacterCodingException, ProtocolDecoderException
  {
    int matchCount = ctx.getMatchCount();

    int oldPos = in.position();
    int oldLimit = in.limit();

    while (in.hasRemaining()) {
      byte b = in.get();
      boolean matched = false;

      switch (b)
      {
      case 13:
        matchCount++;
        break;
      case 10:
        matchCount++;
        matched = true;
        break;
      default:
        matchCount = 0;
      }

      if (matched)
      {
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
          try
          {
            writeText(session, buf.getString(ctx.getDecoder()), out);
          } catch(Exception ex){
        	  System.out.println("出错了！");
        	  ex.printStackTrace();
          }finally {
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

  private void decodeNormal(Context ctx, IoSession session, IoBuffer in, ProtocolDecoderOutput out)
    throws CharacterCodingException, ProtocolDecoderException
  {
    int matchCount = ctx.getMatchCount();

    int oldPos = in.position();
    int oldLimit = in.limit();

    while (in.hasRemaining()) {
      byte b = in.get();

      if (this.delimBuf.get(matchCount) == b) {
        matchCount++;

        if (matchCount == this.delimBuf.limit())
        {
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
            try
            {
              writeText(session, buf.getString(ctx.getDecoder()), out);
            } catch(MalformedInputException ex0){
            	logger.error("字节序列不符合"+charset.name()+"编码要求：HexDump("+buf.getHexDump()+")");
            } catch(UnmappableCharacterException ex1){
            	logger.error("字符原始编码非"+charset.name()+",解码失败:HexDump("+buf.getHexDump()+")");
            }finally {
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
      else {
        in.position(Math.max(0, in.position() - matchCount));
        matchCount = 0;
      }

    }

    in.position(oldPos);
    ctx.append(in);

    ctx.setMatchCount(matchCount);
  }

  protected void writeText(IoSession session, String text, ProtocolDecoderOutput out)
  {
    out.write(text);
  }

  private class Context
  {
    private final CharsetDecoder decoder;
    private final IoBuffer buf;
    private int matchCount = 0;

    private int overflowPosition = 0;

    private Context(int bufferLength)
    {
      this.decoder = MyTextLineDecoder.this.charset.newDecoder();
      this.buf = IoBuffer.allocate(bufferLength).setAutoExpand(true);
    }

    public CharsetDecoder getDecoder() {
      return this.decoder;
    }

    public IoBuffer getBuffer() {
      return this.buf;
    }

    public int getOverflowPosition() {
      return this.overflowPosition;
    }

    public int getMatchCount() {
      return this.matchCount;
    }

    public void setMatchCount(int matchCount) {
      this.matchCount = matchCount;
    }

    public void reset() {
      this.overflowPosition = 0;
      this.matchCount = 0;
      this.decoder.reset();
    }

    public void append(IoBuffer in) {
      if (this.overflowPosition != 0) {
        discard(in);
      } else if (this.buf.position() > MyTextLineDecoder.this.maxLineLength - in.remaining()) {
        this.overflowPosition = this.buf.position();
        this.buf.clear();
        discard(in);
      } else {
        getBuffer().put(in);
      }
    }

    private void discard(IoBuffer in) {
      if (2147483647 - in.remaining() < this.overflowPosition)
        this.overflowPosition = 2147483647;
      else {
        this.overflowPosition += in.remaining();
      }

      in.position(in.limit());
    }
  }
}