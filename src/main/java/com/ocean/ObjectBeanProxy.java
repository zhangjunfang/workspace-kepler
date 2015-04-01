package com.ocean;

public class ObjectBeanProxy implements ObjectBean {
	/**
	 * 
	 */
	private static final long serialVersionUID = 8248475542293764325L;
	Object obj;
	Long vid;
	String name;

	ObjectBeanProxy() {
	}

	/*
	 * private ObjectBeanProxy(ObjValue ov, String domainnodekey){ vid =
	 * (Long)ov.getObj(domainnodekey+"._me_ta.version"); obj =
	 * ov.get(domainnodekey); name = domainnodekey; }
	 */
	// @Delegate(interfaceName="com.ocean.ObjectBean",methodName="toObject",policy=DelegatePolicy.Implements)
	@Override
	public Object toObject() {
		return obj;
	}

	// @Delegate(interfaceName="com.ocean.ObjectBean",methodName="getName",policy=DelegatePolicy.Implements)
	@Override
	public String getName() {
		return name;
	}

	@Override
	public String getDomain() {
		if (name != null)
			return ParkObjValue.getDomainNode(name)[0];
		else
			return null;
	}

	@Override
	public String getNode() {
		if (name != null) {
			String[] arr = ParkObjValue.getDomainNode(name);
			if (arr.length == 2)
				return arr[1];
		}
		return null;
	}

	@Override
	public String toString() {
		return name + ":" + obj.toString();
	}
	/*
	 * @Delegate(interfaceName="com.ocean.ObjectVersion",methodName="getVid",policy
	 * =DelegatePolicy.Implements) public Long getVid(){ return vid; }
	 */
}