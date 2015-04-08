using System;
namespace Model
{
	/// <summary>
	/// U_Admin:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class U_Admin
	{
		public U_Admin()
		{}
		#region Model
		private long _userid;
		private string _username;
		private string _userpwd;
		private string _realname;
		private int _rolesid;
		private DateTime _lastlogintime;
		private bool _enable;
		private DateTime _created;
		/// <summary>
		/// 
		/// </summary>
		public long UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserPwd
		{
			set{ _userpwd=value;}
			get{return _userpwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Realname
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RolesId
		{
			set{ _rolesid=value;}
			get{return _rolesid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime LastLoginTime
		{
			set{ _lastlogintime=value;}
			get{return _lastlogintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Enable
		{
			set{ _enable=value;}
			get{return _enable;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Created
		{
			set{ _created=value;}
			get{return _created;}
		}
		#endregion Model

	}
}

