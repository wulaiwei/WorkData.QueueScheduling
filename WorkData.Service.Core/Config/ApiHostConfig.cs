// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Core
// 文件名：ApiHostConfig.cs
// 创建标识：吴来伟 2018-04-20 14:31
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 14:31
// 修改描述：
//  ------------------------------------------------------------------------------

using System;

namespace WorkData.Service.Core.Config
{
    [Serializable]
    public class ApiHostConfig
    {
        public string AddMessage { get; set; }

        public string UpdateMessage { get; set; }

        public string QueryMessage { get; set; }
    }
}