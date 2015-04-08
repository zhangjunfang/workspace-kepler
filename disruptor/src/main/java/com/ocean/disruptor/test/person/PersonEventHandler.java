package com.ocean.disruptor.test.person;

import com.lmax.disruptor.EventHandler;

/**
 * 消费事件处理处理器
 */
public class PersonEventHandler implements EventHandler<PersonEvent> {

	public PersonEventHandler() {
		// DataSendHelper.start();
	}

	@Override
	public void onEvent(PersonEvent event, long sequence, boolean endOfBatch)
			throws Exception {
		Person person = event.getPerson();
		System.out.println("name = " + person.getName() + ", age = "
				+ person.getAge() + ", gender = " + person.getGender()
				+ ", mobile = " + person.getMobile());
	}

}