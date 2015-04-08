/**
 * 
 */
package com.ctfo.storage.media.util;

/**
 * @author zjhl
 *
 */
public class Point
{
  private long longitude;
  private long latitude;
  private double x;
  private double y;

  public void setX(double x)
  {
    this.x = x;
  }

  public void setY(double y)
  {
    this.y = y;
  }

  public double getX()
  {
    return this.x;
  }

  public double getY()
  {
    return this.y;
  }

  public void setLongitude(long longitude)
  {
    this.longitude = longitude;
  }

  public void setLatitude(long latitude)
  {
    this.latitude = latitude;
  }

  public long getLongitude()
  {
    return this.longitude;
  }

  public long getLatitude()
  {
    return this.latitude;
  }
}
