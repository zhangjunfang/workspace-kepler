package com.ctfo.mcc.mq;

import java.util.TimerTask;

public class TimerMananger extends TimerTask{
	public AreaUpdateServie areaUpdateServie = null;
	
	public TimerMananger(AreaUpdateServie areaUpdateServie){
		this.areaUpdateServie = areaUpdateServie;
	}

	@Override
	public void run() {
		// 解除电子围栏绑定及发下车机解绑指令.
		areaUpdateServie.removeAreaBunding();
	}
}
