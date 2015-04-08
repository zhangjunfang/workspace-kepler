/**
 * 
 */
package com.ctfo.storage.media.util;

/**
 * @author zjhl
 *
 */


public class Converter
{
  double casm_rr;
  long casm_t1;
  long casm_t2;
  double casm_x1;
  double casm_y1;
  double casm_x2;
  double casm_y2;
  double casm_f;

  private double yj_sin2(double x)
  {
    int ff = 0;
    if (x < 0.0D) {
      x = -x;
      ff = 1;
    }
    int cc = (int)(x / 6.28318530717959D);
    double tt = x - cc * 6.28318530717959D;
    if (tt > 3.141592653589793D) {
      tt -= 3.141592653589793D;
      if (ff == 1)
        ff = 0;
      else if (ff == 0)
        ff = 1;
    }
    x = tt;
    double ss = x;
    double s2 = x;
    tt *= tt;
    s2 *= tt;
    ss -= s2 * 0.166666666666667D;
    s2 *= tt;
    ss += s2 * 0.00833333333333333D;
    s2 *= tt;
    ss -= s2 * 0.000198412698412698D;
    s2 *= tt;
    ss += s2 * 2.75573192239859E-006D;
    s2 *= tt;
    ss -= s2 * 2.50521083854417E-008D;
    if (ff == 1) {
      ss = -ss;
    }
    return ss;
  }

  private double Transform_yj5(double x, double y)
  {
    double tt = 300.0D + 1.0D * x + 2.0D * y + 0.1D * x * x + 0.1D * x * y + 0.1D * 
      Math.sqrt(Math.sqrt(x * x));

    tt = tt + 
      (20.0D * yj_sin2(18.849555921538762D * x) + 20.0D * yj_sin2(6.283185307179588D * x)) * 
      0.6667D;

    tt = tt + 
      (20.0D * yj_sin2(3.141592653589794D * x) + 40.0D * yj_sin2(1.047197551196598D * x)) * 
      0.6667D;

    tt = tt + 
      (150.0D * yj_sin2(0.2617993877991495D * x) + 300.0D * yj_sin2(0.1047197551196598D * x)) * 
      0.6667D;

    return tt;
  }

  private double Transform_yjy5(double x, double y)
  {
    double tt = -100.0D + 2.0D * x + 3.0D * y + 0.2D * y * y + 0.1D * x * y + 0.2D * 
      Math.sqrt(Math.sqrt(x * x));

    tt = tt + 
      (20.0D * yj_sin2(18.849555921538762D * x) + 20.0D * yj_sin2(6.283185307179588D * x)) * 
      0.6667D;

    tt = tt + 
      (20.0D * yj_sin2(3.141592653589794D * y) + 40.0D * yj_sin2(1.047197551196598D * y)) * 
      0.6667D;

    tt = tt + 
      (160.0D * yj_sin2(0.2617993877991495D * y) + 320.0D * yj_sin2(0.1047197551196598D * y)) * 
      0.6667D;

    return tt;
  }

  private double Transform_jy5(double x, double xx)
  {
    double a = 6378245.0D;
    double e = 0.00669342D;
    double n = Math.sqrt(1.0D - e * yj_sin2(x * 0.0174532925199433D) * 
      yj_sin2(x * 0.0174532925199433D));
    n = xx * 180.0D / (a / n * Math.cos(x * 0.0174532925199433D) * 3.1415926D);

    return n;
  }

  private double Transform_jyj5(double x, double yy)
  {
    double a = 6378245.0D;
    double e = 0.00669342D;

    double mm = 1.0D - e * yj_sin2(x * 0.0174532925199433D) * 
      yj_sin2(x * 0.0174532925199433D);
    double m = a * (1.0D - e) / (mm * Math.sqrt(mm));

    return yy * 180.0D / (m * 3.1415926D);
  }

//  private double r_yj(){
//    int casm_a = 314159269;
//    int casm_c = 453806245;
//    return 0.0D;
//  }

  private double random_yj()
  {
    int casm_a = 314159269;
    int casm_c = 453806245;
    this.casm_rr = (casm_a * this.casm_rr + casm_c);
    int t = (int)(this.casm_rr / 2.0D);
    this.casm_rr -= t * 2;
    this.casm_rr /= 2.0D;

    return this.casm_rr;
  }

  private void IniCasm(long w_time, long w_lng, long w_lat)
  {
    this.casm_t1 = w_time;
    this.casm_t2 = w_time;
    int tt = (int)(w_time / 0.357D);
    this.casm_rr = (w_time - tt * 0.357D);
    if (w_time == 0L)
      this.casm_rr = 0.3D;
    this.casm_x1 = w_lng;
    this.casm_y1 = w_lat;
    this.casm_x2 = w_lng;
    this.casm_y2 = w_lat;
    this.casm_f = 3.0D;
  }

  public Point wgtochina_lb(int wg_flag, long wg_lng, long wg_lat, int wg_heit, int wg_week, long wg_time)
  {
    Point point = null;

    if (wg_heit > 5000) {
      return point;
    }
    double x_l = wg_lng;
    x_l /= 3686400.0D;
    double y_l = wg_lat;
    y_l /= 3686400.0D;

    if (x_l < 72.004000000000005D) {
      return point;
    }
    if (x_l > 137.8347D) {
      return point;
    }
    if (y_l < 0.8293D) {
      return point;
    }
    if (y_l > 55.827100000000002D) {
      return point;
    }

    if (wg_flag == 0) {
      IniCasm(wg_time, wg_lng, wg_lat);

      point = new Point();
      point.setLatitude(wg_lng);
      point.setLongitude(wg_lat);

      return point;
    }

    this.casm_t2 = wg_time;
    double t1_t2 = (this.casm_t2 - this.casm_t1) / 1000.0D;
    if (t1_t2 <= 0.0D) {
      this.casm_t1 = this.casm_t2;
      this.casm_f += 1.0D;
      this.casm_x1 = this.casm_x2;
      this.casm_f += 1.0D;
      this.casm_y1 = this.casm_y2;
      this.casm_f += 1.0D;
    }
    else if (t1_t2 > 120.0D) {
      if (this.casm_f == 3.0D) {
        this.casm_f = 0.0D;
        this.casm_x2 = wg_lng;
        this.casm_y2 = wg_lat;
        double x1_x2 = this.casm_x2 - this.casm_x1;
        double y1_y2 = this.casm_y2 - this.casm_y1;
        double casm_v = Math.sqrt(x1_x2 * x1_x2 + y1_y2 * y1_y2) / t1_t2;
        if (casm_v > 3185.0D) {
          return point;
        }
      }

      this.casm_t1 = this.casm_t2;
      this.casm_f += 1.0D;
      this.casm_x1 = this.casm_x2;
      this.casm_f += 1.0D;
      this.casm_y1 = this.casm_y2;
      this.casm_f += 1.0D;
    }

    double x_add = Transform_yj5(x_l - 105.0D, y_l - 35.0D);
    double y_add = Transform_yjy5(x_l - 105.0D, y_l - 35.0D);
    double h_add = wg_heit;

    x_add = x_add + h_add * 0.001D + yj_sin2(wg_time * 0.0174532925199433D) + 
      random_yj();
    y_add = y_add + h_add * 0.001D + yj_sin2(wg_time * 0.0174532925199433D) + 
      random_yj();

    point = new Point();

    point.setX(((x_l + Transform_jy5(y_l, x_add)) * 3686400.0D));
    point.setY(((y_l + Transform_jyj5(y_l, y_add)) * 3686400.0D));

    return point;
  }

  public Point getEncryPoint(double x, double y)
  {
    Point point = new Point();

    double x1 = x * 3686400.0D;
    double y1 = y * 3686400.0D;

    int gpsWeek = 0;
    double gpsWeekTime = 0.0D;

    int gpsHeight = 0;
    point = wgtochina_lb(1, (int)x1, (int)y1, gpsHeight, gpsWeek, 
      (int)gpsWeekTime);

    if (point != null)
    {
      double tempx = point.getX();
      double tempy = point.getY();
      tempx /= 3686400.0D;
      tempy /= 3686400.0D;

      point = new Point();
      point.setX(tempx);
      point.setY(tempy);
    }

    return point;
  }

  public static void main(String[] args) {
    Converter conver = new Converter();
    Point point = conver.getEncryPoint(121.6394866878215D, 38.920661548505002D);
    System.out.println(point.getLatitude() + "," + point.getLongitude());
    System.out.println(point.getX() + "," + point.getY());
  }
}
