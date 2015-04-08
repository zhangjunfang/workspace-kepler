using System;
namespace Model
{
	/// <summary>
	/// 人员信息
	/// </summary>
	[Serializable]
	public partial class sys_user
	{
		public sys_user()
		{}
		#region Model
		private string _user_id;
		private string _org_id;
		private string _user_code;
		private string _user_name;
		private string _land_name;
		private string _sex;
		private string _nation;
		private long? _birthday;
		private string _idcard_type;
		private string _idcard_num;
		private string _register_address;
		private string _native_place;
		private string _political_status;
		private string _user_phone;
		private string _user_telephone;
		private string _user_fax;
		private string _user_email;
		private string _user_address;
		private string _user_height;
		private string _user_weight;
		private long? _entry_date;
		private string _post;
		private string _position;
		private string _level;
		private string _graduate_institutions;
		private string _specialty;
		private string _education;
		private long? _graduate_date;
		private string _technical_expertise;
		private string _wage;
		private string _is_operator;
		private string _password;
		private string _remark;
		private string _status;
		private string _data_sources;
		private string _create_by;
		private long _create_time;
		private string _update_by;
		private long? _update_time;
		/// <summary>
		/// 人员id
		/// </summary>
		public string user_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 所属组织，关联组织机构表
		/// </summary>
		public string org_id
		{
			set{ _org_id=value;}
			get{return _org_id;}
		}
		/// <summary>
		/// 人员编码
		/// </summary>
		public string user_code
		{
			set{ _user_code=value;}
			get{return _user_code;}
		}
		/// <summary>
		/// 姓名
		/// </summary>
		public string user_name
		{
			set{ _user_name=value;}
			get{return _user_name;}
		}
		/// <summary>
		/// 登陆账号名称
		/// </summary>
		public string land_name
		{
			set{ _land_name=value;}
			get{return _land_name;}
		}
		/// <summary>
		/// 性别，关联字典码表
		/// </summary>
		public string sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 民族，关联字典码表
		/// </summary>
		public string nation
		{
			set{ _nation=value;}
			get{return _nation;}
		}
		/// <summary>
		/// 出生日期
		/// </summary>
		public long? birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 证件类型，关联字典码表
		/// </summary>
		public string idcard_type
		{
			set{ _idcard_type=value;}
			get{return _idcard_type;}
		}
		/// <summary>
		/// 证件号码
		/// </summary>
		public string idcard_num
		{
			set{ _idcard_num=value;}
			get{return _idcard_num;}
		}
		/// <summary>
		/// 户口所在地
		/// </summary>
		public string register_address
		{
			set{ _register_address=value;}
			get{return _register_address;}
		}
		/// <summary>
		/// 籍贯
		/// </summary>
		public string native_place
		{
			set{ _native_place=value;}
			get{return _native_place;}
		}
		/// <summary>
		/// 政治面貌，关联字典码表
		/// </summary>
		public string political_status
		{
			set{ _political_status=value;}
			get{return _political_status;}
		}
		/// <summary>
		/// 手机
		/// </summary>
		public string user_phone
		{
			set{ _user_phone=value;}
			get{return _user_phone;}
		}
		/// <summary>
		/// 固话
		/// </summary>
		public string user_telephone
		{
			set{ _user_telephone=value;}
			get{return _user_telephone;}
		}
		/// <summary>
		/// 传真
		/// </summary>
		public string user_fax
		{
			set{ _user_fax=value;}
			get{return _user_fax;}
		}
		/// <summary>
		/// 邮箱
		/// </summary>
		public string user_email
		{
			set{ _user_email=value;}
			get{return _user_email;}
		}
		/// <summary>
		/// 联系地址
		/// </summary>
		public string user_address
		{
			set{ _user_address=value;}
			get{return _user_address;}
		}
		/// <summary>
		/// 身高
		/// </summary>
		public string user_height
		{
			set{ _user_height=value;}
			get{return _user_height;}
		}
		/// <summary>
		/// 体重
		/// </summary>
		public string user_weight
		{
			set{ _user_weight=value;}
			get{return _user_weight;}
		}
		/// <summary>
		/// 入职日期
		/// </summary>
		public long? entry_date
		{
			set{ _entry_date=value;}
			get{return _entry_date;}
		}
		/// <summary>
		/// 职务，关联字典码表
		/// </summary>
		public string post
		{
			set{ _post=value;}
			get{return _post;}
		}
		/// <summary>
		/// 岗位，关联字典码表
		/// </summary>
		public string position
		{
			set{ _position=value;}
			get{return _position;}
		}
		/// <summary>
		/// 级别，关联字典码表
		/// </summary>
		public string level
		{
			set{ _level=value;}
			get{return _level;}
		}
		/// <summary>
		/// 毕业院校
		/// </summary>
		public string graduate_institutions
		{
			set{ _graduate_institutions=value;}
			get{return _graduate_institutions;}
		}
		/// <summary>
		/// 专业
		/// </summary>
		public string specialty
		{
			set{ _specialty=value;}
			get{return _specialty;}
		}
		/// <summary>
		/// 学历，关联字典码表
		/// </summary>
		public string education
		{
			set{ _education=value;}
			get{return _education;}
		}
		/// <summary>
		/// 毕业时间
		/// </summary>
		public long? graduate_date
		{
			set{ _graduate_date=value;}
			get{return _graduate_date;}
		}
		/// <summary>
		/// 技术特长
		/// </summary>
		public string technical_expertise
		{
			set{ _technical_expertise=value;}
			get{return _technical_expertise;}
		}
		/// <summary>
		/// 工资
		/// </summary>
		public string wage
		{
			set{ _wage=value;}
			get{return _wage;}
		}
		/// <summary>
		/// 是否操作员，0否，1是
		/// </summary>
		public string is_operator
		{
			set{ _is_operator=value;}
			get{return _is_operator;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string password
		{
			set{ _password=value;}
			get{return _password;}
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
		/// 状态，关联字典码表
		/// </summary>
		public string status
		{
			set{ _status=value;}
			get{return _status;}
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
		/// 创建人
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
		/// 最后编辑人
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

