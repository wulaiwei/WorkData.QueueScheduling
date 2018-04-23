// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：UserOAuthRequest.cs
// 创建标识：吴来伟 2018-03-29 9:31
// 创建描述：
//
// 修改标识：吴来伟2018-03-29 9:31
// 修改描述：
//  ------------------------------------------------------------------------------
namespace WorkData.NancyHost.Models.Tokens
{
    public class UserOAuthRequest
    {
        public string AccountNumber { get; set; }

        public string Password { get; set; }

        //public string VerificationCode { get; set; }
    }
}