using WorkData.Service.Core.DomainServices;
using WorkData.Service.Core.Entity;
using WorkData.Util.Common.Logs;

namespace WorkData.Service.Application.Impls
{
    public class UpdateDimainService : BaseDomainService
    {
        public override void Execute(Message message)
        {
            LoggerHelper.SystemLog.Error($"update:{message.Key}");
        }
    }
}