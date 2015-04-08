using System.Windows.Forms;
using CCWin;

namespace ServiceStationClient.ComponentUI
{
    public partial class FormBG : Form
    {
        public FormBG()
        {
            InitializeComponent();
            SetStyle(               
                ControlStyles.DoubleBuffer, true);
            this.Opacity = 0.5;           
        }
    }
}
