// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：NancyAspHttpRequestHandler.cs
// 创建标识：吴来伟 2018-04-09 11:29
// 创建描述：
//
// 修改标识：吴来伟2018-04-09 11:29
// 修改描述：
//  ------------------------------------------------------------------------------

using Nancy.Hosting.Aspnet;
using System.Web.SessionState;

namespace WorkData.NancyHost.Infrastructure
{
    public class NancyAspHttpRequestHandler : NancyHttpRequestHandler, IRequiresSessionState
    {
    }
}