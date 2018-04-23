// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Application
// 文件名：IWoekDataSubscription.cs
// 创建标识：吴来伟 2018-03-26 17:36
// 创建描述：
//
// 修改标识：吴来伟2018-03-26 17:36
// 修改描述：
//  ------------------------------------------------------------------------------

using WorkData.Service.Core.Entity;

namespace WorkData.Service.MessageTransfer.Dependency
{
    /// <summary>
    /// IWorkDataSubscription
    /// </summary>
    public interface IWorkDataTask
    {
        void CreateTask(Message message);
    }
}