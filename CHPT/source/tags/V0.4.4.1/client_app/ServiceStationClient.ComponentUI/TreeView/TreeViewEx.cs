using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{
    public partial class TreeViewEx : TreeView
    {
        public TreeViewEx()
        {
            InitializeComponent();
            SetStyle(                
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
           
        }

        private bool ignoreAutoCheck = false;

        public bool IgnoreAutoCheck 
        {
            set 
            {
                ignoreAutoCheck = value;
            }
            get 
            {
                return ignoreAutoCheck;
            }
        }

        protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseDoubleClick(e);
            if (e.Node.IsExpanded)
                e.Node.Collapse();
            else
                e.Node.Expand();
        }

        Point currentPoint = new Point();
        protected override void OnMouseUp(MouseEventArgs e)
        {
            currentPoint.X = e.X;
            currentPoint.Y = e.Y;
            base.OnMouseUp(e);
        }

        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            base.OnAfterCheck(e);
            if (e.Action != TreeViewAction.Unknown && !ignoreAutoCheck)
            {
                SetChildNodes(e.Node, e.Node.Checked);
                SetParentNodes(e.Node, e.Node.Checked);
            }
        }

        private void SetParentNodes(TreeNode CurNode, bool Checked)
        {
            if (CurNode.Parent !=  null)
            {
                if (Checked)
                {
                    CurNode.Parent.Checked = Checked;
                    SetParentNodes(CurNode.Parent, Checked);
                }
                else
                {
                    bool ParFlag = false;
                    foreach (TreeNode tmp  in CurNode.Parent.Nodes)
                    {
                        if (tmp.Checked)
                        {
                            ParFlag = true;
                            break;
                        }
                    }
                    CurNode.Parent.Checked = ParFlag;
                    SetParentNodes(CurNode.Parent, ParFlag);
                }
            }
        }
 
        private void SetChildNodes(TreeNode CurNode, bool Checked)
        {
            if (CurNode.Nodes !=  null)
            {
                foreach (TreeNode tmpNode  in CurNode.Nodes)
                {
                    tmpNode.Checked = Checked;
                    SetChildNodes(tmpNode, Checked);
                }
            }
        }

        public event MouseEventHandler OnCustomDoubleClick;

        protected override void WndProc(ref Message m)
        {
            //解决 双击选中复选框事件，但会导致双击不能展开节点
            if (m.Msg == 0x203)
            {
                m.Result = IntPtr.Zero;
                if (OnCustomDoubleClick != null)
                {
                    MouseEventArgs e = new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, currentPoint.X, currentPoint.Y, 1);
                    OnCustomDoubleClick(this, e);
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

         
    }
}
