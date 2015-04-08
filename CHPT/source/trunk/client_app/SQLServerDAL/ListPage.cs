using System;
using System.Data;
using System.Data.SqlClient;
using SYSModel;
namespace SQLServerDAL
{
    class ListPage
    {
        private static readonly Object locker = new Object();      
        

        /// <summary>
        /// 调用分页存储过程返回得到的数据集
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="tableSource"></param>
        /// <param name="whereValue"></param>
        /// <param name="OrderExpression"></param>
        /// <param name="Fields"></param>
        /// <returns></returns>
        public static DataSet GetListPages(string connString, int page, int pageSize, string tableSource, string whereValue, string OrderExpression, string Fields, UserIDOP userID, ref int Counts)
        {
            SqlParameter[] parm = new SqlParameter[]
		    {
			    new SqlParameter("@TableSource", SqlDbType.VarChar, 1000),
			    new SqlParameter("@SelectList", SqlDbType.VarChar, 2000),
			    new SqlParameter("@SearchCondition", SqlDbType.VarChar, 4000),
			    new SqlParameter("@OrderExpression", SqlDbType.VarChar, 100),
			    new SqlParameter("@PageIndex", SqlDbType.Int, 4),
			    new SqlParameter("@PageSize", SqlDbType.Int, 4),
                new SqlParameter("@Counts", SqlDbType.Int, 4)
		    };

            parm[0].Value = tableSource;
            parm[1].Value = Fields;
            parm[2].Value = whereValue;
            parm[3].Value = OrderExpression;
            parm[4].Value = page;
            parm[5].Value = pageSize;

            parm[parm.Length - 1].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            ds = DBUtility.SqlHelper.ExecuteDataSet(connString, CommandType.StoredProcedure, "[dbo].[list_page_tables]", userID, parm);
            Counts = Convert.ToInt32(parm[parm.Length - 1].Value);

            return ds;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public static int GetLsh(string connString, ref Int64 Orderlsh)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@lsh", SqlDbType.BigInt, 8) };
            parm[0].Value = Orderlsh;
            parm[0].Direction = ParameterDirection.Output;
            int i = DBUtility.SqlHelper.ExecuteNonQueryTran(connString, CommandType.StoredProcedure, "[dbo].[getSequence]", new UserIDOP(), parm);
            Orderlsh = Convert.ToInt64(parm[0].Value);
            return i;
        }
    }
}
