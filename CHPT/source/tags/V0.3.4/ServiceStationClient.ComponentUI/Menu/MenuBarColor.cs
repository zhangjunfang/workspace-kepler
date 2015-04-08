using System.Drawing;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI.Menu
{
    /// <summary>
    /// 更改MenuStrip背景色 
    /// </summary>
    /// <remarks>
    /// add by kord
    /// </remarks>
     public class MenuItemRenderer : ToolStripProfessionalRenderer
     {
         #region Backgrounds
         protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
         {
             if (e.Item.IsOnDropDown)
             {
                 if (e.Item.Selected == true && e.Item.Enabled)
                 {
                     Rectangle rect = new Rectangle();
                     rect = new Rectangle(0, 0, (int)e.Graphics.VisibleClipBounds.Width, (int)e.Graphics.VisibleClipBounds.Height);
                     e.Graphics.DrawImage(e.Item.BackgroundImage, rect);
                     return;
                 }
             }
             base.OnRenderMenuItemBackground(e);
         }
         #endregion
     }
 }
