// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Domain
// 文件名：IUserBase.cs
// 创建标识：吴来伟 2018-03-29 14:48
// 创建描述：
//
// 修改标识：吴来伟2018-03-29 14:48
// 修改描述：
//  ------------------------------------------------------------------------------

using WorkData.Service.Domain.UserBases.Dtos;

namespace WorkData.Service.Domain.UserBases.Services
{
    public interface IUserBaseService
    {
        UserBase AddUserBase(UserBase entity);

        UserBase GetUserBase(string key);

        UserBase VerificationUserBase(VerificationUserBaseInputDto input);
    }
}