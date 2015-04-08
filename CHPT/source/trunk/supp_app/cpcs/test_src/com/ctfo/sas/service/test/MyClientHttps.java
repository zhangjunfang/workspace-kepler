package com.ctfo.sas.service.test;


import java.io.File;
import java.io.FileInputStream;
import java.security.KeyStore;

import javax.net.ssl.KeyManager;
import javax.net.ssl.KeyManagerFactory;
import javax.net.ssl.TrustManager;
import javax.net.ssl.TrustManagerFactory;

import org.apache.cxf.configuration.jsse.TLSClientParameters;
import org.apache.cxf.configuration.security.FiltersType;
import org.apache.cxf.endpoint.Client;
import org.apache.cxf.frontend.ClientProxy;
import org.apache.cxf.jaxws.JaxWsProxyFactoryBean;
import org.apache.cxf.transport.http.HTTPConduit;

import com.ctfo.sas.service.MessageForward;
import com.ctfo.sas.service.bean.Message;
import com.ctfo.threedes.ThreeDES;

public class MyClientHttps {
    //https://119.57.151.34:18443/cpcs/services/MessageForwardYt?wsdl
	//https://localhost/cpcs/services/MessageForwardYt?wsdl
    private static final String address = "https://119.57.151.34:18443/cpcs/services/MessageForwardYt?wsdl";  
 
    public static void main(String[] args) throws Exception { 
        JaxWsProxyFactoryBean factoryBean = new JaxWsProxyFactoryBean(); 
        factoryBean.setAddress(address); 
        factoryBean.setServiceClass(MessageForward.class); 
        Object obj = factoryBean.create(); 
        MessageForward userService = (MessageForward) obj; 
         
        configureSSLOnTheClient(userService); 
        Message msg = new Message();
        msg.setServiceStationSap(ThreeDES.Encrypt3DES("0f6b0bab-958f-490b-8aed-60e3f01934f7", "0000152877", "UTF-8"));
        msg.setRequestTime(ThreeDES.Encrypt3DES("0f6b0bab-958f-490b-8aed-60e3f01934f7", "2014-12-09 16:26:40", "UTF-8"));
        msg.setBillNumber(ThreeDES.Encrypt3DES("0f6b0bab-958f-490b-8aed-60e3f01934f7", "ssSSSs", "UTF-8"));
        msg.setBillType(ThreeDES.Encrypt3DES("0f6b0bab-958f-490b-8aed-60e3f01934f7", "ServiceOrder", "UTF-8"));
        msg.setOpType(ThreeDES.Encrypt3DES("0f6b0bab-958f-490b-8aed-60e3f01934f7", "2", "UTF-8"));
        System.out.println(userService.messageForwardYt(msg)); 
    } 
 
    private static void configureSSLOnTheClient(Object obj) { 
       File file = new File("e:/zjhlypt.jks"); 
         
        Client client = ClientProxy.getClient(obj); 
        HTTPConduit httpConduit = (HTTPConduit) client.getConduit(); 
 
        try { 
            TLSClientParameters tlsParams = new TLSClientParameters(); 
            tlsParams.setDisableCNCheck(true); 
 
            KeyStore keyStore = KeyStore.getInstance("JKS"); 
            String password = "zjhlypt"; 
            String storePassword = "zjhlypt"; 
             
            keyStore.load(new FileInputStream(file), storePassword.toCharArray()); 
            TrustManagerFactory trustFactory = TrustManagerFactory.getInstance(TrustManagerFactory.getDefaultAlgorithm()); 
            trustFactory.init(keyStore); 
            TrustManager[] trustManagers = trustFactory.getTrustManagers(); 
            tlsParams.setTrustManagers(trustManagers); 
 
            keyStore.load(new FileInputStream(file), storePassword.toCharArray()); 
            KeyManagerFactory keyFactory = KeyManagerFactory.getInstance(KeyManagerFactory.getDefaultAlgorithm()); 
            keyFactory.init(keyStore, password.toCharArray()); 
            KeyManager[] keyManagers = keyFactory.getKeyManagers(); 
            tlsParams.setKeyManagers(keyManagers); 
             
            FiltersType filtersTypes = new FiltersType(); 
            filtersTypes.getInclude().add(".*_EXPORT_.*"); 
            filtersTypes.getInclude().add(".*_EXPORT1024_.*"); 
            filtersTypes.getInclude().add(".*_WITH_DES_.*"); 
            filtersTypes.getInclude().add(".*_WITH_NULL_.*"); 
            filtersTypes.getExclude().add(".*_DH_anon_.*"); 
            tlsParams.setCipherSuitesFilter(filtersTypes); 
 
            httpConduit.setTlsClientParameters(tlsParams); 
        } catch (Exception e) { 
            e.printStackTrace(); 
        } 
    } 
}
