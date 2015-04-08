using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClientBLL;
using System.Data;
namespace HXCPcClient
{
    public class OpCommand
    {
        public delegate void closeLoadFormDelegate(Form fm);
        public delegate void showLoadFormDelegate(Form M, Form L);
        public delegate void FunctionDelegateHandler(Dictionary<string, MethodParamObj> myDictParm);//无返回值
        public delegate void FunctionStrDelegateHandler(Dictionary<string, MethodParamObj> myDictParm, out String str);//返回String  
        public delegate void FunctionObjDelegateHandler(Dictionary<string, MethodParamObj> myDictParm, out Object obj);//返回Object  
        public delegate void FunctionDictMDelegateHandler(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm);//返回Dictionary<string, MethodParamObj> 
        public delegate void FunctionDataSetDelegateHandler(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds);//返回DataSet
        public delegate void FunctionListDelegateHandler<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list);//返回IList
        public delegate void FunctionDictDelegateHandler<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict);//返回IDictionary
        public static LoadForm LForm = new LoadForm();
        static OpCommand()
        {
            if (LForm == null)
            {
                LForm = new LoadForm();
            }
        }

        public static void closeForm(closeLoadFormDelegate myDelegate, Form fm)
        {
            if (GlobalStaticObj.AppMainForm.InvokeRequired)
            {
               GlobalStaticObj.AppMainForm.Invoke(myDelegate, fm);
            }
            else
            {
                myDelegate(fm);
            }
        }

        public static void showForm(showLoadFormDelegate myDelegate, Form M, Form L)
        {
            if (LForm.InvokeRequired)
            {
                LForm.Invoke(myDelegate, M, L);
            }
            else
            {
                myDelegate(M, L);
            }
        }

        public static void myFormClose(Form fm)
        {
            fm.Visible = false;
        }
        private static void CallBackMethod(IAsyncResult ar)
        {
            closeForm(myFormClose, LForm);
        }

        public static void ShowLoadForm(Form M, Form L)
        {
            if (L == null)
            {
                LForm = new LoadForm();
            }
            if (!L.Visible)
            {
                int Width = M.Width;
                int Height = M.Height;
                L.Top = M.Top + Height / 2;
                L.Left = M.Left + Width / 2;
                L.Width = 124;
                L.Height = 124;
                L.Show(M);
            }
        }

        private static void CallBackShowLoadForm(IAsyncResult ar)
        {
            showForm(ShowLoadForm,GlobalStaticObj.AppMainForm, LForm);
        }

        public static void ExeCommand(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd)
        {
            showForm(ShowLoadForm,GlobalStaticObj.AppMainForm, LForm);
            // Thread  thread = new Thread(new ThreadStart(() =>
            // {
            //     mnd.MethodExec();
            //     closeForm(myFormClose, LForm);
            // }
            //));
            // thread.Start();
            FunctionDelegateHandler asyM = new FunctionDelegateHandler(mnd.MethodExec);
            asyM.BeginInvoke(myDictParm, CallBackMethod, null);
        }

        public static void ExeCommand(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out Object obj, AsyncCallback asyCall)
        {
            showForm(ShowLoadForm,GlobalStaticObj.AppMainForm, LForm);
            FunctionObjDelegateHandler asyM = new FunctionObjDelegateHandler(mnd.MethodExec);
            asyM.BeginInvoke(myDictParm, out obj, asyCall, asyM);
        }

        public static void ExeCommand(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out Dictionary<string, MethodParamObj> outMyDictParm, AsyncCallback asyCall)
        {
            showForm(ShowLoadForm,GlobalStaticObj.AppMainForm, LForm);
            FunctionDictMDelegateHandler asyM = new FunctionDictMDelegateHandler(mnd.MethodExec);
            asyM.BeginInvoke(myDictParm, out outMyDictParm, asyCall, asyM);
        }

        public static void ExeCommand(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out String str, AsyncCallback asyCall)
        {
            showForm(ShowLoadForm,GlobalStaticObj.AppMainForm, LForm);
            FunctionStrDelegateHandler asyM = new FunctionStrDelegateHandler(mnd.MethodExec);
            asyM.BeginInvoke(myDictParm, out str, asyCall, asyM);
        }

        public static void ExeCommand(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out DataSet ds, AsyncCallback asyCall)
        {
            showForm(ShowLoadForm,GlobalStaticObj.AppMainForm, LForm);
            FunctionDataSetDelegateHandler asyM = new FunctionDataSetDelegateHandler(mnd.MethodExec);
            asyM.BeginInvoke(myDictParm, out ds, asyCall, asyM);
        }

        public static void ExeCommand<T>(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out IList<T> list, AsyncCallback asyCall)
        {
            showForm(ShowLoadForm,GlobalStaticObj.AppMainForm, LForm);
            FunctionListDelegateHandler<T> asyM = new FunctionListDelegateHandler<T>(mnd.MethodExec<T>);
            asyM.BeginInvoke(myDictParm, out list, asyCall, asyM);
        }

        public static void ExeCommand<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out IDictionary<TKey, TValue> dict, AsyncCallback asyCall)
        {
            showForm(ShowLoadForm,GlobalStaticObj.AppMainForm, LForm);
            FunctionDictDelegateHandler<TKey, TValue> asyM = new FunctionDictDelegateHandler<TKey, TValue>(mnd.MethodExec<TKey, TValue>);
            asyM.BeginInvoke(myDictParm, out dict, asyCall, asyM);
        }

        public static void InvokeExeCommand(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd)
        {
            mnd.MethodExec(myDictParm);
        }

        public static void InvokeExeCommand(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out Object obj)
        {
            mnd.MethodExec(myDictParm, out obj);
        }

        public static void InvokeExeCommand(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out Dictionary<string, MethodParamObj> outMyDictParm)
        {
            mnd.MethodExec(myDictParm, out outMyDictParm);
        }

        public static void InvokeExeCommand(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out String str)
        {
            mnd.MethodExec(myDictParm, out str);
        }

        public static void InvokeExeCommand(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out DataSet ds)
        {
            mnd.MethodExec(myDictParm, out ds);
        }

        public static void InvokeExeCommand<T>(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out IList<T> list)
        {
            mnd.MethodExec(myDictParm, out list);
        }

        public static void InvokeExeCommand<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, Form m, AbstCommand mnd, out IDictionary<TKey, TValue> dict)
        {
            mnd.MethodExec(myDictParm, out dict);
        }

    }

    public class ThreadParaObj
    {
        public Form m { get; set; }
        public AbstCommand mnd { get; set; }
        public Object obj { get; set; }
    }

    public class CommCommand
    {
        public AbstDALCommand dalCommand = null;
        public CommandOpType cmdOpType = CommandOpType.empty;
        public void MethodExec(Dictionary<string, MethodParamObj> myDictParm)
        {
            switch (this.cmdOpType)
            {
                case CommandOpType.Add:
                    dalCommand.AddCmd(myDictParm);
                    break;
                case CommandOpType.AddList:
                    dalCommand.AddListCmd(myDictParm);
                    break;
                case CommandOpType.AddSome:
                    dalCommand.AddSomeCmd(myDictParm);
                    break;
                case CommandOpType.Delete:
                    dalCommand.DeleteCmd(myDictParm);
                    break;
                case CommandOpType.DeleteList:
                    dalCommand.DeleteListCmd(myDictParm);
                    break;
                case CommandOpType.DeleteSome:
                    dalCommand.DeleteSomeCmd(myDictParm);
                    break;
                case CommandOpType.Update:
                    dalCommand.UpdateCmd(myDictParm);
                    break;
                case CommandOpType.UpdateList:
                    dalCommand.UpdateListCmd(myDictParm);
                    break;
                case CommandOpType.UpdateSome:
                    dalCommand.UpdateSomeCmd(myDictParm);
                    break;
                case CommandOpType.Select:
                    dalCommand.SelectCmd(myDictParm);
                    break;
                case CommandOpType.SelectList:
                    dalCommand.SelectListCmd(myDictParm);
                    break;
                case CommandOpType.SelectSome:
                    dalCommand.SelectSomeCmd(myDictParm);
                    break;
                case CommandOpType.SelectPage:
                    dalCommand.SelectPageCmd(myDictParm);
                    break;
                default:
                    break;
            }
        }
        public void MethodExec(Dictionary<string, MethodParamObj> myDictParm, out Object obj)
        {
            obj = null;
            switch (this.cmdOpType)
            {
                case CommandOpType.Add:
                    dalCommand.AddCmd(myDictParm, out obj);
                    break;
                case CommandOpType.AddList:
                    dalCommand.AddListCmd(myDictParm, out obj);
                    break;
                case CommandOpType.AddSome:
                    dalCommand.AddSomeCmd(myDictParm, out obj);
                    break;
                case CommandOpType.Delete:
                    dalCommand.DeleteCmd(myDictParm, out obj);
                    break;
                case CommandOpType.DeleteList:
                    dalCommand.DeleteListCmd(myDictParm, out obj);
                    break;
                case CommandOpType.DeleteSome:
                    dalCommand.DeleteSomeCmd(myDictParm, out obj);
                    break;
                case CommandOpType.Update:
                    dalCommand.UpdateCmd(myDictParm, out obj);
                    break;
                case CommandOpType.UpdateList:
                    dalCommand.UpdateListCmd(myDictParm, out obj);
                    break;
                case CommandOpType.UpdateSome:
                    dalCommand.UpdateSomeCmd(myDictParm, out obj);
                    break;
                case CommandOpType.Select:
                    dalCommand.SelectCmd(myDictParm, out obj);
                    break;
                case CommandOpType.SelectList:
                    dalCommand.SelectListCmd(myDictParm, out obj);
                    break;
                case CommandOpType.SelectSome:
                    dalCommand.SelectSomeCmd(myDictParm, out obj);
                    break;
                case CommandOpType.SelectPage:
                    dalCommand.SelectPageCmd(myDictParm, out obj);
                    break;
                default:
                    break;
            }

        }

        public void MethodExec(Dictionary<string, MethodParamObj> myDictParm, out String str)
        {
            str = null;
            switch (this.cmdOpType)
            {
                case CommandOpType.Add:
                    dalCommand.AddCmd(myDictParm, out str);
                    break;
                case CommandOpType.AddList:
                    dalCommand.AddListCmd(myDictParm, out str);
                    break;
                case CommandOpType.AddSome:
                    dalCommand.AddSomeCmd(myDictParm, out str);
                    break;
                case CommandOpType.Delete:
                    dalCommand.DeleteCmd(myDictParm, out str);
                    break;
                case CommandOpType.DeleteList:
                    dalCommand.DeleteListCmd(myDictParm, out str);
                    break;
                case CommandOpType.DeleteSome:
                    dalCommand.DeleteSomeCmd(myDictParm, out str);
                    break;
                case CommandOpType.Update:
                    dalCommand.UpdateCmd(myDictParm, out str);
                    break;
                case CommandOpType.UpdateList:
                    dalCommand.UpdateListCmd(myDictParm, out str);
                    break;
                case CommandOpType.UpdateSome:
                    dalCommand.UpdateSomeCmd(myDictParm, out str);
                    break;
                case CommandOpType.Select:
                    dalCommand.SelectCmd(myDictParm, out str);
                    break;
                case CommandOpType.SelectList:
                    dalCommand.SelectListCmd(myDictParm, out str);
                    break;
                case CommandOpType.SelectSome:
                    dalCommand.SelectSomeCmd(myDictParm, out str);
                    break;
                case CommandOpType.SelectPage:
                    dalCommand.SelectPageCmd(myDictParm, out str);
                    break;
                default:
                    break;
            }
        }

        public void MethodExec(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds)
        {
            ds = null;
            switch (this.cmdOpType)
            {
                case CommandOpType.Add:
                    dalCommand.AddCmd(myDictParm, out ds);
                    break;
                case CommandOpType.AddList:
                    dalCommand.AddListCmd(myDictParm, out ds);
                    break;
                case CommandOpType.AddSome:
                    dalCommand.AddSomeCmd(myDictParm, out ds);
                    break;
                case CommandOpType.Delete:
                    dalCommand.DeleteCmd(myDictParm, out ds);
                    break;
                case CommandOpType.DeleteList:
                    dalCommand.DeleteListCmd(myDictParm, out ds);
                    break;
                case CommandOpType.DeleteSome:
                    dalCommand.DeleteSomeCmd(myDictParm, out ds);
                    break;
                case CommandOpType.Update:
                    dalCommand.UpdateCmd(myDictParm, out ds);
                    break;
                case CommandOpType.UpdateList:
                    dalCommand.UpdateListCmd(myDictParm, out ds);
                    break;
                case CommandOpType.UpdateSome:
                    dalCommand.UpdateSomeCmd(myDictParm, out ds);
                    break;
                case CommandOpType.Select:
                    dalCommand.SelectCmd(myDictParm, out ds);
                    break;
                case CommandOpType.SelectList:
                    dalCommand.SelectListCmd(myDictParm, out ds);
                    break;
                case CommandOpType.SelectSome:
                    dalCommand.SelectSomeCmd(myDictParm, out ds);
                    break;
                case CommandOpType.SelectPage:
                    dalCommand.SelectPageCmd(myDictParm, out ds);
                    break;
                default:
                    break;
            }
        }

        public void MethodExec<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list)
        {
            list = null;
            switch (this.cmdOpType)
            {
                case CommandOpType.Add:
                    dalCommand.AddCmd(myDictParm, out list);
                    break;
                case CommandOpType.AddList:
                    dalCommand.AddListCmd(myDictParm, out list);
                    break;
                case CommandOpType.AddSome:
                    dalCommand.AddSomeCmd(myDictParm, out list);
                    break;
                case CommandOpType.Delete:
                    dalCommand.DeleteCmd(myDictParm, out list);
                    break;
                case CommandOpType.DeleteList:
                    dalCommand.DeleteListCmd(myDictParm, out list);
                    break;
                case CommandOpType.DeleteSome:
                    dalCommand.DeleteSomeCmd(myDictParm, out list);
                    break;
                case CommandOpType.Update:
                    dalCommand.UpdateCmd(myDictParm, out list);
                    break;
                case CommandOpType.UpdateList:
                    dalCommand.UpdateListCmd(myDictParm, out list);
                    break;
                case CommandOpType.UpdateSome:
                    dalCommand.UpdateSomeCmd(myDictParm, out list);
                    break;
                case CommandOpType.Select:
                    dalCommand.SelectCmd(myDictParm, out list);
                    break;
                case CommandOpType.SelectList:
                    dalCommand.SelectListCmd(myDictParm, out list);
                    break;
                case CommandOpType.SelectSome:
                    dalCommand.SelectSomeCmd(myDictParm, out list);
                    break;
                case CommandOpType.SelectPage:
                    dalCommand.SelectPageCmd(myDictParm, out list);
                    break;
                default:
                    break;
            }
        }

        public void MethodExec<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict)
        {
            dict = null;
            switch (this.cmdOpType)
            {
                case CommandOpType.Add:
                    dalCommand.AddCmd(myDictParm, out dict);
                    break;
                case CommandOpType.AddList:
                    dalCommand.AddListCmd(myDictParm, out dict);
                    break;
                case CommandOpType.AddSome:
                    dalCommand.AddSomeCmd(myDictParm, out dict);
                    break;
                case CommandOpType.Delete:
                    dalCommand.DeleteCmd(myDictParm, out dict);
                    break;
                case CommandOpType.DeleteList:
                    dalCommand.DeleteListCmd(myDictParm, out dict);
                    break;
                case CommandOpType.DeleteSome:
                    dalCommand.DeleteSomeCmd(myDictParm, out dict);
                    break;
                case CommandOpType.Update:
                    dalCommand.UpdateCmd(myDictParm, out dict);
                    break;
                case CommandOpType.UpdateList:
                    dalCommand.UpdateListCmd(myDictParm, out dict);
                    break;
                case CommandOpType.UpdateSome:
                    dalCommand.UpdateSomeCmd(myDictParm, out dict);
                    break;
                case CommandOpType.Select:
                    dalCommand.SelectCmd(myDictParm, out dict);
                    break;
                case CommandOpType.SelectList:
                    dalCommand.SelectListCmd(myDictParm, out dict);
                    break;
                case CommandOpType.SelectSome:
                    dalCommand.SelectSomeCmd(myDictParm, out dict);
                    break;
                case CommandOpType.SelectPage:
                    dalCommand.SelectPageCmd(myDictParm, out dict);
                    break;
                default:
                    break;
            }
        }

        public void MethodExec(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm)
        {
            outMyDictParm = null;
            switch (this.cmdOpType)
            {
                case CommandOpType.Add:
                    dalCommand.AddCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.AddList:
                    dalCommand.AddListCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.AddSome:
                    dalCommand.AddSomeCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.Delete:
                    dalCommand.DeleteCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.DeleteList:
                    dalCommand.DeleteListCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.DeleteSome:
                    dalCommand.DeleteSomeCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.Update:
                    dalCommand.UpdateCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.UpdateList:
                    dalCommand.UpdateListCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.UpdateSome:
                    dalCommand.UpdateSomeCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.Select:
                    dalCommand.SelectCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.SelectList:
                    dalCommand.SelectListCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.SelectSome:
                    dalCommand.SelectSomeCmd(myDictParm, out outMyDictParm);
                    break;
                case CommandOpType.SelectPage:
                    dalCommand.SelectPageCmd(myDictParm, out outMyDictParm);
                    break;
                default:
                    break;
            }
        }
    }

    public abstract class AbstCommand
    {
        public object cmdSourceObj = null;
        public object cmdForm = null;
        public AbstDALCommand dalCommand = null;
        public virtual void MethodExec(Dictionary<string, MethodParamObj> myDictParm) { }
        public virtual void MethodExec(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        public virtual void MethodExec(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }
        public virtual void MethodExec(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        public virtual void MethodExec(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }
        public virtual void MethodExec<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }
        public virtual void MethodExec<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }
    }
    public class btn1Cmd : AbstCommand
    {
        public override void MethodExec(Dictionary<string, MethodParamObj> myDictParm, out Object obj)
        {
            for (int i = 0; i < 1000000000; i++)
            {
                i += 1;
            }
            MessageBox.Show("this按钮1命令" + this.cmdSourceObj.ToString() + ";" + this.cmdForm.ToString());
            //Thread.Sleep(5000);
            CommCommand ccmd = new CommCommand();
            ccmd.cmdOpType = CommandOpType.Add;
            //ccmd.dalCommand = new UserDALCommand();
            ccmd.dalCommand = this.dalCommand;
            ccmd.MethodExec(myDictParm, out obj);
        }
    }
    public class btn2Cmd : AbstCommand
    {
        public override void MethodExec(Dictionary<string, MethodParamObj> myDictParm)
        {
            for (int i = 0; i < 1000000000; i++)
            {
                i += 1;
            }
            MessageBox.Show("this按钮2命令" + this.cmdSourceObj.ToString() + ";" + this.cmdForm.ToString());
            //Thread.Sleep(5000);
            CommCommand ccmd = new CommCommand();
            ccmd.cmdOpType = CommandOpType.Delete;
            //ccmd.dalCommand = new UserDALCommand();
            ccmd.dalCommand = this.dalCommand;
            ccmd.MethodExec(myDictParm);
        }
    }
    public class btn3Cmd : AbstCommand
    {
        public override void MethodExec(Dictionary<string, MethodParamObj> myDictParm)
        {
            for (int i = 0; i < 1000000000; i++)
            {
                i += 1;
            }
            MessageBox.Show("this按钮3命令" + this.cmdSourceObj.ToString() + ";" + this.cmdForm.ToString());
            //Thread.Sleep(5000);
            CommCommand ccmd = new CommCommand();
            ccmd.cmdOpType = CommandOpType.Update;
            //ccmd.dalCommand = new BussDomainModel1DALCommand();
            ccmd.dalCommand = this.dalCommand;
            ccmd.MethodExec(myDictParm);
        }
    }
    public class btn4Cmd : AbstCommand
    {
        public override void MethodExec(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds)
        {
            for (int i = 0; i < 1000000000; i++)
            {
                i += 1;
            }
            //MessageBox.Show("this按钮4命令" + this.cmdSourceObj.ToString() + ";" + this.cmdForm.ToString());
            //Thread.Sleep(5000);
            CommCommand ccmd = new CommCommand();
            ccmd.cmdOpType = CommandOpType.Select;
            ccmd.dalCommand = this.dalCommand;
            ccmd.MethodExec(myDictParm, out ds);
        }
    }
}
