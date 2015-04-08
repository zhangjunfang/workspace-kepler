using System;
using System.Collections.Generic;

namespace Kord.LogService
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 内部消息
        /// </summary>
        Internal = 1,
        /// <summary>
        /// 用户定义消息
        /// </summary>
        UserDefine = 2
    }

    /// <summary>
    /// 
    /// </summary>
    public struct ExceptionDescriber
    {
        /// <summary>
        /// 异常类型
        /// </summary>
        public MessageType ExceptionType;
        /// <summary>
        /// 异常Id
        /// </summary>
        public UInt32 ExceptionId;
        /// <summary>
        /// 异常名称
        /// </summary>
        public String ExceptionName;
        /// <summary>
        /// 类名称
        /// </summary>
        public String ClassName;
        /// <summary>
        /// 方法名称
        /// </summary>
        public String MethodName;
        /// <summary>
        /// 异常备注
        /// </summary>
        public String ExceptionDescription;
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionHelper
    {
        private static readonly Dictionary<UInt32, ExceptionDescriber> ExceptionDict = null;

        static ExceptionHelper()
        {
            ExceptionDict = new Dictionary<uint, ExceptionDescriber>();
            RegisterInternalException(0, "UndefinedException:", "Undefined:", "Undefined:", "This Exception is not defined:");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionType"></param>
        /// <param name="exceptionID"></param>
        /// <param name="exceptionName"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="exceptionDescription"></param>
        public static void RegisterException(MessageType exceptionType, UInt32 exceptionID, String exceptionName, String className,
            String methodName, String exceptionDescription)
        {
            if (ExceptionDict.ContainsKey(exceptionID))
                return;
            var ed = new ExceptionDescriber
            {
                ExceptionType = exceptionType,
                ExceptionId = exceptionID,
                ExceptionName = exceptionName,
                ClassName = className,
                MethodName = methodName,
                ExceptionDescription = exceptionDescription
            };

            ExceptionDict.Add(ed.ExceptionId, ed);
        }

        //public static void RegisterWAPException(FieldCollection fc)
        //{
        //    string exceptionType = GetExceptionType(fc);
        //    if (exceptionType != "")
        //    {
        //        eExceptionType t = (eExceptionType)Enum.Parse(typeof(eExceptionType), exceptionType, true);

        //        RegisterWAPException(t, (UInt32)GetExceptionID(fc), GetExceptionName(fc), GetClassName(fc),
        //                             GetMethodName(fc), GetExceptionDescription(fc));
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionID"></param>
        /// <param name="exceptionName"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="exceptionDescription"></param>
        public static void RegisterInternalException(UInt32 exceptionID, String exceptionName, String className,
            String methodName, String exceptionDescription)
        {
            RegisterException(MessageType.Internal, exceptionID, exceptionName, className, methodName, exceptionDescription);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionID"></param>
        /// <param name="exceptionName"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="exceptionDescription"></param>
        public static void RegisterUserException(UInt32 exceptionID, String exceptionName, String className,
            String methodName, String exceptionDescription)
        {
            RegisterException(MessageType.UserDefine, exceptionID, exceptionName, className, methodName, exceptionDescription);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionID"></param>
        /// <returns></returns>
        public static ExceptionDescriber GetExceptionDescriber(UInt32 exceptionID)
        {
            var ed = new ExceptionDescriber();

            if (ExceptionDict == null || exceptionID <= 0) return ed;
            if (ExceptionDict.ContainsKey(exceptionID))
            {
                ExceptionDict.TryGetValue(exceptionID, out ed);
            }

            return ed;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionID"></param>
        /// <returns></returns>
        public static string GetExceptionMessage(UInt32 exceptionID)
        {
            if (GetExceptionDescriber(exceptionID).ExceptionDescription != null)
                return GetExceptionDescriber(exceptionID).ExceptionDescription;
            else
                return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionID"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="exDescription"></param>
        /// <param name="extraInfo"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static GeneralException NewException(UInt32 exceptionID, string className, string methodName, string exDescription, string extraInfo, Exception ex)
        {
            return ex == null ? new GeneralException(exceptionID, className, methodName, extraInfo, exDescription, "") : new GeneralException(exceptionID, className, methodName, extraInfo, exDescription, ex.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionID"></param>
        /// <param name="extraInfo"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static GeneralException GetException(UInt32 exceptionID, String extraInfo, Exception ex)
        {
            var ed = GetExceptionDescriber(exceptionID);

            if (ed.ExceptionId != 0)
                return NewException(ed.ExceptionId, ed.ClassName, ed.MethodName, ed.ExceptionDescription, extraInfo, ex);
            ed = GetExceptionDescriber(0);
            ed.ExceptionName = ed.ExceptionName + exceptionID;
            ed.ClassName = ed.ClassName + exceptionID;
            ed.MethodName = ed.MethodName + exceptionID;
            ed.ExceptionDescription = ed.ExceptionDescription + exceptionID;

            return NewException(ed.ExceptionId, ed.ClassName, ed.MethodName, ed.ExceptionDescription, extraInfo, ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class GeneralException : ApplicationException
    {
        private readonly string _eventName;

        /// <summary>
        /// 
        /// </summary>
        public uint Id { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClassName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string MethodName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string EventName
        {
            get { return _eventName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorInfo { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exId"></param>
        /// <param name="exClassName"></param>
        /// <param name="exMethodName"></param>
        /// <param name="exEventName"></param>
        /// <param name="exDescription"></param>
        /// <param name="exErrorInfo"></param>
        public GeneralException(UInt32 exId, string exClassName, string exMethodName,
            string exEventName, string exDescription, string exErrorInfo)
        {
            Id = exId;
            ClassName = exClassName;
            MethodName = exMethodName;
            _eventName = exEventName;
            ErrorInfo = exErrorInfo;
            Description = exDescription;
        }
        /// <summary>
        /// Format:ID|Type|FrmName|ControlName|Event|Message
        /// </summary>
        /// <returns></returns>
        public override string Message
        {
            get
            {
                var msg = InnerException == null ? base.Message : string.Format("{0}, {1}", base.Message, InnerException.Message);

                msg = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", Id, ClassName, MethodName, _eventName, ErrorInfo, Description, msg);
                return msg;
            }
        }
    }
}
