using System;
namespace Model
{
	/// <summary>
	/// 功能菜单
	/// </summary>
	[Serializable]
	public partial class sys_function
	{
		public sys_function()
		{}
		#region Model
		private string _fun_id;
		private int? _num;
		private string _fun_name;
		private string _fun_ename;
		private string _fun_uri;
		private string _fun_img;
		private string _parent_id;
		private string _fun_cbs;
		private int? _fun_level;
		private string _fun_idx;
		private int? _fun_flag;
		private int? _fun_run;
		private string _enable_flag;
		private string _remark;
		private string _create_by;
		private long _create_time;
		private string _update_by;
		private long? _update_time;
		/// <summary>
		/// 自定义编码
		/// </summary>
		public string fun_id
		{
			set{ _fun_id=value;}
			get{return _fun_id;}
		}
		/// <summary>
		/// 序号
		/// </summary>
		public int? num
		{
			set{ _num=value;}
			get{return _num;}
		}
		/// <summary>
		/// 功能名称
		/// </summary>
		public string fun_name
		{
			set{ _fun_name=value;}
			get{return _fun_name;}
		}
		/// <summary>
		/// 菜单英文名
		/// </summary>
		public string fun_ename
		{
			set{ _fun_ename=value;}
			get{return _fun_ename;}
		}
		/// <summary>
		/// 功能对应地址
		/// </summary>
		public string fun_uri
		{
			set{ _fun_uri=value;}
			get{return _fun_uri;}
		}
		/// <summary>
		/// 对应的图标文件地址
		/// </summary>
		public string fun_img
		{
			set{ _fun_img=value;}
			get{return _fun_img;}
		}
		/// <summary>
		/// 上一级功能编号
		/// </summary>
		public string parent_id
		{
			set{ _parent_id=value;}
			get{return _parent_id;}
		}
		/// <summary>
		/// 1，服务端功能   2，客户端功能
		/// </summary>
		public string fun_cbs
		{
			set{ _fun_cbs=value;}
			get{return _fun_cbs;}
		}
		/// <summary>
		/// 0-9
		/// </summary>
		public int? fun_level
		{
			set{ _fun_level=value;}
			get{return _fun_level;}
		}
		/// <summary>
		/// 执行方法
		/// </summary>
		public string fun_idx
		{
			set{ _fun_idx=value;}
			get{return _fun_idx;}
		}
		/// <summary>
		/// 0：未启用1：启用
		/// </summary>
		public int? fun_flag
		{
			set{ _fun_flag=value;}
			get{return _fun_flag;}
		}
		/// <summary>
		/// 1：可运行功能0：不可\r\n运行功能，属于菜单
		/// </summary>
		public int? fun_run
		{
			set{ _fun_run=value;}
			get{return _fun_run;}
		}
		/// <summary>
		/// 1:有效 0:无效 默认为1
		/// </summary>
		public string enable_flag
		{
			set{ _enable_flag=value;}
			get{return _enable_flag;}
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
		/// 最后修改人，关联人员表
		/// </summary>
		public string update_by
		{
			set{ _update_by=value;}
			get{return _update_by;}
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public long? update_time
		{
			set{ _update_time=value;}
			get{return _update_time;}
		}
		#endregion Model

	}
}

