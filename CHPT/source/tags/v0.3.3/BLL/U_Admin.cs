using System;
using System.Data;
using System.Collections.Generic;
using Model;
using DALFactory;
using IDAL;
namespace BLL
{
	/// <summary>
	/// U_Admin
	/// </summary>
	public partial class U_Admin
	{
		//private static IU_Admin dal=DALFactory.CommonDALAccess.U_Admin();
        private static readonly Object locker = new Object();
        private static IDAL.IU_Admin dal = null;
        private static IDAL.IU_Admin Dal
        {
            get
            {
                if (dal == null)
                {
                    lock (locker)
                    {
                        if (dal == null)
                        {
                            dal = DALFactory.CommonDALAccess.U_Admin();
                        }
                    }
                }
                return dal;
            }
        }
		public U_Admin()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static int Exists(long UserId)
		{
            return Dal.Exists(UserId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(Model.U_Admin model)
		{
            return Dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static int Update(Model.U_Admin model)
		{
            return Dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static int Delete(long UserId)
		{

            return Dal.Delete(UserId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static int DeleteList(string UserIdlist )
		{
            return Dal.DeleteList(UserIdlist);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static Model.U_Admin GetModel(int UserId)
		{

            return Dal.GetModel(UserId);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetDataSet(string strWhere)
        {
            return Dal.GetDataSet(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public static DataSet GetList(int pageIndex, int pageSize, ref int count, string strWhere)
        {
            return Dal.GetList(pageIndex, pageSize, ref count, strWhere);
        }

        public static DataSet GetListTables(int pageIndex, int pageSize, string tableSource, string strWhere, string OrderExpression, string Fields, ref int Counts)
        {
            return Dal.GetListTables(pageIndex, pageSize, tableSource, strWhere, OrderExpression, Fields, ref Counts);
        }

		#endregion  Method
	}
}

