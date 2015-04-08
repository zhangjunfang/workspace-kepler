package com.huangjie.dubbo_Service;

import org.springframework.context.support.ClassPathXmlApplicationContext;

public class DubboProviderMain {  
  
    /** 
     * @Title main 
     * @Description  
     * @Author weizhi2018 
     * @param args 
     * @throws 
     */  
  
    public static void main(String[] args) throws Exception {  
        ClassPathXmlApplicationContext context = new ClassPathXmlApplicationContext(  
                new String[]{"applicationProvider.xml"});  
        context.start();
  
        System.out.println("Press any key to exit.");  
        System.in.read();  
    }  
}  