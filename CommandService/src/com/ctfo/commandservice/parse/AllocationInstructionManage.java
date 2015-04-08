/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.parse AllocationInstructionManage.java	</li><br>
 * <li>时        间：2013-9-9  下午2:08:00	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.parse;

/*****************************************
 * <li>描        述：指令分配管理		
 * 
 *****************************************/
public class AllocationInstructionManage {
	
	public AllocationInstructionManage(AllocationInstructionThread allocationInstructionThread){
		AllocationInstructionManage.allocationInstructionThread = allocationInstructionThread;
	}
	
	private static AllocationInstructionThread allocationInstructionThread = null;
	
	public static void setAllocationInstruction(AllocationInstructionThread allocationInstructionThread) {
		AllocationInstructionManage.allocationInstructionThread = allocationInstructionThread;
	}
	public static AllocationInstructionThread getAllocationInstructionThread (){
		return allocationInstructionThread;
	}
	
}
