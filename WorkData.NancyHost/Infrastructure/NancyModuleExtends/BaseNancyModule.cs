// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：BaseNancyModule.cs
// 创建标识：吴来伟 2018-03-27 14:48
// 创建描述：
//
// 修改标识：吴来伟2018-03-27 14:54
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using Nancy;
using System;

#endregion

namespace WorkData.NancyHost.Infrastructure.NancyModuleExtends
{
    /// <summary>
    ///     BaseNancyModule
    /// </summary>
    public abstract class BaseNancyModule : NancyModule
    {
        protected BaseNancyModule()
        {
        }

        protected BaseNancyModule(string modulePath)
            : base(modulePath)
        {
        }

        /// <summary>
        ///     (返回null，继续处理;
        ///     返回Response对象，不再做要干的那件事，换做Response对象要干的事)
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public abstract Response BeforeEvent(NancyContext ctx);

        /// <summary>
        ///     处理之后要干的事。
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public abstract void AfterEvent(NancyContext ctx);

        /// <summary>
        ///     异常事件
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public abstract Response ErrorEvent(NancyContext ctx, Exception ex);
    }
}