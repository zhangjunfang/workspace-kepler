package com.ctfo.sas.service;

import javax.jws.WebService;

import com.ctfo.sas.service.bean.Message;
import com.ctfo.sas.service.bean.MessageResponse;

/*
 * 消息转发服务
 */
@WebService
public interface MessageForward {
	
	/**
	 * 消息转发---宇通
	 * @throws Exception 
	 */
	MessageResponse messageForwardYt(Message message) throws Exception;
}
