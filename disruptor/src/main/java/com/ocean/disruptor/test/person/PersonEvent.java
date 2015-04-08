package com.ocean.disruptor.test.person;

import com.lmax.disruptor.EventFactory;

/**
 * 消费事件
 */
public class PersonEvent {

	private Person person;

	public Person getPerson() {
		return person;
	}

	public void setPerson(Person person) {
		this.person = person;
	}

	public final static EventFactory<PersonEvent> EVENT_FACTORY = new EventFactory<PersonEvent>() {
		public PersonEvent newInstance() {
			return new PersonEvent();
		}
	};
}