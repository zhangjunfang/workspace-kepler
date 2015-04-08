package com.ctfo.hessianproxy.hessian;


import java.io.IOException;
import java.net.URL;
import java.net.URLConnection;

  
import com.caucho.hessian.client.HessianProxyFactory;


public class CtfoHessianProxyFactory extends HessianProxyFactory {

	/*private int connectTimeOut = 5000;

    private int readTimeOut = 10000;

    public int getConnectTimeOut() {
        return connectTimeOut;
    }

    public void setConnectTimeOut(int connectTimeOut) {
        this.connectTimeOut = connectTimeOut;
    }

    public int getReadTimeOut() {
        return readTimeOut;
    }

    public void setReadTimeOut(int readTimeOut) {
        this.readTimeOut = readTimeOut;
    }
*/
    protected URLConnection openConnection(URL url) throws IOException {
        URLConnection conn = url.openConnection();
        super.setChunkedPost(false);
		this.setChunkedPost(false);// 使用Nginx代理WebService时，要禁止检查Post状态，否则不能做负载均衡
        conn.setDoOutput(true);
        /* if (this.connectTimeOut > 0) {
            conn.setConnectTimeout(this.connectTimeOut);
        }
        if (this.readTimeOut > 0) {
            conn.setReadTimeout(this.readTimeOut);
        }
        conn.setRequestProperty("Content-Type", "x-application/hessian");*/
      
        return conn;
    }


}
