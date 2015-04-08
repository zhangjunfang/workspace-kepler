package com.ctfo.sys.test;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.test.BaseTest;
import com.ctfo.sys.beans.TbSpRole;
import com.ctfo.sys.service.TbSpRoleService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 角色管理测试<br>
 * 描述： 角色管理测试<br>
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
 * <td>2014-5-15</td>
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
public class TbSpRoleServiceTest extends BaseTest {

	TbSpRoleService tbSpRoleService = (TbSpRoleService) BaseTest.getClassPath().getBean("tbSpRoleService");

	/**
	 * 添加角色
	 */
	public void testAddRole() {
		TbSpRole tbSpRole = new TbSpRole();
		tbSpRole.setRoleName("xhtest1234");
		tbSpRole.setRoleType("0");
		tbSpRole.setEntId("200");
		tbSpRole.setRoleDesc("mem");
		tbSpRole.setCreateBy("1234");
		tbSpRole.setEnableFlag("1");
		tbSpRole.setRoleStatus(0);
		tbSpRole.setCenterCode("100001");
		String functionId = "FG_MEMU_HOMEPAGE,FG_MEMU_MONITOR,FG_MEMU_STATIC_SELECT_DISPATCH,FG_MEMU_STATIC_SELECT_DISPATCH_EMP,FG_MEMU_STATIC_SELECT_DISPATCH_INFO,FG_MEMU_STATIC_SELECT_PIC,FG_MEMU_STATIC_SELECT_PIC_OVERLOAD,FG_MEMU_STATIC_SELECT_PIC_LOCAL,FG_MEMU_STATIC_SELECT_PIC_LOCAL_EXP,FG_MEMU_STATIC_SELECT_PIC_LOCAL_D,FG_MEMU_SPANNED_STATISTICS,FG_MEMU_OPERATIONS_TRIGGER,FG_MEMU_OPERATIONS_TRIGGER_CLEAR,FG_MEMU_OPERATIONS_TRIGGER_SET,FG_MEMU_OPERATIONS_TRIGGER_SEND,FG_MEMU_REALTIME_MONITOR,FG_MEMU_MONITOR_PHOTOGRAPH,FG_MEMU_MONITOR_FACILITY,FG_MEMU_MONITOR_LOCUS,FG_MEMU_MONITOR_LOCUS_EXPORT,FG_MEMU_MONITOR_LOCUS_INFO,FG_MEMU_SAFE_LOCUS,FG_MEMU_MONITOR_VIEW_OVERLOAD,FG_MEMU_MONITOR_AREA_TIME,FG_MEMU_MONITOR_BATCH,FG_MEMU_MONITOR_BATCH_VIEW,FG_MEMU_MONITOR_MARK,FG_MEMU_MONITOR_DISPATCH,FG_MEMU_MONITOR_AREA,FG_MEMU_MONITOR_OBJSEARCH,FG_MEMU_MONITOR_PHOTOGRAPH_OVERLOAD,FG_MEMU_MONITOR_MAP_INFO,FG_MEMU_MONITOR_BATCH_EMPHASIS,FG_MEMU_MONITOR_MONITOR,FG_MEMU_MONITOR_VIEW,FG_MEMU_MONITOR_NAME,FG_MEMU_MONITOR_ALARM,FG_MEMU_MONITOR_ALARM_MESSAGE,FG_MEMU_MONITOR_ALARM_PHOTOGRAPH,FG_MEMU_MONITOR_ALARM_REMOVE_ALARM,FG_MEMU_MONITOR_ALARM_MONITOR,FG_MEMU_MONITOR_ALARM_DUAL,FG_MEMU_MONITOR_BATCH_PHOTOGRAPH,FG_MEMU_MONITOR_EMPHASIS,FG_MEMU_OPERATIONS_TRACKANALYSIS,FG_MEMU_OPERATIONS_TRACKANALYSIS_QUERY,FG_MEMU_OPERATIONS_TRACKANALYSIS_EXPORT,FG_MEMU_OPERATIONS_STATION,FG_MEMU_OPERATIONS_STATION_UPDATE,FG_MEMU_OPERATIONS_STATION_DEL,FG_MEMU_OPERATIONS_STATION_ADD,FG_MEMU_OPERATIONS_DATA_LINE,FG_MEMU_OPERATIONS_DATA_LINE_BINGING_STATION,FG_MEMU_OPERATIONS_DATA_LINE_D,FG_MEMU_OPERATIONS_DATA_LINE_INFO,FG_MEMU_OPERATIONS_DATA_LINE_BINGING,FG_MEMU_OPERATIONS_DATA_LINE_U,FG_MEMU_OPERATIONS_DATA_LINE_UNBING,FG_MEMU_OPERATIONS_DATA_LINE_C,FG_MEMU_OPERATIONS_FENCE,FG_MEMU_OPERATIONS_FENCE_INFO,FG_MEMU_OPERATIONS_FENCE_D,FG_MEMU_OPERATIONS_FENCE_U,FG_MEMU_OPERATIONS_FENCE_UNBIND,FG_MEMU_OPERATIONS_FENCE_C,FG_MEMU_OPERATIONS_FENCE_BINGING,FG_MEMU_OPERATIONS_FENCE_VBINGING,FG_MEMU_SAFE_LOCUS_AREA";
		tbSpRole.setFunctionId(functionId);
		// tbSpRoleService.addRole(tbSpRole);
	}

	/**
	 * 修改角色
	 */
	public void testModifyRole() {
		TbSpRole tbSpRole = new TbSpRole();
		tbSpRole.setRoleId("3254154646033512557");
		tbSpRole.setRoleName("xhtest1");
		tbSpRole.setRoleDesc("mem1");
		tbSpRole.setUpdateBy("4321");
		tbSpRole.setCenterCode("100001");
		String functionId = "CS_MEMU_3GVIDEO,CS_MEMU_3GVIDEO_MONITOR,CS_MEMU_3GVIDEO_PLAY,CS_MEMU_PARAMETER,CS_MEMU_PARAMETER_TRIGGER_PHOTO,CS_MEMU_PARAMETER_TRIGGER_PHOTO_VIEW,CS_MEMU_PARAMETER_TRIGGER_PHOTO_BATCH_SET,CS_MEMU_PARAMETER_TRIGGER_PHOTO_RESEND,CS_MEMU_PARAMETER_TRIGGER_PHOTO_F,CS_MEMU_PARAMETER_TRIGGER_PHOTO_SET,CS_MEMU_PARAMETER_TRIGGER_PHOTO_BATCH_CLEAR,CS_MEMU_PARAMETER_TRIGGER_PHOTO_CLEAR";
		tbSpRole.setFunctionId(functionId);
		tbSpRoleService.modifyRole(tbSpRole);
	}

	/**
	 * 删除角色
	 */
	public void testDeleteRole() {
		TbSpRole tbSpRole = new TbSpRole();
		tbSpRole.setRoleId("3221382249282834838");
		tbSpRole.setUpdateBy("1");
		tbSpRole.setCenterCode("100001");
		PaginationResult<TbSpRole> result = tbSpRoleService.deleteRole(tbSpRole);
		System.out.println(result.getResultJudge());
	}

	/**
	 * 查询用户信息详情
	 */
	public void testFindRoleDetail() {
		Map<String, String> map = new HashMap<String, String>();
		map.put("roleId", "1");
		map.put("centerCode", "110001");
		TbSpRole tbSpRole = tbSpRoleService.findRoleDetail(map);
		System.out.println(tbSpRole.getRoleName());
	}

	/**
	 * 查询用户列表
	 */
	public void testFindRoleByParamPage() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		Map<String, String> like = new HashMap<String, String>();
		param.setEqual(equal);
		param.setLike(like);
		equal.put("entId", "200"); // 101
		equal.put("centerCode", "11"); // 110001
		equal.put("treeType", "1");
		// like.put("roleName", "演示");
		PaginationResult<TbSpRole> result = tbSpRoleService.findRoleByParamPage(param);
		List<TbSpRole> list = (List<TbSpRole>) result.getData();
		for (TbSpRole tbSpRole : list) {
			System.out.println(tbSpRole.getEntName());
		}
		System.out.println(result.getTotalCount());
	}

	/**
	 * 增加用户时，查询角色列表
	 */
	public void testFindRoleList() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, Object> equal = new HashMap<String, Object>();
		param.setEqual(equal);
		equal.put("entId", "200");
		equal.put("centerCode", "100001");
		List<TbSpRole> list = tbSpRoleService.findRoleList(param);
		for (TbSpRole tbSpRole : list) {
			System.out.println(tbSpRole.getRoleName());
		}
	}
}
