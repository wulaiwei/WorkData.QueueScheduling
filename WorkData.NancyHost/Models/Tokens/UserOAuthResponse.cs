// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：UserOAuthResponse.cs
// 创建标识：吴来伟 2018-04-08 17:37
// 创建描述：
//
// 修改标识：吴来伟2018-04-08 17:37
// 修改描述：
//  ------------------------------------------------------------------------------
namespace WorkData.NancyHost.Models.Tokens
{
    public class UserOAuthResponse
    {
        public string AccessToken { get; set; }

        public string AccountNumber { get; set; }
    }
}