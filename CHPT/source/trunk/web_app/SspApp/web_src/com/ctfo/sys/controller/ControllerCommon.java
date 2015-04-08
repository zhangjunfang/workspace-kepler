package com.ctfo.sys.controller;

import java.util.ArrayList;
import java.util.List;

import com.ctfo.local.obj.FunctionTree;
import com.ctfo.local.obj.OrgTree;
import com.ctfo.sys.beans.SysFunction;
import com.ctfo.sys.beans.TbOrganization;

public class ControllerCommon {

	//递归权限树
	public List<FunctionTree> findSubFunction(List<SysFunction> functionList, String parentId){
		List<FunctionTree> subTreeList = new ArrayList<FunctionTree>();
		for(SysFunction sysFunction : functionList){
			if(parentId.equals(sysFunction.getFunParentId())){
				FunctionTree functionTree = new FunctionTree();
				functionTree.setNODE_TYPE_DRIVER("");
				functionTree.setNODE_TYPE_LINE("");
				functionTree.setNODE_TYPE_TEAM("");
				functionTree.setNODE_TYPE_VEHICLE("");
				functionTree.setENT_TYPE_NOCORP_AND_NOTEAM("");
				functionTree.setNodeId(sysFunction.getFunId());
				functionTree.setText(sysFunction.getFunName());
				functionTree.setIsexpand("true");
				functionTree.setChildren("");
				functionTree.setIschecked(sysFunction.getIschecked());
				functionTree.setId(sysFunction.getFunId());
				functionTree.setParentId(parentId);
				functionTree.setValue("");
				functionTree.setChildrenList(this.findSubFunction(functionList, sysFunction.getFunId()));
				
				subTreeList.add(functionTree);
			}
		}
		return subTreeList;
	}
	
	//递归机构树
	public List<OrgTree> findSubOrg(List<TbOrganization> tbOrganizationList, String parentId){
		List<OrgTree> subTreeList = new ArrayList<OrgTree>();
		for(TbOrganization tbOrganization : tbOrganizationList){
			if(parentId.equals(tbOrganization.getParentId())){
				OrgTree orgTree = new OrgTree();
				orgTree.setNODE_TYPE_DRIVER("");
				orgTree.setNODE_TYPE_LINE("");
				orgTree.setNODE_TYPE_TEAM("");
				orgTree.setNODE_TYPE_VEHICLE("");
				orgTree.setText(tbOrganization.getEntName());
				orgTree.setIsexpand("true");
				orgTree.setENT_TYPE_NOCORP_AND_NOTEAM("");
				orgTree.setId(tbOrganization.getEntId());
				orgTree.setParentId(tbOrganization.getParentId());
				orgTree.setEntType(tbOrganization.getEntType());
				orgTree.setCorpLevel(tbOrganization.getCorpLevel());
				orgTree.setValue(tbOrganization.getCorpCode());
				orgTree.setChildrenList(this.findSubOrg(tbOrganizationList, tbOrganization.getEntId()));
				
				subTreeList.add(orgTree);
			}
		}
		return subTreeList;
	}
	
}
