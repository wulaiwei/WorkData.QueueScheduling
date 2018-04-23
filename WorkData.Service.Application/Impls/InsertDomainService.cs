// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Application
// 文件名：InsertDomainService.cs
// 创建标识：吴来伟 2018-04-17 17:20
// 创建描述：
//
// 修改标识：吴来伟2018-04-19 16:20
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using WorkData.Service.Core.DomainServices;
using WorkData.Service.Core.Entity;
using WorkData.Util.Common.Logs;

#endregion

namespace WorkData.Service.Application.Impls
{
    /// <summary>
    ///     InsertDomainService
    /// </summary>
    public class InsertDomainService : BaseDomainService
    {
        public override void Execute(Message message)
        {
            LoggerHelper.SystemLog.Error($"insert:{message.Key}");
        }
    }
}