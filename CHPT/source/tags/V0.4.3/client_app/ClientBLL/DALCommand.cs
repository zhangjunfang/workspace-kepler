using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
namespace ClientBLL
{
    public abstract class AbstDALCommand
    {
        //++++++++++++++++++++++
        public virtual void AddCmd(Dictionary<string, MethodParamObj> myDictParm) { }
        public virtual void UpdateCmd(Dictionary<string, MethodParamObj> myDictParm) { }
        public virtual void SelectCmd(Dictionary<string, MethodParamObj> myDictParm) { }
        public virtual void DeleteCmd(Dictionary<string, MethodParamObj> myDictParm) { }
        public virtual void SelectPageCmd(Dictionary<string, MethodParamObj> myDictParm) { }

        public virtual void AddListCmd(Dictionary<string, MethodParamObj> myDictParm) { }
        public virtual void UpdateListCmd(Dictionary<string, MethodParamObj> myDictParm) { }
        public virtual void SelectListCmd(Dictionary<string, MethodParamObj> myDictParm) { }
        public virtual void DeleteListCmd(Dictionary<string, MethodParamObj> myDictParm) { }

        public virtual void AddSomeCmd(Dictionary<string, MethodParamObj> myDictParm) { }
        public virtual void UpdateSomeCmd(Dictionary<string, MethodParamObj> myDictParm) { }
        public virtual void SelectSomeCmd(Dictionary<string, MethodParamObj> myDictParm) { }
        public virtual void DeleteSomeCmd(Dictionary<string, MethodParamObj> myDictParm) { }
        //++++++++++++++++++++++
        public virtual void AddCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        public virtual void UpdateCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        public virtual void SelectCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        public virtual void DeleteCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        public virtual void SelectPageCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }

        public virtual void AddListCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        public virtual void UpdateListCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        public virtual void SelectListCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        public virtual void DeleteListCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }

        public virtual void AddSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        public virtual void UpdateSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        public virtual void SelectSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        public virtual void DeleteSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out String str) { str = null; }
        //++++++++++++++++++++++
        public virtual void AddCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        public virtual void UpdateCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        public virtual void SelectCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        public virtual void DeleteCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        public virtual void SelectPageCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }

        public virtual void AddListCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        public virtual void UpdateListCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        public virtual void SelectListCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        public virtual void DeleteListCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }

        public virtual void AddSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        public virtual void UpdateSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        public virtual void SelectSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        public virtual void DeleteSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = null; }
        //++++++++++++++++++++++
        public virtual void AddCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }
        public virtual void UpdateCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }
        public virtual void SelectCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }
        public virtual void DeleteCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }

        public virtual void SelectPageCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }

        public virtual void AddListCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }
        public virtual void UpdateListCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }
        public virtual void SelectListCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }
        public virtual void DeleteListCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }

        public virtual void AddSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }
        public virtual void UpdateSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }
        public virtual void SelectSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }
        public virtual void DeleteSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds) { ds = null; }
        //++++++++++++++++++++++
        public virtual void AddCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }
        public virtual void UpdateCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }
        public virtual void SelectCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }
        public virtual void DeleteCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }

        public virtual void SelectPageCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }

        public virtual void AddListCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }
        public virtual void UpdateListCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }
        public virtual void SelectListCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }
        public virtual void DeleteListCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }

        public virtual void AddSomeCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }
        public virtual void UpdateSomeCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }
        public virtual void SelectSomeCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }
        public virtual void DeleteSomeCmd<T>(Dictionary<string, MethodParamObj> myDictParm, out IList<T> list) { list = null; }
        //++++++++++++++++++++++
        public virtual void AddCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }
        public virtual void UpdateCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }
        public virtual void SelectCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }
        public virtual void DeleteCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }

        public virtual void SelectPageCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }

        public virtual void AddListCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }
        public virtual void UpdateListCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }
        public virtual void SelectListCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }
        public virtual void DeleteListCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }

        public virtual void AddSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }
        public virtual void UpdateSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }
        public virtual void SelectSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }
        public virtual void DeleteSomeCmd(Dictionary<string, MethodParamObj> myDictParm, out Dictionary<string, MethodParamObj> outMyDictParm) { outMyDictParm = null; }
        //++++++++++++++++++++++
        public virtual void AddCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }
        public virtual void UpdateCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }
        public virtual void SelectCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }
        public virtual void DeleteCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }

        public virtual void SelectPageCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }

        public virtual void AddListCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }
        public virtual void UpdateListCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }
        public virtual void SelectListCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }
        public virtual void DeleteListCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }

        public virtual void AddSomeCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }
        public virtual void UpdateSomeCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }
        public virtual void SelectSomeCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }
        public virtual void DeleteSomeCmd<TKey, TValue>(Dictionary<string, MethodParamObj> myDictParm, out IDictionary<TKey, TValue> dict) { dict = null; }
    }

    public class UserDALCommand : AbstDALCommand
    {
        public override void AddCmd(Dictionary<string, MethodParamObj> myDictParm) { MessageBox.Show("UserDALCommand---Add命令"); }
        public override void UpdateCmd(Dictionary<string, MethodParamObj> myDictParm) { MessageBox.Show("UserDALCommand---Update命令"); }
        public override void AddCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = "111Add"; }
        public override void UpdateCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = "111Update"; }
        public override void SelectCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds)
        {
            DataSet dsClass = new DataSet();
            //创建班级表
            DataTable dtClass = new DataTable("User");
            //创建班级名称列
            DataColumn dcClassName = new DataColumn("UserName", typeof(string));
            //创建年级ID列
            DataColumn dcGradeID = new DataColumn("UserID", typeof(int));
            //将定义好列添加到班级表中
            dtClass.Columns.Add(dcClassName);
            dtClass.Columns.Add(dcGradeID);
            //创建一个新的数据行
            DataRow drClass = dtClass.NewRow();
            drClass["UserName"] = "用户1";
            drClass["UserID"] = "1";
            //将新的数据行插入班级表中
            dtClass.Rows.Add(drClass);
            drClass = dtClass.NewRow();
            drClass["UserName"] = "用户2";
            drClass["UserID"] = "2";
            //将新的数据行插入班级表中
            dtClass.Rows.Add(drClass);
            drClass = dtClass.NewRow();
            drClass["UserName"] = "用户3";
            drClass["UserID"] = "3";
            //将班级表添加到DataSet中
            dsClass.Tables.Add(dtClass);
            ds = dsClass;
        }
        public override void DeleteCmd(Dictionary<string, MethodParamObj> myDictParm) { MessageBox.Show("UserDALCommand---Delete命令"); }
    }
    public class OrderDALCommand : AbstDALCommand
    {
        public override void AddCmd(Dictionary<string, MethodParamObj> myDictParm) { MessageBox.Show("OrderDALCommand---Add命令"); }
        public override void UpdateCmd(Dictionary<string, MethodParamObj> myDictParm) { MessageBox.Show("OrderDALCommand---Update命令"); }
        public override void AddCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = "222Add"; }
        public override void UpdateCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = "222Update"; }
        public override void SelectCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds)
        {
            DataSet dsClass = new DataSet();
            //创建班级表
            DataTable dtClass = new DataTable("Order");
            //创建班级名称列
            DataColumn dcClassName = new DataColumn("OrderName", typeof(string));
            //创建年级ID列
            DataColumn dcGradeID = new DataColumn("OrderID", typeof(int));
            //将定义好列添加到班级表中
            dtClass.Columns.Add(dcClassName);
            dtClass.Columns.Add(dcGradeID);
            //创建一个新的数据行
            DataRow drClass = dtClass.NewRow();
            drClass["OrderName"] = "订单1";
            drClass["OrderID"] = "1";
            //将新的数据行插入班级表中
            dtClass.Rows.Add(drClass);
            drClass = dtClass.NewRow();
            drClass["OrderName"] = "订单2";
            drClass["OrderID"] = "2";
            //将新的数据行插入班级表中
            dtClass.Rows.Add(drClass);
            drClass = dtClass.NewRow();
            drClass["OrderName"] = "订单3";
            drClass["OrderID"] = "3";
            //将班级表添加到DataSet中
            dsClass.Tables.Add(dtClass);
            ds = dsClass;
        }
    }
    public class BussDomainModel1DALCommand : AbstDALCommand
    {
        public override void AddCmd(Dictionary<string, MethodParamObj> myDictParm) { MessageBox.Show("BussDomainModel1DALCommand---Add命令"); }
        public override void UpdateCmd(Dictionary<string, MethodParamObj> myDictParm) { MessageBox.Show("BussDomainModel1DALCommand---Update命令"); }
    }
    public class BussDomainModel2DALCommand : AbstDALCommand
    {
        public override void AddCmd(Dictionary<string, MethodParamObj> myDictParm, out Object obj) { obj = "BussDomainModel2DALCommand---Add命令"; }
        //public override void AddCmd(Dictionary<string, MethodParamObj> myDictParm) { MessageBox.Show("BussDomainModel2DALCommand---Add命令"); }
        public override void UpdateCmd(Dictionary<string, MethodParamObj> myDictParm) { MessageBox.Show("BussDomainModel2DALCommand---Update命令"); }
        //IDAL调用ICommon,再调用SQLDAL的Common或者对应的Model(单表、交叉、联合)的DAL操作
        public override void SelectCmd(Dictionary<string, MethodParamObj> myDictParm, out DataSet ds)
        {
            int c = myDictParm.Count;
            User myUser = JsonConvert.DeserializeObject<User>(myDictParm["userModel"].JSONStr);
            DataSet dsClass000 = JsonConvert.DeserializeObject<DataSet>(myDictParm["ClassDataSet"].JSONStr);
            List<BookChapter> myList = JsonConvert.DeserializeObject<List<BookChapter>>(myDictParm["BookChapterList"].JSONStr);
            int iNum = JsonConvert.DeserializeObject<int>(myDictParm["DepID"].JSONStr);
            DataSet dsClass = new DataSet();
            //创建班级表
            DataTable dtClass = new DataTable("Class");
            //创建班级名称列
            DataColumn dcClassName = new DataColumn("ClassName", typeof(string));
            //创建年级ID列
            DataColumn dcGradeID = new DataColumn("GradeID", typeof(int));
            //将定义好列添加到班级表中
            dtClass.Columns.Add(dcClassName);
            dtClass.Columns.Add(dcGradeID);
            //创建一个新的数据行
            DataRow drClass = dtClass.NewRow();
            drClass["className"] = "测试1班";
            drClass["gradeID"] = "1";
            //将新的数据行插入班级表中
            dtClass.Rows.Add(drClass);
            drClass = dtClass.NewRow();
            drClass["className"] = "测试2班";
            drClass["gradeID"] = "2";
            //将新的数据行插入班级表中
            dtClass.Rows.Add(drClass);
            drClass = dtClass.NewRow();
            drClass["className"] = "测试3班";
            drClass["gradeID"] = "3";
            //将班级表添加到DataSet中
            dsClass.Tables.Add(dtClass);
            ds = dsClass;
        }
        public override void DeleteCmd(Dictionary<string, MethodParamObj> myDictParm) { MessageBox.Show("BussDomainModel2DALCommand---Delete命令"); }
    }
}
