package com.ctfo.analy.addin;

import com.ctfo.analy.beans.VehicleMessageBean;
import com.lingtu.xmlconf.XmlConf;

/**
 * Title:        轨迹分析类接口
 * Description:  轨迹分析类实现接口
 */
public interface PacketAnalyser
{
  /**
   * 初始化并启动分析线程
   */
  public void initAnalyser(int nId,XmlConf config, String nodeName) throws Exception;

  /**
   * 增加一个需要分析的异步报文类
   * 
   */
  public void addPacket(VehicleMessageBean vehicleMessage);

  
  /**获得待分析报文的大小，调试使用*/
  public int getPacketsSize();

  /**
   * 线程停止
   */
  public void endAnalyser();
}
