// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Domain
// 文件名：IMessageService.cs
// 创建标识：吴来伟 2018-04-20 11:10
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 14:05
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using System.Collections.Generic;

#endregion

namespace WorkData.Service.Domain.Messges.Services
{
    public interface IMessageService
    {
        void AddMessage(Message entity);

        List<Message> Query();

        void UpdateMessage(string id, bool successed, string content);
    }
}