package com.ocean;

public interface CoolKeyResult {
	@SuppressWarnings("rawtypes")
	public CoolHashMap.CoolKeySet nextBatchKey(int batchLength);
}