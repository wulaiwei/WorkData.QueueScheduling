// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：IUserMapper.cs
// 创建标识：吴来伟 2018-03-28 17:44
// 创建描述：
//
// 修改标识：吴来伟2018-03-28 17:44
// 修改描述：
//  ------------------------------------------------------------------------------

using Nancy.Security;

namespace WorkData.NancyHost.Models.Tokens
{
    public interface IUserMapper
    {
        string GenerateToken(string userName);

        IUserIdentity GetUserFromAccessToken(string token);
    }
}