package com.huangjie.dubbo_Consumer;

import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.huangjie.dubbo_Service.service.IProcessData;

public class ConsumerThd implements Runnable {  
  
    /*  
     * @see java.lang.Runnable#run() 
     */  
    @Override  
    public void run() {
        ClassPathXmlApplicationContext context = new ClassPathXmlApplicationContext(  
                new String[]{"applicationConsumer.xml"});  
        context.start();  
  
        IProcessData demoService = (IProcessData) context.getBean("demoService"); // get  
                                                                                // service  
                                                                                // invocation  
        // proxy  
        String hello = demoService.deal("nihao"); // do invoke!  
  
        System.out.println(Thread.currentThread().getName() + " "+hello);  
    }  
    
	public static void main(String[] args) {
		new Thread(new ConsumerThd()).start();
	}
}  