// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：BootstrapperExtensions.cs
// 创建标识：吴来伟 2018-04-09 11:27
// 创建描述：
//
// 修改标识：吴来伟2018-04-09 11:27
// 修改描述：
//  ------------------------------------------------------------------------------

using Nancy.Bootstrapper;
using Nancy.Session;
using System.Collections.Generic;

namespace WorkData.NancyHost.Infrastructure
{
    public static class BootstrapperExtensions
    {
        public static void WithSession(this IPipelines pipeline, IDictionary<string, object> session)
        {
            pipeline.BeforeRequest.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Request.Session = new Session(session);
                return null;
            });
        }
    }
}