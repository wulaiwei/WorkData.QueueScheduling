// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：WebNancyModule.cs
// 创建标识：吴来伟 2018-03-27 14:48
// 创建描述：
//
// 修改标识：吴来伟2018-03-27 14:48
// 修改描述：
//  ------------------------------------------------------------------------------

using Nancy;
using System;
using WorkData.Util.Common.ExceptionExtensions;

namespace WorkData.NancyHost.Infrastructure.NancyModuleExtends
{
    public class WebNancyModule : BaseNancyModule
    {
        public WebNancyModule()
        {
            Before += BeforeEvent;

            After += AfterEvent;

            OnError += ErrorEvent;
        }

        /// <summary>
        /// BeforeEvent
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public override Response BeforeEvent(NancyContext ctx)
        {
            return null;
        }

        /// <summary>
        /// AfterEvent
        /// </summary>
        /// <param name="ctx"></param>
        public override void AfterEvent(NancyContext ctx)
        {
        }

        /// <summary>
        /// ErrorEvent
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override Response ErrorEvent(NancyContext ctx, Exception ex)
        {
            if (ex is UserFriendlyException)
            {
                ctx.NegotiationContext.StatusCode = HttpStatusCode.OK;
            }

            return null;
        }
    }
}