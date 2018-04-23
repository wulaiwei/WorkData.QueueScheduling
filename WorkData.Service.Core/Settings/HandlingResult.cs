// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Core
// 文件名：HandlingResult.cs
// 创建标识：吴来伟 2018-03-22 15:00
// 创建描述：
//
// 修改标识：吴来伟2018-03-22 15:03
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using System;
using WorkData.Service.Core.Entity;

#endregion

namespace WorkData.Service.Core.Settings
{
    /// <summary>
    ///     HandlingResult
    /// </summary>
    public class HandlingResult
    {
        /// <summary>
        ///     获取或设置处理结果，默认值为true。
        /// </summary>
        public bool Successed { get; set; }

        /// <summary>
        ///     获取或设置处理消息，默认值为空。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Exception
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        ///     获取或设置已处理的消息内部编号
        /// </summary>
        public int MsgNumber { get; set; }

        /// <summary>
        ///     获取或设置处理其他结果，默认值为null。
        /// </summary>
        public Message Result { get; set; }

        /// <summary>
        ///     构造函数
        /// </summary>
        public HandlingResult()
        {
            Successed = true;
            Message = string.Empty;
            Result = null;
        }
    }
}