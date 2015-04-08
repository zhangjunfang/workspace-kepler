using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Kord;

namespace LogService.TXT
{
    /// <summary>
    /// 日志分割信息
    /// </summary>
    public struct StreamFileNameSplitFormat
    {
        #region Filed -- 字段
        /// <summary>
        /// 默认日志时间格式
        /// </summary>
        public static readonly string DefaultDateTimeFormat = "yyyyMMdd";
        /// <summary>
        /// 默认日志文件分割大小
        /// </summary>
        public static readonly string DefaultFileSizeFormat = "M*10";
        /// <summary>
        /// 默认文件名称分隔符
        /// </summary>
        public static readonly string DefaultFileNamePartSplitStr = "_";
        /// <summary>
        /// 默认日志内容分隔符
        /// </summary>
        public static readonly char DefaultFileNameScriptSplitChar = '|';
        /// <summary>
        /// 默认日志文件扩展名
        /// </summary>
        public static readonly string DefaultFileExpandedName = ".txt";
        /// <summary>
        /// 默认是否追加
        /// </summary>
        public static readonly bool DefaultISAPPEND = false;

        /// <summary>
        /// 日志时间格式
        /// </summary>
        public string DateTimeFormat;
        /// <summary>
        /// 日志文件分割大小
        /// </summary>
        public string FileSplitSizeUnit;
        /// <summary>
        /// 文件最大容量
        /// </summary>
        public UInt64 FileMaxSize;
        /// <summary>
        /// 基础文件名
        /// </summary>
        public string BaseFileName;
        /// <summary>
        /// 文件名分隔符
        /// </summary>
        public string FileNameSplitChar;
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExpandedName;
        /// <summary>
        /// 是否进行追加
        /// </summary>
        public bool IsAppend;
        #endregion

        #region Method -- 方法
        /// <summary>
        /// 日志分割信息
        /// </summary>
        /// <param name="logFileName">ProcessEngine|yyyyMMdd|M*5|_|.txt</param>
        /// <returns></returns>
        public static StreamFileNameSplitFormat GetFileSplitFormat(string logFileName)
        {
            var theSf = new StreamFileNameSplitFormat();
            var tmpArray = logFileName.Split(DefaultFileNameScriptSplitChar);
            var dateTimeFormat = "";
            var fileSizeFormat = "";
            var splitChar = "";
            var fileExpandedName = "";
            theSf.BaseFileName = tmpArray[0];

            #region LogFileName信息
            switch (tmpArray.Length)
            {
                case 1:
                    dateTimeFormat = DefaultDateTimeFormat;
                    fileSizeFormat = DefaultFileSizeFormat;
                    splitChar = DefaultFileNamePartSplitStr;
                    fileExpandedName = DefaultFileExpandedName;
                    break;
                case 2:
                    dateTimeFormat = tmpArray[1];
                    fileSizeFormat = DefaultFileSizeFormat;
                    splitChar = DefaultFileNamePartSplitStr;
                    fileExpandedName = DefaultFileExpandedName;
                    break;
                case 3:
                    dateTimeFormat = tmpArray[1];
                    fileSizeFormat = tmpArray[2];
                    splitChar = DefaultFileNamePartSplitStr;
                    fileExpandedName = DefaultFileExpandedName;
                    break;
                case 4:
                    dateTimeFormat = tmpArray[1];
                    fileSizeFormat = tmpArray[2];
                    splitChar = tmpArray[3];
                    fileExpandedName = DefaultFileExpandedName;
                    break;
                case 5:
                    dateTimeFormat = tmpArray[1];
                    fileSizeFormat = tmpArray[2];
                    splitChar = tmpArray[3];
                    fileExpandedName = tmpArray[4];
                    break;

            }
            #endregion

            UInt64 outValue;
            string splitSizeUnit;

            #region Get FileSizeFormat
            var isAppend = DefaultISAPPEND;
            bool r;
            var tmpSize = fileSizeFormat.Split('*');
            switch (tmpSize.Length)
            {
                case 1:
                    r = UInt64.TryParse(tmpSize[0], out outValue);
                    if (r)
                    {
                        splitSizeUnit = "B";
                    }
                    else
                    {
                        outValue = 1;
                        splitSizeUnit = tmpSize[0];
                    }
                    break;
                case 2:
                    splitSizeUnit = tmpSize[0];
                    r = UInt64.TryParse(tmpSize[1], out outValue);
                    if (!r)
                    {
                        outValue = 1;
                    }
                    break;
                default:
                    splitSizeUnit = tmpSize[0];
                    r = UInt64.TryParse(tmpSize[1], out outValue);
                    if (!r)
                    {
                        outValue = 1;
                    }
                    isAppend = tmpSize[2].ToUpper() == "APPEND";
                    break;
            }
            #endregion

            theSf.FileMaxSize = outValue;
            theSf.FileSplitSizeUnit = splitSizeUnit;
            theSf.FileNameSplitChar = splitChar;
            theSf.DateTimeFormat = dateTimeFormat;
            theSf.FileExpandedName = fileExpandedName;
            theSf.IsAppend = isAppend;
            return theSf;
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class StreamFileSplitManager
    {
        #region Field -- 字段
        private StreamFileNameSplitFormat _theSplitFormat;
        /// <summary>
        /// 文件时间名称
        /// </summary>
        public string DateFileName;
        /// <summary>
        /// 文件大小统计
        /// </summary>
        public UInt64 TheFileTotalSize;
        /// <summary>
        /// 递增编号
        /// </summary>
        public UInt64 SeqNo;
        /// <summary>
        /// 是否首次运行
        /// </summary>
        public bool IsFristRun;
        /// <summary>
        /// 没有递增格式的文件名
        /// </summary>
        public string NoSeqFileName;
        /// <summary>
        /// 基础文件名
        /// </summary>
        public string BaseFileName
        {
            get
            {
                return _theSplitFormat.BaseFileName;
            }
        }
        /// <summary>
        /// 是否追加
        /// </summary>
        public bool IsAppend
        {
            get
            {
                return _theSplitFormat.IsAppend;
            }
        }
        /// <summary>
        /// 文件最大大小
        /// </summary>
        public UInt64 FileMaxSizeBytes
        {
            get
            {
                var convetValue = ConvertSize(_theSplitFormat.FileSplitSizeUnit, 1);
                return _theSplitFormat.FileMaxSize * convetValue;
            }
        }
        #endregion

        /// <summary>
        /// 根据字符串获取日志分割信息
        /// </summary>
        /// <param name="logFileName"></param>
        public StreamFileSplitManager(string logFileName)
        {
            _theSplitFormat = StreamFileNameSplitFormat.GetFileSplitFormat(logFileName);
            IsFristRun = true;
        }
        /// <summary>
        /// 设置日志分割信息
        /// </summary>
        /// <param name="splitFormat"></param>
        public StreamFileSplitManager(StreamFileNameSplitFormat splitFormat)
        {
            _theSplitFormat = splitFormat;
            IsFristRun = true;
        }
        /// <summary>
        /// 递增编号
        /// </summary>
        /// <param name="seqNum"></param>
        public void AddSeqNo(UInt16 seqNum)
        {
            var convetValue = ConvertSize(_theSplitFormat.FileSplitSizeUnit, 1);
            TheFileTotalSize = TheFileTotalSize + seqNum * convetValue * _theSplitFormat.FileMaxSize;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theRecSize"></param>
        /// <returns></returns>
        public string GetNewFileName(UInt64 theRecSize)
        {
            var convetValue = ConvertSize(_theSplitFormat.FileSplitSizeUnit, 1);
            TheFileTotalSize = TheFileTotalSize + theRecSize;
            DateFileName = DateTime.Now.ToString(_theSplitFormat.DateTimeFormat);
            SeqNo = TheFileTotalSize / (convetValue * _theSplitFormat.FileMaxSize);

            var tempNoSeqFileName = string.Format("{0}{2}{1}", BaseFileName,
                                                        DateFileName,
                                                        _theSplitFormat.FileNameSplitChar);
            if (tempNoSeqFileName != NoSeqFileName)
            {
                NoSeqFileName = tempNoSeqFileName;
                TheFileTotalSize = theRecSize;
                SeqNo = 0;
            }
            var fileName = string.Format("{0}{2}{1}{3}", NoSeqFileName,
                SeqNo,
                _theSplitFormat.FileNameSplitChar,
                _theSplitFormat.FileExpandedName);
            return fileName;
        }
        private UInt64 ConvertSize(string sizeUnit, UInt64 baseSize)
        {
            UInt64 resultSize = 0;
            switch (sizeUnit)
            {
                case "G":
                    resultSize = ConvertSize("M", baseSize) * 1024;
                    break;
                case "M":
                    resultSize = ConvertSize("KB", baseSize) * 1024;
                    break;
                case "KB":
                    resultSize = ConvertSize("B", baseSize) * 1024;
                    break;
                case "B":
                    resultSize = baseSize;
                    break;
            }
            return resultSize;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class StreamFileLPS : ILogPersistenceService
    {
        /// <summary>
        /// 
        /// </summary>
        public LogServiceConfig LogConfig;
        private String _fileName;
        /// <summary>
        /// 
        /// </summary>
        public int TotalSize = 0;
        private StreamFileSplitManager _theStreamFileSplitManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logcfg"></param>
        public StreamFileLPS(LogServiceConfig logcfg)
        {
            LogConfig = logcfg;
            _theStreamFileSplitManager = new StreamFileSplitManager(TXTLogFactory.GetLogFileName(logcfg));
            _fileName = _theStreamFileSplitManager.GetNewFileName(1);
        }

        public void SetPersistParam(String setting)
        {
            _theStreamFileSplitManager = new StreamFileSplitManager(setting);
            _fileName = _theStreamFileSplitManager.GetNewFileName(1);
        }

        public void Save(List<LogRecord> logRecordList)
        {
            lock (logRecordList)
            {
                var filePath = TXTLogFactory.GetLogFilePath(LogConfig) + TXTLogFactory.GetLogSubFolder(LogConfig);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var file = filePath + "\\" + _fileName;

                if (_theStreamFileSplitManager.IsFristRun)
                {
                    bool rlt;
                    do
                    {
                        file = filePath + "\\" + _fileName;
                        rlt = File.Exists(file);
                        if (!rlt) continue;
                        if (_theStreamFileSplitManager.IsAppend)
                        {
                            var fi = new FileInfo(file);
                            if ((UInt64)fi.Length >= _theStreamFileSplitManager.FileMaxSizeBytes)
                            {
                                _theStreamFileSplitManager.AddSeqNo(1);
                                _fileName = _theStreamFileSplitManager.GetNewFileName(1);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            _theStreamFileSplitManager.AddSeqNo(1);
                            _fileName = _theStreamFileSplitManager.GetNewFileName(1);
                        }
                    } while (rlt);
                    _theStreamFileSplitManager.IsFristRun = false;
                }
                var streamWrite = new StreamWriter(file, true);

                try
                {
                    //多线程可能导致集合不同步
                    //foreach (var record in logRecordList
                    //{
                    //    var recByteSize = (UInt16)(Encoding.Default.GetByteCount(record.LogMessage) + 2);
                    //    var tempfileName = _theStreamFileSplitManager.GetNewFileName(recByteSize);

                    //    if (tempfileName != _fileName)
                    //    {
                    //        streamWrite.Flush();
                    //        streamWrite.Close();
                    //        _fileName = tempfileName;
                    //        file = filePath + "\\" + _fileName;
                    //        streamWrite = new StreamWriter(file, true);
                    //    }
                    //    streamWrite.WriteLine(record.LogMessage);
                    //}

                    do
                    {
                        var record = logRecordList[0];
                        if (record == null || record.LogMessage == null)
                        {
                            logRecordList.Remove(record);
                            continue;
                        }
                        var recByteSize = (UInt16)(Encoding.Default.GetByteCount(record.LogMessage) + 2);
                        var tempfileName = _theStreamFileSplitManager.GetNewFileName(recByteSize);

                        if (tempfileName != _fileName)
                        {
                            streamWrite.Flush();
                            streamWrite.Close();
                            _fileName = tempfileName;
                            file = filePath + "\\" + _fileName;
                            streamWrite = new StreamWriter(file, true);
                        }
                        streamWrite.WriteLine(record.LogMessage);
                        logRecordList.Remove(record);

                    } while (logRecordList.Count > 0);

                    //logRecordList.Clear();

                    streamWrite.Flush();
                    streamWrite.Close();
                }
                catch (Exception ex)
                {
                    //throw ExceptionHelper.GetException(20002001, "TXTLogService_Save", ex);
                }
                finally
                {
                    streamWrite.Close();
                }
            }
        }

        public void Load()
        {

        }
    }
}
