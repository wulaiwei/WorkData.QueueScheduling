// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：WorkDataRootPathProvider.cs
// 创建标识：吴来伟 2018-04-03 10:23
// 创建描述：
//
// 修改标识：吴来伟2018-04-03 10:23
// 修改描述：
//  ------------------------------------------------------------------------------

using Nancy;
using System;

namespace WorkData.NancyHost.Infrastructure
{
    public class WorkDataRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return AppDomain.CurrentDomain.GetData(".appPath").ToString();
        }
    }
}