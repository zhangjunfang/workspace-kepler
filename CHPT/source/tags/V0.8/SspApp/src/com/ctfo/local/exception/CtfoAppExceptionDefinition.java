package com.ctfo.local.exception;

public class CtfoAppExceptionDefinition {
	//公司删除场景
	public static String COM_D_HAVESUBORG = "你要删除的公司已经有组织存在,请删除公司所关联组织";
	
	//组织删除场景
	public static String ORG_D_HAVESUBORG = "请先删除子机构";
	public static String ORG_D_HAVESUBOPERATOR = "你要删除的组织已经有用户存在,请删除组织所关联用户";
	public static String ORG_D_HAVESUBROLE = "请删除机构所关联角色";
	
	//角色添加场景
	public static String OP_A_HAVENAME = "用户登录名称已存在";
	
	//角色删除场景
	public static String ROLE_D_HAVETROPERATOR = "已关联用户";
	
	//角色添加场景
	public static String ROLE_A_HAVENAME = "角色名称已存在";
}
