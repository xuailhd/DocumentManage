using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DocumentManage.Common
{
    /// <summary>
    /// 不直接调用
    /// </summary>
    public static class LogHelper
    {
        readonly static ILog log = LogManager.GetLogger("DefaultLogger");

        static LogHelper()
        {
            log4net.Util.LogLog.InternalDebugging = true;
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="info"></param>
        public static void WriteInfo(string info)
        {
            log.Info(info);
        }

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteError(Exception ex)
        {
            log.Error(ex.Message, ex);
        }

        public static void WriteError(string errorinfo)
        {
            log.Error(errorinfo);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="info"></param>
        //protected static void WriteWarn(string info,Exception ex=null)
        //{
        //    log.Warn(info, ex);
        //}

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="info"></param>
        public static void WriteDebug(string info, Exception ex = null)
        {
            log.Debug(info,ex);
        }
        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="info"></param>
        //protected static void WriteDebug(string info, string ex)
        //{
        //    log.Debug(info, new Exception(ex));
        //}
    }
}