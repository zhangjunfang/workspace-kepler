using System;
namespace Model
{
	/// <summary>
	/// 角色与系统功能关系
	/// </summary>
	[Serializable]
	public partial class tr_role_function
	{
		public tr_role_function()
		{}
		#region Model
		private string _role_fun_id;
		private string _role_id;
		private string _fun_id;
		/// <summary>
		/// 
		/// </summary>
		public string role_fun_id
		{
			set{ _role_fun_id=value;}
			get{return _role_fun_id;}
		}
		/// <summary>
		/// 角色ID，关联角色表
		/// </summary>
		public string role_id
		{
			set{ _role_id=value;}
			get{return _role_id;}
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

