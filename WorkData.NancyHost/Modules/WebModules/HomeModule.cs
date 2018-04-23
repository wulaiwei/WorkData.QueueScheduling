// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.NancyHost
// 文件名：HomeModule.cs
// 创建标识：吴来伟 2018-04-20 10:30
// 创建描述：
//
// 修改标识：吴来伟2018-04-20 13:48
// 修改描述：
//  ------------------------------------------------------------------------------

#region

using WorkData.NancyHost.Infrastructure.NancyModuleExtends;

#endregion

namespace WorkData.NancyHost.Modules.WebModules
{
    public class HomeModule : WebNancyModule
    {
        //注意：这里是构造函数
        public HomeModule()
        {
            //登录页
            Get["/"] = p => View["/Home/index.html"];

            //默认页
            Get["/home/index"] = p => View["/Home/index.html"];
        }
    }
}