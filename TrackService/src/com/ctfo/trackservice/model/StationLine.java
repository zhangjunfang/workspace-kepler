package com.ctfo.trackservice.model;

public class StationLine implements Comparable<StationLine>{
		/**	站点编号	*/
		private String stationId;
		/**	站点序号	*/
		private int stationNo;
		/**	线路方向（1:起点发车；2:终点发车）	*/
		private int lineDirection;
		/** 中心点经度 */
		private long mapLon;
		/** 中心点纬度 */
		private long mapLat;
		/**
		 * 初始化
		 * @param stationId 站点编号
		 * @param stationNo 站点序号
		 * @param lineDirection 线路方向（1:起点发车；2:终点发车）
		 * @param mapLat 
		 * @param mapLon 
		 */
		public StationLine(String stationId, int stationNo, int lineDirection, long lon, long lat) {
			this.stationId = stationId;
			this.stationNo = stationNo;
			this.mapLon = lon;
			this.mapLat = lat;
			this.lineDirection = lineDirection;
		}
		/**
		 * @return the 站点编号
		 */
		public String getStationId() {
			return stationId;
		}
		/**
		 * @param 站点编号 the stationId to set
		 */
		public void setStationId(String stationId) {
			this.stationId = stationId;
		}
		/**
		 * @return the 站点序号
		 */
		public int getStationNo() {
			return stationNo;
		}
		/**
		 * @param 站点序号 the stationNo to set
		 */
		public void setStationNo(int stationNo) {
			this.stationNo = stationNo;
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
		/**
		 * @return the 中心点经度
		 */
		public long getMapLon() {
			return mapLon;
		}
		/**
		 * @param 中心点经度 the mapLon to set
		 */
		public void setMapLon(long mapLon) {
			this.mapLon = mapLon;
		}
		/**
		 * @return the 中心点纬度
		 */
		public long getMapLat() {
			return mapLat;
		}
		/**
		 * @param 中心点纬度 the mapLat to set
		 */
		public void setMapLat(long mapLat) {
			this.mapLat = mapLat;
		}
		@Override
		public int compareTo(StationLine o) {
			return this.stationNo - o.stationNo;
		}
	}
