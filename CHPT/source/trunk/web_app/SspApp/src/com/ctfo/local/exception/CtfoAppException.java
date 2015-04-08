package com.ctfo.local.exception;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

public class CtfoAppException extends RuntimeException
{
  private static final long serialVersionUID = -3585616564496392771L;
  private static Log log = LogFactory.getLog(CtfoAppException.class);

  private String displayMessage = "系统异常，请重试!";

  private CtfoExceptionLevel level = CtfoExceptionLevel.systemError;

  private String message = "系统异常，请联系系统管理员!";

  public CtfoAppException()
  {
  }

  public CtfoAppException(String message)
  {
    if (message != null)
      this.message = message;
  }

  public CtfoAppException(String message, CtfoExceptionLevel l)
  {
    appExceptionInit(l, null);
    if (message != null)
      this.message = message;
  }

  public CtfoAppException(String message, CtfoExceptionLevel l, String displayMessage)
  {
    appExceptionInit(l, displayMessage);
    if (message != null)
      this.message = message;
  }

  public CtfoAppException(Throwable cause)
  {
    super(cause);
    this.message = cause.getMessage();
    log.debug(cause.fillInStackTrace());
  }

  public CtfoAppException(Throwable cause, CtfoExceptionLevel l, String displayMessage)
  {
    super(cause);
    appExceptionInit(l, displayMessage);
    this.message = cause.getMessage();
    log.debug(cause.fillInStackTrace());
  }

  public String getMessage()
  {
    return this.message;
  }

  public String getDisplayMessage()
  {
    return this.displayMessage;
  }

  public CtfoExceptionLevel getLevel()
  {
    return this.level;
  }

  private void appExceptionInit(CtfoExceptionLevel l, String displayMessage)
  {
    if (l != null)
      switch (l.ordinal())
      {
      case 1:
        this.level = CtfoExceptionLevel.systemError;
        if (displayMessage != null) {
          this.displayMessage = displayMessage; return;
        }
        this.displayMessage = "系统异常，请联系系统管理员!";

        break;
      case 2:
        this.level = CtfoExceptionLevel.recoverError;
        if (displayMessage != null) {
          this.displayMessage = displayMessage; return;
        }
        this.displayMessage = "操作异常，请重试!";
      }
  }
}