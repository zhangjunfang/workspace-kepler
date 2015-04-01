package com.ocean.coolhash;

import com.ocean.BeanContext;

public class RunServer {
	public static void main(String[] args) {
		// ���з�ʽ��3������Ϊip���˿ڡ�������������java -cp
		// fourinone.jar; RunServer localhost 2014 8
		BeanContext.startCoolHashServer(args[0], Integer.parseInt(args[1]),
				Integer.parseInt(args[2]), args.length == 4 ? args[3] : null);
	}
}