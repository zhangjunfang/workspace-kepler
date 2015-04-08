package com.ctfo.basic.test;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.basic.beans.TbDvrser;
import com.ctfo.basic.service.TbDvrserService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：3G视频服务器单元测试<br>
 * 描述：3G视频服务器单元测试<br>
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
public class TbDvrserServiceTest extends BaseTest {

	TbDvrserService tbDvrserService = (TbDvrserService) BaseTest.getClassPath().getBean("tbDvrserService");

	/**
	 * 查询3G视频服务器列表
	 */
	public void testFindDvrserByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		Map<String, String> like = new HashMap<String, String>();
		param.setEqual(equal);
		param.setLike(like);
		equal.put("centerCode", "100001");
		like.put("dvrserName", "yypt");
		like.put("dvrserIp", "105");
		PaginationResult<TbDvrser> result = tbDvrserService.findDvrserByParamPage(param);
		List<TbDvrser> list = (List<TbDvrser>) result.getData();
		for (TbDvrser tbDvrser : list) {
			System.out.println(tbDvrser.getDvrMakerCode());
		}
		System.out.println(result.getTotalCount());
	}
}
