package com.ctfo.kypt;

import net.neoremind.sshxcute.core.ConnBean;
import net.neoremind.sshxcute.core.SSHExec;
import net.neoremind.sshxcute.exception.TaskExecFailException;
import net.neoremind.sshxcute.task.CustomTask;
import net.neoremind.sshxcute.task.impl.ExecCommand;

public class Test {

	public static void main(String[] args) {
		
		ConnBean cb = new ConnBean("192.168.100.51", "root","zjxl2012#");
		
		
		SSHExec ssh = SSHExec.getInstance(cb);          
		// Connect to server
		ssh.connect();
		CustomTask sampleTask = new ExecCommand("mkdir /home/kcpt/yangxgtest");
		try {
			ssh.exec(sampleTask);
		} catch (TaskExecFailException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

}
