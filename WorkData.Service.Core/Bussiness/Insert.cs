// ------------------------------------------------------------------------------
// Copyright  吴来伟个人 版权所有。
// 项目名：WorkData.Service.Core
// 文件名：Insert.cs
// 创建标识：吴来伟 2018-03-22 15:08
// 创建描述：
//
// 修改标识：吴来伟2018-03-22 15:08
// 修改描述：
//  ------------------------------------------------------------------------------

using WorkData.Service.Core.Settings;

namespace WorkData.Service.Core.Bussiness
{
    public class Insert : ICallBack
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}