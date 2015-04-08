package com.ctfo.dataanalysisservice.beans;

import java.util.List;

public class LineDataObject extends BaseDataObject {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	/**
	 * 线路ID
	 */
	private String lineId;
	/**
	 * 线段属性
	 */
	private List<SectionsDataObject> sections;

	/**
	 * @return the sections
	 */
	public List<SectionsDataObject> getSections() {
		return sections;
	}

	/**
	 * @param sections
	 *            the sections to set
	 */
	public void setSections(List<SectionsDataObject> sections) {
		this.sections = sections;
	}

	/**
	 * @return the lineId
	 */
	public String getLineId() {
		return lineId;
	}

	/**
	 * @param lineId
	 *            the lineId to set
	 */
	public void setLineId(String lineId) {
		this.lineId = lineId;
	}

}
