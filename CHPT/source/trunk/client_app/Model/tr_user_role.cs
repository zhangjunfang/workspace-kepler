using System;
namespace Model
{
	/// <summary>
	/// 人员与角色关系
	/// </summary>
	[Serializable]
	public partial class tr_user_role
	{
		public tr_user_role()
		{}
		#region Model
		private string _user_role_id;
		private string _user_id;
		private string _role_id;
		/// <summary>
		/// 
		/// </summary>
		public string user_role_id
		{
			set{ _user_role_id=value;}
			get{return _user_role_id;}
		}
		/// <summary>
		/// 人员ID，关联人员表
		/// </summary>
		public string user_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 角色ID，关联角色表
		/// </summary>
		public string role_id
		{
			set{ _role_id=value;}
			get{return _role_id;}
		}
		#endregion Model

	}
}

