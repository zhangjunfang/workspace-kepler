package com.ctfo.storage.command.task;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.command.model.TbOrganization;
import com.ctfo.storage.command.util.TaskAdapter;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： command <br>
 * 功能： 分中心组织id同步任务<br>
 * 描述： 分中心组织id同步任务<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
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
 * <td>2014-8-14</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
@SuppressWarnings("static-access")
public class OrgEntIdUrlSyncTask extends TaskAdapter {

	/** 日志 */
	private static Logger log = LoggerFactory.getLogger(OrgEntIdUrlSyncTask.class);

	public OrgEntIdUrlSyncTask() {
		this.setName("OrgEntIdUrlSyncTask");
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.storage.command.util.TaskAdapter#init()
	 */
	@Override
	public void init() {
		this.setName("OrgEntIdUrlSyncTask");
		log.info("分中心组织id同步程序启动初始化！");
		this.execute();
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.storage.command.util.TaskAdapter#execute()
	 */
	@Override
	public void execute() {
		long start = System.currentTimeMillis();
		PreparedStatement stat = null;
		ResultSet rs = null;
		try {
			conn = mysql.getConnection();
			stat = conn.prepareStatement(config.get("sql_allOrgEntId"));
			rs = stat.executeQuery();

			List<TbOrganization> list = new ArrayList<TbOrganization>(); // 查询所有分中心组织id
			while (rs.next()) {
				TbOrganization org = new TbOrganization();
				org.setEntId(rs.getString("ENT_ID"));
				org.setParentId(rs.getString("PARENT_ID"));
				org.setEnableFlag(rs.getString("ENABLE_FLAG"));
				org.setCenterCode(rs.getString("CENTER_CODE"));
				list.add(org);
			}
			for (TbOrganization org : list) {
				if ("1".equals(org.getEnableFlag())) { // 有效组织
					StringBuffer buffValue = new StringBuffer();
					buffValue.append("#").append(org.getEntId()).append("#");
					this.getOrgParentId(org, buffValue, list); // 查询组织上级父节点
					org.setEntIdUrl(buffValue.toString());
				}
			}
			this.updateMySqlOrgEntIdUrl(list); // 更新entIdUrl字段
			log.info("--OrgEntIdUrlSyncTask--分中心组织id同步任务执行完成, ---同步数据:[{}]条, 正常处理:[{}]条, ---总耗时:[{}]秒 ", list.size(), list.size(), (System.currentTimeMillis() - start) / 1000.0);
			// System.out.println("--OrgEntIdUrlSyncTask--分中心组织id同步任务执行完成, ---同步数据:[{" + list.size() + "}]条, 正常处理:[{" + list.size() + "}]条, ---总耗时:[{" + (System.currentTimeMillis() - start) / 1000.0 + "}]秒 ");
		} catch (SQLException e) {
			log.error("分中心组织id同步异常:" + e.getMessage(), e);
		} finally {
			try {
				if (null != rs) {
					rs.close();
				}
				if (null != stat) {
					stat.close();
				}
				if (null != conn) {
					conn.close();
				}
			} catch (Exception e2) {
				log.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	/**
	 * 更新entIdUrl字段
	 * 
	 * @param list
	 */
	private void updateMySqlOrgEntIdUrl(List<TbOrganization> list) {
		log.info("开始更新分中心组织表TB_ORGANIZATION 数据");
		long start = System.currentTimeMillis();
		PreparedStatement stat = null;
		// int batchSize = 10000; // 批量大小
		// int count = 0;
		try {
			conn = mysql.getConnection();
			stat = conn.prepareStatement(config.get("sql_updateEntIdUrl"));
			conn.setAutoCommit(false); // 设置手动提交
			for (TbOrganization org : list) {
				stat.setString(1, org.getEntIdUrl());
				stat.setString(2, org.getEntId());
				stat.setString(3, org.getCenterCode());
				stat.addBatch();
				// if (++count % batchSize == 0) {
				// stat.executeBatch();
				// conn.commit();
				// stat.clearBatch();
				// }
			}
			stat.executeBatch();
			conn.commit();
			// System.out.println("更新分中心组织表TB_ORGANIZATION 数据:[{" + list.size() + "}]条, 正常处理:[{" + list.size() + "}]条, ---总耗时:[{" + (System.currentTimeMillis() - start) / 1000.0 + "}]秒 ");
			log.info("更新分中心组织表TB_ORGANIZATION 数据:[{}]条, 正常处理:[{}]条, ---总耗时:[{}]秒 ", list.size(), list.size(), (System.currentTimeMillis() - start) / 1000.0);
		} catch (SQLException e) {
			log.error("分中心组织id更新异常:" + e.getMessage(), e);
		} finally {
			try {
				if (null != stat) {
					stat.close();
				}
				if (null != conn) {
					conn.close();
				}
			} catch (Exception e2) {
				log.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	/**
	 * 递归查询父id
	 * 
	 * @param parentId
	 * @param buff
	 * @param list
	 */
	private void getOrgParentId(TbOrganization org, StringBuffer buff, List<TbOrganization> list) {
		try {
			for (TbOrganization tbOrg : list) {
				if (tbOrg.getEntId().equals(org.getParentId()) && tbOrg.getCenterCode().equals(org.getCenterCode())) {
					buff.append(tbOrg.getEntId()).append("#");
					this.getOrgParentId(tbOrg, buff, list);
				}
			}
		} catch (Exception e) {
			log.error("递归组织id异常:" + e.getMessage(), e);
		}
	}
}
