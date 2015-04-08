package com.ctfo.sas.service.test;

import org.apache.cxf.jaxws.JaxWsProxyFactoryBean;

import com.ctfo.sas.service.MessageForward;
import com.ctfo.sas.service.bean.Message;
import com.ctfo.threedes.ThreeDES;

public class MyClientHttp {
	private static final String address = "http://192.168.2.135/cpcs/services/MessageForwardYt?wsdl"; 
	 
    public static void main(String[] args) throws Exception { 
        JaxWsProxyFactoryBean factoryBean = new JaxWsProxyFactoryBean(); 
        factoryBean.setAddress(address); 
        factoryBean.setServiceClass(MessageForward.class); 
        Object obj = factoryBean.create(); 
        MessageForward userService = (MessageForward) obj; 
         
        Message msg = new Message();
        msg.setServiceStationSap(ThreeDES.Encrypt3DES("0f6b0bab-958f-490b-8aed-60e3f01934f7", "0000104332", "UTF-8"));
        msg.setRequestTime(ThreeDES.Encrypt3DES("0f6b0bab-958f-490b-8aed-60e3f01934f7", "ssNNs", "UTF-8"));
        msg.setBillNumber(ThreeDES.Encrypt3DES("0f6b0bab-958f-490b-8aed-60e3f01934f7", "ssSSSs", "UTF-8"));
        msg.setBillType(ThreeDES.Encrypt3DES("0f6b0bab-958f-490b-8aed-60e3f01934f7", "sssZXCVAS", "UTF-8"));
        msg.setOpType(ThreeDES.Encrypt3DES("0f6b0bab-958f-490b-8aed-60e3f01934f7", "sssFG", "UTF-8"));
        
        System.out.println(userService.messageForwardYt(msg)); 
    } 
}
