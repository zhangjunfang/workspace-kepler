using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{
    public partial class FormBG : Form
    {
        public FormBG()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            this.Opacity = 0.5;
        }
    }
}
