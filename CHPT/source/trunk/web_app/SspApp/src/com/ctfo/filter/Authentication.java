package com.ctfo.filter;

import javax.servlet.http.HttpServletRequest;

public abstract class Authentication {
	

	
	abstract public boolean checkAuth(String path,HttpServletRequest request);

	public boolean checkAuth(HttpServletRequest request){
		return this.checkAuth(request.getRequestURI(),request);
	}
}
