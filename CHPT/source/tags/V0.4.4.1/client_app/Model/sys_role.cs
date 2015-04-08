using System;
namespace Model
{
	/// <summary>
	/// 角色信息
	/// </summary>
	[Serializable]
	public partial class sys_role
	{
		public sys_role()
		{}
		#region Model
		private string _role_id;
		private string _role_code;
		private string _role_name;
		private string _remark;
		private string _data_sources;
		private string _state;
		private string _enable_flag;
		private string _create_by;
		private long _create_time;
		private string _update_by;
		private long? _update_time;
		/// <summary>
		/// 角色id
		/// </summary>
		public string role_id
		{
			set{ _role_id=value;}
			get{return _role_id;}
		}
		/// <summary>
		/// 角色编码
		/// </summary>
		public string role_code
		{
			set{ _role_code=value;}
			get{return _role_code;}
		}
		/// <summary>
		/// 角色名称
		/// </summary>
		public string role_name
		{
			set{ _role_name=value;}
			get{return _role_name;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 数据来源，关联字典码表
		/// </summary>
		public string data_sources
		{
			set{ _data_sources=value;}
			get{return _data_sources;}
		}
		/// <summary>
		/// 状态，关联字典码表
		/// </summary>
		public string state
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 删除标记
		/// </summary>
		public string enable_flag
		{
			set{ _enable_flag=value;}
			get{return _enable_flag;}
		}
		/// <summary>
		/// 创建人，关联人员表
		/// </summary>
		public string create_by
		{
			set{ _create_by=value;}
			get{return _create_by;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public long create_time
		{
			set{ _create_time=value;}
			get{return _create_time;}
		}
		/// <summary>
		/// 最后编辑人，关联人员表
		/// </summary>
		public string update_by
		{
			set{ _update_by=value;}
			get{return _update_by;}
		}
		/// <summary>
		/// 最后编辑时间
		/// </summary>
		public long? update_time
		{
			set{ _update_time=value;}
			get{return _update_time;}
		}
		#endregion Model

	}
}

