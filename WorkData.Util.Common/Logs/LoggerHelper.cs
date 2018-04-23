// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Util.Common
// 文件名：LoggerHelper.cs
// 创建标识：吴来伟 2018-03-23 11:31
// 创建描述：
//
// 修改标识：吴来伟2018-03-23 11:31
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using NLog;

#endregion

namespace WorkData.Util.Common.Logs
{
    public class LoggerHelper
    {
        public static volatile ILogger BusinessLog = LogManager.GetLogger("businessLog");
        public static volatile ILogger SystemLog = LogManager.GetLogger("systemLog");
        public static volatile ILogger ApiLog = LogManager.GetLogger("apiLog");
    }
}