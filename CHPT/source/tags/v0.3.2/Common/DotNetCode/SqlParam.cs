using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace HXCCommon.DotNetCode
{
    /// <summary>
    /// 存放参数【键，键值】
    /// </summary>
    public class SqlParam
    {
        public string FieldName { get; set; }

        public DbType DataType { get; set; }

        public object FiledValue { get; set; }

        public SqlParam()
        {

        }
        public SqlParam(string _FieldName, object _FiledValue)
            : this(_FieldName, DbType.AnsiString, _FiledValue)
        {
        }
        public SqlParam(string _FieldName, DbType _DbType, object _FiledValue)
        {
            this.FieldName = _FieldName;
            this.DataType = _DbType;
            this.FiledValue = _FiledValue;
        }
    }
}
