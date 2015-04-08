package com.huangjie.dubbo_Service.service.impl;

import com.huangjie.dubbo_Service.service.IProcessData;

public class ProcessDataImpl implements IProcessData {  
  
    /*  
     * @see com.xxx.bubbo.provider.IProcessData#deal(java.lang.String) 
     */  
    @Override  
    public String deal(String data) {  
        try {  
            Thread.sleep(1000);  
        } catch (InterruptedException e) {  
            e.printStackTrace();  
        }  
        return "Finished:" + data;  
    }  
}  