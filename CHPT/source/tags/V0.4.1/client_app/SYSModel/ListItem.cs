using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYSModel
{
    /// <summary>
    /// 选择项类，用于ComboBox或者ListBox添加项
    /// </summary>
    [Serializable]
    public class ListItem
    {
        private object value = null;
        private string text = string.Empty;
        public ListItem(object value, string text)
        {
            this.value = value;
            this.text = text;
        }
        public override string ToString()
        {
            return this.text;
        }
        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}
