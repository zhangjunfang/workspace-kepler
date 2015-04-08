package com.ctfo.mgdbser.analy;


import com.ctfo.mgdb.beans.Message;
import com.ctfo.mgdb.beans.Record;

/**
 * 
 * 内部协议解析接口
 * @author huangjincheng
 *
 */
public interface IAnalyseService {
	public Record dealPacket(Message message);
}
