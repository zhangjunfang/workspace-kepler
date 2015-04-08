package com.ctfo.trackservice.model;

public class LineList{
		/**	线路编号	*/
		private String lineId;
		/**	线路	*/
		private String lines;
		/**	线路方向（1:起点发车；2:终点发车）	*/
		private int lineDirection;
		/**
		 * 初始化
		 * @param lineId 线路编号
		 * @param lineStr 线路集字符串 
		 * @param direction 线路方向（1:起点发车；2:终点发车）
		 */
		public LineList(String lineId, String lineStr, int direction) {
			this.lineId = lineId;
			this.lines = lineStr;
			this.lineDirection = direction;
		}
		/**
		 * @return the 线路编号
		 */
		public String getLineId() {
			return lineId;
		}
		/**
		 * @param 线路编号 the lineId to set
		 */
		public void setLineId(String lineId) {
			this.lineId = lineId;
		}
		/**
		 * @return the 线路
		 */
		public String getLines() {
			return lines;
		}
		/**
		 * @param 线路 the lines to set
		 */
		public void setLines(String lines) {
			this.lines = lines;
		}
		/**
		 * @return the 线路方向（1:起点发车；2:终点发车）
		 */
		public int getLineDirection() {
			return lineDirection;
		}
		/**
		 * @param 线路方向（1:起点发车；2:终点发车） the lineDirection to set
		 */
		public void setLineDirection(int lineDirection) {
			this.lineDirection = lineDirection;
		}
		
	}


