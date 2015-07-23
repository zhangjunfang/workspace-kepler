package com.fujitsu.fnst.asm.utility;

import org.objectweb.asm.MethodAdapter;
import org.objectweb.asm.MethodVisitor;

/**
 * This class is used to modify the <init> method for enhanced class.
 * @author paul
 *
 */
public class ModifyInitMethodAdapter extends MethodAdapter {
	private String className;

	public ModifyInitMethodAdapter(MethodVisitor mv, String name) {
		super(mv);
		this.className = name;
	}

	@Override
	public void visitMethodInsn(int opcode, String owner, String name,
			String desc) {
		if (name.equals(AddImplementClassAdapter.INTERNAL_INIT_METHOD_NAME)) {
			mv.visitMethodInsn(opcode, className.replace(".", "/"), name, desc);
		}
	}

}
