// ------------------------------------------------------------------------------
//    Copyright (C) 成都联宇创新科技有限公司 版权所有。
//
//    文件名：DapperConfigurationSectionHandler.cs
//    文件功能描述：
//    创建标识： 2015/06/25
//
//    修改标识：2015/06/27
//    修改描述：
//  ------------------------------------------------------------------------------

#region 导入名称空间

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Xml;

#endregion 导入名称空间

namespace Dapper
{
    /// <summary>
    /// 配置处理程序
    /// </summary>
    public class DapperConfigurationSectionHandler : IConfigurationSectionHandler
    {
        [SecuritySafeCritical]
        public object Create(object parent, object configContext, XmlNode section)
        {
            var asmList = new List<Assembly>();

            #region 加载要映射的程序集

            var assemblyNodeList = section.SelectNodes("//mapping/assembly");
            if (assemblyNodeList == null)
            {
                throw new ArgumentNullException("", "必须填写要映射的程序集。mapping/assembly");
            }

            foreach (XmlElement assemblyNode in assemblyNodeList)
            {
                var assemblyNodeValue = assemblyNode.InnerText.Trim().Replace(".dll", "");
                var assemblyName = string.Format("{0}.dll", assemblyNodeValue);
                var assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName);
                if (!TryGetAssembly(assemblyNodeValue, asm =>
                {
                    if (asmList.All(x => x.FullName != asm.FullName))
                    {
                        asmList.Add(asm);
                    }
                }))
                {
                    var asm = Assembly.LoadFrom(assemblyPath);
                    if (asmList.All(x => x.FullName != asm.FullName))
                    {
                        asmList.Add(asm);
                    }
                }
            }

            #endregion 加载要映射的程序集

            #region 加载数据处理上下文配置

            var propertyNodeList = section.SelectNodes("property");
            var propertyData = new Dictionary<string, string>();
            if (propertyNodeList != null)
            {
                foreach (XmlElement propertyNode in propertyNodeList)
                {
                    var peopName = propertyNode.GetAttribute("name");
                    var peopVal = propertyNode.GetAttribute("value");
                    propertyData.Add(peopName, peopVal);
                }
            }

            string connectionString;
            if (!propertyData.TryGetValue("connection_string", out connectionString))
            {
                var connectionName = propertyData["connection_name"];
                connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            }

            var dbConnImplType = Type.GetType(propertyData["connection_class"]);
            var dapperConfig = new DapperConfigurationXml(dbConnImplType, connectionString, asmList);

            #endregion 加载数据处理上下文配置

            return dapperConfig;
        }

        /// <summary>
        /// 尝试加载 在当前应用程序域的Assembly
        /// </summary>
        /// <param name="asmName"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        private static bool TryGetAssembly(string asmName, Action<Assembly> callback)
        {
            if (AppDomain.CurrentDomain.GetAssemblies().All(asm => asm.GetName().Name.ToUpper() != asmName.ToUpper()))
            {
                return false;
            }

            callback(AppDomain.CurrentDomain.GetAssemblies().First(asm => asm.GetName().Name.ToUpper() == asmName.ToUpper()));
            return true;
        }
    }
}