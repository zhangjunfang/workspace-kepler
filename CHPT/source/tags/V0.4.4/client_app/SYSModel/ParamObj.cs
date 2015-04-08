using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYSModel
{
    /// <summary>
    /// 参数对象
    /// </summary>
    public class ParamObj
    {
        public ParamObj()
        {
 
        }
        public ParamObj(string name, object value, SysDbType type, int size, ParamDirection direction)
        {
            this.name = name;
            this.value = value;
            this.type = type;
            this.size = size;
            this.direction = direction;
        }

        public ParamObj(string name, object value, SysDbType type, int size)
        {
            this.name = name;
            this.value = value;
            this.type = type;
            this.size = size;
        }
        public ParamObj(string name, object value, SysDbType type)
        {
            this.name = name;
            this.value = value;
            this.type = type;
        }
        /// <summary>
        /// 参数名称
        /// </summary> 
       public string name { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
       public object value { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
       public SysDbType type { get; set; }
       /// <summary>
       /// 参数长度或者大小
       /// </summary>
       public int? size { get; set; }
        /// <summary>
        /// 参数方向
        /// </summary>
       public ParamDirection direction { get; set; }
    }

    public class SqlParamJSON
    {
        /// <summary>
        /// 参数名称
        /// </summary> 
        public string name { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public object value { get; set; }
    }
}
