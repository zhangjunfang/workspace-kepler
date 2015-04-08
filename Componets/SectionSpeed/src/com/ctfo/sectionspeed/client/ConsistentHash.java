package com.ctfo.sectionspeed.client;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.List;
import java.util.SortedMap;
import java.util.TreeMap;

public class ConsistentHash<S> {
	private final SortedMap<Long, S> circle = new TreeMap<Long, S>();
	private static int NUM_REPS = 160; // 每个机器节点关联的虚拟节点个数

	public ConsistentHash(List<S> nodes, int num_pers) {
		NUM_REPS = num_pers;
		for (S s : nodes) {
			for (int i = 0; i < NUM_REPS / 4; i++) {
				byte[] digest = computeMd5(s.toString() + i);
				for (int h = 0; h < 4; h++) {
					circle.put(hash(digest, h), s);
				}
			}
		}
	}

	public S getPrimary(final String k) {
		return getStringForKey(hash(computeMd5(k), 0));
	}

	private final S getStringForKey(long hash) {
		Long key = hash;
		if (!circle.containsKey(key)) {
			SortedMap<Long, S> tailMap = circle.tailMap(key);
			key = (null == tailMap || tailMap.isEmpty()) ? circle.firstKey() : tailMap.firstKey();
		}
		return circle.get(key);
	}

	private final static long hash(byte[] digest, int nTime) {
		long rv = ((long) (digest[3 + nTime * 4] & 0xFF) << 24) 
						| ((long) (digest[2 + nTime * 4] & 0xFF) << 16)
						| ((long) (digest[1 + nTime * 4] & 0xFF) << 8)
						| (digest[0 + nTime * 4] & 0xFF);

		return rv & 0xFFFFFFFFL; /* Truncate to 32-bits */
	}

	/**
	 * Get the md5 of the given key.
	 */
	public byte[] computeMd5(String k) {
		MessageDigest md5;
		try {
			md5 = MessageDigest.getInstance("MD5");
		} catch (NoSuchAlgorithmException e) {
			throw new RuntimeException("MD5 not supported", e);
		}
		md5.reset();
		byte[] keyBytes = null;
		try {
			keyBytes = k.getBytes("UTF-8");
		} catch (UnsupportedEncodingException e) {
			throw new RuntimeException("Unknown string :" + k, e);
		}

		md5.update(keyBytes);
		return md5.digest();
	}

}
