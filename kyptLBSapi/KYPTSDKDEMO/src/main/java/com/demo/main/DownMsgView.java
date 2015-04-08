package com.demo.main;

import com.kypt.c2pp.buffer.DownCommandBuffer;
import com.kypt.c2pp.inside.msg.InsideMsg;
import com.kypt.c2pp.inside.msg.req.DownTextReq;
import com.kypt.c2pp.inside.msg.req.PhotoGraphReq;
import com.kypt.c2pp.inside.msg.req.TerminalListenerSeq;

	public class DownMsgView extends Thread {
		
		int i=0;

		@Override
		public void run(){

			while (true){
				InsideMsg im=DownCommandBuffer.getInstance().getDownMsg();
				if (null == im ) {
	            	System.out.println("缓冲队列中暂时没有下行数据！");
	                try { 
	                    Thread.sleep(1000*30);
	                } catch (InterruptedException e) {
	                	System.out.println("缓冲队列处理在休眠时出现异常，" + e);
	                }
	                continue;
	            }
				if (im!=null){
					System.out.println(im.getCommand());
					
					if (im.getCommand().equals("0x8300")){//下发文本信息
						DownTextReq dtreq=(DownTextReq)im;
						System.out.println(dtreq.getCommType());
						System.out.println(dtreq.getOemId());
						System.out.println(dtreq.getDeviceNo());
						System.out.println(dtreq.getStatusCode());
						System.out.println(dtreq.getSmsInfo());
						System.out.println(dtreq.getFlagVO().toString());
					}else if (im.getCommand().equals("0x8801")){//拍照
						PhotoGraphReq dtreq=(PhotoGraphReq)im;
						System.out.println(dtreq.getCommType());
						System.out.println(dtreq.getOemId());
						System.out.println(dtreq.getDeviceNo());
						System.out.println(dtreq.getChannelId());
						System.out.println(dtreq.getOptType());
						System.out.println(dtreq.getDpi());
						System.out.println(dtreq.getContrast());
						System.out.println(dtreq.getPicQuality());
						System.out.println(dtreq.getChroma());
						
					}else if (im.getCommand().equals("0x8400")){//监听
						TerminalListenerSeq dtreq=(TerminalListenerSeq)im;
						System.out.println(dtreq.getCommType());
						System.out.println(dtreq.getOemId());
						System.out.println(dtreq.getDeviceNo());
						System.out.println(dtreq.getPhone());
						
					}
				}
				
			}
		}

	}

