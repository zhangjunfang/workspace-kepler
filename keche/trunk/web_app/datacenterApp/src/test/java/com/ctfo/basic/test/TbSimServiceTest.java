package com.ctfo.basic.test;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.basic.beans.TbSim;
import com.ctfo.basic.service.TbSimService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;

import junit.framework.TestCase;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：sim卡单元测试<br>
 * 描述：sim卡单元测试<br>
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
 * <td>2014年5月27日</td>
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
public class TbSimServiceTest extends TestCase {

	TbSimService tbSimService = (TbSimService) BaseTest.getClassPath().getBean("tbSimService");

	/**
	 * 查询Sim卡列表
	 */
	public void testFindSimByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		param.setEqual(equal);
		equal.put("entId", "201");
		equal.put("centerCode", "100001");
		PaginationResult<TbSim> result = tbSimService.findSimByParamPage(param);
		List<TbSim> list = (List<TbSim>) result.getData();
		for (TbSim tbSim : list) {
			System.out.println(tbSim.getCommaddr());
		}
		System.out.println(result.getTotalCount());
	}

}
