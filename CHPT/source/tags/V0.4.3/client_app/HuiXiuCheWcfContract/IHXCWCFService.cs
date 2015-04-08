using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using Model;
namespace HuiXiuCheWcfContract
{
    //[ServiceContract(Name = "HXCWCFService", SessionMode = SessionMode.Allowed)]
    [ServiceContract(Name = "HXCWCFService")]
    public interface IHXCWCFService
    {

        //[OperationContract]
        //string GetData(int value);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: 在此添加您的服务操作

        [OperationContract]
        string TestConnect();

        [OperationContract]
        int GetOnLineUserCount(string str);

        /// <summary> 获取所使用中的有帐套列表
        /// </summary>
        /// <returns>返回resp对象</returns>
        [OperationContract]
        string GetAccList();

        [OperationContract]
        string LoginIn(string str);

        [OperationContract]
        string LoginOut(string str);

        [OperationContract]
        string JsonOperate(string strJson);

        /// <summary> webservice方法调用
        /// </summary>
        /// <param name="strJson">json串</param>
        /// <returns>返回RespFunStruct的json串</returns>
        [OperationContract]
        string HandleWebServ(string strJson);

        //[OperationContract]
        //string GetSysfunction(string strJson);

        //[OperationContract]
        //System.IO.Stream ReadImg();

        //[OperationContract]
        //void ReceiveImg(System.IO.Stream stream);      

    }



    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
