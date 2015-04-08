using System;
namespace Model
{
	/// <summary>
	/// 组织信息
	/// </summary>
	[Serializable]
	public partial class tb_organization
	{
		public tb_organization()
		{}
		#region Model
		private string _org_id;
		private string _com_id;
		private string _org_code;
		private string _org_name;
		private string _org_short_name;
		private string _parent_id;
		private string _contact_name;
		private string _contact_telephone;
		private string _remark;
		private string _data_sources;
		private string _status;
		private string _enable_flag;
		private string _create_by;
		private long _create_time;
		private string _update_by;
		private long? _update_time;
		/// <summary>
		/// 组织id
		/// </summary>
		public string org_id
		{
			set{ _org_id=value;}
			get{return _org_id;}
		}
		/// <summary>
		/// 所属公司，关联公司档案表
		/// </summary>
		public string com_id
		{
			set{ _com_id=value;}
			get{return _com_id;}
		}
		/// <summary>
		/// 组织编码
		/// </summary>
		public string org_code
		{
			set{ _org_code=value;}
			get{return _org_code;}
		}
		/// <summary>
		/// 组织名称
		/// </summary>
		public string org_name
		{
			set{ _org_name=value;}
			get{return _org_name;}
		}
		/// <summary>
		/// 组织简称
		/// </summary>
		public string org_short_name
		{
			set{ _org_short_name=value;}
			get{return _org_short_name;}
		}
		/// <summary>
		/// 父级，关联自己
		/// </summary>
		public string parent_id
		{
			set{ _parent_id=value;}
			get{return _parent_id;}
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public string contact_name
		{
			set{ _contact_name=value;}
			get{return _contact_name;}
		}
		/// <summary>
		/// 联系电话
		/// </summary>
		public string contact_telephone
		{
			set{ _contact_telephone=value;}
			get{return _contact_telephone;}
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
		/// 状态
		/// </summary>
		public string status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 删除标记，0删除，1未删除
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

