package com.ocean;

import java.io.FileDescriptor;
import java.net.InetAddress;

class ParkManager extends SecurityManager {
	@Override
	public void checkCreateClassLoader() {
	}

	@Override
	public void checkAccess(Thread g) {
	}

	@Override
	public void checkAccess(ThreadGroup g) {
	}

	@Override
	public void checkExit(int status) {
	}

	@Override
	public void checkExec(String cmd) {
	}

	@Override
	public void checkLink(String lib) {
	}

	@Override
	public void checkRead(FileDescriptor fd) {
	}

	@Override
	public void checkRead(String file) {
	}

	@Override
	public void checkRead(String file, Object context) {
	}

	@Override
	public void checkWrite(FileDescriptor fd) {
	}

	@Override
	public void checkWrite(String file) {
	}

	@Override
	public void checkDelete(String file) {
	}

	@Override
	public void checkConnect(String host, int port) {
	}

	@Override
	public void checkConnect(String host, int port, Object context) {
	}

	@Override
	public void checkListen(int port) {
	}

	@Override
	public void checkAccept(String host, int port) {
	}

	@Override
	public void checkMulticast(InetAddress maddr) {
	}

	@Override
	public void checkMulticast(InetAddress maddr, byte ttl) {
	}

	@Override
	public void checkPropertiesAccess() {
	}

	@Override
	public void checkPropertyAccess(String key) {
	}

	public void checkPropertyAccess(String key, String def) {
	}

	@Override
	public boolean checkTopLevelWindow(Object window) {
		return true;
	}

	@Override
	public void checkPrintJobAccess() {
	}

	@Override
	public void checkSystemClipboardAccess() {
	}

	@Override
	public void checkAwtEventQueueAccess() {
	}

	@Override
	public void checkPackageAccess(String pkg) {
	}

	@Override
	public void checkPackageDefinition(String pkg) {
	}

	@Override
	public void checkSetFactory() {
	}

	@SuppressWarnings("rawtypes")
	@Override
	public void checkMemberAccess(Class clazz, int which) {
	}

	@Override
	public void checkSecurityAccess(String provider) {
	}
}