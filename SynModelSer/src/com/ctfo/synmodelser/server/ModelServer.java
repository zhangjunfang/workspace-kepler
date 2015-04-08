package com.ctfo.synmodelser.server;

import java.io.File;
import java.util.HashMap;
import java.util.Map;

import com.ctfo.synmodelser.db.regstatus.RegStatusManager;
import com.ctfo.synmodelser.http.HttpSynServiceImpl;
import com.ctfo.synmodelser.util.StaticSession;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SynModelSer <br>
 * 功能： 根据用户的配置信息加载DB数据到Memcached中<br>
 * 描述：根据用户的配置信息加载DB数据到Memcached中 ，要求用户输入配置参数路径文件，文件为properties文件的完整路径，配置数据库信息及Memcached地址。<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>Feb 14, 2012</td>
 * <td>wuqj</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author wuqj
 * @since JDK1.6
 */
public class ModelServer {
	public static void main(String[] args) {
		if (args.length <= 1 || args.length < 2) {
			if (args.length == 1 && "-H".equalsIgnoreCase(args[0])) {
				System.out.println("-F 文件全路径,必须指定");
				System.out.println("-M 同步DB数据到Memcached");
				System.out.println("-U 更新车辆，终端注册信息,参数说明:更新状态,车牌号,车牌颜色,终端ID");
				System.out.println("-H 查看帮助信息");
			} else {
				System.out.println("-H 查看帮助信息");
				System.exit(0);
			}
		} else {
			ModelBulider bulider = null;
			Map<String, String> map = new HashMap<String, String>();
			for (int i = 1; i <= args.length; i++) {
				String key = args[i - 1];
				if (key.equals(StaticSession.SYN_CONFIG_FILE_PATH)) {
					map.put(key, args[i]);
				} else if (key.equals(StaticSession.SYN_TYPE_UPDATE_DATA)) {
					map.put(key, args[i]);
				} else {
					if (key.startsWith("-"))
						map.put(key, "");
				}
			}
			String filePath = "";
			System.out.println(map);
			System.out.println(map.get(StaticSession.SYN_CONFIG_FILE_PATH));
			if (map.get(StaticSession.SYN_CONFIG_FILE_PATH) == null) {
				System.out.println("请输入配置文件地址。。。");
				System.exit(0);
			} else {
				filePath = map.get(StaticSession.SYN_CONFIG_FILE_PATH);
				File f = new File(filePath);
				if (!f.exists()) {
					System.out.println("输入的文件路径无效。。。。");
					System.exit(0);
				}
			}
			if (map.get(StaticSession.SYN_TYPE_MEM_DATA) != null) {
				bulider = new HttpSynServiceImpl(filePath);
				bulider.bulider();
				bulider.getSynData();
			}
			if (map.get(StaticSession.SYN_TYPE_UPDATE_DATA) != null) {
				String value = map.get(StaticSession.SYN_TYPE_UPDATE_DATA);
				bulider = new RegStatusManager(filePath, value);
				bulider.bulider();
				bulider.getSynData();
			}

		}

	}
}
