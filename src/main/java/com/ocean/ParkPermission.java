package com.ocean;

import java.security.Permission;

public class ParkPermission extends Permission {
	/**
	 * 
	 */
	private static final long serialVersionUID = 3952628258108945694L;
	private String action;

	public ParkPermission(String target, String anAction) {
		super(target);
		action = anAction;
	}

	@Override
	public String getActions() {
		return action;
	}

	@Override
	public boolean equals(Object other) {
		return false;
	}

	@Override
	public int hashCode() {
		return getName().hashCode() + action.hashCode();
	}

	@Override
	public boolean implies(Permission other) {
		// System.out.println("implies");
		return true;
	}
}