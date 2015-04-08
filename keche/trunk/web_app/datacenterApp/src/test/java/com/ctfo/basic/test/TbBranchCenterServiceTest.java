package com.ctfo.basic.test;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.basic.beans.TbBranchCenter;
import com.ctfo.basic.service.TbBranchCenterService;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;

public class TbBranchCenterServiceTest extends BaseTest {

	TbBranchCenterService tbBranchCenterService = (TbBranchCenterService) BaseTest.getClassPath().getBean("tbBranchCenterService");

	/**
	 * 查询分中心列表
	 */
	public void testFindBranchCenterByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		param.setEqual(equal);
		// equal.put("dvrserCity", "110108");
		PaginationResult<TbBranchCenter> result = tbBranchCenterService.findBranchCenterByParamPage(param);
		List<TbBranchCenter> list = (List<TbBranchCenter>) result.getData();
		for (TbBranchCenter tbBranchCenter : list) {
			System.out.println(tbBranchCenter.getCenterName());
		}
		System.out.println(result.getTotalCount());
	}

	/**
	 * 添加分中心
	 */
	public void testAddBranchCenter() {
		TbBranchCenter tbBranchCenter = new TbBranchCenter();
		tbBranchCenter.setId("5");
		tbBranchCenter.setCenterCode("1");
		tbBranchCenter.setCenterName("aa");
		tbBranchCenter.setEnableFlag("1");
		// tbBranchCenterService.addBranchCenter(tbBranchCenter);
	}

	/**
	 * 刪除分中心（逻辑删除）
	 */
	public void testDeleteBranchCenter() {
		TbBranchCenter tbBranchCenter = new TbBranchCenter();
		tbBranchCenter.setId("51");
		tbBranchCenterService.deleteBranchCenter(tbBranchCenter);
	}
}
