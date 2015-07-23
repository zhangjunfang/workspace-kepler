package com.fujitsu.fnst.asm.utility;

import java.io.IOException;

import org.objectweb.asm.ClassAdapter;
import org.objectweb.asm.ClassReader;
import org.objectweb.asm.ClassWriter;
import org.objectweb.asm.MethodVisitor;
import org.objectweb.asm.Opcodes;

/**
 * This class is used to add implement classes' bytecode to the original class.
 * The new class is extends the original class,and name with
 * orginalClass+"$PROXY";
 * 
 * @author paul
 * 
 */
public class AddImplementClassAdapter extends ClassAdapter {
	public static final String INTERNAL_INIT_METHOD_NAME = "<init>";
	private ClassWriter classWriter;
	private Class<?>[] implementClasses;
	private String originalClassName;
	private String enhancedClassName;

	public AddImplementClassAdapter(String enhancedClassName,
			Class<?> targetClass, ClassWriter writer,
			Class<?>... implementClasses) {
		super(writer);
		this.classWriter = writer;
		this.implementClasses = implementClasses;
		this.originalClassName = targetClass.getName();
		this.enhancedClassName = enhancedClassName;
	}

	@Override
	public void visit(int version, int access, String name, String signature,
			String superName, String[] interfaces) {
		// 修改类名，将 Java 代码中类的名称替换为虚拟机中使用的形式
		//将增强类设置为待增强类的子类
		cv.visit(version, Opcodes.ACC_PUBLIC,
				enhancedClassName.replace('.', '/'), signature, name,
				interfaces);
	}

	@Override
	public MethodVisitor visitMethod(int access, String name, String desc,
			String signature, String[] exceptions) {
		/*visitMethod 方法中需要判断如果是构造方法就通过 ModifyInitMethodAdapter 修改构造方法。
		其他方法直接返回 null 丢弃（因为增强类已经从待增强类中继承了这些方法，
		所以这些方法不需要在增强类中再出现一遍）*/
		if (INTERNAL_INIT_METHOD_NAME.equals(name)) {
			MethodVisitor mv = classWriter.visitMethod(access,
					INTERNAL_INIT_METHOD_NAME, desc, signature, exceptions);
			return new ModifyInitMethodAdapter(mv, originalClassName);
		}
		return null;
	}

	@Override
	public void visitEnd() {
		//使用 ImplementClassAdapter 与 ClassWriter 将实现类的内容添加到增强类中
		for (Class<?> clazz : implementClasses) {
			try {
				// 逐个将实现类的内容添加到增强类中（clazz.getName()）
				ClassReader reader = new ClassReader(clazz.getName());
				ClassAdapter adapter = new ImplementClassAdapter(classWriter);
				reader.accept(adapter, 0);
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
		cv.visitEnd();
	}
}
