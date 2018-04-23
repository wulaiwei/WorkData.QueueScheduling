// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Application
// 文件名：WorkDataServiceApplicationModule.cs
// 创建标识：吴来伟 2018-04-19 16:59
// 创建描述：
//
// 修改标识：吴来伟2018-04-19 16:59
// 修改描述：
//  ------------------------------------------------------------------------------

using Autofac;
using WorkData.Extensions.Modules;
using WorkData.Extensions.Types;
using WorkData.Service.Core.DomainServices;

namespace WorkData.Service.Application
{
    public class WorkDataServiceApplicationModule : WorkDataBaseModule
    {
        private readonly ILoadType _loadType;

        public WorkDataServiceApplicationModule()
        {
            _loadType = NullLoadType.Instance;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterMatchDomainService();
        }

        /// <summary>
        ///     RegisterMatchDbContexts
        /// </summary>
        private void RegisterMatchDomainService()
        {
            if (IocManager == null)
                return;
            var types = _loadType.GetAll(x => x.IsPublic && x.IsClass && !x.IsAbstract
                                              && typeof(IDomainService).IsAssignableFrom(x));
            var builder = new ContainerBuilder();
            foreach (var type in types)
            {
                builder.RegisterType(type).Named<IDomainService>(type.FullName);
            }
            IocManager.UpdateContainer(builder);
        }
    }
}