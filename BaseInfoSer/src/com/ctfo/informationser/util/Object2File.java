package com.ctfo.informationser.util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.Serializable;
import java.util.ArrayList;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

/**
 * 
 * 对象和文件相互转换类型。
 * 
 * @version 1.0
 * 
 * @author 王鹏
 * @since JDK1.6
 */
@SuppressWarnings("rawtypes")
public class Object2File {

	private static Log log = LogFactory.getLog(Object2File.class);

	/**
	 * 保存java对象实例化为文件
	 * 
	 * @param object
	 *            java实例化对象
	 * @param filedir
	 *            文件目录结构
	 * @return
	 */
	public static Integer saveObject2File(Serializable object, String filedir) {
		File file = new File(filedir);
		FileOutputStream output = null;
		ObjectOutputStream obj = null;

		int result = StaticSession.IO_STAT_OK;
		if (object == null)
			return StaticSession.IO_OBJ_NULL;

		String ch = filedir.substring(0, filedir.lastIndexOf("/") + 1);

		if (mklocaldir(ch) == -1) {
			return StaticSession.IO_MKDIR_FAIL;
		}

		if (!file.exists())
			try {
				file.createNewFile();
			} catch (IOException e1) {
				// TODO Auto-generated catch block
				log.debug("在文件[" + filedir + "]创建文件失败:" + e1.toString());
				return StaticSession.IO_CREATE_FAIL;
			}

		try {
			output = new FileOutputStream(file);
			obj = new ObjectOutputStream(output);
			obj.writeObject(object);
		} catch (Exception e) {
			log.debug("根据文件[" + filedir + "]保存数据到文件失败:" + e.toString());
			result = StaticSession.IO_WRITE_FAIL;
		} finally {
			if (obj != null)
				try {
					obj.close();
					obj = null;
				} catch (IOException e) {
					e.toString();
				}
			if (output != null)
				try {
					output.close();
					output = null;
				} catch (IOException e) {
					e.toString();
				}
		}
		file = null;
		object = null;
		return result;
	}

	/**
	 * 加载文件为java实例化对象
	 * 
	 * @param fileDir
	 *            文件存储目录
	 * @return
	 */
	public static Serializable loadObject2File(String fileDir) {
		File file = new File(fileDir);
		FileInputStream input = null;
		ObjectInputStream obj = null;
		Serializable object = null;
		try {
			if (!file.exists()) {
				return null;
			}
			input = new FileInputStream(file);
			obj = new ObjectInputStream(input);
			object = (Serializable) obj.readObject();
		} catch (IOException e) {

			log.debug("根据文件[" + fileDir + "]获取数据失败:" + e.toString());

		} catch (ClassNotFoundException e) {
			log.debug("根据文件[" + fileDir + "]获取数据失败:" + e.toString());
		} finally {
			if (obj != null)
				try {
					obj.close();
					obj = null;
				} catch (IOException e) {
					e.toString();
				}
			if (input != null)
				try {
					input.close();
					input = null;
				} catch (IOException e) {
					e.toString();
				}
		}
		file = null;
		return object;
	}

	public static int listDirCount(String path) {
		File file = new File(path);
		if (file.isDirectory() && file.canRead()) {
			return file.list().length;
		} else {
			return 0;
		}
	}

	public static Integer deleteFile(String path, String fileName) {
		File file = new File(path + "/" + fileName);
		if (file.delete()) {
			return 0;
		} else {
			return StaticSession.IO_DEL_FAIL;
		}

	}

	/**
	 * 文件拷贝
	 * 
	 * @param filefrom
	 *            需要拷贝的源文件
	 * @param fileto
	 *            拷贝到目的文件
	 * @param rewrite
	 *            是否覆盖 true为是；false为否
	 * @return
	 */
	public static boolean copyFile(java.io.File filefrom, java.io.File fileto,
			boolean rewrite) {
		if (!filefrom.exists()) {
			log.debug("文件不存在");
			return false;
		}
		if (!filefrom.isFile()) {
			log.debug("不能够复制文件夹");
			return false;
		}
		if (!filefrom.canRead()) {
			log.debug("不能够读取需要复制的文件");
			return false;
		}
		if (!fileto.getParentFile().exists()) {
			try{fileto.getParentFile().mkdirs();}catch(Exception e){e.printStackTrace();}
		}
		if (fileto.exists() && rewrite) {
			try{fileto.delete();}catch(Exception e){e.printStackTrace();}
			
		}
		java.io.FileInputStream fosfrom = null;
		java.io.FileOutputStream fosto = null;
		try {
			fosfrom = new java.io.FileInputStream(filefrom);
			fosto = new FileOutputStream(fileto);
			byte bt[] = new byte[1024];
			int c;
			while ((c = fosfrom.read(bt)) > 0) {
				fosto.write(bt, 0, c);
			}
			return true;
		} catch (Exception ex) {
			ex.printStackTrace();
			return false;
		} finally {
			if (fosfrom != null) {
				try {
					fosfrom.close();
				} catch (Exception e) {
				}
			}
			if (fosto != null) {
				try {
					fosto.close();
				} catch (Exception e) {
				}
			}
		}

	}

	/**
	 * 删除文件夹及内部子文件夹下所有文件
	 * 
	 * @param folderPath
	 *            绝对路径
	 */
	public static void delFolder(String folderPath) {
		try {
			delAllFile(folderPath); // 删除完里面所有内容
			String filePath = folderPath;
			filePath = filePath.toString();
			java.io.File myFilePath = new java.io.File(filePath);
			try{myFilePath.delete();}catch(Exception e){e.printStackTrace();} // 删除空文件夹
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	// 删除指定文件夹下所有文件
	// param path 文件夹完整绝对路径

	/**
	 * 删除指定文件夹下所有文件
	 * 
	 * @param folderPath
	 *            文件夹完整绝对路径
	 */
	public static boolean delAllFile(String path) {
		boolean flag = false;
		File file = new File(path);
		if (!file.exists()) {
			return flag;
		}
		if (!file.isDirectory()) {
			return flag;
		}
		String[] tempList = file.list();
		File temp = null;
		for (int i = 0; i < tempList.length; i++) {
			if (path.endsWith(File.separator)) {
				temp = new File(path + tempList[i]);
			} else {
				temp = new File(path + File.separator + tempList[i]);
			}
			if (temp.isFile()) {
				try{temp.delete();}catch(Exception e){e.printStackTrace();}
			}
			if (temp.isDirectory()) {
				delAllFile(path + "/" + tempList[i]);// 先删除文件夹里面的文件
				delFolder(path + "/" + tempList[i]);// 再删除空文件夹
				flag = true;
			}
		}
		return flag;
	}


	public static Integer listFiles(String strPath) {
		ArrayList list = new ArrayList();
		refreshFileList(list, strPath);
		Integer i = list.size();
		list.clear();
		return i;
	}

	@SuppressWarnings({ "unchecked", "unused" })
	private static void refreshFileList(ArrayList filelist, String strPath) {
		File dir = new File(strPath);
		File[] files = dir.listFiles();
		if (files == null)
			return;
		for (int i = 0; i < files.length; i++) {
			if (files[i].isDirectory()) {
				refreshFileList(filelist, files[i].getAbsolutePath());
			} else {
				String strFileName = files[i].getAbsolutePath().toLowerCase();
				filelist.add(files[i].getAbsolutePath());
			}
		}
	}

	public static boolean delAllFileNoDir(String path) {
		boolean flag = false;
		File file = new File(path);
		if (!file.exists()) {
			return flag;
		}
		if (!file.isDirectory()) {
			return flag;
		}
		ArrayList<String> list = new ArrayList<String>();
		refreshFileList(list, path);
		System.out.println(list);
		int i = 0;
		for (String string : list) {
			i++;
			File f = new File(string);
			if (!f.canWrite()) {
				System.out.println("文件不能删除");
				continue;
			} else if (f.getName().equals("server.log")) {
				System.out.println("文件不能删除");
				continue;
			}
			try{f.delete();}catch(Exception e){e.printStackTrace();}
		}
		if (i == list.size()) {
			flag = true;
		}
		return flag;
	}

	public static boolean copyFile(String from, String to) {
		java.io.File filefrom = new java.io.File(from);
		java.io.File fileto = new java.io.File(to);
		return copyFile(filefrom, fileto, true);
	}

	public static Integer deleteFile(String fileName) {
		File file = new File(fileName);
		if (file.delete()) {
			return 0;
		} else {
			return StaticSession.IO_DEL_FAIL;
		}

	}

	/**
	 * 建立本地目录
	 * 
	 * @param path
	 *            目录
	 */
	public synchronized static int mklocaldir(String path) {
		File file = new File(path);

		if (!file.isDirectory())
			if (new File(path).mkdirs())
				return 0;
			else
				return -1;
		else
			return 0;
	}

	public static void main(String[] args) {
		// TaskObjBean taskObjBean = (TaskObjBean)
		// loadObject2File("c:\\taskObjBean");
		// List<TargetObjBean> list = taskObjBean.getTconf();
		// System.out.println(taskObjBean.getDirection());
		// List<String> list = new ArrayList<String>();
		// list.add(null);
		// System.out.println(list);
		// Object2File.saveObject2File(new ArrayList<String>(),
		// "e:/sss/sss/sse/ee.xml");
		Object2File.deleteFile("D:/aa/aa");
		// System.out.println(Object2File.mklocaldir("c:/sss/se/ss/"));
		// for (TargetObjBean targetObjBean : list) {
		// HashMap<String, List<TargetSqlBean>> hm = targetObjBean.getOrgSql();
		// Set<String> set = hm.keySet();
		// Iterator<String> it = set.iterator();
		// System.out.println(targetObjBean.getResourcesid());
		//
		// while (it.hasNext()) {
		// String str = it.next();
		// List<TargetSqlBean> li = hm.get(str);
		// for (TargetSqlBean targetSqlBean : li) {
		// System.out.println(str);
		// System.out.println(targetSqlBean.getSql("insert"));
		// System.out.println(targetSqlBean.getFieldList("insert"));
		//
		// System.out.println("----------------------------");
		// System.out.println(targetSqlBean.getSql("update"));
		// System.out.println(targetSqlBean.getFieldList("update"));
		//
		// System.out.println("----------------------------");
		// System.out.println(targetSqlBean.getSql("delete"));
		// System.out.println(targetSqlBean.getFieldList("delete"));
		//
		// System.out.println("----------------------------");
		//
		// System.out.println(targetSqlBean.getSql("select"));
		// System.out.println(targetSqlBean.getFieldList("select"));
		//
		// System.out.println("----------------------------");
		// }
		// }
		// System.out.println("----------------------------------------");
		// }

	}
}
