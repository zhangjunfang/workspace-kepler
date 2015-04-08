package com.ctfo.basic.test;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import junit.framework.TestCase;

import com.ctfo.basic.beans.TbVehicle;
import com.ctfo.basic.service.TbVehicleService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：<br>
 * 描述：<br>
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
 * <td>2014年5月29日</td>
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
public class TbVehicleServiceTest extends TestCase {

	TbVehicleService tbVehicleService = (TbVehicleService) BaseTest.getClassPath().getBean("tbVehicleService");

	/**
	 * 查询车辆列表
	 */
	public void testFindVehicleByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		Map<String, String> like = new HashMap<String, String>();
		param.setEqual(equal);
		param.setLike(like);
		equal.put("entId", "#200#");
		equal.put("centerCode", "11");
		like.put("vehicleNo", "12");
		PaginationResult<TbVehicle> result = tbVehicleService.findVehicleByParamPage(param);
		List<TbVehicle> list = (List<TbVehicle>) result.getData();
		for (TbVehicle tbVehicle : list) {
			System.out.print(tbVehicle.getVehicleNo());
			System.out.print("\t");
			System.out.println(tbVehicle.getParentEntName());
		}
		System.out.println(result.getTotalCount());
	}
}
