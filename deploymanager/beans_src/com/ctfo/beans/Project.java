package com.ctfo.beans;

public class Project {

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 5207267911718976666L;

	/** */
	private Long projectId;
	
	private String projectName;
	
	private String projectVersion;
	
	private String compileDate;
	
	private String branchName;
	
	private String branchPath;
	
	private String dbscriptPath;
	
	private String deployDesc;

	public Long getProjectId() {
		return projectId;
	}

	public void setProjectId(Long projectId) {
		this.projectId = projectId;
	}

	public String getProjectName() {
		return projectName;
	}

	public void setProjectName(String projectName) {
		this.projectName = projectName;
	}

	public String getProjectVersion() {
		return projectVersion;
	}

	public void setProjectVersion(String projectVersion) {
		this.projectVersion = projectVersion;
	}

	public String getCompileDate() {
		return compileDate;
	}

	public void setCompileDate(String compileDate) {
		this.compileDate = compileDate;
	}

	public String getBranchName() {
		return branchName;
	}

	public void setBranchName(String branchName) {
		this.branchName = branchName;
	}

	public String getBranchPath() {
		return branchPath;
	}

	public void setBranchPath(String branchPath) {
		this.branchPath = branchPath;
	}

	public String getDbscriptPath() {
		return dbscriptPath;
	}

	public void setDbscriptPath(String dbscriptPath) {
		this.dbscriptPath = dbscriptPath;
	}

	public String getDeployDesc() {
		return deployDesc;
	}

	public void setDeployDesc(String deployDesc) {
		this.deployDesc = deployDesc;
	}
}