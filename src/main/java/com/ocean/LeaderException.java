package com.ocean;

public class LeaderException extends Exception {
	/**
	 * 
	 */
	private static final long serialVersionUID = 6707903695473442241L;
	private String[] leaderServer;

	public LeaderException() {
	}

	public LeaderException(String[] thisserver, String[] leaderServer) {
		super(thisserver[0] + ":" + thisserver[1]
				+ " Refuse Service, Leader of group is " + leaderServer[0]
				+ ":" + leaderServer[1]);
		this.leaderServer = leaderServer;
	}

	public String[] getLeaderServer() {
		return leaderServer;
	}
}