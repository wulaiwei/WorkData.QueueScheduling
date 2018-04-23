// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Application
// 文件名：DomainServiceExtensionHelper.cs
// 创建标识：吴来伟 2018-03-22 16:14
// 创建描述：
//
// 修改标识：吴来伟2018-03-22 16:14
// 修改描述：
//  ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkData.Extensions.Types;
using WorkData.Service.Core.Settings;
using WorkData.Util.Common.Reflections;

namespace WorkData.Service.MessageTransfer.Extensions
{
    /// <summary>
    /// DomainServiceExtensionHelper
    /// </summary>
    public class DomainServiceExtensionHelper
    {
        public static IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType)
        {
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(ICallBack))
                select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property.DeclaringType);
        }
    }
}