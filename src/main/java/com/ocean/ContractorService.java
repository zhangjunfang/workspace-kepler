package com.ocean;

class ContractorService extends MigrantWorker {
	/**
	 * 
	 */
	private static final long serialVersionUID = 3340810467878487598L;
	private ContractorParallel ctor = null;

	ContractorService(ContractorParallel ctor) {
		this.ctor = ctor;
	}

	@Override
	public WareHouse doTask(WareHouse inhouse) {
		return ctor.giveTask(inhouse);
	}

	/*
	 * void waitWorking(String host, int port, String workerType){
	 * 
	 * }
	 * 
	 * void waitWorking(String workerType);
	 */
}