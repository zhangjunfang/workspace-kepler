using System.Windows.Forms;

namespace HXCPcClient.UCForm.SysManage.ReminderSet
{
    public partial class UCReminderType : UserControl
    {
        /// <summary>
        /// 获取弹出气泡提醒是否选中
        /// </summary>
        public bool BubbleChecked
        {
            get
            {
                return this.cbBubble.Checked;
            }
        }

        /// <summary>
        /// 获取声音提醒是否选中
        /// </summary>
        public bool SoundChecked
        {
            get
            {
                return this.cbSound.Checked;
            }
        }


        public UCReminderType()
        {
            InitializeComponent();
        }

        public void SetChecked(bool bubble, bool sounds)
        {
            this.cbBubble.Checked = bubble;
            this.cbSound.Checked = sounds;
        }       
    }
}
