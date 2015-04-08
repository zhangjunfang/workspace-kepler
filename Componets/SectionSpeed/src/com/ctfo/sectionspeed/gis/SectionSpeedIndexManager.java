package com.ctfo.sectionspeed.gis;

import java.io.File;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.locks.ReentrantLock;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.zjxl.ssl.SectionSLException;
import com.zjxl.ssl.SectionSpeedLimit;

public enum SectionSpeedIndexManager {
	INSTANCE;

	private static SectionSpeedLimit ssl = null;
	private final static ReentrantLock lock = new ReentrantLock(); // 是否正在加载内存
	private static final Logger logger = LoggerFactory.getLogger(SectionSpeedIndexManager.class);
	private static final ScheduledExecutorService scheduled = Executors.newSingleThreadScheduledExecutor();
	private static long lastTime = 0;

	private void checkManager(final String idxPath) {
		scheduled.scheduleAtFixedRate(new Runnable() {
			@Override
			public void run() {
				File f = new File(idxPath);
				long l = f.lastModified();
				if(f.canRead() &&  l > lastTime) {
					reload(idxPath);
					lastTime = l;
					if(logger.isInfoEnabled()) {
						logger.info("checkManager Reload fileName:" + f.getPath() + " lastTime: " + l);
					}
				} else {
					if(logger.isInfoEnabled())
						logger.info("checkManager not Reload fileName:" + f.getPath() + " lastTime: " + l);
				}
			}
			
		}, 1, 12, TimeUnit.SECONDS);
	}
	
	public SectionSpeedLimit getSectionSpeedLimit(final String idxPath) {
		if (null == ssl) {
			boolean b = reload(idxPath);
			checkManager(idxPath);
			if (logger.isInfoEnabled()) {
				logger.info("索引文件加载 : " + b);
			}
		}
		return ssl;
	}

	public boolean reload(final String idxPath) {
		lock.lock(); // block until condition holds
		SectionSpeedLimit sl = new SectionSpeedLimit();
		try {
			if (sl.load(idxPath)) {
				SectionSpeedIndexManager.ssl = sl;
				return true;
			}
		} catch (SectionSLException e) {
			e.printStackTrace();
		} finally {
			lock.unlock();
		}
		return false;
	}
	
	public static void main(String[] args) {
		SectionSpeedIndexManager.INSTANCE.getSectionSpeedLimit("d:/idx.txt");
	}

}
