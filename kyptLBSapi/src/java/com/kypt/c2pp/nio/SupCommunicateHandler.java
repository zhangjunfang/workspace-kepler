package com.kypt.c2pp.nio;

import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.back.SupCommandQueueMap;
import com.kypt.c2pp.buffer.UpCommandBuffer;
import com.kypt.c2pp.inside.msg.InsideMsg;
import com.kypt.c2pp.inside.msg.InsideMsgFactory;
import com.kypt.c2pp.inside.msg.InsideMsgResp;
import com.kypt.c2pp.inside.msg.InsideMsgStatusCode;
import com.kypt.c2pp.inside.msg.resp.ActiveTestResp;
import com.kypt.c2pp.inside.msg.resp.LoginResp;
import com.kypt.c2pp.inside.processor.IInsideProcessor;
import com.kypt.c2pp.inside.processor.InsideProcessorMap;
import com.kypt.configuration.C2ppCfg;
import com.kypt.nio.client.AbstractNioHandler;

public class SupCommunicateHandler extends AbstractNioHandler<SupCommService> {

	private Logger log = LoggerFactory.getLogger(SupCommunicateHandler.class);

	private static final String NAME = "<SupCommunicateHandler>";

	private boolean isLogined;

	private SupActiveTest activeTest;

	public SupCommunicateHandler(SupCommService nioService) {
		super(nioService);
	}

	public void connectionClosed(SupCommService nioService) throws Exception {
		log.info(NAME + "the session between ota and "
				+ nioService.getRemoteAddress() + " is closed.");
		isLogined = false;
		cancelActiveTest();
		// for(int i=0;i<Constant.array.size();i++){
		// if(nioService.getRemoteAddress().equals(Constant.array.get(i))){
		// Constant.array.remove(i);
		// }
		// }
		// System.out.println("array:"+Constant.array.size());
		nioService.reconnect();
	}

	/*
	 * 建立连接同时发送登录消息
	 */
	public void connectionOpen(SupCommService nioService) throws Exception {

		byte[] bytes = (InsideMsgFactory.createLoginReq().toString())
				.getBytes(C2ppCfg.props.getProperty("superviseEncoding"));
		nioService.send(bytes);
		StringBuilder sb = new StringBuilder();
		sb.append("send a login request message.");
		if (log.isDebugEnabled()) {
			sb.append(new String(bytes));
		}
		log.info(NAME + sb.toString());
	}

	@SuppressWarnings("unchecked")
	public void doMsg(SupCommService nioService, byte[] buf) throws Exception {
		try {
			/*
			 * while (!SpringBootStrap.getInstance().isInit()) {
			 * System.out.println(SpringBootStrap.getInstance().isInit());
			 * Thread.sleep(1000); // System.out.println("-----------"); }
			 */

			String msgStr = new String(buf, C2ppCfg.props
					.getProperty("superviseEncoding"));

			if (msgStr != null && msgStr.length() > 0) {
				String message[] = msgStr.split("\\s+");

				log.info(message.length + ":::");

				for (int i = 0; i < message.length; i++) {
					System.out.println((i + 1) + "=" + message[i]);
				}

				String command = getCommand(message);

				if (!isLogined) {
					if (command.equals(LoginResp.COMMAND)) {
						// doLogin(nioService, message[0], command);
					}
				} else {
					activeTest.clear();
					if (command.equals(ActiveTestResp.COMMAND)) {
						activeTest.doActiveTestResp();
					} else {
						IInsideProcessor processor = InsideProcessorMap
								.getInstance().getProcessor(command);
						if (processor != null) {
							InsideMsg msg = processor.parse(message);
							// processor.valid(msg);
							processor.handle(msg, nioService);
						} else {
							log.error(NAME
									+ "there is no processor for command:"
									+ command);
						}
					}
				}
			}

		} catch (Throwable t) {
			log.error(NAME + "there is a exception when deal with the message:"
					+ new String(buf), t);
		}
	}

	@SuppressWarnings("unchecked")
	private void doLogin(SupCommService nioService, String[] message,
			String command) throws Exception {

		/*
		 * while (!SpringBootStrap.getInstance().isInit()) { Thread.sleep(5000); //
		 * System.out.println("-----------"); }
		 */

		IInsideProcessor processor = InsideProcessorMap.getInstance()
				.getProcessor(command);
		if (processor != null) {
			InsideMsg msg = processor.parse(message);
			// processor.valid(msg);
			if (InsideMsgStatusCode.STATUS_LOGIN_COMMON_OP.equals(msg
					.getStatusCode())
					|| InsideMsgStatusCode.STATUS_LOGIN_MIDDLE_OP.equals(msg
							.getStatusCode())
					|| InsideMsgStatusCode.STATUS_LOGIN_ADMIN_OP.equals(msg
							.getStatusCode())) {
				isLogined = true;
				log
						.info(NAME + "receive login response."
								+ " ------Login "
								+ nioService.getRemoteAddress()
								+ " successfully------");
				startActiveTest(nioService);// 开启心跳包发送线程

				startSendUpCommand(nioService);// 开启向监管平台发送数据线程

				// startVgSendDataService(nioService);
			} else if (InsideMsgStatusCode.STATUS_LOGIN_PASSWORD_ERROR
					.equals(msg.getStatusCode())) {
				log
						.error(NAME
								+ " password in the login message is wrong. Login Failed!!!");
			} else if (InsideMsgStatusCode.STATUS_LOGIN_USER_ONLINE.equals(msg
					.getStatusCode())) {
				log.error(NAME + " user online. Login Failed!!!");
			} else if (InsideMsgStatusCode.STATUS_LOGIN_USER_LOGOFF.equals(msg
					.getStatusCode())) {
				log.error(NAME + " user cancelled. Login Failed!!!");
			} else if (InsideMsgStatusCode.STATUS_LOGIN_USER_UNEXIST.equals(msg
					.getStatusCode())) {
				log.error(NAME + "user disable. Login Failed!!!");
			} else if (InsideMsgStatusCode.STATUS_LOGIN_QUERY_FAILURE
					.equals(msg.getStatusCode())) {
				log.error(NAME + "user search failure. Login Failed!!!");
			} else if (InsideMsgStatusCode.STATUS_LOGIN_DATABASE_ERROR
					.equals(msg.getStatusCode())) {
				log.error(NAME
						+ " paltform database exception. Login Failed!!!");
			} else {
				log.error(NAME + "status in the login response is "
						+ msg.getStatusCode() + ".Login Failed!!!");
			}
		} else {
			log.error(NAME + "there is no processor for command:" + command);
		}
	}

	private void startActiveTest(SupCommService nioService) throws Exception {
		activeTest = new SupActiveTest(nioService, InsideMsgFactory
				.createActiveTestReq().toString().getBytes(
						C2ppCfg.props.getProperty("superviseEncoding")));
		activeTest.start();
	}

	private void startSendUpCommand(SupCommService nioService) throws Exception {
		System.out.print("init send up command thread!");

		BlockingQueue<InsideMsgResp> bq = new LinkedBlockingQueue<InsideMsgResp>();

		SupCommandQueueMap.getInstance().put(nioService.getRemoteAddress(), bq);

		// 启动消息发送线程
		SupUpCommandSend sus = new SupUpCommandSend(nioService);
		Thread upSend = new Thread(sus);
		upSend.start();

		// 启动消息分发线程
		UpCommandBuffer upBuffer = UpCommandBuffer.getInstance();
		if (upBuffer.isShutdownFlag()) {
			Thread worker = new Thread(upBuffer);
			worker.start();
		}

	}

	private void cancelActiveTest() {
		if (activeTest != null) {
			activeTest.cancel();
		}
	}

	private String getCommand(String msg) {
		int i = msg.indexOf("\\s+");
		String command = msg.substring(0, i);
		return command;
	}

	private String getCommand(String msg[]) {
		String command = "";
		if (msg[0].equals("LOGI") || msg[0].equals("LACK")
				|| msg[0].equals("LOGO") || msg[0].equals("NOOP")
				|| msg[0].equals("NOOP_ACK")) {
			command = msg[0];
		} else if (msg[0].equals("CAITS") && msg.length >= 5) {
			String codingLine = msg[4];
			String codingType = msg[5].substring(msg[5].indexOf(":") + 1,
					msg[5].indexOf(","));
			if (codingLine.equals("D_REQD")) {
				if (codingType.equals("1")) {// 多媒体数据检索
					command = "0x8802";
				} else if (codingType.equals("2")) {// 多媒体数据上传
					command = "0x0801";
				}
			}
			if (codingLine.equals("U_REPT")) {
				if (codingType.equals("0")) {// 位置信息汇报
					command = "0x0200";
				} else if (codingType.equals("32")) {// 提问应答
					command = "0x0302";
				}
			}

			if (codingLine.equals("D_SNDM")) {
				if (codingType.equals("1")) {// 文本信息下发
					command = "0x8300";
				} else if (codingType.equals("5")) {// 提问下发
					command = "0x8302";
				}
			}
			if (codingLine.equals("D_CTLM")) {
				if (codingType.equals("9")) {// 终端监听
					command = "0x8400";
				} else if (codingType.equals("10")) {// 终端拍照
					command = "0x8801";
				}
			}
			if (codingLine.equals("D_GETP")) {
				if (codingType.equals("0")) {// 查询终端参数
					command = "0x8104";
				} else if (codingType.equals("10")) {// 终端拍照
					command = "0x8801";
				}
			}
			if (codingLine.equals("D_SETP")) {
				if (codingType.equals("0")) {// 设置终端参数
					command = "0x8103";
				} else if (codingType.equals("11")) {// 事件设置
					command = "0x8301";
				}
			}

		}
		return command;
	}

	public void doMsg(SupCommService nioService, String message)
			throws Exception {
		try {
			String msgs[] = message.split("\\s+");

			String command = getCommand(msgs);

			if (command != null && command.length() > 0) {
				if (!isLogined) {
					if (command.equals(LoginResp.COMMAND)) {
						doLogin(nioService, msgs, command);
					}
				} else {
					log.info("command::::" + command);
					// activeTest.clear();
					if (command.equals(ActiveTestResp.COMMAND)) {
						activeTest.doActiveTestResp();
					} else {
						IInsideProcessor processor = InsideProcessorMap
								.getInstance().getProcessor(command);
						if (processor != null) {
							InsideMsg msg = processor.parse(msgs);
							// processor.valid(msg);
							processor.handle(msg, nioService);
						} else {
							log.error(NAME
									+ "there is no processor for command:"
									+ command);
						}
						// System.out.println(VehicleCacheManager.vehicleMap.size());
						// System.out.println(VehicleCacheManager.getInstance().getValue("SUSUCTD60A1026622").getModify_time());
					}
				}
			}
		} catch (Throwable t) {
			log.error(NAME + "there is a exception when deal with the message:"
					+ message, t);
		}

	}
}
