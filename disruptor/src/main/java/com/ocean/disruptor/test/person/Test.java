package com.ocean.disruptor.test.person;
public class Test {
    /**
     * @param args
     */
    public static void main(String[] args) {
        PersonHelper.start();
        for(int i=0 ; i<20; i++){
            Person p = new Person("jbgtwang"+i, i , "男", "1234566"+i);

            //生产者生产数据
            PersonHelper.produce(p);
        }
    }

}