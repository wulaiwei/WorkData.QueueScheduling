// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：WorkDataNancyBootstrapper.cs
// 创建标识：吴来伟 2018-03-27 15:16
// 创建描述：
//
// 修改标识：吴来伟2018-03-27 15:16
// 修改描述：
//  ------------------------------------------------------------------------------
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using WorkData.NancyHost.Cache;
using WorkData.NancyHost.Models.Tokens;
using WorkData.NancyHost.ResponseHandler;
using WorkData.Service.Domain.Messges.Services;
using WorkData.Service.Domain.UserBases.Services;

namespace WorkData.NancyHost.Infrastructure
{
    public class WorkDataNancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            DiagnosticsHook.Disable(pipelines);

            //替换默认序列化方式
            container.Register<ISerializer, CustomJsonNetSerializer>();
            container.Register<IUserMapper, UserMapper>();
            container.Register<ICacheManager, RedisCacheManager>();
            container.Register<IMessageService, MessageService>();

            #region 业务注入

            container.Register<IUserBaseService, UserBaseService>();

            #endregion 业务注入

            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type,Access-Token");
                ctx.Response.Headers.Add("Access-Control-Expose-Headers", "*");
            };

            var configuration = new StatelessAuthenticationConfiguration(
                nancyContext =>
            {
                //返回null代码token无效或用户未认证
                var token = nancyContext.Request.Headers.Authorization;
                var userValidator = container.Resolve<IUserMapper>();
                return !string.IsNullOrEmpty(token) ?
                    userValidator.GetUserFromAccessToken(token) : null;
            }
                );

            StatelessAuthentication.Enable(pipelines, configuration);

            //启用Session
            //CookieBasedSessions.Enable(pipelines);

            base.ApplicationStartup(container, pipelines);
        }

        /// <summary>
        /// RootPathProvider
        /// </summary>
        protected override IRootPathProvider RootPathProvider =>
            new WorkDataRootPathProvider();

        /// <summary>
        /// 配置静态文件访问权限
        /// </summary>
        /// <param name="conventions"></param>
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            //静态文件夹访问 设置 css,js,image
            conventions.StaticContentsConventions.AddDirectory("Contents");
        }
    }
}