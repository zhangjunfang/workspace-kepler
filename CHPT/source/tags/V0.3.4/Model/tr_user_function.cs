using System;
namespace Model
{
	/// <summary>
	/// 人员与常用功能关系
	/// </summary>
	[Serializable]
	public partial class tr_user_function
	{
		public tr_user_function()
		{}
		#region Model
		private string _user_fun_id;
		private string _user_id;
		private string _fun_id;
		/// <summary>
		/// 
		/// </summary>
		public string user_fun_id
		{
			set{ _user_fun_id=value;}
			get{return _user_fun_id;}
		}
		/// <summary>
		/// 人员ID，关联人员档案
		/// </summary>
		public string user_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 系统功能ID，关联系统功能表
		/// </summary>
		public string fun_id
		{
			set{ _fun_id=value;}
			get{return _fun_id;}
		}
		#endregion Model

	}
}

