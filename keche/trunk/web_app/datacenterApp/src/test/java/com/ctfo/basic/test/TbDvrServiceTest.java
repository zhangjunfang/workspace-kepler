package com.ctfo.basic.test;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import junit.framework.TestCase;

import com.ctfo.basic.beans.TbDvr;
import com.ctfo.basic.service.TbDvrService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：3G视频终端<br>
 * 描述：3G视频终端<br>
 * 授权 : (C) Copyright (c) 2011<br>
 * 公司 : 北京中交慧联信息科技有限公司<br>
 * -----------------------------------------------------------------------------<br>
 * 修改历史<br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014年5月28日</td>
 * <td>JiTuo</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font><br>
 * 
 * @version 1.0
 * 
 * @author JiTuo
 * @since JDK1.6
 */
public class TbDvrServiceTest extends TestCase {

	TbDvrService tbDvrService = (TbDvrService) BaseTest.getClassPath().getBean("tbDvrService");

	/**
	 * 查询3G视频终端列表
	 */
	public void testFindDvrByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		param.setEqual(equal);
		equal.put("entId", "#1#");
		equal.put("centerCode", "11");
		equal.put("corpProvince", "430000");
		PaginationResult<TbDvr> result = tbDvrService.findDvrByParamPage(param);
		List<TbDvr> list = (List<TbDvr>) result.getData();
		for (TbDvr tbDvr : list) {
			System.out.print(tbDvr.getDvrNo());
			System.out.print("\t");
			System.out.print(tbDvr.getDvrserName());
			System.out.print("\t");
			System.out.print(tbDvr.getMaker());
			System.out.print("\t");
			System.out.print(tbDvr.getDvrSerIp());
			System.out.print("\t");
			System.out.print(tbDvr.getDvrSerPort());
			System.out.print("\t");
			System.out.print(tbDvr.getDvrSimNum());
			System.out.print("\t");
			System.out.print(tbDvr.getCreatName());
			System.out.print("\t");
			System.out.print(tbDvr.getUpdateName());
			System.out.println();
		}
	}

}
