using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using  IDAL;
namespace  SQLServerDAL
{
	/// <summary>
	/// 数据访问类:U_Admin
	/// </summary>
	class U_Admin:IU_Admin
	{
		public U_Admin()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public int Exists(long UserId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from U_Admin");
			strSql.Append(" where UserId=@UserId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.BigInt)
};
			parameters[0].Value = UserId;

            return  DBUtility.SqlHelper.ExecuteNonQuery(ConnString.connReadonly, CommandType.Text, strSql.ToString(), parameters);

		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add( Model.U_Admin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into U_Admin(");
			strSql.Append("UserName,UserPwd,Realname,RolesId,LastLoginTime,Enable,Created)");
			strSql.Append(" values (");
			strSql.Append("@UserName,@UserPwd,@Realname,@RolesId,@LastLoginTime,@Enable,@Created)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@UserPwd", SqlDbType.Char,16),
					new SqlParameter("@Realname", SqlDbType.NVarChar,20),
					new SqlParameter("@RolesId", SqlDbType.Int,4),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@Enable", SqlDbType.Bit,1),
					new SqlParameter("@Created", SqlDbType.DateTime)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.UserPwd;
			parameters[2].Value = model.Realname;
			parameters[3].Value = model.RolesId;
			parameters[4].Value = model.LastLoginTime;
			parameters[5].Value = model.Enable;
			parameters[6].Value = model.Created;

            return  DBUtility.SqlHelper.ExecuteNonQuery(ConnString.connWrite, CommandType.Text, strSql.ToString(), parameters);


        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update( Model.U_Admin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update U_Admin set ");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserPwd=@UserPwd,");
			strSql.Append("Realname=@Realname,");
			strSql.Append("RolesId=@RolesId,");
			strSql.Append("LastLoginTime=@LastLoginTime,");
			strSql.Append("Enable=@Enable,");
			strSql.Append("Created=@Created");
			strSql.Append(" where UserId=@UserId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@UserPwd", SqlDbType.Char,16),
					new SqlParameter("@Realname", SqlDbType.NVarChar,20),
					new SqlParameter("@RolesId", SqlDbType.Int,4),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@Enable", SqlDbType.Bit,1),
					new SqlParameter("@Created", SqlDbType.DateTime),
					new SqlParameter("@UserId", SqlDbType.BigInt,8)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.UserPwd;
			parameters[2].Value = model.Realname;
			parameters[3].Value = model.RolesId;
			parameters[4].Value = model.LastLoginTime;
			parameters[5].Value = model.Enable;
			parameters[6].Value = model.Created;
			parameters[7].Value = model.UserId;

            return  DBUtility.SqlHelper.ExecuteNonQuery(ConnString.connWrite, CommandType.Text, strSql.ToString(), parameters);

		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(long UserId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from U_Admin ");
			strSql.Append(" where UserId=@UserId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.BigInt)
};
			parameters[0].Value = UserId;

            return  DBUtility.SqlHelper.ExecuteNonQuery(ConnString.connWrite, CommandType.Text, strSql.ToString(), parameters);
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public int DeleteList(string UserIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from U_Admin ");
			strSql.Append(" where UserId in ("+UserIdlist + ")  ");
            return  DBUtility.SqlHelper.ExecuteNonQuery(ConnString.connWrite, CommandType.Text, strSql.ToString(), null);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public  Model.U_Admin GetModel(long UserId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 UserId,UserName,UserPwd,Realname,RolesId,LastLoginTime,Enable,Created from U_Admin ");
			strSql.Append(" where UserId=@UserId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.BigInt)
};
			parameters[0].Value = UserId;

			 Model.U_Admin model=new  Model.U_Admin();
			DataSet ds=   DBUtility.SqlHelper.ExecuteDataSet(ConnString.connReadonly, CommandType.Text, strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["UserId"]!=null && ds.Tables[0].Rows[0]["UserId"].ToString()!="")
				{
					model.UserId=long.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserName"]!=null && ds.Tables[0].Rows[0]["UserName"].ToString()!="")
				{
					model.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["UserPwd"]!=null && ds.Tables[0].Rows[0]["UserPwd"].ToString()!="")
				{
					model.UserPwd=ds.Tables[0].Rows[0]["UserPwd"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Realname"]!=null && ds.Tables[0].Rows[0]["Realname"].ToString()!="")
				{
					model.Realname=ds.Tables[0].Rows[0]["Realname"].ToString();
				}
				if(ds.Tables[0].Rows[0]["RolesId"]!=null && ds.Tables[0].Rows[0]["RolesId"].ToString()!="")
				{
					model.RolesId=int.Parse(ds.Tables[0].Rows[0]["RolesId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LastLoginTime"]!=null && ds.Tables[0].Rows[0]["LastLoginTime"].ToString()!="")
				{
					model.LastLoginTime=DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Enable"]!=null && ds.Tables[0].Rows[0]["Enable"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Enable"].ToString()=="1")||(ds.Tables[0].Rows[0]["Enable"].ToString().ToLower()=="true"))
					{
						model.Enable=true;
					}
					else
					{
						model.Enable=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Created"]!=null && ds.Tables[0].Rows[0]["Created"].ToString()!="")
				{
					model.Created=DateTime.Parse(ds.Tables[0].Rows[0]["Created"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetDataSet(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserId,UserName,UserPwd,Realname,RolesId,LastLoginTime,Enable,Created ");
			strSql.Append(" FROM U_Admin ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return  DBUtility.SqlHelper.ExecuteDataSet(ConnString.connReadonly, CommandType.Text, strSql.ToString(), null);
		}

        /// <summary>
        /// 分页
        /// </summary>
        // public static DataSet GetListPageStoreProcedure(int page, int pageSize, string tableSource, string whereValue, string OrderExpression, string Fields, ref int Counts)
        public DataSet GetList(int pageindex, int pagesize, ref int Counts, string strWhere)
        {
            return ListPage.GetListPageStoreProcedure(pageindex, pagesize, "U_Admin", strWhere, ref Counts);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex">页号，从0开始</param>
        /// <param name="pageSize">每页记录数(页尺寸)</param>
        /// <param name="Counts">查询到的记录数</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="TableSource">表名或视图</param>
        /// <param name="OrderExpression">排序表达式</param>
        /// <param name="Fields">查询字段列表</param>
        /// <returns></returns>
        public DataSet GetListTables(int pageIndex, int pageSize, string tableSource, string strWhere, string OrderExpression, string Fields, ref int Counts)
        {
            return ListPage.GetListPages(pageIndex, pageSize, tableSource, strWhere, OrderExpression, Fields, ref Counts);
        }
		#endregion  Method
	}
}

