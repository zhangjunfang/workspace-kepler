using System;
using System.Data;
namespace IDAL
{
	/// <summary>
	/// 接口层U_Admin
	/// </summary>
	public interface IU_Admin
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		int Exists(long UserId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Model.U_Admin model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		int Update(Model.U_Admin model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(long UserId);
		int DeleteList(string UserIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Model.U_Admin GetModel(long UserId);
		/// <summary>
		/// 获得数据列表
		/// </summary>
        DataSet GetDataSet(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int pageindex, int pagesize, ref int Counts, string strWhere);
        DataSet GetListTables(int pageIndex, int pageSize, string tableSource, string strWhere, string OrderExpression, string Fields, ref int Counts);
     
		#endregion  成员方法
	} 
}
