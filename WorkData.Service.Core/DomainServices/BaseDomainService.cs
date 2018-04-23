// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.MessageTransfer
// 文件名：BaseDomainService.cs
// 创建标识：吴来伟 2018-04-19 15:59
// 创建描述：
//
// 修改标识：吴来伟2018-04-19 16:00
// 修改描述：
//  ------------------------------------------------------------------------------

#region

#endregion

#region

using WorkData.Service.Core.Entity;

#endregion

namespace WorkData.Service.Core.DomainServices
{
    /// <summary>
    ///     基础服务
    /// </summary>
    public abstract class BaseDomainService : IDomainService
    {
        public abstract void Execute(Message message);
    }
}