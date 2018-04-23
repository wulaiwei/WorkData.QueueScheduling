// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：OAuthModule.cs
// 创建标识：吴来伟 2018-03-29 9:22
// 创建描述：
//
// 修改标识：吴来伟2018-03-29 9:22
// 修改描述：
//  ------------------------------------------------------------------------------

using Nancy.ModelBinding;
using WorkData.NancyHost.Infrastructure.NancyModuleExtends;
using WorkData.NancyHost.Models.Tokens;
using WorkData.NancyHost.ResponseHandler;
using WorkData.Service.Domain.UserBases.Services;

namespace WorkData.NancyHost.Modules.OAuthModules
{
    public class AuthModule : OAuthModule
    {
        public AuthModule(IUserMapper userMapper, IUserBaseService userBaseService)
        {
            Post["/auth/login"] = x =>
            {
                var userOAuthRequest = this.Bind<UserOAuthRequest>();
                if (userOAuthRequest == null)
                    return Response.AsErrorJson("非法访问！");

                //todo 暂不进行dto验证 默认登录成功
                //设置token并返回结果集
                var token = userMapper.GenerateToken(userOAuthRequest.AccountNumber);
                var userOAuthResponse = new UserOAuthResponse
                {
                    AccessToken = token,
                    AccountNumber = userOAuthRequest.AccountNumber
                };
                return Response.AsSuccessJson(userOAuthResponse);
            };
        }
    }
}